Imports Microsoft.Win32

Public Class CICLKernelService

    Public ciclProcess As Process = New Process()

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        Dim activeCICLProcesses() As Process = Process.GetProcessesByName("CiclKernelC")
        Dim i = 0
        While i < activeCICLProcesses.Length()
            activeCICLProcesses(i).Kill()
        End While

        ciclProcess.StartInfo.FileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "CICLKernelPath", Nothing)
        ciclProcess.StartInfo.WindowStyle = ProcessWindowStyle.Minimized
        ciclProcess.Start()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        ciclProcess.Kill()

        'Just in case someone closed and respawned the cicl
        'Dim activeCICLProcesses() As Process = Process.GetProcessesByName("CiclKernelC")
        'Dim i = 0
        'While i < activeCICLProcesses.Length()
        '    activeCICLProcesses(i).Kill()
        'End While
    End Sub

End Class
