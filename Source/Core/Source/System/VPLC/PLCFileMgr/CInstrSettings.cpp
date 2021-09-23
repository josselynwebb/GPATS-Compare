//-----------------------------------------------------------------------------
// File:     CInstrSettings.cpp
// Contains: Class for managing instrument settings
//-----------------------------------------------------------------------------/

//-----------------------------------------------------------------------------/
// History
//-----------------------------------------------------------------------------/
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 06/22/05  AJP       Initial creation
//-----------------------------------------------------------------------------/

#include "CInstrSettings.hpp"

CInstrSettings::CInstrSettings() : mp_File(NULL)
{

}

CInstrSettings::CInstrSettings (const char * FileName)
{
	mp_File = NULL;
	SetFileName(FileName);
}

CInstrSettings::~CInstrSettings()
{
	if (mp_File != NULL)
		delete [] mp_File;
}

bool CInstrSettings::SetFileName(const char * FileName)
{
	if (mp_File != NULL)
		delete [] mp_File;
	mp_File = new char [strlen(FileName) + 1];
	strcpy(mp_File,FileName);
	return true;
}

bool CInstrSettings::SetFileName(std::string FileName)
{
	if (mp_File != NULL)
		delete [] mp_File;
	mp_File = new char [FileName.size()]; // allocate memory for mp_File
	//mp_File = FileName.c_str();
	FileName.copy(mp_File, FileName.size(), 0);
	mp_File[FileName.size()] = 0;		// add null to the end
	return true;
}

bool CInstrSettings::getFileSize(long &fileSize)
{
	long lPosition;
	FILE *stream;

	if (mp_File == NULL)
		return false;

	if (mp_File == NULL)
			return false;

	if ( (stream = fopen( mp_File, "r+b" )) == NULL ){
		return false;
	}
	
	if (fseek(stream, 0, SEEK_SET) != 0){
		fclose( stream );
		return false;
	}
	
	lPosition = ftell(stream);

	if (fseek(stream, 0, SEEK_END) != 0){
		fclose( stream );
		return false;
	}
	
	fileSize = ftell(stream) - lPosition;
	fclose(stream);
	return true;
}

/*
bool CInstrSettings::SaveInstrSettingsST(T_ClientData InstrSet)
{
		FILE *stream;
		if (mp_File == NULL)
			return false;

		if ( (stream = fopen( mp_File, "w+b" )) != NULL )
		{
			fwrite( &InstrSet, sizeof(InstrSet), 1, stream );
			fclose( stream );
		}
		else
		{
			//StoreErrors.SetError(NO_RFID_ERROR, "call to fopen () failed in CRFMa::SaveInstrSettings");   
			return false;
		}
		return true;	
};
*/


