Public Class SPSTArray
    Private _count As Integer = 0
    Private _columns As Integer = 0
    Private _rows As Integer = 0
    Private Const SWITCH_WIDTH = 94
    Private Const SWITCH_HEIGHTH = 18
    Private _switches(,) As SPST

    ''' <summary>
    ''' Get or Set the number of columns of switches in the array
    ''' </summary>
    ''' <remarks>Indexing is by column top down then by row left to right</remarks>
    Property Columns() As Integer
        Get
            Return _columns
        End Get
        Set(value As Integer)
            _columns = value
            _count = _columns * _rows
            BuildArray()
        End Set
    End Property

    ''' <summary>
    ''' Get or Set the number of rows of switches in the array
    ''' </summary>
    ''' <remarks>Indexing is by column top down then by row left to right</remarks>
    Property Rows() As Integer
        Get
            Return _rows
        End Get
        Set(value As Integer)
            _rows = value
            _count = _columns * _rows
            BuildArray()
        End Set
    End Property

    ''' <summary>
    ''' Total number of switches in the array
    ''' </summary>
    Property Count() As Integer
        Get
            Return _count
        End Get
        Private Set(value As Integer)

        End Set
    End Property

    ''' <summary>
    ''' Gets an instance of the switch at an index
    ''' </summary>
    ''' <param name="index"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Switches(ByVal index As Integer) As SwitchLib.SPST
        Get
            Dim c, r As Integer

            For c = 0 To _columns - 1
                For r = 0 To _rows - 1
                    If _switches(r, c).Index = index Then
                        Return _switches(r, c)
                    End If
                Next
            Next
            Throw New System.Exception("Index out of range.  Max index: " + _count.ToString() + " Requested index: " + index.ToString())
        End Get
        Private Set(value As SwitchLib.SPST)
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
        Dim c, r, i As Integer

        i = 0
        If _columns > 0 And _rows > 0 Then
            ReDim _switches(_rows - 1, _columns - 1)
            Me.Width = SWITCH_WIDTH * _columns
            Me.Height = SWITCH_HEIGHTH * _rows

            For c = 0 To _columns - 1
                For r = 0 To _rows - 1
                    _switches(r, c) = New SPST()
                    _switches(r, c).Index = i
                    _switches(r, c).Row = r
                    _switches(r, c).Column = c
                    _switches(r, c).Location = New Point(SWITCH_WIDTH * c, SWITCH_HEIGHTH * r)
                    Me.Controls.Add(_switches(r, c))
                    AddHandler _switches(r, c).SwitchClick, AddressOf ClickHandler
                    i += 1
                Next
            Next
        End If
    End Sub

    Public Sub ClickHandler(ByVal sender As Object, ByVal e As SwitchEventArgs)
        RaiseEvent SwitchClick(sender, e)
    End Sub
End Class
