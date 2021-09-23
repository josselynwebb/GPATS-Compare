'Option Strict Off
Option Explicit On

Imports System

'This module contains all of the definitions needed for SysMon functions
'Only the new option to report temperature when a module fails in the primary/secondary chassis needs this module.
Public Module sysmonitor

    Public DisPrimary As Integer
    Public DisSecondary As Integer
    Public ChassisSelected As Integer

    Public RmSession As Integer
    Public GpibControllerHandle0 As Integer
    Public GpibControllerHandle11 As Integer
    Public ChassisControllerHandle1 As Integer
    Public ChassisControllerHandle2 As Integer
    Public GpibControllerHandle14 As Integer
    Public GpibControllerHandle15 As Integer
    Public PpuControllerHandle(10) As Integer
    Public Addr As Integer
    Public RetValue As Integer
    Public ActChassisAddress As Integer
    Public ActVolt28Ok As Integer
    Public ActProbeEvent As Integer
    Public ActReceiverEvent As Integer
    Public ResourceError As Integer

    Public Const HEATER_RES As Single = 3.145 'The Resistance in Ohms of 1  Heating Unit
    'Assuming that the (Worst Case) Heater Resistance is 3.145 Ohms and The Max Voltage is
    '28 VOlts During Single/Three Phase AC Imput Power Mode and 17-33V in DC Mode
    'therefore the power dissipated by the heating units will be (V*V/R)
    'this gives us about 249 to 346 Watts

    Public Const POWER_MARGIN As Short = 10 'The Power Safety Margin Threshold Constant in Watts
    Public Const FPU_INPUT_POWER_EFFICENCY As Single = 0.7 'Input Power Efficency Factor in decimal
    Public Const PPU_INPUT_POWER_EFFICENCY As Single = 0.8 'Input Power Efficency Factor in decimal
    Public Const FAN_SPEED_POWER As Single = 0.75 'The Power in Watts for each fan speed percentage amount
    Public Const FAN_PDU_MINIMUM_DRAW As Short = 58 'The Power In Watts that fans/IC/Misc draw when active
    Public Const PPU_PRELOAD_CURRENT As Single = 0.025 'Current Per Volt consumed by an unloaded PPU supply
    Public Const IC_POWER As Short = 60 'Instrument Controller Power Disspiation in Watts
    Public Const SE_PRIVILEGE_ENABLED = &H2
    Public Const SE_SHUTDOWN_NAME = "SeShutdownPrivilege"
    Public Const SE_DEBUG_NAME = "SeDebugPrivilege"


    ''  There are 111 bytes of data transferred by a read to IEEE 5:14 (default IEEE address of 5) that contain a mix of data from the two VXI chassis and the PDU.
    ''  It requrires two reads of 111 bytes each to gather all information from both chassis. 
    ''  The data is updated from the two VXI chassis every two seconds.  Chassis data return strings alternate between the Primary and Secondary Chassis. 
    ''  The VXI chassis is identified in the Action Byte.
    ''  A total of 10 new bytes have been added to provide additional information


    ''Byte	Data Item	Source	Scale Factor	Notes
    ''1	Action Byte	VXI		
    ''2	    Slot Current 1	PDU	I = Value * 0.032 ADC	Repeats for all
    ''3	    Slot Polarity 1	PDU	MSB=0 for normal	Repeats for all
    ''4	    Slot Voltage 1	PDU	V = (LSN Byte 3 * 256 + Byte 4) * 0.01 VDC	Repeats for all except Slot 10
    ''5	    Slot Current 2	PDU		
    ''6	    Slot Polarity 2	PDU		
    ''7	    Slot Voltage 2	PDU		
    ''8   	Slot Current 3	PDU		
    ''9	    Slot Polarity 3	PDU		
    ''10	Slot Voltage 3	PDU		
    ''11	Slot Current 4	PDU		
    ''12	Slot Polarity 4	PDU		
    ''13	Slot Voltage 4	PDU		
    ''14	Slot Current 5	PDU		
    ''15	Slot Polarity 5	PDU		
    ''16	Slot Voltage 5	PDU		
    ''17	Slot Current 6	PDU		
    ''18	Slot Polarity 6	PDU		
    ''19	Slot Voltage 6	PDU		
    ''20	Slot Current 7	PDU		
    ''21	Slot Polarity 7	PDU		
    ''22	Slot Voltage 7	PDU		
    ''23	Slot Current 8	PDU		
    ''24	Slot Polarity 8	PDU		
    ''25	Slot Voltage 8	PDU		
    ''26	Slot Current 9	PDU		
    ''27	Slot Polarity 9	PDU		
    ''28	Slot Voltage 9	PDU		
    ''29	Slot Current 10	 PDU		
    ''30	Slot Polarity 10 PDU		
    ''31	Slot Voltage 10	 PDU	V = (LSN Byte 30 * 256 + Byte 31) * 0.02 VDC	

    ''32	ICPU Status	PDU		
    ''33	P5A Current	PDU	Byte 33 * 0.32 ADC	VXI I
    ''34	P5B Current	PDU	Byte 34 * 0.16 ADC	VXI II
    ''35	P12A Current	PDU	Byte 34 * 0.16 ADC	VXI II
    ''36	P24A Current	PDU	Byte 34 * 0.16 ADC	VXI II
    ''37	N2A Current	PDU	Byte 34 * 0.16 ADC	
    ''38	N52A Current	PDU	Byte 34 * 0.16 ADC	VXI II
    ''39	N24A Current	PDU	Byte 34 * 0.16 ADC	VXI II
    ''40	N12A Current	PDU	Byte 34 * 0.16 ADC	VXI II
    ''41	N2B Current	PDU	Byte 34 * 0.16 ADC	
    ''42	P5A Voltage	PDU		VXI I
    ''43	P5A Voltage	PDU	V = (LSN Byte 42 * 256 + Byte 43) * 0.01 VDC	
    ''44	P5B Voltage	PDU		VXI II
    ''45	P5B Voltage	PDU	All voltages repeat P5A scale	
    ''46	P12A Voltage	PDU		VXI I
    ''47	P12A Voltage	PDU		
    ''48	P24A Voltage	PDU		VXI I
    ''49	P24A Voltage	PDU		
    ''50	N2A Voltage	PDU		VXI I
    ''51	N2A Voltage	PDU		
    ''52	N52A Voltage	PDU		VXI I
    ''53	N52A Voltage	PDU		
    ''54	N24A Voltage	PDU		VXI I
    ''55	N24A Voltage	PDU		
    ''56	N12A Voltage	PDU		VXI I
    ''57	N12A Voltage	PDU		
    ''58	N2B Voltage	PDU		VXI II
    ''59	N2B Voltage	PDU		
    ''60	Action Byte	VXI		
    ''61	Chassis Status	PDU		
    ''62	Fan Speed	VXI	

    ''63	Temperature 0	VXI	(VXI - from primary or secondary depending on action byte)	
    ''64	Temperature 1	VXI		
    ''65	Temperature 2	VXI		
    ''66	Temperature 3	VXI		
    ''67	Temperature 4	VXI		
    ''68	Temperature 5	VXI		
    ''69	Temperature 6	VXI		
    ''70	Temperature 7	VXI		
    ''71	Temperature 8	VXI		
    ''72	Temperature 9	VXI		
    ''73	Temperature 10	VXI		
    ''74	Temperature 11	VXI		
    ''75	Temperature 12	VXI		

    ''76	Intake Temperature	VXI	

    ''77	ChP24V	VXI	  (I or II)	
    ''78	ChP24V	VXI		
    ''79	ChP12V	VXI		
    ''80	ChP12V	VXI		
    ''81	ChP5V	VXI		
    ''82	ChP5V	VXI		
    ''83	ChN2V	VXI		
    ''84	ChN2V	VXI		
    ''85	ChN52V	VXI		
    ''86	ChN52V	VXI		
    ''87	ChN12V	VXI		
    ''88	ChN12V	VXI		
    ''89	ChN24V	VXI		
    ''90	ChN24V	VXI		
    ''91	Vin Single Phase	PDU	V = Byte 91 * 0.598 VAC	
    ''92	Vin Three Phase	PDU	V = (1.288 – ((Byte 92 – 103) * 0.00838) * Byte 92 VAC	
    ''93	Current In	PDU	I = Byte 93 / 4.65 AAC	
    ''94	Power OK	PDU		
    ''95	DCV Level	PDU	3949 / Byte 95 VDC	
    ''96	Status Input Power	PDU	

    ''97	P12B Current 	PDU		VXI II
    ''98	P24B Current	PDU		VXI II
    ''99	N52B Current	PDU		VXI II
    ''100	N24B Current	PDU		VXI II
    ''101	N12B Current	PDU		VXI II
    ''102	P12B Voltage	PDU		VXI II
    ''103	P12B Voltage	PDU		
    ''104	P24B Voltage	PDU		VXI II
    ''105	P24B Voltage	PDU		
    ''106	N52B Voltage	PDU		VXI II
    ''107	N52B Voltage	PDU		
    ''108	N24B Voltage	PDU		VXI II
    ''109	N24B Voltage	PDU		
    ''110	N12B Voltage	PDU		VXI II
    ''111	N12B Voltage	PDU		



    Public Structure ChassisData
        Public Action1 As Integer ' 1
        ' Public SlotCurrent(10) As Single
        ' Public SlotVoltage(10) As Single
        ' Public SlotPolarity(10) As Boolean ' Normal=True, Reversed=False
        Public SlotCurrent1 As Single   ' 2 current,      I = Value * 0.32 Adc
        Public SlotPolarity1 As Boolean ' 3 Polarity,     MSBit=0 Normal, 1 for Reverse
        Public SlotVoltage1 As Single   ' 3,4 voltage,    V = (LSN Byte 3 *256 + Byte 4) * 0.01 Vdc
        Public SlotCurrent2 As Single   ' 5,6,7
        Public SlotPolarity2 As Boolean
        Public SlotVoltage2 As Single
        Public SlotCurrent3 As Single   ' 8,9,10
        Public SlotPolarity3 As Boolean
        Public SlotVoltage3 As Single
        Public SlotCurrent4 As Single   '11,12,13
        Public SlotPolarity4 As Boolean
        Public SlotVoltage4 As Single
        Public SlotCurrent5 As Single   '14,15,16
        Public SlotPolarity5 As Boolean
        Public SlotVoltage5 As Single
        Public SlotCurrent6 As Single   '17,18,19
        Public SlotPolarity6 As Boolean
        Public SlotVoltage6 As Single
        Public SlotCurrent7 As Single   '20,21,22
        Public SlotPolarity7 As Boolean
        Public SlotVoltage7 As Single
        Public SlotCurrent8 As Single   '23,24,25
        Public SlotPolarity8 As Boolean
        Public SlotVoltage8 As Single
        Public SlotCurrent9 As Single   '26,27,28
        Public SlotPolarity9 As Boolean
        Public SlotVoltage9 As Single
        Public SlotCurrent10 As Single  '29,30,31
        Public SlotPolarity10 As Boolean
        Public SlotVoltage10 As Single   ' V = (LSN Byte 30 *256 + Byte 31) * 0.02 Vdc 

        Public StatusICPU As Integer '32

        Public P5ACurrent As Single     '33  I = Byte 33 * 0.32 Adc for VXI I
        Public P5BCurrent As Single     '34  I = Byte 34 * 0.32 Adc for VXI II
        Public P12ACurrent As Single    '35  I = Byte 35 * 0.32 Adc for VXI II
        Public P24ACurrent As Single    '36  I = Byte 36 * 0.32 Adc for VXI II
        Public N2ACurrent As Single     '37  I = Byte 37 * 0.32 Adc  
        Public N52ACurrent As Single    '38  I = Byte 38 * 0.32 Adc for VXI II
        Public N24ACurrent As Single    '39  I = Byte 39 * 0.32 Adc for VXI II
        Public N12ACurrent As Single    '40  I = Byte 40 * 0.32 Adc for VXI II
        Public N2BCurrent As Single     '41  I = Byte 41 * 0.32 Adc 

        Public P5AVoltage As Single  '42,43  V = (LSN Byte 42 * 256 + Byte 43) * 0.01 Vdc for VXI 1
        Public P5BVoltage As Single  '44,45  V = (LSN Byte 44 * 256 + Byte 45) * 0.01 Vdc for VXI 1I
        Public P12AVoltage As Single '46,47  V = (LSN Byte 46 * 256 + Byte 47) * 0.01 Vdc for VXI 1
        Public P24AVoltage As Single '48,49  V = (LSN Byte 48 * 256 + Byte 49) * 0.01 Vdc for VXI 1
        Public N2AVoltage As Single  '50,51  V = (LSN Byte 50 * 256 + Byte 51) * 0.01 Vdc for VXI 1
        Public N52AVoltage As Single '52,53  V = (LSN Byte 52 * 256 + Byte 53) * 0.01 Vdc for VXI 1
        Public N24AVoltage As Single '54,55  V = (LSN Byte 54 * 256 + Byte 55) * 0.01 Vdc for VXI 1
        Public N12AVoltage As Single '56,57  V = (LSN Byte 56 * 256 + Byte 57) * 0.01 Vdc for VXI 1
        Public N2BVoltage As Single  '58,59  V = (LSN Byte 58 * 256 + Byte 59) * 0.01 Vdc for VXI 1I
        Public Action2 As Integer ' 60
        Public ChassisStatus As Integer '61
        Public FanSpeed As Integer '62

        ' Public Temperature(12) As Single
        Public Temperature0 As Single ' 63 to 75
        Public Temperature1 As Single
        Public Temperature2 As Single
        Public Temperature3 As Single
        Public Temperature4 As Single
        Public Temperature5 As Single
        Public Temperature6 As Single
        Public Temperature7 As Single
        Public Temperature8 As Single
        Public Temperature9 As Single
        Public Temperature10 As Single
        Public Temperature11 As Single
        Public Temperature12 As Single

        Public IntakeTemperature As Single ' 76
        Public ChP24V As Single          '77,78
        Public ChP12V As Single          '79,80
        Public ChP5V As Single           '81,82
        Public ChN2V As Single           '83,84
        Public ChN52V As Single          '85,86
        Public ChN12V As Single          '87,88
        Public ChN24V As Single          '89,90
        Public VinSinglePhase As Single  '91     PDU  V = Byte 91 * 0.598 Vac
        Public VinThreePhase As Single   '92     PDU  V = (1.288 - ((Byte 92 - 103) * 0.00838) * Byte 92 Vac
        Public CurrentIn As Single       '93     PDU  I = Byte 93 / 4.65 Aac
        Public PowerOk As Integer        '94     PDU
        Public DCVlevel As Single        '95     PDU  V = 3949/(Byte 95) Vdc

        Public StatusInputPower As Integer ' 96  PDU

        'Freedom Supply new stuff
        Public P12BCurrent As Single     '97    I =  Byte 97 * 0.32 Adc for VXI II
        Public P24BCurrent As Single     '98    I =  Byte 98 * 0.32 Adc for VXI II
        Public N52BCurrent As Single     '99    I =  Byte 99 * 0.32 Adc for VXI II
        Public N24BCurrent As Single    '100    I = Byte 100 * 0.32 Adc for VXI II
        Public N12BCurrent As Single    '101    I = Byte 101 * 0.32 Adc for VXI II

        'new voltages
        Public P12BVoltage As Single     '102,103   V = (LSN Byte 102 * 256 + Byte 103) * 0.01 Vdc for VXI II 
        Public P24BVoltage As Single     '104,105   V = (LSN Byte 104 * 256 + Byte 105) * 0.01 Vdc for VXI II
        Public N52BVoltage As Single     '106,107   V = (LSN Byte 106 * 256 + Byte 107) * 0.01 Vdc for VXI II
        Public N24BVoltage As Single     '108,109   V = (LSN Byte 108 * 256 + Byte 109) * 0.01 Vdc for VXI II
        Public N12BVoltage As Single     '110,111   V = (LSN Byte 110 * 256 + Byte 111) * 0.01 Vdc for VXI II

        'Calculated variables not passed from PPU
        Public UutPowerTotal As Single
        Public FpuPowerTotal As Single
        Public TempRisePerSlot0 As Single
        Public TempRisePerSlot1 As Single
        Public TempRisePerSlot2 As Single
        Public TempRisePerSlot3 As Single
        Public TempRisePerSlot4 As Single
        Public TempRisePerSlot5 As Single
        Public TempRisePerSlot6 As Single
        Public TempRisePerSlot7 As Single
        Public TempRisePerSlot8 As Single
        Public TempRisePerSlot9 As Single
        Public TempRisePerSlot10 As Single
        Public TempRisePerSlot11 As Single
        Public TempRisePerSlot12 As Single

    End Structure

    Public PrimaryChassis As ChassisData
    Public SecondaryChassis As ChassisData

    Public InputPower As Integer              'Power Budget Variable
    Public HeaterPower As Integer             'Power Budget Variable
    Public FanPower As Integer                'Power Budget Variable
    Public UserPowerConsumption As Integer    'Power Budget Variable
    Public TotalPowerUsage As Short           'Power Budget Variable
    Public DcInputCurrent As Integer          'Power Budget Variable
    Public StatHeater2 As Integer
    Public StatHeater1 As Integer

    Public FpuStatModFault As Integer          'FPU Status Byte Variable
    Public FpuStatOn As Integer                'FPU Status Byte Variable
    Public FpuStatOffMismnatch As Integer      'FPU Status Byte Variable
    Public FpuStatOverVolt As Integer          'FPU Status Byte Variable
    Public FpuStatAddress As Integer           'FPU Status Byte Variable
    Public FactoryDef(0 To 25) As String       'Factory Default Cooling Curve Thresholds
    Public UserDef(0 To 25) As String          'Factory Default Cooling Curve Thresholds
    Public StatSelfTest As Integer
    Public StatAirFlowSensor3 As Integer
    Public StatAirFlowSensor2 As Integer
    Public StatAirFlowSensor1 As Integer
    Public PowerStatus28FailHigh As Integer    'POWER Status Byte Variable
    Public PowerStatusAc As Integer            'POWER Status Byte Variable AC MODE
    Public PowerStatusDc As Integer            'POWER Status Byte Variable DC MODE
    Public PowerStatusSingle As Integer        'POWER Status Byte Variable
    Public PowerStatusPhase1 As Integer        'POWER Status Byte Variable
    Public PowerStatusPhase2 As Integer        'POWER Status Byte Variable
    Public PowerStatusPhase3 As Integer        'POWER Status Byte Variable
    Public PowerStatus2800Watt As Integer      'POWER Status Byte Variable
    Public PowerSwitch As Integer              'Latches The FPU "Power On" Command
    Public AllPowerOff As Integer              'Shows ALL FPU Voltages at 0 or NOT
    Public SysWaitTimeout As Long              'System Wait Dialog Timeout


    Function FormatSystemMonitorData(Data As String) As ChassisData
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*     This Module takes the SYSMON large data dump string  *
        '*     and formats the bytes into useable data and places   *
        '*     the data into a user defined type --> ChassisData    *
        '*                                                          *
        '*    EXAMPLE:                                              *
        '*     PrimaryChassis = FormatSystemMonitorData(Buffer)     *
        '*   SecondaryChassis = FormatSystemMonitorData(Buffer)     *
        '*                                                          *
        '*   where Buffer is 111 byte string                        *
        '*                                                          *
        '*    RETURNS:                                              *
        '*     Formatted Data as ChassisData User Defined Type      *
        '************************************************************
        Dim SlotIndex As Integer
        Dim Chassis As ChassisData
        Dim Heater As Integer

        'ActionByte
        Chassis.Action1 = Asc(Mid$(Data$, 1, 1))

        'Power Supply Current and Voltage
        Chassis.SlotCurrent1 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 2, 1)), 4)
        Chassis.SlotPolarity1 = FormatPolarity(Mid$(Data$, 3, 1))
        Chassis.SlotVoltage1 = ConvertBytesToWordAndScale(Mid$(Data$, 3, 1), Mid$(Data$, 4, 1), 3)

        Chassis.SlotCurrent2 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 5, 1)), 4)
        Chassis.SlotPolarity2 = FormatPolarity(Mid$(Data$, 6, 1))
        Chassis.SlotVoltage2 = ConvertBytesToWordAndScale(Mid$(Data$, 6, 1), Mid$(Data$, 7, 1), 3)
        Chassis.SlotCurrent3 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 8, 1)), 4)
        Chassis.SlotPolarity3 = FormatPolarity(Mid$(Data$, 9, 1))
        Chassis.SlotVoltage3 = ConvertBytesToWordAndScale(Mid$(Data$, 9, 1), Mid$(Data$, 10, 1), 3)
        Chassis.SlotCurrent4 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 11, 1)), 4)
        Chassis.SlotPolarity4 = FormatPolarity(Mid$(Data$, 12, 1))
        Chassis.SlotVoltage4 = ConvertBytesToWordAndScale(Mid$(Data$, 12, 1), Mid$(Data$, 13, 1), 3)
        Chassis.SlotCurrent5 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 14, 1)), 4)
        Chassis.SlotPolarity5 = FormatPolarity(Mid$(Data$, 15, 1))
        Chassis.SlotVoltage5 = ConvertBytesToWordAndScale(Mid$(Data$, 15, 1), Mid$(Data$, 16, 1), 3)
        Chassis.SlotCurrent6 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 17, 1)), 4)
        Chassis.SlotPolarity6 = FormatPolarity(Mid$(Data$, 18, 1))
        Chassis.SlotVoltage6 = ConvertBytesToWordAndScale(Mid$(Data$, 18, 1), Mid$(Data$, 19, 1), 3)
        Chassis.SlotCurrent7 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 20, 1)), 4)
        Chassis.SlotPolarity7 = FormatPolarity(Mid$(Data$, 21, 1))
        Chassis.SlotVoltage7 = ConvertBytesToWordAndScale(Mid$(Data$, 21, 1), Mid$(Data$, 22, 1), 3)
        Chassis.SlotCurrent8 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 23, 1)), 4)
        Chassis.SlotPolarity8 = FormatPolarity(Mid$(Data$, 24, 1))
        Chassis.SlotVoltage8 = ConvertBytesToWordAndScale(Mid$(Data$, 24, 1), Mid$(Data$, 25, 1), 3)
        Chassis.SlotCurrent9 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 26, 1)), 4)
        Chassis.SlotPolarity9 = FormatPolarity(Mid$(Data$, 27, 1))
        Chassis.SlotVoltage9 = ConvertBytesToWordAndScale(Mid$(Data$, 27, 1), Mid$(Data$, 28, 1), 3)
        Chassis.SlotCurrent10 = ConvertBytesToWordAndScale(0, (Mid$(Data$, 29, 1)), 4)
        Chassis.SlotPolarity10 = FormatPolarity(Mid$(Data$, 30, 1))
        Chassis.SlotVoltage10 = ConvertBytesToWordAndScale(Mid$(Data$, 30, 1), Mid$(Data$, 31, 1), 10)

        'Uut Supply Power
        Chassis.UutPowerTotal! = 0
        For SlotIndex = 1 To 10
            Select Case SlotIndex
                Case 1
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage1 * Chassis.SlotCurrent1))
                    If Chassis.SlotCurrent1 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage1 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 2
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage2 * Chassis.SlotCurrent2))
                    If Chassis.SlotCurrent2 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage2 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 3
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage3 * Chassis.SlotCurrent3))
                    If Chassis.SlotCurrent3 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage3 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 4
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage4 * Chassis.SlotCurrent4))
                    If Chassis.SlotCurrent4 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage4 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 5
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage5 * Chassis.SlotCurrent5))
                    If Chassis.SlotCurrent5 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage5 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 6
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage6 * Chassis.SlotCurrent6))
                    If Chassis.SlotCurrent6 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage6 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 7
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage7 * Chassis.SlotCurrent7))
                    If Chassis.SlotCurrent7 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage7 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 8
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage8 * Chassis.SlotCurrent8))
                    If Chassis.SlotCurrent8 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage8 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 9
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage9 * Chassis.SlotCurrent9))
                    If Chassis.SlotCurrent9 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage9 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
                Case 10
                    Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + ((Chassis.SlotVoltage10 * Chassis.SlotCurrent10))
                    If Chassis.SlotCurrent10 < 0.5 Then
                        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! + (((Chassis.SlotVoltage10 * PPU_PRELOAD_CURRENT!) * PrimaryChassis.DcvLevel) * FPU_INPUT_POWER_EFFICENCY!)
                    End If
            End Select
        Next SlotIndex
        Chassis.UutPowerTotal! = Chassis.UutPowerTotal! / PPU_INPUT_POWER_EFFICENCY!

        'ICPU Status
        Chassis.StatusICPU = Asc(Mid$(Data$, 32, 1))
        CheckFpuStatusByte(Chassis.StatusICPU)
        'Backplane Supply Currents
        Chassis.P5ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 33, 1)), 15)
        Chassis.P5BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 34, 1)), 5)
        Chassis.P12ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 35, 1)), 5)
        Chassis.P24ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 36, 1)), 5)
        Chassis.N2ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 37, 1)), 5)
        Chassis.N52ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 38, 1)), 5)
        Chassis.N24ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 39, 1)), 5)
        Chassis.N12ACurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 40, 1)), 5)
        Chassis.N2BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 41, 1)), 5)
        'Backplane Supply Voltages
        Chassis.P5AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 42, 1), Mid$(Data$, 43, 1), 3)
        Chassis.P5BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 44, 1), Mid$(Data$, 45, 1), 3)
        Chassis.P12AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 46, 1), Mid$(Data$, 47, 1), 3)
        Chassis.P24AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 48, 1), Mid$(Data$, 49, 1), 3)
        Chassis.N2AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 50, 1), Mid$(Data$, 51, 1), 3)
        Chassis.N52AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 52, 1), Mid$(Data$, 53, 1), 3)
        Chassis.N24AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 54, 1), Mid$(Data$, 55, 1), 3)
        Chassis.N12AVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 56, 1), Mid$(Data$, 57, 1), 3)
        Chassis.N2BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 58, 1), Mid$(Data$, 59, 1), 3)

        'DME added for new Freedom Power Supply
        'NEW CURRENTS
        Chassis.P12BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 97, 1)), 5)
        Chassis.P24BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 98, 1)), 5)
        Chassis.N52BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 99, 1)), 5)
        Chassis.N24BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 100, 1)), 5)
        Chassis.N12BCurrent! = ConvertBytesToWordAndScale(CChar(ChrW(0)), (Mid$(Data$, 101, 1)), 5)

        'new voltages
        Chassis.P12BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 102, 1), Mid$(Data$, 103, 1), 3)
        Chassis.P24BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 104, 1), Mid$(Data$, 105, 1), 3)
        Chassis.N52BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 106, 1), Mid$(Data$, 107, 1), 3)
        Chassis.N24BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 108, 1), Mid$(Data$, 109, 1), 3)
        Chassis.N12BVoltage! = ConvertBytesToWordAndScale(Mid$(Data$, 110, 1), Mid$(Data$, 111, 1), 3)

        'Calculate Backplane Supply Power
        Chassis.FpuPowerTotal! = Chassis.P5ACurrent! * Chassis.P5AVoltage!
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.P5BCurrent! * Chassis.P5BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.P12ACurrent! * Chassis.P12AVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.P12BCurrent! * Chassis.P12BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.P24ACurrent! * Chassis.P24AVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.P24BCurrent! * Chassis.P24BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N52ACurrent! * Chassis.N52AVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N52BCurrent! * Chassis.N52BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N24ACurrent! * Chassis.N24AVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N24BCurrent! * Chassis.N24BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N12ACurrent! * Chassis.N12AVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N12BCurrent! * Chassis.N12BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N2ACurrent! * Chassis.N2AVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! + (Chassis.N2BCurrent! * Chassis.N2BVoltage!)
        Chassis.FpuPowerTotal! = Chassis.FpuPowerTotal! / FPU_INPUT_POWER_EFFICENCY!

        'Action Byte
        Chassis.Action2 = Asc(Mid$(Data$, 60, 1))
        'Chassis Status Byte
        Chassis.ChassisStatus = Asc(Mid$(Data$, 61, 1))
        'Fan Speed = Ascue / 2
        Chassis.FanSpeed = (Asc(Mid$(Data$, 62, 1)) / 2)
        'Chassis Temperature
        Chassis.Temperature0 = Asc(Mid$(Data$, 63, 1))
        Chassis.Temperature1 = Asc(Mid$(Data$, 64, 1))
        Chassis.Temperature2 = Asc(Mid$(Data$, 65, 1))
        Chassis.Temperature3 = Asc(Mid$(Data$, 66, 1))
        Chassis.Temperature4 = Asc(Mid$(Data$, 67, 1))
        Chassis.Temperature5 = Asc(Mid$(Data$, 68, 1))
        Chassis.Temperature6 = Asc(Mid$(Data$, 69, 1))
        Chassis.Temperature7 = Asc(Mid$(Data$, 70, 1))
        Chassis.Temperature8 = Asc(Mid$(Data$, 71, 1))
        Chassis.Temperature9 = Asc(Mid$(Data$, 72, 1))
        Chassis.Temperature10 = Asc(Mid$(Data$, 73, 1))
        Chassis.Temperature11 = Asc(Mid$(Data$, 74, 1))
        Chassis.Temperature12 = Asc(Mid$(Data$, 75, 1))
        'Ambient (Intake) Temperature
        Chassis.IntakeTemperature! = (Asc(Mid$(Data$, 76, 1)) / 2) - 45

        'Backplane Data
        Chassis.ChP24V! = ConvertBytesToWordAndScale(Mid$(Data$, 77, 1), Mid$(Data$, 78, 1), 11)
        Chassis.ChP12V! = ConvertBytesToWordAndScale(Mid$(Data$, 79, 1), Mid$(Data$, 80, 1), 12)
        Chassis.ChP5V! = ConvertBytesToWordAndScale(Mid$(Data$, 81, 1), Mid$(Data$, 82, 1), 13)
        Chassis.ChN2V! = ConvertBytesToWordAndScale(Mid$(Data$, 83, 1), Mid$(Data$, 84, 1), 14)
        Chassis.ChN52V! = ConvertBytesToWordAndScale(Mid$(Data$, 85, 1), Mid$(Data$, 86, 1), 13)
        Chassis.ChN12V! = ConvertBytesToWordAndScale(Mid$(Data$, 87, 1), Mid$(Data$, 88, 1), 12)
        Chassis.ChN24V! = ConvertBytesToWordAndScale(Mid$(Data$, 89, 1), Mid$(Data$, 90, 1), 11)

        'Calculate Rise Per Slot (Absolute Values)
        ''For SlotIndex = 0 To 12
        ''    Chassis.TempRisePerSlot!(SlotIndex) = Chassis.Temperature!(SlotIndex) - Chassis.IntakeTemperature!
        ''Next SlotIndex

        For SlotIndex = 0 To 12
            Select SlotIndex
                Case 0 : Chassis.TempRisePerSlot0 = Chassis.Temperature0 - Chassis.IntakeTemperature!
                Case 1 : Chassis.TempRisePerSlot1 = Chassis.Temperature1 - Chassis.IntakeTemperature!
                Case 2 : Chassis.TempRisePerSlot2 = Chassis.Temperature2 - Chassis.IntakeTemperature!
                Case 3 : Chassis.TempRisePerSlot3 = Chassis.Temperature3 - Chassis.IntakeTemperature!
                Case 4 : Chassis.TempRisePerSlot4 = Chassis.Temperature4 - Chassis.IntakeTemperature!
                Case 5 : Chassis.TempRisePerSlot5 = Chassis.Temperature5 - Chassis.IntakeTemperature!
                Case 6 : Chassis.TempRisePerSlot6 = Chassis.Temperature6 - Chassis.IntakeTemperature!
                Case 7 : Chassis.TempRisePerSlot7 = Chassis.Temperature7 - Chassis.IntakeTemperature!
                Case 8 : Chassis.TempRisePerSlot8 = Chassis.Temperature8 - Chassis.IntakeTemperature!
                Case 9 : Chassis.TempRisePerSlot9 = Chassis.Temperature9 - Chassis.IntakeTemperature!
                Case 10 : Chassis.TempRisePerSlot10 = Chassis.Temperature10 - Chassis.IntakeTemperature!
                Case 11 : Chassis.TempRisePerSlot11 = Chassis.Temperature11 - Chassis.IntakeTemperature!
                Case 12 : Chassis.TempRisePerSlot12 = Chassis.Temperature12 - Chassis.IntakeTemperature!
            End Select
        Next SlotIndex




        'External Power Data
        Chassis.VinSinglePhase = ConvertBytesToWordAndScale(0, Mid$(Data$, 91, 1), 6)
        Chassis.VinThreePhase = ConvertBytesToWordAndScale(0, Mid$(Data$, 92, 1), 7)
        Chassis.CurrentIn = ConvertBytesToWordAndScale(0, (Mid$(Data$, 93, 1)), 8)
        Chassis.PowerOk = Asc(Mid$(Data$, 94, 1))
        CheckPowerStatusByte(Chassis.PowerOk)

        Chassis.DcvLevel = ConvertBytesToWordAndScale(0, (Mid$(Data$, 95, 1)), 9)
        Chassis.StatusInputPower = Asc(Mid$(Data$, 96, 1))


        'Power Budget
        'Check Power Limitations
        'Calculate Input Power
        If CInt(Chassis.VinSinglePhase) = 0 And CInt(Chassis.VinThreePhase) = 0 Then 'DC Power
            If Chassis.DcvLevel <= 28 Then
                'ECP 004 - Added additional if statement to allow for error in data
                'dump of GPIB data.  Leaves Input power the same if the level is below
                '18 VDC - GJohnson 4/27/00
                If Chassis.DcvLevel > 18 Then
                    InputPower = Chassis.DcvLevel * 100 'Assume 100 AMP or some Constent Input
                End If
            Else
                InputPower = 2800 '2800 Watts During Normal Operation
            End If
        Else
            InputPower = 2800 '2800 Watts During Normal Operation
        End If

        'Calculate Heater Draw
        CheckChassisStatusByte(PrimaryChassis.ChassisStatus)
        If StatHeater1 Then
            Heater = Heater + 1
        End If
        If StatHeater2 Then
            Heater = Heater + 1
        End If
        CheckChassisStatusByte(SecondaryChassis.ChassisStatus)
        If StatHeater1 Then
            Heater = Heater + 1
        End If
        If StatHeater2 Then
            Heater = Heater + 1
        End If
        HeaterPower = Heater * ((Chassis.DcvLevel * Chassis.DcvLevel) / HEATER_RES!)
        'Calculate Fan Draw
        FanPower = PrimaryChassis.FanSpeed * FAN_SPEED_POWER!
        FanPower = FanPower + (SecondaryChassis.FanSpeed * FAN_SPEED_POWER!)
        FanPower = FanPower + FAN_PDU_MINIMUM_DRAW
        'Calculate User Draw on System
        UserPowerConsumption = (PrimaryChassis.UutPowerTotal) + (PrimaryChassis.FpuPowerTotal)
        UserPowerConsumption = UserPowerConsumption + FanPower + HeaterPower + IC_POWER
        'Calculate System Power Usage
        TotalPowerUsage = InputPower - UserPowerConsumption
        'Calculate DC Input Current Level
        If Int(Chassis.DcvLevel) = 0 Then Chassis.DcvLevel = 1 'Divide By Zero Trap
        DcInputCurrent = UserPowerConsumption / Chassis.DcvLevel

        'Set Function Value
        FormatSystemMonitorData = Chassis

    End Function

    Function ConvertBytesToWordAndScale(DataHighByte As String, DataLowByte As String, ScaleMode As Integer) As Single
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*     This Module Converts System Data Into Values That Can*
        '*     be more easily recognised by the user                *
        '*        DataHighByte$: ASCII Rep Of High Byte Value       *
        '*        DataLowByte$: ASCII Rep Of Low Byte Value         *
        '*        ScaleMode: Chooses Scaling Method                 *
        '*    EXAMPLE:                                              *
        '*     ConvertBytesToWordAndScale "33", "44", 1             *
        '************************************************************
        Dim ScaleValue As Single
        Dim HighByte As Short
        Dim LowByte As Short

        HighByte = Asc(DataHighByte$)
        'Mask Off the MS Nibble
        HighByte = HighByte And 15
        If DataLowByte$ = "" Then
            LowByte = 0
        Else
            LowByte = Asc(DataLowByte$)
        End If
        'Shift Highbyte To Correct Binary Weights
        HighByte = HighByte * 256
        Select Case ScaleMode
            Case 1 '24 Volt Scale
                ScaleValue! = ((HighByte + LowByte) * 0.16)
            Case 2 '12 Volt Scale
                ScaleValue! = ((HighByte + LowByte) * 0.00488) * 2
            Case 3 'Straight Scale
                ScaleValue! = ((HighByte + LowByte) / 100)
            Case 4 'UUT Power Current
                ScaleValue! = (LowByte / 500) * 16
            Case 5 'FPU Currents
                ScaleValue! = (HighByte + LowByte) * 0.16
            Case 6 'Vin Single Scale
                ScaleValue! = LowByte * 0.598
            Case 7 'Vin Three Phase
                ScaleValue! = (1.288 - ((LowByte - 103) * 0.000838)) * LowByte
            Case 8 'Current Input Scale
                ScaleValue! = LowByte / 4.65
            Case 9 'Dc Input Scale
                If LowByte = 0 Then
                    ScaleValue! = 0
                Else
                    ScaleValue! = 4 / (LowByte * 0.001013)
                End If
            Case 10 'Straight Scale for 65V supply changed 5/11/97 SJM
                ScaleValue! = ((HighByte + LowByte) / 50)
            Case 11 'Chassis Measured Supply Level 24v
                ScaleValue! = HighByte + LowByte
                ScaleValue! = (ScaleValue! * 8) / 1000
            Case 12 'Chassis Measured Supply Level 12v
                ScaleValue! = HighByte + LowByte
                ScaleValue! = (ScaleValue! * 4) / 1000
            Case 13 'Chassis Measured Supply Level 5v /5.2
                ScaleValue! = HighByte + LowByte
                ScaleValue! = (ScaleValue! * 2) / 1000
            Case 14 'Chassis Measured Supply Level 2v
                ScaleValue! = HighByte + LowByte
                ScaleValue! = (ScaleValue!) / 1000
            Case 15 'P5A Currents
                ScaleValue! = (HighByte + LowByte) * 0.32
            Case Else ' Error
                ConvertBytesToWordAndScale! = 0
                Exit Function
        End Select
        'Add Corrected HighByte to Low Byte
        ConvertBytesToWordAndScale! = ScaleValue!

    End Function

    Function FormatPolarity(DataHighByte As String) As Boolean
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*     This Module Converts System Data Into Values That Can*
        '*     be more easily recognised by the user                *
        '*        DataHighByte$: ASCII Rep Of High Byte Value       *
        '*    EXAMPLE:                                              *
        '*     SlotPolarity = FormatPolarity(Asc(PowerByte))        *
        '************************************************************
        Dim HighByte As Integer
        HighByte = Asc(DataHighByte$)

        If ((HighByte And &H80) <> 0) Then
            FormatPolarity = False ' Normal=True, Reversed=False
        Else
            FormatPolarity = True ' Normal=True, Reversed=False
        End If

    End Function

    Sub CheckActionByte(ActionByte As String)
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in an Action   *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    ActionByte$: An ASCII Character Representing The Value*
        '*     of the System Monitor Action Byte.                   *
        '*    EXAMPLES:                                             *
        '*     CheckActionByte "33"                                 *
        '*     CheckActionByte Str(AByte)                          *
        '************************************************************
        Dim ActionByteValue As Integer

        '-------------------------------------------------------------------------------
        '- Action Status Byte ----------------------------------------------------------
        '-        B7 |  B6 |        B5 |           B4 | B3 |     B2 |     B1 |      B0 -
        '- Data Dump | RQS | Ret Error | Module Fault | A3 | A2/28V | A1/PRB | A0/RCVR -
        '-------------------------------------------------------------------------------
        'Data Dump 1000 xxxx
        'Query Failed 0010 xxxx
        'Mod Failed 0001 ADDR
        'Action Byte 1011 xABC where A=28V, B=PROBE, C=RECEIVER
        'ICPU Response 0011 xxxx
        'Module Response 0100 ADDR
        'Generally 80H = Data Dump
        '          BXH = Action Byte
        '-------------------------------------------------------------------------------
        ActionByteValue = Val(ActionByte$)
        ActChassisAddress = ActionByteValue And 8
        ActVolt28Ok = ActionByteValue And 4
        ActProbeEvent = ActionByteValue And 2
        ActReceiverEvent = ActionByteValue And 1

    End Sub

    Sub CheckFpuStatusByte(FpuByte As String)
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in an FPU      *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    FpuByte$: An ASCII Character Representing The         *
        '*    Value of the System Monitor FPU Status Byte.          *
        '*    EXAMPLES:                                             *
        '*     CheckFpuStatusByte "33"                              *
        '*     CheckFpuStatusByte Str(AByte)                       *
        '************************************************************
        Dim FpuValue As Integer

        FpuValue = Val(FpuByte$)
        '----------------------------------------------------------------
        '- Fixed Power Unit Status Byte ---------------------------------
        '-        B7 | B6 |             B5 |     B4 | B3 | B2 | B1 | B0 -
        '- Mod Fault | On | Off / Mismatch | OV/*UV | A3 | A2 | A1 | A0 -
        '----------------------------------------------------------------
        'Mismatch 1010 ADDR         ADDR=Address of Module In incorrect slot
        'Over Voltage 1001 ADDR     ADDR=Address of a faulting module
        'Undervoltage 1000 ADDR     ADDR=Address of a faulting module
        'On 0100 XXXX               1 If any modules are on
        'Off 0010 XXXX              0 If any modules are off
        'Ex. FpuStatModFault=1     1 | X | Mismatch=1 VoltProb=0 | OV/*UV | A3 | A2 | A1 | A0
        'Ex. FpuStatModFault=0     0 | On | Off | X | X | X | X | X
        '----------------------------------------------------------------
        FpuStatModFault = FpuValue And 128 'Mask Bit 7 (Clear All Other Bits)
        FpuStatOn = FpuValue And 64 'Mask Bit 6 (Clear All Other Bits)
        FpuStatOffMismnatch = FpuValue And 32 'Mask Bit 5 (Clear All Other Bits)
        FpuStatOverVolt = FpuValue And 16 'Mask Bit 4 (Clear All Other Bits)
        FpuStatAddress = FpuValue And &HF 'Mask Bits 0,1,2,3 (Clear All Other Bits)

    End Sub

    Sub CheckPowerStatusByte(PowerStatusByte As String)
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in a POWER     *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    PowerStatusByte$: An ASCII Character Representing The *
        '*    Value of the System Monitor POWER Status Byte.        *
        '*    EXAMPLES:                                             *
        '*     CheckPowerStatusByte "33"                            *
        '*     CheckPowerStatusByte Str(AByte)                     *
        '************************************************************
        Dim PowerStatusValue As Integer

        PowerStatusValue = Val(PowerStatusByte$)
        '-------------------------------------------------------------------------------------
        '- Power Status Byte -----------------------------------------------------------------
        '-         B7 | B6 |              B5 |            B4 |   B3 |   B2 |   B1 |      B0  -
        '- Input Fail | AC | DC / Fail State | Three/*Single | HPH1 | HPH2 | HPH3 | 2800W OK -
        '-------------------------------------------------------------------------------------
        'Single Phase AC x100 0000 Phase 1,2,or 3 will go Hi on HAM fault
        '                          2800 W will go Hi upon a converter failure
        'Three Phase AC x101 0000  Phase 1,2, or 3 will indicate missing phase or
        '                          HAM Fault, 2800 W will go Hi upon a converter failure
        'DC Mode x010 xxxx
        'Input Failure detected by PPU 1010 0000 Input voltage level exceeded 32 V
        '-------------------------------------------------------------------------------------
        PowerStatus28FailHigh = PowerStatusValue And 128 'Mask Bit 7 (Clear All Other Bits)
        PowerStatusAc = PowerStatusValue And 64 'Mask Bit 6 (Clear All Other Bits)
        PowerStatusDc = PowerStatusValue And 32 'Mask Bit 5 (Clear All Other Bits)
        PowerStatusSingle = PowerStatusValue And 16 'Mask Bit 4 (Clear All Other Bits)
        PowerStatusPhase1 = PowerStatusValue And 8 'Mask Bit 3 (Clear All Other Bits)
        PowerStatusPhase2 = PowerStatusValue And 4 'Mask Bit 2 (Clear All Other Bits)
        PowerStatusPhase3 = PowerStatusValue And 2 'Mask Bit 1 (Clear All Other Bits)
        PowerStatus2800Watt = PowerStatusValue And 1 'Mask Bit 0 (Clear All Other Bits)

    End Sub

    Sub CheckChassisStatusByte(StatusByte As Integer)
        '************************************************************
        '* Nomenclature   : Viper Tools                             *
        '*    DESCRIPTION:                                          *
        '*    This Module Evaluates the Logical Bits in a STATUS    *
        '*    Byte by Masking with logical AND and placing the      *
        '*    results in module level dimensioned variables.        *
        '*    StatusByte: An Integer Representing The              *
        '*    Value of the System Monitor Status Byte.              *
        '*    EXAMPLES:                                             *
        '*     CheckChassisStatusByte 33                            *
        '*     CheckChassisStatusByte Chr$(AsciiByte$)              *
        '************************************************************

        StatSelfTest = StatusByte And 32
        StatHeater2 = StatusByte And 16
        StatHeater1 = StatusByte And 8
        StatAirFlowSensor3 = StatusByte And 4
        StatAirFlowSensor2 = StatusByte And 2
        StatAirFlowSensor1 = StatusByte And 1

    End Sub

    ''    Function ChassisBackplaneSupplyCheck() As String
    ''        '************************************************************
    ''        '* Nomenclature   : System Monitor  [SystemStartUp]         *
    ''        '*    DESCRIPTION:                                          *
    ''        '*     Checks to make sure that there are no hazardous      *
    ''        '*     conditions thst should prevent the FPU from starting *
    ''        '*    RETURNS:                                              *
    ''        '*     Error message describing the failure or a NULL       *
    ''        '*    EXAMPLE:                                              *
    ''        '*      FpuError$ = ChassisBackplaneSupplyCheck             *
    ''        '************************************************************
    ''        Dim ReadTimeout As Integer 'Number of times to try to get a valig GPIB Handle
    ''        Dim Buffer As String = Space(255) 'Data Dump Buffer
    ''        Dim status As Integer 'Status Byte
    ''        Dim retCount As Integer 'Size of returned data dump
    ''        Dim Data As String 'Data Dump String of Bytes describing system events and properties
    ''        Dim FailureMessage As String 'Description of failure
    ''        Dim StartChassis As ChassisData 'Holds Formatted Data in a structure (user defined type)
    ''        Dim InputPowerStatusByte As Integer 'The unformatted Input Power Status Byte
    ''        Dim PhaseFail As Integer 'Shows the Failing Phase Number or Zero

    ''        'Get Chassis Data
    ''        FailureMessage$ = GetOneDataDumpFromBothChassis()
    ''        If FailureMessage$ <> "" Then
    ''            FailureMessage$ = "Communication Error: Status Byte not returned."
    ''            GoTo CompleteFunction 'Chassis Communication Error
    ''        End If

    ''        'Check for Input Power Fault
    ''        InputPowerStatusByte = Asc(Mid$(Buffer$, 96, 1))
    ''        If (InputPowerStatusByte And &H6) = 0 Then
    ''            'Power Ok
    ''        Else 'Input Power Fail
    ''            If (InputPowerStatusByte And &H4) = 4 Then 'Fault Low
    ''                FailureMessage$ = "Error: Input Power Voltages have fallen below the safe operating level."
    ''            End If
    ''            If (InputPowerStatusByte And &H2) = 2 Then 'Fault High
    ''                FailureMessage$ = "Error: Input Power Voltages have risen above the safe operating level."
    ''            End If
    ''            GoTo CompleteFunction
    ''        End If

    ''        'TDR98268 Check for HAM Phase Converter Fault BEFORE Starting FPU
    ''        If (PowerStatusPhase1 <> 0) And (PowerStatusDc = 0) Then 'HAM1
    ''            FailureMessage$ = "Error: FPU HAM Phase 1 is not functioning correctly due to"
    ''            PhaseFail = 1
    ''        End If
    ''        If (PowerStatusPhase2 <> 0) And (PowerStatusDc = 0) Then 'HAM2
    ''            FailureMessage$ = "Error: FPU HAM Phase 2 is not functioning correctly due to"
    ''            PhaseFail = 2
    ''        End If
    ''        If (PowerStatusPhase3 <> 0) And (PowerStatusDc = 0) Then 'HAM3
    ''            FailureMessage$ = "Error: FPU HAM Phase 3 is not functioning correctly due to"
    ''            PhaseFail = 3
    ''        End If
    ''        'Add some diagnostic data
    ''        If (PowerStatusAc <> 0) And (PhaseFail <> 0) Then
    ''            If PowerStatusSingle <> 0 Then 'Three Phase
    ''                'Three Phase
    ''                FailureMessage$ = FailureMessage$ & " a missing neutral connection or a missing phase connection"
    ''            Else
    ''                'Single Phase
    ''                FailureMessage$ = FailureMessage$ & " a faulty power cable or connector"
    ''            End If
    ''            GoTo CompleteFunction
    ''        End If

    ''        'Check For FPU Module Faults
    ''        CheckFpuStatusByte(StartChassis.StatusICPU) 'Get FPU Characteristics
    ''        'FpuStatAddress '= Module With Problem
    ''        If FpuStatModFault <> 0 Then
    ''            If FpuStatOffMismnatch = 0 Then 'Voltage Problem
    ''                If FpuStatOverVolt = 0 Then 'Under Voltage
    ''                    FailureMessage$ = "Error: FPU Module " & Str(FpuStatAddress) & " has faulted due to "
    ''                    FailureMessage$ = FailureMessage$ & "an Under Voltage condition."
    ''                    GoTo CompleteFunction
    ''                Else 'Over Voltage
    ''                    FailureMessage$ = "Error: FPU Module " & Str(FpuStatAddress) & " has faulted due to "
    ''                    FailureMessage$ = FailureMessage$ & "an Over Voltage condition."
    ''                    GoTo CompleteFunction
    ''                End If
    ''            Else 'Mismatched Module
    ''                FailureMessage$ = "Error: FPU Module " & Str(FpuStatAddress)
    ''                FailureMessage$ = FailureMessage$ & " has been inserted in the wrong FPU slot."
    ''                GoTo CompleteFunction
    ''            End If
    ''        End If

    ''CompleteFunction:  'Goto Label Address

    ''        'Return Function Value
    ''        ChassisBackplaneSupplyCheck$ = FailureMessage$

    ''    End Function

    Function GetOneDataDumpFromBothChassis() As String

        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '*    DESCRIPTION:                                          *
        '*     Gats a data dump from both chassis and formats the   *
        '*     data.  Typically used during startup before the      *
        '*     user is presented with the SysMon GUI                *
        '*    RETURNS                                               *
        '*     String containing the failure description text text  *
        '*    EXAMPLE:                                              *
        '*     Failure$ = GetOneDataDumpFromBothChassis()           *
        '************************************************************
        Dim Buffer As String = Space(255) 'Data Dump Buffer
        Dim status As Integer 'System Status Byte
        Dim retCount As Integer 'VISA: Number of characters returned
        Dim Data As String 'Input Buffer Formatted into a VB String
        Dim ReadTimeout As Integer 'Number of "reads" left until an error is realized
        Dim PriChass As Integer 'Got Primary Chassis Data Flag
        Dim SecChass As Integer 'Got Secondary Chassis Data Flag
        Dim FailureMessage As String = "" 'Description of failure
        Dim ChassisIndex As Integer 'Chassis Index value
        On Error Resume Next
        Dim initTry As Integer = 0

        If GpibControllerHandle0 = 0 Then
            If RmSession = 0 Then
                RetValue = viOpenDefaultRM(RmSession)
                If RetValue <> VI_SUCCESS Then
                    FailureMessage = "Error Initializing VISA GPIB Resource Manager "
                    GoTo EndOfFunction
                End If
            End If
            initTry = 1
            Do
                RetValue = viOpen(RmSession, "GPIB0::5::0", VI_NULL, VI_NULL, GpibControllerHandle0)
                Delay(0.5)
                initTry = initTry + 1
            Loop Until (RetValue >= VI_SUCCESS) Or initTry >= 10
            initTry = 1
            Do
                RetValue = viOpen(RmSession, "GPIB0::5::15", VI_NULL, VI_NULL, GpibControllerHandle15)
                Delay(0.5)
                initTry = initTry + 1
            Loop Until (RetValue >= VI_SUCCESS) Or initTry >= 10
        End If

        'Get VXI Chassis Data
        ReadTimeout = 10 '5 Seconds To Acquire Chassis Data Before Time Out Failure
        Do 'Acquire Primary and Secondary Chassis Informaton
            Delay(0.5)
            ReadTimeout = ReadTimeout - 1
            RetValue = viReadSTB(GpibControllerHandle0, status) 'Get Status Byte (B7)
            Buffer = Space(255)
            RetValue = viRead(GpibControllerHandle15, Buffer, 255, retCount) 'Get Data Dump

            If status And 128 Then
                Data$ = Mid(Buffer, 1, retCount)
                If Val((Asc(Mid$(Buffer, 60, 1)))) And 8 Then
                    'Get Chassis #2
                    SecondaryChassis = FormatSystemMonitorData(Buffer)
                    SecChass = True
                    If DisSecondary = True Then
                        DisSecondary = False
                    End If
                Else
                    'Get Chassis #1
                    PrimaryChassis = FormatSystemMonitorData(Buffer)
                    PriChass = True
                    If DisPrimary = True Then
                        DisPrimary = False
                    End If
                End If
            Else

            End If
        Loop Until ((DisPrimary = True Or PriChass = True) And (DisSecondary = True Or SecChass = True)) Or (ReadTimeout <= 0)

        If ReadTimeout <= 0 Then 'If timeout occurs then report an error
            FailureMessage = "Chassis Communication Timeout Error"
        End If


        'Check for Chassis Communication Error
        If PriChass = False And SecChass = False Then
            FailureMessage = "Communication Error: Both Chassis are not reporting data."
            DisPrimary = True
            DisSecondary = True
        ElseIf PriChass = False Then
            If DisPrimary = False Then
                FailureMessage = "Communication Error: Primary VXI Chassis is not reporting data."
                DisPrimary = True
            Else
                FailureMessage = ""
            End If
        ElseIf SecChass = False Then
            If DisSecondary = False Then
                FailureMessage = "Communication Error: Secondary VXI Chassis is not reporting data."
                DisSecondary = True
            Else
                FailureMessage = ""
            End If
        End If

EndOfFunction:
        GetOneDataDumpFromBothChassis = FailureMessage

    End Function




    Sub ReleaseSystemHandles()

        Dim SupplySlot As Short
        Dim ABuffer As String = Space(4)
        Dim count As Integer
        Dim retCount As Integer
        Dim ChassisSlot As Short
        Dim ExitFlag As Short
        Dim UserText As String
        Dim MaximumRiseTemp As Single
        Dim FailureMessage As String
        On Error Resume Next

        'Prevent PPU Supplies from "blinking" after a clean shut-down
        If GpibControllerHandle0 <> 0 Then
            EnableShutdownFailSafeCommand()
            Delay(1)
        End If
        'DoEvents

        'Close System Controller Session
        If GpibControllerHandle11 > 0 Then
            RetValue = viClose(GpibControllerHandle11)
            Echo("Remove GPIB#11 handle")
            Application.DoEvents()
        End If
        If GpibControllerHandle14 > 0 Then
            RetValue = viClose(GpibControllerHandle14)
            Echo("Remove GPIB#14 handle")
            Application.DoEvents()
        End If
        If GpibControllerHandle15 > 0 Then
            RetValue = viClose(GpibControllerHandle15)
            Echo("Remove GPIB#15 handle")
            Application.DoEvents()
        End If

        'Close FPU Sessions
        If GpibControllerHandle0 > 0 Then
            RetValue = viClose(GpibControllerHandle0)
            Echo("Remove GPIB#0 handle")
            Application.DoEvents()
        End If

        'Close Chassis Controllers
        If ChassisControllerHandle1 > 0 Then
            RetValue = viClose(ChassisControllerHandle1)
            Echo("Remove Chassis Controller handle #1")
            Application.DoEvents()
        End If
        If ChassisControllerHandle2 > 0 Then
            RetValue = viClose(ChassisControllerHandle2)
            Echo("Remove Chassis Controller handle #2")
            Application.DoEvents()
        End If

        'Close PPU Sessions
        For SupplySlot = 1 To 10
            If PpuControllerHandle(SupplySlot) > 0 Then
                RetValue = viClose(PpuControllerHandle(SupplySlot))
                PpuControllerHandle(SupplySlot) = 0
                Echo("Remove DC" & CChar(ChrW(SupplySlot)) & " handle")
                Application.DoEvents()
            End If
        Next SupplySlot
        Application.DoEvents()

        'Close Resource Manager Session
        If RmSession > 0 Then
            RetValue = viClose(RmSession)
            Echo("Remove Session handle")
            Application.DoEvents()
        End If

    End Sub

    ''Sub ResetChassis(Chassis As Integer)

    ''    Dim Acount As Integer
    ''    Dim B1 As Short
    ''    Dim B2 As Short
    ''    Dim B3 As Short
    ''    Dim Handle As Integer
    ''    Dim Buffer As String
    ''    Dim retCount As Long
    ''    Dim Pass As Short

    ''    Acount = 3
    ''    B1 = &H7F
    ''    B2 = &HFF
    ''    B3 = &HFF

    ''    If Chassis = 1 Then
    ''        Handle = ChassisControllerHandle1
    ''    Else
    ''        Handle = ChassisControllerHandle2
    ''    End If
    ''    If Handle = 0 Then Exit Sub
    ''    Buffer = CChar(ChrW(B1)) & CChar(ChrW(B2)) & CChar(ChrW(B3))

    ''    Delay(1) 'Pevent Command Overruns
    ''    If Handle <> 0 Then
    ''        RetValue = viWrite(Handle, Buffer, Acount, retCount)
    ''    End If

    ''    If RetValue <> VI_SUCCESS Then
    ''        Pass = False
    ''    End If

    ''End Sub

    Sub EnableShutdownFailSafeCommand()

        Dim Handle As Integer
        Dim Buffer As String
        Dim retCount As Integer
        Dim Pass As Short
        Dim Acount As Integer

        Handle = GpibControllerHandle0
        If Handle = 0 Then Exit Sub
        Buffer$ = CChar(ChrW(&H40)) & "AA"
        Acount = 3
        RetValue = viWrite(Handle, Buffer, Acount, retCount)
        If RetValue <> VI_SUCCESS Then
            Pass = False
        End If


    End Sub


End Module

