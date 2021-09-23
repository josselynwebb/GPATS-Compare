Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module VIPLibrary
	
	'//2345678901234567890123456789012345678901234567890123456789012345678901234567890
	'////////////////////////////////////////////////////////////////////////////
	'//
	'// Virtual Instrument Portable Equipment Repair/Tester (VIPER/T) Software Module
	'//
	'// File:       Main.bas
	'//
	'// Date:       14FEB06
	'//
	'// Purpose:    SAIS: Library Functions
	'//
	'// Instrument: Agilent E1428A Digitizing Oscilloscope (Dso)
	'//
	'//
	'// Revision History
	'// Rev      Date                  Reason
	'// =======  =======  =======================================
	'// 1.0.0.0  14FEB06  Baseline Release
	'////////////////////////////////////////////////////////////////////////////////
	'
	'
	' Legacy Revision History
	'
	'
	'************************************************************
	'* Front Panel Test Software Module                         *
	'*                                                          *
	'* Nomenclature   : VIP Test Library Functions              *
	'* Version        : 2.0                                     *
	'* Written By     : ManTech Test Systems                    *
	'* Last Update    : 09/20/04  by Tom Biggs                  *
	'************************************************************
	' The following are elements of InstParameter
	' Not all elements need be filled.
	'SetCur$    ' "Current Settings" Array
	'SetDef$    ' "Default Settings" Array
	'SetMin$    ' "Maximum Settings" Array
	'SetMax$    ' "Minimum Settings" Array
	'SetUOM$    ' "Unit Of Measure" Array
	' Examples: "Vdc", "Vpk", "A", "Hz", "S", "% Mod", "Deg", and ""
	'SetRes$(1 To MAX_SETTINGS)    ' "Setting Resolution" Array
	' "A0" = Absolute integer resolution
	' "An" where 'n' is digits of displayed resolution
	' "D0" = integer resolution
	' "Dn" where 'n' is digits of displayed resolution
	' Note: Use "An" when resolution is specified as an absolute value.
	'       Use "Dn" when resolution is specified as "digits".
	'SetMinInc$ ' "Minimum Increment Setting" Array
	'SetCod$    ' Message Based Code Array
	'-------------------------------------------------------------
	Structure InstParameter
		Dim SetCod As String
		Dim SetDef As String
		Dim SetCur As String
		Dim SetMin As String
		Dim SetMax As String
		Dim SetUOM As String
		Dim SetRes As String
		Dim SetMinInc As String
		Dim SetFmt As String
		Dim TxtBox As Object
	End Structure
	
	' ** Waveform is a user defined type which can be used in conjunction with
	'    the UnpackWaveform command.  The time of each measurement point can be
	'    calculated with respect to the trigger can be calculated with the following
	'    formula:  [(data point number - reference) x Interval] + Origin
	'    E.g.
	'                               Dim SinWave As Waveform
	Structure Waveform
		Dim Points As Short
		Dim WFType As Short
		Dim Resolution As Short
		Dim Magnitude() As Double
		Dim Time() As Single
		Dim XInterval As Double
		Dim XORIGIN As Double
		Dim XREFERENCE As Short
		Dim XUnits As String
		Dim YInterval As Double
		Dim YORIGIN As Double
		Dim YREFERENCE As Short
		Dim YUnits As String
	End Structure
	
	Const NUMERIC_CHRS As String = "0123456789.+-eE" & vbBack
	Const ESCAPE_KEY As Short = 27
	Const SECS_IN_DAY As Integer = 86400
	Dim SessionHandle As Integer
	
	Public QueryInterupted As Short
	Public UserCancel As Short 'Indicates when the user Cancels from the UserEnter box
	
	Public LiveMode As Short 'Enable/Disable Live Dscope Communication
	
	Public WvPoints As Short
	
	Dim BusAccessCount As Short
	Dim QueryCode(1000) As String
	Dim TempWave(499) As Single
	Dim bReadPending As Boolean 'Flag to designate there is a pending read from the Scope
	Public GlobalCommand As String
	
	'-----------------API / DLL Declarations------------------------------
	Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer
	
	Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer
	
	Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Short, ByRef ActReadLen As Integer) As Integer
	
	Declare Function atxmlDF_zt1428_read_waveform Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal Source As Integer, ByVal TransferType As Integer, ByRef WaveFormArray As Double, ByRef NumberOfPoints As Integer, ByRef AcquisitionCount As Integer, ByRef SampleInterval As Double, ByRef TimeOffset As Double, ByRef XREFERENCE As Integer, ByRef VoltIncrement As Double, ByRef VoltOffset As Double, ByRef YREFERENCE As Integer) As Integer
	
	Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer
	
	Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer
	
	Declare Function atxmlDF_viOut16 Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByVal val16 As Short) As Integer
	
	Declare Function atxmlDF_viClear Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Short) As Integer
	
	Declare Function atxmlDF_viSetAttribute Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer
	
	Declare Function GetSystemDirectory Lib "kernel32"  Alias "GetSystemDirectoryA"(ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
	Declare Function GetWindowsDirectory Lib "kernel32"  Alias "GetWindowsDirectoryA"(ByVal lpBuffer As String, ByVal nSize As Integer) As Integer
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	Declare Function GetPrivateProfileString Lib "kernel32"  Alias "GetPrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpDefault As String, ByVal lpReturnedString As String, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	'UPGRADE_ISSUE: Declaring a parameter 'As Any' is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="FAE78A8D-8978-4FD4-8208-5B7324A8F795"'
	Declare Function WritePrivateProfileString Lib "kernel32"  Alias "WritePrivateProfileStringA"(ByVal lpApplicationName As String, ByVal lpKeyName As Any, ByVal lpString As Any, ByVal lpFileName As String) As Integer
	Declare Function GetTempPath Lib "kernel32.dll"  Alias "GetTempPathA"(ByVal nBufferLength As Integer, ByVal lpBuffer As String) As Integer
	
	'-----------------Init and GUID ---------------------------------------
	Public Const guid As String = "{4A89F54F-7D40-4d2d-B3A2-244914E73115}"
	Public Const proctype As String = "SFP"
	Public ResourceName As String
	Public QuitProgram As Boolean 'used in EndProgram to determine if reset or not
	
	
	
	Function Extract(ByRef FromString As String, ByRef Item As String) As String
		Dim i As Short
		
		Select Case UCase(Item)
			Case "FILENAME"
				If InStr(FromString, ".") Then
					FromString = Left(FromString, InStr(FromString, ".") - 1)
				End If
				For i = Len(FromString) To 1 Step -1
					If Mid(FromString, i, 1) = "\" Then
						Exit For
					End If
				Next i
				Extract = Mid(FromString, i + 1)
			Case "EXTENSION"
			Case "PATH"
			Case "DRIVE"
		End Select
		
	End Function
	
	
	Sub EndProgram()
		Dim NBR As Integer
		
		Dim ClearStatus As Integer
		
		If Not LiveMode Then
			End
		End If
		
		If bTrigMode Or bTIP_Running Then
			ClearStatus = atxmlDF_viClear(ResourceName, 255)
			Delay(0.5)
			WriteMsg("*CLS")
			Delay(0.1)
			WriteMsg("*RST")
		End If
		
		If bReadPending Then
			'hpe1428a_readInstrData instrumentHandle&, 10, ReadBuffer$, NBR&
			ErrorStatus = atxml_ReadCmds(ResourceName, ReadBuffer.Value, 10, NBR)
			
		End If
		
		WriteMsg("MEM:VME:STATE OFF;*OPC?")
		'hpe1428a_readInstrData instrumentHandle&, 10, ReadBuffer$, NBR&
		ErrorStatus = atxml_ReadCmds(ResourceName, ReadBuffer.Value, 10, NBR)
		
		If QuitProgram Then
			WriteMsg("*OPC?")
			QuitProgram = False
		Else
			WriteMsg("*RST;*OPC?")
		End If
		
		'hpe1428a_readInstrData instrumentHandle&, 10, ReadBuffer$, NBR&
		ErrorStatus = atxml_ReadCmds(ResourceName, ReadBuffer.Value, 10, NBR)
		WriteMsg("*CLS")
		
		' WMT Address issue --> ErrorStatus& = viClear(instrumentHandle&)
		'If ErrorStatus& Then DisplayErrorMessage ErrorStatus&
		
		'ErrorStatus& = hpe1428a_close(instrumentHandle&)
		ErrorStatus = atxml_Close()
		If ErrorStatus Then DisplayErrorMessage(ErrorStatus)
		
		If sTIP_Mode = "TIP_RUNPERSIST" Or sTIP_Mode = "TIP_RUNSETUP" Or sTIP_Mode = "TIP_MEASONLY" Then
			If UCase(frmZT1428.txtDataDisplay.Text) <> "ERROR" Then
				'Put measurement into [TIPS] STATUS key for studio
				If CurrentMeas = MEAS_OFF Then
					SetKey("TIPS", "STATUS", "Ready")
				Else
					SetKey("TIPS", "STATUS", "Ready " & sTIP_Measured)
				End If
				SetKey("TIPS", "CMD", "")
			End If
		End If
		
		'Give Resources Back To Op-System
		End
		
	End Sub
	
	
	Function FindLast(ByRef DataEntry As String) As String
		Dim TempCount As Short
		
		TempCount = BusAccessCount
		Do 
			TempCount = TempCount - 1
			If TempCount < 0 Then
				TempCount = 1000
			End If
			If InStr(QueryCode(TempCount), DataEntry) Then
				FindLast = QueryCode(TempCount)
				Exit Function
			End If
		Loop While TempCount <> BusAccessCount
		
	End Function
	
	
	Sub GetMemorySpace(ByRef MemoryAddress As Integer)
		Dim VMEAddress As Integer
		Dim ErrCode As Integer
		
		If Not LiveMode Then Exit Sub
		'Changed the literal &H3FFF00AD to Constant VI_ATTR_MEM_BASE   DJoiner  01/14/02
		'WMT (NOT USING ADDRESSES AT ALL)  ErrCode& = viGetAttribute(instrumentHandle&, VI_ATTR_MEM_BASE, VMEAddress&)
		MemoryAddress = VMEAddress
		
	End Sub
	
	
	Sub GetWaveform(ByRef WaveMagnitudePtr As Double, ByRef WaveTimePtr As Single, ByRef YIncr As Double, ByRef YOrig As Double, ByRef XIncr As Double, ByRef XOrig As Double, ByRef ChannelNum As Short, ByRef TotalChannels As Short, ByRef AquType As Short)
		Dim ErrStatus As Integer
		Dim AcqCnt As Integer
		Dim XTrig As Integer
		Dim YTrig As Integer
		Dim WvPnts As Integer
		Dim i As Short
		
		If Not LiveMode Then Exit Sub
		
		'ErrStatus& = hpe1428a_WvData(instrumentHandle&, YIncr#, YOrig#, XIncr#, XOrig#, ChannelNum%, TotalChannels% + 1, AquType%)
		'ErrStatus& = hpe1428a_GtWv(WaveMagnitudePtr!, WaveTimePtr!)
		WvPnts = WvPoints
		
		If WvPnts = 8000 Then
			If frmZT1428.optDataPoints(1).Value = True Then WriteMsg("ACQ:POIN 8000")
			frmZT1428.txtDataDisplay.Text = "Getting 8K Wave..."
			frmZT1428.Refresh()
			'Delay 2
		End If
		If frmZT1428.chkContinuous.Value = False Or sTIP_Mode = "TIP_MEASONLY" Then
			ErrStatus = atxmlDF_viClear(ResourceName, 255)
		End If
		
		ErrStatus = atxmlDF_zt1428_read_waveform(ResourceName, instrumentHandle, ChannelNum + 1, 0, WaveMagnitudePtr, WvPnts, AcqCnt, XIncr, XOrig, XTrig, YIncr, YOrig, YTrig)
		
		
		'If WaveMagnitude#(ChannelNum%, 0) = 0# Then
		If ErrStatus Then
			'Debug.Print ErrStatus&, WaveMagnitude#(ChannelNum%, 0), WaveTime!(0)
			'ErrStatus& = hpe1428a_WvData(instrumentHandle&, YIncr#, YOrig#, XIncr#, XOrig#, ChannelNum%, TotalChannels% + 1, AquType%)
			'ErrStatus& = hpe1428a_GtWv(WaveMagnitudePtr!, WaveTimePtr!)
			ErrStatus = atxmlDF_viClear(ResourceName, 255)
			WvPnts = WvPoints
			ErrStatus = atxmlDF_zt1428_read_waveform(ResourceName, instrumentHandle, ChannelNum + 1, 0, WaveMagnitudePtr, WvPnts, AcqCnt, XIncr, XOrig, XTrig, YIncr, YOrig, YTrig)
			If ErrStatus Then DisplayErrorMessage(ErrorStatus)
			
		End If
		
		If WvPnts = 8000 Then
			frmZT1428.txtDataDisplay.Text = ""
		End If
		
		For i = 0 To WvPnts - 1
			WaveTime(i) = XOrig + XIncr * i
		Next i
		
	End Sub
	
	
	Sub ScopeMemOffset(ByRef Offset1 As Integer, ByRef Offset2 As Integer, ByRef MathOffset As Integer, ByRef WavePoints As Short, ByRef VMEAddress As Integer)
		Dim ErrStat As Integer
		
		If Not LiveMode Then Exit Sub
		'WMT (not using) ErrStat& = hpe1428a_FindInst(instrumentHandle&, Offset1&, Offset2&, MathOffset&, WavePoints%, VMEAddress&)
		WvPoints = WavePoints
		
	End Sub
	
	
	Sub SoftReset()
		Dim SystErr As Integer
		
		If Not LiveMode Then Exit Sub
		'SystErr& = hpe1428a_softReset(instrumentHandle&)
		SystErr = atxmlDF_viOut16(ResourceName, instrumentHandle, VI_A16_SPACE, 4, &HFFFFs)
		
	End Sub
	
	
	Function StringToList(ByRef strng As String, ByRef List() As String, ByRef delimiter As String) As Short
		
		'DESCRIPTION:
		' Procedure to convert a delimited string into a list array
		'Parameters:
		' strng$     : String to be converted.
		' list$()    : Array in which to return list of strings
		' Delimiter$ : Char array of valid delimiters.
		'Returns:
		' Number of items in list
		' Returns -1 if number of number of elements exceeds
		' upper bound of passed array
		Dim numels As Short
		Dim inflag As Short
		Dim slength As Short
		Dim ch As Short
		'UPGRADE_NOTE: Char was upgraded to Char_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Char_Renamed As String
		
		numels = 0
		inflag = 0
		List(0) = ""
		'Go through parsed string a character at a time.
		slength = Len(strng)
		For ch = 1 To slength
			Char_Renamed = Mid(strng, ch, 1)
			'Test for delimiter
			If InStr(delimiter, Char_Renamed) = 0 Then
				If Not inflag Then
					'Test for too many arguments.
					If numels = UBound(List) Then
						StringToList = -1
						Exit For
					End If
					numels = numels + 1
					List(numels) = ""
					inflag = -1
				End If
				'Add the character to the current argument.
				List(numels) = List(numels) & Char_Renamed
			Else
				'Found a delimiter.
				'Set "Not in element" flag to FALSE.
				inflag = 0
			End If
		Next ch
		
		'Return Function Value
		StringToList = numels
		
	End Function
	
	Public Function iTIPStrToList(ByRef sStr As String, ByRef iLower As Short, ByRef sList() As String, ByRef sDelimiter As String) As Short
		'DESCRIPTION:
		'   Procedure to convert a delimited string into a dynamic string array
		'   ReDims the array from iLower to the number of elements in string
		'Parameters:
		'   sStr       : String to be parsed.
		'   iLower     : Lower bound of target array
		'   sList()    : Dynamic array in which to return list of strings
		'   sDelimiter : Delimiter string.
		'Returns:
		'   Number of items in string
		'   or 0 if string is empty
		
		Dim iNumels As Object
		Dim i As Short
		Dim iDelimiterLength As Short
		
		iDelimiterLength = Len(sDelimiter)
		If sStr = "" Then
			iTIPStrToList = 0
			Exit Function
		End If
		
		'UPGRADE_WARNING: Couldn't resolve default property of object iNumels. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		iNumels = 1
		'UPGRADE_WARNING: Lower bound of array sList was changed from iLower to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
		ReDim sList(iLower)
		'Go through parsed string a character at a time.
		For i = 1 To Len(sStr)
			'Test for delimiter
			If Mid(sStr, i, iDelimiterLength) <> sDelimiter Then
				'Add the character to the current argument.
				'UPGRADE_WARNING: Couldn't resolve default property of object iNumels. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				sList(iLower + iNumels - 1) = sList(iLower + iNumels - 1) & Mid(sStr, i, 1)
			Else
				'Found a delimiter.
				'UPGRADE_WARNING: Lower bound of array sList was changed from iLower to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="0F1C9BE1-AF9D-476E-83B1-17D43BECFF20"'
				'UPGRADE_WARNING: Couldn't resolve default property of object iNumels. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				ReDim Preserve sList(iLower + iNumels)
				'UPGRADE_WARNING: Couldn't resolve default property of object iNumels. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				iNumels = iNumels + 1
				i = i + iDelimiterLength - 1
			End If
		Next i
		'Remove any spaces from each array item
		For i = iLower To UBound(sList)
			sList(i) = Trim(sList(i))
		Next i
		'UPGRADE_WARNING: Couldn't resolve default property of object iNumels. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		iTIPStrToList = iNumels
		
	End Function
	
	
	Public Sub SplashStart()
		
		'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
		Load(frmAbout)
		frmAbout.cmdOK.Visible = False
		frmAbout.Show()
		System.Windows.Forms.Application.DoEvents()
		
	End Sub
	
	
	Public Sub SplashEnd()
		
		frmAbout.Close()
		
	End Sub
	
	
	Sub UnpackPreamble(ByRef PreData As String, ByRef YIncrement As Single, ByRef YOffset As Single, ByRef XIncrement As Single, ByRef XOrigen As Single)
		
		Dim Items As Short
		Dim XOffset As Single
		Const DATA_FORMAT As Short = 1
		Const SIXTEEN_BITS As Short = 2
		Const DATA_POINTS As Short = 3
		Const XIncr As Short = 5
		Const XORIGIN As Short = 6
		Const XREFERENCE As Short = 7
		Const YIncr As Short = 8
		Const YORIGIN As Short = 9
		Const YREFERENCE As Short = 10
		Const TERMINATOR As Short = 2
		Const BYTE_AFTER As Short = 1
		
		Dim PreambleList(10) As Object
		'On Error GoTo ExitSub
		
		Items = StringToList(PreData, PreambleList, ",")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XIncrement = Val(PreambleList(XIncr))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XOffset = Val(PreambleList(XORIGIN))
		
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		YIncrement = Val(PreambleList(YIncr))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		YOffset = Val(PreambleList(YORIGIN))
		
	End Sub
	
	
	Sub UnpackWaveform(ByRef Preamble As String, ByRef WaveData As String, ByRef Magn() As Double, ByRef TimeA() As Single, ByRef WvIndex As Short)
		
		'DESCRIPTION:
		' This routine takes string data of a digitized wave form from an
		' oscilloscope, and translates it to usable values.
		'PARAMETERS:
		' Preamble$ = Preable data pertaining to the waveform in question.
		' WaveData$ = Strind list of digitized waveform points
		' Magn# =     A double precision array to store each "Y" measurement value
		' TimeA! =    A single precision array to store each time bin reference
		'EXAMPLE:
		'               UnpackWaveform Preamble$, "#40010&^%KNBGFCC", WaveMag#(), WaveTime!()
		Dim Items As Short
		Dim Points As Short
		Dim Resolution As Short
		Dim WAVE_FORM_TYPE As Object
		Dim WFType As Short
		Dim XInterval As Double
		Dim XOrg As Double
		Dim XRef As Short
		Dim YInterval As Double
		Dim YOrg As Double
		Dim YRef As Short
		Dim SkippedBytes As Short
		Dim VPoint As Short
		Dim MaxChar As Short
		Dim StringPosition As Short
		Dim MSByte As Single
		Dim LSByte As Short
		Dim YOrig As Double
		Dim WaveByte As Short
		Const DATA_FORMAT As Short = 1
		Const SIXTEEN_BITS As Short = 2
		Const DATA_POINTS As Short = 3
		Const XIncrement As Short = 5
		Const XORIGIN As Short = 6
		Const XREFERENCE As Short = 7
		Const YIncrement As Short = 8
		Const YORIGIN As Short = 9
		Const YREFERENCE As Short = 10
		Const TERMINATOR As Short = 2
		Const BYTE_AFTER As Short = 1
		
		Dim PreambleList(10) As Object
		On Error GoTo ExitSub
		
		Items = StringToList(Preamble, PreambleList, ",")
		
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Points = Val(PreambleList(DATA_POINTS))
		'UPGRADE_WARNING: Couldn't resolve default property of object WAVE_FORM_TYPE. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		WFType = Val(PreambleList(WAVE_FORM_TYPE))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Resolution = Val(PreambleList(DATA_FORMAT))
		
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XInterval = Val(PreambleList(XIncrement))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XOrg = Val(PreambleList(XORIGIN))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		XRef = Val(PreambleList(XREFERENCE))
		
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		YInterval = Val(PreambleList(YIncrement))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		YOrg = Val(PreambleList(YORIGIN))
		'UPGRADE_WARNING: Couldn't resolve default property of object PreambleList$(). Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		YRef = Val(PreambleList(YREFERENCE))
		
		SkippedBytes = 2 + Val(Mid(WaveData, 2, 1))
		VPoint = 0
		
		If Resolution = SIXTEEN_BITS Then
			If Len(WaveData) - (1 + BYTE_AFTER + SkippedBytes) > UBound(Magn, 2) Then
				MaxChar = UBound(Magn, 2) + BYTE_AFTER + SkippedBytes
			Else
				MaxChar = Len(WaveData) - 1
			End If
			For StringPosition = BYTE_AFTER + SkippedBytes To MaxChar Step 2
				MSByte = Asc(Mid(WaveData, StringPosition, 1))
				LSByte = Asc(Mid(WaveData, StringPosition + 1, 1))
				'Voltage Conversion Formula: Voltage=[(data value - yreference)*yincrement]+yorigin
				Magn(WvIndex, VPoint) = ((((256 * MSByte) + LSByte) - YRef) * YInterval) + YOrig
				'Time Conversion Formula: Time=[(data point number - xreference)*xincrement]+xorigin
				TimeA(VPoint) = ((VPoint - XRef) * XInterval) + XOrg
				VPoint = VPoint + 1
			Next StringPosition
		Else
			If Len(WaveData) - (BYTE_AFTER + SkippedBytes) > UBound(Magn, 2) + 1 Then
				MaxChar = UBound(Magn, 2) + BYTE_AFTER + SkippedBytes
				MsgBox("Retrieved wave size greater than currently selected number of waveform points.  Truncating to allowable size.")
			Else
				MaxChar = Len(WaveData)
			End If
			For StringPosition = BYTE_AFTER + SkippedBytes To MaxChar
				WaveByte = Asc(Mid(WaveData, StringPosition, 1))
				'Voltage Conversion Formula: Voltage=[(data value - yreference)*yincrement]+yorigin
				Magn(WvIndex, VPoint) = ((WaveByte - YRef) * YInterval) + YOrg
				'Time Conversion Formula: Time=[(data point number - xreference)*xincrement]+xorigin
				TimeA(VPoint) = ((VPoint - XRef) * XInterval) + XOrg
				VPoint = VPoint + 1
			Next StringPosition
		End If
		
ExitSub: 
		
	End Sub
	
	
	Function ProcessKeyPress(ByRef KeyPress As Short, ByRef Param As InstParameter) As Object
		
		If Chr(KeyPress) = vbCr Then
			frmZT1428.tabMainFunctions.Focus()
			'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ProcessKeyPress = 0
			Exit Function
		End If
		If KeyPress = ESCAPE_KEY Then
			frmZT1428.tabMainFunctions.Focus()
			'UPGRADE_WARNING: Couldn't resolve default property of object Param.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			Param.TxtBox.Text = EngNotate(Val(Param.SetCur), Param)
			'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			ProcessKeyPress = 0
			Exit Function
		End If
		
		'Return Function Value
		'UPGRADE_WARNING: Couldn't resolve default property of object ProcessKeyPress. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		ProcessKeyPress = KeyPress
		
	End Function
	
	
	Function Log10(ByRef Expression As Double) As Integer
		
		Log10 = System.Math.Log(Expression) / System.Math.Log(10)
		
	End Function
	
	
	Public Sub FormatEntry(ByRef Parameter As InstParameter)
		'DESCRIPTION:
		'   Formats user-entered values, or Min, Max or Def.
		'   Resets out-of-range values to Min or Max values.
		'   Stores value in Parameter.SetCur$.
		'   Sets focus to TabOptions at end.
		'EXAMPLES:
		'   FormatEntry txtAmpl, MainParam (VOLT)
		'PARAMETERS:
		'   Num:        The TextBox the user entered a number into which will be _
		'formatted by this procedure.
		'   Paramter:   The parameter to be modified
		'GLOBAL VARIABLES USED: NONE
		'REQUIRES:  EngNotate
		'VERSION:   9/9/96
		'CUSTODIAN: M. McCabe
		Dim i As Short
		Dim n As String
		Dim Eng As String
		Dim Exponent As Short
		Dim temp As Single
		
		'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		If UCase(Left(LTrim(Parameter.TxtBox.Text), 3)) = "MIN" Or UCase(Left(LTrim(Parameter.TxtBox.Text), 1)) = "N" Then
			Parameter.SetCur = Parameter.SetMin
			'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		ElseIf UCase(Left(LTrim(Parameter.TxtBox.Text), 3)) = "MAX" Or UCase(Left(LTrim(Parameter.TxtBox.Text), 1)) = "X" Then 
			Parameter.SetCur = Parameter.SetMax
			'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		ElseIf UCase(Left(LTrim(Parameter.TxtBox.Text), 3)) = "DEF" Or UCase(Left(LTrim(Parameter.TxtBox.Text), 1)) = "D" Then 
			If Parameter.SetDef <> "" Then
				Parameter.SetCur = Parameter.SetDef
			End If
		Else
			'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			For i = 1 To Len(Parameter.TxtBox.Text)
				'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				n = Mid(Parameter.TxtBox.Text, i, 1)
				If Not IsNumeric(n) And n <> " " And n <> "-" And n <> "." Then
					Eng = n
					Exit For
				End If
			Next i
			Select Case Eng
				Case "" : Exponent = 0
				Case "p", "P" : Exponent = -12
				Case "n", "N" : Exponent = -9
				Case Chr(181), "u", "U" : Exponent = -6
				Case "m" : Exponent = -3
				Case "k", "K" : Exponent = 3
				Case "M" : Exponent = 6
				Case "G", "g" : Exponent = 9
				Case "T", "t" : Exponent = 12
				Case Else : Exponent = 0
			End Select
			'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			temp = Val(Parameter.TxtBox.Text) * 10 ^ Exponent
			
			If (Val(Str(temp)) < Val(Parameter.SetMin)) Or (temp > Val(Parameter.SetMax)) Then
				MsgBox(ShowVals(Parameter), MsgBoxStyle.Information, "OUT OF RANGE")
				'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Parameter.TxtBox.Text = EngNotate(CDbl(Parameter.SetCur), Parameter)
				'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.SetFocus. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				Parameter.TxtBox.SetFocus()
				GotFocusSelect()
				Exit Sub
			Else
				Parameter.SetCur = CStr(temp)
			End If
		End If
		
		'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Parameter.TxtBox.Text = EngNotate(Val(Parameter.SetCur), Parameter)
		
	End Sub
	
	
	Sub GotFocusSelect()
		'DESCRIPTION:
		'   Highlights (selects) the TextBox text.
		'   Call from the GotFocus event of the subject TextBox.
		'EXAMPLE:       GotFocusSelect
		'PARAMETERS:    None
		'GLOBAL VARIABLES USED: None
		'REQUIRES:      Nothing
		'VERSION:       9/9/96
		'CUSTODIAN:     J. Hill
		
		'UPGRADE_ISSUE: Control SelStart could not be resolved because it was within the generic namespace ActiveControl. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
		VB6.GetActiveControl().SelStart = 0
		'UPGRADE_ISSUE: Control SelLength could not be resolved because it was within the generic namespace ActiveControl. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
		'UPGRADE_ISSUE: Control Text could not be resolved because it was within the generic namespace ActiveControl. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="084D22AD-ECB1-400F-B4C7-418ECEC5E36E"'
		VB6.GetActiveControl().SelLength = Len(VB6.GetActiveControl().Text)
		
	End Sub
	
	
	Function Round(ByRef x As Object, ByRef numDigits As Short) As Single
		
		'UPGRADE_WARNING: Couldn't resolve default property of object x. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Round = Int(x * (10 ^ numDigits)) / (10 ^ (numDigits))
		
	End Function
	
	
	Public Function ShowVals(ByRef Paramter As InstParameter) As String
		'DESCRIPTION:
		'   Shows values related to the parameter which the mouse pointer is passing over.
		'EXAMPLES:
		'   ShowVals(VOLT)
		'PARAMETERS:
		'   SetIdx%:    The index to a set of parallel global arrays (see below)
		'GLOBAL VARIABLES USED: NONE
		'REQUIRES:  EngNotate
		'VERSION:   9/6/96
		'CUSTODIAN: J. Hill
		Dim S As String
		
		S = "Min:" & EngNotate(Val(Paramter.SetMin), Paramter)
		If Paramter.SetDef <> "" Then
			S = S & "    Default:" & EngNotate(Val(Paramter.SetDef), Paramter)
		End If
		S = S & "    Max:" & EngNotate(Val(Paramter.SetMax), Paramter)
		
		'Return Function Value
		ShowVals = S
		
	End Function
	
	
	Sub SpinAbs(ByRef Direction As String, ByRef Parameter As InstParameter, ByRef Amount As Single)
		'DESCRIPTION:
		'   Increments or decrements SetCur$(SetIdx%) value
		'   by Amount! and returns it as a formatted string.
		'   Will not exceed Min and Max parameter values.
		'EXAMPLE:
		'   panDcOffs = SpinAbs("Down", MainParam(VOLT_OFFS), 0.5)
		'PARAMETERS:
		'   Direction$: Must be "Up" or "Down".  Specifies increment or decrement.
		'   Parameter:  The parameter to be modified
		'   Amount!:    The amount by which to inc. or dec. the current value.
		'REQUIRES:  EngNotate
		'VERSION:   8/13/96
		'CUSTODIAN: J. Hill
		Dim Tmp As Double
		'UPGRADE_NOTE: Step was upgraded to Step_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Step_Renamed As Double
		
		Tmp = Val(Parameter.SetCur)
		If UCase(Direction) = "UP" Then
			Tmp = Tmp + Amount
			If Tmp > Val(Parameter.SetMax) Then
				Tmp = Val(Parameter.SetMax)
			End If
		Else
			Tmp = Tmp - Amount
			If Tmp < Val(Parameter.SetMin) Then
				Tmp = Val(Parameter.SetMin)
			End If
		End If
		Parameter.SetCur = VB6.Format(Tmp)
		'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Parameter.TxtBox.Text = EngNotate(Tmp, Parameter)
		
	End Sub
	
	
	Sub Spin10Pct(ByRef Direction As String, ByRef Parameter As InstParameter)
		'DESCRIPTION:
		'   Increments or decrements SetCur$(SetIdx%) value
		'   by 10%, rounds it to the nearest MSD and returns
		'   it as a formatted string.
		'   Will not exceed Min and Max parameter values.
		'EXAMPLE:
		'   panAmpl.Caption = Spin10Pct("Down", MainParam(VOLT))
		'PARAMETERS:
		'   Direction$: Must be "Up" or "Down".  Specifies increment or decrement.
		'   Parameter:  The parameter to be modified
		'REQUIRES:  RoundMSD, EngNotate
		'VERSION:   10/30/96
		'CUSTODIAN: J. Hill
		Dim Tmp As Double
		'UPGRADE_NOTE: Step was upgraded to Step_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Step_Renamed As Double
		
		If Parameter.SetCur = "INF" And UCase(Direction) = "DOWN" Then
			Tmp = Val(Parameter.SetMax)
		Else
			Tmp = Val(Parameter.SetCur)
		End If
		If UCase(Direction) = "UP" Then
			If Tmp = 0 Then Tmp = 0.91 * Val(Parameter.SetMinInc)
			Tmp = RoundMSD(Direction, Tmp + System.Math.Abs(0.1 * Tmp))
			If Tmp > Val(Parameter.SetMax) Then
				Tmp = Val(Parameter.SetMax)
			End If
			If Tmp > -Val(Parameter.SetMinInc) And Tmp < 0 Then
				Tmp = 0
			End If
		Else
			If Tmp = 0 Then Tmp = -0.91 * Val(Parameter.SetMinInc)
			Tmp = RoundMSD(Direction, Tmp - System.Math.Abs(0.1 * Tmp))
			If Tmp < Val(Parameter.SetMin) Then
				Tmp = Val(Parameter.SetMin)
			End If
			If Tmp < Val(Parameter.SetMinInc) And Tmp > 0 Then
				Tmp = 0
			End If
		End If
		Parameter.SetCur = VB6.Format(Tmp)
		'UPGRADE_WARNING: Couldn't resolve default property of object Parameter.TxtBox.Text. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		Parameter.TxtBox.Text = EngNotate(Tmp, Parameter)
		
	End Sub
	
	
	Public Function RoundMSD(ByRef Direction As String, ByVal Number As Double) As Double
		'DESCRIPTION:
		'   Rounds Number to the nearest MSD (Most Significant Digit).
		'   In other words, it zeroes all digits but the MSD.
		'PARAMETERS:
		'   Direction$: Must be "Up" or "Down".  Signifies which
		'               way to round the number.
		'   Number#:    The number to be rounded.
		'EXAMPLES:
		'   Number=10987.1, Direction$="Down" -> 10000
		'   Number=10000, Direction$="Down" -> 9000
		'   Number=10987.1, Direction$="Up" -> 20000
		'GLOBAL VARIABLES USED: None
		'REQUIRES:  None
		'VERSION:   8/13/96
		'CUSTODIAN: J. Hill
		Dim Exponent As Short
		Dim NEGATIVE As Short
		
		Exponent = 0 : NEGATIVE = False
		If Number < 0 Then ' If negative
			Number = System.Math.Abs(Number) 'Make it positive for now
			NEGATIVE = True 'Set flag
			If UCase(Direction) = "UP" Then
				Direction = "DOWN"
			Else
				Direction = "UP"
			End If
		End If
		If Number >= 10 Then 'For positive exponent
			Do While Number >= 10
				Number = Number / 10#
				Exponent = Exponent + 1
			Loop 
		ElseIf Number < 1 And Number <> 0 Then  'For negative exponent (but not 0)
			Do While Number < 1
				Number = Number * 10#
				Exponent = Exponent - 1
			Loop 
		End If
		Number = Val(Str(Number)) ' Clear out very low LSBs from binary math
		If UCase(Direction) = "UP" Then
			Number = Fix(Number + 0.9) * 10 ^ Exponent
		Else
			Number = Fix(Number) * 10 ^ Exponent
		End If
		If NEGATIVE Then Number = -Number
		RoundMSD = Number
		
	End Function
	
	
	Public Sub CenterForm(ByRef FormToCenter As System.Windows.Forms.Form)
		'DESCRIPTION:
		'   Centers form on screen at any resolution
		'PARAMETERS:
		'   FormToCenter - a form object
		'EXAMPLE:
		'   CenterForm frmHPE1445A
		'GLOBAL VARIABLES USED: None
		'REQUIRES:  None
		'VERSION:   8/13/96
		'CUSTODIAN: J. Hill
		
		FormToCenter.Top = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height) / 2) - (VB6.PixelsToTwipsY(FormToCenter.Height) / 2))
		FormToCenter.Left = VB6.TwipsToPixelsX((VB6.PixelsToTwipsX(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width) / 2) - (VB6.PixelsToTwipsX(FormToCenter.Width) / 2))
		
	End Sub
	
	
	Sub CenterChildForm(ByRef ParentForm As System.Windows.Forms.Form, ByRef Childform As System.Windows.Forms.Form)
		'************************************************************
		'* ManTech Test Systems Software Module                     *
		'************************************************************
		'* Nomenclature   : Instrument About Box / Splash Screen    *
		'* Written By     : David W. Hartley                        *
		'*    DESCRIPTION:                                          *
		'*     This Module Centers One Form With Respect To The     *
		'*     Current Position of Another Form.                    *
		'*    EXAMPLE:                                              *
		'*     CenterChildForm frmMain, frmAboutBox                 *
		'************************************************************
		Childform.Top = VB6.TwipsToPixelsY(((VB6.PixelsToTwipsY(ParentForm.Height) / 2) + VB6.PixelsToTwipsY(ParentForm.Top)) - VB6.PixelsToTwipsY(Childform.Height) / 2)
		Childform.Left = VB6.TwipsToPixelsX(((VB6.PixelsToTwipsX(ParentForm.Width) / 2) + VB6.PixelsToTwipsX(ParentForm.Left)) - VB6.PixelsToTwipsX(Childform.Width) / 2)
		
	End Sub
	
	
	Public Sub Delay(ByVal dSeconds As Double)
		'DESCRIPTION:
		'   Delays for a specified number of seconds.
		'   50 milli-second resolution.
		'   Minimum delay is about 50 milli-seconds.
		'PARAMETERS:
		'   dSeconds: The number of seconds to Delay.
		Dim t As Double
		
		t = dGetTime()
		dSeconds = dSeconds / SECS_IN_DAY
		Do 
			System.Windows.Forms.Application.DoEvents()
		Loop While dGetTime() - t < dSeconds
		
	End Sub
	
	
	Public Function dGetTime() As Double
		'DESCRIPTION:
		'   Determines the time since 1900.
		'   Avoids errors caused by 'Timer' when crossing midnight.
		'PARAMETERS:
		'   None
		'RETURNS:
		'   A double-precision floating-point number in the format d.s where:
		'     d = the number of days since 1900 and
		'     s = the fraction of a day with 10 milli-second resolution
		
		'The CDbl(Now) function returns a number formatted d.s where:
		' d = the number of days since 1900 and
		' s = the fraction of a day with 10 milli-second resolution.
		'CLng(Now) returns only the number of days (the "d." part).
		'Timer returns the number of seconds since midnight with 10 mSec resoulution.
		'So, the following statement returns a composite double-float number representing
		' the time since 1900 with 10 mSec resoultion. (no cross-over midnight bug)
		
		dGetTime = CLng(DateSerial(Year(Now), Month(Now), VB.Day(Now)).ToOADate) + (VB.Timer() / SECS_IN_DAY)
		
	End Function
	
	
	Sub VerifyInstrumentCommunication(ByRef ResrcName As String)
		'************************************************************
		'* ManTech Test Systems Software Module                     *
		'************************************************************
		'* Nomenclature   : HP E1428A Oscilloscope Front Panel      *
		'* Written By     : Michael McCabe                          *
		'*    DESCRIPTION:                                          *
		'*     Verifies Communication With Message Delivery System  *
		'*    EXAMPLE:                                              *
		'*      LiveMode% = VerifyInstrumentCommunication%          *
		'************************************************************
		Dim Ret As Integer
		Dim lpBuffer As New VB6.FixedLengthString(80)
		Dim Pass As Short 'Instrument Pass Test (True/False)
		Dim SystemInit As Short
		Dim InVar As String
		Dim SystErr As Short
		Dim VisaLibrary As String
		Dim SystemDir As String
		Dim Identification As String
		Dim Response As String
		Dim XmlBuf As String
		Dim Allocation As String
		Const VI_NULL As Short = 0
		
		Pass = False 'Until Proven True
		ErrorStatus = 1
		
		Ret = GetSystemDirectory(lpBuffer.Value, 80)
		SystemDir = StripNullCharacters(lpBuffer.Value)
		VisaLibrary = SystemDir & "\VISA32.DLL"
		
		ResourceName = "DSO_1"
		
		'If FileExists(VisaLibrary$) Then
		'Determine If The DSCOPE Is Functioning
		'ErrorStatus& = hpe1428a_init(resourceName:=ResrcName$, IDQuery:=1, resetDevice:=1, instrumentHandle:=instrumentHandle&)
		On Error GoTo ErrHandle
		ErrorStatus = atxml_Initialize(proctype, guid)
		If ErrorStatus <> 0 Then
			MsgBox("Instrument Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
			LiveMode = False
			Exit Sub
		Else
			Pass = True
			
			Response = Space(4096)
			
			XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "<ResourceType>Source</ResourceType>" & "<SignalResourceName>DSO_1</SignalResourceName> " & "</ResourceRequirement>" & "</AtXmlTestRequirements>"
			
			Allocation = "PawsAllocation.xml"
			
			ErrorStatus = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
			
			'Parse Availability XML string to get the status(Mode) of the instrument
			iStatus = gctlParse.ParseAvailiability(Response)
			
			'  Set TimeOut to 10 Seconds
			ErrorStatus = atxmlDF_viSetAttribute(ResourceName, instrumentHandle, VI_ATTR_TMO_VALUE, 10000)
			
		End If
		'   Else
		'       MsgBox "VISA Message Delivery System Is Not Installed. Live Mode Disabled.", vbExclamation
		'   End If
		
		LiveMode = Pass
		
		Exit Sub
		'****************************************
ErrHandle: 
		If Err.Number = 53 Then
			MsgBox("unable to locate AtXmlApi.dll", MsgBoxStyle.Critical)
			Resume Next
		Else
			Debug.Print("Description :" & Err.Description)
			Debug.Print("Number:" & Err.Number)
		End If
		
	End Sub
	
	
	Function ReadMsg() As String
		Dim NBR As Integer
		Dim BlinkTime As Single
		Dim ReadBuffer As New VB6.FixedLengthString(8100)
		Dim BufferFull As Integer
		Dim CaptionOn As Short
		Dim MsgRespReg As Integer
		Dim BlinkRate As Single
		Dim PresentBusAccessCount As Short
		Dim InteruptingCode As Short
		Dim SystErr As Integer
		Dim count As Integer
		
		
		bReadPending = True
		PresentBusAccessCount = BusAccessCount
		BlinkRate = 1
		
		If LiveMode Then
			BlinkTime = VB.Timer()
			
			If (BusAccessCount <> PresentBusAccessCount) Then
				SoftReset()
				InteruptingCode = PresentBusAccessCount + 1
				If InteruptingCode > 1000 Then
					InteruptingCode = 0
				End If
				'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, QueryCode$(InteruptingCode%))
				ErrorStatus = atxml_WriteCmds(ResourceName, QueryCode(InteruptingCode), count)
				
				Do 
					ReadBuffer.Value = ""
					'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, "SYST:ERR?")
					ErrorStatus = atxml_WriteCmds(ResourceName, "SYST:ERR?", count)
					
					'ErrorStatus& = hpe1428a_readInstrData(instrumentHandle&, numberBytesToRead:=8100, ReadBuffer:=ReadBuffer$, numBytesRead:=NBR&)
					ErrorStatus = atxml_ReadCmds(ResourceName, ReadBuffer.Value, 8100, NBR)
					
				Loop While (Val(StripNullCharacters(ReadBuffer.Value)) <> 0)
				If QueryCode(PresentBusAccessCount) = "WAV:PRE?" Then
					'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, "DIG CHAN1,CHAN2")
					ErrorStatus = atxml_WriteCmds(ResourceName, "DIG CHAN1,CHAN2", count)
					
					SetMathFunction(False)
					'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, "WAV:SOUR FUNC1")
					ErrorStatus = atxml_WriteCmds(ResourceName, "WAV:SOUR FUNC1", count)
					'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, "VIEW FUNC1")
					ErrorStatus = atxml_WriteCmds(ResourceName, "VIEW FUNC1", count)
					'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, "WAV:PRE?")
					ErrorStatus = atxml_WriteCmds(ResourceName, "WAV:PRE?", count)
					
				Else
					'ErrorStatus& = hpe1428a_writeInstrData(instrumentHandle&, QueryCode$(PresentBusAccessCount%))
					ErrorStatus = atxml_WriteCmds(ResourceName, QueryCode(PresentBusAccessCount), count)
				End If
				PresentBusAccessCount = BusAccessCount
			End If
			'WMT (not needed) SystErr& = hpe1428a_msgWaiting(instrumentHandle&, BufferFull&)
			' If Timer() - BlinkTime! > BlinkRate! And (MakingMeasurement% Or Digitizing%) Then
			'     BlinkRate! = 0.3
			'     If CaptionOn% Then
			'         StatusBarMessage ""
			'         CaptionOn% = False
			'     Else
			'         StatusBarMessage "Waiting for Oscilloscope Response"
			'         CaptionOn% = True
			'     End If
			'     BlinkTime! = Timer()
			'     DoEvents
			' End If
			'Loop While Not BufferFull&
			bReadPending = False
			If BlinkRate <> 1 Then
				StatusBarMessage("")
			End If
			
			ErrorStatus = atxml_ReadCmds(ResourceName, ReadBuffer.Value, 8100, NBR)
			
			If NBR > 0 Then
				ReadBuffer.Value = Left(ReadBuffer.Value, NBR - 1)
			Else
				ReadBuffer.Value = ""
			End If
			
			If ErrorStatus Then
				If ErrorStatus Then DisplayErrorMessage(ErrorStatus)
				frmZT1428.txtDataDisplay.Text = "Ready"
			Else
				ReadMsg = Trim(ReadBuffer.Value)
			End If
		Else
			'   ##########################################
			'   Action Required: Remove debug code
			'   This will enable simulation of commands
			'    If gcolCmds.count <> 0 Then
			'        ReadBuffer$ = gctlDebug.Simulate(gcolCmds)
			
			'       Clear collection
			'        Set gcolCmds = New Collection
			'        ErrorStatus& = 0
			'    End If
			'   ##########################################
		End If
		
		
		'ReadMsg$ = Left(ReadMsg$, NBR&)     'Return only the instrument data
		
	End Function
	
	
	'UPGRADE_NOTE: Command was upgraded to Command_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Sub WriteMsg(ByRef Command_Renamed As String)
		Dim ReadBuffer As New VB6.FixedLengthString(8100)
		Dim NBR As Integer
		Dim ErrorCode As Integer
		Dim retCount As Integer
		Dim ClearStatus As Integer
		Dim tempString As String
		
		
		'Debug.Print Command$
		If Not LiveMode Then Exit Sub
		If Command_Renamed = "" Then Exit Sub
		tempString = Left(Command_Renamed, 5)
		'If SFP is first coming up do not send cmds to instr. except queries
		If bInstrumentMode And InStr(Command_Renamed, "?") = 0 Then
			Exit Sub
		End If
		If bInstrumentMode And tempString = "STAT?" Then
			Exit Sub
		End If
		
		If frmZT1428.Panel_Conifg.DebugMode = True And InStr(Command_Renamed, "?") = 0 Then
			Exit Sub
		End If
		
		BusAccessCount = BusAccessCount + 1
		If BusAccessCount > 1000 Then
			BusAccessCount = 0
		End If
		QueryCode(BusAccessCount) = Command_Renamed
		NBR = Len(Command_Renamed)
		'ErrorStatus& = viWrite(ByVal instrumentHandle&, ByVal Command$, ByVal NBR&, retCount&)
		ErrorStatus = atxml_WriteCmds(ResourceName, Command_Renamed, retCount)
		
		If ErrorStatus Then
			If ErrorStatus = VI_ERROR_TMO Then 'For Untriggered trigger mode
				' issue vi_clear to clear bus
				'clearStatus = atxmlDF_viClear(ResourceName, 255&)
				'Delay 0.5
				'If ErrorStatus& = VI_SUCCESS Then
				'ErrorStatus& = viWrite(ByVal instrumentHandle&, ByVal Command$, ByVal NBR&, retCount&)
				'    ErrorStatus& = atxml_WriteCmds(ResourceName, Command$, retCount&)
				'Else
				MsgBox("VISA Timeout Sending Command: " & Command_Renamed)
				LiveMode = False
				'End If
				' issue vi_clear to clear bus
				' clearStatus = atxmlDF_viClear(ResourceName, 255&)
			End If
			If ErrorStatus Then DisplayErrorMessage(ErrorStatus)
			ClearStatus = atxmlDF_viClear(ResourceName, 255)
		End If
		
	End Sub
	
	
	Sub DisplayErrorMessage(ByRef ErrorStatus As Integer)
		'DESCRIPTION:
		'   This Module produces a text error message in response
		'   to a VISA error and reports to TIP if in TIP mode.
		'PARAMETERS:
		'   ErrorStatus& - The VISA error number.
		'EXAMPLE:
		'   DisplayErrorMessage (VisaFunctionReturnValue&)
		
		Dim nRetVal As Integer 'Return Value
		Dim lpErrorText As New VB6.FixedLengthString(256) '256 Byte Error Text Buffer
		Dim ClearStatus As Integer
		
		'This Sub Routine is for Debugging Purposes
		'WMT (not used)   nRetVal = hpe1428a_errorMessage(instrumentHandle:=instrumentHandle&, ErrorCode:=ErrorStatus&, ErrorMessage:=lpErrorText)
		If ErrorStatus = VI_ERROR_TMO Then
			
			frmZT1428.txtDataDisplay.Text = "Error"
			
			MsgBox(lpErrorText.Value, MsgBoxStyle.Exclamation, "VISA Time-Out Error")
			If Left(sTIP_Mode, 7) = "TIP_RUN" Then
				SetKey("TIPS", "STATUS", "Error from VISA: " & Hex(ErrorStatus) & " " & lpErrorText.Value)
				EndProgram()
			End If
		End If
		' issue vi_clear to clear bus
		ClearStatus = atxmlDF_viClear(ResourceName, 255)
		
	End Sub
	
	
	Public Function EngNotate(ByVal Number As Double, ByRef Parameter As InstParameter) As String
		'DESCRIPTION:
		'   Returns passed number as numeric string in Engineering notation (every
		'   3rd exponent) with selectable precision along with Unit Of Measure.
		'EXAMPLES:
		'   With SetUOM$(SetIdx%)="Ohm" and SetRes$(SetIdx%)="D3",
		'       EngNotate$(10987.1, MEAS_RES) returns "10.987 KOhm"
		'   With SetUOM$(SetIdx%)="Sec" and SetRes$(SetIdx%)="D2",
		'       EngNotate$(5.43e-5, MEAS_PER) returns "54.30 uSec"
		'PARAMETERS:
		'   Number#:    The number to be formatted
		'   SetIdx%:    The index to a set of parallel global arrays (see below)
		'GLOBAL VARIABLES USED:
		'   SetRes$():  SETtings RESolution array:
		'               "A0" = Absolute integer resolution
		'               "An" where 'n' is digits of displayed resolution
		'               "D0" = integer resolution
		'               "Dn" where 'n' is digits of displayed resolution
		'               "N0" = integer resolution with no engineering notation
		'               "Nn" where 'n' is digits of displayed resolution
		'       Note: Use "An" when resolution is specified as an absolute value.
		'             Use "Dn" when resolution is specified as "digits".
		'   SetUOM$():  SETtings Unit-Of-Measure array:
		'               "V", "Vp-p", "Vpk", "A", "Hz", "S",
		'               "%", "% Mod", "Deg", or ""
		'REQUIRES:  None
		'VERSION:   9/9/96
		'CUSTODIAN: M. McCabe
		Dim MULTIPLIER As Short
		Dim NEGATIVE As Short
		Dim Digits As Short
		Dim Prefix As String
		Dim ReturnString As String
		
		MULTIPLIER = 0 : NEGATIVE = False 'Initialize local variables
		
		If Number < 0 Then ' If negative
			Number = System.Math.Abs(Number) 'Make it positive for now
			NEGATIVE = True 'Set flag
		End If
		
		If Number >= 1000 Then 'For positive exponent
			Do While Number >= 1000 And MULTIPLIER <= 4
				Number = Number / 1000
				MULTIPLIER = MULTIPLIER + 1
			Loop 
		ElseIf Number < 1 And Number <> 0 Then  'For negative exponent (but not 0)
			Do While Number < 1 And MULTIPLIER >= -4
				Number = Number * 1000
				MULTIPLIER = MULTIPLIER - 1
			Loop 
		End If
		
		Select Case MULTIPLIER
			Case 4 : Prefix = " T" 'Tetra  E+12
			Case 3 : Prefix = " G" 'Giga   E+09
			Case 2 : Prefix = " M" 'Mega   E+06
			Case 1 : Prefix = " K" 'kilo   E+03
			Case 0 : Prefix = " " '<none> E+00
			Case -1 : Prefix = " m" 'milli  E-03
			Case -2 : Prefix = " " & Chr(181) 'micro  E-06
			Case -3 : Prefix = " n" 'nano   E-09
			Case -4 : Prefix = " p" 'pico   E-12
			Case Else
				Prefix = " "
				Number = 0
				MULTIPLIER = 0
		End Select
		
		If NEGATIVE Then Number = -Number
		
		If MULTIPLIER > 4 Then
			ReturnString = "Ovr Rng"
		Else
			Number = Val(Str(Number)) ' Clear out very low LSBs from binary math
			If Left(Parameter.SetRes, 1) = "N" Then 'JHill Added per TDR99153
				Digits = Val(Mid(Parameter.SetRes, 2, 1)) ' "      "    "      "
				Prefix = " " ' "      "    "      "
				Number = Number * 10 ^ (MULTIPLIER * 3) ' "      "    "      "
			ElseIf Left(Parameter.SetRes, 1) = "D" Then  'Digits of resolution
				Digits = Val(Mid(Parameter.SetRes, 2, 1)) - Len(VB6.Format(Int(System.Math.Abs(Number))))
				If Digits < 0 Then Digits = 0
				Number = Round(Number, Digits) 'JHill Added per TDR99113
			ElseIf Left(Parameter.SetRes, 1) = "A" Then  'Absolute Digits of resolution
				Digits = Val(Mid(Parameter.SetRes, 2, 1)) + (MULTIPLIER * 3)
				If Digits < 0 Then Digits = 0
				Number = Round(Number, Digits) 'JHill Added per TDR99113
			Else
				Digits = -1
			End If
			Select Case Digits
				Case 0 : ReturnString = VB6.Format(Number, "0")
				Case 1 : ReturnString = VB6.Format(Number, "0.0")
				Case 2 : ReturnString = VB6.Format(Number, "0.00")
				Case 3 : ReturnString = VB6.Format(Number, "0.000")
				Case 4 : ReturnString = VB6.Format(Number, "0.0000")
				Case 5 : ReturnString = VB6.Format(Number, "0.00000")
				Case 6 : ReturnString = VB6.Format(Number, "0.000000")
				Case 7 : ReturnString = VB6.Format(Number, "0.0000000")
				Case 8 : ReturnString = VB6.Format(Number, "0.00000000")
				Case 9 : ReturnString = VB6.Format(Number, "0.000000000")
				Case Else : ReturnString = VB6.Format(Number, "0.0#######")
			End Select
		End If
		
		EngNotate = ReturnString & Prefix & Parameter.SetUOM
		
	End Function
	
	
	Function FileExists(ByRef Path As String) As Short
		'************************************************************
		'* ManTech Test Systems Software Module                     *
		'************************************************************
		'* Nomenclature   : HP E1212A Digital Multimeter Front Panel*
		'* Written By     : David W. Hartley                        *
		'*    DESCRIPTION:                                          *
		'*     This Module Checks To See If A Disk File Exists      *
		'*    EXAMPLE:                                              *
		'*     IsFileThere% = FileExists("C:\ISFILE.EX")            *
		'*    RETURNS:                                              *
		'*     TRUE if File is present.                             *
		'*     False if File is not present.                        *
		'************************************************************
		Dim x As Integer
		
		x = FreeFile
		
		On Error Resume Next
		FileOpen(x, Path, OpenMode.Input)
		If Err.Number = 0 Then
			FileExists = True
		Else
			FileExists = False
		End If
		FileClose(x)
		
	End Function
	
	
	Function StripNullCharacters(ByRef Parsed As String) As String
		'DESCRIPTION:
		'   This routine strips characters with ASCII values less than 32 from the
		'   end of a string
		'PARAMTERS:
		'   Parsed$ = String from which to remove null characters
		'RETURNS:
		'   A the reultant parsed string
		Dim x As Short
		
		For x = Len(Parsed) To 1 Step -1
			If Asc(Mid(Parsed, x, 1)) > 32 Then
				Exit For
			End If
		Next x
		
		'Return Function Value
		StripNullCharacters = Left(Parsed, x)
		
	End Function
	
	
	
	Sub SetKey(ByRef sSection As String, ByRef sKey As String, ByRef sKeyVal As String)
		'DESCRIPTION:
		'   This function sets a key value in VIPERT.INI
		'PARAMETERS:
		'   sSection    The VIPERT.INI file section where the key is located.
		'   sKey        The Key name.
		'   sKeyVal     The string to set the key value to.
		'GLOBAL VARIABLES USED:
		'   sWindowsDir
		'EXAMPLE:
		'   SetKey "TIPS", "STATUS", "Ready"
		
		WritePrivateProfileString(sSection, sKey, sKeyVal, sWindowsDir & "\VIPERT.INI")
		
	End Sub
	
	Function sGetKey(ByRef sSection As String, ByVal sKey As String) As String
		'DESCRIPTION:
		'   This function retrieves a key value from VIPERT.INI
		'PARAMETERS:
		'   sSection    The VIPERT.INI file section where the key is located.
		'   sKey        The Key name.
		'RETURNS:
		'   The key value.
		'GLOBAL VARIABLES USED:
		'   sWindowsDir
		'EXAMPLE:
		'   sTipCmds = sGetKey("TIPS", "CMD")
		Dim lpReturnedString As New VB6.FixedLengthString(5000)
		Dim nNumChars As Integer
		
		'Clear String Buffer
		lpReturnedString.Value = Space(255)
		
		nNumChars = GetPrivateProfileString(sSection, sKey, "", lpReturnedString.Value, Len(lpReturnedString.Value), sWindowsDir & "\VIPERT.INI")
		sGetKey = Left(lpReturnedString.Value, nNumChars)
		
	End Function
	
	Function sMeasQuery() As String
		'DESCRIPTION:
		'   This function is used to query the instrument for the selected
		'   measurement function.  It is used instead of function ReadMsg$()
		'   to cleanly recover from a VISA timeout in the event of no
		'   trigger when Acquisition Mode is "Triggered".
		'RETURNS:
		'   The measured value.
		'EXAMPLE:
		'   sMeasured = sMeasQuery()
		Dim nNBR As Integer
		Dim fBlinkTime As Single
		Dim fBlinkRate As Single
		Dim fTimeOut As Single
		Dim lpReadBuffer As New VB6.FixedLengthString(8100)
		Dim bCaptionOn As Boolean
		Dim ClearStatus As Integer
		
		If Not LiveMode Then Exit Function
		
		'Prevents lockup caused by measurement query when (in Triggered mode AND trigger is absent)
		If bTrigMode Then
			WriteMsg("STOP")
		End If
		WriteMsg(MeasureCode(CurrentMeas).SetCod)
		fTimeOut = VB.Timer() + (dAcqTime * 1.5)
		fBlinkRate = 1
		fBlinkTime = VB.Timer()
		bCaptionOn = True 'To prevent an initial "Wait for Trig" display
		Do 
			If VB.Timer() - fBlinkTime > fBlinkRate Then
				fBlinkRate = 0.3
				If bCaptionOn Then
					StatusBarMessage("")
					bCaptionOn = False
				Else
					StatusBarMessage("Waiting for Trigger")
					bCaptionOn = True
				End If
				fBlinkTime = VB.Timer()
			End If
			lpReadBuffer.Value = ""
			'ErrorStatus& = hpe1428a_readInstrData(instrumentHandle&, numberBytesToRead:=8100, _
			''               ReadBuffer:=lpReadBuffer, numBytesRead:=nNBR)
			
			'only do the read if there is not an error from the WriteCmd
			If Not ErrorStatus Then
				ErrorStatus = atxml_ReadCmds(ResourceName, lpReadBuffer.Value, 8100, nNBR)
			End If
			
			System.Windows.Forms.Application.DoEvents()
		Loop While (VB.Timer() < fTimeOut) And (nNBR = 0)
		If bCaptionOn Then
			StatusBarMessage("")
		End If
		
		'Restore RUN
		If bTrigMode Then
			WriteMsg("RUN")
		End If
		
		If ErrorStatus = VI_ERROR_TMO Then 'For Untriggered trigger mode
			' issue vi_clear to clear bus
			ClearStatus = atxmlDF_viClear(ResourceName, 255)
			
			sMeasQuery = "0"
			StatusBarMessage("No Trigger")
			Delay(1)
		ElseIf ErrorStatus Then 
			DisplayErrorMessage(ErrorStatus)
			' issue vi_clear to clear bus
			ClearStatus = atxmlDF_viClear(ResourceName, 255)
			WriteMsg("*CLS") 'Clear instrument error
			sMeasQuery = "0"
		Else
			StatusBarMessage("")
			sMeasQuery = Left(lpReadBuffer.Value, nNBR) 'Return only the instrument data
		End If
		
	End Function
End Module