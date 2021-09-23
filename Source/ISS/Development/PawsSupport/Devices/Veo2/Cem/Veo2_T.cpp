////////////////////////////////////////////////////////////////////////////////
// File:	    Veo2_T.cpp
//
// Date:	    11-OCT-05
//
// Purpose:	    Instrument Driver for Veo2
//
// Instrument:	Veo2  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//          Library/DLL				Purpose
//     =====================  ==================================================
//     cem.lib                \usr\tyx\lib
//     cemsupport.lib         ..\..\Common\Lib
//
// ATLAS Subset: PAWS-85
//
//
//                          Function Number Map
//
// FNC                           Signal Description
// ---  ------------------------------------------------------------------------
//   1   sensor (los-align-error) infrared
//   2   sensor (modulation-transfer-function) infrared
//   3   sensor (distortion) infrared
//   4   sensor (noise-eq-diff-temp) infrared
//   5   sensor (uniformity) infrared
//   6   sensor (chan-integrity) infrared
//   7   sensor (diff-boresight-angle) infrared
//   8   sensor (min-resolv-temp-diff) infrared
//   9   sensor (differential-temp) infrared
//  10   sensor (ambient-temp) infrared
//  11   sensor (blackbody-temp) infrared
//  12   sensor (pulse-energy) laser scope channel 1
//  13   sensor (pulse-energy) laser scope channel 2
//  14   sensor (pulse-energy-stab) laser channel 1
//  15   sensor (pulse-energy-stab) laser channel 2
//  16   sensor (power-p) laser channel 1
//  17   sensor (power-p) laser channel 2
//  18   sensor (pulse-ampl-stab) laser channel 1
//  19   sensor (pulse-ampl-stab) laser channel 2
//  20   sensor (pulse-width) laser channel 1
//  21   sensor (pulse-width) laser channel 2
//  22   sensor (prf) laser
//  23   sensor (pulse-period-stab) laser
//  24   sensor (diff-boresight-angle) laser
//  25   sensor (divergence) laser
//  26   sensor (boresight-angle) laser
//  28   sensor (receiver-sensitivity) laser
//  29   sensor (range-error) laser
//  30   sensor (los-align-error) light
//  31   sensor (modulation-transfer-function) light
//  32   sensor (distortion) light
//  33   sensor (uniformity) light
//  34   sensor (camera-gain) light
//  35   sensor (dynamic-range) light
//  36   sensor (gray-scale-resolution) light
//  37   sensor (diff-boresight-angle) light
//  38   sensor (min-resolv-contrast) light
//  40   source infrared
//  41   source multi-sensor-infrared
//  42   source laser target return
//  43   source laser
//  44   source light
//  45   source multi-sensor-light
//  46   source larrs
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ===========================================================
// 1.0.0.0  30JAN08  Baseline              
// 1.1.0.0  04NOV08  Phase II Release      
// 1.1.0.2  29JUL10  Normal wavelength to Meters for all cases.
// 1.1.0.3  03AUG10  Increased buffer size in fetch.
// 1.1.0.4  28SEP10  Updated source laser for STR 9677
// 1.1.0.5  01OCT10  Changed wavelength to nm, AtxmlWrapper function normalize 
//                   does not scale for um.
// 1.1.0.6  24JAN11  Added new targets per DME / SBIR update.
////////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Veo2_T.h"

// Local Defines
#define RAD(x) ((x)/ToDeg)
#define DEG(x) ((x)*ToDeg)

//Signal Types
const int INFRARED = 1;
const int LASER    = 2;
const int LIGHT    = 3;
const int LARRS    = 4;
const int SENSOR   = 1;
const int SOURCE   = 2;

// Type MD modifier values & strings
const int DIM_HORIZ =  1;
const int DIM_VERT  =  2;
const int DIM_CH1   =  3;
const int DIM_CH2   =  4;
const int DIM_INT   =  5;
const int DIM_EXT   =  6;
const int DIM_AUTO  =  7;
const int DIM_LASR  =  8;
const int DIM_POS   =  9;
const int DIM_NEG   = 10;
const int DIM_GT	= 11;
const int DIM_LT	= 12;

const int DIM_RS170          =  1;
const int DIM_RS343_675_1_1  =  2;
const int DIM_RS343_675_4_3  =  3;
const int DIM_RS343_729_1_1  =  4;
const int DIM_RS343_729_4_3  =  5;
const int DIM_RS343_875_1_1  =  6;
const int DIM_RS343_875_4_3  =  7;
const int DIM_RS343_945_1_1  =  8;
const int DIM_RS343_945_4_3  =  9;
const int DIM_RS343_1023_1_1 = 10;
const int DIM_RS343_1023_4_3 = 11;
const int DIM_INTERNAL_1     = 12;
const int DIM_INTERNAL_2     = 13;

// Integer is not target wheel position, index into array for string.
const int DIM_TARG_OPNAPR = 1;
const int DIM_TARG_BRSGHT = 2;
const int DIM_TARG_PIESECTOR = 3;
const int DIM_TARG_4BAR5 = 4;
const int DIM_TARG_4BAR383 = 5;
const int DIM_TARG_4BAR267 = 6;
const int DIM_TARG_4BAR15  = 7;
const int DIM_TARG_4BAR33  = 8;
const int DIM_TARG_DIAGLN  = 9;
const int DIM_TARG_ETCHED  = 10;
const int DIM_TARG_SPNPINHL = 11;
const int DIM_TARG_TGTGRP07 = 12;
const int DIM_TARG_TGTGRP1  = 13;
const int DIM_TARG_TGTGRP2  = 14;
const int DIM_TARG_GRYSCL   = 15;
const int DIM_TARG_IRBS00   = 16;
const int DIM_TARG_IRBS01   = 17;
const int DIM_TARG_IRBS02   = 18;
const int DIM_TARG_IRBS03   = 19;
const int DIM_TARG_IRBS04   = 20;
const int DIM_TARG_IRBS05   = 21;
const int DIM_TARG_IRBS06   = 22;
const int DIM_TARG_IRBS07   = 23;
const int DIM_TARG_IRBS08   = 24;
const int DIM_TARG_IRBS09   = 25;
const int DIM_TARG_IRBS10   = 26;
const int DIM_TARG_TVBS01   = 27;
const int DIM_TARG_TVBS02   = 28;
const int DIM_TARG_TVBS03   = 29;
const int DIM_TARG_TVBS04   = 30;
const int DIM_TARG_TVBS05   = 31;
const int DIM_TARG_TVBS06   = 32;
const int DIM_TARG_TVBS07   = 33;
const int DIM_TARG_TVBS08   = 34;
const int DIM_TARG_TVBS09   = 35;
const int DIM_TARG_TVBS10   = 36;
const int DIM_TARG_TVBS11   = 37;
const int DIM_TARG_TVBS12   = 38;
const int DIM_TARG_TVBS13   = 39;
const int DIM_TARG_TVBS14   = 40;
const int DIM_TARG_TVBS15   = 41;
const int DIM_TARG_CROSS17  = 42;	// New Target per DME Target Update 20110124
const int DIM_TARG_4BAR10   = 43;	// New Target per DME Target Update 20110124
const int DIM_TARG_4BAR66   = 44;	// New Target per DME Target Update 20110124
const int DIM_TARG_SQUARE21 = 45;	// New Target per DME Target Update 20110124

#define LOS_ALIGN_ERROR_IR					 1
#define MODULATION_TRANSFER_FUNCTION_IR		 2
#define DISTORTION_IR						 3
#define NOISE_EQ_DIFF_TEMP_IR				 4
#define UNIFORMITY_IR						 5
#define CHAN_INTEGRITY_IR					 6
#define DIFF_BORESIGHT_ANGLE_IR				 7
#define MIN_RESOLV_TEMP_DIFF_IR				 8
#define DIFFERENTIAL_TEMP_IR				 9
#define AMBIENT_TEMP_IR						10
#define BLACKBODY_TEMP_IR					11
#define PULSE_ENERGY_CH1_LASER				12
#define PULSE_ENERGY_CH2_LASER				13
#define PULSE_ENERGY_STAB_CH1_LASER			14
#define PULSE_ENERGY_STAB_CH2_LASER			15
#define POWER_P_CH1_LASER					16
#define POWER_P_CH2_LASER					17
#define PULSE_AMPL_STAB_CH1_LASER			18
#define PULSE_AMPL_STAB_CH2_LASER			19
#define PULSE_WIDTH_CH1_LASER				20
#define PULSE_WIDTH_CH2_LASER				21
#define PRF_LASER							22
#define PULSE_PERIOD_STAB_LASER				23
#define DIFF_BORESIGHT_ANGLE_LASER			24
#define DIVERGENCE_LASER					25
#define BORESIGHT_ANGLE_LASER				26
#define RECEIVER_SENSITIVITY_LASER			28
#define RANGE_ERROR_LASER					29
#define LOS_ALIGN_ERROR_VIS					30
#define MODULATION_TRANSFER_FUNCTION_VIS	31
#define DISTORTION_VIS						32
#define UNIFORMITY_VIS						33
#define CAMERA_GAIN_VIS						34
#define DYNAMIC_RANGE_VIS					35
#define GRAY_SCALE_RESOLUTION_VIS			36
#define DIFF_BORESIGHT_ANGLE_VIS			37
#define MIN_RESOLV_CONTRAST_VIS				38
#define SOURCE_INFRARED						40
#define SOURCE_MULTI_SENSOR_INFRARED		41
#define SOURCE_LASER_TARGET_RETURN			42
#define SOURCE_LASER						43
#define SOURCE_LIGHT						44
#define SOURCE_MULTI_SENSOR_LIGHT			45
#define SOURCE_LARRS						46

const long int CAL_TIME       (86400 * 365); /* one year */
const double VEO2PI = 3.1415926538;
const double ToDeg = 180.0/VEO2PI;

const char DIMS[13][6] = {"",
                          "HORIZ",
						  "VERT",
						  "CH1",
						  "CH2",
						  "INT",
						  "EXT",
                          "AUTO",
						  "LASR",
						  "POS",
						  "NEG",
						  "GT",
						  "LT"};

const char FORMATS[14][15] = {"",
							  "RS170",
							  "RS343-675-1-1",
							  "RS343-675-4-3",
                              "RS343-729-1-1",
							  "RS343-729-4-3",
							  "RS343-875-1-1",
							  "RS343-875-4-3",
							  "RS343-945-1-1",
							  "RS343-945-4-3",
							  "RS343-1023-1-1",
							  "RS343-1023-4-3",
							  "INTERNAL1",
							  "INTERNAL2"};

const char TARGETS[46][15] = {"",
                              "TARG-OPNAPR",
							  "TARG-BRSGHT",
							  "TARG-PIESECTOR",
                              "TARG-4BAR5",
							  "TARG-4BAR383",
							  "TARG-4BAR267",
                              "TARG-4BAR15",
							  "TARG-4BAR33",
							  "TARG-DIAGLN",
                              "TARG-ETCHED",
							  "TARG-SPNPINHL",
							  "TARG-TGTGRP07",
                              "TARG-TGTGRP1",
							  "TARG-TGTGRP2",
							  "TARG-GRYSCL",
                              "TARG-IRBS00",
							  "TARG-IRBS01",
							  "TARG-IRBS02",
                              "TARG-IRBS03",
							  "TARG-IRBS04",
							  "TARG-IRBS05",
                              "TARG-IRBS06",
							  "TARG-IRBS07",
							  "TARG-IRBS08",
                              "TARG-IRBS09",
							  "TARG-IRBS10",
							  "TARG-TVBS01",
                              "TARG-TVBS02",
							  "TARG-TVBS03",
							  "TARG-TVBS04",
                              "TARG-TVBS05",
							  "TARG-TVBS06",
							  "TARG-TVBS07",
                              "TARG-TVBS08",
							  "TARG-TVBS09",
							  "TARG-TVBS10",
                              "TARG-TVBS11",
							  "TARG-TVBS12",
							  "TARG-TVBS13",
                              "TARG-TVBS14",
							  "TARG-TVBS15",
							  "TARG-CROSS17",
							  "TARG-4BAR10",
							  "TARG-4BAR66",
							  "TARG-SQUARE21"};

const char ATTRIBUTES[39][30] = {"",
                                 "losAlignErrorIR", 
								 "modulationTransferFunctionIR",
								 "distortionIR",
								 "noiseEqDiffTempIR",
								 "uniformityIR",
								 "chanIntegrityIR", 
								 "diffBoresightAngleIR", 
								 "minResolvTempDiffIR", 
								 "differentialTempIR", 
								 "ambientTempIR",
								 "blackbodyTempIR",
								 "pulseEnergyCh1Laser",
								 "pulseEnergyCh2Laser",
								 "pulseEnergyStabCh1Laser",
								 "pulseEnergyStabCh2Laser",
								 "powerPCh1Laser",
								 "powerPCh2Laser", 
								 "pulseAmplStabCh1Laser",
								 "pulseAmplStabCh2Laser", 
								 "pulseWidthCh1Laser", 
								 "pulseWidthCh2Laser", 
								 "prfLaser",
								 "pulsePeriodStabLaser", 
								 "diffBoresightAngleLaser", 
								 "divergenceLaser", 
								 "boresightAngleLaser",
								 "", 
								 "receiverSensitivityLaser", 
								 "rangeErrorLaser",
								 "losAlignErrorVis", 
								 "modulationTransferFunctionVis", 
								 "distortionVis",
								 "uniformityVis",
								 "cameraGainVis",
								 "dynamicRangeVis",
								 "grayScaleResolutionVis",
								 "diffBoresightAngleVis",
								 "minResolvContrastDiffVis"};
//
//#define INFRARED	1
//#define LASER		2
//#define LIGHT		3
//#define SENSOR		1
//#define SOURCE		2

// Static Variables
int FIRST = true;

// Local Function Prototypes
void ClearModStruct(ModStruct &);
void ClearDblArrayStruct(DblArrayStruct &);

//++++//////////////////////////////////////////////////////////////////////////
// Exposed Functions
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: CVeo2_T(int Bus, int PrimaryAdr, int SecondaryAdr,
//                      int Dbg, int Sim)
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Bus              int             Bus number
// PrimaryAdr       int             Primary Address (MTA/MLA)
// SecondaryAdr     int             Secondary Address (MSA)
// Dbg              int             Debug flag value
// Sim              int             Simulation flag value (0/1)
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    Class instance.
//
////////////////////////////////////////////////////////////////////////////////
CVeo2_T::CVeo2_T(char *DeviceName, int Bus, int PrimaryAdr, int SecondaryAdr, int Dbg, int Sim)
{
	int Status = 0;

    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;

    if(DeviceName)
	{
        strcpy(m_DeviceName,DeviceName);
	}

	FIRST = true;
    InitPrivateVeo2();
	FIRST = false;
	NullCalDataVeo2();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Veo2 Class called with Device [%s] ", "Sim %d, Dbg %d", DeviceName, Sim, Dbg));

    // Initialize the Veo2 - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


////////////////////////////////////////////////////////////////////////////////
// Function: ~CVeo2_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    Class instance destroyed.
//
////////////////////////////////////////////////////////////////////////////////
CVeo2_T::~CVeo2_T()
{
    // Reset the Veo2
    CEMDEBUG(5,cs_FmtMsg("Veo2 Class Destructor called for Device [%s], ", m_DeviceName));
    return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: StatusVeo2(int Fnc)
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::StatusVeo2(int Fnc)
{
    int	Status = 0;

    CEMDEBUG(5,cs_FmtMsg("StatusVeo2 (%s) called FNC %d", m_DeviceName, Fnc));

	ProcessFnc(Fnc);

	if (m_SignalType == SOURCE)
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, m_SourceSignal, NULL, 0)));
	    CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %d", m_SourceSignal, Status));
	}
	else if (m_SignalType == SENSOR)
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, m_SensorSignal, NULL, 0)));
	    CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %d", m_SensorSignal, Status));
	}

    return(0);
}


////////////////////////////////////////////////////////////////////////////////
// Function: SetupVeo2_T(int Fnc)
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::SetupVeo2(int Fnc)
{
	int		Status = 0;
    char    XmlValue[16384] = "";


    // Setup action for the Veo2
    CEMDEBUG(5,cs_FmtMsg("SetupVeo2 (%s) called FNC %d", m_DeviceName, Fnc));

	ProcessFnc(Fnc);

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
	
	if((Status) < 0)
	{
        return(0);
	}

    if((Status = GetStmtInfoVeo2(Fnc)) != 0)
	{
        return(0);
	}

	if (m_SignalType == SENSOR)
	{
		switch (m_SignalNoun)
		{
		case INFRARED :
			BuildIrSensor1641(Fnc);
			break;
		case LIGHT :
			BuildVisSensor1641(Fnc);
			break;
		case LASER :
			BuildLaserSensor1641(Fnc);
			break;
		}

		if (m_MaxTime.Exists)
		{
			IFNSIM(Status = cs_IssueAtmlSignalMaxTime("Setup", m_DeviceName, m_MaxTime.Real, m_SensorSignal, XmlValue, 16384));
			CEMDEBUG(5, cs_FmtMsg("Issue Signal \"Setup\" [%s] %d [timeout %lf s]", m_SensorSignal, Status, m_MaxTime.Real));
		}
		else
		{
			IFNSIM(Status = cs_IssueAtmlSignal("Setup", m_DeviceName, m_SensorSignal, XmlValue, 16384));
			CEMDEBUG(5, cs_FmtMsg("Issue Signal \"Setup\" [%s] %d", m_SensorSignal, Status));
		}

	}
	else if (m_SignalType == SOURCE)
	{
		BuildSource1641(Fnc);
		if (m_MaxTime.Exists)
		{
			IFNSIM(Status = cs_IssueAtmlSignalMaxTime("Setup", m_DeviceName, m_MaxTime.Real, m_SourceSignal, XmlValue, 16384));
			CEMDEBUG(5, cs_FmtMsg("Issue Signal \"Setup\" [%s] %d [timeout %lf s]", m_SourceSignal, Status, m_MaxTime.Real));
		}
		else
		{
			IFNSIM(Status = cs_IssueAtmlSignal("Setup", m_DeviceName, m_SourceSignal, XmlValue, 16384));
			CEMDEBUG(5, cs_FmtMsg("Issue Signal \"Setup\" [%s] %d", m_SourceSignal, Status));
		}
	}

    return(0);
}

////////////////////////////////////////////////////////////////////////////////
// Function: InitiateVeo2(int Fnc)
//
// Purpose: Perform the Initiate action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::InitiateVeo2(int Fnc)
{
    int   Status = 0;

    // Initiate action for the Veo2
    CEMDEBUG(5,cs_FmtMsg("InitiateVeo2 (%s) called FNC %d", m_DeviceName, Fnc));

	ProcessFnc(Fnc);

	if (m_SignalType == SENSOR)
	{
	    IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SensorSignal, NULL, 0)));
		CEMDEBUG(5, cs_FmtMsg("Issue Signal \"Enable\" [%s] %d", m_SensorSignal, Status));
	}

    return(0);
}


////////////////////////////////////////////////////////////////////////////////
// Function: FetchVeo2(int Fnc)
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::FetchVeo2(int Fnc)
{
    int     Status = 0;
	int * pIntMeasArray = NULL;
	int		Idx = 0;
	int		mrtdstatus = 0;
	int		count = 0;
	double  MeasValue = 0.0;
	double * pMeasArray = NULL;
    char    XmlValue[16384] = "";;
    char    MeasFunc[32] = "";;
	char	prompt[80] = "";
	char	operresp[100] = "";
    DATUM    *fdat = NULL;
    
    // Fetch action for the Veo2
    CEMDEBUG(5, cs_FmtMsg("FetchVeo2 (%s) called FNC %d", m_DeviceName, Fnc));

	if (Fnc == 8)
	{
		strcpy(prompt, "Is target visible in display? Y/N?");
		GetStr(prompt, operresp);
		CEMDEBUG(5, cs_FmtMsg("GetStr(%s, %s);", prompt, operresp));
		if ((strstr(operresp, "Y") != NULL) ||	(strstr(operresp, "y") != NULL))
		{
			mrtdstatus = 1;
		}
		else
		{
			mrtdstatus = 0;
		}
	}

	if (m_MaxTime.Exists)
	{
		IFNSIM(Status = cs_IssueAtmlSignalMaxTime("Fetch", m_DeviceName, m_MaxTime.Real, m_SensorSignal, XmlValue, 16384));
	}
	else
	{
		IFNSIM(Status = cs_IssueAtmlSignal("Fetch", m_DeviceName, m_SensorSignal, XmlValue, 16384));
	}

	CEMDEBUG(5, cs_FmtMsg("Fetch XmlValue:%s", XmlValue));

	strcpy(MeasFunc, ATTRIBUTES[Fnc]);

    if(Status)
    {
        MeasValue = FLT_MAX;
	}
	else
	{
		fdat = FthDat();
		// scalar measurements
		switch (Fnc)
		{
			case DISTORTION_IR :
			case NOISE_EQ_DIFF_TEMP_IR :
			case UNIFORMITY_IR :
			case DIFFERENTIAL_TEMP_IR :
			case AMBIENT_TEMP_IR :
			case BLACKBODY_TEMP_IR :
			case PULSE_ENERGY_CH1_LASER :
			case PULSE_ENERGY_CH2_LASER :
			case PULSE_ENERGY_STAB_CH1_LASER :
			case PULSE_ENERGY_STAB_CH2_LASER :
			case POWER_P_CH1_LASER :
			case POWER_P_CH2_LASER :
			case PULSE_AMPL_STAB_CH1_LASER :
			case PULSE_AMPL_STAB_CH2_LASER :
			case PULSE_WIDTH_CH1_LASER :
			case PULSE_WIDTH_CH2_LASER :
			case PRF_LASER :
			case PULSE_PERIOD_STAB_LASER :
			case RECEIVER_SENSITIVITY_LASER :
			case RANGE_ERROR_LASER :
			case DISTORTION_VIS :
			case UNIFORMITY_VIS :
			case CAMERA_GAIN_VIS :
			case DYNAMIC_RANGE_VIS :
			case GRAY_SCALE_RESOLUTION_VIS :
				// return scalar measured value
				IFNSIM(cs_GetSingleDblValue(XmlValue, MeasFunc, &MeasValue));
				DECDatVal(fdat, 0) = MeasValue;
				FthCnt(1);
				CEMDEBUG(8, cs_FmtMsg("cs_GetSingleDblValue(%s, %s, %lf);", XmlValue, MeasFunc, MeasValue));
				break;
			// angular measurements
			case DIVERGENCE_LASER :
				// return scalar measured value
				IFNSIM(cs_GetSingleDblValue(XmlValue, MeasFunc, &MeasValue));
				CEMDEBUG(8, cs_FmtMsg("cs_GetSingleDblValue(%s, %s, %lf);", XmlValue, MeasFunc, MeasValue));
				DECDatVal(fdat, 0) = MeasValue  * ToDeg;
				FthCnt(1);
				break;
			// arrays
			case LOS_ALIGN_ERROR_IR :
			case DIFF_BORESIGHT_ANGLE_IR :
			case DIFF_BORESIGHT_ANGLE_LASER :
			case BORESIGHT_ANGLE_LASER :
			case LOS_ALIGN_ERROR_VIS :
			case DIFF_BORESIGHT_ANGLE_VIS :

				count = DatCnt(fdat);
				pMeasArray = new double[count];
				CEMDEBUG(5, cs_FmtMsg("attribute:%s, retrieveing %d values", MeasFunc, count));
				IFNSIM(Status = cs_GetDblArrayValue(XmlValue, MeasFunc, pMeasArray, &count));
				CEMDEBUG(5, cs_FmtMsg("cs_GetDblArrayValue(XmlValue, %s, [%lf, %lf...], %d);", MeasFunc, pMeasArray[0], pMeasArray[1], count));

				for (Idx = 0; Idx < count; Idx++)
				{
					DECDatVal(fdat, Idx) = pMeasArray[Idx] * ToDeg; // convert to degrees, measured characteristic must be in base unit
					CEMDEBUG(5, cs_FmtMsg("cs_GetDblArrayValue(XmlValue, %s, [%lf, %lf...], %d);",
						 MeasFunc, pMeasArray[0], pMeasArray[1], count));
				}

				FthCnt(count);
				delete [] pMeasArray;
				break;

			case MODULATION_TRANSFER_FUNCTION_IR :
			case MODULATION_TRANSFER_FUNCTION_VIS :
			case MIN_RESOLV_CONTRAST_VIS :
				count = DatCnt(fdat);
				pMeasArray = new double[count];
				CEMDEBUG(5, cs_FmtMsg("array return count : %d", count));
				IFNSIM(Status = cs_GetDblArrayValue(XmlValue, MeasFunc, pMeasArray, &count));
				CEMDEBUG(5, cs_FmtMsg("cs_GetDblArrayValue(%s, %s, [%lf, %lf, ...], %d);", XmlValue, MeasFunc, pMeasArray[0], pMeasArray[1], count));

				for (Idx = 0; Idx < count; Idx++)
				{
					DECDatVal(fdat, Idx) = pMeasArray[Idx];
				}

				FthCnt(count);
				delete [] pMeasArray;
				break;
			case CHAN_INTEGRITY_IR :
				count = DatCnt(fdat);
				pIntMeasArray = new int[count];
				CEMDEBUG(5, cs_FmtMsg("%d = DatCnt(%x)", count, fdat));
				IFNSIM(Status = cs_GetIntArrayValue(XmlValue, MeasFunc, pIntMeasArray, &count));
				CEMDEBUG(5, cs_FmtMsg("cs_GetIntArrayValue(XmlValue, %s, [%d, %d, ...], %d);", MeasFunc, pIntMeasArray[0], pIntMeasArray[1], count));

				for (Idx = 0; Idx < count; Idx++)
				{
					INTDatVal(fdat, Idx) = pIntMeasArray[Idx];
				}

				FthCnt(count);
				delete [] pIntMeasArray;
				break;
			case MIN_RESOLV_TEMP_DIFF_IR :
				IFNSIM(cs_GetSingleDblValue(XmlValue, MeasFunc, &MeasValue));
				CEMDEBUG(8, cs_FmtMsg("cs_GetSingleDblValue(%s, %s, %lf);", XmlValue, MeasFunc, MeasValue));
				CEMDEBUG(5, cs_FmtMsg("mrtdstatus : %d", mrtdstatus));
				if (mrtdstatus == 0)
				{
					MeasValue = -999.0;
				}
				DECDatVal(fdat, 0) = MeasValue;
				FthCnt(1);
				break;
		}
	}

    return(0);
}



////////////////////////////////////////////////////////////////////////////////
// Function: OpenVeo2(int Fnc)
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::OpenVeo2(int Fnc)
{
    // Open action for the Veo2
    CEMDEBUG(5,cs_FmtMsg("OpenVeo2 (%s) called FNC %d", m_DeviceName, Fnc));

    return(0);
}


////////////////////////////////////////////////////////////////////////////////
// Function: CloseVeo2(int Fnc)
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::CloseVeo2(int Fnc)
{
    // Close action for the Veo2
    CEMDEBUG(5,cs_FmtMsg("CloseVeo2 (%s) called FNC %d", m_DeviceName, Fnc));
    
    return(0);
}


////////////////////////////////////////////////////////////////////////////////
// Function: ResetVeo2(int Fnc)
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::ResetVeo2(int Fnc)
{
    int   Status = 0;

    // Reset action for the Veo2
    CEMDEBUG(5,cs_FmtMsg("ResetVeo2 (%s) called FNC %d", m_DeviceName, Fnc));

	ProcessFnc(Fnc);

    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if(Fnc != 0)
    {
		if (m_SignalType == SENSOR)
		{
			IFNSIM(Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SensorSignal, NULL, 0));
		}
		else if (m_SignalType == SOURCE)
		{
			IFNSIM(Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SourceSignal, NULL, 0));
		}
    }

    InitPrivateVeo2();

    return(0);
}

//++++//////////////////////////////////////////////////////////////////////////
// Private Class Functions
////////////////////////////////////////////////////////////////////////////////

////////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoVeo2(int Fnc)
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             Device Database Function value
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
////////////////////////////////////////////////////////////////////////////////
int CVeo2_T::GetStmtInfoVeo2(int Fnc)
{
    DATUM	*pDatum;
	char	*tmpstr;

	//////////////////////////////////////////////////////////
	// Common Modifiers                                     //
	//////////////////////////////////////////////////////////
	//////////////////////////////////////////////////
	// Modifier  : FILTER                           //
	// Key       : M_FILT                           //
	// Qualfier  : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_FILT, K_SET)) != 0)
	{
		m_Filter.Exists = true;
		m_Filter.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found FILTER %d", m_Filter.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : INTENSITY-RATIO                  //
	// Key       : M_ITRO                           //
	// Qualfier  : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_ITRO, K_SET)) != 0)
	{
		m_IntensityRatio.Exists = true;
		m_IntensityRatio.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found INTENSITY-RATIO %lf PC", m_IntensityRatio.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : MAX-TIME                         //
	// Key       : M_MAXT                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_MAXT, K_SET)) != 0)
	{
		m_MaxTime.Exists = true;
		m_MaxTime.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found MAX-TIME %lf SEC", m_MaxTime.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : SAMPLE-AV                        //
	// Key       : M_SMAV                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
    if (((pDatum = GetDatum(M_SMAV, K_SET)) != 0) || ((pDatum = GetDatum(M_SCNT, K_SET)) != 0))
	{
		m_SampleCount.Exists = true;
		m_SampleCount.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found SAMPLE-COUNT %.0lf COUNTS", m_SampleCount.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : TEST-POINT-COUNT                 //
	// Key       : M_TSPC                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
    if ((pDatum = GetDatum(M_TSPC, K_SET)) != 0)
	{
		m_TestPointCount.Exists = true;
		m_TestPointCount.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found TEST-POINT-COUNT %d", m_TestPointCount.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : WAVE-LENGTH                      //
	// Key       : M_WAVE                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
    if ((pDatum = GetDatum(M_WAVE, K_SET)) != 0)
	{
		m_WaveLength.Exists = true;
		m_WaveLength.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found WAVE-LENGTH %e M", m_WaveLength.Real));
	}

	//////////////////////////////////////////////////////////////////
	// Target Modifiers [common]                                    //
	//////////////////////////////////////////////////////////////////
	//////////////////////////////////////////////////
	// Modifier  : DIST-POS-COUNT                   //
	// Key       : M_DSPC                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_DSPC, K_SET)) != 0)
	{
		m_Target = true;
		m_DistPosCount.Exists = true;
		m_DistPosCount.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found DIST-POS-COUNT %d", m_DistPosCount.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : DISTORTION-POSITIONS             //
	// Key       : M_DSTP                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_DSTP, K_SET)) != 0)
	{
		m_Target = true;
		m_DistortionPositions.Exists = true;
		m_DistortionPositions.Size = DatCnt(pDatum);
		m_DistortionPositions.val = new double[m_DistortionPositions.Size];

		for (int Idx = 0; Idx < m_DistortionPositions.Size; Idx++)
		{
			m_DistortionPositions.val[Idx] = DECDatVal(pDatum, Idx);
		}

		CEMDEBUG(10, cs_FmtMsg("Found DISTORTION-POSITIONS %d elements", m_DistortionPositions.Size));

	}
	//////////////////////////////////////////////////
	// Modifier  : H-TARGET-ANGLE                   //
	// Key       : M_HTAG                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_HTAG, K_SET)) != 0)
	{
		m_Target = true;
		m_HorizTargetAngle.Exists = true;
		m_HorizTargetAngle.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found H-TARGET-ANGLE %lf RAD", m_HorizTargetAngle.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : V-TARGET-ANGLE                   //
	// Key       : M_VTAG                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_VTAG, K_SET)) != 0)
	{
		m_Target = true;
		m_VertTargetAngle.Exists = true;
		m_VertTargetAngle.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found V-TARGET-ANGLE %lf RAD", m_VertTargetAngle.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : H-TARGET-OFFSET                  //
	// Key       : M_HTOF                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_HTOF, K_SET)) != 0)
	{
		m_Target = true;
		m_HorizTargetOffset.Exists = true;
		m_HorizTargetOffset.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found H-TARGET-OFFSET %lf RAD", m_HorizTargetOffset.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : V-TARGET-OFFSET                  //
	// Key       : M_VTOF                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_VTOF, K_SET)) != 0)
	{
		m_Target = true;
		m_VertTargetOffset.Exists = true;
		m_VertTargetOffset.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found V-TARGET-OFFSET %lf RAD", m_VertTargetOffset.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : TGT-COORDINATE-TOP               //
	// Key       : M_TCTP                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TCTP, K_SET)) != 0)
	{
		m_Target = true;
		m_TgtCoordinateTop.Exists = true;
		m_TgtCoordinateTop.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found TGT-COORDINATE-TOP %d", m_TgtCoordinateTop.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : TGT-COORDINATE-LEFT              //
	// Key       : M_TCLT                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TCLT, K_SET)) != 0)
	{
		m_Target = true;
		m_TgtCoordinateLeft.Exists = true;
		m_TgtCoordinateLeft.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found TGT-COORDINATE-LEFT %d", m_TgtCoordinateLeft.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : TGT-COORDINATE-BOTTOM            //
	// Key       : M_TCBT                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TCBT, K_SET)) != 0)
	{
		m_Target = true;
		m_TgtCoordinateBottom.Exists = true;
		m_TgtCoordinateBottom.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found TGT-COORDINATE-BOTTOM %d", m_TgtCoordinateBottom.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : TGT-COORDINATE-RIGHT             //
	// Key       : M_TCRT                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TCRT, K_SET)) != 0)
	{
		m_Target = true;
		m_TgtCoordinateRight.Exists = true;
		m_TgtCoordinateRight.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found TGT-COORDINATE-RIGHT %d", m_TgtCoordinateRight.Int));
	}

	//////////////////////////////////////////////////
	// Modifier  : TARGET-DATA                      //
	// Key       : M_TDAT                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TDAT, K_SET)) != 0)
	{
		m_Target = true;
		m_TargetData.Exists = true;
		m_TargetData.Size = DatCnt(pDatum);
		m_TargetData.val = new double[m_TargetData.Size];
		for (int Idx = 0; Idx < m_TargetData.Size; Idx++)
		{
			m_TargetData.val[Idx] = DECDatVal(pDatum, Idx);
		}
		CEMDEBUG(10, cs_FmtMsg("Found TARGET-DATA %d elements", m_TargetData.Size));
	}

	//////////////////////////////////////////////////
	// Modifier  : TARGET-RANGE                     //
	// Key       : M_TGTD                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TGTD, K_SET)) != 0)
	{
		m_Target = true;
		m_TargetRange.Exists = true;
		m_TargetRange.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found TARGET-RANGE %lf M", m_TargetRange.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : LAST-PULSE-RANGE                 //
	// Key       : M_TGTS                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TGTS, K_SET)) != 0)
	{
		m_Target = true;
		m_LastPulseRange.Exists = true;
		m_LastPulseRange.Real = DECDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found LAST-PULSE-RANGE %lf M", m_LastPulseRange.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : TARGET-TYPE                      //
	// Key       : M_TGTP                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_TGTP, K_SET)) != 0)
	{
		m_Target = true;
		m_TargetType.Exists = true;
		tmpstr = GetTXTDatVal(pDatum, 0);
		// Temp debug
		CEMDEBUG(10, cs_FmtMsg("%s = GetTXTDatVal(%x, 0);", tmpstr, pDatum));
		if (strcmp(tmpstr, "T000") == 0)
		{
			m_TargetType.Int = DIM_TARG_OPNAPR;
		}
		else if (strcmp(tmpstr, "T001") == 0)
		{
			m_TargetType.Int = DIM_TARG_BRSGHT;
		}
		else if (strcmp(tmpstr, "T002") == 0)
		{
			m_TargetType.Int = DIM_TARG_PIESECTOR;
		}
		else if (strcmp(tmpstr, "T003") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR5;
		}
		else if (strcmp(tmpstr, "T004") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR383;
		}
		else if (strcmp(tmpstr, "T005") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR267;
		}
		else if (strcmp(tmpstr, "T006") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR15;
		}
		else if (strcmp(tmpstr, "T007") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR33;
		}
		else if (strcmp(tmpstr, "T008") == 0)
		{
			m_TargetType.Int = DIM_TARG_DIAGLN;
		}
		else if (strcmp(tmpstr, "T009") == 0)
		{
			m_TargetType.Int = DIM_TARG_ETCHED;
		}
		else if (strcmp(tmpstr, "T010") == 0)
		{
			m_TargetType.Int = DIM_TARG_SPNPINHL;
		}
		else if (strcmp(tmpstr, "T011") == 0)
		{
			m_TargetType.Int = DIM_TARG_TGTGRP07;
		}
		else if (strcmp(tmpstr, "T012") == 0)
		{
			m_TargetType.Int = DIM_TARG_TGTGRP1;
		}
		else if (strcmp(tmpstr, "T013") == 0)
		{
			m_TargetType.Int = DIM_TARG_TGTGRP2;
		}
		else if (strcmp(tmpstr, "T014") == 0)
		{
			m_TargetType.Int = DIM_TARG_GRYSCL;
		}
		else if (strcmp(tmpstr, "T015") == 0)
		{
			m_TargetType.Int = DIM_TARG_CROSS17;
		}
		else if (strcmp(tmpstr, "T016") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR10;
		}
		else if (strcmp(tmpstr, "T017") == 0)
		{
			m_TargetType.Int = DIM_TARG_4BAR66;
		}
		else if (strcmp(tmpstr, "T018") == 0)
		{
			m_TargetType.Int = DIM_TARG_SQUARE21;
		}
		else if (strcmp(tmpstr, "IRBS00") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS00;
		}
		else if (strcmp(tmpstr, "IRBS01") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS01;
		}
		else if (strcmp(tmpstr, "IRBS02") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS02;
		}
		else if (strcmp(tmpstr, "IRBS03") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS03;
		}
		else if (strcmp(tmpstr, "IRBS04") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS04;
		}
		else if (strcmp(tmpstr, "IRBS05") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS05;
		}
		else if (strcmp(tmpstr, "IRBS06") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS06;
		}
		else if (strcmp(tmpstr, "IRBS07") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS07;
		}
		else if (strcmp(tmpstr, "IRBS08") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS08;
		}
		else if (strcmp(tmpstr, "IRBS09") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS09;
		}
		else if (strcmp(tmpstr, "IRBS10") == 0)
		{
			m_TargetType.Int = DIM_TARG_IRBS10;
		}
		else if (strcmp(tmpstr, "TVBS01") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS01;
		}
		else if (strcmp(tmpstr, "TVBS02") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS02;
		}
		else if (strcmp(tmpstr, "TVBS03") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS03;
		}
		else if (strcmp(tmpstr, "TVBS04") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS04;
		}
		else if (strcmp(tmpstr, "TVBS05") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS05;
		}
		else if (strcmp(tmpstr, "TVBS06") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS06;
		}
		else if (strcmp(tmpstr, "TVBS07") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS07;
		}
		else if (strcmp(tmpstr, "TVBS08") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS08;
		}
		else if (strcmp(tmpstr, "TVBS09") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS09;
		}
		else if (strcmp(tmpstr, "TVBS10") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS10;
		}
		else if (strcmp(tmpstr, "TVBS11") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS11;
		}
		else if (strcmp(tmpstr, "TVBS12") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS12;
		}
		else if (strcmp(tmpstr, "TVBS13") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS13;
		}
		else if (strcmp(tmpstr, "TVBS14") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS14;
		}
		else if (strcmp(tmpstr, "TVBS15") == 0)
		{
			m_TargetType.Int = DIM_TARG_TVBS15;
		}

		CEMDEBUG(10, cs_FmtMsg("Found TARGET-TYPE %s", TARGETS[m_TargetType.Int]));
	}

	/////////////////////////////////////////////////////////
	//              BORESIGHT MODIFIERS                    //
	/////////////////////////////////////////////////////////
	//////////////////////////////////////////////////
	// Modifier  : H-LOS-ALIGN-ERROR                //
	// Key       : M_HLAE                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_HLAE, K_SET)) != 0)
	{
		m_Boresight = true;
		m_HorizLosAlignError.Exists = true;
		m_HorizLosAlignError.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found H-LOS-ALIGN-ERROR %lf RAD", m_HorizLosAlignError.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : V-LOS-ALIGN-ERROR                //
	// Key       : M_VLAE                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_VLAE, K_SET)) != 0)
	{
		m_Boresight = true;
		m_VertLosAlignError.Exists = true;
		m_VertLosAlignError.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found V-LOS-ALIGN-ERROR %lf RAD", m_VertLosAlignError.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : X-AUTOCOLLIMATION-ERROR          //
	// Key       : M_XACE                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_XACE, K_SET)) != 0)
	{
		m_Boresight = true;
		m_XAutocollimationError.Exists = true;
		m_XAutocollimationError.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found X-AUTOCOLLIMATION-ERROR %lf RAD", m_XAutocollimationError.Real));
		FreeDatum(pDatum);
	}

	//////////////////////////////////////////////////
	// Modifier  : Y-AUTOCOLLIMATION-ERROR          //
	// Key       : M_YACE                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_YACE, K_SET)) != 0)
	{
		m_Boresight = true;
		m_YAutocollimationError.Exists = true;
		m_YAutocollimationError.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found Y-AUTOCOLLIMATION-ERROR %lf RAD", m_YAutocollimationError.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : X-BORESIGHT-ANGLE                //
	// Key       : M_XBAG                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_XBAG, K_SET)) != 0)
	{
		m_Boresight = true;
		m_XBoresightAngle.Exists = true;
		m_XBoresightAngle.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found X-BORESIGHT-ANGLE %lf RAD", m_XBoresightAngle.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : Y-BORESIGHT-ANGLE                //
	// Key       : M_YBAG                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_YBAG, K_SET)) != 0)
	{
		m_Boresight = true;
		m_YBoresightAngle.Exists = true;
		m_YBoresightAngle.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found Y-BORESIGHT-ANGLE %lf RAD", m_YBoresightAngle.Real));
	}

	/////////////////////////////////////////////////////////
	//               VIDEO FORMAT MODIFIERS                //
	/////////////////////////////////////////////////////////
	//////////////////////////////////////////////////
	// Modifier  : FORMAT                           //
	// Key       : M_FRMT                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_FRMT, K_SET)) != 0)
	{
		m_Video = true;
		m_Format.Exists = true;
		tmpstr = GetTXTDatVal(pDatum, 0);
		if (strcmp(tmpstr, "R000") == 0)
		{
			m_Format.Int = DIM_RS170;
		}
		else if (strcmp(tmpstr, "R001") == 0)
		{
			m_Format.Int = DIM_RS343_675_1_1;
		}
		else if (strcmp(tmpstr, "R002") == 0)
		{
			m_Format.Int = DIM_RS343_675_4_3;
		}
		else if (strcmp(tmpstr, "R003") == 0)
		{
			m_Format.Int = DIM_RS343_729_1_1;
		}
		else if (strcmp(tmpstr, "R004") == 0)
		{
			m_Format.Int = DIM_RS343_729_4_3;
		}
		else if (strcmp(tmpstr, "R005") == 0)
		{
			m_Format.Int = DIM_RS343_875_1_1;
		}
		else if (strcmp(tmpstr, "R006") == 0)
		{
			m_Format.Int = DIM_RS343_875_4_3;
		}
		else if (strcmp(tmpstr, "R007") == 0)
		{
			m_Format.Int = DIM_RS343_945_1_1;
		}
		else if (strcmp(tmpstr, "R008") == 0)
		{
			m_Format.Int = DIM_RS343_945_4_3;
		}
		else if (strcmp(tmpstr, "R009") == 0)
		{
			m_Format.Int = DIM_RS343_1023_1_1;
		}
		else if (strcmp(tmpstr, "R010") == 0)
		{
			m_Format.Int = DIM_RS343_1023_4_3;
		}
		else if (strcmp(tmpstr, "INT1") == 0)
		{
			m_Format.Int = DIM_INTERNAL_1;
		}
		else if (strcmp(tmpstr, "INT2") == 0)
		{
			m_Format.Int = DIM_INTERNAL_2;
		}
		CEMDEBUG(10, cs_FmtMsg("Found FORMAT %s", FORMATS[m_Format.Int]));
	}

	//////////////////////////////////////////////////
	// Modifier  : H-FIELD-OF-VIEW                  //
	// Key       : M_HFOV                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_HFOV, K_SET)) != 0)
	{
		m_Video = true;
		m_HorizFieldOfView.Exists = true;
		m_HorizFieldOfView.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found H-FIELD-OF-VIEW %lf RAD", m_HorizFieldOfView.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : V-FIELD-OF-VIEW                  //
	// Key       : M_VFOV                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_VFOV, K_SET)) != 0)
	{
		m_Video = true;
		m_VertFieldOfView.Exists = true;
		m_VertFieldOfView.Real = RAD(DECDatVal(pDatum, 0));
		CEMDEBUG(10, cs_FmtMsg("Found V-FIELD-OF-VIEW %lf RAD", m_VertFieldOfView.Real));
	}

	//////////////////////////////////////////////////
	// Modifier  : MTF-DIRECTION                    //
	// Key       : M_MTFD                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_MTFD, K_SET)) != 0)
	{
		m_MtfDirection.Exists = true;
		tmpstr = GetTXTDatVal(pDatum, 0);
		if (strcmp(tmpstr, "HORIZ") == 0)
		{
			m_MtfDirection.Int = DIM_HORIZ;
		}
		else if (strcmp(tmpstr, "VERT") == 0)
		{
			m_MtfDirection.Int = DIM_VERT;
		}
		CEMDEBUG(10, cs_FmtMsg("Found MTF-DIRECTION %s", DIMS[m_MtfDirection.Int]));
	}

	//////////////////////////////////////////////////
	// Modifier  : MTF-FREQ-POINTS                  //
	// Key       : M_MTFP                           //
	// Qualifier : K_SET                            //
	//////////////////////////////////////////////////
	if ((pDatum = GetDatum(M_MTFP, K_SET)) != 0)
	{
		m_MtfFreqPoints.Exists = true;
		m_MtfFreqPoints.Int = INTDatVal(pDatum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found MTF-FREQ-POINTS %d", m_MtfFreqPoints.Int));
	}

	/////////////////////////////////////////////////////////////////
	//                   INFRARED MODIFIERS                        //
	/////////////////////////////////////////////////////////////////
	if (m_SignalNoun == INFRARED)
	{
		//////////////////////////////////////////////////
		// Modifier  : DIFFERENTIAL-TEMP                //
		// Key       : M_DIFT                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_DIFT, K_SET)) != 0) || ((pDatum = GetDatum(M_DIFT, K_SRX)) != 0))
		{
			m_DifferentialTemp.Exists = true;
			m_DifferentialTemp.Real = DECDatVal(pDatum, 0);
		    CEMDEBUG(10,cs_FmtMsg("Found DIFFERENTIAL-TEMP %lf DEGC", m_DifferentialTemp.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : DIFF-TEMP-ERROR                  //
		// Key       : M_DTER                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_DTER, K_SET)) != 0)
		{
			m_DiffTempError.Exists = true;
			m_DiffTempError.Real = DECDatVal(pDatum, 0);
		    CEMDEBUG(10,cs_FmtMsg("Found DIFF-TEMP-ERROR %lf DEGC", m_DiffTempError.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : DIFF-TEMP-INTERVAL               //
		// Key       : M_DTIL                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_DTIL, K_SET)) != 0)
		{
			m_DiffTempInterval.Exists = true;
			m_DiffTempInterval.Real = DECDatVal(pDatum, 0);
		    CEMDEBUG(10,cs_FmtMsg("Found DIFF-TEMP-INTERVAL %lf DEGC", m_DiffTempInterval.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : DIFF-TEMP-START                  //
		// Key       : M_DTST                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_DTST, K_SET)) != 0)
		{
			m_DiffTempStart.Exists = true;
			m_DiffTempStart.Real = DECDatVal(pDatum, 0);
		    CEMDEBUG(10,cs_FmtMsg("Found DIFF-TEMP-START %lf DEGC", m_DiffTempStart.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : DIFF-TEMP-STOP                   //
		// Key       : M_DTSP                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_DTSP, K_SET)) != 0)
		{
			m_DiffTempStop.Exists = true;
			m_DiffTempStop.Real = DECDatVal(pDatum, 0);
		    CEMDEBUG(10, cs_FmtMsg("Found DIFF-TEMP-STOP %lf DEGC", m_DiffTempStop.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : FIRST-ACTIVE-LINE                //
		// Key       : M_FIAL                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_FIAL, K_SET)) != 0)
		{
			m_FirstActiveLine.Exists = true;
			m_FirstActiveLine.Int = INTDatVal(pDatum, 0);
		    CEMDEBUG(10, cs_FmtMsg("Found FIRST-ACTIVE-LINE %d", m_FirstActiveLine.Int));
		}

		//////////////////////////////////////////////////
		// Modifier  : LINES-PER-CHANNEL                //
		// Key       : M_LIPF                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_LIPF, K_SET)) != 0)
		{
			m_LinesPerChannel.Exists = true;
			m_LinesPerChannel.Int = INTDatVal(pDatum, 0);
		    CEMDEBUG(10, cs_FmtMsg("Found LINES-PER-CHANNEL %d", m_LinesPerChannel.Int));
		}

		//////////////////////////////////////////////////
		// Modifier  : NOISE-EQ-DIFF-TEMP               //
		// Key       : M_NEDT                           //
		// Qualifier : K_SET, K_SRX                     //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_NEDT, K_SET)) != 0)
		{
			m_NoiseEqDiffTemp.Exists = true;
			m_NoiseEqDiffTemp.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found NOISE-EQ-DIFF-TEMP %lf DEGC", m_NoiseEqDiffTemp.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : SETTLE-TIME                      //
		// Key       : M_SETT                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_SETT, K_SET)) != 0)
		{
			m_SettleTime.Exists = true;
			m_SettleTime.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found SETTLE-TIME %lf SEC", m_SettleTime.Real));
		}


	} // end infrared modifiers
	//////////////////////////////////////////////////////////////////
	//                     LIGHT MODIFIERS                          //
	//////////////////////////////////////////////////////////////////
	if (m_SignalNoun == LIGHT)
	{
		//////////////////////////////////////////////////
		// Modifier  : RADIANCE                         //
		// Key       : M_RDNC                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_RDNC, K_SET)) != 0)
		{
			m_Radiance.Exists = true;
			m_Radiance.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found RADIANCE %lf UW/CM2-SR", m_Radiance.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : RADIANCE-INTERVAL                //
		// Key       : M_RAIL                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_RAIL, K_SET)) != 0)
		{
			m_RadianceInterval.Exists = true;
			m_RadianceInterval.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found RADIANCE-INTERVAL %lf UW/CM2-SR", m_RadianceInterval.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : RADIANCE-START                   //
		// Key       : M_RAST                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_RAST, K_SET)) != 0)
		{
			m_RadianceStart.Exists = true;
			m_RadianceStart.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found RADIANCE-START %lf UW/CM2-SR", m_RadianceStart.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : RADIANCE-STOP                    //
		// Key       : M_RASP                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_RASP, K_SET)) != 0)
		{
			m_RadianceStop.Exists = true;
			m_RadianceStop.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found RADIANCE-STOP %lf UW/CM2-SR", m_RadianceStop.Real));
		}

	} // end light modifiers
	//////////////////////////////////////////////////////////////////
	//                       LASER MODIFIERS                        //
	//////////////////////////////////////////////////////////////////
	if (m_SignalNoun == LASER)
	{
		//////////////////////////////////////////////////
		// Modifier  : DELAY                            //
		// Key       : M_DELA                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_DELA, K_SET)) != 0)
		{
			m_Delay.Exists = true;
			m_Delay.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found DELAY %lf SEC", m_Delay.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : MAIN-BEAM-ATTEN                  //
		// Key       : M_MBAT                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_MBAT, K_SET)) != 0)
		{
			m_MainBeamAtten.Exists = true;
			m_MainBeamAtten.Real = DECDatVal(pDatum, 0);
		    CEMDEBUG(10, cs_FmtMsg("Found MAIN-BEAM-ATTEN %lf OD", m_MainBeamAtten.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : PERIOD                           //
		// Key       : M_PERI                           //
		// Qualifier : K_SET, K_SRX                     //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_PERI, K_SET)) != 0) || ((pDatum = GetDatum(M_PERI, K_SRX)) != 0))
		{
			m_Period.Exists = true;
			m_Period.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found PERIOD %e SEC", m_Period.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : POWER-P                          //
		// Key       : M_POWP                           //
		// Qualifier : K_SET, K_SRX                     //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_POWP, K_SET)) != 0) || ((pDatum = GetDatum(M_POWP, K_SRX)) != 0))
		{
			m_PowerP.Exists = true;
			m_PowerP.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found POWER-P %lf W", m_PowerP.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : POWER-DENS                       //
		// Key       : M_PODN                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_PODN, K_SET)) != 0)
		{
			m_PowerDensity.Exists = true;
			m_PowerDensity.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found POWER-DENS %lf W/CM2", m_PowerDensity.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : PULSE-ENERGY                     //
		// Key       : M_PLEG                           //
		// Qualifier : K_SRX, K_SRN                     //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_PLEG, K_SRX)) != 0) || ((pDatum = GetDatum(M_PLEG, K_SRN)) != 0))
		{
			m_PulseEnergy.Exists = true;
			m_PulseEnergy.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found PULSE-ENERGY %lf J", m_PulseEnergy.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : PRF                              //
		// Key       : M_PRFR                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_PRFR, K_SET)) != 0)
		{
			m_Prf.Exists = true;
			m_Prf.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found PRF %lf HZ", m_Prf.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : PULSE-WIDTH                      //
		// Key       : M_PLWD                           //
		// Qualifier : K_SET, K_SRX                     //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_PLWD, K_SRX)) != 0) || ((pDatum = GetDatum(M_PLWD, K_SRN)) != 0))
		{
			m_PulseWidth.Exists = true;
			m_PulseWidth.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found PULSE-WIDTH %e SEC", m_PulseWidth.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : RANGE-ERROR                      //
		// Key       : M_RERR                           //
		// Qualifier : K_SET, K_SRX                     //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_RERR, K_SET)) != 0) || ((pDatum = GetDatum(M_RERR, K_SRX)) != 0))
		{
			m_RangeError.Exists = true;
			m_RangeError.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found RANGE-ERROR %lf M", m_RangeError.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : SAMPLE-TIME                      //
		// Key       : M_SATM                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_SATM, K_SET)) != 0)
		{
			m_SampleTime.Exists = true;
			m_SampleTime.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found SAMPLE-TIME %lf SEC", m_SampleTime.Real));
		}

		//////////////////////////////////////////////////
		// Modifier  : TRIG-LEVEL                       //
		// Key       : M_TRLV                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_TRLV, K_SET)) != 0)
		{
			m_TrigLevel.Exists = true;
			m_TrigLevel.Real = DECDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found TRIG-LEVEL %lf V", m_TrigLevel.Real));
			m_Trigger = true;
		}

		//////////////////////////////////////////////////
		// Modifier  : TRIGGER-MODE                     //
		// Key       : M_TGMD                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if (((pDatum = GetDatum(M_TGMD, K_SET)) != 0) || ((pDatum = GetDatum(M_TRGS, K_SET)) != 0))
		{
			m_Trigger = true;
			m_TriggerMode.Exists = true;
			tmpstr = GetTXTDatVal(pDatum, 0);
			if (strcmp(tmpstr, "INT") == 0)
			{
				m_TriggerMode.Int = DIM_INT;
			}
			else if (strcmp(tmpstr, "EXT") == 0)
			{
				m_TriggerMode.Int = DIM_EXT;
			}
			else if (strcmp(tmpstr, "AUTO") == 0)
			{
				m_TriggerMode.Int = DIM_AUTO;
			}
			else if (strcmp(tmpstr, "LASR") == 0)
			{
				m_TriggerMode.Int = DIM_LASR;
			}
			else if (strcmp(tmpstr, "CH1") == 0)
			{
				m_TriggerMode.Int = DIM_CH1;
			}
			else if (strcmp(tmpstr, "CH2") == 0)
			{
				m_TriggerMode.Int = DIM_CH2;
			}
			CEMDEBUG(10, cs_FmtMsg("Found TRIGGER-MODE[SOURCE] %s", DIMS[m_TriggerMode.Int]));
		}

		//////////////////////////////////////////////////
		// Modifier  : TRIG-SLOPE                       //
		// Key       : M_TRSL                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_TRSL, K_SET)) != 0)
		{
			m_Trigger = true;
			m_TriggerSlope.Exists = true;
			tmpstr = GetTXTDatVal(pDatum, 0);
			if (strcmp(tmpstr, "POS") == 0)
			{
				m_TriggerSlope.Int = DIM_GT;
			}

			else if (strcmp(tmpstr, "NEG") == 0)
			{
				m_TriggerSlope.Int = DIM_LT;
			}
			CEMDEBUG(10, cs_FmtMsg("Found TRIGGER-SLOPE %s", DIMS[m_TriggerSlope.Int]));
		}

	} // end laser modifiers	
	//////////////////////////////////////////////////////////////////
	//                       LARRS MODIFIERS                        //
	//////////////////////////////////////////////////////////////////
	if (m_SignalNoun == LARRS)
	{
		//////////////////////////////////////////////////
		// Modifier  : AZIMUTH                          //
		// Key       : M_AZIM                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_AZIM, K_SET)) != 0)
		{
			m_Azimuth.Exists = true;
			m_Azimuth.Int = INTDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found AZIMUTH %d", m_Azimuth.Int));
		}

		//////////////////////////////////////////////////
		// Modifier  : ELEVATION                        //
		// Key       : M_ELEV                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_ELEV, K_SET)) != 0)
		{
			m_Elevation.Exists = true;
			m_Elevation.Int = INTDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found ELEVATION %d", m_Elevation.Int));
		}

		//////////////////////////////////////////////////
		// Modifier  : POLARIZE                         //
		// Key       : M_POLR                           //
		// Qualifier : K_SET                            //
		//////////////////////////////////////////////////
		if ((pDatum = GetDatum(M_POLR, K_SET)) != 0)
		{
			m_Polarize.Exists = true;
			m_Polarize.Int = INTDatVal(pDatum, 0);
			CEMDEBUG(10, cs_FmtMsg("Found POLARIZE %d DEG", m_Polarize.Int));
		}
	}
    return(0);
}
#pragma warning(default:4100)

////////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateVeo2()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::InitPrivateVeo2(void)
{
	ClearModStruct(m_Azimuth);
	ClearModStruct(m_Delay);
	ClearModStruct(m_DifferentialTemp);
	ClearModStruct(m_DiffTempError);
	ClearModStruct(m_DiffTempInterval);
	ClearModStruct(m_DiffTempStart);
	ClearModStruct(m_DiffTempStop);
	ClearModStruct(m_DistPosCount);
	ClearModStruct(m_Elevation);
	ClearModStruct(m_Filter);
	ClearModStruct(m_FirstActiveLine);
	ClearModStruct(m_IntensityRatio);
	ClearModStruct(m_LinesPerChannel);
	ClearModStruct(m_MainBeamAtten);
	ClearModStruct(m_MaxTime);
	ClearModStruct(m_MtfDirection);
	ClearModStruct(m_MtfFreqPoints);
	ClearModStruct(m_NoiseEqDiffTemp);
	ClearModStruct(m_Period);
	ClearModStruct(m_Polarize);
	ClearModStruct(m_PowerP);
	ClearModStruct(m_PowerDensity);
	ClearModStruct(m_Prf);
	ClearModStruct(m_PulseEnergy);
	ClearModStruct(m_PulseWidth);
	ClearModStruct(m_Radiance);
	ClearModStruct(m_RadianceInterval);
	ClearModStruct(m_RadianceStart);
	ClearModStruct(m_RadianceStop);
	ClearModStruct(m_SampleCount);
	ClearModStruct(m_SampleTime);
	ClearModStruct(m_SettleTime);
	ClearModStruct(m_TestPointCount);
	ClearModStruct(m_TrigLevel);
	ClearModStruct(m_TriggerMode);
	ClearModStruct(m_TriggerSlope);
	ClearModStruct(m_WaveLength);
	ClearModStruct(m_HorizTargetAngle);
	ClearModStruct(m_VertTargetAngle);
	ClearModStruct(m_HorizTargetOffset);
	ClearModStruct(m_VertTargetOffset);
	ClearModStruct(m_LastPulseRange);
	ClearModStruct(m_RangeError);
	ClearModStruct(m_TgtCoordinateTop);
	ClearModStruct(m_TgtCoordinateLeft);
	ClearModStruct(m_TgtCoordinateBottom);
	ClearModStruct(m_TgtCoordinateRight);
	ClearDblArrayStruct(m_TargetData);
	ClearModStruct(m_TargetRange);
	ClearModStruct(m_TargetType);
	ClearModStruct(m_HorizLosAlignError);
	ClearModStruct(m_VertLosAlignError);
	ClearModStruct(m_XAutocollimationError);
	ClearModStruct(m_YAutocollimationError);
	ClearModStruct(m_XBoresightAngle);
	ClearModStruct(m_YBoresightAngle);
	ClearModStruct(m_Format);
	ClearModStruct(m_HorizFieldOfView);
	ClearModStruct(m_VertFieldOfView);
		
	ClearDblArrayStruct(m_TargetData);
	ClearDblArrayStruct(m_DistortionPositions);

	m_SignalNoun = 0;
	m_SignalType = 0;

	m_Boresight = false;
	m_Target = false;
	m_Video = false;
	m_Trigger = false;

    return;
}


////////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataVeo2()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::NullCalDataVeo2(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

    return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: ProcessFnc(int fnc)
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// fnc              int             Function number
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::ProcessFnc(int fnc)
{

	switch(fnc)
	{
		case 1 : case 2: case 3 : case 4 : case 5 : case 6 : case 7 : case 8 :
		case 9 : case 10 : case 11 :
			m_SignalNoun = INFRARED;
			m_SignalType = SENSOR;
			break;
		case 12 : case 13 : case 14 : case 15 : case 16 : case 17 : case 18 :
		case 19 : case 20 : case 21 : case 22 : case 23 : case 24 : case 25 :
		case 26 : case 27 : case 28 : case 29 : 
			m_SignalNoun = LASER;
			m_SignalType = SENSOR;
			break;
		case 30 : case 31 : case 32 : case 33 : case 34 : case 35 : case 36 :
		case 37 : case 38 :
			m_SignalNoun = LIGHT;
			m_SignalType = SENSOR;
			break;
		case 40 : case 41 :
			m_SignalNoun = INFRARED;
			m_SignalType = SOURCE;
			break;
		case 42 : case 43 :
			m_SignalNoun = LASER;
			m_SignalType = SOURCE;
			break;
		case 44 : case 45 :
			m_SignalNoun = LIGHT;
			m_SignalType = SOURCE;
			break;
		case 46:
			m_SignalNoun = LARRS;
			m_SignalType = SOURCE;
		default:
			break;
	}
	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: BuildIrSensor1641(int Fnc)
//
// Purpose: Generate Atml string for infrared sensors
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             Function number
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::BuildIrSensor1641(int Fnc)
{
	char tmpstring[256] = "";
	char InName[50] = "";
	char Port[25] = "";
	int idx = 0;
	char SyncString[25] = "";
	char InString[25] = "";

	SetIRDefaults(Fnc);

	if (Fnc < 8)
		strcpy(Port, "VIDEO");
	else
		strcpy(Port, "ATMOSPHERE");

	sprintf(m_SensorSignal, "<Signal name=\"EOSIGNAL\" Out=\"IR_MEAS\" In=\"%s\">\n", Port);
	sprintf(tmpstring, "\t<atXml:port name=\"%s\"/>\n", Port);
	strcat(m_SensorSignal, tmpstring);

	if (m_Target)
	{
		sprintf(tmpstring, "\t<atXml:Target name=\"IR_TARGET\"");
		strcat(m_SensorSignal, tmpstring);

		strcpy(InName, "IR_TARGET");
		strcpy(InString, " In=\"IR_TARGET\"");

		if (m_DistPosCount.Exists)
		{
			sprintf(tmpstring, " distortionPositionCount=\"%d\"", m_DistPosCount.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_DistortionPositions.Exists)
		{
			sprintf(tmpstring, " distortionPositions=\"[");
			strcat(m_SensorSignal, tmpstring);
			sprintf(tmpstring, "%d", m_DistortionPositions.Size/2);
			strcat(m_SensorSignal, tmpstring);
			for (idx = 0; idx < m_DistortionPositions.Size; idx++)
			{
				strcat(m_SensorSignal, ",");
				sprintf(tmpstring, "%.3lf", m_DistortionPositions.val[idx]);
				strcat(m_SensorSignal, tmpstring);
			}
			strcat(m_SensorSignal, "]\"");
		}

		if (m_TargetType.Exists)
		{
			sprintf(tmpstring, " targetType=\"%s\"", TARGETS[m_TargetType.Int]);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_HorizTargetAngle.Exists)
		{
			sprintf(tmpstring, " horizontalTargetAngle=\"%lf rad\"", m_HorizTargetAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_VertTargetAngle.Exists)
		{
			sprintf(tmpstring, " verticalTargetAngle=\"%lf rad\"", m_VertTargetAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_HorizTargetOffset.Exists)
		{
			sprintf(tmpstring, " horizontalTargetOffset=\"%lf rad\"", m_HorizTargetOffset.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_VertTargetOffset.Exists)
		{
			sprintf(tmpstring, " verticalTargetOffset=\"%lf rad\"", m_VertTargetOffset.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateTop.Exists)
		{
			sprintf(tmpstring, " targetCoordinateTop=\"%d\"", m_TgtCoordinateTop.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateLeft.Exists)
		{
			sprintf(tmpstring, " targetCoordinateLeft=\"%d\"", m_TgtCoordinateLeft.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateBottom.Exists)
		{
			sprintf(tmpstring, " targetCoordinateBottom=\"%d\"", m_TgtCoordinateBottom.Int);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_TgtCoordinateRight.Exists)
		{
			sprintf(tmpstring, " targetCoordinateRight=\"%d\"", m_TgtCoordinateRight.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TargetData.Exists)
		{
			sprintf(tmpstring, " targetData=\"[");
			strcat(m_SensorSignal, tmpstring);
			for (idx = 0; idx < m_TargetData.Size; idx++)
			{
				if (idx > 0)
					strcat(m_SensorSignal, ",");
				sprintf(tmpstring, "%.2lf", m_TargetData.val[idx]);
				strcat(m_SensorSignal, tmpstring);
			}
			strcat(m_SensorSignal, "]\"");
		}
		strcat(m_SensorSignal, "/>\n");
	}

	// Boresight
	if (m_Boresight)
	{
		sprintf(tmpstring, "\t<atXml:Boresight name=\"IR_BORESIGHT\"");
		strcat(m_SensorSignal, tmpstring);

		if (strlen(InName) > 1)
		{
			sprintf(tmpstring, " In=\"%s\"", InName);
			strcat(m_SensorSignal, tmpstring);
		}

		strcpy(InName, "IR_BORESIGHT");
		strcpy(InString, " In=\"IR_BORESIGHT\"");

		if (m_XBoresightAngle.Exists)
		{
			sprintf(tmpstring, " xBoresightAngle=\"%lf rad\"", m_XBoresightAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_YBoresightAngle.Exists)
		{
			sprintf(tmpstring, " yBoresightAngle=\"%lf rad\"", m_YBoresightAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_XAutocollimationError.Exists)
		{
			sprintf(tmpstring, " xAutocollimationError=\"%lf rad\"", m_XAutocollimationError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_YAutocollimationError.Exists)
		{
			sprintf(tmpstring, " yAutocollimationError=\"%lf rad\"", m_YAutocollimationError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		if (m_HorizLosAlignError.Exists)
		{
			sprintf(tmpstring, " horizontalLineOfSightAlignmentError=\"%lf rad\"", m_HorizLosAlignError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		if (m_VertLosAlignError.Exists)
		{
			sprintf(tmpstring, " verticalLineOfSightAlignmentError=\"%lf rad\"", m_VertLosAlignError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}


	// Video
	if (m_Video)
	{
		strcat(m_SensorSignal, "\t<atXml:Video name=\"IR_VIDEO\"");
		if (strlen(InName) > 1)
		{
			sprintf(tmpstring, " In=\"%s\"", InName);
			strcat(m_SensorSignal, tmpstring);
		}

		strcpy(InName, "IR_VIDEO");
		strcpy(InString, " In=\"IR_VIDEO\"");

		if (m_Format.Exists)
		{
			sprintf(tmpstring, " videoFormat=\"%s\"", FORMATS[m_Format.Int]);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_HorizFieldOfView.Exists)
		{
			sprintf(tmpstring, " horizontalFieldOfView=\"%lf rad\"", m_HorizFieldOfView.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_VertFieldOfView.Exists)
		{
			sprintf(tmpstring, " verticalFieldOfView=\"%lf rad\"", m_VertFieldOfView.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}

	//Infrared
	sprintf(tmpstring, "\t<atXml:Infrared name=\"IR_SIG\"%s", InString);
	strcat(m_SensorSignal, tmpstring);

	if (m_DifferentialTemp.Exists)
	{
		sprintf(tmpstring, " differentialTemperature=\"%lf degc\"", m_DifferentialTemp.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_DiffTempInterval.Exists)
	{
		sprintf(tmpstring, " differentialTemperatureInterval=\"%lf degc\"", m_DiffTempInterval.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_DiffTempStart.Exists)
	{
		sprintf(tmpstring, " differentialTemperatureStart=\"%lf degc\"", m_DiffTempStart.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_DiffTempStop.Exists)
	{
		sprintf(tmpstring, " differentialTemperatureStop=\"%lf degc\"", m_DiffTempStop.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_NoiseEqDiffTemp.Exists)
	{
		sprintf(tmpstring, " noiseEquivalentDifferentialTemperature=\"%lf degc\"", m_NoiseEqDiffTemp.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_IntensityRatio.Exists)
	{
		sprintf(tmpstring, " intensityRatio=\"%lf pct\"", m_IntensityRatio.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_MtfFreqPoints.Exists)
	{
		sprintf(tmpstring, " mtfFrequencyPoints=\"%d\"", m_MtfFreqPoints.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_MtfDirection.Exists)
	{
		sprintf(tmpstring, " mtfDirection=\"%s\"", DIMS[m_MtfDirection.Int]);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_Filter.Exists)
	{
		sprintf(tmpstring, " filter=\"%d\"", m_Filter.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_WaveLength.Exists)
	{
		sprintf(tmpstring, " waveLength=\"%.3lf nm\"", (m_WaveLength.Real * 1.0e9));
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_LinesPerChannel.Exists)
	{
		sprintf(tmpstring, " linesPerChannel=\"%d\"", m_LinesPerChannel.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_FirstActiveLine.Exists)
	{
		sprintf(tmpstring, " firstActiveLine=\"%d\"", m_FirstActiveLine.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_TestPointCount.Exists)
	{
		sprintf(tmpstring, " testPointCount=\"%d\"", m_TestPointCount.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	strcat(m_SensorSignal, "/>\n");

	// settling time
	if (m_SettleTime.Exists)
	{
		sprintf(tmpstring, "\t<SignalDelay name=\"IR_SETTLE\" delay=\"%lf sec\"/>\n",
			m_SettleTime.Real);
		strcat(m_SensorSignal, tmpstring);
		strcpy(SyncString, " Sync=\"IR_SETTLE\"");
	}

	// Measure
	sprintf(tmpstring, "\t<Measure name=\"IR_MEAS\" As=\"IR_SIG\"%s attribute=\"%s\"",
		SyncString, ATTRIBUTES[Fnc]);
	strcat(m_SensorSignal, tmpstring);

	if (m_SampleCount.Exists)
	{
		sprintf(tmpstring, " samples=\"%.0lf\"", m_SampleCount.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	strcat(m_SensorSignal, "/>\n</Signal>");
	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: BuildVisSensor1641(int Fnc)
//
// Purpose: Generate Atml string for visible light sensors
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             Function number
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::BuildVisSensor1641(int Fnc)
{
	char tmpstring[256] = "";
	char InName[25] = "";
	int idx = 0;
	char InString[25] = "";
	char Port[12] = "";

	SetVisDefaults(Fnc);

	if (Fnc == 38)
		strcpy(Port, "ATMOSPHERE");
	else
		strcpy(Port, "VIDEO");

	sprintf(m_SensorSignal, "<Signal name=\"EOSENSOR\" In=\"%s\" Out=\"VIS_MEAS\">\n", Port);
	sprintf(tmpstring, "\t<atXml:port name=\"%s\"/>\n", Port);
	strcat(m_SensorSignal, tmpstring);

	if (m_Target)
	{
		sprintf(tmpstring, "\t<atXml:Target name=\"VIS_TARGET\"");
		strcat(m_SensorSignal, tmpstring);

		strcpy(InName, "VIS_TARGET");
		strcpy(InString, " In=\"VIS_TARGET\"");

		if (m_DistPosCount.Exists)
		{
			sprintf(tmpstring, "distortionPositionCount=\"%d\"", m_DistPosCount.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_DistortionPositions.Exists)
		{
			sprintf(tmpstring, " distortionPositions=\"[");
			strcat(m_SensorSignal, tmpstring);
			sprintf(tmpstring, "%d", m_DistortionPositions.Size/2);
			for (idx = 0; idx < m_DistortionPositions.Size; idx++)
			{
				strcat(m_SensorSignal, ",");
				sprintf(tmpstring, "%.3lf", m_DistortionPositions.val[idx]);
				strcat(m_SensorSignal, tmpstring);
			}
			strcat(m_SensorSignal, "]\"");
		}

		if (m_TargetType.Exists)
		{
			sprintf(tmpstring, " targetType=\"%s\"", TARGETS[m_TargetType.Int]);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_HorizTargetAngle.Exists)
		{
			sprintf(tmpstring, " horizontalTargetAngle=\"%lf rad\"", m_HorizTargetAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_VertTargetAngle.Exists)
		{
			sprintf(tmpstring, " verticalTargetAngle=\"%lf rad\"", m_VertTargetAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateTop.Exists)
		{
			sprintf(tmpstring, " targetCoordinateTop=\"%d\"", m_TgtCoordinateTop.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateLeft.Exists)
		{
			sprintf(tmpstring, " targetCoordinateLeft=\"%d\"", m_TgtCoordinateLeft.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateBottom.Exists)
		{
			sprintf(tmpstring, " targetCoordinateBottom=\"%d\"", m_TgtCoordinateBottom.Int);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_TgtCoordinateRight.Exists)
		{
			sprintf(tmpstring, " targetCoordinateRight=\"%d\"", m_TgtCoordinateRight.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TargetData.Exists)
		{
			sprintf(tmpstring, " targetData=\"[");
			strcat(m_SensorSignal, tmpstring);
			for (idx = 0; idx < m_TargetData.Size; idx++)
			{
				if (idx > 0)
					strcat(m_SensorSignal, ",");
				sprintf(tmpstring, "%.3lf", m_TargetData.val[idx]);
				strcat(m_SensorSignal, tmpstring);
			}
			strcat(m_SensorSignal, "]\"");
		}
		strcat(m_SensorSignal, "/>\n");
	}

	// Boresight
	if (m_Boresight)
	{
		sprintf(tmpstring, "\t<atXml:Boresight name=\"VIS_BORESIGHT\"");
		strcat(m_SensorSignal, tmpstring);

		if (strlen(InName) > 1)
		{
			sprintf(tmpstring, " In=\"%s\"", InName);
			strcat(m_SensorSignal, tmpstring);
		}

		strcpy(InName, "VIS_BORESIGHT");
		strcpy(InString, " In=\"VIS_BORESIGHT\"");

		if (m_HorizLosAlignError.Exists)
		{
			sprintf(tmpstring, " horizontalLineOfSightAlignmentError=\"%lf rad\"", m_HorizLosAlignError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_VertLosAlignError.Exists)
		{
			sprintf(tmpstring, " verticalLineOfSightAlignmentError=\"%lf rad\"", m_VertLosAlignError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_XAutocollimationError.Exists)
		{
			sprintf(tmpstring, " xAutocollimationError=\"%lf rad\"", m_XAutocollimationError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
	
		if (m_YAutocollimationError.Exists)
		{
			sprintf(tmpstring, " yAutocollimationError=\"%lf rad\"", m_YAutocollimationError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}

	// Video
	if (m_Video)
	{
		strcat(m_SensorSignal, "\t<atXml:Video name=\"VIS_VIDEO\"");
		if (strlen(InName) > 1)
		{
			sprintf(tmpstring, " In=\"%s\"", InName);
			strcat(m_SensorSignal, tmpstring);
		}

		strcpy(InName, "VIS_VIDEO");
		strcpy(InString, " In=\"VIS_VIDEO\"");

		if (m_Format.Exists)
		{
			sprintf(tmpstring, " videoFormat=\"%s\"", FORMATS[m_Format.Int]);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_HorizFieldOfView.Exists)
		{
			sprintf(tmpstring, " horizontalFieldOfView=\"%lf rad\"", m_HorizFieldOfView.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_VertFieldOfView.Exists)
		{
			sprintf(tmpstring, " verticalFieldOfView=\"%lf rad\"", m_VertFieldOfView.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}

	// light
	sprintf(tmpstring, "\t<atXml:Light name=\"VIS_SIG\"%s", InString);
	strcat(m_SensorSignal, tmpstring);

	if (m_Filter.Exists)
	{
		sprintf(tmpstring, " filter=\"%d\"", m_Filter.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_IntensityRatio.Exists)
	{
		sprintf(tmpstring, " intensityRatio=\"%lf pct\"", m_IntensityRatio.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_MtfDirection.Exists)
	{
		sprintf(tmpstring, " mtfDirection=\"%s\"", DIMS[m_MtfDirection.Int]);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_MtfFreqPoints.Exists)
	{
		sprintf(tmpstring, " mtfFrequencyPoints=\"%d\"", m_MtfFreqPoints.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_Radiance.Exists)
	{
		sprintf(tmpstring, " radiance=\"%lf uw/cm2-sr\"", m_Radiance.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_RadianceInterval.Exists)
	{
		sprintf(tmpstring, " radianceInterval=\"%lf uw/cm2-sr\"", m_RadianceInterval.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_RadianceStart.Exists)
	{
		sprintf(tmpstring, " radianceStart=\"%lf uw/cm2-sr\"", m_RadianceStart.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_RadianceStop.Exists)
	{
		sprintf(tmpstring, " radianceStop=\"%lf uw/cm2-sr\"", m_RadianceStop.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_TestPointCount.Exists)
	{
		sprintf(tmpstring, " testPointCount=\"%d\"", m_TestPointCount.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	strcat(m_SensorSignal, "/>\n");

	sprintf(tmpstring, "\t<Measure name=\"VIS_MEAS\" As=\"VIS_SIG\" attribute=\"%s\"",
		ATTRIBUTES[Fnc]);
	strcat(m_SensorSignal, tmpstring);

	if (m_SampleCount.Exists)
	{
		sprintf(tmpstring, " samples=\"%.0lf\"", m_SampleCount.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	strcat(m_SensorSignal, "/>\n</Signal>");
	return;
	
}

////////////////////////////////////////////////////////////////////////////////
// Function: BuildLaserSensor1641(int Fnc)
//
// Purpose: Generate Atml string for laser sensors
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             Function number
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::BuildLaserSensor1641(int Fnc)
{
	char tmpstring[256] = "";
	char InName[25] = "";
	char Port[12] = "";
	char InString[25] = "";
	char SyncString[25]= "";

	SetLaserDefaults(Fnc);

	switch (Fnc) {
		case PULSE_ENERGY_CH1_LASER : case PULSE_ENERGY_STAB_CH1_LASER :
		case POWER_P_CH1_LASER :      case PULSE_AMPL_STAB_CH1_LASER :
		case PULSE_WIDTH_CH1_LASER :
			strcpy(Port, "SCOPE_CH1");
			break;
		case PULSE_ENERGY_CH2_LASER	: case PULSE_ENERGY_STAB_CH2_LASER :
		case POWER_P_CH2_LASER :      case PULSE_AMPL_STAB_CH2_LASER :
		case PULSE_WIDTH_CH2_LASER :
			strcpy(Port, "SCOPE_CH2");
			break;
		case PRF_LASER : case PULSE_PERIOD_STAB_LASER :
			strcpy(Port, "CTR");
			break;
		case DIFF_BORESIGHT_ANGLE_LASER : case DIVERGENCE_LASER :
		case BORESIGHT_ANGLE_LASER :
			strcpy(Port, "VIA");
			break;
		case RECEIVER_SENSITIVITY_LASER : case RANGE_ERROR_LASER :
			strcpy(Port, "ATMOSPHERE");
			break;
	}

	sprintf(m_SensorSignal, "<Signal name=\"EOSIGNAL\" Out=\"LASER_MEAS\" In=\"%s %s\">\n",
		Port, DIMS[m_TriggerMode.Int]);

	sprintf(tmpstring, "\t<atXml:Port name=\"%s\"/>\n", Port);
	strcat(m_SensorSignal, tmpstring);
	if (m_TriggerMode.Exists)
	{
		sprintf(tmpstring, "\t<atXml:Port name=\"%s\"/>\n", DIMS[m_TriggerMode.Int]);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_Target)
	{
		sprintf(tmpstring, "\t<atXml:Target name=\"LASER_TARGET\"");
		strcat(m_SensorSignal, tmpstring);

		strcpy(InName, "LASER_TARGET");
		strcpy(InString, " In=\"LASER_TARGET\"");

		if (m_TargetRange.Exists)
		{
			sprintf(tmpstring, " targetRange=\"%lf m\"", m_TargetRange.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_LastPulseRange.Exists)
		{
			sprintf(tmpstring, " lastPulseRange=\"%lf m\"", m_LastPulseRange.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_RangeError.Exists)
		{
			sprintf(tmpstring, " rangeError=\"%lf m\"", m_RangeError.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateTop.Exists)
		{
			sprintf(tmpstring, " targetCoordinateTop=\"%d\"", m_TgtCoordinateTop.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateLeft.Exists)
		{
			sprintf(tmpstring, " targetCoordinateLeft=\"%d\"", m_TgtCoordinateLeft.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateBottom.Exists)
		{
			sprintf(tmpstring, " targetCoordinateBottom=\"%d\"", m_TgtCoordinateBottom.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TgtCoordinateRight.Exists)
		{
			sprintf(tmpstring, " targetCoordinateRight=\"%d\"", m_TgtCoordinateRight.Int);
			strcat(m_SensorSignal, tmpstring);
		}

		strcat(m_SensorSignal, "/>\n");
	}


	if (m_Boresight)
	{
		strcat(m_SensorSignal, "\t<atXml:Boresight name=\"LASER_BORESIGHT\"");

		if (strlen(InName) > 1)
		{
			sprintf(tmpstring, " In=\"%s\"", InName);
			strcat(m_SensorSignal, tmpstring);
		}

		strcpy(InName, "LASER_BORESIGHT");
		strcpy(InString, " In=\"LASER_BORESIGHT\"");

		if (m_XBoresightAngle.Exists)
		{
			sprintf(tmpstring, " xBoresightAngle=\"%lf rad\"", m_XBoresightAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_YBoresightAngle.Exists)
		{
			sprintf(tmpstring, " yBoresightAngle=\"%lf rad\"", m_YBoresightAngle.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_XAutocollimationError.Exists)
		{
			sprintf(tmpstring, " xAutocollimationError=\"%lf rad\"", m_XAutocollimationError.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_YAutocollimationError.Exists)
		{
			sprintf(tmpstring, " yAutocollimationError=\"%lf rad\"", m_YAutocollimationError.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_HorizLosAlignError.Exists)
		{
			sprintf(tmpstring, " horizontalLineOfSightAlignmentError=\"%lf rad\"", m_HorizLosAlignError.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_VertLosAlignError.Exists)
		{
			sprintf(tmpstring, " verticalLineOfSightAlignmentError=\"%lf rad\"", m_VertLosAlignError.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}

	if (m_PulseWidth.Exists || m_SampleTime.Exists)
	{
		sprintf(tmpstring, "\t<Trapezoid name=\"LASER_PULSE\" %s", InString);
		strcat(m_SensorSignal, tmpstring);
		strcpy(InString, " In=\"LASER_PULSE\"");

		if(m_PulseWidth.Exists)
		{
			sprintf(tmpstring, " pulseWidth=\"%e sec\"", m_PulseWidth.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		if(m_SampleTime.Exists)
		{
			sprintf(tmpstring, "timeBase=\"%e sec\"", m_SampleTime.Real);
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}

	if (m_Trigger)
	{
		strcat(m_SensorSignal, "\t<Instantaneous name=\"LASER_TRIG\"");

		if (m_TriggerMode.Exists)
		{
			sprintf(tmpstring, " In=\"%s\"", DIMS[m_TriggerMode.Int]);
			strcat(m_SensorSignal, tmpstring);
		}
		strcpy(SyncString, " Sync=\"LASER_TRIG\"");

		if (m_TrigLevel.Exists)
		{
			sprintf(tmpstring, " nominal=\"%lf v\"", m_TrigLevel.Real);
			strcat(m_SensorSignal, tmpstring);
		}

		if (m_TriggerSlope.Exists)
		{
			sprintf(tmpstring, " condition=\"%s\"", DIMS[m_TriggerSlope.Int]);
			strcat(m_SensorSignal, tmpstring);
		}
		else
		{
			sprintf(tmpstring, " condition=\"GT\"");
			strcat(m_SensorSignal, tmpstring);
		}
		strcat(m_SensorSignal, "/>\n");
	}
	sprintf(tmpstring, "\t<atXml:Laser name=\"LASER_SIG\"%s", InString);
	strcat(m_SensorSignal, tmpstring);

	if (m_IntensityRatio.Exists)
	{
		sprintf(tmpstring, " intensityRatio=\"%lf pct\"", m_IntensityRatio.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_MainBeamAtten.Exists)
	{
		sprintf(tmpstring, " mainBeamAttenuation=\"%lf od\"", m_MainBeamAtten.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_TestPointCount.Exists)
	{
		sprintf(tmpstring, " testPointCount=\"%d\"", m_TestPointCount.Int);
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_WaveLength.Exists)
	{
		sprintf(tmpstring, " waveLength=\"%.3lf nm\"", (m_WaveLength.Real * 1.0e9));
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_PowerDensity.Exists)
	{
		sprintf(tmpstring, " powerDensity=\"%e w/cm2\"", (m_PowerDensity.Real));
		strcat(m_SensorSignal, tmpstring);
	}

	if (m_PulseEnergy.Exists)
	{
		sprintf(tmpstring, " pulseEnergy=\"%lf j\"", (m_PulseEnergy.Real));
		strcat(m_SensorSignal, tmpstring);
	}

	strcat(m_SensorSignal, "/>\n");

	if (m_Delay.Exists)
	{
		strcat(m_SensorSignal, "\t<SignalDelay name=\"LASER_DELAY\"");
		if (strlen(SyncString) > 1)
		{
			strcpy(tmpstring, " In=\"LASER_TRIG\"");
			strcat(m_SensorSignal, tmpstring);
		}
		sprintf(tmpstring, " delay=\"%e sec\"/>\n", m_Delay.Real);
		strcat(m_SensorSignal, tmpstring);

		strcpy(SyncString, " Sync=\"LASER_DELAY\"");
	}

	sprintf(tmpstring, "\t<Measure name=\"LASER_MEAS\" As=\"LASER_SIG\"%s attribute=\"%s\"", SyncString, ATTRIBUTES[Fnc]);
	strcat(m_SensorSignal, tmpstring);

	if (m_SampleCount.Exists)
	{
		sprintf(tmpstring, " samples=\"%.0lf\"", m_SampleCount.Real);
		strcat(m_SensorSignal, tmpstring);
	}

	strcat(m_SensorSignal, "/>\n</Signal>");
	return;
}

////////////////////////////////////////////////////////////////////////////////
// Function: BuildSource1641(int Fnc)
//
// Purpose: Generate Atml string for Veo2 sources
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
// Fnc              int             Function number
//
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ============================================
//
// Return: void
//
////////////////////////////////////////////////////////////////////////////////
void CVeo2_T::BuildSource1641(int Fnc)
{
	char tmpstring[256] = "";
	char InName[50] = "";

	SetSourceDefaults(Fnc);

	switch (Fnc)
	{
		case SOURCE_INFRARED :
			// setup signal name
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"IR_SOURCE\"");
			if (m_SettleTime.Exists)
			{
				sprintf(tmpstring, " Sync=\"IR_DELAY\"");
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, ">\n");

			// define infrared element
			strcat(m_SourceSignal, "\t<atXml:Infrared name=\"IR_SOURCE\"");

			if (m_TargetType.Exists)
			{
				strcat(m_SourceSignal, " In=\"EO_TARGET\"");
			}
			if (m_DifferentialTemp.Exists)
			{
				sprintf(tmpstring, " differentialTemperature=\"%.3lf degc\"", m_DifferentialTemp.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_DiffTempError.Exists)
			{
				sprintf(tmpstring, " differentialTemperatureError=\"%.3lf degc\"", m_DiffTempError.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_MaxTime.Exists)
			{
				sprintf(tmpstring, " settleTime=\"%.2lf sec\"", m_MaxTime.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "/>\n");
			// define target
			if (m_TargetType.Exists)
			{
				sprintf(tmpstring, "\t<atXml:Target name=\"EO_TARGET\" targetType=\"%s\"/>\n", TARGETS[m_TargetType.Int]);
				strcat(m_SourceSignal, tmpstring);
			}

			strcat(m_SourceSignal, "</Signal>");

			break;
		case SOURCE_MULTI_SENSOR_INFRARED :
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"IR_SOURCE\"");
			if (m_SettleTime.Exists)
			{
				sprintf(tmpstring, " Sync=\"IR_DELAY\"");
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, ">\n");

			// define multi-sensor infrared
			strcpy(tmpstring, "\t<atXml:MultiSensorInfrared name=\"IR_SOURCE\"");
			strcat(m_SourceSignal, tmpstring);

			if (m_TargetType.Exists)
			{
				strcat(m_SourceSignal, " In=\"EO_TARGET\"");
			}

			strcat(m_SourceSignal, "/>\n");

			if (m_TargetType.Exists)
			{
				sprintf(tmpstring, "\t<atXml:Target name=\"EO_TARGET\" targetType=\"%s\"/>\n",	TARGETS[m_TargetType.Int]);
				strcat(m_SourceSignal, tmpstring);
			}

			if (m_SettleTime.Exists)
			{
				sprintf(tmpstring, "\t<SignalDelay name=\"IR_DELAY\" delay=\"%lf sec\"/>\n", m_SettleTime.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "</Signal>");

			break;
		case SOURCE_LIGHT :
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"LIGHT_SOURCE\">\n");

			strcat(m_SourceSignal, "\t<atXml:Light name=\"LIGHT_SOURCE\"");
			if ((m_TargetType.Exists))
			{
				sprintf(tmpstring, " In=\"EO_TARGET\"", InName);
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_Radiance.Exists)
			{
				sprintf(tmpstring, " radiance=\"%lf uw/cm2-sr\"", m_Radiance.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_MaxTime.Exists)
			{
				sprintf(tmpstring, " settleTime=\"%lf sec\"", m_MaxTime.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "/>\n");

			if ((m_TargetType.Exists))
			{
				strcat(m_SourceSignal, "\t<atXml:Target name=\"EO_TARGET\"");
				if (m_TargetType.Exists)
				{
					sprintf(tmpstring, " targetType=\"%s\"", TARGETS[m_TargetType.Int]);
					strcat(m_SourceSignal, tmpstring);
				}
				strcat(m_SourceSignal, "/>\n");
			}

			strcat(m_SourceSignal, "</Signal>\n");

			break;
		case SOURCE_MULTI_SENSOR_LIGHT :
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"LIGHT_SOURCE\">\n");

			sprintf(tmpstring, "\t<atXml:MultiSensorLight name=\"LIGHT_SOURCE\"");
			strcat(m_SourceSignal, tmpstring);
			if (m_TargetType.Exists)
			{
				sprintf(tmpstring, " In=\"EO_TARGET\"", InName);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "/>\n");
			if (m_TargetType.Exists)
			{
				sprintf(tmpstring, "\t<atXml:Target name=\"EO_TARGET\" targetType=\"%s\"/>\n", TARGETS[m_TargetType.Int]);
				strcat(m_SourceSignal, tmpstring);
			}

			strcat(m_SourceSignal, "</Signal>");
			break;
		case SOURCE_LASER :
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"LASER_SOURCE\"");
			if (m_TriggerMode.Exists)
			{
				sprintf(tmpstring, " In=\"%s\"", DIMS[m_TriggerMode.Int]);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, ">\n");
			if (m_TriggerMode.Exists)
			{
				sprintf(tmpstring, "\t<atXml:port name=\"%s\"/>\n", DIMS[m_TriggerMode.Int]);
				strcat(m_SourceSignal, tmpstring);
			}

			// source laser
			strcat(m_SourceSignal, "\t<atXml:Laser name=\"LASER_SOURCE\" In=\"LASER_PULSE\"");
			if (m_WaveLength.Exists)
			{
				sprintf(tmpstring, " waveLength=\"%.3lf nm\"", (m_WaveLength.Real * 1.0e9));
				strcat(m_SourceSignal, tmpstring);
			}
			else
			{
				CEMERROR(EB_SEVERITY_ERROR, "Missing required modifier WAVE-LENGTH");
			}

			if (m_MaxTime.Exists)
			{
				sprintf(tmpstring, " settleTime=\"%lf sec\"", m_MaxTime.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "/>\n");

			sprintf(tmpstring, "\t<Trapezoid name=\"LASER_PULSE\"");
			strcat(m_SourceSignal, tmpstring);

			// Added period | prf for STR 9677
			if (m_Period.Exists)
			{
				sprintf(tmpstring, " period=\"%e sec\"", m_Period.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			else if (m_Prf.Exists)
			{
				sprintf(tmpstring, " period=\"%e sec\"", (1.0/m_Prf.Real));
				strcat(m_SourceSignal, tmpstring);
			}
			else
			{
				CEMERROR(EB_SEVERITY_ERROR, "Missing required modifier PERIOD or PRF");
			}

			// Added PowerP for STR 9677
			if (m_PowerP.Exists)
			{
				sprintf(tmpstring, " amplitude=\"%lf nw\"", (m_PowerP.Real * 1.0e9));
				strcat(m_SourceSignal, tmpstring);
			}
			else 
			{
				CEMERROR(EB_SEVERITY_ERROR, "Missing required modifier POWER-P");
			}

			strcat(m_SourceSignal, "/>\n");
			strcat(m_SourceSignal, "</Signal>");
			break;
		case SOURCE_LASER_TARGET_RETURN :
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"LASER_SOURCE\"");
			if (m_TriggerMode.Exists)
			{
				sprintf(tmpstring, " In=\"%s\">\n", DIMS[m_TriggerMode.Int]);
				strcat(m_SourceSignal, tmpstring);
				sprintf(tmpstring, "\t<atXml:port name=\"%s\"/>\n", DIMS[m_TriggerMode.Int]);
				strcat(m_SourceSignal, tmpstring);
			}
			else
			{
				strcat(m_SourceSignal, ">\n");
			}

			// Laser target return data
			sprintf(tmpstring, "\t<atXml:LaserTargetReturn name=\"LASER_SOURCE\" In=\"LASER_PULSE\"");
			strcat(m_SourceSignal, tmpstring);
			if (m_WaveLength.Exists)
			{
				sprintf(tmpstring, " waveLength=\"%.3lf nm\"", (m_WaveLength.Real * 1.0e9));
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_PowerDensity.Exists)
			{
				sprintf(tmpstring, " powerDensity=\"%e w/cm2\"", m_PowerDensity.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_MaxTime.Exists)
			{
				sprintf(tmpstring, " settleTime=\"%lf sec\"", m_MaxTime.Real);
				strcat(m_SourceSignal, tmpstring);
			}

			strcat(m_SourceSignal, "/>\n");

			// laser pulse pulse width and period
			sprintf(tmpstring, "\t<Trapezoid name=\"LASER_PULSE\" In=\"LASER_TARGET\"");
			strcat(m_SourceSignal, tmpstring);
			if (m_PulseWidth.Exists)
			{
				sprintf(tmpstring, " pulseWidth=\"%e sec\"", m_PulseWidth.Real);
				strcat(m_SourceSignal, tmpstring);
			}

			if (m_PowerP.Exists)
			{
				sprintf(tmpstring, " amplitude=\"%lf nw\"", (m_PowerP.Real * 1.0e9));
				strcat(m_SourceSignal, tmpstring);
			}

			if (m_Period.Exists)
			{
				sprintf(tmpstring, " period=\"%e sec\"", m_Period.Real);
				strcat(m_SourceSignal, tmpstring);
			}

			strcat(m_SourceSignal, "/>\n");

			sprintf(tmpstring, "\t<atXml:Target name=\"LASER_TARGET\"");
			strcat(m_SourceSignal, tmpstring);
			// laser target
			if (m_TargetRange.Exists)
			{
				sprintf(tmpstring, " targetRange=\"%.2lf m\"", m_TargetRange.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			if (m_LastPulseRange.Exists)
			{
				sprintf(tmpstring, " lastPulseRange=\"%.2lf m\"", m_LastPulseRange.Real);
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "/>\n");

			strcat(m_SourceSignal, "</Signal>\n");
			break;
		case SOURCE_LARRS :
			sprintf(m_SourceSignal, "<Signal name=\"EOSIGNAL\" Out=\"LARRS_SOURCE\"");
			strcat(m_SourceSignal, ">\n");

			strcat(m_SourceSignal, "\t<atXml:Larrs name=\"LARRS_SOURCE\"");
			sprintf(tmpstring, "\t<atXml:XYZPosition name=\"LARRS_CORD\"");
			if (m_Azimuth.Exists)
			{
				sprintf(tmpstring, " azimuth=\"%d\"", (m_Azimuth.Int));
				strcat(m_SourceSignal, tmpstring);
			}

			if (m_Elevation.Exists)
			{
				sprintf(tmpstring, " elevation=\"%d\"", (m_Elevation.Int));
				strcat(m_SourceSignal, tmpstring);
			}if (m_Polarize.Exists)
			{
				sprintf(tmpstring, " polarize=\"%d deg\"", (m_Polarize.Int));
				strcat(m_SourceSignal, tmpstring);
			}
			strcat(m_SourceSignal, "/>\n");

			strcat(m_SourceSignal, "</Signal>");
			break;
	}

	return;
	
}

void CVeo2_T::SetIRDefaults(int Fnc)
{
	switch (Fnc)
	{
		case DISTORTION_IR :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			if (!m_DistortionPositions.Exists)
			{
				CEMERROR(EB_SEVERITY_WARNING, "Missing required modifier DISTORTION-POSITIONS");
			}
			break;
		case MIN_RESOLV_TEMP_DIFF_IR :
			if (m_TestPointCount.Exists == false)
			{
				m_TestPointCount.Exists = true;
				m_TestPointCount.Int = 1;
			}
			break;
		case NOISE_EQ_DIFF_TEMP_IR :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			if (m_HorizTargetOffset.Exists == false)
			{
				m_HorizTargetOffset.Exists = true;
				m_HorizTargetOffset.Real = 0.0;
			}
			if (m_VertTargetOffset.Exists == false)
			{
				m_VertTargetOffset.Exists = true;
				m_VertTargetOffset.Real = 0.0;
			}
			break;
		case UNIFORMITY_IR :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			break;
		case DIFFERENTIAL_TEMP_IR :
		case AMBIENT_TEMP_IR :
		case BLACKBODY_TEMP_IR :
			if (m_MaxTime.Exists == false)
			{
				m_MaxTime.Exists = true;
				m_MaxTime.Real = 10.0;
			}
			break;
		case LOS_ALIGN_ERROR_IR :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_SettleTime.Exists == false)
			{
				m_SettleTime.Exists = true;
				m_SettleTime.Real = 60.0;
			}
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			if (m_XAutocollimationError.Exists == false)
			{
				m_XAutocollimationError.Exists = true;
				m_XAutocollimationError.Real = 0.0;
			}
			if (m_YAutocollimationError.Exists == false)
			{
				m_YAutocollimationError.Exists = true;
				m_YAutocollimationError.Real = 0.0;
			}
			break;
		case MODULATION_TRANSFER_FUNCTION_IR :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_Filter.Exists == false)
			{
				m_Filter.Exists = true;
				m_Filter.Int = 0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			if (m_MtfDirection.Exists == false)
			{
				m_MtfDirection.Exists = true;
				m_MtfDirection.Int = DIM_HORIZ;
			}
			if (m_TargetType.Exists == false)
			{
				m_TargetType.Exists = true;
				m_TargetType.Int = DIM_TARG_PIESECTOR;
			}
			break;
		case CHAN_INTEGRITY_IR :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_NoiseEqDiffTemp.Exists == false)
			{
				m_NoiseEqDiffTemp.Exists = true;
				m_NoiseEqDiffTemp.Real = 0.0;
			}
			if (m_LinesPerChannel.Exists == false)
			{
				m_LinesPerChannel.Exists = true;
				m_LinesPerChannel.Int = 1;
			}
			if (m_FirstActiveLine.Exists == false)
			{
				m_FirstActiveLine.Exists = true;
				m_FirstActiveLine.Int = 0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			break;
		case DIFF_BORESIGHT_ANGLE_IR :
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			if (m_SettleTime.Exists == false)
			{
				m_SettleTime.Exists = true;
				m_SettleTime.Real = 60.0;
			}
			//if ((!m_XBoresightAngle.Exists) && (!m_YBoresightAngle.Exists) && (!m_HorizLosAlignError.Exists) &&	(!m_VertLosAlignError.Exists))
			if (!((m_XBoresightAngle.Exists    && m_YBoresightAngle.Exists) || (m_HorizLosAlignError.Exists && m_VertLosAlignError.Exists)))
			{
				CEMERROR(EB_SEVERITY_WARNING, "Missing required modifier X/Y-BORESIGHT-ANGLE or H/V-LOS-ALIGN-ERROR");
			}
			break;
		default :
			break;
	}
	return;
}

void CVeo2_T::SetVisDefaults(int Fnc)
{
	switch (Fnc)
	{
		case LOS_ALIGN_ERROR_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			if (m_XAutocollimationError.Exists == false)
			{
				m_XAutocollimationError.Exists = true;
				m_XAutocollimationError.Real = 0.0;
			}
			if (m_YAutocollimationError.Exists == false)
			{
				m_YAutocollimationError.Exists = true;
				m_YAutocollimationError.Real = 0.0;
			}
			break;
		case MODULATION_TRANSFER_FUNCTION_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_Filter.Exists == false)
			{
				m_Filter.Exists = true;
				m_Filter.Int = 0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			if (m_MtfDirection.Exists == false)
			{
				m_MtfDirection.Exists = true;
				m_MtfDirection.Int = DIM_HORIZ;
			}
			// Update 20110203 per DME Target Wheel update. Targets may now be specifified,
			// default is pie sector
			if (m_TargetType.Exists == false)
			{
				m_TargetType.Exists = true;
				m_TargetType.Int = DIM_TARG_PIESECTOR;
			}
			break;
		case DISTORTION_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			if (!m_DistortionPositions.Exists)
			{
				CEMERROR(EB_SEVERITY_WARNING, "Missing required modifier DIST-POS-COUNT");
			}
			if (!m_DistortionPositions.Exists)
			{
				CEMERROR(EB_SEVERITY_WARNING, "Missing required modifier DISTORTION-POSITIONS");
			}
			break;
		case MIN_RESOLV_CONTRAST_VIS :
			if (m_TestPointCount.Exists == false)
			{
				m_TestPointCount.Exists = true;
				m_TestPointCount.Int = 1;
			}
			break;
		case UNIFORMITY_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_Radiance.Exists == false)
			{
				m_Radiance.Exists = true;
				m_Radiance.Real = 0.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			// Update 20110203 per DME Target Wheel update. Targets may now be specifified,
			// default is pie sector
			if (m_TargetType.Exists == false)
			{
				m_TargetType.Exists = true;
				m_TargetType.Int = DIM_TARG_PIESECTOR;
			}
			break;
		case CAMERA_GAIN_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			break;
		case DYNAMIC_RANGE_VIS :
		case GRAY_SCALE_RESOLUTION_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_HorizLosAlignError.Exists == false)
			{
				m_HorizLosAlignError.Exists = true;
				m_HorizLosAlignError.Real = 0.0;
			}
			if (m_VertLosAlignError.Exists == false)
			{
				m_VertLosAlignError.Exists = true;
				m_VertLosAlignError.Real = 0.0;
			}
			break;
		case DIFF_BORESIGHT_ANGLE_VIS :
			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			//if ((!m_XBoresightAngle.Exists) && 	(!m_YBoresightAngle.Exists) && 	(!m_HorizLosAlignError.Exists) && (!m_VertLosAlignError.Exists))
			if (!((m_XBoresightAngle.Exists    && m_YBoresightAngle.Exists) || (m_HorizLosAlignError.Exists && m_VertLosAlignError.Exists)))
			{
				CEMERROR(EB_SEVERITY_WARNING, "Missing required modifier X/Y-BORESIGHT-ANGLE or H/V-LOS-ALIGN-ERROR");
			}
			break;
		default :
			break;
	}
	return;
}

void CVeo2_T::SetLaserDefaults(int Fnc)
{
	switch (Fnc)
	{
		case PULSE_ENERGY_CH1_LASER :
		case PULSE_ENERGY_CH2_LASER :
		case PULSE_ENERGY_STAB_CH1_LASER :
		case PULSE_ENERGY_STAB_CH2_LASER :
		case POWER_P_CH1_LASER :
		case POWER_P_CH2_LASER :
		case PULSE_AMPL_STAB_CH1_LASER :
		case PULSE_AMPL_STAB_CH2_LASER :
			if (m_MainBeamAtten.Exists == false)
			{
				m_MainBeamAtten.Exists = true;
				m_MainBeamAtten.Real = 0.0;
			}
			if (m_TrigLevel.Exists == false)
			{
				m_TrigLevel.Exists = true;
				m_TrigLevel.Real = 0.0;
			}
			if (m_TriggerMode.Exists == false)
			{
				m_TriggerMode.Exists = true;
				m_TriggerMode.Int = DIM_INT;
			}
			if (m_TriggerSlope.Exists == false)
			{
				m_TriggerSlope.Exists = true;
				m_TriggerSlope.Int = DIM_POS;
			}
			if (m_SampleTime.Exists == false)
			{
				m_SampleTime.Exists = true;
				m_SampleTime.Real = 0.5e-6;
			}
			break;
		case PULSE_WIDTH_CH1_LASER :
		case PULSE_WIDTH_CH2_LASER :
			if (m_MainBeamAtten.Exists == false)
			{
				m_MainBeamAtten.Exists = true;
				m_MainBeamAtten.Real = 0.0;
			}
			if (m_TrigLevel.Exists == false)
			{
				m_TrigLevel.Exists = true;
				m_TrigLevel.Real = 0.0;
			}
			if (m_TriggerMode.Exists == false)
			{
				m_TriggerMode.Exists = true;
				m_TriggerMode.Int = DIM_INT;
			}
			if (m_TriggerSlope.Exists == false)
			{
				m_TriggerSlope.Exists = true;
				m_TriggerSlope.Int = DIM_POS;
			}
			break;
		case PRF_LASER :
		case PULSE_PERIOD_STAB_LASER :
			if (m_Delay.Exists == false)
			{
				m_Delay.Exists = true;
				m_Delay.Real = 0.0;
			}
			if (m_MainBeamAtten.Exists == false)
			{
				m_MainBeamAtten.Exists = true;
				m_MainBeamAtten.Real = 0.0;
			}
			break;
		case DIFF_BORESIGHT_ANGLE_LASER :
			if (m_SampleTime.Exists == false)
			{
				m_SampleTime.Exists = true;
				m_SampleTime.Real = 33.1e-3;
			}
			if (m_Delay.Exists == false)
			{
				m_Delay.Exists = true;
				m_Delay.Real = 0.0;
			}
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			if (m_TriggerMode.Exists == false)
			{
				m_TriggerMode.Exists = true;
				m_TriggerMode.Int = DIM_INT;
			}
			//if ((!m_XBoresightAngle.Exists) && (!m_YBoresightAngle.Exists) && (!m_HorizLosAlignError.Exists) && (!m_VertLosAlignError.Exists))
			if (!((m_XBoresightAngle.Exists    && m_YBoresightAngle.Exists) || (m_HorizLosAlignError.Exists && m_VertLosAlignError.Exists)))
			{
				CEMERROR(EB_SEVERITY_WARNING, "Missing required modifier X/Y-BORESIGHT-ANGLE or H/V-LOS-ALIGN-ERROR");
			}
			break;
		case DIVERGENCE_LASER :
			if (m_SampleTime.Exists == false)
			{
				m_SampleTime.Exists = true;
				m_SampleTime.Real = 33.1e-3;
			}
			if (m_Delay.Exists == false)
			{
				m_Delay.Exists = true;
				m_Delay.Real = 0.0;
			}
			if (m_TriggerMode.Exists == false)
			{
				m_TriggerMode.Exists = true;
				m_TriggerMode.Int = DIM_INT;
			}
			break;
		case BORESIGHT_ANGLE_LASER :
			if (m_IntensityRatio.Exists == false)
			{
				m_IntensityRatio.Exists = true;
				m_IntensityRatio.Real = 0.0;
			}
			if (m_XAutocollimationError.Exists == false)
			{
				m_XAutocollimationError.Exists = true;
				m_XAutocollimationError.Real = 0.0;
			}
			if (m_YAutocollimationError.Exists == false)
			{
				m_YAutocollimationError.Exists = true;
				m_YAutocollimationError.Real = 0.0;
			}
			if (m_SampleTime.Exists == false)
			{
				m_SampleTime.Exists = true;
				m_SampleTime.Real = 33.1e-3;
			}
			if (m_Delay.Exists == false)
			{
				m_Delay.Exists = true;
				m_Delay.Real = 0.0;
			}
			if (m_TriggerMode.Exists == false)
			{
				m_TriggerMode.Exists = true;
				m_TriggerMode.Int = DIM_INT;
			}
			break;
		case RECEIVER_SENSITIVITY_LASER :
		case RANGE_ERROR_LASER :
			if (m_WaveLength.Exists == false)
			{
				m_WaveLength.Exists = true;
				m_WaveLength.Real = 1.064e-6;
			}
			if (m_PulseWidth.Exists == false)
			{
				m_PulseWidth.Exists = true;
				m_PulseWidth.Real = 20.0e-9;
			}
			break;
		default :
			break;
	}

	switch (Fnc)
	{
		case PULSE_ENERGY_STAB_CH1_LASER :
		case PULSE_ENERGY_STAB_CH2_LASER :
		case PULSE_AMPL_STAB_CH1_LASER :
		case PULSE_AMPL_STAB_CH2_LASER :
		case PULSE_PERIOD_STAB_LASER :

			if (m_SampleCount.Exists == false)
			{
				CEMERROR(EB_SEVERITY_ERROR, "Missing required modifier Sample-Count");
			}
			if (m_PulseWidth.Exists == false && m_SampleTime.Exists == false)
			{
				CEMERROR(EB_SEVERITY_ERROR, "Missing required modifier Pulse-Width or Sample-Time");
			}

			break;

		case PRF_LASER :
		case DIVERGENCE_LASER :
		case POWER_P_CH1_LASER :
		case POWER_P_CH2_LASER :
		case PULSE_WIDTH_CH1_LASER :
		case PULSE_WIDTH_CH2_LASER :
		case BORESIGHT_ANGLE_LASER :
		case PULSE_ENERGY_CH1_LASER :
		case PULSE_ENERGY_CH2_LASER :
		case DIFF_BORESIGHT_ANGLE_LASER :

			if (m_SampleCount.Exists == false)
			{
				m_SampleCount.Exists = true;
				m_SampleCount.Real = 1.0;
			}

			break;

		default :
			break;
	}

	if (Fnc == POWER_P_CH1_LASER || Fnc == POWER_P_CH2_LASER)
	{
		if (m_PulseWidth.Exists == false)
		{
			CEMERROR(EB_SEVERITY_ERROR, "Missing required modifier Pulse-Width");
		}
	}

	return;
}

void CVeo2_T::SetSourceDefaults(int Fnc)
{
	switch (Fnc)
	{
	case SOURCE_INFRARED :
		if (m_MaxTime.Exists == false)
		{
			m_MaxTime.Exists = true;
			m_MaxTime.Real = 10.0;
		}
		if (m_TargetType.Exists == false)
		{
			m_TargetType.Exists = true;
			m_TargetType.Int = DIM_TARG_OPNAPR;
		}
		break;
		case SOURCE_MULTI_SENSOR_LIGHT :
			if (m_MaxTime.Exists == false)
			{
				m_MaxTime.Exists = true;
				m_MaxTime.Real = 0.0;
			}
			if (m_TargetType.Exists == false)
			{
				m_TargetType.Exists = true;
				m_TargetType.Int = DIM_TARG_TVBS15;
			}
			break;
	case SOURCE_MULTI_SENSOR_INFRARED :
		if (m_SettleTime.Exists == false)
		{
			m_SettleTime.Exists = true;
			m_SettleTime.Real = 60.0;
		}
		if (m_TargetType.Exists == false)
		{
			m_TargetType.Exists = true;
			m_TargetType.Int = DIM_TARG_IRBS01;
		}
		break;
	case SOURCE_LIGHT :
		if (m_MaxTime.Exists == false)
		{
			m_MaxTime.Exists = true;
			m_MaxTime.Real = 10.0;
		}
		break;
	case SOURCE_LASER :
		if (m_MaxTime.Exists == false)
		{
			m_MaxTime.Exists = true;
			m_MaxTime.Real = 10.0;
		}

		if (m_WaveLength.Exists == false)
		{
			m_WaveLength.Exists = true;
			m_WaveLength.Real = 1.064e-6;
		}
		break;
	case SOURCE_LASER_TARGET_RETURN :
		if (m_Period.Exists == false)
		{
			m_Period.Exists = true;
			m_Period.Real = 0.050;
		}
		if (m_PulseWidth.Exists == false)
		{
			m_PulseWidth.Exists = true;
			m_PulseWidth.Real = 20.0e-9;
		}
		if (m_WaveLength.Exists == false)
		{
			m_WaveLength.Exists = true;
			m_WaveLength.Real = 1.064e-6;
		}
		if (m_TriggerMode.Exists == false)
		{
			m_TriggerMode.Exists = true;
			m_TriggerMode.Int = DIM_LASR;
		}
		break;
	case SOURCE_LARRS :
		if (m_Polarize.Exists == false)
		{
			m_Polarize.Exists = true;
			m_Polarize.Int = 0;
		}
		break;
	default :
		break;
	}
	return;
}
//++++//////////////////////////////////////////////////////////////////////////
// Local Static Functions
////////////////////////////////////////////////////////////////////////////////

void ClearModStruct(ModStruct & x)
{
	x.Exists = false;
	x.Dim = 0;
	x.Int = 0;
	x.Real = 0.0;

	return;
}

void ClearDblArrayStruct(DblArrayStruct & x)
{
	if (FIRST)
	{
		x.val = NULL;
	}
	if (x.Exists)
	{
		delete [] x.val;
	}
	x.val = NULL;
	x.Exists = false;
	x.Dim = 0;
	x.Size = 0;
	return;
}