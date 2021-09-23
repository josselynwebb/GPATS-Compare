#include <cvidef.h>
#include "vpptype.h"

/* = zt1428 Include File =============================================== */
/* = GLOBAL CONSTANT DECLARATIONS ========================================== */

#ifndef __zt1428_HEADER
#define __zt1428_HEADER

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/*  Replace 10 with the maximum number of devices of this type being used.   */
#define ZT1428_MAX_INSTR  	10
#define ZT_MAX_WAVE		1000000L
#define ZT_MIN_WAVE		100L
#define ZT_MFG_ID		0xE80
#define ZT_INSTR_ID		1428
#define ZT_DL_SIZE		32000
#define ZT_MEAS_INVALID	9.9999e+37

#define ZT1428_ACQ_RTIME	0 /* Realtime */
#define ZT1428_ACQ_REP		1 /* Repetitive */

#define ZT1428_ACQ_NORM		0 /* Normal */
#define ZT1428_ACQ_AVER 	1 /* Average */
#define ZT1428_ACQ_ENV		2 /* Envelope */

#define ZT1428_ACQ_AUTO		0 /* Auto */
#define ZT1428_ACQ_SING		1 /* Single */
#define ZT1428_ACQ_TRIG		2 /* Triggered */

#define ZT1428_ACQ_LEFT		0 /* Left */
#define ZT1428_ACQ_CENT		1 /* Center */
#define ZT1428_ACQ_RIGHT	2 /* Right */

#define ZT1428_NONE			0 /* No source*/
#define ZT1428_CHAN1		1 /* Channel 1 */
#define ZT1428_CHAN2		2 /* Channel 2 */
#define ZT1428_WMEM1		3 /* Memory 1 */
#define ZT1428_WMEM2		4 /* Memory 2 */
#define ZT1428_WMEM3		5 /* Memory 3 */
#define ZT1428_WMEM4		6 /* Memory 4 */
#define ZT1428_FUNC1		7 /* Function 1 */
#define ZT1428_FUNC2		8 /* Function 2 */
#define ZT1428_CHAN_BOTH	10 /* Channels 1 & 2 */  

#define ZT1428_VERT_COUP_AC		0  /* AC 1MO (10 Hz) */
#define ZT1428_VERT_COUP_ACLFR	1  /* AC 1MO (450 Hz) */
#define ZT1428_VERT_COUP_DC		2  /* DC 1MO */
#define ZT1428_VERT_COUP_DCF	3  /* DC 50O */

#define ZT1428_VERT_FILT_OFF	0  /* Off */
#define ZT1428_VERT_FILT_30MHZ	1  /* 30 MHz Lowpass Filter */
#define ZT1428_VERT_FILT_1MHZ	2  /* 1 MHz Lowpass Filter */

#define ZT1428_LOGIC_TTL 	0	/* TTL Logic */
#define ZT1428_LOGIC_ECL 	1	/* ECL Logic */

#define ZT1428_FUNC_ADD		0  /* Add */
#define ZT1428_FUNC_SUB		1  /* Subtract */
#define ZT1428_FUNC_MULT	2  /* Multiply */
#define ZT1428_FUNC_DIFF	3  /* Difference */
#define ZT1428_FUNC_INT		4  /* Integrate */
#define ZT1428_FUNC_INV		5  /* Invert */
#define ZT1428_FUNC_ONLY	6  /* Only */

#define ZT1428_FUNC_OFF		0 	/*  Function Off */
#define ZT1428_FUNC_ON		1  	/*  Function On */

#define ZT1428_TRG_MODE_EDGE 	0	/* Edge */
#define ZT1428_TRG_MODE_PATT	1	/* Pattern */
#define ZT1428_TRG_MODE_STAT 	2	/* State */
#define ZT1428_TRG_MODE_TV   	3	/* TV */

#define ZT1428_TRG_SENS_NORM	0 	/*  Normal */
#define ZT1428_TRG_SENS_LOW		1  	/*  Low (Noise Reject) */
#define ZT1428_TRG_SENS_LFR		2  	/*  Low Freq Reject */
#define ZT1428_TRG_SENS_HFR		3  	/*  High Freq Reject */

#define ZT1428_TRG_SLOPE_NEG	0 /*  Negative slope */
#define ZT1428_TRG_SLOPE_POS	1 /*  Positive slope */

#define ZT1428_TRG_CHAN1	1 	/*  Chan 1 */
#define ZT1428_TRG_CHAN2	2 	/*  Chan 2 */
#define ZT1428_TRG_EXT		3 	/*  External */
#define ZT1428_TRG_ECL0		4 	/*  ECL 0  */
#define ZT1428_TRG_ECL1		5 	/*  ECL 1  */

#define ZT1428_TRG_HOLD_TIME	0  /*  Time  */
#define ZT1428_TRG_HOLD_EVENT	1  /*  Event  */

#define ZT1428_TRG_PATT_ENTER 	0	/* Enter */
#define ZT1428_TRG_PATT_EXIT  	1	/* Exit  */
#define ZT1428_TRG_PATT_GT    	2	/* Greater Than */
#define ZT1428_TRG_PATT_LT    	3	/* Less Than */
#define ZT1428_TRG_PATT_RANGE 	4	/* Between */

#define ZT1428_TRG_PATT_LOW  	0	/* Low  */
#define ZT1428_TRG_PATT_HIGH 	1	/* High */
#define ZT1428_TRG_PATT_DONT   	2	/* Don't Care */

#define ZT1428_TRG_STAT_FALSE 	0	/* False */
#define ZT1428_TRG_STAT_TRUE  	1	/* True */

#define ZT1428_TRG_TV_STAN_525 	525	/* NTSC 525 */
#define ZT1428_TRG_TV_STAN_625 	625	/* PAL 625 */

#define ZT1428_TRG_TV_FIELD1 	1	/* Field 1 */
#define ZT1428_TRG_TV_FIELD2 	2	/* Field 2 */

#define ZT1428_EXT_MODE_TRIG	0  /*  Trigger (Internal Clock) */
#define ZT1428_EXT_MODE_CLK		1  /*  Clock (External Clock)  */

#define ZT1428_EXT_IMP_1M	0 /*  1MOHm  */
#define ZT1428_EXT_IMP_50	1 /*  50OHm  */

#define ZT1428_INIT_VXI		1  /*  VXI  */
#define ZT1428_INIT_GPIB	2  /*  GPIB  */

#define ZT1428_MEAS_RISE	0 /*  Rise Time */
#define ZT1428_MEAS_FALL	1 /*  Fall Time */
#define ZT1428_MEAS_FREQ	2 /*  Frequency */
#define ZT1428_MEAS_PER		3 /*  Period */
#define ZT1428_MEAS_PWID	4 /*  +Width */
#define ZT1428_MEAS_NWID	5 /*  -Width */
#define ZT1428_MEAS_VAMP	6 /*  V. Amplitude */
#define ZT1428_MEAS_VBAS	7 /*  V. Base */
#define ZT1428_MEAS_VTOP	8 /*  V. Top */
#define ZT1428_MEAS_VPP		9 /*  V. Peak to Peak */
#define ZT1428_MEAS_VAVG	10 /*  V. Average */
#define ZT1428_MEAS_VMAX	11 /*  V. Max */
#define ZT1428_MEAS_VMIN	12 /*  V. Min */
#define ZT1428_MEAS_VACR	13 /*  V. AC(rms) */
#define ZT1428_MEAS_VDCR	14 /*  V. DC(rms) */
#define ZT1428_MEAS_DUTY	15 /*  Duty Cycle */
#define ZT1428_MEAS_DEL		16 /*  Delay */
#define ZT1428_MEAS_OVER	17 /*  Over Shoot */
#define ZT1428_MEAS_PRE 	18 /*  Pre Shoot */
#define ZT1428_MEAS_TMAX 	19 /*  T. Max */
#define ZT1428_MEAS_TMIN	20 /*  T. Min */

#define ZT1428_MEAS_MODE_STAN	0 /*  Standard */
#define ZT1428_MEAS_MODE_USER	1 /*  User */

#define ZT1428_MEAS_USER_PCT 	0 /*  Percent */
#define ZT1428_MEAS_USER_VOLT	1 /*  Volts */

#define ZT1428_MEAS_LIM_OFF 	0	/* Limit Test Off */
#define ZT1428_MEAS_LIM_ON  	1	/* Limit Test On */

#define ZT1428_MEAS_POST_STOP 	0	/* Stop upon Failure */
#define ZT1428_MEAS_POST_CONT  	1	/* Continue upon Failure */

#define ZT1428_MEAS_STAT_OFF 	0	/* Statistics Off */
#define ZT1428_MEAS_STAT_ON  	1	/* Statistics On */

#define ZT1428_MEAS_MASK_OFF 	0	/* Mask Test Off */
#define ZT1428_MEAS_MASK_ON  	1	/* Mask Test On */

#define ZT1428_DEL_SLOP_NEG	0 /*  Negative Slope */
#define ZT1428_DEL_SLOP_POS	1 /*  Positive Slope */

#define ZT1428_DEL_LEV_LOW 	0 /*  Lower  */
#define ZT1428_DEL_LEV_MID	1 /*  Middle  */
#define ZT1428_DEL_LEV_UPP	2 /*  Upper  */

#define ZT1428_DIG_NORM	0 /*  Normal  */
#define ZT1428_DIG_ASYN	1 /*  Asynchronous  */

#define ZT1428_TRAN_SER	0 /*  Serial */
#define ZT1428_TRAN_A32	1 /*  A32 */
#define ZT1428_TRAN_PRE	2 /*  Preamble */

#define ZT1428_OUT_BNC_PROB	0 /*  Probe  */
#define ZT1428_OUT_BNC_TRIG	1 /*  Trigger  */
#define ZT1428_OUT_BNC_DC 	2 /*  DC Calibrate  */
#define ZT1428_OUT_BNC_0V	3 /*  0 Volts  */
#define ZT1428_OUT_BNC_5V	4 /*  5 Volts  */
#define ZT1428_OUT_BNC_SCL	5 /*  Sclock  */

#define ZT1428_OUT_OFF	0 /*  Off */
#define ZT1428_OUT_ON	1 /*  On  */

#define ZT1428_STOP 	0 /* Stop */
#define ZT1428_RUN  	1 /* Run */

#define ZT1428_SAVE 	0 /* Save */
#define ZT1428_RCL  	1 /* Recall */



/*===========================error codes==========================*/
#define	ZT1428_COM_ERR				200
#define	ZT1428_ADV_MODE_ERR			201
#define	ZT1428_ID_ERR				202
#define	ZT1428_WAVE_ERR				203


/* = GLOBAL FUNCTION DECLARATIONS ========================================== */
int _VI_FUNC zt1428_init (ViRsrc resource_name,ViSession *instrID);
ViStatus _VI_FUNC zt1428_init_with_options 					(ViRsrc resourceName, ViBoolean IDQuery, ViBoolean resetDevice, ViPSession instrumentHandle);


int _VI_FUNC zt1428_auto_setup (ViSession instrID);

int _VI_FUNC zt1428_auto_logic (ViSession instrID, int channel, int logic);

int _VI_FUNC zt1428_vertical (ViSession instrID, int channel, int coupling,
                              int lowpass_filter, double probe_atten,
                              double range, double offset);

int _VI_FUNC zt1428_query_vertical (ViSession instrID, int channel, int *coupling,
                                    int *lowpassFilter, double *probeAttenuation,
                                    double *range, double *offset);

int _VI_FUNC zt1428_acquisition (ViSession instrID, int points,
                                 double sample_interval, int timbase_ref,
                                 double timebase_delay, int trigger_mode,
                                 int acquire_type, int acquire_count);

int _VI_FUNC zt1428_query_acquisition (ViSession instrID, int *points,
                                       double *sample_interval,
                                       int *timebase_ref,
                                       double *timebase_delay, int *trigger_mode,
                                       int *acquire_type, int *acquire_count);

int _VI_FUNC zt1428_function (ViSession instrID, int func_num,
                              int operation, int source1, int source2,
                              int func_state, double range, double offset);

int _VI_FUNC zt1428_query_function (ViSession instrID, int func_num,
                                    int *func_state, double *range, 
                                    double *offset);

int _VI_FUNC zt1428_ext_input (ViSession instrID, int external_mode,
                               double external_level, int external_impedance);

int _VI_FUNC zt1428_query_ext_input (ViSession instrID, int *external_mode,
                                     double *external_level,
                                     int *external_impedance);

int _VI_FUNC zt1428_outputs (ViSession instrID, int bnc_output, double bnc_voltage,
                             int ecl0, int ecl1);

int _VI_FUNC zt1428_query_outputs (ViSession instrID, int *bnc_output,
                                   double *bnc_voltage, int *ecl0, int *ecl1);

int _VI_FUNC zt1428_edge_trigger (ViSession instrID, int source, double level,
                                  int slope, int sensitivity);

int _VI_FUNC zt1428_query_trigger (ViSession instrID, int *source,
                                   int *trigger_mode, double *level_chan1,
                                   double *level_chan2, double *level_ext,
                                   int *sens_chan1, int *sens_chan2,
                                   int *slope_chan1, int *slope_chan2,
                                   int *slope_ext, int *slope_ecl0, int *slope_ecl1);

int _VI_FUNC zt1428_soft_trigger (ViSession instrID);

int _VI_FUNC zt1428_trigger_center (ViSession instrID, int source);

int _VI_FUNC zt1428_trigger_holdoff (ViSession instrID, int holdoff_type,
                                     int holdoff_events, double holdoff_time);

int _VI_FUNC zt1428_pattern_trigger (ViSession instrID, char logic[],
                                     int condition, double gt_time, double lt_time,
                                     double level_chan1, double level_chan2,
                                     double level_ext, int sens_chan1, int sens_chan2);

int _VI_FUNC zt1428_state_trigger (ViSession instrID, char logic[], int source,
                                   int condition, int slope, double level_chan1,
                                   double level_chan2, double level_ext,
                                   int sens_chan1, int sens_chan2);

int _VI_FUNC zt1428_tv_trigger (ViSession instrID, int standard, int field,
                                int line, int slope, int source, double level,
                                int sensitivity);

int _VI_FUNC zt1428_query_adv_trigger (ViSession instrID, int *holdoff_type, 
                                       double *holdoff_value, char logic[], 
                                       int *pattern_condition,
                                       double *gt_time, double *lt_time,
                                       int *state_condition, int *standard,
                                       int *field, int *line);

int _VI_FUNC zt1428_trigger_event (ViSession instrID, int *triggerEvent);

int _VI_FUNC zt1428_measurement (ViSession instrID, int primary_source,
                                 int secondary_source, int measurement,
                                 double *result);

int _VI_FUNC zt1428_measurement_level (ViSession instrID, int user_mode,
                                       int units, double upper_limit,
                                       double lower_limit);

int _VI_FUNC zt1428_delay_parameters (ViSession instrID, int start_slope,
                                      int start_edge, int start_level,
                                      int stop_slope, int stop_edge, int stop_level);

int _VI_FUNC zt1428_width_parameters (ViSession instrID, int pos_width_level,
                                      int neg_width_level);

int _VI_FUNC zt1428_query_measurement (ViSession instrID, int *user_mode,
                                       int *units, double *upper_limit,
                                       double *lower_limit, int *start_slope,
                                       int *stop_slope, int *start_edge,
                                       int *stop_edge, int *start_level,
                                       int *stop_level, int *pos_width_level,
                                       int *neg_width_level);

int _VI_FUNC zt1428_limit_test (ViSession instrID, int limit_test, int statistics,
                                int primary_source, int secondary_source,
                                int measurement, double upper_limit,
                                double lower_limit, int postfailure,
                                int destination);

int _VI_FUNC zt1428_mask_test (ViSession instrID, int mask_test, int source,
                               int mask, double allowance, int postfailure,
                               int destination);

int _VI_FUNC zt1428_result_stats (ViSession instrID, int measurement,
                                  double *current, double *minimum,
                                  double *maximum, double *avg_pass,
                                  int *limit_test);

int _VI_FUNC zt1428_digitize_waveform (ViSession instrID, int channel, int mode);

int _VI_FUNC zt1428_dig_complete (ViSession instrID, int *dig_complete);

int _VI_FUNC zt1428_read_waveform (ViSession instrID, int source,
                                   int xfer_type, double wave_array[],
                                   int *num_points, int *acq_count,
                                   double *sample_interval, double *time_offset,
                                   int *xref, double *volt_increment,
                                   double *volt_offset, int *yref);

int _VI_FUNC zt1428_store_waveform (ViSession instrID, int source,
                                    int destination);

int _VI_FUNC zt1428_load_array (ViSession instrID, int destination,
                                double wave_array[], int num_points,
                                double sample_interval, double time_offset,
                                int xref, double volt_increment,
                                double volt_offset, int yref);

int _VI_FUNC zt1428_reset (ViSession instrID);

int _VI_FUNC zt1428_device_clear (ViSession instrID);

int _VI_FUNC zt1428_self_test (ViSession instrID, int *result);

int _VI_FUNC zt1428_run_stop (ViSession instrID, int state);

int _VI_FUNC zt1428_calibrate (ViSession instrID, int *result);

int _VI_FUNC zt1428_save_recall (ViSession instrID, int setup, int state_number);

int _VI_FUNC zt1428_id_version (ViSession instrID, char idn_string[],
                                double *driver_version);

int _VI_FUNC zt1428_error (ViSession instrID, int *instrument_error);

int _VI_FUNC zt1428_running (ViSession instrID, int *state);
                                
int _VI_FUNC zt1428_wait_op_complete (ViSession instrID);

ViStatus _VI_FUNC zt1428_close (ViSession instrID);

#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

/*****************************************************************************/
/*=== END INCLUDE FILE ======================================================*/
/*****************************************************************************/

#endif


