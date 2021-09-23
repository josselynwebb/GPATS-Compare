#ifndef _DWGDATA_H
#define _DWGDATA_H


#define	MAX_TSET		256

#define _VI				0x0001
#define _VO				0x0002
#define _LD				0x0004
#define _SR				0x0008
#define _ERR			0x0100

#define _VL(i,vl)		((vl) << (i ? 4 : 0))
#define _VLALL			(_VL(0, _VI | _VO | _LD | _SR) | _VL(1, _VI | _VO | _LD | _SR))

#define _TASK_NONE		0x00
#define _TASK_STIM		0x01
#define _TASK_SENSE		0x02
#define _TASK_PROVE		0x04
#define _TASK_READ		(_TASK_SENSE | _TASK_PROVE)

#define _VAL_NONE		-1
#define _VAL_LO			 0
#define _VAL_HI			 1
#define _VAL_NHIZ		 2
#define _VAL_HIZ		 3
#define _VAL_LO_ISI		 4
#define _VAL_HI_ISI		 5

#define _VL_NONE		-1
#define _VL_0			 0
#define _VL_1			 1

#define _FMT_FORCE		-1
#define _FMT_NORET		 0
#define _FMT_RET1		 1
#define _FMT_RET0		 2
#define _FMT_RETH		 3

#define _TSET_PHASE_ASSERT		0
#define _TSET_PHASE_RETURN		1
#define _TSET_WINDOW_OPEN		2
#define _TSET_WINDOW_CLOSE		3


/*********************************************************/
/*	Data Types											 */
/*********************************************************/
typedef void* HDWG;

typedef struct tag_DwgEvent
{
	int		hEvent;
	double	dValue;
	int		hRef;
} DwgEvent, *HEV;

typedef struct tagTimeSet
{
	double dClockRate;
	double pData[4][4];	
} TimeSet, *HTS;

typedef struct tagPinPatInfo
{
	short	nTask;			// one of _TASK_xxx
	short	nValue;			// one of _VAL_xxx
	short	nRef;			// used only for prove
	short	nMask;			// used only for prove
} PinPatInfo, *HPIN;

extern int dwgtotpcnt;

/************************************************************/
/*	Events													*/
/************************************************************/
HEV  Events_Read(int hEvent);
void Events_Update(int hEvent, double dTimeVal, int hRef);
void Events_Delete(int hEvent);
void Events_Clear();

void Events_Dump();

/************************************************************/
/*	VL														*/
/************************************************************/
int  VL_GetLevelIndexUsage();
void VL_SetLevelIndexUsage(int nVL);
void VL_ResetLevelIndexUsage(int nVL);

/************************************************************/
/*	TSTable													*/
/************************************************************/
int  TSTable_GetTSETCount(HDWG pThis);
HTS  TSTable_GetTSET(HDWG pThis, int nIdx);
void TSTable_AddTSET(HDWG pThis, int nTask, 
	double dPeriod, double dStart, double dStop, int bDefault);

/************************************************************/
/*	Pattern													*/
/************************************************************/
HPIN Pattern_GetPinInfo(HDWG pThis, int nIdx);

/************************************************************/
/*	Layer													*/
/************************************************************/
int  Layer_GetPatternCount(HDWG pThis);
HDWG Layer_GetPattern(HDWG pThis, int nIdx);

void Layer_SetValue(HDWG pThis, int nCount, int* pData, int nPattern);
void Layer_SetRef(HDWG pThis, int nCount, int* pData, int nPattern);
void Layer_SetMask(HDWG pThis, int nCount, int* pData, int nPattern);
int  Layer_GetTsetIndex(HDWG pThis);

int  Layer_GetRepeatCount(HDWG pThis);
void Layer_SetRepeatCount(HDWG pThis, int nCount);

void Layer_GetPinList(HDWG pThis, int nTask, int* pCount, int** ppPins);

void Layer_GetPinListOrder(HDWG pThis, int* pCount, int** ppData);
void Layer_SetPinListOrder(HDWG pThis, int  nCount, int*  pData);
void Layer_GetSaveComp(HDWG pThis, int* pCount, int** ppData);
void Layer_SetSaveComp(HDWG pThis, int  nCount, int*  pData);
void Layer_GetError(HDWG pThis, int* pCount, int** ppData);
void Layer_SetError(HDWG pThis, int  nCount, int*  pData);
void Layer_GetErrorIndex(HDWG pThis, int* pCount, int** ppData);
void Layer_SetErrorIndex(HDWG pThis, int  nCount, int*  pData);
void Layer_GetFaultCount(HDWG pThis, int* pCount);
void Layer_SetFaultCount(HDWG pThis, int  nCount);

/************************************************************/
/*	Test													*/
/************************************************************/
HDWG Test_Create();
void Test_Destroy(HDWG pThis);

int  Test_GetLayerCount(HDWG pThis);
HDWG Test_GetLayer(HDWG pThis, int nIdx);

HDWG Test_Stimulate(HDWG pThis, int nPatterns, int nPins, int* pPins);
HDWG Test_Sense(HDWG pThis, int nPatterns, int nPins, int* pPins);
HDWG Test_Prove(HDWG pThis, int nPatterns, int nPins, int* pPins);
int  Test_FinalizeData(HDWG pThis);

int  Test_GetLevelIndex(HDWG pThis, int nPin);
void Test_SetLevelIndex(HDWG pThis, int nVL, int nCount, int* pPins);

int  Test_GetFormat(HDWG pThis, int nPin);
void Test_SetFormat(HDWG pThis, int nFormat, int nCount, int* pPins);

int  Test_GetISI(HDWG pThis, int nPin);
void Test_SetISI(HDWG pThis, int nCount, int* pPins);

HDWG Test_GetTsetTable(HDWG pThis);

int  Test_GetRepeatCount(HDWG pThis);
void Test_SetRepeatCount(HDWG pThis, int nCount);

void Test_Fetch(HDWG pThis, HDWG* ppLayer, int* pBase);
void Test_FISI(HDWG pThis, HDWG* ppLayer, int* pBase);

void Test_Dump(HDWG pThis);


#endif 
