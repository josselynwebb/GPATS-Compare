Option Strict Off
Option Explicit On
Module ADO
	
	Public Cn As ADODB.Connection ' Connection to Database
	
	Public Function rsOpen(ByRef sql As String, Optional ByRef CursorType As ADODB.CursorTypeEnum = ADODB.CursorTypeEnum.adOpenStatic, Optional ByRef LockType As ADODB.LockTypeEnum = ADODB.LockTypeEnum.adLockOptimistic) As ADODB.Recordset
		Dim rs As ADODB.Recordset
		
		rs = New ADODB.Recordset
		
		rs.Open(sql, Cn, CursorType, LockType)
		rsOpen = rs
	End Function
	
	Public Sub OpenDB(ByRef DSN As String)
		Cn = New ADODB.Connection
		Cn.Open(DSN)
	End Sub
	
	Public Sub CloseDB(ByRef DSN As String)
		'    Set Cn = ADODB.adRsnClose
		Cn.Close()
	End Sub
	
	Public Sub DeleteFiles(ByRef S As String)
		Dim retval As Object
		Cn.Execute(S)
	End Sub
	
	Public Function deColonIt(ByRef S As String) As String
		Dim s1 As String
		Dim i As Short
		
		s1 = ""
		For i = 1 To Len(S)
			If Mid(S, i, 1) = ":" Then
				s1 = s1 & ";"
				
			ElseIf Mid(S, i, 1) = "<" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = ">" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = "\" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = "?" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = """" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = "/" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = "*" Then 
				s1 = s1 & "_"
				
			ElseIf Mid(S, i, 1) = "|" Then 
				s1 = s1 & "_"
				
			Else
				s1 = s1 & Mid(S, i, 1)
			End If
			
		Next i
		deColonIt = s1
	End Function
	
    Public Function SQLize(ByVal S As String) As String
        Dim s1 As String
        Dim i As Short

        s1 = ""
        For i = 1 To Len(S)
            If Mid(S, i, 1) = "'" Then
                s1 = s1 & "''"
            Else
                s1 = s1 & Mid(S, i, 1)
            End If
        Next i
        SQLize = s1
    End Function
	
	Public Function GetDBString(ByRef rs As ADODB.Recordset, ByRef Field As String) As String
		
		If IsDbNull(rs.Fields(Field).Value) = True Then
			GetDBString = ""
		Else
			GetDBString = rs.Fields(Field).Value
		End If
	End Function
	
	Public Function SearchForString() As String
		Dim searchString As String
		Dim searchString1 As String
		Dim tmpstring As String
		Dim BeenDone As Boolean
		
		BeenDone = False
		DateSelected = False
		
		If APSNameBeenSelected = True Then
			If BeenDone = False Then
				tmpstring = "Where APSName = '" & SQLize(selectedAPSName) & "'"
				BeenDone = True
			Else
				tmpstring = " AND APSName = '" & SQLize(selectedAPSName) & "'"
			End If
			searchString1 = searchString1 & tmpstring
		End If
		
		If TPSNameBeenSelected = True Then
			If BeenDone = False Then
				tmpstring = "Where TPSName = '" & SQLize(selectedTPSName) & "'"
				BeenDone = True
			Else
				tmpstring = " AND TPSName = '" & SQLize(selectedTPSName) & "'"
			End If
			searchString1 = searchString1 & tmpstring
		End If
		
		If SerialNumberBeenSelected = True Then
			If BeenDone = False Then
				tmpstring = "Where SerialNumber = '" & SQLize(selectedSerialNumber) & "'"
				BeenDone = True
			Else
				tmpstring = " AND SerialNumber = '" & SQLize(selectedSerialNumber) & "'"
			End If
			searchString1 = searchString1 & tmpstring
		End If
		
		If ERONumberBeenSelected = True Then
			If BeenDone = False Then
				tmpstring = "Where ERONumber = '" & SQLize(selectedERONumber) & "'"
				BeenDone = True
			Else
				tmpstring = " AND ERONumber = '" & SQLize(selectedERONumber) & "'"
			End If
			searchString1 = searchString1 & tmpstring
		End If
		
		If FromRunDateBeenSelected = True Or ToRunDateBeenSelected = True Then
			If BeenDone = False Then
				tmpstring = "Where ((RunDate>=#" & selectedFromRunDate & "#" & " AND RunDate<=#" & selectedToRunDate & "#))"
				BeenDone = True
			Else
				tmpstring = " AND ((RunDate>=#" & selectedFromRunDate & "#" & " AND RunDate<=#" & selectedToRunDate & "#))"
			End If
			searchString1 = searchString1 & tmpstring
			DateSelected = True
		End If
		
		SearchForString = searchString1
		
	End Function
	
	Public Sub CompressDatabase(ByRef oldFile As String)
		Dim newFile As String
		Dim DBE As DAO.DBEngine
		
		newFile = "D:\database\2.Mdb"
		DBE = DAODBEngine_definst
		DBE.CompactDatabase(oldFile, newFile)
		Kill(oldFile)
		Rename(newFile, oldFile)
		
	End Sub
End Module