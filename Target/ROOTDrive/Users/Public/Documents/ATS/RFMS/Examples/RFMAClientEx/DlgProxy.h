// DlgProxy.h : header file
//

#if !defined(AFX_DLGPROXY_H__4BA7C3EB_EF4F_42F3_85C3_1899322DC923__INCLUDED_)
#define AFX_DLGPROXY_H__4BA7C3EB_EF4F_42F3_85C3_1899322DC923__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CRFMAClientExDlg;

/////////////////////////////////////////////////////////////////////////////
// CRFMAClientExDlgAutoProxy command target

class CRFMAClientExDlgAutoProxy : public CCmdTarget
{
	DECLARE_DYNCREATE(CRFMAClientExDlgAutoProxy)

	CRFMAClientExDlgAutoProxy();           // protected constructor used by dynamic creation

// Attributes
public:
	CRFMAClientExDlg* m_pDialog;

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CRFMAClientExDlgAutoProxy)
	public:
	virtual void OnFinalRelease();
	//}}AFX_VIRTUAL

// Implementation
protected:
	virtual ~CRFMAClientExDlgAutoProxy();

	// Generated message map functions
	//{{AFX_MSG(CRFMAClientExDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
	DECLARE_OLECREATE(CRFMAClientExDlgAutoProxy)

	// Generated OLE dispatch map functions
	//{{AFX_DISPATCH(CRFMAClientExDlgAutoProxy)
		// NOTE - the ClassWizard will add and remove member functions here.
	//}}AFX_DISPATCH
	DECLARE_DISPATCH_MAP()
	DECLARE_INTERFACE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_DLGPROXY_H__4BA7C3EB_EF4F_42F3_85C3_1899322DC923__INCLUDED_)
