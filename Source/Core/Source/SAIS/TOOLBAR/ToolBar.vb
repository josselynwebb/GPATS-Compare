Option Strict Off
Option Explicit On
Friend Class frmToolBar
	Inherits System.Windows.Forms.Form
	
	
	
	Private Sub frmToolBar_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		If Button = VB6.MouseButtonConstants.RightButton Then
			' Display the context menu on right mouse click.
			
            'Me.PopupMenufrmPopUp.mnuBar)
			'PopupMenu Form1.mnuPop, vbPopupMenuRightButton, , , Form1.mnuStuff
		End If
		
		
	End Sub
	
	
	
	Private Sub frmToolBar_FormClosing(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
		Dim Cancel As Boolean = eventArgs.Cancel
		Dim UnloadMode As System.Windows.Forms.CloseReason = eventArgs.CloseReason
		
        ExitSaisToolBar()
		
        'eventArgs.Cancel = Cancel
	End Sub
	
	
	
	Private Sub Form_Terminate_Renamed()
		
        ExitSaisToolBar()
		
	End Sub
	
	Private Sub frmToolBar_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
        ExitSaisToolBar()
		
	End Sub
	
	
	Private Sub tbrInstruments_ButtonClick(ByVal eventSender As System.Object, ByVal eventArgs As AxComctlLib.IToolbarEvents_ButtonClickEvent) Handles tbrInstruments.ButtonClick
		
		LaunchApplication(eventArgs.Button)
		
	End Sub
	
	
	Private Sub tbrInstruments_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxComctlLib.IToolbarEvents_MouseDownEvent) Handles tbrInstruments.MouseDownEvent
		
		If eventArgs.Button = VB6.MouseButtonConstants.RightButton Then
			' Display the context menu on right mouse click.
			
            'Me.PopupMenu(frmPopUp.mnuBar)
			'PopupMenu Form1.mnuPop, vbPopupMenuRightButton, , , Form1.mnuStuff
		End If
		
	End Sub
	
	
	
	
	Private Sub tbrInstruments2_ButtonClick(ByVal eventSender As System.Object, ByVal eventArgs As AxComctlLib.IToolbarEvents_ButtonClickEvent) Handles tbrInstruments2.ButtonClick
		
		LaunchApplication(eventArgs.Button)
		
	End Sub
	
	Private Sub tbrInstruments2_MouseDownEvent(ByVal eventSender As System.Object, ByVal eventArgs As AxComctlLib.IToolbarEvents_MouseDownEvent) Handles tbrInstruments2.MouseDownEvent
		
		If eventArgs.Button = VB6.MouseButtonConstants.RightButton Then
			' Display the context menu on right mouse click.
			
            'Me.PopupMenu(frmPopUp.mnuBar)
			'PopupMenu Form1.mnuPop, vbPopupMenuRightButton, , , Form1.mnuStuff
		End If
		
	End Sub
	
	
	Public Sub Timer1_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Timer1.Tick
		
		Dim lParam As Integer
		Dim RetValue As Integer
		Dim bPrompted As Boolean
		
		'Inhibit EO buttons from re-enabling while EO Power on is in progress
		If bEOInitializing Then Exit Sub
        'Timer1.Enabled = False
		lParam = 1
		For InstrumentIndex = 0 To NUM_OF_SAIS_INSTRUMENTS
			SaisInstrument(InstrumentIndex) = True
		Next InstrumentIndex
        'RetValue = EnumWindows(lpEnumFunc, lParam)
		'Set toolbar according to state of SAIS EXE File
        For InstrumentIndex = 0 To NUM_OF_SAIS_INSTRUMENTS
            'If tbrInstruments.Buttons(InstrumentIndex + 1).Enabled <> SaisInstrument(InstrumentIndex) Then
            '    'tbrInstruments.Buttons(InstrumentIndex + 1).Enabled = SaisInstrument(InstrumentIndex)
            '    If (InstrumentIndex = EOV Or InstrumentIndex = IRWIN) And (SaisInstrument(EOV) = True Or SaisInstrument(IRWIN) = True) And bolEoPowerOn And Not bPrompted Then
            '        'Provide user option of leaving EO Power applied, but only prompt once
            '        bPrompted = True
            '        If MsgBox("Do you want to leave power applied to the EO module?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.SystemModal) = MsgBoxResult.No Then
            '            bolEoPowerOn = False
            '            EO_Power(("Off"))
            '        End If

            '    End If
            'End If
            If tbrInstruments2.Buttons(InstrumentIndex + 1).Enabled <> SaisInstrument(InstrumentIndex) Then
                'tbrInstruments2.Buttons(InstrumentIndex + 1).Enabled = SaisInstrument(InstrumentIndex)
            End If
        Next InstrumentIndex

        'check for any open COTS panels
        CheckCOTSPanels()

        'Timer1.Enabled = True

	End Sub

    Private Sub frmToolBar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SAISMGR.Main()
    End Sub

    Sub CheckCOTSPanels()
        Dim digitalProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(DTS)))
        Dim dmmProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(DMM)))
        Dim arbProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(ARB)))
        Dim oscopeProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(DSCOPE)))
        Dim uctProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(UCT)))
        Dim fgProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(FG)))
        Dim switchProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(SW)))
        Dim uutpsProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(UUTPS)))
        Dim copilotProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(MIL1553)))
        Dim busioProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(BUSIO)))
        Dim srProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(SR)))
        Dim rfsProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(RFS)))
        Dim rfcProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(RFC)))
        Dim rfmProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(RFM)))
        Dim rfpProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(RFP)))
        Dim eovProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(EOV)))
        Dim irwinProcesses() As Process = Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(InstrumentFile(IRWIN)))

        If (eovProcesses.Length > 0 Or irwinProcesses.Length > 0) Then
            tbrInstruments.Buttons(EOV + 1).Enabled = False
        Else
            tbrInstruments.Buttons(EOV + 1).Enabled = True
        End If

        If (irwinProcesses.Length > 0 Or eovProcesses.Length > 0) Then
            powerOffPrompted = False
            tbrInstruments.Buttons(IRWIN + 1).Enabled = False
        Else
            If bolEoPowerOn Then
                Me.Timer1.Enabled = False
                CheckEoPower()
                If (powerOffPrompted = False And bolEoPowerOn = True) Then
                    powerOffPrompted = True
                    If MsgBox("Do you want to leave power applied to the EO module?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.SystemModal) = MsgBoxResult.No Then
                        EO_Power(("Off"))
                    End If
                End If
                Me.Timer1.Enabled = True
            End If
            tbrInstruments.Buttons(IRWIN + 1).Enabled = True
        End If

        If (rfsProcesses.Length > 0) Then
            tbrInstruments.Buttons(RFS + 1).Enabled = False
        Else
            tbrInstruments.Buttons(RFS + 1).Enabled = True
        End If

        If (rfcProcesses.Length > 0) Then
            tbrInstruments.Buttons(RFC + 1).Enabled = False
        Else
            tbrInstruments.Buttons(RFC + 1).Enabled = True
        End If

        If (rfmProcesses.Length > 0) Then
            tbrInstruments.Buttons(RFM + 1).Enabled = False
        Else
            tbrInstruments.Buttons(RFM + 1).Enabled = True
        End If

        If (rfpProcesses.Length > 0) Then
            tbrInstruments.Buttons(RFP + 1).Enabled = False
        Else
            tbrInstruments.Buttons(RFP + 1).Enabled = True
        End If

        If (digitalProcesses.Length > 0) Then
            tbrInstruments.Buttons(1).Enabled = False
        Else
            tbrInstruments.Buttons(1).Enabled = True
        End If

        If (dmmProcesses.Length > 0) Then
            tbrInstruments.Buttons(2).Enabled = False
        Else
            tbrInstruments.Buttons(2).Enabled = True
        End If

        If (arbProcesses.Length > 0) Then
            tbrInstruments.Buttons(3).Enabled = False
        Else
            tbrInstruments.Buttons(3).Enabled = True
        End If

        If (oscopeProcesses.Length > 0) Then
            tbrInstruments.Buttons(4).Enabled = False
        Else
            tbrInstruments.Buttons(4).Enabled = True
        End If

        If (uctProcesses.Length > 0) Then
            tbrInstruments.Buttons(5).Enabled = False
        Else
            tbrInstruments.Buttons(5).Enabled = True
        End If

        If (fgProcesses.Length > 0) Then
            tbrInstruments.Buttons(6).Enabled = False
        Else
            tbrInstruments.Buttons(6).Enabled = True
        End If

        If (switchProcesses.Length > 0) Then
            tbrInstruments.Buttons(11).Enabled = False
        Else
            tbrInstruments.Buttons(11).Enabled = True
        End If

        If (uutpsProcesses.Length > 0) Then
            tbrInstruments.Buttons(12).Enabled = False
        Else
            tbrInstruments.Buttons(12).Enabled = True
        End If

        If (copilotProcesses.Length > 0) Then
            tbrInstruments.Buttons(13).Enabled = False
        Else
            tbrInstruments.Buttons(13).Enabled = True
        End If

        If (busioProcesses.Length > 0) Then
            tbrInstruments.Buttons(14).Enabled = False
        Else
            tbrInstruments.Buttons(14).Enabled = True
        End If

        If (srProcesses.Length > 0) Then
            tbrInstruments.Buttons(15).Enabled = False
        Else
            tbrInstruments.Buttons(15).Enabled = True
        End If

    End Sub

End Class