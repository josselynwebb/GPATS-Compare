
Public Module DAOSupport

    Friend pDAODBEngine As New DAO.DBEngine

    Public Function CompareErrNumber(ByVal sConditon As String, ByVal Value As Integer) As Boolean
        Dim te As Integer = Err.Number
        ' te = xFun(te)

        Select Case sConditon
            Case "<=" : Return te <= Value
            Case ">=" : Return te >= Value
            Case "<>" : Return te <> Value
            Case "=" : Return te = Value
            Case ">" : Return te > Value
            Case "<" : Return te < Value
            Case Else
                Return False    ' - ???
        End Select

    End Function

    Public Sub ResumeNext(Optional ByVal sFunction As String = "")
        ' ...
    End Sub

End Module
