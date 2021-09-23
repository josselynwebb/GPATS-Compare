Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module DscopeMain
	'//2345678901234567890123456789012345678901234567890123456789012345678901234567890
	'////////////////////////////////////////////////////////////////////////////
	'//
	'// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
	'//
	'// File:       Main.bas
	'//
	'// Date:       14FEB06
	'//
	'// Purpose:    SAIS: Digitizing Oscilloscope Front Panel
	'//
	'// Instrument: Agilent E1428A Digitizing Oscilloscope (Dso)
	'//
	'//
	'// Revision History
	'// Rev      Date                  Reason                           Author
	'// =======  =======    =======================================     ===========
	'// 1.1.0.0  27Oct06    Initial Release                             EADS, North America Defense
	'// 1.1.0.1  08Nov06    Below are the changes                       M.Hendricksen, EADS
	'//                     - Added variable to Main()which will
	'//                         indicate that SFP is initializing, this will
	'//                         prevent the SFP from sending cmds to instr, before
	'//                         querying it. It is checked in WriteMsg ().
	'//                     - Added Query in ConfigGetCurrent() to find out
	'//                         the last measurment function performed and set
	'//                         the GUI accordingly.
	'// 1.2.0.1 04Jan07     Following changes were made:                M.Hendricksen EADS, North America Defense
	'//                     - Corrected the new of a variable in GetWaveform()
	'//                         that is used to watch for visa errors.
	'//                     - Made provisions to set a default visa TIME-OUT
	'//                         in VerifyInstrumentCommunition()
	'//                     - Add Function Declaration in VIPLibrary.bas for
	'//                         function atxmlDF_viSetAttribute used to set
	'//                         visa time-out variable.
	'//                     - Correct the Lib name of the function declaration
	'//                         located in Visa32.bas.
	'// 1.2.1.0 29Jul09     Added code to allow SFP to remain open until EADS, North America Defense
	'//                     app is closed while running in SINGLE Mode
	'// 1.2.2.0 30Jul09     Added code to ensure compatability with      EADS, North America Defense
	'//                     legacy .DSO files - PCR 505
	'************************************************************************
	
	
	Public DebugDwh As Short 'Debugging Variable
	'-----------------PUBLIC Constants------------------------------------
	Public Q As String 'Utility ASCII Quote (") Symbol
	'-----------------Public Variables------------------------------------
	Public Digitizing As Short 'Digitizing Process Flag
	Public DiskWaveformFlag As Short 'Current Memory Waveform is/isn't from disk
	Public instrumentHandle As Integer 'VISA Instrument Resource Handle
	Public ErrorStatus As Integer 'VISA Error Return Value
	Public ResourceName As String 'VISA Instrument Name and ULA
	Public ReadBuffer As New VB6.FixedLengthString(8100) 'Input Measurement Buffer
	Public dAcqTime As Double 'Waveform acquisition time V1.8 JCB
	Public bTIP_Running As Boolean 'Flag indicating SAIS was invoked by TIP Studio
	Public bBuildingTIPstr As Boolean 'Flag indicating TIP design mode is building the COMP CMD string
	Public bTIP_UserMeas As Boolean 'Flag indicating a USER defined measurement
	Public bTIP_Error As Boolean 'Flag indicating an error has occured in TIP
	Public sTIP_Mode As String 'TIP mode indicator
	Public sTIP_CMDstring As String 'TIP COMP Command string defining instrument setup
	Public sTIP_ACQstring As String
	Public sTIP_Measured As String 'Measured value not in Eng. Notation format (Absolute Value)
	Public sDelayMeasStr As String
	Public bDoNotTalk As Boolean
	Public bSkipDialog As Boolean
	Public sWindowsDir As String 'Path of Windows Directory
	Public iAttenCH1 As Short
	Public iAttenCH2 As Short
	Public sSegFileName As String 'Save waveform segment file name
	Public CurrentMeas As Short
	Public TraceColor(3) As Integer
	Public bTrigMode As Boolean
	Public bInstrumentMode As Boolean ' True if in GetCurrentInstrument Mode and when SFP is only to send queries
	
	'-----------------Arrays---------------------------------------
	Dim DataArray(8000) As Short
	Dim SAMPLE_TYPE(3) As String
	'-----------------Input Text Box / Spin Button Instruction -----------
	Public Const VOLTAGE_OFFSET_CHANNEL_1 As Short = 3
	Public MakingMeasurement As Short
	Public MeasureCode(20) As InstParameter
	Const HORZ_SEN_PAN As Short = 1
	Const CURS_POS_PAN As Short = 2
	Const INFO_TXT_PAN As Short = 3
	Const MODAL As Short = 1
	Const MAX_DEL_RATIO As Short = 1000
	Const TIME_BASE_BREAK_POINT As Double = 0.000001
	Const MIN_DEL_RATIO As Short = -16
	Const MIN_DEL As Double = -0.000000008
	Const TRIG_LEV_ADJ_UPR As Double = 1.5
	Const TRIG_LEV_ADJ_LWR As Double = 1.5
	Const CHANNEL_1 As Short = 0
	Const CHANNEL_2 As Short = 1
	Const MATH_AREA As Short = 2
	Const MEMORY_AREA As Short = 3
	Const EDGE_TRIG As Short = 0
	Const DEL_TRIG As Short = 1
	Const TV_TRIG As Short = 2
	Const PERCENT_LEVEL As Short = 1
	Const ECLT_0 As Short = 2
	Const ECLT_1 As Short = 3
	Const EXT_TRIG As Short = 4
	Const WF_TAB As Short = 5
	Dim ContinuousFlag As Short
	Dim ChannelParam(2) As String
	Dim TempWaveData(3) As Waveform
	Public WaveTime() As Single
	Public WaveMagnitude() As Double
	Dim ChannelOn(3) As Short
	Dim NumberOfTraces As Short
	'Dim DataPoints%
	Dim VSenseBase(12) As Single
	Dim TraceAssociation(3) As Short
	Dim WhatTrace(3) As Short
	Dim VTraceListItem As Short
	Public HScaleListItem As Short
	Dim HoldType As String
	Dim HoldEvents As String
	Dim HoldTime As String
	Dim DelayType As String
	Dim DelayEvents As String
	Dim DelayTime As String
	Dim MEMRangeSetting As Single
	Dim MEMOffsetSetting As Single
	'Main Settings Constants
	'*NOTE*: For proper TIPs operation, the order of these constants should not be altered
	'CHANNEL 1 subsystem settings
	Public Const IN1_COUP As Short = 1
	Public Const IN1_HFREJ As Short = 2
	Public Const IN1_LFREJ As Short = 3
	Public Const IN1_OFFSET As Short = 4
	Public Const IN1_ATTN_FAC As Short = 5
	Public Const IN1_VSENS As Short = 6
	
	'CHANNEL 2 subsystem settings
	Public Const IN2_COUP As Short = 7
	Public Const IN2_HFREJ As Short = 8
	Public Const IN2_LFREJ As Short = 9
	Public Const IN2_OFFSET As Short = 10
	Public Const IN2_ATTN_FAC As Short = 11
	Public Const IN2_VSENS As Short = 12
	
	'Output subsystem settings
	Public Const ECLT0_STATE As Short = 13
	Public Const ECLT1_STATE As Short = 14
	Public Const OUT_TRIG_STATE As Short = 15
	Public Const OUTP_STATE As Short = 16
	
	'Trigger subsystem settings
	Public Const TRIG_MODE As Short = 17
	Public Const TRIG_SOUR As Short = 18
	Public Const TRIG_LEV As Short = 19
	Public Const TRIG_SLOPE As Short = 20
	Public Const TRIG_SENS As Short = 21
	Public Const TRIG_DEL As Short = 22
	Public Const TRIG_DEL_SLOPE As Short = 23
	Public Const TRIG_DEL_SOUR As Short = 24
	Public Const TRIG_OCC As Short = 25
	Public Const TRIG_STAND As Short = 26
	Public Const TRIG_FIELD As Short = 27
	Public Const TRIG_LINE As Short = 28
	Public Const EXT_TRIG_COU As Short = 29
	Public Const HOLD_OFF As Short = 30
	
	'Timebase/Acquisition subsystem settings
	Public Const WF_POINTS As Short = 31
	Public Const SAMPLE_MODE As Short = 32
	Public Const ACQ_MODE As Short = 33
	
	Public Const HORZ_SENS As Short = 34 'Changed HORZ_SENS and REF_DELAY b/c the range should be setup before the delay.  DME PCR 510 #4.
	Public Const REF_DELAY As Short = 35
	
	Public Const H_REF_POS As Short = 36
	
	Public Const ACQ_TYPE As Short = 37
	Public Const AVR_COUNT As Short = 38
	Public Const COMPLETION As Short = 39
	
	'Measure subsystem settings
	Public Const MEAS_MODE As Short = 40
	Public Const MEAS_SOUR As Short = 41
	Public Const MEAS_UNIT As Short = 42
	Public Const MEAS_LOW As Short = 43
	Public Const MEAS_UPP As Short = 44
	Public Const MEAS_DEF As Short = 45
	
	'Function (Math) subsystem settings
	Public Const FUNC_RANGE As Short = 46
	Public Const FUNC_OFFSET As Short = 47
	Public Const FUNC_ADD As Short = 48
	Public Const FUNC_SUBTRACT As Short = 49
	Public Const FUNC_MULTIPLY As Short = 50
	Public Const FUNC_DIFFERENTIATE As Short = 51
	Public Const FUNC_INTEGRATE As Short = 52
	Public Const FUNC_INVERT As Short = 53
	
	'Non-COMP CMD (GUI) settings
	Public Const MEAS_FUNCTION As Short = 54
	Public Const TRACE_THICKNESS As Short = 55
	Public Const CH1_TRACE_COLOR As Short = 56
	Public Const CH2_TRACE_COLOR As Short = 57
	Public Const MATH_TRACE_COLOR As Short = 58
	Public Const MEMORY_TRACE_COLOR As Short = 59
	Public Const SEGMENT_FILE As Short = 60
	
	'Memory subsystem settings
	Public Const MEM_RANGE As Short = 61
	Public Const MEM_OFFSET As Short = 62
	
	'Probe COMP setting
	Public Const PROBE_COMP As Short = 63
	
	Public Const MAX_SETTINGS As Short = PROBE_COMP
	Public Const PNT8000_HLIST As Short = TRIG_LEV 'Changed to Global V1.8 JCB
	Const HORZ_DIV As Short = 10
	Const VERT_DIV As Short = 10
	Const DEF_V_RANGE As Short = 3
	Const DEF_H_RANGE As Short = 11
	Const H_LEFT As String = "LEFT"
	Const H_CENTER As String = "CENT"
	Const H_RIGHT As String = "RIGHT"
	Const D_EVENTS As Short = 1
	Const D_TIME As Short = 0
	Const HOLD_MAX_EVENTS As String = "16000000"
	Const HOLD_MIN_EVENTS As String = "2"
	Const HOLD_MAX_TIME As String = "0.32"
	Const HOLD_MIN_TIME As String = "40E-9"
	Const DELAY_MAX_EVENTS As String = "16000000"
	Const DELAY_MIN_EVENTS As String = "2"
	Const DELAY_MAX_TIME As String = "0.16"
	Const DELAY_MIN_TIME As String = "40E-9"
	Const PTS_500 As Short = 0
	Const REALTIME As Short = 0
	'UPGRADE_WARNING: Lower bound of array MainParam was changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
	Public MainParam(MAX_SETTINGS) As InstParameter
	'UPGRADE_WARNING: Lower bound of array ListBoxFiller was changed from 1,0 to 0,0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
	Public ListBoxFiller(MAX_SETTINGS, 30) As String
	'UPGRADE_WARNING: Lower bound of array ParamCode was changed from 1,0 to 0,0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
	Public ParamCode(MAX_SETTINGS, 30) As String
	'Measurment Constants
	Public Const DC_RMS As Short = 1
	Public Const AC_RMS As Short = 2
	Public Const PERIOD As Short = 3
	Public Const FREQUENCY As Short = 4
	Public Const POS_PWIDTH As Short = 5
	Public Const NEG_PWIDTH As Short = 6
	Public Const DUTY_CYCLE As Short = 7
	Public Const DELAY_MEAS As Short = 8
	Public Const RISE_TIME As Short = 9
	Public Const FALL_TIME As Short = 10
	Public Const PRE_SHOOT As Short = 11
	Public Const OVR_SHOOT As Short = 12
	Public Const V_PP As Short = 13
	Public Const V_MIN As Short = 14
	Public Const V_MAX As Short = 15
	Public Const V_AVERAGE As Short = 16
	Public Const V_AMPL As Short = 17
	Public Const V_BASE As Short = 18
	Public Const V_TOP As Short = 19
	Public Const MEAS_OFF As Short = 20
	
	'Splash Form
	Public Const INSTRUMENT_NAME As String = "Digitizing Oscilloscope"
	Public Const MANUF As String = "ZTec"
	Public Const MODEL_CODE As String = "ZT1428VXI"
	Public Const RESOURCE_NAME As String = "VXI::165"
	Public frmParent As Object
	'Delay Measurement Variables/Constants
	Public Const START_LEVEL As Short = 0
	Public Const STOP_LEVEL As Short = 1
	Public Const START_EDGE As Short = 2
	Public Const STOP_EDGE As Short = 3
	Public Const START_SOURCE As Short = 4
	Public Const STOP_SOURCE As Short = 5
	Public Const START_SLOPE As Short = 6
	Public Const STOP_SLOPE As Short = 7
	Public StartSource As String
	Public StopSource As String
	Dim StartSlope As String
	Dim StopSlope As String
	Dim DelayLevelUnit As String
	Dim StartLevel As InstParameter
	Dim StopLevel As InstParameter
	Dim StartEdge As InstParameter
	Dim StopEdge As InstParameter
	Dim DelMeasParam(STOP_SLOPE, 5) As String
	' Math Sources
	Const MATH_ADD As Short = 0
	Const MATH_SUBTRACT As Short = 1
	Const MATH_MULTIPLY As Short = 2
	Const MATH_DIFFERENTIATE As Short = 3
	Const MATH_INTEGRATE As Short = 4
	Const MATH_INVERT As Short = 5
	
	' Used to get waveform in Digitize function
	Const MATH_FUNC1 As Short = 6
	Const MEM_WMEM1 As Short = 5
	'***Read Instrument Mode***
	Public iStatus As Short
	Public gctlParse As New VIPERT_Common_Controls.Common
	
	
	'JRC Added 033009
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	Declare Function GetPrivateProfileString Lib "kernel32"  Alias "GetPrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
	
	Declare Function GetWindowsDirectory Lib "kernel32"  Alias "GetWindowsDirectoryA"(ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
	
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	Declare Function WritePrivateProfileString Lib "kernel32"  Alias "WritePrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Integer
	'###############################################################
	
	
	Sub SetMathFunction(ByRef SetRange As Short)
		'Declare Procedure Level variables
		Dim Source1 As String
		Dim Source2 As String
		
		If frmZT1428.cboMathSourceA.SelectedIndex <> -1 And frmZT1428.cboMathSourceB.SelectedIndex <> -1 Then
			Source1 = ChannelParam(frmZT1428.cboMathSourceA.SelectedIndex)
			Source2 = ChannelParam(frmZT1428.cboMathSourceB.SelectedIndex)
		Else
			Source1 = ChannelParam(0)
			Source2 = ChannelParam(0)
		End If
		Select Case frmZT1428.cboMathFunction.SelectedIndex
			Case MATH_ADD
				WriteMsg("FUNC1:ADD " & Source1 & "," & Source2)
				frmZT1428.fraMathSourceB.Visible = True
				MainParam(FUNC_RANGE).SetUOM = "V"
				MainParam(FUNC_OFFSET).SetUOM = "V"
			Case MATH_SUBTRACT
				WriteMsg("FUNC1:SUBT " & Source1 & "," & Source2)
				frmZT1428.fraMathSourceB.Visible = True
				MainParam(FUNC_RANGE).SetUOM = "V"
				MainParam(FUNC_OFFSET).SetUOM = "V"
			Case MATH_MULTIPLY
				WriteMsg("FUNC1:MULT " & Source1 & "," & Source2)
				frmZT1428.fraMathSourceB.Visible = True
				MainParam(FUNC_RANGE).SetUOM = "V"
				MainParam(FUNC_OFFSET).SetUOM = "V"
			Case MATH_DIFFERENTIATE
				WriteMsg("FUNC1:DIFF " & Source1)
				frmZT1428.fraMathSourceB.Visible = False
				MainParam(FUNC_RANGE).SetUOM = "V"
				MainParam(FUNC_OFFSET).SetUOM = "V"
			Case MATH_INTEGRATE
				WriteMsg("FUNC1:INT " & Source1)
				frmZT1428.fraMathSourceB.Visible = False
				MainParam(FUNC_RANGE).SetUOM = "V"
				MainParam(FUNC_OFFSET).SetUOM = "V"
			Case MATH_INVERT
				WriteMsg("FUNC1:INV " & Source1)
				frmZT1428.fraMathSourceB.Visible = False
				MainParam(FUNC_RANGE).SetUOM = "V"
				MainParam(FUNC_OFFSET).SetUOM = "V"
		End Select
		
		If SetRange Then
			SetMathRange(WhatTrace(MATH_AREA), False)
		End If
		
	End Sub
	
	
	Sub SetMathRange(ByRef TraceIndex As Short, ByRef InstrumentRange As Short)
		'Declare Procedure Level variables
		Dim Voffset As Single
		Dim VRAnge As Single
		
		If InstrumentRange Then
			WriteMsg("FUNC1:RANGE?")
			MainParam(FUNC_RANGE).SetCur = Str(Val(ReadMsg()) / 10)
			WriteMsg("FUNC1:OFFSET?")
			MainParam(FUNC_OFFSET).SetCur = Str(Val(ReadMsg()) / 10)
		End If
		
		VRAnge = Val(MainParam(FUNC_RANGE).SetCur)
		Voffset = Val(MainParam(FUNC_OFFSET).SetCur)
		
		frmZT1428.txtMathRange.Text = EngNotate(Val(MainParam(FUNC_RANGE).SetCur), MainParam(FUNC_RANGE))
		frmZT1428.txtMathVOffset.Text = EngNotate(Val(MainParam(FUNC_OFFSET).SetCur), MainParam(FUNC_OFFSET))
		frmZT1428.cwgScopeDisplay.Axes.Item(2 + MATH_AREA).Maximum = (VRAnge * 5!) + Voffset
		frmZT1428.cwgScopeDisplay.Axes.Item(2 + MATH_AREA).Minimum = (-VRAnge * 5!) + Voffset
		
		System.Windows.Forms.Application.DoEvents()
		
	End Sub
	
	
	Sub FindMaxMin(ByRef WvMagn() As Double, ByRef Min As Double, ByRef Max As Double)
		'Declare Procedure Level variables
		Dim Elements As Short
		Dim count As Integer
		
		Min = WvMagn(0)
		Max = WvMagn(0)
		For count = 1 To UBound(WvMagn)
			If WvMagn(count) > Max Then
				Max = WvMagn(count)
			End If
			If WvMagn(count) < Min Then
				Min = WvMagn(count)
			End If
		Next count
		
	End Sub
	
	
	Sub GetMemoryWave(ByRef WvMagn() As Double, ByRef TraceNumber As Short, ByRef ChangeRange As Short)
		'Declare Procedure Level variables
		Dim PreambleBuffer As String
		Dim VPoints As String
		Dim WvTime(8000) As Single
		Dim offset As Single
		Dim LogDelta As Single
		Dim RangeSetting As Single
		
		WriteMsg("VIEW WMEM1")
		WriteMsg("WAV:SOUR WMEM1")
		WriteMsg("VIEW WMEM1")
		WriteMsg("WAV:PRE?")
		PreambleBuffer = ReadMsg()
		'ReadMsg$() returns 8100 charters, strip nulls and append a Line Feed to end of preamble
		PreambleBuffer = Trim(StripNullCharacters(PreambleBuffer)) & vbLf
		WriteMsg("WAV:DATA?")
		VPoints = ReadMsg()
		UnpackWaveform(PreambleBuffer, VPoints, WvMagn, WvTime, TraceNumber)
		frmZT1428.cwgScopeDisplay.Axes.Item(2 + TraceNumber).Maximum = (MEMRangeSetting * 5) + MEMOffsetSetting
		frmZT1428.cwgScopeDisplay.Axes.Item(2 + TraceNumber).Minimum = (-MEMRangeSetting * 5) + MEMOffsetSetting
		
	End Sub
	
	
	Sub AutoScaleScope()
		'Declare Procedure Level variables
		Dim OffsetCh1 As Single
		Dim OffsetCh2 As Single
		Dim RangeCh1 As Single
		Dim RangeCh2 As Single
		Dim TimeBase As Single
		Dim TrigLevel As Single
		Dim TrigSlope As String
		Dim TrigSource As String
		Dim HoldVal As String
		Dim HoldElem(2) As String
		Dim TrigHold As Single
		Dim RangeIndex As Short
		Dim Source As Short
		Dim Items As Short
		Dim ContinuousState As Short
		Dim Coupling As String 'String containing coupling mode read from DSO, V1.8 JCB
		
		frmZT1428.cmdReset.Enabled = False 'Disable RESET while auto-scaling due to intermittent lock-up
		frmZT1428.cmdAutoscale.Enabled = False
		MakingMeasurement = True
		frmZT1428.cmdMeasure.Enabled = False
		
		frmZT1428.fraDisplay.Enabled = False
		frmZT1428.fraDataPoints.Enabled = False
		frmZT1428.chkDisplayTrace(0).Enabled = False
		frmZT1428.chkDisplayTrace(1).Enabled = False
		frmZT1428.chkDisplayTrace(2).Enabled = False
		frmZT1428.chkDisplayTrace(3).Enabled = False
		frmZT1428.optDataPoints(0).Enabled = False
		frmZT1428.optDataPoints(1).Enabled = False
		
		frmZT1428.fraAcqMode.Enabled = False
		frmZT1428.cboAcqMode.Enabled = False
		frmZT1428.fraSampleType.Enabled = False
		frmZT1428.cboSampleType.Enabled = False
		frmZT1428.fraAvgCount.Enabled = False
		
		'Perform Auto Scale
		WriteMsg("AUT")
		Delay(2)
		'********Query for new configuration******
		'All Markers To Off
		'All Memories To Off
		WriteMsg("CHAN1:OFFS?")
		MainParam(IN1_OFFSET).SetCur = VB6.Format(Val(ReadMsg()))
		WriteMsg("CHAN2:OFFS?")
		MainParam(IN2_OFFSET).SetCur = VB6.Format(Val(ReadMsg()))
		
		WriteMsg("CHAN1:RANG?")
		RangeCh1 = Val(ReadMsg())
		
		WriteMsg("CHAN2:RANG?")
		RangeCh2 = Val(ReadMsg())
		
		For RangeIndex = 0 To 11
			If Val(ParamCode(IN1_VSENS, RangeIndex)) >= RangeCh1 Then
				If Val(ParamCode(IN1_VSENS, RangeIndex + 1)) < RangeCh1 Then
					frmZT1428.cboVertRange(0).SelectedIndex = RangeIndex
					WriteMsg("CHAN1:RANG " & ParamCode(IN1_VSENS, RangeIndex))
				ElseIf RangeIndex = 11 Then 
					frmZT1428.cboVertRange(0).SelectedIndex = 0
					WriteMsg("CHAN1:RANG " & ParamCode(IN1_VSENS, 0))
				End If
			End If
			If Val(ParamCode(IN2_VSENS, RangeIndex)) >= RangeCh2 Then
				If Val(ParamCode(IN2_VSENS, RangeIndex + 1)) < RangeCh2 Then
					frmZT1428.cboVertRange(1).SelectedIndex = RangeIndex
					WriteMsg("CHAN2:RANG " & ParamCode(IN2_VSENS, RangeIndex))
				ElseIf RangeIndex = 11 Then 
					frmZT1428.cboVertRange(1).SelectedIndex = 0
					WriteMsg("CHAN2:RANG " & ParamCode(IN2_VSENS, 0))
				End If
			End If
		Next RangeIndex
		
		'Function To Off
		'Measure To Off
		WriteMsg("TIM:RANG?")
		TimeBase = Val(ReadMsg())
		For RangeIndex = 0 To 29
			If Val(ParamCode(HORZ_SENS, RangeIndex)) = TimeBase Then
				If Val(MainParam(WF_POINTS).SetCur) = 500 Then
					frmZT1428.cboHorizRange.SelectedIndex = RangeIndex
					HScaleListItem = RangeIndex
				Else
					HScaleListItem = RangeIndex + 4
					If HScaleListItem > 29 Then
						HScaleListItem = 29
					End If
					frmZT1428.cboHorizRange.SelectedIndex = HScaleListItem
				End If
				Exit For
			End If
		Next RangeIndex
		
		WriteMsg("TRIG:HOLD?")
		HoldVal = Trim(StripNullCharacters(ReadMsg()))
		Items = StringToList(HoldVal, HoldElem, ",")
		If Trim(HoldElem(1)) = "TIME" Then
			HoldTime = HoldElem(2)
			frmZT1428.optHoldOff(0).Value = True
			MainParam(HOLD_OFF).SetCur = HoldTime
		Else
			HoldEvents = HoldElem(2)
			frmZT1428.optHoldOff(1).Value = True
			MainParam(HOLD_OFF).SetCur = HoldEvents
		End If
		
		WriteMsg("TRIG:LEVEL?")
		MainParam(TRIG_LEV).SetCur = VB6.Format(Val(ReadMsg()))
		
		WriteMsg(Trim(MainParam(TRIG_SLOPE).SetCod) & "?")
		TrigSlope = StripNullCharacters(ReadMsg())
		If Trim(StripNullCharacters(TrigSlope)) = ParamCode(TRIG_SLOPE, 0) Then
			frmZT1428.optTrigSlope(0)._Value = True
		Else
			frmZT1428.optTrigSlope(1)._Value = True
		End If
		WriteMsg(Trim(MainParam(TRIG_SOUR).SetCod) & "?")
		TrigSource = Trim(StripNullCharacters(ReadMsg()))
		For Source = 0 To 4
			If TrigSource = ParamCode(TRIG_SOUR, Source) Then
				frmZT1428.cboTrigSour.SelectedIndex = Source
			End If
		Next Source
		frmZT1428.optReference(0).Value = True
		frmZT1428.optTrigMode(0).Value = True
		FillTextBoxes()
		If ChannelOn(MATH_AREA) Then
			SetMathRange(WhatTrace(MATH_AREA), True)
			WriteMsg("VIEW FUNC1")
			WriteMsg("*CLS")
		End If
		frmZT1428.cboAcqMode.SelectedIndex = 0
		frmZT1428.fraAcqMode.Enabled = True
		frmZT1428.cboAcqMode.Enabled = True
		System.Windows.Forms.Application.DoEvents()
		
		If Not ContinuousFlag Then
			If bTIP_Running <> True Then
				If frmZT1428.optDataPoints(1).Value = True Then WriteMsg("ACQ:POIN 8000")
				DigitizeWaveform()
			End If
		Else
			WriteMsg("DIG CHAN1,CHAN2")
			WriteMsg("MEM:VME:STAT ON") 'Removed repetitive STATE ON calls   'DJoiner  01/14/02
			WriteMsg("RUN")
			frmZT1428.cmdAutoscale.Enabled = True
		End If
		
		'Added the following code to check the scope for the coupling mode. If the mode is
		'AC but DC is selected on the front panel, the front panel selection is changed
		'to AC. This is needed because the scope will occasionally change the coupling,
		'such as when a large DC signal is being measured. V1.8 JCB
		WriteMsg("CHAN1:COUP?")
		Coupling = StripNullCharacters(ReadMsg())
		If InStr(Coupling, "AC") Then
			frmZT1428.optCoupling1(2).Value = True
		End If
		WriteMsg("CHAN2:COUP?")
		Coupling = StripNullCharacters(ReadMsg())
		If InStr(Coupling, "AC") Then
			frmZT1428.optCoupling2(2).Value = True
		End If
		
		frmZT1428.cmdReset.Enabled = True
		
	End Sub
	
	
	Sub ChangeColorBox()
		
		frmZT1428.picTraceColor.BackColor = System.Drawing.ColorTranslator.FromOle(TraceColor(frmZT1428.cboTraceColor.SelectedIndex))
		
	End Sub
	
	
	Sub ChangeDelayMeasParam(ByRef ParamIndex As Short, ByRef SelectionIndex As Short)
		
		Select Case ParamIndex
			Case START_SOURCE
				StartSource = DelMeasParam(ParamIndex, SelectionIndex)
			Case STOP_SOURCE
				StopSource = DelMeasParam(ParamIndex, SelectionIndex)
			Case START_SLOPE
				StartSlope = DelMeasParam(ParamIndex, SelectionIndex)
			Case STOP_SLOPE
				StopSlope = DelMeasParam(ParamIndex, SelectionIndex)
			Case START_LEVEL
				DelayLevelUnit = DelMeasParam(ParamIndex, SelectionIndex)
				If SelectionIndex = PERCENT_LEVEL Then
					StartLevel.SetDef = "0"
					StopLevel.SetDef = "0"
					StartLevel.SetMin = "-25"
					StopLevel.SetMin = "-25"
					StartLevel.SetMax = "125"
					StopLevel.SetMax = "125"
					StartLevel.SetUOM = "%"
					StopLevel.SetUOM = "%"
				Else
					StartLevel.SetDef = "0"
					StopLevel.SetDef = "0"
					StartLevel.SetMin = "-250"
					StopLevel.SetMin = "-250"
					StartLevel.SetMax = "250"
					StopLevel.SetMax = "250"
					StartLevel.SetUOM = ""
					StopLevel.SetUOM = ""
				End If
				If Val(StartLevel.SetCur) > Val(StartLevel.SetMax) Then
					StartLevel.SetCur = StartLevel.SetMax
				End If
				If Val(StartLevel.SetCur) < Val(StartLevel.SetMin) Then
					StartLevel.SetCur = StartLevel.SetMin
				End If
				If Val(StopLevel.SetCur) > Val(StopLevel.SetMax) Then
					StopLevel.SetCur = StopLevel.SetMax
				End If
				If Val(StopLevel.SetCur) < Val(StopLevel.SetMin) Then
					StopLevel.SetCur = StopLevel.SetMin
				End If
				frmZT1428.txtStartLevel.Text = EngNotate(Val(StartLevel.SetCur), StartLevel)
				frmZT1428.txtStopLevel.Text = EngNotate(Val(StopLevel.SetCur), StopLevel)
				RangeUpdate(START_LEVEL)
		End Select
		
	End Sub
	
	
	Sub ChangeMeasurement(ByRef MeasIndex As Short)
		
		frmZT1428.txtDataDisplay.Text = ""
		bTIP_UserMeas = False
		If frmZT1428.cboMeasSour.Visible = False Then
			frmZT1428.cboMeasSour.Visible = True
			frmZT1428.fraDelayParam.Visible = False
		End If
		Select Case MeasIndex
			Case DELAY_MEAS 'Delay (2 channel) measurement
				bTIP_UserMeas = True
				frmZT1428.cboMeasSour.Visible = False
				frmZT1428.fraDelayParam.Visible = True
			Case MEAS_OFF 'No measurement
				WriteMsg("RUN")
			Case Else 'All other measurement functions
				WriteMsg(MainParam(MEAS_SOUR).SetCod & MainParam(MEAS_SOUR).SetCur)
				WriteMsg("MEAS:MODE STAN")
		End Select
		CurrentMeas = MeasIndex
		
	End Sub
	
	
	Sub ChangeTraceColor()
		'Declare Procedure Level variables
		Dim TraceToChange As Short
		
		TraceToChange = frmZT1428.cboTraceColor.SelectedIndex
		
		'If TIP is running, color is set by saved TIP CMD string
		If Not bSkipDialog Then
			frmZT1428.CommonDialog1Color.Color = System.Drawing.ColorTranslator.FromOle(TraceColor(TraceToChange))
			On Error Resume Next
			frmZT1428.CommonDialog1Color.ShowDialog()
			On Error GoTo 0
			TraceColor(TraceToChange) = System.Drawing.ColorTranslator.ToOle(frmZT1428.CommonDialog1Color.Color)
		End If
		
		If WhatTrace(TraceToChange) <> -1 Then
			frmZT1428.cwgScopeDisplay.Plots.Item(1 + WhatTrace(TraceToChange)).PointColor = System.Convert.ToUInt32(TraceColor(TraceToChange))
		End If
		frmZT1428.picTraceColor.BackColor = System.Drawing.ColorTranslator.FromOle(TraceColor(TraceToChange))
		
	End Sub
	
	
	Sub ChangeTraceThickness()
		'Declare Procedure Level variables
		Dim LineStyle As Short
		
		Select Case frmZT1428.cboTraceThickness.SelectedIndex
			Case 0
				LineStyle = CWUIControlsLib.CWPointStyles.cwPointSimpleDot
			Case 1
				LineStyle = CWUIControlsLib.CWPointStyles.cwPointSmallEmptySquare
			Case 2
				LineStyle = CWUIControlsLib.CWPointStyles.cwPointSmallSolidSquare
		End Select
		frmZT1428.cwgScopeDisplay.Plots.Item(1).PointStyle = LineStyle
		frmZT1428.cwgScopeDisplay.Plots.Item(2).PointStyle = LineStyle
		frmZT1428.cwgScopeDisplay.Plots.Item(3).PointStyle = LineStyle
		frmZT1428.cwgScopeDisplay.Plots.Item(4).PointStyle = LineStyle
		
	End Sub
	
	
	Sub DigitizeWaveform()
		'Declare Procedure Level variables
		Dim PreambleBuffer As String
		Dim NextChannel As Short
		Dim dCurrentMeasurement As Double
		Dim TempWaveMagnitude(8000) As Double
		Dim MathTempWave() As Double
		Dim Ch2TempWave() As Double
		Dim Ch1TempWave() As Double
		Dim MemTempWave() As Double
		Dim VPoints As String
		Dim WaveSize As Short
		Dim WavePoint As Short
		Dim ErrorMsg As String
		Dim dXIncr As Double
		Dim dXOrig As Double
		Dim dYIncr As Double
		Dim dYOrig As Double
		Dim Tme As Single
		Dim Ttl As Single
		Dim dHScale As Double
		Dim DDelay As Double
		Dim PointMlt As Short
		Dim AcqMode As Short
		Dim i As Short
		Dim TempAxis As Double
		
		MakingMeasurement = True
		ContinuousFlag = True
		frmZT1428.cmdMeasure.Enabled = False
		frmZT1428.cmdAutoscale.Enabled = False
		frmZT1428.fraDataPoints.Enabled = False
		''    frmZT1428.fraAcqMode.Enabled = False    JDH: Removed to allow escape from
		''    frmZT1428.cboAcqMode.Enabled = False    WaitForTrigger when no trigger.
		frmZT1428.fraSampleType.Enabled = False
		frmZT1428.cboSampleType.Enabled = False
		frmZT1428.fraAvgCount.Enabled = False
		frmZT1428.optDataPoints(0).Enabled = False
		frmZT1428.optDataPoints(1).Enabled = False
		
		frmZT1428.fraDisplay.Enabled = False
		frmZT1428.chkDisplayTrace(0).Enabled = False
		frmZT1428.chkDisplayTrace(1).Enabled = False
		frmZT1428.chkDisplayTrace(2).Enabled = False
		frmZT1428.chkDisplayTrace(3).Enabled = False
		sTIP_Measured = "0"
		
		WriteMsg("STOP")
		
		'set the hscale, sending each time prevents the measured line on scope from being shortened
		SetHScale()
		
		WriteMsg("MEM:VME:STAT ON")
		'If TIP run-mode, then send CMD string to instrument now
		If InStr(1, sTIP_Mode, "TIP_RUN") > 0 Or InStr(1, sTIP_Mode, "TIP_MEAS") > 0 Then
			If Not bTrigMode Then
				If Not bGetInstrumentStatus() Then EndProgram()
			End If
			If sTIP_Mode = "TIP_RUNPERSIST" Then frmZT1428.chkContinuous.Value = True
			If frmZT1428.picMemOptions.Visible Then
				'Udate the MEMORY waveform on the GUI screen
				RegisterEntry(MEM_RANGE)
				RegisterEntry(MEM_OFFSET)
			End If
		End If
		WriteMsg("RUN")
		Tme = VB.Timer()
		
		'Removed delay multiplier of 16 for 8000 point waveforms. Acquisition will take the
		'same amount of time at either 500 or 8000 points (scope always takes 8000 points)
		'Changed V1.8 JCB
		''This delay is now taken care of in WaitForTrigger
		''    If Val(MainParam(HORZ_SENS).SetCur) < 0.5 Then
		''        Do
		''        Loop Until (Timer - Tme!) > 0.7 + (Val(MainParam(HORZ_SENS).SetCur) * 15)
		''    End If
		
		WaveSize = Val(MainParam(WF_POINTS).SetCur) - 1
		If ChannelOn(MEMORY_AREA) Then
			For WavePoint = 0 To UBound(WaveMagnitude, 2)
				TempWaveMagnitude(WavePoint) = WaveMagnitude(WhatTrace(MEMORY_AREA), WavePoint)
			Next WavePoint
			If UBound(WaveMagnitude, 2) < WaveSize Then
				For WavePoint = UBound(WaveMagnitude, 2) + 1 To WaveSize
					TempWaveMagnitude(WavePoint) = 0
				Next WavePoint
			End If
		End If
		
		ReDim WaveMagnitude(NumberOfTraces, WaveSize)
		ReDim WaveTime(WaveSize)
		ReDim Ch1TempWave(0, WaveSize)
		ReDim Ch2TempWave(0, WaveSize)
		ReDim MathTempWave(0, WaveSize)
		ReDim MemTempWave(0, WaveSize)
		
		If ChannelOn(MEMORY_AREA) Then
			For WavePoint = 0 To UBound(WaveMagnitude, 2)
				WaveMagnitude(WhatTrace(MEMORY_AREA), WavePoint) = TempWaveMagnitude(WavePoint)
			Next WavePoint
		End If
		frmZT1428.cmdAutoscale.Enabled = False
		
		'These two calls allow the user to capture a single event by clicking Measure
		'or execute a TIP single acquisition (TIP_RunSetup).
		WriteMsg("TER?") 'Query the Trigger Event Register
		Call ReadMsg() 'Discard any queued-up trigger
		
		' clear previous plots
		frmZT1428.cwgScopeDisplay.ClearData()
		
		
		Do 
			WaitForTrigger()
			
			NextChannel = 0
			PointMlt = Val(MainParam(WF_POINTS).SetCur) \ 500
			dHScale = Val(MainParam(HORZ_SENS).SetCur) * PointMlt
			DDelay = Val(MainParam(REF_DELAY).SetCur)
			AcqMode = frmZT1428.cboSampleType.SelectedIndex
			Select Case MainParam(H_REF_POS).SetCur
				Case H_LEFT
					dXOrig = DDelay
				Case H_CENTER
					dXOrig = -(dHScale / 2) + DDelay
				Case H_RIGHT
					dXOrig = -dHScale + DDelay
			End Select
			dXIncr = dHScale / Val(MainParam(WF_POINTS).SetCur)
			If dHScale >= 0.5 Then
				Digitizing = True
				''            WriteMsg "DIG CHAN1,CHAN2"
				''            WriteMsg "WAV:PRE?"
				''            PreambleBuffer$ = ReadMsg$()
				''            'ReadMsg$() returns 8100 charters, strip nulls and append a Line Feed to end of preamble
				''            PreambleBuffer$ = Trim(StripNullCharacters(PreambleBuffer$)) + vbLf
				Digitizing = False
			End If
			If ChannelOn(CHANNEL_1) Then
				dYIncr = Val(MainParam(IN1_VSENS).SetCur) / 65536
				dYOrig = -(Val(MainParam(IN1_VSENS).SetCur) / 2) + Val(MainParam(IN1_OFFSET).SetCur)
				'GetWaveform WaveMagnitude#(NextChannel%, 0), WaveTime!(0), _
				''         dYIncr, dYOrig, dXIncr, dXOrig, CHANNEL_1, NumberOfTraces%, AcqMode%
				
				'DoEvents was added before and after waveform acquisition in order to capture all commands sent to the SFP _
				'specifically in order to close the window.  DME PCR 510 #2.
				
				System.Windows.Forms.Application.DoEvents()
				GetWaveform(Ch1TempWave(0, 0), WaveTime(0), dYIncr, dYOrig, dXIncr, dXOrig, CHANNEL_1, NumberOfTraces, AcqMode)
				System.Windows.Forms.Application.DoEvents()
				
				' put data into temp array because plot cannot plot full range multi-Dim arrays
				For i = 0 To WaveSize
					WaveMagnitude(CHANNEL_1, i) = Ch1TempWave(0, i)
				Next i
				
				NextChannel = 1
				
				frmZT1428.cwgScopeDisplay.Plots.Item(NextChannel).PlotXvsY(WaveTime, Ch1TempWave)
				
				
			End If
			If ChannelOn(CHANNEL_2) Then
				dYIncr = Val(MainParam(IN2_VSENS).SetCur) / 65536
				dYOrig = -(Val(MainParam(IN2_VSENS).SetCur) / 2) + Val(MainParam(IN2_OFFSET).SetCur)
				'GetWaveform WaveMagnitude#(NextChannel%, 0), WaveTime!(0), _
				''         dYIncr, dYOrig, dXIncr, dXOrig, CHANNEL_2, NumberOfTraces%, AcqMode%
				'DoEvents was added before and after waveform acquisition in order to capture all commands sent to the SFP _
				'specifically in order to close the window.  DME PCR 510 #2.
				
				System.Windows.Forms.Application.DoEvents()
				GetWaveform(Ch2TempWave(0, 0), WaveTime(0), dYIncr, dYOrig, dXIncr, dXOrig, CHANNEL_2, NumberOfTraces, AcqMode)
				System.Windows.Forms.Application.DoEvents()
				
				' put data into temp array because plot cannot plot full range multi-Dim arrays
				For i = 0 To WaveSize
					WaveMagnitude(NextChannel, i) = Ch2TempWave(0, i)
				Next i
				NextChannel = NextChannel + 1
				frmZT1428.cwgScopeDisplay.Plots.Item(NextChannel).PlotXvsY(WaveTime, Ch2TempWave)
				
			End If
			If ChannelOn(MATH_AREA) Then
				Digitizing = True
				' WriteMsg "DIG CHAN1,CHAN2"
				SetMathFunction(False)
				' WriteMsg "WAV:SOUR FUNC1"
				' WriteMsg "VIEW FUNC1"
				' WriteMsg "WAV:PRE?"
				' PreambleBuffer$ = ReadMsg$()
				' 'ReadMsg$() returns 8100 charters, strip nulls and append a Line Feed to end of preamble
				' PreambleBuffer$ = Trim(StripNullCharacters(PreambleBuffer$)) + vbLf
				' WriteMsg "WAV:DATA?"
				' VPoints$ = ReadMsg$()
				'UnpackWaveform PreambleBuffer$, VPoints$, WaveMagnitude#, WaveTime!, NextChannel%
				' UnpackWaveform PreambleBuffer$, VPoints$, MathTempWave#, WaveTime!, 0
				' NOTE: the delay is needed so the reading of two is complete, otherwise errors out
				Delay(2)
				System.Windows.Forms.Application.DoEvents()
				GetWaveform(MathTempWave(0, 0), WaveTime(0), dYIncr, dYOrig, dXIncr, dXOrig, MATH_FUNC1, NumberOfTraces, AcqMode)
				System.Windows.Forms.Application.DoEvents()
				For i = 0 To WaveSize
					WaveMagnitude(MATH_AREA, i) = MathTempWave(0, i)
				Next i
				NextChannel = NextChannel + 1
				frmZT1428.cwgScopeDisplay.Plots.Item(NextChannel).PlotXvsY(WaveTime, MathTempWave)
				Digitizing = False
			End If
			'Enable display of WMEM1 waveform
			If ChannelOn(MEMORY_AREA) Then
				' WriteMsg "DIG CHAN1,CHAN2"
				' WriteMsg "WAV:SOUR WMEM1"
				' WriteMsg "VIEW WMEM1"
				' WriteMsg "WAV:PRE?"
				' PreambleBuffer$ = ReadMsg$()
				'ReadMsg$() returns 8100 charters, strip nulls and append a Line Feed to end of preamble
				' PreambleBuffer$ = Trim(StripNullCharacters(PreambleBuffer$)) + vbLf
				' WriteMsg "WAV:DATA?"
				' VPoints$ = ReadMsg$()
				' UnpackWaveform PreambleBuffer$, VPoints$, WaveMagnitude#, WaveTime!, NextChannel%
				System.Windows.Forms.Application.DoEvents()
				GetWaveform(MemTempWave(0, 0), WaveTime(0), dYIncr, dYOrig, dXIncr, dXOrig, MEM_WMEM1, NumberOfTraces, AcqMode)
				System.Windows.Forms.Application.DoEvents()
				For i = 0 To WaveSize
					WaveMagnitude(NextChannel, i) = MemTempWave(0, i)
				Next i
				NextChannel = NextChannel + 1
				frmZT1428.cwgScopeDisplay.Plots.Item(NextChannel).PlotXvsY(WaveTime, MemTempWave)
			End If
			' set the cursors position and update the caption
			frmZT1428.cwgScopeDisplay.Cursors.Item(1).SetPosition(0, 0)
			UpdateCursorCaption(System.Math.Abs(frmZT1428.cwgScopeDisplay.Cursors.Item(1).XPosition), 0)
			
			
			If InStr(1, sTIP_Mode, "TIP_RUN") = 0 Or InStr(1, sTIP_Mode, "TIP_MEASONLY") = 0 Then 'If TIP run mode then, CMD string configures instrument
				If CurrentMeas = DELAY_MEAS Then
					WriteMsg("DIG CHAN1,CHAN2")
					WriteMsg("MEAS:SOUR " & StartSource & "," & StopSource)
					WriteMsg("MEAS:MODE USER")
					WriteMsg("MEAS:UNIT " & DelayLevelUnit)
					If Val(StartLevel.SetCur) < Val(StopLevel.SetCur) Then
						WriteMsg("MEAS:LOW " & StartLevel.SetCur)
						WriteMsg("MEAS:UPP " & StopLevel.SetCur)
						WriteMsg("MEAS:DEF DEL," & StartSlope & "," & StartEdge.SetCur & ",LOW," & StopSlope & "," & StopEdge.SetCur & ",UPP")
					Else
						WriteMsg("MEAS:LOW " & StopLevel.SetCur)
						WriteMsg("MEAS:UPP " & StartLevel.SetCur)
						WriteMsg("MEAS:DEF DEL," & StartSlope & "," & StartEdge.SetCur & ",UPP," & StopSlope & "," & StopEdge.SetCur & ",LOW")
					End If
				Else
					If (MainParam(MEAS_SOUR).SetCur <> "WMEM1") And (MainParam(MEAS_SOUR).SetCur <> "FUNC1") Then
						'' ZTEC?              WriteMsg "DIG CHAN1,CHAN2"
					End If
					'' ZTEC?          WriteMsg MainParam(MEAS_SOUR).SetCod + MainParam(MEAS_SOUR).SetCur
				End If
			Else
				If (MainParam(MEAS_SOUR).SetCur <> "WMEM1") And (MainParam(MEAS_SOUR).SetCur <> "FUNC1") Then
					''                WriteMsg "DIG CHAN1,CHAN2"
				End If
				If bTIP_UserMeas Then 'Now send DELAY measurement parameters for TIPs Measurement
					WriteMsg(sDelayMeasStr)
				Else
					''              WriteMsg MainParam(MEAS_SOUR).SetCod + MainParam(MEAS_SOUR).SetCur
				End If
			End If
			If CurrentMeas <> MEAS_OFF Then
				'Query instrument for measured value
				dCurrentMeasurement = Val(sMeasQuery)
			End If
			
			'----------- JHill TDR99153 <begin> ------------------------
			If CurrentMeas = PRE_SHOOT Or CurrentMeas = OVR_SHOOT Then
				If dCurrentMeasurement > 9.9E+31 Then
					frmZT1428.txtDataDisplay.Text = ""
				Else
					dCurrentMeasurement = dCurrentMeasurement * 100 'Convert to percentage
					frmZT1428.txtDataDisplay.Text = EngNotate(dCurrentMeasurement, MeasureCode(CurrentMeas))
				End If
			ElseIf CurrentMeas <> MEAS_OFF Then 
				frmZT1428.txtDataDisplay.Text = EngNotate(dCurrentMeasurement, MeasureCode(CurrentMeas))
			Else
				frmZT1428.txtDataDisplay.Text = ""
			End If
			'----------- JHill TDR99153 <end> ------------------------
			If CurrentMeas <> MEAS_OFF And (InStr(1, sTIP_Mode, "TIP_RUN") > 0 Or InStr(1, sTIP_Mode, "TIP_MEAS") > 0) Then
				If MeasureCode(CurrentMeas).SetUOM = "S" Or MeasureCode(CurrentMeas).SetUOM = "s" Then
					sTIP_Measured = VB6.Format(dCurrentMeasurement, "0.############")
				Else
					sTIP_Measured = VB6.Format(dCurrentMeasurement, "0.0#####")
				End If
			End If
			
			System.Windows.Forms.Application.DoEvents()
			
			If WaveSize + 1 <> Val(MainParam(WF_POINTS).SetCur) Then
				WaveSize = Val(MainParam(WF_POINTS).SetCur) - 1
				ReDim WaveMagnitude(NumberOfTraces, WaveSize)
				ReDim WaveTime(WaveSize)
			End If
			'mh UpdateCursorCaption frmZT1428.cwgScopeDisplay.Cursors(1).XPosition, frmZT1428.cwgScopeDisplay.Cursors(1).YPosition
			If bTIP_Running Then SetKey("TIPS", "DScope_Measure", CStr(dCurrentMeasurement))
			
		Loop Until (frmZT1428.chkContinuous._Value = False)
		
		
		
		
		WriteMsg("STOP")
		MakingMeasurement = False
		frmZT1428.cmdMeasure.Enabled = True
		frmZT1428.cmdAutoscale.Enabled = True
		frmZT1428.fraDisplay.Enabled = True
		''    frmZT1428.fraAcqMode.Enabled = True   No longer needed JDH
		''    frmZT1428.cboAcqMode.Enabled = True
		frmZT1428.fraSampleType.Enabled = True
		frmZT1428.cboSampleType.Enabled = True
		frmZT1428.fraAvgCount.Enabled = True
		
		frmZT1428.chkDisplayTrace(CHANNEL_1).Enabled = True
		frmZT1428.chkDisplayTrace(CHANNEL_2).Enabled = True
		If Val(MainParam(WF_POINTS).SetCur) <> 8000 Then
			frmZT1428.chkDisplayTrace(MATH_AREA).Enabled = True
		End If
		If frmZT1428.cboSampleType.SelectedIndex = REALTIME Then
			frmZT1428.optDataPoints(0).Enabled = True
			frmZT1428.optDataPoints(1).Enabled = True
			frmZT1428.fraDataPoints.Enabled = True
		End If
		
		'EADS Added code to allow SFP to remain open until app is closed while
		'running in SINGLE Mode
		If sTIP_Mode = "TIP_RUNSETUP" Then
			Do 
				System.Windows.Forms.Application.DoEvents()
				'do nothing until app is closed.
			Loop Until sTIP_Mode <> "TIP_RUNSETUP"
		ElseIf sTIP_Mode = "TIP_MEASONLY" Then 
			QuitProgram = True
			EndProgram()
		End If
		
		WriteMsg("MEM:VME:STAT OFF") 'Set VME State OFF after measurement is complete. 'DJoiner  01/14/02
		frmZT1428.chkDisplayTrace(MEMORY_AREA).Enabled = True
		ContinuousFlag = False
		
		'Exit when Continuous is clicked off and running TIP Persist mode
		If sTIP_Mode = "TIP_RUNPERSIST" Then EndProgram()
		
	End Sub
	
	
	Sub FillListBoxes()
		'Declare Procedure Level variables
		Dim ScaleLoop As Short
		Dim IndexLoop As Short
		Dim InitLoop As Short
		Dim PointMlt As Short
		
		frmZT1428.cboHorizRange.Items.Clear()
		For ScaleLoop = 0 To 29
			frmZT1428.cboHorizRange.Items.Insert(ScaleLoop, ListBoxFiller(HORZ_SENS, ScaleLoop))
		Next ScaleLoop
		frmZT1428.cboHorizRange.SelectedIndex = HScaleListItem
		
		'Trigger/Delay Sources
		frmZT1428.cboTrigSour.Items.Clear()
		frmZT1428.cboTrigDelaySour.Items.Clear()
		For ScaleLoop = 0 To 4
			frmZT1428.cboTrigSour.Items.Insert(ScaleLoop, ListBoxFiller(TRIG_SOUR, ScaleLoop))
			frmZT1428.cboTrigDelaySour.Items.Insert(ScaleLoop, ListBoxFiller(TRIG_SOUR, ScaleLoop))
		Next ScaleLoop
		frmZT1428.cboTrigSour.SelectedIndex = 0
		frmZT1428.cboTrigDelaySour.SelectedIndex = 0
		
		'Measurment Sources
		frmZT1428.cboMeasSour.Items.Clear()
		frmZT1428.cboStartSource.Items.Clear()
		frmZT1428.cboStopSource.Items.Clear()
		For ScaleLoop = 0 To 3
			frmZT1428.cboMeasSour.Items.Insert(ScaleLoop, ListBoxFiller(MEAS_SOUR, ScaleLoop))
			frmZT1428.cboStartSource.Items.Insert(ScaleLoop, ListBoxFiller(MEAS_SOUR, ScaleLoop))
			frmZT1428.cboStopSource.Items.Insert(ScaleLoop, ListBoxFiller(MEAS_SOUR, ScaleLoop))
		Next ScaleLoop
		frmZT1428.cboMeasSour.SelectedIndex = 0
		frmZT1428.cboStartSource.SelectedIndex = 0
		frmZT1428.cboStopSource.SelectedIndex = 0
		
		'Aquisition Mode
		frmZT1428.cboAcqMode.Items.Clear()
		frmZT1428.cboAcqMode.Items.Insert(0, "Automatic")
		frmZT1428.cboAcqMode.Items.Insert(1, "Triggered")
		frmZT1428.cboAcqMode.SelectedIndex = 0
		
		'Fill sample list With Specified Number of array elements necessary to Instrument Function
		frmZT1428.cboSampleType.Items.Clear()
		frmZT1428.cboSampleType.Items.Insert(0, "Real Time")
		frmZT1428.cboSampleType.Items.Insert(1, "Repetitive")
		frmZT1428.cboSampleType.SelectedIndex = 0
		
		'Trace Color
		frmZT1428.cboTraceColor.Items.Clear()
		frmZT1428.cboTraceColor.Items.Insert(0, "Channel 1")
		frmZT1428.cboTraceColor.Items.Insert(1, "Channel 2")
		frmZT1428.cboTraceColor.Items.Insert(2, "Math")
		frmZT1428.cboTraceColor.Items.Insert(3, "Memory")
		frmZT1428.cboTraceColor.SelectedIndex = 0
		
		'Trace thickness
		frmZT1428.cboTraceThickness.Items.Clear()
		frmZT1428.cboTraceThickness.Items.Insert(0, "Single Width")
		frmZT1428.cboTraceThickness.Items.Insert(1, "Double Width")
		frmZT1428.cboTraceThickness.Items.Insert(2, "Triple Width")
		frmZT1428.cboTraceThickness.SelectedIndex = 0
		
		'Set Memory/Math List Boxes to Default condition
		frmZT1428.cboMathFunction.SelectedIndex = 0
		frmZT1428.cboMathSourceA.SelectedIndex = 0
		frmZT1428.cboMathSourceB.SelectedIndex = 0
		
	End Sub
	
	
	Sub FillVSense(ByRef Chan As Short, ByRef AttIndex As Short)
		'Declare Procedure Level variables
		Dim ChanIndex As Short
		Dim VPerDiv As Single
		Dim DummyParam As InstParameter
		Dim AttenuationFactor As Short
		Dim RangeSetting As Short
		Dim NewItem As String
		
		DummyParam.SetUOM = "V/Div"
		DummyParam.SetRes = "D0"
		If AttIndex = -1 Then
			If Chan = CHANNEL_1 Then
				ChanIndex = IN1_VSENS
				AttenuationFactor = Val(MainParam(IN1_ATTN_FAC).SetCur)
				iAttenCH1 = AttenuationFactor
			Else
				ChanIndex = IN2_VSENS
				AttenuationFactor = Val(MainParam(IN2_ATTN_FAC).SetCur)
				iAttenCH2 = AttenuationFactor
			End If
		Else
			If Chan = CHANNEL_1 Then
				ChanIndex = IN1_VSENS
				AttenuationFactor = Val(ParamCode(IN1_ATTN_FAC, AttIndex))
				iAttenCH1 = AttenuationFactor
			Else
				ChanIndex = IN2_VSENS
				AttenuationFactor = Val(ParamCode(IN2_ATTN_FAC, AttIndex))
				iAttenCH2 = AttenuationFactor
			End If
		End If
		
		frmZT1428.cboVertRange(Chan).Items.Clear()
		For RangeSetting = 0 To 12
			'        VPerDiv! = CDbl(VSenseBase!(RangeSetting%) * AttenuationFactor%)
			VPerDiv = VSenseBase(RangeSetting) * AttenuationFactor
			ParamCode(ChanIndex, RangeSetting) = VB6.Format(VPerDiv * (VERT_DIV))
			If ParamCode(ChanIndex, RangeSetting) = "9.999999E-02" Then
				ParamCode(ChanIndex, RangeSetting) = "0.1"
			ElseIf Val(ParamCode(ChanIndex, RangeSetting)) < 0.008 Then 
				ParamCode(ChanIndex, RangeSetting) = "0.008"
			End If
			NewItem = EngNotate(CDbl(VB6.Format(VPerDiv)), DummyParam) 'Fix result of TDR99113
			frmZT1428.cboVertRange(Chan).Items.Insert(RangeSetting, NewItem)
		Next RangeSetting
		frmZT1428.cboVertRange(Chan).SelectedIndex = VTraceListItem
		
	End Sub
	
	
	Sub FillTextBoxes()
		'DESCRIPTION:
		'   Fill all text boxes to their current state
		
		frmZT1428.txtStartEdge.Text = EngNotate(Val(StartEdge.SetCur), StartEdge)
		frmZT1428.txtStopEdge.Text = EngNotate(Val(StopEdge.SetCur), StopEdge)
		frmZT1428.txtStartLevel.Text = EngNotate(Val(StartLevel.SetCur), StartLevel)
		frmZT1428.txtStopLevel.Text = EngNotate(Val(StopLevel.SetCur), StopLevel)
		frmZT1428.txtDelay.Text = EngNotate(Val(MainParam(REF_DELAY).SetCur), MainParam(REF_DELAY))
		frmZT1428.txtHoldOff.Text = EngNotate(Val(MainParam(HOLD_OFF).SetCur), MainParam(HOLD_OFF))
		frmZT1428.txtAvgCount.Text = EngNotate(Val(MainParam(AVR_COUNT).SetCur), MainParam(AVR_COUNT))
		frmZT1428.txtCompletion.Text = EngNotate(Val(MainParam(COMPLETION).SetCur), MainParam(COMPLETION))
		frmZT1428.txtVOffset1.Text = EngNotate(Val(MainParam(IN1_OFFSET).SetCur), MainParam(IN1_OFFSET))
		frmZT1428.txtVOffset2.Text = EngNotate(Val(MainParam(IN2_OFFSET).SetCur), MainParam(IN2_OFFSET))
		frmZT1428.txtTrigLevel.Text = EngNotate(Val(MainParam(TRIG_LEV).SetCur), MainParam(TRIG_LEV))
		frmZT1428.txtDelEvents.Text = EngNotate(Val(MainParam(TRIG_DEL).SetCur), MainParam(TRIG_DEL))
		frmZT1428.txtLine.Text = EngNotate(Val(MainParam(TRIG_LINE).SetCur), MainParam(TRIG_LINE))
		frmZT1428.txtOccurrence.Text = EngNotate(Val(MainParam(TRIG_OCC).SetCur), MainParam(TRIG_OCC))
		frmZT1428.txtMathRange.Text = EngNotate(Val(MainParam(FUNC_RANGE).SetCur), MainParam(FUNC_RANGE))
		frmZT1428.txtMathVOffset.Text = EngNotate(Val(MainParam(FUNC_OFFSET).SetCur), MainParam(FUNC_OFFSET))
		frmZT1428.txtMemoryRange.Text = EngNotate(Val(MainParam(MEM_RANGE).SetCur), MainParam(MEM_RANGE))
		frmZT1428.txtMemVoltOffset.Text = EngNotate(Val(MainParam(MEM_OFFSET).SetCur), MainParam(MEM_OFFSET))
		
	End Sub
	
	
	Sub LoadDiskToMemory()
		
		'---Added as a result of TDR98235 for Version 1.6---
		'Waveform file compatibility with ATLAS
		
		Dim FileId As Integer 'File Handle of Waveform Data File
		Dim TmpName As String 'Return Infomation for Common Dailog Control
		Dim FileSize As Integer '1)Actual Length of File, then 2) Length of Waveform Data
		Dim VMAX As Double 'Maximum Voltage Point (Used For Scaling)
		Dim DataPoints As String 'Waveform Data to Upload to Digitizing Oscilloscope
		Dim Element As Integer 'Index for Waveform Data Points
		Dim Wave() As Double 'Waveform Data Array
		Dim PreambleBuffer As String 'Preamble Data to Upload to Digitizing Oscilloscope
		Dim PreList(10) As String 'Preamble Data Array
		Dim Items As Short 'Number of Elements In the Preamble Array
		Dim dYOrg As Double 'Waveform Preamble Data Y-AXIS Origin
		Dim dYInc As Double 'Waveform Preamble Data Y-AXIS Increment
		Dim iXRef As Short 'Waveform Preamble Data X-AXIS Reference
		Dim dXOrg As Double 'Waveform Preamble Data X-AXIS Origin
		Dim dXInc As Double 'Waveform Preamble Data X-AXIS Increment
		Dim iYRef As Short 'Waveform Preamble Data Y-AXIS Reference
		Dim Points As Integer 'Waveform Preamble Data Number of Data Points
		Dim ResMode As Short 'Waveform Preamble Data 8/16 Bit Point Resolution
		Dim ScopeByte As Integer 'Converted DAC Level of a Data Point
		Dim ScopeByteH As Integer 'High Byte Converted DAC Level of a Data Point
		Dim WaveTime As Single 'Calculated For Setting Time/DIV Scale Setting
		Dim RangeIndex As Short 'Index of TIME/DIV Settings
		Dim HeaderBlockCode As Integer 'Waveform File Header Block
		Dim AsciiChar As Byte '8-Bit ASCII Character Code
		Dim PreambleIndex As Short 'Index of Preamble Characters In a Waveform Data File
		Dim PreambleSize As String 'Number of Characters in the preamble
		Dim PList As Short 'COMP Parameter Index
		
		If Not bTIP_Running Or sTIP_Mode = "TIP_DESIGN" Then
			'Show dialog box if in TIP design (and no file has been saved) or not running from TIP
			If sSegFileName <> "" Then
				frmZT1428.dlgSegmentDataOpen.FileName = sSegFileName
				frmZT1428.dlgSegmentDataSave.FileName = sSegFileName
			Else
				frmZT1428.dlgSegmentDataOpen.FileName = "*.seg"
				frmZT1428.dlgSegmentDataSave.FileName = "*.seg"
			End If
			On Error Resume Next
			frmZT1428.dlgSegmentDataOpen.ShowDialog()
			frmZT1428.dlgSegmentDataSave.FileName = frmZT1428.dlgSegmentDataOpen.FileName
			'UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			If Err.Number = DialogResult.Cancel Or frmZT1428.dlgSegmentDataOpen.FileName = "" Or frmZT1428.dlgSegmentDataOpen.FileName = "*.seg" Then
				sSegFileName = ""
				Exit Sub
			End If
			On Error GoTo 0
			TmpName = Extract((frmZT1428.dlgSegmentDataOpen.FileName), "FILENAME")
			TmpName = UCase(TmpName)
			
			'Check for Valid Filename
			If Len(TmpName) > 12 Then
				MsgBox("The segment name cannot be more than 12 characters.  Truncating...", MsgBoxStyle.Critical, "SAIS")
				TmpName = Left(TmpName, 12)
			ElseIf IsNumeric(Left(TmpName, 1)) Then 
				MsgBox("The segment name cannot start with a number.  Please re-name.", MsgBoxStyle.Critical, "SAIS")
				Exit Sub
			End If
			If Not FileExists((frmZT1428.dlgSegmentDataOpen.FileName)) Then
				MsgBox("Cannot find file " & frmZT1428.dlgSegmentDataOpen.FileName & ".  Please re-enter.", MsgBoxStyle.Critical, "SAIS")
				Exit Sub
			End If
			'Now that file validity is verified, make TIP segment file = input file
			If sTIP_Mode = "TIP_DESIGN" Then sSegFileName = TmpName
			FileSize = FileLen(frmZT1428.dlgSegmentDataOpen.FileName)
		Else
			On Error GoTo TIP_ErrorHandle
			FileSize = FileLen(sSegFileName)
		End If
		
		'Check for Valid File Size and Format
		If FileSize > 65000 Then
			'File is too large to be even considered for loading
			MsgBox("The selected waveform data file is too large. The waveform data file must contain 500 or 8,000 data points.", MsgBoxStyle.Critical, "SAIS")
			Exit Sub
		End If
		If FileSize Mod 256 <> 0 Then
			'File is not in a 256 Byte Block Format
			MsgBox("The selected waveform data file has an invalid format.", MsgBoxStyle.Critical, "Invalid Block Format")
			Exit Sub
		End If
		
		FileId = FreeFile
		If InStr(1, sTIP_Mode, "TIP_RUN") = 0 Or InStr(1, sTIP_Mode, "TIP_MEAS") = 0 Then
			FileOpen(FileId, frmZT1428.dlgSegmentDataOpen.FileName, OpenMode.Binary, OpenAccess.Read)
		Else
			FileOpen(FileId, sSegFileName, OpenMode.Binary, OpenAccess.Read)
		End If
		
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(FileId, HeaderBlockCode, 1)
		If HeaderBlockCode <> &HABCD4567 Then
			'File is not in a TYX-ATLAS  Format
			MsgBox("The selected waveform data file has an invalid format.", MsgBoxStyle.Critical, "Invalid Header Bytes")
			FileClose(FileId)
			Exit Sub
		End If
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(FileId, FileSize, 5) 'Read Waveform Length in header block
		If FileSize < 4000 Then
			MsgBox("The selected data file is smaller than 500 pionts.  Padding will be added to the waveform to adjust it to memory size.", MsgBoxStyle.Information, "SAIS")
			FileSize = 4000
		ElseIf FileSize > 4000 And FileSize < 64000 Then 
			MsgBox("The selected data file is less than 8000 points and greater than 500 points.  Padding will be added to adjust the waveform to 8000 points.", MsgBoxStyle.Information, "SAIS")
			FileSize = 64000
		ElseIf FileSize > 64000 Then 
			MsgBox("The selected data file is greater than 8000 points.  The waveform data will be truncated to 8000 points.", MsgBoxStyle.Information, "SAIS")
			FileSize = 64000
		End If
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(FileId, AsciiChar, 9)
		If AsciiChar <> Asc("#") Then
			'File is not in a 256 Byte Block Format (NO Preamble # Character)
			MsgBox("The selected waveform data file has an invalid format.", MsgBoxStyle.Critical, "Invalid Preamble Data")
			FileClose(FileId)
			Exit Sub
		End If
		
		'Get Preamble Data
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(FileId, AsciiChar, 10) 'Number of Description Characters
		For PreambleIndex = 1 To CShort(Chr(AsciiChar))
			'Get the Number of Preamble Characters
			'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(FileId, AsciiChar, 10 + PreambleIndex)
			PreambleSize = PreambleSize & Chr(AsciiChar)
		Next PreambleIndex
		For PreambleIndex = 1 To CShort(PreambleSize)
			'Get the Preamble Data Into A String
			'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(FileId, AsciiChar)
			PreambleBuffer = PreambleBuffer & Chr(AsciiChar)
		Next PreambleIndex
		
		'Get Waveform Data
		ReDim Preserve Wave(FileSize / 8)
		'UPGRADE_WARNING: Get was upgraded to FileGet and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(FileId, Wave, 257)
		
		'Close File Handle
		FileClose(FileId)
		
		'------------------------------------------------------
		'(Re)Format Data For the Digitizing Oscilloscope Memory
		'------------------------------------------------------
		frmZT1428.chkContinuous.Value = False 'Stop All Data Acquisition
		
		'Format Preamble Data
		Items = StringToList(PreambleBuffer, PreList, ",")
		ResMode = CShort(PreList(1))
		Points = CInt(PreList(3))
		dXInc = CDbl(PreList(5))
		dXOrg = CDbl(PreList(6))
		iXRef = CShort(PreList(7))
		dYInc = CDbl(PreList(8))
		dYOrg = CDbl(PreList(9))
		iYRef = CShort(StripNullCharacters(PreList(10)))
		
		If Points = 8000 Then
			frmZT1428.optDataPoints(1).Value = True
		Else
			frmZT1428.optDataPoints(0).Value = True
		End If
		
		'Format Waveform Data
		
		'If 16-bit(Mode-2) points*2
		If ResMode = 2 Then '16-Bit Resolution
			DataPoints = "#" & "8" & VB6.Format(Str(Points * 2), "00000000")
		Else '8-Bit Resolution
			DataPoints = "#" & "8" & VB6.Format(Str(Points), "00000000")
		End If
		For Element = 1 To Points
			'Get Maximum Voltage Point
			If (Wave(Element)) > VMAX Then
				VMAX = System.Math.Abs(Wave(Element)) 'Get Largest Absolute Voltage Value
			End If
			'Trap Invalid Data
			If Int(((Wave(Element) - dYOrg) / dYInc) + iYRef) > 32768 Then
				MsgBox("The selected waveform data file has an invalid format.", MsgBoxStyle.Critical, "SAIS")
				Exit Sub
			End If
			
			'Convert Format To String and Scope DAC Codes
			ScopeByte = Int(((Wave(Element) - dYOrg) / dYInc) + iYRef)
			If ScopeByte > 32767 Then ScopeByte = 32767
			
			If ResMode = 2 Then '16-Bit Resolution
				ScopeByteH = CShort(ScopeByte And &HFF00s) / 256
				ScopeByte = ScopeByte And &HFFs
				DataPoints = DataPoints & Chr(ScopeByteH) & Chr(ScopeByte)
			Else
				ScopeByte = ((Wave(Element) - dYOrg) / dYInc) + iYRef
				DataPoints = DataPoints & Chr(ScopeByte)
			End If
		Next Element
		
		'Reset To Correct Timebase and Lock until memory is deselected
		WaveTime = ((Points - iXRef) * dXInc) + dXOrg
		If Val(MainParam(WF_POINTS).SetCur) = 8000 Then
			PList = PNT8000_HLIST
		Else
			PList = HORZ_SENS
		End If
		For RangeIndex = 1 To 29
			If CDbl(ParamCode(PList, RangeIndex)) < WaveTime Then
				Exit For
			End If
		Next RangeIndex
		frmZT1428.cboHorizRange.SelectedIndex = RangeIndex - 1
		
		'Select Ideal Voltage/Division For Waveform and set
		VMAX = ((VMAX - iYRef) * dYInc) + dYOrg
		If VMAX < 0 Then
			VMAX = VMAX * (-1)
		End If
		
		MEMRangeSetting = VMAX / 5
		MEMOffsetSetting = 0
		MainParam(MEM_OFFSET).SetCur = CStr(0)
		MainParam(MEM_RANGE).SetCur = Str(MEMRangeSetting)
		MainParam(MEM_OFFSET).SetCur = Str(MEMOffsetSetting)
		frmZT1428.txtMemoryRange.Text = EngNotate(MEMRangeSetting, MainParam(MEM_RANGE))
		frmZT1428.txtMemVoltOffset.Text = EngNotate(MEMOffsetSetting, MainParam(MEM_OFFSET))
		SpinParam(MEM_RANGE, "UP")
		
		'Write Preamble Information
		WriteMsg("WAV:SOUR WMEM1")
		WriteMsg("WAV:PREAMBLE " & PreambleBuffer)
		'Upload Waveform to Instrument
		WriteMsg("WAV:DATA " & DataPoints)
		If ChannelOn(MEMORY_AREA) Then
			SelectTraces(MEMORY_AREA, True)
		End If
		
		'Update User Display
		If InStr(1, sTIP_Mode, "TIP_RUN") = 0 Or InStr(1, sTIP_Mode, "TIP_MEAS") = 0 Then
			DigitizeWaveform() 'Don't enter DigitizeWaveform yet if TIP Run mode
		End If
		DiskWaveformFlag = True
		
		'---End Modification for TDR98235 for Version 1.6---
		Exit Sub
		
TIP_ErrorHandle: 
		MsgBox("Error: " & Err.Description, MsgBoxStyle.Exclamation, "File Error")
		
	End Sub
	
	
	Sub RangeUpdate(ByRef ParamIndex As Short)
		
		Select Case ParamIndex
			Case START_EDGE
				StatusBarMessage(ShowVals(StartEdge))
			Case START_LEVEL
				StatusBarMessage(ShowVals(StartLevel))
			Case STOP_EDGE
				StatusBarMessage(ShowVals(StopEdge))
			Case STOP_LEVEL
				StatusBarMessage(ShowVals(StopLevel))
			Case Else
				StatusBarMessage(ShowVals(MainParam(ParamIndex)))
		End Select
		
	End Sub
	
	
	Sub RegisterChange(ByRef ParamIndex As Short)
		'Declare Procedure Level variables
		Dim VRAnge As Single
		Dim Voffset As Single
		
		Select Case ParamIndex
			Case START_EDGE, START_LEVEL, STOP_EDGE, STOP_LEVEL
				Exit Sub
			Case HOLD_OFF
				WriteMsg(MainParam(HOLD_OFF).SetCod & HoldType & MainParam(HOLD_OFF).SetCur)
				Exit Sub
			Case TRIG_DEL
				WriteMsg(MainParam(TRIG_DEL).SetCod & DelayType & MainParam(TRIG_DEL).SetCur)
				Exit Sub
			Case FUNC_RANGE, FUNC_OFFSET
				VRAnge = Val(MainParam(FUNC_RANGE).SetCur) * 10
				Voffset = Val(MainParam(FUNC_OFFSET).SetCur)
				frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(MATH_AREA)).Maximum = (VRAnge / 2!) + Voffset
				frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(MATH_AREA)).Minimum = (-VRAnge / 2!) + Voffset
				Exit Sub
			Case MEM_RANGE, MEM_OFFSET
				VRAnge = Val(MainParam(MEM_RANGE).SetCur)
				Voffset = Val(MainParam(MEM_OFFSET).SetCur)
				frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(MEMORY_AREA)).Maximum = (VRAnge * 5) + Voffset
				frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(MEMORY_AREA)).Minimum = (-VRAnge * 5) + Voffset
				Exit Sub
		End Select
		WriteMsg(MainParam(ParamIndex).SetCod & MainParam(ParamIndex).SetCur)
		Select Case ParamIndex
			Case AVR_COUNT
				WriteMsg(Trim(MainParam(AVR_COUNT).SetCod) & "?")
				MainParam(AVR_COUNT).SetCur = StripNullCharacters(ReadMsg())
				'UPGRADE_WARNING: Couldn't resolve default property of object MainParam(AVR_COUNT).TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				MainParam(AVR_COUNT).TxtBox.Text = EngNotate(Val(MainParam(AVR_COUNT).SetCur), MainParam(AVR_COUNT))
			Case IN1_OFFSET
				SetVScaleGraph(CHANNEL_1)
				SetTrigRange(CHANNEL_1)
			Case IN2_OFFSET
				SetVScaleGraph(CHANNEL_2)
				SetTrigRange(CHANNEL_2)
			Case REF_DELAY
				SetHScaleGraph()
		End Select
		
	End Sub
	
	
	Sub RegisterEntry(ByRef ParamIndex As Short)
		
		Select Case ParamIndex
			Case START_EDGE
				FormatEntry(StartEdge)
			Case START_LEVEL
				FormatEntry(StartLevel)
			Case STOP_EDGE
				FormatEntry(StopEdge)
			Case STOP_LEVEL
				FormatEntry(StopLevel)
			Case Else
				FormatEntry(MainParam(ParamIndex))
		End Select
		
		RegisterChange(ParamIndex)
		
	End Sub
	
	
	Function RegisterKeyPress(ByRef KeyPress As Short, ByRef ParamIndex As Short) As String
		
		Select Case ParamIndex
			Case START_EDGE
				'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				RegisterKeyPress = ProcessKeyPress(KeyPress, StartEdge)
			Case START_LEVEL
				'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				RegisterKeyPress = ProcessKeyPress(KeyPress, StartLevel)
			Case STOP_EDGE
				'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				RegisterKeyPress = ProcessKeyPress(KeyPress, StopEdge)
			Case STOP_LEVEL
				'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				RegisterKeyPress = ProcessKeyPress(KeyPress, StopLevel)
			Case Else
				'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				RegisterKeyPress = ProcessKeyPress(KeyPress, MainParam(ParamIndex))
		End Select
		
	End Function
	
	
	Sub ResetScope()
		'Declare Procedure Level variables
		Dim temp As Integer
		Dim VMEAddress As Integer
		
		'mh SoftReset
		frmZT1428.chkContinuous.Value = False
		frmZT1428.tabMainFunctions.SelectedIndex = 0
		System.Windows.Forms.Application.DoEvents()
		
		' WMT Address issue --> viClear instrumentHandle&
		WriteMsg("MEM:VME:STATE ON")
		WriteMsg("MEM:VME:STATE OFF")
		GetMemorySpace(temp)
		VMEAddress = temp
		
		'Delay to wait for reset to complete
		Delay(0.5)
		
		WriteMsg("MEM:VME:ADDR #H" & Trim(Hex(VMEAddress)))
		WriteMsg("*CLS")
		WriteMsg("*RST")
		bInstrumentMode = True
		'mh WriteMsg "MEM:VME:STATE OFF"
		'  GetMemorySpace temp&
		' VMEAddress& = temp&
		
		'Delay to wait for reset to complete
		' Delay 0.5
		
		' WriteMsg "MEM:VME:ADDR #H" + Trim$(Hex$(VMEAddress&))
		
		InitializeArrays()
		FillListBoxes()
		' WriteMsg "MEM:VME:STATE ON"             'Added to force State to ON allowing
		' WriteMsg "MEM:VME:STATE OFF"            'the State to be set to OFF    'DJoiner 01/15/02
		FillTextBoxes()
		
		frmZT1428.tbrMeasFunction.Buttons(20).Value = 1
		frmZT1428.optStartSlope(0).Value = True
		frmZT1428.optStartLevel(0).Value = True
		frmZT1428.optStopSlope(0).Value = True
		frmZT1428.optReference(0).Value = True
		frmZT1428.optHoldOff(0).Value = True
		frmZT1428.optDataPoints(0).Value = True
		frmZT1428.optProbe1(0).Value = True
		frmZT1428.optCoupling1(0).Value = True
		frmZT1428.optProbe2(0).Value = True
		frmZT1428.optCoupling2(0).Value = True
		frmZT1428.optTrigSlope(0).Value = True
		frmZT1428.optNoiseRej(0).Value = True
		frmZT1428.optTrigMode(0).Value = True
		frmZT1428.optTrigDelay(0).Value = True
		frmZT1428.optDelaySlope(0).Value = True
		frmZT1428.optStandard(0).Value = True
		frmZT1428.optField(0).Value = True
		frmZT1428.optExtTrigCoup(0).Value = True
		
		frmZT1428.chkDisplayTrace(0).Value = True
		frmZT1428.chkDisplayTrace(1).Value = False
		frmZT1428.chkDisplayTrace(2).Value = False
		frmZT1428.chkDisplayTrace(3).Value = False
		
		frmZT1428.chkHFReject1.Value = False
		frmZT1428.chkLFReject1.Value = False
		frmZT1428.chkHFReject2.Value = False
		frmZT1428.chkLFReject2.Value = False
		
		frmZT1428.chkLFReject1.Enabled = False 'JHill TDR99151
		frmZT1428.chkLFReject2.Enabled = False 'JHill TDR99151
		
		frmZT1428.chkExternalOutput.Value = False
		frmZT1428.chkECL0.Value = False
		frmZT1428.chkECL1.Value = False
		
		frmZT1428.cboMeasSour.Visible = True
		frmZT1428.fraDelayParam.Visible = False
		
		frmZT1428.txtDataDisplay.Text = "Ready..."
		frmZT1428.ribCompOFF.Value = True
		frmZT1428.imgProbeComp.Visible = False
		frmZT1428.cwgScopeDisplay.ClearData()
		frmZT1428.cwgScopeDisplay.Cursors.Item(1).XPosition = 0
		
		bInstrumentMode = False
		
	End Sub
	
	
	Sub RunSelfTest()
		'Declare Procedure Level variables
		Dim ReturnCode As Short
		Dim Message As String
		Dim ErrorMessage As String
		Dim BlinkRate As Single
		Dim BlinkTime As Single
		Dim StartTime As Single
		Dim CaptionOn As Short
		
		
		If Not LiveMode Then
			MsgBox("Self Test is not available.  Live mode disabled.", MsgBoxStyle.Information)
			Exit Sub
		End If
		frmZT1428.chkExternalOutput.Value = False
		System.Windows.Forms.Application.DoEvents()
		MsgBox("Remove any external connections from the Oscilloscope.")
		'Disable user interaction
		frmZT1428.Enabled = False
		StatusBarMessage("Performing Built-In Test...")
		'Allow Message to be written to the toolbar
		System.Windows.Forms.Application.DoEvents()
		'Reset Instrument and GUI
		ResetScope()
		'Allow for a full reset
		Delay(1)
		System.Windows.Forms.Application.DoEvents()
		WriteMsg("*TST?")
		
		'Allow time for the instriment to perform BIT
		Delay(17)
		
		'Evaluate Test
		ErrorMessage = ReadMsg()
		If ReturnCode = 0 And ErrorMessage <> "" Then
			MsgBox("Built-In Test Passed", MsgBoxStyle.Information)
		Else
			Message = "Self-Test Failed: Error <" & ErrorMessage & ">"
			WriteMsg("SYST:ERR?")
			ErrorMessage = ReadMsg()
			MsgBox("Built-In Test Failed. " & vbCrLf & ErrorMessage, MsgBoxStyle.Critical)
		End If
		'Enable user interaction
		frmZT1428.Enabled = True
		StatusBarMessage("")
		ResetScope()
		'Notify user of BIT completion
		StatusBarMessage("Built-In Test completed.")
		'Allow message to be written to the status bar control
		Delay(2)
		
	End Sub
	
	
	Sub SaveToDisk(ByRef Index As Short)
		
		'---Added as a result of TDR98235 for Version 1.6---
		'Waveform file compatibility with ATLAS
		
		Dim FileId As Integer 'File Handle For Waveform File
		Dim FileData As Double 'Holds Conversion Value For Waveform Data Point
		Dim FileItem As Integer 'Wave Data Point Index
		Dim PreambleBuffer As String 'Waveform Preamble Description
		Dim WaveformText As String 'COMP Format Command for the selected Waveform Source
		Dim NullByte As Byte 'Byte Sized Zero =&H00
		Dim AtlasHeaderBlock(63) As Integer '4-Byte Header Array
		Dim FilePtrIndex As Double 'Location of File Pointer in Waveform File
		Dim PadLength As Double 'Number of Zero-Bytes to append to the end of the last block
		Dim i As Short
		
		'Get FileName from User
		Err.Clear() 'Clear Program Error Queue
		On Error Resume Next 'Disable Runtime Error Messages and Resume
		frmZT1428.dlgSegmentDataSave.ShowDialog()
		frmZT1428.dlgSegmentDataOpen.FileName = frmZT1428.dlgSegmentDataSave.FileName 'Show File Common Dialog
		If Err.Number Then
			Exit Sub 'The user canceled the File Common Dialog Option
		End If
		On Error GoTo 0 'Send Errors to end of this subroutine
		sSegFileName = Extract((frmZT1428.dlgSegmentDataOpen.FileName), "FileName")
		FileId = FreeFile
		
		If sSegFileName = "" Then
			MsgBox("Enter a segment file name to save (without extension).", MsgBoxStyle.Exclamation)
			Exit Sub
		ElseIf Len(sSegFileName) > 12 Then 
			MsgBox("The segment name cannot be more than 12 characters.  Please re-enter.", MsgBoxStyle.Critical)
			Exit Sub
		ElseIf IsNumeric(Left(sSegFileName, 1)) Then 
			MsgBox("The segment name cannot start with a number.  Please re-enter.", MsgBoxStyle.Critical)
			Exit Sub
		End If
		If FileExists((frmZT1428.dlgSegmentDataOpen.FileName)) Then
			If MsgBox("This folder already contains a file named '" & Extract((frmZT1428.dlgSegmentDataOpen.FileName), "FileName_Extension") & "'." & vbCrLf & "Would you like to replace it?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm File Replace") = MsgBoxResult.Yes Then
				Kill(frmZT1428.dlgSegmentDataOpen.FileName) 'Must be done to resize the file
			Else
				Exit Sub
			End If
		End If
		
		If InStr(frmZT1428.dlgSegmentDataOpen.FileName, ".") = 0 Then 'If No File Extension then use .seg
			'UPGRADE_NOTE: Variable frmZT1428.CommonDialog1 was renamed frmZT1428.CommonDialog1Open. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="94ADAC4D-C65D-414F-A061-8FDC6B83C5EC"'
			frmZT1428.dlgSegmentDataOpen.FileName = frmZT1428.CommonDialog1Open.FileName & ".seg"
			frmZT1428.dlgSegmentDataSave.FileName = frmZT1428.CommonDialog1Open.FileName & ".seg"
		End If
		
		'--------------------------
		'Save Waveform Data to the File
		'[Re]Format Wave for TETS-ATLAS Compatability (256 Byte File Blocks)
		'--------------------------
		
		'Query Scope Instrument for Data to Save to Disk
		Select Case Index
			Case 0
				WaveformText = "CHAN1"
			Case 1
				WaveformText = "CHAN2"
			Case 2
				WaveformText = "FUNC1"
			Case 3
				WaveformText = "WMEM1"
		End Select
		
		WriteMsg("MEM:VME:STAT ON")
		WriteMsg("RUN")
		If MainParam(SAMPLE_MODE).SetCur = "REAL" Then
			WriteMsg("ACQ:TYPE NORM")
		Else
			WriteMsg("ACQ:TYPE AVER")
		End If
		WriteMsg("ACQ:POIN " & MainParam(WF_POINTS).SetCur)
		WriteMsg("DIG " & WaveformText)
		Delay(2)
		WriteMsg("WAV:SOUR " & WaveformText)
		WriteMsg("WAV:FORM WORD")
		WriteMsg("VIEW " & WaveformText)
		WriteMsg("WAV:PRE?")
		PreambleBuffer = ReadMsg()
		WriteMsg("STOP")
		WriteMsg("MEM:VME:STAT OFF")
		
		'ReadMsg$() returns 8100 charters, strip nulls and append a Line Feed to end of preamble
		PreambleBuffer = Trim(StripNullCharacters(PreambleBuffer)) & vbLf
		
		'Write Header Block Data To Disk Memory
		AtlasHeaderBlock(0) = &HABCD4567 'Print Header Constant To File
		AtlasHeaderBlock(1) = Val(MainParam(WF_POINTS).SetCur) * 8 'Print Data Length To File
		FileOpen(FileId, frmZT1428.dlgSegmentDataOpen.FileName, OpenMode.Binary, OpenAccess.Write)
		'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePut(FileId, AtlasHeaderBlock, 1) 'Write Header
		
		'Write Preamble Data To Waveform Header Block
		'UPGRADE_WARNING: Couldn't resolve default property of object sIEEEDefinite(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		PreambleBuffer = sIEEEDefinite("TO", PreambleBuffer)
		'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FilePut(FileId, PreambleBuffer, 9) 'Write Header
		
		'Fill out the first block with nulls
		NullByte = &H0s
		For i = 9 + Len(PreambleBuffer) To 256
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(FileId, NullByte)
		Next i
		
		'Write Waveform Data to Disk Memory
		For FileItem = 0 To Val(MainParam(WF_POINTS).SetCur) - 1
			FileData = WaveMagnitude(WhatTrace(Index), FileItem)
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(FileId, FileData, (FileItem * 8) + 257)
		Next FileItem
		
		'Calculate And Write Footer Bolck with padding to make 256 Byte Blocks
		FilePtrIndex = 256 + (AtlasHeaderBlock(1)) / 8 'Add Data To File Ptr and Convert to BYTE SIZE Count
		'UPGRADE_WARNING: Mod has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		PadLength = 256 - (((FilePtrIndex * 8) + 256) Mod 256) 'Calculate Remaining Space in Block
		If PadLength = 256 Then PadLength = 0 'Do NOT pad if length is multiple of 256
		For FileItem = 1 To PadLength
			'UPGRADE_WARNING: Put was upgraded to FilePut and has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(FileId, NullByte)
		Next FileItem
		
		'Close File
		FileClose(FileId)
		'---End Modification for TDR98235 for Version 1.6---
		
	End Sub
	
	
	Public Function sIEEEDefinite(ByRef sDirection As String, ByRef sMsg As String) As Object
		'Returns IEEE 488.2 Definite length block formatted string
		'Example: "#15HELLO"
		
		Dim iNumDigits As Short
		Dim iNumChars As Short
		Dim i As Short
		
		If UCase(sDirection) = "FROM" Then
			iNumDigits = CShort(Mid(sMsg, 2, 1))
			iNumChars = CShort(Mid(sMsg, 3, iNumDigits))
			sIEEEDefinite = Mid(sMsg, 3 + iNumDigits, iNumChars)
		Else
			iNumChars = Len(sMsg)
			iNumDigits = Len(CStr(iNumChars))
			'UPGRADE_WARNING: Couldn't resolve default property of object sIEEEDefinite. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			sIEEEDefinite = "#" & CStr(iNumDigits) & CStr(iNumChars) & sMsg
		End If
		
	End Function
	
	
	Sub SaveToMemory(ByRef Index As Short)
		
		WriteMsg("STOP")
		Dim StoreCommand As String
		
		Select Case Index
			Case CHANNEL_1
				StoreCommand = "STORE CHAN1,WMEM1"
				WriteMsg(StoreCommand)
				MEMRangeSetting = Val(MainParam(IN1_VSENS).SetCur) / 10
				MEMOffsetSetting = Val(MainParam(IN1_OFFSET).SetCur)
				MainParam(MEM_OFFSET).SetCur = MainParam(IN1_OFFSET).SetCur
			Case CHANNEL_2
				StoreCommand = "STORE CHAN2,WMEM1"
				WriteMsg(StoreCommand)
				MEMRangeSetting = Val(MainParam(IN2_VSENS).SetCur) / 10
				MEMOffsetSetting = Val(MainParam(IN2_OFFSET).SetCur)
			Case MATH_AREA
				StoreCommand = "STORE FUNC1,WMEM1"
				WriteMsg(StoreCommand)
				MEMRangeSetting = Val(MainParam(FUNC_RANGE).SetCur)
				MEMOffsetSetting = Val(MainParam(FUNC_OFFSET).SetCur)
				MainParam(MEM_OFFSET).SetCur = MainParam(FUNC_OFFSET).SetCur
		End Select
		MainParam(MEM_RANGE).SetCur = Str(MEMRangeSetting)
		MainParam(MEM_OFFSET).SetCur = Str(MEMOffsetSetting)
		frmZT1428.txtMemoryRange.Text = EngNotate(MEMRangeSetting, MainParam(MEM_RANGE))
		frmZT1428.txtMemVoltOffset.Text = EngNotate(MEMOffsetSetting, MainParam(MEM_OFFSET))
		If ChannelOn(MEMORY_AREA) Then
			frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(MEMORY_AREA)).Maximum = (MEMRangeSetting * 5) + MEMOffsetSetting
			frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(MEMORY_AREA)).Minimum = (-MEMRangeSetting * 5) + MEMOffsetSetting
			GetMemoryWave(WaveMagnitude, WhatTrace(MEMORY_AREA), False)
			frmZT1428.cwgScopeDisplay.PlotXvsY(WaveTime, WaveMagnitude, True)
		End If
		WriteMsg("RUN")
		
	End Sub
	
	
	Sub SelectOption(ByRef ParamIndex As Short, ByRef SelectIndex As Short)
		'Declare Procedure Level variables
		Dim PointMlt As Short
		Dim ProperList As Short
		Dim ScaleLoop As Short
		
		Select Case ParamIndex
			Case HOLD_OFF
				HoldType = ParamCode(HOLD_OFF, SelectIndex)
				If SelectIndex = D_EVENTS Then
					MainParam(HOLD_OFF).SetCur = HoldEvents
					MainParam(HOLD_OFF).SetDef = HOLD_MIN_EVENTS
					MainParam(HOLD_OFF).SetMax = HOLD_MAX_EVENTS
					MainParam(HOLD_OFF).SetMin = HOLD_MIN_EVENTS
					MainParam(HOLD_OFF).SetUOM = ""
					HoldType = "EVENT, "
					frmZT1428.lblEvents.Text = "Events"
					WriteMsg(MainParam(HOLD_OFF).SetCod & HoldType & MainParam(HOLD_OFF).SetCur)
				Else
					MainParam(HOLD_OFF).SetDef = HOLD_MIN_TIME
					MainParam(HOLD_OFF).SetMax = HOLD_MAX_TIME
					MainParam(HOLD_OFF).SetMin = HOLD_MIN_TIME
					MainParam(HOLD_OFF).SetUOM = "S"
					MainParam(HOLD_OFF).SetCur = HoldTime
					frmZT1428.lblEvents.Text = "Time"
					HoldType = "TIME, "
					WriteMsg(MainParam(HOLD_OFF).SetCod & HoldType & MainParam(HOLD_OFF).SetCur)
				End If
				'UPGRADE_WARNING: Couldn't resolve default property of object MainParam(HOLD_OFF).TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				MainParam(HOLD_OFF).TxtBox.Text = EngNotate(Val(MainParam(HOLD_OFF).SetCur), MainParam(HOLD_OFF))
				RangeUpdate(HOLD_OFF)
				Exit Sub
			Case TRIG_DEL
				DelayType = ParamCode(TRIG_DEL, SelectIndex)
				If SelectIndex = D_EVENTS Then
					frmZT1428.lblTrigDelayEvents.Text = "Events"
					frmZT1428.fraTrigDelaySource.Visible = True
					frmZT1428.fraDelaySlope.Visible = True
					MainParam(TRIG_DEL).SetCur = DelayEvents
					MainParam(TRIG_DEL).SetDef = DELAY_MIN_EVENTS
					MainParam(TRIG_DEL).SetMax = DELAY_MAX_EVENTS
					MainParam(TRIG_DEL).SetMin = DELAY_MIN_EVENTS
					MainParam(TRIG_DEL).SetUOM = ""
					WriteMsg(MainParam(TRIG_DEL).SetCod & DelayType & MainParam(TRIG_DEL).SetCur)
				Else
					frmZT1428.lblTrigDelayEvents.Text = "Time"
					frmZT1428.fraTrigDelaySource.Visible = False
					frmZT1428.fraDelaySlope.Visible = False
					MainParam(TRIG_DEL).SetCur = DelayTime
					MainParam(TRIG_DEL).SetDef = DELAY_MIN_TIME
					MainParam(TRIG_DEL).SetMax = DELAY_MAX_TIME
					MainParam(TRIG_DEL).SetMin = DELAY_MIN_TIME
					MainParam(TRIG_DEL).SetUOM = "S"
					WriteMsg(MainParam(TRIG_DEL).SetCod & DelayType & MainParam(TRIG_DEL).SetCur)
				End If
				'UPGRADE_WARNING: Couldn't resolve default property of object MainParam(TRIG_DEL).TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				MainParam(TRIG_DEL).TxtBox.Text = EngNotate(Val(MainParam(TRIG_DEL).SetCur), MainParam(TRIG_DEL))
				RangeUpdate(TRIG_DEL)
				Exit Sub
			Case TRIG_MODE
				Select Case SelectIndex
					Case EDGE_TRIG
						frmZT1428.fraHoldOff.Visible = True
						frmZT1428.fraTrigDelay.Visible = False
						frmZT1428.fraTrigDelaySource.Visible = False
						frmZT1428.fraDelaySlope.Visible = False
						frmZT1428.fraStandard.Visible = False
						frmZT1428.fraField.Visible = False
						frmZT1428.fraLine.Visible = False
						frmZT1428.fraOccurrence.Visible = False
						MainParam(TRIG_SLOPE).SetCod = ":TRIG:SLOP "
					Case DEL_TRIG
						frmZT1428.fraHoldOff.Visible = False
						frmZT1428.fraTrigDelay.Visible = True
						If frmZT1428.optTrigDelay(D_EVENTS)._Value = True Then
							frmZT1428.fraTrigDelaySource.Visible = True
							frmZT1428.fraDelaySlope.Visible = True
						End If
						frmZT1428.fraStandard.Visible = False
						frmZT1428.fraField.Visible = False
						frmZT1428.fraLine.Visible = False
						frmZT1428.fraOccurrence.Visible = True
						MainParam(TRIG_SLOPE).SetCod = ":TRIG:SLOP "
					Case TV_TRIG
						frmZT1428.fraHoldOff.Visible = True
						frmZT1428.fraTrigDelay.Visible = True
						If frmZT1428.optTrigDelay(D_EVENTS)._Value = True Then
							frmZT1428.fraTrigDelaySource.Visible = True
							frmZT1428.fraDelaySlope.Visible = True
						End If
						frmZT1428.fraStandard.Visible = True
						frmZT1428.fraField.Visible = True
						frmZT1428.fraLine.Visible = True
						frmZT1428.fraOccurrence.Visible = True
						MainParam(TRIG_SLOPE).SetCod = ":TRIG:POL "
				End Select
			Case ECLT0_STATE, ECLT1_STATE, OUT_TRIG_STATE
				MainParam(ParamIndex).SetCur = ParamCode(ParamIndex, SelectIndex)
				If MainParam(OUT_TRIG_STATE).SetCur = "ON" Or MainParam(ECLT0_STATE).SetCur = "ON" Or MainParam(ECLT1_STATE).SetCur = "ON" Then
					WriteMsg("OUTP:STATE ON")
				Else
					WriteMsg("OUTP:STATE OFF")
				End If
			Case WF_POINTS
				If SelectIndex = 1 Then
					frmZT1428.chkDisplayTrace(MATH_AREA).Enabled = False
					frmZT1428.chkDisplayTrace(MATH_AREA).Value = False
					'Disable measurement functions and reset measurement status to OFF
					'Measurements can not be taken in 8000 point mode V1.8 JCB
					frmZT1428.tabMainFunctions.TabPages.Item(3).Enabled = False
					ChangeMeasurement(MEAS_OFF)
					frmZT1428.tbrMeasFunction.Buttons(20).Value = ComctlLib.ValueConstants.tbrPressed
					frmZT1428.txtDataDisplay.Text = "Ready..."
				Else
					If frmZT1428.chkContinuous.Value = False Then
						frmZT1428.chkDisplayTrace(MATH_AREA).Enabled = True
					End If
					'Enable Measurement functions when switching back to 500 points V1.8 JCB
					frmZT1428.tabMainFunctions.TabPages.Item(3).Enabled = True
					'mh added below
					frmZT1428.tbrMeasFunction.Buttons(20).Value = ComctlLib.ValueConstants.tbrPressed
					frmZT1428.txtDataDisplay.Text = "Ready..."
					
				End If
		End Select
		
		MainParam(ParamIndex).SetCur = ParamCode(ParamIndex, SelectIndex)
		WriteMsg(MainParam(ParamIndex).SetCod & MainParam(ParamIndex).SetCur)
		Select Case ParamIndex
			Case WF_POINTS
				SetMemOffsets(Val(MainParam(ParamIndex).SetCur))
				
				If Val(MainParam(WF_POINTS).SetCur) = 8000 Then
					ProperList = PNT8000_HLIST
				Else
					ProperList = HORZ_SENS
				End If
				frmZT1428.cboHorizRange.Items.Clear()
				For ScaleLoop = 0 To 29
					frmZT1428.cboHorizRange.Items.Insert(ScaleLoop, ListBoxFiller(ProperList, ScaleLoop))
				Next ScaleLoop
				frmZT1428.cboHorizRange.SelectedIndex = HScaleListItem
				
				SetHScaleGraph()
			Case H_REF_POS
				SetHScaleGraph()
			Case TRIG_MODE
				If SelectIndex = DEL_TRIG Then
					WriteMsg("TRIG:QUAL EDGE")
				End If
				WriteMsg(MainParam(TRIG_SLOPE).SetCod & MainParam(TRIG_SLOPE).SetCur)
			Case SAMPLE_MODE
				If SelectIndex = REALTIME Then
					WriteMsg("ACQ:TYPE NORM")
					frmZT1428.fraDataPoints.Enabled = True
					If Not MakingMeasurement Then
						frmZT1428.optDataPoints(0).Enabled = True
						frmZT1428.optDataPoints(1).Enabled = True
					End If
					frmZT1428.fraAvgCount.Visible = False
					frmZT1428.fraCompletion.Visible = False
				Else
					frmZT1428.optDataPoints(PTS_500).Value = True
					System.Windows.Forms.Application.DoEvents() 'This will allow the List box to be updated and recursively call this routine
					frmZT1428.fraDataPoints.Enabled = False
					frmZT1428.optDataPoints(0).Enabled = False
					frmZT1428.optDataPoints(1).Enabled = False
					frmZT1428.fraAvgCount.Visible = True
					frmZT1428.fraCompletion.Visible = True
					WriteMsg("ACQ:TYPE AVER")
				End If
			Case IN1_ATTN_FAC
				FillVSense(CHANNEL_1, SelectIndex)
				SetTrigRange(CHANNEL_1)
			Case IN2_ATTN_FAC
				FillVSense(CHANNEL_2, SelectIndex)
				SetTrigRange(CHANNEL_2)
			Case TRIG_SOUR
				If SelectIndex = EXT_TRIG Then
					'Show Ext Trig Impedance frame on OPTION tab only if source = EXT
					frmZT1428.fraExtTriggerCoup.Visible = True
				Else
					frmZT1428.fraExtTriggerCoup.Visible = False
				End If
				If SelectIndex = ECLT_0 Or SelectIndex = ECLT_1 Then
					frmZT1428.fraTrigLevel.Visible = False
				Else
					frmZT1428.fraTrigLevel.Visible = True
					SetTrigRange(SelectIndex)
				End If
			Case TRIG_STAND, TRIG_FIELD
				SetTVTrigLineRange()
			Case ACQ_MODE
				If ParamCode(ParamIndex, SelectIndex) = "TRIG" Then
					bTrigMode = True
				Else
					bTrigMode = False
				End If
		End Select
		
	End Sub
	
	
	Sub SelectTraces(ByRef TraceDisplay As Short, ByRef Value As Short)
		'Declare Procedure Level variables
		Dim OriginalTraces As Short
		Dim TraceNumber As Short
		Dim ChannelNumber As Short
		Dim WavePoint As Short
		Dim TraceOn As Short
		Dim TempWaveMagnitude() As Double
		Dim Ch1TempWave() As Double
		Dim Ch2TempWave() As Double
		Dim MathTempWave() As Double
		Dim MemTempWave() As Double
		Dim DataPoints As Short
		Dim Vscale As Single
		Dim Voffset As Single
		Dim MathRange As String
		Dim TempWvPts As Short
		Dim i As Short
		
		frmZT1428.cwgScopeDisplay.ClearData()
		
		
		
		DataPoints = Val(MainParam(WF_POINTS).SetCur) - 1
		Dim TempWaveTime(DataPoints) As Object
		ReDim TempWaveMagnitude(3, DataPoints)
		
		OriginalTraces = 0
		If UBound(WaveMagnitude, 2) < DataPoints Then
			TempWvPts = UBound(WaveMagnitude, 2)
		Else
			TempWvPts = DataPoints
		End If
		
		
		
		For ChannelNumber = 0 To 3
			If ChannelOn(ChannelNumber) Then
				For WavePoint = 0 To TempWvPts
					TempWaveMagnitude(ChannelNumber, WavePoint) = WaveMagnitude(OriginalTraces, WavePoint)
					'UPGRADE_WARNING: Couldn't resolve default property of object TempWaveTime(WavePoint). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					TempWaveTime(WavePoint) = WaveTime(WavePoint)
				Next WavePoint
				OriginalTraces = OriginalTraces + 1
			End If
		Next ChannelNumber
		
		ChannelOn(TraceDisplay) = Value
		NumberOfTraces = 0
		For ChannelNumber = 0 To 3
			If ChannelOn(ChannelNumber) Then
				TraceAssociation(NumberOfTraces) = ChannelNumber
				WhatTrace(ChannelNumber) = NumberOfTraces
				NumberOfTraces = NumberOfTraces + 1
				frmZT1428.cwgScopeDisplay.Plots.Item(NumberOfTraces).PointColor = System.Convert.ToUInt32(TraceColor(ChannelNumber))
			Else
				WhatTrace(ChannelNumber) = -1
			End If
			Select Case ChannelNumber
				Case CHANNEL_1
					frmZT1428.tabMainFunctions.TabPages.Item(1).Enabled = ChannelOn(CHANNEL_1)
				Case CHANNEL_2
					frmZT1428.tabMainFunctions.TabPages.Item(2).Enabled = ChannelOn(CHANNEL_2)
				Case MATH_AREA
					frmZT1428.picMathOptions.Visible = ChannelOn(MATH_AREA)
				Case MEMORY_AREA
					frmZT1428.picMemOptions.Visible = ChannelOn(MEMORY_AREA)
					If ChannelOn(MEMORY_AREA) = False Then
						'Clear WMEM1
						WriteMsg("STORE WMEM2,WMEM1")
					End If
			End Select
		Next ChannelNumber
		frmZT1428.tabMainFunctions.TabPages.Item(WF_TAB).Enabled = (ChannelOn(MATH_AREA) Or ChannelOn(MEMORY_AREA))
		If NumberOfTraces <> 0 Then
			NumberOfTraces = NumberOfTraces - 1
		Else
			frmZT1428.cwgScopeDisplay.ClearData()
			Exit Sub
		End If
		
		ReDim WaveTime(DataPoints)
		ReDim WaveMagnitude(NumberOfTraces, DataPoints)
		ReDim Ch1TempWave(0, DataPoints)
		ReDim Ch2TempWave(0, DataPoints)
		ReDim MathTempWave(0, DataPoints)
		ReDim MemTempWave(0, DataPoints)
		
		For TraceOn = 0 To NumberOfTraces
			If TraceAssociation(TraceOn) <> MEMORY_AREA Then
				For WavePoint = 0 To DataPoints
					'UPGRADE_WARNING: Couldn't resolve default property of object TempWaveTime(WavePoint). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					WaveTime(WavePoint) = TempWaveTime(WavePoint)
					WaveMagnitude(TraceOn, WavePoint) = TempWaveMagnitude(TraceAssociation(TraceOn), WavePoint)
				Next WavePoint
				Select Case TraceAssociation(TraceOn)
					Case CHANNEL_1
						Vscale = Val(MainParam(IN1_VSENS).SetCur)
						Voffset = Val(MainParam(IN1_OFFSET).SetCur)
						frmZT1428.cwgScopeDisplay.Plots.Item(1).PointColor = System.Convert.ToUInt32(TraceColor(CHANNEL_1))
						For i = 0 To TempWvPts
							Ch1TempWave(0, i) = WaveMagnitude(TraceOn, i)
						Next i
					Case CHANNEL_2
						Vscale = Val(MainParam(IN2_VSENS).SetCur)
						Voffset = Val(MainParam(IN2_OFFSET).SetCur)
						frmZT1428.cwgScopeDisplay.Plots.Item(2).PointColor = System.Convert.ToUInt32(TraceColor(CHANNEL_2))
						For i = 0 To TempWvPts
							Ch2TempWave(0, i) = WaveMagnitude(TraceOn, i)
						Next i
					Case MATH_AREA
						Vscale = Val(MainParam(FUNC_RANGE).SetCur) * 10
						Voffset = Val(MainParam(FUNC_OFFSET).SetCur)
						frmZT1428.cwgScopeDisplay.Plots.Item(3).PointColor = System.Convert.ToUInt32(TraceColor(MATH_AREA))
						For i = 0 To TempWvPts
							MathTempWave(0, i) = WaveMagnitude(TraceOn, i)
						Next i
				End Select
				frmZT1428.cwgScopeDisplay.Axes.Item(2 + TraceOn).Maximum = (Vscale / 2!) + Voffset
				frmZT1428.cwgScopeDisplay.Axes.Item(2 + TraceOn).Minimum = (-Vscale / 2!) + Voffset
			Else
				GetMemoryWave(WaveMagnitude, TraceOn, False) ' , (TraceDisplay% = MEMORY_AREA)
				For i = 0 To DataPoints
					MemTempWave(0, i) = WaveMagnitude(TraceOn, i)
				Next i
				'UPGRADE_WARNING: Couldn't resolve default property of object TempWaveTime(10). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				If TempWaveTime(10) <> 0 Then
					For WavePoint = 0 To DataPoints
						'UPGRADE_WARNING: Couldn't resolve default property of object TempWaveTime(WavePoint). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						WaveTime(WavePoint) = TempWaveTime(WavePoint)
					Next WavePoint
				Else
					For WavePoint = 0 To DataPoints
						WaveTime(WavePoint) = ((Val(MainParam(HORZ_SENS).SetCur) / DataPoints) * WavePoint) - (Val(MainParam(HORZ_SENS).SetCur) / 2) + Val(MainParam(REF_DELAY).SetCur)
					Next WavePoint
				End If
			End If
		Next TraceOn
		
		' frmZT1428.cwgScopeDisplay.ClearData
		' frmZT1428.cwgScopeDisplay.PlotXvsY WaveTime!(), WaveMagnitude#(), True
		frmZT1428.cwgScopeDisplay.Plots.Item(1).PointColor = System.Convert.ToUInt32(TraceColor(CHANNEL_1))
		frmZT1428.cwgScopeDisplay.Plots.Item(1).PlotXvsY(WaveTime, Ch1TempWave)
		frmZT1428.cwgScopeDisplay.Plots.Item(2).PointColor = System.Convert.ToUInt32(TraceColor(CHANNEL_2))
		frmZT1428.cwgScopeDisplay.Plots.Item(2).PlotXvsY(WaveTime, Ch2TempWave)
		frmZT1428.cwgScopeDisplay.Plots.Item(3).PointColor = System.Convert.ToUInt32(TraceColor(MATH_AREA))
		frmZT1428.cwgScopeDisplay.Plots.Item(3).PlotXvsY(WaveTime, MathTempWave)
		frmZT1428.cwgScopeDisplay.Plots.Item(4).PointColor = System.Convert.ToUInt32(TraceColor(MEMORY_AREA))
		frmZT1428.cwgScopeDisplay.Plots.Item(4).PlotXvsY(WaveTime, MemTempWave)
		
		
	End Sub
	
	
	Sub SetHScale()
		'Declare Procedure Level variables
		Dim HScale As Single
		
		MainParam(HORZ_SENS).SetCur = ParamCode(HORZ_SENS, frmZT1428.cboHorizRange.SelectedIndex)
		HScaleListItem = frmZT1428.cboHorizRange.SelectedIndex
		dAcqTime = Val(MainParam(HORZ_SENS).SetCur) * 16
		
		'Rescale Trigger Delay Range
		HScale = Val(MainParam(HORZ_SENS).SetCur)
		MainParam(REF_DELAY).SetMax = Str(HScale * MAX_DEL_RATIO)
		If HScale > TIME_BASE_BREAK_POINT Then
			MainParam(REF_DELAY).SetMin = Str(HScale * MIN_DEL_RATIO)
		Else
			MainParam(REF_DELAY).SetMin = CStr(MIN_DEL)
		End If
		If Val(MainParam(REF_DELAY).SetCur) > Val(MainParam(REF_DELAY).SetMax) Then
			MainParam(REF_DELAY).SetCur = MainParam(REF_DELAY).SetMax
		End If
		If Val(MainParam(REF_DELAY).SetCur) < Val(MainParam(REF_DELAY).SetMin) Then
			MainParam(REF_DELAY).SetCur = MainParam(REF_DELAY).SetMin
		End If
		'UPGRADE_WARNING: Couldn't resolve default property of object MainParam(REF_DELAY).TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainParam(REF_DELAY).TxtBox.Text = EngNotate(Val(MainParam(REF_DELAY).SetCur), MainParam(REF_DELAY))
		
		WriteMsg(MainParam(HORZ_SENS).SetCod & " " & MainParam(HORZ_SENS).SetCur)
		WriteMsg(MainParam(REF_DELAY).SetCod & " " & MainParam(REF_DELAY).SetCur)
		SetMemOffsets(Val(MainParam(WF_POINTS).SetCur))
		frmZT1428.StatusBar1.Panels(HORZ_SEN_PAN).Text = "Hor. Axis = " & frmZT1428.cboHorizRange.Text
		SetHScaleGraph()
		
	End Sub
	
	
	Sub SetHScaleGraph()
		'Declare Procedure Level variables
		Dim HScale As Single
		Dim DDelay As Single
		Dim PointMlt As Short
		Dim TempAxis As Double
		
		PointMlt = Val(MainParam(WF_POINTS).SetCur) \ 500
		HScale = Val(MainParam(HORZ_SENS).SetCur)
		DDelay = Val(MainParam(REF_DELAY).SetCur)
		Select Case MainParam(H_REF_POS).SetCur
			Case H_LEFT
				frmZT1428.cwgScopeDisplay.Axes.Item(1).Maximum = HScale * PointMlt + DDelay
				frmZT1428.cwgScopeDisplay.Axes.Item(1).Minimum = DDelay
			Case H_CENTER
				frmZT1428.cwgScopeDisplay.Axes.Item(1).Maximum = (HScale * PointMlt / 2) + DDelay
				frmZT1428.cwgScopeDisplay.Axes.Item(1).Minimum = -(HScale * PointMlt / 2) + DDelay
				TempAxis = System.Math.Abs(frmZT1428.cwgScopeDisplay.Axes.Item(1).Minimum)
			Case H_RIGHT
				frmZT1428.cwgScopeDisplay.Axes.Item(1).Maximum = DDelay
				frmZT1428.cwgScopeDisplay.Axes.Item(1).Minimum = -HScale * PointMlt + DDelay
		End Select
		frmZT1428.cwgScopeDisplay.Cursors.Item(1).XPosition = ((frmZT1428.cwgScopeDisplay.Axes.Item(1).Maximum - frmZT1428.cwgScopeDisplay.Axes.Item(1).Minimum) / 2) + frmZT1428.cwgScopeDisplay.Axes.Item(1).Minimum
		frmZT1428.cwgScopeDisplay.Cursors.Item(1).SetPosition(0, 0)
		
	End Sub
	
	
	Sub SetMemOffsets(ByRef WavePonts As Short)
		'Declare Procedure Level variables
		Dim temp As Integer
		Const MEM_500_CH1 As Short = 364
		Const MEM_500_CH2 As Short = 2368
		Const MEM_8000_CH1 As Short = 4372 'Added V1.8 JCB
		Const MEM_8000_CH2 As Short = 20948 'Added V1.8 JCB
		
		WriteMsg("MEM:VME:STATE ON")
		If WavePonts = 500 Then
			ScopeMemOffset(MEM_500_CH1, MEM_500_CH2, 0, 500, temp)
		Else
			'ScopeMemOffset called with specific memory offsets V1.8 JCB
			ScopeMemOffset(MEM_8000_CH1, MEM_8000_CH2, 0, 8000, temp)
		End If
		
	End Sub
	
	
	Sub SetTVTrigLineRange()
		'Declare Procedure Level variables
		Dim TempLine As String
		
		TempLine = MainParam(TRIG_LINE).SetCur
		If MainParam(TRIG_STAND).SetCur = "525" Then
			If MainParam(TRIG_FIELD).SetCur = "1" Then
				MainParam(TRIG_LINE).SetMin = "1"
				MainParam(TRIG_LINE).SetMax = "263"
			Else
				MainParam(TRIG_LINE).SetMin = "1"
				MainParam(TRIG_LINE).SetMax = "262"
			End If
		Else
			If MainParam(TRIG_FIELD).SetCur = "1" Then
				MainParam(TRIG_LINE).SetMin = "1"
				MainParam(TRIG_LINE).SetMax = "313"
			Else
				MainParam(TRIG_LINE).SetMin = "314"
				MainParam(TRIG_LINE).SetMax = "625"
			End If
		End If
		Select Case Val(MainParam(TRIG_LINE).SetCur)
			Case Is > Val(MainParam(TRIG_LINE).SetMax)
				MainParam(TRIG_LINE).SetCur = MainParam(TRIG_LINE).SetMax
				WriteMsg(MainParam(TRIG_LINE).SetCod & MainParam(TRIG_LINE).SetCur)
				frmZT1428.txtLine.Text = MainParam(TRIG_LINE).SetCur
			Case Is < Val(MainParam(TRIG_LINE).SetMin)
				MainParam(TRIG_LINE).SetCur = MainParam(TRIG_LINE).SetMin
				WriteMsg(MainParam(TRIG_LINE).SetCod & MainParam(TRIG_LINE).SetCur)
				frmZT1428.txtLine.Text = MainParam(TRIG_LINE).SetCur
		End Select
		
	End Sub
	
	
	Sub SetVOffsetRange(ByRef OffsetIndex As Short, ByRef VSenseIndex As Short, ByRef AttinIndex As Short)
		'Declare Procedure Level variables
		Dim AttenuationFactor As Single
		
		AttenuationFactor = Val(MainParam(AttinIndex).SetCur)
		Select Case Val(MainParam(VSenseIndex).SetCur) / AttenuationFactor
			Case Is <= 0.4
				MainParam(OffsetIndex).SetMin = VB6.Format(-2 * AttenuationFactor)
				MainParam(OffsetIndex).SetMax = VB6.Format(2 * AttenuationFactor)
			Case 0.4 To 2
				MainParam(OffsetIndex).SetMin = VB6.Format(-10 * AttenuationFactor)
				MainParam(OffsetIndex).SetMax = VB6.Format(10 * AttenuationFactor)
			Case 2 To 10
				MainParam(OffsetIndex).SetMin = VB6.Format(-50 * AttenuationFactor)
				MainParam(OffsetIndex).SetMax = VB6.Format(50 * AttenuationFactor)
			Case Is > 10
				MainParam(OffsetIndex).SetMin = VB6.Format(-250 * AttenuationFactor)
				MainParam(OffsetIndex).SetMax = VB6.Format(250 * AttenuationFactor)
		End Select
		If Val(MainParam(OffsetIndex).SetCur) > Val(MainParam(OffsetIndex).SetMax) Then
			MainParam(OffsetIndex).SetCur = MainParam(OffsetIndex).SetMax
		End If
		If Val(MainParam(OffsetIndex).SetCur) < Val(MainParam(OffsetIndex).SetMin) Then
			MainParam(OffsetIndex).SetCur = MainParam(OffsetIndex).SetMin
		End If
		WriteMsg(MainParam(OffsetIndex).SetCod & MainParam(OffsetIndex).SetCur)
		'UPGRADE_WARNING: Couldn't resolve default property of object MainParam(OffsetIndex).TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		MainParam(OffsetIndex).TxtBox.Text = EngNotate(Val(MainParam(OffsetIndex).SetCur), MainParam(OffsetIndex))
		
	End Sub
	
	
	Sub SetVScaleGraph(ByRef VertIndex As Short)
		' set the properties on the vertical axis of the graph according to the user settings
		Dim Vscale As Single
		Dim offset As Single
		
		If frmZT1428.cboVertRange(VertIndex).SelectedIndex = -1 Then
			Exit Sub
		End If
		If VertIndex = CHANNEL_1 Then
			MainParam(IN1_VSENS).SetCur = ParamCode(IN1_VSENS, frmZT1428.cboVertRange(VertIndex).SelectedIndex)
			WriteMsg(MainParam(IN1_VSENS).SetCod & " " & MainParam(IN1_VSENS).SetCur)
			Vscale = Val(MainParam(IN1_VSENS).SetCur)
			offset = Val(MainParam(IN1_OFFSET).SetCur)
			SetVOffsetRange(IN1_OFFSET, IN1_VSENS, IN1_ATTN_FAC)
		End If
		If VertIndex = CHANNEL_2 Then
			MainParam(IN2_VSENS).SetCur = ParamCode(IN2_VSENS, frmZT1428.cboVertRange(VertIndex).SelectedIndex)
			WriteMsg(MainParam(IN2_VSENS).SetCod & " " & MainParam(IN2_VSENS).SetCur)
			Vscale = Val(MainParam(IN2_VSENS).SetCur)
			offset = Val(MainParam(IN2_OFFSET).SetCur)
			SetVOffsetRange(IN2_OFFSET, IN2_VSENS, IN2_ATTN_FAC)
		End If
		If frmZT1428.chkContinuous.Value = True Then
			frmZT1428.cwgScopeDisplay.ClearData()
		End If
		'Filter out invalid WhatTrace selections so that XAxis does not accidently get set
		If WhatTrace(VertIndex) >= 0 Then
			frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(VertIndex)).Maximum = (Vscale / 2!) + offset
			frmZT1428.cwgScopeDisplay.Axes.Item(2 + WhatTrace(VertIndex)).Minimum = (-Vscale / 2!) + offset
		End If
		
		SetTrigRange(VertIndex)
		
	End Sub
	
	
	Sub SetTrigRange(ByRef ChanIndex As Short)
		'Declare Procedure Level variables
		Dim VRAnge As Single
		Dim Voffset As Single
		Dim AttinFact As Single
		
		If ChanIndex = frmZT1428.cboTrigSour.SelectedIndex Then
			Select Case ChanIndex
				Case CHANNEL_1
					VRAnge = Val(MainParam(IN1_VSENS).SetCur)
					Voffset = Val(MainParam(IN1_OFFSET).SetCur)
					AttinFact = Val(MainParam(IN1_ATTN_FAC).SetCur)
					Select Case VRAnge / AttinFact
						Case Is > 4
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (TRIG_LEV_ADJ_UPR * VRAnge)) 'JHill  TDR99152
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (TRIG_LEV_ADJ_UPR * VRAnge)) 'JHill  TDR99152
						Case 0.5
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (AttinFact * 0.74))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (AttinFact * 0.74))
						Case 0.05
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (AttinFact * 0.072))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (AttinFact * 0.072))
						Case 0.01
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (AttinFact * 0.012))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (AttinFact * 0.012))
						Case Else
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (TRIG_LEV_ADJ_LWR * VRAnge))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (TRIG_LEV_ADJ_LWR * VRAnge))
					End Select
				Case CHANNEL_2
					VRAnge = Val(MainParam(IN2_VSENS).SetCur)
					Voffset = Val(MainParam(IN2_OFFSET).SetCur)
					AttinFact = Val(MainParam(IN2_ATTN_FAC).SetCur)
					Select Case VRAnge / AttinFact
						Case Is > 4
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (TRIG_LEV_ADJ_UPR * VRAnge)) 'JHill  TDR99152
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (TRIG_LEV_ADJ_UPR * VRAnge)) 'JHill  TDR99152
						Case 0.5
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (AttinFact * 0.74))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (AttinFact * 0.74))
						Case 0.05
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (AttinFact * 0.072))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (AttinFact * 0.072))
						Case 0.01
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (AttinFact * 0.012))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (AttinFact * 0.012))
						Case Else
							MainParam(TRIG_LEV).SetMax = VB6.Format(Voffset + (TRIG_LEV_ADJ_LWR * VRAnge))
							MainParam(TRIG_LEV).SetMin = VB6.Format(Voffset - (TRIG_LEV_ADJ_LWR * VRAnge))
					End Select
				Case Else
					MainParam(TRIG_LEV).SetMin = "-2"
					MainParam(TRIG_LEV).SetMax = "2"
			End Select
			If Val(MainParam(TRIG_LEV).SetCur) < Val(MainParam(TRIG_LEV).SetMin) Then
				MainParam(TRIG_LEV).SetCur = MainParam(TRIG_LEV).SetMin
			End If
			If Val(MainParam(TRIG_LEV).SetCur) > Val(MainParam(TRIG_LEV).SetMax) Then
				MainParam(TRIG_LEV).SetCur = MainParam(TRIG_LEV).SetMax
			End If
			'UPGRADE_WARNING: Couldn't resolve default property of object MainParam(TRIG_LEV).TxtBox. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			'UPGRADE_WARNING: Couldn't resolve default property of object MainParam().TxtBox. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			MainParam(TRIG_LEV).TxtBox = EngNotate(Val(MainParam(TRIG_LEV).SetCur), MainParam(TRIG_LEV))
			WriteMsg(MainParam(TRIG_LEV).SetCod & MainParam(TRIG_LEV).SetCur)
		End If
		
	End Sub
	
	
	Sub InitializeArrays()
		
		'Initialize various normal variables
		VTraceListItem = DEF_V_RANGE
		HScaleListItem = DEF_H_RANGE
		DelayEvents = DELAY_MIN_EVENTS
		DelayTime = DELAY_MIN_TIME
		HoldEvents = HOLD_MIN_EVENTS
		HoldTime = HOLD_MIN_TIME
		HoldType = "TIME, "
		DelayType = "TIME, "
		NumberOfTraces = 0
		CurrentMeas = 20
		ReDim WaveMagnitude(3, 499)
		ReDim WaveTime(499)
		
		'Trace Associations
		WhatTrace(CHANNEL_1) = 0
		WhatTrace(CHANNEL_2) = -1
		WhatTrace(MATH_AREA) = -1
		WhatTrace(MEMORY_AREA) = -1
		
		'Trace colors
		TraceColor(CHANNEL_1) = RGB(255, 255, 0)
		TraceColor(CHANNEL_2) = RGB(0, 255, 255)
		TraceColor(MATH_AREA) = RGB(255, 0, 255)
		TraceColor(MEMORY_AREA) = RGB(0, 255, 0)
		
		'Delay Measurment Parameters
		StartSource = "CHAN1"
		StopSource = "CHAN1"
		StartSlope = "POS"
		StopSlope = "POS"
		DelayLevelUnit = "VOLT"
		
		StartLevel.SetCur = "0"
		StartLevel.SetMin = "-250"
		StartLevel.SetMax = "250"
		StartLevel.SetUOM = "V"
		StartLevel.SetMinInc = ".001"
		StartLevel.SetRes = "A2"
		StartLevel.TxtBox = frmZT1428.txtStartLevel
		
		StopLevel.SetCur = "0"
		StopLevel.SetMin = "-250"
		StopLevel.SetMax = "250"
		StopLevel.SetUOM = "V"
		StopLevel.SetMinInc = ".001"
		StopLevel.SetRes = "A2"
		StopLevel.TxtBox = frmZT1428.txtStopLevel
		
		StartEdge.SetCur = "1"
		StartEdge.SetMin = "1"
		StartEdge.SetMax = "100"
		StartEdge.SetUOM = ""
		StartEdge.SetMinInc = "1"
		StartEdge.SetRes = "D0"
		StartEdge.TxtBox = frmZT1428.txtStartEdge
		
		StopEdge.SetCur = "1"
		StopEdge.SetMin = "1"
		StopEdge.SetMax = "100"
		StopEdge.SetUOM = ""
		StopEdge.SetMinInc = "1"
		StopEdge.SetRes = "D0"
		StopEdge.TxtBox = frmZT1428.txtStopEdge
		
		'Turn Channel 1 on
		ChannelOn(CHANNEL_1) = True
		ChannelOn(CHANNEL_2) = False
		ChannelOn(MEMORY_AREA) = False
		ChannelOn(MATH_AREA) = False
		
		'Sample Type Options Combo Box
		SAMPLE_TYPE(0) = "Normal"
		SAMPLE_TYPE(1) = "Average"
		SAMPLE_TYPE(2) = "Envelope"
		SAMPLE_TYPE(3) = "Raw Data"
		
		'Root Level - Probe COMP Signal
		MainParam(PROBE_COMP).SetCod = ":BNC "
		
		'Input 1 Coupling
		MainParam(IN1_COUP).SetCod = ":CHAN1:COUP "
		MainParam(IN1_COUP).SetDef = "DC"
		MainParam(IN1_COUP).SetCur = "DC"
		ParamCode(IN1_COUP, 0) = "DC"
		ParamCode(IN1_COUP, 1) = "DCF"
		ParamCode(IN1_COUP, 2) = "AC"
		
		'Input 1 HF Reject
		MainParam(IN1_HFREJ).SetCod = ":CHAN1:HFR "
		MainParam(IN1_HFREJ).SetDef = "OFF"
		MainParam(IN1_HFREJ).SetCur = "OFF"
		ParamCode(IN1_HFREJ, 0) = "OFF"
		ParamCode(IN1_HFREJ, 1) = "ON"
		
		'Input 1 LF Reject
		MainParam(IN1_LFREJ).SetCod = ":CHAN1:LFR "
		MainParam(IN1_LFREJ).SetDef = "OFF"
		MainParam(IN1_LFREJ).SetCur = "OFF"
		ParamCode(IN1_LFREJ, 0) = "OFF"
		ParamCode(IN1_LFREJ, 1) = "ON"
		
		'Input 1 Vertical Offset
		MainParam(IN1_OFFSET).SetCod = ":CHAN1:OFFS "
		MainParam(IN1_OFFSET).SetDef = "0"
		MainParam(IN1_OFFSET).SetCur = "0"
		MainParam(IN1_OFFSET).SetMin = "0"
		MainParam(IN1_OFFSET).SetMax = "50"
		MainParam(IN1_OFFSET).SetUOM = "V"
		MainParam(IN1_OFFSET).SetRes = ""
		MainParam(IN1_OFFSET).SetMinInc = ".01"
		MainParam(IN1_OFFSET).TxtBox = frmZT1428.txtVOffset1
		
		'Input 1 Attenuation factor
		MainParam(IN1_ATTN_FAC).SetCod = ":CHAN1:PROB "
		MainParam(IN1_ATTN_FAC).SetDef = "1"
		MainParam(IN1_ATTN_FAC).SetCur = "1"
		ParamCode(IN1_ATTN_FAC, 0) = "1"
		ParamCode(IN1_ATTN_FAC, 1) = "10"
		ParamCode(IN1_ATTN_FAC, 2) = "100"
		ParamCode(IN1_ATTN_FAC, 3) = "1000"
		
		'Input 1 Vertical Sensitivity
		MainParam(IN1_VSENS).SetCod = ":CHAN1:RANG "
		MainParam(IN1_VSENS).SetDef = "4"
		MainParam(IN1_VSENS).SetCur = "4"
		MainParam(IN1_VSENS).SetMin = ".008"
		MainParam(IN1_VSENS).SetMax = "40"
		MainParam(IN1_VSENS).SetUOM = "V"
		MainParam(IN1_VSENS).SetRes = "D3"
		MainParam(IN1_VSENS).SetMinInc = ".001"
		
		'Input 2 Coupling
		MainParam(IN2_COUP).SetCod = ":CHAN2:COUP "
		MainParam(IN2_COUP).SetDef = "DC"
		MainParam(IN2_COUP).SetCur = "DC"
		ParamCode(IN2_COUP, 0) = "DC"
		ParamCode(IN2_COUP, 1) = "DCF"
		ParamCode(IN2_COUP, 2) = "AC"
		
		'Input 2 HF Reject
		MainParam(IN2_HFREJ).SetCod = ":CHAN2:HFR "
		MainParam(IN2_HFREJ).SetDef = "OFF"
		MainParam(IN2_HFREJ).SetCur = "OFF"
		ParamCode(IN2_HFREJ, 0) = "OFF"
		ParamCode(IN2_HFREJ, 1) = "ON"
		
		'Input 2 LF Reject
		MainParam(IN2_LFREJ).SetCod = ":CHAN2:LFR "
		MainParam(IN2_LFREJ).SetDef = "OFF"
		MainParam(IN2_LFREJ).SetCur = "OFF"
		ParamCode(IN2_LFREJ, 0) = "OFF"
		ParamCode(IN2_LFREJ, 1) = "ON"
		
		'Input 2 Vertical Offset
		MainParam(IN2_OFFSET).SetCod = ":CHAN2:OFFS "
		MainParam(IN2_OFFSET).SetDef = "0"
		MainParam(IN2_OFFSET).SetCur = "0"
		MainParam(IN2_OFFSET).SetMin = "0"
		MainParam(IN2_OFFSET).SetMax = "50"
		MainParam(IN2_OFFSET).SetUOM = "V"
		MainParam(IN2_OFFSET).SetRes = ""
		MainParam(IN2_OFFSET).SetMinInc = ".01"
		MainParam(IN2_OFFSET).TxtBox = frmZT1428.txtVOffset2
		
		'Input 2 Attenuation factor
		MainParam(IN2_ATTN_FAC).SetCod = ":CHAN2:PROB "
		MainParam(IN2_ATTN_FAC).SetDef = "1"
		MainParam(IN2_ATTN_FAC).SetCur = "1"
		ParamCode(IN2_ATTN_FAC, 0) = "1"
		ParamCode(IN2_ATTN_FAC, 1) = "10"
		ParamCode(IN2_ATTN_FAC, 2) = "100"
		ParamCode(IN2_ATTN_FAC, 3) = "1000"
		
		'Input 2 Vertical Sensitivity
		MainParam(IN2_VSENS).SetCod = ":CHAN2:RANG "
		MainParam(IN2_VSENS).SetDef = "4"
		MainParam(IN2_VSENS).SetCur = "4"
		MainParam(IN2_VSENS).SetMin = ".008"
		MainParam(IN2_VSENS).SetMax = "40"
		MainParam(IN2_VSENS).SetUOM = "V"
		MainParam(IN2_VSENS).SetRes = "D3"
		MainParam(IN2_VSENS).SetMinInc = ".001"
		
		'Waveform Points
		MainParam(WF_POINTS).SetCod = ":ACQ:POIN "
		MainParam(WF_POINTS).SetDef = "500"
		MainParam(WF_POINTS).SetCur = "500"
		MainParam(WF_POINTS).SetMin = "500"
		ParamCode(WF_POINTS, 0) = "500"
		ParamCode(WF_POINTS, 1) = "8000"
		
		'Measurement Source
		MainParam(MEAS_SOUR).SetCod = ":MEAS:SOUR "
		MainParam(MEAS_SOUR).SetDef = "CHAN1"
		MainParam(MEAS_SOUR).SetCur = "CHAN1"
		ParamCode(MEAS_SOUR, 0) = "CHAN1"
		ParamCode(MEAS_SOUR, 1) = "CHAN2"
		ParamCode(MEAS_SOUR, 2) = "WMEM1"
		ParamCode(MEAS_SOUR, 3) = "FUNC1"
		
		'Measurement Mode
		MainParam(MEAS_MODE).SetCod = ":MEAS:MODE "
		MainParam(MEAS_MODE).SetDef = "STAN"
		MainParam(MEAS_MODE).SetCur = "STAN"
		ParamCode(MEAS_MODE, 0) = "STAN"
		ParamCode(MEAS_MODE, 1) = "USER"
		
		'User defined measurement parameters added for TIPs support
		MainParam(MEAS_UNIT).SetCod = ":MEAS:UNIT "
		MainParam(MEAS_UNIT).SetDef = "VOLT"
		MainParam(MEAS_UNIT).SetCur = "VOLT"
		ParamCode(MEAS_UNIT, 0) = "VOLT"
		ParamCode(MEAS_UNIT, 1) = "PERC"
		
		'MATH parameters added for TIPs support
		MainParam(FUNC_ADD).SetCod = ":FUNC1:ADD "
		MainParam(FUNC_SUBTRACT).SetCod = ":FUNC1:SUBT "
		MainParam(FUNC_MULTIPLY).SetCod = ":FUNC1:MULT "
		MainParam(FUNC_DIFFERENTIATE).SetCod = ":FUNC1:DIFF "
		MainParam(FUNC_INTEGRATE).SetCod = ":FUNC1:INT "
		MainParam(FUNC_INVERT).SetCod = ":FUNC1:INV "
		
		'NON-COMP GUI parameters added for TIPs support
		MainParam(MEAS_FUNCTION).SetCod = "MEAS~FUNC "
		MainParam(SEGMENT_FILE).SetCod = "FILE~NAME "
		MainParam(TRACE_THICKNESS).SetCod = "TRACE~THICK "
		MainParam(CH1_TRACE_COLOR).SetCod = "CH1~COLOR "
		MainParam(CH2_TRACE_COLOR).SetCod = "CH2~COLOR "
		MainParam(MATH_TRACE_COLOR).SetCod = "MATH~COLOR "
		MainParam(MEMORY_TRACE_COLOR).SetCod = "MEM~COLOR "
		MainParam(FUNC_RANGE).SetCod = "FUNC1~RANGE "
		MainParam(FUNC_OFFSET).SetCod = "FUNC1~OFFSET "
		MainParam(MEM_RANGE).SetCod = "WMEM1~RANGE "
		MainParam(MEM_OFFSET).SetCod = "WMEM1~OFFSET "
		
		MainParam(OUTP_STATE).SetCod = ":OUTP:STAT "
		MainParam(MEAS_LOW).SetCod = ":MEAS:LOW "
		MainParam(MEAS_UPP).SetCod = ":MEAS:UPP "
		MainParam(MEAS_DEF).SetCod = ":MEAS:DEF "
		
		MainParam(ACQ_TYPE).SetCod = ":ACQ:TYPE "
		
		'ECL Trigger 0 State
		MainParam(ECLT0_STATE).SetCod = ":OUTP:ECLT0:STAT "
		MainParam(ECLT0_STATE).SetDef = "OFF"
		MainParam(ECLT0_STATE).SetCur = "OFF"
		ParamCode(ECLT0_STATE, 0) = "OFF"
		ParamCode(ECLT0_STATE, 1) = "ON"
		
		'ECL Trigger 2 State
		MainParam(ECLT1_STATE).SetCod = ":OUTP:ECLT1:STAT "
		MainParam(ECLT1_STATE).SetDef = "OFF"
		MainParam(ECLT1_STATE).SetCur = "OFF"
		ParamCode(ECLT1_STATE, 0) = "OFF"
		ParamCode(ECLT1_STATE, 1) = "ON"
		
		'External Output Trigger State
		MainParam(OUT_TRIG_STATE).SetCod = ":OUTP:EXT:STAT "
		MainParam(OUT_TRIG_STATE).SetDef = "OFF"
		MainParam(OUT_TRIG_STATE).SetCur = "OFF"
		ParamCode(OUT_TRIG_STATE, 0) = "OFF"
		ParamCode(OUT_TRIG_STATE, 1) = "ON"
		
		'Timebase Reference Delay
		MainParam(REF_DELAY).SetCod = ":TIM:DEL "
		MainParam(REF_DELAY).SetDef = "0"
		MainParam(REF_DELAY).SetCur = "0"
		MainParam(REF_DELAY).SetMin = "-16E-3"
		MainParam(REF_DELAY).SetMax = "1"
		MainParam(REF_DELAY).SetUOM = "S"
		MainParam(REF_DELAY).SetRes = "D1"
		MainParam(REF_DELAY).SetMinInc = ".00000000001"
		MainParam(REF_DELAY).TxtBox = frmZT1428.txtDelay
		
		'Acqusition Mode
		MainParam(ACQ_MODE).SetCod = ":TIM:MODE "
		MainParam(ACQ_MODE).SetDef = "AUTO"
		MainParam(ACQ_MODE).SetCur = "AUTO"
		ParamCode(ACQ_MODE, 0) = "AUTO"
		ParamCode(ACQ_MODE, 1) = "TRIG"
		
		'Horizontal Sensitivity
		MainParam(HORZ_SENS).SetCod = ":TIM:RANG "
		MainParam(HORZ_SENS).SetDef = "1E3"
		MainParam(HORZ_SENS).SetCur = "1E3"
		
		'Horizontal Reference Positioning
		MainParam(H_REF_POS).SetCod = ":TIM:REF "
		MainParam(H_REF_POS).SetDef = "CENT"
		MainParam(H_REF_POS).SetCur = "CENT"
		ParamCode(H_REF_POS, 0) = "CENT"
		ParamCode(H_REF_POS, 1) = "LEFT"
		ParamCode(H_REF_POS, 2) = "RIGHT"
		
		'Sample Mode
		MainParam(SAMPLE_MODE).SetCod = ":TIM:SAMP "
		MainParam(SAMPLE_MODE).SetDef = "REAL"
		MainParam(SAMPLE_MODE).SetCur = "REAL"
		ParamCode(SAMPLE_MODE, 0) = "REAL"
		ParamCode(SAMPLE_MODE, 1) = "REP"
		
		'Average count
		MainParam(AVR_COUNT).SetCod = ":ACQ:COUN "
		MainParam(AVR_COUNT).SetDef = "8"
		MainParam(AVR_COUNT).SetCur = "8"
		MainParam(AVR_COUNT).SetMin = "1"
		MainParam(AVR_COUNT).SetMax = "2048"
		MainParam(AVR_COUNT).SetUOM = ""
		MainParam(AVR_COUNT).SetRes = "A0"
		MainParam(AVR_COUNT).SetMinInc = "1"
		MainParam(AVR_COUNT).TxtBox = frmZT1428.txtAvgCount
		
		'Completion Percentage
		MainParam(COMPLETION).SetCod = ":ACQ:COMP "
		MainParam(COMPLETION).SetDef = "100"
		MainParam(COMPLETION).SetCur = "100"
		MainParam(COMPLETION).SetMin = "0"
		MainParam(COMPLETION).SetMax = "100"
		MainParam(COMPLETION).SetUOM = "%"
		MainParam(COMPLETION).SetRes = "D0"
		MainParam(COMPLETION).SetMinInc = "1"
		MainParam(COMPLETION).TxtBox = frmZT1428.txtCompletion
		
		'Trigger Mode
		MainParam(TRIG_MODE).SetCod = ":TRIG:MODE "
		MainParam(TRIG_MODE).SetDef = "EDGE"
		MainParam(TRIG_MODE).SetCur = "EDGE"
		ParamCode(TRIG_MODE, 0) = "EDGE"
		ParamCode(TRIG_MODE, 1) = "DEL"
		ParamCode(TRIG_MODE, 2) = "TV"
		
		'Trigger Source
		MainParam(TRIG_SOUR).SetCod = ":TRIG:SOUR "
		MainParam(TRIG_SOUR).SetDef = "CHAN1"
		MainParam(TRIG_SOUR).SetCur = ""
		ParamCode(TRIG_SOUR, 0) = "CHAN1"
		ParamCode(TRIG_SOUR, 1) = "CHAN2"
		ParamCode(TRIG_SOUR, 2) = "ECLT0"
		ParamCode(TRIG_SOUR, 3) = "ECLT1"
		ParamCode(TRIG_SOUR, 4) = "EXT"
		
		'Trigger Level
		MainParam(TRIG_LEV).SetCod = ":TRIG:LEV "
		MainParam(TRIG_LEV).SetDef = "0"
		MainParam(TRIG_LEV).SetCur = "0"
		MainParam(TRIG_LEV).SetMin = "-5.5"
		MainParam(TRIG_LEV).SetMax = "5.5"
		MainParam(TRIG_LEV).SetUOM = "V"
		MainParam(TRIG_LEV).SetRes = "D3"
		MainParam(TRIG_LEV).SetMinInc = ".00001"
		MainParam(TRIG_LEV).TxtBox = frmZT1428.txtTrigLevel
		
		'Trigger Polarity/Slope
		MainParam(TRIG_SLOPE).SetCod = ":TRIG:SLOP "
		MainParam(TRIG_SLOPE).SetDef = "POS"
		MainParam(TRIG_SLOPE).SetCur = "POS"
		ParamCode(TRIG_SLOPE, 0) = "POS"
		ParamCode(TRIG_SLOPE, 1) = "NEG"
		
		'Trigger Sensitivity
		MainParam(TRIG_SENS).SetCod = ":TRIG:SENS "
		MainParam(TRIG_SENS).SetDef = "NORM"
		MainParam(TRIG_SENS).SetCur = "NORM"
		ParamCode(TRIG_SENS, 0) = "NORM"
		ParamCode(TRIG_SENS, 1) = "LOW"
		
		'Trigger Delay
		MainParam(TRIG_DEL).SetCod = ":TRIG:DEL "
		MainParam(TRIG_DEL).SetDef = "30E-9"
		MainParam(TRIG_DEL).SetCur = "30E-9"
		MainParam(TRIG_DEL).SetMin = "30E-9"
		MainParam(TRIG_DEL).SetMax = ".16"
		MainParam(TRIG_DEL).SetUOM = "S"
		MainParam(TRIG_DEL).SetRes = "D4"
		MainParam(TRIG_DEL).SetMinInc = ""
		MainParam(TRIG_DEL).TxtBox = frmZT1428.txtDelEvents
		ParamCode(TRIG_DEL, 0) = "TIME, "
		ParamCode(TRIG_DEL, 1) = "EVENT, "
		
		'Trigger Delay Slope
		MainParam(TRIG_DEL_SLOPE).SetCod = ":TRIG:DEL:SLOP "
		MainParam(TRIG_DEL_SLOPE).SetDef = "POS"
		MainParam(TRIG_DEL_SLOPE).SetCur = "POS"
		ParamCode(TRIG_DEL_SLOPE, 0) = "POS"
		ParamCode(TRIG_DEL_SLOPE, 1) = "NEG"
		
		'Trigger Delay Source
		MainParam(TRIG_DEL_SOUR).SetCod = ":TRIG:DEL:SOUR "
		MainParam(TRIG_DEL_SOUR).SetDef = "CHAN1"
		MainParam(TRIG_DEL_SOUR).SetCur = "CHAN1"
		ParamCode(TRIG_DEL_SOUR, 0) = "CHAN1"
		ParamCode(TRIG_DEL_SOUR, 1) = "CHAN2"
		ParamCode(TRIG_DEL_SOUR, 2) = "ECLT0"
		ParamCode(TRIG_DEL_SOUR, 3) = "ECLT1"
		ParamCode(TRIG_DEL_SOUR, 4) = "EXT"
		
		'Trigger Occurrence
		MainParam(TRIG_OCC).SetCod = ":TRIG:OCC "
		MainParam(TRIG_OCC).SetDef = "1"
		MainParam(TRIG_OCC).SetCur = "1"
		MainParam(TRIG_OCC).SetMin = "1"
		MainParam(TRIG_OCC).SetMax = "16E6"
		MainParam(TRIG_OCC).SetUOM = ""
		MainParam(TRIG_OCC).SetRes = "D0"
		MainParam(TRIG_OCC).SetMinInc = "1"
		MainParam(TRIG_OCC).TxtBox = frmZT1428.txtOccurrence
		
		'TV Standard (for TV Trigger)
		MainParam(TRIG_STAND).SetCod = ":TRIG:STAN "
		MainParam(TRIG_STAND).SetDef = "525"
		MainParam(TRIG_STAND).SetCur = "525"
		ParamCode(TRIG_STAND, 0) = "525"
		ParamCode(TRIG_STAND, 1) = "625"
		
		'TV Field (for TV Trigger)
		MainParam(TRIG_FIELD).SetCod = ":TRIG:FIEL "
		MainParam(TRIG_FIELD).SetDef = "1"
		MainParam(TRIG_FIELD).SetCur = "1"
		ParamCode(TRIG_FIELD, 0) = "1"
		ParamCode(TRIG_FIELD, 1) = "2"
		
		'TV Field (for TV Trigger)
		MainParam(TRIG_LINE).SetCod = ":TRIG:LINE "
		MainParam(TRIG_LINE).SetDef = "1"
		MainParam(TRIG_LINE).SetCur = "1"
		MainParam(TRIG_LINE).SetMin = "1"
		MainParam(TRIG_LINE).SetMax = "263"
		MainParam(TRIG_LINE).SetUOM = ""
		MainParam(TRIG_LINE).SetRes = "D0"
		MainParam(TRIG_LINE).SetMinInc = "1"
		MainParam(TRIG_LINE).TxtBox = frmZT1428.txtLine
		
		'External Trigger Coupling
		MainParam(EXT_TRIG_COU).SetCod = ":TRIG:COUP "
		MainParam(EXT_TRIG_COU).SetDef = "DC"
		MainParam(EXT_TRIG_COU).SetCur = "DC"
		ParamCode(EXT_TRIG_COU, 0) = "DC"
		ParamCode(EXT_TRIG_COU, 1) = "DCF"
		
		'Trigger Hold Off
		MainParam(HOLD_OFF).SetCod = ":TRIG:HOLD "
		MainParam(HOLD_OFF).SetDef = HOLD_MIN_TIME
		MainParam(HOLD_OFF).SetCur = HOLD_MIN_TIME
		MainParam(HOLD_OFF).SetMin = HOLD_MIN_TIME
		MainParam(HOLD_OFF).SetMax = HOLD_MAX_TIME
		MainParam(HOLD_OFF).SetUOM = "S"
		MainParam(HOLD_OFF).SetRes = "D4"
		MainParam(HOLD_OFF).SetMinInc = ""
		MainParam(HOLD_OFF).TxtBox = frmZT1428.txtHoldOff
		ParamCode(HOLD_OFF, 0) = "TIME, "
		ParamCode(HOLD_OFF, 1) = "EVENT, "
		
		'Function Range
		MainParam(FUNC_RANGE).SetDef = ".4"
		MainParam(FUNC_RANGE).SetCur = ".4"
		MainParam(FUNC_RANGE).SetMin = "1E-12"
		MainParam(FUNC_RANGE).SetMax = "1E13"
		MainParam(FUNC_RANGE).SetUOM = "V"
		MainParam(FUNC_RANGE).SetRes = "D4"
		MainParam(FUNC_RANGE).SetMinInc = "1E-12"
		MainParam(FUNC_RANGE).TxtBox = frmZT1428.txtMathRange
		
		'Function Offset
		MainParam(FUNC_OFFSET).SetDef = "0"
		MainParam(FUNC_OFFSET).SetCur = "0"
		MainParam(FUNC_OFFSET).SetMin = "-1E13"
		MainParam(FUNC_OFFSET).SetMax = "1E13"
		MainParam(FUNC_OFFSET).SetUOM = "V"
		MainParam(FUNC_OFFSET).SetRes = "D4"
		MainParam(FUNC_OFFSET).SetMinInc = "1E-12"
		MainParam(FUNC_OFFSET).TxtBox = frmZT1428.txtMathVOffset
		
		'Memory Range
		MainParam(MEM_RANGE).SetDef = "4"
		MainParam(MEM_RANGE).SetCur = "4"
		MainParam(MEM_RANGE).SetMin = "1E-12"
		MainParam(MEM_RANGE).SetMax = "1E13"
		MainParam(MEM_RANGE).SetUOM = "V"
		MainParam(MEM_RANGE).SetRes = "D4"
		MainParam(MEM_RANGE).SetMinInc = "1E-12"
		MainParam(MEM_RANGE).TxtBox = frmZT1428.txtMemoryRange
		
		'Memory Offset
		MainParam(MEM_OFFSET).SetDef = "0"
		MainParam(MEM_OFFSET).SetCur = "0"
		MainParam(MEM_OFFSET).SetMin = "-1E13"
		MainParam(MEM_OFFSET).SetMax = "1E13"
		MainParam(MEM_OFFSET).SetUOM = "V"
		MainParam(MEM_OFFSET).SetRes = "D4"
		MainParam(MEM_OFFSET).SetMinInc = "1E-12"
		MainParam(MEM_OFFSET).TxtBox = frmZT1428.txtMemVoltOffset
		
		'Measurement arrays
		MeasureCode(DC_RMS).SetCod = "MEAS:VDCR?"
		MeasureCode(DC_RMS).SetUOM = "V"
		MeasureCode(DC_RMS).SetRes = "D4"
		
		MeasureCode(AC_RMS).SetCod = "MEAS:VACR?"
		MeasureCode(AC_RMS).SetUOM = "V"
		MeasureCode(AC_RMS).SetRes = "D4"
		
		MeasureCode(PERIOD).SetCod = "MEAS:PER?"
		MeasureCode(PERIOD).SetUOM = "S"
		MeasureCode(PERIOD).SetRes = "D6"
		
		MeasureCode(FREQUENCY).SetCod = "MEAS:FREQ?"
		MeasureCode(FREQUENCY).SetUOM = "Hz"
		MeasureCode(FREQUENCY).SetRes = "D6"
		
		MeasureCode(POS_PWIDTH).SetCod = "MEAS:PWID?"
		MeasureCode(POS_PWIDTH).SetUOM = "S"
		MeasureCode(POS_PWIDTH).SetRes = "D6"
		
		MeasureCode(NEG_PWIDTH).SetCod = "MEAS:NWID?"
		MeasureCode(NEG_PWIDTH).SetUOM = "S"
		MeasureCode(NEG_PWIDTH).SetRes = "D6"
		
		MeasureCode(DUTY_CYCLE).SetCod = "MEAS:DUT?"
		MeasureCode(DUTY_CYCLE).SetUOM = "%"
		MeasureCode(DUTY_CYCLE).SetRes = "N2"
		
		MeasureCode(DELAY_MEAS).SetCod = "MEAS:DEL?"
		MeasureCode(DELAY_MEAS).SetUOM = "S"
		MeasureCode(DELAY_MEAS).SetRes = "D6"
		
		MeasureCode(RISE_TIME).SetCod = "MEAS:RIS?"
		MeasureCode(RISE_TIME).SetUOM = "S"
		MeasureCode(RISE_TIME).SetRes = "D6"
		
		MeasureCode(FALL_TIME).SetCod = "MEAS:FALL?"
		MeasureCode(FALL_TIME).SetUOM = "S"
		MeasureCode(FALL_TIME).SetRes = "D6"
		
		MeasureCode(PRE_SHOOT).SetCod = "MEAS:PRES?"
		MeasureCode(PRE_SHOOT).SetUOM = "%"
		MeasureCode(PRE_SHOOT).SetRes = "N1"
		
		MeasureCode(OVR_SHOOT).SetCod = "MEAS:OVER?"
		MeasureCode(OVR_SHOOT).SetUOM = "%"
		MeasureCode(OVR_SHOOT).SetRes = "N1"
		
		MeasureCode(V_PP).SetCod = "MEAS:VPP?"
		MeasureCode(V_PP).SetUOM = "V"
		MeasureCode(V_PP).SetRes = "D4"
		
		MeasureCode(V_MIN).SetCod = "MEAS:VMIN?"
		MeasureCode(V_MIN).SetUOM = "V"
		MeasureCode(V_MIN).SetRes = "D4"
		
		MeasureCode(V_MAX).SetCod = "MEAS:VMAX?"
		MeasureCode(V_MAX).SetUOM = "V"
		MeasureCode(V_MAX).SetRes = "D4"
		
		MeasureCode(V_AVERAGE).SetCod = "MEAS:VAV?"
		MeasureCode(V_AVERAGE).SetUOM = "V"
		MeasureCode(V_AVERAGE).SetRes = "D4"
		
		MeasureCode(V_AMPL).SetCod = "MEAS:VAMP?"
		MeasureCode(V_AMPL).SetUOM = "V"
		MeasureCode(V_AMPL).SetRes = "D4"
		
		MeasureCode(V_BASE).SetCod = "MEAS:VBAS?"
		MeasureCode(V_BASE).SetUOM = "V"
		MeasureCode(V_BASE).SetRes = "D4"
		
		MeasureCode(V_TOP).SetCod = "MEAS:VTOP?"
		MeasureCode(V_TOP).SetUOM = "V"
		MeasureCode(V_TOP).SetRes = "D4"
		
		MeasureCode(MEAS_OFF).SetCod = ""
		
		'Horizontal range filler
		ListBoxFiller(HORZ_SENS, 0) = "5 S/DIV" : ParamCode(HORZ_SENS, 0) = "50"
		ListBoxFiller(HORZ_SENS, 1) = "2 S/DIV" : ParamCode(HORZ_SENS, 1) = "20"
		ListBoxFiller(HORZ_SENS, 2) = "1 S/DIV" : ParamCode(HORZ_SENS, 2) = "10"
		ListBoxFiller(HORZ_SENS, 3) = "500 mS/DIV" : ParamCode(HORZ_SENS, 3) = "5"
		ListBoxFiller(HORZ_SENS, 4) = "200 mS/DIV" : ParamCode(HORZ_SENS, 4) = "2"
		ListBoxFiller(HORZ_SENS, 5) = "100 mS/DIV" : ParamCode(HORZ_SENS, 5) = "1"
		ListBoxFiller(HORZ_SENS, 6) = "50 mS/DIV" : ParamCode(HORZ_SENS, 6) = ".5"
		ListBoxFiller(HORZ_SENS, 7) = "20 mS/DIV" : ParamCode(HORZ_SENS, 7) = ".2"
		ListBoxFiller(HORZ_SENS, 8) = "10 mS/DIV" : ParamCode(HORZ_SENS, 8) = ".1"
		ListBoxFiller(HORZ_SENS, 9) = "5 mS/DIV" : ParamCode(HORZ_SENS, 9) = ".05"
		ListBoxFiller(HORZ_SENS, 10) = "2 mS/DIV" : ParamCode(HORZ_SENS, 10) = ".02"
		ListBoxFiller(HORZ_SENS, 11) = "1 mS/DIV" : ParamCode(HORZ_SENS, 11) = ".01"
		ListBoxFiller(HORZ_SENS, 12) = "500 uS/DIV" : ParamCode(HORZ_SENS, 12) = "5E-3"
		ListBoxFiller(HORZ_SENS, 13) = "200 uS/DIV" : ParamCode(HORZ_SENS, 13) = "2E-3"
		ListBoxFiller(HORZ_SENS, 14) = "100 uS/DIV" : ParamCode(HORZ_SENS, 14) = "1E-3"
		ListBoxFiller(HORZ_SENS, 15) = "50 uS/DIV" : ParamCode(HORZ_SENS, 15) = "5E-4"
		ListBoxFiller(HORZ_SENS, 16) = "20 uS/DIV" : ParamCode(HORZ_SENS, 16) = "2E-4"
		ListBoxFiller(HORZ_SENS, 17) = "10 uS/DIV" : ParamCode(HORZ_SENS, 17) = "1E-4"
		ListBoxFiller(HORZ_SENS, 18) = "5 uS/DIV" : ParamCode(HORZ_SENS, 18) = "5E-5"
		ListBoxFiller(HORZ_SENS, 19) = "2 uS/DIV" : ParamCode(HORZ_SENS, 19) = "2E-5"
		ListBoxFiller(HORZ_SENS, 20) = "1 uS/DIV" : ParamCode(HORZ_SENS, 20) = "1E-5"
		ListBoxFiller(HORZ_SENS, 21) = "500 nS/DIV" : ParamCode(HORZ_SENS, 21) = "5E-6"
		ListBoxFiller(HORZ_SENS, 22) = "200 nS/DIV" : ParamCode(HORZ_SENS, 22) = "2E-6"
		ListBoxFiller(HORZ_SENS, 23) = "100 nS/DIV" : ParamCode(HORZ_SENS, 23) = "1E-6"
		ListBoxFiller(HORZ_SENS, 24) = "50 nS/DIV" : ParamCode(HORZ_SENS, 24) = "5E-7"
		ListBoxFiller(HORZ_SENS, 25) = "20 nS/DIV" : ParamCode(HORZ_SENS, 25) = "2E-7"
		ListBoxFiller(HORZ_SENS, 26) = "10 nS/DIV" : ParamCode(HORZ_SENS, 26) = "1E-7"
		ListBoxFiller(HORZ_SENS, 27) = "5 nS/DIV" : ParamCode(HORZ_SENS, 27) = "5E-8"
		ListBoxFiller(HORZ_SENS, 28) = "2 nS/DIV" : ParamCode(HORZ_SENS, 28) = "2E-8"
		ListBoxFiller(HORZ_SENS, 29) = "1 nS/DIV" : ParamCode(HORZ_SENS, 29) = "1E-8"
		
		'Horizontal range filler
		ListBoxFiller(PNT8000_HLIST, 0) = "80 S/DIV" : ParamCode(PNT8000_HLIST, 0) = "80E+1"
		ListBoxFiller(PNT8000_HLIST, 1) = "32 S/DIV" : ParamCode(PNT8000_HLIST, 1) = "32E+1"
		ListBoxFiller(PNT8000_HLIST, 2) = "16 S/DIV" : ParamCode(PNT8000_HLIST, 2) = "16E+1"
		ListBoxFiller(PNT8000_HLIST, 3) = "8 S/DIV" : ParamCode(PNT8000_HLIST, 3) = "8E+1"
		ListBoxFiller(PNT8000_HLIST, 4) = "3.2 S/DIV" : ParamCode(PNT8000_HLIST, 4) = "3.2E+1"
		ListBoxFiller(PNT8000_HLIST, 5) = "1.6 S/DIV" : ParamCode(PNT8000_HLIST, 5) = "1.6E+1"
		ListBoxFiller(PNT8000_HLIST, 6) = "800 mS/DIV" : ParamCode(PNT8000_HLIST, 6) = "800E-2"
		ListBoxFiller(PNT8000_HLIST, 7) = "320 mS/DIV" : ParamCode(PNT8000_HLIST, 7) = "320E-2"
		ListBoxFiller(PNT8000_HLIST, 8) = "160 mS/DIV" : ParamCode(PNT8000_HLIST, 8) = "160E-2"
		ListBoxFiller(PNT8000_HLIST, 9) = "80 mS/DIV" : ParamCode(PNT8000_HLIST, 9) = "80E-2"
		ListBoxFiller(PNT8000_HLIST, 10) = "32 mS/DIV" : ParamCode(PNT8000_HLIST, 10) = "32E-2"
		ListBoxFiller(PNT8000_HLIST, 11) = "16 mS/DIV" : ParamCode(PNT8000_HLIST, 11) = "16E-2"
		ListBoxFiller(PNT8000_HLIST, 12) = "8 mS/DIV" : ParamCode(PNT8000_HLIST, 12) = "8E-2"
		ListBoxFiller(PNT8000_HLIST, 13) = "3.2 mS/DIV" : ParamCode(PNT8000_HLIST, 13) = "3.2E-2"
		ListBoxFiller(PNT8000_HLIST, 14) = "1.6 mS/DIV" : ParamCode(PNT8000_HLIST, 14) = "1.6E-2"
		ListBoxFiller(PNT8000_HLIST, 15) = "800 uS/DIV" : ParamCode(PNT8000_HLIST, 15) = "800E-5"
		ListBoxFiller(PNT8000_HLIST, 16) = "320 uS/DIV" : ParamCode(PNT8000_HLIST, 16) = "320E-5"
		ListBoxFiller(PNT8000_HLIST, 17) = "160 uS/DIV" : ParamCode(PNT8000_HLIST, 17) = "160E-5"
		ListBoxFiller(PNT8000_HLIST, 18) = "80 uS/DIV" : ParamCode(PNT8000_HLIST, 18) = "80E-5"
		ListBoxFiller(PNT8000_HLIST, 19) = "32 uS/DIV" : ParamCode(PNT8000_HLIST, 19) = "32E-5"
		ListBoxFiller(PNT8000_HLIST, 20) = "16 uS/DIV" : ParamCode(PNT8000_HLIST, 20) = "16E-5"
		ListBoxFiller(PNT8000_HLIST, 21) = "8 uS/DIV" : ParamCode(PNT8000_HLIST, 21) = "8E-5"
		ListBoxFiller(PNT8000_HLIST, 22) = "3.2 uS/DIV" : ParamCode(PNT8000_HLIST, 22) = "3.2E-5"
		ListBoxFiller(PNT8000_HLIST, 23) = "1.6 uS/DIV" : ParamCode(PNT8000_HLIST, 23) = "1.6E-5"
		ListBoxFiller(PNT8000_HLIST, 24) = "800 nS/DIV" : ParamCode(PNT8000_HLIST, 24) = "800E-8"
		ListBoxFiller(PNT8000_HLIST, 25) = "320 nS/DIV" : ParamCode(PNT8000_HLIST, 25) = "320E-8"
		ListBoxFiller(PNT8000_HLIST, 26) = "160 nS/DIV" : ParamCode(PNT8000_HLIST, 26) = "160E-8"
		ListBoxFiller(PNT8000_HLIST, 27) = "80 nS/DIV" : ParamCode(PNT8000_HLIST, 27) = "80E-8"
		ListBoxFiller(PNT8000_HLIST, 28) = "32 nS/DIV" : ParamCode(PNT8000_HLIST, 28) = "32E-8"
		ListBoxFiller(PNT8000_HLIST, 29) = "16 nS/DIV" : ParamCode(PNT8000_HLIST, 29) = "16E-8"
		'Vertical Range Boxes
		VSenseBase(0) = 4
		VSenseBase(1) = 2
		VSenseBase(2) = 1
		VSenseBase(3) = 0.5
		VSenseBase(4) = 0.2
		VSenseBase(5) = 0.1
		VSenseBase(6) = 0.05
		VSenseBase(7) = 0.02
		VSenseBase(8) = 0.01
		VSenseBase(9) = 0.005
		VSenseBase(10) = 0.002
		VSenseBase(11) = 0.001
		VSenseBase(12) = 0.0008
		
		FillVSense(CHANNEL_1, -1)
		FillVSense(CHANNEL_2, -1)
		
		'Trigger/Delay sources
		ListBoxFiller(TRIG_SOUR, 0) = "Input 1"
		ListBoxFiller(TRIG_SOUR, 1) = "Input 2"
		ListBoxFiller(TRIG_SOUR, 2) = "ECLT0"
		ListBoxFiller(TRIG_SOUR, 3) = "ECLT1"
		ListBoxFiller(TRIG_SOUR, 4) = "EXT"
		
		'Measurment Sources
		ListBoxFiller(MEAS_SOUR, 0) = "Input 1"
		ListBoxFiller(MEAS_SOUR, 1) = "Input 2"
		ListBoxFiller(MEAS_SOUR, 2) = "Memory"
		ListBoxFiller(MEAS_SOUR, 3) = "Math"
		
		'Delay Measurement Array set-up
		DelMeasParam(START_SOURCE, 0) = "CHAN1"
		DelMeasParam(START_SOURCE, 1) = "CHAN2"
		DelMeasParam(START_SOURCE, 2) = "WMEM1"
		DelMeasParam(START_SOURCE, 3) = "FUNC1"
		
		DelMeasParam(STOP_SOURCE, 0) = "CHAN1"
		DelMeasParam(STOP_SOURCE, 1) = "CHAN2"
		DelMeasParam(STOP_SOURCE, 2) = "WMEM1"
		DelMeasParam(STOP_SOURCE, 3) = "FUNC1"
		
		DelMeasParam(START_SLOPE, 0) = "POS"
		DelMeasParam(START_SLOPE, 1) = "NEG"
		
		DelMeasParam(STOP_SLOPE, 0) = "POS"
		DelMeasParam(STOP_SLOPE, 1) = "NEG"
		
		DelMeasParam(START_LEVEL, 0) = "VOLT"
		DelMeasParam(START_LEVEL, 1) = "PERC"
		
		'Channel Array
		ChannelParam(0) = "CHAN1"
		ChannelParam(1) = "CHAN2"
		ChannelParam(2) = "WMEM1"
		
		SetHScaleGraph()
	End Sub
	
	
	'UPGRADE_WARNING: Application will terminate when Sub Main() finishes. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
	Public Sub Main()
		'*****************************************************************
		'* Nomenclature   : HP E1428A Digitizing Oscilloscope Front Panel*
		'* Written By     : Michael McCabe                               *
		'*    DESCRIPTION:                                               *
		'*     This module is the program entry (starting) point.        *
		'*****************************************************************
		
		Dim temp As Integer
		Dim VMEAddress As Integer
		Dim sCmdLine As String
		Dim lpReturnedString As New VB6.FixedLengthString(255)
		Dim nReturnValue As Integer
		Dim bShowSAIS As Boolean
		Dim sStrCMD As Object
		Dim sStrCMD2 As String
		Dim x As Object
		Dim y As Short
		Dim sCurOffset As String
		
		frmParent = frmZT1428
		'UPGRADE_ISSUE: App property App.PrevInstance was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		If App.PrevInstance Then End
		' initialize the quit variable
		QuitProgram = False
		'Get command line arguments to determine if invoked by TIP Studio
		sCmdLine = VB.Command()
		sTIP_Mode = Trim(UCase(sCmdLine))
		bShowSAIS = True 'Set default to show GUI
		If InStr(sTIP_Mode, "TIP") Then
			bTIP_Running = True
			ResetScope()
			If sTIP_Mode = "TIP_MEASONLY" Then
				'sTIP_Mode = "TIP_RUNSETUP"
				bShowSAIS = False 'Don't show GUI if called by NAM to do measurement only
			End If
		Else
			SplashStart() 'Show splash screen if open from toolbar
		End If
		
		'Check System and Instrument For Errors
		VerifyInstrumentCommunication(RESOURCE_NAME)
		'mh WriteMsg "SYST:LANG COMP"
		Delay(1)
		
		GetMemorySpace(temp)
		VMEAddress = temp
		'mh WriteMsg "MEM:VME:ADDR #H" + Trim$(Hex$(VMEAddress&))
		
		bInstrumentMode = True
		
		InitializeArrays()
		
		'Find Windows Directory
		nReturnValue = GetWindowsDirectory(lpReturnedString.Value, 255)
		sWindowsDir = Left(lpReturnedString.Value, nReturnValue)
		
		If bTIP_Running Then
			'Position SAIS in upper Righthand corner of screen
			frmZT1428.Top = 0
			frmZT1428.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) - VB6.PixelsToTwipsX(frmZT1428.Width))
			frmZT1428.cmdUpdateTIP.Text = "&Update TIP" 'ScopeNAM support, DJoiner 05/27/03
		Else
			CenterForm(frmZT1428)
			frmZT1428.cmdUpdateTIP.Text = "&NAM File" 'ScopeNAM support, DJoiner 05/27/03
		End If
		
		
		FillListBoxes()
		FillTextBoxes()
		
		
		
		frmZT1428.StatusBar1.Panels(HORZ_SEN_PAN).Text = "X Axis: " & frmZT1428.cboHorizRange.Text
		frmZT1428.StatusBar1.Panels(CURS_POS_PAN).Text = "Cursor Position = 0,0"
		frmZT1428.tabMainFunctions.SelectedIndex = 0
		frmZT1428.tabMainFunctions.TabPages.Item(WF_TAB).Enabled = False
		frmZT1428.tabMainFunctions.TabPages.Item(2).Enabled = False
		frmZT1428.chkLFReject1.Enabled = False 'JHill TDR99151
		frmZT1428.chkLFReject2.Enabled = False 'JHill TDR99151
		'Remove Introduction Form
		If Not bTIP_Running Then SplashEnd()
		
		'Show the Front Panel unless invoked by ScpeNAM in MEAS_ONLY mode
		If bShowSAIS Then frmZT1428.Show()
		
		''    'Added in V1.8 to initialize display JCB
		''    frmZT1428.cboHorizRange.ListIndex = 0    What the? This changes the range
		''    frmZT1428.cboHorizRange.ListIndex = HScaleListItem%
		
		'    WriteMsg "MEM:VME:STATE OFF"        'Add to set State to OFF after initialization  DJoiner  01/15/02
		
		If bTIP_Running Then
			'Addded a reset and autorange in order to show the wavefrom currently being input on channel 1 or 2.  DME PCR 510 #1.
			ResetScope()
			AutoScaleScope()
			
			MainParam(IN1_OFFSET).SetCur = "0"
			MainParam(IN2_OFFSET).SetCur = "0"
			frmZT1428.txtVOffset1.Text = EngNotate(Val(MainParam(IN1_OFFSET).SetCur), MainParam(IN1_OFFSET))
			frmZT1428.txtVOffset2.Text = EngNotate(Val(MainParam(IN2_OFFSET).SetCur), MainParam(IN2_OFFSET))
			frmZT1428.Refresh()
			
			
			'Inhibit these controls in a TIP Run mode
			frmZT1428.cmdLoadFromDisk.Enabled = False
			frmZT1428.cmdAutoscale.Enabled = False
			'Read, then clear the [TIPS]CMD key
			sTIP_CMDstring = sGetKey("TIPS", "CMD")
			sTIP_CMDstring = Trim(sTIP_CMDstring)
			If sTIP_CMDstring <> "" Then
				SetKey("TIPS", "CMD", "")
				ConfigureFrontPanel()
				
				'added code to send each command seperately rather than all at once to resolve a the -619 error. DME PCR 510 part 3
				If Len(sTIP_CMDstring) > 30 Then
					'UPGRADE_WARNING: Couldn't resolve default property of object sStrCMD. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					sStrCMD = sTIP_CMDstring
					Do 
						y = Len(sStrCMD)
						'UPGRADE_WARNING: Couldn't resolve default property of object sStrCMD. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						x = InStr(1, sStrCMD, ";")
						'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						If x <> 0 Then
							'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							'UPGRADE_WARNING: Couldn't resolve default property of object sStrCMD. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							sStrCMD2 = Mid(sStrCMD, 1, x - 1)
							'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							'UPGRADE_WARNING: Couldn't resolve default property of object sStrCMD. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							sStrCMD = Mid(sStrCMD, x + 1, y)
							WriteMsg(sStrCMD2)
						Else
							'UPGRADE_WARNING: Couldn't resolve default property of object sStrCMD. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
							sStrCMD2 = sStrCMD
							WriteMsg(sStrCMD2)
						End If
						Delay(0.2)
						'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					Loop While x <> 0
				Else
					WriteMsg(sTIP_CMDstring)
				End If
				
				'Acquisition Commands (if applicable) must be sent seperately
				If sTIP_ACQstring <> "" Then
					WriteMsg(sTIP_ACQstring)
				End If
				sTIP_CMDstring = "" 'Now that SAIS has been setup, Clear old data from string
			End If
			
			Select Case sTIP_Mode
				Case "TIP_RUNPERSIST", "TIP_MANUAL"
					frmZT1428.chkContinuous.Value = True
					DigitizeWaveform()
				Case "TIP_DESIGN"
					'Give user full access to controls in design mode
					frmZT1428.cmdUpdateTIP.Visible = True
					frmZT1428.cmdLoadFromDisk.Enabled = True
					frmZT1428.cmdAutoscale.Enabled = True
				Case Else 'Run Setup mode
					DigitizeWaveform()
					If CurrentMeas = MEAS_OFF Then
						SetKey("TIPS", "STATUS", "Ready")
					Else
						Delay(0.5)
						EndProgram()
					End If
			End Select
			
		Else
			frmZT1428.cmdUpdateTIP.Visible = True
			frmZT1428.cmdLoadFromDisk.Enabled = True
			frmZT1428.cmdAutoscale.Enabled = True
			sTIP_CMDstring = "" 'Now that SAIS has been setup, Clear old data from string
		End If
		
		'Set the Refresh rate when in "Debug" mode from 1000 = 1 sec to 25000 = 25 sec
		'UPGRADE_NOTE: Refresh was upgraded to CtlRefresh. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		frmZT1428.Panel_Conifg.CtlRefresh = 5000
		
		frmZT1428.ConfigGetCurrent()
		bInstrumentMode = False
		
		System.Windows.Forms.Application.DoEvents()
		
		'Set the Mode the Instrument is in: Local = True, Debug = False
		If iStatus <> 1 Then
			frmZT1428.Panel_Conifg.SetDebugStatus(True)
		Else
			frmZT1428.Panel_Conifg.SetDebugStatus(False)
		End If
		
	End Sub
	
	
	Sub SpinParam(ByRef ParamIndex As Short, ByRef Direction As String)
		'Declare Procedure Level variables
		Dim AvgCnt As Short
		Dim LogAvgCnt As Short
		Dim SpnVal As Single
		
		Select Case ParamIndex
			Case START_EDGE
				SpinAbs(Direction, StartEdge, 1)
			Case START_LEVEL
				SpinAbs(Direction, StartLevel, 1)
			Case STOP_EDGE
				SpinAbs(Direction, StopEdge, 1)
			Case STOP_LEVEL
				SpinAbs(Direction, StopLevel, 1)
			Case REF_DELAY, IN1_OFFSET, IN2_OFFSET, TRIG_LEV, FUNC_RANGE, FUNC_OFFSET, MEM_RANGE, MEM_OFFSET
				Spin10Pct(Direction, MainParam(ParamIndex))
			Case COMPLETION, TRIG_LINE, TRIG_OCC
				SpinAbs(Direction, MainParam(ParamIndex), 1)
			Case AVR_COUNT
				AvgCnt = Val(MainParam(AVR_COUNT).SetCur)
				If Direction = "DOWN" Then
					If AvgCnt <> 0 Then
						LogAvgCnt = Int(System.Math.Log(AvgCnt) / System.Math.Log(2))
						SpnVal = 2 ^ (LogAvgCnt - 1)
					End If
				Else
					SpnVal = AvgCnt
				End If
				SpinAbs(Direction, MainParam(ParamIndex), SpnVal)
			Case HOLD_OFF, TRIG_DEL
				If MainParam(ParamIndex).SetUOM = "" Then
					SpinAbs(Direction, MainParam(ParamIndex), 1)
				Else
					Spin10Pct(Direction, MainParam(ParamIndex))
				End If
			Case Else
		End Select
		System.Windows.Forms.Application.DoEvents()
		RegisterChange(ParamIndex)
		
	End Sub
	
	
	Sub UpdateCursorCaption(ByRef XPos As Double, ByRef YPos As Double)
		'************************************************************
		'* ManTech Test Systems Software Module                     *
		'************************************************************
		'* Nomenclature   : HP E1428A Oscilloscope Front Panel      *
		'* Written By     : Michael McCabe                          *
		'*    DESCRIPTION:                                          *
		'*     Changes the status bar text to reflect the present   *
		'*     position of the cursor                               *
		'*    EXAMPLE:                                              *
		'*     UpdateCursorCaption(XPos#, YPos#)                    *
		'************************************************************
		Dim XAxisPos As String
		Dim YAxisPos As String
		Static Data As InstParameter
		
		Data.SetMin = "-1000"
		Data.SetMax = "1000"
		
		Data.SetUOM = "S"
		Data.SetRes = "D3"
		XAxisPos = EngNotate(XPos, Data)
		
		Data.SetUOM = "V"
		Data.SetRes = "A2"
		YAxisPos = EngNotate(YPos, Data)
		frmZT1428.StatusBar1.Panels(CURS_POS_PAN).Text = "Cursor Position = " & XAxisPos & "," & YAxisPos
		If bTIP_Running Then
			SetKey("TIPS", "DScope_Cursor_Time", CStr(XPos))
			SetKey("TIPS", "DScope_Cursor_Volt", CStr(YPos))
		End If
		
	End Sub
	
	
	Sub StatusBarMessage(ByRef MessageString As String)
		
		If frmZT1428.StatusBar1.Panels(3).Text <> MessageString Then
			frmZT1428.StatusBar1.Panels(3).Text = MessageString
		End If
		
	End Sub
	
	Private Sub ConfigureFrontPanel()
		'DESCRIPTION:
		'   This Module configures the SAIS in TIP Mode based on the settings of a
		'   saved command string.
		'EXAMPLE:
		'   ConfigureFrontPanel
		
		Dim i As Short
		Dim iNumItems As Short
		Dim sCmds() As String
		Dim sGUI_Params() As String
		Const iMAXitems As Short = 4
		
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
		System.Windows.Forms.Application.DoEvents()
		
		'Turn off all traces, ActivateControl will turn on appropriate trace(s)
		frmZT1428.chkDisplayTrace(CHANNEL_1).Value = False
		frmZT1428.chkDisplayTrace(CHANNEL_2).Value = False
		frmZT1428.chkDisplayTrace(MATH_AREA).Value = False
		frmZT1428.chkDisplayTrace(MEMORY_AREA).Value = False
		
		iNumItems = iTIPStrToList(sTIP_CMDstring, 1, sGUI_Params, "|")
		If iNumItems < 1 Or iNumItems > iMAXitems Then
			MsgBox("Invalid Digitizing Scope command string passed from TIP Studio." & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation)
			ResetScope()
			bTIP_Error = True 'Flag to exit saved string setup
			GoTo ExitPoint
		End If
		
		'Define COMP CMD strings (Acquisition Commands and User Defined (DELAY) Measurement
		'Parameters, if applicable, must be sent seperately)
		sTIP_CMDstring = sGUI_Params(2)
		If iNumItems >= 3 Then
			If InStr(sGUI_Params(3), ":ACQ:") Then
				sTIP_ACQstring = sGUI_Params(3)
				If iNumItems >= 4 Then sDelayMeasStr = sGUI_Params(4)
			Else
				sDelayMeasStr = sGUI_Params(3)
				sTIP_ACQstring = ""
			End If
		End If
		
		For i = 1 To iTIPStrToList(sGUI_Params(1), 1, sCmds, ";")
			ActivateControl(sCmds(i))
			If bTIP_Error Then GoTo ExitPoint
		Next i
		
		'The first part of COMP CMD string is in sGUI_Params(2)
		For i = 1 To iTIPStrToList(sGUI_Params(2), 1, sCmds, ";")
			ActivateControl(sCmds(i))
			If bTIP_Error Then GoTo ExitPoint
		Next i
		
		'ACQuisition string is in sGUI_Params(3)
		If iNumItems >= 3 Then
			For i = 1 To iTIPStrToList(sGUI_Params(3), 1, sCmds, ";")
				ActivateControl(sCmds(i))
				If bTIP_Error Then GoTo ExitPoint
			Next i
		End If
		
		'If applicable, Delay Measurement string is in sGUI_Params(4)
		If iNumItems >= 4 Then
			For i = 1 To iTIPStrToList(sGUI_Params(4), 1, sCmds, ";")
				ActivateControl(sCmds(i))
				If bTIP_Error Then GoTo ExitPoint
			Next i
		End If
		
		'Put updated values into text boxes
		FillTextBoxes()
		If frmZT1428.chkDisplayTrace(MATH_AREA).Value = True Then
			SetMathFunction(True) 'Required to display Math trace
		End If
		
ExitPoint: 
		'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
		System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
		'Enable user to modify instrument settings if in design mode
		If (sTIP_Mode = "TIP_DESIGN") Or (sTIP_Mode = "TIP_MANUAL") Then
			bDoNotTalk = False
		Else
			bDoNotTalk = True
		End If
		System.Windows.Forms.Application.DoEvents()
		
	End Sub
	
	Public Sub ActivateControl(ByRef sCOMPcmd As String)
		'DESCRIPTION:
		'   Sets a panel control to the state indicated by the argument of the
		'   HP COMP command in the sCOMPcmd string argument. It simulates the action
		'   of a user selecting the control except that the actual commands are not
		'   sent to the instrument.
		'PARAMETERS:
		'   sCOMPcmd:  A single HP Compatible (COMP) command with argument(s) if any.
		
		Dim i As Short
		Dim iNumItems As Short
		Dim iIndex As Short
		Dim sCmd As String
		Dim sArg As String
		Dim sArgList() As String
		Dim sErrMsg As String
		Dim nRange As Single
		Dim lTraceParam As Integer
		
		sErrMsg = ""
		lTraceParam = -1
		'An invalid argument can cause an overflow error in certain cases
		On Error Resume Next
		
		'Filter out blank sends caused by redundant semi-colon character at end of command.
		If Len(sCOMPcmd) < 2 Then Exit Sub
		
		'Filter out IEEE-488.2 (non-COMP) commands. None of these affect the GUI
		'except *RST which is irrelevant because the SAIS and instrument are reset
		'before calling this sub.
		If InStr(sCOMPcmd, "*") Or InStr(sCOMPcmd, "?") > 0 Then Exit Sub
		
		'Parse the command from the argument if it has one.
		If InStr(sCOMPcmd, " ") > 0 Then
			sCmd = Left(sCOMPcmd, InStr(sCOMPcmd, " ")) 'Include space to match MainParam().SetCod
			sArg = Mid(sCOMPcmd, InStr(sCOMPcmd, " ") + 1)
		Else
			sCmd = sCOMPcmd
		End If
		
		'added to fix issues with TETS .dso file TPS settings.
		Select Case sCmd
			Case ":ACQ:POINTS " : sCmd = ":ACQ:POIN "
			Case ":OUTP:STATE " : sCmd = ":OUTP:STAT "
			Case ":OUTP:ECLT0:STATE: " : sCmd = "OUTP:ECLT0:STAT "
			Case ":OUTP:ECLT1:STATE " : sCmd = ":OUTP:ECLT1:STAT "
			Case ":OUTP:EXT:STATE " : sCmd = ":OUTP:EXT:STAT "
			Case ":TRIG:SLOPE " : sCmd = ":TRIG:SLOP "
			Case ":TRIG:FIELD " : sCmd = ":TRIG:FIEL "
		End Select
		
		'Find match to a command index constant
		For i = 1 To MAX_SETTINGS
			If sCmd = MainParam(i).SetCod Then Exit For
		Next i
		If i > MAX_SETTINGS Then
			sErrMsg = "Invalid command: " & sCOMPcmd & " passed from TIP Studio."
		End If
		
		If sErrMsg = "" Then
			
			With frmZT1428
				
				'OK, we have a recognized COMP command. All are listed here but only
				'expected ones have been un-commented for error checking.
				Select Case i
					
					'********** Root Level Commands **********
					Case PROBE_COMP '.SetCod = ":BNC "
						.ribCompON.Value = True
						.imgProbeComp.Visible = True
						WriteMsg("BNC PROB")
						
						'********** Input CHANNEL 1 Options **********
					Case IN1_COUP '.SetCod = ":CHAN1:COUP "
						If Not .chkDisplayTrace(CHANNEL_1).Value Then .chkDisplayTrace(CHANNEL_1).Value = True 'Enable CH1 tab
						Select Case sArg
							Case "DC" : .optCoupling1(0).Value = True
							Case "DCF" : .optCoupling1(1).Value = True
							Case "AC" : .optCoupling1(2).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case IN1_HFREJ '.SetCod = ":CHAN1:HFR "
						If Not .chkDisplayTrace(CHANNEL_1).Value Then .chkDisplayTrace(CHANNEL_1).Value = True 'Enable CH1 tab
						Select Case sArg
							Case "OFF" : .chkHFReject1.Value = False
							Case "ON" : .chkHFReject1.Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case IN1_LFREJ '.SetCod = ":CHAN1:LFR "
						If Not .chkDisplayTrace(CHANNEL_1).Value Then .chkDisplayTrace(CHANNEL_1).Value = True 'Enable CH1 tab
						Select Case sArg
							Case "OFF" : .chkLFReject1.Value = False
							Case "ON" : .chkLFReject1.Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case IN1_OFFSET '.SetCod = ":CHAN1:OFFS "
						If Not .chkDisplayTrace(CHANNEL_1).Value Then .chkDisplayTrace(CHANNEL_1).Value = True 'Enable CH1 tab
						If Val(sArg) < Val(MainParam(IN1_OFFSET).SetMin) Or Val(sArg) > Val(MainParam(IN1_OFFSET).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(IN1_OFFSET).SetCur = sArg 'Sub "FillTextBoxes()" will update
							RegisterChange(IN1_OFFSET) 'Update the scope trace graphic with the new value
						End If
						
					Case IN1_ATTN_FAC '.SetCod = ":CHAN1:PROB "
						If Not .chkDisplayTrace(CHANNEL_1).Value Then .chkDisplayTrace(CHANNEL_1).Value = True 'Enable CH1 tab
						Select Case sArg
							Case "1.00000E+00" : .optProbe1(0).Value = True 'Default
							Case "1.00000E+01" : .optProbe1(1).Value = True
							Case "1.00000E+02" : .optProbe1(2).Value = True
							Case "1.00000E+03" : .optProbe1(3).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						'Reset Vertical range combo box values based upon ATTEN setting
						If sErrMsg = "" And Val(sArg) > 2 Then
							MainParam(IN1_ATTN_FAC).SetCur = sArg
							FillVSense(1, -1)
						End If
						
					Case IN1_VSENS '.SetCod = ":CHAN1:RANG "
						If Not .chkDisplayTrace(CHANNEL_1).Value Then .chkDisplayTrace(CHANNEL_1).Value = True 'Enable CH1 tab
						nRange = Val(sArg) / iAttenCH1 'Calculate the actual range
						Select Case nRange
							Case 40, 4 : .cboVertRange(CHANNEL_1).SelectedIndex = 0
							Case 20 : .cboVertRange(CHANNEL_1).SelectedIndex = 1
							Case 10 : .cboVertRange(CHANNEL_1).SelectedIndex = 2
							Case 5 : .cboVertRange(CHANNEL_1).SelectedIndex = 3
							Case 2 : .cboVertRange(CHANNEL_1).SelectedIndex = 4
							Case 1 : .cboVertRange(CHANNEL_1).SelectedIndex = 5
							Case 0.5 : .cboVertRange(CHANNEL_1).SelectedIndex = 6
							Case 0.2 : .cboVertRange(CHANNEL_1).SelectedIndex = 7
							Case 0.1 : .cboVertRange(CHANNEL_1).SelectedIndex = 8
							Case 0.05 : .cboVertRange(CHANNEL_1).SelectedIndex = 9
							Case 0.02 : .cboVertRange(CHANNEL_1).SelectedIndex = 10
							Case 0.01 : .cboVertRange(CHANNEL_1).SelectedIndex = 11
							Case 0.008 : .cboVertRange(CHANNEL_1).SelectedIndex = 12
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
						'********** Input CHANNEL 2 Options **********
					Case IN2_COUP '.SetCod = ":CHAN2:COUP "
						If Not .chkDisplayTrace(CHANNEL_2).Value Then .chkDisplayTrace(CHANNEL_2).Value = True 'Enable CH2 tab
						Select Case sArg
							Case "DC" : .optCoupling2(0).Value = True
							Case "DCF" : .optCoupling2(1).Value = True
							Case "AC" : .optCoupling2(2).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case IN2_HFREJ '.SetCod = ":CHAN2:HFR "
						If Not .chkDisplayTrace(CHANNEL_2).Value Then .chkDisplayTrace(CHANNEL_2).Value = True 'Enable CH2 tab
						Select Case sArg
							Case "OFF" : .chkHFReject2.Value = False
							Case "ON" : .chkHFReject2.Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case IN2_LFREJ '.SetCod = ":CHAN2:LFR "
						If Not .chkDisplayTrace(CHANNEL_2).Value Then .chkDisplayTrace(CHANNEL_2).Value = True 'Enable CH2 tab
						Select Case sArg
							Case "OFF" : .chkLFReject2.Value = False
							Case "ON" : .chkLFReject2.Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case IN2_OFFSET '.SetCod = ":CHAN2:OFFS "
						If Not .chkDisplayTrace(CHANNEL_2).Value Then .chkDisplayTrace(CHANNEL_2).Value = True 'Enable CH2 tab
						If Val(sArg) < Val(MainParam(IN2_OFFSET).SetMin) Or Val(sArg) > Val(MainParam(IN2_OFFSET).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(IN2_OFFSET).SetCur = sArg 'Sub "FillTextBoxes()" will update
							RegisterChange(IN2_OFFSET) 'Update the scope trace graphic with the new value
						End If
						
					Case IN2_ATTN_FAC '.SetCod = ":CHAN2:PROB "
						If Not .chkDisplayTrace(CHANNEL_2).Value Then .chkDisplayTrace(CHANNEL_2).Value = True 'Enable CH2 tab
						Select Case sArg
							Case "1.00000E+00" : .optProbe2(0).Value = True 'Default
							Case "1.00000E+01" : .optProbe2(1).Value = True
							Case "1.00000E+02" : .optProbe2(2).Value = True
							Case "1.00000E+03" : .optProbe2(3).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						'Reset Vertical range combo box values based upon ATTEN setting
						If sErrMsg = "" And Val(sArg) > 2 Then
							MainParam(IN2_ATTN_FAC).SetCur = sArg
							FillVSense(2, -1)
						End If
						
					Case IN2_VSENS '.SetCod = ":CHAN2:RANG "
						If Not .chkDisplayTrace(CHANNEL_2).Value Then .chkDisplayTrace(CHANNEL_2).Value = True 'Enable CH2 tab
						nRange = Val(sArg) / iAttenCH2 'Calculate the actual range
						Select Case nRange
							Case 40, 4 : .cboVertRange(CHANNEL_2).SelectedIndex = 0
							Case 20 : .cboVertRange(CHANNEL_2).SelectedIndex = 1
							Case 10 : .cboVertRange(CHANNEL_2).SelectedIndex = 2
							Case 5 : .cboVertRange(CHANNEL_2).SelectedIndex = 3
							Case 2 : .cboVertRange(CHANNEL_2).SelectedIndex = 4
							Case 1 : .cboVertRange(CHANNEL_2).SelectedIndex = 5
							Case 0.5 : .cboVertRange(CHANNEL_2).SelectedIndex = 6
							Case 0.2 : .cboVertRange(CHANNEL_2).SelectedIndex = 7
							Case 0.1 : .cboVertRange(CHANNEL_2).SelectedIndex = 8
							Case 0.05 : .cboVertRange(CHANNEL_2).SelectedIndex = 9
							Case 0.02 : .cboVertRange(CHANNEL_2).SelectedIndex = 10
							Case 0.01 : .cboVertRange(CHANNEL_2).SelectedIndex = 11
							Case 0.008 : .cboVertRange(CHANNEL_2).SelectedIndex = 12
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
						'********** OUTput Options **********
					Case ECLT0_STATE '.SetCod = ":OUTP:ECLT0:STATE "
						Select Case sArg
							Case "OFF" : .chkECL0._Value = False
							Case "ON" : .chkECL0._Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case ECLT1_STATE '.SetCod = ":OUTP:ECLT1:STATE "
						Select Case sArg
							Case "OFF" : .chkECL1._Value = False
							Case "ON" : .chkECL1._Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case OUT_TRIG_STATE '.SetCod = ":OUTP:EXT:STATE "
						Select Case sArg
							Case "OFF" : .chkExternalOutput._Value = False
							Case "ON" : .chkExternalOutput._Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case OUTP_STATE '.SetCod = ":OUTP:STATE "
						'No GUI control affected; validity check only
						If sArg <> "ON" And sArg <> "OFF" Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End If
						
						'********** TRIGger Options **********
					Case TRIG_MODE '.SetCod = ":TRIG:MODE "
						Select Case sArg
							Case "EDGE" : .optTrigMode(0).Value = True
							Case "DEL" : .optTrigMode(1).Value = True
							Case "TV" : .optTrigMode(2).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_SOUR '.SetCod = ":TRIG:SOUR "
						Select Case sArg
							Case "CHAN1" : .cboTrigSour.SelectedIndex = 0
							Case "CHAN2" : .cboTrigSour.SelectedIndex = 1
							Case "ECLT0" : .cboTrigSour.SelectedIndex = 2
							Case "ECLT1" : .cboTrigSour.SelectedIndex = 3
							Case "EXT" : .cboTrigSour.SelectedIndex = 4
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_LEV '.SetCod = ":TRIG:LEV "
						If Val(sArg) < Val(MainParam(TRIG_LEV).SetMin) Or Val(sArg) > Val(MainParam(TRIG_LEV).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(TRIG_LEV).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case TRIG_SLOPE '.SetCod = ":TRIG:SLOP "
						Select Case sArg
							Case "POS" : .optTrigSlope(0).Value = True
							Case "NEG" : .optTrigSlope(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_SENS '.SetCod = ":TRIG:SENS "
						Select Case sArg
							Case "NORM" : .optNoiseRej(0).Value = True
							Case "LOW" : .optNoiseRej(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_DEL '.SetCod = ":TRIG:DEL "
						'Parse 2 arguments to process seperately
						iNumItems = iTIPStrToList(sArg, 1, sArgList, ",")
						If iNumItems <> 2 Then sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						If sErrMsg = "" Then
							Select Case sArgList(1)
								Case "TIME" : .optTrigDelay(0).Value = True
								Case "EVEN" : .optTrigDelay(1).Value = True
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
							Delay(0.5)
							If Val(sArgList(2)) < Val(MainParam(TRIG_DEL).SetMin) Or Val(sArgList(2)) > Val(MainParam(TRIG_DEL).SetMax) Then
								sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							Else
								MainParam(TRIG_DEL).SetCur = sArgList(2) 'Sub "FillTextBoxes()" will update
							End If
						End If
						
					Case TRIG_DEL_SLOPE '.SetCod = ":TRIG:DEL:SLOP "
						Select Case sArg
							Case "POS" : .optDelaySlope(0).Value = True
							Case "NEG" : .optDelaySlope(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_DEL_SOUR '.SetCod = ":TRIG:DEL:SOUR "
						Select Case sArg
							Case "CHAN1" : .cboTrigDelaySour.SelectedIndex = 0
							Case "CHAN2" : .cboTrigDelaySour.SelectedIndex = 1
							Case "ECLT0" : .cboTrigDelaySour.SelectedIndex = 2
							Case "ECLT1" : .cboTrigDelaySour.SelectedIndex = 3
							Case "EXT" : .cboTrigDelaySour.SelectedIndex = 4
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_OCC '.SetCod = ":TRIG:OCC "
						If Val(sArg) < Val(MainParam(TRIG_OCC).SetMin) Or Val(sArg) > Val(MainParam(TRIG_OCC).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(TRIG_OCC).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case TRIG_STAND '.SetCod = ":TRIG:STAN "
						Select Case sArg
							Case "525" : .optStandard(0).Value = True
							Case "625" : .optStandard(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_FIELD '.SetCod = ":TRIG:FIEL "
						Select Case sArg
							Case "1" : .optField(0).Value = True
							Case "2" : .optField(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case TRIG_LINE '.SetCod = ":TRIG:LINE "
						If Val(sArg) < Val(MainParam(TRIG_LINE).SetMin) Or Val(sArg) > Val(MainParam(TRIG_LINE).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(TRIG_LINE).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case EXT_TRIG_COU '.SetCod = ":TRIG:COUP "
						Select Case sArg
							Case "DC" : .optExtTrigCoup(0).Value = True
							Case "DCF" : .optExtTrigCoup(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case HOLD_OFF '.SetCod = ":TRIG:HOLD "
						'Parse 2 arguments to process seperately
						iNumItems = iTIPStrToList(sArg, 1, sArgList, ",")
						If iNumItems <> 2 Then sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						If sErrMsg = "" Then
							Select Case sArgList(1)
								Case "TIME" : .optHoldOff(0).Value = True
								Case "EVEN" : .optHoldOff(1).Value = True
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
							Delay(0.5)
							If Val(sArgList(2)) < Val(MainParam(HOLD_OFF).SetMin) Or Val(sArgList(2)) > Val(MainParam(HOLD_OFF).SetMax) Then
								sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							Else
								MainParam(HOLD_OFF).SetCur = sArgList(2) 'Sub "FillTextBoxes()" will update
							End If
						End If
						
						'********** ACQuire Options **********
						'IMPORTANT: ACQ options must be set before TIMebase
					Case ACQ_TYPE '.SetCod = ":ACQ:TYPE "
						Select Case sArg
							Case "NORM", "AVER" 'No GUI settings apply
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case WF_POINTS '.SetCod = ":ACQ:POINTS "
						Select Case sArg
							Case "500" : .optDataPoints(0).Value = True
							Case "8000" : .optDataPoints(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case AVR_COUNT '.SetCod = ":ACQ:COUN "
						If Val(sArg) < Val(MainParam(AVR_COUNT).SetMin) Or Val(sArg) > Val(MainParam(AVR_COUNT).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(AVR_COUNT).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case COMPLETION '.SetCod = ":ACQ:COMP "
						If Val(sArg) < Val(MainParam(COMPLETION).SetMin) Or Val(sArg) > Val(MainParam(COMPLETION).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(COMPLETION).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
						'********** TIMebase Options **********
					Case REF_DELAY '.SetCod = ":TIM:DEL "
						If Val(sArg) < Val(MainParam(REF_DELAY).SetMin) Or Val(sArg) > Val(MainParam(REF_DELAY).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(REF_DELAY).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case HORZ_SENS '.SetCod = ":TIM:RANG "
						nRange = Val(sArg)
						For iIndex = 0 To 29
							If nRange = Val(ParamCode(HORZ_SENS, iIndex)) Then
								.cboHorizRange.SelectedIndex = iIndex
								Exit For
							End If
						Next iIndex
						
					Case H_REF_POS '.SetCod = ":TIM:REF "
						Select Case sArg
							Case "CENT" : .optReference(0).Value = True
							Case "LEFT" : .optReference(1).Value = True
							Case "RIGH" : .optReference(2).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case SAMPLE_MODE '.SetCod = ":TIM:SAMP "
						Select Case sArg
							Case "REAL" : .cboSampleType.SelectedIndex = 0
							Case "REP" : .cboSampleType.SelectedIndex = 1
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case ACQ_MODE '.SetCod = ":TIM:MODE "
						Select Case sArg
							Case "AUTO" : .cboAcqMode.SelectedIndex = 0
							Case "TRIG" : .cboAcqMode.SelectedIndex = 1
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
						'********** MEASure Options **********
					Case MEAS_MODE '.SetCod = ":MEAS:MODE "
						bTIP_UserMeas = True
						Select Case sArg
							Case "STAN" : bTIP_UserMeas = False
							Case "USER"
								'Shows USER defined measurement controls on "Measure" tab
								ChangeMeasurement(DELAY_MEAS)
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						
					Case MEAS_SOUR '.SetCod = ":MEAS:SOUR "
						If Not bTIP_UserMeas Then 'MEAS:MODE = STANDARD
							Select Case sArg
								Case "CHAN1" : .cboMeasSour.SelectedIndex = 0
								Case "CHAN2" : .cboMeasSour.SelectedIndex = 1
								Case "WMEM1" : .cboMeasSour.SelectedIndex = 2
								Case "FUNC1" : .cboMeasSour.SelectedIndex = 3
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
						Else 'MEAS:MODE = USER
							'Parse 2 arguments (input soures) to process seperately
							iNumItems = iTIPStrToList(sArg, 1, sArgList, ",")
							If iNumItems <> 2 Then sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							If sErrMsg = "" Then
								Select Case sArgList(1)
									Case "CHAN1" : .cboStartSource.SelectedIndex = 0
									Case "CHAN2" : .cboStartSource.SelectedIndex = 1
									Case "WMEM1" : .cboStartSource.SelectedIndex = 2
									Case "FUNC1" : .cboStartSource.SelectedIndex = 3
									Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
								End Select
								If sErrMsg = "" Then
									Select Case sArgList(2)
										Case "CHAN1" : .cboStopSource.SelectedIndex = 0
										Case "CHAN2" : .cboStopSource.SelectedIndex = 1
										Case "WMEM1" : .cboStopSource.SelectedIndex = 2
										Case "FUNC1" : .cboStopSource.SelectedIndex = 3
										Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
									End Select
								End If
							End If
						End If
						
					Case MEAS_UNIT
						'If .fraDelayParam.Visible Then
						Select Case sArg
							Case "VOLT" : .optStartLevel(0).Value = True
							Case "PERC" : .optStartLevel(1).Value = True
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						'End If
						
					Case MEAS_LOW 'Start Level
						'If .fraDelayParam.Visible Then
						If Val(sArg) < Val(StartLevel.SetMin) Or Val(sArg) > Val(StartLevel.SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							StartLevel.SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						'End If
						
					Case MEAS_UPP 'Stop Level
						'If .fraDelayParam.Visible Then
						If Val(sArg) < Val(StopLevel.SetMin) Or Val(sArg) > Val(StopLevel.SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							StopLevel.SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						'End If
						
					Case MEAS_DEF
						'If .fraDelayParam.Visible Then
						'Parse 7 arguments to process seperately
						iNumItems = iTIPStrToList(sArg, 1, sArgList, ",")
						If iNumItems <> 7 Then sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						If sErrMsg = "" Then
							Select Case sArgList(2)
								Case "POS" : .optStartSlope(0).Value = True
								Case "NEG" : .optStartSlope(1).Value = True
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
							If Val(sArgList(3)) < Val(StartEdge.SetMin) Or Val(sArgList(3)) > Val(StartEdge.SetMax) Then
								sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							Else
								StartEdge.SetCur = sArgList(3) 'Sub "FillTextBoxes()" will update
							End If
							Select Case sArgList(5)
								Case "POS" : .optStopSlope(0).Value = True
								Case "NEG" : .optStopSlope(1).Value = True
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
							If Val(sArgList(6)) < Val(StopEdge.SetMin) Or Val(sArgList(6)) > Val(StopEdge.SetMax) Then
								sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							Else
								StopEdge.SetCur = sArgList(6) 'Sub "FillTextBoxes()" will update
							End If
						End If
						' End If
						
						'********** FUNCtion (MATH) Options **********
					Case FUNC_RANGE 'GUI only
						.chkDisplayTrace(MATH_AREA).Value = True 'Enable Math area of Math/Memory tab
						If Val(sArg) < Val(MainParam(FUNC_RANGE).SetMin) Or Val(sArg) > Val(MainParam(FUNC_RANGE).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(FUNC_RANGE).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case FUNC_OFFSET 'GUI only
						.chkDisplayTrace(MATH_AREA).Value = True 'Enable Math area of Math/Memory tab
						If Val(sArg) < Val(MainParam(FUNC_OFFSET).SetMin) Or Val(sArg) > Val(MainParam(FUNC_OFFSET).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(FUNC_OFFSET).SetCur = sArg 'Sub "FillTextBoxes()" will update
							RegisterChange(FUNC_OFFSET) 'Update the scope trace graphic with the new value
						End If
						
					Case FUNC_ADD, FUNC_SUBTRACT, FUNC_MULTIPLY
						.chkDisplayTrace(MATH_AREA).Value = True 'Enable Math area of Math/Memory tab
						'Parse 2 arguments to process seperately
						iNumItems = iTIPStrToList(sArg, 1, sArgList, ",")
						If iNumItems <> 2 Then sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						If sErrMsg = "" Then
							Select Case sArgList(1)
								Case "CHAN1" : .cboMathSourceA.SelectedIndex = 0
								Case "CHAN2" : .cboMathSourceA.SelectedIndex = 1
								Case "WMEM1" : .cboMathSourceA.SelectedIndex = 2
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
							Select Case sArgList(2)
								Case "CHAN1" : .cboMathSourceB.SelectedIndex = 0
								Case "CHAN2" : .cboMathSourceB.SelectedIndex = 1
								Case "WMEM1" : .cboMathSourceB.SelectedIndex = 2
								Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
							End Select
						End If
						If i = FUNC_ADD Then .cboMathFunction.SelectedIndex = MATH_ADD
						If i = FUNC_SUBTRACT Then .cboMathFunction.SelectedIndex = MATH_SUBTRACT
						If i = FUNC_MULTIPLY Then .cboMathFunction.SelectedIndex = MATH_MULTIPLY
						MainParam(FUNC_RANGE).SetUOM = "V"
						MainParam(FUNC_OFFSET).SetUOM = "V"
						
					Case FUNC_DIFFERENTIATE
						.chkDisplayTrace(MATH_AREA).Value = True 'Enable Math area of Math/Memory tab
						.cboMathFunction.SelectedIndex = MATH_DIFFERENTIATE
						Select Case sArg
							Case "CHAN1" : .cboMathSourceA.SelectedIndex = 0
							Case "CHAN2" : .cboMathSourceA.SelectedIndex = 1
							Case "WMEM1" : .cboMathSourceA.SelectedIndex = 2
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						MainParam(FUNC_RANGE).SetUOM = "V"
						MainParam(FUNC_OFFSET).SetUOM = "V"
						
					Case FUNC_INTEGRATE
						.chkDisplayTrace(MATH_AREA).Value = True 'Enable Math area of Math/Memory tab
						.cboMathFunction.SelectedIndex = MATH_INTEGRATE
						Select Case sArg
							Case "CHAN1" : .cboMathSourceA.SelectedIndex = 0
							Case "CHAN2" : .cboMathSourceA.SelectedIndex = 1
							Case "WMEM1" : .cboMathSourceA.SelectedIndex = 2
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						MainParam(FUNC_RANGE).SetUOM = "V"
						MainParam(FUNC_OFFSET).SetUOM = "V"
						
					Case FUNC_INVERT
						.chkDisplayTrace(MATH_AREA).Value = True 'Enable Math area of Math/Memory tab
						.cboMathFunction.SelectedIndex = MATH_INVERT
						Select Case sArg
							Case "CHAN1" : .cboMathSourceA.SelectedIndex = 0
							Case "CHAN2" : .cboMathSourceA.SelectedIndex = 1
							Case "WMEM1" : .cboMathSourceA.SelectedIndex = 2
							Case Else : sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						End Select
						MainParam(FUNC_RANGE).SetUOM = "V"
						MainParam(FUNC_OFFSET).SetUOM = "V"
						
						'********** MEASUREMENT FUNCTION **********
					Case MEAS_FUNCTION
						.tabMainFunctions.SelectedIndex = 3
						Delay(0.1) 'Required for correct toolbar operation
						CurrentMeas = 0
						CurrentMeas = CShort(sArg)
						Select Case CurrentMeas
							Case 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20
								.tbrMeasFunction.Buttons(CurrentMeas).Value = ComctlLib.ValueConstants.tbrPressed
								ChangeMeasurement(CurrentMeas)
							Case Else : sErrMsg = "Invalid measurement function argument '" & sArg & "' passed from TIP Studio." & "  Valid range is 1 - 20."
						End Select
						
						
						'********** TRACE PARAMETERS **********
					Case TRACE_THICKNESS
						lTraceParam = CShort(sArg)
						Select Case lTraceParam
							Case 0, 1, 2
								.cboTraceThickness.SelectedIndex = lTraceParam
								ChangeTraceThickness() 'Sets trace thickness
							Case Else : sErrMsg = "Invalid trace thickness argument '" & sArg & "' passed from TIP Studio." & "  Valid values are 1, 2, or 3."
						End Select
						
						'TRACE COLOR (24 Bit color)
					Case CH1_TRACE_COLOR
						lTraceParam = CInt(sArg)
						If lTraceParam < &H0s Or lTraceParam > &HFFFFFF Then
							sErrMsg = "Invalid trace color argument '" & sArg & "' passed from TIP Studio."
						Else
							.cboTraceColor.SelectedIndex = CHANNEL_1
							TraceColor(CHANNEL_1) = lTraceParam
							bSkipDialog = True 'Don't pop up color dialog box in Sub ChangeTraceColor()
							ChangeTraceColor() 'Sets trace color
						End If
						
					Case CH2_TRACE_COLOR
						lTraceParam = CInt(sArg)
						If lTraceParam < &H0s Or lTraceParam > &HFFFFFF Then
							sErrMsg = "Invalid trace color argument '" & sArg & "' passed from TIP Studio."
						Else
							.cboTraceColor.SelectedIndex = CHANNEL_2
							TraceColor(CHANNEL_2) = lTraceParam
							bSkipDialog = True 'Don't pop up color dialog box in Sub ChangeTraceColor()
							ChangeTraceColor() 'Sets trace color
						End If
						
					Case MATH_TRACE_COLOR
						lTraceParam = CInt(sArg)
						If lTraceParam < &H0s Or lTraceParam > &HFFFFFF Then
							sErrMsg = "Invalid trace color argument '" & sArg & "' passed from TIP Studio."
						Else
							.cboTraceColor.SelectedIndex = MATH_AREA
							TraceColor(MATH_AREA) = lTraceParam
							bSkipDialog = True 'Don't pop up color dialog box in Sub ChangeTraceColor()
							ChangeTraceColor() 'Sets trace color
						End If
						
					Case MEMORY_TRACE_COLOR
						lTraceParam = CInt(sArg)
						If lTraceParam < &H0s Or lTraceParam > &HFFFFFF Then
							sErrMsg = "Invalid trace color argument '" & sArg & "' passed from TIP Studio."
						Else
							.cboTraceColor.SelectedIndex = MEMORY_AREA
							TraceColor(MEMORY_AREA) = lTraceParam
							bSkipDialog = True 'Don't pop up color dialog box in Sub ChangeTraceColor()
							ChangeTraceColor() 'Sets trace color
						End If
						
					Case SEGMENT_FILE
						frmZT1428.chkDisplayTrace(MEMORY_AREA).Value = True
						sArg = UCase(sArg)
						If sArg <> "" And InStr(2, sArg, ".SEG") > 1 Then
							sSegFileName = sArg
							LoadDiskToMemory()
							.chkDisplayTrace(MEMORY_AREA).Value = True
						Else
							sErrMsg = "Invalid waveform segment file name: '" & sArg & "' passed from TIP Studio."
						End If
						
						'********** MEMory Options **********
					Case MEM_RANGE 'GUI only
						.chkDisplayTrace(MEMORY_AREA).Value = True 'Enable Math area of Math/Memory tab
						If Val(sArg) < Val(MainParam(MEM_RANGE).SetMin) Or Val(sArg) > Val(MainParam(MEM_RANGE).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(MEM_RANGE).SetCur = sArg 'Sub "FillTextBoxes()" will update
						End If
						
					Case MEM_OFFSET 'GUI only
						.chkDisplayTrace(MEMORY_AREA).Value = True 'Enable Math area of Math/Memory tab
						If Val(sArg) < Val(MainParam(MEM_OFFSET).SetMin) Or Val(sArg) > Val(MainParam(MEM_OFFSET).SetMax) Then
							sErrMsg = "Invalid " & sCOMPcmd & " argument passed from TIP Studio."
						Else
							MainParam(MEM_OFFSET).SetCur = sArg 'Sub "FillTextBoxes()" will update
							RegisterChange(MEM_OFFSET) 'Update the scope trace graphic with the new value
						End If
						
					Case Else
						sErrMsg = "Digitizing Scope SAIS does not support '" & RTrim(sCmd) & "' command passed from TIP Studio."
						
				End Select
				
			End With
			
		End If
		
		If sErrMsg <> "" Then
			'Notify TIP studio that an error has occured and quit SAIS
			frmZT1428.txtDataDisplay.Text = "Error"
			If sTIP_Mode <> "TIP_DESIGN" And sTIP_Mode <> "GET CURR CONFIG" Then
				Delay(1)
				SetKey("TIPS", "STATUS", "Error: " & sErrMsg)
				EndProgram()
			End If
			If sTIP_Mode <> "GET CURR CONFIG" Then
				MsgBox(sErrMsg & vbCrLf & vbCrLf & "Instrument will be set to RESET state.", MsgBoxStyle.Exclamation)
				ResetScope()
				bTIP_Error = True 'Flag to exit saved string setup
			Else
				If InStr(sErrMsg, "command") Then
					sErrMsg = "Invalid command: <" & sCOMPcmd & "> received from instrument query."
				Else
					sErrMsg = "Invalid <" & sCOMPcmd & "> argument received from instrument query."
				End If
				MsgBox(sErrMsg, MsgBoxStyle.OKOnly, "Get Current - Unhandled Parameter")
			End If
		End If
		
	End Sub
	
	Function sTIPsSetCHANnel1Options() As String
		'DESCRIPTION:
		'   Appends input CH1 COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		Dim iParamIndex As Short
		Dim sTemp As String
		
		sTIPsSetCHANnel1Options = ""
		'If CH1 is not selected, exit
		If Not frmZT1428.tabMainFunctions.TabPages.Item(1).Enabled Then Exit Function
		
		For iParamIndex = IN1_COUP To IN1_VSENS
			If IsNumeric(MainParam(iParamIndex).SetCur) Then
				If Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
					sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
				End If
			Else
				If MainParam(iParamIndex).SetCur <> MainParam(iParamIndex).SetDef Then
					sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
				End If
			End If
		Next iParamIndex
		
		sTIPsSetCHANnel1Options = sTemp
		
	End Function
	
	Function sTIPsSetCHANnel2Options() As String
		'DESCRIPTION:
		'   Appends input CH2 COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		Dim iParamIndex As Short
		Dim sTemp As String
		
		sTIPsSetCHANnel2Options = ""
		'If CH2 is not selected, exit
		If Not frmZT1428.tabMainFunctions.TabPages.Item(2).Enabled Then Exit Function
		
		For iParamIndex = IN2_COUP To IN2_VSENS
			If IsNumeric(MainParam(iParamIndex).SetCur) Then
				If Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
					sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
				End If
			Else
				If MainParam(iParamIndex).SetCur <> MainParam(iParamIndex).SetDef Then
					sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
				End If
			End If
		Next iParamIndex
		
		sTIPsSetCHANnel2Options = sTemp
		
	End Function
	
	Function sTIPsSetOUTPutOptions() As String
		'DESCRIPTION:
		'   Appends OUTPut subsystem COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		Dim iParamIndex As Short
		Dim sTemp As String
		
		sTIPsSetOUTPutOptions = ""
		'Exit if no trigger outputs are selected
		If MainParam(OUT_TRIG_STATE).SetCur = "ON" Or MainParam(ECLT0_STATE).SetCur = "ON" Or MainParam(ECLT1_STATE).SetCur = "ON" Then
			sTemp = ":OUTP:STATE ON;"
			For iParamIndex = ECLT0_STATE To OUT_TRIG_STATE
				If MainParam(iParamIndex).SetCur <> MainParam(iParamIndex).SetDef Then
					sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
				End If
			Next iParamIndex
		End If
		
		sTIPsSetOUTPutOptions = sTemp
		
	End Function
	
	Function sTIPsSetTRIGgerOptions() As String
		'DESCRIPTION:
		'   Appends TRIGger subsystem COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		Dim iParamIndex As Short
		Dim sTemp As String
		
		sTIPsSetTRIGgerOptions = ""
		
		For iParamIndex = TRIG_MODE To HOLD_OFF
			
			'Take into account two arguments for HOLD:OFF and TRIG:DEL
			If iParamIndex = HOLD_OFF Then
				If Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
					sTemp = sTemp & MainParam(HOLD_OFF).SetCod & HoldType & MainParam(HOLD_OFF).SetCur & ";"
				End If
			ElseIf iParamIndex = TRIG_DEL Then 
				If frmZT1428.fraTrigDelay.Visible And Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
					sTemp = sTemp & MainParam(TRIG_DEL).SetCod & DelayType & MainParam(TRIG_DEL).SetCur & ";"
				End If
			Else
				If IsNumeric(MainParam(iParamIndex).SetCur) Then
					If Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
						sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
					End If
				Else
					If MainParam(iParamIndex).SetCur <> MainParam(iParamIndex).SetDef Then
						sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
					End If
				End If
			End If
			
		Next iParamIndex
		
		sTIPsSetTRIGgerOptions = sTemp
		
	End Function
	
	Function sTIPsSetACQuireOptions() As String
		'DESCRIPTION:
		'   Appends ACQuire subsystem COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings if Sample mode
		'   is REPETITIVE.
		
		Dim iParamIndex As Short
		Dim sTemp As String
		
		sTIPsSetACQuireOptions = ""
		
		sTemp = ":ACQ:TYPE AVER;"
		For iParamIndex = AVR_COUNT To COMPLETION
			If Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
				sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
			End If
		Next iParamIndex
		
		sTIPsSetACQuireOptions = sTemp
		
	End Function
	
	Function sTIPsSetTIMebaseOptions() As String
		'DESCRIPTION:
		'   Appends TIMebase subsystem COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		Dim iParamIndex As Short
		Dim iListIndex As Short
		Dim sTemp As String
		
		sTIPsSetTIMebaseOptions = ""
		
		For iParamIndex = WF_POINTS To H_REF_POS
			If iParamIndex = HORZ_SENS Then 'Set time range value based upon 500 or 8000 point waveform
				iListIndex = frmZT1428.cboHorizRange.SelectedIndex
				sTemp = sTemp & ":TIM:RANG " & ParamCode(HORZ_SENS, iListIndex) & ";"
			Else
				If IsNumeric(MainParam(iParamIndex).SetCur) Then
					If Val(MainParam(iParamIndex).SetCur) <> Val(MainParam(iParamIndex).SetDef) Then
						sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
					End If
				Else
					If MainParam(iParamIndex).SetCur <> MainParam(iParamIndex).SetDef Then
						sTemp = sTemp & MainParam(iParamIndex).SetCod & MainParam(iParamIndex).SetCur & ";"
					End If
				End If
			End If
		Next iParamIndex
		sTIPsSetTIMebaseOptions = sTemp
		
	End Function
	
	Function sTIPsSetMEASureOptions() As String
		'DESCRIPTION:
		'   Appends MEASure subsystem COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		Dim iParamIndex As Short
		Dim sTemp As String
		
		sTIPsSetMEASureOptions = ""
		
		If bTIP_UserMeas Then
			sTemp = sTemp & ":MEAS:MODE USER;" 'User defined measurement, default is Standard (STAN)
			sTemp = sTemp & ":MEAS:UNIT " & DelayLevelUnit & ";"
			If Val(StartLevel.SetCur) < Val(StopLevel.SetCur) Then
				sTemp = sTemp & ":MEAS:LOW " & StartLevel.SetCur & ";"
				sTemp = sTemp & ":MEAS:UPP " & StopLevel.SetCur & ";"
				sTemp = sTemp & ":MEAS:DEF DEL," & StartSlope & "," & StartEdge.SetCur & ",LOW," & StopSlope & "," & StopEdge.SetCur & ",UPP" & ";"
			Else
				sTemp = sTemp & ":MEAS:LOW " & StopLevel.SetCur & ";"
				sTemp = sTemp & ":MEAS:UPP " & StartLevel.SetCur & ";"
				sTemp = sTemp & ":MEAS:DEF DEL," & StartSlope & "," & StartEdge.SetCur & ",UPP," & StopSlope & "," & StopEdge.SetCur & ",LOW" & ";"
			End If
			sTIPsSetMEASureOptions = sTemp
		Else
			sTIPsSetMEASureOptions = ":MEAS:SOUR " & ParamCode(MEAS_SOUR, frmZT1428.cboMeasSour.SelectedIndex) & ";"
		End If
		
	End Function
	
	Function sTIPsSetFUNCtionOptions() As String
		
		'DESCRIPTION:
		'   Appends FUNCtion (MATH) subsystem COMP commands to the TIP CMD string when the "UpdateTIP"
		'   command button is pressed in TIP design mode based upon GUI settings.
		
		Dim iParamIndex As Short
		Dim sTemp As String
		Dim sSourceA As String
		Dim sSourceB As String
		
		If frmZT1428.picMathOptions.Visible = False Then
			Exit Function
		Else
			'0 = "CHAN1", 1 = "CHAN2", 2 = "WMEM1"
			sSourceA = ParamCode(MEAS_SOUR, frmZT1428.cboMathSourceA.SelectedIndex)
		End If
		
		If frmZT1428.fraMathSourceB.Visible Then
			sSourceB = ParamCode(MEAS_SOUR, frmZT1428.cboMathSourceB.SelectedIndex)
		End If
		
		Select Case frmZT1428.cboMathFunction.SelectedIndex
			Case MATH_ADD
				sTemp = sTemp & MainParam(FUNC_ADD).SetCod & sSourceA & "," & sSourceB & ";"
			Case MATH_SUBTRACT
				sTemp = sTemp & MainParam(FUNC_SUBTRACT).SetCod & sSourceA & "," & sSourceB & ";"
			Case MATH_MULTIPLY
				sTemp = sTemp & MainParam(FUNC_MULTIPLY).SetCod & sSourceA & ", " & sSourceB & ";"
			Case MATH_DIFFERENTIATE
				sTemp = sTemp & MainParam(FUNC_DIFFERENTIATE).SetCod & sSourceA & ";"
			Case MATH_INTEGRATE
				sTemp = sTemp & MainParam(FUNC_INTEGRATE).SetCod & sSourceA & ";"
			Case MATH_INVERT
				sTemp = sTemp & MainParam(FUNC_INVERT).SetCod & sSourceA & ";"
		End Select
		
		sTIPsSetFUNCtionOptions = sTemp
		
	End Function
	
	
	Function bGetInstrumentStatus() As Boolean
		'DESCRIPTION:
		'   Procedure to check the instrument status following issuing setup commands
		'   Updates TETS.INI [TIPS] status key to indicate an error if in TIP mode
		Dim sInstrumentStatus As String
		
		WriteMsg("SYST:ERR?")
		sInstrumentStatus = ReadMsg()
		sInstrumentStatus = StripNullCharacters(sInstrumentStatus)
		If Val(sInstrumentStatus) = 0 Then
			bGetInstrumentStatus = True 'No error
		Else
			bGetInstrumentStatus = False
			frmZT1428.txtDataDisplay.Text = "Error"
			MsgBox("Error Code: " & sInstrumentStatus, MsgBoxStyle.Exclamation, "Digitizing Oscilloscope Error Message")
			If bTIP_Running Then 'Put Error Message in STATUS key, clear CMD key
				SetKey("TIPS", "STATUS", "Error " & sInstrumentStatus)
				SetKey("TIPS", "CMD", "")
			End If
		End If
		
	End Function
	
	
	Public Function bSaveDSO_File(ByRef sCmds As String) As Boolean
		'Added to support the ScopeNAM  DJoiner 05/27/03
		'If the user selects [Cancel] or a file Error occurs --> Returns False
		'otherwise, the function returns True
		
		Dim iFileId As Short 'File Identification Number
		
		With frmZT1428
			
			'Remind the Developer that this file must be saved in the ATLAS Project directory.
			MsgBox("This file MUST be located in the ATLAS project directory" & vbCrLf & "before it can be referenced by the ScopeNAM.", MsgBoxStyle.Information)
			'Cancel Error is True
			'UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			.dlgFileIO.CancelError = True
			'Set Error Handler
			On Error GoTo FileErrHandler
			
			'Setup File I/O dialog
			'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
			.dlgFileIO.DefaultExt = "DSO"
			'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
			'UPGRADE_WARNING: Filter has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			.dlgFileIO.Filter = "DSO File (*.DSO)|*.DSO|All Files (*.*)|*.*"
			'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
			If .dlgFileIO.FileName = "" Then
				'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
				.dlgFileIO.FileName = "Test1.DSO"
			End If
			
			'Get file save name/location from user
			'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
			.dlgFileIO.ShowDialog()
			
			'Confirm File Replace
			'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
			If FileExists((.dlgFileIO.FileName)) Then
				'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
				If MsgBox("This folder already contains a file named '" & .dlgFileIO.FileName & vbCrLf & "Would you like to replace it?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, "Confirm File Replace") = MsgBoxResult.Yes Then
					'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
					Kill((.dlgFileIO.FileName))
				Else
					Exit Function
				End If
			End If
			
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
			System.Windows.Forms.Application.DoEvents()
			
			iFileId = FreeFile
			'UPGRADE_ISSUE: MSComDlg.CommonDialog control dlgFileIO was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E047632-2D91-44D6-B2A3-0801707AF686"'
			FileOpen(iFileId, .dlgFileIO.FileName, OpenMode.Output, OpenAccess.Write)
			
			'Write the file
			PrintLine(iFileId, sCmds)
			FileClose(iFileId)
			
			'UPGRADE_WARNING: Screen property Screen.MousePointer has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
			System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
			System.Windows.Forms.Application.DoEvents()
			bSaveDSO_File = True
			Exit Function
			
			
			'Handle any File Errors encountered
FileErrHandler: 
			'User pressed the [Cancel] button
			If Err.Number <> 32755 Then 'If not Cancel, Report Error to user
				MsgBox("File Error Number: " & Err.Number & vbCrLf & "Description: " & Err.Description)
			End If
			.Cursor = System.Windows.Forms.Cursors.Default
		End With
		
		
	End Function
	
	'JRC Added 033009
	Function GatherIniFileInformation(ByRef lpApplicationName As String, ByRef lpKeyName As String, ByRef lpDefault As String) As String
		'************************************************************
		'*     Finds a value on in the VIPERT.INI File                *
		'*    PARAMETERS:                                           *
		'*     lpApplicationName$ -[Application] in TETS.INI File   *
		'*     lpKeyName$ - KEYNAME= in TETS.INI File               *
		'*     lpDefault$ - Default value to return if not found    *
		'*    RETURNS                                               *
		'*     String containing the key value or the lpDefault     *
		'*    EXAMPLE:                                              *
		'*     FilePath$ = GatherIniFileInformation("Heading", ...  *
		'*      ..."MY_FILE", "")                                   *
		'************************************************************
		'Reqiires (3) Windows Api Functions
		'Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Long, ByVal lpFileName As String) As Long
		'Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Long
		'Declare Function GetWindowsDirectory Lib "kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
		
		Dim lpReturnedString As New VB6.FixedLengthString(255) 'Return Buffer
		Dim nSize As Integer 'Return Buffer Size
		Dim lpFileName As String 'INI File Key Name "Key=?"
		Dim ReturnValue As Integer 'Return Value Buffer
		Dim FileNameInfo As String 'Formatted Return String
		Dim WindowsDirectory As String 'Windows Directory API Query
		Dim lpString As String 'String to write to INI File
		
		'Clear String Buffer
		lpReturnedString.Value = ""
		'Find Windows Directory
		ReturnValue = GetWindowsDirectory(lpReturnedString.Value, 255)
		FileNameInfo = Trim(lpReturnedString.Value)
		FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
		WindowsDirectory = FileNameInfo
		lpFileName = WindowsDirectory & "\VIPERT.INI"
		nSize = 255
		lpReturnedString.Value = ""
		FileNameInfo = ""
		'Find File Locations
		ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString.Value, nSize, lpFileName)
		FileNameInfo = Trim(lpReturnedString.Value)
		FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo) - 1)
		'If File Locations Missing, then create empty keys
		If FileNameInfo = lpDefault & Chr(0) Or FileNameInfo = lpDefault Then
			lpString = Trim(lpDefault)
			ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
		End If
		
		'Return Information In INI File
		GatherIniFileInformation = FileNameInfo
		
	End Function
	
	
	Public Sub SetUpControls()
		
		'Inhibit these controls in a TIP Run mode
		frmZT1428.cmdLoadFromDisk.Enabled = False
		frmZT1428.cmdAutoscale.Enabled = False
		ConfigureFrontPanel()
		'Give user full access to controls in design mode
		frmZT1428.cmdUpdateTIP.Visible = True
		frmZT1428.cmdLoadFromDisk.Enabled = True
		frmZT1428.cmdAutoscale.Enabled = True
		sTIP_CMDstring = "" 'Now that SAIS has been setup, Clear old data from string
		
	End Sub
	Sub WaitForTrigger()
		'DESCRIPTION:
		'   This routine uses the Scope's Trigger Event Register (TER) to
		'   wait until a trigger occurs when in Triggered Mode (:TIM:MODE TRIG)
		'   and notify the user as such.
		'   The TER is always active, meaning that in hardware, anytime trigger
		'   conditions are satisfied, the TER is set. It is automatically
		'   cleared when queried.
		'   The variable delays in this routine are used to prevent the display
		'   from showing "Waiting for Trigger" when this SAIS is looping faster than
		'   the acquisition time.
		
		Do While bTrigMode And LiveMode
			Delay(0.08)
			WriteMsg("TER?") 'Query the Trigger Event Register
			If Val(ReadMsg()) <> 0 Then Exit Do
			''        Delay IIf(dAcqTime < 0.08, 0.08, dAcqTime * 1.2)
			''        WriteMsg "TER?"   'Query the Trigger Event Register
			''        If Val(ReadMsg$()) <> 0 Then Exit Do
			''        Debug.Print "Waiting for Trigger"
			frmZT1428.txtDataDisplay.Text = "Waiting for Trigger"
			Delay(0.25)
		Loop 
		If (dAcqTime > 1) And (CurrentMeas = MEAS_OFF) Then
			frmZT1428.txtDataDisplay.Text = "Acquiring..."
		ElseIf frmZT1428.txtDataDisplay.Text = "Waiting for Trigger" Then 
			frmZT1428.txtDataDisplay.Text = ""
		End If
		If Not frmZT1428.chkContinuous._Value Then
			Select Case dAcqTime
				Case Is < 0.2 : Delay(0.25)
				Case Else : Delay(dAcqTime * 1.2)
			End Select
		Else
			If bTrigMode Then Delay(dAcqTime)
		End If
		'frmZT1428.txtDataDisplay.Caption = ""
		
	End Sub
End Module