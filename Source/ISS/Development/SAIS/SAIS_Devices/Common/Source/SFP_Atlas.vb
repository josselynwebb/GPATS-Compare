
Imports System.Windows.Forms

Public Class Atlas_SFP
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
        fraAtlas(0) = fraAtlas_0

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
    Friend fraAtlas(1) As System.Windows.Forms.GroupBox
    Friend WithEvents CommonDialog_Load As System.Windows.Forms.OpenFileDialog
    Friend WithEvents CommonDialog_Save As System.Windows.Forms.SaveFileDialog
    Friend WithEvents txtATLAS As System.Windows.Forms.TextBox
    Friend WithEvents fraAtlas_0 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdOpen As System.Windows.Forms.Button
    Friend WithEvents cmdAtlas As System.Windows.Forms.Button
    Friend WithEvents cmdSave As System.Windows.Forms.Button
    Friend WithEvents cmdClear As System.Windows.Forms.Button
    Friend WithEvents optClipboard As System.Windows.Forms.RadioButton
    Friend WithEvents optTextFile As System.Windows.Forms.RadioButton
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.CommonDialog_Load = New System.Windows.Forms.OpenFileDialog()
        Me.CommonDialog_Save = New System.Windows.Forms.SaveFileDialog()
        Me.txtATLAS = New System.Windows.Forms.TextBox()
        Me.fraAtlas_0 = New System.Windows.Forms.GroupBox()
        Me.cmdOpen = New System.Windows.Forms.Button()
        Me.cmdAtlas = New System.Windows.Forms.Button()
        Me.cmdSave = New System.Windows.Forms.Button()
        Me.cmdClear = New System.Windows.Forms.Button()
        Me.optClipboard = New System.Windows.Forms.RadioButton()
        Me.optTextFile = New System.Windows.Forms.RadioButton()
        Me.fraAtlas_0.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtATLAS
        '
        Me.txtATLAS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtATLAS.Location = New System.Drawing.Point(0, 0)
        Me.txtATLAS.Multiline = True
        Me.txtATLAS.Name = "txtATLAS"
        Me.txtATLAS.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtATLAS.Size = New System.Drawing.Size(397, 171)
        Me.txtATLAS.TabIndex = 0
        '
        'fraAtlas_0
        '
        Me.fraAtlas_0.Controls.Add(Me.cmdOpen)
        Me.fraAtlas_0.Controls.Add(Me.cmdAtlas)
        Me.fraAtlas_0.Controls.Add(Me.cmdSave)
        Me.fraAtlas_0.Controls.Add(Me.cmdClear)
        Me.fraAtlas_0.Controls.Add(Me.optClipboard)
        Me.fraAtlas_0.Controls.Add(Me.optTextFile)
        Me.fraAtlas_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fraAtlas_0.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.fraAtlas_0.Location = New System.Drawing.Point(3, 169)
        Me.fraAtlas_0.Name = "fraAtlas_0"
        Me.fraAtlas_0.Size = New System.Drawing.Size(393, 41)
        Me.fraAtlas_0.TabIndex = 1
        Me.fraAtlas_0.TabStop = False
        Me.fraAtlas_0.Text = "Options"
        '
        'cmdOpen
        '
        Me.cmdOpen.BackColor = System.Drawing.SystemColors.Control
        Me.cmdOpen.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.cmdOpen.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdOpen.Location = New System.Drawing.Point(295, 13)
        Me.cmdOpen.Name = "cmdOpen"
        Me.cmdOpen.Size = New System.Drawing.Size(49, 23)
        Me.cmdOpen.TabIndex = 7
        Me.cmdOpen.Text = "Open"
        Me.cmdOpen.UseVisualStyleBackColor = False
        '
        'cmdAtlas
        '
        Me.cmdAtlas.BackColor = System.Drawing.SystemColors.Control
        Me.cmdAtlas.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.cmdAtlas.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdAtlas.Location = New System.Drawing.Point(174, 13)
        Me.cmdAtlas.Name = "cmdAtlas"
        Me.cmdAtlas.Size = New System.Drawing.Size(72, 23)
        Me.cmdAtlas.TabIndex = 6
        Me.cmdAtlas.Text = "Generate"
        Me.cmdAtlas.UseVisualStyleBackColor = False
        '
        'cmdSave
        '
        Me.cmdSave.BackColor = System.Drawing.SystemColors.Control
        Me.cmdSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdSave.Location = New System.Drawing.Point(343, 13)
        Me.cmdSave.Name = "cmdSave"
        Me.cmdSave.Size = New System.Drawing.Size(48, 23)
        Me.cmdSave.TabIndex = 5
        Me.cmdSave.Text = "Save"
        Me.cmdSave.UseVisualStyleBackColor = False
        '
        'cmdClear
        '
        Me.cmdClear.BackColor = System.Drawing.SystemColors.Control
        Me.cmdClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.cmdClear.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdClear.Location = New System.Drawing.Point(247, 13)
        Me.cmdClear.Name = "cmdClear"
        Me.cmdClear.Size = New System.Drawing.Size(48, 23)
        Me.cmdClear.TabIndex = 4
        Me.cmdClear.Text = "Clear"
        Me.cmdClear.UseVisualStyleBackColor = False
        '
        'optClipboard
        '
        Me.optClipboard.BackColor = System.Drawing.SystemColors.Control
        Me.optClipboard.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.optClipboard.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optClipboard.Location = New System.Drawing.Point(85, 16)
        Me.optClipboard.Margin = New System.Windows.Forms.Padding(0)
        Me.optClipboard.Name = "optClipboard"
        Me.optClipboard.Size = New System.Drawing.Size(90, 20)
        Me.optClipboard.TabIndex = 2
        Me.optClipboard.Text = "Clipboard"
        Me.optClipboard.UseVisualStyleBackColor = False
        '
        'optTextFile
        '
        Me.optTextFile.BackColor = System.Drawing.SystemColors.Control
        Me.optTextFile.Checked = True
        Me.optTextFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.5!)
        Me.optTextFile.ForeColor = System.Drawing.SystemColors.ControlText
        Me.optTextFile.Location = New System.Drawing.Point(3, 16)
        Me.optTextFile.Margin = New System.Windows.Forms.Padding(0)
        Me.optTextFile.Name = "optTextFile"
        Me.optTextFile.Size = New System.Drawing.Size(87, 19)
        Me.optTextFile.TabIndex = 3
        Me.optTextFile.TabStop = True
        Me.optTextFile.Text = "Text File"
        Me.optTextFile.UseVisualStyleBackColor = False
        '
        'Atlas_SFP
        '
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.Controls.Add(Me.txtATLAS)
        Me.Controls.Add(Me.fraAtlas_0)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Name = "Atlas_SFP"
        Me.Size = New System.Drawing.Size(398, 210)
        Me.fraAtlas_0.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

End Sub

#End Region

    '=========================================================
    'Property Variables:
    Dim m_ATLAS As String
    Dim m_Parent_Object As Object
    'Default Property Values:
    Const m_def_ATLAS As String = ""

    Private Sub cmdAtlas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdAtlas.Click

        Dim objParent As Object
        Dim sATLAS_Statement As String

        '   Connect to parent object
        objParent = m_Parent_Object

        '   Execute parent object ATLAS built function
        sATLAS_Statement = objParent.Build_Atlas

        '   If statement returned append to ATLAS text window
        If sATLAS_Statement <> "" Then

            If optClipboard.Checked = True Then
                '           Send to clipboard
                Clipboard.Clear()
                Clipboard.SetText(sATLAS_Statement)
            Else
                '           Send to ATLAS text window
                txtATLAS.Text = txtATLAS.Text & sATLAS_Statement & vbCrLf
            End If

        End If

    End Sub

    Private Sub cmdClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdClear.Click
        txtATLAS.Text = ""
    End Sub

    Private Sub cmdOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdOpen.Click

        Try ' On Error GoTo ErrHandler
            ' Set filters
            CommonDialog_Load.Filter = "All Files (*.*)|*.*|Data Files" & "(*.dat)|*.dat|Batch Files (*.bat)|*.bat|Text Files (*.txt)|*.txt"

            ' Specify default filter
            CommonDialog_Load.FilterIndex = 4

            ' Display the Open dialog box
            CommonDialog_Load.ShowDialog()

            ' Open the specified file
            Dim reader As System.IO.StreamReader = New IO.StreamReader(CommonDialog_Load.FileName)
            txtATLAS.Text = reader.ReadToEnd()
            reader.Close()

        Catch   ' ErrHandler:
            ' User pressed the Cancel button
            Exit Sub

        End Try
    End Sub

    Private Sub cmdSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdSave.Click

        Try ' On Error GoTo ErrHandler
            ' Set filters
            CommonDialog_Save.Filter = "All Files (*.*)|*.*|Data Files" & "(*.dat)|*.dat|Batch Files (*.bat)|*.bat|Text Files (*.txt)|*.txt"

            ' Specify default filter
            CommonDialog_Save.FilterIndex = 4

            ' Display the Open dialog box
            If CommonDialog_Save.ShowDialog() = DialogResult.OK Then
                ' Save to specified file
                Dim writer As System.IO.StreamWriter = New IO.StreamWriter(CommonDialog_Save.FileName, False)
                writer.Write(txtATLAS.Text)
                writer.Close()
            End If

        Catch   ' ErrHandler:
            ' User pressed the Cancel button
            Exit Sub

        End Try
    End Sub

    Private Sub optClipboard_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optClipboard.Click
        cmdAtlas.Enabled = True
    End Sub

    Private Sub optTextFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles optTextFile.Click
        cmdAtlas.Enabled = True
    End Sub

    'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
    'MemberInfo=9,0,0,0
    Public Property Parent_Object() As Object
        Get
            Parent_Object = m_Parent_Object
        End Get
        '-----
        Set(ByVal New_Parent_Object As Object)
            m_Parent_Object = New_Parent_Object
            'PropertyChanged("Parent_Object")
        End Set
    End Property



    'Load property values from storage
'#Const defUse_UserControl_ReadProperties = True
#If defUse_UserControl_ReadProperties Then
    Private Sub UserControl_ReadProperties()	'? ByVal PropBag As PropertyBag

        m_Parent_Object = PropBag.ReadProperty("Parent_Object", Nothing)
        m_ATLAS = PropBag.ReadProperty("ATLAS", m_def_ATLAS)
    End Sub
#End If

    'Write property values to storage
'#Const defUse_UserControl_WriteProperties = True
#If defUse_UserControl_WriteProperties Then
    Private Sub UserControl_WriteProperties()	'? ByVal PropBag As PropertyBag

        PropBag.WriteProperty("Parent_Object", m_Parent_Object, Nothing)
        PropBag.WriteProperty("ATLAS", m_ATLAS, m_def_ATLAS)
    End Sub
#End If

    'WARNING! DO NOT REMOVE OR MODIFY THE FOLLOWING COMMENTED LINES!
    'MemberInfo=13,0,0,
    Public Property ATLAS() As String
        Get
            ATLAS = m_ATLAS
        End Get
        '-----
        Set(ByVal New_ATLAS As String)
            m_ATLAS = New_ATLAS
            'PropertyChanged("ATLAS")
        End Set
    End Property

    'Initialize Properties for User Control
'#Const defUse_UserControl_InitProperties = True
#If defUse_UserControl_InitProperties Then
    Private Sub UserControl_InitProperties()
        m_ATLAS = m_def_ATLAS
    End Sub
#End If

End Class