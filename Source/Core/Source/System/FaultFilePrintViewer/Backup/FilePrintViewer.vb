Option Strict Off
Option Explicit On
Imports VB = Microsoft.VisualBasic
Friend Class frmSearch
	Inherits System.Windows.Forms.Form
	'-----------------------------------------------------------
	' File Print Viewer
	'-----------------------------------------------------------
	' FilePrintViewer.frm
	'-----------------------------------------------------------
	' Displays the Main form
	'   This is the main interface and workhorse for the File Printer Viewer app.
	'
	' Known Problems:
	'
	' Version Info:
	'   Ver     Date        By  Comment
	'   1.0.0   03/08/03    RAC Baseline
	'   1.0.1   18-MAY-2005 AR  Bug fixes
	'   1.0.1   26-MAY-2005 AR  Recompile
	'-----------------------------------------------------------
	
	
	Private mouseButton As Short
	
	Private frameSelected As String
	
	Private mbMoving As Boolean ' Used to indicate a mouse move
	Private splitVTarget As Short ' Where the splitter wants to be vertically, if the window is large enough
	Private splitHTarget As Short ' Where the splitter wants to be horizontally, if the window is large enough
	
	Private DelayTime As Boolean
	Private searchSelected As Boolean
	Private FirstTimeThrough As Boolean
	Private lvDetailItemClicked As Boolean
	Private lvSearchItemClicked As Boolean
	Private refreshID As String
	Private DoneOnce As Boolean
	
	Private tvNode As System.Windows.Forms.TreeNode
	Private savedNode As System.Windows.Forms.TreeNode
	
	Const sglSplitLimit As Short = 1500 ' Used to limit the Vertical Bar position
	Const sglBSplitLimit As Short = 3300 ' Used to limit the Horizontal Bar position
	
	Const CTRL_A As Short = 1
	Const RETURN_KEY As Short = 13
	Const SPACE_BAR As Short = 32
	
	Const LISTVIEW_MODE0 As String = "View Large Icons"
	Const LISTVIEW_MODE1 As String = "View Small Icons"
	Const LISTVIEW_MODE2 As String = "View List"
	Const TOOLSEARCH As String = "Search"
	Const TOOLDELETE As String = "Delete"
	
	Const DIRNAME As String = "a:\Fault File Info"
	
	Private Sub cmbNewSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbNewSearch.Click
		
		Me.tiDelay.Interval = 10
		DelayTime = True
		Me.tiDelay.Enabled = True
		
		Do While DelayTime = True
			System.Windows.Forms.Application.DoEvents()
		Loop 
		
		Me.tiDelay.Enabled = False
		'UPGRADE_WARNING: Timer property tiDelay.Interval cannot have a value of 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"'
		Me.tiDelay.Interval = 0
		
		APSNameBeenSelected = False
		TPSNameBeenSelected = False
		SerialNumberBeenSelected = False
		ERONumberBeenSelected = False
		FromRunDateBeenSelected = False
		ToRunDateBeenSelected = False
		
		lvSearchList.Items.Clear()
		
		'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		sbStatusBar.Items.Item(1).Text = "Number Of Test Programs Found  " & lvSearchList.Items.Count
		
		buildListEntries()
		
	End Sub
	
	Private Sub frmSearch_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		If KeyAscii = CTRL_A Then
			mnuEditSelectAll_Click(mnuEditSelectAll, New System.EventArgs())
		End If
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub frmSearch_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
		Dim tempstring As String
		
		DoneOnce = False
		FirstTimeThrough = True
		
		Me.Top = VB6.TwipsToPixelsY(CSng(GetSetting(My.Application.Info.Title, "Settings", "SearchTop", CStr(1000))))
		Me.Left = VB6.TwipsToPixelsX(CSng(GetSetting(My.Application.Info.Title, "Settings", "SearchLeft", CStr(1000))))
		Me.Width = VB6.TwipsToPixelsX(CSng(GetSetting(My.Application.Info.Title, "Settings", "SearchWidth", CStr(11130))))
		Me.Height = VB6.TwipsToPixelsY(CSng(GetSetting(My.Application.Info.Title, "Settings", "SearchHeight", CStr(8950))))
		
		tvTrack.Height = VB6.TwipsToPixelsY(CSng(GetSetting(My.Application.Info.Title, "Settings", "TrackHeight", CStr(4200))))
		
		splitVTarget = Val(GetSetting(My.Application.Info.Title, "Settings", "VSplitter", CStr(VB6.PixelsToTwipsX(imgVSplitter.Left))))
		splitHTarget = Val(GetSetting(My.Application.Info.Title, "Settings", "HSplitter", CStr(VB6.PixelsToTwipsY(imgHSplitter.Top))))
		
		lvDetail.View = CShort(GetSetting(My.Application.Info.Title, "Setting", "ViewMode", CStr(System.Windows.Forms.View.LargeIcon)))
		
		fraStatusInfo.Visible = False
		picStatusInfo.Visible = False
		lvDetail.Visible = True
		fraSearch.Visible = False
		picSearch.Visible = False
		lvSearchList.Visible = False
		
		KeyPreview = True
		
		savedID = "HEAD"
		refreshID = "HEAD"
		savedNode = tvTrack.Nodes.Add("")
		savedNode.Text = "APS Listing"
		savedNode.Name = "HEAD"
		savedNode.Tag = "HEAD"
		savedNode.ImageKey = "closed"
		searchSelected = False
		
		APSNameBeenSelected = False
		TPSNameBeenSelected = False
		SerialNumberBeenSelected = False
		ERONumberBeenSelected = False
		
		LoadTreeView()
		
		AdjustTree()
		LoadListView()
		
		'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		sbStatusBar.Items.Item(1).Text = "Number Of Application Program Names  " & lvDetail.Items.Count
		
		frameSelected = "Track"
		
		buildListEntries()
		
		lblTitle(0).Text = "DataBase Contents:"
		
		mnuListViewMode(0).Checked = True
		mnuListViewMode(1).Checked = False
		mnuListViewMode(2).Checked = False
		
		
		If GPNamMode = True Then
			getLastID()
			tempstring = IDToDelete
			expandTreeList()
			mnuEditSelectAll.Enabled = False
			mnuFileDelete.Enabled = False
			mnuFileSearch.Enabled = False
			mnuPopDelete.Enabled = False
			mnuPopSearch.Enabled = False
			tbToolBar.Items.Item(TOOLSEARCH).Enabled = False
			tbToolBar.Items.Item(TOOLDELETE).Enabled = False
		End If
		
	End Sub
	
	Private Sub loadSearchForm()
		
		On Error Resume Next
		
		
		splitHTarget = VB6.PixelsToTwipsY(tvTrack.Top) + VB6.PixelsToTwipsY(tvTrack.Height)
		picSearch.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(tvTrack.Top) + (VB6.PixelsToTwipsY(tvTrack.Height) + 40))
		picSearch.Left = tvTrack.Left
		picSearch.Width = tvTrack.Width
		picSearch.Height = VB6.TwipsToPixelsY(CSng(GetSetting(My.Application.Info.Title, "Settings", "PicSearchHeight", CStr(4800))))
		
		Me.Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(Me.Height) + VB6.PixelsToTwipsY(picSearch.Height) + 40))
		
		fraSearch.Top = 0
		fraSearch.Left = 0
		fraSearch.Width = VB6.ToPixelsUserWidth(VB6.PixelsToTwipsX(tvTrack.Width), 2835, 193)
		fraSearch.Height = VB6.ToPixelsUserHeight(VB6.PixelsToTwipsY(picSearch.Height) - 40, 4995, 337)
		
		imgVSplitter.Top = tvTrack.Top
		imgVSplitter.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(tvTrack.Height) + VB6.PixelsToTwipsY(picSearch.Height) + 40)
		imgHSplitter.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(picSearch.Top) - 40)
		imgHSplitter.Left = picSearch.Left
		imgHSplitter.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(picSearch.Width) + 40 + VB6.PixelsToTwipsX(lvDetail.Width))
		
		picStatusInfo.Top = tvTrack.Top
		picStatusInfo.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(imgVSplitter.Left) + 40)
		picStatusInfo.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 40))
		picStatusInfo.Height = tvTrack.Height
		
		txFaultFileStatus.Top = 0
		txFaultFileStatus.Left = 0
		txFaultFileStatus.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 160))
		txFaultFileStatus.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(picStatusInfo.Height) - 30)
		
		lvSearchList.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(tvTrack.Left) + VB6.PixelsToTwipsX(tvTrack.Width) + 40)
		lvSearchList.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 140))
		lvDetail.Top = tvTrack.Top
		lvSearchList.Top = picSearch.Top
		lvSearchList.Height = picSearch.Height
		
		fraStatusInfo.Top = 0
		fraStatusInfo.Left = 0
		fraStatusInfo.Width = VB6.ToPixelsUserWidth(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 40), 5385.17, 361)
		fraStatusInfo.Height = VB6.ToPixelsUserHeight(VB6.PixelsToTwipsY(picStatusInfo.Height), 4395, 297)
		
		searchSelected = True
		
		lvSearchList.Items.Clear()
		
		'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		sbStatusBar.Items.Item(1).Text = "Number Of Test Programs Found  " & lvSearchList.Items.Count
		
		
	End Sub
	
	Private Sub frmSearch_FormClosed(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
		
		If Me.WindowState = System.Windows.Forms.FormWindowState.Normal Then
			
			If picSearch.Visible = True Then
				SaveSetting(My.Application.Info.Title, "Settings", "SearchHeight", CStr(VB6.PixelsToTwipsY(picSearch.Top)))
			Else
				SaveSetting(My.Application.Info.Title, "Settings", "SearchHeight", CStr(VB6.PixelsToTwipsY(Me.Height)))
			End If
			
			SaveSetting(My.Application.Info.Title, "Settings", "SearchTop", CStr(VB6.PixelsToTwipsY(Me.Top)))
			SaveSetting(My.Application.Info.Title, "Settings", "SearchLeft", CStr(VB6.PixelsToTwipsX(Me.Left)))
			SaveSetting(My.Application.Info.Title, "Settings", "SearchWidth", CStr(VB6.PixelsToTwipsX(Me.Width)))
			SaveSetting(My.Application.Info.Title, "Settings", "TrackHeight", CStr(VB6.PixelsToTwipsY(tvTrack.Height)))
			
		End If
		If DoneOnce = False Then
			If GPNamMode = True Then
				deleteTheRecords(IDToDelete, "", "")
			End If
			
			CompressDatabase("D:\database\FaultFile.mdb")
			
		End If
		
		DoneOnce = True
		
		Me.Close()
		
	End Sub
	
	Private Sub unloadSearchForm()
		
		If Me.WindowState <> System.Windows.Forms.FormWindowState.Maximized Then
			Me.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - (VB6.PixelsToTwipsY(picSearch.Height) + 40))
		ElseIf Me.WindowState = System.Windows.Forms.FormWindowState.Maximized Then 
		End If
		
		searchSelected = False
		
		doNodeClick(savedNode)
		
	End Sub
	
	Private Sub imgHsplitter_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgHsplitter.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		picHSplitter.Visible = False
		splitHTarget = VB6.PixelsToTwipsY(picHSplitter.Top)
		SaveSetting(My.Application.Info.Title, "Settings", "HSplitter", CStr(splitHTarget))
		SizeControls(splitVTarget, splitHTarget)
		mbMoving = False
		lvDetail.Items.Clear()
		LoadListView()
		
	End Sub
	
	Private Sub imgHSplitter_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgHSplitter.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		With imgHSplitter
			picHSplitter.SetBounds(.Left, .Top, VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) - 20), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(.Height) \ 2))
		End With
		
		picHSplitter.Visible = True
		mbMoving = True
		
	End Sub
	
	Private Sub imgHSplitter_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgHSplitter.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Dim sglPos As Single
		
		If mbMoving Then
			sglPos = Y + VB6.PixelsToTwipsY(imgHSplitter.Top)
			
			If sglPos < sglBSplitLimit Then
				picHSplitter.Top = VB6.TwipsToPixelsY(sglBSplitLimit)
			ElseIf sglPos > VB6.PixelsToTwipsY(Me.Height) - sglBSplitLimit Then 
				picHSplitter.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.Height) - sglBSplitLimit)
			Else
				picHSplitter.Top = VB6.TwipsToPixelsY(sglPos)
			End If
		End If
		
	End Sub
	
	Private Sub imgVSplitter_MouseUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgVSplitter.MouseUp
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		picVSplitter.Visible = False
		splitVTarget = VB6.PixelsToTwipsX(picVSplitter.Left)
		SaveSetting(My.Application.Info.Title, "Settings", "VSplitter", CStr(splitVTarget))
		SizeControls(splitVTarget, splitHTarget)
		mbMoving = False
		lvDetail.Items.Clear()
		LoadListView()
		
	End Sub
	
	Private Sub imgVSplitter_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgVSplitter.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		With imgVSplitter
			picVSplitter.SetBounds(.Left, .Top, VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(.Width) \ 2), VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(.Height) - 20))
		End With
		
		picVSplitter.Visible = True
		mbMoving = True
		
	End Sub
	
	Private Sub imgVSplitter_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles imgVSplitter.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		Dim sglPos As Single
		
		If mbMoving Then
			sglPos = X + VB6.PixelsToTwipsX(imgVSplitter.Left)
			
			If sglPos < sglSplitLimit Then
				picVSplitter.Left = VB6.TwipsToPixelsX(sglSplitLimit)
			ElseIf sglPos > VB6.PixelsToTwipsX(Me.Width) - sglSplitLimit Then 
				picVSplitter.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - sglSplitLimit)
			Else
				picVSplitter.Left = VB6.TwipsToPixelsX(sglPos)
			End If
		End If
		
	End Sub
	
	Private Sub lvDetail_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lvDetail.DoubleClick
		Dim tNode As System.Windows.Forms.TreeNode
		Dim i, J As Short
		
		If mouseButton = 1 Then
			If Len(lastID) > 0 Then
				For i = tvTrack.Nodes.Count To 1 Step -1
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If tvTrack.Nodes.Item(i).Tag = lastID Then
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						doNodeClick(tvTrack.Nodes.Item(i))
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						savedNode = tvTrack.Nodes.Item(i)
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
						tvTrack.Nodes.Item(i).Selected = True
						On Error GoTo OuttaHere
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tNode = tvTrack.Nodes.Item(i).Parent
						
						Do 
							'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							tvTrack.Nodes.Item(i).Expand()
							tNode = tNode.Parent
						Loop Until tNode.Index <= 1
						
						'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						tNode = Nothing
						Exit For
					End If
				Next i
			End If
		End If
		
OuttaHere: 
		
		lvDetailItemClicked = False
		
	End Sub
	
	Private Sub lvDetail_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lvDetail.Enter
		
		lvDetailItemClicked = False
		
	End Sub
	
	'UPGRADE_ISSUE: MSComctlLib.ListView event lvDetail.ItemClick was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub lvDetail_ItemClick(ByVal Item As System.Windows.Forms.ListViewItem)
		Dim tmpID As String
		Dim i As Short
		
		lvDetailItemClicked = True
		tmpID = lastID
		lastID = CStr(Item.Name)
		frameSelected = "Detail"
		
		If mouseButton = 2 Then
			'UPGRADE_ISSUE: Form method frmSearch.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			PopupMenu(mnuPopUp)
			lastID = tmpID
		End If
		
	End Sub
	
	Private Sub lvDetail_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles lvDetail.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		If KeyAscii = SPACE_BAR Or KeyAscii = RETURN_KEY Then
			KeyAscii = 0
			mouseButton = 1
			
			If lvDetailItemClicked = False Then
				lastID = lvDetail.TopItem.Name
				frameSelected = "Detail"
			End If
			
			lvDetail_DoubleClick(lvDetail, New System.EventArgs())
			mouseButton = 0
		End If
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub lvDetail_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lvDetail.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		savedID = lastID
		mouseButton = Button
		frameSelected = "Detail"
		
	End Sub
	
	Private Sub lvDetail_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lvDetail.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		If Button = 0 Then
			SetToolTiplvDetail(X, Y)
		End If
		
	End Sub
	
	Private Sub lvSearchList_DoubleClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lvSearchList.DoubleClick
		Dim tNode As System.Windows.Forms.TreeNode
		Dim i, J As Short
		
		If mouseButton = 1 Then
			If Len(lastID) > 0 Then
				For i = tvTrack.Nodes.Count To 1 Step -1
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If tvTrack.Nodes.Item(i).Tag = lastID Then
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						doNodeClick(tvTrack.Nodes.Item(i))
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						savedNode = tvTrack.Nodes.Item(i)
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
						tvTrack.Nodes.Item(i).Selected = True
						On Error GoTo OuttaHere
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tNode = tvTrack.Nodes.Item(i).Parent
						
						Do 
							'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
							tvTrack.Nodes.Item(i).Expand()
							tNode = tNode.Parent
						Loop Until tNode.Index <= 1
						
						'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						tNode = Nothing
						Exit For
					End If
				Next i
			End If
		End If
		
OuttaHere: 
		
	End Sub
	
	Private Sub lvSearchList_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles lvSearchList.Enter
		
		lvSearchItemClicked = False
		
	End Sub
	
	'UPGRADE_ISSUE: MSComctlLib.ListView event lvSearchList.ItemClick was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="ABD9AF39-7E24-4AFF-AD8D-3675C1AA3054"'
	Private Sub lvSearchList_ItemClick(ByVal Item As System.Windows.Forms.ListViewItem)
		
		savedID = lastID
		lastID = CStr(Item.Name)
		frameSelected = "Search"
		lvSearchItemClicked = True
		
		If mouseButton = 2 Then
			'UPGRADE_ISSUE: Form method frmSearch.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			PopupMenu(mnuPopUp)
			lastID = savedID
		End If
		
	End Sub
	
	Private Sub lvSearchList_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles lvSearchList.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		If KeyAscii = SPACE_BAR Or KeyAscii = RETURN_KEY Then
			KeyAscii = 0
			mouseButton = 1
			
			If lvSearchItemClicked = False Then
				lastID = lvSearchList.TopItem.Name
				frameSelected = "Search"
			End If
			
			lvSearchList_DoubleClick(lvSearchList, New System.EventArgs())
			mouseButton = 0
		End If
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub lvSearchList_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lvSearchList.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		savedID = lastID
		mouseButton = Button
		frameSelected = "Search"
		
	End Sub
	
	Private Sub lvSearchList_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles lvSearchList.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		If Button = 0 Then
			SetToolTiplvDetail(X, Y)
		End If
		
	End Sub
	
	Public Sub mnuFileDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileDelete.Click
		
		doRecordDelete()
		
	End Sub
	
	Public Sub mnuFileFont_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileFont.Click
		
		On Error Resume Next
		
		'UPGRADE_ISSUE: MSComDlg.CommonDialog property CommonDialog1.Flags was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
		CommonDialog1.Flags = 0
		'UPGRADE_WARNING: CommonDialog variable was not upgraded Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="671167DC-EA81-475D-B690-7A40C7BF4A23"'
		With CommonDialog1
			.Font = VB6.FontChangeName(.Font, GetSetting(My.Application.Info.Title, "Settings", "PrinterFontN", "Arial"))
			.Font = VB6.FontChangeSize(.Font, CDec(GetSetting(My.Application.Info.Title, "Settings", "PrinterFontS", CStr(12))))
			.Font = VB6.FontChangeBold(.Font, CBool(GetSetting(My.Application.Info.Title, "Settings", "PrinterFontB", CStr(False))))
			.Font = VB6.FontChangeItalic(.Font, CBool(GetSetting(My.Application.Info.Title, "Settings", "PrinterFontI", CStr(False))))
			.Font = VB6.FontChangeUnderline(.Font, CBool(GetSetting(My.Application.Info.Title, "Settings", "PrinterFontU", CStr(False))))
			.Font = VB6.FontChangeStrikeout(.Font, CBool(GetSetting(My.Application.Info.Title, "Settings", "PrinterFontST", CStr(False))))
			.Color = System.Drawing.ColorTranslator.FromOle(CInt(GetSetting(My.Application.Info.Title, "Settings", "PrinterForeC", CStr(0))))
			'UPGRADE_ISSUE: MSComDlg.CommonDialog property CommonDialog1.Flags was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			.Flags = &H100s Or &H4s Or &H3s
			.ShowDialog()
			
			'UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
			If (Err.Number = DialogResult.Cancel) Then Exit Sub
			
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterFontN", .Font.Name)
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterFontI", CStr(.Font.Italic))
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterFontB", CStr(.Font.Bold))
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterFontST", CStr(.Font.Strikeout))
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterFontU", CStr(.Font.Underline))
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterForeC", System.Drawing.ColorTranslator.ToOle(.Color).ToString)
			SaveSetting(My.Application.Info.Title, "Settings", "PrinterFontS", CStr(.Font.Size))
		End With
		
	End Sub
	
	Public Sub mnuFilePrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFilePrint.Click
		
		doPrintStatus()
		
		If GPNamMode = True Then
			mnuFileExit_Click(mnuFileExit, New System.EventArgs())
		End If
		
	End Sub
	
	Public Sub mnuFileSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSearch.Click
		
		If searchSelected = False Then
			picSearch.Visible = True
			lvSearchList.Visible = True
			fraSearch.Visible = True
			
			mnuFileSearch.Checked = True
			
			loadSearchForm()
			
		Else
			picSearch.Visible = False
			lvSearchList.Visible = False
			
			unloadSearchForm()
			APSNameBeenSelected = False
			TPSNameBeenSelected = False
			SerialNumberBeenSelected = False
			ERONumberBeenSelected = False
			FromRunDateBeenSelected = False
			ToRunDateBeenSelected = False
			
			lvSearchList.Items.Clear()
			
			buildListEntries()
			
			mnuFileSearch.Checked = False
			
		End If
		
	End Sub
	
	Public Sub mnuFileSendToFloppy_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileSendToFloppy.Click
		
		doFileStatusToMedia()
		
		If GPNamMode = True Then
			mnuFileExit_Click(mnuFileExit, New System.EventArgs())
		End If
		
	End Sub
	
	Public Sub mnuHelpPrintViewer_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelpPrintViewer.Click
		Dim retval As Double
		retval = Shell("C:\Program Files\Plus!\Microsoft Internet\IEXPLORE.EXE " & "C:\Program Files\FilePrintViewer\help\FilePrintViewerHelp.html", AppWinStyle.MaximizedFocus)
		
	End Sub
	
	Public Sub mnuListViewMode_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuListViewMode.Click
		Dim Index As Short = mnuListViewMode.GetIndex(eventSender)
		
		Select Case Index
			Case 0
				lvDetail.View = System.Windows.Forms.View.LargeIcon
				lvSearchList.View = System.Windows.Forms.View.LargeIcon
				mnuListViewMode(0).Checked = True
				mnuListViewMode(1).Checked = False
				mnuListViewMode(2).Checked = False
			Case 1
				lvDetail.View = System.Windows.Forms.View.SmallIcon
				lvSearchList.View = System.Windows.Forms.View.SmallIcon
				mnuListViewMode(1).Checked = True
				mnuListViewMode(0).Checked = False
				mnuListViewMode(2).Checked = False
			Case 2
				lvDetail.View = System.Windows.Forms.View.List
				lvSearchList.View = System.Windows.Forms.View.List
				mnuListViewMode(2).Checked = True
				mnuListViewMode(1).Checked = False
				mnuListViewMode(0).Checked = False
		End Select
		
		SaveSetting(My.Application.Info.Title, "Settings", "ViewMode", CStr(lvDetail.View))
		
	End Sub
	
	Public Sub mnuPopDelete_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPopDelete.Click
		
		mnuFileDelete_Click(mnuFileDelete, New System.EventArgs())
		
	End Sub
	
	Public Sub mnuPopMedia_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPopMedia.Click
		
		mnuFileSendToFloppy_Click(mnuFileSendToFloppy, New System.EventArgs())
		
	End Sub
	
	Public Sub mnuPopPrint_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPopPrint.Click
		
		mnuFilePrint_Click(mnuFilePrint, New System.EventArgs())
		
	End Sub
	
	Public Sub mnuPopSearch_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuPopSearch.Click
		Dim tempID As String
		
		tempID = lastID
		lastID = savedID
		
		If searchSelected = False Then
			mnuFileSearch_Click(mnuFileSearch, New System.EventArgs())
		ElseIf frameSelected <> "Search" Then 
			APSNameBeenSelected = False
			TPSNameBeenSelected = False
			SerialNumberBeenSelected = False
			ERONumberBeenSelected = False
			FromRunDateBeenSelected = False
			ToRunDateBeenSelected = False
			
			lvSearchList.Items.Clear()
			
			buildListEntries()
			
		End If
		
		lastID = tempID
		seedSearchList()
		
	End Sub
	
	Private Sub nmuSelectAll_Click()
		
		If lvDetail.Visible = True Then
			lvDetail.Focus()
		End If
		
	End Sub
	
	Public Sub mnuEditSelectAll_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuEditSelectAll.Click
		Dim i As Short
		Dim dog As String
		
		If frameSelected = "Search" Then
			
			For i = lvSearchList.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvSearchList.Items.Item(i).Selected = False Then
					'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					lvSearchList.Items.Item(i).Selected = True
				End If
			Next i
			
			lvSearchList.Focus()
			
		ElseIf lvDetail.Visible = True Then 
			
			For i = lvDetail.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvDetail.Items.Item(i).Selected = False Then
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					lvDetail.Items.Item(i).Selected = True
				End If
			Next i
			
			lvDetail.Focus()
			frameSelected = "Detail"
			
		End If
		
	End Sub
	
	Private Sub tbToolBar_ButtonClick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles _tbToolBar_Button1.Click, _tbToolBar_Button2.Click, _tbToolBar_Button3.Click, _tbToolBar_Button4.Click, _tbToolBar_Button5.Click, _tbToolBar_Button6.Click, _tbToolBar_Button7.Click, _tbToolBar_Button8.Click, _tbToolBar_Button9.Click, _tbToolBar_Button10.Click, _tbToolBar_Button11.Click, _tbToolBar_Button12.Click, _tbToolBar_Button13.Click
		Dim Button As System.Windows.Forms.ToolStripItem = CType(eventSender, System.Windows.Forms.ToolStripItem)
		
		On Error Resume Next
		
		Select Case Button.Name
			Case "View Large Icons"
				lvDetail.View = System.Windows.Forms.View.LargeIcon
				lvSearchList.View = System.Windows.Forms.View.LargeIcon
				mnuListViewMode(0).Checked = True
				mnuListViewMode(1).Checked = False
				mnuListViewMode(2).Checked = False
			Case "View Small Icons"
				lvDetail.View = System.Windows.Forms.View.SmallIcon
				lvSearchList.View = System.Windows.Forms.View.SmallIcon
				mnuListViewMode(1).Checked = True
				mnuListViewMode(0).Checked = False
				mnuListViewMode(2).Checked = False
			Case "View List"
				lvDetail.View = System.Windows.Forms.View.List
				lvSearchList.View = System.Windows.Forms.View.List
				mnuListViewMode(2).Checked = True
				mnuListViewMode(1).Checked = False
				mnuListViewMode(0).Checked = False
			Case "View Details"
				lvDetail.View = System.Windows.Forms.View.Details
			Case "Refresh"
				mnuViewRefresh_Click(mnuViewRefresh, New System.EventArgs())
			Case "Print"
				mnuFilePrint_Click(mnuFilePrint, New System.EventArgs())
			Case "Print_To"
				mnuFileSendToFloppy_Click(mnuFileSendToFloppy, New System.EventArgs())
			Case "Delete"
				mnuFileDelete_Click(mnuFileDelete, New System.EventArgs())
			Case "Search"
				mnuFileSearch_Click(mnuFileSearch, New System.EventArgs())
			Case "Font"
				mnuFileFont_Click(mnuFileFont, New System.EventArgs())
		End Select
		
		If GPNamMode = False Then
			SaveSetting(My.Application.Info.Title, "Settings", "ViewMode", CStr(lvDetail.View))
		End If
		
	End Sub
	
	Public Sub mnuViewToolbar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuViewToolbar.Click
		
		mnuViewToolBar.Checked = Not mnuViewToolBar.Checked
		tbToolBar.Visible = mnuViewToolBar.Checked
		SizeControls(splitVTarget, splitHTarget)
		
	End Sub
	
	Public Sub mnuViewRefresh_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuViewRefresh.Click
		Dim tmpID As String
		
		tmpID = lastID
		AdjustTree()
		lvDetail.Items.Clear()
		lastID = refreshID
		LoadListView()
		lastID = tmpID
		
	End Sub
	
	Public Sub mnuViewStatusBar_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuViewStatusBar.Click
		
		mnuViewStatusBar.Checked = Not mnuViewStatusBar.Checked
		sbStatusBar.Visible = mnuViewStatusBar.Checked
		SizeControls(splitVTarget, splitHTarget)
		
	End Sub
	
	Public Sub mnuFileExit_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuFileExit.Click
		
		frmSearch_FormClosed(Me, New System.Windows.Forms.FormClosedEventArgs(0, FormAction.Closed))
		
	End Sub
	
	Public Sub mnuHelpAbout_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles mnuHelpAbout.Click
		
		VB6.ShowForm(frmAbout, VB6.FormShowConstants.Modal, Me)
		
	End Sub
	
	Private Sub tiDelay_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles tiDelay.Tick
		DelayTime = False
		tiDelay.Enabled = False
	End Sub
	
	Private Sub tvTrack_AfterCollapse(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.TreeViewEventArgs) Handles tvTrack.AfterCollapse
		Dim Node As System.Windows.Forms.TreeNode = eventArgs.Node
		
		Node.ImageKey = "closed"
		
	End Sub
	
	Private Sub tvTrack_AfterExpand(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.TreeViewEventArgs) Handles tvTrack.AfterExpand
		Dim Node As System.Windows.Forms.TreeNode = eventArgs.Node
		
		If FirstTimeThrough = True Then
			FirstTimeThrough = False
			GoTo outofhere
		End If
		
		If GPNamMode = False Then
			
			tvNode = Node
			savedID = lastID
			doNodeClick(Node)
			savedNode = Node
			
		End If
		
		Node.ImageKey = "open"
		
outofhere: 
		
	End Sub
	
	Private Sub tvTrack_KeyPress(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyPressEventArgs) Handles tvTrack.KeyPress
		Dim KeyAscii As Short = Asc(eventArgs.KeyChar)
		
		If GPNamMode = False Then
			
			If KeyAscii = SPACE_BAR Or KeyAscii = RETURN_KEY Then
				KeyAscii = 0
				mouseButton = 1
				tvTrack_NodeClick(tvTrack, New System.Windows.Forms.TreeNodeMouseClickEventArgs(tvNode, System.Windows.Forms.MouseButtons.None, 0, 0, 0))
				mouseButton = 0
			End If
		End If
		
		eventArgs.KeyChar = Chr(KeyAscii)
		If KeyAscii = 0 Then
			eventArgs.Handled = True
		End If
	End Sub
	
	Private Sub tvTrack_MouseDown(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles tvTrack.MouseDown
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		If GPNamMode = False Then
			
			mouseButton = Button
			frameSelected = "Track"
		End If
		
	End Sub
	
	Private Sub tvTrack_MouseMove(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.MouseEventArgs) Handles tvTrack.MouseMove
		Dim Button As Short = eventArgs.Button \ &H100000
		Dim Shift As Short = System.Windows.Forms.Control.ModifierKeys \ &H10000
		Dim X As Single = VB6.PixelsToTwipsX(eventArgs.X)
		Dim Y As Single = VB6.PixelsToTwipsY(eventArgs.Y)
		
		If Button = 0 Then
			SetToolTiptvTrack(X, Y)
		End If
		
	End Sub
	
	Private Sub tvTrack_NodeClick(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvTrack.NodeMouseClick
		Dim Node As System.Windows.Forms.TreeNode = eventArgs.Node
		Dim xNode As System.Windows.Forms.TreeNode
		
		If GPNamMode = False Then
			
			xNode = Node
			tvNode = Node
			
			savedID = lastID
			
			If mouseButton = 1 Then
				doNodeClick(Node)
				savedNode = Node
			ElseIf mouseButton = 2 Then 
				frameSelected = "Track"
				'UPGRADE_ISSUE: Form method frmSearch.PopupMenu was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				PopupMenu(mnuPopUp)
				lastID = savedID
			End If
		End If
		
	End Sub
	
	Private Sub cmbAPSName_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbAPSName.Enter
		
		selectedAPSName = cmbAPSName.Text
		
	End Sub
	
	Private Sub cmbAPSName_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbAPSName.Leave
		
		If (selectedAPSName <> cmbAPSName.Text) Then
			APSNameBeenSelected = True
			selectedAPSName = cmbAPSName.Text
			cmbAPSName.Items.Clear()
			VB6.SetItemString(cmbAPSName, 0, selectedAPSName)
			cmbAPSName.Text = VB6.GetItemString(cmbAPSName, 0)
			redoComboBoxes()
		End If
		
	End Sub
	
	Private Sub cmbERONumber_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbERONumber.Enter
		
		selectedERONumber = cmbERONumber.Text
		
	End Sub
	
	Private Sub cmbERONumber_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbERONumber.Leave
		
		If (selectedERONumber <> cmbERONumber.Text) Then
			ERONumberBeenSelected = True
			selectedERONumber = cmbERONumber.Text
			cmbERONumber.Items.Clear()
			VB6.SetItemString(cmbERONumber, 0, selectedERONumber)
			cmbERONumber.Text = VB6.GetItemString(cmbERONumber, 0)
			redoComboBoxes()
		End If
		
	End Sub
	
	Private Sub cmbTPSName_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbTPSName.Enter
		
		selectedTPSName = cmbTPSName.Text
		
	End Sub
	
	Private Sub cmbTPSName_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbTPSName.Leave
		
		If (selectedTPSName <> cmbTPSName.Text) Then
			TPSNameBeenSelected = True
			selectedTPSName = cmbTPSName.Text
			cmbTPSName.Items.Clear()
			VB6.SetItemString(cmbTPSName, 0, selectedTPSName)
			cmbTPSName.Text = VB6.GetItemString(cmbTPSName, 0)
			redoComboBoxes()
		End If
		
	End Sub
	
	Private Sub cmbSerialNumber_Enter(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbSerialNumber.Enter
		
		selectedSerialNumber = cmbSerialNumber.Text
		
	End Sub
	
	Private Sub cmbSerialNumber_Leave(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbSerialNumber.Leave
		
		If (selectedSerialNumber <> cmbSerialNumber.Text) Then
			SerialNumberBeenSelected = True
			selectedSerialNumber = cmbSerialNumber.Text
			cmbSerialNumber.Items.Clear()
			VB6.SetItemString(cmbSerialNumber, 0, selectedSerialNumber)
			cmbSerialNumber.Text = VB6.GetItemString(cmbSerialNumber, 0)
			redoComboBoxes()
		End If
		
	End Sub
	
	Private Sub cmbSearchNow_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles cmbSearchNow.Click
		Dim searchString As String
		Dim searchString1 As String
		Dim rs As ADODB.Recordset
		Dim lvListItem As System.Windows.Forms.ListViewItem
		
		OpenDB("FaultFile")
		
		searchString1 = SearchForString
		
		lvSearchList.Items.Clear()
		
		If searchString1 = "" Then
			searchString = "SELECT ID, RunDate From Faults WHERE" & " ((RunDate>=#" & dtpFrom.Value & "#" & " AND RunDate<=#" & dtpTo.Value & "#))"
			
		ElseIf DateSelected = False Then 
			searchString = "SELECT ID, RunDate From Faults " & searchString1 & " AND ((RunDate>=#" & dtpFrom.Value & "#" & " AND RunDate<=#" & dtpTo.Value & "#))"
			
		ElseIf searchString = "" Then 
			searchString = "SELECT ID, RunDate From Faults " & searchString1
			
		End If
		
		rs = rsOpen(searchString, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			
			Do While Not rs.EOF
				'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems.ImageList has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				lvListItem = lvSearchList.Items.Add("R_G" & rs.Fields("ID").Value, GetDBString(rs, "RunDate"), "file")
				If Not rs.EOF Then
					rs.MoveNext()
				End If
			Loop 
			
		End If
		
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		
		CloseDB("FaultFile")
		
		frameSelected = "Search"
		
		'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
		sbStatusBar.Items.Item(1).Text = "Number Of Test Programs Found  " & lvSearchList.Items.Count
		
	End Sub
	
	Sub SizeControls(ByVal X As Short, ByVal Y As Short)
		
		On Error Resume Next
		
		If X < 2900 Then X = 2900
		If X > (VB6.PixelsToTwipsX(Me.Width) - 2900) Then X = VB6.PixelsToTwipsX(Me.Width) - 2900
		
		tvTrack.Width = VB6.TwipsToPixelsX(X)
		imgVSplitter.Left = VB6.TwipsToPixelsX(X)
		lvDetail.Left = VB6.TwipsToPixelsX(X + 40)
		lvDetail.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 140))
		lblTitle(0).Width = VB6.ToPixelsUserWidth(VB6.PixelsToTwipsX(tvTrack.Width), 11880, 792)
		lblTitle(1).Left = VB6.ToPixelsUserX(VB6.PixelsToTwipsX(lvDetail.Left) + 20, 0, 11880, 792)
		lblTitle(1).Width = VB6.ToPixelsUserWidth(VB6.PixelsToTwipsX(lvDetail.Width) - 40, 11880, 792)
		
		If picSearch.Visible = True Then
			If Y < 4000 Then Y = 4000
			If Y > (VB6.PixelsToTwipsY(Me.Height) - 4000) Then Y = VB6.PixelsToTwipsY(Me.Height) - 4000
		End If
		
		If tbToolBar.Visible Then
			tvTrack.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(tbToolBar.Height) + VB6.PixelsToTwipsY(picTitles.Height))
		Else
			tvTrack.Top = picTitles.Height
		End If
		
		If picSearch.Visible = True Then
			
			picSearch.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(tvTrack.Top) + (VB6.PixelsToTwipsY(tvTrack.Height) + 40))
			picSearch.Left = tvTrack.Left
			picSearch.Width = tvTrack.Width
			
			If Y < VB6.PixelsToTwipsY(picSearch.Top) Then
				picSearch.Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(picSearch.Height) + (VB6.PixelsToTwipsY(picSearch.Top) - Y)))
			Else
				picSearch.Height = VB6.TwipsToPixelsY((VB6.PixelsToTwipsY(picSearch.Height) - (Y - VB6.PixelsToTwipsY(picSearch.Top))))
			End If
			
			If VB6.PixelsToTwipsY(picSearch.Height) < 4000 Then picSearch.Height = VB6.TwipsToPixelsY(4000)
			
			If sbStatusBar.Visible Then
				tvTrack.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - (VB6.PixelsToTwipsY(picTitles.Top) + VB6.PixelsToTwipsY(picTitles.Height) + VB6.PixelsToTwipsY(sbStatusBar.Height) + VB6.PixelsToTwipsY(picSearch.Height)))
			Else
				tvTrack.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - (VB6.PixelsToTwipsY(picTitles.Top) + VB6.PixelsToTwipsY(picTitles.Height) + VB6.PixelsToTwipsY(picSearch.Height)))
			End If
		Else
			If sbStatusBar.Visible Then
				tvTrack.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - (VB6.PixelsToTwipsY(picTitles.Top) + VB6.PixelsToTwipsY(picTitles.Height) + VB6.PixelsToTwipsY(sbStatusBar.Height)))
			Else
				tvTrack.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(Me.ClientRectangle.Height) - (VB6.PixelsToTwipsY(picTitles.Top) + VB6.PixelsToTwipsY(picTitles.Height)))
			End If
		End If
		
		picSearch.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(tvTrack.Top) + (VB6.PixelsToTwipsY(tvTrack.Height) + 40))
		picSearch.Left = tvTrack.Left
		picSearch.Width = tvTrack.Width
		
		fraSearch.Top = 0
		fraSearch.Left = 0
		fraSearch.Width = VB6.ToPixelsUserWidth(VB6.PixelsToTwipsX(tvTrack.Width), 2835, 193)
		fraSearch.Height = VB6.ToPixelsUserHeight(VB6.PixelsToTwipsY(picSearch.Height) - 40, 4995, 337)
		
		lvDetail.Height = tvTrack.Height
		
		imgVSplitter.Top = tvTrack.Top
		imgVSplitter.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(tvTrack.Height) + VB6.PixelsToTwipsY(picSearch.Height) + 40)
		imgHSplitter.Top = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(picSearch.Top) - 40)
		imgHSplitter.Left = picSearch.Left
		imgHSplitter.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(picSearch.Width) + 40 + VB6.PixelsToTwipsX(lvDetail.Width))
		
		picStatusInfo.Top = tvTrack.Top
		picStatusInfo.Left = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(imgVSplitter.Left) + 40)
		picStatusInfo.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 40))
		picStatusInfo.Height = tvTrack.Height
		
		txFaultFileStatus.Top = 0
		txFaultFileStatus.Left = 0
		txFaultFileStatus.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 160))
		txFaultFileStatus.Height = VB6.TwipsToPixelsY(VB6.PixelsToTwipsY(picStatusInfo.Height) - 30)
		
		lvSearchList.Left = VB6.TwipsToPixelsX(X + 40)
		lvSearchList.Width = VB6.TwipsToPixelsX(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 140))
		lvDetail.Top = tvTrack.Top
		lvSearchList.Top = picSearch.Top
		lvSearchList.Height = picSearch.Height
		
		fraStatusInfo.Top = 0
		fraStatusInfo.Left = 0
		fraStatusInfo.Width = VB6.ToPixelsUserWidth(VB6.PixelsToTwipsX(Me.Width) - (VB6.PixelsToTwipsX(tvTrack.Width) + 40), 5385.17, 361)
		fraStatusInfo.Height = VB6.ToPixelsUserHeight(VB6.PixelsToTwipsY(picStatusInfo.Height), 4395, 297)
		
	End Sub
	
	'UPGRADE_WARNING: Event frmSearch.Resize may fire when form is initialized. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="88B12AE1-6DE0-48A0-86F1-60C0686C026A"'
	Private Sub frmSearch_Resize(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Resize
		
		Me.tiDelay.Interval = 20
		DelayTime = True
		Me.tiDelay.Enabled = True
		
		Do While DelayTime = True
			System.Windows.Forms.Application.DoEvents()
		Loop 
		
		Me.tiDelay.Enabled = False
		'UPGRADE_WARNING: Timer property tiDelay.Interval cannot have a value of 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="169ECF4A-1968-402D-B243-16603CC08604"'
		Me.tiDelay.Interval = 0
		
		On Error Resume Next
		
		If lvSearchList.Visible = True Then
			If VB6.PixelsToTwipsX(Me.Width) < 10000 Then
				Me.Width = VB6.TwipsToPixelsX(10000)
			End If
			
			If VB6.PixelsToTwipsY(Me.Height) < 9000 Then
				Me.Height = VB6.TwipsToPixelsY(9000)
				picSearch.Height = VB6.TwipsToPixelsY(4000)
			End If
		Else
			If VB6.PixelsToTwipsX(Me.Width) < 6000 Then
				Me.Width = VB6.TwipsToPixelsX(6000)
			End If
			If VB6.PixelsToTwipsY(Me.Height) < 5000 Then
				Me.Height = VB6.TwipsToPixelsY(5000)
			End If
		End If
		
		SizeControls(splitVTarget, splitHTarget)
		
		If Me.Visible = True Then
			lvDetail.Items.Clear()
			LoadListView()
		End If
		
	End Sub
	
	Public Sub LoadTreeView()
		Dim serNode, apsNode, tpsNode, runNode As System.Windows.Forms.TreeNode
		Dim comparetps, compareaps, compareser As String
		Dim apsrs, tpsrs As ADODB.Recordset
		Dim serrs, runrs As ADODB.Recordset
		Dim J, i, Z As Short
		Dim comparerun As Date
		
		OpenDB("FaultFile")
		
		tvTrack.Nodes.Clear()
		tvTrack.Sorted = True
		apsNode = tvTrack.Nodes.Add("")
		'tvTrack.LabelEdit = False 'Disable to prevent treeview from being edited
		tvTrack.ShowRootLines = True
		
		apsNode.Text = "APS Listing"
		apsNode.Name = "HEAD"
		apsNode.Tag = "HEAD"
		apsNode.ImageKey = "closed"
		
		apsrs = rsOpen("SELECT Min(ID) AS MinID,APSName FROM Faults " & "GROUP BY APSName ORDER BY APSName", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If apsrs.BOF = False Or apsrs.EOF = False Then
			apsrs.MoveFirst()
			Do While Not apsrs.EOF
				
				'UPGRADE_WARNING: Cannot determine Node location Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="196D987F-2118-46D0-80D2-92FB2909C206"'
				apsNode = tvTrack.Nodes.Insert(1, "G" & apsrs.Fields("MinID").Value, CStr(apsrs.Fields("APSName").Value), "closed")
				apsNode.Tag = "G" & apsrs.Fields("MinID").Value
				tpsrs = rsOpen("SELECT Min(ID) AS MinID, TPSName From Faults " & "Where APSName = '" & SQLize(apsrs.Fields("APSName").Value) & "' GROUP BY TPSName ORDER BY TPSName", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
				If tpsrs.BOF = False Or tpsrs.EOF = False Then
					tpsrs.MoveFirst()
					Do While Not tpsrs.EOF
						'UPGRADE_WARNING: Cannot determine Node location Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="196D987F-2118-46D0-80D2-92FB2909C206"'
						tpsNode = tvTrack.Nodes.Insert(apsNode.Index, "T_G" & tpsrs.Fields("MinID").Value, CStr(tpsrs.Fields("TPSName").Value), "closed")
						tpsNode.Tag = "T_G" & tpsrs.Fields("MinID").Value
						serrs = rsOpen("SELECT Min(ID) AS MinID, SerialNumber From Faults " & "Where TPSName = '" & SQLize(tpsrs.Fields("TPSName").Value) & "' AND APSName = '" & SQLize(apsrs.Fields("APSName").Value) & "' GROUP BY SerialNumber ORDER BY SerialNumber", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
						If serrs.BOF = False Or serrs.EOF = False Then
							serrs.MoveFirst()
							Do While Not serrs.EOF
								'UPGRADE_WARNING: Cannot determine Node location Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="196D987F-2118-46D0-80D2-92FB2909C206"'
								serNode = tvTrack.Nodes.Insert(tpsNode.Index, "S_G" & serrs.Fields("MinID").Value, CStr(serrs.Fields("SerialNumber").Value), "closed")
								serNode.Tag = "S_G" & serrs.Fields("MinID").Value
								runrs = rsOpen("SELECT Min(ID) AS MinID, RunDate From " & "Faults Where TPSName = '" & SQLize(tpsrs.Fields("TPSName").Value) & "' AND APSName = '" & SQLize(apsrs.Fields("APSName").Value) & "' AND SerialNumber = '" & SQLize(serrs.Fields("SerialNumber").Value) & "' GROUP BY RunDate ORDER BY RunDate", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
								If runrs.BOF = False Or runrs.EOF = False Then
									runrs.MoveFirst()
									Do While Not runrs.EOF
										'UPGRADE_WARNING: Cannot determine Node location Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="196D987F-2118-46D0-80D2-92FB2909C206"'
										runNode = tvTrack.Nodes.Insert(serNode.Index, "R_G" & runrs.Fields("MinID").Value, CStr(runrs.Fields("RunDate").Value), "file")
										runNode.Tag = "R_G" & runrs.Fields("MinID").Value
										runrs.MoveNext()
									Loop 
								End If
								runrs.Close()
								'UPGRADE_NOTE: Object runrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
								runrs = Nothing
								serrs.MoveNext()
							Loop 
						End If
						serrs.Close()
						'UPGRADE_NOTE: Object serrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						serrs = Nothing
						tpsrs.MoveNext()
					Loop 
				End If
				tpsrs.Close()
				'UPGRADE_NOTE: Object tpsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				tpsrs = Nothing
				If Not apsrs.EOF Then
					apsrs.MoveNext()
				End If
			Loop 
		End If
		apsrs.Close()
		'UPGRADE_NOTE: Object apsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		apsrs = Nothing
		
		CloseDB("FaultFile")
		
	End Sub
	
	Public Sub AdjustTree()
		Dim i As Short
		
		If BeenSelected = 0 Then
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvTrack.Nodes.Item(1).Expand()
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			tvTrack.Nodes.Item(1).Selected = True
			lastID = "HEAD"
		Else
			For i = tvTrack.Nodes.Count To 1 Step -1
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).Tag = lastID Then
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: MSComctlLib.Node property tvTrack.Nodes.Image has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6BA9B8D2-2A32-4B6E-8D36-44949974A5B4"'
					If tvTrack.Nodes.Item(i).ImageKey = "open" Then
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tvTrack.Nodes.Item(i).Expand()
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
						tvTrack.Nodes.Item(i).Selected = True
					End If
				End If
			Next i
		End If
		
	End Sub
	
	Private Sub LoadListView()
		Dim apsrs As ADODB.Recordset
		Dim tpsrs As ADODB.Recordset
		Dim serrs As ADODB.Recordset
		Dim runrs As ADODB.Recordset
		Dim J, i, Z As Short
		Dim lvListItem As System.Windows.Forms.ListViewItem
		
		OpenDB("FaultFile")
		
		If lastID = "HEAD" Then
			apsrs = rsOpen("SELECT Min(ID) AS MinID,APSName FROM Faults " & "GROUP BY APSName ORDER BY APSName", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If apsrs.BOF = False Or apsrs.EOF = False Then
				apsrs.MoveFirst()
				Do While Not apsrs.EOF
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems.ImageList has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: SmallIcon parameter was not upgraded Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B392B480-6E26-492E-84C3-16C21D8D0807"'
					lvListItem = lvDetail.Items.Add("G" & apsrs.Fields("MinID").Value, GetDBString(apsrs, "APSName"), "closed1")
					If Not apsrs.EOF Then
						apsrs.MoveNext()
					End If
				Loop 
			End If
			apsrs.Close()
			'UPGRADE_NOTE: Object apsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			apsrs = Nothing
		ElseIf VB.Left(lastID, 1) = "G" Then 
			tpsrs = rsOpen("SELECT Min(ID) AS MinID, TPSName From Faults " & "Where APSName = '" & SQLize(tvSelectApsName) & "' GROUP BY TPSName ORDER BY TPSName", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If tpsrs.BOF = False Or tpsrs.EOF = False Then
				tpsrs.MoveFirst()
				Do While Not tpsrs.EOF
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems.ImageList has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: SmallIcon parameter was not upgraded Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B392B480-6E26-492E-84C3-16C21D8D0807"'
					lvListItem = lvDetail.Items.Add("T_G" & tpsrs.Fields("MinID").Value, GetDBString(tpsrs, "TPSName"), "closed1")
					If Not tpsrs.EOF Then
						tpsrs.MoveNext()
					End If
				Loop 
			End If
			tpsrs.Close()
			'UPGRADE_NOTE: Object tpsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			tpsrs = Nothing
		ElseIf VB.Left(lastID, 1) = "T" Then 
			serrs = rsOpen("SELECT Min(ID) AS MinID, SerialNumber From Faults " & "Where TPSName = '" & SQLize(tvSelectTpsName) & "' AND APSName = '" & SQLize(tvSelectApsName) & "' GROUP BY SerialNumber ORDER BY SerialNumber", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If serrs.BOF = False Or serrs.EOF = False Then
				serrs.MoveFirst()
				Do While Not serrs.EOF
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems.ImageList has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_WARNING: SmallIcon parameter was not upgraded Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="B392B480-6E26-492E-84C3-16C21D8D0807"'
					lvListItem = lvDetail.Items.Add("S_G" & serrs.Fields("MinID").Value, GetDBString(serrs, "SerialNumber"), "closed1")
					If Not serrs.EOF Then
						serrs.MoveNext()
					End If
				Loop 
			End If
			serrs.Close()
			'UPGRADE_NOTE: Object serrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			serrs = Nothing
		ElseIf VB.Left(lastID, 1) = "S" Then 
			runrs = rsOpen("SELECT Min(ID) AS MinID, RunDate From " & "Faults Where TPSName = '" & SQLize(tvSelectTpsName) & "' AND APSName = '" & SQLize(tvSelectApsName) & "' AND SerialNumber = '" & SQLize(tvSelectSerialNumber) & "' GROUP BY RunDate ORDER BY RunDate", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If runrs.BOF = False Or runrs.EOF = False Then
				runrs.MoveFirst()
				Do While Not runrs.EOF
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems.ImageList has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					lvListItem = lvDetail.Items.Add("R_G" & runrs.Fields("MinID").Value, GetDBString(runrs, "RunDate"), "file")
					If Not runrs.EOF Then
						runrs.MoveNext()
					End If
				Loop 
			End If
			runrs.Close()
			'UPGRADE_NOTE: Object runrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			runrs = Nothing
		End If
		
		CloseDB("FaultFile")
		
	End Sub
	
	Private Sub buildListEntries()
		Dim rs As ADODB.Recordset
		Dim i As Short
		
		OpenDB("FaultFile")
		
		rs = rsOpen("SELECT Min(ID) AS MinID,APSName FROM Faults " & "GROUP BY APSName ORDER BY APSName", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			VB6.SetItemString(cmbAPSName, 0, "<ALL>")
			i = 1
			Do While Not rs.EOF
				VB6.SetItemString(cmbAPSName, i, CStr(rs.Fields("APSName").Value))
				i = i + 1
				If Not rs.EOF Then
					rs.MoveNext()
				End If
			Loop 
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		cmbAPSName.Text = VB6.GetItemString(cmbAPSName, 0)
		
		rs = rsOpen("SELECT Min(ID) AS MinID,TPSName FROM Faults " & "GROUP BY TPSName ORDER BY TPSName", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			VB6.SetItemString(cmbTPSName, 0, "<ALL>")
			i = 1
			Do While Not rs.EOF
				VB6.SetItemString(cmbTPSName, i, CStr(rs.Fields("TPSName").Value))
				i = i + 1
				If Not rs.EOF Then
					rs.MoveNext()
				End If
			Loop 
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		cmbTPSName.Text = VB6.GetItemString(cmbTPSName, 0)
		
		rs = rsOpen("SELECT Min(ID) AS MinID,SerialNumber FROM Faults " & "GROUP BY SerialNumber ORDER BY SerialNumber", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			VB6.SetItemString(cmbSerialNumber, 0, "<ALL>")
			i = 1
			Do While Not rs.EOF
				VB6.SetItemString(cmbSerialNumber, i, CStr(rs.Fields("SerialNumber").Value))
				i = i + 1
				If Not rs.EOF Then
					rs.MoveNext()
				End If
			Loop 
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		cmbSerialNumber.Text = VB6.GetItemString(cmbSerialNumber, 0)
		
		rs = rsOpen("SELECT Min(ID) AS MinID,ERONumber FROM Faults " & "GROUP BY ERONumber ORDER BY ERONumber", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			VB6.SetItemString(cmbERONumber, 0, "<ALL>")
			i = 1
			Do While Not rs.EOF
				VB6.SetItemString(cmbERONumber, i, CStr(rs.Fields("ERONumber").Value))
				i = i + 1
				If Not rs.EOF Then
					rs.MoveNext()
				End If
			Loop 
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		cmbERONumber.Text = VB6.GetItemString(cmbERONumber, 0)
		
		rs = rsOpen("SELECT Min(ID) AS MinID,RunDate FROM Faults " & "GROUP BY RunDate ORDER BY RunDate", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			dtpFrom.Value = rs.Fields("RunDate")
			rs.MoveLast()
			dtpTo.Value = rs.Fields("RunDate")
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		
		CloseDB("FaultFile")
		
	End Sub
	
	Private Sub redoComboBoxes()
		Dim searchString As String
		Dim searchString1 As String
		Dim rs As ADODB.Recordset
		Dim Z, i, J, totalSelected As Short
		
		OpenDB("FaultFile")
		
		searchString1 = SearchForString
		
		If APSNameBeenSelected = False Then
			
			searchString = "SELECT APSName From Faults " & searchString1 & " GROUP BY APSName ORDER BY APSName"
			
			cmbAPSName.Items.Clear()
			rs = rsOpen(searchString, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If rs.BOF = False Or rs.EOF = False Then
				rs.MoveFirst()
				VB6.SetItemString(cmbAPSName, 0, "<ALL>")
				i = 1
				Do While Not rs.EOF
					VB6.SetItemString(cmbAPSName, i, CStr(rs.Fields("APSName").Value))
					i = i + 1
					If Not rs.EOF Then
						rs.MoveNext()
					End If
				Loop 
			End If
			rs.Close()
			'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rs = Nothing
			cmbAPSName.Text = VB6.GetItemString(cmbAPSName, 0)
		End If
		
		If TPSNameBeenSelected = False Then
			
			searchString = "SELECT TPSName From Faults " & searchString1 & " GROUP BY TPSName ORDER BY TPSName"
			
			cmbTPSName.Items.Clear()
			rs = rsOpen(searchString, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If rs.BOF = False Or rs.EOF = False Then
				rs.MoveFirst()
				VB6.SetItemString(cmbTPSName, 0, "<ALL>")
				i = 1
				Do While Not rs.EOF
					VB6.SetItemString(cmbTPSName, i, CStr(rs.Fields("TPSName").Value))
					i = i + 1
					If Not rs.EOF Then
						rs.MoveNext()
					End If
				Loop 
			End If
			rs.Close()
			'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rs = Nothing
			cmbTPSName.Text = VB6.GetItemString(cmbTPSName, 0)
		End If
		
		If SerialNumberBeenSelected = False Then
			
			searchString = "SELECT SerialNumber From Faults " & searchString1 & " GROUP BY SerialNumber ORDER BY SerialNumber"
			
			cmbSerialNumber.Items.Clear()
			rs = rsOpen(searchString, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If rs.BOF = False Or rs.EOF = False Then
				rs.MoveFirst()
				VB6.SetItemString(cmbSerialNumber, 0, "<ALL>")
				i = 1
				Do While Not rs.EOF
					VB6.SetItemString(cmbSerialNumber, i, CStr(rs.Fields("SerialNumber").Value))
					i = i + 1
					If Not rs.EOF Then
						rs.MoveNext()
					End If
				Loop 
			End If
			rs.Close()
			'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rs = Nothing
			cmbSerialNumber.Text = VB6.GetItemString(cmbSerialNumber, 0)
		End If
		
		If ERONumberBeenSelected = False Then
			
			searchString = "SELECT ERONumber From Faults " & searchString1 & " GROUP BY ERONumber ORDER BY ERONumber"
			
			cmbERONumber.Items.Clear()
			rs = rsOpen(searchString, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If rs.BOF = False Or rs.EOF = False Then
				rs.MoveFirst()
				VB6.SetItemString(cmbERONumber, 0, "<ALL>")
				i = 1
				Do While Not rs.EOF
					VB6.SetItemString(cmbERONumber, i, CStr(rs.Fields("ERONumber").Value))
					i = i + 1
					If Not rs.EOF Then
						rs.MoveNext()
					End If
				Loop 
			End If
			rs.Close()
			'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rs = Nothing
			cmbERONumber.Text = VB6.GetItemString(cmbERONumber, 0)
		End If
		
		If FromRunDateBeenSelected = False Or ToRunDateBeenSelected = False Then
			
			searchString = "SELECT RunDate From Faults " & searchString1 & " GROUP BY RunDate ORDER BY RunDate"
			
			rs = rsOpen(searchString, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			If rs.BOF = False Or rs.EOF = False Then
				rs.MoveFirst()
				dtpFrom.Value = rs.Fields("RunDate")
				rs.MoveLast()
				dtpTo.Value = rs.Fields("RunDate")
			End If
			rs.Close()
			'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			rs = Nothing
		End If
		
		CloseDB("FaultFile")
		
	End Sub
	
	Private Sub doNodeClick(ByVal Node As System.Windows.Forms.TreeNode)
		Dim xNode As System.Windows.Forms.TreeNode
		Dim dog As String
		Dim i As Short
		
		BeenSelected = 1
		xNode = Node
		lastID = xNode.Tag
		refreshID = lastID
		
		For i = tvTrack.Nodes.Count To 1 Step -1
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			If tvTrack.Nodes.Item(i).Tag = lastID Then
				Exit For
			End If
		Next i
		
		If lastID = "HEAD" Then
			lblTitle(1).Text = "APS Names:"
			tvSelectApsName = ""
			tvSelectTpsName = ""
			tvSelectSerialNumber = ""
			tvSelectRunDate = ""
			
			'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			sbStatusBar.Items.Item(1).Text = "Number Of Application Program Names  "
			
			setViewList()
			GoTo outofhere
		End If
		If VB.Left(lastID, 1) = "G" Then
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectApsName = tvTrack.Nodes.Item(i).Text
			tvSelectTpsName = ""
			tvSelectSerialNumber = ""
			tvSelectRunDate = ""
			lblTitle(1).Text = "TPS Names:"
			
			'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			sbStatusBar.Items.Item(1).Text = "Number Of Test Program Names  "
			
			setViewList()
		ElseIf VB.Left(lastID, 1) = "T" Then 
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectTpsName = tvTrack.Nodes.Item(i).Text
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectApsName = tvTrack.Nodes.Item(i).Parent.Text
			tvSelectSerialNumber = ""
			tvSelectRunDate = ""
			lblTitle(1).Text = "Serial Numbers:"
			
			'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			sbStatusBar.Items.Item(1).Text = "Number Of Test Program Serial Numbers  "
			
			setViewList()
		ElseIf VB.Left(lastID, 1) = "S" Then 
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectTpsName = tvTrack.Nodes.Item(i).Parent.Text
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectApsName = tvTrack.Nodes.Item(i).Parent.Parent.Text
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectSerialNumber = tvTrack.Nodes.Item(i).Text
			tvSelectRunDate = ""
			lblTitle(1).Text = "Date TPS Was Run:"
			
			'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			sbStatusBar.Items.Item(1).Text = "Number Of Test Program Runs  "
			
			setViewList()
		ElseIf VB.Left(lastID, 1) = "R" Then 
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectSerialNumber = tvTrack.Nodes.Item(i).Parent.Text
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectTpsName = tvTrack.Nodes.Item(i).Parent.Parent.Text
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectApsName = tvTrack.Nodes.Item(i).Parent.Parent.Parent.Text
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvSelectRunDate = tvTrack.Nodes.Item(i).Text
			lblTitle(1).Text = "Test Program Results:"
			setViewFrame()
			
			'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			sbStatusBar.Items.Item(1).Text = "Test Program Result"
			displayFaultFile()
		End If
		
outofhere: 
		i = tvTrack.Nodes.Count
		lvDetail.Items.Clear()
		LoadListView()
		
		If VB.Left(lastID, 1) <> "R" Then
			'UPGRADE_WARNING: Lower bound of collection sbStatusBar.Panels has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			sbStatusBar.Items.Item(1).Text = sbStatusBar.Items.Item(1).Text & lvDetail.Items.Count
		End If
		
	End Sub
	
	Private Sub setViewList()
		
		picStatusInfo.Visible = False
		fraStatusInfo.Visible = False
		lvDetail.Visible = True
		lblTitle(1).Visible = True
		tbToolBar.Items.Item(LISTVIEW_MODE0).Enabled = True
		tbToolBar.Items.Item(LISTVIEW_MODE1).Enabled = True
		tbToolBar.Items.Item(LISTVIEW_MODE2).Enabled = True
		mnuListViewMode(0).Enabled = True
		mnuListViewMode(1).Enabled = True
		mnuListViewMode(2).Enabled = True
		
	End Sub
	
	Private Sub setViewFrame()
		
		picStatusInfo.Visible = True
		fraStatusInfo.Visible = True
		lvDetail.Visible = False
		tbToolBar.Items.Item(LISTVIEW_MODE0).Enabled = False
		tbToolBar.Items.Item(LISTVIEW_MODE1).Enabled = False
		tbToolBar.Items.Item(LISTVIEW_MODE2).Enabled = False
		mnuListViewMode(0).Enabled = False
		mnuListViewMode(1).Enabled = False
		mnuListViewMode(2).Enabled = False
		frameSelected = "Text"
		
	End Sub
	
	Private Sub displayFaultFile()
		Dim rs As ADODB.Recordset
		Dim tempID As String
		Dim S As String
		
		OpenDB("FaultFile")
		
		S = "SELECT Status From " & "Faults Where TPSName = '" & SQLize(tvSelectTpsName) & "' AND APSName = '" & SQLize(tvSelectApsName) & "' AND SerialNumber = '" & SQLize(tvSelectSerialNumber) & "' AND RunDate = #" & tvSelectRunDate & "#"
		rs = rsOpen(S, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveFirst()
			txFaultFileStatus.Text = GetDBString(rs, "Status")
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		
		CloseDB("FaultFile")
		
	End Sub
	
	Private Sub SetToolTiplvDetail(ByVal X As Single, ByVal Y As Single)
		Static last_x As Single
		Static last_y As Single
		Static node_over As System.Windows.Forms.ListViewItem
		Static tempID As String
		Dim new_node_over As System.Windows.Forms.ListViewItem
		
		If (last_x = X) And (last_y = Y) Then Exit Sub
		
		last_x = X
		last_y = Y
		
		new_node_over = lvDetail.GetItemAt(X, Y)
		
		If node_over Is new_node_over Then Exit Sub
		
		node_over = new_node_over
		
		If new_node_over Is Nothing Then
			ToolTip1.SetToolTip(lvDetail, "")
			Exit Sub
		Else
			tempID = new_node_over.Name
		End If
		
		If (tempID = "HEAD") Or (tempID = "") Then Exit Sub
		
		If VB.Left(tempID, 1) = "G" Then
			ToolTip1.SetToolTip(lvDetail, "APS Name")
		ElseIf VB.Left(tempID, 1) = "T" Then 
			ToolTip1.SetToolTip(lvDetail, "TPS Name")
		ElseIf VB.Left(tempID, 1) = "S" Then 
			ToolTip1.SetToolTip(lvDetail, "Serial Number")
		ElseIf VB.Left(tempID, 1) = "R" Then 
			ToolTip1.SetToolTip(lvDetail, "Date Tps Was Run")
		End If
		
	End Sub
	
	Private Sub SetToolTiptvTrack(ByVal X As Single, ByVal Y As Single)
		Static last_x As Single
		Static last_y As Single
		Static tempID As String
		Static node_over As System.Windows.Forms.TreeNode
		Dim new_node_over As System.Windows.Forms.TreeNode
		
		If (last_x = X) And (last_y = Y) Then Exit Sub
		
		last_x = X
		last_y = Y
		
		new_node_over = tvTrack.GetNodeAt(X, Y)
		
		If node_over Is new_node_over Then Exit Sub
		
		node_over = new_node_over
		
		If new_node_over Is Nothing Then
			ToolTip1.SetToolTip(tvTrack, "")
			Exit Sub
		Else
			tempID = new_node_over.Tag
		End If
		
		If (tempID = "HEAD") Or (tempID = "") Then Exit Sub
		
		If VB.Left(tempID, 1) = "G" Then
			ToolTip1.SetToolTip(tvTrack, "APS Name")
		ElseIf VB.Left(tempID, 1) = "T" Then 
			ToolTip1.SetToolTip(tvTrack, "TPS Name")
		ElseIf VB.Left(tempID, 1) = "S" Then 
			ToolTip1.SetToolTip(tvTrack, "Serial Number")
		ElseIf VB.Left(tempID, 1) = "R" Then 
			ToolTip1.SetToolTip(tvTrack, "Date Tps Was Run")
		End If
		
	End Sub
	
	Private Sub seedSearchList()
		Dim i As Short
		Dim rs As ADODB.Recordset
		Dim apsrs As ADODB.Recordset
		Dim itemID As String
		Dim lngLocale As Integer
		Dim tempstring As String
		
		If frameSelected = "Detail" Then
			
			For i = lvDetail.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvDetail.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvDetail.Items.Item(i).Name, 1) = "G" Then
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 2)
					Else
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
					End If
					
					OpenDB("FaultFile")
					
					rs = rsOpen("SELECT APSName, TPSName, RunDate, SerialNumber, ERONumber From Faults" & " Where ID = " & SQLize(itemID), ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
					
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvDetail.Items.Item(i).Name, 1) = "G" Then
						upDateAPSList(rs.Fields("APSName").Value)
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "T" Then 
						
						upDateAPSList(rs.Fields("APSName").Value)
						upDateTPSList(rs.Fields("TPSName").Value)
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "S" Then 
						
						upDateAPSList(rs.Fields("APSName").Value)
						upDateTPSList(rs.Fields("TPSName").Value)
						upDateSerialList(rs.Fields("SerialNumber").Value)
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "R" Then 
						
						upDateAPSList(rs.Fields("APSName").Value)
						upDateTPSList(rs.Fields("TPSName").Value)
						upDateSerialList(rs.Fields("SerialNumber").Value)
						upDateFromBox(rs.Fields("RunDate").Value)
						upDateToBox(rs.Fields("RunDate").Value)
						
					End If
					rs.Close()
					'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					rs = Nothing
					
					CloseDB("FaultFile")
					
					redoComboBoxes()
				End If
			Next i
		ElseIf frameSelected = "Search" Then 
			
			For i = lvSearchList.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvSearchList.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvSearchList.Items.Item(i).Name, 1) = "G" Then
						'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvSearchList.Items.Item(i).Name, 2)
					Else
						'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvSearchList.Items.Item(i).Name, 4)
					End If
					
					OpenDB("FaultFile")
					
					rs = rsOpen("SELECT APSName, TPSName, RunDate, SerialNumber, ERONumber From Faults" & " Where ID = " & SQLize(itemID), ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
					
					'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvSearchList.Items.Item(i).Name, 1) = "G" Then
						upDateAPSList(rs.Fields("APSName").Value)
						
						'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvSearchList.Items.Item(i).Name, 1) = "T" Then 
						
						upDateAPSList(rs.Fields("APSName").Value)
						upDateTPSList(rs.Fields("TPSName").Value)
						
						'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvSearchList.Items.Item(i).Name, 1) = "S" Then 
						
						upDateAPSList(rs.Fields("APSName").Value)
						upDateTPSList(rs.Fields("TPSName").Value)
						upDateSerialList(rs.Fields("SerialNumber").Value)
						
						'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvSearchList.Items.Item(i).Name, 1) = "R" Then 
						
						upDateAPSList(rs.Fields("APSName").Value)
						upDateTPSList(rs.Fields("TPSName").Value)
						upDateSerialList(rs.Fields("SerialNumber").Value)
						upDateFromBox(rs.Fields("RunDate").Value)
						upDateToBox(rs.Fields("RunDate").Value)
						
					End If
					rs.Close()
					'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					rs = Nothing
					
					CloseDB("FaultFile")
					
					redoComboBoxes()
					
				End If
			Next i
		ElseIf frameSelected = "Track" Then 
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).IsSelected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "G" Then
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 2)
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf tvTrack.Nodes.Item(i).Tag = "HEAD" Then 
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = tvTrack.Nodes.Item(i).Tag
					Else
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
					End If
					
					If itemID <> "HEAD" Then
						
						OpenDB("FaultFile")
						
						rs = rsOpen("SELECT APSName, TPSName, RunDate, SerialNumber, ERONumber From Faults" & " Where ID = " & SQLize(itemID), ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
						
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						If VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "G" Then
							
							upDateAPSList(rs.Fields("APSName").Value)
							
							'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "T" Then 
							
							upDateAPSList(rs.Fields("APSName").Value)
							upDateTPSList(rs.Fields("TPSName").Value)
							
							'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "S" Then 
							
							upDateAPSList(rs.Fields("APSName").Value)
							upDateTPSList(rs.Fields("TPSName").Value)
							upDateSerialList(rs.Fields("SerialNumber").Value)
							
							'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "R" Then 
							
							upDateAPSList(rs.Fields("APSName").Value)
							upDateTPSList(rs.Fields("TPSName").Value)
							upDateSerialList(rs.Fields("SerialNumber").Value)
							upDateFromBox(rs.Fields("RunDate").Value)
							upDateToBox(rs.Fields("RunDate").Value)
							
						End If
						rs.Close()
						'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
						rs = Nothing
						
						CloseDB("FaultFile")
						
						redoComboBoxes()
					Else
						
						APSNameBeenSelected = False
						TPSNameBeenSelected = False
						SerialNumberBeenSelected = False
						ERONumberBeenSelected = False
						FromRunDateBeenSelected = False
						ToRunDateBeenSelected = False
						
						lvSearchList.Items.Clear()
						
						buildListEntries()
						
					End If
				End If
			Next i
		End If
		
	End Sub
	
	Private Sub upDateAPSList(ByRef APSName As String)
		
		APSNameBeenSelected = True
		selectedAPSName = APSName
		cmbAPSName.Items.Clear()
		VB6.SetItemString(cmbAPSName, 0, selectedAPSName)
		cmbAPSName.Text = VB6.GetItemString(cmbAPSName, 0)
		
	End Sub
	
	Private Sub upDateTPSList(ByRef TPSName As String)
		
		TPSNameBeenSelected = True
		selectedTPSName = TPSName
		cmbTPSName.Items.Clear()
		VB6.SetItemString(cmbTPSName, 0, selectedTPSName)
		cmbTPSName.Text = VB6.GetItemString(cmbTPSName, 0)
		
	End Sub
	
	Private Sub upDateEROList(ByRef ERONumber As String)
		
		ERONumberBeenSelected = True
		selectedERONumber = ERONumber
		cmbERONumber.Items.Clear()
		VB6.SetItemString(cmbERONumber, 0, selectedERONumber)
		cmbERONumber.Text = VB6.GetItemString(cmbERONumber, 0)
		
	End Sub
	
	Private Sub upDateSerialList(ByRef SerialNumber As String)
		
		SerialNumberBeenSelected = True
		selectedSerialNumber = SerialNumber
		cmbSerialNumber.Items.Clear()
		VB6.SetItemString(cmbSerialNumber, 0, selectedSerialNumber)
		cmbSerialNumber.Text = VB6.GetItemString(cmbSerialNumber, 0)
		
	End Sub
	
	Private Sub upDateFromBox(ByRef FromDate As Date)
		
		FromRunDateBeenSelected = True
		selectedFromRunDate = FromDate
		dtpFrom.Value = FromDate
		
	End Sub
	
	Private Sub upDateToBox(ByRef ToDate As Date)
		
		ToRunDateBeenSelected = True
		selectedToRunDate = ToDate
		dtpTo.Value = ToDate
		
	End Sub
	
	Private Sub postRecordDelete()
		Dim i As Short
		Dim tempstring As String
		Dim notFoundYet As Boolean
		Dim tNode As System.Windows.Forms.TreeNode
		
		notFoundYet = True
		
		lvDetail.Items.Clear()
		tvTrack.Nodes.Clear()
		
		LoadTreeView()
		LoadListView()
		cmbSearchNow_Click(cmbSearchNow, New System.EventArgs())
		
		If tvSelectRunDate <> "" Then
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).Text = tvSelectRunDate Then
					notFoundYet = False
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					doNodeClick(tvTrack.Nodes.Item(i))
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					savedNode = tvTrack.Nodes.Item(i)
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
					tvTrack.Nodes.Item(i).Selected = True
					On Error GoTo OuttaHere
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					tNode = tvTrack.Nodes.Item(i).Parent
					
					Do 
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tvTrack.Nodes.Item(i).Expand()
						tNode = tNode.Parent
					Loop Until tNode.Index <= 1
					
					'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					tNode = Nothing
					Exit For
				End If
			Next i
		End If
		
		If tvSelectSerialNumber <> "" And notFoundYet = True Then
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).Text = tvSelectSerialNumber Then
					notFoundYet = False
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					doNodeClick(tvTrack.Nodes.Item(i))
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					savedNode = tvTrack.Nodes.Item(i)
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
					tvTrack.Nodes.Item(i).Selected = True
					On Error GoTo OuttaHere
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					tNode = tvTrack.Nodes.Item(i).Parent
					
					Do 
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tvTrack.Nodes.Item(i).Expand()
						tNode = tNode.Parent
					Loop Until tNode.Index <= 1
					
					'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					tNode = Nothing
					Exit For
				End If
			Next i
		End If
		
		If tvSelectTpsName <> "" And notFoundYet = True Then
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).Text = tvSelectTpsName Then
					notFoundYet = False
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					doNodeClick(tvTrack.Nodes.Item(i))
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					savedNode = tvTrack.Nodes.Item(i)
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
					tvTrack.Nodes.Item(i).Selected = True
					On Error GoTo OuttaHere
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					tNode = tvTrack.Nodes.Item(i).Parent
					
					Do 
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tvTrack.Nodes.Item(i).Expand()
						tNode = tNode.Parent
					Loop Until tNode.Index <= 1
					
					'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					tNode = Nothing
					Exit For
				End If
			Next i
		End If
		
		If tvSelectApsName <> "" And notFoundYet = True Then
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).Text = tvSelectApsName Then
					notFoundYet = False
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					doNodeClick(tvTrack.Nodes.Item(i))
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					savedNode = tvTrack.Nodes.Item(i)
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
					tvTrack.Nodes.Item(i).Selected = True
					On Error GoTo OuttaHere
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					tNode = tvTrack.Nodes.Item(i).Parent
					
					Do 
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tvTrack.Nodes.Item(i).Expand()
						tNode = tNode.Parent
					Loop Until tNode.Index <= 1
					
					'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
					tNode = Nothing
					Exit For
				End If
			Next i
		End If
		
		If notFoundYet = True Then
			
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			tvTrack.Nodes.Item(1).Expand()
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
			tvTrack.Nodes.Item(1).Selected = True
			lastID = "HEAD"
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			doNodeClick(tvTrack.Nodes.Item(1))
			
		End If
		
OuttaHere: 
		
	End Sub
	
	Private Sub doRecordDelete()
		Dim RetValue As Object
		Dim i As Short
		Dim firstTime As Boolean
		Dim rs1String As String
		Dim rs2String As String
		Dim itemID As String
		Dim tempstring As String
		
		tempstring = ""
		firstTime = True
		
		If frameSelected = "Detail" Then
			
			For i = lvDetail.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvDetail.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvDetail.Items.Item(i).Name, 1) = "G" Then
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 2)
						rs1String = "SELECT APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT ID From Faults Where APSName = '"
						
						deleteTheRecords(rs1String, rs2String, "First")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "T" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT ID From Faults Where APSName = '"
						
						deleteTheRecords(rs1String, rs2String, "Second")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "S" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, SerialNumber, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT ID From Faults Where APSName = '"
						
						deleteTheRecords(rs1String, rs2String, "Third")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "R" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tempstring = Mid(lvDetail.Items.Item(i).Name, 4)
						
						deleteTheRecords(tempstring, "", "")
						
					End If
				End If
			Next i
			
		ElseIf frameSelected = "Search" Then 
			
			For i = lvSearchList.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvSearchList.Items.Item(i).Selected = True Then
					
					If firstTime = False Then
						tempstring = tempstring & ","
					End If
					
					firstTime = False
					
					'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					tempstring = tempstring & Mid(lvSearchList.Items.Item(i).Name, 4)
					
				End If
			Next i
			
			deleteTheRecords(tempstring, "", "")
			
		ElseIf frameSelected = "Track" Or frameSelected = "Text" Then 
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).IsSelected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "G" Then
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 2)
						rs1String = "SELECT APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT ID From Faults Where APSName = '"
						
						deleteTheRecords(rs1String, rs2String, "First")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "T" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT ID From Faults Where APSName = '"
						
						deleteTheRecords(rs1String, rs2String, "Second")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "S" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, SerialNumber, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT ID From Faults Where APSName = '"
						
						deleteTheRecords(rs1String, rs2String, "Third")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "R" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						tempstring = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						
						deleteTheRecords(tempstring, "", "")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "H" Then 
						
						MsgBox("You Tried to Delete All Records, This is NOT Allowed.")
						
					End If
				End If
			Next i
		End If
		
		postRecordDelete()
		
	End Sub
	
	Private Function doFileStatusToMedia() As Object
		Dim file As Object
		Dim RetValue As Object
		Dim i As Short
		Dim rs1String As String
		Dim rs2String As String
		Dim itemID As String
		Dim lngLocale As Integer
		Dim tempstring As String
		'UPGRADE_NOTE: Leave was upgraded to Leave_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Leave_Renamed As Boolean
		
		lngLocale = GetSystemDefaultLCID()
		
		If SetLocaleInfo(lngLocale, LOCALE_SLONGDATE, "dd-MMM-yy") = False Then
			On Error GoTo OuttaHere
		End If
		
		
		If frameSelected = "Detail" Then
			
			For i = lvDetail.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvDetail.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvDetail.Items.Item(i).Name, 1) = "G" Then
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 2)
						rs1String = "SELECT APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT TPSName, RunDate, SerialNumber, Status From Faults Where APSName = '"
						
						sendToMedia(rs1String, rs2String, "First")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "T" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT TPSName, RunDate, SerialNumber, Status From Faults Where APSName = '"
						
						sendToMedia(rs1String, rs2String, "Second")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "S" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, SerialNumber, APSName From Faults " & "Where ID = " & SQLize(itemID)
						rs2String = "SELECT TPSName, RunDate, SerialNumber, Status From Faults Where APSName = '"
						
						sendToMedia(rs1String, rs2String, "Third")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "R" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, SerialNumber, RunDate, APSName, Status " & "From Faults Where ID = " & SQLize(itemID)
						
						sendToMedia(rs1String, "", "")
						
					End If
				End If
			Next i
		ElseIf frameSelected = "Track" Then 
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).IsSelected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "G" Then
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 2)
						rs1String = "SELECT APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT TPSName, RunDate, SerialNumber, Status From Faults Where APSName = '"
						
						sendToMedia(rs1String, rs2String, "First")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "T" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT TPSName, RunDate, SerialNumber, Status From Faults Where APSName = '"
						
						sendToMedia(rs1String, rs2String, "Second")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "S" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, SerialNumber, APSName From Faults " & "Where ID = " & SQLize(itemID)
						rs2String = "SELECT TPSName, RunDate, SerialNumber, Status From Faults Where APSName = '"
						
						sendToMedia(rs1String, rs2String, "Third")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "R" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, SerialNumber, RunDate, APSName, Status " & "From Faults Where ID = " & SQLize(itemID)
						
						sendToMedia(rs1String, "", "")
						
					End If
				End If
			Next i
			
		ElseIf frameSelected = "Search" Then 
			
			For i = lvSearchList.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvSearchList.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					itemID = Mid(lvSearchList.Items.Item(i).Name, 4)
					rs1String = "SELECT TPSName, SerialNumber, RunDate, APSName, Status " & "From Faults Where ID = " & SQLize(itemID)
					rs2String = ""
					
					sendToMedia(rs1String, "", "")
					
				End If
			Next i
			
		ElseIf frameSelected = "Text" Then 
			
			tempstring = tvSelectApsName & " " & tvSelectTpsName & " " & tvSelectSerialNumber & " " & tvSelectRunDate & ".txt"
			
			'UPGRADE_WARNING: Couldn't resolve default property of object file. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			file = DIRNAME & "\" & deColonIt(tempstring)
			
			
			Do 
				
				If checkRemovableMedia = True Then
					
					Leave_Renamed = True
					
					'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
					If Dir(DIRNAME, FileAttribute.Directory) = "" Then
						MkDir(DIRNAME)
					End If
					
					'UPGRADE_WARNING: Couldn't resolve default property of object file. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					FileOpen(1, file, OpenMode.Output)
					PrintLine(1, txFaultFileStatus.Text)
					FileClose(1)
					
				Else
					
					'UPGRADE_WARNING: Couldn't resolve default property of object RetValue. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					RetValue = MsgBox("Please Insert Floppy into Drive", MsgBoxStyle.OKCancel)
					
					If RetValue = MsgBoxResult.Cancel Then
						Leave_Renamed = True
					End If
					
				End If
				
			Loop Until Leave_Renamed = True
			
		End If
OuttaHere: 
		
	End Function
	
	Private Sub doPrintStatus()
		Dim i As Short
		Dim itemID As String
		Dim rs1String As String
		Dim rs2String As String
		Dim tempstring As String
		
		On Error Resume Next
		
		'UPGRADE_ISSUE: MSComDlg.CommonDialog property CommonDialog1.Flags was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
		CommonDialog1.Flags = 0
		'UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
		CommonDialog1.CancelError = True
		CommonDialog1Print.ShowDialog()
		CommonDialog1Font.MaxSize = CommonDialog1Print.PrinterSettings.MaximumPage
		CommonDialog1Font.MinSize = CommonDialog1Print.PrinterSettings.MinimumPage
		
		'UPGRADE_WARNING: The CommonDialog CancelError property is not supported in Visual Basic .NET. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="8B377936-3DF7-4745-AA26-DD00FA5B9BE1"'
		If (Err.Number = DialogResult.Cancel) Then Exit Sub
		
		'UPGRADE_ISSUE: Printer property Printer.Orientation was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.Orientation = CommonDialog1Print.PrinterSettings.DefaultPageSettings.Landscape
		'UPGRADE_ISSUE: Printer property Printer.Copies was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.Copies = CommonDialog1Print.PrinterSettings.Copies
		
		'UPGRADE_ISSUE: Printer property Printer.FontName was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.FontName = GetSetting(My.Application.Info.Title, "Settings", "PrinterFontN", "Arial")
		'UPGRADE_ISSUE: Printer property Printer.FontSize was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.FontSize = GetSetting(My.Application.Info.Title, "Settings", "PrinterFontS", CStr(12))
		'UPGRADE_ISSUE: Printer property Printer.FontBold was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.FontBold = GetSetting(My.Application.Info.Title, "Settings", "PrinterFontB", CStr(False))
		'UPGRADE_ISSUE: Printer property Printer.FontItalic was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.FontItalic = GetSetting(My.Application.Info.Title, "Settings", "PrinterFontI", CStr(False))
		'UPGRADE_ISSUE: Printer property Printer.FontUnderline was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.FontUnderline = GetSetting(My.Application.Info.Title, "Settings", "PrinterFontU", CStr(False))
		'UPGRADE_ISSUE: Printer property Printer.FontStrikethru was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.FontStrikethru = GetSetting(My.Application.Info.Title, "Settings", "PrinterFontST", CStr(False))
		'UPGRADE_ISSUE: Printer property Printer.ForeColor was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
		Printer.ForeColor = GetSetting(My.Application.Info.Title, "Settings", "PrinterForeC", CStr(0))
		
		If frameSelected = "Detail" Then
			
			For i = lvDetail.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvDetail.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(lvDetail.Items.Item(i).Name, 1) = "G" Then
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 2)
						rs1String = "SELECT APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT Status From Faults Where APSName = '"
						
						printItOut(rs1String, rs2String, "First")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "T" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT Status From Faults Where APSName = '"
						
						printItOut(rs1String, rs2String, "Second")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "S" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT TPSName, SerialNumber, APSName " & "From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT Status From Faults Where APSName = '"
						
						printItOut(rs1String, rs2String, "Third")
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(lvDetail.Items.Item(i).Name, 1) = "R" Then 
						
						'UPGRADE_WARNING: Lower bound of collection lvDetail.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(lvDetail.Items.Item(i).Name, 4)
						rs1String = "SELECT Status From Faults Where ID = " & SQLize(itemID)
						rs2String = ""
						
						printItOut(rs1String, rs2String, "")
						
					End If
				End If
			Next i
		ElseIf frameSelected = "Track" Then 
			
			For i = tvTrack.Nodes.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If tvTrack.Nodes.Item(i).IsSelected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					If VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "G" Then
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 2)
						rs1String = "SELECT APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT Status From Faults Where APSName = '"
						
						printItOut(rs1String, rs2String, "First")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "T" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, APSName From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT Status From Faults Where APSName = '"
						
						printItOut(rs1String, rs2String, "Second")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "S" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT TPSName, SerialNumber, APSName " & "From Faults Where ID = " & SQLize(itemID)
						rs2String = "SELECT Status From Faults Where APSName = '"
						
						printItOut(rs1String, rs2String, "Third")
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					ElseIf VB.Left(tvTrack.Nodes.Item(i).Tag, 1) = "R" Then 
						
						'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
						itemID = Mid(tvTrack.Nodes.Item(i).Tag, 4)
						rs1String = "SELECT Status From Faults Where ID = " & SQLize(itemID)
						rs2String = ""
						
						printItOut(rs1String, rs2String, "")
						
					End If
				End If
			Next i
		ElseIf frameSelected = "Search" Then 
			
			For i = lvSearchList.Items.Count To 1 Step -1
				
				'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				If lvSearchList.Items.Item(i).Selected = True Then
					
					'UPGRADE_WARNING: Lower bound of collection lvSearchList.ListItems has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					itemID = Mid(lvSearchList.Items.Item(i).Name, 4)
					rs1String = "SELECT Status From Faults Where ID = " & SQLize(itemID)
					rs2String = ""
					
					printItOut(rs1String, rs2String, "")
					
				End If
			Next i
			
		ElseIf frameSelected = "Text" Then 
			
			tempstring = txFaultFileStatus.Text
			'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
			'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
			Printer.Print(tempstring)
			'UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
			Printer.EndDoc()
			
		End If
OuttaHere: 
		
	End Sub
	
	'UPGRADE_NOTE: second was upgraded to second_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub printItOut(ByVal first As String, ByVal second_Renamed As String, ByVal endOfString As String)
		Dim rs As ADODB.Recordset
		Dim apsrs As ADODB.Recordset
		Dim tempstring As String
		
		OpenDB("FaultFile")
		
		apsrs = rsOpen(first, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		
		If apsrs.BOF = False Or apsrs.EOF = False Then
			
			apsrs.MoveFirst()
			
			If second_Renamed = "" Then
				
				tempstring = GetDBString(apsrs, "Status")
				'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
				'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
				Printer.Print(tempstring)
				'UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
				Printer.EndDoc()
				
			Else
				
				second_Renamed = second_Renamed & SQLize(apsrs.Fields("APSName").Value)
				
				Select Case endOfString
					Case "First"
						second_Renamed = second_Renamed & "'"
					Case "Second"
						second_Renamed = second_Renamed & "' AND TPSName = '" & SQLize(apsrs.Fields("TPSName").Value) & "'"
					Case "Third"
						second_Renamed = second_Renamed & "' AND TPSName = '" & SQLize(apsrs.Fields("TPSName").Value) & "' AND SerialNumber = '" & SQLize(apsrs.Fields("SerialNumber").Value) & "'"
				End Select
				
				rs = rsOpen(second_Renamed, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
				
				If rs.BOF = False Or rs.EOF = False Then
					
					rs.MoveFirst()
					Do While Not rs.EOF
						
						tempstring = GetDBString(rs, "Status")
						'UPGRADE_ISSUE: Printer object was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6B85A2A7-FE9F-4FBE-AA0C-CF11AC86A305"'
						'UPGRADE_ISSUE: Printer method Printer.Print was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
						Printer.Print(tempstring)
						'UPGRADE_ISSUE: Printer method Printer.EndDoc was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="076C26E5-B7A9-4E77-B69C-B4448DF39E58"'
						Printer.EndDoc()
						
						If Not rs.EOF Then
							rs.MoveNext()
						End If
					Loop 
				End If
				
				rs.Close()
				'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				rs = Nothing
				
			End If
		End If
		
		apsrs.Close()
		'UPGRADE_NOTE: Object apsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		apsrs = Nothing
		CloseDB("FaultFile")
		
	End Sub
	
	'UPGRADE_NOTE: second was upgraded to second_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub sendToMedia(ByVal first As String, ByVal second_Renamed As String, ByVal stringToChoose As String)
		Dim file As Object
		Dim RetValue As Object
		Dim fileName As String
		Dim tempstring As String
		Dim rs As ADODB.Recordset
		Dim apsrs As ADODB.Recordset
		'UPGRADE_NOTE: Leave was upgraded to Leave_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim Leave_Renamed As Boolean
		Dim leaveLoop As Boolean
		
		Leave_Renamed = False
		
		OpenDB("FaultFile")
		
		apsrs = rsOpen(first, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		
		If apsrs.BOF = False Or apsrs.EOF = False Then
			
			apsrs.MoveFirst()
			
			If second_Renamed = "" Then
				
				fileName = CStr(apsrs.Fields("APSName").Value) & " " & CStr(apsrs.Fields("TPSName").Value) & " " & CStr(apsrs.Fields("SerialNumber").Value) & " " & CStr(apsrs.Fields("RunDate").Value) & ".txt"
				
				'UPGRADE_WARNING: Couldn't resolve default property of object file. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				file = DIRNAME & "\" & deColonIt(fileName)
				
				Do 
					
					If checkRemovableMedia = True Then
						
						Leave_Renamed = True
						
						'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
						If Dir(DIRNAME, FileAttribute.Directory) = "" Then
							MkDir(DIRNAME)
						End If
						
						'UPGRADE_WARNING: Couldn't resolve default property of object file. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						FileOpen(1, file, OpenMode.Output)
						tempstring = GetDBString(apsrs, "Status")
						PrintLine(1, tempstring)
						FileClose(1)
						
					Else
						
						'UPGRADE_WARNING: Couldn't resolve default property of object RetValue. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						RetValue = MsgBox("Please Insert Floppy into Drive", MsgBoxStyle.OKCancel)
						
						If RetValue = MsgBoxResult.Cancel Then
							Leave_Renamed = True
						End If
						
					End If
					
				Loop Until Leave_Renamed = True
				
			Else
				
				second_Renamed = second_Renamed & SQLize(apsrs.Fields("APSName").Value)
				
				Select Case stringToChoose
					Case "First"
						second_Renamed = second_Renamed & "'"
					Case "Second"
						second_Renamed = second_Renamed & "' AND TPSName = '" & SQLize(apsrs.Fields("TPSName").Value) & "'"
					Case "Third"
						second_Renamed = second_Renamed & "' AND TPSName = '" & SQLize(apsrs.Fields("TPSName").Value) & "' AND SerialNumber = '" & SQLize(apsrs.Fields("SerialNumber").Value) & "'"
				End Select
				
				rs = rsOpen(second_Renamed, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
				
				If rs.BOF = False Or rs.EOF = False Then
					
					rs.MoveFirst()
					Do While Not rs.EOF
						
						Select Case stringToChoose
							Case "First"
								fileName = CStr(apsrs.Fields("APSName").Value) & " " & CStr(rs.Fields("TPSName").Value) & " " & CStr(rs.Fields("SerialNumber").Value) & " " & CStr(rs.Fields("RunDate").Value) & ".txt"
							Case "Second"
								fileName = CStr(apsrs.Fields("APSName").Value) & " " & CStr(apsrs.Fields("TPSName").Value) & " " & CStr(rs.Fields("SerialNumber").Value) & " " & CStr(rs.Fields("RunDate").Value) & ".txt"
							Case "Third"
								fileName = CStr(apsrs.Fields("APSName").Value) & " " & CStr(apsrs.Fields("TPSName").Value) & " " & CStr(apsrs.Fields("SerialNumber").Value) & " " & CStr(rs.Fields("RunDate").Value) & ".txt"
						End Select
						
						'UPGRADE_WARNING: Couldn't resolve default property of object file. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
						file = DIRNAME & "\" & deColonIt(fileName)
						
						Do 
							
							If checkRemovableMedia = True Then
								
								Leave_Renamed = True
								
								'UPGRADE_WARNING: Dir has a new behavior. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
								If Dir(DIRNAME, FileAttribute.Directory) = "" Then
									MkDir(DIRNAME)
								End If
								
								'UPGRADE_WARNING: Couldn't resolve default property of object file. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								FileOpen(1, file, OpenMode.Output)
								tempstring = GetDBString(rs, "Status")
								PrintLine(1, tempstring)
								FileClose(1)
								
							Else
								
								'UPGRADE_WARNING: Couldn't resolve default property of object RetValue. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
								RetValue = MsgBox("Please Insert Floppy into Drive", MsgBoxStyle.OKCancel)
								
								If RetValue = MsgBoxResult.Cancel Then
									Leave_Renamed = True
									leaveLoop = True
								End If
								
							End If
							
						Loop Until Leave_Renamed = True
						
						If leaveLoop = True Then
							Exit Do
						End If
						
						If Not rs.EOF Then
							rs.MoveNext()
						End If
					Loop 
				End If
				
				rs.Close()
				'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				rs = Nothing
				
			End If
		End If
		
		apsrs.Close()
		'UPGRADE_NOTE: Object apsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		apsrs = Nothing
		
		CloseDB("FaultFile")
		
	End Sub
	
	'UPGRADE_NOTE: format was upgraded to format_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	'UPGRADE_NOTE: second was upgraded to second_Renamed. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub deleteTheRecords(ByVal first As String, ByVal second_Renamed As String, ByVal format_Renamed As String)
		Dim RetValue As Object
		Dim file As Object
		Dim i As Short
		Dim rs As ADODB.Recordset
		Dim apsrs As ADODB.Recordset
		Dim itemID As String
		Dim deleteString As String
		Dim tempstring As String
		Dim firstTime As Boolean
		
		firstTime = True
		
		OpenDB("FaultFile")
		
		If format_Renamed = "" Then
			
			tempstring = first
			
		Else
			
			apsrs = rsOpen(first, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
			
			If apsrs.BOF = False Or apsrs.EOF = False Then
				
				apsrs.MoveFirst()
				
				second_Renamed = second_Renamed & SQLize(apsrs.Fields("APSName").Value)
				
				Select Case format_Renamed
					Case "First"
						second_Renamed = second_Renamed & "'"
					Case "Second"
						second_Renamed = second_Renamed & "' AND TPSName = '" & SQLize(apsrs.Fields("TPSName").Value) & "'"
					Case "Third"
						second_Renamed = second_Renamed & "' AND TPSName = '" & SQLize(apsrs.Fields("TPSName").Value) & "' AND SerialNumber = '" & SQLize(apsrs.Fields("SerialNumber").Value) & "'"
				End Select
				
				rs = rsOpen(second_Renamed, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
				
				If rs.BOF = False Or rs.EOF = False Then
					
					rs.MoveFirst()
					Do While Not rs.EOF
						
						If firstTime = False Then
							tempstring = tempstring & ","
						End If
						
						tempstring = tempstring & GetDBString(rs, "ID")
						firstTime = False
						
						If Not rs.EOF Then
							rs.MoveNext()
						End If
					Loop 
				End If
				
				rs.Close()
				'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				rs = Nothing
			End If
			
			apsrs.Close()
			'UPGRADE_NOTE: Object apsrs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			apsrs = Nothing
			
		End If
		
		If GPNamMode = False Then
			'UPGRADE_WARNING: Couldn't resolve default property of object RetValue. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			RetValue = MsgBox("Delete the Selected Records?", MsgBoxStyle.OKCancel)
			If RetValue = MsgBoxResult.OK Then
				
				deleteString = "DELETE FROM Faults WHERE ID IN (" & tempstring & ")"
				DeleteFiles(deleteString)
				
			End If
		Else
			
			deleteString = "DELETE FROM Faults WHERE ID IN (" & tempstring & ")"
			DeleteFiles(deleteString)
			
		End If
		
		CloseDB("FaultFile")
		
	End Sub
	
	Private Sub getLastID()
		Dim rs As ADODB.Recordset
		
		OpenDB("FaultFile")
		
		rs = rsOpen("SELECT ID FROM Faults ", ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockReadOnly)
		If rs.BOF = False Or rs.EOF = False Then
			rs.MoveLast()
			IDToDelete = CStr(rs.Fields("ID").Value)
		End If
		rs.Close()
		'UPGRADE_NOTE: Object rs may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		rs = Nothing
		
		CloseDB("FaultFile")
		
	End Sub
	
	Private Sub expandTreeList()
		Dim tNode As System.Windows.Forms.TreeNode
		Dim i As Short
		
		lastID = "R_G" & IDToDelete
		
		For i = tvTrack.Nodes.Count To 1 Step -1
			
			'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
			If tvTrack.Nodes.Item(i).Tag = lastID Then
				
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				doNodeClick(tvTrack.Nodes.Item(i))
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				savedNode = tvTrack.Nodes.Item(i)
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				'UPGRADE_ISSUE: MSComctlLib.Node property tvTrack.Nodes.Selected was not upgraded. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="CC4C7EC0-C903-48FC-ACCC-81861D12DA4A"'
				tvTrack.Nodes.Item(i).Selected = True
				On Error GoTo OuttaHere
				'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
				tNode = tvTrack.Nodes.Item(i).Parent
				
				Do 
					'UPGRADE_WARNING: Lower bound of collection tvTrack.Nodes has changed from 1 to 0. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A3B628A0-A810-4AE2-BFA2-9E7A29EB9AD0"'
					tvTrack.Nodes.Item(i).Expand()
					tNode = tNode.Parent
					
				Loop Until tNode.Index <= 1
				
				'UPGRADE_NOTE: Object tNode may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
				tNode = Nothing
				
				Exit For
			End If
		Next i
		
OuttaHere: 
		
	End Sub
	
	Private Function checkRemovableMedia() As Boolean
		
		On Error GoTo errhandler
		
		ChDir("A:\")
		
		checkRemovableMedia = True
		
		Exit Function
		
errhandler: 
		
		If Err.Number = 75 Then
			checkRemovableMedia = False
		End If
		
	End Function
End Class