//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    Dso.cpp
//
// Date:	    05-APR-06
//
// Purpose:	    Instrument Driver for Dso
//
// Instrument:	Dso  <device description> (DMM)
//
//                    Required Libraries / DLL's
//		
//		Library/DLL					Purpose
//	=====================  ===============================================
//     cem.lib              \usr\tyx\lib (TYX CEM Library functions)
//     cemsupport.lib       ..\..\Common\lib  (ARC CEM support functions)
//
// ATLAS Subset: All
//
//
//       This initial program was generated via the CEM Wizard.
//       The "ctlr.cpp" functions were copied to this file to
//       allow the use of "static" variables vs. "global" variables.
//
// Revision History described in Dso_T.cpp
//
///////////////////////////////////////////////////////////////////////////////

//Hide 'truncated debug information' warnings
#pragma warning(disable : 4786)

// Includes
#include "cem.h"
#include "key.h"
#include <limits.h>
#include <float.h>
#include "cemsupport.h"
#include "Dso_T.h"

// Local Defines
#define IEEE_716 /*using 716.89 compiler with CLOSE/OPEN missing*/

#define MAX_DEVICES  1
// Static Variables
static int        s_NumDev;
static CS_DEVINFO s_DevInfo[MAX_DEVICES]; // Device and BusConfi information
static int        s_DoDclCount = 0;

// Local Function Prototypes

//++++/////////////////////////////////////////////////////////////////////////
// Exposed Functions
///////////////////////////////////////////////////////////////////////////////

//++++/////////////////////////////////////////////////////////////////////////
// Callback Functions moved from "ctlr.cpp"
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:DCL
int CCALLBACK doDcl (void)
//END{DFW}
{
    if(s_DevInfo[s_DoDclCount].DriverClass != NULL)
	{
		((CDso_T *)(s_DevInfo[s_DoDclCount].DriverClass))->ResetDso(0);
	}

	// Signal system reset in process
	cs_SysResetDevice(s_DevInfo[s_DoDclCount].DeviceName);

    s_DoDclCount++;
    if(s_DoDclCount >= s_NumDev)
	{
        s_DoDclCount = 0;
	}

    return 0;
}

//BEGIN{DFW}:OPEN
int CCALLBACK doOpen (void)
//END{DFW}
{

	int i;

    // DoOpen called prior to each NAM execution
    static bool DoneOnce = FALSE;
    
	if(DoneOnce)
    {
        return 0;
    }
    
	DoneOnce = TRUE;

	// inhibit multiple reset calls
	DclResetDisable("AllDevices");

    // Check if already initialized
    if(s_NumDev)
    {
        return(0);
    }
    
	memset(s_DevInfo,0,sizeof(s_DevInfo));

    s_NumDev = cs_GetDevInfo("DSO", 1, s_DevInfo, MAX_DEVICES);

    if(s_NumDev == 0)
    {
        // Error can't find instrument
        return(0);
    }
    
	for(i = 0; i < s_NumDev; i++)
    {
        s_DevInfo[i].DriverClass = new CDso_T(
                              s_DevInfo[i].DeviceName,
                              s_DevInfo[i].BusNo,
                              s_DevInfo[i].PrimaryAdr,
                              s_DevInfo[i].SecondaryAdr,
                              s_DevInfo[i].Dbg, s_DevInfo[i].Sim);

    }

	return 0;
}

//BEGIN{DFW}:DISCONNECT
int CCALLBACK doUnload (void)
//END{DFW}
{
    int i;

    for(i = 0; i < s_NumDev; i++)
    {
        if(s_DevInfo[i].DriverClass != NULL)
		{
            delete((CDso_T *)(s_DevInfo[i].DriverClass));
		}

        s_DevInfo[i].DriverClass = NULL;

    }

    s_NumDev = 0;
	
	return 0;
}

//++++/////////////////////////////////////////////////////////////////////////
// Wizard Generated Functions
///////////////////////////////////////////////////////////////////////////////

//BEGIN{DFW}:DSO_1:0:0|DSO_1:1:0|DSO_1:2:0|DSO_1:3:0|DSO_1:4:0|DSO_1:5:0|DSO_1:6:0|DSO_1:7:0|DSO_1:11:0|DSO_1:12:0|DSO_1:13:0|DSO_1:14:0|DSO_1:101:0|DSO_1:102:0|DSO_1:103:0|DSO_1:104:0|DSO_1:105:0|DSO_1:106:0|DSO_1:107:0|DSO_1:108:0|DSO_1:109:0|DSO_1:110:0|DSO_1:111:0|DSO_1:112:0|DSO_1:113:0|DSO_1:114:0|DSO_1:115:0|DSO_1:116:0|DSO_1:117:0|DSO_1:118:0|DSO_1:119:0|DSO_1:120:0|DSO_1:121:0|DSO_1:122:0|DSO_1:123:0|DSO_1:124:0|DSO_1:125:0|DSO_1:126:0|DSO_1:127:0|DSO_1:128:0|DSO_1:129:0|DSO_1:132:0|DSO_1:133:0|DSO_1:134:0|DSO_1:135:0|DSO_1:136:0|DSO_1:137:0|DSO_1:138:0|DSO_1:139:0|DSO_1:140:0|DSO_1:142:0|DSO_1:143:0|DSO_1:144:0|DSO_1:145:0|DSO_1:146:0|DSO_1:147:0|DSO_1:148:0|DSO_1:149:0|DSO_1:150:0|DSO_1:152:0|DSO_1:153:0|DSO_1:154:0|DSO_1:155:0|DSO_1:156:0|DSO_1:157:0|DSO_1:158:0|DSO_1:159:0|DSO_1:160:0|DSO_1:161:0|DSO_1:162:0|DSO_1:163:0|DSO_1:201:0|DSO_1:202:0|DSO_1:203:0|DSO_1:204:0|DSO_1:205:0|DSO_1:206:0|DSO_1:207:0|DSO_1:208:0|DSO_1:209:0|DSO_1:210:0|DSO_1:211:0|DSO_1:212:0|DSO_1:213:0|DSO_1:214:0|DSO_1:215:0|DSO_1:216:0|DSO_1:217:0|DSO_1:218:0|DSO_1:219:0|DSO_1:220:0|DSO_1:221:0|DSO_1:222:0|DSO_1:223:0|DSO_1:224:0|DSO_1:225:0|DSO_1:226:0|DSO_1:227:0|DSO_1:228:0|DSO_1:229:0|DSO_1:230:0|DSO_1:232:0|DSO_1:233:0|DSO_1:234:0|DSO_1:235:0|DSO_1:236:0|DSO_1:237:0|DSO_1:238:0|DSO_1:239:0|DSO_1:240:0|DSO_1:241:0|DSO_1:242:0|DSO_1:243:0|DSO_1:244:0|DSO_1:245:0|DSO_1:246:0|DSO_1:247:0|DSO_1:248:0|DSO_1:249:0|DSO_1:250:0|DSO_1:251:0|DSO_1:252:0|DSO_1:253:0|DSO_1:254:0|DSO_1:255:0
int doDSO_Setup ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->SetupDso(Fnc);
	}


#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CDso_T *)(DriverClass))->CloseDso(Fnc);
#endif

	return (int)0;
}

//BEGIN{DFW}:DSO_1:0:1|DSO_1:1:1|DSO_1:2:1|DSO_1:3:1|DSO_1:4:1|DSO_1:5:1|DSO_1:6:1|DSO_1:7:1|DSO_1:11:1|DSO_1:12:1|DSO_1:13:1|DSO_1:14:1|DSO_1:101:1|DSO_1:102:1|DSO_1:103:1|DSO_1:104:1|DSO_1:105:1|DSO_1:106:1|DSO_1:107:1|DSO_1:108:1|DSO_1:109:1|DSO_1:110:1|DSO_1:111:1|DSO_1:112:1|DSO_1:113:1|DSO_1:114:1|DSO_1:115:1|DSO_1:116:1|DSO_1:117:1|DSO_1:118:1|DSO_1:119:1|DSO_1:120:1|DSO_1:121:1|DSO_1:122:1|DSO_1:123:1|DSO_1:124:1|DSO_1:125:1|DSO_1:126:1|DSO_1:127:1|DSO_1:128:1|DSO_1:129:1|DSO_1:132:1|DSO_1:133:1|DSO_1:134:1|DSO_1:135:1|DSO_1:136:1|DSO_1:137:1|DSO_1:138:1|DSO_1:139:1|DSO_1:140:1|DSO_1:142:1|DSO_1:143:1|DSO_1:144:1|DSO_1:145:1|DSO_1:146:1|DSO_1:147:1|DSO_1:148:1|DSO_1:149:1|DSO_1:150:1|DSO_1:152:1|DSO_1:153:1|DSO_1:154:1|DSO_1:155:1|DSO_1:156:1|DSO_1:157:1|DSO_1:158:1|DSO_1:159:1|DSO_1:160:1|DSO_1:161:1|DSO_1:162:1|DSO_1:163:1|DSO_1:201:1|DSO_1:202:1|DSO_1:203:1|DSO_1:204:1|DSO_1:205:1|DSO_1:206:1|DSO_1:207:1|DSO_1:208:1|DSO_1:209:1|DSO_1:210:1|DSO_1:211:1|DSO_1:212:1|DSO_1:213:1|DSO_1:214:1|DSO_1:215:1|DSO_1:216:1|DSO_1:217:1|DSO_1:218:1|DSO_1:219:1|DSO_1:220:1|DSO_1:221:1|DSO_1:222:1|DSO_1:223:1|DSO_1:224:1|DSO_1:225:1|DSO_1:226:1|DSO_1:227:1|DSO_1:228:1|DSO_1:229:1|DSO_1:230:1|DSO_1:232:1|DSO_1:233:1|DSO_1:234:1|DSO_1:235:1|DSO_1:236:1|DSO_1:237:1|DSO_1:238:1|DSO_1:239:1|DSO_1:240:1|DSO_1:241:1|DSO_1:242:1|DSO_1:243:1|DSO_1:244:1|DSO_1:245:1|DSO_1:246:1|DSO_1:247:1|DSO_1:248:1|DSO_1:249:1|DSO_1:250:1|DSO_1:251:1|DSO_1:252:1|DSO_1:253:1|DSO_1:254:1|DSO_1:255:1
int doDSO_Status ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->StatusDso(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:2|DSO_1:1:2|DSO_1:2:2|DSO_1:3:2|DSO_1:4:2|DSO_1:5:2|DSO_1:6:2|DSO_1:7:2|DSO_1:11:2|DSO_1:12:2|DSO_1:13:2|DSO_1:14:2|DSO_1:101:2|DSO_1:102:2|DSO_1:103:2|DSO_1:104:2|DSO_1:105:2|DSO_1:106:2|DSO_1:107:2|DSO_1:108:2|DSO_1:109:2|DSO_1:110:2|DSO_1:111:2|DSO_1:112:2|DSO_1:113:2|DSO_1:114:2|DSO_1:115:2|DSO_1:116:2|DSO_1:117:2|DSO_1:118:2|DSO_1:119:2|DSO_1:120:2|DSO_1:121:2|DSO_1:122:2|DSO_1:123:2|DSO_1:124:2|DSO_1:125:2|DSO_1:126:2|DSO_1:127:2|DSO_1:128:2|DSO_1:129:2|DSO_1:132:2|DSO_1:133:2|DSO_1:134:2|DSO_1:135:2|DSO_1:136:2|DSO_1:137:2|DSO_1:138:2|DSO_1:139:2|DSO_1:140:2|DSO_1:142:2|DSO_1:143:2|DSO_1:144:2|DSO_1:145:2|DSO_1:146:2|DSO_1:147:2|DSO_1:148:2|DSO_1:149:2|DSO_1:150:2|DSO_1:152:2|DSO_1:153:2|DSO_1:154:2|DSO_1:155:2|DSO_1:156:2|DSO_1:157:2|DSO_1:158:2|DSO_1:159:2|DSO_1:160:2|DSO_1:161:2|DSO_1:162:2|DSO_1:163:2|DSO_1:201:2|DSO_1:202:2|DSO_1:203:2|DSO_1:204:2|DSO_1:205:2|DSO_1:206:2|DSO_1:207:2|DSO_1:208:2|DSO_1:209:2|DSO_1:210:2|DSO_1:211:2|DSO_1:212:2|DSO_1:213:2|DSO_1:214:2|DSO_1:215:2|DSO_1:216:2|DSO_1:217:2|DSO_1:218:2|DSO_1:219:2|DSO_1:220:2|DSO_1:221:2|DSO_1:222:2|DSO_1:223:2|DSO_1:224:2|DSO_1:225:2|DSO_1:226:2|DSO_1:227:2|DSO_1:228:2|DSO_1:229:2|DSO_1:230:2|DSO_1:232:2|DSO_1:233:2|DSO_1:234:2|DSO_1:235:2|DSO_1:236:2|DSO_1:237:2|DSO_1:238:2|DSO_1:239:2|DSO_1:240:2|DSO_1:241:2|DSO_1:242:2|DSO_1:243:2|DSO_1:244:2|DSO_1:245:2|DSO_1:246:2|DSO_1:247:2|DSO_1:248:2|DSO_1:249:2|DSO_1:250:2|DSO_1:251:2|DSO_1:252:2|DSO_1:253:2|DSO_1:254:2|DSO_1:255:2
int doDSO_Fetch ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->FetchDso(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:3|DSO_1:1:3|DSO_1:2:3|DSO_1:3:3|DSO_1:4:3|DSO_1:5:3|DSO_1:6:3|DSO_1:7:3|DSO_1:11:3|DSO_1:12:3|DSO_1:13:3|DSO_1:14:3|DSO_1:101:3|DSO_1:102:3|DSO_1:103:3|DSO_1:104:3|DSO_1:105:3|DSO_1:106:3|DSO_1:107:3|DSO_1:108:3|DSO_1:109:3|DSO_1:110:3|DSO_1:111:3|DSO_1:112:3|DSO_1:113:3|DSO_1:114:3|DSO_1:115:3|DSO_1:116:3|DSO_1:117:3|DSO_1:118:3|DSO_1:119:3|DSO_1:120:3|DSO_1:121:3|DSO_1:122:3|DSO_1:123:3|DSO_1:124:3|DSO_1:125:3|DSO_1:126:3|DSO_1:127:3|DSO_1:128:3|DSO_1:129:3|DSO_1:132:3|DSO_1:133:3|DSO_1:134:3|DSO_1:135:3|DSO_1:136:3|DSO_1:137:3|DSO_1:138:3|DSO_1:139:3|DSO_1:140:3|DSO_1:142:3|DSO_1:143:3|DSO_1:144:3|DSO_1:145:3|DSO_1:146:3|DSO_1:147:3|DSO_1:148:3|DSO_1:149:3|DSO_1:150:3|DSO_1:152:3|DSO_1:153:3|DSO_1:154:3|DSO_1:155:3|DSO_1:156:3|DSO_1:157:3|DSO_1:158:3|DSO_1:159:3|DSO_1:160:3|DSO_1:161:3|DSO_1:162:3|DSO_1:163:3|DSO_1:201:3|DSO_1:202:3|DSO_1:203:3|DSO_1:204:3|DSO_1:205:3|DSO_1:206:3|DSO_1:207:3|DSO_1:208:3|DSO_1:209:3|DSO_1:210:3|DSO_1:211:3|DSO_1:212:3|DSO_1:213:3|DSO_1:214:3|DSO_1:215:3|DSO_1:216:3|DSO_1:217:3|DSO_1:218:3|DSO_1:219:3|DSO_1:220:3|DSO_1:221:3|DSO_1:222:3|DSO_1:223:3|DSO_1:224:3|DSO_1:225:3|DSO_1:226:3|DSO_1:227:3|DSO_1:228:3|DSO_1:229:3|DSO_1:230:3|DSO_1:232:3|DSO_1:233:3|DSO_1:234:3|DSO_1:235:3|DSO_1:236:3|DSO_1:237:3|DSO_1:238:3|DSO_1:239:3|DSO_1:240:3|DSO_1:241:3|DSO_1:242:3|DSO_1:243:3|DSO_1:244:3|DSO_1:245:3|DSO_1:246:3|DSO_1:247:3|DSO_1:248:3|DSO_1:249:3|DSO_1:250:3|DSO_1:251:3|DSO_1:252:3|DSO_1:253:3|DSO_1:254:3|DSO_1:255:3
int doDSO_Init ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->InitiateDso(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:4|DSO_1:1:4|DSO_1:2:4|DSO_1:3:4|DSO_1:4:4|DSO_1:5:4|DSO_1:6:4|DSO_1:7:4|DSO_1:11:4|DSO_1:12:4|DSO_1:13:4|DSO_1:14:4|DSO_1:101:4|DSO_1:102:4|DSO_1:103:4|DSO_1:104:4|DSO_1:105:4|DSO_1:106:4|DSO_1:107:4|DSO_1:108:4|DSO_1:109:4|DSO_1:110:4|DSO_1:111:4|DSO_1:112:4|DSO_1:113:4|DSO_1:114:4|DSO_1:115:4|DSO_1:116:4|DSO_1:117:4|DSO_1:118:4|DSO_1:119:4|DSO_1:120:4|DSO_1:121:4|DSO_1:122:4|DSO_1:123:4|DSO_1:124:4|DSO_1:125:4|DSO_1:126:4|DSO_1:127:4|DSO_1:128:4|DSO_1:129:4|DSO_1:132:4|DSO_1:133:4|DSO_1:134:4|DSO_1:135:4|DSO_1:136:4|DSO_1:137:4|DSO_1:138:4|DSO_1:139:4|DSO_1:140:4|DSO_1:142:4|DSO_1:143:4|DSO_1:144:4|DSO_1:145:4|DSO_1:146:4|DSO_1:147:4|DSO_1:148:4|DSO_1:149:4|DSO_1:150:4|DSO_1:152:4|DSO_1:153:4|DSO_1:154:4|DSO_1:155:4|DSO_1:156:4|DSO_1:157:4|DSO_1:158:4|DSO_1:159:4|DSO_1:160:4|DSO_1:161:4|DSO_1:162:4|DSO_1:163:4|DSO_1:201:4|DSO_1:202:4|DSO_1:203:4|DSO_1:204:4|DSO_1:205:4|DSO_1:206:4|DSO_1:207:4|DSO_1:208:4|DSO_1:209:4|DSO_1:210:4|DSO_1:211:4|DSO_1:212:4|DSO_1:213:4|DSO_1:214:4|DSO_1:215:4|DSO_1:216:4|DSO_1:217:4|DSO_1:218:4|DSO_1:219:4|DSO_1:220:4|DSO_1:221:4|DSO_1:222:4|DSO_1:223:4|DSO_1:224:4|DSO_1:225:4|DSO_1:226:4|DSO_1:227:4|DSO_1:228:4|DSO_1:229:4|DSO_1:230:4|DSO_1:232:4|DSO_1:233:4|DSO_1:234:4|DSO_1:235:4|DSO_1:236:4|DSO_1:237:4|DSO_1:238:4|DSO_1:239:4|DSO_1:240:4|DSO_1:241:4|DSO_1:242:4|DSO_1:243:4|DSO_1:244:4|DSO_1:245:4|DSO_1:246:4|DSO_1:247:4|DSO_1:248:4|DSO_1:249:4|DSO_1:250:4|DSO_1:251:4|DSO_1:252:4|DSO_1:253:4|DSO_1:254:4|DSO_1:255:4
int doDSO_Reset ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

#ifdef IEEE_716
    // Any Errors already diagnosed
    Status = ((CDso_T *)(DriverClass))->OpenDso(Fnc);
#endif
    
	// Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->ResetDso(Fnc);
	}
	
	return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:5|DSO_1:1:5|DSO_1:2:5|DSO_1:3:5|DSO_1:4:5|DSO_1:5:5|DSO_1:6:5|DSO_1:7:5|DSO_1:11:5|DSO_1:12:5|DSO_1:13:5|DSO_1:14:5|DSO_1:101:5|DSO_1:102:5|DSO_1:103:5|DSO_1:104:5|DSO_1:105:5|DSO_1:106:5|DSO_1:107:5|DSO_1:108:5|DSO_1:109:5|DSO_1:110:5|DSO_1:111:5|DSO_1:112:5|DSO_1:113:5|DSO_1:114:5|DSO_1:115:5|DSO_1:116:5|DSO_1:117:5|DSO_1:118:5|DSO_1:119:5|DSO_1:120:5|DSO_1:121:5|DSO_1:122:5|DSO_1:123:5|DSO_1:124:5|DSO_1:125:5|DSO_1:126:5|DSO_1:127:5|DSO_1:128:5|DSO_1:129:5|DSO_1:132:5|DSO_1:133:5|DSO_1:134:5|DSO_1:135:5|DSO_1:136:5|DSO_1:137:5|DSO_1:138:5|DSO_1:139:5|DSO_1:140:5|DSO_1:142:5|DSO_1:143:5|DSO_1:144:5|DSO_1:145:5|DSO_1:146:5|DSO_1:147:5|DSO_1:148:5|DSO_1:149:5|DSO_1:150:5|DSO_1:152:5|DSO_1:153:5|DSO_1:154:5|DSO_1:155:5|DSO_1:156:5|DSO_1:157:5|DSO_1:158:5|DSO_1:159:5|DSO_1:160:5|DSO_1:161:5|DSO_1:162:5|DSO_1:163:5|DSO_1:201:5|DSO_1:202:5|DSO_1:203:5|DSO_1:204:5|DSO_1:205:5|DSO_1:206:5|DSO_1:207:5|DSO_1:208:5|DSO_1:209:5|DSO_1:210:5|DSO_1:211:5|DSO_1:212:5|DSO_1:213:5|DSO_1:214:5|DSO_1:215:5|DSO_1:216:5|DSO_1:217:5|DSO_1:218:5|DSO_1:219:5|DSO_1:220:5|DSO_1:221:5|DSO_1:222:5|DSO_1:223:5|DSO_1:224:5|DSO_1:225:5|DSO_1:226:5|DSO_1:227:5|DSO_1:228:5|DSO_1:229:5|DSO_1:230:5|DSO_1:232:5|DSO_1:233:5|DSO_1:234:5|DSO_1:235:5|DSO_1:236:5|DSO_1:237:5|DSO_1:238:5|DSO_1:239:5|DSO_1:240:5|DSO_1:241:5|DSO_1:242:5|DSO_1:243:5|DSO_1:244:5|DSO_1:245:5|DSO_1:246:5|DSO_1:247:5|DSO_1:248:5|DSO_1:249:5|DSO_1:250:5|DSO_1:251:5|DSO_1:252:5|DSO_1:253:5|DSO_1:254:5|DSO_1:255:5
int doDSO_Close ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->CloseDso(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:6|DSO_1:1:6|DSO_1:2:6|DSO_1:3:6|DSO_1:4:6|DSO_1:5:6|DSO_1:6:6|DSO_1:7:6|DSO_1:11:6|DSO_1:12:6|DSO_1:13:6|DSO_1:14:6|DSO_1:101:6|DSO_1:102:6|DSO_1:103:6|DSO_1:104:6|DSO_1:105:6|DSO_1:106:6|DSO_1:107:6|DSO_1:108:6|DSO_1:109:6|DSO_1:110:6|DSO_1:111:6|DSO_1:112:6|DSO_1:113:6|DSO_1:114:6|DSO_1:115:6|DSO_1:116:6|DSO_1:117:6|DSO_1:118:6|DSO_1:119:6|DSO_1:120:6|DSO_1:121:6|DSO_1:122:6|DSO_1:123:6|DSO_1:124:6|DSO_1:125:6|DSO_1:126:6|DSO_1:127:6|DSO_1:128:6|DSO_1:129:6|DSO_1:132:6|DSO_1:133:6|DSO_1:134:6|DSO_1:135:6|DSO_1:136:6|DSO_1:137:6|DSO_1:138:6|DSO_1:139:6|DSO_1:140:6|DSO_1:142:6|DSO_1:143:6|DSO_1:144:6|DSO_1:145:6|DSO_1:146:6|DSO_1:147:6|DSO_1:148:6|DSO_1:149:6|DSO_1:150:6|DSO_1:152:6|DSO_1:153:6|DSO_1:154:6|DSO_1:155:6|DSO_1:156:6|DSO_1:157:6|DSO_1:158:6|DSO_1:159:6|DSO_1:160:6|DSO_1:161:6|DSO_1:162:6|DSO_1:163:6|DSO_1:201:6|DSO_1:202:6|DSO_1:203:6|DSO_1:204:6|DSO_1:205:6|DSO_1:206:6|DSO_1:207:6|DSO_1:208:6|DSO_1:209:6|DSO_1:210:6|DSO_1:211:6|DSO_1:212:6|DSO_1:213:6|DSO_1:214:6|DSO_1:215:6|DSO_1:216:6|DSO_1:217:6|DSO_1:218:6|DSO_1:219:6|DSO_1:220:6|DSO_1:221:6|DSO_1:222:6|DSO_1:223:6|DSO_1:224:6|DSO_1:225:6|DSO_1:226:6|DSO_1:227:6|DSO_1:228:6|DSO_1:229:6|DSO_1:230:6|DSO_1:232:6|DSO_1:233:6|DSO_1:234:6|DSO_1:235:6|DSO_1:236:6|DSO_1:237:6|DSO_1:238:6|DSO_1:239:6|DSO_1:240:6|DSO_1:241:6|DSO_1:242:6|DSO_1:243:6|DSO_1:244:6|DSO_1:245:6|DSO_1:246:6|DSO_1:247:6|DSO_1:248:6|DSO_1:249:6|DSO_1:250:6|DSO_1:251:6|DSO_1:252:6|DSO_1:253:6|DSO_1:254:6|DSO_1:255:6
int doDSO_Open ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    void *DriverClass;
    int Fnc;
    int Status;

    Status = cs_GetCurDeviceFnc(s_DevInfo, MAX_DEVICES, &DriverClass, &Fnc);

    // Any Errors already diagnosed
    if(Status == 0)
	{
		((CDso_T *)(DriverClass))->OpenDso(Fnc);
	}

	return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:7|DSO_1:1:7|DSO_1:2:7|DSO_1:3:7|DSO_1:4:7|DSO_1:5:7|DSO_1:6:7|DSO_1:7:7|DSO_1:11:7|DSO_1:12:7|DSO_1:13:7|DSO_1:14:7|DSO_1:101:7|DSO_1:102:7|DSO_1:103:7|DSO_1:104:7|DSO_1:105:7|DSO_1:106:7|DSO_1:107:7|DSO_1:108:7|DSO_1:109:7|DSO_1:110:7|DSO_1:111:7|DSO_1:112:7|DSO_1:113:7|DSO_1:114:7|DSO_1:115:7|DSO_1:116:7|DSO_1:117:7|DSO_1:118:7|DSO_1:119:7|DSO_1:120:7|DSO_1:121:7|DSO_1:122:7|DSO_1:123:7|DSO_1:124:7|DSO_1:125:7|DSO_1:126:7|DSO_1:127:7|DSO_1:128:7|DSO_1:129:7|DSO_1:132:7|DSO_1:133:7|DSO_1:134:7|DSO_1:135:7|DSO_1:136:7|DSO_1:137:7|DSO_1:138:7|DSO_1:139:7|DSO_1:140:7|DSO_1:142:7|DSO_1:143:7|DSO_1:144:7|DSO_1:145:7|DSO_1:146:7|DSO_1:147:7|DSO_1:148:7|DSO_1:149:7|DSO_1:150:7|DSO_1:152:7|DSO_1:153:7|DSO_1:154:7|DSO_1:155:7|DSO_1:156:7|DSO_1:157:7|DSO_1:158:7|DSO_1:159:7|DSO_1:160:7|DSO_1:161:7|DSO_1:162:7|DSO_1:163:7|DSO_1:201:7|DSO_1:202:7|DSO_1:203:7|DSO_1:204:7|DSO_1:205:7|DSO_1:206:7|DSO_1:207:7|DSO_1:208:7|DSO_1:209:7|DSO_1:210:7|DSO_1:211:7|DSO_1:212:7|DSO_1:213:7|DSO_1:214:7|DSO_1:215:7|DSO_1:216:7|DSO_1:217:7|DSO_1:218:7|DSO_1:219:7|DSO_1:220:7|DSO_1:221:7|DSO_1:222:7|DSO_1:223:7|DSO_1:224:7|DSO_1:225:7|DSO_1:226:7|DSO_1:227:7|DSO_1:228:7|DSO_1:229:7|DSO_1:230:7|DSO_1:232:7|DSO_1:233:7|DSO_1:234:7|DSO_1:235:7|DSO_1:236:7|DSO_1:237:7|DSO_1:238:7|DSO_1:239:7|DSO_1:240:7|DSO_1:241:7|DSO_1:242:7|DSO_1:243:7|DSO_1:244:7|DSO_1:245:7|DSO_1:246:7|DSO_1:247:7|DSO_1:248:7|DSO_1:249:7|DSO_1:250:7|DSO_1:251:7|DSO_1:252:7|DSO_1:253:7|DSO_1:254:7|DSO_1:255:7
int doDSO_Connect ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    cs_DoSwitching(M_PATH);

    return (int) 0;
}

//BEGIN{DFW}:DSO_1:0:8|DSO_1:1:8|DSO_1:2:8|DSO_1:3:8|DSO_1:4:8|DSO_1:5:8|DSO_1:6:8|DSO_1:7:8|DSO_1:11:8|DSO_1:12:8|DSO_1:13:8|DSO_1:14:8|DSO_1:101:8|DSO_1:102:8|DSO_1:103:8|DSO_1:104:8|DSO_1:105:8|DSO_1:106:8|DSO_1:107:8|DSO_1:108:8|DSO_1:109:8|DSO_1:110:8|DSO_1:111:8|DSO_1:112:8|DSO_1:113:8|DSO_1:114:8|DSO_1:115:8|DSO_1:116:8|DSO_1:117:8|DSO_1:118:8|DSO_1:119:8|DSO_1:120:8|DSO_1:121:8|DSO_1:122:8|DSO_1:123:8|DSO_1:124:8|DSO_1:125:8|DSO_1:126:8|DSO_1:127:8|DSO_1:128:8|DSO_1:129:8|DSO_1:132:8|DSO_1:133:8|DSO_1:134:8|DSO_1:135:8|DSO_1:136:8|DSO_1:137:8|DSO_1:138:8|DSO_1:139:8|DSO_1:140:8|DSO_1:142:8|DSO_1:143:8|DSO_1:144:8|DSO_1:145:8|DSO_1:146:8|DSO_1:147:8|DSO_1:148:8|DSO_1:149:8|DSO_1:150:8|DSO_1:152:8|DSO_1:153:8|DSO_1:154:8|DSO_1:155:8|DSO_1:156:8|DSO_1:157:8|DSO_1:158:8|DSO_1:159:8|DSO_1:160:8|DSO_1:161:8|DSO_1:162:8|DSO_1:163:8|DSO_1:201:8|DSO_1:202:8|DSO_1:203:8|DSO_1:204:8|DSO_1:205:8|DSO_1:206:8|DSO_1:207:8|DSO_1:208:8|DSO_1:209:8|DSO_1:210:8|DSO_1:211:8|DSO_1:212:8|DSO_1:213:8|DSO_1:214:8|DSO_1:215:8|DSO_1:216:8|DSO_1:217:8|DSO_1:218:8|DSO_1:219:8|DSO_1:220:8|DSO_1:221:8|DSO_1:222:8|DSO_1:223:8|DSO_1:224:8|DSO_1:225:8|DSO_1:226:8|DSO_1:227:8|DSO_1:228:8|DSO_1:229:8|DSO_1:230:8|DSO_1:232:8|DSO_1:233:8|DSO_1:234:8|DSO_1:235:8|DSO_1:236:8|DSO_1:237:8|DSO_1:238:8|DSO_1:239:8|DSO_1:240:8|DSO_1:241:8|DSO_1:242:8|DSO_1:243:8|DSO_1:244:8|DSO_1:245:8|DSO_1:246:8|DSO_1:247:8|DSO_1:248:8|DSO_1:249:8|DSO_1:250:8|DSO_1:251:8|DSO_1:252:8|DSO_1:253:8|DSO_1:254:8|DSO_1:255:8
int doDSO_Disconnect ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
    cs_DoSwitching(M_PATH);

	return (int) 0;
}


//BEGIN{DFW}:DSO_1:0:9|DSO_1:1:9|DSO_1:2:9|DSO_1:3:9|DSO_1:4:9|DSO_1:5:9|DSO_1:6:9|DSO_1:7:9|DSO_1:11:9|DSO_1:12:9|DSO_1:13:9|DSO_1:14:9|DSO_1:101:9|DSO_1:102:9|DSO_1:103:9|DSO_1:104:9|DSO_1:105:9|DSO_1:106:9|DSO_1:107:9|DSO_1:108:9|DSO_1:109:9|DSO_1:110:9|DSO_1:111:9|DSO_1:112:9|DSO_1:113:9|DSO_1:114:9|DSO_1:115:9|DSO_1:116:9|DSO_1:117:9|DSO_1:118:9|DSO_1:119:9|DSO_1:120:9|DSO_1:121:9|DSO_1:122:9|DSO_1:123:9|DSO_1:124:9|DSO_1:125:9|DSO_1:126:9|DSO_1:127:9|DSO_1:128:9|DSO_1:129:9|DSO_1:132:9|DSO_1:133:9|DSO_1:134:9|DSO_1:135:9|DSO_1:136:9|DSO_1:137:9|DSO_1:138:9|DSO_1:139:9|DSO_1:140:9|DSO_1:142:9|DSO_1:143:9|DSO_1:144:9|DSO_1:145:9|DSO_1:146:9|DSO_1:147:9|DSO_1:148:9|DSO_1:149:9|DSO_1:150:9|DSO_1:152:9|DSO_1:153:9|DSO_1:154:9|DSO_1:155:9|DSO_1:156:9|DSO_1:157:9|DSO_1:158:9|DSO_1:159:9|DSO_1:160:9|DSO_1:161:9|DSO_1:162:9|DSO_1:163:9|DSO_1:201:9|DSO_1:202:9|DSO_1:203:9|DSO_1:204:9|DSO_1:205:9|DSO_1:206:9|DSO_1:207:9|DSO_1:208:9|DSO_1:209:9|DSO_1:210:9|DSO_1:211:9|DSO_1:212:9|DSO_1:213:9|DSO_1:214:9|DSO_1:215:9|DSO_1:216:9|DSO_1:217:9|DSO_1:218:9|DSO_1:219:9|DSO_1:220:9|DSO_1:221:9|DSO_1:222:9|DSO_1:223:9|DSO_1:224:9|DSO_1:225:9|DSO_1:226:9|DSO_1:227:9|DSO_1:228:9|DSO_1:229:9|DSO_1:230:9|DSO_1:232:9|DSO_1:233:9|DSO_1:234:9|DSO_1:235:9|DSO_1:236:9|DSO_1:237:9|DSO_1:238:9|DSO_1:239:9|DSO_1:240:9|DSO_1:241:9|DSO_1:242:9|DSO_1:243:9|DSO_1:244:9|DSO_1:245:9|DSO_1:246:9|DSO_1:247:9|DSO_1:248:9|DSO_1:249:9|DSO_1:250:9|DSO_1:251:9|DSO_1:252:9|DSO_1:253:9|DSO_1:254:9|DSO_1:255:9
int doDSO_Load ()
	// Set the return to a non-negative integer to report a success status.
	// Set the return to a negative integer to report an error status.
//END{DFW}
{
	return (int) 0;
}


//++++/////////////////////////////////////////////////////////////////////////
// Local Static Functions
///////////////////////////////////////////////////////////////////////////////


