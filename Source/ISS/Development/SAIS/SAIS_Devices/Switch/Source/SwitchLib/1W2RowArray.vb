Public Class ctl1W2RowArray
    Private _count As Integer = 0
    Private Const SWITCH_WIDTH = 18
    Private _switches() As ctl1W2Row

    ''' <summary>
    ''' Get or Set the number of switches in the array
    ''' </summary>
    Property Count() As Integer
        Get
            Return _count
        End Get
        Set(value As Integer)
            _count = value
            BuildArray()
        End Set
    End Property

    ''' <summary>
    ''' Gets an instance of the switch at an index
    ''' </summary>
    ''' <param name="index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Switches(ByVal index As Integer) As SwitchLib.ctl1W2Row
        Get
            Return _switches(index)
        End Get
        Private Set(value As SwitchLib.ctl1W2Row)
        End Set
    End Property

    'Create Click Event
    Public Event SwitchClick(ByVal sender As Object, ByVal e As SwitchEventArgs)

    Public Sub New()
    ' This call is required by the designer.
    InitializeComponent()

    BuildArray()
    End Sub

    Private Sub BuildArray()
        Dim i As Integer

        If _count > 0 Then
            ReDim _switches(_count - 1)
            Me.Width = SWITCH_WIDTH * _count

            For i = 0 To _count - 1
                _switches(i) = New ctl1W2Row()
                _switches(i).Index = i
                _switches(i).Location = New Point(SWITCH_WIDTH * i, 0)
                Me.Controls.Add(_switches(i))
                AddHandler _switches(i).SwitchClick, AddressOf ClickHandler
            Next

            _switches(_count - 1).IsCorner = True
        End If
    End Sub

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As SwitchEventArgs)
        RaiseEvent SwitchClick(sender, e)
    End Sub
End Class
