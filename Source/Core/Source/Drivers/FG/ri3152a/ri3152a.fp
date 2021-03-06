s??        ?   ? g?  &@   ?   ????                               ri3152a     Racal 3152A Arbitrary Waveform Generator         
?  	ViInt32 []     ? 	 
ViReal64 []     
?  	ViInt16 []     ? 	 
ViUInt16 []     ? ??ViInt32     	?  ViChar []     ? ??ViReal64     
? 
??ViReal64 *     ? ??ViInt16     	? 	??ViInt16 *     ? ??ViPInt16     ? ??ViStatus     	? 	??ViSession     ? ??ViRsrc     	? 	??ViBoolean     ? ??ViAttrState     	?  ViInt32[]  ?      Instrument Name:  Racal Instruments 3152A Arbitrary
                    Function Generator

  Description:      This instrument driver provides a range of
                    functions which can be used to operate the
                    3152A Arbitrary Function Generators

  Functions and Classes:

  (1)     Initialize:

          Initializes the timer/counter and associates an
          "instrument handle" with the instrument.  The 
          instrument handle is used with all of the other
          functions within this driver.  THIS FUNCTION MUST
          BE CALLED BEFORE ANY OF THE OTHER FUNCTIONS OF THIS
          DRIVER ARE USED

          All functions in this driver may be used with a 3152A.

  (2)     Application Functions

          Functions in this class combine several of the lower
          level functions of this driver to produce a complete
          measurement value.  These functions illustrate the
          use of the other functions within the driver.

  (3)     Configure Functions

          Functions in this class configure the function
          generator to output specific waveforms, including
          standard and arbitrary waveform types.

  (4)     Action/Status Functions

          Functions in this class provide the capability of
          turning the generator on or off.  This class also
          includes a variety of functions used to query the
          present setup of the arbitrary waveform generator.

  (5)     Data Operations

          This class includes the functions used to define and
          download arbitrary waveform data blocks

  (6)     Utility Functions

          Functions in this class include the functions which
          are common to all VXIplug&play drivers.  These
          functions reset, perform a self-test, read the
          instrument and driver revision, perform an error
          query, and convert a numeric error code to a 
          descriptive error message.

  (7)     Close

          This function closes the driver and releases the
          system resources (memory) which were allocated when
          the instrument was initialized.  This function should 
          be called at the end of the application program before
          it terminates.

    l     Panel Name:    Initialize

 Description:   This routine performs the
                following initialization actions:

                -  Opens the instrument by
                   storing information about the
                   instrument in the Instrument
                   Table.

                -  Initializes the VXI interface
                   (NI-VXI or NI-GPIB)
                   accordingly.

                -  Performs an identification
                   query on the Instrument.

                -  Sends initialization commands
                   to the instrument.

                -  Returns an Instrument ID which
                   is used to differentiate
                   between instruments of the same
                   model type.  This value will be
                   used to identify the instrument
                   in subsequent calls.    c    Class Name:    Application Functions

Description:   Functions in this class demonstrate the
               use of the other functions in the driver.
               It shows how to combine the basic functions
               to set-up the counter to make specific
               measurements and to return the results of
               the measurements

    l                   TAKE MEASUREMENT

 Takes the latest reading from the selected
 channel and returns it to the user.
 ERROR will show the code of any errors which
 occur during operation.

 NOTE: For the Peak to Peak voltage measurement,
 the +ve Peak measurement, the -ve Peak
 measurement, and the positive and negative
 duty-cycles may not be triggered manually.    )     Class Name:   Configure

 Description:  This class includes functions which configure
               the 3152A arbitrary waveform generator.  This
               includes such attributes as the type of
               waveform, the output state, the trigger
               conditions, and so on.     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    ?     Class Name:   Operating Mode

 Description:  This class includes functions which provide
               selection and configuration of the four
               trigger modes available with the waveform
               generator (Continuous, Triggered, Burst, Gated).
               It also provides selection of the Standard,
               Arbitrary, or Sequence waveform modes of
               operation.
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?     Class Name:   Waveform Parameters

 Description:  This class includes functions which provide
               programming for the present waveform's
               frequency, amplitude, and offset
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?     Class Name:   Standard Waveforms

 Description:  This class includes functions which program the
               attributes for the various standard waveforms
               that the 3152A can generate.    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?     Class Name:   Arbitrary Waveforms

 Description:  This class includes functions which establish
               the arbitrary waveform trace data sizes     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?     Class Name:   Sequence Programming

 Description:  This class includes functions which select the
               the sequence of arbitrary and/or standard
               waveforms to generate.
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?     Class Name:  Trigger & Sync

 Description:  This class includes functions which program the
               TRIGGER input, TRIGGER output, and SYNC output
               attributes
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    ?     Class Name:   3152 Phase Lock Loop

 Description:  This class includes functions which program a
               3152 to use phase lock loop, referenced to
               an external source.  One function enables or
               disables the phase lock loop (PLL) mode, while
               another allows the adjustment of phase of the
               signal.

               These functions work only with a 3152.  The 3152A
               does not have this capability.
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    >     Class Name:  Phase

 Description:  This class includes functions which program one
               waveform generator as a master and one or more
               waveform generators as slaves.  The slave mode
               provides phase synchronization between two or
               more 3152A waveform generators.
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    ?    Panel Name:    Reset

Description:   This panel resets the switch
               controller.  After this function
               has completed, the state of the
               switch controller is as follows:

                  - all relays are opened
                  - the Confidence mode is disabled
                  - the SYNC OUT delay is 0 msec
                  - there is no Equate list defined
                  - there is no Exclude list defined
                  - the Scan function is enabled
                  - there is no Scan list defined
                  - the SRQMASK is set to 0x40
                  - the SYNC OUT feature is disabled
                  - the TTTLTRG 0 line is enabled
    ?     Class Name:   3152 Phase Lock Loop

 Description:  This class includes functions which program a
               3152 to use phase lock loop, referenced to
               an external source.  One function enables or
               disables the phase lock loop (PLL) mode, while
               another allows the adjustment of phase of the
               signal.

               These functions work only with a 3152.  The 3152A
               does not have this capability.
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
    9                   CHANNEL CONTROLS

 Allows the following parameters to be configured.
 ERROR will show the code of any errors which
 occur during operation.


 Channel        : A or B
 Coupling       : AC or DC
 Input impedance: 50 ohm or 1Megohm
 Slope          : +ve or -ve
 Attenuator     : X1 or X10
 Trigger        : Auto or manual
 Trigger level  : -51.0 to 51.0 volts
 Low pass filter: 50 kHz enable or disable
 Source         : Front panel, sumbus, or timebase
 Chan A chan B  : Common or separate
 Resolution     : 3 to 10 digits
 Gate           : 1ms to 10 s
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    `     Class Name:   Action/Status Functions

 Description:  This class includes functions
               which perform some action or read
               the 3152A status.  The functions in this
               class enable the user to perform triggering,
               serial polling, clearing, and reading the
               IEEE-488.2 status registers.     ?                      Clear instrument

 This function performs a VXI clear or GPIB clear
 of the instrument.

 ERROR will show the code of any errors which
 occur during operation.



     ?                      Trigger instrument

 This function performs a VXI word serial or GPIB
 trigger of the instrument.

 ERROR will show the code of any errors which
 occur during operation.



     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?     Class Name:  IEEE 488.2 Status

 Description:  This class includes functions which program or
               query the IEEE-488.2 defined status and event
               registers.
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).         Class Name:   Data Operations

 Description:  This class includes functions
               which download data to the 3152A
               waveform memory.  The data may be
               downloaded from an array, from an ASCII
               data file, or from a WaveCAD file.
     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    ?     Class Name:   Utility Functions

 Description:  This class includes functions
               which perform operations that
               are common to all VXIplug&play
               drivers.  These are:

                    - Reset to power-up
                    - Perform Self-test
                    - Perform Error Query
                    - Convert Error to Message
                    - Perform Revision Query     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).     ?    Panel Name:    Trigger Instrument

Description:   Performs a VXI word serial trigger
               command (if MXI/VXI) or a GPIB
               trigger (GPIB/VXI).    ?    Panel Name:    Reset

Description:   This panel resets the switch
               controller.  After this function
               has completed, the state of the
               switch controller is as follows:

                  - all relays are opened
                  - the Confidence mode is disabled
                  - the SYNC OUT delay is 0 msec
                  - there is no Equate list defined
                  - there is no Exclude list defined
                  - the Scan function is enabled
                  - there is no Scan list defined
                  - the SRQMASK is set to 0x40
                  - the SYNC OUT feature is disabled
                  - the TTTLTRG 0 line is enabled
    ?    Panel Name:    Reset

Description:   This panel resets the switch
               controller.  After this function
               has completed, the state of the
               switch controller is as follows:

                  - all relays are opened
                  - the Confidence mode is disabled
                  - the SYNC OUT delay is 0 msec
                  - there is no Equate list defined
                  - there is no Exclude list defined
                  - the Scan function is enabled
                  - there is no Scan list defined
                  - the SRQMASK is set to 0x40
                  - the SYNC OUT feature is disabled
                  - the TTTLTRG 0 line is enabled
    1    Panel Name:    Self-Test

Description:   This panel performs the three
               self-tests provided by the
               1260 option 01 smart card.  These
               are:

                  - a RAM test
                  - an EPROM checksum test
                  - a Nonvolatile memory test
     ?    Panel Name:    Revision Query

Description:   This panel performs a query to
               determine the revision of the
               driver as well as the revision
               of the firmware.    6    Panel Name:    Error Query

Description:   This panel performs an error
               query from the instrument.  Upon
               return from the call, the "error"
               parameter will be set to a 0 if
               no error was detected, or set to
               a 1 if an error was detected.    6    Panel Name:    Error Query

Description:   This panel performs an error
               query from the instrument.  Upon
               return from the call, the "error"
               parameter will be set to a 0 if
               no error was detected, or set to
               a 1 if an error was detected.    6    Panel Name:    Error Query

Description:   This panel performs an error
               query from the instrument.  Upon
               return from the call, the "error"
               parameter will be set to a 0 if
               no error was detected, or set to
               a 1 if an error was detected.     ?    Panel Name:    Error Message

Description:   This panel translates an error
               code returned by one of the
               functions in this driver into
               a string error message     ?     Panel Name:     Close

 Description:    The close routine performs the
                 following operations:

                 -  removes the entry for the
                    instrument from the Instrument
                    Table.    y    
Panel Name:    Initialize

Description:   Initializes the instrument and returns and
               "instrument handle".  The instrument handle
               must be used with all of the other functions
               of this driver.

               The initialize call allows the instrument
               to be queried to ensure that it is a 3152A
               Arbitrary Waveform/Function Generator.
               It also resets the 3152A to the power-up 
               state if the "Reset" parameter is True (ON).

               Note that for each "ri3152a_init()" call, a new
               unique instrument handle is returned.  Thus, 
               if four calls are made to the initialize
               call in succession, four unique instrument
               handles will be returned.  This driver supports
               10 instances of instrument handles, so that
               if this function is called 11 times consecutively
               (with no call to ri3152a_close()), the 11th call
               will fail.

               For each instrument handle returned by the
               "ri3152a_init()" function, the "ri3152a_close()"
               function should be called to free up the
               resources allocated by "ri3152a_init()".  The
               call(s) to "ri3152a_close()" should be made
               before the application program terminates.
            Control Name:  Instr Descriptor

 Description:   Specifies which remote instrument
                to establish a communication
                session with.  Based on the syntax
                of the Instr Descriptor, the 
                Initialize function configures the
                I/O interface and generates an 
                Instr Handle 

 Default Value: "VXI::7::INSTR"

 Based on the Instrument Descriptor, this operation
 establishes a communication session with a device.
 The grammar for the Instrument Descriptor is shown
 below.  Optional parameters are shown in square 
 brackets ([]).  The default value is for a VXI
 interface for logical address 7.  For a GPIB-VXI interface
 with the timer/counter set to logical address 19, the 
 value should be:

      "GPIB-VXI::7::INSTR"


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
                function is called.  The ri3152a_close()
                call should be used for EACH handle
                returned by the ri3152a_init() function.
    ?     Control Name:  ID Query

 Description:   Specifies if an ID Query is sent
                to the instrument during the
                initialization procedure.

 Valid Range:   1 = Yes
                0 = No

 Default Value: 1 - Yes

 NOTE:  Under normal circumstances the ID Query
        insures that the instrument initialized
        over the bus is the type supported by
        this driver. However circumstances may
        arise where it is undesirable to send an
        ID Query to the instrument. In those
        cases; set this control to Skip Query
        and this function will initialize the
        bus and the Command arrays in the driver,
        without doing an ID Query.    ?     Control Name:  Reset

 Description:   Specifies if the instrument is to
                be reset to its power-on settings
                during the initialization
                procedure.

 Valid Range:   1 = Yes
                0 = No

 Default Value: 1 - Yes

 NOTE: If you do not want the instrument reset.
       Set this control to No while initializing
       the instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ? O G  ?       Instr Descriptor                  ?E P? ?       Instr Handle                      ?? ? ! ?       ID Query                          ?? ? ? ?       Reset Device                      ?: ?????       Error                              "VXI::7::INSTR"    	           Do Query 1 Skip Query 0   Reset Device 1 Don't Reset 0    	          ?    
Panel Name:    Example:  Generate Sequence Waveform

Description:   This example function shows how to user the
               functions of the driver to generate an arbitrary
               waveform.  The waveform is constructed by
               generating a sequence of segments.  This example
               shows the order in which the segments are
               defined, loaded with data, and sequenced to
               generate a (potentially) complex waveform.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?? ?   ?       Instr Handle                      ?? ?????       Error                                  	           R    
Panel Name:    Output ON/OFF

Description:   Turns the output signal ON or OFF
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ,     Control Name:  Output Switch

 Description:   Turns the output signal On or Off
 
 Valid Range:   RI3152A_OUTPUT_ON -  VI_TRUE   (1)
                RI3152A_OUTPUT_OFF - VI_FALSE  (0)

 Default:       RI3152A_OUTPUT_OFF  (note:  instrument *RST
                                   condition is OFF)     ? (  ?       Instr Handle                     ? ?????       Error                            s u ??      Output Switch                          	                     ON 1 OFF 0    F    
Panel Name:    Output Query

Description:   Reads the output state
     ?     Control Name:      State

 Description:       Returns the output state.
                    0 = output is off
                    1 = output is on    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ? e ? ?  P    State                            	[ ? (  ?       Instr Handle                     ? ?????       Error                              	                	           ?    
Panel Name:    Output Shunt ON/OFF

Description:   Turns the output shunt ON or OFF to bypass or not 
               to bypass the 50 Ohm resistor.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    *     Control Name:  Output Shunt

 Description:   Turns the output shunt On or Off
 
 Valid Range:   RI3152A_OUTPUT_ON -  VI_TRUE   (1)
                RI3152A_OUTPUT_OFF - VI_FALSE  (0)

 Default:       RI3152A_OUTPUT_OFF  (note:  instrument *RST
                                   condition is OFF)   % ? (  ?       Instr Handle                     a ?????       Error                            ? u ??      Output Shunt                           	                     ON 1 OFF 0    R    
Panel Name:    Output Shunt Query

Description:   Reads the output shunt state
     ?     Control Name:      State

 Description:       Returns the output shunt state
                    0 = shunt is off
                    1 = shunt is on    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ? e ? ?  P    State                            ? ? (  ?       Instr Handle                     ? ?????       Error                              	                	           N    
Panel Name:    Output ECLT

Description:   Turns the output ECLT ON or OFF
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Output ECLT Switch

 Description:   Turns the output ECLT On or Off
 
 Valid Range:   RI3152A_OUTPUT_ON -  VI_TRUE   (1)
                RI3152A_OUTPUT_OFF - VI_FALSE  (0)
   " ? (  ?       Instr Handle                     $G ?????       Error                            ({ u ??      Output ECLT Switch                     	                     ON 1 OFF 0    S    
Panel Name:    Output ECLT Query

Description:   Reads the state of output ECLT
     N     Control Name:      State

 Description:       Returns the output ECLT state    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   *e e ? ?  P    State                            *? ? (  ?       Instr Handle                     ,? ?????       Error                              	                	           ?    
Panel Name:    Waveform Mode Selection Switch

Description:   Selects one of the Waveform Modes: STD, ARB,
               SEQ, and SWEEP    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    #     Control Name:  Waveform Mode

 Description:   Selects an Waveform Mode
 
 Valid Range:   RI3152A_MODE_STD

                   This mode provides for generating standard
                   waveforms, such as Sine Wave, Square Wave,
                   Triangle Waves, and so on.

                RI3152A_MODE_ARB

                   This mode outputs a single arbitrary
                   waveform segment

                RI3152A_MODE_SEQ

                    This mode outputs a sequence of one or
                    more arbitrary segments.  Each segment
                    is generated one or more times before
                    moving to a subsequent segment.

                RI3152A_MODE_SWEEP

                    This mode outputs a sequence in sweep mode

 Default:       RI3152A_MODE_STD   2y ?    ?       Instr Handle                     4? ?????       Error                            8? ] ??      Waveform Mode                          	                      XSTD RI3152A_MODE_STD ARB RI3152A_MODE_ARB SEQ RI3152A_MODE_SEQ SWEEP RI3152A_MODE_SWEEP    ?    
Panel Name:    Standard Waveform Selection

Description:   Selects one of the standard waveforms:  SINE, 
               TRI, SQU, PULS, RAMP, GAUS, SINC, EXP, DC    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Standard Waveform

 Description:   Selects a standard waveform
 
 Valid Range:   RI3152A_SINE          (1)
                RI3152A_TRIANGLE      (2)
                RI3152A_SQUARE        (3)
                RI3152A_PULSE         (4)
                RI3152A_RAMP          (5)
                RI3152A_SINC          (6)
                RI3152A_GAUSSIAN      (7)
                RI3152A_EXPONENTIAL   (8)
                RI3152A_DC            (9)

                   

 Default:       RI3152A_SINE   =? ?    ?       Instr Handle                     @ ?????       Error                            DG 0 ??      Standard Waveform                      	                   	   ?Sine RI3152A_SINE Triangle RI3152A_TRIANGLE Square RI3152A_SQUARE Pulse RI3152A_PULSE Ramp RI3152A_RAMP Sinc RI3152A_SINC Gaussian RI3152A_GAUSSIAN Exponential RI3152A_EXPONENTIAL Dc RI3152A_DC       
Panel Name:    Mode Query

Description:   Returns the current mode in which the 
               waveform generator is operating.  If it
               is generating in the "Standard" mode, this
               function also returns which waveform is
               presently selected.
    ?     Control Name:      Present Waveform Mode

 Description:       Reads the presently selected Waveform
                    Operating Mode.  The reply will be one
                    of the following:

                    RI3152A_MODE_STD (0)   - Standard Waveform
                    RI3152A_MODE_ARB (1)   - Arbitrary Waveform
                    RI3152A_MODE_SWEEP (2) - Sweep Waveform
                    RI3152A_MODE_SEQ (3)   - Sequence Waveform

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    \     Control Name:      Present Standard Waveform

 Description:       Indicates the presently selected standard
                    waveform.  This will be one of the 
                    following:

                        RI3152A_NON_STANDARD (0) - Not in
                                                  Standard Mode
 
                        RI3152A_SINE (1)     - Sine Wave
                        RI3152A_TRIANGLE (2) - Triangle Wave
                        RI3152A_SQUARE (3)   - Square Wave
                        RI3152A_PULSE (4)    - Pulse Wave
                        RI3152A_RAMP (5)     - Ramp Wave
                        RI3152A_SINC (6)     - Sine(x)/x Wave
                        RI3152A_GAUSSIAN (7) - Gaussian Wave
                        RI3152A_EXPONENTIAL (8) - Exponential 
                        RI315A_DC           - DC Waveform

   H? H y ?  P    Present Waveform Mode            J?  &  ?       Instr Handle                     L? ?????       Error                            Q1 HJ ?  P    Present Standard Waveform          	                	           	               
Panel Name:    Operating Mode

Description:   Selects one of the four operating modes: 
               Continuous, Trigger, Gated, Burst.

               For a description of these operating modes,
               see the "Operating Mode" parameter description.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ,     Control Name:  Operating Mode

 Description:   Selects one of the modes: CONT, TRIG, GATED
                and BURST.
 
 Valid Range:   RI3152A_MODE_CONT

                   The signal is generated continuously

                RI3152A_MODE_TRIG

                   The signal is generated once for each
                   trigger received

                RI3152A_MODE_GATED

                   The signal is generated as long as the
                   trigger is asserted.  Once started, a
                   complete cycle of the signal will be
                   produced.

                RI3152A_MODE_BURST

                   N cycles are produced each time a trigger
                   is received.  N is set by the function
                   ri3152A_burst_mode().

 Default:       RI3152A_MODE_CONT
   V? ?    ?       Instr Handle                     X? ?????       Error                            ] ` ??      Operating Mode                         	                      `CONT RI3152A_MODE_CONT TRIG RI3152A_MODE_TRIG GATED RI3152A_MODE_GATED BURST RI3152A_MODE_BURST    [    
Panel Name:    Operating Mode Query

Description:   Returns the current operating mode 
    h     Control Name:      Operating Mode

 Description:       Reads the presently selected 
                    Operating Mode.  The reply will be one
                    of the following:

                    RI3152A_MODE_CONT (0) 
                    RI3152A_MODE_TRIG (1) 
                    RI3152A_MODE_GATE (2) 
                    RI3152A_MODE_BURST(3)  

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   a? Y ? ?  P    Operating Mode                   c6  &  ?       Instr Handle                     er ?????       Error                              	                	          ?    
Panel Name:    Set Frequency

Description:   Sets the frequency for the presently selected
               waveform frequency.  Although the range is
               up to 50.0 MHz, the maximum may be as low as
               1.0 MHz depending on the present waveform.
               Care should be taken to program below the
               maximum for the presently selected waveform.

               The maximum frequencies for standard waveforms
               are as follows:

                     Waveform          Max Frequency
                     --------------------------------------
                      Sine               50 MHz
                      Square             50 MHz
                      Triangle            1 MHz
                      Pulse               1 MHz
                      Ramp                1 MHz
                      Sinc                1 MHz
                      Exponential         1 MHz
                      Gaussian            1 MHz

           4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
          Control Name:  Frequency

 Description:   Programs the frequency for the presently
                selected waveform
 
 Valid Range:   100.0E-6 to 50.0E6  (Is 1.0E6 for all but
                                     Sine and Square waves)

                Note:  The upper limit of 1.0E6 is NOT checked
                       It is the user's responsibility to 
                       ensure that the frequency does not
                       exceeed the limit for the presently
                       selected waveform 

 Default:       1.0E6   nD ?    ?       Instr Handle                     p? ?????       Error                            t? ] ? ??      Frequency                              	           1.0E6    ?    
Panel Name:    Frequency Query

Description:   Returns the frequency setting for the currently
               active standard function
     Z     Control Name:      Frequency

 Description:       Reads the current frequency in hertz
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   x& @ ? ?       Frequency                        x?  &  ?       Instr Handle                     z? ?????       Error                              	               	          ?    
Panel Name:    Set Amplitude

Description:   Sets the amplitude for the presently selected
               waveform.  The amplitude may never be greater
               than 16.0.  Note that the value of

                    (Amplitude / 2) + Offset

               must always be between -8.0 and +8.0 volts.

               The user is responsible for ensuring that
               the combination of the amplitude and the
               offset does not exceed these values.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The amplitude selects the voltage range for
                the output (80 mV, 800 mV, or 8 V).  The
                amplitude and offset must always be within the
                following ranges:

                if Amplitude <= 0.080

                     (Amplitude / 2) + ABS(offset) <= 0.080

                if 0.080 < Amplitude <= 0.800

                     (Amplitude / 2) + ABS(offset) <= 0.800

                if 0.800 < Amplitude <= 8.0

                     (Amplitude / 2) + ABS(offset) <= 8.0   ?? ?    ?       Instr Handle                     ?? ?????       Error                            ? ] ? ??      Amplitude                              	           5.0    ?    
Panel Name:    Amplitude Query

Description:   Returns the amplitude setting for the currently
               active standard function
     Q     Control Name:      Amplitude

 Description:       Reads the current amplitude
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Amplitude                        ?O  &  ?       Instr Handle                     ?? ?????       Error                              	               	          ?    
Panel Name:    Set Offset

Description:   Sets the offset for the presently selected
               waveform.  The offset must always be between
               -7.19 to +7.19 volts.  Note that the value of

                    (Amplitude / 2) + Offset

               must always be between -8.0 and +8.0 volts.

               The user is responsible for ensuring that
               the combination of the amplitude and the
               offset does not exceed these values.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Offset

 Description:   Programs the offset for the presently
                selected waveform
 
 Valid Range:   -7.19 to +7.19

 Default:       0.0   ?b ?    ?       Instr Handle                     ?? ?????       Error                            ?? ] ? ??      Offset                                 	           0.0    ?    
Panel Name:    Offset Query

Description:   Returns the Offset setting for the currently
               active standard function
     K     Control Name:      Offset

 Description:       Reads the current Offset
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Offset                           ?  &  ?       Instr Handle                     ?R ?????       Error                              	               	           ?    
Panel Name:    Sine Wave

Description:   select Frequency, Amplitude, Offset, Phase
               and the Power of sine(x).     4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Selects the frequency of the Sine Wave
 
 Valid Range:   100 uHz (100.0E-6) to 50 MHz (50.0E6)

 Default:  100.00 KHz (100.0E3)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     }     Control Name:  Offset

 Description:   Selects the offset level 
 
 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     ?     Control Name:  Phase

 Description:   Selects the starting phase of the Sine Wave
 
 Valid Range:   0 to 360 degrees

 Default:       0 degrees     ?     Control Name:  Power (Sine^x)

 Description:   Selects the exponentional power of the Sine 
                function (sine^1, sine^2, sine^3, ..., sine^9)

 Valid Range:   1 to 9  (RI3152A_POWER_1 to RI3152A_POWER_9)   ?? ? "  ?       Instr Handle                     ? ?o???       Error                            ?9 < 2 ?       Frequency                        ?? ; ? ?       Amplitude                        ?, ;" ?       Offset                           ?? ;? ?       Phase                            ?K ~ ? ??      Power (Sine^x)                         	           	100000.0    5.0    0.0    0            	   ?1 RI3152A_POWER_1 2 RI3152A_POWER_2 3 RI3152A_POWER_3 4 RI3152A_POWER_4 5 RI3152A_POWER_5 6 RI3152A_POWER_6 7 RI3152A_POWER_7 8 RI3152A_POWER_8 9 RI3152A_POWER_9    E    
Panel Name:    Set Sine Wave Phase

Description:   Sets Sine Phase    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Phase

 Description:   Selects the starting phase of the Sine Wave
 
 Valid Range:   0 to 360 degrees

 Default:       0 degrees   ?? ? "  ?       Instr Handle                     ?
 ?o???       Error                            ?> R ? ?       Phase                                  	           0    W    
Panel Name:    Query Sine Phase

Description:   Returns the current sine phase value     X     Control Name:      Sine Phase

 Description:       Reads the current sine phase value    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Sine Phase                       ?K  &  ?       Instr Handle                     ?? ?????       Error                              	                	           E    
Panel Name:    Set Sine Wave Power

Description:   Sets Sine Power    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     k     Control Name:  Power

 Description:   Selects the sine power
 
 Valid Range:   1 to 9

 Default:       1   ?? ? "  ?       Instr Handle                     ?? ?o???       Error                            ?3 R ? ?       Power                                  	           0    W    
Panel Name:    Query Sine Power

Description:   Returns the current sine power value     X     Control Name:      Sine Power

 Description:       Reads the current sine power value    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ʹ @ ? ?       Sine Power                       ?  &  ?       Instr Handle                     ?U ?????       Error                              	                	           ?    
Panel Name:    Triangular Wave

Description:   select Frequency, Amplitude, Offset, Phase
               and the Power of triangle(x).     4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Selects the frequency of the Triangular Wave
 
 Valid Range:   100 uHz (100.0E-6) to 1 MHz (1.0E6)

 Default:  100.00 KHz (10.0E5)     7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     |     Control Name:  Offset

 Description:   Selects the offset level 

 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     ?     Control Name:  Phase

 Description:   Selects the starting phase of the Triangle Wave
 
 Valid Range:   0 to 360 degrees

 Default:       0 degrees     ?     Control Name:  Power

 Description:   Selects the exponentional power of the triangle
                function (triangle^1, triangle^2, ...triangle^9)
 
 Valid Range:   1 to 9  (RI3152A_POWER_1 to RI3152A_POWER_9)   ?? ? "  ?       Instr Handle                     ? ?o???       Error                            ?F < 4 ?       Frequency                        ?? < ? ?       Amplitude                        ?= <" ?       Offset                           ?? <? ?       Phase                            ?_ } ? ??      Power (Triangular^x)                   	           	100000.0    5.0    0.0    0            	   ?1 RI3152A_POWER_1 2 RI3152A_POWER_2 3 RI3152A_POWER_3 4 RI3152A_POWER_4 5 RI3152A_POWER_5 6 RI3152A_POWER_6 7 RI3152A_POWER_7 8 RI3152A_POWER_8 9 RI3152A_POWER_9    H    
Panel Name:    Set Triangle Phase

Description:   Sets Triangle Phase    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Phase

 Description:   Selects the starting phase of the Triangle Wave
 
 Valid Range:   0 to 360 degrees

 Default:       0 degrees   ?? ? "  ?       Instr Handle                     ? ?o???       Error                            ?R R ? ?       Phase                                  	           0    _    
Panel Name:    Query Triangle Phase

Description:   Returns the current triangle phase value     X     Control Name:      Sine Phase

 Description:       Reads the current sine phase value    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ? @ ? ?       Triangle Phase                   ?k  &  ?       Instr Handle                     ?? ?????       Error                              	                	           H    
Panel Name:    Set Triangle Power

Description:   Sets Triangle Power    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     q     Control Name:  Power

 Description:   Selects the starting power of the Triangle Wave
 
 Valid Range:   1 to 9   ?? ? "  ?       Instr Handle                     ?" ?o???       Error                            ?V R ? ?       Power                                  	           1    _    
Panel Name:    Query Triangle Power

Description:   Returns the current triangle power value     X     Control Name:      Sine Phase

 Description:       Reads the current sine phase value    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Triangle Power                   ?J  &  ?       Instr Handle                     ?? ?????       Error                              	                	           ?    
Panel Name:    Square Wave

Description:   Select Frequency, Amplitude, Offset and
               Duty Cycle.
                   4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Selects the frequency of the Square Wave
 
 Valid Range:   100 uHz (100.0E-6) to 50 MHz (50.0E6)

 Default:  100.00 KHz (10.0E5)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     }     Control Name:  Offset

 Description:   Selects the offset level 
 
 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     ?     Control Name:  Duty Cycle

 Description:   Selects the percentage of Duty Cycle
 
 Valid Range:   1% to 99%

 Default:       50%
                  ?? ? "  ?       Instr Handle                     9 ?????       Error                            m @ " ?       Frequency                        " @ ? ?       Amplitude                        a @( ?       Offset                           ? @? ?       Duty Cycle                             	           	100000.0    5.0    0.0    50    J    
Panel Name:    Square Duty Cycle

Description:   Sets square duty cycle    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     Z     Control Name:  Duty Cycle

 Description:   Set the duty cycle
 
 Valid Range:   1 to 99   A ? "  ?       Instr Handle                     } ?o???       Error                            ? R ? ?       Duty Cycle                             	           0    _    
Panel Name:    Query Square Duty Cycle

Description:   Returns the current square duty cycle     Y     Control Name:      Duty Cycle

 Description:       Reads the current square duty cycle    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   . @ ? ?       Duty Cycle                       ?  &  ?       Instr Handle                     ? ?????       Error                              	                	           ?    
Panel Name:    Pulse Wave

Description:   select Frequency, Amplitude, Offset, Delay Time
               Rise Time, High Time and Fall Time.
               
                   4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Select the frequency of the Pulse Wave
 
 Valid Range:   100 uHz (100.0E-6) to 1.0 MHz (1.0E6)

 Default:  100.00 KHz (100.0E3)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     }     Control Name:  Offset

 Description:   Select the offset level 
 
 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     3     Control Name:  Delay Time

 Description:   Select the Time Delay value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + high_time
                       + fall_time  must be less than 100%.               


 Default:       0% of the period                 2     Control Name:  Rise Time

 Description:   Select the Rise Time value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + high_time
                       + fall_time  must be less than 100%.               


 Default:       10% of the period                 2     Control Name:  High Time

 Description:   Select the High Time value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + high_time
                       + fall_time  must be less than 100%.               


 Default:       10% of the period                 4     Control Name:  Fall Time

 Description:   Select the Fall Time value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + high_time
                       + fall_time  must be less than 100%.               

  
 Default:       10% of the period                q ?    ?       Instr Handle                     ? ?o???       Error                            !? ?  ?       Frequency                        "? ? ? ?       Amplitude                        #? ? ?       Offset                           $Y ?? ?       Delay Time                       %? ? d ?       Rise Time                        &? ? ? ?       High Time                        ( ?X ?       Fall Time                              	           	100000.0    5.0    0.0    0.0    10.0    10.0    10.0    ?    
Panel Name:    Set Pulse

Description:   Sets the pulse data    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ]     Control Name:  Delay

 Description:   Programs the pulse delay
 
 Valid Range:   0 to 99.9     [     Control Name:  Rise

 Description:   Programs the pulse rise
 
 Valid Range:   0 to 99.9     ]     Control Name:  Width

 Description:   Programs the pulse width
 
 Valid Range:   0 to 99.9     [     Control Name:  Fall

 Description:   Programs the pulse fall
 
 Valid Range:   0 to 99.9   +? ?    ?       Instr Handle                     -? ?????       Error                            2! C ? ??      Delay                            2? ? ? ??      Rise                             2? C5 ??      Width                            3N ?5 ??      Fall                                   	           1.0E6    1.0E6    1.0E6    1.0E6    I    
Panel Name:    Query Pulse Data

Description:   Returns the pulse data     O     Control Name:      Delay

 Description:       Reads the current pulse delay
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     O     Control Name:      Width

 Description:       Reads the current pulse width
     M     Control Name:      Rise

 Description:       Reads the current pulse rise
     M     Control Name:      Fall

 Description:       Reads the current pulse fall
   5t A ? ?       Delay                            5?  &  ?       Instr Handle                     8 ?????       Error                            <; A! ?       Width                            <? ? ? ?       Rise                             <? ?! ?       Fall                               	               	           	           	           	           ?    
Panel Name:    Ramp Wave

Description:   select Frequency, Amplitude, Offset, Delay Time
               Rise Time and Fall Time.
               
                   4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Select the frequency of the Ramp Wave
 
 Valid Range:   100 uHz (100.0E-6) to 1.0 MHz (1.0E6)

 Default:  100.00 KHz (100.0E3)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     }     Control Name:  Offset

 Description:   Select the offset level 
 
 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     $     Control Name:  Rise Time

 Description:   Select the Rise Time value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + fall_time
                      must be less than 100%.               


 Default:       10% of the period                 $     Control Name:  Fall Time

 Description:   Select the Fall Time value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + fall_time
                      must be less than 100%.               


 Default:       10% of the period                 &     Control Name:  Delay Time

 Description:   Select the Time Delay value 
 
 Valid Range:   0% to 99.9% of the period

                Note: the total of delay + rise_time + fall_time
                      must be less than 100%.               


 Default:       10% of the period                ?e ? "  ?       Instr Handle                     A? ?????       Error                            E? < " ?       Frequency                        F? < ? ?       Amplitude                        G? <  ?       Offset                           HL ? ? ?       Rise Time                        Ix ? ?       Fall Time                        J? <? ?       Delay Time                             	           	100000.0    5.0    0.0    10.0    10.0    10.0    C    
Panel Name:    Set Rampe Data

Description:   Sets the ramp data    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     \     Control Name:  Delay

 Description:   Programs the ramp delay
 
 Valid Range:   0 to 99.9     Z     Control Name:  Rise

 Description:   Programs the ramp rise
 
 Valid Range:   0 to 99.9     Z     Control Name:  Fall

 Description:   Programs the ramp fall
 
 Valid Range:   0 to 99.9   N ?    ?       Instr Handle                     PC ?????       Error                            Tw D ? ??      Delay                            T? ? ? ??      Rise                             U= ?5 ??      Fall                                   	           1.0E6    1.0E6    1.0E6    G    
Panel Name:    Query Ramp Data

Description:   Returns the ramp data     N     Control Name:      Delay

 Description:       Reads the current ramp delay
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     L     Control Name:      Rise

 Description:       Reads the current ramp rise
     L     Control Name:      Fall

 Description:       Reads the current ramp fall
   W" C ? ?       Delay                            Wx  &  ?       Instr Handle                     Y? ?????       Error                            ]? ? ? ?       Rise                             ^< ?! ?       Fall                               	               	           	           	           y    
Panel Name:    Sinc Wave

Description:   select Frequency, Amplitude, Offset and the 
               Number of Cycles.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Select the frequency of the Sinc Wave
 
 Valid Range:   100 uHz (100.0E-6) to 1.0 MHz (1.0E6)

 Default:  100.00 KHz (10.0E5)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     }     Control Name:  Offset

 Description:   Select the offset level 
 
 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V      ?     Control Name:  Number of Cycles

 Description:   Select the number of cycles for the Sinc Wave
 
 Valid Range:   4 to 100

 Default:       10   `N ? "  ?       Instr Handle                     b? ?????       Error                            f? <  ?       Frequency                        gp < ? ?       Amplitude                        h? <% ?       Offset                           i4 <? ?       Number of  Cycles                      	           	100000.0    5.0    0.0    10    @    
Panel Name:    Set Sinc Cycle

Description:   Sets Sinc Cycle    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     s     Control Name:  NCycle

 Description:   Set number of cycles for the sinc wave
 
 Valid Range:   4 to 100 cycles
   k? ? "  ?       Instr Handle                     m? ?o???       Error                            q? R ? ?       NCycle                                 	           10    c    
Panel Name:    Query Sinc NCycle

Description:   Returns the current sinc number of cycles value     _     Control Name:      Sinc NCycle

 Description:       Holds the current sinc number of cycles     4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   s? @ ? ?       Sinc NCycle                      s?  &  ?       Instr Handle                     v0 ?????       Error                              	                	           x    
Panel Name:    Exponential Wave

Description:   select Frequency, Amplitude, Offset and the 
               Exponent.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Select the frequency of the Exponential Wave
 
 Valid Range:   100 uHz (100.0E-6) to 1.0 MHz (1.0E6)

 Default:  100.00 KHz (10.0E5)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     |     Control Name:  Offset

 Description:   Select the offset level 
 
 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     ?     Control Name:  Exponent

 Description:   Select the exponent for the Exponential Wave

 Valid Range:   -200.0 to 200.0  (0.0 is not allowed)

 Default:       -1.0

   {? ? "  ?       Instr Handle                     }? ?????       Error                            ? = & ?       Frequency                        ?? < ? ?       Amplitude                        ? <& ?       Offset                           ?? <? ?       Exponent                               	           	100000.0    5.0    0.0    -1.00    Q    
Panel Name:    Set Exponential Exponent

Description:   Sets gauss exponential    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Exponent

 Description:   Selects the exponential exponent value
 
 Valid Range:   -200 to 200 (0 is not allowed)   ? ? "  ?       Instr Handle                     ?@ ?o???       Error                            ?t R ? ?       Exponent                               	           0    ?    
Panel Name:    Query Exponential Exponent

Description:   Returns the current exponential wave exponent 
               value     _     Control Name:      Exponent

 Description:       Reads the current exponential wave exponent    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?; @ ? ?       Exponent                         ??  &  ?       Instr Handle                     ?? ?????       Error                              	                	           x    
Panel Name:    Gaussian Wave

Description:   select Frequency, Amplitude, Offset and
               the exponent.
       4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Frequency

 Description:   Select the frequency of the Gaussian Wave
 
 Valid Range:   100 uHz (100.0E-6) to 1.0 MHz (1.0E6)

 Default:  100.00 KHz (10.0E5)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     {     Control Name:  Offset

 Description:   Select the offset level 

 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V     ?     Control Name:  Exponent

 Description:   Select the exponent for the Gaussian Wave

 Valid Range:   1 to 200

 Default:       10

   ?M ? "  ?       Instr Handle                     ?? ?????       Error                            ?? ? ! ?       Frequency                        ?s ? ? ?       Amplitude                        ?? ?' ?       Offset                           ?5 ?? ?       Exponent                               	           	100000.0    5.0    0.0    10    H    
Panel Name:    Set Gauss Exponent

Description:   Sets gauss exponent    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     g     Control Name:  Exponent

 Description:   Selects the gauss exponent value
 
 Valid Range:   1 to 200   ?? ? "  ?       Instr Handle                     ?? ?o???       Error                            ?? R ? ?       Exponent                               	           1    _    
Panel Name:    Query Gauss Exponent

Description:   Returns the current gauss exponent value     \     Control Name:      Exponent

 Description:       Reads the current gaussian wave exponent    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?z @ ? ?       Exponent                         ??  &  ?       Instr Handle                     ? ?????       Error                              	                	          ?    
Panel Name:    DC Signal

Description:   Sets the mode of operation to produce a DC
               signal at the most recently programmed
               amplitude.  This function provides the
               ability to program between -100% and +100%
               of the programmed amplitude.

               Note:  The amplitude may be changed using
               the ri3152a_set_amplitude() function.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Percent_Amplitude  

 Description:   Select a percentage of the amplitude

 Valid Range:   -100% to 100%

 Default:       100%   ?? ? "  ?       Instr Handle                     ?? ?????       Error                            ? Y ? ?       Percent Amplitude                      	           100    [    
Panel Name:    Query DC Amplitude

Description:   Returns the current DC Amplitude value     V     Control Name:      DC Amplitude

 Description:       Reads the current DC Amplitude    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       DC Amplitude                     ?'  &  ?       Instr Handle                     ?c ?????       Error                              	                	          <    Panel Name: Load Segment Binary

Description: 

This loads the segment binary data using the fast binary download
method.  This function performs the bit / byte manipulations necessary to generate the segment table in the proper format.  With this function, the programmer merely states the number of segments to download and the size (in points) of each wave.


The traditional ASCII method of downloading one waveform at a time is inefficient in cases where multiple waveforms have to be loaded in the shortest possible time.

There are provisions built into the Model 3152A that allow fast binary downloads of waveform data.  Using this method, the memory is first loaded with one long waveform that is made of many smaller waveforms, then, using fast binary access, the segment table is downloaded to the generator.  The segment table contains start address and size information for each segment.

There are a few things to consider when you prepare segment tables:

1. Data for each segment must have 5 bytes.

2. The number of bytes in a complete segment table must divide
   by 5.  The Model 3152A has no control over data sent to its
   port during data transfer.  Therefore, wrong data and/or
   incorrect number of bytes will cause erroneous memory 
   partition.

3. The start address for segment 1 is 0x1F4.

4. Compute the start address for segment n" using the following
   equation:

   ADD(n) = ADD(n-1) + (SIZE(n-1)/2)

   For example, say you have 2 segments, the first is 10,000 
   points.  The start address for segment number 2 is as 
   follows:

   ADD(2) = ADD(1) + (SIZE(1)/2) 
   = 500 + 10000/2 = 500 + 5000; and in hex
   = 0x1F4 + 0x1388 = 0x157C

5. The segment size is entered using the actual size of the 
   segment.  Do not divide the segment size as was done for the
   address.  For a size of 10,000: use 0x2710

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    &     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152A_error_message()" will return
                a string describing the error. 

 VISA Errors:
        These error codes are defined by the VISA I/O
        library function standards.  These errors are defined
        in the header file "visa.h".  These error codes are
        typically in the range 0xBFFF0000L to 0xBFFFFFFFL
        (-1073807360 to -1073741825).

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152A_error_message() function.  The
        ri3152A_error_message() function may be used to
        convert the error code to a meaningful error
        message.
     i    Control Name: Number of Segments

Description: This sets the number of segments for the segment table.
        Control Name:  Wave Size

Description: This stores the array of wave sizes for the Segment 
             Table.

Valid Range: Each entry must be 1 to the maximum value = memory
             size.

Size of array must be the same as the Number of Segments control
   ʖ  	  ?       Instr Handle                     ??????       Error                            ?  3 k ?      Number of Segments               ?q 3Z ?       Wave Size                              	           ????                    c    
 Control Name:  Define Arbitrary Segment

 Description:   Select the segment number and the segment
                size ( number of points) for a waveform
                segment.  
                
                NOTE: the segment size must match the number
                      of the data points in Input Data Points
                      panel.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     z     Control Name:  Segment Number

 Description:   Select the segment number.

 Valid Range:   1 to 4096

 Default:       1    ,     Control Name:  Segment Size ( Number of Points )

 Description:   Select the segment size.  It should match
                the number of data points in the Data Points
                panel.
 
 Valid Range:   10 to 64536 (64K memory)
                10 to 523288 (512K memory)

 Default:       10   ?? ? (  ?       Instr Handle                     ?' ?????       Error                            ?[ ` ? ?       Segment Number                   ?? `Q ?       Segment Size                           	           1    10        
 Panel Name:  Delete Segments

 Description:   Select a particular segment or all segments to
                be deleted.
 
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Segment Number

 Description:   Select the segment number to be deleted.
 
 Valid Range:   1 to 4096

 Default:       1
     ?     Control Name:  Delete All Segments?

 Description:   Selecting YES will delete all the segments.
 
 Valid Range:   0 or 1 (NO or YES)

 Default:       0
   އ ? (  ?       Instr Handle                     ?? ?????       Error                            ?? J n ?       Segment Number                   ?? E? ??      Delete All Segments?                   	           1               4NO RI3152A_DELETE_ALL_NO YES RI3152A_DELETE_ALL_YES    ?    
 Panel Name:  Output Arbitrary Waveform

 Description:   Select a Segment Number and its Sampling Clock,
                Amplitude, Offset and Clock Source.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     {     Control Name:  Segment Number

 Description:   Select the segment number.
 
 Valid Range:   1 to 4096

 Default:       1     ?     Control Name:  Sampling Clock

 Description:   Selects the Sampling Clock for selected segment.
 
 Variable Type: ViReal64

 Valid Range:   100 mHz (100.0E-3) to 100 MHz (100.0E6)

 Default:       100 MHz (100.0E6)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     |     Control Name:  Offset

 Description:   Selects the offset level 

 Valid Range:   -7.19 to +7.19 V

 Default:       0.0 V    Z     Control Name:  Clock Source  

 Description:   Select one of the Trigger Sources: INTERNAL
                EXTERNAL or ECLTRG0.  ECLTRG0 is used to
                synchronize two 3152As
 
 Valid Range:   INTERNAL (RI3152A_CLK_SOURCE_INT = 0)
                EXTERNAL (RI3152A_CLK_SOURCE_EXT = 1)
                

 Default:       INTERNAL (0)   ? ? (  ?       Instr Handle                     ?= ?????       Error                            ?q W  ?       Segment Number                   ?? W ? ?  x    Sampling Clock                   ?? X9 ?       Amplitude                        ? Y? ?       Offset                           ?? ?	 ??      Clock Source                           	           1    100000000.0    5.0    0.0               6INT RI3152A_CLK_SOURCE_INT EXT RI3152A_CLK_SOURCE_EXT    c    
Panel Name:    Sample Clock Source Query

Description:   Reads the value of sample clock source
     ?     Control Name:      Sample Clock Source

 Description:       Returns the value of sample clock source

 Valid Range:       0 = Internal
                    1 = External    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?O e ? ?  P    Sample Clock Source              ? ? (  ?       Instr Handle                     ?= ?????       Error                              	                	           ?    
 Panell Name:   Sampling Frequency

 Description:   Select the sampling frequency for the
                arbitrary waveform.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sampling Clock

 Description:   Selects the Sampling Clock for selected segment.
 
 Variable Type: ViReal64

 Valid Range:   100 mHz (100.0E-3) to 100 MHz (100.0E6)

 Default:       100 MHz (300.0E6)   ?? ? (  ?       Instr Handle                     ?? ?????       Error                            $ S ? ?  x    Sampling Clock                         	           100e6    Y    
Panel Name:    Sampling Frequency Query

Description:   Returns the sampling frequency     c     Control Name:      Sampling Frequency

 Description:       Reads the current sampling frequency
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    @ ? ?       Sampling Frequency               ?  &  ?       Instr Handle                     ? ?????       Error                              	               	          ^     Panel Name:    Define Sequence

 Description:   Defines a Sequence and its number of
                steps, segment numbers and number of times
                each segment is repeated.

                NOTE: the number of steps must match the number
                of the elements in the segment numbers [] and
                repeat segment [].    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?    
 Control Name:  Number of steps

 Description:   Select the number of steps in a sequence.

 Valid Range:   1 to 4095

 Default:       1    z    
 Control Name:  Segment Numbers []

 Description:   Each array element is the arbitrary waveform
                segment number to generate for that step of
                the sequence.  The number of cycles produced
                of the segment is dictated by the corresponding
                Repeat Segment [] array element

 Valid Range:   1 to 4096

 Default:       1    ?     
 Control Name:  Repeat Segment []

 Description:   Sets the number of times the arbitrary waveform
                segment is generated for the sequence.  The
                corresponding Segment Numbers [] element
                determines which arbitrary segment is being
                repeated.  This element indicates the number
                of times the segment is repeated before moving
                to the next step in the sequence.

 Valid Range:   1 to 1E6  (1000000)

 Default:       1    ? (  ?       Instr Handle                     V ?????       Error                            ? _ T ?       Number of Steps                   ` ? ?       Segment Numbers [ ]              ? a? ?       Repeat Segment [ ]              ???? ? ???                                                	           1            vNote: The Number of Steps must match the
number of elements in the Segment Numbers [ ]
and Repeat Segment [ ] arrays.    ?    
 Panel Name:    Delete Sequence

 Description:   Deletes all steps from any sequence which
                may have been previously defined.
 
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sequence Number

 Description:   Select the sequence number to be deleted.
 
 Valid Range:   0 to 4096

 Default:       1 (0 means delete all sequences)
    ? (  ?       Instr Handle                     N ?????       Error                            "? P ? ?       Sequence Number                        	           1    ?    
 Panel Name:    Output Sequence Waveform

 Description:   Select a Sequence Number and its Sampling Clock,
                Amplitude and Offset.  Also begins generating
                the sequence.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sampling Clock

 Description:   Selects the sampling clock used for each
                point in the sequence
 
 Valid Range:   10 uHz (10.0E-6) to 50 MHz (50.0E6)

 Default:       100.00 KHz (10.0E5)    7     Control Name:  Amplitude

 Description:   Selects the amplitude (Volts peak-to-peak)
 
 Valid Range:   0.01 to 16.0 V
 
 Default:       5.0 V

 Note:          The following relationship must hold between the
                amplitude and the offset:

                     (Amplitude / 2) + ABS(offset) <= 8.0     z     Control Name:  Offset

 Description:   Selects the offset level 

 Valid Range:   -7.19 to +7.19

 Default:       0.0 V    ?     Control Name:  Sequence Trigger Mode

 Description:   Selects the method used to advance from one
                seqment of the sequence to the next.  If
                "triggered" mode is used, the 3152A will wait
                until a valid trigger is received before 
                advancing to the next step in the sequence
                (including repeating the same segment).  If
                "auto" mode is used, the sequences continue
                one after the other.  NOTE:  Selecting the
                "trigger" mode will also place the 3152A in
                "Trigger" mode of operation.  The "Continuous"
                mode of operation is defeated when the
                "trigger" mode of operation is selected for
                the Sequence Trigger Mode

 Valid Range:   Triggered Mode = RI3152A_SEQ_TRIG = VI_TRUE = 1
                Automatic Mode = RI3152A_SEQ_AUTO = VI_FALSE = 0


 Default:       RI3152A_SEQ_AUTO   $? ? (  ?       Instr Handle                     &? ?????       Error                            ++ N S ?  x    Sampling Clock                   , N ? ?       Amplitude                        -M Nv ?       Offset                           -? ? ? ?       Sequence Trigger Mode                  	           100000000.0    5.0    0.0  6  Triggered RI3152A_SEQ_TRIG Automatic RI3152A_SEQ_AUTO    V    
Panel Name:    Sequence Mode Query

Description:   Reads the value of sequence mode     ?     Control Name:      Sequence Mode

 Description:       Returns the value of sequence mode

 Valid Range:       0 = Auto
                    1 = Trig    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   3? e ? ?  P    Sequence Mode                    47 ? (  ?       Instr Handle                     6s ?????       Error                              	                	           V     Panel Name:    Load Sequence Binary

 Description:   Load a sequence of binary data    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?    
 Control Name:  Number of steps

 Description:   Select the number of steps in a sequence.

 Valid Range:   1 to 4096

 Default:       1     ?    
 Control Name:  Segment Numbers []

 Description:   Select the number of segments in a sequence.

 Valid Range:   1 to 4096

 Default:       1     ?     
 Control Name:  Repeat Segment []

 Description:   Select the number of repeat segment in a   
                sequence.

 Valid Range:   1 to 1E6  (1000000)

 Default:       1   ;? ? (  ?       Instr Handle                     =? ?????       Error                            B0 [ E ?       Number of Steps                  B? [ ? ?       Segment Numbers [ ]              C\ [R ?       Repeat Segment [ ]              ???? ? ???                                                	           1            vNote: The Number of Steps must match the
number of elements in the Segment Numbers [ ]
and Repeat Segment [ ] arrays.   ?    
Panel Name:    Burst Trigger Mode

Description:   Selects the Burst Mode of operation and the
               Number of Cycles to produce when the waveform
               generator is triggered.  Use the Trigger Source
               function to select the trigger trigger source.

               If the Internal Trigger Source is selected,
               use the Trigger Rate function to program
               the internal trigger rate.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Number of Cycles  

 Description:   Select the number of cycles to be output per
                burst.

 Valid Range:   1 to 1000000

 Default:       1   G? ? "  ?       Instr Handle                     I? ?????       Error                            N W ? ?       Number of Cycles                       	           1    `    
Panel Name:    Burst Count Query

Description:   Returns the current value of the burst count     T     Control Name:      Burst Count

 Description:       Reads the current burst count    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   O? @ ? ?       Burst Count                      PF  &  ?       Instr Handle                     R? ?????       Error                              	                	           R    
Panel Name:    Trigger Source

Description:   Selects the input trigger source     4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Trigger Source  

 Description:   Select one of the Trigger Sources: Internal
                External, or one of the TTLTRG lines on the
                VXI backplane.

 Valid Range:   RI3152A_TRIGGER_INTERNAL     - 0
                RI3152A_TRIGGER_EXTERNAL     - 1
                RI3152A_TRIGGER_TTLTRG0      - 2
                RI3152A_TRIGGER_TTLTRG1      - 3
                RI3152A_TRIGGER_TTLTRG2      - 4
                RI3152A_TRIGGER_TTLTRG3      - 5
                RI3152A_TRIGGER_TTLTRG4      - 6
                RI3152A_TRIGGER_TTLTRG5      - 7
                RI3152A_TRIGGER_TTLTRG6      - 8
                RI3152A_TRIGGER_TTLTRG7      - 9

 Default:       External   W? "  ?       Instr Handle                     Z????       Error                            ^; ? ??      Trigger Source                         	                  
  DInternal RI3152A_TRIGGER_INTERNAL External RI3152A_TRIGGER_EXTERNAL TTLTRG0 RI3152A_TRIGGER_TTLTRG0 TTLTRG1 RI3152A_TRIGGER_TTLTRG1 TTLTRG2 RI3152A_TRIGGER_TTLTRG2 TTLTRG3 RI3152A_TRIGGER_TTLTRG3 TTLTRG4 RI3152A_TRIGGER_TTLTRG4 TTLTRG5 RI3152A_TRIGGER_TTLTRG5 TTLTRG6 RI3152A_TRIGGER_TTLTRG6 TTLTRG7 RI3152A_TRIGGER_TTLTRG7    q    
Panel Name:    Trigger Source Advance Query

Description:   Returns the current trigger source for the 3152A 
    h     Control Name:      Trigger Source

 Description:       Reads the presently selected 
                    trigger source  

 Valid Range:   RI3152A_TRIGGER_INTERNAL     - 0
                RI3152A_TRIGGER_EXTERNAL     - 1
                RI3152A_TRIGGER_TTLTRG0      - 2
                RI3152A_TRIGGER_TTLTRG1      - 3
                RI3152A_TRIGGER_TTLTRG2      - 4
                RI3152A_TRIGGER_TTLTRG3      - 5
                RI3152A_TRIGGER_TTLTRG4      - 6
                RI3152A_TRIGGER_TTLTRG5      - 7
                RI3152A_TRIGGER_TTLTRG6      - 8
                RI3152A_TRIGGER_TTLTRG7      - 9
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   c} Y ? ?  P    Trigger Source                   e?  &  ?       Instr Handle                     h) ?????       Error                              	                	           ?    
Panel Name:    Internal Trigger Rate

Description:   Selects the trigger rate.  This is meaningful
               only when the Internal Trigger is selected.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Trigger Period  

 Description:   Select a Trigger Period (if the instrument is 
                Internal mode only.)

 Valid Range:   60E-6 to 999 seconds

 Default:       1.0 second   m? "  ?       Instr Handle                     o?????       Error                            t0 M ? ?       Trigger Period                  ???? ? ???                                                	           1.0    MThe Trigger Period is used
only when the Internal
Trigger Source is selected    o    
Panel Name:    Trigger Rate Query

Description:   Returns the current value of the trigger rate in seconds

     f     Control Name:      Trigger Period

 Description:       Reads the current trigger period in seconds
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   v? @ ? ?       Trigger Period                   w!  &  ?       Instr Handle                     y] ?????       Error                              	               	           i    
Panel Name:    External Trigger Slope

Description:   Selects the active edge of the External Trigger.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    $     Control Name:  Trigger Source  

 Description:   Select a Trigger Source if the instrument is 
                External mode only.

 Valid Range:   RI3152A_SLOPE_POS (VI_TRUE) - Positive Slope
                RI3152A_SLOPE_NEG (VI_FALSE) - Negative Slope

 Default:       RI3152A_SLOPE_POS   ~? "  ?       Instr Handle                     ??????       Error                            ?- E ? ?       Trigger Slope                   ???? ? ???                                                	         , Pos RI3152A_SLOPE_POS Neg RI3152A_SLOPE_NEG    LThe Trigger Slope is used
only when the External
Trigger Source is selected    X    
Panel Name:    Trigger Slope Query

Description:   Returns the current trigger slope
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:      Trigger Slope

 Description:       Reads the current trigger slope

 Valid Range:     0 - Negative
                  1 - Positive
   ?  &  ?       Instr Handle                     ?W ?????       Error                            ?? P ? ?       Trigger Slope                          	           	            ?    
Panel Name:    Trigger Delay

Description:   Selects the delay between receipt of the trigger
               and the triggered action performed by the
               3152A waveform generator    3     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Trigger Delay 

 Description:   Selects a Trigger Delay.  This value is 
                the number of sample points to delay after
                receipt of the trigger.  For example, with
                a trigger delay of 10 and a sample clock
                frequency of 100 MHz, a 100 nanosecond delay
                is programmed.

 Valid Range:   10 to 2000000

 Default:       10   ?? "  ?       Instr Handle                     ??????       Error                            ? u ? ?       Trigger Delay                          	           10    d    
Panel Name:    Trigger Delay Query

Description:   Returns the current value of the trigger delay     _     Control Name:      Trigger delay value

 Description:       Reads the current trigger delay
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Trigger delay value              ?F  &  ?       Instr Handle                     ?? ?????       Error                              	               	           ?    
Panel Name:    Output Trigger

Description:   Programs the mode of operation and the
               Trigger Line on which the output trigger
               is generated    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
         Control Name:  Output Trigger Source  

 Description:   Selects the source of the trigger output
                from the 3152A waveform generator.
 
 Valid Range:   RI3152A_TRIGGER_INTERNAL     - 0

                    Generates a trigger signal at intervals
                    set by the internal trigger generator
                    (see the Trigger Rate Panel 
                     [ri3152a_trigger_rate()])

                RI3152A_TRIGGER_EXTERNAL     - 1

                     Generates a trigger signal every time
                     a trigger is applied to the front
                     panel TRIG IN connector

                RI3152A_TRIGGER_BIT         - 2

                     Generates a trigger signal at a particular
                     point in the waveform.  The trigger point
                     in the waveform can be programmed using
                     the BIT Trigger Point parameter of this
                     function.

                RI3152A_TRIGGER_LCOM         - 3

                     Generates a trigger signal in SEQuence
                     mode.  This trigger signal is generated
                     when the selected BIT Trigger point of the
                     selected LCOM Segment is reached.

 Default:       RI3152A_TRIGGER_BIT    ?     Control Name:  Output Trigger Line

 Description:   Select one of the VXI backplane TTLTRG lines
                to receive the trigger signal

 Valid Range:   RI3152A_TRIGGER_TTLTRG0      - 2
                RI3152A_TRIGGER_TTLTRG1      - 3
                RI3152A_TRIGGER_TTLTRG2      - 4
                RI3152A_TRIGGER_TTLTRG3      - 5
                RI3152A_TRIGGER_TTLTRG4      - 6
                RI3152A_TRIGGER_TTLTRG5      - 7
                RI3152A_TRIGGER_TTLTRG6      - 8
                RI3152A_TRIGGER_TTLTRG7      - 9
                RI3152A_TRIGGER_ECLTRG0      - 10
                RI3152A_TRIGGER_NONE         - 11

 Default:       RI3152A_TRIGGER_NONE    (     Control Name:  BIT Trigger Point

 Description:   Selects the point within the signal at which
                the trigger will be output when the BIT Trigger
                Mode is selected.  This is also the point 
                within the LCOM Segment when the LCOM Trigger
                Mode is selected.

                This has no meaning when the other trigger
                modes are used.

 Valid Range:   2 to 65534 (must be less than the number of
                points in the signal).  MUST BE AN EVEN NUMBER.

 Default:       2          Control Name:  LCOM Segment

 Description:   Selects the segment within the sequence at
                which the trigger pulse will be generated.
                This is used only in the LCOM Trigger Mode.
 
 Valid Range:   1 to 4096

 Default:       1     v     Control Name:  Output Width

 Description:   Specifies the pulse width

 Valid Range:   1 to 99%

 Default:       1   ?$# "  ?       Instr Handle                     ?`????       Error                            ?? 4 ? ??      Output Trigger Source            ?? 4~ ??      Output Trigger Line              ?L ? ? ?       BIT Trigger Point                ?| ? ? ?       LCOM Segment                     ?? ? ? ?       Output Width                           	                     vInternal RI3152A_TRIGGER_INTERNAL External RI3152A_TRIGGER_EXTERNAL BIT RI3152A_TRIGGER_BIT LCOM RI3152A_TRIGGER_LCOM           
  :TTLTRG0 RI3152A_TRIGGER_TTLTRG0 TTLTRG1 RI3152A_TRIGGER_TTLTRG1 TTLTRG2 RI3152A_TRIGGER_TTLTRG2 TTLTRG3 RI3152A_TRIGGER_TTLTRG3 TTLTRG4 RI3152A_TRIGGER_TTLTRG4 TTLTRG5 RI3152A_TRIGGER_TTLTRG5 TTLTRG6 RI3152A_TRIGGER_TTLTRG6 TTLTRG7 RI3152A_TRIGGER_TTLTRG7 ECLTRG0 RI3152A_TRIGGER_ECLTRG0 None RI3152A_TRIGGER_NONE    2    1    1    Z    
Panel Name:    Trigger Source Query

Description:   Returns the current trigger source
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     [     Control Name:      Trigger Source

 Description:       Reads the current trigger source
   ??  &  ?       Instr Handle                     ?  ?????       Error                            ?4 P ? ?       Trigger Source                         	           	            \    
Panel Name:    Output SYNC Pulse

Description:   Programs how the SYNC pulse is generated    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  SYNC Pulse Source  

 Description:   Selects the source (cause) of the SYNC
                Pulse generated to the SYNC output of the
                waveform generator

 Valid Range:   RI3152A_SYNC_OFF        - 0

                    Turns off the SYNC output pulse


                RI3152A_SYNC_BIT        - 1

                     Generates a SYNC Pulse every time
                     a semgent is output in arbitrary (USER)
                     mode or sequenced modes

                RI3152A_SYNC_LCOM       - 2

                     Generates a SYNC Pulse when in sequenced
                     mode.  This generates a pulse when the
                     selected segment (see the LCOM Segment
                     parameter) appears for the first time in
                     the sequence.

                RI3152A_SYNC_SSYN       - 3

                     Generates a SYNC Pulse at intervals that
                     are synchronized to the internal clock
                     generator.

                RI3152A_SYNC_HCLOCK  - 4

                     Generates a SYNC pulse at a rate equal
                     to 1/2 the sample clock period.

 Default:       RI3152A_SYNC_OFF    H     Control Name:  BIT SYNC Point

 Description:   Selects the point within the signal at which
                the Sync Pulse will be output when the BIT SYNC
                Mode is selected.  This also indicates point 
                within the selected LCOM Segment when the LCOM 
                Mode is selected.  This has no meaning when
                the other SYNC modes are used.
 
 Valid Range:   0 to memory size - 1 (and must be less than the                    
                number of points in the signal), MUST BE AN EVEN
                NUMBER

 Default:       2     ?     Control Name:  LCOM Segment

 Description:   Selects the segment within the sequence at
                which the SYNC pulse will be generated.
 
 Valid Range:   1 to 4096

 Default:       1     ?     Control Name:  Output Width

 Description:   Specifies the output sync width.

 Valid Range:   1%  to  99%
 
 Default:       1   ?? "  ?       Instr Handle                     ??????       Error                            ?& % ? ??      SYNC Pulse Source                ?? >' ?       BIT SYNC Point                   ?5 l' ?       LCOM Segment                     ?? ?' ?       Output Width                           	                      ?Off RI3152A_SYNC_OFF BIT RI3152A_SYNC_BIT LCOM RI3152A_SYNC_LCOM SSYNC RI3152A_SYNC_SSYNC 1/2 CLK RI3152A_SYNC_HCLOCK PULSE RI3152A_SYNC_PULSE    2    1    1    b    Panel Name:    Output Sync Query

Description:   Determines whether the output sync is ON or OFF    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     d     Control Name:  Output Sync State

 Description:   Determines whether the output sync is ON or OFF   ?? "  ?       Instr Handle                     ?(	????       Error                            ?\ X ? ?       Output Sync State                      	           	            g    
Panel Name:    Sync Source Query

Description:   Returns the current value of the output sync source     \     Control Name:      Sync Source

 Description:       Reads the current output sync source
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Sync Source                      ?V  &  ?       Instr Handle                     ے ?????       Error                              	                	           P    
Panel Name:    Output Sync Position

Description:   Selects the sync position    3     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    N     Control Name:  Sync Position 

 Description:   Selects the point within the signal at which
                the Sync Pulse will be output.
 
 Valid Range:   0 to memory size - 1 (and must be less than the                    
                number of points in the signal), MUST BE AN EVEN
                NUMBER

 Default:       2   ?? "  ?       Instr Handle                     ?????       Error                            ?H u ? ?       Sync Position                          	           2    k    
Panel Name:    Sync Position Query

Description:   Returns the current value of the output sync position     `     Control Name:      Sync Position

 Description:       Reads the current output sync position
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Sync Position                    ?-  &  ?       Instr Handle                     ?i ?????       Error                              	               	              
Panel Name:    Immediate Trigger

Description:   Will send an immediate trigger when this
               panel is executed.  This will trigger the
               3152A if the "BUS" Trigger Source is presently
               selected (see ri3152a_trigger_source()).
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?l ? "  ?       Instr Handle                     ?? ?????       Error                                  	           k    
Panel Name:    Set Trigger Level

Description:   Sets the threshold level at the trigger input connector    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Trigger Level  

 Description:   Contains the trigger level value

 Valid Range:   -10 V to 10 V

 Default:       1.6 V   ?? "  ?       Instr Handle                     ?????       Error                             9 M ? ?       Trigger Level                          	           1.6    o    
Panel Name:    Trigger Level Query

Description:   Returns the current value of the trigger level in volts

     g     Control Name:      Trigger level value

 Description:       Reads the current trigger level in volt
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ? @ ? ?       Trigger level value              f  &  ?       Instr Handle                     ? ?????       Error                              	               	           ?    
Panel Name:    Output Sync Width

Description:   Selects the sync width to control the rising
               edge of the sync pulse.    3     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sync Width 

 Description:   Selects a sync width to control the rising edge
                of the sync pulse.

 Valid Range:   1 to 99%

 Default:       1   
  "  ?       Instr Handle                     [????       Error                            ? u ? ?       Sync Width                             	           1    e    
Panel Name:    Sync Width Query

Description:   Returns the current value of the output sync width     Z     Control Name:      Sync Width

 Description:       Reads the current output sync width
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   f @ ? ?       Sync Width                       ?  &  ?       Instr Handle                      ?????       Error                              	               	           ]    Panel Name:    Set Trigger Delay State

Description:   Enable/disable trigger delay feature    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Trigger Delay State

 Description:   Determines whether the trigger delay state is 
                turned ON or OFF:

                    TRUE = Turn trigger delay on
                    FALSE = Turn trigger delay off   X "  ?       Instr Handle                     ?????       Error                             ? \ ??      Trigger Delay State                    	                     ON 1 OFF 0    ]    
Panel Name:    Trigger Delay State Query

Description:   Reads the state of trigger delay
     ?     Control Name:      Trig Delay State

 Description:       Returns the trigger delay state

                       0 = Trigger is OFF
                       1 = Trigger is ON    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   "? e ? ?  P    Trig Delay State                 #? ? (  ?       Instr Handle                     %? ?????       Error                              	                	           ?    
Panel Name:    Waveform Mode Selection Switch

Description:   Selects one of the Waveform Modes: STD, ARB,
               SEQ, and SWEEP    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sweep Mode

 Description:   Selects a sweep mode
 
 Valid Range:   1 - RI3152A_SINE
                2 - RI3152A_TRIANGLE
                3 - RI3152A_SQUARE

 Default:       1 - RI3152A_SINE   +_ ?    ?       Instr Handle                     -? ?????       Error                            1? ] ??      Sweep Mode                             	                      BSine RI3152A_SINE Triangle RI3152A_TRIANGLE Square RI3152A_SQUARE    Z    
Panel Name:    Query Sweep Function

Description:   Returns the current sweep function
     ?     Control Name:      Present Sweep Function

 Description:       Reads the present sweep function

 Valid Range:       1 :  SINE
                    2 :  TRIANGLE
                    3 :  SQUARE

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   4 D ? ?  P    Present Sweep Function           4?  &  ?       Instr Handle                     7 ?????       Error                              	                	           C    
Panel Name:    Set Sweep Time

Description:   Set the sweep time    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     d     Control Name:  Sweep Time

 Description:   Holds the sweep time
 
 Valid Range:   100E-3 to 10E3
   <K "  ?       Instr Handle                     >?????       Error                            B? b ? ?  P    Sweep Time                             	           0    Q    
Panel Name:    Query Sweep Time

Description:   Returns the present sweep time     S     Control Name:      Sweep Time

 Description:       Reads the current sweep time
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   DN @ ? ?       Sweep Time                       D?  &  ?       Instr Handle                     F? ?????       Error                              	               	           C    
Panel Name:    Set Sweep Step

Description:   Set the sweep step    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     `     Control Name:  Sweep Step

 Description:   Holds the sweep step
 
 Valid Range:   10 to 1000
   L "  ?       Instr Handle                     Nu????       Error                            R? b ? ?  P    Sweep Step                             	           0    Q    
Panel Name:    Query Sweep Step

Description:   Returns the present sweep step     S     Control Name:      Sweep Step

 Description:       Reads the current sweep step
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   T @ ? ?       Sweep Step                       Ty  &  ?       Instr Handle                     V? ?????       Error                              	               	           [    
Panel Name:    Set Sweep Direction

Description:   Set the sweep direction to up or down    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sweep Direction  

 Description:   Set the sweep direction

 Valid Range:   TRUE = Sweep Down (stop freq to start freq)
                FALSE = Sweep Up (start freq to stop freq)

 Default:       TRUE = Sweep Down   \ "  ?       Instr Handle                     ^C????       Error                            bw E ? ?       Sweep Direction                        	          Down 1 Up 0    \    
Panel Name:    Query Sweep Direction

Description:   Returns the current sweep direction
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:      Sweep Direction

 Description:       Reads the current sweep direction

 Valid range:       Down (1)
                    Up   (0)
   d?  &  ?       Instr Handle                     f? ?????       Error                            j? P ? ?       Sweep Direcion                         	           	            W    
Panel Name:    Set Sweep Spacing

Description:   Set the sweep spacing to LOG or LIN    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  Sweep Spacing  

 Description:   Set the sweep spacing

 Valid Range:   (VI_TRUE) - Logarithmic
                (VI_FALSE) - Linear

 Default:       Linear (VI_FALSE)   l? "  ?       Instr Handle                     n?????       Error                            s" E ? ?       Sweep Spacing                          	           Log 1 Lin 0    X    
Panel Name:    Query Sweep Spacing

Description:   Returns the current sweep spacing
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:      Sweep Spacing

 Description:       Reads the current sweep spacing
 
 Valid Range:       Logarithmic (1)
                    Linear      (0)
   u   &  ?       Instr Handle                     w< ?????       Error                            {p P ? ?       Sweep Spacing                          	           	            Y    
Panel Name:    Set Sweep Frequency Start

Description:   Set the sweep frequency start    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     r     Control Name:  Sweep Freq Start

 Description:   Holds the sweep frequency start
 
 Valid Range:   1E-3 to 9E6
   }7 "  ?       Instr Handle                     ?????       Error                            ?? Y ? ?  P    Sweep Freq Start                       	           
100.0E+03    g    
Panel Name:    Query Sweep Frequency Start

Description:   Returns the present sweep frequency start     d     Control Name:      Sweep Freq Start

 Description:       Reads the current sweep frequency start
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?f @ ? ?       Sweep Freq Start                 ??  &  ?       Instr Handle                     ? ?????       Error                              	               	           W    
Panel Name:    Set Sweep Frequency Stop

Description:   Set the sweep frequency stop    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     r     Control Name:  Sweep Freq Stop

 Description:   Holds the sweep frequency stop
 
 Valid Range:   10E-3 to 10E6
   ?\ "  ?       Instr Handle                     ??????       Error                            ?? Y ? ?  P    Sweep Freq Stop                        	           1.0E+06    e    
Panel Name:    Query Sweep Frequency Stop

Description:   Returns the present sweep frequency stop     b     Control Name:      Sweep Freq Stop

 Description:       Reads the current sweep frequency stop
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Sweep Freq Stop                  ??  &  ?       Instr Handle                     ?- ?????       Error                              	               	           [    
Panel Name:    Set Sweep Frequency Raster

Description:   Set the sweep frequency raster    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     x     Control Name:  Sweep Freq Raster

 Description:   Holds the sweep frequency raster
 
 Valid Range:   100E-3 to 100E6
   ? "  ?       Instr Handle                     ??????       Error                            ?	 Y ? ?  P    Sweep Freq Raster                      	           0    i    
Panel Name:    Query Sweep Frequency Raster

Description:   Returns the present sweep frequency raster     f     Control Name:      Sweep Freq Raster

 Description:       Reads the current sweep frequency raster
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Sweep Freq Raster                ?  &  ?       Instr Handle                     ?X ?????       Error                              	               	           [    
Panel Name:    Set Sweep Frequency Marker

Description:   Set the sweep frequency marker    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     v     Control Name:  Sweep Freq Marker

 Description:   Holds the sweep frequency marker
 
 Valid Range:   10E-3 to 10E6
   ?? "  ?       Instr Handle                     ? ????       Error                            ?4 Y ? ?  P    Sweep Freq Marker                      	           0    i    
Panel Name:    Query Sweep Frequency Marker

Description:   Returns the present sweep frequency marker     f     Control Name:      Sweep Freq Marker

 Description:       Reads the current sweep frequency marker
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? @ ? ?       Sweep Freq Marker                ?E  &  ?       Instr Handle                     ?? ?????       Error                              	               	              
Panel Name:    Phase Master Mode

Description:   Selects the phase master mode of operation
               for this 3152A waveform generator.  

               Note that both the master and slave MUST have
               the same number of points defined for the 
               active signal (standard, user, or sequence
               mode) for the phase lock to work correctly.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? "  ?       Instr Handle                     ?3????       Error                                  	          T    
Panel Name:    Phase Slave Mode

Description:   Selects the phase slave mode of operation
               for this 3152A waveform generator.  This
               3152A will be clocked (via ECLTRG0 and ECLTRG1)
               by the 3152A defined to be the master 3152A

               This allows a phase offset to be added to
               the slave 3152A

               Note that both the master and slave MUST have
               the same number of points defined for the 
               active signal (standard, user, or sequence
               mode) for the phase lock to work correctly.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Phase

 Description:   Holds the phase offset for the slave 3152A
 
 Valid Range:   0 to 360.  360 is an absolute maximum.  The
                usable maximum value is:

                     360 - (2880 / n), where n is the number
                     of points in the memory which comprises
                     the waveform.  The minimum number of 
                     points is 10.

                Ensuring that the programmed phase is within
                range is left to the user.  Note:  for standard
                waveforms, the number of points is:

                     1000  (if frequency < 100 KHz).
                           (Sine, Square, Triangle, Pulse,
                            Ramp)

                      500  (if frequency < 100 KHz)
                           (Sinc, Gaussian, Exponential)

                     100E6 / frequency  (if frequency > 100 KHz)
                           (all waveforms)

    ?= "  ?       Instr Handle                     ?y????       Error                            έ b ? ?  P    Phase                                  	               e    
Panel Name:    Phase Query

Description:   Provides the phase offset, phase lock, and phase source    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     T     Control Name:  Phase Offset

 Description:   Holds the phase offset for the 3152A     R     Control Name:  Phase State

 Description:   Holds the phase state for the 3152A     ?     Control Name:  Phase Source

 Description:   Holds the phase source for the 3152A

 Valid Range:   Master (0)
                Slave  (1)   Ӌ "  ?       Instr Handle                     ??????       Error                            ? R M ?       Phase Offset                     ?q R ? ?       Phase State                      ?? Rj ?       Phase Source                           	           	           	           	            H    
Panel Name:    Phase Lock Null

Description:   Set phase lock to null    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   ?? ?????       Error                            ? ? 0  ?       Instr Handle                       	               ?    Panel Name:    3152 Phase Lock Loop (PLL) On/Off

Description:   Determines whether the 3152A is in the Phase
               Lock Loop Mode.  

               This mode only works in Arbitrary and Standard
               Waveform mode.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
         Control Name:  Phase Lock Loop

 Description:   Determines whether the phase lock loop (PLL)
                mode is turned ON or OFF.  
 
 Valid Range:   RI3152A_PLL_ON -  VI_TRUE   (1)
                RI3152A_PLL_OFF - VI_FALSE  (0)

 Default:       RI3152A_PLL_OFF   ?( "  ?       Instr Handle                     ?d????       Error                            ?? ~ ??      Phase Lock Loop                        	                     &ON RI3152A_PLL_ON OFF RI3152A_PLL_OFF   &    
Panel Name:    Phase Lock Phase Adjust

Description:   Provides the (coarse) phase adjustment
               for the 3152A Phase Lock Loop Mode.

               When the 3152A is outputting "Standard" waveforms,
               this function will calculate the number of
               points in the standard waveform and adjust
               both the coarse and fine phase adjustment
               to achieve the desired phase adjustment
               as requested in the "Phase" parameter.

               When the 3152A is outputting the "Arbitrary" 
               waveforms, this function will ONLY adjust
               the coarse phase.  The fine phase adjustment
               must be performed using the "Phase Lock Fine
               Adjust" (ri3152a_pll_fine_phase) function.


              N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Phase

 Description:   Holds the phase offset for the 3152A
 
 Valid Range:   -180.0 to +180.0

 Accuracy:      The accuracy depends on the number of samples
                in the presently selected waveform.  The
                accuracy is:

                    360 / N

                Where N (= the Number of Samples) is determined
                by the presently selected waveform mode
                and standard waveform type:

                    If the generator is in Arbitrary Waveform
                    Mode, the Number of Samples is the number
                    of points allocated in the present segment.

                    If the generator is in Standard Waveform
                    Mode, the Number of Samples (N) is a 
                    function of the Waveform Type and the
                    Frequency (F):

                    SINE and SQUARE:

                       F <= 200 KHz,           N = 500 Points
                       200 KHz < F <= 10 MHz,  N = 100 MHz / F
                       F > 10 MHz,             N = 10

                    RAMP, PULSE, GAUSSIAN, and EXPONENTIAL:

                       F <= 100 KHz,           N = 1000 Points
                       F >  100 KHz,           N = 100 MHz / F

                    TRIANGLE and SINC

                       F <= 200 KHz,           N = 500 Points
                       F >  200 KHz,           N = 100 MHz / F

                    These calculations are used in generating
                    the coarse and fine phase adjustments for
                    the standard waveforms.  Only the coarse
                    adjustment is made for Arbitrary Mode
                    waveforms.
   ?? "  ?       Instr Handle                     ?????       Error                            ?J b ? ?  P    Phase                                  	           0   n    
Panel Name:    Phase Lock Fine Phase Adjust

Description:   Provides the fine phase adjustment
               for the 3152A Phase Lock Loop Mode.

               This is useful when the coarse phase adjust-
               ment does not provide enough granularity, 
               especially with the Arbitrary Waveform Mode
               of operation.
              N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     s     Control Name:  Phase

 Description:   Holds the fine phase offset for the 3152A
 
 Valid Range:   -36.0 to 36.0
   ?$ "  ?       Instr Handle                     z????       Error                            ? b ? ?  P    Fine Phase                             	           0       
Panel Name:    Query Phase Lock Loop

Description:   Returns the on/off state of the 3152A Phase Lock
               Mode, the coarse phase adjustment, the
               fine phase adjustment, and the external
               frequency to which the 3152A is locked.
         Control Name:      Phase Lock State

 Description:       Indicates whether the phase lock loop
                    mode is ON or OFF for the 3152A.

                       VI_TRUE (1)  = Phase Lock Loop ON
                       VI_FALSE (0) = Phase Lock Loop OFF

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:      Coarse Phase

 Description:       Indicates the coarse phase adjustment
                    for a 3152A in phase lock loop mode.  
                       ?     Control Name:      Fine Phase

 Description:       Indicates the fine phase adjustment
                    for a 3152A in phase lock loop mode.  
                         ?     Control Name:      External Frequency

 Description:       Indicates the external frequency to which
                    we are locked, as measured by the 3152A.
                       ? H ) ?  `    Phase Lock State                 	  &  ?       Instr Handle                     B ?????       Error                            v H ? ?  `    Coarse Phase                     ) J? ?  `    Fine Phase                       ? ? ? ?  `    Ext Frequency                      	                	           	           	           	          ?     Control Name:  Filter 

 Description:   Turn one of the Low Pass Filters on, or turn
                the Filter off.

                Note:  This function should not be used if
                       the "Standard Waveform" mode is 
                       in effect and the Sine Wave is 
                       being output.  The filters are 
                       automatically programmed by the
                       Standard Sine Wave.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Filter Switch 

 Description:   Turns one of the Low Pass Filters on, or turns
                the Filter off.

                Note: Low Pass Filters have no effect on the
                      Sine Wave.

 Valid Range:   0 = RI3152A_FILTER_OFF   :  Turn Filter Off
                1 = RI3152A_FILTER_20MHZ : use 20 MHz Filter
                2 = RI3152A_FILTER_25MHZ : use 25 MHz Filter
                3 = RI3152A_FILTER_50MHZ : use 50 MHz Filter

 Default:       OFF   ? ? (  ?       Instr Handle                      ?????       Error                            L A ??      Filter Switch                   ???? ? ???                                                	                      hOFF RI3152A_FILTER_OFF 20MHZ RI3152A_FILTER_20MHZ 25MHZ RI3152A_FILTER_25MHZ 50MHZ RI3152A_FILTER_50MHZ    NOTE:   Do NOT use this function if
              the Standard Waveform mode is
              presently outputting a Sine Wave    ?    
 Panel Name:    Amplitude Modulation

 Description:   Select the internal modulation depth in percent
                and the carrier frequency.
 
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  AM Percent

 Description:   Select the Internal Modulation depth in percent.
 
 Valid Range:   1% to 200%

 Default:       50%     ?     Control Name:  Amplitude Modulation

 Description:   Select the carrier frequency
 
 Valid Range:   10 to 500 

 Default:       100   ? ? (  ?       Instr Handle                     !? ?????       Error                            &' N l ?       AM Percent                       &? N7 ?       AM Frequency                    ???? ? ???                                                	           50    100    ANOTE:  By running this panel,
the current waveform is modulated.    d    
 Panel Name:    Query Amplitude Modulation

 Description:   Returns the amplitude modulation data    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152A_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152A_error_message function.  The
        ri3152A_error_message function may be used to
        convert the error code to a meaningful error
        message.
     @     Control Name:  AM Value

 Description:   Returns the AM value     P     Control Name:  AM Frequency

 Description:   Returns the AM carrier frequency   )##   ?       Instr Handle                     +_????       Error                            /? B e ?      AM Value                         /? B1 ?      AM Frequency                           	           	            	            ?    
Panel Name:    Clear

Description:   Performs a VXI word serial clear command. This
               results in clearing the input and output 
               buffer's of the arbitrary waveform generator.    3     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   2 ?   ?       Instr Handle                     4> ?????       Error                                  	           S    
Panel Name:    Trigger instrument

Description:   Sends a trigger to the device
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   9G ? +  ?       Instr Handle                     ;? ?????       Error                                  	           ]    
Panel Name:    Poll instrument

Description:   Reads the status byte from the instrument.
    ?     Control Name:      Status Byte

 Description:       Returns the IEEE-488.2 defined Status
                    Byte.  The bit assignments are as follows
                    (bit 0 is LSB)


            Bit #     Meaning
            ----------------------------------
              0       Not used, always 0
              1       Not used, always 0
              2       Not used, always 0
              3       Not used, always 0
              4       Message Available (MAV).  The device
                      has output which may be read by the
                      system controller
              5       Standard Event Status Bit (ESB).  One or
                      more of the enabled Standard Event Status
                      Register bits are set.  For an explanation
                      of the Standard Event Status Register, see
                      the description for "ri3152A_read_ESR"
              6       Master Status Summary (MSS).  This bit
                      indicates that one or more of the enabled
                      Status Byte bits are set.  For further 
                      details on enabling Status Byte bits, 
                      consult the "ri3152A_set_ESE" function.
              7       Not used, always 0
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   @? e ? ?  P    Status Byte                      E? ? (  ?       Instr Handle                     G? ?????       Error                              	               	          ;    
Panel Name:    Status Query

Description:   Returns the current value of the Voltage, 
               Frequency, Offset and Filter.

               Note: value returned for Filter type is
                     described in the following table.

                  Value              Representation
                  ------             --------------
                    0                 All filters are off
                    1                 20MHz filter ON
                    2                 25MHz filter ON
                    3                 50MHz filter ON
     ?     Control Name:      Voltage value

 Description:       Reads the current voltage amplitude
                    (Volts peak-to-peak)
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ]     Control Name:      Frequency value

 Description:       Reads the current frequency value
     W     Control Name:      Offset value

 Description:       Reads the current offset value
         Control Name:      Filter type

 Description:       Reads the current Filter type

                    Note: value returned for Filter type is
                     described in the following table.

                  Value              Representation
                  ------             --------------
                    0                 All filters are off
                    1                 20MHz filter ON
                    2                 25MHz filter ON
                    3                 50MHz filter ON
   N? H . ?       Voltage value                    O?  &  ?       Instr Handle                     Q? ?????       Error                            U? H ? ?       Frequency value                  VZ H  ?       Offset value                     V? H? ?       Filter type                     ???? }e??                                            	               	           	           	           	            dFilter type: 0:  OFF
                1:  20MHZ
                2:  25MHZ
                3:  50 MHZ    ^    
Panel Name:    Read Status Byte

Description:   Reads the status byte from the instrument.
    ?     Control Name:      Status Byte

 Description:       Returns the IEEE-488.2 defined Status
                    Byte.  The bit assignments are as follows
                    (bit 0 is LSB)


            Bit #     Meaning
            ----------------------------------
              0       Not used, always 0
              1       Not used, always 0
              2       Not used, always 0
              3       Not used, always 0
              4       Message Available (MAV).  The device
                      has output which may be read by the
                      system controller
              5       Standard Event Status Bit (ESB).  One or
                      more of the enabled Standard Event Status
                      Register bits are set.  For an explanation
                      of the Standard Event Status Register, see
                      the description for "ri3152a_read_ESR"
              6       Master Status Summary (MSS).  This bit
                      indicates that one or more of the enabled
                      Status Byte bits are set.  For further 
                      details on enabling Status Byte bits, 
                      consult the "ri3152a_set_SRE" function.
              7       Not used, always 0
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   [O e ? ?  P    Status Byte                      `B ? (  ?       Instr Handle                     b~ ?????       Error                              	               	              
Panel Name:    set SRE register

Description:   Programs the Service Request Enable (SRE)
               register.  This register enables bits of the
               Status Byte to generate a Service Request
               (SRQ if GPIB/VXI or VXI Request True Event)
    '     Control Name:      SRE Register

 Description:       Holds the SRE register value.  Each bit
                    enables the corresponding bit in the
                    Status Byte:

            Bit #     Meaning
            ----------------------------------
              0       Not used, always 0
              1       Not used, always 0
              2       Not used, always 0
              3       Not used, always 0
              4       Message Available (MAV).  The device
                      has output which may be read by the
                      system controller
              5       Standard Event Status Bit (ESB).  One or
                      more of the enabled Standard Event Status
                      Register bits are set.  For an explanation
                      of the Standard Event Status Register, see
                      the description for "ri3152a_read_ESR"
              6       This is the MSS bit.  Thus, the ESE
                      register should NOT have this bit set.
              7       Not used, always 0
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   h? e ? ?  P    SRE Register                     l? ? (  ?       Instr Handle                     n? ?????       Error                                      	           e    
Panel Name:    read SRE register

Description:   Reads the Service Request Enable (SRE) register.
    F     Control Name:      SRE Register

 Description:       Holds the present SRE register value upon
                    return from the function.  The bit 
                    assignments correspond to the Status Byte:

            Bit #     Meaning
            ----------------------------------
              0       Not used, always 0
              1       Not used, always 0
              2       Not used, always 0
              3       Not used, always 0
              4       Message Available (MAV).  The device
                      has output which may be read by the
                      system controller
              5       Standard Event Status Bit (ESB).  One or
                      more of the enabled Standard Event Status
                      Register bits are set.  For an explanation
                      of the Standard Event Status Register, see
                      the description for "ri3152a_read_ESR"
              6       This is the MSS bit.  Thus, the ESE
                      register should NOT have this bit set.
              7       Not used, always 0
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   tA e ? ?  P    SRE Register                     x? ? (  ?       Instr Handle                     z? ?????       Error                              	                	               
Panel Name:    Read ESR Register

Description:   Reads the IEEE-488.2 defined Standard Event
               Status Register
    ?     Control Name:      ESR Register

 Description:       Returns the IEEE-488.2 defined Standard
                    Event Status Register.  The bit assignments
                    are as follows (bit 0 is LSB)


            Bit #     Meaning
            ----------------------------------
              0       Operation Complete.  Set to 1 when the
                      *OPC command has been received and all
                      pending operations are complete
              1       Request Control.  Not used, always 0
              2       Query Error.  Set to 1 when an attempt
                      to read data from output queue when no
                      data is present.  
              3       Device Dependent Error.  Set when an
                      error in a device function occurs.  For
                      example, setting the voltage to 7.5 and
                      the offset to 4.1 is an illegal 
                      combination of legal components.
              4       Execution Error.  The value of a parameter
                      is outside legal limits.
              5       Command Error.  The command received by
                      the 3152A is invalid.
              6       User Request.  Not used, always 0.
              7       Power On.  Set when the devices power
                      source (VXI chassis) has been turned on.

              Note:  These bits are cleared by the 3152A after
                     the register is read.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?A e ? ?  P    ESR Register                     ? ? (  ?       Instr Handle                     ?R ?????       Error                              	               	          J    
Panel Name:    set ESE register

Description:   Programs the Standard Event Status Enable (ESE)
               register.  This register enables bits of the
               Status Event Status Register (ESR).  When
               one of the enabled ESR bits becomes set, bit
               4 of the Status Byte (ESB bit) is set.
    C     Control Name:      ESE Register

 Description:       Holds the ESE register value.  Each bit
                    enables the corresponding bit in the
                    ESR Register.  For a description of the
                    ESR register, see the description of the
                    "ri3152a_read_ESR" function.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?? e ? ?  P    ESE Register                     ?? ? (  ?       Instr Handle                     ? ?????       Error                                      	          H    
Panel Name:    read ESE register

Description:   Reads the Standard Event Status Enable (ESE)
               register.  This register enables bits of the
               Status Event Status Register (ESR).  When
               one of the enabled ESR bits becomes set, bit
               4 of the Status Byte (ESB bit) is set.
         Control Name:      ESE Register

 Description:       Holds the present ESE register value as
                    read from the instrument.  For a description
                    of the ESR register, see the description of
                    the "ri3152a_read_ESR" function.
    N     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.

 Variable Type: ViSession    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   ?Q e ? ?  P    ESE Register                     ?o ? (  ?       Instr Handle                     ?? ?????       Error                              	                	          ?    
 Panel Name:    Load Arbitrary Data

 Description:   Selects the arbitrary data segment and
                loads data into the segment


 NOTE:  This function uses a data type of ViUInt16 [],
        which is NOT supported by some languages, such as
        Visual BASIC.  Users of languages which do not 
        support the data type of ViUInt16 [] should use
        the function ri3152a_load_and_shift_arb_data(), which
        uses a data type of ViInt16[].    3     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    j     Control Name:  Data Points []

 Description:   Select an array of data to be down loaded to
                a specific memory segment.

 Data Range:    each point in the array represents a single
                DAC value.  The least significant 4 bits of
                the DAC value should be set to 0.  The
                DAC value thus ranges from:

                      0x0000 (0 decimal)      minimum to
                      0xFFFF (65535 decimal)  maximum in                    
                                              increments of
                      0x0010 (16 decimal)  

                The middle of the range voltage is thus
                produced with a value of 0x8000 (32768 decimal)

                The value of 0x0000 represents the minimum
                (most negative) voltage to generate, and the
                value of 0xFFF0 represents the maximum (most
                positive) voltage to generate.  The minimum
                and maximum voltages are determined by the
                "amplitude" and "offset" values in the
                ri3152a_output_arb_waveform() function.
    G     Control Name:  Number of points ( Segment Size )

 Description:   Select the segment size.  It should match
                the number of data points in the Data Points
                panel.
 
 Variable Type: ViInt32

 Valid Range:   10 to 64536 (64K version)
                10 to 523288 (512K version)

 Default:       10     z     Control Name:  Segment Number

 Description:   Select the segment number.

 Valid Range:   1 to 4096

 Default:       1   ?? ? (  ?       Instr Handle                     ?? ?????       Error                            ?? K ? ?       Data Point Array                 ?o Kk ?       Number of Points                ???? u ???                                          ?? K 4 ?       Segment Number                         	               10    ^Note: The number of points must match the Segment
Size on the panel: Define Arbitrary Segment    1    V    
Panel Name:    Sequence Mode Query

Description:   Reads the value of sequence mode     ?     Control Name:      Share Memory Mode

 Description:       Returns the value of Share Memory mode

 Valid Range:       0 = Read
                    1 = Write    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     e     Control Name:      Share Memory State

 Description:       Returns the value of Share Memory State   ?\ O y ?  P    Share Memory Mode                ? ? (  ?       Instr Handle                     ?? ?????       Error                            ?s O4 ?  P    Share Memory State                 	                	           	           ?    
 Control Name:  Load/Shift Arbitrary Data

 Description:   Selects the arbitrary data segment and
                loads data into the segment.  The data
                is shifted left 4 places prior to loading
                to convert DAC values in the range 0 to 
                4095 into values used by the 3152A hardware.


 NOTE:  This function is provided solely as an alternative
        to "ri3152a_load_arb_data()".  This function is 
        provided for those languages, such as Visual BASIC,
        which do NOT support the VISA data type ViUInt16 [].
        For most users, the "ri3152a_load_arb_data()" is
        preferred because of the increased data download
        throughput.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Data Points []

 Description:   Select an array of data to be down loaded to
                a specific memory segment.
 
 Data Range:    each point in the array represents a single
                DAC value.  The DAC values range from:

                      0x0000 (0 decimal)      minimum to
                      0x0FFF (4095 decimal)   maximum 

                The middle of the range voltage is thus
                produced with a value of 0x0800 (2048 decimal)

                The value of 0x0000 represents the minimum
                (most negative) voltage to generate, and the
                value of 0x0FFF represents the maximum (most
                positive) voltage to generate.  The minimum
                and maximum voltages are determined by the
                "amplitude" and "offset" values in the
                ri3152a_output_arb_waveform() function.

                This function shifts the value of each data
                point left 4 places prior to loading.  This
                is done to accommodate the 3152A hardware. 
                This causes a performance penalty over
                the function "ri3152a_load_arb_data()".    .     Control Name:  Number of points ( Segment Size )

 Description:   Select the segment size.  It should match
                the number of data points in the Data Points
                panel.
 
 Valid Range:   10 to 64536 (64K version)
                10 to 523288 (512K version)

 Default:       10     {     Control Name:  Segment Number

 Description:   Select the segment number.
 
 Valid Range:   1 to 4096

 Default:       1   ?? ? (  ?       Instr Handle                     ?? ?????       Error                            ? K ? ?       Data Point Array                 ?? Kk ?       Number of Points                ???? u ???                                          ?? K 4 ?       Segment Number                         	               10    ^Note: The number of points must match the Segment
Size on the panel: Define Arbitrary Segment    1    ?    
 Control Name:  Load ASCII file

 Description:   Selects the arbitrary data segment and
                loads data from an ASCII data file into
                the waveform generator.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    *     Control Name:  Number of points ( Segment Size )

 Description:   Select the segment size.  This should match
                the number of points defined for the segment.
                This should be less than or equal to the
                number of points in the file; if there are
                less data points than are specified with this
                parameter, then the number of points in
                the file will be used.

 Valid Range:   10 to 64536 (64K version)
                10 to 523288 (512K version)

 Default:       10    ?     Control Name:  File Name

 Description:   Determines the name of the file to read
 
 Valid Range:   The file should be composed of ASCII numbers
                separated by one or more white space characters 
                (including end-of-line).  Each number must be
                in the range -2048 to +2047.  The value of 
                -2048 will produce the minimum (most negative) 
                voltage, and the value of 2047 will produce the 
                maximum (most positive) voltage.  The minimum
                and maximum voltages are determined by the
                "amplitude" and "offset" parameters of the
                "ri3152a_output_arb_waveform()" function.

 Default:       "file.asc"     {     Control Name:  Segment Number

 Description:   Select the segment number.
 
 Valid Range:   1 to 4096

 Default:       1   ?? ? (  ?       Instr Handle                     ?9 ?????       Error                            ?m H? ?       Number of Points                ???? n ???                                          ԟ H ? ?       File Name                        ׀ H A ?       Segment Number                         	           10    ^Note: The number of points must match the Segment
Size on the panel: Define Arbitrary Segment    "file.asc"    1   G    
 Control Name:  Load WaveCAD (version 2.0) file

 Description:   Loads all data from a WaveCAD file (*.CAD).
                This includes loading all waveform data
                files specified in the WaveCAD file.

                The instrument configuration is programmed
                as a result of this function.
    3     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
     ?     Control Name:  File Name

 Description:   Determines the name of the file to read
 
 Valid Range:   The file must be one created by WaveCAD version
                2.0 for the Racal 3152A.

 Default:       "C:\\wcad20\\rac3152A\\sample.cad"   ? ? (  ?       Instr Handle                     ?U ?????       Error                            ?? b ? ?       WaveCAD File Name                      	           #"C:\\wcad20\\rac3152A\\sample.cad"   {    
 Control Name:  Load WaveCAD (version 2.0) Waveform File

 Description:   Loads the data from the specified WaveCAD
                waveform data file into the specified 
                segment.

                The WaveCAD data file is a sequence of 16-bit
                two's complement signed binary data.  Each
                data point is in the range -2048 to +2047.    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  File Name

 Description:   Determines the name of the file to read
 
 Valid Range:   The file should be composed of binary 16-bit
                signed integer data.  Each data point must be 
                between -2048 (minimum amplitude) and +2047
                (maximum amplitude).  This file is normally
                created and edited using WaveCAD for the 
                Racal 3152A

 Default:       "C:\\wcad20\\rac3152A\\sample.wav"     {     Control Name:  Segment Number

 Description:   Select the segment number.
 
 Valid Range:   1 to 4096

 Default:       1   ?? ? (  ?       Instr Handle                     ? ?????       Error                            ?L b) ?       WaveCAD Waveform File Name       ?( b k ?       Segment Number                         	           #"C:\\wcad20\\rac3152A\\sample.wav"    1    w    
Panel Name:    Change Mode

Description:   Turns the instrument mode to either normal or 
               fast mode

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    /     Control Name:  Output Switch

 Description:   Turns the output signal On or Off
 
 Valid Range:   RI3152A_MODE_FAST -  VI_TRUE   (1)
                RI3152A_MODE_NORMAL - VI_FALSE  (0)

 Default:       RI3152A_MODE_FAST  (note:  instrument *RST
                                   condition is NORMAL)   ?9 ? (  ?       Instr Handle                     ?u ?????       Error                            ?? u ??      Mode                                   	                      2Fast RI3152A_MODE_FAST Normal RI3152A_MODE_NORMAL    }    
Panel Name:    Format Border

Description:   Specifies the border to be formatted in normal
               swapped mode.

    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    5     Control Name:  Output Switch

 Description:   Turns the output signal On or Off
 
 Valid Range:   RI3152A_FORMAT_NORMAL -  VI_FALSE   (0)
                RI3152A_FORMAT_SWAP - VI_TRUE  (1)

 Default:       RI3152A_FORMAT_SWAP  (note:  instrument *RST
                                   condition is NORMAL)   ?U ? (  ?       Instr Handle                     ?? ?????       Error                            ?? u ??      Format                                 	                      6Swap RI3152A_FORMAT_SWAP Normal RI3152A_FORMAT_NORMAL    W    
Panel Name:    Format Border Query

Description:   Reads the value of format border
     [     Control Name:      Format Border

 Description:       Returns the value of format border    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   U e ? ?  P    Format Border                    ? ? (  ?       Instr Handle                     ? ?????       Error                              	                	          j    
Panel Name:    Format Wave

Description:   Enables or disables access to the least
               significant 4 bits for arbitrary waveform
               download data.  When enabled, the user can
               set the least significant 4 bits.  When 
               disabled, the least significant bits are
               ignored from the downloaded data.
    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Format

 Description:   Enables or disables access to the least
                significant 4 bits of arbitrary waveform
                data that is downloaded.
 
 Valid Range:   NORMAL - VI_FALSE (0) = access is disabled
                USER   - VI_TRUE  (1) = access is enabled

 Default:       USER  (note:  instrument *RST
                    condition is NORMAL)   
U ? (  ?       Instr Handle                     ? ?????       Error                            ? u ??      Format                                 	                      User 1 Normal 0    S    
Panel Name:    Format Wave Query

Description:   Reads the value of format wave
     ?     Control Name:      Format Wave

 Description:       Returns the value of format wave
                    "NORM" = Normal mode is active
                    "USER" = user mode is active    4     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.

                Note that the initialize function may
                be used to associate multiple instrument
                handles with a single instrument.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
   x e ? ?  P    Format Wave                      ; ? (  ?       Instr Handle                     w ?????       Error                              	                	          ?    
Panel Name:    Reset

Description:   Resets the instrument to the power-up state


    Attribute                Power-Up State
    -------------------------------------------------------
    Output State:            Off
    Trigger Mode:            Continuous
    Operating Mode:          Standard Waveform (Sine)
    Frequency:               1.0 MHz
    Amplitude:               5.0 V
    Offset:                  0.0
    Filter State:            Off
    Filter Frequency:        20 MHz
    TTLTRG0-7:               Off
    ECLTRG0/1:               Off
    InterModule Phase Sync:  Off
    Trigger Slope:           Positive
    Shared Memory State:     Off
    Shared Memory Mode:      Read
    SYNC Out State:          Off
    SYNC Out Position:       2 (undefined)
    Internal Trigger Period: 100 microseconds
    Trigger Advance Mode:    Automatic
    Arbitrary Memory Clock:  1 MHz     
        ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   ? ?????       Error                            #& ? 0  ?       Instr Handle                       	               ?    
Panel Name:    Operation Complete

Description:   Set the "operation complete"  bit (bit 0) in the 
               Standard Event register after the previous
               commands have been executed
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   & ?????       Error                            *B ? 0  ?       Instr Handle                       	               R    
Panel Name:    Self-Test

Description:   Performs a self-test of the instrument     ?     Control Name:  Pass/Fail

 Description:   Flag which indicates pass or 
                failure of the self-test

 Valid Range:   0 - pass
                1 - fail
     ?     Control Name:  Self-Test Message

 Description:   Indicates which self-test failed,
                if any.  If no self-tests failed,
                this will be a null string.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   ,? = z ?       Pass/Fail                        -_ =@ ?       Self Test Message                .????       Error                            2P (  ?       Instr Handle                       	            	            	              ?    
Panel Name:    Revision Query

Description:   Queries the instrument for its firmware revision
               and also returns the driver revision.

               Revisions are returned in strings.  The revision
               information for this instrument takes the form

                  "<XX>.<Y>"

               where:

                  <XX> is a 2-digit number representing the
                       major revision number

                  <Y>  is a 1 or 2-digit number representing
                       the minor revision number (typically "1")

                Example:

                    "13.1"   13 = major revision, 1 = minor
     ?     Control Name:  Driver Revision

 Description:   Holds a string which defines the
                current revision of this instrument
                driver.
     ?     Control Name:  Firmware Revision

 Description:   Holds the firmware revision as
                read from the 3152A *IDN? query
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   7y = | ?       Driver Revision                  8! =@ ?       Firmware Revision                8? ?????       Error                            <? *  ?       Instr Handle                       	            	            	               P    
Panel Name:    Version Query

Description:   Queries the instrument's version     h     Control Name:  Version

 Description:   Holds the version returned
                by the instrument
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   ?? G ? ?       Version                          @? ?????       Error                            Ds )  ?       Instr Handle                       	            	               ^    
Panel Name:    Option Query

Description:   Queries the instrument's memory (512K or 64K)

     ?     Control Name:  Option

 Description:   Holds the option returned
                by the instrument

                1 -  512K
                0 -  64K
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   G. G ? ?       Option                           G? ?????       Error                            L )  ?       Instr Handle                       	            	               s    
Panel Name:    Error Query

Description:   Queries the instrument for any errors it has
               detected.     ?     Control Name:  Error?

 Description:   The indication whether an error has occured
                0 = no error detected by the instrument
                1 = error detected by the instrument
     |     Control Name:  Error Message

 Description:   The error message for the error returned
                by the instrument
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   N? = | ?       Error?                           O? =9 ?       Error Message                    P# ?????       Error                            TW )  ?       Instr Handle                       	            	            	               ?    
Panel Name:    Error Message

Description:   Converts a numeric error code returned by one
               of the functions of this driver into a 
               descriptive error message string     ?     Control Name:  Error return code

 Description:   Accepts the error code for returned
                by one of the functions in this
                instrument driver.
     ?     Control Name:  Error Message

 Description:   Upon return from the function,
                holds a text error message which
                corresponds to the error code.
    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   W? ? ? ?       Error Return Value               Xm ?9 ?       Error Message                    Y% ?????       Error                            ]Y ,  ?       Instr Handle                           	            	              ?    
Panel Name:    Close

Description:   Closes the instrument and deallocates the
               resources allocated by the call to the
               initialization function.  This should be 
               called once for every instrument handle returned
               by the initialize function.  This should be
               called prior to terminating the application
               program.    ,     Control Name:  Error

 Description:   Displays the return status of the
                function call.  If the function was
                successful, it will return a status
                of VI_SUCCESS, otherwise it will
                return an error code.  Passing
                the error code into the function
                "ri3152a_error_message" will return
                a string describing the error. 

 VTL/VISA Errors:
        See section 3.3 of the VPP 4.2.2 document
        for a complete list of the WIN Framework
        error codes and their values.  Appendix B
        of VPP 4.2 contains descriptions and a
        breakdown of the error codes returned by
        each of the VTL functions.

 RI3152A Driver Errors
        Errors returned from the RI3152A drivers
        will be between 0xBFFC0800 and 0xBFFC0FFF
        and are described in the source code for
        the ri3152a_error_message function.  The
        ri3152a_error_message function may be used to
        convert the error code to a meaningful error
        message.
    ?     Control Name:  Instr Handle

 Description:   The Instrument Handle is used to
                identify the unique session or
                communication channel between the
                driver and the instrument.

                If more than one instrument of the
                same model type is used, this
                Handle will be used to
                differentiate between them.
   a} ?????       Error                            e? ? +  ?       Instr Handle                       	              
?         ޞ  ?n     K.    init                              ?         ??  ?/     K.    example_generate_seq_waveform     E         ?? ?     K.    output                            ?        p ?     K.    query_output                      ?        ? ?     K.    output_shunt                      R        ?  ?     K.    query_output_shunt                        !? )A     K.    output_eclt                       ?        *
 1+     K.    query_output_eclt                          1? <     K.    select_waveform_mode              A        =* FN     K.    select_standard_waveform          ?        G? T?     F.    mode_query                        1        U? `E     K.    operating_mode                    r        ac i?     F.    query_operating_mode               ?        ja v?     K.    set_frequency                     #1        w? ~?     F.    frequency_query                   #?        ? ??     K.    set_amplitude                     &!        ?d ??     F.    query_amplitude                   &?        ?z ??     K.    set_offset                        )        ?7 ??     F.    query_offset                      *?        ?A ?.     K.    sine_wave                         ,?        ?? ??     K.    set_sine_wave_phase               /        ?? ??     F.    sine_phase_query                  /?        ?v ɦ     K.    set_sine_wave_power               2        ?Z щ     F.    sine_power_query                  2?        ?D ??     K.    triangular_wave                   4?        ߒ ??     K.    set_triangle_phase                77        ?? ??     F.    triangle_phase_query              7?        ?? ??     K.    set_triangle_power                :'        ?? ??     F.    triangle_power_query              :?        ?u 	?     K.    square_wave                       =        
?      K.    set_square_dcycle                 ?X        ? ?     F.    square_dcycle_query               @        ? )D 	    K.    pulse_wave                        BH        +j 3?     K.    set_pulse_data                    D?        5# =<     F.    pulse_data_query                  E8        >? K?     K.    ramp_wave                         Gy        M? U?     K.    set_ramp_data                     I?        V? ^?     F.    ramp_data_query                   Ji        _? i?     K.    sinc_wave                         L?        k: rm     K.    set_sinc_ncycle                   N?        s" zd     F.    sinc_ncycle_query                 O?        { ?:     F.    exponential_wave                  Q?        ?? ??     K.    set_exponential_exp               T        ?? ?     F.    exponential_exp_query             T?        ?? ??     K.    gaussian_wave                     W        ?0 ?_     K.    set_gauss_exp                     YM        ? ?N     F.    gauss_exp_query                   Y?        ?	 ??     K.    dc_signal                         \=        ?f      F.    dc_amplitude_query              ????        ?R ҁ     K.    load_segment_binary               ]?        Ӏ ?     K.    define_arb_segment                ^<        ?  ?-     K.    delete_segments                   ^?        ?Y ??     K.    output_arb_waveform               _?        ?? ?q     K.    sclk_source_query                 `I        ?,      K.    arb_sampling_freq                 `?        ? ?     F.    query_sampling_freq               bs        ? ?     K.    define_sequence                   c"        x #5     K.    delete_sequence                   c?        #? 1?     K.    output_sequence_waveform          d?        3; :?     K.    sequence_mode_query               e/        ;b D     K.    load_sequence_binary              f?        E? N?     K.    burst_mode                        h?        O? V?     F.    burst_count_query                 i?        Wq a     K.    trigger_source                    k?        c l]     F.    trigger_source_adv_query          l}        m u     K.    trigger_rate                      n?        v< }?     F.    trigger_rate_query                om        ~L ?Y     K.    trigger_slope                     q?        ?? ?+     F.    trigger_slope_query               r]        ?? ??     K.    trigger_delay                     t?        ?s ??     F.    trigger_delay_query               uM        ?q ?     K.    output_trigger                    w?        ?b ??     F.    trigger_source_query              x=        ?R ·     K.    output_sync                       z~        Ђ ??     K.    output_sync_query                 |?        ؃ ??     F.    sync_source_query                 }n        ?? ??     K.    output_sync_pos                   ?        ?R ??     F.    sync_pos_query                    ?^        ?X ??     K.    immediate_trigger                 ??        ?V  ?     K.    set_trigger_level                 ??        ? ?     F.    trigger_level_query               ??        	? E     K.    output_sync_width                 ??        ? 8     F.    sync_width_query                  ?        ? !?     K.    trig_delay_state                  ??        "? *     K.    trig_delay_state_query            ?S        *? 2?     K.    sweep_function                    ??        3? ;E     F.    sweep_func_query                  ?C        <  CA     K.    set_sweep_time                    ??        C? K     F.    query_sweep_time                  ?3        K? S     K.    set_sweep_step                    ?t        S? Z?     F.    query_sweep_step                  ?#        [? cf     K.    set_sweep_direction               ?d        d$ k?     F.    query_sweep_direction             ?        lS s?     K.    set_sweep_spacing                 ?T        t? |     F.    query_sweep_spacing               ?        |? ?;     K.    set_sweep_freq_start              ?D        ?? ?B     F.    query_sweep_freq_start            ??        ?? ?`     K.    set_sweep_freq_stop               ?4        ? ?a     F.    query_sweep_freq_stop             ??        ? ??     K.    set_sweep_freq_raster             ?$        ?= ??     F.    query_sweep_freq_raster           ??        ?G ??     K.    set_sweep_freq_marker             ?        ?f ??     F.    query_sweep_freq_marker           ?	        ?p ?g     K.    phase_master                      ?J        ?? ?k     K.    phase_slave                       ??        ? ?^     K.    phase_query                       ??        ܛ ??     K.    phase_lock_null                   ?}        ?3 ??     K.    phase_lock_loop                   ??        ?? ??     K.    pll_phase                         ??        ?? )     K.    pll_fine_phase                    ?@        ? ?     F.    pll_query                         ??         <     K.    filter                            ??         'M     K.    amplitude_modulation              ?M        (? 03     K.    amplitude_modulation_query        ?d        1/ 8r     K.    clear                             ?#        8? ??     K.    trigger                           ??        @1 K?     K.    poll                              ??        L? X?     F.    status_query                      ?
        Z? f?     K.    read_status_byte                  ù        gm s!     K.    set_SRE                           ?h        s? ~?     K.    read_SRE                          ?        ? ??     K.    read_ESR                          ??        ?A ?N     K.    set_ESE                           ?u        ? ??     K.    read_ESE                          ?E        ?? ?@     K.    load_arb_data                     ??        ?? ??     K.    share_memory_query                ɣ        ?? ?|     K.    load_and_shift_arb_data           ?R        ?: ?     K.    load_ascii_file                   ?        ?? ??     K.    load_wavecad_file                 ˰        ?Y ??     K.    load_wavecad_wave_file            ?        ?? ??     K.    change_mode                       μ        ??       K.    format_border                     ?k         ? (     K.    format_border_query               ?        ? O     K.    format_wave                       ??         ?     K.    format_wave_query                 ?x        f $?     K.    reset                             ?E        %: +?     K.    opc                               ?        ,V 3?     K.    self_test                         ?K        4? >{     K.    revision_query                    ?        ?w F     K.    version_query                     ?Z        F? M?     K.    option_query                      ۘ        NY U?     K.    error_query                       ??        V? ^?     K.    error_message                     ݩ        _? gK     K.    close                                 ?                                     DInitialize                          5Application Functions                DExample: Generate Seq. Waveform     Configure Functions                  DOutput On/Off                        DQuery Output                         DOutput Shunt On/Off                  DQuery Output Shunt                   DOutput ECLT                          DQuery Output ECLT                   _Operating Mode                       DWaveform Mode Selection              DStandard Waveform Selection          DQuery Waveform Mode                  DOperating Mode                       DQuery Operating Mode                 !Waveform Parameters                  DSet Frequency                        DQuery Frequency                      DSet Amplitude                        DQuery Amplitude                      DSet Offset                           DQuery Offset                        )?Standard Waveforms                   DSine Wave                            DSine Phase                           DQuery Sine Phase                     DSine Power                           DQuery Sine Power                     DTriangular Wave                      DTriangle Phase                       DQuery Triangle Phase                 DTriangle Power                       DQuery Triangle Power                 DSquare Wave                          DSquare Duty Cycle                    DQuery Square Duty Cycle              DPulse Wave                           DSet Pulse Data                       DQuery Pulse Data                     DRamp Wave                            DSet Ramp Data                        DQuery Ramp Data                      DSinc Wave                            DSinc NCycle                          DQuery Sinc NCycle                    DExponential Wave                     DExponential Exponent                 DQuery Exponential Exponent           DGaussian Wave                        DGauss Exponent                       DQuery Gauss Exponent                 DDC Signal                            DQuery DC Amplitude                  \?Arbitrary Waveforms                  DLoad Segment Binary                  DDefine Arbitrary Segment             DDelete Segments                      DOutput Arbitrary Waveform            DQuery Sample Clock Source            DSampling Frequency                   DQuery Sampling Frequency            a?Sequence Programming                 DDefine Sequence                      DDelete Sequence                      DOutput Sequence Waveform             DQuery Sequence Mode                  DLoad Sequence Binary                e?Trigger & Sync                       DBurst Trigger Mode                   DQuery Burst Count                    DTrigger Source                       DQuery Trigger Source Advance         DInternal Trigger Rate                DQuery Trigger Rate                   DExternal Trigger Slope               DQuery Trigger Slope                  DTrigger Delay                        DQuery Trigger Delay                  DOutput Trigger                       DQuery Trigger Source                 DOutput Sync Pulse                    DQuery Output Sync                    DQuery Sync Source                    DOutput Sync Position                 DQuery Sync Position                  DImmediate Trigger                    DSet Trigger Level                    DQuery Trigger Level                  DOutput Sync Width                    DQuery Sync Width                     DSet Trigger Delay State              DQuery Trigger Delay State           ?oSweep Functions                      DSweep Function                       DQuery Sweep Function                 DSet Sweep Time                       DQuery Sweep Time                     DSet Sweep Step                       DQuery Sweep Step                     DSet Sweep Direction                  DQuery Sweep Direction                DSet Sweep Spacing                    DQuery Sweep Spacing                  DSet Sweep Frequency Start            DQuery Sweep Frequency Start          DSet Sweep Frequency Stop             DQuery Sweep Frequency Stop           DSet Sweep Frequency Raster           DQuery Sweep Frequency Raster         DSet Sweep Frequency Marker           DQuery Sweep Frequency Marker        ??Phase                                DPhase Master Mode                    DPhase Slave Mode                     DQuery Phase                          DPhase Lock Null                     ??3152A Phase Lock Loop                DPhase Lock On/Off                    DPhase Lock Phase Adjust              DPhase Lock Fine Phase Adjust         DQuery Phase Lock Loop                DFilter                               DAmplitude Modulation                 DQuery Amplitude Modulation          ??Action/Status Functions              DClear instrument                     DTrigger instrument                   DPoll Instrument                      DStatus Query                        ?JIEEE 488.2 Status                    DRead Status Byte                     DSet SRE Register                     DRead SRE Register                    DRead ESR Register                    DSet ESE Register                     DRead ESE Register                   ?$Data Operations                      DLoad Arbitrary Data                  DQuery Share Memory                   DLoad/Shift Arbitrary Data            DRead ASCII data file                 DLoad WaveCAD File                    DLoad WaveCAD Waveform File          ?_Utility Functions                    DChange Mode                          DFormat Border                        DQuery Format Border                  DFormat Wave                          DQuery Format Wave                    DReset                                DOperation Complete                   DSelf-Test                            DRevision Query                       DVersion Query                        DOption Query                         DError Query                          DError Message                        DClose                           