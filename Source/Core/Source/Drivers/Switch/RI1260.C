/*= Racal Instruments 1260 Series Switch Controller =======================*
 * VXI Plug&Play WIN Framework Lab Windows/CVI Instrument Driver
 * Original Release: 5/30/85
 * Subsequent Releases: 
 * By: D. Masters, J. Smith
 * Instrument Driver Revision 14.1 (J)
 * VXI Plug&Play WIN95/WINNT Framework Revision: 3.0
 * 1260 Minimum Hardware Revision: 401901     - Supported at Rev A for all
 *                                              modules except the 1260-45
 *                                 401901-003 - Not supported
 *                                 401901-004 - Supported at Rev D
 *                                 401901-005 - Supported at Rev A if PCB
 *                                              is also stamped with part
 *                                              number 401901-004 Rev D,
 *                                              otherwise, it is supported
 *                                              at Rev B.
 * 1260 Minimum Firmware Revision: Varies on a module by module basis.
 *                                 Contact Racal Instruments to receive
 *                                 information regarding individual module
 *                                 minimum firmware requirements.
 * Modification History:
 *      Rev        Date    Comment
 *      1.1 A    05/26/95  Original Release
 *      2.1 B    01/17/96  Added 1260-38 driver support, updated to LabWin 3.1
 *      3.1 C    09/11/96  Changed DLL #ifdef in support of CVI version 4.0
 *      4.1 D    09/27/96  Added support for 1260-18, 1260-39, 1260-39S, 1260-58, 
 *                         1260-59A/B, 1260-66A/B/C cards
 *      5.1 E    11/15/96  Fixed bug in -39S driver
 *      6.1 A    08/27/97  Updated for SFP for 32-bit in WIN95/NT       
 *      7.1 B    04/07/98  Fixed bug in -64ABC driver 
 *      8.1 C    06/01/98  Fixed SFPs for 1260-30ABCD, 1260-38A, 1260-45ABC
 *		9.1 D	 06/22/98  Change SFPs so windows minimizable & moveable
 *     10.1 E    07/28/98  Change SFPs to make panels minimizable & movable
 *     11.1 F    09/25/98  Fix bug in 1260-66ABC driver.
 *     12.1 G    06/10/99  Corrected SFP for the 1260-45B and -45C.  This
 *                         driver DID NOT CHANGE, but the revision was updated
 *                         to be the same as the VXIplug&play install disk
 *                         which installs it.
 *     13.1 H    01/17/00  Corrected Main Soft Front Panel autoconnect to
 *                         continue on regardless of errors detected while
 *                         locating potential 1260-01s.  This driver DID NOT
 *                         CHANGE, but the revision was updated to be the
 *                         same as the VXIplug&play install disk which
 *                         installs it.
 *     14.1 J    01/08/02  RNB, Fix bug in ri1260_format_relay ().  See SQR 303
 *=========================================================================*/
#include <string.h>
#include <stdlib.h>
#include <ctype.h>
#include <time.h>
#include <visa.h>
#include "ri1260.h"


#define RI1260_REVISION             "14.1 (J)"
#define RI1260_MAX_CMD              (ViInt16) 300
#define RI1260_MANF_ID              (ViUInt16) 0xFFB
#define RI1260_MODEL_CODE           (ViUInt16) 0xFFF0

#define RI1260_NO_SESSION           (ViInt16) 0
#define RI1260_MAX_SEND_LEN         (ViInt16) 256
#define RI1260_LAST_PORT            (ViInt16) 11
#define PROG_ERROR_MASK             (ViInt16) 0x20
#define READY_MASK					(ViInt16) 0x10
#define RI1260_MAX_SCAN_CMD_LEN     (ViInt16) 172
#define RI1260_MAX_EXCL_CMD_LEN     (ViInt16) (RI1260_MAX_CMD - 4)
#define RI1260_MAX_EQUATE_CMD_LEN   (ViInt16) 50
#define RI1260_MAX_INSTR            (ViInt16)  12
#define RI1260_MAX_CARDS            (ViInt16)  12

/*= 1260 ERROR VALUES  ======================================================*/

#define _RI1260_ERROR   (_VI_ERROR+0x3FFC0800L) /* 0xBFFC0800L = -1074001920 */
#define RI1260_ERR(x)   ((ViStatus) (_RI1260_ERROR+(x)))

#define RI1260_MAX_INSTR_ERROR      RI1260_ERR (100)
#define RI1260_NSUP_ATTR_VALUE      RI1260_ERR (101)
#define RI1260_WRONG_CARD_TYPE      RI1260_ERR (102)
#define RI1260_SCAN_LIST_TOO_LONG   RI1260_ERR (103)
#define RI1260_BAD_SCAN_LIST_ITEM   RI1260_ERR (104)
#define RI1260_EXCL_LIST_TOO_LONG   RI1260_ERR (105)
#define RI1260_BAD_EXCL_LIST_ITEM   RI1260_ERR (106)
#define RI1260_EQUATE_LIST_TOO_LONG RI1260_ERR (107)
#define RI1260_BAD_PSETUP_RESPONSE  RI1260_ERR (108)
#define RI1260_NOT_SYNC_PORT        RI1260_ERR (109)
#define RI1260_MUST_BE_DISARMED     RI1260_ERR (110)
#define RI1260_WORD_ODD_PORT        RI1260_ERR (111)
#define RI1260_DIGIO_SYNC_TIMEOUT   RI1260_ERR (112)
#define RI1260_NSUP_TIME            RI1260_ERR (113)
#define RI1260_INCL_LIST_TOO_LONG	RI1260_ERR (114)
#define RI1260_INCL_RELAY_TWICE		RI1260_ERR (115)


/*= STATIC DATA ===========================================================*/
static ViSession initialized[RI1260_MAX_INSTR] = {RI1260_NO_SESSION,
    RI1260_NO_SESSION,RI1260_NO_SESSION,RI1260_NO_SESSION,RI1260_NO_SESSION,
    RI1260_NO_SESSION,RI1260_NO_SESSION,RI1260_NO_SESSION,RI1260_NO_SESSION,
    RI1260_NO_SESSION,RI1260_NO_SESSION,RI1260_NO_SESSION};
static ViInt16 module_type[RI1260_MAX_INSTR + 1][RI1260_MAX_CARDS + 1];
static ViChar rest_of_string[RI1260_MAX_CMD + 1] = "";

static ViChar *on_off[2] = {"OFF","ON"};
static ViChar *switch_sequence[3] = {"BBM","MBB","IMM"};
static ViChar *open_close[2] = {"OP","CL"};
static ViChar *scan_stat[3] = {"OFF","ON","CONT"};
static ViChar *store_recall[2] = {"ST","REC"};
static ViChar *arm_disarm[2] = {"OFF","ON"};
static ViChar *data_mode[3] = {"Y","W","X"};
static ViChar *pos_neg[2] = {"NEG","POS"};
static ViChar *SCCS_id = "ri1260.c 5.1 11/18/96";

/*= PROTOTYPES FOR UTILITY ROUTINES =======================================*/
ViBoolean _VI_FUNC ri1260_invalidViBooleanRange (ViBoolean val);
ViBoolean _VI_FUNC ri1260_invalidViInt16Range (ViInt16, ViInt16, ViInt16);
ViStatus _VI_FUNC ri1260_initCleanUp (ViSession rmSession, ViPSession vi,
    ViUInt32 index, ViStatus currentStatus);
ViStatus _VI_FUNC ri1260_get_card_type (ViSession, ViString, ViPInt16);
ViStatus _VI_FUNC ri1260_check_equate_list (ViSession vi, ViPInt16 this_equate_list,
    ViInt16 num_on_list);
ViStatus _VI_FUNC ri1260_read_sequence (ViSession vi, ViInt16 module,
    ViPInt16 sequence_ptr);
ViStatus _VI_FUNC ri1260_build_list (ViSession vi, ViInt16 operation,
    ViInt16 module_addr, ViInt16 mod_type, ViAInt16 states);
ViStatus _VI_FUNC ri1260_format_range (ViSession vi, ViInt16 first_index,
    ViInt16 last_index, ViInt16 mod_type);
ViStatus _VI_FUNC ri1260_format_relay (ViSession vi, ViInt16 relay, ViInt16 mod_type);
ViStatus _VI_FUNC ri1260_scan_states (ViInt16 mod_type, ViAInt16 states,
    ViString relay_info);
ViInt16 _VI_FUNC ri1260_convert_relay_to_index (ViInt16 relay, ViInt16 mod_type);
ViStatus _VI_FUNC ri1260_check_card_type (ViSession vi, ViInt16 module,
    ViInt16 should_be_type);
void _VI_FUNC ri1260_ioCleanUp (ViSession vi);
ViStatus _VI_FUNC ri1260_delay (ViReal64 delay);
ViString _VI_FUNC ri1260_itoa(ViInt16 number);
ViStatus _VI_FUNC ri1260_wait_ready(ViSession vi, ViInt16 num_seconds);



/*= DLL CODE ==============================================================*/
#if defined(WIN32) || defined(_WIN32) || defined(__WIN32__) || defined(__NT__)
/* This is required to produce a Win32 (95/NT) .DLL file */
#elif defined(_WINDOWS)
/* This is required to produce a Win16 .DLL file */
#include <windows.h>
int _VI_FUNC
LibMain(/*Handle, WORD, WORD, LPSTR*/)
{
    return 1; /* signifies success */
}

int _VI_FUNC
WEP(int arg)
{
    return 1; /* signifies success */
}
#endif

/*= DRIVER FUNCTIONS ======================================================*/
/*=========================================================================*/
/* Function: Initialize                                                    */
/* Purpose:  This function opens the instrument, queries for ID, and       */
/*           initializes the instrument to a known state.                  */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_init (ViRsrc RsrcName, ViBoolean IDQuery,
    ViBoolean reset, ViPSession vi)
{
ViStatus error;
ViSession rmSession = 0;
ViUInt16 manf_ID = 0;
ViUInt16 model_code = 0;
ViUInt32 i;

/* Range check the parameters. */
    *vi = VI_NULL;
    if (ri1260_invalidViBooleanRange (IDQuery))
        return (VI_ERROR_PARAMETER2);
    if (ri1260_invalidViBooleanRange (reset))
        return (VI_ERROR_PARAMETER3);

/* Find an empty position in the global data array. */
    for (i=0;(i < RI1260_MAX_INSTR)&&(initialized[i] != RI1260_NO_SESSION);i++);

/* Did we find an empty spot?  If not, return an error. */
    if (i >= RI1260_MAX_INSTR) return (RI1260_MAX_INSTR_ERROR);

/* Initialize entry in Instrument Table and interface for instrument. */
    error = viOpenDefaultRM (&rmSession);
    if (error < 0) return (error);

    error = viOpen (rmSession, RsrcName, VI_NULL, VI_NULL, vi);
    if (error < 0)
    {
        viClose(rmSession);
        return (error);
    }

/* There was a blank spot in the global data table so record the index in */
/* the User Data Attribute provided by VISA. */
    error = viSetAttribute (*vi, VI_ATTR_USER_DATA, i);

/* If we get back an error or a warning from the call to viSetAttribute, */
/* then our index may not have been stored properly, so return an error. */
    if  (error != VI_SUCCESS) {
        if (error == VI_WARN_NSUP_ATTR_STATE)
            error = RI1260_NSUP_ATTR_VALUE;
        if (error < 0)
            return (ri1260_initCleanUp(rmSession, vi, i, error));
    }

/* The call to viSetAttribute was OK, so mark this spot in the global */
/* data array. */
    initialized[i] = rmSession;

/*-- Identification Query -------------------------------------------------*/
/* Perform the ID Query if requested */
    if (IDQuery) {

/* Read the manufacturer ID */
        error = viGetAttribute (*vi, VI_ATTR_MANF_ID, &manf_ID);
        if (error < 0)
            return (ri1260_initCleanUp(rmSession, vi, i, error));

/* Read the model code */
        error = viGetAttribute (*vi, VI_ATTR_MODEL_CODE, &model_code);
        if (error < 0)
            return (ri1260_initCleanUp(rmSession, vi, i, error));

/* check for proper model code and manufacter ID */
        if ((model_code != RI1260_MODEL_CODE) || (manf_ID != RI1260_MANF_ID))
            return (ri1260_initCleanUp(rmSession,vi,i,VI_ERROR_FAIL_ID_QUERY));
    }

/*-- Instrument Reset -----------------------------------------------------*/
/* Reset the instrument if requested. */
    if (reset) {
        error = ri1260_reset(*vi);
        if (error < 0)
            return (ri1260_initCleanUp(rmSession, vi, i, error));
    }

/* -- Default Setup -------------------------------------------------------*/
/* Set timeout to 10 seconds */
    error = viSetAttribute(*vi, VI_ATTR_TMO_VALUE, (ViUInt32)10000);
    if (error < 0)
        return (ri1260_initCleanUp(rmSession, vi, i, error));

/* Set the input/output buffers to 500 characters */
    error = viSetBuf (*vi, VI_READ_BUF|VI_WRITE_BUF, (ViUInt32) 500);
    if (error < 0)
        return (ri1260_initCleanUp(rmSession, vi, i, error));

/* Make sure the input buffer flushes after every viScanf call */
    error = viSetAttribute(*vi, VI_ATTR_RD_BUF_OPER_MODE, VI_FLUSH_ON_ACCESS);
    if (error < 0)
        return (ri1260_initCleanUp(rmSession, vi, i, error));

/* Ensure that read operations terminate when a linefeed is received */
    error = viSetAttribute(*vi, VI_ATTR_TERMCHAR_EN, VI_TRUE);
    if (error < 0)
        return (ri1260_initCleanUp(rmSession, vi, i, error));

/* Ensure that all responses are sent one per line */
    error = viPrintf (*vi, "EOI 0\n");
    if (error < 0)
        return (ri1260_initCleanUp(rmSession, vi, i, error));

/* Go determine which 1260 modules are installed and build a global data */
/* array to describe them. */
    error = ri1260_read_card_types(*vi, module_type[i]);
    if (error < 0)
        return (ri1260_initCleanUp(rmSession, vi, i, error));

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Instrument Dependant Functions                                          */
/*=========================================================================*/

/*=========================================================================*/
/* This function configures the overall operation of the switch controller */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_configure (ViSession vi, ViInt16 conf_test,
    ViInt16 sync_out_delay, ViInt16 sync_line, ViInt16 trig_line,
    ViInt16 pow_up_recall)
{

    if (ri1260_invalidViBooleanRange (conf_test))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (sync_out_delay, 0, 655))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (sync_line, 0, 8))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (trig_line, 0, 8))
        return (VI_ERROR_PARAMETER5);

    if (ri1260_invalidViBooleanRange (pow_up_recall))
        return (VI_ERROR_PARAMETER6);

    return (viPrintf (vi, "CN %s\nDL %d\nSY %d\nTT %d\nPU %s\n",
           on_off[conf_test], sync_out_delay, sync_line,
           trig_line, on_off[pow_up_recall]));
}

/*=========================================================================*/
/* This function selects break-before-make/make-before-break/immediate     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_set_switch_sequence (ViSession vi, ViInt16 module,
    ViInt16 switch_seq)
{
ViStatus error;
ViUInt32 index;
ViInt16 max_seq;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1])
    {
    case RI1260_UNKNOWN_MODULE:
    case RI1260_14_MODULE:
    case 0:
        return (RI1260_WRONG_CARD_TYPE);

    case RI1260_54_MODULE:
        max_seq = RI1260_BBM;
        break;

    default:
        max_seq = RI1260_IMM;
        break;
    }

    if (ri1260_invalidViInt16Range (switch_seq, RI1260_BBM, max_seq))
        return (VI_ERROR_PARAMETER3);

    return (viPrintf (vi, "SE %d.%s\n", module, switch_sequence[switch_seq]));
}

/*=========================================================================*/
/* This function saves or recalls a switch setup to/from nonvoltile memory */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_store_recall_setup (ViSession vi,
    ViInt16 store_or_recall, ViInt16 mem_num)
{
    if (ri1260_invalidViInt16Range (store_or_recall, 0, 1))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (mem_num, 1, 50))
        return (VI_ERROR_PARAMETER3);

    return (viPrintf (vi, "%s %d\n", store_recall[store_or_recall], mem_num));
}

/*=========================================================================*/
/* This function defines the scan control operation                        */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_scan_control (ViSession vi, ViInt16 action)
{
    if (ri1260_invalidViInt16Range (action, 0, 2))
        return (VI_ERROR_PARAMETER2);

    return (viPrintf (vi, "SC %s\n", scan_stat[action]));
}

/*=========================================================================*/
/* This function defines the conditions which generate an interrupt        */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_intr_control (ViSession vi, ViInt16 scan_list_break,
    ViInt16 ready)
{
ViInt16 intr_mask;

    if (ri1260_invalidViInt16Range (scan_list_break, 0, 1))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (ready, 0, 1))
        return (VI_ERROR_PARAMETER3);

    intr_mask = PROG_ERROR_MASK | (scan_list_break << 3) | (ready << 4);
    return (viPrintf (vi, "SR %d\n", intr_mask));
}

/*=========================================================================*/
/* This function defines the scan list                                     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_def_scan_list (ViSession vi, ViString scan_list)
{
ViStatus error;
ViInt16 len;
ViUInt16 stat_byte;

    /* make sure the command is not too long */
    len = strlen (scan_list);
    if (len > RI1260_MAX_SCAN_CMD_LEN)
        return (RI1260_SCAN_LIST_TOO_LONG);

    /* Terminate command with a linefeed */
    error = viPrintf (vi, "SL %s\n", scan_list);
    if (error < 0) return (error);

    /* wait 1/2 second for the string to be parsed */
    ri1260_delay (0.5);

    /* read the status byte */
    error = viReadSTB (vi, &stat_byte);
    if (error < 0) return (error);

    if (stat_byte & PROG_ERROR_MASK)
        return (RI1260_BAD_SCAN_LIST_ITEM);

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function defines the exclusion list                                */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_def_excl_list (ViSession vi, ViString excl_list)
{
ViStatus error;
ViInt16 len;
ViUInt16 stat_byte;

    /* make sure the command is not too long */
    len = strlen (excl_list);
    if (len > RI1260_MAX_EXCL_CMD_LEN)
        return (RI1260_EXCL_LIST_TOO_LONG);

    /* Incorporate the command with the scan list */
    error = viPrintf (vi, "EX %s\n", excl_list);
    if (error < 0) return (error);

    /* wait 1/2 second for the string to be parsed */
    ri1260_delay (0.5);

    /* read the status byte */
    error = viReadSTB (vi, &stat_byte);
    if (error < 0) return (error);

    if (stat_byte & PROG_ERROR_MASK)
        return (RI1260_BAD_EXCL_LIST_ITEM);

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function defines the inclusion list                                */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_def_incl_list (ViSession vi,
										ViInt16 module,
										ViAInt16 relaystoGroup, 
										ViInt16 numbertoGroup)
{
ViInt16 relay1, relay2, cnt, index;
ViChar cmd[300];
ViChar *cmdptr;
int len;


	/* ensure parameters are in range */    
    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (numbertoGroup, 0, 159))
        return (VI_ERROR_PARAMETER2);

	/* specifically eliminate 1 as a numbertoGroup option */
	if (numbertoGroup == 1)
		return (VI_ERROR_PARAMETER2);

	/* check if this is intended to clear existing groups */
	if (numbertoGroup == 0)
		return( viPrintf(vi, "INCL 0\n") );

	/* ensure all  relays are valid */
	for (cnt = 0;  cnt < numbertoGroup;  ++cnt)
		if (ri1260_invalidViInt16Range (relaystoGroup[cnt], 0, 159))
			return(VI_ERROR_PARAMETER3);
			
	/* ensure relays are all unique */
	for (cnt = 0;  cnt < numbertoGroup;  ++cnt)
		for (index = cnt+1;  index < numbertoGroup;  ++index)
			if (relaystoGroup[cnt] == relaystoGroup[index])
				return( RI1260_INCL_RELAY_TWICE );

	/* form the command */
	cmdptr = cmd;
	cmd[0] = 0;
	for (cnt = 0;  cnt < numbertoGroup; ++cnt)
		{
		relay1 = relaystoGroup[cnt];
		relay2 = relay1;
		
		/* group consecutive relays with a hyphen */
		if (cnt < numbertoGroup-1)
			{
			index = cnt + 1;
			relay2 = relay1;
			while (index < numbertoGroup)
				{
				if (relaystoGroup[index] == relay2+1)
					++relay2;
				else
					break;
					
				++index;
				}
			}
			
		/* add the comma separator and the relay range */
		strcat(cmdptr,",");
		strcat(cmdptr,ri1260_itoa(relay1));
		if (relay1 != relay2)
			{
			cnt = index-1;
			strcat(cmdptr, "-");
			strcat(cmdptr,ri1260_itoa(relay2));
			}
			
		cmdptr = cmd + strlen(cmd);
		len = strlen(cmd);
		if (len > 250)
			return( RI1260_INCL_LIST_TOO_LONG );
		}
		
		
	/* so now I've formed the command, send it to the device */
	/* remove the first comma from the INCL data */
	return( viPrintf(vi, "INCL %d.%s\n", module, cmd+1) );
}

/*=========================================================================*/
/* This function defines the equate list                                   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_def_equate_list (ViSession vi, ViString equate_list)
{
ViStatus error;
ViInt16 len;
ViUInt16 stat_byte;


    /* make sure the command is not too long */
    len = strlen (equate_list);
    if (len > RI1260_MAX_EQUATE_CMD_LEN)
        return (RI1260_EQUATE_LIST_TOO_LONG);

    /* Incorporate the command with the scan list */
    error = viPrintf (vi, "EQ %s\n", equate_list);
    if (error < 0) return (error);

    /* wait 1/2 second for the string to be parsed */
    ri1260_delay (0.5);

    /* read the status byte */
    error = viReadSTB (vi, &stat_byte);
    if (error < 0) return (error);

    if (stat_byte & PROG_ERROR_MASK)
        return (RI1260_BAD_EXCL_LIST_ITEM);

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function opens or closes multiple relays on the 1260               */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_operate_multiple (ViSession vi, ViInt16 module,
    ViAInt16 states)
{
ViStatus error;
ViUInt32 index;
ViInt16 switch_seq;
ViInt16 mod_type;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    mod_type = module_type[index][module-1];
    switch (mod_type) {
    case RI1260_14_MODULE:
    case RI1260_UNKNOWN_MODULE:
    case 0:         return (RI1260_WRONG_CARD_TYPE);
    }

    error = ri1260_read_sequence(vi, module, &switch_seq);
    if (error < 0) return (error);

    /* if make-before-break, close the relays before opening.  Otherwise */
    /* open before closing */
    if (switch_seq == RI1260_MBB) {
        error = ri1260_build_list (vi, RI1260_CLOSE_RELAY, module, mod_type,
            states);
        if (error < 0) return (error);

        error = ri1260_build_list (vi, RI1260_OPEN_RELAY, module, mod_type,
            states);
        if (error < 0) return (error);
    } else {
        error = ri1260_build_list (vi, RI1260_OPEN_RELAY, module, mod_type,
            states);
        if (error < 0) return (error);

        error = ri1260_build_list (vi, RI1260_CLOSE_RELAY, module, mod_type,
            states);
        if (error < 0) return (error);
    }

    return (VI_SUCCESS);
}
/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-12             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_12_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (relay, 0, 19))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_12_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%.1d\n", open_close[operation], module,
        relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-13             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_13_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (relay, 0, 39))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_13_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%.1d\n", open_close[operation], module,
        relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-16             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_16_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (relay, 0, 39))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_16_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%.1d\n", open_close[operation], module,
        relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-17             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_17_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (relay, 0, 79))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_17_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%.1d\n", open_close[operation], module,
        relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-18             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_18_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (relay, 0, 159))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_18_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%.3d\n", open_close[operation], module,
        relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-20             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_20_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (relay, 0, 19))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_20_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%.1d\n", open_close[operation], module,
        relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-30 (A,B,C,D)   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_30_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 relay)
{
ViInt16 max_group;
ViInt16 max_relay;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_30A_MODULE:
        max_group = 0;
        max_relay = 39;
        break;
    case RI1260_30B_MODULE:
        max_group = 1;
        max_relay = 19;
        break;
    case RI1260_30C_MODULE:
        max_group = 3;
        max_relay = 9;
        break;
    case RI1260_30D_MODULE:
        max_group = 7;
        max_relay = 4;
        break;
    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (group, 0, max_group))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.2d\n", open_close[operation], module,
        group, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-35 (A or B)    */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_35_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay)
{
ViInt16  max_relay;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_35A_MODULE:     max_relay = 47;     break;
    case RI1260_35B_MODULE:     max_relay = 96;     break;
    default:                    return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER4);

    return (viPrintf (vi, "%s %d.%.1d\n", open_close[operation], module, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-36             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_36_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (mux, 0, 11))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (relay, 0, 7))
        return (VI_ERROR_PARAMETER5);

    error = ri1260_check_card_type (vi, module, RI1260_36_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%d%.1d\n", open_close[operation], module,
        mux, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-37 (A or B)    */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_37_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 relay)
{
ViInt16 max_group;
ViInt16 max_relay;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    max_group = 1;
    switch (module_type[index][module-1]) {
    case RI1260_37A_MODULE:
        if(group == 0)
            max_relay = 23;
        else
            max_relay = 39;
        break;
    case RI1260_37B_MODULE:
        if(group == 0)
            max_relay = 48;
        else
            max_relay = 39;
        break;
    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (group, 0, max_group))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.2d\n", open_close[operation], module,
        group, relay));
}


/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-38 (A,B)       */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_38_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 interconnect, ViInt16 mux, ViInt16 channel)
{
ViInt16 max_interconnect;
ViInt16 max_mux;
ViInt16 max_channel;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_38A_MODULE:
        max_interconnect = 9;
        if(interconnect == 0)
        	max_mux = 15;
        else
        	max_mux = 0;
        if((interconnect == 0) || (interconnect == 9))
        	max_channel = 7;
        else
        	max_channel = 2;
        break;

    case RI1260_38B_MODULE:
        max_interconnect = 9;
        if(interconnect == 0)
        	max_mux = 14;
        else
        	max_mux = 0;
        if((interconnect == 0) || (interconnect == 9))
        	max_channel = 7;
        else
        	max_channel = 0;
        break;

    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if ((ri1260_invalidViInt16Range (interconnect, 0, max_interconnect)) ||
        ((module_type[index][module-1] == RI1260_38B_MODULE) &&
         (interconnect == 8)))
        return (VI_ERROR_PARAMETER4);

    if ((ri1260_invalidViInt16Range (mux, 0, max_mux)) ||
        ((module_type[index][module-1] == RI1260_38B_MODULE) &&
         ((mux & 0x1) != 0)))
        return (VI_ERROR_PARAMETER5);

    if ((ri1260_invalidViInt16Range (channel, 0, max_channel)) ||
        ((module_type[index][module-1] == RI1260_38A_MODULE) &&
         (interconnect == 8) && (channel != 2)))
        return (VI_ERROR_PARAMETER6);

    return (viPrintf (vi, "%s %d.%d%.2d%.1d\n", open_close[operation], module,
                     interconnect, mux, channel));
}


/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-39             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_39_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 relay_type, ViInt16 channel)
{
ViStatus error;
ViInt16 max_channel;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

	switch( relay_type )
		{
		case RI1260_39_DPST:			max_channel = 4;	break;
		case RI1260_39_SPST:			max_channel = 47;	break;
		case RI1260_39_1X2_MUX_1:	
		case RI1260_39_1X2_MUX_2:
		case RI1260_39_1X2_MUX_3:
		case RI1260_39_1X2_MUX_4:
		case RI1260_39_1X2_MUX_5:
		case RI1260_39_1X2_MUX_6:   	max_channel = 1;	break;
		case RI1260_39_1X4_MUX_1:
		case RI1260_39_1X4_MUX_2:
		case RI1260_39_1X4_MUX_3:		max_channel = 3;	break;
		case RI1260_39_2X8_MATRIX_1:
		case RI1260_39_2X8_MATRIX_2:
		case RI1260_39_2X8_MATRIX_3:
		case RI1260_39_2X8_MATRIX_4:
		case RI1260_39_2X8_MATRIX_5:
		
		    if (ri1260_invalidViInt16Range (channel, 0, 7)
		    &&  ri1260_invalidViInt16Range (channel, 10, 17))
        		return (VI_ERROR_PARAMETER5);
        	
        	break;
        	
        default:
        	return( VI_ERROR_PARAMETER4 );
        }

	if (relay_type < RI1260_39_2X8_MATRIX_1)
		if (ri1260_invalidViInt16Range(channel, 0, max_channel))
			return( VI_ERROR_PARAMETER5 );

    error = ri1260_check_card_type (vi, module, RI1260_39_MODULE);
    if (error < 0) return (error);

    return (viPrintf (vi, "%s %d.%04d\n", open_close[operation], module,
        relay_type + channel));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-39S-1507 1x4   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_39S_operate_1x4 (ViSession vi, ViInt16 module,
                                 ViInt16 operation, ViInt16 mux,
                                 ViInt16 relay)
{
ViStatus error;
ViInt16 max_relay;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (mux, 0, 5))
        return (VI_ERROR_PARAMETER4);

	if (operation == 1)
		max_relay = 4;
	else
		max_relay = 3;
		
    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    error = ri1260_check_card_type (vi, module, RI1260_39S_MODULE);
    if (error < 0) return (error);
    
    
    /* we must enforce an "1 of 4 closed at one time" relationship */
    if (operation == RI1260_CLOSE_RELAY)
    	{
    	error = viPrintf(vi, "OP %d.%d-%d\n", module, mux*4, mux*4+3);
    	if (error < 0)
    		return( error );
    		
    	if (relay != 4)
    		error = viPrintf(vi, "CL %d.%d\n", module, mux*4+relay);
    	}
    else
    	{
    	error = viPrintf(vi, "OP %d.%d\n", module, mux*4+relay);
    	}
    									
	return( error );
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-39S-1507 1x8   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_39S_operate_1x8 (ViSession vi, ViInt16 module,
                                 ViInt16 operation, ViInt16 mux,
                                 ViInt16 relay)
{
ViStatus error;
ViInt16 max_relay;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (mux, 0, 3))
        return (VI_ERROR_PARAMETER4);

	if (operation == 1)
		max_relay = 8;
	else
		max_relay = 7;
		
    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    error = ri1260_check_card_type (vi, module, RI1260_39S_MODULE);
    if (error < 0) return (error);

    /* we must enforce an "1 of 8 closed at one time" relationship */
    if (operation == RI1260_CLOSE_RELAY)
    	{
    	error = viPrintf(vi, "OP %d.%d-%d\n", module, mux*8, mux*8+7);
    	if (error < 0)
    		return( error );
    		
    	if (relay != 8)
    		error = viPrintf(vi, "CL %d.%d\n", module, mux*8+relay);
    	}
    else
    	{
    	error = viPrintf(vi, "OP %d.%d\n", module, mux*8+relay);
    	}
    									
	return( error );
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-40 (A,B,C)     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_40_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 matrix, ViInt16 row, ViInt16 column)
{
ViInt16 max_matrix;
ViInt16 max_row;
ViInt16 max_column;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_40A_MODULE:
        max_matrix = 0;
        max_row = 3;
        max_column = 23;
        break;

    case RI1260_40B_MODULE:
        max_matrix = 0;
        max_row = 7;
        max_column = 11;
        break;

    case RI1260_40C_MODULE:
        max_matrix = 1;
        max_row = 3;
        max_column = 11;
        break;

    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (matrix, 0, max_matrix))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (row, 0, max_row))
        return (VI_ERROR_PARAMETER5);

    if (ri1260_invalidViInt16Range (column, 0, max_column))
        return (VI_ERROR_PARAMETER6);

    return (viPrintf (vi, "%s %d.%d%.1d%.2d\n", open_close[operation], module,
                     matrix, row, column));
}


/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-45 (A,B,C)     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_45_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 matrix, ViInt16 row, ViInt16 column)
{
ViInt16 max_matrix;
ViInt16 max_row;
ViInt16 max_column;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_45A_MODULE:
    case RI1260_45B_MODULE:
    case RI1260_45C_MODULE:
        max_matrix = 3;
        max_row = 3;
        max_column = 15;
        break;

    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (matrix, 0, max_matrix))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (row, 0, max_row))
        return (VI_ERROR_PARAMETER5);

    if (ri1260_invalidViInt16Range (column, 0, max_column))
        return (VI_ERROR_PARAMETER6);

    return (viPrintf (vi, "%s %d.%d%.1d%.2d\n", open_close[operation], module,
                        matrix, row, column));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-50 (A,B,C or D)*/
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_50_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay)
{
ViInt16 max_group;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_50A_MODULE:
    case RI1260_50C_MODULE:     max_group = 7;      break;
    case RI1260_50B_MODULE:
    case RI1260_50D_MODULE:     max_group = 15;     break;
    default:                    return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (grp, 0, max_group))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (relay, 0, 4))
        if (!((grp > 0) && (relay == 9)))
            return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.1d\n", open_close[operation], module,
        grp, relay));
}
/*=========================================================================*/
/* This function opens or closes relays necessary for I-O channel		   */
/*  in 1260-51 ( 2 x 2 X 36 RF MUX)                                        */
/*=========================================================================*/
									
ViStatus _VI_FUNC ri1260_operate_51_2X36_Matrix ( ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 inportA, ViInt16 inportB)
{    
        ViStatus error;
	 	ViInt16 IN[2]={-1,-1}, OUT[2]={-1,-1},i,count,met_first=0;
    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_51_MODULE);
    if (error < 0) return (error);

	if (inportA>-1 && inportA<36 ) { IN[0]=inportA; OUT[0]=40;}
	if (inportB>-1 && inportB<36 ) { IN[1]=inportB; OUT[1]=50;}       
	count=0;
	for (i=0;i<2; i++)
	     if(IN[i]>-1 && IN[i]<36)  count++;
	 
	if (count)
	{
	 error = viPrintf (vi, "%s %d.", open_close[operation],module); 
	 if (error < 0) return (error);  
	 
		 for (i=0;i<count;i++)   
	 	{
	 	if (met_first >0)  // active from 2nd path request
            	{   error = viPrintf (vi, ",");
                	if (error < 0) return (error);
                }
	 	if (IN[i]>-1)
	   		{
	   		error= viPrintf (vi, "%02d%02d\n",IN[i],OUT[i]);
	    	if (error < 0) return (error);  
	   		} 
	   	
           met_first=1;     
	   		
	 	}  
	   
	 error =viPrintf (vi, "\n");
     if (error < 0) return (error);     
     error = VI_SUCCESS;
    } // if (count...

	/* wait for up to 2 seconds for the 1260 to complete the command */
	if (error >= 0)
		error = ri1260_wait_ready(vi,2);
    return (error);
}

/*=========================================================================*/
/* This function opens or closes relays necessary for I-O channel		   */
/*  in 1260-51 ( 3 x 2 X 12 RF MUX)                                        */
/*=========================================================================*/
 ViStatus _VI_FUNC ri1260_operate_51_2X12_Matrix ( ViSession vi, ViInt16 module,
    ViInt16 operation, 
    ViInt16 inA1, ViInt16 inA2, ViInt16 inA3, 
    ViInt16 inB1, ViInt16 inB2, ViInt16 inB3)

{
    ViInt16 count,	met_first;
    ViStatus error;
	ViInt16 IN[4][3],i,j;	
 
    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_51_MODULE);
    if (error < 0) return (error);
 
 
    for (i= 2;i<4;i++) //2,3
	   for (j= 0;j<3;j++)  //0,1,2
	  	IN[i][j]=-9; // not used
    count=0;
/* inA1-->ch 20     inB1-->ch 30   
   inA2-->ch 21     inB2-->ch 31   
   inA3-->ch 22     inB3-->ch 32    */
    IN[2][0]=inA1;  IN[3][0]=inB1;
	IN[2][1]=inB2;  IN[3][1]=inB2;
	IN[2][2]=inA3;  IN[3][2]=inB3;
/* process mutiple path */
/* send out command ex. "OPEN 1.0010,1240,0320" */    	
	for (i= 2;i<4;i++)  //2,3
	  	for (j= 0;j<3;j++)  //0,1,2
	  	if (IN[i][j]>-1 )  count++;

	met_first=0; 
	if (count>0)
    {
    error = viPrintf (vi, "%s %d.", open_close[operation],module);   // CL 1.
    if (error < 0) return (error);
    
    for (i= 2;i<4;i++)  //2,3
	  	for (j= 0;j<3;j++)  //0,1,2
	  	if (IN[i][j]>-1 )
	  	{
        if (met_first >0)  // active from 2nd path request
            	{   error = viPrintf (vi, ",");
                	if (error < 0) return (error);
                }
        error =viPrintf (vi, "%02d%02d", IN[i][j],i*10+j);
        met_first=1;
    	if (error < 0) return (error); 
        } //if
        // for j
    // for i
        error =viPrintf (vi, "\n");
    	if (error < 0) return (error);     
        
        error = VI_SUCCESS;
    
    } // if (count...

	/* wait for up to 2 seconds for the 1260 to complete the command */
	if (error >= 0)
		error = ri1260_wait_ready(vi,2);
    return (error);
}

/*=========================================================================*/
/* This function opens or closes relays necessary for I-O channel		   */
/*  in 1260-51 ( 6 x 2 X 6 RF MUX)                                        */
/*=========================================================================*/
 ViStatus _VI_FUNC ri1260_operate_51_2X6_Matrix ( ViSession vi, ViInt16 module,
    ViInt16 operation, 
    ViInt16 IN_a1, ViInt16 IN_a2, ViInt16 IN_a3,ViInt16 IN_a4, ViInt16 IN_a5, ViInt16 IN_a6,
   ViInt16 IN_b1, ViInt16 IN_b2, ViInt16 IN_b3,ViInt16 IN_b4, ViInt16 IN_b5, ViInt16 IN_b6  )
{
    ViInt16 i,j, count, first_met;
    ViStatus error;
    ViInt16 IN[2][6];
    
    
    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_51_MODULE);
    if (error < 0) return (error); 
    
/* count how many paths are requested */

	 IN[0][0]=IN_a1;  IN[1][0]=IN_b1;
	 IN[0][1]=IN_a2;  IN[1][1]=IN_b2; 
	 IN[0][2]=IN_a3;  IN[1][2]=IN_b3;
	 IN[0][3]=IN_a4;  IN[1][3]=IN_b4; 
	 IN[0][4]=IN_a5;  IN[1][4]=IN_b5;
	 IN[0][5]=IN_a6;  IN[1][5]=IN_b6; 
	 
	
      count=0;
      first_met=0;
      for ( i=0; i<2; i++)  
          for ( j=0; j<6; j++)   
      if (IN[i][j]>-1) count++; 
/* send out command ex. "OPEN 1.0010,1240,0320" */     
if (count>0)
   {	    error = viPrintf (vi, "%s %d.", open_close[operation],module);
		for ( i=0; i<2; i++)  
          for ( j=0; j<6; j++)   
              if (IN[i][j]>-1)
			   {
			  
               if (error < 0) return (error);
			   if (first_met>0)  
			         {      error = viPrintf (vi, ",");
                            if (error < 0) return (error);
                     }

               error =viPrintf (vi, "%02d%02d", IN[i][j],i*10+j);
               first_met=1;
    	       if (error < 0) return (error); 
               } // if (IN...
         error =viPrintf (vi, "\n");
         
    	 if (error < 0) return (error);     
   error = VI_SUCCESS;

	/* wait for up to 3 seconds for the 1260 to complete the command */
	if (error >= 0)
		error = ri1260_wait_ready(vi,3);
   }    
    return (error);
    
    
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-54 6 1x4 MUX   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_54_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay)
{
ViInt16 max_relay;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_54_MODULE);
    if (error < 0) return (error);

    if (ri1260_invalidViInt16Range (mux, 0, 5))
        return (VI_ERROR_PARAMETER4);

    if (operation == RI1260_OPEN_RELAY)
        max_relay = 3;
    else
        max_relay = 4;

    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.1d\n", open_close[operation], module,
        mux, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-58 4 by 1x8 MUX*/
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_58_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_58_MODULE);
    if (error < 0) return (error);

    if (ri1260_invalidViInt16Range (mux, 0, 3))
        return (VI_ERROR_PARAMETER4);
    if (ri1260_invalidViInt16Range (relay, 0, 7))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%d\n", open_close[operation], module,
        mux, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-59 (A,B) 1x4 MUX */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_59_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 mux, ViInt16 relay)
{
ViInt16 max_mux;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_59A_MODULE:		max_mux = 3;		break;
    case RI1260_59B_MODULE:     max_mux = 8;		break;
    default:                    return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (mux, 0, max_mux))
        return (VI_ERROR_PARAMETER4);
    if (ri1260_invalidViInt16Range (relay, 0, 3))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%d\n", open_close[operation], module,
        mux, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-60             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_60_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 relay)
{
ViInt16 max_relay;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_60_MODULE);
    if (error < 0) return (error);

/* check for max bank (group) <= 2  */
    if (ri1260_invalidViInt16Range (group, 0, 2))
        return (VI_ERROR_PARAMETER4);

    if (group <= 1)
        max_relay = 11;
    else
        max_relay = 2;

    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.2d\n", open_close[operation], module,
        group, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-64 (A,B,C)       */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_64_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 relay)
{
ViInt16 max_relay;
ViInt16 max_bank;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_64A_MODULE:
        max_bank = 5;
        max_relay = 56;
        break;
    case RI1260_64B_MODULE:
        max_bank = 3;
        max_relay = 44;
        break;
    case RI1260_64C_MODULE:
        max_bank = 2;
        max_relay = 38;
        break;
    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (group, 0, max_bank))
        return (VI_ERROR_PARAMETER4);

    if (group <= 1)
        max_relay = 15;
    else
        max_relay = 5;

    if (ri1260_invalidViInt16Range (relay, 0, max_relay))
        return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.2d\n", open_close[operation], module,
        group, relay));
}


/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-66 (A,B,C)     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_66_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 group, ViInt16 channel)
{
ViInt16 max_bank;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_66A_MODULE:
        max_bank = 5;
        break;
    case RI1260_66B_MODULE:
        max_bank = 3;
        break;
    case RI1260_66C_MODULE:
        max_bank = 1;
        break;
    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (group, 0, max_bank))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (channel, 0, 6))
        return (VI_ERROR_PARAMETER5);
        
    if (channel == 6)  /* Disconnect all poles for specified group */
    	error = viPrintf(vi,"OP %d.%d0-%d5\n", module, group, group);
    else
		error = viPrintf (vi, "%s %d.%d%d\n", open_close[operation], module,
        			group, channel);

	return (error);        			
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-75 (A or B)    */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_75_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay)
{
ViInt16 max_group;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_75A_MODULE:     max_group = 7;      break;
    case RI1260_75B_MODULE:     max_group = 15;     break;
    default:                    return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (grp, 0, max_group))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (relay, 0, 4))
        if (!((grp > 0) && (relay == 9)))
            return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.1d\n", open_close[operation], module,
        grp, relay));
}

/*=========================================================================*/
/* This function opens or closes a single relay on the 1260-93 (A or B)    */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_93_operate_single (ViSession vi, ViInt16 module,
    ViInt16 operation, ViInt16 grp, ViInt16 relay)
{
ViInt16 max_group;
ViStatus error;
ViUInt32 index;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (operation, 0, 1))
        return (VI_ERROR_PARAMETER3);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    switch (module_type[index][module-1]) {
    case RI1260_93A_MODULE:     max_group = 7;      break;
    case RI1260_93B_MODULE:     max_group = 15;     break;
    default:                    return (RI1260_WRONG_CARD_TYPE);
    }

    if (ri1260_invalidViInt16Range (grp, 0, max_group))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (relay, 0, 4))
        if (!((grp > 0) && (relay == 9)))
            return (VI_ERROR_PARAMETER5);

    return (viPrintf (vi, "%s %d.%d%.1d\n", open_close[operation], module,
        grp, relay));
}

/*=========================================================================*/
/* This function performs a serial poll on the instrument.  The status     */
/* byte of the instrument is placed in the response variable.              */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_read_status_byte (ViSession vi, ViPInt16 status_byte)
{
ViStatus error;
ViUInt16 uint16_status;

    /* read the status byte */
    error = viReadSTB (vi, &uint16_status);
    if (error < 0) return (error);

    /* mask to leave only the least significant 8 bits */
    *status_byte = uint16_status & 0xFF;

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function reads the states of all relays for the specified module   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_read_relay_states (ViSession vi, ViInt16 module,
    ViPInt16 mod_type, ViAInt16 states)
{
ViStatus error;
ViUInt32 index;
ViInt16 type;
ViInt16 keep_going;
ViInt32 read_module;
ViInt16 last_relay;
ViInt16 i;
ViInt16 loops;
ViChar *string_ptr;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    type = module_type[index][module-1];
    if (type == 0 || type == RI1260_UNKNOWN_MODULE)
        return (RI1260_WRONG_CARD_TYPE);

    if (type == RI1260_14_MODULE) {
    /* don't assign the relay states, just return module type */
        *mod_type = type;
        return (VI_SUCCESS);
    }

    error = viPrintf (vi, "PD %d\n", module);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* 1st response identifies the type of module, ignore */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* 2nd response holds the first line of closed relays */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* scan the response to ensure that the module address was included */
    /* Allow for no relays closed (scan returns 1) */
    read_module = atoi(rest_of_string);
    if ((strlen(rest_of_string) < 5) || (read_module != module))
        return (RI1260_BAD_PSETUP_RESPONSE);

    /* determine the last index to use for initializing the state array */
    switch (type) {
    case RI1260_12_MODULE:  last_relay = 19;    break;
    case RI1260_13_MODULE:  last_relay = 39;    break;
    case RI1260_16_MODULE:  last_relay = 39;    break;
    case RI1260_17_MODULE:  last_relay = 79;    break;
    case RI1260_18_MODULE:	last_relay = 159;	break;
    case RI1260_20_MODULE:  last_relay = 19;    break;
    case RI1260_30A_MODULE:
    case RI1260_30B_MODULE:
    case RI1260_30C_MODULE:
    case RI1260_30D_MODULE: last_relay = 39;    break;
    case RI1260_35A_MODULE: last_relay = 47;    break;
    case RI1260_35B_MODULE: last_relay = 96;    break;
    case RI1260_36_MODULE:  last_relay = 95;    break;
    case RI1260_37A_MODULE:	last_relay = 63;    break;
    case RI1260_37B_MODULE:	last_relay = 88;    break;
    case RI1260_38A_MODULE:	last_relay = 157;   break;
    case RI1260_38B_MODULE:	last_relay = 79;    break;
    case RI1260_39_MODULE:	last_relay = 156;	break;
    case RI1260_39S_MODULE:	last_relay = 31;	break;
    case RI1260_40A_MODULE:
    case RI1260_40B_MODULE:
    case RI1260_40C_MODULE: last_relay = 95;    break;
    case RI1260_45A_MODULE:
    case RI1260_45B_MODULE:
    case RI1260_45C_MODULE: last_relay = 255;   break;
    case RI1260_50A_MODULE: last_relay = 47;    break;
    case RI1260_50B_MODULE: last_relay = 95;    break;
    case RI1260_50C_MODULE: last_relay = 47;    break;
    case RI1260_50D_MODULE: last_relay = 95;    break;
    case RI1260_54_MODULE:  last_relay = 23;    break;
    case RI1260_58_MODULE:	last_relay = 31;	break;
    case RI1260_59A_MODULE:	last_relay = 15;	break;
    case RI1260_59B_MODULE:	last_relay = 31;	break;
    case RI1260_60_MODULE:  last_relay = 26;    break;
    case RI1260_64A_MODULE: last_relay = 55;    break;
    case RI1260_64B_MODULE: last_relay = 43;    break;
    case RI1260_64C_MODULE: last_relay = 37;    break;
    case RI1260_66A_MODULE:	last_relay = 35;	break;
    case RI1260_66B_MODULE:	last_relay = 23;	break;
    case RI1260_66C_MODULE:	last_relay = 11;	break;
    case RI1260_75A_MODULE: last_relay = 47;    break;
    case RI1260_75B_MODULE: last_relay = 95;    break;
    case RI1260_93A_MODULE: last_relay = 47;    break;
    case RI1260_93B_MODULE: last_relay = 95;    break;

    default:
        return (RI1260_WRONG_CARD_TYPE);
    }

    /* indicate all relays are open.  The closed ones will be filled in */
    /* by the scanning of the response */
    for (i = 0; i <= last_relay; ++i)
        states[i] = RI1260_OPEN_RELAY;

    /* repeatedly scan/read until the response does not end with a comma */
    loops = 0;
    for (keep_going = 1; (loops < 20) && keep_going; ++loops) {
    	/* skip the module address returned on the first reply line only */
    	if (loops == 0)
    		string_ptr = &rest_of_string[5];
    	else
    		string_ptr = rest_of_string;
    	
    	if (isdigit(*string_ptr)) {
        	error = ri1260_scan_states (type, states, string_ptr);
        		if (error < 0) return (error);
        }

    /* get the next response */
        error = viScanf (vi, "%t", rest_of_string);
        if (error < 0) {
            ri1260_ioCleanUp(vi);
            return (error);
        }

    /* the last response should be "<module addr>.END" */
        keep_going = (rest_of_string[4] != 'E');
    }

    /* return the type of module at the specified address */
    *mod_type = type;

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Function: Read Card Types                                               */
/* Purpose:  This function reads the type of cards in the system and       */
/*           places the code for the cards in the destination array.       */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_read_card_types (ViSession vi, ViAInt16 module_type_array)
{
ViStatus error;
ViChar *marker;
ViInt16 i;

/* Clear out the existing enties in the module type array */
    for (i = 0; i < RI1260_MAX_INSTR; i++)
        module_type_array[i] = RI1260_NO_MODULE;;

/* send the query for the card types */
    error = viPrintf (vi, "PD 1-12\n");
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

/* read the first line of the response */
    if (viScanf (vi, "%t", rest_of_string) < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

/* Read the response until we get the xxx.END string */
    while (rest_of_string[4] != 'E') {

/* If this is a new card, get the card type and place it in module_type array */
        if ((marker = strstr(rest_of_string, "1260-")) != 0)
            if ((error = ri1260_get_card_type (vi, rest_of_string,
                    module_type_array)) < 0)
                return (error);

/* Read the next line of the response */
        if (viScanf (vi, "%t", rest_of_string) < 0) {
            ri1260_ioCleanUp(vi);
            return (error);
        }
    }
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function defines the input/output port modes for the 1260-14 card  */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_def_port_modes (ViSession vi, ViInt16 module,
    ViInt16 port_io_bitmask)
{
ViInt16 port;
ViInt16 check_mask;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port_io_bitmask, 0, 4095))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Initially format an enable list with no ports enabled (all inputs) */
    error = viPrintf (vi, "SE %d.EN", module);
    if (error < 0) return (error);

    check_mask = 1;

    /* Add each output port to the enable list */
    for (port = 0; port <= RI1260_LAST_PORT; ++port) {
    if (port_io_bitmask & check_mask) {
        error = viPrintf (vi, ",%d", port);
            if (error < 0) return (error);
        }
    check_mask <<= 1;
    }

    return (viPrintf (vi, "\n"));
}

/*=========================================================================*/
/* This function sets selected digital I/O card ports to synchronous mode  */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_set_sync_mode (ViSession vi, ViInt16 module,
    ViInt16 last_sync_port)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (last_sync_port, -1, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Format the command string */
    return (viPrintf (vi, "SE %d.SYNC,%d\n", module, last_sync_port + 1));
}

/*=========================================================================*/
/* This function defines the high/low active edge/level for handshakes     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_handshake_polarity (ViSession vi, ViInt16 module,
    ViInt16 clkin, ViInt16 busy)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (clkin, RI1260_NEG, RI1260_POS))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (busy, RI1260_NEG, RI1260_POS))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Format and send the command strings */
    error = viPrintf (vi, "SE %d.CL,%s\n", module, pos_neg[clkin]);
    if (error < 0) return (error);

    return (viPrintf (vi, "SE %d.BU,%s\n", module, pos_neg[busy]));
}

/*=========================================================================*/
/* This function reads the configuration for the digital I/O card          */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_read_digio_conf (ViSession vi, ViInt16 module,
    ViPInt16 io_bitmask, ViPInt16 last_sync_port, ViPInt16 arm_state,
    ViPInt16 clkin, ViPInt16 busy)
{
ViInt16   i, enable_array[12];
ViChar    *marker;
ViStatus  error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Format and send the prompt command string */
    error = viPrintf (vi, "PS %d\n", module);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* Scan and convert the responses */
    /* First response identifies the card as 1260-14, so skip it */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* Fill the enable array with -1 so that the end of list can be detected */
    for (i = 0; i < 12; ++i)
        enable_array[i] = -1;

    /* Second response is the enabled buffer list */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    *io_bitmask = 0;
    marker = &rest_of_string[12];
    for (i = 0; (i < 12) && isdigit(*marker); ++i) {
        *io_bitmask |= 1 << atoi(marker);
        while(isdigit(*(++marker)));
        if(*marker == ',') marker += 1;
    }

    /* Next response is the last synchronous port identification */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    *last_sync_port = atoi(&rest_of_string[10]) - 1;

    /* Next response is the BUSY handshake state */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* Look for 'P' for positive */
    if (rest_of_string[10] == 'P')
        *busy = VI_TRUE;
    else
        *busy = VI_FALSE;

    /* Next response is the CLKIN handshake state */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* Look for 'P' for positive */
    if (rest_of_string[11] == 'P')
        *clkin = VI_TRUE;
    else
        *clkin = VI_FALSE;


    /* Next response is the ARM state */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* Look for 'N' for on */
    if (rest_of_string[10] == 'N')
        *arm_state = VI_TRUE;
    else
        *arm_state = VI_FALSE;

    /* Get the last response (xxx. END) */
    error = (viScanf (vi, "%t", rest_of_string));
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    } else return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function sets up the selected port to perform a synchronous read   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_setup_sync_read (ViSession vi, ViInt16 module,
    ViInt16 port, ViInt16 data_length, ViInt16 max_data)
{
ViInt16 io_bitmask;
ViInt16 last_sync_port;
ViInt16 arm_state;
ViInt16 clkin;
ViInt16 busy;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port, 0, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (data_length, RI1260_BYTE, RI1260_WORD))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (max_data, 0, 256))
        return (VI_ERROR_PARAMETER5);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Read the set-up of the card */
    error = ri1260_read_digio_conf (vi, module, &io_bitmask,
                     &last_sync_port, &arm_state, &clkin,
                     &busy);
    if (error < 0) return (error);

    /* Ensure the port is synchronous */
    if (last_sync_port < port)
        return (RI1260_NOT_SYNC_PORT);

    /* Ensure the port is disarmed */
    if (arm_state == RI1260_ARM)
        return (RI1260_MUST_BE_DISARMED);

    /* If word mode is selected, ensure port is even */
    if (data_length == RI1260_WORD && (port & 1))
        return (RI1260_WORD_ODD_PORT);

    /* Format and send a command to select the desired operation */
    /* Always specify hexadecimal format to facilitate data read-back */
    if (data_length == RI1260_WORD)
        return (viPrintf (vi, "SE %d.RD,%d,W,H,%d\n", module, port, max_data));
    else
        return (viPrintf (vi, "SE %d.RD,%d,H,%d\n", module, port, max_data));
}

/*=========================================================================*/
/* This function sets up the selected port to perform a synchronous write  */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_setup_sync_write (ViSession vi, ViInt16 module,
    ViInt16 port, ViInt16 data_length, ViInt16 num_data, ViAInt32 data_array)
{
ViInt16 io_bitmask;
ViInt16 last_sync_port;
ViInt16 arm_state;
ViInt16 clkin;
ViInt16 busy;
ViInt16 i;
ViInt16 max_data_entries;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port, 0, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (data_length, RI1260_BYTE, RI1260_WORD))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (num_data, 1, 256))
        return (VI_ERROR_PARAMETER5);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Read the set-up of the card */
    error = ri1260_read_digio_conf (vi, module, &io_bitmask,
                     &last_sync_port, &arm_state, &clkin, &busy);
    if (error < 0) return (error);

    /* Ensure the port is synchronous */
    if (last_sync_port < port)
        return (RI1260_NOT_SYNC_PORT);

    /* Ensure the port is disarmed */
    if (arm_state == RI1260_ARM)
        return (RI1260_MUST_BE_DISARMED);

    /* If word mode is selected, ensure port is even */
    if (data_length == RI1260_WORD && (port & 1))
        return (RI1260_WORD_ODD_PORT);

    /* Make sure that byte length data is within legal bounds */
    if (data_length == RI1260_BYTE)
        for(i = 0; i < num_data; i++)
            if (data_array[i] > 255)
                return (VI_ERROR_PARAMETER6);

    /* Initialize the port, specifying the mode to use */
    error = viPrintf (vi, "SE %d.WR,%d,%s\n", module, port,
                        data_mode[data_length]);
    if (error < 0) return (error);

    max_data_entries = (RI1260_MAX_SEND_LEN - 15) / 6;
    for (i = 0; i < num_data;) {
        if((num_data - i) >= max_data_entries)
            max_data_entries = num_data - i;
        error = viPrintf (vi, "SE %d.WR,%d,%,*d\n", module, port,
                            max_data_entries, &data_array[i]);
        if(error < 0) return (error);
        i += max_data_entries;
    }
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function arms or disarms the digital I/O card                      */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_arm_digio (ViSession vi, ViInt16 module,
    ViInt16 arm_or_disarm)
{
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (arm_or_disarm, RI1260_DISARM,RI1260_ARM))
        return (VI_ERROR_PARAMETER3);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Format and send the command string */
    return (viPrintf (vi, "SE %d.AR,%s\n", module, arm_disarm[arm_or_disarm]));
}

/*=========================================================================*/
/* This function reads the data that has been read by a synchronous port   */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_read_sync_data (ViSession vi, ViInt16 module,
    ViInt16 port, ViInt16 max_data, ViPInt16 num_read, ViAInt32 data_array)
{
ViInt16 io_bitmask;
ViInt16 last_sync_port;
ViInt16 arm_state;
ViInt16 clkin;
ViInt16 busy;
ViInt16 i;
ViStatus error;
ViChar *marker;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port, 0, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (max_data, 1, 256))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Read the set-up of the card */
    error = ri1260_read_digio_conf (vi, module, &io_bitmask,
                     &last_sync_port, &arm_state, &clkin, &busy);

    /* Ensure the port is synchronous */
    if (last_sync_port < port)
        return (RI1260_NOT_SYNC_PORT);

    /* Ensure the port is disarmed */
    if (arm_state == RI1260_ARM)
        return (RI1260_MUST_BE_DISARMED);


    /* Format and send a command to read the data from the port */
    error = viPrintf (vi, "PD %d.%d\n", module, port);
    if (error < 0)  {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    for(i = 0; rest_of_string[4] != 'E';) {
        error = viScanf (vi, "%t", rest_of_string);
        if (error < 0) {
            ri1260_ioCleanUp(vi);
            return (error);
        }

        marker = &rest_of_string[9];
        while((isdigit(*marker)) && (i < max_data)) {
            data_array[i++] = (ViUInt16) atoi(marker);
            while(isdigit(*(++marker)));
            if(*marker == ',') marker += 1;
        }
    }

    *num_read = i;
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function reads the data from a synchronous or asynchronous port    */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_async_read (ViSession vi, ViInt16 module, ViInt16 port,
    ViInt16 data_length, ViPInt32 data_item)
{
ViInt16 io_bitmask;
ViInt16 last_sync_port;
ViInt16 arm_state;
ViInt16 clkin;
ViInt16 busy;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port, 0, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (data_length, RI1260_BYTE, RI1260_WORD))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Read the set-up of the card */
    error = ri1260_read_digio_conf (vi, module, &io_bitmask,
                     &last_sync_port, &arm_state, &clkin, &busy);
    if (error < 0) return (error);

    /* If word mode is requested, ensure the port number is even */
    if (data_length == RI1260_WORD && (port & 1))
        return (RI1260_WORD_ODD_PORT);

    /* Format and send a command to read the data from the port */
    if (data_length == RI1260_WORD)
        error = viPrintf (vi, "READ %d.%d,W\n", module, port);
    else
        error = viPrintf (vi, "READ %d.%d\n", module, port);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* First response is 1260-14 Digital I/O identification string */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* Second response holds the message <slot>. <Port>: <Data> */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }
    *data_item = atoi (&rest_of_string[9]);

    /* Third response is <slot>.END */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function writes the data to a synchronous or asynchronous port     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_async_write (ViSession vi, ViInt16 module, ViInt16 port,
    ViInt16 data_length, ViInt32 data_item)
{
ViInt16 io_bitmask;
ViInt16 last_sync_port;
ViInt16 arm_state;
ViInt16 clkin;
ViInt16 busy;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port, 0, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (data_length, RI1260_BYTE, RI1260_WORD))
        return (VI_ERROR_PARAMETER4);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* Read the set-up of the card */
    error = ri1260_read_digio_conf (vi, module, &io_bitmask,
                     &last_sync_port, &arm_state, &clkin, &busy);
    if (error < 0) return (error);

    /* If word mode is requested, ensure the port number is even */
    if (data_length == RI1260_WORD && (port & 1))
        return (RI1260_WORD_ODD_PORT);

    if (data_length == RI1260_BYTE)
    {
        if(data_item > 255) return (VI_ERROR_PARAMETER5);
        return (viPrintf (vi, "WR %d.%d,%d\n", module, port, data_item));
    } else
        return (viPrintf (vi, "WR %d.%d,W,%d\n", module, port, data_item));
}

/*=========================================================================*/
/* This function writes bits to a synchronous or asynchronous port     */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_async_bit_write (ViSession vi, ViInt16 module,
    ViInt16 port, ViInt16 high_bits, ViInt16 low_bits)
{
ViInt16 both_bit_mask;
ViInt16 mask;
ViInt16 i;
ViStatus error;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (port, 0, RI1260_LAST_PORT))
        return (VI_ERROR_PARAMETER3);

    if (ri1260_invalidViInt16Range (high_bits, 0, 255))
        return (VI_ERROR_PARAMETER4);

    if (ri1260_invalidViInt16Range (low_bits, 0, 255))
        return (VI_ERROR_PARAMETER5);

    error = ri1260_check_card_type (vi, module, RI1260_14_MODULE);
    if (error < 0) return (error);

    /* first, eliminate the bits which are included in both bit masks */
    both_bit_mask = high_bits & low_bits;
    high_bits &= ~both_bit_mask;
    low_bits &= ~both_bit_mask;

    if ((high_bits == 0) && (low_bits == 0))
        return (VI_SUCCESS);

    error = viPrintf (vi, "WR %d.%d,X", module, port);
    if (error < 0) return (error);

    mask = 1;
    for (i = 0; i < 8; ++i) {
        if (high_bits & mask) {
            error = viPrintf (vi, ",H%d", i);
            if (error < 0) return (error);
        }
        mask <<= 1;
    }

    mask = 1;
    for (i = 0; i < 8; ++i) {
        if (low_bits & mask) {
            error = viPrintf (vi, ",L%d", i);
            if (error < 0) return (error);
        }
        mask <<= 1;
    }

    return (viPrintf (vi, "\n"));
}

/*=========================================================================*/
/* Function: Run Synchronous Test                                          */
/* Purpose:  This function sets up and executes a synchronous test on a    */
/*           1260-14/14C card.  It assumes that the synchronous test has   */
/*           already been setup and is waiting to be armed.                */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_run_synchronous_test (ViSession vi, ViInt16 module,
    ViInt16 test_timeout)
{
time_t start_time;
time_t current_time;
ViStatus error;
ViInt16 io_bitmask;
ViInt16 last_sync_port;
ViInt16 arm_state;
ViInt16 clkin;
ViInt16 busy;

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if(current_time = time (&start_time) == -1)
        return (RI1260_NSUP_TIME);

    error = ri1260_arm_digio (vi, module, RI1260_ARM);
    if (error < 0) return (error);

    do {
        ri1260_delay (1.0);
        error = ri1260_read_digio_conf (vi, module, &io_bitmask,
            &last_sync_port, &arm_state, &clkin, &busy);
        if (error < 0) return (error);
        time (&current_time);
    } while((difftime (current_time, start_time) < test_timeout) &&
            (arm_state == RI1260_ARM));

    if(arm_state == RI1260_ARM) {
        error = ri1260_arm_digio(vi, module, RI1260_DISARM);
        if (error < 0) return (error);
        error = RI1260_DIGIO_SYNC_TIMEOUT;
    } else error = VI_SUCCESS;

    return (error);
}


/*=========================================================================*/
/* Function: Configure 1260-18 Module                                      */
/* Purpose:  This function configures the 1260-18 card as one of the       */
/*           following configurations:  152 independent relays, 76 pairs   */
/*           of relays, 50 groups of 3 relays, or 38 groups of 4 relays    */
/*           NOTE:  This function erases all existing include groups       */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_configure_1260_18 (ViSession vi, 
                                            ViInt16 module,
                                            ViInt16 configuration)
{
ViStatus error;
ViInt16 i, relay;
ViInt16 relay_list[4];

    if (ri1260_invalidViInt16Range (module, 1, RI1260_MAX_CARDS))
        return (VI_ERROR_PARAMETER2);

    if (ri1260_invalidViInt16Range (configuration, 1, 4))
        return (VI_ERROR_PARAMETER3);


    error = ri1260_check_card_type (vi, module, RI1260_18_MODULE);
    if (error < 0) return (error);

	/* clear all existing include lists */
	error = ri1260_def_incl_list(vi, module, relay_list, 0);
	if (error < 0)
		return( error );
    
    /* form include groups */
    switch( configuration )
    	{
    	case 1:		/* 152 independent relays */
    		/* don't do anything, since no include groups = independent relays */
    		break;

		case 2:		/* 76 pairs of relays */
		case 3:		/* 50 groups of 3 relays */
		case 4:		/* 38 groups of 4 relays */
			for (i = 0;  i < 152;  i += configuration)
				{
				for (relay = 0;  relay < configuration;  ++relay)
					relay_list[relay] = i + relay;
					
				error = ri1260_def_incl_list(vi, module, relay_list, configuration);
				if (error < 0)
					return( error );
				}
		}


	return( VI_SUCCESS );
}

/*=========================================================================*/
/* Function: Self-Test                                                     */
/* Purpose:  This function executes the instrument self-test and returns   */
/* the result.                                                             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_self_test (ViSession vi, ViPInt16 test_result,
    ViString test_message)
{
ViStatus error;
ViUInt32 old_timeout;
ViInt32 error_status;
ViInt16 test_num;
ViUInt16 status;

    error = viGetAttribute (vi, VI_ATTR_TMO_VALUE, &old_timeout);
    if (error < 0) return (error);

    error = viSetAttribute (vi, VI_ATTR_TMO_VALUE, (ViUInt32)30000);
    if  (error != VI_SUCCESS) {
        if (error > 0)
            error = RI1260_NSUP_ATTR_VALUE;
        return (error);
    }

    /* perform each self-test until all pass or one fails */
    *test_result = 0;
    *test_message = '\0';
    for (test_num = 1;  (test_num < 4) && (*test_result == 0);  ++test_num) {
        error = viPrintf (vi, "TE 0.%d\n", test_num);
        if (error < 0) {
            ri1260_ioCleanUp (vi);
            viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);
            return (error);
        }

    /* read the result (will be 7F if passed, or will time out if failed) */
        error = viScanf (vi, "%t", rest_of_string);
        if (error < 0) {
            if (error == VI_ERROR_TMO) {
                error = viReadSTB(vi, &status);
                if (error < 0) {
                    viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);
                    return (error);
                }
                error = ri1260_error_query(vi, &error_status, rest_of_string);
                if (error < 0) {
                    viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);
                    return (error);
                }
            } else {
                viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);
                return (error);
            }
        }

        if (strncmp(rest_of_string,"7F",2) != 0) {
            *test_result = 1;
            switch( test_num ) {
                case 1: strcpy(test_message, "RAM test failed");break;
                case 2: strcpy(test_message, "ROM checksum test failed");break;
                case 3: strcpy(test_message, "Non-vol memory test failed");break;
            }
        }
    }

    /* restore old timeout value */
    viSetAttribute (vi, VI_ATTR_TMO_VALUE, old_timeout);
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Function: Error Query                                                   */
/* Purpose:  This function queries the instrument error queue.             */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_error_query (ViSession vi, ViPInt32 error_status,
    ViString error_message)
{
ViStatus error;

    error = viPrintf (vi, "YERR\n");
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }
    error = viScanf (vi, "%t", error_message);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }


    /* test the error message to see if an error has occurred */
    if (strncmp (error_message, "error 000.00", 12) != 0)
        *error_status = VI_TRUE;
    else
        *error_status = VI_FALSE;

    return (VI_SUCCESS);
}
/*=========================================================================*/
/* Function: Revision                                                      */
/* Purpose:  This function returns the driver and instrument revisions.    */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_revision_query (ViSession vi, ViString driver_rev,
    ViString instr_rev)
{
ViStatus error;
ViChar *marker;

    /* write the "PD 0" command to get the 3-line response about option 01 */
    error = viPrintf (vi, "PD 0\n");
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* read/skip the first line */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    /* second line contains the revision information */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }
    /* format of response is '000. OS REV <revision> 1260' */
    marker = &rest_of_string[12];
    while((*instr_rev++ = *marker++) != ' ');
    *instr_rev = '\0';

    /* third line contains the '000.END' string */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0) {
        ri1260_ioCleanUp(vi);
        return (error);
    }

    strcpy(driver_rev, RI1260_REVISION);
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Function: Error Message                                                 */
/* Purpose:  This function returns a text message for a corresponding      */
/* instrument driver error code.                                           */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_error_message (ViSession vi, ViStatus error_code,
    ViString message)
{
    switch( error_code ) {
    case VI_SUCCESS:
        strcpy(message, "No error for ri1260");
        break;

    case VI_ERROR_FAIL_ID_QUERY:
        strcpy(message, "Instrument identification query failed for ri1260");
        break;

    case VI_ERROR_INV_RESPONSE:
        strcpy(message, "Error interpreting instrument response for ri1260");
        break;

    case VI_ERROR_PARAMETER1:
        strcpy(message, "First parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER2:
        strcpy(message, "Second parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER3:
        strcpy(message, "Third parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER4:
        strcpy(message, "Fourth parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER5:
        strcpy(message, "Fifth parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER6:
        strcpy(message, "Sixth parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER7:
        strcpy(message, "Seventh parameter is out of range for ri1260");
        break;

    case VI_ERROR_PARAMETER8:
        strcpy(message, "Eighth parameter is out of range for ri1260");
        break;

    case RI1260_MAX_INSTR_ERROR:
        strcpy(message, "There are too many sessions open for ri1260");
        break;

    case RI1260_WRONG_CARD_TYPE:
        strcpy(message, "An operation was attempted on the wrong card type for ri1260");
        break;

    case RI1260_SCAN_LIST_TOO_LONG:
        strcpy(message, "The scan list is too long for ri1260");
        break;

    case RI1260_BAD_SCAN_LIST_ITEM:
        strcpy(message, "The scan list is incorrect for ri1260");
        break;

    case RI1260_EXCL_LIST_TOO_LONG:
        strcpy(message, "The exclusion list is too long for ri1260");
        break;

    case RI1260_BAD_EXCL_LIST_ITEM:
        strcpy(message, "The exclusion list is incorrect for ri1260");
        break;

    case RI1260_EQUATE_LIST_TOO_LONG:
        strcpy(message, "The equate list is too long for ri1260");
        break;

    case RI1260_INCL_LIST_TOO_LONG:
        strcpy(message, "The inclusion list (INCL) is too long for ri1260");
        break;

	case RI1260_INCL_RELAY_TWICE:
		strcpy(message, "The inclusion list (INCL) contains the same relay more than once");
		break;

    case RI1260_BAD_PSETUP_RESPONSE:
        strcpy(message, "The switch controller returned invalid PSETUP response for ri1260");
        break;

    case RI1260_NOT_SYNC_PORT:
        strcpy(message, "The port being used must be placed in synchronous mode for ri1260");
        break;

    case RI1260_WORD_ODD_PORT:
        strcpy(message, "Word sized operations must be performed on even numbered ports for ri1260");
        break;

    case RI1260_MUST_BE_DISARMED:
        strcpy(message, "The port must be disarmed before doing this fuction for ri1260");
        break;

    case RI1260_NSUP_ATTR_VALUE:
        strcpy(message, "A call to viSetAttribute returned an unsupported value warning for ri1260");
        break;

    case RI1260_DIGIO_SYNC_TIMEOUT:
        strcpy(message, "The synchronous digital I/O test has exceeded the test timeout value for ri1260");
        break;

    case RI1260_NSUP_TIME:
        strcpy(message, "The ri1260_run_synchronous_test function cannot execute due to lack of support from time library for ri1260");
        break;

    default:
        if(viStatusDesc(vi, error_code, message) != VI_SUCCESS)
        {
            strcpy(message, "Unknown error for ri1260");
                    return (VI_WARN_UNKNOWN_STATUS);
        }
        break;
    }

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Function: Reset                                                         */
/* Purpose:  This function resets the instrument.                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_reset (ViSession vi)
{
ViStatus error;

    error = viClear(vi);
    if(error < 0)
        return(error);
    else
        return (ri1260_delay(3.0));
}

/*=========================================================================*/
/* Function: Close                                                         */
/* Purpose:  This function closes the instrument.                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_close (ViSession vi)
{
ViUInt32 index;
ViStatus error;

/* Close the resource and instrument sessions and reset the "initialized" */
/* array  for instrument */
    error = viGetAttribute(vi, VI_ATTR_USER_DATA, &index );
    if (error == VI_SUCCESS) {
    error = (viClose (vi));
        viClose (initialized[index]);
    initialized[index] = 0;
    }

    return (error);
}

/*=========================================================================*/
/* Function: Find1260                                                      */
/* Purpose:  This function searches for 1260 controllers and returns a     */
/*           count of the number found, an array of interface types, an    */
/*           array of interface numbers, an array of logical addresses     */
/*           and an array of slot numbers. From this information, the      */
/*           user will be able to build an instrument descriptor for the   */
/*           1260 he wishes to communicate with.                           */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_find1260 (ViInt16 max_count, ViPInt32 ret_count,
    ViAInt16 intf_type, ViAInt16 intf_num, ViAInt16 logical_address,
    ViAInt16 slot)
{
ViStatus error;
ViSession rmSession;
ViSession vi;
ViChar rname[256];
ViFindList find_list;
ViUInt16 manf_ID;
ViUInt16 model_code;
ViUInt32 rcount;
ViUInt32 i;

/* Initialize entry in Instrument Table and interface for instrument. */
    *ret_count = 0;
    error = viOpenDefaultRM (&rmSession);
    if (error < 0) return (error);

    error = viFindRsrc (rmSession, "?*INSTR", &find_list, &rcount, rname);
    if (error < 0)
        if(error != VI_ERROR_RSRC_NFOUND) {
            viClose (rmSession);
            return (error);
        } else rcount = 0;

/* Check each instrument to see if it is a 1260 */
    for (i=0; (i < rcount) && (*ret_count < max_count); i++) {

        error = viOpen (rmSession, rname, VI_NULL, VI_NULL, &vi);
        if (error < 0) {
            viClose(find_list);
            viClose(rmSession);
            return (error);
        }

/* Read the manufacturer ID */
        error = viGetAttribute (vi, VI_ATTR_MANF_ID, &manf_ID);
        if (error < 0) {
            viClose(vi);
            viClose(find_list);
            viClose(rmSession);
            return (error);
        }

/* Read the model code */
        error = viGetAttribute (vi, VI_ATTR_MODEL_CODE, &model_code);
        if (error < 0) {
            viClose(vi);
            viClose(find_list);
            viClose(rmSession);
            return (error);
        }

/* check for proper model code and manufacter ID */
        if ((model_code == RI1260_MODEL_CODE) &&
            (manf_ID == RI1260_MANF_ID)) {

/* store the interface type */
            error = viGetAttribute (vi, VI_ATTR_INTF_TYPE, (ViAInt16) intf_type++);
            if (error < 0) {
                viClose(vi);
                viClose(find_list);
                viClose(rmSession);
                return (error);
            }

/* store the interface number */
            error = viGetAttribute (vi, VI_ATTR_INTF_NUM, (ViAInt16) intf_num++);
            if (error < 0) {
                viClose(vi);
                viClose(find_list);
                viClose(rmSession);
                return (error);
            }

/* store the logical address */
            error = viGetAttribute (vi, VI_ATTR_VXI_LA, logical_address++);
            if (error < 0) {
                viClose(vi);
                viClose(find_list);
                viClose(rmSession);
                return (error);
            }

/* store the slot number */
            error = viGetAttribute (vi, VI_ATTR_SLOT, slot++);
            if (error < 0) {
                viClose(vi);
                viClose(find_list);
                viClose(rmSession);
                return (error);
            }

/* Increment the return count of 1260's found */
            *ret_count += 1;
        }

/* Close this instrument and get the next one if applicable */
        error = viClose (vi);
        if (error < 0) {
            viClose(find_list);
            viClose(rmSession);
            return (error);
        }

        if (((i+1) < rcount) && (*ret_count < max_count)) {
            error = viFindNext(find_list, rname);
            if (error < 0) {
                viClose(find_list);
                viClose(rmSession);
                return (error);
            }
        }
    }

    if(rcount > 0) {
        error = viClose (find_list);
        if (error < 0) {
            viClose (rmSession);
            return (error);
        }
    }
    return (viClose (rmSession));
}


/*=========================================================================*/
/* Function: Close All Sessions                                            */
/* Purpose:  This function closes all resource manager sessions.           */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_closeAllRMSessions (void)
{
ViStatus rval=VI_SUCCESS;
ViStatus error;
ViInt16 i;

/* Close each resource session and reset the "initialized" array */
    for(i=0; i < RI1260_MAX_INSTR;i++)
        if (initialized[i] != RI1260_NO_SESSION) {
            if ((error = viClose (initialized[i])) < 0)
                rval = error;
            initialized[i] = RI1260_NO_SESSION;
        }
    return (error);
}

/*====== UTILITY ROUTINES =================================================*/
/*=========================================================================*/
/* Function: Invalid Boolean Range                                         */
/* Purpose:  This function checks a boolian to see if it lies between a    */
/*           minimum and maximum value.  If the value is out of range, set */
/*           the return value to VI_FALSE.  If the value is OK, set the    */
/*           return value to VI_TRUE.                                      */
/*=========================================================================*/
ViBoolean _VI_FUNC ri1260_invalidViBooleanRange (ViBoolean val)
{
    return (val < VI_FALSE || val > VI_TRUE);
}

/*=========================================================================*/
/* Function: Invalid 16 bit Integer Range                                  */
/* Purpose:  This function checks an integer to see if it lies between     */
/*           a minimum and maximum value.  If the value is out of range,   */
/*           set the global error variable to the value err_code.  If the  */
/*           value is OK, error = 0.  The return value is equal to the     */
/*           global error value.                                           */
/*=========================================================================*/
ViBoolean _VI_FUNC ri1260_invalidViInt16Range (ViInt16 val, ViInt16 min, ViInt16 max)
{
    return (val < min || val > max);
}

/*=========================================================================*/
/* Function: Initialize Clean Up                                           */
/* Purpose:  This function is used only by the ri1260_init function.  When */
/*           an error is detected this function is called to close the     */
/*           open resource manager and instrument object sessions and to   */
/*           set the instrSession that is returned from ri1260_init to     */
/*           VI_NULL.                                                      */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_initCleanUp (ViSession rmSession, ViPSession vi,
    ViUInt32 index, ViStatus currentStatus)
{
    viClose (*vi);
    viClose (rmSession);
    *vi = VI_NULL;
        initialized[index] = 0;
    return (currentStatus);
}

/*=========================================================================*/
/* Function: I/O Failure Clean Up                                          */
/* Purpose:  This function is used when calls to viScanf fail, leaving the */
/*           1260 in an unknown state.  This routine determines if the     */
/*           1260 has it's ERR* bit set true and if it does, attempts to   */
/*           clear the error by sending a Read Protocol Error command.     */
/*           It then checks the DOR bit and if it is True, calls viScanf   */
/*           repeatedly to flush out the 1260's output buffer.             */
/*=========================================================================*/
void _VI_FUNC ri1260_ioCleanUp (ViSession vi)
{
#define RI1260_WR_RDY (ViUInt16) 0x0200
#define RI1260_RD_RDY (ViUInt16) 0x0400
#define RI1260_ERRN (ViUInt16) 0x0800
#define RI1260_DOR (ViUInt16) 0x2000
#define RI1260_RESP_REG (ViBusAddress) 0x0A
#define RI1260_DL_REG (ViBusAddress) 0x0E
#define RI1260_READ_PROT_ERR (ViUInt16) 0xCDFF

ViUInt16 resp_reg;
ViUInt16 dl_reg;
ViInt16 i;

/* Get the response register value and see if the 1260 has an error */
    if (viIn16 (vi, VI_A16_SPACE, RI1260_RESP_REG, &resp_reg) < 0)
        return;

/* If there is an error, wait until the Write Ready bit is true, then send  */
/* a Read Protocol Error command to clear the error. If the Write Ready bit */
/* does not go true after 1,000 tries, the interface is locked up and there */
/* is nothing we can do but return. */
    if ((resp_reg & RI1260_ERRN) == 0) {
        i = 0;
        while (((resp_reg & RI1260_WR_RDY) == 0) && (i++ < 1000))
            if (viIn16 (vi, VI_A16_SPACE, RI1260_RESP_REG, &resp_reg) < 0)
                return;
        if (i >= 1000) return;
        if (viOut16 (vi, VI_A16_SPACE, RI1260_DL_REG,
                        RI1260_READ_PROT_ERR) < 0) return;

/* Now read and discard the results of the Read Protocol error command.    */
/* If the Read Ready does not go true within 1000 tries, we are locked up. */
        if (viIn16 (vi,VI_A16_SPACE,RI1260_RESP_REG,&resp_reg)<0)
            return;

        while (((resp_reg & RI1260_RD_RDY) == 0) && (i++ < 1000))
            if (viIn16 (vi, VI_A16_SPACE, RI1260_RESP_REG, &resp_reg) < 0)
                return;
        if (i >= 1000) return;
        if (viIn16 (vi, VI_A16_SPACE, RI1260_DL_REG, &dl_reg) < 0)
            return;
    }

/* Check to see if the DOR bit is set and read all the data from the */
/* instrument to flush the output buffer if it is. */
    while ((resp_reg & RI1260_DOR) != 0) {
        if (viScanf(vi, "%t", rest_of_string) < 0)
            return;
        if (viIn16 (vi, VI_A16_SPACE, RI1260_RESP_REG, &resp_reg) < 0)
            return;
    }
}

/*=========================================================================*/
/* Function: Delay                                                         */
/* Purpose:  Delays by the specified period of time in seconds.            */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_delay (ViReal64 delay)
{
time_t start_time;
time_t current_time;

    if((current_time = time (&start_time)) == -1)
        return (RI1260_NSUP_TIME);

    while(difftime (current_time, start_time) < delay)
        if (time (&current_time) == -1)
            return (RI1260_NSUP_TIME);

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Function: Wait for Ready                                                */
/* Purpose:  Waits up to num_seconds for the Ready bit to be set           */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_wait_ready (ViSession vi, ViInt16 num_seconds)
{
	ViStatus error;
	ViUInt16 stat_byte;
	
	/* multiply by 16 so that we can check every 62.5 msec */
	num_seconds <<= 4;
	
	while (num_seconds > 0)
		{
		error = viReadSTB (vi, &stat_byte);
		if (error < 0) return (error);

    	if (stat_byte & READY_MASK)
    		return(VI_SUCCESS);
    		
    	(void) ri1260_delay(0.0625);
    	
    	--num_seconds;
    	}

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* Function: itoa                                                          */
/* Purpose:  converts a (positive) numeric to a string                     */
/*=========================================================================*/
ViString _VI_FUNC ri1260_itoa(ViInt16 number)
{
	static ViChar buffer[10];
	int index;
	int quotient, divisor;
	
	divisor = 10000;
	quotient = 0;
	
	while ((quotient == 0) && (divisor != 0))
		{
		quotient = number / divisor;
		number -= (quotient * divisor);
		divisor /= 10;
		}
	
	buffer[0] = '0' + quotient;
	
	index = 1;
	while (divisor > 0)
		{
		quotient = number / divisor;
		number -= (quotient * divisor);
		buffer[index++] = '0' + quotient;
		divisor /= 10;
		}
		
	buffer[index] = 0;
	
	return( buffer );
}

	

/*=========================================================================*/
/* This function scans the response of the instrument to  determine the    */
/* type of card is located within the switch controller module             */
/*=========================================================================*/
/*=========================================================================*/
/* Function: Get Card Type                                                 */
/* Purpose:  This function gets the card type from the response string     */
/*           and places the code for the card in the global table named    */
/*           mod_type[].  This table is used to verify that the correct    */
/*           type of card is being commanded.                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_get_card_type (ViSession vi, ViString response,
    ViPInt16 mod_type)
{
ViInt16 card_num, module;
ViChar card_type;
ViChar *marker;


    card_num = atoi(response);
    if (card_num < 1 || card_num > RI1260_MAX_CARDS)
        return (VI_ERROR_INV_RESPONSE);
    mod_type += (card_num - 1);
    *mod_type = RI1260_NO_MODULE;

    marker = strchr(response, '-');
    marker += 1;
    module = atoi(marker);
    while(isdigit(*(++marker)));
    while(!isalpha(*marker)) marker += 1;
    card_type = *marker;

    switch (module) {
    case 12:        *mod_type = RI1260_12_MODULE;       break;
    case 13:        *mod_type = RI1260_13_MODULE;       break;
    case 14:        *mod_type = RI1260_14_MODULE;       break;
    case 16:        *mod_type = RI1260_16_MODULE;       break;
    case 17:        *mod_type = RI1260_17_MODULE;       break;
    case 18:        *mod_type = RI1260_18_MODULE;       break;
    case 20:        *mod_type = RI1260_20_MODULE;       break;
    case 30:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_30A_MODULE;      break;
        case 'B':   *mod_type = RI1260_30B_MODULE;      break;
        case 'C':   *mod_type = RI1260_30C_MODULE;      break;
        case 'D':   *mod_type = RI1260_30D_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 35:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_35A_MODULE;      break;
        case 'B':   *mod_type = RI1260_35B_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 36:        *mod_type = RI1260_36_MODULE;       break;

    case 37:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_37A_MODULE;      break;
        case 'B':   *mod_type = RI1260_37B_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 38:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_38A_MODULE;      break;
        case 'B':   *mod_type = RI1260_38B_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 39:        
    	if (card_type == 'S')
    		*mod_type = RI1260_39S_MODULE;
    	else
    		*mod_type = RI1260_39_MODULE;
    	break;
    

    case 40:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_40A_MODULE;      break;
        case 'B':   *mod_type = RI1260_40B_MODULE;      break;
        case 'C':   *mod_type = RI1260_40C_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 45:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_45A_MODULE;      break;
        case 'B':   *mod_type = RI1260_45B_MODULE;      break;
        case 'C':   *mod_type = RI1260_45C_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 50:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_50A_MODULE;      break;
        case 'B':   *mod_type = RI1260_50B_MODULE;      break;
        case 'C':   *mod_type = RI1260_50C_MODULE;      break;
        case 'D':   *mod_type = RI1260_50D_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 54:    *mod_type = RI1260_54_MODULE;       break;
    
    case 58:	*mod_type = RI1260_58_MODULE;		break;

    case 59:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_59A_MODULE;      break;
        case 'B':   *mod_type = RI1260_59B_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 60:    *mod_type = RI1260_60_MODULE;       break;

    case 64:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_64A_MODULE;      break;
        case 'B':   *mod_type = RI1260_64B_MODULE;      break;
        case 'C':   *mod_type = RI1260_64C_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 66:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_66A_MODULE;      break;
        case 'B':   *mod_type = RI1260_66B_MODULE;      break;
        case 'C':   *mod_type = RI1260_66C_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 75:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_75A_MODULE;      break;
        case 'B':   *mod_type = RI1260_75B_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    case 93:
        switch (card_type) {
        case 'A':   *mod_type = RI1260_93A_MODULE;      break;
        case 'B':   *mod_type = RI1260_93B_MODULE;      break;
        default:    *mod_type = RI1260_UNKNOWN_MODULE;  break;
        }
        break;

    default:    *mod_type = RI1260_UNKNOWN_MODULE;          break;
    }

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function scans the PDATAOUT response and reads the state of relays */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_check_card_type (ViSession vi, ViInt16 module,
    ViInt16 should_be_type)
{
ViStatus error;
ViInt32 index;

    error = viGetAttribute (vi, VI_ATTR_USER_DATA, &index);
    if (error < 0) return (error);

    if (module_type[index][module-1] != should_be_type)
    return (RI1260_WRONG_CARD_TYPE);

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function scans the PDATAOUT response and reads the state of relays */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_scan_states (ViInt16 mod_type, ViAInt16 states,
    ViString marker)
{
ViInt16 start_index, end_index;
ViInt16 i;

    /* Repeatedly read the closed relays until the end of the response */

    while((!isdigit(*marker)) && (*marker != '\0')) marker += 1;

    while(isdigit(*marker)) {
        start_index = ri1260_convert_relay_to_index (atoi(marker), mod_type);

        if (start_index == -1) return (RI1260_WRONG_CARD_TYPE);

        while(isdigit(*(++marker)));

        /* check for range or individual relay */
        if (*marker == '-') {

    /* range of relays */
            end_index = ri1260_convert_relay_to_index (atoi(++marker), mod_type);

            if (end_index == -1) return (RI1260_WRONG_CARD_TYPE);

            while(isdigit(*(++marker)));

            for (i = start_index; i <= end_index; ++i)
                states[i] = RI1260_CLOSE_RELAY;
        } else
            states[start_index] = RI1260_CLOSE_RELAY;

        if(*marker == ',') marker += 1;
        while (*marker == ' ') marker++;
    }

    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function converts a relay of the module type to an index into a    */
/* one dimensional array.  This function returns -1 if the module type is  */
/* not supported                                                           */
/*=========================================================================*/
ViInt16 _VI_FUNC ri1260_convert_relay_to_index (ViInt16 relay, ViInt16 mod_type)
{
ViInt16 index;
ViInt16 group;
ViInt16 matrix;
ViInt16 row;
ViInt16 column;
ViInt16 interconnect;
ViInt16 mux;
ViInt16 channel;

    switch (mod_type) {
    case RI1260_12_MODULE:
    case RI1260_13_MODULE:
    case RI1260_16_MODULE:
    case RI1260_17_MODULE:
    case RI1260_18_MODULE:
    case RI1260_20_MODULE:
    case RI1260_35A_MODULE:
    case RI1260_35B_MODULE:     index = relay;      break;

    case RI1260_30A_MODULE:     index = relay;      break;

    case RI1260_30B_MODULE:     group = relay / 100;
                    relay %= 100;
                    index = group * 20 + relay;
                    break;

    case RI1260_30C_MODULE:     group = relay / 100;
                    relay %= 100;
                    index = group * 10 + relay;
                    break;

    case RI1260_30D_MODULE:     group = relay / 100;
                    relay %= 100;
                    index = group * 5 + relay;
                    break;

    case RI1260_36_MODULE:      group = relay / 10;
                    relay %= 10;
                    index = (group * 8) + relay;
                    break;

    case RI1260_37A_MODULE:     if(relay < 100)
                                    index = relay;
                                else
                    index = relay - 76;
                    break;

    case RI1260_37B_MODULE:     if(relay < 100)
                                    index = relay;
                                else
                    index = relay - 51;
                    break;

    case RI1260_38A_MODULE:     interconnect = (relay / 1000);
                    mux = (relay / 10) % 100;
                    channel = relay % 10;
                    if(interconnect == 0)
                    	index = (mux << 3) + channel;
                    else if(interconnect < 8)
                    	index = 128 + ((interconnect-1) * 3) + channel;
                    else if(interconnect == 8)
                    	index = 149;
                    else	
                    	index = 150 + channel;
                    break;

    case RI1260_38B_MODULE:     interconnect = (relay / 1000);
                    mux = (relay / 10) % 100;
                    channel = relay % 10;
                    if(interconnect == 0)
                    	index = (mux << 2) + channel;
                    else if(interconnect < 8)
                    	index = 64 + (interconnect-1);
                    else	
                    	index = 71 + channel;
                    break;

	case RI1260_39_MODULE:
					/* first get the 1st 2 digits, base index on it */
					channel = (relay / 100) * 100;
					relay %= 100;
					row = relay / 10;
					column = relay % 10;
					index = -1;
					
					switch( channel )
						{
						case RI1260_39_DPST:			index = 0;		break;
						case RI1260_39_SPST:			index = 5;		break;
						case RI1260_39_1X2_MUX_1:		index = 53;		break;
						case RI1260_39_1X2_MUX_2:		index = 55;		break;
						case RI1260_39_1X2_MUX_3:		index = 57;		break;
						case RI1260_39_1X2_MUX_4:		index = 59;		break;
						case RI1260_39_1X2_MUX_5:		index = 61;		break;
						case RI1260_39_1X2_MUX_6:		index = 63;		break;
						case RI1260_39_1X4_MUX_1:		index = 65;		break;
						case RI1260_39_1X4_MUX_2:		index = 69;		break;
						case RI1260_39_1X4_MUX_3:		index = 73;		break;
						case RI1260_39_2X8_MATRIX_1:	index = 77; 	
														relay = row * 8 + column;
														break;
						case RI1260_39_2X8_MATRIX_2:	index = 93;
														relay = row * 8 + column;
														break;
						case RI1260_39_2X8_MATRIX_3:	index = 109;
														relay = row * 8 + column;
														break;
						case RI1260_39_2X8_MATRIX_4:	index = 125;
														relay = row * 8 + column;
														break;
						case RI1260_39_2X8_MATRIX_5:	index = 141;
														relay = row * 8 + column;
														break;
						}
						
					index += relay;
					break;
						
	case RI1260_39S_MODULE:
					mux = relay / 10;
					relay %= 10;
					index = mux * 8 + relay;
					break;
	
    case RI1260_40A_MODULE:     row = (relay / 100) % 10;
                    column = relay % 100;
                    index = row * 24 + column;
                    break;

    case RI1260_40B_MODULE:     row = (relay / 100) % 10;
                    column = relay % 100;
                    index = row * 12 + column;
                    break;

    case RI1260_40C_MODULE:     matrix = relay / 1000;
                    row = (relay / 100) % 10;
                    column = relay % 100;
                    index = matrix * 48 + row * 12 + column;
                    break;

      case RI1260_45A_MODULE:
      case RI1260_45B_MODULE:
      case RI1260_45C_MODULE:       matrix = relay / 1000;
                    row = (relay / 100) % 10;
                    column = relay % 100;
                    index = matrix * 64 + row * 16 + column;
                    break;

      case RI1260_50A_MODULE:
      case RI1260_50B_MODULE:
      case RI1260_50C_MODULE:
      case RI1260_50D_MODULE:
      case RI1260_75A_MODULE:
      case RI1260_75B_MODULE:
      case RI1260_93A_MODULE:
      case RI1260_93B_MODULE:       group = relay / 10;
                    relay = relay % 10;
                    index = group * 6;
                    /*
                     * relays connecting groups together
                     * use relay # of 9
                     */
                    if (relay == 9)
                    index += 5;
                    else
                     index += relay;
                    break;

      case RI1260_54_MODULE:      group = relay / 10;
                    relay = relay % 10;
                    index = group * 4 + relay;
                    break;

      case RI1260_58_MODULE:
      				group = relay / 10;
      				relay %= 10;
      				index = group * 8 + relay;
      				break;
      				
      case RI1260_59A_MODULE:
      case RI1260_59B_MODULE:
					group = relay / 10;
					relay %= 10;
                    index = group * 4 + relay;
					break;

	  case RI1260_60_MODULE:        group = relay / 100;
                    relay %= 100;
                    index = group * 12 + relay;
                    break;

      case RI1260_64A_MODULE:
      case RI1260_64B_MODULE:
      case RI1260_64C_MODULE:       group = relay / 100;
                    if (group < 2) {
                        relay %= 100;
                        index = group * 16 + relay;
                    } else {
                        relay %= 100;
                        index = (group - 2) * 6 + relay + 32;
                    }
                    break;

      case RI1260_66A_MODULE:
      case RI1260_66B_MODULE:
	  case RI1260_66C_MODULE:
					group = relay / 10;
					relay %= 10;
                    index = group * 6 + relay;
					break;


    default:        index = -1;
                    break;
    }

    return (index);
}

/*=========================================================================*/
/* This function reads the switch sequence.  It returns one of the         */
/* constants RI1260_BBM, RI1260_MBB, RI1260_IMM for normal returns, or -1  */
/* when an error has occurred                                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_read_sequence (ViSession vi, ViInt16 module,
    ViPInt16 sequence_ptr)
{
ViStatus error;

    error = viPrintf(vi, "PS %d\n", module);
    if (error < 0) {
        ri1260_ioCleanUp (vi);
        return (error);
    }

    /* First string identifies the module type */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0)  {
        ri1260_ioCleanUp (vi);
        return (error);
    }

    /* Second string indicates the mode */
    error = viScanf (vi, "%t",rest_of_string);
    if (error < 0)  {
        ri1260_ioCleanUp (vi);
        return (error);
    }

    *sequence_ptr = -1;

    switch (rest_of_string[5]) {
    case 'I':
        if (rest_of_string[6] == 'M' && rest_of_string[7] == 'M')
            *sequence_ptr = RI1260_IMM;
        break;

    case 'M':
        if (rest_of_string[6] == 'B' && rest_of_string[7] == 'B')
            *sequence_ptr = RI1260_MBB;
        break;

    case 'B':
        if (rest_of_string[6] == 'B' && rest_of_string[7] == 'M')
            *sequence_ptr = RI1260_BBM;
        break;

    default:
        break;
    }

    /* Response was no good */
    if (*sequence_ptr == -1) {
        ri1260_ioCleanUp (vi);
        return (RI1260_BAD_PSETUP_RESPONSE);
    }

    /* read the "END" string */
    error = viScanf (vi, "%t", rest_of_string);
    if (error < 0)  {
        ri1260_ioCleanUp (vi);
        return (error);
    } else return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function produces a command to open or close a sequence of relays  */
/* it returns the length of the command string formed, or 0 if no relays   */
/* are to be opened (closed).                                              */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_build_list (ViSession vi, ViInt16 operation,
    ViInt16 module_addr, ViInt16 mod_type, ViAInt16 states)
{
ViStatus error;
ViInt16 index, max_index, first_index, last_index, any_changed, cmd_len;

    switch (mod_type) {
    case RI1260_12_MODULE:  max_index = 19;     break;

    case RI1260_13_MODULE:  max_index = 39;     break;

    case RI1260_16_MODULE:  max_index = 39;     break;

    case RI1260_17_MODULE:  max_index = 79;     break;
    
    case RI1260_18_MODULE:	max_index = 159;	break;

    case RI1260_20_MODULE:  max_index = 19;     break;

    case RI1260_30A_MODULE:
    case RI1260_30B_MODULE:
    case RI1260_30C_MODULE:

    case RI1260_30D_MODULE: max_index = 39;     break;

    case RI1260_35A_MODULE: max_index = 47;     break;
    case RI1260_35B_MODULE: max_index = 96;     break;

    case RI1260_36_MODULE:  max_index = 95;     break;

    case RI1260_37A_MODULE: max_index = 63;     break;
    case RI1260_37B_MODULE: max_index = 88;     break;

    case RI1260_38A_MODULE: max_index = 157;    break;
    case RI1260_38B_MODULE: max_index = 79;     break;
    
    case RI1260_39_MODULE:	max_index = 156;	break;
    case RI1260_39S_MODULE:	max_index =  31;	break;

    case RI1260_40A_MODULE:
    case RI1260_40B_MODULE:
    case RI1260_40C_MODULE: max_index = 95;     break;

    case RI1260_45A_MODULE:
    case RI1260_45B_MODULE:
    case RI1260_45C_MODULE: max_index = 255;    break;

    case RI1260_50A_MODULE: max_index = 47;     break;
    case RI1260_50B_MODULE: max_index = 95;     break;
    case RI1260_50C_MODULE: max_index = 47;     break;
    case RI1260_50D_MODULE: max_index = 95;     break;

    case RI1260_54_MODULE:  max_index = 23;     break;
    
    case RI1260_58_MODULE:	max_index = 31;		break;
    
    case RI1260_59A_MODULE:	max_index = 15;		break;
    case RI1260_59B_MODULE:	max_index = 31;		break;

    case RI1260_60_MODULE:  max_index = 26;     break;

    case RI1260_64A_MODULE: max_index = 55;     break;
    case RI1260_64B_MODULE: max_index = 43;     break;
    case RI1260_64C_MODULE: max_index = 37;     break;

    case RI1260_66A_MODULE: max_index = 35;     break;
    case RI1260_66B_MODULE: max_index = 23;     break;
    case RI1260_66C_MODULE: max_index = 11;     break;

    case RI1260_75A_MODULE: max_index = 47;     break;
    case RI1260_75B_MODULE: max_index = 95;     break;

    case RI1260_93A_MODULE: max_index = 47;     break;
    case RI1260_93B_MODULE: max_index = 95;     break;

    default:                max_index = -1;     break;
    }

    if (max_index == -1)
        return (RI1260_WRONG_CARD_TYPE);

    /* first note when consecutive elements of the array are different */
    cmd_len = 0;
    any_changed = 0;

    for (index = 0; index <= max_index;) {
    /* find a range of relays to be opened or closed */
        first_index = index;
        while (first_index <= max_index && states[first_index] != operation)
            ++first_index;

    /* found first index in a range, or past end of array */
        last_index = first_index;
        if (first_index <= max_index) {
            while (last_index <= max_index && states[last_index] == operation)
                ++last_index;

            --last_index;

        /* Determine if the command is too long and break it up if it is */
        /* (Leave a 20 space margin for error just in case) */
            if (cmd_len > RI1260_MAX_CMD - 20) {
                error = viPrintf (vi, "\n");
                if (error < 0) return (error);
                cmd_len = 0;
                any_changed = 0;
            }

            /* if this is the first item on a line, send the open/close */
            if (!any_changed) {
                error = viPrintf (vi, "%s %d.", open_close[operation],
                    module_addr);
                if (error < 0) return (error);

                cmd_len += 6;
                any_changed = 1;
            }

        /* separate by comma if not first on the list */
            else {
                error = viPrintf (vi, ",");
                if (error < 0) return (error);
            }

        /* format the range of the relays for the specific relay type */
            error = ri1260_format_range (vi, first_index, last_index, mod_type);
            if (error < 0) return (error);

            /* Bump up the length counter by the worst case value just in case */
            if(first_index == last_index)
                cmd_len += 5;
            else
                cmd_len += 10;
        }
        index = last_index + 1;
    }

    /* write out any residual command */
    if (any_changed)
		{
        error = viPrintf (vi, "\n");
        }
    else
        error = VI_SUCCESS;

	/* wait for up to 3 seconds for the 1260 to complete the command */
	if (error >= 0)
		error = ri1260_wait_ready(vi,3);
        
    return (error);
}

/*=========================================================================*/
/* This function formats a range of relays to be opened or closed, based   */
/* upon the type of relay module.                                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_format_range (ViSession vi, ViInt16 first_index,
    ViInt16 last_index, ViInt16 mod_type)
{
ViStatus error;

    error = ri1260_format_relay (vi, first_index, mod_type);
    if (error < 0) return (error);

    if (first_index < last_index) {
        error = viPrintf(vi, "-");
        if (error < 0) return (error);

        error = ri1260_format_relay (vi, last_index, mod_type);
        if (error < 0) return (error);
    }
    return (VI_SUCCESS);
}

/*=========================================================================*/
/* This function formats a relay based upon the type of relay module.      */
/* This ASSUMES that the relay has already been checked for the proper     */
/* range of values for the specified module type.                          */
/*=========================================================================*/
ViStatus _VI_FUNC ri1260_format_relay (ViSession vi, ViInt16 relay, ViInt16 mod_type)
{
ViInt16 group, matrix, row, column, interconnect, mux, channel;
ViStatus error;

    switch ( mod_type ) {
    case RI1260_12_MODULE:
    case RI1260_13_MODULE:
    case RI1260_16_MODULE:
    case RI1260_17_MODULE:
    case RI1260_18_MODULE:
    case RI1260_20_MODULE:
    case RI1260_39S_MODULE:
        error = viPrintf (vi, "%.1d", relay);
        break;

    case RI1260_30A_MODULE:
        group = 0;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_30B_MODULE:
        group = relay / 20;
        relay %= 20;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_30C_MODULE:
        group = relay / 10;
        relay %= 10;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_30D_MODULE:
        group = relay / 5;
        relay %= 5;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_35A_MODULE:
    case RI1260_35B_MODULE:
        error = viPrintf (vi, "%.1d", relay);
        break;

    case RI1260_36_MODULE:
        group = relay >> 3;   /* relay / 8 */
        relay %= 8;
        error = viPrintf (vi, "%d%.1d", group, relay);
        break;

    case RI1260_37A_MODULE:
        if(relay > 23) {
            group = 1;
            relay -= 24;
        }
        else
            group = 0;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_37B_MODULE:
        if(relay > 48) {
            group = 1;
            relay -= 49;
        }
        else
            group = 0;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

	case RI1260_38A_MODULE:
		interconnect = mux = 0;
		if(relay < 128) {
			mux = relay >> 3;
			channel = relay & 0x7;
		} else if(relay < 149) {
			relay -= 128;
//**** Fix for SQR 303
//**** is:
			interconnect = (relay / 3) + 1;
//**** was:
//****			interconnect = (relay / 3) - 1;
//**** End of SQR 303 fix.  Note: the 1260am.c file was correct!
			channel = relay % 3;
		} else if(relay == 149) {
			interconnect = 8;
			channel = 2;
		} else {
			interconnect = 9;
			channel = relay - 150;
		}
		error = viPrintf (vi, "%d%.2d%.1d", interconnect, mux, channel);
		break;
    
	case RI1260_38B_MODULE:
		interconnect = mux = 0;
		if(relay < 64) {
			channel = relay & 0x7;
			mux = (relay - channel) >> 2;
		} else if(relay < 71) {
			interconnect = relay - 64;
			channel = 0;
		} else {
			interconnect = 9;
			channel = relay - 71;
		}
		error = viPrintf (vi, "%d%.2d%.1d", interconnect, mux, channel);
		break;
    
    case RI1260_39_MODULE:
    	if (relay < 5)			/* DPST relays 0000 to 0004 */
    		{
    		error = viPrintf (vi, "%.1d", relay);
    		}
    	else if (relay < 53)	/* SPST relays, 1000 to 1047 */
    		{
    		relay -= 5;
	        error = viPrintf (vi, "10%.2d", relay);
    		}
    	else if (relay < 65)	/* 1x2 MUX relays, 2000 to 2501 */
    		{
    		relay -= 53;
    		interconnect = 20 + (relay >> 1);
    		row = 0;
    		relay %= 2;
	        error = viPrintf (vi, "%.2d0%.1d", interconnect, relay);
    		}
    	else if (relay < 77)	/* 1x4 MUX relays, 3000 to 3203 */
    		{
    		relay -= 65;
    		interconnect = 30 + (relay >> 2);
    		row = 0;
    		relay %= 4;
	        error = viPrintf (vi, "%.2d0%.1d", interconnect, relay);
    		}
    	else					/* 2x8 Matrix relays, 4000 to 4417 */
    		{
    		relay -= 77;
    		interconnect = 40 + (relay >> 4);
    		relay %= 16;
    		if (relay > 7)
    			{
    			row = 1;
    			relay -= 8;
    			}
    		else
    			row = 0;

	        error = viPrintf (vi, "%.2d%.1d%.1d", interconnect, row, relay);
    		}
    		
        break;
    	
    case RI1260_40A_MODULE:
        matrix = 0;
        row = relay / 24;
        column = relay % 24;
        error = viPrintf (vi, "%d%.1d%.2d", matrix, row, column);
        break;

    case RI1260_40B_MODULE:
        matrix = 0;
        row = relay / 12;
        column = relay % 12;
        error = viPrintf (vi, "%d%.1d%.2d", matrix, row, column);
        break;

    case RI1260_40C_MODULE:
        matrix = relay / 48;
        relay %= 48;
        row = relay / 12;
        column = relay % 12;
        error = viPrintf (vi, "%d%.1d%.2d", matrix, row, column);
        break;

    case RI1260_45A_MODULE:
    case RI1260_45B_MODULE:
    case RI1260_45C_MODULE:
        matrix = relay / 64;
        relay %= 64;
        row = relay / 16;
        column = relay % 16;
        error = viPrintf (vi, "%d%.1d%.2d", matrix, row, column);
        break;

    case RI1260_50A_MODULE:
    case RI1260_50B_MODULE:
    case RI1260_50C_MODULE:
    case RI1260_50D_MODULE:
    case RI1260_75A_MODULE:
    case RI1260_75B_MODULE:
    case RI1260_93A_MODULE:
    case RI1260_93B_MODULE:
        group = relay / 6;
        relay %= 6;
        if (relay == 5)
            relay = 9;

        error = viPrintf (vi, "%d%.1d", group, relay);
        break;

    case RI1260_54_MODULE:
        group = relay / 4;
        relay = relay % 4;
        error = viPrintf (vi, "%d%.1d", group, relay);
        break;

    case RI1260_58_MODULE:
        group = relay / 8;
        relay = relay % 8;
        error = viPrintf (vi, "%d%.1d", group, relay);
        break;

    case RI1260_59A_MODULE:
    case RI1260_59B_MODULE:
        group = relay / 4;
        relay = relay % 4;
        error = viPrintf (vi, "%d%.1d", group, relay);
        break;

    case RI1260_60_MODULE:
        group = relay / 12;
        relay %= 12;
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_64A_MODULE:
    case RI1260_64B_MODULE:
    case RI1260_64C_MODULE:
        if (relay < 32)
        {
            group = relay / 16;
            relay %= 16;
        }
        else
        {
            relay -= 32;
            group = 2 + relay / 6;
            relay %= 6;
        }
        error = viPrintf (vi, "%d%.2d", group, relay);
        break;

    case RI1260_66A_MODULE:
    case RI1260_66B_MODULE:
    case RI1260_66C_MODULE:
        group = relay / 6;
        relay = relay % 6;
        error = viPrintf (vi, "%d%.1d", group, relay);
        break;
    	

    default:    return (RI1260_WRONG_CARD_TYPE);
    }

    return (VI_SUCCESS);
}
/*========END==============================================================*/
