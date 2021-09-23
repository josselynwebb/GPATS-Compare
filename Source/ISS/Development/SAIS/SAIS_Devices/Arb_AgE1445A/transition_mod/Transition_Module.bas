Attribute VB_Name = "Module1"
Option Explicit

Public Function atxml_CallDriverFunction(ParamArray vntParameters() As Variant) As Long
   
   Dim vntParms() As Variant
   
   vntParms = vntParameters
   
   atxml_CallDriverFunction = atxml_CallDriverFunction2(vntParms)
   
End Function

Private Function atxml_CallDriverFunction2(ByRef vntParameters As Variant) As Boolean
   
   Dim strMsg     As String
   Dim lngCounter As Long
   
   strMsg = "Parameter Array = " & Chr(13) & Chr(13)
   For lngCounter = LBound(vntParameters) To UBound(vntParameters)
      
      If IsMissing(vntParameters(lngCounter)) = True Then
         strMsg = strMsg & "MISSING" & Chr(13)
         
      ElseIf IsEmpty(vntParameters(lngCounter)) = True Then
         strMsg = strMsg & "EMPTY" & Chr(13)
         
      ElseIf IsNull(vntParameters(lngCounter)) = True Then
         strMsg = strMsg & "NULL" & Chr(13)
         
      ElseIf IsArray(vntParameters(lngCounter)) = True Then
         strMsg = strMsg & "ARRAY (" & CStr(LBound(vntParameters(lngCounter))) & " To " & CStr(UBound(vntParameters(lngCounter))) & ")" & Chr(13)
         
      ElseIf IsObject(vntParameters(lngCounter)) = True Then
         strMsg = strMsg & "OBJECT (" & CStr(ObjPtr(vntParameters(lngCounter))) & ")" & Chr(13)
         
      Else
         strMsg = strMsg & "VALUE = " & CStr(vntParameters(lngCounter)) & Chr(13)
      End If
   Next
   
   MsgBox strMsg, vbOKOnly + vbInformation, " "
   atxml_CallDriverFunction2 = 0
   
End Function





















