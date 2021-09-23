/*=========================================================================*
 * LabWindows 3.1 Instrument Driver Include File
 * Instrument:    Racal Instruments 1260 Series Switch Controller
 * File:          ri1260.h
 * Revision:      5.1
 * Date:          11/18/96
 * Revision History:
 *      Rev    Date    Comment
 *      1.1  05/26/95  Original Release
 *      2.1  01/17/96  Added 1260-38 driver support, updated to LabWin 3.1
 *      3.1  09/11/96  Updated to support LabWindows/CVI version 4.0 on Win95/WinNT
 *      4.1  09/27/96  Added 1260-18, 1260-39, 1260-58, 1260-59A/B, 
 *                     1260-66A/B/C support
 *      5.1  11/15/96  Fixed problem with 39S driver
 *=========================================================================*/

#ifndef __ri1260_HEADER
#define __ri1260_HEADER

#include <vpptype.h>

#if defined(__cplusplus) || defined(__cplusplus__)
extern "C" {
#endif

/*= GLOBAL CONSTANT DECLARATIONS ============================================*/

/*** Module type codes ***/
#define RI1260_12_MODULE      (ViInt16)  12
#define RI1260_13_MODULE      (ViInt16)  13
#define RI1260_14_MODULE      (ViInt16)  14
#define RI1260_16_MODULE      (ViInt16)  16
#define RI1260_17_MODULE      (ViInt16)  17
#define RI1260_18_MODULE      (ViInt16)  18
#define RI1260_20_MODULE      (ViInt16)  20
#define RI1260_30A_MODULE     (ViInt16) 301
#define RI1260_30B_MODULE     (ViInt16) 302
#define RI1260_30C_MODULE     (ViInt16) 303
#define RI1260_30D_MODULE     (ViInt16) 304
#define RI1260_35A_MODULE     (ViInt16) 351
#define RI1260_35B_MODULE     (ViInt16) 352
#define RI1260_36_MODULE      (ViInt16)  36
#define RI1260_37A_MODULE     (ViInt16) 371
#define RI1260_37B_MODULE     (ViInt16) 372
#define RI1260_38A_MODULE     (ViInt16) 381
#define RI1260_38B_MODULE     (ViInt16) 382
#define RI1260_39_MODULE      (ViInt16)  39
#define RI1260_39S_MODULE     (ViInt16) 391
#define RI1260_40A_MODULE     (ViInt16) 401
#define RI1260_40B_MODULE     (ViInt16) 402
#define RI1260_40C_MODULE     (ViInt16) 403
#define RI1260_45A_MODULE     (ViInt16) 451
#define RI1260_45B_MODULE     (ViInt16) 452
#define RI1260_45C_MODULE     (ViInt16) 453
#define RI1260_50A_MODULE     (ViInt16) 501
#define RI1260_50B_MODULE     (ViInt16) 502
#define RI1260_50C_MODULE     (ViInt16) 503
#define RI1260_50D_MODULE     (ViInt16) 504
#define RI1260_51_MODULE      (ViInt16) 510  
#define RI1260_54_MODULE      (ViInt16)  54
#define RI1260_58_MODULE      (ViInt16)  58
#define RI1260_59A_MODULE     (ViInt16) 591
#define RI1260_59B_MODULE	  (ViInt16) 592
#define RI1260_60_MODULE      (ViInt16)  60
#define RI1260_64A_MODULE     (ViInt16) 641
#define RI1260_64B_MODULE     (ViInt16) 642
#define RI1260_64C_MODULE     (ViInt16) 643
#define RI1260_66A_MODULE     (ViInt16) 661
#define RI1260_66B_MODULE     (ViInt16) 662
#define RI1260_66C_MODULE     (ViInt16) 663
#define RI1260_75A_MODULE     (ViInt16) 751
#define RI1260_75B_MODULE     (ViInt16) 752
#define RI1260_93A_MODULE     (ViInt16) 931
#define RI1260_93B_MODULE     (ViInt16) 932
#define RI1260_UNKNOWN_MODULE (ViInt16)  -1
#define RI1260_NO_MODULE      (ViInt16)   0

/*** Confidence test on/off ***/
#define RI1260_CONF_OFF       (ViInt16) 0
#define RI1260_CONF_ON        (ViInt16) 1

/*** SYNC out connection ***/
#define RI1260_SYNC_NONE      (ViInt16) 8

/*** Trigger source ***/
#define RI1260_TTLTRG_NONE    (ViInt16) 8

/*** Trigger enable/disable ***/
#define RI1260_TRIG_OFF       (ViInt16) 0
#define RI1260_TRIG_ON        (ViInt16) 1

/*** Power-up recall enable/disable ***/
#define RI1260_RECALL_OFF     (ViInt16) 0
#define RI1260_RECALL_ON      (ViInt16) 1

/*** EOI control ***/
#define RI1260_EOI_OFF        (ViInt16) 0
#define RI1260_EOI_ON         (ViInt16) 1

/*** Switch Sequence ***/
#define RI1260_BBM            (ViInt16) 0
#define RI1260_MBB            (ViInt16) 1
#define RI1260_IMM            (ViInt16) 2

/*** Store/Recall Instrument State */
#define RI1260_SAVE           (ViInt16) 0
#define RI1260_RECALL         (ViInt16) 1

/*** Scan Control ***/
#define RI1260_SCAN_OFF       (ViInt16) 0
#define RI1260_SCAN_ON        (ViInt16) 1
#define RI1260_SCAN_CONT      (ViInt16) 2

/*** Interrupt Conditions ***/
#define RI1260_SCANBRK_OFF    (ViInt16) 0
#define RI1260_SCANBRK_ON     (ViInt16) 1
#define RI1260_READY_OFF      (ViInt16) 0
#define RI1260_READY_ON       (ViInt16) 1

/*** Opening and closing relays ***/
#define RI1260_OPEN_RELAY     (ViInt16) 0
#define RI1260_CLOSE_RELAY    (ViInt16) 1

/*** Self-test types */
#define RI1260_RAM_TEST       (ViInt16) 1
#define RI1260_CKSUM_TEST     (ViInt16) 2
#define RI1260_NONVOL_TEST    (ViInt16) 3

/*** Handshake level and edge sensitivity ***/
#define RI1260_NEG            (ViInt16) 0
#define RI1260_POS            (ViInt16) 1

/*** Data Length Byte/Word/Bit ***/
#define RI1260_BYTE           (ViInt16) 0
#define RI1260_WORD           (ViInt16) 1
#define RI1260_BIT            (ViInt16) 2

/*** Arming and Disarming ***/
#define RI1260_DISARM         (ViInt16) 0
#define RI1260_ARM            (ViInt16) 1

/* 1260-39 irregular channel numbers */
#define RI1260_39_DPST          ((ViInt16) 0)
#define RI1260_39_SPST          ((ViInt16) 1000) 
#define RI1260_39_1X2_MUX_1     ((ViInt16) 2000)
#define RI1260_39_1X2_MUX_2     ((ViInt16) 2100)
#define RI1260_39_1X2_MUX_3     ((ViInt16) 2200)
#define RI1260_39_1X2_MUX_4     ((ViInt16) 2300)
#define RI1260_39_1X2_MUX_5     ((ViInt16) 2400)
#define RI1260_39_1X2_MUX_6     ((ViInt16) 2500)
#define RI1260_39_1X4_MUX_1     ((ViInt16) 3000)
#define RI1260_39_1X4_MUX_2     ((ViInt16) 3100)
#define RI1260_39_1X4_MUX_3     ((ViInt16) 3200)
#define RI1260_39_2X8_MATRIX_1  ((ViInt16) 4000)
#define RI1260_39_2X8_MATRIX_2  ((ViInt16) 4100)
#define RI1260_39_2X8_MATRIX_3  ((ViInt16) 4200)
#define RI1260_39_2X8_MATRIX_4  ((ViInt16) 4300)
#define RI1260_39_2X8_MATRIX_5  ((ViInt16) 4400)


/*= GLOBAL FUNCTION DECLARATIONS ============================================*/

ViStatus _VI_FUNC ri1260_init (ViRsrc rsrcName, ViBoolean IDQuery,
    ViBoolean reset, ViPSession vi);
ViStatus _VI_FUNC ri1260_close (ViSession vi);
ViStatus _VI_FUNC ri1260_reset (ViSession vi);
ViStatus _VI_FUNC ri1260_revision_query (ViSession vi, ViString driver_rev,
    ViString instr_rev);
ViStatus _VI_FUNC ri1260_self_test (ViSession vi, ViPInt16 test_result,
    ViString test_message);
ViStatus _VI_FUNC ri1260_error_query (ViSession vi, ViPInt32 error,
    ViString error_message);
ViStatus _VI_FUNC ri1260_error_message (ViSession vi, ViStatus error,
    ViString message);

ViStatus _VI_FUNC ri1260_configure (ViSession vi, ViInt16 conf_test,
    ViInt16 sync_out_delay, ViInt16 sync_line, ViInt16 trig_line,
    ViInt16 pow_up_recall);
ViStatus _VI_FUNC ri1260_set_switch_sequence (ViSession vi, ViInt16 module,
    ViInt16 switch_seq);
ViStatus _VI_FUNC ri1260_store_recall_setup (ViSession vi,
    ViInt16 store_or_recall, ViInt16 mem_num);
ViStatus _VI_FUNC ri1260_scan_control (ViSession vi, ViInt16 action);
ViStatus _VI_FUNC ri1260_intr_control (ViSession vi, ViInt16 scan_list_break,
    ViInt16 ready);
ViStatus _VI_FUNC ri1260_def_scan_list (ViSession vi, ViString scan_list);
ViStatus _VI_FUNC ri1260_def_excl_list (ViSession vi, ViString excl_list);
ViStatus _VI_FUNC ri1260_def_incl_list (ViSession instrHandle, ViInt16 moduleAddress,
                               ViAInt16 relaystoGroup, ViInt16 numbertoGroup);
ViStatus _VI_FUNC ri1260_def_equate_list (ViSession vi, ViString equate_list);
ViStatus _VI_FUNC ri1260_operate_multiple (ViSession vi, ViInt16 module_no,
    ViAInt16 states);
ViStatus _VI_FUNC ri1260_12_operate_single(ViSession vi, ViInt16 module_no,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_13_operate_single(ViSession vi, ViInt16 module_no,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_16_operate_single(ViSession vi, ViInt16 module_no,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_17_operate_single(ViSession vi, ViInt16 module_no,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_18_operate_single(ViSession vi, ViInt16 module_no,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_20_operate_single(ViSession vi, ViInt16 module_no,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_30_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 relay);
ViStatus _VI_FUNC ri1260_35_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay);
ViStatus _VI_FUNC ri1260_36_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay);
ViStatus _VI_FUNC ri1260_37_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 relay);
ViStatus _VI_FUNC ri1260_38_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 interconnect, ViInt16 mux, ViInt16 channel);
ViStatus _VI_FUNC ri1260_39_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay_type, ViInt16 channel);
ViStatus _VI_FUNC ri1260_39S_operate_1x4 (ViSession instrHandle, ViInt16 moduleAddress,
                                 ViInt16 operation, ViInt16 MUXNumber,
                                 ViInt16 relayNumber);
ViStatus _VI_FUNC ri1260_39S_operate_1x8 (ViSession instrHandle, ViInt16 moduleAddress,
                                 ViInt16 operation, ViInt16 MUXNumber,
                                 ViInt16 relayNumber);
ViStatus _VI_FUNC ri1260_40_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 matrix, ViInt16 row, ViInt16 column);
ViStatus _VI_FUNC ri1260_45_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 matrix, ViInt16 row, ViInt16 column);
ViStatus _VI_FUNC ri1260_50_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay);
    
ViStatus _VI_FUNC ri1260_operate_51_2X36_Matrix ( ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 inport, ViInt16 outport);

ViStatus _VI_FUNC ri1260_operate_51_2X12_Matrix ( ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 inA, ViInt16 outA, ViInt16 inB, ViInt16 outB,
     ViInt16 inC, ViInt16 outC);
ViStatus _VI_FUNC ri1260_operate_51_2X6_Matrix ( ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 IN_1, ViInt16 OUT_1,
    							 ViInt16 IN_2, ViInt16 OUT_2,  
    							 ViInt16 IN_3, ViInt16 OUT_3,  
    							 ViInt16 IN_4, ViInt16 OUT_4,  
    							 ViInt16 IN_5, ViInt16 OUT_5,  
    							 ViInt16 IN_6, ViInt16 OUT_6)  ;   
    
    
ViStatus _VI_FUNC ri1260_54_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay);
ViStatus _VI_FUNC ri1260_58_operate_single (ViSession instrHandle, ViInt16 moduleAddress,
                                   ViInt16 operation, ViInt16 MUXNumber,
                                   ViInt16 relayNumber);
ViStatus _VI_FUNC ri1260_59_operate_single(ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay);
ViStatus _VI_FUNC ri1260_60_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay);
ViStatus _VI_FUNC ri1260_64_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay);
ViStatus _VI_FUNC ri1260_66_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay);
ViStatus _VI_FUNC ri1260_75_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay);
ViStatus _VI_FUNC ri1260_93_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay);
ViStatus _VI_FUNC ri1260_read_status_byte (ViSession vi, ViPInt16 response);
ViStatus _VI_FUNC ri1260_read_relay_states (ViSession vi, ViInt16 module_no,
    ViPInt16 mod_type, ViAInt16 states);
ViStatus _VI_FUNC ri1260_read_card_types(ViSession vi, ViAInt16 module_type);
ViStatus _VI_FUNC ri1260_def_port_modes(ViSession vi, ViInt16 module_no,
    ViInt16 port_io_bitmask);
ViStatus _VI_FUNC ri1260_set_sync_mode(ViSession vi, ViInt16 module_no,
    ViInt16 last_sync_port);
ViStatus _VI_FUNC ri1260_handshake_polarity(ViSession vi, ViInt16 module_no,
    ViInt16 clkin, ViInt16 busy);
ViStatus _VI_FUNC ri1260_read_digio_conf(ViSession vi, ViInt16 module_no,
    ViPInt16 io_bitmask, ViPInt16 last_sync_port, ViPInt16 arm_state,
    ViPInt16 clkin, ViPInt16 busy);
ViStatus _VI_FUNC ri1260_setup_sync_read(ViSession vi, ViInt16 module_no,
    ViInt16 port, ViInt16 data_length, ViInt16 max_data);
ViStatus _VI_FUNC ri1260_setup_sync_write(ViSession vi, ViInt16 module_no,
    ViInt16 port, ViInt16 data_length, ViInt16 num_data, ViAInt32 data_array);
ViStatus _VI_FUNC ri1260_arm_digio(ViSession vi, ViInt16 module_no,
    ViInt16 arm_or_disarm);
ViStatus _VI_FUNC ri1260_read_sync_data(ViSession vi, ViInt16 module_no,
    ViInt16 port, ViInt16 max_data, ViPInt16 num_read, ViAInt32 data_array);
ViStatus _VI_FUNC ri1260_async_read(ViSession vi, ViInt16 module_no,
    ViInt16 port, ViInt16 data_length, ViPInt32 data_item);
ViStatus _VI_FUNC ri1260_async_write(ViSession vi, ViInt16 module_no,
    ViInt16 port,ViInt16 data_length, ViInt32 data_item);
ViStatus _VI_FUNC ri1260_async_bit_write(ViSession vi, ViInt16 module_no,
    ViInt16 port, ViInt16 high_bits, ViInt16 low_bits);
ViStatus _VI_FUNC ri1260_run_synchronous_test (ViSession vi, ViInt16 module,
    ViInt16 test_timeout);
ViStatus _VI_FUNC ri1260_configure_1260_18 (ViSession instrHandle, ViInt16 moduleAddress,
                                   ViInt16 cardConfiguration);
ViStatus _VI_FUNC ri1260_find1260 (ViInt16 max_count, ViPInt32 ret_count,
    ViAInt16 instr_type, ViAInt16 instr_num, ViAInt16 logical_address,
    ViAInt16 slot);
ViStatus _VI_FUNC ri1260_closeAllRMSessions (void);

#if defined(__cplusplus) || defined(__cplusplus__)
}
#endif

#endif
