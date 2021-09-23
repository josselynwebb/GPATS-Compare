//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:        Srs_T.cpp
//
// Date:        11-OCT-05
//
// Purpose:     Instrument Driver for Srs
//
// Instrument:  Synchro-Resolver (SRS)
//
//                    Required Libraries / DLL's
//      
//      Library/DLL                 Purpose
//  =====================  ===============================================
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
//////// Place Srs specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE 
//   1  sensor (voltage)   dc signal
//   2  sensor (ac-comp)   dc signal
//
//  11  sensor (voltage)   ac signal
//  12  sensor (dc-offset) ac signal
/* End TEMPLATE_SAMPLE_CODE */
//
// Revision History
// Rev       Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline                         
///////////////////////////////////////////////////////////////////////////////
// Includes
/* Begin TEMPLATE_SAMPLE_CODE 
#include <float.h>
#include <math.h>
/* End TEMPLATE_SAMPLE_CODE */
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Srs_T.h"

// Static Variables
static int   s_Mode;
static int   s_Chan = 0;
static bool  s_Multi = false;
static int   s_App = 0;
static char *s_Resource;
static bool *s_ExtRefPtr;


// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CSrs_T(int Bus, int PrimaryAdr, int SecondaryAdr,
//                      int Dbg, int Sim)
//
// Purpose: Initialize the instrument driver
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Bus              int             Bus number
// PrimaryAdr       int             Primary Address (MTA/MLA)
// SecondaryAdr     int             Secondary Address (MSA)
// Dbg              int             Debug flag value
// Sim              int             Simulation flag value (0/1)
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance.
//
///////////////////////////////////////////////////////////////////////////////
CSrs_T::CSrs_T(char *DeviceName, int Bus, int PrimaryAdr, int SecondaryAdr,
        int Dbg, int Sim)
{
    int Status = 0;
    char sMsg[100] = "";
    char buffer[100] = " ";

    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;

    if (DeviceName)
    {
        strcpy(m_DeviceName, DeviceName);
        strcpy(m_ResourceSim[0], m_DeviceName);
        strcpy(m_ResourceInd[0], m_DeviceName);
        for (int i = 1; i < CHANNELS; i++)
        {
            sprintf(m_ResourceSim[i], "%s_DS%d", m_DeviceName, i);
            sprintf(m_ResourceInd[i], "%s_SD%d", m_DeviceName, i);
        }
    }

    InitPrivateSrs(0);
    NullCalDataSrs();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5, cs_FmtMsg("Srs Class called with Device [%s], "
            "Sim %d, Dbg %d", DeviceName, Sim, Dbg));

    // Initialize the Srs - not required in ATML mode
    // Check Cal Status and Resource Availability
//  Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT, m_Sim);
    for (int i = 1; i < CHANNELS; i++)
    {
        Status = cs_GetUniqCalCfg(m_ResourceSim[i], CAL_TIME, &m_CalData[0], CAL_DATA_COUNT, m_Sim);
        Status = cs_GetUniqCalCfg(m_ResourceInd[i], CAL_TIME, &m_CalData[0], CAL_DATA_COUNT, m_Sim);
    }

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CSrs_T()
//
// Purpose: Destroy the instrument driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    Class instance destroyed.
//
///////////////////////////////////////////////////////////////////////////////
CSrs_T::~CSrs_T()
{
    // Reset the Srs
    CEMDEBUG(5, cs_FmtMsg("Srs Class Destructor called for Device [%s], ",
            m_DeviceName));

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusSrs(int Fnc)
//
// Purpose: Perform the Status action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::StatusSrs(int Fnc)
{
    int Status = 0;

    GetChan(Fnc);

    // Status action for the Srs
    CEMDEBUG(5, cs_FmtMsg("StatusSrs (%s) called FNC %d", 
            s_Resource, Fnc));

    if (s_Chan == 0) return (0);    // nothing to do on events

    // Check for any pending error messages
    IFNSIM((Status = cs_IssueAtmlSignal("Status", s_Resource, "", NULL, 0)));
    CEMDEBUG(9, "IssueStatus");

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupSrs_T(int Fnc)
//
// Purpose: Perform the Setup action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::SetupSrs(int Fnc)
{
    int  Status = 0;
    char SigDesc[1024];
    char Attribute[22];
    char RefIn[30] = "";
    char RefPort[30] = "";
    char Sync[30] = "";
    char string[512];
//  char atXmlString[512];
    char mixedMode[9], upperMode[9];

    GetChan(Fnc);

    // Setup action for the Srs
    CEMDEBUG(5, cs_FmtMsg("SetupSrs (%s) called FNC %d", 
            s_Resource, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if ((Status) < 0)
        return (0);
        
    if ((Status = GetStmtInfoSrs(Fnc)) != 0)
        return (0);

    if (s_Chan == 0) return (0);    // nothing to do on events

    // build proper "s_Mode" substring only once
    if (s_Mode == SYNCHRO)
    {
        strcpy(mixedMode, "Synchro");
        strcpy(upperMode, "SYNCHRO");
    }
    else
    {
        strcpy(mixedMode, "Resolver");
        strcpy(upperMode, "RESOLVER");
    }

    if (*s_ExtRefPtr)
    {
        strcpy(RefPort, CR "<atXml:port name=\"RefExt\"/>");
        strcpy(RefIn, " In=\"RefExt\"");
    }

    if (s_App == MEASUREMENT)
    {
        // sensor mode
        sprintf(SigDesc, "<Signal name=\"SRS\" Out=\"%s\">", mixedMode);

        // Measure, port & TSF elements
        strcpy(Attribute, Fnc & RATE_MASK ? "angle_rate" : "angle");

        sprintf(string, CR "<Measure name=\"%s\" As=\"%s\" samples=\"1\" attribute=\"%s\" In=\"Ch%d%s\"/>"
                        CR "<atXml:port name=\"Ch%d\"/>"
                        "%s"
                        CR "<atXml:%s name=\"%s\"",
                mixedMode, upperMode, Attribute, s_Chan, *s_ExtRefPtr ? " RefExt" : "", s_Chan,
                RefPort, upperMode, upperMode);
        strcat(SigDesc, string);

        // append modifiers
        if (m_RefVoltInd[s_Chan].Exists)
        {
            sprintf(string, " ref_volt=\"%le V\"", m_RefVoltSim[s_Chan].Real);
            strcat(SigDesc, string);
        }

        if (m_FreqInd[s_Chan].Exists)
        {
            sprintf(string, " freq=\"%le Hz\"", m_FreqInd[s_Chan].Real);
            strcat(SigDesc, string);
        }

        /* SpeedRatio?? */
        if (m_RatioInd[s_Chan].Exists)
        {
            sprintf(string, " speed_ratio=\"%d\"", m_RatioInd[s_Chan].Int);
            strcat(SigDesc, string);
        }
    }
    else if (s_App == STIMULUS)
    {
        // source mode
        sprintf(SigDesc, "<Signal name=\"SRS\" Out=\"Ch%d\">", s_Chan);

        // Sync chain
        if (m_SyncToEvent[s_Chan].Exists)
        {
            sprintf(string, CR "<atXml:port name=\"%s\"/>"
                            CR "<Instantaneous name=\"sync\" condition=\"%s\" In=\"%s\"/>",
                    m_Events[m_SyncToEvent[s_Chan].Int].Source,
                    m_Events[m_SyncToEvent[s_Chan].Int].Slope.Int == +1 ? "GE" : "LE",
                    m_Events[m_SyncToEvent[s_Chan].Int].Source);
            strcat(SigDesc, string);
            strcpy(Sync, " Sync=\"sync\"");
        }

        // port & TSF elements
        sprintf(string, "%s"
                        CR "<atXml:port name=\"Ch%d\" In=\"%s\"/>"
                        CR "<atXml:%s name=\"%s\"%s%s",
                RefPort, s_Chan, mixedMode, upperMode, mixedMode,
                RefIn, Sync);
        strcat(SigDesc, string);

        // append modifiers
        if (m_Angle[s_Chan].Exists)
        {
            sprintf(string, " angle=\"%le deg\"", m_Angle[s_Chan].Real);
            strcat(SigDesc, string);
        }

        if (m_AngleRate[s_Chan].Exists)
        {
            sprintf(string, " angle_rate=\"%le deg/s\"", m_AngleRate[s_Chan].Real);
            strcat(SigDesc, string);
        }

        if (m_VoltageOut[s_Chan].Exists)
        {
            sprintf(string, " voltage=\"%le V\"", m_VoltageOut[s_Chan].Real);
            strcat(SigDesc, string);
        }

        if (m_RefVoltSim[s_Chan].Exists)
        {
            sprintf(string, " ref_volt=\"%le V\"", m_RefVoltSim[s_Chan].Real);
            strcat(SigDesc, string);
        }

        if (m_FreqSim[s_Chan].Exists)
        {
            sprintf(string, " freq=\"%le Hz\"", m_FreqSim[s_Chan].Real);
            strcat(SigDesc, string);
        }

        /* SpeedRatio?? */
        if (m_RatioSim[s_Chan].Exists)
        {
            sprintf(string, " speed_ratio=\"%d\"", m_RatioSim[s_Chan].Int);
            strcat(SigDesc, string);
        }
    }

    // terminate XML statement 
    strcat (SigDesc,    "/>"
                     CR "</Signal>");

    IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Setup", s_Resource,
            m_MaxTime[s_Chan].Exists ? m_MaxTime[s_Chan].Real : MAXTIME,
            SigDesc, NULL, 0)));
    CEMDEBUG(9, cs_FmtMsg("IssueSignal (%s)\n%s", s_Resource, SigDesc));

    return (0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateSrs(int Fnc)
//
// Purpose: Perform the Initiate action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::InitiateSrs(int Fnc)
{
    int   Status = 0;

    GetChan(Fnc);

    // Initiate action for the Srs
    CEMDEBUG(5, cs_FmtMsg("InitiateSrs (%s) called FNC %d", 
            s_Resource, Fnc));

    if (s_Chan == 0)
    {
        GetStmtInfoSrs(Fnc);    // may need to get additional info (e.g., MAX-TIME) on Initiate
        return (0);             // nothing else to do on events
    }

    IFNSIM((Status = cs_IssueAtmlSignal("Enable", s_Resource, "", NULL, 0)));

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchSrs(int Fnc)
//
// Purpose: Perform the Fetch action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::FetchSrs(int Fnc)
{
    DATUM  *fdat;
    int     Status = 0;
    double  MeasValue = 0.0;
    char    SigDesc[1024];
    char    XmlValue[1024] = "";
    char    MeasFunc[32] = "";
    char    Speed[30] = "";
    
    GetChan(Fnc);

    // Fetch action for the Srs
    CEMDEBUG(5, cs_FmtMsg("FetchSrs (%s) called FNC %d", 
                          s_Resource, Fnc));

    // don't think this should be necessary
    if (s_Chan == 0)
    {
        CEMDEBUG(10, cs_FmtMsg("Fetch on an Event... %s FNC %d", s_Resource, Fnc));
        return (0);    // nothing to do on events
    }

    strcpy(MeasFunc, Fnc & RATE_MASK ? "angle_rate" : "angle");

    if (m_RatioInd[s_Chan].Exists)
        sprintf(Speed, " speed_ratio=\"%d\"", m_RatioInd[s_Chan].Int);

    sprintf(SigDesc,    "<Signal Out=\"Meas\">"
                     CR "<Measure name=\"Meas\" samples=\"1\" attribute=\"%s\"%s In=\"Ch%d\"/>"
                     CR "</Signal>",
            MeasFunc, Speed, s_Chan);

    IFNSIM((Status = cs_IssueAtmlSignal("Fetch", s_Resource, SigDesc, XmlValue, 1024)));
    CEMDEBUG(9, cs_FmtMsg("IssueSignal (%s)\n%s\n  returns\n%s", s_Resource, SigDesc, XmlValue));

    if (Status)
    {
        MeasValue = FLT_MAX;
     }
    else
    {
        IFNSIM(cs_GetSingleDblValue(XmlValue, MeasFunc, &MeasValue));
    }

    fdat = FthDat();
    DECDatVal(fdat, 0) = MeasValue;
    FthCnt(1);

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: OpenSrs(int Fnc)
//
// Purpose: Perform the Open action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::OpenSrs(int Fnc)
{
    int   Status = 0;

    GetChan(Fnc);

    // Open action for the Srs
    CEMDEBUG(5, cs_FmtMsg("OpenSrs (%s) called FNC %d", 
            s_Resource, Fnc));

    if (s_Chan == 0) return (0);    // nothing to do on events

    IFNSIM((Status = cs_IssueAtmlSignal("Disable", s_Resource, "", NULL, 0)));

    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseSrs(int Fnc)
//
// Purpose: Perform the Close action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::CloseSrs(int Fnc)
{
    int   Status = 0;

    GetChan(Fnc);

    // Close action for the Srs
    CEMDEBUG(5, cs_FmtMsg("CloseSrs (%s) called FNC %d", 
            s_Resource, Fnc));

    if (s_Chan == 0) return (0);    // nothing to do on events

    IFNSIM((Status = cs_IssueAtmlSignal("Enable", s_Resource, "", NULL, 0)));
    
    return (0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetSrs(int Fnc)
//
// Purpose: Perform the Reset action for this driver instance
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             The Allocated FNC code
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::ResetSrs(int Fnc)
{
    int   Status = 0;

    GetChan(Fnc);

    // Reset action for the Srs
    CEMDEBUG(5, cs_FmtMsg("ResetSrs (%s) called FNC %d", 
            s_Resource, Fnc));

    if (s_Chan == 0)
    {
        // make the associated event record available
        CEMDEBUG(10, cs_FmtMsg("  Marking event slot for %d as available", Fnc));
        m_Events[FindEventByFnc(Fnc)].Active = false;

        return (0);          // nothing else to do on events
    }

    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if (Fnc != 0)
    {
        // Reset the Srs
        IFNSIM((Status = cs_IssueAtmlSignal("Reset", s_Resource, "", NULL, 0)));
        InitPrivateSrs(s_Chan);
    }
    else
        InitPrivateSrs(0);

    return (0);
}


//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoSrs(int Fnc)
//
// Purpose: Get the Modifier values from the ATLAS Statement
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             Device Database Function value
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return:
//    zero - All OK.
//    <0   - Error occured and diagnosed
//
///////////////////////////////////////////////////////////////////////////////
int CSrs_T::GetStmtInfoSrs(int Fnc)
{
    DATUM    *datum;
    int       eventNum;
    int       event;

    if (Fnc & EVENT_MASK)
    {
        // event actions

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
              case FNC_SYNC_EXT:
                strcpy(m_Events[event].Source, "EXT");
                break;
              case FNC_SYNC_TTL:
                strcpy(m_Events[event].Source, "TTL");
                break;
            }

            // EVENT-SLOPE
            if ((datum = RetrieveDatum(M_EVSL, K_SET)) ||
                (datum = RetrieveDatum(M_EVSL, K_SRX)) ||
                (datum = RetrieveDatum(M_EVSL, K_SRN)) )
            {
                m_Events[event].Slope.Exists = true;
                m_Events[event].Slope.Int = strcmp(GetTXTDatVal(datum, 0), "POS") == 0 ? +1 : -1;
                CEMDEBUG(10, cs_FmtMsg("Found EVENT-SLOPE: %d", m_Events[event].Slope.Int));
                FreeDatum (datum);
            }
        }
    }
    else
    {
        // "regular" actions

        if (s_App == STIMULUS)
        {
            // ANGLE
            if ((datum = RetrieveDatum(M_ANGL, K_SET)) ||
                (datum = RetrieveDatum(M_ANGL, K_SRX)) ||
                (datum = RetrieveDatum(M_ANGL, K_SRN)) )
            {
                m_Angle[s_Chan].Exists = true;
                m_Angle[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Sim ANGLE %lf", m_Angle[s_Chan].Real));
                FreeDatum (datum);
            }

	        // ANGLE-RATE
            if ((datum = RetrieveDatum(M_ANRT, K_SET)) ||
                (datum = RetrieveDatum(M_ANRT, K_SRX)) ||
                (datum = RetrieveDatum(M_ANRT, K_SRN)) )
            {
                m_AngleRate[s_Chan].Exists = true;
                m_AngleRate[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Sim ANGLE-RATE %lf", m_AngleRate[s_Chan].Real));
                FreeDatum (datum);
            }

 	        // VOLTAGE
            if ((datum = RetrieveDatum(M_VOLT, K_SET)) ||
                (datum = RetrieveDatum(M_VOLT, K_SRX)) ||
                (datum = RetrieveDatum(M_VOLT, K_SRN)) )
            {
                m_VoltageOut[s_Chan].Exists = true;
                m_VoltageOut[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Sim VOLTAGE %lf", m_VoltageOut[s_Chan].Real));
                FreeDatum (datum);
            }

	        // FREQ
            if ((datum = RetrieveDatum(M_FREQ, K_SET)) ||
                (datum = RetrieveDatum(M_FREQ, K_SRX)) ||
                (datum = RetrieveDatum(M_FREQ, K_SRN)) )
            {
                m_FreqSim[s_Chan].Exists = true;
                m_FreqSim[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Sim FREQ %lf", m_FreqSim[s_Chan].Real));
                FreeDatum (datum);
            }

	        // SYNC-TO-EVENT
            if ((datum = RetrieveDatum(M_SYNC, K_SET)) ||
                (datum = RetrieveDatum(M_SYNC, K_SRX)) ||
                (datum = RetrieveDatum(M_SYNC, K_SRN)) )
            {
                m_SyncToEvent[s_Chan].Exists = true;
                m_SyncToEvent[s_Chan].Int = FindEventByNum(INTDatVal(datum, 0));
                CEMDEBUG(10, cs_FmtMsg("Found Sim SYNC-TO-EVENT %d", m_SyncToEvent[s_Chan].Int));
                FreeDatum (datum);
            }

	        // VOLTAGE-REF
            if ((datum = RetrieveDatum(M_REFV, K_SET)) ||
                (datum = RetrieveDatum(M_REFV, K_SRX)) ||
                (datum = RetrieveDatum(M_REFV, K_SRN)) )
            {
                m_RefVoltSim[s_Chan].Exists = true;
                m_RefVoltSim[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Sim VOLTAGE-REF %lf", m_RefVoltSim[s_Chan].Real));
                FreeDatum (datum);
            }

	        // SPEED-RATIO
            if (s_Multi)
            {
                if ((datum = RetrieveDatum(M_SPRT, K_SET)) ||
                    (datum = RetrieveDatum(M_SPRT, K_SRX)) ||
                    (datum = RetrieveDatum(M_SPRT, K_SRN)) )
                {
                    m_RatioSim[s_Chan].Exists = true;
                    m_RatioSim[s_Chan].Int = (int)DECDatVal(datum, 0);
                    CEMDEBUG(10, cs_FmtMsg("Found Sim SPEED-RATIO %d", m_RatioSim[s_Chan].Int));
                    FreeDatum (datum);
                }
            }
        }
        else if (s_App == MEASUREMENT)
        {
	        // FREQ
            if ((datum = RetrieveDatum(M_FREQ, K_SET)) ||
                (datum = RetrieveDatum(M_FREQ, K_SRX)) ||
                (datum = RetrieveDatum(M_FREQ, K_SRN)) )
            {
                m_FreqInd[s_Chan].Exists = true;
                m_FreqInd[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Ind FREQ %lf", m_FreqInd[s_Chan].Real));
                FreeDatum (datum);
            }

            // MAX-TIME
            if ((datum = RetrieveDatum(M_MAXT, K_SET)) ||
                (datum = RetrieveDatum(M_MAXT, K_SRX)) ||
                (datum = RetrieveDatum(M_MAXT, K_SRN)) )
            {
                m_MaxTime[s_Chan].Exists = true;
                m_MaxTime[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found MAX-TIME %lf", m_MaxTime[s_Chan].Real));
                FreeDatum (datum);
            }

	        // VOLTAGE-REF
            if ((datum = RetrieveDatum(M_REFV, K_SET)) ||
                (datum = RetrieveDatum(M_REFV, K_SRX)) ||
                (datum = RetrieveDatum(M_REFV, K_SRN)) )
            {
                m_RefVoltInd[s_Chan].Exists = true;
                m_RefVoltInd[s_Chan].Real = DECDatVal(datum, 0);
                CEMDEBUG(10, cs_FmtMsg("Found Ind VOLTAGE-REF %lf", m_RefVoltInd[s_Chan].Real));
                FreeDatum (datum);
            }

	        // SPEED-RATIO
            if (s_Multi)
            {
                if ((datum = RetrieveDatum(M_SPRT, K_SET)) ||
                    (datum = RetrieveDatum(M_SPRT, K_SRX)) ||
                    (datum = RetrieveDatum(M_SPRT, K_SRN)) )
                {
                    m_RatioInd[s_Chan].Exists = true;
                    m_RatioInd[s_Chan].Int = (int)DECDatVal(datum, 0);
                    CEMDEBUG(10, cs_FmtMsg("Found Ind SPEED-RATIO %d", m_RatioInd[s_Chan].Int));
                    FreeDatum (datum);
                }
            }
        }

	    // REF-SOURCE
        if ((datum = RetrieveDatum(M_REFS, K_SET)) ||
            (datum = RetrieveDatum(M_REFS, K_SRX)) ||
            (datum = RetrieveDatum(M_REFS, K_SRN)) )
        {
            if (strcmp(GetTXTDatVal(datum, 0), "INT") == 0)
                *s_ExtRefPtr = false;
            else
                *s_ExtRefPtr = true;
            CEMDEBUG(10, cs_FmtMsg("Found REF-SOURCE %s", *s_ExtRefPtr ? "EXT" : "INT"));
            FreeDatum (datum); 
        }
        else
            *s_ExtRefPtr = false;           // internal by default
    }

    return (0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateSrs()
//
// Purpose: Initialize/Reset all private modifier variables
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CSrs_T::InitPrivateSrs(int Chan)
{
    int startCh = 0;
    int stopCh = CHANNELS - 1;

    if (Chan != 0)
        startCh = stopCh = Chan;
    else
    {
        for (int i = 0; i < MAX_EVENTS; i++)
        {
            m_Events[i].Fnc = 0;
            m_Events[i].EventNum = 0;
            m_Events[i].Active = false;
            strcpy(m_Events[i].Source, "");
            m_Events[i].Slope.Exists = false;
        }
    }

    for (int ch = startCh; ch <= stopCh; ch++)
    {
        if (s_App == 0 || s_App == STIMULUS)
        {
            CEMDEBUG(10, cs_FmtMsg("Initializing data for SIM Ch %d", ch));

            m_Angle[ch].Exists = false;
            m_AngleRate[ch].Exists = false;
            m_VoltageOut[ch].Exists = false;
            m_SyncToEvent[ch].Exists = false;
            m_FreqSim[ch].Exists = false;
            m_RefVoltSim[ch].Exists = false;
            m_RatioSim[ch].Exists = false;
            m_ExtRefSim[ch] = true;
        }

        if (s_App == 0 || s_App == MEASUREMENT)
        {
            CEMDEBUG(10, cs_FmtMsg("Initializing data for IND Ch %d", ch));

            m_FreqInd[ch].Exists = false;
            m_RefVoltInd[ch].Exists = false;
            m_RatioInd[ch].Exists = false;
            m_ExtRefInd[ch] = true;
            m_MaxTime[ch].Exists = false;
        }
    }

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataSrs()
//
// Purpose: Initialize/Reset calibration data
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CSrs_T::NullCalDataSrs(void)
{
    //////// Place Srs specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE */
    m_CalData[0] = 1.0;
    m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
    //////////////////////////////////////////////////////////
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: GetChan()
//
// Purpose: Set s_Chan based on the FNC
//
// Input Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
// Fnc              int             Device Database Function value
// 
// Output Parameters
// Parameter        Type            Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void CSrs_T::GetChan(int Fnc)
{
    if (Fnc & EVENT_MASK)
    {
        // events
        s_Chan  = 0;
        s_Multi = false;
    }
    else
    {
        s_Chan  = (Fnc & CHAN_MASK) >> 3;
        s_Multi = (Fnc & MULTI_MASK) != 0;
    }

    s_Mode      = Fnc & RES_MASK ? RESOLVER : SYNCHRO;
    s_App       = Fnc & STIM_MASK ? STIMULUS : MEASUREMENT;
    s_Resource  = (s_App == STIMULUS ? m_ResourceSim[s_Chan] : m_ResourceInd[s_Chan]);
    s_ExtRefPtr = (s_App == STIMULUS ? &(m_ExtRefSim[s_Chan]) : &(m_ExtRefInd[s_Chan]));
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
inline int CSrs_T::FindAvailEvent(void)
{
    int event;

    for (event = 0; event < MAX_EVENTS; event++)
        if (!m_Events[event].Active)
        {
//          CEMDEBUG(10, cs_FmtMsg("Found avail event slot %d", event));
            return (event);
        }

    // oops! can't find one
    CEMERROR(EB_SEVERITY_WARNING + EB_ACTION_HALT, cs_FmtMsg("ERROR - no available m_Event slots"));
    return (-1);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FindEventByNum()
//
// Purpose: Find the m_Event record corresponding to given event number
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// num              int             event number
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
inline int CSrs_T::FindEventByNum(int num)
{
    int event;

    for (event = 0; event < MAX_EVENTS; event++)
        if (m_Events[event].EventNum == num)
        {
//          CEMDEBUG(10, cs_FmtMsg("Event %d corresponds to event number %d", event, num));        
            return (event);
        }

    // oops! can't find it
    CEMERROR(EB_SEVERITY_WARNING + EB_ACTION_HALT, cs_FmtMsg("Can't find event number %d", num));
    return (-1);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FindEventByFnc()
//
// Purpose: Find the m_Event record corresponding to given FNC
//
// Input Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
// fnc              int             Device Database Function value
//
// Output Parameters
// Parameter            Type                    Purpose
// ===============  ==============  ===========================================
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
inline int CSrs_T::FindEventByFnc(int fnc)
{
    int event;

    for (event = 0; event < MAX_EVENTS; event++)
    {
//      CEMDEBUG(10, cs_FmtMsg("Event %d FNC %d, looking for %d", event, m_Events[event].Fnc, fnc));        
        if (m_Events[event].Fnc == fnc)
        {
            CEMDEBUG(10, cs_FmtMsg("Event %d corresponds to FNC %d", event, fnc));        
            return (event);
        }
    }

    // oops! can't find it
    CEMERROR(EB_SEVERITY_WARNING + EB_ACTION_HALT, cs_FmtMsg("Can't find event with FNC %d", fnc));
    return (-1);
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


