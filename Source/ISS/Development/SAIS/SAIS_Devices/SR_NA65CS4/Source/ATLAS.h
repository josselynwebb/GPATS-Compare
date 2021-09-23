/**************************************************************************/
/* LabWindows/CVI User Interface Resource (UIR) Include File              */
/* Copyright (c) National Instruments 2006. All Rights Reserved.          */
/*                                                                        */
/* WARNING: Do not add to, delete from, or otherwise modify the contents  */
/*          of this include file.                                         */
/**************************************************************************/

#include <userint.h>

#ifdef __cplusplus
    extern "C" {
#endif

     /* Panels and Controls: */

#define  panAtlas                        1
#define  panAtlas_txtATLAS               2
#define  panAtlas_cmdAtlas               3       /* callback function: Generate */
#define  panAtlas_cmdClear               4       /* callback function: ClearMsg */
#define  panAtlas_cmdOpen                5       /* callback function: Open_File */
#define  panAtlas_cmdSave                6       /* callback function: Save_File */
#define  panAtlas_cmdCLOSE               7       /* callback function: Atlas_Close */
#define  panAtlas_OUTPUTTYPE             8


     /* Menu Bars, Menus, and Menu Items: */

          /* (no menu bars in the resource file) */


     /* Callback Prototypes: */ 

int  CVICALLBACK Atlas_Close(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK ClearMsg(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK Generate(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK Open_File(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK Save_File(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);


#ifdef __cplusplus
    }
#endif
