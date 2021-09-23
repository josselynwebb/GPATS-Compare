'Option Strict Off
Option Explicit On

Imports System.Math




Public Module ViperT_EO


    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  VEO2 Self Test                            *
    '* Version        : 2.0                                       *
    '* Last Update    : Sept 1, 2017                              *
    '* Purpose        : To Test VEO2 hardware.                    *
    '**************************************************************


    '**************************************************************
    '* WARNING: VEO2 POWER ON/OFF REQUIREMENT                     *
    '* VEO2 Power must be turned ON in the following order:       *
    '* DC4 to +15Vdc                                              *
    '* DC3 to +28Vdc                                              *
    '*                                                            *
    '* VEO2 Power must be turned OFF in the following order:      *
    '* DC3 to +28Vdc                                              *
    '* DC4 to +15Vdc                                              *
    '*                                                            *
    '**************************************************************

    Public LaserContinue As Short

    Dim TestStat As Integer
    Dim EoError(100) As String
    Dim nErrNum As Short
    Dim i As Short

    'Larrs data
    Dim AZPOS(3) As Single
    Dim ELPOS(3) As Single
    Dim LAREA_LO(3) As Single
    Dim LAREA_HI(3) As Single
    Dim XCENTROID_LO(3) As Single
    Dim YCENTROID_LO(3) As Single
    Dim XCENTROID_HI(3) As Single
    Dim YCENTROID_HI(3) As Single

    Dim Area As Single
    Dim sIntensityRatio As Single
    Dim sColorTemp As Single
    Dim sRadiance As Single

    'SOFTWARE DLL CALLS
    Private Declare Function BORESIGHT_LASER_SETUP Lib "C:\IRWin2001\VEO2.dll" Alias "BORESIGHT_LASER_SETUP" (ByVal I1 As Integer, ByVal I2 As Integer, ByVal I3 As Integer, ByVal I4 As Integer, ByVal I5 As Integer, ByVal S6 As Single, ByVal I7 As Integer, ByVal S8 As Single) As Short
    Private Declare Function BORESIGHT_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "BORESIGHT_LASER_INITIATE" () As Short
    Private Declare Function BORESIGHT_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "BORESIGHT_LASER_FETCH" (ByRef I1 As Single, ByRef I2 As Single, ByRef I3 As Single, ByRef I4 As Integer) As Short 'bb 1,2,3

    Private Declare Function BORESIGHT_TV_VIS_SETUP Lib "C:\IRWin2001\VEO2.dll" Alias "BORESIGHT_TV_VIS_SETUP" (ByVal lSource As Integer, ByVal lNumFrames As Integer, ByVal sHFieldOfView As Single, ByVal sVFieldOfView As Single, ByVal sRadiance As Single, ByVal lTargetPos As Integer, ByVal lCenterX As Integer, ByVal lCenterY As Integer, ByVal lSBlockTopLeftX As Integer, ByVal lSBlockTopLeftY As Integer, ByVal lSBlockBotRightX As Integer, ByVal lSBlockBotRightY As Integer, ByVal lCameraSelection As Integer, ByVal sColorTemp As Single, ByVal sIntensityRatio As Single) As Short
    Private Declare Function BORESIGHT_TV_VIS_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "BORESIGHT_TV_VIS_INITIATE" () As Short
    Private Declare Function BORESIGHT_TV_VIS_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "BORESIGHT_TV_VIS_FETCH" (ByRef I1 As Single, ByRef I2 As Single, ByRef I3 As Single, ByRef I4 As Integer) As Short '  bb 1,2,3, 4=status



    'HARDWARE CONTROL DLL PROCEDURE CALLS
    Private Declare Function SET_SYSTEM_CONFIGURATION_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_SYSTEM_CONFIGURATION_INITIATE" (ByVal xTarget_Position As Integer) As Short
    'Private Declare Function SET_SYSTEM_CONFIGURATION_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_SYSTEM_CONFIGURATION_FETCH" (ByRef xTarget_Position As Integer) As Short
    '
    Private Declare Function RESET_MODULE_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "RESET_MODULE_INITIATE" () As Short
    '
    Public Declare Function IRWIN_SHUTDOWN Lib "C:\IRWin2001\VEO2.dll" Alias "IRWIN_SHUTDOWN" () As Short
    '
    Private Declare Function GET_BIT_DATA_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "GET_BIT_DATA_INITIATE" () As Short
    Private Declare Function GET_BIT_DATA_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "GET_BIT_DATA_FETCH" (ByRef Error_Number As Integer) As Short
    '
    Private Declare Function GET_MODULE_ID_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "GET_MODULE_ID_INITIATE" () As Short
    'Private Declare Function GET_MODULE_ID_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "GET_MODULE_ID_FETCH" (ByRef xIDent As Integer) As Short
    '
    Private Declare Function GET_STATUS_BYTE_MESSAGE_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "GET_STATUS_BYTE_MESSAGE_INITIATE" () As Short
    Private Declare Function GET_STATUS_BYTE_MESSAGE_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "GET_STATUS_BYTE_MESSAGE_FETCH" (ByRef xStatus As Integer) As Short
    '
    Private Declare Function GET_TEMP_TARGET_IR_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "GET_TEMP_TARGET_IR_INITIATE" () As Short
    Private Declare Function GET_TEMP_TARGET_IR_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "GET_TEMP_TARGET_IR_FETCH" (ByRef xTarget_Temp As Single) As Short 'bb
    '
    Private Declare Function SET_RDY_WINDOW_IR_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_RDY_WINDOW_IR_INITIATE" (ByVal xRdy_Window As Single) As Short
    'Private Declare Function SET_RDY_WINDOW_IR_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_RDY_WINDOW_IR_FETCH" (ByRef xRdy_Window As single) As Short
    '
    Private Declare Function SET_TARGET_POSITION_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TARGET_POSITION_INITIATE" (ByVal xTarget_Position As Integer) As Short
    Private Declare Function SET_TARGET_POSITION_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TARGET_POSITION_FETCH" (ByRef xTarget_Position As Integer) As Short
    '
    Private Declare Function SET_TEMP_ABSOLUTE_IR_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TEMP_ABSOLUTE_IR_INITIATE" (ByVal xtemperature As Single) As Short
    Private Declare Function SET_TEMP_ABSOLUTE_IR_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TEMP_ABSOLUTE_IR_FETCH" (ByRef xtemperature As Single) As Short 'bb
    '
    Private Declare Function SET_TEMP_DIFFERENTIAL_IR_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TEMP_DIFFERENTIAL_IR_INITIATE" (ByVal xtemperature As Single) As Short
    'Private Declare Function SET_TEMP_DIFFERENTIAL_IR_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TEMP_DIFFERENTIAL_IR_FETCH" (ByRef xtemperature As single) As Short'bb
    '
    Private Declare Function SET_CAMERA_TRIGGER_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_CAMERA_TRIGGER_LASER_INITIATE" (ByVal xtrigger As Single) As Short 'bb was int?
    'Private Declare Function SET_CAMERA_TRIGGER_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_CAMERA_TRIGGER_LASER_FETCH" (ByRef xtrigger As single) As Short'bb
    '
    Private Declare Function SET_CAMERA_DELAY_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_CAMERA_DELAY_LASER_INITIATE" (ByVal xdelay As Integer) As Short 'bb ?
    'Private Declare Function SET_CAMERA_DELAY_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_CAMERA_DELAY_LASER_FETCH" (ByRef xdelay As Integer) As Short
    '
    Public Declare Function SET_SOURCE_STAGE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_SOURCE_STAGE_LASER_INITIATE" (ByVal Source_Stage_Position As Integer) As Short
    Public Declare Function SET_SOURCE_STAGE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_SOURCE_STAGE_LASER_FETCH" (ByRef Source_Stage_Position As Integer) As Short
    '
    Public Declare Function SET_SENSOR_STAGE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_SENSOR_STAGE_LASER_INITIATE" (ByVal Sensor_Stage_Position As Integer) As Short
    Public Declare Function SET_SENSOR_STAGE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_SENSOR_STAGE_LASER_FETCH" (ByRef Sensor_Stage_Position As Integer) As Short
    '
    Private Declare Function SET_MODE_MODULATION_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_MODE_MODULATION_LASER_INITIATE" (ByVal xsetting As Integer) As Short
    'Private Declare Function SET_MODE_MODULATION_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_MODE_MODULATION_LASER_FETCH" (ByRef xsetting As Integer) As Short
    '
    Private Declare Function SET_OPERATION_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_OPERATION_LASER_INITIATE" (ByVal xsetting As Integer) As Short
    Private Declare Function SET_OPERATION_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_OPERATION_LASER_FETCH" (ByRef xsetting As Integer) As Short
    '
    Private Declare Function SELECT_DIODE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SELECT_DIODE_LASER_INITIATE" (ByVal xSelect As Integer) As Short
    Private Declare Function SELECT_DIODE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SELECT_DIODE_LASER_FETCH" (ByRef xSelect As Integer) As Short
    '
    Private Declare Function SET_TRIGGER_SOURCE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TRIGGER_SOURCE_LASER_INITIATE" (ByVal xSelect As Integer) As Short
    Private Declare Function SET_TRIGGER_SOURCE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_TRIGGER_SOURCE_LASER_FETCH" (ByRef xSelect As Integer) As Short
    '
    Private Declare Function SET_PULSE_AMPLITUDE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_AMPLITUDE_LASER_INITIATE" (ByVal xPA As Single) As Short
    Private Declare Function SET_PULSE_AMPLITUDE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_AMPLITUDE_LASER_FETCH" (ByRef xPA As Single) As Short
    '
    Private Declare Function SET_PULSE_PERIOD_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_PERIOD_LASER_INITIATE" (ByVal xPA As Single) As Short
    'Private Declare Function SET_PULSE_PERIOD_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_PERIOD_LASER_FETCH" (ByRef xPA As single) As Short  'bb
    '
    Private Declare Function SET_PULSE_WIDTH_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_WIDTH_LASER_INITIATE" (ByVal xPA As Integer) As Short
    'Private Declare Function SET_PULSE_WIDTH_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_WIDTH_LASER_FETCH" (ByRef xPA As Integer) As Short
    '
    Private Declare Function SET_RANGE_EMULATION_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_RANGE_EMULATION_LASER_INITIATE" (ByVal xPA As Single) As Short
    'Private Declare Function SET_RANGE_EMULATION_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_RANGE_EMULATION_LASER_FETCH" (ByRef xPA As single) As Short 'bb
    '
    Private Declare Function SET_PULSE2_DELAY_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE2_DELAY_LASER_INITIATE" (ByVal xPA As Single) As Short
    'Private Declare Function SET_PULSE2_DELAY_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "" (ByRef xPA As Single) As Short'bb
    '
    Private Declare Function SELECT_LARGER_PULSE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SELECT_LARGER_PULSE_LASER_INITIATE" (ByVal xPA As Integer) As Short
    'Private Declare Function SELECT_LARGER_PULSE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SELECT_LARGER_PULSE_LASER_FETCH" (ByRef xPA As Integer) As Short
    '
    Private Declare Function SET_PULSE_PERCENTAGE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_PERCENTAGE_LASER_INITIATE" (ByVal xpercent As Single) As Short
    'Private Declare Function SET_PULSE_PERCENTAGE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_PULSE_PERCENTAGE_LASER_FETCH" (ByRef xpercent As Single) As Short ' bb
    '
    Private Declare Function SET_LASER_TEST_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LASER_TEST_INITIATE" (ByVal xSelect As Integer) As Short
    Private Declare Function SET_LASER_TEST_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LASER_TEST_FETCH" (ByRef xSelect As Integer) As Short
    '
    Private Declare Function SET_ANGULAR_RATE_VIS_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_ANGULAR_RATE_VIS_INITIATE" (ByVal xRate As Single) As Short
    'Private Declare Function SET_ANGULAR_RATE_VIS_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_ANGULAR_RATE_VIS_FETCH" (ByRef xRate As single) As Short' bb
    '
    Private Declare Function SET_RADIANCE_VIS_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_RADIANCE_VIS_INITIATE" (ByVal xRadiance As Single) As Short
    Private Declare Function SET_RADIANCE_VIS_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_RADIANCE_VIS_FETCH" (ByRef xRadiance As Single) As Short ' bb 
    '
    Private Declare Function SET_LARRS_AZ_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LARRS_AZ_LASER_INITIATE" (ByVal xposition As Integer) As Short
    Private Declare Function SET_LARRS_AZ_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LARRS_AZ_LASER_FETCH" (ByRef xposition As Integer) As Short
    '
    Private Declare Function SET_LARRS_EL_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LARRS_EL_LASER_INITIATE" (ByVal yposition As Integer) As Short
    Private Declare Function SET_LARRS_EL_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LARRS_EL_LASER_FETCH" (ByRef yposition As Integer) As Short
    '
    Private Declare Function SET_LARRS_POLARIZE_LASER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LARRS_POLARIZE_LASER_INITIATE" (ByVal xposition As Integer) As Short
    Private Declare Function SET_LARRS_POLARIZE_LASER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_LARRS_POLARIZE_LASER_FETCH" (ByRef xposition As Integer) As Short
    '
    Public Declare Function SET_CAMERA_POWER_INITIATE Lib "C:\IRWin2001\VEO2.dll" Alias "SET_CAMERA_POWER_INITIATE" (ByVal xSelect As Integer) As Short
    Public Declare Function SET_CAMERA_POWER_FETCH Lib "C:\IRWin2001\VEO2.dll" Alias "SET_CAMERA_POWER_FETCH" (ByRef xSelect As Integer) As Short
    '


 #Const defUse_InitVEO2ErrorCodes = True
#If defUse_InitVEO2ErrorCodes Then
    Sub InitVEO2ErrorCodes()
        '
        EoError(1) = "The blackbody temperature is above its temperature range."
        EoError(2) = "After stabilizing, the blackbody has moved out of the Ready window."
        EoError(3) = "The blackbody is unable to reach its set point."
        EoError(4) = "An unrecognized command has been received over the remote bus."
        EoError(5) = "An out-of-range command has been received."
        EoError(10) = "EEPROM confidence check on has failed."
        EoError(11) = "Internal communication has failed."
        EoError(12) = "Internal execution error."
        EoError(22) = "Invalid calibration point."
        EoError(23) = "Shorted Thermister."
        EoError(24) = "Open thermistor."
        EoError(25) = "EEPROM dropout."
        EoError(26) = "ADC2 failed."
        EoError(27) = "ADC1 failed."
        EoError(28) = "Serial buffer overflow."
        EoError(29) = "EEPROM checksum error."
        EoError(30) = "Detent not Set while in idle."
        EoError(31) = "Time-out, motor not to position."
        EoError(34) = "DAC failure."
        EoError(35) = "Power Amplifier failure."
        EoError(36) = "Thermoelectric Cooler (TEC) failure."
        EoError(37) = "Power Supplies failure."
        EoError(38) = "Reset failure."
        EoError(39) = "Following error, rotor not keeping up with commanded velocity"
        EoError(40) = "Encoder BIT failure."
        EoError(42) = "Any lamp failure."
        EoError(43) = ""
        EoError(44) = ""
        EoError(45) = "General communications error."
        EoError(46) = "Fan failure."
        EoError(47) = "Camera failure."
        EoError(48) = "Camera Message Error."
        EoError(49) = "Trigger Failure."
        EoError(50) = "Interlock not set."
        EoError(51) = "Power Supply 1 failure."
        EoError(52) = "Power Supply 2 failure."
        EoError(91) = "The second Smart Probe has failed (Collimator)."
        EoError(95) = "The first Smart Probe has failed."
        EoError(97) = "The power amplifier Card has failed."
        EoError(98) = "The GCC has failed."
        EoError(99) = "The ACC has failed."

        Dim i As Integer
        For i = 1 To 100
            If EoError(i)="" Then
                EoError(i) = "Undefined Error Number: " & nErrNum
            End If
        Next i

    End Sub
#End If

    Function TestVEO() As Integer

        Dim x As DialogResult
        Dim i As Integer, S As String
        Dim TestStatus As Integer
        Dim TestStat1 As Integer
        Dim TestStat2 As Integer
        Dim TestStat5 As Single
        Dim Sensor_Position As Integer
        Dim Ambient_temperature As Single
        Dim BlackBody_temperature As Single
        Dim VIS_Output As Single
        Dim Status_Byte As Integer
        Dim iTry As Short
        Dim DSOnoise(500) As Double
        Dim xCoord As Single
        Dim yCoord As Single
        Dim LoopStepNo As Integer

        Dim dVoltage As Single
        Dim dCurrent As Single

        InitVEO2ErrorCodes()  'added in 1320

        EchoTitle(InstrumentDescription(VEO2), True)
        EchoTitle("ATS-Viper/T EO Test", False)

        TestVEO = UNTESTED
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False
        frmSTest.proProgress.Value = 0
        HelpContextID = 1370

RemoveSAIF:
        ' remove the saif if it is installed
        If FnSAIFinstalled(False) = True Then
            sMsg = "Disconnect the SAIF from the tester (You can leave it on the receiver). " & vbCrLf
            DisplaySetup(sMsg, "ST-SAIF-OFF-1.jpg", 1)
            If AbortTest = True Then GoTo TestComplete
            If FnSAIFinstalled(False) = True Then
                sMsg = "Are you sure that you removed the SAIF from the tester?" & vbCrLf
                x = MsgBox(sMsg, MsgBoxStyle.YesNo)
                If x = DialogResult.No Then GoTo RemoveSAIF ' try again
                If FnSAIFinstalled(False) = True Then
                    Echo("   *******************************************")
                    Echo(FormatResults(False, "Receiver ITA switch open test failed."), 4)
                    Echo("   The Receiver ITA Switch on the tester must be shorted.", 4)
                    Echo("   *******************************************")
                    ReceiverSwitchOK = False
                End If
            End If
        End If
        If AbortTest = True Then GoTo TestComplete
        AbortTest = False

        If FirstPass = True Then
            'frmLaRRS.ShowDialog()
            If AbortTest = True Then
                Echo("Test Aborted" & vbCrLf)
                GoTo TestComplete
            End If
            'Echo("EO Azimuth   = " & Azimuth)
            'Echo("EO Elevation = " & Elevation)
        End If

Step1:
            S = "1. Verify that Red Tag ""Remove Before Use"" pins have been removed from the VEO-2 unit." & vbCrLf
            S &= "FAILURE TO REMOVE TAGS CAN DAMAGE VEO-2 UNIT." & vbCrLf
            DisplaySetup(S, "ST-VEO2-tag12.jpg", 1, True, 1, 4)
            If AbortTest = True Then GoTo TestComplete

Step2:
            S = "Connect the VEO-2 power cable (93006G7300) W300-P1 connector to the ATS PDU J7 connector."
            DisplaySetup(S, "ST-VEO2-PDU.jpg", 1, True, 2, 4)
            If AbortTest = True Then GoTo TestComplete
            If GoBack = True Then GoTo Step1

Step3:
            S = "Connect the VEO-2 ethernet cable (93006G7350) W301-P1 connector to the ATS CIC J16 connector."
            DisplaySetup(S, "ST-VEO2-J16.jpg", 1, True, 3, 4)
            If AbortTest = True Then GoTo TestComplete
            If GoBack = True Then GoTo Step2

Step4:
            S = "1. Connect the VEO-2 ethernet cable W301-P2 to the VEO-2 J1 connector." & vbCrLf
            S &= "2. Connect the VEO-2 power cable W300-P2 connector to the VEO-2 J2 connector." & vbCrLf
            S &= "3. Connect the VEO-2 grounding cable (93006G7907) to the VEO-2 unit and to the ATS ground strap." & vbCrLf
            DisplaySetup(S, "ST-VEO2-cbls1.jpg", 3, True, 4, 4)
            If GoBack = True Then GoTo Step3

            If AbortTest = True Then GoTo TestComplete

            LaserContinue = True
            WarningMsg = ""


            'This routine turns on the message box and waits maximum of 30 seconds for operator to answer
            '  The 1064 Laser is no longer dangerous!
            '  The Laser is only turned on in a short duty-cycle pulse mode which reduces the power
            '    to an EYE-SAFE condition.
        'WarningMsg &= "Do you want to bypass the VEO-2 Laser and LaRRS Tests?" & vbCrLf & vbCrLf
        'WarningMsg &= "Press CONTINUE to test Lasers." & vbCrLf & vbCrLf
        'WarningMsg &= "Press BYPASS TEST to skip all Laser Tests." & vbCrLf

        'FrmlaserWarning.Show() ' form is opened as a nonmodal box
        'FrmlaserWarning.Label3.Visible = True
        'frmSTest.Timer2.Enabled = False
        'frmSTest.Timer2.Interval = 1000 '1 seconds
        'frmSTest.Timer2.Enabled = True ' start timer
        'Timer2timeout = False

        'Dim timercount As Integer
        'timercount = 30
        'Do
        '    Application.DoEvents()
        '    If Timer2timeout = True Then ' 30 second timeout occurred
        '        timercount -= 1
        '        FrmlaserWarning.Label3.Text = "Test will automatically be bypassed in " & CStr(timercount) & " sec."
        '        If timercount = 0 Then
        '            FrmlaserWarning.Hide()
        '            LaserContinue = False
        '            Exit Do
        '        End If
        '        Timer2timeout = False
        '        frmSTest.Timer2.Interval = 1000 '1 seconds
        '        frmSTest.Timer2.Enabled = True ' start timer
        '    End If
        '    Application.DoEvents()
        '    If CloseProgram = True Then Exit Function
        '    If AbortTest = True Then Exit Do
        'Loop Until FrmlaserWarning.Visible = False
        'frmSTest.Timer2.Enabled = False

        '            If LaserContinue = True Then
        '                ' Install the LaRRS
        '                ' Laser Range Receiver Sensitivity (LaRRS) Unit
        '                ' Performs range measurements
        '                ' Sensitivity measurements
        '                ' Autocollimation with main unit



        'Step1b:
        '                S = "1. Attach LaRRS Self-Test fixture (93006G7600) to the front of the VEO-2 unit.  "
        '                S &= "   Torque mounting bolts to a little more than hand tight with a 5/16 inch hex wrench (approximately 35 inch-pounds)." & vbCrLf
        '                S &= "2. Install the LaRRS attenuator (93006G7550) lens into the LaRRS (93006G7200) unit.  "
        '                S &= "   DO NOT TOUCH THE ATTENUATOR LENS.  If necessary see Help file for precautions on cleaning the LaRRS attenuator lens." & vbCrLf
        '                S &= "3. Place the LaRRS onto the LaRRS Self-Test fixture in front of and pointing into the VEO-2 unit.  "
        '                S &= "   Attach LaRRS fixture bolts into base of LaRRS hand tight with a 5/32 inch hex wrench." & vbCrLf
        '                S &= "4. Attach the LaRRS signal cable (93006G7450) W303-P2 connector to the LaRRS."
        '                DisplaySetup(S, "ST-VEO2-LaRRS.jpg", 4, True, 1, 3)
        '                If AbortTest = True Then GoTo TestComplete

        'Step2b:
        '                S = "Attach the LaRRS fiber optics cable (93006G7500) W304-P2 connector to the LaRRS.  "
        '                S &= "Make sure to align copper pin before tightening the connector. " & vbCrLf & vbCrLf
        '                S &= "NOTE: This cable is FRAGILE!  Do not bend sharply.  Also, keep protective covers installed when not in use." & vbCrLf & vbCrLf
        '                DisplaySetup(S, "ST-VEO2-Larrs-Cbl.jpg", 1, True, 2, 3)
        '                If AbortTest = True Then GoTo TestComplete
        '                If GoBack = True Then GoTo Step1b

        'Step3b:
        '                ' Install the LaRRS cables to the VEO-2
        '                S = "1. Attach LaRRS signal cable W303-P1 connector to the VEO-2 J3 connector." & vbCrLf
        '                S &= "2. Attach LaRRS fiber optics cable W304-P1 connector to the VEO-2 J8 connector.  "
        '                S &= "Make sure to align copper pin before tightening the connector. "
        '                DisplaySetup(S, "ST-VEO2-cbls2.jpg", 2, True, 3, 3)
        '                If AbortTest = True Then GoTo TestComplete
        '                If GoBack = True Then GoTo Step2b

        '                Application.DoEvents()
        '                Delay(0.5)
        '            End If
        TestVEO = PASSED

            'TURN ON POWER
        If VEO2PowerOn = False Then ' 3 minute wait not started until after Power Supply tests
                SetVEOPower(True)
            End If

        '---------------------------------------------------------------------------------------
            'Tests added to check voltage and current before 3 minute wait and sending VEO2 commands
VEO2_0_1:
            sTNum = "VEO-00-001" ' check DCPS1,2 Vdc   +28Vdc
            dVoltage = CommandMeasureVolts(1) ' measure volts
            dMeasurement = dVoltage
            RecordTest(sTNum, "VEO2 P28Vdc (DCPS1,2) Voltage test", "27.5", "28.5", dMeasurement, "Vdc", 3)
            If dVoltage < 27.5 Or dVoltage > 28.5 Then
                VEO2PowerOn = False
                TestStatus = FAILED
                TestVEO = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            System.Windows.Forms.Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

VEO2_0_2:

            sTNum = "VEO-00-002" ' Check DCPS1,2 Amps
            dCurrent = CommandMeasureAmps(1) ' measure amps
            dMeasurement = dCurrent
            RecordTest(sTNum, "VEO2 P28Vdc (DCPS1,2) Current Test ", "0.1", "2", dMeasurement, "Adc", 3)
            If dCurrent < 0.1 Or dCurrent > 4 Then
                VEO2PowerOn = False
                TestStatus = FAILED
                TestVEO = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            System.Windows.Forms.Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

VEO2_0_3:

            sTNum = "VEO-00-003" ' check DCPS3 Vdc   +15Vdc
            dVoltage = CommandMeasureVolts(3) ' measure volts
            dMeasurement = dVoltage
            dMeasurement = dVoltage
            RecordTest(sTNum, "VEO2 P15Vdc (DCPS3) Voltage test", "14.5", "15.5", dMeasurement, "Vdc", 3)
            If dVoltage < 14.5 Or dVoltage > 15.5 Then
                VEO2PowerOn = False
                TestStatus = FAILED
                TestVEO = FAILED
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                IncStepPassed()
            End If
            System.Windows.Forms.Application.DoEvents()
            If AbortTest = True Then GoTo TestComplete

VEO2_0_4:

            sTNum = "VEO-00-004" ' Check DCPS3 Amps
            dCurrent = CommandMeasureAmps(3) ' measure amps
            dMeasurement = dCurrent
            RecordTest(sTNum, "VEO2 P15Vdc (DCPS3) Current Test ", "0.5", "4", dMeasurement, "Adc", 3)
            If dCurrent < 0.5 Or dCurrent > 4 Then
                VEO2PowerOn = False
                TestStatus = FAILED
                TestVEO = FAILED
                Call IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                Call IncStepPassed()
            End If
            System.Windows.Forms.Application.DoEvents()
            If AbortTest = True Then
                TestVEO = UNTESTED
                GoTo TestComplete
            End If
        '---------------------------------------------------------------------------------------

                'Wait three minutes
                If frmSTest.cmdAbort.Visible = True Then
                      Echo("- VEO2 Power is ON.")
                      Echo("- System requires three minutes to initialize.")
                      Echo("Waiting (3 minutes) 0") ' show progress in message window
                      For i = 1 To 180
                        Delay(1)
                        Application.DoEvents()
                        If i Mod 10 = 0 Then
                            BumpProgress(i / 10)
                        End If
                        If AbortTest = True Then Exit For
                      Next i
                End If


            If VEO2PowerOn = False Then
                TestVEO = UNTESTED
                GoTo TestComplete
            End If
            LoopStepNo = 1


        Try ' On Error GoTo VEO2errorHandler

            RESET_MODULE_INITIATE()
            IRWIN_SHUTDOWN()

VEO2_1_1:
            '4.1 BIT Power up
            'This procedure verifies that the system is communicating properly with the
            ' Core at power up.
            '4.1.1 BIT Verify electronics communications
            'Upon power up, the VEO-2 system will go through several internal checks within
            ' the firmware that verifies the circuit cards are connected and operational.  Errors
            ' will be reported at the IRWindows level.  Should discrepancies arise, the operator
            ' wil refer to the Operator's manual for diagnostic action and shall report the
            ' discrepancies to the Responsible Equipment Authority.

            sTNum = "VEO-01-001"
            GET_BIT_DATA_INITIATE()
            System.Threading.Thread.Sleep(1000)
            GET_BIT_DATA_FETCH(TestStat1)
            If TestStat1 <> 0 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 Power On BIT Test", False)
                Echo("  Exp 0   Meas " & CStr(Truncate(TestStat1)))
                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(0), Truncate(0), sComment:=" EO Power On BIT Test FAILED.")
                GET_BIT_DATA_FETCH(TestStat2)
                Echo(EoError(TestStat1))
                If TestStat2 <> TestStat1 Then
                    Echo(EoError(TestStat2))
                End If
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " VEO2 Power On BIT Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_1_1
            End If
            frmSTest.proProgress.Value = 2
            LoopStepNo += 1

            'Turn camera on
            SET_CAMERA_POWER_INITIATE(1)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : TestStat1 = 2
            Do While 1 <> TestStat1 'Loop until it gets there or for 20 seconds
                SET_CAMERA_POWER_FETCH(TestStat1)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Echo("Camera did not get to status = 1") 'BB
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete
            Delay(30)

            '4.2 Commanded BIT
            ' These series of tests are performed by sending individual commands to the VEO-2
            'and verifying that the commands are completed by checking feedback sensors or
            'parameters
            '4.2.1 Camera Energy Probe Select Assy (CEPSA) functionality
            'The CEPSA is a motor driven slide assembly with three positions: Pellicle, Energy
            ' Probe and a clear aperture.  It places the selected position into the converging beam
            ' coming from the collimator optics.  This procedure verifies correct operation by
            ' commanding the slide to HOME and the three positions an verifying tht the positions
            ' arereached by feedback sensors.

            'Command the CEPSA to Clear Open position.
            'Verify that the Clear Open position by checking the detent status.

            Echo(vbCrLf & "Test CEPSA (Camera Energy Probe Select Assembly)")

VEO2_2_1:
            sTNum = "VEO-02-001"
            SET_SENSOR_STAGE_LASER_INITIATE(1)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 1 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SENSOR_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If Sensor_Position <> 1 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 CEPSA Clear Open Position Test", False)
                Echo("  Exp 1   Meas " & CStr(Truncate(Sensor_Position)))
                RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(1), Truncate(1), sComment:=" EO CEPSA Clear Open Position Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " EO CEPSA Clear Open Position Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_2_1
            End If
            frmSTest.proProgress.Value = 4
            LoopStepNo += 1

VEO2_2_2:
            'Command the CEPSA to Energy Probe position.
            'Verify Pellicle position by checking the detent status.
            sTNum = "VEO-02-002"
            SET_SENSOR_STAGE_LASER_INITIATE(2)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 2 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SENSOR_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If Sensor_Position <> 2 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 CEPSA Energy Probe Position Test", False)
                Echo("  Exp 2   Meas " & CStr(Truncate(Sensor_Position)))
                RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(2), Truncate(2), sComment:=" EO CEPSA Energy Probe Position Test FAILED.")
                Echo(EoError(TestStat1))
                If TestStat2 <> TestStat1 Then
                    Echo(EoError(TestStat2))
                End If
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " VEO2 CEPSA Energy Probe Position Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_2_2
            End If
            frmSTest.proProgress.Value = 6
            LoopStepNo += 1

VEO2_2_3:
            'Command the CEPSA to Pellicle position.
            'Verify Energy Probe pos by checking the detent status
            sTNum = "VEO-02-003"
            SET_SENSOR_STAGE_LASER_INITIATE(3)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 3 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SENSOR_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If Sensor_Position <> 3 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 CEPSA Pellicle Position Test", False)
                Echo("  Exp 3   Meas " & CStr(Truncate(Sensor_Position)))
                RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(3), Truncate(3), sComment:=" EO CEPSA Pellicle Position Test FAILED.")
                Echo(EoError(TestStat1))
                If TestStat2 <> TestStat1 Then
                    Echo(EoError(TestStat2))
                End If
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " VEO2 CEPSA Pellicle Position Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_2_3
            End If
            frmSTest.proProgress.Value = 8
            LoopStepNo += 1

            '4.2.2 Source Selected Assy (SSA) functionality
            'The SSA is a motor driven slide assembly with three positions:  Visible Source,
            ' Mirror, Laser Boresight Assy and a clear aperature.  It places the selected
            ' position into the optical line of sight of the beam coming from the colimator
            ' optics. This procedure verifies correct operation by commanding the slide to its
            ' HOME position and three testing positions and verifying that the positions are
            ' reached by feedback sensors.
            '
            Echo(vbCrLf & "Test SSA (Source Selected Assembly)")

VEO2_3_1:
            'Command the SSA to the Laser Boresight position.
            'Verify Laser Boresight pos by checking the detent status
            sTNum = "VEO-03-001"
            SET_SOURCE_STAGE_LASER_INITIATE(3)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 3 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SOURCE_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If Sensor_Position <> 3 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 SSA Laser Boresight Position Test", False)
                Echo("  Exp 3   Meas " & CStr(Truncate(Sensor_Position)))
                RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(3), Truncate(3), sComment:=" EO SSA Laser Boresight Position Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " EO SSA Laser Boresight Position Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_3_1
            End If
            frmSTest.proProgress.Value = 10
            LoopStepNo += 1

VEO2_3_2:
            'Command the SSA Laser to Clear Open position.
            'Verify Clear Open by checking the detent status
            sTNum = "VEO-03-002"
            SET_SOURCE_STAGE_LASER_INITIATE(1)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 1 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SOURCE_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If Sensor_Position <> 1 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 SSA Clear Open Position Test", False)
                Echo("  Exp 1   Meas " & CStr(Truncate(Sensor_Position)))
                RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(1), Truncate(1), sComment:=" EO SSA Clear Open Position Test FAILED.")
                Echo(EoError(TestStat1))
                If TestStat2 <> TestStat1 Then
                    Echo(EoError(TestStat2))
                End If
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " VEO2 SSA Clear Open Position Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_3_2
            End If
            frmSTest.proProgress.Value = 12
            LoopStepNo += 1

VEO2_3_3:
            'Command the SSA to Visible Source position.
            'Verify Visible Source pos by checking the detent status
            sTNum = "VEO-03-003"
            SET_SOURCE_STAGE_LASER_INITIATE(2)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 2 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SOURCE_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If Sensor_Position <> 2 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " VEO2 Visible Source Position Test", False)
                Echo("  Exp 2   Meas " & CStr(Truncate(Sensor_Position)))
                RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(2), Truncate(2), sComment:=" EO Visible Source Position Test FAILED.")
                Echo(EoError(TestStat1))
                If TestStat2 <> TestStat1 Then
                    Echo(EoError(TestStat2))
                End If
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " VEO2 Visible Source Position Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                GoTo VEO2_3_3
            End If
            frmSTest.proProgress.Value = 14
            LoopStepNo += 1

            '4.2.3 Target Wheel functionality
            'The target Wheel is a motor driven asy that positions the VEO-2 targets into
            ' the optical line of sight.  This procedure verifies that the Target Wheel is
            'functioning properly by verifying each position is attained after it is commanded.

            Echo(vbCrLf & "Test Target Wheel")

            'Set Target to each position.  Verify that the position was reached by checking the detent status.
            For i = 0 To 14
VEO2_4:
                sTNum = "VEO-04-" & Format(i + 1, "000") ' 001 to 015

                SET_TARGET_POSITION_INITIATE(CLng(i))
                Delay(1) : iTry = 1 : Sensor_Position = -1
                Do While i <> Sensor_Position 'Loop until it gets there or for 20 seconds
                    SET_TARGET_POSITION_FETCH(Sensor_Position)
                    Delay(1)
                    iTry += 1
                    If iTry > 120 Then
                        Exit Do
                    End If
                    Application.DoEvents()
                    If AbortTest = True Then Exit Do
                Loop
                If AbortTest = True Then GoTo TestComplete

                If Sensor_Position <> i Then
                    TestVEO = FAILED
                    FormatResultLine(sTNum & " VEO2 Target Wheel Position " & CStr(i) & " Test", False)
                    Echo("  Expc" & CStr(i) & "  Meas " & CStr(Truncate(Sensor_Position)))
                    RegisterFailure(VEO2, sTNum, Truncate(Sensor_Position), "", Truncate(i), Truncate(i), sComment:=" EO Target Wheel Position " & CStr(i) & " Test FAILED.")
                    IncStepFailed()
                    If OptionFaultMode = SOFmode Then GoTo TestComplete
                Else
                    FormatResultLine(sTNum & " VEO2 Target Wheel Position " & CStr(i) & " Test", True)
                    IncStepPassed()
                End If
                If AbortTest = True Then GoTo TestComplete
                If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
                    GoTo VEO2_4
                End If
                frmSTest.proProgress.Value = frmSTest.proProgress.Value + 2 '16 to 44
                LoopStepNo += 1 ' 8 to 22
            Next i

            'VEO2_4_16:
            '
            ''NOTE THIS TARGET HAS BEEN DELETED.  TESTS VEO2_4_16 and VEO2_4_17 are no longer valid
            '
            '    'Command the Spinning Pinhole target to rotate at 0.5 radians per second.
            '    'Verify rotation from the control feedback indicator.
            '    sTNum = "VEO-04-016"
            '    SET_TARGET_POSITION_INITIATE (10)
            '    Sleep 1000: iTry = 1: Sensor_Position = -1
            '    Do While 10 <> Sensor_Position 'Loop until it gets there or for 20 seconds
            '      SET_TARGET_POSITION_FETCH VarPtr(Sensor_Position)
            '      Sleep 1000
            '      iTry = iTry + 1
            '      If iTry > 20 Then
            '        Exit Do
            '      End If
            '      DoEvents
            '      If AbortTest = True Then Exit Do
            '    Loop
            '    If AbortTest = True Then GoTo TestComplete
            '
            '    SET_ANGULAR_RATE_VIS_INITIATE (0.5)
            '    Sleep 1000: iTry = 1: Rotation_Rate = -1
            '    Do While 0.5 <> Rotation_Rate 'Loop until it gets there or for 20 seconds
            '      SET_ANGULAR_RATE_VIS_FETCH VarPtr(Rotation_Rate)  ' range 0 to 3
            '      Sleep 1000
            '      iTry = iTry + 1
            '      If iTry > 20 Then
            '        Exit Do
            '      End If
            '      DoEvents
            '      If AbortTest = True Then Exit Do
            '    Loop
            '    If AbortTest = True Then GoTo TestComplete
            '
            '    If Rotation_Rate <> 0.5 Then
            '      TestVEO = FAILED
            '      FormatResultLine sTNum & " EO Target Wheel Spinning Pinhole Rate Test1", False
            '      Echo "  Exp 0.5   Meas " & cstr(val(Sensor_Position))
            '      RegisterFailure VEO, sTNum, val(Rotation_Rate), "", val(0.5), val(0.5), sComment:=" EO Target Wheel Spinning Pinhole Rate Test1 FAILED."
            '      Call IncStepFailed
            '      If OptionFaultMode = SOFmode Then GoTo TestComplete
            '    Else
            '      FormatResultLine sTNum & " EO Target Wheel Spinning Pinhole Rate Test1", True
            '      Call IncStepPassed
            '    End If
            '    If AbortTest = True Then GoTo TestComplete
            '    If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 23 Then
            '      GoTo VEO2_4_16
            '    End If
            '    frmSTest.proProgress.value = 46
            '    LoopStepNo = LoopStepNo + 1
            '
            'VEO2_4_17:
            '
            ''NOTE THIS TARGET HAS BEEN DELETED.  TESTS VEO2_4_16 and VEO2_4_17 are no longer valid
            '
            '    'Command the Spinning Pinhole target to rotate at 3 radians per second.
            '    'Verify rotation from the control feedback indicator.
            '    sTNum = "VEO-04-017"
            '
            '    SET_ANGULAR_RATE_VIS_INITIATE (3)
            '    Sleep 1000: iTry = 1: Rotation_Rate = -1
            '    Do While 3 <> Rotation_Rate 'Loop until it gets there or for 20 seconds
            '      SET_ANGULAR_RATE_VIS_FETCH VarPtr(Rotation_Rate)  ' range 0 to 3
            '      Sleep 1000
            '      iTry = iTry + 1
            '      If iTry > 20 Then
            '        Exit Do
            '      End If
            '      DoEvents
            '      If AbortTest = True Then Exit Do
            '    Loop
            '    If AbortTest = True Then GoTo TestComplete
            '
            '    If Rotation_Rate <> 3 Then
            '      TestVEO = FAILED
            '      FormatResultLine sTNum & " EO Target Wheel Spinning Pinhole Rate Test2", False
            '      Echo "  Exp 3   Meas " & CStr(val(Rotation_Rate))
            '      RegisterFailure VEO, sTNum, val(Rotation_Rate), "", val(3), val(3), sComment:=" EO Target Wheel Spinning Pinhole Rate Test2 FAILED."
            '      Call IncStepFailed
            '      If OptionFaultMode = SOFmode Then GoTo TestComplete
            '    Else
            '      FormatResultLine sTNum & " EO Target Wheel Spinning Pinhole Rate Test2", True
            '      Call IncStepPassed
            '    End If
            '    If AbortTest = True Then GoTo TestComplete
            '    If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 24 Then
            '      GoTo VEO2_4_17
            '    End If
            '    frmSTest.proProgress.value = 48
            '    LoopStepNo = LoopStepNo + 1
            '
            '    'stop spinning pinhole
            '   SET_ANGULAR_RATE_VIS_INITIATE (0)
            '   Delay 1

            'End deleted sections VEO2_4_16 and VEO2_4_17 Astronics DME

            SET_TARGET_POSITION_INITIATE(1) 'Move Target Wheel back to boresight position

            '4.2.6 VisibleLamp and Vane functionality
            ' This procedure verifies that the visible lamp is working and within range based on
            ' an internal detector monitor.  Vane functionality is checked by moving them and
            ' detecting light level change within the sphere.

            Echo(vbCrLf & "Test Visible Lamp and Vane")
            LoopStepNo = 25
VEO2_5_1:
            'Command the Visible Source to .001 uW/cm2-sr.
            'Verify that the detector source goes ready.
            'Command the visible light OFF
            sTNum = "VEO-05-001"
            Delay(5)
            SET_RADIANCE_VIS_INITIATE(0.001)
            System.Threading.Thread.Sleep(30000) ' 30 sec
            Status_Byte = 0
            iTry = 1
            Do While Status_Byte <> 128
                If (iTry Mod 10) = 0 Then
                    SET_RADIANCE_VIS_INITIATE(0.001)
                End If
                System.Threading.Thread.Sleep(2000) ' max 2 sec * 300 = 10 min?
                GET_STATUS_BYTE_MESSAGE_INITIATE()
                System.Threading.Thread.Sleep(100)
                GET_STATUS_BYTE_MESSAGE_FETCH(Status_Byte)
                iTry += 1
                If iTry > 300 Then
                    Exit Do
                End If
                Status_Byte = Status_Byte And &HF0
            Loop
            If Status_Byte <> 128 Then
                TestStatus = FAILED
                FormatResultLine(sTNum & " Visible Lamp Set Radiance Status Ready Test", False)
                Echo("  Exp 641   Meas " & CStr(Val(CStr(Status_Byte))))
                RegisterFailure(VEO2, sTNum, Val(CStr(Status_Byte)), "", CDbl(641), CDbl(641), sComment:=" Visible Lamp Set Radiance Status Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Lamp Set Radiance Status Ready Test", True)
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 25
                GoTo VEO2_5_1
            End If
            frmSTest.proProgress.Value = 50
            LoopStepNo += 1

VEO2_5_2:
            Delay(1) : VIS_Output = -1
            iTry = 1
            Do While VIS_Output < 0 Or VIS_Output > 0.0011
                If (iTry Mod 10) = 0 Then
                    SET_RADIANCE_VIS_INITIATE(0.001)
                    System.Threading.Thread.Sleep(2000)
                End If
                SET_RADIANCE_VIS_FETCH(VIS_Output)
                System.Threading.Thread.Sleep(2000)
                iTry += 1
                If iTry > 60 Then ' max time 60 * 2 sec = 2 min
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            sTNum = "VEO-05-002"
            If VIS_Output < 0 Or VIS_Output > 0.0011 Then
                TestStatus = FAILED
                FormatResultLine(sTNum & " Visible Lamp Read Back Value", False)
                Echo("  Min 0   Max 0.0011   Meas " & CStr(Val(CStr(VIS_Output))) & " uw/cm2-sr")
                RegisterFailure(VEO2, sTNum, Val(CStr(VIS_Output)), "uW/cm2-sr", 0, 0.0011, sComment:=" Visible Lamp Read Back Value Test FAILED.")
                Call IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Lamp Read Back Value", True)
                Echo("  Min 0   Max 0.0011   Meas " & CStr(Val(CStr(VIS_Output))) & " uw/cm2-sr")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 26
                GoTo VEO2_5_2
            End If
            frmSTest.proProgress.Value = 52
            LoopStepNo += 1

VEO2_5_3:
            'Command the Visible Source to 4,000 uW/cm2-sr.
            'Verify that the detector source goes ready.
            sTNum = "VEO-05-003"
            Delay(5)

            SET_RADIANCE_VIS_INITIATE(4000)
            VIS_Output = 0
            iTry = 1 'Loop until it gets there or for 60 seconds
            Do While VIS_Output < 3600 Or VIS_Output > 4400
                If (iTry Mod 5) = 0 Then
                    SET_RADIANCE_VIS_INITIATE(4000)
                    System.Threading.Thread.Sleep(2000)
                End If
                SET_RADIANCE_VIS_FETCH(VIS_Output)
                System.Threading.Thread.Sleep(3000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            If AbortTest = True Then GoTo TestComplete

            If VIS_Output < 3500 Or VIS_Output > 4500 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Lamp Read Back Value", False)
                Echo("  Min 3500   Max 4500   Meas " & CStr(Val(CStr(VIS_Output))) & " uw/cm2-sr")
                RegisterFailure(VEO2, sTNum, Val(CStr(VIS_Output)), "uW/cm2-sr", 3500, 4500, sComment:=" Visible Lamp Read Back Value Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Lamp Read Back Value", True)
                Echo("  Min 3500   Max 4500   Meas " & CStr(Val(CStr(VIS_Output))) & " uw/cm2-sr")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 27
                GoTo VEO2_5_3
            End If
            frmSTest.proProgress.Value = 54
            LoopStepNo += 1
            System.Threading.Thread.Sleep(1000)

            '-------------------------------------------------------------------------------
            'From Jeremy 2016-04-25
            Dim Vis_Source_Fail As Boolean
            Dim Vis_Output_Low As Single


            'Verify that the visible source max radiance level goes ready.
            Vis_Source_Fail = False
            Echo("Checking maximum obtainable radiance level...")
            Echo("Setting Visible Source Radiance Level to 4000 µW/cm²/sr.")
            Echo("Allowing Visible Source to settle, please stand-by." & vbCrLf)
            SET_RADIANCE_VIS_INITIATE(4000)
            Delay(60)
            VIS_Output = 0
            iTry = 1
            Do While VIS_Output < 3950 Or VIS_Output > 4050
                SET_RADIANCE_VIS_FETCH(VIS_Output)
                Delay(5)
                iTry = iTry + 1
                If iTry > 30 Then
                    Echo("Visible Source failed to reach 4000 uW/cm2-sr or detector faulted.")
                    Echo("Measured value:  " & VIS_Output & " µW/cm²/sr")
                    Vis_Source_Fail = True
                    If VIS_Output = 0 Then
                        Echo("Aborting maximum radiance adjustment.")
                        iTry = 0
                    End If
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then GoTo testcomplete
            Loop

            If iTry > 30 Then
                Echo(vbCrLf & "Setting Visible Source Radiance Level to 3000 µW/cm²/sr.")
                Echo("Allowing Visible Source to settle, please stand-by." & vbCrLf)
                SET_RADIANCE_VIS_INITIATE(3000)
                Delay(60)
                VIS_Output = 0
                iTry = 1
                Do While VIS_Output < 2950 Or VIS_Output > 3050
                    SET_RADIANCE_VIS_FETCH(VIS_Output)
                    Delay(3)
                    iTry = iTry + 1
                    If iTry > 30 Then
                        Echo("Visible Source failed to reach 3000 uW/cm2-sr or detector faulted.")
                        Echo("Measured value:  " & VIS_Output & " µW/cm²/sr")
                        Exit Do
                    End If
                    Application.DoEvents()
                    If AbortTest = True Then GoTo testcomplete
                Loop
            End If

            If iTry > 30 Then
                Echo(vbCrLf & "Setting Visible Source Radiance Level to 2000 µW/cm²/sr.")
                Echo("Allowing Visible Source to settle, please stand-by." & vbCrLf)
                SET_RADIANCE_VIS_INITIATE(2000)
                Delay(60)
                VIS_Output = 0
                iTry = 1
                Do While VIS_Output < 1950 Or VIS_Output > 2050
                    SET_RADIANCE_VIS_FETCH(VIS_Output)
                    Delay(3)
                    iTry = iTry + 1
                    If iTry > 30 Then
                        Echo("Visible Source failed to reach 2000 uW/cm2-sr or detector faulted.")
                        Echo("Measured value:  " & VIS_Output & " µW/cm²/sr")
                        Exit Do
                    End If
                    Application.DoEvents()
                    If AbortTest = True Then GoTo testcomplete
                Loop
            End If

            If iTry > 30 Then
                Echo(vbCrLf & "Setting Visible Source Radiance Level to 1000 µW/cm²/sr.")
                Echo("Allowing Visible Source to settle, please stand-by." & vbCrLf)
                SET_RADIANCE_VIS_INITIATE(1000)
                Delay(60)
                VIS_Output = 0
                iTry = 1
                Do While VIS_Output < 950 Or VIS_Output > 1050
                    SET_RADIANCE_VIS_FETCH(VIS_Output)
                    Delay(3)
                    iTry = iTry + 1
                    If iTry > 30 Then
                        Echo("Visible Source failed to reach 1000 uW/cm2-sr or detector faulted.")
                        Echo("Measured value:  " & VIS_Output & " µW/cm²/sr")
                        Exit Do
                    End If
                    Application.DoEvents()
                    If AbortTest = True Then GoTo testcomplete
                Loop
            End If

            Echo("Visible Source Measured value:  " & VIS_Output & " µW/cm²/sr")

            '     'Open camera to view spot
            '      Load cy1s16
            '      MaximizeTask ("CyDisplayEx")
            '      MsgBox "Pinhole Target as observed by VEO-2 Vis-NIR Camera", vbOKOnly, "Pinhole Target (Left Position)"
            'sMsg = "Pinhole Target as observed by VEO-2 Vis-NIR Camera"
            'iMsgBox sMsg, "", "&Next >"

            '     'Close camera window
            '      cy1s16.StopAcquisition
            '      cy1s16.CloseDisplay
            '     Unload cy1s16

            'Press RETURN to take a camera boresight pixel reading (this captures the
            '  beam pixels from the boresight return beam).
            VIS_Output = CInt(VIS_Output)
            If Vis_Source_Fail = True Then
                Vis_Output_Low = VIS_Output
                Echo(vbCrLf & "VEO-2 was unable to achieve maximum radaince level.")
                Echo("IR Camera may not detect visible source at this level.")
                Echo("Setting up VEO-2 to capture boresight (pinhole) target with radiance level " & VIS_Output & " µW/cm²/sr...")
            End If

            '---------------------------------------------------------------------------------

            '4.2.8 Boresight pixel determination (Corner Cube Assy)
            'Note: This procedure requires operator interaction.  The Corner Cube Assy must
            ' be manually rotated through its three positions to obtain valid data.
            ' This procedure only needs to be performed once after mounting the VEO-2 to the TPS
            ' fixture and does not need to be performed again unless either a severe bump to the
            ' system occurs or it is removed and replaced on a fixture.

            Echo(vbCrLf & "Test Boresight Pixel Determination (Corner Cube Assy)")

            'This procedure verifies that the boresight pixel on the camera, as established
            'by the internal corner cube, is within its operating region(center 10% of the array).
            'It will require that the corner cube be moved to each of its three postions and a
            'reading taken at each position.

            IRWIN_SHUTDOWN()

            S = "1. Use a 1/2 inch hex wrench to rotate Corner Cube Assembly fully counterclockwise." & vbCrLf
            S &= "2. Then Rotate it clockwise to the FIRST detent position (at about 1/3 full movement)." & vbCrLf
            S &= "3. Press CONTINUE to take a camera boresight pixel reading." & vbCrLf
            DisplaySetup(S, "ST-VEO2-rotate.jpg", 3)
            If AbortTest = True Then GoTo TestComplete

            Application.DoEvents()
            Delay(30)

            'Press RETURN to take a camera boresight pixel reading (this captures the
            '  beam pixels from the boresight return beam).
            LoopStepNo = 28

VEO2_6_1:
            sTNum = "VEO-06-001" ' Radiance = VIS_Output
            BORESIGHT_TV_VIS_SETUP(4, 1, 320, 256, VIS_Output, 1, 1, 1, 20, 20, 300, 236, 1, 2856, 50) '
            Delay(5)
            BORESIGHT_TV_VIS_INITIATE()
            Delay(1)
            BORESIGHT_TV_VIS_FETCH(xCoord, yCoord, Area, Status_Byte)
            If xCoord < 136 Or xCoord > 184 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (1st CCA Position, X-axis)", False)
                Echo("  Min 136   Max 184   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
                RegisterFailure(VEO2, sTNum, Truncate(xCoord), "mR", 136, 184, sComment:=" Visible Boresight (1st CCA Position, X-axis) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (1st CCA Position, X-axis)", True)
                Echo("  Min 136   Max 184   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            frmSTest.proProgress.Value = 56

            sTNum = "VEO-06-002"
            If yCoord < 108 Or yCoord > 147 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (1st CCA Position, Y-axis)", False)
                Echo("  Min 108   Max 147   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
                RegisterFailure(VEO2, sTNum, Truncate(yCoord), "mR", 108, 147, sComment:=" Visible Boresight (1st CCA Position, Y-axis) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (1st CCA Position, Y-axis)", True)
                Echo("  Min 108   Max 147   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            frmSTest.proProgress.Value = 57

            sTNum = "VEO-06-003"
            If Area < 200 Or Area > 800 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (1st CCA Position, Area)", False)
                Echo("  Min 200   Max 800   Meas " & CStr(Truncate(Area)) & " pixels")
                RegisterFailure(VEO2, sTNum, Truncate(Area), "pixels", 200, 800, sComment:=" Visible Boresight (1st CCA Position, Area) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (1st CCA Position, Area)", True)
                Echo("  Min 200   Max 800   Meas " & CStr(Truncate(Area)) & " pixels")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then '28
                GoTo VEO2_6_1
            End If
            frmSTest.proProgress.Value = 58
            LoopStepNo += 1


            S = "Rotate Corner Cube Assembly clockwise to the next (SECOND) detent position." & vbCrLf
            S &= "Press OK to take a camera boresight pixel reading." & vbCrLf
            MsgBox(S)
            Application.DoEvents()
            Delay(0.5)

VEO2_6_4:
            sTNum = "VEO-06-004"
            Delay(1)
            BORESIGHT_TV_VIS_SETUP(4, 1, 320, 256, VIS_Output, 1, 1, 1, 20, 20, 300, 236, 1, 2856, 50) '
            Delay(1)
            BORESIGHT_TV_VIS_INITIATE()
            Delay(1)
            BORESIGHT_TV_VIS_FETCH(xCoord, yCoord, Area, Status_Byte)
            If xCoord < 136 Or xCoord > 184 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (2nd CCA Position, X-axis)", False)
                Echo("  Min 136   Max 184   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
                RegisterFailure(VEO2, sTNum, Truncate(xCoord), "mR", 136, 184, sComment:=" Visible Boresight (2nd CCA Position, X-axis) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (2nd CCA Position, X-axis)", True)
                Echo("  Min 136   Max 184   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            frmSTest.proProgress.Value = 60

            sTNum = "VEO-06-005"
            If yCoord < 108 Or yCoord > 147 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (2nd CCA Position, Y-axis)", False)
                Echo("  Min 108   Max 147   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
                RegisterFailure(VEO2, sTNum, Truncate(yCoord), "mR", 108, 147, sComment:=" Visible Boresight (2nd CCA Position, Y-axis) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (2nd CCA Position, Y-axis)", True)
                Echo("  Min 108   Max 147   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            frmSTest.proProgress.Value = 61

            sTNum = "VEO-06-006"
            If Area < 200 Or Area > 800 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (2nd CCA Position, Area)", False)
                Echo("  Min 200   Max 800   Meas " & CStr(Truncate(Area)) & " pixels")
                RegisterFailure(VEO2, sTNum, Truncate(Area), "pixels", 200, 800, sComment:=" Visible Boresight (2nd CCA Position, Area) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (2nd CCA Position, Area)", True)
                Echo("  Min 200   Max 800   Meas " & CStr(Truncate(Area)) & " pixels")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 29
                GoTo VEO2_6_4
            End If
            frmSTest.proProgress.Value = 62
            LoopStepNo += 1

            S = "Rotate Corner Cube Assembly fully clockwise to THIRD detent position." & vbCrLf
            S &= "Press OK to take a camera boresight pixel reading." & vbCrLf
            MsgBox(S)
            Application.DoEvents()
            Delay(0.5)
VEO2_6_7:
            sTNum = "VEO-06-007"
            Delay(1)
            BORESIGHT_TV_VIS_SETUP(4, 1, 320, 256, VIS_Output, 1, 1, 1, 20, 20, 300, 236, 1, 2856, 50) '
            Delay(1)
            BORESIGHT_TV_VIS_INITIATE()
            Delay(1)
            BORESIGHT_TV_VIS_FETCH(xCoord, yCoord, Area, Status_Byte)
            If xCoord < 136 Or xCoord > 184 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (3rd CCA Position, X-axis)", False)
                Echo("  Min 136   Max 184   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
                RegisterFailure(VEO2, sTNum, Truncate(xCoord), "mR", 136, 184, sComment:=" Visible Boresight (3rd CCA Position, X-axis) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (3nd CCA Position, X-axis)", True)
                Echo("  Min 136   Max 184   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            frmSTest.proProgress.Value = 64

            sTNum = "VEO-06-008"
            If yCoord < 108 Or yCoord > 147 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (3rd CCA Position, Y-axis)", False)
                Echo("  Min 108   Max 147   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
                RegisterFailure(VEO2, sTNum, Truncate(yCoord), "mR", 108, 147, sComment:=" Visible Boresight (3rd CCA Position, Y-axis) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (3nd CCA Position, Y-axis)", True)
                Echo("  Min 108   Max 147   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            frmSTest.proProgress.Value = 65

            sTNum = "VEO-06-009"
            If Area < 200 Or Area > 800 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " Visible Boresight (3rd CCA Position, Area)", False)
                Echo("  Min 200   Max 800   Meas " & CStr(Truncate(Area)) & " pixels")
                RegisterFailure(VEO2, sTNum, Truncate(Area), "pixels", 200, 800, sComment:=" Visible Boresight (3rd CCA Position, Area) Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " Visible Boresight (3nd CCA Position, Area)", True)
                Echo("  Min 200   Max 800   Meas " & CStr(Truncate(Area)) & " pixels")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 30
                GoTo VEO2_6_7
            End If
            frmSTest.proProgress.Value = 66
            LoopStepNo += 1


            'Command the Vis lamp to OFF.
            System.Threading.Thread.Sleep(1000)
            SET_RADIANCE_VIS_INITIATE(0)
            System.Threading.Thread.Sleep(1000)

            S = "Rotate Corner Cube Assembly to its STOW (fully counter clockwise) position." & vbCrLf
            S &= "Press OK to continue." & vbCrLf
            MsgBox(S)
            Application.DoEvents()
            Delay(0.5)

            '4.2.9 Precision Pulsed Laser Source (PPLS) laser test
            'This procedure verifies that laser power is coming from the LARRS

            'If LaserContinue = False Then
            '    Echo("VEO-07-001 to VEO-08-006 Laser Tests skipped." & vbCrLf)
            '    GoTo VEO2_9_1
            'End If

            'Echo(vbCrLf & "Precision Pulsed Laser Source (PPLS) tests")

            'S = "Short Laser Interlock as follows:" & vbCrLf & vbCrLf
            'S &= "  Attach cable W25 to VEO-2 J7 (LASR INTLK)." & vbCrLf
            'S &= "  Attach a shorted cable W24 to cable W25." & vbCrLf
            'DisplaySetup(S, "ST-VEO2-interlock.jpg", 1)


            'If AbortTest = True Then GoTo TestComplete
            'Application.DoEvents()
            'Delay(0.5)

            ''Check for LARRS.DAT file ' AZPOS, ELPOS, LAREA_LO, LAREA_HI, XCENTROID_LO, YCENTROID_LO, XCENTROID_HI, YCENTROID_HI
            'If Dir(sLarrs_path & "LARRS.DAT", 17) = "" Then
            '    Echo(" Cannot find file " & sLarrs_path & "LARRS.Dat.")
            '    TestVEO = FAILED
            '    GoTo TestComplete
            'End If

            ''Parse LARRS.DAT
            'x = FreeFile()
            'FileOpen(x, sLarrs_path & "LARRS.DAT", OpenMode.Input)
            'For i = 1 To 3
            '    If Not EOF(x) Then
            '        Input(x, S)
            '        If IsNumeric(S) = False Then
            '            MsgBox("AZPOS invalid! " & vbCrLf & S)
            '            Exit For
            '        End If
            '        AZPOS(i) = CSng(S)

            '        Input(x, S)
            '        If IsNumeric(S) = False Then
            '            MsgBox("ELPOS invalid! " & vbCrLf & S)
            '            Exit For
            '        End If
            '        ELPOS(i) = CSng(S)

            '        'These numbers are now hardcoded into selftest
            '        LAREA_LO(i) = 30
            '        LAREA_HI(i) = 4500
            '        If i = 3 Then LAREA_HI(i) = 12000
            '        XCENTROID_LO(i) = 20
            '        YCENTROID_LO(i) = 20
            '        XCENTROID_HI(i) = 310
            '        YCENTROID_HI(i) = 246


            '    End If
            'Next i
            'FileClose(x)

            'SET_LARRS_EL_LASER_INITIATE(ELPOS(1))
            'Delay(0.1) : TestStat1 = -1 : iTry = 1
            'Do While ELPOS(1) <> TestStat1 'Loop until it gets there or for 90 seconds
            '    SET_LARRS_EL_LASER_FETCH(TestStat1)
            '    Sleep(1000)
            '    iTry += 1
            '    If iTry > 90 Then
            '        Exit Do
            '    End If
            '    Application.DoEvents()
            '    If AbortTest = True Then Exit Do
            'Loop
            'If AbortTest = True Then GoTo TestComplete

            'SET_LARRS_AZ_LASER_INITIATE(AZPOS(1))
            'Delay(1) : TestStat = -1 : iTry = 1
            'Do While AZPOS(1) <> TestStat1 'Loop until it gets there or for 90 seconds
            '    SET_LARRS_AZ_LASER_FETCH(TestStat1)
            '    Sleep(1000)
            '    iTry += 1
            '    If iTry > 90 Then
            '        Exit Do
            '    End If
            '    Application.DoEvents()
            '    If AbortTest = True Then Exit Do
            'Loop
            'If AbortTest = True Then GoTo TestComplete

            'VEO2_07_1:
            '            SELECT_DIODE_LASER_INITIATE(1) ' selects 1540 diode    Delay 1
            '            For i = 1 To 3
            '                SELECT_DIODE_LASER_FETCH(TestStat1)
            '                If TestStat1 = 1 Then Exit For
            '            Next i

            '            SET_LASER_TEST_INITIATE(1)
            '            TestStat1 = 2 : Delay(1) : iTry = 1
            '            Do While 1 <> TestStat1 'Loop until it gets there or for 20 seconds
            '                SET_LASER_TEST_FETCH(TestStat1)
            '                Delay(1)
            '                iTry += 1
            '                If iTry > 20 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete
            '            Delay(20)
            '            BORESIGHT_LASER_SETUP(1, 10, 10, 310, 246, 0, 0, 50)
            '            Delay(5)
            '            BORESIGHT_LASER_INITIATE()
            '            Delay(5)
            '            BORESIGHT_LASER_FETCH(xCoord, yCoord, Area, Status_Byte)
            '            SET_LASER_TEST_INITIATE(0)
            '            SET_LASER_TEST_FETCH(TestStat1)
            '            sTNum = "VEO-07-001"

            '            i = 1
            'DoAgain:
            '            If (xCoord = 0 And yCoord = 0) Or Area = 0 Then
            '                x = MsgBox("The 1540 Laser did not fire!  Verify that the Laser Interlock is shorted." & vbCrLf & vbCrLf & "Do you want to retry?", MsgBoxStyle.YesNo)
            '                If x = DialogResult.Yes Then
            '                    Delay(20)
            '                    SELECT_DIODE_LASER_INITIATE(1) ' selects 1540 diode    Delay 1
            '                    SELECT_DIODE_LASER_FETCH(TestStat1)
            '                    SET_LASER_TEST_INITIATE(1)
            '                    TestStat1 = 2 : Delay(1) : iTry = 1
            '                    Do While 1 <> TestStat1 'Loop until it gets there or for 20 seconds
            '                        SET_LASER_TEST_FETCH(TestStat1)
            '                        Delay(1)
            '                        iTry += 1
            '                        If iTry > 20 Then
            '                            Exit Do
            '                        End If
            '                        Application.DoEvents()
            '                        If AbortTest = True Then Exit Do
            '                    Loop
            '                End If
            '                i += 1
            '                If i < 3 Then
            '                    Delay(20)
            '                    BORESIGHT_LASER_SETUP(1, 10, 10, 310, 246, 0, 0, 50)
            '                    Delay(5)
            '                    BORESIGHT_LASER_INITIATE()
            '                    Delay(5)
            '                    BORESIGHT_LASER_FETCH(xCoord, yCoord, Area, Status_Byte)
            '                    SET_LASER_TEST_INITIATE(0)
            '                    GoTo DoAgain
            '                End If
            '            End If

            '            If xCoord < XCENTROID_LO(1) Or xCoord > XCENTROID_HI(1) Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1540nm LASER Test, (X-axis)", False)
            '                Echo("  Min " & CStr(Truncate(XCENTROID_LO(1))) & "   Max " & CStr(Truncate(XCENTROID_HI(1))) & "   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
            '                RegisterFailure(VEO2, sTNum, Truncate(xCoord), "mR", Truncate(XCENTROID_LO(1)), Truncate(XCENTROID_HI(1)), sComment:=" PPLS 1540nm LASER Test, (X-axis) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1540nm LASER Test, (X-axis)", True)
            '                Echo("  Min " & CStr(Truncate(XCENTROID_LO(1))) & "   Max " & CStr(Truncate(XCENTROID_HI(1))) & "   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
            '                IncStepPassed()
            '            End If
            '            frmSTest.proProgress.Value = 68

            '            sTNum = "VEO-07-002"
            '            If yCoord < YCENTROID_LO(1) Or yCoord > YCENTROID_HI(1) Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1540nm LASER Test, (Y-axis)", False)
            '                Echo("  Min " & CStr(Truncate(YCENTROID_LO(1))) & "   Max " & CStr(Truncate(YCENTROID_HI(1))) & "   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
            '                RegisterFailure(VEO2, sTNum, Truncate(yCoord), "mR", Truncate(YCENTROID_LO(1)), Truncate(YCENTROID_HI(1)), sComment:=" PPLS 1540nm LASER Test, (Y-axis) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1540nm LASER Test, (Y-axis)", True)
            '                Echo("  Min " & CStr(Truncate(YCENTROID_LO(1))) & "   Max " & CStr(Truncate(YCENTROID_HI(1))) & "   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
            '                IncStepPassed()
            '            End If
            '            frmSTest.proProgress.Value = 69

            '            sTNum = "VEO-07-003"
            '            If LAREA_LO(1) > Area Or LAREA_HI(1) < Area Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1540nm LASER Test, (Area)", False)
            '                Echo("  Min " & CStr(Truncate(LAREA_LO(1))) & "   Max " & CStr(Truncate(LAREA_HI(1))) & "   Meas " & CStr(Truncate(Area)) & " pixels")
            '                RegisterFailure(VEO2, sTNum, Truncate(Area), "pixels", Truncate(LAREA_LO(1)), Truncate(LAREA_HI(1)), sComment:=" PPLS 1540nm LASER Test, (Area) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1540nm LASER Test, (Area)", True)
            '                Echo("  Min " & CStr(Truncate(LAREA_LO(1))) & "   Max " & CStr(Truncate(LAREA_HI(1))) & "   Meas " & CStr(Truncate(Area)) & " pixels")
            '                IncStepPassed()
            '            End If
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 31 Then
            '                GoTo VEO2_07_1
            '            End If
            '            LoopStepNo += 1
            '            frmSTest.proProgress.Value = 70


            'VEO2_07_4:
            '            SET_LASER_TEST_INITIATE(0)
            '            For i = 1 To 3
            '                SET_LASER_TEST_FETCH(TestStat1)
            '                If TestStat1 = 0 Then Exit For
            '            Next i

            '            SELECT_DIODE_LASER_INITIATE(0) ' selects 1570 diode
            '            For i = 1 To 3
            '                SELECT_DIODE_LASER_FETCH(TestStat1)
            '                Delay(1)
            '                If TestStat1 = 0 Then Exit For
            '            Next i

            '            SET_LASER_TEST_INITIATE(1)
            '            TestStat1 = 2 : Delay(1) : iTry = 1
            '            Do While 1 <> TestStat1 'Loop until it gets there or for 20 seconds
            '                SET_LASER_TEST_FETCH(TestStat1)
            '                Delay(1)
            '                iTry += 1
            '                If iTry > 20 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete
            '            Delay(20)
            '            BORESIGHT_LASER_SETUP(1, 10, 10, 310, 246, 0, 0, 50) ' 1 sec
            '            Delay(5)
            '            BORESIGHT_LASER_INITIATE() ' 25 sec
            '            Delay(5)
            '            BORESIGHT_LASER_FETCH(xCoord, yCoord, Area, Status_Byte)
            '            sTNum = "VEO-07-004"
            '            If xCoord < XCENTROID_LO(2) Or xCoord > XCENTROID_HI(2) Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1570nm LASER Test, (X-axis)", False)
            '                Echo("  Min " & CStr(Truncate(XCENTROID_LO(2))) & "   Max " & CStr(Truncate(XCENTROID_HI(2))) & "   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
            '                RegisterFailure(VEO2, sTNum, Truncate(xCoord), "mR", Truncate(XCENTROID_LO(2)), Truncate(XCENTROID_HI(2)), sComment:=" PPLS 1570nm LASER Test, (X-axis) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1570nm LASER Test, (X-axis)", True)
            '                Echo("  Min " & CStr(Truncate(XCENTROID_LO(2))) & "   Max " & CStr(Truncate(XCENTROID_HI(2))) & "   Meas " & CStr(Truncate(xCoord)) & " milliRadians")
            '                IncStepPassed()
            '            End If
            '            frmSTest.proProgress.Value = 72

            '            sTNum = "VEO-07-005"
            '            If yCoord < YCENTROID_LO(2) Or yCoord > YCENTROID_HI(2) Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1570nm LASER Test, (Y-axis)", False)
            '                Echo("  Min " & CStr(Truncate(YCENTROID_LO(2))) & "   Max " & CStr(Truncate(YCENTROID_HI(2))) & "   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
            '                RegisterFailure(VEO2, sTNum, Truncate(yCoord), "mR", Truncate(YCENTROID_LO(2)), Truncate(YCENTROID_HI(2)), sComment:=" PPLS 1570nm LASER Test, (Y-axis) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1570nm LASER Test, (Y-axis)", True)
            '                Echo("  Min " & CStr(Truncate(YCENTROID_LO(2))) & "   Max " & CStr(Truncate(YCENTROID_HI(2))) & "   Meas " & CStr(Truncate(yCoord)) & " milliRadians")
            '                IncStepPassed()
            '            End If
            '            frmSTest.proProgress.Value = 73

            '            sTNum = "VEO-07-006"
            '            If LAREA_LO(2) > Area Or LAREA_HI(2) < Area Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1570nm LASER Test, (Area)", False)
            '                Echo("  Min " & CStr(Truncate(LAREA_LO(2))) & "   Max " & CStr(Truncate(LAREA_HI(2))) & "   Meas " & CStr(Truncate(Area)) & " pixels")
            '                RegisterFailure(VEO2, sTNum, Truncate(Area), "pixels", Truncate(LAREA_LO(2)), Truncate(LAREA_HI(2)), sComment:=" PPLS 1570nm LASER Test, (Area) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1570nm LASER Test, (Area)", True)
            '                Echo("  Min " & CStr(Truncate(LAREA_LO(2))) & "   Max " & CStr(Truncate(LAREA_HI(2))) & "   Meas " & CStr(Truncate(Area)) & " pixels")
            '                IncStepPassed()
            '            End If
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 32 Then
            '                GoTo VEO2_07_4
            '            End If
            '            LoopStepNo += 1
            '            frmSTest.proProgress.Value = 74


            'VEO2_07_7:
            '            SET_LASER_TEST_INITIATE(0) ' sets test mode (full power)
            '            For i = 1 To 3
            '                SET_LASER_TEST_FETCH(TestStat1)
            '                If TestStat1 = 0 Then Exit For
            '            Next i

            '            SELECT_DIODE_LASER_INITIATE(2) ' selects 1064 diode
            '            For i = 1 To 3
            '                SELECT_DIODE_LASER_FETCH(TestStat1)
            '                Delay(1)
            '                If TestStat1 = 0 Then Exit For
            '            Next i

            '            SET_TRIGGER_SOURCE_LASER_INITIATE(1) ' Free run
            '            For i = 1 To 3
            '                SET_TRIGGER_SOURCE_LASER_FETCH(TestStat1)
            '                If TestStat1 = 0 Then Exit For
            '            Next i
            '            SET_PULSE_AMPLITUDE_LASER_INITIATE(3000)
            '            For i = 1 To 3
            '                SET_PULSE_AMPLITUDE_LASER_FETCH(TestStat5) ' single
            '            Next i

            '            For i = 1 To 3
            '                SET_OPERATION_LASER_INITIATE(1) 'LASER ON

            '                TestStat1 = 1 : Delay(1) : iTry = 1
            '                Do While 2 <> TestStat1 'Loop until it gets there or for 30 seconds
            '                    SET_OPERATION_LASER_FETCH(TestStat1)
            '                    Delay(1)
            '                    iTry += 1
            '                    If iTry > 30 Then
            '                        Exit Do
            '                    End If
            '                    Application.DoEvents()
            '                    If AbortTest = True Then Exit Do
            '                Loop
            '                If TestStat1 = 2 Then Exit For
            '            Next i
            '            SET_OPERATION_LASER_INITIATE(0) 'LASER OFF
            '            For i = 1 To 3
            '                SET_OPERATION_LASER_FETCH(TestStat2)
            '            Next i
            '            SET_LASER_TEST_INITIATE(0)
            '            For i = 1 To 3
            '                SET_LASER_TEST_FETCH(TestStat2)
            '            Next i
            '            If AbortTest = True Then GoTo TestComplete

            '            sTNum = "VEO-07-007"
            '            If TestStat1 <> 2 Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1064nm LASER Test, (Pulse Mode)", False)
            '                Echo("  Status = " & CStr(TestStat1))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), , , , sComment:=" PPLS 1064nm LASER Test, (Pulse Mode) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1064nm LASER Test, (Pulse Mode)", True)
            '                Echo("  Status = " & CStr(TestStat1))
            '                IncStepPassed()
            '            End If
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 33 Then
            '                GoTo VEO2_07_7
            '            End If
            '            LoopStepNo += 1
            '            frmSTest.proProgress.Value = 76

            '            'Now test the laser when turned off
            'VEO2_07_8:
            '            sTNum = "VEO-07-008"

            '            For i = 1 To 3
            '                SET_OPERATION_LASER_INITIATE(0) 'LASER OFF
            '                SET_OPERATION_LASER_FETCH(TestStat1)
            '                TestStat1 = 2 : Delay(1) : iTry = 1
            '                Do While 0 <> TestStat1 'Loop until it gets there or for 30 seconds
            '                    SET_LASER_TEST_FETCH(TestStat1)
            '                    SET_LASER_TEST_INITIATE(0)
            '                    Delay(1)
            '                    iTry += 1
            '                    If iTry > 30 Then
            '                        Exit Do
            '                    End If
            '                    Application.DoEvents()
            '                    If AbortTest = True Then Exit Do
            '                Loop
            '                If TestStat1 = 0 Then Exit For
            '            Next i
            '            i = 1
            '            If TestStat1 < 0 Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " PPLS 1064nm LASER Test, (Pulse Mode)", False)
            '                Echo("  Status = " & CStr(TestStat1))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), , , , sComment:=" PPLS 1064nm LASER Test, (Pulse Mode) Test FAILED.")
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " PPLS 1064nm LASER Test, (Pulse Mode)", True)
            '                Echo("  Status = " & CStr(TestStat1))
            '                IncStepPassed()
            '            End If
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 34 Then
            '                GoTo VEO2_07_8
            '            End If
            '            LoopStepNo += 1
            '            frmSTest.proProgress.Value = 78


            '            SET_LASER_TEST_INITIATE(0)

            '            S = "Remove the Laser Interlock short on VEO-2 J7." & vbCrLf & vbCrLf
            '            S &= "Press OK to continue." & vbCrLf
            '            MsgBox(S, MsgBoxStyle.OkOnly)
            '            Application.DoEvents()
            '            Delay(0.5)

            '            '4.2.10 LaRRS Azimuth/Elevation and Polarizer motor functionality
            '            'This procedure verifies that the LaRRS positioning motors are functioning properly.

            '            Echo(vbCrLf & "Test LaRRS Azimuth/Elevation and Polarizer Motors")

            '            'Before you move the LaRRS, save off the original position information.

            'VEO2_8_1:
            '            SET_LASER_TEST_INITIATE(0)
            '            'Command the Azimuth motor to HOME.
            '            'Verify that HOME was reached by polling the AZ sensor.
            '            sTNum = "VEO-08-001"

            '            SET_LARRS_AZ_LASER_INITIATE(850)
            '            Sleep(1000) : TestStat1 = -1 : iTry = 1
            '            Do While 850 <> TestStat1 'Loop until it gets there or for 90 seconds
            '                SET_LARRS_AZ_LASER_FETCH(TestStat1)
            '                Sleep(1000)
            '                iTry += 1
            '                If iTry > 90 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete

            '            If TestStat1 <> 850 Then
            '                TestVEO = FAILED
            '                FormatResultLine(sTNum & " LaRRS AZ Motor to Home Position", False)
            '                Echo("  Exp 850   Meas " & CStr(Truncate(TestStat1)))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(850), Truncate(850), sComment:=" LaRRS AZ Motor to Home Position Test FAILED.")
            '                TestVEO = FAILED
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " LaRRS AZ Motor to Home Position", True)
            '                IncStepPassed()
            '            End If
            '            If AbortTest = True Then GoTo TestComplete
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 35 Then
            '                GoTo VEO2_8_1
            '            End If
            '            frmSTest.proProgress.Value = 80
            '            LoopStepNo += 1

            'VEO2_8_2:
            '            'Command the Azimuth motor to move.
            '            'Poll the AZ sensor and verify that it moved.
            '            sTNum = "VEO-08-002"

            '            SET_LARRS_AZ_LASER_INITIATE(950)
            '            Delay(1) : TestStat = -1 : iTry = 1
            '            Do While 950 <> TestStat1 'Loop until it gets there or for 90 seconds
            '                SET_LARRS_AZ_LASER_FETCH(TestStat1)
            '                Sleep(1000)
            '                iTry += 1
            '                If iTry > 90 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete

            '            If TestStat1 <> 950 Then
            '                FormatResultLine(sTNum & " LaRRS AZ Motor jog", False)
            '                Echo("  Exp 950   Meas " & CStr(Truncate(TestStat1)))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(850), Truncate(850), sComment:=" LaRRS AZ Motor jog Test FAILED.")
            '                TestVEO = FAILED
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " LaRRS AZ Motor jog", True)
            '                IncStepPassed()
            '            End If
            '            If AbortTest = True Then GoTo TestComplete
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 36 Then
            '                GoTo VEO2_8_2
            '            End If
            '            frmSTest.proProgress.Value = 82
            '            LoopStepNo += 1

            'VEO2_8_3:
            '            'Command all Elevation motor to HOME.
            '            'Verify that HOME was reached by polling the EL sensor.
            '            sTNum = "VEO-08-003"

            '            SET_LARRS_EL_LASER_INITIATE(850)
            '            Delay(0.1) : TestStat1 = -1 : iTry = 1
            '            Do While 850 <> TestStat1 'Loop until it gets there or for 90 seconds
            '                SET_LARRS_EL_LASER_FETCH(TestStat1)
            '                Sleep(1000)
            '                iTry += 1
            '                If iTry > 90 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete

            '            If TestStat1 <> 850 Then
            '                FormatResultLine(sTNum & " LaRRS EL Motor to Home Position", False)
            '                Echo("  Exp 850   Meas " & CStr(Truncate(TestStat1)))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(850), Truncate(850), sComment:=" LaRRS EL Motor to Home Position Test FAILED.")
            '                TestVEO = FAILED
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " LaRRS EL Motor to Home position", True)
            '                IncStepPassed()
            '            End If
            '            If AbortTest = True Then GoTo TestComplete
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 37 Then
            '                GoTo VEO2_8_3
            '            End If
            '            frmSTest.proProgress.Value = 84
            '            LoopStepNo += 1

            'VEO2_8_4:
            '            'Command the Elevation motor to move.
            '            'Poll the EL sensor and verify that it moved.
            '            sTNum = "VEO-08-004"

            '            SET_LARRS_EL_LASER_INITIATE(950)
            '            Delay(0.1) : TestStat = -1 : iTry = 1
            '            Do While 950 <> TestStat1 'Loop until it gets there or for 90 seconds
            '                SET_LARRS_EL_LASER_FETCH(TestStat1)
            '                Sleep(1000)
            '                iTry += 1
            '                If iTry > 90 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete

            '            If TestStat1 <> 950 Then
            '                FormatResultLine(sTNum & " LaRRS EL Motor jog", False)
            '                Echo("  Exp 950   Meas " & CStr(Truncate(TestStat1)))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(950), Truncate(950), sComment:=" LaRRS EL Motor jog Test FAILED.")
            '                TestVEO = FAILED
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " LaRRS EL Motor jog", True)
            '                IncStepPassed()
            '            End If
            '            If AbortTest = True Then GoTo TestComplete
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 38 Then
            '                GoTo VEO2_8_4
            '            End If
            '            frmSTest.proProgress.Value = 86
            '            LoopStepNo += 1

            '            SET_LARRS_AZ_LASER_INITIATE(0)
            '            Delay(1) : TestStat = -1 : iTry = 1
            '            Do While 0 <> TestStat1 'Loop until it gets there or for 90 seconds
            '                SET_LARRS_AZ_LASER_FETCH(TestStat1)
            '                Sleep(1000)
            '                iTry += 1
            '                If iTry > 90 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete

            'VEO2_8_5:
            '            'Command all Polarizer to HOME.
            '            'Verify that HOME was reached by polling the Polarizer sensor.
            '            sTNum = "VEO-08-005"

            '            For i = 1 To 3
            '                SET_LARRS_POLARIZE_LASER_INITIATE(0)
            '                Delay(0.1) : TestStat1 = -1 : iTry = 1
            '                Do While 0 <> TestStat1 'Loop until it gets there or for 90 seconds
            '                    SET_LARRS_POLARIZE_LASER_FETCH(TestStat1)
            '                    Sleep(1000)
            '                    iTry += 1
            '                    If iTry > 90 Then
            '                        Exit Do
            '                    End If
            '                    Application.DoEvents()
            '                    If AbortTest = True Then Exit Do
            '                Loop
            '                If AbortTest = True Then GoTo TestComplete
            '                If TestStat1 = 0 Then Exit For
            '            Next i

            '            If TestStat1 <> 0 Then
            '                FormatResultLine(sTNum & " LaRRS Polarizer Motor to Home position", False)
            '                Echo("  Exp 0   Meas " & CStr(Truncate(TestStat1)))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(0), Truncate(0), sComment:=" LaRRS Polarizer Motor to Home Position Test FAILED.")
            '                TestVEO = FAILED
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " LaRRS Polarizer Motor to Home position", True)
            '                IncStepPassed()
            '            End If
            '            If AbortTest = True Then GoTo TestComplete
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = 39 Then
            '                GoTo VEO2_8_5
            '            End If
            '            frmSTest.proProgress.Value = 88
            '            LoopStepNo += 1

            'VEO2_8_6:
            '            'Command the Polarizer motor to move.
            '            'Poll the Polarizer sensor and verify that it moved.
            '            sTNum = "VEO-08-006"
            '            LoopStepNo = 40
            '            SET_LARRS_POLARIZE_LASER_INITIATE(100)
            '            Delay(1) : TestStat1 = -1 : iTry = 1
            '            Do While TestStat1 <> 100 'Loop until it gets there or for 180 seconds
            '                SET_LARRS_POLARIZE_LASER_FETCH(TestStat1)
            '                Sleep(1000)
            '                iTry += 1
            '                If iTry > 180 Then
            '                    Exit Do
            '                End If
            '                Application.DoEvents()
            '                If AbortTest = True Then Exit Do
            '            Loop
            '            If AbortTest = True Then GoTo TestComplete

            '            If TestStat1 <> 100 Then
            '                FormatResultLine(sTNum & " LaRRS Polarizer Motor jog", False)
            '                Echo("  Exp 100   Meas " & CStr(Truncate(TestStat1)))
            '                RegisterFailure(VEO2, sTNum, Truncate(TestStat1), "", Truncate(100), Truncate(100), sComment:=" LaRRS Polarizer Motor jog Test FAILED.")
            '                TestVEO = FAILED
            '                IncStepFailed()
            '                If OptionFaultMode = SOFmode Then GoTo TestComplete
            '            Else
            '                FormatResultLine(sTNum & " LaRRS Polarizer Motor jog", True)
            '                IncStepPassed()
            '            End If
            '            If AbortTest = True Then GoTo TestComplete
            '            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then
            '                GoTo VEO2_8_6
            '            End If
            '            frmSTest.proProgress.Value = 90
            '            LoopStepNo += 1

            '            '4.2.4 Target Temperature
            '            'This procedure verifies that the target temperature sensor is functioning and within
            '            'its acceptable limits at room ambient temperature.
            '            '
            '            Echo(vbCrLf & "Test Target Temperature")
            '            SET_SYSTEM_CONFIGURATION_INITIATE(2)
            '            Delay(1)


VEO2_9_1:
            'Query the temperature sensor and validate it is within range.
            sTNum = "VEO-09-001"

            GET_TEMP_TARGET_IR_INITIATE()
            System.Threading.Thread.Sleep(1000)
            For i = 1 To 5
                System.Threading.Thread.Sleep(1000)
                GET_TEMP_TARGET_IR_FETCH(Ambient_temperature)
                If Ambient_temperature <> 0 Then Exit For
            Next i

            If Ambient_temperature < -20 Or Ambient_temperature > 70 Then
                TestVEO = FAILED
                FormatResultLine(sTNum & " EO Target Temperature Test", False)
                Echo("  Min -20    Max 70   Meas " & EngNotate(Truncate(Ambient_temperature), 2) & " degrees C")
                RegisterFailure(VEO2, sTNum, Truncate(Ambient_temperature), "deg C", Truncate(-20), Truncate(70), sComment:=" EO Target Temperature Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            Else
                FormatResultLine(sTNum & " EO Target Temperature Test", True)
                Echo("  Min -20   Max 70   Meas " & EngNotate(Truncate(Ambient_temperature), 2) & " degrees C")
                IncStepPassed()
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 41
                GoTo VEO2_9_1
            End If
            frmSTest.proProgress.Value = 92
            LoopStepNo += 1

            '4.2.5 Blackbody Temperature
            'This procedure checks to see that the Blackbody responds to a set point command.
            '
            Echo(vbCrLf & "Test Blackbody Temperature")

            SET_SYSTEM_CONFIGURATION_INITIATE(2)
            Delay(1)

VEO2_10_1:
            'Read ambient temperature from Target Wheel temperature probe.
            'Command the Blackbody to -1 degree C (TBR) below ambient.
            'Verify that the blackbody reaches the temperature.

            GET_TEMP_TARGET_IR_INITIATE()
            For i = 1 To 5
                System.Threading.Thread.Sleep(1000)
                GET_TEMP_TARGET_IR_FETCH(Ambient_temperature)
                If Ambient_temperature <> 0 Then Exit For
            Next i
            System.Threading.Thread.Sleep(1000)
            'Command the Blackbody to -1 degreeC (TBR) below ambient.
            'Verify that the Blackbody reaches the temperature.
            sTNum = "VEO-10-001"
            SET_TEMP_ABSOLUTE_IR_INITIATE(Ambient_temperature - 1)
            System.Threading.Thread.Sleep(1000)

            iTry = 0 'Loop until it gets there or for 120 seconds
            Do
                SET_TEMP_ABSOLUTE_IR_FETCH(BlackBody_temperature)
                If BlackBody_temperature = 0 Then
                    SET_TEMP_ABSOLUTE_IR_FETCH(BlackBody_temperature)
                End If
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 120 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop Until BlackBody_temperature < (Ambient_temperature - 0.7)
            If AbortTest = True Then GoTo TestComplete

            If (BlackBody_temperature < (Ambient_temperature - 0.5)) Then
                FormatResultLine(sTNum & " EO Blackbody Temperature Drop Test", True)
                Echo("  Max " & EngNotate(Truncate(Ambient_temperature - 0.5), 2) & "   Meas " & EngNotate(Truncate(BlackBody_temperature), 2) & " degrees C")
                Echo("  Blackbody Fall Time = " & CStr(iTry) & " seconds")
                IncStepPassed()
            Else
                TestVEO = FAILED
                FormatResultLine(sTNum & " EO Blackbody Temperature Drop Test", False)
                Echo("  Max " & EngNotate(Truncate(Ambient_temperature - 0.5), 2) & "   Meas " & EngNotate(Truncate(BlackBody_temperature), 2) & " degrees C")
                RegisterFailure(VEO2, sTNum, Truncate(BlackBody_temperature), "deg C", , Truncate(Ambient_temperature - 0.5), sComment:=" EO Blackbody Temperaure Drop Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then '42
                GoTo VEO2_10_1
            End If
            frmSTest.proProgress.Value = 94
            LoopStepNo += 1

VEO2_10_2:
            'Command the Blackbody to +1 degreeC (TBR) above ambient.
            'Verify that the Blackbody reaches the temperature.
            sTNum = "VEO-10-002"
            SET_TEMP_ABSOLUTE_IR_INITIATE(Ambient_temperature + 1)
            System.Threading.Thread.Sleep(1000) ' was delay 30

            iTry = 0 'Loop until it gets there or for 120 seconds
            Do
                SET_TEMP_ABSOLUTE_IR_FETCH(BlackBody_temperature)
                If BlackBody_temperature = 0 Then ' ignore zeros
                    SET_TEMP_ABSOLUTE_IR_FETCH(BlackBody_temperature)
                End If
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 120 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop Until BlackBody_temperature > Ambient_temperature + 0.7
            If AbortTest = True Then GoTo TestComplete

            If (BlackBody_temperature > Ambient_temperature + 0.5) Then
                FormatResultLine(sTNum & " EO Blackbody Temperature Rise Test", True)
                Echo("  Min " & EngNotate(Truncate(Ambient_temperature + 0.5), 2) & "   Meas " & EngNotate(Truncate(BlackBody_temperature), 2) & " degrees C")
                Echo("  Blackbody Rise Time = " & CStr(iTry) & " seconds")
                IncStepPassed()
            Else
                TestVEO = FAILED
                FormatResultLine(sTNum & " EO Blackbody Temperature Rise Test", False)
                Echo("  Min " & EngNotate(Truncate(Ambient_temperature + 0.5), 2) & "   Meas " & EngNotate(Truncate(BlackBody_temperature), 2) & " degrees C")
                RegisterFailure(VEO2, sTNum, Truncate(BlackBody_temperature), "deg C", Truncate(Ambient_temperature + 0.5), , sComment:=" EO Blackbody Temperaure Rise Test FAILED.")
                IncStepFailed()
                If OptionFaultMode = SOFmode Then GoTo TestComplete
            End If
            If AbortTest = True Then GoTo TestComplete
            If OptionMode = LOSmode And OptionTestName = "VEO" And OptionStep = LoopStepNo Then ' 43
                GoTo VEO2_10_2
            End If
            frmSTest.proProgress.Value = 96
            LoopStepNo += 1

        Catch   ' VEO2errorHandler:
            If (Err.Number <> 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description, MsgBoxStyle.Exclamation)
                Err.Clear()
            End If
            If TestVEO = PASSED Then
                TestVEO = UNTESTED
            End If
            '? On Error Resume Next  ' in case there is another error in TestComplete routine
            GoTo TestComplete

        End Try


TestComplete:
        frmSTest.proProgress.Value = 100
        Reset() ' close any open files
        If AbortTest = True Then
            sMsg = vbCrLf
            sMsg &= "  ******************************************************** *" & vbCrLf
            sMsg &= "  *                  VEO2 Module test aborted!             *" & vbCrLf
            sMsg &= "  ******************************************************** *" & vbCrLf
            Echo(sMsg)
            If TestVEO = PASSED Then
                TestVEO = UNTESTED
            End If
        End If

        'Turn camera off and stow if power is still on
        If VEO2PowerOn = True Then
            SET_CAMERA_POWER_INITIATE(0)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : TestStat1 = 2
            Do While 0 <> TestStat1 'Loop until it gets there or for 20 seconds
                SET_CAMERA_POWER_FETCH(TestStat1)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Echo("Camera did not get to status = 0") 'BB
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop

            sMsg = "**** Sending stages to STOW ****"
            Echo(sMsg)
            'Send stages to home position so the stow pins can be installed if required.
            SET_SENSOR_STAGE_LASER_INITIATE(3)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 3 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SENSOR_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop

            SET_SOURCE_STAGE_LASER_INITIATE(1)
            System.Threading.Thread.Sleep(1000) : iTry = 1 : Sensor_Position = -1
            Do While 1 <> Sensor_Position 'Loop until it gets there or for 20 seconds
                SET_SOURCE_STAGE_LASER_FETCH(Sensor_Position)
                System.Threading.Thread.Sleep(1000)
                iTry += 1
                If iTry > 20 Then
                    Exit Do
                End If
                Application.DoEvents()
                If AbortTest = True Then Exit Do
            Loop
            IRWIN_SHUTDOWN()
            SetVEOPower(False)

            If iTry < 20 Then
                sMsg = "ATS VEO2 Module and Power Supplies have been reset." & vbCrLf & vbCrLf
                sMsg &= "Verify that the corner cube assembly is in its STOW (full counter clockwise) position." & vbCrLf & vbCrLf
                sMsg &= "The VEO2 system is now in its STOW (ready for storage) position. " & vbCrLf & vbCrLf
                sMsg &= "If the VEO2 is going to be put back into its transit cases, " & vbCrLf
                sMsg &= " then reinstall the Reg Tag ""Remove Before Use"" pins." & vbCrLf
            Else
                sMsg = "Sending stages to STOW failed." & vbCrLf & vbCrLf
                sMsg &= "ATS VEO2 Module and Power Supplies have been reset." & vbCrLf & vbCrLf
                sMsg &= "The VEO2 system is ----NOT---- in its STOW (NOT ready for storage) position. " & vbCrLf & vbCrLf
                sMsg &= "Try running the test again.  After powering up the VEO2, you should be able" & vbCrLf
                sMsg &= " to abort the test without getting this message." & vbCrLf
            End If
            MsgBox(sMsg)
        Else
            SetVEOPower(False)
        End If

        S = "Remove the VEO2 power cable W300 (93006G7300) from the ATS PDU J7 connector."
        DisplaySetup(S, "ST-VEO2-PDU.jpg", 1)
        Application.DoEvents()
        frmSTest.cmdAbort.Text = "Abort Test"
        frmSTest.cmdPause.Text = "Pause Test"

        If AbortTest = True Then
            If TestVEO = FAILED Then
                ReportFailure(VEO2)
            Else
                ReportUnknown(VEO2)
                TestVEO = -99
            End If
        ElseIf TestVEO = PASSED Then
            ReportPass(VEO2)
        ElseIf TestVEO = FAILED Then
            ReportFailure(VEO2)
        Else
            ReportUnknown(VEO2)
        End If
        If CloseProgram = True Then
            EndProgram()
        End If
        Exit Function

    End Function


End Module