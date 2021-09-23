// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2021-03-03 14:22:4#$: Date of last commit
//    $Rev:: 28146              $: Revision of last commit

extern int IsSimOrDeb(char dev_name[20]); 
extern void setup_frequency1();
extern void setup_frequency2();
extern void setup_period1();
extern void setup_period2();
extern void setup_voltage_pp1();
extern void setup_voltage_pp2();
extern void setup_voltage_p_pos1();
extern void setup_voltage_p_pos2();
extern void setup_voltage_p1();
extern void setup_voltage_p2();
extern void setup_voltage_p_neg1();
extern void setup_voltage_p_neg2();
extern void setup_risetime1();
extern void setup_risetime2();
extern void setup_falltime1();
extern void setup_falltime2();
extern void setup_pulsewidth1();
extern void setup_pulsewidth2();
extern void setup_neg_pulsewidth1();
extern void setup_neg_pulsewidth2();
extern void setup_pos_pulsewidth1();
extern void setup_pos_pulsewidth2();
extern void setup_dutycycle1();
extern void setup_dutycycle2();
extern void setup_event_from1();
extern void setup_event_to1();
extern void setup_event_from2();
extern void setup_event_to2();
extern void setup_delay_ab();
extern void setup_delay_aa();
extern void setup_delay_ba();
extern void setup_delay_bb();
extern void setup_wave1();
extern void setup_wave2();
extern void setup_ch1_trig();
extern void setup_ch2_trig();
extern void setup_ext_trig();
extern void init_scope1();
extern void init_scope2();
extern void reset_scope();
extern void modsw_cls();
extern void modsw_opn();
extern void setup_voltage_dc1();
extern void setup_voltage_dc2();
extern void setup_voltage_av1();
extern void setup_voltage_av2();
extern void setup_overshoot1();
extern void setup_overshoot2();
extern void setup_preshoot1();
extern void setup_preshoot2();
extern void setup_save_wave1();
extern void setup_save_wave2();
extern void setup_load_wave1();
extern void setup_load_wave2();
extern void setup_comp_wave1();
extern void setup_comp_wave2();
extern void setup_add1();
extern void setup_add2();
extern void setup_subtract1();
extern void setup_subtract2();
extern void setup_multiply1();
extern void setup_multiply2();
extern void setup_differentiate1();
extern void setup_differentiate2();
extern void setup_integrate1();
extern void setup_integrate2();
extern double fetch_scope();
extern double fetch_volt_avg();
extern double read_scope();
extern int read_waveform();
extern double fetch_compare();
extern double fetch_Pre_OvrShoot();

extern void init_test();
extern int read_wave_test();
extern void setup_internal_trig();

#define FLAG 10

