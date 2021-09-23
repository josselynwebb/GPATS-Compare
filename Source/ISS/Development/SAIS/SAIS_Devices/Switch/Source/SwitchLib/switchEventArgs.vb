Public Class SwitchEventArgs
    Inherits EventArgs

    Public Enum SwitchLocations
        Top
        Bottom
    End Enum

    Private _whichSwitch As SwitchLocations = SwitchLocations.Top
    Private _index As Integer = 0

    ''' <summary>
    ''' Indicates switch position; top or bottom (if applicable)
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property WhichSwitch() As SwitchLocations
        Get
            Return _whichSwitch
        End Get
        Set(value As SwitchLocations)
            _whichSwitch = value
        End Set
    End Property

    ''' <summary>
    ''' Index into control array, if applicable
    ''' </summary>
    Property Index() As Integer
        Get
            Return _index
        End Get
        Set(value As Integer)
            _index = value
        End Set
    End Property
End Class
