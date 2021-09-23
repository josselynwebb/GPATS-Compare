/****************************************************************************
 *	File:	FaultFile.h														*
 *																			*
 *	Creation Date:	30 June 2008											*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0.0.0		Complete rebuild of fhdb nam, visual dll software no	*
 *					longer available.  Include the dll code into the nam	*
 *					program.												*
 *																			*
 ***************************************************************************/

#ifndef __FAULTFILE_H_
#define __FAULTFILE_H_

class CFAULTSAccessor
{

	public:

		DBTIMESTAMP		m_Start_Time;
		DBTIMESTAMP		m_Stop_Time;
	    TCHAR			m_ERO[6];
	    TCHAR			m_TPCCN[26];
		TCHAR			m_UUT_Serial_No[16];
		TCHAR			m_UUT_Rev[11];
		TCHAR			m_ID_Serial_No[11];
		VARIANT_BOOL	m_Test_Status;
		TCHAR			m_Failure_Step[11];
		TCHAR			m_Fault_Callout[20001];
		double			m_Meas_Value;
		TCHAR			m_Dimension[13];
		double			m_Upper_Limit;
		double			m_Lower_Limit;
		TCHAR			m_Operator_Comments[257];
		double			m_Temperature;
		VARIANT_BOOL	m_UDP;


		BEGIN_COLUMN_MAP(CFAULTSAccessor)
			COLUMN_ENTRY(2,  m_Start_Time)
			COLUMN_ENTRY(3,  m_Stop_Time)
			COLUMN_ENTRY(4,  m_ERO)
		    COLUMN_ENTRY(5,  m_TPCCN)
			COLUMN_ENTRY(6,  m_UUT_Serial_No)
			COLUMN_ENTRY(7,  m_UUT_Rev)
			COLUMN_ENTRY(8,  m_ID_Serial_No)
			COLUMN_ENTRY_TYPE(9,  DBTYPE_BOOL,  m_Test_Status)
			COLUMN_ENTRY(10, m_Failure_Step)
			COLUMN_ENTRY(11, m_Fault_Callout)
			COLUMN_ENTRY(12, m_Meas_Value)
			COLUMN_ENTRY(13, m_Dimension)
			COLUMN_ENTRY(14, m_Upper_Limit)
			COLUMN_ENTRY(15, m_Lower_Limit)
			COLUMN_ENTRY(16, m_Operator_Comments)
			COLUMN_ENTRY(17, m_Temperature)
			COLUMN_ENTRY_TYPE(18, DBTYPE_BOOL,  m_UDP)
		END_COLUMN_MAP()

		void ClearRecord() {
			memset(this, 0, sizeof(*this));
		}

};

class CFAULTS : public CTable<CAccessor<CFAULTSAccessor> >
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

			dbinit.AddProperty(DBPROP_INIT_DATASOURCE, OLESTR("FHDB"));
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

			return CTable<CAccessor<CFAULTSAccessor> >::Open(m_session, _T("FAULTS"), &propset);
		}

		CSession	m_session;
};

#endif
