'Option Strict Off
Option Explicit On

Public Module VideoCaptureTest

    Public SaveDir As String
    'New module for CIC

    Function TestVCC() As Integer
        Dim x As Single
        Dim s As String = ""
        Dim TestStatus As Short = PASSED
        Dim TestName As String = "VCC"

        TestVCC = PASSED
        SaveDir = CurDir()
        Try
            ChDir("C:\Program Files (x86)\ATS\System Test\")
        Catch ex As Exception
            Echo("Error trying to change to directory ""C:\Program Files (x86)\ATS\System Test\"" ")
        End Try


        'Digital Multi-Meter Test Title block
        EchoTitle(InstrumentDescription(EO_VCC), True)
        EchoTitle("Video Capture Card Test", False)

InstallW201:
        If FirstPass = True Then
            x = MsgBox("Is the VCC cable W208 installed?", MsgBoxStyle.YesNo)
            If x <> DialogResult.Yes Then
                s = "Connect cable W208 from the CIC to the SAIF as follows:" & vbCrLf
                s &= " 1. W208-P1  to CIC J14 connector." & vbCrLf
                s &= " 2. W208-J1 (VIDEO IN) to SAIF ARB OUTPUT via a W15 cable." & vbCrLf & vbCrLf
                s &= "Note: W208-J2, J3, J4, J5 not connected." & vbCrLf
                DisplaySetup(s, "ST-VCC-W208-1.jpg", 2)
                If AbortTest = True Then GoTo TestComplete
            End If
        End If
        TestVCC = PASSED


VCC_1:
        sTNum = "VCC-01-001"
        nSystErr = WriteMsg(ARB, "*RST")
        Echo("Starting ARB RS170 Video output pattern, please wait...")
        Try
            AppHandle = Shell("RS170_640x480.bat", AppWinStyle.Hide, True)

        Catch ex As Exception
            MessageBox.Show("""RS170_640x480.bat"" Could Not Be Found", "Error")
            TestVCC = FAILED
            GoTo Testcomplete
        End Try

        'C:\usr\Tyx\Bin\VidDisplayNam.exe
        Try
            AppHandle = Shell("VidDisplay.exe RS170 CONTINUOUS ""Testing RS170 Video. Observe Video pattern (White Cross, Black Background) and then click the Close button.""", AppWinStyle.NormalFocus, True)
        Catch ex As Exception
            MessageBox.Show("""VidDisplay.exe"" Could Not Be Found", "Error")
            TestVCC = FAILED
            GoTo Testcomplete
        End Try

        x = MsgBox("Did you see a White Cross on a Black background pattern on the Video Display?", vbYesNoCancel)
        If x <> vbYes Then
            TestVCC = FAILED
            FormatResultLine(sTNum & " RS170 Video Test", False) 'failed
            RegisterFailure(EO_VCC, sTNum, sComment:=" VCC Video Test FAILED.")
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & " RS170 Video Test", True) 'Pass
            Call IncStepPassed()
        End If
        System.Windows.Forms.Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 2 Then
            GoTo VCC_1
        End If
        frmSTest.proProgress.Value += 1




VCC_2:
        sTNum = "VCC-02-001"
        nSystErr = WriteMsg(ARB, "*RST")
        Echo("Starting ARB RS343 Video output pattern, please wait...")

        Try
            AppHandle = Shell("RS343_808x808.bat", AppWinStyle.Hide, True)

        Catch ex As Exception
            MessageBox.Show("""RS343_808x808.bat"" Could Not Be Found", "Error")
            TestVCC = FAILED
            GoTo Testcomplete
        End Try
        Try
            AppHandle = Shell("VidDisplay.exe RS343 CONTINUOUS ""Testing RS343 Video. Observe Video pattern (Black Cross, White Background)  and then click the Close button.""", AppWinStyle.NormalFocus, True)
        Catch ex As Exception
            MessageBox.Show("""VidDisplay.exe"" Could Not Be Found", "Error")
            TestVCC = FAILED
            GoTo Testcomplete
        End Try

        x = MsgBox("Did you see a Black Cross on a White background pattern on the Video Display?", vbYesNoCancel)
        If x <> vbYes Then
            TestVCC = FAILED
            FormatResultLine(sTNum & " RS343 Video Test", False) 'failed
            RegisterFailure(EO_VCC, sTNum, sComment:=" VCC Video Test FAILED.")
            Call IncStepFailed()
            If OptionFaultMode = SOFmode Then GoTo TestComplete
        Else
            FormatResultLine(sTNum & " RS343 Video Test", True) 'Pass
            Call IncStepPassed()
        End If
        System.Windows.Forms.Application.DoEvents()
        If AbortTest = True Then GoTo TestComplete
        If OptionMode = LOSmode And OptionTestName = TestName And OptionStep = 2 Then
            GoTo VCC_2
        End If
        frmSTest.proProgress.Value += 1


TestComplete:
        ChDir(SaveDir)
        frmSTest.proProgress.Value = frmSTest.proProgress.Maximum
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"
        If AbortTest = True Then
            If TestVCC = FAILED Then
                ReportFailure(EO_VCC)
            Else
                ReportUnknown(EO_VCC)
                TestVCC = -99
            End If
        ElseIf TestVCC = PASSED Then
            ReportPass(EO_VCC)
        ElseIf TestVCC = FAILED Then
            ReportFailure(EO_VCC)
        Else
            ReportUnknown(EO_VCC)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If

    End Function


End Module