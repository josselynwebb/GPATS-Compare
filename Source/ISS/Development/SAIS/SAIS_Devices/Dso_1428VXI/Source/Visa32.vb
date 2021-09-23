Option Strict Off
Option Explicit On
Module Visa32
	' -------------------------------------------------------------------------
	'  Distributed by VXIplug&play Systems Alliance
	'  Do not modify the contents of this file.
	' -------------------------------------------------------------------------
	'  Title   : VISA32.BAS
	'  Date    : 11-27-95
	'  Purpose : Include file for the VISA Library 1.0 spec
	' -------------------------------------------------------------------------
	
	Public Const VI_SPEC_VERSION As Integer = &H100000
	
	' - Resource Template Functions and Operations ----------------------------
    Declare Function atxmlDF_viSetAttribute Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer

	' - Shared Memory Operations ----------------------------------------------	
	Declare Function viMemAlloc Lib "VISA32.DLL"  Alias "#291"(ByVal vi As Integer, ByVal memSize As Integer, ByRef offset As Integer) As Integer
	Declare Function viMemFree Lib "VISA32.DLL"  Alias "#292"(ByVal vi As Integer, ByVal offset As Integer) As Integer
	
	' - Attributes ------------------------------------------------------------
	
	Public Const VI_ATTR_RSRC_NAME As Integer = &HBFFF0002
	Public Const VI_ATTR_RSRC_IMPL_VERSION As Integer = &H3FFF0003
	Public Const VI_ATTR_RSRC_LOCK_STATE As Integer = &H3FFF0004
	Public Const VI_ATTR_MAX_QUEUE_LENGTH As Integer = &H3FFF0005
	Public Const VI_ATTR_USER_DATA As Integer = &H3FFF0007
	Public Const VI_ATTR_FDC_CHNL As Integer = &H3FFF000D
	Public Const VI_ATTR_FDC_MODE As Integer = &H3FFF000F
	Public Const VI_ATTR_FDC_GEN_SIGNAL_EN As Integer = &H3FFF0011
	Public Const VI_ATTR_FDC_USE_PAIR As Integer = &H3FFF0013
	Public Const VI_ATTR_SEND_END_EN As Integer = &H3FFF0016
	Public Const VI_ATTR_TERMCHAR As Integer = &H3FFF0018
	Public Const VI_ATTR_TMO_VALUE As Integer = &H3FFF001A
	Public Const VI_ATTR_IO_PROT As Integer = &H3FFF001C
	Public Const VI_ATTR_ASRL_BAUD As Integer = &H3FFF0021
	Public Const VI_ATTR_ASRL_DATA_BITS As Integer = &H3FFF0022
	Public Const VI_ATTR_ASRL_PARITY As Integer = &H3FFF0023
	Public Const VI_ATTR_ASRL_STOP_BITS As Integer = &H3FFF0024
	Public Const VI_ATTR_ASRL_FLOW_CNTRL As Integer = &H3FFF0025
	Public Const VI_ATTR_RD_BUF_OPER_MODE As Integer = &H3FFF002A
	Public Const VI_ATTR_WR_BUF_OPER_MODE As Integer = &H3FFF002D
	Public Const VI_ATTR_SUPPRESS_END_EN As Integer = &H3FFF0036
	Public Const VI_ATTR_TERMCHAR_EN As Integer = &H3FFF0038
	Public Const VI_ATTR_SRC_INCREMENT As Integer = &H3FFF0040
	Public Const VI_ATTR_DEST_INCREMENT As Integer = &H3FFF0041
	Public Const VI_ATTR_CMDR_LA As Integer = &H3FFF006B
	Public Const VI_ATTR_MAINFRAME_LA As Integer = &H3FFF0070
	Public Const VI_ATTR_WIN_BASE_ADDR As Integer = &H3FFF0098
	Public Const VI_ATTR_WIN_SIZE As Integer = &H3FFF009A
	Public Const VI_ATTR_ASRL_AVAIL_NUM As Integer = &H3FFF00AC
	Public Const VI_ATTR_MEM_BASE As Integer = &H3FFF00AD
	Public Const VI_ATTR_ASRL_END_IN As Integer = &H3FFF00B3
	Public Const VI_ATTR_ASRL_END_OUT As Integer = &H3FFF00B4
	Public Const VI_ATTR_WIN_ACCESS As Integer = &H3FFF00C3
	Public Const VI_ATTR_RM_SESSION As Integer = &H3FFF00C4
	Public Const VI_ATTR_VXI_LA As Integer = &H3FFF00D5
	Public Const VI_ATTR_MANF_ID As Integer = &H3FFF00D9
	Public Const VI_ATTR_MEM_SIZE As Integer = &H3FFF00DD
	Public Const VI_ATTR_MEM_SPACE As Integer = &H3FFF00DE
	Public Const VI_ATTR_MODEL_CODE As Integer = &H3FFF00DF
	Public Const VI_ATTR_SLOT As Integer = &H3FFF00E8
	Public Const VI_ATTR_IMMEDIATE_SERV As Integer = &H3FFF0100
	Public Const VI_ATTR_INTF_PARENT_NUM As Integer = &H3FFF0101
	Public Const VI_ATTR_RSRC_SPEC_VERSION As Integer = &H3FFF0170
	Public Const VI_ATTR_INTF_TYPE As Integer = &H3FFF0171
	Public Const VI_ATTR_GPIB_PRIMARY_ADDR As Integer = &H3FFF0172
	Public Const VI_ATTR_GPIB_SECONDARY_ADDR As Integer = &H3FFF0173
	Public Const VI_ATTR_RSRC_MANF_NAME As Integer = &HBFFF0174
	Public Const VI_ATTR_RSRC_MANF_ID As Integer = &H3FFF0175
	Public Const VI_ATTR_INTF_NUM As Integer = &H3FFF0176
	Public Const VI_ATTR_TRIG_ID As Integer = &H3FFF0177
	Public Const VI_ATTR_JOB_ID As Integer = &H3FFF4006
	Public Const VI_ATTR_EVENT_TYPE As Integer = &H3FFF4010
	Public Const VI_ATTR_SIGP_STATUS_ID As Integer = &H3FFF4011
	Public Const VI_ATTR_RECV_TRID_ID As Integer = &H3FFF4012
	Public Const VI_ATTR_STATUS As Integer = &H3FFF4025
	Public Const VI_ATTR_RET_COUNT As Integer = &H3FFF4026
	Public Const VI_ATTR_BUFFER As Integer = &H3FFF4027
	
	' - Event Types -----------------------------------------------------------
	
	Public Const VI_EVENT_IO_COMPLETION As Integer = &H3FFF2009
	Public Const VI_EVENT_TRIG As Integer = &HBFFF200A
	Public Const VI_EVENT_SERVICE_REQ As Integer = &H3FFF200B
	Public Const VI_EVENT_VXI_SIGP As Integer = &H3FFF2020
	
	Public Const VI_ALL_ENABLED_EVENTS As Integer = &H3FFF7FFF
	
	' - Completion and Error Codes --------------------------------------------
	
	Public Const VI_SUCCESS As Integer = &H0
	Public Const VI_SUCCESS_SHARED_LOCK As Integer = &H3FFF0001
	Public Const VI_SUCCESS_EVENT_EN As Integer = &H3FFF0002
	Public Const VI_SUCCESS_EVENT_DIS As Integer = &H3FFF0003
	Public Const VI_SUCCESS_QUEUE_EMPTY As Integer = &H3FFF0004
	Public Const VI_SUCCESS_TERM_CHAR As Integer = &H3FFF0005
	Public Const VI_SUCCESS_MAX_CNT As Integer = &H3FFF0006
	Public Const VI_SUCCESS_QUEUE_NEMPTY As Integer = &H3FFF0080
	Public Const VI_SUCCESS_NESTED_SHARED As Integer = &H3FFF0099
	Public Const VI_SUCCESS_NESTED_EXCLUSIVE As Integer = &H3FFF009A
	Public Const VI_SUCCESS_SYNC As Integer = &H3FFF009B
	Public Const VI_WARN_NSUP_ATTR_STATE As Integer = &H3FFF0084
	Public Const VI_WARN_UNKNOWN_STATUS As Integer = &H3FFF0085
	Public Const VI_WARN_NSUP_BUF As Integer = &H3FFF0088
	
	Public Const VI_ERROR_SYSTEM_ERROR As Integer = &HBFFF0000
	Public Const VI_ERROR_INV_OBJECT As Integer = &HBFFF000E
	Public Const VI_ERROR_INV_SESSION As Integer = &HBFFF000E
	Public Const VI_ERROR_RSRC_LOCKED As Integer = &HBFFF000F
	Public Const VI_ERROR_INV_EXPR As Integer = &HBFFF0010
	Public Const VI_ERROR_RSRC_NFOUND As Integer = &HBFFF0011
	Public Const VI_ERROR_INV_RSRC_NAME As Integer = &HBFFF0012
	Public Const VI_ERROR_INV_ACC_MODE As Integer = &HBFFF0013
	Public Const VI_ERROR_TMO As Integer = &HBFFF0015
	Public Const VI_ERROR_CLOSING_FAILED As Integer = &HBFFF0016
	Public Const VI_ERROR_INV_DEGREE As Integer = &HBFFF001B
	Public Const VI_ERROR_INV_JOB_ID As Integer = &HBFFF001C
	Public Const VI_ERROR_NSUP_ATTR As Integer = &HBFFF001D
	Public Const VI_ERROR_NSUP_ATTR_STATE As Integer = &HBFFF001E
	Public Const VI_ERROR_ATTR_READONLY As Integer = &HBFFF001F
	Public Const VI_ERROR_INV_LOCK_TYPE As Integer = &HBFFF0020
	Public Const VI_ERROR_INV_ACCESS_KEY As Integer = &HBFFF0021
	Public Const VI_ERROR_INV_EVENT As Integer = &HBFFF0026
	Public Const VI_ERROR_INV_MECH As Integer = &HBFFF0027
	Public Const VI_ERROR_HNDLR_NINSTALLED As Integer = &HBFFF0028
	Public Const VI_ERROR_INV_HNDLR_REF As Integer = &HBFFF0029
	Public Const VI_ERROR_INV_CONTEXT As Integer = &HBFFF002A
	Public Const VI_ERROR_ABORT As Integer = &HBFFF0030
	Public Const VI_ERROR_RAW_WR_PROT_VIOL As Integer = &HBFFF0034
	Public Const VI_ERROR_RAW_RD_PROT_VIOL As Integer = &HBFFF0035
	Public Const VI_ERROR_OUTP_PROT_VIOL As Integer = &HBFFF0036
	Public Const VI_ERROR_INP_PROT_VIOL As Integer = &HBFFF0037
	Public Const VI_ERROR_BERR As Integer = &HBFFF0038
	Public Const VI_ERROR_INV_SETUP As Integer = &HBFFF003A
	Public Const VI_ERROR_QUEUE_ERROR As Integer = &HBFFF003B
	Public Const VI_ERROR_ALLOC As Integer = &HBFFF003C
	Public Const VI_ERROR_INV_MASK As Integer = &HBFFF003D
	Public Const VI_ERROR_IO As Integer = &HBFFF003E
	Public Const VI_ERROR_INV_FMT As Integer = &HBFFF003F
	Public Const VI_ERROR_NSUP_FMT As Integer = &HBFFF0041
	Public Const VI_ERROR_LINE_IN_USE As Integer = &HBFFF0042
	Public Const VI_ERROR_SRQ_NOCCURRED As Integer = &HBFFF004A
	Public Const VI_ERROR_INV_SPACE As Integer = &HBFFF004E
	Public Const VI_ERROR_INV_OFFSET As Integer = &HBFFF0051
	Public Const VI_ERROR_NSUP_OFFSET As Integer = &HBFFF0054
	Public Const VI_ERROR_WINDOW_NMAPPED As Integer = &HBFFF0057
	Public Const VI_ERROR_NLISTENERS As Integer = &HBFFF005F
	Public Const VI_ERROR_NCIC As Integer = &HBFFF0060
	Public Const VI_ERROR_NSUP_OPER As Integer = &HBFFF0067
	Public Const VI_ERROR_ASRL_PARITY As Integer = &HBFFF006A
	Public Const VI_ERROR_ASRL_FRAMING As Integer = &HBFFF006B
	Public Const VI_ERROR_ASRL_OVERRUN As Integer = &HBFFF006C
	Public Const VI_ERROR_NSUP_WIDTH As Integer = &HBFFF0076
	Public Const VI_ERROR_INV_PARAMETER As Integer = &HBFFF0078
	Public Const VI_ERROR_INV_PROT As Integer = &HBFFF0079
	Public Const VI_ERROR_INV_SIZE As Integer = &HBFFF007B
	Public Const VI_ERROR_WINDOW_MAPPED As Integer = &HBFFF0080
	Public Const VI_ERROR_NIMPL_OPER As Integer = &HBFFF0081
	Public Const VI_ERROR_INV_LENGTH As Integer = &HBFFF0083
	Public Const VI_ERROR_SESN_NLOCKED As Integer = &HBFFF009C
	
	' - Other VISA Definitions ------------------------------------------------
	
	Public Const VI_FIND_BUFLEN As Short = 256
	
	Public Const VI_NULL As Short = 0
	Public Const VI_TRUE As Short = 1
	Public Const VI_FALSE As Short = 0
	
	Public Const VI_INTF_GPIB As Short = 1
	Public Const VI_INTF_VXI As Short = 2
	Public Const VI_INTF_GPIB_VXI As Short = 3
	Public Const VI_INTF_ASRL As Short = 4
	
	Public Const VI_NORMAL As Short = 1
	Public Const VI_FDC As Short = 2
	Public Const VI_HS488 As Short = 3
	
	Public Const VI_FDC_NORMAL As Short = 1
	Public Const VI_FDC_STREAM As Short = 2
	
	Public Const VI_A16_SPACE As Short = 1
	Public Const VI_A24_SPACE As Short = 2
	Public Const VI_A32_SPACE As Short = 3
	
	Public Const VI_UNKNOWN_LA As Short = -1
	Public Const VI_UNKNOWN_SLOT As Short = -1
	
	Public Const VI_QUEUE As Short = 1
	Public Const VI_ALL_MECH As Short = &HFFFFs
	
	Public Const VI_TRIG_SW As Short = -1
	Public Const VI_TRIG_TTL0 As Short = 0
	Public Const VI_TRIG_TTL1 As Short = 1
	Public Const VI_TRIG_TTL2 As Short = 2
	Public Const VI_TRIG_TTL3 As Short = 3
	Public Const VI_TRIG_TTL4 As Short = 4
	Public Const VI_TRIG_TTL5 As Short = 5
	Public Const VI_TRIG_TTL6 As Short = 6
	Public Const VI_TRIG_TTL7 As Short = 7
	Public Const VI_TRIG_ECL0 As Short = 8
	Public Const VI_TRIG_ECL1 As Short = 9
	
	Public Const VI_TRIG_PROT_DEFAULT As Short = 0
	Public Const VI_TRIG_PROT_ON As Short = 1
	Public Const VI_TRIG_PROT_OFF As Short = 2
	Public Const VI_TRIG_PROT_SYNC As Short = 5
	
	Public Const VI_READ_BUF As Short = 1
	Public Const VI_WRITE_BUF As Short = 2
	Public Const VI_READ_BUF_DISCARD As Short = 4
	Public Const VI_WRITE_BUF_DISCARD As Short = 8
	Public Const VI_ASRL_IN_BUF As Short = 16
	Public Const VI_ASRL_OUT_BUF As Short = 32
	Public Const VI_ASRL_IN_BUF_DISCARD As Short = 64
	Public Const VI_ASRL_OUT_BUF_DISCARD As Short = 128
	
	Public Const VI_FLUSH_ON_ACCESS As Short = 1
	Public Const VI_FLUSH_WHEN_FULL As Short = 2
	Public Const VI_FLUSH_DISABLE As Short = 3
	
	Public Const VI_NMAPPED As Short = 1
	Public Const VI_USE_OPERS As Short = 2
	Public Const VI_DEREF_ADDR As Short = 3
	
	Public Const VI_TMO_IMMEDIATE As Integer = &H0
	Public Const VI_TMO_INFINITE As Short = &HFFFFs
	Public Const VI_INFINITE As Short = &HFFFFs
	
	Public Const VI_NO_LOCK As Short = 0
	Public Const VI_EXCLUSIVE_LOCK As Short = 1
	Public Const VI_SHARED_LOCK As Short = 2
	
	Public Const VI_NO_SEC_ADDR As Short = &HFFFFs
	
	Public Const VI_ASRL_PAR_NONE As Short = 0
	Public Const VI_ASRL_PAR_ODD As Short = 1
	Public Const VI_ASRL_PAR_EVEN As Short = 2
	Public Const VI_ASRL_PAR_MARK As Short = 3
	Public Const VI_ASRL_PAR_SPACE As Short = 4
	
	Public Const VI_ASRL_STOP_ONE As Short = 10
	Public Const VI_ASRL_STOP_TWO As Short = 20
	
	Public Const VI_ASRL_FLOW_NONE As Short = 0
	Public Const VI_ASRL_FLOW_XON_XOFF As Short = 1
	Public Const VI_ASRL_FLOW_RTS_CTS As Short = 2
	
	Public Const VI_ASRL_END_NONE As Short = 0
	Public Const VI_ASRL_END_LAST_BIT As Short = 1
	Public Const VI_ASRL_END_TERMCHAR As Short = 2
	Public Const VI_ASRL_END_BREAK As Short = 3
	
	Sub debugtest()
		Dim ErrorCode As Integer
		
		Dim ErrorMessage As New VB6.FixedLengthString(255)
		Stop
		' Ret& = hpe1428a_errorQuery(instrumentHandle&, ErrorCode&, ErrorMessage$)
		Debug.Print(ErrorMessage.Value)
		Debug.Print(ErrorCode)
		Stop
	End Sub
End Module