/////////////////////////////////////////////////////////////////////////////
//	File:	fillindb.cpp													/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					OpenDataBase()											/
//					Changed the name from open_db to present. Added the info/
//					header to correctly reflect the function				/
//					FillInDataBase()										/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					CloseDataBase()											/
//					Changed the name from close_db to present. Added the	/
//					info header to correctly reflect the function			/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <atldbcli.h>
#include <winerror.h>
#include <stdio.h>
#include "stdafx.h"
#include "gpconcur.h"
#include "FaultFile.h"

/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

CFaults m_Faults;

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

HRESULT	hResult;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	OpenDataBase:	This function will try to open a database located at	/
//						d:\\database\\FaultFile.mdb							/
//																			/
// Parameters:																/
//		none:																/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	OpenDataBase(void)
{

	hResult = m_Faults.Open();

//
// Here we open the data base d:\database\FaultFile.mdb, if it doesn't exists then
// I don't know how to make one on the fly so I will exit.
//

	if (FAILED(hResult)) {
		DoDebug(0, "OpenDataBase()", "%s", "Can't open or find d:\\database\\FaultFile.mdb", (char*)NULL);
		return(GP_ERROR);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// FillInDataBase:	This program will if it can fill in the data base		/
//						located at d:\database\FaultFile.mdb.				/
//																			/
// Parameters:																/
//		none:		This function will use the m_Faults, tm and FaultFp		/
//					structures.												/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int FillInDataBase(void)
{

	DBTIMESTAMP tm;

//
// Here we will prepare the data base for an insertion of a record.
//

	m_Faults.ClearRecord();

	strncpy(m_Faults.m_APSName,			FaultFp.APSName,	APS_NAME_SIZE);
	strncpy(m_Faults.m_ERONumber,		FaultFp.ERONumber,	ERO_NUM_SIZE);
	strncpy(m_Faults.m_PartNumber,		FaultFp.PartNumber,	PART_NUM_SIZE);
	strncpy(m_Faults.m_Status,			FaultFp.FileInfo,	FILE_INFO_SIZE);
	strncpy(m_Faults.m_TPSName,			FaultFp.TPSName,	TPS_NAME_SIZE);
	strncpy(m_Faults.m_SerialNumber,	FaultFp.Serial,		SERIAL_NUM_SIZE);

	tm.day      = FaultFp.DateTime.day;
	tm.fraction = 0;
	tm.hour     = FaultFp.DateTime.hour;
	tm.minute   = FaultFp.DateTime.min;
	tm.month    = FaultFp.DateTime.month;
	tm.second   = FaultFp.DateTime.sec;
	tm.year     = FaultFp.DateTime.year;

	m_Faults.m_RunDate = tm;

	hResult = m_Faults.Insert(0);

	if (FAILED(hResult)) {
		DoDebug(0, "FillInDataBase()", "Failed writing to database", (char*)NULL);
		DoDebug(0, "FillInDataBase()", "hResult = %d", hResult, (char*)NULL);
	}

	return 0;
}

/////////////////////////////////////////////////////////////////////////////
//	CloseDataBase:	This function will try to open a database located at	/
//					d:\\database\\FaultFile.mdb								/
//																			/
// Parameters:																/
//		none:																/
//																			/
// Returns:																	/
//		none:	This is a void function.									/
//																			/
/////////////////////////////////////////////////////////////////////////////

void CloseDataBase(void)
{

	m_Faults.Close();
	fflush(NULL);

	return;
}