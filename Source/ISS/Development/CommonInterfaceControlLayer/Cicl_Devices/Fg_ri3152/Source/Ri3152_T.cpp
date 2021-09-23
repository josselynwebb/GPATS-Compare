
///////////////////////////////////////////////////////////////////////////78901234567
// File:	    Ri3152_T.cpp
//
// Date:	    30JAN06
//
// Purpose:	    ATXMLW Instrument Driver for Ri3152
//
// Instrument:	Ri3152  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason								Author		
// =======  =======		=======================================			=====================
// 1.1.0.0  27Oct06		Initial Release									EADS, North America Defense
// 1.1.0.1	14Nov06		The following changes were made:				M.Hendricksen, EADS
//						- In Setup, changed the default of trigger-delay
//							from 0 to 10.  Valid params are 10-2000000 
//							clock cycles.
//						- Corrected code to calculate clock cycles.  The 
//							delay is sent in seconds must convert to clock
//							cycles.	
// 1.1.0.2  15Nov11     The following changes were made:
//                      - Updated VIPER/T CICL for new 3152B Function   B.Kachadurian, AIDI
//                          Generator
//
///////////////////////////////////////////////////////////////////////////////
#include "visa.h"
#include "AtxmlWrapper.h"
//#include "ri3152a.h"
#include "ri3151.h"
//#include "ri3152b.h"
#include "Ri3152_T.h"

// Local Defines

// Function codesb
#define DC_SIGNAL		 0
#define AC_SIGNAL		 1
#define SQUARE_WAVE		 2
#define TRIANGLE_WAVE    3
#define RAMP_SIGNAL		 4
#define PULSED_DC		 5
#define SINC_WAVE		 6
#define EXP_PULSE		 7
#define WAVEFORM		 8

#define POS_SLOPE			1
#define NEG_SLOPE			0

#define TRIGGER  			0
#define GATE     			1

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables
bool trigflag;

// Variables
ViUInt32 rxCount = 0, txCount = 0;
ViChar txBuff[100];
ViChar rxBuff[100]; 

// Local Function Prototypes
void string_to_array(char input [], double array [], int * size);

static void s_IssueDriverFunctionCallDeviceXxx(int Handle , ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize);

int	DE_BUG = 0;
FILE *debugfp = 0;
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CRi3152_T
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
CRi3152_T::CRi3152_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, 
					 int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;
    //int  WriteCount;
	ISDODEBUG(dodebug(0, "CRi3152_T()", "Entering function", (char*)NULL));
    // Save Device Info
    m_InstNo = Instno;
    m_ResourceType = ResourceType;
    m_ResourceName[0] = '\0';
    if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }

    m_Sim = Sim;
    m_Dbg = Dbglvl;
    // Save Address Information
    if(AddressInfoPtr)
    {
        m_AddressInfo.ResourceAddress = AddressInfoPtr->ResourceAddress;
        if(AddressInfoPtr->InstrumentQueryID)
        {
            strnzcpy(m_AddressInfo.InstrumentQueryID, 
                         AddressInfoPtr->InstrumentQueryID, ATXMLW_MAX_NAME);
        }

        m_AddressInfo.InstrumentTypeNumber = AddressInfoPtr->InstrumentTypeNumber;
        if(AddressInfoPtr->ControllerType)
        {
            strnzcpy(m_AddressInfo.ControllerType, 
                         AddressInfoPtr->ControllerType, ATXMLW_MAX_NAME);
        }

        m_AddressInfo.ControllerNumber = AddressInfoPtr->ControllerNumber;
        m_AddressInfo.PrimaryAddress = AddressInfoPtr->PrimaryAddress;
        m_AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
        m_AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
    }


    m_Handle= NULL;
    m_InitString[0] = '\0';

    InitPrivateRi3152();
	NullCalDataRi3152();

    // The Form Init String
    sprintf(m_InitString,"%s%d::%d::INSTR", m_AddressInfo.ControllerType,m_AddressInfo.ControllerNumber, m_AddressInfo.PrimaryAddress);
    sprintf(LclMsg,"Wrap-CRi3152 Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);
	ISDODEBUG(dodebug(0, "CRi3152_T()", "CRi3152 Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg, (char*)NULL));

    // Initialize the Ri3152
	IFNSIM(Status = ri3151_init(m_InitString, false, true, &m_Handle));

    if(Status && ErrorRi3152(Status, Response, Buffersize))
	{
        return;
	}

	SanitizeRi3152();
	
	ISDODEBUG(dodebug(0, "CRi3152_T()", "Leaving function", (char*)NULL));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CRi3152_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================b
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CRi3152_T::~CRi3152_T()
{
    char Dummy[1024];

	ISDODEBUG(dodebug(0, "~CRi3152_T()", "Entering function", (char*)NULL));
    // Reset the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CRi3152 Class Distructor called "),Dummy,1024);

	//turn output off
	IFNSIM(ri3151_output(m_Handle, RI3151_OUTPUT_OFF));

    // Reset the Ri3152
	IFNSIM(ri3151_reset(m_Handle));

	ISDODEBUG(dodebug(0, "~CRi3152_T()", "Leaving function", (char*)NULL));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusRi3152
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
int CRi3152_T::StatusRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

	ISDODEBUG(dodebug(0, "StatusRi3152()", "Entering function", (char*)NULL));
    // Status action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusRi3152 called "), Response, BufferSize);
    // Check for any pending error messages
    Status = ErrorRi3152(0, Response, BufferSize);
	ISDODEBUG(dodebug(0, "StatusRi3152()", "Leaving function", (char*)NULL));

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalRi3152
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
int CRi3152_T::IssueSignalRi3152(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

	ISDODEBUG(dodebug(0, "IssueSignalRi3152()", "Entering function", (char*)NULL));
    // IEEE 1641 Issue Signal action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalRi3152 Signal: "), Response, BufferSize);

    if((Status = GetStmtInfoRi3152(SignalDescription, Response, BufferSize)) != 0)
	{
        return(Status);
	}

	ISDODEBUG(dodebug(0, "IssueSignalRi3152()", "Action is [%s]", m_Action, (char*)NULL));

    switch(m_Action)
    {
		case ATXMLW_SA_APPLY:
			if((Status = SetupRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			if((Status = EnableRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}
				
			break;

		case ATXMLW_SA_REMOVE:
			if((Status = DisableRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			if((Status = ResetRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_MEASURE:
			if((Status = SetupRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_READ:
			if((Status = EnableRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			if((Status = FetchRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_RESET:
			if((Status = ResetRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_SETUP:
			if((Status = SetupRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_CONNECT:
			break;

		case ATXMLW_SA_ENABLE:
			if((Status = EnableRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_DISABLE:
			if((Status = DisableRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;

		case ATXMLW_SA_FETCH:
			if((Status = FetchRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}
				
			break;

		case ATXMLW_SA_DISCONNECT:
			break;

		case ATXMLW_SA_STATUS:
			if((Status = StatusRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
			
		case ATXMLW_SA_IDENTIFY:
			if ((Status = StatusRi3152(Response, BufferSize)) != 0)
			{
				return(Status);
			}

			break;
    }

	ISDODEBUG(dodebug(0, "IssueSignalRi3152()", "Leaving function", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalRi3152
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
int CRi3152_T::RegCalRi3152(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
//    char     *CalString;
//    ATXMLW_XML_HANDLE CalHandle;
    char      Dummy[1024];

	ISDODEBUG(dodebug(0, "RegCalRi3152()", "Entering function", (char*)NULL));
    // Setup action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalRi3152 CalData: %s", CalData),Dummy,1024);
	
	ISDODEBUG(dodebug(0, "RegCalRi3152()", "Leaving function", (char*)NULL));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetRi3152
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
int CRi3152_T::ResetRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";
	
	ISDODEBUG(dodebug(0, "ResetRi3152()", "Entering function", (char*)NULL));
    // Reset action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetRi3152 called "), Response, BufferSize);

	//disable output
	IFNSIM((Status = ri3151_output(m_Handle, RI3151_OUTPUT_OFF)));
	if(Status)
	{
        ErrorRi3152(Status, Response, BufferSize);
	}
    
	// Reset the Ri3152
	IFNSIM((Status = ri3151_reset(m_Handle)));
    if(Status)
	{
        ErrorRi3152(Status, Response, BufferSize);
	}

	if(m_MaxTime.Exists)
	{
		m_MaxTime.Real=10*1000; //default 10 seconds
		IFNSIM(Status=viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (unsigned long) m_MaxTime.Real));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}
	}

	InitPrivateRi3152();
	SanitizeRi3152();
	  
	ISDODEBUG(dodebug(0, "ResetRi3152()", "Leaving function", (char*)NULL));
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstRi3152
//
// Purpose: Perform the SelfTest Action action for this driver instance
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Level              ATXMLW_INTF_STLEVEL    Indicates the Instrument Level To Be Performed
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
int CRi3152_T::IstRi3152(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
	ISDODEBUG(dodebug(0, "IstRi3152()", "Entering function", (char*)NULL));
    // Reset action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstRi3152 called Level %d", 
                              Level), Response, BufferSize);
    // Reset the Ri3152
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			Status = StatusRi3152(Response,BufferSize);
			break;
		case ATXMLW_IST_LVL_IST:
			break;
		case ATXMLW_IST_LVL_CNF:
			Status = StatusRi3152(Response,BufferSize);
			break;
		default: // Hopefully BIT 1-9
			break;
    }
    if(Status)
	{
        ErrorRi3152(Status, Response, BufferSize);
	}

    InitPrivateRi3152();
	
	ISDODEBUG(dodebug(0, "IstRi3152()", "Entering function", (char*)NULL));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsRi3152
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
int CRi3152_T::IssueNativeCmdsRi3152(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsRi3152()", "Entering function", (char*)NULL));
    // Setup action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsRi3152 "), Response, BufferSize);

    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }

	//If *RST Issues, Sanitize Instrument
	if(strstr("*RST", InstrumentCmds))
	{
		SanitizeRi3152();
	}
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsRi3152()", "Leaving function", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallRi3152
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
int CRi3152_T::IssueDriverFunctionCallRi3152(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallRi3152()", "Entering function", (char*)NULL));
    // Setup action for the Ri3152
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallRi3152", Response, BufferSize);

	    // Retrieve the Parameters
    IFNSIM(s_IssueDriverFunctionCallDeviceXxx(m_Handle, DriverFunction, Response, BufferSize));
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallRi3152()", "Leaving function", (char*)NULL));

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupRi3152
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Response           ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CRi3152_T::SetupRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	ViInt16  filter;
	ViUInt16 *DACarray;
	ViReal64 percentTime;
	ViInt32	 trigDelayClockCycles = 10;

	ISDODEBUG(dodebug(0, "SetupRi3152()", "Entering function", (char*)NULL));
    // Setup action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupRi3152 called "), Response, BufferSize);

	// Determine Instrument Present
	sprintf(txBuff, "*IDN?");
	txCount = strlen(txBuff);

	IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
	if(Status)
	{
		ErrorRi3152(Status, Response, BufferSize);
	}

	IFNSIM(Status = viRead(m_Handle, (ViPBuf)rxBuff, 1024, &rxCount));  
	if(Status)
	{
		ErrorRi3152(Status, Response, BufferSize);
	}

	if(m_MaxTime.Exists)
	{
		m_MaxTime.Real*=1000; //convert to milliseconds
		IFNSIM(Status=viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (unsigned long) m_MaxTime.Real));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}
	}

	if(m_Burst.Exists)
	{
		IFNSIM(Status = ri3151_operating_mode(m_Handle, RI3151_MODE_BURST));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}

		IFNSIM(Status = ri3151_burst_mode(m_Handle, m_Burst.Int));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}

		if(m_BurstRepRate.Exists)
		{
			IFNSIM(Status = ri3151_trigger_source(m_Handle, RI3151_TRIGGER_INTERNAL));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			IFNSIM(Status = ri3151_trigger_rate(m_Handle, 1/m_BurstRepRate.Real));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}
		}
	}
	//set up triggering
	if(m_Event.Exists)
	{
		if((strstr(rxBuff,"3152B")) != NULL)
		{
		  if(!m_TriggerDelay.Exists)
		  {
			//Time Base
			m_TriggerDelay.Real=100E-9;
		  }
		}
		else
		{
			if(!m_TriggerDelay.Exists)
			{
				//Note: This is not in sec, it is in clock cycle.  See help file for
				//		more details
				trigDelayClockCycles=10;
			}

			else
			{
				//Note: This is not in sec, it is in clock cycles.  See help file for
				//		more details. Convert to clock cycles.  Fsclk default is 1MHZ
				//		if not a waveform.
				if(m_SignalType.Dim == WAVEFORM)
				{
					if(!m_SampleSpacing.Exists)
					{
						trigDelayClockCycles = (ViInt32)(m_TriggerDelay.Real * (1 / 0.00000001));
					}
					else
					{
						trigDelayClockCycles = (ViInt32)(m_TriggerDelay.Real * (1 / m_SampleSpacing.Real));
					}
				}

				else
				{					
					trigDelayClockCycles = (ViInt32)(m_TriggerDelay.Real *  1e6);
				}

				// check for the min and max limits
				if(trigDelayClockCycles <= 10.0)
				{
					trigDelayClockCycles = (ViInt32)10.0;
				}
				if(trigDelayClockCycles >= 2.0e6)
				{
					trigDelayClockCycles = (ViInt32)2.0e6;
				}
			}

		}

		if(!m_TriggerSlope.Exists)
		{
			m_TriggerSlope.Dim=POS_SLOPE;
		}

		if(!m_TriggerVolt.Exists)
		{
			m_TriggerVolt.Real=1.6;
		}

		if(m_Event.Dim==TRIGGER)
		{			
			if(!m_Burst.Exists)//allow burst mode to use external triggering
			{
				IFNSIM(Status = ri3151_operating_mode(m_Handle, RI3151_MODE_TRIG));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
			}

			IFNSIM(Status = ri3151_trigger_source(m_Handle, RI3151_TRIGGER_EXTERNAL));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}
			Sleep(50);
			
			//IFNSIM(Status = ri3151_set_trigger_level (m_Handle, m_TriggerVolt.Real));
			{	
				double triglevel = m_TriggerVolt.Real;
				// checks for treshold level since we can not check treshold level
				// at the allocation time
				if(m_TriggerVolt.Real > 10.0)
				{
					triglevel = 10.0;
				}

				else if (m_TriggerVolt.Real < -10.0)
				{
					triglevel = -10.0;
				}

				Status = viPrintf(m_Handle, "TRIG:LEV %10.2f", triglevel);
			}	
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			IFNSIM(Status = ri3151_trigger_slope (m_Handle, (ViBoolean) m_TriggerSlope.Dim));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			if((strstr(rxBuff,"3152B")) != NULL)
			{
			  // Time Base
			  sprintf(txBuff, ":TRIG:DEL:STAT 1");
			  txCount = strlen(txBuff);
			  IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
			  if(Status)
			  {
		  		  ErrorRi3152(Status, Response, BufferSize);
			  }

			  sprintf(txBuff, ":TRIG:DEL:TIME %g", m_TriggerDelay.Real);
			  txCount = strlen(txBuff);
			  IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
			  if(Status)
			  {
		  		  ErrorRi3152(Status, Response, BufferSize);
			  }
			}
			else
			{
				IFNSIM(Status = ri3151_trigger_delay (m_Handle, trigDelayClockCycles));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
			}
		}

		else if(m_Event.Dim==GATE)
		{			
			IFNSIM(Status =ri3151_operating_mode (m_Handle, RI3151_MODE_GATED));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			IFNSIM(Status =ri3151_trigger_source (m_Handle, RI3151_TRIGGER_EXTERNAL));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			//IFNSIM(Status = ri3151_set_trigger_level (m_Handle, m_TriggerVolt.Real));
			{	
				double triglevel = m_TriggerVolt.Real;
				// checks for treshold level since we can not check treshold level
				// at the allocation time
				if(m_TriggerVolt.Real > 10.0)
				{
					triglevel = 10.0;
				}

				else if (m_TriggerVolt.Real < -10.0)
				{
					triglevel = -10.0;
				}

				Status = viPrintf(m_Handle, "TRIG:LEV %10.2f", triglevel);
			}	
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			IFNSIM(Status = ri3151_trigger_slope (m_Handle, (ViBoolean) m_TriggerSlope.Dim));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			if((strstr(rxBuff,"3152B")) != NULL)
			{
				// Time Base
				sprintf(txBuff, ":TRIG:DEL:STAT 1");
				txCount = strlen(txBuff);
				IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
				
				sprintf(txBuff, ":TRIG:DEL:TIME %g", m_TriggerDelay.Real);
				txCount = strlen(txBuff);
				IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
			}
			else
			{
				IFNSIM(Status = ri3151_trigger_delay (m_Handle, trigDelayClockCycles));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
			}
		}
	}

	if(m_Sync.Exists)
	{
		if((strstr(rxBuff, "3152B")) != NULL)
		{
			IFNSIM(Status = ri3151_output_sync (m_Handle, RI3151_SYNC_BIT, 2, 1));	//CJW removed 1,
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}
		}

		else
		{
			IFNSIM(Status= ri3151_output_sync(m_Handle, RI3151_SYNC_BIT, 2, 1));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}
				
		}

		// Clear status here - this driver call seems to always cause an error on TETS because of the POS arg int(2)
		// For now....Clearing the device status buffer here to remove this error status
		sprintf(txBuff, "*CLS");
		txCount = strlen(txBuff);
		IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
	}

	if(m_SignalType.Dim==WAVEFORM)
	{
		IFNSIM(Status=ri3151_select_waveform_mode (m_Handle, RI3151_MODE_ARB));
	}
	else
	{
		IFNSIM(Status=ri3151_select_waveform_mode (m_Handle, RI3151_MODE_STD));
	}

	if(Status)
	{
		ErrorRi3152(Status, Response, BufferSize);
	}

	switch(m_SignalType.Dim)
	{
		case DC_SIGNAL:
			if((strstr(rxBuff, "3152B")) != NULL)
			{
				sprintf(txBuff, ":FUNC:SHAP DC");
				txCount = strlen(txBuff);
				IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}

				if(!m_Volt.Exists)
				{
					m_Volt.Real = 1;
				}

				sprintf(txBuff, ":VOLT %g", m_Volt.Real);
				txCount = strlen(txBuff);
				IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
				if(Status)
				{
		  			ErrorRi3152(Status, Response, BufferSize);
				}

				sprintf(txBuff, ":DC 100");
				txCount = strlen(txBuff);
				IFNSIM(Status = viWrite(m_Handle, (ViBuf)txBuff, strlen(txBuff), &txCount));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
			}

			else
			{
			// sets max_amplitde for the instrument
				IFNSIM(Status = ri3151_set_amplitude(m_Handle, 16.0));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
 
				if(!m_Volt.Exists)
				{
					m_Volt.Real=1;
				}

				IFNSIM(Status = ri3151_dc_signal(m_Handle, (short) (m_Volt.Real*2/16*100)));
				if(Status)
				{
					ErrorRi3152(Status, Response, BufferSize);
				}
			}
			break;
		case AC_SIGNAL:
			if(!m_Freq.Exists)
			{
				m_Freq.Real=1000;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real=1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real=0;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real=25000000;
			}
		
			IFNSIM(Status = ri3151_sine_wave(m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, 0, 1));
			if(Status)
				ErrorRi3152(Status, Response, BufferSize);

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3; 
					break;
			}
			//IFNSIM(Status= ri3151_filter(m_Handle, filter)); cannot be programmed on sine wave
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}
			break;
		case SQUARE_WAVE:
			if(!m_Freq.Exists)
			{
				m_Freq.Real=1000;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real=1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real=0;
			}

			if(!m_DutyCycle.Exists)
			{
				m_DutyCycle.Real=50;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real=25000000;
			}

			IFNSIM(Status = ri3151_square_wave(m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, (ViInt16)m_DutyCycle.Real));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3; 
					break;
			}

			IFNSIM(Status= ri3151_filter(m_Handle, filter));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			break;
		case TRIANGLE_WAVE:
			if(!m_Freq.Exists)
			{
				m_Freq.Real = 1000;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real = 1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real = 0;
			}

			if(!m_DutyCycle.Exists)
			{
				m_DutyCycle.Real = 50;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real = 25000000;
			}

			IFNSIM(Status= ri3151_triangular_wave(m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, 0, 1));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3; 
					break;
			}
			IFNSIM(Status= ri3151_filter(m_Handle, filter));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			break;
		case RAMP_SIGNAL:
			if(!m_Freq.Exists)
			{
				m_Freq.Real = 1;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real = 1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real = 0;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real = 25000000;
			}

			if(!m_RiseTime.Exists)
			{
				m_RiseTime.Real = 1;
			}

			percentTime = m_RiseTime.Real*m_Freq.Real*100;

			IFNSIM(Status= ri3151_ramp_wave(m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, 0.0, percentTime, 100-percentTime));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3; 
					break;
			}
			IFNSIM(Status = ri3151_filter(m_Handle, filter));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			break;
		case PULSED_DC:
			if(!m_Freq.Exists)
			{
				m_Freq.Real = 1000;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real = 1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real = 0;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real = 25000000;
			}

			if(!m_RiseTime.Exists)
			{
				m_RiseTime.Real = .25;
			}

			if(!m_PulseWidth.Exists)
			{
				m_PulseWidth.Real = .5;
			}

			if(!m_FallTime.Exists)
			{
				m_FallTime.Real = .25;
			}

			IFNSIM(Status= ri3151_pulse_wave (m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, 0.0, m_RiseTime.Real*m_Freq.Real*100, 
							m_PulseWidth.Real*m_Freq.Real*100, m_FallTime.Real*m_Freq.Real*100));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3; 
					break;
			}
			IFNSIM(Status = ri3151_filter(m_Handle, filter));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			break;
		case SINC_WAVE:
			if(!m_Freq.Exists)
			{
				m_Freq.Real = 1000;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real = 1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real = 0;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real = 25000000;
			}

			IFNSIM(Status = ri3151_sinc_wave (m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, 10));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3;
					break;
			}

			IFNSIM(Status= ri3151_filter(m_Handle, filter));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			break;
		case EXP_PULSE:
			if(!m_Freq.Exists)
			{
				m_Freq.Real = 1000;
			}

			if(!m_Volt.Exists)
			{
				m_Volt.Real = 1;
			}

			if(!m_DCOffset.Exists)
			{
				m_DCOffset.Real = 0;
			}

			if(!m_Bandwidth.Exists)
			{
				m_Bandwidth.Real = 25000000;
			}

			if(!m_Exponent.Exists)
			{
				m_Exponent.Real = -1;
			}

			IFNSIM(Status= ri3151_exponential_wave (m_Handle, m_Freq.Real, m_Volt.Real, m_DCOffset.Real, (ViInt16)m_Exponent.Real));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			switch ((int)m_Bandwidth.Real)
			{
				case 20000000: 
					filter = 1; 
					break;
				case 25000000: 
					filter = 2; 
					break;
				case 50000000: 
					filter = 3; 
					break;
			}

			IFNSIM(Status= ri3151_filter(m_Handle, filter));
			if(Status)
			{
				ErrorRi3152(Status, Response, BufferSize);
			}

			break;
	case WAVEFORM:
		if(!m_StimSize.Exists)
		{
			m_StimSize.Int = 1;
		}

		if(!m_DCOffset.Exists)
		{
			m_DCOffset.Real = 0;
		}

		if(!m_Volt.Exists)
		{
			m_Volt.Real = 1;
		}

		if(!m_SampleSpacing.Exists)
		{
			m_SampleSpacing.Real = 0.00000001;
		}

		IFNSIM(Status = ri3151_define_arb_segment(m_Handle, 1, m_StimSize.Int));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}
		
		DACarray = new ViUInt16[m_StimSize.Int];
		for(int i = 0; i < m_StimSize.Int; i++)
		{
			DACarray[i] = (ViUInt16)(((m_StimData[i])/(m_Volt.Real)*65536 + 32768)) & 0xFFF0;
		}

		IFNSIM(Status= ri3151_load_arb_data(m_Handle, 1, DACarray, (ViInt32) m_StimSize.Int));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}

		delete DACarray;

		IFNSIM(Status= ri3151_output_arb_waveform(m_Handle, 1, 1/m_SampleSpacing.Real, m_Volt.Real, m_DCOffset.Real, RI3151_CLK_SOURCE_INT));
		if(Status)
		{
			ErrorRi3152(Status, Response, BufferSize);
		}

		break;
	}

	if((strstr(rxBuff,"3152B")) != NULL)
	{
		if(m_Event.Exists && m_Burst.Exists && m_Event.Dim != GATE)
		{
		  Sleep((m_TriggerDelay.Real + ((1/m_Freq.Real) * m_Burst.Int) + (1/m_Freq.Real)) * 1000);
		}
		if(m_Event.Exists && !m_Burst.Exists && m_Event.Dim != GATE)
		{
	      Sleep((m_TriggerDelay.Real + (2 * (1 / m_Freq.Real))) * 1000);
		}
	}

    if(Status)
	{
        ErrorRi3152(Status, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "SetupRi3152()", "Leaving function", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchRi3152
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
int CRi3152_T::FetchRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char   *ErrMsg = "";
	
	ISDODEBUG(dodebug(0, "FetchRi3152()", "Entering function", (char*)NULL));
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000;
	}

    // Fetch action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchRi3152 called "), Response, BufferSize);
	   
	ISDODEBUG(dodebug(0, "FetchRi3152()", "Leaving function", (char*)NULL));
    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableRi3152
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
int CRi3152_T::DisableRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
	ISDODEBUG(dodebug(0, "DisableRi3152()", "Entering function", (char*)NULL));
    // Open action for the Ri3152
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenRi3152 called "), Response, BufferSize);

    //////// Place Ri3152 specific data here //////////////
	IFNSIM(Status=ri3151_output(m_Handle, RI3151_OUTPUT_OFF));
	if(Status)
	{
		ErrorRi3152(Status, Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "DisableRi3152()", "Leaving function", (char*)NULL));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableRi3152
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
int CRi3152_T::EnableRi3152(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
    // Close action for the Ri3152
	ISDODEBUG(dodebug(0, "EnableRi3152()", "Entering function", (char*)NULL));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableRi3152 called "), Response, BufferSize);
	
	IFNSIM(Status=ri3151_output(m_Handle, RI3151_OUTPUT_ON));
	if(Status)
	{
		ErrorRi3152(Status, Response, BufferSize);
	}
    
	ISDODEBUG(dodebug(0, "EnableRi3152()", "Leaving function", (char*)NULL));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorRi3152
//
// Purpose: Query Ri3152 for the error text and send to WRTS
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
int  CRi3152_T::ErrorRi3152(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int retval;
	int Err = 0;
	char Msg[MAX_MSG_SIZE];
	ViInt32  QError;

	retval = Status;
	Msg[0] = '\0';
	
	ISDODEBUG(dodebug(0, "ErrorRi3152()", "Entering function", (char*)NULL));
	if(Status)
	{
		// Decode Ri3152 lib return code
		IFNSIM((Err = ri3151_error_message(m_Handle, Status, Msg)));
		
		if(Err)
		{
			sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
		}

		if(Status == VI_ERROR_TMO)
		{
			Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;
		}

		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);
	}

	QError = 1;
	// Retrieve any pending errors in the device
	while(QError)
	{
		QError = 0;
		IFNSIM((Err = ri3151_error_query(m_Handle, &QError, Msg)));
		if(Err != 0)
		{
			break;
		}
		if(QError)
		{            
			if(Status == VI_ERROR_TMO)
			{
				Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;
			}

			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);
		}
	}
	
	ISDODEBUG(dodebug(0, "ErrorRi3152()", "Leaving function", (char*)NULL));
	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoRi3152
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
int CRi3152_T::GetStmtInfoRi3152(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char LclResource[ATXMLW_MAX_NAME];
	trigflag = false;
	
	ISDODEBUG(dodebug(0, "GetStmtInfoRi3152()", "Entering function", (char*)NULL));
    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoRi3152()", "Found Action %d", m_Action, (char*)NULL));

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoRi3152()", "Found Resource %d", LclResource, (char*)NULL));

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoRi3152()", "Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement, (char*)NULL));
	}

	//recursive function called to get all available attributes and store
	Get_Variables(m_SignalName, Response, BufferSize);

    atxmlw_Close1641Xml(&m_SignalDescription);
	
	ISDODEBUG(dodebug(0, "GetStmtInfoRi3152()", "Leaving function", (char*)NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateRi3152
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
void CRi3152_T::InitPrivateRi3152(void)
{
	ISDODEBUG(dodebug(0, "InitPrivateRi3152()", "eNTERING function", (char*)NULL));

	m_Volt.Exists=false;
	m_DCOffset.Exists=false;
	m_Freq.Exists=false;
	m_Burst.Exists=false;
	m_Bandwidth.Exists=false;
	m_DutyCycle.Exists=false;
	m_BurstRepRate.Exists=false;
	m_RiseTime.Exists=false;
	m_FallTime.Exists=false;
	m_Exponent.Exists=false;
	m_StimSize.Exists=false;
	m_SampleSpacing.Exists=false;
	m_TestEquipImp.Exists=false;
	m_MaxTime.Exists=false;
    m_TriggerDelay.Exists=false;
    m_TriggerSlope.Exists=false;
	m_TriggerVolt.Exists=false;
	m_TriggerDelayTo.Exists=false;
    m_TriggerSlopeTo.Exists=false;
	m_TriggerVoltTo.Exists=false;
	m_PulseWidth.Exists=false;
	m_SignalType.Exists=false;
	m_Event.Exists=false;
	m_Sync.Exists=false;

	ISDODEBUG(dodebug(0, "InitPrivateRi3152()", "Leaving function", (char*)NULL));
	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataRi3152
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
void CRi3152_T::NullCalDataRi3152(void)
{    
	ISDODEBUG(dodebug(0, "NullCalDataRi3152()", "Entering function", (char*)NULL));
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
	ISDODEBUG(dodebug(0, "NullCalDataRi3152()", "Leaving function", (char*)NULL));
    return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: Get_Variables
//
// Purpose: Recursively transverses the xml signal to store data in member variables
//
// Input Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
// Name               char *               Name of line to parse
// 
// Output Parameters
// Parameter		  Type			       Purpose
// =================  ===================  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CRi3152_T::Get_Variables(char *Name, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
    char   LclElement[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME];
    char  *LclCharValuePtr;
	char AC_Component[ATXMLW_MAX_NAME], DC_Component[ATXMLW_MAX_NAME], temp[ATXMLW_MAX_NAME];
	ISDODEBUG(dodebug(0, "Get_Variables()", "Leaving function", (char*)NULL));
		
	//get element type
	if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, LclElement)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element [%s]", Name, LclElement, (char*)NULL));
		//Used to determine the base signal type
		if(strcmp(LclElement, "Sinusoid")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=AC_SIGNAL;
		}

		//A square wave can also specify a trigger, make sure an existing signaltype doesn't exist
		//DCOffset is set when a sum type is found(used for extra verification that this is a square wave)
		else if(!m_SignalType.Exists && m_DCOffset.Exists && strcmp(LclElement, "SquareWave")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=SQUARE_WAVE;
		}

		else if(strcmp(LclElement, "Triangle")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=TRIANGLE_WAVE;
		}

		else if(strcmp(LclElement, "Ramp")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=RAMP_SIGNAL;
		}
		else if(strcmp(LclElement, "Trapezoid")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=PULSED_DC;
		}

		else if(strcmp(LclElement, "Sinc")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=SINC_WAVE;
		}

		else if(strcmp(LclElement, "ExponentialWave")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=EXP_PULSE;
		}

		else if(strcmp(LclElement, "WaveformStep")==0)
		{
			m_SignalType.Exists=true;
			m_SignalType.Dim=WAVEFORM;
		}

		if(!m_SignalType.Exists)//Only check for DC if type isn't set, so it isn't mistaken for DC offset
		{
			if(strcmp(LclElement, "Constant")==0)
			{
				m_SignalType.Exists=true;
				m_SignalType.Dim=DC_SIGNAL;
			}
		}
	}

	//try to get any attributes
	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "amplitude", &LclDblValue, LclUnit)))
	{
		if(strcmp(LclElement, "Constant")==0) //may be DC-Offset
		{
			if(m_DCOffset.Exists)
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s DC-Offset %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s DC-Offset %E [%s]", LclElement, LclDblValue,LclUnit, (char*)NULL));
				m_DCOffset.Real=LclDblValue;
			}

			else //amplitude for DC source
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s amplitude %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s amplitude %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
				m_Volt.Exists=true;
				m_Volt.Real=LclDblValue;
			}
		}

		else //amplitude for source
		{
			if(!trigflag)
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s amplitude %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s amplitude %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
				m_Volt.Exists=true;
				m_Volt.Real=2*LclDblValue;
			}
		}
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "frequency", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s frequency %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s frequency %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_Freq.Exists=true;
		m_Freq.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "period", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s period %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s period %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_Freq.Exists=true;
		m_Freq.Real=1/LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "pulses", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s burst %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s burst %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_Burst.Exists=true;
		m_Burst.Int=(int)LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "repetition", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s burst rate %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s burst rate %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_BurstRepRate.Exists=true;
		m_BurstRepRate.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "nominal", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger level %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger level %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_TriggerVolt.Exists=true;
		m_TriggerVolt.Real=LclDblValue;
		m_Event.Exists=true;
		m_Event.Dim=TRIGGER;
		//check for delay here, recursion not possible
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
			ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In  [%s]", Name, LclCharValuePtr,(char*)NULL));

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, temp)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element [%s]", Name, LclElement,(char*)NULL));
				
				if(strcmp(temp, "SignalDelay") == 0)
				{
					if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "delay", &LclDblValue, LclUnit)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger delay %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
						ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger delay %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
						m_TriggerDelay.Exists = true;
						m_TriggerDelay.Real = LclDblValue;
					}
				}
			}
		}
	}

	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "condition", &LclCharValuePtr)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger slope [%s]", LclElement, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger slope  [%s]", LclElement, LclCharValuePtr,(char*)NULL));
		m_TriggerSlope.Exists=true;
		if(strcmp(LclCharValuePtr, "GT") == 0)
		{
			m_TriggerSlope.Dim=POS_SLOPE;
		}

		else
		{
			m_TriggerSlope.Dim=NEG_SLOPE;
		}
		m_Event.Exists=true;
		m_Event.Dim=TRIGGER;
		//check for delay here, recursion not possible
		if(!m_TriggerDelay.Exists && (atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
			ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In  [%s]", Name, LclCharValuePtr,(char*)NULL));

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, temp)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element  [%s]", Name, LclElement,(char*)NULL));
				
				if(strcmp(temp, "SignalDelay")==0)
				{
					if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "delay", &LclDblValue, LclUnit)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger delay %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
						ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger delay %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
						m_TriggerDelay.Exists=true;
						m_TriggerDelay.Real=LclDblValue;
					}
				}
			}
		}
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "startNominal", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger level %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger level %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_TriggerVolt.Exists=true;
		m_TriggerVolt.Real=LclDblValue;
		m_Event.Exists=true;
		m_Event.Dim=GATE;
		//check for delay here, recursion not possible
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
			ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In  [%s]", Name, LclCharValuePtr,(char*)NULL));

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, temp)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element [%s]", Name, LclElement,(char*)NULL));
				
				if(strcmp(temp, "SignalDelay")==0)
				{
					if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "delay", &LclDblValue, LclUnit)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger delay %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
						ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger delay %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
						m_TriggerDelay.Exists=true;
						m_TriggerDelay.Real=LclDblValue;
					}
				}
			}
		}
	}

	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "startCondition", &LclCharValuePtr)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger slope [%s]", LclElement, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger slope %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_TriggerSlope.Exists=true;
		if(strcmp(LclCharValuePtr, "GT")==0)
			m_TriggerSlope.Dim=POS_SLOPE;
		else
			m_TriggerSlope.Dim=NEG_SLOPE;
		m_Event.Exists=true;
		m_Event.Dim=GATE;
		//check for delay here, recursion not possible
		if(!m_TriggerDelay.Exists && (atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
			ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In  [%s]", Name, LclCharValuePtr, (char*)NULL));

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, temp)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element [%s]", Name, LclElement,(char*)NULL));
				
				if(strcmp(temp, "SignalDelay")==0)
				{
					if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "delay", &LclDblValue, LclUnit)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger delay %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
						ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger delay %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
						m_TriggerDelay.Exists=true;
						m_TriggerDelay.Real=LclDblValue;
					}
				}
			}
		}
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "stopNominal", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger level %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger level %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_TriggerVoltTo.Exists=true;
		m_TriggerVoltTo.Real=LclDblValue;
		m_Event.Exists=true;
		m_Event.Dim=GATE;
		//check for delay here, recursion not possible
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
			ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In  [%s]", Name, LclCharValuePtr,(char*)NULL));

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, temp)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element  [%s]", Name, LclElement, (char*)NULL));
				
				if(strcmp(temp, "SignalDelay")==0)
				{
					if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "delay", &LclDblValue, LclUnit)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger delay to %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
						ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger delay %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
						m_TriggerDelayTo.Exists=true;
						m_TriggerDelayTo.Real=LclDblValue;
					}
				}
			}
		}
	}
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "stopCondition", &LclCharValuePtr)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger slope [%s]", LclElement, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger slope  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_TriggerSlope.Exists=true;
		if(strcmp(LclCharValuePtr, "GT")==0)
		{
			m_TriggerSlopeTo.Dim=POS_SLOPE;
		}

		else
		{
			m_TriggerSlopeTo.Dim=NEG_SLOPE;
		}

		m_Event.Exists=true;
		m_Event.Dim=GATE;
		//check for delay here, recursion not possible
		if(!m_TriggerDelayTo.Exists && (atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
			ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In [%s]", Name, LclCharValuePtr,(char*)NULL));

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, temp)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]", Name, LclElement), Response, BufferSize);
				ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element [%s]", Name, LclElement,(char*)NULL));
				
				if(strcmp(temp, "SignalDelay")==0)
				{
					if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, "delay", &LclDblValue, LclUnit)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s trigger delay to %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
						ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s trigger delay %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
						m_TriggerDelayTo.Exists=true;
						m_TriggerDelayTo.Real=LclDblValue;
					}
				}
			}
		}
	}
    
	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "cutoff", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s bandwidth %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s bandwidth %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_Bandwidth.Exists=true;
		m_Bandwidth.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "maxTime", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Max-Time %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Max-Time %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_MaxTime.Exists=true;
		m_MaxTime.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "dutyCycle", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Duty-Cycle %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Duty-Cycle %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_DutyCycle.Exists=true;
		m_DutyCycle.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "riseTime", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s RiseTime %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s RiseTime %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_RiseTime.Exists=true;
		m_RiseTime.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "fallTime", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s FallTime %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s FallTime %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_FallTime.Exists=true;
		m_FallTime.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "pulseWidth", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s FallTime %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s FallTime %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_PulseWidth.Exists=true;
		m_PulseWidth.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "exponent", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s exponent %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s exponent %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_Exponent.Exists=true;
		m_Exponent.Real=LclDblValue;
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "samplingInterval", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sample-Spacing %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Sample-Spacing %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_SampleSpacing.Exists=true;
		m_SampleSpacing.Real=LclDblValue;
	}

	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "points", &LclCharValuePtr)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s points [%s]", LclElement, &LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s points [%s]", LclElement, &LclCharValuePtr,(char*)NULL));
		m_StimSize.Exists=true;
		string_to_array(LclCharValuePtr, &m_StimData[0], &m_StimSize.Int);
	}

	if((atxmlw_Get1641DoubleAttribute(m_SignalDescription, Name, "resistance", &LclDblValue, LclUnit)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Test-Equip-Imp %E [%s]", LclElement, LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Test-Equip-Imp %E  [%s]", LclElement, LclDblValue,LclUnit,(char*)NULL));
		m_TestEquipImp.Exists=true;
		m_TestEquipImp.Real=LclDblValue;
	}

	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "syncOut", &LclCharValuePtr)))
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s syncOut [%s]", LclElement, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s syncOut [%s]", LclElement, LclCharValuePtr,(char*)NULL));
		if(strcmp(LclCharValuePtr, "on")==0)
		{
			m_Sync.Exists=true;
		}

		else
		{
			m_Sync.Exists=false;
		}
	}

	//call function recursivly if needed
	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "In", &LclCharValuePtr)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s   [%s]", Name, LclCharValuePtr,(char*)NULL));

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s Element [%s]", LclCharValuePtr, LclElement,(char*)NULL));
		if(strcmp(LclElement, "Sum") == 0) //Combo of Signals
		{
			m_DCOffset.Exists=true; //assume DC offset is being specified
			m_DCOffset.Real=0;
			
			AC_Component[0]='\0';
			DC_Component[0]='\0';
			sscanf(LclCharValuePtr,"%s %s", AC_Component, DC_Component);

			Get_Variables(AC_Component, Response, BufferSize);
			Get_Variables(DC_Component, Response, BufferSize);
		}
		else
		{
			Get_Variables(LclCharValuePtr, Response, BufferSize);
		}
	}

	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "Sync", &LclCharValuePtr)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In [%s]", Name, LclCharValuePtr,(char*)NULL));
		trigflag=true;
		Get_Variables(LclCharValuePtr, Response, BufferSize);
		trigflag=false;
	}

	if((atxmlw_Get1641StringAttribute(m_SignalDescription, Name, "Gate", &LclCharValuePtr)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", Name, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "Get_Variables()", "Found %s In [%s]", Name, LclCharValuePtr,(char*)NULL));
		trigflag=true;
		Get_Variables(LclCharValuePtr, Response, BufferSize);
		trigflag=false;
	}

	ISDODEBUG(dodebug(0, "GetVariables()", "Leaving function", (char*)NULL));
	return;
}

void CRi3152_T::SanitizeRi3152 (void)
{
	IFNSIM(ri3151_set_frequency(m_Handle, 100e-6));
	IFNSIM(ri3151_set_amplitude(m_Handle, 10e-3));
	IFNSIM(ri3151_output(m_Handle, false));
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: string_to_array()
//
// Purpose: Converts a Null terminated string containing integer values
//           separated by ", " to an integer array
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// input            char *          Null terminated string in 1641 format
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int **          integer array to be filled
// size             int *           size of integer array filled
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void string_to_array(char input [], double array [], int * size)
{
	char string[2048] = "";
	char *temp;
	int i = 0;
	strcpy(string, input);

	temp = strtok(string, ",");
	do
	{
		array[i] = 0;
		array[i] = atof(temp);
		i++;
		temp=strtok(NULL, ",");
		if(temp)
		{
			temp++;
		}

	}while(temp != NULL);

	*size=i;
	return;
}

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
    if((atxmlw_ParseDriverFunction(DeviceHandle, DriverFunction, &DfHandle, Response, BufferSize)) || (DfHandle == NULL))
	{
        return;
	}

    atxmlw_GetDFName(DfHandle,Name);
    RetVal.Int32 = 0;
    ///////// Implement Supported Driver Function Calls ///////////////////////
	// For now only the standard Vi calls
    if((strncmp("vi",Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal))
	{
        int x = 0;
	}

    else
    {
        atxmlw_ErrorResponse("", Response, BufferSize, "IssueDriverFunction ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, atxmlw_FmtMsg(" Invalid/Unimplemented Function [%s]",Name));
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
		sprintf(TmpBuf, "%s", DEBUGIT_FG);
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
