#ifndef _DM_SERVICES_H
#define _DM_SERVICES_H
/*
 * Copyright 1997 by Teradyne, Inc., Boston, MA
 *
 * Module:   DM_Services.h
 * Creator:  Henry M. Benson
 *
 * Abstract: This file contains the function prototypes for the
 *			 services of the Diagnostic Manager domain
 *
 * Revision History:
 *
 // DM5.0.3.0	30/06/03	JXY		- Added DM_setDiagnosis/ErrorOutputCallback()
 // DM5.0.000	11/02/01	JXY		- Added DM_verifyTest
 // DM2.1.006	07/19/01	JXY		- Added failure collection limit
 // DM2.1.003	05/30/01	JXY		- Added DM_getTestResult
 // DM2.0.000   11/16/99     JWC     Starting work for 2.0
 // DM1.2.006    8/20/99     JWC     - Adding reset enable/disable functions to hand off to probe
 // DM1.2.000    2/22/99     DJL     Change DM_g/s/etDiagOutputDir to DM_g/s/etDiagnosticOutputDir
 // DM1.1.011	1/29/99		eap		Added functions DM_startDiagnoseTest and DM_isDiagnosisReady
 //									to support asynchronous diagnostics
 // DM1.1.008   1/19/99      DMD     Fixed bug where NULL outputDirectory
 //                                  caused DM_setDiagnosticOutputFile to fail.
 // DM1.1.007   1/18/99      DMD     Updated comments for consistency with
 //                                  reality and diagnostics.fp.
 // DM1.1.006   1/12/99      DMD     Added functions DM_setDiagOutputDir, 
 //                                  DM_getDiagOutputDir.
 // DM1.1.005   1/11/99      DMD     Added functions DM_setScreenDiagEnable,
 //                                  DM_getScreenDiagEnable.
 // DM1.1.003   12/1/98      DMD     Added new function DM_getFaultInfo. 
 // DM1.1.002   11/10/98     DMD     Added new functions:
 //                                  DM_setDiagnosticOutputFile,
 //                                  DM_getDiagnosticOutputFile, 
 //                                  overloaded function DM_getDiagnosticText
 // DM01.005    05/12/98     eap     added get/setProbeThroughTesterPin
 // DM01.004	4/17/98	     eap     changed order of parameters in DM_errorMessage
 * DM00.011	19-Feb-98    eap     added versioning in DM_Version.h, plus
 *                                     the DM_getVersion function.
 * DM003        04-Feb-98    eap     add DM_errorMessage
 * DM002        29-Jan-98    hmb     Add __stdcall
 * DM001	20-Nov-97    hmb     Creation
 */

#include "diagnostics.h"
#include "terM9.h"

#if defined (__cplusplus) || defined (__cplusplus__)
extern "C" {
#endif

typedef DiagFaultHandle DM_DiagFaultHandle;

#ifdef BOOLEAN 
#undef BOOLEAN
#endif

typedef unsigned char BOOLEAN;


typedef void (__stdcall * DiagnosisDoneCallback) (DiagStatus);

typedef void (__stdcall * DM_GenericCallback) (char *);
//typedef unsigned short (DTX_TestNotificationCallback) (const unsigned long,const char *);
//typedef unsigned short (DTX_DynamicPatternNotificationCallback)(const unsigned long, const char *, const signed long);

typedef int (DTXTestMainType) (int argc, int * argv);

typedef enum {

	 GSV_SST_BON=0,
     GSV_FAULT_SET_LIMIT,	 
     GSV_SST_MAX_MISMATCH,	 
     GSV_DICTIONARY,			 
     GSV_SST_STABILITY_COUNT,
	 TOTAL_GSV_TYPES

} LtoM_GSV_Types;


// DM_getVersion
//
// This function is used to obtain the version of the diagnostic manager.
//
// Paremeters:
//   version - should point to a buffer of at least 32 characters. gets 
//   filled in with the null terminated version string
//
DLL_EXPORT DiagStatus __stdcall DM_getVersion (char * version);

DLL_EXPORT DiagStatus __stdcall DM_isDiagnosisReady (BOOLEAN * ready);

DLL_EXPORT DiagStatus __stdcall DM_setDiagnoseTestCallback(DiagnosisDoneCallback callback);

//-------------- probe related callbacks ----------------------------------------------------------------- 
DLL_EXPORT DiagStatus __stdcall DM_setProbePointReadyCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setProbeSequenceStartedCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setProbeButtonPressedCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setProbeSequenceEndedCallback (DM_GenericCallback callback);
//------------- dtx callbacks --------------------------------------------------------------------------
DLL_EXPORT DiagStatus __stdcall DM_setStartOfTestCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setEndOfTestCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setStartOfPatternSetCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setEndOfPatternSetCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_disableGenericCallbacks (void);
//-------------- Output callbacks----------------------------------------------------------------------------------------
DLL_EXPORT DiagStatus __stdcall DM_setDiagnosisOutputCallback (DM_GenericCallback callback);
DLL_EXPORT DiagStatus __stdcall DM_setErrorOutputCallback (DM_GenericCallback callback);
													  

DLL_EXPORT DiagStatus __stdcall DM_startDiagnoseTest(char * testName, 
													char * assemblyName,
    												char * unitID, 
  													ViSession vi);


DLL_EXPORT DiagStatus __stdcall DM_diagnoseTest(char * testName, 
												char * assemblyName,
    											char * unitID, 
												ViSession vi);

DLL_EXPORT DiagStatus __stdcall DM_diagnosticCompleted(DiagType diagnostic, 
						       DiagStatus status,
                                                       DiagDiagnosisHandle diagnosisHandle);

DLL_EXPORT DiagStatus __stdcall DM_setDiagnosticEnable (DiagType diagnostic, 
							DiagEnable enable);

DLL_EXPORT DiagStatus __stdcall DM_getDiagnosticEnable (DiagType diagnostic, 
							DiagEnable *enable);

DLL_EXPORT DiagStatus __stdcall DM_getDiagnosticAvailable (char * testName,
							   DiagType diagnostic,
							   DiagAvailable *available);

DLL_EXPORT DiagStatus __stdcall DM_getProbeThroughTesterPin (DiagEnable * enable);

DLL_EXPORT DiagStatus __stdcall DM_setProbeThroughTesterPin (DiagEnable enable);
 
DLL_EXPORT DiagStatus __stdcall DM_getProbeResetEnable (DiagEnable * enable); // jwc

DLL_EXPORT DiagStatus __stdcall DM_setProbeResetEnable (DiagEnable enable);

DLL_EXPORT DiagStatus __stdcall DM_setProbeNoWaitEnable (DiagEnable enable);
DLL_EXPORT DiagStatus __stdcall DM_getProbeNoWaitEnable (DiagEnable *enable);

DLL_EXPORT DiagStatus __stdcall DM_getTestResult (DiagTestResult *result);

DLL_EXPORT DiagStatus __stdcall DM_setDTFCollectionLimitEnable (DiagType diagnostic, 
							DiagEnable enable);

DLL_EXPORT DiagStatus __stdcall DM_getDTFCollectionLimitEnable (DiagType diagnostic, 
							DiagEnable *enable);

// the following four functions were added for diag 2.0 to support GSV from the translator
// the set functions take a single parameter which is used to load the probe command line
// the get functions simply return that value in the input parameter

DLL_EXPORT DiagStatus __stdcall DM_setProbeStabilityCount(long ProbeStabilityCount);
DLL_EXPORT DiagStatus __stdcall DM_getProbeStabilityCount(long * ProbeStabilityCount);
DLL_EXPORT DiagStatus __stdcall DM_setProbeMismatchValue(int ProbeMismatchValue);
DLL_EXPORT DiagStatus __stdcall DM_getProbeMismatchValue(int * ProbeMismatchValue);
DLL_EXPORT DiagStatus __stdcall DM_setMaxSeedingFaultSetsValue(int ProbeFaultSetValue);
DLL_EXPORT DiagStatus __stdcall DM_getMaxSeedingFaultSetsValue(int * ProbeFaultSetValue);
// calls other DM functions to set up GSVs for L Series test
DLL_EXPORT DiagStatus __stdcall DM_setGlobalSystemVariables(int argc, int *argv);

DLL_EXPORT DiagStatus __stdcall DM_verifyTest(char * testName, char * scenarioName, ViSession vi);

/* DM_errorMessage
 *
 * This function takes a DIAG status and returns the severity of the error, 
 * and the string describing the error.
 *
 * INPUT PARAMETERS:
 *   status - DiagStatus value which is to be described
 *   masSize - the size of the buffer
 *
 * OUTPUT PARAMETERS:
 *   severity - a pointer to one character that describes the severity oaf the error:
 *		'W' - warning
 *		'E' - error
 *		'I' - informational
 *		'F' - fatal
 *   buffer - pointer to memoty of size maxSize, o be filled in with the description 
 *            of the DIAG status
 *
 * RETURN VALUES:
 *   DIAG_SUCCESS - error interprited successfully
 *   DIAG_ERROR_NAME_NOT_FOUND - status was not recognized
 *   DIAG_WARN_BUFFER_TOO_SMALL - buffer was too small to fit all of the description.
 *
 */
DLL_EXPORT DiagStatus __stdcall DM_errorMessage(DiagStatus status, 
						int maxSize, 
						char * severity, 
					        char * buffer);


                    /* TOP DM_setDiagnosticOutputFile */
DLL_EXPORT DiagStatus __stdcall
DM_setDiagnosticOutputFile(
			   char * fileName     /* Name of the file to 
						* put the text form of
						* diagnosis. 
						* Invariant:
						*   fileName != NULL.
						*   strlen(fileName) <= 
						*      DIAG_MAX_NAMELEN */
			   );
/*
 *	PURPOSE:	To specify the name of the file in which to put
 *			the ASCII diagnosis.
 *
 *	RETURN VALUE:	DIAG_SUCCESS, if service completed successfully.
 *                      DIAG_ERROR_FILE_CANNOT_BE_OPENED, if the file
 *                          was NULL or couldn't be opened. NULL will be 
 *                          assigned to outfile, and default file name in 
 *                          current working directory will be used instead 
 *                          when diagnosis is performed. The default file 
 *                          name is <test name>.dia.
 *                      DIAG_WARN_BUFFER_TOO_SMALL if the filename was longer
 *                          than DIAG_MAX_NAMELEN characters. In that case, 
 *                          the filename is truncated.
 *                      DIAG_ERROR_OUT_OF_MEMORY if there wasn't enough
 *                          free memory to allocate.
 *			
 *	REQUIREMENTS:	See invariants.
 *
 *	SIDE EFFECTS:	Allocates memory and sets global variable "outfile".
 *                      May set outputDirectory to DEFAULT_OUTDIR. 
 *			Frees existing filename. If a file of the given
 *                      name exists, will overwrite it with 0 bytes.
 *                      Will overwrite outfile with NULL if file was NULL or
 *                      couldn't be opened.
 *
 *	Description     The name can be any of the following:
 *			
 *			File name using default path ("test.dia").
 *			File name using relative path ("mytest\\test.dia").
 *			File name using absolute path ("C:\\tests\\mytest\test.dia").
 *
 *			The file extension is NOT assumed. If you
 *			specify a filename without an extension, a 
 *			file with no extension will be created. The
 *                      final file name must not exceed DIAG_MAX_NAMELEN 
 *                      characters in length.
 *
 *	                This routine interacts with the routine
 *                      DM_setDiagnosticOutputDir as follows:
 *
 *                      If this routine is called with a bare filename, 
 *                      the filename is concatenated with the 
 *                      directory specified in DM_setDiagnosticOutputDir,
 *                      or with the default directory (if outputDirectory
 *                      hasn't been set yet).
 *                      
 *                      If this routine is called with a pathname
 *                      (relative or full), the diagnostic log file is 
 *                      placed in the location given by the pathname, 
 *                      regardless of the directory specified in 
 *                      DM_setDiagnosticOutputDir.
 */
                    /* BOTTOM DM_setDiagnosticOutputFile */
                    /* TOP DM_getDiagnosticOutputFile */
DLL_EXPORT DiagStatus __stdcall
DM_getDiagnosticOutputFile(
			   char * fileName     /* Pointer to a string
						* buffer to place the 
						* name of the diagnosis
						* file in.
						* Invariant:
						*   fileName != NULL. */
			   );
/*
 *	PURPOSE:	To copy into the passed parameter the name of the file 
 *			into which the ASCII diagnosis is written.
 *
 *	RETURN VALUE:	DIAG_SUCCESS, if service completed successfully.
 *                      DIAG_ERROR_NULL_POINTER if fileName is NULL.
 *
 *	REQUIREMENTS:	If outfile is NULL, returns a null string.
 *                      Assumes the buffer is at least DIAG_MAX_NAMELEN+1 
 *                      characters in length.
 *
 *	SIDE EFFECTS:	Reads global variable "output" and copies into string
 *                      buffer.
 *			
 *	Description     The name can be any of the following:
 *			
 *			File name using default path ("test.dia").
 *			File name using relative path ("mytest\\test.dia").
 *			File name using absolute path ("C:\\tests\\mytest\test.dia").
 */
                    /* BOTTOM DM_getDiagnosticOutputFile */ 
                    /* TOP DM_getDiagnosisText */
DLL_EXPORT DiagStatus __stdcall
DM_getDiagnosisText(
		    DiagType diagnostic,    /* Type of diagnosis.
					     * Invariant: One of:
					     *     DIAG_TYPE_FAULT_DICTIONARY,
					     *     DIAG_TYPE_GUIDED_PROBE,
					     *     DIAG_TYPE_BOUNDARY_SCAN */
		    int diagnosisMaxSize,   /* Size in characters of the
					     * diagnosis space to be filled
					     * in. If <= 1, nothing will be
					     * filled in. If this is too
					     * small, the string will
					     * be truncated, with a warning. */
		    char * diagnosis        /* A string to receive the 
					     * diagnosis. Must be allocated 
					     * by the caller.
					     * Invariant:
					     *     diagnosis != NULL */
		    );
/*
 *	PURPOSE:	To put into a user-provided buffer the diagnosis string
 *                      of the most recently performed diagnostic of the given
 *                      type, if any.
 *			
 *	RETURN VALUE:	DIAG_SUCCESS, if service completed successfully.
 *			DIAG_WARN_BUFFER_TOO_SMALL if the buffer was too small.
 *                      In that case, the message is truncated.  
 *                      DIAG_ERROR_UNKNOWN_DIAGNOSTIC if a bad diagnostic type
 *                      is passed.
 *                      DIAG_ERROR_NULL_POINTER if diagnosis is NULL.
 *                      DIAG_ERROR_FILE_CANNOT_BE_OPENED if the given diagnostic
 *                      file was not found or could not be opened. In this case,
 *                      a null string is returned.
 *
 *	REQUIREMENTS:	See invariants.
 *			
 *	SIDE EFFECTS:	Writes into passed-in buffer. 
 *			
 *	Description     This is an overload of the existing function
 *			DM_getDiagnosisText, which is specified to take
 *			different parameters.
 */

                    /* BOTTOM DM_getDiagnosisText */

                    /* TOP DM_getFaultInfo */
DLL_EXPORT DiagStatus __stdcall
DM_getFaultInfo(
	    char * testName,        /* In: String containing name of
				     * test. Needed for accessing test
				     * results databases. 
				     * Invariant: 
				     *     testName != NULL. */
	    DiagType diagnostic,    /* In: Type of diagnosis.
				     * Invariant: One of:
				     *     DIAG_TYPE_FAULT_DICTIONARY,
				     *     DIAG_TYPE_GUIDED_PROBE,
				     *     DIAG_TYPE_BOUNDARY_SCAN */
	    int maxMsgSize,         /* In: Maximum size of a NULL terminated
				     * message string. Buffer must be large
				     * enough for a reasonably sized message
				     * string. Longer strings will be
				     * truncated.
				     * Invariant: 
				     *     maxMsgSize > 1. 
				     */
	    char * faultStr,        /* Out: Pointer to buffer into 
				     * which to write the faulty net, 
				     * pin, or component as diagnosed,
				     * or "NONE" if not found. 
				     * Invariant:
				     *     faultStr != NULL. 
				     *     sizeof(*faultStr) >= maxMsgSize + 1
				     */
	    char * msgStr,          /* Out: Pointer to buffer into 
				     * which to write the message 
				     * associated with the faulty net, 
				     * pin, or component. If no fault 
				     * found, set to the NULL string. 
				     * Invariant:
				     *     msgStr != NULL. 
				     *     sizeof(*msgStr) >= maxMsgSize + 1
				     */
	     DM_DiagFaultHandle prevFault,
	                            /* In: Handle of the previous fault,
				     * if any. If NULL, routine will get 
				     * the first fault and return the handle 
				     * of the next in *nextFault. */

	     DM_DiagFaultHandle *nextFault
	                            /* Out: Pointer to a variable for 
				     * receiving the handle of the next 
				     * fault, if any. If there is no next
				     * fault, *nextFault is set to NULL. 
				     * It is the responsibility of the 
				     * caller to check that nextFault is 
				     * NULL in order to break from a loop. 
				     * Invariant:
				     *     nextFault != NULL. */
	   );
/*
 *	PURPOSE:	To get the faulty object and associated fault
 *			message.
 *
 *	RETURN VALUE:	DIAG_SUCCESS if successful, otherwise: 
 *			DIAG_WARN_BUFFER_TOO_SMALL if a buffer was too 
 *                      small. In that case, the relevant string is 
 *                      truncated. 
 *                      DIAG_ERROR_UNKNOWN_DIAGNOSTIC if a bad diagnostic 
 *                      type was passed.
 *                      DIAG_ERROR_NULL_POINTER if faultStr, msgStr, or
 *                      nextFault is NULL.
 *                      DIAG_ERROR_FILE_CANNOT_BE_OPENED if the relevant 
 *                      diagnostic database was not found or could not be 
 *                      opened. 
 *                      DIAG_ERROR_FAILS_NOT_FOUND if failure information
 *                      could not be found in the database.
 *                      DIAG_ERROR_FILE_IO if there was an error reading
 *                      the file.
 *
 *                      For all errors but NULL_POINTER, "NONE" is returned 
 *                      in faultStr and a null string is returned in msgStr.
 *                      The LAST call returns a nextFault of NULL along with
 *                      the last actual fault data.
 *			
 *	REQUIREMENTS:	See invariants. 
 *			
 *	SIDE EFFECTS:	Writes into string buffers and into variable passed
 *			in as "nextFault". Opens files and databases, 
 *                      allocates memory, closes files and databases when
 *                      the last item is returned and *nextFault is set to 
 *                      NULL.
 *
 *	Description     Caller calls routine with the given diagnostic type,
 *			with the string buffers set to allocated memory,
 *			and with nextFault set to the address of a variable
 *			of type DM_DiagFaultHandle.
 */
                    /* BOTTOM DM_getFaultInfo */

                    /* TOP DM_setScreenDiagEnable */
DLL_EXPORT DiagStatus __stdcall
DM_setScreenDiagEnable(
		       DiagEnable value       /* New value of ScreenDiagEnable.
					       * Invariant:
					       *    One of DIAG_ENABLE,
					       *    DIAG_DISABLE. */
		       );

/*
 *	PURPOSE:	To set a flag that allows the diagnosis to be printed 
 *			to the screen.
 *
 *	RETURN VALUE:	DIAG_ERROR_NAME_NOT_FOUND if value is out of range,
 *                      otherwise DIAG_SUCCESS.
 *			
 *	REQUIREMENTS:	None.
 *			
 *	SIDE EFFECTS:	Writes into static global ScreenDiagEnable.
 *			
 *	Description     First checks the input value is in range, then sets it.
 */

                    /* BOTTOM DM_setScreenDiagEnable */

                    /* TOP DM_getScreenDiagEnable */
DLL_EXPORT DiagStatus __stdcall
DM_getScreenDiagEnable(
		       DiagEnable *value_ptr   /* Pointer to variable for
						* receiving enable value.
						* Invariant:
						*    value_ptr != NULL */
		       );

/*
 *	PURPOSE:	To get the current value of a flag that allows the
 *			diagnosis to be printed to the screen.
 *
 *	RETURN VALUE:	DIAG_ERROR_NULL_POINTER if value_ptr is NULL,
 *                      DIAG_ERROR_NAME_NOT_FOUND if ScreenDiagEnable is out 
 *			of range, otherwise DIAG_SUCCESS.
 *
 *	REQUIREMENTS:	See invariants. ScreenDiagEnable must be in range. 
 *			
 *	SIDE EFFECTS:	Sets the variable pointed to by value_ptr to
 *                      DIAG_ENABLE or DIAG_DISABLE.
 *			
 *	Description     Checks for null pointer and out-of-range value,
 *			then sets output parameter.
 */

                    /* BOTTOM DM_getScreenDiagEnable */

                    /* TOP DM_setDiagnosticOutputDir */
DLL_EXPORT DiagStatus __stdcall
DM_setDiagnosticOutputDir(
		    char * dirName /* Name of the directory
				    * in which to put the
				    * diagnosis files.
				    * Invariant:
				    *   dirName != NULL.
				    *   strlen(dirName) <= 
				    *      DIAG_MAX_NAMELEN */
		    );
/*
 *	PURPOSE:	To specify the directory in which to put the
 *			diagnosis files.
 *
 *	RETURN VALUE:	DIAG_SUCCESS, if service completed successfully.
 *                      DIAG_ERROR_FILE_CANNOT_BE_OPENED, if the directory
 *                          was NULL or the null string. NULL will be assigned
 *                          to outputDirectory, and the default directory 
 *                          (usually .\) will be used instead.
 *                      DIAG_WARN_BUFFER_TOO_SMALL if the dirName was longer
 *                          than DIAG_MAX_NAMELEN characters. In that case, 
 *                          dirName is truncated.
 *                      DIAG_ERROR_OUT_OF_MEMORY if there wasn't enough
 *                          free memory to allocate buffer.
 *			
 *	REQUIREMENTS:	See invariants. The file name must not exceed 
 *                      DIAG_MAX_NAMELEN characters in length.
 *
 *	SIDE EFFECTS:	Allocates memory and sets global variable 
 *			"outputDirectory". Frees existing outputDirectory.
 *                      Will overwrite outputDirectory with NULL if dirName
 *                      was NULL.  If directory spec doesn't end with a \\,
 *                      adds one.
 *
 *	Description     This routine interacts with the routine
 *                      DM_setDiagnosticOutputFile as follows:
 *
 *                      If DM_setDiagnosticOutputFile is called with a bare
 *                      filename, the diagnostic log file is placed in the 
 *                      directory specified here. 
 *                      
 *                      If DM_setDiagnosticOutputFile is called with a
 *                      full pathname, the diagnostic log file is placed
 *                      in the location given by the full pathname, 
 *                      regardless of what directory is specified here.
 */

                    /* BOTTOM DM_setDiagnosticOutputDir */
                    /* TOP DM_getDiagnosticOutputDir */
DLL_EXPORT DiagStatus __stdcall
DM_getDiagnosticOutputDir(
		    char * dirName_ptr /* Pointer to a string
					* buffer to place the 
					* name of the diagnosis
					* output directory in.
					* Invariant:
					*   dirName_ptr != NULL. */
		    );
/*
 *	PURPOSE:	To copy into the passed parameter the name of the 
 *			directory which the diagnosis output is placed.
 *
 *	RETURN VALUE:	DIAG_SUCCESS, if service completed successfully.
 *                      DIAG_ERROR_NULL_POINTER if dirName_ptr is NULL.
 *
 *	REQUIREMENTS:	If outputDirectory is NULL, returns a null string.
 *                      ASSUMES THE BUFFER IS AT LEAST DIAG_MAX_NAMELEN+1 
 *                      CHARACTERS IN LENGTH.
 *
 *	SIDE EFFECTS:	Reads global variable "outputDirectory" and copies 
 *                      string into string buffer.
 *			
 *	Description     
 *			
 */

                    /* BOTTOM DM_getDiagnosticOutputDir */ 



#if defined (__cplusplus) || defined (__cplusplus__)
}
#endif

#endif  //ifndef _DM_SERVICES_H
