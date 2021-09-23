'Option Strict Off
'Option Explicit On

Imports System

Public Module SystemCommunication


	'=========================================================
    '**************************************************************
    '**************************************************************
    '** ManTech Test Systems Software Module                     **
    '**                                                          **
    '** Nomenclature  : Chassis Environmental Monitor And Control**
    '**                 (CEMAC) AKA System Monitor Communication **
    '** Version       : 1.0a                                     **
    '** Written By    : David W. Hartley                         **
    '** Last Update   : 02/15/97                                 **
    '** Purpose       : This module contains communication       **
    '**                 routines for the system monitor Hardware **
    '**************************************************************
    '**************************************************************
    Public DebugDwh As Short
    Dim ControllerHandle As Integer
    Dim VxiMxiHandle As Integer
    'Public RetValue As Integer
    Const SDA_LINE As Short = 7
    Const SCK_LINE As Short = 6
    Const PRIMARY_CHASSIS_ADDRESS As Integer = &H10
    Const SECONDARY_CHASSIS_ADDRESS As Integer = &H12
    Const DALLAS_TEMP_SENSOR_ADDRESS As Integer = &H90



    Sub InitI2C()

        Dim N As Short
        '============================================================================
        'Begin I2C driver in Pseudocode
        '     SDA = 1
        '     SCK = 0
        '     For n = 0 To 3
        '       CALL STOP
        '     Next n
        'End I2C driver in Pseudocode
        '============================================================================
        SendData(SDA_LINE, 1)
        SendData(SCK_LINE, 0)
        For N = 0 To 3
            StopI2C()
        Next N

    End Sub



    Sub SysCommMain()

        'Init Sessions
        SetupCommunication()
        InitI2C()


        'Close Sessions
        ClearCommunication()

    End Sub


    Sub ReadI2C(ByVal Device_address As Object, ByVal Number_of_bytes As Object)

        '============================================================================
        'Begin I2C driver in Pseudocode
        'Device_adress=Device_adress OR (0000.0001)b / This sets the READ FLAG /
        'Call Start
        'Call PUTBYTE(Device_adress)
        'Call GETACK
        'FOR x= 0 to Number_of_bytes)
        'Call GETBYTE
        'DATA(x)=BUFFER / Copy the received BYTE in the DATA array /
        'IF X< Number_of_bytes THEN  / Prevent giving an ack on the last   /
        'CALL GIVEACK             / byte read out of the chip.Otherways /
        'END IF                      / there is risk of bus hang-up        /
        'Next X
        'CALL STOP
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub



    
    Sub WriteI2C(ByRef DeviceAddress As Integer, ByVal NumberOfBytes As Short)

        Dim X As Short
        '============================================================================
        'Begin I2C driver in Pseudocode
        'Device_adress=Device_adress AND (1111.1110)b / This clears READ flag /
        'Call Start
        'Call PUTBYTE(Device_adress)
        'Call GETACK
        'For X = 0 To Number_of_bytes
        '    Call PUTBYTE(Data(X))
        '    Call GETACK
        'Next X
        'CALL STOP
        'End I2C driver in Pseudocode
        '============================================================================

        '(1111.1110)b = &HFE& / This clears READ Bit /
        DeviceAddress = DeviceAddress And &HFE
        StartI2C() 'Call Start
        'Call PUTBYTE(Device_adress)
        'Call GETACK
        'For X = 0 To NumberOfBytes%
        '    Call PUTBYTE(Data(X))
        '    Call GETACK
        'Next X
        'CALL STOP

    End Sub


    Sub RandomReadI2C(ByVal Device_adress As Object, ByVal Start_adress As Object, ByVal Number_of_bytes As Object)

        '============================================================================
        'Begin I2C driver in Pseudocode
        'Device_adress=Device_adress AND (1111.1110)b / This clears READ flag /
        'Call Start
        'Call PUTBYTE(Device_adress)
        'Call GETACK
        'Call PUTBYTE(Start_adress)
        'Call GETACK
        'Call Start
        'Device_adress=Device_adress OR (0000.0001)b / This sets the READ FLAG /
        'Call PUTBYTE(Device_adress)
        'Call GETACK
        'For X = 0 To Number_of_bytes
        '    Call GETBYTE
        '    Data(X) = Buffer
        '    Call GIVEACK
        'Next X
        'CALL STOP
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub



    Sub RandomWrite(ByVal Device_adress As Object, ByVal Start_adress As Object, ByVal Number_of_bytes As Object)
        '============================================================================
        'Begin I2C driver in Pseudocode
        'Device_adress=Device_adress AND (1111.1110)b / This clears READ flag /
        'Call Start
        'Call PUTBYTE(Device_adress)
        'Call GETACK
        'Call PUTBYTE(Start_adress)
        'Call GETACK
        'For X = 0 To Number_of_bytes
        '    Call PUTBYTE(Data(X))
        '    Call GETACK
        'Next X
        'CALL STOP
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub

    Sub StartI2C()

        '============================================================================
        'Begin I2C driver in Pseudocode
        '     SCK=1 / BUGFIX !/
        '     SDA=1 / Improvement /
        '     SDA = 0
        '     SCK = 0
        '     SDA = 1
        'End I2C driver in Pseudocode
        '============================================================================

        SendData(SCK_LINE, 1)
        SendData(SDA_LINE, 1)
        SendData(SDA_LINE, 0)
        SendData(SCK_LINE, 0)
        SendData(SDA_LINE, 1)
    End Sub


    Sub StopI2C()

        '============================================================================
        'Begin I2C driver in Pseudocode
        '     SDA = 0
        '     SCK = 1
        '     SDA = 1
        'End I2C driver in Pseudocode
        '============================================================================

        SendData(SDA_LINE, 0)
        SendData(SCK_LINE, 1)
        SendData(SDA_LINE, 1)
    End Sub

    Sub PutByteI2C(ByVal Buffer As String)
        '============================================================================
        'Begin I2C driver in Pseudocode
        'For N = 7 To 0
        '   SDA= BIT(n) of BUFFER
        '   SCK = 1
        '   SCK = 0
        'Next N
        'SDA = 1
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub

    Sub GetByteI2C()
        '============================================================================
        'Begin I2C driver in Pseudocode
        'For n = 7 To 0
        '    SCK = 1
        '    BIT(n) OF BUFFER = SDA
        '    SCK = 0
        'Next n
        'SDA = 1
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub

    Sub GiveAckI2C()

        '============================================================================
        'Begin I2C driver in Pseudocode
        'SDA = 0
        'SCK = 1
        'SCK = 0
        'SDA = 1
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub

    Sub GetAckI2C()

        '============================================================================
        'Begin I2C driver in Pseudocode
        'SDA = 1
        'SCK = 1
        'WAITFOR SDA = 0
        'SCK = 0
        'End I2C driver in Pseudocode
        '============================================================================

    End Sub

    Sub SetupCommunication()

        Dim resourceName As String

        RetValue = viOpenDefaultRM(RmSession)
        resourceName = "VXI::0::INSTR"
        RetValue = viOpen(RmSession, resourceName, VI_NULL, VI_NULL, ControllerHandle)
        resourceName = "VXI::1::INSTR"
        RetValue = viOpen(RmSession, resourceName, VI_NULL, VI_NULL, VxiMxiHandle)

    End Sub

    Sub ClearCommunication()
        RetValue = viClose(ControllerHandle)
        RetValue = viClose(VxiMxiHandle)
        RetValue = viClose(RmSession)
    End Sub

    Sub SendData(ByVal Line As Short, ByVal State As Short)

        Dim SendDataTo As Integer

        If ObjectToDouble(Line) = 6 Then
            SendDataTo = VI_TRIG_TTL6
        Else
            SendDataTo = VI_TRIG_TTL7
        End If
        If ObjectToDouble(State) = 1 Then
            SendDataTo = VI_TRIG_PROT_ON
        Else
            SendDataTo = VI_TRIG_PROT_OFF
        End If
        RetValue = viSetAttribute(ControllerHandle, VI_ATTR_TRIG_ID, SendDataTo)
        RetValue = viAssertTrigger(ControllerHandle, SendDataTo)
    End Sub

    Function DetectEvent() As Short

        Dim outEventType As Integer
        Dim outEventContext As Integer
        Dim StatID As Integer
        Dim TriggerLine As Short
        'Detect TTLT6, TTLT7, *ACFAIL
        RetValue = viEnableEvent(VxiMxiHandle, VI_EVENT_TRIG, VI_QUEUE, VI_NULL)
        'RetValue& = viWaitOnEvent(ByVal VxiMxiHandle&, ByVal VI_ALL_ENABLED_EVENTS, ByVal 20000, outEventType&, outEventContext&)
        RetValue = viWaitOnEvent(VxiMxiHandle, VI_EVENT_TRIG, 20000, outEventType, outEventContext)

        If RetValue <> 0 Then
            MsgBox("Timeout!")
        Else

            RetValue = viGetAttribute(outEventContext, VI_ATTR_RECV_TRIG_ID, StatID)
            If RetValue = 0 Then
                MsgBox("Trigger event Recieved event from: " + Str(StatID))
            End If
        End If
        Select Case StatID
            Case 6
                TriggerLine = 6
            Case 7
                TriggerLine = 7

            Case Else
                TriggerLine = 0
        End Select
        RetValue = viClose(outEventContext)
        RetValue = viDisableEvent(VxiMxiHandle, VI_EVENT_TRIG, VI_QUEUE)

        DetectEvent = TriggerLine
    End Function









End Module