// DlgProxy.cpp : implementation file
//

#include "stdafx.h"
#include "RFMAClientEx.h"
#include "DlgProxy.h"
#include "RFMAClientExDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CRFMAClientExDlgAutoProxy

IMPLEMENT_DYNCREATE(CRFMAClientExDlgAutoProxy, CCmdTarget)

CRFMAClientExDlgAutoProxy::CRFMAClientExDlgAutoProxy()
{
	EnableAutomation();
	
	// To keep the application running as long as an automation 
	//	object is active, the constructor calls AfxOleLockApp.
	AfxOleLockApp();

	// Get access to the dialog through the application's
	//  main window pointer.  Set the proxy's internal pointer
	//  to point to the dialog, and set the dialog's back pointer to
	//  this proxy.
	ASSERT (AfxGetApp()->m_pMainWnd != NULL);
	ASSERT_VALID (AfxGetApp()->m_pMainWnd);
	ASSERT_KINDOF(CRFMAClientExDlg, AfxGetApp()->m_pMainWnd);
	m_pDialog = (CRFMAClientExDlg*) AfxGetApp()->m_pMainWnd;
	m_pDialog->m_pAutoProxy = this;
}

CRFMAClientExDlgAutoProxy::~CRFMAClientExDlgAutoProxy()
{
	// To terminate the application when all objects created with
	// 	with automation, the destructor calls AfxOleUnlockApp.
	//  Among other things, this will destroy the main dialog
	if (m_pDialog != NULL)
		m_pDialog->m_pAutoProxy = NULL;
	AfxOleUnlockApp();
}

void CRFMAClientExDlgAutoProxy::OnFinalRelease()
{
	// When the last reference for an automation object is released
	// OnFinalRelease is called.  The base class will automatically
	// deletes the object.  Add additional cleanup required for your
	// object before calling the base class.

	CCmdTarget::OnFinalRelease();
}

BEGIN_MESSAGE_MAP(CRFMAClientExDlgAutoProxy, CCmdTarget)
	//{{AFX_MSG_MAP(CRFMAClientExDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

BEGIN_DISPATCH_MAP(CRFMAClientExDlgAutoProxy, CCmdTarget)
	//{{AFX_DISPATCH_MAP(CRFMAClientExDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove mapping macros here.
	//}}AFX_DISPATCH_MAP
END_DISPATCH_MAP()

// Note: we add support for IID_IRFMAClientEx to support typesafe binding
//  from VBA.  This IID must match the GUID that is attached to the 
//  dispinterface in the .ODL file.

// {6DCBF7A0-DC33-4385-B363-5E9ED91295CC}
static const IID IID_IRFMAClientEx =
{ 0x6dcbf7a0, 0xdc33, 0x4385, { 0xb3, 0x63, 0x5e, 0x9e, 0xd9, 0x12, 0x95, 0xcc } };

BEGIN_INTERFACE_MAP(CRFMAClientExDlgAutoProxy, CCmdTarget)
	INTERFACE_PART(CRFMAClientExDlgAutoProxy, IID_IRFMAClientEx, Dispatch)
END_INTERFACE_MAP()

// The IMPLEMENT_OLECREATE2 macro is defined in StdAfx.h of this project
// {DFFFD96E-C381-4877-B963-AE3363066DB3}
IMPLEMENT_OLECREATE2(CRFMAClientExDlgAutoProxy, "RFMAClientEx.Application", 0xdfffd96e, 0xc381, 0x4877, 0xb9, 0x63, 0xae, 0x33, 0x63, 0x6, 0x6d, 0xb3)

/////////////////////////////////////////////////////////////////////////////
// CRFMAClientExDlgAutoProxy message handlers
