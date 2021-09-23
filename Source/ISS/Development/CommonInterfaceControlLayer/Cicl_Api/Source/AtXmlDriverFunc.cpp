//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    AtXmlDriverFunc.cpp
//
// Date:	    11OCT05
//
// Purpose:	    ATXML Interface API for DriverFunctionCalls Primarily for
//                    VB
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//
//
//
// Revision History
// Rev	     Date                  Reason
// =======  =======  ======================================= 
// 1.0.0.0  11OCT05  Baseline Release                        
///////////////////////////////////////////////////////////////////////////////
// Includes
#include <process.h>
#include <stdio.h>
#include "AtXmlInterfaceApiC.h"
#include "AtXmlDriverFunc.h"

/*- Resource Template Operations --------------------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viSetAttribute  (ViObject vi, ViAttr attrName, ViAttrState attrValue);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viSetAttribute(char *ResourceName,
                   int vi,
                   unsigned long Attrib,
                   unsigned long AttribValue)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viSetAttribute",
		     "RetInt32,Handle,SrcUInt32,SrcUInt32",
			 &RetVal,  vi,    Attrib,   AttribValue);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGetAttribute  (ViObject vi, ViAttr attrName, void _VI_PTR attrValue /**=**/RetUInt32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGetAttribute (char *ResourceName,
                    unsigned long vi,
                    unsigned long attrName,
                    unsigned long *attrValue)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viGetAttribute",
		     "RetInt32,Handle,SrcUInt32,RetUInt32",
			 &RetVal,  vi,    attrName, attrValue);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viStatusDesc    (ViObject vi, ViStatus status, ViChar desc[]);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viStatusDesc (char *ResourceName,
                  unsigned long vi,
                  long status,
                  char *desc)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viStatusDesc",
		     "RetInt32,Handle,SrcInt32,RetStrPtr",
			 &RetVal,  vi,    status,  desc);

	return(RetVal);
}

//*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viTerminate     (ViObject vi, ViUInt16 degree, ViJobId jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viTerminate(char *ResourceName,
                   int vi,
                   unsigned short degree,
                   unsigned long jobId)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viTerminate",
		     "RetInt32,Handle,SrcUInt16,SrcUInt32",
			 &RetVal,  vi,    degree,   jobId);

	return(RetVal);
}

//*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viLock          (ViSession vi, ViAccessMode lockType, ViUInt32 timeout,
//                                    ViKeyId requestedKey, ViChar accessKey[]);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viLock (char *ResourceName,
            unsigned long vi,
            unsigned long lockType,      // couldn't find ViAccessMode
            unsigned long timeout,
            unsigned long requestedKey,  
            char *accessKey)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viLock",
		     "RetInt32,Handle,SrcUInt32,SrcUInt32,SrcStrPtr,RetStrPtr",
			 &RetVal,  vi,    lockType, timeout,  requestedKey,accessKey);

	return(RetVal);
}


/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUnlock        (ViSession vi);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viUnlock (char *ResourceName,
              int vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viUnlock",
		     "RetInt32,Handle",
			 &RetVal,  vi);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viEnableEvent   (ViSession vi, ViEventType eventType, ViUInt16 mechanism,
//                                    ViEventFilter context);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viEnableEvent (char *ResourceName,
                   int vi,
                   unsigned long eventType,
                   unsigned short mechanism,
                   unsigned long context)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viEnableEvent",
		     "RetInt32,Handle,SrcUInt32,SrcUInt16,SrcUInt32",
			 &RetVal,  vi,    eventType,mechanism,context);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viDisableEvent  (ViSession vi, ViEventType eventType, ViUInt16 mechanism);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viDisableEvent (char *ResourceName,
                    int vi,
                    unsigned long eventType,
                    unsigned short mechanism)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viDisableEvent",
		     "RetInt32,Handle,SrcUInt32,SrcUInt16",
			 &RetVal,  vi,    eventType,mechanism);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viDiscardEvents (ViSession vi, ViEventType eventType, ViUInt16 mechanism);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viDiscardEvents (char *ResourceName,
                     int vi,
                     unsigned long eventType,
                     unsigned short mechanism)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viDiscardEvents",
		     "RetInt32,Handle,SrcUInt32,SrcUInt16",
			 &RetVal,  vi,    eventType,mechanism);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWaitOnEvent   (ViSession vi, ViEventType inEventType, ViUInt32 timeout,
//                                    ViPEventType outEventType, ViPEvent outContext);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWaitOnEvent (char *ResourceName,
                   int vi,
                   unsigned long eventType,
                   unsigned long timeout,
                   unsigned long *outEventType,
                   unsigned long *outContext)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viWaitOnEvent",
		     "RetInt32,Handle,SrcUInt32,SrcUInt32,RetUInt32,   RetUInt32",
			 &RetVal,  vi,    eventType,timeout,  outEventType,outContext);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viInstallHandler(ViSession vi, ViEventType eventType, ViHndlr handler,
//                                    ViAddr userHandle);
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUninstallHandler(ViSession vi, ViEventType eventType, ViHndlr handler,
//                                      ViAddr userHandle);
// Not handled by AtXml functionality


/*- Basic I/O Operations ----------------------------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viRead          (ViSession vi, ViPBuf buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viRead (char *ResourceName,
            int vi,
            unsigned char *buf,
            unsigned long cnt,
            unsigned long *retCnt)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,RetUInt8Ptr %d,SrcUInt32,RetInt32",
             cnt);

	Status = atxml_CallDriverFunction (ResourceName,
			 "viRead",
		     TmpStr,
			 &RetVal,  vi,    buf,           cnt,      retCnt);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viReadAsync     (ViSession vi, ViPBuf buf, ViUInt32 cnt, ViPJobId  jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viReadAsync (char *ResourceName,
            int vi,
            unsigned char *buf,
            unsigned long cnt,
            unsigned long *jobId)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,RetUInt8Ptr %d,SrcUInt32,RetInt32",
             cnt);

	Status = atxml_CallDriverFunction (ResourceName,
			 "viReadAsync",
		     TmpStr,
			 &RetVal,  vi,    buf,           cnt,      jobId);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viReadToFile    (ViSession vi, ViConstString filename, ViUInt32 cnt,
//                                    ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viReadToFile (char *ResourceName,
                  int vi,
                  unsigned const char *filename,
                  unsigned long cnt,
                  unsigned long *retCnt)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viReadToFile",
             "RetInt32,Handle,SrcStrPtr,SrcUInt32,RetUInt32",
			 &RetVal,  vi,    filename, cnt,      retCnt);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWrite         (ViSession vi, ViBuf  buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWrite (char *ResourceName,
             int vi,
             unsigned char *buf,
             unsigned long cnt,
             int *retCnt)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt8Ptr %d,SrcUInt32,RetInt32",
             cnt);

	Status = atxml_CallDriverFunction (ResourceName,
			 "viWrite",
		     TmpStr,
			 &RetVal,  vi,    buf,           cnt,      retCnt);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWriteAsync    (ViSession vi, ViBuf  buf, ViUInt32 cnt, ViPJobId  jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWriteAsync (char *ResourceName,
                  int vi,
                  unsigned char *buf,
                  unsigned long cnt,
                  unsigned long *jobId)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt8Ptr %d,SrcUInt32,RetInt32",
             cnt);

	Status = atxml_CallDriverFunction (ResourceName,
			 "viWriteAsync",
             TmpStr,
			 &RetVal,  vi,    buf,        cnt,      jobId);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWriteFromFile (ViSession vi, ViConstString filename, ViUInt32 cnt,
//                                    ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWriteFromFile (char *ResourceName,
                     int vi,
                     unsigned const char *filename,
                     unsigned long cnt,
                     unsigned long *retCnt)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viWriteFromFile",
		     "RetInt32,Handle,SrcStrPtr,SrcUInt32,RetUInt32",
			 &RetVal,  vi,    filename, cnt,      retCnt);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viAssertTrigger (ViSession vi, ViUInt16 protocol);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viAssertTrigger (char *ResourceName,
                     int vi,
                     unsigned short protocol)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viAssertTrigger",
               "RetInt32,Handle,SrcUInt16",
                &RetVal, vi,    protocol);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viReadSTB       (ViSession vi, ViPUInt16 status);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viReadSTB (char *ResourceName,
               int vi,
               unsigned short *status)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viReadSTB",
               "RetInt32,Handle,RetUInt16",
                &RetVal, vi,    status);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viClear         (ViSession vi);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viClear (char *ResourceName,
             int vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viClear",
		     "RetInt32,Handle",
			 &RetVal,  vi);

	return(RetVal);
}


/*- Formatted and Buffered I/O Operations -----------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viSetBuf        (ViSession vi, ViUInt16 mask, ViUInt32 size);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viSetBuf (char *ResourceName,
              int vi,
              unsigned short mask,
              unsigned long size)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viSetBuf",
               "RetInt32,Handle,SrcUInt16,SrcUInt32",
                &RetVal, vi,    mask,     size);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viFlush         (ViSession vi, ViUInt16 mask);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viFlush (char *ResourceName,
             int vi,
             unsigned short mask)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viFlush",
               "RetInt32,Handle,SrcUInt16",
                &RetVal, vi,    mask);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viBufWrite      (ViSession vi, ViBuf  buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viBufWrite (char *ResourceName,
                int vi,
                unsigned char *buf,
                unsigned long cnt,
                unsigned long *retCnt)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt8Ptr %d,SrcUInt32,RetInt32",
             cnt);

	Status = atxml_CallDriverFunction (ResourceName,
               "viBufWrite",
               TmpStr,
                &RetVal, vi,    buf,      cnt,      retCnt);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viBufRead       (ViSession vi, ViPBuf buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viBufRead (char *ResourceName,
               int vi,
               unsigned char *buf, 
               unsigned long cnt,
               unsigned long *retCnt)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,RetUInt8Ptr %d,SrcUInt32,RetInt32",
             cnt);

	Status = atxml_CallDriverFunction (ResourceName,
               "viBufRead",
               TmpStr,
                &RetVal, vi,    buf,      cnt,      retCnt);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNCC viPrintf        (ViSession vi, ViString writeFmt, ...);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVPrintf       (ViSession vi, ViString writeFmt, ViVAList params);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNCC viSPrintf       (ViSession vi, ViPBuf buf, ViString writeFmt, ...);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVSPrintf      (ViSession vi, ViPBuf buf, ViString writeFmt,
//                                    ViVAList parms);
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNCC viScanf         (ViSession vi, ViString readFmt, ...);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVScanf        (ViSession vi, ViString readFmt, ViVAList params);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNCC viSScanf        (ViSession vi, ViBuf buf, ViString readFmt, ...);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVSScanf       (ViSession vi, ViBuf buf, ViString readFmt,
//                                    ViVAList parms);
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNCC viQueryf        (ViSession vi, ViString writeFmt, ViString readFmt, ...);
//
// Not handled by AtXml functionality

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVQueryf       (ViSession vi, ViString writeFmt, ViString readFmt,
//                                    ViVAList params);
// Not handled by AtXml functionality


/*- Memory I/O Operations ---------------------------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viIn8           (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViPUInt8  val8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viIn8 (char *ResourceName,
           int vi,
           unsigned short space,
           unsigned long offset,
           unsigned char *val8)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viIn8",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,RetUInt8",
                &RetVal, vi,    space,    offset,   val8);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOut8          (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViUInt8   val8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viOut8 (char *ResourceName,
            int vi,
            unsigned short space,
            unsigned long offset,
            unsigned char val8)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viOut8",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt8",
                &RetVal, vi,    space,    offset,   val8);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viIn16          (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViPUInt16 val16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viIn16 (char *ResourceName,
            int vi,
            unsigned short space,
            unsigned long offset,
            unsigned short *val16)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viIn16",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,RetUInt16",
                &RetVal, vi,    space,    offset,   val16);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOut16         (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViUInt16  val16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viOut16 (char *ResourceName,
             int vi,
             unsigned short space,
             unsigned long offset,
             unsigned short val16)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viOut16",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16",
                &RetVal, vi,    space,    offset,   val16);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viIn32          (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViPUInt32 val32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viIn32 (char *ResourceName,
            int vi,
            unsigned short space,
            unsigned long offset,
            unsigned long *val32)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viIn32",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,RetUInt32",
                &RetVal, vi,    space,    offset,   val32);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOut32         (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViUInt32  val32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viOut32 (char *ResourceName,
             int vi,
             unsigned short space,
             unsigned long offset,
             unsigned long val32)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viOut32",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt32",
                &RetVal, vi,    space,    offset,   val32);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveIn8       (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt8  buf8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveIn8 (char *ResourceName,
               int vi,
               unsigned short space,
               unsigned long offset,
               unsigned short length,
               unsigned char *buf8)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,RetUInt8Ptr %d",
             length);

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveIn8",
                TmpStr,
                &RetVal, vi, space, offset, length, buf8);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveOut8      (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt8  buf8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveOut8 (char *ResourceName,
               int vi,
               unsigned short space,
               unsigned long offset,
               unsigned short length,
               unsigned char *buf8)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,RetUInt8Ptr %d",
             length);

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveOut8",
                TmpStr,
                &RetVal, vi, space, offset, length, buf8);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveIn16      (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt16 buf16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveIn16 (char *ResourceName,
                int vi,
                unsigned short space,
                unsigned long offset,
                unsigned short length,
                unsigned short *buf16)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,RetUInt16Ptr %d",
             length);

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveIn16",
                TmpStr,
                &RetVal, vi, space, offset, length, buf16);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveOut16     (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt16 buf16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveOut16 (char *ResourceName,
                 int vi,
                 unsigned short space,
                 unsigned long offset,
                 unsigned short length,
                 unsigned short *buf16)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,RetUInt16Ptr %d",
             length);

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveOut16",
                TmpStr,
                &RetVal, vi, space, offset, length, buf16);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveIn32      (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt32 buf32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveIn32 (char *ResourceName,
                int vi,
                unsigned short space,
                unsigned long offset,
                unsigned short length,
                unsigned long *buf32)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,RetUInt32Ptr %d",
             length);

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveIn32",
                TmpStr,
                &RetVal, vi,    space,    offset,   length,   buf32);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveOut32     (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt32 buf32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveOut32 (char *ResourceName,
                 int vi,
                 unsigned short space,
                 unsigned long offset,
                 unsigned short length,
                 unsigned long *buf32)
{
	int RetVal;
	int Status;
	char TmpStr[64];

	sprintf (TmpStr,
             "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,RetUInt32Ptr %d",
             length);

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveOut32",
                TmpStr,
                &RetVal, vi, space, offset, length, buf32);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMove          (ViSession vi, ViUInt16 srcSpace, ViBusAddress srcOffset,
//                                    ViUInt16 srcWidth, ViUInt16 destSpace,
//                                    ViBusAddress destOffset, ViUInt16 destWidth,
//                                    ViBusSize srcLength);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMove (char *ResourceName,
            int vi,
            unsigned short srcSpace,
            unsigned long srcOffset,
            unsigned short srcWidth,
            unsigned short destSpace,
            unsigned long destOffset,
            unsigned short destWidth,
            unsigned short srcLength)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viMove",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,SrcUInt16,SrcUInt32, SrcUInt16,SrcUInt16",
                &RetVal, vi,    srcSpace, srcOffset,srcWidth, destSpace,destOffset,destWidth,srcLength);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveAsync     (ViSession vi, ViUInt16 srcSpace, ViBusAddress srcOffset,
//                                    ViUInt16 srcWidth, ViUInt16 destSpace,
//                                    ViBusAddress destOffset, ViUInt16 destWidth,
//                                    ViBusSize srcLength, ViPJobId jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveAsync (char *ResourceName,
                 int vi,
                 unsigned short srcSpace,
                 unsigned long srcOffset,
                 unsigned short srcWidth,
                 unsigned short destSpace,
                 unsigned long destOffset,
                 unsigned short destWidth,
                 unsigned short srcLength,
                 unsigned long *jobId)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viMoveAsync",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt16,SrcUInt16,SrcUInt32, SrcUInt16,SrcUInt32,RetUInt32",
                &RetVal, vi,    srcSpace, srcOffset,srcWidth, destSpace,destOffset,destWidth,srcLength,jobId);

	return (RetVal);
}

/*- Interface Specific Operations -------------------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibControlREN(ViSession vi, ViUInt16 mode);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibControlREN (char *ResourceName,
                      int vi,
                      unsigned short mode)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viGpibControlREN",
               "RetInt32,Handle,SrcUInt16",
                &RetVal, vi,    mode);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibControlATN(ViSession vi, ViUInt16 mode);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibControlATN (char *ResourceName,
                      int vi,
                      unsigned short mode)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viGpibControlATN",
               "RetInt32,Handle,SrcUInt16",
                &RetVal, vi,    mode);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibSendIFC   (ViSession vi);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibSendIFC (char *ResourceName,
                int vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viGpibSendIFC",
               "RetInt32,Handle",
                &RetVal, vi);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibCommand   (ViSession vi, ViBuf cmd, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibCommand (char *ResourceName,
                   int vi,
                   unsigned char *cmd,
                   unsigned long cnt,
                   unsigned long *retCnt)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viGpibCommand",
               "RetInt32,Handle,SrcStrPtr,SrcUInt32,RetUInt32",
                &RetVal, vi,    cmd,      cnt,      retCnt);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibPassControl(ViSession vi, ViUInt16 primAddr, ViUInt16 secAddr);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibPassControl (char *ResourceName,
                       int vi,
                       unsigned short primAddr,
                       unsigned short secAddr)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viGpibPassControl",
               "RetInt32,Handle,SrcUInt16,SrcUInt16",
                &RetVal, vi,    primAddr, secAddr);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVxiCommandQuery(ViSession vi, ViUInt16 mode, ViUInt32 cmd,
//                                     ViPUInt32 response);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viVxiCommandQuery (char *ResourceName,
                       int vi,
                       unsigned short mode,
                       unsigned long cmd,
                       unsigned long *response)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viVxiCommandQuery",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,RetUInt32",
                &RetVal, vi,    mode,     cmd,      response);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viAssertUtilSignal(ViSession vi, ViUInt16 line);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viAssertUtilSignal (char *ResourceName,
                        int vi,
                        unsigned short line)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viAssertUtilSignal",
               "RetInt32,Handle,SrcUInt16",
                &RetVal, vi,    line);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viAssertIntrSignal(ViSession vi, ViInt16 mode, ViUInt32 statusID);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viAssertIntrSignal (char *ResourceName,
                        int vi,
                        short mode,
                        unsigned long statusID)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viAssertIntrSignal",
               "RetInt32,Handle,SrcInt16,SrcUInt32",
                &RetVal, vi,    mode,    statusID);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMapTrigger    (ViSession vi, ViInt16 trigSrc, ViInt16 trigDest,
//                                    ViUInt16 mode);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMapTrigger (char *ResourceName,
                  int vi,
                  short trigSrc,
                  short trigDest,
                  unsigned short mode)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viMapTrigger",
               "RetInt32,Handle,SrcInt16,SrcInt16,SrcUInt16",
                &RetVal, vi,    trigSrc, trigDest,mode);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUnmapTrigger  (ViSession vi, ViInt16 trigSrc, ViInt16 trigDest);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viUnmapTrigger (char *ResourceName,
                    int vi,
                    short trigSrc,
                    short trigDest)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viUnmapTrigger",
               "RetInt32,Handle,SrcInt16,SrcInt16",
                &RetVal, vi,    trigSrc, trigDest);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//int zt1428_read_waveform (ViSession instrumentHandle, int source,
//                          int transferType, double waveformArray[],
//                          int *number_ofPoints, int *acquisitionCount,
//                          double *sampleInterval, double *timeOffset,
//                          int *xReference, double *voltIncrement,
//                          double *voltOffset, int *yReference);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_zt1428_read_waveform (
                     char *ResourceName,
                     int vi,
                     int source,
                     int transferType,
                     double waveformArray[],
                     int *number_ofPoints,
                     int *acquisitionCount,
                     double *sampleInterval,
                     double *timeOffset,
                     int *xReference,
                     double *voltIncrement,
                     double *voltOffset,
                     int *yReference)
{
	int RetVal;
	int Status;
    char format[512];

    sprintf(format, "RetInt32,Handle,SrcInt32,SrcInt32,RetDblPtr %d,RetUInt32,RetUInt32,"
                    "RetDbl,RetDbl,RetUInt32,RetDbl,RetDbl,RetUInt32", *number_ofPoints);

	Status = atxml_CallDriverFunction (ResourceName,
               "zt1428_read_waveform",
               format,
               &RetVal,vi,source,transferType,waveformArray,number_ofPoints,acquisitionCount,
               sampleInterval,timeOffset,xReference,voltIncrement,voltOffset,yReference);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//int atxmlDF_hpe1428a_WvData (
//                     char *ResourceName,
//					 int vi,
//                   float YIncrement, 
//					 float YOffset, 
//					 float XIncrement, 
//					 float XOffset, 
//					 unsigned short WhichArea, 
//					 unsigned short TotChan, 
//					 unsigned short AquType);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_hpe1428a_WvData (
                     char *ResourceName,
					 int vi,
                     float YIncrement, 
					 float YOffset, 
					 float XIncrement, 
					 float XOffset, 
					 unsigned short WhichArea, 
					 unsigned short TotChan, 
					 unsigned short AquType)
{
	int RetVal;
	int Status;
    char format[512];

    sprintf(format, "RetInt32,Handle,SrcFlt,SrcFlt,SrcFlt,SrcFlt,SrcUInt16,SrcUInt16,SrcUInt16");

	Status = atxml_CallDriverFunction (ResourceName,
               "hpe1428a_WvData",
               format,
               &RetVal,vi,YIncrement,YOffset,XIncrement,XOffset,WhichArea,TotChan,AquType);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//int atxmlDF_hpe1428a_GtWv (
//                     char *ResourceName,
//					 int CALL_TYPE atxmlDF_hpe1428a_GtWv (
//                     char *ResourceName,
///                    float WaveArray[], 
//					 float TimeArray[]);

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_hpe1428a_GtWv (
                     char *ResourceName,
					 int number_ofPoints,
					 float WaveArray[], 
					 float TimeArray[])
{
	int RetVal;
	int Status;
    char format[512];

    sprintf(format, "RetInt32,SrcInt32,RetFltPtr %d,RetFltPtr %d",number_ofPoints,number_ofPoints);

	Status = atxml_CallDriverFunction (ResourceName,
               "hpe1428a_GtWv",
               format,
               &RetVal,number_ofPoints,WaveArray,TimeArray);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//int atxmlDF_hpe1428a_FindInst (
				//int vi,
//                     unsigned int MemOffset1,
//					 unsigned int MemOffset2,
//					 unsigned int MathOffset,
//					 unsigned short MemoryPoints,
//					 unsigned int *VMEAddress

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_hpe1428a_FindInst (
                     char *ResourceName,
					 int vi,
                     unsigned int MemOffset1,
					 unsigned int MemOffset2,
					 unsigned int MathOffset,
					 unsigned short MemoryPoints,
					 unsigned int *VMEAddress)
{
	int RetVal;
	int Status;
    char format[512];

    sprintf(format, "RetInt32,SrcInt32,SrcUInt32,SrcUInt32,SrcUInt32,SrcUInt16,RetUInt32Ptr");

	Status = atxml_CallDriverFunction (ResourceName,
               "hpe1428a_FindInst",
               format,
               &RetVal,vi,MemOffset1,MemOffset2,MathOffset,MemoryPoints,VMEAddress);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//selectCorrectionsTable (ViString FilePath, ViString TableName)
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_eip_selectCorrectionsTable(
							char *ResourceName,
							const char *FilePath, 
							const char *TableName)
{
	int RetVal;
	int Status;
	char format[512];

	sprintf(format, "RetInt32,SrcStrPtr,SrcStrPtr");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "eip_selectCorrectionsTable",
               format,
               &RetVal, FilePath, TableName);

	return (RetVal);
}
/*---------------------------------------------------------------------------*/
//setCorrectedPower(ViSession instrSession, double Frequency, float Power)
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_eip_setCorrectedPower(
							char *ResourceName,
							int instrSession, 
							double Frequency, 
							float Power)
{
	int RetVal;
	int Status;
	char format[512];

	sprintf(format, "RetInt32,Handle,SrcDbl,SrcFlt");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "eip_setCorrectedPower",
               format,
               &RetVal, instrSession, Frequency, Power);

	return (RetVal);
}

/**** TETS RF CNTR *****/
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_measfreq (
                     char *ResourceName,
					 double expectedfreq,
					 int attn,
					 double *measfreq
					 )
{
	int RetVal;
	int Status;
	char format[512];

	sprintf(format, "RetInt32,SrcDbl,SrcInt32,RetDbl");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "rf_measfreq",
               format,
               &RetVal, expectedfreq, attn, measfreq);

	return (RetVal);
}
					
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_measpw (
                     char *ResourceName,
					 double carrierfreq,
					 double expectedpw,
					 int attn,
					 double *measpw
					 )
{
	int RetVal;
	int Status;
	char format[512];

	sprintf(format, "RetInt32,SrcDbl,SrcDbl,SrcInt32,RetDbl");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "rf_measpw",
               format,
               &RetVal, carrierfreq, expectedpw, attn, measpw);

	return (RetVal);
}

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_measpd (
                     char *ResourceName,
					 double carrierfreq,
					 double expectedpd,
					 int attn,
					 double *measpd
					 )
{
	int RetVal;
	int Status;
	char format[512];
	
	sprintf(format, "RetInt32,SrcDbl,SrcDbl,SrcInt32,RetDbl");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "rf_measpd",
               format,
               &RetVal, carrierfreq, expectedpd, attn, measpd);

	return (RetVal);

}

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_peaktune (
                     char *ResourceName,
					 double *measfreq,
					 int attn
					 )
{
	int RetVal;
	int Status;
	char format[512];
	
	sprintf(format, "RetInt32,RetDbl,SrcInt32");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "rf_peaktune",
               format,
               &RetVal, measfreq, attn);

	return (RetVal);
}

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_eip1315a_setLO (
                     char *ResourceName,
					 double frequency,
					 short mode,
					 int attn,
					 short IFDET)
{
	int RetVal;
	int Status;
	char format[512];
	
	sprintf(format, "RetInt32,SrcDbl,SrcInt16,SrcInt32,SrcInt16");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "eip1315a_setLO",
               format,
               &RetVal, frequency, mode, attn, IFDET);

	return (RetVal);
}

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_gt55210a_setLO (
                     char *ResourceName,
					 double frequency,
					 short mode,
					 int attn,
					 int IFDET)
{
	int RetVal;
	int Status;
	char format[512];
	
	sprintf(format, "RetInt32,SrcDbl,SrcInt16,SrcInt32,SrcInt32");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "gt55210a_setLO",
               format,
               &RetVal, frequency, mode, attn, IFDET);

	return (RetVal);
}

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_atlas_rf_peaktune (
                    char *ResourceName, 
					double *measfreq, 
					int attn, 
					double freqmin, 
					double freqmax)
{
	int RetVal;
	int Status;
	char format[512];
	
	sprintf(format, "RetInt32,RetDbl,SrcInt32,SrcDbl,SrcDbl");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "atlas_rf_peaktune",
               format,
               &RetVal, measfreq, attn, freqmin, freqmax);

	return (RetVal);
}

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rfcnt_reset (
                    char *ResourceName)
{
	int RetVal;
	int Status;
	char format[512];
	
	sprintf(format, "RetInt32");
	
	Status = atxml_CallDriverFunction (ResourceName,
               "rfcnt_reset",
               format,
               &RetVal);

	return (RetVal);
}

#ifdef NOT_IMPLEMENTED
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOpenDefaultRM (ViPSession vi); /**=**/
int viOpenDefaultRM (char *ResourceName,
                     int vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viOpenDefaultRM",
		     "RetInt32,Handle",
			 &RetVal,  vi);

	return(RetVal);
}
#endif

#ifdef NOT_IMPLEMENTED
/*- Resource Manager Functions and Operations -------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viFindRsrc      (ViSession sesn, ViString expr, ViPFindList vi,
//                                    ViPUInt32 retCnt, ViChar desc[]); /**=**/
int viFindRsrc (char *ResourceName,
                unsigned long sesn,
                char *expr,
    /**=**/     unsigned long *vi,        // couldn't find ViPFindList
                unsigned long *retCnt,
                char *desc)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viFindRsrc",
		     "RetInt32,Handle,SrcStrPtr,RetUInt32,RetUInt32,RetStrPtr",
			 &RetVal,  sesn,  expr,     vi,       retCnt,   desc);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viFindNext      (ViFindList vi, ViChar desc[]);
int viFindNext (char *ResourceName,
      /**=**/   //ViFindList vi,        // couldn't find ViFindList
                char *desc)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viFindNext",
		     "RetInt32,Handle,RetStrPtr",
			 &RetVal,  vi,    desc);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viParseRsrc     (ViSession rmSesn, ViRsrc rsrcName,
//             (not in NI-VISA book)  ViPUInt16 intfType, ViPUInt16 intfNum);
int viParseRsrc (char *ResourceName,
                unsigned long rmSesn,
                char *rsrcName,
                unsigned short *intfType,
                unsigned short *intfNum)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viParseRsrc",
		     "RetInt32,Handle,SrcStrPtr,RetUInt16,RetUInt16",
			 &RetVal,  rmSesn,rsrcName, intfType, intfNum);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viParseRsrcEx   (ViSession rmSesn, ViRsrc rsrcName, ViPUInt16 intfType,
//                                    ViPUInt16 intfNum, ViChar _VI_FAR rsrcClass[],
//                                    ViChar _VI_FAR expandedUnaliasedName[],
//                                    ViChar _VI_FAR aliasIfExists[]);
int viParseRsrcEx (char *ResourceName,
                   unsigned long rmSesn,
                   char *rsrcName,
                   unsigned short *intfType,
                   unsigned short *intfNum,
                   char *rsrcClass,
                   char *expandedUnaliasedName,
                   char *aliasIfExists)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viParseRsrcEx",
		     "RetInt32,Handle,SrcStrPtr,RetUInt16,RetUInt16,SrcStrPtr,SrcStrPtr,            SrcStrPtr",
			 &RetVal,  rmSesn,rsrcName, intfType, intfNum,  rsrcClass,expandedUnaliasedName,aliasIfExists);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOpen          (ViSession sesn, ViRsrc name, ViAccessMode mode,
//                                    ViUInt32 timeout, ViPSession vi);
int viOpen (char *ResourceName,
            unsigned long sesn,
            char *name,
  /**=**/   unsigned long mode,		// couldn't find ViAccessMode
            unsigned long timeout,
            unsigned long *vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
             "viOpen",
		     "RetInt32,Handle,SrcStrPtr,SrcUInt32,SrcUInt32,RetUInt32",
			 &RetVal,  sesn,  name,     mode,     timeout,  vi);

	return(RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viClose         (ViObject vi);
int viClose (char *ResourceName,
             int vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
			 "viClose",
		     "RetInt32,Handle",
			 &RetVal,  vi);

	return(RetVal);
}

#endif

#ifdef NOT_IMPLEMENTED
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMapAddress    (ViSession vi, ViUInt16 mapSpace, ViBusAddress mapOffset,
//                                    ViBusSize mapSize, ViBoolean access,
//                                    ViAddr suggested, ViPAddr address);
int viMapAddress (char *ResourceName,
                  int vi,
                  unsigned short mapSpace,
                  unsigned long mapOffset,
                  unsigned short mapSize,
                  unsigned short access,
                  void *suggested,              /**=**/
                  void *address)               /**=**/
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viMapAddress",
               "RetInt32,Handle,SrcUInt16,SrcUInt32,SrcUInt32,SrcBool,SrcUInt32,SrcUInt16,SrcUInt32,SrcUInt32",
                &RetVal, vi,    mapSpace, mapOffset,mapSize,  access, suggested,destWidth,srcLength,jobId);

	return (RetVal);
}
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUnmapAddress  (ViSession vi);
int viUnmapAddress (char *ResourceName,
                    int vi)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viUnmapAddress",
               "RetInt32,Handle",
                &RetVal, vi);

	return (RetVal);
}


/*---------------------------------------------------------------------------*/
//void     _VI_FUNC  viPeek8         (ViSession vi, ViAddr address, ViPUInt8  val8);
void viPeek8 (char *ResourceName,
                int vi,
                unsigned char *address,
                unsigned char *val8)
{
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viPeek8",
               "Void,Handle,SrcUInt8,RetUInt8",
                vi,    address, val8);

	return;
}

/*---------------------------------------------------------------------------*/
//void     _VI_FUNC  viPoke8         (ViSession vi, ViAddr address, ViUInt8   val8);
void viPoke8 (char *ResourceName,
                int vi,
                unsigned char *address,
                unsigned char val8)
{
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viPoke8",
               "Void,Handle,RetUInt8,SrcUInt8",
                vi,    address, val8);

	return;
}

/*---------------------------------------------------------------------------*/
//void     _VI_FUNC  viPeek16        (ViSession vi, ViAddr address, ViPUInt16 val16);
void viPeek16 (char *ResourceName,
                int vi,
                unsigned short *address,
                unsigned short *val16)
{
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viPeek16",
               "Void,Handle,SrcUInt16,RetUInt16",
                vi,    address,  val16);

	return;
}

/*---------------------------------------------------------------------------*/
//void     _VI_FUNC  viPoke16        (ViSession vi, ViAddr address, ViUInt16  val16);
void viPoke16 (char *ResourceName,
                int vi,
                unsigned char *address,
                unsigned char val16)
{
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viPoke16",
               "Void,Handle,RetUInt16,SrcUInt16",
                vi,    address,  val16);

	return;
}

/*---------------------------------------------------------------------------*/
//void     _VI_FUNC  viPeek32        (ViSession vi, ViAddr address, ViPUInt32 val32);
void viPeek32 (char *ResourceName,
               int vi,
               unsigned long *address,
               unsigned long *val32)
{
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viPeek32",
               "Void,Handle,SrcUInt32,RetUInt32",
                vi,    address,  val32);

	return;
}

/*---------------------------------------------------------------------------*/
//void     _VI_FUNC  viPoke32        (ViSession vi, ViAddr address, ViUInt32  val32);
void viPoke32 (char *ResourceName,
               int vi,
               unsigned long *address,
               unsigned long val32)
{
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viPoke32",
               "Void,Handle,RetUInt32,SrcUInt32",
                vi,    address,  val32);

	return;
}


/*- Shared Memory Operations ------------------------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMemAlloc      (ViSession vi, ViBusSize size, ViPBusAddress offset);
int viMemAlloc (char *ResourceName,
                int vi,
                unsigned short size,
                unsigned long *offset)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viMemAlloc",
               "RetInt32,Handle,SrcUInt16,RetUInt32",
                &RetVal, vi,    size,  offset);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMemFree       (ViSession vi, ViBusAddress offset);
int viMemFree (char *ResourceName,
               int vi,
               unsigned long offset)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viMemFree",
               "RetInt32,Handle,SrcUInt32",
                &RetVal, vi,    offset);

	return (RetVal);
}
#endif

#ifdef NOT_IMPLEMENTED
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUsbControlIn  (ViSession vi, ViInt16 bmRequestType, ViInt16 bRequest,
//                                    ViUInt16 wValue, ViUInt16 wIndex, ViUInt16 wLength,
//                                    ViPBuf buf, ViPUInt16 retCnt);
int viUsbControlIn (char *ResourceName,
                    int vi,
                    short bmRequestType,
                    short bRequest,
                    unsigned short wValue,
                    unsigned short wIndex,
                    unsigned short wLength,
                    unsigned char *buf,      /**=**/
                    unsigned short *retCnt)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viUsbControlIn",
               "RetInt32,Handle,SrcInt16,     SrcInt16,SrcUInt16,SrcUInt16,SrcUInt16,SrcStrPtr,RetUInt16",
                &RetVal, vi,    bmRequestType,bRequest,wValue,   wIndex,   wLength,  buf.      retCnt);

	return (RetVal);
}

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUsbControlOut (ViSession vi, ViInt16 bmRequestType, ViInt16 bRequest,
//                                    ViUInt16 wValue, ViUInt16 wIndex, ViUInt16 wLength,
//                                    ViBuf buf);
int viUsbControlOut (char *ResourceName,
                     int vi,
                     short bmRequestType,
                     short bRequest,
                     unsigned short wValue,
                     unsigned short wIndex,
                     unsigned short wLength,
                     unsigned char *buf)
{
	int RetVal;
	int Status;

	Status = atxml_CallDriverFunction (ResourceName,
               "viUsbControlOut",
               "RetInt32,Handle,SrcInt16,     SrcInt16,SrcUInt16,SrcUInt16,SrcUInt16,SrcStrPtr",
                &RetVal, vi,    bmRequestType,bRequest,wValue,   wIndex,   wLength,  buf);

	return (RetVal);
}

#endif


