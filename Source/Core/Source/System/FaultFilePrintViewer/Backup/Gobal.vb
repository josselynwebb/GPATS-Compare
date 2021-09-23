Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Module Gobal
	'-----------------------------------------------------------
	' Contract Tracking
	'-----------------------------------------------------------
	' Global.bas
	'-----------------------------------------------------------
	' Contains all Global variable declarations and Subs/Functions
	'
	' Known Problems:
	'
	' Version Info:
	'   Ver    Date      By   Comment
	'   1.1.0  11/08/99  JFO  Baseline
	'-----------------------------------------------------------
	
	
	Public Const LOCALE_SLONGDATE As Short = &H1Fs
	
	Public selectedAPSName As String
	Public selectedTPSName As String
	Public selectedSerialNumber As String
	Public selectedERONumber As String
	Public selectedFromRunDate As Date
	Public selectedToRunDate As Date
	
	Public APSNameBeenSelected As Boolean
	Public TPSNameBeenSelected As Boolean
	Public SerialNumberBeenSelected As Boolean
	Public ERONumberBeenSelected As Boolean
	Public FromRunDateBeenSelected As Boolean
	Public ToRunDateBeenSelected As Boolean
	Public DateSelected As Boolean
	
	Public tvSelectApsName As String
	Public tvSelectTpsName As String
	Public tvSelectSerialNumber As String
	Public tvSelectRunDate As String
	
	Public lastID As String
	Public savedID As String
	Public BeenSelected As Short
	
	Public GPNamMode As Boolean
	Public IDToDelete As String
	
	Public Declare Function GetSystemDefaultLCID Lib "kernel32" () As Integer
	
	
	
	Public Declare Function SetLocaleInfo Lib "kernel32"  Alias "SetLocaleInfoA"(ByVal Locale As Integer, ByVal LCType As Integer, ByVal lpLCData As String) As Boolean
	
	'UPGRADE_WARNING: Application will terminate when Sub Main() finishes. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
	Public Sub Main()
		Dim junk As Short
		Dim rs As ADODB.Recordset
		
		OpenDB("FaultFile")
		
		rs = rsOpen("SELECT [TPSName], [SerialNumber], [APSName] FROM Faults", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		'If no records returned then this isn't a valid user
		If rs.BOF = True And rs.EOF = True Then
			junk = MsgBox("There was an Error Starting this Application" & vbCrLf & vbCrLf & "The Possible Problems are as Follows:" & vbCrLf & vbCrLf & "1. You don't have permission to use this application." & vbCrLf & "2. The Database is Empty" & vbCrLf & vbCrLf & "Database File: D:\database\FaultFile.mdb", MsgBoxStyle.OKOnly, "Fault File Error")
			rs.Close()
			End
			Exit Sub
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		
		CloseDB("FaultFile")
		
		If VB.Command() = "PrintIt" Or VB.Command() = "PRINTIT" Or VB.Command() = "printit" Then
			GPNamMode = True
		ElseIf VB.Command() = "error" Or VB.Command() = "ERROR" Or VB.Command() = "Error" Then 
			junk = MsgBox("There was an Error Processing the Information" & "You Wanted to Print." & vbCrLf & "The Error Seems to be in the File" & vbCrLf & "C:\APS\DATA\FAULTFILE" & vbCrLf & "Sorry", MsgBoxStyle.OKOnly, "Fault File Error")
			Exit Sub
		Else
			GPNamMode = False
		End If
		
		frmSearch.Show()
		'UPGRADE_ISSUE: Load statement is not supported. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B530EFF2-3132-48F8-B8BC-D88AF543D321"'
		Load(frmSearch)
		
	End Sub
End Module