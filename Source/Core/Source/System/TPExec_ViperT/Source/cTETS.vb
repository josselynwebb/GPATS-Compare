Option Strict Off
Option Explicit On

<System.Runtime.InteropServices.ProgId("cTETS_NET.cTETS")> Public Class cTETS
    Public Function bTetsInit(ByRef bRequiredInstruments() As Boolean) As Boolean
        bTetsInit = False
        ReDim bInstrRequired(UBound(bRequiredInstruments))
        Array.Copy(bRequiredInstruments, bInstrRequired, bRequiredInstruments.Length)
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON) : Exit Function
        bSystemInitialized = iTETSInit()
        bTetsInit = bSystemInitialized 'copy array
        If UserEvent = ABORT_BUTTON Then Err.Raise(USER_EVENT + ABORT_BUTTON)
    End Function
	
	Public Sub ResetSystem()
		bSystemResetComplete = False
		If bSystemInitialized Then TETS.ResetSystem()
		If bSystemInitialized Then TETS.ReleaseSystem()
		bSystemResetComplete = True
	End Sub
End Class