#ifndef DATASTRUCTURES_H
#define DATASTRUCTURES_H

#include <afx.h>

typedef char NameStr[128];

//Specific types of cameras available
char* sCameraSelection[11] = {"RS170_350x320",
							  "RS343_675x800",
							  "RS343_675x1000",
							  "RS343_729x800",
							  "RS343_729x1000",
							  "RS343_875x800",
							  "RS343_875x1000",
							  "RS343_945x800",
							  "RS343_945x1000",
							  "RS343_1023x800",
							  "RS343_1023x1000"};


//Image specific data
struct CnfImgData
{
	long unNumFrames;
	float fHFOV;
	float fVFOV;
	unsigned short int unCenterX;
	unsigned short int unCenterY;
};

//Target specific data
struct CnfTrgData
{
	short int snPosition;
	short int snFeature;
};

//Rectangular Block (con't)
struct Rect
{
	long Left;
	long Top;
	long Right;
	long Bottom;
};

//Rectangular block
struct BlockData
{
	Rect Block;
	long unFlag;
};


struct BRCPData
{
	unsigned short int unSize;
	unsigned short int unFlag;
	unsigned short int unLine;
	unsigned short int unUnused;
};


struct WordFlagData
{
	short int snValue;
	unsigned short int unFlag;
};


struct CriteriaPair
{
	float fLo;
	float fHi;
};


struct Criteria
{
	NameStr sHead;
	CriteriaPair Good;
	CriteriaPair Accept;
	CriteriaPair Poor;
};

//Collimator specific data
struct CollimatorData
{
	char* CollimatorName;
	float fFocalLength;
	float fTransmitance;
};

//Minimum Resolvable Temperature/Contrast specific data
struct CnfMMrtdData
{
	short int snPosition;
	short int snFeature;
	float fTemperature;
	Criteria Crit;
};

//Temperature specific data
struct TmpRngData
{
	float fBegTemp;
	float fEndTemp;
	float fIncTemp;
};

//Radiance specific data
struct RadRngData
{
	float fBegRad;
	float fEndRad;
	float fIncRad;
};

//single point on a curve
struct GraphPoint
{
	float X;
	float Y;
};

//used to obtain data from a source that has more than one curve
#define  MAX_YUNITS  7
#define  MAX_XUNITS  7

struct GraphData
{
	char* Name;
	unsigned short int nGrType;
	unsigned short int nGrColor;
	unsigned short int nGrMark;
	unsigned short int nCount;
	GraphPoint XLimit;
	GraphPoint YLimit;
	NameStr XLabelUnits;
	NameStr YLabelUnits;
	NameStr Title;
    NameStr GYUnits[MAX_YUNITS+1];
	long GYUnitsCount;
	long GYUnitsSel;
    NameStr GXUnits[MAX_YUNITS+1];
	long GXUnitsCount;
	long GXUnitsSel;
};


//Interface Constants
  const int OPDISPLAY_FLAG = 2;


  const int ID_CONFIGFILE    = 1;
  const int ID_TPLIST        = 2;
  const int ID_ACTIVETP      = 3;
  const int ID_ACTIVECONFIG  = 4;
  const int ID_ACTIVEREPORT  = 5;
  const int ID_ACTIVERESULTS = 6;
  const int ID_ACTIVEMEASURE = 7;
  const int ID_ACTIVEPARAM   = 8;
  const int ID_DCDLIST       = 9;
  const int ID_ACTIVEDCD     = 10;
  const int ID_UUTRECORD     = 11;
  const int ID_UUTDIRECT     = 12;
  const int ID_ACTIVEMEMO    = 13;
  const int ID_REPORTLIST    = 14;
  const int ID_TRGWHEEL      = 15;
  const int ID_ACTIVETARGET  = 16;
  const int ID_ACTIVEFEATURE = 17;
  const int ID_KVALUES       = 18;
  const int ID_ACTIVEKVALUE  = 19;
  const int ID_ASSETLIST     = 20;
  const int ID_ACTIVEASSET   = 21;
  const int ID_SYSTEMOPTIONS = 22;
  const int ID_SUBASSET      = 23; 
  const int ID_MEASURELIST   = 24;
  const int ID_COLLIMATOR    = 25;
  const int ID_MACROLIST     = 26;
  const int ID_ACTIVEMACRO   = 27;
  const int ID_FLTWHEEL      = 28;
  const int ID_APRWHEEL      = 29;
  const int ID_ACTIVEKPOINTS = 30;
  const int ID_ACTIVEKDATA   = 31;

  const int CMD_NAME     = 1001;
  const int CMD_FILENAME = 1002;
  const int CMD_TYPE     = 1003;
  const int CMD_REVISION = 1004;
  const int CMD_LISTSIZE = 1005;
  const int CMD_SELECT   = 1006;
  const int CMD_USEMEMO  = 1007;
  const int CMD_GETFILE  = 1008;
  const int CMD_SETFILE  = 1009;
  const int CMD_CLEAR    = 1010;
  const int CMD_STATUS   = 1011; 
  const int CMD_EXECUTE  = 1012; 
  const int CMD_DELETE   = 1013; 
  const int CMD_FLAGS    = 1014; 
  const int CMD_VALUE    = 1015;
  const int CMD_ADDNEW   = 1016;
  const int CMD_WIDTH    = 1017;
  const int CMD_HEIGHT   = 1018;
  const int CMD_MAGNIFY  = 1019;
  const int CMD_IMGBMP   = 1020; 
  const int CMD_IMGFIT   = 1021; 
  const int CMD_IMGWIDTH = 1022; 
  const int CMD_IMGHEIGHT= 1023; 
  const int CMD_UNITS    = 1024;
  const int CMD_FREQUENCY= 1025;
  const int CMD_MAXVALUE = 1026; 
  const int CMD_MINVALUE = 1027; 
  const int CMD_SETPOINT = 1028;
  const int CMD_ERROR    = 1028;
  const int CMD_DISPLAY  = 1029;
  const int CMD_CURVE    = 1030;
  const int CMD_CURVEPNT = 1031;
  const int CMD_SCALAR   = 1032;
  const int CMD_TABLECELL= 1033;
  const int CMD_IMAGEX   = 1034; 
  const int CMD_IMAGEY   = 1035;
  const int CMD_DATAX    = 1036; 
  const int CMD_DATAY    = 1037;
  const int CMD_IMGDIMEN = 1038;
  const int CMD_IMGOFFS  = 1039;
  const int CMD_IMGAGC   = 1040;
  const int CMD_GAIN     = 1041;
  const int CMD_OFFSET   = 1042;
  const int CMD_COPYFROM = 1043; 
  const int CMD_MOVETO   = 1044; 
  const int CMD_PRTABLE  = 1045;
  const int CMD_PRGRAPH  = 1046;
  const int CMD_KDATA    = 1047;
  const int CMD_ADDRESS  = 1048;
  const int CMD_IMMEDIATE= 1049; // These are used to send immediate commands to an asset 
                                 //CMD_SETPOINT & CMD_VALUE are indirect, through the ASSET state machine 


  const int STATUS_AVAILABLE = 0x0200;

  const int DTC_ID = 1;      //{ Differential temperature control }
  const int TRG_ID = 2;      //{ Target wheel control }
  const int ILL_ID = 3;      //{ Illuminator control }
  const int APR_ID = 4;      //{ Aperture wheel }
  const int FLT_ID = 5;      //{ Filter wheel }
  const int SHT_ID = 6;      //{ Shutter }
  const int MDL_ID = 7;      //{ Modulator }

  const int XPS_ID = 8;      //{ X Axis motion control }
  const int YPS_ID = 9;      //{ Y axis motion control }
  const int ZPS_ID = 10;     //{ Z axis motion control }

  const int HTC_ID = 11;     //{ High tempreature controller }

  const int ATC_ID = 12;     //{ Absolute tempreature control - differential controller }
  const int ATM_ID = 13;     //{ Ambient temperature monitor - differential controller }
  const int RDY_ID = 14;     //{ Ready window access }

  const int VAP_ID = 15;     //{ Visible Aperture }
  const int ROT_ID = 16;     //{ Target Rotation }
  const int COL_ID = 17;     //{ Lamp Color Setting }
  const int RAD_ID = 18;     //{ Lamp Radiance (Iris opening) }

  const int MMF_ID = 19;     //{ Modulator, Multifunction Module }
  const int MLS_ID = 20;     //{ Modulator and laser camera stage }
  const int SRC_ID = 21;     //{ Source stage }
  const int FMS_ID = 22;     //{ Folding mirror stage }
  const int I1A_ID = 23;     //{ Diode 105 Amplitude }
  const int I1P_ID = 24;     //{ Diode 105 Period }
  const int I1W_ID = 25;     //{ Diode 105 Pulse Width }
  const int I2A_ID = 26;     //{ Diode 155 Amplitude }
  const int I2P_ID = 27;     //{ Diode 155 Period }
  const int I2W_ID = 28;     //{ Diode 155 Pulse Width }
  const int CAP_ID = 29;     //{ Camera trigger }
  const int EXP_ID = 30;     //{ Camera exposure }
  const int CFR_ID = 31;     //{ Camera free run rate }
  const int CLV_ID = 32;     //{ Camera Live View }
  const int PDL_ID = 33;     //{ Pulse delay }
  const int EXT_ID = 34;     //{ External camera trigger }

  const int TGT_ID = 35;
  const int CTR_ID = 36;
  const int CXP_ID = 37;
  const int CAT_ID = 38;
  const int CPD_ID = 39;
  const int MFP_ID = 40;
  const int MPS_ID = 41;
  const int MFR_ID = 42;
  const int MLC_ID = 43;
  const int CXT_ID = 44;

  const int M920_ID = 101;
  const int M921_ID = 102;
  const int M930_ID = 103;
  const int MOVE_ID = 104;
  const int VISM_ID = 105;
  const int MFM_ID  = 106;
  const int MSM_ID  = 107;



#endif
