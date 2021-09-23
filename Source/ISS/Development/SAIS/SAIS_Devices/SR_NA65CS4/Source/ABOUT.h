/**************************************************************************/
/* LabWindows/CVI User Interface Resource (UIR) Include File              */
/* Copyright (c) National Instruments 2009. All Rights Reserved.          */
/*                                                                        */
/* WARNING: Do not add to, delete from, or otherwise modify the contents  */
/*          of this include file.                                         */
/**************************************************************************/

#include <userint.h>

#ifdef __cplusplus
    extern "C" {
#endif

     /* Panels and Controls: */

#define  ABOUT                           1
#define  ABOUT_ABOUTOK                   2       /* callback function: about_ok */
#define  ABOUT_TEXTMSG_18                3
#define  ABOUT_PICTURE                   4
#define  ABOUT_TEXTMSG_13                5
#define  ABOUT_TEXTMSG_15                6
#define  ABOUT_TEXTMSG_14                7
#define  ABOUT_TEXTMSG_16                8
#define  ABOUT_TEXTMSG_17                9


     /* Menu Bars, Menus, and Menu Items: */

          /* (no menu bars in the resource file) */


     /* Callback Prototypes: */ 

int  CVICALLBACK about_ok(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);


#ifdef __cplusplus
    }
#endif
