Attribute VB_Name = "Module2"
Declare Function eip1143a_init& Lib "Eip1143a_32.dll" (ByVal x1$, ByVal x2%, ByVal x3%, x4&)
Declare Function eip1143a_appExample& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, x3#, ByVal x4#, ByVal x5%, ByVal x6&, ByVal x7#)
Declare Function eip1143a_confTrigger& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3#, ByVal x4%, ByVal x5&, ByVal x6&)
Declare Function eip1143a_confAcCurr& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3#, ByVal x4%)
Declare Function eip1143a_confDcCurr& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3%, ByVal x4#, ByVal x5%)
Declare Function eip1143a_confAcVolt& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3%, ByVal x4#)
Declare Function eip1143a_confDcVolt& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3%, ByVal x4%, ByVal x5#, ByVal x6%, ByVal x7%)
Declare Function eip1143a_confFreqPer& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3%, ByVal x4%)
Declare Function eip1143a_confMath& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3#, ByVal x4%, ByVal x5#, ByVal x6#)
Declare Function eip1143a_confOut& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%)
Declare Function eip1143a_confResist& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3%, ByVal x4#, ByVal x5%, ByVal x6%)
Declare Function eip1143a_abort& Lib "Eip1143a_32.dll" (ByVal x1&)
Declare Function eip1143a_trigger& Lib "Eip1143a_32.dll" (ByVal x1&)
Declare Function eip1143a_initiate& Lib "Eip1143a_32.dll" (ByVal x1&)
Declare Function eip1143a_readMeas& Lib "Eip1143a_32.dll" (ByVal x1&, x2#)
Declare Function eip1143a_querDataPts& Lib "Eip1143a_32.dll" (ByVal x1&, x2%)
Declare Function eip1143a_readMinMax& Lib "Eip1143a_32.dll" (ByVal x1&, x2#, x3#, x4#, x5#)
Declare Function eip1143a_writeInstrData& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2$)
Declare Function eip1143a_readInstrData& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2%, ByVal x3$, x4&)
Declare Function eip1143a_reset& Lib "Eip1143a_32.dll" (ByVal x1&)
Declare Function eip1143a_selfTest& Lib "Eip1143a_32.dll" (ByVal x1&, x2%, ByVal x3$)
Declare Function eip1143a_errorQuery& Lib "Eip1143a_32.dll" (ByVal x1&, x2&, ByVal x3$)
Declare Function eip1143a_errorMessage& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2&, ByVal x3$)
Declare Function eip1143a_revisionQuery& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2$, ByVal x3$)
Declare Function eip1143a_waitForRqs& Lib "Eip1143a_32.dll" (ByVal x1&, ByVal x2&, x3%)
Declare Function eip1143a_close& Lib "Eip1143a_32.dll" (ByVal x1&)

Const VI_INSTR_ERROR_OFFSET = &HBFFC0900
Const VI_ERROR_FILE_OPEN = &HBFFC0800
Const VI_ERROR_FILE_WRITE = &HBFFC0801
Const VI_ERROR_INTERPRETING_RESPONSE = HBFFC0803

