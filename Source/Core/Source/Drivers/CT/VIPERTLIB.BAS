Attribute VB_Name = "VIPERTSaisLibrary"
'Option Explicit

Sub CenterForm(x As Form)
  
    'Screen.MousePointer = 11
    'x.Top = (Screen.Height * 0.85) / 2 - x.Height / 2
    'x.Left = Screen.Width / 2 - x.Width / 2
    x.Top = (Screen.Height) / 2 - x.Height / 2
    x.Left = Screen.Width / 2 - x.Width / 2
    'Screen.MousePointer = 0

End Sub



Public Sub Delay(Seconds As Single)
    'DESCRIPTION:
    '   Delays the program for a specified time
    'PARAMETERS:
    '   Seconds! = number of seconds to delay
    'EXAMPLE:
    '           Delay 2.3
    
    'Sleep CLng(Seconds * 1000!)
     t! = Timer
    Do While Timer - t! < Seconds
    Loop

End Sub
Public Sub HelpPanel(Text$)
    'If frmRac3152.sbrUserInformation.Panels(2).Text <> Text$ Then
    '    frmRac3152.sbrUserInformation.Panels(2).Text = Text$
    'End If
End Sub


Public Function UserEnter(SetIdx%, ByVal Title$) As String
    'Dim Prmpt$, CurVal!, Rtn$, Warning$
   '
    'Rtn$ = SetCur$(SetIdx%)
    'Prmpt$ = "Maximum:" & vbTab & SetMax$(SetIdx%) & " " & SetUOM$(SetIdx%)
    'Prmpt$ = Prmpt$ & vbCrLf
    'Prmpt$ = Prmpt$ & "Minimum:" & vbTab & SetMin$(SetIdx%) & " " & SetUOM$(SetIdx%)
    'Prmpt$ = Prmpt$ & vbCrLf
    'Prmpt$ = Prmpt$ & "Default:" & vbTab & vbTab & SetDef$(SetIdx%) & " " & SetUOM$(SetIdx%)
    'If Left$(SetRes$(SetIdx%), 1) = "D" Then
    '    Prmpt$ = Prmpt$ & vbCrLf
    '    If Mid$(SetRes$(SetIdx%), 2, 1) = "0" Then
    '        Prmpt$ = Prmpt$ & "Resolution:" & vbTab & "Integer"
    '    Else
    '        Prmpt$ = Prmpt$ & "Resolution:" & vbTab & Mid$(SetRes$(SetIdx%), 2, 1) & " digits"
    '    End If
    'End If
    'Prmpt$ = Prmpt$ & vbCrLf & vbCrLf
    'Prmpt$ = Prmpt$ & "Example: " & SetMax$(SetIdx%)
    'Warning$ = ""
    
    'Title$ = "Enter " & Title$
    'If SetUOM$(SetIdx%) <> "" Then
    '    Title$ = Title$ & " in " & SetUOM$(SetIdx%)
    'End If
    
    'Do
    '    Rtn$ = InputBox(Warning$ & Prmpt$, Title$, Rtn$)
    '    If Rtn$ = "" Then Exit Do   '"Cancel"
    '    Warning$ = "OUT OF RANGE!" & vbCrLf
    'Loop While Val(Rtn$) < SetMin$(SetIdx%) Or Val(Rtn$) > SetMax$(SetIdx%)
    '
    'If Rtn$ <> "" Then
    '    SetCur$(SetIdx%) = Val(Rtn$)
    '    UserEnter = EngNotate(Val(Rtn$), SetIdx%)
    'Else
    '    UserEnter = EngNotate(Val(SetCur$(SetIdx%)), SetIdx%)
    'End If
End Function

Public Function EngNotate(ByVal Number#, Digits%, Unit$) As String
    'Description
    '   Returns passed number as numeric string in Engineering notation (every
    '   3rd exponent) with selectable precision along with Unit Of Measure.
    '   EXAMPLES: Number=10987.1, Uom="Ohm" -> "10.987 KOhm"
    '             Number=5.43e-5, Uom="Sec" -> "54.300 uSec"
    
    Dim Multiplier%, Negative%
    Dim Prefix$, ReturnString$
    
    Multiplier% = 0: Negative% = False  'Initialize local variables
    
    If Number < 0 Then      ' If negative
        Number = Abs(Number)    'Make it positive for now
        Negative% = True        'Set flag
    End If
    
    If Number >= 1000 Then                    'For positive exponent
        Do While Number >= 1000 And Multiplier <= 4
            Number = Number / 1000
            Multiplier% = Multiplier% + 1
        Loop
    ElseIf Number < 1 And Number <> 0 Then    'For negative exponent (but not 0)
        Do While Number < 1 And Multiplier >= -4
            Number = Number * 1000
            Multiplier% = Multiplier% - 1
        Loop
    End If
    
    Select Case Multiplier%
        Case 4:  Prefix$ = " T"     'Terra  E+12
        Case 3:  Prefix$ = " G"     'Giga   E+09
        Case 2:  Prefix$ = " M"     'Mega   E+06
        Case 1:  Prefix$ = " K"     'Kilo   E+03
        Case 0:  Prefix$ = "  "      '<none> E+00
        Case -1: Prefix$ = " m"     'milli  E-03
        Case -2: Prefix$ = " " & Chr$(181)     'micro  E-06
        Case -3: Prefix$ = " n"     'nano   E-09
        Case -4: Prefix$ = " p"     'pico   E-12
        Case Else: Prefix$ = " "
    End Select
    
    If Negative% Then Number = -Number
    
    If Multiplier > 4 Then
        ReturnString$ = "Ovr Rng"
    ElseIf Multiplier < -4 Then
        ReturnString$ = "UndrRng"
    Else
        'If Left$(SetRes$(SetIdx%), 1) = "D" Then
        '    Number = Val(Str$(Number))      ' Clear out very low LSBs from binary math
        '    Digits% = Val(Mid$(SetRes$(SetIdx%), 2, 1)) - Len(Format(Int(Abs(Number))))
        '    If Digits% < 0 Then Digits% = 0
        'Else
        '    Digits% = -1
        'End If
        Select Case Digits%
            Case 0: ReturnString$ = Format(Number, "0")
            Case 1: ReturnString$ = Format(Number, "0.0")
            Case 2: ReturnString$ = Format(Number, "0.00")
            Case 3: ReturnString$ = Format(Number, "0.000")
            Case 4: ReturnString$ = Format(Number, "0.0000")
            Case 5: ReturnString$ = Format(Number, "0.00000")
            Case 6: ReturnString$ = Format(Number, "0.000000")
            Case 7: ReturnString$ = Format(Number, "0.0000000")
            Case 8: ReturnString$ = Format(Number, "0.00000000")
            Case Else: ReturnString$ = Format(Number, "0.00000000")
        End Select
    End If
    If Not (Negative%) Then
        EngNotate$ = "+" & ReturnString$ & Prefix$ & Unit$
    Else
        EngNotate$ = ReturnString$ & Prefix$ & Unit$
    End If
End Function


Function FileExists(Path$) As Integer

    x = FreeFile

    On Error Resume Next
    Open Path$ For Input As x
    If Err = 0 Then
        FileExists = True
    Else
        FileExists = False
    End If
    Close x

End Function

Function StringToList%(strng$, List$(), delimiter$)

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
    
    numels% = 0
    inflag% = 0

    'Go through parsed string a character at a time.
    slength% = Len(strng$)
    For ch% = 1 To slength%
        Char$ = Mid$(strng$, ch%, 1)
        'Test for delimiter
        If InStr(delimiter$, Char$) = 0 Then
            If Not inflag% Then
                'Test for too many arguments.
                If numels% = UBound(List$) Then
                    StringToList% = -1
                    Exit For
                End If
                numels% = numels% + 1
                inflag% = -1
            End If
            'Add the character to the current argument.
            List$(numels%) = List$(numels%) + Char$
        Else
            'Found a delimiter.
            'Set "Not in element" flag to FALSE.
            inflag% = 0
        End If
    Next ch%
    StringToList% = numels%
    
End Function

