// RFMAClientExDlg.h : header file
//

#if !defined(AFX_RFMACLIENTEXDLG_H__51D7D160_C1B7_474C_B816_298A67683679__INCLUDED_)
#define AFX_RFMACLIENTEXDLG_H__51D7D160_C1B7_474C_B816_298A67683679__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
#include "rfms.h"

class CRFMAClientExDlgAutoProxy;

/////////////////////////////////////////////////////////////////////////////
// CRFMAClientExDlg dialog

class CRFMAClientExDlg : public CDialog
{
	DECLARE_DYNAMIC(CRFMAClientExDlg);
	friend class CRFMAClientExDlgAutoProxy;

// Construction
public:
	CRFMAClientExDlg(CWnd* pParent = NULL);	// standard constructor
	virtual ~CRFMAClientExDlg();
	void checkError();
	void enterSimulationMode();
	void enterExecutionMode();
	void setInstrSettings();
	void getInstrSettings();
// Dialog Data
	//{{AFX_DATA(CRFMAClientExDlg)
	enum { IDD = IDD_RFMACLIENTEX_DIALOG };
		// NOTE: the ClassWizard will add data members here
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CRFMAClientExDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	CRFMAClientExDlgAutoProxy* m_pAutoProxy;
	HICON m_hIcon;

	BOOL CanExit();
	IRFMa_if RFMaObj;
	// Generated message map functions
	//{{AFX_MSG(CRFMAClientExDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnClose();
	virtual void OnOK();
	virtual void OnCancel();
	afx_msg void OnBtnMeasure();
	afx_msg void OnRdoSim();
	afx_msg void OnRdoExecution();
	afx_msg void OnBtnClrErrors();
	afx_msg void OnEditchangeCmbMeastype();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_RFMACLIENTEXDLG_H__51D7D160_C1B7_474C_B816_298A67683679__INCLUDED_)
