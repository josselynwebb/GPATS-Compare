// Machine generated IDispatch wrapper class(es) created with ClassWizard
/////////////////////////////////////////////////////////////////////////////
// IRFPm_if wrapper class

class IRFPm_if : public COleDispatchDriver
{
public:
	IRFPm_if() {}		// Calls COleDispatchDriver default constructor
	IRFPm_if(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	IRFPm_if(const IRFPm_if& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

// Attributes
public:

// Operations
public:
	long getClientCount(long* clientCount);
	long setLock(long lock);
	long getLock(long* lock);
	long getSimulatorMode(long* simulatorMode);
	long open(long simMode);
	long close();
	long reset();
	long bit();
	long restoreInstrSettings(LPCTSTR fname);
	long saveInstrSettings(LPCTSTR fname);
	long getError(long* ErrorCode, long* ErrorSeverity, BSTR* ErrorDescr, long EDLen, BSTR* MoreErrorInfo, long MEILen);
	long getLockStatus(long* status);
	long setMeasureMode(long Mode);
	long getMeasureMode(long* Mode);
	long setCalState(long state);
	long getCalState(long* state);
	long setPowerOffset(long state);
	long getPowerOffset(long* state);
	long setPowerOffsetVal(float OffsetVal, LPCTSTR Units);
	long getPowerOffsetVal(float* OffsetVal, BSTR* Units);
	long setPowerHead(LPCTSTR powerHead);
	long getPowerHead(BSTR* powerHead);
	long setPwrCorrectionFactor(float PwrCorr);
	long getPwrCorrectionFactor(float* PwrCorr);
	long setExpFreq(float expFreqGHz, LPCTSTR Units);
	long getExpFreq(float* expFreqGHz, BSTR* Units);
	long setAveraging(long state);
	long getAveraging(long* state);
	long setNumOfAverages(long NumOfAverages);
	long getNumOfAverages(long* NumOfAverages);
	long setTrigType(long trig);
	long getTrigType(long* trig);
	long setRefOsc(long state);
	long getRefOsc(long* state);
	long setDisplayRate(long dispRate);
	long getDisplayRate(long* dispRate);
	long getMeasurement(float* measResult, BSTR* Units);
	long setMeasureUnits(LPCTSTR Units);
	long doCal();
	long doZero();
	long getLastMeasurement(float* measurement, BSTR* Units);
	long setPwrMeterOn_Off(long state);
	long getNumOfPowerHeads(long* NumOfHeads);
	long readCFTable(BSTR* table);
	long getPowerHeadList(VARIANT* PowerHeadList);
	long changePHDescr(LPCTSTR NewDescr);
	long saveCFTable(LPCTSTR table);
	long getRFMSVXIResrcInfo(BSTR* DigitizerDesc, BSTR* CalModDesc, BSTR* LODesc, BSTR* DownConverterDesc);
	long getVersion(BSTR* Version);
	long setMaxTime(long time);
	long getMaxTime(long* time);
	long setMonitorMode(long state);
	long getMonitorMode(long* state);
	long getMeasureUnits(BSTR* Units);
	long getPwrMeterOn_Off(long* state);
	long setRefCalFac(float refCalFac);
	long getRefCalFac(float* refCalFac);
	long doZeroAndCal();
	long setRangeUpper(float range, LPCTSTR Units);
	long getRangeUpper(float* range, BSTR* Units);
	long setRangeLower(float range, LPCTSTR Units);
	long getRangeLower(float* range, BSTR* Units);
	long setAutoRange();
	long getRangeMode(long* Mode);
	long setPowerHeadSerNum(LPCTSTR SerNum);
	long getPowerHeadSerNum(BSTR* SerNum);
	long setAttenuatorSerNum(LPCTSTR SerNum);
	long getAttenuatorSerNum(BSTR* SerNum);
	long getZeroCalStatus(long* status);
	long getInstrStatus(long* status);
};
