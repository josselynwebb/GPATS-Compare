s??        ??   ? ɦ  %0   ?   ????                               ri4152a     RI4152A Digital Multimeter                    ? ? ??ViInt16  ?  ? ??ViInt32  ? ? ??ViReal64  ?  
?  	ViInt16 []  ?  
?  	ViInt32 []  ? ?  
ViReal64 []  ?  ? ??ViRsrc  ?  	? 	??ViSession     	?  ViChar []     ? ??ViStatus     	? 	??ViBoolean     ? 	 ViBoolean []     	? 	??ViPString     ? ??ViString  ? ? ??ViUInt16  ? ? ??ViUInt32     	?  ViInt16[]     	?  ViInt32[]     
?  	ViReal64[]     ?  ViChar[]     ? 	 
ViBoolean[]  t    Instrument: Racal 4152A 6.5 Digit Multimeter

Description: The Racal 4152A is a 6.5 Digit Multimeter which performs measurements on voltage, current, resistance, frequency, and period, among other measurements.  The Racal 4152A is a VXI C-Size module.

Minimum VXI Plug&Play Revision 4.0
Minimum Firmware Revision: 1.0
Minimum Hardware Revision:
Driver Revision: A.1.1

    G    High Level Control provides easy control over the instrument's subsystems and provides access to the most common instrument features.  For finer control or for accessing specialized instrument capabilities use Low Level Control functions.

High Level Control functions are recommended for learning how to use the instrument.      ?    Measure functions configure for, and make, a single measurement of the specified function type.  The allowable types are AC volts, DC volts, AC current, DC current, ohms, period, and frequency.     &    The voltmeter's present measurement settings can be configured with these functions. After changing the desired settings, select 'Read using Present Settings' to take the readings and bring back the data.

Additional configuration settings are available in the 'Low Level Control' functions.      ?    This selects the type of measurements to make.  There are measurements for voltage, current, resistance, period, and frequency.      A    The sample subsystem is used to set or query the sample count.      ?    The trigger subsystem is used to synchronize device action(s) with events.  The trigger subsystem provides services such as selecting trigger source, trigger delay, and trigger count.     ?    Low Level Control provides fine control over the instrument. They are used for accessing specialized instrument capabilities and provide the most sophisticated and highest performance method for controlling the instrument.

Low Level Control functions are recommended for sophisticated users who need the best performance from their instrument driver or who need to access specialized capabilities.     >    The voltmeter's present measurement settings can be configured with these functions. In order to complete a measurement, it is necessary to select 'Initiate a Measurement' followed by a 'Fetch Data from Instrument' in the Low Level Control functions or use 'Read using Present Settings' from the High Level Control.      ?    This selects the functional mode that the multimeter will be operating in. This can be period, frequency, 2 wire resistance, 4 wire resistance, DC Volts, AC Volts, DC current, and AC current.      X    This subsystem is used to configure AC Current settings such as range and resolution.      ?    This subsystem is used to configure DC Current settings such as range, resolution, aperture, and number of Power Line Cycles.      ?    This subsystem is used to configure Resistance measurement settings such as range, resolution, aperture, and number of Power Line Cycles.      V    This subsystem is used to configure AC Voltage settings such as range and aperture.      ?    This subsystem is used to configure DC Voltage settings such as range, resolution, aperture, and number of Power Line Cycles.      ]    This subsystem is used to configure Frequency settings such as aperture and voltage range.      Z    This subsystem is used to configure Period settings such as aperture and voltage range.      ?    The trigger subsystem is used to synchronize device action(s) with events.  The trigger subsystem provides services such as selecting trigger sources, trigger delays, and trigger count.          The trigger output section determines if the voltmeter complete signal should be routed to one of the VXI TTL trigger lines.      v    This sub-system allows the user to provide the multimeter with information on the bandwidth of the measured signal.      E    This subsystem is used to configure the automatic input impedance.      ?    This sub-system allows the user to enable math operations on the measurement data, such as averaging, limit checking, compensating for offsets, etc.      G    Class Name: Measure

Description: This class takes the measurements.
     ?    This subsystem performs some calibration functions.  For more complete calibration access, see the service manual for the multimeter.      d    Status functions obtain the current status of the instrument or the status of pending operations.      ?    Events indicate that something happened to the instrument. The event subsystem provides functions for reading events and clearing all events.      ?    The synchronization subsystem provides functions that are used to synchronize instrument measurements by monitoring the status of pending operations.      ?    Utility functions provide a variety of operations that affect the instrument. These include self test, reset, error handling, etc.     C    Error Handling functions deal with error conditions.  There are two types of errors - driver errors and instrument errors.  Driver errors are errors in parameters that are passed to functions.  Instrument errors are errors that occur on the instrument.  Instrument errors may not be automatically detected by the driver.      ?    This is used for automatically querying the instrument for instrument errors. Normally, the it is up to the user to manually ask the instrument if an error occurred.      ?    Timeout functions are used to set the timeout period for the driver.  The timeout value may have to be changed when taking measurements which take a long time to complete.      ?    These function are provided to aid in calibration of the instrument.  Read the service manual before proceeding with any calibration function in this section.     u    Panel Name: Initialize

The initialize function initializes the software connection to the instrument and optionally verifies that the instrument is in the system.  In addition, it may perform any necessary actions to place the instrument in its reset state.

If the ri4152a_init() function encounters an error, then the value of the vi output parameter will be VI_NULL.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".          Control Name:  Instr Descriptor

 Description:   Specifies which remote instrument
                to establish a communication
                session with.  Based on the syntax
                of the Instr Descriptor, the 
                Initialize function configures the
                I/O interface and generates an 
                Instr Handle 

 Default Value: "VXI::16::INSTR"

 Based on the Instrument Descriptor, this operation
 establishes a communication session with a device.
 The grammar for the Instrument Descriptor is shown
 below.  Optional parameters are shown in square 
 brackets ([]).  The default value is for a VXI
 interface for logical address 16.  For a GPIB-VXI interface
 with the DMM set to logical address 19, the 
 value should be:

      "GPIB-VXI::19::INSTR"


Interface Grammar
----------------------------------------------------
GPIB      GPIB[board]::primary address
          [::secondary address][::INSTR]
VXI       VXI[board]::VXI logical address[::INSTR]
GPIB-VXI  GPIB-VXI[board][::GPIB-VXI primary address]
          ::VXI logical address[::INSTR]
            
The GPIB keyword can be used with GPIB instruments.
The VXI keyword is used for VXI instruments via 
either embedded or MXIbus controllers.  
The GPIB-VXI keyword is used for a  GPIB-VXI 
controller.    

The default value for optional parameters are shown
below.

Optional Parameter          Default Value
-----------------------------------------
board                       0
secondary address           none - 31
GPIB-VXI primary address    1
     {    Control Name: id_query

if( VI_TRUE) Perform In-System Verification. if(VI_FALSE) Do not perform In-System Verification.      m    Control Name: do_reset

IF( VI_TRUE) Perform Reset operation. if(VI_FALSE) Do not perform Reset operation.     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?????       Error                             ?     ?      InstrDescriptor                    ? ?   ?       id_query                          !V ? ? ?       do_reset                          !? ? ?       Instr Handle                       	           "VXI::16::INSTR"   On VI_TRUE Off VI_FALSE   On VI_TRUE Off VI_FALSE    	            ?    Panel Name: Take Measurement

Configures the instrument to measure the specified function and takes a single measurement.  Use this routine when the default parameters are acceptable.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: func

The desired function to configure the multimeter to measure.  The allowable settings are as shown below. 

 Macro Name                 Value  Description
-----------------------------------------------------------
 ri4152a_CONF_FREQ              0  Frequency
 ri4152a_CONF_PER               1  Period
 ri4152a_CONF_FRES              2  4-Wire Resistance
 ri4152a_CONF_RES               3  2-Wire Resistance
 ri4152a_CONF_VOLT_AC           4  AC Voltage
 ri4152a_CONF_VOLT_DC           5  DC Voltage
 ri4152a_CONF_CURR_AC           6  AC Current
 ri4152a_CONF_CURR_DC           7  DC Current
 ri4152a_CONF_VOLT_RAT          8  DC Voltage Ratio
     s    Control Name: Reading

The result of the measurement is returned in this parameter.

The number of elements shoul    &?????       Error                             'V !  ?       Instr Handle                      )? / x ?      func                              ,? /A ?       reading                            	           0           	  "Frequency ri4152a_CONF_FREQ Period ri4152a_CONF_PER 4-Wire Resistance ri4152a_CONF_FRES 2-Wire Resistance ri4152a_CONF_RES AC Voltage ri4152a_CONF_VOLT_AC DC Voltage ri4152a_CONF_VOLT_DC AC Current ri4152a_CONF_CURR_AC DC Current ri4152a_CONF_CURR_DC DC Voltage Ratio ri4152a_CONF_VOLT_RAT    	          ?    Panel Name: Fetch Multiple Measurements

Description:  This routine returns readings stored in memory from the last measurement operation, and the numReadings parameter indicate show many readings were returned in the readings array.  NOTE: If a large number of readings were taken, and this call is made before the multimeter has taken all the data, a time out may occur.  You can increase the time out value with the ri4152a_timeOut function.

Execute the INITiate command before sending the FETC? command to place the multimeter in a wait for trigger state.  If the multimeter has not taken any data (i.e., if INITiate has not been executed), or if settings have been altered since the last FETCh? (i.e., changing the function or range), the "Data corrupt or stale" error will be generated.

Note: If you do not alter settings, you could "FETCh?" the same data over and over again without error.

The Multimeter's internal memory stores 512 readings maximum.

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      ?    Control Name: Readings

Description: The actual readings

The array size should be greater than or equal to the sample count.

     J    Control Name: numReadings

Description: Returns the number of readings

    3
& 	  ?       Instr Handle                      5?????       Error                             6h !  ?       Readings                          6? G ?       numReadings                        0    	           	            	           ?    Panel Name: Measurement Type Setup

Configures the instrument to measure the specified function.  Use this routine when specific multimeter parameters need to be changed from their default values (listed below).  The measurement process will not begin until the ri4152a_initImm function is called. 

After configuring the multimeter, use the INITiate command to place the multimeter in wait-for-trigger state and store readings in the multimeter's internal memory.  Or, use the READ? command to make the measurement and send readings to the output buffer when the trigger is received.

Default Settings:

FUNCTION             RANGE                RESOLUTION
-----------------------------------------------------
CURR[:DC]             1A                     1uA
CURR:AC               1A                    10uA
FREQ                  FREQ:RANG = 3 Hz      30uHz
                      VOLT:RANG = 10 V      
FRES                  1kOhm                 1 mOhm
PER                   PER:RANG = 0.333 sec  3.33 usec
                      VOLT:RANG = 10 V
RES                   1kOhm                 1 mOhm
VOLT[:DC]             10V                   10uV
VOLT[:DC]:RAT         10V                   10uV
VOLT:AC               10V                   100uV
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: Func

The desired function to configure the multimeter to measure.  The allowable settings are shown below. 

 Macro Name                 Value  Description
-----------------------------------------------------------
 ri4152a_CONF_FREQ              0  Frequency
 ri4152a_CONF_PER               1  Period
 ri4152a_CONF_FRES              2  4-Wire Resistance
 ri4152a_CONF_RES               3  2-Wire Resistance
 ri4152a_CONF_VOLT_AC           4  AC Voltage
 ri4152a_CONF_VOLT_DC           5  DC Voltage
 ri4152a_CONF_CURR_AC           6  AC Current
 ri4152a_CONF_CURR_DC           7  DC Current
 ri4152a_CONF_VOLT_RAT          8  DC Voltage Ratio
    =*????       Error                             =? "  ?       Instr Handle                      @? / ? ?      func                               	           0           	  "Frequency ri4152a_CONF_FREQ Period ri4152a_CONF_PER 4-Wire Resistance ri4152a_CONF_FRES 2-Wire Resistance ri4152a_CONF_RES AC Voltage ri4152a_CONF_VOLT_AC DC Voltage ri4152a_CONF_VOLT_DC AC Current ri4152a_CONF_CURR_AC DC Current ri4152a_CONF_CURR_DC DC Voltage Ratio ri4152a_CONF_VOLT_RAT    d    Panel Name: Measurement Type Query

Returns the current configuration settings of the multimeter.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: Func

Returns an integer representing the current configuration function.  The values are as shown below. 

 Macro Name                  Value  Description
-----------------------------------------------------------
 ri4152a_CONFQ_FREQ              0  Frequency
 ri4152a_CONFQ_PER               1  Period
 ri4152a_CONFQ_FRES              2  4-Wire Resistance
 ri4152a_CONFQ_RES               3  2-Wire Resistance
 ri4152a_CONFQ_VOLT_AC           4  AC Voltage
 ri4152a_CONFQ_VOLT_DC           5  DC Voltage
 ri4152a_CONFQ_CURR_AC           6  AC Current
 ri4152a_CONFQ_CURR_DC           7  DC Current
 ri4152a_CONFQ_VOLT_RAT          8  DC Voltage Ratio
     ?    Contorl Name: autoRange

The present setting of auto ranging is returned in this variable.  If a 1 is returned, auto ranging is enabled, if 0, it is disabled.      \    Control Name: range

The instrument's present range setting is returned in this variable.      Z    Control Name: resolution

The present resolution setting is returned in this parameter.     Es????       Error                             F( "  ?       Instr Handle                      H? / = ?       func                              Kw / ? ?       autoRange                         L  / ?       range                             L? /? ?       resolution                         	           0    	            	            	           	           P    Panel Name: Sample Count Setup

Specifies the number of readings per trigger.      ?    Control Name: Error
VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: sampCoun

Sets the number of readings per trigger. 

The allowable range is from 1 to 50000

If 50000 is specified as the value, the command executes without error.  When an INIT is executed requiring readings to be stored in internal memory, an "Insufficient memory" error is generated to show that the number of readings exceeds the memory available.  However, you can execute READ? which returns the readings to the output buffer and does not use internal memory.
    N?????       Error                             Oq "  ?       Instr Handle                      R / ? ?      sampCoun                           	           0      ?P                 \    Panel Name: Sample Count Query

This routine queries the current setting of sample count.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     j    Control Name: sampCoun

Returns the latest number of readings per trigger (values between 1 and 50000).     U0????       Error                             U? "  ?       Instr Handle                      X? / ? ?       sampCoun                           	           0    	           0    Panel Name: Trigger Setup



This routine sets up the trigger system with a single function call.  Note that each of the trigger settings in this call may also be set individually by the function of a similar name as the parameter (i.e. trigger source can also be set by function ri4152a_trigSour() ).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: Count

Specifies the trigger count.  This is the number of "bursts" of samples that will be taken.  Normally, the multimeter only has enough memory to store 512 readings.  This limits the trigger count * number of samples to be <= 512. If you require more readings than this, see the ri4152a_read_Q function for a way to get data directly from the multimeter without storing the data into memory first.  The min and max values of trigger_count are 1 and 50000 respectively.


    >    Control Name: autoDelay

Enables (1) or disables (0) selection of an automatic trigger delay.  This is the time period from receipt of the trigger until the start of the first reading.  The automatic delay varies by function, but may be changed by the user with the delay parameter when autoDelay is set to OFF (0).         Control Name: Delay

This specifies the amount of time (in seconds) to delay after receipt of the trigger before taking the first sample.  This value is ignored if trigDelAuto is ON (1).  One reason for specifying a specific delay time is to allow extra time for a relay to settle when measurement signals are being switched (multiplexed) to the multimeter.

If the sample count is > 1, this delay will also occur before EACH additional sample instead of only before the first sample.

Valid Range: 0.0 to 3600.0
     ?    Control Name: Source


Selects the trigger source, as follows. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_TRIG_SOUR_BUS            0  Bus
 ri4152a_TRIG_SOUR_EXT            1  Ext Trig BNC Connector
 ri4152a_TRIG_SOUR_IMM            2  Immediate
 ri4152a_TRIG_SOUR_TTLT0          3  TTL Trigger Line 0
 ri4152a_TRIG_SOUR_TTLT1          4  TTL Trigger Line 1
 ri4152a_TRIG_SOUR_TTLT2          5  TTL Trigger Line 2
 ri4152a_TRIG_SOUR_TTLT3          6  TTL Trigger Line 3
 ri4152a_TRIG_SOUR_TTLT4          7  TTL Trigger Line 4
 ri4152a_TRIG_SOUR_TTLT5          8  TTL Trigger Line 5
 ri4152a_TRIG_SOUR_TTLT6          9  TTL Trigger Line 6
 ri4152a_TRIG_SOUR_TTLT7         10  TTL Trigger Line 7

The TRIGger:SOURce command only selects the trigger source.   You must use the INITiate command to place the multimeter in the wait-for-trigger state.  (The MEASure command automatically executes an INITiate command.)

TRIGger:SOURce EXT uses the multimeter's front panel "Trig" BNC connector as the trigger source.  The multimeter triggers on the falling (negative-going) edge of a +/-5 V TTL input signal; (maximum input is +5V to the front panel BNC connector).

TRIGger:IMMediate causes a trigger to occur immediately provided the multimeter is placed in the wait-for-trigger state using INITiate, READ? or MEAS?

The READ? command cannot be used if the trigger source is TRIG:SOUR BUS



    Z?????       Error                             [? "  ?       Instr Handle                      ^R / G ?      count                             `F / ? ?       autoDelay                         a? /& ?      delay                             c? ? ? ? ?    source                             	           0      ?P                On VI_TRUE Off VI_FALSE ??
=p??@?              ?6?????                       ?Bus ri4152a_TRIG_SOUR_BUS Ext Trig BNC Connector ri4152a_TRIG_SOUR_EXT Immediate ri4152a_TRIG_SOUR_IMM TTL Trigger Line 0 ri4152a_TRIG_SOUR_TTLT0 TTL Trigger Line 1 ri4152a_TRIG_SOUR_TTLT1 TTL Trigger Line 2 ri4152a_TRIG_SOUR_TTLT2 TTL Trigger Line 3 ri4152a_TRIG_SOUR_TTLT3 TTL Trigger Line 4 ri4152a_TRIG_SOUR_TTLT4 TTL Trigger Line 5 ri4152a_TRIG_SOUR_TTLT5 TTL Trigger Line 6 ri4152a_TRIG_SOUR_TTLT6 TTL Trigger Line 7 ri4152a_TRIG_SOUR_TTLT7    g    Panel Name: Trigger Query

This routine queries all of the trigger system settings in a single call.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
      2    Control Name: Count

Returns the trigger count.      g    Control Name: Auto Delay

Returns the current setting of automatic trigger delay: 1 (ON) or 0 (OFF).      ?    Control Name: Delay

Returns the current setting of trigger delay.  If autoDelay is 1, then the delay returned here is the value automatically set by the multimeter; otherwise, the delay returned is the one previously set by the user.     R    Control Name: Source

Returns the current setting of trigger source.  This will be an integer that represents one of the following settings: 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_TRIG_SOUR_BUS            0  Bus
 ri4152a_TRIG_SOUR_EXT            1  Ext Trig BNC Connector
 ri4152a_TRIG_SOUR_IMM            2  Immediate
 ri4152a_TRIG_SOUR_TTLT0          3  TTL Trigger Line 0
 ri4152a_TRIG_SOUR_TTLT1          4  TTL Trigger Line 1
 ri4152a_TRIG_SOUR_TTLT2          5  TTL Trigger Line 2
 ri4152a_TRIG_SOUR_TTLT3          6  TTL Trigger Line 3
 ri4152a_TRIG_SOUR_TTLT4          7  TTL Trigger Line 4
 ri4152a_TRIG_SOUR_TTLT5          8  TTL Trigger Line 5
 ri4152a_TRIG_SOUR_TTLT6          9  TTL Trigger Line 6
 ri4152a_TRIG_SOUR_TTLT7         10  TTL Trigger Line 7
    mC????       Error                             m? "  ?       Instr Handle                      p? / = ?       count                             p? / ? ?       autoDelay                         qK / ?       delay                             r@ /? ?       source                             	           0    	            	            	           	           ?    Panel Name: Read Using Present Settings

Places the multimeter in the wait-for-trigger state and transfers readings directly to the output buffer after receiving a trigger.  Because multimeter memory is not used to store the readings, there is no restriction on the sample count and trigger count.  Because the readings are formatted before being sent to the output buffer, the sample rate is not as high as the one attainable using the ri4152a_initImm and ri4152a_fetc_Q routines.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    #    Control Name: Readings

Returns the readings taken by the multimeter.  The array pointer passed in must point to an array large enough to hold the data.  If the array is not large enough, bad things may happen as we index off the end of the array. 

The array size must be >= sample count
     E    Control Name: numReadings

Returns the number of readings fetched.     y????       Error                             y? "  ?       Instr Handle                      |c / ? ?       readings                          }? /A ?       numReadings                        	           0    	            	           $    Panel Name: Initiate Measurement

Places the multimeter in the wait-for-trigger state and stores readings in multimeter memory when a trigger occurs. The new readings replace the readings in memory from previous commands. 

After the trigger system is initiate using INITiate, use the TRIGger command subsystem to control the behavior of the trigger system.

If the TRIGger:SOURce is IMMediate, the measurement starts and readings are stored in interanl memory as soon as INITiate is executed.  Readings stored in memory from preivous commands are replaced by the new readings.

To transfer readings from memory to the output buffer, use the FETCh? command.

If TRIGger:SOURce is not IMMediate, the measurement starts as soon as a trigger is received either from the external BNC connector, the VXIbus backplane (TTLT<n> trigger lines) or a BUS trigger.

The READ? command executeds INITiate implicitly.  The MEASure command executes READ? implicitly.  Executing READ? outputs data directly to the output buffer, bypassing the multimeter's internal memory.

     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?????       Error                             ?? "  ?       Instr Handle                       	           0   ?    Panel Name: Execute Immediate Trigger

This function is used to send a trigger to the instrument.  Usually this is needed when the trigger source is set to HOLD or
BUS, but this command will force a trigger no matter what the
trigger source is set to if the instrument is in the wait-for-trigger state.

For this command to complete without error, the instrument must have been previously initiated (usually via the ri4152a_initImm function) and must be in the wait-for-trigger state.  If the instrument has already received
all of the triggers specified, or if it is not in the wait-for-trigger state, an error will occur.

/*-------------------- Prototype ---------------------*/
ViStatus ri4152a_trigImm (ViSession vi);     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ??????       Error                             ?o #  ?       Instr Handle                       	           0   ~    Panel Name: Execute Bus Trigger

This function is used to trigger the instrument when the 
trigger source is set to BUS.  The instrument must also have
been initiated previously with the ri4152a_initImm function so that it is in the wait-for-trigger state when this function is
called.

/*-------------------- Prototype ---------------------*/
ViStatus ri4152a_trg (ViSession vi);     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?????       Error                             ?? "  ?       Instr Handle                       	           0   ?    Panel Name: Abort Measurement

Aborts a measurement in progress, the trigger system is returned to the idle state.  No other settings are affected.  It is only effective when the trigger source is BUS.

Note: 

Abort does not affect any other settings of the trigger system.  When the INITiate command is sent, the trigger system will respond as it did before ABORt was executed.

ABORt returns the multimeter to the idle state for TRIG:SOUR BUS.  The "Trigger ingored" error is generated when a Group Execute Trigger bus command or *TRG common command is executed after an ABORt command (which puts the multimeter into the idle state).

     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?{????       Error                             ?0 "  ?       Instr Handle                       	           0   z    Panel Name: Fetch Data Measurement

This routine returns readings stored in memory from the last measurement operation, and the numReadings parameter indicates how many readings were returned in the readings array.  The timeOut parameter allows a time out value to be used only during this function, with the original time out value being re-stored after the data is fetched.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: timeOut

The value (in milli-seconds) to set the time out value to for the duration of this function.  When the function completes, the original time out value is re-stored. 

Valid Ranges:

1 to MaxInt.

     L    Control Name: readings

The array of double which will hold the readings.      E    Control Name: numReadings

Returns the number of readings fetched.     ??????       Error                             ?? "  ?       Instr Handle                      ?4 / c ?      timeOut                           ? / ? ?       readings                          ?m /o ?       numReadings                        	           0    ???      N            	            	            ?    Panel Name: Data Points Query

This routine returns the number of readings presently stored in memory from the last measurement operation.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     N    Control Name: numReadings

Returns the number of readings stored in memory.     ??????       Error                             ?M "  ?       Instr Handle                      ?? / ? ?       numReadings                        	           0    	            ?    Panel Name: Function Setup

This selects the measurement function consisting of 2-wire and 4-wire resistance, DC voltages, DC voltage Ratio, AC RMS voltages, DC and AC Current, frequency, and period measurements.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: func

This parameter is an integer which represents the multimeter function that is desired.  The mapping is as follows. 

 Macro Name                    Value  Description
-----------------------------------------------------------
 ri4152a_FUNC_FREQ                 0  Frequency
 ri4152a_FUNC_PER                  1  Period
 ri4152a_FUNC_FRES                 2  4-Wire Resistance
 ri4152a_FUNC_RES                  3  2-Wire Resistance
 ri4152a_FUNC_VOLT_AC              4  AC Voltage
 ri4152a_FUNC_VOLT_DC              5  DC Voltage
 ri4152a_FUNC_CURR_AC              6  AC Current
 ri4152a_FUNC_CURR_DC              7  DC Current
 ri4152a_FUNC_VOLT_DC_RAT          8  DC Voltage Ratio
    ??????       Error                             ?? "  ?       Instr Handle                      ?E / ? ?      func                               	           0           	  %Frequency ri4152a_FUNC_FREQ Period ri4152a_FUNC_PER 4-Wire Resistance ri4152a_FUNC_FRES 2-Wire Resistance ri4152a_FUNC_RES AC Voltage ri4152a_FUNC_VOLT_AC DC Voltage ri4152a_FUNC_VOLT_DC AC Current ri4152a_FUNC_CURR_AC DC Current ri4152a_FUNC_CURR_DC DC Voltage Ratio ri4152a_FUNC_VOLT_DC_RAT    ?    Panel Name: Function Query

This routine queries the current setting of the function.  Notice that if you have configured the instrument for Temperature, the function returned is either RES or FRES depending on the transducer type selected.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: Func

Returns an integer representing the current function setting.  The mapping is as follows. 

 Macro Name                    Value  Description
-----------------------------------------------------------
 ri4152a_FUNC_FREQ                 0  Frequency
 ri4152a_FUNC_PER                  1  Period
 ri4152a_FUNC_FRES                 2  4-Wire Resistance
 ri4152a_FUNC_RES                  3  2-Wire Resistance
 ri4152a_FUNC_VOLT_AC              4  AC Voltage
 ri4152a_FUNC_VOLT_DC              5  DC Voltage
 ri4152a_FUNC_CURR_AC              6  AC Current
 ri4152a_FUNC_CURR_DC              7  DC Current
 ri4152a_FUNC_VOLT_DC_RAT          8  DC Voltage Ratio
    ??????       Error                             ?? "  ?       Instr Handle                      ?K / ? ?       func                               	           0    	            _    Panel Name: Configure AC Current

Description: This sets the DMM mode to measure AC current.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ? +  ?       Instr Handle                      ??????       Error                              0    	           Y    Panel Name: AC Current Range Setup

Selects the range for the AC current measurements.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: autoRange

Enables (1) or disables (0) the auto ranging feature.      ?    Control Name: Range

Sets the AC current range, as follows. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_CURR_AC_RANG_1A        1.0  1.0
 ri4152a_CURR_AC_RANG_3A        3.0  3.0
    ?X????       Error                             ? "  ?       Instr Handle                      ?? / ? ?       autoRange                         ? /^ ?       range                              	           0   On VI_TRUE Off VI_FALSE  8  1.0 ri4152a_CURR_AC_RANG_1A 3.0 ri4152a_CURR_AC_RANG_3A    `    Panel Name: AC Current Range Query
This routine queries the current setting of current range.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      E    Control Name: range

Returns the present AC current range setting.     ??????       Error                             ?n "  ?       Instr Handle                      ? / ? ?       autoRange                         Ő /A ?       range                              	           0    	            	           }    Panel Name: AC Current Resolution Setup

Selects the resolution for AC current measurements. 

Must select the range first
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    Y    Control Name: resolution

Specifies the resolution for AC current measurements.  Note that resolution is a function of range; thus 100 microAmps of resolution is only possible on the lowest range, and if selected on a higher range will result in an error from the instrument.  See the table below for the settings possible.


 Macro Name                         Value  Description
-----------------------------------------------------------
 ri4152a_CURR_AC_RES_1_MICRO       1.0e-6  1.0e-6 (For range = 1 A)
 ri4152a_CURR_AC_RES_3_MICRO       3.0e-6  3.0e-6 (For range = 3 A)
 ri4152a_CURR_AC_RES_10_MICRO     10.0e-6  10.0e-6 (For range = 1 A)
 ri4152a_CURR_AC_RES_30_MICRO     30.0e-6  30.0e-6 (For range = 3 A)
 ri4152a_CURR_AC_RES_100_MICRO   100.0e-6  100.0e-6 (For range = 1 A)
 ri4152a_CURR_AC_RES_300_MICRO   300.0e-6  300.0e-6 (For range = 3 A)
    ?_????       Error                             ? "  ?       Instr Handle                      ʽ /U ?       resolution                         	           0              J1.0e-6 (For range = 1 A) ri4152a_CURR_AC_RES_1_MICRO 3.0e-6 (For range = 3 A) ri4152a_CURR_AC_RES_3_MICRO 10.0e-6 (For range = 1 A) ri4152a_CURR_AC_RES_10_MICRO 30.0e-6 (For range = 3 A) ri4152a_CURR_AC_RES_30_MICRO 100.0e-6 (For range = 1 A) ri4152a_CURR_AC_RES_100_MICRO 300.0e-6 (For range = 3 A) ri4152a_CURR_AC_RES_300_MICRO    k    Panel Name: AC Current Resolution Query

This routine queries the present setting of current resolution.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     C    Control Name: Resolution

Returns current AC current resolution.     К????       Error                             ?O "  ?       Instr Handle                      ?? / ? ?       resolution                         	           0    	           _    Panel Name: Configure DC Current

Description: This sets the DMM mode to measure DC current.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?f +  ?       Instr Handle                      ?????       Error                              0    	           y    Panel Name: DC Current Range Setup

Selects a specific range for DC current measurements or turns auto ranging on/off.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: autoRange

Enables (1) or disables (0) the auto ranging feature.     ?    Control Name: range

Sets the DC current range to the nearest range >= to the level specified.  The actual current ranges are 10 milliAmps, 100 milliAmps, 1 Amp, and 3 Amps. 

 Macro Name                      Value  Description
-----------------------------------------------------------
 ri4152a_CURR_DC_RANG_10MA     10.0e-3  10.0e-3
 ri4152a_CURR_DC_RANG_100MA   100.0e-3  100.0e-3
 ri4152a_CURR_DC_RANG_1A           1.0  1.0
 ri4152a_CURR_DC_RANG_3A           3.0  3.0
    ??????       Error                             ?u "  ?       Instr Handle                      ? / ? ?       autoRange                         ?w / ?       range                              	           0   On VI_TRUE Off VI_FALSE              ~10.0e-3 ri4152a_CURR_DC_RANG_10MA 100.0e-3 ri4152a_CURR_DC_RANG_100MA 1.0 ri4152a_CURR_DC_RANG_1A 3.0 ri4152a_CURR_DC_RANG_3A    ?    Panel Name: DC Current Range Query

This routine queries the present setting of DC current range, and whether auto ranging is enabled or not.      ?    Control Name: ERror
VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?    Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      ?    Control Name: range

Returns the currently used DC current range.  The possible return values are 0.01 Amps, 0.1 Amps, 1 Amp, and 3 Amps.     ?????       Error                             ?3.   ?       Instr Handle                      ?? / ? ?       autoRange                         ?T /A ?       range                              	           0    	            	          $    Panel Name: DC Current Resolution Setup

Selects the resolution for DC current measurements.

Note: Must change range first.

See also the ri4152a_currDcAper and ri4152a_currDcNplc functions because changing resolution affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    	?    Control Name: resolution

Specifies the resolution for DC current measurements.  Note that resolution is really determined by aperture or NPLC settings, and so the minimum aperture on A GIVEN RANGE determines the resolution; thus 10 nanoVolts of resolution is only possible on the lowest range, and if selected on a higher range will result in an error from the instrument.  See the table below for the settings possible.

Also, selecting a resolution changes the aperture and NPLC settings, see the second table below for the interactions of Range, Resolution, Aperture, and NPLCs.

 Macro Name                       Value     Description
---------------------------------------------------------------
 ri4152a_CURR_DC_RES_3_NANO       3.0e-9    For range = 100e-3 A
 ri4152a_CURR_DC_RES_10_NANO      10.0e-9   For range = 100e-3 A
 ri4152a_CURR_DC_RES_30_NANO      30.0e-9   For range <= 10e-3 A
 ri4152a_CURR_DC_RES_100_NANO     100.0e-9  For range <= 10e-3 A
 ri4152a_CURR_DC_RES_300_NANO     300.0e-9  For range <= 1 A
 ri4152a_CURR_DC_RES_900_NANO     900.0e-9  For range = 3A
 ri4152a_CURR_DC_RES_1_MICRO      1.0e-6    For range <= 3A
 ri4152a_CURR_DC_RES_3_MICRO      3.0e-6    For range = 1A, 3A
 ri4152a_CURR_DC_RES_9_MICRO      9.0e-6    For range = 3A
 ri4152a_CURR_DC_RES_10_MICRO     10.0e-6   For range = 100mA,1A
 ri4152a_CURR_DC_RES_30_MICRO     30.0e-6   For range = 3A
 ri4152a_CURR_DC_RES_100_MICRO    100.0e-6  For range = 1A
 ri4152a_CURR_DC_RES_300_MICRO    300.0e-6  For range = 3A

In the following table, resolution is shown as a function of
range and integration time given in Power Line Cycles (PLCs).
The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 10 mA|   3 nA  |  10 nA |  30 nA | 100 nA   |    1 uA  |
100 mA|  30 nA  | 100 nA | 300 nA |   1 uA   |   10 uA  |
  1 A | 300 nA  |   1 uA |   3 uA |  10 uA   |  100 uA  |
  3 A | 900 nA  |   3 uA |   9 uA |  30 uA   |  300 uA  |
---------------------------------------------------------
      |                Resolution                       |
      ---------------------------------------------------


    ?????       Error                             ?? "  ?       Instr Handle                      ?o 0 ? ?     resolution                      ???? M 3??                                         ???? c b??                                            	           0             ?3.0e-9 (For range = 10e-3 A) ri4152a_CURR_DC_RES_3_NANO 10.0e-9 (For range = 10e-3 A) ri4152a_CURR_DC_RES_10_NANO 30.0e-9 (For range <= 100e-3 A) ri4152a_CURR_DC_RES_30_NANO 100.0e-9 (For range <= 100e-3 A) ri4152a_CURR_DC_RES_100_NANO 300.0e-9 (For range <= 1 A) ri4152a_CURR_DC_RES_300_NANO 900.0e-9 (For range = 3A) ri4152a_CURR_DC_RES_900_NANO 1.0e-6 For range <= 1A) ri4152a_CURR_DC_RES_1_MICRO 3.0e-6 (for ranges =1A or 3A) ri4152a_CURR_DC_RES_3_MICRO 9.0e-6 (for range = 3A) ri4152a_CURR_DC_RES_9_MICRO 10.0e-6 (For range = 100mA and 1A) ri4152a_CURR_DC_RES_10_MICRO 30.0e-6 (For3A only) ri4152a_CURR_DC_RES_30_MICRO 100.0e-6 (For range = 1A) ri4152a_CURR_DC_RES_100_MICRO 300.0e-6 (For range = 3A) ri4152a_CURR_DC_RES_300_MICRO    =WARNING: Changing Resolution also changes NPLC and Aperture!    6See Help/Control for more specifics on this coupling.    ?    Panel Name: DC Current Resolution Query

This routine queries the present setting of current resolution.

Reset Condition: None      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     C    Control Name: Resolution

Returns current DC current resolution.     ?E????       Error                             ?? "  ?       Instr Handle                      ?? % ? ?       resolution                         	           0    	          B    Panel Name: DC Current Aperture Setup

Sets the integration time in seconds for DC current measurements. The multimeter rounds values UP to the nearest time.

See also the ri4152a_currDcRes and ri4152a_currDcNplc functions because changing aperture affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    G    Control Name: Aperture

Sets the integration time in seconds (aperture) for resistance measurements. Input values are rounded UP to the nearest aperture time shown in the table below.

Aperture is one of three ways to set the resolution of the reading.  The other two are resRes and resNplc.  The relationships between range, resolution, aperture and NPLC's (Number Power Line Cycles) is shown below.

Aperture is determined by the NPLC setting; for example, for 1 power line cycle of 60 Hz power, the aperture is 16.7 milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power source were 50 Hz instead of 60 Hz, the above numbers would be 20.0 milliseconds and 200 milliseconds respectively.

In the following table, resolution is shown as a function of range and integration time given in Power Line Cycles (PLCs).  The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
   1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
  10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
   1M | 300e-3  |   1    |   3    |  10      |  100     |
  10M |   3     |  10    |  30    | 100      |    1e3   |
 100M |  30     | 100    | 300    |   1e3    |   10e3   |
---------------------------------------------------------
 Ohms |          Resolution  Ohms                       |
---------------------------------------------------------
    ??????       Error                             ?    ?       Instr Handle                    ???? M 3??                                         ???? c e??                                          R & ? ?      aperture                           	           0    =WARNING: Changing Aperture also changes NPLC and Resolution!    6See Help/Control for more specifics on this coupling.              U1.66667 s 1.66667e0 166.667 ms 1.66667e-1 16.6667 ms 1.66e-2 3 ms 3e-3 0.400 ms 4e-4    j    Panel Name: DC Current Aperture Query

This routine queries the present setting of DC current aperture.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     k    Control Name: aperture

Returns current integration time setting in seconds for DC current measurements.    
????       Error                            ? "  ?       Instr Handle                     h / ? ?       aperture                           	           0    	          J    Panel Name: DC Current NPLC Setup

Sets the integration time in Power Line Cycles (PLC) for DC current measurements. The multimeter rounds values UP to the nearest time.

See also the ri4152a_currDcRes and ri4152a_currDcAper functions because changing NPLC affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: nplc

Sets the integration time in Power Line Cycles (PLCs) for DC current measurements.  Input values are rounded UP to the nearest aperture time shown in the table below.

NPLC is one of three ways to set the resolution of the reading.  The other two are currDcRes and currDcAper.  The relationships between range, resolution, aperture and NPLC's (Number Power Line Cycles) is shown below.

Aperture is determined by the NPLC setting; for example, for 1 power line cycle of 60 Hz power, the aperture is 16.7 milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power source were 50 Hz instead of 60 Hz, the above numbers would be 20.0 milliseconds and 200 milliseconds respectively.

In the following table, resolution is shown as a function of range and integration time given in Power Line Cycles (PLCs).  The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 10 mA|   3 nA  |  10 nA |  30 nA | 100 nA   |    1 uA  |
100 mA|  30 nA  | 100 nA | 300 nA |   1 uA   |   10 uA  |
  1 A | 300 nA  |   1 uA |   3 uA |  10 uA   |  100 uA  |
  3 A | 900 nA  |   3 uA |   9 uA |  30 uA   |  300 uA  |
---------------------------------------------------------
      |                Resolution                       |
      ---------------------------------------------------
   ?????       Error                            ? "  ?       Instr Handle                     G / ? ?      nplc                            ???? M 3??                                         ???? c b??                                            	           0 ??y??4	@Y      ??z?G?{@$                    =WARNING: Changing NPLC also changes Aperture and Resolution!    6See Help/Control for more specifics on this coupling.    ?    Panel Name: DC Current NPLC Query


This routine queries the present setting of number of power line cycles integration time (NPLC's).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     t    Control Name: nplc

Returns the present integration time in power line cycles (PLCs) for DC current measurements.    <????       Error                            ? "  ?       Instr Handle                     "? / ? ?       nplc                               	           0    	           i    Panel Name: Configure Four-Wire Res

Description: This sets the DMM mode to measure 4-wire resistance

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"    $C +  ?       Instr Handle                     &?????       Error                              0    	           h    Panel Name: Configure Two-Wire Res

Description: This sets the DMM mode to measure 2-wire resistance

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"    (? +  ?       Instr Handle                     +5????       Error                              0    	           s    Panel Name: Resistance Range Setup

Selects a specific range for ohms measurements or turns auto ranging on/off.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: AutoRange

Enables (1) or disables (0) the auto ranging feature.     _    Control Name: range

Sets the resistance range to the nearest range >= to the level specified.  The actual resistance ranges are 100 Ohms, 1 kOhms, 10 kOhms, 100 kOhms, 1 MOhms, 10 MOhms, and 100 MOhms. 

 Macro Name                 Value  Description
-----------------------------------------------------------
 ri4152a_RES_RANG_100         100  100.0
 ri4152a_RES_RANG_1K        1.0e3  1.0e3
 ri4152a_RES_RANG_10K      10.0e3  10.0e3
 ri4152a_RES_RANG_100K    100.0e3  100.0e3
 ri4152a_RES_RANG_1M        1.0e6  1.0e6
 ri4152a_RES_RANG_10M      10.0e6  10.0e6
 ri4152a_RES_RANG_100M    100.0e6  100.0e6
   ,?????       Error                            -? "  ?       Instr Handle                     0> / ? ?       autoRange                        0? /| ?       range                              	           0   On VI_TRUE Off VI_FALSE              ?100.0 ri4152a_RES_RANG_100 1.0e3 ri4152a_RES_RANG_1K 10.0e3 ri4152a_RES_RANG_10K 100.0e3 ri4152a_RES_RANG_100K 1.0e6 ri4152a_RES_RANG_1M 10.0e6 ri4152a_RES_RANG_10M 100.0e6 ri4152a_RES_RANG_100M    ?    Panel Name: Resistance Range Query

This routine queries the current setting of resistance range, and whether auto ranging is enabled or not.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      ?    Control Name: range

Returns the currently used resistance range.  The possible return values are 100 Ohms, 1 kOhms, 10 kOhms, 100 kOhms, 1 MOhms, 10 MOhms, and 100 MOhms.

   5h????       Error                            6 "  ?       Instr Handle                     8? / ? ?       autoRange                        9? /A ?       range                              	           0    	            	           ?    Panel Name: Resistance Resolution Setup

Selects the resolution for resistance measurements.

See also the ri4152a_resAper and ri4152a_resNplc functions because changing resolution affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    s    Control Name: resolution

Specifies the resolution for resistance measurements.  Note that resolution is really determined by aperture or NPLC settings, and so the minimum aperture on A GIVEN RANGE determines the resolution; thus 10 nanoVolts of resolution is only possible on the lowest range, and if selected on a higher range will result in an error from the instrument.  See the table below for the settings possible.

Also, selecting a resolution changes the aperture and NPLC settings, see the second table below for the interactions of Range, Resolution, Aperture, and NPLCs.

 Macro Name                Value          Description
---------------------------------------------------------------
 ri4152a_RES_RES_30_MICRO  30.0e-6        For range = 100 Ohms
 ri4152a_RES_RES_100_MICRO 100.0e-6       For range <= 1 kOhms
 ri4152a_RES_RES_1_MILLI   1.0e-3         For range <= 1 kOhms
 ri4152a_RES_RES_3_MILLI   3.0e-3         For range <= 10kOhms
 ri4152a_RES_RES_10_MILLI  10.0e-3        For range >= 1 kOhm
                                                    <= 10 KOhm
 ri4152a_RES_RES_30_MILLI  30.0e-3        For range = 10 KOhms,
                                                      100 kOhms,
 ri4152a_RES_RES_100_MILLI 100.0e-3       For range >= 1 KOhm,
                                                    <= 100 KOhm,
 ri4152a_RES_RES_300_MILLI 300.0e-3       For range = 1 KOhm,
                                                      100 kOhm,
                                                      1 MOhm
 ri4152a_RES_RES_1         1.0            For range >= 10 KOhm,
                                                    <= 1 MOhm
 ri4152a_RES_RES_3         3.0            For range = 1 MOhm,
                                                      10 MOhm,
 ri4152a_RES_RES_10        10.0           For range >= 100 kOhm,
                                                    <= 10 MOhm
 ri4152a_RES_RES_30        30.0           For range = 10 MOhm, 
                                                    = 100 MOhm
 ri4152a_RES_RES_100       100.0          For range >= 1 MOhm
 ri4152a_RES_RES_300       300.0          For range = 100 MOhm
 ri4152a_RES_RES_1K        1.0e3          For range >= 10 MOhm
 ri4152a_RES_RES_10K       10.0e3         For range = 100 MOhm


In the following table, resolution is shown as a function of
range and integration time given in Power Line Cycles (PLCs).
The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
   1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
  10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
   1M | 300e-3  |   1    |   3    |  10      |  100     |
  10M |   3     |  10    |  30    | 100      |    1e3   |
 100M |  30     | 100    | 300    |   1e3    |   10e3   |
---------------------------------------------------------
 Ohms |          Resolution  Ohms                       |
---------------------------------------------------------
   ;?????       Error                            <? "  ?       Instr Handle                     ?W / ? ?     resolution                      ???? M 3??                                         ???? c b??                                            	           0             ?30.0e-6 (For range = 100 Ohms) ri4152a_RES_RES_30_MICRO 100.0e-6 (For range = 100 Ohms) ri4152a_RES_RES_100_MICRO 300.0e-6 (For range <= 1 kOhms) ri4152a_RES_RES_300_MICRO 1.0e-3 (For range <= 1 kOhms) ri4152a_RES_RES_1_MILLI 3.0e-3 (For range 1KOhm, 10 kOhm) ri4152a_RES_RES_3_MILLI 10.0e-3 (For range <= 10 kOhms) ri4152a_RES_RES_10_MILLI 30.0e-3 (For range= 10 kOhms, 100 Ohms) ri4152a_RES_RES_30_MILLI 100.0e-3 (For range = 1 kOhm, 10 kOhm, 100 kOhm) ri4152a_RES_RES_100_MILLI 300.0e-3 (For range = 1 KOhm, 100 kOhm, 1 MOhm) ri4152a_RES_RES_300_MILLI 1.0 (For range = 10 kOhm, 100 kOhm, 1 MOhm) ri4152a_RES_RES_1 3.0 (For range = 1MOhm, 10 MOhm) ri4152a_RES_RES_3 10.0 (For range >= 100 kHz) ri4152a_RES_RES_10 30.0 (For ranges >= 10 MOhm) ri4152a_RES_RES_30 100.0 (For ranges = 1 MOhm, 10 MOhm) ri4152a_RES_RES_100 300.0 (For 100 MOhm Range) ri4152a_RES_RES_300 1.0e3 (For ranges >= 10 MOhm) ri4152a_RES_RES_1K 10.0e3 (For range = 100 MOhm) ri4152a_RES_RES_10K    =WARNING: Changing Resolution also changes NPLC and Aperture!    6See Help/Control for more specifics on this coupling.    n    Panel Name: Resistance Resolution Query

This routine queries the current setting of resistance resolution.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     C    Control Name: Resolution

Returns current resistance resolution.    R?????       Error                            Se "  ?       Instr Handle                     V / ? ?       resolution                         	           0    	          ;    Panel Name: Resistance Aperture Setup

Sets the integration time in seconds for resistance measurements. The multimeter rounds values UP to the nearest time.

See also theri4152a_resRes and ri4152a_resNplc functions because changing aperture affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    G    Control Name: Aperture

Sets the integration time in seconds (aperture) for resistance measurements. Input values are rounded UP to the nearest aperture time shown in the table below.

Aperture is one of three ways to set the resolution of the reading.  The other two are resRes and resNplc.  The relationships between range, resolution, aperture and NPLC's (Number Power Line Cycles) is shown below.

Aperture is determined by the NPLC setting; for example, for 1 power line cycle of 60 Hz power, the aperture is 16.7 milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power source were 50 Hz instead of 60 Hz, the above numbers would be 20.0 milliseconds and 200 milliseconds respectively.

In the following table, resolution is shown as a function of range and integration time given in Power Line Cycles (PLCs).  The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
   1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
  10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
   1M | 300e-3  |   1    |   3    |  10      |  100     |
  10M |   3     |  10    |  30    | 100      |    1e3   |
 100M |  30     | 100    | 300    |   1e3    |   10e3   |
---------------------------------------------------------
 Ohms |          Resolution  Ohms                       |
---------------------------------------------------------
   XX????       Error                            Y "  ?       Instr Handle                     [? * ? ?      aperture                        ???? M 3??                                         ???? c b??                                            	           0              V1.66667 s 1.66667e0 166.667 ms 1.66667e-1 16.6667 ms 1.666e-2 3 ms 3e-3 0.400 ms 4e-4    =WARNING: Changing Aperture also changes NPLC and Resolution!    6See Help/Control for more specifics on this coupling.    j    Panel Name: Resistance Aperture Query

This routine queries the current setting of resistance aperture.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     k    Control Name: Aperture

Returns current integration time setting in seconds for resistance measurements.    eo????       Error                            f$ "  ?       Instr Handle                     h? / ? ?       aperture                           	           0    	          C    Panel Name: Resistance NPLC Setup

Sets the integration time in Power Line Cycles (PLC) for resistance measurements. The multimeter rounds values UP to the nearest time. See also the ri4152a_resRes and ri4152a_resAper functions because changing NPLC affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    2    Sets the integration time in Power Line Cycles (PLCs) for resistance measurements.  Input values are rounded UP to the nearest aperture time shown in the table below.

NPLC is one of three ways to set the resolution of the reading.  The other two are resRes and resAper.  The relationships between range, resolution, aperture and NPLC's (Number Power Line Cycles) is shown below.

Aperture is determined by the NPLC setting; for example, for 1 power line cycle of 60 Hz power, the aperture is 16.7 milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power source were 50 Hz instead of 60 Hz, the above numbers would be 20.0 milliseconds and 200 milliseconds respectively.

In the following table, resolution is shown as a function of range and integration time given in Power Line Cycles (PLCs).  The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
   1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
  10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
   1M | 300e-3  |   1    |   3    |  10      |  100     |
  10M |   3     |  10    |  30    | 100      |    1e3   |
 100M |  30     | 100    | 300    |   1e3    |   10e3   |
---------------------------------------------------------
 Ohms |          Resolution  Ohms                       |
---------------------------------------------------------
   kG????       Error                            k? "  ?       Instr Handle                     n? / ? ?      nplc                            ???? M 3??                                         ???? c b??                                            	           0 ??y??4	@Y      ??z?G?{@$                    =WARNING: Changing NPLC also changes Aperture and Resolution!    6See Help/Control for more specifics on this coupling.    ?    Panel Name: Resistance NPLC Query

This routine queries the current setting of resistance number of power line cycles integration time (NPLC's).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     w    Control Name: nplc

Returns the current integration time in power line cycles (PLCs) for DC resistance measurements.    x8????       Error                            x? "  ?       Instr Handle                     {? / ? ?       nplc                               	           0    	           _    Panel Name: Configure AC Voltage

Description: This sets the DMM mode to measure AC Voltage

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"    }8 +  ?       Instr Handle                     ?????       Error                              0    	           W    Panel Name: AC Voltage Range Setup

Selects the range for the AC volts measurements.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: autoRange

Enables (1) or disables (0) the auto ranging feature.     ?    Control Name: range

Sets the AC voltage range. The allowable ranges are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_VOLT_RANG_100MV   100.0e-3  100.0e-3
 ri4152a_VOLT_RANG_1V           1.0  1.0
 ri4152a_VOLT_RANG_10V         10.0  10.0
 ri4152a_VOLT_RANG_100V       100.0  100.0
 ri4152a_VOLT_RANG_300V       300.0  300.0
   ?p????       Error                            ?% "  ?       Instr Handle                     ?? / ? ?       autoRange                        ?' / ?       range                              	           0   On VI_TRUE Off VI_FALSE              ?100.0e-3 ri4152a_VOLT_RANG_100MV 1.0 ri4152a_VOLT_RANG_1V 10.0 ri4152a_VOLT_RANG_10V 100.0 ri4152a_VOLT_RANG_100V 300.0 ri4152a_VOLT_RANG_300V    ?    Panel Name: AC Voltage Range Query

This routine queries the current setting of voltage range and whether auto ranging is enabled or not.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      E    Control Name: Range

Returns the current AC voltage range setting.    ?-????       Error                            ?? "  ?       Instr Handle                     ?? / ? ?       autoRange                        ? /A ?       range                              	           0    	            	           _    Panel Name: AC Voltage Resolution Setup

Selects the resolution for AC voltage measurements.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
        Control Name: resolution

Specifies the resolution for AC voltage measurements.  Note that the resolution is a function of range; thus 100 nanoVolts of resolution is only possible on the lowest range, and if selected on a higher range will result in an error from the instrument.  See the table below for the settings possible.

-----------------------------------
Range |           Resolutions     |
-----------------------------------
100 mV| 100 nV  |   1 uV |  10 uV |
  1 V |   1 uV  |  10 uV | 100 uV |
 10 V |  10 uV  | 100 uV |   1 mV |
100 V | 100 uV  |   1 mV |  10 mV |
300 V |   1 mV  |  10 mV | 100 mV |
-----------------------------------

 Macro Name                         Value  Description
-----------------------------------------------------------
 ri4152a_VOLT_AC_RES_100_NANO    100.0e-9  100.0e-9 (For range = 100e-3 V)
 ri4152a_VOLT_AC_RES_1_MICRO       1.0e-6  1.0e-6 (For range <= 1 V)
 ri4152a_VOLT_AC_RES_10_MICRO     10.0e-6  10.0e-6 (For range <= 10 V)
 ri4152a_VOLT_AC_RES_100_MICRO   100.0e-6  100.0e-6 (For range >= 1V and <= 100 V)
 ri4152a_VOLT_AC_RES_1_MILLI       1.0e-3  1.0e-3 (For range >= 10V)
 ri4152a_VOLT_AC_RES_10_MILLI     10.0e-3  10.0e-3 (For range >= 100 V)
 ri4152a_VOLT_AC_RES_100_MILLI   100.0e-3  100.0e-3 (For range = 300V)
   ??????       Error                            ?j "  ?       Instr Handle                     ? /d ?       resolution                         	           0             ?100.0e-9 (For range = 100e-3 V) ri4152a_VOLT_AC_RES_100_NANO 1.0e-6 (For range <= 1 V) ri4152a_VOLT_AC_RES_1_MICRO 10.0e-6 (For range <= 10 V) ri4152a_VOLT_AC_RES_10_MICRO 100.0e-6 (For range >= 1 V ) ri4152a_VOLT_AC_RES_100_MICRO 1.0e-3 (For Range >= 10 V) ri4152a_VOLT_AC_RES_1_MILLI 10.0e-3 (For range >= 100V) ri4152a_VOLT_AC_RES_10_MILLI 100.0e-3 (For range >= 300V) ri4152a_VOLT_AC_RES_100_MILLI    n    Panel Name: AC Voltage Resolution Query

This routine queries the current setting of AC voltage resolution.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     C    Control Name: Resolution

Returns current AC voltage resolution.    ??????       Error                            ?? "  ?       Instr Handle                     ?A / ? ?       resolution                         	           0    	           _    Panel Name: Configure DC Voltage

Description: This sets the DMM mode to measure DC Voltage

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"    ?? +  ?       Instr Handle                     ?X????       Error                              0    	           k    Panel Name: Configure DC Voltage Ratio

Description: This sets the DMM mode to measure DC Voltage Ratio

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"    ?? +  ?       Instr Handle                     ??????       Error                              0    	           w    Panel Name: DC Voltage Range Setup

Selects a specific range for DC volts measurements or turns auto ranging on/off.      ?    control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: autoRange

Enables (1) or disables (0) the auto ranging feature.         Control Name: range

Sets the DC voltage range to the nearest range >= to the level specified.  The actual voltage ranges are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_VOLT_RANG_100MV   100.0e-3  100.0e-3
 ri4152a_VOLT_RANG_1V           1.0  1.0
 ri4152a_VOLT_RANG_10V         10.0  10.0
 ri4152a_VOLT_RANG_100V       100.0  100.0
 ri4152a_VOLT_RANG_300V       300.0  300.0
   ?S????       Error                            ? "  ?       Instr Handle                     ?? / ? ?       autoRange                        ?
 / ?       range                              	           0   On VI_TRUE Off VI_FALSE              ?100.0e-3 ri4152a_VOLT_RANG_100MV 1.0 ri4152a_VOLT_RANG_1V 10.0 ri4152a_VOLT_RANG_10V 100.0 ri4152a_VOLT_RANG_100V 300.0 ri4152a_VOLT_RANG_300V    ?    Panel Name: DC Voltage Range Query

This routine queries the current setting of DC voltage range, and whether auto ranging is enabled or not.      ?    Control Name: error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      ?    Control Name: range

Returns the currently used DC voltage range.  The possible return values are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts.    ?I????       Error                            ?? "  ?       Instr Handle                     ?? / ? ?       autoRange                        ?  /A ?       range                              	           0    	            	              Panel Name: DC Voltage Resolution Setup

Selects the resolution for DC voltage measurements.

See also the ri4152a_voltDcAper and ri4152a_voltDcNplc functions because changing resolution affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    
j    Control Name: Resolution

Specifies the resolution for DC voltage measurements.  Note that resolution is really determined by aperture or NPLC settings, and so the minimum aperture on A GIVEN RANGE determines the resolution; thus 10 nanoVolts of resolution is only possible on the lowest range, and if selected on a higher range will result in an error from the instrument.  See the table below for the settings possible.

Also, selecting a resolution changes the aperture and NPLC settings, see the second table below for the interactions of Range, Resolution, Aperture, and NPLCs.

 Macro Name                  Value      Description
---------------------------------------------------------------
 ri4152a_VOLT_DC_RES_30_NANO   30.0e-9     For range = 100e-3 V
 ri4152a_VOLT_DC_RES_100_NANO  100.0e-9    For range = 100e-3 V
 ri4152a_VOLT_DC_RES_300_NANO  300.0e-9    For range <= 1 V
 ri4152a_VOLT_DC_RES_1_MICRO   1.0e-6      For range <= 1 V
 ri4152a_VOLT_DC_RES_3_MICRO   3.0e-6      For range = 1V, 10V
 ri4152a_VOLT_DC_RES_10_MICRO  10.0e-6     For range <= 10V
 ri4152a_VOLT_DC_RES_30_MICRO  30.0e-6     For range = 10V,100 V
 ri4152a_VOLT_DC_RES_100_MICRO   100.0e-6  For range >= 1V,
                                                     <= 100V
 ri4152a_VOLT_DC_RES_300_MICRO   300.0e-6  For range = 100V,
                                                       300V
 ri4152a_VOLT_DC_RES_1_MILLI     1.0e-3    For range >= 10V
 ri4152a_VOLT_DC_RES_3_MILLI     3.0e-3    For range = 300V
 ri4152a_VOLT_DC_RES_10_MILLI    10.0e-3   For range >= 100V
 ri4152a_VOLT_DC_RES_100_MILLI   100.0e-3  For range = 300V

In the following table, resolution is shown as a function of
range and integration time given in Power Line Cycles (PLCs).
The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
100 mV|  30 nV  | 100 nV | 300 nV |   1 uV   |   10 uV  |
  1 V | 300 nV  |   1 uV |   3 uV |  10 uV   |  100 uV  |
 10 V |   3 uV  |  10 uV |  30 uV | 100 uV   |    1 mV  |
100 V |  30 uV  | 100 uV | 300 uV |   1 mV   |   10 mV  |
300 V | 300 uV  |   1 mV |   3 mV |  10 mV   |  100 mV  |
---------------------------------------------------------
      |                Resolution                       |
      ---------------------------------------------------

   ??????       Error                            ?? "  ?       Instr Handle                     ?. / ? ?     resolution                      ???? M 3??                                         ???? c b??                                            	           0             ?30.0e-9 (For range = 100e-3 V) ri4152a_VOLT_DC_RES_30_NANO 100.0e-9 (For range = 100e-3 V) ri4152a_VOLT_DC_RES_100_NANO 300.0e-9 (For range <= 1 V) ri4152a_VOLT_DC_RES_300_NANO 1.0e-6 (For range <= 1 V) ri4152a_VOLT_DC_RES_1_MICRO 3.0e-6 (For range 1V, 10V) ri4152a_VOLT_DC_RES_3_MICRO 10.0e-6 (For range <= 10 V) ri4152a_VOLT_DC_RES_10_MICRO 30.0e-6 (For range 10V, 100V) ri4152a_VOLT_DC_RES_30_MICRO 100.0e-6 (For range >= 1V & <= 100V) ri4152a_VOLT_DC_RES_100_MICRO 300.0e-6 (For range >= 100V) ri4152a_VOLT_DC_RES_300_MICRO 1.0e-3 (For ranges >= 10V) ri4152a_VOLT_DC_RES_1_MILLI 3.0e-3 (For range = 300V) ri4152a_VOLT_DC_RES_3_MILLI 10.0e-3 (For range >= 100V) ri4152a_VOLT_DC_RES_10_MILLI 100.0e-3 (For range = 300V)) ri4152a_VOLT_DC_RES_100_MILLI    =WARNING: Changing Resolution also changes NPLC and Aperture!    6See Help/Control for more specifics on this coupling.    k    Panel Name: DC Voltage Resolution Query

This routine queries the current setting of voltage resolution.      ?    VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     C    Control Name: Resolution

Returns current DC voltage resolution.    Ȧ????       Error                            ?F "  ?       Instr Handle                     ?? / ? ?       resolution                         	           0    	          B    Panel Name: DC Voltage Aperture Setup

Sets the integration time in seconds for DC voltage measurements. The multimeter rounds values UP to the nearest time.

See also the ri4152a_voltDcRes and ri4152a_voltDcNplc functions because changing aperture affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    G    Control Name: Aperture

Sets the integration time in seconds (aperture) for resistance measurements. Input values are rounded UP to the nearest aperture time shown in the table below.

Aperture is one of three ways to set the resolution of the reading.  The other two are resRes and resNplc.  The relationships between range, resolution, aperture and NPLC's (Number Power Line Cycles) is shown below.

Aperture is determined by the NPLC setting; for example, for 1 power line cycle of 60 Hz power, the aperture is 16.7 milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power source were 50 Hz instead of 60 Hz, the above numbers would be 20.0 milliseconds and 200 milliseconds respectively.

In the following table, resolution is shown as a function of range and integration time given in Power Line Cycles (PLCs).  The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
 100  |  30e-6  | 100e-6 | 300e-6 |   1e-3   |   10e-3  |
   1k | 300e-6  |   1e-3 |   3e-3 |  10e-3   |  100e-3  |
  10k |   3e-3  |  10e-3 |  30e-3 | 100e-3   |    1     |
 100k |  30e-3  | 100e-3 | 300e-3 |   1      |   10     |
   1M | 300e-3  |   1    |   3    |  10      |  100     |
  10M |   3     |  10    |  30    | 100      |    1e3   |
 100M |  30     | 100    | 300    |   1e3    |   10e3   |
---------------------------------------------------------
 Ohms |          Resolution  Ohms                       |
---------------------------------------------------------
   ?@????       Error                            ??    ?       Instr Handle                    ???? M 3??                                         ???? c b??                                          ў 0 ? ?      aperture                           	           0    =WARNING: Changing Aperture also changes NPLC and Resolution!    6See Help/Control for more specifics on this coupling.              U1.66667 s 1.66667e0 166.667 ms 1.66667e-1 16.6667 ms 1.66e-2 3 ms 3e-3 0.400 ms 4e-4    j    Panel Name: DC Voltage Aperture Query

This routine queries the current setting of DC voltage aperture.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     k    Control Name: Aperture

Returns current integration time setting in seconds for DC voltage measurements.    ?V????       Error                            ? "  ?       Instr Handle                     ޴ / ? ?       aperture                           	           0    	          J    Panel Name: DC Voltage NPLC Setup

Sets the integration time in Power Line Cycles (PLC) for DC voltage measurements. The multimeter rounds values UP to the nearest time.

See also the ri4152a_voltDcRes and ri4152a_voltDcAper functions because changing NPLC affects the setting on those two functions as well (they are coupled).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: nplc

Sets the integration time in Power Line Cycles (PLCs) for DC voltage measurements.  Input values are rounded UP to the nearest aperture time shown in the table below.

NPLC is one of three ways to set the resolution of the reading.  The other two are voltDcRes and voltDcAper.  The relationships between range, resolution, aperture and NPLC's (Number Power Line Cycles) is shown below.

Aperture is determined by the NPLC setting; for example, for 1 power line cycle of 60 Hz power, the aperture is 16.7 milliseconds, for 10 PLCs, it is 167 milliseconds.  If the power source were 50 Hz instead of 60 Hz, the above numbers would be 20.0 milliseconds and 200 milliseconds respectively.

In the following table, resolution is shown as a function of range and integration time given in Power Line Cycles (PLCs).  The associated aperture is shown for 60 Hz power.

      --------------------------------------------------|
      | Integration time in Power Line Cycles (PLCs)    |
      | Aperture for 60 Hz power                        |
      --------------------------------------------------
------| 100 PLCs| 10 PLCs|  1 PLC |  0.2 PLC |  0.02 PLC|
Range | 1.67s   | 167ms  | 16.7ms |  3.00ms  |  0.400ms |
---------------------------------------------------------
100 mV|  30 nV  | 100 nV | 300 nV |   1 uV   |   10 uV  |
  1 V | 300 nV  |   1 uV |   3 uV |  10 uV   |  100 uV  |
 10 V |   3 uV  |  10 uV |  30 uV | 100 uV   |    1 mV  |
100 V |  30 uV  | 100 uV | 300 uV |   1 mV   |   10 mV  |
300 V | 300 uV  |   1 mV |   3 mV |  10 mV   |  100 mV  |
---------------------------------------------------------
      |                Resolution                       |
      ---------------------------------------------------
   ?5????       Error                            ?? '  ?       Instr Handle                     ?? / ? ?      nplc                            ???? M 3??                                         ???? c b??                                            	           0 ??y??4	@Y      ??z?G?{??                    =WARNING: Changing NPLC also changes Aperture and Resolution!    6See Help/Control for more specifics on this coupling.    ?    Panel Name: DC Voltage NPLC Query

This routine queries the current setting of voltage number of power line cycles integration time (NPLC's).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     t    Control Name: nplc

Returns the current integration time in power line cycles (PLCs) for DC voltage measurements.    ??????       Error                            ?~ "  ?       Instr Handle                     ?' / ? ?       nplc                               	           0    	           W    Panel Name: Configure Freq

Description: This sets the DMM mode to measure frequency
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"    ?? +  ?       Instr Handle                     ?g????       Error                              0    	           ?    Panel Name: Frequency Aperture Setup

Sets the integration time in seconds for frequency measurements. The multimeter rounds values UP to the nearest time.      ?    Control Name: error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: aperture

Sets the integration time in seconds (aperture) for frequency measurements. The allowable settings are 0.01, 0.1, and 1 seconds.

   ?=????       Error                            ?? "  ?       Instr Handle                     ?? / ? ?      aperture                           	           0 ????kw??      ??z?G?{????????              h    Panel Name: Frequency Aperture Query

This routine queries the current setting of frequency aperture.      ?    Control Name: error
VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"          The handle to the instrument      l    Control Name: aperture



Returns current integration time setting in seconds for frequency measurements.    ??????       Error                            ?> "  ?       Instr Handle                     ?e / ? ?       aperture                           	           0    	           ?    Panel Name: Frequency Volt. Range Setup

Selects a specific voltage range for frequency measurements, or turns auto ranging on/off.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: autoRange

Enables (1) or disables (0) the auto ranging feature.          Control Name: Range

Sets the frequency range to the nearest range >= to the level specified.  The actual voltage ranges are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_VOLT_RANG_100MV   100.0e-3  100.0e-3
 ri4152a_VOLT_RANG_1V           1.0  1.0
 ri4152a_VOLT_RANG_10V         10.0  10.0
 ri4152a_VOLT_RANG_100V       100.0  100.0
 ri4152a_VOLT_RANG_300V       300.0  300.0
   ?#????       Error                            ?? "  ?       Instr Handle                     ? / ? ?       autoRange                        ? / ?       range                              	           0   On VI_TRUE Off VI_FALSE              ?100.0e-3 ri4152a_VOLT_RANG_100MV 1.0 ri4152a_VOLT_RANG_1V 10.0 ri4152a_VOLT_RANG_10V 100.0 ri4152a_VOLT_RANG_100V 300.0 ri4152a_VOLT_RANG_300V    ?    Panel Name: Frequency Volt. Range Query

This routine queries the current setting of frequency voltage range, and whether auto ranging is enabled or not.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      ?    Control Name: range

Returns the currently used frequency voltage range.  The possible return values are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts.    $????       Error                            ? "  ?       Instr Handle                     
? / ? ?       autoRange                        
? /A ?       range                              	           0    	            	           W    Panel Name: Configure Period

Description: This sets the DMM mode to measure period.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     +  ?       Instr Handle                     ?????       Error                              0    	           ?    Panel Name: Period Aperture Setup

Sets the integration time in seconds for period measurements. The multimeter rounds values UP to the nearest time.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: aperture

Sets the integration time in seconds (aperture) for period measurements. The allowable settings are 0.01, 0.1, and 1 seconds.

   ~????       Error                            3 "  ?       Instr Handle                     ? / ? ?      aperture                           	           0 ????kw??      ??z?G?{????????              b    Panel Name: Period Aperture Query

This routine queries the current setting of period aperture.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     O    Returns current integration time setting in seconds for period measurements.    ?????       Error                            w "  ?       Instr Handle                       / ? ?       aperture                           	           0    	           ?    Panel Name: Period Volt. Range Setup

Selects a specific voltage range for period measurements, or turns auto ranging on/off.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: autoRange

Enables (1) or disables (0) the auto ranging feature.     ?    Control Name: range

Sets the period range to the nearest range >= to the level specified.  The actual voltage ranges are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_VOLT_RANG_100MV   100.0e-3  100.0e-3
 ri4152a_VOLT_RANG_1V           1.0  1.0
 ri4152a_VOLT_RANG_10V         10.0  10.0
 ri4152a_VOLT_RANG_100V       100.0  100.0
 ri4152a_VOLT_RANG_300V       300.0  300.0
   ?????       Error                            p "  ?       Instr Handle                      / ? ?       autoRange                        r / ?       range                              	           0   On VI_TRUE Off VI_FALSE              ?100.0e-3 ri4152a_VOLT_RANG_100MV 1.0 ri4152a_VOLT_RANG_1V 10.0 ri4152a_VOLT_RANG_10V 100.0 ri4152a_VOLT_RANG_100V 300.0 ri4152a_VOLT_RANG_300V    ?    Panel Name: Period Volt. Range Query

This routine queries the current setting of period voltage range, and whether auto ranging is enabled or not.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     q    Control Name: autoRange

Returns the current setting: enabled (1) or disabled (0) of the auto ranging feature.      ?    Control Name: range

Returns the currently used period voltage range.  The possible return values are 100 millivolts, 1 Volt, 10 Volts, 100 Volts and 300 Volts.    #?????       Error                            $h "  ?       Instr Handle                     ' / ? ?       autoRange                        '? /A ?       range                              	           0    	            	          ?    Panel Name: Trigger Count Setup

Specifies the trigger count.  This is the number of "bursts" of samples that will be taken.  Normally, the multimeter only has enough memory to store 4096 readings.  This limits the trigger count * number of samples to be <= 4096. If you require more readings than this, see the ri4152a_read_Q function for a way to get data directly from the multimeter without storing the data into memory first.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     X    Control Name: trigCoun

Sets the number of triggers issued. 

Valid Range: 1 to 50000
   *?????       Error                            +? "  ?       Instr Handle                     .I / ? ?      trigCoun                           	           0      ?P                 ^    Panel Name: Trigger Count Query

This routine queries the current setting of trigger count.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     D    Control Name: trigCoun

Returns the number of trigger counts set.    /?????       Error                            0? "  ?       Instr Handle                     34 / ? ?       trigCoun                           	           0    	            ?    Panel Name: Trigger Delay Setup

Sets delay period between receipt of trigger and start of measurement.  One use of this would be to specify a longer delay to give a signal extra time to settle after a relay closure before measurement.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: trigDelAuto

Enables (1) or disables (0) the auto delay feature.      ?    Control Name: trigDel

Sets delay period between receipt of trigger and start of measurement.  This value is ignored and not used if the trigDelAuto parameter is enabled (1). 

Valid Ranges: 0 to 3600.0
   52????       Error                            5? "  ?       Instr Handle                     8? / ? ?       trigDelAuto                      8? /K ?      trigDel                            	           0   On VI_TRUE Off VI_FALSE ??
=p??@?              ?6?????              ^    Panel Name: Trigger Delay Query

This routine queries the current setting of trigger delay.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     J    Control Name: autoDelay

Returns current setting of auto delay feature.      =    Control Name: delay

Returns current trigger delay period.    ;O????       Error                            < "  ?       Instr Handle                     >? / ? ?       autoDelay                        >? /A ?       delay                              	           0    	            	           f    Panel Name: Trigger Source Setup

Selects the trigger source to which the multimeter is to respond.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
        Control Name: trigSour

Selects the trigger source, as follows. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_TRIG_SOUR_BUS            0  Bus
 ri4152a_TRIG_SOUR_EXT            1  Ext Trig BNC Connector
 ri4152a_TRIG_SOUR_IMM            2  Immediate
 ri4152a_TRIG_SOUR_TTLT0          3  TTL Trigger Line 0
 ri4152a_TRIG_SOUR_TTLT1          4  TTL Trigger Line 1
 ri4152a_TRIG_SOUR_TTLT2          5  TTL Trigger Line 2
 ri4152a_TRIG_SOUR_TTLT3          6  TTL Trigger Line 3
 ri4152a_TRIG_SOUR_TTLT4          7  TTL Trigger Line 4
 ri4152a_TRIG_SOUR_TTLT5          8  TTL Trigger Line 5
 ri4152a_TRIG_SOUR_TTLT6          9  TTL Trigger Line 6
 ri4152a_TRIG_SOUR_TTLT7         10  TTL Trigger Line 7
   @?????       Error                            Ad "  ?       Instr Handle                     D / ? ? ?    trigSour                           	           0             ?Bus ri4152a_TRIG_SOUR_BUS Ext Trig BNC Connector ri4152a_TRIG_SOUR_EXT Immediate ri4152a_TRIG_SOUR_IMM TTL Trigger Line 0 ri4152a_TRIG_SOUR_TTLT0 TTL Trigger Line 1 ri4152a_TRIG_SOUR_TTLT1 TTL Trigger Line 2 ri4152a_TRIG_SOUR_TTLT2 TTL Trigger Line 3 ri4152a_TRIG_SOUR_TTLT3 TTL Trigger Line 4 ri4152a_TRIG_SOUR_TTLT4 TTL Trigger Line 5 ri4152a_TRIG_SOUR_TTLT5 TTL Trigger Line 6 ri4152a_TRIG_SOUR_TTLT6 TTL Trigger Line 7 ri4152a_TRIG_SOUR_TTLT7    d    Panel Name: Trigger Source Query

This routine queries the current setting of the trigger source.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    #    Control Name: trigSour

Returns an integer representing one of the following trigger sources. 

 Macro Name                   Value  Description
-----------------------------------------------------------
 ri4152a_TRIG_SOUR_BUS            0  Bus
 ri4152a_TRIG_SOUR_EXT            1  Ext Trig BNC Connector
 ri4152a_TRIG_SOUR_IMM            2  Immediate
 ri4152a_TRIG_SOUR_TTLT0          3  TTL Trigger Line 0
 ri4152a_TRIG_SOUR_TTLT1          4  TTL Trigger Line 1
 ri4152a_TRIG_SOUR_TTLT2          5  TTL Trigger Line 2
 ri4152a_TRIG_SOUR_TTLT3          6  TTL Trigger Line 3
 ri4152a_TRIG_SOUR_TTLT4          7  TTL Trigger Line 4
 ri4152a_TRIG_SOUR_TTLT5          8  TTL Trigger Line 5
 ri4152a_TRIG_SOUR_TTLT6          9  TTL Trigger Line 6
 ri4152a_TRIG_SOUR_TTLT7         10  TTL Trigger Line 7
   J????       Error                            J? "  ?       Instr Handle                     Mb / ? ?       trigSour                           	           0    	            ?    Panel Name: Trigger Output Setup


Enables or disables routing of the "Voltmeter Complete" signal to the specified VXIbus trigger line on the backplane P2 connector.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     5    Control Name: ttltLine

Specifies the TTLtrg line.      y    Control Name: ttltState

OFF (0) disables or ON (1) enables "Voltmeter Complete" pulse for the specified trigger line.    Q?????       Error                            R? "  ?       Instr Handle                     UW / ? ?      ttltLine                         U? /] ?       ttltState                          	           0                          On VI_TRUE Off VI_FALSE    ?    Panel Name: Trigger Output Query

This routine queries the current state of the specified TTL trigger line.  A 1 indicates the line is enabled to route the "Voltmeter Complete" pulse; a 0 indicates the line is disabled.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     K    Control Name: ttltLine

Specifies the TTLTrg trigger line to be queried.      [    Control Name: ttltState

Returns 0 if "Voltmeter Complete" is disabled, or 1 if enabled.    X????       Error                            X? "  ?       Instr Handle                     [p / ? ?      ttltLine                         [? /A ?       ttltState                          	           0                          	           ?    Panel Name: Detector Bandwidth Setup

Selects the multimeter's fast, medium and slow modes for AC voltage, frequency, or period measurements.

For signal frequencies between 3.0 and less than 20.0 Hz, the slow (3 Hz) filter is chosen.  For frequencies >= 20 Hz and < 200 Hz, the medium speed (20 Hz) filter is chosen.  For frequencies >= 200 Hz, the fast (200 Hz) filter is chosen.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     Q    Control Name: detBand

The expected signal frequency. 

Valid Range: 3 to 200

   ^?????       Error                            _k "  ?       Instr Handle                     b / ? ?      detBand                            	           0 @=??V?ϫAO?    @      @4                    ?    Panel Name: Detector Bandwidth Query

This routine queries the current setting of detector bandwidth.  The possible settings are 3, 20, or 200.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     `    Control Name: detBand

Returns the detector bandwidth.  This will be either 3, 20, or 200 Hz.    c?????       Error                            d? "  ?       Instr Handle                     g@ / ? ?       detBand                            	           0    	          G    Panel Name: Auto Impedance Setup

Enables or disables the automatic input impedance mode for DC voltage measurements. When disabled (0 or OFF), the multimeter maintains its input impedance of 10 Mohms for all DC voltage ranges. This prevents an input impedance change (caused by changing ranges) from affecting measurements.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
      W    Control Name: inpImpAuto

Selects the automatic input impedance for DC measurements.    i?????       Error                            jh !  ?       Instr Handle                     m /  ?       inpImpAuto                         	           0   On VI_TRUE Off VI_FALSE    [    Panel Name: Auto Impedance Query

This routine queries the current setting of inpImpAuto      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     g    Control Name: impImpAuto

Returns OFF (0) if input impedance mode is disabled or ON (1) if enabled..    n?????       Error                            oT "  ?       Instr Handle                     q? / ? ?       inpImpAuto                         	           0    	           ?    Panel Name: Function Setup

Selects the desired calculate function.  Only one function may be enabled at a time.  The calculate function does not become active until enabled with the ri4152a_calcStat routine.  Once enabled, the calculate function remains in effect until it is disabled via the ri4152a_calcStat routine, the measurement function is changed, a reset is performed, or power is cycled.  See the Comments below for more information about particular function settings.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: calcFunc

An integer which determines the current calc function.  The mapping is as follows. 

 Macro Name                  Value  Description
-----------------------------------------------------------
 ri4152a_CALC_FUNC_NULL          0  Null Offset
 ri4152a_CALC_FUNC_AVER          1  Average
 ri4152a_CALC_FUNC_LIM           2  Limit
 ri4152a_CALC_FUNC_DBM           3  dBm
 ri4152a_CALC_FUNC_DB            4  dB
   u????       Error                            u? "  ?       Instr Handle                     xp /+ ?       calcFunc                           	           0               ?Null Offset ri4152a_CALC_FUNC_NULL Average ri4152a_CALC_FUNC_AVER Limit ri4152a_CALC_FUNC_LIM dBm ri4152a_CALC_FUNC_DBM dB ri4152a_CALC_FUNC_DB    b    Panel Name: Function Query

This routine queries the current setting of the calculate function.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: calcFuncReturns the current setting of the calculate function.  This will be one of the following. 

 Macro Name                  Value  Description
-----------------------------------------------------------
 ri4152a_CALC_FUNC_NULL          0  Null Offset
 ri4152a_CALC_FUNC_AVER          1  Average
 ri4152a_CALC_FUNC_LIM           2  Limit
 ri4152a_CALC_FUNC_DBM           3  DBM
 ri4152a_CALC_FUNC_DB            4  DB
   {?????       Error                            |? "  ?       Instr Handle                     ? / ? ?       calcFunc                           	           0    	            ?    Panel Name: State Setup

Enables or disables the multimeter's calculate function.  When disabled, no calculations are performed.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     U    Control Name: calcStat

ON (1) enables or OFF (0) disables the calculate function.    ?D????       Error                            ?? "  ?       Instr Handle                     ?? /  ?       calcStat                           	           0   On VI_TRUE Off VI_FALSE    ]    Panel Name: State Query

This routine queries the current state of the calculate function.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     r    Control Name: calcStat

Returns the current state of the calculate function; 0 (if disabled) or 1 (if enabled).    ?/????       Error                            ?? "  ?       Instr Handle                     ?? / ? ?       calcStat                           	           0    	           ?    Panel Name: Average Query

This routine returns some statistical data on all readings taken since the AVERage function was enabled.  The data returned are the average value of readings, the minimum reading, the maximum reading, and the number of readings taken.  Each of these data parameters is individually available as well through a separate function call (i.e. the minimum value can be obtained by calling the function ri4152a_averMin_Q ).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Average

Returns the average of all readings taken since the calculate function was set to AVERage and enabled.      ?    Control Name: minValue

Returns the minimum value of all readings taken since the calculate function was set to AVERage and enabled.      ?    Control Name: maxValue

Returns the maximum value of all readings taken since the calculate function was set to AVERage and enabled.      ?    Control Name: points

Returns the number of readings taken since the calculate function was set to AVERage and enabled.  This is the number used to obtain the average.    ??????       Error                            ?? "  ?       Instr Handle                     ?? / = ?       average                          ?p / ? ?       minValue                         ?? / ?       maxValue                         ?? /? ?       points                             	           0    	           	           	           	            ?    Panel Name: Calculate Average of Readings

Description: This routine returns the average of all readings taken since the calculate function was set to average and enabled.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      A    Control Name: Average

Description: This returns the average.

   ?u   ?       Instr Handle                     ?????       Error                            ?? * ? ?       Average                            0    	           	           ?    Panel Name: Calculate Number of Readings

Description: This returns the number of readings taken since the calculate function was set to average and enabled.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      &    This returns the number of readings
   ??   ?       Instr Handle                     ?)????       Error                            ?? % ? ?       Readings                           0    	           	            ?    Panel Name: Calculate Max Readings

Description: This funciton calculates the maximum of readings since the calculate function was set to Average and enabled


    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      V    Control Name: Readings

Description: This returns the maximum value of all readings
   ?r*   ?       Instr Handle                     ?????       Error                            ?? $ ? ?       Readings                           0    	           	           ?    Panel Name: Calculate Min of Readings

Description: Returns the minimum value of all readings taken since the calculate function was set to Average and enabled.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      L    Control Name: readings

Description: Returns the minimum of all readings.
   ??$   ?       Instr Handle                     ?>????       Error                            ?? * ? ?       Readings                           0    	           	           ?    Panel Name: dB Reference Setup

This function sets the dB reference value for use when the calculate function is DB.

This value is used only for measurement functions of DC and AC voltage.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     b    Control Name: calcDbRef

Sets the dB reference value for use when the calculate function is DB.    ??????       Error                            ?? "  ?       Instr Handle                     ?) / ? ?      calcDbRef                          	           0 ??z?G?{@i      ?i                            ^    Panel Name: dB Reference Query

This routine queries the current value of the dB reference.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     o    Control Name: calcDbRef

Returns the current value of the dB reference value used by the calculate function.    ??????       Error                            ?? "  ?       Instr Handle                     ?2 / ? ?       calcDbRef                          	           0    	           ?    Panel Name: dbM Reference Setup

This function sets the dBm reference resistor value for use when the calculate function is DBM.  The dBm calculate function, when enabled, will calculate the power delivered to a resistance referenced to 1 milliWatt.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ^    Control Name: calcDbmRef

Sets the dBm reference resistor value for use when the calculate function is DBM.  The allowable resistor values are: 50, 75, 93, 110, 124, 125, 135, 150, 250, 300, 500, 600, 800, 900, 1000, 1200, and 8000 Ohms. 

 Macro Name                     Value  Description
-----------------------------------------------------------
 ri4152a_CALC_DBM_REF_50         50.0  50.0
 ri4152a_CALC_DBM_REF_75         75.0  75.0
 ri4152a_CALC_DBM_REF_93         93.0  93.0
 ri4152a_CALC_DBM_REF_110       110.0  110.0
 ri4152a_CALC_DBM_REF_124       124.0  124.0
 ri4152a_CALC_DBM_REF_125       125.0  125.0
 ri4152a_CALC_DBM_REF_135       135.0  135.0
 ri4152a_CALC_DBM_REF_150       150.0  150.0
 ri4152a_CALC_DBM_REF_250       250.0  250.0
 ri4152a_CALC_DBM_REF_300       300.0  300.0
 ri4152a_CALC_DBM_REF_500       500.0  500.0
 ri4152a_CALC_DBM_REF_600       600.0  600.0
 ri4152a_CALC_DBM_REF_800       800.0  800.0
 ri4152a_CALC_DBM_REF_900       900.0  900.0
 ri4152a_CALC_DBM_REF_1000     1000.0  1000.0
 ri4152a_CALC_DBM_REF_1200     1200.0  1200.0
 ri4152a_CALC_DBM_REF_8000     8000.0  8000.0
   ?i????       Error                            ? "  ?       Instr Handle                     ?? / ? ?      calcDbmRef                         	           0             50.0 ri4152a_CALC_DBM_REF_50 75.0 ri4152a_CALC_DBM_REF_75 93.0 ri4152a_CALC_DBM_REF_93 110.0 ri4152a_CALC_DBM_REF_110 124.0 ri4152a_CALC_DBM_REF_124 125.0 ri4152a_CALC_DBM_REF_125 135.0 ri4152a_CALC_DBM_REF_135 150.0 ri4152a_CALC_DBM_REF_150 250.0 ri4152a_CALC_DBM_REF_250 300.0 ri4152a_CALC_DBM_REF_300 500.0 ri4152a_CALC_DBM_REF_500 600.0 ri4152a_CALC_DBM_REF_600 800.0 ri4152a_CALC_DBM_REF_800 900.0 ri4152a_CALC_DBM_REF_900 1000.0 ri4152a_CALC_DBM_REF_1000 1200.0 ri4152a_CALC_DBM_REF_1200 8000.0 ri4152a_CALC_DBM_REF_8000    `    Panel Name: dBm Reference Query

This routine queries the current value of the dBm reference.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: calcDbmRef

Returns the current value of the dBm reference value used by the calculate function.  This will be one of the following values: 50, 75, 93, 110, 124, 125, 135, 150, 250, 300, 500, 600, 800, 900, 1000, 1200, or 8000 Ohms.    ?c????       Error                            ? "  ?       Instr Handle                     ?? / ? ?       calcDbmRef                         	           0    	              Panel Name: Lower Limit Setup

This function sets the lower limit used when the calculate function is LIMit. When enabled, a status bit will be set whenever the reading falls below this lower limit.

This setting may be used by all measurement functions.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     s    Control Name: calcLimLow

Sets the lower limit for limit checking. 

Valid Range: -120 to 120% of highest range.
   Ň????       Error                            ?< "  ?       Instr Handle                     ?? / ? ?      calcLimLow                         	           0 @?p     A??8    ???8                          \    Panel Name: Lower Limit Query

This routine queries the current value of the lower limit.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     i    Control Name: calcLimLow

Returns the current value of the lower limit used by the calculate function.    ʟ????       Error                            ?T "  ?       Instr Handle                     ?? / ? ?       calcLimLow                         	           0    	           |    Panel Name: Null Offset Setup

This functions sets the null offset value used when the CALCulate function is set to NULL.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: calcNullOffs

Sets the null offset value when the CALCulate function is set to NULL. 

Valid Range: 0 to 120% of the highest range.
   Ϯ????       Error                            ?c "  ?       Instr Handle                     ? / ? ?      calcNullOffs                       	           0 @?p     A??8    ???8                          \    Panel Name: Null Offset Query

This routine queries the current value of the null offset.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     k    Control Name: calcNullOffs

Returns the current value of the null offset used by the calculate function.    ??????       Error                            ՜ "  ?       Instr Handle                     ?E / ? ?       calcNullOffs                       	           0    	              Panel Name: Upper Limit Setup

This function sets the upper limit used when the calculate function is LIMit. When enabled, a status bit will be set whenever the reading falls below this upper limit.

This setting may be used by all measurement functions.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     u    Control Name: calcLimUpp

Sets the upper limit for limit checking. 

Valid Range: -120% to 120% of highest range.

   ?}????       Error                            ?2 "  ?       Instr Handle                     ?? / ? ?      calcLimUpp                         	           0 @?p     A??8    ???8                          \    Pnael Name: Upper Limit Query

This routine queries the current value of the upper limit.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     i    Control Name: calcLimUpp

Returns the current value of the upper limit used by the calculate function.    ߗ????       Error                            ?L "  ?       Instr Handle                     ?? / ? ?       calcLimUpp                         	           0    	           U    Panel Name: Measure AC Current

Description: This takes an AC current measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      C    Control Name: Reading

Description: Takes an AC Current reading

   ?   ?       Instr Handle                     ?(????       Error                            ??  ? ?       Reading                            0    	           	           U    Panel Name: Measure DC Current

Description: This takes an DC current measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      C    Control Name: Reading

Description: Takes an DC Current reading

   ?A   ?       Instr Handle                     ??????       Error                            ??  ? ?       Reading                            0    	           	           N    Panel Name: Measure Freq

Description: This takes an Frequency measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      A    Control Name: Reading

Description: Takes an frequency reading
   ??   ?       Instr Handle                     ??????       Error                            ?Z  ? ?       Reading                            0    	           	           f    Panel Name: Measure Four-Wire Resistance

Description: This takes an 4-wire resistance measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      L    Control Name: Reading

Description: Takes an four-wire resistance reading
   ??   ?       Instr Handle                     ?v????       Error                            ?+  ? ?       Reading                            0    	           	           e    Panel Name: Measure Two-Wire Resistance

Description: This takes an 2-wire resistance measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      K    Control Name: Reading

Description: Takes an two-wire resistance reading
   ??   ?       Instr Handle                     ?Q????       Error                            ?  ? ?       Reading                            0    	           	           M    Panel Name: Measure Period

Description: This takes an Period measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      >    Control Name: Reading

Description: Takes an period reading
   ?j   ?       Instr Handle                     ?????       Error                            ??  ? ?       Reading                            0    	           	           U    Panel Name: Measure AC Voltage

Description: This takes an AC voltage measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      C    Control Name: Reading

Description: Takes an AC Voltage reading

   '   ?       Instr Handle                     ?????       Error                            ?  ? ?       Reading                            0    	           	           U    Panel Name: Measure DC Voltage

Description: This takes an Dc Voltage measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      C    Control Name: Reading

Description: Takes an DC Voltage reading

   ?   ?       Instr Handle                     ?????       Error                            	G  ? ?       Reading                            0    	           	           a    Panel Name: Measure DC Voltage Ratio

Description: This takes an Dc Voltage Ratio measurement.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      J    Control Name: Reading

Description: Takes an DC voltage ratio reading


   
?   ?       Instr Handle                     `????       Error                              ? ?       Reading                            0    	           	           ?    Panel Name: Autozero State Setup

Disables or enables the auto zero mode.  Auto zero applies to DC voltage, DC current, and 2-wire ohms measurements only.  4-wire ohms and dc voltage ratio measurements automatically set auto zero to ON.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     W    Control Name: calZeroAuto

OFF (0) disables or ON (1) enables autozero measurements.    ????       Error                            ? "  ?       Instr Handle                     x ( ? ?      calZeroAuto                        	           0                         g    Panel Name: Autozero State Query

This routine queries the current setting of the auto zero feature.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     /    Returns a value of either 0 (OFF) or 1 (ON).    ????       Error                            ? "  ?       Instr Handle                     k / ? ?       calZeroAuto                        	           0    	               Panel Name: Line Freq Calibration Setup

This routine selects the line reference frequency used by the A to D converter. This function is provided for compatibility with other multimeters which use the SYSTem:LFRequency command instead of CALibration:LFRequency command.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    D    Control Name: systLfr

Indicates the desired line reference frequency; the legal values are 50 or 60. 

 Macro Name                Value  Description
-----------------------------------------------------------
 ri4152a_SYST_LFR_50          50  50
 ri4152a_SYST_LFR_60          60  60
 ri4152a_SYST_LFR_400        400  400
   w????       Error                            , "  ?       Instr Handle                     ? / ?       systLfr                            	           0               G50 ri4152a_SYST_LFR_50 60 ri4152a_SYST_LFR_60 400 ri4152a_SYST_LFR_400       Panel Name: Line Freq Calibration Query

This routine queries the current setting of the reference line frequency. This function is provided for compatibility with other multimeters which use the SYSTem:LFRequency? command instead of CALibration:LFRequency? command.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     S    Returns the current setting of the reference line frequency; either 50 or 60 Hz.    !<????       Error                            !? "  ?       Instr Handle                     $? / ? ?       systLfr                            	           0    	            ?    Panel Name: Calibratrion Count Query

This routine returns the number of times a point calibration has occurred.  A complete calibration of the instrument increments this value by more than just a single count.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"          The handle to the instrument      ?    Control Name: calCoun

A number between 0 and 32767 which represents the number of times a point calibration has occurred.  A complete calibration of the instrument increments this value by the number of points calibrated, not by only 1.    &?????       Error                            'C "  ?       Instr handle                     'j / ? ?       calCoun                            	           0    	            _    Panel Name: Read Status Byte

This routine returns the contents of the status byte register.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     \    Control Name: statusByte

The contents of the status byte are returned in this parameter.    )?????       Error                            *: "  ?       Instr Handle                     ,? / ? ?       statusByte                         	           0    	           ?    Panel Name: Condition Query

This function is used to determine the current condition (VI_TRUE or VI_FALSE) of a specified happening.

The happening input parameter corresponds to a valid condition value. (see the happening parameter description above for more information).

The pCondition output parameter represents the current state of a condition. It is either VI_TRUE (condition is true) or VI_FALSE (condition is not true).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: happening

The following conditions can occur on the multimeter.
Conditions are transient in nature, and this function reports
the current setting of the condition. A 1 is returned if the
condition is currently true, and 0 is returned if the
condition is currently false.

ri4152a_QUES_VOLT      = Voltage is Questionable
ri4152a_QUES_CURR      = Current is Questionable
ri4152a_QUES_RES       = Resistance is Questionable
ri4152a_QUES_LIMIT_LO  = High limit exceeded
ri4152a_QUES_LIMIT_HI  = High limit exceeded


 Macro Name               Value  Description
-----------------------------------------------------------
 ri4152a_QUES_VOLT          401  Voltage Questionable
 ri4152a_QUES_CURR          402  Current Questionable
 ri4152a_QUES_RES           410  Resistance Questionable
 ri4152a_QUES_LIM_LO        412  Low Limit Exceeded
 ri4152a_QUES_LIM_HI        413  High Limit Exceeded
     n    Control Name: pCondition

VI_TRUE  = condition is currently set.
VI_FALSE = condition is currently not set.
   /?????       Error                            0r "  ?       Instr Handle                     3 / ? ?       happening                        6? /A ?       pCondition                         	           0               ?Voltage Questionable ri4152a_QUES_VOLT Current Questionable ri4152a_QUES_CURR Resistance Questionable ri4152a_QUES_RES Low Limit Exceeded ri4152a_QUES_LIM_LO High Limit Exceeded ri4152a_QUES_LIM_HI    	           Y    This function indicates if an event has occurred since the last time the event was queried.  This function can be used as a history mechanism to record when an event occurred.  By repeated polling, a program can determine if or when an event occurs.

The happening input parameter corresponds to a valid happening value. (see happening section for more information).

The pEvent output parameter indicates if an event has occurred since the last time this routine was called. The output values are either VI_TRUE (event occurred since last query) or VI_FALSE (event did not occur since last query).      ?    VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    ?    Control Name: Happening

Happenings refer to something that happens. These can refer
to conditions or events. All of the conditions listed in the
ri4152a_statCond_Q() function will be detected as events as
well.   An event may be registered when a condition changes
state from  VI_FALSE to VI_TRUE.

The following events are detected by this instrument

ri4152a_QUES_VOLT                 = Voltage is Questionable
ri4152a_QUES_CURR                 = Current is Questionable
ri4152a_QUES_RES                  = Resistance Questionable
ri4152a_QUES_LIMIT_LO             = High limit exceeded
ri4152a_QUES_LIMIT_HI             = High limit exceeded
ri4152a_ESR_OPC                   = Operation Complete
ri4152a_ESR_QUERY_ERROR            = Query Error
ri4152a_ESR_DEVICE_DEPENDENT_ERROR = Device Dependent Error
ri4152a_ESR_EXECUTION_ERROR        = Execution Error
ri4152a_ESR_COMMAND_ERROR          = Command Error


 Macro Name                              Value  Description
-----------------------------------------------------------
 ri4152a_QUES_VOLT                         401  Voltage Questionable
 ri4152a_QUES_CURR                         402  Current Questionable
 ri4152a_QUES_RES                          410  Resistance Questionable
 ri4152a_QUES_LIM_LO                       412  Low Limit Exceeded
 ri4152a_QUES_LIM_HI                       413  High Limit Exceeded
 ri4152a_ESR_OPC                           601  Operation Complete
 ri4152a_ESR_QUERY_ERROR                   603  Query Error
 ri4152a_ESR_DEVICE_DEPENDENT_ERROR        604  Device Dependent Error
 ri4152a_ESR_EXECUTION_ERROR               605  Execution Error
 ri4152a_ESR_COMMAND_ERROR                 606  Command Error
     ?    Control Name: pEvent

VI_TRUE  = event occurred sometime between event readings.
VI_FALSE = the event did not occur between event readings.
   ;J????       Error                            ;? "  ?       Instr Handle                     >? / & ? ?    happening                        EG /A ?       pEvent                             	           0            
  ?Voltage Questionable ri4152a_QUES_VOLT Current Questionable ri4152a_QUES_CURR Resistance Questionable ri4152a_QUES_RES Low Limit Exceeded ri4152a_QUES_LIM_LO High Limit Exceeded ri4152a_QUES_LIM_HI Operation Complete ri4152a_ESR_OPC  Query Error ri4152a_ESR_QUERY_ERROR Device Dependent Error ri4152a_ESR_DEVICE_DEPENDENT_ERROR Execution Error ri4152a_ESR_EXECUTION_ERROR Command Error ri4152a_ESR_COMMAND_ERROR    	            ?    Panel Name: Clear All Events

This function will clear the status system of all events. It will also delete any errors in the instrument's error queue.

This will not affect conditions or callbacks.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
   IJ????       Error                            J  "  ?       Instr Handle                       	           0    ?    Panel Name: Operation Complete

This function sends an IEEE Operation Complete (*OPC) to the instrument. This causes the instrument to set the ri4152a_ESR_OPC event when the pending operation completes.  See the ri4152a_statEven_Q function.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
   N????       Error                            N? "  ?       Instr Handle                       	           0    ?    Panel Name: Operation Complete Query

This function sends an IEEE Operation Complete Query (*OPC?) to the instrument to aid in synchronization with other instruments or processes. When the operation completes, this function returns 1.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     /    Control Name: OPC

*OPC? response, always 1.    R?????       Error                            S? "  ?       Instr Handle                     VM / ? ?       opc                                	           0    	            ?    Panel Name: Wait To Continue

This function sends an IEEE Wait To Continue (*WAI) command to the instrument to aid in synchronization with other instruments or processes.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
   W?????       Error                            X?/   ?       Instr Handle                       	           0    ?    Panel Name: Device Clear

This function sends a device clear (DCL) to the instrument.

A device clear will abort the current operation and enable the instrument to accept a new command or query.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
   \?????       Error                            ]R#   ?       Instr Handle                       	           0    X    Panel Name: Get System Version

Description: This gets the multimeter's SCPI Version.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".      R    Control Name: Version

Description: This returns the most recent system version
   `?%   ?       Instr Handle                     c????       Error                            d5   ? ?      Version                            0    	           	            ?    Panel Name: Driver Error Message Query

The error message function translates the error return value from an instrument driver function to a user-readable string.      l    Control Name: Error

VI_SUCCESS           : No error
VI_ERROR_PARAMETER2  : The ViStatus error is invalid
     G    Instrument Handle returned from ri4152a_init(). This may be VI_NULL.      V    Control Name: error_num

The error return value from an instrument driver function.      S    Control Name: message

Error message string.  This is limited to 256 characters.    e?????       Error                            fl "  ?       Instr Handle                     f? / ? ?       error_num                        g /A ?       message                            	           0    0    	               Panel Name: Query Instrument for Error

The error query function queries the instrument and returns the instrument specific error information. Instrument errors may occur when a user places the instrument in a bad state such as sending an invalid sequence of coupled commands.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     4    Control Name: error_num

Instrument's error code.      _    Control Name: error_message

Instrument's error message.  This is limited to 256 characters.    i?????       Error                            j? "  ?       Instr Handle                     l? / ? ?       error_num                        m$ /A ?       error_message                      	           0    	            	           ?    Panel Name: Auto Error Detection Setup

This function enables (VI_TRUE) or disables (VI_FALSE) automatic instrument error checking.

If automatic error checking is enabled then the driver will query the instrument for an error at the end of each function call.

Automatic error checking should only be used in a polling environment.  Automatic error checking should be disabled (VI_FALSE) when instrument error callbacks are utilized.      ?    VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
    J    Control Name: errorQueryDetect

Boolean which enables (VI_TRUE) or disables (VI_FALSE) automatic instrument error querying. if VI_TRUE this will query the instrument for an error before returning from all driver functions.

if VI_FALSE this will not query the instrument for an error before returning from all driver functions.    pE????       Error                            p? "  ?       Instr Handle                     s? /  ?       errorQueryDetect                   	           0    On VI_TRUE Off VI_FALSE    ?    Panel Name: Auto Error Detection Query

This function indicates if automatic instrument error detection is enabled (VI_TRUE) or disabled (VI_FALSE).      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     d    Control Name: pErrDetect

Boolean indicating if automatic instrument error querying is performed.    vK????       Error                            w  "  ?       Instr Handle                     y? / ? ?       pErrDetect                         	           0    	               Panel Name: Reset

The reset function places the instrument in a default state. Before issuing this function, it may be necessary to send a device clear to ensure that the instrument can execute a reset.  A device clear can be issued by invoking ri4152a_dcl().      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
   {?????       Error                            |? "  ?       Instr Handle                       	           0    ?    Panel Name: Self Test

The self-test function causes the instrument to perform a self-test and returns the result of that self-test.  This is used to verify that an instrument is operating properly.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
         Control Name: test_result

Numeric result from self-test operation is returned.  See description below for more information.      \    Control Name: test_message

Self-test status message.  This is limited to 256 characters.    ??????       Error                            ?@ "  ?       Instr Handle                     ?? / ? ?       test_result                      ?p /A ?       test_message                       	           0    	            	            ?    Panel Name: Time Out Setup

The timeout function sets a minimum timeout value for driver I/O transactions in milliseconds. The timeout period may vary on computer platforms.

The default timeout period is 2 seconds.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: timeOut

Valid Range: 1 to 2147483647

This value sets the I/O timeout for all functions in the driver. It is specified in milliseconds.    ??????       Error                            ?i "  ?       Instr Handle                     ? / ? ?      timeOut                            	           0    ???                   ?    Panel Name: Time Out Query

The timeout query function returns the timeout value for driver I/O transactions in milliseconds.

The timeout period may vary on computer platforms.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     }    Control Name: pTimeout

This is the minimum timeout period that the driver can be set to. It is specified in milliseconds.    ?6????       Error                            ?? "  ?       Instr Handle                     ?? / ? ?       pTimeOut                           	           0    	            ?    Panel Name: Revision Query

The revision function returns the revision of the instrument driver and the firmware of the instrument being used.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     \    Control Name: Driver_rev

Instrument driver revision.  This is limited to 256 characters.      ]    Control Name: instr_rev

Instrument firmware revision.  This is limited to 256 characters.    ?o????       Error                            ?% "  ?       Instr Handle                     ?? / ? ?       driver_rev                       ?2 /A ?       instr_rev                          	           0    	            	            k    Panel Name: Cal LFR

Description: This routine selects the line frequency used by the A to D converter.

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      :    Control Name: Line

Description: The appropriate line.

   ?   ?       Instr Handle                     ??????       Error                            ?e , ? ?      Line                               0    	             ?   2   2           ^    Panel Name: Cal LFR Query

Description: Returns the appropriate calibration line frequency

    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"      A    Control Name: Line

Description: Returns the appropriate line.
   ??"   ?       Instr Handle                     ?}????       Error                            ?2  ? ?       Line                               0    	           	            ?    This routine enables or disables calibration security.  When security is disabled, the multimeter's calibration constants may be altered.  Read the service manual for more details on how to calibrate the multimeter.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     K    Control Name: State

The desired setting of security: 1 (ON) or 0 (OFF).      ;    Control Name: Code

The security access code (password).    ?????       Error                            ?? "  ?       Instr Handle                     ?w / ? ?       state                            ?? /A ?       code                               	           0    On VI_TRUE Off VI_FALSE    
"RI4152A"    D    This routine queries the present setting of calibration security.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     D    Control Name: State

The present setting of calibration security.    ?f????       Error                            ? "  ?       Instr Handle                     ?? / ? ?       state                              	           0    	            ?    Panel Name: Security Code Setup

This routine specifies the new security code.  To accept the new code, security must first be turned OFF (0) with the ri4152a_calSecStat routine.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     C    The new security code that will allow access to calibration ram.    ??????       Error                            ?> "  ?       Instr Handle                     ?? / ? ?       code                               	           0    "RI_4152A"    ?    Panel Name: Signal Level Setup

This routine selects the value of the calibration signal.  See the service manual for details on how to use this command.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     m    The value of the calibration signal applied.  See calibration manual for allowable values and their usage.    ??????       Error                            ?I "  ?       Instr Handle                     ?? / ? ?      value                           ???? M 3??                                         ???? c 3??                                            	           0 @È?
=qA?ׄ    ?r?                           GWARNING: Voltmeter accuracy affected by this routine.  See the service    %manual before calling this function.    a    Panel Name: Signal Level Query

This routine queries the present setting of calibration value.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message"     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ,    The present setting of calibration value.    ??????       Error                            ?< "  ?       Instr Handle                     ?? / ? ?       value                              	           0    	           ?    Panel Name: Calibration String

Description: This allows the user to record calibration information about the multimeter while the Calibration Secure State is Off.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".      `    Control Name: String

Description: The calibration String to enter

Number of Characters = 40
   ??   ?       Instr Handle                     ?,????       Error                            ?? . ? ?       Calibration String                 0    	               ?    Panel Name: Calibration String Query

Description: This allows the user to record calibration information about the multimeter while the Calibration Secure State is Off.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
     ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".    ??   ?       Instr Handle                     ?[????       Error                           ???? % ? ?       Result                             0    	           	            ?    Panel Name: Close

The close function terminates the software connection to the instrument and deallocates system resources.  It is generally a good programming habit to close the instrument handle when the program is done using the instrument.      ?    Control Name: Error

VI_SUCCESS: No error. Non VI_SUCCESS: Indicates error condition. To determine error message, pass the return value to routine "ri4152a_error_message".     ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                NOTE:  A new (unique) handle will be 
                returned EACH time the Initialize
                function is called.  The ri4152a_close()
                call should be used for EACH handle
                returned by the ri4152a_init() function.
   ??????       Error                            Ƃ "  ?       Instr Handle                       	           0 ????         ?  $t     K-    init                            ????         %?  -     K-    measure_Q                       ????         /=  7C     K.    fetc_Q                          ????         8@  C&     K-    configure                       ????         E  L?     K-    conf_Q                          ????         Ne  T     K-    sampCoun                        ????         T?  Y      K-    sampCoun_Q                      ????         Y?  iZ     K-    trigger                         ????         l?  u?     K-    trigger_Q                       ????         w  }?     K-    read_Q                          ????         ~?  ?b     K-    initImm                         ????         ??  ?     K-    trigImm                         ????         ??  ?x     K-    trg                             ????         ??  ??     K-    abor                            ????         ?T  ??     K-    timedFetch_Q                    ????         ?  ?L     K-    dataPoin_Q                      ????         ?  ?     K-    func                            ????         ??  ??     K-    func_Q                          ????         ??  ?|     K.    confCurrAc                      ????         ??  ?     K-    currAcRang                      ????         ?Q  ??     K-    currAcRang_Q                    ????         ??  ?     K-    currAcRes                       ????         ?'  ?C     K-    currAcRes_Q                     ????         ??  ??     K.    confCurrDc                      ????         ??  ?Z     K-    currDcRang                      ????         ??  ??     K-    currDcRang_Q                    ????         ??  ?9     K-    currDcRes                       ????         ??  ??     K-    currDcRes_Q                     ????         ?? 
?     K-    currDcAper                      ????        ? ?     K-    currDcAper_Q                    ????        ? ?     K-    currDcNplc                      ????        ? #     K-    currDcNplc_Q                    ????        #? '?     K.    confFres                        ????        ( +?     K.    confRes                         ????        ,e 2?     K-    resRang                         ????        4? 9?     K-    resRang_Q                       ????        :? L?     K-    resRes                          ????        R: VY     K-    resRes_Q                        ????        W c     K-    resAper                         ????        d? i@     K-    resAper_Q                       ????        i? u?     K-    resNplc                         ????        w? |     K-    resNplc_Q                       ????        |? ??     K.    confVoltAc                      ????        ? ??     K-    voltAcRang                      ????        ?? ?Q     K-    voltAcRang_Q                    ????        ?N ?     K-    voltAcRes                       ????        ?m ??     K-    voltAcRes_Q                     ????        ?H ?     K.    confVoltDc                      ????        ?? ?Y     K.    confVoltDcRat                   ????        ?? ?     K-    voltDcRang                      ????        ?? ??     K-    voltDcRang_Q                    ????        ?? à     K-    voltDcRes                       ????        ?3 ?:     K-    voltDcRes_Q                     ????        ?? ??     K-    voltDcAper                      ????        ?? ?'     K-    voltDcAper_Q                    ????        ?? ?s     K-    voltDcNplc                      ????        ?1 ??     K-    voltDcNplc_Q                    ????        ?_ ?     K.    confFreq                        ????        ?? ??     K-    freqAper                        ????        ? ??     K-    freqAper_Q                      ????        ?? ?     K-    freqVoltRang                    ????        ? ?     K-    freqVoltRang_Q                  ????        ? c     K.    confPer                         ????        ? }     K-    perAper                         ????        X w     K-    perAper_Q                       ????        3 !w     K-    perVoltRang                     ????        # (5     K-    perVoltRang_Q                   ????        )2 .?     K-    trigCoun                        ????        /p 3?     K-    trigCoun_Q                      ????        4< 9?     K-    trigDel                         ????        :? ?D     K-    trigDel_Q                       ????        @A G     K-    trigSour                        ????        I? P?     K-    trigSour_Q                      ????        QI V     K-    outpTtlt_M                      ????        W, \&     K-    outpTtlt_M_Q                    ????        ]. bm     K-    detBand                         ????        cH g?     K-    detBand_Q                       ????        hd mq     K-    inpImpAuto                      ????        n< rl     K-    inpImpAuto_Q                    ????        s( z(     K-    calcFunc                        ????        {w ??     K-    calcFunc_Q                      ????        ?? ??     K-    calcStat                        ????        ?? ?     K-    calcStat_Q                      ????        ?? ?@     K-    average_Q                       ????        ?? ?     K.    calcAverAver_Q                  ????        ?? ?     K.    calcAverCoun_Q                  ????        ?? ?.     K.    calcAverMax_Q                   ????        ?? ?G     K.    calcAverMin_Q                   ????        ? ??     K-    calcDbRef                       ????        ?n ??     K-    calcDbRef_Q                     ????        ?e ?-     K-    calcDbmRef                      ????        ?? ??     K-    calcDbmRef_Q                    ????        ?~ ?`     K-    calcLimLow                      ????        ?; ?n     K-    calcLimLow_Q                    ????        ?* Ө     K-    calcNullOffs                    ????        ԃ ظ     K-    calcNullOffs_Q                  ????        ?t ?X     K-    calcLimUpp                      ????        ?3 ?f     K-    calcLimUpp_Q                    ????        ?" ?(     K.    measCurrAc_Q                    ????        ?? ??     K.    measCurrDc_Q                    ????        ?? ??     K.    measFreq_Q                      ????        ?_ ?     K.    measFres_Q                      ????        ?; ?Y     K.    measRes_Q                       ????        ?       K.    measPer_Q                       ????         ? ?     K.    measVoltAc_Q                    ????        ? 	?     K.    measVoltDc_Q                    ????        
N g     K.    measVoltDcRat_Q                 ????        # ?     K*    calZeroAuto                     ????        ? ?     K-    calZeroAuto_Q                   ????        ^ !     K-    systLfr                         ????         ' $?     K-    systLfr_Q                       ????        %? (b     K-    calCoun_Q                       ????        ) -G     K-    readStatusByte_Q                ????        . 7#     K-    statCond_Q                      ????        8? E?     K-    statEven_Q                      ????        Hy L?     K-    statEvenClr                     ????        M$ Q~     K-    opc                             ????        Q? V?     K-    opc_Q                           ????        W@ [T     K-    wai                             ????        [? _?     K-    dcl                             ????        `v d?     K.    get_syst_vers                   ????        eK gt     K-    error_message                   ????        hj m?     K-    error_query                     ????        n? t?     K-    errorQueryDetect                ????        u? z     K-    errorQueryDetect_Q              ????        z? ?     K-    reset                           ????        ? ??     K-    self_test                       ????        ?? ??     K-    timeOut                         ????        ?z ?     K-    timeOut_Q                       ????        ?? ??     K-    revision_query                  ????        ?? ??     K.    calLfr                          ????        ?n ?{     K.    calLfr_Q                        ????        ?7 ?     K-    calSecStat                      ????        ? ?     K-    calSecStat_Q                    ????        ?? ?2     K-   calSecCode                      ????        ?? ?g     K-    calVal                          ????        ? ?     K-    calVal_Q                        ????        ?? ?J     K.    cal_string                      ????        ?? ?     K.    get_cal_string                  ????        ?? ?+     K-    close                                 6                                     DInitialize                          ?High Level Control                  Measure                              DTake Measurement                     DFetch Multiple Measurements         ?Configure Present Settings          ?Measurement Type                     DMeasurement Type Setup               DMeasurement Type Query              ?Sample                               DSample Count Setup                   DSample Count Query                  ?Trigger                              DTrigger Setup                        DTrigger Query                        DRead Using Present Settings         ?Low Level Control                    DInitiate Measurement                 DExecute Immediate Trigger            DExecute Bus Trigger                  DAbort Measurement                    DFetch Data from Instrument           DData Points Query                   
)Configure                           oFunction                             DFunction Setup                       DFunction Query                      9AC Current                           DConfigure AC Current                 DAC Current Range Setup               DAC Current Range Query               DAC Current Resolution Setup          DAC Current Resolution Query         ?DC Current                           DConfigure DC Current                 DDC Current Range Setup               DDC Current Range Query               DDC Current Resolution Setup          DDC Current Resolution Query          DDC Current Aperture Setup            DDC Current Aperture Query            DDC Current NPLC Setup                DDC Current NPLC Query               !Resistance                           DConfigure Four-Wire Res              DConfigure Two-Wire Res               DResistance Range Setup               DResistance Range Query               DResistance Resolution Setup          DResistance Resolution Query          DResistance Aperture Setup            DResistance Aperture Query            DResistance NPLC Setup                DResistance NPLC Query               ?AC Voltage                           DConfigure AC Voltage                 DAC Voltage Range Setup               DAC Voltage Range Query               DAC Voltage Resolution Setup          DAC Voltage Resolution Query         DC Voltage                           DConfigure DC Voltage                 DConfigure DC VoltageRatio            DDC Voltage Range Setup               DDC Voltage Range Query               DDC Voltage Resolution Setup          DDC Voltage Resolution Query          DDC Voltage Aperture Setup            DDC Voltage Aperture Query            DDC Voltage NPLC Setup                DDC Voltage NPLC Query               ?Frequency                            DConfigure Freq                       DFrequency Aperture Setup             DFrequency Aperture Query             DFrequency Volt. Range Setup          DFrequency Volt. Range Query          Period                               DConfigure Period                     DPeriod Aperture Setup                DPeriod Aperture Query                DPeriod Volt. Range Setup             DPeriod Volt. Range Query            bTrigger                              DTrigger Count Setup                  DTrigger Count Query                  DTrigger Delay Setup                  DTrigger Delay Query                  DTrigger Source Setup                 DTrigger Source Query                &Trigger Output                       DTrigger Output Setup                 DTrigger Output Query                ?Detector Bandwidth                   DDetector Bandwidth Setup             DDetector Bandwidth Query            +Input                                DAuto Impedance Setup                 DAuto Impedance Query                xCalculate                            DFunction Setup                       DFunction Query                       DState Setup                          DState Query                          DAverage Query                        DCalculate Average of Readings        DCalculate Number of Readings         DCalculate Max of Readings            DCalculate Min of Readings            DdB Reference Setup                   DdB Reference Query                   DdBm Reference Setup                  DdBm Reference Query                  DLower Limit Setup                    DLower Limit Query                    DNull Offset Setup                    DNull Offset Query                    DUpper Limit Setup                    DUpper Limit Query                   Measure                              DMeasure AC Current                   DMeasure DC Current                   DMeasure Freq                         DMeasure 4-wire resistance            DMeasure 2-wire resistance            DMeasure Period                       DMeasure AC Voltage                   DMeasure DC Voltage                   DMeasure DC Voltage Ratio            fCalibration                          DAutozero State Setup                 DAutozero State Query                 DLine Freq Calibration Setup          DLine Freq Calibration Query          DCalibration Count Query             ?Status                               DRead Status Byte                     DCondition Query                     bEvent                                DEvent Query                          DClear All Events                    ?Synchronization                      DOperation Complete                   DOperation Complete Query             DWait To Continue                    ?Utility                              DDevice Clear                         DGet System Version                  'Error Handling                       DDriver Error Message Query           DQuery Instrument for Error          rAuto Instrument Error Detection      DAuto Error Detection Setup           DAuto Error Detection Query           DReset                                DSelf Test                           "Time Out                             DTime Out Setup                       DTime Out Query                       DRevision Query                      ?Calibration                          DCal LFR                              DCal LFR Query                        DSecurity State Setting               DSecurity State Query                 DSecurity Code Setup                  DSignal Level Setup                   DSignal Level Query                   DCalibration String                   DCalibration String Query             DClose                           