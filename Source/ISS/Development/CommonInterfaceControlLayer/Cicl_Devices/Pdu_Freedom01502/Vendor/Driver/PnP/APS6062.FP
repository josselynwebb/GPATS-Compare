s��         SQ   
  Q1      �   ����                               aps6062     APS6062 Programmable Power Source(VISA)       � � ��ViInt16  �  � ��ViInt32  � � ��ViReal64     � ��ViRsrc     	� 	��ViBoolean     	� 	��ViSession     � ��ViStatus     �  ViChar[]     � ��ViChar     � ��ViString     	�  ViInt16[]     	�  ViInt32[]     
�  	ViReal64[]     � 	 
ViBoolean[]  �    This instrument module provides programming support for the APS 6062 Programmable Power Sources.  The module is divided into the following functions:

Functions/Classes:

(1) Initialize:
This function initializes the instrument and sets it to a default configuration.

(2) Utility Functions: (Class)
This class of functions provides lower level functions to communicate with the instrument, and change instrument parameters.

(3) Close:
This function takes the instrument offline.
    ,    This class of functions performs  operations which are common among all Test and Measurement instruments.

Functions/Classes:


(1) Write to Instrument:
-This function writes commands and queries to the instrument to modify parameters and query device settings.

(2) Read Instrument Data:
-This function reads data from the instrument's output buffer and returns it to the specified variable in memory.

(3) Reset:
- Resets the instrument to its reset state documented in the instrument manual.

(4) Self Test:
- Performs a complete self test of the instrument.

(5) Error Query:
-This function reads an error code from the instrument's error queue.

(6) Error Message:
-This function takes the Status Code returned by the instrument driver functions, interprets it and returns it as a user readable string. 

    2    This function performs the following initialization actions:

- Opens a session to the Default Resource Manager resource and a session to the specified device using the interface and address specified in the Resource_Name control.

- Performs an identification query on the Instrument.

- Resets the instrument to a known state.

- Sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.

- Returns an Instrument Handle which is used to differentiate between different sessions of this instrument driver.

- Each time this function is invoked a Unique Session is opened.  It is possible to have more than one session open for the same resource.
    r    This control specifies the interface and address of the device that is to be initialized (Instrument Descriptor). The exact grammar to be used in this control is shown in the note below. 

Default Value:  "GPIB::22"

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

(1) If you do not want the instrument reset. Set this control to "Don't Reset" while initializing the instrument.    7    This control returns an Instrument Handle that is used in all subsequent function calls to differentiate between different sessions of this instrument driver.

Notes:

(1) Each time this function is invoked a Unique Session is opened.  It is possible to have more than one session open for the same resource.
    ;    This control contains the status code returned by the function call.

Status Codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0005  The specified termination character was read.
3FFF0006  The specified number of bytes was read.

BFFC0002  Parameter 2 (ID Query) out of range.
BFFC0003  Parameter 3 (Reset Device) out of range.
BFFC0011  Instrument returned invalid response to ID Query

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
    	� @   �  �    Resource Name                     n = �       ID Query                          � @� �       Reset Device                      � � � �  }    Instrument Handle                 ����  �    Status                          ����   ��                                            "GPIB::22"  ! Do Query VI_ON Skip Query VI_OFF  & Reset Device VI_ON Don't Reset VI_OFF    	           	           FCopyright 1995 National Instruments Corporation. All Rights Reserved.    n    This function writes commands and queries to the instrument to modify parameters and query device settings.
    c    The user can use this control to send common commands and queries to the instrument. This control can also be used to write any valid command to the instrument.

Default Value:  "*RST"

Notes:

(1) The command or query to be sent to the instrument may be a literal enclosed in double quotes i.e. "*RST" or may be contained in a variable of type string.
    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    � = E � �    Write Buffer                      9����  �    Status                            � 	  �  �    Instrument Handle                  "*RST"    	              8    This function reads data from the instrument's output buffer and returns it to the specified variable in memory.

Notes:

(1) Because the instrument may return both numeric and text data in response to queries this function returns the data in string format.

(2) If valid data is not available at the instrument's output buffer when this function is called the instrument will hang up and the function will not return until it times out.  If the time-out is disabled this function will hang indefinitely and it may be necessary to reboot the computer to break out.
    �    The number of bytes specified by this control should be greater than or equal to the number of bytes which are to be read from the instrument. If the actual number of bytes to be read is greater than the number this control specifies then multiple reads will be required to empty the instrument's output buffer.

If the instrument's output buffer is not emptied the instrument may return invalid data when future reads are performed.

Default Value:  50 (See NOTE 2)

Notes:

(1) If NO DATA is available at the instrument's output buffer when this function is called the instrument will hang up and the function will not return until it times out.  If the time-out is disabled, this function will hang indefinitely and it may be necessary to reboot the computer.

(2) If the number of bytes expected is greater than 50 the value of this control may be increased. If the actual number of bytes read is larger than the declared size of the read buffer a run-time error will be generated.
    �    The incoming data from the instrument is placed into this variable.

Notes:

(1) Because the instrument may return both numeric and text data in response to queries this function returns the data in string format.

(2) This function does not overwrite any old data left in the string variable from the last time the function was called. String data in LabWindows/CVI is terminated with an ASCII null(0x0) and string manipulation functions will only recognize data before the ASCII null.

(3) The declared size of the string variable must be greater than the actual number of bytes read from the instrument if it is not a run-time error will be generated.
    �    This variable contains the actual number of bytes read from the instrument. This is the value which is returned by the read function.

Notes:

(1) If the actual number of bytes read is less than the number of bytes specified in the Number Bytes To Read control then the output buffer has probably been emptied. If the read function fails and the number of bytes read is 0, the most probable cause for the failure is there was no data available at the instrument's output buffer.    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    !v = K �       Number Bytes To Read              %Z � H � �    Read Buffer                       '� =x �       NumBytes Read                     )�����  �    Status                            -� 	  �  �    Instrument Handle                  50    	            	            	                  This function resets the instrument to a known state and sends initialization commands to the instrument that set any necessary programmatic variables such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.
    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    0|����  �    Status                            4! 	  �  �    Instrument Handle                  	              "    This function runs the instrument's self test routine and returns the test result.

Notes:
(1) The Self-Test routine will take approximately 7 seconds.

(2) This instrument does not return a self-test result string so the numerical result is copied into the string parameter and returned.     �    This control contains the value returned from the instrument self test.  Zero means success.  One means the self-test failed.  For any other code, see the device's operator's manual.
    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    ^    This control contains the string returned from the self test. See the device's operation manual for an explanation of the string's contents.

Notes:

(1) The array must contain at least 256 elements ViChar[256].

(2) This instrument does not return a self-test result string so the numerical result is copied into the string parameter and returned.    6d = 4 �       Self Test Result                  7%����  �    Status                            :� 	  �  �    Instrument Handle               ���� � A��                                           ;i = � � ,    Self-Test Message                  	           	               9The Self-Test routine will take approximately 7 seconds.    	            G    This function reads an error code from the instrument's error queue.
     N    This control returns the error code read from the instrument's error queue.
     �    This control returns the error message string read from the instrument's error message queue.

Notes:

(1) The array must contain at least 256 elements ViChar[256].    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    >� = 2 �       Error Code                        >� = � � ,    Error Message                     ?�����  �    Status                            C4 	  �  �    Instrument Handle                  	            	            	               �    This function takes the Status Code returned by the instrument driver functions, interprets it and returns it as a user readable string.
     t    This control accepts the Status Code returned from the instrument driver functions.

Default Value:
0 - VI_SUCCESS     �    This control returns the interpreted Status Code as a user readable message string.

Notes:

(1) The array must contain at least 256 elements ViChar[256].    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  VI_NULL    Eb =  �       Error Code                        E� = � � �    Error Message                     F�����  �    Status                            J' 	  �  �    Instrument Handle                  0    	            	           VI_NULL    �    This function performs the following operations:
viClose (instrSession) and viClose (rmSession).

Notes:

(1) The instrument must be reinitialized to use it again.
    �    This control contains the status code returned by the function call.

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
     �    This control accepts the Instrument Handle returned by the Initialize function to select the desired instrument driver session.

Default Value:  None    Ls����  �    Status                            P 	  �  �    Instrument Handle                  	            ����         �  ^     K.    init                            ����         X  }     K.    writeInstrData                  ����         6  .     K.    readInstrData                   ����         /V  4�     K.    reset                           ����         5:  <�     K.    selfTest                        ����         ><  C�     K.    errorQuery                      ����         D�  J�     K.    errorMessage                    ����         K�  P�     K.    close                                 �                                     DInitialize                          �Utility Functions                    DWrite To Instrument                  DRead Instrument Data                 DReset                                DSelf-Test                            DError-Query                          DError Message                        DClose                           