//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlSwx_Ri1260_T.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXMLW Instrument Driver for Swx_Ri1260
//
// Instrument:	Swx_Ri1260  Switch Device (Switch)
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
// 1.0.0.0  07JAN06  Baseline Release (Drop 2)
// 1.0.0.1  30JAN06  Update Interface methods & cleanup (Drop 3)
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "AtxmlWrapper.h"
#include "AtXmlSwx_Ri1260_T.h"
#include "ri1260.h"  

// Local Defines

//////// Place Swx_Ri1260 specific data here //////////////

//////////////////////////////////////////////////////////

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables

// Local Function Prototypes
static void s_IssueDriverFunctionCallDeviceXxx(int Handle, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CSwx_Ri1260_T
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

CSwx_Ri1260_T::CSwx_Ri1260_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
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
            strnzcpy(m_AddressInfo.ControllerType, AddressInfoPtr->ControllerType, ATXMLW_MAX_NAME);
        }
        
		m_AddressInfo.ControllerNumber = AddressInfoPtr->ControllerNumber;
        m_AddressInfo.PrimaryAddress = AddressInfoPtr->PrimaryAddress;
        m_AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
        m_AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
    }


    m_Handle= NULL;
    m_InitString[0] = '\0';
    m_SigResourceName[0] = '\0';
    m_PawsPath.StringPtr = NULL;

    InitPrivateSwx_Ri1260();
	NullCalDataSwx_Ri1260();

    // The Form Init String
    sprintf(m_InitString,"%s%d::%d::INSTR", m_AddressInfo.ControllerType,m_AddressInfo.ControllerNumber, m_AddressInfo.PrimaryAddress);
    sprintf(LclMsg,"Wrap-CSwx_Ri1260 Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

    // Initialize the Swx_Ri1260
    IFNSIM(Status = ri1260_init(m_InitString, false, false, (ViPSession)(&m_Handle)));

    if(Status && ErrorSwx_Ri1260(Status, Response, Buffersize))
	{
        return;
	}

	SanitizeRi1260();

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CSwx_Ri1260_T()
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
CSwx_Ri1260_T::~CSwx_Ri1260_T()
{
    char Dummy[1024];

    // Reset the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CSwx_Ri1260 Class Distructor called "),Dummy,1024);

    // Reset the Swx_Ri1260
    IFNSIM(ri1260_close(m_Handle));

    InitPrivateSwx_Ri1260();
    //////////////////////////////////////////////////////////

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusSwx_Ri1260
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
int CSwx_Ri1260_T::StatusSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusSwx_Ri1260 called "), Response, BufferSize);
    
	// Check for any pending error messages
    Status = ErrorSwx_Ri1260(0, Response, BufferSize);

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalSwx_Ri1260
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
int CSwx_Ri1260_T::IssueSignalSwx_Ri1260(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalSwx_Ri1260 Signal: "), Response, BufferSize);

    if((Status = GetStmtInfoSwx_Ri1260(SignalDescription, Response, BufferSize)) != 0)
	{
        return(Status);
	}

    switch(m_Action)
    {
    case ATXMLW_SA_STATUS:
        if((Status = StatusSwx_Ri1260(Response, BufferSize)) != 0)
		{
            return(Status);
		}
        break;

    case ATXMLW_SA_RESET:
        if((Status = ResetSwx_Ri1260(Response, BufferSize)) != 0)
		{
            return(Status);
		}
        break;

    case ATXMLW_SA_CONNECT:
        if((Status = ConnectSwx_Ri1260(Response, BufferSize)) != 0)
		{
            return(Status);
		}
        break;

    case ATXMLW_SA_DISCONNECT:
        if((Status = DisconnectSwx_Ri1260(Response, BufferSize)) != 0)
		{
            return(Status);
		}
        break;

    default:
        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Wrapper Call ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, ATXMLW_WRAPPER_ERRMSG_INVALID_ACTION);
        break;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalSwx_Ri1260
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
int CSwx_Ri1260_T::RegCalSwx_Ri1260(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    // Setup action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalSwx_Ri1260 CalData: %s", CalData),Dummy,1024);

    // Retrieve the CalData

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetSwx_Ri1260
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
int CSwx_Ri1260_T::ResetSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetSwx_Ri1260 called "), Response, BufferSize);
    
	// Reset the Swx_Ri1260
    IFNSIM((Status = ri1260_reset(m_Handle)));

	// Interconnecting Channels
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,1,0,1)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,2,0,2)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,3,0,0)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,3,0,1)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,4,0,0)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,4,0,1)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,6,0,0)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,6,0,1)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,7,0,0)));
	IFNSIM((Status = ri1260_38_operate_single(m_Handle,3,1,7,0,1))); 

    if(Status)
	{
        ErrorSwx_Ri1260(Status, Response, BufferSize);
	}

    InitPrivateSwx_Ri1260();

	SanitizeRi1260();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstSwx_Ri1260
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
int CSwx_Ri1260_T::IstSwx_Ri1260(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    short PassFail = 1;//Pass Default For Simulation
    char  Msg[MAX_MSG_SIZE];

    Msg[0] = '\0';

    // Reset action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstSwx_Ri1260 called Level %d", Level), Response, BufferSize);
    
	// Reset the Swx_Ri1260
    switch(Level)
    {
    case ATXMLW_IST_LVL_PST:
        Status = StatusSwx_Ri1260(Response,BufferSize);
        break;

    case ATXMLW_IST_LVL_IST:
        IFNSIM(PassFail = 0);
        IFNSIM((Status = ri1260_self_test(m_Handle,(ViPInt16)(&PassFail),Msg)));
        
		if(Status == 0)
        {
            atxmlw_ScalerIntegerReturn("IST_PassFail", "", PassFail, Response, BufferSize);
            atxmlw_ScalerStringReturn("IST_Message", "", Msg, Response, BufferSize);
        }
        break;

    case ATXMLW_IST_LVL_CNF:
        Status = StatusSwx_Ri1260(Response,BufferSize);
        break;

    default: // Hopefully BIT 1-9
        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Wrapper IST ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, ATXMLW_WRAPPER_ERRMSG_INVALID_ACTION);
        break;
    }

    if(Status)
	{
        ErrorSwx_Ri1260(Status, Response, BufferSize);
	}

    IFNSIM((Status = ri1260_reset(m_Handle)));
    
	if(Status)
	{
        ErrorSwx_Ri1260(Status, Response, BufferSize);
	}

    InitPrivateSwx_Ri1260();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsSwx_Ri1260
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
int CSwx_Ri1260_T::IssueNativeCmdsSwx_Ri1260(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsSwx_Ri1260 "), Response, BufferSize);

    // Issue Instrument Commands
    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }

	if (strstr(InstrumentCmds, "*RST") != NULL)
	{
		SanitizeRi1260(); 
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallSwx_Ri1260
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
int CSwx_Ri1260_T::IssueDriverFunctionCallSwx_Ri1260(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Swx_Ri1260
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallSwx_Ri1260", Response, BufferSize);

	// Retrieve the Parameters
    IFNSIM(s_IssueDriverFunctionCallDeviceXxx(m_Handle, DriverFunction, Response, BufferSize));

    // Retrieve the CalData
    /* Begin TEMPLATE_SAMPLE_CODE 
    if((Status = atxmlw_IssueDriverFunctionCallSwx_Ri1260(m_Handle, DriverFunction, Response, BufferSize)))
    {
        return(Status);
    }
    /* End TEMPLATE_SAMPLE_CODE */

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ConnectSwx_Ri1260
//
// Purpose: Perform the Connect action for the switch driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
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
int CSwx_Ri1260_T::ConnectSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      Status = 0;
    int      idx;

    // Connect action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ConnSwx_Ri1260 called "), Response, BufferSize);

    if(Status)
	{
        return(Status);
	}

    for(idx = 0; idx < m_SwitchCount; idx++)
    {
        switch(m_SwitchInfo[idx].SwitchType)
        {
        case SWX_TYPE_RI1260_38:
            IFNSIM((Status = ri1260_38_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             1,
                             m_SwitchInfo[idx].InterConSelect,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        case SWX_TYPE_RI1260_39:
            IFNSIM((Status = ri1260_39_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             1,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        case SWX_TYPE_RI1260_58:
            IFNSIM((Status = ri1260_58_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             1,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        case SWX_TYPE_RI1260_66:
            IFNSIM((Status = ri1260_66_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             1,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        default:
            atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Wrapper Connect ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, ATXMLW_WRAPPER_ERRMSG_INVALID_ACTION);
        }
    }

    if(Status)
	{
        ErrorSwx_Ri1260(Status, Response, BufferSize);
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: DisconnectSwx_Ri1260
//
// Purpose: Perform the Connect action for the switch driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
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
int CSwx_Ri1260_T::DisconnectSwx_Ri1260(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      Status = 0;
    int      idx;

    // Connect action for the Swx_Ri1260
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-DisconnSwx_Ri1260 called "), Response, BufferSize);

    if(Status)
	{
        return(Status);
	}

    for(idx = 0; idx < m_SwitchCount; idx++)
    {
        switch(m_SwitchInfo[idx].SwitchType)
        {
        case SWX_TYPE_RI1260_38:
            IFNSIM((Status = ri1260_38_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             0,
                             m_SwitchInfo[idx].InterConSelect,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        case SWX_TYPE_RI1260_39:
            IFNSIM((Status = ri1260_39_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             0,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        case SWX_TYPE_RI1260_58:
            IFNSIM((Status = ri1260_58_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             0,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        case SWX_TYPE_RI1260_66:
            IFNSIM((Status = ri1260_66_operate_single(m_Handle, 
                             m_SwitchInfo[idx].ModAddress,
                             0,
                             m_SwitchInfo[idx].Mux_RelayType_SwitchNo,
                             m_SwitchInfo[idx].Chan_RelNo_Pole)));
            break;

        default:
            atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Wrapper Disconnect ",
                            ATXMLW_WRAPPER_ERRCD_INVALID_ACTION,
                            ATXMLW_WRAPPER_ERRMSG_INVALID_ACTION);
        }
    }

    if(Status)
	{
        ErrorSwx_Ri1260(Status, Response, BufferSize);
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorSwx_Ri1260
//
// Purpose: Query Swx_Ri1260 for the error text and send to WRTS
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
int  CSwx_Ri1260_T::ErrorSwx_Ri1260(int Status,
                                  ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
    int      QError;

    retval = Status;
    Msg[0] = '\0';

    if(Status)
    {
        // Decode Swx_Ri1260 lib return code
        IFNSIM((Err = ri1260_error_message(m_Handle, Status, Msg)));
        if(Err)
		{
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
		}

        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);

    }

    QError = 1;

    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;

        IFNSIM((Err = ri1260_error_query(m_Handle, (ViPInt32)(&QError), Msg)));
        
		if(Err != 0)
		{
            break;
		}

        if(QError)
        {
            atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", QError, Msg);
        }
    }
	
    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoSwx_Ri1260
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
int CSwx_Ri1260_T::GetStmtInfoSwx_Ri1260(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
    char   LclSignalName[ATXMLW_MAX_NAME];
    char   LclElement[ATXMLW_MAX_NAME];
    char  *LclCharValuePtr;


    //////// Place Swx_Ri1260 specific data here //////////////
/*
	<stdbsc:Signal Out="SHORTx"  >
		<Connection name="SHORTx" path="12,13,1500;1,2,3"/>
	</stdbsc:Signal>
*/
    //FIXStatus = s_GetAction(SignalDescription, &m_Action);
    //FIXStatus = s_GetResource(SignalDescription, &m_SigResourceName);
/* Begin TEMPLATE_SAMPLE_CODE */

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, m_SigResourceName, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", m_SigResourceName), Response, BufferSize);

    if((atxmlw_Get1641SignalOut(m_SignalDescription, LclSignalName, LclElement))==0)
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", LclSignalName, LclElement), Response, BufferSize);
	}

    if((strcmp(m_SigResourceName,"PAWS_SWITCH")==0) && (strcmp(LclElement,"Connection")==0))
    {
        if(atxmlw_Get1641StringAttribute(m_SignalDescription, LclSignalName, "path", &LclCharValuePtr))
        {
            ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]", LclSignalName, LclCharValuePtr), Response, BufferSize);
            m_PawsPath.Exists = true;
            m_PawsPath.StringPtr = new char[((int)strlen(LclCharValuePtr))+20];
            
			if(m_PawsPath.StringPtr)
			{
                strcpy(m_PawsPath.StringPtr,LclCharValuePtr);
			}
        }
    }
    
	/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    atxmlw_Close1641Xml(&m_SignalDescription);

    m_SwitchCount = 0; // clear out old swx paths
    Status = GetSwitchInfo(Response,BufferSize); // Uses saved path strings e.g. m_PawsPath

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetSwitchInfo
//
// Purpose: Perform the switch table lookup
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Response         ATXMLW_INTF_RESPONSE*  Return any error codes and messages
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSwx_Ri1260_T::GetSwitchInfo(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    int   Block, Mod, Path;
    char *PathPtr;

    if((strcmp(m_SigResourceName,"PAWS_SWITCH") == 0) && m_PawsPath.Exists)
    {
        PathPtr = m_PawsPath.StringPtr;

        while(PathPtr && (sscanf(PathPtr,"%d,%d,%d",&Block,&Mod,&Path) == 3))
        {
            if(m_SwitchCount >= MAX_SWITCH_PATHS)
			{
                break;
			}

            m_SwitchInfo[m_SwitchCount].ModAddress = Block;
            m_SwitchInfo[m_SwitchCount].InterConSelect = 0;
            m_SwitchInfo[m_SwitchCount].Mux_RelayType_SwitchNo = Mod;
            m_SwitchInfo[m_SwitchCount].Chan_RelNo_Pole = Path;
            
			switch(Block)
            {
            case 1:
            case 2:
                m_SwitchInfo[m_SwitchCount].SwitchType = SWX_TYPE_RI1260_39;
                break;

            case 3:
                m_SwitchInfo[m_SwitchCount].SwitchType = SWX_TYPE_RI1260_38;
                break;

            case 4:
                m_SwitchInfo[m_SwitchCount].SwitchType = SWX_TYPE_RI1260_58;
                break;

            case 5:
                m_SwitchInfo[m_SwitchCount].SwitchType = SWX_TYPE_RI1260_66;
                break;

            default:
                m_SwitchInfo[m_SwitchCount].SwitchType = 0;
            }

            m_SwitchCount++;
            
			if((PathPtr = strstr(PathPtr,";")))
			{
                PathPtr++; // Skip ';'
			}
        }
    }
    // elseif Point to Point switching
	else if((m_Action != ATXMLW_SA_RESET) && (m_Action != ATXMLW_SA_STATUS))
     {
        // Bad Resource Name
        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Wrapper - SwitchPath ", ATXMLW_WRAPPER_ERRCD_INVALID_SYSTEM_DATA, ATXMLW_WRAPPER_ERRMSG_INVALID_SYSTEM_DATA);
    }

    if(Status)
	{
        ErrorSwx_Ri1260(Status, Response, BufferSize);
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateSwx_Ri1260
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
void CSwx_Ri1260_T::InitPrivateSwx_Ri1260(void)
{
    m_Action = 0;
    m_SigResourceName[0] = '\0';
    
	if(m_PawsPath.StringPtr)
	{
        delete(m_PawsPath.StringPtr);
	}

    m_PawsPath.StringPtr = NULL;
    m_PawsPath.Exists = false;
    m_SwitchCount = 0;
    
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataSwx_Ri1260
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
void CSwx_Ri1260_T::NullCalDataSwx_Ri1260(void)
{
    //////// Place Swx_Ri1260 specific data here //////////////
    //////////////////////////////////////////////////////////
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: SanitizeRi1260
//
// Sets instrument to lowest stim and highest impedance settings. Opens all output relays
//
//
///////////////////////////////////////////////////////////////////////////////
void CSwx_Ri1260_T::SanitizeRi1260(void)
{
	
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


