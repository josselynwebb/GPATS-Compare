Public Class frmOperatorMsg
    Inherits System.Windows.Forms.Form

    Private Sub cmdContinue_Click(sender As Object, e As EventArgs) Handles cmdContinue.Click
        Continue_Click()
    End Sub

    Private Sub Continue_Click()
        If InputData.Text <> "" And Me.InputData.Visible = True Then
            UserInputData = InputData.Text
            Me.Hide()
        Else
            'added by Soon Nam 7/17/03
            'following line fixes a bug that sets UserInputData equal to
            'previous data if nothing was entered at user prompt
            UserInputData = ""
            'end add
            Me.Hide()
        End If
    End Sub

    Private Sub frmOperatorMsg_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        If Asc(eventArgs.KeyChar) = System.Windows.Forms.Keys.Return Then Continue_Click()
    End Sub

    Private Sub InputData_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles InputData.KeyPress
        If Asc(eventArgs.KeyChar) = System.Windows.Forms.Keys.Return Then Continue_Click()
    End Sub

    Public Function lblMsg(ByVal Index As Integer) As Label
        Dim labelName As String = "lblMsg_" & Index.ToString()
        Dim parent As Control = lblMsg_1.Parent

        If LabelExists(labelName, parent) Then
            lblMsg = GetLabel(labelName, parent)
        Else
            Dim lbl As New Label
            lbl.Name = labelName
            lbl.BackColor = lblMsg_1.BackColor
            lbl.Cursor = lblMsg_1.Cursor
            lbl.Font = lblMsg_1.Font
            lbl.ForeColor = lblMsg_1.ForeColor
            lbl.RightToLeft = lblMsg_1.RightToLeft
            lbl.BorderStyle = lblMsg_1.BorderStyle
            lbl.Left = lblMsg_1.Left
            lbl.Size = lblMsg_1.Size
            lbl.TextAlign = lblMsg_1.TextAlign
            lbl.Visible = lblMsg_1.Visible
            parent.Controls.Add(lbl)
            lblMsg = lbl
        End If
    End Function

    ''' <summary>
    ''' This function check for the existance of a label on this form
    ''' </summary>
    ''' <param name="LabelName">Name property of the label in question</param>
    ''' <param name="parent">Reference to the parent of the label in question</param>
    ''' <returns>true if the label exists otherwise false</returns>
    ''' <remarks></remarks>
    Private Function LabelExists(ByVal LabelName As String, ByRef parent As Control) As Boolean
        Dim retVal As Boolean = False

        For Each ctrl As Control In parent.Controls
            If (TypeOf ctrl Is Label) And (ctrl.Name = LabelName) Then
                Return True
            End If
        Next
        Return False
    End Function

    ''' <summary>
    ''' Gets the instance of a label by name.  This function should only be called after a call 
    ''' to LabelExists confirms the label exists or the return must be checked for 'Nothing'
    ''' </summary>
    ''' <param name="LabelName">Name property of the label of interest</param>
    ''' <param name="parent">Reference to the parent of the label of interest</param>
    ''' <returns>Reference to an instance of label with the desired name property or 'Nothing' if thelable does not exist</returns>
    ''' <remarks></remarks>
    Private Function GetLabel(ByVal LabelName As String, ByRef parent As Control) As Label
        For Each ctrl As Control In parent.Controls
            If (TypeOf ctrl Is Label) And (ctrl.Name = LabelName) Then
                Return ctrl
            End If
        Next
        Return Nothing
    End Function

    Private Sub frmOperatorMsg_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        Me.cmdContinue.SetBounds(Limit(Me.Width - 156, 592), Limit(Me.Height - 79, 137), 0, 0, BoundsSpecified.Location)
        Me.InputData.SetBounds(Limit(Me.Width - 557, 191), Limit(Me.Height - 78, 138), 0, 0, BoundsSpecified.Location)
        Me.lblInput.SetBounds(Limit(Me.Width - 613, 135), Limit(Me.Height - 77, 139), 0, 0, BoundsSpecified.Location)
        Me.SSPanel1.SetBounds(8, 8, Limit(Me.Width - 50, 524), Limit(Me.Height - 99, 66), BoundsSpecified.Size)
        Me.msgInBox.SetBounds(0, 0, Limit(Me.SSPanel1.Width - 22, 685), Limit(Me.SSPanel1.Height - 16, 50), BoundsSpecified.Size)
    End Sub

    Private Sub frmOperatorMsg_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        frmOperatorMsg_Resize(Me, New System.EventArgs())
    End Sub
End Class