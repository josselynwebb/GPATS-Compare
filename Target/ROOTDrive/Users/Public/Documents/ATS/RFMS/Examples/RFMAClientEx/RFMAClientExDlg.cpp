// RFMAClientExDlg.cpp : implementation file
//

#include "stdafx.h"
#include "RFMAClientEx.h"
#include "RFMAClientExDlg.h"
#include "DlgProxy.h"
#include <comdef.h>	// required for the BSTR manipulations/conversions
#include <string>

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CRFMAClientExDlg dialog

IMPLEMENT_DYNAMIC(CRFMAClientExDlg, CDialog);

CRFMAClientExDlg::CRFMAClientExDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CRFMAClientExDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CRFMAClientExDlg)
		// NOTE: the ClassWizard will add member initialization here
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_pAutoProxy = NULL;
}

CRFMAClientExDlg::~CRFMAClientExDlg()
{
	// If there is an automation proxy for this dialog, set
	//  its back pointer to this dialog to NULL, so it knows
	//  the dialog has been deleted.
	if (m_pAutoProxy != NULL)
		m_pAutoProxy->m_pDialog = NULL;
}

void CRFMAClientExDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CRFMAClientExDlg)
		// NOTE: the ClassWizard will add DDX and DDV calls here
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CRFMAClientExDlg, CDialog)
	//{{AFX_MSG_MAP(CRFMAClientExDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CLOSE()
	ON_BN_CLICKED(IDC_BTN_MEASURE, OnBtnMeasure)
	ON_BN_CLICKED(IDC_RDO_SIM, OnRdoSim)
	ON_BN_CLICKED(IDC_RDO_EXECUTION, OnRdoExecution)
	ON_BN_CLICKED(IDC_BTN_CLR_ERRORS, OnBtnClrErrors)
	ON_CBN_EDITCHANGE(IDC_CMB_MEASTYPE, OnEditchangeCmbMeastype)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CRFMAClientExDlg message handlers

BOOL CRFMAClientExDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	
	// connect to the server
	RFMaObj.CreateDispatch(_T("RFMS.RFMa_if"));
	
	// open a session in execution mode--if there are hardware errors
	//  simulation mode will be entered instead.
	enterExecutionMode();

	// load some defaults
	SetDlgItemText(IDC_CMB_MEAS_SIG_TYPE, "AC Signal");
	SetDlgItemText(IDC_CMB_MEASTYPE, "FREQ");
	SetDlgItemText(IDC_CMB_MEAS_UNITS, "dBm");

	// get the current instrument settings and display them
	getInstrSettings();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CRFMAClientExDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CRFMAClientExDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

// Automation servers should not exit when a user closes the UI
//  if a controller still holds on to one of its objects.  These
//  message handlers make sure that if the proxy is still in use,
//  then the UI is hidden but the dialog remains around if it
//  is dismissed.

void CRFMAClientExDlg::OnClose() 
{
	if (CanExit())
		CDialog::OnClose();
}

void CRFMAClientExDlg::OnOK() 
{
	if (CanExit())
		CDialog::OnOK();
}

void CRFMAClientExDlg::OnCancel() 
{
	if (CanExit())
		CDialog::OnCancel();
}

BOOL CRFMAClientExDlg::CanExit()
{
	// If the proxy object is still around, then the automation
	//  controller is still holding on to this application.  Leave
	//  the dialog around, but hide its UI.
	if (m_pAutoProxy != NULL)
	{
		ShowWindow(SW_HIDE);
		return FALSE;
	}

	return TRUE;
}

//-----------------------------------------------------------------------------
// OnBtnMeasure()
//  This method takes a measurment. It reads the contents of the combo boxes to
//   determine which measurement is being requested.
//-----------------------------------------------------------------------------
void CRFMAClientExDlg::OnBtnMeasure() 
{
	int retVal;
	CString measSigType;
	CString measMode;
	CString measUnits;
	char measResultString[15];
	VARIANT measResult;
	BSTR bstr_MeasUnits;	// required to recover the measured units from the server

	// Set Instrument settings
	setInstrSettings();
	
	// All Variant data types require this VariantInit call.
	VariantInit(&measResult);
	
	// Make the Variant a double type. VT_R8 indicates double.
	measResult.vt = VT_R8;
	
	// Get the requested measured signal types and measure mode types.
	GetDlgItemText(IDC_CMB_MEAS_SIG_TYPE, measSigType);
	GetDlgItemText(IDC_CMB_MEASTYPE, measMode);
	GetDlgItemText(IDC_CMB_MEAS_UNITS, measUnits);
	
	// Tell the server which units of measure we want to use
	retVal = RFMaObj.setMeasureUnits(measUnits);
	if (retVal == -1)
		checkError();

	// allocated enough memory for the tmpString to hold the measured units.
	bstr_MeasUnits = _com_util::ConvertStringToBSTR("abc");
	
	if (measSigType == "AC Signal"){
		retVal = RFMaObj.setMeasSignalType(0);
		if (retVal == -1)
			checkError();
	}
	
	if (measMode == "PEAK_POWER"){
		retVal = RFMaObj.setMeasureMode(10);
		retVal = RFMaObj.getMeasurement(&measResult,&bstr_MeasUnits);
		if (retVal == -1)
			checkError();
	}
	
	if (measMode == "FREQ"){
		retVal = RFMaObj.setMeasureMode(1);
		retVal = RFMaObj.getMeasurement(&measResult,&bstr_MeasUnits);
		if (retVal == -1)
			checkError();
	}

	measUnits = _com_util::ConvertBSTRToString(bstr_MeasUnits);
	sprintf(measResultString,"%1.7g",measResult.dblVal);

	// In the case of WAVE_FORM_CAPTURE or any measurement mode call that
	//  returns an array of data; a SafeArray will be returned in measResult.
	if (measMode == "WAVE_FORM_CAPTURE"){
		
		long NumOfFFTSamples;
		long lDimension[1];
		
		retVal = RFMaObj.setMeasureMode(0);
		retVal = RFMaObj.getMeasurement(&measResult, &bstr_MeasUnits);
		retVal = RFMaObj.getFFTSmplLen(&NumOfFFTSamples);
		if (retVal == -1)
			checkError();
		
		double *FFTArray = new double [NumOfFFTSamples];
				
		// Copy from the SafeArray to anywhere you want the data
		for (int i=0; i<NumOfFFTSamples; i++)
		{
			lDimension[0]=i;
			SafeArrayGetElement(measResult.parray,lDimension, &FFTArray[i]);
		}
		delete [] FFTArray;
		measUnits = "dBm";
		sprintf(measResultString,"N/A");
	}
		
	SetDlgItemText(IDC_EDIT_UNITS,measUnits);
	SetDlgItemText(IDC_EDIT_MEAS_RES,measResultString);
	
	// VariantClear must be called to release the resources. If the variant type
	//  is an array, the array will cleaned up.
	VariantClear(&measResult);
}

//-----------------------------------------------------------------------------
// Open RFCt session in Execution mode
//-----------------------------------------------------------------------------
void CRFMAClientExDlg::enterExecutionMode(){
    
	int retVal;
	// Radio Buttons are a member of CButton
	CButton* pRadioButton;
	
	// Close the session--RFMS ignores this if a session is not open
    RFMaObj.close();
    retVal = RFMaObj.open(0);
	if (retVal == -1){
		checkError();
        //enterSimulationMode();
		pRadioButton = (CButton*) GetDlgItem(IDC_RDO_SIM);  // gets handle to the RadioBox control
		pRadioButton->SetCheck(1);
    }else{
        SetWindowText("RFMS RF Counter SFP: EXECUTION MODE");
		pRadioButton = (CButton*) GetDlgItem(IDC_RDO_EXECUTION);  // gets handle to the RadioBox control
		pRadioButton->SetCheck(1);
	}
}

//-----------------------------------------------------------------------------
// Open RFCt session in Simulation mode
//-----------------------------------------------------------------------------
void CRFMAClientExDlg::enterSimulationMode(){
    
	int retVal;
	// Radio Buttons are a member of CButton
	CButton* pRadioButton;
	
	// Close the session--RFMS ignores this if a session is not open
    RFMaObj.close();
    retVal = RFMaObj.open(1);
	if (retVal == -1){
		checkError();
	}else{
        SetWindowText("RFMS RF Counter SFP: SIMULATION MODE");
		pRadioButton = (CButton*) GetDlgItem(IDC_RDO_SIM);  // gets handle to the RadioBox control
		pRadioButton->SetCheck(1);
	}
}

//-----------------------------------------------------------------------------
void CRFMAClientExDlg::OnRdoSim(){
	enterSimulationMode();
}

//-----------------------------------------------------------------------------
void CRFMAClientExDlg::OnRdoExecution(){
	enterExecutionMode();
}

//-----------------------------------------------------------------------------
// setInstrSettings()
//  This method reads the user entered instrument settings from the dialog and
//   tells the server to set them.
//-----------------------------------------------------------------------------
void CRFMAClientExDlg::setInstrSettings(){

	int retVal;
	CString SettingsStr;
	
	GetDlgItemText(IDC_EDIT_CENTER_FREQ, SettingsStr);
	retVal = RFMaObj.setCenterFreq(atof(SettingsStr), "MHz");
		
	GetDlgItemText(IDC_EDIT_SPAN, SettingsStr);
	retVal = RFMaObj.setSpan(atof(SettingsStr), "MHz");
	
	GetDlgItemText(IDC_EDIT_ATTN, SettingsStr);
	retVal = RFMaObj.setAttenuator(atol(SettingsStr));

	if (retVal == -1)
		checkError();
}

//-----------------------------------------------------------------------------
// getInstrSettings()
//  This method gets some of the current instrument settings from the server
//   and displays them on the dialog.
//-----------------------------------------------------------------------------
void CRFMAClientExDlg::getInstrSettings(){

	int retVal;
	CString SettingsStr;
	double tmpd;
	long tmpl;
	BSTR bstr_MeasUnits;	// required to recover the measured units from the server
	
	// allocated enough memory for the tmpString to hold the measured units.
	bstr_MeasUnits = _com_util::ConvertStringToBSTR("abc");

	// Note: the bstr_MeasUnits is being ignored here to keep this example simple.
	//  Normally these returned units should not be passed back to the user.
	retVal = RFMaObj.getCenterFreq(&tmpd, &bstr_MeasUnits);
	SettingsStr.Format("%g",tmpd);
	SetDlgItemText(IDC_EDIT_CENTER_FREQ, SettingsStr);
		
	retVal = RFMaObj.getSpan(&tmpd, &bstr_MeasUnits);
	SettingsStr.Format("%g",tmpd);
	SetDlgItemText(IDC_EDIT_SPAN, SettingsStr);
	
	retVal = RFMaObj.getAttenuator(&tmpl);
	SettingsStr.Format("%d",tmpl);
	SetDlgItemText(IDC_EDIT_ATTN, SettingsStr);
	if (retVal == -1)
		checkError();
}

//-----------------------------------------------------------------------------
// checkError()
//  This method is used to recover error information from the RFMS server and 
//  display it on the dialog box.
//-----------------------------------------------------------------------------
void CRFMAClientExDlg::checkError() 
{
	long ErrorCode, ErrorSeverity;
	std::string tmpString;
	char tmps[32];
	char *ErrorDescr;
	char *MoreErrorInfo;
	BSTR bstrErrDescr;						// required to recover character strings from RFMS service
	BSTR bstrMoreInfo;						
	
	bstrErrDescr = SysAllocStringLen(NULL,0); 
	bstrMoreInfo = SysAllocStringLen(NULL,0); 
	
	// this call returns all of the error information
	RFMaObj.getError(&ErrorCode, &ErrorSeverity, &bstrErrDescr, 10, &bstrMoreInfo, 10);
	
	// these two lines convert the returned BSTR strings into standard ANSI strings.
	ErrorDescr = _com_util::ConvertBSTRToString(bstrErrDescr);
	MoreErrorInfo = _com_util::ConvertBSTRToString(bstrMoreInfo);
	
	// these two lines display the error information in the dialog box
	SetDlgItemText(IDC_STATIC_ERROR_MESSAGE,ErrorDescr);
	SetDlgItemText(IDC_STATIC_ERROR_INFO,MoreErrorInfo);
	sprintf(tmps,"%d",ErrorCode);
	SetDlgItemText(IDC_EDIT_ERROR_CODE,tmps);
	sprintf(tmps,"%d",ErrorSeverity);
	SetDlgItemText(IDC_EDIT_ERROR_SEVERITY,tmps);
	
	delete [] ErrorDescr;
	delete [] MoreErrorInfo;
	SysFreeString(bstrErrDescr); 
	SysFreeString(bstrMoreInfo); 
	
}

void CRFMAClientExDlg::OnBtnClrErrors() 
{
	// these two lines clear the error messages
	SetDlgItemText(IDC_STATIC_ERROR_MESSAGE,"");
	SetDlgItemText(IDC_STATIC_ERROR_INFO,"");	
}

void CRFMAClientExDlg::OnEditchangeCmbMeastype() 
{
	// TODO: Add your control notification handler code here
	
}
