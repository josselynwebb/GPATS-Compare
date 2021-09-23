// VisaNamCalls.h

// CICL Driver Function Interface Calls for Visa
// Note all calls in the code must add an initial argument of ResourceName

#ifndef __DRVRFNC_ATXML_HEADER__
#define __DRVRFNC_ATXML_HEADER__

#pragma warning(disable:4786)
#pragma warning(disable:4996)

#define CALL_TYPE __stdcall

#ifndef __cplusplus

#ifdef ATXML_DRVR_EXPORTS
#define ATXML_DRVRFNC __declspec(dllexport)
#else
#define ATXML_DRVRFNC __declspec(dllimport)
#endif

#else

#ifdef ATXML_DRVR_EXPORTS
#define ATXML_DRVRFNC "C" __declspec(dllexport)
#else
#define ATXML_DRVRFNC "C" __declspec(dllimport)
#endif

#endif
// 8-bit  == char
// 16-bit == short
// 32-bit == long
// Phase 3: Build the core (huh?)
#define ViStatus unsigned long
#define ViSession unsigned long
#define ViInt32   long
#define ViUInt32  unsigned long
#define ViUInt16  unsigned short
#define ViUInt8   unsigned char
#define ViChar    char

#define VI_SUCCESS          (0L)
#define VI_NULL             (0)
#define VI_TRUE             (1)
#define VI_FALSE            (0)
#define VI_ATTR_TMO_VALUE           (0x3FFF001AUL)
//#define VI_LOCAL_SPACE              (0)
//#define VI_A16_SPACE                (1)
#define VI_A24_SPACE                (2)
//#define VI_A32_SPACE                (3)
//#define VI_OPAQUE_SPACE             (0xFFFF)

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viSetAttribute  (ViObject vi, ViAttr attrName, ViAttrState attrValue);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viSetAttribute(char *ResourceName,
                   int vi,
                   unsigned long Attrib,
                   unsigned long AttribValue);

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGetAttribute  (ViObject vi, ViAttr attrName, void _VI_PTR attrValue /**=**/RetUInt32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGetAttribute (char *ResourceName,
                    unsigned long vi,
                    unsigned long attrName,
                    unsigned long *attrValue);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viStatusDesc    (ViObject vi, ViStatus status, ViChar desc[]);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viStatusDesc (char *ResourceName,
                  unsigned long vi,
                  long status,
                  char *desc);
//*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viTerminate     (ViObject vi, ViUInt16 degree, ViJobId jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viTerminate(char *ResourceName,
                   int vi,
                   unsigned short degree,
                   unsigned long jobId);
//*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viLock          (ViSession vi, ViAccessMode lockType, ViUInt32 timeout,
//                                    ViKeyId requestedKey, ViChar accessKey[]);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viLock (char *ResourceName,
            unsigned long vi,
            unsigned long lockType,      // couldn't find ViAccessMode
            unsigned long timeout,
            unsigned long requestedKey,  
            char *accessKey);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUnlock        (ViSession vi);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viUnlock (char *ResourceName,
              int vi);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viEnableEvent   (ViSession vi, ViEventType eventType, ViUInt16 mechanism,
//                                    ViEventFilter context);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viEnableEvent (char *ResourceName,
                   int vi,
                   unsigned long eventType,
                   unsigned short mechanism,
                   unsigned long context);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viDisableEvent  (ViSession vi, ViEventType eventType, ViUInt16 mechanism);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viDisableEvent (char *ResourceName,
                    int vi,
                    unsigned long eventType,
                    unsigned short mechanism);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viDiscardEvents (ViSession vi, ViEventType eventType, ViUInt16 mechanism);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viDiscardEvents (char *ResourceName,
                     int vi,
                     unsigned long eventType,
                     unsigned short mechanism);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWaitOnEvent   (ViSession vi, ViEventType inEventType, ViUInt32 timeout,
//                                    ViPEventType outEventType, ViPEvent outContext);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWaitOnEvent (char *ResourceName,
                   int vi,
                   unsigned long eventType,
                   unsigned long timeout,
                   unsigned long *outEventType,
                   unsigned long *outContext);
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
            unsigned long *retCnt);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viReadAsync     (ViSession vi, ViPBuf buf, ViUInt32 cnt, ViPJobId  jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viReadAsync (char *ResourceName,
            int vi,
            unsigned char *buf,
            unsigned long cnt,
            unsigned long *jobId);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viReadToFile    (ViSession vi, ViConstString filename, ViUInt32 cnt,
//                                    ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viReadToFile (char *ResourceName,
                  int vi,
                  unsigned const char *filename,
                  unsigned long cnt,
                  unsigned long *retCnt);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWrite         (ViSession vi, ViBuf  buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWrite (char *ResourceName,
             int vi,
             unsigned char *buf,
             unsigned long cnt,
             int *retCnt);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWriteAsync    (ViSession vi, ViBuf  buf, ViUInt32 cnt, ViPJobId  jobId);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWriteAsync (char *ResourceName,
                  int vi,
                  unsigned char *buf,
                  unsigned long cnt,
                  unsigned long *jobId);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viWriteFromFile (ViSession vi, ViConstString filename, ViUInt32 cnt,
//                                    ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viWriteFromFile (char *ResourceName,
                     int vi,
                     unsigned const char *filename,
                     unsigned long cnt,
                     unsigned long *retCnt);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viAssertTrigger (ViSession vi, ViUInt16 protocol);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viAssertTrigger (char *ResourceName,
                     int vi,
                     unsigned short protocol);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viReadSTB       (ViSession vi, ViPUInt16 status);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viReadSTB (char *ResourceName,
               int vi,
               unsigned short *status);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viClear         (ViSession vi);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viClear (char *ResourceName,
             int vi);
/*- Formatted and Buffered I/O Operations -----------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viSetBuf        (ViSession vi, ViUInt16 mask, ViUInt32 size);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viSetBuf (char *ResourceName,
              int vi,
              unsigned short mask,
              unsigned long size);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viFlush         (ViSession vi, ViUInt16 mask);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viFlush (char *ResourceName,
             int vi,
             unsigned short mask);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viBufWrite      (ViSession vi, ViBuf  buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viBufWrite (char *ResourceName,
                int vi,
                unsigned char *buf,
                unsigned long cnt,
                unsigned long *retCnt);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viBufRead       (ViSession vi, ViPBuf buf, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viBufRead (char *ResourceName,
               int vi,
               unsigned char *buf, 
               unsigned long cnt,
               unsigned long *retCnt);
/*---------------------------------------------------------------------------*/

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
           unsigned char *val8);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOut8          (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViUInt8   val8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viOut8 (char *ResourceName,
            int vi,
            unsigned short space,
            unsigned long offset,
            unsigned char val8);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viIn16          (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViPUInt16 val16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viIn16 (char *ResourceName,
            int vi,
            unsigned short space,
            unsigned long offset,
            unsigned short *val16);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOut16         (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViUInt16  val16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viOut16 (char *ResourceName,
             int vi,
             unsigned short space,
             unsigned long offset,
             unsigned short val16);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viIn32          (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViPUInt32 val32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viIn32 (char *ResourceName,
            int vi,
            unsigned short space,
            unsigned long offset,
            unsigned long *val32);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viOut32         (ViSession vi, ViUInt16 space,
//                                    ViBusAddress offset, ViUInt32  val32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viOut32 (char *ResourceName,
             int vi,
             unsigned short space,
             unsigned long offset,
             unsigned long val32);

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveIn8       (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt8  buf8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveIn8 (char *ResourceName,
               int vi,
               unsigned short space,
               unsigned long offset,
               unsigned short length,
               unsigned char *buf8);

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveOut8      (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt8  buf8);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveOut8 (char *ResourceName,
               int vi,
               unsigned short space,
               unsigned long offset,
               unsigned short length,
               unsigned char *buf8);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveIn16      (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt16 buf16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveIn16 (char *ResourceName,
                int vi,
                unsigned short space,
                unsigned long offset,
                unsigned short length,
                unsigned short *buf16);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveOut16     (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt16 buf16);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveOut16 (char *ResourceName,
                 int vi,
                 unsigned short space,
                 unsigned long offset,
                 unsigned short length,
                 unsigned short *buf16);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveIn32      (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt32 buf32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveIn32 (char *ResourceName,
                int vi,
                unsigned short space,
                unsigned long offset,
                unsigned short length,
                unsigned long *buf32);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMoveOut32     (ViSession vi, ViUInt16 space, ViBusAddress offset,
//                                    ViBusSize length, ViAUInt32 buf32);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMoveOut32 (char *ResourceName,
                 int vi,
                 unsigned short space,
                 unsigned long offset,
                 unsigned short length,
                 unsigned long *buf32);
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
            unsigned short srcLength);
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
                 unsigned long *jobId);
/*- Interface Specific Operations -------------------------------------------*/

/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibControlREN(ViSession vi, ViUInt16 mode);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibControlREN (char *ResourceName,
                      int vi,
                      unsigned short mode);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibControlATN(ViSession vi, ViUInt16 mode);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibControlATN (char *ResourceName,
                      int vi,
                      unsigned short mode);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibSendIFC   (ViSession vi);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibSendIFC (char *ResourceName,
                int vi);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibCommand   (ViSession vi, ViBuf cmd, ViUInt32 cnt, ViPUInt32 retCnt);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibCommand (char *ResourceName,
                   int vi,
                   unsigned char *cmd,
                   unsigned long cnt,
                   unsigned long *retCnt);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viGpibPassControl(ViSession vi, ViUInt16 primAddr, ViUInt16 secAddr);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viGpibPassControl (char *ResourceName,
                       int vi,
                       unsigned short primAddr,
                       unsigned short secAddr);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viVxiCommandQuery(ViSession vi, ViUInt16 mode, ViUInt32 cmd,
//                                     ViPUInt32 response);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viVxiCommandQuery (char *ResourceName,
                       int vi,
                       unsigned short mode,
                       unsigned long cmd,
                       unsigned long *response);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viAssertUtilSignal(ViSession vi, ViUInt16 line);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viAssertUtilSignal (char *ResourceName,
                        int vi,
                        unsigned short line);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viAssertIntrSignal(ViSession vi, ViInt16 mode, ViUInt32 statusID);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viAssertIntrSignal (char *ResourceName,
                        int vi,
                        short mode,
                        unsigned long statusID);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viMapTrigger    (ViSession vi, ViInt16 trigSrc, ViInt16 trigDest,
//                                    ViUInt16 mode);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viMapTrigger (char *ResourceName,
                  int vi,
                  short trigSrc,
                  short trigDest,
                  unsigned short mode);
/*---------------------------------------------------------------------------*/
//ViStatus _VI_FUNC  viUnmapTrigger  (ViSession vi, ViInt16 trigSrc, ViInt16 trigDest);
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viUnmapTrigger (char *ResourceName,
                    int vi,
                    short trigSrc,
                    short trigDest);
/*---------------------------------------------------------------------------*/
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_zt1428_read_waveform (char *ResourceName,
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
                     int *yReference);


extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_hpe1428a_WvData(
char *ResourceName,
int vi,
float YIncrement, 
float YOffset, 
float XIncrement, 
float XOffset, 
unsigned short WhichArea, 
unsigned short TotChan, 
unsigned short AquType);

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_hpe1428a_GtWv (
                     char *ResourceName,
					 int  number_ofPoints,
                     float WaveArray[], 
					 float TimeArray[]);

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_hpe1428a_FindInst (
                     char *ResourceName,
					 int vi,
                     unsigned int MemOffset1,
					 unsigned int MemOffset2,
					 unsigned int MathOffset,
					 unsigned short MemoryPoints,
					 unsigned int *VMEAddress);

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_measfreq (
                     char *ResourceName,
					 double expectedfreq,
					 int attn,
					 double *measfreq
					 );
					
extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_measpw (
                     char *ResourceName,
					 double carrierfreq,
					 double expectedpw,
					 int attn,
					 double *measpw
					 );

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_measpd (
                     char *ResourceName,
					 double carrierfreq,
					 double expectedpd,
					 int attn,
					 double *measpd
					 );

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rf_peaktune (
                     char *ResourceName,
					 double *measfreq,
					 int attn
					 );

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_eip1315a_setLO (
                     char *ResourceName,
					 double frequency,
					 short mode,
					 int attn,
					 short IFDET
					 );

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_gt55210a_setLO (
                     char *ResourceName,
					 double frequency,
					 short mode,
					 int attn,
					 int IFDET
					 );

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_atlas_rf_peaktune (
									char *ResourceName, 
									double *measfreq, 
									int attn, 
									double freqmin, 
									double freqmax);

extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_rfcnt_reset (
									char *ResourceName);

#endif // __DRVRFNC_ATXML_HEADER__