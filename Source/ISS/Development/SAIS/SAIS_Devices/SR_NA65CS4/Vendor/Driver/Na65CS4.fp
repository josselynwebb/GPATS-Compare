s??        ??   y i?  L   ?   ????                               na65cs3     Synchro/Resolver                                 ? ??NA65CS3Found  ? ? ??ViInt16  ?  ? ??ViInt32  ? ? ??ViReal64     ? ??ViRsrc     	? 	??ViBoolean     	? 	??ViSession     ? ??ViStatus     ?  ViChar[]     ? ??ViChar     ? ??ViString     	?  ViInt16[]     	?  ViInt32[]     
?  	ViReal64[]     ? 	 
ViBoolean[]     ? ??ViConstString     ? ??ViAttr   ?    This instrument driver contains programming support for the Synchro/Resolver.  This driver has all the functions that IVI and VXIplug&play require.  

Note:  This driver requires the VISA and IVI libraries.       ?    This class contains high-level test and measurement functions.  
These functions call other instrument driver functions to configure and perform complete instrument operations.
     ?    This class contains functions and sub-classes that configure the instrument.  The class includes high-level functions that configure multiple instrument settings as well as low-level functions that set, get, and check individual attribute values.
     T    This class contains sub-classes for the set, get, and check attribute functions.       ?    This class contains functions that set an attribute to a new value.  There are typesafe functions for each attribute data type.     ?    This class contains functions that obtain the current value of an attribute.  There are typesafe functions for each attribute data type.     ?    This class contains functions that obtain the current value of an attribute.  There are typesafe functions for each attribute data type.     m    This class contains functions and sub-classes that initiate instrument operations and report their status.
     _    This class contains functions and sub-classes that transfer data to and from the instrument.
    _    This class contains functions and sub-classes that control common instrument operations.  These functions include many of functions that VXIplug&play require, such as reset, self-test, revision query, error query, and error message.  This class also contains functions that access IVI error infomation, lock the session, and perform instrument I/O.
     R    This class contains functions that retrieve and clear the IVI error information.     ?    This class contains functions that retrieve coercion records.     J    This class contains functions that retrieve interchangeability warnings.     k    This class contains functions that lock and unlock IVI instrument driver sessions for multithread safefy.     F    This class contains functions that send and receive instrument data.    A    This function performs the following initialization actions:

- Creates a new IVI instrument driver session.

- Opens a session to the specified device using the interface and address you specify for the resourceName parameter.

- If the IDQuery parameter is set to VI_TRUE, this function queries the instrument ID and checks that it is valid for this instrument driver.

- If the reset parameter is set to VI_TRUE, this function resets the instrument to a known state.

- Sends initialization commands to set the instrument to the state necessary for the operation of the instrument driver.

- Returns a ViSession handle that you use to identify the instrument in all subsequent instrument driver function calls.

Note:  This function creates a new session each time you invoke it. Although you can open more than one IVI session for the same resource, it is best not to do so.  You can use the same session in multiple program threads.  You can use the na65cs3_LockSession and na65cs3_UnlockSession functions to protect sections of code that require exclusive access to the resource.

        Pass the resource name of the device to initialize.

You can also pass the name of a driver session or logical name that you configure with the IVI Configuration utility.  The driver session identifies a specific device and specifies the initial settings for the session.  A logical Name identifies a particular driver session.

Refer to the following table below for the exact grammar to use for this parameter.  Optional fields are shown in square brackets ([]).

Syntax
------------------------------------------------------
GPIB[board]::<primary address>[::secondary address]::INSTR
VXI[board]::<logical address>::INSTR
GPIB-VXI[board]::<logical address>::INSTR
ASRL<port>::INSTR
<LogicalName>
<Driver Session>

If you do not specify a value for an optional field, the following values are used:

Optional Field - Value
------------------------------------------------------
board - 0
secondary address - none (31)

The following table contains example valid values for this parameter.

"Valid Value" - Description
------------------------------------------------------
"GPIB::22::INSTR" - GPIB board 0, primary address 22 no
                    secondary address
"GPIB::22::5::INSTR" - GPIB board 0, primary address 22
                       secondary address 5
"GPIB1::22::5::INSTR" - GPIB board 1, primary address 22
                        secondary address 5
"VXI::64::INSTR" - VXI board 0, logical address 64
"VXI1::64::INSTR" - VXI board 1, logical address 64
"GPIB-VXI::64::INSTR" - GPIB-VXI board 0, logical address 64
"GPIB-VXI1::64::INSTR" - GPIB-VXI board 1, logical address 64
"ASRL2::INSTR" - COM port 2
"SampleInstr" - Logical name "SampleInstr"
"xyz432" - Logical Name or Driver Session "xyz432"

/*=CHANGE:===================================================* 

Modify the following default value so that it reflects the default address for your instrument.  You must make the corresponding change to the Default Value entry for the control.

 *================================================END=CHANGE=*/ 
Default Value:  "VXI::14::INSTR"

        Specify whether you want the instrument driver to perform an ID Query.

Valid Range:
VI_TRUE  (1) - Perform ID Query (Default Value)
VI_FALSE (0) - Skip ID Query

When you set this parameter to VI_TRUE, the driver verifies that the instrument you initialize is a type that this driver supports.  

Circumstances can arise where it is undesirable to send an ID Query command string to the instrument.  When you set this parameter to VI_FALSE, the function initializes the instrument without performing an ID Query.     ?    Specify whether you want the to reset the instrument during the initialization procedure.

Valid Range:
VI_TRUE  (1) - Reset Device (Default Value)
VI_FALSE (0) - Don't Reset

    ?    Returns a ViSession handle that you use to identify the instrument in all subsequent instrument driver function calls.

Notes:

(1) This function creates a new session each time you invoke it.  This is useful if you have multiple physical instances of the same type of instrument.  

(2) Avoid creating multiple concurrent sessions to the same physical instrument.  Although you can create more than one IVI session for the same resource, it is best not to do so.  A better approach is to use the same IVI session in multiple execution threads.  You can use functions na65cs3_LockSession and na65cs3_UnlockSession to protect sections of code that require exclusive access to the resource.

    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors    ? =   ?  ?    resourceName                      ? : ?       IDQuery                           ? =? ?       reset                             ? 
 ?  ?    instrumentHandle                  V#????  ?    Status                             "VXI::14::INSTR"   Yes VI_TRUE No VI_FALSE   Yes VI_TRUE No VI_FALSE    	           	          I    This function performs the following initialization actions:

- Creates a new IVI instrument driver and optionally sets the initial state of the following session attributes:

    NA65CS3_ATTR_RANGE_CHECK         
    NA65CS3_ATTR_QUERY_INSTRUMENT_STATUS  
    NA65CS3_ATTR_CACHE               
    NA65CS3_ATTR_SIMULATE            
    NA65CS3_ATTR_RECORD_COERCIONS    

- Opens a session to the specified device using the interface and address you specify for the Resource Name parameter.

- If the ID Query parameter is set to VI_TRUE, this function queries the instrument ID and checks that it is valid for this instrument driver.

- If the Reset parameter is set to VI_TRUE, this function resets the instrument to a known state.

- Sends initialization commands to set the instrument to the state necessary for the operation of the instrument driver.

- Returns a ViSession handle that you use to identify the instrument in all subsequent instrument driver function calls.

Note:  This function creates a new session each time you invoke it. Although you can open more than one IVI session for the same resource, it is best not to do so.  You can use the same session in multiple program threads.  You can use the na65cs3_LockSession and na65cs3_UnlockSession functions to protect sections of code that require exclusive access to the resource.

        Pass the resource name of the device to initialize.

You can also pass the name of a driver session or logical name that you configure with the IVI Configuration utility.  The driver session identifies a specific device and specifies the initial settings for the session.  A logical Name identifies a particular driver session.

Refer to the following table below for the exact grammar to use for this parameter.  Optional fields are shown in square brackets ([]).

Syntax
------------------------------------------------------
GPIB[board]::<primary address>[::secondary address]::INSTR
VXI[board]::<logical address>::INSTR
GPIB-VXI[board]::<logical address>::INSTR
ASRL<port>::INSTR
<LogicalName>
<Driver Session>

If you do not specify a value for an optional field, the following values are used:

Optional Field - Value
------------------------------------------------------
board - 0
secondary address - none (31)

The following table contains example valid values for this parameter.

"Valid Value" - Description
------------------------------------------------------
"GPIB::22::INSTR" - GPIB board 0, primary address 22 no
                    secondary address
"GPIB::22::5::INSTR" - GPIB board 0, primary address 22
                       secondary address 5
"GPIB1::22::5::INSTR" - GPIB board 1, primary address 22
                        secondary address 5
"VXI::64::INSTR" - VXI board 0, logical address 64
"VXI1::64::INSTR" - VXI board 1, logical address 64
"GPIB-VXI::64::INSTR" - GPIB-VXI board 0, logical address 64
"GPIB-VXI1::64::INSTR" - GPIB-VXI board 1, logical address 64
"ASRL2::INSTR" - COM port 2
"SampleInstr" - Logical name "SampleInstr"
"xyz432" - Logical Name or Driver Session "xyz432"

/*=CHANGE:===================================================* 

Modify the following default value so that it reflects the default address for your instrument.  You must make the corresponding change to the Default Value entry for the control.

 *================================================END=CHANGE=*/ 
Default Value:  "VXI::14::INSTR"

        Specify whether you want the instrument driver to perform an ID Query.

Valid Range:
VI_TRUE  (1) - Perform ID Query (Default Value)
VI_FALSE (0) - Skip ID Query

When you set this parameter to VI_TRUE, the driver verifies that the instrument you initialize is a type that this driver supports.  

Circumstances can arise where it is undesirable to send an ID Query command string to the instrument.  When you set this parameter to VI_FALSE, the function initializes the instrument without performing an ID Query.     ?    Specify whether you want the to reset the instrument during the initialization procedure.

Valid Range:
VI_TRUE  (1) - Reset Device (Default Value)
VI_FALSE (0) - Don't Reset

    ?    Returns a ViSession handle that you use to identify the instrument in all subsequent instrument driver function calls.

Notes:

(1) This function creates a new session each time you invoke it.  This is useful if you have multiple physical instances of the same type of instrument.  

(2) Avoid creating multiple concurrent sessions to the same physical instrument.  Although you can create more than one IVI session for the same resource, it is best not to do so.  A better approach is to use the same IVI session in multiple execution threads.  You can use functions na65cs3_LockSession and na65cs3_UnlockSession to protect sections of code that require exclusive access to the resource.

    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    n    You can use this control to set the initial value of certain attributes for the session.  The following table lists the attributes and the name you use in this parameter to identify the attribute.

Name              Attribute Defined Constant   
--------------------------------------------
RangeCheck        NA65CS3_ATTR_RANGE_CHECK
QueryInstrStatus  NA65CS3_ATTR_QUERY_INSTRUMENT_STATUS   
Cache             NA65CS3_ATTR_CACHE   
Simulate          NA65CS3_ATTR_SIMULATE  
RecordCoercions   NA65CS3_ATTR_RECORD_COERCIONS

The format of this string is, "AttributeName=Value" where AttributeName is the name of the attribute and Value is the value to which the attribute will be set.  To set multiple attributes, separate their assignments with a comma.  

If you pass NULL or an empty string for this parameter, the session uses the default values for the attributes.   You can override the default values by assigning a value explicitly in a string you pass for this parameter.  You do not have to specify all of the attributes and may leave any of them out.  If you do not specify one of the attributes, its default value will be used.  

The default values for the attributes are shown below:

    Attribute Name     Default Value
    ----------------   -------------
    RangeCheck         VI_TRUE
    QueryInstrStatus   VI_FALSE
    Cache              VI_TRUE
    Simulate           VI_FALSE
    RecordCoercions    VI_FALSE
    

The following are the valid values for ViBoolean attributes:

    True:     1, True, or VI_TRUE
    False:    0, False, or VI_FALSE


Default Value:
       "Simulate=0,RangeCheck=1,QueryInstrStatus=0,Cache=1"
    *? =   ?  ?    resourceName                      2? : ?       IDQuery                           4? =? ?       reset                             5}  ?  ?    instrumentHandle                  89#????  ?    Status                            ?? ?  ? ?    optionString                       "VXI::14::INSTR"   Yes VI_TRUE No VI_FALSE   Yes VI_TRUE No VI_FALSE    	           	           5"Simulate=0,RangeCheck=1,QueryInstrStatus=0,Cache=1"       This function finds all 65CS3 in the sytem and stores the resource name, slot, logic address and model code in a na65CS3Found structure. Before calling this function you must allocate an array of these structures as large as the possible number of 65CS3s expected in your system.
       ?    Pass a pointer to an array of NA65CS3Found structures that is allocated with the possible number of 65CS3s expected in your system. The function will populate the structure with information about the NA65CS3 devices that are found in your system.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
     y    Pass a pointer to a ViInt16 variable. The function return the number of NA65CS3 devices that are found in your system.
    I: C ?  ?  ?    na65CS3inSystem                   J;????  ?    Status                            Q? ? ? ?  ?    cnt                                        NULL    	           
        0    ?    This function provides information about the maximum number of  SD, Reference Generator, Instrument grade and Operation grade   DS channels supported by the Model.                                 ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

  ???? / ?  ?  ?    modelName                       ???? p c ?  ?    maxSDChan                       ???? p. ?  ?    maxRefChan                      ???? ? c ?  ?    maxDSInstChan                   ???? ?8 ?  ?    maxDSOperChan                     S?$????  ?    Status                             0    	            	            	            	            	           F    This function provides a list of supported part number designations.     y    Pass a pointer to a ViInt16 variable. The function return the number of NA65CS3 devices that are found in your system.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
  ???? % ?  ?  ?    modelName                       ???? ?^    ?    supportedParts                    ]: ? i ?  ?    partCnt                           ]?$????  ?    Status                          ???? a f ?  ?    maxPartCnt                      ???? h[ ?  ?    maxPartLength                      0    	            
        0    	           0    0   ?    This function will return the specifications for the number of measurement (S/D) channels, the number of instrument-grade stimulus (D/S) channels, the number of operational-grade stimulus (D/S) channels, the number of reference channels, the
frequency range for the reference channels, the reference output for Reference 1 and Reference 2, and the supported language interface for a given part specification.
     ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

  ????    ?  ?    partSpecification               ????  ? ?  P    numSDChan                       ???? = ?  P    numDSInstChan                   ???? ? ?  P    numDSOperChan                   ???? [  ?  P    numRefChan                      ???? [ ? ?  ?    minRefFreq                      ???? [# ?  ?    maxRefFreq                      ???? ?  ?  ?    ref1PowerOutput                 ???? ?  
?  ?    ref2PowerOutput                   h`'????  ?    Status                          ????(  ?  ?    langInterface                   ???? ? ? ?  ?    ref1MinVolt                     ???? ?? 	?  ?    ref1MaxVolt                     ???? ? ? ?  ?    ref2MinVolt                     ???? ?? ?  ?    ref2MaxVolt                        0    	            	            	            	            	           	           	           	           	           	            	           	           	           	           ?    This function will query the instrument using the *LANG? command to determine the language support. Possible return values are
"Native", "SCPI", or "Mate CILL".  By default the language supported is "Native".     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    t?0   ?  ?    vi                              ???? g ? ?  ?    Language                          uI%????  ?    Status                                 	            	           ?    This function will query the instrument using the *IDN? command for instrument identification. Return the value in the following format: "north atlantic,<part number>,<serial #>,<firmware revision>     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ~b%    ?  ?    vi                                %????  ?    Status                          ???? l ? ?  ?    Identification                         	           	            L    This functions sets the measurement (SD) channel's configurable settings.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.


     S    Pass the channel signal format mode. Valid values are:

0 = RESOLVER
1 = SYNCHRO
     ?    Pass the channel bandwidth mode for which you want the channel to operate with. Valid values are:

0 = HIGH bandwidth for 100 Hz Bandwidth
1 = LOW bandwidth for 10 Hz Bandwidth

Use LOW bandwidth for carrier (reference) frequency < 300 Hz.
     ?    Pass the channel reference source. Valid values are:

0 = Internal
1 = External

Note, the internal reference source for Channels 3 and 4 is Reference 2.
    ?    Pass the channel DC output scale for which you want to set for the channel's DC output voltage. The full scale voltage output is +/- 10 volts. The valid range for the DC output scale is 100 to 1000.

The value entered for the DC output scale determines the DC output voltage. For example:
 DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts     ?    Pass the maximum wait time in seconds for the settled API (SD) reading. The valid range for the maximum angle settle time is 0 to 20.
     ?    This function sets the channel 2-speed/Multi-speed ratio for EVEN channels only. The ratio cannot be set for ODD channels and will be ignored. Valid ratio range is 1 to 255.  
     T    Pass the channel I/O isolation relay state. Valid values are:

0 = OPEN
1 = CLOSE
     G    Pass the channel update mode. Valid values are:

0 = LATCH
1 = TRACK
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ??   ?  ?    vi                                ?n / * ?      Channel                           ? / ? ? P    Signal Mode                       ?f 2 ? F    Bandwidth                         ?` /? ? U    Ref Source Mode                   ? w ? ?      DC Scale                          ?? ?? 	?      Max Wait Time (Sec)               ?> z ?      Ratio                             ?? ? ? ? U    Relay IO State                    ?T ? ? U    Update Mode                       ??"????  ?    Status                                   ?                            RESOLVER 0 SYNCHRO 1               HIGH 0 LOW 1               INT 0 EXT 1      ????   ?                                    ?                           OPEN 0 CLOSE 1               LATCH 0 TRACK 1    	          O    This function sets the measurement channel DC output scale. Full scale is 10 Volts. Range: 100 <= DC Scale <= 1000.

The value entered for the DC output scale determines the DC output voltage. For example:
 DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None    ?    Pass the channel DC output scale for which you want to set for the channel's DC output voltage. The full scale voltage output is +/- 10 volts. The valid range for the DC output scale is 100 to 1000.

The value entered for the DC output scale determines the DC output voltage. For example:
 DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ??   ?  ?    vi                                ?? ? ? ?      DC Scale                          ?5 ; ? ?      Channel                           ??????  ?    Status                                   ?   d  ?                               	           ?    This function sets bandwidth for the measurement channel. High Bandwidth for 100 Hz or Low Bandwidth for 10 Hz.  Note, use Low Bandwidth for carrier (reference) frequency < 300 Hz.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
        Pass the channel bandwidth mode for which you want the channel to operate with. Valid values are:

NA65CS3_HIGH_BW = 0   (HIGH bandwidth for 100 Hz Bandwidth)
NA65CS3_LOW_BW  = 1   (LOW bandwidth for 10 Hz Bandwidth)

Use LOW bandwidth for carrier (reference) frequency < 300 Hz.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ?/   ?  ?    vi                                ?? > ? ?      Channel                           ?? ? ? ? F    Bandwidth                         ??????  ?    Status                                                                (HIGH NA65CS3_HIGH_BW LOW NA65CS3_LOW_BW    	           ?    This function sets the maximum wait time in seconds to wait for the API (S/D) channel to settle before reading the measurement. Allowable range is 0 to 20.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the maximum wait time in seconds for the settled API (SD) reading. The valid range for the maximum angle settle time is 0 to 20.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ?
   ?  ?    vi                                ?? : ? ?      Channel                           ?\ ? ? ?      Max Wait Time (Sec)               ??#????  ?    Status                                                                           	           _    This function sets the measurement channel signal format mode to either Resolver or Synchro.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel signal format mode. Valid values are:

NA65CS3_RESOLVER = 0   (RESOLVER Signal Format Mode)
NA65CS3_SYNCHRO  = 1   (SYNCHRO Signal Format Mode)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ??   ?  ?    vi                                ?? 7 ? ?      channel                           ?F ? ? ? P    mode                              ??!????  ?    Status                                                                2RESOLVER NA65CS3_RESOLVER SYNCHRO NA65CS3_SYNCHRO    	           ?    This function sets the measurement channel 2-speed/Multi-speed ratio for EVEN channels only. The ratio cannot be set for ODD channels and will be ignored. Valid ratio range is 1 to 255.  

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     Y    Pass the channel 2-speed/multi-speed ratio. The valid range for the ratio is 1 to 255.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    Ƀ   ?  ?    vi                                ?: D ? ?      Channel                           ?? ? ? ?      Ratio                             ?6????  ?    Status                                                        ?                 	           ?    This function sets the measurement channel's reference source to either Internal or External. Note, Internal Reference Source for Channels 3 and 4 is Reference 2.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel reference source. Valid values are:

NA65CS3_INT = 0   (INTERNAL Reference Source)
NA65CS3_EXT = 1   (EXTERNAL Reference Source)

Note, the internal reference source for Channels 3 and 4 is Reference 2.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ԃ%   ?  ?    vi                                ?: > ? ?      Channel                           ?? ? ? ? U    Ref Source Mode                   ֻ%????  ?    Status                                                                 INT NA65CS3_INT EXT NA65CS3_EXT    	           a    This function sets the measurement channel's I/O isolation relay state to either Open or Close.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel I/O isolation relay state. Valid values are:

NA65CS3_OPEN  = 0  (OPEN relay state)
NA65CS3_CLOSE = 1  (CLOSE relay state)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ??   ?  ?    vi                                ?? > ? ?      Channel                           ?/ ? ? ? U    Relay IO State                    ??#????  ?    Status                                                               &OPEN NA65CS3_OPEN CLOSE NA65CS3_CLOSE    	           T    This function sets the measurement channel's update mode to either Latch or Track.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel update mode. Valid values are:

NA65CS3_LATCH = 0  (LATCH Update Mode)
NA65CS3_TRACK = 1  (TRACK Update Mode)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    ??   ?  ?    vi                                ?? : ? ?      Channel                           ?2 ? ? ? U    Update Mode                       ??!????  ?    Status                                                                (LATCH NA65CS3_LATCH TRACK NA65CS3_TRACK    	           \    This functions sets the stimulus (DS) channel's configurable settings for angle rotation.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
     z    Pass the channel rotation rate in revolutions per second (RPS).  The valid range for the rotation rate is 0.15 to 13.60.     ?    Pass the channel rotation stop angle in degrees. The valid range for the rotation stop angle is -359.9999 to 359.9999 degrees.
     ?    Pass the channel rotation mode. Valid values are:

NA65CS3_CONT = 0   (CONTINUOUS Rotation Mode)
NA65CS3_STEP = 1   (STEP Rotation Mode)
    ??/   ?  ?    vi                                ??????  ?    Status                            ?& > % ?      Channel                           ?? > ? ? d    Channel Grade                     ?l ? ? ?      Rotation Rate                     ?? ? ?      Rotation Stop Angle               w ?? ? U    Rotation Mode                          	                                          >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL ??      @+333333??333333??333333          ??      @v??$tT?v??$tT                                $CONT NA65CS3_CONT STEP NA65CS3_STEP    ?    This function sets the stimulus channel output angle in degrees. The range for the output angle is -359.9999 to 359.9999 degrees.     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     s    Pass the channel output angle in degrees. The valid range for the output angle is -359.9999 to 359.9999 degrees.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
    > ? ?      Channel                          ?   ?  ?    vi                               W ? ? ?      Angle                            ? ? ? ? d    Channel Grade                    }%????  ?    Status                                                  ??      @v??$tT?v??$tT                                >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	          O    This function sets the stimulus channel's DC output scale. Full scale is 10 Volts. Range: 100 <= DC Scale <= 1000.

The value entered for the DC output scale determines the DC output voltage. For example:
 DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
    ?    Pass the channel DC output scale for which you want to set for the channel's DC output voltage. The full scale voltage output is +/- 10 volts. The valid range for the DC output scale is 100 to 1000.

The value entered for the DC output scale determines the DC output voltage. For example:
 DC output scale value = 100 sets +/-100 deg/sec = +/-10 volts
 DC output scale value = 1000 sets +/-1000 deg/sec = +/-10 volts     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
      ?  ?    vi                               ? B ? ?      Channel                          ] ? ? ?      DC Scale                          ? ? ? d    Channel Grade                    ?????  ?    Status                                                       ?   d  ?                      >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           \    This function sets the stimulus channel signal format mode to either Resolver or Synchro.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel signal format mode. Valid values are:

NA65CS3_RESOLVER = 0   (RESOLVER Signal Format Mode)
NA65CS3_SYNCHRO  = 1   (SYNCHRO Signal Format Mode)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   9   ?  ?    vi                               ? > ? ?      Channel                          ? ? ? ? P    Signal Mode                       6 ? ? ? d    Channel Grade                     ?????  ?    Status                                                                2RESOLVER NA65CS3_RESOLVER SYNCHRO NA65CS3_SYNCHRO               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ?    This function sets the stimulus channel 2-speed/Multi-speed ratio for EVEN channels only. The ratio cannot be set for ODD channels and will be ignored. Valid ratio range is 1 to 255.  
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     Y    Pass the channel 2-speed/multi-speed ratio. The valid range for the ratio is 1 to 255.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   *?   ?  ?    vi                               +? > ? ?      Channel                          ,C ? ? ?      Ratio                            ,? ? ? ? d    Channel Grade                    -O????  ?    Status                                                        ?                            >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ?    This function sets the stimulus channel reference source to either Internal or External. Note, Internal Reference Source for Channels 1 and 2 is Reference 1 and Internal Reference Source for Channels 3 and 4 is Reference 2.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel reference source. Valid values are:

NA65CS3_INT = 0   (INTERNAL Reference Source)
NA65CS3_EXT = 1   (EXTERNAL Reference Source)

Note, the internal reference source for Channels 3 and 4 is Reference 2.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   7[   ?  ?    vi                               8 ? ? ?      Channel                          8? ? ? ? U    Ref Source Mode                  9? ? ? ? d    Channel Grade                    :>????  ?    Status                                                                 INT NA65CS3_INT EXT NA65CS3_EXT               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ]    This function sets the stimulus channel I/O isolation relay state to either Open or Close.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel I/O isolation relay state. Valid values are:

NA65CS3_OPEN  = 0  (OPEN relay state)
NA65CS3_CLOSE = 1  (CLOSE relay state)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   C?   ?  ?    vi                               D? > ? ?      Channel                          E0 ? ? ? U    Relay IO State                   E? ? ? ? d    Channel Grade                    Fq????  ?    Status                                                               &OPEN NA65CS3_OPEN CLOSE NA65CS3_CLOSE               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ?    This function sets the stimulus channel line-to-line voltage in volts. The valid range for the line-to-line voltage is 1 to 90 volts.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     q    Pass the channel line-to-line voltage in Volts. The valid range for the line-to-line voltage is 1 to 90 volts.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   PB   ?  ?    vi                               P? D ? ?      Channel                          Q? ? ? ?      LineToLine_v                     R ? ? ? d    Channel Grade                    R?????  ?    Status                                                  ??      @V?     ??      ??                              >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ?    This function sets the stimulus channel input reference voltage in Volts. This does not apply when the channel reference source is internal and will be ignored. The valid range for the input reference voltage is 2 to 115 volts.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel input reference voltage in Volts. This does not apply when the channel reference source is internal and will be ignored. The valid range for the input reference voltage is 2 to 115 volts.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   \?   ?  ?    vi                               ]? < ? ?      Channel                          ^. ? ? ?      InputRefVoltage_v                _ ? ? ? d    Channel Grade                    _?????  ?    Status                                                  ??      @\?     @       @                               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ?    This function sets the stimulus channel rotation rate in revolutions per second (RPS). The valid range for the rotation rate is 0.15 to 13.60 and 0 to stop rotation.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     z    Pass the channel rotation rate in revolutions per second (RPS).  The valid range for the rotation rate is 0.15 to 13.60.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   i?   ?  ?    vi                               jL > ? ?      Channel                          j? ? ? ?      Rotation Rate                    ki ? ? ? d    Channel Grade                    l????  ?    Status                                                  ??      @+333333??333333??333333                        >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           ?    This function sets the stimulus channel rotation stop angle in degrees. The valid range for the rotation stop angle is -359.9999 to 359.9999 degrees.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.

     ?    Pass the channel rotation stop angle in degrees. The valid range for the rotation stop angle is -359.9999 to 359.9999 degrees.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   u?   ?  ?    vi                               v? > ? ?      Channel                          w= ? ? ?      Rotation Stop Angle              w? ? ? ? d    Channel Grade                    xq????  ?    Status                                                  ??      @v??$tT?v??$tT                                >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           U    This function sets the stimulus channel rotation mode to either Continuous or Step.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel rotation mode. Valid values are:

NA65CS3_CONT = 0   (CONTINUOUS Rotation Mode)
NA65CS3_STEP = 1   (STEP Rotation Mode)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   ?   ?  ?    vi                               ?? > ? ?      Channel                          ?W ? ? ? U    Rotation Mode                    ?? ? ? ? d    Channel Grade                    ??????  ?    Status                                                                $CONT NA65CS3_CONT STEP NA65CS3_STEP               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    	           c    This function sets the stimulus channel trigger source to Bus, Internal, External, or TTL Level.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel trigger source. Valid values are:

NA65CS3_INT = 0   (INTERNAL Trigger Source)
NA65CS3_EXT = 1   (EXTERNAL Trigger Source)
NA65CS3_BUS = 2   (BUS Trigger Source)
NA65CS3_TTL = 3   (TTL Level Trigger Source)

     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ??   ?  ?    vi                               ??????  ?    Status                           ?? B ? ?      Channel                          ?  ? ? ? P    Trigger Source                   ? ? ? ? d    Channel Grade                          	                                          @INT NA65CS3_INT EXT NA65CS3_EXT BUS NA65CS3_BUS TTL NA65CS3_TTL               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    X    This function sets the stimulus channel trigger slope to either Negative or Positive.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel trigger slope. Valid values are:

NA65CS3_NEG = 0   (NEGATIVE going level)
NA65CS3_POS = 1   (POSITIVE going level)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ??   ?  ?    vi                              ????????  ?    Status                           ?? @ ? ?      Channel                          ?4 ? ? ? P    Trigger Slope                    ?? ? ? ? d    Channel Grade                          	                                          NEG 0 POS 1               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function sets the stimulus channel phase shift in degrees. The valid range for the phase shift is -179.9 to 179.9 degrees.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to configure. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     l    Pass the channel phase shift in degrees. The valid range for the phase shift is -179.9 to 179.9 degrees.

     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ??   ?  ?    vi                              ????????  ?    Status                           ?F = ? ?      Channel                          ?? ? ? ?      Phase Shift                      ?U ? ? ? d    Channel Grade                          	                             ??      @f|??????f|?????                                >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    O    This functions sets the reference generator channel's configurable settings.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to configure the instrument. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
         Pass the reference generator frequency in hertz. The valid range for the frequency is based on the Part Number Designation.

     w    Pass the reference generator voltage in volts. The valid range for the reference generator voltage is 2 to 115 volts.     ?    Pass the channel I/O isolation relay state. Valid values are:

NA65CS3_OPEN  = 0  (OPEN relay state)
NA65CS3_CLOSE = 1  (CLOSE relay state)
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors
   ??   ?  ?    vi                               ?? > | ?      Channel                          ?t > ?      Frequency_hz                     ?? ? ?      Voltage_v                        ?z ? ? U    Relay IO State                   ?%????  ?    Status                                                  ??      @ӈ     @G?     @v?               ??      @\?     @       @'??????                       &OPEN NA65CS3_OPEN CLOSE NA65CS3_CLOSE    	          I    This function sets the reference generator channel frequency in hertz. The valid range for the frequency is based on the Part Number Designation.

Part Number Frequency Range Designators:
A = 360 Hz to 2 KHz
B = 360 Hz to 4 KHz
C = 47 Hz to 2 KHz
D = 47 Hz to 4 KHz
E = 47 Hz to 10 KHz
F = 47 Hz to 20 KHz
G = 47 Hz to 5 KHz


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to configure the instrument. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
         Pass the reference generator frequency in hertz. The valid range for the frequency is based on the Part Number Designation.

   ??0   ?  ?    vi                              ????&????  ?    Status                           ?? ? ? ?      Channel                          ?U ? ? ?      Frequency (Hz)                         	                             ??      @ӈ     @G?     @v?                  ?    This function sets the reference generator channel voltage in volts. The valid range for the reference generator voltage is 2 to 115 volts.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to configure the instrument. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
     w    Pass the reference generator voltage in volts. The valid range for the reference generator voltage is 2 to 115 volts.   ??%   ?  ?    vi                              ????%????  ?    Status                           ?N > ? ?      Channel                          ? ? ? ?      Voltage_v                              	                             ??      @\?     @       @'??????             h    This function sets the reference generator channel I/O isolation relay state to either Open or Close.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to configure the instrument. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
     ?    Pass the channel I/O isolation relay state. Valid values are:

NA65CS3_OPEN  = 0  (OPEN relay state)
NA65CS3_CLOSE = 1  (CLOSE relay state)
   ?!   ?  ?    vi                              ????????  ?    Status                           ?? A ? ?      Channel                          ?? ? ? ? U    Relay IO State                         	                                          &OPEN NA65CS3_OPEN CLOSE NA65CS3_CLOSE   ?    This function sets the value of a ViInt32 attribute.

This is a low-level function that you can use to set the values of instrument-specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid or is different than the value you specify. 

This instrument driver contains high-level functions that set most of the instrument attributes.  It is best to use the high-level driver functions as much as possible.  They handle order dependencies and multithread locking for you.  In addition, they perform status checking only after setting all of the attributes.  In contrast, when you set multiple attributes using the SetAttribute functions, the functions check the instrument status after each call.

Also, when state caching is enabled, the high-level functions that configure multiple attributes perform instrument I/O only for the attributes whose value you change.  Thus, you can safely call the high-level functions without the penalty of redundant instrument I/O.


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

        Pass the value to which you want to set the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none    C    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViInt32 type.   
  If you choose to see all IVI attributes, the data types appear
  to the right of the attribute names in the list box. 
  Attributes with data types other than ViInt32 are dim.  If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to set the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
   ?/-   ?  ?    Instrument Handle                ??#????  ?    Status                           ?w ? ? ?  ?    Attribute Value                 ???? ? ???                                          ˒ = ? ? ?    Attribute ID                     ?? =  ?  ?    Channel Name                           	               .Press <ENTER> for a list of 
value constants.                0    ""   ?    This function sets the value of a ViReal64 attribute.

This is a low-level function that you can use to set the values of instrument-specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid or is different than the value you specify. 

This instrument driver contains high-level functions that set most of the instrument attributes.  It is best to use the high-level driver functions as much as possible.  They handle order dependencies and multithread locking for you.  In addition, they perform status checking only after setting all of the attributes.  In contrast, when you set multiple attributes using the SetAttribute functions, the functions check the instrument status after each call.

Also, when state caching is enabled, the high-level functions that configure multiple attributes perform instrument I/O only for the attributes whose value you change.  Thus, you can safely call the high-level functions without the penalty of redundant instrument I/O.


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

        Pass the value to which you want to set the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none    B    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViReal64
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box.
  Attributes with data types other than ViReal64 are dim.  If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to set the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
   ?P-   ?  ?    Instrument Handle                ?#????  ?    Status                           ?? ? ? ?  ?    Attribute Value                 ???? ? ???                                          ?? = ? ? ?    Attribute ID                     ?? =  ?  ?    Channel Name                           	               .Press <ENTER> for a list of 
value constants.                0    ""   ?    This function sets the value of a ViString attribute.

This is a low-level function that you can use to set the values of instrument-specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid or is different than the value you specify. 

This instrument driver contains high-level functions that set most of the instrument attributes.  It is best to use the high-level driver functions as much as possible.  They handle order dependencies and multithread locking for you.  In addition, they perform status checking only after setting all of the attributes.  In contrast, when you set multiple attributes using the SetAttribute functions, the functions check the instrument status after each call.

Also, when state caching is enabled, the high-level functions that configure multiple attributes perform instrument I/O only for the attributes whose value you change.  Thus, you can safely call the high-level functions without the penalty of redundant instrument I/O.


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

        Pass the value to which you want to set the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none    A    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViString
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box.
  Attributes with data types other than ViString are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to set the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
   ?p-   ?  ?    Instrument Handle                ?(#????  ?    Status                           ?? ? ? ?  ?    Attribute Value                 ???? ? ???                                          ?? = ? ? ?    Attribute ID                     ? =  ?  ?    Channel Name                           	               .Press <ENTER> for a list of 
value constants.                0    ""   ?    This function sets the value of a ViBoolean attribute.

This is a low-level function that you can use to set the values of instrument-specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid or is different than the value you specify. 

This instrument driver contains high-level functions that set most of the instrument attributes.  It is best to use the high-level driver functions as much as possible.  They handle order dependencies and multithread locking for you.  In addition, they perform status checking only after setting all of the attributes.  In contrast, when you set multiple attributes using the SetAttribute functions, the functions check the instrument status after each call.

Also, when state caching is enabled, the high-level functions that configure multiple attributes perform instrument I/O only for the attributes whose value you change.  Thus, you can safely call the high-level functions without the penalty of redundant instrument I/O.


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

        Pass the value to which you want to set the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none    C    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViBoolean
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box.
  Attributes with data types other than ViBoolean are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to set the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
   ?-   ?  ?    Instrument Handle                H#????  ?    Status                           ? ? ? ?  ?    Attribute Value                 ???? ? ???                                          ? = ? ? ?    Attribute ID                     > =  ?  ?    Channel Name                           	               .Press <ENTER> for a list of 
value constants.                0    ""   ?    This function sets the value of a ViSession attribute.

This is a low-level function that you can use to set the values of instrument-specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid or is different than the value you specify. 

This instrument driver contains high-level functions that set most of the instrument attributes.  It is best to use the high-level driver functions as much as possible.  They handle order dependencies and multithread locking for you.  In addition, they perform status checking only after setting all of the attributes.  In contrast, when you set multiple attributes using the SetAttribute functions, the functions check the instrument status after each call.

Also, when state caching is enabled, the high-level functions that configure multiple attributes perform instrument I/O only for the attributes whose value you change.  Thus, you can safely call the high-level functions without the penalty of redundant instrument I/O.


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

        Pass the value to which you want to set the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to set the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    C    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViSession
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box.
  Attributes with data types other than ViSession are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   ?-   ?  ?    Instrument Handle                j#????  ?    Status                           %? ? ? ?  ?    Attribute Value                  ( =  ?  ?    Channel Name                     ) = ? ? ?    Attribute ID                    ???? ? ???                                                	               ""                0    .Press <ENTER> for a list of 
value constants.   ?    This function queries the value of a ViInt32 attribute.

You can use this function to get the values of instrument- specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid. 

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    ?    Returns the current value of the attribute.  Pass the address of a ViInt32 variable.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has named constants as valid values, you can view a
  list of the constants by pressing <ENTER> on this control.  
  Select a value by double-clicking on it or by selecting it and 
  then pressing <ENTER>.  
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to obtain the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    ?    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Help text is
  shown for each attribute.  Select an attribute by 
  double-clicking on it or by selecting it and then pressing
  <ENTER>.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViInt32 type. 
  If you choose to see all IVI attributes, the data types appear
  to the right of the attribute names in the list box.  
  Attributes with data types other than ViInt32 are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   1?-   ?  ?    Instrument Handle                2t#????  ?    Status                           : ? ? ?  ?    Attribute Value                  ;? =  ?  ?    Channel Name                     <? = ? ? ?    Attribute ID                           	           	            ""                0   ?    This function queries the value of a ViReal64 attribute.

You can use this function to get the values of instrument- specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid. 

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    ?    Returns the current value of the attribute.  Pass the address of a ViReal64 variable.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has named constants as valid values, you can view a
  list of the constants by pressing <ENTER> on this control.  
  Select a value by double-clicking on it or by selecting it and 
  then pressing <ENTER>.  
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to obtain the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    ?    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Help text is
  shown for each attribute.  Select an attribute by 
  double-clicking on it or by selecting it and then pressing
  <ENTER>.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViReal64
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViReal64 are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   Dd-   ?  ?    Instrument Handle                E#????  ?    Status                           L? ? ? ?  ?    Attribute Value                  Nk =  ?  ?    Channel Name                     Or = ? ? ?    Attribute ID                           	           	           ""                0   4    This function queries the value of a ViString attribute.

You can use this function to get the values of instrument- specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid. 

You must provide a ViChar array to serve as a buffer for the value.  You pass the number of bytes in the buffer as the Buffer Size parameter.  If the current value of the attribute, including the terminating NUL byte, is larger than the size you indicate in the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you want to call this function just to get the required buffer size, you can pass 0 for the Buffer Size and VI_NULL for the Attribute Value buffer.  

If you want the function to fill in the buffer regardless of the   number of bytes in the value, pass a negative number for the Buffer Size parameter.  


     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    	F    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

If the current value of the return buffer, including the terminating NUL byte, is larger than the size you indicate in the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    &    The buffer in which the function returns the current value of the attribute.  The buffer must be of type ViChar and have at least as many bytes as indicated in the Buffer Size parameter.

If the current value of the attribute, including the terminating NUL byte, contains more bytes that you indicate in this parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you specify 0 for the Buffer Size parameter, you can pass VI_NULL for this parameter.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has named constants as valid values, you can view a
  list of the constants by pressing <ENTER> on this control.  
  Select a value by double-clicking on it or by selecting it and 
  then pressing <ENTER>.  
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to obtain the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    ?    Pass the number of bytes in the ViChar array you specify for the Attribute Value parameter.  

If the current value of the attribute, including the terminating NUL byte, contains more bytes that you indicate in this parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you pass a negative number, the function copies the value to the buffer regardless of the number of bytes in the value.

If you pass 0, you can pass VI_NULL for the Attribute Value buffer parameter.
    ?    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Help text is
  shown for each attribute.  Select an attribute by 
  double-clicking on it or by selecting it and then pressing
  <ENTER>.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViString
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViString are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   Z?-   ?  ?    Instrument Handle                [D#????  ?    Status or Required Size          d? ? L ? ?    Attribute Value                  h? =  ?  ?    Channel Name                     i? =? ?  ?    Buffer Size                      l? = ? ? ?    Attribute ID                           	           	            ""    512                0   ?    This function queries the value of a ViBoolean attribute.

You can use this function to get the values of instrument- specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid. 

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    ?    Returns the current value of the attribute.  Pass the address of a ViBoolean variable.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has named constants as valid values, you can view a
  list of the constants by pressing <ENTER> on this control.  
  Select a value by double-clicking on it or by selecting it and 
  then pressing <ENTER>.  
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to obtain the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    ?    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Help text is
  shown for each attribute.  Select an attribute by 
  double-clicking on it or by selecting it and then pressing
  <ENTER>.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViBoolean
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViBoolean are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   t?-   ?  ?    Instrument Handle                u;#????  ?    Status                           |? ? ? ?  ?    Attribute Value                  ~? =  ?  ?    Channel Name                     ? = ? ? ?    Attribute ID                           	           	            ""                0   ?    This function queries the value of a ViSession attribute.

You can use this function to get the values of instrument- specific attributes and inherent IVI attributes.  If the attribute represents an instrument state, this function performs instrument I/O in the following cases:

- State caching is disabled for the entire session or for the particular attribute.

- State caching is enabled and the currently cached value is invalid. 

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    ?    Returns the current value of the attribute.  Pass the address of a ViSession variable.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has named constants as valid values, you can view a
  list of the constants by pressing <ENTER> on this control.  
  Select a value by double-clicking on it or by selecting it and 
  then pressing <ENTER>.  
     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to obtain the value of the attribute. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    ?    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Help text is
  shown for each attribute.  Select an attribute by 
  double-clicking on it or by selecting it and then pressing
  <ENTER>.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViSession
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViSession are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   ?1-   ?  ?    Instrument Handle                ??#????  ?    Status                           ?y ? ? ?  ?    Attribute Value                  ?9 =  ?  ?    Channel Name                     ?@ = ? ? ?    Attribute ID                           	           	            ""                0    S    This function checks the validity of a value you specify for a ViInt32 attribute.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    (    Pass the value which you want to verify as a valid value for the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to check the attribute value. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    @    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViInt32 type. 
  If you choose to see all IVI attributes, the data types appear
  to the right of the attribute names in the list box. 
  Attributes with data types other than ViInt32 are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   ?{-   ?  ?    Instrument Handle                ?3#????  ?    Status                           ?? ? ? ?  ?    Attribute Value                  ?? =  ?  ?    Channel Name                    ???? ? ???                                          ?? = ? ? ?    Attribute ID                           	               ""    .Press <ENTER> for a list of 
value constants.                0    T    This function checks the validity of a value you specify for a ViReal64 attribute.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    (    Pass the value which you want to verify as a valid value for the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to check the attribute value. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    B    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViReal64
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViReal64 are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   ?1-   ?  ?    Instrument Handle                ??#????  ?    Status                           ?y ? ? ?  ?    Attribute Value                  ?? =  ?  ?    Channel Name                    ???? ? ???                                          ?? = ? ? ?    Attribute ID                           	               ""    .Press <ENTER> for a list of 
value constants.                0    T    This function checks the validity of a value you specify for a ViString attribute.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    (    Pass the value which you want to verify as a valid value for the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to check the attribute value. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    B    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViString
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViString are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   ??-   ?  ?    Instrument Handle                ??#????  ?    Status                           ?1 ? ? ?  ?    Attribute Value                  ?a =  ?  ?    Channel Name                    ???? ? ???                                          ?` = ? ? ?    Attribute ID                           	               ""    .Press <ENTER> for a list of 
value constants.                0    U    This function checks the validity of a value you specify for a ViBoolean attribute.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    (    Pass the value which you want to verify as a valid value for the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to check the attribute value. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    D    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViBoolean
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViBoolean are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   Т-   ?  ?    Instrument Handle                ?Z#????  ?    Status                           ?? ? ? ?  ?    Attribute Value                  ? =  ?  ?    Channel Name                    ???? ? ???                                          ? = ? ? ?    Attribute ID                           	               ""    .Press <ENTER> for a list of 
value constants.                0    U    This function checks the validity of a value you specify for a ViSession attribute.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

    (    Pass the value which you want to verify as a valid value for the attribute.

From the function panel window, you can use this control as follows.

- If the attribute currently showing in the Attribute ID ring
  control has constants as valid values, you can view a list of
  the constants by pressing <ENTER> on this control.  Select a
  value by double-clicking on it or by selecting it and then
  pressing <ENTER>.  

  Note:  Some of the values might not be valid depending on the
  current settings of the instrument session.

Default Value: none     ?    If the attribute is channel-based, this parameter specifies the name of the channel on which to check the attribute value. If the attribute is not channel-based, then pass VI_NULL or an empty string.

Valid Channel Names:  1

Default Value:  ""
    D    Pass the ID of an attribute.

From the function panel window, you can use this control as follows.

- Click on the control or press <ENTER>, <spacebar>, or
  <ctrl-down arrow>, to display a dialog box containing a
  hierarchical list of the available attributes.  Attributes 
  whose value cannot be set are dim.  Help text is shown for 
  each attribute.  Select an attribute by double-clicking on it  
  or by selecting it and then pressing <ENTER>.

  Read-only attributes appear dim in the list box.  If you 
  select a read-only attribute, an error message appears.

  A ring control at the top of the dialog box allows you to see 
  all IVI attributes or only the attributes of the ViSession
  type.  If you choose to see all IVI attributes, the data types
  appear to the right of the attribute names in the list box. 
  Attributes with data types other than ViSession are dim. If
  you select an attribute data type that is dim, LabWindows/CVI
  transfers you to the function panel for the corresponding
  function that is consistent with the data type.

- If you want to enter a variable name, press <CTRL-T> to change
  this ring control to a manual input box.

- If the attribute in this ring control has named constants as  
  valid values, you can view the constants by moving to the 
  Attribute Value control and pressing <ENTER>.
   ?]-   ?  ?    Instrument Handle                ?#????  ?    Status                           ?? ? ? ?  ?    Attribute Value                  ?? =  ?  ?    Channel Name                    ???? ? ???                                          ?? = ? ? ?    Attribute ID                           	               ""    .Press <ENTER> for a list of 
value constants.                0    =    This function initiates the rotation of the channel output.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the stimulus channel number to initiate rotation. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ?    ?  ?    vi                              ????????  ?    Status                           ?? C ? ?      Channel                          ?c ? ? ? d    Channel Grade                          	                                           >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the Step Rotation Status.  This only applies when in Step Rotation Mode.  This function will set the Rotation_Complete variable to VI_TRUE if the Step Rotation is Done, otherwise it will return VI_FALSE.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the stimulus channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     K    Returns VI_TRUE if the Step Rotation is Done, otherwise returns VI_FALSE.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ?B   ?  ?    vi                              ????????  ?    Status                           ?? > ? ?      Channel                          ?? ? ? ?  U    Rotation Complete                ?? ? ? ? d    Channel Grade                          	                                        VI_FALSE               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the measurement channel angle position in degrees. The range returned for the angle position is 0 to 359.999.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.     _    Returns the angle position in degrees. The range returned for angle position is 0 to 359.999.   ??   ?  ?    vi                              ????????  ?    Status                           ?m < ? ?      Channel                            ? ? ?  U    Angle                                  	                                       0.0000    ?    This function returns the measurement channel velocity in degrees/second. The range returned for the velocity is -32767 and 32767.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.     _    Returns the angle position in degrees. The range returned for angle position is 0 to 359.999.   0   ?  ?    vi                              ????????  ?    Status                           ? > ? ?      Channel                          Q ? ? ?  U    Velocity                               	                                       0.0000    t    This function returns the measurement channel DC Scale value. The range returned for the DC Scale is 100 to 1000.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     Y    Returns the channel DC scale value. The range returned for the DC scale is 100 to 1000.   A   ?  ?    vi                              ????????  ?    Status                           ? < ? ?      Channel                          ? ? ? ?  U    DC Scale                               	                                        100    ?    This function returns the measurement channel bandwidth setting.
The possible return values are:

NA65CS3_HIGH_BW = 0   (HIGH bandwidth)
NA65CS3_LOW_BW  = 1   (LOW bandwidth)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel bandwidth. Possible return values are:

NA65CS3_HIGH_BW = 0   (HIGH bandwidth)
NA65CS3_LOW_BW  = 1   (LOW bandwidth)

   ?   ?  ?    vi                              ????????  ?    Status                           	j > ? ?      Channel                          
 ? ? ?  x    Bandwidth                              	                                        NA65CS3_HIGH    ?    This function returns the measurement channel maximum angle wait time in seconds. The range returned for the maximum angle settle time is 0 to 20.

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     r    Returns the maximum angle settle time in seconds. The range returned for maximum angle settle time is 0 to 20.

   F   ?  ?    vi                              ????????  ?    Status                           ? D ? ?      Channel                          ? ? ? ?  U    Max Angle Settle Time                  	                                
        0    ?    This function returns the measurement channel signal format mode setting. The possible return values are:

NA65CS3_RESOLVER = 0   (RESOLVER Signal Format Mode)
NA65CS3_SYNCHRO  = 1   (SYNCRHO Signal Format Mode)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel signal format mode. Possible return values are:

NA65CS3_RESOLVER = 0   (RESOLVER Signal Format Mode)
NA65CS3_SYNCHRO  = 1   (SYNCHRO Signal Format Mode)
   ?   ?  ?    vi                              ????????  ?    Status                           ? > ? ?      Channel                          B ? ? ?  ?    Signal Mode                            	                                        NA65CS3_RESOLVER    ?    This function returns the measurement channel one-speed/two-speed/multi-speed ratio setting. The range returned for the ratio is 1 to 255 for EVEN channels, 1 if an ODD channel is specified.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel one-speed/two-speed/multi-speed ratio setting. The range returned for the ratio is 1 to 255 for EVEN channels, 1 if an ODD channel is specified.
   ?   ?  ?    vi                              ????????  ?    Status                           ? ; ? ?      Channel                          ( ? ? ?  U    Ratio                                  	                                
        1    ?    This function returns the measurement reference source setting.
The possible return values are:

NA65CS3_INT = 0   (INTERNAL Reference Source)
NA65CS3_EXT = 1   (EXTERNAL Reference Source)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel reference source. Possible return values are:

NA65CS3_INT = 0   (INTERNAL Reference Source)
NA65CS3_EXT = 1   (EXTERNAL Reference Source)
   ?   ?  ?    vi                              ????????  ?    Status                           ] > ? ?      Channel                          ? ? ? ?  x    Ref Source Mode                        	                                        NA65CS3_INT    ?    This function returns the measurement channel relay I/O state.
The possible return values are:

NA65CS3_OPEN  = 0   (OPENED relay status)
NA65CS3_CLOSE = 1   (CLOSED relay status)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel relay IO state. Possible return values are:

NA65CS3_OPEN  = 0   (OPENED relay status)
NA65CS3_CLOSE = 1   (CLOSED relay status)
   m   ?  ?    vi                              ????????  ?    Status                           $ > ? ?      Channel                          ? ? ? ?  ?    Relay IO State                         	                                        NA63CS3_OPEN    ?    This function returns the measurement update mode setting.
The possible return values are:

NA65CS3_LATCH  = 0   (LATCH Update Mode)
NA65CS3_TRACK  = 1   (TRACK Update Mode)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel update mode. Possible return values are:

NA65CS3_LATCH  = 0   (LATCH Update Mode)
NA65CS3_TRACK  = 1   (TRACK Update Mode)
   %   ?  ?    vi                              ????????  ?    Status                           ? ? ? ?      Channel                           s ? ? ?  ?    Update Mode                            	                                        NA65CS3_LATCH    ?    This function returns the measurement channel angle position in degrees. The range returned for the angle position is 0 to 359.999.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     `    Returns the angle position in degrees. The range returned for angle position is 0 to 359.999.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   "?   ?  ?    vi                              ????????  ?    Status                           #f D ? ?      Channel                          #? ? ? ?  d    Angle                            $e ? ? ? d    Channel Grade                          	                                       0.0000               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    s    This function returns the measurement channel DC Scale value. The range returned for the DC Scale is 100 to 1000.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     Y    Returns the channel DC scale value. The range returned for the DC scale is 100 to 1000.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   '   ?  ?    vi                              ???? ?????  ?    Status                           '? 3 ? ?      Channel                          (h ? ? ?  U    DC Scale                         (?  ? ? d    Channel Grade                          	                                        100               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the stimulus channel signal format mode setting. The possible return values are:

NA65CS3_RESOLVER = 0   (RESOLVER Signal Format Mode)
NA65CS3_SYNCHRO  = 1   (SYNCRHO Signal Format Mode)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel signal format mode. Possible return values are:

NA65CS3_RESOLVER = 0   (RESOLVER Signal Format Mode)
NA65CS3_SYNCHRO  = 1   (SYNCHRO Signal Format Mode)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   +?   ?  ?    vi                              ????????  ?    Status                           ,? - ? ?      Channel                          -) ? ? ?  ?    Signal Mode                      -? z ? ? d    Channel Grade                          	                                        NA65CS3_RESOLVER               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the stimulus channel one-speed/two-speed/multi-speed ratio setting. The range returned for the ratio is 1 to 255 for EVEN channels, 1 if an ODD channel is specified.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel one-speed/two-speed/multi-speed ratio setting. The range returned for the ratio is 1 to 255 for EVEN channels, 1 if an ODD channel is specified.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   0?   ?  ?    vi                              ????????  ?    Status                           1? 5 ? ?      Channel                          29 ? ? ?  U    Ratio                            2? x ? ? d    Channel Grade                          	                                
        1               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the stimulus reference source setting.
The possible return values are:

NA65CS3_INT = 0   (INTERNAL Reference Source)
NA65CS3_EXT = 1   (EXTERNAL Reference Source)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel reference source. Possible return values are:

NA65CS3_INT = 0   (INTERNAL Reference Source)
NA65CS3_EXT = 1   (EXTERNAL Reference Source)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   5?   ?  ?    vi                              ????????  ?    Status                           6? > ? ?      Channel                          7/ ? ? ?  x    Ref Source Mode                  7? ? ? ? d    Channel Grade                          	                                        NA65CS3_INT               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the stimulus channel relay I/O state.
The possible return values are:

NA65CS3_OPEN  = 0   (OPENED relay status)
NA65CS3_CLOSE = 1   (CLOSED relay status)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel relay IO state. Possible return values are:

NA65CS3_OPEN  = 0   (OPENED relay status)
NA65CS3_CLOSE = 1   (CLOSED relay status)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   :?   ?  ?    vi                              ????????  ?    Status                           ;? 4 ? ?      Channel                          <  ? ? ?  ?    Relay IO State                   <? } ? ? d    Channel Grade                          	                                        NA63CS3_OPEN               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the channel line-to-line voltage in Volts. The range returned for the line-to-line voltage is 1 to 90 volts.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     v    Returns the channel line-to-line voltage in Volts. The range returned for the line-to-line voltage is 1 to 90 volts.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ??    ?  ?    vi                              ????????  ?    Status                           @B 3 ? ?      Channel                          @? ? ? ?  U    Line to Line Voltage             AW l ? ? d    Channel Grade                          	                                       1.0               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL       This function returns the channel input reference voltage in Volts. This does not apply when the channel reference source is internal. The  range returned for the input reference voltage is 2 to 115 volts if the channel reference source is external, otherwise 0 volts is returned.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
        Returns the channel input reference voltage in Volts. This does not apply when the channel reference source is internal. The  range returned for the input reference voltage is 2 to 115 volts if the channel reference source is external, otherwise 0 volts is returned.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   D?   ?  ?    vi                              ????????  ?    Status                           Eg 2 ? ?      Channel                          E? ? ? ?  d    Input Ref Voltage (V)            G t ? ? d    Channel Grade                          	                                       2.00               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the stimulus channel rotation rate in revolutions per second (RPS). The range returned for the rotation rate is 0.15 to 13.60 or 0.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel rotation rate in revolutions per second (RPS).  The range returned for the rotation rate is 0.15 to 13.60.     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   I?   ?  ?    vi                              ????????  ?    Status                           J? > ?      Channel                          K; ? ?  U    Rotation Rate                    K? ? ? d    Channel Grade                          	                                       0.15               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the channel rotation stop angle in degrees. The range returned for the rotation stop angle is 0 to 359.9999 degrees.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
         Returns the channel rotation stop angle in degrees. The range returned for the rotation stop angle is 0 to 359.9999 degrees.
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   N?   ?  ?    vi                              ????????  ?    Status                           OG > ? ?      Channel                          O? ? ? ?  d    Rotation Stop Angle              Pe ? ? ? d    Channel Grade                          	                                       0.0000               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the channel rotation mode. The possible return values are:

NA65CS3_CONT = 0   (CONTINUOUS Rotation Mode)
NA65CS3_STEP = 1   (STEP Rotation Mode)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel rotation mode. Possible return values are:

NA65CS3_CONT = 0   (CONTINUOUS Rotation Mode)
NA65CS3_STEP = 1   (STEP Rotation Mode)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   SQ   ?  ?    vi                              ????????  ?    Status                           T > ? ?      Channel                          T? ? ? ?  x    Rotation Mode                    U? } ? ? d    Channel Grade                          	                                        NA65CS3_CONT               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the channel trigger source. Possible return values are:

NA65CS3_INT = 0   (INTERNAL Trigger Source)
NA65CS3_EXT = 1   (EXTERNAL Trigger Source)
NA65CS3_BUS = 2   (BUS Trigger Source)
NA65CS3_TTL = 3   (TTL Level Trigger Source)

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel trigger source. Possible return values are:

NA65CS3_INT = 0   (INTERNAL Trigger Source)
NA65CS3_EXT = 1   (EXTERNAL Trigger Source)
NA65CS3_BUS = 2   (BUS Trigger Source)
NA65CS3_TTL = 3   (TTL Level Trigger Source)

     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   X?)   ?  ?    vi                              ????????  ?    Status                           Y< > ? ?      Channel                          Y? ? ? ?  d    Trigger Source                   Z? } ? ? d    Channel Grade                          	                                        NA65CS3_BUS               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the channel trigger slope. The possible return values are:

NA65CS3_NEG = 0   (NEGATIVE going level)
NA65CS3_POS = 1   (POSITIVE going level)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     ?    Returns the channel trigger slope. Possible return values are:

NA65CS3_NEG = 0   (NEGATIVE going level)
NA65CS3_POS = 1   (POSITIVE going level)
     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   ]?   ?  ?    vi                              ????????  ?    Status                           ^o 5 ? ?      Channel                          _ ? ? ?  d    Trigger Slope                    _? s ? ? d    Channel Grade                          	                                        NA65CS3_NEG               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL    ?    This function returns the channel phase shift in degrees. The range returned for the phase shift is -179.9 to 179.9 degrees.

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 4.
     r    Returns the channel phase shift in degrees. The range returned for the phase shift is -179.9 to 179.9 degrees.

     ?    Pass the channel grade. 
Valid values are:

NA65CS3_INSTRUMENT = 0    (Instrument-grade channel)
NA65CS3_OPERATIONAL  = 1  (Operational/Converter-grade channel)
   bi   ?  ?    vi                              ????????  ?    Status                           c  2 ? ?      Channel                          c? ? ? ?  d    Phase Shift                      d1 } ? ? d    Channel Grade                          	                                       0.0               >Instrument NA65CS3_INSTRUMENT Operational NA65CS3_OPERATIONAL   H    This function returns the reference generator frequency in hertz. The range  returned for the frequency is based on the Part Number Designation.


Part Number Frequency Range Designators:
A = 360 Hz to 2 KHz
B = 360 Hz to 4 KHz
C = 47 Hz to 2 KHz
D = 47 Hz to 4 KHz
E = 47 Hz to 10 KHz
F = 47 Hz to 20 KHz
G = 47 Hz to 5 KHz

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
     ?    Returns the reference generator frequency in hertz. The valid range for the frequency is based on the Part Number Designation.

   g?   ?  ?    vi                              ????????  ?    Status                           ho > ? ?      Channel                          i ? ? ?  d    Frequency                              	                                       47    ?    This function returns the reference generator voltage in volts. The range returned for the reference generator voltage is 2 to 115 volts.     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
     }    Returns the reference generator voltage in volts. The range returned for the reference generator voltage is 2 to 115 volts.   k@   ?  ?    vi                              ????????  ?    Status                           k? 5 ? ?      Channel                          l? ? ? ?  d    Voltage                                	                                       11.8    ?    This function returns the reference generator channel relay I/O state.  The possible return values are:

NA65CS3_OPEN  = 0   (OPENED relay status)
NA65CS3_CLOSE = 1   (CLOSED relay status)
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None     ?    Pass the reference generator channel number to query. Number of Valid Channels are based on the Part Number Designation. Valid range of Channel Numbers: 1 to 3.
     ?    Returns the channel relay IO state. Possible return values are:

NA65CS3_OPEN  = 0   (OPENED relay status)
NA65CS3_CLOSE = 1   (CLOSED relay status)
   n?0   ?  ?    vi                              ????????  ?    Status                           o? > ? ?      Channel                          p[ ? ? ?  ?    Relay IO State                         	                                        NA63CS3_OPEN   &    This function resets the instrument to a known state and sends initialization commands to the instrument.  The initialization commands set instrument settings such as Headers Off, Short Command form, and Data Transfer Binary to the state necessary for the operation of the instrument driver.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   s;#????  ?    Status                           z?-   ?  ?    Instrument Handle                  	                   This function resets the instrument and applies initial user specified settings from the Logical Name which was used to initialize the session.  If the session was created without a Logical Name, this function is equivalent to the na65cs3_reset function.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   }#????  ?    Status                           ??-   ?  ?    Instrument Handle                  	               ?    This function places the instrument in a quiescent state where it has minimal or no impact on the system to which it is connected.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   ?S#????  ?    Status                           ??-   ?  ?    Instrument Handle                  	               Z    This function runs the instrument's self test routine and returns the test result(s). 

    &    This control contains the value returned from the instrument self test.  Zero means success.  For any other code, see the device's operator's manual.

Self-Test Code    Description
---------------------------------------
   0              Passed self test
   1              Self test failed

     ?    Returns the self-test response string from the instrument. See the device's operation manual for an explanation of the string's contents.

You must pass a ViChar array with at least 256 bytes.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   ?w =  ?  ?    Self Test Result                 ?? = ? ? ,    Self-Test Message                ?o#????  ?    Status                           ??-   ?  ?    Instrument Handle                  	           	            	               `    This function returns the revision numbers of the instrument driver and instrument firmware.

     ?    Returns the instrument driver software revision numbers in the form of a string.

You must pass a ViChar array with at least 256 bytes.     ?    Returns the instrument firmware revision numbers in the form of a string.

You must pass a ViChar array with at least 256 bytes.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   ? = 3 ?  ?    Instrument Driver Revision       ?? =6 ?  ?    Firmware Revision                ?7#????  ?    Status                           ??-   ?  ?    Instrument Handle                  	            	            	               V    This function reads an error code and a message from the instrument's error queue.

     B    Returns the error code read from the instrument's error queue.

     ?    Returns the error message string read from the instrument's error message queue.

You must pass a ViChar array with at least 256 bytes.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   ?? =  ?  ?    Error Code                       ?# = ? ? ,    Error Message                    ??#????  ?    Status                           ?E-   ?  ?    Instrument Handle                  	           	            	               n    This function converts a status code returned by an instrument driver function into a user-readable string.      z    Pass the Status parameter that is returned from any of the instrument driver functions.

Default Value:  0  (VI_SUCCESS)     ?    Returns the user-readable message string that corresponds to the status code you specify.

You must pass a ViChar array with at least 256 bytes.
    H    Reports the status of this operation.  

This function can return only three possible status codes:

Status    Description
-------------------------------------------------
       0  No error (the call was successful).

3FFF0085  Unknown status code (warning).

BFFF000A  Invalid parameter (Error Message buffer is VI_NULL).

        The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

You can pass VI_NULL for this parameter.  This is useful when one of the initialize functions fail.

Default Value:  VI_NULL
   ?o =  ?  h    Error Code                       ?? = ? ? ?    Error Message                    ??#????  ?    Status                           ??-   ?  ?    Instrument Handle                  0    	            	           VI_NULL    P    This function invalidates the cached values of all attributes for the session.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   ?P#????  ?    Status                           ??-   ?  ?    Instrument Handle                  	              ?    This function retrieves and then clears the IVI error information for the session or the current execution thread. One exception exists: If the BufferSize parameter is 0, the function does not clear the error information. By passing 0 for the buffer size, the caller can ascertain the buffer size required to get the entire error description string and then call the function again with a sufficiently large buffer.

If the user specifies a valid IVI session for the InstrumentHandle parameter, Get Error retrieves and then clears the error information for the session.  If the user passes VI_NULL for the InstrumentHandle parameter, this function retrieves and then clears the error information for the current execution thread.  If the InstrumentHandle parameter is an invalid session, the function does nothing and returns an error. Normally, the error information describes the first error that occurred since the user last called na65cs3_GetError or na65cs3_ClearError.
    	D    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

If the current value of the return buffer, including the terminating NUL byte, is larger than the size you indicate in the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Pass the number of bytes in the ViChar array you specify for the Description parameter.

If the error description, including the terminating NUL byte, contains more bytes than you indicate in this parameter, the function copies BufferSize - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you pass a negative number, the function copies the value to the buffer regardless of the number of bytes in the value.

If you pass 0, you can pass VI_NULL for the Description buffer parameter.

Default Value:  None     ?    Returns the error code for the session or execution thread.

If you pass 0 for the Buffer Size, you can pass VI_NULL for this parameter.
    ?    Returns the error description for the IVI session or execution thread.  If there is no description, the function returns an empty string.

The buffer must contain at least as many elements as the value you specify with the Buffer Size parameter.  If the error description, including the terminating NUL byte, contains more bytes than you indicate with the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you pass 0 for the Buffer Size, you can pass VI_NULL for this parameter.   ??#????  ?    Status or Required Size          ?7-   ?  ?    Instrument Handle                ?? 3< ?  ?    BufferSize                       ?? 3 P ?  ?    Code                             ?b ? Q ? ?    Description                        	                   	           	           b    This function clears the error code and error description for the IVI session. If the user specifies a valid IVI session for the instrument_handle parameter, this function clears the error information for the session. If the user passes VI_NULL for the Vi parameter, this function clears the error information for the current execution thread. If the Vi parameter is an invalid session, the function does nothing and returns an error.
The function clears the error code by setting it to VI_SUCCESS.  If the error description string is non-NULL, the function de-allocates the error description string and sets the address to VI_NULL.

Maintaining the error information separately for each thread is useful if the user does not have a session handle to pass to the na65cs3_GetError function, which occurs when a call to na65cs3_Init or na65cs3_InitWithOptions fails.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   ??#????  ?    Status                           ލ-   ?  ?    Instrument Handle                  	              ?    This function returns the coercion information associated with the IVI session.  This function retrieves and clears the oldest instance in which the instrument driver coerced a value you specified to another value.

If you set the NA65CS3_ATTR_RECORD_COERCIONS attribute to VI_TRUE, the instrument driver keeps a list of all coercions it makes on ViInt32 or ViReal64 values you pass to instrument driver functions.  You use this function to retrieve information from that list.

If the next coercion record string, including the terminating NUL byte, contains more bytes than you indicate in this parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you pass a negative number, the function copies the value to the buffer regardless of the number of bytes in the value.

If you pass 0, you can pass VI_NULL for the Coercion Record buffer parameter.

The function returns an empty string in the Coercion Record parameter if no coercion records remain for the session.

    	D    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

If the current value of the return buffer, including the terminating NUL byte, is larger than the size you indicate in the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors     ?    The ViSession handle that you obtain from the na65cs3_init function.  The handle identifies a particular instrument session.

Default Value:  None    ?    Returns the next coercion record for the IVI session.  If there are no coercion records, the function returns an empty string.

The buffer must contain at least as many elements as the value you specify with the Buffer Size parameter.  If the next coercion record string, including the terminating NUL byte, contains more bytes than you indicate with the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

This parameter returns an empty string if no coercion records remain for the session.
    ?    Pass the number of bytes in the ViChar array you specify for the Coercion Record parameter.

If the next coercion record string, including the terminating NUL byte, contains more bytes than you indicate in this parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you pass a negative number, the function copies the value to the buffer regardless of the number of bytes in the value.

If you pass 0, you can pass VI_NULL for the Coercion Record buffer parameter.

Default Value:  None

   ??#????  ?    Status or Required Size          ??-   ?  ?    Instrument Handle                ?~ ? Q ? ?    Coercion Record                  ?? = ? ?  ?    Buffer Size                        	               	               .    This function returns the interchangeability warnings associated with the IVI session. It retrieves and clears the oldest instance in which the class driver recorded an interchangeability warning.  Interchangeability warnings indicate that using your application with a different instrument might cause different behavior. You use this function to retrieve interchangeability warnings.

The driver performs interchangeability checking when the NA65CS3_ATTR_INTERCHANGE_CHECK attribute is set to VI_TRUE.

The function returns an empty string in the Interchange Warning parameter if no interchangeability warnings remain for the session.

In general, the instrument driver generates interchangeability warnings when an attribute that affects the behavior of the instrument is in a state that you did not specify.
    	D    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

If the current value of the return buffer, including the terminating NUL byte, is larger than the size you indicate in the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Pass the number of bytes in the ViChar array you specify for the Interchange Warning parameter.

If the next interchangeability warning string, including the terminating NUL byte, contains more bytes than you indicate in this parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value. For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

If you pass a negative number, the function copies the value to the buffer regardless of the number of bytes in the value.

If you pass 0, you can pass VI_NULL for the Interchange Warning buffer parameter.

Default Value:  None
        Returns the next interchange warning for the IVI session. If there are no interchange warnings, the function returns an empty  string.

The buffer must contain at least as many elements as the value you specify with the Buffer Size parameter. If the next interchangeability warning string, including the terminating NUL byte, contains more bytes than you indicate with the Buffer Size parameter, the function copies Buffer Size - 1 bytes into the buffer, places an ASCII NUL byte at the end of the buffer, and returns the buffer size you must pass to get the entire value.  For example, if the value is "123456" and the Buffer Size is 4, the function places "123" into the buffer and returns 7.

This parameter returns an empty string if no interchangeability
warnings remain for the session.

   ??#????  ?    Status or Required Size          ?-   ?  ?    Instrument Handle                ? = ? ?  ?    Buffer Size                      ? ? Q ? ?    Interchange Warning                	                   	            A    This function clears the list of current interchange warnings.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

   
-   ?  ?    Instrument Handle                
?#????  ?    Status                                 	          ?    When developing a complex test system that consists of multiple test modules, it is generally a good idea to design the test modules so that they can run in any order.  To do so requires ensuring that each test module completely configures the state of each instrument it uses.  If a particular test module does not completely configure the state of an instrument, the state of the instrument depends on the configuration from a previously executed test module.  If you execute the test modules in a different order, the behavior of the instrument and therefore the entire test module is likely to change.  This change in behavior is generally instrument specific and represents an interchangeability problem.

You can use this function to test for such cases.  After you call this function, the interchangeability checking algorithms in the specific driver ignore all previous configuration operations.  By calling this function at the beginning of a test module, you can determine whether the test module has dependencies on the operation of previously executed test modules.

This function does not clear the interchangeability warnings from the list of previously recorded interchangeability warnings.  If you want to guarantee that the na65cs3_GetNextInterchangeWarning function only returns those interchangeability warnings that are generated after calling this function, you must clear the list of interchangeability warnings.  You can clear the interchangeability warnings list by repeatedly calling the na65cs3_GetNextInterchangeWarning function until no more interchangeability warnings are returned.  If you are not interested in the content of those warnings, you can call the na65cs3_ClearInterchangeWarnings function.
     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

   ?-   ?  ?    Instrument Handle                W#????  ?    Status                                 	          .    This function obtains a multithread lock on the instrument session.  Before it does so, it waits until all other execution threads have released their locks on the instrument session.

Other threads might have obtained a lock on this session in the following ways:

- The user's application called na65cs3_LockSession.

- A call to the instrument driver locked the session.

- A call to the IVI engine locked the session.

After your call to na65cs3_LockSession returns successfully, no other threads can access the instrument session until you call na65cs3_UnlockSession.

Use na65cs3_LockSession and na65cs3_UnlockSession around a sequence of calls to instrument driver functions if you require that the instrument retain its settings through the end of the sequence.

You can safely make nested calls to na65cs3_LockSession within the same thread.  To completely unlock the session, you must balance each call to na65cs3_LockSession with a call to na65cs3_UnlockSession.  If, however, you use the Caller Has Lock parameter in all calls to na65cs3_LockSession and na65cs3_UnlockSession within a function, the IVI Library locks the session only once within the function regardless of the number of calls you make to na65cs3_LockSession.  This allows you to call na65cs3_UnlockSession just once at the end of the function. 
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    This parameter serves as a convenience.  If you do not want to use this parameter, pass VI_NULL. 

Use this parameter in complex functions to keep track of whether you obtain a lock and therefore need to unlock the session.  Pass the address of a local ViBoolean variable.  In the declaration of the local variable, initialize it to VI_FALSE.  Pass the address of the same local variable to any other calls you make to na65cs3_LockSession or na65cs3_UnlockSession in the same function.

The parameter is an input/output parameter.  na65cs3_LockSession and na65cs3_UnlockSession each inspect the current value and take the following actions:

- If the value is VI_TRUE, na65cs3_LockSession does not lock the session again.  If the value is VI_FALSE, na65cs3_LockSession obtains the lock and sets the value of the parameter to VI_TRUE.

- If the value is VI_FALSE, na65cs3_UnlockSession does not attempt to unlock the session.  If the value is VI_TRUE, na65cs3_UnlockSession releases the lock and sets the value of the parameter to VI_FALSE.
 
Thus, you can, call na65cs3_UnlockSession at the end of your function without worrying about whether you actually have the lock.  

Example:

ViStatus TestFunc (ViSession vi, ViInt32 flags)
{
    ViStatus error = VI_SUCCESS;
    ViBoolean haveLock = VI_FALSE;

    if (flags & BIT_1)
        {
        viCheckErr( na65cs3_LockSession(vi, &haveLock));
        viCheckErr( TakeAction1(vi));
        if (flags & BIT_2)
            {
            viCheckErr( na65cs3_UnlockSession(vi, &haveLock));
            viCheckErr( TakeAction2(vi));
            viCheckErr( na65cs3_LockSession(vi, &haveLock);
            } 
        if (flags & BIT_3)
            viCheckErr( TakeAction3(vi));
        }

Error:
    /* 
       At this point, you cannot really be sure that 
       you have the lock.  Fortunately, the haveLock 
       variable takes care of that for you.          
    */
    na65cs3_UnlockSession(vi, &haveLock);
    return error;
}   '?#????  ?    Status                           /'-   ?  ?    Instrument Handle                /? H ? ?  ?    Caller Has Lock                    	               	            ?    This function releases a lock that you acquired on an instrument session using na65cs3_LockSession.  Refer to na65cs3_LockSession for additional information on session locks.
    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
    ?    This parameter serves as a convenience.  If you do not want to use this parameter, pass VI_NULL. 

Use this parameter in complex functions to keep track of whether you obtain a lock and therefore need to unlock the session. 
Pass the address of a local ViBoolean variable.  In the declaration of the local variable, initialize it to VI_FALSE.  Pass the address of the same local variable to any other calls you make to na65cs3_LockSession or na65cs3_UnlockSession in the same function.

The parameter is an input/output parameter.  na65cs3_LockSession and na65cs3_UnlockSession each inspect the current value and take the following actions:

- If the value is VI_TRUE, na65cs3_LockSession does not lock the session again.  If the value is VI_FALSE, na65cs3_LockSession obtains the lock and sets the value of the parameter to VI_TRUE.

- If the value is VI_FALSE, na65cs3_UnlockSession does not attempt to unlock the session.  If the value is VI_TRUE, na65cs3_UnlockSession releases the lock and sets the value of the parameter to VI_FALSE.
 
Thus, you can, call na65cs3_UnlockSession at the end of your function without worrying about whether you actually have the lock.  

Example:

ViStatus TestFunc (ViSession vi, ViInt32 flags)
{
    ViStatus error = VI_SUCCESS;
    ViBoolean haveLock = VI_FALSE;

    if (flags & BIT_1)
        {
        viCheckErr( na65cs3_LockSession(vi, &haveLock));
        viCheckErr( TakeAction1(vi));
        if (flags & BIT_2)
            {
            viCheckErr( na65cs3_UnlockSession(vi, &haveLock));
            viCheckErr( TakeAction2(vi));
            viCheckErr( na65cs3_LockSession(vi, &haveLock);
            } 
        if (flags & BIT_3)
            viCheckErr( TakeAction3(vi));
        }

Error:
    /* 
       At this point, you cannot really be sure that 
       you have the lock.  Fortunately, the haveLock 
       variable takes care of that for you.          
    */
    na65cs3_UnlockSession(vi, &haveLock);
    return error;
}   9#????  ?    Status                           @?-   ?  ?    Instrument Handle                A^ H ? ?  ?    Caller Has Lock                    	               	            ?    This function writes a user-specified string to the instrument.

Note:  This function bypasses IVI attribute state caching.  Therefore, when you call this function, the cached values for all attributes will be invalidated.     2    Pass the string to be written to the instrument.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   J? A ? ?  ?    Write Buffer                     J?#????  ?    Status                           R?-   ?  ?    Instrument Handle                      	               /    This function reads data from the instrument.     c    After this function executes, this parameter contains the data that was read from the instrument.    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
     ?    Pass the maximum number of bytes to read from the instruments.  

Valid Range:  0 to the number of elements in the Read Buffer.

Default:  0

     ^    Returns the number of bytes actually read from the instrument and stored in the Read Buffer.   T0 @ ?  ?    Read Buffer                      T?#????  ?    Status                           \+-   ?  ?    Instrument Handle                \? @ C ?  ?    Number of Bytes To Read          ]{ ?' ?  ?    Num Bytes Read                     	            	               0    	           ?    This function performs the following operations:

- Closes the instrument I/O session.

- Destroys the instrument driver session and all of its attributes.

- Deallocates any memory resources the driver uses.

Notes:

(1) You must unlock the session before calling na65cs3_close.

(2) After calling na65cs3_close, you cannot use the instrument driver again until you call na65cs3_init or na65cs3_InitWithOptions.

    ?    Returns the status code of this operation.  The status code  either indicates success or describes an error or warning condition.  You examine the status code from each call to an instrument driver function to determine if an error occurred.

To obtain a text description of the status code, call the na65cs3_error_message function.  To obtain additional information about the error condition, call the na65cs3_GetError function.  To clear the error information from the driver, call the na65cs3_ClearError function.
          
The general meaning of the status code is as follows:

Value                  Meaning
-------------------------------
0                      Success
Positive Values        Warnings
Negative Values        Errors

This driver defines the following status codes:
          
Status    Description
-------------------------------------------------
WARNINGS:
/*=CHANGE:===================================================* 
Insert Instrument-defined warning codes here.
 *================================================END=CHANGE=*/ 

ERRORS:
/*=CHANGE:===================================================* 
Insert Instrument-defined error codes here.
 *================================================END=CHANGE=*/ 
          
This instrument driver also returns errors and warnings defined by other sources.  The following table defines the ranges of additional status codes that this driver can return.  The table lists the different include files that contain the defined constants for the particular status codes:
          
Numeric Range (in Hex)   Status Code Types    
-------------------------------------------------
3FFA0000 to 3FFA1FFF     IVI      Warnings
3FFF0000 to 3FFFFFFF     VISA     Warnings
3FFC0000 to 3FFCFFFF     VXIPnP   Driver Warnings
          
BFFA0000 to BFFA1FFF     IVI      Errors
BFFF0000 to BFFFFFFF     VISA     Errors
BFFC0000 to BFFCFFFF     VXIPnP   Driver Errors

     ?    The ViSession handle that you obtain from the na65cs3_init or na65cs3_InitWithOptions function.  The handle identifies a particular instrument session.

Default Value:  None
   `?#????  ?    Status                           hO-   ?  ?    Instrument Handle                  	            ????         
t  #?     K.    init                            ????         %O  F>     K.    InitWithOptions                 ????         H  RK     K.    find                            ????         S  [m     K.    getModelInfo                    ????         \?  eJ     K.    getModelParts                   ????         f?  o?     K.    getPartSpecInfo                 ????         s?  |?     K.    query_language                  ????         }?  ??     K.    query_id                        ????         ?c  ?2     K.    config_SD                       ????         ?}  ?_     K.    config_SDDCScale                ????         ?q  ?2     K.    config_SDBandwidth              ????         ?d  ?{     K.    config_SDMaxAngSettleTime       ????         ??  ǀ     K.    config_SDSignalMode             ????         ȼ  ??     K.    config_SDRatio                  ????         ??  ?J     K.    config_SDRefSrcMode             ????         ?t  ?T     K.    config_SDRelayIOState           ????         ??  ?J     K.    config_SDUpdateMode             ????         ?| 
     K.    config_DSRotation               ????        z      K.    config_DSAngle                  ????        ? A     K.    config_DSDCScale                ????        ? (p     K.    config_DSSignalMode             ????        *. 4?     K.    config_DSRatio                  ????        6r A?     K.    config_DSRefSrcMode             ????        Cy N      K.    config_DSRelayIOState           ????        O? ZG     K.    config_DSLineToLineVoltage      ????        [? g>     K.    config_DSInputRefVolt           ????        h? s?     K.    config_DSRotationRate           ????        uK ?      K.    config_DSRotateStopAng          ????        ?? ?$     K.    config_DSRotationMode           ????        ?? ??     K.    config_DSTriggerSrc             ????        ?? ?n     K.    config_DSTriggerSlope           ????        ? ?      K.    config_DSPhaseShift             ????        ?? ??     K.    config_REF                      ????        ?? ??     K.    config_REFFreq                  ????        ? ??     K.    config_REFVolt                  ????        ?? ?,     K.    config_REFRelayIOState          ????        ?\ ??     K.    SetAttributeViInt32             ????        ?| ?     K.    SetAttributeViReal64            ????        ??        K.    SetAttributeViString            ????        ? B     K.    SetAttributeViBoolean           ????        ? .d     K.    SetAttributeViSession           ????        /? Ai     K.    GetAttributeViInt32             ????        B? T     K.    GetAttributeViReal64            ????        UP qK     K.    GetAttributeViString            ????        r? ?5     K.    GetAttributeViBoolean           ????        ?r ??     K.    GetAttributeViSession           ????        ?  ?:     K.    CheckAttributeViInt32           ????        ?? ??     K.    CheckAttributeViReal64          ????        ?? Ϊ     K.    CheckAttributeViString          ????        ?E ?e     K.    CheckAttributeViBoolean         ????        ?  ?      K.    CheckAttributeViSession         ????        ?? ?     K.    initiate_DSRotation             ????        ?V ??     K.    get_DSRotationComplete          ????        ?(  j     K.    query_SDAngle                   ????        w ?     K.    query_SDVelocity                ????        ? ?     K.    query_SDDCScale                 ????        ? 
?     K.    query_SDBandwidth               ????        ?      K.    query_SDMaxAngTime              ????         ?     K.    query_SDSignalMode              ????         ?     K.    query_SDRatio                   ????        ? ?     K.    query_SDRefSrcMode              ????        ? Z     K.    query_SDRelayIOState            ????        m !     K.    query_SDUpdateMode              ????        "! %     K.    query_DSAngle                   ????        &? )t     K.    query_DSDCScale                 ????        +  .?     K.    query_DSSignalMode              ????        0% 3?     K.    query_DSRatio                   ????        5 8?     K.    query_DSRefSrcMode              ????        : =j     K.    query_DSRelayIOState            ????        >? B     K.    query_DSLineToLineVoltage       ????        C? G?     K.    query_DSInputRefVolt            ????        IJ Ln     K.    query_DSRotationRate            ????        M? Q     K.    query_DSRotateStopAng           ????        R? U?     K.    query_DSRotationMode            ????        W [v     K.    query_DSTriggerSrc              ????        ]
 `M     K.    query_DSTriggerSlope            ????        a? d?     K.    query_DSPhaseShift              ????        fh i?     K.    query_RefFreq                   ????        j? m'     K.    query_RefVolt                   ????        n2 p?     K.    query_RefRelayIOState           ????        r {?     K.    reset                           ????        {? ?M     K.    ResetWithDefaults               ????        ?? ??     K.    Disable                         ????        ? ??     K.    self_test                       ????        ?? ?     K.    revision_query                  ????        ?{ ??     K.    error_query                     ????        ?? ??     K.    error_message                   ????        ?? ??     K.    InvalidateAllAttributes         ????        ? ?^     K.    GetError                        ????        ӓ ?E     K.    ClearError                      ????        ߿ ?y     K.    GetNextCoercionRecord           ????        ?m ?     K.    GetNextInterchangeWarning       ????        	? V     K.    ClearInterchangeWarnings        ????        ? !?     K.    ResetInterchangeCheck           ????        "a 7?     K.    LockSession                     ????        8] I!     K.    UnlockSession                   ????        I? SF     K.    WriteInstrData                  ????        S? ]?     K.    ReadInstrData                   ????        _ i     K.    close                                 ?                                     DInitialize                           DInitialize With Options             ?Application Functions                DInstrument Find                      DInstrument Model Info                DInstrument Model Part Support        DInstrument Part Spec Info            DQuery Language Support               DQuery Instrument ID                 sConfiguration Functions           ????Configure Measurement Channel        DConfigure Measurement Channel        DConfig Measurement DC Scale          DConfig Measurement Bandwidth         DConfig Measurement Max Ang Time      DConfig Measurement Signal Mode       DConfig Measurement Ratio             DConfig Measurement Ref Src           DConfig Measurement Relay State       DConfig Measurement Update Mode    ????Configure Stimulus Channel           DConfigure Stimulus Rotation          DConfig Stimulus Angle                DConfig Stimulus DC Scale             DConfig Stimulus Signal Mode          DConfig Stimulus Ratio                DConfig Stimulus Ref Src              DConfig Stimulus Relay State          DConfig Stimulus Line-Line Volt       DConfig Stimulus Input Ref Volt       DConfig Stimulus Rotation Rate        DConfig Stimulus Rotate Stop Ang      DConfig Stimulus Rotation Mode        DConfig Stimulus Trigger Source       DConfig Stimulus Trigger Slope        DConfig Stimulus Phase Shift       ????Configure Reference Channel          DConfigure Reference Channel          DConfig Reference Freq                DConfig Reference Volt                DConfig Reference Relay State        tSet/Get/Check Attribute             ?Set Attribute                        DSet Attribute ViInt32                DSet Attribute ViReal64               DSet Attribute ViString               DSet Attribute ViBoolean              DSet Attribute ViSession             YGet Attribute                        DGet Attribute ViInt32                DGet Attribute ViReal64               DGet Attribute ViString               DGet Attribute ViBoolean              DGet Attribute ViSession             ?Check Attribute                      DCheck Attribute ViInt32              DCheck Attribute ViReal64             DCheck Attribute ViString             DCheck Attribute ViBoolean            DCheck Attribute ViSession           }Action/Status Functions           ????Stimulus Command                     DInitiate Rotation                    DGet Rotation Complete               ?Data Functions                    ????Measurement Queries                  DRead Measurement Angle               DRead Measurement Velocity            DRead Measurement DC Scale            DRead Measurement Bandwidth           DRead Measurement Max Ang Time        DRead Measurement Signal Mode         DRead Measurement Ratio               DRead Measurement Ref Src             DRead Measurement Relay State         DRead Measurement Update Mode      ????Stimulus Queries                     DRead Stimulus Angle                  DRead Stimulus DC Scale               DRead Stimulus Signal Mode            DRead Stimulus Ratio                  DRead Stimulus Ref Src                DRead Stimulus Relay State            DRead Stimulus Line-Line Volt         DRead Stimulus Input Ref Volt         DRead Stimulus Rotation Rate          DRead Stimulus Rotate Stop Ang        DRead Stimulus Rotation Mode          DRead Stimulus Trigger Source         DRead Stimulus Trigger Slope          DRead Stimulus Phase Shift         ????Reference Queries                    DRead Reference Freq                  DRead Reference Volt                  DRead Reference Relay State          YUtility Functions                    DReset                                DReset With Defaults                  DDisable                              DSelf-Test                            DRevision Query                       DError-Query                          DError Message                        DInvalidate All Attributes           ?Error Info                           DGet Error                            DClear Error                         	Coercion Info                        DGet Next Coercion Record            	aInterchangeability Info              DGet Next Interchange Warning         DClear Interchange Warnings           DReset Interchange Check             	?Locking                              DLock Session                         DUnlock Session                      
&Instrument I/O                       DWrite Instrument Data                DRead Instrument Data                 DClose                           