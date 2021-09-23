//////////////////////////////////////////////////////////////////////////////
// File:     CInstrSettings.hpp
// Contains: Class for managing instrument settings
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// History
///////////////////////////////////////////////////////////////////////////////
// Date      Eng  PCR  Description
// --------  ---  ---  --------------------------------------------------------
// 06/22/05  AJP       Initial creation
///////////////////////////////////////////////////////////////////////////////

#ifndef INCLUDED_CINSTRSETTINGS_CLASS_HPP
#define INCLUDED_CINSTRSETTINGS_CLASS_HPP
#include <string>

class CInstrSettings
{
  public:
	CInstrSettings ();
	CInstrSettings (const char * FileName);
	~CInstrSettings ();
	bool SetFileName(const char * FileName);
	bool SetFileName(std::string FileName);
	bool getFileSize(long &fileSize);

	template <class T1> 
	bool  SaveInstrSettings (T1 InstrSet)
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
		
	template <class T1> 
	bool RestoreInstrSettings (T1 &InstrSet)
	{
		FILE *stream;
		if (mp_File == NULL)
			return false;

		if ( (stream = fopen( mp_File, "r+b" )) != NULL )
		{
			fread( &InstrSet, sizeof(InstrSet), 1, stream );
			fclose( stream );
		}
		else
		{
		//	StoreErrors.SetError(NO_RFID_ERROR, "call to fopen () failed in CRFMa::SaveInstrSettings");   
			return false;
		}
		return true;
	};

	
  template <class T1> 
	bool  SaveText (T1 InstrSet)
	{
		FILE *stream;
		if (mp_File == NULL)
			return false;

		if ( (stream = fopen( mp_File, "w+t" )) != NULL )
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
		
	template <class T1> 
	bool ReadText (T1 &InstrSet)
	{
		FILE *stream;
		if (mp_File == NULL)
			return false;

		if ( (stream = fopen( mp_File, "r+t" )) != NULL )
		{
			fread( &InstrSet, sizeof(InstrSet), 1, stream );
			fclose( stream );
		}
		else
		{
		//	StoreErrors.SetError(NO_RFID_ERROR, "call to fopen () failed in CRFMa::SaveInstrSettings");   
			return false;
		}
		return true;
	};

  
  private:
	char *mp_File;
};
#endif
