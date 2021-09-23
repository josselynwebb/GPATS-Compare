Option Strict Off
Option Explicit On
Module VppType
	' -------------------------------------------------------------------------
	'  Distributed by VXIplug&play Systems Alliance
	'  Do not modify the contents of this file.
	' -------------------------------------------------------------------------
	'  Title   : VPPTYPE.BAS
	'  Date    : 02-14-95
	'  Purpose : VXIplug&play instrument driver header file
	' -------------------------------------------------------------------------
	
	Public Const VI_NULL As Short = 0
	Public Const VI_TRUE As Short = 1
	Public Const VI_FALSE As Short = 0
	
	' - Completion and Error Codes --------------------------------------------
	
	Public Const VI_WARN_NSUP_ID_QUERY As Integer = &H3FFC0101
	Public Const VI_WARN_NSUP_RESET As Integer = &H3FFC0102
	Public Const VI_WARN_NSUP_SELF_TEST As Integer = &H3FFC0103
	Public Const VI_WARN_NSUP_ERROR_QUERY As Integer = &H3FFC0104
	Public Const VI_WARN_NSUP_REV_QUERY As Integer = &H3FFC0105
	
	Public Const VI_ERROR_PARAMETER1 As Integer = &HBFFC0001
	Public Const VI_ERROR_PARAMETER2 As Integer = &HBFFC0002
	Public Const VI_ERROR_PARAMETER3 As Integer = &HBFFC0003
	Public Const VI_ERROR_PARAMETER4 As Integer = &HBFFC0004
	Public Const VI_ERROR_PARAMETER5 As Integer = &HBFFC0005
	Public Const VI_ERROR_PARAMETER6 As Integer = &HBFFC0006
	Public Const VI_ERROR_PARAMETER7 As Integer = &HBFFC0007
	Public Const VI_ERROR_PARAMETER8 As Integer = &HBFFC0008
	Public Const VI_ERROR_FAIL_ID_QUERY As Integer = &HBFFC0011
	Public Const VI_ERROR_INV_RESPONSE As Integer = &HBFFC0012
	
	' - Additional Definitions ------------------------------------------------
	
	Public Const VI_ON As Short = 1
	Public Const VI_OFF As Short = 0
End Module