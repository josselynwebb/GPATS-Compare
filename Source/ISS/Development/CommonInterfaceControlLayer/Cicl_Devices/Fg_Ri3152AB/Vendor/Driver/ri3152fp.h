/**************************************************************************/
/* LabWindows/CVI User Interface Resource (UIR) Include File              */
/* Copyright (c) National Instruments 2003. All Rights Reserved.          */
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
#define  ABOUT_EXIT                      2       /* callback function: exit_about_panel */
#define  ABOUT_FWREV                     3
#define  ABOUT_RACAL_TITLE               4
#define  ABOUT_PICTURE                   5
#define  ABOUT_PRESS_TEXTMSG             6
#define  ABOUT_ABOUT_TEXTMSG             7
#define  ABOUT_ABOUT_TEXTMSG_5           8
#define  ABOUT_ARBITRARYWAVEFORMGENE     9
#define  ABOUT_PICTURE_2                 10
#define  ABOUT_ABOUT_TEXTMSG_2           11
#define  ABOUT_ABOUT3152                 12
#define  ABOUT_ABOUT_TEXTMSG_3           13

#define  AM                              2
#define  AM_DONE                         2       /* callback function: exit_am_panel */
#define  AM_AM_FREQUENCY                 3
#define  AM_APPLY                        4       /* callback function: amp_modulation */
#define  AM_AM_PERCENT                   5
#define  AM_TEXTMSG_3                    6
#define  AM_RACAL_TITLE                  7
#define  AM_PICTURE                      8
#define  AM_TEXTMSG_2                    9

#define  ARBITRARY                       3
#define  ARBITRARY_CHOOSE                2       /* callback function: choose_ascii_file */
#define  ARBITRARY_DONE                  3       /* callback function: exit_arb_panel */
#define  ARBITRARY_CLOCK_SOURCE          4
#define  ARBITRARY_APPLY                 5       /* callback function: send_arb_segment */
#define  ARBITRARY_SEGMENT_NUM           6
#define  ARBITRARY_CLOCK                 7
#define  ARBITRARY_AMPLITUDE             8
#define  ARBITRARY_OFFSET                9
#define  ARBITRARY_FILENAME              10
#define  ARBITRARY_DELETE_SEG            11      /* callback function: select_delete_segments */
#define  ARBITRARY_TEXTMSG_3             12
#define  ARBITRARY_RACAL_TITLE           13
#define  ARBITRARY_PICTURE               14
#define  ARBITRARY_TEXTMSG_2             15
#define  ARBITRARY_DECORATION            16

#define  DC                              4
#define  DC_DONE                         2       /* callback function: exit_dc_panel */
#define  DC_AMP_LEVEL                    3
#define  DC_APPLY                        4       /* callback function: dc_signal */
#define  DC_TEXTMSG_3                    5
#define  DC_RACAL_TITLE                  6
#define  DC_PICTURE                      7
#define  DC_TEXTMSG_2                    8

#define  DELETE                          5
#define  DELETE_DONE                     2       /* callback function: exit_delete_panel */
#define  DELETE_SEGMENT_NUM              3
#define  DELETE_DELETE_ALL               4       /* callback function: delete_all_segments */
#define  DELETE_APPLY                    5       /* callback function: delete_segments */
#define  DELETE_TEXTMSG_3                6
#define  DELETE_PICTURE                  7
#define  DELETE_RACAL_TITLE              8
#define  DELETE_TEXTMSG_2                9

#define  EXPONENT                        6
#define  EXPONENT_DONE                   2       /* callback function: exit_exponential_panel */
#define  EXPONENT_TIME_CONSTANT          3
#define  EXPONENT_APPLY                  4       /* callback function: exponential_waveform */
#define  EXPONENT_AMPLITUDE              5
#define  EXPONENT_FREQUENCY              6
#define  EXPONENT_OFFSET                 7
#define  EXPONENT_TEXTMSG_3              8
#define  EXPONENT_RACAL_TITLE            9
#define  EXPONENT_PICTURE                10
#define  EXPONENT_TEXTMSG_2              11

#define  GAUSS                           7
#define  GAUSS_DONE                      2       /* callback function: exit_gauss_panel */
#define  GAUSS_TIME_CONSTANT             3
#define  GAUSS_APPLY                     4       /* callback function: gauss_waveform */
#define  GAUSS_AMPLITUDE                 5
#define  GAUSS_FREQUENCY                 6
#define  GAUSS_OFFSET                    7
#define  GAUSS_TEXTMSG_3                 8
#define  GAUSS_RACAL_TITLE               9
#define  GAUSS_PICTURE                   10
#define  GAUSS_TEXTMSG_2                 11

#define  PANEL                           8
#define  PANEL_QUIT                      2       /* callback function: quit */
#define  PANEL_FILTER                    3       /* callback function: filter */
#define  PANEL_RESET                     4       /* callback function: reset */
#define  PANEL_ABOUT                     5       /* callback function: about */
#define  PANEL_SELECT_MODE               6       /* callback function: select_mode */
#define  PANEL_WAVEFORM                  7       /* callback function: select_waveform */
#define  PANEL_AMP_MODULATION            8       /* callback function: select_amp_modulation */
#define  PANEL_LAMP                      9
#define  PANEL_SLOT                      10
#define  PANEL_SYNC_SWITCH               11      /* callback function: sync_switch */
#define  PANEL_MODE_SWITCH               12      /* callback function: mode_switch */
#define  PANEL_OUTPUT_SWITCH             13      /* callback function: output_switch */
#define  PANEL_PLL                       14      /* callback function: phase_lock_loop */
#define  PANEL_WAVECAD                   15      /* callback function: wavecad_file_support */
#define  PANEL_PARAMS                    16      /* callback function: set_freq_ampl_offset */
#define  PANEL_MODEL                     17
#define  PANEL_PICTURE                   18
#define  PANEL_TEXTMSG_2                 19
#define  PANEL_RACAL_TITLE               20
#define  PANEL_TEXTMSG                   21

#define  PARAMS                          9
#define  PARAMS_DONE                     2       /* callback function: exit_params_panel */
#define  PARAMS_FREQUENCY                3
#define  PARAMS_APPLY                    4       /* callback function: apply_parameters */
#define  PARAMS_AMPLITUDE                5
#define  PARAMS_OFFSET                   6
#define  PARAMS_RACAL_TITLE              7
#define  PARAMS_PICTURE                  8
#define  PARAMS_TEXTMSG_3                9
#define  PARAMS_TEXTMSG_2                10

#define  PLL                             10
#define  PLL_DONE                        2       /* callback function: exit_pll_panel */
#define  PLL_APPLY                       3       /* callback function: apply_pll */
#define  PLL_FINE_PHASE                  4
#define  PLL_COARSE_PHASE                5
#define  PLL_ON_OFF                      6       /* callback function: pll_switch */
#define  PLL_INDICATE_FINE               7
#define  PLL_INDICATE_FREQ               8
#define  PLL_INDICATE_COARSE             9
#define  PLL_RACAL_TITLE                 10
#define  PLL_PICTURE                     11
#define  PLL_TEXTMSG_3                   12
#define  PLL_TEXTMSG_2                   13

#define  PULSE                           11
#define  PULSE_DONE                      2       /* callback function: exit_pulse_panel */
#define  PULSE_FALL_TIME                 3
#define  PULSE_APPLY                     4       /* callback function: pulse_waveform */
#define  PULSE_RISE_TIME                 5
#define  PULSE_FREQUENCY                 6
#define  PULSE_OFFSET                    7
#define  PULSE_AMPLITUDE                 8
#define  PULSE_HIGH_TIME                 9
#define  PULSE_DELAY_TIME                10
#define  PULSE_TEXTMSG_3                 11
#define  PULSE_RACAL_TITLE               12
#define  PULSE_PICTURE                   13
#define  PULSE_TEXTMSG_2                 14

#define  RAMP                            12
#define  RAMP_FALL_TIME                  2
#define  RAMP_DONE                       3       /* callback function: exit_ramp_panel */
#define  RAMP_APPLY                      4       /* callback function: ramp_waveform */
#define  RAMP_RISE_TIME                  5
#define  RAMP_FREQUENCY                  6
#define  RAMP_DELAY_TIME                 7
#define  RAMP_OFFSET                     8
#define  RAMP_AMPLITUDE                  9
#define  RAMP_TEXTMSG_3                  10
#define  RAMP_RACAL_TITLE                11
#define  RAMP_PICTURE                    12
#define  RAMP_TEXTMSG_2                  13

#define  SELECT                          13
#define  SELECT_CHECKBOX_8               2
#define  SELECT_CHECKBOX_7               3
#define  SELECT_CHECKBOX_6               4
#define  SELECT_CHECKBOX_5               5
#define  SELECT_CHECKBOX_4               6
#define  SELECT_CHECKBOX_3               7
#define  SELECT_CHECKBOX_2               8
#define  SELECT_CHECKBOX_1               9
#define  SELECT_CONTROLLER_8             10
#define  SELECT_CONTROLLER_7             11
#define  SELECT_CONTROLLER_6             12
#define  SELECT_CONTROLLER_5             13
#define  SELECT_CONTROLLER_4             14
#define  SELECT_CONTROLLER_3             15
#define  SELECT_CONTROLLER_2             16
#define  SELECT_SLOT_8                   17
#define  SELECT_SLOT_7                   18
#define  SELECT_SLOT_6                   19
#define  SELECT_SLOT_5                   20
#define  SELECT_SLOT_4                   21
#define  SELECT_SLOT_3                   22
#define  SELECT_SLOT_2                   23
#define  SELECT_LOGADDR_8                24
#define  SELECT_LOGADDR_7                25
#define  SELECT_LOGADDR_6                26
#define  SELECT_LOGADDR_5                27
#define  SELECT_LOGADDR_4                28
#define  SELECT_LOGADDR_3                29
#define  SELECT_LOGADDR_2                30
#define  SELECT_LOGADDR_1                31
#define  SELECT_SLOT_1                   32
#define  SELECT_CONTROLLER_1             33
#define  SELECT_RACAL_TITLE              34
#define  SELECT_PICTURE                  35
#define  SELECT_TEXTMSG_CONTROLLER       36
#define  SELECT_TEXTMSG_SLOT             37
#define  SELECT_TEXTMSG_LOGADDR          38
#define  SELECT_TEXTMSG_2                39
#define  SELECT_TEXTMSG_3                40
#define  SELECT_TEXTMSG                  41

#define  SINC                            14
#define  SINC_DONE                       2       /* callback function: exit_sinc_panel */
#define  SINC_CYCLE_NUMBER               3
#define  SINC_APPLY                      4       /* callback function: sinc_waveform */
#define  SINC_AMPLITUDE                  5
#define  SINC_FREQUENCY                  6
#define  SINC_OFFSET                     7
#define  SINC_TEXTMSG_3                  8
#define  SINC_RACAL_TITLE                9
#define  SINC_PICTURE                    10
#define  SINC_TEXTMSG_2                  11

#define  SINE                            15
#define  SINE_DONE                       2       /* callback function: exit_sine_panel */
#define  SINE_PWR_SINE                   3
#define  SINE_APPLY                      4       /* callback function: sine_waveform */
#define  SINE_PHASE                      5
#define  SINE_FREQUENCY                  6
#define  SINE_OFFSET                     7
#define  SINE_AMPLITUDE                  8
#define  SINE_TEXTMSG_3                  9
#define  SINE_RACAL_TITLE                10
#define  SINE_PICTURE                    11
#define  SINE_TEXTMSG_2                  12

#define  SQUARE                          16
#define  SQUARE_DONE                     2       /* callback function: exit_square_panel */
#define  SQUARE_FREQUENCY                3
#define  SQUARE_APPLY                    4       /* callback function: square_waveform */
#define  SQUARE_AMPLITUDE                5
#define  SQUARE_OFFSET                   6
#define  SQUARE_DUTY_CYCLE               7
#define  SQUARE_TEXTMSG_3                8
#define  SQUARE_RACAL_TITLE              9
#define  SQUARE_PICTURE                  10
#define  SQUARE_TEXTMSG_2                11

#define  STANDARD                        17
#define  STANDARD_DONE                   2       /* callback function: exit_std_panel */
#define  STANDARD_SELECT_STD             3
#define  STANDARD_APPLY                  4       /* callback function: select_std */
#define  STANDARD_TEXTMSG_3              5
#define  STANDARD_PICTURE                6
#define  STANDARD_RACAL_TITLE            7
#define  STANDARD_TEXTMSG_2              8

#define  SWEEP                           18
#define  SWEEP_DONE                      2       /* callback function: exit_sweep_panel */
#define  SWEEP_SPACING                   3
#define  SWEEP_DIRECTION                 4
#define  SWEEP_APPLY                     5       /* callback function: apply_sweep */
#define  SWEEP_FUNCTION                  6
#define  SWEEP_STEP                      7
#define  SWEEP_MARKER                    8
#define  SWEEP_S_CLOCK                   9
#define  SWEEP_STOP_FREQ                 10
#define  SWEEP_START_FREQ                11
#define  SWEEP_TIME                      12
#define  SWEEP_RACAL_TITLE               13
#define  SWEEP_PICTURE                   14
#define  SWEEP_TEXTMSG_2                 15
#define  SWEEP_TEXTMSG_3                 16

#define  SYNC                            19
#define  SYNC_SYNCWIDT                   2
#define  SYNC_SYNCPOS                    3
#define  SYNC_DONE                       4       /* callback function: exit_sync_panel */
#define  SYNC_APPLY                      5       /* callback function: apply_sync */
#define  SYNC_SYNCSRC                    6
#define  SYNC_RACAL_TITLE                7
#define  SYNC_PICTURE                    8
#define  SYNC_TEXTMSG_2                  9
#define  SYNC_TEXTMSG_3                  10

#define  TRIANGLE                        20
#define  TRIANGLE_DONE                   2       /* callback function: exit_triangle_panel */
#define  TRIANGLE_PWR_TRIANGLE           3
#define  TRIANGLE_APPLY                  4       /* callback function: triangle_waveform */
#define  TRIANGLE_PHASE                  5
#define  TRIANGLE_FREQUENCY              6
#define  TRIANGLE_OFFSET                 7
#define  TRIANGLE_AMPLITUDE              8
#define  TRIANGLE_TEXTMSG_3              9
#define  TRIANGLE_RACAL_TITLE            10
#define  TRIANGLE_PICTURE                11
#define  TRIANGLE_TEXTMSG_2              12

#define  TRIGGER                         21
#define  TRIGGER_SOURCE                  2       /* callback function: trigger_mode */
#define  TRIGGER_BURSTCYCLE              3       /* callback function: set_burst_cycles */
#define  TRIGGER_DONE                    4       /* callback function: exit_trigger_panel */
#define  TRIGGER_INTERNAL                5       /* callback function: internal_trigger */
#define  TRIGGER_LEVEL                   6       /* callback function: trigger_level */
#define  TRIGGER_RATE                    7
#define  TRIGGER_TEXTMSG_3               8
#define  TRIGGER_RACAL_TITLE             9
#define  TRIGGER_PICTURE                 10
#define  TRIGGER_TEXTMSG_2               11

#define  WAVECAD                         22
#define  WAVECAD_CHOOSE                  2       /* callback function: choose_wavecad_file */
#define  WAVECAD_DONE                    3       /* callback function: exit_wavecad_panel */
#define  WAVECAD_CLOCK_SOURCE            4
#define  WAVECAD_APPLY                   5       /* callback function: apply_wavecad_file */
#define  WAVECAD_SEGMENT_NUM             6
#define  WAVECAD_CLOCK                   7
#define  WAVECAD_AMPLITUDE               8
#define  WAVECAD_OFFSET                  9
#define  WAVECAD_FILENAME                10
#define  WAVECAD_RACAL_TITLE             11
#define  WAVECAD_PICTURE                 12
#define  WAVECAD_TEXTMSG_3               13
#define  WAVECAD_DECORATION              14
#define  WAVECAD_TEXTMSG_2               15


     /* Menu Bars, Menus, and Menu Items: */

          /* (no menu bars in the resource file) */


     /* Callback Prototypes: */ 

int  CVICALLBACK about(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK amp_modulation(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK apply_parameters(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK apply_pll(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK apply_sweep(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK apply_sync(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK apply_wavecad_file(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK choose_ascii_file(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK choose_wavecad_file(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK dc_signal(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK delete_all_segments(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK delete_segments(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_about_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_am_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_arb_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_dc_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_delete_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_exponential_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_gauss_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_params_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_pll_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_pulse_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_ramp_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_sinc_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_sine_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_square_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_std_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_sweep_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_sync_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_triangle_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_trigger_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exit_wavecad_panel(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK exponential_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK filter(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK gauss_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK internal_trigger(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK mode_switch(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK output_switch(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK phase_lock_loop(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK pll_switch(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK pulse_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK quit(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK ramp_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK reset(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK select_amp_modulation(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK select_delete_segments(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK select_mode(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK select_std(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK select_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK send_arb_segment(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK set_burst_cycles(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK set_freq_ampl_offset(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK sinc_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK sine_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK square_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK sync_switch(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK triangle_waveform(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK trigger_level(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK trigger_mode(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);
int  CVICALLBACK wavecad_file_support(int panel, int control, int event, void *callbackData, int eventData1, int eventData2);


#ifdef __cplusplus
    }
#endif
