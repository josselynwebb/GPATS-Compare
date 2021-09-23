//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    WrapperInf.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for WrapperInf
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline                       
///////////////////////////////////////////////////////////////////////////////
#if     _MSC_VER > 1000
#pragma once
#endif


#ifndef WRAPPER_INF_H
#define WRAPPER_INF_H

#pragma warning(disable:4786)
#pragma warning(disable:4996)

// External wrapper data typedefs
typedef char ATML_INTF_RESPONSE;
typedef char ATML_XML_FILENAME;
typedef char ATML_INTF_CALDATA;
typedef char ATML_INTF_SIGDESC;
typedef char ATML_INTF_INSTCMD;
typedef char ATML_INTF_DRVRFNC;
typedef char ATML_INSTCMD_RESPONSE_TYPE;
#define ATXMLW_RESTYPE_BASE      1
#define ATXMLW_RESTYPE_PHYSICAL  2
#define ATXMLW_RESTYPE_VIRTUAL   3
// Data Structures
typedef int (*INITIALIZE_PROC)(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATML_INTF_RESPONSE *Response, int Buffersize); 
typedef int (*CLOSE_PROC)(void);
typedef int (*REGISTERTSF_PROC)(ATML_XML_FILENAME* TSFSignalDefinition, 
                               ATML_XML_FILENAME* TSFLibrary,
                               ATML_XML_FILENAME* STDTSF,
                               ATML_XML_FILENAME* STDBSC);
typedef int (*ISSUESIGNAL_PROC)(int Instno, int ResourceType, char* ResourceName,
                               ATML_INTF_SIGDESC* SignalDescription,
                               ATML_INTF_RESPONSE* Response, int BufferSize);
typedef int (*STATUS_PROC)(int Instno, int ResourceType, char* ResourceName,
                               ATML_INTF_RESPONSE* Response, int BufferSize);
typedef int (*REGCAL_PROC)(int Instno, int ResourceType, char* ResourceName,
                               ATML_INTF_CALDATA* CalData);
typedef int (*RESET_PROC)(int Instno, int ResourceType, char* ResourceName,
                               ATML_INTF_RESPONSE* Response, int BufferSize);
typedef int (*IST_PROC)(int Instno, int ResourceType, char* ResourceName,
                               int Level, ATML_INTF_RESPONSE* Response, int BufferSize);
typedef int (*ISSUENATIVECMDS_PROC)(int Instno, int ResourceType, char* ResourceName,
                               ATML_INTF_INSTCMD* InstrumentCmds,
                                  ATML_INTF_RESPONSE* Response, int BufferSize);
typedef int (*ISSUEDRIVERFNC_PROC)(int Instno, int ResourceType, char* ResourceName,
                               ATML_INTF_DRVRFNC* DriverFunction,
                               ATML_INTF_RESPONSE* Response, int BufferSize);

class CWrapperInf
{
private:
    HMODULE               m_Module;
    char                  m_WrapperName[ATXML_MAX_NAME];
    int                   m_Instno;
    INITIALIZE_PROC       m_Initialize;
    CLOSE_PROC            m_Close;
    REGISTERTSF_PROC      m_RegisterTSF;
    ISSUESIGNAL_PROC      m_IssueSignal;
    STATUS_PROC           m_Status;
    REGCAL_PROC           m_RegCal;
    RESET_PROC            m_Reset;
    IST_PROC              m_Ist;
    ISSUENATIVECMDS_PROC  m_IssueNativeCmds;
    ISSUEDRIVERFNC_PROC   m_IssueDriverFunctionCall;

public:
     CWrapperInf(char *WrapperName);
    ~CWrapperInf(void);
public:
    int CInitialize(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATML_INTF_RESPONSE *Response, int Buffersize);
    int CClose(void);
    int CRegisterTSF(ATML_XML_FILENAME* TSFSignalDefinition, 
                              ATML_XML_FILENAME* TSFLibrary,
                              ATML_XML_FILENAME* STDTSF,
                              ATML_XML_FILENAME* STDBSC);
    int CIssueSignal(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_SIGDESC* SignalDescription,
                ATML_INTF_RESPONSE* Response, int BufferSize);
    int CStatus(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_RESPONSE* Response, int BufferSize);
    int CRegCal(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_CALDATA* CalData);
    int CReset(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_RESPONSE* Response, int BufferSize);
    int CIst(int Instno, int ResourceType, char* ResourceName,
                              int Level, ATML_INTF_RESPONSE* Response, int BufferSize);
    int CIssueNativeCmds(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_INSTCMD* InstrumentCmds,
                                  ATML_INTF_RESPONSE* Response, int BufferSize);
    int CIssueDriverFunctionCall(int Instno, int ResourceType, char* ResourceName,
                              ATML_INTF_DRVRFNC* DriverFunction,
                              ATML_INTF_RESPONSE* Response, int BufferSize);

private:
};

#endif // WRAPPER_INF_H