s??         q?     mQ  @   ?   ????                               hpe1428a    HPE1428A Digitizing Oscilloscope (VISA)          ? ??ViStatus     	? 	??ViSession     ? ??ViRsrc     	? 	??ViBoolean     ? ??ViString  ? ? ??ViInt16     ?  ViChar[]  ?  ? ??ViInt32  '    This class of functions provides lower level functions to communicate with the instrument, and change instrument parameters.

Functions:

(1) Write To Instrument:
This function writes commands and queries to the instrument to modify parameters and query device settings.

(2) Read Instrument Data:
This function reads data from instrument buffer and returns it to the specified variable in memory.

(3) Reset:
This function resets the instrument to its default state.

(4) Self-Test:
This function runs the instrument self test and returns the test code.

(5) Error Query:
This function reads an error code from the instrument error queue.

(6) Error Message:
This function takes the Status Code and returns it as a user readable string.
 
(7) Revision Query:
This function returns the revision numbers of the instrument driver and instrument firmware.

(8) Wait for RQS
This function waits for a Service Request from the device with "viWaitOnEvent".  If the event arrives within the specified timeout period, the status byte of the device is read and returned.    2    This function performs the following initialization actions:

- Opens a session to the Default Resource Manager resource and a session to the specified device using the interface and address specified in the Resource_Name control.

- Performs an identification query on the Instrument.

- Resets the instrument to a known state.

- Sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.

- Returns an Instrument Handle which is used to differentiate between different sessions of this instrument driver.

- Each time this function is invoked a Unique Session is opened.  It is possible to have more than one session open for the same resource.
    m    This control specifies the interface and address of the device that is to be initialized (Instrument Descriptor). The exact grammar to be used in this control is shown in the note below. 

Default Value:  "VXI::24"

Notes:

(1) Based on the Instrument Descriptor, this operation establishes a communication session with a device.  The grammar for the Instrument Descriptor is shown below.  Optional parameters are shown in square brackets ([]).

Interface   Grammar
------------------------------------------------------
VXI        VXI[board]::primary address[::secondary address]
            [::INSTR]
            
The VXI keyword is used with VXI instruments.

The default value for optional parameters are shown below.

Optional Parameter          Default Value
-----------------------------------------
board                       0
secondary address           none - 31
        This control specifies if an ID Query is sent to the instrument during the initialization procedure.

Valid Range:
VI_OFF (0) - Skip Query
VI_ON  (1) - Do Query (Default Value)

Notes:

(1) Under normal circumstances the ID Query ensures that the instrument initialized is the type supported by this driver. However circumstances may arise where it is undesirable to send an ID Query to the instrument.  In those cases; set this control to "Skip Query" and this function will initialize the selected interface, without doing an ID Query.    C    This control specifies if the instrument is to be reset to its power-on settings during the initialization procedure.

Valid Range:
VI_OFF (0) - Don't Reset
VI_ON  (1) - Reset Device (Default Value)

Notes:

(1) If you do not want the instrument reset, set this control to "Don't Reset" while initializing the instrument.    7    This control returns an Instrument Handle that is used in all subsequent function calls to differentiate between different sessions of this instrument driver.

Notes:

(1) Each time this function is invoked a Unique Session is opened.  It is possible to have more than one session open for the same resource.
        This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).
 
3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

BFFC0002  Parameter 2 (ID Query) out of range.
BFFC0003  Parameter 3 (Reset Device) out of range.
BFFC0011  Instrument returned invalid response to ID Query.

BFFC0803  Error Scanning Response VI_ERROR_INTERPRETING_RESPONSE

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
    ? =   ?  ?    Resource Name                     ? : ?       ID Query                           =? ?       Reset Device                      j ? ? ?  }    Instrument Handle                 ?#????  ?    Status                          ????   O??                                            
"VXI::24"  ! Do Query VI_ON Skip Query VI_OFF  & Reset Device VI_ON Don't Reset VI_OFF    	           	           GCopyright 1996 National Instruments Corporation.  All Rights Reserved.    m    This function writes commands and queries to the instrument to modify parameters and query device settings.    c    The user can use this control to send common commands and queries to the instrument. This control can also be used to write any valid command to the instrument.

Default Value:  "*RST"

Notes:

(1) The command or query to be sent to the instrument may be a literal enclosed in double quotes i.e. "*RST" or may be contained in a variable of type string.
    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).
 
3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    ? M E ? ?    Write Buffer                      
#????  ?    Status                            ?#   ?  ?    Instrument Handle                  "*RST"    	            ???? ????       _VI_FUNC                        ???? ! -  ?       instrSession                    ???? d ,         MemOffset1                      ???? ? -         MemOffset2                      ???? 0         MemoryPoints                    ???? ??         VMEAddress                      ???? ? -         MathOffset                         	            0    0    0    0    	            0 ????????       Status                          ???? $   ?       Inst Session                    ???? j          YInc                            ???? ? (         YOff                            ???? ? '         XInc                            ????& /         XOff                            ???? C ?          Area                            ???? ? ?          Total Channels                  ???? ? ?          Aquire Type                        	           0    0    0    0    0    0    0    0 ????????       Status                          ???? ? !          WaveArray                       ???? ? ?         TimeArray                          	           	           	          7    This function reads data from the instrument's output buffer and returns it to the specified variable in memory.

Notes:

(1) Because the instrument may return both numeric and text data in response to queries this function returns the data in string format.

(2) If valid data is not available at the instrument's output buffer when this function is called the instrument will hang up and the function will not return until it times out.  If the time-out is disabled this function will hang indefinitely and it may be necessary to reboot the computer to break out.    ?    The number of bytes specified by this control should be greater than or equal to the number of bytes which are to be read from the instrument. If the actual number of bytes to be read is greater than the number this control specifies then multiple reads will be required to empty the instrument's output buffer.

If the instrument's output buffer is not emptied the instrument may return invalid data when future reads are performed.

Default Value:  50 (See NOTE 2)

Notes:

(1) If NO DATA is available at the instrument's output buffer when this function is called the instrument will hang up and the function will not return until it times out.  If the time-out is disabled, this function will hang indefinitely and it may be necessary to reboot the computer.

(2) If the number of bytes expected is greater than 50 the value of this control may be increased. If the actual number of bytes read is larger than the declared size of the read buffer a run-time error will be generated.
    ?    The incoming data from the instrument is placed into this variable.

Notes:

(1) Because the instrument may return both numeric and text data in response to queries this function returns the data in string format.

(2) This function does not overwrite any old data left in the string variable from the last time the function was called. String data in LabWindows/CVI is terminated with an ASCII null(0x0) and string manipulation functions will only recognize data before the ASCII null.

(3) The declared size of the string variable must be greater than the actual number of bytes read from the instrument if it is not a run-time error will be generated.
    ?    This variable contains the actual number of bytes read from the instrument. This is the value which is returned by the read function.

Notes:

(1) If the actual number of bytes read is less than the number of bytes specified in the Number Bytes To Read control then the output buffer has probably been emptied. If the read function fails and the number of bytes read is 0, the most probable cause for the failure is there was no data available at the instrument's output buffer.    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    $? M K ?       Number Bytes To Read              (? ? H ? ?    Read Buffer                       +< Mx ?       NumBytes Read                     -$#????  ?    Status                            0?#   ?  ?    Instrument Handle                  50    	            	            	                  This function resets the instrument to a known state and sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.

    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    3?#????  ?    Status                            7k#   ?  ?    Instrument Handle                  	                  This function resets the instrument to a known state and sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.

    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    9?#????  ?    Status                            =P#   ?  ?    Instrument Handle                  	                  This function resets the instrument to a known state and sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.

    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    ??#????  ?    Status                            C5#   ?  ?    Instrument Handle                  	            ???? $   ?       instrSession                    ????????       Status                          ???? ){ ?       buffFull                           0    	           	            X    This function runs the instrument's self test routine and returns the test result(s).
     ?    This control contains the value returned from the instrument self test.  Zero means success.  For any other code, see the device's operator's manual.
     ?    This control contains the string returned from the self test. See the device's operation manual for an explanation of the string's contents.

Notes:

(1) The array must contain at least 256 elements ViChar[256].    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    Ej = 2 ?       Self Test Result                  F
 = ? ? ,    Self-Test Message                 F?#????  ?    Status                            J?#   ?  ?    Instrument Handle                  	           	            	               H    This function reads an error code from the instrument's error queue.

     N    This control returns the error code read from the instrument's error queue.
     ?    This control returns the error message string read from the instrument's error message queue.

Notes:

(1) The array must contain at least 256 elements ViChar[256].    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    Lw = 2 ?       Error Code                        L? = ? ? ,    Error Message                     M{#????  ?    Status                            Q #   ?  ?    Instrument Handle                  	            	            	               ?    This function takes the Status Code returned by the instrument driver functions, interprets it and returns it as a user readable string.      t    This control accepts the Status Code returned from the instrument driver functions.

Default Value:
0 - VI_SUCCESS     ?    This control returns the interpreted Status Code as a user readable message string.

Notes:

(1) The array must contain at least 256 elements ViChar[256].    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  VI_NULL    SN =  ?       Error Code                        S? = ? ? ?    Error Message                     Tn#????  ?    Status                            X#   ?  ?    Instrument Handle                  0    	            	           VI_NULL       This function returns the revision numbers of the instrument driver and instrument firmware, and tells the user what firmware the driver is compatible with, this instrument driver's Revision Number is "Rev 1.0, 2/96, CVI 3.1".  The firmware revision number is unknown.  
     ?    This control returns the Instrument Driver Software Revision.

Notes:

(1) The array must contain at least 256 elements ViChar[256].         This control returns the Instrument Firmware Revision.

Notes:

(1) The array must contain at least 256 elements ViChar[256].    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).
 
3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    Z? = 2 ?  ?    Instrument Driver Revision        [X =, ?  ?    Firmware Revision                 [?#????  ?    Status                            _?#   ?  ?    Instrument Handle                  	            	            	               P    This function waits for a service request to be generated by the instrument.      ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

BFFC0002  Parameter 2 (Timeout) out of range.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None     ?    Maximum time to wait (in milliseconds) for an event
of the specified type to occur.  Special values are:
 VI_TMO_IMMEDIATE (dequeue only, do not wait)
 VI_TMO_INFINITE  (wait forever, do not fail)
     +    The status byte read from the instrument.    ax#????  ?    Status                            eL#   ?  ?    Instrument Handle                 e? @ B ?       Timeout (ms)                      f? <b ?       Status Byte                        	               30000    	           ?    This function performs the following operations:
viClose (instrSession) and viClose (rmSession).

Notes:

(1) The instrument must be reinitialized to use it again.    ?    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

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
     ?    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    h?#????  ?    Status                            l8#   ?  ?    Instrument Handle                  	            ????         M  0     K.    init                            ????         *  O     K.    writeInstrData                  ????       ????       K.    FindInst                        ????       ????  ? 	    K.    WvData                          ????       ????  !?     K.    GtWv                            ????         "?  1h     K.    readInstrData                   ????         2?  8
     K.    reset                           ????         8?  =?     K.    softReset                       ????         >i  C?     K.    ResetTM                         ????       ????  DN     K.    msgWaiting                      ????         E
  K+     K.    selfTest                        ????         L'  Q?     K.    errorQuery                      ????         R?  X?     K.    errorMessage                    ????         Y?  `$     K.    revisionQuery                   ????         a   f?     K.    waitForRqs                      ????         g?  l?     K.    close                               ????                                     DInitialize                          Utility Functions                    DWrite To Instrument                  DFind Instrument                      DWavve Data                           DGet Waveform                         DRead Instrument Data                 DReset                                DSoft Reset                           DReset Timeout                        DMessage Waiting                      DSelf-Test                            DError-Query                          DError Message                        DRevision Query                       DWait for RQS                         DClose                           