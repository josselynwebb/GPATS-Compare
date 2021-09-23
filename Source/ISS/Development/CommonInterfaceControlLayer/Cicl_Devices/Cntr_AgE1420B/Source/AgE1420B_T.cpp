////////////////////////////////////////////////////////////////////////////////
// File:	    AgE1420B_T.cpp
//
// Date:	    14FEB06
//
// Purpose:	    ATXMLW Instrument Driver for AgE1420B
//
// Instrument:	Agilent E1420B Universal Counter (Cntr)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  14FEB06  Baseline Release    
// 1.2.1.2  14FEB07  Correct code compensate for a large trigger level by adjusting
//                      the attenuation to 10.
// 1.2.1.3  22FEB07	 Corrected code to see if Trigger Exists first before it adjust
//						the attenuation.
// 1.4.1.0  24MAR09  Added code to allow TI measurements between Ch1 and 2
// 1.4.2.0  28JUL10  Corrected comparison of start and stop ports.
// 1.4.2.1  04AUG10  Corrected setting of stop channel impedance
// 1.4.2.2  06AUG10  Updated format and sequence of commands based on NiSPY capture.
// 1.4.2.3  06AUG10  Added *WAI to setup, Initialize TTLT ports to off.
// 1.4.2.4  09AUG10  Added config for averaging, delay, hysteresis to TINT.
// 1.4.2.5  15FEB11  Corrected scaling of trigger/atten in PortSetup()
// 1.4.2.6  17FEB11  Test case for error in attenuation in COMM
// 1.4.2.7  18FEB11  Corrected logic error in previous
// 1.4.2.8  18FEB11  Corrected logic error in previous
// 1.4.2.9  22FEB11  Corrected trig level stop when atten set in COMM.
// 1.4.2.10 22FEB11  Corrected setting of global var m_Atten
// 1.4.2.11 23FEB11  Corrected error in logic in saving m_Atten
// 1.4.2.12 24FEB11  Changed time interval code so both start and stop
//                   channels use the same attenuation.
// 1.4.2.13 11MAR11  Modified code per USMC Albany request
// 1.4.2.14 05APR11  Corrected TIME INTERVAL code, removed SENSx header,
//                   deleted command that was disabling TINT delay.
////////////////////////////////////////////////////////////////////////////////
// Includes
#include <visa.h>
#include "hpe1420b.h"
#include "AtxmlWrapper.h"
#include "AgE1420B_T.h"


#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024


static void s_IssueDriverFunctionCallDeviceXxx(int Handle,
                ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);

int DE_BUG = 0;
FILE *debugfp = 0;
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CAgE1420B_T
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Instno           int             System assigned instrument number
// ResourceType     int             Type of Resource Base, Physical, Virtual
// ResourceName     char*           Station resource name
// Sim              int             Simulation flag value (0/1)
// Dbglvl           int             Debug flag value
// AddressInfoPtr   InstAddPtr*     Contains all addressing information available
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
// Response         ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
CAgE1420B_T::CAgE1420B_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    char scpi[80];
    int  Status = 0;

	ISDODEBUG(dodebug(0, "CAgE1420B_T()", "Wrap-CAgE1420B_T called", (char*)NULL));
	memset(LclMsg, '\0', sizeof(LclMsg));
	memset(scpi, '\0', sizeof(scpi));

	// Save Device Info
	m_InstNo = Instno;
	m_ResourceType = ResourceType;
	m_ResourceName[0] = '\0';

	if (ResourceName)
	{
		strnzcpy(m_ResourceName, ResourceName, ATXMLW_MAX_NAME);
	}
	m_Sim = Sim;
	m_Dbg = Dbglvl;

	// Save Address Information
	if (AddressInfoPtr)
	{
		m_AddressInfo.ResourceAddress = AddressInfoPtr->ResourceAddress;
		if (AddressInfoPtr->InstrumentQueryID)
		{
			strnzcpy(m_AddressInfo.InstrumentQueryID, 
					AddressInfoPtr->InstrumentQueryID, ATXMLW_MAX_NAME);
		}
		m_AddressInfo.InstrumentTypeNumber = AddressInfoPtr->InstrumentTypeNumber;
		if (AddressInfoPtr->ControllerType)
		{
			strnzcpy(m_AddressInfo.ControllerType, 
					AddressInfoPtr->ControllerType, ATXMLW_MAX_NAME);
		}
		m_AddressInfo.ControllerNumber = AddressInfoPtr->ControllerNumber;
		m_AddressInfo.PrimaryAddress = AddressInfoPtr->PrimaryAddress;
		m_AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
		m_AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
	}

	m_Handle = NULL;
	memset(m_InitString, '\0', sizeof(m_InitString));

	InitPrivateAgE1420B();
	NullCalDataAgE1420B();

	// The Form Init String
	sprintf(m_InitString, "%s%d::%d::INSTR",
			m_AddressInfo.ControllerType, m_AddressInfo.ControllerNumber,
			m_AddressInfo.PrimaryAddress);
	sprintf(LclMsg,"Wrap-CAgE1420B Class called with Instno %d, Sim %d, Dbg %d", 
			m_InstNo, m_Sim, m_Dbg);
	ATXMLW_DEBUG(5, LclMsg, Response, Buffersize);

	// Initialize the AgE1420B
	IFNSIM(Status = hpe1420b_init(m_InitString, false, true, &m_Handle));
	if (Status)
		ErrorAgE1420B(Status, Response, Buffersize);

	sprintf(scpi, "*CLS");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, Buffersize);
	if (Status)
		ErrorAgE1420B(Status, Response, Buffersize);

	SanitizeAgE1420B();

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CAgE1420B_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CAgE1420B_T::~CAgE1420B_T()
{
	char Dummy[1024];

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-~CAgE1420B Class destructor called "),Dummy,1024);

	memset(Dummy, '\0', sizeof(Dummy));

	// Reset the AgE1420B
	IFNSIM(hpe1420b_writeInstrData(m_Handle, "*CLS"));
	IFNSIM(hpe1420b_reset(m_Handle));

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusAgE1420B
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::StatusAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status = 0;
	char *ErrMsg = NULL;

	// Status action for the AgE1420B
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-StatusAgE1420B called "), Response, BufferSize);

	// Check for any pending error messages
	Status = ErrorAgE1420B(0, Response, BufferSize);

	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalAgE1420B
//
// Purpose: Perform the IEEE 1641 / Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// SignalDescription  ATXMLW_INTF_SIGDESC*   The Allocated FNC code
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::IssueSignalAgE1420B(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char   *ErrMsg = NULL;
	int     Status = 0;


	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IssueSignalAgE1420B Signal: "), Response, BufferSize);

	if ((Status = GetStmtInfoAgE1420B(SignalDescription, Response, BufferSize)) != 0)
	{
		return(Status);
	}

	switch(m_Action)
	{
	case ATXMLW_SA_APPLY:
		if ((Status = SetupAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		if ((Status = EnableAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_REMOVE:
		if ((Status = DisableAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		if ((Status = ResetAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_MEASURE:
		if ((Status = SetupAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_READ:
		if ((Status = EnableAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		if ((Status = FetchAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_RESET:
		if ((Status = ResetAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_SETUP:
		if ((Status = SetupAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_CONNECT:
		break;
	case ATXMLW_SA_ENABLE:
		if ((Status = EnableAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_DISABLE:
		if ((Status = DisableAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_FETCH:
		if ((Status = FetchAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	case ATXMLW_SA_DISCONNECT:
		break;
	case ATXMLW_SA_STATUS:
		if ((Status = StatusAgE1420B(Response, BufferSize)) != 0)
			return(Status);
		break;
	}

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: RegCalAgE1420B
//
// Purpose: Register/Provide the Calibration data for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// CalData            ATXMLW_INTF_CALDATA*   Xml description of the calibration data
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::RegCalAgE1420B(ATXMLW_INTF_CALDATA* CalData)
{
	int       Status = 0;
	char      Dummy[1024];

	memset(Dummy, '\0', sizeof(Dummy));

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-RegCalAgE1420B CalData: %s", 
							   CalData),Dummy,1024);


	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetAgE1420B
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::ResetAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int   Status = 0;
	char *ErrMsg = NULL;


	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-ResetAgE1420B called"), Response, BufferSize);


	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, "*CLS"));
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);
	IFNSIM((Status = hpe1420b_reset(m_Handle)));
	if (Status && Status != VI_SUCCESS_EVENT_EN)
		ErrorAgE1420B(Status, Response, BufferSize);
    // Force use of internal reference oscillator.  The instrument reset uses the VXI
    // clock, which isn't particularly accurate, by default.
	Sleep(500);
    IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, "SENS1:ROSC:SOUR INT"));
    if (Status)
        ErrorAgE1420B(Status, Response, BufferSize);
	Sleep(500);
    IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, "SENS2:ROSC:SOUR INT"));
    if (Status)
        ErrorAgE1420B(Status, Response, BufferSize);

	InitPrivateAgE1420B();

	SanitizeAgE1420B();

	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IstAgE1420B
//
// Purpose: Perform the SelfTest Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Level              ATXMLW_INTF_STLEVEL  Indicates the Instrument Level To Be Performed
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::IstAgE1420B(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int      Status = 0;
	ViInt16  stResult = 0;
	ViChar   stMessage[256];


	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IstAgE1420B called Level %d", 
			Level), Response, BufferSize);

	memset(stMessage, '\0', sizeof(stMessage));

	IFNSIM((Status = hpe1420b_selfTest(m_Handle, &stResult, stMessage)));
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);
	atxmlw_ScalerStringReturn(m_MeasAttr, "", stMessage, Response, BufferSize);

	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, "*CLS"));
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);
	IFNSIM((Status = hpe1420b_reset(m_Handle)));
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	InitPrivateAgE1420B();

	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsAgE1420B
//
// Purpose: Issue Native nstrument commands for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// InstrumentCmds     ATXMLW_INTF_INSTCMD*   Xml description of the Native Instrument commands
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::IssueNativeCmdsAgE1420B(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int       Status = 0;

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IssueNativeCmdsAgE1420B "), Response, BufferSize);

	if ((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
	{
		return(Status);
	}
	
	if (strstr(InstrumentCmds, "*RST") != NULL)
	{
		SanitizeAgE1420B(); 
	}

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallAgE1420B
//
// Purpose: Issue Instrument Driver function calls for to this instrument
//          Return in the response values in Response
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// DriverFunction     ATXMLW_INTF_DRVRFNC*   Xml description of the IVI Instrument commands
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::IssueDriverFunctionCallAgE1420B(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int       Status = 0;

	ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallAgE1420B", Response, BufferSize);

	IFNSIM(s_IssueDriverFunctionCallDeviceXxx(m_Handle, DriverFunction,	Response, BufferSize));

	return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: SetupAgE1420B
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Fnc                int                  The Allocated FNC code
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::SetupAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int		  atten = 1;
	int		  idx = 0;
	double    MaxTime = 0.0;
	char      scpi[80];
	int       Status = 0;
	char      trig[80];

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-SetupAgE1420B called"), Response, BufferSize);

	memset(scpi, '\0', sizeof(scpi));
	memset(trig, '\0', sizeof(trig));

	// Clear counter status
	sprintf(scpi, "*CLS");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	// Check MaxTime Modifier
	if(m_MaxTime.Exists)
		MaxTime = m_MaxTime.Real * 1000;
	else
		MaxTime = 10*1000;

	IFNSIM(Status=viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (unsigned long) MaxTime));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = viSetAttribute(%x, VI_ATTR_TMO_VALUE, %.0lf);", Status, m_Handle, MaxTime), Response, BufferSize);
	if(Status)
		ErrorAgE1420B(Status, Response, BufferSize);


	// Initialize ttlt ports to off. 
	for (idx = 0; idx < 8; idx++)
	{
		sprintf(scpi, "OUTP:TTLT%d:STAT OFF", idx);
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
			ErrorAgE1420B(Status, Response, BufferSize);
	}

	sprintf(scpi, "OUTP:ROSC:STAT OFF");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	sprintf(scpi, "SENS1:AVER:STAT OFF");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	// Update 17FEB2011 - Set common mode prior to PortSetup
	// In PortSetup, if in common mode check the stop channel 
	// levels to see if atten required.
	if ((strcmp(m_MeasAttr, "timeInterval") == 0) && (strcmp(m_Port, m_Stop.Port) == 0))
	{
		m_Common = true;
	}
	else
	{
		m_Common = false;
	}

	// Added call to set attenuation based on trigger level, and stop level
	// if in time interval mode
	SetAtten(Response, BufferSize);

	PortSetup(m_Port, m_Coupling, m_TestEquipImp, m_Trig, m_Slope,
			Response, BufferSize);

	// timeInterval
	if (strcmp(m_MeasAttr, "timeInterval") == 0)
	{
		// Setup these parameters if not in common mode. They are upstream of the 
		// sep/comm switch.
		if (!m_Common) 
		{
			// input impedance
			sprintf(scpi, "INPUT2:IMP %d", m_Stop.TestEquipImp.Int);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}

			// attenuation
			sprintf(scpi, "INPUT2:ATT %d", m_Atten);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// input coupling
		sprintf(scpi, "INPUT2:COUP %s", m_Stop.Coupling.Int == 100 ? "AC" : "DC");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}
		

		// Program stop event parameters not affected by COMMON mode
		// trigger slope
		// Note: Channel 2 must still be setup even in common mode. Stop port will always be 2 for 
		//       time interval measurements
		sprintf(scpi, "SENS2:EVEN:SLOP %s", m_Stop.Slope.Int == +1 ? "POS" : "NEG");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}

		//account for attenuation
		m_Stop.TrigVolt.Real = (m_Stop.TrigVolt.Real / (double)m_Atten);
		sprintf(scpi, "SENS2:EVEN:LEV %.3lf", m_Stop.TrigVolt.Real);         
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}

		// add hysteresis
		sprintf(scpi, "SENS2:EVEN:HYST DEF");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}

		// "holdoff" delay
		if (m_Stop.SettleTime.Exists && m_Stop.SettleTime.Real > 0.0)
		{
			sprintf(scpi, ":TINT:DEL:TIME %es", m_Stop.SettleTime.Real);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}

			sprintf(scpi, ":TINT:DEL:STAT ON");
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}
		else
		{
			sprintf(scpi, ":TINT:DEL:STAT OFF");
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// Added 05AUG2010
		sprintf(scpi, "ARM:STAR:SOUR IMM");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg(scpi), Response, BufferSize);

		sprintf(scpi, "ARM:STOP:SOUR IMM");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg(scpi), Response, BufferSize);

		if (m_Expected.Exists)
		{
			if (m_Expected.Real < 1.0e-9 || m_Expected.Real > 1000.0)
			{				
				ATXMLW_DEBUG(0, atxmlw_FmtMsg("WrapGSI-RANGE: Time Interval %f s outside allowed range [1ns-1000s]", m_Expected.Real), Response, BufferSize);
			}
			sprintf(scpi, "CONF%s:TINT %lf", m_Port, m_Expected.Real);
		}
		else
		{
			sprintf(scpi, "CONF%s:TINT AUTO", m_Port);
		}
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}

		// Added for test 20100806
		sprintf(scpi, "SENS1:FUNC \"TINT\"");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}

		// Added for test 20100809
		sprintf(scpi, "SENS1:ROSC:SOUR INT");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
		{
			ErrorAgE1420B(Status, Response, BufferSize);
		}
	}

	// totalize
	else if (strcmp(m_MeasAttr, "totalize") == 0)
	{
		sprintf(scpi, "CONF%s:VOLT:TOT", m_Port);
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
			ErrorAgE1420B(Status, Response, BufferSize);

		// if gated by 2 (special case)
		if (strcmp(m_Stop.Port, "2") == 0)
		{
			sprintf(scpi, "SENS%s:TOT:GATE:STAT ON", m_Port);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}

			sprintf(scpi, "SENS%s:TOT:GATE:POL NORM", m_Port);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}

			strcpy(scpi, "ARM:STAR:SOUR IMM");
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}

			strcpy(scpi, "ARM:STOP:SOUR IMM");
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}
		else
		{   // normal gates & sync
			GateSetup(m_Start, m_Stop, m_Sync, Response, BufferSize);
		}
	}

	else
	{
		// normal gates & sync
		GateSetup(m_Start, m_Stop, m_Sync, Response, BufferSize);
    
		// frequency
		if (strcmp(m_MeasAttr, "frequency") == 0)
		{
			if (m_Expected.Exists)
			{
				if (strcmp(m_Port, "1"))
				{
					if (m_Expected.Real < 0.001 || m_Expected.Real > 200.0e+6)
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-RANGE: Frequency %f Hz outside allowed range [0.001Hz-200MHz]", m_Expected.Real), Response, BufferSize);
				}
				else
				{
					if (m_Expected.Real < 0.001 || m_Expected.Real > 100.0e+6)
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-RANGE: Frequency %f Hz outside allowed range [0.001Hz-100MHz]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:FREQ %e", m_Port, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:FREQ AUTO", m_Port);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// period
		else if (strcmp(m_MeasAttr, "period") == 0)
		{
			if (m_Expected.Exists)
			{
				if (strcmp(m_Port, "1"))
				{
					if (m_Expected.Real < 5.0e-9 || m_Expected.Real > 1000.0)
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-RANGE: Period %f s outside allowed range [5ns-1000s]", m_Expected.Real), Response, BufferSize);
				}
				else
				{
					if (m_Expected.Real < 10.0e-9 || m_Expected.Real > 1000.0)
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-RANGE: Period %f s outside allowed range [5ns-1000s]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:PER %e", m_Port, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:PER AUTO", m_Port);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// fallTime
		else if (strcmp(m_MeasAttr, "fallTime") == 0)
		{
			if (m_Expected.Exists)
			{
				if (m_Expected.Real < 15.0e-9 || m_Expected.Real > 1.0e-3)
				{
					ATXMLW_DEBUG(0, atxmlw_FmtMsg("WrapGSI-RANGE: Falltime %f s outside allowed range [15ns-1ms]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:FTIM DEF,DEF,%e", m_Port, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:FTIM DEF,DEF,AUTO", m_Port);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// riseTime
		else if (strcmp(m_MeasAttr, "riseTime") == 0)
		{
			if (m_Expected.Exists)
			{
				if (m_Expected.Real < 15.0e-9 || m_Expected.Real > 1.0e-3)
				{
					ATXMLW_DEBUG(0, atxmlw_FmtMsg( "WrapGSI-RANGE: Risetime %f s outside allowed range [15ns-1ms]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:RTIM DEF,DEF,%e", m_Port, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:RTIM DEF,DEF,AUTO", m_Port);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// pulseWidth
		else if (strcmp(m_MeasAttr, "pulseWidth") == 0)
		{
			if (m_Trig.Exists)
				sprintf(trig, "%eV", m_Trig.Real); 
			else if (m_Sync.Exists)
				sprintf(trig, "%eV", m_Sync.TrigVolt.Real);
			else
				strcpy(trig, "50PCT");

			if (m_Expected.Exists)
			{
				if (m_Expected.Real < 5.0e-9 || m_Expected.Real > 1.0e-3)
				{
					ATXMLW_DEBUG(0, atxmlw_FmtMsg("WrapGSI-RANGE: Pulsewidth %f s outside allowed range [5ns-1ms]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:PWID %s,%e", m_Port, trig, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:PWID %s,AUTO", m_Port, trig);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
        
		}

		// negPulseWidth
		else if (strcmp(m_MeasAttr, "negPulseWidth") == 0)
		{
			if (m_Trig.Exists)
				sprintf(trig, "%eV", m_Trig.Real);
			else
				strcpy(trig, "50PCT");

			if (m_Expected.Exists)
			{
				if (m_Expected.Real < 5.0e-9 || m_Expected.Real > 1.0e-3)
				{
					ATXMLW_DEBUG(0, atxmlw_FmtMsg("WrapGSI-RANGE: Pulsewidth %f s outside allowed range [5ns-1ms]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:NWID %s,%e", m_Port, trig, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:NWID %s,AUTO", m_Port, trig);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// freqRatio
		else if (strcmp(m_MeasAttr, "freqRatio") == 0)
		{
			PortSetup(m_RefPort, m_Coupling, m_TestEquipImp,
					m_Trig, m_Slope, Response, BufferSize);

			if (m_Expected.Exists)
			{
				if (m_Expected.Real < 1.0e-11 || m_Expected.Real > 1.0e+11)
				{
					ATXMLW_DEBUG(0, atxmlw_FmtMsg("WrapGSI-RANGE: Freq Ratio %f:1 outside allowed range [1E-11 - 1E11]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:FREQ:RAT %e", m_Port, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:FREQ:RAT", m_Port);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}

		// phaseAngle
		else if (strcmp(m_MeasAttr, "phaseAngle") == 0)
		{
			PortSetup(m_RefPort, m_Coupling, m_TestEquipImp,
					m_Trig, m_Slope, Response, BufferSize);

			if (m_Expected.Exists)
			{
				if (m_Expected.Real < 0.0 || m_Expected.Real > 359.9)
				{
					ATXMLW_DEBUG(0, atxmlw_FmtMsg("WrapGSI-RANGE: Phase Angle %f:1 outside allowed range [0-359.9 deg]", m_Expected.Real), Response, BufferSize);
				}
				sprintf(scpi, "CONF%s:VOLT:PHAS %e", m_Port, m_Expected.Real);
			}
			else
			{
				sprintf(scpi, "CONF%s:VOLT:PHAS AUTO", m_Port);
			}
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
			{
				ErrorAgE1420B(Status, Response, BufferSize);
			}
		}
	}

	// Test 20100806 match up with panel do *CLS, ABOR1, *WAI
	sprintf(scpi, "*CLS");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
	{
		ErrorAgE1420B(Status, Response, BufferSize);
	}

	sprintf(scpi, "ABOR1");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
	{
		ErrorAgE1420B(Status, Response, BufferSize);
	}

	sprintf(scpi, "*WAI");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
	{
		ErrorAgE1420B(Status, Response, BufferSize);
	}

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchAgE1420B
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::FetchAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char   *ErrMsg = NULL;
	ViInt32 length = 0;
	double  MeasValue = 0.0;
	double  MaxTime = 5000;
	ViChar  respStr[256];
	char    scpi[80];
	int     Status = 0;
	int		time_out = 0;

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-FetchAgE1420B called "), Response, BufferSize);

	memset(scpi, '\0', sizeof(scpi));
	memset(respStr, '\0', sizeof(respStr));

	sprintf(scpi, "FETC%s?", m_Port);
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	IFNSIM(Status = hpe1420b_readInstrData(m_Handle, 256, respStr, &length));
	if (Status || (strcmp(respStr, "9.91E+37\n") == 0))
	{
		MeasValue = FLT_MAX;

		// return the time-out error
		if(Status == 0xBFFF0015 || (strcmp(respStr, "9.91E+37\n") == 0))
		{
			Status = 0xBFFF0015;
			time_out = 1;
			atxmlw_ScalerIntegerReturn("timeOut", "", time_out,
					Response, BufferSize);
		}

		ErrorAgE1420B(Status, Response, BufferSize);
	}
	else
	{
		respStr[length] = '\0';
		sscanf(respStr, "%lf", &MeasValue);
	}

	if (Response && (BufferSize > (int)(strlen(Response)+200)))
	{
		atxmlw_ScalerDoubleReturn(m_MeasAttr, "", MeasValue,
					Response, BufferSize);
	}

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: DisableAgE1420B
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::DisableAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-OpenAgE1420B called "), Response, BufferSize);


	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: EnableAgE1420B
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::EnableAgE1420B(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char    scpi[80];
	int     Status = 0;

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-EnableAgE1420B called"), Response, BufferSize);

	memset(scpi, '\0', sizeof(scpi));

	sprintf(scpi, "SENS:ATIM:TIME %lf", m_MeasInfo.TimeOut);
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, "SENS:ATIM:CHEC ON"));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg("SENS:ATIM:CHEC ON"), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	sprintf(scpi, "INIT%s:IMM", m_Port);
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorAgE1420B
//
// Purpose: Query AgE1420B for the error text and send to WRTS
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Status           int             Error code returned from driver
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::ErrorAgE1420B(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int      Err = 0;
	char     Msg[MAX_MSG_SIZE];
	ViInt32  QError = 0;
	int      retval = 0;

	retval = Status;

	memset(Msg, '\0', sizeof(Msg));

	if (Status)
	{
		IFNSIM(Err = hpe1420b_errorMessage(m_Handle, Status, Msg));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = hpe1420b_errorMessage(%x, %x, %s);",\
			Err, m_Handle, Status, Msg), Response, BufferSize);
		if (Err)
			sprintf(Msg, "Plug-n-Play Error: Unable to get error text [%X]!", Err);

		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
				Status, Msg);
	}

	// Retrieve any pending errors in the device
	QError = 1;
	while(QError)
	{
		QError = 0;
		IFNSIM(Err = hpe1420b_errorQuery(m_Handle, &QError, Msg));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = hpe1420b_errorMessage(%x, %x, %s);",\
			Err, m_Handle, QError, Msg), Response, BufferSize);
		if (Err != 0)
			break;
		if (QError)
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
					Status, Msg);
		}
		Sleep(10);
	}

	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoAgE1420B
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CAgE1420B_T::GetStmtInfoAgE1420B(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	double dcOff = 0.0;
	char   LclAsIn[ATXMLW_MAX_NAME];
	char   LclAsIn2[ATXMLW_MAX_NAME];
	char  *LclCharValuePtr = NULL;
	char   LclElement[ATXMLW_MAX_NAME];
	AttributeStruct LclJunk;
	char   LclJunkPort[ATXMLW_MAX_NAME];
	char   LclGate[ATXMLW_MAX_NAME];
	char   LclGate2[ATXMLW_MAX_NAME];
	char  *LclHoldoffPtr = NULL;
	char   LclSync[ATXMLW_MAX_NAME];
	char   LclUnit[ATXMLW_MAX_NAME];
	int    Status = 0;

	memset(LclAsIn, '\0', sizeof(LclAsIn));
	memset(LclAsIn2, '\0', sizeof(LclAsIn2));
	memset(LclElement, '\0', sizeof(LclElement));
	memset(LclGate, '\0', sizeof(LclGate));
	memset(LclGate2, '\0', sizeof(LclGate2));
	memset(LclJunkPort, '\0', sizeof(LclJunkPort));
	memset(LclUnit, '\0', sizeof(LclUnit));
	memset(LclSync, '\0', sizeof(LclSync));

	if (Status = atxmlw_Parse1641Xml(SignalDescription, &m_SigDesc,
			Response, BufferSize))
		return(Status);

	// get signal action
	m_Action = atxmlw_Get1641SignalAction(m_SigDesc, Response, BufferSize);
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action),
			Response, BufferSize);

	// if not setup or enable, we don't need to parse any further...
	if (m_Action == ATXMLW_SA_SETUP || m_Action == ATXMLW_SA_ENABLE)
	{
		// get the "measure" name & element type
		if (atxmlw_Get1641SignalOut(m_SigDesc, m_SignalName, m_SignalElement))
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
					m_SignalName, m_SignalElement), Response, BufferSize);

		// get standard measurement characteristics (including TimeOut)
		if ((atxmlw_Get1641StdMeasChar(m_SigDesc, m_SignalName, &m_MeasInfo)))
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s MeasChar %d",
					m_SignalName, m_MeasInfo.StdType), Response, BufferSize);

		if (m_Action == ATXMLW_SA_SETUP)
		{
			//
			// parse "top-level" 1641 elements
			//
        
			// attribute
			if (atxmlw_Get1641StringAttribute(m_SigDesc, m_SignalName, "attribute",
					&LclCharValuePtr))
			{
				strcpy(m_MeasAttr, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s attribute [%s]",
						m_SignalName, m_MeasAttr), Response, BufferSize);
			}

			if (atxmlw_Get1641DoubleAttribute(m_SigDesc, m_SignalName, "nominal",
					&m_Expected.Real, LclUnit))
			{
				m_Expected.Exists = true;
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s nominal %f [%s]",
						m_SignalName, m_Expected.Real, LclUnit), Response, BufferSize);
			}

			if (strcmp(m_MeasAttr, "totalize") == 0)
			{
				// trigger (totalize only)
				if (atxmlw_Get1641DoubleAttribute(m_SigDesc, m_SignalName, "nominal",
						&m_Trig.Real, LclUnit))
				{
					m_Trig.Exists = true;
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found nominal %f [%s]",
							m_Trig.Real, LclUnit), Response, BufferSize);
				}

				// trigger slope (totalize only)
				if (atxmlw_Get1641StringAttribute(m_SigDesc, m_SignalName, "condition",
						&LclCharValuePtr))
				{
					m_Slope.Exists = true;
					m_Slope.Int = strcmp(LclCharValuePtr, "GE") == 0 ? +1 : -1;
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s condition [%d]",
							LclCharValuePtr, m_Slope.Int), Response, BufferSize);
				}
			}
            
			//
			// parse Gate info
			//

			if (atxmlw_Get1641StringAttribute(m_SigDesc, m_SignalName, "Gate",
					&LclCharValuePtr))
			{
				strcpy(LclGate, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s Gate [%s]",
						m_SignalName, LclGate), Response, BufferSize);

				// start port (indicates "extended" gate [Instantaneous] node)
				if (atxmlw_Get1641StringAttribute(m_SigDesc, LclGate, "startChannel",
						&LclCharValuePtr))
				{
					m_Start.Exists = true;
					strcpy(m_Start.Port, LclCharValuePtr);
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s startChannel [%s]",
							LclGate, m_Start.Port), Response, BufferSize);

					// start trigger
					if (atxmlw_Get1641DoubleAttribute(m_SigDesc, LclGate, "startNominal",
							&m_Start.TrigVolt.Real, LclUnit))
					{
						m_Start.TrigVolt.Exists = true;
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s startNominal [%f %s]",
								LclGate, m_Start.TrigVolt.Real, LclUnit), Response, BufferSize);
					}

					// start slope
					if (atxmlw_Get1641StringAttribute(m_SigDesc, LclGate, "startCondition",
							&LclCharValuePtr))
					{
						m_Start.Slope.Exists = true;
						m_Start.Slope.Int = strcmp(LclCharValuePtr, "GE") == 0 ? +1 : -1;
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s startCondition [%d]",
								LclGate, m_Start.Slope.Int), Response, BufferSize);
					}
            
					// stop port
					if (atxmlw_Get1641StringAttribute(m_SigDesc, LclGate, "stopChannel",
							&LclCharValuePtr))
					{
						m_Stop.Exists = true;
						strcpy(m_Stop.Port, LclCharValuePtr);
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s stopChannel [%s]",
								LclGate, m_Stop.Port), Response, BufferSize);
					}

					// stop trigger
					if (atxmlw_Get1641DoubleAttribute(m_SigDesc, LclGate, "stopNominal",
							&m_Stop.TrigVolt.Real, LclUnit))
					{
						m_Stop.TrigVolt.Exists = true;
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s stopNominal [%f %s]",
								LclGate, m_Stop.TrigVolt.Real, LclUnit), Response, BufferSize);
					}

					// stop slope
					if (atxmlw_Get1641StringAttribute(m_SigDesc, LclGate, "stopCondition",
							&LclCharValuePtr))
					{
						m_Stop.Slope.Exists = true;
						m_Stop.Slope.Int = strcmp(LclCharValuePtr, "GE") == 0 ? +1 : -1;
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s stopCondition [%d]",
								LclGate, m_Stop.Slope.Int), Response, BufferSize);
					}
				}
				else
				{   // "standard" Gate [Instantaneous] tree
					//   only used as the "stop" signal for timeInterval
					Instantaneous(m_SigDesc, LclGate, LclAsIn, &m_Stop.TrigVolt,
							&m_Stop.Slope, LclGate2, Response, BufferSize);
					m_Stop.Exists = m_Stop.TrigVolt.Exists || m_Stop.Slope.Exists;

					// hold off
					if (*LclGate2)
					{
						if (atxmlw_Get1641DoubleAttribute(m_SigDesc, LclGate2, "delay",
								&m_Stop.SettleTime.Real, LclUnit))
						{
							m_Stop.SettleTime.Exists = true;
							ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s delay [%f %s]",
									LclGate2, m_Stop.SettleTime.Real, LclUnit), Response, BufferSize);
						}
					}
            
					// In
					if (LclAsIn[0] != NULL)
					{
						SigTree(m_SigDesc, LclAsIn, m_Stop.Port, LclJunkPort,
								&m_Stop.VoltMax, &LclJunk, &m_Stop.Coupling, &m_Stop.TestEquipImp,
								Response, BufferSize);
					}
				}
			}
        
			//
			// parse Sync info
			//

			if (atxmlw_Get1641StringAttribute(m_SigDesc, m_SignalName, "Sync",
					&LclCharValuePtr))
			{
				strcpy(LclSync, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]",
						m_SignalName, LclSync), Response, BufferSize);

				Instantaneous(m_SigDesc, LclSync, LclSync, &m_Sync.TrigVolt,
						&m_Sync.Slope, LclGate, Response, BufferSize);
				m_Sync.Exists = m_Sync.TrigVolt.Exists || m_Sync.Slope.Exists;

				if (atxmlw_Get1641ElementByName(m_SigDesc, LclSync, LclElement))
				{
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
							LclSync, LclElement), Response, BufferSize);

					// port
					if (strcmp(LclElement, "TwoWire") == 0)
					{
						if (atxmlw_Get1641StringAttribute(m_SigDesc, LclSync, "hi",
								&LclCharValuePtr))
						{
							strcpy(m_Sync.Port, LclCharValuePtr);
							ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s hi %s",
									LclElement, m_Sync.Port), Response, BufferSize);
						}
					}
				}
			}

			//
			// parse As signal info
			//

			if (atxmlw_Get1641StringAttribute(m_SigDesc, m_SignalName, "As",
					&LclCharValuePtr))
			{
				strcpy(LclAsIn, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
						m_SignalName, LclAsIn), Response, BufferSize);

				SigTree(m_SigDesc, LclAsIn, m_Port, LclJunkPort,
						&m_Voltage, &m_DcOffset, &m_Coupling, &m_TestEquipImp,
						Response, BufferSize);
			}

			//
			// parse In signal info
			//

			if (atxmlw_Get1641StringAttribute(m_SigDesc, m_SignalName, "In",
					&LclCharValuePtr))
			{
				strcpy(LclAsIn, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s In [%s]",
						m_SignalName, LclAsIn), Response, BufferSize);

				if (atxmlw_Get1641ElementByName(m_SigDesc, LclAsIn, LclElement))
				{
					if (strcmp(LclElement, "Instantaneous") == 0)
					{
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
								LclAsIn, LclElement), Response, BufferSize);

						Instantaneous(m_SigDesc, LclAsIn, LclAsIn2, &m_Trig,
								&m_Slope, LclGate, Response, BufferSize);
						strcpy(LclAsIn, LclAsIn2);
					}

					if (LclAsIn[0] != 0)
						SigTree(m_SigDesc, LclAsIn, m_Port, m_RefPort,
								&m_Voltage, &m_DcOffset, &m_Coupling, &m_TestEquipImp,
								Response, BufferSize);
				}
			}
		}
	}

	atxmlw_Close1641Xml(&m_SigDesc);

	//
	// handle defaults and range checking
	//

	// attenuation
	if (m_Coupling.Exists && m_Coupling.Real == 0)
		dcOff = m_DcOffset.Exists ? m_DcOffset.Real : 0;
	else
		dcOff = 0;

	if (dcOff + (m_Voltage.Exists ? m_Voltage.Real : 0) > 5)
	{
		m_Atten = 10;

		// input impedance
		if (m_TestEquipImp.Exists && m_TestEquipImp.Int == 50)
		{
			if (dcOff + (m_Voltage.Exists ? m_Voltage.Real : 0) > 10)
			{
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-RANGE: total voltage %f bigger than allowed at 50 Ohm",
						dcOff + (m_Voltage.Exists ? m_Voltage.Real : 0)), Response, BufferSize);
			}
		}
	}
	else
	{
		m_Atten = 1;
	}

	// max time defaults to 5 s
	if (m_MeasInfo.TimeOut == 0.)
	{    
		m_MeasInfo.TimeOut = 5.;
	}

	// input coupling defaults to DC
	if (!m_Coupling.Exists)
	{    
		m_Coupling.Exists = true;
		m_Coupling.Int = 0;
	}

	// input impedance defaults to 1 MOhm
	if (!m_TestEquipImp.Exists)
	{    
		m_TestEquipImp.Exists = true;
		m_TestEquipImp.Int = 1000000;
	}

	// trigger slope (totalize only) defaults to POS
	if (strcmp(m_MeasAttr, "totalize") == 0 && !m_Slope.Exists)
		m_Slope.Int = +1;

	// event defaults
	if (m_Start.Exists)
	{
		if (!m_Start.Coupling.Exists)
		{    
			m_Start.Coupling.Exists = true;
			m_Start.Coupling.Int = 0;
		}

		if (!m_Start.TestEquipImp.Exists)
		{    
			m_Start.TestEquipImp.Exists = true;
			m_Start.TestEquipImp.Int = 1000000;
		}
	}

	if (m_Stop.Exists)
	{
		if (!m_Stop.Coupling.Exists)
		{    
			m_Stop.Coupling.Exists = true;
			m_Stop.Coupling.Int = 0;
		}

		if (!m_Stop.TestEquipImp.Exists)
		{    
			m_Stop.TestEquipImp.Exists = true;
			m_Stop.TestEquipImp.Int = 1000000;
		}
	}

	if (m_Sync.Exists)
	{
		if (!m_Sync.Coupling.Exists)
		{    
			m_Sync.Coupling.Exists = true;
			m_Sync.Coupling.Int = 0;
		}

		if (!m_Sync.TestEquipImp.Exists)
		{    
			m_Sync.TestEquipImp.Exists = true;
			m_Sync.TestEquipImp.Int = 1000000;
		}
	}

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateAgE1420B
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::InitPrivateAgE1420B(void)
{
	m_Coupling.Exists = false;
	m_DcOffset.Exists = false;
	m_EventGateFrom.Exists = false;
	m_EventGateTo.Exists = false;
	m_EventSlope.Exists = false;
	m_EventTimeFrom.Exists = false;
	m_EventTimeTo.Exists = false;
	m_Slope.Exists = false;
	m_StrobeToEvent.Exists = false;
	m_TestEquipImp.Exists = false;
	m_Trig.Exists = false;
	m_Voltage.Exists = false;
	m_MaxTime.Exists = false;
	memset(&m_MeasInfo, 0, sizeof(m_MeasInfo));

	m_Start.Exists = false;
	strcpy(m_Start.Port, "");
	m_Start.Coupling.Exists = false;
	m_Start.SettleTime.Exists = false;
	m_Start.Slope.Exists = false;
	m_Start.TestEquipImp.Exists = false;
	m_Start.TrigVolt.Exists = false;
	m_Start.VoltMax.Exists = false;

	m_Stop.Exists = false;
	strcpy(m_Stop.Port, "");
	m_Stop.Coupling.Exists = false;
	m_Stop.SettleTime.Exists = false;
	m_Stop.Slope.Exists = false;
	m_Stop.TestEquipImp.Exists = false;
	m_Stop.TrigVolt.Exists = false;
	m_Stop.VoltMax.Exists = false;

	m_Sync.Exists = false;
	strcpy(m_Sync.Port, "");
	m_Sync.Coupling.Exists = false;
	m_Sync.SettleTime.Exists = false;
	m_Sync.Slope.Exists = false;
	m_Sync.TestEquipImp.Exists = false;
	m_Sync.TrigVolt.Exists = false;
	m_Sync.VoltMax.Exists = false;

	m_Common = false;

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataAgE1420B
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::NullCalDataAgE1420B(void)
{
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: Instantaneous
//
// Purpose: Parse an Instantaneous 1641 node
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// SigDesc            ATXMLW_XML_HANDLE    Signal Description
// Name               char *               name value of node
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// In                 char **              In name               
// Trig               AttributeStruct *    nominal value
// Slope              AttributeStruct *    condition (+/-1)
// Gate               char **              Gate name
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::Instantaneous(ATXMLW_XML_HANDLE SigDesc, char *Name, char *In, AttributeStruct *Trig, AttributeStruct *Slope, char *Gate, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char  *LclCharValuePtr = NULL;
	char   LclUnit[ATXMLW_MAX_NAME];

	memset(LclUnit, '\0', sizeof(LclUnit));

	// nominal
	if (atxmlw_Get1641DoubleAttribute(SigDesc, Name, "nominal",
			&Trig->Real, LclUnit))
	{
		Trig->Exists = true;
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s nominal [%e %s]",
				Name, Trig->Real, LclUnit), Response, BufferSize);
	}

	// condition
	if (atxmlw_Get1641StringAttribute(SigDesc, Name, "condition",
			&LclCharValuePtr))
	{
		Slope->Exists = true;
		Slope->Int = strcmp(LclCharValuePtr, "GE") == 0 ? +1 : -1;
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s condition [%d]",
				Name, Slope->Int), Response, BufferSize);
	}

	// Gate
	if (atxmlw_Get1641StringAttribute(SigDesc, Name, "Gate",
			&LclCharValuePtr))
	{
		strcpy(Gate, LclCharValuePtr);
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s Gate [%s]",
				Name, Gate), Response, BufferSize);
	}
	else
		strcpy(Gate, "");
	// In
	if (atxmlw_Get1641StringAttribute(SigDesc, Name, "In",
			&LclCharValuePtr))
	{
		strcpy(In, LclCharValuePtr);
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s In [%s]",
				Name, In), Response, BufferSize);
	}
	else
		strcpy(In, "");

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: SigTree
//
// Purpose: Parse a 1641 "input" signal tree
//          (HighPass/Load/TwoWire/Sum/Sinusoid/Constant)
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// SigDesc            ATXMLW_XML_HANDLE    Signal Description
// Name               char *               name value of node
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Port               char *               input port
// RefPort            char *               reference port
// SigAmp             AttributeStruct *    amplitude of signal (i.e., sinusoid) part
// DcOffset           AttributeStruct *    amplitude of constant part
// Coupling           AttributeStruct *    input coupling cutoff [Hz]
// Impedance          AttributeStruct *    input impedance
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::SigTree(ATXMLW_XML_HANDLE SigDesc, char *Name, char *Port, char *RefPort, AttributeStruct *SigAmp, AttributeStruct *DcOffset, AttributeStruct *Coupling,
        AttributeStruct *Impedance, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char   *LclCharValuePtr = NULL;
	double  LclDblValue = 0.0;
	char    LclElement[ATXMLW_MAX_NAME];
	char    LclSum[ATXMLW_MAX_NAME];
	char    LclUnit[ATXMLW_MAX_NAME];
	char    nextName[ATXMLW_MAX_NAME];

	memset(LclElement, '\0', sizeof(LclElement));
	memset(LclSum, '\0', sizeof(LclSum));
	memset(LclUnit, '\0', sizeof(LclUnit));
	memset(nextName, '\0', sizeof(nextName));

	strcpy(nextName, Name);

	if (atxmlw_Get1641ElementByName(SigDesc, nextName, LclElement))
	{
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
				nextName, LclElement), Response, BufferSize);

		// coupling
		if (strcmp(LclElement, "HighPass") == 0)
		{
			if (atxmlw_Get1641DoubleAttribute(SigDesc, nextName, "cutoff",
					&LclDblValue, LclUnit))
			{
				Coupling->Exists = true;
				Coupling->Int = (int)LclDblValue;
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s cutoff %d [%s]",
						LclElement, Coupling->Int, LclUnit), Response, BufferSize);
			}

			// get next level...
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "In", &LclCharValuePtr))
			{
				strcpy(nextName, LclCharValuePtr);
				if (atxmlw_Get1641ElementByName(SigDesc, nextName, LclElement))
				{
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
							nextName, LclElement), Response, BufferSize);
				}
			}
			else
				return;
		}

		// impedance
		if (strcmp(LclElement, "Load") == 0)
		{
			if (atxmlw_Get1641DoubleAttribute(SigDesc, nextName, "resistance",
					&LclDblValue, LclUnit))
			{
				Impedance->Exists = true;
				Impedance->Int = (int)LclDblValue;
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s resistance %d [%s]",
						LclElement, Impedance->Int, LclUnit), Response, BufferSize);
			}

			// get next level...
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "In", &LclCharValuePtr))
			{
				strcpy(nextName, LclCharValuePtr);
				if (atxmlw_Get1641ElementByName(SigDesc, nextName, LclElement))
				{
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
							nextName, LclElement), Response, BufferSize);
				}
			}
			else
				return;
		}

		// port(s)
		if (strcmp(LclElement, "TwoWire") == 0)
		{
			// hi
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "hi", &LclCharValuePtr))
			{
				strcpy(Port, LclCharValuePtr);
				strcpy(nextName, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s hi %s",
						LclElement, Port), Response, BufferSize);
			}

			// get next level...
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "In", &LclCharValuePtr))
			{
				strcpy(nextName, LclCharValuePtr);
				if (atxmlw_Get1641ElementByName(SigDesc, nextName, LclElement))
				{
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
							nextName, LclElement), Response, BufferSize);
				}
			}
			else
				return;
		}
		else if (strcmp(LclElement, "FourWire") == 0)
		{
			// hi
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "hi",
					&LclCharValuePtr))
			{
				strcpy(Port, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s hi %s",
						LclElement, Port), Response, BufferSize);
			}

			// shi
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "shi",
					&LclCharValuePtr))
			{
				strcpy(RefPort, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s shi %s",
						LclElement, RefPort), Response, BufferSize);
			}

			return;
		}

		// sum
		if (strcmp(LclElement, "Sum") == 0)
		{
			if (atxmlw_Get1641StringAttribute(SigDesc, nextName, "In",
					&LclCharValuePtr))
			{
				strcpy(LclSum, LclCharValuePtr);
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s In %s",
						LclElement, LclSum), Response, BufferSize);

				// parse In
				if ((LclCharValuePtr = strstr(LclSum, " ")) != NULL)    // find the separating space
				{                                                       //   if one exists
					*LclCharValuePtr = '\0';                            // set it to NULL
					strcpy(nextName, ++LclCharValuePtr);                    // continue with rest of string

					// get next level...
					if (atxmlw_Get1641ElementByName(SigDesc, LclSum, LclElement))
					{
						ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
								LclSum, LclElement), Response, BufferSize);
					}

					// DC offset
					if (strcmp(LclElement, "Constant") == 0)
					{
						if (atxmlw_Get1641DoubleAttribute(SigDesc, LclSum, "amplitude",
								&DcOffset->Real, LclUnit))
						{
							DcOffset->Exists = true;
							ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s amplitude %f [%s]",
									LclElement, DcOffset->Real, LclUnit), Response, BufferSize);
						}
					}
                
					// now continue on to process the Sinusoid portion
				}

				// get next level...
				if (atxmlw_Get1641ElementByName(SigDesc, nextName, LclElement))
				{
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s %s",
							nextName, LclElement), Response, BufferSize);
				}
			}
		}

		// signal amplitude
		if (strcmp(LclElement, "Sinusoid") == 0)
		{
			if (atxmlw_Get1641DoubleAttribute(SigDesc, nextName, "amplitude",
					&SigAmp->Real, LclUnit))
			{
				SigAmp->Exists = true;
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s amplitude %f [%s]",
						LclElement, SigAmp->Real, LclUnit), Response, BufferSize);

				if (SigAmp->Real < 0.1 || SigAmp->Real > 100.0)
				{
					ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-RANGE: amplitude %f [%s] outside allowed range [0.1-100]",
							SigAmp->Real, LclUnit), Response, BufferSize);
				}
			}
		}
	}
	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: PortSetup
//
// Purpose: Parse a 1641 "input" signal tree
//          (HighPass/Load/TwoWire/Sum/Sinusoid/Constant)
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Port               char *               input port
// Atten              AttributeStruct      input attenuation
// Coupling           AttributeStruct      input coupling cutoff [Hz]
// Impedance          AttributeStruct      input impedance
// Trigger            AttributeStruct      trigger level
// Slope              AttributeStruct      trigger slope
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::PortSetup(char *Port, AttributeStruct Coupling, AttributeStruct Impedance, AttributeStruct Trigger, AttributeStruct Slope, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char      scpi[80];
	int       Status = 0;   

	memset(scpi, '\0', sizeof(scpi));

	// input routing
	if ((strcmp(m_MeasAttr, "timeInterval") == 0) && (m_Common))
	{
		sprintf(scpi, "INPUT1:ROUT COMM");
	}
	else
	{
		sprintf(scpi, "INPUT1:ROUT SEP");
	}
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	// input impedance
	sprintf(scpi, "INPUT%s:IMP %d", Port, Impedance.Int);
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	// attenuation
	sprintf(scpi, "INPUT%s:ATT %d", Port, m_Atten);
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	// input coupling
	sprintf(scpi, "INPUT%s:COUP %s", Port, Coupling.Int == 100 ? "AC" : "DC");
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);


	if( Trigger.Exists)
	{
		//account for attenuation
		Trigger.Real = (Trigger.Real / (double)m_Atten);
		sprintf(scpi, "SENS%s:EVEN:LEV %.3lf", Port, Trigger.Real);         
	}
	else
	{
		sprintf(scpi, "SENS%s:EVEN:LEV:AUTO ON", Port);
	}
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	// trigger slope
	if (Slope.Exists)
	{
		sprintf(scpi, "SENS%s:EVEN:SLOP %s", Port, Slope.Int == +1 ? "POS" : "NEG");
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
			ErrorAgE1420B(Status, Response, BufferSize);
    
	}

	// add hysteresis
	sprintf(scpi, "SENS%s:EVEN:HYST DEF", Port);
	IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
	ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
	if (Status)
		ErrorAgE1420B(Status, Response, BufferSize);

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: GateSetup
//
// Purpose: Parse a 1641 "input" signal tree
//          (HighPass/Load/TwoWire/Sum/Sinusoid/Constant)
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Start              event                start event structure
// Stop               event                stop event structure
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::GateSetup(event Start, event Stop, event Sync, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	char      scpi[80];
	int       Status = 0;

	memset(scpi, '\0', sizeof(scpi));

	// gate start
	if (Start.Exists)
	{
		sprintf(scpi, "ARM:STAR:SOUR %s", Start.Port);
		IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
		ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
		if (Status)
			ErrorAgE1420B(Status, Response, BufferSize);

		if (Start.Slope.Exists)
		{
			sprintf(scpi, "ARM:STAR:SLOP %s", Start.Slope.Int == +1 ? "POS" : "NEG");
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
				ErrorAgE1420B(Status, Response, BufferSize);
		}

		if (Start.TrigVolt.Exists)
		{
			sprintf(scpi, "ARM:STAR:LEV %e", Start.TrigVolt.Real);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
				ErrorAgE1420B(Status, Response, BufferSize);
		}

		// gate stop
		if (Stop.Exists)
		{
			sprintf(scpi, "ARM:STOP:SOUR %s", Stop.Port);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
				ErrorAgE1420B(Status, Response, BufferSize);

			if (Stop.Slope.Exists)
			{
				sprintf(scpi, "ARM:STOP:SLOP %s", Stop.Slope.Int == +1 ? "POS" : "NEG");
				IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
				ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
				if (Status)
					ErrorAgE1420B(Status, Response, BufferSize);
			}

			if (Stop.TrigVolt.Exists)
			{
				sprintf(scpi, "ARM:STOP:LEV %e", Stop.TrigVolt.Real);
				IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
				ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
				if (Status)
					ErrorAgE1420B(Status, Response, BufferSize);
			}
		}   
	}
	else if (Sync.Exists)
	{
    
		//sprintf(scpi, "ARM:STAR:SOUR %s", Sync.Port);
		// ch1 and ch2 are not arm source choices       
		if((strcmp(Sync.Port,"1") != 0) && (strcmp(Sync.Port,"2") != 0))
		{
			sprintf(scpi, "ARM:STAR:SOUR %s", Sync.Port);        

			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
				ErrorAgE1420B(Status, Response, BufferSize);
		}


		if (Start.Slope.Exists)
		{
			sprintf(scpi, "ARM:STAR:SLOP %s", Sync.Slope.Int == +1 ? "POS" : "NEG");
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
				ErrorAgE1420B(Status, Response, BufferSize);
		}

		if (Start.TrigVolt.Exists)
		{
			sprintf(scpi, "ARM:STAR:LEV %e", Sync.TrigVolt.Real);
			IFNSIM(Status = hpe1420b_writeInstrData(m_Handle, scpi));
			ATXMLW_DEBUG(9, atxmlw_FmtMsg(scpi), Response, BufferSize);
			if (Status)
				ErrorAgE1420B(Status, Response, BufferSize);
		}
	}
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: SetAtten
//
// Purpose: Set the system attenuation for a channel(s)
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE* Return any error codes and messages
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::SetAtten(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	if (m_Trig.Exists)
	{
		if ((m_Trig.Real < - 5.0) || (m_Trig.Real > 5.0))
		{
			m_Atten = 10;
		}
		else
		{
			m_Atten = 1;
		}
	}

	if (strcmp(m_MeasAttr, "timeInterval") == 0)
	{
		if ((m_Stop.TrigVolt.Real < -5.0) || (m_Stop.TrigVolt.Real > 5.0))
		{
			m_Atten = 10;
		}
	}

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: SanitizeAgE1420B
//
// Sets instrument to lowest stim and highest impedance settings. Opens all output relays
//
//
///////////////////////////////////////////////////////////////////////////////
void CAgE1420B_T::SanitizeAgE1420B(void)
{
	IFNSIM(hpe1420b_writeInstrData(m_Handle, "INP1:IMP 1E6"));
	IFNSIM(hpe1420b_writeInstrData(m_Handle, "INP2:IMP 1E6"));
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: s_IssueDriverFunctionCallDeviceXxx
//
// Purpose: Parse the DriverFunction XML and execute requested Driver Function
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// DeviceHandle       int                  Instrument PnP/Vi Handle 
// DriverFunction     ATXMLW_INTF_DRVRFNC* Pointer to DriverFunction XML String
//
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return: 
//      void
//
///////////////////////////////////////////////////////////////////////////////
void s_IssueDriverFunctionCallDeviceXxx(int DeviceHandle, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	ATXMLW_DF_VAL RetVal;
	ATXMLW_XML_HANDLE DfHandle=NULL;
	char Name[ATXMLW_MAX_NAME];

	memset(Name, '\0', sizeof(Name));

/*
	<AtXmlDriverFunctionCall>
		<SignalResourceName>DEVX_1</SignalResourceName>
		<SignalChannelName>Chan1</SignalChannelName>
		<DriverFunctionCall>
			<FunctionName>TestParam</FunctionName>
			<ReturnType>RetInt32</ReturnType>
			<Parameter   ParamNumber="1" ParamType="Handle" Value=""/>
			<Parameter   ParamNumber="2" ParamType="SrcUInt16" Value="0x3B05"/>
			<Parameter   ParamNumber="3" ParamType="SrcDblPtr" Value="35.e+3 36.0 37.0e5"/>
			<Parameter   ParamNumber="4" ParamType="SrcStrPtr" Value="xxx yyy zzz"/>
			<Parameter   ParamNumber="5" ParamType="RetInt8"/>
			<Parameter   ParamNumber="6" ParamType="RetStrPtr" Size="52"/>
		</DriverFunctionCall>
	</AtXmlDriverFunctionCall>
*/
	if((atxmlw_ParseDriverFunction(DeviceHandle, DriverFunction, &DfHandle, Response, BufferSize)) ||
	   (DfHandle == NULL))
		return;

	atxmlw_GetDFName(DfHandle,Name);
	RetVal.Int32 = 0;
	///////// Implement Supported Driver Function Calls ///////////////////////
	// For now only the standard Vi calls
	if((strncmp("vi",Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal))
		int x = 0;
	else
	{
		atxmlw_ErrorResponse("", Response, BufferSize, "IssueDriverFunction ",
						   ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, 
						   atxmlw_FmtMsg(" Invalid/Unimplemented Function [%s]",Name));
	}
	// return "Ret..." values and RetVal value to caller
	atxmlw_ReturnDFResponse(DfHandle,RetVal,Response,BufferSize);
	atxmlw_CloseDFXml(DfHandle);

	return;
}


////////////////////////////////////////////////////////////////////////////////
// Function : dodebug(int, char*, char* format, ...);                         //
// Purpose  : Print the message to a debug file.                              //
// Return   : None if it don't work o well                                    //
////////////////////////////////////////////////////////////////////////////////
void dodebug(int code, char *function_name, char *format, ...)
{

	static int FirstRun = 0;
	char TmpBuf[_MAX_PATH];

	if (DE_BUG == 1 && FirstRun == 0) 
	{
		sprintf(TmpBuf, "%s", DEBUGIT_1420Arb);
		if ((debugfp = fopen(TmpBuf, "w+b")) == NULL) 
		{
			DE_BUG = 0;
			return;
		}
		FirstRun++;
	}

	va_list	arglist;

	if (DE_BUG) 
	{
		if (code != 0) 
		{
			fprintf(debugfp, "%s in %s\r\n", strerror(code), function_name);
		}
		else
		{
			va_start(arglist, format);
			fprintf(debugfp, "In function %s ", function_name);
			vfprintf(debugfp, format, arglist);
			fprintf(debugfp, "\r\n");
			va_end(arglist);
		}

		fflush(debugfp);
	}

	return;
}
