#ifndef DIAGNOSTICS_INCLUDED
#define DIAGNOSTICS_INCLUDED

/* 
 * Copyright 1997 by Teradyne, Inc., Boston, MA
 *
 * Diagnostics.h
 * 
 * This file contains the various datatypes used in diagnostics.
 *
 * Revision History:
 *
 * 00014 05/30/01 jxy added DiagTestResult
 * 00013 06/14/99 eap added DiagProbetagHandle
 * 00012 03/26/99 eap added DIAG_ERROR_NO_NETLIST_DB, DIAG_ERROR_NO_GPD and DIAG_ERROR_SYSTEM
 * 00011 11/10/98 dmd defined DIAG_MAX_NAMELEN.
 * 00010 04/13/98 eap added DIAG_ERROR_FAULT_MISMATCH_EXISTS and
 *						DIAG_ERROR_NO_FAULT_MISMATCH
 * 00009 04/03/98 hmb added DiagProbedPointHandle
 * 00008 03/31/98 eap added DIAG_DIAG_UNIT_ID_EXISTS, DIAG_NO_DIAG_UNIT_ID
 * 00007 03/19/98 eap added DIAG_ERROR_INDEX_NOT_FOUND
 * 00006 03/04/98 hmb added DIAG_ERROR_NO_RAW_DATA
 * 00005 01/26/98 awm moved DiagFaultType, DiagPointType,
 *                    and DiagBSIDStatus from DiagnosticTypes.h
 * 00004 01/22/98 eap added DIAG_ERROR_START_TRACE and DIAG_ERROR_NO_TRACE_DIAGNOSIS
 * 00003 01/20/98 awm added another new DiagStatus
 * 00002 01/16/98 awm add new DiagStatus
 * 00001 xx/xx/xx xxx Creation
 */

#ifndef DLL_EXPORT
#ifdef BUILDING_DLL
#define DLL_EXPORT __declspec (dllexport)
#else
#define DLL_EXPORT
#endif
#endif

/* Maximum name length in diagnostics. */

#define DIAG_MAX_NAMELEN 255

/* do not change any numbers! */

typedef enum {
  DIAG_SUCCESS = 0,
  DIAG_ERROR_FAILS_NOT_FOUND = 1,
  DIAG_ERROR_FILE_IO  = 2,
  DIAG_ERROR_IN_USE = 3,
  DIAG_ERROR_INTERRUPTED = 4,
  DIAG_ERROR_NOT_AVAILABLE = 5,
  DIAG_ERROR_NOT_EXECUTED = 6,
  DIAG_ERROR_NOT_LICENSED = 7,
  DIAG_ERROR_NO_STARTING_INFO = 8,
  DIAG_ERROR_TEST_NOT_FOUND = 9,
  DIAG_WARN_BUFFER_TOO_SMALL = 10,
  DIAG_WARN_ALREADY_INITIALIZED = 11,
  DIAG_WARN_FILE_IO = 12,
  DIAG_WARN_NAME_NOT_FOUND = 13,
  DIAG_WARN_NONE_EXECUTED = 14,
  DIAG_WARN_NOT_AVAILABLE = 15,
  DIAG_WARN_NOT_LICENSED = 16,
  DIAG_WARN_TEST_PASSED = 17,
  DIAG_ERROR_NO_TEST_SELECTED = 18,
  DIAG_ERROR_NAME_NOT_FOUND = 19,
  DIAG_ERROR_SEQUENCE_NOT_DEFINED = 20,
  DIAG_ERROR_NO_PROBETAG_ASSIGNED = 21,
  DIAG_ERROR_UNKNOWN_DIAGNOSTIC = 101,
  DIAG_ERROR_OUT_OF_MEMORY = 102,
  DIAG_ERROR_NO_ASSEMBLY = 103,
  DIAG_ERROR_PRBL_MODE = 104,
  DIAG_ERROR_EXECUTE_DIGITAL_TEST = 105,
  DIAG_ERROR_DETECT_DATA = 106,
  DIAG_ERROR_PROBING_POINT = 107,
  DIAG_ERROR_PROBE_LISTENER = 108,
  DIAG_ERROR_PROBE_DATA = 109,
  DIAG_ERROR_PROBE_RESULT = 110,
  DIAG_ERROR_NULL_POINTER = 111,
  DIAG_ERROR_INVALID_HANDLE = 112, /*djl */
  DIAG_ERROR_NO_FAILURE_DATA = 113,
  DIAG_ERROR_NO_DETECT_DATA = 114,
  DIAG_ERROR_NO_EXPECT_DATA = 115,
  DIAG_ERROR_NO_COMPARE_DATA = 116,
  DIAG_ERROR_PIN_LISTENER = 117,
  DIAG_ERROR_UNSTABLE = 118,
  DIAG_ERROR_FILE_CONTAINS_ERRORS = 119,
  DIAG_ERROR_FILE_CANNOT_BE_OPENED = 120,
  DIAG_ERROR_START_TRACE = 121,
  DIAG_ERROR_NO_TRACE_DIAGNOSIS = 122,
  DIAG_ERROR_NO_RAW_DATA = 123,
  DIAG_ERROR_INDEX_NOT_FOUND = 124,
  DIAG_ERROR_DIAG_UNIT_ID_EXISTS = 125,
  DIAG_ERROR_NO_DIAG_UNIT_ID = 126,
  DIAG_ERROR_FAULT_MISMATCH_EXISTS = 127,
  DIAG_ERROR_NO_FAULT_MISMATCH = 128,
  DIAG_ERROR_NO_NETLIST_DB = 129,
  DIAG_ERROR_NO_GPD = 130,
  DIAG_ERROR_SYSTEM = 131,
  DIAG_ERROR_THREAD_ERROR = 132,
  DIAG_ERROR_PIPE_TIMEOUT = 133
} DiagStatus;


typedef enum {
  DIAG_TYPE_FAULT_DICTIONARY = 0,
  DIAG_TYPE_GUIDED_PROBE = 1,
  DIAG_TYPE_BOUNDARY_SCAN = 2,
  DIAG_TYPE_NONE = 3
} DiagType;

typedef enum {
  DIAG_RESULT_PASS = 0,
  DIAG_RESULT_FAIL = 1,
  DIAG_RESULT_NOT_RUN = 2
} DiagTestResult;

typedef void * DiagDiagnosisHandle;
typedef void * DiagFaultHandle;
typedef void * DiagNetHandle;
typedef void * DiagPointHandle;
typedef void * DiagProbeDataHandle;
typedef void * DiagProbedPointHandle;
typedef void * DiagProbetagHandle;

typedef enum {
  DIAG_ENABLE = 0,
  DIAG_DISABLE = 1
} DiagEnable;

typedef enum {
  DIAG_AVAILABLE = 0,
  DIAG_NOT_AVAILABLE = 1
} DiagAvailable;

/* 00005 */
typedef enum {
  DIAG_BSID_STATUS_FAILED = 0,
  DIAG_BSID_STATUS_PASSED = 1
} DiagBSIDStatus;

typedef enum {
  DIAG_FAULT_STUCK_AT_0,
  DIAG_FAULT_STUCK_AT_1,
  DIAG_FAULT_OPEN,
  DIAG_FAULT_SHORT,
  DIAG_FAULT_BAD_BIDIRECTIONAL_CELL,
  DIAG_FAULT_FAILING_NET
} DiagFaultType;

typedef enum {
  DIAG_POINT_DEVICE_LEAD = 0,
  DIAG_POINT_TESTER_PIN = 1,
  DIAG_POINT_VIA = 2
} DiagPointType;

#define DM_API_MEMORY_PIPE  "\\\\.\\pipe\\DM_InterprocessPipe"  // interprocess pipe name

// if you add any new callbacks to the following list,
// make sure that the corresponding element in the callback
// array is initialized to NULL.

typedef enum DM_CALLBACK_LIST {

	SET_PROBE_POINT_READY,				   	    
	SET_PROBE_SEQUENCE_STARTED,					
	SET_PROBE_BUTTON_PRESSED,					
	SET_PROBE_SEQUENCE_ENDED,					
	SET_START_OF_TEST_CALLBACK,
	SET_END_OF_TEST_CALLBACK,
	SET_START_OF_PATTERN_CALLBACK,
	SET_END_OF_PATTERN_CALLBACK,
	CHECK_PIPE_CONNECTION,
	KILL_DM_CALLBACK_THREAD           					 

} DM_CallbackType;

// make sure the next two defs match the list above...

#define FIRST_DM_CALLBACK  SET_PROBE_POINT_READY
#define LAST_DM_CALLBACK   SET_END_OF_PATTERN_CALLBACK

// this is the string message that the server pipe in
// dm sends to any client that connects

#define PIPE_SERVER_ACKNOWLEDGE  "Received OK"


#endif

