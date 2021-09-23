
Friend Class frmAbout
    Inherits Form
    '**********************************************************************
    '***    United States Marine Corps                                  ***
    '***                                                                ***
    '***    Nomenclature:   Form "frmAbout" : FHDB_Processor            ***
    '***    Written By:     Dave Joiner                                 ***
    '***    Purpose:                                                    ***
    '***  ECP-TETS-023                                                  ***
    '***    To allow the User to View the Infomation about the          ***
    '***    FHDB_Processor.                                             ***
    '***
    '*** 
    '***    Updated 8/31/2015 by Jess Gillespie
    '***    - Update code from vs2006 to VS2012
    '***
    '**********************************************************************
    

    Private Sub frmAbout_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        'Updates Version number to reflect Project Properties.  DJoiner  07/19/2001
        lblInstrument(1).Text = My.Application.Info.Version.Major & "." & My.Application.Info.Version.Minor & "." & My.Application.Info.Version.Revision
    End Sub

    Private Sub cmdOk_Click(sender As Object, e As EventArgs) Handles cmdOk.Click
        Me.Close()
    End Sub
End Class