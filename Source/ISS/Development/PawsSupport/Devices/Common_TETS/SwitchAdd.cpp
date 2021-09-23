// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2020-07-06 16:01:5#$: Date of last commit
//    $Rev:: 27851              $: Revision of last commit
/***********************************************************************
*    PAWS Driver for the TETS Switching Subsystem
*    For ManTech 
*    Instrument is Racal Dana 1260 series VXI cards 
*    uses vxipnp extensively:
* ri1260_init (ViChar instrDescriptor[], ViBoolean IDQuery, 
*					 ViBoolean resetDevice, ViSession *instrHandle);
* ri1260_reset (ViSession instrHandle);
* ri1260_close (ViSession instrHandle);
* ri1260_38_operate_single (ViSession instrHandle, ViInt16 moduleAddress,
*					 ViInt16 operation,
*					 ViInt16 interconnectorSelector,
*					 ViInt16 muxNumber, ViInt16 channelNumber);
* ri1260_39_operate_single (ViSession instrHandle, ViInt16 moduleAddress,
*					 ViInt16 operation, ViInt16 relayType,
*					 ViInt16 channelNumber);
* ri1260_58_operate_single (ViSession instrHandle, ViInt16 moduleAddress,
*					 ViInt16 operation, ViInt16 MUXNumber,
*					 ViInt16 relayNumber);
* ri1260_66_operate_single (ViSession instrHandle, ViInt16 moduleAddress,
*					 ViInt16 operation, ViInt16 switchNumber,
*					 ViInt16 pole);
* PD Reference: paragraph 3.4.11 and 3.4.12.2.4 dated 21 March 1996 
*               update 30 April 1996
************************************************************************
*  FILENAME   :  Modsw.c
*
*  REVISION   :  2.4
*
*  DATE       :  05-OCT-00
*
*  DESCRIPTION:  PAWS driver for the Switch Interface
*                used in the ManTech TETS.
************************************************************************
*                       SOURCE REVISION HISTORY
*
*  VER  DATE     DESCRIPTION                       AUTHOR
*  ---  ------   --------------------------------  ---------------------
*  
*  2.2  10FEB98   Changed M_CON to M_DIS in        
*                 modsw_open function                   J. Colson, TYX Corp
*  2.2  01MAR98   Removed modsw_init and modsw_close
*                 functions for timing reasons          J. Colson, TYX Corp
*
*  2.3  13JUN00   Mike Eckart, MTSI
*				  Added procedure to modsw_cls to capture Block,
*				  Mod, and Path of LF switches to be used when
*				  calculating resistive path loss. 
*  2.3  13JUN00   Mike Eckart, MTSI
*				  Added function modsw_cls2 which is called by
*				  the DMM when it makes a resistance measurement.
*				  This function calls the modsw_cls function and sets a
*				  variable that tells modsw_cls to capture LF switch
*				  data for use in calculating resistive path loss.
*  2.4  05OCT00   Quoc Nguyen, MTSI
*                 Modified "void modsw_cls()" function to capture number of MF & HF 
*                 switches being closed for use in calculating pathloss.
*                 Modified "void modsw_opn()" function to open all MF & HF switches
***********************************************************************/

#include <stdio.h>
#include "key.h"
#include <visa.h>
#pragma warning(disable : 4115)
#include "cem.h"
#pragma warning(default : 4115)
#include "ri1260.h"  
#include "tets.h"

#define FLAG 10

DATUM		*p; /* Ptr: PATH Data */
int			cnt, trip_list[100], i, j; /* PATH Count */
short		blk, mod, pth, mux;
short		modsw_debug = 0, timedebug = 0;
static int	isAllocated = 0;
BOOL	    resistance_meas = FALSE;	//indicates whether a resistance measurement is being made  M.E. 06/20/00
ViStatus    ErrStatus;

extern int IsSimOrDeb(char dev_name[20]); 

void display_errormsg()
{
	ViChar		ERR_MSG[256];

	if (modsw_debug)
		Display("modsw debug: Entering display_errormsg()\n");

	if (ErrStatus != 0)
	{
		sprintf_s(msg_buf,sizeof(msg_buf), "error from r11260 - errno %x\n",ErrStatus);
		Display(msg_buf);
		ri1260_error_message(modsw_handle,ErrStatus,ERR_MSG);
		Display(ERR_MSG);
	}
}

/*************************************************************/
void cls_mux_interconnects()
{
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,1,0,1); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,2,0,2); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,3,0,0); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,3,0,1); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,4,0,0); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,4,0,1); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,6,0,0); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,6,0,1); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,7,0,0); 
	ErrStatus = ri1260_38_operate_single (modsw_handle, 3,1,7,0,1); 
}

/*************************************************************/
void modsw_cls()
{
    int no_rf_switch=0;
	if(modsw_debug)
		Display("modsw debug: Entering modsw_cls()\n");

	mux = 0; /* set 1260-38 to Mux programming config  */

int ii = M_PATH;
	//p = GetDatum(M_PATH, K_CON );   /* Get Pointer to PATH Data */
	p = RetrieveDatum(M_PATH, K_CON );   /* Get Pointer to PATH Data */
	cnt = DatCnt(p);                /* Get Number of Triplets   */

	/*  Do some error checking */
	if(modsw_debug)
	{                         
		sprintf_s(msg_buf,sizeof(msg_buf),"modsw debug: <modsw_cls> p = %d, cnt = %d", p, cnt);
		Display(msg_buf);
	}
	
	if (p == DNULL)
		ErrMsg(7, " No PATH Information");
	else if(DatTyp(p) != INTV)
		ErrMsg(7, " Invalid PATH Data Type");
	else if( cnt <= 0)
		ErrMsg(7, " Invalid PATH Data Count");
			
	for (i = 0; i < cnt; i++)
		trip_list[i] = (int) INTDatVal(p, i); /* Get Triplets */
	
	for(i=0; i < cnt; i = i + 3)
	{      
		blk = (short)trip_list[i];             /* Get BLocK Number     */
		mod = (short)trip_list[i+1];           /* Get MODule Number    */
		pth = (short)trip_list[i+2];           /* Get PaTH Number      */

		if(modsw_debug)
		{                                                                                     
			sprintf_s(msg_buf,sizeof(msg_buf),"modsw debug: <modsw_cls> blk = %d, mod = %d, pth = %d, mux = %d",blk,mod,pth,mux); 
			Display(msg_buf);
		}  

		switch(blk)
		{
			case 1 :
				ErrStatus = ri1260_39_operate_single (modsw_handle, blk,1, mod, pth); 
				display_errormsg();
				if (resistance_meas == TRUE)
				{
					lf_sw_path [no_of_switches][1] = blk; //added by ME 05/08/00
					lf_sw_path [no_of_switches][2] = mod; //to capture switch data used to 
					lf_sw_path [no_of_switches][3] = pth; //find resistive path loss
					no_of_switches ++;
				}
				break;
			case 2 :
				ErrStatus = ri1260_39_operate_single (modsw_handle, blk,1, mod, pth); 
				display_errormsg();
				if (resistance_meas == TRUE)
				{
					lf_sw_path [no_of_switches][1] = blk; //added by ME 05/08/00
					lf_sw_path [no_of_switches][2] = mod; //to capture switch data used to 
					lf_sw_path [no_of_switches][3] = pth; //find resistive path loss
					no_of_switches ++;
				}
				break;
			case 3:
				cls_mux_interconnects();
				ErrStatus = ri1260_38_operate_single (modsw_handle, blk,1, mux, mod, pth); 
				display_errormsg();
				if (resistance_meas == TRUE)
				{
					lf_sw_path [no_of_switches][1] = blk; //added by ME 05/08/00
					lf_sw_path [no_of_switches][2] = mod; //to capture switch data used to 
					lf_sw_path [no_of_switches][3] = pth; //find resistive path loss
					no_of_switches ++;
				}
				break;
			case 4 :
				ErrStatus = ri1260_58_operate_single (modsw_handle, blk,1,mod, pth); 
				display_errormsg();	
                j=sprintf_s(path_id[no_rf_switch],7,"S80%d-%d", mod + 1, pth + 2 ); // Modified by QN on 10/05/00 to capture number of MF switches being closed
                no_rf_switch ++;  // Modified by QN on 10/05/00 to capture number of MF switches being closed
				break;
			case 5 :
				ErrStatus = ri1260_66_operate_single (modsw_handle, blk,1,mod, pth);      
				display_errormsg();  
			 	j=sprintf_s(path_id[no_rf_switch],7,"S90%d-%d", mod + 1, pth + 2 ); // Modified by QN on 10/05/00 to capture number of HF switches being closed
                no_rf_switch ++; // Modified by QN on 10/05/00 to capture number of HF switches being closed
                break;
   			case 6 :
				break;
		} /* End switch */
	} /* End for */
    
	Sleep(100); /* Relay Settle Delay */
	FreeDatum(p); /*added 2-9-98 per BP */

	if(timedebug)
	{
		sprintf_s(msg_buf,sizeof(msg_buf),"<modsw_cls> time=%d",timeGetTime());
		Display(msg_buf);
	}
} /* End modsw_cls */

/*************************************************************/
void modsw_opn()
{
    int no_rf_switch=0;
	if(modsw_debug)
		Display("modsw debug: Entering modsw_opn()\n");

	mux = 0; /* set 1260-38 to Mux programming config  */

	//p = GetDatum(M_PATH, K_CON );   /* Get Pointer to PATH Data */
	p = RetrieveDatum(M_PATH, K_DIS );   /* Get Pointer to PATH Data */
	cnt = DatCnt(p);                /* Get Number of Triplets   */

	/*  Do some error checking   */
	if(modsw_debug)
	{                         
		sprintf_s(msg_buf,sizeof(msg_buf),"modsw debug: <modsw_opn> p = %d, cnt = %d",p,cnt);
		Display(msg_buf);
	}
	
	if (p == DNULL)
		ErrMsg(7, " No PATH Information");
	else if(DatTyp(p) != INTV)
		ErrMsg(7, " Invalid PATH Data Type");
	else if( cnt <= 0)
		ErrMsg(7, " Invalid PATH Data Count");
			
	for (i = 0; i < cnt; i++)
	{
		trip_list[i] = 0;
		trip_list[i] = (int) INTDatVal(p, i); /* Get Triplets */
	}

	for(i=0; i < cnt; i = i + 3)
	{ 
		blk = (short)trip_list[i];             /* Get BLocK Number     */
		mod = (short)trip_list[i+1];           /* Get MODule Number    */
		pth = (short)trip_list[i+2];           /* Get PaTH Number      */
         
		if(modsw_debug)
		{                                                                                     
			sprintf_s(msg_buf,sizeof(msg_buf),"modsw debug: <modsw_opn> blk = %d, mod = %d, pth = %d, mux = %d",blk,mod,pth,mux); 
			Display(msg_buf);
		} 
 	
		switch(blk)
		{
			case 1 :
				ErrStatus = ri1260_39_operate_single (modsw_handle, blk,0, mod, pth); 
				display_errormsg();
				if (no_of_switches >= 0)
				{
					no_of_switches --;
					lf_sw_path [no_of_switches][1] = 0; //added by ME 05/08/00
					lf_sw_path [no_of_switches][2] = 0; //to clear array lf_sw_path
					lf_sw_path [no_of_switches][3] = 0;
				}
				break;
			case 2 :
				ErrStatus = ri1260_39_operate_single (modsw_handle, blk,0, mod, pth); 
				display_errormsg();   
				if (no_of_switches >= 0)
				{
					no_of_switches --;
					lf_sw_path [no_of_switches][1] = 0; //added by ME 05/08/00
					lf_sw_path [no_of_switches][2] = 0; //to clear array lf_sw_path
					lf_sw_path [no_of_switches][3] = 0;
				}
				break;
			case 3:
				ErrStatus = ri1260_38_operate_single (modsw_handle, blk,0, mux, mod, pth); 
				display_errormsg();   
				if (no_of_switches >= 0)
				{
					no_of_switches --;
					lf_sw_path [no_of_switches][1] = 0; //added by ME 05/08/00
					lf_sw_path [no_of_switches][2] = 0; //to clear array lf_sw_path
					lf_sw_path [no_of_switches][3] = 0;
				}
				break;
			case 4 :
				ErrStatus = ri1260_58_operate_single (modsw_handle, blk,0,mod, pth); 
				display_errormsg();  
				j  = sprintf_s( path_id[no_rf_switch],7,"" ); // Modified by QN on 10/05/00 to open all MF switches
                no_rf_switch ++; // Modified by QN on 10/05/00 to disconnect MF switches
				break;
			case 5 :
				ErrStatus = ri1260_66_operate_single (modsw_handle, blk,0,mod, pth);            
				display_errormsg();   
				j  = sprintf_s( path_id[no_rf_switch],7,"" ); // Modified by QN on 10/05/00 to open all HF switches
                no_rf_switch ++; //// Modified by QN on 10/05/00 to disconnect HF switches
				break;
			case 6 :
				break;
		} /* End switch */
	} /* End for */

	FreeDatum(p); /* added 2-9-98 per BP */

	if(timedebug)
	{
		sprintf_s(msg_buf,sizeof(msg_buf),"<modsw_opn> time=%d",timeGetTime());
		Display(msg_buf);
	}
}
/************************************************************/
void modsw_cls2()
{
	int n = 0;

	resistance_meas = TRUE;
	no_of_switches = 0;

	modsw_cls();

	if (IsSimOrDeb("DMM") & FLAG) //added 6/13/00 by M.E. in order to 
	{							  //view data in array lf_sw_path
		n = no_of_switches;
		while(n > 0)
		{
			n --;
			sprintf_s(msg_buf,sizeof(msg_buf),"\033[32;40mdmm debug: Block=%d Module=%d Path=%d\033[m\n", lf_sw_path [n][1],
					lf_sw_path [n][2], lf_sw_path [n][3]);
			Display(msg_buf);
		}
	}
	resistance_meas = FALSE;
}
