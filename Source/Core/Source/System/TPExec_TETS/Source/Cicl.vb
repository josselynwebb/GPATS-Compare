Option Strict Off
Option Explicit On
Module CiclLib
    '**************************************************************
    '* Nomenclature   : VIPER/T SYSTEM SELF TEST                  *
    '*                  Common Instrument Control Layer           *
    '* Version        : 1.0                                       *
    '* Last Update    :                                           *
    '* Purpose        : This module is the CICL include file.     *
    '*                  It replaces the Visa32.bas file.          *
    '**************************************************************

    ' bbbb new module added to support the CICL (replaces most of the vxi stuff)
    '----------------- VXIplug&play Driver Declarations ---------
    Declare Function atxml_Initialize Lib "C:\Program Files\VIPERT\ISS\Bin\AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll" () As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal InstrumentCmds As String, ByRef ActWriteLen As Integer) As Integer

    Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" (ByVal ResourceName As String, ByVal ReadBuffer As String, ByVal BufferSize As Short, ByRef ActReadLen As Integer) As Integer

    Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Short) As Integer

    'bus functions
    Declare Function atxml_GetSingleIntValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef IntValue As Short) As Integer
    Declare Function atxml_GetIntArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal IntArrayPtr As Short, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetSingleDblValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef DblValue As Double) As Integer
    Declare Function atxml_GetDblArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal DblArrayPtr As Double, ByVal ArraySize As Short) As Integer
    Declare Function atxml_GetStringValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal StrPtr As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" (ByVal SignalDescription As String, ByVal Response As String, ByVal BufferSize As Short) As Integer

    'AtXmlDriverFunc.def
    Declare Function atxmlDF_viSetAttribute Lib "C:\Program Files\VIPERT\ISS\bin\AtxmlDriverFunc.DLL" (ByVal ResourceName As String, ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer

    Declare Function atxmlDF_viOut16 Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByVal val16 As Short) As Integer


    Declare Function atxmlDF_viStatusDesc Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal status As Integer, ByVal desc As String) As Integer

    Declare Function atxmlDF_zt1428_read_waveform Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal Source As Integer, ByVal TransferType As Integer, ByRef WaveFormArray As Double, ByRef NumberOfPoints As Integer, ByRef AcquisitionCount As Integer, ByRef SampleInterval As Double, ByRef TimeOffset As Double, ByRef XREFERENCE As Integer, ByRef VoltIncrement As Double, ByRef VoltOffset As Double, ByRef YREFERENCE As Integer) As Integer

    Declare Function atxmlDF_eip_selectCorrectionsTable Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal FilePath As String, ByVal TableName As String) As Integer

    Declare Function atxmlDF_eip_setCorrectedPower Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal instrSession As Integer, ByVal Frequency As Double, ByVal Power As Single) As Integer

    Declare Function atxmlDF_viClear Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer) As Integer

    Declare Function atxmlDF_viWrite Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    Declare Function atxmlDF_viRead Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    Declare Function atxmlDF_viIn16 Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal spaces As Short, ByVal offset As Integer, ByRef val16 As Short) As Integer

    Declare Function atxmlDF_viOpen Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal sesn As Integer, ByVal name As String, ByVal timeout As Integer, ByRef vi As Integer) As Integer

    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{F0036371-9B45-4d60-839B-4B94CEA259CE}"
    Public Const proctype As String = "SFP" ' selftest??
    Public Const conNoDLL As Short = 48

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
    Public Const VI_SUCCESS As Integer = &H0
    Public Const VI_ATTR_TMO_VALUE As Integer = &H3FFF001A
    Public Const VI_ERROR_RSRC_NFOUND As Integer = &HBFFF0011
    Public Const VI_ERROR_TMO As Integer = &HBFFF0015
    Public Const VI_A16_SPACE As Short = 1
    Public Const VI_READ_BUF As Short = 1
    Public Const VI_WRITE_BUF As Short = 2

    ' - Resource Template Functions and Operations ----------------------------
    Declare Function viOpenDefaultRM Lib "VISA32.DLL" Alias "#141" (ByRef sesn As Integer) As Integer
    Declare Function viOpen Lib "VISA32.DLL" Alias "#131" (ByVal sesn As Integer, ByVal viDesc As String, ByVal mode As Integer, ByVal timeout As Integer, ByRef vi As Integer) As Integer
    Declare Function viClose Lib "VISA32.DLL" Alias "#132" (ByVal vi As Integer) As Integer
    Declare Function viGetAttribute Lib "VISA32.DLL" Alias "#133" (ByVal vi As Integer, ByVal attrName As Integer, ByRef attrValue As String) As Integer
    Declare Function viSetAttribute Lib "VISA32.DLL" Alias "#134" (ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer
    Declare Function viStatusDesc Lib "VISA32.DLL" Alias "#142" (ByVal vi As Integer, ByVal status As Integer, ByVal desc As String) As Integer
    Declare Function viReadSTB Lib "VISA32.DLL" Alias "#259" (ByVal vi As Integer, ByRef status As Short) As Integer
    'Declare Function viClear Lib "VISA32.DLL" Alias "#260" (ByVal vi As Long) As Long

    ' - Basic I/O Operations --------------------------------------------------
    Declare Function viRead Lib "VISA32.DLL" Alias "#256" (ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function viWrite Lib "VISA32.DLL" Alias "#257" (ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer
    Declare Function viFlush Lib "VISA32.DLL" Alias "#268" (ByVal vi As Integer, ByVal mask As Short) As Integer

    ' - Memory I/O Operations -------------------------------------------------
    Declare Function viIn16 Lib "VISA32.DLL" Alias "#261" (ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByRef val16 As Short) As Integer
    'Declare Function viOut16 Lib "VISA32.DLL" Alias "#262" (ByVal vi As Long, ByVal accSpace As Integer, ByVal offset As Long, ByVal val16 As Integer) As Long
End Module