//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlWrapperInf.cpp
//
// Date:	    11OCT05
//
// Purpose:	    Wrapper Driver interface Class
//
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     AtmlDeviceXxx.dll   Device driver/wrapper.dll
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline                       
///////////////////////////////////////////////////////////////////////////////
// Includes
#include "CiCoreCommon.h"
#include "WrapperInf.h"

// Local Defines

// Function codes

//////// Place WrapperInf specific data here //////////////
//////////////////////////////////////////////////////////

// Static Variables

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////
// Function: CWrapperInf
//
// Purpose: Initialize the instrument driver
//
///////////////////////////////////////////////////////////////////////////////
CWrapperInf::CWrapperInf(char *WrapperName)
{
    m_Module = NULL;
    strnzcpy(m_WrapperName,WrapperName,ATXML_MAX_NAME);
    return;
}


///////////////////////////////////////////////////////////////////////////////
// Function: ~CWrapperInf()
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
CWrapperInf::~CWrapperInf()
{
    CClose();
    FreeLibrary(m_Module);
    m_Module = NULL;
    return;
}

///////////////////////////////////////////////////////////////////////////////
// Function: CInitialize
//
// Purpose: Load the wrapper and initialize the instrument
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
int CWrapperInf::CInitialize(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               INSTRUMENT_ADDRESS *AddressInfoPtr,
                               char* Response, int Buffersize)
{
    int Status = 0;

    m_Instno = Instno;
    // Load the wrapper Dll if not loaded
    if(m_Module == NULL)
    {
        m_Module = LoadLibrary(m_WrapperName);
        if(m_Module == NULL)
        {
            atxml_ErrorResponse(Response, Buffersize, 
                            "AtXmlWrapInit", ATXML_ERRCD_INIT_WRAPPER,
                 atxml_FmtMsg("%s - %s %s",ATXML_ERRMSG_INIT_WRAPPER,ResourceName,m_WrapperName));
            return(ATXML_ERRCD_INIT_WRAPPER);
        }
        // Save Dll pointers in calss Info
        m_Initialize = (INITIALIZE_PROC)GetProcAddress(m_Module,"Initialize");
        m_Close = (CLOSE_PROC)GetProcAddress(m_Module,"Close");
        m_RegisterTSF = (REGISTERTSF_PROC)GetProcAddress(m_Module,"RegisterTSF");
        m_IssueSignal = (ISSUESIGNAL_PROC)GetProcAddress(m_Module,"IssueSignal");
        m_Status = (STATUS_PROC)GetProcAddress(m_Module,"Status");
        m_RegCal = (REGCAL_PROC)GetProcAddress(m_Module,"RegCal");
        m_Reset = (RESET_PROC)GetProcAddress(m_Module,"Reset");
        m_Ist = (IST_PROC)GetProcAddress(m_Module,"Ist");
        m_IssueNativeCmds = (ISSUENATIVECMDS_PROC)GetProcAddress(m_Module,"IssueNativeCmds");
        m_IssueDriverFunctionCall = (ISSUEDRIVERFNC_PROC)GetProcAddress(m_Module,"IssueDriverFunctionCall");
        if((m_Initialize == NULL) ||
           (m_Close == NULL) ||
           (m_RegisterTSF == NULL) ||
           (m_IssueSignal == NULL) ||
           (m_Status == NULL) ||
           (m_RegCal == NULL) ||
           (m_Reset == NULL) ||
           (m_Ist == NULL) ||
           (m_IssueNativeCmds == NULL) ||
           (m_IssueDriverFunctionCall == NULL)
            )
        {
            atxml_ErrorResponse(Response, Buffersize, 
                            "AtXmlWrapInit Functions", ATXML_ERRCD_INIT_WRAPPER,
                 atxml_FmtMsg("%s - %s",ATXML_ERRMSG_INIT_WRAPPER,ResourceName));
            return(ATXML_ERRCD_INIT_WRAPPER);
        }
    }

    // Initialize the device
    Status = (m_Initialize)(Instno, ResourceType, ResourceName, Sim, Dbglvl,
                         AddressInfoPtr,
                         Response, Buffersize);
/*
    FARPROC   m_Close;
    FARPROC   m_RegisterTSF;
    FARPROC   m_IssueSignal;
    FARPROC   m_Status;
    FARPROC   m_RegCal;
    FARPROC   m_Reset;
    FARPROC   m_Ist;
    FARPROC   m_IssueNativeCmds;
    FARPROC   m_IssueDriverFunctionCall;
LoadLibrary(WrapperName);
HMODULE = LoadLibraryEx(WrapperName, NULL,NULL);
FreeLibrary(HMODULE);
FARPROC GetProcAddress(HMODULE hModule,LPCSTR lpProcName );



*/
    return(Status);
}
///////////////////////////////////////////////////////////////////////////////
// Function: Pass Through Calls
//
// Purpose: The following are pass through calls
//
// Return:
//    0 on success.
//   -ErrorCode on failure
///////////////////////////////////////////////////////////////////////////////
int CWrapperInf::CClose(void)
{
    int Status = 0;

    if((m_Module != NULL) && (m_Close != NULL))
        Status = m_Close();
    return(Status);
}
int CWrapperInf::CRegisterTSF(ATML_XML_FILENAME* TSFSignalDefinition, 
                              ATML_XML_FILENAME* TSFLibrary,
                              ATML_XML_FILENAME* STDTSF,
                              ATML_XML_FILENAME* STDBSC)
{
    int Status = 0;

    if((m_Module != NULL) && (m_RegisterTSF != NULL))
        Status = m_RegisterTSF(TSFSignalDefinition, 
                              TSFLibrary,
                              STDTSF,
                              STDBSC);
    return(Status);
}
int CWrapperInf::CIssueSignal(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_SIGDESC* SignalDescription,
                ATML_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    if((m_Module != NULL) && (m_IssueSignal != NULL))
        Status = m_IssueSignal(Instno, ResourceType, ResourceName,
                        SignalDescription,
                        Response, BufferSize);
    return(Status);
}
int CWrapperInf::CStatus(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    if((m_Module != NULL) && (m_Status != NULL))
        Status = m_Status(Instno, ResourceType, ResourceName,
                        Response, BufferSize);
    return(Status);
}
int CWrapperInf::CRegCal(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_CALDATA* CalData)
{
    int Status = 0;

    if((m_Module != NULL) && (m_RegCal != NULL))
        Status = m_RegCal(Instno, ResourceType, ResourceName,
                        CalData);
    return(Status);
}
int CWrapperInf::CReset(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    if((m_Module != NULL) && (m_Reset != NULL))
        Status = m_Reset(Instno, ResourceType, ResourceName,
                        Response, BufferSize);
    return(Status);
}
int CWrapperInf::CIst(int Instno, int ResourceType, char* ResourceName,
                              int Level, ATML_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    if((m_Module != NULL) && (m_Ist != NULL))
        Status = m_Ist(Instno, ResourceType, ResourceName,
                        Level, Response, BufferSize);
    return(Status);
}
int CWrapperInf::CIssueNativeCmds(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_INSTCMD* InstrumentCmds,
                              ATML_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    if((m_Module != NULL) && (m_IssueNativeCmds != NULL))
        Status = m_IssueNativeCmds(Instno, ResourceType, ResourceName,
                              InstrumentCmds,
                              Response, BufferSize);
    return(Status);
}
int CWrapperInf::CIssueDriverFunctionCall(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_DRVRFNC* DriverFunction,
                              ATML_INTF_RESPONSE* Response, int BufferSize)
{
    int Status = 0;

    if((m_Module != NULL) && (m_IssueDriverFunctionCall != NULL))
        Status = m_IssueDriverFunctionCall(Instno, ResourceType, ResourceName,
                        DriverFunction,
                        Response, BufferSize);
    return(Status);
}

//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


