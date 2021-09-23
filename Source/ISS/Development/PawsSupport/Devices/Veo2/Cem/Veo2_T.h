//345678901234567890123456789012345678901234567890123456789012345678901234567890
////////////////////////////////////////////////////////////////////////////////
// File:	    Veo2_T.h
//
// Date:	    15-Oct-07
//
// Purpose:	    Instrument Driver for VEO2
//
// Instrument:	VEO2  Electro-Optical Test Set
//
//
// Revision History described in Veo2_T.cpp
//
////////////////////////////////////////////////////////////////////////////////

typedef struct ModStructStruct 
{
    bool   Exists;
    int    Int;
    int    Dim;
    double Real;

}ModStruct;

typedef struct DblArrayStructStruct
{
	bool	Exists;
	double * val;
	int		Size;
	int		Dim;
}DblArrayStruct;

#define CAL_DATA_COUNT 2

class CVeo2_T
{
private:
    char      m_DeviceName[MAX_BC_DEV_NAME];
    int       m_Bus;
    int       m_PrimaryAdr;
    int       m_SecondaryAdr;
    int       m_Dbg;
    int       m_Sim;
    int       m_Handle;
	double    m_CalData[CAL_DATA_COUNT];

	char	m_SourceSignal[1024];
	char	m_SensorSignal[1024];

    // Modifiers
	ModStruct m_Azimuth;				// int
	ModStruct m_AngleRate;				// real
	ModStruct m_Delay;					// real
    ModStruct m_DifferentialTemp;		// real
    ModStruct m_DiffTempError;			// real
    ModStruct m_DiffTempInterval;		// real
    ModStruct m_DiffTempStart;			// real
    ModStruct m_DiffTempStop;			// real
	ModStruct m_Elevation;				// int
    ModStruct m_Filter;					// int
    ModStruct m_FirstActiveLine;		// int
    ModStruct m_IntensityRatio;			// real
    ModStruct m_LinesPerChannel;		// int
	ModStruct m_MainBeamAtten;			// real
	ModStruct m_MaxTime;				// real
    ModStruct m_MtfDirection;			// dim
    ModStruct m_MtfFreqPoints;			// int
	ModStruct m_NoiseEqDiffTemp;		// real
	ModStruct m_Period;					// real
	ModStruct m_Polarize;				// int
	ModStruct m_PowerP;					// real
	ModStruct m_PowerDensity;			// real
	ModStruct m_PulseEnergy;			// real
	ModStruct m_PulseWidth;				// real
	ModStruct m_Radiance;				// real
	ModStruct m_RadianceInterval;		// real
	ModStruct m_RadianceStart;			// real
	ModStruct m_RadianceStop;			// real
	ModStruct m_SampleCount;			// real
	ModStruct m_SampleTime;				// real
	ModStruct m_SettleTime;				// real
	ModStruct m_TestPointCount;			// int
	ModStruct m_TrigLevel;				// real
	ModStruct m_TriggerMode;			// int [dsc]
	ModStruct m_TriggerSlope;			// int [dsc]
	ModStruct m_WaveLength;				// real
	// Target Modifiers	
	ModStruct m_HorizTargetAngle;		// real, passed as DEG
	ModStruct m_VertTargetAngle;		// real, passed as DEG
	ModStruct m_HorizTargetOffset;		// real, passed as DEG
	ModStruct m_VertTargetOffset;		// real, passed as DEG
	ModStruct m_LastPulseRange;			// real
	ModStruct m_RangeError;				// real
	ModStruct m_TgtCoordinateTop;		// int
	ModStruct m_TgtCoordinateLeft;		// int
	ModStruct m_TgtCoordinateBottom;	// int
	ModStruct m_TgtCoordinateRight;		// int
	DblArrayStruct m_TargetData;		// real array
	ModStruct m_TargetRange;			// real
	ModStruct m_TargetType;				// int [dsc]
	// Boresight
	ModStruct m_HorizLosAlignError;		// real
	ModStruct m_VertLosAlignError;		// real
	ModStruct m_XAutocollimationError;	// real
	ModStruct m_YAutocollimationError;	// real
	ModStruct m_XBoresightAngle;		// real
	ModStruct m_YBoresightAngle;		// real
	// Video
	ModStruct m_Format;					// int [dsc]
	ModStruct m_HorizFieldOfView;		// real
	ModStruct m_VertFieldOfView;		// real
	// Distortion modifiers
	ModStruct m_DistPosCount;
	DblArrayStruct m_DistortionPositions;

	// Added laser source for STR 9677
	ModStruct m_Prf;					// real

	int	m_SignalNoun;
	int m_SignalType;

	// signal class flags
	bool	m_Boresight;
	bool	m_Target;
	bool	m_Video;
	bool	m_Trigger;

public:
     CVeo2_T(char *DeviceName, int Bus, int Prime, int Second, int Dbg, int Sim);
    ~CVeo2_T(void);
public:
    int StatusVeo2(int);
    int SetupVeo2(int);
    int InitiateVeo2(int);
    int FetchVeo2(int);
    int OpenVeo2(int);
    int CloseVeo2(int);
    int ResetVeo2(int);
private:
    int  ErrorVeo2(int Status, char *ErrMsg);
    int  GetStmtInfoVeo2(int Fnc);
    void InitPrivateVeo2(void);
    void NullCalDataVeo2(void);
	void ProcessFnc(int);
	void BuildIrSensor1641(int Fnc);
	void BuildLaserSensor1641(int Fnc);
	void BuildVisSensor1641(int Fnc);
	void BuildSource1641(int Fnc);
	void SetIRDefaults(int Fnc);
	void SetVisDefaults(int Fnc);
	void SetLaserDefaults(int Fnc);
	void SetSourceDefaults(int Fnc);
};
