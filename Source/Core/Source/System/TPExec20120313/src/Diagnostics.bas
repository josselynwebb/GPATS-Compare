Attribute VB_Name = "Diagnostics"
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
DefInt A-Z
Option Explicit

' Maximum name length in diagnostics
  Global Const DIAG_MAX_NAMELEN = 255

'Enumerations for DiagStatus
  Global Const DIAG_SUCCESS = 0
  Global Const DIAG_ERROR_FAILS_NOT_FOUND = 1
  Global Const DIAG_ERROR_FILE_IO = 2
  Global Const DIAG_ERROR_IN_USE = 3
  Global Const DIAG_ERROR_INTERRUPTED = 4
  Global Const DIAG_ERROR_NOT_AVAILABLE = 5
  Global Const DIAG_ERROR_NOT_EXECUTED = 6
  Global Const DIAG_ERROR_NOT_LICENSED = 7
  Global Const DIAG_ERROR_NO_STARTING_INFO = 8
  Global Const DIAG_ERROR_TEST_NOT_FOUND = 9
  Global Const DIAG_WARN_BUFFER_TOO_SMALL = 10
  Global Const DIAG_WARN_ALREADY_INITIALIZED = 11
  Global Const DIAG_WARN_FILE_IO = 12
  Global Const DIAG_WARN_NAME_NOT_FOUND = 13
  Global Const DIAG_WARN_NONE_EXECUTED = 14
  Global Const DIAG_WARN_NOT_AVAILABLE = 15
  Global Const DIAG_WARN_NOT_LICENSED = 16
  Global Const DIAG_WARN_TEST_PASSED = 17
  Global Const DIAG_ERROR_NO_TEST_SELECTED = 18
  Global Const DIAG_ERROR_NAME_NOT_FOUND = 19
  Global Const DIAG_ERROR_SEQUENCE_NOT_DEFINED = 20
  Global Const DIAG_ERROR_NO_PROBETAG_ASSIGNED = 21
  Global Const DIAG_ERROR_UNKNOWN_DIAGNOSTIC = 101
  Global Const DIAG_ERROR_OUT_OF_MEMORY = 102
  Global Const DIAG_ERROR_NO_ASSEMBLY = 103
  Global Const DIAG_ERROR_PRBL_MODE = 104
  Global Const DIAG_ERROR_EXECUTE_DIGITAL_TEST = 105
  Global Const DIAG_ERROR_DETECT_DATA = 106
  Global Const DIAG_ERROR_PROBING_POINT = 107
  Global Const DIAG_ERROR_PROBE_LISTENER = 108
  Global Const DIAG_ERROR_PROBE_DATA = 109
  Global Const DIAG_ERROR_PROBE_RESULT = 110
  Global Const DIAG_ERROR_NULL_POINTER = 111
  Global Const DIAG_ERROR_INVALID_HANDLE = 112 ' /*djl */
  Global Const DIAG_ERROR_NO_FAILURE_DATA = 113
  Global Const DIAG_ERROR_NO_DETECT_DATA = 114
  Global Const DIAG_ERROR_NO_EXPECT_DATA = 115
  Global Const DIAG_ERROR_NO_COMPARE_DATA = 116
  Global Const DIAG_ERROR_PIN_LISTENER = 117
  Global Const DIAG_ERROR_UNSTABLE = 118
  Global Const DIAG_ERROR_FILE_CONTAINS_ERRORS = 119
  Global Const DIAG_ERROR_FILE_CANNOT_BE_OPENED = 120
  Global Const DIAG_ERROR_START_TRACE = 121
  Global Const DIAG_ERROR_NO_TRACE_DIAGNOSIS = 122
  Global Const DIAG_ERROR_NO_RAW_DATA = 123
  Global Const DIAG_ERROR_INDEX_NOT_FOUND = 124
  Global Const DIAG_ERROR_DIAG_UNIT_ID_EXISTS = 125
  Global Const DIAG_ERROR_NO_DIAG_UNIT_ID = 126
  Global Const DIAG_ERROR_FAULT_MISMATCH_EXISTS = 127
  Global Const DIAG_ERROR_NO_FAULT_MISMATCH = 128

'Enumarations for DiagType
  Global Const DIAG_TYPE_FAULT_DICTIONARY = 0
  Global Const DIAG_TYPE_GUIDED_PROBE = 1
  Global Const DIAG_TYPE_BOUNDARY_SCAN = 2
  Global Const DIAG_TYPE_NONE = 3

'Enumarations for DiagEnable
  Global Const DIAG_ENABLE = 0
  Global Const DIAG_DISABLE = 1

'Enumarations for DiagAvailable
  Global Const DIAG_AVAILABLE = 0
  Global Const DIAG_NOT_AVAILABLE = 1

'Enumarations for DiagBSIDStatus
  Global Const DIAG_BSID_STATUS_FAILED = 0
  Global Const DIAG_BSID_STATUS_PASSED = 1

'Enumarations for DiagFaultType
  Global Const DIAG_FAULT_STUCK_AT_0 = 0
  Global Const DIAG_FAULT_STUCK_AT_1 = 1
  Global Const DIAG_FAULT_OPEN = 2
  Global Const DIAG_FAULT_SHORT = 3
  Global Const DIAG_FAULT_BAD_BIDIRECTIONAL_CELL = 4
  Global Const DIAG_FAULT_FAILING_NET = 5

'Enumarations for DiagPointType
  Global Const DIAG_POINT_DEVICE_LEAD = 0
  Global Const DIAG_POINT_TESTER_PIN = 1
  Global Const DIAG_POINT_VIA = 2
