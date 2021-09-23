
' *=========================================================================*
' * LabWindows/CVI Instrument Driver Visual BASIC Header File
' * Instrument:    Racal Instruments 1260 Series Switch Controller
' * File:          ri1260.bas
' * Revision:      5.1
' * Date:          11/18/96
' * Revision History:
' *      Rev      Date      Comment
' *      1.1      05/26/95  Original Release
' *      2.1      01/17/96  Added 1260-38 driver support, updated to LabWin 3.1
' *      3.1      09/11/96  Updated to support LabWindows/CVI version 4.0 on Win95/WinNT
' *      4.1      09/27/96  Added 1260-18, 1260-39, 1260-58, 1260-59A/B,
' *                         1260-66A/B/C support
' *      5.1      11/15/96  Fixed problem with 39S driver
' *      6.1 A    08/27/97  Updated for SFP for 32-bit in WIN95/NT
' *      7.1 B    04/07/98  Fixed bug in -64ABC driver
' *      8.1 C    06/01/98  Fixed SFPs for 1260-30ABCD, 1260-38A, 1260-45ABC
' *      9.1 D    06/22/98  Change SFPs so windows minimizable & moveable
' *     10.1 E    07/28/98  Change SFPs to make panels minimizable & movable
' *     11.1 F    09/25/98  Fix bug in 1260-66ABC driver.
' *     12.1 G    06/10/99  Corrected SFP for the 1260-45B and -45C.  This
' *                         driver DID NOT CHANGE, but the revision was updated
' *                         to be the same as the VXIplug&play install disk
' *                         which installs it.
' *     14.1 J    01/08/02  RNB, Fix bug in ri1260_format_relay ().  See SQR 303
' *=========================================================================*

' *= GLOBAL CONSTANT DECLARATIONS ==========================================*

' *** Module type codes ***
Global Const RI1260_12_MODULE = 12
Global Const RI1260_13_MODULE = 13
Global Const RI1260_14_MODULE = 14
Global Const RI1260_16_MODULE = 16
Global Const RI1260_17_MODULE = 17
Global Const RI1260_18_MODULE = 18
Global Const RI1260_20_MODULE = 20
Global Const RI1260_30A_MODULE = 301
Global Const RI1260_30B_MODULE = 302
Global Const RI1260_30C_MODULE = 303
Global Const RI1260_30D_MODULE = 304
Global Const RI1260_35A_MODULE = 351
Global Const RI1260_35B_MODULE = 352
Global Const RI1260_37A_MODULE = 371
Global Const RI1260_37B_MODULE = 372
Global Const RI1260_38A_MODULE = 381
Global Const RI1260_38B_MODULE = 382
Global Const RI1260_36_MODULE = 36
Global Const RI1260_39_MODULE = 39
Global Const RI1260_39S_MODULE = 391
Global Const RI1260_40A_MODULE = 401
Global Const RI1260_40B_MODULE = 402
Global Const RI1260_40C_MODULE = 403
Global Const RI1260_45A_MODULE = 451
Global Const RI1260_45B_MODULE = 452
Global Const RI1260_45C_MODULE = 453
Global Const RI1260_50A_MODULE = 501
Global Const RI1260_50B_MODULE = 502
Global Const RI1260_50C_MODULE = 503
Global Const RI1260_50D_MODULE = 504
Global Const RI1260_51_MODULE = 510
Global Const RI1260_54_MODULE = 54
Global Const RI1260_58_MODULE = 58
Global Const RI1260_59A_MODULE = 591
Global Const RI1260_59B_MODULE = 592
Global Const RI1260_60_MODULE = 60
Global Const RI1260_64A_MODULE = 641
Global Const RI1260_64B_MODULE = 642
Global Const RI1260_64C_MODULE = 643
Global Const RI1260_66A_MODULE = 661
Global Const RI1260_66B_MODULE = 662
Global Const RI1260_66C_MODULE = 663
Global Const RI1260_75A_MODULE = 751
Global Const RI1260_75B_MODULE = 752
Global Const RI1260_93A_MODULE = 931
Global Const RI1260_93B_MODULE = 932
Global Const RI1260_UNKNOWN_MODULE = -1
Global Const RI1260_NO_MODULE = 0

' *** Confidence test on/off ***
Global Const RI1260_CONF_OFF = 0
Global Const RI1260_CONF_ON = 1

' *** SYNC out connection ***
Global Const RI1260_SYNC_NONE = 8

' *** Trigger source ***
Global Const RI1260_TTLTRG_NONE = 8

' *** Trigger enable/disable ***
Global Const RI1260_TRIG_OFF = 0
Global Const RI1260_TRIG_ON = 1

' *** Power-up recall enable/disable ***
Global Const RI1260_RECALL_OFF = 0
Global Const RI1260_RECALL_ON = 1

' *** EOI control ***
Global Const RI1260_EOI_OFF = 0
Global Const RI1260_EOI_ON = 1

' *** Switch Sequence ***
Global Const RI1260_BBM = 0
Global Const RI1260_MBB = 1
Global Const RI1260_IMM = 2

' *** Store/Recall Instrument State ***
Global Const RI1260_SAVE = 0
Global Const RI1260_RECALL = 1

' *** Scan Control ***
Global Const RI1260_SCAN_OFF = 0
Global Const RI1260_SCAN_ON = 1
Global Const RI1260_SCAN_CONT = 2

' *** Interrupt Conditions ***
Global Const RI1260_SCANBRK_OFF = 0
Global Const RI1260_SCANBRK_ON = 1
Global Const RI1260_READY_OFF = 0
Global Const RI1260_READY_ON = 1

' *** Opening and closing relays ***
Global Const RI1260_OPEN_RELAY = 0
Global Const RI1260_CLOSE_RELAY = 1

' *** Self-test types ***
Global Const RI1260_RAM_TEST = 1
Global Const RI1260_CKSUM_TEST = 2
Global Const RI1260_NONVOL_TEST = 3

' *** Handshake level and edge sensitivity ***
Global Const RI1260_NEG = 0
Global Const RI1260_POS = 1

' *** Data Length Byte/Word/Bit ***
Global Const RI1260_BYTE = 0
Global Const RI1260_WORD = 1
Global Const RI1260_BIT = 2

' *** Arming and Disarming ***
Global Const RI1260_DISARM = 0
Global Const RI1260_ARM = 1

' *= GLOBAL FUNCTION DECLARATIONS ============================================*

Declare Function ri1260_init Lib "ri1260_32.dll" (ByVal rsrcName As String, ByVal IDQuery As Integer, ByVal reset_inst As Integer, vi As Long) As Long

Declare Function ri1260_close Lib "ri1260_32.dll" (ByVal vi As Long) As Long

Declare Function ri1260_reset Lib "ri1260_32.dll" (ByVal vi As Long) As Long

Declare Function ri1260_revision_query Lib "ri1260_32.dll" (ByVal vi As Long, ByVal driver_rev As String, ByVal instr_rev As String) As Long

Declare Function ri1260_self_test Lib "ri1260_32.dll" (ByVal vi As Long, test_result As Integer, ByVal test_message As String) As Long

Declare Function ri1260_error_query Lib "ri1260_32.dll" (ByVal vi As Long, error_code As Long, ByVal error_message As String) As Long

Declare Function ri1260_error_message Lib "ri1260_32.dll" (ByVal vi As Long, error_code As Long, ByVal message As String) As Long

Declare Function ri1260_configure Lib "ri1260_32.dll" (ByVal vi As Long, ByVal conf_test As Integer, ByVal sync_out_delay As Integer, ByVal sync_line As Integer, ByVal trig_line As Integer, ByVal pow_up_recall As Integer) As Long

Declare Function ri1260_set_switch_sequence Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal switch_seq As Integer) As Long

Declare Function ri1260_store_recall_setup Lib "ri1260_32.dll" (ByVal vi As Long, ByVal store_or_recall As Integer, ByVal mem_num As Integer) As Long

Declare Function ri1260_scan_control Lib "ri1260_32.dll" (ByVal vi As Long, ByVal action As Integer) As Long

Declare Function ri1260_intr_control Lib "ri1260_32.dll" (ByVal vi As Long, ByVal scan_list_break As Integer, ByVal ready As Integer) As Long

Declare Function ri1260_def_scan_list Lib "ri1260_32.dll" (ByVal vi As Long, ByVal scan_list As String) As Long

Declare Function ri1260_def_excl_list Lib "ri1260_32.dll" (ByVal vi As Long, ByVal excl_list As String) As Long

Declare Function ri1260_def_equate_list Lib "ri1260_32.dll" (ByVal vi As Long, ByVal equate_list As String) As Long

Declare Function ri1260_operate_multiple Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, states As Integer) As Long

Declare Function ri1260_12_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_13_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_16_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_17_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_18_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_20_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_30_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal group As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_35_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_36_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_37_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal group As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_38_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal interconnect As Integer, ByVal mux As Integer, ByVal channel As Integer) As Long

Declare Function ri1260_39_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal relay_type As Integer, ByVal channel As Integer) As Long

Declare Function ri1260_39S_operate_1x4 Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal pole As Integer) As Long

Declare Function ri1260_39S_operate_1x8 Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal pole As Integer) As Long

Declare Function ri1260_40_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal matrix As Integer, ByVal row As Integer, ByVal column As Integer) As Long

Declare Function ri1260_45_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal matrix As Integer, ByVal row As Integer, ByVal column As Integer) As Long

Declare Function ri1260_50_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_operate_51_2X36_MUX Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal inport As Integer, ByVal outport As Integer) As Long

Declare Function ri1260_operate_51_2X12_Matrix Lib "ri1260_32.dll" (ByVal vi&, ByVal module%, ByVal operation%, ByVal inA%, ByVal outA%, ByVal inB%, ByVal outB%, ByVal inC%, ByVal outC%) As Long

Declare Function ri1260_operate_51_2X6_Matrix Lib "ri1260_32.dll" (ByVal vi&, ByVal module%, ByVal operation%, ByVal inA%, ByVal outA%, ByVal inB%, ByVal outB%, ByVal inC%, ByVal outC%, ByVal inD%, ByVal outD%, ByVal inE%, ByVal outE%, ByVal inF%, ByVal outF%) As Long

Declare Function ri1260_54_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_58_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_59_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal mux As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_60_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_64_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_66_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal switch_num As Integer, ByVal pole As Integer) As Long

Declare Function ri1260_75_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_93_operate_single Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal operation As Integer, ByVal grp As Integer, ByVal relay As Integer) As Long

Declare Function ri1260_read_status_byte Lib "ri1260_32.dll" (ByVal vi As Long, response As Integer) As Long

Declare Function ri1260_read_relay_states Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, mod_type As Integer, states As Integer) As Long

Declare Function ri1260_read_card_types Lib "ri1260_32.dll" (ByVal vi As Long, module_type As Integer) As Long

Declare Function ri1260_def_port_modes Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port_io_bitmask As Integer) As Long

Declare Function ri1260_set_sync_mode Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal last_sync_port As Integer) As Long

Declare Function ri1260_handshake_polarity Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal clkin As Integer, ByVal busy As Integer) As Long

Declare Function ri1260_read_digio_conf Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, io_bitmask As Integer, last_sync_port As Integer, arm_state As Integer, clkin As Integer, busy As Integer) As Long

Declare Function ri1260_setup_sync_read Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, ByVal max_data As Integer) As Long

Declare Function ri1260_setup_sync_write Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, ByVal num_data As Integer, data_array As Long) As Long

Declare Function ri1260_arm_digio Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal arm_or_disarm As Integer) As Long

Declare Function ri1260_read_sync_data Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal max_data As Integer, num_read As Integer, data_array As Long) As Long

Declare Function ri1260_async_read Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, data_item As Long) As Long

Declare Function ri1260_async_write Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal data_length As Integer, data_item As Long) As Long

Declare Function ri1260_async_bit_write Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module_no As Integer, ByVal port As Integer, ByVal high_bits As Integer, ByVal low_bits As Integer) As Long

Declare Function ri1260_run_synchronous_test Lib "ri1260_32.dll" (ByVal vi As Long, ByVal module As Integer, ByVal test_timeout As Integer) As Long

Declare Function ri1260_find1260 Lib "ri1260_32.dll" (ByVal max_count As Integer, ret_count As Long, instr_type As Integer, instr_num As Integer, logical_address As Integer, slot As Integer) As Long

Declare Function ri1260_closeAllRMSessions Lib "ri1260_32.dll" () As Long



