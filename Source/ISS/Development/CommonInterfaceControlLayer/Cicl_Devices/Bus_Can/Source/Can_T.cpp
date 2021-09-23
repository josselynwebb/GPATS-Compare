//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Can_TDR010_T.cpp
//
// Date:	    26JUN06
//
// Purpose:	    ATXMLW Instrument Driver for Can_TDR010
//
// Instrument:	Can_TDR010  <Device Description> (<device Type>)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
// Revision History
// Rev	     Date                  Reason					  AUTHOR
// =======  =======  =======================================  =================
// 1.0.0.0  26JUN06  Baseline								  D. Bubenik, EADS North America Defense
// 1.4.1.0  04JUN07  Updated CBTS per DME changes             DME
// 1.5.0.0  04JUN07  Updated CBTS for MNG Release             J. French
// 1.6.0.0  04APR19  Updated for CAN bus bit timeing problem  J. Witcher
// 1.7.0.0  30MAY19  Updated for CAN bus Start problem        J. Witcher
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "AtxmlWrapper.h"
#include "tdrv010api.h"
#include "tdrv010.h"
#include "Can_T.h"

// Local Defines
#define TCP		1
#define UDP		2

#define DATA    1
#define ALL		2
#define MAX_CANS 8
// Function codes

#define _TL_TEQT		1	// test-equip-talk
#define _TL_TEQL		2	// test-equip-listen
#define _TL_UUTT		4	// uut talker
#define _TL_UUTL		8	// uuut listener


#define CAL_TIME       (86400 * 365) /* one year */
#define MAX_MSG_SIZE    1024


#define CHANNEL1 1
#define CHANNEL2 2

// Static Variables
//HANDLE	CanBusEvent;
//HANDLE	ThreadReadDone;

// Local Function Prototypes
extern void stuff_string(char * string); 
extern void unstuff_string(char * string); 
void bin_string_to_array(char input [], int length, int array []);
void array_to_bin_string(int array [], int size, int length, char output []);


static int startstatus[16];

int Starttdrv010Start( TDRV010_HANDLE FileDescriptor,  UCHAR canChan )
{
	int Status = 0;
	//TDRV010_STATUS statusBuf;

	if( startstatus[canChan] > 0 )
	{
		return (0);
	}

	startstatus[canChan] = 1;
	//tdrv010GetControllerStatus( FileDescriptor, canChan, &statusBuf );
	Status = tdrv010Start( FileDescriptor,  canChan );
	//tdrv010GetControllerStatus( FileDescriptor, canChan, &statusBuf );
	
	return (Status);
}

int Stoptdrv010Stop( TDRV010_HANDLE FileDescriptor,  UCHAR canChan )
{
	int Status = 0;
	//TDRV010_STATUS statusBuf;

	if( startstatus[canChan] == 0 )
	{
		return (0);
	}

	startstatus[canChan] = 0;
	//tdrv010GetControllerStatus( FileDescriptor, canChan, &statusBuf );
	Status = tdrv010Stop( FileDescriptor,  canChan );
	//tdrv010GetControllerStatus( FileDescriptor, canChan, &statusBuf );

	return (Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: CCan_TDR010_T
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

CCan_TDR010_T::CCan_TDR010_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{
    char LclMsg[1024];
    int Status = 0;
	char Path[100];

    // Save Device Info
    m_InstNo = Instno;
    m_ResourceType = ResourceType;
    m_ResourceName[0] = '\0';
	Path[0] ='\0';
	startstatus[1] = -1;	// force start/stop
	startstatus[2] = -1;	// force start/stop

	if(ResourceName)
    {
        strnzcpy(m_ResourceName,ResourceName,ATXMLW_MAX_NAME);
    }

    m_Sim = Sim;
    m_Dbg = Dbglvl;

	/*if (CanBusEvent == NULL) {
		if ((CanBusEvent = CreateEvent(0, true, false, "CanBusReadHalt")) == NULL) 
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ", Status, "Could not CreateEvent");
		
			return;
		}
	}

	if (ThreadReadDone == NULL) {
		if ((ThreadReadDone = CreateEvent(0, true, false, "ThreadReadingDone")) == NULL) 
		{
			atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ", Status, "Could not CreateEvent");
			
			return;
		}
	}*/

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

    m_InitString[0] = '\0';

    InitPrivateCan_TDR010();
	NullCalDataCan_TDR010();

    // The Form Init String
    sprintf(m_InitString,"%s", m_AddressInfo.ControllerType);
    sprintf(LclMsg,"Wrap-CCan_TEWS Tdr010 Class called with Instno %d, Sim %d, Dbg %d", m_InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,Buffersize);
	CanHandle = INVALID_HANDLE_VALUE;

	for (int i = 1; i < MAX_CANS; i++)
	{
		 //  Win32 device names ending in a one-digit number
		 //  ( TDRV010_1, TDRV010_2, ... )
		 sprintf_s(Path, 100, "\\\\.\\TDRV010_%d", i);
		 CanHandle = tdrv010Open(Path);
	
		 if (CanHandle != INVALID_HANDLE_VALUE)
		 {
			//printf(" %s Device found --> TDR010\n", Path);
			i = MAX_CANS;
		 }
	}

	DWORD Errorword = GetLastError();
	
	if (!m_Sim && (CanHandle == INVALID_HANDLE_VALUE))
	{
		printf (" %s Device Not Found, %d \n",Path,Errorword);
	    atxmlw_ErrorResponse(m_ResourceName, Response, Buffersize, "Instrument Error ",-1, "tdrv010Open failed.");
    } 

    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: ~CCan_TDR010_T()
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
CCan_TDR010_T::~CCan_TDR010_T()
{
    char Dummy[1024];

    // Reset the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-~CCan_TDR010 Class Distructor called "),Dummy,1024);
	//FreeLibrary(CanHandle);
		
	tdrv010Close(CanHandle);

	/*if (CanBusEvent != NULL) 
	{
		CloseHandle(CanBusEvent);
		CanBusEvent = NULL;
	}

	if (ThreadReadDone != NULL) 
	{
		CloseHandle(ThreadReadDone);
		ThreadReadDone = NULL;
	}*/

    return;
}

///////////////////////////////////////////////////////////////////////////////
// ANTHONY ADDED THIS
int CCan_TDR010_T::Open(char *VXIAddress)
{
 
    sprintf(m_InitString, "TDRV010_%d", m_device);

    IFNSIM(m_Handle = (COMM_HANDLE)tdrv010Open(m_InitString));

    if(m_Handle == (COMM_HANDLE)INVALID_HANDLE_VALUE)
    { 
		//atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ",m_HAndle, "CddiOpen() failed.");
    }

	return(m_Handle);
}

///////////////////////////////////////////////////////////////////////////////
// Function: StatusCan_TDR010
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
int CCan_TDR010_T::StatusCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int            Status = 0;
    char           sMsg[1024] = "";
	TDRV010_STATUS statusBuf;

    // Status action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-StatusCan_TDR010 called "), Response, BufferSize);
    // Check for any pending error messages
	IFNSIM(Status = tdrv010GetControllerStatus(CanHandle, mchannel, &statusBuf));

	if (Status == 0)
	{
		 printf("\nRead Status successful completed!\n");

		 sprintf(sMsg,"\nArbitration lost capture register   = 0x%02X\n", statusBuf.ArbitrationLostCapture);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"Error code capture register         = 0x%02X\n", statusBuf.ErrorCodeCapture);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"TX error counter register           = 0x%02X\n", statusBuf.TxErrorCounter);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"RX error counter register           = 0x%02X\n", statusBuf.RxErrorCounter);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"Error warning limit register        = 0x%02X\n", statusBuf.ErrorWarningLimit);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"Status register                     = 0x%02X\n", statusBuf.StatusRegister);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"Mode register                       = 0x%02X\n", statusBuf.ModeRegister);
		 strncpy(Response, sMsg, strlen(sMsg));
		 sprintf(sMsg,"Peak value RX message counter       = %d\n", statusBuf.RxMessageCounterMax);
		 strncpy(Response, sMsg, strlen(sMsg));
		 strncpy(Response, "\0", 1);
		 BufferSize = strlen(Response);
	}
	else
	{
		 ErrorCan_TDR010(Status, Response, BufferSize);
	}

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: IssueSignalCan_TDR010
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
int CCan_TDR010_T::IssueSignalCan_TDR010(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    char *ErrMsg = "";
    int  Status = 0;

    // IEEE 1641 Issue Signal action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueSignalCan_TDR010 Signal: "), Response, BufferSize);

    if((Status = GetStmtInfoCan_TDR010(SignalDescription, Response, BufferSize)) != 0)
	{
        return(Status);
	}

    switch(m_Action)
    {
        case ATXMLW_SA_APPLY:
			if((Status = SetupCan_TDR010(Response, BufferSize)) != 0)
			{
				return(Status);
			}

            if((Status = EnableCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_REMOVE:
            if((Status = DisableCan_TDR010(Response, BufferSize)) != 0)
		    {
                return(Status);
		    }

            if((Status = ResetCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_MEASURE:
            if((Status = SetupCan_TDR010(Response, BufferSize)) != 0)
            {
			    return(Status);
	        }
            break;

        case ATXMLW_SA_READ:
            if((Status = EnableCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}

            if((Status = FetchCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_RESET:
            if((Status = ResetCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_SETUP:
            if((Status = SetupCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_CONNECT:
            break;

        case ATXMLW_SA_ENABLE:
            if((Status = EnableCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_DISABLE:
            if((Status = DisableCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_FETCH:
            if((Status = FetchCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

        case ATXMLW_SA_DISCONNECT:
            break;

	    case ATXMLW_SA_STATUS:
            if((Status = StatusCan_TDR010(Response, BufferSize)) != 0)
            {
				return(Status);
			}
            break;

	    case ATXMLW_SA_IDENTIFY:
	        break;
    }
    
	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: RegCalCan_TDR010
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
int CCan_TDR010_T::RegCalCan_TDR010(ATXMLW_INTF_CALDATA* CalData)
{
    char      Dummy[1024];

    // Setup action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-RegCalCan_TDR010 CalData: %s", CalData),Dummy,1024);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ResetCan_TDR010
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
int CCan_TDR010_T::ResetCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-ResetCan_TDR010 called "), Response, BufferSize);

	// Reset the Can_TDR010

    IFNSIM(Status = Stoptdrv010Stop(CanHandle, mchannel));	//CJW added for 810 board
		
    //IFNSIM(Status = tdrv010FlushReceiveFifo(CanHandle, mchannel));

    if(Status)
	{
        ErrorCan_TDR010(Status, Response, BufferSize);
	}

    InitPrivateCan_TDR010();

    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IstCan_TDR010
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
int CCan_TDR010_T::IstCan_TDR010(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   Status = 0;

    // Reset action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IstCan_TDR010 called Level %d", Level), Response, BufferSize);

    switch(Level)
    {
        case ATXMLW_IST_LVL_PST:
            Status = StatusCan_TDR010(Response,BufferSize);
            break;

        case ATXMLW_IST_LVL_IST:
 		    IFNSIM(Status = tdrv010SelftestEnable(CanHandle, mchannel));
		    IFNSIM(Status = Starttdrv010Start(CanHandle, mchannel));
            break;

        case ATXMLW_IST_LVL_CNF:
            Status = StatusCan_TDR010(Response,BufferSize);
            break;

        default: // Hopefully BIT 1-9
		    IFNSIM(Status = tdrv010SelftestDisable(CanHandle, mchannel));
            break;
    }

	IFNSIM(Status = Stoptdrv010Stop(CanHandle, mchannel));
	IFNSIM(Status = tdrv010SelftestDisable(CanHandle, mchannel));
    
	if(Status)
	{
        ErrorCan_TDR010(Status, Response, BufferSize);
		Status = ResetCan_TDR010(Response, BufferSize);
	}

    InitPrivateCan_TDR010();
    
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueNativeCmdsCan_TDR010
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
int CCan_TDR010_T::IssueNativeCmdsCan_TDR010(ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int       Status = 0;

    // Setup action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-IssueNativeCmdsCan_TDR010 "), Response, BufferSize);

    //if((Status = atxmlw_InstrumentCommands(10, InstrumentCmds, Response, BufferSize, m_Dbg, m_Sim)))
    //{
    //    return(Status);
    //}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: IssueDriverFunctionCallCan_TDR010
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
int CCan_TDR010_T::IssueDriverFunctionCallCan_TDR010(ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    // Setup action for the Can_TDR010
    ATXMLW_DEBUG(5,"Wrap-IssueDriverFunctionCallCan_TDR010", Response, BufferSize);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: SetupCan_TDR010
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
int CCan_TDR010_T::SetupCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int     Status          = 0;
	USHORT  timingValue     = TDRV010_20KBIT;	// CJW TDRV010_125KBIT;
	BOOLEAN useThreeSamples = FALSE;
	BOOLEAN useSingleFilter = FALSE;
	int     acceptanceCode  = 0;
	int     acceptanceMask  = 0xFFFFFFFF;

    // Setup action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-SetupCan_TDR010 called "), Response, BufferSize);
	
	mchannel = (UCHAR)CHANNEL1;
	
	if(m_Channel1.Exists)
	{
		mchannel=(UCHAR)m_Channel1.Int;
	}
	
	if(m_Channel2.Exists)
	{
		mchannel=(UCHAR)m_Channel2.Int;
	}

	IFNSIM(Status = Stoptdrv010Stop(CanHandle, mchannel));

	// TDRV010_1_6MBIT               0x1100  /* 1.6 Mbit/s   max. distance = 20 m    */
	// TDRV010_1MBIT                 0x1400  /*   1 Mbit/s   max. distance = 40 m    */
	// TDRV010_500KBIT               0x1c00  /* 500 Kbit/s   max. distance = 130 m   */
	// TDRV010_250KBIT               0x1c01  /* 250 Kbit/s   max. distance = 270 m   */
	// TDRV010_125KBIT               0x1c03  /* 125 Kbit/s   max. distance = 530 m   */
	// TDRV010_100KBIT               0x2f43  /* 100 Kbit/s   max. distance = 620 m   */
	// TDRV010_50KBIT                0x2f47  /*  50 Kbit/s   max. distance = 1.3 km  */
	// TDRV010_20KBIT                0x2f53  /*  20 Kbit/s   max. distance = 3.3 km  */

	if(m_TimingValue.Exists) //CJW changed this 20190404
	{
		if( m_TimingValue.Int >= 1600000 )		// 1.6 M
		{
			timingValue = TDRV010_1_6MBIT;
		}
		else if( m_TimingValue.Int >= 1000000 )	// 1.0 M
		{
			timingValue = TDRV010_1MBIT;
		}
		else if( m_TimingValue.Int >= 500000 )	// 500 K
		{
			timingValue = TDRV010_500KBIT;
		}
		else if( m_TimingValue.Int >= 250000 )	//250 K
		{
			timingValue = TDRV010_250KBIT;
		}
		else if( m_TimingValue.Int >= 125000 )	// 250 K
		{
			timingValue = TDRV010_125KBIT;
		}
		else if( m_TimingValue.Int >= 100000 )	// 100 K
		{
			timingValue = TDRV010_100KBIT;
		}
		else if( m_TimingValue.Int >= 50000 )	// 50 K
		{
			timingValue = TDRV010_50KBIT;
		}
		else if( m_TimingValue.Int >= 20000 )	// 20 K
		{
			timingValue = TDRV010_20KBIT;
		}
		else
		{
			timingValue = TDRV010_20KBIT;		// 20 K default
		}
	}
	
	if(m_ThreeSamples.Exists)
	{
		useThreeSamples = m_ThreeSamples.Int;
	}

	IFNSIM(Status = tdrv010SetBitTiming(CanHandle, mchannel, timingValue, useThreeSamples));

	if(m_SingleFilter.Exists)
	{
		useSingleFilter = TRUE;
	}

	if(m_AcceptanceCode.Exists)
	{
		acceptanceCode = m_AcceptanceCode.Int;
	}

	if(m_AcceptanceMask.Exists)
	{
		acceptanceMask = m_AcceptanceMask.Int;
	}
	
	IFNSIM(Status = tdrv010SetFilter(CanHandle, mchannel, useSingleFilter, acceptanceCode, acceptanceMask));
	
	//	IFNSIM(Status = Starttdrv010Start(CanHandle, mchannel));
    if(Status)
	{
		ErrorCan_TDR010(Status, Response, BufferSize);
	}

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: FetchCan_TDR010
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
int CCan_TDR010_T::FetchCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int           Status       = 0;
	char          attribute[10];
	char          tempString[40];
	//DWORD         WaitReturn   = 0;
	double        MeasValue    = 0.0;
    double        MaxTime      = 5000;
    char          ErrMsg[1024] = "";
	int           GetData      = 1;
	unsigned long identifier   = 0;
	unsigned char IOflags      = 0;
	unsigned char msgStatus    = 0;
	int           datalen      = 0;
	unsigned long timeout      = 0;

	// m_RetData = new unsigned char[MAX_DATA];
	//memset(Response, '\0', BufferSize);
	memset( m_RetData, 0, sizeof(m_RetData));
	
	if(m_Sim)
	{
		strcpy((char*)m_RetData, "THIS IS MY TEST MESSAGE");
	}
	
	/*SetEvent(CanBusEvent);

	if ((WaitReturn = WaitForSingleObject(ThreadReadDone, 60000)) == WAIT_OBJECT_0) 
	{
        // MessageBox(0, "WaitForSingleObject(ThreadReadDone", "FETCH", MB_OK);
		ResetEvent(ThreadReadDone);
	}*/

	if(m_Channel1.Exists)
	{
		mchannel = m_Channel1.Int;
	}
	else if(m_Channel2.Exists)
	{
		mchannel = m_Channel2.Int;
	}

    // Fetch action for the Can_TDR010
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("Wrap-FetchCan_TDR010 called "), Response, BufferSize);

	if (m_Attribute.Dim == DATA)
	{
        // TDRV010_STATUS statusBuf;
        // tdrv010GetControllerStatus( CanHandle, mchannel, &statusBuf );
		
		IFNSIM(Status = Starttdrv010Start(CanHandle, mchannel));	//CJW 20190514
		
		if (!m_MaxTime.Exists)
		{
		    m_MaxTime.Real = 20; //in seconds
			IFNSIM(Status = tdrv010ReadNoWait(CanHandle, mchannel, &identifier, &IOflags, &msgStatus, &datalen, &m_RetData[0]));
			
			if (Status != 0)
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, atxmlw_FmtMsg("CanReadNoWait returned error code %d", Status));
				return 1;
			}
		}
		else
		{
			timeout = (unsigned long)m_MaxTime.Real;
			IFNSIM(Status = tdrv010Read(CanHandle, mchannel,timeout, &identifier, &IOflags, &msgStatus, &datalen, &m_RetData[0]));
		
			if (Status != 0)
			{
				atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, atxmlw_FmtMsg("CanReadWait returned error code %d", Status));
				return 1;
			}
		}
	}	
	else if (m_Attribute.Dim == ALL)
	{
		// return status
	}

	// Fetch data
	switch(m_Attribute.Dim)
	{
	    case DATA:
		    stuff_string((char*)m_RetData);
		    strcpy(attribute, "data");
		    break;
	}
    
	if(Status)
    {
        //MeasValue = FLT_MAX;
    }
    else
    {
        sscanf(ErrMsg,"%E",&MeasValue);
    }
	
	if(Response && (BufferSize > (int)(strlen(Response)+200)))
	{
		if(m_Attribute.Dim==DATA)
		{
			char  retdata[1024];
			int tempi[8];
			
			for( int i = 0; i < 8 ; i++ )
			{
				tempi[i] = (int)((UCHAR)m_RetData[i]);
			}

			memset( retdata, 0, sizeof(retdata));		//CJW
			array_to_bin_string((int*)tempi, 8, 8, retdata);

			atxmlw_ScalerStringReturn(attribute, "", (char*)retdata,Response, BufferSize);
			atxmlw_ScalerStringReturn("data_bits", "", (char*)retdata,Response, BufferSize);
		}
		else
		{
			if(m_TimingValue.Exists)
			{
				atxmlw_ScalerIntegerReturn("timingValue", "", m_TimingValue.Int,Response, BufferSize);
			}

			if(m_ThreeSamples.Exists)
			{
				atxmlw_ScalerIntegerReturn("threeSamples", "", m_ThreeSamples.Int,Response, BufferSize);
			}

			if(m_Channel1.Exists)
			{
				atxmlw_ScalerIntegerReturn("m_Channel", "", m_Channel1.Int,Response, BufferSize);
			}
			else if(m_Channel2.Exists)
			{
				atxmlw_ScalerIntegerReturn("m_Channel", "", m_Channel2.Int,Response, BufferSize);
			}

			if(m_SingleFilter.Exists)
			{
				atxmlw_ScalerIntegerReturn("singleFilter", "", m_SingleFilter.Int,Response, BufferSize);
			}

			if(m_AcceptanceCode.Exists)
			{
				tempString[0]='\0';
				array_to_bin_string(&m_AcceptanceCode.Int, 1, 32, tempString);
				atxmlw_ScalerStringReturn("acceptanceCode", "", tempString,	Response, BufferSize);
			}

			if(m_AcceptanceMask.Exists)
			{
				tempString[0]='\0';
				array_to_bin_string(&m_AcceptanceMask.Int, 1, 32, tempString);
				atxmlw_ScalerStringReturn("acceptanceMask", "", tempString,	Response, BufferSize);
			}

			if(m_MaxTime.Exists)
			{
				atxmlw_ScalerDoubleReturn("maxTime", "", m_MaxTime.Real, Response, BufferSize);
			}

			atxmlw_ScalerStringReturn("spec", "", "CAN",Response, BufferSize);
		}
	}

	return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: DisableCan_TDR010
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
int CCan_TDR010_T::DisableCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-DisableCan_TDR010 called "), Response, BufferSize);
    IFNSIM(Status = Stoptdrv010Stop(CanHandle, mchannel));
	
    if (Status !=0)
	{ 
        atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, atxmlw_FmtMsg("CanDisable returned error code %d", Status));  
    }

	IFNSIM(Status = tdrv010FlushReceiveFifo(CanHandle, mchannel));
 
	return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: EnableCan_TDR010
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
int CCan_TDR010_T::EnableCan_TDR010(ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int Status=0;
	unsigned char IOflags = TDRV010_STANDARD;    /* set default flags */
	unsigned long timeout = 0;
	unsigned long identifier = 1;

	ATXMLW_DEBUG(5, atxmlw_FmtMsg("Wrap-EnableCan_TDR010 called "), Response, BufferSize);

	if(m_Channel1.Exists)
	{
		mchannel = m_Channel1.Int;
	}
	
	if(m_Channel2.Exists)
	{
		mchannel = m_Channel2.Int;
	}
	
	IFNSIM(Status = Starttdrv010Start(CanHandle, mchannel));

	if( m_ListenOnly.Exists == false )
	{
		if(m_DataSize.Exists)//send Data
		{
			identifier = *((ULONG *)(&m_DataSize.Address[0]));
			IOflags |= (UCHAR)m_DataSize.Address[4];
			
			if (!m_MaxTime.Exists)
			{
				m_MaxTime.Real = 20; //in seconds
				IFNSIM(Status = tdrv010WriteNoWait(CanHandle, mchannel, identifier, IOflags, m_DataSize.Int, (UCHAR*)&m_DataSize.Address[5]));	//m_Data));
				
				if (Status != 0)
				{
					atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, atxmlw_FmtMsg("EnableWriteNoWait returned error code %d", Status));
					
					return (-1);
				}
			}
			else
			{
				timeout = (unsigned long)m_MaxTime.Real;	//in seconds
				IFNSIM(Status = tdrv010Write(CanHandle, mchannel, timeout, identifier, IOflags, m_DataSize.Int, (UCHAR*)&m_DataSize.Address[5])); //m_Data));
				if (Status != 0)
				{
					atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, atxmlw_FmtMsg("EnableWriteWait returned error code %d", Status));
				
					return (-1);
				}
			}

			if (Status != 0)
			{
			    atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", 0, atxmlw_FmtMsg("CanEnableNoWait returned error code %d", Status));
			 
				return (-1);
			}
		}
	}

    return(Status);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ErrorCan_TDR010
//
// Purpose: Query Can_TDR010 for the error text and send to WRTS
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
int  CCan_TDR010_T::ErrorCan_TDR010(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int  Err = 0;
 	char ErrorCodeBuff[512] = "Driver call error";
    
	if(Status)
    {
		sprintf(ErrorCodeBuff, " Error Code = %d .", Status);
		atxmlw_ErrorResponse(m_ResourceName, Response, BufferSize, "Instrument Error ", Status, ErrorCodeBuff);
    }
    
	return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: GetStmtInfoCan_TDR010
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
int CCan_TDR010_T::GetStmtInfoCan_TDR010(ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int    Status = 0;
    char   LclResource[ATXMLW_MAX_NAME];
    double LclDblValue;
    char   LclUnit[ATXMLW_MAX_NAME]="";
    char  *LclCharValuePtr;

    if((Status = atxmlw_Parse1641Xml(SignalDescription, &m_SignalDescription, Response, BufferSize)))
	{
         return(Status);
	}

    m_Action = atxmlw_Get1641SignalAction(m_SignalDescription, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Action %d", m_Action), Response, BufferSize);

    Status = atxmlw_Get1641SignalResource(m_SignalDescription, LclResource, Response, BufferSize);
    ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Resource [%s]", LclResource), Response, BufferSize);

    if((atxmlw_Get1641SignalOut(m_SignalDescription, m_SignalName, m_SignalElement)))
	{
        ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found SignalOut [%s] [%s]", m_SignalName, m_SignalElement), Response, BufferSize);
	}

	//attribute
	if((m_Attribute.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "attribute", &LclCharValuePtr)))
	{
		if(strcmp(LclCharValuePtr, "data")==0)
		{
			m_Attribute.Dim=DATA;
		}
		else if(strcmp(LclCharValuePtr, "config")==0)
		{
			m_Attribute.Dim=ALL;
		
			return 0;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s attribute [%s]",  m_SignalName, LclCharValuePtr), Response, BufferSize);
	}
	
	//timingValue
	if((m_TimingValue.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "timingValue", &LclDblValue, LclUnit)))
	{
		m_TimingValue.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found timingValue %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}

	//threeSamples
	if((m_ThreeSamples.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "threeSamples", &LclDblValue, LclUnit)))
	{
		m_ThreeSamples.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found threeSamples %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}

	//singleFilter
	if((m_SingleFilter.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "singleFilter", &LclDblValue, LclUnit)))
	{
		m_SingleFilter.Int=(int)LclDblValue;	
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found singleFilter %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}

	//acceptanceCode
	if((m_AcceptanceCode.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "acceptanceCode", &LclCharValuePtr)))
	{
		//m_AcceptanceCode.Int=(int)LclDblValue;
		bin_string_to_array(LclCharValuePtr, 32, &m_AcceptanceCode.Int);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found acceptanceCode %x",	m_AcceptanceCode.Int), Response, BufferSize);
	}

	//m_AcceptanceMask
	if((m_AcceptanceMask.Exists=atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "acceptanceMask", &LclCharValuePtr)))
	{
		//m_AcceptanceMask.Int=(int)LclDblValue;
		bin_string_to_array(LclCharValuePtr, 32, &m_AcceptanceMask.Int);
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found acceptanceMask %x", m_AcceptanceMask.Int), Response, BufferSize);
	}

	//mchannel
	if(( /*m_Channel.Exists=*/ atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "channel", &LclDblValue, LclUnit)))
	{
		int Intxx = (int)LclDblValue;
		
		if( Intxx == 1 )
		{
			m_Channel1.Exists = true;
			m_Channel1.Int    = (int)LclDblValue;
			mchannel          = m_Channel1.Int;
		}
		else
		{
			m_Channel1.Exists = false;
		}
		
		if( Intxx == 2 ) 
		{
			m_Channel2.Exists = true;
			m_Channel2.Int    = (int)LclDblValue;	
			mchannel          = m_Channel2.Int;
		}
		else
		{
			m_Channel2.Exists = false;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found channel %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}

	//data
	if((m_DataSize.Exists = atxmlw_Get1641StringAttribute(m_SignalDescription, m_SignalName, "data_bits", &LclCharValuePtr)))
	{
		strcpy(m_Data, LclCharValuePtr);
		m_DataSize.Int = 0;
		unstuff_string(&m_Data[0]);
		//bin_string_to_array(LclCharValuePtr, &m_Data[0], &m_DataSize.Int);
		{
			char  *cp     = strtok( m_Data, "," );
			int   indx    = 0;
			ULONG idbits  = 0;
			UINT  ioflags = 0;
			UINT  iodata  = 0;

			sscanf( m_Data, "%lx", &idbits );
			*((ULONG *)&m_DataSize.Address[0]) = idbits;
			cp = strtok( NULL, "," );
			sscanf( cp, "%x", &ioflags );
			*((UCHAR *)&m_DataSize.Address[4]) = (UCHAR)(ioflags & 0xf);
			cp = strtok( NULL, "," );
			
			while( cp != NULL )
			{
				sscanf( cp, "%x", &iodata );
				*((UCHAR *)&m_DataSize.Address[5+(m_DataSize.Int++)]) = (UCHAR)(iodata & 0xff);
				if( m_DataSize.Int > 7 )
				{
					break;
				}

				cp = strtok( NULL, "," );
			}

			idbits = 0 ;
		}
		
		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found %s data [%s]", m_SignalName, m_Data), Response, BufferSize);
	}

	//maxTime
    if((m_MaxTime.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "maxTime", &LclDblValue, LclUnit)))
	{
		m_MaxTime.Real=LclDblValue;

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Max-Time %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}

	//Listen Only
	if((m_ListenOnly.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "listenOnly", &LclDblValue, LclUnit)))
	{
		m_ListenOnly.Int=(int)LclDblValue;

		if( m_ListenOnly.Int == 0 )
		{
			m_ListenOnly.Exists = false;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Listen Only %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}
	
	//TalkListen
	if((m_TalkerListenerBits.Exists=atxmlw_Get1641DoubleAttribute(m_SignalDescription, m_SignalName, "talkerListenerBits", &LclDblValue, LclUnit)))
	{
		m_TalkerListenerBits.Int=(int)LclDblValue;

		if( m_TalkerListenerBits.Int == 0 )
		{
			m_TalkerListenerBits.Exists = false;
		}

		ATXMLW_DEBUG(5,atxmlw_FmtMsg("WrapGSI-Found Talker Listener Bits %E [%s]", LclDblValue,LclUnit), Response, BufferSize);
	}
	
	atxmlw_Close1641Xml(&m_SignalDescription);

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: InitPrivateCan_TDR010
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
void CCan_TDR010_T::InitPrivateCan_TDR010(void)
{
	m_TimingValue.Exists        = false;
	m_ThreeSamples.Exists       = false;
	m_SingleFilter.Exists       = false;
	m_AcceptanceCode.Exists     = false;
	m_AcceptanceMask.Exists     = false;
	m_DataSize.Exists           = false;
	m_MaxTime.Exists            = false;
	m_Attribute.Exists          = false;
	m_Channel1.Exists           = false;
	m_Channel1.Int              = 0;
	m_Channel2.Exists           = false;
	m_Channel2.Int              = 0;
	m_ListenOnly.Exists         = false;
	m_TalkerListenerBits.Exists = false;
	m_TalkerListenerBits.Int    = 0;
	startstatus[mchannel]       = -1;	// reset channel 2 flag
    
	//mchannel = 1;	//CJW added
    //ResetEvent(CanBusEvent);
	//ResetEvent(ThreadReadDone);

	return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: NullCalDataCan_TDR010
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
void CCan_TDR010_T::NullCalDataCan_TDR010(void)
{

    m_CalData[0] = 1.0;
	m_CalData[1] = 0.0;

    return;
}
//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////
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
void bin_string_to_array(char input [], int length, int array [])
{
	char string[2048] = "";
	char *temp;
	int  i = 0;

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
	char temp[40]="";

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
    for (int i = 0; i<(int)strlen(string); i++)
    {
	    if (string[i] == '"')
    	{
	        string[i] = 0x02;
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
    int j, length = (int)strlen(string);
    
	for (int i = 0; i<length; i++)
    {
	    if (string[i] == 0x02)
	    {
	        string[i] = '"';
        }

	if (string[i] == '\\')
	{
	    if (string[i + 1] == 'n')
	    {
		    string[i] = 0x0A;
		
		    for (j = i + 1; j<length; j++)
	 	    {
		        string[j] = string[j + 1];
	        }
		    
			length--;
	    }
	    else if (string[i + 1] == 't')
	    {
		    string[i] = 0x09;

		    for (j = i + 1; j<length; j++)
		    {
		        string[j] = string[j + 1];
		    }
		    
			length--;
	    }
	    else if (string[i + 1] == '\\')
	    {
		    for (j = i; j<length; j++)
		    {
		        string[j] = string[j + 1];
		    }
		    
			length--;
	    }
	}
 }

 return;
}
