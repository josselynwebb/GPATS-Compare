/////////////////////////////////////////////////////////////////////////////
//	File:	fillInDB.cpp													*
//																			/
//	Creation Date:	30 June 2008											/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		2.0.0.0		Complete rebuild of fhdb nam, visual dll software no	/
//					longer available.  Include the dll code into the nam	/
//					program.												/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <atldbcli.h>
#include <winerror.h>
#include "stdafx.h"
#include "fhdb.h"
#include "FaultFile.h"

/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

CFAULTS m_FAULTS;

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

HRESULT	hResult;

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

//
// opendb:	This function will attempt to open the database fhdb.mdb for later writing.
//
// Parameters:
//		none:		This function will use the m_Faults structure.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int	opendb()
{
	hResult = 0;
	hResult = m_FAULTS.Open();

	if (FAILED(hResult)) 
	{
		dodebug(NO_PRE_DEFINE, "opendb()", "hResult = %d", hResult, (char*)NULL);
		dodebug(NO_PRE_DEFINE, "opendb()", "Can't open or find data base", (char*)NULL);
		return(NAM_ERROR);
	}

	return(SUCCESS);
}

//
// fillInDataBase:	This function will if it can fill in the data base fhdb.mdb.
//
// Parameters:
//		none:		This function will use the m_Faults, tm and faultfp structures.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
int fillInDataBase()
{
	DBTIMESTAMP tm;
	//
	// Here we will prepare the data base for an insertion of a record.
	//

	m_FAULTS.ClearRecord();

	strncpy(m_FAULTS.m_ERO,					dataCollectionfp.eroNo,				ERO_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_TPCCN,				dataCollectionfp.tpsProgCntrlNo,	TPCCN_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_UUT_Serial_No,		dataCollectionfp.uutSerialNo,		UUT_SERIAL_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_UUT_Rev,				dataCollectionfp.uutRev,			REVISION_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_ID_Serial_No,		dataCollectionfp.idSerialNo,		ID_SERIAL_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_Failure_Step,		dataCollectionfp.failedStep,		FAILED_STEP_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_Fault_Callout,		dataCollectionfp.faultCallout,		FAULT_CALLOUT_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_Dimension,			dataCollectionfp.dimension,			DIMENSION_SIZE + TERMINATOR);
	strncpy(m_FAULTS.m_Operator_Comments,	dataCollectionfp.operatorComments,	OP_COMMENT_SIZE + TERMINATOR);

	m_FAULTS.m_Test_Status	= dataCollectionfp.testStatus;
	m_FAULTS.m_Meas_Value	= dataCollectionfp.measureValue;
	m_FAULTS.m_Upper_Limit	= dataCollectionfp.upperLimit;
	m_FAULTS.m_Lower_Limit	= dataCollectionfp.lowerLimit;
	m_FAULTS.m_Temperature	= dataCollectionfp.temperature;
	m_FAULTS.m_UDP			= (bool)FALSE;

	tm.day      = dataCollectionfp.dateTimeStart.day;
	tm.fraction = 0;
	tm.hour     = dataCollectionfp.dateTimeStart.hour;
	tm.minute   = dataCollectionfp.dateTimeStart.min;
	tm.month    = dataCollectionfp.dateTimeStart.month;
	tm.second   = dataCollectionfp.dateTimeStart.sec;
	tm.year     = dataCollectionfp.dateTimeStart.year;

	m_FAULTS.m_Start_Time = tm;
	
	tm.day      = dataCollectionfp.dateTimeStop.day;
	tm.fraction = 0;
	tm.hour     = dataCollectionfp.dateTimeStop.hour;
	tm.minute   = dataCollectionfp.dateTimeStop.min;
	tm.month    = dataCollectionfp.dateTimeStop.month;
	tm.second   = dataCollectionfp.dateTimeStop.sec;
	tm.year     = dataCollectionfp.dateTimeStop.year;

	m_FAULTS.m_Stop_Time = tm;

	hResult = m_FAULTS.Insert(0);

	if (FAILED(hResult)) 
	{
		dodebug(NO_PRE_DEFINE, "fillInDataBase()", "Failed writing to data base", (char*)NULL);
		dodebug(NO_PRE_DEFINE, "fillInDataBase()", "hResult = %d", hResult, (char*)NULL);
	}

	return 0;
}

//
// closedb:	This function will close the database fhdb.mdb.
//
// Parameters:
//		none:		This function will use the m_Faults structure.
//
// Returns:
//		SUCCESS:	  0		= successful completion of the function.
//		NAM_ERROR:	(-1)	= failure of a required task.
//
void closedb()
{
	m_FAULTS.Close();
}
