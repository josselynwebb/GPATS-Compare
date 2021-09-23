'Option Strict Off
'Option Explicit On

Imports System

Public Module Module1


	'=========================================================
    ' *=========================================================================*
    ' * LabWindows/CVI Instrument Driver Visual BASIC Header File
    ' * Instrument:    Racal Instruments 1260 Series Switch Controller
    ' * File:          ri1260.bas
    ' * Revision:      5.1
    ' * Date:          11/18/96
    ' * Revision History:
    ' *      Rev    Date    Comment
    ' *      1.1  05/26/95  Original Release
    ' *      2.1  01/17/96  Added 1260-38 driver support, updated to LabWin 3.1
    ' *      3.1  09/11/96  Updated CVI Driver to support CVI 4.0
    ' *      4.1  09/27/96  Added 1260-18, -39, -39S, -58, -59A/B, -66A/B/C
    ' *      5.1  11/18/96  Added changes to the 39S drivers
    ' *=========================================================================*

    ' *= GLOBAL CONSTANT DECLARATIONS ==========================================*

    ' *** Module type codes ***
    Public Const RI1260_12_MODULE As Short = 12
    Public Const RI1260_13_MODULE As Short = 13
    Public Const RI1260_14_MODULE As Short = 14
    Public Const RI1260_16_MODULE As Short = 16
    Public Const RI1260_17_MODULE As Short = 17
    Public Const RI1260_18_MODULE As Short = 18
    Public Const RI1260_20_MODULE As Short = 20
    Public Const RI1260_30A_MODULE As Short = 301
    Public Const RI1260_30B_MODULE As Short = 302
    Public Const RI1260_30C_MODULE As Short = 303
    Public Const RI1260_30D_MODULE As Short = 304
    Public Const RI1260_35A_MODULE As Short = 351
    Public Const RI1260_35B_MODULE As Short = 352
    Public Const RI1260_37A_MODULE As Short = 371
    Public Const RI1260_37B_MODULE As Short = 372
    Public Const RI1260_38A_MODULE As Short = 381
    Public Const RI1260_38B_MODULE As Short = 382
    Public Const RI1260_36_MODULE As Short = 36
    Public Const RI1260_39_MODULE As Short = 39
    Public Const RI1260_39S_MODULE As Short = 391
    Public Const RI1260_40A_MODULE As Short = 401
    Public Const RI1260_40B_MODULE As Short = 402
    Public Const RI1260_40C_MODULE As Short = 403
    Public Const RI1260_45A_MODULE As Short = 451
    Public Const RI1260_45B_MODULE As Short = 452
    Public Const RI1260_45C_MODULE As Short = 453
    Public Const RI1260_50A_MODULE As Short = 501
    Public Const RI1260_50B_MODULE As Short = 502
    Public Const RI1260_50C_MODULE As Short = 503
    Public Const RI1260_50D_MODULE As Short = 504
    Public Const RI1260_54_MODULE As Short = 54
    Public Const RI1260_58_MODULE As Short = 58
    Public Const RI1260_59A_MODULE As Short = 591
    Public Const RI1260_59B_MODULE As Short = 592
    Public Const RI1260_60_MODULE As Short = 60
    Public Const RI1260_64A_MODULE As Short = 641
    Public Const RI1260_64B_MODULE As Short = 642
    Public Const RI1260_64C_MODULE As Short = 643
    Public Const RI1260_66A_MODULE As Short = 661
    Public Const RI1260_66B_MODULE As Short = 662
    Public Const RI1260_66C_MODULE As Short = 663
    Public Const RI1260_75A_MODULE As Short = 751
    Public Const RI1260_75B_MODULE As Short = 752
    Public Const RI1260_93A_MODULE As Short = 931
    Public Const RI1260_93B_MODULE As Short = 932
    Public Const RI1260_UNKNOWN_MODULE As Short = -1
    Public Const RI1260_NO_MODULE As Short = 0

    ' *** Confidence test on/off ***
    Public Const RI1260_CONF_OFF As Short = 0
    Public Const RI1260_CONF_ON As Short = 1

    ' *** SYNC out connection ***
    Public Const RI1260_SYNC_NONE As Short = 8

    ' *** Trigger source ***
    Public Const RI1260_TTLTRG_NONE As Short = 8

    ' *** Trigger enable/disable ***
    Public Const RI1260_TRIG_OFF As Short = 0
    Public Const RI1260_TRIG_ON As Short = 1

    ' *** Power-up recall enable/disable ***
    Public Const RI1260_RECALL_OFF As Short = 0
    Public Const RI1260_RECALL_ON As Short = 1

    ' *** EOI control ***
    Public Const RI1260_EOI_OFF As Short = 0
    Public Const RI1260_EOI_ON As Short = 1

    ' *** Switch Sequence ***
    Public Const RI1260_BBM As Short = 0
    Public Const RI1260_MBB As Short = 1
    Public Const RI1260_IMM As Short = 2

    ' *** Store/Recall Instrument State ***
    Public Const RI1260_SAVE As Short = 0
    Public Const RI1260_RECALL As Short = 1

    ' *** Scan Control ***
    Public Const RI1260_SCAN_OFF As Short = 0
    Public Const RI1260_SCAN_ON As Short = 1
    Public Const RI1260_SCAN_CONT As Short = 2

    ' *** Interrupt Conditions ***
    Public Const RI1260_SCANBRK_OFF As Short = 0
    Public Const RI1260_SCANBRK_ON As Short = 1
    Public Const RI1260_READY_OFF As Short = 0
    Public Const RI1260_READY_ON As Short = 1

    ' *** Opening and closing relays ***
    Public Const RI1260_OPEN_RELAY As Short = 0
    Public Const RI1260_CLOSE_RELAY As Short = 1

    ' *** Self-test types ***
    Public Const RI1260_RAM_TEST As Short = 1
    Public Const RI1260_CKSUM_TEST As Short = 2
    Public Const RI1260_NONVOL_TEST As Short = 3

    ' *** Handshake level and edge sensitivity ***
    Public Const RI1260_NEG As Short = 0
    Public Const RI1260_POS As Short = 1

    ' *** Data Length Byte/Word/Bit ***
    Public Const RI1260_BYTE As Short = 0
    Public Const RI1260_WORD As Short = 1
    Public Const RI1260_BIT As Short = 2

    ' *** Arming and Disarming ***
    Public Const RI1260_DISARM As Short = 0
    Public Const RI1260_ARM As Short = 1

    ' *= GLOBAL FUNCTION DECLARATIONS ============================================*

    'Declare Function ri1260_init Lib "RI1260.DLL" (ByVal rsrcName As String, ByVal IDQuery As Integer, ByVal reset_inst As Integer, Vi As Long) As Long
    'Declare Function ri1260_close Lib "RI1260.DLL" (ByVal Vi As Long) As Long
    'Declare Function ri1260_reset Lib "RI1260.DLL" (ByVal Vi As Long) As Long
    'Declare Function ri1260_revision_query Lib "RI1260.DLL" (ByVal Vi As Long, driver_rev As String, instr_rev As String) As Long
    'Declare Function ri1260_self_test Lib "RI1260.DLL" (ByVal Vi As Long, test_result As Integer, test_message As String) As Long
    'Declare Function ri1260_error_query Lib "RI1260.DLL" (ByVal Vi As Long, error_code As Long, error_message As String) As Long
    'Declare Function ri1260_error_message Lib "RI1260.DLL" (ByVal Vi As Long, error_code As Long, message As String) As Long
    'Declare Function ri1260_configure Lib "RI1260.DLL" (ByVal Vi As Long, ByVal conf_test As Integer, ByVal sync_out_delay As Integer, ByVal sync_line As Integer, ByVal trig_line As Integer, ByVal pow_up_recall As Integer) As Long
    'Declare Function ri1260_set_switch_sequence Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal switch_seq As Integer) As Long
    'Declare Function ri1260_store_recall_setup Lib "RI1260.DLL" (ByVal Vi As Long, ByVal store_or_recall As Integer, ByVal mem_num As Integer) As Long
    'Declare Function ri1260_scan_control Lib "RI1260.DLL" (ByVal Vi As Long, ByVal action As Integer) As Long
    'Declare Function ri1260_intr_control Lib "RI1260.DLL" (ByVal Vi As Long, ByVal scan_list_break As Integer, ByVal ready As Integer) As Long
    'Declare Function ri1260_def_scan_list Lib "RI1260.DLL" (ByVal Vi As Long, ByVal scan_list As String) As Long
    'Declare Function ri1260_def_excl_list Lib "RI1260.DLL" (ByVal Vi As Long, ByVal excl_list As String) As Long
    'Declare Function ri1260_def_equate_list Lib "RI1260.DLL" (ByVal Vi As Long, ByVal equate_list As String) As Long
    'Declare Function ri1260_operate_multiple Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, states As Integer) As Long
    'Declare Function ri1260_12_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_13_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_16_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_17_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_18_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_20_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_30_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal group As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_35_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_36_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_37_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal group As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_38_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal interconnect As Integer, ByVal mux As Integer, ByVal channel As Integer) As Long
    'Declare Function ri1260_39_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal relay_type As Integer, ByVal channel As Integer) As Long
    'Declare Function ri1260_40_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal matrix As Integer, ByVal row As Integer, ByVal column As Integer) As Long
    'Declare Function ri1260_45_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal matrix As Integer, ByVal row As Integer, ByVal column As Integer) As Long
    'Declare Function ri1260_50_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_54_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_58_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_59_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_60_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_64_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_66_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal switch_num As Integer, ByVal pole As Integer) As Long
    'Declare Function ri1260_75_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_93_operate_single Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long
    'Declare Function ri1260_read_status_byte Lib "RI1260.DLL" (ByVal Vi As Long, response As Integer) As Long
    'Declare Function ri1260_read_relay_states Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, mod_type As Integer, states As Integer) As Long
    'Declare Function ri1260_read_card_types Lib "RI1260.DLL" (ByVal Vi As Long, module_type As Integer) As Long
    'Declare Function ri1260_def_port_modes Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port_io_bitmask As Integer) As Long
    'Declare Function ri1260_set_sync_mode Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal last_sync_port As Integer) As Long
    'Declare Function ri1260_handshake_polarity Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal clkin As Integer, ByVal busy As Integer) As Long
    'Declare Function ri1260_read_digio_conf Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, io_bitmask As Integer, last_sync_port As Integer, arm_state As Integer, clkin As Integer, busy As Integer) As Long
    'Declare Function ri1260_setup_sync_read Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, ByVal max_data As Integer) As Long
    'Declare Function ri1260_setup_sync_write Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, ByVal num_data As Integer, data_array As Long) As Long
    'Declare Function ri1260_arm_digio Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal arm_or_disarm As Integer) As Long
    'Declare Function ri1260_read_sync_data Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal max_data As Integer, num_read As Integer, data_array As Long) As Long
    'Declare Function ri1260_async_read Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, data_item As Long) As Long
    'Declare Function ri1260_async_write Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, data_item As Long) As Long
    'Declare Function ri1260_async_bit_write Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal high_bits As Integer, ByVal low_bits As Integer) As Long
    'Declare Function ri1260_run_synchronous_test Lib "RI1260.DLL" (ByVal Vi As Long, ByVal module As Integer, ByVal test_timeout As Integer) As Long
    'Declare Function ri1260_find1260 Lib "RI1260.DLL" (ByVal max_count As Integer, ret_count As Long, instr_type As Integer, instr_num As Integer, logical_address As Integer, slot As Integer) As Long
    'Declare Function ri1260_closeAllRMSessions Lib "RI1260.DLL" () As Long


End Module