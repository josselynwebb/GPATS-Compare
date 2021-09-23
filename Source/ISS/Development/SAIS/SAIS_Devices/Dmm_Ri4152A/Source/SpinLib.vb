'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Module SpinLib
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
    '// Instrument: Agilent E1412A Digital Multimeter (Dmm)
    '//
    '//
    '// Revision History
    '// Rev      Date                  Reason
    '// =======  =======  =======================================
    '// 1.0.0.0  14FEB06  Baseline Release
    '////////////////////////////////////////////////////////////////////////////////
    Public Sub Spin10Pct(ByRef Display As TextBox, ByVal Direction As String, ByVal SetIdx As Short)
        Dim LastDirection As String ' - "AutoDim"

        'DESCRIPTION:
        '   Increments or decrements SetCur$(SetIdx%) value
        '       by 10%, rounds it to the nearest MSD and stores
        '       it in the TextBox as a formatted string.
        '   Will not exceed Min and Max parameter values.
        '   Sends the current parameter setting to the instrument.
        '   Refreshes TextBox that stores new value.
        'EXAMPLE:
        '   Spin10Pct(TextBox(idx), "Down", S)
        'PARAMETERS:
        '   Direction$: Must be "Up" or "Down".  Specifies increment or decrement.
        '   SetIdx%:    The index to a set of parallel global arrays (see below)
        'GLOBAL VARIABLES USED:
        '   SetCur$():      CURrent value SETting array
        '   SetMin$():      MINimum value SETting array
        '   SetMax$():      MAXimum value SETting array
        '   SetMinInc$():   MINimum INCrement value SETting array
        '   LastDirection$
        'REQUIRES:  RoundMSD, TimeNotate
        'VERSION:   2/3/97
        'CUSTODIAN: J. Hill
        'Modified to display seconds without rounding to MSD after 1S

        Dim Tmp As Double, Negative As Boolean

        Negative = False
        Direction = UCase(Direction)
        LastDirection = Direction
        If SetCur(SetIdx) = "INF" And UCase(Direction) = "DOWN" Then
            Tmp = Val(SetMax(SetIdx))
        Else
            Tmp = Val(SetCur(SetIdx))
        End If
        If Tmp < 0 Or ((Tmp = 0) And (Val(SetMin(SetIdx)) < 0) And (Direction = "DOWN")) Then 'If negative
            Tmp = System.Math.Abs(Tmp) 'Make it positive for now
            Negative = True 'Set flag
            If Convert.ToString(Direction) = "UP" Then
                Direction = "DOWN"
            Else
                Direction = "UP"
            End If
        End If
        If Direction = "UP" Then
            If Tmp = 0 Then Tmp = 0.9 * Val(SetMinInc(SetIdx))
            If Tmp < 10 Then
                Tmp = RoundMSD(Direction, Tmp + (0.1 * Tmp))
            Else
                Tmp = Int(Tmp + 1)
            End If
            If Tmp > Val(SetMax(SetIdx)) Then
                Tmp = Val(SetMax(SetIdx))
                Beep()
            End If
        Else
            If Tmp > 10 Then
                Tmp = Int(Tmp - 1)
            Else
                Tmp = RoundMSD(Direction, Tmp - (0.1 * Tmp))
            End If
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
        SetCur(SetIdx) = Strings.Format(Tmp)
        'EngNotate(SetCur$(SetIndex%), 0, SetUOM$(SetIndex%))
        Display.Text = TimeNotate(Tmp, SetUOM(SetIdx))
        Display.Refresh()
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
        If UCase(Convert.ToString(Direction))="UP" Then
            Number = Fix(Number+0.9)*10^Exponent
        Else
            Number = Fix(Number)*10^Exponent
        End If
        RoundMSD = Number
    End Function

    Public Function TimeNotate(ByVal Number As Double, ByVal Unit As String) As String
        TimeNotate = ""
        Dim Negative As Short   ' - "AutoDim"

        'DESCRIPTION:
        '   Returns passed number as numeric string in Engineering notation (every
        '   3rd exponent) with selectable precision along with Unit Of Measure.
        'EXAMPLE:
        '   Number=10987.1, Uom="Ohm" -> "10.987 KOhm"

        Dim Multiplier As Short
        Dim Prefix As String
        Dim ReturnString As String

        Multiplier = 0 'Initialize local variables

        If Number >= 1000 Then 'For positive exponent
        ElseIf Number < 1 And Number <> 0 Then            'For negative exponent (but not 0)
            Do While Number < 1 And Multiplier >= -4
                Number *= 1000
                Multiplier -= 1
            Loop
        End If

        Select Case Multiplier
            Case 0
                Prefix = "  " '<none> E+00
            Case -1
                Prefix = " m" 'milli  E-03
            Case -2
                Prefix = " " & Strings.Chr(181) 'micro  E-06
            Case -3
                Prefix = " n" 'nano   E-09
            Case -4
                Prefix = " p" 'pico   E-12

            Case Else
                Prefix = " "
        End Select

        If Negative Then Number = -Number

        If Multiplier < -4 Then
            ReturnString = "UndrRng"
        Else
            ReturnString = Strings.Format(Number, "0")
        End If

        'Return Function Value
        TimeNotate = ReturnString & Prefix & Unit
    End Function
End Module