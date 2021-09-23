//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    LE1553_T.cpp
//
// Date:	    1JAN06
//
// Purpose:	    ATXMLW Instrument Driver for LE1553
//
// Instrument:	LE1553  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  1JAN06   Baseline								  D. Bubenik, EADS North America Defense
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <time.h>
//#include <winioctl.h>
#include "visa.h"
#include "AtxmlWrapper.h"
//#include "stdemace.h"
#include "BTI1553.H"
#include "BTICARD.H"
#include "atxmlLE1553_5_T.h"

// Local Defines
#define MASTER			1
#define SLAVE			2
#define MONITOR			3

#define CON_RT			4
#define RT_CON			5
#define CON_MODE		6
#define RT_RT			7
#define ALL_LISTENER	8

#define DATA            9
#define STATUS         10
#define COMMAND        11
#define CONFIG         12

#define ACE_BCCTRL_BCST 0x01

// Function codes
#define _DECL	WINAPI

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024
#define DBLK           (m_NumExch +1)
#define MSG            (m_NumExch +1)

#define S16BIT short
#define U32BIT unsigned

#define RESPTIME_18US	185		// CJW was 0.0000185 must be INT
#define RESPTIME_22US	220		// CJW was 0.0000225 must be INT
#define RESPTIME_50US	505		// CJW was 0.0000505 must be INT
#define RESPTIME_130US	1300	// CJW was 0.0001300 must be INT

int TERMADDR = 1;
int SUBADDR = 2;
XMITFIELDS1553 sendMsg;				/*Message structure*/
XMITFIELDS1553 sMsgField;
ULONG StatusOfLastDataWrite = 0;


// Static Variables
static bool BCmsgDone;
int	DE_BUG = 0;
FILE *debugfp = 0;

// Local Function Prototypes

void bin_string_to_array(char input [], unsigned short array [], int * size);
void array_to_bin_string(unsigned short array [], int size, char output []);
void string_to_array(char input [], int array [], int * size);
void array_to_string(int array [], int size, char output []);
void _DECL BCmsgComplete( S16BIT DevNum, U32BIT Status);  //interupt handler

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CAtxmlLE1553_5_T
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
CAtxmlLE1553_5_T::CAtxmlLE1553_5_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, 
								   ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
	char LclMsg[1024];
	ERRVAL Status = 0;
	int corenum = 0;

	// Save Device Info
	m_InstNo = Instno;
	m_ResourceType = ResourceType;
	memset(m_ResourceName, '\0', sizeof(m_ResourceName));

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

		ISDODEBUG(dodebug(0, "CAtxmlLE1553_5_T()", "m_AddressInfo.PrimaryAddress = %d", m_AddressInfo.PrimaryAddress, (char*)0));
		ISDODEBUG(dodebug(0, "CAtxmlLE1553_5_T()", "m_AddressInfo.ControllerNumber = %d", m_AddressInfo.ControllerNumber, (char*)0));
		ISDODEBUG(dodebug(0, "CAtxmlLE1553_5_T()", "m_AddressInfo.SecondaryAddress = %d", m_AddressInfo.SecondaryAddress, (char*)0));
		ISDODEBUG(dodebug(0, "CAtxmlLE1553_5_T()", "m_AddressInfo.SubModuleAddress = %d", m_AddressInfo.SubModuleAddress, (char*)0));
	}

	m_HCore = NULL;
	memset(m_InitString, '\0', sizeof(m_InitString));
	HCORE hcore = 0;
	m_InstNo = 0;	//Only 1 card

	IFNSIM(Status = BTICard_CardOpen(&m_HandleCard, m_InstNo));

	corenum = 0;
	//for (corenum = 0; corenum < 4; corenum++)
	{
		IFNSIM(Status = BTICard_CoreOpen(&m_HCore, corenum, (HCARD)m_HandleCard));
		IFNSIM(BTICard_CardReset(m_HCore));

		int channum = 0;
		{
			IFNSIM(Status = BTI1553_ChIs1553(channum, (HCORE)m_HCore));
			IFNSIM(Status = BTI1553_ChGetInfo(INFO1553_MULTIMODE, channum, (HCORE)m_HCore));
		}
	}

	InitPrivateLE1553();
	NullCalDataLE1553();

	// The Form Init String
	sprintf(m_InitString,"%d", m_AddressInfo.PrimaryAddress);
	m_ResourceAddress = m_AddressInfo.PrimaryAddress;
	sprintf(LclMsg,"Wrap-CLE1553 Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
	ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: ~CAtxmlLE1553_5_T()
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
CAtxmlLE1553_5_T::~CAtxmlLE1553_5_T()
{
	char Dummy[1024];
	
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CLE1553 Class Distructor called "),Dummy,1024);
	ISDODEBUG(dodebug(0, "~CAtxmlLE1553_5_T()", "%s", "Class Distructor called", (char*)0));
	IFNSIM(BTICard_CardClose((HCARD)m_HandleCard));
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusLE1553
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
int CAtxmlLE1553_5_T::StatusLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";
	BOOL running = false; 

    // Status action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusLE1553 called "), Response, BufferSize);
    // Check for any pending error messages
	IFNSIM(running = BTICard_CardIsRunning((HCARD)m_HCore));
	if (running) 
	{
		Status = 1; //Card is executing;
	}

	if(Status <0)
	{
		Status = ErrorLE1553(Status, Response, BufferSize);
	}

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalLE1553
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
int CAtxmlLE1553_5_T::IssueSignalLE1553(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

	memset(Response, '\0', BufferSize);
    // IEEE 1641 Issue Signal action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalLE1553 Signal: "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "IssueSignalLE1553()", "IssueSignalLE1553 Signal: %s", SignalDescription, (char*)0));

	if ((Status = GetStmtInfoLE1553(SignalDescription, Response, BufferSize)) == 0) {
		Status = CallSignalLE1553(m_Action, Response, BufferSize);
	} 

	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: CallSignalDdc65569i1
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
int CAtxmlLE1553_5_T::CallSignalLE1553(int action, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status = 0;

	ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: %d", action, (char*)0));   

	switch (m_Action)
	{
		case ATXMLW_SA_APPLY:
			if ((Status = CallSignalLE1553(ATXMLW_SA_SETUP, Response, BufferSize)) != 0)
			{
				break;
			}

			Status = CallSignalLE1553(ATXMLW_SA_ENABLE,Response, BufferSize);
			break;

		case ATXMLW_SA_REMOVE:
			if ((Status = CallSignalLE1553(ATXMLW_SA_DISABLE,Response, BufferSize)) != 0)
			{
				break;
			}
			
			Status = CallSignalLE1553(ATXMLW_SA_RESET, Response, BufferSize);
			break;

		case ATXMLW_SA_READ:
			if ((Status = CallSignalLE1553(ATXMLW_SA_ENABLE, Response, BufferSize)) != 0)
			{
				break;
			}
			
			Status = CallSignalLE1553(ATXMLW_SA_FETCH, Response, BufferSize);
			break;

		case ATXMLW_SA_MEASURE:
			break;		
		
		case ATXMLW_SA_SETUP:
			ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: ATXMLW_SA_MEASURE or ATXMLW_SA_SETUP", (char*)0));
			Status = SetupLE1553(Response, BufferSize);
			break;

		case ATXMLW_SA_ENABLE:
		 	ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: ATXMLW_SA_ENABLE", (char*)0));
			Status = EnableLE1553(Response, BufferSize);
			break;

		case ATXMLW_SA_DISABLE:
		 	ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: ATXMLW_SA_DISABLE", (char*)0));
			Status = DisableLE1553(Response, BufferSize);
			break;

		case ATXMLW_SA_FETCH:
		 	ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: ATXMLW_SA_FETCH", (char*)0));
			Status = FetchLE1553(Response, BufferSize);
			break;
			
		case ATXMLW_SA_CONNECT:
			break;

		case ATXMLW_SA_DISCONNECT:
			break;
			
		case ATXMLW_SA_RESET:
		 	ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: ATXMLW_SA_RESET", (char*)0));
			Status = ResetLE1553(Response, BufferSize);
			break;

		case ATXMLW_SA_STATUS:
		 	ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 action: ATXMLW_SA_STATUS", (char*)0));
			Status = StatusLE1553(Response, BufferSize);
			break;

		default:
			ATXMLW_ERROR(-1, "CallSignalLE1553", "Invalid Action passed to function", Response, BufferSize);
			ISDODEBUG(dodebug(0, "CallSignalLE1553()", "CallSignalLE1553 called with %d Error", action, (char*)0));
			Status = -1;
			break;
	}

	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalLE1553
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
int CAtxmlLE1553_5_T::RegCalLE1553(ATXMLW_INTF_CALDATA* CalData)
{
    char      Dummy[1024];

    // Setup action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalLE1553 CalData: %s", CalData),Dummy,1024);
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ResetLE1553
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
int CAtxmlLE1553_5_T::ResetLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetLE1553 called "), Response, BufferSize);
    // Reset the LE1553
	m_initialized = false;

	IFNSIM(BTICard_CardReset((HCORE)m_HCore));
	if(Status)
	{
        ErrorLE1553(Status, Response, BufferSize);
	}

    InitPrivateLE1553();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstLE1553
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
int CAtxmlLE1553_5_T::IstLE1553(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	unsigned long statusFlags = 0x00;

    // Reset action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstLE1553 called Level %d", Level), Response, BufferSize);
    // Reset the LE1553
    //////// Place LE1553 specific data here //////////////
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			IFNSIM(Status = BTICard_CardTest( TEST_LEVEL_3 , m_HCore ));
			IFNSIM(statusFlags = BTICard_BITStatusRd((HCARD)m_HCore));//for BIT
			break;
		case ATXMLW_IST_LVL_IST:
			 IFNSIM(BTICard_CardReset( m_HandleCard ));
			 IFNSIM(Status = BTICard_CardTest( TEST_LEVEL_3 , m_HCore ));
			 IFNSIM(statusFlags = BTICard_BITStatusRd((HCARD)m_HandleCard));//for BIT
			break;
		case ATXMLW_IST_LVL_CNF:
			//Status = StatusLE1553(Response,BufferSize);
 			IFNSIM(Status = BTICard_CardTest( TEST_LEVEL_3 , m_HCore ));
			IFNSIM(statusFlags = BTICard_BITStatusRd((HCARD)m_HCore));//for BIT
		   break;
		default: // Hopefully BIT 1-9
			break;
    }
    //////////////////////////////////////////////////////////
    if(statusFlags)
	{
        ErrorLE1553(statusFlags, Response, BufferSize);
		sprintf(Response, "<AtXmlResponse>\n <ReturnData>\n"
			 "<AtxmlResource>%s</AtxmlResource>\n"
			 "<Value>\n<c:Datum xsi:type=\"c:String\" unit=\"Null\" value=\"status error see return info\"/>\n</Value>\n"
			 "</ReturnData>\n </AtXmlResponse>\n",m_ResourceName);
	}

	else
	{
		sprintf(Response, "<AtXmlResponse>\n <ReturnData>\n"
			 "<AtxmlResource>%s</AtxmlResource>\n"
			 "<Value>\n<c:Datum xsi:type=\"c:String\" unit=\"Null\" value=\"status clean see return info\"/>\n</Value>\n"
			 "</ReturnData>\n </AtXmlResponse>\n",m_ResourceName);
	}

	IFNSIM(BTICard_CardReset((HCORE)m_HCore));
    InitPrivateLE1553();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsLE1553
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
int CAtxmlLE1553_5_T::IssueNativeCmdsLE1553(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    // Setup action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsLE1553 "), Response, BufferSize);

	Status = atxmlw_InstrumentCommands((int)m_HCore, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim);
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallLE1553
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
int CAtxmlLE1553_5_T::IssueDriverFunctionCallLE1553(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallLE1553", Response, BufferSize);
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupLE1553
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
int CAtxmlLE1553_5_T::SetupLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
		int rtAddr = 0;
		int channel = CH0;

    // Setup action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupLE1553 called "), Response, BufferSize);	
	ISDODEBUG(dodebug(0, "SetupLE1553()", "%s", "SetupLE1553 called", (char*)0));

	switch(m_TestEquipRole.Dim)
	{
		case MASTER:
			ISDODEBUG(dodebug(0, "SetupLE1553()", "m_TestEquipRole.Dim MASTER %d", m_TestEquipRole.Dim, (char*)0));
			Setup_BC(Response, BufferSize);
			break;
		case SLAVE:
			ISDODEBUG(dodebug(0, "SetupLE1553()", "m_TestEquipRole.Dim SLAVE %d", m_TestEquipRole.Dim, (char*)0));
			Setup_RT(Response, BufferSize);
			break;
		default:
			ISDODEBUG(dodebug(0, "SetupLE1553()", "m_TestEquipRole.Dim MONITOR %d", m_TestEquipRole.Dim, (char*)0));
			Setup_BM(Response, BufferSize);
			break;
	}

	switch((int)(m_ResponseTime.Real*1e6))
	{
		case 18:
			IFNSIM(Status = BTI1553_RTResponseTimeSet(RESPTIME_18US, rtAddr, channel, (HCORE)m_HCore));
			break;
		case 50:
			IFNSIM(Status = BTI1553_RTResponseTimeSet(RESPTIME_50US, rtAddr, channel, (HCORE)m_HCore));
			break;
		case 130:
			IFNSIM(Status = BTI1553_RTResponseTimeSet(RESPTIME_130US, rtAddr, channel, (HCORE)m_HCore));
			break;
		default:
			IFNSIM(Status = BTI1553_RTResponseTimeSet(RESPTIME_22US, rtAddr, channel, (HCORE)m_HCore));
			break;
	}
    if(Status)
	{
        ErrorLE1553(Status, Response, BufferSize);
	}

    return(0);
}
///////////////////////////////////////////////////////////////////////////////
// Function: Setup_BC
//
// Purpose: Setup up the device to work as a bus controller
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
void CAtxmlLE1553_5_T::Setup_BC(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status = 0;
	unsigned int  ctrlWord = 0;
	unsigned int	msgGap = 0, 
			rtSubaddrMode = 0, 
			modeCode = 0, 
			rtAddr = 0,
			dataCntModeCode	= 0,
			pStkAddr = 0,
			pMemBlkAddr = 0;
	short aOpCodes[1];
	int channel = CH0;
	unsigned short entriescount = 512;

	aOpCodes[0]=MSG;
	if(!m_controlFlags.Exists)
	{
		m_controlFlags.UnLong  = BCCFG1553_DEFAULT;
	}

	else
	{
		m_controlFlags.UnLong  = RTCFG1553_DEFAULT;
	}

	// Setup action for the LE1553
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-Setup_BC called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "Setup_BC()", "%s", "Setup_BC called", (char*)0));


	if(!m_MessageGap.Exists) 
	{
		m_MessageGap.Real=0;
	}

	if(!m_Delay.Exists)	
	{
		m_Delay.Real=0;
	}

	m_MessageGap.Real+=m_Delay.Real;  //this is how delay was implemented in the Legacy system
	ISDODEBUG(dodebug(0, "Setup_BC()", "m_MessageGap = %g", m_MessageGap.Real, (char*)0));

	if(!m_initialized)
	{
		//initialize card
		IFNSIM(Status = BTI1553_BCConfigEx((ULONG)m_controlFlags.UnLong, entriescount, channel,(HCORE)m_HCore));
		ISDODEBUG(dodebug(0, "Setup_BC()", "%s", "aceInitialize(m_ResourceAddress, ACE_ACCESS_CARD, ACE_MODE_BC, 0, 0, 0)", (char*)0));
		if (Status)	
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "Setup_BC()", "Status = %d\nResponse %s", Status, Response, (char*)0));

		}

		m_initialized = true;
	}

	/* Create control word and calculate the message gap */
	rtAddr = (m_Command[0].Int & 0xF800) >> 11;	
	ISDODEBUG(dodebug(0, "Setup_BC()", "rtAddr = [%x]", rtAddr, (char*)0));

	rtSubaddrMode = m_Command[0].Int & 0x03E0;
	ISDODEBUG(dodebug(0, "Setup_BC()", "rtSubaddrMode = [%x]", rtSubaddrMode, (char*)0));

	dataCntModeCode = m_Command[0].Int & 0x001F;
	dataCntModeCode = (dataCntModeCode == 0 ? 32 : dataCntModeCode);
	ISDODEBUG(dodebug(0, "Setup_BC()", "dataCntModeCode = [%x]", dataCntModeCode, (char*)0));


	if(!m_Delay.Exists)
	{
		m_Delay.Real = 0;
	}

	/* Not broadcast mode */
	if ((rtAddr != 31) && (m_BusMode.Dim != CON_MODE)) 
	{
		ISDODEBUG(dodebug(0, "Setup_BC()", "Not broadcast mode", (char*)0));

		/* Mode Code */
		if ((rtSubaddrMode == 0x0370) || (rtSubaddrMode == 0x0000)) 
		{
			ISDODEBUG(dodebug(0, "Setup_BC()", "Not broadcast mode - Mode Code", (char*)0));

			if (dataCntModeCode >= 0x0010) 
			{
				ctrlWord = BCCFG1553_ENABLE;// BCCFG1553_ENABLE, BCCFG1553_MC0  /* set mode code bit in control word */
			}
			if (dataCntModeCode >= 0x0010) 
			{
				m_Delay.Real += .000072;    // BCCFG1553_MC01/* Mode code with data */
				ISDODEBUG(dodebug(0, "Setup_BC()", "Not broadcast mode - Mode Code - Mode code with data", (char*)0));
			}
			else
			{
				m_Delay.Real += .000052;    /* Mode code w/out data */    
				ISDODEBUG(dodebug(0, "Setup_BC()", "Not broadcast mode - Mode Code - Mode code w/out data", (char*)0));
			}
		}
		else /* Not mode code */
		{  
			ISDODEBUG(dodebug(0, "Setup_BC()", "Not broadcast mode - Not Mode Code", (char*)0));

			ctrlWord = BCCFG1553_MCNONE; // 

			if (m_BusMode.Dim != RT_CON) 
			{
				/* compare command word to actual data count */
				if (m_DataSize.Int == dataCntModeCode)			
				{
					atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, "LE1553: command word does match actual data count");
					ISDODEBUG(dodebug(0, "Setup_BC()", "Error1 LE1553: command word does match actual data count", (char*)0)); 
				}
				else if ((m_DataSize.Int == 0) && (m_Data[0] == 0) && ((dataCntModeCode == 32 ? 0 : dataCntModeCode) != 0))
				{
					atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, "LE1553: command word does not match actual data count");
					ISDODEBUG(dodebug(0, "Setup_BC()", "Error2 LE1553: command word does not match actual data count", (char*)0));
				}

				m_Delay.Real += ((double)(m_DataSize.Int + 2)) * .000020 + .000012;
			}
			else if (m_BusMode.Dim == RT_CON)
			{
				m_Delay.Real += .000052;
			}
		}                
	}	// end not Broadcast mode

	//Broadcast
	else if ((rtAddr == 31) && (m_BusMode.Dim == CON_MODE)) 
	{
		ISDODEBUG(dodebug(0, "Setup_BC()", "Broadcast mode", (char*)0));
		/* Mode Code */
		if ((rtSubaddrMode == 0x0370) || (rtSubaddrMode == 0x0000)) 
		{
			ISDODEBUG(dodebug(0, "Setup_BC()", "Broadcast mode - Mode Code", (char*)0));
			ctrlWord = ACE_BCCTRL_BCST | BCCFG1553_MC0;

			/* Mode Code with Data */
			if (dataCntModeCode >= 0x0010)
			{
				m_Delay.Real += .00004;
				ISDODEBUG(dodebug(0, "Setup_BC()", "Broadcast mode - Mode Code - Mode code with data", (char*)0));
			}
			else 	/* mode code with no data */
			{
				m_Delay.Real += .00002;
				ISDODEBUG(dodebug(0, "Setup_BC()", "Broadcast mode - Mode Code - Mode code w/out data", (char*)0));
			}
		}

		/* Command and Data */
		else 
		{
			ISDODEBUG(dodebug(0, "Setup_BC()", "Not broadcast mode - Command and Data", (char*)0));
			ctrlWord = ACE_BCCTRL_BCST;
			m_Delay.Real += ((dataCntModeCode + 1) * 0.000020);

			/* Validate word count */
			if ((m_DataSize.Int == dataCntModeCode))
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, "LE1553: command word does not match actual data count");
				ISDODEBUG(dodebug(0, "Setup_BC()", "Error4 LE1553: command word does not match actual data count", (char*)0));
			}

			else if ((m_DataSize.Int == 0) && (m_Data[0] == 0) && ((dataCntModeCode == 32 ? 0 : dataCntModeCode) != 0))
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, "LE1553: command word does not match actual data count");
				ISDODEBUG(dodebug(0, "Setup_BC()", "Error5 LE1553: command word does not match actual data count", (char*)0));
			}
		}
	}

	else
	{
		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, "LE1553: Specified RT address not legal with bus mode"); 
		ISDODEBUG(dodebug(0, "Setup_BC()", "%s", "atxmlw_ErrorResponse(m_ResourceAddress, Response, BufferSize, Instrument Error, 0, DDC1553: Specified RT address not legal with bus mode)", 
					(char*)0));
	}

	msgGap += (unsigned short) (m_Delay.Real * 1e6);
	ctrlWord += BCCFG1553_TERMONA;  // set channel A bit

	/* Create BC Message and store handle for later inclusion in frame */
	if(m_BusMode.Dim!=RT_CON)
	{
		IFNSIM(Status = BTI1553_BCConfigMsg(m_controlFlags.UnLong, m_ResourceAddress, (HCORE)m_HCore))
		IFNSIM(Status = BTI1553_BCCreateMsg(m_controlFlags.UnLong, DBLK, (USHORT)ctrlWord , (LPUSHORT)m_Data, (HCORE)m_HCore));
		ISDODEBUG(dodebug(0, "Setup_BC()", "%s", "BTI1553_BCCreateMsg - != RT_CON", (char*)0));
		ISDODEBUG(dodebug(0, "Setup_BC()", "DataWordCnt = %d", m_DataSize.Int, (char*)0));
	}

	else
	{
		IFNSIM(Status = BTI1553_BCCreateMsg(m_controlFlags.UnLong, (int)DBLK, (USHORT)ctrlWord, NULL, (HCORE)m_HCore));
		ISDODEBUG(dodebug(0, "Setup_BC()", "%s", "BTI1553_BCCreateMsg - != RT_CON else", (char*)0));
	}
	if(Status)
	{
		ErrorLE1553(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "Setup_BC()", "ErrorLE1553 ", (char*)0));
	}

	rtSubaddrMode = rtSubaddrMode >> 5;

	switch(m_BusMode.Dim)
	{
		case RT_CON: // rt response
			IFNSIM(Status = BTI1553_RTCreateMsg(m_controlFlags.UnLong,TRUE,rtAddr,TRUE, rtSubaddrMode,channel,(HCORE)m_HCore));
			ISDODEBUG(dodebug(0, "Setup_BC()", "BTI1553_RTCreateMsg RT_CON", (char*)0));
			break;
		case RT_RT: // rt to rt message
			IFNSIM(Status = BTI1553_RTCreateMsg(m_controlFlags.UnLong, TRUE, rtAddr, FALSE, rtSubaddrMode, channel, (HCORE)m_HCore));
			ISDODEBUG(dodebug(0, "Setup_BC()", "BTI1553_RTCreateMsg RT_RT", (char*)0));
			break;
		case CON_MODE: // BC vradcast
			IFNSIM(Status = BTI1553_BCCreateMsg(m_controlFlags.UnLong, DBLK, (USHORT)ctrlWord, (LPUSHORT)m_Data, (HCORE)m_HCore));
			ISDODEBUG(dodebug(0, "Setup_BC()", "BTI1553_RTCreateMsg CON_MODE", (char*)0));
			break;
		default:  //con-rt Console message
			IFNSIM(Status = BTI1553_BCCreateMsg(m_controlFlags.UnLong, DBLK, (USHORT)ctrlWord, (LPUSHORT)m_Data, (HCORE)m_HCore));
			ISDODEBUG(dodebug(0, "Setup_BC()", "BTI1553_RTCreateMsg default", (char*)0));
			break;
	}

	if(Status) 
	{
		ErrorLE1553(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "Setup_BC()", "ErrorLE1553 BTI1553_BCCreateMsg Response(%s)\n Buffer size = %d", Response, BufferSize, (char*)0));
	}

	/* Create XEQ opcode that will use msg block */
	/* Create Minor Frame */
	IFNSIM(Status = BTI1553_BCSchedFrame(1000, CH0, (HCORE)m_HCore));
	ISDODEBUG(dodebug(0, "Setup_BC()", "BTI1553_BCSchedFrame", (char*)0));
	ISDODEBUG(dodebug(0, "Setup_BC()", "m_MajorFrameTime = %d\nm_Delay.Real = %d\nm_MessageGap.Real = %d", m_MajorFrameTime, m_Delay.Real, m_MessageGap.Real, (char*)0));
	m_MajorFrameTime+=m_Delay.Real+m_MessageGap.Real; //add minor frame time to major frame time
	ISDODEBUG(dodebug(0, "Setup_BC()", "m_MajorFrameTime += m_Delay.Real + m_MessageGap.Real = %d", m_MajorFrameTime, (char*)0));

	if(Status)
	{
		ErrorLE1553(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "Setup_BC()", "ErrorLE1553 BTI1553_BCSchedFrame Response(%s)\n Buffer size = %d", Response, BufferSize, (char*)0));
	}

	/* Store the exchange number */
	if (m_tExnm.Exists)
	{
		m_Exnm[m_NumExch] = m_tExnm.Int;
	}
	else
	{
		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, "LE1553: No exchange number in Setup BC");
		ISDODEBUG(dodebug(0, "Setup_BC()", "atxmlw_ErrorResponse BTI1553_BCSchedFrame Response(%s)\n Buffer size = %d %s",
						  Response, BufferSize, "Instrument Error 0, LE1553: No exchange number in Setup BC", (char*)0));
	}

	m_NumExch++; //

	m_Command[0].Int = 0;			// set command to 0
	m_Command[1].Int = 0;

	for (int i = 0; i < 32; ++i)
	{
		m_Data[i] = 0;			// set all data values to 0
	}

	m_BusMode.Exists = false;				// reset BUS-MODE
	m_tExnm.Exists = false;				// reset exchange number
	m_DataSize.Exists = false;			// reset exchange data count
	m_Delay.Exists =false;					// reset delay value
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_RT
//
// Purpose: Setup up the device to work as a remote terminal
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
void CAtxmlLE1553_5_T::Setup_RT(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
	int *Addresses=NULL;
	static unsigned short buRtAddr = 0;
	int rtSubAddrCnt=0;

    // Setup action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-Setup_RT called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "Setup_RT()", "%s", "Setup_RT called", (char*)0));

	//figure out which addresses to use
	switch(m_BusMode.Dim)
	{
		case CON_RT:
		case CON_MODE:
			if(!m_RecieveAddressSize.Exists)
			{
				m_RecieveAddress[0] = (m_Status.Int>>11)&0x001F;
				m_RecieveAddress[1] = 0;
				m_RecieveAddressSize.Int = 1;
				ISDODEBUG(dodebug(0, "Setup_RT()", "m_RecieveAddress = [%x]", m_RecieveAddress[0], (char*)0));
			}

			Addresses = m_RecieveAddress;
			rtSubAddrCnt = m_RecieveAddressSize.Int-1;
			break;
		default:
			if(!m_SendAddressSize.Exists)
			{
				m_SendAddress[0] = (m_Status.Int>>11)&0x001F;
				m_SendAddress[1] = 0;
				m_SendAddressSize.Int = 1;
				ISDODEBUG(dodebug(0, "Setup_RT()", "m_SendAddress = [%x]", m_SendAddress[0], (char*)0));
			}

			Addresses = m_SendAddress;
			rtSubAddrCnt = m_SendAddressSize.Int-1;
			break;
	}
	m_SA[m_NumExch]=Addresses[1]; //save SA for fetch

    /* set Rt Addr for first exchange */
	if (m_NumExch == 0)	
	{
		if (Addresses[0] != 0) 		/*address from test-equip-talker/listener */
		{
			buRtAddr = (unsigned short)m_RecieveAddress[0];
		}

		IFNSIM(Status = BTI1553_RTConfig((ULONG)m_controlFlags.UnLong,buRtAddr,CH0,(HCORE)m_HCore));
		ISDODEBUG(dodebug(0, "Setup_RT()", "BTI1553_RTConfig", (char*)0));

		if(Status)
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
		}

		IFNSIM(Status = BTI1553_RTCreateList(LISTCRT1553_DEFAULT, 512, MSGCRT1553_DEFAULT, true, m_RecieveAddress[0],true, Addresses[0], CH0, (HCORE)m_HCore));
		ISDODEBUG(dodebug(0, "Setup_RT()", "BTI1553_RTCreateList", (char*)0));

		if (Status)
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
		}

		if (m_Status.Exists) /* address from status word */
		{ 
			if (buRtAddr == 0)
			{
				buRtAddr = (m_Status.Int & 0xF800) >> 11;
			}

			else if ((((m_Status.Int & 0xF800) >> 11) != buRtAddr) && (m_BusMode.Dim != CON_MODE))
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "LE1553:STATUS word does not match RT address");
				ISDODEBUG(dodebug(0, "Setup_RT()", "LE1553:STATUS word does not match RT address", (char*)0));
			}
		}
				
		if (buRtAddr == 0)
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "LE1553:No RT address specified");
		}
 
		if ((buRtAddr == 0x0000) || (buRtAddr == 0x001F))
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "LE1553: RT address cannot be 0 or 31"); 
			ISDODEBUG(dodebug(0, "Setup_RT()", "LE1553:RT address cannot be 0 or 31", (char*)0));
		}
	}

	else  /* validate address for second or subsequent exchange */
	{
		if (((Addresses[0] != 0) && (Addresses[0] != buRtAddr)) && (m_BusMode.Dim != CON_MODE))
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "LE1553:RT address does not match first exchange");
			ISDODEBUG(dodebug(0, "Setup_RT()", "LE1553:RT address does not match first exchange", (char*)0));
		}

		if (m_Status.Int != 0)  /* address from status word */
		{
			/* does status word match rt addr from test-equip-talker/listener */
			if ((Addresses[0] != 0) && (((m_Status.Int & 0xF800) >> 11) != Addresses[0])) 
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "BU65550:status word not match RT address");
				ISDODEBUG(dodebug(0, "Setup_RT()", "Error8 LE1553:STATUS word does not match RT address", (char*)0));
			}

			if ((Addresses[0] == 0) && (((m_Status.Int & 0xF800) >> 11) != buRtAddr)) 
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "LE1553:status word does not match RT address");
				ISDODEBUG(dodebug(0, "Setup_RT()", "LE1553:status word does not match RT address", (char*)0));
			}
		}

		if ((Addresses[0] == 0) && (((m_Status.Int & 0xF800) >> 11) == 0))
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, "LE1553: no rt address specified for exchange");
			ISDODEBUG(dodebug(0, "Setup_RT()", "LE1553: no rt address specified for exchange", (char*)0));
		}
	}

	/* Illegalize all subaddresses if RT subaddresses are specified */

	if ((rtSubAddrCnt != 0) && (m_NumExch == 0))
	{
		//IFNSIM(Status=aceRTMsgLegalityDisable(m_ResourceAddress, 
		//	ACE_RT_MODIFY_ALL, 
		//	ACE_RT_MODIFY_ALL, 
		//	ACE_RT_MODIFY_ALL, 
		//	0xFFFFFFFF));
		if(Status)
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
		}
	}

	/* Store the exchange number */
	if (m_tExnm.Exists) 
	{
		m_Exnm[m_NumExch] = m_tExnm.Int;
	}

	else
	{
		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",Status, "LE1553: No Exchange Number");
		ISDODEBUG(dodebug(0, "Setup_RT()", "LE1553: No Exchange Number", (char*)0));
	}

	switch (m_BusMode.Dim) 
	{
		case CON_RT :
			if (rtSubAddrCnt != 0) 
			{
				for (int i = 0; i < rtSubAddrCnt; i++) 
				{
					//IFNSIM(Status=aceRTMsgLegalityEnable(m_ResourceAddress,	ACE_RT_MODIFY_ALL, 0,Addresses[(i+1)], 0xFFFFFFFF));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}

					// Allocate a memory block for the subaddress 
					//IFNSIM(Status=aceRTDataBlkCreate(m_ResourceAddress,
					//	Addresses[(i+1)]+1,  //use SA +1 for data buf num
					//	32,
					//	NULL,
					//	0));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}

					if(i < rtSubAddrCnt -1)
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	Addresses[(i+1)]+1,  //data buf num
						//	Addresses[(i+1)],
						//	ACE_RT_MSGTYPE_RX,
						//	0,
						//	TRUE));
					}
					else //set up interupt on last transfer
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	Addresses[(i+1)]+1,  //data buf num
						//	Addresses[(i+1)],
						//	ACE_RT_MSGTYPE_RX,
						//	ACE_RT_DBLK_EOM_IRQ,
						//	TRUE));
					}
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				}
			}
			else 
			{
				//Enable all SAs
				//aceRTMsgLegalityEnable(m_ResourceAddress,	ACE_RT_MODIFY_ALL, 0,	ACE_RT_MODIFY_ALL, 	0xFFFFFFFF);

				/* Allocate all subaddresses to receive if no subaddress is specified */
				/* Allocate memory for all subaddresses */
				for (int i = 0;i < 32;i++) 
				{
					//IFNSIM(Status=aceRTDataBlkCreate(m_ResourceAddress, i+1, 32,NULL,	0));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);				
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}

					if(i<31)
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,i+1,i,ACE_RT_MSGTYPE_RX,0,TRUE));
					}
					else //set up interupt on last transfer
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,i+1,i,ACE_RT_MSGTYPE_RX,ACE_RT_DBLK_EOM_IRQ,TRUE));
					}
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				}
			}
			break;
		case RT_CON:
			if (rtSubAddrCnt != 0) 
			{
				for (int i = 0; i < rtSubAddrCnt;i++) 
				{
					//IFNSIM(Status=aceRTMsgLegalityEnable(m_ResourceAddress,
					//	ACE_RT_MODIFY_ALL, 
					//	1,				//Send
					//	Addresses[(i+1)], 
					//	0xFFFFFFFF));
					//if(Status)
					//	ErrorLE1553(Status, Response, BufferSize);

					/* Allocate a memory block for the subaddress */
					//IFNSIM(Status=aceRTDataBlkCreate(m_ResourceAddress,
					//	Addresses[(i+1)]+1,  //use SA +1 for data buf num
					//	m_DataSize.Int,
					//	m_Data,
					//	m_DataSize.Int));
					//if(Status)
					//	ErrorLE1553(Status, Response, BufferSize);

					if(i<rtSubAddrCnt-1)
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	Addresses[(i+1)]+1,  //data buf num
						//	Addresses[(i+1)],
						//	ACE_RT_MSGTYPE_TX,
						//	0,
						//	TRUE));
					}
					else //set up interupt on last transfer
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	Addresses[(i+1)]+1,  //data buf num
						//	Addresses[(i+1)],
						//	ACE_RT_MSGTYPE_TX,
						//	ACE_RT_DBLK_EOM_IRQ,
						//	TRUE));
					}
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				}
			}
			else 
			{
				//Enable all addresses
				//IFNSIM(Status=aceRTMsgLegalityEnable(m_ResourceAddress,
				//		ACE_RT_MODIFY_ALL, 
				//		1,				//Send
				//		ACE_RT_MODIFY_ALL, 
				//		0xFFFFFFFF));
				if(Status)
				{
					ErrorLE1553(Status, Response, BufferSize);						
					ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
				}
				/* Allocate all subaddresses to receive if no subaddress is specified */
				/* Allocate memory for all subaddresses */
				for (int i = 0;i < 32;i++) 
				{
					//IFNSIM(Status=aceRTDataBlkCreate(m_ResourceAddress,
					//	i+1,  //use SA +1 for data buf num
					//	m_DataSize.Int,
					//	m_Data,
					//	m_DataSize.Int));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}

					if(i<31)
					{
					//	IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,i+1, i,ACE_RT_MSGTYPE_TX,0,TRUE));
					}
					else //set up interupt on last transfer
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	(i+1),
						//	i,
						//	ACE_RT_MSGTYPE_TX,
						//	ACE_RT_DBLK_EOM_IRQ,
						//	TRUE));
					}
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				}
			}
			break;
		case CON_MODE :
			if (rtSubAddrCnt != 0) 
			{
				for (int i = 0; i < rtSubAddrCnt; i++) 
				{
					//IFNSIM(Status=aceRTMsgLegalityEnable(m_ResourceAddress,
					//	ACE_RT_BCST_ADDRSS, 
					//	0,				//Recieve 
					//	Addresses[(i+1)], 
					//	0xFFFFFFFF));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				
					/* Allocate a memory block for the subaddress */
					//IFNSIM(Status=aceRTDataBlkCreate(m_ResourceAddress,
					//	Addresses[(i+1)]+1,  //use SA +1 for data buf num
					//	32,
					//	NULL,
					//	0));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}

					if(i<rtSubAddrCnt-1)
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	Addresses[(i+1)]+1,  //data buf num
						//	Addresses[(i+1)],
						//	ACE_RT_MSGTYPE_BCST,
						//	0,
						//	TRUE));
					}
					else //set up interupt on last transfer
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	Addresses[(i+1)]+1,  //data buf num
						//	Addresses[(i+1)],
						//	ACE_RT_MSGTYPE_BCST,
						//	ACE_RT_DBLK_EOM_IRQ,
						//	TRUE));
					}
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				}
			}
			else 
			{
				//Enable all SAs
				//aceRTMsgLegalityEnable(m_ResourceAddress,
				//		ACE_RT_BCST_ADDRSS, 
				//		0,				//Recieve 
				//		ACE_RT_MODIFY_ALL, 
				//		0xFFFFFFFF);

				/* Allocate all subaddresses to receive if no subaddress is specified */
				/* Allocate memory for all subaddresses */
				for (int i = 0; i < 32; i++)  
				{
					//IFNSIM(Status=aceRTDataBlkCreate(m_ResourceAddress,
					//	i+1,
					//	32,
					//	NULL,
					//	0));
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}

					if(i < 31)
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	i+1,
						//	i,
						//	ACE_RT_MSGTYPE_BCST,
						//	0,
						//	TRUE));
					}
					else //set up interupt on last transfer
					{
						//IFNSIM(Status=aceRTDataBlkMapToSA(m_ResourceAddress,
						//	i+1,
						//	i,
						//	ACE_RT_MSGTYPE_BCST,
						//	ACE_RT_DBLK_EOM_IRQ,
						//	TRUE));
					}
					if(Status)
					{
						ErrorLE1553(Status, Response, BufferSize);
						ISDODEBUG(dodebug(0, "Setup_RT()", "ErrorLE1553 = %d", Status, (char*)0));
					}
				}
			}

			break;   
		case RT_RT :
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",Status, "LE1553: RT to RT Transfer not supported at this time");
			ISDODEBUG(dodebug(0, "Setup_RT()", "DDC1553: RT to RT Transfer not supported at this time", (char*)0));
			break;
		default:
			atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",	Status, "LE1553: illegal or no BUS-MODE assigned"); 
			ISDODEBUG(dodebug(0, "Setup_RT()", "DDC1553: illegal or no BUS-MODE assigned", (char*)0));
			break;
	}

	/* Store the exchange number */
	if (m_tExnm.Exists)
	{
		m_Exnm[m_NumExch] = m_tExnm.Int;
	}

	// reset all exchange specific variables
	++m_NumExch;
	rtSubAddrCnt = 0;

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: Setup_BM
//
// Purpose: Setup up the device to work as a bus monitor
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
void CAtxmlLE1553_5_T::Setup_BM(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the LE1553
	ISDODEBUG(dodebug(0, "Setup_BM()", "%s", "Setup_BM called", (char*)0));
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-Setup_BM called "), Response, BufferSize);

    /* set Rt Addr for first exchange */
	if (m_NumExch == 0)	
	{
		//intialize card
		IFNSIM(Status = BTI1553_MonConfig(MONCFG1553_DEFAULT,CH0,(HCORE)m_HCore));
		ISDODEBUG(dodebug(0, "Setup_BM()", "BTI1553_MonConfig", (char*)0));

		if(Status)
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "Setup_BM()", "ErrorLE1553 = %d", Status, (char*)0));
		}

		//IFNSIM(Status=aceMTConfigure(m_ResourceAddress, ACE_MT_DOUBLESTK, ACE_MT_CMDSTK_256, ACE_MT_DATASTK_1K, 0));
		//if(Status)
		//	ErrorLE1553(Status, Response, BufferSize);
	}
    
	/* Store the exchange number */
	if (m_tExnm.Exists)
	{
		m_Exnm[m_NumExch] = m_tExnm.Int;
	}

	// reset all exchange specific variables
	++m_NumExch;

	if(Status)
	{
        ErrorLE1553(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "Setup_BM()", "ErrorDdc65569i1 = %d", Status, (char*)0));
	}

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchLE1553
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
int CAtxmlLE1553_5_T::FetchLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
	char   results[768];
	char   temp[65];
	char attribute[10];
	char *ErrMsg = "";
	int fetched_exch;
	unsigned short temp_sarray[66];
	unsigned short RTData[32];
	sendMsg.datacount = 0;
	MSGADDR RTMsg = 0;

	for(int i=0; i< 32; i++)
	{
		if(m_Exnm[i]==m_tExnm.Int)
		{
			fetched_exch=i;
		}
	}

	XMITFIELDS1553 BCRecvMsg;
	USHORT seqbuf[2048];
	SEQFINDINFO sfinfo;
	LPSEQRECORD1553 pRec1553;
	ULONG  seqcount;
	ULONG  blkcnt;
	memset( &BCRecvMsg, 0, sizeof(BCRecvMsg));
	BCRecvMsg.ctrlflags = MSGCRT1553_MON;
	BCRecvMsg.cwd1 = BTI1553_ValPackCWD(TERMADDR,1,SUBADDR,m_DataSize.Int); //Receive command
	int ii = 0;
	unsigned long xxx = 0;
	switch (m_TestEquipRole.Dim) 
	{
		case MASTER:
			ISDODEBUG(dodebug(0, "FetchLE1553()", "%s", "FetchLE1553 MASTER", (char*)0));
			switch( m_attribute.Dim)
				{
					case COMMAND:
						ii = 0;
						break;
					case DATA:
						ii = 0;
						BTI1553_BCSchedFrame(100000,0,m_HandleCard);
						Status = BTI1553_MonConfig(MONCFG1553_DEFAULT,0,m_HandleCard);
						Status = BTICard_SeqConfig(SEQCFG_DEFAULT,m_HandleCard);
						BTICard_CardStart(m_HandleCard);
						Status = BTI1553_BCTransmitMsg( &BCRecvMsg, 0, m_HandleCard);
						seqcount = BTICard_SeqBlkRd(seqbuf, sizeof(seqbuf) / sizeof(seqbuf[0]), &blkcnt, m_HandleCard);
						if( seqcount != 0 )
						{
							BTICard_SeqFindInit(seqbuf, seqcount, &sfinfo);
							while (!BTICard_SeqFindNext1553(&pRec1553, &sfinfo))
							{
								sendMsg.datacount = pRec1553->datacount;
								for (int i = 0; i < pRec1553->datacount; i++)
								{
									sendMsg.data[i] = pRec1553->data[i];
								}
							}
						}
						break;
					case STATUS:
						ii = 0;
					default:
						array_to_bin_string( &(sendMsg.swd1), 1, &results[0]);
						strcpy(attribute, "status");
						atxmlw_ScalerStringReturn(attribute, "", results, Response, BufferSize);
						return(0);
						break;
				}
				break;
			case SLAVE:
				ISDODEBUG(dodebug(0, "FetchLE1553()", "%s", "FetchLE1553 SLAVE", (char*)0));
				BTICard_CardStart(m_HandleCard);
				switch( m_attribute.Dim)
					{
						case COMMAND:
							ii = 0;
							if( m_RecieveAddress[0] == 0 )
							{
								IFNSIM(RTMsg = BTI1553_RTGetMsg(SUBADDRESS,m_SendAddress[0],XMT,m_SendAddress[1],0,m_HandleCard));
							}
							else
							{
								IFNSIM(RTMsg = BTI1553_RTGetMsg(SUBADDRESS,m_RecieveAddress[0],XMT,m_RecieveAddress[1],0,m_HandleCard));
							}
							IFNSIM(sendMsg.cwd1 = (USHORT)BTI1553_MsgFieldRd( FIELD1553_CWD1, RTMsg, m_HandleCard));
							if(Status<0)
								ErrorLE1553(Status, Response, BufferSize);
							break;
						case DATA:
							ii = 0;
							IFNSIM(RTMsg = BTI1553_RTGetMsg(SUBADDRESS,m_RecieveAddress[0],RCV,m_RecieveAddress[1],0,m_HandleCard));
							IFNSIM(BTI1553_MsgDataRd(RTData,32,RTMsg,m_HandleCard));	/*Read the data received by the RT*/
							IFNSIM( xxx = BTI1553_MsgFieldRd( FIELD1553_ERROR, RTMsg, m_HandleCard));
							IFNSIM(sendMsg.datacount = (USHORT)BTI1553_MsgFieldRd( FIELD1553_COUNT, RTMsg, m_HandleCard));
							memcpy( sendMsg.data, RTData, sizeof(RTData));
							if(Status<0)
								ErrorLE1553(Status, Response, BufferSize);
							break;
						case STATUS:
							ii = 0;
						default:
							break;
					}
					BTICard_CardStop(m_HandleCard);
				break;
			case MONITOR:
				ISDODEBUG(dodebug(0, "FetchLE1553()", "%s", "FetchLE1553 MONITOR", (char*)0));
//					case DATA:
						ii = 0;
						BTI1553_BCSchedFrame(100000,0,m_HandleCard);
						Status = BTI1553_MonConfig(MONCFG1553_DEFAULT,0,m_HandleCard);
						Status = BTICard_SeqConfig(SEQCFG_DEFAULT,m_HandleCard);
						BTICard_CardStart(m_HandleCard);
						seqcount = 0;
						{
							clock_t	t1 = 0,
							t2 = 0;
							double	timeDiff;
								if (m_Wait.Exists) 
								{
									t1 = clock();
									do 
									{
										seqcount = BTICard_SeqBlkRd(seqbuf, sizeof(seqbuf) / sizeof(seqbuf[0]), &blkcnt, m_HandleCard);
										t2 = clock();
										timeDiff = (difftime(t2,t1) / CLK_TCK);
									} while ((!seqcount) && (timeDiff < m_MaxTime.Real));
								}
						}
						if( seqcount != 0 )
						{
							BTICard_SeqFindInit(seqbuf, seqcount, &sfinfo);
							while (!BTICard_SeqFindNext1553(&pRec1553, &sfinfo))
							{
								sendMsg.datacount = pRec1553->datacount;
								for (int i = 0; i < pRec1553->datacount; i++)
								{
									sendMsg.data[i] = pRec1553->data[i];
								}
								//printf("\n");
							}
						}
						else
						{
							sendMsg.datacount = 0;
							for (int i = 0; i < 32; i++)
							{
								sendMsg.data[i] = 0;
							}
						}
						break;
			default:
				ISDODEBUG(dodebug(0, "FetchLE1553()", "%s", "FetchLE1553 default", (char*)0));
				IFNSIM(RTMsg = BTI1553_RTGetMsg(SUBADDRESS,m_SendAddress[0],RCV,m_SendAddress[1],0,m_HandleCard));
				IFNSIM(BTI1553_MsgDataRd(RTData,32,RTMsg,m_HandleCard));	/*Read the data received by the RT*/
				if(Status < 0)
				{
					ErrorLE1553(Status, Response, BufferSize);
				ISDODEBUG(dodebug(0, "FetchLE1553()", "ErrorLE1553 = %d", Status, (char*)0));
				}
				break;
		}

    // Fetch action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchLE1553 called "), Response, BufferSize);
	ISDODEBUG(dodebug(0, "FetchLE1553()", "FetchLE1553 called", (char*)0));


	if(Response && (BufferSize <= (int)(strlen(Response)+200)))
	{
		Response[0]='\0';
	}

	// Fetch data
	//Send Data	
	//CJW 20170222 m_Data[0]=0;
	if( sendMsg.datacount < 0 )	//CJW patch for debug ONLY
	{
		m_DataSize.Int = 32;
	}
	if( sendMsg.datacount == 0 )
	{
		m_DataSize.Int = 32;
	}
	else
	{
		m_DataSize.Int = sendMsg.datacount;
	}
		
	array_to_bin_string(sendMsg.data, m_DataSize.Int, &results[0]);
	strcpy(attribute, "data");
	ISDODEBUG(dodebug(0, "FetchLE1553()", "The data array is %s", results, (char*)0));	
	atxmlw_ScalerStringReturn(attribute, "", results,
                   Response, BufferSize);
	ISDODEBUG(dodebug(0, "FetchDdc65569i1()", "The data array is %s\nResponse is %s", results, Response, (char*)0));
	//Send Status
	m_Status.Int=0;

	if (sMsgField.ctrlflags /*msgflag*/)
	{
		temp_sarray[0] = sMsgField.swd1;
		m_Status.Int=1;
		ISDODEBUG(dodebug(0, "FetchLE1553()", "temp_sarray[0] is %d", temp_sarray[0], (char*)0));	
		ISDODEBUG(dodebug(0, "FetchLE1553()", "Got Message word 1", (char*)0));	
	}
	if (sendMsg.extflag)
	{
		temp_sarray[1] = sMsgField.swd2;
		m_Status.Int=2;
		ISDODEBUG(dodebug(0, "FetchLE1553()", "temp_sarray[1] is %d", temp_sarray[1], (char*)0));	
		ISDODEBUG(dodebug(0, "FetchLE1553()", "Got Message word 2", (char*)0));	
	}

	array_to_bin_string(temp_sarray, m_Status.Int, &results[0]);
	ISDODEBUG(dodebug(0, "FetchLE1553()", "temp_sarray is %s", temp_sarray, (char*)0));	

	strcpy(attribute, "status");
    atxmlw_ScalerStringReturn(attribute, "", results, Response, BufferSize);

	ISDODEBUG(dodebug(0, "FetchLE1553()", "The data array is %s\nResponse is %s", results, Response, (char*)0));
	//send command
	m_Command[0].Int=0;
	m_Command[1].Exists=false;
		
	if (sendMsg.cwd1)
	{
		m_Command[0].Int = (int)sendMsg.cwd1;
		ISDODEBUG(dodebug(0, "FetchLE1553()", "m_Command[0].Int is %d", m_Command[0].Int, (char*)0));
	}

	array_to_bin_string((unsigned short *)&m_Command[0].Int, 1, &results[0]);
		
	if (sendMsg.cwd2)
	{
		array_to_bin_string(&sendMsg.cwd2, 1, &temp[0]);
		strcat(results, ", ");
		strcat(results, temp);
		ISDODEBUG(dodebug(0, "FetchLE1553()", "results is %s", results, (char*)0));	
	}
	strcpy(attribute, "command");
    atxmlw_ScalerStringReturn(attribute, "", results, Response, BufferSize);
	ISDODEBUG(dodebug(0, "FetchLE1553()", "The data array is %s\nResponse is %s", results, Response, (char*)0));	
	ISDODEBUG(dodebug(0, "FetchLE1553()", "Status is %d", Status, (char*)0));

    if(Status<0)
    {
        MeasValue = FLT_MAX;
        ErrorLE1553(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "FetchLE1553()", "ErrorLE1553 = %d", Status, (char*)0));
    }
    else
    {
        sscanf(ErrMsg,"%E",&MeasValue);
    }

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: DisableLE1553
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
int CAtxmlLE1553_5_T::DisableLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
	ISDODEBUG(dodebug(0, "DisableDdc65569i1()", "%s", "DisableDdc65569i1 called", (char*)0));
    // Open action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenLE1553 called "), Response, BufferSize);

	if(m_ReleaseHandle.Exists)
	{
		//release 1553 handle
		if(m_ReleaseHandle.Int == (int)true)
		{
			IFNSIM(BTICard_CardClose((HCARD)m_HandleCard));
			if(Status)
				ErrorLE1553(Status, Response, BufferSize);

			return(0);
		}
	}


	switch (m_BusMode.Dim) 
	{
	case MASTER:
		//IFNSIM(Status=aceBCStop(m_ResourceAddress));
		//Disable interupt to say when frame is done
		//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, FALSE, ACE_IMR2_BC_UIRQ0, BCmsgComplete));
		break;  
	case SLAVE: 
		//IFNSIM(Status=aceRTStop(m_ResourceAddress));
		//Disable interupt to say when frame is done
		//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, FALSE, ACE_IMR1_RT_SUBADDR_EOM, BCmsgComplete));
		break;
	case MONITOR:
		 Status=0;
	default:  //MONITOR 
		//IFNSIM(Status=aceMTStop(m_ResourceAddress));
		//Disable interupt to say when frame is done
		//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, FALSE, ACE_IMR1_EOM, BCmsgComplete));
		break;
	}
	if(Status)
	{
		ErrorLE1553(Status, Response, BufferSize);
		ISDODEBUG(dodebug(0, "FetchLE1553()", "ErrorLE1553 = %d", Status, (char*)0));
	}

	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: EnableLE1553
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
int CAtxmlLE1553_5_T::EnableLE1553(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
	ISDODEBUG(dodebug(0, "EnableLE1553()", "%s", "EnableLE1553 called", (char*)0));
    // Close action for the LE1553
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableLE1553 called "), Response, BufferSize);
	short    aOpCodes[100];
	clock_t				t1 = 0,
						t2 = 0;
	double				timeDiff;
	int                 i;

	if(m_ReleaseHandle.Exists)
	{
		//re-establish 1553 handle
		if(m_ReleaseHandle.Int == false)
		{
			//re-establish CArd and Core handles
			 IFNSIM(Status = BTICard_CardOpen(&m_HandleCard, m_InstNo));
			 {
				IFNSIM(Status = BTICard_CoreOpen(&m_HCore, 0, (HCARD)m_HandleCard));
				IFNSIM(BTICard_CardReset(m_HCore));
				int channum = 0;
				{
				 IFNSIM(Status = BTI1553_ChIs1553(channum, (HCORE)m_HCore));
				 IFNSIM(Status = BTI1553_ChGetInfo(INFO1553_MULTIMODE, channum, (HCORE)m_HCore));
				}
			 }
			return(0);
		}
	}	


	for(i=0; i<100; i++)
	{
		aOpCodes[i]=i+1;
	}

	MSGADDR RTMsg = 0;
	ULONG xxx = 0;
	switch (m_TestEquipRole.Dim) 
	{
	case MASTER:

		ISDODEBUG(dodebug(0, "EnableLE1553()", "MASTER", (char*)0));
		for(i=(m_NumExch+1); i<=(2*m_NumExch); i++) //link all minor frames
		{
			//IFNSIM(Status=aceBCOpCodeCreate(
			//	m_ResourceAddress,
			//	i,
			//	ACE_OPCODE_CAL,
			//	ACE_CNDTST_ALWAYS,
			//	i-m_NumExch,                     //ID number
			//	0,
			//	0));
		}
		/* Create Interupt opcode that will use msg block */
		//IFNSIM(Status=aceBCOpCodeCreate(
		//	m_ResourceAddress,
		//	2*m_NumExch+1,
		//	ACE_OPCODE_IRQ,
		//	ACE_CNDTST_ALWAYS,
		//	3,                     //ID number
		//	0,
		//	0));
		//if(Status)
		//	ErrorLE1553(Status, Response, BufferSize);

		/* Create Major Frame */
		//IFNSIM(Status=aceBCFrameCreate(m_ResourceAddress,
		//	2*m_NumExch+2,
		//	ACE_FRAME_MAJOR,
		//	aOpCodes+m_NumExch,
		//	m_NumExch+1,
		//	(unsigned short)(m_MajorFrameTime*1e6),
		//	0));
		//if(Status)
		//	ErrorLE1553(Status, Response, BufferSize);
		
		//IFNSIM(Status=aceBCSetMsgRetry(m_ResourceAddress, 
		//	ACE_RETRY_NONE, 
		//	ACE_RETRY_ALT, 
		//	ACE_RETRY_ALT, 
		//	0));
		
		/* Create Host Buffer */
		if(Status)
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "EnableLE1553()", "ErrorLE1553 = %d", Status, (char*)0));
		}

		if (m_Wait.Exists) 
		{
			BCmsgDone=false;  //Will be set to true when interupt occurs
			//Enable interupt to say when frame is done
			//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, 
			//			TRUE, 
			//			ACE_IMR2_BC_UIRQ0, 
			//			BCmsgComplete));
		}
		memset( &sendMsg, 0, sizeof(sendMsg));
		IFNSIM(BTI1553_BCSchedFrame(1000,0,m_HandleCard));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTI1553_BCSchedFrame", (char*)0));

		IFNSIM(BTICard_CardStart(m_HandleCard));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_CardStart", (char*)0));
		if(Status)
		{
			ErrorLE1553(Status, Response, BufferSize);
			ISDODEBUG(dodebug(0, "EnableLE1553()", "ErrorLE1553 = %d", Status, (char*)0));
		}

		sendMsg.ctrlflags = MSGCRT1553_BCRT;					/*Selects BC-RT transfer*/
		sendMsg.errflags = 0;
		sendMsg.cwd1 = BTI1553_ValPackCWD(TERMADDR,RCV,SUBADDR,m_DataSize.Int);		/*Receive command word*/
		for( int ii = 0; ii < 32; ii++ )					// copy 32 data words
		{
			sendMsg.data[ii]   = m_Data[ii];					/*Data word 1 thru 32*/
		}
		IFNSIM(Status = BTI1553_BCTransmitMsg(&sendMsg,0,m_HandleCard));	/*Transmit the message*/
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTI1553_BCTransmitMsg", (char*)0));
		StatusOfLastDataWrite = sendMsg.errflags;
		if (m_Wait.Exists) 
		{
			t1 = clock();
			do 
			{
				t2 = clock();
				timeDiff = (difftime(t2,t1) / CLK_TCK);
				StatusOfLastDataWrite = sendMsg.errflags;
				if( (sendMsg.errflags & (MSGERR1553_NORESP | MSGERR1553_MANCH)) == 0 )
				{
					break;
				}
				Sleep( 10 );
				IFNSIM(Status = BTI1553_BCTransmitMsg(&sendMsg,0,m_HandleCard));	/*Transmit the message*/
				ISDODEBUG(dodebug(0, "EnableLE1553()", "BTI1553_BCTransmitMsg", (char*)0));
			} while (!BCmsgDone && (timeDiff < m_MaxTime.Real)); /* wait until frame is complete */
			//IFNSIM(Status=aceBCStop(m_ResourceAddress));
			//Disable interupt to say when frame is done
			//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, 
			//		FALSE, 
			//		ACE_IMR2_BC_UIRQ0, 
			//		BCmsgComplete));
		}
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("Time diff = %g ", timeDiff), Response, BufferSize);
		ISDODEBUG(dodebug(0, "EnableLE1553()", "Time diff = %g ", timeDiff, (char*)0));
		//{
		//	XMITFIELDS1553 xMsg;				/*Message structure*/
		//	ULONG RTMsg = 0;
		//	USHORT RTData[32];
		//	memset( RTData, 0, sizeof(RTData));
		//	IFNSIM(Status = BTI1553_RTConfig(RTCFG1553_DEFAULT,1,CH0,m_HandleCard));
		//	IFNSIM(RTMsg = BTI1553_RTGetMsg(SUBADDRESS,1,RCV,2,0,m_HandleCard));
		//	if( RTMsg != 0 )
		//		IFNSIM(BTI1553_MsgDataRd(RTData,32,RTMsg,m_HandleCard));	/*Read the data received by the RT*/
		//	RTMsg = 0;
		//}
		IFNSIM(BTICard_CardStop(m_HCore));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_CardStop", (char*)0));

		break;  
	case SLAVE: 
		
		ISDODEBUG(dodebug(0, "EnableLE1553()", "SLAVE", (char*)0));
		IFNSIM(BTICard_CardStart(m_HCore));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_CardStart", (char*)0));
		if(m_Wait.Exists)
		{
			BCmsgDone=false;  //Will be set to true when interupt occurs
			//Enable interupt to say when frame is done
			//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, 
			//		TRUE, 
			//		ACE_IMR1_RT_SUBADDR_EOM, 
			//		BCmsgComplete));
		}
		XMITFIELDS1553 recvMsg;
		ULONG   MsgErrors;
		MSGADDR sendMsgAddr;
		MsgErrors = 0;
		IFNSIM(Status = BTI1553_RTConfig(RTCFG1553_DEFAULT,1,CH0,m_HandleCard));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTI1553_RTConfig", (char*)0));
		memset( &recvMsg, 0, sizeof(recvMsg));
		/* Start RT */
		IFNSIM(BTICard_CardStart(m_HandleCard));
		if( m_BusMode.Dim == RT_CON )
		{
			sendMsgAddr = BTI1553_RTGetMsg(SUBADDRESS, m_SendAddress[0],RCV,m_SendAddress[1],0,m_HandleCard);
			if(Status)
				ErrorLE1553(Status, Response, BufferSize);
			RTMsg = BTI1553_RTGetMsg(SUBADDRESS,m_SendAddress[0],XMT,m_SendAddress[1],CH0,m_HandleCard);
			if (m_Wait.Exists) 
			{
				MsgErrors = BTI1553_MsgFieldRd(FIELD1553_ERROR,RTMsg,m_HandleCard);	/*Read data received from the BC*/
				t1 = clock();
				/* wait for either last exchange to finish or max-time */
				/* statusWord bit 15 (0x8000) = EOM (end of message)   */
				/* function clock() has a resolution of 1 msec         */
				do 
				{
					t2 = clock();
					MsgErrors = BTI1553_MsgFieldRd(FIELD1553_ERROR,RTMsg,m_HandleCard);	/*Read data received from the BC*/
					if( (MsgErrors & MSGERR1553_NORESP) == 0 )
						break;
					timeDiff = (difftime(t2,t1) / CLK_TCK);
				} while ((!BCmsgDone) && (timeDiff < m_MaxTime.Real));

				ATXMLW_DEBUG(5,atxmlw_FmtMsg("Time diff = %g ", timeDiff), Response, BufferSize);
	
				/* Stop RT */
				if(Status)
					ErrorLE1553(Status, Response, BufferSize);
				//Disable interupt to say when frame is done
				//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, 
				//		FALSE, 
				//		ACE_IMR1_RT_SUBADDR_EOM, 
				//		BCmsgComplete));
			}
		}
		else 
		{
			RTMsg = BTI1553_RTGetMsg(SUBADDRESS,m_SendAddress[0],RCV,m_SendAddress[1],CH0,m_HandleCard);
			if (m_Wait.Exists) 
			{
				MsgErrors = BTI1553_MsgFieldRd(FIELD1553_ERROR,RTMsg,m_HandleCard);	/*Read data received from the BC*/
				t1 = clock();
				/* wait for either last exchange to finish or max-time */
				/* statusWord bit 15 (0x8000) = EOM (end of message)   */
				/* function clock() has a resolution of 1 msec         */
				do 
				{
					t2 = clock();
					MsgErrors = BTI1553_MsgFieldRd(FIELD1553_ERROR,RTMsg,m_HandleCard);	/*Read data received from the BC*/
					if( (MsgErrors & MSGERR1553_NORESP) == 0 )
						break;
					timeDiff = (difftime(t2,t1) / CLK_TCK);
				} while ((!BCmsgDone) && (timeDiff < m_MaxTime.Real));

				ATXMLW_DEBUG(5,atxmlw_FmtMsg("Time diff = %g ", timeDiff), Response, BufferSize);
				ISDODEBUG(dodebug(0, "EnableLE1553()", "Time diff = %g ", timeDiff, (char*)0));
	
				/* Stop RT */
				if(Status)
					ErrorLE1553(Status, Response, BufferSize);
				//Disable interupt to say when frame is done
				//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, 
				//		FALSE, 
				//		ACE_IMR1_RT_SUBADDR_EOM, 
				//		BCmsgComplete));
			}
		}
		int datsiz;
		datsiz = 32;
		if( m_DataSize.Exists == true )
			datsiz = m_DataSize.Int;
		BTI1553_MsgDataWr( m_Data, datsiz, RTMsg, m_HandleCard);
		BTI1553_MsgFieldWr(0,FIELD1553_ERROR,RTMsg,m_HandleCard);		/*Clear the 'hit' bit*/
		memset( &sendMsg, 0, sizeof(sendMsg));
		sendMsg.ctrlflags = MSGCRT1553_RTRT;					/*Selects RT-BC transfer*/
		sendMsg.errflags = 0;
		sendMsg.cwd1      = BTI1553_ValPackCWD(m_SendAddress[0],RCV,m_SendAddress[1],datsiz);		/*Receive command word*/
		for( int ii = 0; ii < 32; ii++ )					// copy 32 data words
			sendMsg.data[ii]   = m_Data[ii];					/*Data word 1 thru 32*/
		MSGADDR XmtMsg;
		XmtMsg = BTI1553_RTGetMsg(SUBADDRESS,m_SendAddress[0],RCV,m_SendAddress[1],CH0,m_HandleCard);
		if( XmtMsg != 0 )
		{
			BTI1553_MsgDataWr( m_Data, datsiz, XmtMsg, m_HandleCard);
			Status = BTI1553_MsgFieldRd(FIELD1553_ERROR,RTMsg,m_HandleCard);		/*Clear the 'hit' bit*/
				Sleep( 10 );	//was 500);
		}
		Sleep( 200 );	//was 500);
		IFNSIM(Status = BTICard_CardStop( m_HandleCard ));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_CardStop", (char*)0));
		break;

	case MONITOR:
		Status = 0;		
		ISDODEBUG(dodebug(0, "EnableLE1553()", "MONITOR", (char*)0));
	default:  //MONITOR 
		if(m_Wait.Exists)
		{
			BCmsgDone=false;  //Will be set to true when interupt occurs
			//Enable interupt to say when frame is done
			//IFNSIM(Status=aceSetIrqConditions(m_ResourceAddress, 
			//		TRUE, 
			//		ACE_IMR1_EOM, 
			//		BCmsgComplete));
		}
		XMITFIELDS1553 Msg;				/*Message structure*/
		memset( &Msg, 0, sizeof(Msg));
		IFNSIM(BTI1553_BCSchedFrame(1000,0,m_HandleCard));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTI1553_BCSchedFrame", (char*)0));
		IFNSIM(Status=BTICard_CardStart(m_HandleCard));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_CardStart", (char*)0));
		{
			//IFNSIM(Status = BTICard_CardStart( m_HandleCard ));
			//for( int i = 0; i < 1000; i++ )
			//{
			//	Msg.ctrlflags = MSGCRT1553_RTRT;					/*Selects RT-RT transfer*/
			//	Msg.cwd1      = BTI1553_ValPackCWD(1,0,2,3);		/*Receive command word*/
			//	for( int ii = 0; ii < 32; ii++ )					// copy 32 data words
			//		Msg.data[ii]   = m_Data[ii];					/*Data word 1 thru 32*/
			//	IFNSIM(Status = BTI1553_BCTransmitMsg(&Msg,0,m_HandleCard));	/*Transmit the message*/
			//	Sleep( 2000 );
			//}
		}

		IFNSIM(Status=BTICard_CardStop(m_HandleCard));
		ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_CardStop", (char*)0));
		break;
	}
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorLE1553
//
// Purpose: Query LE1553 for the error text and send to WRTS
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
int  CAtxmlLE1553_5_T::ErrorLE1553(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
 LPCSTR    Msg = "\0";

 if(Status)
 {
	// Decode LE1553 lib return code
	IFNSIM(Msg = BTICard_ErrDescStr((ERRVAL)Status, (HCORE)m_HCore));
	ISDODEBUG(dodebug(0, "EnableLE1553()", "BTICard_ErrDescStr", (char*)0));
	sprintf(Response,"PnP Error [%X] %s! \n", Status, Msg);
	BufferSize = strlen(Response);
	atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, (char*)Msg);
 }
 return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoLE1553
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
int CAtxmlLE1553_5_T::GetStmtInfoLE1553(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue=-1;
    char   LclUnit[ATXMLW_MAX_NAME]="";
    char  *LclCharValuePtr, *cptr;
	int size, tempArray[2], i;
	
	ISDODEBUG(dodebug(0, "Entering GetStmtInfoLE1553()", "SignalDescription %s", SignalDescription, (char*)0));
	//get MaxTime
	if(cptr = strstr(SignalDescription, "<SignalTimeOut>"))
	{
		m_MaxTime.Exists=true;
		sscanf(cptr, "<SignalTimeOut>%lf<", &m_MaxTime.Real);
	}

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{		
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Response buffer %s", Response, (char*)0));
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "SignalDescription buffer %s", SignalDescription, (char*)0));
        return(Status);
	}

	m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "m_Action = %d", m_Action, (char*)0));
    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource), Response, BufferSize);
	ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "LclResource = %d", LclResource, (char*)0));
    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "SignalOut [%s] [%s]", m_SignalName, m_SignalElement, (char*)0));
	}

	//respTime
    if((m_ResponseTime.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "respTime", &LclDblValue, LclUnit)))
	{
		m_ResponseTime.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Response-Time %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Response-Time %E [%s]", LclDblValue, LclUnit, (char*)0));
	}

	//messageGap
    if((m_MessageGap.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "messageGap", &LclDblValue, LclUnit)))
	{
		m_MessageGap.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Message-Gap %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Message-Gap %E [%s]", LclDblValue, LclUnit, (char*)0));
	}

	//Role
	if((m_TestEquipRole.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "Role", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "Master")==0)
			m_TestEquipRole.Dim=MASTER;
		else if(strcmp(LclCharValuePtr, "Slave")==0)
			m_TestEquipRole.Dim=SLAVE;
		else if(strcmp(LclCharValuePtr, "Monitor")==0)
			m_TestEquipRole.Dim=MONITOR;
	    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Test-Equip-Role [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s Test-Equip-Role [%s]", m_SignalName, LclCharValuePtr, (char*)0));
	}

	//Mode
	if((m_BusMode.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "Mode", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "Con-RT")==0)
			m_BusMode.Dim=CON_RT;
		else if(strcmp(LclCharValuePtr, "RT-Con")==0)
			m_BusMode.Dim=RT_CON;
		else if(strcmp(LclCharValuePtr, "RT-RT")==0)
			m_BusMode.Dim=RT_RT;
		else if(strcmp(LclCharValuePtr, "Con-Mode")==0)
			m_BusMode.Dim=CON_MODE;
		else if(strcmp(LclCharValuePtr, "All-Listener")==0)
			m_BusMode.Dim=ALL_LISTENER;

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Bus-Mode [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s Bus-Mode [%s]", m_SignalName, LclCharValuePtr, (char*)0));
	}

	//data
	if((m_DataSize.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "data", &LclCharValuePtr)))
	{
		bin_string_to_array(LclCharValuePtr, &m_Data[0], &m_DataSize.Int);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s data [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s data [%s]\nConversion:", m_SignalName, LclCharValuePtr, (char*)0));
		for(i=0; i<m_DataSize.Int; i++)
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("data[%d] = [%x]", i, m_Data[i]), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "data[%d] = [%x]", i, m_Data[i], (char*)0));
		}
	}

	//command
	if((m_Command[0].Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "command", &LclCharValuePtr)))
	{
		tempArray[0]=0;
		tempArray[1]=0;
		bin_string_to_array(LclCharValuePtr, (unsigned short *)&tempArray[0], &size);
		m_Command[0].Int=tempArray[0];
		if(size>1)
		{
			m_Command[1].Exists=true;
			m_Command[1].Int=tempArray[1];
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s command [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s command is %s\nConversion:", m_SignalName, LclCharValuePtr, (char*)0));
		for(i=0; i<size; i++)
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("command[%d] = [%x]", i, m_Command[i].Int), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "command[%d] = [%x]", i, m_Command[i].Int, (char*)0));
		}
	}

	//status
	if((m_Status.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "status", &LclCharValuePtr)))
	{
		m_Status.Int=0;
		bin_string_to_array(LclCharValuePtr, (unsigned short *)&m_Status.Int, &size);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s status [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s status %s\nConversion:", m_SignalName, LclCharValuePtr, (char*)0));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("status = [%x]", m_Status.Int), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "status = [%x]", m_Status.Int, (char*)0));
	}

	//sendAddress
	if((m_SendAddressSize.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "sendAddress", &LclCharValuePtr)))
	{
		string_to_array(LclCharValuePtr, &m_SendAddress[0], &m_SendAddressSize.Int);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sending address [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s Sending address [%s]\nConversion:", m_SignalName, LclCharValuePtr, (char*)0));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("RT = [%d]", m_SendAddress[0]), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "RT = [%d]", m_SendAddress[0], (char*)0));
		for(i=1; i<m_SendAddressSize.Int; i++)
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("SubAddress[%d] = [%d]", i, m_SendAddress[i]), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "SubAddress[%d] = [%d]", i, m_SendAddress[i], (char*)0));
		}
	}

	//recieveAddress
	if((m_RecieveAddressSize.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "recieveAddress", &LclCharValuePtr)))
	{
		string_to_array(LclCharValuePtr, &m_RecieveAddress[0], &m_RecieveAddressSize.Int);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Recieveing address [%s]\nConversion:", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s Recieveing address [%s]\nConversion:", m_SignalName, LclCharValuePtr, (char*)0));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("RT = [%d]", m_RecieveAddress[0]), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "RT = [%d]", m_RecieveAddress[0], (char*)0));
		for(i=1; i<m_RecieveAddressSize.Int; i++)
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("SubAddress[%d] = [%d]", i, m_RecieveAddress[i]), Response, BufferSize);
			ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "SubAddress[%d] = [%d]", i, m_RecieveAddress[i], (char*)0));
		}
	}

	//process
	if((m_TestEquipRole.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "process", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "Proceed")==0)
			m_Proceed.Exists=true;
		else if(strcmp(LclCharValuePtr, "Wait")==0)
			m_Wait.Exists=true;
		
	 ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s - [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s - [%s]", m_SignalName, LclCharValuePtr, (char*)0));
	}

	//maxTime
    if((m_MaxTime.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "maxTime", &LclDblValue, LclUnit)))
	{
		m_MaxTime.Real=LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Max-Time %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found Max-Time %E [%s]", LclDblValue, LclUnit, (char*)0));
	}

	//exnm
    if((m_tExnm.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "Exnm", &LclDblValue, LclUnit)))
	{
		m_tExnm.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Exnm %d [%s]", m_tExnm.Int,LclUnit), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found Exnm %d [%s]", m_tExnm.Int, LclUnit, (char*)0));
	}

	//attribute
	if((m_attribute.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "data")==0)
		{
			m_attribute.Dim=DATA;
		}
		else if(strcmp(LclCharValuePtr, "status")==0)
		{
			m_attribute.Dim=STATUS;
		}
		else if(strcmp(LclCharValuePtr, "command")==0)
		{
			m_attribute.Dim=COMMAND;
		}
		else if(strcmp(LclCharValuePtr, "config")==0)
		{
			m_attribute.Dim=CONFIG;
		}
	    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s attribute [%s]", m_SignalName, LclCharValuePtr), Response, BufferSize);
		ISDODEBUG(dodebug(0, "GetStmtInfoLE1553()", "Found %s attribute [%s]", m_SignalName, LclCharValuePtr, (char*)0));
	}

	////Close Handle for Copilot
	////exnm
    if((m_ReleaseHandle.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "ReleaseHandle", &LclCharValuePtr)))
	{
		if(!strcmp(LclCharValuePtr, "True"))
			m_ReleaseHandle.Int = true;
		else if(!strcmp(LclCharValuePtr, "False"))
			m_ReleaseHandle.Int = false;
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found ReleaseHandle [%s]", LclCharValuePtr), Response, BufferSize);
	}

	atxmlw_Close1641Xml(&m_SignalDescription);
  return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateLE1553
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
void CAtxmlLE1553_5_T::InitPrivateLE1553(void)
{
	m_BusSpec.Exists=false;
	m_DataSize.Exists=false;
	m_Command[0].Exists=false;
	m_Command[1].Exists=false;
	m_Command[0].Int = 0;			// set command to 0
	m_Command[1].Int = 0;
	m_Status.Exists=false;
	m_BusMode.Exists=false;
	m_Proceed.Exists=false;
	m_Wait.Exists=false;
	m_TestEquipRole.Exists=false;
	m_MessageGap.Exists=false;
	m_ResponseTime.Exists=false;
	m_MaxTime.Exists=false;
	m_SendAddressSize.Exists=false;
	m_RecieveAddressSize.Exists=false;
	m_attribute.Exists=false;
	m_tExnm.Exists=false;
	m_Delay.Exists=false;
	m_initialized=false;
	m_NumExch=0;
	m_SendAddress[0]=0;
	m_SendAddress[1]=0;
	m_RecieveAddress[0]=0;
	m_RecieveAddress[1]=0;
	m_MajorFrameTime=0; //zero major frame time
	m_controlFlags.Exists=false;
	m_controlFlags.UnLong = 0;
	StatusOfLastDataWrite = 0;
	memset( &sendMsg, 0, sizeof(sendMsg));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataLE1553
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
void CAtxmlLE1553_5_T::NullCalDataLE1553(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: bin_string_to_array()
//
// Purpose: Converts a Null terminated string comforming to
//           the serial data type(H's and L's) in xml 1641 to an integer array
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
void bin_string_to_array(char input [], unsigned short array [], int * size)
{
	char string[2048]="";
	char *temp;
	int i=0;
	int j;
	strcpy(string, input);

	temp=strtok(string, ",");
	while(temp!=NULL)
	{
		array[i]=0;
		for(j=0; j<16; j++)
		{
			if(temp[j]=='H')
				array[i]=array[i] + (1 << (15-j)); 
		}
		i++;
		temp=strtok(NULL, ",");
		if(temp)
			temp++;
	}
	*size=i;

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: array_to_bin_string()
//
// Purpose: Converts an integer array to a Null terminated string comforming to
//           the serial data type(H's and L's) in xml 1641
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int *           integer array to be converted
// size             int             size of integer array to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void array_to_bin_string(unsigned short array [], int size, char output [])
{
	char string[768]="";
	char temp[20]="";

	for(int i=0; i<size; i++)
	{
		for(int j=15; j>=0; j--)
		{
			if((array[i]>>j)&0x0001)
			{
				strcat(temp, "H");
			}
			else
			{
				strcat(temp, "L");
			}
		}
		if(i!=size-1)
			strcat(temp,", ");
		strcat(string,temp);
		temp[0]='\0';
	}
	if(size<1)
		strcpy(string, " ");
    strcpy(output, string);
	return;
}

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
void string_to_array(char input [], int array [], int * size)
{
	char string[768]="";
	char *temp;
	int i=0;
	strcpy(string, input);

	temp=strtok(string, ",");
	while(temp!=NULL)
	{
		array[i]=0;
		array[i]=atoi(temp);
		i++;
		temp=strtok(NULL, ",");
		if(temp)
			temp++;
	}
	*size=i;

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: array_to_string()
//
// Purpose: Converts an integer array to a Null terminated string containing
//           integer values separated by ", "
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int *           integer array to be converted
// size             int             size of integer array to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void array_to_string(int array [], int size, char output [])
{
	char string[768]="";
	char temp[20]="";

	for(int i=0; i<size; i++)
	{
		itoa(array[i], temp, 10);
		if(i!=size-1)
			strcat(temp,", ");
		strcat(string,temp);
		temp[0]='\0';
	}
    strcpy(output, string);
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: BCmsgComplete()
//
// Purpose: Sets a global variable to specify that the Bus Controller transfer
//           is complete                       
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// DevNum           S16BIT          None                         
// Status           U32BIT          None                                 
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
/* Definition of User Interrupt Service Routine */ 
void _DECL BCmsgComplete( S16BIT DevNum, U32BIT Status) 
{ 
	BCmsgDone=true;
	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function : dodebug(int, char*, char* format, ...);                         //
// Purpose  : Print the message to a debug file.                              //
// Return   : None if it don't work o well                                    //
////////////////////////////////////////////////////////////////////////////////
void dodebug(int code, char *function_name, char *format, ...)
{

	static int		FirstRun = 0;
	char			TmpBuf[_MAX_PATH];

	if (DE_BUG == 1 && FirstRun == 0) {

		sprintf(TmpBuf, "%s", DEBUGIT_1553);
		if ((debugfp = fopen(TmpBuf, "w+b")) == NULL) {
			DE_BUG = 0;
			return;
		}
		FirstRun++;
	}

	va_list	arglist;

	if (DE_BUG) {

		if (code != 0) {
			fprintf(debugfp, "%s in %s\r\n", strerror(code), function_name);
		}
		else {
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