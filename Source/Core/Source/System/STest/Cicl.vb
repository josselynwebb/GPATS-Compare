'Option Strict Off
Option Explicit On

Imports System

Public Module CiclLib

    '**************************************************************
    '* Nomenclature   : ATS-ViperT SYSTEM SELF TEST               *
    '*                  Common Instrument Control Layer           *
    '* Version        : 2.0                                       *
    '* Last Update    : Apr 1, 2017                               *
    '* Purpose        : This module is the CICL include file      *
    '**************************************************************



    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{F0036371-9B45-4d60-839B-4B94CEA259CE}"
    Public Const proctype As String = "SFP" ' selftest
    Public Const conNoDLL As Short = 48

    '----------------- XML Declarations ---------
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer
    Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer
    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" _
        (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer
    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" _
        (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer
    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" _
        (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Short, ByRef ActReadLen As Integer) As Integer
    Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" _
        (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Short) As Integer
    Declare Function atxml_IssueIst Lib "AtXmlApi.dll" _
        (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Integer) As Integer


    'bus functions
    Declare Function atxml_GetSingleIntValue Lib "AtXmlApi.dll" _
        (ByVal Response As String, ByVal Attr As String, ByRef IntValue As Short) As Integer
    Declare Function atxml_GetIntArrayValue Lib "AtXmlApi.dll" _
        (ByVal Response As String, ByVal Attr As String, ByVal IntArrayPtr As Short, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetSingleDblValue Lib "AtXmlApi.dll" _
        (ByVal Response As String, ByVal Attr As String, ByRef DblValue As Double) As Integer
    Declare Function atxml_GetDblArrayValue Lib "AtXmlApi.dll" _
        (ByVal Response As String, ByVal Attr As String, ByVal DblArrayPtr As Double, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetStringValue Lib "AtXmlApi.dll" _
        (ByVal Response As String, ByVal Attr As String, ByVal StrPtr As String, ByVal BufferSize As Short) As Integer
    Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" _
        (ByVal SignalDescription As String, ByVal Response As String, ByVal BufferSize As Short) As Integer


    '----------------- VXIplug&play Driver Declarations ---------


    'AtXmlDriverFunc.def
    Declare Function atxmlDF_viSetAttribute Lib "AtxmlDriverFunc.DLL" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer

    Declare Function atxmlDF_viOut16 Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByVal val16 As Short) As Integer
    Declare Function atxmlDF_viStatusDesc Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal status As Integer, ByVal desc As String) As Integer
    Declare Function atxmlDF_zt1428_read_waveform Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal Source As Integer, ByVal TransferType As Integer, ByRef WaveFormArray As Double, ByRef NumberOfPoints As Integer, ByRef AcquisitionCount As Integer, ByRef SampleInterval As Double, ByRef TimeOffset As Double, ByRef XREFERENCE As Integer, ByRef VoltIncrement As Double, ByRef VoltOffset As Double, ByRef YREFERENCE As Integer) As Integer

    Declare Function atxmlDF_eip_selectCorrectionsTable Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal FilePath As String, ByVal TableName As String) As Integer

    Declare Function atxmlDF_eip_setCorrectedPower Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal instrSession As Integer, ByVal Frequency As Double, ByVal Power As Single) As Integer
    Declare Function atxmlDF_viClear Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer) As Integer
    Declare Function atxmlDF_viWrite Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function atxmlDF_viRead Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function atxmlDF_viIn16 Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal vi As Integer, ByVal space As Short, ByVal offset As Integer, ByRef val16 As Short) As Integer
    Declare Function atxmlDF_viOpen Lib "AtXmlDriverFunc.dll" _
        (ByVal ResourceName As String, ByVal sesn As Integer, ByVal name As String, ByVal timeout As Integer, ByRef vi As Integer) As Integer



    '   status = viIn16 (instrID, VI_A16_SPACE, rel_addr, &val);
    '/*---------------------------------------------------------------------------*/
    '//ViStatus _VI_FUNC  viIn16          (ViSession vi, ViUInt16 space,
    '//                                    ViBusAddress offset, ViPUInt16 val16);
    'extern ATXML_DRVRFNC int CALL_TYPE atxmlDF_viIn16 (char *ResourceName,
    '            int vi,
    '            unsigned short space,
    '            unsigned long offset,
    '            unsigned short *val16);





    'Vxi stuff that I am replacing as I can

    Public Const VI_NULL As Short = 0
    Public Const VI_TRUE As Short = 1
    Public Const VI_FALSE As Short = 0
    Public Const VI_SUCCESS As Short = &H0
    Public Const VI_ATTR_TMO_VALUE As Integer = &H3FFF001A
    Public Const VI_ERROR_RSRC_NFOUND As Integer = &HBFFF0011
    Public Const VI_ERROR_TMO As Integer = &HBFFF0015
    Public Const VI_A16_SPACE As Short = 1
    Public Const VI_A24_SPACE As Short = 2
    Public Const VI_A32_SPACE As Short = 3
    Public Const VI_READ_BUF As Short = 1
    Public Const VI_WRITE_BUF As Short = 2

    Public Const VI_SUCCESS_SYNC As Integer = -1073676443  'Operation completed successfully, but the operation was actually synchronous rather than asynchronous.
    Public Const VI_SUCCESS_NESTED_EXCLUSIVE As Integer = -1073676442  'Operation completed successfully, and this session has nested exclusive locks.
    Public Const VI_SUCCESS_NESTED_SHARED As Integer = -1073676441   'Operation completed successfully, and this session has nested shared locks.
    Public Const VI_SUCCESS_NCHAIN As Integer = -1073676440   'Event handled successfully. Do not invoke any other handlers on this session for this event.
    Public Const VI_WARN_NSUP_BUF As Integer = -1073676424   'The specified I/O buffer is not supported.
    Public Const VI_WARN_UNKNOWN_STATUS As Integer = -1073676421   'The status code passed to the operation could not be interpreted.
    Public Const VI_WARN_NSUP_ATTR_STATE As Integer = -1073676420   'Although the specified state of the attribute is valid, it is not supported by this resource implementation.
    Public Const VI_WARN_NULL_OBJECT As Integer = -1073676418   'The specified object reference is uninitialized.
    Public Const VI_SUCCESS_QUEUE_NEMPTY As Integer = -1073676416   'Wait terminated successfully on receipt of an event notification. There is at least one more event occurrence of the type specified by inEventType available for this session.
    Public Const VI_SUCCESS_DEV_NPRESENT As Integer = -1073676413   'Session opened successfully, but the device at the specified address is not responding.
    Public Const VI_WARN_CONFIG_NLOADED As Integer = -1073676407   'The specified configuration either does not exist or could not be loaded. VISA-specified defaults will be used.
    Public Const VI_SUCCESS_MAX_CNT As Integer = -1073676294   'The number of bytes transferred is equal to the input count.
    Public Const VI_SUCCESS_TERM_CHAR As Integer = -1073676293   'The specified termination character was read.
    Public Const VI_SUCCESS_QUEUE_EMPTY As Integer = -1073676292   'Operation completed successfully, but queue was already empty.
    Public Const VI_SUCCESS_EVENT_DIS As Integer = -1073676291   'Specified event is already disabled for at least one of the specified mechanisms.
    Public Const VI_SUCCESS_EVENT_EN As Integer = -1073676290   'Specified event is already enabled for at least one of the specified mechanisms.

    Public Const VI_ERROR_LIBRARY_NFOUND As Integer = -1073807202   'A code library required by VISA could not be located or loaded.
    Public Const VI_ERROR_SESN_NLOCKED As Integer = -1073807204   'The current session did not have a lock on the resource.
    Public Const VI_ERROR_INV_MODE As Integer = -1073807215   'Invalid mode specified.
    Public Const VI_ERROR_INV_LENGTH As Integer = -1073807229   'Invalid length specified.
    Public Const VI_ERROR_INV_PARAMETER As Integer = -1073807240   'The value of some parameter (which parameter is not known) is invalid.
    Public Const VI_ERROR_RSRC_BUSY As Integer = -1073807246   'The resource is valid, but VISA cannot currently access it.
    Public Const VI_ERROR_USER_BUF As Integer = -1073807247   'A specified user buffer is not valid or cannot be accessed for the required size.
    Public Const VI_ERROR_NSUP_ALIGN_OFFSET As Integer = -1073807248   'The specified offset is not properly aligned for the access width of the operation.
    Public Const VI_ERROR_ASRL_OVERRUN As Integer = -1073807252   'An overrun error occurred during transfer. A character was not read from the hardware before the next character arrived.
    Public Const VI_ERROR_ASRL_FRAMING As Integer = -1073807253   'A framing error occurred during transfer.
    Public Const VI_ERROR_ASRL_PARITY As Integer = -1073807254   'A parity error occurred during transfer.
    Public Const VI_ERROR_NSYS_CNTLR As Integer = -1073807263   'The interface associated with this session is not the system controller.
    Public Const VI_ERROR_RESP_PENDING As Integer = -1073807271   'A previous response is still pending, causing a multiple query error.
    Public Const VI_ERROR_NSUP_VAR_WIDTH As Integer = -1073807275   'Cannot support source and destination widths that are different.
    Public Const VI_ERROR_INV_WIDTH As Integer = -1073807278   'Invalid access width specified.
    Public Const VI_ERROR_QUEUE_ERROR As Integer = -1073807301   'Unable to queue the asynchronous operation.
    Public Const VI_ERROR_IN_PROGRESS As Integer = -1073807303   'Unable to queue the asynchronous operation because there is already an operation in progress.
    Public Const VI_ERROR_ABORT As Integer = -1073807312   'User abort occurred during transfer.
    Public Const VI_ERROR_NENABLED As Integer = -1073807313   'You must be enabled for events of the specified type in order to receive them.
    Public Const VI_ERROR_QUEUE_OVERFLOW As Integer = -1073807315   'The event queue for the specified type has overflowed (usually due to previous events not having been closed).
    Public Const VI_ERROR_INV_ACCESS_KEY As Integer = -1073807327   'The access key to the specified resource is invalid.
    Public Const VI_ERROR_INV_LOCK_TYPE As Integer = -1073807328   'The specified type of lock is not supported by this resource.
    Public Const VI_ERROR_INV_DEGREE As Integer = -1073807333  'Specified degree is invalid.

    Public Const VI_ERROR_INV_OBJECT As Integer = -1073807346 'The given session or object reference is invalid.
    Public Const VI_ERROR_INV_SESSION As Integer = -1073807346 ' The given session or object reference is invalid.
    Public Const VI_ERROR_NSUP_OPER As Integer = -1073807257  'The given session or object reference does not support this operation.
    Public Const VI_ERROR_RAW_WR_PROT_VIOL As Integer = -1073807308  'Violation of raw write protocol occurred during transfer.
    Public Const VI_ERROR_RAW_RD_PROT_VIOL As Integer = -1073807307  'Violation of raw read protocol occurred during transfer.
    Public Const VI_ERROR_INV_SETUP As Integer = -1073807302  'Unable to start operation because setup is invalid (due to attributes being set to an inconsistent state).
    Public Const VI_ERROR_NCIC As Integer = -1073807264  'The interface associated with this session is not currently the controller in charge.
    Public Const VI_ERROR_NLISTENERS As Integer = -1073807265 'No Listeners condition is detected (both NRFD and NDAC are deasserted).
    Public Const VI_ERROR_IO As Integer = -1073807298 'Could not perform read/write operation because of I/O error.
    Public Const VI_ERROR_RSRC_LOCKED As Integer = -1073807345 'Operation failed due to locked resource
    Public Const VI_ERROR_CONN_LOST As Integer = -1073807194 'The I/O connection for the given session has been lost.
    Public Const VI_ERROR_OUTP_PROT_VIOL As Integer = -1073807306 ' Device reported an output protocol error occurred during transfer.
    Public Const VI_ERROR_BERR As Integer = -1073807304 'A bus error occurred during transfer.


    ' - Resource Template Functions and Operations ----------------------------
    Declare Function viOpenDefaultRM Lib "VISA32.DLL" Alias "#141" _
        (ByRef sesn As Integer) As Integer
    Declare Function viOpen Lib "VISA32.DLL" Alias "#131" _
        (ByVal sesn As Integer, ByVal viDesc As String, ByVal mode As Integer, ByVal timeout As Integer, ByRef vi As Integer) As Integer
    Declare Function viClose Lib "VISA32.DLL" Alias "#132" _
        (ByVal vi As Integer) As Integer
    Declare Function viGetAttribute Lib "VISA32.DLL" Alias "#133" _
        (ByVal vi As Integer, ByVal attrName As Integer, ByRef attrValue As String) As Integer
    Declare Function viSetAttribute Lib "VISA32.DLL" Alias "#134" _
        (ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer
    Declare Function viStatusDesc Lib "VISA32.DLL" Alias "#142" _
        (ByVal vi As Integer, ByVal status As Integer, ByVal desc As String) As Integer
    Declare Function viReadSTB Lib "VISA32.DLL" Alias "#259" _
        (ByVal vi As Integer, ByRef status As Short) As Integer
    Declare Function viClear Lib "VISA32.DLL" Alias "#260" (ByVal vi As Integer) As Integer

    ' - Basic I/O Operations --------------------------------------------------
    Declare Function viRead Lib "VISA32.DLL" Alias "#256" _
        (ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function viWrite Lib "VISA32.DLL" Alias "#257" _
        (ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function viFlush Lib "VISA32.DLL" Alias "#268" _
        (ByVal vi As Integer, ByVal mask As Short) As Integer

    ' - Memory I/O Operations -------------------------------------------------
    Declare Function viIn16 Lib "VISA32.DLL" Alias "#261" _
        (ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByRef val16 As Short) As Integer
    'Declare Function viOut16 Lib "VISA32.DLL" Alias "#262" (ByVal vi As Long, ByVal accSpace As Integer, ByVal offset As Long, ByVal val16 As Integer) As Long




End Module