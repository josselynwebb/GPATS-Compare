#include <utility.h>

#include <stdlib.h>		/* prototype for malloc() */
#include <string.h>		/* prototype for strcpy() */
#include <stdio.h>		/* prototype for sprintf() */
#include "visa.h"
#define INSTR_CALLBACKS		/* needed to get hander types in ri4152a.h */
#include "ri4152a.h"
			
#define ri4152a_MODEL_CODE  579
#define ri4152a_MANF_ID     0xFFB
#define ri4152a_IDN_STRING  "Racal Instruments,4152A"
#define hpe1412a_IDN_STRING  "HEWLETT-PACKARD,E1412A"
#define hpe1412_MODEL_CODE  579
#define hpe1412_MANF_ID     4095

#define ri4152a_REV_CODE "B.2.1"  /* Driver Revision */

#define ri4152a_ERR_MSG_LENGTH 256  /* size of error message buffer */

#define ri4152a_MAX_STAT_HAP 11		    /* number of happenings */
#define ri4152a_MAX_STAT_REG 4		    /* number of IEEE 488.2 status registers */

/* this has to match the index of the ESR register in ri4152a_accessInfo[] */
#define ri4152a_ESR_REG_IDX 2

/* this needs to match the index location of ri4152a_USER_ERROR_HANLDER
 * in the ri4152a_statusHap[] array.  Normally, this is zero.
 */
#define ri4152a_USER_ERROR_HANDLER_IDX 0

/*=============================================================== 
 *
 *  All messages are stored in this area to aid in localization 
 *
 *=============================================================== 
 */

#define ri4152a_MSG_VI_OPEN_ERR 				\
	"vi was zero.  Was the ri4152a_init() successful?"

#define ri4152a_MSG_CONDITION					\
	"condition"
	/* ri4152a_statCond_Q() */

#define ri4152a_MSG_EVENT						\
	"event"	
	/* ri4152a_statEvent_Q() */

#define ri4152a_MSG_EVENT_HDLR_INSTALLED				\
	"event handler is already installed for event happening"
	/* ri4152a_statEvent_Q() */

#define ri4152a_MSG_EVENT_HDLR_INST2				\
	"Only 1 handler can be installed at a time."	
	/* ri4152a_statEvent_Q() */

#define ri4152a_MSG_INVALID_HAPPENING				\
	"is not a valid happening."
	/* ri4152a_statCond_Q() */
	/* ri4152a_statEven_Q() */
	/* ri4152a_statEvenHdlr() */
	/* ri4152a_statEvenHdlr_Q() */

#define ri4152a_MSG_NOT_QUERIABLE					\
	"is not queriable."	
	/* ri4152a_statCond_Q() */
	/* ri4152a_statEven_Q() */


#define ri4152a_MSG_IN_FUNCTION					\
	"in function" 		
	/* ri4152a_error_message() */

#define ri4152a_MSG_INVALID_STATUS					\
  	"Parameter 2 is invalid"				\
	"in function ri4152a_error_message()."
	/* ri4152a_error_message() */

#define ri4152a_MSG_INVALID_STATUS_VALUE				\
	"is not a valid viStatus value."
	/* ri4152a_error_message() */

#define  ri4152a_MSG_INVALID_VI					\
  	"Parameter 1 is invalid"				\
	" in function ri4152a_error_message()"			\
	".  Using an inactive ViSession may cause this error."	\
	"  Was the instrument driver closed prematurely?"
	/* ri4152a_message_query() */

#define ri4152a_MSG_NO_ERRORS					\
	"No Errors"
	/* ri4152a_error_message() */

#define ri4152a_MSG_SELF_TEST_FAILED 				\
	"Self test failed." 	
	/* ri4152a_self_test() */

#define ri4152a_MSG_SELF_TEST_PASSED 				\
	"Self test passed."
	/* ri4152a_self_test() */

/* the following messages are used by the functions to check parameters */

#define ri4152a_MSG_BOOLEAN   "Expected 0 or 1; Got %d"

#define ri4152a_MSG_REAL   "Expected %lg to %lg; Got %lg"
   
#define ri4152a_MSG_INT   "Expected %hd to %hd; Got %hd"

#define ri4152a_MSG_LONG   "Expected %ld to %ld; Got %ld"

#define ri4152a_MSG_LOOKUP "Error converting string response to integer"

#define ri4152a_MSG_NO_MATCH "Could not match string %s"

/* 
 * static error message 
 */

#define VI_ERROR_PARAMETER1_MSG				\
	"Parameter 1 is invalid"

#define VI_ERROR_PARAMETER2_MSG				\
	"Parameter 2 is invalid"

#define VI_ERROR_PARAMETER3_MSG				\
	"Parameter 3 is invalid"

#define VI_ERROR_PARAMETER4_MSG				\
	"Parameter 4 is invalid"

#define VI_ERROR_PARAMETER5_MSG				\
	"Parameter 5 is invalid"

#define VI_ERROR_PARAMETER6_MSG				\
	"Parameter 6 is invalid"

#define VI_ERROR_PARAMETER7_MSG				\
	"Parameter 7 is invalid"

#define VI_ERROR_PARAMETER8_MSG				\
	"Parameter 8 is invalid"

#define VI_ERROR_FAIL_ID_QUERY_MSG				\
	"Instrument IDN does not match."

#define INSTR_ERROR_INV_SESSION_MSG 				\
	"ViSession (parmeter 1) was not created by this driver"

#define INSTR_ERROR_NULL_PTR_MSG				\
	"NULL pointer detected"

#define INSTR_ERROR_RESET_FAILED_MSG				\
	"reset failed"

#define INSTR_ERROR_UNEXPECTED_MSG 				\
	"An unexpected error occurred"

#define INSTR_ERROR_DETECTED_MSG			\
	"Instrument Error Detected, call ri4152a_error_query()."

#define INSTR_ERROR_LOOKUP_MSG   				\
	"String not found in table"

/*================================================================*/

#define ri4152a_DEBUG_CHK_THIS( vi, thisPtr) 			\
	/* check for NULL user data */				\
	if( 0 == thisPtr)					\
	{							\
 		ri4152a_LOG_STATUS(                             	\
		  vi, NULL, ri4152a_INSTR_ERROR_INV_SESSION );		\
	}							\
	{							\
		ViSession defRM;				\
								\
		/* This should never fail */			\
		errStatus = viGetAttribute( vi,                 \
			VI_ATTR_RM_SESSION, &defRM);		\
		if( VI_SUCCESS > errStatus )			\
		{						\
 			ri4152a_LOG_STATUS(				\
			  vi, NULL, ri4152a_INSTR_ERROR_UNEXPECTED );	\
		}						\
		if( defRM != thisPtr->defRMSession)		\
		{						\
 			ri4152a_LOG_STATUS(				\
			  vi, NULL, ri4152a_INSTR_ERROR_INV_SESSION );	\
		}						\
	}

#define ri4152a_DEBUG_CHK_NULL_PTR( vi, ptr) 			\
	if( 0 == ptr) 						\
	{							\
 		ri4152a_LOG_STATUS( 				\
		   vi, NULL, ri4152a_INSTR_ERROR_NULL_PTR );		\
	}

#define ri4152a_CDE_INIT( funcname)  				\
	strcpy(thisPtr->errFuncName, funcname);			\
	thisPtr->errNumber = VI_SUCCESS;			\
	thisPtr->errMessage[0] = 0;

#define ri4152a_CDE_MESSAGE( message ) 	 			\
	strcpy(thisPtr->errMessage, message)

#define ri4152a_CDE_ERROR( status)					\
	thisPtr->errNumber = status		


struct ri4152a_eventHdlrStruct {
	ri4152a_InstrEventHandler eventHandler;
	ViAddr userData;
};

struct ri4152a_globals {
	ViSession			defRMSession;
	ViStatus			errNumber;
	char 				errFuncName[40];
	char				errMessage[160];
	ViBoolean			errQueryDetect;
	ViInt32 			driverEventArray[ri4152a_MAX_STAT_REG];
	ViInt32 			numEventHandlers;
	struct ri4152a_eventHdlrStruct	eventHandlerArray[ri4152a_MAX_STAT_HAP];

	/* Setting this to VI_TRUE, 
	 * will prevent I/O from occuring in an SRQ
	 * This is needed, because VTL 3.0 doesn't provide
	 * an atomic write/read operations.  Don't want
	 * to do I/O if the driver is in the middle of
	 * a read from the instrument.
	 */
	ViBoolean			blockSrqIO;
	ViInt32             countSrqIO;
};
/* add the following to the globals data structure */

/* this prototype needed for statusUpdate routine */
static void ri4152a_srqTraverse( ViSession vi, ViInt32 eventReg);


/* Note: for debugging, you can add __FILE__, __LINE__ as parameters
 * to ri4152a_LOG_STATUS, and ViString filename, and ViInt32 lineNumber to
 * ri4152a_statusUpdate() in order to determine exactly where an error
 * occured in a driver.
 */
#define ri4152a_LOG_STATUS( vi, thisPtr, status ) 	\
  return ri4152a_statusUpdate( vi, thisPtr, status)

static int is_four_wire = 0;

static ViStatus ri4152a_statusUpdate(
  ViSession vi,
  struct ri4152a_globals *thisPtr,
  ViStatus status
  )
{
	ViInt32  eventQ;

	/* ALL functions exit through here.
	 *
	 * this allows errors to be trapped and
	 * a user error handler could be called.
	 */

        /* can do nothing without status structure */
        if (!thisPtr) return status;

	/* Check if user wants to query the instrument state.
         * This is only done, if the vi is valid and 
         * no other errors has occured.
         */
	if( VI_TRUE == thisPtr->errQueryDetect && 
            VI_SUCCESS <= status &&
            vi != VI_NULL ) {
	    /* assume IEEE 488.2 Instrument and query standard
             * status event register for a parser error
             */		
        thisPtr->blockSrqIO = VI_TRUE;

	    status = viPrintf(vi, "*esr?\n");
	    if( VI_SUCCESS <= status) {
			status = viScanf(vi, "%ld%*t\n", &eventQ);
			if( VI_SUCCESS <= status){
	  	    /* update driver's copy of the esr register 
       		     * This index 'ri4152a_ESR_REG_IDX' should 
		     * match the access event register array 
		     * and correspond to the 
      		     * standard event status register.
       		     */
		    	thisPtr->driverEventArray[ri4152a_ESR_REG_IDX] = 
		      	thisPtr->driverEventArray[ri4152a_ESR_REG_IDX] | eventQ;

		    /* see if the instrument thinks an error occured */
		    	if( (  0x04 /* Query Error */
                      | 0x08 /* Device Dependent Error */
                      | 0x10 /* Execution Error */
                      | 0x20 /* Command Error */
                    ) & eventQ ) 
			/* set our error status, to indicate that a 
			 * instrument error occured.
                         */
					status =  ri4152a_INSTR_ERROR_DETECTED;
			} /* viScanf was successful */
	    } /* viPrinf was successful */
	} /* if we need to query the instrument */	

	/* check if we need to service SRQ events */
	if( 0 != thisPtr->countSrqIO && VI_NULL != vi ) {
            ViStatus errStatus;

            /* suspend event processing for now */
            errStatus = viEnableEvent( vi, 
              VI_EVENT_SERVICE_REQ, VI_SUSPEND_HNDLR, VI_NULL);
            if( VI_SUCCESS > errStatus ){
                /* an existing error message has priority */
                if( VI_SUCCESS <= status)
                    /* no existing error message, so set new error */
                    status = errStatus;
            } /* if - an error occured */
            else { 
	        /* reset the srq counter */
	        	thisPtr->countSrqIO = 0;

                /* recursively traverse status byte */
                ri4152a_srqTraverse( vi, 0);

                /* re-enable event processing */
                errStatus = viEnableEvent( vi, VI_EVENT_SERVICE_REQ, 
                  VI_HNDLR, VI_NULL);
                if( VI_SUCCESS > errStatus ) {
                    /* an existing error message has priority */
                    if( VI_SUCCESS <= status)
                        /* no existing error message, so set new error */
                        status = errStatus;
                } /* if - an error occured */
            } /* else - no error in viEnableEvent */

	} /* if - srq needs servicing */

        /* unblock IO in SRQs */
	thisPtr->blockSrqIO = VI_FALSE;

	/* store the context dependent error number */
        ri4152a_CDE_ERROR(status);

	/* if an error occurs, see if the user has an error handler enabled */
        if( status != ri4152a_INSTR_ERROR_DETECTED &&
	    VI_SUCCESS > status &&
	    thisPtr->eventHandlerArray[
	        ri4152a_USER_ERROR_HANDLER_IDX].eventHandler){ 
	    /* call the users handler */
	    thisPtr->eventHandlerArray[
                ri4152a_USER_ERROR_HANDLER_IDX].eventHandler(
		  vi, (ViInt32)status, 
	    	  thisPtr->eventHandlerArray[
                      ri4152a_USER_ERROR_HANDLER_IDX].userData);
	} 

	/* return the appropriate error number */
	return status;
}



/* 
 * Error Message Structures
 */

struct instrErrStruct {
	ViStatus errStatus;
	ViString errMessage;
};

const static struct instrErrStruct instrErrMsgTable[] ={
        { VI_ERROR_PARAMETER1,	VI_ERROR_PARAMETER1_MSG },
        { VI_ERROR_PARAMETER2,	VI_ERROR_PARAMETER2_MSG },
        { VI_ERROR_PARAMETER3,	VI_ERROR_PARAMETER3_MSG },
        { VI_ERROR_PARAMETER4,	VI_ERROR_PARAMETER4_MSG },
        { VI_ERROR_PARAMETER5,	VI_ERROR_PARAMETER5_MSG },
        { VI_ERROR_PARAMETER6,	VI_ERROR_PARAMETER6_MSG },
        { VI_ERROR_PARAMETER7,	VI_ERROR_PARAMETER7_MSG },
        { VI_ERROR_PARAMETER8,	VI_ERROR_PARAMETER8_MSG },
        { VI_ERROR_FAIL_ID_QUERY,	VI_ERROR_FAIL_ID_QUERY_MSG },

	{ ri4152a_INSTR_ERROR_INV_SESSION,	INSTR_ERROR_INV_SESSION_MSG },
        { ri4152a_INSTR_ERROR_NULL_PTR,	INSTR_ERROR_NULL_PTR_MSG },
        { ri4152a_INSTR_ERROR_RESET_FAILED,INSTR_ERROR_RESET_FAILED_MSG },
        { ri4152a_INSTR_ERROR_UNEXPECTED,	INSTR_ERROR_UNEXPECTED_MSG },
	{ ri4152a_INSTR_ERROR_DETECTED,	INSTR_ERROR_DETECTED_MSG },
	{ ri4152a_INSTR_ERROR_LOOKUP,	INSTR_ERROR_LOOKUP_MSG },
};


/* macros for testing parameters */
#define ri4152a_CHK_BOOLEAN( my_val, err ) if( ri4152a_chk_boolean( thisPtr, my_val) ) ri4152a_LOG_STATUS( vi, thisPtr, err);

static ViBoolean ri4152a_chk_boolean(
  struct ri4152a_globals *thisPtr,
  ViBoolean my_val)
{
   char message[ri4152a_ERR_MSG_LENGTH];
   if( (my_val != VI_TRUE) && (my_val != VI_FALSE) ) {
      /* true = parameter is invalid */
      sprintf(message, ri4152a_MSG_BOOLEAN, my_val);	
      ri4152a_CDE_MESSAGE(message);				
      /* true = parameter is invalid */
      return VI_TRUE;
   }

   /* false = okay */
   return VI_FALSE;
}


#define ri4152a_CHK_REAL_RANGE( my_val, min, max, err ) if( ri4152a_chk_real_range( thisPtr, my_val, min, max) ) ri4152a_LOG_STATUS( vi, thisPtr, err);

static ViBoolean ri4152a_chk_real_range( 
  struct ri4152a_globals *thisPtr,
  ViReal64 my_val,
  ViReal64 min,
  ViReal64 max)
{
   char message[ri4152a_ERR_MSG_LENGTH];

   if ( ( my_val < min ) || (my_val > max) ){								
      sprintf(message, ri4152a_MSG_REAL, min, max, my_val);	
      ri4152a_CDE_MESSAGE(message);				
      /* true = parameter is invalid */
      return VI_TRUE;
   }
   return VI_FALSE;
} 
   
#define ri4152a_CHK_INT_RANGE( my_val, min, max, err ) if( ri4152a_chk_int_range( thisPtr, my_val, min, max) ) ri4152a_LOG_STATUS( vi, thisPtr, err);

static ViBoolean ri4152a_chk_int_range( 
  struct ri4152a_globals *thisPtr,
  ViInt16 my_val,
  ViInt16 min,
  ViInt16 max)
{
   char message[ri4152a_ERR_MSG_LENGTH];

   if ( ( my_val < min ) || (my_val > max) ){								
      sprintf(message, ri4152a_MSG_INT, min, max, my_val);	
      ri4152a_CDE_MESSAGE(message);				
      /* true = parameter is invalid */
      return VI_TRUE;
   }
   return VI_FALSE;
} 
   
   
#define ri4152a_CHK_LONG_RANGE( my_val, min, max, err ) if( ri4152a_chk_long_range( thisPtr, my_val, min, max) ) ri4152a_LOG_STATUS( vi, thisPtr, err);

static ViBoolean ri4152a_chk_long_range( 
  struct ri4152a_globals *thisPtr,
  ViInt32 my_val,
  ViInt32 min,
  ViInt32 max)
{
   char message[ri4152a_ERR_MSG_LENGTH];

   if ( ( my_val < min ) || (my_val > max) )  			
   {								
      sprintf(message, ri4152a_MSG_LONG, min, max, my_val);	
      ri4152a_CDE_MESSAGE(message);				
      /* true = parameter is invalid */
      return VI_TRUE;
   }
   return VI_FALSE;
} 
   
   
#define ri4152a_CHK_ENUM( my_val, limit, err ) if( ri4152a_chk_enum( thisPtr, my_val, limit) ) ri4152a_LOG_STATUS( vi, thisPtr, err);

/* utility routine which searches for a string in an array of strings. */
/* This is used by the CHK_ENUM macro */
static ViBoolean ri4152a_chk_enum (
  struct ri4152a_globals *thisPtr,
  ViInt16 my_val,
  ViInt16 limit)
{
    char message[ri4152a_ERR_MSG_LENGTH];

    if ( ( my_val < 0 ) || (my_val > limit)) {								
        sprintf(message, ri4152a_MSG_INT, 0, limit, my_val);	
        ri4152a_CDE_MESSAGE(message);				
        /* true = parameter is invalid */
        return VI_TRUE;
    }

    return VI_FALSE;
}


/*  ==========================================================================  
     This function searches an array of strings for a specific string and     
     returns its index.  If successful, a VI_SUCCESS is returned, 
     else ri4152a_INSTR_ERROR_LOOKUP is returned.
    ======================================================================== */
ViStatus ri4152a_findIndex (struct ri4152a_globals *thisPtr,
			const char * const array_of_strings[],
			  /*last entry in array must be 0 */
			const char *string, /* string read from instrument */
			ViPInt16 index) /* result index */
{
    ViInt16 i;
    ViInt16 my_len;
    char search_str[20];
    char message[80];

    strcpy(search_str, string);

  /* get rid of newline if present in string */
  /* needed because %s includes newline in some VTL's */
    my_len = strlen(search_str);
    if (search_str[my_len - 1] == '\n') search_str[my_len - 1] = '\0';

    for (i = 0; array_of_strings[i]; i++)
		if (!strcmp (array_of_strings[i], search_str)){
	    	*index = i;
	    	return VI_SUCCESS;
		}

  /* if we got here, we did not find it */
    sprintf(message, ri4152a_MSG_NO_MATCH, search_str);	
    ri4152a_CDE_MESSAGE(message);				
    return ri4152a_INSTR_ERROR_LOOKUP;
}

ViStatus  ri4152a_findRsrc (
  ViInt16 arraySize,
  char rsrcList[][80],
  ViRsrc rsrcDesc,
  ViPInt16 matches );

ViStatus  ri4152a_findRsrc(
  ViInt16 arraySize,
  char rsrcList[][80],
  ViRsrc rsrcDesc,
  ViPInt16 matches )
{
	ViStatus errStatus;
	ViSession defRM;
	ViUInt16 manfId;
	ViUInt16 modelCode;
    ViFindList foundList;
    char instrDesc[256];
    ViUInt32 retCnt, i;
    ViSession vi;
	ViUInt16 intf;
	char idn_buf[256];
    ViInt16 success;

	*matches = 0;

	/* Find the Default Resource Manager */
	if( errStatus = viOpenDefaultRM( &defRM) )
		/* Errors: VI_ERROR_SYSTEM_ERROR 
		 *         VI_ERROR_ALLOC
		 */
		ri4152a_LOG_STATUS( VI_NULL, NULL, errStatus);

        /* go get the first instrument of type rsrcDesc */
    if (errStatus = viFindRsrc( defRM, rsrcDesc, &foundList, &retCnt,
                                    instrDesc) )
		/* Errors: VI_ERROR_INV_SESSION
		 *         VI_ERROR_INV_OBJECT
		 *         VI_ERROR_NSUP_OPER
		 *         VI_ERROR_INV_EXPR
		 *         VI_ERROR_RSRC_NFOUND
		 */
		ri4152a_LOG_STATUS( VI_NULL, NULL, errStatus);
  
        /* check out all the instruments found to see if they are 
         * ones this driver supports.  Note that we fall through if
         * the retCnt is 0 (no instruments found).
        */
    for (i=0; i<retCnt; i++) {
            /* find the next one if not first time through this loop */
      	if (i)
        	if (errStatus = viFindNext( foundList, instrDesc))
		    /* Errors: VI_ERROR_INV_SESSION
		     *         VI_ERROR_INV_OBJECT
		     *         VI_ERROR_NSUP_OPER
		     *         VI_ERROR_RSRC_NFOUND
		     */
		    	ri4152a_LOG_STATUS( VI_NULL, NULL, errStatus);

       /* if not GPIB-VXI or VXI, then don't open it because some vendors'
          VTL crashes if try to open GPIB only stuff.
       */
            if (strstr(instrDesc,"VXI")==0 )  
               continue;

	    if( errStatus = viOpen( defRM, instrDesc, VI_NULL, VI_NULL, &vi) )
		/* Errors: VI_ERROR_NSUP_OPER
		 *         VI_ERROR_INV_RSRC_NAME
		 *         VI_ERROR_INV_ACC_MODE
		 *         VI_ERROR_RSRC_NFOUND
		 *         VI_ERROR_ALLOC
		 */
			ri4152a_LOG_STATUS(VI_NULL, NULL, errStatus);

	    /* find the interface type */
	    if( errStatus = viGetAttribute( vi, VI_ATTR_INTF_TYPE, &intf) ){	
		/* Errors: VI_ERROR_NSUP_ATTR */

               /* Unexpected error, so close things and exit via LOG_STATUS */
				viClose( vi);
				viClose( defRM);	/* also closes vi session */
	
			ri4152a_LOG_STATUS( VI_NULL, NULL, errStatus);
	    }

            /* initialize success flag, model code and manuf ID */
            success = 0;
            manfId = 0;
            modelCode = 0;
	    
	    switch( intf) {
	        case VI_INTF_GPIB:
		    /* check for a idn match
                     * need strncmp due to \n EOI imbedded in %t
                     */
		    viPrintf(vi, "*IDN?\n");
		    viScanf(vi, "%t", idn_buf); 

                    /* if we find a ri4152a, indicate success */
                    if( strncmp( idn_buf, ri4152a_IDN_STRING, 23) == 0)
                        success = 1;

		    break;

		    case VI_INTF_VXI:
		    case VI_INTF_GPIB_VXI:
	
			/* look for a matching VXI manfacturer's ID */
			if( errStatus = viGetAttribute(vi, VI_ATTR_MANF_ID,
                            &manfId) ) {	
			    /* Errors: VI_ERROR_NSUP_ATTR */
               		    /* Unexpected error, so close things and exit
                             * via LOG_STATUS
                            */
			    viClose( vi);
			    viClose( defRM);	/* also closes vi session */
			}

            if( errStatus = viGetAttribute(vi,
               VI_ATTR_MODEL_CODE, (ViPAttrState)(&modelCode)) ){	
			    /* Errors: VI_ERROR_NSUP_ATTR */
			    /* Note: this should never happen 
			     *   with a VXI instrument
			     */

               		    /* Unexpected error, so close things and exit
                             * via LOG_STATUS
                            */
			    viClose( vi);
			    viClose( defRM);	/* also closes vi session */
			}


			if( (manfId == ri4152a_MANF_ID) && 
			    (modelCode == ri4152a_MODEL_CODE))
			break;

		    default:
               		/* Should not get here, if we do, close things and exit
                         * via LOG_STATUS
                         */
				viClose( vi);
				viClose( defRM);	/* also closes vi session */
				ri4152a_LOG_STATUS(vi, NULL,
                		            ri4152a_INSTR_ERROR_UNEXPECTED);

        }	

             /* Close this one and possibly go open another */
	    viClose( vi);

        if (success) {
              *matches += 1;
              if ( *matches <= arraySize)
                    strcpy( rsrcList[*matches - 1], instrDesc);
               else
                    /* assume someone only wanted the first n found, so
                     * once we find that many, exit successfully via
                     * LOG_STATUS.
                    */
                    ri4152a_LOG_STATUS(VI_NULL, NULL, VI_SUCCESS);
         }
      }

        /* We are done, so close the Default Resource Manager */
        viClose( defRM );

        ri4152a_LOG_STATUS(VI_NULL, NULL, VI_SUCCESS);

} /* end of ri4152a_findRsrc */



/****************************************************************************
ri4152a_init
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | InstrDesc                                         ViRsrc      IN
  |   ---------------------------------------------------------------------
  |  | The Instrument Description.
  |  |
  |  | Examples: VXI::5, GPIB-VXI::128::INSTR
   ------------------------------------------------------------------------
  | id_query                                          ViBoolean   IN
  |   ---------------------------------------------------------------------
  |  | if( VI_TRUE) Perform In-System Verification.
  |  | if(VI_FALSE) Do not perform In-System Verification
   ------------------------------------------------------------------------
  | reset                                             ViBoolean   IN
  |   ---------------------------------------------------------------------
  |  | IF( VI_TRUE) Perform Reset Operation.
  |  | if(VI_FALSE) Do not perform Reset operation
   ------------------------------------------------------------------------
  | vi                                                ViPSession  OUT
  |   ---------------------------------------------------------------------
  |  | Instrument Handle. This is VI_NULL if an error occurred
  |  | during the init.

*****************************************************************************/

ViStatus _VI_FUNC ri4152a_init (
  ViRsrc InstrDesc, 
  ViBoolean id_query,
  ViBoolean reset,
  ViPSession vi )
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;
	ViSession defRM;

	ViUInt16 manfId;
	ViUInt16 modelCode;
	ViUInt32 cnt;
	ViInt32 idx;

	*vi = VI_NULL;

	/* Find the Default Resource Manager */
	errStatus = viOpenDefaultRM( &defRM);
	if( VI_SUCCESS > errStatus)
		/* Errors: VI_ERROR_SYSTEM_ERROR 
		 *         VI_ERROR_ALLOC
		 */
		ri4152a_LOG_STATUS( *vi, NULL, errStatus);
		
	/* Open a session to the instrument */
	errStatus = viOpen( defRM, InstrDesc, VI_NULL, VI_NULL, vi);
	if( VI_SUCCESS > errStatus) {	
		viClose( defRM);
		/* Errors: VI_ERROR_NSUP_OPER
		 *         VI_ERROR_INV_RSRC_NAME
		 *         VI_ERROR_INV_ACC_MODE
		 *         VI_ERROR_RSRC_NFOUND
		 *         VI_ERROR_ALLOC
		 */
		*vi = VI_NULL;
		ri4152a_LOG_STATUS( *vi, NULL, errStatus);
	}

	/* get memory for instance specific globals */
	thisPtr = (struct ri4152a_globals *)malloc(sizeof( struct ri4152a_globals) );
	if( 0 == thisPtr) {
		viClose( defRM);	/* also closes vi session */
		*vi = VI_NULL;
		ri4152a_LOG_STATUS(*vi, NULL, VI_ERROR_ALLOC);
	}

	/* associate memory with session, should not fail because
	 *   session is valid; and attribute is defined, supported,
	 *   and writable.
	 */
	errStatus = viSetAttribute( 
	              *vi, 
		      VI_ATTR_USER_DATA, 
	              (ViAttrState)thisPtr); 
	if( VI_SUCCESS > errStatus) {
		viClose( *vi);
		viClose( defRM);	/* also closes vi session */
		*vi = VI_NULL;
		ri4152a_LOG_STATUS(*vi, NULL, errStatus);
	}

	/* initialize instance globals */
	thisPtr->defRMSession = defRM;
        thisPtr->errNumber = VI_SUCCESS;
	thisPtr->errFuncName[0] = 0;
	thisPtr->errMessage[0] = 0;
	thisPtr->errQueryDetect = VI_FALSE;
	thisPtr->blockSrqIO = VI_FALSE;
	thisPtr->countSrqIO = 0;


	for( idx=0;idx<ri4152a_MAX_STAT_REG; idx++)
	    thisPtr->driverEventArray[idx] = 0;
	thisPtr->numEventHandlers = 0;
	
	for( idx=0;idx<ri4152a_MAX_STAT_HAP; idx++) {
	    thisPtr->eventHandlerArray[idx].eventHandler = NULL;
	    thisPtr->eventHandlerArray[idx].userData = 0;
	}
	
	if( VI_TRUE == reset )
		/* dev clr andcall the reset function to reset the instrument */
		if( viClear(*vi) < VI_SUCCESS || ri4152a_reset(*vi) < VI_SUCCESS ) {
			/* ignore any errors in PREFIX_close */
			ri4152a_close( *vi);
                        *vi=VI_NULL;
			ri4152a_LOG_STATUS( *vi, NULL, 
		    ri4152a_INSTR_ERROR_RESET_FAILED);
		}
	
	if( VI_TRUE == id_query ) {
		ViUInt16 intf;
		char     idn_buf[256];
		
		/* find the interface type */
		if( errStatus = viGetAttribute( *vi, VI_ATTR_INTF_TYPE, &intf) ){	
			/* Errors: VI_ERROR_NSUP_ATTR */

			/* ignore any errors in PREFIX_close */
			ri4152a_close( *vi);
                        *vi=VI_NULL;
	
			ri4152a_LOG_STATUS( *vi, NULL, errStatus);
		}

		switch( intf){
			case VI_INTF_GPIB:
				if (
				    viClear(*vi) < VI_SUCCESS ||
				    viPrintf(*vi, "*IDN?\n") < VI_SUCCESS ||
				    viRead(*vi, (ViPBuf) idn_buf, (ViUInt32) sizeof (idn_buf), &cnt) < VI_SUCCESS ||
				    /* check for a idn match */
				    (
				        strncmp(idn_buf, ri4152a_IDN_STRING, 
					    strlen (ri4152a_IDN_STRING)) &&
					    strncmp (idn_buf, hpe1412a_IDN_STRING,
					    20)
					)
				    )
                		{   
					/* ignore any errors in PREFIX_close */
					ri4152a_close( *vi);
                                        *vi=VI_NULL;
					ri4152a_LOG_STATUS( *vi, NULL,
					  VI_ERROR_FAIL_ID_QUERY);
                		}

				break;
       
			case VI_INTF_VXI:
			case VI_INTF_GPIB_VXI:
	
				/* find the VXI manfacturer's ID */
				errStatus = viGetAttribute( *vi, 
				    VI_ATTR_MANF_ID, &manfId);
				if( VI_SUCCESS > errStatus)
				{	
					/* Errors: VI_ERROR_NSUP_ATTR */
	
					/* ignore any errors in PREFIX_close */
					ri4152a_close( *vi);
                                        *vi=VI_NULL;
	
					ri4152a_LOG_STATUS( *vi, NULL,
					  errStatus);
				}

				/* find the instrument's model code */
				errStatus = viGetAttribute( *vi, 
				    VI_ATTR_MODEL_CODE, 
		       		(ViPAttrState)(&modelCode)); 
				if( VI_SUCCESS > errStatus)
				{	
					/* Errors: VI_ERROR_NSUP_ATTR */
					/* Note: this should never happen 
					 *   with a VXI instrument
					 */

					/* ignore any errors in PREFIX_close */
					ri4152a_close( *vi);
                                        *vi=VI_NULL;
					ri4152a_LOG_STATUS( *vi, NULL, errStatus);
				}

				if( ((manfId != ri4152a_MANF_ID) || 
				    (modelCode != ri4152a_MODEL_CODE)) &&
				    ((manfId != hpe1412_MANF_ID) ||
				    (modelCode != hpe1412_MODEL_CODE))
				  ) {
					/* ignore any errors in PREFIX_close */
					ri4152a_close( *vi);
                                        *vi = VI_NULL;

					ri4152a_LOG_STATUS( *vi, NULL,
					  VI_ERROR_FAIL_ID_QUERY);
				}

				break;

			default:
				/* ignore any errors in PREFIX_close */
				ri4152a_close( *vi);
				*vi = VI_NULL;
				ri4152a_LOG_STATUS( *vi, NULL,
				  ri4152a_INSTR_ERROR_UNEXPECTED);

		}	

	} /* if - id_query */

	ri4152a_LOG_STATUS( *vi, thisPtr, VI_SUCCESS);
}


/****************************************************************************
ri4152a_close
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_close (
  ViSession vi )
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;
	ViSession defRM;

	errStatus = viGetAttribute( 
	       vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
	if( VI_SUCCESS > errStatus)
	{
		/* Errors: VI_ERROR_INV_SESSION	*/
		ri4152a_LOG_STATUS( vi, NULL, errStatus);
	}
	ri4152a_DEBUG_CHK_THIS( vi, thisPtr);

	/* retrieve Resource Management session */
	defRM = thisPtr->defRMSession;

	/* free memory */
	if( thisPtr)		
		/* make sure there is something to free */
		free( thisPtr);
	

	/* close the vi and RM sessions */
        return viClose( defRM);
}


/****************************************************************************
ri4152a_reset
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_reset (
  ViSession vi )
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;

	errStatus = viGetAttribute( 
	       vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
	if( VI_SUCCESS > errStatus)
        /* Errors: VI_ERROR_INV_SESSION	*/
		ri4152a_LOG_STATUS( vi, NULL, errStatus);
	   
	ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
	ri4152a_CDE_INIT( "ri4152a_reset");

	errStatus = viPrintf( vi, "*RST\n");
	if( VI_SUCCESS > errStatus) ri4152a_LOG_STATUS( vi, thisPtr, errStatus);

	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}


/****************************************************************************
ri4152a_self_test
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | test_result                                       ViPInt16    OUT
  |   ---------------------------------------------------------------------
  |  | Numeric result from self-test operation
  |  |
  |  | 0 = no error ( test passed)
  |  | 1 = failure
   ------------------------------------------------------------------------
  | test_message                                      ViChar _VI_FAR []OUT
  |   ---------------------------------------------------------------------
  |  | Self-test status message.  This is limited to 256 characters.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_self_test (
  ViSession vi,
  ViPInt16 test_result,
  ViChar _VI_FAR test_message[])
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;

	/* initialize output parameters */
	*test_result = -1; 
	test_message[0] = 0; 

	errStatus = viGetAttribute( 
		vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
	if( VI_SUCCESS > errStatus)
	{
		/* Errors: VI_ERROR_INV_SESSION	*/
		ri4152a_LOG_STATUS( vi, NULL, errStatus);
	}
	ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
	ri4152a_CDE_INIT( "ri4152a_self_test");

	thisPtr->blockSrqIO = VI_TRUE;
	errStatus = viPrintf( vi, "*TST?\n");
	if( VI_SUCCESS > errStatus)
	{	
		
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	Delay (30.0);
	
	errStatus = viScanf( vi, "%hd%*t", test_result);
	if( VI_SUCCESS > errStatus) {
		*test_result = -1; 
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	switch( *test_result) {
           case 0:
              sprintf(test_message, ri4152a_MSG_SELF_TEST_PASSED);
              break;

           default:
              sprintf(test_message, "Self test failed; use ri4152a_error_query to get specific messages about failures.");
              break;
    }
	
	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}

/****************************************************************************
ri4152a_error_query
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | error                                             ViPInt32    OUT
  |   ---------------------------------------------------------------------
  |  | Instruments error code
   ------------------------------------------------------------------------
  | error_message                                     ViChar _VI_FAR []OUT
  |   ---------------------------------------------------------------------
  |  | Instrument's error message.  This is limited to 256 characters.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_error_query (
  ViSession vi,
  ViPInt32 error,
  ViChar _VI_FAR error_message[])
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;

	/* initialize output parameters */
	*error = -1; 
	error_message[0] = 0; 

	errStatus = viGetAttribute( 
		vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
	if( VI_SUCCESS > errStatus)
	{
		/* Errors: VI_ERROR_INV_SESSION	*/
		ri4152a_LOG_STATUS( vi, NULL, errStatus);
	}
	ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
	ri4152a_CDE_INIT( "ri4152a_error_query");

	thisPtr->blockSrqIO = VI_TRUE;
        errStatus = viPrintf( vi, "SYST:ERR?\n");
	if( VI_SUCCESS > errStatus)
	{
		
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	/* get the error number */
	errStatus = viScanf( vi, "%ld,%t", error, error_message);
	/* check for error during the scan */
        if( VI_SUCCESS > errStatus)
	{
		*error = -1; 
		error_message[0] = 0; 
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	/* get rid of extra LF at the end of the error_message */
	error_message[strlen(error_message)-1] = 0;

	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}

/****************************************************************************
ri4152a_error_message
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init().
  |  | This may be VI_NULL.
   ------------------------------------------------------------------------
  | error                                             ViStatus    IN
  |   ---------------------------------------------------------------------
  |  | The error return value from an instrument driver function
   ------------------------------------------------------------------------
  | message                                           ViChar _VI_FAR []OUT
  |   ---------------------------------------------------------------------
  |  | Error message string.  This is limited to 256 characters.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_error_message (
  ViSession vi,
  ViStatus error,
  ViChar _VI_FAR message[])
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;  
	ViInt32 idx;

	/* initialize output parameters */
	message[0] = 0;

	thisPtr = NULL;

	/* try to find a thisPtr */
	if( VI_NULL != vi)
	{
		errStatus = viGetAttribute( 
			vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
		if( VI_SUCCESS > errStatus)
		{
			/* Errors: VI_ERROR_INV_SESSION	*/
			strcpy( message, ri4152a_MSG_INVALID_VI);
			ri4152a_LOG_STATUS( vi, NULL, errStatus);
		}
		ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
	} 

	if( VI_SUCCESS == error)
	{
		sprintf( message, ri4152a_MSG_NO_ERRORS);
		ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
	}

	/* return the static error message */
	for(idx=0; 
	    idx < (sizeof instrErrMsgTable / 
	           sizeof( struct instrErrStruct));
	    idx++)
	{
		/* check for a matching error number */
		if( instrErrMsgTable[idx].errStatus == error)
		{
			if( (thisPtr) &&
			    (thisPtr->errNumber == error))
			{
				/* context dependent error
				 * message is available.
				 */
				sprintf( message,
				  "%s " ri4152a_MSG_IN_FUNCTION " %s() %s",
		          	  instrErrMsgTable[idx].errMessage,
				  thisPtr->errFuncName,
				  thisPtr->errMessage);
			}
			else
			{
				/* No context dependent eror 
				 * message available so copy 
				 * the static error message
				 */
				strcpy( message,
		          	  instrErrMsgTable[idx].errMessage);

			}
            
			ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
		}
	}


	/* try VTL/VISA to see if it exists there 
	 *
	 * At this point vi is either VALID or VI_NULL
	 */
	errStatus = viStatusDesc(vi, error, message);
	if( VI_SUCCESS == errStatus)
	{
		/* check for a context dependent error message */
		if( (thisPtr) &&
		    (thisPtr->errNumber == error))
		{
			/* context dependent error
			 * message is available.
			 */
			strcat( message, " ");
			strcat( message, ri4152a_MSG_IN_FUNCTION);
			strcat( message, " ");
			strcat( message, thisPtr->errFuncName);
			strcat( message, "() ");
			strcat( message, thisPtr->errMessage);
		}

		/* VTL found an error message, so return success */
		ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
	}

	/* if we have a VI_NULL, then we need to return a error message */
	if( VI_NULL == vi)
	{
		strcpy(message, ri4152a_MSG_VI_OPEN_ERR);
		ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
	} 

	/* user passed in a invalid status */
	sprintf( message, 
	  ri4152a_MSG_INVALID_STATUS
	  "  %ld"
	  ri4152a_MSG_INVALID_STATUS_VALUE
	  , (long)error );
	
	ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
}


/****************************************************************************
ri4152a_revision_query
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession      IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | driver_rev                                ViChar _VI_FAR []      OUT
  |   ---------------------------------------------------------------------
  |  | Instrument driver revision.  This is limited to 256 characters.
   ------------------------------------------------------------------------
  | instr_rev                                 ViChar _VI_FAR []      OUT
  |   ---------------------------------------------------------------------
  |  | Instrument firmware revision.  This is limited to 256 characters.

*****************************************************************************/

ViStatus _VI_FUNC ri4152a_revision_query (
  ViSession vi,
  ViChar _VI_FAR driver_rev[],
  ViChar _VI_FAR instr_rev[])
{
	struct ri4152a_globals *thisPtr;
	ViStatus errStatus;
        char temp_str[256];		/* temp hold for instr rev string */
        char *last_comma;		/* last comma in *IDN string */

	/* initialize output parameters */
        driver_rev[0] = 0; 
	instr_rev[0] = 0; 

	if( errStatus = viGetAttribute( 
		vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr) )
	{
		/* Errors: VI_ERROR_INV_SESSION	*/
		ri4152a_LOG_STATUS( vi, NULL, errStatus);
	}
	ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
	ri4152a_CDE_INIT( "ri4152a_revision_query");

	sprintf( driver_rev, "%s", ri4152a_REV_CODE);

	thisPtr->blockSrqIO = VI_TRUE;

	if( errStatus = viPrintf( vi, "*IDN?\n"))
	{
        	driver_rev[0] = 0; 
		 
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus); 
	}

	if( errStatus = viScanf( vi, "%t", temp_str))
	{
        	driver_rev[0] = 0; 
		instr_rev[0] = 0; 
		 
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus); 
	}
        
        last_comma = strrchr(temp_str,',');
      /* error and exit if last comma not found */
        if (!last_comma) 
        {
           instr_rev[0] = 0;
	   ri4152a_CDE_MESSAGE("no last comma found in IDN string" ); 
	   ri4152a_LOG_STATUS( vi, thisPtr, ri4152a_INSTR_ERROR_UNEXPECTED); 
        }

        strcpy(instr_rev, last_comma+1);
	
	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS); 
}


/****************************************************************************
ri4152a_timeOut
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | timeOut                                           ViInt32     IN
  |   ---------------------------------------------------------------------
  |  | This value sets the I/O timeout for all functions in
  |  | the driver. It is specified in milliseconds.

*****************************************************************************/
/* ----------------------------------------------------------------------- */
/* Purpose:  Changes the timeout value of the instrument.  Input is in     */
/*           milliseconds.                                                 */
/* ----------------------------------------------------------------------- */
ViStatus _VI_FUNC ri4152a_timeOut (ViSession vi, ViInt32 timeOut)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;
   

   errStatus = viGetAttribute( 
	      vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus)
   {
	/* Errors: VI_ERROR_INV_SESSION	*/
	ri4152a_LOG_STATUS( vi, NULL, errStatus);
   }
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_timeOut" );

   ri4152a_CHK_LONG_RANGE( timeOut, 1, 2147483647, VI_ERROR_PARAMETER2 );

   errStatus = viSetAttribute(vi, VI_ATTR_TMO_VALUE, timeOut);
   if ( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
   }

   ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_timeOut_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | pTimeOut                                          ViPInt32    OUT
  |   ---------------------------------------------------------------------
  |  | This is the minimum timeout period that the driver
  |  | can be set to. It is specified in milliseconds.

*****************************************************************************/
/* ----------------------------------------------------------------------- */
/* Purpose:  Returns the current setting of the timeout value of the       */
/*           instrument in milliseconds.                                   */
/* ----------------------------------------------------------------------- */
ViStatus _VI_FUNC ri4152a_timeOut_Q (ViSession vi, ViPInt32 timeOut)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS( vi, NULL, errStatus );
   }
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_timeOut_Q" );

   errStatus = viGetAttribute(vi, VI_ATTR_TMO_VALUE, timeOut );
   if( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
   }

   ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_errorQueryDetect
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | errorQueryDetect                                  ViBoolean   IN
  |   ---------------------------------------------------------------------
  |  | Boolean which enables (VI_TRUE) or disables (VI_FALSE)
  |  | automatic instrument error querying.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_errorQueryDetect( 
  ViSession vi, 
  ViBoolean errDetect)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus ) 
   { 
	ri4152a_LOG_STATUS( vi, NULL, errStatus ); 
   }

   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_errorQueryDetect" );

   ri4152a_CHK_BOOLEAN( errDetect, VI_ERROR_PARAMETER2 );

   thisPtr->errQueryDetect = errDetect;

   ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_errorQueryDetect_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | pErrDetect                                        ViPBoolean  OUT
  |   ---------------------------------------------------------------------
  |  | Boolean indicating if automatic instrument error
  |  | querying is performed.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_errorQueryDetect_Q( 
  ViSession vi, 
  ViPBoolean pErrDetect)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus ) 
   { 
	ri4152a_LOG_STATUS( vi, NULL, errStatus ); 
   }

   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_errorQueryDetect_Q" );

   *pErrDetect = thisPtr->errQueryDetect;

   ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_dcl
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_dcl( 
  ViSession vi)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus ) 
   { 
	ri4152a_LOG_STATUS( vi, NULL, errStatus ); 
   }

   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_dcl" );

   errStatus = viClear(vi);
   if( VI_SUCCESS > errStatus) { ri4152a_LOG_STATUS( vi, thisPtr, errStatus ); }	

   ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_opc
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
*****************************************************************************/
ViStatus _VI_FUNC ri4152a_opc(ViSession vi)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus) ri4152a_LOG_STATUS(vi, NULL, errStatus );
 
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );		  
   ri4152a_CDE_INIT( "ri4152a_opc" );

   errStatus = viPrintf(vi,"*OPC\n");
   if( VI_SUCCESS > errStatus) ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
   ri4152a_LOG_STATUS(vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_opc_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | opc                                               ViPInt16    OUT
  |   ---------------------------------------------------------------------
  |  | returns a 1 when all pending instruments commands are completed

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_opc_Q (ViSession vi, ViPInt16 opc)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, NULL, errStatus );
   }
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_opc_Q" );

   thisPtr->blockSrqIO = VI_TRUE;
   errStatus = viPrintf(vi,"*OPC?\n");
   if( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
   }

   errStatus = viScanf(vi, "%hd%*t", opc);
   if( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
   }

   ri4152a_LOG_STATUS(vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_wai
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_wai (ViSession vi)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if ( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, NULL, errStatus );
   }
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_wai" );

   errStatus = viPrintf(vi,"*WAI\n");
   if (VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
   }

   ri4152a_LOG_STATUS(vi, thisPtr, VI_SUCCESS );
}

/****************************************************************************
ri4152a_trg
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_trg (ViSession vi)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if ( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, NULL, errStatus );
   }
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_trg" );

   errStatus = viPrintf(vi,"*TRG\n");
   if ( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
   }

   ri4152a_LOG_STATUS(vi, thisPtr, VI_SUCCESS );
}


/****************************************************************************
ri4152a_readStatusByte_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | statusByte                                        ViPInt16    OUT
  |   ---------------------------------------------------------------------
  |  | returns the contents of the status byte

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_readStatusByte_Q(ViSession vi, ViPInt16 statusByte)
{
   ViStatus errStatus = 0;
   struct ri4152a_globals *thisPtr;
   ViUInt16 stb;

   errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   if ( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, NULL, errStatus );
   }
   ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
   ri4152a_CDE_INIT( "ri4152a_readStatusByte_Q" );

   errStatus = viReadSTB(vi,&stb);
   if( VI_SUCCESS > errStatus)
   {
      ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
   }

   *statusByte = (ViInt16)stb;

   ri4152a_LOG_STATUS(vi, thisPtr, VI_SUCCESS );
}

/******************************************************
 * INSTR_DEVELOPER: Take out what you don't need 
 ******************************************************
 */


ViInt32 ri4152a_statusHap[ri4152a_MAX_STAT_HAP] = {
ri4152a_USER_ERROR_HANDLER,

ri4152a_QUES_VOLT,
ri4152a_QUES_CURR,
ri4152a_QUES_RES,
ri4152a_QUES_LIM_LO,
ri4152a_QUES_LIM_HI,

ri4152a_ESR_OPC,
ri4152a_ESR_QUERY_ERROR,
ri4152a_ESR_DEVICE_DEPENDENT_ERROR,
ri4152a_ESR_EXECUTION_ERROR,
ri4152a_ESR_COMMAND_ERROR,
};


/* Assumes we have driver copies of the event register.  This is needed
 * because in IEEE 488.2, the event register are cleared after they are 
 * read.  Since the event register contains several events, we need to
 * keep this information around to pass back to the user.
 */


struct ri4152a_statusAccess {
ViInt32 registerIdx; 
ViString condQry;
ViString eventQry;
ViString enableCmd;
};

const struct ri4152a_statusAccess ri4152a_accessInfo[ri4152a_MAX_STAT_REG] = {
{0, 	"",	 		"*STB?",		"*SRE"},
{400, 	"STAT:QUES:COND?",	"STAT:QUES:EVEN?",	"STAT:QUES:ENAB"},
{600,	"",			"*ESR?",			"*ESE"},
{800, 	"STAT:OPER:COND?",	"STAT:OPER:EVEN?",	"STAT:OPER:ENAB"}
};

/* this will return the index associated with the happening */
ViBoolean ri4152a_findHappeningIdx(
  ViInt32 happening,
  ViPInt32 pIdx)
{
	/* Note: this is a linear search, for faster access this
	 * could be done as a binary search since the data is arrange
	 * in order numerically.
	 */
	for( *pIdx=0; *pIdx<ri4152a_MAX_STAT_HAP; *pIdx = *pIdx + 1)
	{
		if( ri4152a_statusHap[*pIdx] == happening)
		{
			return VI_TRUE;
		}	
	}

	return VI_FALSE;
}

/* this will return the index that corresponds with regNum */
static ViBoolean ri4152a_findAccessIdx(
  ViInt32 regNum,
  ViPInt32 pIdx )
{
	for(*pIdx=0; *pIdx<ri4152a_MAX_STAT_REG; *pIdx = *pIdx + 1)
	{
		if( regNum == ri4152a_accessInfo[*pIdx].registerIdx)
		{	
			return VI_TRUE;
		}
	}
	return VI_FALSE;
}

static ViStatus ri4152a_readAllEvents( 
  ViSession vi)
{
	ViInt32 idx;
	ViStatus errStatus;
	ViInt32 eventResp;
        struct ri4152a_globals *thisPtr;

	errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
   	if( VI_SUCCESS > errStatus) 
   	{
   	   ri4152a_LOG_STATUS( vi, NULL, errStatus );
   	}
   	ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
        

	/* read all events and update driver events */
	for(idx=0; idx<ri4152a_MAX_STAT_REG; idx++)
	{
		/* if there is an event query */
		if( 0 != strlen(ri4152a_accessInfo[idx].eventQry))
		{
			/* okay - query the instrument for event */
			thisPtr->blockSrqIO = VI_TRUE;

			errStatus = viPrintf(vi,
			  "%s\n", ri4152a_accessInfo[idx].eventQry);
			if( VI_SUCCESS > errStatus)
			{
				return errStatus;
			}

			/* get the response */
			errStatus = viScanf(vi, "%ld%*t", &eventResp);
			if( VI_SUCCESS > errStatus )
			{
				return errStatus;
			}

			/* update the driver events copy of instr events */
			thisPtr->driverEventArray[idx] = 	
			  thisPtr->driverEventArray[idx] | eventResp;

		} /* if - an event query exists */

	} /* for - all status registers */

	return VI_SUCCESS;

} /* ri4152a_readAllEvents() */


/* this recursive routine traverse the IEEE 488.2 status structures and
 * processes events.
 */
static void ri4152a_srqTraverse(
  ViSession vi,
  ViInt32 eventReg)
{
        struct ri4152a_globals *thisPtr;
	ViInt32 accessIdx;
	ViInt32 hapIdx;
	ViUInt16 Status;
	ViInt32 eventVal;
	ViInt32 happening;
	ViInt32 bitPos;

        if(viGetAttribute( vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr) )
	{
		/* error couldn't get attribute */
		return;  
	}

	if( VI_FALSE == ri4152a_findAccessIdx( eventReg, &accessIdx))
	{
		/* couldn't find register num 
		 *   don't know what to do so leave 
		 */
		return;
	}

	if( 0 == eventReg )
	{
		/* need to read status byte through VTL instead of SCPI */
		if( viReadSTB( vi, &Status))
		{
			/* an error occured */
			return;
		}
		eventVal = (ViInt32)Status;
	}
	else
	{
		/* find the event in the table */
		if( 0 != strlen(ri4152a_accessInfo[accessIdx].eventQry))
		{
			/* query instrument to find what event(s) occured. */
			if( viPrintf(vi,"%s\n", 
		              ri4152a_accessInfo[accessIdx].eventQry))
			{
				/* an error occured */
				return;
			}

			if( viScanf(vi,"%ld%*t\n", &eventVal))
			{
				/* an error occured */
				return;
			}
		}
		else
		{
			/* error - no events for this status register */
			return;
		}
	} /* else - not status byte */

	/* update driver events */
	thisPtr->driverEventArray[accessIdx] = 
	  thisPtr->driverEventArray[accessIdx] | eventVal;

	for( bitPos = 0; bitPos < 16; bitPos++)
	{
		/* check for an event occurance */
		if( eventVal & (1 << bitPos) )
		{
			/* find happening index */
			happening = eventReg + bitPos + 1;
			if( VI_TRUE == 
			       ri4152a_findHappeningIdx( happening, &hapIdx) )
			{
				/* does event have a handler enabled? */
				if( thisPtr->eventHandlerArray[hapIdx].eventHandler)
				{
					/* call the users handler */
			    	thisPtr->eventHandlerArray[hapIdx].eventHandler(
			    	  vi, happening, 
			    	  thisPtr->eventHandlerArray[hapIdx].userData);
				}				
			}

		} /* if - event occured */

		/* check for more status registers */
		if( VI_TRUE == ri4152a_findAccessIdx( happening*100, &accessIdx))
		{
			/* need to traverse these other registers */
			ri4152a_srqTraverse( vi, happening*100);
		}

	} /* for - all bits in the event */
}

/* this is the SRQ event handler */
ViStatus _VI_FUNCH ri4152a_srqHdlr(
  ViSession vi,
  ViEventType eventType,
  ViEvent event,
  ViAddr userData)
{
	/* the only reason we should get called is for a Service Request */	

	/* for VTL 3.0 we always need to return VI_SUCCESS, therefore
	 * we will ignore any VTL errors at this level.
	 */

        struct ri4152a_globals *thisPtr;

        if(VI_SUCCESS > 
	  viGetAttribute( vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr) )
	{
		/* error couldn't get attribute, 
		 * but we lie and return VI_SUCCESS */
		return VI_SUCCESS;  
	}

	/* check if it is okay to do I/O in SRQ handler */
	if( VI_TRUE == thisPtr->blockSrqIO)
	{
	    /* not okay to do I/O so just update srq count */
	    thisPtr->countSrqIO = thisPtr->countSrqIO + 1;
     	    return VI_SUCCESS;  
	}

	/* suspend event processing for now */
	viEnableEvent( vi, VI_EVENT_SERVICE_REQ, VI_SUSPEND_HNDLR, VI_NULL);

	/* recursively traverse status byte */
	ri4152a_srqTraverse( vi, 0);

	/* re-enable event processing */
        viEnableEvent( vi, VI_EVENT_SERVICE_REQ, VI_HNDLR, VI_NULL);

	/* always return VI_SUCCESS */
	return VI_SUCCESS;
} 



/****************************************************************************
ri4152a_statCond_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | happening                                         ViInt32     IN
  |   ---------------------------------------------------------------------
  |  | The following conditions can occur on the counter.
  |  | Conditions are transient in nature, and this function reports
  |  | the current setting of the condition. A 1 is returned if the
  |  | condition is currently true, and 0 is returned if the condition
  |  | is currently false.
  |  |
   ------------------------------------------------------------------------
  | pCondition                                        ViPBoolean  OUT
  |   ---------------------------------------------------------------------
  |  | VI_TRUE  = condition is currently set
  |  | VI_FALSE = condition is currently not set

*****************************************************************************/

ViStatus _VI_FUNC ri4152a_statCond_Q(
  ViSession	vi,
  ViInt32	happening,
  ViPBoolean	pCondition 
)
{
        struct ri4152a_globals *thisPtr;
        ViStatus errStatus;
	ViInt32 hapIdx;
	ViInt32 accessIdx;
	ViInt32 cond_q;
	ViInt32 regNum;
	ViInt32 bitNum;
	char errMsg[256];

        /* initialize output parameters */
	*pCondition = VI_FALSE;

	errStatus = viGetAttribute(
                vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
        if( VI_SUCCESS > errStatus)
        {
                /* Errors: VI_ERROR_INV_SESSION */
                ri4152a_LOG_STATUS( vi, NULL, errStatus);
        }
        ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
        ri4152a_CDE_INIT( "ri4152a_statCond_Q");

	/* check validity of happening */
	if( (ri4152a_USER_ERROR_HANDLER ==  happening) ||
            (VI_FALSE == ri4152a_findHappeningIdx( happening, &hapIdx) ))
	{
		sprintf(errMsg,"%hd " ri4152a_MSG_INVALID_HAPPENING, happening);
		ri4152a_CDE_MESSAGE( errMsg);
		
		ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
	}

	regNum = happening / 100;
	bitNum = happening % 100;

	/* get access index */
	if( VI_FALSE == ri4152a_findAccessIdx( regNum * 100, &accessIdx))
	{
		
		ri4152a_LOG_STATUS( vi, thisPtr, ri4152a_INSTR_ERROR_UNEXPECTED);
	}

	if( 0 == strlen(ri4152a_accessInfo[accessIdx].condQry))
	{
		char errMsg[256];

		/* unable to query the condition */
		sprintf(errMsg, ri4152a_MSG_CONDITION
                        "%hd" ri4152a_MSG_NOT_QUERIABLE, happening);
		ri4152a_CDE_MESSAGE(errMsg); 
		
		ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
	}
	else
	{

                thisPtr->blockSrqIO = VI_TRUE;

		/* okay - query the instrument for condition */
		errStatus = viPrintf(vi,
		  "%s\n", ri4152a_accessInfo[accessIdx].condQry);
		if( 0 > errStatus)
		{
			
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

		/* get the condition response  */
		errStatus = viScanf(vi, "%ld%*t", &cond_q);
		if( 0 > errStatus )
		{
			
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

		/* check appropriate condition bit */
		if( cond_q & (1 << (bitNum-1)) )
		{
			*pCondition = VI_TRUE;
		}
		else
		{
			*pCondition = VI_FALSE;
		}
	}

	
	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}	


/****************************************************************************
ri4152a_statEven_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | happening                                         ViInt32     IN
  |   ---------------------------------------------------------------------
  |  | Happenings refer to something that happens. These can refer
  |  | to conditions or events. All of the conditions listed in the
  |  | ri4152a_statCond_Q() function will be detected as events as
  |  | well.   An event may be registered when a condition changes
  |  | state from  VI_FALSE to VI_TRUE.
  |  |
   ------------------------------------------------------------------------
  | pEvent                                            ViPBoolean  OUT
  |   ---------------------------------------------------------------------
  |  | VI_TRUE  = event occured sometime between event readings
  |  | VI_FALSE = the event did not occur between event readings

*****************************************************************************/

ViStatus _VI_FUNC ri4152a_statEven_Q(
  ViSession 	vi,
  ViInt32	happening,
  ViPBoolean	pEvent
)
{
        struct ri4152a_globals *thisPtr;
        ViStatus errStatus;
	ViInt32 hapIdx;
	ViInt32 accessIdx;
	ViInt32 event_q;
	ViInt32 regNum;
	ViInt32 bitNum;
	ViInt32 mask;
	char errMsg[256];

        /* initialize output parameters */
	*pEvent = VI_FALSE;

	errStatus = viGetAttribute( vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
        if( VI_SUCCESS > errStatus)
        {
                /* Errors: VI_ERROR_INV_SESSION */
                ri4152a_LOG_STATUS( vi, NULL, errStatus);
        }
        ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
        ri4152a_CDE_INIT( "ri4152a_statEven_Q");

	/* check validity of happening */
	if( (ri4152a_USER_ERROR_HANDLER == happening) ||
            (VI_FALSE == ri4152a_findHappeningIdx( happening, &hapIdx) ))
	{
		sprintf(errMsg,"%hd " ri4152a_MSG_INVALID_HAPPENING, happening);
		ri4152a_CDE_MESSAGE( errMsg);
		
		ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
	}

	regNum = happening / 100;
	bitNum = happening % 100;

	/* get access index */
	if( VI_FALSE == ri4152a_findAccessIdx( regNum * 100, &accessIdx))
	{
		
		ri4152a_LOG_STATUS( vi, thisPtr, ri4152a_INSTR_ERROR_UNEXPECTED);
	}

	/* see if we can query the event from the instrument */
	if( 0 == strlen(ri4152a_accessInfo[accessIdx].eventQry))
	{
		char errMsg[256];

		/* unable to query the event */
		sprintf(errMsg, ri4152a_MSG_EVENT
		  "%hd" ri4152a_MSG_NOT_QUERIABLE, happening);
		ri4152a_CDE_MESSAGE(errMsg); 
		
		ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
	}
	else
	{
                thisPtr->blockSrqIO = VI_TRUE;

		/* okay - query the instrument for event */
		errStatus = viPrintf(vi,
		  "%s\n", ri4152a_accessInfo[accessIdx].eventQry);
		if( 0 > errStatus)
		{
			
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

		/* get the response */
		errStatus = viScanf(vi, "%ld%*t", &event_q);
		if( 0 > errStatus )
		{
			
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

		/* update the driver events copy of instrument events */
		thisPtr->driverEventArray[accessIdx] = 	
		  thisPtr->driverEventArray[accessIdx] | event_q;

		/* check state of event bit */
		mask = 1 << (bitNum -1);
		if( thisPtr->driverEventArray[accessIdx] & mask )
		{
			*pEvent = VI_TRUE;

			/* clear event bit in driver events */
			thisPtr->driverEventArray[accessIdx] = 	
		 	  thisPtr->driverEventArray[accessIdx] & (~mask);
		}
		else
		{
			*pEvent = VI_FALSE;

			/* event bit in driver event array
			 * is already cleared 
			 */
		}
	}

	
	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}	


/****************************************************************************
ri4152a_statEvenHdlr
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | happening                                         ViInt32     IN
  |   ---------------------------------------------------------------------
  |  | Happenings refer to something that happens. These can refer to
  |  | conditions or events. Happenings are enumerated as ViInt32
  |  | numbers.  Only events can have happenings associated with it.
  |  |
   ------------------------------------------------------------------------
  | eventHandler                           ri4152a_InstrEventHandler  IN
  |   ---------------------------------------------------------------------
  |  | This is either NULL or a pointer to the user specified
  |  | event handler.  A NULL disables the event handler.
  |  |
  |  | An event handler has the following prototype:
  |  |
  |  | typedef void (_VI_FUNCH _VI_PTR  ri4152a_InstrEventHandler)(
  |  | ViSession vi,
  |  | ViInt32 happening,
  |  | ViAddr userData
  |  | );
   ------------------------------------------------------------------------
  | userData                                          ViAddr      IN
  |   ---------------------------------------------------------------------
  |  | This is a pointer that is passed to the handler when the
  |  | specified event occurs.  This can be used by the programmer
  |  | to pass additional information to the handler.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_statEvenHdlr(
  ViSession vi,
  ViInt32 happening,
  ri4152a_InstrEventHandler eventHandler,
  ViAddr userData
)
{
        struct ri4152a_globals *thisPtr;
        ViStatus errStatus;
	ViInt32 hapIdx;
	ViInt32 accessIdx;
	ViInt32 propagate;
	ViInt32 regNum;
	ViInt32 bitNum;
	ViInt32 enableMask;
	char errMsg[80];

        /* initialize output parameters */

	errStatus = viGetAttribute(
                vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
        if( VI_SUCCESS > errStatus)
        {
                /* Errors: VI_ERROR_INV_SESSION */
                ri4152a_LOG_STATUS( vi, NULL, errStatus);
        }
        ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
        ri4152a_CDE_INIT( "ri4152a_statEvenHdlr");

	if( ri4152a_USER_ERROR_HANDLER == happening)
	{
		/* store handler and user data */
		thisPtr->eventHandlerArray[
                   ri4152a_USER_ERROR_HANDLER_IDX].eventHandler = eventHandler;
		thisPtr->eventHandlerArray[
                   ri4152a_USER_ERROR_HANDLER_IDX].userData = userData;
		ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
	}

	/* check validity of happening */
	if( VI_FALSE == ri4152a_findHappeningIdx( happening, &hapIdx) )
	{

		sprintf(errMsg,"%ld " ri4152a_MSG_INVALID_HAPPENING, 
		  (long)happening);
		ri4152a_CDE_MESSAGE( errMsg);
		
		ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
	}

	regNum = happening / 100;
	bitNum = happening % 100;

	/* suspend SRQ events - if handler is installed */
	if( 0 > thisPtr->numEventHandlers)
	{
		errStatus = viEnableEvent( vi,VI_EVENT_SERVICE_REQ ,
		  VI_SUSPEND_HNDLR, VI_NULL);
		if( errStatus)
		{
                	
                	ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}
	}	

	/* see if we want to enable or disable the handler */
	if( NULL != eventHandler)
	{
		/* see if event handler already exists */		
		if( NULL == thisPtr->eventHandlerArray[hapIdx].eventHandler )
		{
			/* okay - no event handler */
			thisPtr->numEventHandlers =
			  thisPtr->numEventHandlers + 1;
		}
		else
		{
			/* reenable SRQ events - if handler is installed */
			if( 0 > thisPtr->numEventHandlers)
			{
				errStatus = viEnableEvent( vi,
				  VI_EVENT_SERVICE_REQ, VI_HNDLR, VI_NULL);
				if( errStatus)
				{
               			 	
                			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
				}
			}	

			/* error - event handler already exists */
			sprintf(errMsg, 
			  ri4152a_MSG_EVENT_HDLR_INSTALLED
			  " %ld.  "
			  ri4152a_MSG_EVENT_HDLR_INST2, (long)happening);
			
			ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
			
		}

		/* store handler and user data */
		thisPtr->eventHandlerArray[hapIdx].eventHandler = eventHandler;
		thisPtr->eventHandlerArray[hapIdx].userData = userData;

		/* do we need to install the event handler? */
		if( 1 == thisPtr->numEventHandlers)
		{
			/* install the event handler */
			errStatus = viInstallHandler(vi, 
			  VI_EVENT_SERVICE_REQ, ri4152a_srqHdlr, VI_NULL);
			if( errStatus)
			{
               			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
			}

			/* enable events */
			errStatus = viEnableEvent( vi,
			  VI_EVENT_SERVICE_REQ, VI_HNDLR, VI_NULL);
			if( errStatus)
			{
                		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
			}
		}

	} /* if - install event handler */
	else
	{
		/* see if event handler already exists */		
		if( NULL != thisPtr->eventHandlerArray[hapIdx].eventHandler )
		{
			/* okay - no event handler */
			thisPtr->numEventHandlers =
			  thisPtr->numEventHandlers - 1;
		}

		/* clear handler and user data */
		thisPtr->eventHandlerArray[hapIdx].eventHandler = NULL;
		thisPtr->eventHandlerArray[hapIdx].userData = NULL;

		/* do we need to uninstall the event handler? */
		if( 0 == thisPtr->numEventHandlers)
		{
			/* disable SRQ events */
			errStatus = viDisableEvent( vi,
			  VI_EVENT_SERVICE_REQ ,VI_HNDLR);
			if( VI_SUCCESS > errStatus)
			{
               			
     		          	ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
			}

			/* flush any pending SRQ events */
			errStatus = viDiscardEvents( vi,
			  VI_EVENT_SERVICE_REQ ,VI_SUSPEND_HNDLR);
			if( VI_SUCCESS > errStatus)
			{
               			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
			}

			/* uninstall the event handler */
			errStatus = viUninstallHandler(vi, 
			  VI_EVENT_SERVICE_REQ, ri4152a_srqHdlr, VI_NULL);
			if( VI_SUCCESS > errStatus && 
			    VI_ERROR_INV_HNDLR_REF != errStatus)
			{
               			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
			}

		}

		/* remove all stale events */
		errStatus = ri4152a_readAllEvents( vi);
		if( VI_SUCCESS > errStatus)
		{
               		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

	} /* else - remove event handler */

	/* enable event bits to propagate through the IEEE 488.2 
	 * status system and eventually assert SRQ.
	 */
	propagate = happening;
	while( propagate > 0)
	{
		regNum = propagate / 100;
		bitNum = propagate % 100;

		/* get access index */
		if( VI_FALSE == ri4152a_findAccessIdx( regNum * 100, &accessIdx))
		{
			ri4152a_CDE_MESSAGE( "ri4152a_findAccessIdx failed");
			ri4152a_LOG_STATUS( vi, thisPtr, ri4152a_INSTR_ERROR_UNEXPECTED);
		}

		/* query enable value from instrument */
                thisPtr->blockSrqIO = VI_TRUE;

		errStatus = viPrintf( vi, "%s?\n",
		   ri4152a_accessInfo[accessIdx].enableCmd);
		if( VI_SUCCESS > errStatus)
		{
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}
	
		/* read back the response */
		errStatus = viScanf( vi, "%ld%*t", &enableMask);
		if( VI_SUCCESS > errStatus)
		{
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

		/* enable/disable  the corresponding event bit */
		if( NULL != eventHandler)
		{
			/* enable event bit */
			enableMask = enableMask | (1 << (bitNum - 1));
		}	
		else
		{
			/* disable event bit */
			enableMask = enableMask & (~(1 << (bitNum - 1)));
		}

		/* write back the new enable mask */
		errStatus = viPrintf( vi, "%s %hd\n",
		   ri4152a_accessInfo[accessIdx].enableCmd,
		   enableMask);
		if( VI_SUCCESS > errStatus)
		{
			
			ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
		}

		if( NULL == eventHandler)
		{
			/* for disabling events, only disable the
			 * lowest level.
			 */
			propagate = 0;
		}
		else
		{
			propagate = propagate / 100;
		}

	} /* while - propagate */

	
	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}	



/****************************************************************************
ri4152a_statEvenHdlr_Q
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()
   ------------------------------------------------------------------------
  | happening                                         ViInt32 happeningIN
  |   ---------------------------------------------------------------------
  |  | Happenings refer to something that happens.  These can refer to
  |  | conditions or events.  Happenings are enumerated as ViInt32
  |  | numbers.
  |  |
   ------------------------------------------------------------------------
  | pEventHandler                     ri4152a_InstrPEventHandler      OUT
  |   ---------------------------------------------------------------------
  |  | This is the definition of a ri4152a_InstrPEventHandler:
  |  |
  |  | typedef void (_VI_PTR _VI_PTR ri4152a_InstrPEventHandler)(
  |  | ViSession vi,
  |  | ViInt32 happening,
  |  | ViAddr userData
  |  | );
   ------------------------------------------------------------------------
  | pUserData                                         ViPAddr     OUT
  |   ---------------------------------------------------------------------
  |  | This is a pointer to the userData that was registered
  |  | with the handler.

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_statEvenHdlr_Q(
  ViSession vi,
  ViInt32 happening,
  ri4152a_InstrPEventHandler pEventHandler,
  ViPAddr pUserData)
{
        struct ri4152a_globals *thisPtr;
        ViStatus errStatus;
	ViInt32 hapIdx;
	char errMsg[80];

        /* initialize output parameters */

	errStatus = viGetAttribute(
                vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
        if( VI_SUCCESS > errStatus)
        {
                /* Errors: VI_ERROR_INV_SESSION */
                ri4152a_LOG_STATUS( vi, NULL, errStatus);
        }
        ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
        ri4152a_CDE_INIT( "ri4152a_statEvenHdlr_Q");

	/* check validity of happening */
	if( ri4152a_USER_ERROR_HANDLER == happening)
	{
		hapIdx = ri4152a_USER_ERROR_HANDLER_IDX;
	}
	else if( VI_FALSE == ri4152a_findHappeningIdx( happening, &hapIdx) )
	{
		sprintf(errMsg,"%hd " ri4152a_MSG_INVALID_HAPPENING, happening);
		ri4152a_CDE_MESSAGE( errMsg);
		ri4152a_LOG_STATUS( vi, thisPtr, VI_ERROR_PARAMETER2);
	}

	*((void **)pEventHandler) = (void *)thisPtr->eventHandlerArray[hapIdx].eventHandler;
	*pUserData = thisPtr->eventHandlerArray[hapIdx].userData;

	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}	

/****************************************************************************
ri4152a_statEvenHdlrDelAll
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_statEvenHdlrDelAll(
  ViSession vi)
{
        struct ri4152a_globals *thisPtr;
        ViStatus errStatus;

        /* initialize output parameters */

	errStatus = viGetAttribute(
                vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
        if( VI_SUCCESS > errStatus)
        {
                /* Errors: VI_ERROR_INV_SESSION */
                ri4152a_LOG_STATUS( vi, NULL, errStatus);
        }
        ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
        ri4152a_CDE_INIT( "ri4152a_statEvenHdlrDelAll");

	/* disable SRQ events */
	errStatus = viDisableEvent( vi,VI_EVENT_SERVICE_REQ ,VI_HNDLR);
	if( VI_SUCCESS > errStatus)
	{
               	ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	/* flush any pending SRQ events */
	errStatus = viDiscardEvents( vi,VI_EVENT_SERVICE_REQ ,VI_SUSPEND_HNDLR);
	if( VI_SUCCESS > errStatus)
	{
               	ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	/* uninstall the event handler */
	errStatus = viUninstallHandler(vi, 
	  VI_EVENT_SERVICE_REQ, ri4152a_srqHdlr, VI_NULL);
	if( VI_SUCCESS > errStatus && VI_ERROR_INV_HNDLR_REF != errStatus)
	{
        	ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}	

/****************************************************************************
ri4152a_statEvenClr
*****************************************************************************
    Parameter Name                                       Type    Direction
   ------------------------------------------------------------------------
  | vi                                                ViSession   IN
  |   ---------------------------------------------------------------------
  |  | Instrument Handle returned from ri4152a_init()

*****************************************************************************/
ViStatus _VI_FUNC ri4152a_statEvenClr(
  ViSession vi
)
{
        struct ri4152a_globals *thisPtr;
        ViStatus errStatus;
	ViInt32 idx;

	errStatus = viGetAttribute(
                vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
        if( VI_SUCCESS > errStatus)
        {
                /* Errors: VI_ERROR_INV_SESSION */
                ri4152a_LOG_STATUS( vi, NULL, errStatus);
        }
        ri4152a_DEBUG_CHK_THIS( vi, thisPtr);
        ri4152a_CDE_INIT( "ri4152a_statEvenClr");

	/* clear instrument events */
	errStatus = viPrintf(vi, "*CLS\n");
	if( VI_SUCCESS > errStatus)
	{
		ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
	}

	/* clear driver copy of instrument events */
	for(idx=0; idx<ri4152a_MAX_STAT_REG; idx++)
	{
		thisPtr->driverEventArray[idx] = 0;
	}

	ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS);
}	

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_abor
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Aborts a measurement in progress, the trigger system is
 *           returned to the idle state.  No other settings are affected.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_abor(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_abor" );

    errStatus = viPrintf(vi,"ABOR\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_average_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns some statistical data on all readings
 *           taken since the AVERage function was enabled.  The data returned
 *           are the average value of readings, the minimum reading, the
 *           maximum reading, and the number of readings taken.  Each of
 *           these data parameters is individually available as well through
 *           a separate function call (i.e. the minimum value can be obtained
 *           by calling the function ri4152a_averMin_Q ).
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 average
 * OUT       
 *            Returns the average of all readings taken since the calculate
 *           function was set to AVERage and enabled.
 * 
 * PARAM 3 : ViPReal64 minValue
 * OUT       
 *            Returns the minimum value of all readings taken since the
 *           calculate function was set to AVERage and enabled.
 * 
 * PARAM 4 : ViPReal64 maxValue
 * OUT       
 *            Returns the maximum value of all readings taken since the
 *           calculate function was set to AVERage and enabled.
 * 
 * PARAM 5 : ViPInt32 points
 * OUT       
 *            Returns the number of readings taken since the calculate
 *           function was set to AVERage and enabled.  This is the number
 *           used to obtain the average.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_average_Q(ViSession vi,
  ViPReal64 average,
  ViPReal64 minValue,
  ViPReal64 maxValue,
  ViPInt32 points)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_average_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi, "CALC:AVER:AVER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%lg%*t", average);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viPrintf(vi, "CALC:AVER:MIN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%lg%*t", minValue);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viPrintf(vi, "CALC:AVER:MAX?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%lg%*t", maxValue);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viPrintf(vi, "CALC:AVER:COUN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%ld%*t", points);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
       
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calCoun_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns the number of times a point calibration
 *           has occurred.  A complete calibration of the instrument
 *           increments this value by more than just a single count.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 calCoun
 * OUT       
 *            A number between 0 and 32767 which represents the number of
 *           times a point calibration has occurred.  A complete calibration
 *           of the instrument increments this value by the number of points
 *           calibrated, not by only 1.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calCoun_Q(ViSession vi,
  ViPInt16 calCoun)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calCoun_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CAL:COUN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",calCoun);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calLfr
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine selects the line reference frequency used by the A
 *           to D converter.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 calLfr
 * IN        
 *            Indicates the desired line reference frequency; the legal
 *           values are 50 and 60.
 * 
 *      MAX = ri4152a_CAL_LFR_MAX   400
 *      MIN = ri4152a_CAL_LFR_MIN   50
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calLfr(ViSession vi,
  ViInt16 calLfr)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calLfr" );

    ri4152a_CHK_INT_RANGE(calLfr
                         ,ri4152a_CAL_LFR_MIN
                         ,ri4152a_CAL_LFR_MAX
                         ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CAL:LFR %d\n",(int)calLfr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calLfr_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of the reference line
 *           frequency.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 calLfr
 * OUT       
 *            Returns the current setting of the reference line frequency,
 *           either 50 or 60.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calLfr_Q(ViSession vi,
  ViPInt16 calLfr)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calLfr_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CAL:LFR?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",calLfr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calSecCode
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine specifies the new security code.  To accept the
 *           new code, security must first be turned OFF (0) with the
 *           ri4152a_calSecStat routine.
 *            Reset Condition:  Not affected
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViString code
 * IN        
 *            The new security code that will allow access to calibration
 *           ram.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calSecCode(ViSession vi,
  ViString code)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calSecCode" );

    errStatus = viPrintf(vi,"CAL:SEC:CODE %s\n",code);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calSecStat
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine enables or disables calibration security.  When
 *           security is disabled, the multimeter's calibration constants may
 *           be altered.  Read the service manual for more details on how to
 *           calibrate the multimeter.
 *            Reset Condition: Security is ON (1).
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean state
 * IN        
 *            The desired setting of security: 1 (ON) or 0 (OFF).
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViString code
 * IN        
 *            The security access code (password).
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calSecStat(ViSession vi,
  ViBoolean state,
  ViString code)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calSecStat" );

    ri4152a_CHK_BOOLEAN(state,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CAL:SEC:STAT %u,%s\n",state,code);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calSecStat_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of calibration
 *           security.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean state
 * OUT       
 *            The present setting of calibration security.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calSecStat_Q(ViSession vi,
  ViPBoolean state)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calSecStat_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CAL:SEC:STAT?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",state);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calVal
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine selects the value of the calibration signal.  See
 *           the service manual for details on how to use this command.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 value
 * IN        
 *            The value of the calibration signal applied.  See calibration
 *           manual for allowable values and their usage.
 * 
 *      MAX = ri4152a_CAL_VAL_MAX   100.0e6
 *      MIN = ri4152a_CAL_VAL_MIN   -300.0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calVal(ViSession vi,
  ViReal64 value)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calVal" );

    ri4152a_CHK_REAL_RANGE(value
                          ,ri4152a_CAL_VAL_MIN
                          ,ri4152a_CAL_VAL_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CAL:VAL %lg\n",value);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calVal_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of calibration value.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 value
 * OUT       
 *            The present setting of calibration value.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calVal_Q(ViSession vi,
  ViPReal64 value)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calVal_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CAL:VAL?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",value);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calZeroAuto
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Disables or enables the auto zero mode.  Auto zero applies to
 *           DC voltage, DC current, and 2-wire ohms measurements only. 
 *           4-wire ohms and dc voltage ratio measurements automatically set
 *           auto zero to ON.
 *            If 0 (OFF or ONCE), then the multimeter will make one zero
 *           measurement (with the input signal disconnected) and subtract
 *           this value from all measurements. A zero measurement is again
 *           taken whenever the range, function, or NPLC setting is changed. 
 *           To force a single zero reading, send the parameter set to 0;
 *           this will cause the multimeter to take a new zero reading and save it.
 *            When set to 1 (ON), the multimeter will make a zero measurement
 *           after each reading and subtracts this "zero value" from the
 *           measurement before returning the result.  Using the autoZero ON
 *           setting will give the most accurate measurements, but will
 *           result in slower acquisition times.
 *            Reset Condition: 1 (ON).
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 calZeroAuto
 * IN        
 *            OFF (0) disables or ON (1) enables autozero measurements.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calZeroAuto(ViSession vi,
  ViInt16 calZeroAuto)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calZeroAuto" );

    ri4152a_CHK_INT_RANGE(calZeroAuto
                         ,VI_FALSE
                         ,VI_TRUE
                         ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CAL:ZERO:AUTO %d\n",(int)calZeroAuto);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calZeroAuto_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of the auto zero
 *           feature.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 calZeroAuto
 * OUT       
 *            Returns a value of either 0 (OFF) or 1 (ON).
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calZeroAuto_Q(ViSession vi,
  ViPInt16 calZeroAuto)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calZeroAuto_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CAL:ZERO:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",calZeroAuto);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcAverAver_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns the average of all readings taken since
 *           the calculate function was set to AVERage and enabled.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 average
 * OUT       
 *            Returns the average of all readings taken since the calculate
 *           function was set to AVERage and enabled.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcAverAver_Q(ViSession vi,
  ViPReal64 average)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcAverAver_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:AVER:AVER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",average);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcAverCoun_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns the number of readings taken since the
 *           calculate function was set to AVERage and enabled.  This is the
 *           number used to obtain the average.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt32 points
 * OUT       
 *            Returns the number of readings taken since the calculate
 *           function was set to AVERage and enabled.  This is the number
 *           used to obtain the average.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcAverCoun_Q(ViSession vi,
  ViPInt32 points)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcAverCoun_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:AVER:COUN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%ld%*t",points);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcAverMax_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns the maximum value of all readings taken
 *           since the calculate function was set to AVERage and enabled.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 maxValue
 * OUT       
 *            Returns the maximum value of all readings taken since the
 *           calculate function was set to AVERage and enabled.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcAverMax_Q(ViSession vi,
  ViPReal64 maxValue)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcAverMax_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:AVER:MAX?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",maxValue);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcAverMin_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns the minimum value of all readings taken
 *           since the calculate function was set to AVERage and enabled.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 minValue
 * OUT       
 *            Returns the minimum value of all readings taken since the
 *           calculate function was set to AVERage and enabled.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcAverMin_Q(ViSession vi,
  ViPReal64 minValue)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcAverMin_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:AVER:MIN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",minValue);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcDbRef
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This function sets the dB reference value for use when the
 *           calculate function is DB.
 *           
 *            This value is used only for measurement functions of DC and AC
 *           voltage.
 *            When enabled by selecting calculate function of "DB" and
 *           turning calculate state to 1 (ON), the multimeter will return
 *           readings in dB, where
 *           
 *               dB = reading in dBm - reference value in dBm
 *           
 *            Reset Condition: 0.00
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 calcDbRef
 * IN        
 *            Sets the dB reference value for use when the calculate function
 *           is DB.
 * 
 *      MAX = ri4152a_CALC_DB_REF_MAX   200.0
 *      MIN = ri4152a_CALC_DB_REF_MIN   -200.0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcDbRef(ViSession vi,
  ViReal64 calcDbRef)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcDbRef" );

    ri4152a_CHK_REAL_RANGE(calcDbRef
                          ,ri4152a_CALC_DB_REF_MIN
                          ,ri4152a_CALC_DB_REF_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CALC:DB:REF %lg\n",calcDbRef);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcDbRef_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current value of the dB reference.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 calcDbRef
 * OUT       
 *            Returns the current value of the dB reference value used by the
 *           calculate function.
 * 
 *      MAX = ri4152a_CALC_DB_REF_MAX   200.0
 *      MIN = ri4152a_CALC_DB_REF_MIN   -200.0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcDbRef_Q(ViSession vi,
  ViPReal64 calcDbRef)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcDbRef_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:DB:REF?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",calcDbRef);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcDbmRef
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This function sets the dBm reference resistor value for use
 *           when the calculate function is DBM.  The dBm calculate function,
 *           when enabled, will calculate the power delivered to a resistance
 *           referenced to 1 milliWatt.  The equation for this is shown in
 *           the comments below.
 *            This value is used only for measurement functions of DC and AC
 *           voltage.
 *            When enabled by selecting calculate function of "DBM" and
 *           turning calculate state to 1 (ON), the multimeter will return
 *           readings in dBm, where
 *           
 *           dBm=10 * Log(reading * reading/reference resistance/ 1e-3 Watt)
 *           
 *            Once set, this value is saved in non-volatile memory.  It is
 *           unaffected by power on or reset.  The initial factory setting is
 *           600.
 *            Reset Condition: not affected, initially set to 600 at factory.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 calcDbmRef
 * IN        
 *            Sets the dBm reference resistor value for use when the
 *           calculate function is DBM.  The allowable resistor values are:
 *           50, 75, 93, 110, 124, 125, 135, 150, 250, 300, 500, 600, 800,
 *           900, 1000, 1200, and 8000 Ohms.
 * 
 *      MIN = ri4152a_CALC_DBM_REF_MAX   8000
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcDbmRef(ViSession vi,
  ViReal64 calcDbmRef)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcDbmRef" );

    ri4152a_CHK_REAL_RANGE(calcDbmRef
                          ,ri4152a_CALC_DBM_REF_MIN
                          ,ri4152a_CALC_DBM_REF_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CALC:DBM:REF %lg\n",calcDbmRef);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcDbmRef_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current value of the dBm reference.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 calcDbmRef
 * OUT       
 *            Returns the current value of the dBm reference value used by
 *           the calculate function.  This will be one of the following
 *           values: 50, 75, 93, 110, 124, 125, 135, 150, 250, 300, 500, 600,
 *           800, 900, 1000, 1200, or 8000 Ohms.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcDbmRef_Q(ViSession vi,
  ViPReal64 calcDbmRef)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcDbmRef_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:DBM:REF?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",calcDbmRef);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcFunc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the desired calculate function.  Only one function may
 *           be enabled at a time.  The calculate function does not become
 *           active until enabled with the ri4152a_calcStat routine.  Once
 *           enabled, the calculate function remains in effect until it is
 *           disabled via the ri4152a_calcStat routine, the measurement
 *           function is changed, a reset is performed, or power is cycled. 
 *           See the comments below for more information about particular function settings.
 *            NULL -- This setting is valid for all measurement types except
 *           DC voltage ratio. When this setting is enabled, each reading is
 *           the difference between the stored null value and the input
 *           signal.  One possible application for this is to make more
 *           accurate 2-wire resistance measurements by storing the test lead
 *           resistance measured with a shorted load as the null value.  See
 *           the ri4152a_calcNullOffs routine for more specifics on how to store a null value.
 *            AVERage -- This setting is valid for all measurement types. 
 *           When this setting is enabled, the multimeter records the
 *           minimum, maximum, and average values of all readings taken, and
 *           also the number of readings (count).  These values are stored in
 *           volatile memory.  See the ri4152a_calcAver routine for more
 *           specific information.
 *            LIMit -- This setting is valid for all measurement functions. 
 *           When this setting is enabled, pass fail testing is performed
 *           against upper and lower limits specified with the
 *           ri4152a_calcLimLow and ri4152a_calcLimUpp routines. When a limit
 *           is exceeded, the multimeter will set the corresponding bit in
 *           the questionable status register.  These two events can be
 *           detected and can cause a service routine specified by the user
 *           (a callback) to be called when either event occurrs.
 *            DBM -- This setting is valid only for DC and AC voltage
 *           measurements.  When this setting is enabled, readings are
 *           returned, in dBm, as the power delivered to a resistance
 *           referenced to 1 milliWatt.  This reference resistor is specified
 *           by the routine ri4152a_calcDbmRef.  See the ri4152a_calcDbmRef
 *           routine for more specific information.
 *            DB -- This setting is valid only for DC and AC voltage
 *           measurements.  When this setting is enabled, readings are
 *           returned, in dB, as the difference between the input signal and
 *           the dB reference value.  This reference value is set by using
 *           the rouinte ri4152a_calcDbRef.  See the ri4152a_calcDb routine
 *           for more specific information.
 *           
 *           
 *            Reset Condition: NULL
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 calcFunc
 * IN        
 *            An integer which determines the current calc function.  The
 *           mapping is as follows.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_calcFunc_calcFunc_a[] = {"NULL","AVER",
        "LIM","DBM","DB",0};
ViStatus _VI_FUNC ri4152a_calcFunc(ViSession vi,
  ViInt16 calcFunc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcFunc" );

    ri4152a_CHK_ENUM(calcFunc,4,VI_ERROR_PARAMETER2);
    errStatus = viPrintf(vi,"CALC:FUNC %s\n",ri4152a_calcFunc_calcFunc_a[calcFunc]);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcFunc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of the calculate
 *           function.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 calcFunc
 * OUT       
 *            Returns the current setting of the calculate function.  This
 *           will be one of the following.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_calcFunc_Q_calcFunc_a[] = {"NULL",
        "AVER","LIM","DBM","DB",0};
ViStatus _VI_FUNC ri4152a_calcFunc_Q(ViSession vi,
  ViPInt16 calcFunc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    char calcFunc_str[32];

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcFunc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:FUNC?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%s%*t",calcFunc_str);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = ri4152a_findIndex(thisPtr,ri4152a_calcFunc_Q_calcFunc_a,
        calcFunc_str, calcFunc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcLimLow
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This function sets the lower limit used when the calculate
 *           function is LIMit. When enabled, a status bit will be set
 *           whenever the reading falls below this lower limit.
 *           
 *            This setting may be used by all measurement functions.
 *           
 *            Reset Condition: 0.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 calcLimLow
 * IN        
 *            Sets the lower limit for limit checking.
 * 
 *      MAX = ri4152a_CALC_LIM_MAX   1.2e8
 *      MIN = ri4152a_CALC_LIM_MIN   -1.2e8
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcLimLow(ViSession vi,
  ViReal64 calcLimLow)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcLimLow" );

    ri4152a_CHK_REAL_RANGE(calcLimLow
                          ,ri4152a_CALC_LIM_MIN
                          ,ri4152a_CALC_LIM_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CALC:LIM:LOW %lg\n",calcLimLow);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcLimLow_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current value of the lower limit.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 calcLimLow
 * OUT       
 *            Returns the current value of the lower limit used by the
 *           calculate function.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcLimLow_Q(ViSession vi,
  ViPReal64 calcLimLow)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcLimLow_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:LIM:LOW?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",calcLimLow);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcLimUpp
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This function sets the upper limit used when the calculate
 *           function is LIMit. When enabled, a status bit will be set
 *           whenever the reading falls below this upper limit.
 *           
 *            This setting may be used by all measurement functions.
 *           
 *            Reset Condition: 0.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 calcLimUpp
 * IN        
 *            Sets the upper limit for limit checking.
 * 
 *      MAX = ri4152a_CALC_LIM_MAX   1.2e8
 *      MIN = ri4152a_CALC_LIM_MIN   -1.2e8
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcLimUpp(ViSession vi,
  ViReal64 calcLimUpp)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcLimUpp" );

    ri4152a_CHK_REAL_RANGE(calcLimUpp
                          ,ri4152a_CALC_LIM_MIN
                          ,ri4152a_CALC_LIM_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CALC:LIM:UPP %lg\n",calcLimUpp);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcLimUpp_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current value of the upper limit.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 calcLimUpp
 * OUT       
 *            Returns the current value of the upper limit used by the
 *           calculate function.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcLimUpp_Q(ViSession vi,
  ViPReal64 calcLimUpp)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcLimUpp_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:LIM:UPP?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",calcLimUpp);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcNullOffs
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This functions sets the null offset value used when the
 *           CALCulate function is set to NULL.
 *           
 *            The allowable values for the null offset vary depending on the
 *           current setting of the measurement function.  Changing the
 *           measurement function will reset the null offset value to 0.00.
 *           
 *            Reset Condition: 0.00
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 calcNullOffs
 * IN        
 *            Sets the null offset value when the CALCulate function is set
 *           to NULL.
 * 
 *      MAX = ri4152a_CALC_NULL_OFFS_MAX   1.2e8
 *      MIN = ri4152a_CALC_NULL_OFFS_MIN   -1.2e8
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcNullOffs(ViSession vi,
  ViReal64 calcNullOffs)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcNullOffs" );

    ri4152a_CHK_REAL_RANGE(calcNullOffs
                          ,ri4152a_CALC_NULL_OFFS_MIN
                          ,ri4152a_CALC_NULL_OFFS_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CALC:NULL:OFFS %lg\n",calcNullOffs);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcNullOffs_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current value of the null offset.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 calcNullOffs
 * OUT       
 *            Returns the current value of the null offset used by the
 *           calculate function.
 * 
 *      MAX = ri4152a_CALC_NULL_OFFS_MAX   1.2e8
 *      MIN = ri4152a_CALC_NULL_OFFS_MIN   -1.2e8
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcNullOffs_Q(ViSession vi,
  ViPReal64 calcNullOffs)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcNullOffs_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:NULL:OFFS?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",calcNullOffs);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcStat
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Enables or disables the multimeter's calculate function.  When
 *           disabled, no calculations are performed.
 *           
 *            Reset Condition: 0 (OFF).
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean calcStat
 * IN        
 *            ON (1) enables or OFF (0) disables the calculate function.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcStat(ViSession vi,
  ViBoolean calcStat)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcStat" );

    ri4152a_CHK_BOOLEAN(calcStat,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CALC:STAT %u\n",calcStat);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_calcStat_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current state of the calculate
 *           function.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean calcStat
 * OUT       
 *            Returns the current state of the calculate function; 0 (if
 *           disabled) or 1 (if enabled).
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_calcStat_Q(ViSession vi,
  ViPBoolean calcStat)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_calcStat_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CALC:STAT?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",calcStat);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confCurrAc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure AC current.  Use this
 *           routine when specific multimeter parameters need to be changed
 *           from their default values (listed below).  The measurement
 *           process will not begin until the ri4152a_initImm function is
 *           called.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 1 Amp, resolution to 10
 *           microAmps, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_currAcRang
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currAcRes
 *           
 *               Set to 10 microAmps.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confCurrAc(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confCurrAc" );

    errStatus = viPrintf(vi,"CONF:CURR:AC\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confCurrDc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure DC current.  Use this
 *           routine when specific multimeter parameters need to be changed
 *           from their default values (listed below).  The measurement
 *           process will not begin until the ri4152a_initImm function is
 *           called.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 1 A, resolution to 1
 *           microAmp, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_currDcRang
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currDcRes
 *           
 *               Set to 1 microAmp.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_currDcNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_currDcAper
 *           
 *               Set to 0.16667s if 60 Hz power, 0.20s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confCurrDc(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confCurrDc" );

    errStatus = viPrintf(vi,"CONF:CURR\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confFreq
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure frequency.  Use this
 *           routine when specific multimeter parameters need to be changed
 *           from their default values (listed below).  The measurement
 *           process will not begin until the ri4152a_initImm function is
 *           called.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range and resolution, and
 *           autoranging is enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_freqVoltRang
 *            ri4152a_perVoltRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_freqAper
 *            ri4152a_perAper
 *           
 *               Set to 0.1 seconds.
 *           
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confFreq(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confFreq" );

    errStatus = viPrintf(vi,"CONF:FREQ\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confFres
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure resistance in Four Wire
 *           mode.  Use this routine when specific multimeter parameters need
 *           to be changed from their default values (listed below).  The
 *           measurement process will not begin until the ri4152a_initImm
 *           function is called.
 *            Note that no range or resolution need be specified with this
 *           command. The multimeter will default to the 1000 Ohm range and
 *           0.001 Ohm resolution (10 NPLC setting), and autoranging will be
 *           enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_resRang
 *           
 *               Set to 1000 Ohm range, and auto-ranging is enabled.
 *           
 *            ri4152a_resNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_resAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRes
 *           
 *               Set to 0.001 Ohm.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            The following settings are also changed, but they don't apply
 *            to Resistance measurements:
 *           ----------------------------------------------------------------
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confFres(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confFres" );

    errStatus = viPrintf(vi,"CONF:FRES\n");
    is_four_wire = 1;
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confPer
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure period.  Use this routine
 *           when specific multimeter parameters need to be changed from
 *           their default values (listed below).  The measurement process
 *           will not begin until the ri4152a_initImm function is called.
 *           
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range and resolution, and auto
 *           ranging is enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_freqVoltRang
 *            ri4152a_perVoltRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_freqAper
 *            ri4152a_perAper
 *           
 *               Set to 0.1 seconds.
 *           
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confPer(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confPer" );

    errStatus = viPrintf(vi,"CONF:PER\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confRes
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure resistance.  Use this
 *           routine when some specific multimeter parameters need to be
 *           changed from their default values (listed below).  The
 *           measurement process will not begin until the ri4152a_initImm
 *           function is called.
 *            Note that no range or resolution need be specified with this
 *           command. The multimeter will default to the 1000 Ohm range and
 *           0.001 Ohm resolution (10 NPLC setting), and autoranging will be
 *           enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_resRang
 *           
 *               Set to 1000 Ohm range, and auto-ranging is enabled.
 *           
 *            ri4152a_resNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_resAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRes
 *           
 *               Set to 0.001 Ohm.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            The following settings are also changed, but they don't apply
 *            to Resistance measurements:
 *           ----------------------------------------------------------------
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confRes(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confRes" );

    errStatus = viPrintf(vi,"CONF:RES\n");
    is_four_wire = 0;
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confVoltAc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure AC voltage.  Use this
 *           routine when specific multimeter parameters need to be changed
 *           from their default values (listed below).  The measurement
 *           process will not begin until the ri4152a_initImm function is
 *           called.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 10 V, resolution to 10
 *           microVolts, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_voltAcRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltAcRes
 *           
 *               Set to 10 microvolts.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confVoltAc(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confVoltAc" );

    errStatus = viPrintf(vi,"CONF:VOLT:AC\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confVoltDc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure DC voltage.  Use this
 *           routine when specific multimeter parameters need to be changed
 *           from their default values (listed below).  The measurement
 *           process will not begin until the ri4152a_initImm function is
 *           called.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 10 V, resolution to 10
 *           microVolts, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_voltDcRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltDcRes
 *           
 *               Set to 10 microvolts.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_voltDcNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_voltDcAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImp
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confVoltDc(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confVoltDc" );

    errStatus = viPrintf(vi,"CONF:VOLT\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_confVoltDcRat
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure DC voltage ratio.  The
 *           ratio calculated is determined by dividing the voltage on the Hi
 *           and Lo terminals by the reference voltage connected to the Ohms
 *           Sense Hi and Lo terminals.
 *            Use this routine when specific multimeter parameters need to be
 *           changed from their default values (listed below).  The
 *           measurement process will not begin until the ri4152a_initImm
 *           function is called.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 10 V, resolution to 10
 *           microVolts, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_voltDcRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltDcRes
 *           
 *               Set to 10 microvolts.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_voltDcNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_voltDcAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImp
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_confVoltDcRat(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_confVoltDcRat" );

    errStatus = viPrintf(vi,"CONF:RAT\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_conf_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Returns the current configuration settings of the multimeter.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 function
 * OUT       
 *            Returns an integer representing the current configuration
 *           function.  The values are as shown below.
 * 
 * PARAM 3 : ViPBoolean autoRange
 * OUT       
 *            The present setting of auto ranging is returned in this
 *           variable.  If a 1 is returned, auto ranging is enabled, if 0, it
 *           is disabled.
 * 
 * PARAM 4 : ViPReal64 range
 * OUT       
 *            The instrument's present range setting is returned in this
 *           variable.
 * 
 * PARAM 5 : ViPReal64 resolution
 * OUT       
 *            The present resolution setting is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_conf_Q_function_a[] = {"FREQ","PER",
        "FRES","RES","VOLT:AC","VOLT","CURR:AC","CURR","VOLT:RAT",0};
ViStatus _VI_FUNC ri4152a_conf_Q(ViSession vi,
  ViPInt16 function,
  ViPBoolean autoRange,
  ViPReal64 range,
  ViPReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    char func_str[40];

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_conf_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CONF?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    /* the query returns 3 fields in a quoted response string.  The response
       looks like:
    
        "FUNC range,resolution" 
    
    */
    
    errStatus = viScanf(vi, "%s", func_str);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_CDE_MESSAGE( "Was reading function" );
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    /* get the index for the current function */
    /* send func_str[1] so skip past leading quote */
    errStatus = ri4152a_findIndex(thisPtr, ri4152a_conf_Q_function_a,
                           &func_str[1], function);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
       
    /* get range and resolution, added "%*t" to flush out buffer */
    errStatus = viScanf(vi, "%lg,%lg%*t", range, resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_CDE_MESSAGE( "Was getting conf_Q range and res" );
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    if ( *function == ri4152a_CONFQ_FREQ)
    {
       errStatus = viPrintf(vi,"FREQ:VOLT:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:freq auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (*function == ri4152a_CONFQ_PER )
    {
       errStatus = viPrintf(vi,"PER:VOLT:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:per auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (*function == ri4152a_CONFQ_RES || *function == ri4152a_CONFQ_FRES) 
    {
       errStatus = viPrintf(vi,"RES:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:res|fres:auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (*function == ri4152a_CONFQ_VOLT_DC ||
             *function == ri4152a_CONFQ_VOLT_RAT )
    {
       errStatus = viPrintf(vi,"VOLT:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "Was querying VOLT:DC range auto" );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:volt:dc:auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (*function == ri4152a_CONFQ_VOLT_AC )
    {
       errStatus = viPrintf(vi,"VOLT:AC:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "Was querying VOLT:AC range auto" );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:volt:ac:auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (*function == ri4152a_CONFQ_CURR_DC )
    {
       errStatus = viPrintf(vi,"CURR:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "Was querying CURR:DC range auto" );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:curr:dc:auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (*function == ri4152a_CONFQ_CURR_AC )
    {
       errStatus = viPrintf(vi,"CURR:AC:RANG:AUTO?\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "Was querying CURR:AC range auto" );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       errStatus = viScanf(vi, "%hd%*t", autoRange);
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_CDE_MESSAGE( "viScanf of conf:curr:ac:auto range setting " );
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_configure
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure the specified function. 
 *           Use this routine when specific multimeter parameters need to be
 *           changed from their default values (listed below).  The
 *           measurement process will not begin until the ri4152a_initImm
 *           function is called.
 *            Reset Condition: configured for DC voltage
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *           
 *            ri4152a_voltAcRang  (only when function is AC Voltage)
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltAcRes  (only when function is AC Voltage)
 *           
 *               Set to 10 microvolts.
 *           
 *            ri4152a_voltDcRang  (only when function is DC Voltage)
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltDcRes  (only when function is DC Voltage)
 *           
 *               Set to 10 microvolts.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_voltDcNplc  (only when function is DC Voltage)
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_voltDcAper  (only when function is DC Voltage)
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRang  (only when function is Res or Fres)
 *           
 *               Set to 1000 Ohm range, and auto-ranging is enabled.
 *           
 *            ri4152a_resNplc  (only when function is Res or Fres)
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_resAper  (only when function is Res or Fres)
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRes  (only when function is Res or Fres)
 *           
 *               Set to 0.001 Ohm.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_currAcRang  (only when function is AC current)
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currAcRes  (only when function is AC current)
 *           
 *               Set to 10 microAmps.
 *           
 *            ri4152a_currDcRang  (only when function is DC current)
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currDcRes  (only when function is DC current)
 *           
 *               Set to 1 microAmp.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_currDcNplc  (only when function is DC current)
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_currDcAper  (only when function is DC current)
 *           
 *               Set to 0.16667s if 60 Hz power, 0.20s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_freqVoltRang  (only when function is Frequency)
 *            ri4152a_perVoltRang  (only when function is Period)
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_freqAper  (only when function is Frequency)
 *            ri4152a_perAper  (only when function is Period)
 *           
 *               Set to 0.1 seconds.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 function
 * IN        
 *            The desired function to configure the multimeter to measure. 
 *           The allowable settings are shown below.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_configure_function_a[] = {"FREQ","PER",
        "FRES","RES","VOLT:AC","VOLT","CURR:AC","CURR","VOLT:RAT",0};
ViStatus _VI_FUNC ri4152a_configure(ViSession vi,
  ViInt16 function)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_configure" );

    ri4152a_CHK_ENUM(function,8,VI_ERROR_PARAMETER2);
    errStatus = viPrintf(vi,"CONF:%s\n",ri4152a_configure_function_a[function]);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currAcRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the range for the AC current measurements.
 *           
 *            Reset Condition: 1.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the AC current range, as follows.
 * 
 *      MAX = ri4152a_CURR_AC_RANG_MAX   3.0
 *      MIN = ri4152a_CURR_AC_RANG_MIN   1.0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currAcRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currAcRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the range parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"CURR:AC:RANG:AUTO ON\n" );
       if (errStatus < VI_SUCCESS)
       {	
          ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
       }
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(range
                             ,ri4152a_CURR_AC_RANG_MIN
                             ,ri4152a_CURR_AC_RANG_MAX
                             ,VI_ERROR_PARAMETER3);

    
       errStatus = viPrintf(vi,"CURR:AC:RANG %lg\n", range );
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currAcRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of current range.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the present AC current range setting.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currAcRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currAcRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:AC:RANG:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viPrintf(vi,"CURR:AC:RANG?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currAcRes
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the resolution for AC current measurements.
 *           
 *            Reset Condition: 1.0e-5
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 resolution
 * IN        
 *            Specifies the resolution for AC current measurements.  Note
 *           that resolution is a function of range; thus 100 microAmps of
 *           resolution is only possible on the lowest range, and if selected
 *           on a higher range will result in an error from the instrument. 
 *           See the table below for the settings possible.
 * 
 *      MAX = ri4152a_CURR_AC_RES_MAX   300.0e-6
 *      MIN = ri4152a_CURR_AC_RES_MIN   1.0e-6
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currAcRes(ViSession vi,
  ViReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currAcRes" );

    ri4152a_CHK_REAL_RANGE(resolution
                          ,ri4152a_CURR_AC_RES_MIN
                          ,ri4152a_CURR_AC_RES_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CURR:AC:RES %lg\n",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currAcRes_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of current resolution.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 resolution
 * OUT       
 *            Returns current AC current resolution.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currAcRes_Q(ViSession vi,
  ViPReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currAcRes_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:AC:RES?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcAper
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in seconds for DC current
 *           measurements. The multimeter rounds values UP to the nearest
 *           time. See also the ri4152a_currDcRes and ri4152a_currDcNplc
 *           functions because changing aperture affects the setting on those
 *           two functions as well (they are coupled).
 *           
 *            Reset Condition: 166.7e-3
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 aperture
 * IN        
 *            Sets the integration time in seconds (aperture) for DC current
 *           measurements. Input values are rounded UP to the nearest
 *           aperture time shown in the table below.
 *           
 *            Aperture is one of three ways to set the resolution of the
 *           reading.  The other two are currDcRes and currDcNplc.  The
 *           relationships between range, resolution, aperture and NPLC's
 *           (Number Power Line Cycles) is shown below.
 *            Aperture is determined by the NPLC setting; for example, for 1
 *           power line cycle of 60 Hz power, the aperture is 16.7
 *           milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power
 *           source were 50 Hz instead of 60 Hz, the above numbers would be
 *           20.0 milliseconds and 200 milliseconds respectively.
 *            In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs). 
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *            10 mA|   3 nA  |  10 nA |  30 nA | 100 nA   |    1 uA  |
 *           100 mA|  30 nA  | 100 nA | 300 nA |   1 uA   |   10 uA  |
 *             1 A | 300 nA  |   1 uA |   3 uA |  10 uA   |  100 uA  |
 *             3 A | 900 nA  |   3 uA |   9 uA |  30 uA   |  300 uA  |
 *           ---------------------------------------------------------
 *                 |                Resolution                       |
 *                 ---------------------------------------------------
 * 
 *      MAX = ri4152a_APER_MAX   2.0
 *      MIN = ri4152a_APER_MIN   0.400e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcAper(ViSession vi,
  ViReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcAper" );

    ri4152a_CHK_REAL_RANGE(aperture
                          ,ri4152a_APER_MIN
                          ,ri4152a_APER_MAX
                          ,VI_ERROR_PARAMETER2);

    /* if aperture is largest, then send max to avoid getting
       error if sent macro APER_MAX when in 60 Hz mode.  This
       causes settings conflict if just send the number.
    */
    if (aperture > 1.5)
        errStatus = viPrintf(vi,"CURR:APER MAX\n");
    else
        errStatus = viPrintf(vi,"CURR:APER %lg\n",aperture);

    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcAper_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of DC current
 *           aperture.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 aperture
 * OUT       
 *            Returns current integration time setting in seconds for DC
 *           current measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcAper_Q(ViSession vi,
  ViPReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcAper_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:APER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcNplc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in Power Line Cycles (PLC) for DC
 *           current measurements. The multimeter rounds values UP to the
 *           nearest time. See also the ri4152a_currDcRes and
 *           ri4152a_currDcAper functions because changing NPLC affects the
 *           setting on those two functions as well (they are coupled).
 *           
 *            Reset Condition: 10.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 nplc
 * IN        
 *            Sets the integration time in Power Line Cycles (PLCs) for DC
 *           current measurements.  Input values are rounded UP to the
 *           nearest aperture time shown in the table below.
 *           
 *            NPLC is one of three ways to set the resolution of the reading.
 *            The other two are currDcRes and currDcAper.  The relationships
 *           between range, resolution, aperture and NPLC's (Number Power
 *           Line Cycles) is shown below.
 *            Aperture is determined by the NPLC setting; for example, for 1
 *           power line cycle of 60 Hz power, the aperture is 16.7
 *           milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power
 *           source were 50 Hz instead of 60 Hz, the above numbers would be
 *           20.0 milliseconds and 200 milliseconds respectively.
 *            In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs). 
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *            10 mA|   3 nA  |  10 nA |  30 nA | 100 nA   |    1 uA  |
 *           100 mA|  30 nA  | 100 nA | 300 nA |   1 uA   |   10 uA  |
 *             1 A | 300 nA  |   1 uA |   3 uA |  10 uA   |  100 uA  |
 *             3 A | 900 nA  |   3 uA |   9 uA |  30 uA   |  300 uA  |
 *           ---------------------------------------------------------
 *                 |                Resolution                       |
 *                 ---------------------------------------------------
 * 
 *      MAX = ri4152a_NPLC_MAX   100.0
 *      MIN = ri4152a_NPLC_MIN   0.02
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcNplc(ViSession vi,
  ViReal64 nplc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcNplc" );

    ri4152a_CHK_REAL_RANGE(nplc
                          ,ri4152a_NPLC_MIN
                          ,ri4152a_NPLC_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CURR:NPLC %lg\n",nplc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcNplc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of number of power
 *           line cycles integration time (NPLC's).
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 nplc
 * OUT       
 *            Returns the present integration time in power line cycles
 *           (PLCs) for DC current measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcNplc_Q(ViSession vi,
  ViPReal64 nplc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcNplc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:NPLC?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",nplc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects a specific range for DC current measurements or turns
 *           auto ranging on/off.
 *           
 *            Reset Condition: 10.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the DC current range to the nearest range >= to the level
 *           specified.  The actual current ranges are 10 milliAmps, 100
 *           milliAmps, 1 Amp, and 3 Amps.
 * 
 *      MAX = ri4152a_CURR_DC_RANG_MAX   3.0
 *      MIN = ri4152a_CURR_DC_RANG_MIN   100.0e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the range parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"CURR:RANG:AUTO ON\n" );
       if (errStatus < VI_SUCCESS)
       {	
          ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
       }
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(range
                             ,ri4152a_CURR_DC_RANG_MIN
                             ,ri4152a_CURR_DC_RANG_MAX
                             ,VI_ERROR_PARAMETER3);

       errStatus = viPrintf(vi,"CURR:RANG %lg\n", range );
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of DC current range,
 *           and whether auto ranging is enabled or not.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the currently used DC current range.  The possible
 *           return values are 0.01 Amps, 0.1 Amps, 1 Amp, and 3 Amps.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:RANG:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:RANG?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcRes
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the resolution for DC current measurements.
 *            See also the ri4152a_currDcAper and ri4152a_currDcNplc
 *           functions because changing resolution affects the setting on
 *           those two functions as well (they are coupled).
 *           
 *            Reset Condition: 1.0e-6
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 resolution
 * IN        
 *            Specifies the resolution for DC current measurements.  Note
 *           that resolution is is really determined by aperture or NPLC
 *           settings, and so the minimum aperture on A GIVEN RANGE
 *           determines the resolution; thus 10 nanoVolts of resolution is
 *           only possible on the lowest range, and if selected on a higher
 *           range will result in an error from the instrument.  See the
 *           table below for the settings possible.
 *            Also, selecting a resolution changes the aperture and NPLC
 *           settings, see the second table below for the interactions of
 *           Range, Resolution, Aperture, and NPLCs.
 *           
 *            Macro Name			Value      Description
 *           ---------------------------------------------------------------
 *            ri4152a_CURR_DC_RES_3_NANO	3.0e-9	  For range = 100e-3 A
 *            ri4152a_CURR_DC_RES_10_NANO	10.0e-9	  For range = 100e-3 A
 *            ri4152a_CURR_DC_RES_30_NANO	30.0e-9	  For range <= 10e-3 A
 *            ri4152a_CURR_DC_RES_100_NANO	100.0e-9  For range <= 10e-3 A
 *            ri4152a_CURR_DC_RES_300_NANO	300.0e-9  For range <= 1 A
 *            ri4152a_CURR_DC_RES_1_MICRO	1.0e-6	  For range <= 1 A
 *            ri4152a_CURR_DC_RES_3_MICRO	3.0e-6	  For all ranges
 *            ri4152a_CURR_DC_RES_9_MICRO	9.0e-6	  For all ranges
 *            ri4152a_CURR_DC_RES_30_MICRO	30.0e-6	  For all ranges
 *            ri4152a_CURR_DC_RES_100_MICRO	100.0e-6  For all ranges
 *            ri4152a_CURR_DC_RES_300_MICRO	300.0e-6  For all ranges
 *           
 *           In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs).
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *            10 mA|   3 nA  |  10 nA |  30 nA | 100 nA   |    1 uA  |
 *           100 mA|  30 nA  | 100 nA | 300 nA |   1 uA   |   10 uA  |
 *             1 A | 300 nA  |   1 uA |   3 uA |  10 uA   |  100 uA  |
 *             3 A | 900 nA  |   3 uA |   9 uA |  30 uA   |  300 uA  |
 *           ---------------------------------------------------------
 *                 |                Resolution                       |
 *                 ---------------------------------------------------
 * 
 *      MAX = ri4152a_CURR_DC_RES_MAX   3.0
 *      MIN = ri4152a_CURR_DC_RES_MIN   3.0e-9
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcRes(ViSession vi,
  ViReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcRes" );

    ri4152a_CHK_REAL_RANGE(resolution
                          ,ri4152a_CURR_DC_RES_MIN
                          ,ri4152a_CURR_DC_RES_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CURR:RES %lg\n",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_currDcRes_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the present setting of current resolution.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 resolution
 * OUT       
 *            Returns current DC current resolution.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_currDcRes_Q(ViSession vi,
  ViPReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_currDcRes_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CURR:RES?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_dataPoin_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns the number of readings presently stored in
 *           memory from the last measurement operation.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt32 numReadings
 * OUT       
 *            Returns the number of readings stored in memory.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_dataPoin_Q(ViSession vi,
  ViPInt32 numReadings)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_dataPoin_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"DATA:POIN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%ld%*t",numReadings);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_detBand
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the multimeter's fast, medium and slow modes for AC
 *           voltage, frequency, or period measurements.
 *           
 *            For signal frequencies between 3.0 and less than 20.0 Hz, the
 *           slow (3 Hz) filter is chosen.  For frequencies >= 20 Hz and <
 *           200 Hz, the medium speed (20 Hz) filter is chosen.  For
 *           frequencies >= 200 Hz, the fast (200 Hz) filter is chosen.
 *           
 *            Reset Condition: 20
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 detBand
 * IN        
 *            The expected signal frequency.
 * 
 *      MAX = ri4152a_DET_BAND_MAX   3.0e5
 *      MIN = ri4152a_DET_BAND_MIN   3.0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_detBand(ViSession vi,
  ViReal64 detBand)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_detBand" );

    ri4152a_CHK_REAL_RANGE(detBand
                          ,ri4152a_DET_BAND_MIN
                          ,ri4152a_DET_BAND_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"DET:BAND %lg\n",detBand);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_detBand_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of detector bandwidth.
 *            The possible settings are 3, 20, or 200.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 detBand
 * OUT       
 *            Returns the detector bandwidth.  This will be either 3, 20, or
 *           200 Hz.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_detBand_Q(ViSession vi,
  ViPReal64 detBand)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_detBand_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"DET:BAND?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",detBand);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_fetc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns readings stored in memory from the last
 *           measurement operation, and the numReadings parameter indicates
 *           how many readings were returned in the readings array.  NOTE: If
 *           a large number of readings were taken, and this call is made
 *           before the multimeter has taken all the data, a time out may
 *           occur.  You can increase the time out value with the
 *           ri4152a_timeOut function.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 _VI_FAR readings[]
 * OUT       
 *            The array of double which will hold the readings.
 * 
 * PARAM 3 : ViPInt32 numReadings
 * OUT       
 *            Returns the number of readings fetched.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_fetc_Q(ViSession vi,
  ViReal64 _VI_FAR readings[],
  ViPInt32 numReadings)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    int done;
    char separator;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_fetc_Q" );
    
    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"FETC?\n");
    if (errStatus < VI_SUCCESS)
    {
       
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    *numReadings = 1;
    done = 0;
    while (!done)
    {
       errStatus = viScanf(vi, "%lg%c", &readings[*numReadings - 1], &separator);
       if (errStatus < VI_SUCCESS)
       {
          *numReadings -= 1;
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       if (separator != ',')
       {
          done = 1;
    
        /* get rid of any left over stuff */
          errStatus = viFlush(vi, VI_READ_BUF_DISCARD);
          if (errStatus < VI_SUCCESS)
          {
             ri4152a_CDE_MESSAGE( "Error flushing buffer" );
             ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
          }
       }
       else
          *numReadings += 1;
    }
       
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_freqAper
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in seconds for frequency
 *           measurements. The multimeter rounds values UP to the nearest
 *           time.
 *            The number of digits of accuracy is determined by the aperture
 *           setting.  The relationship is shown below.
 *           
 *            Aperture     Digits
 *            ------------------------
 *            0.01         4.5
 *            0.1          5.5
 *            1.0          6.5
 *           
 *            Additionally, the larger the aperture setting, the longer the
 *           measurement takes to complete.
 *            Reset Condition: 0.1 seconds
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 aperture
 * IN        
 *            Sets the integration time in seconds (aperture) for frequency
 *           measurements. The allowable settings are 0.01, 0.1, and 1
 *           seconds.
 * 
 *      MAX = ri4152a_FREQ_APER_MAX   1.0
 *      MIN = ri4152a_FREQ_APER_MIN   0.01
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_freqAper(ViSession vi,
  ViReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_freqAper" );

    ri4152a_CHK_REAL_RANGE(aperture
                          ,ri4152a_FREQ_APER_MIN
                          ,ri4152a_FREQ_APER_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"FREQ:APER %lg\n",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_freqAper_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of frequency aperture.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 aperture
 * OUT       
 *            Returns current integration time setting in seconds for
 *           frequency measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_freqAper_Q(ViSession vi,
  ViPReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_freqAper_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"FREQ:APER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_freqVoltRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of frequency voltage
 *           range, and whether auto ranging is enabled or not.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the currently used frequency voltage range.  The
 *           possible return values are 100 millivolts, 1 Volt, 10 Volts, 100
 *           Volts and 300 Volts.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_freqVoltRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_freqVoltRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"FREQ:VOLT:RANG:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viPrintf(vi,"FREQ:VOLT:RANG?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_freqVoltRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects a specific voltage range for frequency measurements, or
 *           turns auto ranging on/off.
 *           
 *            Reset Condition: 10.0 volts
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the frequency range to the nearest range >= to the level
 *           specified.  The actual voltage ranges are 100 millivolts, 1
 *           Volt, 10 Volts, 100 Volts and 300 Volts.
 * 
 *      MAX = ri4152a_VOLT_RANG_MAX   300.0
 *      MIN = ri4152a_VOLT_RANG_MIN   100.0e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_freqVoltRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_freqVoltRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the voltRange parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"FREQ:VOLT:RANG:AUTO 1\n" );
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(range
                             ,ri4152a_VOLT_RANG_MIN
                             ,ri4152a_VOLT_RANG_MAX
                             ,VI_ERROR_PARAMETER3);

    
       errStatus = viPrintf(vi,"FREQ:VOLT:RANG %lg\n", range );
    }
   
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_func
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This selects the measurement function consisting of 2-wire and
 *           4-wire resistance, DC voltages, DC voltage Ratio, AC RMS
 *           voltages, DC and AC Current, frequency, and period measurements.
 *           
 *            Reset Condition: VOLT:DC
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument.
 * 
 * PARAM 2 : ViInt16 func
 * IN        
 *            This parameter is an integer which represents the multimeter
 *           function that is desired.  The mapping is as follows.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_func_func_a[] = {"FREQ","PER","FRES",
        "RES","VOLT:AC","VOLT","CURR:AC","CURR","VOLT:RAT",0};
ViStatus _VI_FUNC ri4152a_func(ViSession vi,
  ViInt16 function)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_func" );

    ri4152a_CHK_ENUM(function,8,VI_ERROR_PARAMETER2);
    /* we have some non-SCPI syntax that can save a little parse time if
     * the function is RES, FRES, DCV, or DCI
     */
    if (function == ri4152a_FUNC_RES)
    {
       is_four_wire = 0;
       errStatus = viPrintf(vi,"F3\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (function == ri4152a_FUNC_VOLT_DC)
    {
       errStatus = viPrintf(vi,"F1\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (function == ri4152a_FUNC_CURR_DC)
    {
       errStatus = viPrintf(vi,"F2\n");
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else if (function == ri4152a_FUNC_FRES)
    {
       errStatus = viPrintf(vi,"F4\n");
       is_four_wire = 1;
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    else
    {
       errStatus = viPrintf(vi,"FUNC \"%s\"\n", ri4152a_func_func_a[function] );
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_func_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of the function.  Note
 *           that if you have configured the instrument for Temperature, the
 *           function returned is either RES or FRES depending on the
 *           tranducer type selected.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 func
 * OUT       
 *            Returns an integer representing the current function setting. 
 *           The mapping is as follows.
 *           
 *            Macro Name             Value      Description
 *           ---------------------------------------------------------------
 *            ri4152a_FUNC_FREQ          0      Frequency
 *            ri4152a_FUNC_PER           1      Period
 *            ri4152a_FUNC_FRES          2      4-Wire Resistance
 *            ri4152a_FUNC_RES           3      2-Wire Resistance
 *            ri4152a_FUNC_VOLT_AC       4      AC Voltage
 *            ri4152a_FUNC_VOLT_DC       5      DC Voltage
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_func_Q(ViSession vi,
  ViPInt16 func)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    char temp_str[20];
    char my_func[20];
    char *quote;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_func_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi, "FUNC?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%s%*t", temp_str);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    /* get rid of leading and trailing quotes */
    quote = strchr(&temp_str[1],'"');	/* get pointer to last quote */
    *quote = '\0';			/* replace quote with \0 */
    sprintf(my_func, &temp_str[1]);
    
    errStatus = ri4152a_findIndex(thisPtr, ri4152a_func_func_a,
                      my_func, func);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_initImm
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Places the multimeter in the wait-for-trigger state and stores
 *           readings in multimeter memory when a trigger occurs. The new
 *           readings replace the readings in memory from previous commands.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_initImm(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_initImm" );

    errStatus = viPrintf(vi,"INIT\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}
 /*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigImm
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Internal Triiger is always present.
 *       	  If the DMM is in the wait-for-trigger state, this command  sends the trigger.
 *		
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_trigImm(ViSession vi)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigtImm" );

    errStatus = viPrintf(vi,"TRIG:SOUR IMM\n"); 
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_inpImpAuto
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Enables or disables the automatic input impedance mode for DC
 *           voltage measurements. When disabled (0 or OFF), the multimeter
 *           maintains its input impedance of 10 Mohms for all DC voltage
 *           ranges. This prevents an input impedance change (caused by
 *           changing ranges) from affecting measurements.
 *            Reset Condition: 0 (OFF)
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean inpImpAuto
 * IN        
 *            Selects the automatic input impedance for DC measurements.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_inpImpAuto(ViSession vi,
  ViBoolean inpImpAuto)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_inpImpAuto" );

    ri4152a_CHK_BOOLEAN(inpImpAuto,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"INP:IMP:AUTO %u\n",inpImpAuto);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_inpImpAuto_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of inpImpAuto
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean inpImpAuto
 * OUT       
 *            Returns OFF (0) if input impedance mode is disabled or ON (1)
 *           if enabled..
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_inpImpAuto_Q(ViSession vi,
  ViPBoolean inpImpAuto)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_inpImpAuto_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"INP:IMP:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",inpImpAuto);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measCurrAc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure AC current and takes a
 *           reading using the default settings forced by the configure. 
 *           This is the simplest way to take a reading with the multimeter. 
 *           If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 1 Amp, resolution to 10
 *           microAmps, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_currAcRang
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currAcRes
 *           
 *               Set to 10 microAmps.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measCurrAc_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measCurrAc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:CURR:AC?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measCurrDc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure DC current and takes a
 *           reading using the default settings forced by the configure. 
 *           This is the simplest way to take a reading with the multimeter. 
 *           If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 1 A, resolution to 1
 *           microAmp, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_currDcRang
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currDcRes
 *           
 *               Set to 1 microAmp.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_currDcNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_currDcAper
 *           
 *               Set to 0.16667s if 60 Hz power, 0.20s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measCurrDc_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measCurrDc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:CURR?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measFreq_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure frequency and takes a
 *           reading using the default settings forced by the configure. 
 *           This is the simplest way to take a reading with the multimeter. 
 *           If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range and resolution, and
 *           autoranging is enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_freqVoltRang
 *            ri4152a_perVoltRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_freqAper
 *            ri4152a_perAper
 *           
 *               Set to 0.1 seconds.
 *           
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measFreq_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measFreq_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:FREQ?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

	Delay (5.0);
	
    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measFres_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure resistance in the
 *           Four-wire mode, and takes a reading using the default settings
 *           forced by the configure.  This is the simplest way to take a
 *           reading with the multimeter.  If the default settings are
 *           acceptable, and a single reading is all that is desired, then
 *           this routine is the one to use.
 *            Note that no range or resolution need be specified with this
 *           command. The multimeter will default to the 1000 Ohm range and
 *           0.001 Ohm resolution (10 NPLC setting), and autoranging will be
 *           enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_resRang
 *           
 *               Set to 1000 Ohm range, and auto-ranging is enabled.
 *           
 *            ri4152a_resNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_resAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRes
 *           
 *               Set to 0.001 Ohm.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            The following settings are also changed, but they don't apply
 *            to Resistance measurements:
 *           ----------------------------------------------------------------
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measFres_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measFres_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:FRES?\n");
    is_four_wire = 1;
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measPer_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure period and takes a reading
 *           using the default settings forced by the configure.  This is the
 *           simplest way to take a reading with the multimeter.  If the
 *           default settings are acceptable, and a single reading is all
 *           that is desired, then this routine is the one to use.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range and resolution, and auto
 *           ranging is enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_freqVoltRang
 *            ri4152a_perVoltRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_freqAper
 *            ri4152a_perAper
 *           
 *               Set to 0.1 seconds.
 *           
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measPer_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measPer_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:PER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

	Delay (5.0);
	
    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measRes_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure resistance and takes a
 *           reading using the default settings forced by the configure. 
 *           This is the simplest way to take a reading with the multimeter. 
 *           If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *            Note that no range or resolution need be specified with this
 *           command. The multimeter will default to the 1000 Ohm range and
 *           0.001 Ohm resolution (10 NPLC setting), and autoranging will be
 *           enabled.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_resRang
 *           
 *               Set to 1000 Ohm range, and auto-ranging is enabled.
 *           
 *            ri4152a_resNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_resAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRes
 *           
 *               Set to 0.001 Ohm.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            The following settings are also changed, but they don't apply
 *            to Resistance measurements:
 *           ----------------------------------------------------------------
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measRes_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measRes_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:RES?\n");
    is_four_wire = 0;
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measVoltAc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure AC voltage and takes a
 *           reading using the default settings forced by the configure. 
 *           This is the simplest way to take a reading with the multimeter. 
 *           If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 10 V, resolution to 10
 *           microVolts, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_voltAcRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltAcRes
 *           
 *               Set to 10 microvolts.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measVoltAc_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, 0, errStatus );
    

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measVoltAc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:VOLT:AC?\n");
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measVoltDcRat_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure DC voltage ratio.  The
 *           ratio calculated is determined by dividing the voltage on the Hi
 *           and Lo terminals by the reference voltage connected to the Ohms
 *           Sense Hi and Lo terminals.
 *            This is the simplest way to take a reading with the multimeter.
 *            If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *           
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 10 V, resolution to 10
 *           microVolts, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_voltDcRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltDcRes
 *           
 *               Set to 10 microvolts.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_voltDcNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_voltDcAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImp
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measVoltDcRat_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, 0, errStatus );
    

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measVoltDcRat_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:RAT?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measVoltDc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure DC voltage and takes a
 *           reading using the default settings forced by the configure. 
 *           This is the simplest way to take a reading with the multimeter. 
 *           If the default settings are acceptable, and a single reading is
 *           all that is desired, then this routine is the one to use.
 *            Note that no range and resolution parameters are necessary. 
 *           The multimeter defaults the range to 10 V, resolution to 10
 *           microVolts, and enables auto ranging.
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_voltDcRang
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltDcRes
 *           
 *               Set to 10 microvolts.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_voltDcNplc
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_voltDcAper
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImp
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 reading
 * OUT       
 *            The measurement result is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_measVoltDc_Q(ViSession vi,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measVoltDc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:VOLT?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",reading);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_measure_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Configures the instrument to measure the specified function and
 *           takes a single measurement.  Use this routine when the default
 *           parameters are acceptable.
 *            Reset Condition: configured for DC voltage
 *           
 *           Notes: This function performs a soft reset on the instrument and
 *                  forces the following settings to be current.  The
 *                  following table shows the related function and the
 *                  setting that is forced by this function.  To change the
 *                  setting, call the associated function.
 *           
 *            ri4152a_calZeroAuto
 *           
 *               ON (performs autozero after each reading). This slows the
 *               measurement down by about a factor of 2, but provides
 *               greatest accuracy. Set this to OFF for more speed.
 *           
 *            ri4152a_sampCoun
 *           
 *               Set to 1.  The multimeter will take a single reading.
 *           
 *            ri4152a_trigSour
 *           
 *               Set to IMMediate.  Samples begin to be taken as soon as the
 *               multimeter receives the InitImm command.
 *           
 *            ri4152a_trigCoun
 *           
 *               Set to 1.  This is the number of trigger events which will
 *               occur to complete the measurement.  For each trigger count,
 *               sampCoun readings will be taken after a trigger event.  For
 *               example, if trigCoun was set to 2 and sampCoun is set to 20,
 *               then a measurement would consistant of 2 bursts of 20
 *               readings, with each burst occurring when the trigger event
 *               specified by trigSour occurs.
 *           
 *            ri4152a_trigDel
 *           
 *               The automatic trigger delay feature is enabled, which will
 *               cause the multimeter to delay a certain period after a
 *               trigger is received and before a sample is taken. This delay
 *               is different for each measurement function.  The auto delay
 *               feature may be disabled if it is desired to wait longer
 *               before taking samples so that a switch may settle or the
 *               signal may stabilize in some way before samples are taken.
 *               It is not a good idea to shorten this delay time less than
 *               the default time, because the reading integrity will be
 *               affected.
 *           
 *            ri4152a_inpImpAuto
 *           
 *               impImpAuto is set to OFF (0).  For DC voltage measurements
 *               on ranges <= 10 Volts, the input impedance will remain a
 *               constant 10 MegaOhm.  When set to ON (1) the impedance
 *               is changed to 10 GigaOhm. This only affects DC voltage
 *               measurements on ranges <= 10 Volts.
 *           
 *            ri4152a_detBand
 *           
 *               Set to 20 Hz.  This only affects AC measurements.
 *           
 *           
 *            ri4152a_voltAcRang  (only when function is AC Voltage)
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltAcRes  (only when function is AC Voltage)
 *           
 *               Set to 10 microvolts.
 *           
 *            ri4152a_voltDcRang  (only when function is DC Voltage)
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_voltDcRes  (only when function is DC Voltage)
 *           
 *               Set to 10 microvolts.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_voltDcNplc  (only when function is DC Voltage)
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_voltDcAper  (only when function is DC Voltage)
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRang  (only when function is Res or Fres)
 *           
 *               Set to 1000 Ohm range, and auto-ranging is enabled.
 *           
 *            ri4152a_resNplc  (only when function is Res or Fres)
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_resAper  (only when function is Res or Fres)
 *           
 *               Set to 1.6667s if 60 Hz power, 2.0s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_resRes  (only when function is Res or Fres)
 *           
 *               Set to 0.001 Ohm.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_currAcRang  (only when function is AC current)
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currAcRes  (only when function is AC current)
 *           
 *               Set to 10 microAmps.
 *           
 *            ri4152a_currDcRang  (only when function is DC current)
 *           
 *               Set to 1 Amp range, and auto-ranging is enabled.
 *           
 *            ri4152a_currDcRes  (only when function is DC current)
 *           
 *               Set to 1 microAmp.  This setting is coupled to the number
 *               power line cycles and aperture settings; changing one forces
 *               a change in the others.
 *           
 *            ri4152a_currDcNplc  (only when function is DC current)
 *           
 *               Selects 10 power line cycles of integration.  This setting
 *               is coupled to the aperture and resolution settings,
 *               changing one forces a change in the others.
 *           
 *            ri4152a_currDcAper  (only when function is DC current)
 *           
 *               Set to 0.16667s if 60 Hz power, 0.20s if 50 Hz power.  This
 *               setting is coupled to the number power line cycles and
 *               resolution settings; changing one forces a change in the
 *               others.
 *           
 *            ri4152a_freqVoltRang  (only when function is Frequency)
 *            ri4152a_perVoltRang  (only when function is Period)
 *           
 *               Set to 10 Volt range, and auto-ranging is enabled.
 *           
 *            ri4152a_freqAper  (only when function is Frequency)
 *            ri4152a_perAper  (only when function is Period)
 *           
 *               Set to 0.1 seconds.
 *           
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 function
 * IN        
 *            The desired function to configure the multimeter to measure. 
 *           The allowable settings are as shown below.
 * 
 * PARAM 3 : ViPReal64 reading
 * OUT       
 *            The result of the measurement is returned in this parameter.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_measure_Q_function_a[] = {"FREQ","PER",
        "FRES","RES","VOLT:AC","VOLT","CURR:AC","CURR","VOLT:RAT",0};
ViStatus _VI_FUNC ri4152a_measure_Q(ViSession vi,
  ViInt16 function,
  ViPReal64 reading)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_measure_Q" );

    ri4152a_CHK_ENUM(function,8,VI_ERROR_PARAMETER2);
    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"MEAS:%s?\n",ri4152a_measure_Q_function_a[function]);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

	Delay (5.0);
	
    errStatus = viScanf(vi,"%lg%*t",reading);
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_outpTtlt_M
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Enables or disables routing of the "Voltmeter Complete" signal
 *           to the specified VXIbus trigger line on the backplane P2
 *           connector.
 *            Reset Condition: State = 0 (OFF) on all lines.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 ttltLine
 * IN        
 *            Specifies the TTLtrg line.
 * 
 *      MAX = ri4152a_TTLT_LINE_MAX   7
 *      MIN = ri4152a_TTLT_LINE_MIN   0
 * 
 * PARAM 3 : ViBoolean ttltState
 * IN        
 *            OFF (0) disables or ON (1) enables "Voltmeter Complete" pulse
 *           for the specified trigger line.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_outpTtlt_M(ViSession vi,
  ViInt16 ttltLine,
  ViBoolean ttltState)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_outpTtlt_M" );

    ri4152a_CHK_INT_RANGE(ttltLine
                         ,ri4152a_TTLT_LINE_MIN
                         ,ri4152a_TTLT_LINE_MAX
                         ,VI_ERROR_PARAMETER2);

    ri4152a_CHK_BOOLEAN(ttltState,VI_ERROR_PARAMETER3);

    errStatus = viPrintf(vi,"OUTP:TTLT%d %u\n",(int)ttltLine,(int)ttltState);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_outpTtlt_M_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current state of the specified TTL
 *           trigger line.  A 1 indicates the line is enabled to route the
 *           "Voltmeter Complete" pulse; a 0 indicates the line is disabled.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 ttltLine
 * IN        
 *            Specifies the TTLTrg trigger line to be queried.
 * 
 *      MAX = ri4152a_TTLT_LINE_MAX   7
 *      MIN = ri4152a_TTLT_LINE_MIN   0
 * 
 * PARAM 3 : ViPBoolean ttltState
 * OUT       
 *            Returns 0 if "Voltmeter Complete" is disabled, or 1 if enabled.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_outpTtlt_M_Q(ViSession vi,
  ViInt16 ttltLine,
  ViPBoolean ttltState)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_outpTtlt_M_Q" );

    ri4152a_CHK_INT_RANGE(ttltLine
                         ,ri4152a_TTLT_LINE_MIN
                         ,ri4152a_TTLT_LINE_MAX
                         ,VI_ERROR_PARAMETER2);

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"OUTP:TTLT%d?\n",(int)ttltLine);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",ttltState);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_perAper
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in seconds for period measurements.
 *           The multimeter rounds values UP to the nearest time.
 *           
 *            The number of digits of accuracy is determined by the aperture
 *           setting.  The relationship is shown below.
 *           
 *            Aperture     Digits
 *            ------------------------
 *            0.01         4.5
 *            0.1          5.5
 *            1.0          6.5
 *           
 *            Additionally, the larger the aperture setting, the longer the
 *           measurement takes to complete.
 *            Reset Condition: 0.1 seconds
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 aperture
 * IN        
 *            Sets the integration time in seconds (aperture) for period
 *           measurements. The allowable settings are 0.01, 0.1, and 1
 *           seconds.
 * 
 *      MAX = ri4152a_PER_APER_MAX   1.0
 *      MIN = ri4152a_PER_APER_MIN   0.01
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_perAper(ViSession vi,
  ViReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_perAper" );

    ri4152a_CHK_REAL_RANGE(aperture
                          ,ri4152a_PER_APER_MIN
                          ,ri4152a_PER_APER_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"PER:APER %lg\n",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_perAper_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of period aperture.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 aperture
 * OUT       
 *            Returns current integration time setting in seconds for period
 *           measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_perAper_Q(ViSession vi,
  ViPReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_perAper_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"PER:APER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_perVoltRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of period voltage
 *           range, and whether auto ranging is enabled or not.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the currently used period voltage range.  The possible
 *           return values are 100 millivolts, 1 Volt, 10 Volts, 100 Volts
 *           and 300 Volts.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_perVoltRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_perVoltRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"PER:VOLT:RANG:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viPrintf(vi,"PER:VOLT:RANG?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_perVoltRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects a specific voltage range for period measurements, or
 *           turns auto ranging on/off.
 *           
 *            Reset Condition: 10.0 volts
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the period range to the nearest range >= to the level
 *           specified.  The actual voltage ranges are 100 millivolts, 1
 *           Volt, 10 Volts, 100 Volts and 300 Volts.
 * 
 *      MAX = ri4152a_VOLT_RANG_MAX   300.0
 *      MIN = ri4152a_VOLT_RANG_MIN   100.0e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_perVoltRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_perVoltRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the voltRange parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"PER:VOLT:RANG:AUTO 1\n" );
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(range
                             ,ri4152a_VOLT_RANG_MIN
                             ,ri4152a_VOLT_RANG_MAX
                             ,VI_ERROR_PARAMETER3);
    
       errStatus = viPrintf(vi,"PER:VOLT:RANG %lg\n", range );
    }

    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_read_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Places the multimeter in the wait-for-trigger state and
 *           transfers readings directly to the output buffer after receiving
 *           a trigger.  Because multimeter memory is not used to store the
 *           readings, there is no restriction on the sample count and
 *           trigger count.  Because the readings are formatted before being
 *           sent to the output buffer, the sample rate is not as high as the
 *           one attainable using the ri4152a_initImm and ri4152a_fetc_Q routines.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 _VI_FAR readings[]
 * OUT       
 *            Returns the readings taken by the multimeter.  The array
 *           pointer passed in must point to an array large enough to hold
 *           the data.  If the array is not large enough, bad things may
 *           happen as we index off the end of the array.
 * 
 * PARAM 3 : ViPInt32 numReadings
 * OUT       
 *            Returns the number of readings fetched.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_read_Q(ViSession vi,
  ViReal64 _VI_FAR readings[],
  ViPInt32 numReadings)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    int done;
    char separator;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_read_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"READ?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    *numReadings = 1;
    done = 0;
    while (!done)
    {
       errStatus = viScanf(vi, "%lg%c", &readings[*numReadings - 1], &separator);
       if (errStatus < VI_SUCCESS)
       {
          *numReadings -= 1;
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       if (separator != ',')
       {
          done = 1;
    
        /* get rid of any left over stuff */
          errStatus = viFlush(vi, VI_READ_BUF_DISCARD);
          if (errStatus < VI_SUCCESS)
          {
             ri4152a_CDE_MESSAGE( "Error flushing buffer" );
             ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
          }
       }
       else
          *numReadings += 1;
    }
       
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resAper
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in seconds for resistance
 *           measurements. The multimeter rounds values UP to the nearest
 *           time. See also the ri4152a_resRes and ri4152a_resNplc functions
 *           because changing aperture affects the setting on those two
 *           functions as well (they are coupled).
 *           
 *            Reset Condition: 166.7e-3
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 aperture
 * IN        
 *            Sets the integration time in seconds (aperture) for resistance
 *           measurements. Input values are rounded UP to the nearest
 *           aperture time shown in the table below.
 *           
 *            Aperture is one of three ways to set the resolution of the
 *           reading.  The other two are resRes and resNplc.  The
 *           relationships between range, resolution, aperture and NPLC's
 *           (Number Power Line Cycles) is shown below.
 *            Aperture is determined by the NPLC setting; for example, for 1
 *           power line cycle of 60 Hz power, the aperture is 16.7
 *           milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power
 *           source were 50 Hz instead of 60 Hz, the above numbers would be
 *           20.0 milliseconds and 200 milliseconds respectively.
 *            In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs). 
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *            100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
 *              1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
 *             10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 *            100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
 *              1M | 300e-3  |   1    |   3    |  10      |  100     |
 *             10M |   3     |  10    |  30    | 100      |    1e3   |
 *            100M |  30     | 100    | 300    |   1e3    |   10e3   |
 *           ---------------------------------------------------------
 *            Ohms |          Resolution  Ohms                       |
 *           ---------------------------------------------------------
 * 
 *      MAX = ri4152a_APER_MAX   2.0
 *      MIN = ri4152a_APER_MIN   0.400e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resAper(ViSession vi,
  ViReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resAper" );

    ri4152a_CHK_REAL_RANGE(aperture
                          ,ri4152a_APER_MIN
                          ,ri4152a_APER_MAX
                          ,VI_ERROR_PARAMETER2);

    /* if aperture is largest, then send max to avoid getting
       error if sent macro APER_MAX when in 60 Hz mode.  This
       causes settings conflict if just send the number.
    */
    if (is_four_wire) {
    	if (aperture > 1.5)
	        errStatus = viPrintf(vi,"FRES:APER MAX\n");
    	else
        	errStatus = viPrintf(vi,"FRES:APER %lg\n",aperture);
    }
	else {
	    	if (aperture > 1.5)
	        	errStatus = viPrintf(vi,"RES:APER MAX\n");
    		else
        		errStatus = viPrintf(vi,"RES:APER %lg\n",aperture);
	}
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resAper_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of resistance
 *           aperture.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 aperture
 * OUT       
 *            Returns current integration time setting in seconds for
 *           resistance measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resAper_Q(ViSession vi,
  ViPReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resAper_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    if (is_four_wire) errStatus = viPrintf(vi,"FRES:APER?\n");
    else errStatus = viPrintf (vi, "RES:APER?\n");
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resNplc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in Power Line Cycles (PLC) for
 *           resistance measurements. The multimeter rounds values UP to the
 *           nearest time. See also the ri4152a_resRes and ri4152a_resAper
 *           functions because changing NPLC affects the setting on those two
 *           functions as well (they are coupled).
 *           
 *            Reset Condition: 10.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 nplc
 * IN        
 *            Sets the integration time in Power Line Cycles (PLCs) for
 *           resistance measurements.  Input values are rounded UP to the
 *           nearest aperture time shown in the table below.
 *           
 *            NPLC is one of three ways to set the resolution of the reading.
 *            The other two are resRes and resAper.  The relationships
 *           between range, resolution, aperture and NPLC's (Number Power
 *           Line Cycles) is shown below.
 *            Aperture is determined by the NPLC setting; for example, for 1
 *           power line cycle of 60 Hz power, the aperture is 16.7
 *           milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power
 *           source were 50 Hz instead of 60 Hz, the above numbers would be
 *           20.0 milliseconds and 200 milliseconds respectively.
 *            In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs). 
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *            100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
 *              1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
 *             10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 *            100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
 *              1M | 300e-3  |   1    |   3    |  10      |  100     |
 *             10M |   3     |  10    |  30    | 100      |    1e3   |
 *            100M |  30     | 100    | 300    |   1e3    |   10e3   |
 *           ---------------------------------------------------------
 *            Ohms |          Resolution  Ohms                       |
 *           ---------------------------------------------------------
 * 
 *      MAX = ri4152a_NPLC_MAX   100.0
 *      MIN = ri4152a_NPLC_MIN   0.02
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resNplc(ViSession vi,
  ViReal64 nplc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resNplc" );

    ri4152a_CHK_REAL_RANGE(nplc
                          ,ri4152a_NPLC_MIN
                          ,ri4152a_NPLC_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"RES:NPLC %lg\n",nplc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resNplc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of resistance number
 *           of power line cycles integration time (NPLC's).
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 nplc
 * OUT       
 *            Returns the current integration time in power line cycles
 *           (PLCs) for DC resistance measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resNplc_Q(ViSession vi,
  ViPReal64 nplc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resNplc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    if (is_four_wire) 
    	errStatus = viPrintf(vi,"FRES:NPLC?\n");
    else
		errStatus = viPrintf(vi,"RES:NPLC?\n");
		
    if (errStatus < VI_SUCCESS){
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",nplc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects a specific range for ohms measurements or turns auto
 *           ranging on/off.
 *           
 *            Reset Condition: 1000.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the resistance range to the nearest range >= to the level
 *           specified.  The actual resistance ranges are 100 Ohms, 1 kOhms,
 *           10 kOhms, 100 kOhms, 1 MOhms, 10 MOhms, and 100 MOhms.
 * 
 *      MAX = ri4152a_RES_RANG_MAX   100.0e6
 *      MIN = ri4152a_RES_RANG_MIN   100.0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    ri4152a_CHK_REAL_RANGE(range
                          ,ri4152a_RES_RANG_MIN
                          ,ri4152a_RES_RANG_MAX
                          ,VI_ERROR_PARAMETER3);

    /* if AUTO range, then ignore the range parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       if (is_four_wire) errStatus = viPrintf(vi,"FRES:RANG:AUTO ON\n" );
       else			     errStatus = viPrintf(vi,"RES:RANG:AUTO ON\n" ); 
       if (errStatus < VI_SUCCESS)
       {	
          ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
       }
    }
    else
    {
       ri4152a_CHK_REAL_RANGE( range, ri4152a_RES_RANG_MIN, ri4152a_RES_RANG_MAX, VI_ERROR_PARAMETER3 );
       if (is_four_wire) errStatus = viPrintf(vi,"FRES:RANG %lg\n", range );
       else        	     errStatus = viPrintf(vi,"RES:RANG %lg\n", range );

       if (errStatus < VI_SUCCESS)
       
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of resistance range,
 *           and whether auto ranging is enabled or not.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the currently used resistance range.  The possible
 *           return values are 100 Ohms, 1 kOhms, 10 kOhms, 100 kOhms, 1
 *           MOhms, 10 MOhms, and 100 MOhms.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    if (is_four_wire)  errStatus = viPrintf(vi,"FRES:RANG:AUTO?\n");
    else errStatus = viPrintf (vi, "RES:RANG:AUTO?\n");
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }
	if (is_four_wire) errStatus = viPrintf (vi, "FRES:RANG?\n");
    else errStatus = viPrintf(vi,"RES:RANG?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resRes
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the resolution for resistance measurements.
 *            See also the ri4152a_resAper and ri4152a_resNplc functions
 *           because changing resolution affects the setting on those two
 *           functions as well (they are coupled).
 *           
 *            Reset Condition: 1.0e-3
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 resolution
 * IN        
 *            Specifies the resolution for resistance measurements.  Note
 *           that resolution is is really determined by aperture or NPLC
 *           settings, and so the minimum aperture on A GIVEN RANGE
 *           determines the resolution; thus 10 nanoVolts of resolution is
 *           only possible on the lowest range, and if selected on a higher
 *           range will result in an error from the instrument.  See the
 *           table below for the settings possible.
 *            Also, selecting a resolution changes the aperture and NPLC
 *           settings, see the second table below for the interactions of
 *           Range, Resolution, Aperture, and NPLCs.
 *           
 *            Macro Name               	Value           Description
 *           ---------------------------------------------------------------
 *            ri4152a_RES_RES_30_MICRO	30.0e-6 	For range = 100 Ohms
 *            ri4152a_RES_RES_100_MICRO	100.0e-6	For range = 100 Ohms
 *            ri4152a_RES_RES_300_MICRO	300.0e-6	For range <= 1 kOhms
 *            ri4152a_RES_RES_1_MILLI	1.0e-3  	For range <= 1 kOhms
 *            ri4152a_RES_RES_3_MILLI	3.0e-3  	For range <= 10 kOhms
 *            ri4152a_RES_RES_10_MILLI	10.0e-3  	For range <= 10 kOhms
 *            ri4152a_RES_RES_30_MILLI	30.0e-3  	For range <= 100 kOhms
 *            ri4152a_RES_RES_100_MILLI	100.0e-3  	For range <= 100 kOhms
 *            ri4152a_RES_RES_300_MILLI	300.0e-3  	For range <=   1 MOhms
 *            ri4152a_RES_RES_1		1.0 		For range <=   1 MOhms
 *            ri4152a_RES_RES_3		3.0 		For range <=  10 MOhms
 *            ri4152a_RES_RES_10		10.0 		For range <=  10 MOhms
 *            ri4152a_RES_RES_30		30.0 		For all ranges
 *            ri4152a_RES_RES_100		100.0 		For all ranges
 *            ri4152a_RES_RES_300		300.0 		For all ranges
 *            ri4152a_RES_RES_1K		1.0e3 		For all ranges
 *            ri4152a_RES_RES_10K		10.0e3 		For all ranges
 *           
 *           In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs).
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *            100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
 *              1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
 *             10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 *            100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
 *              1M | 300e-3  |   1    |   3    |  10      |  100     |
 *             10M |   3     |  10    |  30    | 100      |    1e3   |
 *            100M |  30     | 100    | 300    |   1e3    |   10e3   |
 *           ---------------------------------------------------------
 *            Ohms |          Resolution  Ohms                       |
 *           ---------------------------------------------------------
 *           
 * 
 *      MAX = ri4152a_RES_RES_MAX   30.0e3
 *      MIN = ri4152a_RES_RES_MIN   30.0e-6
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resRes(ViSession vi,
  ViReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resRes" );

    ri4152a_CHK_REAL_RANGE(resolution
                          ,ri4152a_RES_RES_MIN
                          ,ri4152a_RES_RES_MAX
                          ,VI_ERROR_PARAMETER2);
                          
	if (is_four_wire) errStatus = viPrintf(vi,"FRES:RES %lg\n",resolution);
	else errStatus = viPrintf(vi,"RES:RES %lg\n",resolution);

    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_resRes_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of resistance
 *           resolution.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 resolution
 * OUT       
 *            Returns current resistance resolution.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_resRes_Q(ViSession vi,
  ViPReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_resRes_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    
    if (is_four_wire) errStatus = viPrintf(vi,"FRES:RES?\n");
    else errStatus = viPrintf(vi,"RES:RES?\n");    
    
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_sampCoun
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Specifies the number of readings per trigger.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt32 sampCoun
 * IN        
 *            Sets the number of readings per trigger.  The allowable range
 *           is as follows.
 * 
 *      MAX = ri4152a_SAMP_COUN_MAX   50000
 *      MIN = ri4152a_SAMP_COUN_MIN   1
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_sampCoun(ViSession vi,
  ViInt32 sampCoun)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_sampCoun" );

    ri4152a_CHK_LONG_RANGE(sampCoun
                          ,ri4152a_SAMP_COUN_MIN
                          ,ri4152a_SAMP_COUN_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"SAMP:COUN %ld\n",sampCoun);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_sampCoun_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of sample count.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt32 sampCoun
 * OUT       
 *            Returns the latest number of readings per trigger (values
 *           between 1 and 16777215).
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_sampCoun_Q(ViSession vi,
  ViPInt32 sampCoun)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_sampCoun_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"SAMP:COUN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%ld%*t",sampCoun);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_systLfr
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine selects the line reference frequency used by the A
 *           to D converter. This function is provided for compatibility with
 *           other multimeters which use the SYSTem:LFRequency command
 *           instead of CALibration:LFRequency command.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 systLfr
 * IN        
 *            Indicates the desired line reference frequency; the legal
 *           values are 50 or 60.
 * 
 *      MAX = ri4152a_SYST_LFR_MAX   400
 *      MIN = ri4152a_SYST_LFR_MIN   50
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_systLfr(ViSession vi,
  ViInt16 systLfr)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_systLfr" );

    ri4152a_CHK_INT_RANGE(systLfr
                         ,ri4152a_SYST_LFR_MIN
                         ,ri4152a_SYST_LFR_MAX
                         ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"CAL:LFR %d\n",(int)systLfr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_systLfr_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of the reference line
 *           frequency. This function is provided for compatibility with
 *           other multimeters which use the SYSTem:LFRequency? command
 *           instead of CALibration:LFRequency? command.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 systLfr
 * OUT       
 *            Returns the current setting of the reference line frequency;
 *           either 50 or 60 Hz.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_systLfr_Q(ViSession vi,
  ViPInt16 systLfr)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_systLfr_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"CAL:LFR?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",systLfr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_timedFetch_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine returns readings stored in memory from the last
 *           measurement operation, and the numReadings parameter indicates
 *           how many readings were returned in the readings array.  The
 *           timeOut parameter allows a time out value to be used only during
 *           this function, with the original time out value being re-stored
 *           after the data is fetched.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt32 timeOut
 * IN        
 *            The value (in milli-seconds) to set the time out value to for
 *           the duration of this function.  When the function completes, the
 *           original time out value is re-stored.
 * 
 *      MAX = ri4152a_TIMEOUT_MAX   2147483647
 *      MIN = ri4152a_TIMEOUT_MIN   0
 * 
 * PARAM 3 : ViReal64 _VI_FAR readings[]
 * OUT       
 *            The array of double which will hold the readings.
 * 
 * PARAM 4 : ViPInt32 numReadings
 * OUT       
 *            Returns the number of readings fetched.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_timedFetch_Q(ViSession vi,
  ViInt32 timeOut,
  ViReal64 _VI_FAR readings[],
  ViPInt32 numReadings)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    ViInt32 saveTimeOut;
    int done;
    char separator;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_timedFetch_Q" );

    ri4152a_CHK_LONG_RANGE(timeOut
                          ,ri4152a_TIMEOUT_MIN
                          ,ri4152a_TIMEOUT_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viGetAttribute(vi, VI_ATTR_TMO_VALUE, &saveTimeOut );
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }
    
    errStatus = viSetAttribute(vi, VI_ATTR_TMO_VALUE, timeOut);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }
    
    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"FETC?\n");
    if (errStatus < VI_SUCCESS)
    {
       /* re-store original timeOut value before exiting */
       viSetAttribute(vi, VI_ATTR_TMO_VALUE, saveTimeOut);
       
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    *numReadings = 1;
    done = 0;
    while (!done)
    {
       errStatus = viScanf(vi, "%lg%c", &readings[*numReadings - 1], &separator);
       if (errStatus < VI_SUCCESS)
       {
          *numReadings -= 1;
    
          /* re-store original timeOut value before exiting */
          viSetAttribute(vi, VI_ATTR_TMO_VALUE, saveTimeOut);
          
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    
       if (separator != ',')
       {
          done = 1;
    
        /* get rid of any left over stuff */
          errStatus = viFlush(vi, VI_READ_BUF_DISCARD);
          if (errStatus < VI_SUCCESS)
          {
             
             /* re-store original timeOut value before exiting */
             viSetAttribute(vi, VI_ATTR_TMO_VALUE, saveTimeOut);
    
             ri4152a_CDE_MESSAGE( "Error flushing buffer" );
             ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
          }
       }
       else
          *numReadings += 1;
    }
    
    errStatus = viSetAttribute(vi, VI_ATTR_TMO_VALUE, saveTimeOut);
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigCoun
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Specifies the trigger count.  This is the number of "bursts" of
 *           samples that will be taken.  Normally, the multimeter only has
 *           enough memory to store 4096 readings.  This limits the trigger
 *           count * number of samples to be <= 4096. If you require more
 *           readings than this, see the ri4152a_read_Q function for a way to
 *           get data directly from the multimeter without storing the data
 *           into memory first.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt32 trigCoun
 * IN        
 *            Sets the number of triggers issued.
 * 
 *      MAX = ri4152a_TRIG_COUN_MAX   50000
 *      MIN = ri4152a_TRIG_COUN_MIN   1
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_trigCoun(ViSession vi,
  ViInt32 trigCoun)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigCoun" );

    ri4152a_CHK_LONG_RANGE(trigCoun
                          ,ri4152a_TRIG_COUN_MIN
                          ,ri4152a_TRIG_COUN_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"TRIG:COUN %ld\n",trigCoun);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigCoun_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of trigger count.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt32 trigCoun
 * OUT       
 *            Returns the number of trigger counts set.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_trigCoun_Q(ViSession vi,
  ViPInt32 trigCoun)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigCoun_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"TRIG:COUN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%ld%*t",trigCoun);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigDel
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets delay period between receipt of trigger and start of
 *           measurement.  One use of this would be to specify a longer delay
 *           to give a signal extra time to settle after a relay closure
 *           before measurement.
 *            Note that if sample count is > 1, this delay will also occur
 *           before each additional sample beyond the first one.
 *           
 *            If trigDelAuto is enabled, the following delays occur:
 *           
 *                              Range or                 Automatic
 *            Function          Condition              Trigger Delay
 *           ---------------------------------------------------------------
 *           DC Voltage         All Ranges with:
 *             and               NPLC >= 1                1.5 mS
 *           DC Current          NPLC < 1                 1.0 mS
 *           ---------------------------------------------------------------
 *           Resistance         Range (ohms)             NPLC >=1    NPLC < 1
 *                               100                     1.5 mS      1.0 mS
 *                               1000                    1.5 mS      1.0 mS
 *                               10000                   1.5 mS      1.0 mS
 *                               100e3                   1.5 mS      1.0 mS
 *                               1e6                     1.5 mS      1.0 mS
 *                               10e6                    100 mS      100 mS
 *                               100e6                   100 mS      100 mS
 *           ---------------------------------------------------------------
 *           AC Voltage        All Ranges with
 *             and             Detector Bandwidth:
 *           AC Current           3                       7.0 S
 *                               20                       1.0 S
 *                              200                       600 mS
 *           ---------------------------------------------------------------
 *           Frequency
 *             and
 *           Period              All Ranges               1.0 S
 *           ---------------------------------------------------------------
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean trigDelAuto
 * IN        
 *            Enables (1) or disables (0) the auto delay feature.
 * 
 * PARAM 3 : ViReal64 trigDel
 * IN        
 *            Sets delay period between receipt of trigger and start of
 *           measurement.  This value is ignored and not used if the
 *           trigDelAuto parameter is enabled (1).
 * 
 *      MAX = ri4152a_TRIG_DEL_MAX   3600
 *      MIN = ri4152a_TRIG_DEL_MIN   0
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_trigDel(ViSession vi,
  ViBoolean trigDelAuto,
  ViReal64 trigDel)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigDel" );

    ri4152a_CHK_BOOLEAN(trigDelAuto,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the voltRange parameter */
    if (trigDelAuto) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"TRIG:DEL:AUTO 1\n" );
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(trigDel
                             ,ri4152a_TRIG_DEL_MIN
                             ,ri4152a_TRIG_DEL_MAX
                             ,VI_ERROR_PARAMETER3);

       errStatus = viPrintf(vi,"TRIG:DEL %lg\n", trigDel );
    }

    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigDel_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of trigger delay.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean trigDelAuto
 * OUT       
 *            Returns current setting of auto delay feature.
 * 
 * PARAM 3 : ViPReal64 trigDel
 * OUT       
 *            Returns current trigger delay period.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_trigDel_Q(ViSession vi,
  ViPBoolean autoDelay,
  ViPReal64 delay)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigDel_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"TRIG:DEL:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoDelay);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viPrintf(vi,"TRIG:DEL?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",delay);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigSour
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the trigger source to which the multimeter is to
 *           respond.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt16 trigSour
 * IN        
 *            Selects the trigger source, as follows.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_trigSour_trigSour_a[] = {"BUS","EXT",
        "IMM","TTLT0","TTLT1","TTLT2","TTLT3","TTLT4","TTLT5","TTLT6","TTLT7",0};
ViStatus _VI_FUNC ri4152a_trigSour(ViSession vi,
  ViInt16 trigSour)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigSour" );

    ri4152a_CHK_ENUM(trigSour,10,VI_ERROR_PARAMETER2);
    errStatus = viPrintf(vi,"TRIG:SOUR %s\n",ri4152a_trigSour_trigSour_a[trigSour]);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigSour_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of the trigger source.
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt16 trigSour
 * OUT       
 *            Returns an integer representing one of the following trigger
 *           sources.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_trigSour_Q_trigSour_a[] = {"BUS","EXT",
        "IMM","TTLT0","TTLT1","TTLT2","TTLT3","TTLT4","TTLT5","TTLT6","TTLT7",0};
ViStatus _VI_FUNC ri4152a_trigSour_Q(ViSession vi,
  ViPInt16 trigSour)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    char trigSour_str[32];

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigSour_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"TRIG:SOUR?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%s%*t",trigSour_str);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = ri4152a_findIndex(thisPtr,ri4152a_trigSour_Q_trigSour_a,
        trigSour_str, trigSour);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigger
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine sets up the trigger system with a single function
 *           call.  Note that each of the trigger settings in this call may
 *           also be set individually by the function of a similar name as
 *           the parameter (i.e. trigger source can also be set by function
 *           ri4152a_trigSour() ).
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViInt32 count
 * IN        
 *            Specifies the trigger count.  This is the number of "bursts" of
 *           samples that will be taken.  Normally, the multimeter only has
 *           enough memory to store 512 readings.  This limits the trigger
 *           count * number of samples to be <= 512. If you require more
 *           readings than this, see the ri4152a_read_Q function for a way to
 *           get data directly from the multimeter without storing the data
 *           into memory first.  The min and max values of trigger count are
 *           defined as macros shown below.
 * 
 *      MAX = ri4152a_TRIG_COUN_MAX   50000
 *      MIN = ri4152a_TRIG_COUN_MIN   1
 * 
 * PARAM 3 : ViBoolean autoDelay
 * IN        
 *            Enables (1) or disables (0) selection of an automatic trigger
 *           delay.  This is the time period from receipt of the trigger
 *           until the start of the first reading.  The automatic delay
 *           varies by function, but may be changed by the user with the
 *           delay parameter when autoDelay is set to OFF (0).
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 4 : ViReal64 delay
 * IN        
 *            This specifies the amount of time (in seconds) to delay after
 *           receipt of the trigger before taking the first sample.  This
 *           value is ignored if trigDelAuto is ON (1).  One reason for
 *           specifying a specific delay time is to allow extra time for a
 *           relay to settle when measurement signals are being switched
 *           (multiplexed) to the multimeter.
 *            Note:  If sample count is > 1, this delay will also occur
 *           before EACH additional sample instead of only before the first
 *           sample.
 *            The min and max values for delay are shown below.
 * 
 *      MAX = ri4152a_TRIG_DEL_MAX   3600
 *      MIN = ri4152a_TRIG_DEL_MIN   0
 * 
 * PARAM 5 : ViInt16 source
 * IN        
 *            Selects the trigger source, as follows.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_trigger(ViSession vi,
  ViInt32 count,
  ViBoolean autoDelay,
  ViReal64 delay,
  ViInt16 source)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigger" );

    ri4152a_CHK_LONG_RANGE(count
                          ,ri4152a_TRIG_COUN_MIN
                          ,ri4152a_TRIG_COUN_MAX
                          ,VI_ERROR_PARAMETER2);

    ri4152a_CHK_BOOLEAN(autoDelay,VI_ERROR_PARAMETER3);

    ri4152a_CHK_ENUM(source,10,VI_ERROR_PARAMETER5);

    if (autoDelay)
    {
       errStatus = viPrintf(vi,"TRIG:COUN %ld;SOUR %s;DEL:AUTO 1\n",
                     count, ri4152a_trigSour_trigSour_a[source]);
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(delay
                             ,ri4152a_TRIG_DEL_MIN
                             ,ri4152a_TRIG_DEL_MAX
                             ,VI_ERROR_PARAMETER4);

       errStatus = viPrintf(vi,"TRIG:COUN %ld;DEL %lf;SOUR %s\n",
                     count, delay, ri4152a_trigSour_trigSour_a[source]);
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_trigger_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries all of the trigger system settings in a
 *           single call.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPInt32 count
 * OUT       
 *            Returns the trigger count.
 * 
 * PARAM 3 : ViPBoolean autoDelay
 * OUT       
 *            Returns the current setting of automatic trigger delay: 1 (ON)
 *           or 0 (OFF).
 * 
 * PARAM 4 : ViPReal64 delay
 * OUT       
 *            Returns the current setting of trigger delay.  If autoDelay is
 *           1, then the delay returned here is the value automatically set
 *           by the multimeter; otherwise, the delay returned is the one
 *           previously set by the user.
 * 
 * PARAM 5 : ViPInt16 trigSour
 * OUT       
 *            Returns the current setting of trigger source.  This will be an
 *           integer that represents one of the following settings:
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
static const char * const ri4152a_trigger_Q_trigSour_a[] = {"BUS","EXT",
        "IMM","TTLT0","TTLT1","TTLT2","TTLT3","TTLT4","TTLT5","TTLT6","TTLT7",0};
ViStatus _VI_FUNC ri4152a_trigger_Q(ViSession vi,
  ViPInt32 count,
  ViPBoolean autoDelay,
  ViPReal64 delay,
  ViPInt16 source)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;
    char source_str[32];

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_trigger_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi, "TRIG:COUN?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%ld%*t", count);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viPrintf(vi, "TRIG:DEL?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%lg%*t", delay);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viPrintf(vi, "TRIG:DEL:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%hd%*t", autoDelay);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viPrintf(vi, "TRIG:SOUR?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    errStatus = viScanf(vi, "%s%*t", source_str);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    /* get index of trigSour type */
    errStatus = ri4152a_findIndex(thisPtr, ri4152a_trigSour_trigSour_a,
                         source_str, source);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
       
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltAcRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the range for the AC volts measurements.
 *           
 *            Reset Condition: 10.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the AC voltage range. The allowable ranges are 100
 *           millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts.
 * 
 *      MAX = ri4152a_VOLT_RANG_MAX   300.0
 *      MIN = ri4152a_VOLT_RANG_MIN   100.0e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltAcRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltAcRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the voltRange parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"VOLT:AC:RANG:AUTO 1\n" );
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(range
                             ,ri4152a_VOLT_RANG_MIN
                             ,ri4152a_VOLT_RANG_MAX
                             ,VI_ERROR_PARAMETER3);

       errStatus = viPrintf(vi,"VOLT:AC:RANG %lg\n", range );
    }

    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
    }
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltAcRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of voltage range and
 *           whether auto ranging is enabled or not.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the current AC voltage range setting.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltAcRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltAcRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"VOLT:AC:RANG:AUTO?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viPrintf(vi,"VOLT:AC:RANG?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltAcRes
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the resolution for AC voltage measurements.
 *           
 *            Reset Condition: 1.0e-4
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 resolution
 * IN        
 *            Specifies the resolution for AC voltage measurements.  Note
 *           that the resolution is a function of range; thus 100 nanoVolts
 *           of resolution is only possible on the lowest range, and if
 *           selected on a higher range will result in an error from the
 *           instrument.  See the table below for the settings possible.
 *           
 *           -----------------------------------
 *           Range |           Resolutions     |
 *           -----------------------------------
 *           100 mV| 100 nV  |   1 uV |  10 uV |
 *             1 V |   1 uV  |  10 uV | 100 uV |
 *            10 V |  10 uV  | 100 uV |   1 mV |
 *           100 V | 100 uV  |   1 mV |  10 mV |
 *           300 V |   1 mV  |  10 mV | 100 mV |
 *           -----------------------------------
 * 
 *      MAX = ri4152a_VOLT_AC_RES_MAX   3.0
 *      MIN = ri4152a_VOLT_AC_RES_MIN   100.0e-9
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltAcRes(ViSession vi,
  ViReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltAcRes" );

    ri4152a_CHK_REAL_RANGE(resolution
                          ,ri4152a_VOLT_AC_RES_MIN
                          ,ri4152a_VOLT_AC_RES_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"VOLT:AC:RES %lg\n",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltAcRes_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of AC voltage
 *           resolution.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 resolution
 * OUT       
 *            Returns current AC voltage resolution.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltAcRes_Q(ViSession vi,
  ViPReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltAcRes_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"VOLT:AC:RES?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",resolution);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcAper
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in seconds for DC voltage
 *           measurements. The multimeter rounds values UP to the nearest
 *           time. See also the ri4152a_voltDcRes and ri4152a_voltDcNplc
 *           functions because changing aperture affects the setting on those
 *           two functions as well (they are coupled).
 *           
 *            Reset Condition: 166.7e-3
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 aperture
 * IN        
 *            Sets the integration time in seconds (aperture) for DC voltage
 *           measurements. Input values are rounded UP to the nearest
 *           aperture time shown in the table below.
 *           
 *            Aperture is one of three ways to set the resolution of the
 *           reading.  The other two are voltDcRes and voltDcNplc.  The
 *           relationships between range, resolution, aperture and NPLC's
 *           (Number Power Line Cycles) is shown below.
 *            Aperture is determined by the NPLC setting; for example, for 1
 *           power line cycle of 60 Hz power, the aperture is 16.7
 *           milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power
 *           source were 50 Hz instead of 60 Hz, the above numbers would be
 *           20.0 milliseconds and 200 milliseconds respectively.
 *            In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs). 
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *           100 mV|  30 nV  | 100 nV | 300 nV |   1 uV   |   10 uV  |
 *             1 V | 300 nV  |   1 uV |   3 uV |  10 uV   |  100 uV  |
 *            10 V |   3 uV  |  10 uV |  30 uV | 100 uV   |    1 mV  |
 *           100 V |  30 uV  | 100 uV | 300 uV |   1 mV   |   10 mV  |
 *           300 V | 300 uV  |   1 mV |   3 mV |  10 mV   |  100 mV  |
 *           ---------------------------------------------------------
 *                 |                Resolution                       |
 *                 ---------------------------------------------------
 * 
 *      MAX = ri4152a_APER_MAX   2.0
 *      MIN = ri4152a_APER_MIN   0.400e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcAper(ViSession vi,
  ViReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcAper" );

    ri4152a_CHK_REAL_RANGE(aperture
                          ,ri4152a_APER_MIN
                          ,ri4152a_APER_MAX
                          ,VI_ERROR_PARAMETER2);

    /* if aperture is largest, then send max to avoid getting
       error if sent macro APER_MAX when in 60 Hz mode.  This
       causes settings conflict if just send the number.
    */
    if (aperture > 1.5)
        errStatus = viPrintf(vi,"VOLT:APER MAX\n");
    else
        errStatus = viPrintf(vi,"VOLT:APER %lg\n",aperture);

    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcAper_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of DC voltage
 *           aperture.
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 aperture
 * OUT       
 *            Returns current integration time setting in seconds for DC
 *           voltage measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcAper_Q(ViSession vi,
  ViPReal64 aperture)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcAper_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"VOLT:APER?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",aperture);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcNplc
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Sets the integration time in Power Line Cycles (PLC) for DC
 *           voltage measurements. The multimeter rounds values UP to the
 *           nearest time. See also the ri4152a_voltDcRes and
 *           ri4152a_voltDcAper functions because changing NPLC affects the
 *           setting on those two functions as well (they are coupled).
 *           
 *            Reset Condition: 10.0
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 nplc
 * IN        
 *            Sets the integration time in Power Line Cycles (PLCs) for DC
 *           voltage measurements.  Input values are rounded UP to the
 *           nearest aperture time shown in the table below.
 *           
 *            NPLC is one of three ways to set the resolution of the reading.
 *            The other two are voltDcRes and voltDcAper.  The relationships
 *           between range, resolution, aperture and NPLC's (Number Power
 *           Line Cycles) is shown below.
 *            Aperture is determined by the NPLC setting; for example, for 1
 *           power line cycle of 60 Hz power, the aperture is 16.7
 *           milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power
 *           source were 50 Hz instead of 60 Hz, the above numbers would be
 *           20.0 milliseconds and 200 milliseconds respectively.
 *            In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs). 
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *           100 mV|  30 nV  | 100 nV | 300 nV |   1 uV   |   10 uV  |
 *             1 V | 300 nV  |   1 uV |   3 uV |  10 uV   |  100 uV  |
 *            10 V |   3 uV  |  10 uV |  30 uV | 100 uV   |    1 mV  |
 *           100 V |  30 uV  | 100 uV | 300 uV |   1 mV   |   10 mV  |
 *           300 V | 300 uV  |   1 mV |   3 mV |  10 mV   |  100 mV  |
 *           ---------------------------------------------------------
 *                 |                Resolution                       |
 *                 ---------------------------------------------------
 * 
 *      MAX = ri4152a_NPLC_MAX   100.0
 *      MIN = ri4152a_NPLC_MIN   0.02
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcNplc(ViSession vi,
  ViReal64 nplc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS){
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcNplc" );

    ri4152a_CHK_REAL_RANGE(nplc
                          ,ri4152a_NPLC_MIN
                          ,ri4152a_NPLC_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"VOLT:NPLC %lg\n",nplc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcNplc_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of voltage number of
 *           power line cycles integration time (NPLC's).
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *           The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 nplc
 * OUT       
 *            Returns the current integration time in power line cycles
 *           (PLCs) for DC voltage measurements.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcNplc_Q(ViSession vi,
  ViPReal64 nplc)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcNplc_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"VOLT:NPLC?\n");
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    errStatus = viScanf(vi,"%lg%*t",nplc);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcRang
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects a specific range for DC volts measurements or turns
 *           auto ranging on/off.
 *           
 *            Reset Condition: 300.0 volts
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViBoolean autoRange
 * IN        
 *            Enables (1) or disables (0) the auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViReal64 range
 * IN        
 *            Sets the DC voltage range to the nearest range >= to the level
 *           specified.  The actual voltage ranges are 100 millivolts, 1
 *           Volt, 10 Volts, 100 Volts and 300 Volts.
 * 
 *      MAX = ri4152a_VOLT_RANG_MAX   300.0
 *      MIN = ri4152a_VOLT_RANG_MIN   100.0e-3
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcRang(ViSession vi,
  ViBoolean autoRange,
  ViReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcRang" );

    ri4152a_CHK_BOOLEAN(autoRange,VI_ERROR_PARAMETER2);

    /* if AUTO range, then ignore the voltRange parameter */
    if (autoRange) 	/* is AUTO RANGE */
    {
       errStatus = viPrintf(vi,"VOLT:RANG:AUTO ON\n" );
       if (errStatus < VI_SUCCESS)
       {	
          ri4152a_LOG_STATUS( vi, thisPtr, errStatus);
       }
    }
    else
    {
       ri4152a_CHK_REAL_RANGE(range
                             ,ri4152a_VOLT_RANG_MIN
                             ,ri4152a_VOLT_RANG_MAX
                             ,VI_ERROR_PARAMETER3);

       errStatus = viPrintf(vi,"VOLT:RANG %lg\n", range );
       if (errStatus < VI_SUCCESS)
       {
          ri4152a_LOG_STATUS(vi, thisPtr, errStatus );
       }
    }
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcRang_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of DC voltage range,
 *           and whether auto ranging is enabled or not.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPBoolean autoRange
 * OUT       
 *            Returns the current setting: enabled (1) or disabled (0) of the
 *           auto ranging feature.
 * 
 *      MAX = VI_TRUE   1
 *      MIN = VI_FALSE   0
 * 
 * PARAM 3 : ViPReal64 range
 * OUT       
 *            Returns the currently used DC voltage range.  The possible
 *           return values are 100 millivolts, 1 Volt, 10 Volts, 100 Volts
 *           and 300 Volts.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcRang_Q(ViSession vi,
  ViPBoolean autoRange,
  ViPReal64 range)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
       ri4152a_LOG_STATUS( vi, 0, errStatus );

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcRang_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"VOLT:RANG:AUTO?\n");
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    
    errStatus = viScanf(vi,"%hd%*t",autoRange);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    

    errStatus = viPrintf(vi,"VOLT:RANG?\n");
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    

    errStatus = viScanf(vi,"%lg%*t",range);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    
    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcRes
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  Selects the resolution for DC voltage measurements.
 *            See also the ri4152a_voltDcAper and ri4152a_voltDcNplc
 *           functions because changing resolution affects the setting on
 *           those two functions as well (they are coupled).
 *           
 *            Reset Condition: 10.0e-6
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViReal64 resolution
 * IN        
 *            Specifies the resolution for DC voltage measurements.  Note
 *           that resolution is is really determined by aperture or NPLC
 *           settings, and so the minimum aperture on A GIVEN RANGE
 *           determines the resolution; thus 10 nanoVolts of resolution is
 *           only possible on the lowest range, and if selected on a higher
 *           range will result in an error from the instrument.  See the
 *           table below for the settings possible.
 *            Also, selecting a resolution changes the aperture and NPLC
 *           settings, see the second table below for the interactions of
 *           Range, Resolution, Aperture, and NPLCs.
 *           
 *            Macro Name                  Value      Description
 *           ---------------------------------------------------------------
 *            ri4152a_VOLT_DC_RES_30_NANO   30.0e-9     For range = 100e-3 V
 *            ri4152a_VOLT_DC_RES_100_NANO  100.0e-9    For range = 100e-3 V
 *            ri4152a_VOLT_DC_RES_300_NANO  300.0e-9    For range <= 1 V
 *            ri4152a_VOLT_DC_RES_1_MICRO   1.0e-6      For range <= 1 V
 *            ri4152a_VOLT_DC_RES_3_MICRO   3.0e-6      For range <= 10 V
 *            ri4152a_VOLT_DC_RES_10_MICRO  10.0e-6     For range <= 30 V
 *            ri4152a_VOLT_DC_RES_30_MICRO  30.0e-6     For range <= 100 V
 *            ri4152a_VOLT_DC_RES_100_MICRO   100.0e-6  For range <= 100 V
 *            ri4152a_VOLT_DC_RES_300_MICRO   300.0e-6  Valid for all ranges
 *            ri4152a_VOLT_DC_RES_1_MILLI     1.0e-3    Valid for all ranges
 *            ri4152a_VOLT_DC_RES_3_MILLI     3.0e-3    Valid for all ranges
 *            ri4152a_VOLT_DC_RES_10_MILLI    10.0e-3   Valid for all ranges
 *            ri4152a_VOLT_DC_RES_100_MILLI   100.0e-3  Valid for all ranges
 *           
 *           In the following table, resolution is shown as a function of
 *           range and integration time given in Power Line Cycles (PLCs).
 *           The associated aperture is shown for 60 Hz power.
 *           
 *                 --------------------------------------------------|
 *                 | Integration time in Power Line Cycles (PLCs)    |
 *                 | Aperture for 60 Hz power                        |
 *                 --------------------------------------------------
 *           ------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
 *           Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
 *           ---------------------------------------------------------
 *           100 mV|  30 nV  | 100 nV | 300 nV |   1 uV   |   10 uV  |
 *             1 V | 300 nV  |   1 uV |   3 uV |  10 uV   |  100 uV  |
 *            10 V |   3 uV  |  10 uV |  30 uV | 100 uV   |    1 mV  |
 *           100 V |  30 uV  | 100 uV | 300 uV |   1 mV   |   10 mV  |
 *           300 V | 300 uV  |   1 mV |   3 mV |  10 mV   |  100 mV  |
 *           ---------------------------------------------------------
 *                 |                Resolution                       |
 *                 ---------------------------------------------------
 * 
 *      MAX = ri4152a_VOLT_DC_RES_MAX   3.0
 *      MIN = ri4152a_VOLT_DC_RES_MIN   30.0e-9
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcRes(ViSession vi,
  ViReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, 0, errStatus );

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcRes" );

    ri4152a_CHK_REAL_RANGE(resolution
                          ,ri4152a_VOLT_DC_RES_MIN
                          ,ri4152a_VOLT_DC_RES_MAX
                          ,VI_ERROR_PARAMETER2);

    errStatus = viPrintf(vi,"VOLT:RES %lg\n",resolution);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/*-----------------------------------------------------------------------------
 * FUNC    : ViStatus _VI_FUNC ri4152a_voltDcRes_Q
 *-----------------------------------------------------------------------------
 * 
 * PURPOSE :  This routine queries the current setting of voltage resolution.
 *           
 *            Reset Condition: None
 * 
 * PARAM 1 : ViSession vi
 * IN        
 *            The handle to the instrument
 * 
 * PARAM 2 : ViPReal64 resolution
 * OUT       
 *            Returns current DC voltage resolution.
 * 
 * RETURN  :  VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error
 *           condition. To determine error message, pass the return value to
 *           routine "ri4152a_error_message"
 * 
 *-----------------------------------------------------------------------------
 */
ViStatus _VI_FUNC ri4152a_voltDcRes_Q(ViSession vi,
  ViPReal64 resolution)
{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, 0, errStatus );

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_voltDcRes_Q" );

    thisPtr->blockSrqIO = VI_TRUE;
    errStatus = viPrintf(vi,"VOLT:RES?\n");
    if (errStatus < VI_SUCCESS)
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    
    errStatus = viScanf(vi,"%lg%*t",resolution);
    if (errStatus < VI_SUCCESS) {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

/* ========================================================================== */
/* Func: Get System Version                                                   */
/* Description: Gets the system version                                       */
/* ========================================================================== */

ViStatus _VI_FUNC ri4152a_get_syst_vers (ViSession vi, ViPString version)
{
    ViStatus errStatus = 0;
    ViUInt32 cnt;
    ViChar readbuf[496];
    
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, 0, errStatus );

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_get_syst_vers" );

    errStatus = viPrintf(vi,"SYST:VERS?\n");
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    
    errStatus = viRead (vi, (ViPBuf) readbuf, (ViUInt32) sizeof (readbuf), &cnt);
    readbuf[cnt-1] = 0;
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
 
 	strcpy (version, readbuf);
 	
 	return 0;
}

/* ======================================================================== */
/* Function: Cal String                                                     */
/* Description: Sets the calibration string                                 */
/* ======================================================================== */

ViStatus _VI_FUNC ri4152a_cal_string (ViSession vi, ViChar input[])

{
    ViStatus errStatus = 0;
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, 0, errStatus );
    }

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_cal_string" );

    errStatus = viPrintf(vi,"CAL:STR %s\n",input);
    if (errStatus < VI_SUCCESS)
    {
       ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    }

    ri4152a_LOG_STATUS( vi, thisPtr, VI_SUCCESS );
}

ViStatus _VI_FUNC ri4152a_get_cal_string (ViSession vi, ViPString result)
{
    ViStatus errStatus = 0;
    ViUInt32 cnt;
    ViChar readbuf[496];
    
    struct ri4152a_globals *thisPtr;

    errStatus = viGetAttribute(vi, VI_ATTR_USER_DATA, (ViAddr) &thisPtr);
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, 0, errStatus );

    ri4152a_DEBUG_CHK_THIS( vi, thisPtr );
    ri4152a_CDE_INIT( "ri4152a_get_cal_string");

    errStatus = viPrintf(vi,"CAL:STR?\n");
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
    
    errStatus = viRead (vi, (ViPBuf) readbuf, (ViUInt32) sizeof (readbuf), &cnt);
    readbuf[cnt-1] = '\0';
    if (errStatus < VI_SUCCESS) ri4152a_LOG_STATUS( vi, thisPtr, errStatus );
 
 	strcpy (result, readbuf);
 	
 	return 0;
}
