//345678901234567890123456789012345678901234567890123456789012345678901234567890
////////////////////////////////////////////////////////////////////////////////
// File:            Cntr_T.cpp
//
// Date:            14FEB06
//
// Purpose:         Instrument Driver for Cntr
//
// Instrument:      Universal Counter Virtual Device (Cntr)
//
//                    Required Libraries / DLL's
//
//   Library/DLL            Purpose
//   =====================  ===============================================
//   cem.lib                \usr\tyx\lib
//   cemsupport.lib         ..\..\Common\Lib
//
// ATLAS Subset: PAWS-85
//
//
//                          Function Number Map
//
//   FNC
// CH1 CH2 Signal
// ------- ----------------------------------------------------------------
//   1 101 sensor (freq) ac signal
//   2 102 sensor (period) ac signal
//   3 103 sensor (freq-ratio) ac signal
//   4     sensor (rise-time) pulsed dc
//   5     sensor (fall-time) pulsed dc
//   6 106 sensor (pulse-width) pulsed dc
//   7 107 sensor (period) pulsed dc
//   8 108 sensor (prf) pulsed dc
//   9 109 sensor (neg-pulse-width) pulsed dc
//  10 110 sensor (pos-pulse-width) pulsed dc
//  11     sensor (count) events
//  12     sensor (count) events (ch1 by ch2)
//  13 113 event monitor (voltage) pulsed dc [pos slope]
//  14 114 event monitor (voltage) pulsed dc [neg slope]
//  15 115 sensor (phase-angle) ac signal
//  16     sensor (rise-time) square wave
//  17     sensor (fall-time) square wave
//  18 118 sensor (period) square wave
//  19 119 sensor (freq) square wave
//  20     sensor (rise-time) ramp signal
//  21     sensor (fall-time) ramp signal
//  22 122 sensor (period) ramp signal
//  23 123 sensor (freq) ramp signal
//  24     sensor (rise-time) triangular wave signal
//  25     sensor (fall-time) triangular wave signal
//  26 126 sensor (period) triangular wave signal
//  27 127 sensor (freq) triangular wave signal
//  213    event monitor (voltage) pulsed dc [pos slope] for stop on A
//  214    event monitor (voltage) pulsed dc [neg slope] for stop on A
// 
// Time Interval
//   130   sensor (time) time interval
// 
// Arm Port
//   131   event monitor (voltage) dc signal [START CT-Arm]
//   132   event monitor (voltage) dc signal [STOP CT-Arm]
//   133   event monitor (voltage) dc signal [START TTLTrig0]
//   134   event monitor (voltage) dc signal [STOP TTLTrig0]
//   135   event monitor (voltage) dc signal [START TTLTrig1]
//   136   event monitor (voltage) dc signal [STOP TTLTrig1]
//   137   event monitor (voltage) dc signal [START TTLTrig2]
//   138   event monitor (voltage) dc signal [STOP TTLTrig2]
//   139   event monitor (voltage) dc signal [START TTLTrig3]
//   140   event monitor (voltage) dc signal [STOP TTLTrig3]
//   141   event monitor (voltage) dc signal [START TTLTrig4]
//   142   event monitor (voltage) dc signal [STOP TTLTrig4]
//   143   event monitor (voltage) dc signal [START TTLTrig5]
//   144   event monitor (voltage) dc signal [STOP TTLTrig5]
//   145   event monitor (voltage) dc signal [START TTLTrig6]
//   146   event monitor (voltage) dc signal [STOP TTLTrig6]
//   147   event monitor (voltage) dc signal [START TTLTrig7]
//   148   event monitor (voltage) dc signal [STOP TTLTrig7]
// 
// Counter-Trigger
// 150 152 event monitor (voltage) pulsed dc [pos slope]
// 151 153 event monitor (voltage) pulsed dc [neg slope]
//
// Revision History
// Rev       Date                  Reason
// =======  =======  =======================================
// 1.0.0.0  14FEB06  Baseline
// 1.4.0.1  03JUL07  Updated to allow negative voltages for
//                   triggers. PCR 170
// 1.4.1.0  23MAR09  Modified to allow for A->A time interval
// 1.4.2.0  22JUL09	 Corrected signal references in time interval
//                   measurements.
// 1.4.3.0  28JUL10  Rebuilt cem interface for new FNC's for time interval
//                   A->A measurements including FNC 213 & 214.
// 1.4.3.1  09AUG10  Increased buffer size in setup. Clipping some debug msgs.
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "Key.h"
#include "cemsupport.h"
#include "Cntr_T.h"
#include <math.h>

// Local Defines

#define CAL_TIME       (86400 * 365) /* one year */

// Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CCntr_T(int Bus, int PrimaryAdr, int SecondaryAdr,
//                      int Dbg, int Sim)
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Bus              int             Bus number
// PrimaryAdr       int             Primary Address (MTA/MLA)
// SecondaryAdr     int             Secondary Address (MSA)
// Dbg              int             Debug flag value
// Sim              int             Simulation flag value (0/1)
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance.
//
///////////////////////////////////////////////////////////////////////////////
CCntr_T::CCntr_T(char *DeviceName,
                       int Bus, int PrimaryAdr, int SecondaryAdr,
                       int Dbg, int Sim)
{
    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;
    int Status = 0;

    if (DeviceName)
        strcpy(m_DeviceName,DeviceName);

    InitPrivateCntr(0);
    NullCalDataCntr();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Cntr Class called with Device [%s], "
                         "Sim %d, Dbg %d",
                          DeviceName, Sim, Dbg));

    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CCntr_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CCntr_T::~CCntr_T()
{
    // Reset the Cntr
    CEMDEBUG(5,cs_FmtMsg("Cntr Class Destructor called for Device [%s], ",
                          m_DeviceName));

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusCntr(int Fnc)
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::StatusCntr(int Fnc)
{
    int Status = 0;

    // Status action for the Cntr
    CEMDEBUG(5,cs_FmtMsg("StatusCntr (%s) called FNC %d",
                          m_DeviceName, Fnc));

    if (Fnc % 100 == 13 || Fnc % 100 == 14 || Fnc >= 131)
    {   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("  EVENT FNC, no 1641 to send"));
    }
    else
    {
        // Check for any pending error messages
        IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, m_SigDesc,
                                        NULL, 0)));
        CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %d", m_SigDesc, Status));
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupCntr(int Fnc)
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::SetupCntr(int Fnc)
{
    int       Status = 0;
    int       func = Fnc % 100;
    char      nominal[256] = "";
    char      gate[256] = "";
    char      sync[256] = "";
    char      gateSync[256] = "";
    char      dcOffset[256] = "";
    char      sigAttribs[256] = "";
    char      impedance[256] = "";
    char      coupling[256] = "";
    char      out[256] = "";
    char      sigAttribs2[256] = "";
    char      impedance2[256] = "";
    char      coupling2[256] = "";
    char      out2[256] = "";
	char	  Buffer[12000] = "";

    // Setup action for the Cntr
    CEMDEBUG(5,cs_FmtMsg("SetupCntr (%s) called FNC %d",
                          m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if ((Status) < 0)
        return(0);

    if ((Status = GetStmtInfoCntr(Fnc)) != 0)
        return(0);

    if (func == 13 || func == 14 || Fnc >= 131)
    {   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("  EVENT FNC, no 1641 to send"));
    }
    else
    {
        strcpy(gateSync, "");
        strcpy(sigAttribs, "");

        if (m_EventGateFrom.Exists)
        {
            sprintf(gate, CR "<Instantaneous name=\"gate\""
                             " startChannel=\"%s\" startNominal=\"%le V\" startCondition=\"%s\""
                             " stopChannel=\"%s\" stopNominal=\"%le V\" stopCondition=\"%s\"/>",
                          m_Events[m_EventGateFrom.Int].Source,
                          m_Events[m_EventGateFrom.Int].TrigVolt.Real,
                          m_Events[m_EventGateFrom.Int].Slope.Int == +1 ? "GE" : "LE",
                          m_Events[m_EventGateTo.Int].Source,
                          m_Events[m_EventGateTo.Int].TrigVolt.Real,
                          m_Events[m_EventGateTo.Int].Slope.Int == +1 ? "GE" : "LE");
            strcat(gateSync, " Gate=\"gate\"");
        }
        else
            strcpy(gate, "");

        if (m_StrobeToEvent.Exists)
        {
            sprintf(sync, CR "<TwoWire name=\"syncInput\" hi=\"%s\"/>"
                          CR "<Instantaneous name=\"sync\" In=\"syncInput\" nominal=\"%e V\" condition=\"%s\"/>",
                          m_Events[m_StrobeToEvent.Int].Source,
                          m_Events[m_StrobeToEvent.Int].TrigVolt.Real,
                          m_Events[m_StrobeToEvent.Int].Slope.Int == +1 ? "GE" : "LE");
            //strcat(gateSync, " Sync=\"syncInput\"");
            strcat(gateSync, " Sync=\"sync\"");
        }
        else
            strcpy(sync, "");

        if (m_DcOffset.Exists)
            sprintf(dcOffset, " amplitude=\"%e V\"", m_DcOffset.Real);
        else
            strcpy(dcOffset, "");

        if (m_Voltage.Exists)
            sprintf(sigAttribs, " amplitude=\"%e V\"", m_Voltage.Real);

        if (m_TestEquipImp.Exists || m_Coupling.Exists)
        {
            if (m_TestEquipImp.Exists)
            {
                sprintf(impedance, CR "<Load name=\"impedance\" resistance=\"%d Ohm\" In=\"composite\"/>",
                        m_TestEquipImp.Int);

                if (m_Coupling.Exists)
                {
                    sprintf(coupling, CR "<HighPass name=\"coupling\" cutoff=\"%d Hz\" In=\"impedance\"/>",
                            m_Coupling.Int);
                    strcpy(out, "coupling");
                }
                else
                {
                    strcpy(coupling, "");
                    strcpy(out, "impedance");
                }
            }
            else
            {
                strcpy(impedance, "");
                sprintf(coupling, CR "<HighPass name=\"coupling\" cutoff=\"%d Hz\" In=\"composite\"/>",
                        m_Coupling.Int);
                strcpy(out, "coupling");
            }
        }
        else
        {
            strcpy(impedance, "");
            strcpy(coupling, "");
            strcpy(out, "composite");
        }

        if (func == 3 || func == 15)
        {   // freqRatio & phaseAngle
            if (func == 3 && m_FreqRatio.Exists)
                sprintf(nominal, " nominal=\"%e\"", m_FreqRatio.Real);
            else if (func == 15 && m_PhaseAngle.Exists)
                sprintf(nominal, " nominal=\"%e deg\"", m_PhaseAngle.Real);

            sprintf(m_SigDesc,
                   "<Signal name=\"Cntr\" Out=\"meas\">"
                CR   "<Constant name=\"dcOffset\"%s/>"
                CR   "<Sinusoid name=\"signal\"%s/>"
                CR   "<Sum name=\"composite\" In=\"dcOffset signal\"/>"
                     "%s"
                     "%s"
                CR   "<FourWire name=\"measChan\" %s/>"
                     "%s"
                     "%s"
                CR   "<Measure name=\"meas\" In=\"measChan\" As=\"%s\"%s"
                       " attribute=\"%s\"%s/>"
                CR "</Signal>",
                dcOffset, sigAttribs, impedance, coupling,
                Fnc < 100 ? "hi=\"1\" shi=\"2\"" : "hi=\"2\" shi=\"1\"",
                gate, sync, out, gateSync, m_MeasAttr, nominal);
        }
        else if (Fnc == 130)
        {   // timeInterval

            if (m_TimeInterval.Exists)
                sprintf(nominal, " nominal=\"%e s\"", m_TimeInterval.Real);

            if (m_Events[m_EventTimeFrom.Int].VoltMax.Exists)
                sprintf(sigAttribs, " amplitude=\"%e V\"", m_Events[m_EventTimeFrom.Int].VoltMax.Real);
            else
                strcpy(sigAttribs, "");

            if (m_Events[m_EventTimeTo.Int].VoltMax.Exists)
                sprintf(sigAttribs2, " amplitude=\"%e V\"", m_Events[m_EventTimeTo.Int].VoltMax.Real);
            else
                strcpy(sigAttribs2, "");

            if (m_Events[m_EventTimeFrom.Int].TestEquipImp.Exists
                || m_Events[m_EventTimeFrom.Int].Coupling.Exists)
            {
                if (m_Events[m_EventTimeFrom.Int].TestEquipImp.Exists)
                {
                    sprintf(impedance, CR "<Load name=\"impedance1\" resistance=\"%d Ohm\" In=\"input1\"/>",
                            m_Events[m_EventTimeFrom.Int].TestEquipImp.Int);

                    if (m_Events[m_EventTimeFrom.Int].Coupling.Exists)
                    {
                        sprintf(coupling, CR "<HighPass name=\"coupling1\" cutoff=\"%d Hz\" In=\"impedance1\"/>",
                                m_Events[m_EventTimeFrom.Int].Coupling.Int);
                        strcpy(out, "coupling1");
                    }
                    else
                    {
                        strcpy(coupling, "");
                        strcpy(out, "impedance1");
                    }
                }
                else
                {
                    strcpy(impedance, "");
                    sprintf(coupling, CR "<HighPass name=\"coupling1\" cutoff=\"%d Hz\" In=\"input1\"/>",
                            m_Events[m_EventTimeFrom.Int].Coupling.Int);
                    strcpy(out, "coupling");
                }
            }
            else
            {
                strcpy(impedance, "");
                strcpy(coupling, "");
                strcpy(out, "input1");
            }

            if (m_Events[m_EventTimeTo.Int].TestEquipImp.Exists
                || m_Events[m_EventTimeTo.Int].Coupling.Exists)
            {
                if (m_Events[m_EventTimeTo.Int].TestEquipImp.Exists)
                {
                    sprintf(impedance2, CR "<Load name=\"impedance2\" resistance=\"%d Ohm\" In=\"input2\"/>",
                            m_Events[m_EventTimeTo.Int].TestEquipImp.Int);

                    if (m_Events[m_EventTimeTo.Int].Coupling.Exists)
                    {
                        sprintf(coupling2, CR "<HighPass name=\"coupling2\" cutoff=\"%d Hz\" In=\"impedance2\"/>",
                                m_Events[m_EventTimeTo.Int].Coupling.Int);
                        strcpy(out2, "coupling2");
                    }
                    else
                    {
                        strcpy(coupling2, "");
                        strcpy(out2, "impedance2");
                    }
                }
                else
                {
                    strcpy(impedance2, "");
                    sprintf(coupling2, CR "<HighPass name=\"coupling2\" cutoff=\"%d Hz\" In=\"input2\"/>",
                            m_Events[m_EventTimeTo.Int].Coupling.Int);
                    strcpy(out2, "coupling2");
                }
            }
            else
            {
                strcpy(impedance2, "");
                strcpy(coupling2, "");
                strcpy(out2, "input2");
            }

            sprintf(m_SigDesc,
                   "<Signal name=\"Cntr\" Out=\"meas\">"
                CR   "<Sinusoid name=\"signal1\"%s/>"
                CR   "<TwoWire name=\"input1\" hi=\"%s\" In=\"signal1\"/>"
                     "%s"
                     "%s"
                CR   "<Instantaneous name=\"start\" In=\"%s\" nominal=\"%e V\" condition=\"%s\"/>"
                CR   "<TimedEvent type=\"Time\" name=\"holdoff\" delay=\"%e s\" Sync=\"start\"/>"
                CR   "<Sinusoid name=\"signal2\"%s/>"
                CR   "<TwoWire name=\"input2\" hi=\"%s\" In=\"signal2\"/>"
                     "%s"
                     "%s"
                CR   "<Instantaneous name=\"stop\" In=\"%s\" nominal=\"%e V\" condition=\"%s\" Gate=\"holdoff\"/>"
                CR   "<TimeInterval type=\"Time\" name=\"meas\" In=\"start\" Gate=\"stop\""
                       " attribute=\"%s\"%s/>"
                CR "</Signal>",
                sigAttribs, m_Events[m_EventTimeFrom.Int].Source,
                impedance, coupling, out,
                m_Events[m_EventTimeFrom.Int].TrigVolt.Real,
                m_Events[m_EventTimeFrom.Int].Slope.Int == +1 ? "GE" : "LE",
                m_Events[m_EventTimeFrom.Int].SettleTime.Exists ? m_Events[m_EventTimeFrom.Int].SettleTime.Real : 0,
                sigAttribs2, m_Events[m_EventTimeTo.Int].Source,
                impedance2, coupling2, out2,
                m_Events[m_EventTimeTo.Int].TrigVolt.Real,
                m_Events[m_EventTimeTo.Int].Slope.Int == +1 ? "GE" : "LE",
                m_MeasAttr, nominal);
        }
        else if (Fnc == 11 || Fnc == 12)
        {   // totalize
            if (Fnc == 12)
            {   // totalize ch1 by ch2
                strcpy(gate, CR "<Instantaneous name=\"gate\" startChannel=\"2\" stopChannel=\"2\"/>");
                if (strstr(gateSync, " Gate=\"gate\"") == 0)
                    strcat(gateSync, " Gate=\"gate\"");
            }

            sprintf(m_SigDesc,
                   "<Signal name=\"Cntr\" Out=\"meas\">"
                CR   "<Sinusoid name=\"composite\"%s/>"
                     "%s"
                     "%s"
                CR   "<TwoWire name=\"measChan\" hi=\"1\" In=\"%s\"/>"
                     "%s"
                     "%s"
                CR   "<Counter name=\"meas\" In=\"measChan\" nominal=\"%e V\" condition=\"%s\""
                       "%s attribute=\"%s\"/>"
                CR "</Signal>",
                sigAttribs, impedance, coupling, out, gate, sync, m_Trig.Real,
                m_Slope.Int == +1 ? "GE" : "LE", gateSync, m_MeasAttr);
        }
        else
        {   // frequency, period, rise/fallTime, pulseWidth
            if (strcmp(m_MeasAttr, "frequency") == 0 && m_Frequency.Exists)
                sprintf(nominal, " nominal=\"%e Hz\"", m_Frequency.Real);
            else if (strcmp(m_MeasAttr, "period") == 0 && m_Period.Exists)
                sprintf(nominal, " nominal=\"%e s\"", m_Period.Real);
            else if (strcmp(m_MeasAttr, "riseTime") == 0 && m_RiseTime.Exists)
                sprintf(nominal, " nominal=\"%e s\"", m_RiseTime.Real);
            else if (strcmp(m_MeasAttr, "fallTime") == 0 && m_FallTime.Exists)
                sprintf(nominal, " nominal=\"%e s\"", m_FallTime.Real);
            else if (strcmp(m_MeasAttr, "pulseWidth") == 0 && m_PulseWidth.Exists)
                sprintf(nominal, " nominal=\"%e s\"", m_PulseWidth.Real);
            else if (strcmp(m_MeasAttr, "negPulseWidth") == 0 && m_PulseWidth.Exists)
                sprintf(nominal, " nominal=\"%e s\"", m_PulseWidth.Real);

            sprintf(m_SigDesc,
                   "<Signal name=\"Cntr\" Out=\"meas\">"
                CR   "<Constant name=\"dcOffset\"%s/>"
                CR   "<Sinusoid name=\"signal\"%s/>"
                CR   "<Sum name=\"composite\" In=\"dcOffset signal\"/>"
                     "%s"
                     "%s"
                CR   "<TwoWire name=\"measChan\" hi=\"%s\"/>"
                     "%s"
                     "%s"
                CR   "<Measure name=\"meas\" In=\"measChan\" As=\"%s\"%s"
                       " attribute=\"%s\"%s/>"
                CR "</Signal>",
                dcOffset, sigAttribs, impedance, coupling, Fnc < 100 ? "1" : "2",
                gate, sync, out, gateSync, m_MeasAttr, nominal);
        }

	///Check MaxTime Modifier
		if(m_MaxTime.Exists)
		{			
			IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Setup", m_DeviceName, m_MaxTime.Real, m_SigDesc, Buffer, 12000)));
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal("Setup", m_DeviceName, m_SigDesc, Buffer, 12000)));
		}
        
        CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SigDesc, Status));
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseCntr(int Fnc)
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::CloseCntr(int Fnc)
{
    // Close action for the Cntr
    CEMDEBUG(5,cs_FmtMsg("CloseCntr (%s) called FNC %d",
                          m_DeviceName, Fnc));

    // close not needed for sensors
    //IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SigDesc,
    //                                    NULL, 0)));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitiateCntr(int Fnc)
//
// Purpose: Perform the Initiate action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::InitiateCntr(int Fnc)
{
    int   Status = 0;

    if ((Status = GetStmtInfoCntr(Fnc)) != 0)
        return(0);

    // Initiate action for the Cntr
    CEMDEBUG(5,cs_FmtMsg("InitiateCntr (%s) called FNC %d",
                          m_DeviceName, Fnc));

    if (Fnc % 100 == 13 || Fnc % 100 == 14 || Fnc >= 131)
    {   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("  EVENT FNC, no 1641 to send"));
    }
    else
    {
        if (m_MaxTime.Exists)
		{
			IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Enable", m_DeviceName, m_MaxTime.Real, m_SigDesc, NULL, 0)));
			CEMDEBUG(9,cs_FmtMsg("IssueSignalMaxTime [%s][%e] %d", m_SigDesc, m_MaxTime.Real, Status));
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SigDesc, NULL, 0)));
			CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SigDesc, Status));
		}
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchCntr(int Fnc)
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::FetchCntr(int Fnc)
{
    DATUM    *fdat;
    int       Status = 0;
    double    MeasValue = 0.0;
    char      XmlValue[1024];

    // Fetch action for the Cntr
    CEMDEBUG(5,cs_FmtMsg("FetchCntr (%s) called FNC %d",
                          m_DeviceName, Fnc));

    if (Fnc % 100 == 13 || Fnc % 100 == 14 || Fnc >= 131)
    {   // events -- we're done!
        CEMDEBUG(5, cs_FmtMsg("  EVENT FNC, no 1641 to send"));
    }
    else
    {
        // Fetch data
        IFNSIM((Status = cs_IssueAtmlSignal("Fetch", m_DeviceName, m_SigDesc,
                                            XmlValue, 1024)));

        if (Status)
        {
            MeasValue = FLT_MAX;
        }
        else
        {
            IFNSIM(cs_GetSingleDblValue(XmlValue, m_MeasAttr, &MeasValue));
        }

        fdat = FthDat();
        DECDatVal(fdat, 0) = MeasValue;
        FthCnt(1);
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: OpenCntr(int Fnc)
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::OpenCntr(int Fnc)
{
    // Open action for the Cntr
    CEMDEBUG(5,cs_FmtMsg("OpenCntr (%s) called FNC %d",
                          m_DeviceName, Fnc));

    // open not needed for sensors
    //IFNSIM((Status = cs_IssueAtmlSignal("Disable", m_DeviceName, m_SigDesc,
    //                                    NULL, 0)));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetCntr(int Fnc)
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::ResetCntr(int Fnc)
{
    int   Status = 0;
    int   func = Fnc % 100;

    // Reset action for the Cntr
    CEMDEBUG(5, cs_FmtMsg("ResetCntr (%s) called FNC %d",
                         m_DeviceName, Fnc));

    if (func == 13 || func == 14 || Fnc >= 131)
    {   // events -- no 1641
        CEMDEBUG(5, cs_FmtMsg("  EVENT FNC, no 1641 to send"));

        // make the associated event record available
        CEMDEBUG(10, cs_FmtMsg("  Marking event slot for %d as available", Fnc));
        m_Events[FindEventByFnc(Fnc)].Active = false;
    }
    else
    {
        // Check for not Remove All --
        //   Remove All will use Station Sequence called only from the SwitchCEM.dll
        if (Fnc != 0)
        {   // Reset the Cntr
            IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SigDesc, NULL, 0)));
        }

        InitPrivateCntr(Fnc);
    }

    return(0);
}


//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoCntr(int Fnc)
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// Fnc              int             Device Database Function value
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CCntr_T::GetStmtInfoCntr(int Fnc)
{
    DATUM    *datum;
    int       func = Fnc % 100;
    int       eventNum;
    int       event;
    double    Volt;

    //////////////////// NOUN ///////////////////////////

    m_Noun = GetCurNoun();

    ////////////////// MODIFIERS ////////////////////////

    if (func == 13 || func == 14 || Fnc >= 131)
    {   // EVENT FNCs

        // MAX-TIME
        if ((datum = RetrieveDatum(M_MAXT, K_SET)) ||
            (datum = RetrieveDatum(M_MAXT, K_SRX)))
        {
            m_MaxTime.Exists = true;
            m_MaxTime.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found MAX-TIME: %f", m_MaxTime.Real));
            FreeDatum(datum);
        }

        // EVENT-OUT (event enable)
        if ((datum = RetrieveDatum(M_EVOU, K_SET)) ||
            (datum = RetrieveDatum(M_EVOU, K_SRX)) )
        {
            // get event number
            eventNum = INTDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found EVENT-OUT (%d) for FNC %d", eventNum, Fnc));
            FreeDatum(datum);

            // find next available event record
            event = FindAvailEvent();

            if (event < 0)
            {   // oops! ran out of slots!
                event = 0;  // overwriting active event, but at least not overflowing...
            }

            m_Events[event].Active = true;
            m_Events[event].EventNum = eventNum;
            m_Events[event].Fnc = Fnc;
            CEMDEBUG(10, cs_FmtMsg("Storing event number %d (FNC %d) in slot %d",
                    m_Events[event].EventNum, m_Events[event].Fnc, event));

            // determine EVENT source
            switch (Fnc)
            {
              case 13:
              case 14:
              case 150:
              case 151:
			  case 213:
			  case 214:
                strcpy(m_Events[event].Source, "1");
                break;
              case 113:
              case 114:
              case 152:
              case 153:
                strcpy(m_Events[event].Source, "2");
                break;
              case 131:
              case 132:
                strcpy(m_Events[event].Source, "EXT");
                break;
              case 133:
              case 134:
                strcpy(m_Events[event].Source, "TTLT0");
                break;
              case 135:
              case 136:
                strcpy(m_Events[event].Source, "TTLT1");
                break;
              case 137:
              case 138:
                strcpy(m_Events[event].Source, "TTLT2");
                break;
              case 139:
              case 140:
                strcpy(m_Events[event].Source, "TTLT3");
                break;
              case 141:
              case 142:
                strcpy(m_Events[event].Source, "TTLT4");
                break;
              case 143:
              case 144:
                strcpy(m_Events[event].Source, "TTLT5");
                break;
              case 145:
              case 146:
                strcpy(m_Events[event].Source, "TTLT6");
                break;
              case 147:
              case 148:
                strcpy(m_Events[event].Source, "TTLT7");
                break;
            }

            // COUPLING
            if ((datum = RetrieveDatum(M_CPLG, K_SET)) ||
                (datum = RetrieveDatum(M_CPLG, K_SRX)))
            {
                m_Events[event].Coupling.Exists = true;
                // Coupling.Int = 100 (AC) or 0 (DC) [cutoff freq for HP filter]
                m_Events[event].Coupling.Int = strcmp(GetTXTDatVal(datum, 0), "AC") == 0 ? 100 : 0;
                CEMDEBUG(10, cs_FmtMsg("Found COUPLING: %d", m_Events[event].Coupling.Int));
                FreeDatum(datum);
            }

            // EVENT-SLOPE
            if ((datum = RetrieveDatum(M_EVSL, K_SET)) ||
                (datum = RetrieveDatum(M_EVSL, K_SRX)))
            {
                m_Events[event].Slope.Exists = true;
                m_Events[event].Slope.Int = strcmp(GetTXTDatVal(datum, 0), "POS") == 0 ? +1 : -1;
                CEMDEBUG(10, cs_FmtMsg("Found EVENT-SLOPE: %d", m_Events[event].Slope.Int));
                FreeDatum(datum);
            }

            // SETTLE-TIME
            if ((datum = RetrieveDatum(M_SETT, K_SET)) ||
                (datum = RetrieveDatum(M_SETT, K_SRX)))
            {
                m_Events[event].SettleTime.Exists = true;
                m_Events[event].SettleTime.Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found SETTLE-TIME: %f", m_Events[event].SettleTime.Real));
                FreeDatum(datum);
            }

            // TEST-EQUIP-IMP
            if ((datum = RetrieveDatum(M_TIMP, K_SET)) ||
                (datum = RetrieveDatum(M_TIMP, K_SRX)))
            {
                m_Events[event].TestEquipImp.Exists = true;
                m_Events[event].TestEquipImp.Int = (int)DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found TEST-EQUIP-IMP: %d", m_Events[event].TestEquipImp.Int));
                FreeDatum(datum);
            }

            // SET (trigger) VOLTAGE
            if (datum = RetrieveDatum(M_VOLT, K_SET))
            {
                m_Events[event].TrigVolt.Exists = true;
                m_Events[event].TrigVolt.Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found trigger VOLTAGE %f", m_Events[event].TrigVolt.Real));
            }

            // SET VOLTAGE MAX
            if (datum = RetrieveDatum(M_VOLT, K_SRX))
            {
                m_Events[event].VoltMax.Exists = true;
                m_Events[event].VoltMax.Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found VOLTAGE MAX %f", m_Events[event].VoltMax.Real));
            }
        }
    }
    else
    {   // MEASURE FNCs

        switch (func)
        {
          case 1:
          case 8:
          case 19:
          case 23:
          case 27:
            strcpy(m_MeasAttr, "frequency");
            break;
          case 2:
          case 7:
          case 18:
          case 22:
          case 26:
            strcpy(m_MeasAttr, "period");
            break;
          case 3:
            strcpy(m_MeasAttr, "freqRatio");
            break;
          case 4:
          case 16:
          case 20:
          case 24:
            strcpy(m_MeasAttr, "riseTime");
            break;
          case 5:
          case 17:
          case 21:
          case 25:
            strcpy(m_MeasAttr, "fallTime");
            break;
          case 6:
          case 10:
            strcpy(m_MeasAttr, "pulseWidth");
            break;
          case 9:
            strcpy(m_MeasAttr, "negPulseWidth");
            break;
          case 11:
          case 12:
            strcpy(m_MeasAttr, "totalize");
            break;
          case 15:
            strcpy(m_MeasAttr, "phaseAngle");
            break;
          case 30:  // FNC 130
            strcpy(m_MeasAttr, "timeInterval");
            break;
          default:
            CEMDEBUG(1,cs_FmtMsg("GetStmtInfoCntr (%s) called with invalid FNC %d",
                                 m_DeviceName, Fnc));
            break;
        }

        // COUPLING
        if ((datum = RetrieveDatum(M_CPLG, K_SET)) ||
            (datum = RetrieveDatum(M_CPLG, K_SRX)))
        {
            m_Coupling.Exists = true;
            // Coupling.Int = 100 (AC) or 0 (DC) [cutoff freq for HP filter]
            m_Coupling.Int = strcmp(GetTXTDatVal(datum, 0), "AC") == 0 ? 100 : 0;
            CEMDEBUG(10, cs_FmtMsg("Found COUPLING: %d", m_Coupling.Int));
            FreeDatum(datum);
        }

        // DC-OFFSET
        if ((datum = RetrieveDatum(M_DCOF, K_SET)) ||
            (datum = RetrieveDatum(M_DCOF, K_SRX)))
        {
            m_DcOffset.Exists = true;
            m_DcOffset.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found DC-OFFSET: %f", m_DcOffset.Real));
            FreeDatum(datum);
        }

        // EVENT-GATE-FROM
        if ((datum = RetrieveDatum(M_EVGF, K_SET)) ||
            (datum = RetrieveDatum(M_EVGF, K_SRX)))
        {
            m_EventGateFrom.Exists = true;
            m_EventGateFrom.Int = FindEventByNum(INTDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found EVENT-GATE-FROM: %d", m_EventGateFrom.Int));
            FreeDatum(datum);
        }

        // EVENT-GATE-TO
        if ((datum = RetrieveDatum(M_EVGT, K_SET)) ||
            (datum = RetrieveDatum(M_EVGT, K_SRX)))
        {
            m_EventGateTo.Exists = true;
            m_EventGateTo.Int = FindEventByNum(INTDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found EVENT-GATE-TO: %d", m_EventGateTo.Int));
            FreeDatum(datum);
        }

        // EVENT-TIME-FROM
        if ((datum = RetrieveDatum(M_EVTF, K_SET)) ||
            (datum = RetrieveDatum(M_EVTF, K_SRX)))
        {
            m_EventTimeFrom.Exists = true;
            m_EventTimeFrom.Int = FindEventByNum(INTDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found EVENT-TIME-FROM: %d", m_EventTimeFrom.Int));
            FreeDatum(datum);
        }

        // EVENT-TIME-TO
        if ((datum = RetrieveDatum(M_EVTT, K_SET)) ||
            (datum = RetrieveDatum(M_EVTT, K_SRX)))
        {
            m_EventTimeTo.Exists = true;
            m_EventTimeTo.Int = FindEventByNum(INTDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found EVENT-TIME-TO: %d", m_EventTimeTo.Int));
            FreeDatum(datum);
        }

        // FALL-TIME
        if ((datum = RetrieveDatum(M_FALL, K_SET)) ||
            (datum = RetrieveDatum(M_FALL, K_SRX)))
        {
            m_FallTime.Exists = true;
            m_FallTime.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found FALL-TIME: %f", m_FallTime.Real));
            FreeDatum(datum);
        }

        // FREQ-RATIO
        if ((datum = RetrieveDatum(M_FRQR, K_SET)) ||
            (datum = RetrieveDatum(M_FRQR, K_SRX)))
        {
            m_FreqRatio.Exists = true;
            m_FreqRatio.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found FREQ-RATIO: %f", m_FreqRatio.Real));
            FreeDatum(datum);
        }

        // FREQUENCY & PRF
        if ((datum = RetrieveDatum(M_FREQ, K_SET)) ||
            (datum = RetrieveDatum(M_FREQ, K_SRX)) ||
            (datum = RetrieveDatum(M_PRFR, K_SET)) ||
            (datum = RetrieveDatum(M_PRFR, K_SRX)))
        {
            m_Frequency.Exists = true;
            m_Frequency.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found FREQUENCY: %f", m_Frequency.Real));
            FreeDatum(datum);
        }

        // MAX-TIME
        if ((datum = RetrieveDatum(M_MAXT, K_SET)) ||
            (datum = RetrieveDatum(M_MAXT, K_SRX)))
        {
            m_MaxTime.Exists = true;
            m_MaxTime.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found MAX-TIME: %f", m_MaxTime.Real));
            FreeDatum(datum);
        }

        // NEG-SLOPE
        if (RemoveMod(M_NEGS))
        {
            m_Slope.Exists = true;
            m_Slope.Int = -1;
            CEMDEBUG(10, cs_FmtMsg("Found NEG-SLOPE: %d", m_Slope.Int));

            // if MC is pulse-width or pos-pulse-width & have neg-slope MOD,
            //   treat it like MC neg-pulse-width
            if (func == 6 || func == 10)
                strcpy(m_MeasAttr, "negPulseWidth");
        }

        // PERIOD
        if ((datum = RetrieveDatum(M_PERI, K_SET)) ||
            (datum = RetrieveDatum(M_PERI, K_SRX)))
        {
            m_Period.Exists = true;
            m_Period.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found PERIOD: %f", m_Period.Real));
            FreeDatum(datum);
        }

        // PHASE-ANGLE
        if ((datum = RetrieveDatum(M_PANG, K_SET)) ||
            (datum = RetrieveDatum(M_PANG, K_SRX)))
        {
            m_PhaseAngle.Exists = true;
            m_PhaseAngle.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found PERIOD: %f", m_PhaseAngle.Real));
            FreeDatum(datum);
        }

        // PULSE-WIDTH, POS-PULSE-WIDTH & NEG-PULSE-WIDTH
        if ((datum = RetrieveDatum(M_PLWD, K_SET)) ||
            (datum = RetrieveDatum(M_PLWD, K_SRX)) ||
            (datum = RetrieveDatum(M_PPWT, K_SET)) ||
            (datum = RetrieveDatum(M_PPWT, K_SRX)) ||
            (datum = RetrieveDatum(M_NPWT, K_SET)) ||
            (datum = RetrieveDatum(M_NPWT, K_SRX)))
        {
            m_PulseWidth.Exists = true;
            m_PulseWidth.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found PULSE-WIDTH: %f", m_PulseWidth.Real));
            FreeDatum(datum);
        }

        // POS-SLOPE
        if (RemoveMod(M_POSS))
        {
            m_Slope.Exists = true;
            m_Slope.Int = +1;
            CEMDEBUG(10, cs_FmtMsg("Found POS-SLOPE: %d", m_Slope.Int));
        }

        // RISE-TIME
        if ((datum = RetrieveDatum(M_RISE, K_SET)) ||
            (datum = RetrieveDatum(M_RISE, K_SRX)))
        {
            m_RiseTime.Exists = true;
            m_RiseTime.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found RISE-TIME: %f", m_RiseTime.Real));
            FreeDatum(datum);
        }

        // STROBE-TO-EVENT
        if ((datum = RetrieveDatum(M_SBEV, K_SET)) ||
            (datum = RetrieveDatum(M_SBEV, K_SRX)))
        {
            m_StrobeToEvent.Exists = true;
            m_StrobeToEvent.Int = FindEventByNum(INTDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found STROBE-TO-EVENT: %d", m_StrobeToEvent.Int));
            FreeDatum(datum);
        }

        // TEST-EQUIP-IMP
        if ((datum = RetrieveDatum(M_TIMP, K_SET)) ||
            (datum = RetrieveDatum(M_TIMP, K_SRX)))
        {
            m_TestEquipImp.Exists = true;
            m_TestEquipImp.Int = (int)DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found TEST-EQUIP-IMP: %d", m_TestEquipImp.Int));
            FreeDatum(datum);
        }

        // TIME
        if ((datum = RetrieveDatum(M_TIME, K_SET)) ||
            (datum = RetrieveDatum(M_TIME, K_SRX)))
        {
            m_TimeInterval.Exists = true;
            m_TimeInterval.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found TIME: %f", m_TimeInterval.Real));
            FreeDatum(datum);
        }

        // TRIG
        if ((datum = RetrieveDatum(M_TRIG, K_SET)) ||
            (datum = RetrieveDatum(M_TRIG, K_SRX)))
        {
            m_Trig.Exists = true;
            m_Trig.Real = DECDatVal(datum, 0);
            CEMDEBUG(10, cs_FmtMsg("Found TRIG: %f", m_Trig.Real));
            FreeDatum(datum);
        }

        // VOLTAGE-PP
        if ((datum = RetrieveDatum(M_VLPP, K_SET)) ||
            (datum = RetrieveDatum(M_VLPP, K_SRX)))
        {
            m_Voltage.Exists = true;
            m_Voltage.Real = DECDatVal(datum, 0) / 2.0;
            CEMDEBUG(10, cs_FmtMsg("Found VOLTAGE-PP: %f (Vp)", m_Voltage.Real));
            FreeDatum(datum);
        }

        // VOLTAGE
        if ((datum = RetrieveDatum(M_VOLT, K_SET)) ||
            (datum = RetrieveDatum(M_VOLT, K_SRX)))
        {
            Volt = fabs(DECDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found VOLTAGE %f", Volt));
            if (m_Noun == N_ACS || m_Noun == N_EVS)
            {   // AC SIGNAL
                m_Voltage.Exists = true;
                m_Voltage.Real = Volt * RMS_PK;
                CEMDEBUG(10, cs_FmtMsg("  ACS: storing %f", m_Voltage.Real));
            }
            else
            {   // no other signal supports VOLTAGE
                CEMDEBUG(1, "  VOLTAGE not supported for non-AC SIGNALs");
            }
        }

        // VOLTAGE-P
        if ((datum = RetrieveDatum(M_VLPK, K_SET)) ||
            (datum = RetrieveDatum(M_VLPK, K_SRX)))
        {
            m_Voltage.Exists = true;
            Volt = fabs(DECDatVal(datum, 0));
            CEMDEBUG(10, cs_FmtMsg("Found VOLTAGE-P %f", Volt));
            if (m_Noun == N_PDC)
            {   // PULSED DC
                m_Voltage.Real = Volt / 2.0;
                m_DcOffset.Real += m_Voltage.Real;
                CEMDEBUG(10, cs_FmtMsg("  PDC: storing %f\n  DCO: storing %f",
                m_Voltage.Real, m_DcOffset.Real));
            }
            else
            {   // all other signal types
                m_Voltage.Real = Volt;
                CEMDEBUG(10, cs_FmtMsg("  *: storing %f", m_Voltage.Real));
            }
        }
    }

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateCntr()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CCntr_T::InitPrivateCntr(int fnc)
{
//  CEMDEBUG(10, cs_FmtMsg("InitPrivateCntr called"));
    m_Coupling.Exists = false;
    m_DcOffset.Exists = false;
    m_EventGateFrom.Exists = false;
    m_EventGateTo.Exists = false;
    m_EventSlope.Exists = false;
    m_EventTimeFrom.Exists = false;
    m_EventTimeTo.Exists = false;
    m_FallTime.Exists = false;
    m_Frequency.Exists = false;
    m_MaxTime.Exists = false;
    m_RiseTime.Exists = false;
    m_Slope.Exists = false;
    m_StrobeToEvent.Exists = false;
    m_TestEquipImp.Exists = false;
    m_Trig.Exists = false;
    m_Voltage.Exists = false;

    if (fnc == 0)
    {
        for (int i = 0; i < MAX_EVENTS; i++)
        {
            m_Events[i].Fnc = 0;
            m_Events[i].EventNum = 0;
            m_Events[i].Active = false;
            strcpy(m_Events[i].Source, "");
            m_Events[i].Coupling.Exists = false;
            m_Events[i].SettleTime.Exists = false;
            m_Events[i].Slope.Exists = false;
            m_Events[i].TestEquipImp.Exists = false;
            m_Events[i].TrigVolt.Exists = false;
            m_Events[i].VoltMax.Exists = false;
        }
    }

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataCntr()
//
// Purpose: Initialize/Reset all calibration variables
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CCntr_T::NullCalDataCntr(void)
{
    //////// Place Cntr specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE
    m_CalData[0] = 1.0;
    m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: FindAvailEvent()
//
// Purpose: Find the next available (inactive) m_Event record
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
inline int CCntr_T::FindAvailEvent(void)
{
    int event;

    for (event = 0; event < MAX_EVENTS; event++)
        if (!m_Events[event].Active)
        {
//          CEMDEBUG(10, cs_FmtMsg("Found avail event slot %d", event));
            return(event);
        }

    // oops! can't find it
    CEMDEBUG(0, cs_FmtMsg("ERROR - no available m_Event slots"));
    return(-1);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FindEventByNum()
//
// Purpose: Find the m_Event record corresponding to given event number
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
inline int CCntr_T::FindEventByNum(int num)
{
    int event;

    for (event = 0; event < MAX_EVENTS; event++)
        if (m_Events[event].EventNum == num)
        {
//          CEMDEBUG(10, cs_FmtMsg("Event %d corresponds to event number %d", event, num));        
            return(event);
        }

    // oops! can't find it
    CEMDEBUG(0, cs_FmtMsg("Can't find event number %d", num));
    return(-1);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FindEventByFnc()
//
// Purpose: Find the m_Event record corresponding to given FNC
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
inline int CCntr_T::FindEventByFnc(int fnc)
{
    int event;

    for (event = 0; event < MAX_EVENTS; event++)
    {
//      CEMDEBUG(10, cs_FmtMsg("Event %d FNC %d, looking for %d", event, m_Events[event].Fnc, fnc));        
        if (m_Events[event].Fnc == fnc)
        {
            CEMDEBUG(10, cs_FmtMsg("Event %d corresponds to FNC %d", event, fnc));        
            return(event);
        }
    }

    // oops! can't find it
    CEMDEBUG(0, cs_FmtMsg("Can't find event with FNC %d", fnc));
    return(-1);
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
