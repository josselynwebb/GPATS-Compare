//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Ri4152A_T.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXMLW Instrument Driver for Ri4152A
//
// Instrument:	Ri4152A  <Device Description> (<device Type>)
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
// 1.2.0.1  19DEC06	 Corrected sprintf of a debug statement variable in GetStmtInfo()
//					 Corrected code in Fetch() to use the generic fetc_Q function
//						to obtain the measured value.  All other "measXxx" function
//						will default all parameters and enable auto range for the measurment
// 1.2.1.0  -------  No Change
// 1.2.1.1  22JAN07  Adjusted the ranges in Setup() to not bump up a range when right on border
// 1.2.1.2  23JAN07  Corrected code to account for the Absolute of a DC voltage in setting 
//						up the range in Setup().
// 1.2.1.3  26JAN07  Customer requested that a 800mS delay for AC Voltage be put between
//						enabling the signal and fetch.  This delay was put in Enable().
// 1.2.1.5  14FEB07  Corrected the delay in enable only to affect AC measurments.
// 1.2.1.6  14FEB07  Corrected code in Fetch() not to INIT, because this is already performed
//                      in Enable(). 
//                   Corrected code in Enable for the Trigger to not INIT until the trigger
//                      is setup.
// 1.2.1.7  20FEB07  Re-located the delay in Enable to delay on AC measurements even when
//						there is a trigger.
// 1.2.1.8  22FEB07  Re-located the INIT to after the delay in Enable().
// 1.4.0.1  04DEC08  Added sample-time (sample-width) processing for impedance
// 1.6.2.0  08MAR09  Added code to allow autoranging for RES measurements
// 1.6.2.1  27JUL09  Modified code to correct floating point rounding error when 
//                   setting SAMPLE_WIDTH      
// 1.7.0.1 31MAY16 Updated baseline for MNG , in VS2012 J. French Astronics T&S
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "visa.h"
#include "AtxmlWrapper.h"
#include "Ri4152A_T.h"
#include "ri4152a.h"
#include <math.h>

// Local Defines

// Function codes
//Attribute Strings
#define ATTRIB_VOLTS_DC          "dc_ampl"
#define ATTRIB_VOLTS_AC          "ac_ampl"
#define ATTRIB_VOLTS_AC_P        "ac_ampl_p"
#define ATTRIB_VOLTS_AC_PP       "ac_ampl_pp"
#define ATTRIB_VOLTS_AC_R        "ac_ratio"
#define ATTRIB_IMP               "resistance"
#define ATTRIB_IMP_4WIRE		 "resistance4Wire"
#define ATTRIB_FREQ_AC           "ac_freq"
#define ATTRIB_CURR_AC           "ac_current"
#define ATTRIB_CURR_DC           "dc_current"
#define ATTRIB_PERIOD_AC         "ac_period"
#define ATTRIB_VOLTS_DC_R        "dc_ratio"
#define ATTRIB_VOLTS_DC_AV       "dc_ampl_av"
#define ATTRIB_VOLTS_AC_AV       "ac_ampl_av"
#define ATTRIB_CURR_DC_AV        "dc_current_av"
#define ATTRIB_CURR_AC_AV        "ac_current_av"
#define ATTRIB_AC_COMP_DC        "dc_ac_comp"
#define ATTRIB_AC_COMP_DC_F      "dc_ac_comp_f"
#define ATTRIB_DC_OFF_AC         "ac_dc_offset"

#define ATTRIB_VOLTS_SQ          "sq_ampl"
#define ATTRIB_VOLTS_SQ_P        "sq_ampl_p"
#define ATTRIB_VOLTS_SQ_PP       "sq_ampl_pp"
#define ATTRIB_FREQ_SQ           "sq_freq"
#define ATTRIB_PERIOD_SQ         "sq_period"

#define ATTRIB_VOLTS_TR          "tr_ampl"
#define ATTRIB_VOLTS_TR_P        "tr_ampl_p"
#define ATTRIB_VOLTS_TR_PP       "tr_ampl_pp"
#define ATTRIB_FREQ_TR           "tr_freq"
#define ATTRIB_PERIOD_TR         "tr_period"

//Global Constants
#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024
#define SQRT_2         1.414213562373 //As defined in DmmSdd
#define SQRT_3         1.73205080808 //As defined in DmmSdd


// Static Variables

// Local Function Prototypes

static void s_IssueDriverFunctionCallDeviceXxx(int Handle,
                ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CRi4152A_T
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

CRi4152A_T::CRi4152A_T(int Instno, int ResourceType, char* ResourceName,
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

    InitPrivateRi4152A();
	NullCalDataRi4152A();

    // The Form Init String
    sprintf(m_InitString,"%s%d::%d::INSTR",
              m_AddressInfo.ControllerType,m_AddressInfo.ControllerNumber,
              m_AddressInfo.PrimaryAddress);
    sprintf(LclMsg,"Wrap-CRi4152A Class called with Instno %d, Sim %d, Dbg %d", 
                                      m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

    // Initialize the Ri4152A
	IFNSIM(Status = ri4152a_init(m_InitString, false,
                            true, &m_Handle));

    if(Status && ErrorRi4152A(Status, Response, Buffersize))
        return;

	SanitizeRi4152();

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CRi4152A_T()
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
CRi4152A_T::~CRi4152A_T()
{
    char Dummy[1024];

    // Reset the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CRi4152A Class Distructor called "),Dummy,1024);

    // Reset the Ri4152A
    IFNSIM(ri4152a_reset(m_Handle));
	IFNSIM(ri4152a_close(m_Handle));

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusRi4152A
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
int CRi4152A_T::StatusRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusRi4152A called "), Response, BufferSize);
    // Check for any pending error messages
    Status = ErrorRi4152A(0, Response, BufferSize);

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalRi4152A
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
int CRi4152A_T::IssueSignalRi4152A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;
	

    // IEEE 1641 Issue Signal action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalRi4152A Signal: "),
                              Response, BufferSize);

    if((Status = GetStmtInfoRi4152A(SignalDescription, Response, BufferSize)) != 0)
        return(Status);

	

    switch(m_Action)
    {
    case ATXMLW_SA_APPLY:
        if((Status = SetupRi4152A(Response, BufferSize)) != 0)
            return(Status);
        if((Status = EnableRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_REMOVE:
        if((Status = DisableRi4152A(Response, BufferSize)) != 0)
            return(Status);
        if((Status = ResetRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_MEASURE:
        if((Status = SetupRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_READ:
        if((Status = EnableRi4152A(Response, BufferSize)) != 0)
            return(Status);
        if((Status = FetchRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_RESET:
        if((Status = ResetRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_SETUP:
        if((Status = SetupRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_STATUS:
        if((Status = StatusRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_CONNECT:
        break;
    case ATXMLW_SA_ENABLE:
        if((Status = EnableRi4152A(Response, BufferSize)) != 0)
                return(Status);        
        break;
    case ATXMLW_SA_DISABLE:
        if((Status = DisableRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_FETCH:	
        if((Status = FetchRi4152A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISCONNECT:
        break;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalRi4152A
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
int CRi4152A_T::RegCalRi4152A(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    // Setup action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalRi4152A CalData: %s", 
                               CalData),Dummy,1024);

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetRi4152A
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
int CRi4152A_T::ResetRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetRi4152A called "), Response, BufferSize);
	// send a dcl
	IFNSIM(Status =  ri4152a_dcl (m_Handle));
	if(Status)
        ErrorRi4152A(Status, Response, BufferSize);

    // Reset the Ri4152A
    IFNSIM((Status = ri4152a_reset(m_Handle)));
	if(Status)
        ErrorRi4152A(Status, Response, BufferSize);

    InitPrivateRi4152A();

	SanitizeRi4152();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstRi4152A
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
int CRi4152A_T::IstRi4152A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstRi4152A called Level %d", 
                              Level), Response, BufferSize);
    // Reset the Ri4152A
    //////// Place Ri4152A specific data here //////////////
    switch(Level)
    {
    case ATXMLW_IST_LVL_PST:
        Status = StatusRi4152A(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_IST:
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = Ri4152A_IST(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    case ATXMLW_IST_LVL_CNF:
        Status = StatusRi4152A(Response,BufferSize);
        break;
    default: // Hopefully BIT 1-9
/* Begin TEMPLATE_SAMPLE_CODE 
        IFNSIM((Status = Ri4152A_BIT(m_Handle, (Level-ATXMLW_IST_LVL_BIT))));
/* End TEMPLATE_SAMPLE_CODE */
        break;
    }
/* Begin TEMPLATE_SAMPLE_CODE 
    IFNSIM((Status = Ri4152A_PnPreset(m_Handle)));
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    if(Status)
        ErrorRi4152A(Status, Response, BufferSize);

    InitPrivateRi4152A();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsRi4152A
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
int CRi4152A_T::IssueNativeCmdsRi4152A(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsRi4152A "), Response, BufferSize);

    // Retrieve the CalData
    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    {
        return(Status);
    }

	if (strstr(InstrumentCmds, "*RST") != NULL)
	{
		SanitizeRi4152(); 
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallRi4152A
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
int CRi4152A_T::IssueDriverFunctionCallRi4152A(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Ri4152A
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallRi4152A", Response, BufferSize);

	    // Retrieve the Parameters
    IFNSIM(s_IssueDriverFunctionCallDeviceXxx(m_Handle, DriverFunction,
                            Response, BufferSize));

    // Retrieve the CalData
/* Begin TEMPLATE_SAMPLE_CODE 
    if((Status = atxmlw_IssueDriverFunctionCallRi4152A(m_Handle, DriverFunction,
                                        Response, BufferSize)))
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
// Function: SetupRi4152A
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
int CRi4152A_T::SetupRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status = 0;
	// JRC 040709
	char msg[256] = "";


    // Setup action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupRi4152A called "), Response, BufferSize);

	 // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
 		IFNSIM(Status = ri4152a_timeOut(m_Handle, (ViInt32)(m_MaxTime.Real*1000)));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_timeOut(%x, %d);",
			Status, m_Handle, (ViInt32)(m_MaxTime.Real*1000)), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
	}

	//AC voltage measurement
	if((strcmp(m_Attribute, ATTRIB_VOLTS_AC) == 0)		||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_P) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_PP) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_AV) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_SQ) == 0)		||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_SQ_P) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_SQ_PP) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_TR) == 0)		||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_TR_P) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_TR_PP) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_AC_COMP_DC) == 0))
	{
		IFNSIM(Status = ri4152a_confVoltAc(m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confVoltAc(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
			
		if(m_Volt.Exists)
		{
			if (m_Volt.Real <= 0.1)
			{
				IFNSIM(Status = ri4152a_voltAcRang(m_Handle, false, ri4152a_VOLT_RANG_100MV));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRang (%x, false, ri4152a_VOLT_RANG_100MV);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);

				IFNSIM(Status = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_100_NANO));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRes (m_Handle, ri4152a_VOLT_AC_RES_100_NANO);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if (m_Volt.Real <= 1.0)
			{
				IFNSIM(Status = ri4152a_voltAcRang(m_Handle, false, ri4152a_VOLT_RANG_1V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRang(%x, false, ri4152a_VOLT_RANG_1V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);

				IFNSIM(Status = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_1_MICRO));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_1_MICRO);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if (m_Volt.Real <= 10.0)
			{
				IFNSIM(Status = ri4152a_voltAcRang(m_Handle, false, ri4152a_VOLT_RANG_10V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRang (%x,false, ri4152a_VOLT_RANG_10V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);

				IFNSIM(Status = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_10_MICRO));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_10_MICRO);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if (m_Volt.Real <= 100.0)
			{
				IFNSIM(Status = ri4152a_voltAcRang(m_Handle, false, ri4152a_VOLT_RANG_100V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRang(%x, false, ri4152a_VOLT_RANG_100V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);

				IFNSIM(Status = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_100_MICRO));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_100_MICRO);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else
			{
				IFNSIM((Status = ri4152a_voltAcRang(m_Handle, false, ri4152a_VOLT_RANG_300V)));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRang(%x, false, ri4152a_VOLT_RANG_300V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);

				IFNSIM((Status = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_1_MILLI)));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRes(m_Handle, ri4152a_VOLT_AC_RES_1_MILLI);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else //default auto range
		{
			IFNSIM((Status = ri4152a_voltAcRang(m_Handle, true, ri4152a_VOLT_RANG_MAX)));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltAcRang(%x, true, ri4152a_VOLT_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}

		if(m_Bandwidth.Exists)
		{
			IFNSIM(Status = ri4152a_detBand(m_Handle, (ViReal64)m_Bandwidth.Int));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_detBand(%x, %d);",
				Status, m_Handle, m_Bandwidth.Int), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}

	//AC current measurement
	if((strcmp(m_Attribute, ATTRIB_CURR_AC) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_CURR_AC_AV) == 0))
	{
		IFNSIM(Status = ri4152a_confCurrAc(m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = ri4152a_confCurrAc(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		if(m_Current.Exists)
		{
			if(m_Current.Real <= 1)
			{
				IFNSIM(Status = ri4152a_currAcRang(m_Handle, false, ri4152a_CURR_AC_RANG_1A));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang(%x, false, ri4152a_CURR_AC_RANG_1A);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);		
		
			}
			else
			{
				IFNSIM(Status=ri4152a_currAcRang(m_Handle, false, ri4152a_CURR_AC_RANG_3A));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang(%x, false, ri4152a_CURR_AC_RANG_3A);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else //auto range
		{
			IFNSIM(Status = ri4152a_currAcRang(m_Handle, true, ri4152a_CURR_AC_RANG_MAX));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang(%x, true, ri4152a_CURR_AC_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}

		if(m_Bandwidth.Exists)
		{
			IFNSIM(Status = ri4152a_detBand(m_Handle, (ViReal64)m_Bandwidth.Int));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_detBand(%x, %d);",
				Status, m_Handle, m_Bandwidth.Int), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}
		
	//AC frequency measurement
	if((strcmp(m_Attribute, ATTRIB_FREQ_AC) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_FREQ_SQ) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_FREQ_TR) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_AC_COMP_DC_F) == 0))
	{
		IFNSIM(Status = ri4152a_confFreq(m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confFreq(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		if(m_Volt.Exists)
		{
			if(m_Volt.Real <= 0.1)
			{
				IFNSIM(Status = ri4152a_freqVoltRang(m_Handle, false, ri4152a_VOLT_RANG_100MV));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_freqVoltRang(%x, false, ri4152a_VOLT_RANG_100MV);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Volt.Real > 0.1) && (m_Volt.Real <= 1))
			{
				IFNSIM(Status = ri4152a_freqVoltRang(m_Handle, false, ri4152a_VOLT_RANG_1V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_freqVoltRang(%x, false, ri4152a_VOLT_RANG_1V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Volt.Real > 1) && (m_Volt.Real <= 10))
			{
				IFNSIM(Status = ri4152a_freqVoltRang(m_Handle, false, ri4152a_VOLT_RANG_10V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_freqVoltRang(%x, false, ri4152a_VOLT_RANG_10V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if(m_Volt.Real > 10 && m_Volt.Real <= 100)
			{
				IFNSIM(Status = ri4152a_freqVoltRang(m_Handle, false, ri4152a_VOLT_RANG_100V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_freqVoltRang(%x, false, ri4152a_VOLT_RANG_100V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if(m_Volt.Real > 100)
			{
				IFNSIM(Status = ri4152a_freqVoltRang(m_Handle, false, ri4152a_VOLT_RANG_300V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_freqVoltRang(%x, false, ri4152a_VOLT_RANG_300V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}

		}
		else //auto range
		{
			IFNSIM(Status = ri4152a_freqVoltRang(m_Handle, true, ri4152a_VOLT_RANG_MAX));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_freqVoltRang(%x, true, ri4152a_VOLT_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}
	//AC period measurement
	if((strcmp(m_Attribute, ATTRIB_PERIOD_AC) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_PERIOD_SQ) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_PERIOD_TR) == 0))
	{
		IFNSIM(Status = ri4152a_confPer(m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confPer(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		if(m_Volt.Exists)
		{
			if(m_Volt.Real <= 0.1)
			{
				IFNSIM(Status = ri4152a_perVoltRang(m_Handle, false, ri4152a_VOLT_RANG_100MV));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_perVoltRang(%x, false, ri4152a_VOLT_RANG_100MV);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if((m_Volt.Real > 0.1) && (m_Volt.Real <= 1))
			{
				IFNSIM(Status = ri4152a_perVoltRang(m_Handle, false, ri4152a_VOLT_RANG_1V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_perVoltRang(%x, false, ri4152a_VOLT_RANG_1V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if((m_Volt.Real > 1) && (m_Volt.Real <= 10))
			{
				IFNSIM(Status = ri4152a_perVoltRang(m_Handle, false, ri4152a_VOLT_RANG_10V));				
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_perVoltRang(%x, false, ri4152a_VOLT_RANG_10V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if((m_Volt.Real > 10) && (m_Volt.Real <= 100))
			{
				IFNSIM(Status = ri4152a_perVoltRang(m_Handle, false, ri4152a_VOLT_RANG_100V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_perVoltRang(%x, false, ri4152a_VOLT_RANG_100V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if(m_Volt.Real > 100)
			{
				IFNSIM(Status = ri4152a_perVoltRang(m_Handle, false, ri4152a_VOLT_RANG_300V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_perVoltRang(%x, false, ri4152a_VOLT_RANG_300V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else
		{
			IFNSIM(Status = ri4152a_perVoltRang(m_Handle, true, ri4152a_VOLT_RANG_MAX));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_perVoltRang(%x, true, ri4152a_VOLT_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}

	//DC Voltage measurement
	if((strcmp(m_Attribute, ATTRIB_VOLTS_DC) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_DC_AV) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_DC_OFF_AC) == 0))
	{
		IFNSIM(Status = ri4152a_confVoltDc(m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confVoltDc(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		if(m_Volt.Exists)
		{	
			// set the range according to voltage (the absolute voltage because of negative
			// voltages.
			if(fabs(m_Volt.Real) <= 0.1)
			{
				IFNSIM(Status = ri4152a_voltDcRang(m_Handle, false, ri4152a_VOLT_RANG_100MV));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_100MV);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if((fabs(m_Volt.Real) <= 1.0))
			{
				IFNSIM(Status = ri4152a_voltDcRang (m_Handle, false, ri4152a_VOLT_RANG_1V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_1V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if(fabs(m_Volt.Real) <= 10.0)
			{
				IFNSIM(Status = ri4152a_voltDcRang (m_Handle, false, ri4152a_VOLT_RANG_10V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_10V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if(fabs(m_Volt.Real) <= 100.0)
			{
				IFNSIM(Status = ri4152a_voltDcRang (m_Handle, false, ri4152a_VOLT_RANG_100V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_100V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else
			{
				IFNSIM(Status = ri4152a_voltDcRang (m_Handle, false, ri4152a_VOLT_RANG_300V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_300V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else
		{
			IFNSIM(Status = ri4152a_voltDcRang (m_Handle, true, ri4152a_VOLT_RANG_MAX));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, true, ri4152a_VOLT_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}

		if(m_SampleWidth.Exists)
		{
			// Test - scale sample width by 99% to test if device firmware
			// has floating point error in rounding up.
			IFNSIM(Status = ri4152a_voltDcAper(m_Handle, (m_SampleWidth.Real * 0.99)));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcAper(%x, %e);",
				Status, m_Handle, (m_SampleWidth.Real * 0.99)), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}

		if(strcmp(m_Attribute, ATTRIB_DC_OFF_AC) == 0)
		{
			IFNSIM(Status = ri4152a_voltDcAper(m_Handle, 1.67));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcAper(%x, 1.67);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}

	//DC current measurement
    if((strcmp(m_Attribute, ATTRIB_CURR_DC) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_CURR_DC_AV) == 0))
	{
		IFNSIM((Status = ri4152a_confCurrDc(m_Handle)));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confCurrDc(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		if(m_Current.Exists)
		{
			if(m_Current.Real <= ri4152a_CURR_DC_RANG_10MA)
			{
				IFNSIM(Status = ri4152a_currAcRang(m_Handle, false, ri4152a_CURR_DC_RANG_10MA ));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang (%x,false, ri4152a_CURR_DC_RANG_10MA);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if((m_Current.Real > ri4152a_CURR_DC_RANG_10MA) && (m_Current.Real <= ri4152a_CURR_DC_RANG_100MA))
			{
				IFNSIM(Status = ri4152a_currAcRang(m_Handle, false, ri4152a_CURR_DC_RANG_100MA));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang (%x,false, ri4152a_CURR_DC_RANG_100MA);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if((m_Current.Real > ri4152a_CURR_DC_RANG_100MA) && (m_Current.Real <= ri4152a_CURR_DC_RANG_1A))
			{
				IFNSIM(Status = ri4152a_currAcRang(m_Handle, false, ri4152a_CURR_DC_RANG_1A));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang (%x,false, ri4152a_CURR_DC_RANG_1A);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if(m_Current.Real > ri4152a_CURR_DC_RANG_1A)
			{
				IFNSIM(Status = ri4152a_currAcRang(m_Handle, false, ri4152a_CURR_DC_RANG_3A));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang (%x,false, ri4152a_CURR_DC_RANG_3A);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else //auto range
		{
			IFNSIM(Status = ri4152a_currDcRang(m_Handle, true, ri4152a_CURR_DC_RANG_MAX));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_currAcRang (%x, true, ri4152a_CURR_DC_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}

	//Resistance measurement
	if((strcmp(m_Attribute, ATTRIB_IMP) == 0) ||
	   (strcmp(m_Attribute, ATTRIB_IMP_4WIRE) == 0))
	{
		if(strcmp(m_Attribute, ATTRIB_IMP) == 0)
		{
			IFNSIM(Status = ri4152a_confRes(m_Handle));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confRes(%x);",\
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
		else if (strcmp(m_Attribute, ATTRIB_IMP_4WIRE) == 0)
		{
			IFNSIM(Status = ri4152a_confFres(m_Handle));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confFres(%x);",\
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}

		if (m_Res.Exists)
		{
			//setup range	
			if (m_Res.Real <= ri4152a_RES_RANG_100)
			{
				IFNSIM(Status = ri4152a_resRang(m_Handle, false, ri4152a_RES_RANG_100));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_100);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Res.Real > ri4152a_RES_RANG_100) && (m_Res.Real <= ri4152a_RES_RANG_1K))
			{
				IFNSIM(Status = ri4152a_resRang(m_Handle, false, ri4152a_RES_RANG_1K));				
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_1K);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Res.Real > ri4152a_RES_RANG_1K) && (m_Res.Real <= ri4152a_RES_RANG_10K))
			{
				IFNSIM(Status = ri4152a_resRang(m_Handle, false, ri4152a_RES_RANG_10K));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_10K);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Res.Real > ri4152a_RES_RANG_10K) && (m_Res.Real <= ri4152a_RES_RANG_100K))
			{
				IFNSIM(Status = ri4152a_resRang(m_Handle, 0, ri4152a_RES_RANG_100K));	
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_100K);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Res.Real > ri4152a_RES_RANG_100K) && (m_Res.Real <= ri4152a_RES_RANG_1M))
			{
				IFNSIM(Status = ri4152a_resRang(m_Handle, false, ri4152a_RES_RANG_1M));	
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_1M);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Res.Real > ri4152a_RES_RANG_1M) && (m_Res.Real <= ri4152a_RES_RANG_10M))
			{
				IFNSIM(Status = ri4152a_resRang (m_Handle, false, ri4152a_RES_RANG_10M));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_10M);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if (m_Res.Real > ri4152a_RES_RANG_100M)
			{
				IFNSIM(Status = ri4152a_resRang (m_Handle, false, ri4152a_RES_RANG_100M));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, false, ri4152a_RES_RANG_100M);",\
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else //auto range
		{
			//IFNSIM(Status = ri4152a_resRang(m_Handle, true, ri4152a_RES_RANG_MAX));
			//ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resRang(%x, true, ri4152a_RES_RANG_MAX);",\
			//	Status, m_Handle), Response, BufferSize);
			// JRC 040709
			
			if (strcmp(m_Attribute, ATTRIB_IMP_4WIRE) == 0)
			{
				IFNSIM(Status = viPrintf(m_Handle,"FRES:RANG:AUTO ON\n"));    
			    ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = FRES:RANG:AUTO ON",Status), Response, BufferSize);
			    if(Status)
				   ErrorRi4152A(Status, Response, BufferSize);
            
			    // JRC 0450709
			    IFNSIM(Status = viQueryf(m_Handle, "FRES:RANG:AUTO?\n", "%s", msg));
                ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = viQueryf(%x, \"FRES:RANG:AUTO ON ?\", \"%s\")", Status, m_Handle, msg),Response, BufferSize);
			}
			else // 2-wire auto
			{
				IFNSIM(Status = viPrintf(m_Handle,"RES:RANG:AUTO ON\n"));    
			    ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = RES:RANG:AUTO ON",Status), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
            
				// JRC 0450709
				IFNSIM(Status = viQueryf(m_Handle, "RES:RANG:AUTO?\n", "%s", msg));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%x = viQueryf(%x, \"RES:RANG:AUTO ON ?\", \"%s\")", Status, m_Handle, msg),Response, BufferSize);
			}
		}
		if (m_SampleWidth.Exists)
		{
			IFNSIM(Status = ri4152a_resAper(m_Handle, (ViReal64)m_SampleWidth.Real));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_resAper(%x, %lf);",\
				Status, m_Handle, m_SampleWidth.Real), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}

	//DC Ratio
	if ((strcmp(m_Attribute, ATTRIB_VOLTS_DC_R) == 0) ||
		(strcmp(m_Attribute, ATTRIB_VOLTS_AC_R) == 0))
	{
		IFNSIM(Status = ri4152a_confVoltDcRat(m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_confVoltDcRat(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		// set the range according to voltage across HI LO pins only, documentation states Ref-Voltage is automatically in autorange

		if (m_Volt.Exists)
		{
			if (m_Volt.Real <= 0.1)
			{
				IFNSIM(Status = ri4152a_voltDcRang(m_Handle, false, ri4152a_VOLT_RANG_100MV));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_100MV);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Volt.Real > 0.1) && (m_Volt.Real <= 1))
			{
				IFNSIM(Status = ri4152a_voltDcRang(m_Handle, false, ri4152a_VOLT_RANG_1V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_1V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Volt.Real > 1) && (m_Volt.Real <= 10))
			{
				IFNSIM(Status = ri4152a_voltDcRang(m_Handle, false, ri4152a_VOLT_RANG_10V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_10V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if ((m_Volt.Real > 10) && (m_Volt.Real <= 100))
			{
				IFNSIM(Status = ri4152a_voltDcRang(m_Handle, false, ri4152a_VOLT_RANG_100V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_100V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
			else if (m_Volt.Real > 100)
			{
				IFNSIM(Status = ri4152a_voltDcRang(m_Handle, false, ri4152a_VOLT_RANG_300V));
				ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, false, ri4152a_VOLT_RANG_300V);",
					Status, m_Handle), Response, BufferSize);
				if(Status)
					ErrorRi4152A(Status, Response, BufferSize);
			}
		}
		else
		{
			IFNSIM(Status = ri4152a_voltDcRang (m_Handle, true, ri4152a_VOLT_RANG_MAX));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcRang(%x, true, ri4152a_VOLT_RANG_MAX);",
				Status, m_Handle), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
		
		if (m_SampleWidth.Exists)
		{
			IFNSIM(Status = ri4152a_voltDcAper(m_Handle, m_SampleWidth.Real));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_voltDcAper(%x, %e);",
				Status, m_Handle, m_SampleWidth.Real), Response, BufferSize);
			if(Status)
				ErrorRi4152A(Status, Response, BufferSize);
		}
	}

	if (m_RefVolt.Exists)
	{
		IFNSIM(Status = ri4152a_calcNullOffs(m_Handle, m_RefVolt.Real));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_calcNullOffs(%x, %lf);",
			Status, m_Handle, m_RefVolt.Real), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
	}
	if (m_RefRes.Exists)
	{
		IFNSIM(Status = ri4152a_calcNullOffs(m_Handle, m_RefRes.Real));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_calcNullOffs((%x, %lf);",
			Status, m_Handle, m_RefRes.Real), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
	}

    if (m_SampleCount.Exists)
    {
        IFNSIM(Status = ri4152a_sampCoun(m_Handle, m_SampleCount.Int));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_sampCoun(%x, %d);",
			Status, m_Handle, m_SampleCount.Int), Response, BufferSize);
        if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchRi4152A
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
int CRi4152A_T::FetchRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status = 0;
	double  MeasValue = 10.0;
    ViInt32  MaxTime = 5000;
    char	*ErrMsg = "";
	int		i = 0;
	double	AveTotal;
	
	long	num_rdgs = 0;
	ViReal64 rdgs[512];
	
	for (i = 0; i < 512; i++)
		rdgs[i] = 0.0;
    
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchRi4152A called "), Response, BufferSize);
	
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = (ViInt32)(m_MaxTime.Real * 1000);
	}
	if (m_SampleCount.Exists)
	{
		if ((MaxTime/m_SampleCount.Int) < 500)
			MaxTime = m_SampleCount.Int * 500;
	}

	IFNSIM(Status = ri4152a_timeOut(m_Handle, (ViInt32)MaxTime));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_timeOut(%x, %d);",
		Status, m_Handle, (ViInt32)MaxTime), Response, BufferSize);
	if(Status)
		ErrorRi4152A(Status, Response, BufferSize);

//	This is the exact code called by ri4152a_timeOut()	    					
//	// set the visa time-out using above MaxTime	
//	IFNSIM(Status = viSetAttribute(m_Handle, VI_ATTR_TMO_VALUE,(unsigned long) MaxTime));
//	if(Status)
//		ErrorRi4152A(Status, Response, BufferSize);		
	

    // Fetch data
	// ri4152a_fetc_Q must be used otherwise the other measure commands uses default parameters
	// to take the measurement, even though you set up parameters.  And it forces auto range to
	// be enabled.
	
	
	IFNSIM(Status = ri4152a_fetc_Q (m_Handle, rdgs, &num_rdgs));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_fetc_Q(%x, [%lf, ...], %d);",
		Status, m_Handle, rdgs[0], num_rdgs), Response, BufferSize);
	if(Status)
		ErrorRi4152A(Status, Response, BufferSize);

	AveTotal=0;
	for(i=0; i< num_rdgs; i++)
	{				
		AveTotal+=rdgs[i];
	}
	MeasValue=AveTotal/num_rdgs;
			

	ATXMLW_DEBUG(5,atxmlw_FmtMsg("MeasValu = %E", MeasValue), Response, BufferSize);
	

    if(Status)
    {
        MeasValue = FLT_MAX;
        ErrorRi4152A(Status, Response, BufferSize);
    }
    else
    {
        sscanf(ErrMsg,"%E",&MeasValue);
    }
	if(Response && (BufferSize > (int)(strlen(Response)+200)))
	{
		//AC voltage measurement
		if((strcmp(m_Attribute,ATTRIB_VOLTS_AC) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_P) == 0)	||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_PP) == 0)	||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_AV) == 0)	||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_SQ) == 0)      ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_SQ_P) == 0)    ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_SQ_PP) == 0)   ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_TR) == 0)      ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_TR_P) == 0)    ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_TR_PP) == 0)   ||
		   (strcmp(m_Attribute, ATTRIB_AC_COMP_DC) == 0)    ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_DC) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_DC_AV) == 0)	||
		   (strcmp(m_Attribute, ATTRIB_DC_OFF_AC) == 0))
		{
			if(strcmp(m_Attribute, ATTRIB_VOLTS_AC_P) ==0)
				MeasValue=MeasValue*SQRT_2;
			if(strcmp(m_Attribute, ATTRIB_VOLTS_AC_PP) ==0)
				MeasValue=MeasValue*SQRT_2*2;
			if(strcmp(m_Attribute, ATTRIB_VOLTS_SQ_PP)==0)
				MeasValue=MeasValue*2;
			if(strcmp(m_Attribute, ATTRIB_VOLTS_TR_P)==0)
				MeasValue=MeasValue*SQRT_3;
			if(strcmp(m_Attribute, ATTRIB_VOLTS_TR_PP)==0)
				MeasValue=MeasValue*SQRT_3*2;
	
			atxmlw_ScalerDoubleReturn(m_Attribute, "V", MeasValue,
					Response, BufferSize);
		}
		if((strcmp(m_Attribute, ATTRIB_FREQ_AC) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_FREQ_SQ) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_FREQ_TR) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_AC_COMP_DC_F) == 0))

		{
			atxmlw_ScalerDoubleReturn(m_Attribute, "Hz", MeasValue,
				Response, BufferSize);
		}
		if((strcmp(m_Attribute, ATTRIB_CURR_AC) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_CURR_AC_AV) == 0)	||
		   (strcmp(m_Attribute, ATTRIB_CURR_DC) == 0)		||
		   (strcmp(m_Attribute, ATTRIB_CURR_DC_AV) == 0))
		{
			atxmlw_ScalerDoubleReturn(m_Attribute, "A", MeasValue,
					Response, BufferSize);
		}
		if((strcmp(m_Attribute, ATTRIB_IMP) == 0) ||
		   (strcmp(m_Attribute, ATTRIB_IMP_4WIRE) == 0))
		{
			atxmlw_ScalerDoubleReturn(m_Attribute, "Ohm", MeasValue,
					Response, BufferSize);
		}
		if((strcmp(m_Attribute, ATTRIB_PERIOD_AC) == 0) ||
		   (strcmp(m_Attribute, ATTRIB_PERIOD_SQ) == 0) ||
		   (strcmp(m_Attribute, ATTRIB_PERIOD_TR) == 0))
		{
			atxmlw_ScalerDoubleReturn(m_Attribute, "s", MeasValue,
					Response, BufferSize);
		}
		if((strcmp(m_Attribute,ATTRIB_VOLTS_DC_R) == 0) ||
		   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_R) == 0))
		{
			atxmlw_ScalerDoubleReturn(m_Attribute, "", MeasValue,
					Response, BufferSize);
		}

	}
	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: DisableRi4152A
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
int CRi4152A_T::DisableRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    // Open action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenRi4152A called "), Response, BufferSize);

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableRi4152A
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
int CRi4152A_T::EnableRi4152A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int       Status = 0;
	long	  max_time = 0;
	ViInt16	  operationComplete = 0;
    
    
    // Close action for the Ri4152A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableRi4152A called "), Response, BufferSize);

	
 	if(m_EventDelay.Exists)//set up triggering
	{		

		IFNSIM((Status = ri4152a_trigger(m_Handle,
										 1,
										 false,
										 m_EventDelay.Real,
										 ri4152a_TRIG_SOUR_EXT)));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_trigger(%x, 1, false, %e, ri4152a_TRIG_SOUR_EXT);",
			Status, m_Handle, m_EventDelay.Real), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
	}
	else
	{
		// These commands cause this function to wait until the measurement is complete 
		// or the VISA timeout at read (10 seconds). This is necessary so the 
	    // persistance of the RTS-displayed value during a MONITOR statement is readable.
    	// Otherwise, the time spent waiting for the DMM to complete measurement is 
		// during the FETCH part, when the RTS has already blanked-out the measurement 
		// display.
		// Max measurement time is 7 sec (AC signal, slow filter). 
		// TMO value is 10 sec per hpe1412a_reset, or user-specified MAX-TIME.
		
		// set VISA TMO_VALUE, if max-time is specified 


		// specify that the trigger source is immediate
		IFNSIM(Status = ri4152a_trigger(m_Handle, 1, false, 0, ri4152a_TRIG_SOUR_IMM));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_trigger(%x, 1, false, 0, ri4152a_TRIG_SOUR_IMM);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
	
		//IFNSIM(Status = ri4152a_initImm (m_Handle));
		//if(Status)
		//			ErrorRi4152A(Status, Response, BufferSize);

		if(m_MaxTime.Exists)
		{
			max_time = (long)(m_MaxTime.Real * 1000); 
		}
		else
		{
			max_time = 5 * 1000;
		}						
			
		IFNSIM(Status = viSetAttribute(m_Handle, VI_ATTR_TMO_VALUE,(unsigned long) max_time));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = viSetAttribute(%s, VI_ATTR_TMO_VALUE, %d);",
			Status, m_Handle, (int)max_time), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
		
		IFNSIM(Status = ri4152a_opc (m_Handle));
		ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_opc(%x);",
			Status, m_Handle), Response, BufferSize);
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);

		do
		{
			IFNSIM(ri4152a_opc_Q (m_Handle, &operationComplete));
			ATXMLW_DEBUG(5, atxmlw_FmtMsg("ri4152a_opc_Q(%x, %d);",
				m_Handle, operationComplete), Response, BufferSize);
			Sleep(500);
		} while(operationComplete = 0);	        
		if(Status)
			ErrorRi4152A(Status, Response, BufferSize);
		
	}

	//customer requested a 800mS delay for AC Voltage measurements for signal to settle
    if((strcmp(m_Attribute, ATTRIB_VOLTS_AC) == 0)		||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_P) == 0)	||
	   (strcmp(m_Attribute, ATTRIB_VOLTS_AC_PP) == 0)	||
       (strcmp(m_Attribute, ATTRIB_VOLTS_AC_R) == 0)	||
       (strcmp(m_Attribute, ATTRIB_FREQ_AC) == 0)		||
       (strcmp(m_Attribute, ATTRIB_PERIOD_AC) == 0))
    {
        Sleep(800);
    }

    //moved init here
    IFNSIM((Status = ri4152a_initImm(m_Handle)));
	ATXMLW_DEBUG(5, atxmlw_FmtMsg("%d = ri4152a_initImm(%x);",
		Status, m_Handle), Response, BufferSize);
	if(Status)
		ErrorRi4152A(Status, Response, BufferSize);
    
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorRi4152A
//
// Purpose: Query Ri4152A for the error text and send to WRTS
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
int  CRi4152A_T::ErrorRi4152A(int Status,
                                  ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int      retval;
    int      Err = 0;
    char     Msg[MAX_MSG_SIZE];
    ViInt32      QError;

    retval = Status;
    Msg[0] = '\0';

    if(Status)
    {
        // Decode Ri4152A lib return code
        IFNSIM(Err = ri4152a_error_message(m_Handle, Status, Msg));
        if(Err)
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);

		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
			Status, Msg);
    }

    QError = 1;
    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;
        IFNSIM(Err = ri4152a_error_query(m_Handle, &QError, Msg));
        if(Err != 0)
            break;
        if(QError)
        {
	        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",
				Status, Msg);
        }
    }


    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoRi4152A
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
int CRi4152A_T::GetStmtInfoRi4152A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char LclElement[ATXMLW_MAX_NAME];
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME];
    char  LclCharValuePtr[128];
	char  *TempCharValuePtr;
	char AC_Component[ATXMLW_MAX_NAME], DC_Component[ATXMLW_MAX_NAME], temp[ATXMLW_MAX_NAME];

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription,
                             Response, BufferSize)))
         return(Status);

     m_Action = atxmlw_Get1641SignalAction(m_SignalDescription,
                      Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action),
                              Response, BufferSize);

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource,
                      Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource),
                              Response, BufferSize);

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
                              m_SignalName, m_SignalElement),
                              Response, BufferSize);

 	if(strcmp(m_SignalElement, "Measure")==0) //get elements
	{
		//Get attribute and store
		if(atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "Attribute",
											&TempCharValuePtr))
		{
			strcpy(LclCharValuePtr,TempCharValuePtr);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Attribute [%s]",
				m_SignalName, LclCharValuePtr),
				Response, BufferSize);
		}	
		strcpy(m_Attribute, LclCharValuePtr);

		//Get signal discription
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "As",
											&TempCharValuePtr)))
		{
			strcpy(LclCharValuePtr,TempCharValuePtr);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
								m_SignalName, LclCharValuePtr),
								Response, BufferSize);

			//Get parameters based on measurement type
			//AC measurements
			if(strcmp(m_Attribute, ATTRIB_VOLTS_AC) ==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_AC_P) ==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_AC_PP) ==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_AC_R) ==0 ||
				strcmp(m_Attribute, ATTRIB_FREQ_AC) ==0 ||
				strcmp(m_Attribute, ATTRIB_CURR_AC) ==0 ||
				strcmp(m_Attribute, ATTRIB_PERIOD_AC) ==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_AC_AV) ==0 ||
				strcmp(m_Attribute, ATTRIB_CURR_AC_AV) ==0 ||
				strcmp(m_Attribute, ATTRIB_DC_OFF_AC) ==0)
			{
				//find element type
				if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
				{
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

					if(strcmp(LclElement, "HighPass")==0) //set bandwidth modifier
					{
						if((m_Bandwidth.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"cutoff", &LclDblValue, LclUnit)))
						{
							m_Bandwidth.Int=(int)LclDblValue;
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Bandwidth frequency %d [%s]",
									m_Bandwidth.Int,LclUnit),
									Response, BufferSize);
						}

						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						strcpy(LclCharValuePtr, TempCharValuePtr);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);
						
					}
					if(strcmp(LclElement, "Sum")==0) //Combo of Signals
					{
						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						//strcpy(LclCharValuePtr, TempCharValuePtr);
						sscanf(TempCharValuePtr,"%s %s", AC_Component, DC_Component);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, AC_Component, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

						//Component names need to be swapped
						if(strcmp(LclElement, "Constant")==0)
						{
							strcpy(temp, AC_Component);
							strcpy(AC_Component, DC_Component);
							strcpy(DC_Component, temp);
						}

						//get AC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);
						//Check to see if current was specified instead
						if(strcmp(LclUnit, "A")==0)
						{
							//move data to current member variable
							m_Current.Real=m_Volt.Real;
							m_Current.Exists=true;
							m_Volt.Exists=false;
						}

						if((m_Freq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"frequency", &m_Freq.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Frequency %E [%s]",
									m_Freq.Real,LclUnit),
									Response, BufferSize);
							//set period modifier
							m_Period.Real = 1.0/m_Freq.Real;
							m_Period.Exists=true;
						}
						
						//get DC component info modifiers
						if((m_DCOffset.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, DC_Component, 
							"amplitude", &m_DCOffset.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_DCOffset.Real,LclUnit),
									Response, BufferSize);
		
					}
					else if(strcmp(LclElement, "Sinusoid")==0) //AC signal only
					{
						//get AC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);
						//Check to see if current was specified instead
						if(strcmp(LclUnit, "A")==0)
						{
							//move data to current member variable
							m_Current.Real=m_Volt.Real;
							m_Current.Exists=true;
							m_Volt.Exists=false;
						}

						if((m_Freq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"frequency", &m_Freq.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Frequency %E [%s]",
									m_Freq.Real,LclUnit),
									Response, BufferSize);
							//set period modifier
							m_Period.Real=1.0/m_Freq.Real;
							m_Period.Exists=true;
						}
					}
					else if(strcmp(LclElement, "Constant")==0) //DC signal only
					{
						//get DC component info modifiers
						if((m_DCOffset.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_DCOffset.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_DCOffset.Real,LclUnit),
									Response, BufferSize);
					}
				}
			}
			//DC measurement
			else if(strcmp(m_Attribute, ATTRIB_VOLTS_DC) ==0 ||
				strcmp(m_Attribute, ATTRIB_CURR_DC) ==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_DC_R) ==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_DC_AV) ==0 ||
				strcmp(m_Attribute, ATTRIB_CURR_DC_AV) ==0 ||
				strcmp(m_Attribute, ATTRIB_AC_COMP_DC) ==0 ||
				strcmp(m_Attribute, ATTRIB_AC_COMP_DC_F) ==0)
			{
				//find element type
				if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
				{
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

					if(strcmp(LclElement, "HighPass")==0) //set bandwidth modifier
					{
						if((m_Bandwidth.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"cutoff", &LclDblValue, LclUnit)))
						{
							m_Bandwidth.Int=(int)LclDblValue;
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Bandwidth frequency %d [%s]",
									m_Bandwidth.Int,LclUnit),
									Response, BufferSize);
						}

						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						strcpy(LclCharValuePtr, TempCharValuePtr);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);
						
					}
					if(strcmp(LclElement, "Sum")==0) //Combo of Signals
					{
						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						//strcpy(LclCharValuePtr, TempCharValuePtr);
						sscanf(TempCharValuePtr,"%s %s", AC_Component, DC_Component);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, AC_Component, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

						//Component names need to be swapped
						if(strcmp(LclElement, "Constant")==0)
						{
							strcpy(temp, AC_Component);
							strcpy(AC_Component, DC_Component);
							strcpy(DC_Component, temp);
						}

						//get AC component info modifiers
						if((m_ACComp.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"amplitude", &m_ACComp.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found AC-COMP %E [%s]",
									m_ACComp.Real,LclUnit),
									Response, BufferSize);

						if((m_ACCompFreq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"frequency", &m_ACCompFreq.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found AC-COMP Frequency %E [%s]",
									m_ACCompFreq.Real,LclUnit),
									Response, BufferSize);
						
						//get DC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, DC_Component, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);
						//Check to see if current was specified instead
						if(strcmp(LclUnit, "A")==0)
						{
							//move data to current member variable
							m_Current.Real=m_Volt.Real;
							m_Current.Exists=true;
							m_Volt.Exists=false;
						}
		
					}
					else if(strcmp(LclElement, "Sinusoid")==0) //AC signal only
					{
						//get AC component info modifiers
						if((m_ACComp.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"amplitude", &m_ACComp.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found AC-COMP %E [%s]",
									m_ACComp.Real,LclUnit),
									Response, BufferSize);

						if((m_ACCompFreq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"frequency", &m_ACCompFreq.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found AC-COMP Frequency %E [%s]",
									m_ACCompFreq.Real,LclUnit),
									Response, BufferSize);
					}
					else if(strcmp(LclElement, "Constant")==0) //DC signal only
					{
						//get DC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);
						//Check to see if current was specified instead
						if(strcmp(LclUnit, "A")==0)
						{
							//move data to current member variable
							m_Current.Real=m_Volt.Real;
							m_Current.Exists=true;
							m_Volt.Exists=false;
						}
					}
				}
			}
			//Resistance Measurement
			else if(strcmp(m_Attribute, ATTRIB_IMP) ==0)
			{
								//find element type
				if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
				{
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

					if(strcmp(LclElement, "Load")==0) //Verify this is a load
					{
						if((m_Res.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"resistance", &m_Res.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resistance %E [%s]",
									m_Res.Real,LclUnit),
									Response, BufferSize);
						}
					}
				}
			}
			//Square Wave Measurement
			else if(strcmp(m_Attribute, ATTRIB_VOLTS_SQ)==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_SQ_P)==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_SQ_PP)==0 ||
				strcmp(m_Attribute, ATTRIB_FREQ_SQ)==0 ||
				strcmp(m_Attribute, ATTRIB_PERIOD_SQ)==0)
			{
				//find element type
				if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
				{
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

					if(strcmp(LclElement, "HighPass")==0) //set bandwidth modifier
					{
						if((m_Bandwidth.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"cutoff", &LclDblValue, LclUnit)))
						{
							m_Bandwidth.Int=(int)LclDblValue;
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Bandwidth frequency %d [%s]",
									m_Bandwidth.Int,LclUnit),
									Response, BufferSize);
						}

						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						strcpy(LclCharValuePtr, TempCharValuePtr);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);
						
					}
					if(strcmp(LclElement, "Sum")==0) //Combo of Signals
					{
						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						//strcpy(LclCharValuePtr, TempCharValuePtr);
						sscanf(TempCharValuePtr,"%s %s", AC_Component, DC_Component);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, AC_Component, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

						//Component names need to be swapped
						if(strcmp(LclElement, "Constant")==0)
						{
							strcpy(temp, AC_Component);
							strcpy(AC_Component, DC_Component);
							strcpy(DC_Component, temp);
						}

						//get AC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);

						if((m_Freq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"frequency", &m_Freq.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Frequency %E [%s]",
									m_Freq.Real,LclUnit),
									Response, BufferSize);
							//set period modifier
							m_Period.Real=1.0/m_Freq.Real;
							m_Period.Exists=true;
						}
						
						//get DC component info modifiers
						if((m_DCOffset.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, DC_Component, 
							"amplitude", &m_DCOffset.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_DCOffset.Real,LclUnit),
									Response, BufferSize);
		
					}
					else if(strcmp(LclElement, "Squarewave")==0) //AC signal only
					{
						//get AC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);

						if((m_Freq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"frequency", &m_Freq.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Frequency %E [%s]",
									m_Freq.Real,LclUnit),
									Response, BufferSize);
							//set period modifier
							m_Period.Real=1/m_Freq.Real;
							m_Period.Exists=true;
						}
					}
					else if(strcmp(LclElement, "Constant")==0) //DC signal only
					{
						//get DC component info modifiers
						if((m_DCOffset.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_DCOffset.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_DCOffset.Real,LclUnit),
									Response, BufferSize);
					}
				}
			}
			//Triangle Wave Measurement
			else if(strcmp(m_Attribute, ATTRIB_VOLTS_TR)==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_TR_P)==0 ||
				strcmp(m_Attribute, ATTRIB_VOLTS_TR_PP)==0 ||
				strcmp(m_Attribute, ATTRIB_FREQ_TR)==0 ||
				strcmp(m_Attribute, ATTRIB_PERIOD_TR)==0)
			{
								//find element type
				if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
				{
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

					if(strcmp(LclElement, "HighPass")==0) //set bandwidth modifier
					{
						if((m_Bandwidth.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"cutoff", &LclDblValue, LclUnit)))
						{
							m_Bandwidth.Int=(int)LclDblValue;
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Bandwidth frequency %d [%s]",
									m_Bandwidth.Int,LclUnit),
									Response, BufferSize);
						}

						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						strcpy(LclCharValuePtr, TempCharValuePtr);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);
						
					}
					if(strcmp(LclElement, "Sum")==0) //Combo of Signals
					{
						if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
												&TempCharValuePtr)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s As [%s]",
									LclCharValuePtr, TempCharValuePtr),
									Response, BufferSize);

						//strcpy(LclCharValuePtr, TempCharValuePtr);
						sscanf(TempCharValuePtr,"%s %s", AC_Component, DC_Component);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, AC_Component, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

						//Component names need to be swapped
						if(strcmp(LclElement, "Constant")==0)
						{
							strcpy(temp, AC_Component);
							strcpy(AC_Component, DC_Component);
							strcpy(DC_Component, temp);
						}

						//get AC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);

						if((m_Freq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, AC_Component, 
							"frequency", &m_Freq.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Frequency %E [%s]",
									m_Freq.Real,LclUnit),
									Response, BufferSize);
							//set period modifier
							m_Period.Real=1/m_Freq.Real;
							m_Period.Exists=true;
						}
						
						//get DC component info modifiers
						if((m_DCOffset.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, DC_Component, 
							"amplitude", &m_DCOffset.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_DCOffset.Real,LclUnit),
									Response, BufferSize);
		
					}
					else if(strcmp(LclElement, "Triangle")==0) //AC signal only
					{
						//get AC component info modifiers
						if((m_Volt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_Volt.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_Volt.Real,LclUnit),
									Response, BufferSize);

						if((m_Freq.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"frequency", &m_Freq.Real, LclUnit)))
						{
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Frequency %E [%s]",
									m_Freq.Real,LclUnit),
									Response, BufferSize);
							//set period modifier
							m_Period.Real=1/m_Freq.Real;
							m_Period.Exists=true;
						}
					}
					else if(strcmp(LclElement, "Constant")==0) //DC signal only
					{
						//get DC component info modifiers
						if((m_DCOffset.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"amplitude", &m_DCOffset.Real, LclUnit)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Voltage %E [%s]",
									m_DCOffset.Real,LclUnit),
									Response, BufferSize);
					}
				}
			}
		}
		//Get Triggering
		if((atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "Sync",
											&TempCharValuePtr)))
		{
			strcpy(LclCharValuePtr,TempCharValuePtr);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]",
								m_SignalName, LclCharValuePtr),
								Response, BufferSize);

			if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
			{
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
								Response, BufferSize);

				if(strcmp(LclElement, "Instantaneous")==0) //Verify this is a sync
				{
                    
					//find delay if there is one
					if((atxmlw_Get1641StringAttribute(m_SignalDescription, LclCharValuePtr, "In",
											&TempCharValuePtr)))
					{
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]",
								LclCharValuePtr, TempCharValuePtr),
								Response, BufferSize);

						strcpy(LclCharValuePtr, TempCharValuePtr);

						if((atxmlw_Get1641ElementByName(m_SignalDescription, LclCharValuePtr, LclElement)))
							ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Element [%s]",LclCharValuePtr, LclElement),
									Response, BufferSize);

						if((m_EventDelay.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, LclCharValuePtr, 
							"delay", &m_EventDelay.Real, LclUnit)))
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found found Event Delay %E [%s]",
								m_EventDelay.Real,LclUnit),
								Response, BufferSize);
					}
				}
			}
		}
		if((m_RefVolt.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, 
							"refVolt", &m_RefVolt.Real, LclUnit)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found found REF-VOLT %E [%s]",
								m_RefVolt.Real,LclUnit),
								Response, BufferSize);
		}
		if((m_SampleWidth.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, 
							"gateTime", &m_SampleWidth.Real, LclUnit)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found found SAMPLE-WIDTH %E [%s]",
								m_SampleWidth.Real,LclUnit),
								Response, BufferSize);
		}
		if((m_SampleCount.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, 
							"samples", &LclDblValue, LclUnit)))
		{
			m_SampleCount.Int=(int)LclDblValue;
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found found SAMPLE-COUNT %d [%s]",
								m_SampleCount.Int,LclUnit),
								Response, BufferSize);
		}
		
		if((m_RefRes.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, 
							"refRes", &m_RefRes.Real, LclUnit)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found found REF-RES %d [%s]",
								m_RefRes.Int,LclUnit),
								Response, BufferSize);
		}
		if((m_MaxTime.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, 
							"maxTime", &m_MaxTime.Real, LclUnit)))
		{
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found found MAX-TIME %E [%s]",
							m_MaxTime.Real,LclUnit),
								Response, BufferSize);
		}
	}
	else
	{
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Element name [%s] is not valid",
								LclElement), Response, BufferSize);
	}

	atxmlw_Close1641Xml(&m_SignalDescription);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateRi4152A
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
void CRi4152A_T::InitPrivateRi4152A(void)
{
    m_Volt.Exists=false;
    m_RefVolt.Exists=false;
    m_SampleWidth.Exists=false;
    m_Bandwidth.Exists=false;
    m_DCOffset.Exists=false;
	m_VoltRatio.Exists=false;
	m_Res.Exists=false;
    m_RefRes.Exists=false;
    m_SampleCount.Exists=false;
    m_Freq.Exists=false;
    m_Current.Exists=false;
    m_Period.Exists=false;
    m_AvVolt.Exists=false;
    m_AvCurrent.Exists=false;
    m_ACComp.Exists=false;
    m_ACCompFreq.Exists=false;
	m_MaxTime.Exists=false;
	m_EventSampleCount.Exists=false;
	m_Delay.Exists=false;
	m_EventDelay.Exists=false;
	m_EventDelay.Real=0;

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataRi4152A
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
void CRi4152A_T::NullCalDataRi4152A(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: SanitizeRi4152
//
// Sets instrument to lowest stim and highest impedance settings. Opens all output relays
//
//
///////////////////////////////////////////////////////////////////////////////
void CRi4152A_T::SanitizeRi4152(void)
{
	IFNSIM(ri4152a_inpImpAuto(m_Handle, true));
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




