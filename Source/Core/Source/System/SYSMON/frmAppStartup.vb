Public Class frmAppStartup

    Private Shared WM_QUERYENDSESSION As Integer = &H11
    Private Shared WM_ENDSESSION As Integer = &H16
    Private Shared ENDSESSION_LOGOFF As Integer = &H80000000

    Public Shared systemShutdown As Boolean = False


    Private Declare Function ShutdownBlockReasonCreate Lib "user32.dll" (ByVal hWnd As IntPtr, _
                                                                         <Runtime.InteropServices.MarshalAs(Runtime.InteropServices.UnmanagedType.LPWStr)> ByVal reason As String) As Boolean
    Public Declare Function ShutdownBlockReasonDestroy Lib "user32.dll" (ByVal hWnd As IntPtr) As Boolean

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub frmAppStartup_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ShutdownBlockReasonCreate(Me.Handle, "Checking Temperature drop.  Please click on cancel, then allow the dialog to control shutdown/logoff.")
    End Sub

    Private Sub TimerStart_Tick(sender As Object, e As EventArgs) Handles TimerStart.Tick
        Me.TimerStart.Enabled = False
        Me.Hide()
        SysmonMain.Main()
    End Sub


    Private Sub frmAppStartup_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If systemShutdown Then  ' whatever the viable is in the WndProc
            e.Cancel = True
        End If
    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        Try
            If m.Msg = WM_QUERYENDSESSION Then
                Dim iLParam As Integer = m.LParam.ToInt32()
                If iLParam = ENDSESSION_LOGOFF Then
                        LOGOUT_FROM_SYSMON = True
                Else
                        LOGOUT_FROM_SYSMON = False
                End If

            ElseIf m.Msg = WM_ENDSESSION Then
                Dim iLParam As Integer = m.LParam.ToInt32()
                If iLParam = ENDSESSION_LOGOFF Then
                        LOGOUT_FROM_SYSMON = True
                Else
                        LOGOUT_FROM_SYSMON = False
                End If
                ShutDownSysmon()
            End If

        Catch ex As System.Exception
            MessageBox.Show(ex.Message)
        End Try
        MyBase.WndProc(m)
    End Sub


End Class