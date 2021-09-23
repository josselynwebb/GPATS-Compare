//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Bus_1553_T.cpp
//
// Date:	    1-JAN-06
//
// Purpose:	    Instrument Driver for Bus_1553
//
// Instrument:	Bus_1553  <Device Description> (<device Type>)
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
//   1  bus protocol                
//   2  monitor exchange            
//   3  master exchange
//   4  slave exchange               
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  1JAN06   Baseline								  D. Bubenik, EADS North America Defense
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "cem.h"
#include "key.h"
#include "cemsupport.h"
#include "Bus_1553_T.h"

// Local Defines

// Function codes

#define FNC_MASTER     3
#define FNC_SLAVE      4
#define FNC_MONITOR    2

#define CAL_TIME       (86400 * 365) /* one year */

#define  ATLAS_RESET	  2 //EADS - added to mimic Legacy

// Static Variables
int            atlasProtoStmt = 0;  //EADS - added to mimic Legacy

// Local Function Prototypes
void array_to_bin_string(int array [], int size, char output []);
void bin_string_to_array(char input [], int array [], int * size);
void array_to_string(int array [], int size, char output []);
void string_to_array(char input [], int array [], int * size);

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CBus_1553_T(int Bus, int PrimaryAdr, int SecondaryAdr,
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
CBus_1553_T::CBus_1553_T(char *DeviceName, 
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

    InitPrivateBus_1553();
	NullCalDataBus_1553();

    // The BusConfi only supplies the Sim and Debug Flags
    CEMDEBUG(5,cs_FmtMsg("Bus Class called with Device [%s], "
                         "Sim %d, Dbg %d", 
                          DeviceName, Sim, Dbg));

    // Initialize the Bus - not required in ATML mode
    // Check Cal Status and Resource Availability
    Status = cs_GetUniqCalCfg(DeviceName, CAL_TIME, &m_CalData[0], CAL_DATA_COUNT,  m_Sim);

	strcpy(m_SignalDescription, "<Signal />");

    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CBus_1553_T()
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
CBus_1553_T::~CBus_1553_T()
{
    // Reset the Bus
    CEMDEBUG(5,cs_FmtMsg("Bus_1553 Class Distructor called for Device [%s], ",
                          m_DeviceName));


    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusBus_1553(int Fnc)
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
int CBus_1553_T::StatusBus_1553(int Fnc)
{
    int Status = 0;

    // Status action for the Bus
    CEMDEBUG(5,cs_FmtMsg("StatusBus_1553 (%s) called FNC %d", 
                          m_DeviceName, Fnc));
    // Check for any pending error messages
	IFNSIM((Status = cs_IssueAtmlSignal("Status", m_DeviceName, "<Signal />", NULL, 0)));
	
    //CEMDEBUG(9,cs_FmtMsg("IssueStatus [%s] %lf",
    //                             m_SignalDescription));

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: SetupBus_1553_T(int Fnc)
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
int CBus_1553_T::SetupBus_1553(int Fnc)
{
	int  Status = 0;
   
    // Setup action for the Bus_1553
    CEMDEBUG(5,cs_FmtMsg("SetupBus_1553 (%s) called FNC %d", 
                          m_DeviceName, Fnc));

    // Check Station status
    IFNSIM((Status = cs_CheckStationStatus()));
    if((Status) < 0)
        return(0);
    
    if (atlasProtoStmt == ATLAS_RESET) //EADS - Added to mimic Legacy
	{
		ResetBus_1553(Fnc);
	}

    if((Status = GetStmtInfoBus_1553(Fnc)) != 0)
        return(0);

	//++atlasProtoStmt; //EADS - Added to mimic Legacy

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitiateBus_1553(int Fnc)
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
int CBus_1553_T::InitiateBus_1553(int Fnc)
{
    int   Status = 0;
	// Initiate action for the Bus_1553
	CEMDEBUG(5,cs_FmtMsg("InitiateBus_1553 (%s) called FNC %d", 
						m_DeviceName, Fnc));

	if(m_initiate) //initiate has  been previously executed, start transfer
	{
		// Internal Trigger
		IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Enable", m_DeviceName, m_MaxTime.Real, m_SignalDescription, NULL, 0)));
		atlasProtoStmt = ATLAS_RESET;
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
// Function: FetchBus_1553(int Fnc)
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
int CBus_1553_T::FetchBus_1553(int Fnc)
{
    DATUM    *fdat;
    int     Status = 0;
	char  MeasValue[1024];
    double  MaxTime = 5000;
    char    MeasFunc[32];
	int RetData[32];
	int retDataCnt;
	int retDataCnt_xml;
	char *cp;
	int size;
    
    // Check MaxTime Modifier
    if(m_MaxTime.Exists)
	{
        MaxTime = m_MaxTime.Real * 1000;
	}

    // Fetch action for the Bus_1553
    CEMDEBUG(5,cs_FmtMsg("FetchBus_1553 (%s) called FNC %d", m_DeviceName, Fnc));

    // Fetch data
    fdat = FthDat();
	retDataCnt = DatCnt(fdat);
	m_attribute.Dim = FthMod();

	switch(m_attribute.Dim)
	{
		case M_DATA:
			strcpy(MeasFunc,"data");
			CEMDEBUG(5,cs_FmtMsg("FetchBus_1553 called FNC M_DATA"));
			//sprintf_s(MeasFunc,sizeof(MeasFunc),"dataWordCount\"=\"%d\" data", retDataCnt);	//CJW
			break;
		case M_STAT:
			strcpy(MeasFunc,"status");
			CEMDEBUG(5,cs_FmtMsg("FetchBus_1553 called FNC M_STAT"));
			break;
		case M_COMD:
			strcpy(MeasFunc,"command");
			CEMDEBUG(5,cs_FmtMsg("FetchBus_1553 called FNC M_COMD"));
			break;
	}

	m_Exnm.Int=FthQual();
	m_attribute.Exists=true;
	SendSetupMods(Fnc, "Fetch");

	//retDataCnt=DatCnt(fdat);
	memset(MeasValue, '\0', sizeof(MeasValue));

    if(Status)
    {
        //MeasValue = FLT_MAX;
    }

    else
    {
        IFNSIM(cs_GetStringValue(m_XmlValue, MeasFunc, MeasValue,1024));
    }

    CEMDEBUG(5,cs_FmtMsg("FetchBus_1553 MeasValue = %s", MeasValue));

	//clear returned data array
	for (int i = 0; i < 32; i++)
	{
		RetData[i] = 0;
	}

	//fill in MeasValue array
	bin_string_to_array(MeasValue, &RetData[0], &retDataCnt_xml);

	//return data
	for (int j = 0; j < retDataCnt; j++)
    {
		cp = DIGDatVal(fdat,j);
        size = DatSiz(fdat);

        for(int k=0; k<size;k++)
        {
			DIGDatByte(cp,k)=char((RetData[j] >> (size*8- 8*(k+1))) & 0x00FF);
		}
	}

    FthCnt(retDataCnt);

    return(0);
}



///////////////////////////////////////////////////////////////////////////////
// Function: OpenBus_1553(int Fnc)
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
int CBus_1553_T::OpenBus_1553(int Fnc)
{
    // Open action for the Bus_1553
    CEMDEBUG(5,cs_FmtMsg("OpenBus_1553 (%s) called FNC %d", m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: CloseBus_1553(int Fnc)
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
int CBus_1553_T::CloseBus_1553(int Fnc)
{
    // Close action for the Bus_1553
    CEMDEBUG(5,cs_FmtMsg("CloseBus_1553 (%s) called FNC %d", m_DeviceName, Fnc));

    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ResetBus_1553(int Fnc)
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
int CBus_1553_T::ResetBus_1553(int Fnc)
{
    int   Status = 0;

    // Reset action for the Bus_1553
    CEMDEBUG(5,cs_FmtMsg("ResetBus_1553 (%s) called FNC %d", m_DeviceName, Fnc));
    // Check for not Remove All - Remove All will use Station Sequence called only from the SwitchCEM.dll
    if(Fnc != 0)
    {
		m_SignalDescription[0] = '\0';
		if(!m_MaxTime.Exists)
		{
			m_MaxTime.Real=10;
		}

        // Reset the Bus_1553
		IFNSIM((Status = cs_IssueAtmlSignalMaxTime("Reset", m_DeviceName, m_MaxTime.Real, m_SignalDescription, NULL, 0)));
	}

	atlasProtoStmt = 0; //EADS - Added to mimic Legacy
    InitPrivateBus_1553();

    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Private Class Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoBus_1553(int Fnc)
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
int CBus_1553_T::GetStmtInfoBus_1553(int Fnc)
{
	DATUM *datum;
    char *cp;
    unsigned char byte;
    int size;

    // BUS-SPEC
    if((datum = GetDatum(M_BUSM, K_SET)) != NULL)
    {
        m_BusSpec.Exists = true;
        m_BusSpec.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Bus-Spec %d",m_BusSpec.Dim));
    }

	// DATA
	if((datum = RetrieveDatum(M_DATA, K_SET)) != NULL)
    {
        m_DataSize.Exists = true;
        m_DataSize.Int = DatCnt (datum);   //get # of vars
        
        for (int i = 0; i < m_DataSize.Int; i++)
        {
            if(DatTyp(datum)==DIGV)
		    {
                m_Data[i]=0;
                cp = DIGDatVal(datum,i);
                size=DatSiz(datum);
                for(int j=0;j<size;j++)
                {
                    byte = DIGDatByte(cp,j);
                    m_Data[i]=(m_Data[i] | byte <<(size*8- 8*(j+1)));
                }

                CEMDEBUG(10,  cs_FmtMsg("data word %d is 0x%X",i, m_Data[i]));
		    }
            else
                CEMDEBUG(10,cs_FmtMsg("Data: Data Type Unknown"));
        }
		FreeDatum(datum);
    }

	// COMMAND
	if((datum = GetDatum(M_COMD, K_SET)) != NULL)
    {
        m_Command[0].Exists = true;
        if(DatTyp(datum)==DIGV)
		{
            m_Command[0].Int=0;
            cp = DIGDatVal(datum,0);
            size=DatSiz(datum);
	        for(int j=0;j<size;j++)
            {
                byte = DIGDatByte(cp,j);    
                m_Command[0].Int=m_Command[0].Int| (byte << (size*8- 8*(j+1)));
            }

            CEMDEBUG(10,  cs_FmtMsg("Command[0] word is 0x%X", m_Command[0].Int));
        }
        else
            CEMDEBUG(10,cs_FmtMsg("Command[0]: Data Type Unknown"));

		if(DatCnt (datum)>1) //get second word
		{
			m_Command[1].Exists = true;
			if(DatTyp(datum)==DIGV)
			{
				m_Command[1].Int=0;
				cp = DIGDatVal(datum,1);
				size=DatSiz(datum);
				for(int j=0;j<size;j++)
				{
					byte = DIGDatByte(cp,j);    
					m_Command[1].Int=m_Command[1].Int| (byte << (size*8- 8*(j+1)));
				}

				CEMDEBUG(10,  cs_FmtMsg("Command[1] word is 0x%X", m_Command[1].Int));
			}
			else
				CEMDEBUG(10,cs_FmtMsg("Command[1]: Data Type Unknown"));

		}
    }

	// STATUS
	if((datum = GetDatum(M_STAT, K_SET)) != NULL)
    {
        m_Status.Exists = true;
        if(DatTyp(datum)==DIGV)
		{
            m_Status.Int=0;
            cp = DIGDatVal(datum,0);
            size=DatSiz(datum);
	        for(int j=0;j<size;j++)
            {
                byte = DIGDatByte(cp,j);
                m_Status.Int=m_Status.Int| (byte << (size*8- 8*(j+1)));
            }

            CEMDEBUG(10,  cs_FmtMsg("Status word is 0x%X", m_Status.Int));
        }
        else
            CEMDEBUG(10,cs_FmtMsg("Status: Data Type Unknown"));
    }

	// BUS-MODE
	if((datum = GetDatum(M_BUSM, K_SET)) != NULL)
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
	
	// TEST-EQUIP-ROLE
	if((datum = GetDatum(M_TROL, K_SET)) != NULL)
    {
        m_TestEquipRole.Exists = true;
        m_TestEquipRole.Dim =	DSCDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Test-Equip-Role %d",m_TestEquipRole.Dim));
    }

	// MESSAGE-GAP
	if((datum = GetDatum(M_MGAP, K_SET)) != NULL)
    {
        m_MessageGap.Exists = true;
        m_MessageGap.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Message-Gap %lf",m_MessageGap.Real));
    }

	// RESPONSE-TIME
	if((datum = GetDatum(M_RSPT, K_SET)) != NULL)
    {
        m_ResponseTime.Exists = true;
        m_ResponseTime.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Response-Time %lf",m_ResponseTime.Real));
    }

	// MAX-TIME
	if((datum = GetDatum(M_MAXT, K_SET)) != NULL)
    {
        m_MaxTime.Exists = true;
        m_MaxTime.Real = DECDatVal(datum, 0);
        CEMDEBUG(10,cs_FmtMsg("Found Max-Time %lf",m_MaxTime.Real));
    }

	// EXNM
	if((datum = GetDatum(M_EXNM, K_SET)) != NULL)
    {
        m_Exnm.Exists = true;
		m_Exnm.Int = INTDatVal(datum, 0);
		CEMDEBUG(10,cs_FmtMsg("Found Exnm %d",m_Exnm.Int));
    }

	//Get Addresses
	switch(Fnc)
	{
		case FNC_MASTER:
			//UUT-TALKER
			if((datum = GetDatum(M_UUTT, K_SET)) != NULL)
			{
				m_SendAddressSize.Exists = true;
				m_SendAddressSize.Int = DatCnt (datum);   //get # of vars

				for(int i=0; i<m_SendAddressSize.Int; i++)
				{
					m_SendAddress[i] = INTDatVal(datum, i);
					CEMDEBUG(10,cs_FmtMsg("Found Sending-Address[%d] - %d", i, m_SendAddress[i]));
				}
			}
			//UUT-LISTENER
			if((datum = GetDatum(M_UUTL, K_SET)) != NULL)
			{
				m_RecieveAddressSize.Exists = true;
				m_RecieveAddressSize.Int = DatCnt (datum);   //get # of vars

				for(int i=0; i<m_RecieveAddressSize.Int; i++)
				{
					m_RecieveAddress[i] = INTDatVal(datum, i);
					CEMDEBUG(10,cs_FmtMsg("Found Receiveing-Address[%d] - %d", i, m_RecieveAddress[i]));
				}
			}
			break;
		case FNC_SLAVE:
			//UUT-TALKER
			if((datum = GetDatum(M_UUTT, K_SET)) != NULL)
			{
				m_SendAddressSize.Exists = true;
				m_SendAddressSize.Int = DatCnt (datum);   //get # of vars

				for(int i=0; i<m_SendAddressSize.Int; i++)
				{
					m_SendAddress[i] = INTDatVal(datum, i);
					CEMDEBUG(10,cs_FmtMsg("Found Sending-Address[%d] - %d", i, m_SendAddress[i]));
				}
			}
		
			//UUT-LISTENER
			if((datum = GetDatum(M_UUTL, K_SET)) != NULL)
			{
				m_RecieveAddressSize.Exists = true;
				m_RecieveAddressSize.Int = DatCnt (datum);   //get # of vars

				for(int i=0; i<m_RecieveAddressSize.Int; i++)
				{
					m_RecieveAddress[i] = INTDatVal(datum, i);
					CEMDEBUG(10,cs_FmtMsg("Found Receiveing-Address[%d] - %d", i, m_RecieveAddress[i]));
				}
			}
			//TEST-EQUIP-TALKER
			if((datum = GetDatum(M_TEQT, K_SET)) != NULL)
			{
				m_SendAddressSize.Exists = true;
				m_SendAddressSize.Int = DatCnt (datum);   //get # of vars

				for(int i=0; i<m_SendAddressSize.Int; i++)
				{
					m_SendAddress[i] = INTDatVal(datum, i);
					CEMDEBUG(10,cs_FmtMsg("Found Sending-Address[%d] - %d", i, m_SendAddress[i]));
				}
			}
			//TEST-EQUIP-LISTENER
			if((datum = GetDatum(M_TEQL, K_SET)) != NULL)
			{
				m_RecieveAddressSize.Exists = true;
				m_RecieveAddressSize.Int = DatCnt (datum);   //get # of vars

				for(int i=0; i<m_RecieveAddressSize.Int; i++)
				{
					m_RecieveAddress[i] = INTDatVal(datum, i);
					CEMDEBUG(10,cs_FmtMsg("Found Receiveing-Address[%d] - %d", i, m_RecieveAddress[i]));
				}
			}
			break;
		case FNC_MONITOR:
			break;
	}
	

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_1553()
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
void CBus_1553_T::InitPrivateBus_1553(void)
{
	int i=0;
	m_BusSpec.Exists=false;
	m_FaultTest.Exists=false;
	m_Standard.Exists=false;
	m_DataSize.Exists=false;
	m_Command[0].Exists=false;
	m_Command[1].Exists=false;
	m_Status.Exists=false;
	m_BusMode.Exists=false;
	m_Proceed.Exists=false;
	m_Wait.Exists=false;
	m_TestEquipRole.Exists=false;
	m_MessageGap.Exists=false;
	m_ResponseTime.Exists=false;
	m_MaxTime.Exists=false;
	m_initiate=false;
	m_SendAddressSize.Exists=false;
	m_RecieveAddressSize.Exists=false;
	m_Exnm.Exists=false;
	m_attribute.Exists=false;
	for(i=0; i<32; i++)
		m_Fetched[i]=-1;

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateBus_1553()
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
void CBus_1553_T::NullCalDataBus_1553(void)
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
void CBus_1553_T::SendSetupMods(int Fnc, char type [])
{
    int       Status = 0, i=0;
	char stringinput[1024];
	char tempstring[768];
	char string[768];
    
    CEMDEBUG(5,cs_FmtMsg("SendSetupMods succesfully called"));

	sprintf(stringinput, "<Signal Name=\"1553_SIGNAL\" Out=\"exchange\">\n<IEEE_1553B name=\"exchange\" ");

	if(m_ResponseTime.Exists)
	{
		sprintf(tempstring, "respTime=\"%3.6lfs\" ", m_ResponseTime.Real);
        strcat(stringinput,tempstring);
	}

	if(m_MessageGap.Exists)
	{
	    sprintf(tempstring, "messageGap=\"%3.6lfs\" ", m_MessageGap.Real);
        strcat(stringinput,tempstring);
	}

	if(m_TestEquipRole.Exists)
	{
		sprintf(tempstring, "Role=\"");
        switch(m_TestEquipRole.Dim)
		{
			case D_MASTR:
				strcat(tempstring, "Master\" ");
				break;
			case D_SLAVE:
				strcat(tempstring, "Slave\" ");
				break;
			case D_MONTR:
				strcat(tempstring, "Monitor\" ");
				break;
			}
		strcat(stringinput,tempstring);
	}
    
	switch(Fnc)
    {
		case FNC_MASTER:
			if(m_BusMode.Exists)
			{
				sprintf(tempstring, "Mode=\"");
				switch(m_BusMode.Dim)
				{
				case D_CONRT:
					strcat(tempstring, "Con-RT\" ");
					break;
				case D_RTCON:
					strcat(tempstring, "RT-Con\" ");
					break;
				case D_CONMD:
					strcat(tempstring, "Con-Mode\" ");
					break;
				case D_ALLLS:
					strcat(tempstring, "All-Listener\" ");
					break;
				}
				strcat(stringinput,tempstring);
			}
			if(m_DataSize.Exists)
			{
				array_to_bin_string(m_Data, m_DataSize.Int, &string[0]);
				sprintf(tempstring, "data=\"%s\" ", string);
				strcat(stringinput,tempstring);
			}
			if(m_Command[0].Exists)
			{
				array_to_bin_string(&m_Command[0].Int, 1, &string[0]);
				CEMDEBUG(10,  cs_FmtMsg("Command[0] word is %s", string));
				sprintf(tempstring, "command=\"%s", string);
			
				if(m_Command[1].Exists)
				{
					array_to_bin_string(&m_Command[1].Int, 1, string);
					strcat(tempstring, ", ");
					strcat(tempstring, string);
				}
				strcat(tempstring, "\" ");
				strcat(stringinput,tempstring);
			}
			break;
		case FNC_SLAVE:
			if(m_BusMode.Exists)
			{
				sprintf(tempstring, "Mode=\"");
				switch(m_BusMode.Dim)
				{
				case D_CONRT:
					strcat(tempstring, "Con-RT\" ");
					break;
				case D_RTCON:
					strcat(tempstring, "RT-Con\" ");
					break;
				case D_CONMD:
					strcat(tempstring, "Con-Mode\" ");
					break;
				}
				strcat(stringinput,tempstring);
			}
			if(m_DataSize.Exists)
			{
				array_to_bin_string(m_Data, m_DataSize.Int, &string[0]);
				sprintf(tempstring, "data=\"%s\" ", string);
				strcat(stringinput,tempstring);
			}
			if(m_Status.Exists)
			{
				array_to_bin_string(&m_Status.Int, 1, &string[0]);
				sprintf(tempstring, "status=\"%s\" ", string);
				strcat(stringinput,tempstring);
			}
			break;
		case FNC_MONITOR:
			if(m_BusMode.Exists)
			{
				sprintf(tempstring, "Mode=\"");
				switch(m_BusMode.Dim)
				{
				case D_ALLLS:
					strcat(tempstring, "All-Listener\" ");
					break;
				case D_CONRT:
					strcat(tempstring, "Con-RT\" ");
					break;
				case D_RTCON:
					strcat(tempstring, "RT-Con\" ");
					break;
				case D_CONMD:
					strcat(tempstring, "Con-Mode\" ");
					break;
				}
				strcat(stringinput,tempstring);
			}
			break;
	}

	if(m_SendAddressSize.Exists)
	{
		array_to_string(m_SendAddress, m_SendAddressSize.Int, &string[0]);
		sprintf(tempstring, "sendAddress=\"%s\" ", string);
		strcat(stringinput,tempstring);
	}
    if(m_RecieveAddressSize.Exists)
	{
		array_to_string(m_RecieveAddress, m_RecieveAddressSize.Int, &string[0]);
		sprintf(tempstring, "recieveAddress=\"%s\" ", string);
		strcat(stringinput,tempstring);
	}

	if(m_Wait.Exists)
	{
	    sprintf(tempstring, "process=\"Wait\" ");
        strcat(stringinput,tempstring);
	}
	else
	{
	    sprintf(tempstring, "process=\"Proceed\" ");
        strcat(stringinput,tempstring);
	}
	
	if(!m_MaxTime.Exists)
	{
		m_MaxTime.Real=10;
	}

	sprintf(tempstring, "Exnm=\"%d\" ", m_Exnm.Int);
    strcat(stringinput,tempstring);

	if(m_attribute.Exists)
	{
		switch(m_attribute.Dim)
		{
			case M_DATA:
				sprintf(tempstring, "attribute=\"data\" ");
				//sprintf(tempstring, "dataWordCount=\"32\" attribute=\"data\" ");
				break;
			case M_STAT:
				sprintf(tempstring, "attribute=\"status\" ");
				break;
			case M_COMD:
				sprintf(tempstring, "attribute=\"command\" ");
				break;
		}
		strcat(stringinput,tempstring);
		m_attribute.Exists=false;
	}

	//complete signal statement
	sprintf(tempstring,"/>\n</Signal>\n");
	strcat(stringinput,tempstring);
	strcpy(m_SignalDescription, stringinput);

    if(strcmp(type, "Fetch")==0)
	{
		for(i=0;i<32;i++)
		{
			if(m_Fetched[i]==m_Exnm.Int)
				break;
			else if(m_Fetched[i]==-1)
			{
				m_Fetched[i]=m_Exnm.Int;
				IFNSIM((Status = cs_IssueAtmlSignalMaxTime(type, m_DeviceName, m_MaxTime.Real, m_SignalDescription, m_XmlValue, 2048)));
				break;
			}
		}
	}
	else
	{
		IFNSIM((Status = cs_IssueAtmlSignalMaxTime(type, m_DeviceName, m_MaxTime.Real, m_SignalDescription, NULL, 0)));
	}
    CEMDEBUG(9,cs_FmtMsg("IssueSignal [%s] %d", m_SignalDescription, Status));
}
//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////
// Function: array_to_bin_string()
//
// Purpose: Converts an integer array to a Null terminated string comforming to
//           the serial data type in xml 1641
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int *           integer array to be converted
// size             int             size of integer array to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void array_to_bin_string(int array [], int size, char output [])
{
	char string[768] = {0};
	char temp[20] = {0};

	for(int i = 0; i < size; i++)
	{
		for(int j = 15; j >= 0; j--)
		{
			if((array[i] >> j) & 0x0001)
			{
				strcat(temp, "H");
			}

			else
			{
				strcat(temp, "L");
			}
		}

		if(i != size - 1)
		{
			strcat(temp,", ");
		}

		strcat(string,temp);
		temp[0] = '\0';
	}

    strcpy(output, string);
	return;
}
///////////////////////////////////////////////////////////////////////////////
// Function: bin_string_to_array()
//
// Purpose: Converts a Null terminated string comforming to
//           the serial data type in xml 1641 to an integer array
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
void bin_string_to_array(char input [], int array [], int * size)
{
	char string[2048] = {0};
	char *temp;
	int i = 0;
	strcpy(string, input);

	temp=strtok(string, ",");
	while(temp!=NULL)
	{
		array[i] = 0;
		for(int j = 0; j < 16; j++)
		{
			if(temp[j]=='H')
			{
				array[i] = array[i] + (1 << (15 - j)); 
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
void string_to_array(char input [], int array [], int * size)
{
	char string[768] = {0};
	char *temp;
	int i=0;
	strcpy(string, input);

	temp=strtok(string, ",");
	while(temp!=NULL)
	{
		array[i] = 0;
		array[i] = atoi(temp);
		i++;
		temp = strtok(NULL, ",");
		if(temp)
		{
			temp++;
		}
	}
	*size=i;

	return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: array_to_string()
//
// Purpose: Converts an integer array to a Null terminated string containing
//           integer values separated by ", "
//
// Input Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// array            int *           integer array to be converted
// size             int             size of integer array to be converted
// 
// Output Parameters
// Parameter		Type			Purpose
// ===============  ==============  ===========================================
// output           char *          Null terminated string in 1641 format
//
// Return: void
//
///////////////////////////////////////////////////////////////////////////////
void array_to_string(int array [], int size, char output [])
{
	char string[768] = {0};
	char temp[20] = {0};

	for(int i = 0; i < size; i++)
	{
		itoa(array[i], temp, 10);
		if(i != size - 1)
		{
			strcat(temp,", ");
		}

		strcat(string,temp);
		temp[0]='\0';
	}

    strcpy(output, string);
	return;
}