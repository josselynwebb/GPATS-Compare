/////////////////////////////////////////////////////////////////////////////
//	File:	FaultFile.h														/
//																			/
//	Creation Date:	19 Aug 2004												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0	Assigned it a version number.								/
//		1.0.1.0	Changed the way comments are done							/
//																			/
/////////////////////////////////////////////////////////////////////////////

#ifndef __FAULTFILE_H_
#define __FAULTFILE_H_

class CFaultsAccessor
{

	public:

	    TCHAR		m_TPSName[27];
	    TCHAR		m_SerialNumber[25];
	    DBTIMESTAMP m_RunDate;
		TCHAR		m_APSName[27];
		TCHAR		m_PartNumber[14];
		TCHAR		m_ERONumber[6];
		TCHAR		m_Status[5001];


		BEGIN_COLUMN_MAP(CFaultsAccessor)
			COLUMN_ENTRY(2, m_TPSName)
			COLUMN_ENTRY(3, m_SerialNumber)
			COLUMN_ENTRY(4, m_RunDate)
			COLUMN_ENTRY(5, m_APSName)
			COLUMN_ENTRY(6, m_PartNumber)
			COLUMN_ENTRY(7, m_ERONumber)
			COLUMN_ENTRY(8, m_Status)
		END_COLUMN_MAP()

		void ClearRecord() {
			memset(this, 0, sizeof(*this));
		}

};

class CFaults : public CTable<CAccessor<CFaultsAccessor> >
{
	public:
		HRESULT Open() {

			HRESULT		hr;

			hr = OpenDataSource();

			if (FAILED(hr)) {
				return hr;
			}

			return OpenRowset();
		}

		HRESULT OpenDataSource() {

			HRESULT		hr;
			CDataSource db;
			CDBPropSet	dbinit(DBPROPSET_DBINIT);

			dbinit.AddProperty(DBPROP_INIT_DATASOURCE, OLESTR("FaultFile"));
			dbinit.AddProperty(DBPROP_INIT_MODE, (long)3);
			dbinit.AddProperty(DBPROP_INIT_PROMPT, (short)4);
			dbinit.AddProperty(DBPROP_INIT_LCID, (long)2057);

			hr = db.Open(_T("MSDASQL"), &dbinit);

			if (FAILED(hr)) {
				return hr;
			}

			return m_session.Open(db);
		}

		HRESULT OpenRowset() {
			CDBPropSet	propset(DBPROPSET_ROWSET);
			propset.AddProperty(DBPROP_IRowsetChange, true);
			propset.AddProperty(DBPROP_UPDATABILITY, DBPROPVAL_UP_CHANGE |
								DBPROPVAL_UP_INSERT | DBPROPVAL_UP_DELETE);

			return CTable<CAccessor<CFaultsAccessor> >::Open(m_session, _T("Faults"), &propset);
		}

		CSession	m_session;
};

#endif