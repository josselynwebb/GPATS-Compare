////////////////////////////////////////////////////////////////////////////////
// File:	    HPE1428A_T.cpp
//
// Date:	    05APR06
//
// Purpose:	    ATXMLW Instrument Driver for HPE1428A
//
// Instrument:	HPE1428A  <Device Description> (<device Type>)
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
// 1.0.0.0  05APR06  Baseline Release						  D. Bubenik, EADS North America Defense
// 1.2.0.1  04JAN07	 Added 200mS delay in initScope()		  M. Hendricksen, EADS North America Defense
//					  before the capture of the signal
//					  and after the capture of signal
//					  which is before the READ.  Customer
//					  recommended.
// 1.2.1.1 23FEB07  Corrected code in scope setup to change   M. Hendricksen EADS North America Defense 
//                      the amplitude to full-scale, before 
//                      setting the vertical parameters.
//                  Corrected code in scope_setup() to treat
//                   a pulsed dc measurment same as dc measurment
//                   when setting the amplitude.
// 1.2.1.2 12Mar07  Corrected code in scope_setup(), for DC	  M. Hendricksen, EADS
//						and Pulsed DC to only not the voltage to
//						the m_Amplitude.Real and not the offset.
// 1.2.1.3 12Mar07  Corrected code in scope_setup()           M.Hendricksen, EADS
//                      to only use offset for amplitude if
//                      the signal is DC.
// 1.2.1.4 23Mar07  Added code to set the max-time in         M.Hendricksen, EADS
//                      function SetupHPE1428A, without it being
//                      set digitizing the waveform would time-out.
// 1.4.0.1 02Jul07  Increased maximum return data array       D. Bubenik, EADS
//                      size to 8000. PCR 167
// 1.4.0.2 06Sep07  Changed the scope reference from center   D. Bubenik, EADS
//                      to left. PCR 167
// 1.4.0.3 23MAR11  Corrected retrieval of maxtime value      E. Larson, EADS
// 1.4.0.4 14MAY11  Corrected sample time for waveform        E. Larson, EADS
// 1.4.0.5 17MAY11  Corrected for floating point error        E. Larson, EADS
// 1.4.0.6 18MAY11  Corrected format for invalid variable     E. Larson, EADS
////////////////////////////////////////////////////////////////////////////////
// Includes
#include "visa.h"
#include "AtxmlWrapper.h"
#include "AtxmlHPE1428A_T.h"
#include "HPE1428A.H"

//from legacy
ViString	Out_Buf, Mea_Buf;
ViStatus	err;
char		fstring[80]; 

#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024
#define SQRT_2         1.414213562373 //As defined
#define SQRT_3         1.73205080808 //As defined

int	DE_BUG = 0;
FILE *debugfp = 0;
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////


///////////////////////////////////////////////////////////////////////////////
// Function: CHPE1428A_T
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
CHPE1428A_T::CHPE1428A_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;
    unsigned int attrValue = 0;
    ViUInt32 retCnt  = 0;
    char cstr[256] = {""};

	memset(LclMsg, '\0', sizeof(LclMsg));
	ISDODEBUG(dodebug(0, "CHPE1428A_T", "Entering function", (char*) NULL));
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
            strnzcpy(m_AddressInfo.InstrumentQueryID, AddressInfoPtr->InstrumentQueryID, ATXMLW_MAX_NAME);
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
	if (m_Sim)
	{
		m_Handle = 1;
	}

    m_InitString[0] = '\0';

    InitPrivateHPE1428A();
	NullCalDataHPE1428A();

    // The Form Init String
    sprintf(m_InitString,"%s%d::%d::INSTR", m_AddressInfo.ControllerType,m_AddressInfo.ControllerNumber, m_AddressInfo.PrimaryAddress);
    sprintf(LclMsg,"Wrap-CHPE1428A Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);
	
	ISDODEBUG(dodebug(0, "CHPE1428A_T", "Wrap-CHPE1428A Class called with Instno %d, Sim %d, Dbg %d",m_InstNo, m_Sim, m_Dbg, (char*) NULL));
    // Initialize the HPE1428A
	IFNSIM(Status = hpe1428a_init(m_InitString, true, true, &m_Handle));
	ATXMLW_DEBUG(9,atxmlw_FmtMsg("HPE1428A_init_with_options(%s, false, true, %d)", m_InitString, m_Handle), Response, Buffersize);
	
	ISDODEBUG(dodebug(0, "CHPE1428A_T", "HPE1428A_init_with_options(%s, false, true, %d)", m_InitString, m_Handle, (char*) NULL));
    //set vme address space to for waveform retrieval
    IFNSIM(Status = viGetAttribute(m_Handle, VI_ATTR_MEM_BASE, &attrValue));
	ISDODEBUG(dodebug(0, "CHPE1428A_T", "Status returning from viGetAttribute() is [%d]", Status, (char*) NULL));
    
    sprintf(cstr, "MEM:VME:ADDR #H%x\n", attrValue); 

    IFNSIM(Status = viWrite(m_Handle, (ViBuf)cstr, (sizeof(cstr)/sizeof(char)), &retCnt));
	
	ISDODEBUG(dodebug(0, "CHPE1428A_T", "Status returning from viWrite() is [%d]", Status, (char*) NULL));

	ISDODEBUG(dodebug(0, "CHPE1428A_T", "Leaving function", (char*) NULL));
    if(Status && ErrorHPE1428A(Status, Response, Buffersize))
	{
        return;
	}
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CHPE1428A_T()
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
CHPE1428A_T::~CHPE1428A_T()
{
    char Dummy[1024];
	
	ISDODEBUG(dodebug(0, "~CHPE1428A_T", "Entering function", (char*) NULL));
    // Reset the HPE1428A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CHPE1428A Class Destructor called "),Dummy,1024);

    // Reset the HPE1428A
    IFNSIM(hpe1428a_reset(m_Handle));
	// Release HPE1428A
	IFNSIM(hpe1428a_close(m_Handle));
	
	ISDODEBUG(dodebug(0, "~CHPE1428A_T", "Leaving function", (char*) NULL));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusHPE1428A
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
int CHPE1428A_T::StatusHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
	
	ISDODEBUG(dodebug(0, "StatusHPE1428A", "Entering function", (char*) NULL));
    // Status action for the HPE1428A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusHPE1428A called "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "StatusHPE1428A", "Wrap-StatusHPE1428A called with Response {%s}", Response, (char*) NULL));
    // Check for any pending error messages
    Status = ErrorHPE1428A(0, Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "StatusHPE1428A", "Status after return from ErrorHPE1428A is [%d]", Status, (char*) NULL));
	ISDODEBUG(dodebug(0, "StatusHPE1428A", "Leaving function", (char*) NULL));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ResetHPE1428A
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
int CHPE1428A_T::ResetHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char cstr[256] = {""};
    int   Status = 0;
    unsigned int attrValue = 0;
    ViUInt32 retCnt  = 0;
	
	ISDODEBUG(dodebug(0, "ResetHPE1428A", "Entering function", (char*) NULL));
    // Reset action for the HPE1428A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetHPE1428A called "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "ResetHPE1428A", "Wrap-ResetHPE1428A called with Response {%s}",Response ,  (char*) NULL));
    // Reset the HPE1428A
    IFNSIM((Status = hpe1428a_reset (m_Handle)));

    //set vme address space to for waveform retrieval
    IFNSIM(Status=viGetAttribute(m_Handle, VI_ATTR_MEM_BASE, &attrValue));
    
	ISDODEBUG(dodebug(0, "ResetHPE1428A", "Wrap-ResetHPE1428A called with Response {%d}", Status, (char*) NULL));
    sprintf(cstr, "MEM:VME:ADDR #H%x\n", attrValue); 

    IFNSIM(Status=viWrite(m_Handle, (ViBuf)cstr, (sizeof(cstr)/sizeof(char)), &retCnt));

    if(Status)
	{
        ErrorHPE1428A(Status, Response, BufferSize);
	}

    InitPrivateHPE1428A();
	
	ISDODEBUG(dodebug(0, "ResetHPE1428A", "Leaving function", (char*) NULL));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: HPE1428A
//
// Purpose: Query HPE1428A for the error text and send to WRTS
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
int  CHPE1428A_T::ErrorHPE1428A(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
	ViInt32	 BytesRead = 25;
	ViChar   errorMessage[512] = "";
	ViInt32      errorCode = 0;
	char     hp1428_string[256]="";
	
	ISDODEBUG(dodebug(0, "ErrorHPE1428A", "Entering function", (char*) NULL));
    retval = Status;
    Msg[0] = '\0';

    if(Status)
    {
		IFNSIM(Err = hpe1428a_errorQuery(m_Handle, &errorCode, errorMessage));
		ATXMLW_DEBUG(9,atxmlw_FmtMsg("hp1428_error(%d, %x)",m_Handle, errorCode), Response, BufferSize);
		
		ISDODEBUG(dodebug(0, "ErrorHPE1428A", "hp1428_error(%d, %x)",m_Handle, errorCode, (char*) NULL));
        if(Err)
		{
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
		}

		// Timeout occurred before operation could complete
        if(Status == 0xBFFF0015)
		{
            Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;
			ISDODEBUG(dodebug(0, "ErrorHPE1428A", "Timeout occurred status should be 0xBFFF0015 and is {%d}", Status, (char*) NULL));
		}

		if (errorCode == -303)
		{
			Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;
			ISDODEBUG(dodebug(0, "ErrorHPE1428A", "Timeout occurred error code is -303", (char*) NULL));
			m_TimeOut = true;
		}
	}
		
    IFNSIM(errorCode = 1);
    // Retrieve any pending errors in the device
    while(errorCode)
    {
		IFNSIM(Err = hpe1428a_errorQuery(m_Handle, &errorCode, errorMessage));
		ATXMLW_DEBUG(9,atxmlw_FmtMsg("hp1428_error(%d, %x)",m_Handle, errorCode), Response, BufferSize); 
		
		ISDODEBUG(dodebug(0, "ErrorHPE1428A", "hp1428_error(%d, %x)",m_Handle, errorCode, (char*) NULL));

        if(Err != 0)
		{
            break;
		}

		if (errorCode == -303)
		{
			m_TimeOut = true;
		}

        if(errorCode < 0)
		{
			ATXMLW_ERROR(Status, "DSO 1428 Error", errorMessage, Response, BufferSize);
		
			ISDODEBUG(dodebug(0, "ErrorHPE1428A", "DSO 1428 Error [%s]", errorMessage, (char*) NULL));
		}

		else if (errorCode > 0)
		{
			ATXMLW_ERROR(Status, "DSO 1428 Warning", errorMessage, Response, BufferSize);
		
			ISDODEBUG(dodebug(0, "ErrorHPE1428A", "DSO 1428 Warning [%s]", errorMessage, (char*) NULL));
		}
    }
	
	ISDODEBUG(dodebug(0, "ErrorHPE1428A", "Leaving function", (char*) NULL));
    return(Status);
}



///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsHPE1428A
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
int CHPE1428A_T::IssueNativeCmdsHPE1428A(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;
    char cstr[256] = {""};
    unsigned int attrValue = 0;
    ViUInt32 retCnt  = 0;
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsHPE1428A", "Entering function", (char*) NULL));
    // Setup action for the HPE1428A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsHPE1428A "), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsHPE1428A", "Wrap-IssueNativeCmdsHPE1428A with Response [%s]",Response,  (char*) NULL));
	ISDODEBUG(dodebug(0, "IssueNativeCmdsHPE1428A", "Calling atxmlw_InstrumentCommands(m_Handle[%s], InstrumentCmds[%s], Response, BufferSize, m_Dbg, m_Sim", m_Handle, InstrumentCmds,   (char*) NULL));
    // Retrieve the CalData
    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }
    
    //if *RST sent, resent vme base address for waveform plotting
    if(strstr(InstrumentCmds, "*RST"))
    {
        //set vme address space to for waveform retrieval
        IFNSIM(Status=viGetAttribute(m_Handle, VI_ATTR_MEM_BASE, &attrValue));
    
        sprintf(cstr, "MEM:VME:ADDR #H%x\n", attrValue); 

        IFNSIM(Status = viWrite(m_Handle, (ViBuf)cstr, (sizeof(cstr)/sizeof(char)), &retCnt));
    }

	
	ISDODEBUG(dodebug(0, "IssueNativeCmdsHPE1428A", "Leaving function", (char*) NULL));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstZT1428VXI
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
int CHPE1428A_T::IstHPE1428A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	
	ISDODEBUG(dodebug(0, "IstHPE1428A", "Entering function", (char*) NULL));
    // Reset action for the ZT1428VXI
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstZT1428VXI called Level %d", Level), Response, BufferSize);
	
	ISDODEBUG(dodebug(0, "IstHPE1428A", "Wrap-IstZT1428VXI called Level %d", Level, (char*) NULL));
    // Reset the ZT1428VXI
    switch(Level)
    {
		case ATXMLW_IST_LVL_PST:
			Status = StatusHPE1428A(Response,BufferSize);		
			ISDODEBUG(dodebug(0, "IstHPE1428A", "In case ATXMLW_IST_LVL_PST", (char*) NULL));
			break;
		case ATXMLW_IST_LVL_IST:		
			ISDODEBUG(dodebug(0, "IstHPE1428A", "In case ATXMLW_IST_LVL_IST", (char*) NULL));
			break;
		case ATXMLW_IST_LVL_CNF:
			Status = StatusHPE1428A(Response,BufferSize);		
			ISDODEBUG(dodebug(0, "IstHPE1428A", "In case ATXMLW_IST_LVL_CNF", (char*) NULL));
			break;
		default: // Hopefully BIT 1-9		
			ISDODEBUG(dodebug(0, "IstHPE1428A", "In case default", (char*) NULL));
			break;
    }

    if(Status)
	{
        ErrorHPE1428A(Status, Response, BufferSize);
	}

    InitPrivateHPE1428A();
	
	ISDODEBUG(dodebug(0, "IstHPE1428A", "Leaving function", (char*) NULL));
    return(Status);
}



///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateHPE1428A
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
void CHPE1428A_T::InitPrivateHPE1428A(void)
{
	
	ISDODEBUG(dodebug(0, "InitPrivateHPE1428A", "Entering function", (char*) NULL));
	m_dDc_offset.Real = 0.0;
	m_dAmplitude.Real = 0.0;
	m_dFrequency.Real = 0.0;
	m_dPeriod.Real = 1000;
	m_nAcCoupling.Int = 0;
	m_dBanwidthMin.Real = 0.0;
	m_dBanwidthMax.Real = 20.0e+9;
	m_nGain.Int = 1;
	m_nImpedance.Real = 1000000;
	m_nChannel.Int = 1;
	m_nSampleCount.Int = 1;
	m_nMeasChar.Int = 1;
	//
	m_dTrigLevel.Real = 0.0;
	m_nTrigSlope.Int = 1;
	m_nTrigChannel.Int = 1;
	//
	m_dGateStartLevel.Real = 0.0;
	m_dGateStartSlope.Real = 1;
	m_dGateStartChannel.Real = 1;
	m_dGateStopLevel.Real = 0.0;
	m_dGateStopSlope.Real = 1;
	m_dGateStopChannel.Real = 1;
	//
	m_dDc_offset.Exists = false;
	m_dAmplitude.Exists = false;
	m_dFrequency.Exists = false;
	m_MaxTime.Exists=false;
	m_dPeriod.Exists = false;
	m_nAcCoupling.Exists = false;
	m_dBanwidthMin.Exists = false;
	m_dBanwidthMax.Exists = false;
	m_nGain.Exists = false;
	m_nImpedance.Exists = false;
	m_nChannel.Exists = false;
	m_nSampleCount.Exists = false;
	m_nMeasChar.Exists = false;
	//
	m_dTrigLevel.Exists = false;
	m_nTrigSlope.Exists= false;
	m_nTrigChannel.Exists = false;
	m_dGateStartLevel.Exists = false;
	m_dGateStartSlope.Exists = false;
	m_dGateStartChannel.Exists = false;
	m_dGateStopLevel.Exists = false;
	m_dGateStopSlope.Exists = false;
	m_dGateStopChannel.Exists = false;
	m_sSaveTo.Exists = false;
	m_sSaveFrom.Exists = false;
	m_sLoadFrom.Exists = false;
	m_sCompareCh.Exists = false;
	m_sCompareTo.Exists = false;
	m_nAllowance.Exists = false;
	m_sDifferentiate.Exists = false;
	m_sIntegrate.Exists = false;
	m_sMultiplyFrom.Exists = false;
	m_sMultiplyTo.Exists = false;
	m_sAddFrom.Exists = false;
	m_sAddTo.Exists = false;
    m_sSubtractFrom.Exists = false;
	m_sSubtractTo.Exists = false;
	m_sDestination.Exists = false;

	m_TrigInfo.TrigExists=false;
	m_TrigInfo.TrigDelay=0;
	m_TrigInfo.TrigExt=false;
	m_TrigInfo.TrigLevel=1.5;
	m_TrigInfo.TrigPort[0]='\0';
	m_TrigInfo.TrigSlopePos=true;

	m_PulseWidth.Exists=false;
	m_RiseTime.Exists=false;
	m_FallTime.Exists=false;

	m_dSampleTime.Exists = false;

	m_dSamples.Exists = false;
	m_dSamples.Real = 0.0;
	m_dSamples.Int = 0;

	// PCR10030
	m_TimeOut = false;
	
	ISDODEBUG(dodebug(0, "InitPrivateHPE1428A", "Leaving function", (char*) NULL));
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataHPE1428A
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
void CHPE1428A_T::NullCalDataHPE1428A(void)
{
	
	ISDODEBUG(dodebug(0, "NullCalDataHPE1428A", "Entering function", (char*) NULL));
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
	
	ISDODEBUG(dodebug(0, "NullCalDataHPE1428A", "Leaving function", (char*) NULL));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallZT1428VXI
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
int CHPE1428A_T::IssueDriverFunctionCallHPE1428A(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    ATXMLW_DF_VAL RetVal;
    ATXMLW_XML_HANDLE DfHandle=NULL;
    char Name[ATXMLW_MAX_NAME];
	
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "Entering function", (char*) NULL));
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "Driver Function [%s], Response [%s]", DriverFunction, Response, (char*) NULL));
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
    if((atxmlw_ParseDriverFunction(m_Handle, DriverFunction, &DfHandle, Response, BufferSize)) || (DfHandle == NULL))
	{
        return(0);
	}

    atxmlw_GetDFName(DfHandle,Name);
    RetVal.Int32 = 0;

    ///////// Implement Supported Driver Function Calls ///////////////////////
	if(ISDFNAME("hpe1428a_WvData"))
	{	
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "hpe1428a_WvData", (char*) NULL));
		RetVal.Int32 = hpe1428a_WvData(m_Handle, SrcFlt(2), SrcFlt(3), SrcFlt(4), SrcFlt(5), SrcUInt16(6), SrcUInt16(7), SrcUInt16(8)); 
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "hpe1428a_WvData(m_Handle, SrcFlt(%f), SrcFlt(%f), SrcFlt(%f), SrcFlt(%f), SrcUInt16(%d), SrcUInt16(%d), SrcUInt16(%d)", 
									SrcFlt(2), SrcFlt(3), SrcFlt(4), SrcFlt(5), SrcUInt16(6), SrcUInt16(7), SrcUInt16(8),(char*) NULL));
	}
	else if(ISDFNAME("hpe1428a_GtWv"))
	{	
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "hpe1428a_GtWv", (char*) NULL));
		RetVal.Int32 = hpe1428a_GtWv(RetFltPtr(2), RetFltPtr(3));
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "hpe1428a_GtWv(RetFltPtr(2), RetFltPtr(3))", RetFltPtr(2), RetFltPtr(3),(char*) NULL));
	}
	else if(ISDFNAME("hpe1428a_FindInst"))
	{	
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "hpe1428a_FindInst", (char*) NULL));
		RetVal.Int32 = hpe1428a_FindInst(m_Handle, SrcUInt32(2), SrcUInt32(3), SrcUInt32(4), SrcUInt16(5), RetUInt32Ptr(6));
	}
	else if((strncmp("vi",Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal))
	{
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "strncmp(vi,Name,2)==0) && atxmlw_doViDrvrFunc(DfHandle,Name,&RetVal)", (char*) NULL));
        int x = 0;
	}
    else
    {
        atxmlw_ErrorResponse("", Response, BufferSize, "IssueDriverFunction ", ATXMLW_WRAPPER_ERRCD_INVALID_ACTION, atxmlw_FmtMsg(" Invalid/Unimplemented Function [%s]",Name));
		ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "atxmlw_ErrorResponse()", (char*) NULL));
    }
    // return "Ret..." values and RetVal value to caller
    atxmlw_ReturnDFResponse(DfHandle,RetVal,Response,BufferSize);
    atxmlw_CloseDFXml(DfHandle);
	ISDODEBUG(dodebug(0, "IssueDriverFunctionCallHPE1428A", "Leaving function", (char*) NULL));
    return(0);
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

	if (DE_BUG == 1 && FirstRun == 0) {

		sprintf(TmpBuf, "%s", DEBUGIT_HP1428);
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


