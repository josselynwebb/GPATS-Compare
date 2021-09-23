Public Class ctl1x6Mux
    Public Enum SwitchStates
        Pin1
        Pin2
        Pin3
        Pin4
        Pin5
        Pin6
        Pin7
    End Enum

    Private _switchState As SwitchStates = SwitchStates.Pin7

    ''' <summary>
    ''' Current state of the switch
    ''' </summary>
    Property SwitchState() As SwitchStates
        Get
            Return _switchState
        End Get
        Set(value As SwitchStates)
            _switchState = value
            SetSwitchImage()
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the top pin label object
    ''' </summary>
    Property Pin1Label() As Label
        Get
            Return lblPin1
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' string designator for the top pin label
    ''' </summary>
    Property Pin1Text() As String
        Get
            Return lblPin1.Text
        End Get
        Set(value As String)
            lblPin1.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the second from top pin label object
    ''' </summary>
    Property Pin2Label() As Label
        Get
            Return lblPin2
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' string designator for the second from top pin label
    ''' </summary>
    Property Pin2Text() As String
        Get
            Return lblPin2.Text
        End Get
        Set(value As String)
            lblPin2.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the third from top pin label object
    ''' </summary>
    Property Pin3Label() As Label
        Get
            Return lblPin3
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for the third from top pin label
    ''' </summary>
    Property Pin3Text() As String
        Get
            Return lblPin3.Text
        End Get
        Set(value As String)
            lblPin3.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the fourth from  top pin label object
    ''' </summary>
    Property Pin4Label() As Label
        Get
            Return lblPin4
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for the fourth from  top pin label
    ''' </summary>
    Property Pin4Text() As String
        Get
            Return lblPin4.Text
        End Get
        Set(value As String)
            lblPin4.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the fifth from top pin label object
    ''' </summary>
    Property Pin5Label() As Label
        Get
            Return lblPin5
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for the fifth from top pin label
    ''' </summary>
    Property Pin5Text() As String
        Get
            Return lblPin5.Text
        End Get
        Set(value As String)
            lblPin5.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the sixth from top pin label object
    ''' </summary>
    Property Pin6Label() As Label
        Get
            Return lblPin6
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for sixth from top pin label
    ''' </summary>
    Property Pin6Text() As String
        Get
            Return lblPin6.Text
        End Get
        Set(value As String)
            lblPin6.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the bottom pin label object
    ''' </summary>
    Property Pin7Label() As Label
        Get
            Return lblPin7
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for bottom pin label
    ''' </summary>
    Property Pin7Text() As String
        Get
            Return lblPin7.Text
        End Get
        Set(value As String)
            lblPin7.Text = value
        End Set
    End Property

    'Create Click Event
    Public Event SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs)

    Private Sub ctl1x6Mux_Load(sender As Object, e As EventArgs) Handles Me.Load
        _switchState = SwitchStates.Pin7
        SetSwitchImage()
    End Sub

    Private Sub lblPin1_Click(sender As Object, e As EventArgs) Handles lblPin1.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin1
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub lblPin2_Click(sender As Object, e As EventArgs) Handles lblPin2.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin2
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub lblPin3_Click(sender As Object, e As EventArgs) Handles lblPin3.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin3
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub lblPin4_Click(sender As Object, e As EventArgs) Handles lblPin4.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin4
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub lblPin5_Click(sender As Object, e As EventArgs) Handles lblPin5.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin5
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub lblPin6_Click(sender As Object, e As EventArgs) Handles lblPin6.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin6
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub lblPin7_Click(sender As Object, e As EventArgs) Handles lblPin7.Click
         Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        _switchState = SwitchStates.Pin7
        SetSwitchImage()
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub SetSwitchImage()
        Select Case _switchState
            Case SwitchStates.Pin1
        picSwitch.Image = Iml1x6Mux.Images(0)

            Case SwitchStates.Pin2
        picSwitch.Image = Iml1x6Mux.Images(1)

            Case SwitchStates.Pin3
        picSwitch.Image = Iml1x6Mux.Images(2)

            Case SwitchStates.Pin4
        picSwitch.Image = Iml1x6Mux.Images(3)

            Case SwitchStates.Pin5
        picSwitch.Image = Iml1x6Mux.Images(4)

            Case SwitchStates.Pin6
        picSwitch.Image = Iml1x6Mux.Images(5)

            Case SwitchStates.Pin7
        picSwitch.Image = Iml1x6Mux.Images(6)
        End Select
        picSwitch.Refresh()
    End Sub

    Private Sub lblPi1_TextChanged(sender As Object, e As EventArgs) Handles lblPin1.TextChanged
        If lblPin1.Text.Length <= 0 Or lblPin1.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub

    Private Sub lblPin2_TextChanged(sender As Object, e As EventArgs) Handles lblPin2.TextChanged
        If lblPin2.Text.Length <= 0 Or lblPin2.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub

    Private Sub lblPin3_TextChanged(sender As Object, e As EventArgs) Handles lblPin3.TextChanged
        If lblPin3.Text.Length <= 0 Or lblPin3.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub

    Private Sub lblPin4_TextChanged(sender As Object, e As EventArgs) Handles lblPin4.TextChanged
        If lblPin4.Text.Length <= 0 Or lblPin4.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub

    Private Sub llblPin5_TextChanged(sender As Object, e As EventArgs) Handles lblPin5.TextChanged
        If lblPin5.Text.Length <= 0 Or lblPin5.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub

    Private Sub lblPin6_TextChanged(sender As Object, e As EventArgs) Handles lblPin6.TextChanged
        If lblPin6.Text.Length <= 0 Or lblPin6.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub

    Private Sub lblPin7_TextChanged(sender As Object, e As EventArgs) Handles lblPin7.TextChanged
        If lblPin7.Text.Length <= 0 Or lblPin7.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be two characters or less")
        End If
    End Sub
End Class
