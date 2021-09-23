VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
  Persistable = 0  'NotPersistable
  DataBindingBehavior = 0  'vbNone
  DataSourceBehavior  = 0  'vbNone
  MTSTransactionMode  = 0  'NotAnMTSObject
END
Attribute VB_Name = "cTETS"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
Attribute VB_Exposed = True
Option Explicit
DefInt A-Z

Public Function bTETSInit(bRequiredInstruments() As Boolean) As Boolean

    ReDim bInstrRequired(1 To UBound(bRequiredInstruments))
    bInstrRequired = bRequiredInstruments
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON: Exit Function
    bSystemInitialized = iTETSInit()
    bTETSInit = bSystemInitialized
    If UserEvent = ABORT_BUTTON Then Err.Raise USER_EVENT + ABORT_BUTTON
    
End Function

Public Sub ResetSystem()

    bSystemResetComplete = False
    If bSystemInitialized Then TETS.ResetSystem
    If bSystemInitialized Then TETS.ReleaseSystem
    bSystemResetComplete = True
    
End Sub

Public Sub ResetInstruments()

    If bSystemInitialized Then TETS.ResetSystem
    
End Sub
