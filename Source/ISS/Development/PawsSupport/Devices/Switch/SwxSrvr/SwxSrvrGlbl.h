#define LANG_STD     "PAWS"
#define SYST_SUBSET  "TETS"
///////////////////////////////////////////////////////////////////////////////
// File:	SwxSrvrGlbl.h
//
// Purpose: Definitions and prototypes for SwxSrvr.cpp global utilities.
//          These are equivilent functions of cemsupport and are duplicated
//          ere to avoid circular .dll referencing
//
// Date:	05/12/05
//
//
// Revision History
//           Revisions tracked in SwxSrvr.cpp file
//
///////////////////////////////////////////////////////////////////////////////
#define strnzcpy(x,y,z) {strncpy(x,y,z); x[z - 1] = '\0';}
#define IFNSIM(x,y) { \
                    if(!x) { y ;} \
                  }
#define CEMDEBUG(dbg, lvl, devicename, msg)  g_DebugMsg(dbg,lvl,devicename,msg)
#define CEMERROR(devicename, msg)  g_ErrorMsg(devicename,msg)

#define MAX_MSG_SIZE 256

// Global Variables
extern int g_Sim;
extern int g_Dbg;

// Global Utility Functions
char *g_FmtMsg(char *Fmt,...);
void  g_DebugMsg(int MemLevel, int ReqLevel, char *MemDevName, char *UsrMsg);
void  g_ErrorMsg(char *MemDevName, char *UsrMsg);
void  g_GetSwxInfo(void);
