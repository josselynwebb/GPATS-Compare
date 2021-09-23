Option Strict Off
Option Explicit On

Module Diagnostics
	' Copyright 1997 by Teradyne, Inc., Boston, MA
	'
	' Module:   Diagnostics.bas
	' Creator:  Michel H. Pradieu
	'
	' Abstract:  This file contains the enumerations for the datatypes used in
	'            diagnostics. This is the VB copy of the C header file
	'            Diagnostics.h
	'
	' Revision History:
	'
	' Version  Date    Who   What
	' 1.1      12/9/98 DMD   Edited to make consistent with diagnostics.h changes.
	' 1.0      980604  mhp   Creation
	'
	
    ' Maximum name length in diagnostics
	Public Const DIAG_MAX_NAMELEN As Short = 255
	
	'Enumerations for DiagStatus
	Public Const DIAG_SUCCESS As Short = 0
	Public Const DIAG_ERROR_FAILS_NOT_FOUND As Short = 1
	Public Const DIAG_ERROR_FILE_IO As Short = 2
	Public Const DIAG_ERROR_IN_USE As Short = 3
	Public Const DIAG_ERROR_INTERRUPTED As Short = 4
	Public Const DIAG_ERROR_NOT_AVAILABLE As Short = 5
	Public Const DIAG_ERROR_NOT_EXECUTED As Short = 6
	Public Const DIAG_ERROR_NOT_LICENSED As Short = 7
	Public Const DIAG_ERROR_NO_STARTING_INFO As Short = 8
	Public Const DIAG_ERROR_TEST_NOT_FOUND As Short = 9
	Public Const DIAG_WARN_BUFFER_TOO_SMALL As Short = 10
	Public Const DIAG_WARN_ALREADY_INITIALIZED As Short = 11
	Public Const DIAG_WARN_FILE_IO As Short = 12
	Public Const DIAG_WARN_NAME_NOT_FOUND As Short = 13
	Public Const DIAG_WARN_NONE_EXECUTED As Short = 14
	Public Const DIAG_WARN_NOT_AVAILABLE As Short = 15
	Public Const DIAG_WARN_NOT_LICENSED As Short = 16
	Public Const DIAG_WARN_TEST_PASSED As Short = 17
	Public Const DIAG_ERROR_NO_TEST_SELECTED As Short = 18
	Public Const DIAG_ERROR_NAME_NOT_FOUND As Short = 19
	Public Const DIAG_ERROR_SEQUENCE_NOT_DEFINED As Short = 20
	Public Const DIAG_ERROR_NO_PROBETAG_ASSIGNED As Short = 21
	Public Const DIAG_ERROR_UNKNOWN_DIAGNOSTIC As Short = 101
	Public Const DIAG_ERROR_OUT_OF_MEMORY As Short = 102
	Public Const DIAG_ERROR_NO_ASSEMBLY As Short = 103
	Public Const DIAG_ERROR_PRBL_MODE As Short = 104
	Public Const DIAG_ERROR_EXECUTE_DIGITAL_TEST As Short = 105
	Public Const DIAG_ERROR_DETECT_DATA As Short = 106
	Public Const DIAG_ERROR_PROBING_POINT As Short = 107
	Public Const DIAG_ERROR_PROBE_LISTENER As Short = 108
	Public Const DIAG_ERROR_PROBE_DATA As Short = 109
	Public Const DIAG_ERROR_PROBE_RESULT As Short = 110
	Public Const DIAG_ERROR_NULL_POINTER As Short = 111
	Public Const DIAG_ERROR_INVALID_HANDLE As Short = 112 ' /*djl */
	Public Const DIAG_ERROR_NO_FAILURE_DATA As Short = 113
	Public Const DIAG_ERROR_NO_DETECT_DATA As Short = 114
	Public Const DIAG_ERROR_NO_EXPECT_DATA As Short = 115
	Public Const DIAG_ERROR_NO_COMPARE_DATA As Short = 116
	Public Const DIAG_ERROR_PIN_LISTENER As Short = 117
	Public Const DIAG_ERROR_UNSTABLE As Short = 118
	Public Const DIAG_ERROR_FILE_CONTAINS_ERRORS As Short = 119
	Public Const DIAG_ERROR_FILE_CANNOT_BE_OPENED As Short = 120
	Public Const DIAG_ERROR_START_TRACE As Short = 121
	Public Const DIAG_ERROR_NO_TRACE_DIAGNOSIS As Short = 122
	Public Const DIAG_ERROR_NO_RAW_DATA As Short = 123
	Public Const DIAG_ERROR_INDEX_NOT_FOUND As Short = 124
	Public Const DIAG_ERROR_DIAG_UNIT_ID_EXISTS As Short = 125
	Public Const DIAG_ERROR_NO_DIAG_UNIT_ID As Short = 126
	Public Const DIAG_ERROR_FAULT_MISMATCH_EXISTS As Short = 127
	Public Const DIAG_ERROR_NO_FAULT_MISMATCH As Short = 128
	
	'Enumarations for DiagType
	Public Const DIAG_TYPE_FAULT_DICTIONARY As Short = 0
	Public Const DIAG_TYPE_GUIDED_PROBE As Short = 1
	Public Const DIAG_TYPE_BOUNDARY_SCAN As Short = 2
	Public Const DIAG_TYPE_NONE As Short = 3
	
	'typedef void * DiagDiagnosisHandle;
	'typedef void * DiagFaultHandle;
	'typedef void * DiagNetHandle;
	'typedef void * DiagPointHandle;
	'typedef void * DiagProbeDataHandle;
	'typedef void * DiagProbedPointHandle;
	
	'Enumarations for DiagEnable
	Public Const DIAG_ENABLE As Short = 0
	Public Const DIAG_DISABLE As Short = 1
	
	'Enumarations for DiagAvailable
	Public Const DIAG_AVAILABLE As Short = 0
	Public Const DIAG_NOT_AVAILABLE As Short = 1
	
	'Enumarations for DiagBSIDStatus
	Public Const DIAG_BSID_STATUS_FAILED As Short = 0
	Public Const DIAG_BSID_STATUS_PASSED As Short = 1
	
	'Enumarations for DiagFaultType
	Public Const DIAG_FAULT_STUCK_AT_0 As Short = 0
	Public Const DIAG_FAULT_STUCK_AT_1 As Short = 1
	Public Const DIAG_FAULT_OPEN As Short = 2
	Public Const DIAG_FAULT_SHORT As Short = 3
	Public Const DIAG_FAULT_BAD_BIDIRECTIONAL_CELL As Short = 4
	Public Const DIAG_FAULT_FAILING_NET As Short = 5
	
	'Enumarations for DiagPointType
	Public Const DIAG_POINT_DEVICE_LEAD As Short = 0
	Public Const DIAG_POINT_TESTER_PIN As Short = 1
	Public Const DIAG_POINT_VIA As Short = 2
End Module