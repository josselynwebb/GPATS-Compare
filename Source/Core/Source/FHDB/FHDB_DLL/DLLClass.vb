'Option Explicit On

Imports System
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports DAO

Public Class DLLClass


	'=========================================================
    '**************************************************************************
    '***    ManTech Test Systems                                            ***
    '***                                                                    ***
    '***    Nomenclature:   Class Module "DLLClass" : FHDB DLL              ***
    '***    Version:        1.2                                             ***
    '***    Written By:     Dave Joiner                                     ***
    '***    Last Update:    09/28/01                                        ***
    '***    Purpose:                                                        ***
    '***    Class Module to Update Database from values passed              ***
    '***    from a C NAM, CTest, STest, or SysMon.                          ***
    '***    This DLL references Microsoft DAO 2.5/3.51 Compatability        ***
    '***    Object Library.                                                 ***
    '***                                                                    ***
    '**************************************************************************


    Friend DAODBEngine_definst As New DAO.DBEngine

    'M910 Modifications                     'DJoiner  04/10/2001
    Public Sub ConvertFileName()
        '**********************************************************************
        '***     If the text file is passed it will have an .IDE extension. ***
        '***     This is the file that is created by the M910NAM to display ***
        '***     the Fault Callout to the Operator prior to Writing the     ***
        '***     record. This will give the Operator an opportunity to add  ***
        '***     Comments after analyzing the Faults. Since the .IDE file   ***
        '***     contains SGML tags, the contents of the .DIA file needs    ***
        '***     dumped into the Fault Callout Data Field.                  ***
        '***     Test the Fault Callout value for a ".IDE" extension.       ***
        '***     If the extension id .IDE then replace the .IDE with        ***
        '***     with the ".DIA" file extension. This is the text file      ***
        '***     without the SGML Tags.                                     ***
        '***                                Dave Joiner     04/10/2001      ***
        '**********************************************************************

        
        Dim X As Integer
        Dim sFileTemp As String = ""

        For X = 1 To Len(sFileName)
            If Asc(Mid(sFileName, X, 1))=46 Then 'Find "."
                If UCase(Mid(sFileName, X, 4))=IDE_EXT Then 'If an .IDE extension
                    sFileName = sFileTemp & DIA_EXT 'Replace with .DIA Extension
                    Exit Sub
                End If
            End If
            sFileTemp &= UCase(Mid(sFileName, X, 1))
        Next

    End Sub


    'M910 Modifications                     'DJoiner  04/10/2001
    
    Function FileExists(ByVal Path As String) As Short
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM MONITOR                   *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Checks To See If A Disk File Exists      *
        '*    EXAMPLE:                                              *
        '*     IsFileThere% = FileExists("C:\ISFILE.EX")            *
        '*    RETURNS:                                              *
        '*     TRUE if File is present.                             *
        '*     False if File is not present.                        *
        '************************************************************
        Dim X As Integer

        X = FreeFile()

        On Error Resume Next
        FileOpen(X, Path, OpenMode.Input)
        If CompareErrNumber("=", 0) Then
            FileExists = True
        Else
            FileExists = False
        End If
        FileClose(X)

    End Function


    Public Sub Return_Err_Code()
        '**************************************************************************
        '***    Routine to convert DAO Error number into an Error Category      ***
        '***    Number to return to the calling C NAM.                          ***
        '***                        Dave Joiner     02/21/2001                  ***
        '**************************************************************************

        Select Case lErrNumber
                'Operation Succesful
            Case 0:
                lErrReturn = 0

                'Invalid Data Passed
            Case 2421 To 3003, 3016 To 3018:
                lErrReturn = 1

                'Not Found - No Permission
            Case 3005 To 3023, 3024, 3028 To 3034:
                lErrReturn = 2

                'Corrupt Database
            Case 3049, 3056, 3176, 3180, 3340:
                lErrReturn = 3

                'Database Full
            Case 3025, 3026, 3183, 3465, 3472, 3036 To 3039:
                lErrReturn = 4

                'Not Found - No Permission
                'Any other Error

            Case Else
                lErrReturn = 2

        End Select

        If lErrTemp>lErrReturn Then
            lErrReturn = lErrTemp
        End If

    End Sub


    
    
    Public Function ReturnDateTime(ByVal vGetInstrumentTime As String) As Date
        '**************************************************************************
        '***    Function to parse 15 character Date/Time into a windows         ***
        '***    Instrument format = "YYMMDDhhmmssss"                            ***
        '***    System definable format (24 hour time).                         ***
        '***                    Dave Joiner     02/21/2001                      ***
        '**************************************************************************

        
        Dim vTimeConverted As String 'Date/Time Return Variable
        Dim vConvertingTime As Object 'Date/Time Character to convert
        
        Dim iDigit As Integer 'Date/Time string Digit Counter
        Dim sYear As String = "" 'Years Place holder
        Dim sMonth As String = "" 'Months Place holder
        Dim sDay As String = "" 'Days Place holder
        Dim sHour As String = "" 'Hours Place holder
        Dim sMinute As String = "" 'Minutes Place holder
        Dim sSecond As String = "" 'Seconds Place holder


        Try ' On Error GoTo ErrLabel

            vTimeConverted = "" 'Reset TimeConvered value
            If Convert.ToString(vGetInstrumentTime).Length=0 Then 'Test for a value to convert
                Exit Function
            End If

            'loop through value from 1 to the length of value
            For iDigit = 1 To Convert.ToString(vGetInstrumentTime).Length

                Select Case iDigit

                    Case 1, 2:
                        sYear += Mid(vGetInstrumentTime, iDigit, 1)

                    Case 3, 4:
                        sMonth += Mid(vGetInstrumentTime, iDigit, 1)

                    Case 5, 6:
                        sDay += Mid(vGetInstrumentTime, iDigit, 1)

                    Case 7, 8:
                        sHour += Mid(vGetInstrumentTime, iDigit, 1)

                    Case 9, 10:
                        sMinute += Mid(vGetInstrumentTime, iDigit, 1)

                    Case 11, 12:
                        sSecond += Mid(vGetInstrumentTime, iDigit, 1)

                End Select

                'Disregard any characters over 12 digits in length
                If iDigit>12 Then
                    Exit For
                End If
            Next

            vTimeConverted = sMonth & "/" & sDay & "/" & sYear & " " & sHour & ":" & sMinute & ":" & sSecond
            'Format converted Date/Time and Return in 24 hour format
            ReturnDateTime = VB6.Format(vTimeConverted, "MM/DD/YY h:mm:Ss")

            Exit Function 'Prevent from dropping through the ErrLabel

        Catch	' ErrLabel:
            Err.Number = lErrNumber
            Err.Clear()
            ResumeNext()

        End Try
    End Function


    
    Public Function SaveData(ByVal StartTime As String, ByRef StopTime As String, ByVal ERO As String, ByVal TPCCN As String, ByVal UUTSerialNumber As String, ByVal UUTRevision As String, ByVal IDSerialNumber As String, ByVal TestStatus As Integer, ByVal FailureStep As String, ByVal FaultCallout As String, ByVal MeasuredValue As Double, ByVal Dimension As String, ByVal UpperLimit As Double, ByVal LowerLimit As Double, ByRef OperatorComments As String) As Integer
        '**************************************************************************
        '***        Function to Get Data passed from C NAM and return           ***
        '***        an Error Code based on the DAO Error Collection.            ***
        '***        If no data is passed in a field, a Default value            ***
        '***        is assigned.                                                ***
        '***                                    Dave Joiner     02/21/2001      ***
        '**************************************************************************

        Try ' On Error GoTo ErrLabel

            If Len(StartTime)>5 Then 'Length is greater than 5 it may be a Date
                If IsDate(StartTime) Then 'If value is a Date,
                    dtStartTime = CDate(StartTime) 'convert to Date and Assign to Date variable.
                Else                    'If not a Date, Convert and Format, convert to
                    dtStartTime = CDate(ReturnDateTime(StartTime)) 'Date and Assign to Date variable.
                End If
            Else
                dtStartTime = dtDefault 'If not a date value then assign a default
            End If

            'If there is no StopTime, Assign the Default Value to the StopTime Date Variable
            If Len(StopTime)<6 Then 'If length is less than 6, It couldn't ba a Date
                StopTime = Convert.ToString(dtDefault) 'If not a date value then assign a default
            End If
            If IsDate(StopTime) Then 'If value is a Date,
                dtStopTime = CDate(StopTime) 'convert to Date and Assign to Date variable.
            Else                'If not a Date, Convert and Format, convert to
                dtStopTime = CDate(ReturnDateTime(StopTime)) 'Date and Assign to Date variable.
            End If

            'Assign values to Global String Variables
            'Check for a value, if none assign an empty string
            'If ERO Then sERO = ERO Else sERO = sDefault 
            sERO = ERO
            'If TPCCN Then sTPCCN = TPCCN Else sTPCCN = sDefault
            sTPCCN = TPCCN
            'If UUTSerialNumber Then sUUTSerial = UUTSerialNumber Else sUUTSerial = sDefault
            sUUTSerial = UUTSerialNumber
            'If UUTRevision Then sUUTRev = UUTRevision Else sUUTRev = sDefault
            sUUTRev = UUTRevision
            'If IDSerialNumber Then sIDSerial = IDSerialNumber Else sIDSerial = sDefault
            sIDSerial = IDSerialNumber
            'If FailureStep Then sFailureStep = FailureStep Else sFailureStep = sDefault
            sFailureStep = FailureStep

            '********************************************************************************
            'M910 Modifications                     'DJoiner  04/10/2001
            'If FaultCallout has a .dia file name, dump text into sFaultCallout
            'and Return Record Number

            If FaultCallout <> "" Then
                sFaultCallout = Trim(StripNullCharacters(FaultCallout))
                sFileName = sFaultCallout 'Add FileName to end of Path

                ConvertFileName()

                If FileExists(sFileName) Then
                    FileOpen(1, sFileName, OpenMode.Input) 'Open .dia file
                    Input(vbDefaultButton1, sFaultCallout) '#1) 'Dump Contents of file into sFaultCallout
                    FileClose(1) 'Close file and Exit
                End If
            Else
                sFaultCallout = sDefault
            End If

            '********************************************************************************

            'If Dimension Then 
            sDimension = Dimension 'Else sDimension = sDefault

            'Check for a Comment Entered, if not assign a Default of " "
            OperatorComments = Trim(StripNullCharacters(OperatorComments))
            If Len(OperatorComments) > 0 Then 'If string length is > 0, a comment was entered.
                sOperatorComments = OperatorComments
            Else
                sOperatorComments = " " 'If not, set to Default
            End If

            'Convert to Boolan Data Type and assign to Global Boolean variable
            bTestStatus = CBool(TestStatus)

            'Convert to Double Data Type and assign to Global Double variable
            dMeasValue = MeasuredValue
            dUpperLimit = UpperLimit
            dLowerLimit = LowerLimit

            WriteToDB() 'Call routine to Add/Update a record

            SaveData = lErrReturn 'Return Error Code

            Exit Function 'Prevent program from flowing through Error Handler

        Catch   ' ErrLabel:
            Err.Number = lErrNumber
            Err.Clear()
            ResumeNext()

        End Try
    End Function


    'M910 Modifications                     'DJoiner  04/10/2001
    Function StripNullCharacters(ByVal Parsed As String) As String
        StripNullCharacters = ""
        'DESCRIPTION:
        '   This routine strips characters with ASCII values less than 32 from the
        '   end of a string
        'PARAMTERS:
        '   Parsed$ = String from which to remove null characters
        'RETURNS:
        '   A the resultant parsed string
        
        Dim X As Integer

        For X = Convert.ToString(Parsed).Length To 1 Step -1
            If Asc(Mid(Convert.ToString(Parsed), X, 1))>32 And Asc(Mid(Convert.ToString(Parsed), X, 1))<127 Then
                Exit For
            End If
        Next X
        StripNullCharacters = Strings.Left(Convert.ToString(Parsed), X)
    End Function


    Public Sub WriteToDB()
        '**************************************************************************
        '***            Read record and compare with Default Data               ***
        '***            If Data <> to Default then Update that Field and        ***
        '***            reset Global variable to Default.                       ***
        '***                    Dave Joiner     02/21/2001                      ***
        '**************************************************************************

        Try ' On Error GoTo ErrLabel
            'Open database and fill recordset (Reference: Microsoft DAO 3.6 Object Library)
            wrkJet = DAODBEngine_definst.Workspaces(0)
            dbsFHDB = wrkJet.OpenDatabase(sDBFile) 'Open Database
            rstFaults = dbsFHDB.OpenRecordset(DBTable, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset
            If rstFaults.RecordCount<>0 Then
                rstFaults.MoveLast() 'Move to last Record in table
            End If

            rstFaults.AddNew() 'Add a new record

            'Assign Values to Data Objects
            'If there is no value, don't assign value, use Default
            'If there is a value, compare to Default, If different, Assign New Value
            If dtStartTime.ToOADate<>dtDefault.ToOADate Then
                rstFaults.Fields(1).Value = dtStartTime
            End If

            If dtStopTime.ToOADate<>dtDefault.ToOADate Then
                rstFaults.Fields(2).Value = dtStopTime
            End If
            '==========================================================================
            dtStopTime = dtDefault 'Reset Stop Time Variable
            dtStartTime = dtDefault 'Reset Start Time Variable
            '==========================================================================

            If sERO<>"" Then
                rstFaults.Fields(3).Value = sERO
                sERO = sDefault
            End If
            If sTPCCN<>"" Then
                rstFaults.Fields(4).Value = sTPCCN
                sTPCCN = sDefault
            End If
            If sUUTSerial<>"" Then
                rstFaults.Fields(5).Value = sUUTSerial
                sUUTSerial = sDefault
            End If
            If sUUTRev<>"" Then
                rstFaults.Fields(6).Value = sUUTRev
                sUUTRev = sDefault
            End If
            If sIDSerial<>"" Then
                rstFaults.Fields(7).Value = sIDSerial
                sIDSerial = sDefault
            End If
            If bTestStatus<>False Then
                rstFaults.Fields(8).Value = bTestStatus
                bTestStatus = bDefault
            End If
            If sFailureStep<>"" Then
                rstFaults.Fields(9).Value = sFailureStep
                sFailureStep = sDefault
            End If
            If sFaultCallout<>"" Then
                rstFaults.Fields(10).Value = sFaultCallout
                sFaultCallout = sDefault
            End If
            If dMeasValue <> 0 Then
                rstFaults.Fields(11).Value = Convert.ToString(dMeasValue)
                dMeasValue = dDefault
            End If
            If sDimension<>"" Then
                rstFaults.Fields(12).Value = sDimension
                sDimension = sDefault
            End If
            If dUpperLimit<>0 Then
                rstFaults.Fields(13).Value = Convert.ToString(dUpperLimit)
                dUpperLimit = dDefault
            End If
            If dLowerLimit<>0 Then
                rstFaults.Fields(14).Value = Convert.ToString(dLowerLimit)
                dLowerLimit = dDefault
            End If

            rstFaults.Fields(15).Value = sOperatorComments

            sTemperature = GatherIniFileInformation("System Monitor", "PRI_TEMP", "")
            dTemperature = CDbl(sTemperature)

            If dTemperature<>0 Then
                rstFaults.Fields(16).Value = dTemperature
                dTemperature = dDefault
            End If

            rstFaults.Update() 'Update Record
            rstFaults.Close() 'Close DB connection and Release Object from Memory
            dbsFHDB.Close()

            Exit Sub 'Prevent from dropping through the ErrLabel

        Catch	' ErrLabel:
            Err.Number = lErrNumber
            Err.Clear()
            ResumeNext()

        End Try
    End Sub


    Public Sub New()
        Main()
    End Sub
End Class