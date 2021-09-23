//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Cbts.cpp
//
// Date:	    26JUN06
//
// Purpose:	    Instrument Driver for Cbts
//
// Instrument:	Cbts  <device description> (DMM)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtXmlWrapper.lib       ..\..\Common\lib  (EADS Wrapper support functions)
//
//
//
// Revision History described in Cbts_T.cpp
//
// 1.4.1.0  04JUN07  Updated CBTS per DME changes             DME
// 1.4.1.1  30MAY19  'CAN" string compare length incorrect    CJW
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "AtxmlWrapper.h"
#include "CDDI_T.h"
#include "PCIDM_T.h"
#include "MIC_T.h"
#include "CAN_T.h"
//#include "CAN2_T.h"

// Local Defines
#define CDDI	1
#define PCIDM	2
#define MIC		3
#define CAN     4
#define CAN2	5

#define MAX_DEVICES  10

// Static Variables
static int s_NumDev = 0;
static     ATXMLW_DEVINFO s_DevInfo[MAX_DEVICES]; // Device and Address information
char       WrapperType[MAX_DEVICES];
const char VXI_ADDRESS[] = "VXI0::29::INSTR";
// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////
BOOL APIENTRY DllMain(HANDLE hModule, DWORD  ul_reason_for_call, LPVOID lpReserved)
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

ATXMLW_WRAP_FNC int Initialize(int Instno, int ResourceType, char* ResourceName, int Sim, int Dbglvl, ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr, ATXMLW_INTF_RESPONSE* Response, int Buffersize)
{

	// Insure Response is empty
    if(Response != NULL)
	{
        Response[0] = '\0';
	}

	// Check if not already initialized
    if(s_NumDev == 0)
    {
        memset(s_DevInfo,0,sizeof(s_DevInfo));
    }

    //CJW Removed if(ResourceType == ATXMLW_RESTYPE_PHYSICAL)
    {
        // Check for MAX Driver Overflow
        if(s_NumDev >= MAX_DEVICES)
        {
            atxmlw_ErrorResponse(ResourceName,Response,Buffersize, "Initialize ", ATXMLW_WRAPPER_ERRCD_TOO_MANY_DEVICES, ATXMLW_WRAPPER_ERRMSG_TOO_MANY_DEVICES);
            
			return(ATXMLW_WRAPPER_ERRCD_TOO_MANY_DEVICES);
        }

        // Save Device Array Info
        s_DevInfo[s_NumDev].InstNo = Instno;
        s_DevInfo[s_NumDev].ResourceType = ResourceType;

        if(ResourceName)
        {
            strnzcpy(s_DevInfo[s_NumDev].ResourceName, ResourceName, ATXMLW_MAX_NAME);
        }

        s_DevInfo[s_NumDev].Dbg = Dbglvl;
        s_DevInfo[s_NumDev].Sim = Sim;

        // Save Address Information
        if(AddressInfoPtr)
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
            s_DevInfo[s_NumDev].AddressInfo.PrimaryAddress   = AddressInfoPtr->PrimaryAddress;
            s_DevInfo[s_NumDev].AddressInfo.SecondaryAddress = AddressInfoPtr->SecondaryAddress;
            s_DevInfo[s_NumDev].AddressInfo.SubModuleAddress = AddressInfoPtr->SubModuleAddress;
		}

		//else if (strncmp(ResourceName, "CAN", 3) == 0)
		if (strncmp(ResourceName, "CAN_1", 5) == 0)
		{
		    s_DevInfo[s_NumDev].DriverClass = new CCan_TDR010_T(
							s_DevInfo[s_NumDev].InstNo,
							s_DevInfo[s_NumDev].ResourceType,
							s_DevInfo[s_NumDev].ResourceName,
							s_DevInfo[s_NumDev].Sim,
							s_DevInfo[s_NumDev].Dbg, 
							&s_DevInfo[s_NumDev].AddressInfo,
							Response, Buffersize);
			
			WrapperType[s_NumDev]=CAN;
			
			//CJW Removed if (((CCan_TDR010_T *)(s_DevInfo[s_NumDev].DriverClass))->Open((char*)VXI_ADDRESS) == 0) // CODE_CHECK: ANTHONY ADDED
			
			s_NumDev++;
		}

		//if (strncmp(ResourceName, "CAN_2", 5) == 0)
		//{
		//    s_DevInfo[s_NumDev].DriverClass = new CCan2_TDR010_T(
		//	  s_DevInfo[s_NumDev].InstNo,
		//	  s_DevInfo[s_NumDev].ResourceType,
		//	  s_DevInfo[s_NumDev].ResourceName,
		//	  s_DevInfo[s_NumDev].Sim,
		//	  s_DevInfo[s_NumDev].Dbg,
		//	  &s_DevInfo[s_NumDev].AddressInfo,
		//	  Response, Buffersize);
		// 
		//    WrapperType[s_NumDev] = CAN2;
		//    if (((CCan2_TDR010_T *)(s_DevInfo[s_NumDev].DriverClass))->Open((char*)VXI_ADDRESS) == 0) // CODE_CHECK: ANTHONY ADDED
		//	      s_NumDev++;
		//}
    }

//	CoInitialize(NULL);	

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
    int i;

    for(i = 0; i < s_NumDev; i++)
    {
        if(s_DevInfo[i].DriverClass != NULL)
		{
			switch(WrapperType[i])
			{
			    case CAN:
				    delete((CCan_TDR010_T *)(s_DevInfo[i].DriverClass));
				    break;

			    //case CAN2:
			        //delete((CCan2_TDR010_T *)(s_DevInfo[i].DriverClass));
			        //break;
			}
		}

        s_DevInfo[i].DriverClass = NULL;
	}

    s_NumDev = 0;

//	CoUninitialize();

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
// ================================== ===========================================
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

    // Insure Response is empty
    if(Response != NULL)
	{
        Response[0] = '\0';
	}

    // Find the class to invoke via Instno or ResourceName etc.
    for(i = 0; i < s_NumDev; i++)
    {
        if(strcmp(ResourceName, s_DevInfo[i].ResourceName) == 0)
        {
            DriverClass = (void *)(s_DevInfo[i].DriverClass);
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

    //else if (strncmp(ResourceName, "CAN", 4) == 0)

    if (strncmp(ResourceName, "CAN_1", 5) == 0)
	{
	    ((CCan_TDR010_T *)(DriverClass))->IssueSignalCan_TDR010(SignalDescription,
		Response, BufferSize);
	}

	//else if (strncmp(ResourceName, "CAN2", 4) == 0)
	//{
	//    ((CCan2_TDR010_T *)(DriverClass))->IssueSignalCan_TDR010(SignalDescription,
	//    Response, BufferSize);
	//}

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

    // Insure Response is empty
    if (Response != NULL)
	{
	    Response[0] = '\0';
	}

    // Find the class to invoke via Instno or ResourceName etc.
    for (i = 0; i < s_NumDev; i++)
    {
	    if (strcmp(ResourceName, s_DevInfo[i].ResourceName) == 0)
	    {
	        DriverClass = (void *)(s_DevInfo[i].DriverClass);
	        break;
	    }
    }
 
	// Any Errors ?
    if (i >= s_NumDev)
    {
	    itoa(Instno, cInstNo, 10);
	    atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "Status ",
	    ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND,
	    ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
	
		return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
    }

    if (strncmp(ResourceName, "CAN_", 4) == 0)	//CJW
    {
	    ((CCan_TDR010_T *)(DriverClass))->StatusCan_TDR010(Response, BufferSize);
    }
    //else if (strncmp(ResourceName, "CAN2", 4) == 0)
    //{
	//    ((CCan2_TDR010_T *)(DriverClass))->StatusCan_TDR010(Response, BufferSize);
    //}

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

	// Find the class to invoke via Instno or ResourceName etc.
	for (i = 0; i < s_NumDev; i++)
	{
	    if (strcmp(ResourceName, s_DevInfo[i].ResourceName) == 0)
	    {
		    DriverClass = (void *)(s_DevInfo[i].DriverClass);
		    break;
	    }
	}
	
	// Any Errors ?
	if (i >= s_NumDev)
	{
	    return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
	}

	if (strncmp(ResourceName, "CAN_", 4) == 0)	//CJW
	{
	    ((CCan_TDR010_T *)(DriverClass))->RegCalCan_TDR010(CalData);
	}
	//else if (strncmp(ResourceName, "CAN2", 4) == 0)
	//{
	//    ((CCan2_TDR010_T *)(DriverClass))->RegCalCan_TDR010(CalData);
	//}

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
ATXMLW_WRAP_FNC int Reset(int Instno, int ResourceType, char* ResourceName,	ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int   i;
	void *DriverClass = NULL;

	// Insure Response is empty
	if (Response != NULL)
	{
	    Response[0] = '\0';
	}

	// Find the class to invoke via Instno or ResourceName etc.
	for (i = 0; i < s_NumDev; i++)
	{
	    // Reset All devices on Instno = 0 (Probably never used)
	    if (s_DevInfo[i].ResourceName && ((strcmp(s_DevInfo[i].ResourceName, ResourceName) == 0) || (strcmp(ResourceName, "CBTS") == 0)))
	    {
		    if (s_DevInfo[i].DriverClass)
		    {
		        if ((strncmp(s_DevInfo[i].ResourceName, "CAN_", 4) == 0))	//CJW
		        {
			        ((CCan_TDR010_T *)(s_DevInfo[i].DriverClass))->ResetCan_TDR010(Response, BufferSize);
		        }
		        
				//if ((strncmp(s_DevInfo[i].ResourceName, "CAN2", 4) == 0))
		        //{
			    //    ((CCan2_TDR010_T *)(s_DevInfo[i].DriverClass))->ResetCan_TDR010(Response, BufferSize);
		        //}
		    }
	    }
	}

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

	// Insure Response is empty
	if (Response != NULL)
	{
	    Response[0] = '\0';
	}

	// Find the class to invoke via Instno or ResourceName etc.
	for (i = 0; i < s_NumDev; i++)
	{
	    if (strcmp(ResourceName, s_DevInfo[i].ResourceName) == 0)
	    {
		    DriverClass = (void *)(s_DevInfo[i].DriverClass);
		    break;
	    }
	}

	// Any Errors ?
	if (i >= s_NumDev)
	{
	    itoa(Instno, cInstNo, 10);
	    atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "Ist ",
		ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND,
		ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
	 
		return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
	}

	if (strncmp(ResourceName, "CAN_1", 5) == 0)	//CJW
	{
	    ((CCan_TDR010_T *)(DriverClass))->IstCan_TDR010(Level, Response, BufferSize);
	}
	//else if (strncmp(ResourceName, "CAN2", 4) == 0)
	//{
	//    ((CCan2_TDR010_T *)(DriverClass))->IstCan_TDR010(Level, Response, BufferSize);
	//}

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
ATXMLW_WRAP_FNC int IssueNativeCmds(int Instno, int ResourceType, char* ResourceName, ATXMLW_INTF_INSTCMD* InstrumentCmds, ATXMLW_INTF_RESPONSE* Response, int BufferSize)
{
	int i;
	char  cInstNo[20];
	void *DriverClass = NULL;
	int   Status = 0;

	// Insure Response is empty
	if (Response != NULL)
	{
	    Response[0] = '\0';
	}

	// Find the class to invoke via Instno or ResourceName etc.
	for (i = 0; i < s_NumDev; i++)
	{
	    if (strcmp(ResourceName, s_DevInfo[i].ResourceName) == 0)
	    {
		    DriverClass = (void *)(s_DevInfo[i].DriverClass);
		    break;
	    }
	}

	// Any Errors ?
	if (i >= s_NumDev)
	{
	    itoa(Instno, cInstNo, 10);
	    atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "IssueNativeCmds ",	ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND, ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);

		return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
	}

	if (strncmp(ResourceName, "CAN_", 4) == 0)	//CJW
	{
	    Status = ((CCan_TDR010_T *)(DriverClass))->IssueNativeCmdsCan_TDR010(InstrumentCmds, Response, BufferSize);
	}
	//else if (strncmp(ResourceName, "CAN2", 4) == 0)
	//{
	//    Status = ((CCan2_TDR010_T *)(DriverClass))->IssueNativeCmdsCan_TDR010(InstrumentCmds,
	//	  Response, BufferSize);
	//}

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

	// Insure Response is empty
	if (Response != NULL)
	{
		Response[0] = '\0';
	}

	// Find the class to invoke via Instno or ResourceName etc.
	for (i = 0; i < s_NumDev; i++)
	{
	    if (strcmp(ResourceName, s_DevInfo[i].ResourceName) == 0)
	    {
		    DriverClass = (void *)(s_DevInfo[i].DriverClass);
		    break;
	    }
	}

	// Any Errors ?
	if (i >= s_NumDev)
	{
	    itoa(Instno, cInstNo, 10);
	    atxmlw_ErrorResponse(cInstNo, Response, BufferSize, "IssueDriverFunctionCall ",
		ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND,
		ATXMLW_WRAPPER_ERRMSG_DEVICE_NOT_FOUND);
	 
		return(ATXMLW_WRAPPER_ERRCD_DEVICE_NOT_FOUND);
	}

	if (strncmp(ResourceName, "CAN_", 4) == 0)	//CJW
	{
	    ((CCan_TDR010_T *)(DriverClass))->IssueDriverFunctionCallCan_TDR010(DriverFunction,
		Response, BufferSize);
	}

	return(0);
 }

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


