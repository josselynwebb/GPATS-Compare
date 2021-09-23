Imports System.Runtime.InteropServices

Module VEO2
    ''' <summary>
    ''' Set the VEO2 system configuration.
    ''' </summary>
    ''' <param name="SysConfig">
    ''' Integer can be one of the following:
    ''' 1.		None
    ''' 2.		Blackbody
    ''' 3.		Visible
    ''' 4.		Vis. Align
    ''' 5.		Laser
    ''' 6.		Laser Align
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_SYSTEM_CONFIGURATION_INITIATE", ExactSpelling:=True, CharSet:=CharSet.Ansi, SetLastError:=True)> _
    Public Sub SET_SYSTEM_CONFIGURATION_INITIATE(SysConfig As Integer)
    End Sub

    ''' <summary>
    ''' Fetch the VEO2 system configuration.
    ''' </summary>
    ''' <param name="SysConfig">
    ''' int pointer, memory location to contain one of the following:
    ''' 1.		None
    ''' 2.		Blackbody
    ''' 3.		Visible
    ''' 4.		Vis. Align
    ''' 5.		Laser
    ''' 6.		Laser Align
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_SYSTEM_CONFIGURATION_FETCH")> _
    Public Sub SET_SYSTEM_CONFIGURATION_FETCH(ByRef SysConfig As Integer)
    End Sub

    ''' <summary>
    ''' This function should be called any time the power is reset to the module and IRWindows is not restarted. 
    ''' There is no need to call this before all commands unless the power to the module has been cycled. 
    ''' This function only has an initiate that takes no parameters. 
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="RESET_MODULE_INITIATE")> _
    Public Sub RESET_MODULE_INITIATE()
    End Sub

    ''' <summary>
    ''' This function shuts down the IRWindows engine, releasing resources.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="IRWIN_SHUTDOWN")> _
    Public Sub IRWIN_SHUTDOWN()
    End Sub

    ''' <summary>
    ''' During the operation, the instrument performs a series of tests to make sure that the module is operating normally. 
    ''' These tests are performed in the background and transparent to the operator. 
    ''' If an error condition is detected, an SRQ will be generated over the IEEE bus.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_BIT_DATA_INITIATE")> _
    Public Sub GET_BIT_DATA_INITIATE()
    End Sub

    ''' <summary>
    ''' This routine fetches the BIT DATA from the GET_BIT_DATA_INITIATE() routine
    ''' as described below.
    ''' Error	Condition
    '''  01	The blackbody temperature is above its temperature range.
    '''  02	After stabilizing, the blackbody has moved out of the Ready window.
    '''  03	The blackbody is unable to reach its set point.
    '''  04	An unrecognized command has been received over the remote bus.
    '''  05	An out-of-range command has been received
    '''  10	EEPROM confidence check has failed
    '''  11	Internal communication has failed
    '''  12	Internal execution error
    '''  22	Invalid calibration point.
    '''  23	Shorted thermistor
    '''  24	Open thermistor
    '''  25	EEPROM dropout
    '''  26	ADC2 failed
    '''  27	ADC1 failed
    '''  28	Serial buffer overflow 
    '''  29	EEPROM checksum error
    '''  30	Detent not Set while in idle
    '''  31	Time-out, motor not to position
    '''  34	DAC failure
    '''  35	Power Amplifier failure
    '''  36	Thermoelectric Cooler (TEC) failure
    '''  37	Power supplies failure
    '''  38	Reset failure
    '''  39	Following error, rotor not keeping up with commanded velocity
    '''  40	Encoder BIT failure
    '''  42	Any lamp failure
    '''  45	General communications error
    '''  46	Fan failure
    '''  47	Camera Failure
    '''  48	Camera Message Error
    '''  49	Trigger Failure
    '''  50	Interlock not set
    '''  51	Power Supply 1 failure
    '''  52	Power Supply 2 failure
    '''  91	The second Smart Probe has failed (Collimator)
    '''  95	The first Smart Probe has failed
    '''  97	The power amplifier Card has failed
    '''  98	The GCC has failed
    '''  99	The ACC has failed
    ''' </summary>
    ''' <param name="Error_Number"></param>
    ''' int pointer, location to store the BIT DATA information
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_BIT_DATA_FETCH")> _
    Public Sub GET_BIT_DATA_FETCH(ByRef Error_Number As Integer)
    End Sub

    ''' <summary>
    ''' Command the module to return its ID
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_MODULE_ID_INITIATE")> _
    Public Sub GET_MODULE_ID_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetch the module ID
    ''' 0: Wrong ID detected.
    ''' 1: Correct ID detected.
    ''' </summary>
    ''' <param name="xIDent">
    ''' int pointer, location to store the module ID
    ''' 0: Wrong ID detected.
    ''' 1: Correct ID detected.
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_MODULE_ID_FETCH")> _
    Public Sub GET_MODULE_ID_FETCH(ByRef xIDent As Integer)
    End Sub

    ''' <summary>
    ''' Command module to return Status byte on next fetch.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_STATUS_BYTE_MESSAGE_INITIATE")> _
    Public Sub GET_STATUS_BYTE_MESSAGE_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetch the module Status byte.
    ''' BIT			TRUE		FALSE
    '''08 (MSB)		 X			  X
    '''07			SRQ issued	 No SRQ issued
    '''06			error		 No error
    '''05			busy		 ready
    '''04			 X			  X
    '''03			 X			  X
    '''02			 X			  X
    '''01(LSB)		 X			  X
    '''
    '''X = Don’t care.
    ''' </summary>
    ''' <param name="xStatus">
    ''' int pointer, location to store the fetched Status byte.
    ''' BIT			TRUE		FALSE
    '''08 (MSB)		 X			  X
    '''07			SRQ issued	 No SRQ issued
    '''06			error		 No error
    '''05			busy		 ready
    '''04			 X			  X
    '''03			 X			  X
    '''02			 X			  X
    '''01(LSB)		 X			  X
    '''
    '''X = Don’t care.
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_STATUS_BYTE_MESSAGE_FETCH")> _
    Public Sub GET_STATUS_BYTE_MESSAGE_FETCH(ByRef xStatus As Integer)
    End Sub

    ''' <summary>
    ''' Command module to return the target temperature on next fetch
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_TEMP_TARGET_IR_INITIATE")> _
    Public Sub GET_TEMP_TARGET_IR_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetch the target temperature
    ''' </summary>
    ''' <param name="xTarget_Temp">
    ''' float pointer, location to store the target temperature
    ''' +10 to +60 degrees Celsius
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="GET_TEMP_TARGET_IR_FETCH")> _
    Public Sub GET_TEMP_TARGET_IR_FETCH(ByRef xTarget_Temp As Single)
    End Sub

    ''' <summary>
    ''' When the blackbody temperature is stable within the limits specified by this command, 
    ''' the Busy/Ready Bit in the status byte will be set to 1
    ''' </summary>
    ''' <param name="xRdy_Window">
    ''' float 0.005 to 5.000 Celsius degrees 
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_RDY_WINDOW_IR_INITIATE")> _
    Public Sub SET_RDY_WINDOW_IR_INITIATE(xRdy_Window As Single)
    End Sub

    ''' <summary>
    ''' Ready window value from the instrument is returned
    ''' </summary>
    ''' <param name="xRdy_Window">
    ''' float pointer, value representing the ready window value between 0.005 and 5.000 Celsius degrees 
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_RDY_WINDOW_IR_FETCH")> _
    Public Sub SET_RDY_WINDOW_IR_FETCH(ByRef xRdy_Window As Single)
    End Sub

    ''' <summary>
    ''' Set the target Position 
    ''' Pos	Target 
    '''  0	Open
    '''  1	Pinhole 
    '''  2	Pie Sector
    '''  3	5.00 Cyc/mrad 4 Bar 
    '''  4	3.8325 Cyc/mrad 4 Bar  
    '''  5	2.665 Cyc/mrad 4 Bar 
    '''  6	1.4975  Cyc/mrad 4 Bar  
    '''  7	0.33 Cyc/mrad 4 Bar 
    '''  8	Diagonal Slit
    '''  9	Multi Pinhole 
    ''' 10	Alignment Cross 
    ''' 11	USAF 1951, Groups 0-4
    ''' 12	1.0 Cyc/mrad 4 Bar
    ''' 13	0.66 Cyc/mrad 4 Bar
    ''' 14	21 mrad square aperture  
    ''' </summary>
    ''' <param name="xTarget_Position">
    ''' int target position
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TARGET_POSITION_INITIATE")> _
    Public Sub SET_TARGET_POSITION_INITIATE(xTarget_Position As Integer)
    End Sub

    ''' <summary>
    ''' Get the current target Position 
    ''' Pos	Target 
    '''  0	Open
    '''  1	Pinhole 
    '''  2	Pie Sector
    '''  3	5.00 Cyc/mrad 4 Bar 
    '''  4	3.8325 Cyc/mrad 4 Bar  
    '''  5	2.665 Cyc/mrad 4 Bar 
    '''  6	1.4975  Cyc/mrad 4 Bar  
    '''  7	0.33 Cyc/mrad 4 Bar 
    '''  8	Diagonal Slit
    '''  9	Multi Pinhole 
    ''' 10	Alignment Cross 
    ''' 11	USAF 1951, Groups 0-4
    ''' 12	1.0 Cyc/mrad 4 Bar
    ''' 13	0.66 Cyc/mrad 4 Bar
    ''' 14	21 mrad square aperture  
    ''' </summary>
    ''' <param name="xTarget_Position">
    ''' int pointer for the current target position
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TARGET_POSITION_FETCH")> _
    Public Sub SET_TARGET_POSITION_FETCH(ByRef xTarget_Position As Integer)
    End Sub

    ''' <summary>
    ''' Commands the black body temperature to parameter value. Resolution is .001 °C 
    ''' </summary>
    ''' <param name="xtemperature">
    ''' float +10 to +60, resolution of .001 °C
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TEMP_ABSOLUTE_IR_INITIATE")> _
    Public Sub SET_TEMP_ABSOLUTE_IR_INITIATE(xtemperature As Single)
    End Sub

    ''' <summary>
    ''' Gets the actual reading from the black body
    ''' </summary>
    ''' <param name="xtemperature">
    ''' float pointer, +10 to +60, resolution of .001 °C
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TEMP_ABSOLUTE_IR_FETCH")> _
    Public Sub SET_TEMP_ABSOLUTE_IR_FETCH(ByRef xtemperature As Single)
    End Sub

    ''' <summary>
    ''' Commands the black body differential temperature to parameter value. Resolution is .001 °C 
    ''' </summary>
    ''' <param name="xtemperature">
    ''' float -15 to +35, resolution of .001 °C
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TEMP_DIFFERENTIAL_IR_INITIATE")> _
    Public Sub SET_TEMP_DIFFERENTIAL_IR_INITIATE(xtemperature As Single)
    End Sub

    ''' <summary>
    ''' Gets the actual temperature differential reading from the black body
    ''' </summary>
    ''' <param name="xtemperature">
    ''' float pointer -15 to +35, resolution of .001 °C
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TEMP_DIFFERENTIAL_IR_FETCH")> _
    Public Sub SET_TEMP_DIFFERENTIAL_IR_FETCH(ByRef xtemperature As Single)
    End Sub

    ''' <summary>
    ''' Sets the laser camera trigger source to 0, 1 or 2
    ''' 0. Internal
    ''' 1. External
    ''' 2. Laser Trigger
    ''' </summary>
    ''' <param name="xtrigger">
    ''' int trigger value between 0 and 2
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_CAMERA_TRIGGER_LASER_INITIATE")> _
    Public Sub SET_CAMERA_TRIGGER_LASER_INITIATE(xtrigger As Integer)
    End Sub

    ''' <summary>
    ''' Gets the laser camera trigger setting
    ''' </summary>
    ''' <param name="xtrigger">
    ''' int pointer for current trigger setting
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_CAMERA_TRIGGER_LASER_FETCH")> _
    Public Sub SET_CAMERA_TRIGGER_LASER_FETCH(ByRef xtrigger As Integer)
    End Sub

    ''' <summary>
    ''' Set delay from initial camera trigger to start capture
    ''' </summary>
    ''' <param name="xdelay">
    ''' float 0 to 6553 milliseconds
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_CAMERA_DELAY_LASER_INITIATE")> _
    Public Sub SET_CAMERA_DELAY_LASER_INITIATE(xdelay As Single)
    End Sub

    ''' <summary>
    ''' Get delay setting from initial camera trigger to start capture
    ''' </summary>
    ''' <param name="xdelay">
    ''' float pointer, 0 to 6553 milliseconds
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_CAMERA_DELAY_LASER_FETCH")> _
    Public Sub SET_CAMERA_DELAY_LASER_FETCH(ByRef xdelay As Single)
    End Sub

    ''' <summary>
    ''' Set source stage to requested position, 1-3
    ''' Stow position is 1
    ''' Pos	Stage
    '''  1	Blackbody
    '''  2	Laser
    '''  3	Visible
    ''' </summary>
    ''' <param name="Source_Stage_Position">
    ''' int:
    ''' Pos	Stage
    '''  1	Blackbody
    '''  2	Laser
    '''  3	Visible
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_SOURCE_STAGE_LASER_INITIATE")> _
    Public Sub SET_SOURCE_STAGE_LASER_INITIATE(Source_Stage_Position As Integer)
    End Sub

    ''' <summary>
    ''' Get source stage current position
    ''' Pos	Stage
    '''  1	Blackbody
    '''  2	Laser
    '''  3	Visible
    ''' </summary>
    ''' <param name="Source_Stage_Position">
    ''' int pointer 
    ''' Pos	Stage
    '''  1	Blackbody
    '''  2	Laser
    '''  3	Visible
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_SOURCE_STAGE_LASER_FETCH")> _
    Public Sub SET_SOURCE_STAGE_LASER_FETCH(ByRef Source_Stage_Position As Integer)
    End Sub

    ''' <summary>
    ''' Set sensor stage to requested position, 1-3
    ''' Pos	Stage
    '''  1	None (Pass through)
    '''  2	Energy Meter
    '''  3	Camera (Beam Splitter)
    ''' Stow position is 3
    ''' </summary>
    ''' <param name="Sensor_Stage_Position">
    ''' int:
    ''' Pos	Stage
    '''  1	None (Pass through)
    '''  2	Energy Meter
    '''  3	Camera (Beam Splitter)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_SENSOR_STAGE_LASER_INITIATE")> _
    Public Sub SET_SENSOR_STAGE_LASER_INITIATE(Sensor_Stage_Position As Integer)
    End Sub

    ''' <summary>
    ''' Get sensor stage current position, 1-3
    ''' Pos	Stage
    '''  1	None (Pass through)
    '''  2	Energy Meter
    '''  3	Camera (Beam Splitter)
    ''' </summary>
    ''' <param name="Sensor_Stage_Position">
    ''' int pointer
    ''' Pos	Stage
    '''  1	None (Pass through)
    '''  2	Energy Meter
    '''  3	Camera (Beam Splitter)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_SENSOR_STAGE_LASER_FETCH")> _
    Public Sub SET_SENSOR_STAGE_LASER_FETCH(ByRef Sensor_Stage_Position As Integer)
    End Sub

    ''' <summary>
    ''' This selects the diode that will use the configuration settings, and 
    ''' be controlled by the Laser Power Command
    ''' Setting	Description
    '''   0	    1570 diode 
    '''   1	    1540 diode
    '''   2	    1064 diode
    ''' </summary>
    ''' <param name="xSelect">
    ''' int integer denoting the requested laser diode:
    ''' Setting	Description
    '''   0	    1570 diode 
    '''   1	    1540 diode
    '''   2	    1064 diode
    '''</param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SELECT_DIODE_LASER_INITIATE")> _
    Public Sub SELECT_DIODE_LASER_INITIATE(xSelect As Integer)
    End Sub

    ''' <summary>
    ''' This returns the VEO2 currently selected laser diode
    ''' Setting	Description
    '''   0	    1570 diode 
    '''   1	    1540 diode
    '''   2	    1064 diode
    ''' </summary>
    ''' <param name="xSelect">
    ''' int integer pointer denoting the currently selected laser diode:
    ''' Setting	Description
    '''   0	    1570 diode 
    '''   1	    1540 diode
    '''   2	    1064 diode
    '''</param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SELECT_DIODE_LASER_FETCH")> _
    Public Sub SELECT_DIODE_LASER_FETCH(ByRef xSelect As Integer)
    End Sub

    ''' <summary>
    ''' This sets the configured laser trigger source
    ''' Pos	Stage
    '''  0	Alignment
    '''  1	Free Run
    '''  2	Laser Pulse Trigger
    '''  3	External Trigger
    '''  4	Calibrate
    ''' </summary>
    ''' <param name="xSelect">
    ''' int integer denoting requested laser trigger source
    ''' Pos	Stage
    '''  0	Alignment
    '''  1	Free Run
    '''  2	Laser Pulse Trigger
    '''  3	External Trigger
    '''  4	Calibrate
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TRIGGER_SOURCE_LASER_INITIATE")> _
    Public Sub SET_TRIGGER_SOURCE_LASER_INITIATE(xSelect As Integer)
    End Sub

    ''' <summary>
    ''' This returns the VEO2 currently selected laser trigger source
    ''' Pos	Stage
    '''  0	Alignment
    '''  1	Free Run
    '''  2	Laser Pulse Trigger
    '''  3	External Trigger
    '''  4	Calibrate
    ''' </summary>
    ''' <param name="xSelect">
    ''' int integer pointer denoting the currently selected laser trigger source:
    ''' Pos	Stage
    '''  0	Alignment
    '''  1	Free Run
    '''  2	Laser Pulse Trigger
    '''  3	External Trigger
    '''  4	Calibrate
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_TRIGGER_SOURCE_LASER_FETCH")> _
    Public Sub SET_TRIGGER_SOURCE_LASER_FETCH(ByRef xSelect As Integer)
    End Sub

    ''' <summary>
    ''' Sets the amplitude of returned ranging pulse. Only available when TRIGGER_SOURCE is set to 
    ''' 1 (Free Run), 2 (Laser), or 3 (External) 
    ''' </summary>
    ''' <param name="xPA">
    ''' float 0 - 3000 nW/cm²/sr
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE_AMPLITUDE_LASER_INITIATE")> _
    Public Sub SET_PULSE_AMPLITUDE_LASER_INITIATE(xPA As Single)
    End Sub

    ' ''' <summary>
    ' ''' Gets the set amplitude of returned ranging pulse.
    ' ''' </summary>
    ' ''' <param name="xPA">
    ' ''' float pointer valid values are 0 - 3000 nW/cm²/sr
    ' ''' </param>
    '<DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE_AMPLITUDE_LASER_FETCH")> _
    'Public Shared Sub SET_PULSE_AMPLITUDE_LASER_FETCH(ByRef xPA As Single)
    'End Sub

    ''' <summary>
    ''' Sets the period of laser pulse. Only available when TRIGGER_SOURCE is set to 1 (Free Run)
    ''' </summary>
    ''' <param name="xPA">
    ''' float 50 - 125 ms
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE_PERIOD_LASER_INITIATE")> _
    Public Sub SET_PULSE_PERIOD_LASER_INITIATE(xPA As Single)
    End Sub

    ''' <summary>
    ''' Gets the set period of laser pulse. Only available when TRIGGER_SOURCE is set to 1 (Free Run)
    ''' </summary>
    ''' <param name="xPA">
    ''' float pointer valid values are 50 - 125 ms
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE_PERIOD_LASER_FETCH")> _
    Public Sub SET_PULSE_PERIOD_LASER_FETCH(ByRef xPA As Single)
    End Sub

    ''' <summary>
    ''' Turn on/off laser (Available for all TRIGGER_SOURCE settings)
    ''' Setting	Description
    '''   0	    Turn laser off
    '''   1	    Turn laser on
    ''' </summary>
    ''' <param name="xPA">
    ''' int 0 or 1
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_OPERATION_LASER_INITIATE")> _
    Public Sub SET_OPERATION_LASER_INITIATE(xPA As Integer)
    End Sub

    ''' <summary>
    ''' Get current state of the laser
    ''' Setting	Description
    '''   0	    laser is off
    '''   1	    laser is on
    ''' </summary>
    ''' <param name="xPA">
    ''' int pointer 0 or 1:
    ''' Setting	Description
    '''   0	    laser is off
    '''   1	    laser is on
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_OPERATION_LASER_FETCH")> _
    Public Sub SET_OPERATION_LASER_FETCH(ByRef xPA As Integer)
    End Sub

    ''' <summary>
    ''' Set emulated range of returned pulse. Only available when TRIGGER_SOURCE 
    ''' is set to 2 (Laser) or 3 (External)
    ''' </summary>
    ''' <param name="xPA"></param>
    ''' float 500-60000 m
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_RANGE_EMULATION_LASER_INITIATE")> _
    Public Sub SET_RANGE_EMULATION_LASER_INITIATE(xPA As Single)
    End Sub

    ''' <summary>
    ''' Get emulated range of returned pulse. Only available when TRIGGER_SOURCE 
    ''' is set to 2 (Laser) or 3 (External)
    ''' </summary>
    ''' <param name="xPA"></param>
    ''' float pointer valid between 500-60000 m
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_RANGE_EMULATION_LASER_FETCH")> _
    Public Sub SET_RANGE_EMULATION_LASER_FETCH(ByRef xPA As Single)
    End Sub

    ''' <summary>
    ''' Set timed delay between first and second returned pulses. Set to 0 for no second pulse. 
    ''' Only available when TRIGGER_SOURCE is set to 2 (Laser) or 3 (External)
    ''' </summary>
    ''' <param name="xPA">
    ''' float 0, or 60 to 2000 in 20ns steps
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE2_DELAY_LASER_INITIATE")> _
    Public Sub SET_PULSE2_DELAY_LASER_INITIATE(xPA As Single)
    End Sub

    ''' <summary>
    ''' Get timed delay between first and second returned pulses.
    ''' Only available when TRIGGER_SOURCE is set to 2 (Laser) or 3 (External)
    ''' </summary>
    ''' <param name="xPA">
    ''' float pointer; valid responses are 0 for no second pulse, or 60 to 2000 in 20ns steps
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE2_DELAY_LASER_FETCH")> _
    Public Sub SET_PULSE2_DELAY_LASER_FETCH(ByRef xPA As Integer)
    End Sub

    ''' <summary>
    ''' When a second pulse is returned from a trigger, this command specifies which pulse is the larger of the two. 
    ''' The amplitude of the specified pulse is set by the Pulse Amplitude.
    ''' Only available when TRIGGER_SOURCE is set to 2 (Laser) or 3 (External)
    ''' Setting	  Description
    '''     0	  First return pulse is larger 
    '''     1	  Second return pulse is larger
    ''' </summary>
    ''' <param name="xPA">
    ''' int integer value equal to 0 or 1:
    ''' Setting	  Description
    '''     0	  First return pulse is larger 
    '''     1	  Second return pulse is larger
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SELECT_LARGER_PULSE_LASER_INITIATE")> _
    Public Sub SELECT_LARGER_PULSE_LASER_INITIATE(xPA As Integer)
    End Sub

    ''' <summary>
    ''' When a second pulse is returned from a trigger, this command retrieves which pulse is the larger of the two. 
    ''' The amplitude of the specified pulse is set by the Pulse Amplitude.
    ''' Only available when TRIGGER_SOURCE is set to 2 (Laser) or 3 (External)
    ''' Setting	  Description
    '''     0	  First return pulse is larger 
    '''     1	  Second return pulse is larger
    ''' </summary>
    ''' <param name="xPA">
    ''' int integer pointer value:
    ''' Setting	  Description
    '''     0	  First return pulse is larger 
    '''     1	  Second return pulse is larger
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SELECT_LARGER_PULSE_LASER_FETCH")> _
    Public Sub SELECT_LARGER_PULSE_LASER_FETCH(ByRef xPA As Integer)
    End Sub

    ''' <summary>
    ''' When a second pulse is returned from a trigger, this command specifies the relative amplitude of the two pulses. 
    ''' This control specifies the amplitude of the smaller pulse relative to the larger pulse. 
    ''' Only available when TRIGGER_SOURCE is set to 2 (Laser) or 3 (External) 
    ''' </summary>
    ''' <param name="xpercent">
    ''' float percentage valid 10 to 100
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE_PERCENTAGE_LASER_INITIATE")> _
    Public Sub SET_PULSE_PERCENTAGE_LASER_INITIATE(xpercent As Single)
    End Sub

    ''' <summary>
    ''' When a second pulse is returned from a trigger, this command retrieves the relative amplitude of the two pulses. 
    ''' This control specifies the amplitude of the smaller pulse relative to the larger pulse. 
    ''' Only available when TRIGGER_SOURCE is set to 2 (Laser) or 3 (External) 
    ''' </summary>
    ''' <param name="xpercent">
    ''' float pointer percentage valid 10 to 100
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_PULSE_PERCENTAGE_LASER_FETCH")> _
    Public Sub SET_PULSE_PERCENTAGE_LASER_FETCH(ByRef xpercent As Single)
    End Sub

    ''' <summary>
    ''' Start/Stop laser test mode
    ''' Setting	Description
    '''     0	Stop laser testing
    '''     1	Start laser in test configuration
    ''' </summary>
    ''' <param name="xSelect">
    ''' Integer valid 0 or 1:
    ''' Setting	Description
    '''     0	Stop laser testing
    '''     1	Start laser in test configuration
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LASER_TEST_INITIATE")> _
    Public Sub SET_LASER_TEST_INITIATE(xSelect As Integer)
    End Sub

    ''' <summary>
    ''' Get current laser test mode
    ''' Setting	Description
    '''     0	Stop laser testing
    '''     1	Start laser in test configuration
    ''' </summary>
    ''' <param name="xSelect">
    ''' int pointer valid 0 or 1:
    ''' Setting	Description
    '''     0	Stop laser testing
    '''     1	Start laser in test configuration
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LASER_TEST_FETCH")> _
    Public Sub SET_LASER_TEST_FETCH(ByRef xSelect As Integer)
    End Sub

    ''' <summary>
    ''' Command the visible light radiant strength.  When the radiance is within 10% of the value set by this 
    ''' command then Busy/Ready Bit in the status byte will be set to 1. The resolution is 1.8%.
    ''' </summary>
    ''' <param name="xRadiance">
    ''' float from .0005 to 5000 (μw/cm2-sr) is valid
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_RADIANCE_VIS_INITIATE")> _
    Public Sub SET_RADIANCE_VIS_INITIATE(xRadiance As Single)
    End Sub

    ''' <summary>
    ''' Retrieve the current visible light radiant strength.
    ''' </summary>
    ''' <param name="xRadiance">
    ''' float pointer value from .0005 to 5000 (μw/cm2-sr) is valid
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_RADIANCE_VIS_FETCH")> _
    Public Sub SET_RADIANCE_VIS_FETCH(ByRef xRadiance As Single)
    End Sub

    ''' <summary>
    ''' Set LARRS azimuth position
    ''' </summary>
    ''' <param name="xposition">
    ''' int from 0 - 20000 valid (units=steps)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LARRS_AZ_LASER_INITIATE")> _
    Public Sub SET_LARRS_AZ_LASER_INITIATE(xposition As Integer)
    End Sub

    ''' <summary>
    ''' Get current LARRS azimuth position
    ''' </summary>
    ''' <param name="xposition">
    ''' int pointer values from 0 - 20000 valid (units=steps)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LARRS_AZ_LASER_FETCH")> _
    Public Sub SET_LARRS_AZ_LASER_FETCH(ByRef xposition As Integer)
    End Sub

    ''' <summary>
    ''' Set LARRS elevation position
    ''' </summary>
    ''' <param name="yposition">
    ''' int from 0 - 20000 valid (units=steps)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LARRS_EL_LASER_INITIATE")> _
    Public Sub SET_LARRS_EL_LASER_INITIATE(yposition As Integer)
    End Sub

    ''' <summary>
    ''' Get current LARRS elevation position
    ''' </summary>
    ''' <param name="yposition">
    ''' int pointer values from 0 - 20000 valid (units=steps)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LARRS_EL_LASER_FETCH")> _
    Public Sub SET_LARRS_EL_LASER_FETCH(ByRef yposition As Integer)
    End Sub

    ''' <summary>
    ''' Set LARRS polarization angle
    ''' </summary>
    ''' <param name="xposition">
    ''' int from 0 to 359 degrees valid 
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_LARRS_POLARIZE_LASER_INITIATE")> _
    Public Sub SET_LARRS_POLARIZE_LASER_INITIATE(xposition As Integer)
    End Sub

    ''' <summary>
    ''' Set LARRS polarization angle
    ''' </summary>
    ''' <param name="xposition">
    ''' int pointer values from 0 to 359 degrees valid 
    ''' </param>
    Public Sub SET_LARRS_POLARIZE_LASER_FETCH(ByRef xposition As Integer)
    End Sub

    ''' <summary>
    ''' Set camera power on/off
    ''' Setting	Description
    '''     0	Turn camera power off
    '''     1	Turn camera power on
    ''' </summary>
    ''' <param name="xSelect">
    ''' int valid 0 or 1:
    ''' Setting	Description
    '''     0	Turn camera power off
    '''     1	Turn camera power on
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_CAMERA_POWER_INITIATE")> _
    Public Sub SET_CAMERA_POWER_INITIATE(xSelect As Integer)
    End Sub

    ''' <summary>
    ''' Get current camera power on/off state
    ''' Setting	Description
    '''     0	Turn camera power off
    '''     1	Turn camera power on
    ''' </summary>
    ''' <param name="xSelect">
    ''' int pointer values valid 0 or 1:
    ''' Setting	Description
    '''     0	Turn camera power off
    '''     1	Turn camera power on
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="SET_CAMERA_POWER_FETCH")> _
    Public Sub SET_CAMERA_POWER_FETCH(ByRef xSelect As Integer)
    End Sub

    'These are software VEO2 DLL calls
    ''' <summary>
    ''' Setup will set up the category 1 parameters plus 1 additional parameter,Threshold. Threshold is the percent above the minimum pixel value that is a cutoff value 
    ''' for finding the beam.  
    ''' Image_Num_Frames is the Number of Frames to Average (value 1 - 128)
    ''' Signal_Block_Top_Left_X is the Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' Signal_Block_Top_Left_Y	is the Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.	
    ''' Signal_Block_Bot_Right_X is the Bottom right "x" coordinate of the Signal Block rectangle.
    ''' Signal_Block_Bot_Right_Y is the Bottom right "y" coordinate of the Signal Block rectangle.
    ''' Camera_delaytime Delay time is the time from the input pulse to start the camera capture. This would be used in the instance of a laser pulse of known period. The camera is delayed from the first pulse such that it is setup to capture to second pulse.  .002 increments are valid.
    ''' Camera_trigger (value 0, 1 or 2) 0: Internal, 1: External, 2: Laser Trigger
    ''' Intensity_ratio is the percent above the minimum pixel value that is a cutoff value for finding the beam. (valid 0 - 100%)
    ''' </summary>
    ''' <param name="Image_numframes">
    ''' Integer equal to the number of frames to average (valid 1-128)
    ''' </param>
    ''' <param name="Signal_topleft_X">
    ''' Signal_Block_Top_Left_X is the Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="Signal_topleft_Y">
    ''' Signal_Block_Top_Left_Y	is the Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.	
    ''' </param>
    ''' <param name="Signal_botright_X">
    ''' Signal_Block_Bot_Right_X is the Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="Signal_botright_Y">
    ''' Signal_Block_Bot_Right_Y is the Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="Camera_delaytime">
    ''' The time from the input pulse to start the camera capture. This would be used in the instance of a laser pulse of 
    ''' known period. The camera is delayed from the first pulse such that it is setup to capture to second pulse.  .002 increments are valid.
    ''' </param>
    ''' <param name="Camera_trigger">
    ''' Camera_trigger (value 0, 1 or 2) 0: Internal, 1: External, 2: Laser Trigger
    ''' </param>
    ''' <param name="Intensity_ratio">
    ''' Intensity_ratio is the percent above the minimum pixel value that is a cutoff value for finding the beam. (valid 0 - 100%)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_LASER_SETUP")> _
    Public Sub BORESIGHT_LASER_SETUP(Image_numframes As Integer, Signal_topleft_X As Integer, Signal_topleft_Y As Integer, Signal_botright_X As Integer, Signal_botright_Y As Integer, _
                                                 Camera_delaytime As Single, Camera_trigger As Integer, Intensity_ratio As Single)
    End Sub

    ''' <summary>
    ''' On receipt of the Initiate command IRWindows will execute the following:
    ''' Instruct the operator to fire the laser.
    ''' Capture the image.
    ''' Perform the analysis to determine the centroid.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_LASER_INITIATE")> _
    Public Sub BORESIGHT_LASER_INITIATE()
    End Sub

    ''' <summary>
    ''' The Fetch command will return the (x,y) coordinates (in mr) of the centroid of the beam, the number of pixels in the blob identified as the beam, and Status.
    ''' </summary>
    ''' <param name="LBeam_Align_Coord_X">
    ''' Pointer to float
    ''' The centroid of the beam (x coordinate of the centroid)
    ''' </param>
    ''' <param name="LBeam_Align_Coord_Y">
    ''' Pointer to float
    ''' The centroid of the beam (y coordinate of the centroid)
    ''' </param>
    ''' <param name="LBeam_Area">
    ''' Pointer to float
    ''' Area of the beam in pixels
    ''' </param>
    ''' <param name="Status">
    ''' Pointer to integer
    ''' The status word will consist of a 16-bit word where each bit will indicate the type of failure that has occurred.
    '''      Bit	Condition
    '''       1 	BIT error has occurred
    '''       2 	Image_Num_Frames out of range
    '''       3 	Coordinates of the Signal Block’s Upper left corner are greater than the coordinates of the signal block’s lower right corner 
    '''       4 	Camera_Delay_Time out of range
    '''       5 	Camera_Trigger out of range
    '''       6 	Intensity_Ratio out of range
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_LASER_FETCH")> _
    Public Sub BORESIGHT_LASER_FETCH(ByRef LBeam_Align_Coord_X As Single, ByRef LBeam_Align_Coord_Y As Single, ByRef LBeam_Area As Single, ByRef Status As Integer)
    End Sub

    ''' <summary>
    ''' This procedure determines the centroid of the Boresight Image. The BORESIGNT_TV_VIS_FETCH procedure returns the x,y coordinates of the centroid and the status. 
    ''' In addition to the common VIS setup parameters, the Intensity_Ratio parameter must be sent as the last parameter. The Intensity_Ratio is a percentage over the minimum value that determines if a blob is to be considered the boresight signal. 
    ''' This test uses target Number 1, Boresight Target. 
    ''' lSource selects configuration of the VEO2 to be used to perform this test: 1=None, 2=Blackbody, 3=Visible, 4=Vis. Align, 5=Laser, 6=Las. Align.
    ''' lNumFrames is the number of frames to average (1 - 128).
    ''' sHFieldOfView is 0 to 9999 of millirads horizontal.
    ''' sVFieldOfView is 0 to 9999 of millirads vertical.
    ''' sRadiance is µw/cm2-sr (.0005 - 5000) to set the visible lamp radiance.
    ''' lTargetPos is the Target: 0=Open,1=Pinhole,2=Pie Sector,3=5.00 Cyc/mrad 4 Bar,4=3.8325 Cyc/mrad 4 Bar,5=2.665 Cyc/mrad 4 Bar,6=1.4975 Cyc/mrad 4 Bar,7=0.33 Cyc/mrad 4 Bar,8=Diagonal Slit,9=Multi Pinhole target,10=Alignment Cross,11=USAF 1951 Groups 0-4,12=0.33 Cyc/mrad 4 Bar,13=0.66 Cyc/mrad 4 Bar,14=21 mrad square aperture.
    ''' lCenterX is 0 - 1023 center X coordinate value.
    ''' lCenterY is 0 - 1023 center X coordinate value.
    ''' lSBlockTopLeftX is top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockTopLeftY is top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockBotRightX is bottom right "x" coordinate of the Signal Block rectangle.
    ''' lSBlockBotRightY is bottom right "y" coordinate of the Signal Block rectangle.
    ''' lCameraSelection is number of camera type being used for the test. The types of cameras that can be specified are:
    ''' 0=Dummy Source,1=Laser Camera,2=RS170:640x480,3=RS343:832x624hl,4=RS343:624x624 hl,5=RS343:896x672,6=RS343:672x672,7=RS343:1080x808,8=RS343:808x808,9=RS343:1160x872,10=RS343:872x872,11=RS343:1256x944 hl,12=RS343:944x944.
    ''' sColorTemp is (0-3440) the value of the lamp color temperature 
    ''' sIntensityRatio is (0-100) a percentage over the minimum value that determines if a blob is to be considered the boresight signal.
    ''' </summary>
    ''' <param name="lSource">
    '''Integer to select configuration of the VEO2 to be used to perform this test:
    '''    Value	Configuration
    '''     1		None
    '''     2		Blackbody
    '''     3		Visible
    '''     4		Vis. Align
    '''     5		Laser
    '''     6		Las. Align
    ''' </param>
    ''' <param name="lNumFrames">
    ''' Integer	number of frames to average (1 - 128)
    ''' </param>
    ''' <param name="sHFieldOfView">
    ''' Real number 0 to 9999 of millirads horizontal
    ''' </param>
    ''' <param name="sVFieldOfView">
    ''' Real number 0 to 9999 of millirads vertical
    ''' </param>
    ''' <param name="sRadiance">
    ''' Real number μw/cm2-sr (.0005 - 5000) to set the visible lamp radiance
    ''' </param>
    ''' <param name="lTargetPos">
    ''' Integer	0 to 14	
    ''' Pos	  Target Feature(s)
    '''  0	  Open
    '''  1	  Pinhole:
    '''  2	  Pie Sector
    '''  3    5.00 Cyc/mrad  4 Bar 
    '''  4	  3.8325 Cyc/mrad  4 Bar  
    '''  5	  2.665 Cyc/mrad  4 Bar 
    '''  6	  1.4975  Cyc/mrad  4 Bar  
    '''  7	  0.33 Cyc/mrad  4 Bar 
    '''  8	  Diagonal Slit
    '''  9	  Multi Pinhole target 
    '''  10	  Alignment Cross
    '''  11	  USAF 1951, Groups 0-4
    '''  12	  0.33 Cyc/mrad  4 Bar 
    '''  13	  0.66 Cyc/mrad  4 Bar 
    '''  14	  21 mrad square aperture  
    ''' </param>
    ''' <param name="lCenterX"></param>
    ''' Center X coordinate valid 0 - 1023
    ''' <param name="lCenterY">
    ''' Center Y coordinate valid 0 - 1023
    '''</param>
    ''' <param name="lSBlockTopLeftX">
    ''' Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockTopLeftY">
    ''' Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockBotRightX">
    ''' Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lSBlockBotRightY">
    ''' Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lCameraSelection">
    ''' This number will identify which type of camera is being used for the test. The types of cameras that can be specified are:
    '''  0    Dummy Source
    '''  1	  Laser Camera
    '''  2	  RS170: 640 vl, 480 hl 
    '''  3	  RS343: 832 vl, 624 hl
    '''  4	  RS343: 624 vl, 624 hl
    '''  5	  RS343: 896 vl, 672 hl
    '''  6	  RS343: 672 vl, 672 hl
    '''  7	  RS343: 1080 vl, 808 hl
    '''  8	  RS343: 808 vl, 808 hl
    '''  9	  RS343: 1160 vl, 872 hl
    '''  10	  RS343: 872 vl, 872 hl
    '''  11   RS343: 1256 vl, 944 hl
    '''  12   RS343: 944 vl, 944 hl
    ''' </param>
    ''' <param name="sColorTemp">
    ''' Real value of the lamp color temperature (0-3440)
    ''' </param>
    ''' <param name="sIntensityRatio">
    ''' The Intensity_Ratio is a percentage over the minimum value that determines if a blob is to be considered the boresight signal.  (valid 0 - 100%)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_TV_VIS_SETUP")> _
    Public Sub BORESIGHT_TV_VIS_SETUP(lSource As Integer, lNumFrames As Integer, sHFieldOfView As Single, sVFieldOfView As Single, sRadiance As Single, lTargetPos As Integer, _
    lCenterX As Integer, lCenterY As Integer, lSBlockTopLeftX As Integer, lSBlockTopLeftY As Integer, lSBlockBotRightX As Integer, lSBlockBotRightY As Integer, _
    lCameraSelection As Integer, sColorTemp As Single, sIntensityRatio As Single)
    End Sub

    ''' <summary>
    ''' Initiates the BORESIGHT_TV_VIS test.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_TV_VIS_INITIATE")> _
    Public Sub BORESIGHT_TV_VIS_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetches the data of the completed BORESIGHT_TV_VIS test.
    ''' Boresight_X_Coord is X coordinate of the center of gravity.
    ''' Boresight_Y_Coord is Y coordinate of the center of gravity.
    ''' LBeam_Area is the number (0-32000) of pixels in the blob identified as the beam.
    ''' Status is a 16-bit word where each bit will indicate the type of failure that has occurred:1=BIT error,2=Image_Num_Frames out of range,3=H_Field_Of_View out of range,4=V_Field_Of_View out of range,5=Radiance out of range,6=Target Position out of range,7=Center X or Y coordinate out of range,8=Signal Block coordinates of Upper left corner exceed Lower right corner,9=Unknown Camera out of range,10=Color Temperature out of range,11=Intensity_Ratio out of range.
    ''' </summary>
    ''' <param name="Boresight_X_Coord">
    ''' Pointer to float - X coordinate of the center of gravity
    ''' </param>
    ''' <param name="Boresight_Y_Coord">
    ''' Pointer to float - Y coordinate of the center of gravity
    ''' </param>
    ''' <param name="LBeam_Area">
    ''' Pointer to float - Number (0-32000) of pixels in the blob identified as the beam
    ''' </param>
    ''' <param name="Status">
    ''' Pointer to int - a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Image_Num_Frames out of range
    '''    3	H_Field_Of_View out of range
    '''    4	V_Field_Of_View out of range
    '''    5	Radiance out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera out of range
    '''    10	Color Temperature out of range
    '''    11	Intensity Ratio out of range
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_TV_VIS_FETCH")> _
    Public Sub BORESIGHT_TV_VIS_FETCH(ByRef Boresight_X_Coord As Single, ByRef Boresight_Y_Coord As Single, ByRef LBeam_Area As Single, ByRef Status As Integer)
    End Sub

    ''' <summary>
    ''' This procedure determines the centroid of the Boresight Image. The BORESIGNT_IR_FETCH procedure returns the x,y coordinates of the centroid and the status. 
    ''' In addition to the common IR setup parameters, the Intensity_Ratio parameter must be sent as the last parameter. The Intensity_Ratio is a percentage over the minimum value that determines if a blob is to be considered the boresight signal. 
    ''' This test uses target Number 1, Boresight Target. 
    ''' lSource selects configuration of the VEO2 to be used to perform this test: 1=None, 2=Blackbody, 3=Visible, 4=Vis. Align, 5=Laser, 6=Las. Align.
    ''' lNumFrames is the number of frames to average (1 - 128).
    ''' sHFieldOfView is 0 to 9999 of millirads horizontal.
    ''' sVFieldOfView is 0 to 9999 of millirads vertical.
    ''' sDiffTemp is temperature differential °C from -15 to 35.
    ''' lTargetPos is the Target: 0=Open,1=Pinhole,2=Pie Sector,3=5.00 Cyc/mrad 4 Bar,4=3.8325 Cyc/mrad 4 Bar,5=2.665 Cyc/mrad 4 Bar,6=1.4975 Cyc/mrad 4 Bar,7=0.33 Cyc/mrad 4 Bar,8=Diagonal Slit,9=Multi Pinhole target,10=Alignment Cross,11=USAF 1951 Groups 0-4,12=0.33 Cyc/mrad 4 Bar,13=0.66 Cyc/mrad 4 Bar,14=21 mrad square aperture.
    ''' lCenterX is 0 - 1023 center X coordinate value.
    ''' lCenterY is 0 - 1023 center X coordinate value.
    ''' lSBlockTopLeftX is top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockTopLeftY is top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockBotRightX is bottom right "x" coordinate of the Signal Block rectangle.
    ''' lSBlockBotRightY is bottom right "y" coordinate of the Signal Block rectangle.
    ''' lCameraSelection is number of camera type being used for the test. The types of cameras that can be specified are:
    ''' 0=Dummy Source,1=Laser Camera,2=RS170:640x480,3=RS343:832x624hl,4=RS343:624x624 hl,5=RS343:896x672,6=RS343:672x672,7=RS343:1080x808,8=RS343:808x808,9=RS343:1160x872,10=RS343:872x872,11=RS343:1256x944 hl,12=RS343:944x944.
    ''' sIntensityRatio is (0-100) a percentage over the minimum value that determines if a blob is to be considered the boresight signal.
    ''' </summary>
    ''' <param name="lSource">
    '''Integer to select configuration of the VEO2 to be used to perform this test:
    '''    Value	Configuration
    '''     1		None
    '''     2		Blackbody
    '''     3		Visible
    '''     4		Vis. Align
    '''     5		Laser
    '''     6		Las. Align
    ''' </param>
    ''' <param name="lNumFrames">
    ''' Integer	number of frames to average (1 - 128)
    ''' </param>
    ''' <param name="sHFieldOfView">
    ''' Real number 0 to 9999 of millirads horizontal
    ''' </param>
    ''' <param name="sVFieldOfView">
    ''' Real number 0 to 9999 of millirads vertical
    ''' </param>
    ''' <param name="sDiffTemp">
    ''' Real number °C from -15 to 35
    ''' </param>
    ''' <param name="lTargetPos">
    ''' Integer	0 to 14	
    ''' Pos	  Target Feature(s)
    '''  0	  Open
    '''  1	  Pinhole
    '''  2	  Pie Sector
    '''  3    5.00 Cyc/mrad  4 Bar 
    '''  4	  3.8325 Cyc/mrad  4 Bar  
    '''  5	  2.665 Cyc/mrad  4 Bar 
    '''  6	  1.4975  Cyc/mrad  4 Bar  
    '''  7	  0.33 Cyc/mrad  4 Bar 
    '''  8	  Diagonal Slit
    '''  9	  Multi Pinhole target 
    '''  10	  Alignment Cross
    '''  11	  USAF 1951, Groups 0-4
    '''  12	  0.33 Cyc/mrad  4 Bar 
    '''  13	  0.66 Cyc/mrad  4 Bar 
    '''  14	  21 mrad square aperture  
    ''' </param>
    ''' <param name="lCenterX"></param>
    ''' Center X coordinate valid 0 - 1023
    ''' <param name="lCenterY">
    ''' Center Y coordinate valid 0 - 1023
    '''</param>
    ''' <param name="lSBlockTopLeftX">
    ''' Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockTopLeftY">
    ''' Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockBotRightX">
    ''' Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lSBlockBotRightY">
    ''' Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lCameraSelection">
    ''' This number will identify which type of camera is being used for the test. The types of cameras that can be specified are:
    '''  0    Dummy Source
    '''  1	  Laser Camera
    '''  2	  RS170: 640 vl, 480 hl 
    '''  3	  RS343: 832 vl, 624 hl
    '''  4	  RS343: 624 vl, 624 hl
    '''  5	  RS343: 896 vl, 672 hl
    '''  6	  RS343: 672 vl, 672 hl
    '''  7	  RS343: 1080 vl, 808 hl
    '''  8	  RS343: 808 vl, 808 hl
    '''  9	  RS343: 1160 vl, 872 hl
    '''  10	  RS343: 872 vl, 872 hl
    '''  11   RS343: 1256 vl, 944 hl
    '''  12   RS343: 944 vl, 944 hl
    ''' </param>
    ''' <param name="sIntensityRatio">
    ''' The Intensity_Ratio is a percentage over the minimum value that determines if a blob is to be considered the boresight signal.  (valid 0 - 100%)
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_IR_SETUP")> _
    Public Sub BORESIGHT_IR_SETUP(lSource As Integer, lNumFrames As Integer, sHFieldOfView As Single, sVFieldOfView As Single, sDiffTemp As Single, lTargetPos As Integer, _
    lCenterX As Integer, lCenterY As Integer, lSBlockTopLeftX As Integer, lSBlockTopLeftY As Integer, lSBlockBotRightX As Integer, lSBlockBotRightY As Integer, _
    lCameraSelection As Integer, sIntensityRatio As Single)
    End Sub

    ''' <summary>
    ''' Initiates the BORESIGHT_IR test.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_IR_INITIATE")> _
    Public Sub BORESIGHT_IR_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetches the data of the completed BORESIGHT_IR test.
    ''' Boresight_X_Coord is X coordinate (millirads) of the center of gravity.
    ''' Boresight_Y_Coord is Y coordinate (millirads) of the center of gravity.
    ''' Status is a 16-bit word where each bit will indicate the type of failure that has occurred:1=BIT error,2=Number of Image Frames out of range,3=Hor Field Of View out of range,4=Vert Field Of View out of range,5=Diff Temp out of range,6=Target Position out of range,7=Center X or Y coordinate out of range,8=Signal Block coordinates of Upper left corner exceed Lower right corner,9=Unknown Camera out of range,10=Color Temperature out of range,11=Intensity_Ratio out of range.
    ''' </summary>
    ''' <param name="Boresight_X_Coord">
    ''' Pointer to float - X coordinate of the center of gravity in millirads
    ''' </param>
    ''' <param name="Boresight_Y_Coord">
    ''' Pointer to float - Y coordinate of the center of gravity in millirads
    ''' </param>
    ''' <param name="Status">
    ''' Pointer to int - a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    '''    10	Intensity Ratio out of range
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="BORESIGHT_IR_FETCH")> _
    Public Sub BORESIGHT_IR_FETCH(ByRef Boresight_X_Coord As Single, ByRef Boresight_Y_Coord As Single, ByRef Status As Integer)
    End Sub

    ''' <summary>
    ''' This procedure calculates the Image Uniformity of a rectangular block. The calculation is based on the measurement of each pixel in the specified area. 
    ''' The mean and sigma are calculated over the range of all points. The Image Uniformity is defined as (sigma/mean * 100) and is expressed as a percentage. 
    ''' The rectangular block is specified as input parameters. The Target Position input parameter will always be 0 (Open Aperture) for this test. 
    ''' lSource selects configuration of the VEO2 to be used to perform this test: 1=None, 2=Blackbody, 3=Visible, 4=Vis. Align, 5=Laser, 6=Las. Align.
    ''' lNumFrames is the number of frames to average (1 - 128).
    ''' sHFieldOfView is 0 to 9999 of millirads horizontal.
    ''' sVFieldOfView is 0 to 9999 of millirads vertical.
    ''' sDiffTemp is temperature differential °C from -15 to 35.
    ''' lTargetPos is the Target: 0=Open,1=Pinhole,2=Pie Sector,3=5.00 Cyc/mrad 4 Bar,4=3.8325 Cyc/mrad 4 Bar,5=2.665 Cyc/mrad 4 Bar,6=1.4975 Cyc/mrad 4 Bar,7=0.33 Cyc/mrad 4 Bar,8=Diagonal Slit,9=Multi Pinhole target,10=Alignment Cross,11=USAF 1951 Groups 0-4,12=0.33 Cyc/mrad 4 Bar,13=0.66 Cyc/mrad 4 Bar,14=21 mrad square aperture.
    ''' lCenterX is 0 - 1023 center X coordinate value.
    ''' lCenterY is 0 - 1023 center X coordinate value.
    ''' lSBlockTopLeftX is top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockTopLeftY is top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockBotRightX is bottom right "x" coordinate of the Signal Block rectangle.
    ''' lSBlockBotRightY is bottom right "y" coordinate of the Signal Block rectangle.
    ''' lCameraSelection is number of camera type being used for the test. The types of cameras that can be specified are:
    ''' 0=Dummy Source,1=Laser Camera,2=RS170:640x480,3=RS343:832x624hl,4=RS343:624x624 hl,5=RS343:896x672,6=RS343:672x672,7=RS343:1080x808,8=RS343:808x808,9=RS343:1160x872,10=RS343:872x872,11=RS343:1256x944 hl,12=RS343:944x944.
    ''' </summary>
    ''' <param name="lSource">
    ''' Integer to select configuration of the VEO2 to be used to perform this test:
    '''    Value	Configuration
    '''     1		None
    '''     2		Blackbody
    '''     3		Visible
    '''     4		Vis. Align
    '''     5		Laser
    '''     6		Las. Align
    ''' </param>
    ''' <param name="lNumFrames">
    ''' Integer	number of frames to average (1 - 128)
    ''' </param>
    ''' <param name="sHFieldOfView">
    ''' Real number 0 to 9999 of millirads horizontal
    ''' </param>
    ''' <param name="sVFieldOfView">
    ''' Real number 0 to 9999 of millirads vertical
    ''' </param>
    ''' <param name="sDiffTemp">
    ''' Real number °C from -15 to 35
    ''' </param>
    ''' <param name="lTargetPos">
    ''' Integer	0 to 14	
    ''' Pos	  Target Feature(s)
    '''  0	  Open
    '''  1	  Pinhole
    '''  2	  Pie Sector
    '''  3    5.00 Cyc/mrad  4 Bar 
    '''  4	  3.8325 Cyc/mrad  4 Bar  
    '''  5	  2.665 Cyc/mrad  4 Bar 
    '''  6	  1.4975  Cyc/mrad  4 Bar  
    '''  7	  0.33 Cyc/mrad  4 Bar 
    '''  8	  Diagonal Slit
    '''  9	  Multi Pinhole target 
    '''  10	  Alignment Cross
    '''  11	  USAF 1951, Groups 0-4
    '''  12	  0.33 Cyc/mrad  4 Bar 
    '''  13	  0.66 Cyc/mrad  4 Bar 
    '''  14	  21 mrad square aperture  
    ''' </param>
    ''' <param name="lCenterX"></param>
    ''' Center X coordinate valid 0 - 1023
    ''' <param name="lCenterY">
    ''' Center Y coordinate valid 0 - 1023
    '''</param>
    ''' <param name="lSBlockTopLeftX">
    ''' Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockTopLeftY">
    ''' Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockBotRightX">
    ''' Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lSBlockBotRightY">
    ''' Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lCameraSelection">
    ''' This number will identify which type of camera is being used for the test. The types of cameras that can be specified are:
    '''  0    Dummy Source
    '''  1	  Laser Camera
    '''  2	  RS170: 640 vl, 480 hl 
    '''  3	  RS343: 832 vl, 624 hl
    '''  4	  RS343: 624 vl, 624 hl
    '''  5	  RS343: 896 vl, 672 hl
    '''  6	  RS343: 672 vl, 672 hl
    '''  7	  RS343: 1080 vl, 808 hl
    '''  8	  RS343: 808 vl, 808 hl
    '''  9	  RS343: 1160 vl, 872 hl
    '''  10	  RS343: 872 vl, 872 hl
    '''  11   RS343: 1256 vl, 944 hl
    '''  12   RS343: 944 vl, 944 hl
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="IMAGE_UNIFORMITY_IR_SETUP")> _
    Public Sub IMAGE_UNIFORMITY_IR_SETUP(lSource As Integer, lNumFrames As Integer, sHFieldOfView As Single, sVFieldOfView As Single, sDiffTemp As Single, lTargetPos As Integer, _
    lCenterX As Integer, lCenterY As Integer, lSBlockTopLeftX As Integer, lSBlockTopLeftY As Integer, lSBlockBotRightX As Integer, lSBlockBotRightY As Integer, _
    lCameraSelection As Integer)
    End Sub

    ''' <summary>
    ''' Initiates the IMAGE_UNIFORMITY_IR test.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="IMAGE_UNIFORMITY_IR_INITIATE")> _
    Public Sub IMAGE_UNIFORMITY_IR_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetches the data of the IMAGE_UNIFORMITY_IR Test. 
    ''' Image_Uniformity is a Pointer to float representing the variation from uniformity as a percentage
    ''' Status is a pointer to an integer representing a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    ''' </summary>
    ''' <param name="Image_Uniformity">
    ''' Pointer to float - Variation from uniformity as a percentage
    ''' </param>
    ''' <param name="Status">
    ''' Pointer to int - a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="IMAGE_UNIFORMITY_IR_FETCH")> _
    Public Sub IMAGE_UNIFORMITY_IR_FETCH(ByRef Image_Uniformity As Single, ByRef Status As Integer)
    End Sub

    ''' <summary>
    ''' This procedure generates an MTF curve from measurements taken across a sharp edge on a target. The measurement temperature is specified with the Differential_Temp parameter. The measurement target is set with the Target_Position parameter. 
    ''' The signal block is defined using the Signal_Block_Top_Left_X, Signal_Block_Top_Left_Y, Signal_Block_Bot_Right_X, and Signal_Block_Bot_Right_Y parameters. The defined block must crossover a target edge. For this test, the Target Position input parameter will always be 9 (Pie Sector).
    ''' lSource selects configuration of the VEO2 to be used to perform this test: 1=None, 2=Blackbody, 3=Visible, 4=Vis. Align, 5=Laser, 6=Las. Align.
    ''' lNumFrames is the number of frames to average (1 - 128).
    ''' sHFieldOfView is 0 to 9999 of millirads horizontal.
    ''' sVFieldOfView is 0 to 9999 of millirads vertical.
    ''' sDiffTemp is temperature differential °C from -15 to 35.
    ''' lTargetPos is the Target: 0=Open,1=Pinhole,2=Pie Sector,3=5.00 Cyc/mrad 4 Bar,4=3.8325 Cyc/mrad 4 Bar,5=2.665 Cyc/mrad 4 Bar,6=1.4975 Cyc/mrad 4 Bar,7=0.33 Cyc/mrad 4 Bar,8=Diagonal Slit,9=Multi Pinhole target,10=Alignment Cross,11=USAF 1951 Groups 0-4,12=0.33 Cyc/mrad 4 Bar,13=0.66 Cyc/mrad 4 Bar,14=21 mrad square aperture.
    ''' lCenterX is 0 - 1023 center X coordinate value.
    ''' lCenterY is 0 - 1023 center X coordinate value.
    ''' lSBlockTopLeftX is top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockTopLeftY is top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockBotRightX is bottom right "x" coordinate of the Signal Block rectangle.
    ''' lSBlockBotRightY is bottom right "y" coordinate of the Signal Block rectangle.
    ''' lCameraSelection is number of camera type being used for the test. The types of cameras that can be specified are:
    ''' 0=Dummy Source,1=Laser Camera,2=RS170:640x480,3=RS343:832x624hl,4=RS343:624x624 hl,5=RS343:896x672,6=RS343:672x672,7=RS343:1080x808,8=RS343:808x808,9=RS343:1160x872,10=RS343:872x872,11=RS343:1256x944 hl,12=RS343:944x944.
    ''' lOrientation is 0=Horizontal or 1=Vertical.
    ''' lPedestalFilter (value 0-100) is the minimum size of the measurement that is retained. Any measurement that is less than the Pedestal_filter is ignored.
    ''' lSmoothing (value 0-8) The smoothing parameter is used to correct mismatches between the number of sensor elements in the UUT and the number of measurement points taken by the frame capture system. Since this is rarely 1:1, a frame grabber with more resolution than the UUT can introduce a false stair step artifact.
    ''' lNumFreqPoints is the maximum number of points to report in the Fetch operation.  Current implementation requires a value of 32 here.
    ''' lCorrectionCurveIndex (value 0-3) is a correction curve based on the frequency response of the optics. This must be used for high spatial frequencies. This is an Index into a predefined array of correction curves: 0=No Curve, 1=8-12um, FAR IR, 2=3-5um, MID IR, 3=Visible.
    ''' </summary>
    ''' <param name="lSource">
    ''' Integer to select configuration of the VEO2 to be used to perform this test:
    '''    Value	Configuration
    '''     1		None
    '''     2		Blackbody
    '''     3		Visible
    '''     4		Vis. Align
    '''     5		Laser
    '''     6		Las. Align
    ''' </param>
    ''' <param name="lNumFrames">
    ''' Integer	number of frames to average (1 - 128)
    ''' </param>
    ''' <param name="sHFieldOfView">
    ''' Real number 0 to 9999 of millirads horizontal
    ''' </param>
    ''' <param name="sVFieldOfView">
    ''' Real number 0 to 9999 of millirads vertical
    ''' </param>
    ''' <param name="sDiffTemp">
    ''' Real number °C from -15 to 35
    ''' </param>
    ''' <param name="lTargetPos">
    ''' Integer	0 to 14	
    ''' Pos	  Target Feature(s)
    '''  0	  Open
    '''  1	  Pinhole
    '''  2	  Pie Sector
    '''  3    5.00 Cyc/mrad  4 Bar 
    '''  4	  3.8325 Cyc/mrad  4 Bar  
    '''  5	  2.665 Cyc/mrad  4 Bar 
    '''  6	  1.4975  Cyc/mrad  4 Bar  
    '''  7	  0.33 Cyc/mrad  4 Bar 
    '''  8	  Diagonal Slit
    '''  9	  Multi Pinhole target 
    '''  10	  Alignment Cross
    '''  11	  USAF 1951, Groups 0-4
    '''  12	  0.33 Cyc/mrad  4 Bar 
    '''  13	  0.66 Cyc/mrad  4 Bar 
    '''  14	  21 mrad square aperture  
    ''' </param>
    ''' <param name="lCenterX"></param>
    ''' Center X coordinate valid 0 - 1023
    ''' <param name="lCenterY">
    ''' Center Y coordinate valid 0 - 1023
    '''</param>
    ''' <param name="lSBlockTopLeftX">
    ''' Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockTopLeftY">
    ''' Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockBotRightX">
    ''' Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lSBlockBotRightY">
    ''' Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lCameraSelection">
    ''' This number will identify which type of camera is being used for the test. The types of cameras that can be specified are:
    '''  0    Dummy Source
    '''  1	  Laser Camera
    '''  2	  RS170: 640 vl, 480 hl 
    '''  3	  RS343: 832 vl, 624 hl
    '''  4	  RS343: 624 vl, 624 hl
    '''  5	  RS343: 896 vl, 672 hl
    '''  6	  RS343: 672 vl, 672 hl
    '''  7	  RS343: 1080 vl, 808 hl
    '''  8	  RS343: 808 vl, 808 hl
    '''  9	  RS343: 1160 vl, 872 hl
    '''  10	  RS343: 872 vl, 872 hl
    '''  11   RS343: 1256 vl, 944 hl
    '''  12   RS343: 944 vl, 944 hl
    ''' </param>
    ''' <param name="lOrientation">
    ''' lOrientation is 0=Horizontal or 1=Vertical.
    ''' </param>
    ''' <param name="lPedestalFilter">
    ''' lPedestalFilter (value 0-100) is the minimum size of the measurement that is retained. Any measurement that is less than the Pedestal_filter is ignored.
    ''' </param>
    ''' <param name="lSmoothing">
    ''' lSmoothing (value 0-8) The smoothing parameter is used to correct mismatches between the number of sensor elements in the UUT and the number of measurement points taken by the frame capture system. Since this is rarely 1:1, a frame grabber with more resolution than the UUT can introduce a false stair step artifact.
    ''' </param>
    ''' <param name="lNumFreqPoints">
    ''' lNumFreqPoints is the maximum number of points to report in the Fetch operation.  Current implementation requires a value of 3 here.
    ''' </param>
    ''' <param name="lCorrectionCurveIndex">
    ''' lCorrectionCurveIndex (value 0-3) is a correction curve based on the frequency response of the optics. This must be used for high spatial frequencies. This is an Index into a predefined array of correction curves: 0=No Curve, 1=8-12um, FAR IR, 2=3-5um, MID IR, 3=Visible.
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="MODULATION_TRANSFER_FUNCTION_IR_SETUP")> _
    Public Sub MODULATION_TRANSFER_FUNCTION_IR_SETUP(lSource As Integer, lNumFrames As Integer, sHFieldOfView As Single, sVFieldOfView As Single, sDiffTemp As Single, lTargetPos As Integer, _
    lCenterX As Integer, lCenterY As Integer, lSBlockTopLeftX As Integer, lSBlockTopLeftY As Integer, lSBlockBotRightX As Integer, lSBlockBotRightY As Integer, _
    lCameraSelection As Integer, lOrientation As Integer, lPedestalFilter As Integer, lSmoothing As Integer, lNumFreqPoints As Integer, lCorrectionCurveIndex As Integer)
    End Sub

    ''' <summary>
    ''' This function initiates the MODULATION_TRANSFER_FUNCTION_IR function.
    ''' /// </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="MODULATION_TRANSFER_FUNCTION_IR_INITIATE")> _
    Public Sub MODULATION_TRANSFER_FUNCTION_IR_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetches the data from the MODULATION_TRANSFER_FUNCTION_IR function into an array of floats freqmtf.
    ''' The first array position equals the number of frequencies to follow, which was dictated by the related SETUP function call.
    ''' The next array position equals the first frequency having an associated mtf value.
    ''' The next array position equals the mtf value associated with the first frequency.
    ''' The next array position equals the second frequency having an associated mtf value.
    ''' The next array position equals the mtf value associated with the second frequency.
    ''' The next array position equals the third frequency having an associated mtf value.
    ''' The next array position equals mtf value associated with the third frequency.
    ''' etc...etc....
    ''' Status is a pointer to an integer representing a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    '''    10	Orientation
    '''    11	Pedestal_Filter out of range
    '''    12	Smoothing out of range
    '''    13	Num_Freq_Points out of range
    '''    14	Correction Curve out of range
    ''' </summary>
    ''' <param name="freqmtf">
    ''' Pointer to float array - list of frequencies and their associated mtf values
    ''' </param>
    ''' <param name="Status">
    ''' Status is a pointer to an integer representing a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    '''    10	Orientation
    '''    11	Pedestal_Filter out of range
    '''    12	Smoothing out of range
    '''    13	Num_Freq_Points out of range
    '''    14	Correction Curve out of range
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="MODULATION_TRANSFER_FUNCTION_IR_FETCH")> _
    Public Sub MODULATION_TRANSFER_FUNCTION_IR_FETCH(freqmtf As Single(), ByRef Status As Integer)
    End Sub

    ''' <summary>
    ''' This procedure calculates Noise Equivalent Differential Temperature (NEDT) over a range of temperatures. Initially, an SiTF measurement and calculation will be performed over the 
    ''' temperature range specified by the parameters, Begin_Temp, End_Temp and Temp_Interval. This test uses target 9, Pie Sector. A single NEDT is calculated based on the specified Signal and Ambient Blocks. 
    ''' The signal block should be placed inside the Pie cutout of the target. The ambient block should be placed completely outside of the pie cutout, and should be of comparable size to the signal block.
    ''' lSource selects configuration of the VEO2 to be used to perform this test: 1=None, 2=Blackbody, 3=Visible, 4=Vis. Align, 5=Laser, 6=Las. Align.
    ''' lNumFrames is the number of frames to average (1 - 128).
    ''' sHFieldOfView is 0 to 9999 of millirads horizontal.
    ''' sVFieldOfView is 0 to 9999 of millirads vertical.
    ''' sDiffTemp is temperature differential °C from -15 to 35.
    ''' lTargetPos is the Target: 0=Open,1=Pinhole,2=Pie Sector,3=5.00 Cyc/mrad 4 Bar,4=3.8325 Cyc/mrad 4 Bar,5=2.665 Cyc/mrad 4 Bar,6=1.4975 Cyc/mrad 4 Bar,7=0.33 Cyc/mrad 4 Bar,8=Diagonal Slit,9=Multi Pinhole target,10=Alignment Cross,11=USAF 1951 Groups 0-4,12=0.33 Cyc/mrad 4 Bar,13=0.66 Cyc/mrad 4 Bar,14=21 mrad square aperture.
    ''' lCenterX is 0 - 1023 center X coordinate value.
    ''' lCenterY is 0 - 1023 center X coordinate value.
    ''' lSBlockTopLeftX is top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockTopLeftY is top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockBotRightX is bottom right "x" coordinate of the Signal Block rectangle.
    ''' lSBlockBotRightY is bottom right "y" coordinate of the Signal Block rectangle.
    ''' lCameraSelection is number of camera type being used for the test. The types of cameras that can be specified are:
    ''' 0=Dummy Source,1=Laser Camera,2=RS170:640x480,3=RS343:832x624hl,4=RS343:624x624 hl,5=RS343:896x672,6=RS343:672x672,7=RS343:1080x808,8=RS343:808x808,9=RS343:1160x872,10=RS343:872x872,11=RS343:1256x944 hl,12=RS343:944x944.
    ''' sBeginTemp is the temperature used to begin testing (from -15 to 35 °C)
    ''' sEndTemp is the last temperature used in the testing (from -15 to 35 °C)
    ''' sTempInterval is the amount of variance used to span the temperature range (.001 to 35 °C)
    ''' lAmbBlockTopLeftX is the Ambient Block rectangle X coordinate of the upper left corner.  The (0,0) point is located at the upper left corner of the entire frame.
    ''' lAmbBlockTopLeftY is the Ambient Block rectangle Y coordinate of the upper left corner.  The (0,0) point is located at the upper left corner of the entire frame.
    ''' lAmbBlockBotRightX is the Ambient Block rectangle X coordinate of the lower right corner.
    ''' lAmbBlockBotRightY is the Ambient Block rectangle Y coordinate of the lower right corner.
    ''' </summary>
    ''' <param name="lSource">
    ''' Integer to select configuration of the VEO2 to be used to perform this test:
    '''    Value	Configuration
    '''     1		None
    '''     2		Blackbody
    '''     3		Visible
    '''     4		Vis. Align
    '''     5		Laser
    '''     6		Las. Align
    ''' </param>
    ''' <param name="lNumFrames">
    ''' Integer	number of frames to average (1 - 128)
    ''' </param>
    ''' <param name="sHFieldOfView">
    ''' Real number 0 to 9999 of millirads horizontal
    ''' </param>
    ''' <param name="sVFieldOfView">
    ''' Real number 0 to 9999 of millirads vertical
    ''' </param>
    ''' <param name="sDiffTemp">
    ''' Real number °C from -15 to 35
    ''' </param>
    ''' <param name="lTargetPos">
    ''' Integer	0 to 14	
    ''' Pos	  Target Feature(s)
    '''  0	  Open
    '''  1	  Pinhole
    '''  2	  Pie Sector
    '''  3    5.00 Cyc/mrad  4 Bar 
    '''  4	  3.8325 Cyc/mrad  4 Bar  
    '''  5	  2.665 Cyc/mrad  4 Bar 
    '''  6	  1.4975  Cyc/mrad  4 Bar  
    '''  7	  0.33 Cyc/mrad  4 Bar 
    '''  8	  Diagonal Slit
    '''  9	  Multi Pinhole target 
    '''  10	  Alignment Cross
    '''  11	  USAF 1951, Groups 0-4
    '''  12	  0.33 Cyc/mrad  4 Bar 
    '''  13	  0.66 Cyc/mrad  4 Bar 
    '''  14	  21 mrad square aperture  
    ''' </param>
    ''' <param name="lCenterX"></param>
    ''' Center X coordinate valid 0 - 1023
    ''' <param name="lCenterY">
    ''' Center Y coordinate valid 0 - 1023
    '''</param>
    ''' <param name="lSBlockTopLeftX">
    ''' Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockTopLeftY">
    ''' Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockBotRightX">
    ''' Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lSBlockBotRightY">
    ''' Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lCameraSelection">
    ''' This number will identify which type of camera is being used for the test. The types of cameras that can be specified are:
    '''  0    Dummy Source
    '''  1	  Laser Camera
    '''  2	  RS170: 640 vl, 480 hl 
    '''  3	  RS343: 832 vl, 624 hl
    '''  4	  RS343: 624 vl, 624 hl
    '''  5	  RS343: 896 vl, 672 hl
    '''  6	  RS343: 672 vl, 672 hl
    '''  7	  RS343: 1080 vl, 808 hl
    '''  8	  RS343: 808 vl, 808 hl
    '''  9	  RS343: 1160 vl, 872 hl
    '''  10	  RS343: 872 vl, 872 hl
    '''  11   RS343: 1256 vl, 944 hl
    '''  12   RS343: 944 vl, 944 hl
    ''' </param>
    ''' <param name="sBeginTemp">
    ''' The temperature used to begin testing (from -15 to 35 °C)
    ''' </param>
    ''' <param name="sEndTemp">
    ''' The last temperature used in the testing (from -15 to 35 °C)
    ''' </param>
    ''' <param name="sTempInterval">
    ''' sTempInterval is the amount of variance used to span the temperature range (.001 to 35 °C)
    ''' </param>
    ''' <param name="lAmbBlockTopLeftX">
    ''' lAmbBlockTopLeftX is the Ambient Block rectangle X coordinate of the upper left corner.  The (0,0) point is located at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lAmbBlockTopLeftY">
    ''' lAmbBlockTopLeftY is the Ambient Block rectangle Y coordinate of the upper left corner.  The (0,0) point is located at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lAmbBlockBotRightX">
    ''' lAmbBlockBotRightX is the Ambient Block rectangle X coordinate of the lower right corner.
    ''' </param>
    ''' <param name="lAmblockBotRightY">
    ''' lAmbBlockBotRightY is the Ambient Block rectangle Y coordinate of the lower right corner.
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP")> _
    Public Sub NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_SETUP(lSource As Integer, lNumFrames As Integer, sHFieldOfView As Single, sVFieldOfView As Single, sDiffTemp As Single, lTargetPos As Integer, _
    lCenterX As Integer, lCenterY As Integer, lSBlockTopLeftX As Integer, lSBlockTopLeftY As Integer, lSBlockBotRightX As Integer, lSBlockBotRightY As Integer, _
    lCameraSelection As Integer, sBeginTemp As Single, sEndTemp As Single, sTempInterval As Single, lAmbBlockTopLeftX As Integer, lAmbBlockTopLeftY As Integer, _
    lAmbBlockBotRightX As Integer, lAmblockBotRightY As Integer)
    End Sub

    ''' <summary>
    ''' Initiates the NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR test. 
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_INITIATE")> _
    Public Sub NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_INITIATE()
    End Sub

    ''' <summary>
    ''' Fetches the data of the NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR Test. 
    ''' nedt is a pointer to a real returning the NEDT value in °C.
    ''' Status is a pointer to an integer representing a 16-bit word where each bit will indicate the type of failure that has occurred:1=BIT error,2=Number of Image Frames out of range,3=Hor Field Of View out of range,4=Vert Field Of View out of range,5=Diff Temp out of range,6=Target Position out of range,7=Center X or Y coordinate out of range,
    ''' 8=Signal Block coordinates of Upper left corner exceed Lower right corner,9=Unknown Camera,10=Begin_Temp > End_Temp,11=Begin_Temp out of range,12=End_Temp out of range,13=Temp_Interval out of range,14=Ambient Block out of range.
    ''' </summary>
    ''' <param name="NETD">
    ''' Pointer to float - represents the NEDT result in °C.
    ''' </param>
    ''' <param name="Status">
    ''' Pointer to int - a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    '''   10	Begin_Temp > End_Temp
    '''   11	Begin_Temp out of range
    '''   12	End_Temp out of range
    '''   13	Temp_Interval out of range
    '''   14	Ambient Block out of range
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH")> _
    Public Sub NOISE_EQUIVALENT_DIFFERENTIAL_TEMPERATURE_IR_FETCH(ByRef NETD As Single, ByRef Status As Integer)
    End Sub

    '''This procedure detects and determines the channel position of dead/noisy/strapped and flashing channels. The measurement temperature is specified with the Differential_Temp parameter. The measurement target is set with the Target_Position parameter. 
    '''The signal block is defined using the Signal_Block_Top_Left_X, Signal_Block_Top_Left_Y, Signal_Block_Bot_Right_X, and Signal_Block_Bot_Right_Y parameters. This suggested target for this test is Diagonal Slit.
    '''The returned value, Channel_List, is a list of integer value pairs. The first integer of each pair is the channel ID. The second integer is the status of that channel encoded as described in the table. Each channel contained in the signal block is listed in the Channel_List.
    ''' lSource selects configuration of the VEO2 to be used to perform this test: 1=None, 2=Blackbody, 3=Visible, 4=Vis. Align, 5=Laser, 6=Las. Align.
    ''' lNumFrames is the number of frames to average (1 - 128).
    ''' sHFieldOfView is 0 to 9999 of millirads horizontal.
    ''' sVFieldOfView is 0 to 9999 of millirads vertical.
    ''' sDiffTemp is temperature differential °C from -15 to 35.
    ''' lTargetPos is the Target: 0=Open,1=Pinhole,2=Pie Sector,3=5.00 Cyc/mrad 4 Bar,4=3.8325 Cyc/mrad 4 Bar,5=2.665 Cyc/mrad 4 Bar,6=1.4975 Cyc/mrad 4 Bar,7=0.33 Cyc/mrad 4 Bar,8=Diagonal Slit,9=Multi Pinhole target,10=Alignment Cross,11=USAF 1951 Groups 0-4,12=0.33 Cyc/mrad 4 Bar,13=0.66 Cyc/mrad 4 Bar,14=21 mrad square aperture.
    ''' lCenterX is 0 - 1023 center X coordinate value.
    ''' lCenterY is 0 - 1023 center X coordinate value.
    ''' lSBlockTopLeftX is top left "x" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockTopLeftY is top left "y" coordinate of the Signal Block rectangle. (0,0) point is located at the upper left corner of the entire frame.
    ''' lSBlockBotRightX is bottom right "x" coordinate of the Signal Block rectangle.
    ''' lSBlockBotRightY is bottom right "y" coordinate of the Signal Block rectangle.
    ''' lCameraSelection is number of camera type being used for the test. The types of cameras that can be specified are:
    ''' 0=Dummy Source,1=Laser Camera,2=RS170:640x480,3=RS343:832x624hl,4=RS343:624x624 hl,5=RS343:896x672,6=RS343:672x672,7=RS343:1080x808,8=RS343:808x808,9=RS343:1160x872,10=RS343:872x872,11=RS343:1256x944 hl,12=RS343:944x944.
    ''' linesperchannel is the number of video scan lines generated by one camera sensor channel.
    ''' linesfirstchannel is the number of video scan lines to the first camera sensor channel.
    ''' noisecriteria is the noise threshold before a channel is failed.
    ''' <param name="lSource">
    ''' Integer to select configuration of the VEO2 to be used to perform this test:
    '''    Value	Configuration
    '''     1		None
    '''     2		Blackbody
    '''     3		Visible
    '''     4		Vis. Align
    '''     5		Laser
    '''     6		Las. Align
    ''' </param>
    ''' <param name="lNumFrames">
    ''' Integer	number of frames to average (1 - 128)
    ''' </param>
    ''' <param name="sHFieldOfView">
    ''' Real number 0 to 9999 of millirads horizontal
    ''' </param>
    ''' <param name="sVFieldOfView">
    ''' Real number 0 to 9999 of millirads vertical
    ''' </param>
    ''' <param name="sDiffTemp">
    ''' Real number °C from -15 to 35
    ''' </param>
    ''' <param name="lTargetPos">
    ''' Integer	0 to 14	
    ''' Pos	  Target Feature(s)
    '''  0	  Open
    '''  1	  Pinhole
    '''  2	  Pie Sector
    '''  3    5.00 Cyc/mrad  4 Bar 
    '''  4	  3.8325 Cyc/mrad  4 Bar  
    '''  5	  2.665 Cyc/mrad  4 Bar 
    '''  6	  1.4975  Cyc/mrad  4 Bar  
    '''  7	  0.33 Cyc/mrad  4 Bar 
    '''  8	  Diagonal Slit
    '''  9	  Multi Pinhole target 
    '''  10	  Alignment Cross
    '''  11	  USAF 1951, Groups 0-4
    '''  12	  1.0 Cyc/mrad  4 Bar 
    '''  13	  0.66 Cyc/mrad  4 Bar 
    '''  14	  21 mrad square aperture  
    ''' </param>
    ''' <param name="lCenterX"></param>
    ''' Center X coordinate valid 0 - 1023
    ''' <param name="lCenterY">
    ''' Center Y coordinate valid 0 - 1023
    '''</param>
    ''' <param name="lSBlockTopLeftX">
    ''' Top left "x" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockTopLeftY">
    ''' Top left "y" coordinate of the Signal Block rectangle. (0,0) point is located 
    ''' at the upper left corner of the entire frame.
    ''' </param>
    ''' <param name="lSBlockBotRightX">
    ''' Bottom right "x" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="lSBlockBotRightY">
    ''' Bottom right "y" coordinate of the Signal Block rectangle.
    ''' </param>
    ''' <param name="linesperchannel">
    ''' linesperchannel is the number of video scan lines generated by one camera sensor channel.
    ''' </param>
    ''' <param name="linesfirstchannel">
    ''' linesfirstchannel is the number of video scan lines to the first camera sensor channel.
    ''' </param>
    ''' <param name="noisecriteria">
    ''' noisecriteria is the noise threshold before a channel is failed.
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="CHANNEL_INTEGRITY_IR_SETUP")> _
    Public Sub CHANNEL_INTEGRITY_IR_SETUP(lSource As Integer, lNumFrames As Integer, sHFieldOfView As Single, sVFieldOfView As Single, sDiffTemp As Single, lTargetPos As Integer, _
    lCenterX As Integer, lCenterY As Integer, lSBlockTopLeftX As Integer, lSBlockTopLeftY As Integer, lSBlockBotRightX As Integer, lSBlockBotRightY As Integer, _
    lCameraSelection As Integer, linesperchannel As Single, linesfirstchannel As Single, noisecriteria As Single)
    End Sub

    ''' <summary>
    ''' Initiates the Channel Integrity Test.
    ''' </summary>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="CHANNEL_INTEGRITY_IR_INITIATE")> _
    Public Sub CHANNEL_INTEGRITY_IR_INITIATE()
    End Sub

    ''' <summary>
    ''' Retrieves the data from the Channel Integrity Test.  
    ''' The returned values, Channel_List, is a pointer to a list (array) of integer value pairs. The first integer of each pair is the channel ID. The second integer is the status of that channel encoded as described in the table. Each channel contained in the signal block is listed in the Channel_List.
    ''' Status is a pointer to an integer representing a 16-bit word where each bit will indicate the type of failure that has occurred:1=BIT error,2=Number of Image Frames out of range,3=Hor Field Of View out of range,4=Vert Field Of View out of range,5=Diff Temp out of range,6=Target Position out of range,7=Center X or Y coordinate out of range,
    ''' </summary>
    ''' <param name="channel_list">
    ''' Pointer to an array of integers
    '''     Index	Contents
    '''     1		number of test channels (N)
    '''     2		First channel in area
    '''     3		Status of first channel
    '''     4		Second channel in area
    '''     5		Status of selected channel
    '''     …
    '''     2N		Last channel in area
    '''     2N+1	Status of last channel
    '''     Value encoding channel status:
    '''     Bits 0-3 (value=0x000F) set if dead
    '''     Bits 4-7 (value=0x00F0) set if noisy
    '''     Bits 8-11 (value=0x0F00) set if strap
    '''     Bits 12-15 (value=0xF000) set if flash
    ''' 	range is 0 to 65535
    ''' </param>
    ''' <param name="Status">
    ''' Pointer to int - a 16-bit word where each bit will indicate the type of failure that has occurred:
    '''   Bit	Condition
    '''    1	BIT error
    '''    2	Number of Image Frames out of range
    '''    3	Hor Field Of View out of range
    '''    4	Vert Field Of View out of range
    '''    5	Diff Temp out of range
    '''    6	Target Position out of range
    '''    7	Center X or Y coordinate out of range
    '''    8	Signal Block coordinates of Upper left corner exceed Lower right corner
    '''    9	Unknown Camera 
    '''   10	Lines_Per_Channel out of range
    '''   11	Lines_First_Channel out of range
    '''   12	Noise Critera is out of range
    ''' </param>
    <DllImport("C:\IRWin2001\VEO2.dll", EntryPoint:="CHANNEL_INTEGRITY_IR_FETCH")> _
    Public Sub CHANNEL_INTEGRITY_IR_FETCH(channel_list As Integer(), ByRef Status As Integer)
    End Sub
End Module
