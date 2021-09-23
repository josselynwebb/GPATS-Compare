// Machine generated IDispatch wrapper class(es) created with ClassWizard

#include "stdafx.h"
#include "rfms.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif



/////////////////////////////////////////////////////////////////////////////
// IRFMa_if properties

/////////////////////////////////////////////////////////////////////////////
// IRFMa_if operations

long IRFMa_if::getClientCount(long* clientCount)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		clientCount);
	return result;
}

long IRFMa_if::setLock(long lock)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x2, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		lock);
	return result;
}

long IRFMa_if::getLock(long* lock)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x3, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		lock);
	return result;
}

long IRFMa_if::getSimulatorMode(long* simulatorMode)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x4, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		simulatorMode);
	return result;
}

long IRFMa_if::open(long simMode)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x5, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		simMode);
	return result;
}

long IRFMa_if::close()
{
	long result;
	InvokeHelper(0x6, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFMa_if::reset()
{
	long result;
	InvokeHelper(0x7, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFMa_if::bit()
{
	long result;
	InvokeHelper(0x8, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFMa_if::restoreInstrSettings(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x9, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFMa_if::saveInstrSettings(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0xa, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFMa_if::getError(long* ErrorCode, long* ErrorSeverity, BSTR* ErrorDescr, long EDLen, BSTR* MoreErrorInfo, long MEILen)
{
	long result;
	static BYTE parms[] =
		VTS_PI4 VTS_PI4 VTS_PBSTR VTS_I4 VTS_PBSTR VTS_I4;
	InvokeHelper(0xb, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		ErrorCode, ErrorSeverity, ErrorDescr, EDLen, MoreErrorInfo, MEILen);
	return result;
}

long IRFMa_if::getLockStatus(long* status)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0xc, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		status);
	return result;
}

long IRFMa_if::setCenterFreq(double freq, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0xd, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		freq, Units);
	return result;
}

long IRFMa_if::setAttenuator(long Attenuator)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0xe, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Attenuator);
	return result;
}

long IRFMa_if::setSpan(double span, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0xf, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		span, Units);
	return result;
}

long IRFMa_if::setAverage(long avg)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x10, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		avg);
	return result;
}

long IRFMa_if::getCenterFreq(double* freq, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x11, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		freq, Units);
	return result;
}

long IRFMa_if::getAttenuator(long* ATNN)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x12, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		ATNN);
	return result;
}

long IRFMa_if::getSpan(double* span, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x13, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		span, Units);
	return result;
}

long IRFMa_if::getAverage(long* avg)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x14, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		avg);
	return result;
}

long IRFMa_if::getPeakHold(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x17, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFMa_if::setPeakHold(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x18, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFMa_if::getNextMarkerDown(long* peakPos)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		peakPos);
	return result;
}

long IRFMa_if::getNextMarkerUp(long* peakPos)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		peakPos);
	return result;
}

long IRFMa_if::markerPeakSearch(long* peakPos)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		peakPos);
	return result;
}

long IRFMa_if::compareMarkers(long PeakPosA, long PeakPosB, float* FrequencyDiff, BSTR* FreqUnits, float* PowerDiff, BSTR* PwrUnits)
{
	long result;
	static BYTE parms[] =
		VTS_I4 VTS_I4 VTS_PR4 VTS_PBSTR VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x1d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		PeakPosA, PeakPosB, FrequencyDiff, FreqUnits, PowerDiff, PwrUnits);
	return result;
}

long IRFMa_if::getNumOfMarkerPks(long* NumOfMarkers)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		NumOfMarkers);
	return result;
}

long IRFMa_if::setNumOfMarkerPks(long NumOfMarkers)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x1f, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		NumOfMarkers);
	return result;
}

long IRFMa_if::setStartFreq(double startFreq, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0x24, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		startFreq, Units);
	return result;
}

long IRFMa_if::getStartFreq(double* startFreq, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x25, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		startFreq, Units);
	return result;
}

long IRFMa_if::setStopFreq(double stopFreq, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0x26, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		stopFreq, Units);
	return result;
}

long IRFMa_if::getStopFreq(double* stoptFreq, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x27, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		stoptFreq, Units);
	return result;
}

long IRFMa_if::getFreqStep(double* FreqStep, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x28, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		FreqStep, Units);
	return result;
}

long IRFMa_if::setFreqStep(double FreqStep, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0x29, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		FreqStep, Units);
	return result;
}

long IRFMa_if::setMeasureUnits(LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x2a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFMa_if::getRBW(double* RBW, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x2b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		RBW, Units);
	return result;
}

long IRFMa_if::setRBW(double RBW, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0x2c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		RBW, Units);
	return result;
}

long IRFMa_if::getRFMSVXIResrcInfo(BSTR* DigitizerDesc, BSTR* CalModDesc, BSTR* LODesc, BSTR* DownConverterDesc)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR VTS_PBSTR VTS_PBSTR VTS_PBSTR;
	InvokeHelper(0x2d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		DigitizerDesc, CalModDesc, LODesc, DownConverterDesc);
	return result;
}

long IRFMa_if::getVersion(BSTR* Version)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x2e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Version);
	return result;
}

long IRFMa_if::setYigFreq(double yigFreq, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R8 VTS_BSTR;
	InvokeHelper(0x31, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		yigFreq, Units);
	return result;
}

long IRFMa_if::getYigFreq(double* yigFreq, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_PBSTR;
	InvokeHelper(0x32, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		yigFreq, Units);
	return result;
}

long IRFMa_if::setYigState(long yigState)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x33, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		yigState);
	return result;
}

long IRFMa_if::getYigState(long* yigState)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x34, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		yigState);
	return result;
}

long IRFMa_if::saveWaveform(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x35, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFMa_if::getWaveform(VARIANT* WaveformData, LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_PVARIANT VTS_BSTR;
	InvokeHelper(0x36, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		WaveformData, fname);
	return result;
}

long IRFMa_if::addWaveform(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x37, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFMa_if::subWaveform(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x38, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFMa_if::compWaveform(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x39, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFMa_if::getSampleSpacing(double* sampleSpacing, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_BSTR;
	InvokeHelper(0x3a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		sampleSpacing, Units);
	return result;
}

long IRFMa_if::getSampleTime(double* sampleTime, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_BSTR;
	InvokeHelper(0x3b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		sampleTime, Units);
	return result;
}

long IRFMa_if::getSampleWidth(double* sampleWidth, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR8 VTS_BSTR;
	InvokeHelper(0x3c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		sampleWidth, Units);
	return result;
}

long IRFMa_if::setMaxTime(long time)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x3d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		time);
	return result;
}

long IRFMa_if::getMaxTime(long* time)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x3e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		time);
	return result;
}

long IRFMa_if::getMeasurement(VARIANT* Measurment, BSTR* aUnits)
{
	long result;
	static BYTE parms[] =
		VTS_PVARIANT VTS_PBSTR;
	InvokeHelper(0x3f, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Measurment, aUnits);
	return result;
}

long IRFMa_if::setTrigSlope(long slope)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x40, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		slope);
	return result;
}

long IRFMa_if::getTrigSlope(long* slope)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x41, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		slope);
	return result;
}

long IRFMa_if::setTrigSource(long source)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x42, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		source);
	return result;
}

long IRFMa_if::getTrigSource(long* source)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x43, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		source);
	return result;
}

long IRFMa_if::getTrigState(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x44, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFMa_if::setTrigLevel(double level)
{
	long result;
	static BYTE parms[] =
		VTS_R8;
	InvokeHelper(0x45, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		level);
	return result;
}

long IRFMa_if::getTrigLevel(double* level)
{
	long result;
	static BYTE parms[] =
		VTS_PR8;
	InvokeHelper(0x46, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		level);
	return result;
}

long IRFMa_if::setPreTrigDelay(double delayMs)
{
	long result;
	static BYTE parms[] =
		VTS_R8;
	InvokeHelper(0x47, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		delayMs);
	return result;
}

long IRFMa_if::getPreTrigDelay(double* delayMs)
{
	long result;
	static BYTE parms[] =
		VTS_PR8;
	InvokeHelper(0x48, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		delayMs);
	return result;
}

long IRFMa_if::setArmSource(long source)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x49, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		source);
	return result;
}

long IRFMa_if::getArmSource(long* source)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x4a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		source);
	return result;
}

long IRFMa_if::setArmSlope(long slope)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x4b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		slope);
	return result;
}

long IRFMa_if::getArmSlope(long* slope)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x4c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		slope);
	return result;
}

long IRFMa_if::getArmState(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x4d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFMa_if::setMeasSignalType(long sigType)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x4e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		sigType);
	return result;
}

long IRFMa_if::getMeasSignalType(long* sigType)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x4f, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		sigType);
	return result;
}

long IRFMa_if::setMeasureMode(long measMode)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x50, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		measMode);
	return result;
}

long IRFMa_if::getMeasureMode(long* measMode)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x51, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		measMode);
	return result;
}

long IRFMa_if::getFFTSmplLen(long* FFTSmpLen)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x52, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		FFTSmpLen);
	return result;
}

long IRFMa_if::setFilterType(long FilterType)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x53, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		FilterType);
	return result;
}

long IRFMa_if::getFilterType(long* FilterType)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x54, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		FilterType);
	return result;
}

long IRFMa_if::getWaveformLen(long* sampleLen)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x55, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		sampleLen);
	return result;
}

long IRFMa_if::setMarkerFreqUnits(LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x56, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFMa_if::getMarkerFreqUnits(BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x57, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFMa_if::setMarkerPwrUnits(LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x58, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFMa_if::getMarkerPwrUnits(BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x59, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFMa_if::setMonitorMode(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x5a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFMa_if::getMonitorMode(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x5b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFMa_if::setHarmonic(long Harmonic)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x5c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Harmonic);
	return result;
}

long IRFMa_if::getHarmonic(long* Harmonic)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x5d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Harmonic);
	return result;
}

long IRFMa_if::getMeasureUnits(BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x5e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}
