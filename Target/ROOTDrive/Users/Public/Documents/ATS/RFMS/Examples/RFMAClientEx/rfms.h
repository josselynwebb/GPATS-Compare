// Machine generated IDispatch wrapper class(es) created with ClassWizard
/////////////////////////////////////////////////////////////////////////////
// IRFMa_if wrapper class

class IRFMa_if : public COleDispatchDriver
{
public:
	IRFMa_if() {}		// Calls COleDispatchDriver default constructor
	IRFMa_if(LPDISPATCH pDispatch) : COleDispatchDriver(pDispatch) {}
	IRFMa_if(const IRFMa_if& dispatchSrc) : COleDispatchDriver(dispatchSrc) {}

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
	long setCenterFreq(double freq, LPCTSTR Units);
	long setAttenuator(long Attenuator);
	long setSpan(double span, LPCTSTR Units);
	long setAverage(long avg);
	long getCenterFreq(double* freq, BSTR* Units);
	long getAttenuator(long* ATNN);
	long getSpan(double* span, BSTR* Units);
	long getAverage(long* avg);
	long getPeakHold(long* state);
	long setPeakHold(long state);
	long getNextMarkerDown(long* peakPos);
	long getNextMarkerUp(long* peakPos);
	long markerPeakSearch(long* peakPos);
	long compareMarkers(long PeakPosA, long PeakPosB, float* FrequencyDiff, BSTR* FreqUnits, float* PowerDiff, BSTR* PwrUnits);
	long getNumOfMarkerPks(long* NumOfMarkers);
	long setNumOfMarkerPks(long NumOfMarkers);
	long setStartFreq(double startFreq, LPCTSTR Units);
	long getStartFreq(double* startFreq, BSTR* Units);
	long setStopFreq(double stopFreq, LPCTSTR Units);
	long getStopFreq(double* stoptFreq, BSTR* Units);
	long getFreqStep(double* FreqStep, BSTR* Units);
	long setFreqStep(double FreqStep, LPCTSTR Units);
	long setMeasureUnits(LPCTSTR Units);
	long getRBW(double* RBW, BSTR* Units);
	long setRBW(double RBW, LPCTSTR Units);
	long getRFMSVXIResrcInfo(BSTR* DigitizerDesc, BSTR* CalModDesc, BSTR* LODesc, BSTR* DownConverterDesc);
	long getVersion(BSTR* Version);
	long setYigFreq(double yigFreq, LPCTSTR Units);
	long getYigFreq(double* yigFreq, BSTR* Units);
	long setYigState(long yigState);
	long getYigState(long* yigState);
	long saveWaveform(LPCTSTR fname);
	long getWaveform(VARIANT* WaveformData, LPCTSTR fname);
	long addWaveform(LPCTSTR fname);
	long subWaveform(LPCTSTR fname);
	long compWaveform(LPCTSTR fname);
	long getSampleSpacing(double* sampleSpacing, LPCTSTR Units);
	long getSampleTime(double* sampleTime, LPCTSTR Units);
	long getSampleWidth(double* sampleWidth, LPCTSTR Units);
	long setMaxTime(long time);
	long getMaxTime(long* time);
	long getMeasurement(VARIANT* Measurment, BSTR* aUnits);
	long setTrigSlope(long slope);
	long getTrigSlope(long* slope);
	long setTrigSource(long source);
	long getTrigSource(long* source);
	long getTrigState(long* state);
	long setTrigLevel(double level);
	long getTrigLevel(double* level);
	long setPreTrigDelay(double delayMs);
	long getPreTrigDelay(double* delayMs);
	long setArmSource(long source);
	long getArmSource(long* source);
	long setArmSlope(long slope);
	long getArmSlope(long* slope);
	long getArmState(long* state);
	long setMeasSignalType(long sigType);
	long getMeasSignalType(long* sigType);
	long setMeasureMode(long measMode);
	long getMeasureMode(long* measMode);
	long getFFTSmplLen(long* FFTSmpLen);
	long setFilterType(long FilterType);
	long getFilterType(long* FilterType);
	long getWaveformLen(long* sampleLen);
	long setMarkerFreqUnits(LPCTSTR Units);
	long getMarkerFreqUnits(BSTR* Units);
	long setMarkerPwrUnits(LPCTSTR Units);
	long getMarkerPwrUnits(BSTR* Units);
	long setMonitorMode(long state);
	long getMonitorMode(long* state);
	long setHarmonic(long Harmonic);
	long getHarmonic(long* Harmonic);
	long getMeasureUnits(BSTR* Units);
};
