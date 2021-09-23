//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    TerM9_T.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXMLW Instrument Driver for TerM9
//
// Instrument:	TerM9  <Device Description> (<device Type>)
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
// 1.4.0.1  13AUG07  Added code so wrapper would not get and use handle to the dwg                        
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "visa.h"
#include "terM9.h"
#include "AtxmlWrapper.h"
#include "TerM9_T.h"

// Local Defines
#define NOHANDLE  //Stops the wrapper from aquiring and using a handle

#define IFHNDL(x) { \
                    if(m_Handle) { x ;} \
                  }
// Function codes

//////// Place TerM9 specific data here //////////////
//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CTerM9_T
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

CTerM9_T::CTerM9_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;
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

    InitPrivateTerM9();
	NullCalDataTerM9();

    // The Form Init String
	sprintf(m_InitString, "TERM::#%d",m_AddressInfo.PrimaryAddress);

    sprintf(LclMsg,"Wrap-CTerM9 Class called with Instno %d, Sim %d, Dbg %d", 
                                      m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

#ifndef NOHANDLE
    // Initialize the TerM9
    IFNSIM(Status = terM9_init(m_InitString, VI_FALSE, VI_TRUE, (ViPSession)&m_Handle));

    ATXMLW_DEBUG(10,atxmlw_FmtMsg("terM9_init(%s, VI_FALSE, VI_TRUE, &m_Handle)", m_InitString),
                     Response,Buffersize);
    if(Status && ErrorTerM9(Status, Response, Buffersize))
        return;
#endif

	Sleep(2000);
    IFNSIM(IFHNDL(Status = terM9_reset(m_Handle)));
	ATXMLW_DEBUG(10,atxmlw_FmtMsg("terM9_reset(%d)",m_Handle),Response,Buffersize);

    if(Status && ErrorTerM9(Status, Response, Buffersize))
        return;

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CTerM9_T()
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
CTerM9_T::~CTerM9_T()
{
    int Status = 0;
    // Reset the TerM9
    IFNSIM(IFHNDL(Status = terM9_reset(m_Handle)));
    IFNSIM(IFHNDL(Status = terM9_close(m_Handle)));

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusTerM9
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
int CTerM9_T::StatusTerM9(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the TerM9
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusTerM9 called "), Response, BufferSize);
    // Check for any pending error messages
    Status = ErrorTerM9(0, Response, BufferSize);

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalTerM9
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
int CTerM9_T::IssueSignalTerM9(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the TerM9
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalTerM9 Signal: "),
                              Response, BufferSize);

    // Ignore
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalTerM9
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
int CTerM9_T::RegCalTerM9(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    // Setup action for the TerM9
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalTerM9 CalData: %s", 
                               CalData),Dummy,1024);

    // Ignore

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetTerM9
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
int CTerM9_T::ResetTerM9(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the TerM9
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetTerM9 called "), Response, BufferSize);
    // Reset the TerM9
    IFNSIM(IFHNDL(Status = terM9_reset(m_Handle)));
	ATXMLW_DEBUG(10,atxmlw_FmtMsg("terM9_reset(%d)",m_Handle),Response,BufferSize);
    if(Status)
        ErrorTerM9(Status, Response, BufferSize);

    InitPrivateTerM9();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstTerM9
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
int CTerM9_T::IstTerM9(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the TerM9
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstTerM9 called Level %d", 
                              Level), Response, BufferSize);
    // Reset the TerM9
    //////// Place TerM9 specific data here //////////////
    switch(Level)
    {
    case ATXMLW_IST_LVL_PST:
        Status = StatusTerM9(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_IST:
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = TerM9_IST(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    case ATXMLW_IST_LVL_CNF:
        Status = StatusTerM9(Response,BufferSize);
        break;
    default: // Hopefully BIT 1-9
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = TerM9_BIT(m_Handle, (Level-ATXMLW_IST_LVL_BIT))));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    }
/* Begin TEMPLATE_SAMPLE_CODE 
    IFNSIM((Status = TerM9_PnPreset(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    if(Status)
        ErrorTerM9(Status, Response, BufferSize);

    InitPrivateTerM9();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsTerM9
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
int CTerM9_T::IssueNativeCmdsTerM9(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the TerM9
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsTerM9 "), Response, BufferSize);

    // Ignore
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallTerM9
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
int CTerM9_T::IssueDriverFunctionCallTerM9(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the TerM9
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallTerM9", Response, BufferSize);

    // Ignore
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////
// Function: ErrorTerM9
//
// Purpose: Query TerM9 for the error text and send to WRTS
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
int  CTerM9_T::ErrorTerM9(int Status,
                                  ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     Msg[MAX_MSG_SIZE];

    Msg[0] = '\0';

    if(Status)
    {
        strcpy(Msg,"Unknown error with wrapper/instrument communications");
        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
                           Status, Msg);

    }


    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateTerM9
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
void CTerM9_T::InitPrivateTerM9(void)
{
    //////// Place TerM9 specific data here //////////////
    m_DcComp.Exists = false;
    m_Periodic.Exists = false;
    m_RiseTime.Exists = false;
    m_FallTime.Exists = false;
    m_PulseWidth.Exists = false;
    m_Freq.Exists = false;
    m_TEI.Exists = false;
    m_HighPassFilter.Exists = false;
    m_Attenuator.Exists = false;

    memset(&m_TrigInfo,0,sizeof(m_TrigInfo));
    memset(&m_GateInfo,0,sizeof(m_GateInfo));
    memset(&m_MeasInfo,0,sizeof(m_MeasInfo));

    //////////////////////////////////////////////////////////
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataTerM9
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
void CTerM9_T::NullCalDataTerM9(void)
{
    //////// Place TerM9 specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE */
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    return;
}



//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


