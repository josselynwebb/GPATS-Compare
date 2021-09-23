
Imports System.Windows.Forms
Imports Microsoft.VisualBasic

Public Class Panel_Conifg
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents fraPanelConfig As System.Windows.Forms.GroupBox
    Friend WithEvents timDebug As System.Windows.Forms.Timer
    Friend WithEvents imgList As System.Windows.Forms.ImageList
    Friend WithEvents cmdPanelConfigLoadFromFile As System.Windows.Forms.Button
    Friend WithEvents cmdPanelConfigSaveToFile As System.Windows.Forms.Button
    Friend WithEvents cmdPanelConfigLoadFromInst As System.Windows.Forms.Button
    Friend WithEvents cmdLocRem As System.Windows.Forms.Button
    Friend WithEvents CommonDialog_Save As System.Windows.Forms.SaveFileDialog
    Friend WithEvents pctMode As NationalInstruments.UI.WindowsForms.Led
    Friend WithEvents CommonDialog_Load As System.Windows.Forms.OpenFileDialog
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Panel_Conifg))
        Me.CommonDialog_Save = New System.Windows.Forms.SaveFileDialog()
        Me.CommonDialog_Load = New System.Windows.Forms.OpenFileDialog()
        Me.fraPanelConfig = New System.Windows.Forms.GroupBox()
        Me.pctMode = New NationalInstruments.UI.WindowsForms.Led()
        Me.cmdPanelConfigLoadFromFile = New System.Windows.Forms.Button()
        Me.cmdPanelConfigSaveToFile = New System.Windows.Forms.Button()
        Me.cmdPanelConfigLoadFromInst = New System.Windows.Forms.Button()
        Me.cmdLocRem = New System.Windows.Forms.Button()
        Me.timDebug = New System.Windows.Forms.Timer(Me.components)
        Me.imgList = New System.Windows.Forms.ImageList(Me.components)
        Me.fraPanelConfig.SuspendLayout()
        CType(Me.pctMode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'fraPanelConfig
        '
        Me.fraPanelConfig.Controls.Add(Me.pctMode)
        Me.fraPanelConfig.Controls.Add(Me.cmdPanelConfigLoadFromFile)
        Me.fraPanelConfig.Controls.Add(Me.cmdPanelConfigSaveToFile)
        Me.fraPanelConfig.Controls.Add(Me.cmdPanelConfigLoadFromInst)
        Me.fraPanelConfig.Controls.Add(Me.cmdLocRem)
        Me.fraPanelConfig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.0!)
        Me.fraPanelConfig.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraPanelConfig.Location = New System.Drawing.Point(0, 0)
        Me.fraPanelConfig.Name = "fraPanelConfig"
        Me.fraPanelConfig.Size = New System.Drawing.Size(181, 139)
        Me.fraPanelConfig.TabIndex = 0
        Me.fraPanelConfig.TabStop = False
        Me.fraPanelConfig.Text = "Panel Configuration"
        '
        'pctMode
        '
        Me.pctMode.LedStyle = NationalInstruments.UI.LedStyle.Round3D
        Me.pctMode.Location = New System.Drawing.Point(36, 104)
        Me.pctMode.Name = "pctMode"
        Me.pctMode.OffColor = System.Drawing.Color.Lime
        Me.pctMode.OnColor = System.Drawing.Color.Red
        Me.pctMode.Size = New System.Drawing.Size(26, 26)
        Me.pctMode.TabIndex = 6
        '
        'cmdPanelConfigLoadFromFile
        '
        Me.cmdPanelConfigLoadFromFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPanelConfigLoadFromFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPanelConfigLoadFromFile.Location = New System.Drawing.Point(34, 16)
        Me.cmdPanelConfigLoadFromFile.Name = "cmdPanelConfigLoadFromFile"
        Me.cmdPanelConfigLoadFromFile.Size = New System.Drawing.Size(114, 25)
        Me.cmdPanelConfigLoadFromFile.TabIndex = 4
        Me.cmdPanelConfigLoadFromFile.Text = "Load from File"
        Me.cmdPanelConfigLoadFromFile.UseVisualStyleBackColor = False
        '
        'cmdPanelConfigSaveToFile
        '
        Me.cmdPanelConfigSaveToFile.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPanelConfigSaveToFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPanelConfigSaveToFile.Location = New System.Drawing.Point(34, 46)
        Me.cmdPanelConfigSaveToFile.Name = "cmdPanelConfigSaveToFile"
        Me.cmdPanelConfigSaveToFile.Size = New System.Drawing.Size(114, 25)
        Me.cmdPanelConfigSaveToFile.TabIndex = 3
        Me.cmdPanelConfigSaveToFile.Text = "Save to File"
        Me.cmdPanelConfigSaveToFile.UseVisualStyleBackColor = False
        '
        'cmdPanelConfigLoadFromInst
        '
        Me.cmdPanelConfigLoadFromInst.BackColor = System.Drawing.SystemColors.Control
        Me.cmdPanelConfigLoadFromInst.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdPanelConfigLoadFromInst.Location = New System.Drawing.Point(14, 76)
        Me.cmdPanelConfigLoadFromInst.Name = "cmdPanelConfigLoadFromInst"
        Me.cmdPanelConfigLoadFromInst.Size = New System.Drawing.Size(154, 25)
        Me.cmdPanelConfigLoadFromInst.TabIndex = 2
        Me.cmdPanelConfigLoadFromInst.Text = "Load from Instrument"
        Me.cmdPanelConfigLoadFromInst.UseVisualStyleBackColor = False
        '
        'cmdLocRem
        '
        Me.cmdLocRem.BackColor = System.Drawing.SystemColors.Control
        Me.cmdLocRem.Enabled = False
        Me.cmdLocRem.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdLocRem.Location = New System.Drawing.Point(72, 105)
        Me.cmdLocRem.Name = "cmdLocRem"
        Me.cmdLocRem.Size = New System.Drawing.Size(66, 25)
        Me.cmdLocRem.TabIndex = 1
        Me.cmdLocRem.Text = "Local"
        Me.cmdLocRem.UseVisualStyleBackColor = False
        '
        'timDebug
        '
        Me.timDebug.Interval = 10000
        '
        'imgList
        '
        Me.imgList.ImageStream = CType(resources.GetObject("imgList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.imgList.TransparentColor = System.Drawing.SystemColors.Control
        Me.imgList.Images.SetKeyName(0, "Greenled.bmp")
        Me.imgList.Images.SetKeyName(1, "Redled.bmp")
        '
        'Panel_Conifg
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.fraPanelConfig)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "Panel_Conifg"
        Me.Size = New System.Drawing.Size(187, 140)
        Me.fraPanelConfig.ResumeLayout(False)
        CType(Me.pctMode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub

#End Region

    '=========================================================
    'Property Variables:
    Dim m_Parent_Object As Object
    Dim m_DebugMode As Boolean
    Dim m_TimerEnable As Boolean

    ' List of controls on the parent window
    Public ControlList As New System.Collections.Generic.List(Of System.Windows.Forms.Control)

    'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
    'MemberInfo=9,0,0,0
    Public Property Parent_Object() As Object
        Get
            Parent_Object = m_Parent_Object
        End Get
        '-----
        Set(ByVal New_Parent_Object As Object)
            m_Parent_Object = New_Parent_Object
        End Set
    End Property



    Public ReadOnly Property DebugMode() As Boolean
        Get
            DebugMode = m_DebugMode
        End Get
    End Property

    Public Shadows Property Refresh() As Short
        Get
            Dim Interval As Integer ' - "AutoDim"

            Interval = timDebug.Interval
            Return (0)
        End Get
        '-----
        Set(ByVal iRate As Short)
            If iRate > 999 And iRate < 25001 Then
                timDebug.Interval = iRate
                m_TimerEnable = True
            End If
        End Set
    End Property



    Public WriteOnly Property TimerEnable() As Boolean
        Set(ByVal bEnable As Boolean)
            'Allow Instrument to override the debug Timer
            m_TimerEnable = bEnable
            timDebug.Enabled = bEnable
        End Set
    End Property

    Public Sub SetDebugStatus(ByVal bLocal As Boolean)
        If bLocal = True Then
            'Local mode
            m_DebugMode = False
        Else
            'Debug mode
            m_DebugMode = True
        End If
        pctMode.Value = m_DebugMode

        timDebug.Enabled = m_DebugMode
        cmdLocRem.Enabled = m_DebugMode
    End Sub

    Private Sub pctMode_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        MessageBox.Show("Version: " & My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision, "VIPERT Common Control", MessageBoxButtons.OK)
    End Sub

    Private Sub timDebug_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles timDebug.Tick
        Dim objParent As Object

        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object

            timDebug.Enabled = False
            'Loop to get info from Instrument
            objParent.ConfigGetCurrent()

            If m_TimerEnable = True Then
                timDebug.Enabled = True
            End If
        End If

    End Sub

    Private Sub cmdLocRem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdLocRem.Click
        'Switch Mode
        m_DebugMode = Not m_DebugMode

        'Start/Stop Timer
        timDebug.Enabled = m_DebugMode

        If m_DebugMode = True Then
            cmdLocRem.Text = "Local"
        Else
            cmdLocRem.Text = "Remote"
        End If
        pctMode.Value = m_DebugMode

    End Sub

    Private Sub cmdPanelConfigLoadFromFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPanelConfigLoadFromFile.Click
        Dim objParent As Object

        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
            '   Run local subroutine to set controls on legacy form
            ConfigLoad()
        End If
    End Sub

    Private Sub cmdPanelConfigSaveToFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPanelConfigSaveToFile.Click
        Dim objParent As Object
        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
            '   Run subroutine to set controls on legacy form
            ConfigSave()
        End If
    End Sub

    Private Sub cmdPanelConfigLoadFromInst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdPanelConfigLoadFromInst.Click
        Dim objParent As Object

        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
            '   Run subroutine on legacy form
            objParent.ConfigGetCurrent()
        End If
    End Sub


    Private Sub ConfigSave()
        'local variables
        Dim sName As String
        Dim sValue As String
        Dim Msg As String
        Dim ctl As Control
        Dim objParent As Object = New Object
        Dim writer As System.IO.StreamWriter = Nothing

        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
        End If

        Try ' On Error GoTo ErrHandler
            CommonDialog_Save.Filter = "All Files (*.*)|*.*|Data Files" & "(*.dat)|*.dat|Batch Files (*.bat)|*.bat|Text Files (*.txt)|*.txt"
            CommonDialog_Save.FilterIndex = 4

            If CommonDialog_Save.ShowDialog() = DialogResult.OK Then
                 writer = New System.IO.StreamWriter(CommonDialog_Save.FileName, False)

                'Get the instrument mode and write it to the file first
                writer.WriteLine(objParent.GetMode())

                ' Build the list of controls on the parent window
                GetWindowControls()

                'Iterate through all of the controls
                 For Each ctl In ControlList

                    'clear values
                    sName = ""
                    sValue = ""

                    If TypeOf ctl Is TextBox Then
                        Dim txtbox As TextBox = CType(ctl, TextBox)
                        sName = txtbox.Name
                        sValue = txtbox.Text
                    ElseIf TypeOf ctl Is ComboBox Then
                        Dim cbbox As ComboBox = CType(ctl, ComboBox)
                        sName = cbbox.Name
                        sValue = cbbox.SelectedIndex.ToString()
                    ElseIf TypeOf ctl Is CheckBox Then
                        Dim chkbox As CheckBox = CType(ctl, CheckBox)
                        sName = chkbox.Name
                        sValue = chkbox.Checked.ToString()
                    ElseIf TypeOf ctl Is RadioButton Then
                        Dim rdobtn As RadioButton = CType(ctl, RadioButton)
                        sName = rdobtn.Name
                        sValue = rdobtn.Checked.ToString()
                    ElseIf TypeOf ctl Is UserControl Then
                        If ctl.Name <> "Panel_Conifg" And ctl.Name <> "Atlas_SFP" Then
                            sName = ctl.Name
                            sValue = objParent.GetData(sName)
                        End If
                    ElseIf TypeOf ctl Is ToolBar Then
                        Dim toolbar As ToolBar = CType(ctl, ToolBar)
                        Dim i As Integer = 0
                        sName = toolbar.Name
                        For Each Button As ToolBarButton In toolbar.Buttons
                            If (Button.Pushed = True) Then
                                sValue = i
                                Exit For
                            End If
                            i = i + 1
                        Next
                    End If

                    If Not (sName = "") Then
                        'seperator then value
                        sName += "=" + sValue
                        writer.WriteLine(sName)
                    End If
                Next ctl
                '
            End If

        Catch   ' ErrHandler:
            If Err.Number = 343 Then
                'Object is not an Array
                MessageBox.Show("Bad Array Object")
            End If

            If Err.Number = 61 Or Err.Number = 31036 Then
                'Disk Full or Can not save
                MessageBox.Show("Error writing to Disk")
            ElseIf Err.Number = 75 Or Err.Number = 76 Then
                'Path/File access or path not found
                MessageBox.Show("Unable to save File")
            ElseIf Err.Number = 70 Then
                'Object is not an Array
                MessageBox.Show("Permission denied")
            ElseIf Err.Number = 32755 Then
                'User pressed the Cancel button
            Else
                ' Display unanticipated error message.
                Msg = "Unanticipated error " & Err.Number
                Msg &= ": " & Err.Description
                MessageBox.Show(Msg, "error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Finally
            If Not (writer Is Nothing) Then
                writer.Close()
            End If
        End Try
    End Sub

    Private Sub ConfigLoad()
        Dim Msg As String
        Dim sNextLine As String = ""
         Dim reader As System.IO.StreamReader = Nothing

        Dim objParent As Object = New Object
        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
        End If

        Try ' On Error GoTo ErrHandler
            CommonDialog_Load.Filter = "All Files (*.*)|*.*|Data Files" & "(*.dat)|*.dat|Batch Files (*.bat)|*.bat|Text Files (*.txt)|*.txt"
            CommonDialog_Load.FilterIndex = 4
            If CommonDialog_Load.ShowDialog() = DialogResult.OK Then
                reader = New IO.StreamReader(CommonDialog_Load.FileName)

                'Put the parent (SFP) in update mode
                objParent.UpdatingFromFile = True

                'Instrument Mode is allway first
                sNextLine = reader.ReadLine()
                objParent.SetMode(sNextLine)

                'Read all of the controls in the file
                Do Until reader.EndOfStream
                    sNextLine = reader.ReadLine()

                    ' Save to correct controls
                    SetFormData(sNextLine)
                Loop
            End If
        Catch   ' ErrHandler:
            If Err.Number = 71 Or Err.Number = 31037 Then
                'Disk Full or Can not save
                MessageBox.Show("Error reading from Disk")
            ElseIf Err.Number = 53 Or Err.Number = 75 Then
                'Path/File access or file not found
                MessageBox.Show("File not found")
            ElseIf Err.Number = 70 Then
                'Object is not an Array
                MessageBox.Show("Permission denied")
            ElseIf Err.Number = 32755 Then
                'User pressed the Cancel button

            Else
                ' Display unanticipated error message.
                Msg = "Unanticipated error " & Err.Number
                Msg &= ": " & Err.Description
                MessageBox.Show(Msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Finally
            If Not (reader Is Nothing) Then
                reader.Close()
            End If

            'Take the parent (SFP) out of update mode
            objParent.UpdatingFromFile = False
        End Try
    End Sub

    Private Sub SetFormData(ByVal sNextLine As String)
        Dim sValue As String
        Dim sControl As String = ""
        Dim nIndex As Short

        Dim objParent As Object = New Object
        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
        End If

        'grab the value fron the string
        sValue = Strings.Right(sNextLine, Len(sNextLine) - InStr(sNextLine, "="))
        If InStr(sNextLine, "=") Then
            sControl = Strings.Left(sNextLine, InStr(sNextLine, "=") - 1)
        End If
        If InStr(sControl, "(") Then
            sControl = Strings.Left(sControl, InStr(sNextLine, "(") - 1)
        End If
        Try ' On Error GoTo ErrHandle
            ' Build the list of controls on the parent window
            GetWindowControls()

            For Each ctl As System.Windows.Forms.Control In ControlList
                If sControl = ctl.Name Then
                    'get the index if any
                    If InStr(sNextLine, "(") Then
                        nIndex = CInt(Mid(sNextLine, InStr(sNextLine, "(") + 1, InStr(sNextLine, ")") - InStr(sNextLine, "(") - 1))
                    Else
                        nIndex = -1
                    End If

                    ' Start looking for the matching type
                    If TypeOf ctl Is Windows.Forms.TextBox Then
                        ctl.Text = sValue
                    ElseIf TypeOf ctl Is Windows.Forms.ComboBox Then
                        Dim cbobox As ComboBox = CType(ctl, ComboBox)
                        cbobox.SelectedIndex = Convert.ToInt16(sValue)
                    ElseIf TypeOf ctl Is Windows.Forms.CheckBox Then
                        Dim chkbox As CheckBox = CType(ctl, CheckBox)
                        chkbox.Checked = Convert.ToBoolean(sValue)
                    ElseIf TypeOf ctl Is Windows.Forms.RadioButton Then
                        Dim rdobtn As RadioButton = CType(ctl, RadioButton)
                        rdobtn.Checked = Convert.ToBoolean(sValue)
                    ElseIf TypeOf ctl Is Windows.Forms.UserControl Then
                        objParent.SetData(ctl.Name, sValue)
                        Exit For
                     ElseIf TypeOf ctl Is ToolBar Then
                        Dim toolbar As ToolBar = CType(ctl, ToolBar)
                        Dim i As Integer = 0
                        'first reset all buttons
                        For Each button As ToolBarButton In toolbar.Buttons
                            button.Pushed = False
                        Next
                        'then push the selected button
                        toolbar.Buttons(Convert.ToInt32(sValue)).Pushed = True
                    Else
                        'No index
                        'ctl.Text = sValue
                        Exit Sub
                    End If
                End If
            Next ctl
        Catch   ' ErrHandle:
            MessageBox.Show("Error Processing: " & sNextLine & " with " & sControl & vbCrLf & Err.Description, "SetFormData Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub GetWindowControls()
        Dim ctl As System.Windows.Forms.Control
        Dim objParent As Object = New Object

        ControlList.Clear()

        If Not (m_Parent_Object Is Nothing) Then
            '   Connect to parent object
            objParent = m_Parent_Object
        End If

        For Each ctl In objParent.Controls
            BuildControlList(ctl)
        Next
    End Sub

    Private Sub BuildControlList(InCtl As System.Windows.Forms.Control)
        Dim ctl As System.Windows.Forms.Control
        If TypeOf InCtl Is Windows.Forms.UserControl Then
            ControlList.Add(InCtl)
        ElseIf InCtl.HasChildren Then
            For Each ctl In InCtl.Controls
                BuildControlList(ctl)
            Next
        Else
            ControlList.Add(InCtl)
        End If
    End Sub
End Class