Option Strict Off
Option Explicit On

Imports System.IO
Imports System.IO.Compression
Imports System.IO.Packaging
Imports Ionic.Zip
Imports Ionic.Crc


Friend Class frmExport
    Inherits System.Windows.Forms.Form
    '**********************************************************************
    '***    United States Marine Corps                                  ***
    '***                                                                ***
    '***    Nomenclature:   Form "frmExport" : FHDB FHDB_Processor      ***
    '***    Written By:     Dave Joiner                                 ***
    '***    Purpose:                                                    ***
    '***  ECP-TETS-023                                                  ***
    '***    This form provides the User Interface to Export             ***
    '***    Files from the FHDB. Both the Faults Table and the          ***
    '***    System Table are Exported as a Zipped version of the        ***
    '***    FHBD.MDB File.(EXPORT.zip) The Zipped file will span        ***
    '***    multiple disks if size dictates.                            ***
    '***    The System Table contains only one record that is either    ***
    '***    created or updated immediately pior to Export.              ***
    '***  2.1.0                                                         ***
    '***    Changed all references to either TETS.INI or VIPERT.ini in  ***
    '***    any comments to Tester.ini                                  ***
    '***    Sub Form_Load                                               ***
    '***    Added a variable dDisplayMsg, used to hold a message string ***
    '***    to be displayed in a mesgbox, used to shorten and be able to***
    '***    view without a lot of scrolling.                            ***
    '***    Left the unload call to frmExport intead of Me.             ***
    '***    Modified the lower portion of the function on where the FHDB***
    '***    will be saved and how that it is selected.  Also removed an ***
    '***    operator message on what to do, no longer revelant.         ***
    '***    Sub xZip_FileStatus                                         ***
    '***    Modified the call to StatusMsg to display the location that ***
    '***    was being written to.                                       ***
    '***    xZip_ProcessCompleted                                       ***
    '***    Added the test for xerCannotUpdateAndSpan to display a      ***
    '***    possible reason for the error, what was being displayed was ***
    '***    kind of lacking explaning what happen. Also tested for the  ***
    '***    return xerOpenZipFile to display a proper error message     ***
    '**********************************************************************





    Public Sub AddMessage(ByRef sToAdd As String)
        '--------------------------------------------------
        '---    Adds a line in the lstResults Listbox   ---
        '--------------------------------------------------

        Dim lList As System.Windows.Forms.ListViewItem
        lList = Me.lstResults.Items.Add(sToAdd)
        System.Windows.Forms.Application.DoEvents()
    End Sub

    Private Sub cmdClose_Click(sender As Object, e As EventArgs) Handles cmdClose.Click
        On Error GoTo ErrorLabel

        Me.Close() 'Unload Export Form from memory

ErrorLabel:
        If Err.Number = 53 Then 'Handle Error if File Does Not Exist
            Resume Next
        End If

    End Sub

    Private Sub txtFilesToProcess_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtFilesToProcess.Click
        'Prevent Operator from changing Default File Location and Name
        cmdClose.Focus()

    End Sub


    Private Sub txtZipFilename_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles txtZipFilename.Click
        'Prevent Operator from changing Default File Location and Name
        cmdClose.Focus()

    End Sub


    Private Sub frmExport_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load

        Dim dDisplayMsg As String 'message to be displayed in a message box
        '--------------------------------------------------------------
        '---        Load Export Form and Initialize Fields          ---
        '--------------------------------------------------------------

        If bOnStart = True Then
            'Pop Up About Screen
            bNoUnload = True
            CenterForm(frmAbout) : frmAbout.Show() : frmAbout.Refresh()
            Delay(1) 'Show About Screen for 1 Second
            frmAbout.Close() 'Remove About Screen
            bOnStart = False
        End If

        bExport = True 'Set the Export Operation Flag
        Call CloseDB() 'Make sure the Database Connection is not open
        Call ClearViewer()
        Call Disable_Navigation()

        'Check Database for Record Count, If no Records, Abort
        Call OpenDB()
        If rstFaults.RecordCount = 0 Then
            dDisplayMsg = "There are no records in the DataBase - Operation Aborted"
            MsgBox(dDisplayMsg, MsgBoxStyle.Exclamation, "Export Operation Aborted")
            bNoRecords = True
            If bNoUnload = False Then
                Me.Close()
            End If
            bNoUnload = False
            Exit Sub
        End If
        Call CloseDB()

        'Get File Names from the Tester's INI File
        sDatabase = GatherIniFileInformation("File Locations", "FHDB_DATABASE", "")
        sExportFile = GatherIniFileInformation("File Locations", "FHDB_EXPORT", "")

        Me.Show() 'Launches the Export Data Form

        sFileLocation = UDD_HD_Path(EXPORT_FILE)
        If sFileLocation = "" Then
            bExport = False
            gResponse = MsgBoxResult.Cancel
            Exit Sub
        End If

        'Set Status Messsage
        Call StatusMsg("File Export Utility -- Ready to Export Files")

        Call InitializeExportFields()
        txtZipFilename.Text = sFileLocation & "\" & sExportFile
        txtFilesToProcess.Text = sDatabase

    End Sub


    Private Sub frmExport_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '--------------------------------------------------------------
        '---        When Export Form Unloads                        ---
        '---        If Viewer is loaded, set Status Message         ---
        '---        Else Load Viewer Form                           ---
        '--------------------------------------------------------------

        If frmViewer.Visible Then
            Call StatusMsg("Record Viewer") 'Set Status Messsage
            Call ReturnAll() 'Query all records and load into RecordSet
            'set record slider max to number of records
            Call LastRecord() 'Move curser to Last record in RecordSet
            frmViewer.SldRecord.Value = frmViewer.SldRecord.Maximum
            System.Windows.Forms.Application.DoEvents()
        Else
            frmViewer.Show()
            frmViewer.Activate()
        End If
        bExport = False 'Reset Export Operation Flag

    End Sub


    Private Sub cmdZip_Click(sender As Object, e As EventArgs) Handles cmdZip.Click
        '----------------------------------------------------------------------
        '--         Call procedure to initiate the Zipping Process          ---
        '----------------------------------------------------------------------

        cmdClose.Enabled = False
        Dim bexportsuccess As Boolean = False
        Call UpdateSystemRecord()
        If bSystemRecordError = True Then
            Me.Close()
            Exit Sub
        End If
        bexportsuccess = Export_DB_To_CSV()
        Call Zip_It(bexportsuccess)
        If bDiskError Then
            cmdClose.Enabled = True
            cmdZip.Focus()
            bDiskError = False 'Reset Disk Error Flag
        Else
            cmdClose.Enabled = True
            cmdClose.Focus()
        End If




        '***********************************************************************
        '****             Procedures to handle ZIP Events                   ****
        '***********************************************************************

    End Sub
    Public Sub Zip_It(ByVal zipOK As Boolean)

        '--------------------------------------------------------------------------
        '---   Main properties are first reset to the desired default values,   ---
        '---   then the values of the form's controls are copied to xZip's      ---
        '---   properties and the Zip method is called.                         ---
        '---   If a value is encountered in the SfxBinaryModule property,       ---
        '---   XceedZip will create a self-extracting zip file with an          ---
        '---   integrated self-extracting zip file module specified by          ---
        '---   the SfxBinaryModule property.                                    ---
        '---------------------------------------------------------------------------

        'Dim xErr As String
        Dim ZipFileName As String
        Dim FilesToProcess As String
        Dim result As Boolean
        Dim directoryPath As String
        Dim dialogResult As DialogResult

        ZipFileName = Me.txtZipFilename.Text 'Get Output File name
        directoryPath = Path.GetDirectoryName(ZipFileName)
        Try 'delete any existing zip name if exists
            File.Delete(ZipFileName)
        Catch ex As Exception
            'dont care
        End Try

        If zipOK = True Then
            Call StatusMsg("Zipping Export File") 'Set Status Messsage

            ZipFileName = Me.txtZipFilename.Text 'Get Output File name

            FilesToProcess = Me.txtFilesToProcess.Text

            Me.lstResults.Items.Clear() 'Clear the Results List Field

            Dim zip As Package = ZipPackage.Open(ZipFileName, _
                IO.FileMode.Create, IO.FileAccess.ReadWrite)
            AddToArchive(zip, FilesToProcess)

            AddToArchive(zip, directoryPath & "\FAULTS.csv")
            AddToArchive(zip, directoryPath & "\SYSTEM.csv")
            My.Computer.FileSystem.DeleteFile(directoryPath & "\FAULTS.csv")
            My.Computer.FileSystem.DeleteFile(directoryPath & "\SYSTEM.csv")
            Try
                zip.Close() 'Close the zip file, if an exception is thrown there is nt enough space on the current media
                GoTo Cleanup
            Catch ex As Exception
            End Try

            dialogResult = MessageBox.Show("Not Enough Space on """ & directoryPath & """" & vbCrLf & "Would You Like to Span Across Multiple Media?", "UDD Error",
                        MessageBoxButtons.YesNo)
            If dialogResult = dialogResult.Yes Then
                File.Delete(ZipFileName)

InputMediaSize: Dim sMediaSize As String = InputBox("Please enter Media size in bytes:" & vbCrLf & "(Mininum size is 65536 bytes)", "Input Media Size", "65536", -1, -1)
                If (sMediaSize = "") Then 'cancel button selected
                    Exit Sub
                End If
                If (Convert.ToInt32(sMediaSize) < 65536) Then
                    If (MessageBox.Show("Invalid media size Specified. Would like to enter a different media size?", "Error", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes) Then
                        GoTo InputMediaSize
                    Else
                        Exit Sub
                    End If
                End If
                Try 'delete any existing temp files
                    File.Delete("C:\Temp\TempFHDBExport.zip")
                Catch ex As Exception
                    'dont care
                End Try
                Dim zipPartPackage As New Ionic.Zip.ZipFile("C:\Temp\TempFHDBExport.zip")
                zipPartPackage.AddFile(FilesToProcess)
                zipPartPackage.MaxOutputSegmentSize = Convert.ToInt32(sMediaSize)
                Try
                    zipPartPackage.Save()
                Catch ex As Exception
                    MessageBox.Show(ex.Message & " Aborting Operation.", "FHDB Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try

                'See how many part files were created
                Dim numberString = "z01"
                Dim mediaCount = 0
                Dim counter As Integer = 1
                While (File.Exists("C:\Temp\TempFHDBExport." & numberString) = True)

                    mediaCount = mediaCount + 1

                    If (mediaCount < 10) Then
                        numberString = "z0" & mediaCount
                    Else
                        numberString = "z" & mediaCount
                    End If

                End While

                If (MessageBox.Show("This Will Require " & mediaCount - 1 & " media(s) of size " & sMediaSize & " (in bytes) or greater." & vbCrLf & "Do you wish to continue?", "Question", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.No) Then
                    Exit Sub
                End If

                For i As Integer = 1 To mediaCount - 1

                    If (MessageBox.Show("Please insert disc number " & i & ", then select OK when ready.", "Insert Disc", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.Cancel) Then
                        Exit Sub
                    End If

                    Try
                        Dim crc As New CRC32()
                        Dim crcValueSource As Integer
                        Dim crcValueDestination As Integer
                        Dim ioStream As System.IO.Stream

                        If (i < 10) Then
                            numberString = "z0" & i
                        Else
                            numberString = "z" & i
                        End If

                        If (i = 1) Then 'first media, copy full file and part
                            ioStream = File.Open("C:\Temp\TempFHDBExport.zip", FileMode.Open)
                            crc.Reset()
                            crcValueSource = crc.GetCrc32(ioStream)
                            ioStream.Close()
                            File.Copy("C:\Temp\TempFHDBExport.zip", ZipFileName, True)
                            ioStream = File.Open(ZipFileName, FileMode.Open)
                            crc.Reset()
                            crcValueDestination = crc.GetCrc32(ioStream)
                            If (crcValueSource <> crcValueDestination) Then
                                MessageBox.Show("CRC does not match. Aborting Operation.", "FHDB Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            End If
                            ioStream.Close()
                        End If

                        Dim partFileName As String = Path.GetDirectoryName(ZipFileName)
                        partFileName = partFileName & "\" & Path.GetFileNameWithoutExtension(ZipFileName)
                        partFileName = partFileName & "." & numberString
                        ioStream = File.Open("C:\Temp\TempFHDBExport." & numberString, FileMode.Open)
                        crc.Reset()
                        crcValueSource = crc.GetCrc32(ioStream)
                        ioStream.Close()
                        File.Copy("C:\Temp\TempFHDBExport." & numberString, partFileName, True)
                        ioStream = File.Open(partFileName, FileMode.Open)
                        crc.Reset()
                        crcValueDestination = crc.GetCrc32(ioStream)
                        If (crcValueSource <> crcValueDestination) Then
                            MessageBox.Show("CRC does not match. Aborting Operation.", "FHDB Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        End If
                        ioStream.Close()

                    Catch ex As Exception
                        MessageBox.Show(ex.Message & " Aborting Operation.", "FHDB Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                Next

            End If


Cleanup:
        End If
        If FileExists((directoryPath & "\FAULTS.csv")) Then
            My.Computer.FileSystem.DeleteFile(directoryPath & "\FAULTS.csv")
        End If

        If FileExists((directoryPath & "\SYSTEM.csv")) Then
            My.Computer.FileSystem.DeleteFile(directoryPath & "\SYSTEM.csv")
        End If

        If (FileExists(ZipFileName)) Then
            lstResults.Items.Add("Zipping of FHDB completed successfully")
            UpdateIniFile("FHDB", "EXPORT_TIME", "")
        Else
            lstResults.Items.Add("An error occured while zipping the FHDB database")
        End If



    End Sub

    Private Sub AddToArchive(ByVal zip As Package, _
                     ByVal fileToAdd As String)

        'Replace spaces with an underscore (_) 
        Dim uriFileName As String = fileToAdd.Replace(" ", "_")

        'A Uri always starts with a forward slash "/" 
        Dim zipUri As String = String.Concat("/", _
                   IO.Path.GetFileName(uriFileName))

        Dim partUri As New Uri(zipUri, UriKind.Relative)
        Dim contentType As String = _
                   Net.Mime.MediaTypeNames.Application.Zip

        'The PackagePart contains the information: 
        ' Where to extract the file when it's extracted (partUri) 
        ' The type of content stream (MIME type):  (contentType) 
        ' The type of compression:  (CompressionOption.Normal)   
        Dim pkgPart As PackagePart = zip.CreatePart(partUri, _
                 contentType, CompressionOption.Normal)

        'Read all of the bytes from the file to add to the zip file 
        Dim bites As Byte() = File.ReadAllBytes(fileToAdd)

        'Compress and write the bytes to the zip file 
        pkgPart.GetStream().Write(bites, 0, bites.Length)

    End Sub



    Public Sub xZip_FileStatus(ByVal sFilename As String, ByVal lSize As Integer, ByVal lCompressedSize As Integer, ByVal lBytesProcessed As Integer, ByVal nBytesPercent As Short, ByVal nCompressionRatio As Short, ByVal bFileCompleted As Boolean)
        '----------------------------------------------------------------------
        '---    The FileStatus Event is triggered during the processing     ---
        '---    of each file. It provides progress information about        ---
        '---    the status of each file.                                    ---
        '----------------------------------------------------------------------

        Debug.Print(nBytesPercent)
        If nBytesPercent < 98 Then
            Call StatusMsg("Creating Zip file")
        Else
            Call StatusMsg("Writing file to " & sFileLocation & ".............................")
        End If

    End Sub


    Public Sub xZip_GlobalStatus(ByVal lFilesTotal As Integer, ByVal lFilesProcessed As Integer, ByVal lFilesSkipped As Integer, ByVal nFilesPercent As Short, ByVal lBytesTotal As Integer, ByVal lBytesProcessed As Integer, ByVal lBytesSkipped As Integer, ByVal nBytesPercent As Short, ByVal lBytesOutput As Integer, ByVal nCompressionRatio As Short)
        '--------------------------------------------------------------------------
        '---    The GlobalStatus Event provides progress information on the     ---
        '---    execution of any Xceed Zip method.                              ---
        '--------------------------------------------------------------------------

        Me.BarGlobal.Value = nBytesPercent 'Update Progress Bar

    End Sub


    Public Sub xZip_InsertDisk(ByVal nDiskNumber As Integer, ByRef bDiskInserted As Boolean)
        '------------------------------------------------------------------------------
        '---    The InsertDisk Event is triggered when reading or writing           ---
        '---    a spanned zip file and a new or different disk must be inserted     ---
        '---    into the drive to continue the current operation.                   ---
        '------------------------------------------------------------------------------

        Dim nAnswer As MsgBoxResult

        If nDiskNumber = 0 Then
            If bExport Then
                MsgBox("Please insert a USB disk into the USB slot" & vbCrLf & "This USB Disk will be overwritten" & vbCrLf & vbCrLf & "Then depress the 'Zip' Button to begin Export process" & vbCrLf & "Press the Close button to Cancel Export operation." & vbCrLf & vbCrLf & "        Label Disk: Export Disk Number 1", MsgBoxStyle.Information, "Insert a USB into the USB slot ")
                bDiskError = True
                Exit Sub
            Else
                nAnswer = MsgBox("This zip file is part of a multidisk zip file. " & "Please insert the last disk of the set.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel)
            End If
        Else
            nAnswer = MsgBox("Please insert USB Disk #" & Trim(Str(nDiskNumber)) & vbCrLf & vbCrLf & "Label Disk: Export Disk Number " & Trim(Str(nDiskNumber)), MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel)
        End If

        bDiskInserted = (nAnswer = MsgBoxResult.Ok)

    End Sub


    Public Sub xZip_ProcessCompleted(ByVal lFilesTotal As Integer, ByVal lFilesProcessed As Integer, ByVal lFilesSkipped As Integer, ByVal lBytesTotal As Integer, ByVal lBytesProcessed As Integer, ByVal lBytesSkipped As Integer, ByVal lBytesOutput As Integer, ByVal nCompressionRatio As Short, ByVal xResult As String)
        Dim xerOpenZipFile As Object
        Dim xcoPreviewing As Object
        Dim xcoListing As Object
        Dim xerCannotUpdateAndSpan As Object
        Dim xvtError As Object
        Dim XceedZipLibCtl As Object
        '----------------------------------------------------------------------------------
        '---    The ProcessCompleted event is triggered every time an Xceed Zip         ---
        '---    method has completed its operation. This event provides you with        ---
        '---    some statistics on the operation. It also provides the method           ---
        '---    call's return value (xResult parameter), which is of particular         ---
        '---    use when you have set the BackgroundProcessing flag to True.            ---
        '-----------------------------------------------------------------------------------

        If bDiskError Then Exit Sub
        ' Display error code and description

        Call AddMessage("Process Completed. Error code: " & xResult)

        Call AddMessage(xResult)
        If xResult = xerCannotUpdateAndSpan Then
            Call AddMessage("Probable cause " & sFileLocation & "Export.Zip already exists")
        End If

        AddMessage(("Total number of files processed: " & Str(lFilesProcessed)))
        AddMessage(("Total number of files skipped: " & Str(lFilesSkipped)))
        AddMessage(("The Compression Ratio is: " & Str(nCompressionRatio) & " Percent"))

        Me.BarGlobal.Visible = False



        If CDbl(xResult) = 0 Then
            sMessageString = "File Export Operation Successful"
            System.Windows.Forms.Application.DoEvents()
            Call UpdateIniFile("FHDB", "EXPORT_TIME", "")
            System.Windows.Forms.Application.DoEvents()
            MsgBox("File Export Operation was Successful", , "FILE EXPORT SUCCESSFUL")
        ElseIf xResult = xerOpenZipFile Then
            sMessageString = "File Export Operation Unsuccessful, See Results for cause."
            MsgBox("There was an Error Exporting Files" & vbCrLf & "See Results readout for cause." & vbCrLf & vbCrLf & "The most probable cause is" & vbCrLf & sFileLocation & "Export.Zip already exists" & vbCrLf & "and the user doesn't have permission to modify it.", MsgBoxStyle.Critical, "EXPORT ERROR")
        Else
            sMessageString = "File Export Operation Unsuccessful, See Results for cause."
            MsgBox("There was an Error Exporting Files" & vbCrLf & "See Results readout for cause.", MsgBoxStyle.Critical, "EXPORT ERROR")
        End If


        If sMessageString <> frmBackground.stbMain.Items.Item(1).Text Then

            frmBackground.stbMain.Items.Item(1).Text = sMessageString
        End If

    End Sub

    '***********************************************************************
    '****                      Menu Functions                           ****
    '***********************************************************************

    Public Sub mnuAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuAbout.Click

        Call ShowAbout() 'Show About Form

    End Sub


    Public Sub mnuExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuExit.Click

        MDIMain.Close()

    End Sub


    Public Sub mnuImport_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuImport.Click

        Me.Close()
        bTerminateImporter = False
        Call Import()

    End Sub


    Public Sub mnuViewer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuViewer.Click

        Me.Close()

    End Sub


    '***********************************************************************
    '****                   Status Line Messages                        ****
    '***********************************************************************

    Private Sub txtZipFilename_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtZipFilename.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        txtZipFilename.Cursor = System.Windows.Forms.Cursors.Arrow
        Call StatusMsg("The Export File name and location.")
    End Sub

    Private Sub frmExport_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        Call StatusMsg("File Exporter.")
    End Sub

    Private Sub cmdClose_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
        Call StatusMsg("Closes File Exporter and Launches the Viewer.")
    End Sub

    Private Sub cmdZip_MouseMove(ByRef Button As Short, ByRef Shift As Short, ByRef x As Single, ByRef Y As Single)
        Call StatusMsg("Zips Database to Export File.")
    End Sub

    Private Sub txtFilesToProcess_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles txtFilesToProcess.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        txtFilesToProcess.Cursor = System.Windows.Forms.Cursors.Arrow
        Call StatusMsg("The Database File name and location.")
    End Sub

    Private Sub lstResults_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lstResults.MouseMove
        Dim Button As Short = eventArgs.Button \ &H100000
        Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
        Dim x As Single = VB6.PixelsToTwipsX(eventArgs.X)
        Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
        Call StatusMsg("Results of the File Export Operation.")
    End Sub

    Public Sub mnuOperation_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuOperation.Click
        Call StatusMsg("Chose another Operation or Quit.")
    End Sub

    Public Sub mnuHelp_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelp.Click
        Call StatusMsg("FHDB Help.")
    End Sub
    Private Sub frmExport_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

    End Sub
End Class