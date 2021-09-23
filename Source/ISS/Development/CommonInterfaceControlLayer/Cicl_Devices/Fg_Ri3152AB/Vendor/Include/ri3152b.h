
/*=========================================================================*/
/* LabWindows/CVI VXIplug&play Instrument Driver Include File 
/* Instrument:  Racal Instruments 3152B Arbitrary Waveform Generator
/* File:        ri3152b.h
/* Revision:    1.0.21
/* Date:        5/05/2015
/*=========================================================================*/
#ifndef __ri3152b_HEADER
#define __ri3152b_HEADER

#include <vpptype.h>

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif


/* = GLOBAL CONSTANT DECLARATIONS =========================================== */
/* Maximum number of ri3152b instances = 12 = max VXI slots per chassis */
#define RI3152B_MAX_INSTR                 ((ViInt16) 12)

#define REG_BASE_EN                        //for Registry base support

#define RI3152B_NO_SESSION                ((ViSession) 0)

/*----- Frequency Max and Min limits for various standard waveforms --------*/
#define RI3152B_MIN_FREQ                  ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ                  ((ViReal64) 100.0E6)
#define RI3152B_MIN_FREQ_A                ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_A                ((ViReal64) 50.0E6)
#define RI3152B_MIN_FREQ_SIN_A            ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_SIN_A            ((ViReal64) 50.0E6)
#define RI3152B_MIN_FREQ_TRI_A            ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_TRI_A            ((ViReal64) 1.0E6)
#define RI3152B_MIN_FREQ_SQU_A            ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_SQU_A            ((ViReal64) 50.0E6)
#define RI3152B_MIN_FREQ_PULS_A           ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_PULS_A           ((ViReal64) 1.0E6)
#define RI3152B_MIN_FREQ_RAMP_A           ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_RAMP_A           ((ViReal64) 1.0E6)
#define RI3152B_MIN_FREQ_SINC_A           ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_SINC_A           ((ViReal64) 1.0E6)
#define RI3152B_MIN_FREQ_EXP_A            ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_EXP_A            ((ViReal64) 1.0E6)
#define RI3152B_MIN_FREQ_GAU_A            ((ViReal64) 100.0E-6)
#define RI3152B_MAX_FREQ_GAU_A            ((ViReal64) 1.0E6)
/*---------------------------------------------------------------------------*/


/*----- General Max and Min limits for various standard waveforms -----------*/
#define RI3152B_MIN_AMPLITUDE             ((ViReal64) 10.0E-3)
#define RI3152B_MAX_AMPLITUDE             ((ViReal64) 16.0)

#define RI3152B_MIN_OFFSET                ((ViReal64) -7.992)
#define RI3152B_MAX_OFFSET                ((ViReal64) 7.992)
#define RI3152B_MIN_OFFSET_A              ((ViReal64) -7.19)
#define RI3152B_MAX_OFFSET_A              ((ViReal64) 7.19)

#define RI3152B_MAX_PEAK_VOLTS		      ((ViReal64) 8.0)
#define RI3152B_MIN_PEAK_VOLTS		      ((ViReal64) -8.0)

#define RI3152B_MIN_PHASE                 ((ViReal64) 0.0)
#define RI3152B_MAX_PHASE                 ((ViReal64) 360.0)

#define RI3152B_POWER_1                   ((ViInt16) 1)
#define RI3152B_POWER_2                   ((ViInt16) 2)
#define RI3152B_POWER_3                   ((ViInt16) 3)
#define RI3152B_POWER_4                   ((ViInt16) 4)
#define RI3152B_POWER_5                   ((ViInt16) 5)
#define RI3152B_POWER_6                   ((ViInt16) 6)
#define RI3152B_POWER_7                   ((ViInt16) 7)
#define RI3152B_POWER_8                   ((ViInt16) 8)
#define RI3152B_POWER_9                   ((ViInt16) 9)

#define RI3152B_MIN_DUTY_CYCLE            ((ViReal64) 0) 
#define RI3152B_MIN_DUTY_CYCLE_A          ((ViReal64) 1)
#define RI3152B_MAX_DUTY_CYCLE            ((ViReal64) 99.99)
#define RI3152B_MAX_DUTY_CYCLE_A          ((ViReal64) 99.9)

#define RI3152B_MIN_DELAY_PULSE           ((ViReal64) 0.0)
#define RI3152B_MAX_DELAY_PULSE           ((ViReal64) 99.999)
#define RI3152B_MAX_DELAY_PULSE_A         ((ViReal64) 99.9)

#define RI3152B_MIN_RISE_TIME_PULSE       ((ViReal64) 0.0)
#define RI3152B_MAX_RISE_TIME_PULSE       ((ViReal64) 99.999)
#define RI3152B_MAX_RISE_TIME_PULSE_A     ((ViReal64) 99.9)

#define RI3152B_MIN_HIGH_TIME_PULSE       ((ViReal64) 0.0)
#define RI3152B_MAX_HIGH_TIME_PULSE       ((ViReal64) 99.999)
#define RI3152B_MAX_HIGH_TIME_PULSE_A     ((ViReal64) 99.9)

#define RI3152B_MIN_FALL_TIME_PULSE       ((ViReal64) 0.0)
#define RI3152B_MAX_FALL_TIME_PULSE       ((ViReal64) 99.999)
#define RI3152B_MAX_FALL_TIME_PULSE_A     ((ViReal64) 99.9)

#define RI3152B_MIN_DELAY_RAMP            ((ViReal64) 0.0)
#define RI3152B_MAX_DELAY_RAMP            ((ViReal64) 99.99)
#define RI3152B_MAX_DELAY_RAMP_A          ((ViReal64) 99.9)

#define RI3152B_MIN_RISE_TIME_RAMP        ((ViReal64) 0.0)
#define RI3152B_MAX_RISE_TIME_RAMP        ((ViReal64) 99.99)
#define RI3152B_MAX_RISE_TIME_RAMP_A      ((ViReal64) 99.9)

#define RI3152B_MIN_FALL_TIME_RAMP        ((ViReal64) 0.0)
#define RI3152B_MAX_FALL_TIME_RAMP        ((ViReal64) 99.99)
#define RI3152B_MAX_FALL_TIME_RAMP_A      ((ViReal64) 99.9)

#define RI3152B_MIN_CYCLE_NUMBER          ((ViInt16) 4)
#define RI3152B_MAX_CYCLE_NUMBER          ((ViInt16) 100)

#define RI3152B_MIN_EXPONENT_EXP          ((ViInt16) -100)
#define RI3152B_MAX_EXPONENT_EXP          ((ViInt16) 100)
#define RI3152B_MIN_EXPONENT_EXP_A        ((ViInt16) -200)
#define RI3152B_MAX_EXPONENT_EXP_A        ((ViInt16) 200)

#define RI3152B_MIN_EXPONENT_GAU          ((ViInt16) 1)
#define RI3152B_MAX_EXPONENT_GAU          ((ViInt16) 200)

#define RI3152B_MIN_PERCENT_AMP           ((ViInt16) -100)
#define RI3152B_MAX_PERCENT_AMP           ((ViInt16) 100)

#define RI3152B_MIN_DC_LEVEL              ((ViReal64) -8)
#define RI3152B_MAX_DC_LEVEL              ((ViReal64) 8)

#define RI3152B_OUTPUT_ON                 ((ViBoolean) 1)
#define RI3152B_OUTPUT_OFF                ((ViBoolean) 0)

#define RI3152B_OUTPUT_IMPED_MIN          ((ViReal64) 50.0)
#define RI3152B_OUTPUT_IMPED_MAX          ((ViReal64) 1.0E6)  

#define RI3152B_MODE_FAST                 ((ViBoolean) 1)
#define RI3152B_MODE_NORMAL               ((ViBoolean) 0)

#define RI3152B_FORMAT_NORMAL             ((ViBoolean) 0)
#define RI3152B_FORMAT_SWAP               ((ViBoolean) 1)

#define RI3152B_PHASE_OFFSET_MIN	      ((ViInt32)  0) 
/*-------------------------------------------------------------------------------*/

/*---------------------------------Ref Clock Source parameters-------------------*/
#define RI3152B_REF_SOURCE_INT            ((ViInt16) 0)
#define RI3152B_REF_SOURCE_EXT            ((ViInt16) 1)
#define RI3152B_REF_SOURCE_CLK10          ((ViInt16) 2)
/*-------------------------------------------------------------------------------*/


/*------------------------------ Filter parameters ------------------------------*/
#define RI3152B_FILTER_OFF                ((ViInt16) 0)
#define RI3152B_FILTER_20MHZ              ((ViInt16) 1)
#define RI3152B_FILTER_25MHZ              ((ViInt16) 2)
#define RI3152B_FILTER_50MHZ              ((ViInt16) 3)
#define RI3152B_FILTER_60MHZ              ((ViInt16) 4)
#define RI3152B_FILTER_120MHZ             ((ViInt16) 5) 
/*-------------------------------------------------------------------------------*/

/*------------------------------ Operation Modes --------------------------------*/
#define RI3152B_MODE_CONT                 ((ViInt16) 0)
#define RI3152B_MODE_TRIG                 ((ViInt16) 1)
#define RI3152B_MODE_GATED                ((ViInt16) 2)
#define RI3152B_MODE_BURST                ((ViInt16) 3)
/*--------------------------------------------------------------------------------*/

/*------------------------- Trigger Parameters -----------------------------------*/
#define RI3152B_TRIGGER_INTERNAL          ((ViInt16) 0)
#define RI3152B_TRIGGER_EXTERNAL          ((ViInt16) 1)

#define RI3152B_TRIGGER_TTLTRG0		      ((ViInt16) 2)
#define RI3152B_TRIGGER_TTLTRG1		      ((ViInt16) 3)
#define RI3152B_TRIGGER_TTLTRG2		      ((ViInt16) 4)
#define RI3152B_TRIGGER_TTLTRG3		      ((ViInt16) 5)
#define RI3152B_TRIGGER_TTLTRG4		      ((ViInt16) 6)
#define RI3152B_TRIGGER_TTLTRG5		      ((ViInt16) 7)
#define RI3152B_TRIGGER_TTLTRG6		      ((ViInt16) 8)
#define RI3152B_TRIGGER_TTLTRG7		      ((ViInt16) 9)
#define RI3152B_TRIGGER_ECLTRG0		      ((ViInt16) 10)
#define RI3152B_TRIGGER_ECLTRG1		      ((ViInt16) 11)  
#define RI3152B_TRIGGER_BUS			      ((ViInt16) 12)
#define RI3152B_TRIGGER_NONE	          ((ViInt16) 13)

#define RI3152B_TRIGGER_BIT			      ((ViInt16) 2)
#define RI3152B_TRIGGER_LCOM		      ((ViInt16) 3)

#define RI3152B_SLOPE_POS			      (VI_TRUE)
#define RI3152B_SLOPE_NEG			      (VI_FALSE)

#define RI3152B_TRIGGER_SLOPE_NEG	      ((ViInt16) 0) 
#define RI3152B_TRIGGER_SLOPE_POS	      ((ViInt16) 1)

#define RI3152B_TRIGGER_RATE_MIN          ((ViReal64) 1.0E-6)
#define RI3152B_TRIGGER_RATE_MAX          ((ViReal64) 20.0)
#define RI3152B_TRIGGER_RATE_MIN_A        ((ViReal64) 60E-6)
#define RI3152B_TRIGGER_RATE_MAX_A        ((ViReal64) 1000.0)

#define RI3152B_TRIGGER_LEVEL_MIN         ((ViReal64) -10.0)
#define RI3152B_TRIGGER_LEVEL_MAX         ((ViReal64) 10.0)	

#define RI3152B_TRIGGER_DELAY_MIN	      ((ViInt32) 10)
#define RI3152B_TRIGGER_DELAY_MAX	      ((ViInt32) 2E6)

#define RI3152B_TRIGGER_COUNT_MIN	      ((ViInt32)  1)
#define RI3152B_TRIGGER_COUNT_MAX	      ((ViInt32) 1000000)

#define RI3152B_BURST_INTERNAL            ((ViInt16) 0)
#define RI3152B_BURST_EXTERNAL            ((ViInt16) 1)
#define RI3152B_BURST_BUS                 ((ViInt16) 2)

#define RI3152B_BURST_RATE_MIN            ((ViReal64) 20E-6)
#define RI3152B_BURST_RATE_MAX            ((ViReal64) 999)

#define RI3152B_BURST_MIN_CYCLE           ((ViInt32) 1)
#define RI3152B_BURST_MAX_CYCLE           ((ViInt32) 1000000)

#define RI3152B_MIN_SYNC_POS_POINT        ((ViInt32) 0)
#define RI3152B_MIN_SYNC_POS_POINT_A      ((ViInt32) 0)
#define RI3152B_MAX_SYNC_POS_POINT_LEG    ((ViInt32) 500000)    

#define RI3152B_GATE_MODE_LEV             ((ViInt16) 0)
#define RI3152B_GATE_MODE_TRAN            ((ViInt16) 1)

#define RI3152B_RE_TRIGGER_TIME_MIN       ((ViReal64) 100.0E-9)
#define RI3152B_RE_TRIGGER_TIME_MAX       ((ViReal64) 20.0)	

#define RI3152B_TRIGGER_DELAY_TIME_MIN    ((ViReal64) 100.0E-9)
#define RI3152B_TRIGGER_DELAY_TIME_MAX    ((ViReal64) 20.0)	
/*--------------------------------------------------------------------------------*/

/*------------------------- Waveform Mode Selection Paramaters -------------------*/
#define RI3152B_MODE_STD                  ((ViInt16) 0)
#define RI3152B_MODE_ARB                  ((ViInt16) 1)
#define RI3152B_MODE_SWEEP                ((ViInt16) 2)
#define RI3152B_MODE_SEQ                  ((ViInt16) 3)   
#define RI3152B_MODE_MOD                  ((ViInt16) 4)
#define RI3152B_MODE_DPUL                 ((ViInt16) 5)
#define RI3152B_MODE_HALF                 ((ViInt16) 6)
#define RI3152B_MODE_COUN                 ((ViInt16) 7)
/*--------------------------------------------------------------------------------*/

/*------------------------- Standard Waveform Indication Values ------------------*/
#define RI3152B_NON_STANDARD		      ((ViInt16) 0)
#define RI3152B_SINE				      ((ViInt16) 1)
#define RI3152B_TRIANGLE			      ((ViInt16) 2)
#define RI3152B_SQUARE				      ((ViInt16) 3)
#define RI3152B_PULSE				      ((ViInt16) 4)
#define RI3152B_RAMP				      ((ViInt16) 5)
#define RI3152B_SINC				      ((ViInt16) 6)
#define RI3152B_GAUSSIAN			      ((ViInt16) 7)
#define RI3152B_EXPONENTIAL			      ((ViInt16) 8)
#define RI3152B_DC					      ((ViInt16) 9)
#define RI3152B_NOISE			          ((ViInt16) 10)
/*--------------------------------------------------------------------------------*/

/*------------------------- Amplitude Modulation Parameters ----------------------*/
#define RI3152B_MIN_AM_PERCENT            ((ViInt16) 0)
#define RI3152B_MAX_AM_PERCENT            ((ViInt16) 100)
#define RI3152B_MIN_AM_PERCENT_A          ((ViInt16) 1)
#define RI3152B_MAX_AM_PERCENT_A          ((ViInt16) 200)

#define RI3152B_MIN_AM_FREQ               ((ViInt32) 1)
#define RI3152B_MAX_AM_FREQ               ((ViInt32) 1.0E6)
#define RI3152B_MIN_AM_FREQ_A             ((ViInt32) 10)
#define RI3152B_MAX_AM_FREQ_A             ((ViInt32) 500)
/*--------------------------------------------------------------------------------*/

/*------------------------- Arbitrary Amplitude Modulation Parameters ------------*/
#define RI3152B_MIN_ARB_AM_PERCENT        ((ViReal64) 100)
#define RI3152B_MAX_ARB_AM_PERCENT        ((ViReal64) 200)
/*--------------------------------------------------------------------------------*/

/*------------------------- SYNC Pulse Paramaters --------------------------------*/
#define RI3152B_SYNC_OFF			      ((ViInt16) 0)
#define RI3152B_SYNC_BIT			      ((ViInt16) 1)
#define RI3152B_SYNC_LCOM			      ((ViInt16) 2)
#define RI3152B_SYNC_SSYNC			      ((ViInt16) 3)
#define RI3152B_SYNC_HCLOCK			      ((ViInt16) 4)
#define RI3152B_SYNC_PULSE			      ((ViInt16) 5)
#define RI3152B_SYNC_ZEROCROSS		      ((ViInt16) 6)  
/*--------------------------------------------------------------------------------*/

/*-------------------------  Arbitrary Waveform Constants ------------------------*/
#define RI3152B_MIN_SEG_NUMBER           ((ViInt16) 1)
#define RI3152B_MAX_SEG_NUMBER_A         ((ViInt16) 4096)
#define RI3152B_MAX_SEG_NUMBER           ((ViInt16) 10000) 

#define RI3152B_MIN_WIDTH      	         ((ViInt32) 4)
#define RI3152B_MAX_WIDTH      	         ((ViInt32) 4E6)  
#define RI3152B_MIN_WIDTH_A      	     ((ViInt32) 2)		
#define RI3152B_MAX_WIDTH_A      	     ((ViInt32) 500)

#define RI3152B_MIN_SEGMENT_SIZE_A       ((ViInt32) 10)
#define RI3152B_MAX_SEGMENT_SIZE_LEG     ((ViInt32) 500000)   
#define RI3152B_MIN_SEGMENT_SIZE         ((ViInt32) 16) 
#define RI3152B_MAX_64K_SEG_SIZE 	     ((ViInt32) 65536)
#define RI3152B_MAX_256K_SEG_SIZE 	     ((ViInt32) 262144)  
#define RI3152B_MAX_512K_SEG_SIZE	     ((ViInt32) 524288)
#define RI3152B_MAX_1M_SEG_SIZE	         ((ViInt32) 1E6)
#define RI3152B_MAX_2M_SEG_SIZE	         ((ViInt32) 2E6)
#define RI3152B_MAX_4M_SEG_SIZE	         ((ViInt32) 4E6)  

#define RI3152B_DELETE_ALL_NO            ((ViBoolean) 0)
#define RI3152B_DELETE_ALL_YES           ((ViBoolean) 1)

#define RI3152B_MIN_SAMP_CLK             ((ViReal64) 10.0E-6)
#define RI3152B_MAX_SAMP_CLK             ((ViReal64) 250.0E6)
#define RI3152B_MIN_SAMP_CLK_A           ((ViReal64) 100E-3)
#define RI3152B_MAX_SAMP_CLK_A           ((ViReal64) 100E6)

#define RI3152B_CLK_SOURCE_INT           ((ViInt16) 0)
#define RI3152B_CLK_SOURCE_EXT           ((ViInt16) 1)
#define RI3152B_CLK_SOURCE_ECLTRG0       ((ViInt16) 2)
#define RI3152B_CLK_SOURCE_LBUS          ((ViInt16) 3)  
/*---------------------------------------------------------------------------------*/

/*-------------------------  Sequential Waveform Constants -------------------------*/
#define RI3152B_MIN_NUM_STEPS		     ((ViInt16) 1)
#define RI3152B_MAX_NUM_STEPS		     ((ViInt16) 4096)

#define RI3152B_MIN_NUM_REPEAT		     ((ViInt32) 1)
#define RI3152B_MAX_NUM_REPEAT		     ((ViInt32) 1000000)

#define RI3152B_SEQ_AUTO			     (VI_FALSE)
#define RI3152B_SEQ_TRIG		         (VI_TRUE)

#define RI3152B_SEQ_MODE_AUTO            ((ViInt16) 0)
#define RI3152B_SEQ_MODE_TRIG            ((ViInt16) 1)
#define RI3152B_SEQ_MODE_STEP            ((ViInt16) 2)
#define RI3152B_SEQ_MODE_MIX             ((ViInt16) 3)

#define RI3152B_NUM_SEQUENCE_MIN		 ((ViInt16) 1)
#define RI3152B_NUM_SEQUENCE_MAX		 ((ViInt16) 10) 
/*---------------------------------------------------------------------------------*/


/*-------------------------Sequence Sync Type Constants----------------------------*/
#define RI3152B_SEQ_SYNC_LCOM            ((ViInt16) 0)
#define RI3152B_SEQ_SYNC_BIT             ((ViInt16) 1)
/*---------------------------------------------------------------------------------*/


/*------------------------  3152 Phase Lock Loop Constants ------------------------*/
#define RI3152B_PLL_ON				     (VI_TRUE)
#define RI3152B_PLL_OFF				     (VI_FALSE)

#define RI3152B_PLL_MIN_PHASE		     ((ViReal64) -180.0)
#define RI3152B_PLL_MAX_PHASE		     ((ViReal64) 180.0)

#define RI3152B_MIN_FINE_PHASE		     ((ViReal64) -36.0)
#define RI3152B_MAX_FINE_PHASE		     ((ViReal64) 36.0)
/*----------------------------------------------------------------------------------*/


/*----------------------------- 3152B Synchronization Commands ---------------------*/
#define RI3152B_PHASE_SOURCE_MASTER      ((ViInt16) 0)
#define RI3152B_PHASE_SOURCE_SLAVE       ((ViInt16) 1) 
/*----------------------------------------------------------------------------------*/


/*------------------------------Wave Resolution Constants---------------------------*/
#define RI3152B_WAVE_RES_16BIT           ((ViInt16) 1)
#define RI3152B_WAVE_RES_12BIT           ((ViInt16) 0) 
/*---------------------------------------------------------------------------------*/

/*-------------------------------File Range Format ------------------------------------------*/
#define RI3152B_MIN_16BIT                ((ViInt16) -32768)
#define RI3152B_MAX_16BIT                ((ViInt16) 32767) 

#define RI3152B_MIN_12BIT                ((ViInt16) -2048)
#define RI3152B_MAX_12BIT                ((ViInt16) 2047) 
/*---------------------------------------------------------------------------------------------*/


/*-------------------------Modulation Type Constants-------------------------------*/
#define RI3152B_MOD_TYPE_OFF             ((ViInt16) 0)
#define RI3152B_MOD_TYPE_AM              ((ViInt16) 1) 
#define RI3152B_MOD_TYPE_FM              ((ViInt16) 2) 
#define RI3152B_MOD_TYPE_SWE             ((ViInt16) 3) 
#define RI3152B_MOD_TYPE_FSK             ((ViInt16) 4) 
#define RI3152B_MOD_TYPE_ASK             ((ViInt16) 5) 
#define RI3152B_MOD_TYPE_PSK             ((ViInt16) 6) 
#define RI3152B_MOD_TYPE_FHOP            ((ViInt16) 7) 
#define RI3152B_MOD_TYPE_AHOP            ((ViInt16) 8) 
#define RI3152B_MOD_TYPE_3D              ((ViInt16) 9)
#define RI3152B_MOD_TYPE_ARB_AM          ((ViInt16) 10)  
/*---------------------------------------------------------------------------------*/


/*---------------------Common Modulation Parameters--------------------------------*/
#define RI3152B_MOD_CARR_FREQ_MIN        ((ViReal64) 10)
#define RI3152B_MOD_CARR_FREQ_MAX        ((ViReal64) 100E6)
/*---------------------------------------------------------------------------------*/


/*----------------------Modulation Carrier Baseline Constants----------------------*/
#define RI3152B_CARR_BASELINE_CARR       ((ViInt16) 0)
#define RI3152B_CARR_BASELINE_DC         ((ViInt16) 1) 
/*---------------------------------------------------------------------------------*/


/*-----------------------AM Waveform Constants-------------------------------------*/
#define RI3152B_AM_WAVE_SINE		     ((ViInt16) 0)
#define RI3152B_AM_WAVE_TRI		         ((ViInt16) 1) 
#define RI3152B_AM_WAVE_SQU		         ((ViInt16) 2) 
#define RI3152B_AM_WAVE_RAMP		     ((ViInt16) 3)
/*----------------------------------------------------------------------------------*/

/*-----------------------ARB AM Waveform Constants-------------------------------------*/
#define RI3152B_ARB_AM_WAVE_SINE		  ((ViInt16) 0)
#define RI3152B_ARB_AM_WAVE_TRI		      ((ViInt16) 1) 
#define RI3152B_ARB_AM_WAVE_SQU		      ((ViInt16) 2) 
#define RI3152B_ARB_AM_WAVE_RAMP		  ((ViInt16) 3)
/*----------------------------------------------------------------------------------*/


/*------------------------  3152 Sweep Constants ------------------------*/
#define RI3152B_SWEEP_MIN_TIME           ((ViReal64) 1.4E-6)
#define RI3152B_SWEEP_MAX_TIME           ((ViReal64) 40.0)
#define RI3152B_SWEEP_MIN_TIME_A         ((ViReal64) 100E-3)
#define RI3152B_SWEEP_MAX_TIME_A         ((ViReal64) 10E3)

#define RI3152B_SWEEP_MIN_STEP           ((ViInt16) 10)
#define RI3152B_SWEEP_MAX_STEP           ((ViInt16) 2000)
#define RI3152B_SWEEP_MAX_STEP_A         ((ViInt16) 1000)

#define RI3152B_SWEEP_MIN_FREQ           ((ViReal64) 10.0)
#define RI3152B_SWEEP_MAX_FREQ           ((ViReal64) 100.0E6)
#define RI3152B_SWEEP_MIN_FREQ_A         ((ViReal64) 1E-3)
#define RI3152B_SWEEP_MAX_FREQ_A         ((ViReal64) 9E6)

#define RI3152B_SWEEP_MIN_FREQ_STOP      ((ViReal64) 10.0)
#define RI3152B_SWEEP_MAX_FREQ_STOP      ((ViReal64) 100.0E6)
#define RI3152B_SWEEP_MIN_FREQ_STOP_A    ((ViReal64) 10E-3)
#define RI3152B_SWEEP_MAX_FREQ_STOP_A    ((ViReal64) 10E6)

#define RI3152B_SWEEP_MIN_FREQ_RAST      ((ViReal64) 10.0E-6)
#define RI3152B_SWEEP_MAX_FREQ_RAST      ((ViReal64) 250.0E6)
#define RI3152B_SWEEP_MIN_FREQ_RAST_A    ((ViReal64) 100E-3)
#define RI3152B_SWEEP_MAX_FREQ_RAST_A    ((ViReal64) 1E8)

#define RI3152B_SWEEP_DIR_UP             ((ViInt16) 0)
#define RI3152B_SWEEP_DIR_DOWN           ((ViInt16) 1)

#define RI3152B_SWEEP_SPAC_LIN           ((ViInt16) 0)
#define RI3152B_SWEEP_SPAC_LOG           ((ViInt16) 1)
/*-------------------------------------------------------------------------*/


/*-----------------------FM Modulation Parameters---------------------------*/
#define RI3152B_FM_DEV_MIN               ((ViReal64) 10.0E-3)
#define RI3152B_FM_DEV_MAX               ((ViReal64) 100E6)

#define RI3152B_FM_FREQ_MIN              ((ViReal64) 10.0E-3)
#define RI3152B_FM_FREQ_MAX              ((ViReal64) 350.0E3)

#define RI3152B_FM_SCLK_MIN              ((ViReal64) 1.0)
#define RI3152B_FM_SCLK_MAX              ((ViReal64) 2.5E6)

#define RI3152B_FM_MARKER_MIN            ((ViReal64) 10.0E-3)
#define RI3152B_FM_MARKER_MAX            ((ViReal64) 100.0E6)
/*--------------------------------------------------------------------------*/


/*------------------------FM Waveform Constants-----------------------------*/
#define RI3152B_FM_WAVE_SINE		     ((ViInt16) 0)
#define RI3152B_FM_WAVE_TRI		         ((ViInt16) 1) 
#define RI3152B_FM_WAVE_SQU		         ((ViInt16) 2) 
#define RI3152B_FM_WAVE_RAMP		     ((ViInt16) 3)
#define RI3152B_FM_WAVE_ARB		         ((ViInt16) 4)  
/*--------------------------------------------------------------------------*/


/*------------------------Frequency Shift Keying Parameters-----------------*/
#define RI3152B_FSK_SHIFT_FREQ_MIN       ((ViReal64) 10.0E-3)
#define RI3152B_FSK_SHIFT_FREQ_MAX       ((ViReal64) 100.0E6)

#define RI3152B_FSK_BAUD_MIN             ((ViReal64) 1.0)
#define RI3152B_FSK_BAUD_MAX             ((ViReal64) 10.0E6)

#define RI3152B_FSK_MARKER_MIN		     ((ViInt16) 1)
#define RI3152B_FSK_MARKER_MAX		     ((ViInt16) 4000)

#define RI3152B_MIN_FSK_LENGTH		     ((ViInt16) 1)
#define RI3152B_MAX_FSK_LENGTH		     ((ViInt16) 4000)
/*--------------------------------------------------------------------------*/


/*------------------------ASK Modulation Parameters-------------------------*/
#define RI3152B_ASK_AMPL_START_MIN       ((ViReal64) 0)
#define RI3152B_ASK_AMPL_START_MAX       ((ViReal64) 16)

#define RI3152B_ASK_AMPL_SHIFT_MIN       ((ViReal64) 0)
#define RI3152B_ASK_AMPL_SHIFT_MAX       ((ViReal64) 16)

#define RI3152B_ASK_BAUD_MIN             ((ViReal64) 1)
#define RI3152B_ASK_BAUD_MAX             ((ViReal64) 2.5E6)

#define RI3152B_ASK_MARKER_MIN		     ((ViInt16) 1)
#define RI3152B_ASK_MARKER_MAX		     ((ViInt16) 1000)

#define RI3152B_MIN_ASK_LENGTH		     ((ViInt16) 1)
#define RI3152B_MAX_ASK_LENGTH		     ((ViInt16) 1000)
/*--------------------------------------------------------------------------*/


/*-----------------------Phase Shift Key Modulation Parameters--------------*/
#define RI3152B_PSK_PHASE_START_MIN      ((ViReal64) 0.0)
#define RI3152B_PSK_PHASE_START_MAX      ((ViReal64) 360.0)

#define RI3152B_PSK_PHASE_SHIFT_MIN      ((ViReal64) 0.0)
#define RI3152B_PSK_PHASE_SHIFT_MAX      ((ViReal64) 360.0)

#define RI3152B_PSK_RATE_MIN             ((ViReal64) 1.0)
#define RI3152B_PSK_RATE_MAX             ((ViReal64) 10.0E6)

#define RI3152B_PSK_MARKER_MIN		     ((ViInt16) 1)
#define RI3152B_PSK_MARKER_MAX		     ((ViInt16) 4000)

#define RI3152B_MIN_PSK_LENGTH		     ((ViInt16) 1)
#define RI3152B_MAX_PSK_LENGTH		     ((ViInt16) 4000)
/*--------------------------------------------------------------------------*/


/*-------------------Frequency Hopping Modulation Parameters----------------*/
#define RI3152B_FHOP_DWELL_MODE_FIX	     ((ViInt16) 0)
#define RI3152B_FHOP_DWELL_MODE_VAR	     ((ViInt16) 1)

#define RI3152B_MIN_HOP_FREQ             ((ViReal64) 10.0e-3)   
#define RI3152B_MAX_HOP_FREQ             ((ViReal64) 100.0e6)   

#define RI3152B_FHOP_DWELL_TIME_MIN      ((ViReal64) 200.0E-9)
#define RI3152B_FHOP_DWELL_TIME_MAX      ((ViReal64) 20.0)

#define RI3152B_FHOP_MARKER_MIN		     ((ViInt16) 1)
#define RI3152B_FHOP_MARKER_MAX		     ((ViInt16) 5000)
									   
#define RI3152B_MIN_FIX_HOP_LENGTH	     ((ViInt16) 1)
#define RI3152B_MAX_FIX_HOP_LENGTH	     ((ViInt16) 5000)

#define RI3152B_MIN_VAR_HOP_LENGTH	     ((ViInt16) 2)
#define RI3152B_MAX_VAR_HOP_LENGTH	     ((ViInt16) 10000) 
/*--------------------------------------------------------------------------*/


/*-------------------Amplitude Hopping Modulation Parameters----------------*/
#define RI3152B_AHOP_DWELL_MODE_FIX	     ((ViInt16) 0)
#define RI3152B_AHOP_DWELL_MODE_VAR	     ((ViInt16) 1)

#define RI3152B_AHOP_DWELL_TIME_MIN      ((ViReal64) 200.0E-9)
#define RI3152B_AHOP_DWELL_TIME_MAX      ((ViReal64) 20.0)

#define RI3152B_AHOP_MARKER_MIN		     ((ViInt16) 1)
#define RI3152B_AHOP_MARKER_MAX		     ((ViInt16) 5000)

#define RI3152B_MIN_AHOP_LENGTH          ((ViInt16) 1)   
#define RI3152B_MAX_AHOP_LENGTH          ((ViInt16) 5000)

#define RI3152B_MIN_AHOP_AMPL            ((ViReal64) 10.0E-3)   
#define RI3152B_MAX_AHOP_AMPL            ((ViReal64) 16.0)
/*--------------------------------------------------------------------------*/


/*-----------------------3D Modulation Parameters---------------------------*/
#define RI3152B_3D_MARKER_MIN		     ((ViInt32) 1)
#define RI3152B_3D_MARKER_MAX		     ((ViInt32) 30000)

#define RI3152B_3D_SCLK_MIN              ((ViReal64) 1.0)
#define RI3152B_3D_SCLK_MAX              ((ViReal64) 2.5E6) 
/*--------------------------------------------------------------------------*/


/*---------------------Digital Pulse Parameters-----------------------------*/
#define RI3152B_DPULSE_DELAY_MIN         ((ViReal64) 0.0)
#define RI3152B_DPULSE_DELAY_MAX         ((ViReal64) 10.0)

#define RI3152B_DPULSE_DOUBLE_DELAY_MIN  ((ViReal64) 0.0)
#define RI3152B_DPULSE_DOUBLE_DELAY_MAX  ((ViReal64) 1.0E3)

#define RI3152B_DPULSE_HIGH_LEV_MIN      ((ViReal64) -7.992)
#define RI3152B_DPULSE_HIGH_LEV_MAX      ((ViReal64) 8.0)

#define RI3152B_DPULSE_LOW_LEV_MIN       ((ViReal64) -8.0)
#define RI3152B_DPULSE_LOW_LEV_MAX       ((ViReal64) 7.992)

#define RI3152B_DPULSE_HIGH_MIN          ((ViReal64) 0.0)
#define RI3152B_DPULSE_HIGH_MAX          ((ViReal64) 1.0E3)

#define RI3152B_DPULSE_PER_MIN           ((ViReal64) 80.0E-9)

#define RI3152B_DPULSE_RISE_TIME_MIN     ((ViReal64) 0.0)
#define RI3152B_DPULSE_RISE_TIME_MAX     ((ViReal64) 1.0E3)

#define RI3152B_DPULSE_FALL_TIME_MIN     ((ViReal64) 0.0)
#define RI3152B_DPULSE_FALL_TIME_MAX     ((ViReal64) 1.0E3) 
/*--------------------------------------------------------------------------*/


/*------------------Digital Pulse Polarity Constants------------------------*/
#define RI3152B_DPULSE_POL_NORM		     ((ViInt16) 0)
#define RI3152B_DPULSE_POL_COMP		     ((ViInt16) 1)  
#define RI3152B_DPULSE_POL_INV		     ((ViInt16) 2)  
/*--------------------------------------------------------------------------*/


/*------------------Half Cycle Parameters-----------------------------------*/
#define RI3152B_HALF_CYCLE_DEL_MIN       ((ViReal64) 200.0E-9)
#define RI3152B_HALF_CYCLE_DEL_MAX       ((ViReal64) 20.0)

#define RI3152B_HALF_CYCLE_DCYCLE_MIN    ((ViReal64) 0.0)
#define RI3152B_HALF_CYCLE_DCYCLE_MAX    ((ViReal64) 99.99)

#define RI3152B_HALF_CYCLE_FREQ_MIN      ((ViReal64) 10.0E-3)
#define RI3152B_HALF_CYCLE_FREQ_MAX      ((ViReal64) 1.0E6)

#define RI3152B_HALF_CYCLE_PHASE_MIN     ((ViReal64) 0.0)
#define RI3152B_HALF_CYCLE_PHASE_MAX     ((ViReal64) 360.0)
/*--------------------------------------------------------------------------*/


/*-------------------Half Cycle Waveform Constants--------------------------*/
#define RI3152B_HALF_CYCLE_WAVE_SINE     ((ViInt16) 0)
#define RI3152B_HALF_CYCLE_WAVE_TRI      ((ViInt16) 1)
#define RI3152B_HALF_CYCLE_WAVE_SQU      ((ViInt16) 2) 
/*--------------------------------------------------------------------------*/


/*--------------------Counter Function Constants----------------------------*/
#define RI3152B_COUNT_FUNC_FREQ          ((ViInt16) 0)
#define RI3152B_COUNT_FUNC_PER           ((ViInt16) 1) 
#define RI3152B_COUNT_FUNC_APER          ((ViInt16) 2) 
#define RI3152B_COUNT_FUNC_PULSE         ((ViInt16) 3) 
#define RI3152B_COUNT_FUNC_TOT           ((ViInt16) 4) 
/*--------------------------------------------------------------------------*/


/*-------------------Counter Display Mode Constants-------------------------*/
#define RI3152B_COUNT_DISP_MODE_NORM     ((ViInt16) 0)
#define RI3152B_COUNT_DISP_MODE_HOLD     ((ViInt16) 1) 
/*--------------------------------------------------------------------------*/


/*--------------------Counter Parameters------------------------------------*/
#define RI3152B_COUNT_GATE_TIME_MIN      ((ViReal64) 100.0E-6)
#define RI3152B_COUNT_GATE_TIME_MAX      ((ViReal64) 1.0) 
/*--------------------------------------------------------------------------*/


/*--------------------Format Instrument Constants --------------------------*/
#define RI3152B_FORMAT_INSTR_LEG         ((ViInt16) 0)
#define RI3152B_FORMAT_INSTR_MOD         ((ViInt16) 1) 
/*--------------------------------------------------------------------------*/

/*-----------------Multi-Instr. Synchronization ----------------------------*/
#define RI3152B_COUPLE_MODE_MASTER       ((ViInt16) 0)
#define RI3152B_COUPLE_MODE_SLAVE        ((ViInt16) 1) 

#define RI3152B_COUPLE_DELAY_MIN         ((ViReal64) 0.0) 
#define RI3152B_COUPLE_DELAY_MAX         ((ViReal64) 360.0)
/*--------------------------------------------------------------------------*/

/*-------------- Couple Mode Path Constants---------------------------------*/
#define RI3152B_COUPLE_PATH_ADJ          ((ViInt16) 0)
#define RI3152B_COUPLE_PATH_ECLT         ((ViInt16) 1)
#define RI3152B_COUPLE_PATH_LBUS         ((ViInt16) 2) 
/*--------------------------------------------------------------------------*/

/*------------------------- Phase2 Source Constants ------------------------*/
#define RI3152B_PHASE2_SOURCE_EXT        ((ViInt16) 0)
#define RI3152B_PHASE2_SOURCE_ECLT0      ((ViInt16) 1)
#define RI3152B_PHASE2_SOURCE_TTLT0      ((ViInt16) 2)
#define RI3152B_PHASE2_SOURCE_TTLT1      ((ViInt16) 3)  
#define RI3152B_PHASE2_SOURCE_TTLT2      ((ViInt16) 4)  
#define RI3152B_PHASE2_SOURCE_TTLT3      ((ViInt16) 5)  
#define RI3152B_PHASE2_SOURCE_TTLT4      ((ViInt16) 6)  
#define RI3152B_PHASE2_SOURCE_TTLT5      ((ViInt16) 7)  
#define RI3152B_PHASE2_SOURCE_TTLT6      ((ViInt16) 8)  
#define RI3152B_PHASE2_SOURCE_TTLT7      ((ViInt16) 9)  
/*--------------------------------------------------------------------------*/

/*--------------------------Output Load Calibration parameters--------------*/
#define RI3152B_OUTPUT_LOAD_CAL_50        ((ViInt16) 0)
#define RI3152B_OUTPUT_LOAD_CAL_1M        ((ViInt16) 1) 
/*--------------------------------------------------------------------------*/



/*-------------------------------- Other Constants --------------------------------*/
#define RI3152B_MAX_OPC_TIME             ((ViInt32) 100000) // Seconds


/*---------------------------------------------------------------------------------*/

#define _RI3152B_ERROR   (_VI_ERROR+0x3FFC0800L)          /* 0xBFFC0800L = -1074001920 */
#define RI3152B_ERR(x)   ((ViStatus) (_RI3152B_ERROR+(x)))

/* = FUNCTION PROTOTYPES ========================================================= */
ViStatus _VI_FUNC ri3152b_init (ViRsrc instrDescriptor, 
                                ViBoolean IDQuery,
                                ViBoolean resetDevice, 
                                ViSession *instrHandle);
                      
ViStatus _VI_FUNC ri3152b_sine_wave (ViSession vi, 
                                     ViReal64 frequency,
                                     ViReal64 amplitude, 
                                     ViReal64 offset, 
                                     ViInt16 phase, 
                                     ViInt16 powerSinex);

ViStatus _VI_FUNC ri3152b_triangular_wave (ViSession vi, 
                                           ViReal64 frequency,
                                           ViReal64 amplitude, 
                                           ViReal64 offset, 
                                           ViInt16 phase, 
                                           ViInt16 powerTriangularx);

ViStatus _VI_FUNC ri3152b_square_wave (ViSession vi, 
                                       ViReal64 frequency,
                                       ViReal64 amplitude, 
                                       ViReal64 offset, 
                                       ViInt16 duty_cycle);

ViStatus _VI_FUNC ri3152b_pulse_wave (ViSession vi, 
                                      ViReal64 frequency,
                                      ViReal64 amplitude, 
                                      ViReal64 offset, 
                                      ViReal64 delay, 
                                      ViReal64 rise_time, 
                                      ViReal64 high_time, 
                                      ViReal64 fall_time);

ViStatus _VI_FUNC ri3152b_ramp_wave (ViSession vi, 
                                     ViReal64 frequency,
                                     ViReal64 amplitude, 
                                     ViReal64 offset, 
                                     ViReal64 delay, 
                                     ViReal64 rise_time, 
                                     ViReal64 fall_time);

ViStatus _VI_FUNC ri3152b_sinc_wave (ViSession vi, 
                                     ViReal64 frequency,
                                     ViReal64 amplitude, 
                                     ViReal64 offset, 
                                     ViInt16 cycle_number);

ViStatus _VI_FUNC ri3152b_gaussian_wave (ViSession vi, 
                                         ViReal64 frequency,
                                         ViReal64 amplitude, 
                                         ViReal64 offset, 
                                         ViInt16 exponent);

ViStatus _VI_FUNC ri3152b_exponential_wave (ViSession vi,
                                            ViReal64 frequency,
                                            ViReal64 amplitude,
                                            ViReal64 offset,
                                            ViInt16 exponent);
                                            
ViStatus _VI_FUNC ri3152b_dc_signal (ViSession vi, 
                                     ViInt16 percent_amplitude);
/*---------------------------------------------------------------------------------------------*/


/*---------------------------------------- Arbitrary Waveforms --------------------------------*/
ViStatus _VI_FUNC ri3152b_define_arb_segment (ViSession vi, 
                                              ViInt16 segment_number,
                                              ViInt32 segment_size);

ViStatus _VI_FUNC ri3152b_set_active_segment (ViSession vi,
                                              ViInt16 segment_number);
ViStatus _VI_FUNC ri3152b_query_active_segment (ViSession vi,
                                               ViInt16 *segment_number);

ViStatus _VI_FUNC ri3152b_delete_segments (ViSession vi, 
                                           ViInt16 segment_number,
                                           ViBoolean delete_all_segments);

ViStatus _VI_FUNC ri3152b_load_arb_data (ViSession vi, 
                                         ViInt16 segment_number,
									     ViUInt16 *data_pts, 
									     ViInt32 number_of_points);

ViStatus _VI_FUNC ri3152b_load_and_shift_arb_data (ViSession vi,
                                                   ViInt16 segmentNumber,
                                                   ViUInt16 dataPointArray[],
                                                   ViInt32 numberofPoints);

ViStatus _VI_FUNC ri3152b_load_ascii_file (ViSession vi, 
                                           ViInt16 segment,
										   ViString filename, 
										   ViInt32 size);

ViStatus _VI_FUNC ri3152b_load_wavecad_file (ViSession vi, 
                                             ViChar waveCADFileName[]);
                                             
ViStatus _VI_FUNC ri3152b_load_wavecad_wave_file (ViSession vi,
                                        		  ViInt16 segment,
                                        		  ViChar filename[]);

ViStatus _VI_FUNC ri3152b_output_arb_waveform (ViSession vi, 
                                               ViInt16 segment_number,
                                               ViReal64 sampling_clock, 
                                               ViReal64 amplitude,
                                               ViReal64 offset, 
                                               ViInt16 clock_source);
/*----------------------------------------------------------------------------------------------*/



/*----------------------------------------- Sequence Waveforms ---------------------------------*/
ViStatus _VI_FUNC ri3152b_define_sequence (ViSession vi,
                                           ViInt16 number_of_steps,
                                           ViInt16 *segment_number,
                                           ViInt32 *repeat_number);

ViStatus _VI_FUNC ri3152b_define_sequence_adv (ViSession vi,
                                               ViInt16 number_of_steps,
                                               ViInt16 segment_number[],
                                               ViInt32 repeat_number[],
                                               unsigned char mode[]);

ViStatus _VI_FUNC ri3152b_delete_sequence (ViSession vi, 
                                           ViInt16 stepNumber);

ViStatus _VI_FUNC ri3152b_output_sequence_waveform (ViSession vi,
                                                    ViReal64 sampling_clock,
                                                    ViReal64 amplitude,
                                                    ViReal64 offset,
                                                    ViInt16 sequence_mode);
                                                    
ViStatus _VI_FUNC ri3152b_set_active_sequence (ViSession vi,
                                               ViInt16 numSequence);
ViStatus _VI_FUNC ri3152b_query_active_sequence (ViSession vi,
                                                 ViInt16 *numSequence);
/*----------------------------------------------------------------------------------------------*/

/*---------------------------------- Example ---------------------------------------------------*/

ViStatus _VI_FUNC ri3152b_example_generate_seq_waveform (ViSession instrHandle);

/*----------------------------------------------------------------------------------------------*/


/*-------------------------- Multi-Instr. Synchronization --------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_couple_mode (ViSession vi,
                                           ViInt16 coupleMode);
ViStatus _VI_FUNC ri3152b_query_couple_mode (ViSession vi,
                                             ViInt16 *coupleMode);
                                             
ViStatus _VI_FUNC ri3152b_set_couple_delay (ViSession vi,
                                            ViReal64 coupleDelay);
ViStatus _VI_FUNC ri3152b_query_couple_delay (ViSession vi,
                                              ViReal64 *coupleDelay);
                                              
ViStatus _VI_FUNC ri3152b_set_couple (ViSession vi,
                                      ViBoolean coupleSwitch);
ViStatus _VI_FUNC ri3152b_query_couple (ViSession vi, 
                                        ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_couple_path (ViSession vi,
                                           ViInt16 couplePath);
ViStatus _VI_FUNC ri3152b_query_couple_path (ViSession vi,
                                             ViInt16 *couplePath);

ViStatus _VI_FUNC ri3152b_insert_slave (ViSession vi, 
                                        ViChar IPAddress[]);
ViStatus _VI_FUNC ri3152b_delete_slave (ViSession vi);

ViStatus _VI_FUNC ri3152b_set_active_channel (ViSession vi,
                                              ViInt16 activeChannel);
ViStatus _VI_FUNC ri3152b_query_active_channel (ViSession vi,
                                                ViInt16 *activeChannel);
/*----------------------------------------------------------------------------------------------*/


/*------------------------------------- Filters ------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_filter (ViSession vi, 
                                  ViInt16 lp_filter);
ViStatus _VI_FUNC ri3152b_query_filter (ViSession vi, 
                                        ViInt16 *filter);
                                        
ViStatus _VI_FUNC ri3152b_set_filter_state (ViSession vi,
                                            ViBoolean filterSwitch);
ViStatus _VI_FUNC ri3152b_query_filter_state (ViSession vi,
                                              ViInt16 *state);
/*----------------------------------------------------------------------------------------------*/


/*-------------------------------FM Modulation -------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_FM_frequency_deviation (ViSession vi,
                                                      ViReal64 freq_dev);
ViStatus _VI_FUNC ri3152b_query_FM_frequency_deviation (ViSession vi,
                                                        ViReal64 *freq_dev);

ViStatus _VI_FUNC ri3152b_set_FM_modulation_waveform (ViSession vi,
                                                      ViInt16 wave);
ViStatus _VI_FUNC ri3152b_query_FM_modulation_waveform (ViSession vi,
                                                        ViInt16 *wave);

ViStatus _VI_FUNC ri3152b_set_FM_modulation_frequency (ViSession vi,
                                                       ViReal64 mod_freq);
ViStatus _VI_FUNC ri3152b_query_FM_modulation_frequency (ViSession vi,
                                                         ViReal64 *mod_freq);

ViStatus _VI_FUNC ri3152b_set_FM_modulation_SCLK (ViSession vi,
                                                  ViReal64 modSCLK);
ViStatus _VI_FUNC ri3152b_query_FM_modulation_SCLK (ViSession vi,
                                                    ViReal64 *modSCLK);

ViStatus _VI_FUNC ri3152b_set_FM_marker (ViSession vi,
                                         ViReal64 marker);
ViStatus _VI_FUNC ri3152b_query_FM_marker (ViSession vi,
                                           ViReal64 *marker);

ViStatus _VI_FUNC ri3152b_load_arb_FM_data (ViSession vi,
                                            ViInt32 wfmSize,
                                            ViReal64 wfmData[]);

ViStatus _VI_FUNC ri3152b_load_FM_Arb_waveform_from_file (ViSession vi,
                                                          ViChar fileName[]);

ViStatus _VI_FUNC ri3152b_amplitude_modulation (ViSession vi, 
                                                ViInt16 percent_amplitude,
                                                ViInt32 internal_frequency);

ViStatus _VI_FUNC ri3152b_output (ViSession vi, 
                                  ViBoolean output_switch);

ViStatus _VI_FUNC ri3152b_operating_mode (ViSession vi, 
                                          ViInt16 operating_mode);

ViStatus _VI_FUNC ri3152b_set_load_impedance (ViSession vi,
                                              ViReal64 impedance);
ViStatus _VI_FUNC ri3152b_query_load_impedance (ViSession vi,
                                                ViReal64 *impedance);

ViStatus _VI_FUNC ri3152b_set_ref_clock_source (ViSession vi,
                                                ViInt16 ref_source);
ViStatus _VI_FUNC ri3152b_query_ref_clock_source (ViSession vi,
                                                  ViInt16 *ref_source);

ViStatus _VI_FUNC ri3152b_set_phase_offset (ViSession vi,
                                            ViInt32 phase_offset);
ViStatus _VI_FUNC ri3152b_query_phase_offset (ViSession vi,
                                              ViInt32 *phase_offset);
                                              
/*--------------------------------- Output Load Calibration ---------------------------------*/
ViStatus _VI_FUNC ri3152b_set_output_load_calibration (ViSession vi,
                                                       ViInt16 loadCalibration);

ViStatus _VI_FUNC ri3152b_query_output_load_calibration (ViSession instrHandle,
                                                        ViInt16 *outputLoadCalibration);

/*-------------------------------------------------------------------------------------------*/

ViStatus _VI_FUNC ri3152b_select_waveform_mode (ViSession vi, 
                                                ViInt16 waveform_type);
                                                
ViStatus _VI_FUNC ri3152b_set_frequency (ViSession vi, 
                                         ViReal64 frequency);
                                         
ViStatus _VI_FUNC ri3152b_set_amplitude (ViSession vi, 
                                         ViReal64 amplitude);
                                         
ViStatus _VI_FUNC ri3152b_set_offset (ViSession vi, 
                                      ViReal64 offset);

ViStatus _VI_FUNC ri3152b_trigger_source (ViSession vi, 
                                          ViInt16 triggerSource);

ViStatus _VI_FUNC ri3152b_trigger_rate (ViSession vi, 
                                        ViReal64 triggerRate);

ViStatus _VI_FUNC ri3152b_trigger_slope (ViSession vi, 
                                         ViInt16 triggerSlope);

ViStatus _VI_FUNC ri3152b_trigger_delay (ViSession vi, 
                                         ViInt32 triggerDelay);

ViStatus _VI_FUNC ri3152b_set_trigger_delay_time (ViSession vi,
                                                  ViReal64 delTime);
ViStatus _VI_FUNC ri3152b_query_trigger_delay_time (ViSession vi,
                                                    ViReal64 *delTime);

ViStatus _VI_FUNC ri3152b_set_TTLT0_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT0_state (ViSession vi,
                                            ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT1_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT1_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT2_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT2_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT3_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT3_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT4_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT4_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT5_On_Off (ViSession vi,
                                            ViBoolean TTLT5);
ViStatus _VI_FUNC ri3152b_query_TTLT5_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT6_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT6_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_TTLT7_On_Off (ViSession vi,
                                            ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_TTLT7_state (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_output_trigger (ViSession vi, 
                                         ViInt16 outputTriggerSource,
                                         ViInt16 outputTriggerLine, 
                                         ViInt32 BITTriggerPoint,
                                         ViInt16 LCOMSegment,
                                         ViInt32 outputWidth);
                                         
ViStatus _VI_FUNC ri3152b_output_sync (ViSession vi, 
									  ViInt16 SYNCPulseSource,
                                      ViInt32 BITSYNCPoint,
                                      ViInt32 outputWidth,
                                      ViInt16 LCOMSegment);
                                      
ViStatus _VI_FUNC ri3152b_immediate_trigger (ViSession vi);

ViStatus _VI_FUNC ri3152b_set_AM_modulation_waveform (ViSession vi, 
                                                      ViInt16 wave);
ViStatus _VI_FUNC ri3152b_query_AM_modulation_waveform (ViSession vi, 
                                                        ViInt16 *wave);
                                                        
ViStatus _VI_FUNC ri3152b_ArbAmplitudeModulation (ViSession vi,
                                                  ViReal64 amplDepth,
                                                  ViReal64 carrierFreq,
                                                  ViReal64 modFreq,
                                                  ViInt16 segmentNumber,
                                                  ViInt32 *segmLength,
                                                  ViReal64 *sampleClock,
                                                  ViInt16 waveform);

/*------------------------------- FSK Modulation --------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_FSK_shifted_frequency (ViSession vi,
                                                     ViReal64 frequency);
ViStatus _VI_FUNC ri3152b_query_FSK_shifted_frequency (ViSession vi,
                                                       ViReal64 *frequency);

ViStatus _VI_FUNC ri3152b_set_FSK_baud (ViSession vi, 
                                        ViReal64 baud);
ViStatus _VI_FUNC ri3152b_query_FSK_baud (ViSession vi,
                                          ViReal64 *baud);

ViStatus _VI_FUNC ri3152b_set_FSK_marker (ViSession vi,
                                          ViInt16 marker);
ViStatus _VI_FUNC ri3152b_query_FSK_marker (ViSession vi,
                                            ViInt16 *marker);

ViStatus _VI_FUNC ri3152b_load_FSK_data_file (ViSession vi,
                                              ViChar fileName[]);

ViStatus _VI_FUNC ri3152b_load_FSK_data_array (ViSession vi,
                                               ViInt16 size,
                                               unsigned char FSKData[]);

/*-------------------------------------------------------------------------------------------------*/



/*------------------------------- ASK Modulation --------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_ASK_start_amplitude (ViSession vi,
                                                   ViReal64 amplitude);
ViStatus _VI_FUNC ri3152b_query_ASK_start_amplitude (ViSession vi,
                                                     ViReal64 *amplitude);

ViStatus _VI_FUNC ri3152b_set_ASK_shifted_amplitude (ViSession vi,
                                                     ViReal64 amplitude);
ViStatus _VI_FUNC ri3152b_query_ASK_shifted_amplitude (ViSession vi,
                                                       ViReal64 *amplitude);

ViStatus _VI_FUNC ri3152b_set_ASK_baud (ViSession vi, 
                                        ViReal64 baud);
ViStatus _VI_FUNC ri3152b_query_ASK_baud (ViSession vi,
                                          ViReal64 *baud);

ViStatus _VI_FUNC ri3152b_set_ASK_marker (ViSession vi,
                                          ViInt16 marker);
ViStatus _VI_FUNC ri3152b_query_ASK_marker (ViSession vi,
                                            ViInt16 *marker);

ViStatus _VI_FUNC ri3152b_load_ASK_data_file (ViSession vi,
                                              ViChar fileName[]);

ViStatus _VI_FUNC ri3152b_load_ASK_data_array (ViSession vi,
                                               ViInt16 size,
                                               unsigned char ASKData[]);
/*------------------------------------------------------------------------------------------------------*/


/*---------------------------------- PSK Modulation -----------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_PSK_start_phase (ViSession vi,
                                               ViReal64 phase);
ViStatus _VI_FUNC ri3152b_query_PSK_start_phase (ViSession vi,
                                                 ViReal64 *phase);

ViStatus _VI_FUNC ri3152b_set_PSK_shifted_phase (ViSession vi,
                                                 ViReal64 phase);
ViStatus _VI_FUNC ri3152b_query_PSK_shifted_phase (ViSession vi,
                                                   ViReal64 *phase);

ViStatus _VI_FUNC ri3152b_set_PSK_rate (ViSession vi, 
                                        ViReal64 rate);
ViStatus _VI_FUNC ri3152b_query_PSK_rate (ViSession vi,
                                          ViReal64 *rate);

ViStatus _VI_FUNC ri3152b_set_PSK_marker (ViSession vi,
                                          ViInt16 marker);
ViStatus _VI_FUNC ri3152b_query_PSK_marker (ViSession vi,
                                            ViInt16 *marker);

ViStatus _VI_FUNC ri3152b_load_PSK_data_file (ViSession vi,
                                              ViChar fileName[]);

ViStatus _VI_FUNC ri3152b_load_PSK_data_array (ViSession vi,
                                              ViInt16 size,
                                              unsigned char PSKData[]);
/*----------------------------------------------------------------------------------------------------------*/


/*--------------------------FHOP Modulation ----------------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_FHOP_dwell_mode (ViSession vi,
                                               ViInt16 dwell_mode);
ViStatus _VI_FUNC ri3152b_query_FHOP_dwell_mode (ViSession vi,
                                                 ViInt16 *dwell_mode);

ViStatus _VI_FUNC ri3152b_set_FHOP_dwell_time (ViSession vi,
                                               ViReal64 dwell_time);
ViStatus _VI_FUNC ri3152b_query_FHOP_dwell_time (ViSession vi,
                                                 ViReal64 *dwell_time);

ViStatus _VI_FUNC ri3152b_set_FHOP_marker (ViSession vi,
                                           ViInt16 marker);
ViStatus _VI_FUNC ri3152b_query_FHOP_marker (ViSession vi,
                                             ViInt16 *marker);

ViStatus _VI_FUNC ri3152b_load_FHOP_data_file (ViSession vi,
                                               ViChar fileName[]);

ViStatus _VI_FUNC ri3152b_load_freq_hop_fixed_data_array (ViSession vi,
                                                          ViInt16 size,
                                                          ViReal64 hopFixData[]);
                                                          
ViStatus _VI_FUNC ri3152b_load_freq_hop_var_data_array (ViSession vi,
                                                        ViReal64 freqData[],
                                                        ViReal64 dwellTimeData[],
                                                        ViInt16 size);
/*----------------------------------------------------------------------------------------------------------*/


/*----------------------------- AHOP Modulation ------------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_AHOP_dwell_mode (ViSession vi,
                                               ViInt16 dwell_mode);
ViStatus _VI_FUNC ri3152b_query_AHOP_dwell_mode (ViSession vi,
                                                 ViInt16 *dwell_mode);

ViStatus _VI_FUNC ri3152b_set_AHOP_dwell_time (ViSession vi,
                                               ViReal64 dwell_time);
ViStatus _VI_FUNC ri3152b_query_AHOP_dwell_time (ViSession vi,
                                                 ViReal64 *dwell_time);

ViStatus _VI_FUNC ri3152b_set_AHOP_marker (ViSession vi,
                                           ViInt16 marker);
ViStatus _VI_FUNC ri3152b_query_AHOP_marker (ViSession vi,
                                             ViInt16 *marker);

ViStatus _VI_FUNC ri3152b_load_AHOP_data_file (ViSession vi,
                                               ViChar fileName[]);

ViStatus _VI_FUNC ri3152b_load_AHOP_fixed_data_array (ViSession vi,
                                                      ViInt16 size,
                                                      float amplitudeData[]);
                                                      
ViStatus _VI_FUNC ri3152b_load_AHOP_var_data_array (ViSession vi,
                                                    ViInt16 size,
                                                    ViReal64 amplitudeData[],
                                                    ViReal64 dwellTimeData[]);
/*------------------------------------------------------------------------------------------------------------*/


/*---------------------------------- 3D Modulation ------------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_3D_marker (ViSession vi,
                                         ViInt32 marker);
ViStatus _VI_FUNC ri3152b_query_3D_marker (ViSession vi,
                                           ViInt32 *marker);

ViStatus _VI_FUNC ri3152b_set_3D_sample_rate (ViSession vi,
                                              ViReal64 sample_rate);
ViStatus _VI_FUNC ri3152b_query_3D_sample_rate (ViSession vi,
                                                ViReal64 *sample_rate);
/*--------------------------------------------------------------------------------------------------------------*/

ViStatus _VI_FUNC ri3152b_set_pulse_delay (ViSession vi,
                                           ViReal64 delay);
ViStatus _VI_FUNC ri3152b_query_pulse_delay (ViSession vi,
                                             ViReal64 *delay);

ViStatus _VI_FUNC ri3152b_set_double_pulse_delay (ViSession vi,
                                                  ViReal64 double_delay);

ViStatus _VI_FUNC ri3152b_query_double_pulse_delay (ViSession vi,
                                                    ViReal64 *double_delay);

ViStatus _VI_FUNC ri3152b_set_double_pulse_state (ViSession vi,
                                                  ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_double_pulse_state (ViSession vi,
                                                    ViInt16 *state);

ViStatus _VI_FUNC ri3152b_set_pulse_high_level (ViSession vi,
                                                ViReal64 high_level);
ViStatus _VI_FUNC ri3152b_query_pulse_high_level (ViSession vi,
                                                  ViReal64 *high_level);

ViStatus _VI_FUNC ri3152b_set_pulse_low_level (ViSession vi,
                                               ViReal64 low_level);
ViStatus _VI_FUNC ri3152b_query_pulse_low_level (ViSession vi,
                                                 ViReal64 *low_level);

ViStatus _VI_FUNC ri3152b_set_pulse_high_time (ViSession vi,
                                               ViReal64 high_time);
ViStatus _VI_FUNC ri3152b_query_pulse_high_time (ViSession vi,
                                                 ViReal64 *high_time);

ViStatus _VI_FUNC ri3152b_set_pulse_polarity (ViSession vi,
                                              ViInt16 polarity);
ViStatus _VI_FUNC ri3152b_query_pulse_polarity (ViSession vi,
                                                ViInt16 *polarity);

ViStatus _VI_FUNC ri3152b_set_pulse_period (ViSession vi,
                                            ViReal64 period);
ViStatus _VI_FUNC ri3152b_query_pulse_period (ViSession vi,
                                              ViReal64 *period);

ViStatus _VI_FUNC ri3152b_set_pulse_rise_time (ViSession vi,
                                               ViReal64 rise_time);
ViStatus _VI_FUNC ri3152b_query_pulse_rise_time (ViSession vi,
                                                 ViReal64 *rise_time);

ViStatus _VI_FUNC ri3152b_set_pulse_fall_time (ViSession vi,
                                               ViReal64 fall_time);
ViStatus _VI_FUNC ri3152b_query_pulse_fall_time (ViSession vi,
                                                 ViReal64 *fall_time);
                                                 
/*---------------------------------- Phase1 /Phase2 -------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_phase_master (ViSession vi);

ViStatus _VI_FUNC ri3152b_phase_slave (ViSession vi, 
                                       ViReal64 phaseOffset);
                                       
ViStatus _VI_FUNC ri3152b_phase_lock_loop (ViSession vi, 
                                           ViBoolean phaseLockLoop);
                                           
ViStatus _VI_FUNC ri3152b_pll_phase (ViSession vi, 
                                     ViReal64 phase);
                                     
ViStatus _VI_FUNC ri3152b_pll_fine_phase(ViSession vi, 
                                         ViReal64 phase);
                                         
ViStatus _VI_FUNC ri3152b_set_phase2_source (ViSession vi,
                                             ViInt16 phase2Source);
ViStatus _VI_FUNC ri3152b_query_phase2_source (ViSession vi,
                                               ViInt16 *phase2Source);
/*--------------------------------------------------------------------------------------------------------*/


ViStatus _VI_FUNC ri3152b_set_sequence_sync_type (ViSession vi,
                                                  ViInt16 sync_type);
ViStatus _VI_FUNC ri3152b_query_sequence_sync_type (ViSession vi,
                                                    ViInt16 *sync_type);

ViStatus _VI_FUNC ri3152b_burst_mode (ViSession vi, 
                                      ViInt32 number_of_cycles);


ViStatus _VI_FUNC ri3152b_status_query (ViSession vi, 
                                        ViReal64 *voltage_value,
                                        ViReal64 *frequency_value, 
                                        ViReal64 *offset_value,
                                        ViInt16 *filter_type);
                                        
ViStatus _VI_FUNC ri3152b_mode_query (ViSession vi, 
                                      ViInt16 *presentWaveformMode,
                                      ViInt16 *presentStandardWaveform);
                                      
ViStatus _VI_FUNC ri3152b_pll_query (ViSession vi, 
                                     ViBoolean *phaseLockState,
                                     ViReal64 *coarsePhase, 
                                     ViReal64 *finePhase, 
                                     ViReal64 *extFrequency);
                                     
                                     
/*----------------------------- Counter -------------------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_counter_function (ViSession vi,
                                                ViInt16 count_func);
ViStatus _VI_FUNC ri3152b_query_counter_function (ViSession vi,
                                                  ViInt16 *count_func);

ViStatus _VI_FUNC ri3152b_set_counter_display_mode (ViSession vi,
                                                    ViInt16 disp_mode);
ViStatus _VI_FUNC ri3152b_query_counter_display_mode (ViSession vi,
                                                      ViInt16 *disp_mode);

ViStatus _VI_FUNC ri3152b_set_counter_gate_time (ViSession vi,
                                                 ViReal64 gate_time);
ViStatus _VI_FUNC ri3152b_query_counter_gate_time (ViSession vi,
                                                   ViReal64 *gate_time);

ViStatus _VI_FUNC ri3152b_reset_counter (ViSession vi);

ViStatus _VI_FUNC ri3152b_read_counter_value (ViSession vi,
                                              ViReal64 *value);
/*------------------------------------------------------------------------------------------------------------*/

ViStatus _VI_FUNC ri3152b_clear (ViSession vi);

ViStatus _VI_FUNC ri3152b_trigger (ViSession vi);

ViStatus _VI_FUNC ri3152b_poll (ViSession vi, 
                                ViInt16 *statusByte);

ViStatus _VI_FUNC ri3152b_set_wave_resolution (ViSession vi,
                                               ViInt16 wave_res);
ViStatus _VI_FUNC ri3152b_query_wave_resolution (ViSession vi,
                                                 ViInt16 *waveResolution);

ViStatus _VI_FUNC ri3152b_format_instrument (ViSession vi,
                                             ViInt16 format);
ViStatus _VI_FUNC ri3152b_query_format_instrument (ViSession vi,
                                                   ViInt16 *format);

ViStatus _VI_FUNC ri3152b_reset (ViSession vi);

ViStatus _VI_FUNC ri3152b_read_status_byte (ViSession vi, 
                                            ViInt16 *statusByte);
                                            
ViStatus _VI_FUNC ri3152b_set_SRE (ViSession vi, 
                                   ViInt16 SRERegister);
ViStatus _VI_FUNC ri3152b_read_SRE (ViSession vi, 
                                    ViInt16 *SRERegister);
                                    
ViStatus _VI_FUNC ri3152b_read_ESR (ViSession vi, 
                                    ViInt16 *ESRRegister);
                                    
ViStatus _VI_FUNC ri3152b_set_ESE (ViSession vi, 
                                   ViInt16 ESERegister);
ViStatus _VI_FUNC ri3152b_read_ESE (ViSession vi, 
                                    ViInt16 *ESERegister);
                                    
ViStatus _VI_FUNC ri3152b_self_test (ViSession vi, 
                                     ViInt16 *passFail,
                                     ViChar selfTestMessage[]);
                                     
ViStatus _VI_FUNC ri3152b_revision_query (ViSession vi, 
                                          ViString driverRevision,
                                          ViString firmwareRevision);
                                          
ViStatus _VI_FUNC ri3152b_error_query (ViSession vi, 
                                       ViInt32 *error,
                                       ViString errorMessage);
                                       
ViStatus _VI_FUNC ri3152b_error_message (ViSession vi, 
                                         ViStatus errorReturnValue,
                                         ViString errorMessage);

ViStatus _VI_FUNC ri3152b_write_instrument_data (ViSession vi,
                                                 ViChar writeBuffer[]);

ViStatus _VI_FUNC ri3152b_query_instrument_data (ViSession vi,
                                                 ViChar cmdBuffer[],
                                                 ViChar rdBuffer[],
                                                 ViInt32 count);

ViStatus _VI_FUNC ri3152b_close (ViSession vi);

ViStatus _VI_FUNC ri3152b_trigger_level_query (ViSession vi, 
                                               ViReal64* trigger_level_value);
                                               
ViStatus _VI_FUNC ri3152b_set_trigger_level (ViSession vi, 
                                             ViReal64 triggerLevel);
                                             
ViStatus _VI_FUNC ri3152b_load_sequence_binary (ViSession vi, 
                                                ViInt16 number_of_steps,
                                                ViInt16 *segment_number,
                                                ViInt32 *repeat_number);
                                                
ViStatus _VI_FUNC ri3152b_format_border    (ViSession vi,
                                            ViBoolean format);

ViStatus _VI_FUNC ri3152b_set_shared_flag_on_off (ViSession vi,
                                                  ViBoolean state);
ViStatus _VI_FUNC ri3152b_query_shared_flag (ViSession vi,
                                             ViInt16 *state);

ViStatus _VI_FUNC ri3152b_change_mode    (ViSession vi,
                                          ViBoolean mode);
                                          
ViStatus _VI_FUNC ri3152b_arb_sampling_freq (ViSession vi,
											 ViReal64 sampling_clock);
											 
ViStatus _VI_FUNC ri3152b_version_query (ViSession vi,
                                         ViString  version);
                                         
ViStatus _VI_FUNC ri3152b_opc (ViSession vi);
ViStatus _VI_FUNC ri3152b_select_standard_waveform  (ViSession vi,
                                          		     ViInt16 waveform_type);
                                          		     
ViStatus _VI_FUNC ri3152b_output_sync_pos (ViSession vi, 
                                           ViInt32 syncPos);
ViStatus _VI_FUNC ri3152b_sync_pos_query (ViSession vi,
										  ViInt32 *pos);
										  
ViStatus _VI_FUNC ri3152b_output_sync_width (ViSession vi, 
                                             ViInt32 syncWidth);
                                             
ViStatus _VI_FUNC ri3152b_option_query (ViSession vi,
                                        ViString  option);

ViStatus _VI_FUNC ri3152b_query_sampling_freq (ViSession vi, 
                                               ViReal64* samp_freq);
                                               
ViStatus _VI_FUNC ri3152b_format_border_query (ViSession vi,
										       ViString format);
										       
ViStatus _VI_FUNC ri3152b_query_output (ViSession vi,
										ViPInt16 output_state);
										
ViStatus _VI_FUNC ri3152b_output_eclt    (ViSession vi,
                                          ViBoolean output_switch);
ViStatus _VI_FUNC ri3152b_query_output_eclt (ViSession vi,
										     ViPInt16 output_state);
										 
ViStatus _VI_FUNC ri3152b_output_ECLT1 (ViSession vi,
                                        ViBoolean output_switch);
ViStatus _VI_FUNC ri3152b_query_output_ECLT1 (ViSession vi,
                                              ViInt16 *output_state);

ViStatus _VI_FUNC ri3152b_set_output_trigger_source (ViSession vi,
                                                     ViInt16 trig_sour);
ViStatus _VI_FUNC ri3152b_trigger_source_query (ViSession vi,
										        ViString trig_sour);
										        
ViStatus _VI_FUNC ri3152b_output_sync_query (ViSession vi,
										     ViInt16 *sync_state);
										     
ViStatus _VI_FUNC ri3152b_sync_source_query (ViSession vi,
										     ViString source);
										     
ViStatus _VI_FUNC ri3152b_sync_width_query (ViSession vi,
										    ViInt32 *width);
										    
ViStatus _VI_FUNC ri3152b_frequency_query (ViSession vi,
										   ViReal64 *frequency);
										   
ViStatus _VI_FUNC ri3152b_query_amplitude(ViSession vi, 
                                          ViPReal64 amplitude);
                                          
ViStatus _VI_FUNC ri3152b_query_offset(ViSession vi, 
                                       ViPReal64 offset);
                                       
ViStatus _VI_FUNC ri3152b_sclk_source_query (ViSession vi,
										     ViPInt16 source);
										
ViStatus _VI_FUNC ri3152b_sine_phase_query (ViSession vi,
										    ViPInt16 phase);
										    
ViStatus _VI_FUNC ri3152b_set_triangle_phase (ViSession vi,  
                                              ViInt16 phase);
ViStatus _VI_FUNC ri3152b_triangle_phase_query (ViSession vi,
										        ViPInt16 phase);
										        
ViStatus _VI_FUNC ri3152b_set_square_dcycle (ViSession vi,  
                                             ViInt16 dcycle);
ViStatus _VI_FUNC ri3152b_square_dcycle_query (ViSession vi,
										       ViPInt16 dcycle);
										       
ViStatus _VI_FUNC ri3152b_set_pulse_data (ViSession vi,  
                                          ViReal64 delay,
										  ViReal64 width, 
										  ViReal64 tran, 
										  ViReal64 trail);
ViStatus _VI_FUNC ri3152b_pulse_data_query (ViSession vi,
										    ViPReal64 delay, 
										    ViPReal64 width,
										    ViPReal64 tran, 
										    ViPReal64 trail);
										    
ViStatus _VI_FUNC ri3152b_set_ramp_data (ViSession vi,  
                                         ViReal64 delay,
										 ViReal64 tran, 
										 ViReal64 trail);
ViStatus _VI_FUNC ri3152b_ramp_data_query (ViSession vi,
										   ViPReal64 delay,
										   ViPReal64 tran, 
										   ViPReal64 trail);
										   
ViStatus _VI_FUNC ri3152b_set_gauss_exp (ViSession vi,  
                                         ViInt16 exp);
ViStatus _VI_FUNC ri3152b_gauss_exp_query (ViSession vi,
										   ViPInt16 exp);
										   
ViStatus _VI_FUNC ri3152b_set_exponential_exp (ViSession vi,  
                                               ViInt16 exp);
ViStatus _VI_FUNC ri3152b_exponential_exp_query (ViSession vi,
										         ViReal64 *exp);
										         
ViStatus _VI_FUNC ri3152b_dc_amplitude_query (ViSession vi,
                                              ViPInt16 amplitude);
										      
ViStatus _VI_FUNC ri3152b_amplitude_modulation_query (ViSession vi,
										              ViPInt16 am_val, 
										              ViInt32 *internal_freq);
ViStatus _VI_FUNC ri3152b_phase_query (ViSession vi,
									   ViPReal64 phase_offset, 
									   ViPInt16 phase_state,
									   ViPInt16 phase_source);
									   
ViStatus _VI_FUNC ri3152b_sequence_mode_query (ViSession vi,
										       ViPInt16 sequence_mode);
										       
ViStatus _VI_FUNC ri3152b_query_operating_mode (ViSession vi, 
                                                ViPInt16 operating_mode);
                                                
ViStatus _VI_FUNC ri3152b_set_burst_count (ViSession vi,
                                          ViInt32 number_of_cycles);
ViStatus _VI_FUNC ri3152b_burst_count_query (ViSession vi,
										     ViPInt32 count);
										     
ViStatus _VI_FUNC ri3152b_trigger_delay_query (ViSession vi,
										       ViPInt32 delay);
										       
ViStatus _VI_FUNC ri3152b_trig_delay_state (ViSession vi, 
                                            ViBoolean on_off);
ViStatus _VI_FUNC ri3152b_trig_delay_state_query (ViSession vi,
										          ViPInt16 delay_state);
										          
ViStatus _VI_FUNC ri3152b_trigger_source_adv_query (ViSession vi,
										            ViPInt16 trig_source);
										            
ViStatus _VI_FUNC ri3152b_trigger_slope_query (ViSession vi,
										       ViPInt16 trig_slope);
										       
ViStatus _VI_FUNC ri3152b_trigger_rate_query (ViSession vi,
										      ViReal64 *triggerRate);
										      
ViStatus _VI_FUNC ri3152b_share_memory_query (ViSession vi,
										      ViPInt16 smem_state,
										      ViPInt16 smem_mode);
										
ViStatus _VI_FUNC ri3152b_format_wave    (ViSession vi,
                                          ViBoolean format);
ViStatus _VI_FUNC ri3152b_format_wave_query (ViSession vi,
										     ViString format);
										     
ViStatus _VI_FUNC ri3152b_output_shunt    (ViSession vi,
                                           ViBoolean output_switch);
ViStatus _VI_FUNC ri3152b_query_output_shunt (ViSession vi,
										      ViPInt16 output_state);
										      
ViStatus _VI_FUNC ri3152b_set_sine_wave_power (ViSession vi,  
                                               ViInt16 power);
ViStatus _VI_FUNC ri3152b_sine_power_query (ViSession vi,
										    ViPInt16 power);
										    
ViStatus _VI_FUNC ri3152b_set_triangle_power (ViSession vi,  
                                              ViInt16 power);
ViStatus _VI_FUNC ri3152b_triangle_power_query (ViSession vi,
										        ViPInt16 power);
										        
ViStatus _VI_FUNC ri3152b_set_sinc_ncycle (ViSession vi,  
                                           ViInt16 ncycle);
ViStatus _VI_FUNC ri3152b_sinc_ncycle_query (ViSession vi,
										     ViPInt16 ncycle);
										     
ViStatus _VI_FUNC ri3152b_phase_lock_null (ViSession vi);

/*------------------------------- Sweep Modulation ------------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_sweep_time (ViSession vi,
							   			  ViReal64 sweep_time);
ViStatus _VI_FUNC ri3152b_query_sweep_time (ViSession vi,
							   			    ViReal64 *sweep_time);
							   			  
ViStatus _VI_FUNC ri3152b_set_sweep_direction (ViSession vi,
							   			       ViInt16 mode);
ViStatus _VI_FUNC ri3152b_query_sweep_direction (ViSession vi,
							   			         ViPInt16 mode);
							   			  
ViStatus _VI_FUNC ri3152b_set_sweep_step (ViSession vi,
							   			  ViInt16 sweep_step);
ViStatus _VI_FUNC ri3152b_query_sweep_step (ViSession vi,
							   			    ViInt16 *sweep_step);
							   			    
ViStatus _VI_FUNC ri3152b_set_sweep_freq_start (ViSession vi,
							   			        ViReal64 freq);
ViStatus _VI_FUNC ri3152b_query_sweep_freq_start (ViSession vi,
							   			          ViReal64 *freq);
							   			          
ViStatus _VI_FUNC ri3152b_set_sweep_freq_stop (ViSession vi,
							   			       ViReal64 freq);
ViStatus _VI_FUNC ri3152b_query_sweep_freq_stop (ViSession vi,
							   			         ViReal64 *freq);
							   			         
ViStatus _VI_FUNC ri3152b_set_sweep_freq_raster (ViSession vi,
							   			         ViReal64 freq);
ViStatus _VI_FUNC ri3152b_query_sweep_freq_raster (ViSession vi,
							   			           ViReal64 *freq);
							   			           
ViStatus _VI_FUNC ri3152b_set_sweep_freq_marker (ViSession vi,
							   			         ViReal64 marker);
ViStatus _VI_FUNC ri3152b_query_sweep_freq_marker (ViSession vi,
							   			           ViReal64 *marker);

ViStatus _VI_FUNC ri3152b_set_gate_mode (ViSession vi, 
                                         ViInt16 gateMode);
ViStatus _VI_FUNC ri3152b_query_gate_mode (ViSession vi,
                                           ViInt16 *gateMode);

ViStatus _VI_FUNC ri3152b_set_re_trigger_delay_state (ViSession vi,
                                                      ViBoolean on_off);
ViStatus _VI_FUNC ri3152b_query_re_trigger_delay_state (ViSession vi,
                                                        ViInt16 *delay_state);

ViStatus _VI_FUNC ri3152b_set_re_trigger_time (ViSession vi,
                                               ViReal64 reTriggerTime);
ViStatus _VI_FUNC ri3152b_query_re_trigger_time (ViSession vi,
                                                 ViReal64 *reTriggerTime);

ViStatus _VI_FUNC ri3152b_set_modulation_type (ViSession vi,
                                               ViInt16 mod_type);
ViStatus _VI_FUNC ri3152b_query_modulation_type (ViSession vi,
                                                 ViInt16 *mod_type);

ViStatus _VI_FUNC ri3152b_set_carrier_frequency (ViSession vi,
                                                 ViReal64 frequency);
ViStatus _VI_FUNC ri3152b_query_carrier_frequency (ViSession vi,
                                                   ViReal64 *frequency);

ViStatus _VI_FUNC ri3152b_set_carrier_baseline (ViSession vi,
                                                ViInt16 carr_baseline);
ViStatus _VI_FUNC ri3152b_query_carrier_baseline (ViSession vi,
                                                  ViInt16 *carr_baseline);

ViStatus _VI_FUNC ri3152b_sweep_function  (ViSession vi,
                                           ViInt16 function_type);
ViStatus _VI_FUNC ri3152b_sweep_func_query (ViSession vi,
                            		        ViInt16 *function_type);
                            		 
ViStatus _VI_FUNC ri3152b_set_sweep_spacing (ViSession vi,
							   			     ViInt16 mode);
ViStatus _VI_FUNC ri3152b_query_sweep_spacing (ViSession vi,
							   			       ViPInt16 mode);
/*------------------------------------------------------------------------------------------------------------------*/
							   			  
ViStatus _VI_FUNC ri3152b_set_sine_wave_phase (ViSession vi,  
                                               ViInt16 phase);
ViStatus _VI_FUNC ri3152b_sine_phase_query (ViSession vi,
										    ViPInt16 phase);
										    
ViStatus _VI_FUNC ri3152b_set_dc_level (ViSession vi, 
                                        ViReal64 dc_level);
ViStatus _VI_FUNC ri3152b_query_dc_level (ViSession vi,
                                          ViReal64 *dc_level);

/*------------------------------ Half Cycle -------------------------------------------------------------------------*/
ViStatus _VI_FUNC ri3152b_set_half_cycle_waveform (ViSession vi,
                                                   ViInt16 wave);
ViStatus _VI_FUNC ri3152b_query_half_cycle_waveform (ViSession vi,
                                                     ViInt16 *wave);

ViStatus _VI_FUNC ri3152b_set_half_cycle_delay (ViSession vi,
                                                ViReal64 delay);
ViStatus _VI_FUNC ri3152b_query_half_cycle_delay (ViSession vi,
                                                  ViReal64 *delay);

ViStatus _VI_FUNC ri3152b_set_half_cycle_duty_cycle (ViSession vi,
                                                     ViReal64 duty_cycle);
ViStatus _VI_FUNC ri3152b_query_half_cycle_duty_cycle (ViSession vi,
                                                       ViReal64 *halfCycleDutyCycle);

ViStatus _VI_FUNC ri3152b_set_half_cycle_frequency (ViSession vi,
                                                    ViReal64 frequency);
ViStatus _VI_FUNC ri3152b_query_half_cycle_frequency (ViSession vi,
                                                      ViReal64 *frequency);

ViStatus _VI_FUNC ri3152b_set_half_cycle_phase (ViSession vi,
                                                ViReal64 phase);
ViStatus _VI_FUNC ri3152b_query_half_cycle_phase (ViSession vi,
                                                  ViReal64 *phase);
/*--------------------------------------------------------------------------------------------------------------------*/

ViStatus _VI_FUNC ri3152b_load_segment_binary (ViSession vi,
											   ViInt16 number_of_segments,
											   ViAInt32 segment_size);

										
// New Functions: 20060404, RNB										
ViStatus _VI_FUNC ri3152b_WaitForOPC( ViSession vi,
                                      ViInt32 MaxTime );
                                      
ViStatus _VI_FUNC ri3152b_output_sync_setup( ViSession vi, 
                                             ViInt16 SYNCPulseSource,
                                             ViInt32 BITSYNCPoint,
                                             ViInt32 outputWidth,
                                             ViInt16 LCOMSegment );
                                             
ViStatus _VI_FUNC ri3152b_output_sync_enable( ViSession vi,
                                              ViBoolean output_sync );



/*--------------------------------------------------------------------------------------------*/


#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

#endif
