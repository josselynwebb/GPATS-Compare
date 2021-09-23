/*=========================================================================*/
/* LabWindows/CVI VXIplug&play Instrument Driver Include File (VTL 3.0)
/* Instrument:  Racal Instruments 3152A Arbitrary Waveform Generator
/* File:        ri3152a.h
/* Revision:    5.1
/* Date:        6/2/1999
/*=========================================================================*/
#ifndef __ri3152a_HEADER
#define __ri3152a_HEADER

#include <vpptype.h>

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif


/* = GLOBAL CONSTANT DECLARATIONS =========================================== */
/* Maximum number of ri3152a instances = 12 = max VXI slots per chassis */
#define RI3152A_MAX_INSTR    ((ViInt16) 12)

#define RI3152A_NO_SESSION   ((ViSession) 0)

/*----- Frequency Max and Min limits for various standard waveforms --------*/

#define RI3152A_MIN_FREQ_SIN         ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_SIN         ((ViReal64) 50.0E6)
#define RI3152A_MIN_FREQ_TRI         ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_TRI         ((ViReal64) 1.0E6)
#define RI3152A_MIN_FREQ_SQU         ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_SQU         ((ViReal64) 50.0E6)
#define RI3152A_MIN_FREQ_PULS        ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_PULS        ((ViReal64) 1.0E6)
#define RI3152A_MIN_FREQ_RAMP        ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_RAMP        ((ViReal64) 1.0E6)
#define RI3152A_MIN_FREQ_SINC        ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_SINC        ((ViReal64) 1.0E6)
#define RI3152A_MIN_FREQ_EXP         ((ViReal64) 100.0E-6
#define RI3152A_MAX_FREQ_EXP         ((ViReal64) 1.0E6)
#define RI3152A_MIN_FREQ_GAU         ((ViReal64) 100.0E-6)
#define RI3152A_MAX_FREQ_GAU         ((ViReal64) 1.0E6)

/*---------------------------------------------------------------------------*/

/*----- General Max and Min limits for various standard waveforms -----------*/

#define RI3152A_MIN_AMPLITUDE        ((ViReal64) 10.0E-3)
#define RI3152A_MAX_AMPLITUDE        ((ViReal64) 16.0)
#define RI3152A_MIN_OFFSET           ((ViReal64) -7.19)
#define RI3152A_MAX_OFFSET           ((ViReal64) 7.19)
#define RI3152A_MAX_PEAK_VOLTS		((ViReal64) 8.0)
#define RI3152A_MIN_PEAK_VOLTS		((ViReal64) -8.0)
#define RI3152A_MIN_PHASE            ((ViInt16) 0)
#define RI3152A_MAX_PHASE            ((ViInt16) 360)

/*----------------------------------------------------------------------------*/

/*------ Specific Max and Min limits for various standard waveforms ----------*/

#define RI3152A_POWER_1              ((ViInt16) 1)
#define RI3152A_POWER_2              ((ViInt16) 2)
#define RI3152A_POWER_3              ((ViInt16) 3)
#define RI3152A_POWER_4              ((ViInt16) 4)
#define RI3152A_POWER_5              ((ViInt16) 5)
#define RI3152A_POWER_6              ((ViInt16) 6)
#define RI3152A_POWER_7              ((ViInt16) 7)
#define RI3152A_POWER_8              ((ViInt16) 8)
#define RI3152A_POWER_9              ((ViInt16) 9)

#define RI3152A_MIN_DUTY_CYCLE       ((ViInt16) 1)
#define RI3152A_MAX_DUTY_CYCLE       ((ViInt16) 99)

#define RI3152A_MIN_DELAY_PULSE      ((ViReal64) 0.0)
#define RI3152A_MAX_DELAY_PULSE      ((ViReal64) 99.9)

#define RI3152A_MIN_RISE_TIME_PULSE  ((ViReal64) 0.0)
#define RI3152A_MAX_RISE_TIME_PULSE  ((ViReal64) 99.9)

#define RI3152A_MIN_HIGH_TIME_PULSE  ((ViReal64) 0.0)
#define RI3152A_MAX_HIGH_TIME_PULSE  ((ViReal64) 99.9)

#define RI3152A_MIN_FALL_TIME_PULSE  ((ViReal64) 0.0)
#define RI3152A_MAX_FALL_TIME_PULSE  ((ViReal64) 99.9)

#define RI3152A_MIN_DELAY_RAMP       ((ViReal64) 0.0)
#define RI3152A_MAX_DELAY_RAMP       ((ViReal64) 99.9)

#define RI3152A_MIN_RISE_TIME_RAMP   ((ViReal64) 0.0)
#define RI3152A_MAX_RISE_TIME_RAMP   ((ViReal64) 99.9)

#define RI3152A_MIN_FALL_TIME_RAMP   ((ViReal64) 0.0)
#define RI3152A_MAX_FALL_TIME_RAMP   ((ViReal64) 99.9)

#define RI3152A_MIN_CYCLE_NUMBER     ((ViInt16) 4)
#define RI3152A_MAX_CYCLE_NUMBER     ((ViInt16) 100)

#define RI3152A_MIN_EXPONENT_EXP     ((ViReal64) -200.0)
#define RI3152A_MAX_EXPONENT_EXP     ((ViReal64) 200.0)

#define RI3152A_MIN_EXPONENT_GAU     ((ViInt16) 1)
#define RI3152A_MAX_EXPONENT_GAU     ((ViInt16) 200)

#define RI3152A_MIN_PERCENT_AMP      ((ViInt16) -100)
#define RI3152A_MAX_PERCENT_AMP      ((ViInt16) 100)

#define RI3152A_OUTPUT_ON            ((ViBoolean) 1)
#define RI3152A_OUTPUT_OFF           ((ViBoolean) 0)

#define RI3152A_MODE_FAST            ((ViBoolean) 1)
#define RI3152A_MODE_NORMAL          ((ViBoolean) 0)

#define RI3152A_FORMAT_NORMAL        ((ViBoolean) 0)
#define RI3152A_FORMAT_SWAP          ((ViBoolean) 1)
/*-------------------------------------------------------------------------------*/

/*------------------------------ Filter parameters ------------------------------*/

#define RI3152A_FILTER_OFF           ((ViInt16) 0)
#define RI3152A_FILTER_20MHZ         ((ViInt16) 1)
#define RI3152A_FILTER_25MHZ         ((ViInt16) 2)
#define RI3152A_FILTER_50MHZ         ((ViInt16) 3)

/*-------------------------------------------------------------------------------*/


/*------------------------------ Operation Modes --------------------------------*/

#define RI3152A_MODE_CONT            ((ViInt16) 0)
#define RI3152A_MODE_TRIG            ((ViInt16) 1)
#define RI3152A_MODE_GATED           ((ViInt16) 2)
#define RI3152A_MODE_BURST           ((ViInt16) 3)

/*--------------------------------------------------------------------------------*/

/*------------------------- Amplitude Modulation Parameters ----------------------*/

#define RI3152A_TRIGGER_INTERNAL     ((ViInt16) 0)
#define RI3152A_TRIGGER_EXTERNAL     ((ViInt16) 1)
#define RI3152A_TRIGGER_TTLTRG0		((ViInt16) 2)
#define RI3152A_TRIGGER_TTLTRG1		((ViInt16) 3)
#define RI3152A_TRIGGER_TTLTRG2		((ViInt16) 4)
#define RI3152A_TRIGGER_TTLTRG3		((ViInt16) 5)
#define RI3152A_TRIGGER_TTLTRG4		((ViInt16) 6)
#define RI3152A_TRIGGER_TTLTRG5		((ViInt16) 7)
#define RI3152A_TRIGGER_TTLTRG6		((ViInt16) 8)
#define RI3152A_TRIGGER_TTLTRG7		((ViInt16) 9)
#define RI3152A_TRIGGER_ECLTRG0		((ViInt16) 10)
#define RI3152A_TRIGGER_NONE			((ViInt16) 11)

#define RI3152A_TRIGGER_BIT			((ViInt16) 2)
#define RI3152A_TRIGGER_LCOM			((ViInt16) 3)

#define RI3152A_SLOPE_POS			(VI_TRUE)
#define RI3152A_SLOPE_NEG			(VI_FALSE)


#define RI3152A_TRIGGER_RATE_MIN     ((ViReal64) 60E-6)
#define RI3152A_TRIGGER_RATE_MAX     ((ViReal64) 1000.0)

#define RI3152A_TRIGGER_LEVEL_MIN     ((ViReal64) -10.0)
#define RI3152A_TRIGGER_LEVEL_MAX     ((ViReal64) 10.0)	

#define RI3152A_TRIGGER_DELAY_MIN	((ViInt32) 10)
#define RI3152A_TRIGGER_DELAY_MAX	((ViInt32) 2000000)

#define RI3152A_TRIGGER_COUNT_MIN	((ViInt32)  1)
#define RI3152A_TRIGGER_COUNT_MAX	((ViInt32) 1000000)

#define RI3152A_BURST_INTERNAL       ((ViInt16) 0)
#define RI3152A_BURST_EXTERNAL       ((ViInt16) 1)
#define RI3152A_BURST_BUS            ((ViInt16) 2)

#define RI3152A_BURST_RATE_MIN       ((ViReal64) 20E-6)
#define RI3152A_BURST_RATE_MAX       ((ViReal64) 999)

#define RI3152A_BURST_MIN_CYCLE      ((ViInt32) 1)
#define RI3152A_BURST_MAX_CYCLE      ((ViInt32) 1000000)

/*--------------------------------------------------------------------------------*/

/*------------------------- Waveform Mode Selection Paramaters -------------------*/

#define RI3152A_MODE_STD             ((ViInt16) 0)
#define RI3152A_MODE_ARB             ((ViInt16) 1)
#define RI3152A_MODE_SWEEP           ((ViInt16) 2)
#define RI3152A_MODE_SEQ             ((ViInt16) 3)

/*------------------------- Standard Waveform Indication Values ------------------*/
#define RI3152A_NON_STANDARD			((ViInt16) 0)
#define RI3152A_SINE					((ViInt16) 1)
#define RI3152A_TRIANGLE				((ViInt16) 2)
#define RI3152A_SQUARE				((ViInt16) 3)
#define RI3152A_PULSE				((ViInt16) 4)
#define RI3152A_RAMP					((ViInt16) 5)
#define RI3152A_SINC					((ViInt16) 6)
#define RI3152A_GAUSSIAN				((ViInt16) 7)
#define RI3152A_EXPONENTIAL			((ViInt16) 8)
#define RI3152A_DC					((ViInt16) 9)

/*------------------------- Trigger & Burst Mode Constants ----------------------*/

#define RI3152A_MIN_AM_PERCENT       ((ViInt16) 1)
#define RI3152A_MAX_AM_PERCENT       ((ViInt16) 200)

#define RI3152A_MIN_AM_FREQ          ((ViInt16) 10)
#define RI3152A_MAX_AM_FREQ          ((ViInt32) 500)

/*------------------------- SYNC Pulse Paramaters --------------------------------*/
#define RI3152A_SYNC_OFF					((ViInt16) 0)
#define RI3152A_SYNC_BIT					((ViInt16) 1)
#define RI3152A_SYNC_LCOM				((ViInt16) 2)
#define RI3152A_SYNC_SSYNC				((ViInt16) 3)
#define RI3152A_SYNC_HCLOCK				((ViInt16) 4)
#define RI3152A_SYNC_PULSE				((ViInt16) 5)

/*--------------------------------------------------------------------------------*/

/*-------------------------  Arbitrary Waveform Constants ------------------------*/

#define RI3152A_MIN_SEG_NUMBER      ((ViInt16) 1)
#define RI3152A_MAX_SEG_NUMBER      ((ViInt16) 4096)

#define RI3152A_MIN_WIDTH      		((ViInt16) 1)		
#define RI3152A_MAX_WIDTH      		((ViInt16) 99)

#define RI3152A_MIN_SEGMENT_SIZE     ((ViInt32) 10)
#define RI3152A_MAX_64K_SEG_SIZE 	((ViInt32) 65536)
#define RI3152A_MAX_512K_SEG_SIZE	((ViInt32) 524288)

#define RI3152A_DELETE_ALL_NO        ((ViBoolean) 0)
#define RI3152A_DELETE_ALL_YES       ((ViBoolean) 1)

#define RI3152A_MIN_SAMP_CLK         ((ViReal64) 100E-3)
#define RI3152A_MAX_SAMP_CLK         ((ViReal64) 100E6)

#define RI3152A_CLK_SOURCE_INT       ((ViInt16) 0)
#define RI3152A_CLK_SOURCE_EXT       ((ViInt16) 1)
#define RI3152A_CLK_SOURCE_ECLTRG0   ((ViInt16) 2)

/*---------------------------------------------------------------------------------*/

/*-------------------------  Sequential Waveform Constants -------------------------*/

#define RI3152A_MIN_NUM_STEPS		((ViInt16) 1)
#define RI3152A_MAX_NUM_STEPS		((ViInt16) 4096)

#define RI3152A_MIN_NUM_REPEAT		((ViInt32) 1)
#define RI3152A_MAX_NUM_REPEAT		((ViInt32) 1000000)

#define RI3152A_SEQ_AUTO			 	(VI_FALSE)
#define RI3152A_SEQ_TRIG				(VI_TRUE)

/*---------------------------------------------------------------------------------*/

/*------------------------  3152 Phase Lock Loop Constants ------------------------*/

#define RI3152A_PLL_ON				(VI_TRUE)
#define RI3152A_PLL_OFF				(VI_FALSE)

#define RI3152A_PLL_MIN_PHASE			-180.0
#define RI3152A_PLL_MAX_PHASE			180.0

#define RI3152A_MIN_FINE_PHASE		-36.0
#define RI3152A_MAX_FINE_PHASE		 36.0

/*---------------------------------------------------------------------------------*/

/*---------------------------------------------------------------------------------*/

/*------------------------  3152 Sweep Constants ------------------------*/
#define RI3152A_SWEEP_MIN_TIME       ((ViInt16) 100E-3)
#define RI3152A_SWEEP_MAX_TIME       ((ViInt16) 10E3)
#define RI3152A_SWEEP_MIN_STEP       ((ViInt16) 10)
#define RI3152A_SWEEP_MAX_STEP       ((ViInt16) 1000)
#define RI3152A_SWEEP_MIN_FREQ         ((ViReal64) 1E-3)
#define RI3152A_SWEEP_MAX_FREQ         ((ViReal64) 9E6)
#define RI3152A_SWEEP_MIN_FREQ_STOP         ((ViReal64) 10E-3)
#define RI3152A_SWEEP_MAX_FREQ_STOP         ((ViReal64) 10E6)
#define RI3152A_SWEEP_MIN_FREQ_RAST         ((ViReal64) 100E-3)
#define RI3152A_SWEEP_MAX_FREQ_RAST         ((ViReal64) 1E8)

/*-------------------------------- Other Constants --------------------------------*/


/*---------------------------------------------------------------------------------*/

#define _RI3152A_ERROR   (_VI_ERROR+0x3FFC0800L)          /* 0xBFFC0800L = -1074001920 */
#define RI3152A_ERR(x)   ((ViStatus) (_RI3152A_ERROR+(x)))

/* = FUNCTION PROTOTYPES ========================================================= */

ViStatus _VI_FUNC ri3152a_init (ViRsrc instrDescriptor, ViBoolean IDQuery,
                      ViBoolean resetDevice, ViSession *instrHandle);
ViStatus _VI_FUNC ri3152a_sine_wave (ViSession instrHandle, ViReal64 frequency,
                                    ViReal64 amplitude, ViReal64 offset, 
                                    ViInt16 phase, ViInt16 powerSinex);

ViStatus _VI_FUNC ri3152a_triangular_wave (ViSession instrHandle, ViReal64 frequency,
                                          ViReal64 amplitude, ViReal64 offset, 
                                          ViInt16 phase, ViInt16 powerTriangularx);

ViStatus _VI_FUNC ri3152a_square_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViInt16 duty_cycle);

ViStatus _VI_FUNC ri3152a_pulse_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViReal64 delay, ViReal64 rise_time, 
                                      ViReal64 high_time, ViReal64 fall_time);

ViStatus _VI_FUNC ri3152a_ramp_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViReal64 delay, ViReal64 rise_time, 
                                      ViReal64 fall_time);

ViStatus _VI_FUNC ri3152a_sinc_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViInt16 cycle_number);

ViStatus _VI_FUNC ri3152a_gaussian_wave (ViSession instrHandle, ViReal64 frequency,
                                        ViReal64 amplitude, ViReal64 offset, 
                                        ViInt16 exponent);

ViStatus _VI_FUNC ri3152a_exponential_wave (ViSession vi,
                                           ViReal64 frequency,
                                           ViReal64 amplitude,
                                           ViReal64 offset,
                                           ViInt16 exponent);
ViStatus _VI_FUNC ri3152a_dc_signal (ViSession instrHandle, ViInt16 percent_amplitude);

/*---------------------------------------- Arbitrary Waveforms --------------------------------*/

ViStatus _VI_FUNC ri3152a_define_arb_segment (ViSession instrHandle, ViInt16 segment_number,
                                            ViInt32 segment_size);

ViStatus _VI_FUNC ri3152a_delete_segments (ViSession instrHandle, ViInt16 segment_number,
                                            ViBoolean delete_all_segments);

ViStatus _VI_FUNC ri3152a_load_arb_data (ViSession instrHandle, ViInt16 segment_number,
											ViUInt16 *data_pts, ViInt32 number_of_points);

ViStatus _VI_FUNC ri3152a_load_and_shift_arb_data (ViSession instrHandle,
                                                  ViInt16 segmentNumber,
                                                  ViInt16 dataPointArray[],
                                                  ViInt32 numberofPoints);

ViStatus _VI_FUNC ri3152a_load_ascii_file (ViSession instrHandle, ViInt16 segment_number,
											ViString file_name, ViInt32 number_of_points);

ViStatus _VI_FUNC ri3152a_load_wavecad_file (ViSession instrHandle, ViChar waveCADFileName[]);
ViStatus _VI_FUNC ri3152a_load_wavecad_wave_file (ViSession instrHandle,
                                        		 ViInt16 segmentNumber,
                                        		 ViChar waveCADWaveformFileName[]);


ViStatus _VI_FUNC ri3152a_output_arb_waveform (ViSession instrHandle, ViInt16 segment_number,
                                              ViReal64 sampling_clock, ViReal64 amplitude,
                                              ViReal64 offset, ViInt16 clock_source);

/*----------------------------------------------------------------------------------------------*/


/*----------------------------------------- Sequence Waveforms ---------------------------------*/


ViStatus _VI_FUNC ri3152a_define_sequence (ViSession instrHandle,
                                                  ViInt16 number_of_steps,
                                                  ViInt16 *segment_number,
                                                  ViInt32 *repeat_number);

ViStatus _VI_FUNC ri3152a_delete_sequence (ViSession vi, ViInt16 stepNumber);

ViStatus _VI_FUNC ri3152a_output_sequence_waveform (ViSession instrHandle,
                                                    ViReal64 sampling_clock,
                                                    ViReal64 amplitude,
                                                    ViReal64 offset,
                                                    ViBoolean sequence_mode);

/*----------------------------------------------------------------------------------------------*/

/*---------------------------------- Example ---------------------------------------------------*/

ViStatus _VI_FUNC ri3152a_example_generate_seq_waveform (ViSession instrHandle);

/*----------------------------------------------------------------------------------------------*/

ViStatus _VI_FUNC ri3152a_filter (ViSession instrHandle, ViInt16 lp_filter);

ViStatus _VI_FUNC ri3152a_amplitude_modulation (ViSession instrHandle, ViInt16 percent_amplitude,
                                                ViInt32 internal_frequency);


ViStatus _VI_FUNC ri3152a_output (ViSession instrHandle, ViBoolean output_switch);

ViStatus _VI_FUNC ri3152a_operating_mode (ViSession instrHandle, ViInt16 operating_mode);

ViStatus _VI_FUNC ri3152a_select_waveform_mode (ViSession instrHandle, ViInt16 waveform_type);
ViStatus _VI_FUNC ri3152a_set_frequency (ViSession instrHandle, ViReal64 frequency);
ViStatus _VI_FUNC ri3152a_set_amplitude (ViSession instrHandle, ViReal64 amplitude);
ViStatus _VI_FUNC ri3152a_set_offset (ViSession instrHandle, ViReal64 offset);

ViStatus _VI_FUNC ri3152a_trigger_source (ViSession instrHandle, ViInt16 triggerSource);
ViStatus _VI_FUNC ri3152a_trigger_rate (ViSession instrHandle, ViReal64 triggerRate);
ViStatus _VI_FUNC ri3152a_trigger_slope (ViSession instrHandle, ViBoolean triggerSlope);
ViStatus _VI_FUNC ri3152a_trigger_delay (ViSession instrHandle, ViInt32 triggerDelay);
ViStatus _VI_FUNC ri3152a_output_trigger (ViSession vi, 
                                         ViInt16 outputTriggerSource,
                                         ViInt16 outputTriggerLine, 
                                         ViInt32 BITTriggerPoint,
                                         ViInt16 LCOMSegment,
                                         ViInt16 outputWidth);
ViStatus _VI_FUNC ri3152a_output_sync (ViSession vi, 
									  ViInt16 SYNCPulseSource,
                                      ViInt32 BITSYNCPoint,
                                      ViInt16 outputWidth,
                                      ViInt16 LCOMSegment);
ViStatus _VI_FUNC ri3152a_immediate_trigger (ViSession instrHandle);
ViStatus _VI_FUNC ri3152a_phase_master (ViSession instrHandle);
ViStatus _VI_FUNC ri3152a_phase_slave (ViSession instrHandle, ViInt16 phaseOffset);
ViStatus _VI_FUNC ri3152a_phase_lock_loop (ViSession instrHandle, ViBoolean phaseLockLoop);
ViStatus _VI_FUNC ri3152a_pll_phase (ViSession instrHandle, ViReal64 phase);
ViStatus _VI_FUNC ri3152a_pll_fine_phase(ViSession instrHandle, ViReal64 phase);

ViStatus _VI_FUNC ri3152a_burst_mode (ViSession instrHandle, ViInt32 number_of_cycles);


ViStatus _VI_FUNC ri3152a_status_query (ViSession vi, ViReal64 *voltage_value,
                                       ViReal64 *frequency_value, ViReal64 *offset_value,
                                       ViInt16 *filter_type);
ViStatus _VI_FUNC ri3152a_mode_query (ViSession instrHandle, ViInt16 *presentWaveformMode,
                            ViInt16 *presentStandardWaveform);
ViStatus _VI_FUNC ri3152a_pll_query (ViSession instrHandle, ViBoolean *phaseLockState,
                           ViReal64 *coarsePhase, ViReal64 *finePhase, ViReal64 *extFrequency);
ViStatus _VI_FUNC ri3152a_clear (ViSession instrHandle);
ViStatus _VI_FUNC ri3152a_trigger (ViSession instrHandle);
ViStatus _VI_FUNC ri3152a_poll (ViSession instrHandle, ViInt16 *statusByte);
ViStatus _VI_FUNC ri3152a_reset (ViSession instrHandle);
ViStatus _VI_FUNC ri3152a_read_status_byte (ViSession instrHandle, ViInt16 *statusByte);
ViStatus _VI_FUNC ri3152a_set_SRE (ViSession instrHandle, ViInt16 SRERegister);
ViStatus _VI_FUNC ri3152a_read_SRE (ViSession instrHandle, ViInt16 *SRERegister);
ViStatus _VI_FUNC ri3152a_read_ESR (ViSession instrHandle, ViInt16 *ESRRegister);
ViStatus _VI_FUNC ri3152a_set_ESE (ViSession instrHandle, ViInt16 ESERegister);
ViStatus _VI_FUNC ri3152a_read_ESE (ViSession instrHandle, ViInt16 *ESERegister);
ViStatus _VI_FUNC ri3152a_self_test (ViSession instrHandle, ViInt16 *passFail,
                           ViChar selfTestMessage[]);
ViStatus _VI_FUNC ri3152a_revision_query (ViSession instrHandle, ViString driverRevision,
                                ViString firmwareRevision);
ViStatus _VI_FUNC ri3152a_error_query (ViSession instrHandle, ViInt32 *error,
                             ViString errorMessage);
ViStatus _VI_FUNC ri3152a_error_message (ViSession instrHandle, ViStatus errorReturnValue,
                               ViString errorMessage);
ViStatus _VI_FUNC ri3152a_close (ViSession instrHandle);
ViStatus _VI_FUNC ri3152a_trigger_level_query (ViSession vi, ViReal64* trigger_level_value);
ViStatus _VI_FUNC ri3152a_set_trigger_level (ViSession vi, ViReal64 triggerLevel);
ViStatus _VI_FUNC ri3152a_load_sequence_binary (ViSession vi, 
                                          ViInt16 number_of_steps,
                                          ViInt16 *segment_number,
                                          ViInt32 *repeat_number);
ViStatus _VI_FUNC ri3152a_format_border    (ViSession vi,
                                    ViBoolean format);
ViStatus _VI_FUNC ri3152a_change_mode    (ViSession vi,
                                    ViBoolean mode);
ViStatus _VI_FUNC ri3152a_arb_sampling_freq (ViSession vi,
											ViReal64 sampling_clock);
ViStatus _VI_FUNC ri3152a_version_query (ViSession vi,
                                      ViString  version);
ViStatus _VI_FUNC ri3152a_opc (ViSession vi);
ViStatus _VI_FUNC ri3152a_select_standard_waveform  (ViSession vi,
                                          		ViInt16 waveform_type);
ViStatus _VI_FUNC ri3152a_output_sync_pos (ViSession vi, ViInt32 syncPos);
ViStatus _VI_FUNC ri3152a_sync_pos_query (ViSession vi,
										ViInt32 *pos);
ViStatus _VI_FUNC ri3152a_output_sync_width (ViSession vi, ViInt32 syncWidth);
ViStatus _VI_FUNC ri3152a_option_query (ViSession vi,
                                      ViString  option);






ViStatus _VI_FUNC ri3152a_query_sampling_freq (ViSession vi, ViReal64* samp_freq);
ViStatus _VI_FUNC ri3152a_format_border_query (ViSession vi,
										ViString format);
ViStatus _VI_FUNC ri3152a_query_output (ViSession vi,
										ViPInt16 output_state);
ViStatus _VI_FUNC ri3152a_output_eclt    (ViSession vi,
                                    ViBoolean output_switch);
ViStatus _VI_FUNC ri3152a_query_output_eclt (ViSession vi,
										ViPInt16 output_state);
ViStatus _VI_FUNC ri3152a_trigger_source_query (ViSession vi,
										ViString trig_sour);
ViStatus _VI_FUNC ri3152a_output_sync_query (ViSession vi,
										ViInt16 *sync_state);
ViStatus _VI_FUNC ri3152a_sync_source_query (ViSession vi,
										ViString source);
ViStatus _VI_FUNC ri3152a_sync_width_query (ViSession vi,
										ViInt32 *width);
ViStatus _VI_FUNC ri3152a_frequency_query (ViSession vi,
										ViReal64 *frequency);
ViStatus _VI_FUNC ri3152a_query_amplitude(ViSession vi, ViPReal64 amplitude);
ViStatus _VI_FUNC ri3152a_query_offset(ViSession vi, ViPReal64 offset);
ViStatus _VI_FUNC ri3152a_sclk_source_query (ViSession vi,
										ViPInt16 source);
ViStatus _VI_FUNC ri3152a_sine_phase_query (ViSession vi,
										ViPInt16 phase);
ViStatus _VI_FUNC ri3152a_set_triangle_phase (ViSession vi,  ViInt16 phase);
ViStatus _VI_FUNC ri3152a_triangle_phase_query (ViSession vi,
										ViPInt16 phase);
ViStatus _VI_FUNC ri3152a_set_square_dcycle (ViSession vi,  ViInt16 dcycle);
ViStatus _VI_FUNC ri3152a_square_dcycle_query (ViSession vi,
										ViPInt16 dcycle);
ViStatus _VI_FUNC ri3152a_set_pulse_data (ViSession vi,  ViReal64 delay,
										ViReal64 width, ViReal64 tran, ViReal64 trail);
ViStatus _VI_FUNC ri3152a_pulse_data_query (ViSession vi,
										ViPReal64 delay, ViPReal64 width,
										ViPReal64 tran, ViPReal64 trail);
ViStatus _VI_FUNC ri3152a_set_ramp_data (ViSession vi,  ViReal64 delay,
										ViReal64 tran, ViReal64 trail);
ViStatus _VI_FUNC ri3152a_ramp_data_query (ViSession vi,
										ViPReal64 delay,
										ViPReal64 tran, ViPReal64 trail);
ViStatus _VI_FUNC ri3152a_set_gauss_exp (ViSession vi,  ViInt16 exp);
ViStatus _VI_FUNC ri3152a_gauss_exp_query (ViSession vi,
										ViPInt16 exp);
ViStatus _VI_FUNC ri3152a_set_exponential_exp (ViSession vi,  ViInt16 exp);
ViStatus _VI_FUNC ri3152a_exponential_exp_query (ViSession vi,
										ViReal64 *exp);
ViStatus _VI_FUNC ri3152a_dc_amplitude_query (ViSession vi,
										ViPInt16 amplitude);
ViStatus _VI_FUNC ri3152a_amplitude_modulation_query (ViSession vi,
										ViPInt16 am_val, ViInt32 *internal_freq);
ViStatus _VI_FUNC ri3152a_phase_query (ViSession vi,
										ViPInt16 phase_offset, ViPInt16 phase_state,
										ViPInt16 phase_source);
ViStatus _VI_FUNC ri3152a_sequence_mode_query (ViSession vi,
										ViPInt16 sequence_mode);
ViStatus _VI_FUNC ri3152a_query_operating_mode (ViSession vi, ViPInt16 operating_mode);
ViStatus _VI_FUNC ri3152a_burst_count_query (ViSession vi,
										ViPInt32 count);
ViStatus _VI_FUNC ri3152a_trigger_delay_query (ViSession vi,
										ViPInt32 delay);
ViStatus _VI_FUNC ri3152a_trig_delay_state (ViSession vi, ViBoolean on_off);
ViStatus _VI_FUNC ri3152a_trig_delay_state_query (ViSession vi,
										ViPInt16 delay_state);
ViStatus _VI_FUNC ri3152a_trigger_source_adv_query (ViSession vi,
										ViPInt16 trig_source);
ViStatus _VI_FUNC ri3152a_trigger_slope_query (ViSession vi,
										ViPInt16 trig_slope);
ViStatus _VI_FUNC ri3152a_trigger_rate_query (ViSession vi,
										ViReal64 *triggerRate);
ViStatus _VI_FUNC ri3152a_share_memory_query (ViSession vi,
										ViPInt16 smem_state,
										ViPInt16 smem_mode);
										
										
ViStatus _VI_FUNC ri3152a_format_wave    (ViSession vi,
                                    ViBoolean format);
ViStatus _VI_FUNC ri3152a_format_wave_query (ViSession vi,
										ViString format);
ViStatus _VI_FUNC ri3152a_output_shunt    (ViSession vi,
                                    ViBoolean output_switch);
ViStatus _VI_FUNC ri3152a_query_output_shunt (ViSession vi,
										ViPInt16 output_state);
ViStatus _VI_FUNC ri3152a_set_sine_wave_power (ViSession vi,  ViInt16 power);
ViStatus _VI_FUNC ri3152a_sine_power_query (ViSession vi,
										ViPInt16 power);
ViStatus _VI_FUNC ri3152a_set_triangle_power (ViSession vi,  ViInt16 power);
ViStatus _VI_FUNC ri3152a_triangle_power_query (ViSession vi,
										ViPInt16 power);
ViStatus _VI_FUNC ri3152a_set_sinc_ncycle (ViSession vi,  ViInt16 ncycle);
ViStatus _VI_FUNC ri3152a_sinc_ncycle_query (ViSession vi,
										ViPInt16 ncycle);
ViStatus _VI_FUNC ri3152a_phase_lock_null (ViSession vi);
ViStatus _VI_FUNC ri3152a_set_sweep_time (ViSession vi,
							   			  ViReal64 sweep_time);
ViStatus _VI_FUNC ri3152a_query_sweep_time (ViSession vi,
							   			  ViReal64 *sweep_time);
ViStatus _VI_FUNC ri3152a_set_sweep_direction (ViSession vi,
							   			  ViBoolean mode);
ViStatus _VI_FUNC ri3152a_query_sweep_direction (ViSession vi,
							   			  ViBoolean *mode);
ViStatus _VI_FUNC ri3152a_set_sweep_step (ViSession vi,
							   			  ViInt16 sweep_step);
ViStatus _VI_FUNC ri3152a_query_sweep_step (ViSession vi,
							   			  ViInt16 *sweep_step);
ViStatus _VI_FUNC ri3152a_set_sweep_freq_start (ViSession vi,
							   			  ViReal64 freq);
ViStatus _VI_FUNC ri3152a_query_sweep_freq_start (ViSession vi,
							   			  ViReal64 *freq);
ViStatus _VI_FUNC ri3152a_set_sweep_freq_stop (ViSession vi,
							   			  ViReal64 freq);
ViStatus _VI_FUNC ri3152a_query_sweep_freq_stop (ViSession vi,
							   			  ViReal64 *freq);
ViStatus _VI_FUNC ri3152a_set_sweep_freq_raster (ViSession vi,
							   			  ViReal64 freq);
ViStatus _VI_FUNC ri3152a_query_sweep_freq_raster (ViSession vi,
							   			  ViReal64 *freq);
ViStatus _VI_FUNC ri3152a_set_sweep_freq_marker (ViSession vi,
							   			  ViReal64 marker);
ViStatus _VI_FUNC ri3152a_query_sweep_freq_marker (ViSession vi,
							   			  ViReal64 *marker);
ViStatus _VI_FUNC ri3152a_sweep_function  (ViSession vi,
                                          		ViInt16 function_type);
ViStatus _VI_FUNC ri3152a_sweep_func_query (ViSession vi,
                            		 ViInt16 *function_type);
ViStatus _VI_FUNC ri3152a_set_sweep_spacing (ViSession vi,
							   			  ViBoolean mode);
ViStatus _VI_FUNC ri3152a_query_sweep_spacing (ViSession vi,
							   			  ViBoolean *mode);
ViStatus _VI_FUNC ri3152a_set_sine_wave_phase (ViSession vi,  ViInt16 phase);
ViStatus _VI_FUNC ri3152a_sine_phase_query (ViSession vi,
										ViPInt16 phase);
ViStatus _VI_FUNC ri3152a_load_segment_binary (ViSession vi,
											 ViInt16 num_segments,
											 ViAInt32 wavesize);

										
										
										
										
										
										
										
										



/*--------------------------------------------------------------------------------------------*/


#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

#endif
