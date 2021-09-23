Public Class ctl2W2Row
    Public Enum SwitchStates
        Open
        Closed
    End Enum

    Private _isCorner As Boolean = False
    Private _topSwitchState As SwitchStates = SwitchStates.Open
    Private _bottomSwitchState As SwitchStates = SwitchStates.Open
    Private _index As Integer
    Private _topSwitchText As String
    Private _bottomSwitchText As String

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
    ''' Current state of the top switch
    ''' </summary>
    ''' <remarks></remarks>
    Property TopSwitchState() As SwitchStates
        Get
            Return _topSwitchState
        End Get
        Set(value As SwitchStates)
            _topSwitchState = value
            SetSwitchImage()
        End Set
    End Property

    ''' <summary>
    ''' Current state of the bottom switch
    ''' </summary>
    ''' <remarks></remarks>
    Property BottomSwitchState() As SwitchStates
        Get
            Return _bottomSwitchState
        End Get
        Set(value As SwitchStates)
            _bottomSwitchState = value
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
    ''' Holds the Text property of the top switch
    ''' </summary>
    Property TopSwitchText() As String
        Get
            Return _topSwitchText
        End Get
        Set(value As String)
            _topSwitchText = value
            SetSwitchImage()
        End Set
    End Property

    ''' <summary>
    ''' Holds the Text property of the bottom switch
    ''' </summary>
    Property BottomSwitchText() As String
        Get
            Return _bottomSwitchText
        End Get
        Set(value As String)
            _bottomSwitchText = value
            SetSwitchImage()
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
    ''' Numeric designator for top left label
    ''' </summary>
    ''' <remarks>Must be numeric between 1 and 99</remarks>
    Property Pin1Text() As String
        Get
            Return lblPin1.Text
        End Get
        Set(value As String)
            If IsNumeric(value) And value.Length > 0 And value.Length < 3 Then
                lblPin1.Text = value
            Else
                Throw New SystemException("Invalid value.  Labels must be numeric and two digits or less")
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns an instance of the pin label object
    ''' </summary>
    Property Pin2Label() As Label
        Get
            Return lblPin2
        End Get
        Private Set(value As Label)

        End Set
    End Property

    ''' <summary>
    ''' Numeric designator for top right label
    ''' </summary>
    ''' <remarks>Must be numeric between 1 and 99</remarks>
    Property Pin2Text() As String
        Get
            Return lblPin2.Text
        End Get
        Set(value As String)
            If IsNumeric(value) And value.Length > 0 And value.Length < 3 Then
                lblPin2.Text = value
            Else
                Throw New SystemException("Invalid value.  Labels must be numeric and two digits or less")
            End If
        End Set
    End Property

    'Create Click Event
    Public Event SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs)

    Private Sub ctl1W1Row_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.picTopSwitch.Image = Iml2W2Row.Images(0)
        Me.picBottomSwitch.Image = Iml2W2Row.Images(2)
    End Sub

    Private Sub picTopSwitch_Click(sender As Object, e As MouseEventArgs) Handles picTopSwitch.Click
        Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        If _topSwitchState = SwitchStates.Closed Then
            _topSwitchState = SwitchStates.Open
        Else
            _topSwitchState = SwitchStates.Closed
        End If
        SetSwitchImage()
        switchEvent.WhichSwitch = SwitchEventArgs.SwitchLocations.Top
        switchEvent.Index = _index
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub picBottomSwitch_Click(sender As Object, e As MouseEventArgs) Handles picBottomSwitch.Click
        Dim switchEvent As SwitchEventArgs = New SwitchEventArgs()

        If _bottomSwitchState = SwitchStates.Closed Then
            _bottomSwitchState = SwitchStates.Open
        Else
            _bottomSwitchState = SwitchStates.Closed
        End If
        SetSwitchImage()
        switchEvent.WhichSwitch = SwitchEventArgs.SwitchLocations.Bottom
        switchEvent.Index = _index
        RaiseEvent SwitchClick(sender, switchEvent)
    End Sub

    Private Sub SetSwitchImage()
        If _topSwitchState = SwitchStates.Closed Then
            If _isCorner Then
                picTopSwitch.Image = Iml2W2Row.Images(5)
            Else
                picTopSwitch.Image = Iml2W2Row.Images(1)
            End If
        Else
            If _isCorner Then
                picTopSwitch.Image = Iml2W2Row.Images(4)
            Else
                picTopSwitch.Image = Iml2W2Row.Images(0)
            End If
        End If
        If _bottomSwitchState = SwitchStates.Closed Then
            If _isCorner Then
                picBottomSwitch.Image = Iml2W2Row.Images(7)
            Else
                picBottomSwitch.Image = Iml2W2Row.Images(3)
            End If
        Else
            If _isCorner Then
                picBottomSwitch.Image = Iml2W2Row.Images(6)
            Else
                picBottomSwitch.Image = Iml2W2Row.Images(2)
            End If
        End If
        picTopSwitch.Refresh()
        picBottomSwitch.Refresh()
    End Sub

    Private Sub lblPi1_TextChanged(sender As Object, e As EventArgs) Handles lblPin1.TextChanged
        If Not IsNumeric(lblPin1.Text) Or lblPin1.Text.Length <= 0 Or lblPin1.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be numeric and two digits or less")
        End If
    End Sub

    Private Sub lblPin2_TextChanged(sender As Object, e As EventArgs) Handles lblPin2.TextChanged
        If Not IsNumeric(lblPin2.Text) Or lblPin2.Text.Length <= 0 Or lblPin2.Text.Length >= 3 Then
            Throw New SystemException("Invalid value.  Labels must be numeric and two digits or less")
        End If
    End Sub
End Class
