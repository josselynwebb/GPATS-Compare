'Option Strict Off
'Option Explicit On

Imports System.Windows.Forms
Imports Microsoft.VisualBasic
Imports Microsoft.Win32

Public Module ViperT_EO

    Public ciclHasBeenInitialized As Boolean
    Sub StowVEO2Collimator()

        Dim x As DialogResult, S As String = "", Supply As Short, ErrorStatus As Integer
        Dim TestStat1 As Integer
        Dim Sensor_Position As Integer
        Dim iTry As Short
        Dim status As Integer = 0
        Dim Allocation As String
        Dim Response As String = Space(256)
        Dim XmlBuf As String
        Dim nSystErr As Integer
        Dim atxmlApiDLLHandle As Integer = 0
        frmSysPanl.Cursor = Cursors.WaitCursor

        ' new cicl stuff, validate each instrument
        On Error Resume Next

        If (ciclHasBeenInitialized = False) Then
            status = atxml_Initialize(proctype, guid)
        End If

        Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

        PsResourceName(1) = "DCPS_1"
        PsResourceName(2) = "DCPS_2"
        PsResourceName(3) = "DCPS_3"
        PsResourceName(4) = "DCPS_4"
        PsResourceName(5) = "DCPS_5"
        PsResourceName(6) = "DCPS_6"
        PsResourceName(7) = "DCPS_7"
        PsResourceName(8) = "DCPS_8"
        PsResourceName(9) = "DCPS_9"
        PsResourceName(10) = "DCPS_10"


        'Determine if the Freedom PS is functioning
        Response = Space(4096)
        XmlBuf = "<AtXmlTestRequirements>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_1</SignalResourceName> " & "</ResourceRequirement> "
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_2</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_3</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_4</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_5</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_6</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_7</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_8</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_9</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_10</SignalResourceName> " & "</ResourceRequirement>"
        XmlBuf &= "</AtXmlTestRequirements>"
        If (ciclHasBeenInitialized = False) Then
                status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))
        End If

        If (status = 0) Then
                ciclHasBeenInitialized = True
        End If

RemoveSAIF:
        ' remove the saif if it is installed
        If FnSAIFinstalled(False) = False Then
            S = "Disconnect the SAIF from the tester (You can leave it on the receiver). " + vbCrLf
            DisplaySetup(S, "ST-SAIF-OFF-1.BMP", 1)

            If FnSAIFinstalled(False) = True Then
                S = "Are you sure that you removed the SAIF from the tester?" + vbCrLf
                x = MsgBox(S, MsgBoxStyle.YesNo)

                If x = DialogResult.No Then GoTo RemoveSAIF ' try again

                If FnSAIFinstalled(False) = True Then

                End If
            End If
        End If

        'Step2:
        S = "Connect the VEO-2 power cable (93006G7300) W300-P1 connector to the VIPER/T PDU J7 connector."
        DisplaySetup(S, "ST-VEO2-PDU.bmp", 1, True, 2, 4)

        'Step3:
        S = "Connect the VEO-2 ethernet cable (93006G7350) W301-P1 connector to the VIPER/T docking station J16 connector."
        DisplaySetup(S, "ST-VEO2-J16.bmp", 1, True, 3, 4)

        'Step4:
        S = "1. Connect the VEO-2 ethernet cable W301-P2 to the VEO-2 J1 connector." + vbCrLf
        S += "2. Connect the VEO-2 power cable W300-P2 connector to the VEO-2 J2 connector." + vbCrLf
        S += "3. Connect the VEO-2 grounding cable (93006G7907) to the VEO-2 unit and to the VIPER/T ground strap." + vbCrLf
        DisplaySetup(S, "ST-VEO2-cbls1.bmp", 3, True, 4, 4)

        SetVEOPower(True)

        frmDialog.lblDialog.Text = "Stowing VEO2 Collimator..."
        frmDialog.Show()
        frmDialog.Refresh()
        Application.DoEvents()
        RESET_MODULE_INITIATE()
        frmDialog.Refresh()
        Application.DoEvents()
        Delay(1)
        IRWIN_SHUTDOWN()
        frmDialog.Refresh()
        Application.DoEvents()
        Delay(1)
        RESET_MODULE_INITIATE()
        frmDialog.Refresh()
        Application.DoEvents()
        Delay(1)

        If VEO2PowerOn = True Then
            SET_SENSOR_STAGE_LASER_INITIATE(3)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1

            Do While 3 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SENSOR_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1

                If iTry > 20 Then
                    frmSysPanl.Cursor = Cursors.Default
                    If (Sensor_Position <> 3) Then
                        MessageBox.Show("Sensor Stage Failed to Reach Stow Position", "VEO2 Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    End If
                    Exit Do
                End If
            Loop

            SET_SOURCE_STAGE_LASER_INITIATE(1)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1

            Do While 1 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SOURCE_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1

                If iTry > 20 Then
                    frmSysPanl.Cursor = Cursors.Default
                     If (Sensor_Position <> 1) Then
                        MessageBox.Show("Source Stage Failed to Reach Stow Position", "VEO2 Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                     End If
                    Exit Do
                End If

            Loop
            IRWIN_SHUTDOWN()
            SetVEOPower(False)

        End If

        ReleaseDCPS()

        frmSysPanl.Cursor = Cursors.Default

        frmDialog.Close()

        'Step2:
        S = "Remove the VEO-2 power cable (93006G7300) W300-P1 connector to the VIPER/T PDU J7 connector."
        DisplaySetup(S, "ST-VEO2-PDU.bmp", 1, True, 2, 4)

        'Step3:
        S = "Remove the VEO-2 ethernet cable (93006G7350) W301-P1 connector to the VIPER/T docking station J16 connector."
        DisplaySetup(S, "ST-VEO2-J16.bmp", 1, True, 3, 4)

        'Step4:
        S = "1. Remove the VEO-2 ethernet cable W301-P2 to the VEO-2 J1 connector." + vbCrLf
        S += "2. Connect the VEO-2 power cable W300-P2 connector to the VEO-2 J2 connector." + vbCrLf
        S += "3. Connect the VEO-2 grounding cable (93006G7907) to the VEO-2 unit and to the VIPER/T ground strap." + vbCrLf
        DisplaySetup(S, "ST-VEO2-cbls1.bmp", 3, True, 4, 4)

        S = "1. Install that Red Tag ""Remove Before Use"" pins have been removed from the VEO-2 unit." + vbCrLf
        DisplaySetup(S, "ST-VEO2-tag12.BMP", 1, True, 1, 4)

    End Sub


End Module