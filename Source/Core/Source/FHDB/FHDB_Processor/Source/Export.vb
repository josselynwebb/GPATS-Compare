
Imports Microsoft.VisualBasic.FileIO
Imports System.IO

Module m_Export
    '**********************************************************************
    '***    United States Marine Corps                                  ***
    '***                                                                ***
    '***    Nomenclature:   Module "m_Export" : FHDB_Processor          ***
    '***    Written By:     Dave Joiner                                 ***
    '***    Purpose:                                                    ***
    '***  ECP-TETS-023                                                  ***
    '***    General Module to support the Export Form.                  ***
    '***    The Exporter will allow the Operator to Export              ***
    '***    Files from the FHDB. Both the Faults Table and the          ***
    '***    System Table are Exported as a Zipped version of the        ***
    '***    FHBD.MDB File.(EXPORT.zip) The Zipped file will span        ***
    '***    multiple disks if size dictates.                            ***
    '***    The System Table contains only one record that is either    ***
    '***    created or updated immediately pior to Export.              ***
    '***  2.1.0                                                         ***
    '***    Deleted Public dtCalDue As Date line in the header area of  ***
    '***    this file, on the 657 it was commented out anyway, it is not***
    '***    being used by the program.                                  ***
    '***    Changed all references to either TETS.INI or VIPERT.ini in  ***
    '***    any comments to Tester.ini                                  ***
    '***    Sub UpdateSystemRecord()                                    ***
    '***    Deleted the variable sControllerSN, added the variable      ***
    '***    sAteType.  The serial number of the controller isn't used   ***
    '***    for any data mining what so ever, however the Ate family is ***
    '***    of importance for data mining and this replaces it.         ***
    '***    Added the line to get the AteType from the ini file and     ***
    '***    deleted the reference to the Controller serial number.      ***
    '***    Added the verification code of the AteType value to the     ***
    '***    procedure.                                                  ***
    '***    Changed the message to display Tester.ini now instead of    ***
    '***    either TETS.ini or VIPERT.ini.                              ***
    '**********************************************************************


    'Dimension Public Variables
    Public sDatabase As String 'Drive/Path/File Name of the Database
    Public sExportFile As String 'Export File Drive/Path/Name
    Public bSystemRecordError As Boolean 'Flag to indicate the ATE Serial Number is invalid
    Public bDiskError As Boolean 'Flag to indicate the device is not in the USB Drive

    Const DblQts As String = """"


    Public Sub InitializeExportFields()
        '**********************************************************************
        '***             Initializes the File Export Fields.                ***
        '**********************************************************************

        frmExport.txtZipFilename.Text = sExportFile
        frmExport.txtFilesToProcess.Text = sDatabase
        frmExport.lstResults.Items.Clear() 'Clear the Results List Field

    End Sub


    Sub UpdateSystemRecord()
        '**********************************************************************
        '*** Procedure to either create or update System Information Record ***
        '**********************************************************************

        Dim sSysSN As String 'ATE Serial Number from the Tester's INI file
        Dim sAteType As String 'String value for the ATE family of tester
        Dim sSSV As String 'Software Revison Value from the Tester's INI file
        Dim sCalDue As String 'String value for the Date Effective
        Dim iRecCount As Short 'Total number of Records Returned from Query
        Dim slpBuffer As New VB6.FixedLengthString(255) '255 character Buffer for Computer Name
        Dim nRetVal As Integer 'Return value for Computer Name

        On Error GoTo ErrLabel

        bSystemRecordError = False 'Reset System Record Error flag

        'Get Info from Tester's INI file
        sDBFile = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
        sSysSN = GatherIniFileInformation("System Startup", "SN", "")
        sSSV = GatherIniFileInformation("System Startup", "SWR", "")
        sAteType = GatherIniFileInformation("System Startup", "ATE_TYPE", "")
        sCalDue = GatherIniFileInformation("Calibration", "SYSTEM_EFFECTIVE", "")

        bDBOpened = True
        wrkJet = DAODBEngine_definst.Workspaces(0) 'Open Workspace
        dbsFHDB = wrkJet.OpenDatabase(sDBFile) 'Open Database
        rstFaults = dbsFHDB.OpenRecordset(DB_SYSTEM_TABLE, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open Recordset

        rstFaults.MoveLast() 'Move to last Record
        iRecCount = rstFaults.RecordCount
        rstFaults.MoveFirst()
        'Limit SYSTEM Table to only one record
        If iRecCount = 0 Then 'If no records, Create New record
            rstFaults.AddNew()
        Else
            rstFaults.Edit() 'If Record exists, Edit it
        End If

        If Len(sCalDue) > 4 Then 'If > 4 Digits, it is proberly a date
            If IsNumeric(sCalDue) Then 'Check to see if value is Numeric
                If Len(sCalDue) > 18 Then 'If Field value Length is > 18, Truncate
                    sCalDue = Mid(sCalDue, 1, 18)
                End If
            Else
                sCalDue = "UNKNOWN" 'If there is no Date in Tester's INI
            End If 'Assign Default of "UNKNOWN"
        Else
            sCalDue = "UNKNOWN" 'If there is no Date in Tester's INI
        End If 'Assign Default of "UNKNOWN"

        '   Validate ATE family type
        If IsNumeric(sAteType) Then
            rstFaults.Fields("ATE_Identifer").Value = sAteType
        End If

        'Validate System Serial Number
        If IsNumeric(sSysSN) Then
            rstFaults.Fields("ATE_Serial").Value = sSysSN
        Else
            'Inform the Operator that the ATE Serial Number Invalid and terminate Operation.
            MsgBox("The ATE Serial number referenced in the Tester's INI file is invalid." & vbCrLf & "Please update the Proper Key in the Tester's INI file with a valid value." & vbCrLf & "The ATE Serial Number: " & sSysSN & " is invalid." & vbCrLf & "This Operation will be terminated.", MsgBoxStyle.Critical, "ATE Serial Number Invalid")
            bSystemRecordError = True
        End If

        'Assign Date to FHDB fields
        rstFaults.Fields("Sys_Software_Ver").Value = sSSV
        rstFaults.Fields("Cal_Due_Date").Value = sCalDue

        rstFaults.Update() 'Update Data in FHDB

        Call UpdateIniFile("FHDB", "LASTCHECKED", "") 'Update Last time checked key

        Call CloseDB()
        Exit Sub 'To prevent from falling through Error Handler

ErrLabel:
        If Err.Number = 3021 Then 'If No Current Record, ignore
            Resume Next
        Else
            MsgBox(Err.Description, MsgBoxStyle.Exclamation, "ERROR")
            Err.Clear()
            Resume Next
        End If

    End Sub

    Function Export_DB_To_CSV() As Boolean


        Dim Faults As DAO.Recordset
        Dim System As DAO.Recordset
        Dim objWriter As IO.StreamWriter

        wrkJet = DAODBEngine_definst.Workspaces(0) 'Open Workspace
        dbsFHDB = wrkJet.OpenDatabase(sDBFile) 'Open Database
        Try
            System = dbsFHDB.OpenRecordset(DB_SYSTEM_TABLE, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open SYSTEM TABLE
            objWriter = IO.File.CreateText(Path.GetDirectoryName(frmExport.txtZipFilename.Text) & "\SYSTEM.csv")
        Catch ex As UnauthorizedAccessException
            MsgBox("You do not have access permissions for your selected directory!", MsgBoxStyle.Exclamation, "Access Violation")
            dbsFHDB.Close()
            wrkJet.Close()
            Return False
        End Try

        Try
            objWriter.WriteLine("""Record_ID""" & "," & """ATE_Serial""" & "," & """Sys_Software_Ver""" & "," &
                               """Cal_Due_Date""" & "," & """ATE_Identifier""")
        Catch ex As Exception
            Dim style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            Dim msg = (String.Format("Error exporting the FHDB database: {0}", ex.Message))
            Dim title = "FHDB Export"
            MsgBox(msg, style, title)
            System.Close()
            Try
                objWriter.Close()
            Catch
            End Try
            dbsFHDB.Close()
            wrkJet.Close()
            Return False
        End Try

        Dim i As Integer = 0
        Dim pos As Integer = 0
        Dim buffer As String
        Dim fields(16) As String

        While i < System.RecordCount
            Dim systemField As DAO.Fields = System.Fields

            For j As Integer = 0 To 4
                buffer = systemField(j).Value
                buffer = Replace(buffer, ControlChars.Quote, ControlChars.Quote + ControlChars.Quote)
                buffer = buffer.Insert(0, ControlChars.Quote)
                buffer = buffer.Insert(buffer.Length, ControlChars.Quote)
                fields(j) = buffer
            Next

            Try
                objWriter.WriteLine(fields(0) & "," & fields(1) & "," & fields(2) & "," &
                                fields(3) & "," & fields(4))
            Catch ex As Exception
                Dim style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                Dim msg = (String.Format("Error exporting the FHDB database: {0}", ex.Message))
                Dim title = "FHDB Export"
                MsgBox(msg, style, title)
                System.Close()
                Try
                    objWriter.Close()
                Catch
                End Try
                dbsFHDB.Close()
                wrkJet.Close()
                Return False
            End Try

            System.MoveNext()
            i = i + 1

        End While

        Try
            objWriter.Close()
            System.Close()
        Catch ex As Exception
            Dim style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            Dim msg = (String.Format("Error exporting the FHDB database: {0}", ex.Message))
            Dim title = "FHDB Export"
            MsgBox(msg, style, title)
            dbsFHDB.Close()
            wrkJet.Close()
            Return False
        End Try

        i = 0

        Faults = dbsFHDB.OpenRecordset(DB_FAULT_TABLE, DAO.RecordsetTypeEnum.dbOpenDynaset) 'Open FAULTS TABLE
        objWriter = IO.File.CreateText((Path.GetDirectoryName(frmExport.txtZipFilename.Text) & "\FAULTS.csv"))
        Try
            objWriter.WriteLine("""Record_Identifier""" & "," & """Start_Time""" & "," & """Stop_Time""" & "," &
                                """ERO""" & "," & """TPCCN""" & "," & """UUT_Serial_No""" & "," &
                                """UUT_Rev""" & "," & """ID_Serial_No""" & "," & """Test_Status""" & "," &
                                """Failure_Step""" & "," & """Fault_Callout""" & "," & """Meas_Value""" & "," &
                                """Dimension""" & "," & """Upper_Limit""" & "," & """Lower_Limit""" & "," &
                                """Operator_Comments""")
        Catch ex As Exception
            Dim style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            Dim msg = (String.Format("Error exporting the FHDB database: {0}", ex.Message))
            Dim title = "FHDB Export"
            MsgBox(msg, style, title)
            Faults.Close()
            Try
                objWriter.Close()
            Catch
            End Try
            dbsFHDB.Close()
            wrkJet.Close()
            Return False
        End Try

        While i < Faults.RecordCount
            Dim faultField As DAO.Fields = Faults.Fields

            For k As Integer = 0 To 15
                Try
                    buffer = faultField(k).Value
                Catch ex As Exception
                    buffer = " "
                End Try
                If String.IsNullOrEmpty(buffer) Then
                    buffer = " "
                End If
                buffer = Replace(buffer, ControlChars.Quote, ControlChars.Quote + ControlChars.Quote)
                buffer = buffer.Insert(0, ControlChars.Quote)
                buffer = buffer.Insert(buffer.Length, ControlChars.Quote)
                fields(k) = buffer
            Next

            buffer = fields(0) & "," & fields(1) & "," & fields(2) & "," &
                                fields(3) & "," & fields(4) & "," & fields(5) & "," &
                                fields(6) & "," & fields(7) & "," & fields(8) & "," &
                                fields(9) & "," & fields(10) & "," & fields(11) & "," &
                                fields(12) & "," & fields(13) & "," & fields(14) & "," &
                                fields(15)

            buffer = Replace(buffer, vbCrLf, " ")
            Try
                objWriter.Write(buffer)
                objWriter.WriteLine()
            Catch ex As Exception
                Dim style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
                Dim msg = (String.Format("Error exporting the FHDB database: {0}", ex.Message))
                Dim title = "FHDB Export"
                MsgBox(msg, style, title)
                Try
                    objWriter.Close()
                Catch
                End Try
                Faults.Close()
                dbsFHDB.Close()
                wrkJet.Close()
                Return False
            End Try

            Faults.MoveNext()
            i = i + 1
        End While
        Try
            Faults.Close()
            objWriter.Close()
        Catch ex As Exception
            Dim style = MsgBoxStyle.Exclamation Or MsgBoxStyle.OkOnly
            Dim msg = (String.Format("Error exporting the FHDB database: {0}", ex.Message))
            Dim title = "FHDB Export"
            MsgBox(msg, style, title)
        End Try
        dbsFHDB.Close()
        wrkJet.Close()
        Return True
    End Function

End Module