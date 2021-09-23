// Machine generated IDispatch wrapper class(es) created with ClassWizard

#include "stdafx.h"
#include "rfms.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif



/////////////////////////////////////////////////////////////////////////////
// IRFPm_if properties

/////////////////////////////////////////////////////////////////////////////
// IRFPm_if operations

long IRFPm_if::getClientCount(long* clientCount)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		clientCount);
	return result;
}

long IRFPm_if::setLock(long lock)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x2, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		lock);
	return result;
}

long IRFPm_if::getLock(long* lock)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x3, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		lock);
	return result;
}

long IRFPm_if::getSimulatorMode(long* simulatorMode)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x4, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		simulatorMode);
	return result;
}

long IRFPm_if::open(long simMode)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x5, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		simMode);
	return result;
}

long IRFPm_if::close()
{
	long result;
	InvokeHelper(0x6, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::reset()
{
	long result;
	InvokeHelper(0x7, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::bit()
{
	long result;
	InvokeHelper(0x8, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::restoreInstrSettings(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x9, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFPm_if::saveInstrSettings(LPCTSTR fname)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0xa, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		fname);
	return result;
}

long IRFPm_if::getError(long* ErrorCode, long* ErrorSeverity, BSTR* ErrorDescr, long EDLen, BSTR* MoreErrorInfo, long MEILen)
{
	long result;
	static BYTE parms[] =
		VTS_PI4 VTS_PI4 VTS_PBSTR VTS_I4 VTS_PBSTR VTS_I4;
	InvokeHelper(0xb, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		ErrorCode, ErrorSeverity, ErrorDescr, EDLen, MoreErrorInfo, MEILen);
	return result;
}

long IRFPm_if::getLockStatus(long* status)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0xc, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		status);
	return result;
}

long IRFPm_if::setMeasureMode(long Mode)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0xd, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Mode);
	return result;
}

long IRFPm_if::getMeasureMode(long* Mode)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0xe, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Mode);
	return result;
}

long IRFPm_if::setCalState(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0xf, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getCalState(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x10, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::setPowerOffset(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x11, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getPowerOffset(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x12, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::setPowerOffsetVal(float OffsetVal, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R4 VTS_BSTR;
	InvokeHelper(0x13, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		OffsetVal, Units);
	return result;
}

long IRFPm_if::getPowerOffsetVal(float* OffsetVal, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x14, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		OffsetVal, Units);
	return result;
}

long IRFPm_if::setPowerHead(LPCTSTR powerHead)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x15, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		powerHead);
	return result;
}

long IRFPm_if::getPowerHead(BSTR* powerHead)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x16, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		powerHead);
	return result;
}

long IRFPm_if::setPwrCorrectionFactor(float PwrCorr)
{
	long result;
	static BYTE parms[] =
		VTS_R4;
	InvokeHelper(0x17, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		PwrCorr);
	return result;
}

long IRFPm_if::getPwrCorrectionFactor(float* PwrCorr)
{
	long result;
	static BYTE parms[] =
		VTS_PR4;
	InvokeHelper(0x18, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		PwrCorr);
	return result;
}

long IRFPm_if::setExpFreq(float expFreqGHz, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R4 VTS_BSTR;
	InvokeHelper(0x19, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		expFreqGHz, Units);
	return result;
}

long IRFPm_if::getExpFreq(float* expFreqGHz, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x1a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		expFreqGHz, Units);
	return result;
}

long IRFPm_if::setAveraging(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x1b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getAveraging(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::setNumOfAverages(long NumOfAverages)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x1d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		NumOfAverages);
	return result;
}

long IRFPm_if::getNumOfAverages(long* NumOfAverages)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x1e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		NumOfAverages);
	return result;
}

long IRFPm_if::setTrigType(long trig)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x1f, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		trig);
	return result;
}

long IRFPm_if::getTrigType(long* trig)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x20, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		trig);
	return result;
}

long IRFPm_if::setRefOsc(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x21, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getRefOsc(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x22, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::setDisplayRate(long dispRate)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x23, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		dispRate);
	return result;
}

long IRFPm_if::getDisplayRate(long* dispRate)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x24, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		dispRate);
	return result;
}

long IRFPm_if::getMeasurement(float* measResult, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x25, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		measResult, Units);
	return result;
}

long IRFPm_if::setMeasureUnits(LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x26, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFPm_if::doCal()
{
	long result;
	InvokeHelper(0x27, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::doZero()
{
	long result;
	InvokeHelper(0x28, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::getLastMeasurement(float* measurement, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x29, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		measurement, Units);
	return result;
}

long IRFPm_if::setPwrMeterOn_Off(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x2a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getNumOfPowerHeads(long* NumOfHeads)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x2b, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		NumOfHeads);
	return result;
}

long IRFPm_if::readCFTable(BSTR* table)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x2c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		table);
	return result;
}

long IRFPm_if::getPowerHeadList(VARIANT* PowerHeadList)
{
	long result;
	static BYTE parms[] =
		VTS_PVARIANT;
	InvokeHelper(0x2d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		PowerHeadList);
	return result;
}

long IRFPm_if::changePHDescr(LPCTSTR NewDescr)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x2e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		NewDescr);
	return result;
}

long IRFPm_if::saveCFTable(LPCTSTR table)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x2f, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		table);
	return result;
}

long IRFPm_if::getRFMSVXIResrcInfo(BSTR* DigitizerDesc, BSTR* CalModDesc, BSTR* LODesc, BSTR* DownConverterDesc)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR VTS_PBSTR VTS_PBSTR VTS_PBSTR;
	InvokeHelper(0x31, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		DigitizerDesc, CalModDesc, LODesc, DownConverterDesc);
	return result;
}

long IRFPm_if::getVersion(BSTR* Version)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x32, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Version);
	return result;
}

long IRFPm_if::setMaxTime(long time)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x33, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		time);
	return result;
}

long IRFPm_if::getMaxTime(long* time)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x34, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		time);
	return result;
}

long IRFPm_if::setMonitorMode(long state)
{
	long result;
	static BYTE parms[] =
		VTS_I4;
	InvokeHelper(0x35, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getMonitorMode(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x36, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::getMeasureUnits(BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x37, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Units);
	return result;
}

long IRFPm_if::getPwrMeterOn_Off(long* state)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x38, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		state);
	return result;
}

long IRFPm_if::setRefCalFac(float refCalFac)
{
	long result;
	static BYTE parms[] =
		VTS_R4;
	InvokeHelper(0x39, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		refCalFac);
	return result;
}

long IRFPm_if::getRefCalFac(float* refCalFac)
{
	long result;
	static BYTE parms[] =
		VTS_PR4;
	InvokeHelper(0x3a, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		refCalFac);
	return result;
}

long IRFPm_if::doZeroAndCal()
{
	long result;
	InvokeHelper(0x3b, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::setRangeUpper(float range, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R4 VTS_BSTR;
	InvokeHelper(0x3c, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		range, Units);
	return result;
}

long IRFPm_if::getRangeUpper(float* range, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x3d, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		range, Units);
	return result;
}

long IRFPm_if::setRangeLower(float range, LPCTSTR Units)
{
	long result;
	static BYTE parms[] =
		VTS_R4 VTS_BSTR;
	InvokeHelper(0x3e, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		range, Units);
	return result;
}

long IRFPm_if::getRangeLower(float* range, BSTR* Units)
{
	long result;
	static BYTE parms[] =
		VTS_PR4 VTS_PBSTR;
	InvokeHelper(0x3f, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		range, Units);
	return result;
}

long IRFPm_if::setAutoRange()
{
	long result;
	InvokeHelper(0x40, DISPATCH_METHOD, VT_I4, (void*)&result, NULL);
	return result;
}

long IRFPm_if::getRangeMode(long* Mode)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x41, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		Mode);
	return result;
}

long IRFPm_if::setPowerHeadSerNum(LPCTSTR SerNum)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x42, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		SerNum);
	return result;
}

long IRFPm_if::getPowerHeadSerNum(BSTR* SerNum)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x43, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		SerNum);
	return result;
}

long IRFPm_if::setAttenuatorSerNum(LPCTSTR SerNum)
{
	long result;
	static BYTE parms[] =
		VTS_BSTR;
	InvokeHelper(0x44, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		SerNum);
	return result;
}

long IRFPm_if::getAttenuatorSerNum(BSTR* SerNum)
{
	long result;
	static BYTE parms[] =
		VTS_PBSTR;
	InvokeHelper(0x45, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		SerNum);
	return result;
}

long IRFPm_if::getZeroCalStatus(long* status)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x46, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		status);
	return result;
}

long IRFPm_if::getInstrStatus(long* status)
{
	long result;
	static BYTE parms[] =
		VTS_PI4;
	InvokeHelper(0x47, DISPATCH_METHOD, VT_I4, (void*)&result, parms,
		status);
	return result;
}
