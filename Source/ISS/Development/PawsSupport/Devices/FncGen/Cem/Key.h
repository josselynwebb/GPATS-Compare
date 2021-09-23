#ifndef _LEXKEY_H_
#define _LEXKEY_H_
//
//	VERBS
//
enum enumTyxVerbs {
	V_APP       =   1, // apply
	V_ARM       =   2, // arm
	V_CAL       =   3, // calculate
	V_CHN       =   4, // change
	V_CLS       =   5, // close
	V_CMP       =   6, // compare
	V_CON       =   7, // connect
	V_CPL       =   8, // couple
	V_CRE       =   9, // create
	V_DCL       =  10, // declare
	V_DEF       =  11, // define
	V_DEL       =  12, // delete
	V_DIS       =  13, // disconnect
	V_DO        =  14, // do
	V_DSB       =  15, // disable
	V_ELS       =  16, // else
	V_ENB       =  17, // enable
	V_END       =  18, // end
	V_EST       =  19, // establish
	V_FIN       =  20, // finish
	V_FOR       =  21, // for
	V_FTH       =  22, // fetch
	V_GTO       =  23, // go to
	V_IDY       =  24, // identify
	V_IF        =  25, // if
	V_INC       =  26, // include
	V_INP       =  27, // input
	V_INX       =  28, // initiate
	V_LVE       =  29, // leave
	V_MEA       =  30, // measure
	V_MON       =  31, // monitor
	V_OPN       =  32, // open
	V_OUT       =  33, // output
	V_PFM       =  34, // perform
	V_PRV       =  35, // prove
	V_RD        =  36, // read
	V_REM       =  37, // remove
	V_REQ       =  38, // require
	V_RES       =  39, // resume
	V_RST       =  40, // reset
	V_SET       =  41, // setup
	V_SNS       =  42, // sense
	V_STI       =  43, // stimulate
	V_TRM       =  44, // terminate
	V_UCP       =  45, // uncouple
	V_UPD       =  46, // update
	V_VER       =  47, // verify
	V_WHL       =  48, // while
	V_WTF       =  49, // wait for
	V_XTN       =  50, // extend
	V__CNT      =  51, // symbol count
};
//
//	NOUNS
//
enum enumTyxNouns {
	N_ACS       =   1, // ac signal
	N_ADF       =   2, // adf
	N_AMB       =   3, // ambient conditions
	N_AMS       =   4, // am signal
	N_ATC       =   5, // atc
	N_BUS       =   6, // bus protocol
	N_BUT       =   7, // button
	N_CLX       =   8, // complex signal
	N_COM       =   9, // common
	N_DCF       =  10, // digital configuration
	N_DCS       =  11, // dc signal
	N_DGT       =  12, // digital test
	N_DIS       =  13, // displacement
	N_DME       =  14, // dme
	N_DOP       =  15, // doppler
	N_EAR       =  16, // earth
	N_EMF       =  17, // em field
	N_EPW       =  18, // exponential pulse wave
	N_EVS       =  19, // events
	N_EVT       =  20, // event
	N_EXC       =  21, // exchange
	N_FLU       =  22, // fluid signal
	N_FMS       =  23, // fm signal
	N_HEA       =  24, // heat
	N_IFF       =  25, // iff
	N_ILS       =  26, // ils
	N_IMP       =  27, // impedance
	N_INF       =  28, // infrared
	N_LAS       =  29, // laser
	N_LCL       =  30, // logic control
	N_LDT       =  31, // logic data
	N_LGT       =  32, // light
	N_LLD       =  33, // logic load
	N_LRF       =  34, // logic reference
	N_LTR       =  35, // laser target return
	N_MAN       =  36, // manometric
	N_MDS       =  37, // modulated signal
	N_MIF       =  38, // multi-sensor-infrared
	N_MIL       =  39, // multi-sensor-light
	N_PAC       =  40, // pulsed ac
	N_PAM       =  41, // pam
	N_PAT       =  42, // pulsed ac train
	N_PDC       =  43, // pulsed dc
	N_PDP       =  44, // pulsed doppler
	N_PDT       =  45, // pulsed dc train
	N_PMS       =  46, // pm signal
	N_RDN       =  47, // random noise
	N_RDS       =  48, // radar signal
	N_RPS       =  49, // ramp signal
	N_RSL       =  50, // resolver
	N_RTN       =  51, // rotation
	N_SCS       =  52, // sup car signal
	N_SHT       =  53, // short
	N_SIM       =  54, // simultaneous
	N_SIN       =  55, // sinc wave
	N_SQW       =  56, // square wave
	N_STM       =  57, // stepper motor
	N_STS       =  58, // step signal
	N_SYN       =  59, // synchro
	N_TAC       =  60, // tacan
	N_TDG       =  61, // timed digital
	N_TED       =  62, // turbine engine data
	N_TMI       =  63, // time interval
	N_TMR       =  64, // timer
	N_TRI       =  65, // triangular wave signal
	N_VBR       =  66, // vibration
	N_VID       =  67, // video signal
	N_VOR       =  68, // vor
	N_WAV       =  69, // waveform
	N__CNT      =  70, // symbol count
};
//
//	MODIFIERS
//
enum enumTyxModifiers {
	M_ACCF      =   1, // ac-comp-freq
	M_ACCP      =   2, // ac-comp
	M_ADFM      =   3, // add-from
	M_ADLN      =   4, // addr-lines
	M_ADTO      =   5, // add-to
	M_AGER      =   6, // age-rate
	M_ALLW      =   7, // allowance
	M_ALTI      =   8, // alt
	M_ALTR      =   9, // alt-rate
	M_AMBT      =  10, // ambient-temp
	M_AMCP      =  11, // am-comp
	M_AMCU      =  12, // am-coupl
	M_AMFQ      =  13, // am-freq
	M_AMMC      =  14, // ampl-mod-c
	M_AMMF      =  15, // ampl-mod-f
	M_AMPL      =  16, // ampl-mod
	M_AMSH      =  17, // am-shift
	M_AMSR      =  18, // am-source
	M_ANAC      =  19, // angle-accel
	M_ANAX      =  20, // angle-accel-x
	M_ANAY      =  21, // angle-accel-y
	M_ANAZ      =  22, // angle-accel-z
	M_ANGL      =  23, // angle
	M_ANGP      =  24, // angle-phi
	M_ANGT      =  25, // angle-theta
	M_ANGX      =  26, // angle-x
	M_ANGY      =  27, // angle-y
	M_ANGZ      =  28, // angle-z
	M_ANRT      =  29, // angle-rate
	M_ANRX      =  30, // angle-rate-x
	M_ANRY      =  31, // angle-rate-y
	M_ANRZ      =  32, // angle-rate-z
	M_ANSD      =  33, // ant-speed-dev
	M_ATMS      =  34, // atmos
	M_ATTE      =  35, // attenuation
	M_ATTN      =  36, // atten
	M_AUCO      =  37, // autocollimation-error
	M_BAND      =  38, // bandwidth
	M_BARP      =  39, // barometric-press
	M_BDTH      =  40, // beam-detection-threshold
	M_BITP      =  41, // bit-period
	M_BITR      =  42, // bit-rate
	M_BKPH      =  43, // back-porch
	M_BLKT      =  44, // blackbody-temp
	M_BRAN      =  45, // boresight-angle
	M_BTRN      =  46, // bit-transition
	M_BURD      =  47, // burst-droop
	M_BURR      =  48, // burst-rep-rate
	M_BURS      =  49, // burst
	M_BUSM      =  50, // bus-mode
	M_BUSS      =  51, // bus-spec
	M_CAMG      =  52, // camera-gain
	M_CAMP      =  53, // car-ampl
	M_CAPA      =  54, // cap
	M_CCOM      =  55, // count-command
	M_CDAT      =  56, // count-data
	M_CFRQ      =  57, // car-freq
	M_CHAN      =  58, // channel
	M_CHCT      =  59, // chan-crosstalk
	M_CHID      =  60, // channel-ident
	M_CHIT      =  61, // chan-integrity
	M_CHRM      =  62, // car-harmonics
	M_CLKS      =  63, // clock-source
	M_CMCH      =  64, // compare-ch
	M_CMDW      =  65, // command-word
	M_CMPL      =  66, // compl
	M_CMTO      =  67, // compare-to
	M_CMWB      =  68, // command-word-bit
	M_CMWV      =  69, // compare-wave
	M_COMD      =  70, // command
	M_COND      =  71, // conductance
	M_COUN      =  72, // count
	M_CPHS      =  73, // car-phase
	M_CPKN      =  74, // current-p-neg
	M_CPKP      =  75, // current-p-pos
	M_CPLG      =  76, // coupling
	M_CRSD      =  77, // car-resid
	M_CRSF      =  78, // crest-factor
	M_CSTS      =  79, // count-status
	M_CTRQ      =  80, // cutoff-freq
	M_CUPK      =  81, // current-p
	M_CUPP      =  82, // current-pp
	M_CUR0      =  83, // current-zero
	M_CUR1      =  84, // current-one
	M_CURA      =  85, // av-current
	M_CURI      =  86, // current-inst
	M_CURL      =  87, // current-lmt
	M_CURQ      =  88, // current-quies
	M_CURR      =  89, // current
	M_CURT      =  90, // current-trms
	M_CWLV      =  91, // cw-level
	M_DATA      =  92, // data
	M_DATL      =  93, // data-word-length
	M_DATP      =  94, // data-word-parity
	M_DATS      =  95, // data-word-sync
	M_DATT      =  96, // data-word-tr
	M_DATW      =  97, // data-word
	M_DBLI      =  98, // dbl-int
	M_DBND      =  99, // doppler-bandwidth
	M_DBRC      = 100, // debris-count
	M_DBRS      = 101, // debris-size
	M_DCOF      = 102, // dc-offset
	M_DDMD      = 103, // ddm
	M_DEEM      = 104, // de-emphasis
	M_DELA      = 105, // delay
	M_DEST      = 106, // destination
	M_DEWP      = 107, // dewpoint
	M_DFBA      = 108, // diff-boresight-angle
	M_DIFR      = 109, // differentiate
	M_DIFT      = 110, // differential-temp
	M_DIGS      = 111, // dig-spec
	M_DISP      = 112, // display
	M_DIST      = 113, // distance
	M_DIVG      = 114, // divergence
	M_DIVS      = 115, // divisions
	M_DMDS      = 116, // dominant-mod-sig
	M_DPFR      = 117, // doppler-freq
	M_DPSH      = 118, // doppler-shift
	M_DROO      = 119, // droop
	M_DSFC      = 120, // diss-factor
	M_DSTR      = 121, // distance-r
	M_DSTX      = 122, // distance-x
	M_DSTY      = 123, // distance-y
	M_DSTZ      = 124, // distance-z
	M_DTCT      = 125, // detector-count
	M_DTER      = 126, // diff-temp-error
	M_DTIL      = 127, // diff-temp-interval
	M_DTMD      = 128, // do-timed-digital
	M_DTOR      = 129, // distortion
	M_DTSC      = 130, // detector-scan
	M_DTSP      = 131, // diff-temp-stop
	M_DTST      = 132, // diff-temp-start
	M_DUTY      = 133, // duty-cycle
	M_DVPN      = 134, // dev-pneg
	M_DVPP      = 135, // dev-ppos
	M_DWBT      = 136, // data-word-bit
	M_DYRA      = 137, // dynamic-range
	M_EDLN      = 138, // end-line
	M_EDUT      = 139, // end-unit
	M_EFCY      = 140, // efficacy
	M_EFFI      = 141, // eff
	M_EGDR      = 142, // energy-distribution
	M_EINM      = 143, // event-interval
	M_ERRI      = 144, // error-index
	M_ERRO      = 145, // error
	M_EVAO      = 146, // event-after-occurrences
	M_EVDL      = 147, // event-delay
	M_EVEO      = 148, // event-each-occurrence
	M_EVEV      = 149, // event-every
	M_EVFO      = 150, // event-first-occurrence
	M_EVGB      = 151, // event-gated-by
	M_EVGF      = 152, // event-gate-from
	M_EVGR      = 153, // event-gate-forever
	M_EVGT      = 154, // event-gate-to
	M_EVOU      = 155, // event-out
	M_EVSB      = 156, // event-sync-bit
	M_EVSF      = 157, // event-sync-freq
	M_EVSL      = 158, // event-slope
	M_EVSW      = 159, // event-sync-word
	M_EVTF      = 160, // event-time-from
	M_EVTI      = 161, // event-indicator
	M_EVTR      = 162, // event-time-forever
	M_EVTT      = 163, // event-time-to
	M_EVUN      = 164, // event-until
	M_EVWH      = 165, // event-when
	M_EVXE      = 166, // event-sense
	M_EVXM      = 167, // event-stim
	M_EXAE      = 168, // except at every
	M_EXNM      = 169, // exchange-number
	M_EXPO      = 170, // exponent
	M_FALL      = 171, // fall-time
	M_FCLN      = 172, // focal-length
	M_FCNT      = 173, // frame-count
	M_FDST      = 174, // field-strength
	M_FDVW      = 175, // field-of-view
	M_FIAL      = 176, // first-active-line
	M_FILT      = 177, // filter
	M_FLTC      = 178, // fault-count
	M_FLTS      = 179, // fault-test
	M_FLUT      = 180, // fluid-type
	M_FMCP      = 181, // fm-comp
	M_FMCU      = 182, // fm-coupl
	M_FMFQ      = 183, // fm-freq
	M_FMSR      = 184, // fm-source
	M_FRCE      = 185, // force
	M_FRCR      = 186, // force-rate
	M_FREQ      = 187, // freq
	M_FRMT      = 188, // format
	M_FRQ0      = 189, // freq-zero
	M_FRQ1      = 190, // freq-one
	M_FRQD      = 191, // freq-dev
	M_FRQP      = 192, // freq-pairing
	M_FRQQ      = 193, // freq-quies
	M_FRQR      = 194, // freq-ratio
	M_FRQW      = 195, // freq-window
	M_FUEL      = 196, // fuel-supply
	M_FXDN      = 197, // flux-dens
	M_FXIP      = 198, // flux-dens-in-phase
	M_FXQD      = 199, // flux-dens-quad
	M_GAMA      = 200, // gamma
	M_GSLP      = 201, // glide-slope
	M_GSRE      = 202, // gray-scale-resolution
	M_HAPW      = 203, // harm-power
	M_HARM      = 204, // harmonics
	M_HARN      = 205, // harm-number
	M_HARP      = 206, // harm-phase
	M_HARV      = 207, // harm-voltage
	M_HFOV      = 208, // h-field-of-view
	M_HIZZ      = 209, // hiz
	M_HLAE      = 210, // h-los-align-error
	M_HMDF      = 211, // hi-mod-freq
	M_HRAG      = 212, // h-reference-angle
	M_HSRM      = 213, // h-sensor-resolution-min
	M_HTAG      = 214, // h-target-angle
	M_HTOF      = 215, // h-target-offset
	M_HUMY      = 216, // humidity
	M_IASP      = 217, // ias
	M_ICWB      = 218, // invalid-command-word-bit
	M_IDSE      = 219, // ident-sig-ep
	M_IDSF      = 220, // ident-sig-freq
	M_IDSG      = 221, // ident-sig
	M_IDSM      = 222, // ident-sig-mod
	M_IDWB      = 223, // invalid-data-word-bit
	M_IJIT      = 224, // int-jitter
	M_ILLU      = 225, // illum
	M_INDU      = 226, // ind
	M_INTG      = 227, // integrate
	M_INTL      = 228, // interval
	M_IRAT      = 229, // int-rate
	M_ISTI      = 230, // illegal-state-indicator
	M_ISWB      = 231, // invalid-status-word-bit
	M_ITER      = 232, // iterate
	M_ITRO      = 233, // intensity-ratio
	M_IVCW      = 234, // invalid-command-word
	M_IVDL      = 235, // invalid-data-word-length
	M_IVDP      = 236, // invalid-data-word-parity
	M_IVDS      = 237, // invalid-data-word-sync
	M_IVDT      = 238, // invalid-data-word-tr
	M_IVDW      = 239, // invalid-data-word
	M_IVMG      = 240, // invalid-message-gap
	M_IVOA      = 241, // invalid-one-amplitude
	M_IVRT      = 242, // invalid-response-time
	M_IVSW      = 243, // invalid-status-word
	M_IVWC      = 244, // invalid-word-count
	M_IVWG      = 245, // invalid-word-gap
	M_IVWL      = 246, // invalid-word-length
	M_IVZA      = 247, // invalid-zero-amplitude
	M_IVZC      = 248, // invalid-zero-crossing
	M_LDFM      = 249, // load-from
	M_LDTO      = 250, // load-to
	M_LDVW      = 251, // load-wave
	M_LINE      = 252, // line
	M_LIPF      = 253, // lines-per-channel
	M_LMDF      = 254, // lo-mod-freq
	M_LMIN      = 255, // lum-int
	M_LOCL      = 256, // localizer
	M_LRAN      = 257, // lst-reference-angle
	M_LSAE      = 258, // los-align-error
	M_LSTG      = 259, // laser-stage
	M_LUMF      = 260, // lum-flux
	M_LUMI      = 261, // luminance
	M_LUMT      = 262, // lum-temp
	M_LVLO      = 263, // level-logic-one
	M_LVLZ      = 264, // level-logic-zero
	M_MAGB      = 265, // mag-bearing
	M_MAGR      = 266, // mag-bearing-rate
	M_MAMP      = 267, // mod-ampl
	M_MANI      = 268, // manual-intervention
	M_MASF      = 269, // mass-flow
	M_MASK      = 270, // mask-one
	M_MATH      = 271, // math
	M_MAXT      = 272, // max-time
	M_MBAT      = 273, // main-beam-atten
	M_MDPN      = 274, // mod-pneg
	M_MDPP      = 275, // mod-ppos
	M_MDSC      = 276, // mod-source
	M_MGAP      = 277, // message-gap
	M_MMOD      = 278, // mean-mod
	M_MODD      = 279, // mod-dist
	M_MODE      = 280, // mode
	M_MODF      = 281, // mod-freq
	M_MODO      = 282, // mod-offset
	M_MODP      = 283, // mod-phase
	M_MPFM      = 284, // multp-from
	M_MPTO      = 285, // multp-to
	M_MRCO      = 286, // min-resolv-contrast
	M_MRKB      = 287, // marker-beacon
	M_MRTD      = 288, // min-resolv-temp-diff
	M_MSKZ      = 289, // mask-zero
	M_MSNR      = 290, // minimum-sense-rate
	M_MTFD      = 291, // mtf-direction
	M_MTFP      = 292, // mtf-freq-points
	M_MTFU      = 293, // modulation-transfer-function
	M_NEDT      = 294, // noise-eq-diff-temp
	M_NEGS      = 295, // neg-slope
	M_NHAR      = 296, // non-harmonics
	M_NLIN      = 297, // non-lin
	M_NOAD      = 298, // noise-ampl-dens
	M_NOAV      = 299, // noise-av
	M_NOIS      = 300, // noise
	M_NOPD      = 301, // noise-pwr-dens
	M_NOPK      = 302, // noise-p
	M_NOPP      = 303, // noise-pp
	M_NOTR      = 304, // noise-trms
	M_NPWT      = 305, // neg-pulse-width
	M_OAMP      = 306, // one-amplitude
	M_OTMP      = 307, // oper-temp
	M_OVER      = 308, // overshoot
	M_P3DV      = 309, // p3-dev
	M_P3LV      = 310, // p3-level
	M_PAMP      = 311, // p-ampl
	M_PANG      = 312, // phase-angle
	M_PARE      = 313, // parity-even
	M_PARO      = 314, // parity-odd
	M_PAST      = 315, // pulse-ampl-stab
	M_PATH      = 316, // path
	M_PATN      = 317, // pulse-atten
	M_PATT      = 318, // pattern
	M_PCCU      = 319, // pac-coupl
	M_PCLS      = 320, // pulse-class
	M_PCSR      = 321, // pac-source
	M_PDEV      = 322, // phase-dev
	M_PDGN      = 323, // peak-degen
	M_PDRP      = 324, // pair-droop
	M_PDVN      = 325, // phase-dev-pn
	M_PDVP      = 326, // phase-dev-pp
	M_PERI      = 327, // period
	M_PEST      = 328, // pulse-energy-stab
	M_PHPN      = 329, // phase-pneg
	M_PHPP      = 330, // phase-ppos
	M_PJIT      = 331, // phase-jit
	M_PKDV      = 332, // peak-dev
	M_PLAN      = 333, // pla
	M_PLAR      = 334, // pla-rate
	M_PLEG      = 335, // pulse-energy
	M_PLID      = 336, // pulse-ident
	M_PLSE      = 337, // pulses-excl
	M_PLSI      = 338, // pulses-incl
	M_PLWD      = 339, // pulse-width
	M_PMCU      = 340, // pm-coupl
	M_PMFQ      = 341, // pm-freq
	M_PMSR      = 342, // pm-source
	M_PODN      = 343, // power-dens
	M_POSI      = 344, // position
	M_POSS      = 345, // pos-slope
	M_POWA      = 346, // power-av
	M_POWP      = 347, // power-p
	M_POWR      = 348, // power
	M_PPOS      = 349, // pulse-posn
	M_PPST      = 350, // pulse-period-stab
	M_PPWT      = 351, // pos-pulse-width
	M_PRCD      = 352, // proceed
	M_PRDF      = 353, // power-diff
	M_PRFR      = 354, // prf
	M_PRIO      = 355, // priority
	M_PROA      = 356, // press-osc-ampl
	M_PROF      = 357, // press-osc-freq
	M_PRSA      = 358, // press-a
	M_PRSG      = 359, // press-g
	M_PRSR      = 360, // press-rate
	M_PRTY      = 361, // parity
	M_PSHI      = 362, // phase-shift
	M_PSHT      = 363, // preshoot
	M_PSPC      = 364, // pair-spacing
	M_PSPE      = 365, // pulse-spect
	M_PSPT      = 366, // pulse-spect-threshold
	M_PSRC      = 367, // peak-search
	M_PWRL      = 368, // pwr-lmt
	M_QFAC      = 369, // q
	M_QUAD      = 370, // quad
	M_RADL      = 371, // radial
	M_RADR      = 372, // radial-rate
	M_RAIL      = 373, // radiance-interval
	M_RASP      = 374, // radiance-stop
	M_RAST      = 375, // radiance-start
	M_RCVS      = 376, // receiver-sensitivity
	M_RDNC      = 377, // radiance
	M_REAC      = 378, // reactance
	M_REFF      = 379, // ref-freq
	M_REFI      = 380, // ref-inertial
	M_REFM      = 381, // reference-mirror-size
	M_REFP      = 382, // ref-power
	M_REFR      = 383, // ref-res
	M_REFS      = 384, // ref-source
	M_REFU      = 385, // ref-uut
	M_REFV      = 386, // ref-volt
	M_REFX      = 387, // ref
	M_RELB      = 388, // rel-bearing
	M_RELH      = 389, // relative-humidity
	M_RELW      = 390, // relative-wind
	M_REPT      = 391, // repeat
	M_RERR      = 392, // range-error
	M_RESB      = 393, // resolution-bandwidth
	M_RESI      = 394, // res
	M_RESP      = 395, // resp
	M_RESR      = 396, // res-ratio
	M_RING      = 397, // ringing
	M_RISE      = 398, // rise-time
	M_RLBR      = 399, // rel-bearing-rate
	M_RLVL      = 400, // radiance-level-count
	M_RMNS      = 401, // ramp-neg-slope
	M_RMOD      = 402, // ranging-mode
	M_RMPS      = 403, // ramp-pos-slope
	M_ROUN      = 404, // rounding
	M_RPDV      = 405, // range-pulse-dev
	M_RPEC      = 406, // range-pulse-echo
	M_RPHF      = 407, // ref-phase-freq
	M_RPLD      = 408, // ref-pulses-dev
	M_RPLE      = 409, // reply-eff
	M_RPLI      = 410, // ref-pulses-incl
	M_RPLX      = 411, // ref-pulses-excl
	M_RSPH      = 412, // resp-hiz
	M_RSPO      = 413, // resp-one
	M_RSPT      = 414, // response-time
	M_RSPZ      = 415, // resp-zero
	M_RTRS      = 416, // rotor-speed
	M_SASP      = 417, // sample-spacing
	M_SATM      = 418, // sample-time
	M_SBAT      = 419, // satellite-beam-atten
	M_SBCF      = 420, // sub-car-freq
	M_SBCM      = 421, // sub-car-mod
	M_SBEV      = 422, // strobe-to-event
	M_SBFM      = 423, // subtract-from
	M_SBTO      = 424, // subtract-to
	M_SCNT      = 425, // sample-count
	M_SDEL      = 426, // sense-delay
	M_SERL      = 427, // serial-lsb-first
	M_SERM      = 428, // serial-msb-first
	M_SESA      = 429, // sensor-aperture
	M_SETT      = 430, // settle-time
	M_SGNO      = 431, // sig-noise
	M_SGTF      = 432, // sig-transfer-function
	M_SHFS      = 433, // shaft-speed
	M_SIMU      = 434, // do-simultaneous
	M_SITF      = 435, // sys-intens-transfer-function
	M_SKEW      = 436, // skew-time
	M_SLEW      = 437, // slew-rate
	M_SLRA      = 438, // slant-range-accel
	M_SLRG      = 439, // slant-range
	M_SLRR      = 440, // slant-range-rate
	M_SLSD      = 441, // sls-dev
	M_SLSL      = 442, // sls-level
	M_SMAV      = 443, // sample-av
	M_SMPL      = 444, // sample
	M_SMPW      = 445, // sample-width
	M_SMTH      = 446, // smooth
	M_SNAD      = 447, // sinad
	M_SNSR      = 448, // sense-rate
	M_SPCG      = 449, // spacing
	M_SPED      = 450, // speed
	M_SPGR      = 451, // spec-grav
	M_SPRT      = 452, // speed-ratio
	M_SPTM      = 453, // spec-temp
	M_SQD1      = 454, // sqtr-dist-1
	M_SQD2      = 455, // sqtr-dist-2
	M_SQD3      = 456, // sqtr-dist-3
	M_SQTD      = 457, // sqtr-dist
	M_SQTR      = 458, // sqtr-rate
	M_SRFR      = 459, // sensor-freq
	M_SSMD      = 460, // sensitivity-mode
	M_STAT      = 461, // status
	M_STBM      = 462, // satellite-beam
	M_STIM      = 463, // stim
	M_STLN      = 464, // start-line
	M_STMH      = 465, // stim-hiz
	M_STMO      = 466, // stim-one
	M_STMP      = 467, // static-temp
	M_STMR      = 468, // stim-rate
	M_STMZ      = 469, // stim-zero
	M_STOP      = 470, // stop
	M_STPA      = 471, // static-press-a
	M_STPB      = 472, // stop-bits
	M_STPG      = 473, // static-press-g
	M_STPR      = 474, // static-press-rate
	M_STPT      = 475, // set-of-points
	M_STRD      = 476, // standard
	M_STRT      = 477, // start
	M_STUT      = 478, // start-unit
	M_STWD      = 479, // status-word
	M_SUSP      = 480, // susceptance
	M_SVCP      = 481, // save-comp
	M_SVFM      = 482, // save-from
	M_SVTO      = 483, // save-to
	M_SVWV      = 484, // save-wave
	M_SWBT      = 485, // status-word-bit
	M_SWPT      = 486, // sweep-time
	M_SWRA      = 487, // swr
	M_SYDL      = 488, // sync-delay
	M_SYEV      = 489, // sync-to-event
	M_SYNC      = 490, // sync
	M_TASP      = 491, // tas
	M_TASY      = 492, // time-asym
	M_TCAP      = 493, // temp-coeff-cap
	M_TCBT      = 494, // tgt-coordinate-bottom
	M_TCLT      = 495, // tgt-coordinate-left
	M_TCRT      = 496, // tgt-coordinate-right
	M_TCTP      = 497, // tgt-coordinate-top
	M_TCUR      = 498, // temp-coeff-current
	M_TDAT      = 499, // target-data
	M_TEFC      = 500, // test-freq-count
	M_TEMP      = 501, // temp
	M_TEQL      = 502, // test-equip-listener
	M_TEQT      = 503, // test-equip-talker
	M_TGMD      = 504, // trigger-mode
	M_TGPL      = 505, // target-polarity
	M_TGTA      = 506, // target-range-accel
	M_TGTD      = 507, // target-range
	M_TGTH      = 508, // target-threshold
	M_TGTP      = 509, // target-type
	M_TGTR      = 510, // target-range-rate
	M_TGTS      = 511, // last-pulse-range
	M_THRT      = 512, // thrust
	M_TIEV      = 513, // time-every
	M_TILT      = 514, // tilt
	M_TIME      = 515, // time
	M_TIMP      = 516, // test-equip-imp
	M_TIND      = 517, // temp-coeff-ind
	M_TIUN      = 518, // time-until
	M_TIWH      = 519, // time-when
	M_TJIT      = 520, // time-jit
	M_TLAX      = 521, // tilt-axis
	M_TMON      = 522, // test-equip-monitor
	M_TOPA      = 523, // total-press-a
	M_TOPG      = 524, // total-press-g
	M_TOPR      = 525, // total-press-rate
	M_TORQ      = 526, // torque
	M_TPHD      = 527, // three-phase-delta
	M_TPHY      = 528, // three-phase-wye
	M_TREA      = 529, // temp-coeff-react
	M_TRES      = 530, // temp-coeff-res
	M_TRGS      = 531, // trig-source
	M_TRIG      = 532, // trig
	M_TRLV      = 533, // trig-level
	M_TRN0      = 534, // trans-zero
	M_TRN1      = 535, // trans-one
	M_TRNP      = 536, // trans-period
	M_TRNS      = 537, // trans-sync
	M_TROL      = 538, // test-equip-role
	M_TRSL      = 539, // trig-slope
	M_TRUE      = 540, // true
	M_TRUN      = 541, // timer-until
	M_TRWH      = 542, // timer-when
	M_TSAC      = 543, // test-area-count
	M_TSCC      = 544, // test-chan-count
	M_TSIM      = 545, // test-equip-simulate
	M_TSPC      = 546, // test-point-count
	M_TSTF      = 547, // test-field
	M_TTMP      = 548, // total-temp
	M_TVOL      = 549, // temp-coeff-volt
	M_TYPE      = 550, // type
	M_UNDR      = 551, // undershoot
	M_UNFY      = 552, // uniformity
	M_UNIT      = 553, // unit
	M_UUPL      = 554, // units-per-line
	M_UUTL      = 555, // uut-listener
	M_UUTT      = 556, // uut-talker
	M_VALU      = 557, // value
	M_VBAC      = 558, // vibration-accel
	M_VBAN      = 559, // video-bandwidth
	M_VBAP      = 560, // vibration-ampl-p
	M_VBPP      = 561, // vibration-ampl-pp
	M_VBRT      = 562, // vibration-rate
	M_VBTR      = 563, // vibration-ampl-trms
	M_VDIV      = 564, // volt-per-div
	M_VEAO      = 565, // event-after-occurrences-value
	M_VEDL      = 566, // event-delay-value
	M_VEEO      = 567, // event-each-occurrence-value
	M_VEFO      = 568, // event-first-occurrence-value
	M_VEGF      = 569, // event-gate-for
	M_VETF      = 570, // event-time-for
	M_VFOV      = 571, // v-field-of-view
	M_VINS      = 572, // voltage-inst
	M_VIST      = 573, // video-sync-type
	M_VLAE      = 574, // v-los-align-error
	M_VLAV      = 575, // av-voltage
	M_VLPK      = 576, // voltage-p
	M_VLPP      = 577, // voltage-pp
	M_VLT0      = 578, // voltage-zero
	M_VLT1      = 579, // voltage-one
	M_VLTL      = 580, // volt-lmt
	M_VLTQ      = 581, // voltage-quies
	M_VLTR      = 582, // voltage-ramped
	M_VLTS      = 583, // voltage-stepped
	M_VOLF      = 584, // volume-flow
	M_VOLR      = 585, // voltage-ratio
	M_VOLT      = 586, // voltage
	M_VPHF      = 587, // var-phase-freq
	M_VPHM      = 588, // var-phase-mod
	M_VPKN      = 589, // voltage-p-neg
	M_VPKP      = 590, // voltage-p-pos
	M_VRAG      = 591, // v-reference-angle
	M_VRMS      = 592, // voltage-trms
	M_VSRM      = 593, // v-sensor-resolution-min
	M_VTAG      = 594, // v-target-angle
	M_VTOF      = 595, // v-target-offset
	M_WAIT      = 596, // wait
	M_WAVE      = 597, // wave-length
	M_WDLN      = 598, // word-length
	M_WDRT      = 599, // word-rate
	M_WGAP      = 600, // word-gap
	M_WILD      = 601, // wild
	M_WIND      = 602, // wind-speed
	M_WRDC      = 603, // word-count
	M_WTRN      = 604, // word-transition
	M_XACE      = 605, // x-autocollimation-error
	M_XAGR      = 606, // x-angle-of-regard
	M_XBAG      = 607, // x-boresight-angle
	M_XTAR      = 608, // x-target-angle
	M_YACE      = 609, // y-autocollimation-error
	M_YAGR      = 610, // y-angle-of-regard
	M_YBAG      = 611, // y-boresight-angle
	M_YTAR      = 612, // y-target-angle
	M_ZAMP      = 613, // zero-amplitude
	M_ZCRS      = 614, // zero-crossing
	M_ZERO      = 615, // zero-index
	M__CNT      = 616, // symbol count
};
//
//	MODULETYPES
//
enum enumTyxModuletypes {
	T_ACS       =   1, // name not available
	T_API       =   2, // name not available
	T_ARB       =   3, // name not available
	T_ASA       =   4, // name not available
	T_DCS       =   5, // name not available
	T_DIG       =   6, // name not available
	T_DMM       =   7, // name not available
	T_DWG       =   8, // name not available
	T_FNG       =   9, // name not available
	T_FTM       =  10, // name not available
	T_MBT       =  11, // name not available
	T_MFU       =  12, // name not available
	T_MTR       =  13, // name not available
	T_PAT       =  14, // name not available
	T_PCM       =  15, // name not available
	T_PLG       =  16, // name not available
	T_PSS       =  17, // name not available
	T_PWM       =  18, // name not available
	T_QAL       =  19, // name not available
	T_RSV       =  20, // name not available
	T_RSY       =  21, // name not available
	T_SNG       =  22, // name not available
	T_SRS       =  23, // name not available
	T_VDG       =  24, // name not available
	T__CNT      =  25, // symbol count
};
//
//	DIMS-A
//
enum enumTyxDimsA {
	D_1000BASET =   1, // 1000baset
	D_100BASET  =   2, // 100baset
	D_10BASET   =   3, // 10baset
	D_1553A     =   4, // mil-1553a
	D_1553B     =   5, // mil-1553b
	D_AFAPD     =   6, // afapd
	D_ALLLS     =   7, // all-listener
	D_AMI       =   8, // ami
	D_AR429     =   9, // arinc-429
	D_ARDC      =  10, // ardc
	D_BIP       =  11, // bip
	D_CANA      =  12, // cana
	D_CANB      =  13, // canb
	D_CDDI      =  14, // cddi
	D_CONMD     =  15, // con-mode
	D_CONRT     =  16, // con-rt
	D_CSM       =  17, // csm
	D_CSN       =  18, // csn
	D_CSOC      =  19, // consecutive-occurrences
	D_HDB       =  20, // hdb
	D_ICAN      =  21, // ican
	D_ICAO      =  22, // icao
	D_IDL       =  23, // idl
	D_IEEE488   =  24, // ieee-488
	D_LNGTH     =  25, // length
	D_MASTR     =  26, // master
	D_MIC       =  27, // mic
	D_MIP       =  28, // mip
	D_MONTR     =  29, // monitor
	D_MTS       =  30, // mts
	D_NRZ       =  31, // nrz
	D_OFF       =  32, // off
	D_ON        =  33, // on
	D_PARA      =  34, // parallel
	D_PRIM      =  35, // primary
	D_PRTY      =  36, // parity
	D_REDT      =  37, // redundant
	D_RS232     =  38, // rs-232
	D_RS422     =  39, // rs-422
	D_RS485     =  40, // rs-485
	D_RTCON     =  41, // rt-con
	D_RTRT      =  42, // rt-rt
	D_RZ        =  43, // rz
	D_SERL      =  44, // serial-lsb-first
	D_SERM      =  45, // serial-msb-first
	D_SLAVE     =  46, // slave
	D_SYNC      =  47, // sync
	D_TACFIRE   =  48, // tacfire
	D_TLKLS     =  49, // talker-listener
	D_TR        =  50, // t-r
	D_WADC      =  51, // wadc
	D__CNT      =  52, // symbol count
};
//
//	DIMS-B
//
enum enumTyxDimsB {
	R__CNT      =   1, // symbol count
};
#endif // _LEXKEY_H_
