'Option Strict Off
'Option Explicit On

Imports System
Imports System.Text
Imports System.Data.OleDb
Imports Microsoft.VisualBasic
Imports DAO
Imports Microsoft.Win32

Public Module DLLMain


    '=========================================================
    '**********************************************************************
    '***    ManTech Test Systems                                        ***
    '***                                                                ***
    '***    Nomenclature:   Module "General" : FHDB DLL                 ***
    '***    Version:        1.2   Release 8.0                           ***
    '***    Written By:     Dave Joiner                                 ***
    '***    Last Update:    09/28/01                                    ***
    '***    Purpose:                                                    ***
    '***    Module to hold Variables for the FHDB DLL.                  ***
    '***    This DLL allows a C NAM application to add a record to      ***
    '***    FHDB, Returning an Error Code.                              ***
    '***    Data is checked against a default value to verifiy data     ***
    '***    was sent. Only Fields that contain Data are updated.        ***
    '***    If Comments were added, A TimeDate Stamp and User Name      ***
    '***    are added to the first line.                                ***
    '***    It also incorporates a conversion Function to convert       ***
    '***    Instrument time into a windows format.                      ***
    '***                                                                ***
    '***    Change History:                                             ***
    '***    --- Version:        1.1   Release 7 Patch 2 ---             ***
    '***    Dave Joiner    04/10/2001                                   ***
    '***    Added ConvertFileName, FileExists, and StripNullCharacters  ***
    '***    to support the M910NAM. Modifications test the              ***
    '***    Fault Callout Data for a .IDE file extention. If it exsits  ***
    '***    it will be replaced with a .DIA file extension, Opened and  ***
    '***    dumped into the Fault Callout Data Field.                   ***
    '***    Note: The .IDE file is created from the .DIA file and       ***
    '***    contains embeded SGML Tags.                                 ***
    '***    --- Version:        1.2   Release 8.0 ---                   ***
    '***    Dave Joiner    09/28/2001                                   ***
    '***    Modified Operator Comments field, Took out automatic        ***
    '***    Time/Date/Operator Comment header on each comment entered.  ***
    '***    Changed the Operator Comments Default value from "NONE" to  ***
    '***    an empty String.                                            ***
    '**********************************************************************



    'Dimension DAO Database Objects
    Public wrkJet As DAO.Workspace
    
    Public rstFaults As DAO.Recordset
    Public dbsFHDB As DAO.Database

    'Dimension Global Level Variables
    Public dtStartTime As Date
    Public dtStopTime As Date
    Public sERO As String 'character length * 5
    Public sTPCCN As String 'character length * 16
    Public sUUTSerial As String 'character length * 15
    Public sUUTRev As String 'character length * 10
    Public sIDSerial As String 'character length * 10
    Public bTestStatus As Boolean
    Public sFailureStep As String 'character length * 10
    Public sFaultCallout As String 'character length * 256
    Public dMeasValue As Double
    Public sDimension As String 'character length * 12
    Public dUpperLimit As Double
    Public dLowerLimit As Double
    Public sOperatorComments As String 'character length * 256
    Public dTemperature As Double
    Public sTemperature As String
    Public lErrReturn As Integer
    Public lErrNumber As Integer
    Public lErrTemp As Integer
    Public sDBFile As String
    'For M910 Support   'DJoiner  04/10/2001
    Public sFileName As String

    'Requires (3) Windows Api Functions
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer

    'Set Constants to identify Database Location and Table Name
    'Public Const DBFile = "C:\VIPERT\FHDB\Database\FHDB.mdb"
    Public Const DBTable As String = "FAULTS"
    'For M910 Support   'DJoiner  04/10/2001
    Public Const IDE_EXT As String = ".IDE"
    Public Const DIA_EXT As String = ".DIA"

    'Set default Values
    
    Public Const dtDefault As Date = #12:00:00 PM#
    Public Const dDefault As Short = 0
    Public Const sDefault As String = " "
    Public Const bDefault As Boolean = False
    Public Const lDefault As Short = 0


    Public Sub Main()
        '**************************************************************************
        '***    Initialize variables on start up.                               ***
        '***                                    Dave Joiner     03/12/2001      ***
        '**************************************************************************

        dtStartTime = dtDefault
        dtStopTime = dtDefault
        sERO = sDefault
        sTPCCN = sDefault
        sUUTSerial = sDefault
        sUUTRev = sDefault
        sIDSerial = sDefault
        bTestStatus = bDefault
        sFailureStep = sDefault
        sFaultCallout = sDefault
        dMeasValue = dDefault
        sDimension = sDefault
        dUpperLimit = dDefault
        dLowerLimit = dDefault
        sOperatorComments = sDefault
        dTemperature = dDefault
        sTemperature = sDefault
        lErrReturn = lDefault
        lErrNumber = lDefault
        lErrTemp = lDefault

        sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")

    End Sub



    Public Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the VIPERT.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in VIPERT.INI File   *
        '*     lpKeyName$ - KEYNAME= in VIPERT.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************


        
        Dim lpReturnedString As New StringBuilder(255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        nSize = 255
        FileNameInfo = ""
        'Find File Locations
        ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function









End Module