Public Class ctl1W1Row
    Public Enum SwitchStates
        Open
        Closed
    End Enum

    Private _isCorner As Boolean = False
    Private _switchState As SwitchStates = SwitchStates.Open
    Private _index As Integer

    ''' <summary>
    ''' Gets or sets whether or not the switch is the end of a line or corner 
    ''' and as such gets a different set of graphics
    ''' </summary>
    ''' <remarks></remarks>
    Property IsCorner() As Boolean
        Get
            Return _isCorner
        End Get
        Set(value As Boolean)
            _isCorner = value
            SetSwitchImage()
        End Set
    End Property

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
    ''' Returns an instance of the pin label object
    ''' </summary>
    Property Pin1Label() As Label
        Get
            Return lblPin1
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for the pin label
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

    'Create Click Event
    Public Event SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs)

    Private Sub ctl1W1Row_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.picSwitch.Image = Iml1W1Row.Images(0)
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
            If _isCorner Then
                picSwitch.Image = Iml1W1Row.Images(3)
            Else
                picSwitch.Image = Iml1W1Row.Images(1)
            End If
        Else
            If _isCorner Then
                picSwitch.Image = Iml1W1Row.Images(2)
            Else
                picSwitch.Image = Iml1W1Row.Images(0)
            End If
        End If
        picSwitch.Refresh()
    End Sub

    Private Sub lblPin1_TextChanged(sender As Object, e As EventArgs) Handles lblPin1.TextChanged
        If Not IsNumeric(lblPin1.Text) Or lblPin1.Text.Length <= 0 Or lblPin1.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be numeric and two digits or less")
        End If
    End Sub
End Class
