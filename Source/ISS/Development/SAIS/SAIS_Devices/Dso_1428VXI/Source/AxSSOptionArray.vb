'UPGRADE_WARNING: The entire project must be compiled once before a form with an ActiveX Control Array can be displayed

Imports System.ComponentModel

<ProvideProperty("Index",GetType(AxThreed.AxSSOption))> Public Class AxSSOptionArray
	Inherits Microsoft.VisualBasic.Compatibility.VB6.BaseOcxArray
	Implements IExtenderProvider

	Public Sub New()
		MyBase.New()
	End Sub

	Public Sub New(ByVal Container As IContainer)
		MyBase.New(Container)
	End Sub

	Public Shadows Event [ClickEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_ClickEvent)
	Public Shadows Event [DblClick] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_DblClickEvent)
	Public Shadows Event [KeyDownEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_KeyDownEvent)
	Public Shadows Event [KeyPressEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_KeyPressEvent)
	Public Shadows Event [KeyUpEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_KeyUpEvent)
	Public Shadows Event [MouseDownEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_MouseDownEvent)
	Public Shadows Event [MouseMoveEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_MouseMoveEvent)
	Public Shadows Event [MouseUpEvent] (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_MouseUpEvent)

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Function CanExtend(ByVal target As Object) As Boolean Implements IExtenderProvider.CanExtend
		If TypeOf target Is AxThreed.AxSSOption Then
			Return BaseCanExtend(target)
		End If
	End Function

	Public Function GetIndex(ByVal o As AxThreed.AxSSOption) As Short
		Return BaseGetIndex(o)
	End Function

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Sub SetIndex(ByVal o As AxThreed.AxSSOption, ByVal Index As Short)
		BaseSetIndex(o, Index)
	End Sub

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Function ShouldSerializeIndex(ByVal o As AxThreed.AxSSOption) As Boolean
		Return BaseShouldSerializeIndex(o)
	End Function

	<System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)> Public Sub ResetIndex(ByVal o As AxThreed.AxSSOption)
		BaseResetIndex(o)
	End Sub

	Default Public ReadOnly Property Item(ByVal Index As Short) As AxThreed.AxSSOption
		Get
			Item = CType(BaseGetItem(Index), AxThreed.AxSSOption)
		End Get
	End Property

	Protected Overrides Function GetControlInstanceType() As System.Type
		Return GetType(AxThreed.AxSSOption)
	End Function

	Protected Overrides Sub HookUpControlEvents(ByVal o As Object)
		Dim ctl As AxThreed.AxSSOption = CType(o, AxThreed.AxSSOption)
		MyBase.HookUpControlEvents(o)
		If Not ClickEventEvent Is Nothing Then
			AddHandler ctl.ClickEvent, New AxThreed.ISSRBCtrlEvents_ClickEventHandler(AddressOf HandleClickEvent)
		End If
		If Not DblClickEvent Is Nothing Then
			AddHandler ctl.DblClick, New AxThreed.ISSRBCtrlEvents_DblClickEventHandler(AddressOf HandleDblClick)
		End If
		If Not KeyDownEventEvent Is Nothing Then
			AddHandler ctl.KeyDownEvent, New AxThreed.ISSRBCtrlEvents_KeyDownEventHandler(AddressOf HandleKeyDownEvent)
		End If
		If Not KeyPressEventEvent Is Nothing Then
			AddHandler ctl.KeyPressEvent, New AxThreed.ISSRBCtrlEvents_KeyPressEventHandler(AddressOf HandleKeyPressEvent)
		End If
		If Not KeyUpEventEvent Is Nothing Then
			AddHandler ctl.KeyUpEvent, New AxThreed.ISSRBCtrlEvents_KeyUpEventHandler(AddressOf HandleKeyUpEvent)
		End If
		If Not MouseDownEventEvent Is Nothing Then
			AddHandler ctl.MouseDownEvent, New AxThreed.ISSRBCtrlEvents_MouseDownEventHandler(AddressOf HandleMouseDownEvent)
		End If
		If Not MouseMoveEventEvent Is Nothing Then
			AddHandler ctl.MouseMoveEvent, New AxThreed.ISSRBCtrlEvents_MouseMoveEventHandler(AddressOf HandleMouseMoveEvent)
		End If
		If Not MouseUpEventEvent Is Nothing Then
			AddHandler ctl.MouseUpEvent, New AxThreed.ISSRBCtrlEvents_MouseUpEventHandler(AddressOf HandleMouseUpEvent)
		End If
	End Sub

	Private Sub HandleClickEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_ClickEvent) 
		RaiseEvent [ClickEvent] (sender, e)
	End Sub

	Private Sub HandleDblClick (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_DblClickEvent) 
		RaiseEvent [DblClick] (sender, e)
	End Sub

	Private Sub HandleKeyDownEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_KeyDownEvent) 
		RaiseEvent [KeyDownEvent] (sender, e)
	End Sub

	Private Sub HandleKeyPressEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_KeyPressEvent) 
		RaiseEvent [KeyPressEvent] (sender, e)
	End Sub

	Private Sub HandleKeyUpEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_KeyUpEvent) 
		RaiseEvent [KeyUpEvent] (sender, e)
	End Sub

	Private Sub HandleMouseDownEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_MouseDownEvent) 
		RaiseEvent [MouseDownEvent] (sender, e)
	End Sub

	Private Sub HandleMouseMoveEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_MouseMoveEvent) 
		RaiseEvent [MouseMoveEvent] (sender, e)
	End Sub

	Private Sub HandleMouseUpEvent (ByVal sender As System.Object, ByVal e As AxThreed.ISSRBCtrlEvents_MouseUpEvent) 
		RaiseEvent [MouseUpEvent] (sender, e)
	End Sub

End Class

