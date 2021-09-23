/*=========================================================================*/
/* LabWindows/CVI VXIplug&play Instrument Driver Include File (VTL 3.0)
/* Instrument:  Racal Instruments 3151 Arbitrary Waveform Generator
/* File:        ri3151.h
/* Revision:    2.1
/* Date:        11/4/96
/*=========================================================================*/
#ifndef __ri3151_HEADER
#define __ri3151_HEADER

#include <vpptype.h>

#if defined(__cplusplus) || defined(__cplusplus__)
export "C" {
#endif


/* = GLOBAL CONSTANT DECLARATIONS =========================================== */
/* Maximum number of ri3151 instances = 12 = max VXI slots per chassis */
#define RI3151_MAX_INSTR    ((ViInt16) 12)

#define RI3151_NO_SESSION   ((ViSession) 0)

/*----- Frequency Max and Min limits for various standard waveforms --------*/

#define RI3151_MIN_FREQ_SIN         ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_SIN         ((ViReal64) 50.0E6)
#define RI3151_MIN_FREQ_TRI         ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_TRI         ((ViReal64) 1.0E6)
#define RI3151_MIN_FREQ_SQU         ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_SQU         ((ViReal64) 50.0E6)
#define RI3151_MIN_FREQ_PULS        ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_PULS        ((ViReal64) 1.0E6)
#define RI3151_MIN_FREQ_RAMP        ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_RAMP        ((ViReal64) 1.0E6)
#define RI3151_MIN_FREQ_SINC        ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_SINC        ((ViReal64) 1.0E6)
#define RI3151_MIN_FREQ_EXP         ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_EXP         ((ViReal64) 1.0E6)
#define RI3151_MIN_FREQ_GAU         ((ViReal64) 100.0E-6)
#define RI3151_MAX_FREQ_GAU         ((ViReal64) 1.0E6)

/*---------------------------------------------------------------------------*/

/*----- General Max and Min limits for various standard waveforms -----------*/

#define RI3151_MIN_AMPLITUDE        ((ViReal64) 10.0E-3)
#define RI3151_MAX_AMPLITUDE        ((ViReal64) 16.0)
#define RI3151_MIN_OFFSET           ((ViReal64) -7.19)
#define RI3151_MAX_OFFSET           ((ViReal64) 7.19)
#define RI3151_MAX_PEAK_VOLTS		((ViReal64) 8.0)
#define RI3151_MIN_PEAK_VOLTS		((ViReal64) -8.0)
#define RI3151_MIN_PHASE            ((ViInt16) 0)
#define RI3151_MAX_PHASE            ((ViInt16) 360)

/*----------------------------------------------------------------------------*/

/*------ Specific Max and Min limits for various standard waveforms ----------*/

#define RI3151_POWER_1              ((ViInt16) 1)
#define RI3151_POWER_2              ((ViInt16) 2)
#define RI3151_POWER_3              ((ViInt16) 3)
#define RI3151_POWER_4              ((ViInt16) 4)
#define RI3151_POWER_5              ((ViInt16) 5)
#define RI3151_POWER_6              ((ViInt16) 6)
#define RI3151_POWER_7              ((ViInt16) 7)
#define RI3151_POWER_8              ((ViInt16) 8)
#define RI3151_POWER_9              ((ViInt16) 9)

#define RI3151_MIN_DUTY_CYCLE       ((ViInt16) 1)
#define RI3151_MAX_DUTY_CYCLE       ((ViInt16) 99)

#define RI3151_MIN_DELAY_PULSE      ((ViReal64) 0.0)
#define RI3151_MAX_DELAY_PULSE      ((ViReal64) 99.9)

#define RI3151_MIN_RISE_TIME_PULSE  ((ViReal64) 0.0)
#define RI3151_MAX_RISE_TIME_PULSE  ((ViReal64) 99.9)

#define RI3151_MIN_HIGH_TIME_PULSE  ((ViReal64) 0.0)
#define RI3151_MAX_HIGH_TIME_PULSE  ((ViReal64) 99.9)

#define RI3151_MIN_FALL_TIME_PULSE  ((ViReal64) 0.0)
#define RI3151_MAX_FALL_TIME_PULSE  ((ViReal64) 99.9)

#define RI3151_MIN_DELAY_RAMP       ((ViReal64) 0.0)
#define RI3151_MAX_DELAY_RAMP       ((ViReal64) 99.9)

#define RI3151_MIN_RISE_TIME_RAMP   ((ViReal64) 0.0)
#define RI3151_MAX_RISE_TIME_RAMP   ((ViReal64) 99.9)

#define RI3151_MIN_FALL_TIME_RAMP   ((ViReal64) 0.0)
#define RI3151_MAX_FALL_TIME_RAMP   ((ViReal64) 99.9)

#define RI3151_MIN_CYCLE_NUMBER     ((ViInt16) 4)
#define RI3151_MAX_CYCLE_NUMBER     ((ViInt16) 100)

#define RI3151_MIN_EXPONENT_EXP     ((ViReal64) -200.0)
#define RI3151_MAX_EXPONENT_EXP     ((ViReal64) 200.0)

#define RI3151_MIN_EXPONENT_GAU     ((ViInt16) 1)
#define RI3151_MAX_EXPONENT_GAU     ((ViInt16) 200)

#define RI3151_MIN_PERCENT_AMP      ((ViInt16) -100)
#define RI3151_MAX_PERCENT_AMP      ((ViInt16) 100)

#define RI3151_OUTPUT_ON            ((ViBoolean) 1)
#define RI3151_OUTPUT_OFF           ((ViBoolean) 0)
/*-------------------------------------------------------------------------------*/

/*------------------------------ Filter parameters ------------------------------*/

#define RI3151_FILTER_OFF           ((ViInt16) 0)
#define RI3151_FILTER_20MHZ         ((ViInt16) 1)
#define RI3151_FILTER_25MHZ         ((ViInt16) 2)
#define RI3151_FILTER_50MHZ         ((ViInt16) 3)

/*-------------------------------------------------------------------------------*/


/*------------------------------ Operation Modes --------------------------------*/

#define RI3151_MODE_CONT            ((ViInt16) 0)
#define RI3151_MODE_TRIG            ((ViInt16) 1)
#define RI3151_MODE_GATED           ((ViInt16) 2)
#define RI3151_MODE_BURST           ((ViInt16) 3)

/*--------------------------------------------------------------------------------*/

/*------------------------- Amplitude Modulation Parameters ----------------------*/

#define RI3151_TRIGGER_INTERNAL     ((ViInt16) 0)
#define RI3151_TRIGGER_EXTERNAL     ((ViInt16) 1)
#define RI3151_TRIGGER_TTLTRG0		((ViInt16) 2)
#define RI3151_TRIGGER_TTLTRG1		((ViInt16) 3)
#define RI3151_TRIGGER_TTLTRG2		((ViInt16) 4)
#define RI3151_TRIGGER_TTLTRG3		((ViInt16) 5)
#define RI3151_TRIGGER_TTLTRG4		((ViInt16) 6)
#define RI3151_TRIGGER_TTLTRG5		((ViInt16) 7)
#define RI3151_TRIGGER_TTLTRG6		((ViInt16) 8)
#define RI3151_TRIGGER_TTLTRG7		((ViInt16) 9)
#define RI3151_TRIGGER_NONE			((ViInt16) 10)

#define RI3151_TRIGGER_BIT			((ViInt16) 2)
#define RI3151_TRIGGER_LCOM			((ViInt16) 3)

#define RI3151_SLOPE_POS			(VI_TRUE)
#define RI3151_SLOPE_NEG			(VI_FALSE)


#define RI3151_TRIGGER_RATE_MIN     ((ViReal64) 60E-6)
#define RI3151_TRIGGER_RATE_MAX     ((ViReal64) 1000.0)

#define RI3151_TRIGGER_DELAY_MIN	((ViInt32) 10)
#define RI3151_TRIGGER_DELAY_MAX	((ViInt32) 2000000)

#define RI3151_TRIGGER_COUNT_MIN	((ViInt32)  1)
#define RI3151_TRIGGER_COUNT_MAX	((ViInt32) 1000000)

#define RI3151_BURST_INTERNAL       ((ViInt16) 0)
#define RI3151_BURST_EXTERNAL       ((ViInt16) 1)
#define RI3151_BURST_BUS            ((ViInt16) 2)

#define RI3151_BURST_RATE_MIN       ((ViReal64) 20E-6)
#define RI3151_BURST_RATE_MAX       ((ViReal64) 999)

#define RI3151_BURST_MIN_CYCLE      ((ViInt32) 1)
#define RI3151_BURST_MAX_CYCLE      ((ViInt32) 1000000)

/*--------------------------------------------------------------------------------*/

/*------------------------- Waveform Mode Selection Paramaters -------------------*/

#define RI3151_MODE_STD             ((ViInt16) 0)
#define RI3151_MODE_ARB             ((ViInt16) 1)
#define RI3151_MODE_SEQ             ((ViInt16) 2)

/*------------------------- Standard Waveform Indication Values ------------------*/
#define RI3151_NON_STANDARD			((ViInt16) 0)
#define RI3151_SINE					((ViInt16) 1)
#define RI3151_TRIANGLE				((ViInt16) 2)
#define RI3151_SQUARE				((ViInt16) 3)
#define RI3151_PULSE				((ViInt16) 4)
#define RI3151_RAMP					((ViInt16) 5)
#define RI3151_SINC					((ViInt16) 6)
#define RI3151_GAUSSIAN				((ViInt16) 7)
#define RI3151_EXPONENTIAL			((ViInt16) 8)
#define RI3151_DC					((ViInt16) 9)

/*------------------------- Trigger & Burst Mode Constants ----------------------*/

#define RI3151_MIN_AM_PERCENT       ((ViInt16) 0)
#define RI3151_MAX_AM_PERCENT       ((ViInt16) 200)

#define RI3151_MIN_AM_FREQ          ((ViInt16) 10)
#define RI3151_MAX_AM_FREQ          ((ViInt32) 500)

/*------------------------- SYNC Pulse Paramaters --------------------------------*/
#define RI3151_SYNC_OFF					((ViInt16) 0)
#define RI3151_SYNC_BIT					((ViInt16) 1)
#define RI3151_SYNC_LCOM				((ViInt16) 2)
#define RI3151_SYNC_SSYNC				((ViInt16) 3)
#define RI3151_SYNC_HCLOCK				((ViInt16) 4)


/*--------------------------------------------------------------------------------*/

/*-------------------------  Arbitrary Waveform Constants ------------------------*/

#define RI3151_MIN_SEG_NUMBER      ((ViInt16) 1)
#define RI3151_MAX_SEG_NUMBER      ((ViInt16) 4096)

#define RI3151_MIN_SEGMENT_SIZE     ((ViInt32) 10)
#define RI3151_MAX_64K_SEG_SIZE 	((ViInt32) 64536)
#define RI3151_MAX_512K_SEG_SIZE	((ViInt32) 523288)

#define RI3151_DELETE_ALL_NO        ((ViBoolean) 0)
#define RI3151_DELETE_ALL_YES       ((ViBoolean) 1)

#define RI3151_MIN_SAMP_CLK         ((ViReal64) 10E-3)
#define RI3151_MAX_SAMP_CLK         ((ViReal64) 100E6)

#define RI3151_CLK_SOURCE_INT       ((ViInt16) 0)
#define RI3151_CLK_SOURCE_EXT       ((ViInt16) 1)
#define RI3151_CLK_SOURCE_ECLTRG0   ((ViInt16) 2)

/*---------------------------------------------------------------------------------*/

/*-------------------------  Sequential Waveform Constants -------------------------*/

#define RI3151_MIN_NUM_STEPS		((ViInt16) 1)
#define RI3151_MAX_NUM_STEPS		((ViInt16) 4095)

#define RI3151_MIN_NUM_REPEAT		((ViInt32) 1)
#define RI3151_MAX_NUM_REPEAT		((ViInt32) 1000000)

#define RI3151_SEQ_AUTO			 	(VI_FALSE)
#define RI3151_SEQ_TRIG				(VI_TRUE)

/*---------------------------------------------------------------------------------*/

/*------------------------  3152 Phase Lock Loop Constants ------------------------*/

#define RI3152_PLL_ON				(VI_TRUE)
#define RI3152_PLL_OFF				(VI_FALSE)

#define RI3152_MIN_PHASE			-180.0
#define RI3152_MAX_PHASE			 180.0

#define RI3152_MIN_FINE_PHASE		-36.0
#define RI3152_MAX_FINE_PHASE		 36.0

/*---------------------------------------------------------------------------------*/

/*-------------------------------- Other Constants --------------------------------*/


/*---------------------------------------------------------------------------------*/

#define _RI3151_ERROR   (_VI_ERROR+0x3FFC0800L)          /* 0xBFFC0800L = -1074001920 */
#define RI3151_ERR(x)   ((ViStatus) (_RI3151_ERROR+(x)))

/* = FUNCTION PROTOTYPES ========================================================= */

ViStatus _VI_FUNC ri3151_init (ViRsrc instrDescriptor, ViBoolean IDQuery,
                      ViBoolean resetDevice, ViSession *instrHandle);
ViStatus _VI_FUNC ri3151_sine_wave (ViSession instrHandle, ViReal64 frequency,
                                    ViReal64 amplitude, ViReal64 offset, 
                                    ViInt16 phase, ViInt16 powerSinex);

ViStatus _VI_FUNC ri3151_triangular_wave (ViSession instrHandle, ViReal64 frequency,
                                          ViReal64 amplitude, ViReal64 offset, 
                                          ViInt16 phase, ViInt16 powerTriangularx);

ViStatus _VI_FUNC ri3151_square_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViInt16 duty_cycle);

ViStatus _VI_FUNC ri3151_pulse_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViReal64 delay, ViReal64 rise_time, 
                                      ViReal64 high_time, ViReal64 fall_time);

ViStatus _VI_FUNC ri3151_ramp_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViReal64 delay, ViReal64 rise_time, 
                                      ViReal64 fall_time);

ViStatus _VI_FUNC ri3151_sinc_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViInt16 cycle_number);

ViStatus _VI_FUNC ri3151_gaussian_wave (ViSession instrHandle, ViReal64 frequency,
                                        ViReal64 amplitude, ViReal64 offset, 
                                        ViInt16 exponent);

ViStatus _VI_FUNC ri3151_exponential_wave (ViSession instrHandle, ViReal64 frequency,
                                      ViReal64 amplitude, ViReal64 offset, 
                                      ViReal64 exponent);

ViStatus _VI_FUNC ri3151_dc_signal (ViSession instrHandle, ViInt16 percent_amplitude);

/*---------------------------------------- Arbitrary Waveforms --------------------------------*/

ViStatus _VI_FUNC ri3151_define_arb_segment (ViSession instrHandle, ViInt16 segment_number,
                                            ViInt32 segment_size);

ViStatus _VI_FUNC ri3151_delete_segments (ViSession instrHandle, ViInt16 segment_number,
                                            ViBoolean delete_all_segments);

ViStatus _VI_FUNC ri3151_load_arb_data (ViSession instrHandle, ViInt16 segment_number,
											ViUInt16 *data_pts, ViInt32 number_of_points);

ViStatus _VI_FUNC ri3151_load_and_shift_arb_data (ViSession instrHandle,
                                                  ViInt16 segmentNumber,
                                                  ViInt16 dataPointArray[],
                                                  ViInt32 numberofPoints);

ViStatus _VI_FUNC ri3151_load_ascii_file (ViSession instrHandle, ViInt16 segment_number,
											ViString file_name, ViInt32 number_of_points);

ViStatus _VI_FUNC ri3151_load_wavecad_file (ViSession instrHandle, ViChar waveCADFileName[]);
ViStatus _VI_FUNC ri3151_load_wavecad_wave_file (ViSession instrHandle,
                                        		 ViInt16 segmentNumber,
                                        		 ViChar waveCADWaveformFileName[]);


ViStatus _VI_FUNC ri3151_output_arb_waveform (ViSession instrHandle, ViInt16 segment_number,
                                              ViReal64 sampling_clock, ViReal64 amplitude,
                                              ViReal64 offset, ViInt16 clock_source);

/*----------------------------------------------------------------------------------------------*/


/*----------------------------------------- Sequence Waveforms ---------------------------------*/


ViStatus _VI_FUNC ri3151_define_sequence (ViSession instrHandle,
                                                  ViInt16 number_of_steps,
                                                  ViInt16 *segment_number,
                                                  ViInt32 *repeat_number);

ViStatus _VI_FUNC ri3151_delete_sequence (ViSession instrHandle);

ViStatus _VI_FUNC ri3151_output_sequence_waveform (ViSession instrHandle,
                                                    ViReal64 sampling_clock,
                                                    ViReal64 amplitude,
                                                    ViReal64 offset,
                                                    ViBoolean sequence_mode);

/*----------------------------------------------------------------------------------------------*/

/*---------------------------------- Example ---------------------------------------------------*/

ViStatus _VI_FUNC ri3151_example_generate_seq_waveform (ViSession instrHandle);

/*----------------------------------------------------------------------------------------------*/

ViStatus _VI_FUNC ri3151_filter (ViSession instrHandle, ViInt16 lp_filter);

ViStatus _VI_FUNC ri3151_amplitude_modulation (ViSession instrHandle, ViInt16 percent_amplitude,
                                                ViInt32 internal_frequency);


ViStatus _VI_FUNC ri3151_output (ViSession instrHandle, ViBoolean output_switch);

ViStatus _VI_FUNC ri3151_operating_mode (ViSession instrHandle, ViInt16 operating_mode);

ViStatus _VI_FUNC ri3151_select_waveform_mode (ViSession instrHandle, ViInt16 waveform_type);
ViStatus _VI_FUNC ri3151_set_frequency (ViSession instrHandle, ViReal64 frequency);
ViStatus _VI_FUNC ri3151_set_amplitude (ViSession instrHandle, ViReal64 amplitude);
ViStatus _VI_FUNC ri3151_set_offset (ViSession instrHandle, ViReal64 offset);

ViStatus _VI_FUNC ri3151_trigger_source (ViSession instrHandle, ViInt16 triggerSource);
ViStatus _VI_FUNC ri3151_trigger_rate (ViSession instrHandle, ViReal64 triggerRate);
ViStatus _VI_FUNC ri3151_trigger_slope (ViSession instrHandle, ViBoolean triggerSlope);
ViStatus _VI_FUNC ri3151_trigger_delay (ViSession instrHandle, ViInt32 triggerDelay);
ViStatus _VI_FUNC ri3151_output_trigger (ViSession instrHandle, ViInt16 outputTriggerSource,
                                         ViInt16 outputTriggerLine, ViInt32 BITTriggerPoint,
                                         ViInt16 LCOMSegment);
ViStatus _VI_FUNC ri3151_output_sync (ViSession instrHandle, ViInt16 SYNCPulseSource,
                                      ViInt32 BITSYNCPoint, ViInt16 LCOMSegment);
ViStatus _VI_FUNC ri3151_immediate_trigger (ViSession instrHandle);
ViStatus _VI_FUNC ri3151_phase_master (ViSession instrHandle);
ViStatus _VI_FUNC ri3151_phase_slave (ViSession instrHandle, ViInt16 phaseOffset);
ViStatus _VI_FUNC ri3151_phase_lock_loop (ViSession instrHandle, ViBoolean phaseLockLoop);
ViStatus _VI_FUNC ri3151_pll_phase (ViSession instrHandle, ViReal64 phase);
ViStatus _VI_FUNC ri3151_pll_fine_phase(ViSession instrHandle, ViReal64 phase);

ViStatus _VI_FUNC ri3151_burst_mode (ViSession instrHandle, ViInt32 number_of_cycles);


ViStatus _VI_FUNC ri3151_status_query (ViSession vi, ViReal64 *voltage_value,
                                       ViReal64 *frequency_value, ViReal64 *offset_value,
                                       ViInt16 *filter_type);
ViStatus _VI_FUNC ri3151_mode_query (ViSession instrHandle, ViInt16 *presentWaveformMode,
                            ViInt16 *presentStandardWaveform);
ViStatus _VI_FUNC ri3151_pll_query (ViSession instrHandle, ViBoolean *phaseLockState,
                           ViReal64 *coarsePhase, ViReal64 *finePhase, ViReal64 *extFrequency);
ViStatus _VI_FUNC ri3151_clear (ViSession instrHandle);
ViStatus _VI_FUNC ri3151_trigger (ViSession instrHandle);
ViStatus _VI_FUNC ri3151_poll (ViSession instrHandle, ViInt16 *statusByte);
ViStatus _VI_FUNC ri3151_reset (ViSession instrHandle);
ViStatus _VI_FUNC ri3151_read_status_byte (ViSession instrHandle, ViInt16 *statusByte);
ViStatus _VI_FUNC ri3151_set_SRE (ViSession instrHandle, ViInt16 SRERegister);
ViStatus _VI_FUNC ri3151_read_SRE (ViSession instrHandle, ViInt16 *SRERegister);
ViStatus _VI_FUNC ri3151_read_ESR (ViSession instrHandle, ViInt16 *ESRRegister);
ViStatus _VI_FUNC ri3151_set_ESE (ViSession instrHandle, ViInt16 ESERegister);
ViStatus _VI_FUNC ri3151_read_ESE (ViSession instrHandle, ViInt16 *ESERegister);
ViStatus _VI_FUNC ri3151_self_test (ViSession instrHandle, ViInt16 *passFail,
                           ViChar selfTestMessage[]);
ViStatus _VI_FUNC ri3151_revision_query (ViSession instrHandle, ViString driverRevision,
                                ViString firmwareRevision);
ViStatus _VI_FUNC ri3151_error_query (ViSession instrHandle, ViInt32 *error,
                             ViString errorMessage);
ViStatus _VI_FUNC ri3151_error_message (ViSession instrHandle, ViStatus errorReturnValue,
                               ViString errorMessage);
ViStatus _VI_FUNC ri3151_close (ViSession instrHandle);


/*--------------------------------------------------------------------------------------------*/


#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

#endif
