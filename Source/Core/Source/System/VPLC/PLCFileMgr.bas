Attribute VB_Name = "Module1"
        
Public Const SAIF_PLC_DATA = 0
Public Const RECIEVER_PLC_DATA = 1
Public Const S901_2 = 2
Public Const S901_3 = 3
Public Const S901_4 = 4
Public Const S901_5 = 5
Public Const S901_6 = 6
Public Const S901_7 = 7
Public Const S902_2 = 8
Public Const S902_3 = 9
Public Const S902_4 = 10
Public Const S902_5 = 11
Public Const S902_6 = 12
Public Const S902_7 = 13
Public Const S903_2 = 14
Public Const S903_3 = 15
Public Const S903_4 = 16
Public Const S903_5 = 17
Public Const S903_6 = 18
Public Const S903_7 = 19
Public Const S904_2 = 20
Public Const S904_3 = 21
Public Const S904_4 = 22
Public Const S904_5 = 23
Public Const S904_6 = 24
Public Const S904_7 = 25
Public Const S905_2 = 26
Public Const S905_3 = 27
Public Const S905_4 = 28
Public Const S905_5 = 29
Public Const S905_6 = 30
Public Const S905_7 = 31
Public Const S906_2 = 32
Public Const S906_3 = 33
Public Const S906_4 = 34
Public Const S906_5 = 35
Public Const S906_6 = 36
Public Const S906_7 = 37

'---------------- CPLCFileMgr Declarations ----------------------
'Declare Function readPLCFile_if Lib "..\..\..\target\PLCFileMgr.dll" () As Long
Declare Function readPLCFile_if Lib "..\..\..\target\PLCFileMgr.dll" () As Long
Declare Function savePLCData_if Lib "..\..\..\target\PLCFileMgr.dll" () As Long
Declare Function setPLCPathData_if Lib "..\..\..\target\PLCFileMgr.dll" (ByVal path As Long, ByVal freqMHz As Single, ByVal CF_dBm As Single) As Long
Declare Function getPLCData_if Lib "..\..\..\target\PLCFileMgr.dll" (ByVal path As Long, ByVal freqMHz As Single, ByRef CF_dBm As Single) As Long
Declare Function exportToExcel_if Lib "..\..\..\target\PLCFileMgr.dll" (ByVal filePath As String) As Long
Declare Function getError_if Lib "..\..\..\target\PLCFileMgr.dll" (ByRef ErrorCode As Long, ByRef ErrorSeverity As Long, ByRef ErrorDesc As String, _
                                                    ByVal EDLen As Long, ByRef MoreErrorInfo As String, ByVal MEI As Long) As Long
Declare Function exportSplinesToExcel_if Lib "..\..\..\target\PLCFileMgr.dll" (ByVal filePath As String) As Long

Public Const cPLCFILEMGR_ERROR = "PLC File Manager Error."


