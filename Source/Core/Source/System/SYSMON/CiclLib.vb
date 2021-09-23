'Option Strict Off
'Option Explicit On

Imports System

Public Module CiclLib


	'=========================================================
    ' -------------------------------------------------------------------------
    '  Title   : CICL.BAS  - Common Instrument Control Layer for VIPERT
    '  Date    : 07-06-06
    '  Purpose : Include file for the CICL
    ' -------------------------------------------------------------------------

    'DefInt(A-Z)    'VB.net does not support this, must explicity declare all variables
    Dim A As Short
    Dim B As Short
    Dim C As Short
    Dim D As Short
    Dim E As Short
    Dim F As Short
    Dim G As Short
    Dim H As Short
    Dim I As Short
    Dim J As Short
    Dim K As Short
    Dim L As Short
    Dim M As Short
    Dim N As Short
    Dim O As Short
    Dim P As Short
    Dim Q As Short
    Dim R As Short
    Dim S As Short
    Dim T As Short
    Dim U As Short
    Dim V As Short
    Dim W As Short
    Dim X As Short
    Dim Y As Short
    Dim Z As Short

    'ATXmlAPI.def
    '   atxml_Initialize
    '   atxml_Close
    '   atxml_RegisterTSF
    '   atxml_ValidateRequirements
    '   atxml_IssueSignal
    '   atxml_TestStationStatus
    '   atxml_RegisterInstStatus
    '   atxml_InvokeRemoveAllSequence
    '   atxml_IssueNativeCmds
    '   atxml_IssueDriverFunctionCall
    '   atxml_ParseAvailability
    '   atxml_ParseErrDbgResponse
    '   atxml_ErrDbgNextError
    '   atxml_ErrDbgNextDebug
    '   atxml_ErrDbgClose
    '   atxml_GetSingleIntValue
    '   atxml_GetIntArrayValue
    '   atxml_GetSingleDblValue
    '   atxml_GetDblArrayValue
    '   atxml_GetStringValue
    '   atxml_WriteCmds
    '   atxml_ReadCmds
    '   atxml_CallDriverFunction


    '----------------- VXIplug&play Driver Declarations ---------
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal ProcType As String, ByVal guid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Short, ByRef ActReadLen As Integer) As Integer


    'bus functions
    Declare Function atxml_GetSingleIntValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef IntValue As Short) As Integer
    Declare Function atxml_GetIntArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal IntArrayPtr As Short, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetSingleDblValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef DblValue As Double) As Integer
    Declare Function atxml_GetDblArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal DblArrayPtr As Double, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetStringValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal StrPtr As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" (ByVal SignalDescription As String, ByVal Response As String, ByVal BufferSize As Short) As Integer



    'AtXmlDriverFunc.def
    '   atxmlDF_viSetAttribute
    '   atxmlDF_viGetAttribute
    '   atxmlDF_viStatusDesc
    '   atxmlDF_viTerminate
    '   atxmlDF_viLock
    '   atxmlDF_viUnlock
    '   atxmlDF_viEnableEvent
    '   atxmlDF_viDisableEvent
    '   atxmlDF_viDiscardEvents
    '   atxmlDF_viWaitOnEvent
    '   atxmlDF_viRead
    '   atxmlDF_viReadAsync
    '   atxmlDF_viReadToFile
    '   atxmlDF_viWrite
    '   atxmlDF_viWriteAsync
    '   atxmlDF_viWriteFromFile
    '   atxmlDF_viAssertTrigger
    '   atxmlDF_viReadSTB
    '   atxmlDF_viClear
    '   atxmlDF_viSetBuf
    '   atxmlDF_viFlush
    '   atxmlDF_viBufWrite
    '   atxmlDF_viBufRead
    '   atxmlDF_viIn8
    '   atxmlDF_viOut8
    '   atxmlDF_viIn16
    '   atxmlDF_viOut16
    '   atxmlDF_viIn32
    '   atxmlDF_viOut32
    '   atxmlDF_viMoveIn8
    '   atxmlDF_viMoveOut8
    '   atxmlDF_viMoveIn16
    '   atxmlDF_viMoveOut16
    '   atxmlDF_viMoveIn32
    '   atxmlDF_viMoveOut32
    '   atxmlDF_viMove
    '   atxmlDF_viMoveAsync
    '   atxmlDF_viGpibControlREN
    '   atxmlDF_viGpibControlATN
    '   atxmlDF_viGpibSendIFC
    '   atxmlDF_viGpibCommand
    '   atxmlDF_viGpibPassControl
    '   atxmlDF_viVxiCommandQuery
    '   atxmlDF_viAssertUtilSignal
    '   atxmlDF_viAssertIntrSignal
    '   atxmlDF_viMapTrigger
    '   atxmlDF_viUnmapTrigger
    '   atxmlDF_zt1428_read_waveform
    '   atxmlDF_eip_selectCorrectionsTable
    '   atxmlDF_eip_setCorrectedPower


    Declare Function atxmlDF_viSetAttribute Lib "AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer

    Declare Function atxmlDF_viOut16 Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByVal val16 As Short) As Integer

    Declare Function atxmlDF_viStatusDesc Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal Status As Integer, ByVal desc As String) As Integer

    Declare Function atxmlDF_zt1428_read_waveform Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal Source As Integer, ByVal TransferType As Integer, ByRef WaveFormArray As Double, ByRef NumberOfPoints As Integer, ByRef AcquisitionCount As Integer, ByRef SampleInterval As Double, ByRef TimeOffset As Double, ByRef XREFERENCE As Integer, ByRef VoltIncrement As Double, ByRef VoltOffset As Double, ByRef YREFERENCE As Integer) As Integer

    Declare Function atxmlDF_eip_selectCorrectionsTable Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal FilePath As String, ByVal TableName As String) As Integer

    Declare Function atxmlDF_eip_setCorrectedPower Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal instrSession As Integer, ByVal Frequency As Double, ByVal Power As Single) As Integer

    Declare Function atxmlDF_viClear Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer) As Integer

    Declare Function atxmlDF_viWrite Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal Buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    Declare Function atxmlDF_viRead Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal Buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    Declare Function atxmlDF_viIn16 Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal space As Short, ByVal offset As Integer, ByRef val16 As Short) As Integer

    Declare Function atxmlDF_viOpen Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal sesn As Integer, ByVal name As String, ByVal timeout As Integer, ByRef vi As Integer) As Integer


    '----------------- Init and GUID ---------------------------------------
    Public Const guid As String = "{A6936371-9B69-8A34-839B-2B04CEA269AB}"
    Public Const ProcType As String = "SFP"
    Public Const conNoDLL As Short = 48

    '----------------- VISA -------------------------------------

    Public Const VI_ATTR_RECV_TRIG_ID As Integer = &H3FFF4012

End Module