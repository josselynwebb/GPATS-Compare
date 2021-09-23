'Option Strict Off
'Option Explicit On

Imports System

Public Module SPLASH


	'=========================================================
'#Const defUse_SplashStart = True
#If defUse_SplashStart Then
    Public Sub SplashStart()

        '! Load frmAbout
        frmAbout.cmdOK.Visible = False
        frmAbout.Show()
        Application.DoEvents()

    End Sub
#End If




'#Const defUse_SplashEnd = True
#If defUse_SplashEnd Then
    Public Sub SplashEnd()

        frmAbout.Hide()

    End Sub
#End If


End Module