//345678901234567890123456789012345678901234567890123456789012345678901234567890
////////////////////////////////////////////////////////////////////////////////
// File:	    Veo2_IrWin_T.h
//
// Date:	    11OCT05
//
// Purpose:	    Instrument Driver for Veo2_IrWin
//
// Instrument:	Veo2_IrWin  <device description> (<device type>)
//
//
// Revision History described in Veo2_IrWin_T.cpp
//
////////////////////////////////////////////////////////////////////////////////
extern int DE_BUG;

enum LsrErrMsg {
	PulseRepLsr,
	PulseWidLsr,
	PulseEngLsr
};

enum IrErrMsg {
	BoreSightIr,
	ModTranFunIr,
	GeoFidDistIr,
	NoiseEquivIr,
	ImageUniform,
	ChanIntegIr
};

enum VisErrMsg {
	BoreSightTv,
	ModTranFunVi,
	GeoFidDistVi,
	CameraUnifrm,
	GainVisible,
	ShadeOfGray,
	MinResolCont
};

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    char   Dim[ATXMLW_MAX_NAME];
    double Real;

}AttributeStruct;

typedef struct DblArrayStructStruct
{
	bool	Exists;
	double * val;
	int		Size;
}DblArrayStruct;

#define CAL_DATA_COUNT 2

#define	DEBUG_VEO2		"c:/aps/data/debugit_veo2"
#define	DEBUGIT_VEO2	"c:/aps/data/veo2Debug.txt"
#define LARRS_DAT		"C:/Users/Public/Documents/ATS/LARRS/larrs.dat"

#define IFNSIMVEO2(x)	{if((!m_Sim) && (m_PowerState)) { x ;} }
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

class CVeo2_IrWin_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    int       m_ResourceAddress;
    int       m_Dbg;
    int       m_Sim;
    int       m_Action;	
	float	m_Atten;
	double	m_CalData[CAL_DATA_COUNT];
    char	m_ResourceName[ATXMLW_MAX_NAME];
    char	m_InitString[ATXMLW_MAX_NAME+20];
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
    char      m_InputChannel[ATXMLW_MAX_NAME];  // For Sensors
    char      m_OutputChannel[ATXMLW_MAX_NAME]; // For Sources
    HINSTANCE m_Handle;
    ATXMLW_XML_HANDLE m_SignalDescription;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;

    // Modifiers
	AttributeStruct m_Azimuth;
    AttributeStruct m_Delay; 
    AttributeStruct m_DifferentialTemp;
	AttributeStruct m_DiffTempError;
    AttributeStruct m_DiffTempInterval;
	AttributeStruct m_DiffTempStart;
	AttributeStruct m_DiffTempStop;
	AttributeStruct m_Elevation;
	AttributeStruct m_Filter;
	AttributeStruct m_FirstActiveLine;
	AttributeStruct m_IntensityRatio;
	AttributeStruct m_LinesPerChannel;
	AttributeStruct m_MainBeamAtten;
    AttributeStruct m_MtfDirection;
    AttributeStruct m_MtfFreqPoints;
	AttributeStruct m_NoiseEqDiffTemp;
	AttributeStruct m_Period;
	AttributeStruct m_Polarize;
	AttributeStruct m_PowerP;
	AttributeStruct m_PowerDensity;
    AttributeStruct m_PulseEnergy;
    AttributeStruct m_PulseWidth;
	AttributeStruct m_Radiance;
    AttributeStruct m_RadianceInterval;
    AttributeStruct m_RadianceStart;
    AttributeStruct m_RadianceStop;
	AttributeStruct m_SampleCount;
	AttributeStruct m_SampleTime;
	AttributeStruct m_SettleTime;
	AttributeStruct m_TestPointCount;
	AttributeStruct m_WaveLength;
	// Target Modifiers	
	AttributeStruct m_DistPosCount;	
	DblArrayStruct  m_DistortionPositions;
	AttributeStruct m_HorizTargetAngle;
	AttributeStruct m_VertTargetAngle;
	AttributeStruct m_HorizTargetOffset;
	AttributeStruct m_VertTargetOffset;
	AttributeStruct m_LastPulseRange;
	AttributeStruct m_RangeError;
	AttributeStruct m_TgtCoordinateTop;
	AttributeStruct m_TgtCoordinateLeft;
	AttributeStruct m_TgtCoordinateBottom;
	AttributeStruct m_TgtCoordinateRight;
	DblArrayStruct  m_TargetData;
	AttributeStruct m_TargetRange;
	AttributeStruct m_TargetType;
	// Boresight
	AttributeStruct m_HorizLosAlignError;
	AttributeStruct m_VertLosAlignError;
	AttributeStruct m_XAutocollimationError;
	AttributeStruct m_YAutocollimationError;
	AttributeStruct m_XBoresightAngle;
	AttributeStruct m_YBoresightAngle;
	// Video
	AttributeStruct m_Format;
	AttributeStruct m_HorizFieldOfView;
	AttributeStruct m_VertFieldOfView;

	int m_BoresightIntensity;
	int m_SignalType;
	int m_SignalNoun;

	// State variables used to keep track of power on state
	int m_SourceState;
	int m_SensorState;
	int m_PowerState;

    ////// The following are standard attributes parsed by
    ATXMLW_STD_TRIG_INFO m_TrigInfo;
    ATXMLW_STD_GATE_INFO m_GateInfo;
    ATXMLW_STD_MEAS_INFO m_MeasInfo;

	// IrWindows function call place holders
	int		m_TrigInfoInt;	
	int		m_CenterX;
	int		m_CenterY;
	int		m_SignalBlockTopLeftX;
	int		m_SignalBlockTopLeftY;
	int		m_SignalBlockBotRightX;
	int		m_SignalBlockBotRightY;
	int		m_CorrectionCurve;
	int		m_Smoothing;
	int		m_TargetPosition[9];
	int		m_TargetFeatures[9];
	int		m_CameraTrigger;
	int		m_NumberOfSteps;
	int		m_VertLines;
	int		m_HorzLines;
	int		m_AzPos;
	int		m_ElPos;
	int		m_LAreaLo;
	int		m_LAreaHi;
	int		m_XCentroidLo;
	int		m_XCentroidHi;
	int		m_YCentroidLo;
	int		m_YCentroidHi;
	int		m_VoltScale;
	int		m_TimeBase;
	float	m_RangeCriteria;
	float	m_StartingDiffTemps[9];
	float	m_StartingRadiance[9];
	double	m_VertScale;
	double	m_HorzScale;
	double	m_VideoVoltRes;
public:
     CVeo2_IrWin_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CVeo2_IrWin_T(void);
public:
    int StatusVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalVeo2_IrWin(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalVeo2_IrWin(ATXMLW_INTF_CALDATA* CalData);
    int ResetVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstVeo2_IrWin(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsVeo2_IrWin(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallVeo2_IrWin(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void InitPrivateVeo2_IrWin(void);
private:
    /////// Single Action Functionality
	int  CVeo2_IrWin_T::CallSignalVeo2_IrWin(int action,
				ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SetupVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinIRSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinIRSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinLarrsSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinLaserSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinLaserSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinVisSensors(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int  SetupVeo2_IrWinVisSources(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchVeo2_IrWin(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  ErrorVeo2_IrWin(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  GetStmtInfoVeo2_IrWin(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    void NullCalDataVeo2_IrWin(void);
    bool GetSignalChar(char *Name, char *Response, int BufferSize);
    bool GetSignalCond(char *Name, char *InputNames, char *Response, int BufferSize);
    bool GetSignalSrcChar(char *Name, char *InputNames, char *Response, int BufferSize);
	void SetMeasDefaults(char * Response, int BufferSize);
	void SetSourceDefaults(void);
	bool WaitForReady(double time, double * elapsed, char * Response, int BufferSize);
	void ProcessCameraSelection(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void ReadLarrsDat();
	void SetCameraPower(int setting, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void SetSensorStage(int setting, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void CalculateTimeBaseSetting(void);
	void CalculateInputScaleData(void);
	float GetAveragePulseWidth(int channel, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void InitVeo2FuncPtrs(char * Response, int BufferSize);
	void ResetVeo2FuncPtrs(void);
};

void dodebug(int code, char *function_name, char *format, ...);
void s_IssueDriverFunctionCallVeo2_IrWin(int Handle,
                ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);

void ClearAttributeStruct(AttributeStruct & val);
void InitVeo2FuncPtrs(HINSTANCE);
void GetErrorMessage(int ecode, char *msg);
void GetIrStatusMessage(int Status, IrErrMsg IrMeasType, char *msg);
void GetCat1LaserStatusMessage(int Status, char *msg);
void GetCat2LaserStatusMessage(int Status, char *msg);
void GetVisStatusMessage(int Status, VisErrMsg VisMeasType, char *msg);
void GetLsrStatusMessage(int Status, LsrErrMsg IsrMeasType, char *msg);
float CalcPulse2Delay(double target, double pulse2);

static int s_TestParam(unsigned char *Param5, char*Param6);

static void s_TestParamAll(double *RetDblVal, double *RetDblArray,
                unsigned long *RetIntVal, unsigned long *RetIntArray,
                unsigned short *RetShortVal, unsigned short *RetShortArray,
                unsigned char *RetCharVal, unsigned char *RetCharArray,
                char *RetStr);