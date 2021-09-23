//2345678901234567890123456789012345678901234567890123456789012345678901234567890
///////////////////////////////////////////////////////////////////////////
// File:	    HPE1428A_T.h
//
// Date:	    05APR06
//
// Purpose:	    Instrument Driver for HPE1428A
//
// Instrument:	HPE1428A  <device description> (<device type>)
//
//
// Revision History described in HPE1428A_T.cpp
//
///////////////////////////////////////////////////////////////////////////////
#include "Visa.h"
#include <time.h>

typedef struct AttStructStruct 
{
    bool   Exists;
    int    Int;
    char   Dim[ATXMLW_MAX_NAME];	// ARC redefinition
    double Real;
	char   Text[256];

}AttributeStruct;

#define CAL_DATA_COUNT 2
#define JUST_DC_SIGNAL 0

extern int DE_BUG;
#define	DEBUG_HP1428		"c:/aps/data/debugit_HP1428"
#define	DEBUGIT_HP1428	"c:/aps/data/HP1428Debug.txt"
#define	ISDODEBUG(x)	{if (DE_BUG) {x ;} }

class CHPE1428A_T
{
private:
    int       m_InstNo;
    int       m_ResourceType;
    char      m_ResourceName[ATXMLW_MAX_NAME];
    int       m_ResourceAddress;
    ATXMLW_INSTRUMENT_ADDRESS m_AddressInfo;
    int       m_Dbg;
    int       m_Sim;
    char      m_InitString[ATXMLW_MAX_NAME+20];
	double    m_CalData[CAL_DATA_COUNT];
    int       m_Action;
    ATXMLW_XML_HANDLE m_SignalDescription;
    char      m_SignalName[ATXMLW_MAX_NAME];
    char      m_SignalElement[ATXMLW_MAX_NAME];
	char      m_InputChannel[ATXMLW_MAX_NAME];
	int		  Status;
	double    m_Result[8000];

	ViSession oscope_handle;
	ViSession m_Handle;

    // Modifiers
	AttributeStruct m_MaxTime;
    /////// Place HPE1428A Attribute structures here /////////
	AttributeStruct m_dDc_offset;
	AttributeStruct m_dAmplitude;
	AttributeStruct m_dFrequency;
	AttributeStruct m_Freq;				// ARC definition
	AttributeStruct m_dPeriod;
	AttributeStruct m_Periodic;			// ARC definition
	AttributeStruct m_RiseTime;			// ARC definition
	AttributeStruct m_FallTime;			// ARC definition
	AttributeStruct m_PulseWidth;		// ARC definition
	AttributeStruct m_nAcCoupling;
	AttributeStruct m_dBanwidthMin;
	AttributeStruct m_dBanwidthMax;
	AttributeStruct m_HighPassFilter;	// ARC definition
	AttributeStruct m_nGain;
	AttributeStruct m_Attenuator;		// ARC definition
	AttributeStruct m_nImpedance;
	AttributeStruct m_nChannel;
	AttributeStruct m_nSampleCount;
	AttributeStruct m_nMeasChar;
	AttributeStruct m_dSampleTime;
	AttributeStruct m_TEI;				// ARC definition
	AttributeStruct m_sSaveTo;
	AttributeStruct m_sSaveFrom;
	AttributeStruct m_sLoadFrom;
	AttributeStruct m_sCompareCh;
	AttributeStruct m_sCompareTo;
	AttributeStruct m_nAllowance;
	AttributeStruct m_sDifferentiate;
	AttributeStruct m_sIntegrate;
	AttributeStruct m_sMultiplyFrom;
	AttributeStruct m_sMultiplyTo;
	AttributeStruct m_sAddFrom;
	AttributeStruct m_sAddTo;
    AttributeStruct m_sSubtractFrom;
	AttributeStruct m_sSubtractTo;
	AttributeStruct m_sDestination;
	// 10MAY11 Added to set # of samples
	AttributeStruct m_dSamples;

	//
	AttributeStruct m_dTrigLevel;
	AttributeStruct m_nTrigSlope;
	AttributeStruct m_nTrigChannel;
	//
	AttributeStruct m_dGateStartLevel;
	AttributeStruct m_dGateStartSlope;
	AttributeStruct m_dGateStartChannel;
	AttributeStruct m_dGateStopLevel;
	AttributeStruct m_dGateStopSlope;
	AttributeStruct m_dGateStopChannel;

	////// The following are standard attributes parsed by
    ////// atxmlw_Get1641StdTrigChar, atxmlw_Get1641StdGateChar, and
    ////// atxmlw_Get1641StdMeasChar respectively
    ATXMLW_STD_TRIG_INFO m_TrigInfo;
    ATXMLW_STD_GATE_INFO m_GateInfo;
    ATXMLW_STD_MEAS_INFO m_MeasInfo;

	// PCR10030
	time_t m_StartTime;
	time_t m_StopTime;
	bool   m_TimeOut;

public:
     CHPE1428A_T(int Instno, int ResourceType, char* ResourceName,
                               int Sim, int Dbglvl,
                               ATXMLW_INSTRUMENT_ADDRESS *AddressInfoPtr,
                               ATXMLW_INTF_RESPONSE* Response, int Buffersize);
    ~CHPE1428A_T(void);
public:
    int StatusHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueSignalHPE1428A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int RegCalHPE1428A(ATXMLW_INTF_CALDATA* CalData);
    int ResetHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IstHPE1428A(int Level, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueNativeCmdsHPE1428A(ATXMLW_INTF_INSTCMD* InstrumentCommands,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int IssueDriverFunctionCallHPE1428A(ATXMLW_INTF_DRVRFNC* DriverFunction,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int ErrorHPE1428A(int Status, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
private:
    /////// Single Action Functionality
    int  SetupHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  EnableHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  DisableHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  SaResetHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    int  FetchHPE1428A(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
    ////// Utility functions
    int  GetStmtInfoHPE1428A(ATXMLW_INTF_SIGDESC* SignalDescription,
                ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	int read_waveform (ViSession instrID, int source,
                                   int xfer_type, double wave_array[],
                                   int *num_points, int *acq_count,
                                   double *sample_interval, double *time_offset,
                                   int *xref, double *volt_increment,
                                   double *volt_offset, int *yref);
    void InitPrivateHPE1428A(void);
    void NullCalDataHPE1428A(void);
	bool GetSignalChar(char *Name, char *Response, int BufferSize);
    bool GetSignalCond(char *Name, char *InputNames, char *Response, int BufferSize);
    bool GetSignalSrcChar(char *Name, char *InputNames, char *Response, int BufferSize);
	// Local function
	void scope_setup(double	sweeptime, ATXMLW_INTF_RESPONSE* Response, int Buffersize);
	//void set_scope_ampl(ATXMLW_INTF_RESPONSE* Response, int BufferSize);

	//void WriteHPe1428AB(ViString Out_Buf, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Voltage_pp(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Voltage_p(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Frequency(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Period(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Voltage_AC(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Voltage_DC(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Av_Voltage(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Voltage_p_pos(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Voltage_p_neg(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_RiseTime(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_FallTime(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_DutyCycle(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_PulseWidth(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Preshoot(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Overshoot(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_NegPulseWidth(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_PosPulseWidth(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Sample(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Save(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Load(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Compare(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_Math(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void Setup_TimeInterval(int Fnc, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void init_scope(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void send_digital_data(int chan, ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void save_waveform(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
	void send_compare_data(ATXMLW_INTF_RESPONSE* Response, int BufferSize);
};

void dodebug(int code, char *function_name, char *format, ...);