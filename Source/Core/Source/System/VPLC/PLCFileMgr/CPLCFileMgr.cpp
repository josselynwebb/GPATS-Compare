//-----------------------------------------------------------------------------
// File:     CPLCFileMgr.cpp
// Contains: Implementaion of the CPLCFileMgr class for saving and restoring 
//           Path Loss compensation data for the TETS System.
//-----------------------------------------------------------------------------

//-----------------------------------------------------------------------------
// History
//-----------------------------------------------------------------------------
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 05/01/06  AJP       Initial creation

//-----------------------------------------------------------------------------

#include "CPLCFileMgr.hpp"
#include "CInstrSettings.hpp"
#include <string>
#include <math.h>
#include "PLCErrors.h"
#include <CSpline.h>


CPLCFileMgr::CPLCFileMgr() : START_FREQ(100.0f), STOP_FREQ(18e3f), STEP_FREQ(100.0f){

	// this needs to be determined from INI file CODE_CHECK
	m_fileName="C:\\Program Files\\DME Corporation\\RFMS\\Source\\ConfigFiles\\PLC.dat"; //CODE_CHECK
	
	for (int i=0; i<END_PLC_OBJECTS; i++){
		m_FreqPathSpline[i] = -1;
	}
	initPLCData();
}


CPLCFileMgr::~CPLCFileMgr(){

}

//-----------------------------------------------------------------------------
// clearPLCData()
// Sets all of the calibration data to zero.
//-----------------------------------------------------------------------------
int CPLCFileMgr::clearPLCData(){

	long PLCVecLen;
	for (int j=0; j<END_PLC_OBJECTS; j++){

		PLCVecLen = m_PLCDataVec[j].size();
		for (int i=0; i<PLCVecLen;i++){
			m_PLCDataVec[j][i].freqMHz;
			m_PLCDataVec[j][i].CF_dBm= 0;
		}	
	}
	return 0;
}

//-----------------------------------------------------------------------------
// initPLCData()
// Creates all of the elements required for the m_PLCDataVec and initializes the
//  frequency and data portions.
//-----------------------------------------------------------------------------
int CPLCFileMgr::initPLCData(){

	st_PLCData lPLCData;
	float freqMhz;
	
	for (int j=0; j<END_PLC_OBJECTS; j++){
		
		freqMhz = START_FREQ;
		for (int i=0; i<MAX_POINTS;i++){
			lPLCData.freqMHz = freqMhz;
			lPLCData.CF_dBm= 0;
			m_PLCDataVec[j].push_back(lPLCData);
			freqMhz += STEP_FREQ;
		}	
	}
	return 0;
}

//-----------------------------------------------------------------------------
// readPLCFile()
//  Description:
//  Opens and begins reading the PLC data file. It loads the vector m_PLCDataVec,
//   with the PLC data so that it can be recovered via the "get" methods.
//   returns -1 if the PLC data file was not found or could not be opened.
//-----------------------------------------------------------------------------
int CPLCFileMgr::readPLCFile(){

	CInstrSettings PLCDataReader(m_fileName.c_str());
	st_PLCDataFile lPLCDataFile;
	int lowFreqPoints = 0;
	int j;

	if (PLCDataReader.RestoreInstrSettings(lPLCDataFile) == false){
		m_ErrorCode = FILE_IO;
		m_ErrorDesc = "File not found in CPLCFileMgr::read().";
		return -1;
	}
	
	// copy into vector
	for (j=0; j<END_PLC_OBJECTS; j++){
		
		for (int i=0; i<MAX_POINTS; i++){
			m_PLCDataVec[j].push_back(lPLCDataFile.PLCData[j][i]);
		}
		initPLCSpline(j, MAX_POINTS);
	}
	return 0;
}

//-----------------------------------------------------------------------------
// savePLCData()
// Copy the m_PLCDataFile structure to the hard drive. This should be done after
//  all of the data for all of the paths has been loaded into m_PLCDataFile via
//  the method savePLCPathData.
//-----------------------------------------------------------------------------
int CPLCFileMgr::savePLCData(){

	CInstrSettings PLCDataSaver(m_fileName.c_str());
	int vectorSize;
	st_PLCDataFile PLCDataFile;
		
	for (int j=0; j<END_PLC_OBJECTS; j++){
		
		vectorSize = m_PLCDataVec[j].size();
		if (vectorSize > MAX_POINTS){
			vectorSize = MAX_POINTS;
		}
		
		for (int i=0; i<MAX_POINTS; i++){
			if (i >= vectorSize){
				PLCDataFile.PLCData[j][i].freqMHz = 0;
				PLCDataFile.PLCData[j][i].CF_dBm = 0;
			}else{
				PLCDataFile.PLCData[j][i] = m_PLCDataVec[j][i];
			}
		}
	}
	
	// copy to hard drive
	if (PLCDataSaver.SaveInstrSettings(PLCDataFile) == false){
		m_ErrorCode = FILE_IO;
		m_ErrorDesc = "File not found in CPLCFileMgr::savePLCData().";
		
		return -1;
	}
	return 0;
}

//-----------------------------------------------------------------------------
// setPLCPathData()
// Copy the data for a path, frequency and Correction factor.
//-----------------------------------------------------------------------------
int CPLCFileMgr::setPLCPathData(long path, float freqMHz, float CF_dBm){

	st_PLCData lPLCData;
	int upperIndex, lowerIndex;

	if (path < S901_2 || path >= END_PLC_OBJECTS){
		m_ErrorCode = PATH_ERROR;
		m_ErrorDesc = "Invalid path in CPLCFileMgr::setPLCPathData";
		return -1;
	}
	// search to see if this freq exists in vector
	if (searchFreq(path, freqMHz, upperIndex, lowerIndex) == 0){
	
		if (m_PLCDataVec[path][upperIndex].freqMHz == freqMHz){
			m_PLCDataVec[path][upperIndex].CF_dBm = CF_dBm;
			return 0;
		}
	}	
	lPLCData.freqMHz = freqMHz;
	lPLCData.CF_dBm = CF_dBm;
	m_PLCDataVec[path].push_back(lPLCData);
	return 0;
}

//-----------------------------------------------------------------------------
// getCalData()
//  Returns -1 if the vector m_PLCDataVec[path] is empty.
//-----------------------------------------------------------------------------
int CPLCFileMgr::getPLCData(const int path, const float freqMHz, float &CF_dBm){

	int upperIndex, lowerIndex;
//	int theIndex;
//	float upperDiff, lowerDiff;
	double cf;

	if (searchFreq(path, freqMHz, upperIndex, lowerIndex) == 0){
		if (m_PLCDataVec[path][upperIndex].freqMHz == freqMHz){
			CF_dBm = m_PLCDataVec[path][upperIndex].CF_dBm;
		}else{
			// *** ROUND TO THE NEAREST CAL DATA FREQ *** // CODE_CHECK
		/*	
			upperDiff = m_PLCDataVec[path][upperIndex].freqMHz - freqMHz;
			upperDiff = sqrt(upperDiff*upperDiff);
			lowerDiff = freqMHz - m_PLCDataVec[path][lowerIndex].freqMHz;
			lowerDiff = sqrt(lowerDiff*lowerDiff);
			if (upperDiff < lowerDiff){
				theIndex = upperIndex;
			}else{
				theIndex = lowerIndex;
			}
			
			CF_dBm = m_PLCDataVec[path][theIndex].CF_dBm;
		*/	
			// *** USE SPLINE TO DO INTERPILATION *** // CODE_CHECK
			// get CF from spline
			//evalSpline(mp_LowFreqPathSpline, mp_LowFreqPathAccel, (double)freqMHz, (double *)&gain);
			evalSpline(m_FreqPathSpline[path], (double)freqMHz, &cf); 
			CF_dBm = cf;
		}
	}else
		return -1;
	return 0;
}

//-----------------------------------------------------------------------------
// exportToExcel()
//  Read the binary frequency and correction factor data for each path, and
//  formats it as comma delimited file that can be viewed in Microsoft Excel.
//-----------------------------------------------------------------------------
int CPLCFileMgr::exportToExcel(std::string fileName){

	int retVal=0;
	std::string tmps;
	FILE *stream;
	int i, j;
	
	//fileName = "C:\\Program Files\\DME Corporation\\RFMS\\Source\\ConfigFiles\\PLC.xls";
	if (fileName == ""){
		m_ErrorCode = FILE_IO;
		m_ErrorDesc = "Invalid file name passed into CPLCFileMgr::exportToExcel().";
		return -1;
	}
	CInstrSettings CalDataSaver(fileName.c_str());
	
	// get the data
	if (readPLCFile() != 0)
		return -1;

	if ( (stream = fopen(fileName.c_str(), "w+t" )) == NULL ){
		m_ErrorCode = FILE_IO;
		m_ErrorDesc = "Could not open PLC file in CPLCFileMgr::exportToExcel().";
		return -1;
		fclose(stream);
	}
	
	// get data and format for excel
	for (j=0; j<END_PLC_OBJECTS; j++){
		
		// write path string to file
		tmps = getPathString(j);
		fprintf(stream,"%s,",tmps.c_str());
		/*	
		// loop through inserting frequencies
		for (i=0; i<MAX_POINTS; i++){
			
			fprintf(stream,"%1.3f,",m_PLCDataVec[j][i].freqMHz);
		}
		
		fprintf(stream,"\n,",tmps.c_str());
		*/
		// loop through inserting power
		for (i=0; i<MAX_POINTS; i++){
			
			fprintf(stream,"%1.3f,", m_PLCDataVec[j][i].CF_dBm);
		}
		fprintf(stream,"\n");
	}
	fclose(stream);
	return retVal;
}

//-----------------------------------------------------------------------------
// exportSplinesToExcel()
//  Read the binary frequency and correction factor data for each path at 10MHz
//  intervals, and formats it as comma delimited file that can be viewed in
//  Microsoft Excel. This allows the results of the splines contribution to be
//  Evaluated.
//-----------------------------------------------------------------------------
int CPLCFileMgr::exportSplinesToExcel(std::string fileName){

	int retVal=0;
	std::string tmps;
	FILE *stream;
//	int i, j;
	float CF_dBm;
	float freqMHz;
	
	if (fileName == ""){
		m_ErrorCode = FILE_IO;
		m_ErrorDesc = "Invalid file name passed into CPLCFileMgr::exportToExcel().";
		return -1;
	}
	CInstrSettings CalDataSaver(fileName.c_str());
	
	// get the data
	if (readPLCFile() != 0)
		return -1;

	if ( (stream = fopen(fileName.c_str(), "w+t" )) == NULL ){
		m_ErrorCode = FILE_IO;
		m_ErrorDesc = "Could not open PLC file in CPLCFileMgr::exportToExcel().";
		return -1;
		fclose(stream);
	}
	
	// get data and format for excel
	//for (j=0; j<END_PLC_OBJECTS; j++){
		
		// write path string to file
		//tmps = getPathString(j);
		//fprintf(stream,"%s\nFreqMHz,",tmps.c_str());
		
		freqMHz = 100.0;
		// loop through inserting frequencies
		while (freqMHz <= 8500.0){

			getPLCData(2, freqMHz, CF_dBm);
			fprintf(stream,"%1.3f,%1.3f\n",freqMHz,CF_dBm);
			freqMHz += 10.0;
		}
	//}
	fclose(stream);
	return retVal;
}

//-----------------------------------------------------------------------------
// searchFreq() -- PRIVATE METHOD
//  Description: Searches the vector for a match or the nearest freqs. If a match
//   is found then LowerIndex returns -1. If no match is found the UpperIndex and
//   LowerIndex contain the upper and lower frequencies that the input FreqMHz lies
//   between.
//  Returns -1 if the vector m_CalDataVec is empty.
//-----------------------------------------------------------------------------
int CPLCFileMgr::searchFreq(const int path, const float FreqMHz, int &UpperIndex, int &LowerIndex)
{
	float lastFreqDiff=1e12f;
	float currentFreqDiff;
	float tempf;
	int lastIndex=0;
	long vectorSize;

	
	// return if the m_CalDataVec is empty -- also assign vectorSize for the for loop
	if ((vectorSize = m_PLCDataVec[path].size()) == 0){
		return -1;
	}

	for (int i=0; i<vectorSize; i++)
	{
		tempf = m_PLCDataVec[path][i].freqMHz;	// ENABLES DEBUG SINCE THE DEBUGGER CANNOT EVALUATE m_CalDataVec[i]
		if (m_PLCDataVec[path][i].freqMHz == FreqMHz){
			UpperIndex = i;
			LowerIndex = -1;
			return 0;
		}else{
			currentFreqDiff = m_PLCDataVec[path][i].freqMHz - FreqMHz;
			currentFreqDiff = sqrt(currentFreqDiff*currentFreqDiff);
				
			if (lastFreqDiff >= currentFreqDiff || i == 1){	// || i==1 ensures we always get at least an upper freq
				if(m_PLCDataVec[path][lastIndex].freqMHz > m_PLCDataVec[path][i].freqMHz){
					// last freq higher than current freq
					UpperIndex = lastIndex;
					LowerIndex = i;
				}else{
					LowerIndex = lastIndex;
					UpperIndex = i;
				}
				lastFreqDiff = currentFreqDiff;
				lastIndex = i;
			}
		}
	}
	return 0;
}

//-----------------------------------------------------------------------------
// initFreqSpline -- PRIVATE METHOD
//-----------------------------------------------------------------------------
int CPLCFileMgr::initPLCSpline(int path, int size){

	double * FreqArray = new double[size];
	double * CFArray = new double[size];

	if (m_FreqPathSpline[path] != -1){
		// spline has already been initialized.
		return 0;
	}

	// copy low frequency portion of the vector
	for (int i=0; i<size; i++){
		FreqArray[i] = m_PLCDataVec[path][i].freqMHz;
		CFArray[i] = m_PLCDataVec[path][i].CF_dBm;
	}
		
	initSpline(&m_FreqPathSpline[path], FreqArray, CFArray, size);
	
	delete [] FreqArray;
	delete [] CFArray; 
	return 0;	
}

//-----------------------------------------------------------------------------
// getError()
//  Returns error information about the last error.
//-----------------------------------------------------------------------------
int CPLCFileMgr::getError(int &ErrorCode, char * ErrorDesc, int EDLen){

	ErrorCode = m_ErrorCode;
	if (strlen(m_ErrorDesc.c_str()) > EDLen){
		strncpy(ErrorDesc,m_ErrorDesc.c_str(),EDLen);
	}else{
		strcpy(ErrorDesc,m_ErrorDesc.c_str());
	}
	return 0;
}

//-----------------------------------------------------------------------------
// getPathString()
//  Converts the const version of the paths to the string representation, used
//  for displaying the data in a Microsoft excel format.
//-----------------------------------------------------------------------------
std::string CPLCFileMgr::getPathString(int path){	
	switch (path){

		case POWER_METER:
			return("POWERMETER");
		case S901_2:
			return ("S901-2");
		case S901_3:
			return ("S901-3");
		case S901_4:
			return ("S901-4");
		case S901_5:
			return ("S901-5");
		case S901_6:
			return ("S901-6");
		case S901_7:
			return ("S901-7");		
		case S902_2:
			return ("S902-2");		
		case S902_3:
			return ("S902-3");
		case S902_4:
			return ("S902-4");		
		case S902_5:
			return ("S902-5");		
		case S902_6:
			return ("S902-6");
		case S902_7:
			return ("S902-7");	
		case S903_2:
			return ("S903-2");
		case S903_3:
			return ("S903-3");
		case S903_4:
			return ("S903-4");
		case S903_5:
			return ("S903-5");
		case S903_6:
			return ("S903-6");
		case S903_7:
			return ("S903-7");
		case S904_2:
			return ("S904-2");
		case S904_3:
			return ("S904-3");
		case S904_4:
			return ("S904-4");
		case S904_5:
			return ("S904-5");
		case S904_6:
			return ("S904-6");
		case S904_7:
			return ("S904-7");
		case S905_2:
			return ("S905-2");
		case S905_3:
			return ("S905-3");
		case S905_4:
			return ("S905-4");
		case S905_5:
			return ("S905-5");
		case S905_6:
			return ("S905-6");
		case S905_7:
			return ("S905-7");
		case S906_2:
			return ("S906-2");
		case S906_3:
			return ("S906-3");
		case S906_4:
			return ("S906-4");
		case S906_5:
			return ("S906-5");
		case S906_6:
			return ("S906-6");
		case S906_7:
			return ("S906-7");
	}
	return "";
}