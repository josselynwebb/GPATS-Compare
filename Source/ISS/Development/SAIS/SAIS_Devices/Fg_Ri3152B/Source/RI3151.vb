'Option Strict Off
'Option Explicit On

Imports System

Public Module RI3151


	'=========================================================
    ' =========================================================================
    '  Visual BASIC VXIplug&play Instrument Driver Include File (VTL 3.0)
    '  Instrument:  Racal Instruments 3151 Arbitrary Waveform Generator
    '  File:        ri3151.bas
    '  Revision:    2.1
    '  Date:        11/4/96
    ' =========================================================================

    '----- Frequency Max and Min limits for various standard waveforms --------

    Public Const RI3151_MIN_FREQ_SIN As Double = .0001
    Public Const RI3151_MAX_FREQ_SIN As Double = 50000000.0
    Public Const RI3151_MIN_FREQ_TRI As Double = .0001
    Public Const RI3151_MAX_FREQ_TRI As Double = 1000000.0
    Public Const RI3151_MIN_FREQ_SQU As Double = .0001
    Public Const RI3151_MAX_FREQ_SQU As Double = 50000000.0
    Public Const RI3151_MIN_FREQ_PULS As Double = .0001
    Public Const RI3151_MAX_FREQ_PULS As Double = 1000000.0
    Public Const RI3151_MIN_FREQ_RAMP As Double = .0001
    Public Const RI3151_MAX_FREQ_RAMP As Double = 1000000.0
    Public Const RI3151_MIN_FREQ_SINC As Double = .0001
    Public Const RI3151_MAX_FREQ_SINC As Double = 1000000.0
    Public Const RI3151_MIN_FREQ_EXP As Double = .0001
    Public Const RI3151_MAX_FREQ_EXP As Double = 1000000.0
    Public Const RI3151_MIN_FREQ_GAU As Double = .0001
    Public Const RI3151_MAX_FREQ_GAU As Double = 1000000.0

    '--------------------------------------------------------------------------

    '----- General Max and Min limits for various standard waveforms ----------

    Public Const RI3151_MIN_AMPLITUDE As Double = .01
    Public Const RI3151_MAX_AMPLITUDE As Double = 16.0
    Public Const RI3151_MIN_OFFSET As Double = -7.19
    Public Const RI3151_MAX_OFFSET As Double = 7.19
    Public Const RI3151_MAX_PEAK_VOLTS As Double = 8.0
    Public Const RI3151_MIN_PEAK_VOLTS As Double = -8.0
    Public Const RI3151_MIN_PHASE As Short = 0
    Public Const RI3151_MAX_PHASE As Short = 360

    '---------------------------------------------------------------------------

    '------ Specific Max and Min limits for various standard waveforms ---------

    Public Const RI3151_POWER_1 As Short = 1
    Public Const RI3151_POWER_2 As Short = 2
    Public Const RI3151_POWER_3 As Short = 3
    Public Const RI3151_POWER_4 As Short = 4
    Public Const RI3151_POWER_5 As Short = 5
    Public Const RI3151_POWER_6 As Short = 6
    Public Const RI3151_POWER_7 As Short = 7
    Public Const RI3151_POWER_8 As Short = 8
    Public Const RI3151_POWER_9 As Short = 9

    Public Const RI3151_MIN_DUTY_CYCLE As Short = 1
    Public Const RI3151_MAX_DUTY_CYCLE As Short = 99

    Public Const RI3151_MIN_DELAY_PULSE As Double = 0.0
    Public Const RI3151_MAX_DELAY_PULSE As Double = 99.9

    Public Const RI3151_MIN_RISE_TIME_PULSE As Double = 0.0
    Public Const RI3151_MAX_RISE_TIME_PULSE As Double = 99.9

    Public Const RI3151_MIN_HIGH_TIME_PULSE As Double = 0.0
    Public Const RI3151_MAX_HIGH_TIME_PULSE As Double = 99.9

    Public Const RI3151_MIN_FALL_TIME_PULSE As Double = 0.0
    Public Const RI3151_MAX_FALL_TIME_PULSE As Double = 99.9

    Public Const RI3151_MIN_DELAY_RAMP As Double = 0.0
    Public Const RI3151_MAX_DELAY_RAMP As Double = 99.9

    Public Const RI3151_MIN_RISE_TIME_RAMP As Double = 0.0
    Public Const RI3151_MAX_RISE_TIME_RAMP As Double = 99.9

    Public Const RI3151_MIN_FALL_TIME_RAMP As Double = 0.0
    Public Const RI3151_MAX_FALL_TIME_RAMP As Double = 99.9

    Public Const RI3151_MIN_CYCLE_NUMBER As Short = 4
    Public Const RI3151_MAX_CYCLE_NUMBER As Short = 100

    Public Const RI3151_MIN_EXPONENT_EXP As Double = -200.0
    Public Const RI3151_MAX_EXPONENT_EXP As Double = 200.0

    Public Const RI3151_MIN_EXPONENT_GAU As Short = 1
    Public Const RI3151_MAX_EXPONENT_GAU As Short = 200

    Public Const RI3151_MIN_PERCENT_AMP As Short = -100
    Public Const RI3151_MAX_PERCENT_AMP As Short = 100

    Public Const RI3151_OUTPUT_ON As Short = 1
    Public Const RI3151_OUTPUT_OFF As Short = 0
    '-----------------------------------------------------------------------------

    '------------------------------ Filter parameters ----------------------------

    Public Const RI3151_FILTER_OFF As Short = 0
    Public Const RI3151_FILTER_20MHZ As Short = 1
    Public Const RI3151_FILTER_25MHZ As Short = 2
    Public Const RI3151_FILTER_50MHZ As Short = 3

    '-----------------------------------------------------------------------------


    '------------------------------ Operation Modes ------------------------------

    Public Const RI3151_MODE_CONT As Short = 0
    Public Const RI3151_MODE_TRIG As Short = 1
    Public Const RI3151_MODE_GATED As Short = 2
    Public Const RI3151_MODE_BURST As Short = 3

    '------------------------------------------------------------------------------

    '------------------------- Amplitude Modulation Parameters --------------------

    Public Const RI3151_TRIGGER_INTERNAL As Short = 0
    Public Const RI3151_TRIGGER_EXTERNAL As Short = 1
    Public Const RI3151_TRIGGER_TTLTRG0 As Short = 2
    Public Const RI3151_TRIGGER_TTLTRG1 As Short = 3
    Public Const RI3151_TRIGGER_TTLTRG2 As Short = 4
    Public Const RI3151_TRIGGER_TTLTRG3 As Short = 5
    Public Const RI3151_TRIGGER_TTLTRG4 As Short = 6
    Public Const RI3151_TRIGGER_TTLTRG5 As Short = 7
    Public Const RI3151_TRIGGER_TTLTRG6 As Short = 8
    Public Const RI3151_TRIGGER_TTLTRG7 As Short = 9
    Public Const RI3151_TRIGGER_NONE As Short = 10

    Public Const RI3151_TRIGGER_BIT As Short = 2
    Public Const RI3151_TRIGGER_LCOM As Short = 3

    Public Const RI3151_SLOPE_POS As Short = 1
    Public Const RI3151_SLOPE_NEG As Short = 0


    Public Const RI3151_TRIGGER_RATE_MIN As Double = .00006
    Public Const RI3151_TRIGGER_RATE_MAX As Double = 1000.0

    Public Const RI3151_TRIGGER_DELAY_MIN As Short = 10
    Public Const RI3151_TRIGGER_DELAY_MAX As Integer = 2000000

    Public Const RI3151_TRIGGER_COUNT_MIN As Short = 1
    Public Const RI3151_TRIGGER_COUNT_MAX As Integer = 1000000

    Public Const RI3151_BURST_INTERNAL As Short = 0
    Public Const RI3151_BURST_EXTERNAL As Short = 1
    Public Const RI3151_BURST_BUS As Short = 2

    Public Const RI3151_BURST_RATE_MIN As Double = .00002
    Public Const RI3151_BURST_RATE_MAX As Short = 999

    Public Const RI3151_BURST_MIN_CYCLE As Short = 1
    Public Const RI3151_BURST_MAX_CYCLE As Integer = 1000000

    '------------------------------------------------------------------------------

    '------------------------- Waveform Mode Selection Paramaters -----------------

    Public Const RI3151_MODE_STD As Short = 0
    Public Const RI3151_MODE_ARB As Short = 1
    Public Const RI3151_MODE_SEQ As Short = 2

    '------------------------- Trigger & Burst Mode Constants --------------------

    Public Const RI3151_MIN_AM_PERCENT As Short = 0
    Public Const RI3151_MAX_AM_PERCENT As Short = 200

    Public Const RI3151_MIN_AM_FREQ As Short = 10
    Public Const RI3151_MAX_AM_FREQ As Short = 500

    '------------------------- SYNC Pulse Parameters ------------------------------
    Public Const RI3151_SYNC_OFF As Short = 0
    Public Const RI3151_SYNC_BIT As Short = 1
    Public Const RI3151_SYNC_LCOM As Short = 2
    Public Const RI3151_SYNC_SSYNC As Short = 3
    Public Const RI3151_SYNC_HCLOCK As Short = 4


    '-----------------------------------------------------------------------------

    '-------------------------  Arbitrary Waveform Constants ---------------------

    Public Const RI3151_MIN_SEG_NUMBER As Short = 1
    Public Const RI3151_MAX_SEG_NUMBER As Short = 4096

    Public Const RI3151_MIN_SEGMENT_SIZE As Short = 10
    Public Const RI3151_MAX_64K_SEG_SIZE As Integer = 64536
    Public Const RI3151_MAX_512K_SEG_SIZE As Integer = 523288

    Public Const RI3151_DELETE_ALL_NO As Short = 0
    Public Const RI3151_DELETE_ALL_YES As Short = 1

    Public Const RI3151_MIN_SAMP_CLK As Double = .01
    Public Const RI3151_MAX_SAMP_CLK As Double = 100000000.0

    Public Const RI3151_CLK_SOURCE_INT As Short = 0
    Public Const RI3151_CLK_SOURCE_EXT As Short = 1
    Public Const RI3151_CLK_SOURCE_ECLTRG0 As Short = 2

    '---------------------------------------------------------------------------

    '----------------------- Sequential Waveform Constants ---------------------

    Public Const RI3151_MIN_NUM_STEPS As Short = 1
    Public Const RI3151_MAX_NUM_STEPS As Short = 4095

    Public Const RI3151_MIN_NUM_REPEAT As Short = 1
    Public Const RI3151_MAX_NUM_REPEAT As Integer = 1000000

    Public Const RI3151_SEQ_AUTO As Short = 0
    Public Const RI3151_SEQ_TRIG As Short = 1


    '------------------3152 Phase Lock Loop Constants -------------------------
    Public Const RI3152_PLL_ON As Short = 1
    Public Const RI3152_PLL_OFF As Short = 0

    Public Const RI3152_MIN_PHASE As Double = -180.0
    Public Const RI3152_MAX_PHASE As Short = 180

    Public Const RI3152_MIN_FINE_PHASE As Double = -36.0
    Public Const RI3152_MAX_FINE_PHASE As Double = 36.0

    '---------------------------- Function Prototypes --------------------------

    Declare Function ri3151_init Lib "ri3151.dll" (ByVal rsrcName As String, ByVal id_query As Short, ByVal reset_inst As Short, ByRef vi As Integer) As Integer

    Declare Function ri3151_sine_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal phase As Short, ByVal powerSinex As Short) As Integer
    Declare Function ri3151_triangular_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal phase As Short, ByVal powerTriangle As Short) As Integer
    Declare Function ri3151_square_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal dutyCycle As Short) As Integer
    Declare Function ri3151_pulse_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal delay As Double, ByVal rise_time As Double, ByVal high_time As Double, ByVal fall_time As Double) As Integer
    Declare Function ri3151_ramp_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal delay As Double, ByVal rise_time As Double, ByVal fall_time As Double) As Integer
    Declare Function ri3151_sinc_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal cycle_number As Short) As Integer
    Declare Function ri3151_gaussian_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal exponent As Short) As Integer
    Declare Function ri3151_exponential_wave Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal exponent As Double) As Integer
    Declare Function ri3151_dc_signal Lib "ri3151.dll" (ByVal vi As Integer, ByVal pct_amplitude As Short) As Integer
    Declare Function ri3151_define_arb_segment Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByVal segment_size As Integer) As Integer
    Declare Function ri3151_delete_segments Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByVal delete_all_segments As Short) As Integer
    Declare Function ri3151_load_arb_data Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByRef data_points As Short, ByVal number_of_points As Integer) As Integer
    Declare Function ri3151_load_and_shift_arb_data Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByRef data_points As Short, ByVal number_of_points As Integer) As Integer
    Declare Function ri3151_load_ascii_file Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByVal file_name As String, ByVal number_of_points As Integer) As Integer
    Declare Function ri3151_load_wavecad_file Lib "ri3151.dll" (ByVal vi As Integer, ByVal waveCAD_file_name As String) As Integer
    Declare Function ri3151_load_wavecad_wave_file Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByVal waveCAD_wave_file_name As String) As Integer
    Declare Function ri3151_output_arb_waveform Lib "ri3151.dll" (ByVal vi As Integer, ByVal segment As Short, ByVal sample_clk_freq As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal clock_source As Short) As Integer
    Declare Function ri3151_define_sequence Lib "ri3151.dll" (ByVal vi As Integer, ByVal number_of_steps As Short, ByRef segment_array As Short, ByRef repeat_array As Integer) As Integer
    Declare Function ri3151_delete_sequence Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_output_sequence_waveform Lib "ri3151.dll" (ByVal vi As Integer, ByVal sample_clk_freq As Double, ByVal amplitude As Double, ByVal offset As Double, ByVal sequence_mode As Short) As Integer
    Declare Function ri3151_example_generate_seq_waveform Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_filter Lib "ri3151.dll" (ByVal vi As Integer, ByVal filter As Short) As Integer
    Declare Function ri3151_amplitude_modulation Lib "ri3151.dll" (ByVal vi As Integer, ByVal pct_amplitude As Short, ByVal internal_frequency As Integer) As Integer
    Declare Function ri3151_output Lib "ri3151.dll" (ByVal vi As Integer, ByVal output_on_off As Short) As Integer
    Declare Function ri3151_operating_mode Lib "ri3151.dll" (ByVal vi As Integer, ByVal operating_mode As Short) As Integer
    Declare Function ri3151_select_waveform_mode Lib "ri3151.dll" (ByVal vi As Integer, ByVal waveform_type As Short) As Integer
    Declare Function ri3151_set_frequency Lib "ri3151.dll" (ByVal vi As Integer, ByVal frequency As Double) As Integer
    Declare Function ri3151_set_amplitude Lib "ri3151.dll" (ByVal vi As Integer, ByVal amplitude As Double) As Integer
    Declare Function ri3151_set_offset Lib "ri3151.dll" (ByVal vi As Integer, ByVal offset As Double) As Integer
    Declare Function ri3151_trigger_source Lib "ri3151.dll" (ByVal vi As Integer, ByVal trigger_source As Short) As Integer
    Declare Function ri3151_trigger_rate Lib "ri3151.dll" (ByVal vi As Integer, ByVal trigger_rate As Double) As Integer
    Declare Function ri3151_trigger_slope Lib "ri3151.dll" (ByVal vi As Integer, ByVal trigger_slope As Short) As Integer
    Declare Function ri3151_trigger_delay Lib "ri3151.dll" (ByVal vi As Integer, ByVal trigger_delay As Integer) As Integer
    Declare Function ri3151_output_trigger Lib "ri3151.dll" (ByVal vi As Integer, ByVal out_trigger_src As Short, ByVal out_trigger_line As Short, ByVal BITTriggerPoint As Integer, ByVal LCOMSegment As Short) As Integer
    Declare Function ri3151_output_sync Lib "ri3151.dll" (ByVal vi As Integer, ByVal SYNCPulseSource As Short, ByVal BITSYNCPoint As Integer, ByVal LCOMSegment As Short) As Integer
    Declare Function ri3151_immediate_trigger Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_phase_master Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_phase_slave Lib "ri3151.dll" (ByVal vi As Integer, ByVal phase_offset As Short) As Integer
    Declare Function ri3151_phase_lock_loop Lib "ri3151.dll" (ByVal vi As Integer, ByVal phaseLockLoop As Double) As Integer
    Declare Function ri3151_pll_phase Lib "ri3151.dll" (ByVal vi As Integer, ByVal phase As Double) As Integer
    Declare Function ri3151_pll_fine_phase Lib "ri3151.dll" (ByVal vi As Integer, ByVal phase As Short) As Integer
    Declare Function ri3151_burst_mode Lib "ri3151.dll" (ByVal vi As Integer, ByVal number_of_cycles As Integer) As Integer
    Declare Function ri3151_status_query Lib "ri3151.dll" (ByVal vi As Integer, ByRef amplitude As Double, ByRef frequency As Double, ByRef offset As Double, ByRef filter_type As Short) As Integer
    Declare Function ri3151_mode_query Lib "ri3151.dll" (ByVal vi As Integer, ByRef presentWaveformMode As Short, ByRef presentStandardWaveform As Short) As Integer
    Declare Function ri3151_pll_query Lib "ri3151.dll" (ByVal vi As Integer, ByRef phaseLockState As Short, ByRef coarsePhase As Double, ByRef finePhase As Double, ByRef extFrequency As Double) As Integer
    Declare Function ri3151_clear Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_trigger Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_poll Lib "ri3151.dll" (ByVal vi As Integer, ByRef status_byte As Short) As Integer
    Declare Function ri3151_reset Lib "ri3151.dll" (ByVal vi As Integer) As Integer
    Declare Function ri3151_read_status_byte Lib "ri3151.dll" (ByVal vi As Integer, ByRef status_byte As Short) As Integer
    Declare Function ri3151_set_SRE Lib "ri3151.dll" (ByVal vi As Integer, ByVal SRE_register As Short) As Integer
    Declare Function ri3151_read_SRE Lib "ri3151.dll" (ByVal vi As Integer, ByRef SRE_register As Short) As Integer
    Declare Function ri3151_read_ESR Lib "ri3151.dll" (ByVal vi As Integer, ByRef ESR_register As Short) As Integer
    Declare Function ri3151_set_ESE Lib "ri3151.dll" (ByVal vi As Integer, ByVal ESE_register As Short) As Integer
    Declare Function ri3151_read_ESE Lib "ri3151.dll" (ByVal vi As Integer, ByRef ESE_register As Short) As Integer
    Declare Function ri3151_self_test Lib "ri3151.dll" (ByVal vi As Integer, ByRef test_result As Integer, ByRef test_message As String) As Integer
    Declare Function ri3151_revision_query Lib "ri3151.dll" (ByVal vi As Integer, ByRef driver_rev As String, ByRef firmware_rev As String) As Integer
    Declare Function ri3151_error_query Lib "ri3151.dll" (ByVal vi As Integer, ByRef error_code As Short, ByRef error_message As String) As Integer
    Declare Function ri3151_error_message Lib "ri3151.dll" (ByVal vi As Integer, ByVal error_code As Integer, ByRef message As String) As Integer
    Declare Function ri3151_close Lib "ri3151.dll" (ByVal vi As Integer) As Integer


End Module