//////////////////////////////////////////////////////////////////////////////
// File:     CPLCFileMgr.hpp
// Contains: Class dealing with PLC calibration files
///////////////////////////////////////////////////////////////////////////////
// Usage Notes:
// Write Path Loss data
//  1.) instanite the class
//  2.) call the set method to store the Path Loss data
//  3.) call savePLCData() when finished. This save the data to a file
//      on the system drive.
// Reading Path Loss data
//  1.) instaniate the class
//  2.) call the read() method to read the Path Loss data from a
//      file on the system drive.
//  3.) use the get method to recover the PLC data at a specific frequency.
///////////////////////////////////////////////////////////////////////////////
// History
///////////////////////////////////////////////////////////////////////////////
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 02/28/06  AJP       Initial creation
///////////////////////////////////////////////////////////////////////////////

#pragma warning( disable : 4786 ) // disable warning that debug cannot support the vector class
#include <vector>
#include <string>


#ifndef INCLUDED_CPLCFILEMGR_CLASS_HPP
#define INCLUDED_CPLCFILEMGR_CLASS_HPP

class CPLCFileMgr{

	public:
	CPLCFileMgr();
	~CPLCFileMgr();
	
	const enum { MAX_POINTS = 180
	};
	
	const enum {
		S901_2=0,
		S901_3,
		S901_4,
		S901_5,
		S901_6,
		S901_7,
		S902_2,
		S902_3,
		S902_4,
		S902_5,
		S902_6,
		S902_7,
		S903_2,
		S903_3,
		S903_4,
		S903_5,
		S903_6,
		S903_7,
		S904_2,
		S904_3,
		S904_4,
		S904_5,
		S904_6,
		S904_7,
		S905_2,
		S905_3,
		S905_4,
		S905_5,
		S905_6,
		S905_7,
		S906_2,
		S906_3,
		S906_4,
		S906_5,
		S906_6,
		S906_7,
		POWER_METER,
		END_PLC_OBJECTS
	};

	typedef struct{
		float freqMHz;
		float CF_dBm;
	} st_PLCData;
	
	typedef struct{
		int PLCObjects;
		st_PLCData PLCData[END_PLC_OBJECTS][MAX_POINTS];
	} st_PLCDataFile;
	
	int initPLCData();
	int exportToExcel(std::string fileName);
	int exportSplinesToExcel(std::string fileName);
	int clearPLCData();
	int readPLCFile();
	int savePLCData();
	int setPLCPathData(long path, float freqMHz, float CF_dBm);
	int getPLCData(const int Path, const float freqMHz, float &CF_dBm);
	int getError(int &ErrorCode, char * ErrorDesc, int EDLen);
		
	private:
		std::string m_fileName;
		std::vector<st_PLCData> m_PLCDataVec[END_PLC_OBJECTS];		// for collecting PLC data
		st_PLCDataFile m_PLCDataFile;
		std::string m_ErrorDesc;
		int m_ErrorCode;
		int searchFreq(const int path, const float FreqMHz, int &UpperIndex, int &LowerIndex);
		std::string getPathString(int path);
			
		// spline interp stuff
		long m_FreqPathSpline[END_PLC_OBJECTS];
		int initPLCSpline (const int path, const int size);

		const float START_FREQ;
		const float STOP_FREQ;
		const float STEP_FREQ;
};

#endif
