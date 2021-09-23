'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Text
Imports Microsoft.VisualBasic

Public Module TetsSaisLibrary

	'=========================================================
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
    '// Instrument: Ri3152A Function Generator (Fg)
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
    '* Third Echelon Test System (TETS) Software Module         *
    '*                                                          *
    '* Nomenclature   : SAIS Library Functions                  *
    '* Written By     : ManTech Test Systems                    *
    '************************************************************

    '-----------------Global Variables----------------------------
    ' Add these declarations to your Main.Bas declarations.
    ' Not all elements need be filled.
    'Global SetCur$(1 To MAX_SETTINGS)    ' "Current Settings" Array
    'Global SetDef$(1 To MAX_SETTINGS)    ' "Default Settings" Array
    'Global SetMin$(1 To MAX_SETTINGS)    ' "Maximum Settings" Array
    'Global SetMax$(1 To MAX_SETTINGS)    ' "Minimum Settings" Array
    'Global SetUOM$(1 To MAX_SETTINGS)    ' "Unit Of Measure" Array
    ' Examples: "Vdc", "Vpk", "A", "Hz", "S", "% Mod", "Deg", and ""
    'Global SetRes$(1 To MAX_SETTINGS)    ' "Setting Resolution" Array
    ' "A0" = Absolute integer resolution
    ' "An" where 'n' is digits of displayed resolution
    ' "D0" = integer resolution
    ' "Dn" where 'n' is digits of displayed resolution
    ' "N0" = integer resolution with no engineering notation
    ' "Nn" where 'n' is digits of displayed resolution
    ' Note: Use "An" when resolution is specified as an absolute value.
    '       Use "Dn" when resolution is specified as "digits".
    'Global SetMinInc$(1 To MAX_SETTINGS) ' "Minimum Increment Setting" Array
    'Global SetCod$(1 To MAX_SETTINGS)    ' "SCPI Code" Array
    '-------------------------------------------------------------

    Public LastDirection As String ' Indicates the last spin direction
    Public Quote As String ' Holds Quote mark - Chr$(34)
    Public LiveMode As Boolean ' Instrument Communication flag
    Public UserEnteringData As Boolean ' Indicates when the user is entering data
    Public DoNotTalk As Boolean ' "Don't Talk" to instrument flag
    Public instrumentHandle As Integer ' Instrument Address
    Public ErrorStatus As Integer ' Return status for instrument calls
    Public ReadBuffer As String ' Holds return strings from the instrument
    Public Void As Object ' A "don't care" variable

    Function Round(ByVal X As Double, ByVal NumDigits As Short) As Double
        'DESCRIPTION:
        '   Rounds the decimal part of a number.
        'PARAMETERS:
        '   X:      The floating point value to be rounded.
        '   numDigits%: The number of digits to leave to the right of the
        '                   decimal point.
        'EXAMPLE:
        '   X = 10.9871, numDigits% = 2 returns 10.98
        'GLOBAL VARIABLES USED: None
        'REQUIRES:  None
        'VERSION:   1/17/97
        'CUSTODIAN: J. Hill

        Round = Int(X * (10 ^ (NumDigits)) + 0.5) / (10 ^ (NumDigits))
    End Function

    Public Sub FormatEntry(ByRef Num As TextBox, ByVal SetIdx As Short)
        'DESCRIPTION:
        '   Formats user-entered values, or "Min", "Max", "Def" or "Inf".
        '   Resets out-of-range values to Min or Max values and pops-up warning.
        '   Stores value in SetCur$() array.
        '   Sends the current parameter setting to the instrument.
        'EXAMPLES:
        '   Fmt txtAmpl, VOLT
        'PARAMETERS:
        '   Num:    The TextBox the user entered a number into which will be
        '              formatted by this procedure.
        '   SetIdx%:    The index to a set of parallel global arrays (see below)
        'GLOBAL VARIABLES USED:
        '   SetCur$():  CURrent value SETting array
        '   SetDef$():  DEFault value SETting array
        '   SetMin$():  MINimum value SETting array
        '   SetMax$():  MAXimum value SETting array
        '   SetRes$():  SETting RESolution array:
        '   SetUOM$():  SETtings Unit-Of-Measure array:
        '   UserEnteringData%
        'REQUIRES:  EngNotate, SendCommand
        'VERSION:   2/26/97
        'CUSTODIAN: J. Hill

        Dim i As Integer, c As String, n As String, Eng As String = "", Exponent As Short

        If UCase(Strings.Left(LTrim(Num.Text), 3)) = "MIN" Or UCase(Strings.Left(LTrim(Num.Text), 1)) = "N" Then
            SetCur(SetIdx) = SetMin(SetIdx)
            Num.Text = EngNotate(Val(SetCur(SetIdx)), SetIdx)
        ElseIf UCase(Strings.Left(LTrim(Num.Text), 3)) = "MAX" Or UCase(Strings.Left(LTrim(Num.Text), 1)) = "X" Then
            SetCur(SetIdx) = SetMax(SetIdx)
            Num.Text = EngNotate(Val(SetCur(SetIdx)), SetIdx)
        ElseIf UCase(Strings.Left(LTrim(Num.Text), 1)) = "D" Then            'Default
            If SetDef(SetIdx) <> "" Then
                SetCur(SetIdx) = SetDef(SetIdx)
            End If
            Num.Text = EngNotate(Val(SetCur(SetIdx)), SetIdx)
        ElseIf UCase(Strings.Left(LTrim(Num.Text), 3)) = "INF" Then            'Infinity
            SetCur(SetIdx) = "INF"
            Num.Text = SetCur(SetIdx)
        Else
            n = ""
            For i = 1 To Len(Num.Text) 'Look for a possible Eng Notation character
                c = Mid(Num.Text, i, 1)
                If Not IsNumeric(c) And c <> " " And c <> "e" And c <> "E" And c <> "-" And c <> "." And c <> "," Then
                    Eng = c
                    Exit For
                End If
                If c <> "," Then 'Filter out any commas
                    n &= c
                End If
            Next i
            Select Case Eng
                Case ""
                    Exponent = 0
                Case "p"
                    Exponent = -12
                Case "n"
                    Exponent = -9
                Case Convert.ToString(Chr(181)), "u"
                    Exponent = -6
                Case "m"
                    Exponent = -3
                Case "k", "K"
                    Exponent = 3
                Case "M"
                    Exponent = 6
                Case "G"
                    Exponent = 9
                Case "T", "t"
                    Exponent = 12

                Case Else
                    Exponent = 0
            End Select
            Num.Text = Val(n) * 10 ^ Exponent

            If Convert.ToDouble(Num.Text) < Convert.ToDouble(SetMin(SetIdx)) Or Convert.ToDouble(Num.Text) > Convert.ToDouble(SetMax(SetIdx)) Then
                MsgBox(GetVals(SetIdx), MsgBoxStyle.Information, "Invalid Value")
                Num.Text = EngNotate(Val(SetCur(SetIdx)), SetIdx)
                UserEnteringData = False
                SetCur(SetIdx) = SetDef(SetIdx)
                Exit Sub
            Else
                SetCur(SetIdx) = Num.Text
            End If
            Num.Text = EngNotate(Val(SetCur(SetIdx)), SetIdx)
        End If

        If Not frmAbout.Visible Then
            frmRac3152.tabOptions.Focus()
        End If
        'EADS Removed 5/11/2006
        'frmRac3152.tabOptions.SetFocus

        UserEnteringData = False
        SendCommand(SetIdx)
    End Sub

    Function GetVals(ByVal SetIdx As Short) As String
        GetVals = ""

        GetVals = "Min:" & EngNotate(Val(SetMin(SetIdx)), SetIdx)
        If SetDef(SetIdx)<>"" And SetDef(SetIdx)<>"INF" Then
            GetVals = GetVals & "    Def:" & EngNotate(SetDef(SetIdx), SetIdx)
        End If
        GetVals = GetVals & "    Max:" & EngNotate(Val(SetMax(SetIdx)), SetIdx)
    End Function

    'Sub GotFocusSelect()
    '    'DESCRIPTION:
    '    '   Highlights (selects) the TextBox text.
    '    '   Call from the GotFocus event of the subject TextBox.
    '    'EXAMPLE:       GotFocusSelect
    '    'PARAMETERS:    None
    '    'GLOBAL VARIABLES USED:
    '    '   UserEnteringData%
    '    '   DoNotTalk%
    '    'REQUIRES:      Nothing
    '    'VERSION:       2/20/97
    '    'CUSTODIAN:     J. Hill

    '    If DoNotTalk Then Exit Sub
    '    If GetVB6Name(VB6.GetActiveControl())="tabOptions" Then Exit Sub

    '    'Set Error Handler to handle a Run Time Error when Active Control
    '    'does not have a "Text" Property. This will allow any Errors encountered
    '    'while executing the following lines of code to be ignored.
    '    Try ' On Error GoTo ErrorHandler

    '        UserEnteringData = True
    '        VB6.GetActiveControl()SelStart = 0
    '        VB6.GetActiveControl()SelLength = Len(VB6.GetActiveControl().Text)
    '        Exit Sub 'Prevent from dropping into the Error Handler

    '    Catch	' ErrorHandler:
    '        Err.Clear()

    '    End Try
    'End Sub

    Public Sub HelpPanel(ByVal Text As String)
        'DESCRIPTION:
        '   Displays "Text$" on a Status Bar names 'sbrUserInformation'.
        '   Normally called from the 'ShowVals' sub or an opject's 'MouseMove' event.
        '   Only updates when new different text is passed. Prevents blinking.
        'EXAMPLE:       HelpPanel "Loading file"
        'PARAMETERS:
        '   Text$:      The string to display on the Status Bar
        'GLOBAL VARIABLES USED:
        '   UserEnteringData%
        'REQUIRES:      Nothing
        'VERSION:       2/20/97
        'CUSTODIAN:     J. Hill

        If UserEnteringData Then Exit Sub

        '    If Screen.ActiveForm.Name = "frmAbout" Then Exit Sub
        '    If Screen.ActiveForm.sbrUserInformation.SimpleText <> Text$ Then
        '        Screen.ActiveForm.sbrUserInformation.SimpleText = Text$
        '    End If

        If frmAbout.Visible Then Exit Sub
        If frmRac3152.sbrUserInformation.Text < Text Then
            frmRac3152.sbrUserInformation.Text = Text
        End If
    End Sub

    Function Limit(ByVal CheckValue As Short, ByVal LimitValue As Short) As Short
        If CheckValue<LimitValue Then
            Limit = LimitValue
        Else
            Limit = CheckValue
        End If
    End Function

    Public Sub ShowVals(ByVal SetIdx As Short)
        'DESCRIPTION:
        '   Shows values related to the parameter which the mouse pointer
        '   is passing over.
        'EXAMPLES:
        '   ShowVals(VOLT)
        'PARAMETERS:
        '   SetIdx%:    The index to a set of parallel global arrays (see below)
        'GLOBAL VARIABLES USED: None
        'REQUIRES:  EngNotate, HelpPanel
        'VERSION:   2/19/97
        'CUSTODIAN: J. Hill

        HelpPanel(GetVals(SetIdx))
    End Sub

    Public Sub SpinAbs(ByRef Display As TextBox, ByVal Direction As String, ByVal SetIdx As Short, ByVal Amount As Single)
        'DESCRIPTION:
        '   Increments or decrements SetCur$(SetIdx%) value
        '       by Amount! and stores it in the TextBox as
        '       a formatted string.
        '   Will not exceed Min and Max parameter values.
        '   Sends the current parameter setting to the instrument.
        '   Refreshes TextBox that stores new value.
        'EXAMPLE:
        '   SpinAbs txtDcOffs, "Down", VOLT_OFFS, 0.5
        'PARAMETERS:
        '   Display:    The TextBox where the result is displayed.
        '   Direction$: Must be "Up" or "Down".  Specifies increment or decrement.
        '   SetIdx%:    The index to a set of parallel global arrays (see below)
        '   Amount!:    The amount by which to inc. or dec. the current value.
        'GLOBAL VARIABLES USED:
        '   SetCur$():      CURrent value SETting array
        '   SetMin$():      MINimum value SETting array
        '   SetMax$():      MAXimum value SETting array
        '   LastDirection$
        'REQUIRES:  EngNotate, SendCommand
        'VERSION:   2/3/97
        'CUSTODIAN: J. Hill

        Dim Tmp As Double

        Tmp = Val(SetCur(SetIdx))
        If UCase(Convert.ToString(Direction)) = "UP" Then
            LastDirection = "UP"
            Tmp += Amount
            If Tmp > Val(SetMax(SetIdx)) Then
                Tmp = Val(SetMax(SetIdx))
                Beep()
            End If
        Else
            LastDirection = "DOWN"
            Tmp -= Amount
            If Tmp < Val(SetMin(SetIdx)) Then
                Tmp = Val(SetMin(SetIdx))
                Beep()
            End If
        End If
        SetCur(SetIdx) = Tmp
        SendCommand(SetIdx)
        System.Threading.Thread.Sleep(50)
        SendCommand(SetIdx)
        Display.Text = EngNotate(Tmp, SetIdx)
        Display.Refresh()
    End Sub

    Public Sub Spin10Pct(ByRef Display As TextBox, ByVal Direction As String, ByVal SetIdx As Short)
        'DESCRIPTION:
        '   Increments or decrements SetCur$(SetIdx%) value
        '       by 10%, rounds it to the nearest MSD and stores
        '       it in the TextBox as a formatted string.
        '   Will not exceed Min and Max parameter values.
        '   Sends the current parameter setting to the instrument.
        '   Refreshes TextBox that stores new value.
        'EXAMPLE:
        '   txtAmpl = Spin10Pct("Down", VOLT)
        'PARAMETERS:
        '   Direction$: Must be "Up" or "Down".  Specifies increment or decrement.
        '   SetIdx%:    The index to a set of parallel global arrays (see below)
        'GLOBAL VARIABLES USED:
        '   SetCur$():      CURrent value SETting array
        '   SetMin$():      MINimum value SETting array
        '   SetMax$():      MAXimum value SETting array
        '   SetMinInc$():   MINimum INCrement value SETting array
        '   LastDirection$
        'REQUIRES:  RoundMSD, EngNotate, SendCommand
        'VERSION:   2/3/97
        'CUSTODIAN: J. Hill

        Dim Tmp As Double, Negative As Boolean

        Negative = False
        Direction = UCase(Direction)
        LastDirection = Direction
        If SetCur(SetIdx) = "INF" And UCase(Direction) = "DOWN" Then
            Tmp = Val(SetMax(SetIdx))
        Else
            Tmp = Val(SetCur(SetIdx))
        End If
        If Tmp < 0 Or ((Tmp = 0) And (Val(SetMin(SetIdx)) < 0) And (Convert.ToString(Direction) = "DOWN")) Then 'If negative
            Tmp = Math.Abs(Tmp) 'Make it positive for now
            Negative = True 'Set flag
            If Convert.ToString(Direction) = "UP" Then
                Direction = "DOWN"
            Else
                Direction = "UP"
            End If
        End If
        If Convert.ToString(Direction) = "UP" Then
            If Tmp = 0 Then Tmp = 0.9 * Val(SetMinInc(SetIdx))
            Tmp = RoundMSD(Direction, Tmp + (0.1 * Tmp))
            If Tmp > Val(SetMax(SetIdx)) Then
                Tmp = Val(SetMax(SetIdx))
                Beep()
            End If
        Else
            Tmp = RoundMSD(Direction, Tmp - (0.1 * Tmp))
            If (Tmp < Val(SetMinInc(SetIdx))) Then
                Tmp = 0
                Beep()
            End If
        End If
        If Negative Then Tmp = -Tmp
        If Tmp < Val(SetMin(SetIdx)) Then
            Tmp = Val(SetMin(SetIdx))
            Beep()
        End If
        SetCur(SetIdx) = Tmp
        Display.Text = EngNotate(Tmp, SetIdx)
        SendCommand(SetIdx)
    End Sub

    Public Function RoundMSD(ByVal Direction As String, ByVal Number As Double) As Double
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

        Exponent = 0
        If Number >= 10 Then 'For positive exponent
            Do While Number >= 10
                Number /= 10.0
                Exponent += 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1
                Number *= 10.0
                Exponent -= 1
            Loop
        End If
        Number = Val(Str(Number)) ' Clear out very low LSBs from binary math
        If UCase(Convert.ToString(Direction)) = "UP" Then
            Number = Fix(Number + 0.9) * 10 ^ Exponent
        Else
            Number = Fix(Number) * 10 ^ Exponent
        End If
        RoundMSD = Number
    End Function

    Public Function EngNotate(ByVal Number As Double, ByVal SetIdx As Short) As String
        EngNotate = ""
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
        'VERSION:   3/14/97
        'CUSTODIAN: J. Hill

        Dim Multiplier As Short, Negative As Boolean, Digits As Short
        Dim Prefix As String, ReturnString As String, NumDigits As Short, FormatSpec As String

        Multiplier = 0 : Negative = False 'Initialize local variables

        If Number < 0 Then ' If negative
            Number = Math.Abs(Number) 'Make it positive for now
            Negative = True 'Set flag
        End If

        If Number >= 1000 Then 'For positive exponent
            Do While Number >= 1000 And Multiplier <= 4
                Number /= 1000
                Multiplier += 1
            Loop
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1 And Multiplier >= -4
                Number *= 1000
                Multiplier -= 1
            Loop
        End If

        Select Case Multiplier
            Case 4
                Prefix = " T" 'Tetra  E+12
            Case 3
                Prefix = " G" 'Giga   E+09
            Case 2
                Prefix = " M" 'Mega   E+06
            Case 1
                Prefix = " K" 'kilo   E+03
            Case 0
                Prefix = " " '<none> E+00
            Case -1
                Prefix = " m" 'milli  E-03
            Case -2
                Prefix = " " & Convert.ToString(Chr(181)) 'micro  E-06
            Case -3
                Prefix = " n" 'nano   E-09
            Case -4
                Prefix = " p" 'pico   E-12

            Case Else
                Prefix = " "
        End Select

        If Negative Then Number = -Number

        If (Multiplier < -4) Or (Multiplier > 4) Then
            ReturnString = Convert.ToString(Number)
        Else
            Number = Val(Str(Number)) ' Clear out very low LSBs from binary math
            If Strings.Left(SetRes(SetIdx), 1) = "N" Then
                Digits = Val(Mid(SetRes(SetIdx), 2, 1))
                Prefix = " "
                Number *= 10 ^ (Multiplier * 3)
            ElseIf Strings.Left(SetRes(SetIdx), 1) = "D" Then                'Digits of resolution
                Digits = Val(Mid(SetRes(SetIdx), 2, 1)) - Int(Math.Abs(Number)).ToString().Length
                If Digits < 0 Then Digits = 0
            ElseIf Strings.Left(SetRes(SetIdx), 1) = "A" Then                'Absolute Digits of resolution
                Digits = Val(Mid(SetRes(SetIdx), 2, 1)) + (Multiplier * 3)
                If Digits < 0 Then Digits = 0
            Else
                MsgBox("EngNotate detected unspecified resolution for parameter: " & SetCod(SetIdx) & ". Notify maintenance.", MsgBoxStyle.Exclamation)
                Digits = -1
            End If

            FormatSpec = "#,###,###,###,###,##0"
            If Digits > 0 Then
                FormatSpec &= "."
            ElseIf Digits < 0 Then
                FormatSpec = "0.0#######"
            End If

            For NumDigits = 1 To Digits
                FormatSpec &= "0"
            Next NumDigits

            ReturnString = Number.ToString(FormatSpec)
        End If

        'Trim trailing zeros after decimal
        If InStr(ReturnString, ".") Then
            While Strings.Right(ReturnString, 1) = "0"
                ReturnString = Strings.Left(ReturnString, Len(ReturnString) - 1)
            End While
            If Strings.Right(ReturnString, 1) = "." Then
                ReturnString = Strings.Left(ReturnString, Len(ReturnString) - 1)
            End If
        End If

        EngNotate = ReturnString & Prefix & SetUOM(SetIdx)
    End Function

    Public Sub SplashStart()
        '! Load frmAbout
        frmAbout.cmdOK.Visible = False
        frmAbout.Show()
        Application.DoEvents()
    End Sub

    Public Sub SplashEnd()
        frmAbout.Hide()
    End Sub

    Public Function StringToList(ByVal sStr As String, ByVal iLower As Short, ByRef List() As String, ByVal sDelimiter As String) As Short
        'DESCRIPTION:
        '   Procedure to convert a delimited string into a dynamic string array
        '   ReDims the array from iLower to the number of elements in string
        'Parameters:
        '   sStr       : String to be parsed.
        '   iLower     : Lower bound of target array
        '   List$()    : Dynamic array in which to return list of strings
        '   sDelimiter : Delimiter string.
        'Returns:
        '   Number of items in string
        '   or 0 if string is empty

        Dim numels As Short, i As Integer
        Dim iDelimiterLength As Integer

        iDelimiterLength = Len(sDelimiter)
        If sStr = "" Then
            StringToList = 0
            Exit Function
        End If

        numels = 1
        ReDim List(iLower)
        'Go through parsed string a character at a time.
        For i = 1 To Len(sStr)
            'Test for delimiter
            If Mid(sStr, i, iDelimiterLength) <> sDelimiter Then
                'Add the character to the current argument.
                List(iLower + numels - 1) &= Mid(sStr, i, 1)
            Else
                'Found a delimiter.
                ReDim Preserve List(iLower + numels)
                numels += 1
                i += iDelimiterLength - 1
            End If
        Next i
        StringToList = numels
    End Function
End Module