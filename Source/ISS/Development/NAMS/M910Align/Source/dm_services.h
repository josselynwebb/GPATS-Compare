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
// DM01.004	4/17/98		eap	changed order of parameters in DM_errorMessage
 * DM00.011	19-Feb-98	 eap		   added versioning in DM_Version.h, plus
 *                                     the DM_getVersion function.
 * DM003    04-Feb-98    eap           add DM_errorMessage
 * DM002    29-Jan-98    hmb           Add __stdcall
 * DM001	20-Nov-97    hmb           Creation
 */

#include "Diagnostics.h"

#if defined (__cplusplus) || defined (__cplusplus__)
extern "C" {
#endif

// DM_getVersion
//
// This function is used to obtain the version of the diagnostic manager.
//
// Paremeters:
//   version - should point to a buffer of at least 32 characters. gets 
//   filled in with the null terminated version string
//
DLL_EXPORT DiagStatus __stdcall DM_getVersion (char * version);


DLL_EXPORT DiagStatus __stdcall DM_diagnoseTest(char * testName, 
												char * assemblyName,
    		                                    char * unitID, ViSession vi);


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


#if defined (__cplusplus) || defined (__cplusplus__)
}
#endif

#endif  //ifndef _DM_SERVICES_H