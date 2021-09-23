//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_Can_T.cpp
//
// Date:	    26-JUN-06
//
// Purpose:	    Instrument Driver for Bus_Can
//
// Instrument:	Bus_Can  <Device Description> (<device Type>)
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
// 1    bus protocol
// 2    exchange
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  26JUN06  Baseline								  D. Bubenik, EADS North America Defense
// 1.1.0.0  04APR19  Corrected bus errors due to memory faults  Jim Witcher Astronics Test Systems
// 1.1.0.1  30MAY19  Corrected Start & Decode & timeouts        Jim Witcher Astronics Test Systems
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Bus_Can_T.h"

// Local Defines
#define MIC 1
#define CAN 2

// Function codes

#define CAL_TIME (86400 * 365) /* one year */

// Static Variables
char BusName[2][10]={"CAN_1", "CAN_1"};

// Local Function Prototypes
void stuff_string(char * string);
void unstuff_string(char * string);
void array_to_bin_string(int array [], int size, int length, char output []);
void bin_string_to_array(char input [], int length, int array [], int * size);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CBus_Can_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CBus_Can_T::CBus_Can_T(char *DeviceName, int Bus, int PrimaryAdr, int SecondaryAdr, int Dbg, int Sim)
{
    m_Bus = Bus;
    m_PrimaryAdr = PrimaryAdr;
    m_SecondaryAdr = SecondaryAdr;
    m_Dbg = Dbg;
    m_Sim = Sim;
    m_Handle= NULL;
    int Status = 0;

    if(DeviceName)
	{
        strcpy(m_DeviceName,DeviceName);
	}

    InitPrivateBus_Can();
	NullCalDataBus_Can();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Bus_Can Class called with Device [%s], Sim %d, Dbg %d", DeviceName, Sim, Dbg));

    // Initialize the Bus_Can - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(BusName[0], CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);
	Status = cs_GetUniqCalCfg(BusName[1], CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CBus_Can_T()
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
CBus_Can_T::~CBus_Can_T()
{
    // Reset the Bus_Can
    CEMDEBUG(5,cs_FmtMsg("Bus_Can Class Distructor called for Device [%s], ", m_DeviceName));

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusBus_Can(int Fnc)
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
int CBus_Can_T::StatusBus_Can(int Fnc)
{
    int Status = 0;

    // Status action for the Bus_Can
    CEMDEBUG(5,cs_FmtMsg("StatusBus_Can (%s) called FNC %d", m_DeviceName, Fnc));

    // Check for any pending error messages
	if(Fnc%100<10)
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Status", BusName[0], 
			"<Signal name=\"\" Out=\"Meas\" In=\"Cha Ext Chb\">\n"
				"<Instantaneous name=\"Meas\" "
					"In=\"Cha\" attribute=\"ac_ampl\"/>\n" 
			"</Signal>\n",
			NULL, 0)));
		CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %d", BusName[0],Status));
	}
	else
	{
		IFNSIM((Status = cs_IssueAtmlSignal("Status", BusName[1], 
			"<Signal name=\"\" Out=\"Meas\" In=\"Cha Ext Chb\">\n"
				"<Instantaneous name=\"Meas\" "
					"In=\"Cha\" attribute=\"ac_ampl\"/>\n" 
			"</Signal>\n",
			NULL, 0)));
		CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %d", BusName[1],Status));
	}

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupBus_Can_T(int Fnc)
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
int CBus_Can_T::SetupBus_Can(int Fnc)
{
    int       Status = 0;

	// This conditional statement was not in the legacy driver - JLW
	if( m_initiate )		// cjw 20190521
	{
		m_initiate = false;
	}

    // Setup action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("SetupBus_Can (%s) called FNC %d", m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
	{
        return(0);
	}

    if((Status = GetStmtInfoBus_Can(Fnc)) != 0)
	{
        return(0);
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateBus_Can(int Fnc)
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
int CBus_Can_T::InitiateBus_Can(int Fnc)
{
    int   Status = 0;
	// Initiate action for the Bus_1553
	CEMDEBUG(5,cs_FmtMsg("InitiateBus_Can (%s) called FNC %d", m_DeviceName, Fnc));

	if(m_initiate) //initiate has  been previously executed, start transfer
	{
		//SendSetupMods(Fnc, "Setup");
		// Internal Trigger
		IFNSIM((Status = cs_IssueAtmlSignal("Enable", BusName[1], m_SignalDescription, NULL, 0)));
		m_initiate=false;
	}
	else //this is the first initiate, setup all the parameters
	{
		SendSetupMods(Fnc, "Setup");
		m_initiate=true;
	}

	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchBus_Can(int Fnc)
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
int CBus_Can_T::FetchBus_Can(int Fnc)
{
    int     Status = 0;
	int     retDataCnt;
	//int   ReturnCount = 0;
    double  MaxTime = 5;
    char    MeasFunc[32];
	char    MeasValue[10000];
    DATUM   *fdat;


    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real;
	}

    // Fetch action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("FetchBus_Can (%s) called FNC %d", m_DeviceName, Fnc));

    // Fetch data
	fdat = FthDat();
	m_Attribute.Dim=FthMod();

	switch(m_Attribute.Dim)
	{
		case M_DATA:
			strcpy(MeasFunc,"data");
			break;
	}

	m_Exnm.Int=FthQual();
	m_Attribute.Exists=true;
	SendSetupMods(Fnc, "Fetch");

	retDataCnt=DatCnt(fdat);

	MeasValue[0]='\0';

    if(Status)
    {
        //MeasValue = FLT_MAX;
    }
    else
    {	
		IFNSIM(cs_GetStringValue(m_XmlValue, MeasFunc, MeasValue, 10000));
		//IFNSIM(cs_GetSingleIntValue(m_XmlValue, MeasFunc, &ReturnCount));
    }

	unstuff_string(MeasValue);

	//return data
	for (int j = 0; j < retDataCnt; j++)
    {
		PutTXTDatVal(fdat, j, MeasValue);
	}

    FthCnt(retDataCnt);

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: OpenBus_Can(int Fnc)
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
int CBus_Can_T::OpenBus_Can(int Fnc)
{
    // Open action for the Bus_Can
    CEMDEBUG(5,cs_FmtMsg("OpenBus_Can (%s) called FNC %d", m_DeviceName, Fnc));

    //////// Place Bus_Can specific data here //////////////

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseBus_Can(int Fnc)
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
int CBus_Can_T::CloseBus_Can(int Fnc)
{
    // Close action for the Bus_Can
    CEMDEBUG(5,cs_FmtMsg("CloseBus_Can (%s) called FNC %d", m_DeviceName, Fnc));

    //////// Place Bus_Can specific data here //////////////
    
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetBus_Can(int Fnc)
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
int CBus_Can_T::ResetBus_Can(int Fnc)
{
    int   Status = 0;

    // Reset action for the Bus_Can
    CEMDEBUG(5,cs_FmtMsg("ResetBus_Can (%s) called FNC %d", m_DeviceName, Fnc));
    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if(Fnc != 0)
    {
		// Reset the Bus_Can
		if(Fnc%100<10)
		{
        	IFNSIM((Status = cs_IssueAtmlSignal("Reset", BusName[0], m_SignalDescription, NULL, 0)));
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal("Reset", BusName[1], m_SignalDescription, NULL, 0)));
		}
    }
	else
	{
		//char* sigDesc = "<Signal Name=\"BUS_SIGNAL\" Out=\"exchange\"><CAN name=\"exchange\" timingValue=\"20000Hz\" threeSamples=\"0\" singleFilter=\"1\" acceptanceCode=\"LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL\" acceptanceMask=\"HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH\" listenOnly=\"0\" channel=\"1\" data_bits=\"FEDC,1,FE,DC,BA,98,76,54,32,10\" maxTime=\"20s\" /></Signal>";
		
		char* sigDesc = "<Signal Name=\"BUS_SIGNAL\" Out=\"exchange\">\n<CAN name=\"exchange\" timingValue=\"20000Hz\" threeSamples=\"0\" singleFilter=\"1\" acceptanceCode=\"LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL\" acceptanceMask=\"HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH\" listenOnly=\"0\" channel=\"1\" data_bits=\"FEDC,1,FE,DC,BA,98,76,54,32,10\" maxTime=\"2s\" /></Signal>";
		IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, sigDesc, NULL, 0)));
		sigDesc = "<Signal Name=\"BUS_SIGNAL\" Out=\"exchange\">\n<CAN name=\"exchange\" timingValue=\"20000Hz\" threeSamples=\"0\" singleFilter=\"0\" acceptanceCode=\"LLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL\" acceptanceMask=\"HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH\" listenOnly=\"0\" channel=\"2\" data_bits=\"ABCD,1,AB,CD,EF,01,23,45,56,89\" maxTime=\"2s\" /></Signal>";
		IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, sigDesc, NULL, 0)));
	}
	
    InitPrivateBus_Can();

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoBus_Can(int Fnc)
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
int CBus_Can_T::GetStmtInfoBus_Can(int Fnc)
{
	DATUM    *datum;
    char *cp;//, *temp;
    int size;
	unsigned char byte;

    // BUS-SPEC
    if((datum = GetDatum(M_BUSS, K_SET)))
    {
        m_BusSpec.Exists = true;
        m_BusSpec.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Bus-Spec %d",m_BusSpec.Dim));
    }
	
	// DATA
	if((datum = RetrieveDatum(M_DATA, K_SET)))
    {
        m_DataSize.Exists = true;
        m_DataSize.Int = DatCnt (datum);   //get # of vars
        
		cp = GetTXTDatVal(datum, 0);
		strcpy(m_Data, cp);
		CEMDEBUG(10,  cs_FmtMsg("Found Data: %s",m_Data));
   
		FreeDatum(datum);
    }

	// COMMAND
	if((datum = RetrieveDatum(M_COMD, K_SET)))
    {
        m_Command.Exists = true;

		if(DatTyp(datum)==DIGV)
		{
            m_Command.Int=0;
            cp = DIGDatVal(datum,0);
            size=DatSiz(datum);

	        for(int j=0;j<size;j++)
            {
                byte = DIGDatByte(cp,j);    
                m_Command.Int=m_Command.Int| (byte << (size*8- 8*(j+1)));
            }

            CEMDEBUG(10,  cs_FmtMsg("Command word is 0x%X", m_Command.Int));
        }
        else
		{
            CEMDEBUG(10,cs_FmtMsg("Command: Data Type Unknown"));
		}

		FreeDatum(datum);
    }

	// BUS-MODE
	if((datum = GetDatum(M_BUSM, K_SET)))
    {
        m_BusMode.Exists = true;
        m_BusMode.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Bus-Mode %d",m_BusMode.Dim));
    }
	
	// PROCEED
	if(RemoveMod(M_PRCD))
    {
        m_Proceed.Exists = true;
        CEMDEBUG(10,"Found Proceed");
    }
	
	// WAIT
	if(RemoveMod(M_WAIT))
    {
        m_Wait.Exists = true;
        CEMDEBUG(10,"Found Wait");
    }

	// MAX-TIME
	if((datum = GetDatum(M_MAXT, K_SET)))
    {
        m_MaxTime.Exists = true;
        m_MaxTime.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Max-Time %lf",m_MaxTime.Real));
    }

	// EXNM
	if((datum = GetDatum(M_EXNM, K_SET)))
    {
        m_Exnm.Exists = true;
		m_Exnm.Int = INTDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Exnm %d",m_Exnm.Int));
    }

	// BUS-TIMEOUT
	if((datum = GetDatum(M_BSTO, K_SET)))
    {
        m_BusTimeout.Exists = true;
        m_BusTimeout.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Bus-Timeout %d",m_BusTimeout.Int));
    }

	// NO-COMMAND-TIMEOUT 
	if((datum = GetDatum(M_NCTO, K_SET)))
    {
        m_NoCommandTimeout.Exists = true;
        m_NoCommandTimeout.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found No-Command-Timeout %d",m_NoCommandTimeout.Int));
    }

	// NO-RESPONSE-TIMEOUT 
	if((datum = GetDatum(M_NRTO, K_SET)))
    {
        m_NoResponseTimeout.Exists = true;
        m_NoResponseTimeout.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found No-Response-Timeout %d",m_NoResponseTimeout.Int));
    }

	//INTERRUPT-ACK-TIMEOUT 
	if((datum = GetDatum(M_IATO, K_SET)))
    {
		m_InterruptAckTimeout.Exists = true;
        m_InterruptAckTimeout.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Interrupt-Ack-Timeout %d",m_InterruptAckTimeout.Int));
    }

	//BASE-VECTOR 
	if((datum = GetDatum(M_BSVT, K_SET)))
    {
        m_BaseVector.Exists = true;
        m_BaseVector.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Base-Vector %d",m_BaseVector.Int));
    }

	//ADDRESS-REG 
	if((datum = GetDatum(M_ADRG, K_SET)))
    {
        m_AddressReg.Exists = true;
        m_AddressReg.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Address-Reg %d",m_AddressReg.Int));
    }

	//TIMING-VALUE 
	if((datum = GetDatum(M_TMVL, K_SET)))
    {
        m_TimingValue.Exists = true;
        m_TimingValue.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Timing-Value %d",m_TimingValue.Int));
    }

	//THREE-SAMPLES 
	if((datum = GetDatum(M_THSM, K_SET)))
    {
        m_ThreeSamples.Exists = true;
        m_ThreeSamples.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Three-Samples %d",m_ThreeSamples.Int));
    }

	//SINGLE-FILTER 
	if((datum = GetDatum(M_SNFL, K_SET)))
    {
        m_SingleFilter.Exists = true;
        m_SingleFilter.Int = (int)DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Single-Filter %d",m_SingleFilter.Int));
    }

	//ACCEPTANCE-CODE 
	if((datum = RetrieveDatum(M_ACCD, K_SET)))
    {
        m_AcceptanceCode.Exists = true;

		if(DatTyp(datum)==DIGV)
		{
            m_AcceptanceCode.Int=0;
            cp = DIGDatVal(datum,0);
            size=DatSiz(datum);

	        for(int j=0;j<size;j++)
            {
                byte = DIGDatByte(cp,j);    
                m_AcceptanceCode.Int=m_AcceptanceCode.Int| (byte << (size*8- 8*(j+1)));
            }

            CEMDEBUG(10,  cs_FmtMsg("Acceptance-Code is 0x%X", m_AcceptanceCode.Int));
        }
        else
		{
            CEMDEBUG(10,cs_FmtMsg("AcceptanceCode: Data Type Unknown"));
		}

		FreeDatum(datum);
    }

	//ACCEPTANCE-MASK 
	if((datum = RetrieveDatum(M_ACMK, K_SET)))
    {
        m_AcceptanceMask.Exists = true;

		if(DatTyp(datum)==DIGV)
		{
            m_AcceptanceMask.Int=0;
            cp = DIGDatVal(datum,0);
            size=DatSiz(datum);

	        for(int j=0;j<size;j++)
            {
                byte = DIGDatByte(cp,j);    
                m_AcceptanceMask.Int=m_AcceptanceMask.Int| (byte << (size*8- 8*(j+1)));
            }

            CEMDEBUG(10,  cs_FmtMsg("Acceptance-Mask is 0x%X", m_AcceptanceMask.Int));
        }
        else
		{
            CEMDEBUG(10,cs_FmtMsg("AcceptanceMask: Data Type Unknown"));
		}

		FreeDatum(datum);
    }

//	//TEST-EQUIP LISTENER OR UUT TALKER 
//	if((datum = GetDatum(M_TEQL, K_SET)) || (datum = GetDatum(M_UUTT, K_SET)))	// M_UUTL
//    {
//        m_Talker.Exists = true;
//        m_Talker.Int = 1;	//CJW (int)DECDatVal(datum, 0);
//        CEMDEBUG(10,cs_FmtMsg("Found Test-Equip Talker %d",m_Talker.Int));
//    }

	//TEST-EQUIP TALKER
	if( IsMod(M_TEQT))	// M_TEQL
    {
        m_Talker.Exists = true;
		RemoveMod(M_TEQT);
        m_Talker.Int = 1;	// _TL_TEQT	 //CJW (int)DECDatVal(datum, 0);
        m_TalkerListener.Exists = true;
        m_TalkerListener.Int |= _TL_TEQT;	//CJW
        CEMDEBUG(10,cs_FmtMsg("Found Test-Equip Talker %d",m_TalkerListener.Int));
    }

	////TEST-EQUIP LISTENER
	if( IsMod(M_TEQL))	// M_TEQL
    {
        m_TalkerListener.Exists = true;
		RemoveMod(M_TEQL);
        m_TalkerListener.Int |= _TL_TEQL;	//CJW
        CEMDEBUG(10,cs_FmtMsg("Found Test-Equip Listener %d",m_TalkerListener.Int));
    }

	if( IsMod(M_UUTT))	// M_UUT Talker
    {
        m_Talker.Exists = true;
		RemoveMod(M_UUTT);
        m_Talker.Int = 1;	//CJW
        m_TalkerListener.Exists = true;
        m_TalkerListener.Int |= _TL_UUTT;	//CJW 
        CEMDEBUG(10,cs_FmtMsg("Found UUT Talker %d",m_TalkerListener.Int));
    }

	if( IsMod(M_UUTL))	// M_UUT Listener
    {
        m_TalkerListener.Exists = true;
		RemoveMod(M_UUTL);
        m_TalkerListener.Int |= _TL_UUTL;	//CJW 
        CEMDEBUG(10,cs_FmtMsg("Found UUT Listener %d",m_TalkerListener.Int));
    }


    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_Can()
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
void CBus_Can_T::InitPrivateBus_Can(void)
{
	m_BusSpec.Exists=false;
	m_DataSize.Exists=false;
    m_BusMode.Exists=false;
    m_Proceed.Exists=false;
    m_Wait.Exists=false;
	m_MaxTime.Exists=false;
	m_Exnm.Exists=false;
	m_Attribute.Exists=false;
	m_initiate=false;
    m_BusTimeout.Exists=false;
    m_NoCommandTimeout.Exists=false;
    m_NoResponseTimeout.Exists=false;
	m_InterruptAckTimeout.Exists=false;
	m_BaseVector.Exists=false;
	m_AddressReg.Exists=false;
	m_TimingValue.Exists=false;
	m_ThreeSamples.Exists=false;
	m_SingleFilter.Exists=false;
	m_AcceptanceCode.Exists=false;
	m_AcceptanceMask.Exists=false;
	m_Talker.Exists=false;
	m_Talker.Int = 0;
	m_TalkerListener.Exists=false;
	m_TalkerListener.Int = 0;
	
	//memset(m_XmlValue, '\0', sizeof(m_XmlValue));
	//memset(m_SignalDescription, '\0', sizeof(m_SignalDescription));

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_Can()
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
void CBus_Can_T::NullCalDataBus_Can(void)
{
    //////// Place Bus_Can specific data here //////////////
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
    //////////////////////////////////////////////////////////
    return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: SendSetupMods()
//
// Purpose: Creates xml string of all the modifiers and call IssueAtmlSignal using Setup
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
void CBus_Can_T::SendSetupMods(int Fnc, char type [])
{
    int       Status = 0;
	char stringinput[1024];
	char tempstring[768];
	char tempWord[133];
    
    CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully called"));

	sprintf(stringinput, "<Signal Name=\"BUS_SIGNAL\" Out=\"exchange\">\n");

	if(Fnc%100)
	{
		strcat(stringinput, "<CAN name=\"exchange\" ");

		/*if(m_NoResponseTimeout.Exists)
		{
			sprintf(tempstring, "noResponseTimeout=\"%ds\" ", m_NoResponseTimeout.Int);
			strcat(stringinput,tempstring);
		}*/

		if(m_TimingValue.Exists)
		{
			sprintf(tempstring, "timingValue=\"%dHz\" ", m_TimingValue.Int);
			strcat(stringinput,tempstring);
		}

        if(m_ThreeSamples.Exists)
		{
			sprintf(tempstring, "threeSamples=\"%d\" ", m_ThreeSamples.Int);
			strcat(stringinput,tempstring);
		}

        if(m_SingleFilter.Exists)
		{
			sprintf(tempstring, "singleFilter=\"%d\" ", m_SingleFilter.Int);
			strcat(stringinput,tempstring);
		}

        if(m_AcceptanceCode.Exists)
		{
			array_to_bin_string(&m_AcceptanceCode.Int, 1, 32, tempWord);
			tempWord[32] = '\0';	//CJW Test Code
			sprintf(tempstring, "acceptanceCode=\"%s\" ", tempWord);
			strcat(stringinput,tempstring);
		}

        if(m_AcceptanceMask.Exists)
		{
			array_to_bin_string(&m_AcceptanceMask.Int, 1, 32, tempWord);
			tempWord[32] = '\0';	//CJW Test Code
			sprintf(tempstring, "acceptanceMask=\"%s\" ", tempWord);
			strcat(stringinput,tempstring);
		}

        if(m_Talker.Exists)
		{			
			sprintf(tempstring, "listenOnly=\"%d\" ", 0);	// default to talker (0)
			strcat(stringinput,tempstring);
		}
		else
		{
			sprintf(tempstring, "listenOnly=\"%d\" ", m_Talker.Int);  // default to listener (1)
			strcat(stringinput,tempstring);
		}

        if(m_TalkerListener.Exists)
		{
			sprintf(tempstring, "talkerListenerBits=\"%d\" ", m_TalkerListener.Int);
			strcat(stringinput,tempstring);
		}
		else
		{
			sprintf(tempstring, "talkerListenerBits=\"%d\" ", 0);	// default to listener
			strcat(stringinput,tempstring);
		}
	}

	if(Fnc/100)
	{
		strcat(stringinput,"channel=\"2\" ");
	}
	else
	{
		strcat(stringinput,"channel=\"1\" ");
	}

	if(m_DataSize.Exists)
	{
		stuff_string(&m_Data[0]);
		sprintf(tempstring, "data_bits=\"%s\" ", m_Data);
		strcat(stringinput,tempstring);
	}
	
	if(m_MaxTime.Exists)
	{
		sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
        strcat(stringinput,tempstring);
	}

	if(m_Attribute.Exists)
	{
		tempstring[0] = 0;	//CJW 20190325

		switch(m_Attribute.Dim)
		{
		case M_DATA:
			sprintf(tempstring, "attribute=\"data\" ");
			break;

		case M_STAT:
			sprintf(tempstring, "attribute=\"status\" ");
			break;

		case M_COMD:
			sprintf(tempstring, "attribute=\"command\" ");
			break;
		}
		strcat(stringinput,tempstring);
		m_Attribute.Exists=false;
	}

	//complete signal statement
	sprintf(tempstring,"/>\n</Signal>\n");
	strcat(stringinput,tempstring);
	strcpy(m_SignalDescription, stringinput);

    if(strcmp(type, "Fetch")==0)
	{
		if(Fnc%100<10)
		{
			IFNSIM((Status = cs_IssueAtmlSignal(type, BusName[0], m_SignalDescription, m_XmlValue, 1024)));  // 4096
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal(type, BusName[1], m_SignalDescription, m_XmlValue, 1024)));  // 4096
		}
	}
	else
	{
		if(Fnc%100<10)
		{
			IFNSIM((Status = cs_IssueAtmlSignal(type, BusName[0], m_SignalDescription, NULL, 0)));
		}
		else
		{
			IFNSIM((Status = cs_IssueAtmlSignal(type, BusName[1], m_SignalDescription, NULL, 0)));
		}
	}
    CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SignalDescription, Status));
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: stuff_string()
//
// Purpose: Finds any " and replaces with 0x02 
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          string to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          Null terminated string with escape characters
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void stuff_string(char * string)
{
	for(int i=0; i<(int)strlen(string); i++)
	{
		if(string[i]=='"')
		{
			string[i]=0x02;
		}
	}
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: unstuff_string()
//
// Purpose: Finds any 0x02 and replaces it with "
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          string to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// string           char *          Null terminated string without escape characters
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void unstuff_string(char * string)
{
	for(int i=0; i<(int)strlen(string); i++)
	{
		if(string[i]==0x02)
		{
			string[i]='"';
		}
	}
	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: array_to_bin_string()
//
// Purpose: Converts an integer array to a Null terminated string comforming to
//           the serial data type(H's and L's) in xml 1641
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int *           integer array to be converted
// size             int             size of integer array to be converted
// length			int				length of word in bits
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void array_to_bin_string(int array [], int size, int length, char output [])
{
	char string[768]="";
	char temp[100]="";

	for(int i=0; i<size; i++)
	{
		for(int j=length-1; j>=0; j--)
		{
			if((array[i]>>j)&0x0001)
			{
				strcat(temp, "H");
			}
			else
			{
				strcat(temp, "L");
			}
		}

		//itoa(array[i], temp, 10);
		if(i!=size-1)
		{
			strcat(temp,", ");
		}

		strcat(string,temp);
		temp[0]='\0';
	}

    strcpy(output, string);
	
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: bin_string_to_array()
//
// Purpose: Converts a Null terminated string comforming to
//           the serial data type(H's and L's) in xml 1641 to an integer array
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// input            char *          Null terminated string in 1641 format
// length			int				length of word in bits
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
void bin_string_to_array(char input [], int length, int array [], int * size)
{
	char string[768]="";
	char *temp;
	int i=0;
	strcpy(string, input);

	temp=strtok(string, ",");

	while(temp!=NULL)
	{
		array[i]=0;
	
		for(int j=0; j<length; j++)
		{
			if(temp[j]=='H')
			{
				array[i]=array[i] + (1 << (length-1-j)); 
			}
		}

		i++;
		temp=strtok(NULL, ",");
		
		if(temp)
		{
			temp++;
		}
	}

	*size=i;

	return;
}


