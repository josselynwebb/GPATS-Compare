'Option Strict Off
'Option Explicit On

Imports System

Public Module CiclLib


    '----------------- VXIplug&play Driver Declarations ---------
    Declare Function atxml_Initialize Lib "AtXmlApi.dll" (ByVal proctype As String, ByVal guid As String) As Integer

    Declare Function atxml_Close Lib "AtXmlApi.dll"  As Integer

    Declare Function atxml_ValidateRequirements Lib "AtXmlApi.dll" (ByVal TestRequirements As String, ByVal Allocation As String, ByVal Availability As String, ByVal BufferSize As Short) As Integer

    Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Short) As Integer



    'bus functions
    Declare Function atxmlDF_viWrite Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    Declare Function atxmlDF_viRead Lib "AtXmlDriverFunc.dll" (ByVal ResourceName As String, ByVal vi As Integer, ByVal buffer As String, ByVal count As Integer, ByRef retCount As Integer) As Integer

    '-----------------Init and GUID ---------------------------------------
    Public Const guid As String = "{F0036371-9B45-4d60-839B-4B94CEA259CE}"
    Public Const proctype As String = "SFP" ' selftest??

    'Vxi stuff that I am replacing as I can

    Public Const VI_SUCCESS As Short = &H0
    Public Const VI_ATTR_TMO_VALUE As Integer = &H3FFF001A

    ' - Resource Template Functions and Operations ----------------------------
    Declare Function viClose Lib "VISA32.DLL" Alias "#132" (ByVal vi As Integer) As Integer
    Declare Function viSetAttribute Lib "VISA32.DLL" Alias "#134" (ByVal vi As Integer, ByVal attrName As Integer, ByVal attrValue As Integer) As Integer
    Declare Function viReadSTB Lib "VISA32.DLL" Alias "#259" (ByVal vi As Integer, ByRef status As Short) As Integer

    ' - Memory I/O Operations -------------------------------------------------
    Declare Function viIn16 Lib "VISA32.DLL" Alias "#261" (ByVal vi As Integer, ByVal accSpace As Short, ByVal offset As Integer, ByRef val16 As Short) As Integer


End Module