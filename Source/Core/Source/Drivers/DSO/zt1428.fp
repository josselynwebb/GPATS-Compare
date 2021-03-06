s??        ??   8 ?  ?   ?   ????                               zt1428      ZT1428VXI Digitizing Oscilloscope             ? ? ??ViInt16  ? ? ??ViReal64     ? ??ViString     	? 	??ViBoolean     	? 	??ViSession     ? ??ViRsrc     ? ??ViStatus  ?     Instrument Name: Ztec Instruments ZT1428VXI
                  Digitizing Oscilloscope

 Description:     This instrument module provides
                  programming support for the
                  ZT1428VXI.  The module
                  is divided into the following
                  functions:

 Functions/Classes:

 (1)  Initialize
      Initialize the instrument and set to default
      configuration.

 (2)  Configure
      This class of functions set the vertical,
      acquisition, function, external input,
      outputs, and trigger settings on the 
      instrument.

 (3)  Configuration Readback
      This class of functions query the instrument
      settings.

 (4)  Measure
      This class of functions select and retrieve
      measurements.

 (5)  Waveform Operations
      This class of functions digitize, store,
      and retrieve waveform data.

 (6)  Low Level Operations
      This class of functions perform basic low
      level routines.

 (7)  Close
      Take the instrument offline.    ?    Initialize

This routine performs the following initialization:
-  Opens the instrument by starting a VISA Session.
-  Performs an identification query on the 
   Instrument.
-  Verifies that that the instrumnet is in
   advanced mode.
-  Returns an Instrument Handle which is used to 
   differentiate between instruments of the same
   model type.  This value will be used to identify
   the instrument in subsequent calls.    ?     Class Name:    Configure

 Description:   These functions configure the
                vertical and horizontal axes,
                trigger and acquisition modes.

 Functions/Classes:

 (1)  Auto Setup
      This function runs the built in autoscale
      function.

 (2)  Set Vertical
      This function configures the vertical
      parameters.  These are range, offset,
      coupling, probe attenuation, and filters.

 (3)  Set Acquisition
      This function configures the acquisition
      settings of the instrument.  These settings
      include type, number of waveforms to average,
      number of points to take, sample interval,
      timebase delay, timebase reference and
      trigger mode.

 (4)  Set Math Function
      This function configures the math functions
      of the instrument.

 (5)  Set External Input
      This function configures the frontpanel
      connector which can input either an external
      trigger or an external system clock.

 (6)  Set Outputs
      This function configures the frontpanel
      programable Output connector.

 (7)  Set Edge Trigger
      This function configures the edge trigger
      subsystem.  The settings include source,
      level, slope and sensitivity(trigger filter).

 (8)  Advanced Trigger
      This class contains advanced functions for
      configuring the trigger.  Functions include
      Soft Trigger, Set Trigger to Offset,
      Set Trigger Holdoff, Set Pattern Trigger,
      Set TV Trigger, and Get Trigger Event.    ?    Auto Setup

Commands the instrument to autoscale.  
Autoscale disables the following controls:
- All markers OFF
- All memories OFF
- Functions OFF
- Measurements OFF

Autoscale determines settings for the applied 
input signals, affecting the following controls:
- Channel Offset as required
- Channel Range as required
- Channel Coupling as required
- Channel State On/Off as required
- Timebase Range as required
- Trigger level as required
- Trigger mode to edge
     ?    Auto Logic Setup

Configures the vertical settings for selected 
channel to standard logic levels (either TTL
or ECL).  The affected settings are:
- Channel Voltage Range
- Channel DC Offset
- Channel Coupling
- Trigger Level     M    Set Vertical

Configures the vertical settings for the selected
channel(s).     t    Set Acquisition

Congfigures the acquisition and timebase settings
of the oscilloscope (horizontal-axis settings).     Q    Set Math Function

Configures the waveform math functions of the 
oscilloscope.     ?    Set External Input

Configures the Ext Trig BNC input connection which
has the dual functionality of external trigger 
input and external 100MHz timebase clock input.     q    Set Outputs

Configures the Probe Comp/Cal/Trig Output BNC 
connection and backplane ECLTRG0-1 trigger outputs.     D    Set Edge Trigger

Configures the oscilloscope for edge triggering.    ?     Class Name:    Trigger

 Description:   Contains functions for setting up
                the trigger modes.

 Functions:

 (1)  Set Trigger Mode
      This function sets the trigger mode and time
      base mode.

 (2)  External Trigger Coupling
      This function sets the external trigger
      coupling.

 (3)  Edge Trigger
      This function sets the edge trigger
      requirments.  These include source, trig.
      level, slope, sensitivity, and hold off
      time.

 (4)  Pattern Trigger
      This class contains routines to set the
      pattern trigger requirments.

 (5)  State Trigger
      This class contains routines to set the
      state trigger requirments.

 (6)  Delay Trigger
      This class contains routines to set the
      delay trigger requirments.

 (7)  TV Trigger
      This class contains routines to set the
      tv trigger requirments.

 (8)  Trigger Output
      This function enables or disables the
      trigger output.     ?    Soft Trigger

Causes a software-generated trigger event. This
is useful when operating in triggered mode and 
the trigger source is not present.     ?    Set Trigger to Offset

Configures the level of the selected trigger 
to its vertical center, which is equivalent to
the DC offset for that selected source.     g    Set Trigger Holdoff

Sets the time or number of events to holdoff before
detecting the trigger event.     J    Set Pattern Trigger

Configures the oscilloscope for pattern triggering.     F    Set State Trigger

Configures the oscilloscope for state triggering.     @    Set TV Trigger

Configures the oscilloscope for tv triggering.     p    Get Trigger Event

Returns the trigger event register status to 
indicate whether a trigger event has occured.    D     Class Name:    Configuration Readback

 Description:   These functions queries the
                instrument setup.

 Functions:

 (1)  Query Vertical
 (2)  Query Acquisition
 (3)  Query Math Function
 (4)  Query External Input
 (5)  Query Outputs
 (6)  Query Trigger
 (7)  Query Advanced Trigger
 (8)  Query Measurement     J    Query Vertical

Queries the vertical settings for the selected 
channel.     u    Query Acquistion

Queries the the acquisition and timebase settings
of the oscilloscope (horizontal-axis settings).     Q    Query Math Function

Queries the waveform math setup of the selected 
function.     ?    Query External Input

Queries the setup of the Ext Trig BNC input 
connection which has the dual functionality of 
external trigger input and external 100MHz 
timebase clock input.     ?    Query Outputs

Queries the configuration of the
Probe Comp/Cal/Trig Output BNC connection and 
backplane ECLTRG0-1 trigger outputs.     J    Query Trigger

Queries the triggering configuration of the
oscilloscope.     ]    Query Advanced Trigger

Queries the advanced triggering configuration of 
the oscilloscope.     |    Query Measurement

Queries the upper and lower threshold levels, delay
parameters, and width parameters for measurements.
         Class Name:    Measurement

 Description:   These functions select the
                measurement mode and return
                measurement results.

 Functions/Classes:

 (1)  Get Measurement
      This function gets a measurement result.

 (2)  Advanced Measurement
      This class of functions configures configures
      advanced measurement settings.  The functions
      are Set Measurement Level, Set Delay
      Parameters, Set Width Parameters, Set Limit
      Test, Set Mask Test, Get Result Statistics.     ?    Get Measurement

Causes the instrument to make the specified 
measurement on a previously captured waveform
and returns the result.          Class Name:    User Defined Limits

 Description:   These functions set the user
                defined limits for measurements.

 Functions/Classes:

 (1)  Limits
      This functions sets upper and lower
      threshold limits.

 (2)  Delay
      This function sets the starting and stoping
      condtitions for delay measurements.

 (3)  Pulse
      This function sets the + width level and -
      width level for width measurements.

 (4)  Enable User Defined
      This function enables or disables the user
      defined settings.     V    Set Measurement Level

Sets the upper and lower threshold levels for 
measurements.
     S    Set Delay Parameters

Sets the start and stop conditions for delay 
measurements.     e    Set Width Parameters

Sets the level conditions for positive width and
negative width measurements.     ?    Set Limit Test

Sets the instrument to perform limit test 
measurement comparisons or statistical
measurement recording. Up to three different
limit test or statistical measurements may
be specified.
     Q    Set Mask Test

Sets the instrument to perform mask test 
waveform comparisons.
        Get Result Statistics

Gets the statistical results of the statistics or 
limit test measurements, and the pass/fail results
of the limit test or mask test. Because up to 3
statistical or limit test results can be returned,
the measurement type is specified.    ?     Class Name:    Waveform Operations

 Description:   This class contains functions for
                digitizing, storing and reading
                waveforms.

 Functions:

 (1)  Digitize Waveform
      Digitizes the selected channel(s).

 (2)  Get Digitize Complete
      Queries the operation complete bit to
      determine when the digitize is done.

 (3)  Read Waveform to Array
      Reads data from the selected memory.

 (4)  Store to Memory
      Stores the waveform of the selected channel
      to the selected scope memory.

 (5)  Load Array to Memory
      Loads an array from the controlling system
      to instrument memory.        Digitize Waveform

Commands the oscilloscope to digitize the waveform
for the specified source(s). Normal digitize mode
waits for the digitize operation to complete. 
Asynchronous digitize mode uses the Get Digitize 
Complete function to synchronize the digitize 
operation.
     ?    Digitize Waveform

Returns the digitize operation complete status. 
This is used with the asynchronous digitize mode 
of the Digitize Waveform function to synchronize 
the digitize operation.
     k    Read Waveform to Array

Reads a waveform and its preamble information
from the specified waveform source.     ?    Store Waveform to Memory

Stores waveform data from the specified input
channel or math function to the specified waveform
memory location.     d    Load Array to Memory

Loads waveform data from an array to the specified
waveform memory location.    ?     Class Name:    Low Level Routines

 Description:   This class contains functions to
                perform some low level routines on
                the instrument.

 Functions:

 (1)  Reset
      Initalizes the instrument to the startup
      state.

 (2)  Device Clear
      Performs a low level VXI interface clear.

 (3)  Self Test
      Performs an instrument self test and returns
      the result.

 (4)  Run / Stop
      Enables or disables data acquisiton.

 (5)  Calibrate
      Performs an instrument self calibration 
      routine.

 (6)  Store / Recall State
      Stores or recalls instrument settings to or
      from a file.

 (7)  Get ID and Version
      Queries the instrument ID and version.

 (8)  Get Error
      Queries the instrument error code.

 (9)  Get Running
      Queries the data acquire state.

 (10) Wait for Operation Complete
      Instrument will not respond until an 
      operation is completed.     5    Reset

Resets the instrument to its power-on state.     b    Device Clear

Low-level VXIbus device clear that resets the
command interface to the instrument.     F    Self Test

Performs an instrument self test and returns the 
result.     <    Run/Stop

Enables or disables continuous data acquisition.     ?    Calibrate

Performs a calibration routine on the instrument
and returns the result. The calibration may take 
up to 10 minutes to complete. Note that the two
input channels must be disconnected before starting
the calibration.     l    Save/Recall Setup

Saves or recalls the oscilloscope setup from/to
non-volatile memory on the instrument.
     ^    Get ID and Version

Returns the instrument identification string and
the CVI driver version.     ?    Get Error

Returns the instrument error code for an existing 
error.  Also clears the instrument error light when
all errors are read.     >    Get Run/Stop

Returns the continuous data acquisition state.     K    Get Operation Complete

Returns the instrument operation complete status.          Close

Close the VISA session.     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

    C    Instrument Handle

This control returns an Instrument Handle that is 
used in all subsequent function calls to 
differentiate between different sessions of this 
instrument driver.  Each time this function is 
invoked a Unique Session is opened.  It is possible
to have more than one session open for the same 
resource.
        Resource Name

This control specifies the interface and address 
of the device that is to be initialized (Instrument
Descriptor). The exact grammar to be used in this 
control is: 
GPIB[board]::primary addr[::second addr][::INSTR]
VXI[board]::logical address::INSTR    3? ????    `    Error                           ????, ??                                           4l Y? ?  d    Instrument Handle                 5? a R  ?  ?    Resource Name                      	            <Copyright 2004, ZTEC Instruments, Inc.
All Rights Reserved.    	           "VXI0::128::INSTR"   ?    This function performs the following initialization actions:

- Opens a session to the Default Resource Manager resource and a session to the specified device using the interface and address specified in the Resource_Name control.

- Performs an identification query on the Instrument.

- Resets the instrument to a known state.

- Sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.

- Returns an Instrument Handle which is used to differentiate between different sessions of this instrument driver.

- Each time this function is invoked a Unique Session is opened.  It is possible to have more than one session open for the same resource.

Notes:

(1) If this instrument does not support an ID Query, and the ID Query control is set to "Do Query" then this function should return the Warning Code 0x3FFC0101 - VI_WARN_NSUP_ID_QUERY.
   
(2) If this instrument does not support a Reset, and the Reset control is set to "Reset Device" then this function should return the Warning Code 0x3FFC0102 - VI_WARN_NSUP_RESET.
    q    This control specifies the interface and address of the device that is to be initialized (Instrument Descriptor). The exact grammar to be used in this control is shown in the note below. 

Default Value:  "GPIB::1"

Notes:

(1) Based on the Instrument Descriptor, this operation establishes a communication session with a device.  The grammar for the Instrument Descriptor is shown below.  Optional parameters are shown in square brackets ([]).

Interface   Grammar
------------------------------------------------------
GPIB        GPIB[board]::primary address[::secondary address]
            [::INSTR]
            
The GPIB keyword is used with GPIB instruments.

The default value for optional parameters are shown below.

Optional Parameter          Default Value
-----------------------------------------
board                       0
secondary address           none - 31
        This control specifies if an ID Query is sent to the instrument during the initialization procedure.

Valid Range:
VI_OFF (0) - Skip Query
VI_ON  (1) - Do Query (Default Value)

Notes:
   
(1) Under normal circumstances the ID Query ensures that the instrument initialized is the type supported by this driver. However circumstances may arise where it is undesirable to send an ID Query to the instrument.  In those cases; set this control to "Skip Query" and this function will initialize the selected interface, without doing an ID Query.

/**** DELETE THIS NOTE AND THE STATUS CODE IF SUPPORTED *****/
(2) If this instrument does not support an ID Query, and this control is set to "Do Query" then this function should return the Warning Code 0x3FFC0101 - VI_WARN_NSUP_ID_QUERY.
    2    This control specifies if the instrument is to be reset to its power-on settings during the initialization procedure.

Valid Range:
VI_OFF (0) - Don't Reset
VI_ON  (1) - Reset Device (Default Value)

Notes:

(1) If you do not want the instrument reset. Set this control to "Don't Reset" while initializing the instrument.

/**** DELETE THIS NOTE AND THE STATUS CODE IF SUPPORTED *****/
(2) If this instrument does not support a Reset, and this control is set to "Reset Device" then this function should return the Warning Code 0x3FFC0102 - VI_WARN_NSUP_RESET.
    7    This control returns an Instrument Handle that is used in all subsequent function calls to differentiate between different sessions of this instrument driver.

Notes:

(1) Each time this function is invoked a Unique Session is opened.  It is possible to have more than one session open for the same resource.
    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFC0101  ID Query not supported - VI_WARN_NSUP_ID_QUERY   
3FFC0102  Reset not supported - VI_WARN_NSUP_RESET      
3FFC0103  Self Test not supported - VI_WARN_NSUP_SELF_TEST  
3FFC0104  Error Query not supported - VI_WARN_NSUP_ERROR_QUERY
3FFC0105  Revision Query not supported - VI_WARN_NSUP_REV_QUERY  
3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

BFFC0001  Parameter 1 out of range. (String not range checked)
BFFC0002  Parameter 2 (ID Query) out of range.
BFFC0003  Parameter 3 (Reset Device) out of range.
BFFC0004  Parameter 4 out of range.
BFFC0005  Parameter 5 out of range.
BFFC0006  Parameter 6 out of range.
BFFC0007  Parameter 7 out of range.
BFFC0008  Parameter 8 out of range.
BFFC0011  Instrument returned invalid response to ID Query

BFFC0800  Error Opening File      VI_ERROR_INSTR_FILE_OPEN
BFFC0801  Error Writing to File   VI_ERROR_INSTR_FILE_WRITE
BFFC0803  Invalid Response VI_ERROR_INSTR_INTERPRETING_RESPONSE
BFFC0809  Parameter 9 out of range.  VI_ERROR_INSTR_PARAMETER9
BFFC080A  Parameter 10 out of range. VI_ERROR_INSTR_PARAMETER10
BFFC080B  Parameter 11 out of range. VI_ERROR_INSTR_PARAMETER11
BFFC080C  Parameter 12 out of range. VI_ERROR_INSTR_PARAMETER12

BFFF0000  Miscellaneous or system error occurred.
BFFF000E  Invalid session handle.
BFFF0015  Timeout occurred before operation could complete.
BFFF0034  Violation of raw write protocol occurred.
BFFF0035  Violation of raw read protocol occurred.
BFFF0036  Device reported an output protocol error.
BFFF0037  Device reported an input protocol error.
BFFF0038  Bus error occurred during transfer.
BFFF003A  Invalid setup (attributes are not consistent).
BFFF005F  No listeners condition was detected.
BFFF0060  This interface is not the controller in charge.
BFFF0067  Operation is not supported on this session.

Notes:

(1) Parameter Error Codes for parameters 1 through 8 are defined in the vpptype.h header file the range is BFFC0001 - BFFC0008; Parameter Error Codes for parameters 9 through 15 are defined in the instrument driver's header file the range is BFFC0809 - BFFC080F; for parameter errors greater than 15, and other instrument specific error codes, use an error code in the range of BFFC0900 to BFFC0FFF.  This is equivalent to using (VI_ERROR_INSTR_OFFSET + n); where n represents each instrument specific error number.  Valid ranges for n are 0 to 6FF.  (All values are given in Hexadecimal Notation)

(2) Delete all unused status codes from the Status Control of each function panel when you are finished development of your instrument driver, for example in this control the status codes for parameters 1, 3-8, and the codes for Error Opening and Writing to File should be deleted.  Those status codes are provided here as a convenience for during driver development.  

(3) Delete these three (3) notes when you are finished with your driver development.    <? =   ?  ?    Resource Name                     @7 = ?       ID Query                          CO =? ?       Reset Device                      E? ? ? ?  }    Instrument Handle                 F?#????  ?    Status                          ????( ??                                            "VXI0::128::INSTR"  ! Do Query VI_ON Skip Query VI_OFF  &  Reset Device VI_ON Don't Reset VI_OFF    	           	           <Copyright 2004, ZTEC Instruments, Inc.
All Rights Reserved.    i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    U ????    `    Error                             Ux ?   ?  d    Instrument Handle                  	            handle    ?    Channel

Selects the channel to be configured.

Valid Range:
1  - ZT1428_CHAN1     - Channel 1
2  - ZT1428_CHAN2     - Channel 2     ?    Logic

Selects the logic to set the selected channel(s).
The offset, range, coupling, and trigger level are
configured for the selected logic type.

Valid Range:
0 - ZT1428_LOGIC_TTL - TTL Logic
1 - ZT1428_LOGIC_ECL - ECL Logic     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    Vz l ?          Channel                           W l%          Logic                             W? ????    `    Error                             Xb
   ?  d    Instrument Handle                $ CH 1 ZT1428_CHAN1 CH 2 ZT1428_CHAN2  *  TTL ZT1428_LOGIC_TTL ECL ZT1428_LOGIC_ECL    	            handle    ?    Channel

Selects the channel to be configured.

Valid Range:
1  - ZT1428_CHAN1     - Channel 1
2  - ZT1428_CHAN2     - Channel 2
10 - ZT1428_CHAN_BOTH - Channels 1 & 2    ?    Coupling

Sets the input coupling for the selected channel.
The coupling for each channel can be set to AC, DC,
or DCFifty, or ACLFR. DCFifty is DC coupling with 
50 ohm impedance. ACLFR is AC coupling which also 
selects an internal high pass filter to reject 
frequencies below approximately 450Hz. 

Valid Range:
0 - ZT1428_VERT_COUP_AC    - AC 1M? (10 Hz)
1 - ZT1428_VERT_COUP_ACLFR - AC 1M? (450 Hz)
2 - ZT1428_VERT_COUP_DC    - DC 1M?
3 - ZT1428_VERT_COUP_DCF   - DC 50?    x    Lowpass Filter

Selects which lowpass filter, if any, will be used.
When OFF, the lowpass filter is bypassed, providing
approximately 250 MHz bandwidth.  The bandwidth 
limit filter may be used with all coupling 
selections. 

Valid Range:
0 - ZT1428_VERT_FILT_OFF   - Off
1 - ZT1428_VERT_FILT_30MHZ - 30 MHz Lowpass Filter
2 - ZT1428_VERT_FILT_1MHZ  - 1 MHz Lowpass Filter
        Probe Attenuation

Specifies the probe's attenuation factor for the 
specified channel.  The probe attenuation changes 
the reference constants for scaling the vertical 
range and offset, automatic measurements, trigger 
levels, etc. 

Valid Range:
0.9 to 1000.0     ?    Range

Specifies the full scale acquisition range in volts
for the specified input channel.

Valid Range depends upon probe attenuation (P):
(0.008 * P) to (50 * P)
with (0.008 * P) resolution
    l    Offset

Specifies the DC offset voltage that is represented
at vertical center for the selected channel.

Valid Range depends upon range and probe 
attenuation (P):

        Channel range       Offset Limit
      8mV * P to 400mV * P    ?2V * P
  > 400mV * P to  2.0V * P    ?10V * P
   > 2.0V * P to 10.0V * P    ?50V * P
  > 10.0V * P to 50.0V * P    ?250V * P     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    Z" 1 L          Channel                           Z? )          Coupling                          \? 9?          Lowpass Filter                    ^9 ? /    `    Probe Attenuation                 _I ? ?    `    Range                             ` ??    `    Offset                            a? ????    `    Error                             a? /  ?  d    Instrument Handle                             :CH 1 ZT1428_CHAN1 CH 2 ZT1428_CHAN2 Both ZT1428_CHAN_BOTH              ?AC 1MOhms (10Hz) ZT1428_VERT_COUP_AC AC 1MOhms (450Hz) ZT1428_VERT_COUP_ACLFR DC 1MOhms ZT1428_VERT_COUP_DC DC 50Ohms ZT1428_VERT_COUP_DCF               QOff ZT1428_VERT_FILT_OFF 30MHz ZT1428_VERT_FILT_30MHZ 1MHz ZT1428_VERT_FILT_1MHZ    1.0    4.0    0.0    	            handle   ?    Acquire Type

Specifies the type of acquisition that is to take
place when a Digitize or Run command is executed.
In Normal mode, a single waveform is captured.
In Average mode, multiple captured waveforms are
averaged. In Envelope mode, the minimum and maximum
values of multiple captured waveforms are used to 
create an envelope.

Valid Range:
0 - ZT1428_ACQ_NORM - Normal
1 - ZT1428_ACQ_AVER - Average
2 - ZT1428_ACQ_ENV  - Envelope
    ?    Acquire Count

Specifies the acquisition count for repetitive 
aquisition modes. In Normal mode, this parameter 
is ignored. In Average mode, this specifies the 
number of waveforms to be averaged before the 
acquisition is complete.  In Envelope mode, this 
specifies the number of waveforms for which to 
capture minimum and maximumvalues before the
acquisition is complete.

Valid Range:
1 to 2048
     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     ?    Trigger Mode

Selects the trigger mode to enable automatic
triggering in absence of trigger event. 

0 - ZT1428_ACQ_AUTO - Auto
1 - ZT1428_ACQ_SING - Single
2 - ZT1428_ACQ_TRIG - Triggered
     ?    Sample Interval

Specifies the acquisition sampling interval in 
seconds.

Valid Range:   
20 ps (50 GS/s) to 1 sec (1 S/s) in 1, 2, 4 steps     ?    Number of Points

Specifies the number of points for each waveform.

Valid Range depends upon Sample Interval:
100 to Max_points

Max_points = 125,000 for Sample Interval > 10 us
Max_points = 1,000,000 for Sample Interval <= 10 us
     ?    Timebase Reference

Specifies the timebase reference to the left, 
center, or right of the active waveform.

0 - ZT1428_ACQ_LEFT  - Left
1 - ZT1428_ACQ_CENT  - Center
2 - ZT1428_ACQ_RIGHT - Right        Timebase Delay

Specifies the timebase delay, the time between the 
trigger event and the waveform timebase reference 
point.  This value is specified in seconds.

Valid Range depends upon Number of Points, Sample 
Interval, and Timebase Reference:
Minimum to 500 s

Minimum = -Max_Points + (Ref * Number of Points)
Max_points = 125,000 for Sample Interval > 10 us
Max_points = 1,000,000 for Sample Interval <= 10 us
Ref = 0 for Left Timebase Reference
Ref = 0.5 for Center Timebase Reference
Ref = 1.0 for Right Timebase Reference
     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    e? ?          Acquire Type                      gP ?]     `    Acquire Count                     h? ????    `    Error                             i\ ? ?          Trigger Mode                      j# 9 ?   d    Sample Interval                   j? 9 1     d    Number of Points                  k? 6r          Timebase Reference                lw <?    `    Timebase Delay                    n? ?   ?  d    Instrument Handle                             GNormal ZT1428_ACQ_NORM Average ZT1428_ACQ_AVER Envelope ZT1428_ACQ_ENV    8    	                       FAuto ZT1428_ACQ_AUTO Single ZT1428_ACQ_SING Triggered ZT1428_ACQ_TRIG           !  ?20 pS 20.0e-12 40 pS 40.0e-12 100 pS 100.0e-12 200 pS 200.0e-12 500 pS 500.0e-12 1 nS 1.0e-9 2 nS 2.0e-9 4 nS 4.0e-9 10 nS 10.0e-9 20 nS 20.0e-9 40 nS 40.0e-9 100 nS 100.0e-9 200 nS 200.0e-9 400 nS 400.0e-9 1 uS 1e-6 2 uS 2e-6 4 uS 4e-6 10 uS 10.0e-6 20 uS 20.0e-6 40 uS 40.0e-6 100 uS 100.0e-6 200 uS 200.0e-6 400 uS 400.0e-6 1 mS 1.0e-3 2 mS 2.0e-3 4 mS 4.0e-3 10 mS 10.0e-3 20 mS 20.0e-3 40 mS 40.0e-3 100 mS 100.0e-3 200 mS 200.0e-3 400 mS 400.0e-3 1 S 1.00    500              CLeft ZT1428_ACQ_LEFT Center ZT1428_ACQ_CENT Right ZT1428_ACQ_RIGHT    0.0    handle   q    Operation

Specifies what operation is to take place.  
The difference, integrate, invert, and only
operations ignore the second source.

Valid Values:
0 - ZT1428_FUNC_ADD  - Add
1 - ZT1428_FUNC_SUB  - Subtract
2 - ZT1428_FUNC_MULT - Multiply
3 - ZT1428_FUNC_DIFF - Difference
4 - ZT1428_FUNC_INT  - Integrate
5 - ZT1428_FUNC_INV  - Invert
6 - ZT1428_FUNC_ONLY - Only    G    Source 1

Specifies the channel to be used as the first 
operand.  In operations that need only one
operand this control selects the source.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4    C    Source 2

Specifies the channel to be used as the second 
operand.  In operations that need only one
operand this control has no effect.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4     ?    Function Number

Specifies the function to be configured.

Valid Values:
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     ?    Function State

Controls the function on/off state. Unused math 
functions should be disabled to decrease waveform
processing time.

Valid Values:
0 - ZT1428_FUNC_OFF - Function Off
1 - ZT1428_FUNC_ON  - Function On     ?    Offset

Specifies the DC offset in volts
for the specified function channel.

Valid Range:
-1E+38 to 1E+38

Note: A 0.0 Range Control setting leaves the 
      ZT1428VXI-calculated range and offset values
      unchanged at the auto-calculated values.
     ?    Range

Specifies the full scale range in volts
for the specified function channel.

Valid Range:
0.0 or 1E-38 to 1E+38
0.0 leaves the ZT1428VXI-calculated range and 
    offset values unchanged at the auto-calculated
    values.
     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    s? 8 ?          Operation                         up ;e          Source 1                          v? 8?          Source 2                          x
 M C          Function Number                   x?	???    `    Error                             y	 ? K          Function State                    y? ?d    d    Offset                            z? ? ?    d    Range                             {?   ?  d    Instrument Handle                             ?Add ZT1428_FUNC_ADD Subtract ZT1428_FUNC_SUB Multiply ZT1428_FUNC_MULT Differentiate ZT1428_FUNC_DIFF Integrate ZT1428_FUNC_INT Invert ZT1428_FUNC_INV Only ZT1428_FUNC_ONLY               ?Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Memory 1 ZT1428_WMEM1 Memory 2 ZT1428_WMEM2 Memory 3 ZT1428_WMEM3 Memory 4 ZT1428_WMEM4              ?Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Memory 1 ZT1428_WMEM1 Memory 2 ZT1428_WMEM2 Memory 3 ZT1428_WMEM3 Memory 4 ZT1428_WMEM4   1 ZT1428_FUNC1 2 ZT1428_FUNC2    	          &  On ZT1428_FUNC_ON Off ZT1428_FUNC_OFF    0.0    0.0    handle   ?    External Mode

Specifies the external connector input function.
If Trigger is specified, the external connector
is used as a trigger input and the internal clock
reference is used. If Clock is specified, an 
external 100 MHz clock must be applied to the 
external input for use as the timebase reference.
In clock mode, the external trigger function cannot
be used.

Valid Range:
0 - ZT1428_EXT_MODE_TRIG - Trigger (Internal Clock)
1 - ZT1428_EXT_MODE_CLK  - Clock (External Clock)
     ?    External Impedance

Specifies the input impedance for the external 
trigger or clock input.

Valid Range:
0 - ZT1428_EXT_IMP_1M - 1M?
1 - ZT1428_EXT_IMP_50 - 50?     ?    External Level

Specifies the threshold voltage level of the 
external trigger or sample clock connected to 
the EXT TRIG input.

Valid Range:   
-2.0 V to 2.0 V     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?? L z          External Mode                     ?~ H?          External Impedance                ?) P ?    `    External Level                    ?? ????    `    Error                             ?E ?    ?  d    Instrument Handle                             7Trigger ZT1428_EXT_MODE_TRIG Clock ZT1428_EXT_MODE_CLK               21MOhms ZT1428_EXT_IMP_1M 50Ohms ZT1428_EXT_IMP_50    0.0    	            handle   ?    BNC Output

Specifies the output mode of the BNC
Probe Comp/Cal/Trig Output connector.
Probe selects a 500 Hz output. Trigger 
selects a trigger output pulse upon a 
detected trigger event. SClock selects
a 10 MHz output. DC Calibrate, 0V and 5V
select DC output levels.

Valid Range:
0 - ZT1428_OUT_BNC_PROB - Probe
1 - ZT1428_OUT_BNC_TRIG - Trigger
2 - ZT1428_OUT_BNC_DC   - DC Calibrate
3 - ZT1428_OUT_BNC_0V   - 0 Volts
4 - ZT1428_OUT_BNC_5V   - 5 Volts
5 - ZT1428_OUT_BNC_SCL  - SClock     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    ECL 0

Turns the ECL 0 trigger output on or off. The ECL
trigger output occurs when the instrument detects
a trigger event.

Valid Values:  
0 - ZT1428_OUT_OFF - Off
1 - ZT1428_OUT_ON  - On     ?    ECL 1

Turns the ECL 1 trigger output on or off. The ECL
trigger output occurs when the instrument detects
a trigger event.

Valid Values:  
0 - ZT1428_OUT_OFF - Off
1 - ZT1428_OUT_ON  - On    \    BNC Voltage

Specifies the active-state output voltage for the
BNC output. For the Probe, Trigger and Sclock
output modes, the signal transitions between 0V 
and this voltage level. For DC CAL mode, the DC
output voltage is set at this level. This control
is ignored for 0V and 5V output modes.

Valid Range:
-3.5V to +8.5V (into high impedance)
     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?t F v          BNC Output                        ?h ????    `    Error                             ?? ZF          ECL 0                             ?? Z?          ECL 1                             ?f ] ?    `    BNC Voltage                       ?? ?   ?  d    Instrument Handle                             ?Probe ZT1428_OUT_BNC_PROB Trigger ZT1428_OUT_BNC_TRIG DC CAL ZT1428_OUT_BNC_DC 0 V ZT1428_OUT_BNC_0V 5 V ZT1428_OUT_BNC_5V SClock ZT1428_OUT_BNC_SCL    	          $  On ZT1428_OUT_ON Off ZT1428_OUT_OFF  $  On ZT1428_OUT_ON Off ZT1428_OUT_OFF    0.0    handle    ?    Source

Specifies the source for the trigger signal.

Valid Values:
1 - ZT1428_TRG_CHAN1 - Chan 1
2 - ZT1428_TRG_CHAN2 - Chan 2
3 - ZT1428_TRG_EXT   - External
4 - ZT1428_TRG_ECL0  - ECL 0
5 - ZT1428_TRG_ECL0  - ECL 1     ?    Level

Specifies the trigger level of the selected source
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     ?    Slope

Specifies the trigger slope for the specified
source.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
        Sensitivity

Specifies the trigger filter mode.  If Normal is 
selected, trigger filtering is turned off. If Low
is selected, noise rejection hysteresis is enabled.
If Low Freq Reject is selected, the trigger signal
is AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

    ?? H P          Source                            ?| Y ?    `    Level                             ? S;          Slope                             ?? M?   ?      Sensitivity                       ??   ?  d    Instrument Handle                 ?a???    `    Error                                         tChan 1 ZT1428_TRG_CHAN1 Chan 2 ZT1428_TRG_CHAN2 External ZT1428_TRG_EXT ECL 0 ZT1428_TRG_ECL0 ECL 1 ZT1428_TRG_ECL1    0.0  . + ZT1428_TRG_SLOPE_POS - ZT1428_TRG_SLOPE_NEG               }Normal ZT1428_TRG_SENS_NORM Low ZT1428_TRG_SENS_LOW Low Freq Reject ZT1428_TRG_SENS_LFR High Freq Reject ZT1428_TRG_SENS_HFR    handle    	            i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?m ????    `    Error                             ?? ?   ?  d    Instrument Handle                  	            handle    i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     ?    Source

Specifies the trigger source to center by setting
its level at its DC offset setting.

Valid Values:
1 - ZT1428_TRG_CHAN1 - Chan 1
2 - ZT1428_TRG_CHAN2 - Chan 2
3 - ZT1428_TRG_EXT   - External     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?? ????    `    Error                             ?Q m          Source                            ?# ?   ?  d    Instrument Handle                  	                       HChan 1 ZT1428_TRG_CHAN1 Chan 2 ZT1428_TRG_CHAN2 External ZT1428_TRG_EXT    handle    ?    Holdoff Type

Specifies the type of trigger holdoff.

Valid Values:
0 - ZT1428_TRG_HOLD_TIME  - Time
1 - ZT1428_TRG_HOLD_EVENT - Event     ^    Holdoff Events

Specifies the holdoff by number of events.

Valid Values:
1 to 65536 events
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     U    Holdoff Time

Specifies the holdoff time in seconds.

Valid Values:
40 ns to 320 ms     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?? l \          Holdoff Type                      ?A J ?     `    Holdoff Events                    ?? ????    `    Error                             ? ? ?    `    Holdoff Time                      ?t ? %  ?  d    Instrument Handle                6  Event ZT1428_TRG_HOLD_EVENT Time ZT1428_TRG_HOLD_TIME    1    	            40e-9    handle    ?    GT Time

This parameter specifes the greater than time in 
seconds. The pattern must be present for more than
this time when using either greater than mode 
or range mode.

Valid Values:
20 ns to 160 ms     ?    LT Time

This parameter specifes the less than time in 
seconds. The pattern must be present for less than
this time when using either less than mode or 
range mode.

Valid Values:
30 ns to 160 ms     g     Control Name:  Error

 Description:   Displays status relating to the
                function call.         Condition

Sets the pattern condition that must be satisfied
in order to generate a trigger event.  In GT mode,
the pattern must be present for more than the GT
Time. In LT mode, the pattern must be present for
less than the LT Time. In Range mode, the pattern
must be present between the GT Time and the LT Time.

Valid Range:
0 - ZT1428_TRG_PATT_ENTER - Enter
1 - ZT1428_TRG_PATT_EXIT  - Exit
2 - ZT1428_TRG_PATT_GT    - Greater Than
3 - ZT1428_TRG_PATT_LT    - Less Than
4 - ZT1428_TRG_PATT_RANGE - Between
     ?    Level Chan1

Specifies the trigger level of input channel 1
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     ?    Level Chan2

Specifies the trigger level of input channel 2
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     m    Level Ext

Specifies the trigger level of the external trigger
input in Volts.

Valid Range:
-2.0V to +2.0V    ?    Logic

Specifies the logical relationship between 
the signal and the defined voltage level that 
must exist before the pattern is considered valid. 
The logic pattern to be matched uses:
  L to represent logic Low
  H to represent logic High
  X to represent Don't Care
A five-character string should be specified.
The first character is for Channel 1, 
the second for Channel 2, the third for External,
the fourth for ECLT0 and the last for ECLT1. 
    3    Sensitivity1

Specifies the trigger filter mode for input
channel 1.  If Normal is  selected, trigger
filtering is turned off. If Low is selected, 
noise rejection hysteresis is enabled. If Low 
Freq Reject is selected, the trigger signal is 
AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject    3    Sensitivity2

Specifies the trigger filter mode for input
channel 2.  If Normal is  selected, trigger
filtering is turned off. If Low is selected, 
noise rejection hysteresis is enabled. If Low 
Freq Reject is selected, the trigger signal is 
AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?\ | ?    `    GT Time                           ?0 ? ?    `    LT Time                           ?????    `    Error                             ?m ? j          Condition                         ?u 1    `    Level Chan1                       ? ~   `    Level Chan2                       ?? ?   `    Level Ext                         ? 9 1    `    Logic                             ?? +?   ?      Sensitivity1                      ?$ |? 	  ?      Sensitivity2                      ?_ %  ?  d    Instrument Handle                  20e-9    30e-9    	                       ?Enter ZT1428_TRG_PATT_ENTER Exit ZT1428_TRG_PATT_EXIT Greater Than ZT1428_TRG_PATT_GT Less Than ZT1428_TRG_PATT_LT Between ZT1428_TRG_PATT_RANGE    0.0    0.0    0.0    "XXXXX"               }Normal ZT1428_TRG_SENS_NORM Low ZT1428_TRG_SENS_LOW Low Freq Reject ZT1428_TRG_SENS_LFR High Freq Reject ZT1428_TRG_SENS_HFR               }Normal ZT1428_TRG_SENS_NORM Low ZT1428_TRG_SENS_LOW Low Freq Reject ZT1428_TRG_SENS_LFR High Freq Reject ZT1428_TRG_SENS_HFR    handle    g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     ?    Level Chan1

Specifies the trigger level of input channel 1
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     ?    Level Chan2

Specifies the trigger level of input channel 2
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     m    Level Ext

Specifies the trigger level of the external trigger
input in Volts.

Valid Range:
-2.0V to +2.0V        Logic

This logic specifies the relationship between 
the signal and the defined voltage level that 
must exist before the pattern is considered valid. 
The logic pattern to be matched uses:
  L to represent logic Low
  H to represent logic High
  X to represent Don't Care
A five-character string should be specified.
The first character is for Channel 1, 
the second for Channel 2, the third for External,
the fourth for ECLT0 and the last for ECLT1. 
The logic pattern for the selected state trigger 
source is ignored.
    3    Sensitivity1

Specifies the trigger filter mode for input
channel 1.  If Normal is  selected, trigger
filtering is turned off. If Low is selected, 
noise rejection hysteresis is enabled. If Low 
Freq Reject is selected, the trigger signal is 
AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject    3    Sensitivity2

Specifies the trigger filter mode for input
channel 2.  If Normal is  selected, trigger
filtering is turned off. If Low is selected, 
noise rejection hysteresis is enabled. If Low 
Freq Reject is selected, the trigger signal is 
AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject     ?    Source

Specifies the source for the state trigger signal.

Valid Values:
1 - ZT1428_TRG_CHAN1 - Chan 1
2 - ZT1428_TRG_CHAN2 - Chan 2
3 - ZT1428_TRG_EXT   - External
4 - ZT1428_TRG_ECL0  - ECL 0
5 - ZT1428_TRG_ECL0  - ECL 1     ?    Condition

Selects the condition for the pattern that must be
present while detecting an edge on the selected
trigger source.

Valid Values:
0 - ZT1428_TRG_STAT_FALSE - False
1 - ZT1428_TRG_STAT_TRUE  - True
     ?    Slope

Specifies the trigger slope for the specified
state trigger source.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?(???    `    Error                             ?? 1    `    Level Chan1                       ?0 ~   `    Level Chan2                       ?? ?   `    Level Ext                         ?> = 0    `    Logic                             ?S +?   ?      Sensitivity1                      ?? |? 	  ?      Sensitivity2                      ?? { h          Source                            ?? @ ?          Condition                         ?? ? ?          Slope                             ?D   ?  d    Instrument Handle                  	            0.0    0.0    0.0    "XXXXX"               }Normal ZT1428_TRG_SENS_NORM Low ZT1428_TRG_SENS_LOW Low Freq Reject ZT1428_TRG_SENS_LFR High Freq Reject ZT1428_TRG_SENS_HFR               }Normal ZT1428_TRG_SENS_NORM Low ZT1428_TRG_SENS_LOW Low Freq Reject ZT1428_TRG_SENS_LFR High Freq Reject ZT1428_TRG_SENS_HFR               tChan 1 ZT1428_TRG_CHAN1 Chan 2 ZT1428_TRG_CHAN2 External ZT1428_TRG_EXT ECL 0 ZT1428_TRG_ECL0 ECL 1 ZT1428_TRG_ECL1  6  False ZT1428_TRG_STAT_FALSE True ZT1428_TRG_STAT_TRUE  . + ZT1428_TRG_SLOPE_POS - ZT1428_TRG_SLOPE_NEG    handle    ?    Standard

Specifies which TV standard to use.
525 - United States(60Hz) NTSC
625 - European(50Hz) PAL

Valid Range:
525 - ZT1428_TRG_TV_STAN_525 - NTSC
625 - ZT1428_TRG_TV_STAN_625 - PAL
     ?    Field

Specifies the field for the standard video signal.
This determines the line availability.

Valid Range:
1 - ZT1428_TRG_TV_FIELD1 - Field 1
2 - ZT1428_TRG_TV_FIELD2 - Field 2        Line

Specifies which line in the TV signal will 
generate a trigger event.

Valid Range depends upon Standard and Field
   Field  Standard     Range
     1      525        1 to 263
     2      525        1 to 262
     1      625        1 to 313
     2      625        314 to 625
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    Slope

Specifies the trigger slope for the specified
source.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
     ?    Source

Specifies the source for the trigger signal.

Valid Values:
1 - ZT1428_TRG_CHAN1 - Chan 1
2 - ZT1428_TRG_CHAN2 - Chan 2
3 - ZT1428_TRG_EXT   - External
4 - ZT1428_TRG_ECL0  - ECL 0
5 - ZT1428_TRG_ECL0  - ECL 1     ?    Level

Specifies the trigger level of the selected source
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.        Sensitivity

Specifies the trigger filter mode.  If Normal is 
selected, trigger filtering is turned off. If Low
is selected, noise rejection hysteresis is enabled.
If Low Freq Reject is selected, the trigger signal
is AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?H ? x          Standard                          ? ? ?          Field                             ?? ?;     `    Line                              ?? ????    `    Error                             ?] QH          Slope                             ? @ f          Source                            ?? Q ?    `    Level                             Ā E?   ?      Sensitivity                       ƣ   ?  d    Instrument Handle                6  625 ZT1428_TRG_TV_STAN_625 525 ZT1428_TRG_TV_STAN_525  .  2 ZT1428_TRG_TV_FIELD2 1 ZT1428_TRG_TV_FIELD1    1    	          . + ZT1428_TRG_SLOPE_POS - ZT1428_TRG_SLOPE_NEG               tChan 1 ZT1428_TRG_CHAN1 Chan 2 ZT1428_TRG_CHAN2 External ZT1428_TRG_EXT ECL 0 ZT1428_TRG_ECL0 ECL 1 ZT1428_TRG_ECL1    0.0               }Normal ZT1428_TRG_SENS_NORM Low ZT1428_TRG_SENS_LOW Low Freq Reject ZT1428_TRG_SENS_LFR High Freq Reject ZT1428_TRG_SENS_HFR    handle    ?    Trigger Event

Specifies the variable name in which to place the
trigger event status. Reading the trigger event
status clears the trigger event status.

Values Returned: 
0 - No trigger Event
1 - Trigger Event Occurred.     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?? h ?     `    Trigger Event                     ˴ ????    `    Error                             ?% ? #  ?  d    Instrument Handle                  	            	            handle    z    Channel

Selects the channel to be read back.

Valid Range:
1  - ZT1428_CHAN1 - Channel 1
2  - ZT1428_CHAN2 - Channel 2
     i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     ?    Range

Returns the full scale acquisition range in volts
for the specified input channel.

Valid Range depends upon probe attenuation (P):
0.008 * P to 50 * P    j    Offset

Returns the DC offset voltage that is represented
at vertical center for the selected channel.

Valid Range depends upon range and probe 
attenuation (P):

        Channel range       Offset Limit
      8mV * P to 400mV * P    ?2V * P
  > 400mV * P to  2.0V * P    ?10V * P
   > 2.0V * P to 10.0V * P    ?50V * P
  > 10.0V * P to 50.0V * P    ?250V * P    ?    Coupling

Returns the input coupling for the selected channel.
The coupling for each channel can be set to AC, DC,
or DCFifty, or ACLFR. DCFifty is DC coupling with 
50 ohm impedance. ACLFR is AC coupling which also 
selects an internal high pass filter to reject 
frequencies below approximately 450Hz. 

Valid Range:
0 - ZT1428_VERT_COUP_AC    - AC 1M? (10 Hz)
1 - ZT1428_VERT_COUP_ACLFR - AC 1M? (450 Hz)
2 - ZT1428_VERT_COUP_DC    - DC 1M?
3 - ZT1428_VERT_COUP_DCF   - DC 50?        Probe Attenuation

Returns the probe's attenuation factor for the 
specified channel.  The probe attenuation changes 
the reference constants for scaling the vertical 
range and offset, automatic measurements, trigger 
levels, etc. 

Valid Range:
0.9 to 1000.0    u    Lowpass Filter

Returns the state of an internal lowpass filter. 
When OFF, the lowpass filter is bypassed, providing
approximately 250 MHz bandwidth.  The bandwidth 
limit filter may be used with all coupling 
selections. 

Valid Range:
0 - ZT1428_VERT_FILT_OFF   - Off
1 - ZT1428_VERT_FILT_30MHZ - 30 MHz Lowpass Filter
2 - ZT1428_VERT_FILT_1MHZ  - 1 MHz Lowpass Filter     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?h 3 2          Channel                           ?? ????    `    Error                             ?[ ? ?    `    range                             ? ?    `    offset                            ?u 4 ?     `    Coupling                          ?^ 3?    `    Probe Attenuation                 ?l 3     `    Lowpass Filter                    ?? !  ?  d    Instrument Handle                $ CH 1 ZT1428_CHAN1 CH 2 ZT1428_CHAN2    	            	           	           	            	           	            handle    i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     ?    Range

Returns the full scale acquisition range in volts
for the specified input channel.

Valid Range depends upon probe attenuation (P):
0.008 * P to 50 * P     ?    Trigger Mode

Returns the trigger mode to enable automatic
triggering in absence of trigger event. 

0 - ZT1428_ACQ_AUTO - Auto
1 - ZT1428_ACQ_SING - Single
2 - ZT1428_ACQ_TRIG - Triggered
     ?    Number of Points

Returns the number of points for each waveform.

Valid Range depends upon Sample Interval:
100 to Max_points

Max_points = 125,000 for Sample Interval > 10 us
Max_points = 1,000,000 for Sample Interval <= 10 us
     ?    Timebase Reference

Returns the timebase reference to the left, 
center, or right of the active waveform.

0 - ZT1428_ACQ_LEFT  - Left
1 - ZT1428_ACQ_CENT  - Center
2 - ZT1428_ACQ_RIGHT - Right     ?    Sample Interval

Returns the acquisition sampling interval in 
seconds.

Valid Range:   
20 ps (50 GS/s) to 1 sec (1 S/s) in 1, 2, 4 steps    ?    Acquire Type

Returns the type of acquisition that is to take
place when a Digitize or Run command is executed.
In Normal mode, a single waveform is captured.
In Average mode, multiple captured waveforms are
averaged. In Envelope mode, the minimum and maximum
values of multiple captured waveforms are used to 
create an envelope.

Valid Range:
0 - ZT1428_ACQ_NORM - Normal
1 - ZT1428_ACQ_AVER - Average
2 - ZT1428_ACQ_ENV  - Envelope
    ?    Acquire Count

Returns the acquisition count for repetitive 
aquisition modes. In Normal mode, this parameter 
is ignored. In Average mode, this specifies the 
number of waveforms to be averaged before the 
acquisition is complete.  In Envelope mode, this 
specifies the number of waveforms for which to 
capture minimum and maximumvalues before the
acquisition is complete.

Valid Range:
1 to 2048
     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ׌ ????    `    Error                             ?? 4?    `    Timebase Delay                    إ ?      `    Trigger Mode                      ?l 5      `    Number of Points                  ?[ 4     `    Timebase Reference                ?& 4 ?    `    Sample Interval                   ۺ ? ?     `    Acquire Type                      ?w ?     `    Acquire Count                     ?    ?  d    Instrument Handle                  	            	           	            	            	            	           	            	            handle    i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

     ?    Function State

Returns the function on/off state. Unused math 
functions should be disabled to decrease waveform
processing time.

Valid Values:
0 - ZT1428_FUNC_OFF - Function Off
1 - ZT1428_FUNC_ON  - Function On     ?    Range

Returns the full scale range in volts
for the specified function channel.

Valid Range:
0.0 or 1E-38 to 1E+38
0.0 leaves the ZT1428VXI-calculated range and 
    offset values unchanged at the auto-calculated
    values.
     ?    Offset

Returns the DC offset in volts
for the specified function channel.

Valid Range:
-1E+38 to 1E+38

Note: A 0.0 Range Control setting leaves the 
      ZT1428VXI-calculated range and offset values
      unchanged at the auto-calculated values.     ?    Function Number

Specifies the function to be queried.

Valid Values:
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?? ????    `    Error                             ?J 8 ?     `    Function State                    ?* 8    `    Range                             ? 7?    `    Offset                            ? = B          Function Number                   ??   ?  d    Instrument Handle                  	            	            	           	          1 ZT1428_FUNC1 2 ZT1428_FUNC2    handle    i     Control Name:  Error

 Description:   Displays status relating to the
                function call.

    ?    External Mode

Returns the external connector input function.
If Trigger is specified, the external connector
is used as a trigger input and the internal clock
reference is used. If Clock is specified, an 
external 100 MHz clock must be applied to the 
external input for use as the timebase reference.
In clock mode, the external trigger function cannot
be used.

Valid Range:
0 - ZT1428_EXT_MODE_TRIG - Trigger (Internal Clock)
1 - ZT1428_EXT_MODE_CLK  - Clock (External Clock)
     ?    External Level

Returns the threshold voltage level of the 
external trigger or sample clock connected to 
the EXT TRIG input.

Valid Range:   
-2.0 V to 2.0 V     ?    External Impedance

Returns the input impedance for the external 
trigger or clock input.

Valid Range:
0 - ZT1428_EXT_IMP_1M - 1M?
1 - ZT1428_EXT_IMP_50 - 50?     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?? ????    `    Error                             ?1 T I     `    External Mode                     ? U ?    `    External Level                    ?? V|     `    External Impedance                ?m ?   ?  d    Instrument Handle                  	            	            	           	            handle    g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     ?    ECL 0

Returns the ECL 0 trigger output on or off state. 
The ECL trigger output occurs when the instrument 
detects a trigger event.

Valid Values:  
0 - ZT1428_OUT_OFF - Off
1 - ZT1428_OUT_ON  - On    ?    BNC Output

Returns the output mode of the BNC
Probe Comp/Cal/Trig Output connector.
Probe selects a 500 Hz output. Trigger 
selects a trigger output pulse upon a 
detected trigger event. SClock selects
a 10 MHz output. DC Calibrate, 0V and 5V
select DC output levels.

Valid Range:
0 - ZT1428_OUT_BNC_PROB - Probe
1 - ZT1428_OUT_BNC_TRIG - Trigger
2 - ZT1428_OUT_BNC_DC   - DC Calibrate
3 - ZT1428_OUT_BNC_0V   - 0 Volts
4 - ZT1428_OUT_BNC_5V   - 5 Volts
5 - ZT1428_OUT_BNC_SCL  - SClock    Z    BNC Voltage

Returns the active-state output voltage for the
BNC output. For the Probe, Trigger and Sclock
output modes, the signal transitions between 0V 
and this voltage level. For DC CAL mode, the DC
output voltage is set at this level. This control
is ignored for 0V and 5V output modes.

Valid Range:
-3.5V to +8.5V (into high impedance)
     ?    ECL 1

Returns the ECL 1 trigger output on or off state. 
The ECL trigger output occurs when the instrument 
detects a trigger event.

Valid Values:  
0 - ZT1428_OUT_OFF - Off
1 - ZT1428_OUT_ON  - On     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?2???    `    Error                             ?? 8     `    ECL 0                             ?r ; %     `    BNC Output                        ?d : ?    `    BNC Voltage                       ?? :?     `    ECL 1                             ?? '  ?  d    Instrument Handle                  	            	            	            	           	            handle    g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     ?    Level Chan1

Returns the trigger level of input channel 1
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     ?    Level Chan2

Returns the trigger level of input channel 2
in Volts.

Valid Range:
?0.75 of the current voltage range from the current
offset.     k    Level Ext

Returns the trigger level of the external trigger
input in Volts.

Valid Range:
-2.0V to +2.0V    1    Sensitivity1

Returns the trigger filter mode for input
channel 1.  If Normal is  selected, trigger
filtering is turned off. If Low is selected, 
noise rejection hysteresis is enabled. If Low 
Freq Reject is selected, the trigger signal is 
AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject    1    Sensitivity2

Returns the trigger filter mode for input
channel 2.  If Normal is  selected, trigger
filtering is turned off. If Low is selected, 
noise rejection hysteresis is enabled. If Low 
Freq Reject is selected, the trigger signal is 
AC coupled with a 50 kHz high-pass filter. If
High Freq Reject is selected, the trigger signal
is filtered with a 50 kHz low-pass filter.

Valid Range:
0 - ZT1428_TRG_SENS_NORM - Normal
1 - ZT1428_TRG_SENS_LOW  - Low (Noise Reject)
2 - ZT1428_TRG_SENS_LFR  - Low Freq Reject
3 - ZT1428_TRG_SENS_HFR  - High Freq Reject     ?    Slope Chan1

Returns the trigger slope for the input
channel 1.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
     ?    Slope Chan2

Returns the trigger slope for the input
channel 2.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
     ?    Slope Ext

Returns the trigger slope for the external
trigger input.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
     ?    Slope ECL0

Returns the trigger slope for the ECLTRG0
trigger input.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
     ?    Slope ECL1

Returns the trigger slope for the ECLTRG1
trigger input.

Valid Value:
0 - ZT1428_TRG_SLOPE_NEG - Negative slope
1 - ZT1428_TRG_SLOPE_POS - Positive slope
     ?    Source

Returns the source for the trigger signal.

Valid Values:
1 - ZT1428_TRG_CHAN1 - Chan 1
2 - ZT1428_TRG_CHAN2 - Chan 2
3 - ZT1428_TRG_EXT   - External
4 - ZT1428_TRG_ECL0  - ECL 0
5 - ZT1428_TRG_ECL0  - ECL 1     ?    Trigger Mode

Returns the selected trigger mode.

Valid Values:
0 - ZT1428_TRG_MODE_EDGE - Edge
1 - ZT1428_TRG_MODE_PATT - Pattern
2 - ZT1428_TRG_MODE_STAT - State
3 - ZT1428_TRG_MODE_TV   - TV     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.    ?????    `    Error                             ? 5    `    Level Chan1                       ?? 4?    `    Level Chan2                       ?: ?     `    Level Ext                         ?? ~ ?     `    Sensitivity1                      ?? ~     `    Sensitivity2                      ? |?     `    Slope Chan1                       ?? ?  	    `    Slope Chan2                       ?w ? ? 
    `    Slope Ext                         ?( ?     `    Slope ECL0                        ?? ??     `    Slope ECL1                        ?? 8      `    Source                            ?k 7 ?     `    Trigger Mode                      6   ?  d    Instrument Handle                  	            	           	           	           	            	            	            	            	            	            	            	            	            handle    g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     ?    Holdoff Value

Returns the holdoff time in seconds or the holdoff
events by number of events. This value depends upon
the setting for the Holdfoff Type.

Valid Values:
40 ns to 320 ms
1 to 65536 events    ?    Logic

Returns the logical relationship between 
the signal and the defined voltage level that 
must exist before the pattern is considered valid. 
The logic pattern to be matched uses:
  L to represent logic Low
  H to represent logic High
  X to represent Don't Care
A five-character string should be specified.
The first character is for Channel 1, 
the second for Channel 2, the third for External,
the fourth for ECLT0 and the last for ECLT1. 
        Pattern Condition

Returns the pattern condition that must be satisfied
in order to generate a trigger event.  In GT mode,
the pattern must be present for more than the GT
Time. In LT mode, the pattern must be present for
less than the LT Time. In Range mode, the pattern
must be present between the GT Time and the LT Time.

Valid Range:
0 - ZT1428_TRG_PATT_ENTER - Enter
1 - ZT1428_TRG_PATT_EXIT  - Exit
2 - ZT1428_TRG_PATT_GT    - Greater Than
3 - ZT1428_TRG_PATT_LT    - Less Than
4 - ZT1428_TRG_PATT_RANGE - Between
     ?    GT Time

Returns the greater than time in  seconds. The 
pattern must be present for more than this time 
when using either greater than mode or range mode.

Valid Values:
20 ns to 160 ms     ?    LT Time

Specifes the less than time in seconds. The pattern
must be present for less than this time when using 
either less than mode or range mode.

Valid Values:
30 ns to 160 ms     ?    State Condition

Returns the condition for the pattern that must be
present while detecting an edge on the selected
trigger source.

Valid Values:
0 - ZT1428_TRG_STAT_FALSE - False
1 - ZT1428_TRG_STAT_TRUE  - True
     ?    Standard

Returns which TV standard to use.
525 - United States(60Hz) NTSC
625 - European(50Hz) PAL

Valid Range:
525 - ZT1428_TRG_TV_STAN_525 - NTSC
625 - ZT1428_TRG_TV_STAN_625 - PAL
     ?    Field

Returns the field for the standard video signal.
This determines the line availability.

Valid Range:
1 - ZT1428_TRG_TV_FIELD1 - Field 1
2 - ZT1428_TRG_TV_FIELD2 - Field 2        Line

Returns which line in the TV signal will 
generate a trigger event.

Valid Range depends upon Standard and Field
   Field  Standard     Range
     1      525        1 to 263
     2      525        1 to 262
     1      625        1 to 313
     2      625        314 to 625
     ?    Holdoff Type

Returns the type of trigger holdoff.

Valid Values:
0 - ZT1428_TRG_HOLD_TIME  - Time
1 - ZT1428_TRG_HOLD_EVENT - Event     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.   D???    `    Error                            ? - ?    `    Holdoff Value                    ? +     `    Logic                            Q +?     `    Pattern Condition                	d t $    `    GT Time                          
) s ?    `    LT Time                          
? s!     `    State Condition                  ? q?     `    Standard                         ? ? % 	    `    Field                            F ? ? 
    `    Line                             f - #     `    Holdoff Type                     ? '  ?  d    Instrument Handle                  	            	           	            	            	           	           	            	            	            	            	            handle    g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     ?    Upper Limit

Returns the upper threshold level for 
measurements.

Valid range depends upon Units:
    Units       Range
    Percent    -25.00 to 125.00
    Volts      -250,000 V to 250,000 V     ?    Lower Limit

Returns the lower threshold level for 
measurements.

Valid range depends upon Units:
    Units       Range
    Percent    -25.00 to 125.00
    Volts      -250,000 V to 250,000 V     n    Start Edge

Returns the edge number for the start condition in 
Delay measurements.

Valid Values:
1 to 4000     l    Stop Edge

Returns the edge number for the stop condition in 
Delay measurements.

Valid Values:
1 to 4000     ?    Start Level

Returns the level for the start condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     ?    Stop Level

Returns the level for the stop condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     ?    Positive Width Level

Returns the level for the positive pulse width 
measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     ?    Negative Width Level

Returns the level for the negative pulse width 
measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper    ?    User Mode

Returns the measurement mode as either user-defined
or standard for upper, middle and lower thresholds.
This mode applies to all measurements that require 
threshold crossings. Standard values for the upper,
middle and lower thresholds are 90%, 50% and 10%.
A user threshold can be defined as either a percent
of waveform level or as a specific voltage.

Valid Values:
0 - ZT1428_MEAS_MODE_STAN - Standard
1 - ZT1428_MEAS_MODE_USER - User     ?    Units

Returns the units used for the user-defined
limits as either a percent of waveform level or
as a specific voltage.

Valid Values:
0 - ZT1428_MEAS_USER_PCT  - Percent
1 - ZT1428_MEAS_USER_VOLT - Volts     ?    Start Slope

Returns the slope for the start condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_SLOP_NEG - Negative Slope
1 - ZT1428_DEL_SLOP_POS - Positive Slope     ?    Stop Slope

Returns the slope for the stop condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_SLOP_NEG - Negative Slope
1 - ZT1428_DEL_SLOP_POS - Positive Slope     z    Instrument Handle

Accepts the Instrument Handle returned by the 
Initialize function to select the desired 
instrument.   ????    `    Error                            ? ,    `    Upper Limit                      ? +?    `    Lower Limit                      ? v     `    Start Edge                       ? u?     `    Stop Edge                        k ?   	    `    Start Level                      0 ? ? 
    `    Stop Level                       ? ?     `    Positive Width Level             ? ??     `    Negative Width Level             ? / %     `    User Mode                        R . ?     `    Units                            * w $     `    Start Slope                      ? v ?     `    Stop Slope                       ?	    ?  d    Instrument Handle                  	            	           	           	            	            	            	            	            	            	            	            	            	            handle   ?    Primary Source

Specifies the source for the measurement function. 
Valid sources include input channels, waveforms
saved in memory, and math function waveforms.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2
    ?    Secondary Source

Specifies the secondary source for the measurement
function. This is only used in delay measurements.

Valid Values:
0 - ZT1428_NONE  - None Selected
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2        Measurement

Specifies the measurement to be performed.

Valid Values:
0  - ZT1428_MEAS_RISE - Rise Time
1  - ZT1428_MEAS_FALL - Fall Time
2  - ZT1428_MEAS_FREQ - Frequency
3  - ZT1428_MEAS_PER  - Period
4  - ZT1428_MEAS_PWID - +Width
5  - ZT1428_MEAS_NWID - -Width
6  - ZT1428_MEAS_VAMP - V. Amplitude
7  - ZT1428_MEAS_VBAS - V. Base
8  - ZT1428_MEAS_VTOP - V. Top
9  - ZT1428_MEAS_VPP  - V. Peak to Peak
10 - ZT1428_MEAS_VAVG - V. Average
11 - ZT1428_MEAS_VMAX - V. Max
12 - ZT1428_MEAS_VMIN - V. Min
13 - ZT1428_MEAS_VACR - V. AC(rms)
14 - ZT1428_MEAS_VDCR - V. DC(rms)
15 - ZT1428_MEAS_DUTY - Duty Cycle
16 - ZT1428_MEAS_DEL  - Delay
17 - ZT1428_MEAS_OVER - Over Shoot
18 - ZT1428_MEAS_PRE  - Pre Shoot
19 - ZT1428_MEAS_TMAX - T. Max
20 - ZT1428_MEAS_TMIN - T. Min     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    Result

Specifies the variable name in which to place 
the result of the measurement.  If 9.9999E+37
is returned, a result for the selected measurement 
cannot be determined.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.    ? 8 }          Primary Source                   "I 8          Secondary Source                 #? >\    ?    Measurement                      &? ????    `    Error                            'Z ?l    `    Result                           (   ?  d    Instrument Handle                             ?Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Mem 1 ZT1428_WMEM1 Mem 2 ZT1428_WMEM2 Mem 3 ZT1428_WMEM3 Mem 4 ZT1428_WMEM4 Function 1 ZT1428_FUNC1 Function 2 ZT1428_FUNC2            	   ?None ZT1428_NONE Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Mem 1 ZT1428_WMEM1 Mem 2 ZT1428_WMEM2 Mem 3 ZT1428_WMEM3 Mem 4 ZT1428_WMEM4 Function 1 ZT1428_FUNC1 Function 2 ZT1428_FUNC2              !Rise Time ZT1428_MEAS_RISE Fall Time ZT1428_MEAS_FALL Frequency ZT1428_MEAS_FREQ Period ZT1428_MEAS_PER + Width ZT1428_MEAS_PWID - Width ZT1428_MEAS_NWID V. Amplitude ZT1428_MEAS_VAMP V. Base ZT1428_MEAS_VBAS V. Top ZT1428_MEAS_VTOP V. P. to P. ZT1428_MEAS_VPP V. Average ZT1428_MEAS_VAVG V. Max ZT1428_MEAS_VMAX V. Min ZT1428_MEAS_VMIN V. AC(rms) ZT1428_MEAS_VACR V. DC(rms) ZT1428_MEAS_VDCR Duty Cycle ZT1428_MEAS_DUTY Delay ZT1428_MEAS_DEL Over Shoot ZT1428_MEAS_OVER Pre Shoot ZT1428_MEAS_PRE T. Max ZT1428_MEAS_TMAX T. Min ZT1428_MEAS_TMIN    	            	           handle    ?    Units

Specifies the units used for the user-defined
limits as either a percent of waveform level or
as a specific voltage.

Valid Values:
0 - ZT1428_MEAS_USER_PCT  - Percent
1 - ZT1428_MEAS_USER_VOLT - Volts     ?    Upper Level

Specifies the upper threshold level for 
measurements.

Valid range depends upon Units:
    Units       Range
    Percent    -25.00 to 125.00
    Volts      -250,000 V to 250,000 V     ?    Lower Level

Specifies the lower threshold level for 
measurements.

Valid range depends upon Units:
    Units       Range
    Percent    -25.00 to 125.00
    Volts      -250,000 V to 250,000 V     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
    ?    User Mode

Defines the measurement mode as either user-defined
or standard for upper, middle and lower thresholds.
This mode applies to all measurements that require 
threshold crossings. Standard values for the upper,
middle and lower thresholds are 90%, 50% and 10%.
A user threshold can be defined as either a percent
of waveform level or as a specific voltage.

Valid Values:
0 - ZT1428_MEAS_MODE_STAN - Standard
1 - ZT1428_MEAS_MODE_USER - User     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   -? X ?          Units                            .? _    `    Upper Level                      /O `?    `    Lower Level                      0 ????    `    Error                            0? X 2          User Mode                        2U ? !  ?  d    Instrument Handle                9  Volts ZT1428_MEAS_USER_VOLT Percent ZT1428_MEAS_USER_PCT    90.0    10.0    	          :  User ZT1428_MEAS_MODE_USER Standard ZT1428_MEAS_MODE_STAN    handle    ?    Start Slope

Specifies the slope for the start condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_SLOP_NEG - Negative Slope
1 - ZT1428_DEL_SLOP_POS - Positive Slope     i    Start Edge

Specifies the edge for the start condition in 
Delay measurements.

Valid Values:
1 to 4000     ?    Start Level

Specifies the level for the start condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     ?    Stop Slope

Specifies the slope for the stop condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_SLOP_NEG - Negative Slope
1 - ZT1428_DEL_SLOP_POS - Positive Slope     g    Stop Edge

Specifies the edge for the stop condition in 
Delay measurements.

Valid Values:
1 to 4000     ?    Stop Level

Specifies the level for the stop condition in 
Delay measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   4? 8 u          Start Slope                      5q 8 ?     `    Start Edge                       5? 8?          Start Level                      6? ? x          Stop Slope                       7b ? ?     `    Stop Edge                        7? ??          Stop Level                       8?]??    `    Error                           ????  ???                                         ???? t ???                                          9 "  ?  d    Instrument Handle                , + ZT1428_DEL_SLOP_POS - ZT1428_DEL_SLOP_NEG    1              LLower ZT1428_DEL_LEV_LOW Middle ZT1428_DEL_LEV_MID Upper ZT1428_DEL_LEV_UPP  , + ZT1428_DEL_SLOP_POS - ZT1428_DEL_SLOP_NEG    1              LLower ZT1428_DEL_LEV_LOW Middle ZT1428_DEL_LEV_MID Upper ZT1428_DEL_LEV_UPP    	            Start Condition    Stop Condition    handle    h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    Positive Width Level

Specifies the level for the positive pulse width 
measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     ?    Negative Width Level

Specifies the level for the negative pulse width 
measurements.

Valid Values:
0 - ZT1428_DEL_LEV_LOW - Lower
1 - ZT1428_DEL_LEV_MID - Middle
2 - ZT1428_DEL_LEV_UPP - Upper     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   <? ????    `    Error                            =e [ ?          Positive Width Level             >1 \]          Negative Width Level             >? ? &  ?  d    Instrument Handle                  	                      LLower ZT1428_DEL_LEV_LOW Middle ZT1428_DEL_LEV_MID Upper ZT1428_DEL_LEV_UPP              LLower ZT1428_DEL_LEV_LOW Middle ZT1428_DEL_LEV_MID Upper ZT1428_DEL_LEV_UPP    handle   ?    Limit Test

Controls the on/off state of the limit testing.
If limit testing is enabled, the high, low and
pass/fail statistics are recorded for the specified
measurement over many continuous acquisitions. 
The continuous acquisition is started by a run 
command. The results of the limit test are returned
in the result statistics and limit test event 
register.

Valid Values:
0 - ZT1428_MEAS_LIM_OFF - Limit Test Off
1 - ZT1428_MEAS_LIM_ON  - Limit Test On
     y    Upper Limit

Specifies the upper limit of the measurement
limit test comparison.

Valid Values depends upon Measurement     y    Lower Limit

Specifies the lower limit of the measurement
limit test comparison.

Valid Values depends upon Measurement    
    Post Failure

Specifies the postfailure condition.  If set to 
Stop, the instrument will stop acquiring waveforms
after a limit test comparison failure.

Valid Values:
0 - ZT1428_MEAS_POST_STOP - Stop upon Failure
1 - ZT1428_MEAS_POST_CONT - Continue upon Failure
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
    ?    Primary Source

Specifies the source for the measurement function. 
Valid sources include input channels, waveforms
saved in memory, and math function waveforms.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2
    ?    Secondary Source

Specifies the secondary source for the measurement
function. This is only used in delay measurements.

Valid Values:
0 - ZT1428_NONE  - None Selected
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2    ?    Measurement

Specifies the measurement to be performed.

Valid Values:
0  - ZT1428_MEAS_RISE - Rise Time
1  - ZT1428_MEAS_FALL - Fall Time
2  - ZT1428_MEAS_FREQ - Frequency
3  - ZT1428_MEAS_PER  - Period
4  - ZT1428_MEAS_PWID - +Width
5  - ZT1428_MEAS_NWID - -Width
6  - ZT1428_MEAS_VAMP - V. Amplitude
7  - ZT1428_MEAS_VBAS - V. Base
8  - ZT1428_MEAS_VTOP - V. Top
9  - ZT1428_MEAS_VPP  - V. Peak to Peak
10 - ZT1428_MEAS_VAVG - V. Average
11 - ZT1428_MEAS_VMAX - V. Max
12 - ZT1428_MEAS_VMIN - V. Min
13 - ZT1428_MEAS_VACR - V. AC(rms)
14 - ZT1428_MEAS_VDCR - V. DC(rms)
15 - ZT1428_MEAS_DUTY - Duty Cycle
16 - ZT1428_MEAS_DEL  - Delay
17 - ZT1428_MEAS_OVER - Over Shoot
18 - ZT1428_MEAS_PRE  - Pre Shoot    ?    Statistics

Controls the on/off state of the statistics.
If statistics are enabled, the high, low and
average statistics are recorded for the specified
measurement over many continuous acquisitions. 
The continuous acquisition is started by a run 
command. The measurement statistics are returned 
in the result statistics.

Valid Values:
0 - ZT1428_MEAS_STAT_OFF - Statistics Off
1 - ZT1428_MEAS_STAT_ON  - Statistics On
    _    Destination

Specifies the destination for the waveform to be
stored when a limit test fails. 

Note:  When storing in envelope acq. mode, the
min. and max. waveforms are stored in two memories.
For example, storing the wave to memory 1 will 
place the min. waveform in memory 1 and the max. 
waveform in memory 2.  Memories are grouped as 1
& 2, and 3 & 4.  Selecting 1 or 2 has the same 
effect. Selecting 3 or 4 has the same effect.

Valid Values:
0 - ZT1428_NONE  - Not Saved Upon Failure
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   A! ( -          Limit Test                       B? ? '    `    Upper Limit                      Cx ? ?    `    Lower Limit                      C? ?,          Postfailure                      E???    `    Error                            E{ . ?          Primary Source                   G /-          Secondary Source                 H? Rv    ?    Measurement                      K u -          Statistics                       M/ ?? 	         Destination                      O? '  ?  d    Instrument Handle                .  On ZT1428_MEAS_LIM_ON Off ZT1428_MEAS_LIM_OFF    0.0    0.0  <  Continuous ZT1428_MEAS_POST_CONT Stop ZT1428_MEAS_POST_STOP    	                       ?Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Mem 1 ZT1428_WMEM1 Mem 2 ZT1428_WMEM2 Mem 3 ZT1428_WMEM3 Mem 4 ZT1428_WMEM4 Function 1 ZT1428_FUNC1 Function 2 ZT1428_FUNC2            	   ?None ZT1428_NONE Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Mem 1 ZT1428_WMEM1 Mem 2 ZT1428_WMEM2 Mem 3 ZT1428_WMEM3 Mem 4 ZT1428_WMEM4 Function 1 ZT1428_FUNC1 Function 2 ZT1428_FUNC2              ?Rise Time ZT1428_MEAS_RISE Fall Time ZT1428_MEAS_FALL Frequency ZT1428_MEAS_FREQ Period ZT1428_MEAS_PER + Width ZT1428_MEAS_PWID - Width ZT1428_MEAS_NWID V. Amplitude ZT1428_MEAS_VAMP V. Base ZT1428_MEAS_VBAS V. Top ZT1428_MEAS_VTOP V. P. to P. ZT1428_MEAS_VPP V. Average ZT1428_MEAS_VAVG V. Max ZT1428_MEAS_VMAX V. Min ZT1428_MEAS_VMIN V. AC(rms) ZT1428_MEAS_VACR V. DC(rms) ZT1428_MEAS_VDCR Duty Cycle ZT1428_MEAS_DUTY Delay ZT1428_MEAS_DEL Over Shoot ZT1428_MEAS_OVER Pre Shoot ZT1428_MEAS_PRE    On 1 Off 0               lOff ZT1428_NONE  Memory 1 ZT1428_WMEM1  Memory 2 ZT1428_WMEM2  Memory 3 ZT1428_WMEM3  Memory 4 ZT1428_WMEM4    handle       Mask

Selects the mask waveforms to which the source will 
be compared. The maximum waveform mask is stored in
Memory 1 or 3. The minimum waveform mask is stored
in Memory 2 or 4.

Valid Values:
3 - ZT1428_WMEM1 - Memory 1 & 2
5 - ZT1428_WMEM3 - Memory 3 & 4     ?    Allowance

Specifies the allowable number of divisions that 
the waveform mask comparison test can deviate
from and still pass. One division is 1/8 of the
full-scale range of the selected input source.

Valid Values:
0.0 to 8.0 divisions     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    Source

Specifies the source for the mask test function. 
Valid sources include the two input channels.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2    	    Post Failure

Specifies the postfailure condition.  If set to 
Stop, the instrument will stop acquiring waveforms
after a mask test comparison failure.

Valid Values:
0 - ZT1428_MEAS_POST_STOP - Stop upon Failure
1 - ZT1428_MEAS_POST_CONT - Continue upon Failure
    ?    Destination

Specifies the destination for the waveform to be
stored when a mask test fails. Data may be stored 
to any of the waveform memories EXCEPT the pair of
memories used for the mask in the comparison test.

Valid Values:
0 - ZT1428_NONE  - Not Saved Upon Failure
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
    ?    Mask Test

Controls the on/off state of the mask testing.
If mask testing is enabled, the source input
is compared to the mask over many continuous 
acquisitions.  The continuous acquisition is 
started by a run command. The result of the 
mask test is returned in the limit test event 
register.

Valid Values:
0 - ZT1428_MEAS_MASK_OFF - Mask Test Off
1 - ZT1428_MEAS_MASK_ON  - Mask Test On
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   W I?          Mask                             X ? ]    `    Allowance                        Y???    `    Error                            Yt J          Source                           Z. ? ?          Postfailure                      [? ??          Destination                      \? M q          Mask Test                        ^\    ?  d    Instrument Handle                             .Mem 1 & 2 ZT1428_WMEM1 Mem 3 & 4 ZT1428_WMEM3    0.0    	                       .Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2  <  Continuous ZT1428_MEAS_POST_CONT Stop ZT1428_MEAS_POST_STOP               lOff ZT1428_NONE  Memory 1 ZT1428_WMEM1  Memory 2 ZT1428_WMEM2  Memory 3 ZT1428_WMEM3  Memory 4 ZT1428_WMEM4  0  On ZT1428_MEAS_MASK_ON Off ZT1428_MEAS_MASK_OFF    handle    k    Minimum

Specifies the name of the variable into which 
the minimum result of the measurement is placed.
     ?    Average Pass Ratio

Specifies the name of the variable into which 
the average (Statistics Mode) or pass ratio
(Limit Test Mode) result of the measurement is 
placed.     j    Maximum

Specifies the name of the variable into which 
the maximum result of the measurement is placed.     i    Current

Specifies the name of the variable into which the
current result of the measurement is placed.     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    Limit Test Result

Specifies the name of the variable into which 
the result of the limit test or mask test 
comparison is placed.

Valid Values:
0 - Passed
1 - Failed
    ?    Measurement

Specifies the measurement results to be returned.

Valid Values:
0  - ZT1428_MEAS_RISE - Rise Time
1  - ZT1428_MEAS_FALL - Fall Time
2  - ZT1428_MEAS_FREQ - Frequency
3  - ZT1428_MEAS_PER  - Period
4  - ZT1428_MEAS_PWID - +Width
5  - ZT1428_MEAS_NWID - -Width
6  - ZT1428_MEAS_VAMP - V. Amplitude
7  - ZT1428_MEAS_VBAS - V. Base
8  - ZT1428_MEAS_VTOP - V. Top
9  - ZT1428_MEAS_VPP  - V. Peak to Peak
10 - ZT1428_MEAS_VAVG - V. Average
11 - ZT1428_MEAS_VMAX - V. Max
12 - ZT1428_MEAS_VMIN - V. Min
13 - ZT1428_MEAS_VACR - V. AC(rms)
14 - ZT1428_MEAS_VDCR - V. DC(rms)
15 - ZT1428_MEAS_DUTY - Duty Cycle
16 - ZT1428_MEAS_DEL  - Delay
17 - ZT1428_MEAS_OVER - Over Shoot
18 - ZT1428_MEAS_PRE  - Pre Shoot     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   b H/    `    Minimum                          b ? !    `    Average Pass Ratio               c/ H?    `    Maximum                          c? I ?    `    Current                          d???    `    Error                            d? ? ?     `    Limit Test Result                e4 T     ?    Measurement                      h '  ?  d    Instrument Handle                  	           	           	           	           	            	                      ?Rise Time ZT1428_MEAS_RISE Fall Time ZT1428_MEAS_FALL Frequency ZT1428_MEAS_FREQ Period ZT1428_MEAS_PER + Width ZT1428_MEAS_PWID - Width ZT1428_MEAS_NWID V. Amplitude ZT1428_MEAS_VAMP V. Base ZT1428_MEAS_VBAS V. Top ZT1428_MEAS_VTOP V. P. to P. ZT1428_MEAS_VPP V. Average ZT1428_MEAS_VAVG V. Max ZT1428_MEAS_VMAX V. Min ZT1428_MEAS_VMIN V. AC(rms) ZT1428_MEAS_VACR V. DC(rms) ZT1428_MEAS_VDCR Duty Cycle ZT1428_MEAS_DUTY Delay ZT1428_MEAS_DEL Over Shoot ZT1428_MEAS_OVER Pre Shoot ZT1428_MEAS_PRE    handle    ?    Channel(s)

Selects the channel(s) to be digitized.

Valid Range:
1  - ZT1428_CHAN1     - Channel 1
2  - ZT1428_CHAN2     - Channel 2
10 - ZT1428_CHAN_BOTH - Channels 1 & 2     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
        Mode

Specifies the mode to be used for a digitize 
operation. Normal operation uses the operation
complete query to halt all instrument communication
unitl the digitize operation is complete. 
Asynchronous digitize mode sets the instrument to
use its status register reporting to identify when
the digitize operation is complete. Asynchronous
digitize mode should only be used by advanced
users familier with the IEEE-488 status register
reporting structures.

Valid Values:
0 - ZT1428_DIG_NORM - Normal
1 - ZT1428_DIG_ASYN - Asynchronous
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   l? H ?          Channel(s)                       m; ????    `    Error                            m? H<          Mode                             o? ? '  ?  d    Instrument Handle                             DChan 1 ZT1428_CHAN1 Chan 2 ZT1428_CHAN2 Chan 1 & 2 ZT1428_CHAN_BOTH    	          4  Asynchronous ZT1428_DIG_ASYN Normal ZT1428_DIG_NORM    handle       Dig Complete

Specifies the variable name in which to place the
status of an on-going digitize operation. A 
returned value of 1 indicates that the digitize
operation is complete. A returned value of 0 
indicates that the operation is still in progress.

     g     Control Name:  Error

 Description:   Displays status relating to the
               function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   q? h ?     `    Dig Complete                     r? ????    `    Error                            sA ? "  ?  d    Instrument Handle                  	            	            handle   ?    Waveform Array

Specifies the name of array in which to place the 
waveform data. The data is returned as an array
of floating point numbers that represents the 
acquired waveform in voltage units.

Note: When the acquisition mode is set to envelope,
two arrays will be returned.  They will both be
placed in this array.  The first half of the array 
will be an array of minimums.  The second half of
the array will be an array of maximums.
     ?    Sample Interval

Specifies the variable name in which to place the 
sample interval in seconds at which the waveform
was digitized (i.e. time interval between points).
    ?    Transfer Type

Specifies the type of data transfer to be used.  
A32 transfers are only available with VXI 
(non-GPIB) interfaces.

For Preamble transfers, the waveform arrway will
not be returned.  Only the preamble data is 
returned.

Note: A32 transfers can only be used with Input 
channels and Math Function channels.  The
memories must be read using word-serial.

Valid Values:
0 - ZT1428_TRAN_SER - Word Serial
1 - ZT1428_TRAN_A32 - A32
2 - ZT1428_TRAN_PRE - Preamble     ?    Time Offset

Specifies the variable name in which to place the 
time of the first data point in seconds relative to
the trigger point.
    1    Number of Points

Specifies the variable name in which to place the 
number of points read from the selected waveform. 

Note: If the acquisition type is set to envelope 
then this number is the length of the entire array 
returned.  Divide this number by two to get the
length of each individual array.     i    X Reference

Specifies the variable name in which to place the
horizontal axis trigger reference point.     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
    ?    Source

Specifies the source waveform to be read. 
Valid sources include input channels, waveforms
saved in memory, and math function waveforms.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2
     ?    Acquistion Count

Specifies the variable name in which to place the
acquired waveform count used to create the selected
average or envelope waveform. In Normal acquisition
the Acquisition Count is always 1.      ?    Volt Increment

Specifies the variable name in which to place the 
voltage increment in volts at which the waveform
was digitized (voltage increment between LSBs).
     ?    Volt Offset

Specifies the variable name in which to place the 
zero-voltage reference or DC offset voltage for 
the specified waveform.
     g    Y Reference

Specifies the variable name in which to place the
vertical axis voltage reference point.     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   t? 1 ?    `    Waveform Array                   vI ~ ?    `    Sample Interval                  v? ? V   ? P    Transfer Type                    x? }    `    Time Offset                      yp 2     `    Number of Points                 z? }?     `    X Reference                      {???    `    Error                           ???? ???                                         ???? ???                                          {? 4 Y          Source                           } 1?     `    Acquisition Count                }? ? ? 	   `    Volt Increment                   ~? ? 
   `    Volt Offset                      4 ??     `    Y Reference                      ?   ?  d    Instrument Handle                  	           	                      IA32 ZT1428_TRAN_A32 Word Serial ZT1428_TRAN_SER Preamble ZT1428_TRAN_PRE    	           	            	            	            $Note: Waveforms should be digitized    before they are read.               ?Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Mem 1 ZT1428_WMEM1 Mem 2 ZT1428_WMEM2 Mem 3 ZT1428_WMEM3 Mem 4 ZT1428_WMEM4 Function 1 ZT1428_FUNC1 Function 2 ZT1428_FUNC2    	            	           	           	            handle       Destination

Specifies the destination for the waveform to be
stored. 

Note:  When storing in envelope acq. mode, the
min. and max. waveforms are stored in two memories.
For example, storing the wave to memory 1 will 
place the min. waveform in memory 1 and the max. 
waveform in memory 2.  Memories are grouped as 1
& 2, and 3 & 4.  Selecting 1 or 2 has the same 
effect. Selecting 3 or 4 has the same effect.

Valid Values:
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
    ?    Source

Specifies the source waveform to be stored. 
Valid sources include input channels, waveforms
saved in memory, and math function waveforms.

Valid Values:
1 - ZT1428_CHAN1 - Channel 1
2 - ZT1428_CHAN2 - Channel 2
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
7 - ZT1428_FUNC1 - Function 1
8 - ZT1428_FUNC2 - Function 2
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ? Hi          Destination                      ?: ????    `    Error                            ?? 9 ?          Source                           ?< ?   ?  d    Instrument Handle                             \ Memory 1 ZT1428_WMEM1  Memory 2 ZT1428_WMEM2  Memory 3 ZT1428_WMEM3  Memory 4 ZT1428_WMEM4    	                       ?Channel 1 ZT1428_CHAN1 Channel 2 ZT1428_CHAN2 Mem 1 ZT1428_WMEM1 Mem 2 ZT1428_WMEM2 Mem 3 ZT1428_WMEM3 Mem 4 ZT1428_WMEM4 Function 1 ZT1428_FUNC1 Function 2 ZT1428_FUNC2    handle    ?    Destination

Specifies the destination for the waveform to be
stored. 

Valid Values:
3 - ZT1428_WMEM1 - Memory 1
4 - ZT1428_WMEM2 - Memory 2
5 - ZT1428_WMEM3 - Memory 3
6 - ZT1428_WMEM4 - Memory 4
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     ?    Waveform Array

Specifies the name of array of waveform data to
be stored. The waveform is specified in voltage
units and converted to codes according to the
preamble settings.
     ?    Sample Interval

Specifies the sample interval in seconds at which
the waveform to be stored was digitized (i.e. time
interval between points).
     ?    Time Offset

Specifies the time of the first data point in 
seconds relative to the trigger point of the 
waveform to be stored.
     Z    Number of Points

Specifies the number of points to be stored to
the selected waveform.      c    X Reference

Specifies the horizontal axis trigger reference 
point of the waveform to be stored.     ?    Volt Increment

Specifies the voltage increment in volts at which
the waveform to be stored was digitized (voltage 
increment between LSBs).
     g    Volt Offset

Specifies the zero-voltage reference or DC offset
voltage of the waveform to be stored.
     `    Y Reference

Specifies the vertical axis voltage reference point
of the waveform to be stored.     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? ( ?          Destination                      ?????    `    Error                            ? < ?    `    Waveform Array                   ??  U    `    Sample Interval                  ?c ~ ?    `    Time Offset                      ?? ;I     `    Number of Points                 ?P ~K     `    X Reference                      ?? ? U    `    Volt Increment                   ?R ? ?    `    Volt Offset                      ?? ?J 	    `    Y Reference                      ?)   ?  d    Instrument Handle                             \ Memory 1 ZT1428_WMEM1  Memory 2 ZT1428_WMEM2  Memory 3 ZT1428_WMEM3  Memory 4 ZT1428_WMEM4    	                
2.0000E-6    -5.0000E-4    500    0    
1.2207E-4    0.0    16384    handle    h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? ????    `    Error                            ?- ?   ?  d    Instrument Handle                  	            handle    h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?1 ????    `    Error                            ?? ?    ?  d    Instrument Handle                  	            handle    ?    Result

Specifies the variable name in which to place the 
result of the self test.  If zero is returned, the 
self test passed.
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? h ?     `    Result                           ?0 ????    `    Error                            ?? ? !  ?  d    Instrument Handle                  	            	            handle    ?    State

Specifies state in which to place the instrument.
Run enables continuous acquisition.  Stop disables
an on-going acquisition.

Valid Values:
0 - ZT1428_STOP - Stop
1 - ZT1428_RUN  - Run
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? X ?          State                            ?? ????    `    Error                            ?  %  ?  d    Instrument Handle                   Run ZT1428_RUN Stop ZT1428_STOP    	            handle    g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     ?    Result

Specifies the variable name in which to place the 
result of the calibration.  If zero is returned, 
the internal self-calibration was successful.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?| ????    `    Error                            ?? \ ?     `    Result                           ?? ? %  ?  d    Instrument Handle                  	            	            handle   
    Setup

Defines the setup state operation to be performed. 
Save stores the current instrument settings to
non-volatile memory. Recall loads a previously
saved instrument state from non-volatile memory.

Valid Range:
0 - ZT1428_SAVE - Save
1 - ZT1428_RCL  - Recall
     g     Control Name:  Error

 Description:   Displays status relating to the
                function call.     w    State Number

Defines the setup state number to be saved or 
recalled from non-volatile memory.

Valid Range:
1 to 48     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? a ?          Setup                            ?? ????    `    Error                            ?V d'     `    State Number                     ?? *  ?  d    Instrument Handle                #  Recall ZT1428_RCL Save ZT1428_SAVE    	            1    handle    ?    Instrument ID

Specifies the variable name in which to place the 
instrument id string (returned from *IDN?). This 
array must be at least 100 characters in length.

     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     Y    Driver Version

Specifies the variable name in which to place the 
CVI driver version.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?n C W   ?    Instrument ID                    ? ????    `    Error                            ?? ? ?    `    Driver Version                   ?? ? )  ?  d    Instrument Handle                  	            	            	           handle    \    Instrument Error

Specifies the variable name in which to place the
instrument error code.     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?u h ?     `    Instrument Error                 ?? ????    `    Error                            ?I ? %  ?  d    Instrument Handle                  	            	            handle    ?    State

Queries run state of the instrument. Run indicates 
on-going continuous acquisition.  Stop indicates
that acquisitions are stopped.

Valid Values:
0 - ZT1428_STOP - Stopped
1 - ZT1428_RUN  - Running
     h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? h ?     `    State                            ?f ????    `    Error                            ?? ? (  ?  d    Instrument Handle                  	            	            handle    h     Control Name:  Error

 Description:   Displays status relating to the
                function call.
     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ? ????    `    Error                            ?? ? '  ?  d    Instrument Handle                  	            handle    f     Control Name:  Error

 Description:   Displays status relating to the
               function call.     |    Instrument Handle

Accepts the Instrument Handle, returned by the 
Initialize function, to select the desired 
instrument.   ?? ????    `    Error                            ?? *  ?  d    Instrument Handle                  	            handle          ????  6?     K.    init                            ????         8  S     K.    init_with_options                 ?       ????  U?     K.    auto_setup                        ?       ????  X?     K.    auto_logic                        y       ????  b{     K.    vertical                          ?       ????  o 	    K.    acquisition                       J       ????  |a 	    K.    function                          ?       ????  ??     K.    ext_input                         S       ????  ?L     K.    outputs                           ?       ????  ??     K.    edge_trigger                      ?       ????  ?`     K.    soft_trigger                             ????  ??     K.    trigger_center                    $       ????  ??     K.    trigger_holdoff                   ?       ????  ??     K.    pattern_trigger                   ?       ????  ??     K.    state_trigger                     3       ????  ?% 	    K.    tv_trigger                        {       ????  ̧     K.    trigger_event                     ?       ????  ?k     K.    query_vertical                    ?       ????  ߒ 	    K.    query_acquisition                        ????  ?'     K.    query_function                    g       ????  ??     K.    query_ext_input                   %       ????  ?     K.    query_outputs                     ?       ????  ?     K.    query_trigger                            ???? v     K.    query_adv_trigger                 i       ????      K.    query_measurement                 ?       ???? (?     K.    measurement                       "?       ???? 2?     K.    measurement_level                 #       ???? 9? 
    K.    delay_parameters                  #j       ???? ??     K.    width_parameters                  #?       ???? P     K.    limit_test                        $?       ???? ^?     K.    mask_test                         %       ???? h?     K.    result_stats                      (?       ???? pU     K.    digitize_waveform                 )?       ???? s?     K.    dig_complete                      *?       ???? ?'     K.    read_waveform                     *?       ???? ??     K.    store_waveform                    +?       ???? ??     K.    load_array                        /?       ???? ??     K.    reset                             /?       ???? ?%     K.    device_clear                      0R       ???? ?$     K.    self_test                         0?       ???? ??     K.    run_stop                          0?       ???? ?     K.    calibrate                         1?       ???? ?Y     K.    save_recall                       2D       ???? ?s     K.    id_version                        2?       ???? ??     K.    error                             3:       ???? ?Z     K.    running                           3?       ???? ?     K.    wait_op_complete                  3?       ???? ??     K.    close                                                                      DInitialize                           DInitialize with Options             ?Configure                            DAuto Setup                           DAuto Logic Setup                     DSet Vertical                         DSet Acquisition                      DSet Math Function                    DSet External Input                   DSet Outputs                          DSet Edge Trigger                    Advanced Trigger                     DSoft Trigger                         DSet Trigger to Offset                DSet Trigger Holdoff                  DSet Pattern Trigger                  DSet State Trigger                    DSet TV Trigger                       DGet Trigger Event                   ?Configuration Readback               DQuery Vertical                       DQuery Acquisition                    DQuery Math Function                  DQuery External Input                 DQuery Outputs                        DQuery Trigger                        DQuery Advanced Trigger               DQuery Measurement                   ?Measurement                          DGet Measurement                      ?Advanced Measurement                 DSet Measurement Level                DSet Delay Parameters                 DSet Width Parameters                 DSet Limit Test                       DSet Mask Test                        DGet Result Statistics               &Waveform Operations                  DDigitize Waveform                    DGet Digitize Complete                DRead Waveform to Array               DStore Waveform to Memory             DLoad Array to Memory                +?Low Level Operations                 DReset                                DDevice Clear                         DSelf Test                            DRun/Stop                             DCalibrate                            DSave/Recall State                    DGet ID and Version                   DGet Error                            DGet Running                          DWait for Operation Complete          DClose                           