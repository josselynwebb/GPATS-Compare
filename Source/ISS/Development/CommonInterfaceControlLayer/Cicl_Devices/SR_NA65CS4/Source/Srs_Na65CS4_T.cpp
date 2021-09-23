//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Srs_Na65CS4_T.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXMLW Instrument Driver for Srs_Na65CS4
//
// Instrument:	Srs_Na65CS4  <Device Description> (<device Type>)
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
// 1.0.0.0  11OCT05  Baseline Release                        
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <math.h>
#include "AtxmlWrapper.h"
#include "Srs_Na65CS4_T.h"
#include "visa.h"

// Local Defines

// Function codes

//////// Place Srs_Na65CS4 specific data here //////////////
//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables
static ViSession defaultRM;

static char  s_Resource[ATXMLW_MAX_NAME] = "";
static int   s_Mode = 0;
static int   s_Chan = 0;
static bool  s_Multi = false;
static bool *s_ExtRefPtr;
static int   s_App = 0;

static AttributeStruct *s_RefVoltPtr;
static AttributeStruct *s_FreqPtr;
static AttributeStruct *s_RatioPtr;

// Local Function Prototypes

static void s_IssueDriverFunctionCallDeviceXxx(int Handle,
                ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);


//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CSrs_Na65CS4_T
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

CSrs_Na65CS4_T::CSrs_Na65CS4_T(int Instno, int ResourceType, char* ResourceName,
        int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
        ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    int  Status = 0;
    char Dummy[100] = "";
   	char buffer[100] = "";
    char LclMsg[1024] = "";
  	ViUInt32 writeCount;
   	ViUInt32 retCount;

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
    m_InitString[0] = '\0';

    InitPrivateSrs_Na65CS4(0);
	NullCalDataSrs_Na65CS4();

    // form init string
    sprintf(m_InitString, "%s%d::%d::INSTR",
              m_AddressInfo.ControllerType, m_AddressInfo.ControllerNumber,
              m_AddressInfo.PrimaryAddress);
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-CSrs_Na65CS4 Class called with Instno %d, Sim %d, Dbg %d", 
            m_InstNo, m_Sim, m_Dbg), Response, Buffersize);

    // initialize the Srs_Na65CS4

    // get resource manager handle
    IFNSIM(Status = viOpenDefaultRM(&defaultRM));
    if (Status < VI_SUCCESS)  
        return;
	ATXMLW_DEBUG(2, atxmlw_FmtMsg("%x = viOpenDefaultRM;", Status), Response, Buffersize);

	// get instrument handle
	IFNSIM(Status = viOpen(defaultRM, m_InitString, VI_NULL, VI_NULL, (ViPSession)&m_Handle));
	ATXMLW_DEBUG(2, atxmlw_FmtMsg("%x = viOpen;", Status, m_InitString), Response, Buffersize);
	if (Status < VI_SUCCESS)  
		return;

    // IDN
	strcpy(Dummy, "*IDN?" CRLF);
    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	if (Status)
        ErrorSrs_Na65CS4(Status, Response, Buffersize);
	ATXMLW_DEBUG(2, atxmlw_FmtMsg("%x = viWrite RST (%x);", Status, m_Handle), Response, Buffersize);
	IFNSIM(Status = viBufRead(m_Handle, (unsigned char *)buffer, 100, &retCount));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Response: %s", buffer), Response, Buffersize);

	// reset
	strcpy(Dummy, "*RST" CRLF);
    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	if (Status)
        ErrorSrs_Na65CS4(Status, Response, Buffersize);
	ATXMLW_DEBUG(2, atxmlw_FmtMsg("%x = viWrite RST (%x);", Status, m_Handle), Response, Buffersize);
	
    //if (Status && ErrorSrs_Na65CS4(Status, Response, Buffersize)) // redundant?
    //    return;
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CSrs_Na65CS4_T()
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
CSrs_Na65CS4_T::~CSrs_Na65CS4_T()
{
    int Status = 0;
    char Dummy[1024];
  	ViUInt32 writeCount;

    // Reset the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-~CSrs_Na65CS4 Class Destructor called "), Dummy, 1024);

    // Reset the Na65CS4
	strcpy(Dummy, "*RST" CRLF); 
    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
//  if (Status)
//      ErrorSrs_Na65CS4(Status, Response, Buffersize);
//  ATXMLW_DEBUG(5, Dummy, Response, Buffersize);
	
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: StatusSrs_Na65CS4
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
int CSrs_Na65CS4_T::StatusSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Status action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-StatusSrs_Na65CS4 called "), Response, BufferSize);

    // Check for any pending error messages
    Status = ErrorSrs_Na65CS4(0, Response, BufferSize);

    return (Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalSrs_Na65CS4
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
int CSrs_Na65CS4_T::IssueSignalSrs_Na65CS4(ATXMLW_INTF_SIGDESC* SignalDescription,
        ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IssueSignalSrs_Na65CS4 Signal: "),
            Response, BufferSize);

    // GetStmtInfoSrs_Na65CS4 parses the signal description and captures the
    //   member attributes defined in the AtXmlSrs_Na65CS4_T.h file
    if ((Status = GetStmtInfoSrs_Na65CS4(SignalDescription, Response, BufferSize)) != 0)
        return (Status);

    // The following Actions use the
    //   member attributes defined in the AtXmlSrs_Na65CS4_T.h file
    switch(m_Action)
    {
    case ATXMLW_SA_APPLY:
        if ((Status = SetupSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        if ((Status = EnableSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_REMOVE:
        if ((Status = DisableSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        if ((Status = ResetSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_MEASURE:
        if ((Status = SetupSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_READ:
        if ((Status = EnableSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        if ((Status = FetchSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_RESET:
        if ((Status = ResetSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_SETUP:
        if ((Status = SetupSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_CONNECT:
        break;
    case ATXMLW_SA_ENABLE:
        if ((Status = EnableSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_DISABLE:
        if ((Status = DisableSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_FETCH:
        if ((Status = FetchSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    case ATXMLW_SA_DISCONNECT:
        break;
	case ATXMLW_SA_STATUS:
        if ((Status = StatusSrs_Na65CS4(Response, BufferSize)) != 0)
            return (Status);
        break;
    }

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: RegCalSrs_Na65CS4
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
int CSrs_Na65CS4_T::RegCalSrs_Na65CS4(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
//  char     *CalString;
//  ATXMLW_XML_HANDLE CalHandle;
    char      Dummy[1024];

    // Setup action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-RegCalSrs_Na65CS4 CalData: %s", 
            CalData), Dummy, 1024);

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE 
    if ((Status = atxmlw_ParseXml(CalData, &CalHandle, NULL, 0)))
    {
        atxmlw_CloseXmlHandle();
        return (Status);
    }
    if ((CalString = atxmlw_GetAttributeFirst(CalHandle, &CalString,
                       "voltagecomp", "value") != NULL))
    {
        m_CalData[0] = atxmlw_GetDoubleValue(CalString);
        ...
    }
    
    atxmlw_CloseXmlHandle();

/* End TEMPLATE_SAMPLE_CODE */

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetSrs_Na65CS4
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
int CSrs_Na65CS4_T::ResetSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";
    char  Dummy[100] = "";
   	ViUInt32 writeCount;


    // Reset action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-ResetSrs_Na65CS4 called "), Response, BufferSize);

    // Reset the Na65CS4
	strcpy(Dummy, "*RST" CRLF); 
    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
    if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);
	ATXMLW_DEBUG(5, Dummy, Response, BufferSize);

    InitPrivateSrs_Na65CS4(0);

    return (Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IstSrs_Na65CS4
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
int CSrs_Na65CS4_T::IstSrs_Na65CS4(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char  Dummy[100] = "";
  	ViUInt32 writeCount;
    ViUInt32 retCount;
	char buffer[100] = "";

    // self-test action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IstSrs_Na65CS4 called Level %d", 
            Level), Response, BufferSize);

    // self-test the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IstAgE1420B called Level %d", 
            Level), Response, BufferSize);

	strcpy(Dummy, "*TST?" CRLF); 
    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
    if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);
	ATXMLW_DEBUG(5, Dummy, Response, BufferSize);

	IFNSIM(Status = viBufRead(m_Handle, (unsigned char *)buffer, 100, &retCount));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Response: %s", buffer), Response, BufferSize);
//  atxmlw_ScalerStringReturn(m_MeasAttr, "", buffer, Response, BufferSize);

    if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);

    InitPrivateSrs_Na65CS4(s_Chan);

    return (Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsSrs_Na65CS4
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
int CSrs_Na65CS4_T::IssueNativeCmdsSrs_Na65CS4(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Native action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-IssueNativeCmdsSrs_Na65CS4 "), Response, BufferSize);

    // issue native command
    if ((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return (Status);
    }

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallSrs_Na65CS4
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
int CSrs_Na65CS4_T::IssueDriverFunctionCallSrs_Na65CS4(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, "Wrap-IssueDriverFunctionCallSrs_Na65CS4", Response, BufferSize);
	    // Retrieve the Parameters

    IFNSIM(s_IssueDriverFunctionCallDeviceXxx(m_Handle, DriverFunction,
                            Response, BufferSize));

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE 
    if ((Status = atxmlw_IssueDriverFunctionCallSrs_Na65CS4(m_Handle, DriverFunction,
                                        Response, BufferSize)))
    {
        return (Status);
    }
/* End TEMPLATE_SAMPLE_CODE */

    return (0);
}


//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupSrs_Na65CS4
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
int CSrs_Na65CS4_T::SetupSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char  AppStr[10] = "";
    char  ModeStr[10] = "";

    // Setup action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, "Wrap-SetupSrs_Na65CS4 called ", Response, BufferSize);

    strcpy(AppStr, s_App == STIMULUS ? "DSH" : "SDH");
    strcpy(ModeStr, s_Mode == SYNCHRO ? "SYN" : "RSL");

    SetupChan(s_Chan, AppStr, ModeStr, true, Response, BufferSize);
    if (s_RatioPtr->Exists)
        SetupChan(s_Chan, AppStr, ModeStr, false, Response, BufferSize);

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchSrs_Na65CS4
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
int CSrs_Na65CS4_T::FetchSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
	double    MeasValue = 0.0;
    int       Ch = s_Chan + (s_RatioPtr->Exists ? 1 : 0);
//  double    MaxTime = 5000;
    char      buffer[100] = "";
    char      Dummy[1024] = "";
    ViUInt32  writeCount;
    ViUInt32  retCount;
    char     *ErrMsg = "";

    // Fetch action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-FetchSrs_Na65CS4 called "), Response, BufferSize);

    // get resource information
    GetResourceInfo(Response, BufferSize);

//  // Check MaxTime Modifier
//  if (m_MeasInfo[s_Chan].TimeOut > 0.0)
//      MaxTime = m_MeasInfo[s_Chan].TimeOut * 1000;

    // Fetch data

    // get attribute
	if (strcmp(m_MeasInfo[s_Chan].cyType, "angle") == 0) 
	{
        Sleep(1000);
		sprintf(Dummy, "SDH%d ANGLE?" CRLF, Ch); 
	}
	else if (strcmp(m_MeasInfo[s_Chan].cyType, "angle_rate") == 0) 
	{
		Sleep(500);
		sprintf(Dummy, "SDH%d VEL?" CRLF, Ch); 
	}

	ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);

    Sleep(1000);  //JRC

	IFNSIM(Status = viBufRead(m_Handle, (unsigned char *)buffer, 100, &retCount));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Response: %s", buffer), Response, BufferSize);

    if (Status)
    {
        MeasValue = FLT_MAX;
        ErrorSrs_Na65CS4(Status, Response, BufferSize);
    }
    else
    {
        sscanf(buffer, "%le", &MeasValue);
    }

    if (Response && (BufferSize > (int)(strlen(Response)+200)))
    {
        atxmlw_ScalerDoubleReturn(m_MeasInfo[s_Chan].cyType, "", MeasValue,
                    Response, BufferSize);
    }

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: DisableSrs_Na65CS4
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
int CSrs_Na65CS4_T::DisableSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
    char      Dummy[1024] = "";
    ViUInt32  writeCount;

    // Open action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-OpenSrs_Na65CS4 called "), Response, BufferSize);

    // get resource information
    GetResourceInfo(Response, BufferSize);

	if (s_App == STIMULUS) {
		sprintf(Dummy, "DSH%d STATE OPEN" CRLF, s_Chan); 
	}
    else if (s_App == MEASUREMENT) {
		sprintf(Dummy, "SDH%d STATE OPEN" CRLF, s_Chan); 
	}

    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);

    if (s_RatioPtr->Exists)     // open the "fine" relay as well if multi-speed
    {
	    if (s_App == STIMULUS) {
		    sprintf(Dummy, "DSH%d STATE OPEN" CRLF, s_Chan + 1); 
	    }
        else if (s_App == MEASUREMENT) {
		    sprintf(Dummy, "SDH%d STATE OPEN" CRLF, s_Chan + 1); 
	    }

        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	    if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);
    }

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableSrs_Na65CS4
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
int CSrs_Na65CS4_T::EnableSrs_Na65CS4(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
    char      Dummy[1024] = "";
    ViUInt32  writeCount;

    // Close action for the Srs_Na65CS4
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-EnableSrs_Na65CS4 called "), Response, BufferSize);

    Sleep(1000);

    // get resource information
    GetResourceInfo(Response, BufferSize);

	if (s_App == STIMULUS)
    {
		sprintf(Dummy, "DSH%d STATE CLOSE" CRLF, s_Chan); 
        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	    if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);

        if (s_RatioPtr->Exists)     // close the "fine" relay as well if multi-speed
        {
            sprintf(Dummy, "DSH%d STATE CLOSE" CRLF, s_Chan + 1); 
            ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	        if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);
        }

        if ((m_AngleRate[s_Chan].Exists) && (m_AngleRate[s_Chan].Real != 0.0) && !m_TrigInfo[s_Chan].TrigExists)
		{
			sprintf(Dummy, "DSH%d ROT_INIT" CRLF, s_Chan); 
            ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	        if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);
		}
	}
    else if (s_App == MEASUREMENT)
    {
		sprintf(Dummy, "SDH%d STATE CLOSE" CRLF, s_Chan); 
        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	    if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);

        if (s_RatioPtr->Exists)     // close the "fine" relay as well if multi-speed
        {
            sprintf(Dummy, "SDH%d STATE CLOSE" CRLF, s_Chan + 1); 
            ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	        if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);
        }
	}
 
    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorSrs_Na65CS4
//
// Purpose: Query Srs_Na65CS4 for the error text and send to WRTS
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
int CSrs_Na65CS4_T::ErrorSrs_Na65CS4(int Status,
        ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char      buffer[100] = "";
    char      Dummy[1024] = "";
    ViUInt32  writeCount;
    ViUInt32  retCount;
    int       Err = 0;
    char      Msg[MAX_MSG_SIZE] = "";
    int       QError;

    if (Status)
    {
        // Decode Srs_Na65CS4 lib return code
        IFNSIM(Err = viStatusDesc(defaultRM, (ViStatus)Status, Msg));
        if (Err)
            sprintf(Msg, "Plug 'n' Play Error: Unable to get error text [%X]!", Err);

        if(Status == VI_ERROR_TMO)
            Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;

        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
                Status, Msg);
    }

    // Retrieve any pending errors in the device
    QError = 1;
    while(QError)
    {
        QError = 0;

		strcpy(Dummy, "*ERR?" CRLF);
        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));

		IFNSIM(Status = viBufRead(m_Handle, (unsigned char *)buffer, 100, &retCount));
		IFNSIM(buffer[retCount] = '\0');
        ATXMLW_DEBUG(5, atxmlw_FmtMsg("Response: %s", buffer), Response, BufferSize);

        if (Status != 0)
            break;

        if (QError)
        {
            if (Status == VI_ERROR_TMO)
                Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;

            atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
                    Status, Msg);
        }
    }

    return (Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoSrs_Na65CS4
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
int CSrs_Na65CS4_T::GetStmtInfoSrs_Na65CS4(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
    char   Name[ATXMLW_MAX_NAME];
    char  *NextName;
    char  *InputNames = "";

    if ((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription,
                Response, BufferSize)))
        return (Status);

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription,
            Response, BufferSize);
    ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action),
            Response, BufferSize);

    // get resource information
    GetResourceInfo(Response, BufferSize);

    // get Signal Out name
    if ((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
    {
        ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
                m_SignalName, m_SignalElement), Response, BufferSize);

        strncpy(Name, m_SignalName, ATXMLW_MAX_NAME);
    }
    else
        return (0);     // no Signal Out so probably no Signal at all...

    if (s_App == STIMULUS)
    {
        // get signal conditioning from the Signal Out chain 
        if (GetSignalCond(Name, InputNames, Response, BufferSize) == 0)
        {
            ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", Name),
                    Response, BufferSize);
        }
    }
    else if (s_App == MEASUREMENT)
    {
        //  get measurement type
        if ((atxmlw_Get1641StdMeasChar(m_SignalDescription, Name, &m_MeasInfo[s_Chan])))
            ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s MeasChar %d",
                    Name, m_MeasInfo[s_Chan].StdType), Response, BufferSize);

        // get signal characteristics from As chain
        if (ISSTRATTR("As", &NextName))
        {
            ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
                    Name, NextName), Response, BufferSize);
            if (GetSignalCond(NextName, InputNames, Response, BufferSize) == 0)
                ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", NextName),
                        Response, BufferSize);
        }

        // get signal conditioning from the In chain 
        if (ISSTRATTR("In", &NextName))
        {
            ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s In [%s]",
                    Name, NextName), Response, BufferSize);

            if ((GetSignalCond(NextName, InputNames, Response, BufferSize)))
            {
                ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", NextName),
                        Response, BufferSize);
            }
        }
    }

    atxmlw_Close1641Xml(&m_SignalDescription);

    return (0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateSrs_Na65CS4
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
void CSrs_Na65CS4_T::InitPrivateSrs_Na65CS4(int s_Chan)
{
    int startCh = 0;
    int stopCh = CHANNELS - 1;

    if (s_Chan != 0)
        startCh = stopCh = s_Chan;

    for (int ch = startCh; ch <= stopCh; ch++)
    {
        if (s_App == 0 || s_App == STIMULUS)
        {
            m_Angle[ch].Exists = false;
            m_AngleRate[ch].Exists = false;
            m_VoltageOut[ch].Exists = false;
            m_FreqSim[ch].Exists = false;
            m_RefVoltSim[ch].Exists = false;
            m_RatioSim[ch].Exists = false;
            m_ExtRefSim[ch] = false;

            memset(&m_TrigInfo[ch], 0, sizeof(ATXMLW_STD_TRIG_INFO));
        }

        if (s_App == 0 || s_App == MEASUREMENT)
        {
            m_FreqInd[ch].Exists = false;
            m_RefVoltInd[ch].Exists = false;
            m_RatioInd[ch].Exists = false;
            m_ExtRefInd[ch] = false;
            m_MaxTime[ch].Exists = false;

            memset(&m_MeasInfo[ch], 0, sizeof(ATXMLW_STD_MEAS_INFO));
        }
    }

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataSrs_Na65CS4
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
void CSrs_Na65CS4_T::NullCalDataSrs_Na65CS4(void)
{
    //////// Place Srs_Na65CS4 specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE */
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetSignalCond()
//
// Purpose: Parse the 1641 for signal conditioning starting with Name
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
//      0 - OK
//      - - Failure code
//   m_Signal char filled in accordingly
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_Na65CS4_T::GetSignalCond(char *Name, char *InputNames, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char   Element[ATXMLW_MAX_NAME] = "";
    char   Unit[ATXMLW_MAX_NAME] = "";
    char  *TempPtr;
    int    RetVal = 0;

    if (strstr(Name, " "))      // Name with multiple chains
    {
        // get initial token
        char *InToken = strtok(Name, " ");
        while (InToken != NULL)
        {
            // parse the chain for this token
            GetSignalCond(InToken, InputNames, Response, BufferSize);

            // get next token
            InToken = strtok(NULL, " ");
        }
    }
    else
    {
        if (atxmlw_IsPortNameTsf(m_SignalDescription, InputNames, Name, &TempPtr))
        {
            // found an input port name
            switch (*Name)
            {
            //case 'C':   // channel number "Ch#"
            //    // already have the channel number from the resource name
            //    break;

            case 'R':   // "RefExt"
                *s_ExtRefPtr = true;
                break;
            }
        }
        else if ((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	    {
            if (ISELEMENT("SYNCHRO"))       // atXml:SYNCHRO
                s_Mode = SYNCHRO;
            else if (ISELEMENT("RESOLVER")) // atXml:RESOLVER
                s_Mode = RESOLVER;

            if (s_Mode)
		    {
                if (ISINTATTR("speed_ratio", &(s_RatioPtr->Int)))
                {
                    s_RatioPtr->Exists = true;
                    if (s_RatioPtr->Int < 1 || s_RatioPtr->Int > 255)
                    {
                        atxmlw_ErrorResponse(s_Resource, Response, BufferSize, "GetStmtInfo",
                                ATXMLW_WRAPPER_ERRCD_RANGE_ERROR,
                                atxmlw_FmtMsg(ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR ": speed_ratio %d outside [1-255]",
                                s_RatioPtr->Int));
                        return (ATXMLW_WRAPPER_ERRCD_RANGE_ERROR);
                    }
                }
                if (ISDBLATTR("freq", &(s_FreqPtr->Real), s_FreqPtr->Dim))
                {
                    s_FreqPtr->Exists = true;
                    if (s_FreqPtr->Real < 47.0 || s_FreqPtr->Real > 2000.0)
                    { 
                        atxmlw_ErrorResponse(s_Resource, Response, BufferSize, "GetStmtInfo",
                                ATXMLW_WRAPPER_ERRCD_RANGE_ERROR,
                                atxmlw_FmtMsg(ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR ": freq %lf outside [47-2000]",
                                s_FreqPtr->Real));
                        return (ATXMLW_WRAPPER_ERRCD_RANGE_ERROR);
                    }
                }
                if (ISDBLATTR("ref_volt", &(s_RefVoltPtr->Real), s_RefVoltPtr->Dim))
                {
                    s_RefVoltPtr->Exists = true;
                    if (s_RefVoltPtr->Real < 2.0 || s_RefVoltPtr->Real > 115.0)
                    { 
                        atxmlw_ErrorResponse(s_Resource, Response, BufferSize, "GetStmtInfo",
                                ATXMLW_WRAPPER_ERRCD_RANGE_ERROR,
                                atxmlw_FmtMsg(ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR ": ref_volt %lf outside [2-115]",
                                s_RefVoltPtr->Real));
                         return (ATXMLW_WRAPPER_ERRCD_RANGE_ERROR);
                    }
               }

                if (s_App == STIMULUS)
                {
                    if (ISDBLATTR("angle", &(m_Angle[s_Chan].Real), m_Angle[s_Chan].Dim))
                    {
                        m_Angle[s_Chan].Exists = true;
                        m_Angle[s_Chan].Real = fmod(m_Angle[s_Chan].Real, 360.0);
                        // shouldn't need...
                        if (m_Angle[s_Chan].Real < 0.0 || m_Angle[s_Chan].Real >= 360.0)
                        { 
                            atxmlw_ErrorResponse(s_Resource, Response, BufferSize, "GetStmtInfo",
                                    ATXMLW_WRAPPER_ERRCD_RANGE_ERROR,
                                    atxmlw_FmtMsg(ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR ": angle %lf outside [0-360)",
                                    m_Angle[s_Chan].Real));
                            return (ATXMLW_WRAPPER_ERRCD_RANGE_ERROR);
                        }
                    }
                    if (ISDBLATTR("angle_rate", &(m_AngleRate[s_Chan].Real), m_AngleRate[s_Chan].Dim))
                    {
                        m_AngleRate[s_Chan].Exists = true;
                        if (m_AngleRate[s_Chan].Real < 0.0 || m_AngleRate[s_Chan].Real > 4896.0)
                        { 
                            atxmlw_ErrorResponse(s_Resource, Response, BufferSize, "GetStmtInfo",
                                    ATXMLW_WRAPPER_ERRCD_RANGE_ERROR,
                                    atxmlw_FmtMsg(ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR ": angle_rate %lf outside [0-4896]",
                                    m_AngleRate[s_Chan].Real));
                            return (ATXMLW_WRAPPER_ERRCD_RANGE_ERROR);
                        }
                    }
                    if (ISDBLATTR("voltage", &(m_VoltageOut[s_Chan].Real), m_VoltageOut[s_Chan].Dim))
                    {
                        m_VoltageOut[s_Chan].Exists = true;
                        if (m_VoltageOut[s_Chan].Real < 1.0 || m_VoltageOut[s_Chan].Real > 90.0)
                        { 
                            atxmlw_ErrorResponse(s_Resource, Response, BufferSize, "GetStmtInfo",
                                    ATXMLW_WRAPPER_ERRCD_RANGE_ERROR,
                                    atxmlw_FmtMsg(ATXMLW_WRAPPER_ERRMSG_RANGE_ERROR ": voltage %lf outside [1-90]",
                                    m_VoltageOut[s_Chan].Real));
                            return (ATXMLW_WRAPPER_ERRCD_RANGE_ERROR);
                        }
                    }
                    if (ISSTRATTR("Sync", &TempPtr))
                    {
                        ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]",
                                Name, TempPtr), Response, BufferSize);
                        if ((atxmlw_Get1641StdTrigChar(m_SignalDescription, TempPtr, InputNames, &m_TrigInfo[s_Chan])))
                            ATXMLW_DEBUG(5, atxmlw_FmtMsg("WrapGSI-Found Trig True for [%s]", TempPtr),
                                    Response, BufferSize);
                    }
                }
            }

        }
        else
        {
            // Unknown name ??
            ATXMLW_DEBUG(1, atxmlw_FmtMsg("WrapGSI-Error finding Signal Chars for [%s]", Name),
                        Response, BufferSize);
            return (1);
        }

        // find the next signal name or port name
        if (ISSTRATTR("In", &TempPtr))
        {
            RetVal = GetSignalCond(TempPtr, InputNames, Response, BufferSize);
        }
    }

    return (RetVal);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetResourceInfo(Response, BufferSize)
//
// Purpose: Set s_Resource, s_App, s_Chan and s_*Ptr based on IssueSignal received
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
void CSrs_Na65CS4_T::GetResourceInfo(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char *NextName;

    // get resource name
    if (atxmlw_Get1641SignalResource(m_SignalDescription, s_Resource, Response, BufferSize) == 0)
    {
        // resource name in the form {baseName}_{DS|SD}{chan}
        NextName = strrchr(s_Resource, '_') + 1;
        s_App = (*NextName == 'D' ? STIMULUS : MEASUREMENT);
        NextName += 2;
        s_Chan = atoi(NextName);
    }

    if (s_App == STIMULUS)
    {
        s_ExtRefPtr  = &(m_ExtRefSim[s_Chan]);
        s_RefVoltPtr = &(m_RefVoltSim[s_Chan]);
        s_FreqPtr    = &(m_FreqSim[s_Chan]);
        s_RatioPtr   = &(m_RatioSim[s_Chan]);
    }
    else if (s_App == MEASUREMENT)
    {
        s_ExtRefPtr  = &(m_ExtRefInd[s_Chan]);
        s_RefVoltPtr = &(m_RefVoltInd[s_Chan]);
        s_FreqPtr    = &(m_FreqInd[s_Chan]);
        s_RatioPtr   = &(m_RatioInd[s_Chan]);
    }
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupChan()
//
// Purpose: Perform the Setup for this channel
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
int CSrs_Na65CS4_T::SetupChan(int Chan, char *App, char *Mode, bool Coarse,
        ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    int   Ch = Chan + (Coarse ? 0 : 1);
    char  Dummy[100] = "";
  	ViUInt32 writeCount;

    if (*s_ExtRefPtr)
    {
        sprintf(Dummy, "%s%d REF_SOURCE EXT" CRLF, App, Ch);
	    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	    if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);

		if (s_App == STIMULUS && s_RefVoltPtr->Exists)
        {
		    sprintf(Dummy, "DSH%d REF_VOLT_IN %.4lf", Ch, s_RefVoltPtr->Real);
        }
    }
    else    // internal reference
    {
        if (Coarse)
        {
            if (s_RefVoltPtr->Exists) 
	        {
                sprintf(Dummy, "REF_GEN1 VOLT %.4lf" CRLF, s_RefVoltPtr->Real);
		        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		        if (Status)
                    ErrorSrs_Na65CS4(Status, Response, BufferSize);
	        }

            if (s_FreqPtr->Exists)
            {
                sprintf(Dummy, "REF_GEN1 FREQ %.4lf" CRLF, s_FreqPtr->Real);
		        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		        if (Status)
                    ErrorSrs_Na65CS4(Status, Response, BufferSize);
	        }
        }

		sprintf(Dummy, "REF_GEN1 STATE CLOSE" CRLF);
		ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);

        sprintf(Dummy, "%s%d REF_SOURCE INT" CRLF, App, Ch);
	    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	    if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);
    }

	//Sleep(3000);

    sprintf(Dummy, "%s%d MODE %s" CRLF, App, Ch, Mode);
	ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
	IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
	if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);

	// set modifier values

	if (s_App == STIMULUS)
	{
	    if (m_VoltageOut[Chan].Exists)
	    {
		    sprintf(Dummy, "DSH%d VLL_VOLT %.4lf" CRLF, Ch, m_VoltageOut[Chan].Real); 
		    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		    if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);
		    //ErrorSrs_Na65CS4(0, Response, BufferSize);
	    }

        if (Coarse) {
		    if (m_Angle[Chan].Exists) 
		    {
			    sprintf(Dummy, "DSH%d ANGLE %.4lf" CRLF, Ch, m_Angle[Chan].Real); 
		        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		        if (Status)
                    ErrorSrs_Na65CS4(Status, Response, BufferSize);
		        //ErrorSrs_Na65CS4(0, Response, BufferSize);
		    }

		    if (m_AngleRate[Chan].Exists) 
		    {
		        Sleep(500);
			    sprintf(Dummy, "DSH%d ROT_RATE %.4lf" CRLF, Ch, m_AngleRate[Chan].Real); 
		        ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		        IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		        if (Status)
                    ErrorSrs_Na65CS4(Status, Response, BufferSize);
		        //ErrorSrs_Na65CS4(0, Response, BufferSize);
		    }
        }

        if (m_TrigInfo[Chan].TrigExists)
        {
            sprintf(Dummy, "DSH%d TRIG_SOURCE %s" CRLF, Ch, m_TrigInfo[Chan].TrigPort);
		    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		    if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);

            sprintf(Dummy, "DSH%d TRIG_SLOPE %s" CRLF, Ch, m_TrigInfo[Chan].TrigSlopePos ? "POS" : "NEG");
 		    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		    IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		    if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);
        }
	}

    /* MAXT command isn't really what's meant by ATLAS MAX-TIME
    if (s_App == MEASUREMENT)
	{
		if (m_MeasInfo[Chan].TimeOut.Exists)
		{
			dMaxT = m_MeasInfo[Chan].TimeOut.Real;
		    sprintf(Dummy, "SDH%d MAXT %.4lf" CRLF, Ch, dMaxT); 
		    ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		    IFNSIM(Status = viWrite (m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		    if (Status)
                ErrorSrs_Na65CS4(Status, Response, BufferSize);
       	}
	}
    /* MAXT command... */

    if (!Coarse)
    {
        sprintf(Dummy, "%s%d RATIO %d\r\n", App, Ch, s_RatioPtr->Int);
		ATXMLW_DEBUG(5, Dummy, Response, BufferSize);
		IFNSIM(Status = viWrite(m_Handle, (ViBuf)Dummy, strlen(Dummy), &writeCount));
		if (Status)
            ErrorSrs_Na65CS4(Status, Response, BufferSize);
    }
	
    if (Status)
        ErrorSrs_Na65CS4(Status, Response, BufferSize);

    return (0);
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
void s_IssueDriverFunctionCallDeviceXxx(int DeviceHandle, ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
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



