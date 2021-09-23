Attribute VB_Name = "CiclLib"
'**************************************************************
'* Nomenclature   : VIPER/T SYSTEM SELF TEST                  *
'*                  Common Instrument Control Layer           *
'* Version        : 1.0                                       *
'* Last Update    :                                           *
'* Purpose        : This module is the CICL include file.     *
'*                  It replaces the Visa32.bas file.          *
'**************************************************************

Option Explicit
DefInt A-Z

'----------------- VXIplug&play Driver Declarations ---------
Declare Function atxml_Initialize Lib "C:\Program Files\VIPERT\ISS\Bin\AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Long

Declare Function atxml_Close Lib "AtXmlApi.dll" () As Long

Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" _
                         (ByVal TestRequirements As String, _
                          ByVal Allocation As String, _
                          ByVal Availability As String, _
                          ByVal BufferSize As Integer) As Long

Declare Function atxml_WriteCmds Lib "AtXmlApi.dll" _
                        (ByVal ResourceName As String, _
                         ByVal InstrumentCmds As String, _
                         ActWriteLen As Long) As Long

Declare Function atxml_ReadCmds Lib "AtXmlApi.dll" _
                        (ByVal ResourceName As String, _
                         ByVal ReadBuffer As String, _
                         ByVal BufferSize As Integer, _
                         ByRef ActReadLen As Long) As Long
                         
Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" _
                         (ByVal XmlBuffer As String, _
                          ByVal Response As String, _
                          ByVal BufferSize As Integer) As Long

                         
                         
'bus functions
Declare Function atxml_GetSingleIntValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef IntValue As Integer) As Long
Declare Function atxml_GetIntArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, _
                                                              ByVal IntArrayPtr As Integer, ByVal ArraySize As Integer) As Long
Declare Function atxml_GetSingleDblValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByRef DblValue As Double) As Long
Declare Function atxml_GetDblArrayValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, _
                                                              ByVal DblArrayPtr As Double, ByVal ArraySize As Integer) As Long
Declare Function atxml_GetStringValue Lib "AtXmlApi.dll" (ByVal Response As String, ByVal Attr As String, ByVal StrPtr As String, ByVal BufferSize As Integer) As Long
                          
Declare Function atxml_IssueSignal Lib "AtXmlApi.dll" (ByVal SignalDescription As String, ByVal Response As String, ByVal BufferSize As Integer) As Long
                         
                         
'AtXmlDriverFunc.def
Declare Function atxmlDF_viSetAttribute Lib "C:\Program Files\VIPERT\ISS\bin\AtxmlDriverFunc.DLL" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal attrName As Long, _
                         ByVal attrValue As Long) As Long
                        
Declare Function atxmlDF_viOut16 Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal accSpace As Integer, _
                         ByVal offset As Long, _
                         ByVal val16 As Integer) As Long
                        
                        
Declare Function atxmlDF_viStatusDesc Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal Status As Long, _
                         ByVal desc As String) As Long
                         
                      
Declare Function atxmlDF_zt1428_read_waveform Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal source As Long, _
                         ByVal transferType As Long, _
                         waveformArray#, _
                         ByRef NumberOfPoints As Long, _
                         ByRef acquisitionCount As Long, _
                         ByRef sampleInterval As Double, _
                         ByRef timeOffset As Double, _
                         ByRef xReference As Long, _
                         ByRef voltIncrement As Double, _
                         ByRef voltOffset As Double, _
                         ByRef yReference As Long) As Long
                         
Declare Function atxmlDF_eip_selectCorrectionsTable Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal FilePath As String, _
                         ByVal TableName As String) As Long

Declare Function atxmlDF_eip_setCorrectedPower Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal instrSession As Long, _
                         ByVal Frequency As Double, _
                         ByVal Power!) As Long

Declare Function atxmlDF_viClear Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long) As Long

Declare Function atxmlDF_viWrite Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal buffer As String, _
                         ByVal count As Long, _
                         retCount As Long) As Long
                         
Declare Function atxmlDF_viRead Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal buffer As String, _
                         ByVal count As Long, _
                         retCount As Long) As Long
                         
Declare Function atxmlDF_viIn16 Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal vi As Long, _
                         ByVal Space As Integer, _
                         ByVal offset As Long, _
                         val16 As Integer) As Long

Declare Function atxmlDF_viOpen Lib "AtXmlDriverFunc.dll" _
                        (ByVal ResourceName As String, _
                         ByVal sesn As Long, _
                         ByVal name As String, _
                         ByVal timeout As Long, _
                         vi As Long) As Long


'-----------------Init and GUID ---------------------------------------
Global Const guid = "{F0036371-9B45-4d60-839B-4B94CEA259CE}"
Global Const proctype = "SFP"
Global Const conNoDLL = 48

Global Const VI_NULL = 0
Global Const VI_TRUE = 1
Global Const VI_FALSE = 0
Global Const VI_SUCCESS = &H0&
Global Const VI_ATTR_TMO_VALUE = &H3FFF001A
Global Const VI_ERROR_RSRC_NFOUND = &HBFFF0011
Global Const VI_ERROR_TMO = &HBFFF0015
Global Const VI_A16_SPACE = 1
Global Const VI_READ_BUF = 1
Global Const VI_WRITE_BUF = 2


' - Resource Template Functions and Operations ----------------------------
Declare Function viOpenDefaultRM Lib "VISA32.DLL" Alias "#141" (sesn As Long) As Long
Declare Function viOpen Lib "VISA32.DLL" Alias "#131" (ByVal sesn As Long, ByVal viDesc As String, ByVal mode As Long, ByVal timeout As Long, vi As Long) As Long
Declare Function viClose Lib "VISA32.DLL" Alias "#132" (ByVal vi As Long) As Long
Declare Function viGetAttribute Lib "VISA32.DLL" Alias "#133" (ByVal vi As Long, ByVal attrName As Long, attrValue As Any) As Long
Declare Function viSetAttribute Lib "VISA32.DLL" Alias "#134" (ByVal vi As Long, ByVal attrName As Long, ByVal attrValue As Long) As Long
Declare Function viStatusDesc Lib "VISA32.DLL" Alias "#142" (ByVal vi As Long, ByVal Status As Long, ByVal desc As String) As Long
Declare Function viReadSTB Lib "VISA32.DLL" Alias "#259" (ByVal vi As Long, Status As Integer) As Long

' - Basic I/O Operations --------------------------------------------------
Declare Function viRead Lib "VISA32.DLL" Alias "#256" (ByVal vi As Long, ByVal buffer As String, ByVal count As Long, retCount As Long) As Long
Declare Function viWrite Lib "VISA32.DLL" Alias "#257" (ByVal vi As Long, ByVal buffer As String, ByVal count As Long, retCount As Long) As Long
Declare Function viFlush Lib "VISA32.DLL" Alias "#268" (ByVal vi As Long, ByVal mask As Integer) As Long

' - Memory I/O Operations -------------------------------------------------
Declare Function viIn16 Lib "VISA32.DLL" Alias "#261" (ByVal vi As Long, ByVal accSpace As Integer, ByVal offset As Long, val16 As Integer) As Long

'ztec scope dll used to get waveform block in Oscope.AcqWaveform
Declare Function zt1428_read_waveform Lib "ZT1428.DLL" (ByVal instrumentHandle&, ByVal source&, ByVal transferType, ByRef waveformArray() As Double, number_ofPoints&, acquisitionCount&, sampleInterval As Double, timeOffset As Double, xReference&, voltIncrement As Double, voltOffset As Double, yReference&) As Long


