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

#define  DSCHPANEL                       1
#define  DSCHPANEL_DS_ROT_STOP_ANG       2       /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_REF_SOURCE         3       /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_ROT_RATE           4       /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_CHAN               5
#define  DSCHPANEL_DS_TRIG_SOURCE        6       /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_TRIG_SLOPE         7       /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_ROT_MODE           8       /* callback function: SET_DS_ALL */
#define  DSCHPANEL_ROTATEDSBUTTON        9       /* callback function: ROT_INIT */
#define  DSCHPANEL_DS_REF_SOURCE_BOX     10
#define  DSCHPANEL_DS_TRIG_SOURCE_BOX    11
#define  DSCHPANEL_DS_ROT_STOP_ANG_BOX   12
#define  DSCHPANEL_DS_TRIG_SLOPE_BOX     13
#define  DSCHPANEL_DS_ROT_RATE_BOX       14
#define  DSCHPANEL_DS_ROT_MODE_BOX       15
#define  DSCHPANEL_DS_ANGLE_8            16
#define  DSCHPANEL_DS_ANGLE_7            17
#define  DSCHPANEL_DS_ANGLE_6            18
#define  DSCHPANEL_DS_ANGLE_5            19
#define  DSCHPANEL_DS_ANGLE_4            20
#define  DSCHPANEL_DS_ANGLE_3            21
#define  DSCHPANEL_DS_ANGLE_2            22
#define  DSCHPANEL_DS_ANGLE_1            23
#define  DSCHPANEL_DS_IO_8               24      /* callback function: SetDSio */
#define  DSCHPANEL_DS_PH_BOX             25
#define  DSCHPANEL_DS_IO_7               26      /* callback function: SetDSio */
#define  DSCHPANEL_DS_LTL_BOX            27
#define  DSCHPANEL_DS_IO_6               28      /* callback function: SetDSio */
#define  DSCHPANEL_WRT_8                 29      /* callback function: WrtDS */
#define  DSCHPANEL_DS_RATIO_BOX          30
#define  DSCHPANEL_DS_IO_5               31      /* callback function: SetDSio */
#define  DSCHPANEL_WRT_7                 32      /* callback function: WrtDS */
#define  DSCHPANEL_DS_REF_BOX            33
#define  DSCHPANEL_DS_IO_4               34      /* callback function: SetDSio */
#define  DSCHPANEL_WRT_6                 35      /* callback function: WrtDS */
#define  DSCHPANEL_DS_DC_BOX             36
#define  DSCHPANEL_DS_IO_3               37      /* callback function: SetDSio */
#define  DSCHPANEL_WRT_5                 38      /* callback function: WrtDS */
#define  DSCHPANEL_RD_DS_BUTTON          39      /* callback function: RD_DS_ALL */
#define  DSCHPANEL_DS_IO_2               40      /* callback function: SetDSio */
#define  DSCHPANEL_WRT_4                 41      /* callback function: WrtDS */
#define  DSCHPANEL_DS_MODE_BOX           42
#define  DSCHPANEL_DS_IO_1               43      /* callback function: SetDSio */
#define  DSCHPANEL_WRT_3                 44      /* callback function: WrtDS */
#define  DSCHPANEL_DS_DC                 45      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_APPLYDSBUTTON         46      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_WRT_2                 47      /* callback function: WrtDS */
#define  DSCHPANEL_DS_ROT_DONE_BOX       48
#define  DSCHPANEL_DS_ANG_BOX            49
#define  DSCHPANEL_WRT_1                 50      /* callback function: WrtDS */
#define  DSCHPANEL_DS_RATIO              51      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_IO_BOX             52
#define  DSCHPANEL_DECORATION            53
#define  DSCHPANEL_DS_REF                54      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_PH                 55      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_LTL                56      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_DS_MODE               57      /* callback function: SET_DS_ALL */
#define  DSCHPANEL_TEXTMSG_2             58
#define  DSCHPANEL_TEXTMSG               59

#define  na65CS                          2
#define  na65CS_RD_DUT                   2       /* callback function: RdDut */
#define  na65CS_WRT_DUT                  3       /* callback function: WrtDut */
#define  na65CS_LA                       4
#define  na65CS_SLOT                     5
#define  na65CS_DEMO_LED                 6
#define  na65CS_CONNECT_LED              7
#define  na65CS_ABOUT                    8       /* callback function: about */
#define  na65CS_RESET                    9       /* callback function: reset */
#define  na65CS_ATLAS                    10      /* callback function: Atlas */
#define  na65CS_FRESH_START              11      /* callback function: FreshStart */
#define  na65CS_IDN                      12      /* callback function: idn */
#define  na65CS_HELP                     13      /* callback function: getSRHelp */
#define  na65CS_STAT_REQ                 14      /* callback function: getStatus */
#define  na65CS_SELFTEST                 15      /* callback function: selftest */
#define  na65CS_QUIT                     16      /* callback function: quit */
#define  na65CS_MODEL                    17
#define  na65CS_WRT_MSG                  18
#define  na65CS_RD_MSG                   19
#define  na65CS_TEXTMSG_4                20
#define  na65CS_PICTURE                  21
#define  na65CS_PICTURE_2                22
#define  na65CS_TEXTMSG_27               23
#define  na65CS_TEXTMSG_5                24
#define  na65CS_DECORATION_4             25
#define  na65CS_TEXTMSG_3                26
#define  na65CS_TEXTMSG_6                27
#define  na65CS_ID_MESSAGE               28
#define  na65CS_DECORATION_2             29
#define  na65CS_DECORATION_7             30
#define  na65CS_TEXTMSG_7                31
#define  na65CS_DECORATION_8             32
#define  na65CS_SELECTION_TREE           33      /* callback function: SelectionTree */
#define  na65CS_PART_SPEC_RING           34      /* callback function: PartSpecSelect */
#define  na65CS_SD_CHAN_CNT              35
#define  na65CS_DSO_CHAN_CNT             36
#define  na65CS_DSH_CHAN_CNT             37
#define  na65CS_REF_CHAN_CNT             38
#define  na65CS_DUT_FAMILY_RING          39      /* callback function: DutFamilySelect */
#define  na65CS_DECORATION               40

#define  REFCHPANEL                      3
#define  REFCHPANEL_REF_IO_4             2       /* callback function: SetREFio */
#define  REFCHPANEL_REF_VOLT_4           3
#define  REFCHPANEL_REF_IO_3             4       /* callback function: SetREFio */
#define  REFCHPANEL_REF_CHAN             5
#define  REFCHPANEL_REF_FREQ_4           6
#define  REFCHPANEL_REF_FREQ_LBL_4       7
#define  REFCHPANEL_REF_LBL_4            8
#define  REFCHPANEL_REF_VOLT_LBL_4       9
#define  REFCHPANEL_REF_WRITE_4          10      /* callback function: SetRef */
#define  REFCHPANEL_REF_VOLT_3           11
#define  REFCHPANEL_RD_REF_BUTTON        12      /* callback function: RD_REF_ALL */
#define  REFCHPANEL_REF_FREQ_3           13
#define  REFCHPANEL_REF_FREQ_LBL_3       14
#define  REFCHPANEL_REF_LBL_3            15
#define  REFCHPANEL_REF_VOLT_LBL_3       16
#define  REFCHPANEL_REF_WRITE_3          17      /* callback function: SetRef */
#define  REFCHPANEL_REF_IO_2             18      /* callback function: SetREFio */
#define  REFCHPANEL_REF_VOLT_2           19
#define  REFCHPANEL_REF_FREQ_2           20
#define  REFCHPANEL_REF_FREQ_LBL_2       21
#define  REFCHPANEL_REF_LBL_2            22
#define  REFCHPANEL_REF_VOLT_LBL_2       23
#define  REFCHPANEL_REF_FRQ_BOX          24
#define  REFCHPANEL_REF_WRITE_2          25      /* callback function: SetRef */
#define  REFCHPANEL_REF_IO_1             26      /* callback function: SetREFio */
#define  REFCHPANEL_REF_VOLT_1           27
#define  REFCHPANEL_REF_FREQ_1           28
#define  REFCHPANEL_REF_FREQ_LBL_1       29
#define  REFCHPANEL_REF_VOLT_BOX         30
#define  REFCHPANEL_REF_LBL_1            31
#define  REFCHPANEL_REF_VOLT_LBL_1       32
#define  REFCHPANEL_REF_WRITE_1          33      /* callback function: SetRef */
#define  REFCHPANEL_REF_IO_BOX           34
#define  REFCHPANEL_DECORATION           35

#define  SDCHPANEL                       4
#define  SDCHPANEL_SD_MODE               2       /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_REF_SOURCE         3       /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_BW                 4       /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_TRACK              5       /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_CHAN               6
#define  SDCHPANEL_APPLYSDBUTTON         7       /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_REF_SOURCE_BOX     8
#define  SDCHPANEL_SD_ANGLE_8            9
#define  SDCHPANEL_SD_ANGLE_7            10
#define  SDCHPANEL_SD_ANGLE_6            11
#define  SDCHPANEL_SD_ANGLE_5            12
#define  SDCHPANEL_SD_ANGLE_4            13
#define  SDCHPANEL_SD_ANGLE_3            14
#define  SDCHPANEL_SD_ANGLE_2            15
#define  SDCHPANEL_SD_ANGLE_1            16
#define  SDCHPANEL_SD_BW_BOX             17
#define  SDCHPANEL_RD_SD_BUTTON          18      /* callback function: RD_SD_ALL */
#define  SDCHPANEL_SD_TRACK_BOX          19
#define  SDCHPANEL_SD_MODE_BOX           20
#define  SDCHPANEL_SD_RATIO_BOX          21
#define  SDCHPANEL_SD_MAXT_BOX           22
#define  SDCHPANEL_SD_DC_BOX             23
#define  SDCHPANEL_SD_VEL_BOX            24
#define  SDCHPANEL_SD_IO_BOX             25
#define  SDCHPANEL_SD_DC                 26      /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_MAXT               27      /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_IO_8               28      /* callback function: SetSDio */
#define  SDCHPANEL_SD_RATIO              29      /* callback function: SET_SD_ALL */
#define  SDCHPANEL_SD_IO_7               30      /* callback function: SetSDio */
#define  SDCHPANEL_RD_8                  31      /* callback function: RdSD */
#define  SDCHPANEL_SD_IO_6               32      /* callback function: SetSDio */
#define  SDCHPANEL_RD_7                  33      /* callback function: RdSD */
#define  SDCHPANEL_SD_IO_5               34      /* callback function: SetSDio */
#define  SDCHPANEL_RD_6                  35      /* callback function: RdSD */
#define  SDCHPANEL_SD_IO_4               36      /* callback function: SetSDio */
#define  SDCHPANEL_RD_5                  37      /* callback function: RdSD */
#define  SDCHPANEL_SD_IO_3               38      /* callback function: SetSDio */
#define  SDCHPANEL_RD_4                  39      /* callback function: RdSD */
#define  SDCHPANEL_SD_IO_2               40      /* callback function: SetSDio */
#define  SDCHPANEL_RD_3                  41      /* callback function: RdSD */
#define  SDCHPANEL_SD_IO_1               42      /* callback function: SetSDio */
#define  SDCHPANEL_RD_2                  43      /* callback function: RdSD */
#define  SDCHPANEL_RD_1                  44      /* callback function: RdSD */
#define  SDCHPANEL_DECORATION            45


     /* Menu Bars, Menus, and Menu Items: */

          /* (no menu bars in the resource file) */


     /* Callback Prototypes: */ 

int  CVICALLBACK about(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK Atlas(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK DutFamilySelect(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK FreshStart(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK getSRHelp(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK getStatus(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK idn(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK PartSpecSelect(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK quit(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK RD_DS_ALL(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK RD_REF_ALL(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK RD_SD_ALL(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK RdDut(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK RdSD(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK reset(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK ROT_INIT(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SelectionTree(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK selftest(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SET_DS_ALL(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SET_SD_ALL(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SetDSio(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SetRef(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SetREFio(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK SetSDio(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK WrtDS(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK WrtDut(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);


#ifdef __cplusplus
    }
#endif
