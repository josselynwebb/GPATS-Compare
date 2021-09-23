 //2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dwg_T.cpp
//
// Date:	    11-OCT-05
//
// Purpose:	    Instrument Driver for Dwg

//
// Instrument:	Dwg  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     cem.lib              \usr\tyx\lib
//     cemsupport.lib       ..\..\Common\Lib
//
// ATLAS Subset: PAWS-85
//
//
//                          Function Number Map
//
// FNC                           Signal Description
// ---  -------------------------------------------------------------------------
//////// Place Dwg specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE 
//   1  sensor (voltage)   dc signal
//   2  sensor (ac-comp)   dc signal
//
//  11  sensor (voltage)   ac signal
//  12  sensor (dc-offset) ac signal
/* End TEMPLATE_SAMPLE_CODE */
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline       
// 1.0.1.0  26SEP06  Correct reading ID field, Dynamic byte retrieved as a static operation. 
// 1.4.0.1  13AUG07  Added SIMCICL back in. 
// 1.4.1.1  10AUG09  Added reset channels code for Remove Logic Data. 
// 1.4.2.0  10AUG09  Added code to fix problem of removing all pins   EADS NA T&S
//                   when using REMOVE, LOGIC DATA on selected pins 
//
///////////////////////////////////////////////////////////////////////////////
// Includes
/* Begin TEMPLATE_SAMPLE_CODE 
#include <float.h>
#include <math.h>
/* End TEMPLATE_SAMPLE_CODE */
#include <math.h>
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Dwg_T.h"


#define CARD_HV           11
#define DRIVE_HV         101
#define READ_HV          102

#define SETUP_STIM        10 //STIMULATE
#define SETUP_RESP        11 //SENSE
#define SETUP_DUAL_RESP   12 
#define DO_DIGITAL         1
#define STIM_ONLY          1
#define RESP_COMP          2
#define STIM_RESP_COMP     3
#define STIM_RESP_SAVE     4
#define RESP_ONLY          5
#define PARTIAL_REMOVE     6


#define DEFAULT_VOLT_ONE_STIM  4.0
#define DEFAULT_VOLT_ZERO_STIM 0.2
#define DEFAULT_VOLT_ONE_RESP  2.0
#define DEFAULT_VOLT_ZERO_RESP 1.8
#define MIL_TTL_VOLT_ZERO_RESP 0.4
#define DEFAULT_CURR_SOUR_RESP -0.010
#define DEFAULT_CURR_SINK_RESP 0.010
#define DEFAULT_VOLT_COMM 2.5




#define STIMPINS 1001

#define CAL_TIME       (86400 * 365) /* one year */
#define DEFAULT_SLEW_RATE  TERM9_SLEWRATE_MEDIUM

#define DYNAMIC 1
#define STATIC  2

ViInt32 pinlist[TOTAL_PINS];
#define EVENTS_DELTA		  32

static int  FudgeEq (double , double);
static int  TisStimPin(int *StimPinList, int StimPinCount, int RespPin);

HEV  Events_Read(int hEvent);
void Events_Update(int hEvent, double dTimeVal, int hRef);
void Events_Delete(int hEvent);
void Events_Clear();

void Events_Dump();
int			   m_RespChnlCnt;

////////////////////////////////////////////////////////////////////////////
// Function: TisStimPin(int *StimPinList, int StimPinCount, int RespPin)
//
// Purpose: Returns True if pin used for Stim and Response
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// StimPinList      int*            Pointer to list of stim pins
// StimPinCount     int             StimPinList length
// RespPin          int             Resp Pin being programmed
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
static int TisStimPin(int *StimPinList, int StimPinCount, int RespPin)
{
    int n;

    for(n=0;n<StimPinCount;n++)
    {
        if(StimPinList[n] == RespPin)
            return(n);
    }
    return(-1);
}

////////////////////////////////////////////////////////////////////////////
// Function: FudgeEq (double x, double y)
//
// Purpose: Compares two decimal values for equality
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Handle           ViSession       Device Handle
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
static int FudgeEq (double x, double y)
{
	if ((fabs(x*1.01) >= fabs(y)) && (fabs(x*0.99) <= fabs(y)))
		return 1;
	else
		return 0;
}

// Static Variables

// Local Function Prototypes
////////////////////////////////////////////////////////////////////////////
// Function: ClearStmtMemberVars(void)
//
// Purpose: Reset all Statement specific member variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
void CDwg_T::ClearStmtMemberVars(void)
{
	m_VoltOne.Exists         = FALSE;
	m_VoltZero.Exists        = FALSE;
	m_RespDelay.Exists       = FALSE;
	m_Circulate.Exists       = FALSE;
	m_MaxTime.Exists         = FALSE;
	m_TrigSource.Exists      = FALSE;
	m_ThrshVolt.Exists       = FALSE;
	m_CirculateCont.Exists   = FALSE;
//	m_StimClockSource.Exists = FALSE; // Required accross SETUP / SETUP / DO Boundry
//	m_StimClockFreq.Exists   = FALSE; // 02/19/07- Don't know when to clear?
//	m_RespClockSource.Exists = FALSE;
	m_RespClockFreq.Exists   = FALSE;
	m_VoltageQuies.Exists	 = FALSE;
	m_CurrentOne.Exists		 = FALSE;
	m_CurrentZero.Exists	 = FALSE;

//eads
	//digitalArray

//	if (m_digitalValue !=NULL) free(m_digitalValue);
//    m_digitalValue = NULL;
	    
	//stimOneArray
	
	//stim

	if(m_pStimPins!=NULL)
		free(m_pStimPins);
    m_pStimPins = NULL;
	
	if(m_pStimValue!=NULL)
	free(m_pStimValue);
    m_pStimValue = NULL;

  //response
 
	 if(m_pRespValue!=NULL)  //03/14/07a-
	   free(m_pRespValue);
     m_pRespValue = NULL; 
	m_wasDynamic=false;  //03/14/07a-
	 m_StimPcnt=0;

// Clear Do Digital Arrays
	m_RespOnly = 0;
	m_StmRespSave = 0;
	m_errorIndex=0;
	m_hvStimCount=0;
	m_hvRespCount=0;
	m_PatCount = 0;

//	if(m_MaskBinData != NULL)
//		free(m_MaskBinData);
//	m_MaskBinData = NULL;

    m_RespByteCnt = 0;
    m_RespBytesPerPat = 0;

	if(m_ErrByteData != NULL)
	{//		free(m_ErrByteData);
    m_ErrByteData = NULL;
	}
    m_ErrByteCnt = 0;
	m_littleWord=0;

    m_ErrIdxCnt = 0;
    m_FaultCnt = 0;

    return;
}
////////////////////////////////////////////////////////////////////////////
// Function: InitMemberVars(void)
//
// Purpose: Reset all member variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
bool s_InitArrays = TRUE;
void CDwg_T::InitMemberVars(void)
{
	int n,m;

	// Set array pointers to NULL initially
	// ClearStmtMemberVars will "free" non-null pointers
	if(s_InitArrays)
	{
		//m_MaskBinData = NULL;
		m_ErrByteData = NULL;

//eads
		//digitalValue
		//	m_digitalValue=NULL;
		//stimOne array
		//stim
		m_pStimPins=NULL;
		m_pStimValue=NULL;

		//resp
//	if(m_pRespValue!=NULL)      //03/14/07a-
		m_pRespValue=NULL;  
//	  free(m_pRespValue);

		runOnce=0;
		m_retFormat=0;

		//m_patternBuffer=NULL;
//eads-end


		m_StimOne.Exists=FALSE;
		s_InitArrays = FALSE;
	}
	// m_StimClockSource.Exists = FALSE; // Required accross SETUP / SETUP / DO Boundry
	m_StimClockFreq.Exists   = FALSE; // Only cleared on init and reset.
	// m_RespClockSource.Exists = FALSE;
	m_RespClockFreq.Exists   = FALSE;

    ClearStmtMemberVars();

	//Initialize flag for all card to set reference ground once

	for(n=1;n<=TOTAL_CARDS;n++)
    {
        for(m=0;m<2;m++)
        {
            m_Td925setup[n].Level[m].used.stim = FALSE;
            m_Td925setup[n].Level[m].used.resp = FALSE;
        }
		m_Td925setup[n].SetGndLvl = 0;
    }

	// Initialize Reset list of current pins in use
	for (n=0;n<TOTAL_PINS;n++)
	    m_Td925pin[n].reset   =  0; 

    for (n=0; n<=TOTAL_CARDS; n++)
	{
		for(m=0;m<PINS_PER_CARD;m++)
		{
			m_Td925setup[n].PinIdx[m].lvlidx.stim = 0;
			m_Td925setup[n].PinIdx[m].lvlidx.resp = 0;
        }
	}
       goforcnxflag = false;
	   Fnc1st = 0;
    return;
}

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CDwg_T(int Bus, int PrimaryAdr, int SecondaryAdr,
//                      int Dbg, int Sim)
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Bus              int             Bus number
// PrimaryAdr       int             Primary Address (MTA/MLA)
// SecondaryAdr     int             Secondary Address (MSA)
// Dbg              int             Debug flag value
// Sim              int             Simulation flag value (0/1)
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance.
//
///////////////////////////////////////////////////////////////////////////////
CDwg_T::CDwg_T(char *DeviceName, 
                       int Bus, int PrimaryAdr, int SecondaryAdr,
                       int Dbg, int Sim)
{
      if(DeviceName)
        strcpy(m_DeviceName,DeviceName);

    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;
    m_ResourceName[0] = '\0';
    ViStatus Status = 0;
    char s_Msg[256];
//    char *ErrMsg;

    // Reset all member variables
    InitMemberVars();

    // Initialize the M9
    CEMDEBUG(5,cs_FmtMsg("M9 Class called with Device [%s], "
                         "Bus %d, PA %d, SA %d, Sim %d, Dbg %d", 
                          DeviceName, Bus, PrimaryAdr, SecondaryAdr, Sim, Dbg));
	// 9/21/06 - these lines added such that term9 software simulation can be used. Set the debug value in busconfi to 11.
	 if(m_Dbg == 12)
	 { 
//		Status = terM9_loadSimulationConfiguration ("C:\\vxipnp\\winnt\\term9\\tets_Simulation_m9_config.txt");
		Status = terM9_loadSimulationConfiguration ("C:\\usr\\Tyx\\Sub\\IEEE716.89\\VIPERT\\Station\\tets_Simulation_m9_config.txt");// added to directory on site stations 10/17/06
	    sprintf(s_Msg, "TERM9::#s%d",PrimaryAdr);
     }
	 else sprintf(s_Msg, "TERM9::#%d",PrimaryAdr);
	// sprintf(s_Msg, "TERM::#%d",PrimaryAdr);  // 11/15/06 REVISED TO BE THE ONLY M9 IN THE SYSTEM

    IFNSIM(Status = terM9_init(s_Msg, VI_FALSE, VI_TRUE, &m_Handle));
    CEMDEBUG(10,cs_FmtMsg("terM9_init(%s, VI_FALSE, VI_TRUE, &m_Handle)", s_Msg));
	// Status = 0; // for debugging offline
	if (Status == TERM9_WARN_SOFTWARE_SIM)
	{ CEMDEBUG(10,cs_FmtMsg("term9 in Software Simulation mode %d", Status));
	 Status = 0;
	}
	if (Status)  
    {
		//ErrorM9(m_Handle, Status, "terM9_init");
		INSTERROR(10,cs_FmtMsg("terM9_init"));
        return;
    }

    Sleep(2000);
    IFNSIM(Status = terM9_reset(m_Handle));
	CEMDEBUG(5,cs_FmtMsg("class init terM9_reset(%d)",m_Handle));
    IFNSIM(Status = terM9_setLowPower(m_Handle,TERM9_SCOPE_SYSTEM,VI_FALSE));
    Sleep(2000);
	IFNSIM(Status = terM9_setLevelRange (m_Handle,TERM9_SCOPE_SYSTEM,TERM9_LEVEL_RANGE_AUTO));
	CEMDEBUG(10,cs_FmtMsg("terM9_setLevelRange(%d, TERM9_SCOPE_SYSTEM, TERM9_LEVEL_RANGE_AUTO)",m_Handle));

	//eads- REQUIRED in later dti driver versions
	IFNSIM(Status = terM9_setSystemEnable(m_Handle, VI_TRUE));
	IFNSIM(Status = terM9_setLowPower(m_Handle, TERM9_SCOPE_SYSTEM, VI_FALSE));

	// Check Call Status
	if (Status)  
	{
	 ErrorM9(m_Handle, Status, "terM9_setLevelRange after initial reset");
	 CEMDEBUG(10,cs_FmtMsg("terM9_setLevelRange(%d, TERM9_SCOPE_SYSTEM, levels setup error, %d)", m_Handle,Status));
	 Status = 0;
	}

	//  cs_GetUniqCalCfg(DeviceName, CAL_TIME, &ErrMsg);
	 Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);
	if (Status)  
	{
	 ErrorM9(m_Handle, Status, "cs_GetUniqCalCfg tagged.");
	 Status = 0;
	 CEMDEBUG(10,cs_FmtMsg("cs_GetUniqCalCfg(%d, CAL_TIME, CAL Data %d, CAL_DATA_COUNT, SIM %d)",DeviceName,m_CalData[0],Sim));
	}
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CDwg_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CDwg_T::~CDwg_T()
{
    ViStatus Status = 0;

    CEMDEBUG(5,cs_FmtMsg("Class Destructor called for Device [%s]",
                          m_DeviceName));
    // Reset the M9
    IFNSIM(Status = terM9_reset(m_Handle));
 	if (Status)
		{
		//ErrorM9(m_Handle, Status, "terM9_reset");
		INSTERROR(10,cs_FmtMsg("terM9_reset"));
        return;
		}
   CEMDEBUG(5,cs_FmtMsg("Destructor terM9_reset(%d)", m_Handle));
	// Release the handle
    IFNSIM(Status = terM9_close(m_Handle));
 	if (Status)
		{
		//ErrorM9(m_Handle, Status, "terM9_close");
		INSTERROR(10,cs_FmtMsg("terM9_close"));
        return;
		}
   CEMDEBUG(5,cs_FmtMsg("terM9_close(%d)", m_Handle));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusDwg(int Fnc)
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::StatusDwg(int Fnc)
{
    int Status = 0;
   
    CEMDEBUG(5,cs_FmtMsg("StatusDwg (%s) called FNC %d", 
                          m_DeviceName, Fnc));
 
	
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupDwg_T(int Fnc)
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
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::SetupDwg(int Fnc)
{
    int		Status = 0;
	int i=0;

    CEMDEBUG(5,cs_FmtMsg("SetupDwg (%s) called FNC %d",m_DeviceName, Fnc));
    
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
	{
	 CEMDEBUG(10,cs_FmtMsg("Station Status is bogus from Setup utility call (%s) called FNC %d  STATUS %d ",m_DeviceName, Fnc,Status));
     return(0);
	}
    if (Fnc == 911) // check for CHANGE verb. Clear the comments flag if so.
    {
      Fnc -=900;
      goforcnxflag = false;
    }

   // CEMDEBUG(5,cs_FmtMsg("cs_checkstationstatus returned a %d",Status));
#ifndef SIMCICLDEBUG
	 IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName,"<Signal></Signal>", NULL, 0)));
#endif

	DATUM *datumIter;
	 ClearStmtMemberVars(); //03/14/07a-
    // CEMDEBUG(5,cs_FmtMsg("cfinished clearstmtvaribles %d",Status));
	
//    if((Status = GetStmtInfoDwg(Fnc)) != 0)
  //      return(0);
    Status = GetStmtInfoDwg(Fnc);

	Fnc1st = Fnc;

	switch(Fnc)
    {
	DATUM* datum;
//	DATUM* datumI;					BUILD_WARN

	case FNC_DIGITAL_CONFIG:

		 if((datum=RetrieveDatum(M_DTMD,K_SET))!=NULL)
			{
				m_inDynamic=1;
				m_tsetIndex=0;
				switch(DSCDatVal(datum, 0))
				{
					case D_ON:/* starting of DO-TIMED-DIGITAL	*/
						m_firstTime=1;
						m_globalPatternIndex=0;
					//	m_globalStimPatternIndex=0;
						
						m_respBuffCount=0;
						m_fetchCounter=0;
						m_wasDynamic=false;
						
						
						for(i=0;i<(32*1024);i++)
							for(int j=0;j<TOTAL_PINS+1;j++)    
								m_patternBuffer[i][j]=0;

						m_iterations=1;
						
						if(datumIter=RetrieveDatum(M_ITER,K_SET))
						{	
							m_iterations=INTDatVal(datumIter,0);
							if (m_iterations <=0) m_iterations=1;
						//	FreeDatum(datumIter);
						}
					 FreeDatum(datum);
					break;
					case D_OFF:/* exit of DO-TIMED-DIGITAL	*/
						
						m_wasDynamic=true;
			    	//	FreeDatum(datum);
					break;
				}
			}		 
		 CEMDEBUG(10,cs_FmtMsg("Setup Digital Config, datum= %x",datum));  //03/07/07-
		 // FreeDatum(datum);

		 DoDigitalSetup(Fnc);
		
	break;
	
    case FNC_APLY_STATIC_LOGIC_DATA_DS:
	case FNC_STIM_STATIC_LOGIC_DATA:
	       SetupStim(Fnc);
	break;

	case FNC_MEAS_STATIC_LOGIC_DATA_DS:
	case FNC_SENS_STATIC_LOGIC_DATA:
	case FNC_PROV_STATIC_LOGIC_DATA:
	
	       SetupResp(Fnc);
	break;
	
	case FNC_APLY_STATIC_LOGIC_DATA_SCAN:
	case FNC_MEAS_STATIC_LOGIC_DATA_SCAN:

		   setupHV(Fnc);
	break;

	case FNC_EVNT_EBE_CALL_TBE_SBE_2:
	case FNC_EVNT_EBE_CALL_EBE:				
	case FNC_EVNT_EBE_CALL_TBE_SBE_3:
	case FNC_EVNT_TBE_2:
	case FNC_EVNT_SBE_2:
	case FNC_EVNT_TBE_3:
	case FNC_EVNT_SBE_3:

			setupEvents(Fnc);

	break;

	default:
	break;
	}
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateDwg(int Fnc)
//
// Purpose: Perform the Initiate action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::InitiateDwg(int Fnc)
{
    int   Status = 0;

    // Initiate action for the Dwg
    CEMDEBUG(5,cs_FmtMsg("InitiateDwg (%s) called FNC %d",m_DeviceName, Fnc));
 	int ret=-1, recOrGenMode=0, n=0, m=0;
	int pinCnt,pinSiz;				// Number of pin count
	int *pPins = NULL;				// List of pins being retrieved
	int *StimPins = NULL;			// List of pins being retrieved
	int *RespPins = NULL;			// List of pins being retrieved
	int *pPinsBackup = NULL;		// List of pins being retrieved
	BOOL *tmpdata=NULL;				// Pointer to the array of rearranged dynamicdata
	BOOL *RespTmpData=NULL;			// Pointer to the array of rearranged dynamicdata
	BOOL *tmpdata2=NULL;			// Pointer to the array of rearranged dynamicdata
	BOOL *RespDynData = NULL;		// Array of data for normal data
	BOOL *dynamicdata = NULL;		// Array of data for normal data
	int tmp;						// Temporary value that is used to rearrange pins and data
	ViInt32 testResult=0; 
    int LastPatternResponse[TOTAL_PINS+8];
	int status;
	DATUM *pinDatO,*pinDatZ,*pinDatH;
	int count=0;
	DATUM *pd,*data;
	int dcnt,dsiz;
	int num;
	memset(LastPatternResponse,0,sizeof(LastPatternResponse));
	int total=0;
	ViUInt32 HVPinDriveState [] = { 0x80002aaa };
	ViUInt32 HVPinDetectState [] = {0x00000000};
	char* maskWordData;
	int tsetDebug=0;
	int iterationCounter=0;
	int counter=0;
	int byte=0;
	int bit=0;
	int hv=0;
	int j=0;
	int clearPins=0;
	int k=0;
	int i=0;
	int word=0;
	int patternIndex=0;
	int w=0;
	int indexer=0;

	switch(Fnc)
	{
	case FNC_APLY_STATIC_LOGIC_DATA_SCAN:
			{
			DATUM *datum;  //DATUM *datum = 0;  //03/15/07a-

			runOnceHV=1;//1 = flag to CDwg_T::Close so initiate is only ran once.
			CEMDEBUG(10,cs_FmtMsg("Entering: Apply High Voltage"));
			IFNSIM(status = terM9_setHighVoltagePinGroupThreshold(m_Handle,m_hvThreshold)); // set all the thresholds. as that is the way udbs work. 
			if(datum=RetrieveDatum(M_VALU,K_SET))
			{
				dcnt=DatCnt(datum);
				dsiz=DatSiz(datum);	
				char *pelm=DIGDatVal(datum,0);
			unsigned long UDBOutput = 0;    //first set all the defined channels off. 
			unsigned long UDBfromPAWS = 0;
			unsigned char  holdingbyte = 0;
			for (byte=0;byte<dsiz;byte++)
			{
			 holdingbyte = DIGDatByte(pelm,byte)&0xFF;
			 UDBfromPAWS |= (int)holdingbyte<<((dsiz-byte-1)*8); // retrieve the UDB polarities from PAWS
		    }
			for (byte = 0;byte<m_hvStimCount;byte++)
			{ 
			 if (m_hvPinsStim[byte].hvdrive_detectNot == 0x00) // 10/26/06 - If it was set as a detect flare a warning and change pin modes.  
			 {
			   ErrorM9(m_Handle,-10," Warning UDB channel was set for detect."); 
			   m_hvPinsStim[byte].hvdrive_detectNot = 1; 
			 }
			 UDBOutput = UDBfromPAWS & 0x00000001;
			  if (UDBOutput == 1)  // channels get flipped here because the shift is lsb to msb, and m_hvPinsStim[0].hvchnnl is always msb.
			  {
			    IFNSIM(status = terM9_setHighVoltagePinMode(m_Handle,m_hvPinsStim[m_hvStimCount-byte-1].hvchnnl,TERM9_HVPINMODE_OFF )); //10/30/06 was TERM9_HVPINMODE_ON
			    m_hvPinsStim[m_hvStimCount-byte-1].hvState =TERM9_HVPINMODE_OFF;
			  }
			  else 
			  {
			   IFNSIM(status = terM9_setHighVoltagePinMode(m_Handle,m_hvPinsStim[m_hvStimCount-byte-1].hvchnnl,TERM9_HVPINMODE_ON )); //10/30/06 was TERM9_HVPINMODE_OFF
			   m_hvPinsStim[m_hvStimCount-byte-1].hvState=TERM9_HVPINMODE_ON;
			  }
			 UDBfromPAWS = UDBfromPAWS >> 1;
 			if (Status) ErrorM9(m_Handle, Status, "terM9_InitiateHVDetectPattern");
			}
			FreeDatum(datum);  // free(datum); //03/14/07a- 
		   }
		  }
	break;
	case FNC_MEAS_STATIC_LOGIC_DATA_SCAN:  // 10/20/06 apparently this is the read mode. Don't initialize anything. Setup has set the udb pins. 
	{
		Status = 0;
		runOnceHV=1;
		CEMDEBUG(10,cs_FmtMsg("Entering Measure High Voltage")); 
		if(!m_VoltOne.Exists) m_hvThreshold = 1.5; //If the udb response group threshold wasn't defined in the setup, set to 1.5 volts;
		if(m_VoltOne.Exists) m_hvThreshold= m_VoltOne.Real/2;  // there is no low threshold for udbs so ignore. 
		if (m_hvThreshold<1.5) m_hvThreshold=1.5; // and if the threshod is less than 1.5 make it 1.5.
  		IFNSIM(status = terM9_setHighVoltagePinGroupThreshold(m_Handle,m_hvThreshold)); // set all the thresholds. as that is the way udbs work. 

		for (i =0;i <m_hvRespCount;i++)
		{ 
			if (m_hvPinsResp[m_hvRespCount-i-1].hvdrive_detectNot == 0x01) //10/24/06  If it is defined as drive toss out a warning and set to detect.
			{ 
			 ErrorM9(m_Handle,-10," Warning UDB channel was set for drive."); 
			 m_hvPinsResp[m_hvRespCount-i-1].hvdrive_detectNot = 0; 
			}
			IFNSIM(status = terM9_setHighVoltagePinMode(m_Handle,m_hvPinsResp[m_hvRespCount-i-1].hvchnnl,TERM9_HVPINMODE_OFF ));
			m_hvPinsResp[m_hvRespCount-i-1].hvState=TERM9_HVPINMODE_OFF;		
			if (Status) ErrorM9(m_Handle, Status, "terM9_InitiateHVDetectPattern");
		}
	  }
	break;
	case FNC_DIGITAL_CONFIG:
	{
	for(iterationCounter=0;iterationCounter<m_iterations;iterationCounter++)
	{
		if(m_inDynamic==1)
			{	
			 CEMDEBUG(10,cs_FmtMsg("In Dynamic Test" ));
			 for(j=0;j<m_globalPatternIndex;j++)
				{
					for(k=0;k<TOTAL_PINS;k++)
						{
							if(m_patternBuffer[j][k]==1)
							{
								IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_IL));
 								if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							//	CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IL) pattern group %d", m_Handle,k,j));
							}
							if(m_patternBuffer[j][k]==2)
							{
								IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_IH));
 								if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							//	CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IH) pattern group %d", m_Handle,k,j));
							}
							if(m_patternBuffer[j][k]==3)
							{
								IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_OL));
 								if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							//	CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_OL) pattern group %d", m_Handle,k,j));
							}
							if(m_patternBuffer[j][k]==4)
							{
								IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_IOX));
 								if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							//	CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IOX) pattern group %d", m_Handle,k,j));
							}
	
						}//end of total pins

					tsetDebug=m_patternBuffer[j][TOTAL_PINS];

					IFNSIM(Status = terM9_setDynamicPattern (m_Handle,1,tsetDebug, TERM9_COP_NOP,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE));  //01/16/07 changed to NOP was  TERM9_COP_HALT      LAST ROW IN PATTERN BUFFER ARRAY	//0,
					if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPattern");
					IFNSIM(Status = terM9_loadDynamicPattern(m_Handle, VI_FALSE, VI_FALSE));
					if (Status)ErrorM9(m_Handle, Status, "terM9_loadDynamicPattern");
					CEMDEBUG(10,cs_FmtMsg("terM9_loadDynamicPattern(%d, VI_FALSE, VI_FALSE)initiate global pattern index TERM9_COP_NOP tsetdebug %d %x", m_Handle,tsetDebug,j));
				}//global pattern index

				IFNSIM(Status = terM9_setDynamicPattern (m_Handle,1,0,TERM9_COP_HALT,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE));//1/16/07 changed from  TERM9_COP_NOP 
				if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPattern");
				IFNSIM(Status = terM9_loadDynamicPattern(m_Handle, VI_FALSE, VI_FALSE));
				if (Status) ErrorM9(m_Handle, Status, "terM9_loadDynamicPattern");
				CEMDEBUG(10,cs_FmtMsg("terM9_loadDynamicPattern(%d, VI_FALSE, VI_FALSE) initiate local pattern TERM9_COP_HALT tsetDebug %d ", m_Handle,tsetDebug));
				//Run "BURST" dynamic patterns using terM9_runDynamicPatternSet
				Status = checklevelSets();
				IFNSIM(Status = terM9_runDynamicPatternSet(m_Handle, VI_FALSE, &testResult));
				if (Status) ErrorM9(m_Handle, Status, "terM9_runDynamicPatternSet initiate fault");
				CEMDEBUG(10,cs_FmtMsg("terM9_runDynamicPatternSet(%d, VI_FALSE, %d)", m_Handle,testResult));
				if(m_iterations==1)
				{
					m_inDynamic=0;
					m_tsetIndex=0;
				}
		}//end of in dynamic
	  }	//end of iteration counter
	}
	break;
	case FNC_MEAS_STATIC_LOGIC_DATA_DS:
		{
			m_PatCount=1;
//				DATUM *pinData;
			int dcnt=0,dsiz=0;
			int temp=0;

			m_patternCount =1;
		
			DoDigitalSetup(Fnc);

			for (n=0; n<m_RespPcnt; n++)
		    	{
      				m_Td925pin[m_RespPins[n]].current =  1; // Flag current pins in use by Do-Digital Test
        			{

						IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(m_RespPins[n]),TERM9_OP_OL));
 						if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
						//CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_OL)", m_Handle,m_RespPins[n]));
		
						// Save last pattern state
						//LastPatternResponse[m_RespPins[n]] = TERM9_OP_IOX;
					}
    			}
			runBurst(Fnc);
		}//getDwgData(Fnc);
	break;	
	case FNC_APLY_STATIC_LOGIC_DATA_DS:
		{
			ViInt32 *tempStimArray;
			DATUM *stimData;
			int dcnt=0,dsiz=0;
	
			m_patternCount=1;
			tempStimArray=(ViInt32*)calloc(m_StimPcnt,sizeof(ViInt32));
			DoDigitalSetup(Fnc);

			runOnce=1;//1 = flag to CDwg_T::Close so initiate is only ran once.

			for(n=0;n<m_StimPcnt;n++)
			{	
				tempStimArray[n]=m_StimPins[n];
			}
			stimData=RetrieveDatum(M_VALU,K_SET);
			if (stimData == 0) 
			{ 
				Status = -2;
 				CEMDEBUG(5,cs_FmtMsg("No valid stim data defined for Channel: %d",tempStimArray[0]));
				ErrorM9(m_Handle,Status , "VALID STIMULUS IS NOT DEFINED.");
				free(tempStimArray);
				return -2;	//12/18/06 - added tag if an open ended apply logic data is encountered.
			}

			dcnt=DatCnt(stimData);
			dsiz=DatSiz(stimData);

			char *pByte;
			int *bitArray=(int*)malloc(dcnt*dsiz*8*sizeof(int));
			int chanIndexer=0;
			int pinCount;
			int pcnt=m_StimPcnt;
			pByte=DIGDatVal(stimData,0);
					
			for( byte=0;byte<dsiz;byte++)
			{
				if(pcnt%8==0)
				 pinCount=8;
				else 
				 pinCount=pcnt%8;
				 for( bit=pinCount-1;bit>=0;bit--)
					{
					num=(int)pow(2,bit);
						if(DIGDatByte(pByte,byte) & num)
						{
						bitArray[chanIndexer]=1;
						}	
					else
						{
						bitArray[chanIndexer]=0;
						}
					// CEMDEBUG(8,cs_FmtMsg("Assert DTI: Bit to %d of Byte %d on Channel: %d",bitArray[chanIndexer],byte,tempStimArray[chanIndexer]));
					chanIndexer++;
					}
				pcnt=pcnt-(8*byte)-pinCount;				
			}
		
			for( i=0;i<m_StimPcnt;i++)
			{
				if(bitArray[i]==1)
					{
 						IFNSIM(status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(tempStimArray[i]), TERM9_OP_IH))
 						if (status)	ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
						// CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IH)", m_Handle,tempStimArray[i]));
					}
				else
					{
						IFNSIM(status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(tempStimArray[i]), TERM9_OP_IL));
 						if (status)	ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
						// CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IL)", m_Handle,tempStimArray[i]));
					}
				//LastPatternResponse[tempStimArray[i]] = TERM9_OP_IOX;
			}
			free (stimData); //03/14/07-
			// free(m_StimPins); // 03/13/07-
			free(tempStimArray);   //04/30/07- may need to add a temp stim data save repository here for downstream measures. 
			free(bitArray);
			runBurst(Fnc);
		}
	break;
	case FNC_PROV_STATIC_LOGIC_DATA:
	case FNC_SENS_STATIC_LOGIC_DATA:
	{
		DATUM* datumW;
		DATUM* datum;			
		DATUM* ddat;
		DATUM  *maskOne,*maskZero;
		int dcnt, dsiz;
		int maskCount=0,maskSize=0;
		bool newLayer;
		int num;
		//m_referenceByteArray = NULL;
		m_referenceByteArrayflag = false;
		m_patternCount = 1;  // 9/26/06 - incase this hasn't been established set it for at least one pattern.
		m_comparewordsArrayflag = false;  //03/08/07-
		m_errorIdxflag = false;  //03/08/07-
	
		if(datumW=RetrieveDatum(M_WRDC,K_SET))
		{
			if(m_inDynamic==1)
			{
				m_respBuff[m_respBuffCount].size.wordCount=INTDatVal(datumW,0);
				m_respBuff[m_respBuffCount].size.wordSize=DatSiz(datumW);
				m_patternCount=m_respBuff[m_respBuffCount].size.wordCount;  //12/20/06- moved to here because datumw can be void
			}
			FreeDatum(datumW);
		}		
	    if(m_inDynamic==1 )
			{
			 if(m_stimFlag==1)
				{
					m_stimFlag=0;
					newLayer=false;
				}
			 else
				{
				newLayer=true;
				//m_globalPatternIndex=m_respBuff[m_respBuffCount].size.wordCount;
				if(m_firstTime==false)
					m_tsetIndex++;
				else
					m_firstTime=false;
				}
			}	
		if(ddat=RetrieveDatum(M_REFX,K_LOD))
			{
				dcnt=DatCnt(ddat);
				dsiz=DatSiz(ddat);
				if(m_inDynamic==1)
					{
						m_respBuff[m_respBuffCount].size.wordCount=dcnt;
						m_respBuff[m_respBuffCount].size.wordSize=dsiz;
						m_patternCount=m_respBuff[m_respBuffCount].size.wordCount;  //12/20/06- 
					}
				m_referenceWordCount=dcnt;
				m_referenceWordSize=dsiz;
				m_maskByteArray=NULL;

				if((maskOne=RetrieveDatum(M_MASK,K_LOD)) || (maskZero=RetrieveDatum(M_MSKZ,K_LOD)))
				{
					if(maskOne)
						{
							maskCount=DatCnt(maskOne);
							maskSize=DatSiz(maskOne);
							
							m_maskByteArray=(int*)malloc(maskCount*maskSize*sizeof(int));
							m_maskByteArrayflag = true;
							if(m_inDynamic==1)
								m_respBuff[m_respBuffCount].mask.array=(int*)malloc(maskCount*maskSize*sizeof(int));
							for( word=0;word<maskCount;word++)
								{
									maskWordData=DIGDatVal(maskOne,word);
									for( byte=0;byte<maskSize;byte++)
											{
												m_maskByteArray[word*maskSize+byte]=DIGDatByte(maskWordData,byte)&0xFF;	
												if(m_inDynamic==1)
														m_respBuff[m_respBuffCount].mask.array[word*maskSize+byte]=DIGDatByte(maskWordData,byte);
											}
								}
							FreeDatum(maskOne);
						}
					else
						{
							maskCount=DatCnt(maskZero);
							maskSize=DatSiz(maskZero);

							m_maskByteArray=(int*)malloc(maskCount*maskSize*sizeof(int));
							m_maskByteArrayflag = true;
							if(m_inDynamic==1)
								m_respBuff[m_respBuffCount].mask.array=(int*)malloc(maskCount*maskSize*sizeof(int));
							for( word=0;word<maskCount;word++)
								{
									maskWordData=DIGDatVal(maskZero,word);
									for( byte=0;byte<maskSize;byte++)
											{
												m_maskByteArray[word*maskSize+byte]=DIGDatByte(maskWordData,byte)&0xFF;	
												m_maskByteArray[word*maskSize+byte] = m_maskByteArray[word*maskSize+byte] ^ 0xFF;
												if(m_inDynamic==1)
													m_respBuff[m_respBuffCount].mask.array[word*maskSize+byte]=DIGDatByte(maskWordData,byte);
											}
								}
						FreeDatum(maskZero);
						}
				}
			
				m_referenceByteArray=(int*)calloc(dcnt*dsiz,sizeof(int)); // 03/14/07-
				m_referenceByteArrayflag = true;
				CEMDEBUG(11,cs_FmtMsg("m_refernceByteArray =%x  m_referenceByteArrayflag=%x dcnt=%x dsiz=%x isDynamic=%d ",m_referenceByteArray,m_referenceByteArrayflag,dcnt,dsiz,m_inDynamic));
				if(m_inDynamic==1)
						m_respBuff[m_respBuffCount].ref.array=(int*)malloc(dcnt*dsiz*sizeof(int));

				for(i=0;i<m_referenceWordCount;i++)
				{
					char *referenceWord=DIGDatVal(ddat,i);

					for(j=0;j<m_referenceWordSize;j++)
						
						{	
							m_referenceByteArray[i*m_referenceWordSize+j]=DIGDatByte(referenceWord,j)&0xFF;  // 12/12/06 mask to a byte. 
							if(m_inDynamic==1)
								m_respBuff[m_respBuffCount].ref.array[i*m_referenceWordSize+j]=DIGDatByte(referenceWord,j);
						}
				}
			// free(ddat); //FreeDatum(ddat);   //03/07/07- tried using FreeDatum, didn't seem to make a difference.
			}//END OF RETRIEVEDATUM REFX

		ViInt32 goodArray[TOTAL_PINS];
		int counter=0;
		int goodPins=0;

		if(datum=RetrieveDatum(M_RESP,K_FDD))
			{
				dcnt=DatCnt(datum);
				dsiz=DatSiz(datum);

				for(counter=0;counter<dcnt;counter++)
					{	
						num=INTDatVal(datum,counter);
						if(num!=0)
							{
								goodArray[goodPins]=num-200;
								if(m_inDynamic==1)
									m_respBuff[m_respBuffCount].pins.list[goodPins]=num-200;
								goodPins++;
							}
					}
					if(m_inDynamic==1)
						m_respBuff[m_respBuffCount].pins.count=goodPins;
					if ((Fnc == FNC_PROV_STATIC_LOGIC_DATA)|| (Fnc == FNC_SENS_STATIC_LOGIC_DATA))
						m_RespChnlCnt = goodPins;

					for( counter=0;counter<goodPins;counter++)
					{
						if(m_inDynamic==1)   // 12/19/06 check pole. for first initializaton.
						{			
							if(!newLayer)
								for( i=m_globalPatternIndex-m_patternCount;i<m_patternCount+m_globalPatternIndex;i++)
										{
											m_patternBuffer[i][goodArray[counter]]=3;
											m_patternBuffer[i][TOTAL_PINS]=m_tsetIndex;
										}
							else
								for( i=m_globalPatternIndex;i<m_patternCount+m_globalPatternIndex;i++)
										{
											m_patternBuffer[i][goodArray[counter]]=3;
											m_patternBuffer[i][TOTAL_PINS]=m_tsetIndex;
										}						
						// CEMDEBUG(11,cs_FmtMsg("response pattern set flag to TERM9_OP_OL in global pattern buffer %d pin %d",i,counter)); // 2/13/07- 
						}
						else
						{
							IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(goodArray[counter]),TERM9_OP_OL));
 							if (Status)
								ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
						//	CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_OL)", m_Handle,goodArray[counter]));
						}	
				// Save last pattern state
					//LastPatternResponse[goodArray[counter]] = TERM9_OP_IOX;
					m_RespPins[counter]=goodArray[counter];//need for fetch
					}
			FreeDatum(datum);		//12/21/06 - moved to within the if statement. 
			}	
		if (goodPins !=0) m_RespPcnt=goodPins;   //03/14/07-
		runBurst(Fnc);
		if(m_inDynamic==1)
			{
				m_respBuffCount++;
				if(newLayer==true)
					m_globalPatternIndex+=m_patternCount;
			}
	}
	break;
	case FNC_STIM_STATIC_LOGIC_DATA:
		{			//Get the stim data and pins and set pin opcodes to IH or IL for the following cases
		 if(m_inDynamic==1)
			{
			 m_stimFlag=1;
			 if(m_firstTime==false)
			 m_tsetIndex++;
			}
			count=0;		
			if(pd=RetrieveDatum(M_STIM,K_FDD))// Retrieve the channel description list pd
			{
			 pinCnt = DatCnt(pd);
			 pinSiz = DatSiz(pd);
			 pPins = (int*)malloc((pinCnt+8) * sizeof(int));
			 m_pStimPins=(ViInt32*)malloc((pinCnt+8)*sizeof(int));
					
			 for (n=0; n<pinCnt; n++)   
				{
				 tmp=INTDatVal(pd,n)-200;//-200 for block information in switch database. 
				 if(tmp!=-200)//need to account for (if any eg...) padded 0's in first word(32). eg....(1,2,3,4,5,6,7,8,9,10) looks like (0,0,0,0,0,0,1,2),(3,4,5,6,7,8,9,10)
					{
					 m_pStimPins[count] = (ViInt32) tmp;
					 pPins[count] =tmp;
					 m_Td925pin[pPins[count]-1].current =  1; // Flag current pins in use by Do-Digital Test(11)
					 count++;   // and update the legitimate channels count.  
					}
				 }
			 m_pStimPinsCnt=count; // now have the legitimate channels and word count. 
			 FreeDatum(pd);
			 int chanIndexer=0;
			 int pinCount=0;
			 int pcnt=count;
			 char stimbyte = 0;
			 // STIM
			 if(data=RetrieveDatum(M_STIM,K_LOD))  // now retrieve the stim patterns 
			 {
			  dcnt=DatCnt(data);//# of word patterns 
			  dsiz=DatSiz(data);//size of each word in bytes 
			  m_patternCount=dcnt;
 			  m_pStimValue=(ViInt32*)(malloc((m_patternCount*dsiz*8)*sizeof(ViInt32)));
			  int dSizeword = dsiz *8;
			  if (dSizeword < m_pStimPinsCnt) CEMDEBUG(10,cs_FmtMsg("the bits perword buffer %d is less than the number of channels %d", dSizeword,m_pStimPinsCnt));
//			  for( patternIndex=m_globalStimPatternIndex;patternIndex<m_patternCount+m_globalStimPatternIndex;patternIndex++)
			  for( patternIndex=m_globalPatternIndex;patternIndex<m_patternCount+m_globalPatternIndex;patternIndex++)
			  {
//			   char* charWord=DIGDatVal(data,patternIndex-m_globalPatternIndex); // pointer to the first word 
			   char* charWord=DIGDatVal(data,patternIndex-m_globalPatternIndex); // pointer to the first word 
  			   pinCount=m_pStimPinsCnt%8;  // if the stim words are not byte boundries then start with the odd bits.
			   if(m_pStimPinsCnt%8==0)pinCount=8;
			   byte=0;  //byte = -1;
			   for(pcnt=0;pcnt<m_pStimPinsCnt;++byte) // for( byte=0;byte<dsiz;byte++)
			   {
			    for( bit=pinCount-1;bit>=0;bit--,pcnt++)
				{
				 stimbyte = DIGDatByte(charWord,byte);  // 11/13/06 revised these two lines for debug. 
				 num=(int)pow(2,bit);
				 if( stimbyte & num)
				 {
				  if(m_inDynamic==1)
				  {		
				   m_patternBuffer[patternIndex][m_pStimPins[pcnt]]=2;
				  // CEMDEBUG(10,cs_FmtMsg("m_patternBuffer[%d][%d]=TERM9_OP_IH", patternIndex,m_pStimPins[pcnt]));
				   m_patternBuffer[patternIndex][TOTAL_PINS]=m_tsetIndex;
				  }
				  else
				  {
				   IFNSIM(status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(m_pStimPins[pcnt]), TERM9_OP_IH))
				  // CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IH)", m_Handle,m_pStimPins[pcnt]));
				  }	
				 }	
				 else
				 {
				  if(m_inDynamic==1)
				  {
				   m_patternBuffer[patternIndex][m_pStimPins[pcnt]]=1;
				 //  CEMDEBUG(10,cs_FmtMsg("m_patternBuffer[%d][%d]=TERM9_OP_IL", patternIndex,m_pStimPins[pcnt]));
				   m_patternBuffer[patternIndex][TOTAL_PINS]=m_tsetIndex;
				  }
				  else
				  {
				   IFNSIM(status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(m_pStimPins[pcnt]), TERM9_OP_IL)); //11/13/06 count correction
				  // CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IL)", m_Handle,m_pStimPins[pcnt]));
				  }
 				 }//end of set pin polarity 
			    } 
			   pinCount = 8; // this assumes there is more than one byte.
			  }//end of patterns 
			}//end of stim,lod
		  m_globalPatternIndex+=m_patternCount;				
		  FreeDatum(data);
		  runBurst(Fnc);
		 }//end of stim,fdd
			 //STMO
			 if(pinDatO	= RetrieveDatum(M_STMO,	K_FDD))
					{

					pinCnt = DatCnt(pinDatO);
					pinSiz = DatSiz(pinDatO);
					count=0;
					pPins = (int*)malloc((pinCnt+8) * sizeof(int));

					for (n=0; n<pinCnt; n++)
						{
							tmp=INTDatVal(pinDatO,n)-200;//-200 for block information in switch database
							if(tmp!=-200)
//  																	
								{
									pPins[count] = tmp;
									m_Td925pin[pPins[count]-1].current =  1; // Flag current pins in use by Do-Digital Test(21)
									count++;
								}
						}
					FreeDatum(pinDatO);  
					//01/22/07 need to add card and channel levels (VIH,VIL) check here for static stim function (initi fnc 10)
					for(n=0;n<count;n++)
						{	
							if(m_inDynamic==1)
								{		
									for( i=0;i<m_patternCount;i++)
										{
										m_patternBuffer[i][pPins[n]]=2;
										m_patternBuffer[i][TOTAL_PINS]=m_tsetIndex;
										}
								}
								else
								{
								 IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(pPins[n]),TERM9_OP_IH));
 							     if (Status)ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							    // CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IH)", m_Handle,pPins[n]));
								}
						}
					free(pPins);
					runBurst(Fnc);
		}

			 //STMZ
			 if(pinDatZ = RetrieveDatum(M_STMZ, K_FDD))
				{
			
					pinCnt = DatCnt(pinDatZ);
					pinSiz = DatSiz(pinDatZ);
						
				
					pPins = (int*)malloc((pinCnt+8) * sizeof(int));
					count=0;
					for (n=0; n<pinCnt; n++)
						{
							tmp=INTDatVal(pinDatZ,n)-200;//-200 for block information in switch database
							if(tmp!=-200)//need to account for padded 0's in first byte.  eg....(1,2,3,4,5,6,7,8,9,10)
														//looks like (0,0,0,0,0,0,1,2),(3,4,5,6,7,8,9,10)
								{
									pPins[count] = tmp;
									m_Td925pin[pPins[count]-1].current =  1; // Flag current pins in use by Do-Digital Test(31)
									count++;
								}
						}
					//01/22/07 need to add card and channel levels (VIH,VIL) check here for static stim function (initi fnc 10)
				  for(n=0;n<count;n++)
						{	if(m_inDynamic==1)
								{		
									for( i=0;i<m_patternCount;i++)
										{
										m_patternBuffer[i][pPins[n]]=1;
										m_patternBuffer[i][TOTAL_PINS]=m_tsetIndex;
										}
								}
								else
								{
								 IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(pPins[n]),TERM9_OP_IL));
 								 if (Status)ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							 //	 CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IL)", m_Handle,fnPin(pPins[n])));
								}
						}
					free(pPins);
					FreeDatum(pinDatZ);			
					runBurst(Fnc);
		
				}

			 //STMH
			 if(pinDatH = RetrieveDatum(M_STMH, K_FDD))
				{
					pinCnt = DatCnt(pinDatH);
					pinSiz = DatSiz(pinDatH);

					
					count=0;
					pPins = (int*)malloc((pinCnt+8) * sizeof(int));
					
					for (n=0; n<pinCnt; n++)
						{
							tmp=INTDatVal(pinDatH,n)-200;//-200 for block information in switch database
							if(tmp!=-200)				 //need to account for padded 0's in first byte.  eg....(1,2,3,4,5,6,7,8,9,10)
//																		looks like (0,0,0,0,0,0,1,2),(3,4,5,6,7,8,9,10)
								{
									pPins[count] = tmp;
									m_Td925pin[pPins[count]-1].current =  1; // Flag current pins in use by Do-Digital Test
									count++;
								}
						}	
  				//01/22/07 need to add card and channel levels (VIH,VIL) check here for static stim function (initi fnc 10)
					FreeDatum(pinDatH);				
					for(n=0;n<count;n++)
						{	
							if(m_inDynamic==1)
								{		
									for( i=0;i<m_patternCount;i++)
										{
										m_patternBuffer[i][pPins[n]]=4;
										m_patternBuffer[i][TOTAL_PINS]=m_tsetIndex;
										}
								}
								else
								{
							     IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(pPins[n]),TERM9_OP_IOX));
 							     if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
//							     CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IOX)", m_Handle,fnPin(pPins[n])));
								}
						}
					free(pPins);
					runBurst(Fnc);
				}
		 }// 11/13/06
		}
	break;
    default:
	break;
	}//end Fnc switch stmt
   return 0;
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchDwg(int Fnc)
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::FetchDwg(int Fnc)
{
    int     Status = 0;
	double  MeasValue = 0.0;
    double  MaxTime = 5000;
    char    XmlValue[1024];
    char    MeasFunc[32];
	int dcnt,dsiz,tmpDcnt,dtyp;     // Data count, word size in bytes, tempcounter, word type.
	DATUM *ddat;                    // Pointer to the value needed to be retrieved
	int fetchMod = 0;               // Currently fetched modifier in the fetch function
	char* pelm,tot;
	int j = 0;
	int maskCount=0,maskSize=0;
	int errorWords=0;
	int index=0;
	int k=0,l=0;
	int rtwrd=0,rtbits=0;   // 09/28/06 - additional bit, pin information.
	int byte=0;
	static BOOL checkerrors = TRUE; // initially have the compare flag on until a respone is retrieved.
	static BOOL returnvalues = FALSE; // initially have the a respone not done.//
    int wordidx = 0;
	int errfault = 0;
	
// Check MaxTime Modifier
	if(m_MaxTime.Exists)
        MaxTime = m_MaxTime.Real * 1000;

// Fetch action for the Dwg
    CEMDEBUG(5,cs_FmtMsg("FetchDwg (%s) called FNC %d",m_DeviceName, Fnc));
  
	if(Fnc==FNC_MEAS_STATIC_LOGIC_DATA_SCAN)
		{
		fetchHV(Fnc);
		return 0;
		}

    if(Status)
    {
      MeasValue = FLT_MAX;
     }
    else
    {
     IFNSIM(cs_GetSingleDblValue(XmlValue, MeasFunc, &MeasValue));
    }

	ddat = FthDat();	
	dcnt = DatCnt(ddat);
	dsiz = DatSiz(ddat);
	dtyp = DatTyp(ddat);  //DIGITAL VALUE FOR M_VALU
		
//Need to consider 3 cases: Fetch (Meas, Sense or Prove)Resp, (Prove)Error,(Prove)ErrI, and (Prove)Fault-Count
	fetchMod = FthMod();
//If it's RespOnly or m_StmRespSave, check the response of dcnt patterns (i.e # of elements in the RESP array) starting with RAM 0
	if(m_RespOnly == 1 || m_StmRespSave == 1 ) // 1/30/07 - check
		m_PatCount = dcnt;
	else
		m_PatCount = 1;

//Use m_PatCount (#of patterns in REF) as oppose to dcnt (#of Patterns in RESP) such that if dcnt>m_PatCount, zeros are added to the MSB side and not LSB
	if(dcnt<m_PatCount)
	{
		tmpDcnt = dcnt;
	}
	else 
	{
		tmpDcnt = m_PatCount;
	}
	tot=0;

	if((m_wasDynamic==true) && (fetchMod==M_VALU))
		{
			m_PatCount=m_patternCount=m_respBuff[m_fetchCounter].size.wordCount;
			if(m_respBuff[m_fetchCounter].size.wordCount!=dcnt) m_respBuff[m_fetchCounter].size.wordCount=dcnt;  //03/14/07a
			if(m_respBuff[m_fetchCounter].size.wordSize!=dsiz) m_respBuff[m_fetchCounter].size.wordSize=dsiz;
				m_pRespValue=(ViInt32*)malloc(m_respBuff[m_fetchCounter].size.wordCount*m_respBuff[m_fetchCounter].size.wordSize*8*sizeof(ViInt32));
				 // CEMDEBUG(10,cs_FmtMsg("m_pRespValue address %x and extent %d, count %d, size %d  Dynamic ",
				ProcessResponseData(fetchMod,m_respBuff[m_fetchCounter].size.wordCount,m_respBuff[m_fetchCounter].size.wordSize);
		}
	if(m_wasDynamic==false)  // 9/26/06 occurs when a dynamic pattern is run but as a static set. 
			{
				m_PatCount=m_patternCount; //9/26/06 This is what it should be but the struct.count wasn't set. m_PatCount = m_respBuff[m_respBuffCount].pins.count 
				if(m_pRespValue==NULL)
				{
				 if((m_referenceByteArrayflag)&&(dsiz<m_referenceWordSize))
				  m_pRespValue=(ViInt32*)malloc(dcnt*m_referenceWordSize*8*sizeof(ViInt32));  // m_pRespValue=(int*)malloc(dcnt*dsiz*8*sizeof(int)); 
				else
				  m_pRespValue=(ViInt32*)malloc(dcnt*dsiz*8*sizeof(ViInt32));  // m_pRespValue=(int*)malloc(dcnt*dsiz*8*sizeof(int)); 
		//		 m_pRespValue=(int*)calloc(dcnt*dsiz*8+8,sizeof(int));  //03/14/07a-
				}
				m_fetchCounter=0;
				// CEMDEBUG(10,cs_FmtMsg("m_pRespValue address %x and extent %d, count %d, size %d  Static ",m_pRespValue,dcnt*dsiz*8,dcnt,dsiz)); // 03/06/07- commented out to shorten log
				ProcessResponseData(fetchMod,dcnt,dsiz);
			}
	tot=0;
  //  CEMDEBUG(10,cs_FmtMsg("Completed ProcessResponseData, retrieving each bit value from the m9, fetchmode %d, function %d",fetchMod,Fnc)); // 03/06/07- commented out to shorten log
	switch(fetchMod)
	{
	case M_VALU:     // 12/5/06 return the response data
	{
	 returnvalues = TRUE;
	 retrieveDwg(fetchMod,ddat,Fnc);
	FthCnt(dcnt);  
	checkerrors = FALSE;  // reset the errors check flag in case this response data is part of a PROVE.
	}
	break;

	case M_SVCP:  // 12/5/06 - revised return the (response exor ref) data.
	{
	 errfault = 0;
	 if (!returnvalues) retrieveDwg(fetchMod,ddat,Fnc);
	 if (!checkerrors) // use to determine if the errors have already been checked. 
	 {
	  CEMDEBUG(10,cs_FmtMsg("Determine the Response to reference errors called from Save Compare"));
	  CEMDEBUG(10,cs_FmtMsg("Save Compare Fetchmod %d, words %x, word size %x",fetchMod,dcnt,dsiz));
	  errfault = ErrorDwg(fetchMod,dcnt,dsiz);
	 }
	 if (errfault < 0) CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned from M_SVCP with a problem."));
 	 checkerrors = TRUE; 
	  if (m_FaultCnt > (dcnt+1))
	  {
	   CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned with more faults than save compare to response words."));
	  }
	  int wordidx = 0;
	 for (j = 0;j < dcnt; j++) // send back all the compare values. 
	 { 
	  pelm = DIGDatVal(ddat,j);  //ErrorDwg formated them into the expected words. Assume there is pattern depth.
	  if(dtyp==DIGV)
	  {
	   for( k=0; k<dsiz;k++)		 // And that ErrorDwg has accounted for the bit padding to the left. 
	   {
	    DIGDatByte(pelm,k)=m_comparewordsArray[wordidx]; //m_comparewordsArray[k+(k*j)]
		CEMDEBUG(10,cs_FmtMsg("Data: Type Dec. SVCP. %x  REF. %x  wrd.%d byte of wrd. %d   ret wrd addr %d  ret addr %d",m_comparewordsArray[k+(k*j)],m_referenceByteArray[k+(k*j)],j,k,pelm,ddat));
	   ++wordidx;
	   }
	  }
	  else if(dtyp==INTV)
        {
		 CEMDEBUG(10,cs_FmtMsg("Data: Data Type Int SVCP "));
		 if(dcnt!=0) INTDatVal(ddat, j) = m_comparewordsArray[j];
		 else INTDatVal(ddat, j)=0;
		}
	  else CEMDEBUG(10,cs_FmtMsg("Compare Save Data: RTS Data Type is Unknown"));
	 }
     FthCnt(dcnt);
	}
	break;

	case M_ERRO:  // 12/5/06 return all the ( response exor ref) data that result was not 0. 
	{
	if (!returnvalues) retrieveDwg(fetchMod,ddat,Fnc);
	 errfault = 0;
	 if (!checkerrors) // used to determine if the errors have already been checked. 
	 {
	  errfault = ErrorDwg(fetchMod,dcnt,dsiz);
	  CEMDEBUG(10,cs_FmtMsg("Response to Reference errors called from retrieve Errors."));
	 if (errfault < 0) CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned from M_ERRO with a problem."));
	 checkerrors = TRUE;
	 }
	 CEMDEBUG(10,cs_FmtMsg("Fault Count = %x, words count = %x,  word size (bytes) = %x",m_FaultCnt,dcnt,dsiz));  // 03/06/07- 
	 if (m_FaultCnt > dcnt) 
	 {
	  CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned with more faults than response/compare words."));
	  m_FaultCnt = dcnt;
	 }
	 if (m_FaultCnt < 1)  // no errors
	 { int jk = 0;   //03/06/07- correct array pointer for debug display log
	  for (j=0;j<dcnt;j++)  // 03/06/07-
	  {
	   pelm = DIGDatVal(ddat,j);
	   for (k=0;k<dsiz;k++)  // 03/06/07- 
	   {
	    DIGDatByte(pelm,k) = m_errorwordsArray[jk];    //= 0;  // 03/06/07- its this way because there were no compare faults. 
  	    CEMDEBUG(10,cs_FmtMsg("Data: Type Dec. ERRO. %x  REF. %x  word %x, byte %d word addr = %x ",m_errorwordsArray[jk],m_referenceByteArray[jk],j,k,pelm));   // 03/06/07- fix the message for byte wide arrays  
		++jk;
	   }
	  }
	  FthCnt(dcnt); // 03/06/07-
	  break;
	 }
	 for (j = 0;j < m_FaultCnt; j++) // send back all the error compare values. 
	 { wordidx = (m_errorIdx[j]-1)*dsiz;  // if (wordidx <0) wordidx = 0; shouldn't need this decision because the erroridndex[] needs to start at 1.
	  pelm = DIGDatVal(ddat,j);  //ErrorDwg formated them into the expected words. Assume there is pattern depth.
	  if(dtyp==DIGV)
	  {
	   for( k=0; k<dsiz;k++)		 // And that ErrorDwg has accounted for the bit padding to the left. 
	   { 
	    DIGDatByte(pelm,k)=m_errorwordsArray[k+wordidx];  // (m_errorIdx[j]-1) = rspnswrd+1; //m_errorIndex;
		CEMDEBUG(10,cs_FmtMsg("Data: Type Dec. ERRO. %x  REF. %x  wrd.%d byte %d   ret wrd addr %d  ret addr %d",m_errorwordsArray[k+wordidx],m_referenceByteArray[k+(k*j)],j,k,pelm,ddat));
	   }
	  }
	  else if(dtyp==INTV) 
        {
		 INTDatVal(ddat, j) = m_errorwordsArray[j];
		 CEMDEBUG(10,cs_FmtMsg("Data: Data Type Int ERRO %x   %d",m_errorwordsArray[j],j));
		}
	  else CEMDEBUG(10,cs_FmtMsg("ERROR Data: RTS Data Type is Unknown"));
	 }
     FthCnt(m_FaultCnt);  //FthCnt(dcnt);//05/19/07- ?
	}
	break;

	case M_ERRI: // return all the word locations ( within the response word field) of (response exor ref != 0) data.
	{
	 errfault = 0;
	 if (!returnvalues) retrieveDwg(fetchMod,ddat,Fnc);
	 if (!checkerrors) // used to determine if the errors have already been checked. 
	 {
	  errfault = ErrorDwg(fetchMod,dcnt,dsiz);
	  CEMDEBUG(10,cs_FmtMsg("Response to Reference errors called from retrieve Error Indices "));
	  if (errfault < 0) CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned from M_ERRI with a problem."));
	  checkerrors = TRUE;
	 }
	 if (m_FaultCnt > dcnt) CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned with more faults than response/compare words."));
     if (dcnt == 0) CEMDEBUG(5,cs_FmtMsg("Retrieve Error Indices doesn't have a valid return data address. %d",dcnt));

	 if (m_FaultCnt < 1)  // no errors
	 {
	  for (j=0;j<dcnt;j++) // send back all the fail compare value indices
	  {
	   INTDatVal(ddat,j)= 0;
	   CEMDEBUG(10,cs_FmtMsg("Data: type Int ERRI. 0  from wrd. %d",j));
	  }
	  FthCnt(dcnt); //03/19/07-
	  break;
	 }
	 for (j=0;j<m_FaultCnt;j++) // send back all the fail compare value indices
	 { 
	//  pelm = DIGDatVal(ddat,j);  //ErrorDwg formated into integer values.
/*	  if(dtyp==DIGV) // error index should aways be an integer value. So ignore returning a decimal value
	  {
	   for( k=0; k<4;k++)		 // If it is a decimal word return the erroridx as 4 bytes. 
	   {
	    tempByte = (m_errorIdx[k]>>(4-k))& 0xFF;
	    DIGDatByte(pelm,k)=tempByte;
	   }
	  }
  */
	   if(dtyp==INTV)
       {
		  INTDatVal(ddat,j)= m_errorIdx[j];
		 CEMDEBUG(10,cs_FmtMsg("Data: type Int ERRI. %x  from wrd. %d",m_errorIdx[j],j));
	   }
	  else CEMDEBUG(10,cs_FmtMsg("ERROR Data: RTS Data Type is Unknown.  %d ",dtyp));
	 }
     FthCnt(m_FaultCnt);  
	}
	break;

	case M_FLTC:  // 12/05/06 return the extent of (response exor ref != 0) data.  
	{
	 errfault = 0;
	 if (!returnvalues) retrieveDwg(fetchMod,ddat,Fnc);
	 if (!checkerrors) // used to determine if the errors have already been checked. 
	 {
	  errfault = ErrorDwg(fetchMod,dcnt,dsiz);
	  CEMDEBUG(10,cs_FmtMsg("Response to Reference errors called from Fault Count. "));
	   checkerrors = TRUE; // 2/7/07 ?
	 }
	 if (errfault < 0) 
	 {
	  CEMDEBUG(5,cs_FmtMsg("ErrorDwg returned from M_FLTC with a problem."));
	  m_FaultCnt = 1;
	 }
//	  for (j=0;j<m_FaultCnt;j++)  // 12/7/06 this loop is to bump the display fault counter. But rts bogarts it. 
	
	  INTDatVal(ddat,0)=m_FaultCnt;  // 12/7/06 The data is correct but rts doesn't see it. 
	  CEMDEBUG(10,cs_FmtMsg("Faults counted  %x  %d  %d",m_FaultCnt,ddat,dcnt));
	  FthCnt(dcnt);
   // below is to finish clean up of the prove. cleanup occurs after the faultcount is sent to wrts. 
	  if ((m_comparewordsArrayflag)&&(checkerrors)) 
		  free (m_comparewordsArray); // 12/5/06 - Assumptions: 
	  if ((m_errorwordsArrayflag)&&(checkerrors))// &&(Fnc != FNC_DIGITAL_CONFIG	)) 
		  free (m_errorwordsArray);	// a FLTC is always called by Paws within a PROVE statement.
	  if ((m_errorIdxflag)&&(checkerrors))//&&(Fnc != FNC_DIGITAL_CONFIG	))
		  free (m_errorIdx);			// b. FLTC is always called last within a PROVE statement.
	  m_errorIndex = 0;			// As such cleanup and free up the end of errorDwg. 
	  checkerrors = FALSE;		// And turn off the check for errors flag. 
	  returnvalues = FALSE;
      if (m_referenceByteArrayflag)//&&(Fnc != FNC_DIGITAL_CONFIG	))
		  free (m_referenceByteArray);
	  m_referenceByteArray = NULL;
      if (m_maskByteArrayflag)//&&(Fnc != FNC_DIGITAL_CONFIG	))
		  free (m_maskByteArray);
	  m_maskByteArray = NULL;

  	m_referenceByteArrayflag = false;
	m_maskByteArrayflag = false;
	m_comparewordsArrayflag = false;
	m_errorwordsArrayflag = false;
	m_errorIdxflag = false;
   }
	break;
	default:
	break;
	}

	if(m_wasDynamic) m_fetchCounter++;
	if((fetchMod  == M_FLTC)&&(Fnc == FNC_PROV_STATIC_LOGIC_DATA)) 
		return(m_FaultCnt);

	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: OpenDwg(int Fnc)
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::OpenDwg(int Fnc)
{
  // Open action for the Dwg
  CEMDEBUG(5,cs_FmtMsg("OpenDwg (%s) called FNC %d",m_DeviceName, Fnc));

  return(0);
}
///////////////////////////////////////////////////////////////////////////////
// Function: CloseDwg(int Fnc)
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::CloseDwg(int Fnc)
{
	// Close action for the Dwg
    CEMDEBUG(5,cs_FmtMsg("CloseDwg (%s) called FNC %d",m_DeviceName, Fnc));

    if ((Fnc == 911)||(Fnc==931)) // check to see if thisis a CHANGE verb
    {
      Fnc -= 900;
      runOnce=0;
      runOnceHV=0;
    }

    if(Fnc==FNC_APLY_STATIC_LOGIC_DATA_DS && !(runOnce)) InitiateDwg(Fnc);	
	if(Fnc==FNC_APLY_STATIC_LOGIC_DATA_SCAN && !(runOnceHV)) InitiateDwg(Fnc);	

    //////// Place Dwg specific data here //////////////
    return (0);
}
///////////////////////////////////////////////////////////////////////////////
// Function: ResetDwg(int Fnc)
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::ResetDwg(int Fnc)
{
   ViStatus Status = 0;
    int resetFlag = 0;
    int m,q;
	int tsetDebug= 0;
	DATUM	*pdat=NULL;
    int goodPins=0;
    int spins[TOTAL_PINS+4];
 #define PINBYPINRESET

 // Events_Clear();
    // Reset action for the M9
    CEMDEBUG(5,cs_FmtMsg("ResetDwg (%s) called FNC %d",m_DeviceName, Fnc));
  if (Fnc == FNC_APLY_STATIC_LOGIC_DATA_SCAN)
	  resetHV(Fnc);

  if ((Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS)||(Fnc == FNC_DIGITAL_CONFIG)
		||(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS+300))  // 11/2/06 - does the end of a do timed digital and clear stims. 
  {
   // IFNSIM((Status = cs_IssueAtmlSignal(V_RST, m_DeviceName, m_SignalDescription, NULL, 0)));
#ifdef PINBYPINRESET
	int n,disablecount=0;	
	int x=1,m=0;
    long testResult;
    
	pdat = RetrieveDatum(M_PATH, K_DIS);  			
	if(!pdat)pdat = RetrieveDatum(M_PATH,K_CON);
    if((pdat)&&(Fnc==11))    // 08/10/09 - added the removal of static pins by pin using the list from a Remove Logic Data. 
    {
      int nCount = DatCnt(pdat);
	  nCount = nCount/3;
      int intpin=0;
      int pcnt;
      int temp;

      pcnt = DatCnt(pdat);
	  if (nCount !=0) pcnt = nCount;
	  CEMDEBUG(10,cs_FmtMsg(" Reset of static channels count = %x for device %x Fnc %d ", pcnt,m_Handle,Fnc));  //08/10/09- checkstep tag the Remove Logic Data 
 	  intpin = 2;
	  for (int n=0; n< pcnt; n++)
	  {
		temp=INTDatVal(pdat,n);
		if (nCount !=0) temp=INTDatVal(pdat,intpin);
		if(temp!=0)
			spins[goodPins] = (temp=temp-5)/10;
		else
			spins[goodPins] = temp - 200 ;
		if (m_Td925pin[spins[goodPins]].reset == Fnc) m_Td925pin[spins[goodPins]].reset = Fnc+300;
		CEMDEBUG(10,cs_FmtMsg("OpenDwg via handle %x Fnc = %x channel = %d of count = %x ",m_DeviceName, Fnc, spins[goodPins], goodPins)); // 08/05/09-checkstep log the channesl being disconnected.

		goodPins++;	
		intpin+=3;	
	  }
    Fnc+=300;
	}
	if (pdat != NULL) FreeDatum(pdat); //08/07/09- possible addin
 	CEMDEBUG(10,cs_FmtMsg("releasing RTS data address %x device %x Fnc %d ", pdat,m_Handle,Fnc));

    ViInt32 *disablechnls = NULL;
	disablechnls = (ViInt32 *)malloc((TOTAL_PINS) * sizeof(int));

    for (n=0;n<TOTAL_PINS;n++)
	{
  	 if(m_Td925pin[n].reset == Fnc)
	 {
	  IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle, TERM9_SCOPE_CHAN(n),TERM9_OP_IOX));
 	  if (Status)	ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
	  //CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IOX, %d FNC)", m_Handle,n,Fnc)); // 2/13/07-
  	  m_Td925pin[n].reset  =  0; // Reset the list of current pins in use
	  m = n;
	  if (n > 63) {x=2;m = n-64;}
	  if (n > 127) {x=3;m = n-128;}
	  disablechnls[disablecount]=TERM9_SCOPE_CHAN(SystemPin(m,x));
	  disablecount++;
	  if(Fnc == FNC_DIGITAL_CONFIG)
	  {
		m_Td925pin[n].current = false;
		 m_inDynamic = 0; //02/19/07- commented out
	    m_patternCount = 1; 
	  }
	 }
 	}
	if(disablecount !=0)
	{
	 --disablecount;
	 if (disablecount == 0) disablecount = 1;
	 resetFlag = 1;
	 IFNSIM(Status = terM9_setChannelConnect(m_Handle,disablecount,disablechnls,TERM9_RELAY_OPEN)); 
 	 if (Status) ErrorM9(m_Handle, disablecount, "terM9_setChannelConnect to Open %d");	 
	 IFNSIM(Status = terM9_setChannelLoad (m_Handle,disablecount,disablechnls,TERM9_LOAD_OFF)); 	//2/10/07- Default Load Off	
 	  if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelLoad");
	 CEMDEBUG(11,cs_FmtMsg("terM9_setChannelConnect(%d,total channels %d ,TERM9_RELAY_OPEN) and open Load", m_Handle,disablecount));
	}
	free(disablechnls); //02/20/07-?
		 
	if(resetFlag)  // 1/29/07 force channels from (just above) scan to IOX and open state via run of a burst.
    {
	 IFNSIM(Status = terM9_setDynamicPattern (m_Handle,1,tsetDebug,TERM9_COP_HALT ,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE)); //or TERM9_COP_NOP //or TERM9_COND_NONE 		//tsetDebug=m_patternBuffer[n][TOTAL_PINS];
	 CEMDEBUG(10,cs_FmtMsg("terM9_SetDynamicPattern(%d,1,%d,TERM9_COP_HALT,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE) reset ", m_Handle,tsetDebug)); //TERM9_COP_NOP
	 if (Status) ErrorM9(m_Handle, Status, "terM9_SetDynamicPattern");
	 IFNSIM(Status = terM9_loadDynamicPattern(m_Handle, VI_TRUE, VI_TRUE));
	 if (Status) ErrorM9(m_Handle, Status, "terM9_loadDynamicPattern");
	 CEMDEBUG(10,cs_FmtMsg("terM9_loadDynamicPattern(%d, VI_TRUE, VI_TRUE)", m_Handle));
     IFNSIM(Status = terM9_runDynamicPatternSet(m_Handle, VI_FALSE, &testResult));//Run "BURST" dynamic patterns using terM9_runDynamicPatternSet
     if (Status) ErrorM9(m_Handle, Status, "terM9_runDynamicPatternSet reset fault ");
     CEMDEBUG(10,cs_FmtMsg("terM9_runDynamicPatternSet(%d, VI_FALSE, %d)", m_Handle,testResult)); 
     // now check for card level clearing. If all channels are reset for a particular card then clean the level sets.
    for( q=1;q<=TOTAL_CARDS;q++)		
    {    
	   m = PINS_PER_CARD;
	   for (n=0;n<PINS_PER_CARD;n++)
	   {
		 if (m_Td925pin[n].reset==0)
		 {
		  --m;
		  if ((Fnc == FNC_DIGITAL_CONFIG)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS+300)) 
		  {
		   m_Td925setup[q].PinIdx[n].lvlidx.stim = 0;   // clears static and dynamic stims,
		  // m_Td925setup[q].Level[0].vi.h = 0.00;     // 03/05/07- leave last state of channel levels ntact but clear the flags.
		  // m_Td925setup[q].Level[0].vi.l = -0.00;
		  // m_Td925setup[q].Level[1].vi.h = 0.00; 
		  // m_Td925setup[q].Level[1].vi.l = -0.00;
		  }
		  if ((Fnc == FNC_DIGITAL_CONFIG)||(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS)) 
		  {
		   m_Td925setup[q].PinIdx[n].lvlidx.resp = 0; // clears dynamic resp. 
		  //  m_Td925setup[q].Level[0].vo.h = 0.02;   // 03/05/07- leave last state of channel levels intact but clear the flags.
		  //  m_Td925setup[q].Level[0].vo.l = -0.02;
		  //  m_Td925setup[q].Level[1].vo.h = 0.02; 
		  //  m_Td925setup[q].Level[1].vo.l = -0.02;
		  } 
		 }
	   }
	  if ((m==0)||(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS))  // 03/05/07- added flag for pcr 219 where apply pins may still be active
		{
		// meas_illegalState = 0;  //04/24/07- added for pcr234
 		 m_Td925setup[q].SetGndLvl = 0;
		 if ((Fnc == FNC_DIGITAL_CONFIG)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS+300)) 
		 {
		  m_Td925setup[q].Level[0].used.stim = FALSE;
		  m_Td925setup[q].Level[1].used.stim = FALSE;
		 }
		 if ((Fnc == FNC_DIGITAL_CONFIG)||(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS))
		 {
		  m_Td925setup[q].Level[0].used.resp = FALSE;
		  m_Td925setup[q].Level[1].used.resp = FALSE;
		//  CEMDEBUG(10,cs_FmtMsg("clearing response level flags per function %d",Fnc)); //3/5/07- for debug of pcr219
     }
		}
	 //03/06/07- Discard the status because it will be incorrect and no importance for the following set commands. 
	  IFNSIM(Status = terM9_setLevelSetVCOM(m_Handle, TERM9_SCOPE_CARD(q),0, 0.00));
	  if (Status) Status = 0;  // 03/06/07-
	  IFNSIM(Status = terM9_setLevelSetVCOM(m_Handle, TERM9_SCOPE_CARD(q),1, 0.00));
	  if (Status) Status = 0;  // 03/06/07-
	  CEMDEBUG(10,cs_FmtMsg("reset terM9_setLevelSet(%d, TERM9_SCOPE_CARD%d  VCOM levels 0&1 to 0.00 )",m_Handle,q));// 03/06/07- added reset of IOH and IOL to clearing m9
	  IFNSIM(Status = terM9_setLevelSetIOH (m_Handle, TERM9_SCOPE_CARD(q),0,-0.00));  // 04/20/07- removed max magnitude,-0.010mamps to load current for default
	  if (Status) Status = 0;  // 03/06/07-
	  IFNSIM(Status = terM9_setLevelSetIOH (m_Handle, TERM9_SCOPE_CARD(q),1,-0.00));  
	  if (Status) Status = 0;  // 03/06/07-
	  IFNSIM(Status = terM9_setLevelSetIOL (m_Handle, TERM9_SCOPE_CARD(q),0,0.00));   // 04/20/07- removed max magnitude, 0.010mamps to load current for default
	  if (Status) Status = 0;  // 03/06/07-
	  IFNSIM(Status = terM9_setLevelSetIOL (m_Handle, TERM9_SCOPE_CARD(q),1,0.00));
	  if (Status) Status = 0;  // 03/06/07-
	  CEMDEBUG(10,cs_FmtMsg("reset terM9_setLevelSet(%d, TERM9_SCOPE_CARD%d IOH, IOL levels 0&1 to 0.00 )",m_Handle,q));
	} 
    resetFlag = 0;
   }
#endif //PINBYPINRESET
   }
  if ((Fnc == 0)||(Fnc== 101))  // 10/25/06 wipe the internal slate if its a remove all. 
	{ 
    if (Fnc == 0)  // 1/30/07 - atml, general, reset should clear the hardware. 
	{
	 IFNSIM(Status = terM9_reset(m_Handle));
     if (Status) ErrorM9(m_Handle, Status, "terM9_reset");
	 CEMDEBUG(5,cs_FmtMsg("ResetDwg terM9_reset(%d)", m_Handle));
	 }
	resetFlag = 1;
	// for everything else flush all the lists of current pins in use
	 CEMDEBUG(5,cs_FmtMsg("Reset clear all pin states(%d)", m_Handle));
	 //04/18/07- Add reset the levelsets for each card here if it doesn't wack any weakly written TPS. set to 10.48V = VIH,-10.48V = VIL, 0.040V = VOH, -0.040 VOL

	for ( q=0;q<TOTAL_PINS;q++)
	    m_Td925pin[q].reset   =  0; 
	
	//meas_illegalState = 0;	//04/24/07-
	}
  if (resetFlag == 1)
  {
   CEMDEBUG(5,cs_FmtMsg("Reset clear all card levels & flags(%d)", m_Handle));
   for( q=1;q<=TOTAL_CARDS;q++)
    {
        for(m=0;m<2;m++)
        {
            m_Td925setup[q].Level[m].used.stim = FALSE;
            m_Td925setup[q].Level[m].used.resp = FALSE;
        }
		m_Td925setup[q].SetGndLvl = 0;
    }
    for (q=0; q<=TOTAL_CARDS; q++)
	{
		for(m=0;m<PINS_PER_CARD;m++)
		{
			m_Td925setup[q].PinIdx[m].lvlidx.stim = 0;
			m_Td925setup[q].PinIdx[m].lvlidx.resp = 0;
        }
	}
	resetFlag = 0;
  }
  if ((Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS)||(Fnc == FNC_DIGITAL_CONFIG)||(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS)
	  ||(Fnc == 0)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_SCAN)||(Fnc ==FNC_MEAS_STATIC_LOGIC_DATA_SCAN)||(Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS+300))
  {
  for( q=0;q<=32767;q++)
   {   
	  for( m=0;m<TOTAL_PINS;m++)
	m_patternBuffer[q][m]=0;
   } 
  InitPrivateDwg();    // Reset all member variables
  CEMDEBUG(10,cs_FmtMsg("cleared pattern buffers, levels and flags for handle(%d)",m_Handle));
  }
  if ((Fnc == FNC_TIMER)||(Fnc == FNC_EVNT_TBE_2)||(Fnc == FNC_DIGITAL_CONFIG) //03/13/07-
	  ||(Fnc == FNC_EVNT_EBE_CALL_EBE)||(Fnc ==FNC_EVNT_EBE_CALL_TBE_SBE_2)
	  ||(Fnc== FNC_EVNT_EBE_CALL_TBE_SBE_3)||(Fnc== FNC_EVNT_SBE_2)||(Fnc== FNC_EVNT_TBE_3)||(Fnc== FNC_EVNT_SBE_3)) //||(Fnc == FNC_DIGITAL_CONFIG) 02/20/07-
  {
   Events_Clear();
   CEMDEBUG(10,cs_FmtMsg("cleared events based on Fnc %d",Fnc));
  }
  	// CEMDEBUG(5,cs_FmtMsg("Reset finished (%d)", m_Handle));
  return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoDwg(int Fnc)
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             Device Database Function value
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CDwg_T::GetStmtInfoDwg(int Fnc)
{

	DATUM *datum;

	//descriptive//StimOne Array
	if(datum=RetrieveDatum(M_STMZ,K_SET))
	{
		m_StimZero.Exists=true;
		CEMDEBUG(10,cs_FmtMsg("Found Stim-Zero."));
		FreeDatum(datum);
	}

	//Voltage-One//Real

	if(datum=RetrieveDatum(M_VLT1,K_SET))  //(datum=GetDatum(M_VLT1,K_SET))   //02/19/07-
	{
		m_VoltOne.Exists=true;
		m_VoltOne.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Voltage-One %lf",m_VoltOne.Real));
		FreeDatum(datum);
	}

	//Voltage-Zero//Real

	if(datum=RetrieveDatum(M_VLT0,K_SET)) // (datum=GetDatum(M_VLT0,K_SET))   //02/19/07-
	{
		m_VoltZero.Exists=true;
		m_VoltZero.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Voltage-Zero %lf",m_VoltZero.Real));
	    FreeDatum(datum);
	}

	//Voltage-Quies//Real

	if(datum=RetrieveDatum(M_VLTQ,K_SET))   //(datum=GetDatum(M_VLTQ,K_SET))  //02/19/07-
	{
		m_VoltageQuies.Exists=true;
		m_VoltageQuies.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Voltage-Quies %lf",m_VoltageQuies.Real));
		FreeDatum(datum); 
	}

	//Current-One//Real

	if(datum=RetrieveDatum(M_CUR1,K_SET))   //(datum=GetDatum(M_CUR1,K_SET))   //02/19/07-
	{
		m_CurrentOne.Exists=true;
		m_CurrentOne.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Current-One %lf",m_CurrentOne.Real));
		FreeDatum(datum); //02/19/07-
		if (m_CurrentOne.Real > 0) 
		{ 
		  CEMDEBUG(10,cs_FmtMsg("Found Current-One %lf with the wrong magnitude ",m_CurrentOne.Real));
		  m_CurrentOne.Real = -1*m_CurrentOne.Real;
		}
	}
	if (datum=RetrieveDatum(M_CUR1,K_SRX))   //(datum=GetDatum(M_CUR1,K_SRX)) // 03/13/07-
	{
		m_CurrentOne.Exists=true;
		m_CurrentOne.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Current-One  SRX %lf",m_CurrentOne.Real));
		  FreeDatum(datum); //02/19/07- //03/13/07-
		if (m_CurrentOne.Real > 0) 
		{ 
		  CEMDEBUG(10,cs_FmtMsg("Found Current-One %lf with the wrong magnitude ",m_CurrentOne.Real));
		  m_CurrentOne.Real = -1*m_CurrentOne.Real;
		}
	}
	if(datum=RetrieveDatum(M_CUR1,K_SRN)) //(datum=GetDatum(M_CUR1,K_SRN))  //02/19/07-
	{
		m_CurrentOne.Exists=true;
		m_CurrentOne.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Current-One  SRN%lf",m_CurrentOne.Real));
		 FreeDatum(datum); //02/19/07-
		if (m_CurrentOne.Real > 0) 
		{ 
		  CEMDEBUG(10,cs_FmtMsg("Found Current-One %lf with the wrong magnitude ",m_CurrentOne.Real));
		  m_CurrentOne.Real = -1*m_CurrentOne.Real;
		}
	}

	//Current-Zero//Real

	if(datum=RetrieveDatum(M_CUR0,K_SET)) //(datum=GetDatum(M_CUR0,K_SET))   //02/19/07-
	{
		m_CurrentZero.Exists=true;
		m_CurrentZero.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Current-Zero %lf",m_CurrentZero.Real));
		 FreeDatum(datum); //02/19/07-
		if (m_CurrentZero.Real < 0) 
		{ 
		  CEMDEBUG(10,cs_FmtMsg("Found Current-Zero %lf with the wrong magnitude ",m_CurrentZero.Real));
		  m_CurrentZero.Real = -1*m_CurrentZero.Real;
		}
	}
	if(datum=RetrieveDatum(M_CUR0,K_SRX)) //(datum=GetDatum(M_CUR0,K_SRX))   //02/19/07-
	{
		m_CurrentZero.Exists=true;
		m_CurrentZero.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Current-Zero %lf",m_CurrentZero.Real));
		 FreeDatum(datum); //02/19/07-
		if (m_CurrentZero.Real < 0) 
		{ 
		  CEMDEBUG(10,cs_FmtMsg("Found Current-Zero %lf with the wrong magnitude ",m_CurrentZero.Real));
		  m_CurrentZero.Real = -1*m_CurrentZero.Real;
		}
	}
	if(datum=RetrieveDatum(M_CUR0,K_SRN))  //(datum=GetDatum(M_CUR0,K_SRN))  //02/19/07-
	{
		m_CurrentZero.Exists=true;
		m_CurrentZero.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Current-Zero %lf",m_CurrentZero.Real));
		  FreeDatum(datum); //02/19/07-
		if (m_CurrentZero.Real < 0) 
		{ 
		  CEMDEBUG(10,cs_FmtMsg("Found Current-Zero %lf with the wrong magnitude ",m_CurrentZero.Real));
		  m_CurrentZero.Real = -1*m_CurrentZero.Real;
		}
	}

	//Illegal-State-Indicator//Integer

	
	//Rise-Time//Real

	if(datum=RetrieveDatum(M_RISE,K_SET))
	{
		m_RiseTime.Exists=true;
		m_RiseTime.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Rise Time %lf",m_RiseTime.Real));
		FreeDatum(datum);
	}

	//Fall-Time//Real

	if(datum=RetrieveDatum(M_FALL,K_SET))
	{
		m_FallTime.Exists=true;
		m_FallTime.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Fall Time %lf",m_FallTime.Real));
		FreeDatum(datum);
	}

	//Max-Time//Real

	if(datum=RetrieveDatum(M_MAXT,K_SET))
	{
		m_MaxTime.Exists=true;
		m_MaxTime.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Max Time %lf",m_MaxTime.Real));
		FreeDatum(datum);
	}

	//Repeat//Integer

	if(datum=RetrieveDatum(M_REPT,K_SET))
	{
		m_MaxTime.Exists=true;
		m_MaxTime.Int=INTDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Repeat %d",m_Repeat.Int));
		FreeDatum(datum);
	}

	//HIZ//Non-Dimensional

	if(datum=RetrieveDatum(M_HIZZ,K_SET))
	{
		m_MaxTime.Exists=true;
		m_MaxTime.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Max Time %lf",m_Ref.Real));
		FreeDatum(datum);
	}

	//Ref//Real

	if(datum=RetrieveDatum(M_REFX,K_SET))
	{
		m_Ref.Exists=true;
		m_Ref.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Ref %lf",m_Ref.Real));
		FreeDatum(datum);
	}

	//Mask-One//Binary

	if(datum=RetrieveDatum(M_MASK,K_SET))
	{
		m_MaskOne.Exists=true;
		m_MaskOne.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Mask One %lf",m_MaskOne.Real));
		FreeDatum(datum);
	}
	//Mask-Zero//Binary
	if(datum=RetrieveDatum(M_MSKZ,K_SET))
	{
		m_MaskZero.Exists=true;
		m_MaskZero.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Mask Zero %lf",m_MaskZero.Real));
		FreeDatum(datum);
	}
	//Save-Comp//Non-Dimensional
	if(datum=RetrieveDatum(M_SVCP,K_SET))
	{
		m_SaveComp.Exists=true;
		m_SaveComp.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Save Comp %lf",m_SaveComp.Real));
		FreeDatum(datum);
	}
	//Error//Integer
	if(datum=RetrieveDatum(M_ERRO,K_SET))
	{
		m_Error.Exists=true;
		m_Error.Int=INTDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Error %d",m_Error.Int));
		FreeDatum(datum);
	}
	//Error-Index//Integer
	if(datum=RetrieveDatum(M_ERRI,K_SET))
	{
		m_ErrorIndex.Exists=true;
		m_ErrorIndex.Int=INTDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Error-Index %d",m_ErrorIndex.Int));
		FreeDatum(datum);
	}
	//Fault-Count//Integer
	if(datum=RetrieveDatum(M_FLTC,K_SET))
	{
		m_FaultCount.Exists=true;
		m_FaultCount.Int=INTDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Fault-Count %d",m_FaultCount.Int));
		FreeDatum(datum);
	}
	//Stim-Rate//Real
	if(datum=RetrieveDatum(M_STMR,K_SET))
	{
		m_StimClockFreq.Exists=true;
		m_StimClockFreq.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Stim-Rate %lf",m_StimClockFreq.Real));
		FreeDatum(datum);
	}
	//Sense-Delay//Real
	if(datum=RetrieveDatum(M_SDEL,K_SET))
	{
		m_SenseDelay.Exists=true;
		m_SenseDelay.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Sense-Delay %lf",m_SenseDelay.Real));
		FreeDatum(datum);
	}
	//Sense-Rate//Real
	if(datum=RetrieveDatum(M_SNSR,K_SET))
	{
		m_SenseRate.Exists=true;
		m_SenseRate.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Sense-Rate %lf",m_SenseRate.Real));
		FreeDatum(datum);
	}
	//Level-Logic-One Voltage//Decimal
	if(datum=RetrieveDatum(M_VOLT|LL1,K_SET))
	{
		m_LevelLogic1Voltage.Exists=true;
		m_LevelLogic1Voltage.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Level-Logic-One Voltage %lf",m_LevelLogic1Voltage.Real));
		FreeDatum(datum);
	}
	//Level-Logic-Zero Voltage//Decimal
	if(datum=RetrieveDatum(M_VOLT|LL0,K_SET))
	{
		m_LevelLogic0Voltage.Exists=true;
		m_LevelLogic0Voltage.Real=DECDatVal(datum,0);
		CEMDEBUG(10,cs_FmtMsg("Found Level-Logic-Zero Voltage %lf",m_LevelLogic0Voltage.Real));
		FreeDatum(datum);
	}
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateDwg()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CDwg_T::InitPrivateDwg(void)
{
 	m_DigitalConfig.Exists=false;
	m_VoltageOne.Exists=false;
	m_VoltageZero.Exists=false;
	m_VoltageQuies.Exists=false;
	m_CurrentOne.Exists=false;
	m_CurrentZero.Exists=false;
	m_IllegalStateIndicator.Exists=false;
	m_LevelLogic1Voltage.Exists=false;
	m_LevelLogic0Voltage.Exists=false;
	m_RiseTime.Exists=false;
	m_FallTime.Exists=false;
	m_ValueLogicData.Exists=false;
	m_StimEvent.Exists=false;
	m_SenseEvent.Exists=false;
	m_Event.Exists=false;
	m_AsVoltageSquareEq.Exists=false;
	m_PeriodRange.Exists=false;
	m_Repeat.Exists=false;
	m_Hiz.Exists=false;
	m_On.Exists=false;
	m_Value.Exists=false;
	m_Into.Exists=false;
	m_Ref.Exists=false;
	m_MaskOne.Exists=false;
	m_MaskZero.Exists=false;
	m_SaveComp.Exists=false;
	m_Error.Exists=false;
	m_ErrorIndex.Exists=false;
	m_FaultCount.Exists=false;
	m_ZeroOn.Exists=false;
	m_OneOn.Exists=false;
	m_HizOn.Exists=false;
	m_StimRate.Exists=false;
	m_SenseDelay.Exists=false;
	m_Iterate.Exists=false;

	m_FaultCnt = 0;
	m_iterations = 1;

	m_referenceByteArrayflag = false;
	m_maskByteArrayflag = false;
	m_comparewordsArrayflag = false;
	m_errorwordsArrayflag = false;
	m_errorIdxflag = false;

	goforcnxflag = false;

	runOnce = 0;
	runOnceHV = 0;
	// m_patternCount = 1;


  // meas_illegalState = 0;  //04/24/07-
	//END DWG
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataDwg()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CDwg_T::NullCalDataDwg(void)
{
  

    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

    return;
}
// This function retrieves the Integer data from the DATUM block.
int RetrieveData(short arg_token, int arg_mod, void *arg_val, int debugmode)
{
// Needed header files included: "key.h" and "cem.h" and "stdio.h"
// How to use the function for single values example: RetrieveData(M_VOLT,K_SET,&volt,1);
// How to use the function for an array example: RetrieveData(M_VOLT,K_SET,volt_array,0);
// Return of that function is 1 for succesfull fetch of Atlas value and 0 for unsuccesfull retrieval
// arg_token: argument token such as the one in the lex files (ex: M_VOLT)
// arg_mod: argument modifier such as min, nominal value or max (resp. K_SRN, K_SET, K_SRX)
// arg_val: argument value that has been retrieved from the Atlas.
// debugmode: If the debugmode is passed on (1 for yes and 0 for no -default-) then display
//			values that have been retrieved from the Atlas

DATUM *p_val;   // pointer in the DATUM block pointing to the value that we want to retrieve
int cnt_val;    // count number of elements in the data being retrieved
int i;          // index i
int t_val;      // This is the type number of the data being retrieved
char txtbuf[128];// This is the buffer that includes the text to be displayed in debugmode
int inttmp;     // buffer int value
double dectmp;  // buffer dec value


// Retrieve the pointer to the data of minimum value
if (arg_mod == K_SRN)
    p_val = RetrieveDatum(arg_token,K_SRN);
// Retrieve the pointer to the data of nominal value
if (arg_mod == K_SET)
    p_val = RetrieveDatum(arg_token,K_SET);
// Retrieve the pointer to the data of maximum value
if (arg_mod == K_SRX)
    p_val = RetrieveDatum(arg_token,K_SRX);

// If we have a pointer, that means that the value exists in the Atlas
// If not, we return the default value passed on
if (p_val != NULL)
  {
    // Got a pointer so we know that the value is there to be retrieved
    // Get the number of elements in the data
    cnt_val = DatCnt(p_val);
    // check on the type of the data
    t_val = DatTyp(p_val);
     switch(t_val)
      {
        case INTV:
            if (cnt_val == 1)
              {
				inttmp = INTDatVal(p_val,0);
                *((long *)arg_val) = inttmp;
				if (debugmode == 1)
				{
					sprintf(txtbuf," -debug- Value associated to %d retrieved:%d \n",arg_token,inttmp);
					Display(txtbuf);
				}
              }
            else
              {
                // retrieve the actual elements of the data
		          for (i = 0; i < cnt_val; i++)
				{
					inttmp = INTDatVal(p_val,i);
                    *((long *)arg_val) = inttmp;
					if (debugmode == 1)
					{
						sprintf(txtbuf,"%d ",inttmp);
						Display(txtbuf);
					}
				}
				if (debugmode == 1)
					Display("\n");
              }
            break;

        case DECV:
            if (cnt_val == 1)
              {
				dectmp = DECDatVal(p_val,0);
                *((double *)arg_val) = dectmp;
				if (debugmode == 1)
				{
					sprintf(txtbuf," -debug- Value associated to %d retrieved:%f \n",arg_token,dectmp);
					Display(txtbuf);
				}
              }
            else
              {
                // retrieve the actual elements of the data
				if (debugmode == 1)
				{
					sprintf(txtbuf," -debug- Value associated to %d retrieved:",arg_token);
					Display(txtbuf);
				}
                for (i = 0; i < cnt_val; i++)
				{
					dectmp = DECDatVal(p_val,i);
                    *((double *)arg_val) = dectmp;
					if (debugmode == 1)
					{
						sprintf(txtbuf,"%f ",dectmp);
						Display(txtbuf);
					}
				}
				if (debugmode == 1)
					Display("\n");
              }
           break;

//!! Handle this case later as needed
//        case DSCV:
//            if (cnt_val == 1)
//              {
//                *((short *)arg_val) = DSCDatVal(p_val,0);
//              }
//            else
//              {
//                // retrieve the actual elements of the data
//                for (i = 0; i < cnt_val; i++)
//                    *((short *)arg_val) = DSCDatVal(p_val,i);
//              }
//           break;

//!! Handle this case later as needed
//        case TXTV:
//            if (cnt_val == 1)
//              {
//                *((char *)arg_val) = GetTXTDatVal(p_val,0);
//              }
//            else
//              {
//                // retrieve the actual elements of the data
//                for (i = 0; i < cnt_val; i++)
//                    *((char *)arg_val) = GetTXTDatVal(p_val,i);
//              {
//           break;
       }
     FreeDatum(p_val);
     return 1;
  }
else
  {
	if (debugmode == 1)
	{
		sprintf(txtbuf," -debug- No value has been retrieved for %d \n",arg_token);
		Display(txtbuf);
	}
    // No need to do anything since the variable has been set to the default before calling this function
    FreeDatum(p_val);
    return 0;
  }
}

///////////////////////////////////////////////////////////////////////////////
// Function: DoDigital(int Fnc)
//
// Purpose: Perform the Do Digital action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////

int CDwg_T::DoDigitalSetup(int Fnc)
{
    ViStatus Status = 0;
	double  clockPeriod = 1.e-6;             // Stim and Resp Clock Period initialized to 1/1MHz
	ViInt32 clockRef=TERM9_SOURCE_INTERNAL;  // Default Clock Reference to Internal
	ViInt32 channelConnect = TERM9_RELAY_OPEN;
	ViInt32 slewRate=TERM9_SLEWRATE_MEDIUM;  // slew-rate for terM9_setLevelSetSlewRate will be defaulted to Medium
	BOOL stopConnect = FALSE; 
	double respDelay  = 100.e-9;             // Response Delay used in Resp Period  9/25/06
 	ViReal64  maxTime = 10;				 	 // Maximum Timeout value.
	ViReal64  window_array[2];				 // Response Period with start and end elements
 	ViReal64  phase_array[2];				 // Stim Period with start and end elements
	static ViInt32 *pinlist0 = NULL;          //11-29-06 made these static on a swag hangs runtime		 // Variable to store converted pin lists as a parameter of PNP for index 0
	static ViInt32 *pinlist1 = NULL;          // Variable to store converted pin lists as a parameter of PNP for index 1
    //char s_Msg[256];						used only for hybrid - non-existent in M917
	int pcnt = 0;        						 // Pin Count
	int n,m,x,y;

//	int ecnt;BUILD_WARN
	DATUM *pdat=NULL;  //DATUM *pdat;  //03/15/07a-
//	DATUM *ret;BUILD_WARN
	HEV EventRet1,EventRet2,EventRet3;// Return from the Events_Read api
//	HTS TSetVal;                      // TSet values returned by the TSTable TYX structure
	//ViReal64 phase_array[2];          // Array for phases associated to TSet
	//ViReal64 window_array[4];         // Array for windows associated to TSet
//	HDWG dwgpTsetTbl;    BUILD_WARN
	HDWG pdwgTest=NULL;       // Get handle for the dynamic test
//	ViInt32 stimRate;BUILD_WARN
	ViReal64 window[512],phase[512],period[512];
	int windowCount=0,phaseCount=0;

	int tsetCount=0;
			
	 // Setup action for the M9
    CEMDEBUG(5,cs_FmtMsg("SetupDO (MPA %d) called FNC %d", 
                          m_PrimaryAdr, Fnc));

    pinlist0  = (ViInt32 *)malloc((TOTAL_PINS+16) *  sizeof(int));
    pinlist1  = (ViInt32 *)malloc((TOTAL_PINS+16) *  sizeof(int));

///event handler
	/*
						stimEvent?
						    |
							|
				no       	|          yes										  
			----------------------------------------                             
			|										|							  
			|										|
			|										|
			|or										| or
 stimRate----------								-------------
	| and		  |								|			|
	|			  |								|			|
 senseDelay		senseRate					senseDelay     senseEvent
	*/		
		if (pdat=RetrieveDatum(M_EVXM,K_SET))//STIM EVENTS
			{
		// We have one or more Stim-Events. Retrieve them one after another
				int ecnt = DatCnt(pdat);
				phaseCount=ecnt;
				for (n=0; n<ecnt*2; n+=2)
				{
					// Retrieve then handle value; Expecting EBE at this point
					EventRet1 = Events_Read(INTDatVal(pdat,n/2));
					// Need to retrieve the next handle
				    if (EventRet1 == 0x00)  
						CEMDEBUG(10,cs_FmtMsg("Timing Configuration is incorrect no event has been idenitified."));// 10/26/06 
					EventRet2 = Events_Read(EventRet1->hRef);
					if ( (EventRet2->hRef) == -2 ||(EventRet2->hRef) == -3 )  // Pb with allocation
					{
						CEMDEBUG(5,cs_FmtMsg("Stim-Event(%d) Period=%f(us), Start=%f(us) , No Stop (-1)\n",
														n,(EventRet2->dValue)*1000000,(EventRet1->dValue)*1000000));

						//IFNSIM(Status = terM9_setTimingSetPhaseAssert (m_Handle,n, TERM9_SCOPE_SYSTEM , 1, EventRet1->dValue));

								phase[n]=EventRet2->dValue;
								phase[n+1]=-1;//no return
            	
					}
					else  // hRef > 0 and refers to a 3 layer events
					{
						// We have 3 layers
						EventRet3 = Events_Read(EventRet2->hRef);
						//Set up inst with
						//EventRet3->dValue,                    // Either -2 for SBE or period for TBE
						//EventRet2->dValue,                    // Start TQ
						//EventRet1->dValue+EventRet2->dValue,  // Stop TQ
	                    
						CEMDEBUG(5,cs_FmtMsg("(debug) Stim-Event(%d) Period=%f(us), Start=%f(us) , Stop=%f(us)\n",n,
															(EventRet3->dValue)*1000000,
															(EventRet2->dValue)*1000000,
															(EventRet1->dValue+EventRet2->dValue)*1000000));

						//IFNSIM(Status = terM9_setTimingSetPhaseAssert (m_Handle,n, TERM9_SCOPE_SYSTEM , 1, EventRet2->dValue));
						//IFNSIM(Status = terM9_setTimingSetPhaseReturn (m_Handle,n, TERM9_SCOPE_SYSTEM , 1, EventRet1->dValue+EventRet2->dValue));
						phase[n]=EventRet2->dValue;
						phase[n+1]=EventRet1->dValue+EventRet2->dValue;
						period[n/2]=EventRet3->dValue;
            		}
				}
				if ( pdat=RetrieveDatum(M_EVXE,K_SET) )//RESPONSE EVENTS
						{
							ecnt = DatCnt(pdat);//original//want to make sure that we have same number of timing phases
							windowCount=ecnt;
							
							for (n=0; n<ecnt*2; n+=2)
								{
									// Retrieve then handle value; Expecting EBE at this point
									EventRet1 = Events_Read(INTDatVal(pdat,n/2));
									// Need to retrieve the next handle
									EventRet2 = Events_Read(EventRet1->hRef);
									if ( (EventRet2->hRef) == -2 || (EventRet2->hRef) == -3 )  // We can only have two layers for Sensing
										{
											window[n]=EventRet1->dValue;
											window[n+1]=EventRet1->dValue+10e-9;
										}
								}  // end for
                
						}
				else
					{
						respDelay = clockPeriod * 0.1; // 9/25
						if(m_RespDelay.Exists)
							respDelay = m_RespDelay.Real;
						for( n=0;n<phaseCount*2;n+=2)
							{
								window[n]=period[n/2];
								window[n+1]=period[n/2]+10e-9;
							}
					}


				for( tsetCount=0;tsetCount<phaseCount;tsetCount++)
				{
					if(period[tsetCount]==-2)
					{
						CEMDEBUG(10,cs_FmtMsg("External Trigger Timing Configuration"));
						clockRef=TERM9_SOURCE_EXTERNAL;
						Status = terM9_setSystemClock (m_Handle,
										1,                       /* divider parameter       */
										TERM9_EDGE_RISING,       /* triggerEdge parameter   */
										TERM9_CLOCKMODE_ON,      /* clockEnable parameter   */
										TERM9_SOURCE_EXTERNAL,   /* reference parameter     */
										TERM9_PATH_DISABLE,      /* outPathEnable parameter */
										TERM9_LOGIC_TTL);        /* logicSelect parameter   */

					}
					else
						{
							CEMDEBUG(10,cs_FmtMsg("External Trigger Timing Configuration"));
							IFNSIM(Status = terM9_setTimingSet (m_Handle,tsetCount,
														period[tsetCount],
														TERM9_SCOPE_SYSTEM,
														phaseCount*2,phase,
														windowCount*2,window));
 							if (Status)
								ErrorM9(m_Handle, Status, "terM9_setTimingSet");
							CEMDEBUG(10,cs_FmtMsg("terM9_setTimingSet(%d, 0, %8.7f, TERM9_SCOPE_SYSTEM, 2, phase_array, 2, window_array)", m_Handle,clockPeriod));
							clockRef=TERM9_SOURCE_INTERNAL;
						}
				}
			}//end events
		else
		{
			if (m_StimClockFreq.Exists)//stim rate - YES
			{
			  clockPeriod = 1/m_StimClockFreq.Real;
			  respDelay = clockPeriod * 0.1;
			  if(m_RespDelay.Exists)respDelay = m_RespDelay.Real;
			}
			else if	( m_RespClockFreq.Exists)//sense rate - YES
				clockPeriod = 1/m_RespClockFreq.Real;
			else 
			{
			  if(m_inDynamic==1)
			  {
			   CEMDEBUG(10,cs_FmtMsg("Dynamic Configuration is set."));
			   return 0;
			  }
			}
															
		m_Td925timing.phase.assertEdge = clockPeriod/2; //Stim period will start at 50% of pattern period
		m_Td925timing.phase.returnEdge = clockPeriod;   //Stim period will end at the end of pattern period
		m_Td925timing.window.openEdge  = clockPeriod/2 + respDelay; //Resp period will start at assertEdge + respDelay
		m_Td925timing.window.closeEdge = clockPeriod;   //Resp period will end at the end of pattern period

		phase_array[0]  = m_Td925timing.phase.assertEdge;
		phase_array[1]  = m_Td925timing.phase.returnEdge;
		window_array[0] = m_Td925timing.window.openEdge;
		window_array[1] = m_Td925timing.window.closeEdge;

		Status = 0;    // Set m9 Clock Range ( normal or slow)
		//	else if (clockPeriod < 6.5e-5 && clockPeriod > 0)
  		IFNSIM(Status = terM9_setSystemTimingMode (m_Handle, TERM9_TIMING_STANDARD)); // default, if (clockPeriod < 6.5e-5 && clockPeriod > 0)
		if (clockPeriod >=  6.5e-5 && clockPeriod > 0)  // frequency of 15.38 kHz or less
		{ IFNSIM(Status = terM9_setSystemTimingMode (m_Handle, TERM9_TIMING_SLOW));
		//  CEMDEBUG(10,cs_FmtMsg("terM9_setSystemTimingMode(%d,%8.7f, TERM9_TIMING_SLOW)", m_Handle,clockPeriod)); // 11/18/06 debug message. 
		}
		if (Status)	ErrorM9(m_Handle, Status, "terM9_setSystemTimingMode");
		IFNSIM(Status = terM9_setTimingSet (m_Handle,0,clockPeriod,TERM9_SCOPE_SYSTEM,2,phase_array,2,window_array));
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setTimingSet");
		CEMDEBUG(10,cs_FmtMsg("terM9_setTimingSet(%d, 0, %8.7f, TERM9_SCOPE_SYSTEM, 2, phase_array, 2, window_array)", m_Handle,clockPeriod));
		} 

	IFNSIM(Status = terM9_setSystemClockReference (m_Handle, clockRef));
 	if (Status) ErrorM9(m_Handle, Status, "terM9_setSystemClockReference");
	CEMDEBUG(10,cs_FmtMsg("terM9_setSystemClockReference(%d, %d)", m_Handle,clockRef)); 

    // Set Clock Frequency Range
/* 11/17/06    if (clockPeriod >=  6.5e-5 && clockPeriod > 0)  // period of 15.38 kHz
    {
  		IFNSIM(Status = terM9_setSystemTimingMode (m_Handle, TERM9_TIMING_SLOW));
 		if (Status)
			ErrorM9(m_Handle, Status, "terM9_setSystemTimingMode");
		CEMDEBUG(10,cs_FmtMsg("terM9_setSystemTimingMode(%d, TERM9_TIMING_SLOW)", m_Handle));
	}
    if (clockPeriod < 6.5e-5 && clockPeriod > 0)  // period of 15.38 kHz and period of 250 kHz (in between)
    {
  		IFNSIM(Status = terM9_setSystemTimingMode (m_Handle, TERM9_TIMING_STANDARD));
 		if (Status)
				ErrorM9(m_Handle, Status, "terM9_setSystemTimingMode");
		CEMDEBUG(10,cs_FmtMsg("terM9_setSystemTimingMode(%d, TERM9_TIMING_STANDARD)", m_Handle));
    }
*/

    // call terM9_setTimingSet to define timing in hardware
 
    for (n=1, m=0, y=0; n<=TOTAL_CARDS; n++)
	{
		for(x=0;x<PINS_PER_CARD;x++)
		{
			if (((m_Td925setup[n].PinIdx[x].lvlidx.stim) == 1)||
				((m_Td925setup[n].PinIdx[x].lvlidx.resp) == 1))
			{
				// We have a candidate for level index 0
				pinlist0[m] = TERM9_SCOPE_CHAN(SystemPin(x,n));
				m++;
			}
			if ((m_Td925setup[n].PinIdx[x].lvlidx.stim == 2)||
				(m_Td925setup[n].PinIdx[x].lvlidx.resp == 2))
			{
				// We have a candidate for level index 1
	            pinlist1[y] = TERM9_SCOPE_CHAN(SystemPin(x,n));
				y++;
			}
        }
	}
    if (m<TOTAL_PINS) pinlist0[m] = -1;  // Terminate the index-0 pin list with -1 (not a valid pin)
    if (y<TOTAL_PINS) pinlist1[y] = -1;  // Terminate the index-1 pin list with -1 (not a valid pin)
    
    for (n=0; (n<TOTAL_PINS && pinlist0[n]!=-1); n++)
	{ 	;// CEMDEBUG(11,cs_FmtMsg("Channel: pinlist0[%d]=%d",n,pinlist0[n])); //01/02/07, actual channel = (pinlist0[n]-33968) 02/19/07-
	}
	// Now program the voltage level index for the proper pins from list 0 First for voltage level index 0
    pcnt = n;  // pin count is equal to n after exiting the for loop
    if (pcnt != 0 && stopConnect == FALSE)
    {
  		IFNSIM(Status = terM9_setChannelLevel (m_Handle,pcnt,pinlist0,0)); 		//Level index 0
 		if (Status)	ErrorM9(m_Handle, Status, "terM9_setChannelLevel");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelLevel(%d, %d, pinlist0, 0)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelChanMode (m_Handle,pcnt,pinlist0,TERM9_CHANMODE_DYNAMIC)); 		
 		if (Status)ErrorM9(m_Handle, Status, "terM9_setChannelChanMode");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelChanMode(%d, %d, pinlist0, TERM9_CHANMODE_DYNAMIC)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelConnect (m_Handle,pcnt,pinlist0,TERM9_RELAY_CLOSED)); 		
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelConnect");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelConnect(%d, %d, pinlist0, TERM9_RELAY_CLOSED)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelImpedance (m_Handle,pcnt,pinlist0,TERM9_IMPED_50OHM)); 		
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelImpedance");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelImpedance(%d, %d, pinlist0, TERM9_IMPED_50OHM)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelCapture (m_Handle,pcnt,pinlist0,TERM9_CAPTURE_WINDOW)); 		
 		if (Status)ErrorM9(m_Handle, Status, "terM9_setChannelCapture");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelCapture(%d, %d, pinlist0, TERM9_CAPTURE_WINDOW)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelPhase (m_Handle,pcnt,pinlist0,0)); 		//Default level phase 0	
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPhase");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPhase(%d, %d, pinlist0, 0)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelWindow (m_Handle,pcnt,pinlist0,0)); 					//Default level window 0
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelWindow");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelWindow(%d, %d, pinlist0, 0)", m_Handle,pcnt));

		IFNSIM(Status = terM9_setChannelFormat (m_Handle,pcnt,pinlist0,TERM9_FORMAT_NRET)); 	//Default format NRZ	
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelFormat");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelFormat(%d, %d, pinlist0, TERM9_FORMAT_NRET)", m_Handle,pcnt));
/*      eads-taken out and added to setupRespRoutine
  		IFNSIM(Status = terM9_setChannelLoad (m_Handle,pcnt,pinlist0,TERM9_LOAD_OFF)); 	//Default Load Off	
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelLoad");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelLoad(%d, %d, pinlist0, TERM9_LOAD_OFF)", m_Handle,pcnt));
*/
    }
	for (n=0; (n<TOTAL_PINS && pinlist1[n]!=-1); n++)
	{	;//CEMDEBUG(11,cs_FmtMsg("Channel: pinlist1[%d]=%d",n,pinlist1[n]));	//02/19/07- 
	} 
    // Now program the voltage level index for the proper pins First for voltage level index 1
    pcnt = n;  // pin count is equal to n after exiting the for loop
    if (pcnt != 0 && stopConnect == FALSE)
    {
  		IFNSIM(Status = terM9_setChannelLevel (m_Handle, pcnt, pinlist1, 1)); 		//Level index 1
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelLevel");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelLevel(%d, %d, pinlist1, 1)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelChanMode (m_Handle, pcnt, pinlist1,TERM9_CHANMODE_DYNAMIC)); 		
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelChanMode");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelChanMode(%d, %d, pinlist1, TERM9_CHANMODE_DYNAMIC)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelConnect (m_Handle,pcnt,pinlist1,TERM9_RELAY_CLOSED)); 		
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelConnect");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelConnect(%d, %d, pinlist1, TERM9_RELAY_CLOSED)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelImpedance (m_Handle,pcnt,pinlist1,TERM9_IMPED_50OHM)); 		
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelImpedance");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelImpedance(%d, %d, pinlist1, TERM9_IMPED_50OHM)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelCapture (m_Handle,pcnt,pinlist1,TERM9_CAPTURE_WINDOW)); 		
 		if (Status)ErrorM9(m_Handle, Status, "terM9_setChannelCapture");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelCapture(%d, %d, pinlist1, TERM9_CAPTURE_WINDOW)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelPhase (m_Handle,pcnt,pinlist1,0)); 		//Default level phase 0	
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPhase");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelPhase(%d, %d, pinlist1, 0)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelWindow (m_Handle,pcnt,pinlist1,0)); 		//Default level window 0
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelWindow");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelWindow(%d, %d, pinlist1, 0)", m_Handle,pcnt));

  		IFNSIM(Status = terM9_setChannelFormat (m_Handle,pcnt,pinlist1,TERM9_FORMAT_NRET)); 	//Default format NRZ	
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelFormat");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelFormat(%d, %d, pinlist1, TERM9_FORMAT_NRET)", m_Handle,pcnt));

/* 11/30/06 moved set channel load to RespSetup function.  
  		IFNSIM(Status = terM9_setChannelLoad (m_Handle,pcnt,pinlist1,TERM9_LOAD_OFF)); 	//Default Load Off	
 		if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelLoad");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelLoad(%d, %d, pinlist1, TERM9_LOAD_OFF)", m_Handle,pcnt));
*/		
  }
  IFNSIM(Status = terM9_setLowPower (m_Handle,TERM9_SCOPE_SYSTEM,VI_FALSE));
  if (Status)ErrorM9(m_Handle, Status, "terM9_setLowPower");
  CEMDEBUG(10,cs_FmtMsg("terM9_setLowPower(%d, TERM9_SCOPE_SYSTEM, VI_FALSE)", m_Handle));
			
  //Start the dynamic test environment
  IFNSIM(Status = terM9_prepareDynamicPatternSetLoading (m_Handle)); 
  if (Status)ErrorM9(m_Handle, Status, "terM9_prepareDynamicPatternSetLoading");
  CEMDEBUG(10,cs_FmtMsg("terM9_prepareDynamicPatternSetLoading(%d)", m_Handle));

  // Start at addr. 0
  IFNSIM(Status = terM9_setDynamicPatternIndex (m_Handle,0));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternIndex");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternIndex(%d, 0)", m_Handle));

  IFNSIM(Status = terM9_setDynamicPatternClocksPerPattern (m_Handle,1));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternClocksPerPattern");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternClocksPerPattern(%d, 1)", m_Handle));

  IFNSIM(Status = terM9_setDynamicPatternResultCapture (m_Handle,VI_TRUE));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternResultCapture");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternResultCapture(%d, VI_TRUE)", m_Handle));

  IFNSIM(Status = terM9_setDynamicPatternFailCapture (m_Handle,VI_TRUE));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternFailCapture");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternFailCapture(%d, VI_TRUE)", m_Handle));

  IFNSIM(Status = terM9_setDynamicPatternProbeTimingSetIndex (m_Handle,0));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternProbeTimingSetIndex");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternProbeTimingSetIndex(%d, 0)", m_Handle));

  IFNSIM(Status = terM9_setDynamicPatternProbeCapture (m_Handle,VI_FALSE));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternProbeCapture");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternProbeCapture(%d, VI_FALSE)", m_Handle));

  //Retreive the Max-Time Value from Atlas for Timeout
  if(m_MaxTime.Exists)
		maxTime = m_MaxTime.Real;

  // Special case to not max time on Circulate-Continuously.
  if(m_CirculateCont.Exists)
        maxTime = -1.0;

  IFNSIM(Status = terM9_setDynamicPatternSetTimeout(m_Handle,maxTime));
  if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPatternSetTimeout");
  CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPatternSetTimeout(%d, %f)", m_Handle,maxTime));

  if (pinlist0  != NULL)
  {
   free(pinlist0);
   // CEMDEBUG(7,cs_FmtMsg("Freeing the allocated memory (pinlist0)"));
   pinlist0 = NULL;  //02/19/07-
  }
  if (pinlist1  != NULL)
  {
   free(pinlist1); 
  //CEMDEBUG(7,cs_FmtMsg("Freeing the allocated memory (pinlist1)"));
   pinlist1 = NULL; //02/19/07-
  }
  if(pdat !=NULL) FreeDatum(pdat);  //03/15/07a-
  return 0;
} //End Do-Digital

///////////////////////////////////////////////////////////////////////////////
// Function: SetupResp(int Fnc)
//
// Purpose: Perform the Setup Resp action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////

int CDwg_T::SetupResp(int Fnc)
{
    
	int rpins[TOTAL_PINS+4];   					 // list of pins being addressed
	int pcnt=0, pinidx;        	             // Pin Count
    int ThisPin, ThisCard;
	ViInt32 numOresp=0;
	ViInt32 test_result=0;
	ViInt32 numPins=0;
	ViInt32 plist[TOTAL_PINS];
	ViStatus Status; //BUILD_WARN
	
    double VoltOne = DEFAULT_VOLT_ONE_RESP;
    double VoltZero = DEFAULT_VOLT_ZERO_RESP;

    // Setup action for the M9
    CEMDEBUG(5,cs_FmtMsg("SetupResp (MPA %d) called FNC %d",m_PrimaryAdr, Fnc));
//	if (Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS ) meas_illegalState = 0; // 04/24/07- added for pcr 234.Force meas to use illegal state indicators
	if(ParseSetup(Fnc, rpins, &pcnt))
    {
	 CEMDEBUG(5,cs_FmtMsg("ParseSetupResp returning with bad datum pointer FNC %d", Fnc));  //03/15/07a-
     return(0);
	}
    if(m_VoltOne.Exists)
        VoltOne = m_VoltOne.Real;
	
    // Verify that it is 40mV from VCOM (0.0)
    if((VoltOne < 0.04) && (VoltOne > -0.04))
        VoltOne = 0.04;

    // Set to single threshold (V1=V0) if no V0
    if(m_VoltZero.Exists)
        VoltZero = m_VoltZero.Real;
    else if(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS)  //
		VoltZero = MIL_TTL_VOLT_ZERO_RESP;  // 9/26/06 if not defined in a meas set the default to ttl low. 
	else 
        VoltZero = VoltOne; 

    // Walk Pinlist and Setup Cards as required
    for(pinidx = 0; pinidx < pcnt; pinidx++)
   {
        // Get the DTI Card Number and relative pin
//        if(GetCardPin(rpins[pinidx],&ThisPin,&ThisCard)) // 2/13/07 - GetCardPin always returns zero. make a void return.
//            return(0);
     GetCardPin(rpins[pinidx],&ThisPin,&ThisCard);  // ThisPin (0-479(m927)or 639(m917)) and ThisCard are 0-9 relative
     SetRespCardLevel(VoltOne, VoltZero, ThisPin, ThisCard,&m_Td925setup[ThisCard],Fnc);
   	 plist[pinidx]=TERM9_SCOPE_CHAN(rpins[pinidx]);
   }
	//tets-if the CURRENT-ONE MAX or CURRENT-ONE MIN modifiers are used, apply active load,
	if(m_CurrentOne.Exists||m_CurrentZero.Exists||m_VoltageQuies.Exists) //
	{	
		IFNSIM(Status = terM9_setChannelLoad (m_Handle, pcnt, plist, TERM9_LOAD_ON));
 		if (Status)
		 ErrorM9(m_Handle, Status, "terM9_setChannelLoad");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelLoad(%d, %d, plist, TERM9_LOAD_ON)", m_Handle,pcnt));
	}
	else
	{
		IFNSIM(Status = terM9_setChannelLoad (m_Handle, pcnt, plist, TERM9_LOAD_OFF)); 	//Default Load Off	
 		if (Status)
     	 ErrorM9(m_Handle, Status, "terM9_setChannelLoad");
		CEMDEBUG(10,cs_FmtMsg("terM9_setChannelLoad(%d, %d, plist, TERM9_LOAD_OFF)", m_Handle,pcnt));
    }
	return 0;
}	


///////////////////////////////////////////////////////////////////////////////
// Function: SetupStim(int Fnc)
//
// Purpose: Perform the Setup STIM action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////


int CDwg_T::SetupStim(int Fnc)
{
	int spins[TOTAL_PINS+4];   					 // list of pins being addressed
	int pcnt=0, pinidx;        	             // Pin Count
    int ThisPin, ThisCard;
//	int dcnt,dsiz;BUILD_WARN
//	int pinCount;BUILD_WARN
//	int num;BUILD_WARN
	
//	int chanIndexer;BUILD_WARN

	int tempCount=0;
	
	double VoltOne = DEFAULT_VOLT_ONE_STIM;
    double VoltZero = DEFAULT_VOLT_ZERO_STIM;

//	DATUM *datum;BUILD_WARN

    // Setup action for the M9
    CEMDEBUG(5,cs_FmtMsg("SetupStim (MPA %d) called FNC %d",m_PrimaryAdr, Fnc));
    if(ParseSetup(Fnc, spins, &pcnt)) 
        return(0);
		
	//set permanant count for terM9 calls
		
    if(m_VoltOne.Exists)
        VoltOne = m_VoltOne.Real;
    if(m_VoltZero.Exists)
        VoltZero = m_VoltZero.Real;

    // Walk Pinlist and Setup Cards as required
    for(pinidx = 0; pinidx < pcnt; pinidx++)
    {
        // Get the DTI Card Number and relative pin  
//        if(GetCardPin(spins[pinidx],&ThisPin,&ThisCard)) //2/13/07 - GetCardPin always returns zero. void return
//            return(0);
        GetCardPin(spins[pinidx],&ThisPin,&ThisCard);  // ThisPin (0-479) and ThisCard are 0-9 relative
        SetStimCardLevel(VoltOne, VoltZero, ThisPin, ThisCard,&m_Td925setup[ThisCard],Fnc);
    }
   return 0;
 }	

////////////////////////////////////////////////////////////////////////////
// Function: ParseSetup(int Fnc, int *pins, int *pcnt)
//
// Purpose: Maps pin or channel number to array index
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Handle           ViSession       Device Handle
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////


int CDwg_T::ParseSetup(int Fnc, int *pins, int *pcnt)
{
	
//	int tempVal;BUILD_WARN
    int tempCount=0;
	int temp;
	DATUM	*pdat=NULL;      
	int n;
	static int goodPins=0;
	int nCount = 0;

	CEMDEBUG(5,cs_FmtMsg("Parse Setup (MPA %d) called FNC %d",m_PrimaryAdr, Fnc));

// Get the list of pins that are going to be set up as being part of a digital configuration
    CEMDEBUG(10,cs_FmtMsg("Check Go Connect flag %d for (MPA %d) called FNC %d",goforcnxflag,m_PrimaryAdr,Fnc));
	if (!goforcnxflag)
	{   Fnc1st = Fnc;
		switch (Fnc)
			{
			     case FNC_APLY_STATIC_LOGIC_DATA_DS:
					pdat = RetrieveDatum(M_STIM, K_FDD);
                    if(!pdat)
                        pdat = RetrieveDatum(M_STIM,K_CDP);
				break;

				case FNC_STIM_STATIC_LOGIC_DATA:
					pdat = RetrieveDatum(M_STIM, K_CDP);
				break;

				case FNC_MEAS_STATIC_LOGIC_DATA_DS:
					pdat = RetrieveDatum(M_RESP, K_FDD);
				break;

				case FNC_PROV_STATIC_LOGIC_DATA:
				case FNC_SENS_STATIC_LOGIC_DATA:
					pdat = RetrieveDatum(M_RESP, K_CDP);
				break;
			
				default:
		        CEMDEBUG(10,cs_FmtMsg("returning without setting datum pointer (%x) or parsing", pdat));
				 return 0;
				break;
			}

 // 12/15/06 - check to make sure there is a channel address. If not then will need to try and collect it from a wrts CON (M_PATH) ststement. 
	// This was caused by a variable connection, CNX, field.
      CEMDEBUG(10,cs_FmtMsg("datum pointer (%x) gocnxflag %d", pdat,goforcnxflag));
		if (pdat == 0) 
		{ 
		 goforcnxflag = true; // the channel data pointer is void. recover the channels when the do connect happens. 
		 return 1;
		}
    }   
	if (goforcnxflag) 
	    {
	     if (pdat != NULL) FreeDatum(pdat); // 03/15/07a-
		 pdat = RetrieveDatum(M_PATH, K_CON);
		 nCount = DatCnt(pdat);
		 nCount = nCount/3;
		 goforcnxflag = false; 
		CEMDEBUG(10,cs_FmtMsg("datum pointer= %x gocnxflag= %d nCountretrieved = %x", pdat,goforcnxflag,nCount));
		 nCount = nCount/3;
		}
	goodPins=0;
    CEMDEBUG(10,cs_FmtMsg("datum pointer= %x gocnxflag= %d nCount = %x", pdat,goforcnxflag,nCount));

	if (pdat != NULL)
		{
    	 *pcnt = DatCnt(pdat);
 		 if (nCount !=0) *pcnt = nCount;
         CEMDEBUG(10,cs_FmtMsg("pin count= %x ", *pcnt));
		 for (n=0; n<*pcnt; n++)
 			{
				temp=INTDatVal(pdat,n);
				if (nCount !=0) temp=INTDatVal(pdat,n*nCount+2);
				if(temp!=0)
				{
					if((Fnc==FNC_APLY_STATIC_LOGIC_DATA_DS) || (Fnc==FNC_MEAS_STATIC_LOGIC_DATA_DS))
						pins[goodPins] = (temp=temp-5)/10;
					else
						pins[goodPins] = temp - 200 ;
					goodPins++;				   //note - this added 7/28/06 PLL-EADS
 				   // CEMDEBUG(11,cs_FmtMsg("Parse Setup pin[%d]=%d  Path# %d",goodPins-1,(pins[goodPins-1]),temp));    //02/19/07-
				}
			  }
		 *pcnt=goodPins;
	     if ((Fnc == FNC_PROV_STATIC_LOGIC_DATA)|| (Fnc == FNC_SENS_STATIC_LOGIC_DATA))
		   m_RespChnlCnt = goodPins;
		 if (pdat!=NULL) FreeDatum(pdat);  //03/15/07a-
	     // FreeDatum(pdat); // 02/19/07-
		} 
	if(Fnc==FNC_APLY_STATIC_LOGIC_DATA_DS) //0313/07-??
		{
			m_StimPcnt=*pcnt;
		//	m_StimPins=(ViInt32*)malloc(m_StimPcnt*sizeof(ViInt32));  //03/13/07-??
			runOnce=0;//to reset flag indicating whether or not to run initiate following CDwg_T::Close();
//			for (n=0; n<*pcnt; n++)
			for (n=0; n<goodPins; n++)  //03/14/07-
			{
			  m_StimPins[n]=pins[n];
			  m_Td925pin[pins[n]].reset = FNC_APLY_STATIC_LOGIC_DATA_DS; // Flag to be reset at disable digital configuration
            }			
		}
	if (Fnc==FNC_MEAS_STATIC_LOGIC_DATA_DS)
		{
 		 m_RespPcnt = *pcnt;
			for (n=0; n<m_RespPcnt; n++)
			{
			  m_RespPins[n]=pins[n];
			  if(m_Td925pin[pins[n]].reset != FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[pins[n]].reset = FNC_MEAS_STATIC_LOGIC_DATA_DS; // Flag to be reset at disable digital configuration //03/19/07-
			}			
		}
	else
		for (n=0; n<*pcnt; n++)
			//01/07/07- embellish the channels to reset. 
			if (Fnc == FNC_APLY_STATIC_LOGIC_DATA_DS)m_Td925pin[pins[n]].reset = m_Td925pin[pins[n]].reset;
			else if((Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS)&&(m_Td925pin[pins[n]].reset != m_Td925pin[pins[n]].reset)) 
            {
		       m_Td925pin[pins[n]].reset = FNC_MEAS_STATIC_LOGIC_DATA_DS; //03/19/07-
            }
			else m_Td925pin[pins[n]].reset = 1; // Flag to be reset at disable digital configuration 
	
    return 0;
}
////////////////////////////////////////////////////////////////////////////
// Function: GetCardPin(int pinNum, int *ThisPin, int *ThisCard)
//
// Purpose: Gets the pin number and card number
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Handle           ViSession       Device Handle
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////

void CDwg_T::GetCardPin(int pinNum, int *ThisPin, int *ThisCard)
{
	*ThisPin = pinNum;
	*ThisCard = (pinNum/PINS_PER_CARD)+1;
	return;
//	return 0;
}
////////////////////////////////////////////////////////////////////////////
// Function: SystemPin(int pin, int card)
//
// Purpose: Maps pin number of a card to systemwide pin number
//          Example pin# 10 on card# 5 = system pin# 202 (i.e. (5-1)*48+10))
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Status           int             Error code returned from driver
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////

int CDwg_T::SystemPin(int CardPin, int CardNumber)
{
	if ((CardPin >= 0 && CardPin < PINS_PER_CARD)&&
	   (CardNumber>= 1 && CardNumber <=TOTAL_CARDS))
	{
		return (CardNumber - 1)*PINS_PER_CARD+CardPin;
	}
	return -1;
}

////////////////////////////////////////////////////////////////////////////
// Function: SetRespCardLevel(ViSession Handle,
//                            double VoltOne, double VoltZero, double SlewRate,
//                            int ThisPin, int ThisCard, TRM9cardset *Setup, int Fnc)
//
// Purpose: Maps pin or channel number to array index
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Handle           ViSession       Device Handle
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Setup            TRM9cardset*  Set/use the appropriate values in struct
//
////////////////////////////////////////////////////////////////////////////
int CDwg_T::SetRespCardLevel(double VoltOne, double VoltZero, int ThisPin, int ThisCard, TRM9cardset *Setup, int Fnc)
{
    int lvlidx = 1; 
	int Status = 0;
	int range = TERM9_LEVEL_RANGE_AUTO;
	int chnlcnt = 1;
	char ErrMsg[256];
	ViInt32 lvlidxp;

	// Check the voltage value to set voltage range.
    // Not required for M927 or M925 card SET FOR (20V span -15 to 15)
	if (VoltOne > VoltZero)
	{
		if ((VoltOne <= 5) && (VoltZero >= -2))
			range = TERM9_LEVEL_RANGE_2TO5;
		else if ((VoltOne <= 10) && (VoltZero >= -10))
			range = TERM9_LEVEL_RANGE_10TO10;
		else if ((VoltOne <= 15) && (VoltZero >= -5))
			range = TERM9_LEVEL_RANGE_5TO15;
	}
	else // VoltZero > VoltOne
	{
		if ((VoltZero <= 5) && (VoltOne >= -2))
			range = TERM9_LEVEL_RANGE_2TO5;
		else if ((VoltZero <= 10) && (VoltOne >= -10))
			range = TERM9_LEVEL_RANGE_10TO10;
		else if ((VoltZero <= 15) && (VoltOne >= -5))
			range = TERM9_LEVEL_RANGE_5TO15;
	}

   // find which level to be used also check to see if the other cards have been set for this level.
	lvlidx = 0;
	switch (ThisCard)
	{
	case 1:
	 if(m_Td925setup[2].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[2].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vo.l == VoltZero)) break;
	  ++lvlidx;
	  break;
	 }
	 if(m_Td925setup[3].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[3].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vo.l == VoltZero)) break;
	  ++lvlidx;
	  break;	  	
	 }
   ++lvlidx;
	 if(m_Td925setup[2].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[2].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vo.l == VoltZero)) break;
	 --lvlidx;
	 break;
	 }
	 if(m_Td925setup[3].Level[lvlidx].used.resp)
	 {
	  if ((m_Td925setup[3].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vo.l == VoltZero)) break;
	 --lvlidx;
	 break;
	 }
	--lvlidx;
	break;
	case 2:
	 if(m_Td925setup[1].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[1].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vo.l == VoltZero)) break;
	  ++lvlidx;
	  break;	  	
	 }
	 if(m_Td925setup[3].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[3].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vo.l == VoltZero)) break;
	  ++lvlidx;
	  break;
	 }
   ++lvlidx;
	 if(m_Td925setup[1].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[1].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vo.l == VoltZero)) break;
	  --lvlidx;
	  break;
	 }
	 if(m_Td925setup[3].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[3].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vo.l == VoltZero)) break;
	  --lvlidx;
	  break;
	 }
	--lvlidx;
	break;
	case 3:
	 if(m_Td925setup[2].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[2].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vo.l == VoltZero)) break;
	  ++lvlidx;
	  break;
	 }
	 if(m_Td925setup[1].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[1].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vo.l == VoltZero)) break;
	  ++lvlidx;
	  break;
	 }
   ++lvlidx;
	 if(m_Td925setup[2].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[2].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vo.l == VoltZero)) break;
	  --lvlidx;
	  break;
	 }
	 if(m_Td925setup[1].Level[lvlidx].used.resp)
	 {
	  if((m_Td925setup[1].Level[lvlidx].vo.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vo.l == VoltZero)) break;
	  --lvlidx;
	  break;
	 }
	--lvlidx;
	break;
	default:
 	 break;
    }

	for(; lvlidx <= 2; lvlidx++)
    {
     if(lvlidx >= 2)
        {
            // ERROR More than two levels for this card 
		 sprintf(ErrMsg, "More Than 2 Voltage Levels Used For Card %d, Pin %d, setting to first voltage level set",ThisCard, ThisPin);
		 //CEMERROR(10,"MORE THAN 2 LEVELS"); 
            return(-1);
        }
	// check if using existing level on ThisCard
     if((*Setup).Level[lvlidx].used.resp)
        {
		 if(((*Setup).Level[lvlidx].vo.h == VoltOne)&&((*Setup).Level[lvlidx].vo.l == VoltZero))
		 {
		  (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.resp = lvlidx+1;
		  if(m_Td925pin[ThisPin].reset!= FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = FNC_MEAS_STATIC_LOGIC_DATA_DS;   //03/19/07-
	 	  if(Fnc != FNC_MEAS_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = 1;  // Flag to be reset at disable digital configuration. either by meas, sese or prove. stims are done seperately
		  break; //Yes it is using existing level
		 }
		 ++lvlidx;
        if((*Setup).Level[lvlidx].used.resp)
        {
		 if(((*Setup).Level[lvlidx].vo.h == VoltOne)&&((*Setup).Level[lvlidx].vo.l == VoltZero))
		 {
		  (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.resp = lvlidx+1;
		  if(m_Td925pin[ThisPin].reset!= FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = FNC_MEAS_STATIC_LOGIC_DATA_DS;   //03/19/07-
		  if(Fnc != FNC_MEAS_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = 1;  // Flag to be reset at disable digital configuration
		  break; //Yes it is using existing level
		 }
		 //1/29/07- if response is set but levels don't match need to either clear the level or ignore entirely. needs message here. 
		 // (*Setup).Level[lvlidx-1].used.resp = 0;
		CEMDEBUG(10,cs_FmtMsg("Response is set but levels don't match and 2  for this card %d and pin %d are in use %d",ThisCard,ThisPin,lvlidx)); // 3/5/07- debug for pcr219
		}
		--lvlidx;
       }
     else
        {
          if(!((*Setup).SetGndLvl))
            {
				IFNSIM(Status = terM9_setGroundReference(m_Handle,TERM9_SCOPE_CARD(ThisCard - 1),TERM9_GROUND_INTERNAL));
 				if (Status) ErrorM9(m_Handle, Status, "terM9_setGroundReference");
				CEMDEBUG(10,cs_FmtMsg("terM9_setGroundReference(%d, TERM9_SCOPE_CARD(%d),TERM9_GROUND_INTERNAL)", m_Handle, ThisCard));
				IFNSIM(Status = terM9_setLevelRange (m_Handle,TERM9_SCOPE_CARD(ThisCard),range));
				if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelRange");
				CEMDEBUG(10,cs_FmtMsg("terM9_setLevelRange(%d, TERM9_SCOPE_CARD(%d), %d)", m_Handle, ThisCard, range));
            }
 		if((m_Td925pin[ThisPin].reset == FNC_APLY_STATIC_LOGIC_DATA_DS)&&(Fnc == FNC_MEAS_STATIC_LOGIC_DATA_DS)) //04/30/07 - added for [cr 234.
		 {
		  IFNSIM(Status = terM9_getChannelLevel(m_Handle,TERM9_SCOPE_CHAN(ThisPin),&lvlidxp));  //04/30/07- pcr 234. The stim channel has been set. use that levelset index
		  lvlidx = (int)lvlidxp;
		  (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.resp = lvlidx;
		 }
	    else		
	     {
		  (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.resp = lvlidx+1;   //04/30/07-  tag out by default. 
		  if(m_Td925pin[ThisPin].reset!= FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = FNC_MEAS_STATIC_LOGIC_DATA_DS;  //04/05/07-
		  if (Fnc != FNC_MEAS_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = 1;// 1/27/07- Flag to be reset at disable digital configuration
	     }
		// Setup new level for this card
		(*Setup).Level[lvlidx].used.resp = TRUE;
		(*Setup).Level[lvlidx].vo.h = VoltOne;
		(*Setup).Level[lvlidx].vo.l = VoltZero;
		(*Setup).Level[lvlidx].slewrate = DEFAULT_SLEW_RATE;
		(*Setup).SetGndLvl = 1;

	   IFNSIM(Status = terM9_setLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vo.h));
 	   if (Status) // ErrorM9(m_Handle, Status, "terM9_setLevelSetVOH");
		{
			(*Setup).Level[lvlidx].vo.h = (*Setup).Level[lvlidx].vo.h - 0.040;
			Status = 0;
			IFNSIM(Status = terM9_setLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vo.h));
 			if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetVOH");
		}
		CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVOH(%d, TERM9_SCOPE_CARD(%d), %d, %f)",m_Handle, ThisCard, lvlidx,(*Setup).Level[lvlidx].vo.h));

 		IFNSIM(Status = terM9_setLevelSetVOL (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vo.l));
 		if (Status) //ErrorM9(m_Handle, Status, "terM9_setLevelSetVOL");
		{
			(*Setup).Level[lvlidx].vo.l = (*Setup).Level[lvlidx].vo.l + 0.040;
			Status = 0;
 			IFNSIM(Status = terM9_setLevelSetVOL (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vo.l));
 			if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetVOL");			 
		}
		CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVOL(%d, TERM9_SCOPE_CARD(%d), %d, %f)",m_Handle, ThisCard, lvlidx,(*Setup).Level[lvlidx].vo.l));

		if(m_VoltageQuies.Exists)
		{ // ViReal64 VoltQuies = m_VoltageQuies.Real;
			IFNSIM(Status = terM9_setLevelSetVCOM(m_Handle, TERM9_SCOPE_CARD(ThisCard), lvlidx, m_VoltageQuies.Real));
	        CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVCOM(%d, TERM9_SCOPE_CARD%d, levelidx= %d,VCOM= %f  Status= %d)",m_Handle, ThisCard, lvlidx, m_VoltageQuies.Real));
 	        if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetVCOM");
 	       // if (Status) Status = 0;
		}
		if (m_CurrentOne.Exists)
		 { // ViReal64 CurrSour = m_CurrentOne.Real;
			IFNSIM(Status = terM9_setLevelSetIOH (m_Handle, TERM9_SCOPE_CARD(ThisCard),lvlidx, m_CurrentOne.Real));
 	        if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetIOH");
	        CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetIOH(%d, TERM9_SCOPE_CARD%d, %d, %f)",m_Handle, ThisCard, lvlidx, m_CurrentOne.Real));
		 }
		if (m_CurrentZero.Exists)
		 { // ViReal64 CurrSink = m_CurrentZero.Real;
			IFNSIM(Status = terM9_setLevelSetIOL(m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx, m_CurrentZero.Real));
 	        if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetIOL");
	        CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetIOL(%d, TERM9_SCOPE_CARD%d, %d, %f)",m_Handle, ThisCard, lvlidx, m_CurrentZero.Real));
		 }
        break; // 1/29/07 - 
		}
    }
  return(0);
}
////////////////////////////////////////////////////////////////////////////
// Function: SetStimCardLevel(double VoltOne, double VoltZero, int ThisPin, 
//                                      int ThisCard, TRM9cardset *Setup, Fnc)
//
// Purpose: Maps pin or channel number to array index
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Handle           ViSession       Device Handle
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Setup            TRM9cardset*  Set/use the appropriate values in struct
//
////////////////////////////////////////////////////////////////////////////

int CDwg_T::SetStimCardLevel(double VoltOne, double VoltZero, int ThisPin, int ThisCard, TRM9cardset *Setup, int Fnc)
{
    int lvlidx = 0;
	int Status = 0;
	int range = TERM9_LEVEL_RANGE_AUTO;
    char ErrMsg[256];
   // Not required for M927 OR M925 card set for (20V span -15 to 15)
	// Check the voltage value to set voltage range.
	if (VoltOne > VoltZero)
	{
		if ((VoltOne <= 5) && (VoltZero >= -2))
			range = TERM9_LEVEL_RANGE_2TO5;
		else if ((VoltOne <= 10) && (VoltZero >= -10))
			range = TERM9_LEVEL_RANGE_10TO10;
		else if ((VoltOne <= 15) && (VoltZero >= -5))
			range = TERM9_LEVEL_RANGE_5TO15;
	}
	else // VoltZero > VoltOne
	{
		if ((VoltZero <= 5) && (VoltOne >= -2))
			range = TERM9_LEVEL_RANGE_2TO5;
		else if ((VoltZero <= 10) && (VoltOne >= -10))
			range = TERM9_LEVEL_RANGE_10TO10;
		else if ((VoltZero <= 15) && (VoltOne >= -5))
			range = TERM9_LEVEL_RANGE_5TO15;
	}
   // find which level to be used. Also check the other cards first to see if they have been set for these voltage levels
	lvlidx = 0;
	switch (ThisCard)
	{
	case 1:
	 if (m_Td925setup[2].Level[lvlidx].used.stim)
	  {
		if ((m_Td925setup[2].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vi.l == VoltZero)) break;
		++lvlidx;
		break;
	  }
	 if (m_Td925setup[3].Level[lvlidx].used.stim)
	  {
	   if((m_Td925setup[3].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vi.l == VoltZero)) break;
	   ++lvlidx;
	   break;
	  }
   ++lvlidx;
	 if (m_Td925setup[2].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[2].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vi.l == VoltZero)) break;
  	   --lvlidx;
	   break;
	  }
	 if (m_Td925setup[3].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[3].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vi.l == VoltZero)) break;
	   --lvlidx;
	   break;
	  }
	--lvlidx;
	break;
	case 2:
	 if (m_Td925setup[1].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[1].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vi.l == VoltZero)) break;
	   ++lvlidx;
	   break;
	  }
	 if (m_Td925setup[3].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[3].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vi.l == VoltZero)) break;
	   ++lvlidx;
	   break;
	  }
    ++lvlidx;
	 if (m_Td925setup[1].Level[lvlidx].used.stim)
	  { 
	   if ((m_Td925setup[1].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vi.l == VoltZero)) break;
	   --lvlidx;
	   break;
	  }
	 if (m_Td925setup[3].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[3].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[3].Level[lvlidx].vi.l == VoltZero)) break;
  	   --lvlidx;
	   break;
	  }
	--lvlidx;
	break;
	case 3:
	 if (m_Td925setup[2].Level[lvlidx].used.stim)
	  {
	   if((m_Td925setup[2].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vi.l == VoltZero)) break;
	   ++lvlidx;
	   break;
	  }
	 if (m_Td925setup[1].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[1].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vi.l == VoltZero)) break;
	   ++lvlidx;
	   break;
	  }
     ++lvlidx;
	 if (m_Td925setup[2].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[2].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[2].Level[lvlidx].vi.l == VoltZero)) break;
	   --lvlidx;
	   break;	   
	  }
	 if (m_Td925setup[1].Level[lvlidx].used.stim)
	  {
	   if ((m_Td925setup[1].Level[lvlidx].vi.h == VoltOne)&&(m_Td925setup[1].Level[lvlidx].vi.l == VoltZero)) break;
	   --lvlidx;
	   break;
	  }
	--lvlidx;
	break;
	default:
 	 break;
   }
//	for(lvlidx = 0; lvlidx <= 2; lvlidx++)
	for(; lvlidx <= 2; lvlidx++)
    {
        if(lvlidx >=2)
        {
         // Warning, More than two levels for this card //11/14/06 found a way to trip into this. by using just one card and setting levels per pin. ( see benchmark .atl for notes on this date. 
            sprintf(ErrMsg, "More Than 2 Voltage Levels Used For Card %d, Pin %d, setting to first voltage level set",
                            ThisCard, ThisPin);
         //   CEMERROR(EB_SEVERITY_WARNING, ErrMsg);
            return(-1);  // revised this to a warning 11/14/06 just use previous levels. 
        }

        // check if using existing level on the same card. 
        if((*Setup).Level[lvlidx].used.stim)
        {
         // if((FudgeEq(VoltOne, (*Setup).Level[lvlidx].vi.h)) &&(FudgeEq(VoltZero, (*Setup).Level[lvlidx].vi.l))) // 1/29/07 - don't understand wufs this does. 
	     if(((*Setup).Level[lvlidx].vi.h == VoltOne)&&((*Setup).Level[lvlidx].vi.l == VoltZero))
		  {
           (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.stim = lvlidx + 1;
		    m_Td925pin[ThisPin].reset = FNC_APLY_STATIC_LOGIC_DATA_DS;
			if (Fnc != FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = 1; // Flag to be reset at disable digital configuration
            break; // ?? Yes it is using existing level
          }
		 ++lvlidx;
		 if((*Setup).Level[lvlidx].used.stim)
		 {
		  if(((*Setup).Level[lvlidx].vi.h == VoltOne)&&((*Setup).Level[lvlidx].vi.l == VoltZero))
		  {
           (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.stim = lvlidx + 1;
		    m_Td925pin[ThisPin].reset = FNC_APLY_STATIC_LOGIC_DATA_DS;
			if (Fnc != FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = 1; // Flag to be reset at disable digital configuration
            break; // ?? Yes it is using existing level
          }
		 }
		 --lvlidx;  //1/31/07 - moved for pcr 201. was incorrect increment
        }
        else
        {
            if(!((*Setup).SetGndLvl))
            {
				IFNSIM(Status = terM9_setGroundReference(m_Handle,TERM9_SCOPE_CARD(ThisCard - 1),TERM9_GROUND_INTERNAL));
 				if (Status) ErrorM9(m_Handle, Status, "terM9_setGroundReference");
				CEMDEBUG(10,cs_FmtMsg("terM9_setGroundReference(%d, TERM9_SCOPE_CARD(%d),TERM9_GROUND_INTERNAL)", m_Handle, ThisCard));

				IFNSIM(Status = terM9_setLevelRange (m_Handle,TERM9_SCOPE_CARD(ThisCard),range));
				if (Status)	ErrorM9(m_Handle, Status, "terM9_setLevelRange");
				CEMDEBUG(10,cs_FmtMsg("terM9_setLevelRange(%d, TERM9_SCOPE_CARD(%d), %d)", m_Handle, ThisCard, range));
            }
            // Setup new level for this card
            (*Setup).Level[lvlidx].used.stim = TRUE;
            (*Setup).Level[lvlidx].vi.h = VoltOne;
            (*Setup).Level[lvlidx].vi.l = VoltZero;
            	//neither rt nor ft
			if(!m_RiseTime.Exists && !m_FallTime.Exists)
				{
				(*Setup).Level[lvlidx].slewrate = DEFAULT_SLEW_RATE;
				}//ft only
			if(!m_RiseTime.Exists && m_FallTime.Exists)
			{
				if(((VoltOne-VoltZero)/(m_FallTime.Real*1e9))>.4)
					(*Setup).Level[lvlidx].slewrate = TERM9_SLEWRATE_HIGH;
				else if(((VoltOne-VoltZero)/(m_FallTime.Real*1e9))<1.5)
					(*Setup).Level[lvlidx].slewrate = TERM9_SLEWRATE_LOW;
				else
					(*Setup).Level[lvlidx].slewrate = DEFAULT_SLEW_RATE;
			
			}//rt only
			if(m_RiseTime.Exists && !m_FallTime.Exists)
			{
				if(((VoltOne-VoltZero)/(m_RiseTime.Real*1e9))>.4)
					(*Setup).Level[lvlidx].slewrate = TERM9_SLEWRATE_HIGH;
				else if(((VoltOne-VoltZero)/(m_RiseTime.Real*1e9))<1.5)
					(*Setup).Level[lvlidx].slewrate = TERM9_SLEWRATE_LOW;
				else
					(*Setup).Level[lvlidx].slewrate = DEFAULT_SLEW_RATE;
			
			}//both rt and ft present
			if(m_RiseTime.Exists && m_FallTime.Exists)
			{
				if(((VoltOne-VoltZero)/(m_RiseTime.Real*1e9))>.4)
					(*Setup).Level[lvlidx].slewrate = TERM9_SLEWRATE_HIGH;
				else if(((VoltOne-VoltZero)/(m_RiseTime.Real*1e9))<1.5)
					(*Setup).Level[lvlidx].slewrate = TERM9_SLEWRATE_LOW;
				else
					(*Setup).Level[lvlidx].slewrate = DEFAULT_SLEW_RATE;
			
			}
		(*Setup).SetGndLvl = 1;
        (*Setup).PinIdx[ThisPin%PINS_PER_CARD].lvlidx.stim = lvlidx + 1; // 01/02/07 ? why the remainder and not the total pins?
	    m_Td925pin[ThisPin].reset = FNC_APLY_STATIC_LOGIC_DATA_DS;
		if (Fnc != FNC_APLY_STATIC_LOGIC_DATA_DS) m_Td925pin[ThisPin].reset = 1;    // Flag to be reset at disable digital configuration

  		// 1/17/07 - a check to see if that set level has actually been set. 
		// ViReal64 VIHValue;
		//IFNSIM(Status = terM9_getLevelSetVIH (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,VIHValue));
		// if (((*Setup).Level[lvlidx].vi.h != VIHValue) || (VIHValue == 0)) 
		// then set it. otherwise skip out. 
	    IFNSIM(Status = terM9_setLevelSetVIH (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vi.h));
 	    if (Status)// 1/18/07 - if we get an error VIH == to VOH or VOL and VIH needs adjustment.
		{
		 (*Setup).Level[lvlidx].vi.h =(*Setup).Level[lvlidx].vi.h+0.040;
		 Status = 0;
		 IFNSIM(Status = terM9_setLevelSetVIH (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vi.h));
 	     if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetVIH");
		}
	    CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVIH(%d, TERM9_SCOPE_CARD(%d), %d, %f)",m_Handle,ThisCard,lvlidx,(*Setup).Level[lvlidx].vi.h));

 	    IFNSIM(Status = terM9_setLevelSetVIL (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vi.l));
 	    if (Status)  // 1/18/07 - if we get an error VIL == to VOH or VOL and VIL needs adjustment.
		{
		 (*Setup).Level[lvlidx].vi.l = (*Setup).Level[lvlidx].vi.l-0.040;
		 Status = 0;
 	     IFNSIM(Status = terM9_setLevelSetVIL (m_Handle,TERM9_SCOPE_CARD(ThisCard),lvlidx,(*Setup).Level[lvlidx].vi.l));
 	     if (Status) ErrorM9(m_Handle, Status, "terM9_setLevelSetVIL");  // if it is the second (minus 40mV) setup then throw the exception. 
		}
	    CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVIL(%d, TERM9_SCOPE_CARD(%d), %d, %f)",m_Handle,ThisCard,lvlidx,(*Setup).Level[lvlidx].vi.l));
        break; 
        }
    }
    return(0);
}

////////////////////////////////////////////////////////////////////////////
// Function: fnPin(int iChan)
//
// Purpose: Maps pin or channel number to array index
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Status           int             Error code returned from driver
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////

int CDwg_T::fnPin(int iChan)
{
	if (iChan >= 1 && (iChan-5)/10 <= TOTAL_PINS) 
	//return iChan - 1;
	
		return iChan ;
	return -1;
}

////////////////////////////////////////////////////////////////////////////
// Function: ErrorM9(int Status)
//
// Purpose: Query M9 for the error text and send to WRTS
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// Status           int             Error code returned from driver
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
//
// 
////////////////////////////////////////////////////////////////////////////

void CDwg_T::ErrorM9(ViSession id, ViStatus Status, char api[256])
{
	ViChar message[256]; 
	ViChar msg_buf[256]; 
    int      Flags, Err;
    ViInt32  QError;
	
	if (Status != VI_SUCCESS)
	{
		message[0] = '\0';
		IFNSIM((Err = terM9_errorMessage(id,Status,&message[0])));
		CEMDEBUG(10,cs_FmtMsg("terM9_errorMessage(%d, %d, &message[0])", id,Status));
        if(Err)
		{
            sprintf(msg_buf,"Plug 'n' Play Error: Unable to get error text [%X]!", Err);
			Display(msg_buf);
		}

		CEMDEBUG(5,cs_FmtMsg("\033[31;40mERROR FROM %s - [%s]\033[m",api, message));

        if(Status < 0)
            Flags = EB_SEVERITY_ERROR ;
        else
            Flags = EB_SEVERITY_WARNING ;

        INSTERROR(Flags, message);
	}
    QError = 1;
    // Retrieve any pending errors in the device
    while(QError)
    {
        QError = 0;
        IFNSIM((Err = terM9_errorQuery(id, &QError, message)));
		CEMDEBUG(10,cs_FmtMsg("terM9_errorQuery(%d, &QError, message)", id,message));
        if(Err != 0)
            break;
        if(QError)
        {
			CEMDEBUG(5,cs_FmtMsg("\033[31;40mERROR FROM %s - %d [%s]\033[m",api, QError, message));
            if(QError < 0)
                Flags = EB_SEVERITY_ERROR ;
            else
                Flags = EB_SEVERITY_WARNING ;

            INSTERROR(Flags, message);
        }
    }
}
////////////////////////////////////////////////////////////////////////////
// Function: ProcessResponseData(void)
//
// Purpose: Reads the data from the DWG and creates:
//            m_RespByteData
//            m_ErrByteData
//            m_FaultCnt
//          for subsequent fetches as appropriate
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
int CDwg_T::ProcessResponseData(int fetchMod,int dcnt,int dsiz)
{
    ViStatus Status = 0;

    ViInt32 *ResultList=NULL;
    long  ResultListCnt=0;
    int   bitidx;
	int   BytesPerPat=0;
	//original-ResultList = (long*)malloc(((m_RespPcnt * m_PatCount) +8) * sizeof(ViInt32));
    ResultList=(ViInt32*)malloc(((m_RespPcnt * m_PatCount) +8) * sizeof(ViInt32));
    m_RespByteCnt = (BytesPerPat * m_PatCount);
	ViInt32 *tempArray=NULL;
	int counter=0;

    // Get the data from the DWG
	if(m_wasDynamic==1)
		{
			tempArray=(ViInt32*)malloc(m_respBuff[m_fetchCounter].pins.count*sizeof(ViInt32));
			for( counter=0;counter<m_respBuff[m_fetchCounter].pins.count;counter++)
				{
					tempArray[counter]=TERM9_SCOPE_CHAN(m_respBuff[m_fetchCounter].pins.list[counter]);
				}
			IFNSIM(Status = terM9_fetchDynamicPinResults(m_Handle,(ViInt32)m_respBuff[m_fetchCounter].pins.count,
														 tempArray,m_fetchCounter,(ViInt32)m_PatCount,&ResultListCnt,ResultList));
			if (Status)ErrorM9(m_Handle, Status, "terM9_fetchDynamicPinResults");
			CEMDEBUG(10,cs_FmtMsg("terM9_fetchDynamicPinResults(handle %d, respPinCnt %d, patCnt %d, ResultsListCnt %d, was dynamic)", 
								   m_Handle,m_RespPcnt,m_PatCount,ResultListCnt));
		}
		else
		{
			tempArray=(ViInt32*)malloc(m_RespPcnt*sizeof(ViInt32));
			for( counter=0;counter<m_RespPcnt;counter++)
				{
					tempArray[counter]=TERM9_SCOPE_CHAN(m_RespPins[counter]);
				}
	//		if( meas_illegalState == FNC_MEAS_STATIC_LOGIC_DATA_DS)
	//		{
	//		  IFNSIM(Status = terM9_fetchStaticPinResults(m_Handle,m_RespPcnt,tempArray,&ResultListCnt,ResultList)); //04/24/07- added for pcr 234 mea and 
	//		}
	//		else
	//		{
			  IFNSIM(Status = terM9_fetchDynamicPinResults(m_Handle,m_RespPcnt,tempArray,0,(ViInt32) m_PatCount,&ResultListCnt,ResultList)); 
	//		}
			if (Status) ErrorM9(m_Handle, Status, "terM9_fetchDynamicPinResults");
			CEMDEBUG(10,cs_FmtMsg("terM9_fetchDynamicPinResults(handle %d, respPinCnt %d, patCnt %d, ResultsListCnt %d, was static)",
								   m_Handle,m_RespPcnt,m_PatCount,ResultListCnt));
		}
	//CEMDEBUG(10,cs_FmtMsg("freeing tempArray, %x",tempArray));
 	free(tempArray);
	int bitCount= ResultListCnt;
	m_BitCount=ResultListCnt;
	CEMDEBUG(10,cs_FmtMsg("setup for polling bits: m_pRespValue= %x, bitCount= %d, resultList= %x",m_pRespValue,bitCount,ResultList));
	for (bitidx=0; bitidx<ResultListCnt; bitidx++)
	{
	 // CEMDEBUG(10,cs_FmtMsg("bit: %d results from m9: %d ",bitidx,ResultList[bitidx]));
		if (ResultList[bitidx] == TERM9_RESULT_FAIL)   // as in failed the hardware compare, looking for VOH, or VOL. Tristate is in purgatory. 
		  {
			m_pRespValue[bitidx]=1;
		//   if( meas_illegalState == FNC_MEAS_STATIC_LOGIC_DATA_DS) m_pRespValue[bitidx]=0;
		  }
		else 
		 {
		   m_pRespValue[bitidx]=0;		
		//   if( meas_illegalState == FNC_MEAS_STATIC_LOGIC_DATA_DS) m_pRespValue[bitidx]=0;
		 }
    //CEMDEBUG(10,cs_FmtMsg("bit: %d results from m9: %d ",bitidx,ResultList[bitidx]));
	}
	if (m_BitCount != bitCount) m_BitCount = bitCount;   
    return(0);
}

//============================================================================================
void CDwg_T::runBurst(int Fnc)
{
	ViStatus Status=0;
	ViInt32 testResult=0;
	
//	if( meas_illegalState == FNC_MEAS_STATIC_LOGIC_DATA_DS)  //04/24/07- added for pcr 234 
//	{
//	 IFNSIM(Status = terM9_runStaticPattern(m_Handle,VI_FALSE,VI_TRUE)); //04/24/07- added for pcr 234
//	 return;
//	}
	if(m_inDynamic==1)
	{
		/*
		Display("In Dynamic Test");
		
		for(int j=0;j<m_patternCount;j++)
			{
			for(int k=0;k<TOTAL_PINS;k++)
				{
					if(m_patternBuffer[j][k]==1)
						{
							IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_IL));
 							if (Status) ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IL)", m_Handle,k));

						}
					if(m_patternBuffer[j][k]==2)
						{
							IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_IH));
 							if (Status)	ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_IH)", m_Handle,k));

						}
						if(m_patternBuffer[j][k]==3)
						{
							IFNSIM(Status = terM9_setChannelPinOpcode(m_Handle,TERM9_SCOPE_CHAN(k),TERM9_OP_OL));
 							if (Status)	ErrorM9(m_Handle, Status, "terM9_setChannelPinOpcode");
							CEMDEBUG(11,cs_FmtMsg("terM9_setChannelPinOpcode(%d, TERM9_SCOPE_CHAN(%d),TERM9_OP_OL)", m_Handle,k));

						}
				}
			IFNSIM(Status = terM9_loadDynamicPattern(m_Handle, VI_FALSE, VI_FALSE));
			if (Status)
				ErrorM9(m_Handle, Status, "terM9_loadDynamicPattern");

			CEMDEBUG(10,cs_FmtMsg("terM9_loadDynamicPattern(%d, VI_FALSE, VI_FALSE)", m_Handle));
			*/
	}		
	else
	{
	 Status = checklevelSets();
	if (m_patternCount>1) // 01/17/07 - make a distinction between a 1 off pattern burst and a multiple pattern burst. 
	{   // 1/18/07 - multiple pattern burst  but should never execute this segment. It is part of initiate 
     IFNSIM(Status = terM9_setDynamicPattern (m_Handle,1,0,TERM9_COP_NOP ,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE)); //TERM9_COP_HALT 1/15/07 
	 if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPattern");
	 CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPattern(%d,1,0,TERM9_COP_NOP,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE) multiple runburst status = %d  pattern count %d", m_Handle,Status,m_patternCount));
	}
	else
	{  // one off burst. 
	 IFNSIM(Status = terM9_setDynamicPattern (m_Handle,1,0,TERM9_COP_HALT ,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE)); //TERM9_COP_HALT 1/15/07 
	 if (Status) ErrorM9(m_Handle, Status, "terM9_setDynamicPattern");
	 CEMDEBUG(10,cs_FmtMsg("terM9_setDynamicPattern(%d,1,0,TERM9_COP_HALT,0,TERM9_COND_TRUE,VI_FALSE,VI_FALSE,0,VI_FALSE) single runburst status = %d    %d", m_Handle,Status,m_patternCount));
	}
	//Load this initialization pattern Into RAM and increment counter
	IFNSIM(Status = terM9_loadDynamicPattern(m_Handle, VI_FALSE, VI_FALSE));
	if (Status) ErrorM9(m_Handle, Status, "terM9_loadDynamicPattern");
	CEMDEBUG(10,cs_FmtMsg("terM9_loadDynamicPattern(%d, VI_FALSE, VI_FALSE)", m_Handle));

	//Run "BURST" dynamic patterns using terM9_runDynamicPatternSet
    IFNSIM(Status = terM9_runDynamicPatternSet(m_Handle, VI_FALSE, &testResult));
    if (Status) ErrorM9(m_Handle, Status, "terM9_runDynamicPatternSet Burst fault");
    CEMDEBUG(10,cs_FmtMsg("terM9_runDynamicPatternSet(%d, VI_FALSE, %d)", m_Handle,testResult));
	}
}
//=====================================================================================================================
	void CDwg_T::resetHV(int Fnc) // 32 CRB pins
	{
		terM9_resetHighVoltagePins(m_Handle);
	}
//=======================================================================================================================
	void CDwg_T::setupHV(int Fnc)
	{
		DATUM *datum;

		ViReal64 VoltOne = 4.5;     // 9/25 should be 4.0V
		ViReal64 VoltZero = 1.0;   // 9/25 should be 0.8v
		int dcnt=0,dsiz=0,dtyp=0;
		int idx=0;
		int temp=0;
		int i=0;
		ViInt32 lastHVPin;
		int Status =0;

			if(m_VoltOne.Exists)
				VoltOne=m_VoltOne.Real;
		if(m_VoltZero.Exists)
				VoltZero=m_VoltZero.Real;
		m_hvThreshold= VoltOne/2;  // there is no low threshold for udbs so ignore. 
		if (m_hvThreshold<1.5) m_hvThreshold=1.5; // and if the threshod is less than 1.5 make it 1.5.
		
		IFNSIM(Status = terM9_getLastHighVoltagePinIndex(m_Handle,&lastHVPin)); // 10/20/06 retrieve the highest udb channel available. 
		if (Status) ErrorM9(m_Handle, Status, "terM9_getLastHighVoltagePinIndex ");

		if(Fnc==FNC_APLY_STATIC_LOGIC_DATA_SCAN)
		{
			runOnceHV=0;
			if(datum=RetrieveDatum(M_STIM,K_FDD)) 
				{
				dcnt=DatCnt(datum);
				dsiz=DatSiz(datum);
//				dtyp=DatTyp(datum);
				m_hvStimCount=0;
				if (dcnt > (lastHVPin+1)) ErrorM9(dcnt,lastHVPin, "terM9_general error the number drive UDBs is greater than what is available."); 
				for( i=0;i<dcnt;i++)
					{
					temp=(INTDatVal(datum,i)-5)/10;
					if((temp>0)||(INTDatVal(datum,i)==5))
						{
							if ((0>temp)||(temp>lastHVPin)) ErrorM9(temp, lastHVPin, "Request UDB channel is not within Pin Group. ");
	//						m_hvPinsStim[idx]=temp;
							m_hvPinsStim[m_hvStimCount].hvchnnl = temp;
							m_hvPinsStim[m_hvStimCount].hvdrive_detectNot = 1;  // Drive mode but off because we don't as yet know what it's state is.
							m_hvPinsStim[m_hvStimCount].hvState = TERM9_HVPINMODE_OFF;  //  or TERM9_HVPINMODE_ON
							++m_hvStimCount;
						}
					}
				FreeDatum(datum);
				}
			}
		else 
			if(Fnc !=FNC_MEAS_STATIC_LOGIC_DATA_SCAN) return; // not setup for udb stimulus or response.
			if(datum=RetrieveDatum(M_RESP,K_FDD))		
				{
				dcnt=DatCnt(datum);
				dsiz=DatSiz(datum);
				dtyp=DatTyp(datum);
				m_hvRespCount=0;
				for(i=0;i<dcnt;i++)
					{
					temp=(INTDatVal(datum,i)-5)/10;  // here is a quandry InDatVal returna all the bits of the field, valid or otherwise.
					if((temp>0)||(INTDatVal(datum,i)==5))  // also note that channel 0 is 5, so the computation bogs. 
						{
							if (temp>lastHVPin) ErrorM9(temp, lastHVPin, "Request UDB channel is not within Pin Group. ");						
							m_hvPinsResp[m_hvRespCount].hvchnnl = temp;
							m_hvPinsResp[m_hvRespCount].hvdrive_detectNot = 0;  // set for detect mode. and turn off drives.
							m_hvPinsResp[m_hvRespCount].hvState = TERM9_HVPINMODE_OFF;  //  or TERM9_HVPINMODE_ON
							++m_hvRespCount;
						}
					}  
				FreeDatum(datum);
				}
	}
	//===================================================================================================
	void CDwg_T::fetchHV(int Fnc)  //applies to the 32 CRB pins
	{
		
		ViInt32 HVPinDetectState;
		DATUM *ddat;
		int dcnt=0,dsiz=0,dtyp=0;
		CEMDEBUG(10,cs_FmtMsg("Entering Fetch HV"));
		char *pelm,tot;
		ViInt32 tempBuffer[32];
		int getHV=0;
		int word=0;
		int bitCnt=0;
		int byteCnt=0;
		int Status =0;
		ddat = FthDat();
		dcnt = DatCnt(ddat);
		dsiz = DatSiz(ddat);
		dtyp = DatTyp(ddat);//DIGITAL VALUE FOR M_VALU
		for (getHV=0;getHV<=31;getHV++) // start with a clean bit buffer.
		tempBuffer[getHV] = 0;

//10/25/06 for debug of HV detects  
//		ViInt32 stateCount = 0;
//		ViUInt32 HVDetectState[32];
//		IFNSIM(Status = terM9_fetchHighVoltagePinGroupState(m_Handle,32,&stateCount,HVDetectState));

		for( getHV=0;getHV<m_hvRespCount;getHV++) 
		{
		  if (m_hvPinsResp[m_hvRespCount-getHV-1].hvdrive_detectNot == 0x01)
		  {	  
			ErrorM9(m_Handle, getHV, "Detect pin is set for drive");
			IFNSIM(Status = terM9_getHighVoltagePinMode(m_Handle,m_hvPinsResp[m_hvRespCount-getHV-1].hvchnnl,&m_hvPinsResp[m_hvRespCount-getHV-1].hvdrive_detectNot));
		  }
 		 IFNSIM(Status = terM9_fetchHighVoltagePinState (m_Handle,m_hvPinsResp[m_hvRespCount-getHV-1].hvchnnl, &HVPinDetectState));
		  if (Status) ErrorM9(m_Handle, Status, "terM9_fetchHVDetectPinState");
			if(HVPinDetectState==TERM9_STATE_HIGH)
				{
				CEMDEBUG(10,cs_FmtMsg("HV Pin - %d : m9_OFF  vipert_ON",m_hvPinsResp[m_hvRespCount-getHV-1]));//remember OFF is when the HV Pin is pulled to val. > thresh
				tempBuffer[getHV]=1;  //10/26/06 was tempBuffer[getHV]=0;
				}
			else // if(HVPinDetectState==TERM9_STATE_LOW)  return value cannot be tristate. 
				{
				CEMDEBUG(10,cs_FmtMsg("HV Pin - %d : m9_ON  vipert_OFF ",m_hvPinsResp[m_hvRespCount-getHV-1]));//remember ON is when the HV Pin is pulled to ground
				tempBuffer[getHV]=0;  // 10/26/06 was tempBuffer[getHV]=1;
				}
			}
		for( word=0;word<dcnt;word++)//number of words requested from ATLAS
		{
				pelm=DIGDatVal(ddat,word);
				for(int byteCnt=0;byteCnt<dsiz;byteCnt++)
					{
						tot=0;
						for( bitCnt=7;bitCnt>=0;bitCnt--)
							{
							 if(tempBuffer[(word+byteCnt)*8+(bitCnt)]==1)  tot=tot+(int)pow(2,bitCnt);
							}
						DIGDatByte(pelm,(dsiz-(byteCnt+1)))=tot;	
					}
		}
		FthCnt(dcnt*dsiz);
	
	}
//=======================================================================================================
void CDwg_T::setupEvents(int Fnc)
{
	int handle;    // This is the event handle
	int refh;      // This is a handle that the event refers to for EBE
	double time;   // This is the variable that the event refers to (either period or delay)
//	int type;      // Type of event: 5:EBE, 6:TBE, 7:SBEBUILD_WARN
	int leveltype; // 2:two-level, 3:three-level
//	DATUM datum;BUILD_WARN
	//if (dwgdebugflag) 
	//{
	//	sprintf(msg_buf,"\033[32;40m dwg debug: Entering DGCDGCEventSetup(%d)\033[m\n",funcnum);
	//	Display(msg_buf);
	//}
	

	// First retrieve the information from the Atlas
	//type = GetCurChnNum()/10;
	switch(Fnc)
	{
		case FNC_EVNT_EBE_CALL_TBE_SBE_2:
		case FNC_EVNT_EBE_CALL_EBE:
		case FNC_EVNT_EBE_CALL_TBE_SBE_3:
			RetrieveData(M_EVOU,K_SET,&handle,0);  // Handle for the event
			RetrieveData(M_EVDL,K_SET,&refh,0);    // Handle for the reference
			RetrieveData(M_VEDL,K_SET,&time,0);    // Time between the events
			//if (dwgdebugflag)
			//{
			//	sprintf(msg_buf,"(debug) -EBE- Handle:%d, Time-interval=%f(us), Reference Handle:%d\n",handle,time*1e6,refh);
			//	Display(msg_buf);
			//}
			Events_Update(handle, time, refh);
			break;
		case FNC_EVNT_TBE_2:
		case FNC_EVNT_TBE_3:
			leveltype = -1*(GetCurChnNum()%10);    // Level type =-2 for 1EVBE+TBE; =-3 for 2EBE+TBE
			RetrieveData(M_EVOU,K_SET,&handle,0);  // Handle for the event
			RetrieveData(M_TIME,K_SET,&time,0);    // Time between the events
			//if (dwgdebugflag)
			//{
			//	sprintf(msg_buf,"(debug) -TBE- Handle:%d, Time-interval=%f(us), LevelType:%d\n",handle,time*1e6,leveltype);
			//	Display(msg_buf);
			//}
			Events_Update(handle, time, leveltype);
			break;
		case FNC_EVNT_SBE_2:
		case FNC_EVNT_SBE_3:
			leveltype = -1*(GetCurChnNum()%10);    // Level type =-2 for 1EVBE+SBE; =-3 for 2EBE+SBE
			RetrieveData(M_EVOU,K_SET,&handle,0);  // Handle for the event
			//if (dwgdebugflag)
			//{
			//	sprintf(msg_buf,"(debug) -SBE- Handle:%d, LevelType:%d\n",handle,leveltype);
			//	Display(msg_buf);
			//}
			Events_Update(handle, -2, leveltype);  // Time =-2 indicates a SBE
			break;
	}
	return;

}
////////////////////////////////////////////////////////////////////////////
// Function: ErrorDwg (type of fetch in play, number of words to send and the word size)
//
// Purpose: Reads the data from the m_RespValue and m_respBuff.
//		    Then creates a list of compared values, the errors, the error index and the number of faults.
//            m_RespByteData
//            m_ErrByteData
//            m_FaultCnt   Where m_faultCnt is returned as the status of ErrorDwg. A negative number is an Errordwg failure.
//          for subsequent fetches as appropriate
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
int CDwg_T::ErrorDwg(int fetchMod,int dcnt,int dsiz)
{ 
  int rspnswrd,rspnswdidx,tempindex;
 
  // Prove errors check stuff here. arrays that can be returned are:
  // the save-compare array, the error array, the error index array. 
  // and a faultcount.

   m_errorIdx=(int*)malloc(dcnt*sizeof(int));
//  m_errorIdx=(int*)calloc(dcnt,sizeof(int));  //03/19/07-
 // int m_erroridxsize = dcnt * sizeof(int);   //03/19/07-
   //    CEMDEBUG(5,cs_FmtMsg("errorindex address = %x, extent %x",m_errorIdx,m_erroridxsize));  //03/19/07-

  m_errorwordsArrayflag = true;
  m_errorIdxflag = true;
  m_errorIndex = 0;
   m_FaultCnt = 0;
   if (m_BitCount != (dsiz*8)) // 12/21/06 check the response bit size verus the requested number of bits needed to check.  
   {
	dsiz = m_BitCount/8; // if the number of bits to the requested size don't match align to the response bits. 
	 if( m_BitCount%8 != 0) 
	   ++dsiz;   
   }
//  m_errorwordsArray=(int*)malloc(dcnt*dsiz*sizeof(int)); 
  m_errorwordsArray=(int*)calloc(dcnt*dsiz,sizeof(int)); 
 for (rspnswrd = 0; rspnswrd <dcnt*dsiz;rspnswrd++)
  m_errorwordsArray[rspnswrd] = 0;  // clear the the error word collection.
   m_referenceIndex = m_errorIndex;
  for(rspnswrd=0;rspnswrd<dcnt;rspnswrd++) // check each word by bytes.
  {
   if (m_CompareWordSize < m_referenceWordSize) m_referenceIndex = m_referenceIndex+(m_referenceWordSize-m_CompareWordSize);
   tempindex = 0x4E414441;  // max errors is 0x5fff40 = TOTAL_PINS(192) * max response size(32767)
   for(rspnswdidx=0;rspnswdidx<dsiz;rspnswdidx++)  //check each byte within the word.
   { 
	 CEMDEBUG(10,cs_FmtMsg("Data Compare byte %x,compare index %x, Reference Byte %x  referenceindex %x, responsewordindex %x, loadedrefcount %x, loadedrefsize %x",
		 m_comparewordsArray[m_errorIndex],m_errorIndex,m_referenceByteArray[m_referenceIndex],m_referenceIndex,rspnswdidx,m_referenceWordCount,m_referenceWordSize));//03/15/07a-

	 m_comparewordsArray[m_errorIndex] = m_comparewordsArray[m_errorIndex]^m_referenceByteArray[m_referenceIndex];
	 if ((m_comparewordsArray[m_errorIndex]!=0)&&(m_maskByteArray == NULL)) // update fault count if no mask. 
//	 if ((m_comparewordsArray[m_errorIndex]!=0)&&(!m_maskByteArrayflag)) // update fault count if no mask. 
	 {
	  m_errorwordsArray[m_errorIndex] = m_comparewordsArray[m_errorIndex];   //m_errorwordsArray[rspnswdidx+(dsiz*m_FaultCnt)]
	  tempindex = rspnswrd;
	 }
	 if(m_maskByteArray != NULL)   //if (m_maskByteArrayflag)	// update error and faultcount using mask   //03/15/07a-
	 {
	  m_comparewordsArray[m_errorIndex] = m_comparewordsArray[m_errorIndex] & m_maskByteArray[m_errorIndex];  //03/15/07a-
	  m_errorwordsArray[m_errorIndex] = m_comparewordsArray[m_errorIndex];// & m_maskByteArray[m_errorIndex];
	  if (m_errorwordsArray[m_errorIndex] !=0) tempindex = rspnswrd; //m_errorwordsArray[rspnswdidx+(dsiz*m_FaultCnt)]
	  else tempindex = 0x4E414441;
	 }
	 ++m_errorIndex; ++m_referenceIndex;
   }
	 if (tempindex != 0x4E414441) // check when to update the fault counter
	 { 
	  m_errorIdx[m_FaultCnt] = rspnswrd+1; //m_errorIndex;
	  ++m_FaultCnt;
	 }
   }
 return m_FaultCnt;
}
////////////////////////////////////////////////////////////////////////////
// Function: RetrieveDwg(int,DATUM*,int)
//
// Purpose: Sends to RTS the response words as queued by M_VALU or a Meas or Sense
// If it is a prove, and the values are not to be sent. the routine still parses the data for ErrorDwg. 
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
void CDwg_T::retrieveDwg(int fetchMod,	DATUM* ddat, int Fnc)
{
	char* pelm;
	int i;
	int retData[TOTAL_PINS/32]; // was int retData; 9/27/06
	int tempByte;
	int j = 0;
	int maskCount=0,maskSize=0;
	int errorWords=0;
	int index=0;
	int k=0,lword=0;
	int rtwrd=0,rtbits=0;   // 09/28/06 - additional bit, pin information.
	int byte=0,abyte;
	int m_wordsize;
	int m_totalBitCount;
	int m_numberofwords;
	int bitdiff;
    int compareIdx=0; 
	int returnbytesperword = 1;
	static bool donotreturnm_valu = false;  //03/15/07-

//	DATUM* ddat = FthDat();

	int dcnt = DatCnt(ddat);
	int dsiz = DatSiz(ddat);
	int dtyp = DatTyp(ddat); 
	abyte = dsiz;
	 CEMDEBUG(10,cs_FmtMsg("into retrieveDwg, format bits to words and send packing, address %x, wordcount %x, dsize %x, dtype %x returnedbits %x proveflag %x",
		 ddat,dcnt,dsiz,dtyp,m_BitCount,m_referenceByteArrayflag)); // 1/31/07 - uncomment for debug of runtime exception

	donotreturnm_valu = false;  //03/15/07-

	if(m_referenceByteArrayflag)
	{ 
	 abyte = m_BitCount/8 +4;
//	 m_comparewordsArray=(int*)malloc(dcnt*dsiz*sizeof(int));
	 m_comparewordsArray=(int*)calloc(dcnt*abyte,sizeof(int));  //03/14/07-
   	 m_referenceByteArrayflag = true;
	 m_comparewordsArrayflag = true;
     compareIdx = 0; 
	 // build a decision tree here for M_VALU and dcnt*dsiz*8 != return bits,, ie PROVE (VALUE) nothing to return;
	 if ((dcnt == dsiz)&&(dsiz==1)&&(Fnc==FNC_PROV_STATIC_LOGIC_DATA)&&(fetchMod==M_VALU)&&(dtyp==DIGV)&&(m_wasDynamic == false)&&(dsiz*8!=m_BitCount))
	 donotreturnm_valu = true;
	}

	 m_wordsize = dsiz *8;
	 m_totalBitCount = m_BitCount; // place holder for total response bits retrieved.
	 m_numberofwords = m_totalBitCount/m_wordsize;
	 if (m_wasDynamic == true) // If it is a dynamic response. Need to refine the word field and depth. 
	 { 
		if (m_wordsize != m_RespChnlCnt) 
		{
			bitdiff = abs(m_wordsize - m_RespChnlCnt); 
			m_wordsize = m_RespChnlCnt;
		}
	   if (m_wordsize > TOTAL_PINS) CEMDEBUG(10,cs_FmtMsg( " response wordsize is greater than the total channels. %d",m_wordsize));
//	   m_numberofwords = m_respBuff[m_fetchCounter].size.wordCount;
	   m_numberofwords = m_totalBitCount/m_wordsize;
	   if (m_numberofwords!= dcnt)  CEMDEBUG(10,cs_FmtMsg( "the number of words requested doesn't agree with what is available %d  %d",m_numberofwords,dcnt));
   	   if (m_wordsize!= (m_BitCount/dcnt))  CEMDEBUG(10,cs_FmtMsg( "Response word sizes disagree"));
	   m_BitCount = m_wordsize;   // set the dynamic bit count to the wordsize.
	 }
		 k = 0;  // response bit index. If it is static only one pattern is being retrieved.
	 for (j = 0; j < dcnt; j++) // send results by each pattern. 
        {
	    if (m_wasDynamic == true) m_BitCount = m_wordsize;   // set the Dynamic bit count to the wordsize. Leave alone if Static. remove this line after debug
		for (i=0;i <=m_BitCount/32; i++) //  flush a set of temp registers. Includes a partial word register 
		  retData[i] = 0;
		 rtbits = m_BitCount%32-1;  // determine any partial bits 
		 rtwrd = m_BitCount/32;    
		 if (m_BitCount%32 == 0) --rtwrd;	//store data in a temp integer, where integer has been defined as a 32 bit word.
		 for (rtwrd;rtwrd>=0;rtwrd--)  // start from the top shift in each bit into 32 bit ints.
		  {
		 	if (rtbits<0) rtbits = 31;
			for (rtbits; rtbits>=0;rtbits--,k++)
				retData[rtwrd] += m_pRespValue[k] << rtbits;
		  }
	if(dtyp==DIGV)
	    {
		 pelm = DIGDatVal(ddat,j);
		 CEMDEBUG(12,cs_FmtMsg("Data Type Decimal Digits, address %x, word %d, returnaddr %x compare flag %x",ddat,j,pelm,m_referenceByteArrayflag));
		 if (dcnt == 0)   // If there aren’t any requested digits return zero. 
		 {
		  for( lword=0; lword<dsiz;lword++)
		  DIGDatByte(pelm,lword)=0;
		 }	
		 if (dcnt!=0) //Check to skip the following. 
		 {
			i = m_BitCount%32;
			rtwrd = m_BitCount/32;  // first find the number of valid bytes to return. rtwrd contains 4 byte words. 
			if (i==0)				// i is the number of remaining bits. 
			{ --rtwrd;
			 i = 31;
			}
			i = ((i-1)/8)+1;        // that turn out to be at least one byte. or one 32 bit word. 
			returnbytesperword = i + (rtwrd *4); // find the total bytes that can be returned. 
			lword = 0;
			if ( dsiz>returnbytesperword) // if paws is asking for more bytes than are valid, pad the field
			{ for (int padcount=0;padcount<(dsiz-returnbytesperword);padcount++,lword++)
			  {
			   if(!donotreturnm_valu) DIGDatByte(pelm,lword)=00;
			   CEMDEBUG(10,cs_FmtMsg("Data: Response Data Type Decimal = %x  word = %d   byte = %d daddress = %x  rtnaddr = %x ",0,j,i,ddat,pelm)); //1/31/07- uncomment for debug of runtime exception
  			   if (m_referenceByteArrayflag)    //03/14/07- added decision to check for a valid compare array.
   			    m_comparewordsArray[lword]=00;
	//		    m_comparewordsArray[compareIdx]=00; //03/15/07a-
   //			   	++compareIdx;
			  }
			}
			for (;lword<dsiz;)  // then finish with the valid bytes. ie always right justified. 
			{
			 for ( i;i>0;i--)
			  {
			   tempByte = (retData[rtwrd]>>(i*8-8)& 0x00FF);
			   if(fetchMod == M_VALU)
			   CEMDEBUG(10,cs_FmtMsg("Data: Response Data Type Decimal = %x  word = %d   byte = %d daddress = %x  rtnaddr = %x ",tempByte,j,i,ddat,pelm)); //1/31/07- uncomment for debug of runtime exception
			   if(!donotreturnm_valu) DIGDatByte(pelm,lword)=tempByte;
			   if(m_referenceByteArrayflag)  //03/14/07-
			    {
				 m_comparewordsArray[compareIdx]=tempByte;  // (l+(l*j))
				 ++compareIdx;
			    }
			   ++lword; 
			  }
		     if (i<=0)i=4;
			 if (rtwrd > 0) --rtwrd;
			}
		  }
		 m_CompareWordSize = returnbytesperword; // 1/03/07 - establish a compare word size for prove error checks
		}
	else if(dtyp==INTV)
        {
		 CEMDEBUG(10,cs_FmtMsg("Data: Data Type Int"));
		 if(dcnt!=0)
		 {	
		  if(fetchMod == M_VALU)
			 INTDatVal(ddat, j) = m_pRespValue[j];
			   if(m_referenceByteArrayflag)  //03/14/07-
			 m_comparewordsArray[j]=m_pRespValue[j]; 
		 }
		 else
		 {	
		  INTDatVal(ddat, j)=0;
			 //   if(m_referenceByteArray !=NULL) //03/14/07-
			   if(m_referenceByteArrayflag)
		   m_comparewordsArray[j]=0;
		 }
		}
	else
		CEMDEBUG(10,cs_FmtMsg("Data: Data Type Unknown"));
	  }
 return;
}
////////////////////////////////////////////////////////////////////////////
// Function: checklevelSets(int)
//
// Purpose: Prior to a run, static or dynamic verify that both levelindexs per card are established. 
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
int CDwg_T::checklevelSets(void)
{
	double vih[2];
	double vil[2];
	double voh[2];
	double vol[2];

	int card,lvlndx,Status;

	for ( card = 1; card <= TOTAL_CARDS; card++)  //4/30/07- checks each levelset by card. Levelsets are programmed at the card level not at channel level. 
	{
	 for ( lvlndx = 0; lvlndx <=1;lvlndx++)
	 {
	  IFNSIM(Status = terM9_getLevelSetVIH(m_Handle,TERM9_SCOPE_CARD(card),lvlndx,&vih[lvlndx]));
	  IFNSIM(Status = terM9_getLevelSetVIL(m_Handle,TERM9_SCOPE_CARD(card),lvlndx,&vil[lvlndx]));
	  IFNSIM(Status = terM9_getLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(card),lvlndx,&voh[lvlndx]));
	  IFNSIM(Status = terM9_getLevelSetVOL(m_Handle,TERM9_SCOPE_CARD(card),lvlndx,&vol[lvlndx]));
      CEMDEBUG(10,cs_FmtMsg("terM9_getLevelSetVs(%d, TERM9_SCOPE_CARD(%d), index=%d, \n VIH=%4.3f, VIL=%4.3f,\n VOH=%4.3f, VOL=%4.3f)",
		  m_Handle,card,lvlndx,vih[lvlndx],vil[lvlndx],voh[lvlndx],vol[lvlndx]));
      if (Status)
	  {
	   ErrorM9(m_Handle, Status, "terM9_getLevelSetVIH,VIL,VOH,VOL for a level");
	   return (Status);
	  }
	 }
	 // test VIH and VOH, assume that if whether one is set to other than 0.00 then the low side has been set as well
	 //		ignore the load settings for now. Stuff in later if needed and check for load off first.  
	 if(((vih[0]< -2.0)||(vih[0]>5.2))&&((-2.1<=vih[1])&&(vih[1]<5.2)))  // Level 0 VIs are not programmed. 
	 {
	  IFNSIM(Status = terM9_setLevelSetVIH(m_Handle,TERM9_SCOPE_CARD(card),0,vih[1]));
	  IFNSIM(Status = terM9_setLevelSetVIL(m_Handle,TERM9_SCOPE_CARD(card),0,vil[1]));
	  IFNSIM(Status = terM9_getLevelSetVIH(m_Handle,TERM9_SCOPE_CARD(card),0,&vih[0]));
	  IFNSIM(Status = terM9_getLevelSetVIL(m_Handle,TERM9_SCOPE_CARD(card),0,&vil[0]));
      CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVIH(%d, TERM9_SCOPE_CARD(%d), %d, %f  %f)",m_Handle,card,0,vih[0],vil[0]));
      if (Status)
	  {
	   ErrorM9(m_Handle, Status, "terM9_setLevelSetVIH,VIL for level 0");
	  return (Status);
	  }
	 }
 	 if(((vih[1]<-2.0)||(vih[1]>5.2))&&((-2.1<vih[0])&&(vih[0]<5.2)))  // Level 1 VIs are not programmed.
	 {
	  IFNSIM(Status = terM9_setLevelSetVIH(m_Handle,TERM9_SCOPE_CARD(card),1,vih[0]));
	  IFNSIM(Status = terM9_setLevelSetVIL(m_Handle,TERM9_SCOPE_CARD(card),1,vil[0]));
  	  IFNSIM(Status = terM9_getLevelSetVIH(m_Handle,TERM9_SCOPE_CARD(card),1,&vih[1]));
	  IFNSIM(Status = terM9_getLevelSetVIL(m_Handle,TERM9_SCOPE_CARD(card),1,&vil[1]));
      CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVIH,VIL (%d, TERM9_SCOPE_CARD(%d), %d, %f  %f)",m_Handle,card,1,vih[1],vil[1]));
      if (Status)
	  {
	   ErrorM9(m_Handle, Status, "terM9_setLevelSetVIH,VIL for level 1");
	  return (Status);
	  }
	 }
	 if(((voh[0]<-2.0)||(voh[0]>5.2)||(voh[0]<=0.040)&&(voh[0]>=-0.040))&&((-2.1<voh[1])&&(voh[1]<5.2))) // Level 0 VIs not programmed. 
	 {
	  IFNSIM(Status = terM9_setLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(card),0,voh[1]));
	  IFNSIM(Status = terM9_setLevelSetVOL(m_Handle,TERM9_SCOPE_CARD(card),0,vol[1]));
  	  IFNSIM(Status = terM9_getLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(card),0,&voh[0]));
	  IFNSIM(Status = terM9_getLevelSetVOL(m_Handle,TERM9_SCOPE_CARD(card),0,&vol[0]));
      CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVOH(%d, TERM9_SCOPE_CARD(%d), %d, %f  %f)",m_Handle,card,0,voh[0],vol[0]));
      if (Status)
	  {
	   ErrorM9(m_Handle, Status, "terM9_setLevelSetVOH,VOL for level 0");
	   return (Status);
	  }
	 }
	 if(((voh[1]<-2.0)||(voh[1]>5.2)||(voh[1]<=0.040)&&(voh[1]>=-0.040))&&((-2.1<voh[0])&&(voh[0]<5.2))) // Level 1 VIs not programmed. 
	 {
	  IFNSIM(Status = terM9_setLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(card),1,voh[0]));
	  IFNSIM(Status = terM9_setLevelSetVOL(m_Handle,TERM9_SCOPE_CARD(card),1,vol[0]));
	  IFNSIM(Status = terM9_getLevelSetVOH(m_Handle,TERM9_SCOPE_CARD(card),1,&voh[1]));
	  IFNSIM(Status = terM9_getLevelSetVOL(m_Handle,TERM9_SCOPE_CARD(card),1,&vol[1]));
      CEMDEBUG(10,cs_FmtMsg("terM9_setLevelSetVOH,VOL (%d, TERM9_SCOPE_CARD(%d), %d, %f  %f)",m_Handle,card,1,voh[1],vol[1]));
      if (Status)
	  {
	   ErrorM9(m_Handle, Status, "terM9_setLevelSetVOH,VOL for level 1");
	   return (Status);
	  }
	 }
	}
	return 0;
}
////////////////////////////////////////////////////////////////////////////
// Function: getcnx(void)
//
// Purpose: Get the address and the channels of interest if they weren't passed from the FNC (setup, or initiate) call.
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ========================================
//
////////////////////////////////////////////////////////////////////////////
int CDwg_T::getcnx(int Fnc)
{
	if (goforcnxflag == false) return 0; // already establshed, bag it. 
// Need to establish the channels thru a path connection 
	// retrieveconnection field pointers. //    cs_DoSwitching(M_PATH);
	//DATUM * pdatumcnx;

	// test for CONNECT data
	//pdatumcnx = RetrieveDatum(M_PATH, K_CON); // 12/15/06 moved to do this in ParseSetup

// then setup the channels
	switch (Fnc1st)
	{
		case FNC_APLY_STATIC_LOGIC_DATA_DS:
		case FNC_STIM_STATIC_LOGIC_DATA:
			SetupStim(Fnc1st);
		break;
		case FNC_MEAS_STATIC_LOGIC_DATA_DS:
		case FNC_PROV_STATIC_LOGIC_DATA:
		case FNC_SENS_STATIC_LOGIC_DATA:
			SetupResp(Fnc1st);
		break;
	
		default:
			return 0;
		break;
	}
	goforcnxflag = false;
	return 0;
}
//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

/************************************************************/
/*	Events													*/
/************************************************************/

typedef struct tag_DwgEvents
{
	int nSize;
	int nCount;
	DwgEvent* pData;
} DwgEvents;

static DwgEvents g_events = { 0, 0, NULL };

// ordered array; perform binary search
static int Events__Find(int hEvent, int* at)
{
	int lower = 0;
	int upper;
	int mid;

	upper = g_events.nCount - 1;
	while (upper >= lower)
	{
		mid = (lower + upper) / 2;
		if (g_events.pData[mid].hEvent == hEvent)
		{
			*at = mid;
			return 1;
		}

		if (hEvent < g_events.pData[mid].hEvent)
			upper = mid - 1;
		else
			lower = mid + 1;
	}
	*at = lower;
	return 0;
}

HEV  Events_Read(int hEvent)
{
	int pos;
	DwgEvent* pE = NULL;
	if (Events__Find(hEvent, &pos))
		pE = g_events.pData + pos;
	return pE;
}

void Events_Update(int hEvent, double dTimeVal, int hRef)
{
	DwgEvent* pE;
	int pos;

	if (Events__Find(hEvent, &pos))
	{
		pE = g_events.pData + pos;
		pE->hEvent = hEvent;
		pE->dValue = dTimeVal;
		pE->hRef = hRef;
		return;
	}

	if (g_events.nCount == g_events.nSize)
	{
		void* pData = NULL;
		int len;
		len = sizeof(DwgEvent);

		g_events.nSize += EVENTS_DELTA;
		pData = malloc(g_events.nSize * len);
		memcpy(pData, (void*)g_events.pData, pos * len);
		pE = (DwgEvent*)pData + pos;
		memset(pE, -1, len);
		memcpy((void*)(pE + 1),  (void*)(g_events.pData + pos), (g_events.nCount - pos) * len);
	
		if (g_events.pData != NULL)
			free(g_events.pData);
		g_events.pData = (DwgEvent*)pData;
	}
	else
	{
		int i;
		for (i = g_events.nCount - 1; i >= pos; i--)
			memcpy((void*)(g_events.pData + i + 1), (void*)(g_events.pData + i), sizeof(DwgEvent));
	}
	g_events.nCount++;
	pE = g_events.pData + pos;
	pE->hEvent = hEvent;
	pE->dValue = dTimeVal;
	pE->hRef = hRef;
}

void Events_Delete(int hEvent)
{
	int pos;
	if (Events__Find(hEvent, &pos))
	{
		int i;

		for (i = pos + 1; i < g_events.nCount; i++)
			memcpy((void*)(g_events.pData + i - 1), (void*)(g_events.pData + i), sizeof(DwgEvent));
		g_events.nCount--;
	}
}

void Events_Clear()
{
	if (g_events.pData != NULL)
		free(g_events.pData);
	g_events.nCount = 0;
	g_events.nSize  = 0;
	g_events.pData  = NULL;
}

void Events_Dump()
{
#ifdef DWG_DEBUG

	int i;
	DwgEvent* pE;

	printf("     #    ID       VALUE   REF\n");
	printf("  ----------------------------\n");
	for (i = 0; i < g_events.nCount; i++)
	{
		pE = g_events.pData + i;
		printf("  %4d  %4d  %10.8f  %4d\n", i, pE->hEvent, pE->dValue, pE->hRef);
	}	
	printf("  ----------------------------\n\n");

#endif
}




