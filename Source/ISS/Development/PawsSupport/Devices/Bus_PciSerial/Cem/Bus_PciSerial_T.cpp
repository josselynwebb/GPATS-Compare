//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_PciSerial_T.cpp
//
// Date:	    13-FEB-06
//
// Purpose:	    Instrument Driver for Bus_PciSerial
//
// Instrument:	Bus_PciSerial  <Device Description> (<device Type>)
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
//   1  RS232
//   2  RS422, RS485, RS485T
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  13FEB06  Baseline								  D. Bubenik, EADS North America Defense
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Bus_PciSerial_T.h"

// Local Defines

// Function codes
#define FNC_RS232       1
#define FNC_RS422       2

#define CAL_TIME       (86400 * 365) /* one year */

//FILE *fptr;
char fileName[] = "C:\\aps\\data\\PciIssueDebug.txt";
// Static Variables
int activereset;
int com1active;
int com2active;
int com3active;
int com4active;
int com5active;
int readlength;

void MapToComXNames( int Fnc, char *devname )
{
	//if( strnicmp( devname,"pciserial", 9) != 0 )
	//	return;
	// uncomment this return to leave original device name return;
	switch(Fnc)
	{
			case 1:
			case 2:
				strcpy( devname, "COM_1");
				com1active = 1;
				break;
 			case 3:
			case 4:
				strcpy( devname, "COM_2");
				com2active = 1;
				break;
			case 5:
			case 6:
				strcpy( devname, "COM_3");
				com3active = 1;
				break;
			case 7:
			case 8:
				strcpy( devname, "COM_4");
				com4active = 1;
				break;
			case 9:
			case 10:
				strcpy( devname, "COM_5");
				com5active = 1;
				break;
			default:
				break;
	}
}
//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CBus_RS485_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CBus_RS485_T::CBus_RS485_T(char *DeviceName, int Bus, int PrimaryAdr, int SecondaryAdr, int Dbg, int Sim)
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

    InitPrivateBus_RS485();
	NullCalDataBus_RS485();
    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Bus_RS485 Class called with Device [%s], "
                         "Sim %d, Dbg %d", 
                          DeviceName, Sim, Dbg));

    // Initialize the Bus_RS485 - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);
	activereset = 0;
	com1active = 0;
	com2active = 0;
	com3active = 0;
	com4active = 0;
	com5active = 0;
	readlength = 0;
	
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CBus_RS485_T()
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
CBus_RS485_T::~CBus_RS485_T()
{
    // Reset the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("Bus_RS485 Class Distructor called for Device [%s], ",m_DeviceName));
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusBus_RS485(int Fnc)
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
int CBus_RS485_T::StatusBus_RS485(int Fnc)
{
    int Status = 0;

	MapToComXNames( Fnc, m_DeviceName);
    // Status action for the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("StatusBus_RS485 (%s) called FNC %d", m_DeviceName, Fnc));
    // Check for any pending error messages
    IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, "<Signal />",  NULL, 0)));   
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: SetupBus_RS485_T(int Fnc)
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
int CBus_RS485_T::SetupBus_RS485(int Fnc)
{
    int       Status = 0;

	MapToComXNames( Fnc, m_DeviceName);
    // Setup action for the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("SetupBus_RS485 (%s) called FNC [%d]", m_DeviceName, Fnc));
    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
	{
        return(0);
	}    
    
    if((Status = GetStmtInfoBus_RS485(Fnc)) != 0)
	{
        return(0);
	}
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateBus_RS485(int Fnc)
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
int CBus_RS485_T::InitiateBus_RS485(int Fnc)
{
    int   Status = 0;
	MapToComXNames( Fnc, m_DeviceName);
	// Initiate action for the Bus_1553
	CEMDEBUG(5,cs_FmtMsg("InitiateBus_RS485 (%s) called FNC %d", 
						m_DeviceName, Fnc));

	if(m_initiate) //initiate has  been previously executed, start transfer
	{
		// Internal Trigger
		IFNSIM((Status = cs_IssueAtmlSignal("Enable", m_DeviceName, m_SignalDescription,
												NULL, 0)));
		CEMDEBUG(5,cs_FmtMsg("InitiateBus_RS485 (%s) calling cs_IssueAtmlSignal(Enable, m_DeviceName[%s], m_SignalDescription[%s]", 
						m_DeviceName, m_SignalDescription));
		
		m_initiate=false;
	}
	else //this is the first initiate, setup all the parameters
	{
		SendSetupMods(Fnc, "Setup");
		m_initiate=true;
		CEMDEBUG(5,cs_FmtMsg("InitiateBus_RS485 calling SendSetupMods(Fnc[%d], Setup)", Fnc));
	}
	return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: FetchBus_RS485(int Fnc)
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
int CBus_RS485_T::FetchBus_RS485(int Fnc)
{
    DATUM *fdat;
    int Status = 0;
	char MeasValue[1024];
    double MaxTime = 5000;
    char MeasFunc[32];
	int RetData[1024];
	int retDataCnt;
	int retDataCnt_xml;
	char *cp;
    int size;
	//fptr = fopen(fileName, "a+");
	MapToComXNames( Fnc, m_DeviceName);
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000;
	}

    // Fetch action for the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("FetchBus_RS485 (%s) called FNC %d", m_DeviceName, Fnc));
    // Fetch data
	fdat = FthDat();
	readlength = fdat->dat_cnt;
	m_Attribute.Dim = FthMod();

	if(m_Attribute.Dim == M_DATA)
	{
		strcpy(MeasFunc,"data");
	}

	m_Exnm.Int = FthQual();
	m_Attribute.Exists = true;
	SendSetupMods(Fnc, "Fetch");

	retDataCnt = DatCnt(fdat);
	
	readlength = 0;
	MeasValue[0] = '\0';

	if(Status)
	{
		 //MeasValue = FLT_MAX
	}
	else
	{
		CEMDEBUG(5,cs_FmtMsg("FetchBus_RS485() calling cs_GetStringValue(m_XmlValue[%s], MeasFunc[%s], MeasValue[%s],1024)", m_XmlValue, MeasFunc, MeasValue));
		IFNSIM(cs_GetStringValue(m_XmlValue, MeasFunc, MeasValue,1024));
	}

    //clear returned data array
	for (int i = 0; i < MAX_DATA; i++)
	{
		RetData[i] = 0;
	}

	CEMDEBUG(5,cs_FmtMsg("FetchBus_RS485() calling bin_string_to_array(MeasValue[%s], m_WordLength.Int[%d], &RetData[0][%d], &retDataCnt_xml[%d])", MeasValue, m_WordLength.Int, &RetData[0], &retDataCnt_xml));
	
	cs_bin_string_to_array(MeasValue, m_WordLength.Int, &RetData[0], &retDataCnt_xml);
	//return data
	for (int i = 0; i < retDataCnt; i++)
    {
		cp = DIGDatVal(fdat,i);
        size = DatSiz(fdat);
        for(int j = 0; j < size; j++)
        {			
			DIGDatByte(cp,j) = char((RetData[i] >> (size*8- 8*(j + 1))) & 0x00FF);
		}
	}
    FthCnt(retDataCnt);
    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: OpenBus_RS485(int Fnc)
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
int CBus_RS485_T::OpenBus_RS485(int Fnc)
{
	MapToComXNames( Fnc, m_DeviceName);
    // Open action for the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("OpenBus_RS485 (%s) called FNC %d", m_DeviceName, Fnc));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseBus_RS485(int Fnc)
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
int CBus_RS485_T::CloseBus_RS485(int Fnc)
{
	MapToComXNames( Fnc, m_DeviceName);
    // Close action for the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("CloseBus_RS485 (%s) called FNC %d", m_DeviceName, Fnc));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetBus_RS485(int Fnc)
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
int CBus_RS485_T::ResetBus_RS485(int Fnc)
{
    int   Status = 0;
	
	MapToComXNames( Fnc, m_DeviceName);
    // Reset action for the Bus_RS485
    CEMDEBUG(5,cs_FmtMsg("ResetBus_RS485 (%s) called FNC %d", m_DeviceName, Fnc));
    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
	if((Fnc != 0 && m_Wait.Exists) || (Fnc != 0 && (bool)activereset == true) )
    {
        // Reset the Bus_RS485
         IFNSIM((Status = cs_IssueAtmlSignal("Reset", m_DeviceName, m_SignalDescription, NULL, 0)));
    }

    InitPrivateBus_RS485();
	
	//fclose(fptr);
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoBus_RS485(int Fnc)
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
int CBus_RS485_T::GetStmtInfoBus_RS485(int Fnc)
{
	DATUM *datum;
    char *cp;
    unsigned char byte;
    int size;
	CEMDEBUG(5,cs_FmtMsg("GetStmtInfoBus_RS485 succesfully called Fnc  = %d", Fnc));

    // BUS-SPEC
    if((datum = GetDatum(M_BUSS, K_SET)) != 0)
    {
        m_BusSpec.Exists = true;
        m_BusSpec.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Bus-Spec %d",m_BusSpec.Dim));
    }
	
	//WORD-LENGTH
	if((datum = GetDatum(M_WDLN, K_SET)) != 0)
    {
        m_WordLength.Exists = true;
		m_WordLength.Int = (int)DECDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Word-Length %d",m_WordLength.Int));
    }

	// Terminated
	if(RemoveMod(M_TERM))
    {
        m_Terminated.Exists = true;
        CEMDEBUG(10,"Found Terminated");
    }

	// DATA
	if((datum = RetrieveDatum(M_DATA, K_SET)) != 0)
    {
        m_DataSize.Exists = true;
        m_DataSize.Int = DatCnt (datum);   //get # of vars
        
        for (int i = 0; i < m_DataSize.Int; i++)
        {
            if(DatTyp(datum)==DIGV)
		    {
                m_Data[i] = 0;
                cp = DIGDatVal(datum,i);
                size = DatSiz(datum);
                for(int j = 0; j < size; j++)
                {
                    byte = DIGDatByte(cp,j);
                    m_Data[i] = (m_Data[i] | byte <<(size*8- 8*(j + 1)));
					CEMDEBUG(10, cs_FmtMsg("m_Data[%d] is m_data[0x%X]", i, m_Data[i]));
                }

                CEMDEBUG(10, cs_FmtMsg("data word %d is 0x%X", i, m_Data[i]));
		    }
            else
			{
                CEMDEBUG(10,cs_FmtMsg("Data: Data Type Unknown"));
			}
        }
		FreeDatum(datum);
    }

	// BUS-MODE
	if((datum = GetDatum(M_BUSM, K_SET)) != 0)
    {
        m_BusMode.Exists = true;
        m_BusMode.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10, cs_FmtMsg("Found Bus-Mode %d", m_BusMode.Dim));
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
	
	// BIT-RATE
	if((datum = GetDatum(M_BITR, K_SET)) != 0)
    {
        m_BitRate.Exists = true;
        m_BitRate.Real = DECDatVal(datum, 0);
        CEMDEBUG(10, cs_FmtMsg("Found Bit-Rate %lf", m_BitRate.Real));
    }

	// STOP-BITS
	if((datum = GetDatum(M_STPB, K_SET)) != 0)
    {
        m_StopBits.Exists = true;
        m_StopBits.Real = DECDatVal(datum, 0);
        CEMDEBUG(10, cs_FmtMsg("Found Stop-Bits %lf", m_StopBits.Real));
    }

	// DELAY
	if((datum = GetDatum(M_DELA, K_SET)) != 0)
    {
        m_Delay.Exists = true;
        m_Delay.Real = DECDatVal(datum, 0);
        CEMDEBUG(10, cs_FmtMsg("Found Delay %lf", m_Delay.Real));
    }

	// PARITY
	if((datum = GetDatum(M_PRTY, K_SET)) != 0)
    {
        m_Parity.Exists = true;
        m_Parity.Dim = DSCDatVal(datum, 0);
        CEMDEBUG(10, cs_FmtMsg("Found Parity %d", m_Parity.Dim));
    }

	// MAX-TIME
	if((datum = GetDatum(M_MAXT, K_SET)) != 0)
    {
        m_MaxTime.Exists = true;
        m_MaxTime.Real = DECDatVal(datum, 0);
        CEMDEBUG(10, cs_FmtMsg("Found Max-Time %lf", m_MaxTime.Real));
    }

	// EXNM
	if((datum = GetDatum(M_EXNM, K_SET)) != 0)
    {
        m_Exnm.Exists = true;
		m_Exnm.Int = INTDatVal(datum, 0);
		CEMDEBUG(10, cs_FmtMsg("Found Exnm %d", m_Exnm.Int));
    }

	//UUT-TALKER
	if(RemoveMod(M_UUTT))
	{
		m_UutTalker.Exists = true;
	    CEMDEBUG(10, cs_FmtMsg("Found Talker UUT"));
	}
	
	//UUT-LISTENER
	if(RemoveMod(M_UUTL))
	{
		m_UutListener.Exists = true;
	    CEMDEBUG(10, cs_FmtMsg("Found Listener UUT"));
	}
	
	//TEST-EQUIP-TALKER
	if(RemoveMod(M_UUTT))
	{
		m_TestEquipTalker.Exists = true;
	    CEMDEBUG(10, cs_FmtMsg("Found Talker Test-Equip"));
	}

	//TEST-EQUIP-LISTENER
	if(RemoveMod(M_UUTL))
	{
		m_TestEquipListener.Exists = true;
	    CEMDEBUG(10, cs_FmtMsg("Found Listener Test-Equip"));
	}
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_RS485()
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
void CBus_RS485_T::InitPrivateBus_RS485(void)
{
	m_BusSpec.Exists = false;
	m_DataSize.Exists = false;
    m_BusMode.Exists = false;
    m_Proceed.Exists = false;
    m_Wait.Exists = false;
	m_BitRate.Exists = false;
	m_StopBits.Exists = false;
	m_Delay.Exists = false;
	m_Parity.Exists = false;
	m_MaxTime.Exists = false;
	m_Exnm.Exists = false;
	m_UutTalker.Exists = false;
	m_UutListener.Exists = false;
	m_TestEquipTalker.Exists = false;
	m_TestEquipListener.Exists = false;
	m_Terminated.Exists = false;
	m_Attribute.Exists = false;
	m_initiate = false;
	m_WordLength.Exists = false;
	m_WordLength.Int = 8;
	m_SignalDescription[0] = 0;
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_RS485()
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
void CBus_RS485_T::NullCalDataBus_RS485(void)
{
    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;
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
void CBus_RS485_T::SendSetupMods(int Fnc, char type [])
{
	int  Status = 0;
	char stringinput[2048];
	char tempstring[2048];
	char string[2048];

	CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully called Fnc = %d", Fnc));

	sprintf(stringinput, "<Signal Name=\"RS_SIGNAL\" Out=\"exchange\">\n");

	switch(m_BusSpec.Dim)
	{
		case D_RS422:
			strcat(stringinput, "<RS_422 name=\"exchange\" ");
			CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully case RS422"));
			break;

		case D_RS485:
			strcat(stringinput, "<RS_485 name=\"exchange\" ");
			CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully case RS485"));
			break;

		default:
			strcat(stringinput, "<RS_232 name=\"exchange\" ");
			CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully case RS232"));
			break;
	}

	if(m_Parity.Exists)
	{
		switch(m_Parity.Dim)
		{
			case D_EVEN:
				sprintf(tempstring, "parity=\"EVEN\" ");
				strcat(stringinput, tempstring);
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully case Even parity"));
				break;

			case D_NONE:
				sprintf(tempstring, "parity=\"NONE\" ");
				strcat(stringinput, tempstring);
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully case NONE parity"));
				break;

			default:
				sprintf(tempstring, "parity=\"ODD\" ");
				strcat(stringinput, tempstring);
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully case ODD parity"));
				break;
		}
	}

	if(m_BitRate.Exists)
	{
		sprintf(tempstring, "baud_rate=\"%gHz\" ", m_BitRate.Real);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully baud_rate=\"%gHz\" ", m_BitRate.Real));
		strcat(stringinput, tempstring);
	}

	if(m_StopBits.Exists)
	{
		sprintf(tempstring, "stop_bits=\"%gbits\" ", m_StopBits.Real);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully stop_bits=\"%gbits\" ", m_StopBits.Real));
		strcat(stringinput, tempstring);
	}
	
	if(m_DataSize.Exists)
	{
		cs_array_to_bin_string(m_Data, m_DataSize.Int, m_WordLength.Int, &string[0]);
		sprintf(tempstring, "data_bits=\"%s\" ", string);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully data_bits=\"%s\" ", string));
		strcat(stringinput, tempstring);
	}
	
	if(m_TestEquipTalker.Exists)
	{
		sprintf(tempstring, "talker=\"Test-Equip\" ");
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully talker=\"Test-Equip\" "));
		strcat(stringinput, tempstring);
	}
	if(m_TestEquipListener.Exists)
	{
		sprintf(tempstring, "listener=\"Test-Equip\" ");
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully listener=\"Test-Equip\" "));
		strcat(stringinput, tempstring);
	}
	if(m_UutTalker.Exists)
	{
		sprintf(tempstring, "talker=\"UUT\" ");
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully talker=\"UUT\" "));
		strcat(stringinput, tempstring);
	}
	if(m_UutListener.Exists)
	{
		sprintf(tempstring, "listener=\"UUT\" ");
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully listener=\"UUT\" "));
		strcat(stringinput, tempstring);
	}

	if(m_BusMode.Exists)
	{
		sprintf(tempstring, "Mode=\"");
		switch(m_BusMode.Dim)
		{
			case D_ALLLS:
				strcat(tempstring, "All-Listener\" ");
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully Mode All-Listener"));
				break;

			case D_TLKLS:
				strcat(tempstring, "Talker-Listener\" ");
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully Mode Talker-Listener"));
				break;
		}
		strcat(stringinput, tempstring);
	}

	if(m_WordLength.Exists)
	{
		sprintf(tempstring, "wordLength=\"%dbits\" ", m_WordLength.Int);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully wordLength=\"%dbits\" ", m_WordLength.Int));
		strcat(stringinput, tempstring);
	}

	if(m_Terminated.Exists)
	{
		sprintf(tempstring, "terminate=\"ON\" ");
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully terminate=\"ON\" "));
		strcat(stringinput, tempstring);
	}
	else
	{
		sprintf(tempstring, "terminate=\"OFF\" ");
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully terminate=\"OFF\" "));
		strcat(stringinput, tempstring);
	}
	
	if(m_MaxTime.Exists)
	{
		sprintf(tempstring, "maxTime=\"%gs\" ", m_MaxTime.Real);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully maxTime=\"%gs\" ", m_MaxTime.Real));
		strcat(stringinput, tempstring);
	}

	if(m_Delay.Exists)
	{
		sprintf(tempstring, "delay=\"%gs\" ", m_Delay.Real);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully delay=\"%gs\" ", m_Delay.Real));
		strcat(stringinput, tempstring);
	}
	if(readlength != 0)
	{
		sprintf(tempstring, "readLength=\"%d\" ", readlength);
		CEMDEBUG(5,cs_FmtMsg("SendSetupMods readLength=\"%d\" ", readlength));
		strcat(stringinput,tempstring);
	}

	if(m_Attribute.Exists)
	{
		switch(m_Attribute.Dim)
		{
			case M_DATA:
				sprintf(tempstring, "attribute=\"data\" ");
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods attribute=\"data\" "));
				break;

			case M_STAT:
				sprintf(tempstring, "attribute=\"status\" ");
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods attribute=\"status\" "));
				break;

			case M_COMD:
				sprintf(tempstring, "attribute=\"command\" ");
				CEMDEBUG(5,cs_FmtMsg("SendSetupMods attribute=\"command\" "));
				break;
		}
		strcat(stringinput,tempstring);
		m_Attribute.Exists = false;
	}

	//complete signal statement
	sprintf(tempstring, "/>\n</Signal>\n");
	strcat(stringinput, tempstring);
	strcpy(m_SignalDescription, stringinput);

	if(strcmp(type, "Fetch") == 0)
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



