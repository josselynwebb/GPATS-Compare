//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_Ethernet_Gigabit_T.cpp
//
// Date:	    20-FEB-06
//
// Purpose:	    Instrument Driver for Bus_Ethernet_Gigabit
//
// Instrument:	Bus_Ethernet_Gigabit  <Device Description> (<device Type>)
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
// 1.0.0.0  20FEB06  Baseline								  D. Bubenik, EADS North America Defense
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Bus_Ethernet_Gigabit_T.h"

// Local Defines
#define TCP       1
#define UDP       2

// Function codes

#define CAL_TIME       (86400 * 365) /* one year */

// Static Variables

// Local Function Prototypes
void stuff_string(char * string);
void unstuff_string(char * string);


//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CBus_Ethernet_Gigabit_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CBus_Ethernet_Gigabit_T::CBus_Ethernet_Gigabit_T(char *DeviceName, 
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

    if(DeviceName)
        strcpy(m_DeviceName,DeviceName);

    InitPrivateBus_Ethernet_Gigabit();
	NullCalDataBus_Ethernet_Gigabit();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Bus_Ethernet_Gigabit Class called with Device [%s], "
                         "Sim %d, Dbg %d", 
                          DeviceName, Sim, Dbg));

    // Initialize the Bus_Ethernet_Gigabit - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CBus_Ethernet_Gigabit_T()
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
CBus_Ethernet_Gigabit_T::~CBus_Ethernet_Gigabit_T()
{
    // Reset the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("Bus_Ethernet_Gigabit Class Distructor called for Device [%s], ",
                          m_DeviceName));


    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::StatusBus_Ethernet_Gigabit(int Fnc)
{
    int Status = 0;

    // Status action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("StatusBus_Ethernet_Gigabit (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    // Check for any pending error messages
    IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, "<Signal />",
                                       NULL, 0)));
    CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %lf",
                                 "<Signal />"));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupBus_Ethernet_Gigabit_T(int Fnc)
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
int CBus_Ethernet_Gigabit_T::SetupBus_Ethernet_Gigabit(int Fnc)
{
    int       Status = 0;

    // Setup action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("SetupBus_Ethernet_Gigabit (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
        return(0);
    
    if((Status = GetStmtInfoBus_Ethernet_Gigabit(Fnc)) != 0)
        return(0);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::InitiateBus_Ethernet_Gigabit(int Fnc)
{
    int   Status = 0;
	// Initiate action for the Bus_1553
	CEMDEBUG(5,cs_FmtMsg("InitiateBus_1553 (%s) called FNC %d", 
						m_DeviceName, Fnc));

	if(m_initiate) //initiate has  been previously executed, start transfer
	{
		// Internal Trigger
		IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription,
												NULL, 0)));
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
// Function: FetchBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::FetchBus_Ethernet_Gigabit(int Fnc)
{
    DATUM    *fdat;
    int     Status = 0;
	char MeasValue[10000];
    double  MaxTime = 5000;
    char    MeasFunc[32];
	int retDataCnt;

    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
        MaxTime = m_MaxTime.Real * 1000;

    // Fetch action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("FetchBus_Ethernet_Gigabit (%s) called FNC %d", 
                          m_DeviceName, Fnc));

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
		IFNSIM(cs_GetStringValue(m_XmlValue, MeasFunc, MeasValue,10000));
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
// Function: OpenBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::OpenBus_Ethernet_Gigabit(int Fnc)
{
    // Open action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("OpenBus_Ethernet_Gigabit (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::CloseBus_Ethernet_Gigabit(int Fnc)
{
    // Close action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("CloseBus_Ethernet_Gigabit (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::ResetBus_Ethernet_Gigabit(int Fnc)
{
    int   Status = 0;

    // Reset action for the Bus_Ethernet_Gigabit
    CEMDEBUG(5,cs_FmtMsg("ResetBus_Ethernet_Gigabit (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
	if(Fnc != 0 && m_Wait.Exists)
    {
        IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SignalDescription, NULL, 0)));
    }

    InitPrivateBus_Ethernet_Gigabit();

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoBus_Ethernet_Gigabit(int Fnc)
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
int CBus_Ethernet_Gigabit_T::GetStmtInfoBus_Ethernet_Gigabit(int Fnc)
{
	DATUM    *datum;
    char *cp, *temp;
    int size;

    // BUS-SPEC
    if((datum = GetDatum(M_BUSS, K_SET)))
    {
        m_BusSpec.Exists = true;
        m_BusSpec.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Bus-Spec %d",m_BusSpec.Dim));
    }
	
	//int m_Data[32];
	//m_DataSize;
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
        size = DatCnt (datum);   //get # of vars
   
		//get remote info
		cp = GetTXTDatVal(datum, 0);
		temp=strtok(cp, ":");
		strcpy(m_RemoteIP.Address, temp);
		temp=strtok(NULL, ":");
		sscanf(temp,"%d", &m_RemotePort.Int);
		m_RemoteIP.Exists=true;
		m_RemotePort.Exists=true;
		CEMDEBUG(10,  cs_FmtMsg("Found Remote Address %s and Port %d", m_RemoteIP.Address, m_RemotePort.Int));
   
		//get localIP or DHCP
		cp = GetTXTDatVal(datum, 1);
		strcpy(m_LocalIP.Address, cp);
		m_LocalIP.Exists=true;
		CEMDEBUG(10,  cs_FmtMsg("Found Local IP: %s",m_LocalIP.Address));
		
		if(size>2)
		{
			//get local Subnet Mask
			cp = GetTXTDatVal(datum, 2);
			strcpy(m_LocalMask.Address, cp);
			m_LocalMask.Exists=true;
			CEMDEBUG(10,  cs_FmtMsg("Found Local Subnet Mask: %s",m_LocalMask.Address));

			//get Default Gateway
			cp = GetTXTDatVal(datum, 3);
			strcpy(m_LocalGateway.Address, cp);
			m_LocalGateway.Exists=true;
			CEMDEBUG(10,  cs_FmtMsg("Found Default Gateway: %s",m_LocalGateway.Address));
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

	// TCP
	if(RemoveMod(M_PTCP))
    {
        m_Protocol.Exists = true;
		m_Protocol.Dim=TCP;
        CEMDEBUG(10,"Found TCP");
    }

	// UDP
	if(RemoveMod(M_PUDP))
    {
        m_Protocol.Exists = true;
		m_Protocol.Dim=UDP;
        CEMDEBUG(10,"Found UDP");
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

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_Ethernet_Gigabit()
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
void CBus_Ethernet_Gigabit_T::InitPrivateBus_Ethernet_Gigabit(void)
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
    m_LocalIP.Exists=false;
    m_LocalMask.Exists=false;
    m_LocalGateway.Exists=false;
	m_RemoteIP.Exists=false;
	m_RemotePort.Exists=false;
	m_Protocol.Exists=false;
	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_Ethernet_Gigabit()
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
void CBus_Ethernet_Gigabit_T::NullCalDataBus_Ethernet_Gigabit(void)
{
    //////// Place Bus_Ethernet_Gigabit specific data here //////////////
/* Begin TEMPLATE_SAMPLE_CODE */
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
/* End TEMPLATE_SAMPLE_CODE */
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
void CBus_Ethernet_Gigabit_T::SendSetupMods(int Fnc, char type [])
{
    int       Status = 0;
	char stringinput[1024];
	char tempstring[768];
    
    CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully called"));

	sprintf(stringinput, "<Signal Name=\"ETHERNET_SIGNAL\" Out=\"exchange\">\n");

	if(!m_Protocol.Exists)
		m_Protocol.Dim=TCP;
	switch(m_Protocol.Dim)
	{
	case UDP:
		strcat(stringinput, "<UDP name=\"exchange\" ");
		break;
	default:
		strcat(stringinput, "<TCP name=\"exchange\" ");
		break;
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

	if(m_LocalIP.Exists)
	{
		sprintf(tempstring, "localIP=\"%s\" ", m_LocalIP.Address);
		strcat(stringinput,tempstring);
	}

	if(m_LocalMask.Exists)
	{
		sprintf(tempstring, "localSubnetMask=\"%s\" ", m_LocalMask.Address);
		strcat(stringinput,tempstring);
	}

	if(m_LocalGateway.Exists)
	{
		sprintf(tempstring, "localGateway=\"%s\" ", m_LocalGateway.Address);
		strcat(stringinput,tempstring);
	}

	if(m_RemoteIP.Exists)
	{
		sprintf(tempstring, "remoteIP=\"%s\" ", m_RemoteIP.Address);
		strcat(stringinput,tempstring);
	}

	if(m_RemotePort.Exists)
	{
		sprintf(tempstring, "remotePort=\"%d\" ", m_RemotePort.Int);
		strcat(stringinput,tempstring);
	}

	if(m_Attribute.Exists)
	{
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
		IFNSIM((Status = cs_IssueAtmlSignal(type, m_DeviceName, m_SignalDescription, m_XmlValue, 1024)));
	}
	else
	{
		IFNSIM((Status = cs_IssueAtmlSignal(type, m_DeviceName, m_SignalDescription, NULL, 0)));
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