'UPGRADE_WARNING: The entire project must be compiled once before a form with an ActiveX Control Array can be displayed

Imports System.ComponentModel

<ProvideProperty("Index",GetType(AxThreed.AxSSFrame))> Public Class AxSSFrameArray
	Inherits Microsoft.VisualBasic.Compatibility.VB6.BaseOcxArray
	Implements IExtenderProvider

	Public Sub New()
		MyBase.New()
	End Sub

	Public Sub New(ByVal Container As IContainer)
		MyBase.New(Container)
	End Sub

	Public Shadows Event [ClickEvent] (ByVal sender As System.Object, ByVal e As System.EventArgs)
	Public Shadows Event [DblClick] (ByVal sender As System.Object, ByVal e As System.EventArgs)
	Public Shadows Event [MouseDownEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSFRCtrlEvents_MouseDownEvent)
	Public Shadows Event [MouseMoveEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSFRCtrlEvents_MouseMoveEvent)
	Public Shadows Event [MouseUpEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSFRCtrlEvents_MouseUpEvent)

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Function CanExtend(ByVal target As Object) As Boolean Implements IExtenderProvider.CanExtend
		If TypeOf target Is AxThreed.AxSSFrame Then
			Return BaseCanExtend(target)
		End If
	End Function

	Public Function GetIndex(ByVal o As AxThreed.AxSSFrame) As Short
		Return BaseGetIndex(o)
	End Function

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Sub SetIndex(ByVal o As AxThreed.AxSSFrame, ByVal Index As Short)
		BaseSetIndex(o, Index)
	End Sub

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Function ShouldSerializeIndex(ByVal o As AxThreed.AxSSFrame) As Boolean
		Return BaseShouldSerializeIndex(o)
	End Function

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Sub ResetIndex(ByVal o As AxThreed.AxSSFrame)
		BaseResetIndex(o)
	End Sub

	Default Public ReadOnly Property Item(ByVal Index As Short) As AxThreed.AxSSFrame
		Get
			Item = CType(BaseGetItem(Index), AxThreed.AxSSFrame)
		End Get
	End Property

	Protected Overrides Function GetControlInstanceType() As System.Type
		Return GetType(AxThreed.AxSSFrame)
	End Function

	Protected Overrides Sub HookUpControlEvents(ByVal o As Object)
		Dim ctl As AxThreed.AxSSFrame = CType(o, AxThreed.AxSSFrame)
		MyBase.HookUpControlEvents(o)
		If Not ClickEventEvent Is Nothing Then
			AddHandler ctl.ClickEvent, New System.EventHandler(AddressOf HandleClickEvent)
		End If
		If Not DblClickEvent Is Nothing Then
			AddHandler ctl.DblClick, New System.EventHandler(AddressOf HandleDblClick)
		End If
		If Not MouseDownEventEvent Is Nothing Then
			AddHandler ctl.MouseDownEvent, New AxThreed.ISSFRCtrlEvents_MouseDownEventHandler(AddressOf HandleMouseDownEvent)
		End If
		If Not MouseMoveEventEvent Is Nothing Then
			AddHandler ctl.MouseMoveEvent, New AxThreed.ISSFRCtrlEvents_MouseMoveEventHandler(AddressOf HandleMouseMoveEvent)
		End If
		If Not MouseUpEventEvent Is Nothing Then
			AddHandler ctl.MouseUpEvent, New AxThreed.ISSFRCtrlEvents_MouseUpEventHandler(AddressOf HandleMouseUpEvent)
		End If
	End Sub

	Private Sub HandleClickEvent (ByVal sender As System.Object, ByVal e As System.EventArgs) 
		RaiseEvent [ClickEvent] (sender, e)
	End Sub

	Private Sub HandleDblClick (ByVal sender As System.Object, ByVal e As System.EventArgs) 
		RaiseEvent [DblClick] (sender, e)
	End Sub

	Private Sub HandleMouseDownEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSFRCtrlEvents_MouseDownEvent) 
		RaiseEvent [MouseDownEvent] (sender, e)
	End Sub

	Private Sub HandleMouseMoveEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSFRCtrlEvents_MouseMoveEvent) 
		RaiseEvent [MouseMoveEvent] (sender, e)
	End Sub

	Private Sub HandleMouseUpEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSFRCtrlEvents_MouseUpEvent) 
		RaiseEvent [MouseUpEvent] (sender, e)
	End Sub

End Class

