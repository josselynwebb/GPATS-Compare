//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Arb_AgE1445A_T.cpp
//
// Date:	    27OCT06
//
// Purpose:	    ATXMLW Instrument Driver for AgE1445A
//
// Instrument:	AgE1445A  Arbitrary Waveform Generator
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
// =======  =======  =======================================			===================
// 1.1.0.0  27OCT06  Initial Release									EADS, North America Defense
// 1.1.0.1	16NOV06	 In Enable() changed the layer2 count to 1 and
//						added LAYER2:SOUR Immediate						M.Hendricksen, EADS
// 1.4.0.1	09JUL07	 In Enable() changed the triggering so each   
//						trigger will gernerate 1 wave. PCR 173			D.Bubenik, EADS
// 1.4.0.2  02MAR09  Corrected array dimensioning, deleted 
//                   Vars.                                              E.Larson, EADS
///////////////////////////////////////////////////////////////////////////////

#pragma warning (disable : 4996)

// Includes
#include "visa.h"
#include "AtxmlWrapper.h"
#include "Arb_AgE1445A_T.h"
#include "HPE1445A.H"

// Local Defines

// Function codes

//////// Place Arb_AgE1445A specific data here //////////////
//////////////////////////////////////////////////////////
//#define ISSTRATTR(x,y) (atxmlw_Get1641StringAttribute(m_SignalDescription, Name, x, y);

#define STD_PERIODIC_DC	8
#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024

// Static Variables
static int	OffsetCheckFlag = 1;

// Local Function Prototypes
static void string_to_array(char input [], double array [], int * size);
static void s_IssueDriverFunctionCallDeviceXxx(int Handle, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize);

int DE_BUG = 0;
FILE *debugfp = 0;
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CArb_AgE1445A_T
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

CArb_AgE1445A_T::CArb_AgE1445A_T(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl,
								 ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;

	ISDODEBUG(dodebug(0, "CArb_AgE1445A_T()", "Wrap-CArb_AgE1445A_T called", (char*)NULL));
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
    m_InitString[0] = '\0';

    InitPrivateArb_AgE1445A();
	NullCalDataArb_AgE1445A();

    // Form Init String
    sprintf(m_InitString,"%s%d::%d::INSTR", m_AddressInfo.ControllerType,m_AddressInfo.ControllerNumber, m_AddressInfo.PrimaryAddress);
    sprintf(LclMsg,"Wrap-CArb_AgE1445A Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    
	ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);

    // Initialize the Arb-Gen AgE1445A and retrieve a handle
    IFNSIM(Status = hpe1445a_init (m_InitString, false, true, (ViPSession)&m_Handle));

	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_init(Init String, false, true, handle)")
														, Response, Buffersize);

	if(Status && ErrorArb_AgE1445A(Status, Response, Buffersize))
	{
        return;
	}

	SanitizeArb_AgE1445A();

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CArb_AgE1445A_T()
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
CArb_AgE1445A_T::~CArb_AgE1445A_T()
{
    char Dummy[1024];

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CArb_AgE1445A Class Destructor called "),Dummy,1024);

    // Close the session and release instrument handle
    IFNSIM(hpe1445a_close (m_Handle));
	//ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_close(%d)",m_Handle), Response, Buffersize);

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::StatusArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;
    char *ErrMsg = "";

    // Status action for the Arb_AgE1445A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("ATXMLDEBUG: ARB_GEN_1 - StatusArb_AgE1445A() - Wrap-StatusArb_AgE1445A called "), Response, BufferSize);
    // Check for any pending error messages
    Status = ErrorArb_AgE1445A(0, Response, BufferSize);

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalArb_AgE1445A
//
// Purpose: Perform the IEEE 1641 / Action for this driver instance
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::IssueSignalArb_AgE1445A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char     *ErrMsg = "";
    int       Status = 0;

    // IEEE 1641 Issue Signal action for the Arb_AgE1445A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalArb_AgE1445A Signal: "),
                              Response, BufferSize);

	if((Status = GetStmtInfoArb_AgE1445A(SignalDescription, Response, BufferSize)) != 0)
        return(Status);

    switch(m_Action)
    {
    case ATXMLW_SA_APPLY:
        if((Status = SetupArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        if((Status = EnableArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_REMOVE:
        if((Status = DisableArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        if((Status = ResetArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_MEASURE:
        if((Status = SetupArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_RESET:
        if((Status = ResetArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_SETUP:
        if((Status = SetupArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_CONNECT:
        break;
    case ATXMLW_SA_ENABLE:
        if((Status = EnableArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISABLE:
        if((Status = DisableArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    case ATXMLW_SA_DISCONNECT:
        break;
	case ATXMLW_SA_STATUS:
        if((Status = StatusArb_AgE1445A(Response, BufferSize)) != 0)
            return(Status);
        break;
    }

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::RegCalArb_AgE1445A(ATXMLW_INTF_CALDATA* CalData)
{
    int       Status = 0;
    char      Dummy[1024];

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalArb_AgE1445A CalData: %s", 
                               CalData),Dummy,1024);
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::ResetArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
    char *ErrMsg = "";

    // Reset action for the Arb_AgE1445A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetArb_AgE1445A called "), Response, BufferSize);

    // Reset the Arb_AgE1445A
    IFNSIM((Status = hpe1445a_reset (m_Handle)));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_reset"), Response, BufferSize);

	IFNSIM(Status = viPrintf(m_Handle, "*CLS"));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(m_Handle, *CLS)"), Response, BufferSize);

    // Open output relay to disable output 
    IFNSIM(Status = viPrintf(m_Handle, "OUTP:STAT OFF"));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(m_Handle, OUTP:STAT OFF)"), Response, BufferSize);
	
	if(Status)
        ErrorArb_AgE1445A(Status, Response, BufferSize);

    InitPrivateArb_AgE1445A();

	SanitizeArb_AgE1445A();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::IstArb_AgE1445A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;
	int   TestResult = 0;

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstArb_AgE1445A called Level %d", 
                              Level), Response, BufferSize);
    // Reset the Arb_AgE1445A
    IFNSIM((Status = hpe1445a_reset (m_Handle)));

	switch(Level)
    {
    case ATXMLW_IST_LVL_PST: // Power Up Self Test
        Status = StatusArb_AgE1445A(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_IST: // Internal Self Test (IST*)
		IFNSIM((Status = hpe1445a_selfTest (m_Handle, (ViPInt16)TestResult, Response)));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_selfTest(%d, %d)",m_Handle, (ViPInt16)TestResult), Response, BufferSize);
        Status = StatusArb_AgE1445A(Response,BufferSize);
        break;
    case ATXMLW_IST_LVL_CNF: // Confidence Test (Are you alive)
        Status = StatusArb_AgE1445A(Response,BufferSize);
        break;
    default: // // Multiple BIT levels - Hopefully BIT 1-9
        break;
    }
    IFNSIM((Status = hpe1445a_reset (m_Handle)));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_reset"), Response, BufferSize);

    if(Status)
        ErrorArb_AgE1445A(Status, Response, BufferSize);

    InitPrivateArb_AgE1445A();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsArb_AgE1445A
//
// Purpose: Issue Native instrument commands for this instrument
//          Return the response values in Response
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::IssueNativeCmdsArb_AgE1445A(ATXMLW_INTF_INSTCMD* InstrumentCmds,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsArb_AgE1445A "), Response, BufferSize);

    if((Status = atxmlw_InstrumentCommands(m_Handle, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
        return(Status);

	if (strstr(InstrumentCmds, "*RST") != NULL)
	{
		SanitizeArb_AgE1445A(); 
	}

	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::IssueDriverFunctionCallArb_AgE1445A(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallArb_AgE1445A", Response, BufferSize);

    // Retrieve the Parameters
    IFNSIM(s_IssueDriverFunctionCallDeviceXxx(m_Handle, DriverFunction,
                            Response, BufferSize));

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: SetupArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::SetupArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	Status = 0;

	// Setup action for the Arb_AgE1445A
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("ATXMLDEBUG: ARB_GEN_1 - SetupArb_AgE1445A()"), Response, BufferSize);

	// Common hardware programming for all signals other than DC Signal and Arb. Waveform
	if(!(m_Periodic.Int == STD_PERIODIC_WAVEFORMSTEP) && !(m_Periodic.Int == STD_PERIODIC_WAVEFORMRAMP) && !(m_Periodic.Int == STD_PERIODIC_DC))
	{
	
		// Program Test Equipment Impedence
		switch ((int)m_TEI.Real)
		{
			case 50:
				sprintf(StringToInst, "OUTP1:LOAD:AUTO ON");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(9,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD:AUTO ON)",m_Handle), Response, BufferSize);
				sprintf(StringToInst, "OUTP1:IMP 50");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(9,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:IMP 50)",m_Handle), Response, BufferSize);
				break;
			case 75:
				sprintf(StringToInst, "OUTP1:LOAD:AUTO ON");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(9,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD:AUTO ON)",m_Handle), Response, BufferSize);
				sprintf(StringToInst, "OUTP1:IMP 75");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(9,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:IMP 75)",m_Handle), Response, BufferSize);
				break;
			case 1000000:
				sprintf(StringToInst, "OUTP1:LOAD INF");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);

				sprintf(StringToInst, "OUTP1:IMP 50");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);

				sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:AMPL 0.35");
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);

				ATXMLW_DEBUG(9,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD INF; OUTP1:IMP 50; SOUR:VOLT:LEV:IMM:AMPL 0.35)",m_Handle), Response, BufferSize);
				break;
			default:
				ATXMLW_ERROR(Status,"Wrap-CArb_AgE1445A","No impedence value present",Response,BufferSize);
				break;
		}
		
		// Setup Filters
		if(m_Bandwidth.Real == 250000)
		{
			
			sprintf(StringToInst, "OUTP1:FILT:FREQ 250 KHZ");
			IFNSIM(Status = viPrintf(m_Handle, StringToInst));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:FILT:FREQ 250 KHZ)",m_Handle), Response, BufferSize);
		}
		else
		{
			sprintf(StringToInst, "OUTP1:FILT:FREQ 10 MHZ");
			IFNSIM(Status = viPrintf(m_Handle, StringToInst));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:FILT:FREQ 10 MHZ)",m_Handle), Response, BufferSize);
		}

			sprintf(StringToInst, "OUTP1:FILT ON");
			IFNSIM(Status = viPrintf(m_Handle, StringToInst));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:FILT ON)",m_Handle), Response, BufferSize);

			//Set units for the amplitude to Voltage-P (legacy was VPP)
			IFNSIM(Status = viPrintf(m_Handle, "VOLT:UNIT:VOLT VPK"));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VOLT:UNIT:VOLT VPK)",m_Handle), Response, BufferSize);

			// Perform the following Max-Time routine. Note: If Max-Time is only available in CLOSE, move the code to CLOSE - TBD
			if(SyncEvent)
			{
				// Set VISA TMO_VALUE, if max-time is specified
				if (m_MaxTime.Exists)
					m_MaxTime.Real *= 1000; // convert to milliseconds 
				else
					m_MaxTime.Real = 10000; // Use a default of 10 milliseconds

				IFNSIM(Status = viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (unsigned long) m_MaxTime.Real))
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VI_ATTR_TMO_VALUE, %f)",m_Handle, m_MaxTime.Real), Response, BufferSize);
			}
		} // End common programming for all other signals (except DC and Waveform)

		// configure the waveforms
		switch(m_Periodic.Int)
		{
			case STD_PERIODIC_SINUSOID:			

				if(m_FreqWindow.Exists && m_AgeRate.Exists)
				{
					//Select frequency Sweep Mode
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FREQ1:MODE SWE"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FREQ1:MODE SWE)",m_Handle), Response, BufferSize);

					//Sets Start Frequency
					if(m_AgeRate.Real > 0)
					{
						sprintf(StringToInst, "SOUR:FREQ1:STAR %20.8lf", (m_CenterFreq.Real - (m_FreqWindow.Real / 2)));
					}
					else if(m_AgeRate.Real < 0)
					{
						sprintf(StringToInst, "SOUR:FREQ1:STAR %20.8lf", (m_CenterFreq.Real + (m_FreqWindow.Real / 2)));
					}
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

					//Sets Stop Frequency
					if(m_AgeRate.Real > 0)
					{
						sprintf(StringToInst, "SOUR:FREQ1:STOP %20.8lf", (m_CenterFreq.Real + (m_FreqWindow.Real / 2)));
					}
					else if(m_AgeRate.Real < 0)
					{
						sprintf(StringToInst, "SOUR:FREQ1:STOP %20.8lf", (m_CenterFreq.Real - (m_FreqWindow.Real / 2)));
					}
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

					//Select Linear Frequency Sweep Mode
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:SWE:SPAC LIN"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:SWE:SPAC LIN)",m_Handle), Response, BufferSize);

					//Calculate Sweep Time
					SweepTime = (double)abs((int)(m_FreqWindow.Real / m_AgeRate.Real));

					//Selects the duration of the Sweep
					sprintf(StringToInst, "SOUR:SWE:TIME %10.2f", SweepTime);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
		 			if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}
				else
				{
					sprintf(StringToInst, "SOUR:FREQ1:FIX %20.8lf",m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				// Select SINE WAVE function
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP SIN"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP SIN)",m_Handle), Response, BufferSize);

				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				// Set AC Signal Amplitude (Voltage-P)
				sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:AMPL %12.4f",m_Periodic.Real);
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				if (m_DcComp.Real != 0.0)
				{
					sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:OFFS %12.4f",m_DcComp.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				break;

			case STD_PERIODIC_SQUAREWAVE:
				
				if (m_Freq.Real < 2684354.56)
				{
					// reference oscillator
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT1)",m_Handle), Response, BufferSize);

					// trigger source
					IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT1)",m_Handle), Response, BufferSize);

					// Select SQUARE WAVE function
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP SQU"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP SQU)",m_Handle), Response, BufferSize);

					// Set Frequency value
					sprintf(StringToInst, "SOUR:FREQ1:FIX %20.8lf",m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				}
				else
				{
					// select SQUARE WAVE function
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP SQU"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP SQU)",m_Handle), Response, BufferSize);

					// reference oscillator
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT2"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT)",m_Handle), Response, BufferSize);
				
					// trigger source
					IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT2"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT2)",m_Handle), Response, BufferSize);

					// Set Frequency value
					sprintf(StringToInst, "SOUR:FREQ2:FIX %20.8lf",m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
			
				}

				// Set AC Signal Amplitude (Voltage-P)
				sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:AMPL %12.4f",m_Periodic.Real);
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				if (m_DcComp.Real != 0.0)
				{
					sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:OFFS %12.4f",m_DcComp.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				break;

			case STD_PERIODIC_RAMP:			
				
				if (m_Freq.Real < 107374.1824)
				{
					// reference oscillator
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT1)",m_Handle), Response, BufferSize);

					// trigger source
					IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT1)",m_Handle), Response, BufferSize);

					// Select RAMP WAVE function
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP RAMP"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP RAMP)",m_Handle), Response, BufferSize);

					// Set Frequency value
					sprintf(StringToInst, "SOUR:FREQ1:FIX %20.8lf",m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}
				else
				{
					//sets number of points to be 100 default to generate the ramp
					sprintf(StringToInst,"SOUR:RAMP:POIN 100");
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

					if (m_Freq.Real > 214748.3648)
					{
						Points = (int) (21474836.48 / m_Freq.Real);

						// specify the number of points to be used to generate the ramp
						sprintf(StringToInst,"SOUR:RAMP:POIN %d", Points);
						IFNSIM(Status = viPrintf(m_Handle, StringToInst));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
					}

					// reference oscillator
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT1)",m_Handle), Response, BufferSize);

					// trigger source
					IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT1)",m_Handle), Response, BufferSize);

					// Select RAMP WAVE function
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP RAMP"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP RAMP)",m_Handle), Response, BufferSize);

					// Enable Frequency Doubling
					sprintf(StringToInst, "SOUR:FREQ1:RANG %20.8lf;:SOUR:FREQ1:FIX %20.8lf", m_Freq.Real, m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				// Set AC Signal Amplitude (Voltage-P)
				sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:AMPL %12.4f",m_Periodic.Real);
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				if (m_DcComp.Real != 0.0)
				{
					sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:OFFS %12.4f",m_DcComp.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				break;


			case STD_PERIODIC_TRIANGLE:

				if (m_Freq.Real < 107374.1824)
				{
					// reference oscillator
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT1)",m_Handle), Response, BufferSize);

					// trigger source
					IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT1)",m_Handle), Response, BufferSize);

					// Select TRIANGLE WAVE function
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP TRI"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP TRI)",m_Handle), Response, BufferSize);

					// Set Frequency value
					sprintf(StringToInst, "SOUR:FREQ1:FIX %20.8lf",m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}
				else
				{
					//sets number of points to be 100 which are used to generate the ramp; where 100 is defualt
					sprintf(StringToInst,"SOUR:RAMP:POIN 100");
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

					if (m_Freq.Real > 214748.3648)
					{
						Points = (int) (21474836.48 / m_Freq.Real);
						Points /= 4;	//for the best triangle waveform shape number of points should
						Points *= 4;	//be multiple of 4

						/* specifies number of points to be used to generate the ramp, not executable while initiated */
						sprintf(StringToInst,"SOUR:RAMP:POIN %d", Points);
						IFNSIM(Status = viPrintf(m_Handle, StringToInst));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
					}

					// reference oscillator
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT1"));
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT1)",m_Handle), Response, BufferSize);

					// trigger source
					IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT1"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT1)",m_Handle), Response, BufferSize);

					// Select TRIANGLE WAVE function
					IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP TRI"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP TRI)",m_Handle), Response, BufferSize);

					// Enable Frequency Doubling
					sprintf(StringToInst, "SOUR:FREQ1:RANG %20.8lf;:SOUR:FREQ1:FIX %20.8lf",m_Freq.Real, m_Freq.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}
				
				// Set AC Signal Amplitude (Voltage-P)
				sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:AMPL %12.4f",m_Periodic.Real);
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				if (m_DcComp.Real != 0.0)
				{
					sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:OFFS %12.4f",m_DcComp.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}
				break;

			case STD_PERIODIC_WAVEFORMRAMP:			
				
				// Set Frequency = 1/Sample-Spacing
				ArbWaveFequency = 1/m_SampleSpacing.Real;

				IFNSIM(Status = viPrintf(m_Handle, "VOLT:UNIT:VOLT V"));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VOLT:UNIT:VOLT V)",m_Handle), Response, BufferSize);

				// Program Test Equipment Impedence
				switch ((int)m_TEI.Real)
				{
					case 50:
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:LOAD:AUTO ON"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD:AUTO ON)",m_Handle), Response, BufferSize);
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:IMP 50"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:IMP 50)",m_Handle), Response, BufferSize);
						break;
					case 75:
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:LOAD:AUTO ON"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD:AUTO ON)",m_Handle), Response, BufferSize);
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:IMP 75"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:IMP 75)",m_Handle), Response, BufferSize);
						break;
					case 1000000:
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:LOAD INF"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);

						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:IMP 50"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);

						IFNSIM(Status = viPrintf(m_Handle, "SOUR:VOLT:LEV:IMM:AMPL 0.35"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);

						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD INF;:OUTP1:IMP 50;:SOUR:VOLT:LEV:IMM:AMPL 0.35)",m_Handle), Response, BufferSize);
						break;
					default:
						ATXMLW_ERROR(Status,"Wrap-CArb_AgE1445A","No impedence value present",Response,BufferSize);
						break;
				}

				// Perform the following Max-Time routine. If Max-Time is available in CLOSE, move the code there - TBD
				if (SyncEvent != 0)
				{
					// Set VISA TMO_VALUE, if max-time is specified
					if (m_MaxTime.Exists)
						m_MaxTime.Real *= 1000; // convert to milliseconds 
					else
						m_MaxTime.Real = 10000; // Default 10 milliseconds

					IFNSIM(Status = viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (unsigned long) m_MaxTime.Real))
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VI_ATTR_TMO_VALUE, %f)",m_Handle, m_MaxTime.Real), Response, BufferSize);
				}

				//if ((WaveForm = (char*)malloc(((StimCount * 10) * sizeof(char) + 300 ))) == NULL)
				if ((WaveForm = (char*)malloc(((m_StimSize.Int * 10) * sizeof(char) + 300 ))) == NULL)
				{	
					//* The + 300 is to take into account the SCPI code that is part of the string
					//Display("\033[31;40m*** Allocation of Waveform Memory Failed! ***\033[m\n");		
				}
				else
				{
					sprintf(WaveForm,"SOUR:LIST1:SEGM:VOLT ");
					for (i = 0; i < m_StimSize.Int; i++)
					{
						sprintf(StringToInst,"");
						RawData = m_StimData[i]; // Place Stim Array elements into RawData
						// Add a "," between data StimData
						if (i == 0)
							sprintf(StringToInst,"%.3lf", RawData);
						else
							sprintf(StringToInst,",%.3lf", RawData);
						strcat(WaveForm, StringToInst);
					}
				}  

				// delete all segment sequences data stored in the sequence memory
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:LIST:SSEQ:DEL:ALL"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:LIST:SSEQ:DEL:ALL)",m_Handle), Response, BufferSize);

				// delete all waveform segment definitions in the segment memory
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:LIST:SEGM:DEL:ALL"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUR:LIST:SEGM:DEL:ALL)",m_Handle), Response, BufferSize);

				// select segment sequence
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:USER NONE"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:USER NONE)",m_Handle), Response, BufferSize);

				// select user-defined waveform function
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP USER"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP USER)",m_Handle), Response, BufferSize);

				// reference oscillator
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:ROSC:SOUR INT1"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:ROSC:SOUR INT1)",m_Handle), Response, BufferSize);
				
				// trigger source, not executable while initiated
				IFNSIM(Status = viPrintf(m_Handle, "TRIG:STAR:SOUR INT1"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, TRIG:STAR:SOUR INT1)",m_Handle), Response, BufferSize);
				
				if (ArbWaveFequency < 10737418.24)
				{
					sprintf(StringToInst, "SOUR:FREQ1:FIX %20.8lf",ArbWaveFequency);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}
				else
				{
					// enable frequency doubling, not executable while initiated
					sprintf(StringToInst,"SOUR:FREQ1:RANG %20.8lf;:SOUR:FREQ1:FIX %20.8lf",ArbWaveFequency,ArbWaveFequency);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				//sets the amplitude level to 1.0; specified StimData represent voltage levels
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:VOLT:LEV:IMM:AMPL 5.11875"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:VOLT:LEV:IMM:AMPL 5.11875)",m_Handle), Response, BufferSize);

				//added this statement because dc-offset changes dc level in the setup
				if (m_DcComp.Real != 0.0)
				{
					sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:OFFS %12.4f",m_DcComp.Real);
					IFNSIM(Status = viPrintf(m_Handle, StringToInst));
					if(Status)
						ErrorArb_AgE1445A(Status, Response, BufferSize);
					ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				}

				IFNSIM(Status = viPrintf(m_Handle, "SOUR:LIST1:SEGM:SEL USER_SEG"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:LIST1:SEGM:SEL USER_SEG)",m_Handle), Response, BufferSize);

				sprintf(StringToInst, "SOUR:LIST1:SEGM:DEF %d",m_StimSize.Int);
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
				
				// Retrieve current TimeOut value for later
				IFNSIM(Status = viGetAttribute(m_Handle, VI_ATTR_TMO_VALUE,&TimeOut));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VI_ATTR_TMO_VALUE %f)",m_Handle, TimeOut), Response, BufferSize);
				
				// Disable TimeOut for large array
				IFNSIM(Status = viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE,VI_TMO_INFINITE));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VI_ATTR_TMO_VALUE,VI_TMO_INFINITE)",m_Handle), Response, BufferSize);

				Sleep(100);
				
				IFNSIM(Status = viPrintf(m_Handle, WaveForm));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				//ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, WaveForm), Response, BufferSize);

				// Restore TimeOut to original value
				IFNSIM(Status = viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (ViAttrState)TimeOut));
//				IFNSIM(Status = viSetAttribute (m_Handle, VI_ATTR_TMO_VALUE, (ViAttrState)10000));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VI_ATTR_TMO_VALUE, %f)",m_Handle, TimeOut), Response, BufferSize);

				IFNSIM(Status = viPrintf(m_Handle, "SOUR:LIST1:SSEQ:SEL USER_SEQ"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:LIST1:SSEQ:SEL USER_SEQ)",m_Handle), Response, BufferSize);
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:LIST1:SSEQ:DEF 1"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:LIST1:SSEQ:DEF 1)",m_Handle), Response, BufferSize);
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:LIST1:SSEQ:SEQ USER_SEG"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:LIST1:SSEQ:SEQ USER_SEG)",m_Handle), Response, BufferSize);
				IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:USER USER_SEQ"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:USER USER_SEQ)",m_Handle), Response, BufferSize);

				free(WaveForm);

				break;

			case STD_PERIODIC_DC:			

				//If DC Voltage values is missing, Error message
				if (!m_DcComp.Exists)
					ATXMLW_ERROR(Status,"Wrap-CArb_AgE1445A","Required Field Voltage Missing",Response,BufferSize);

				IFNSIM(Status = viPrintf(m_Handle, "VOLT:UNIT:VOLT V"));
				if(Status)
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, VOLT:UNIT:VOLT V)",m_Handle), Response, BufferSize);

				// Program Test Equipment Impedence
				switch ((int)m_TEI.Real)
				{
					case 50:
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:LOAD:AUTO ON"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD:AUTO ON)",m_Handle), Response, BufferSize);
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:IMP 50"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:IMP 50)",m_Handle), Response, BufferSize);
						break;
					case 75:
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:LOAD:AUTO ON"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD:AUTO ON)",m_Handle), Response, BufferSize);
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:IMP 75"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);
						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:IMP 75)",m_Handle), Response, BufferSize);
						break;
					case 1000000:
						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:LOAD INF"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);

						IFNSIM(Status = viPrintf(m_Handle, "OUTP1:IMP 50"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);

						IFNSIM(Status = viPrintf(m_Handle, "SOUR:VOLT:LEV:IMM:AMPL 0.35"));
						if(Status)
							ErrorArb_AgE1445A(Status, Response, BufferSize);

						ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP1:LOAD INF;:OUTP1:IMP 50;:SOUR:VOLT:LEV:IMM:AMPL 0.35)",m_Handle), Response, BufferSize);
						break;
					default:
						ATXMLW_ERROR(Status,"Wrap-CArb_AgE1445A","No impedence value present",Response,BufferSize);
						break;
				}

				IFNSIM(Status = viPrintf(m_Handle, "SOUR:FUNC:SHAP DC"));
				
				if(Status)
				{
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				}
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, SOUR:FUNC:SHAP DC)",m_Handle), Response, BufferSize);

				// Set DC Signal Amplitude (Voltage)
				sprintf(StringToInst, "SOUR:VOLT:LEV:IMM:AMPL %12.4fV",m_DcComp.Real);
				IFNSIM(Status = viPrintf(m_Handle, StringToInst));
				// This command seems to result in error -221 frequently on both TETS and VIPERT??? The error does no display in RT on VIPERT however
				// For now....Clearing the device status buffer so that this doesn't produce an error/warning display in RT
                IFNSIM(Status = viPrintf(m_Handle, "*CLS"));
				
				if(Status)
				{
					ErrorArb_AgE1445A(Status, Response, BufferSize);
				}
				ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);

				break;

			default:
				break;
		} // end of switch


    if(Status)
        ErrorArb_AgE1445A(Status, Response, BufferSize);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: DisableArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::DisableArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

	// Open action for the Arb_AgE1445A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-OpenArb_AgE1445A called "), Response, BufferSize);

    // Open output relay to disable output 
    IFNSIM(Status = viPrintf(m_Handle, "OUTP:STAT OFF"));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(m_Handle, OUTP:STAT OFF)"), Response, BufferSize);
    // End wavefrom generation
    IFNSIM(Status = viPrintf(m_Handle, "ABORT"));
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(m_Handle, ABORT)"), Response, BufferSize);

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableArb_AgE1445A
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::EnableArb_AgE1445A(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

	// Close action for the Arb_AgE1445A
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-EnableArb_AgE1445A called "), Response, BufferSize);

	if(this->m_TrigInfo.TrigExists == true)
	{
		// This function selects the external source that will start waveform output
	    IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY2:SOUR EXT"));
		if(Status)
			ErrorArb_AgE1445A(Status, Response, BufferSize);

		if(this->m_TrigInfo.TrigSlopePos == true)
		{
			// This function selects positive slope of the trigger for the external trigger 
			IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY2:SLOP POS"));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, ARM:STAR:LAY2:SLOP POS)",m_Handle), Response, BufferSize);
		}
		else
		{
			// This function selects negative slope of the trigger for the external trigger 
			IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY2:SLOP NEG"));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, ARM:STAR:LAY2:SLOP NEG)",m_Handle), Response, BufferSize);
		}
		
		// This function specifies the number of triggers; in this case it is set to infinity
		IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY2:COUN INF")); //7/9/2007 changed from "ARM:STAR:LAY2:COUN 1"
		if(Status)
			ErrorArb_AgE1445A(Status, Response, BufferSize);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, ARM:STAR:LAY2:COUN 1)",m_Handle), Response, BufferSize);

		// This function specifies the number of cycles per triggers; in this case it is set to 1
		IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY1:COUN 1")); //7/9/2007 changed from "ARM:STAR:LAY1:COUN INF"
		if(Status)
			ErrorArb_AgE1445A(Status, Response, BufferSize);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, ARM:STAR:LAY1:COUN INF)",m_Handle), Response, BufferSize);

		//Max time...

	}
	if(m_Burst.Exists)
	{
		IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY2:SOUR Immediate"));
		if(Status)
			ErrorArb_AgE1445A(Status, Response, BufferSize);

	
		if(m_BurstRepRate.Exists)
		{
		}
		else
		{
			IFNSIM(Status = viPrintf(m_Handle, "ARM:STAR:LAY2:COUN 1"));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, ARM:STAR:LAY2:COUN 1)",m_Handle), Response, BufferSize);

			sprintf(StringToInst, "ARM:STAR:LAY1:COUN %d", (int)(m_Burst.Real*m_Freq.Real));
			IFNSIM(Status = viPrintf(m_Handle, StringToInst));
			if(Status)
				ErrorArb_AgE1445A(Status, Response, BufferSize);
			ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, %s)",m_Handle, StringToInst), Response, BufferSize);
		
		}
	}
	// Initiate the wavefrom
	if(m_Periodic.Int != STD_PERIODIC_DC)
		IFNSIM(Status = viPrintf(m_Handle, "INIT:IMM"));
	if(Status)
		ErrorArb_AgE1445A(Status, Response, BufferSize);
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, INIT:IMM)",m_Handle), Response, BufferSize);

	// Close output relay to enable output 
	IFNSIM(Status = viPrintf(m_Handle, "OUTP:STAT ON"));
	if(Status)
		ErrorArb_AgE1445A(Status, Response, BufferSize);
	ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_writeInstrData(%d, OUTP:STAT ON)",m_Handle), Response, BufferSize);

	this->m_TrigInfo.TrigExists = false;
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorArb_AgE1445A
//
// Purpose: Query Arb_AgE1445A for the error text and send to WRTS
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
//    <0   - Error AC_ and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int  CArb_AgE1445A_T::ErrorArb_AgE1445A(int Status,
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
        // Decode Arb_AgE1445A lib return code
        IFNSIM((Err = hpe1445a_errorMessage(m_Handle, Status, Msg)));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_errorMessage(%d, %d, %s)",m_Handle, Status, Msg), Response, BufferSize);

		if(Err)
            sprintf(Msg,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);

		// Timeout occurred before operation could complete
        if(Status == 0xBFFF0015)
            Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;

        //ATXMLW_ERRORResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);
    }

    QError = 1;
    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;
        IFNSIM((Err = hpe1445a_errorQuery(m_Handle, (ViPInt32)&QError, Msg)));
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("hpe1445a_errorQuery"), Response, BufferSize);

        if(Err != 0)
            break;
        if(QError)
        {
			// Timeout occurred before operation could complete
			if(QError == 0xBFFF0015)
				Status = ATXMLW_WRAPPER_ERRCD_MAX_TIME;

			//ATXMLW_ERRORResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, Msg);
        }
    }

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoArb_AgE1445A
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
//    <0   - Error occurred and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CArb_AgE1445A_T::GetStmtInfoArb_AgE1445A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    Status = 0;
    char   Name[ATXMLW_MAX_NAME];
    char  *InputNames;

	if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription,
                             Response, BufferSize)))
         return(Status);

	m_Action = atxmlw_Get1641SignalAction(m_SignalDescription,
								Response, BufferSize);

    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action),
                              Response, BufferSize);
	
    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]",
                              m_SignalName, m_SignalElement),
                              Response, BufferSize);

	    strncpy(Name,m_SignalName,ATXMLW_MAX_NAME);

    //  Get Input signal "Port" names
    if(atxmlw_Get1641SignalIn(m_SignalDescription, &InputNames))
    {
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s In [%s]",
                             Name, InputNames),
                              Response, BufferSize);
        //------ Possibly validate InputNames --------
    }

	if((GetSignalSrcChar(Name, InputNames, Response, BufferSize)))
    {
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Signal Chars for [%s]", Name),
                              Response, BufferSize);
    }

    atxmlw_Close1641Xml(&m_SignalDescription);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateArb_AgE1445A
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
void CArb_AgE1445A_T::InitPrivateArb_AgE1445A(void)
{
    m_AgeRate.Exists		= false;
	m_AgeRate.Int			= 0;
	m_AgeRate.Real			= 0.0;	
	
	m_StimSize.Exists=false;

	m_Amplitude.Exists		= false;
	m_Amplitude.Int			= 0;
	m_Amplitude.Real		= 0.0;	

	m_Bandwidth.Exists		= false;
	m_Bandwidth.Int			= 0;
	m_Bandwidth.Real		= 0.0;	

	m_Burst.Exists			= false;
	m_Burst.Int				= 0;
	m_Burst.Real			= 0.0;	
	m_BurstRepRate.Exists   = false;
	m_BurstRepRate.Int      = -1;

	m_CenterFreq.Exists		= false;
	m_CenterFreq.Int		= 0;
	m_CenterFreq.Real		= 0.0;	
	
	m_DcComp.Exists			= false;
	m_DcComp.Int			= 0;
	m_DcComp.Real			= 0.0;	

	m_EventDelay.Exists		= false;
	m_EventDelay.Int		= 0;
	m_EventDelay.Real		= 0.0;	

	m_EventSlope.Exists		= false;
	m_EventSlope.Int		= 0;
	m_EventSlope.Real		= 0.0;	

	m_EventVoltage.Exists	= false;
	m_EventVoltage.Int		= 0;
	m_EventVoltage.Real		= 0.0;	

	m_Freq.Exists			= false;
	m_Freq.Int				= 0;
	m_Freq.Real				= 0.0;	

	m_FreqWindow.Exists		= false;
	m_FreqWindow.Int		= 0;
	m_FreqWindow.Real		= 0.0;	

	m_MaxTime.Exists		= false;
	m_MaxTime.Real			= 10000;

	m_Period.Exists			= false;
	m_Period.Int			= 0;
	m_Period.Real			= 0.0;	

	m_PulseWidth.Exists		= false;
	m_PulseWidth.Int		= 0;
	m_PulseWidth.Real		= 0.0;	

	m_RiseTime.Exists		= false;
	m_RiseTime.Int			= 0;
	m_RiseTime.Real			= 0.0;	

	m_SampleSpacing.Exists	= false;
	m_SampleSpacing.Int		= 0;
	m_SampleSpacing.Real	= 0.0;	

	m_Sync.Exists			= false;
	m_Sync.Int				= 0;
	m_Sync.Real				= 0.0;	

	m_TEI.Exists			= false;
	m_TEI.Int				= 0;
	m_TEI.Real				= 0.0;	


    memset(&m_TrigInfo,0,sizeof(m_TrigInfo));
    memset(&m_GateInfo,0,sizeof(m_GateInfo));

	this->m_TrigInfo.TrigExists	= false;
	this->m_TrigInfo.TrigDelay	= 0.0;
	this->m_TrigInfo.TrigExt	= false;
	this->m_TrigInfo.TrigLevel	= 0.0;
	sprintf(this->m_TrigInfo.TrigPort, "");
	this->m_TrigInfo.TrigSlopePos = true;

	this->m_GateInfo.GateExists	= false;
	this->m_GateInfo.GateStartDelay	= 0.0;
	this->m_GateInfo.GateStartExt	= false;
	this->m_GateInfo.GateStartLevel	= 0.0;
	sprintf(this->m_GateInfo.GateStartPort, "");
	this->m_GateInfo.GateStartSlopePos = true;
	this->m_GateInfo.GateStopExt = true;
	this->m_GateInfo.GateStopLevel = 0.0;
	sprintf(this->m_GateInfo.GateStopPort, "");
	this->m_GateInfo.GateStopSlopePos = true;
		
	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataArb_AgE1445A
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
void CArb_AgE1445A_T::NullCalDataArb_AgE1445A(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: GetSignalSrcChar
//
// Purpose: Parse the 1641 for signal charistics starting with Name
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
//      0 - OK
//      - - Failure code
//   m_Signal char filled in accordingly
//
///////////////////////////////////////////////////////////////////////////////
bool CArb_AgE1445A_T::GetSignalSrcChar(char *Name, char *InputNames, char *Response, int BufferSize)
{
	bool   RetVal = true;
    char   Element[ATXMLW_MAX_NAME];
	char   *PulseDef;
	double PulseArray[6]={0,0,0,0,0,0};
	double DblValue;// DblValue2;
    char   Unit[ATXMLW_MAX_NAME];
    char  *NextName;
    char  *TempPtr;
    int    SumCount;
	char   SumName1[ATXMLW_MAX_NAME], SumName2[ATXMLW_MAX_NAME];

    Element[0] = '\0';
    Unit[0] = '\0';

    if(atxmlw_IsPortNameTsf(m_SignalDescription,InputNames,Name,&TempPtr))
    {
        //Found the Output Port Name
        strnzcpy(m_OutputChannel,Name,ATXMLW_MAX_NAME);
        if(TempPtr)
            GetSignalSrcChar(TempPtr,InputNames, Response, BufferSize);
    }
    else if((atxmlw_Get1641ElementByName(m_SignalDescription, Name, Element)))
	{
		if(ISELEMENT("Constant"))
		{
			m_Periodic.Exists = true;
			m_Periodic.Int = STD_PERIODIC_DC;
			if(ISDBLATTR("amplitude", &(m_DcComp.Real), m_DcComp.Dim))
                m_DcComp.Exists = true;
			if(ISDBLATTR("maxTime", &(m_MaxTime.Real), m_MaxTime.Dim))
				m_MaxTime.Exists = true;
		}
		else if(ISELEMENT("Ramp"))
		{
			m_Periodic.Exists = true;
            m_Periodic.Int = STD_PERIODIC_RAMP;
 			ISDBLATTR("amplitude", &(m_Periodic.Real), m_Periodic.Dim);
            if(ISDBLATTR("period", &(m_Freq.Real), m_Freq.Dim))
            {
                m_Freq.Exists = true;
                m_Freq.Real = 1 / m_Freq.Real;
            }
			if(ISDBLATTR("burst", &(m_Burst.Real), m_Burst.Dim))
				m_Burst.Exists = true;
			if(ISDBLATTR("maxTime", &(m_MaxTime.Real), m_MaxTime.Dim))
				m_MaxTime.Exists = true;
		}
		else if(ISELEMENT("Sinusoid"))
		{        
            m_Periodic.Exists = true;
            m_Periodic.Int = STD_PERIODIC_SINUSOID;
			ISDBLATTR("amplitude", &(m_Periodic.Real), m_Periodic.Dim);
			if(ISDBLATTR("frequency", &(m_Freq.Real), m_Freq.Dim))
                m_Freq.Exists = true;
			if(ISDBLATTR("frequencyBand", &(m_FreqWindow.Real), m_FreqWindow.Dim))
               m_FreqWindow.Exists = true;
			if(ISDBLATTR("centerFrequency", &(m_CenterFreq.Real), m_CenterFreq.Dim))
               m_CenterFreq.Exists = true;
			if(ISDBLATTR("ageRate", &(m_AgeRate.Real), m_AgeRate.Dim))
               m_AgeRate.Exists = true;

		}
		else if(ISELEMENT("SquareWave"))
		{
            m_Periodic.Exists = true;
            m_Periodic.Int = STD_PERIODIC_SQUAREWAVE;
            DblValue = 0.0;
			ISDBLATTR("amplitude", &(m_Periodic.Real), m_Periodic.Dim);
            if(ISDBLATTR("period", &DblValue, Unit) && (DblValue > 0.0))
            {
                m_Freq.Exists = true;
                m_Freq.Real = 1 / DblValue;
                strcpy(m_Freq.Dim,"Hz");
			}
		}
		else if(ISELEMENT("Triangle"))
		{
            m_Periodic.Exists = true;
            m_Periodic.Int = STD_PERIODIC_TRIANGLE;
            DblValue = 0.0;
            ISDBLATTR("amplitude", &(m_Periodic.Real), m_Periodic.Dim);
            if(ISDBLATTR("period", &DblValue, Unit) && (DblValue > 0.0))
            {
                m_Freq.Exists = true;
                m_Freq.Real = 1 / DblValue;
                strcpy(m_Freq.Dim,"Hz");
            }
		}
		else if(ISELEMENT("WaveformRamp") || ISELEMENT("WaveformStep"))
		{
            // For a FTIC treat like a sinusoid
            m_Periodic.Exists = true;
            m_Periodic.Int = STD_PERIODIC_WAVEFORMRAMP;
            DblValue = 0.0;
            ISDBLATTR("amplitude", &(m_Periodic.Real), m_Periodic.Dim);
            if(ISDBLATTR("period", &DblValue, Unit) && (DblValue > 0.0))
            {
                m_Freq.Exists = true;
                m_Freq.Real = 1 / DblValue;
                strcpy(m_Freq.Dim,"Hz");
            }

// Unused variables, actuall stored in m_StimData
//			// Get Stim Value from the 1641 signal description string, place data in StimData and count in StimCount
//			if((atxmlw_Get1641DoubleArrayAttribute(m_SignalDescription, "Waveform_Components", "stim",
//												StimData, &StimCount)))
//			{	
//			ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found AC-Component stim %s %d",
//												StimData,StimCount),
//												Response, BufferSize);
//			}

			if(ISDBLATTR("samplingInterval", &(m_SampleSpacing.Real), m_SampleSpacing.Dim))
			{
				m_SampleSpacing.Exists = true;
			}

			if(ISSTRATTR("points", &(TempPtr)))
			{
				m_StimSize.Exists=true;				
				string_to_array(TempPtr, &m_StimData[0], &m_StimSize.Int);
			}

		}
        if(ISELEMENT("Load"))
		{
            if(ISDBLATTR("resistance", &(m_TEI.Real), m_TEI.Dim))
            {
                m_TEI.Exists = true;
            }
		}
		else if(ISELEMENT("PulseTrain"))
		{	
			if(ISDBLATTR("repetition", &(DblValue), m_Burst.Dim))
			{				
				m_Burst.Exists = true;
				if(DblValue==1)//don't repeat
				{
					m_BurstRepRate.Exists=false;
					if(ISSTRATTR("pulses",&PulseDef))
					{
						sscanf(PulseDef, "[(%lf,%lf,%lf),(%lf,%lf,%lf)]", &PulseArray[0], &PulseArray[1], &PulseArray[2],
							&PulseArray[3], &PulseArray[4], &PulseArray[5]);
						m_Burst.Real=PulseArray[1];
					}
					else
						m_Burst.Exists = false;
				}
				else //calculate burstreprate
				{
					if(ISSTRATTR("pulses",&PulseDef))
					{
						sscanf(PulseDef, "[(%lf,%lf,%lf),(%lf,%lf,%lf)]", &PulseArray[0], &PulseArray[1], &PulseArray[2],
							&PulseArray[3], &PulseArray[4], &PulseArray[5]);
						m_BurstRepRate.Exists=true;
						m_BurstRepRate.Real=1/(PulseArray[1]+PulseArray[4]);
						m_Burst.Real=PulseArray[1];
					}
					else
						m_Burst.Exists = false;
				}
			}
			else //calculate burstreprate
			{
					if(ISSTRATTR("pulses",&PulseDef))
					{
						sscanf(PulseDef, "[(%lf,%lf,%lf),(%lf,%lf,%lf)]", &PulseArray[0], &PulseArray[1], &PulseArray[2],
							&PulseArray[3], &PulseArray[4], &PulseArray[5]);
						m_BurstRepRate.Exists=true;
						m_BurstRepRate.Real=1/(PulseArray[1]+PulseArray[4]);
						m_Burst.Real=PulseArray[1];
					}
					else
						m_Burst.Exists = false;
			}
		}
		else if(ISELEMENT("Attenuator"))
		{
            if(ISDBLATTR("gain", &(m_Attenuator.Real), m_Attenuator.Dim))
            {
                m_Attenuator.Exists = true;
            }
		}
		else if(ISELEMENT("LowPass"))
		{
            if(ISDBLATTR("cutoff", &(m_Bandwidth.Real), m_Bandwidth.Dim))
            {
                m_Bandwidth.Exists = true;
            }
		}
        //  Handle Sum
		else if(ISELEMENT("Sum"))
		{
            if(ISSTRATTR("In",&TempPtr))
            {
                SumCount = sscanf(TempPtr,"%s %s",SumName1,SumName2);
                if(SumCount > 0)
                    GetSignalSrcChar(SumName1,InputNames,Response,BufferSize);
                if(SumCount > 1)
                    GetSignalSrcChar(SumName2,InputNames,Response,BufferSize);
            }
        } 

		//  Get Trigger Info from SYNC on Measure
        if(ISSTRATTR("Sync",&NextName))
        {
            ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Sync [%s]",
                                Name, NextName),
                                Response, BufferSize);
            if((atxmlw_Get1641StdTrigChar(m_SignalDescription, NextName, InputNames, 
                                &m_TrigInfo)))
                ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Trig True for [%s]", NextName),
                                Response, BufferSize);
        }
		
		//  Get Gate Info from GATE on Measure
        if(ISSTRATTR("Gate",&NextName))
        {
            ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s Gate [%s]",
                                Name, NextName),
                                Response, BufferSize);
            if((atxmlw_Get1641StdGateChar(m_SignalDescription, NextName, InputNames, 
                                &m_GateInfo)))
                ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Gate True for [%s]", NextName),
                                Response, BufferSize);
        }


        // Find the next signal name or port name
        if(ISSTRATTR("In",&TempPtr))
        {
            GetSignalSrcChar(TempPtr,InputNames, Response, BufferSize);
        } 
    }
    else
    {
        // Unknown name ??
        RetVal=false;
    }
    return(RetVal);
}

///////////////////////////////////////////////////////////////////////////////
// Function: SanitizeArb_AgE1445A
//
// Sets instrument to lowest stim and highest impedance settings. Opens all output relays
//
//
///////////////////////////////////////////////////////////////////////////////
void CArb_AgE1445A_T::SanitizeArb_AgE1445A(void)
{
	ATXMLW_INTF_RESPONSE* Response;
	int Buffersize;

	//ViUInt32 retCnt = 0;
	IFNSIM(Status = viPrintf(m_Handle, "OUTP OFF"));
	IFNSIM(Status = viPrintf(m_Handle, "OUTP:LOAD INF"));
	IFNSIM(Status = viPrintf(m_Handle, "FREQ 0.0"));
	IFNSIM(Status = viPrintf(m_Handle, "SOUR:VOLT:LEV:IMM:AMPL 0.35"));
	
	if(Status)
	{
        // Just read any error(s) that might be present
		// keeps throwing error relating to voltage level, but only on this first msg
		ErrorArb_AgE1445A(Status, Response, Buffersize);
	}

	// Vi buffer seems to be getting corrupt 
	//IFNSIM(viWrite(m_Handle, (ViBuf)"OUTP OFF", 256, &retCnt));
	//IFNSIM(viWrite(m_Handle, (ViBuf)"OUTP:LOAD INF", 256, &retCnt));
	//IFNSIM(viWrite(m_Handle, (ViBuf)"FREQ 0.0", 256, &retCnt));
	//IFNSIM(viWrite(m_Handle, (ViBuf)"SOUR:VOLT:LEV:IMM:AMPL 0.35", 256, &retCnt));
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
static void string_to_array(char input [], double array [], int * size)
{
	char *string;
	char *temp;
	int i=0;
	string=input;
	//strcpy(string, input);

	temp=strtok(string, ",");
	do
	{
		array[i]=0;
		array[i]=atof(temp);
		i++;
		temp=strtok(NULL, ",");
		if(temp)
			temp++;
	}while(temp!=NULL);
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

		sprintf(TmpBuf, "%s", DEBUGIT_1445Arb);
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