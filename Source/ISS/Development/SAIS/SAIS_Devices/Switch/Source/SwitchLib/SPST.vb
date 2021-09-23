Public Class SPST
    Public Enum SwitchStates
        Open
        Closed
    End Enum

    Private _switchState As SwitchStates = SwitchStates.Open
    Private _index As Integer
    Private _row As Integer
    Private _column As Integer

    ''' <summary>
    ''' Current state of the switch
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' Holds the index of the switch if it in a switch array
    ''' </summary>
    Property Index() As Integer
        Get
            Return _index
        End Get
        Set(value As Integer)
            _index = value
        End Set
    End Property

    ''' <summary>
    ''' Holds the row containing the switch if it in a switch array
    ''' </summary>
    Property Row() As Integer
        Get
            Return _row
        End Get
        Set(value As Integer)
            _row = value
        End Set
    End Property

    ''' <summary>
    ''' Holds the column containing the switch if it in a switch array
    ''' </summary>
    Property Column() As Integer
        Get
            Return _column
        End Get
        Set(value As Integer)
            _column = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the left pin label object
    ''' </summary>
    Property Pin1Label() As Label
        Get
            Return lblPin1
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for left pin label
    ''' </summary>
    ''' <remarks>Must be numeric between 1 and 99</remarks>
    Property Pin1Text() As String
        Get
            Return lblPin1.Text
        End Get
        Set(value As String)
            lblPin1.Text = value
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the right pin label object
    ''' </summary>
    Property Pin2Label() As Label
        Get
            Return lblPin2
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for right pin label
    ''' </summary>
    ''' <remarks>Must be numeric between 1 and 999</remarks>
    Property Pin2Text() As String
        Get
            Return lblPin2.Text
        End Get
        Set(value As String)
            lblPin2.Text = value
        End Set
    End Property

    'Create Click Event
    Public Event SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs)

    Private Sub ctlDPST_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.picSwitch.Image = ImlSPST.Images(0)
    End Sub

    Private Sub picSwitch_Click(sender As Object, e As EventArgs) Handles picSwitch.Click
        Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        If _switchState = SwitchStates.Closed Then
            _switchState = SwitchStates.Open
        Else
            _switchState = SwitchStates.Closed
        End If
        SetSwitchImage()
        switchEvent.Index = _index
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub SetSwitchImage()
        If _switchState = SwitchStates.Closed Then
            picSwitch.Image = ImlSPST.Images(1)
        Else
            picSwitch.Image = ImlSPST.Images(0)
        End If
        picSwitch.Refresh()
    End Sub

    Private Sub lblPi1_TextChanged(sender As Object, e As EventArgs) Handles lblPin1.TextChanged
        If Not IsNumeric(lblPin1.Text) Or lblPin1.Text.Length <= 0 Or lblPin1.Text.Length >= 4 Then
            Throw New SystemException("Invalid value.  Labels must be numeric and three digits or less")
        End If
    End Sub

    Private Sub lblPin2_TextChanged(sender As Object, e As EventArgs) Handles lblPin2.TextChanged
        If Not IsNumeric(lblPin2.Text) Or lblPin2.Text.Length <= 0 Or lblPin2.Text.Length >= 4 Then
            Throw New SystemException("Invalid value.  Labels must be numeric and three digits or less")
        End If
    End Sub
End Class
