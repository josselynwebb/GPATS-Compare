'Option Strict Off
'Option Explicit On

Imports System

Public Module Module1

	'=========================================================
    Public Sub SplashStart()

        '! Load frmAbout
        frmAbout.cmdOK.Visible = False
        frmAbout.Show()
        Application.DoEvents()

    End Sub

    Public Sub SplashEnd()
        frmAbout.Hide()
    End Sub
End Module