//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlPdu_Freedom01502.cpp
//
// Date:	    07-JAN-2006
//
// Purpose:	    Instrument Driver for AtXmlPdu_Freedom01502
//
// Instrument:	AtXmlPdu_Freedom01502  Power Supplies (PDU)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  07JAN06  Baseline Release (Drop 2)
// 1.0.0.1  30JAN06  Update Interface methods & cleanup (Drop 3)
// 1.2.1.1  12FEB07  Corrected code to not set the the polarity relay if it was
//						previously set (setting it again causes PPU to Fault).
//					 Corrected code to display an error if user is performing a CHANGE
//						on the PPU from a positive to negative voltage or vise versa
//						without a reset in between.
// 1.2.1.2  13FEB07  Corrected code to keep track of each individual PPU's polarity
//						and previous voltage to aprropiately change the polarity relay.
//						this is needed because of the atlas CHANGE.
// 1.2.1.3  15FEB07  Customer required that the check for revision number in polarity
//						is negative, be removed.  In Enable_Parallel and Setup_Single
//						the revision of the firmware is no longer checked.
//					 Performed general code clean-up.
// 1.2.1.4  23FEB07  Customer requested that if error occured dealing with going
//						from positive to negative and vise versa to set the voltage
//						to zero as well give an error.
// 1.2.1.5  30MAR07  Addresses PCR 220.  Corrected code to set the voltage to zero
//						when going from positive to negative.  
// 1.4.0.1  20AUG07  Addresses PCR 172.  DME decreased the time for several of the
//						delays so instrument would power up more quickly.  
// 1.4.1.0  30JUL09  Deleted code in Reset() that was exiting function if device
//                      not physical resource. This behavior was preventing the
//                      programmable remove all sequence from reseting parallel
//                      configuration supplies.
// 1.4.2.0  19AUG10  Added Driver functions for aps6062_xxxx functions to allow
//                      PDU SAIS to call through CICL. Previously, CICL was only 
//                      called for viWrite, which left PDU in "InUse" state. 
//                      state is only returned to not in use on "_reset" call.
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "AtXmlWrapper.h"
#include "AtXmlFreedom01502_T.h"
#include "aps6062.h"
#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>

// Local Defines

#define SINGLE          1
#define PARALLEL        2
//#define	SERIES          3
//#define SERIES_PARALLEL 4
#define MAX_DEVICES     12 
#define MAX_RESOURCES   120 // # base, physical, and virtual devices allowed for s_DevInfo


typedef struct ResourceDataStruct
{
	char  ResourceName[ATXMLW_MAX_NAME];
	int   ConfigType;
	int   ModuleArr[15];
	int   ModuleCnt;
} ATXMLW_RESINFO;


// Static Variables
static int s_NumDev = 0;
static int s_NumRes = 0;
static int s_NumSing = 0;
static int s_NumPar = 0;
static int s_NumSer = 0;
static int s_NumSP = 0;
static int s_Dbg = 0;
static int s_Sim = 0;


static ATXMLW_DEVINFO s_DevInfo[MAX_RESOURCES]; 
static ViSession  s_HandleArr[MAX_DEVICES],
                  s_DefaultRM1 = NULL,
                  s_DefaultRM2 = NULL;

static ATXMLW_RESINFO s_ResInfo[MAX_RESOURCES]=
{  
	//	Resource Name			ConfigType	ModuleArr		ModuleCnt
	{	"DCPS_40V5A_1",			SINGLE,		{1},					1},
	{	"DCPS_40V5A_2",			SINGLE,		{2},					1},
	{	"DCPS_40V5A_3",			SINGLE,		{3},					1},
	{	"DCPS_40V5A_4",			SINGLE,		{4},					1},
	{	"DCPS_40V5A_5",			SINGLE,		{5},					1},
	{	"DCPS_40V5A_6",			SINGLE,		{6},					1},
	{	"DCPS_40V5A_7",			SINGLE,		{7},					1},
	{	"DCPS_40V5A_8",			SINGLE,		{8},					1},
	{	"DCPS_40V5A_9",			SINGLE,		{9},					1},
	{	"DCPS_65V5A_10",		SINGLE,		{10},					1},
	{	"DCPS_40V10A_89",		PARALLEL,	{8,9},					2},
	{	"DCPS_40V10A_78",		PARALLEL,	{7,8},					2},
	{	"DCPS_40V15A_789",		PARALLEL,	{7,8,9},				3},
	{	"DCPS_40V10A_67",		PARALLEL,	{6,7},					2},
	{	"DCPS_40V15A_678",		PARALLEL,	{6,7,8},				3},
	{	"DCPS_40V20A_6789",		PARALLEL,	{6,7,8,9},				4},
	{	"DCPS_40V10A_56",		PARALLEL,	{5,6},					2},
	{	"DCPS_40V15A_567",		PARALLEL,	{5,6,7},				3},
	{	"DCPS_40V20A_5678",		PARALLEL,	{5,6,7,8},				4},
	{	"DCPS_40V25A_56789",	PARALLEL,	{5,6,7,8,9},			5},
	{	"DCPS_40V10A_45",		PARALLEL,	{4,5},					2},
	{	"DCPS_40V15A_456",		PARALLEL,	{4,5,6},				3},
	{	"DCPS_40V20A_4567",		PARALLEL,	{4,5,6,7},				4},
	{	"DCPS_40V25A_45678",	PARALLEL,	{4,5,6,7,8},			5},
	{	"DCPS_40V30A_456789",	PARALLEL,	{4,5,6,7,8,9},			6},
	{	"DCPS_40V10A_34",		PARALLEL,	{3,4},					2},
	{	"DCPS_40V15A_345",		PARALLEL,	{3,4,5},				3},
	{	"DCPS_40V20A_3456",		PARALLEL,	{3,4,5,6},				4},
	{	"DCPS_40V25A_34567",	PARALLEL,	{3,4,5,6,7},			5},
	{	"DCPS_40V30A_345678",	PARALLEL,	{3,4,5,6,7,8},			6},
	{	"DCPS_40V35A_3456789",	PARALLEL,	{3,4,5,6,7,8,9},		7},
	//	Resource Name			ConfigType	ModuleArr		ModuleCnt
	{	"DCPS_40V10A_23",		PARALLEL,	{2,3},					2},
	{	"DCPS_40V15A_234",		PARALLEL,	{2,3,4},				3},
	{	"DCPS_40V20A_2345",		PARALLEL,	{2,3,4,5},				4},
	{	"DCPS_40V25A_23456",	PARALLEL,	{2,3,4,5,6},			5},
	{	"DCPS_40V30A_234567",	PARALLEL,	{2,3,4,5,6,7},			6},
	{	"DCPS_40V35A_2345678",	PARALLEL,	{2,3,4,5,6,7,8},		7},
	{	"DCPS_40V40A_23456789",	PARALLEL,	{2,3,4,5,6,7,8,9},		8},
	{	"DCPS_40V10A_12",		PARALLEL,	{1,2},					2},
	{	"DCPS_40V15A_123",		PARALLEL,	{1,2,3},				3},
	{	"DCPS_40V20A_1234",		PARALLEL,	{1,2,3,4},				4},
	{	"DCPS_40V25A_12345",	PARALLEL,	{1,2,3,4,5},			5},
	{	"DCPS_40V30A_123456",	PARALLEL,	{1,2,3,4,5,6},			6},
	{	"DCPS_40V35A_1234567",	PARALLEL,	{1,2,3,4,5,6,7},		7},
	{	"DCPS_40V40A_12345678",	PARALLEL,	{1,2,3,4,5,6,7,8},		8},
	{	"DCPS_40V45A_123456789",PARALLEL,	{1,2,3,4,5,6,7,8,9},	9},
	{	"",						0,			{0},					0},
};



// Local Function Prototypes
static void s_GetDcpsHandle(int InstNo, char *m_ResourceName, ATXMLW_INSTRUMENT_ADDRESS AddressInfo, int m_Sim, int m_Dbg, ATXMLW_INTF_RESPONSE* Response, int BufferSize, ViSession *Handle);

// Forward referenced
ATXMLW_WRAP_FNC int Reset(int Instno, int ResourceType, char* ResourceName, ATXMLW_INTF_RESPONSE* Response, int BufferSize);


//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////
BOOL APIENTRY DllMain( HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
{
    switch (ul_reason_for_call)
	{
		case DLL_PROCESS_ATTACH:
		case DLL_THREAD_ATTACH:
		case DLL_THREAD_DETACH:
		case DLL_PROCESS_DETACH:
			;
    }
    return TRUE;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC Initialize
//
// Purpose: Initialize the instrument driver Called for each Base/Physical/Virtual
//            Resource
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
ATXMLW_WRAP_FNC int Initialize(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{	
	int i=0;
	int m_Sim, m_Dbg;
	char m_ResourceName[56] = {0};		
	char * strPtr = NULL;
	char			TmpBuf[_MAX_PATH];
	struct _stat	StatBuf;

	if (DE_BUG == 0) {
		sprintf(TmpBuf, "%s", DEBUG_DCPS);
		DE_BUG = _stat(TmpBuf, &StatBuf) == -1 ? 0 : 1;
	}

	ISDODEBUG(dodebug(0, "Initialize()", "Entering function", (char*)0));

    // Insure Response is empty
    if(Response != NULL)
	{
		memset(Response, '\0',BufferSize);
	}

    // Check if not already initialized
    if(s_NumDev == 0)
    {
        memset(s_DevInfo,0,sizeof(s_DevInfo));
    }
	
	// Check for MAX Driver Overflow
    if(s_NumDev >= MAX_RESOURCES)
    {
        atxmlw_ErrorResponse(ResourceName,Response,BufferSize, "Initialize ", ATXMLW_WRAPPER_ERRCD_TOO_MANY_DEVICES, ATXMLW_WRAPPER_ERRMSG_TOO_MANY_DEVICES);
        return(ATXMLW_WRAPPER_ERRCD_TOO_MANY_DEVICES);
    }
	
	/*****************************NEW************************************/
	// Save Device Array Info--instead of calling cs_GetDevInfo like in B1B
    s_DevInfo[s_NumDev].InstNo = Instno;
    s_DevInfo[s_NumDev].ResourceType = ResourceType;
    if(ResourceName)
    {
        strnzcpy(s_DevInfo[s_NumDev].ResourceName, ResourceName, ATXMLW_MAX_NAME);
    }
    s_DevInfo[s_NumDev].Dbg = Dbglvl;
    s_DevInfo[s_NumDev].Sim = Sim;
    
    if(AddressInfoPtr)  //Save Address Information
    {
        s_DevInfo[s_NumDev].AddressInfo.ResourceAddress = AddressInfoPtr->ResourceAddress;
        if(AddressInfoPtr->InstrumentQueryID)
        {
            strnzcpy(s_DevInfo[s_NumDev].AddressInfo.InstrumentQueryID, AddressInfoPtr->InstrumentQueryID, ATXMLW_MAX_NAME);
        }
        s_DevInfo[s_NumDev].AddressInfo.InstrumentTypeNumber = AddressInfoPtr->InstrumentTypeNumber;
        if(AddressInfoPtr->ControllerType)
        {
            strnzcpy(s_DevInfo[s_NumDev].AddressInfo.ControllerType, AddressInfoPtr->ControllerType, ATXMLW_MAX_NAME);
        }
        s_DevInfo[s_NumDev].AddressInfo.ControllerNumber = AddressInfoPtr->ControllerNumber;
        s_DevInfo[s_NumDev].AddressInfo.PrimaryAddress = AddressInfoPtr->PrimaryAddress;
        s_DevInfo[s_NumDev].AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
        s_DevInfo[s_NumDev].AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
    }

	
	if (s_DevInfo[s_NumDev].ResourceType == ATXMLW_RESTYPE_BASE)
	{
		s_Dbg = s_DevInfo[s_NumDev].Dbg;
		s_Sim = s_DevInfo[s_NumDev].Sim;
		while(strcmp(s_ResInfo[i].ResourceName,"")!=0) 
		{
			i++;
		}
		s_NumRes = i;
	}

	m_Sim = s_Sim;
	m_Dbg = s_Dbg;
	strnzcpy(m_ResourceName, ResourceName, ATXMLW_MAX_NAME);
	ATXMLW_DEBUG(5, ResourceName, Response, BufferSize);

	//if resource type is physical, get the handle
	if (s_DevInfo[s_NumDev].ResourceType == ATXMLW_RESTYPE_PHYSICAL)
	{
		s_GetDcpsHandle(s_DevInfo[s_NumDev].InstNo, s_DevInfo[s_NumDev].ResourceName, s_DevInfo[s_NumDev].AddressInfo, s_DevInfo[s_NumDev].Sim,
                    s_DevInfo[s_NumDev].Dbg, Response, BufferSize, &s_HandleArr[s_DevInfo[s_NumDev].AddressInfo.SecondaryAddress]);

		
		//s_Handle = Handle;
		//determine length of resource table defined above
		
	}

	for(i = 0; i < s_NumRes; i++) //go thru entire s_ResInfo table defined at the top
	{
		if (strcmp(s_ResInfo[i].ResourceName, s_DevInfo[s_NumDev].ResourceName)==0)
		{
			if (s_ResInfo[i].ConfigType == SINGLE) 
			{
				s_DevInfo[s_NumDev].DriverClass = new CAtXmlFreedom01502_Single_T( s_ResInfo[i].ResourceName, s_ResInfo[i].ConfigType, s_ResInfo[i].ModuleArr[0],
							s_ResInfo[i].ModuleCnt, s_DevInfo[s_NumDev].Dbg, s_DevInfo[s_NumDev].Sim,s_HandleArr[s_DevInfo[s_NumDev].AddressInfo.SecondaryAddress], Response, BufferSize);
				break;
				//s_NumSing++;
			}
			if (s_ResInfo[i].ConfigType==PARALLEL) 
			{
				s_DevInfo[s_NumDev].DriverClass = new CAtXmlFreedom01502_Parallel_T( s_ResInfo[i].ResourceName, s_ResInfo[i].ConfigType,s_ResInfo[i].ModuleArr,
							s_ResInfo[i].ModuleCnt, s_DevInfo[s_NumDev].Dbg,s_DevInfo[s_NumDev].Sim,s_HandleArr, Response, BufferSize);
				break;
				//s_NumPar++;
			}

		}
	}

    s_NumDev++;  //s_NumDev includes the controller device	
	
	ISDODEBUG(dodebug(0, "Initialize()", "Leaving function", (char*)0));
	return (0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC Close
//
// Purpose: Close the instrument driver
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
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

ATXMLW_WRAP_FNC int Close(void)
{
    ISDODEBUG(dodebug(0, "Close()", "Entering function", (char*)0));

	s_NumDev = 0;

	ISDODEBUG(dodebug(0, "Close()", "Leaving function", (char*)0));
	return 0;
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC RegisterTSF
//
// Purpose: Send an ATXMLW IEEE 1641 (TSF) description to the instrument driver
//          For efficency sake, This function is not currently implemented
//          When implemented, It will cash the TSF locally and Interseed
//          IssueSignal via local static routines.
//          It will not pass it on to the _T processing!
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////

ATXMLW_WRAP_FNC int RegisterTSF(ATXMLW_XML_FILENAME* TSFSignalDefinition, ATXMLW_XML_FILENAME* TSFLibrary, ATXMLW_XML_FILENAME* STDTSF, ATXMLW_XML_FILENAME* STDBSC)
{
    return(ATXMLW_WRAPPER_ERRCD_REGISTER_TSF_NOT_IMPLEMENTED);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC IssueSignal
//
// Purpose: Send an ATXMLW IEEE 1641 (BSC) signal description to the instrument driver
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
// SignalDescription ATXMLW_INTF_SIGDESC*  IEEE 1641 BSC Signal description + action/resource
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
ATXMLW_WRAP_FNC int IssueSignal(int Instno, int ResourceType, char* ResourceName, ATXMLW_INTF_SIGDESC* SignalDescription, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int i;
    char  cInstNo[20];
    void *DriverClass = NULL;
	int Status = 0;
	short ConfigType;
	int m_Dbg, m_Sim; //used in macros
	char m_ResourceName[56] = {0}; //used in macros

	
  	ISDODEBUG(dodebug(0, "IssueSignal()", "Entering function", (char*)0));
    // Insure Response is empty
    if(Response != NULL)
	{
		memset(Response, '\0', (size_t)BufferSize);
	}
		
	m_Dbg = s_Dbg;
	m_Sim = s_Sim;
	strnzcpy(m_ResourceName, ResourceName, ATXMLW_MAX_NAME); //set 56th char to NULL

	if((ResourceType == ATXMLW_RESTYPE_BASE) && strstr(SignalDescription,"Reset"))
	{
		Status = Reset(Instno, ResourceType, ResourceName, Response, BufferSize);
		return(Status);
	}

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }
    // Any Errors ?
    if(i >= s_NumDev)
    {
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "IssueSignal ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }

    // 
	if (ConfigType == SINGLE)
	{
			((CAtXmlFreedom01502_Single_T *)(DriverClass))->IssueSignalAtXmlFreedom01502_Single(SignalDescription, Response, BufferSize);
	}
	else if (ConfigType == PARALLEL)//parallel
	{
			((CAtXmlFreedom01502_Parallel_T *)(DriverClass))->IssueSignalAtXmlFreedom01502_Parallel(SignalDescription, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "IssueSignal()", "Leaving function.", (char*) NULL));

    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC Status
//
// Purpose: Query the instrument status
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
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
ATXMLW_WRAP_FNC int Status(int Instno, int ResourceType, char* ResourceName,
                         ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   i;
    char  cInstNo[20];
    void *DriverClass = NULL;
	short ConfigType;

    // Insure Response is empty
    ISDODEBUG(dodebug(0, "Status()", "Entering function", (char*)0));

	if(Response != NULL) {
		memset(Response, '\0', (size_t)BufferSize);
	}

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }

    // Any Errors ?
    if(i >= s_NumDev)
    {
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "Status ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }

	if (ConfigType == SINGLE)
	{
			((CAtXmlFreedom01502_Single_T *)(DriverClass))->StatusAtXmlFreedom01502_Single(Response, BufferSize);
	}

	else if (ConfigType == PARALLEL)//parallel
	{
			((CAtXmlFreedom01502_Parallel_T *)(DriverClass))->StatusAtXmlFreedom01502_Parallel(Response, BufferSize);
	}
	
    ISDODEBUG(dodebug(0, "Status()", "Leaving function", (char*)0));
    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC RegCal
//
// Purpose: Register the latest Calibration Factors to the Instrument Driver
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
// CalData           ATXMLW_INTF_CALDATA*  XML String for cal data
//
// Output Parameters
// Parameter		Type			    Purpose
// ===============  =================== ===========================================
//
// Return:
//    0 on success.
//   -ErrorCode on failure
//
///////////////////////////////////////////////////////////////////////////////
ATXMLW_WRAP_FNC int RegCal(int Instno, int ResourceType, char* ResourceName, ATXMLW_INTF_CALDATA* CalData)
{
    int   i;
    void *DriverClass = NULL;
	short ConfigType;

	ISDODEBUG(dodebug(0, "RegCal()", "Entering function", (char*)0));

    // Find the class to invoke
    if(ResourceType != ATXMLW_RESTYPE_PHYSICAL)
	{
        return(0);
	}

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }

	// Any Errors ?
    if(i >= s_NumDev)
    {
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }
	if (ConfigType == SINGLE)
	{
			((CAtXmlFreedom01502_Single_T *)(DriverClass))->RegCalAtXmlFreedom01502_Single(CalData);
	}
	else
	{
		return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
	}


    return(0);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC Reset
//
// Purpose: Reset the instrument
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
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
ATXMLW_WRAP_FNC int Reset(int Instno, int ResourceType, char* ResourceName,
                         ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int i;
    char  cInstNo[20];
    void *DriverClass = NULL;
	short ConfigType;
	//declare dummy "overall DCPS" member variables
	int m_Dbg, m_Sim; //used in macros
	char m_ResourceName[56]=""; //used in macros

	ISDODEBUG(dodebug(0, "Reset()", "Entering function", (char*)0));
	
    // Insure Response is empty
	if(Response != NULL) 
	{
		memset(Response, '\0', (size_t)BufferSize);
	}

	if(ResourceType == ATXMLW_RESTYPE_BASE)
	{
		// reset all single resources
		for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
		{
			if(s_ResInfo[i].ConfigType == SINGLE)
			{
				DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
				((CAtXmlFreedom01502_Single_T *)(DriverClass))->ResetAtXmlFreedom01502_Single(Response, BufferSize);
			}
		}
		return(0);
	}

	m_Dbg = s_Dbg;
	m_Sim = s_Sim;
	strnzcpy(m_ResourceName, ResourceName, ATXMLW_MAX_NAME); //set 56th char to NULL

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }

    // Any Errors ?
    if(i >= s_NumDev)
    {
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "Reset ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }


	if (ConfigType == SINGLE)
	{
			((CAtXmlFreedom01502_Single_T *)(DriverClass))->ResetAtXmlFreedom01502_Single(Response, BufferSize);
	}
	else if (ConfigType == PARALLEL)//parallel
	{
			((CAtXmlFreedom01502_Parallel_T *)(DriverClass))->ResetAtXmlFreedom01502_Parallel(Response, BufferSize);
	}
	
	ISDODEBUG(dodebug(0, "Reset()", "Leaving function", (char*)0));
    return(0);
}


///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC Ist
//
// Purpose: Invoke Instrument Self Test and return response
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ===========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
// Level             int                 Instrument Self Test Level ATXMLW_IST_LVL 
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
ATXMLW_WRAP_FNC int Ist(int Instno, int ResourceType, char* ResourceName, int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int   i;
    char  cInstNo[20];
    void *DriverClass = NULL;
	short ConfigType;
	
    ISDODEBUG(dodebug(0, "Ist()", "Entering function", (char*)0));
    // Insure Response is empty
	if(Response != NULL) 
	{
		memset(Response, '\0', (size_t)BufferSize);
	}

    // Find the class to invoke
    if(ResourceType != ATXMLW_RESTYPE_PHYSICAL)
	{
        return(0);
	}

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }

    // Any Errors ?
    if(i >= s_NumDev)
    {
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "Ist ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }

	if (ConfigType == SINGLE)
			((CAtXmlFreedom01502_Single_T *)(DriverClass))->IstAtXmlFreedom01502_Single(Level, Response, BufferSize);
	else
	{
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "Ist ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }
	
	ISDODEBUG(dodebug(0, "Ist()", "Leaving function", (char*)0));
    return(0);
}

//++++/////////////////////////////////////////////////////////////////////////
// Special Non-ATXMLW Cheat Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC IssueNativeCmds
//
// Purpose: Invoke Instrument Self Test and return response
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
// InstrumentCmds    ATXMLW_INTF_INSTCMD*  XML String for Instrument Commands
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
ATXMLW_WRAP_FNC int IssueNativeCmds(int Instno, int ResourceType, char* ResourceName,
                         ATXMLW_INTF_INSTCMD* InstrumentCmds,
                         ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int    i;
    char   cInstNo[20];
    void  *DriverClass = NULL;
	short  ConfigType;
    int    Status = 0;

	ISDODEBUG(dodebug(0, "IssueNativeCmds()", "Entering function", (char*)0));

    // Insure Response is empty
	if(Response != NULL) 
	{
		memset(Response, '\0', (size_t)BufferSize);
	}

    // Find the class to invoke
    if(ResourceType != ATXMLW_RESTYPE_PHYSICAL)
	{
        return(0);
	}

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }

    // Any Errors ?
    if(i >= s_NumDev)
    {
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "IssueNativeCmds ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }

	if (ConfigType == SINGLE)
	{
		Status = ((CAtXmlFreedom01502_Single_T *)(DriverClass))->IssueNativeCmdsAtXmlFreedom01502_Single(InstrumentCmds, Response, BufferSize);
	}

	else if (ConfigType == PARALLEL)//parallel
	{
		Status = ((CAtXmlFreedom01502_Parallel_T *)(DriverClass))->IssueNativeCmdsAtXmlFreedom01502_Parallel(InstrumentCmds, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "IssueNativeCmds()", "Leaving function", (char*)0));
    return(Status);
}

///////////////////////////////////////////////////////////////////////////////
// Function: ATXMLW_WRAP_FNC IssueDriverFunctionCall
//
// Purpose: Invoke Instrument Self Test and return response
//
// Input Parameters
// Parameter		 Type			    Purpose
// ================= ==================  ========================================
// Instno            int                 System assigned instrument number
// ResourceType      int                 Type of Resource Base, Physical, Virtual
// ResourceName      char*               Station resource name
// DriverFunction    ATXMLW_INTF_DRVRFNC*  XML String for Driver Function and parameters
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
ATXMLW_WRAP_FNC int IssueDriverFunctionCall(int Instno, int ResourceType, char* ResourceName, ATXMLW_INTF_DRVRFNC* DriverFunction, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
    int i;
    char  cInstNo[20];
    void *DriverClass = NULL;
	short ConfigType;

    ISDODEBUG(dodebug(0, "IssueDriverFunctionCall()", "Entering function", (char*)0));
	
	// Insure Response is empty
	if(Response != NULL)
	{
		memset(Response, '\0', (size_t)BufferSize);
	}

    // Find the class to invoke
    if(ResourceType != ATXMLW_RESTYPE_PHYSICAL)
        return(0);

    // Find the class to invoke
    for(i = 0; i < s_NumDev; i++) // subtract 1 since s_NumDev includes controller
    {
        //if(s_DevInfo[i].InstNo == Instno)
		if(strcmp(s_ResInfo[i].ResourceName, ResourceName)==0)
        {
            DriverClass = (void *)(s_DevInfo[i+1].DriverClass);
			ConfigType = s_ResInfo[i].ConfigType;
            break;
        }
    }
    // Any Errors ?
    if(i >= s_NumDev)
    {
        itoa(Instno,cInstNo,10);
        atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "IssueDriverFunctionCall ", ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
        return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }
    
	if (ConfigType == SINGLE)
	{
			((CAtXmlFreedom01502_Single_T *)(DriverClass))->IssueDriverFunctionCallAtXmlFreedom01502_Single(DriverFunction, Response, BufferSize);
	}

	else if (ConfigType == PARALLEL)//parallel
	{
			((CAtXmlFreedom01502_Parallel_T *)(DriverClass))->IssueDriverFunctionCallAtXmlFreedom01502_Parallel(DriverFunction, Response, BufferSize);
	}

	ISDODEBUG(dodebug(0, "IssueDriverFunctionCall()", "Leaving function", (char*)0));
    return(0);
}

                                  
//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: GetDcpsHandle()
//
// Purpose: Open only one handle for each physical device
//
// Note:  The m_ prefix on m_DeviceName, m_Sim, and m_Dbg is required to use the
//        IFNSIM and CEMDEBUG macros.
//
// Return:
//    ViSession RM, ViSession Handle and
//    s_Arb_1_Handle or s_Arb_2_Handle set
//
//////////////////////////////////////////////////////////////
void s_GetDcpsHandle(int InstNo, char *m_ResourceName, ATXMLW_INSTRUMENT_ADDRESS AddressInfo, int m_Sim, int m_Dbg, ATXMLW_INTF_RESPONSE* Response, int BufferSize, ViSession *Handle)
{
	char     InitString[128];
    char     LclMsg[128];
    ViStatus Status=0;

	InitString[0] = '\0';

	ISDODEBUG(dodebug(0, "s_GetDcpsHandle()", "Entering function", (char*)0));
	// The Form Init String
    sprintf(InitString,"%s%d::%d::%d::INSTR", AddressInfo.ControllerType,AddressInfo.ControllerNumber, AddressInfo.PrimaryAddress,AddressInfo.SecondaryAddress);
	sprintf(LclMsg,"Wrap-CAtXmlFreedom01502 Class called with Instno %d, Sim %d, Dbg %d",InstNo, m_Sim, m_Dbg);
    ATXMLW_DEBUG(5,LclMsg,Response,BufferSize);

	//Initialize device
	IFNSIM(Status = viOpenDefaultRM (Handle));
    if (Status < VI_SUCCESS)
    {
        sprintf(LclMsg,"Wrap-Instrument %s viOpenDefaultRM Failed", m_ResourceName);

        ATXMLW_ERROR(Status,"Wrap-CAtXmlFreedom01502",LclMsg,Response,BufferSize);
        m_Sim = 1;
        return;
    }
	sprintf(LclMsg, "%x = viOpenDefaultRM;", Status);
    ATXMLW_DEBUG(2,LclMsg,Response,BufferSize);

	// Get Instrument Handle
	
    IFNSIM(Status = viOpen(*Handle, InitString, VI_NULL, VI_NULL, Handle));

    if (Status || (*Handle == 0))
	{
        sprintf(LclMsg,"Wrap-Instrument %s OpenString %s ",
                        m_ResourceName, InitString);
        ATXMLW_ERROR(Status,"Wrap-CAtXmlFreedom01502",LclMsg,Response,BufferSize);
        m_Sim = 1;
		return;
	}
	sprintf(LclMsg, "Wrap-CAtXmlFreedom01502_T %x = viOpen %s;", Status, InitString);
    ATXMLW_DEBUG(5,LclMsg,Response,BufferSize);
    
	ISDODEBUG(dodebug(0, "s_GetDcpsHandle()", "Leaving function", (char*)0));
    return;

}

