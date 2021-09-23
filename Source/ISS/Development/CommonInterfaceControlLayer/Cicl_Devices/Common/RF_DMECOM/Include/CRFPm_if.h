// Machine generated IDispatch wrapper class(es) created with Add Class from Typelib Wizard

// CRFPm_if wrapper class

class CRFPm_if : public COleDispatchDriver
{
public:
	CRFPm_if(){} // Calls COleDispatchDriver default constructor
	CRFPm_if(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	CRFPm_if(const CRFPm_if& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

	// Attributes
public:

	// Operations
public:


	// IRFPm_if methods
public:
	long getClientCount(long * clientCount)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x1, DISPATCH_METHOD, VT_I4, (void*)&result, parms, clientCount);
		return result;
	}
	long setLock(long lock)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x2, DISPATCH_METHOD, VT_I4, (void*)&result, parms, lock);
		return result;
	}
	long getLock(long * lock)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x3, DISPATCH_METHOD, VT_I4, (void*)&result, parms, lock);
		return result;
	}
	long getSimulatorMode(long * simulatorMode)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x4, DISPATCH_METHOD, VT_I4, (void*)&result, parms, simulatorMode);
		return result;
	}
	long open(long simMode)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x5, DISPATCH_METHOD, VT_I4, (void*)&result, parms, simMode);
		return result;
	}
	long close()
	{
		long result;
		InvokeHelper(0x6, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long reset()
	{
		long result;
		InvokeHelper(0x7, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long bit()
	{
		long result;
		InvokeHelper(0x8, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long restoreInstrSettings(LPCTSTR fname)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x9, DISPATCH_METHOD, VT_I4, (void*)&result, parms, fname);
		return result;
	}
	long saveInstrSettings(LPCTSTR fname)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0xa, DISPATCH_METHOD, VT_I4, (void*)&result, parms, fname);
		return result;
	}
	long getError(long * ErrorCode, long * ErrorSeverity, BSTR * ErrorDescr, long EDLen, BSTR * MoreErrorInfo, long MEILen)
	{
		long result;
		static BYTE parms[] = VTS_PI4 VTS_PI4 VTS_PBSTR VTS_I4 VTS_PBSTR VTS_I4 ;
		InvokeHelper(0xb, DISPATCH_METHOD, VT_I4, (void*)&result, parms, ErrorCode, ErrorSeverity, ErrorDescr, EDLen, MoreErrorInfo, MEILen);
		return result;
	}
	long getLockStatus(long * status)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0xc, DISPATCH_METHOD, VT_I4, (void*)&result, parms, status);
		return result;
	}
	long setMeasureMode(long Mode)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xd, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Mode);
		return result;
	}
	long getMeasureMode(long * Mode)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0xe, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Mode);
		return result;
	}
	long setCalState(long state)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0xf, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getCalState(long * state)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x10, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long setPowerOffset(long state)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x11, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getPowerOffset(long * state)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x12, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long setPowerOffsetVal(float OffsetVal, LPCTSTR Units)
	{
		long result;
		static BYTE parms[] = VTS_R4 VTS_BSTR ;
		InvokeHelper(0x13, DISPATCH_METHOD, VT_I4, (void*)&result, parms, OffsetVal, Units);
		return result;
	}
	long getPowerOffsetVal(float * OffsetVal, BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PR4 VTS_PBSTR ;
		InvokeHelper(0x14, DISPATCH_METHOD, VT_I4, (void*)&result, parms, OffsetVal, Units);
		return result;
	}
	long setPowerHead(LPCTSTR powerHead)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x15, DISPATCH_METHOD, VT_I4, (void*)&result, parms, powerHead);
		return result;
	}
	long getPowerHead(BSTR * powerHead)
	{
		long result;
		static BYTE parms[] = VTS_PBSTR ;
		InvokeHelper(0x16, DISPATCH_METHOD, VT_I4, (void*)&result, parms, powerHead);
		return result;
	}
	long setPwrCorrectionFactor(float PwrCorr)
	{
		long result;
		static BYTE parms[] = VTS_R4 ;
		InvokeHelper(0x17, DISPATCH_METHOD, VT_I4, (void*)&result, parms, PwrCorr);
		return result;
	}
	long getPwrCorrectionFactor(float * PwrCorr)
	{
		long result;
		static BYTE parms[] = VTS_PR4 ;
		InvokeHelper(0x18, DISPATCH_METHOD, VT_I4, (void*)&result, parms, PwrCorr);
		return result;
	}
	long setExpFreq(float expFreqGHz, LPCTSTR Units)
	{
		long result;
		static BYTE parms[] = VTS_R4 VTS_BSTR ;
		InvokeHelper(0x19, DISPATCH_METHOD, VT_I4, (void*)&result, parms, expFreqGHz, Units);
		return result;
	}
	long getExpFreq(float * expFreqGHz, BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PR4 VTS_PBSTR ;
		InvokeHelper(0x1a, DISPATCH_METHOD, VT_I4, (void*)&result, parms, expFreqGHz, Units);
		return result;
	}
	long setAveraging(long state)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1b, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getAveraging(long * state)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x1c, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long setNumOfAverages(long NumOfAverages)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1d, DISPATCH_METHOD, VT_I4, (void*)&result, parms, NumOfAverages);
		return result;
	}
	long getNumOfAverages(long * NumOfAverages)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x1e, DISPATCH_METHOD, VT_I4, (void*)&result, parms, NumOfAverages);
		return result;
	}
	long setTrigType(long trig)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x1f, DISPATCH_METHOD, VT_I4, (void*)&result, parms, trig);
		return result;
	}
	long getTrigType(long * trig)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x20, DISPATCH_METHOD, VT_I4, (void*)&result, parms, trig);
		return result;
	}
	long setRefOsc(long state)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x21, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getRefOsc(long * state)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x22, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long setDisplayRate(long dispRate)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x23, DISPATCH_METHOD, VT_I4, (void*)&result, parms, dispRate);
		return result;
	}
	long getDisplayRate(long * dispRate)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x24, DISPATCH_METHOD, VT_I4, (void*)&result, parms, dispRate);
		return result;
	}
	long getMeasurement(float * measResult, BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PR4 VTS_PBSTR ;
		InvokeHelper(0x25, DISPATCH_METHOD, VT_I4, (void*)&result, parms, measResult, Units);
		return result;
	}
	long setMeasureUnits(LPCTSTR Units)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x26, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Units);
		return result;
	}
	long doCal()
	{
		long result;
		InvokeHelper(0x27, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long doZero()
	{
		long result;
		InvokeHelper(0x28, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long getLastMeasurement(float * measurement, BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PR4 VTS_PBSTR ;
		InvokeHelper(0x29, DISPATCH_METHOD, VT_I4, (void*)&result, parms, measurement, Units);
		return result;
	}
	long setPwrMeterOn_Off(long state)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x2a, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getNumOfPowerHeads(long * NumOfHeads)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x2b, DISPATCH_METHOD, VT_I4, (void*)&result, parms, NumOfHeads);
		return result;
	}
	long readCFTable(VARIANT * CFData)
	{
		long result;
		static BYTE parms[] = VTS_PVARIANT ;
		InvokeHelper(0x2c, DISPATCH_METHOD, VT_I4, (void*)&result, parms, CFData);
		return result;
	}
	long getPowerHeadList(VARIANT * PowerHeadList)
	{
		long result;
		static BYTE parms[] = VTS_PVARIANT ;
		InvokeHelper(0x2d, DISPATCH_METHOD, VT_I4, (void*)&result, parms, PowerHeadList);
		return result;
	}
	long changePHDescr(LPCTSTR NewDescr)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x2e, DISPATCH_METHOD, VT_I4, (void*)&result, parms, NewDescr);
		return result;
	}
	long saveCFTable(VARIANT * CFData)
	{
		long result;
		static BYTE parms[] = VTS_PVARIANT ;
		InvokeHelper(0x2f, DISPATCH_METHOD, VT_I4, (void*)&result, parms, CFData);
		return result;
	}
	long saveCFTableAs(VARIANT * CFData, LPCTSTR Descr, LPCTSTR NewDescrNamechangePHDescr)
	{
		long result;
		static BYTE parms[] = VTS_PVARIANT VTS_BSTR VTS_BSTR ;
		InvokeHelper(0x30, DISPATCH_METHOD, VT_I4, (void*)&result, parms, CFData, Descr, NewDescrNamechangePHDescr);
		return result;
	}
	long getRFMSVXIResrcInfo(BSTR * DigitizerDesc, BSTR * CalModDesc, BSTR * LODesc, BSTR * DownConverterDesc)
	{
		long result;
		static BYTE parms[] = VTS_PBSTR VTS_PBSTR VTS_PBSTR VTS_PBSTR ;
		InvokeHelper(0x31, DISPATCH_METHOD, VT_I4, (void*)&result, parms, DigitizerDesc, CalModDesc, LODesc, DownConverterDesc);
		return result;
	}
	long getVersion(BSTR * Version)
	{
		long result;
		static BYTE parms[] = VTS_PBSTR ;
		InvokeHelper(0x32, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Version);
		return result;
	}
	long setMaxTime(long time)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x33, DISPATCH_METHOD, VT_I4, (void*)&result, parms, time);
		return result;
	}
	long getMaxTime(long * time)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x34, DISPATCH_METHOD, VT_I4, (void*)&result, parms, time);
		return result;
	}
	long setMonitorMode(long state)
	{
		long result;
		static BYTE parms[] = VTS_I4 ;
		InvokeHelper(0x35, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getMonitorMode(long * state)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x36, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long getMeasureUnits(BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PBSTR ;
		InvokeHelper(0x37, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Units);
		return result;
	}
	long getPwrMeterOn_Off(long * state)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x38, DISPATCH_METHOD, VT_I4, (void*)&result, parms, state);
		return result;
	}
	long setRefCalFac(float refCalFac)
	{
		long result;
		static BYTE parms[] = VTS_R4 ;
		InvokeHelper(0x39, DISPATCH_METHOD, VT_I4, (void*)&result, parms, refCalFac);
		return result;
	}
	long getRefCalFac(float * refCalFac)
	{
		long result;
		static BYTE parms[] = VTS_PR4 ;
		InvokeHelper(0x3a, DISPATCH_METHOD, VT_I4, (void*)&result, parms, refCalFac);
		return result;
	}
	long doZeroAndCal()
	{
		long result;
		InvokeHelper(0x3b, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long setRangeUpper(float range, LPCTSTR Units)
	{
		long result;
		static BYTE parms[] = VTS_R4 VTS_BSTR ;
		InvokeHelper(0x3c, DISPATCH_METHOD, VT_I4, (void*)&result, parms, range, Units);
		return result;
	}
	long getRangeUpper(float * range, BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PR4 VTS_PBSTR ;
		InvokeHelper(0x3d, DISPATCH_METHOD, VT_I4, (void*)&result, parms, range, Units);
		return result;
	}
	long setRangeLower(float range, LPCTSTR Units)
	{
		long result;
		static BYTE parms[] = VTS_R4 VTS_BSTR ;
		InvokeHelper(0x3e, DISPATCH_METHOD, VT_I4, (void*)&result, parms, range, Units);
		return result;
	}
	long getRangeLower(float * range, BSTR * Units)
	{
		long result;
		static BYTE parms[] = VTS_PR4 VTS_PBSTR ;
		InvokeHelper(0x3f, DISPATCH_METHOD, VT_I4, (void*)&result, parms, range, Units);
		return result;
	}
	long setAutoRange()
	{
		long result;
		InvokeHelper(0x40, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
		return result;
	}
	long getRangeMode(long * Mode)
	{
		long result;
		static BYTE parms[] = VTS_PI4 ;
		InvokeHelper(0x41, DISPATCH_METHOD, VT_I4, (void*)&result, parms, Mode);
		return result;
	}
	long setPowerHeadSerNum(LPCTSTR SerNum)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x42, DISPATCH_METHOD, VT_I4, (void*)&result, parms, SerNum);
		return result;
	}
	long getPowerHeadSerNum(BSTR * SerNum)
	{
		long result;
		static BYTE parms[] = VTS_PBSTR ;
		InvokeHelper(0x43, DISPATCH_METHOD, VT_I4, (void*)&result, parms, SerNum);
		return result;
	}
	long setAttenuatorSerNum(LPCTSTR SerNum)
	{
		long result;
		static BYTE parms[] = VTS_BSTR ;
		InvokeHelper(0x44, DISPATCH_METHOD, VT_I4, (void*)&result, parms, SerNum);
		return result;
	}
	long getAttenuatorSerNum(BSTR * SerNum)
	{
		long result;
		static BYTE parms[] = VTS_PBSTR ;
		InvokeHelper(0x45, DISPATCH_METHOD, VT_I4, (void*)&result, parms, SerNum);
		return result;
	}

	// IRFPm_if properties
public:

};
