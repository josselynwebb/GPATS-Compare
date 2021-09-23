#include "ATLAS.h"
#include <cviauto.h>
#include <formatio.h>
#include <ansi_c.h>
#include <cvirte.h>     
#include <userint.h>
#include "fpNA65CS4.h"
#include "na65CS4.h"

#include "stdlib.h"
#include "search.h"      
#include "connect.h" 
#include "test.h"
#include "about.h"

void Connect(void);
static int panelHandle, refchpanel, dschpanel, sdchpanel;       
static int aboutHandle, testHandle, connectHandle, menuHandle, searchHandle;
int atlasHandle;

static int nSDVisibleChannelGrp = 1;
static int nSDChannelGrade = 0;     /* 0 for Instrument, 1 for Operational */   

static int nDSVisibleChannelGrp = 1;
static int nDSChannelGrade = 0;     /* 0 for Instrument, 1 for Operational */
static BOOL bSupportBothGrade = 1;

/* Part Specifications for DUT */
#define     MAX_PART_CNT        20
#define     MAX_PART_LENGTH     100

/* Tag definitions for tree nodes */
#define     SD_SET1_TAG         "SD_SET1"
#define     SD_SET2_TAG         "SD_SET2"
#define     DSI_SET1_TAG        "DSI_SET1"
#define     DSI_SET2_TAG        "DSI_SET2"
#define     DSO_SET1_TAG        "DSO_SET1" 
#define     DSO_SET2_TAG        "DSO_SET2" 
#define     REF_TAG             "REF_SET"

ViInt16     nPartSpecCnt = 0;
ViChar      szPartSpec[MAX_PART_CNT][MAX_PART_LENGTH];
ViInt16     nSDIChanCnt = 0; 
ViInt16     nSDOChanCnt = 0; 
ViInt16     nDSIChanCnt = 0;
ViInt16     nDSOChanCnt = 0;
ViInt16     nRefChanCnt = 0;
ViReal64    fMinRefFreq;
ViReal64    fMaxRefFreq;
ViReal64    fRef1PowerOutput;
ViReal64    fRef1MinVoltage;
ViReal64    fRef1MaxVoltage;
ViReal64    fRef2PowerOutput;
ViReal64    fRef2MinVoltage;
ViReal64    fRef2MaxVoltage;
ViInt16     nLanguage;

/* Global variables for tree control */
char szFirstSetLabel[25];
char szSecondSetLabel[25];

char szBuffer[512];

/* Array of control IDs */
ViInt16          aSD_ANGLE[8];
ViInt16          aSD_RDMODE[8];
int         aSD_IO[8];
int         aSD_RD[8];

int         aDS_ANGLE[8];
int         aDS_IO[8];
int         aDS_WRT[8];

int         aREF_VOLT[4];
int         aREF_FREQ[4];
int         aREF_IO[4];
int         aREF_WRITE[4];
int         aREF_LBL[4];
int         aREF_VOLT_LBL[4];
int         aREF_FREQ_LBL[4];

/*----------------------------------------------------------------------------*/
/* Function Prototypes                                                        */
/*----------------------------------------------------------------------------*/
void InitControlIDArrays(void); 
void SetSDChannelGroup(void);
void ShowDisableSDControls(int nCtrlNo, int nShow, int nDisable);
void SetDSChannelGradeAndGroup(void);
void ShowDisableDSControls(int nCtrlNo, int nShow, int nDisable);
void SetRefChannelGroup(void);
void ShowDisableRefControls(int nCtrlNo, int nShow, int nDisable);

void Set65CS1Dut(void);
void Set65CS2Dut(void);
void Set65CS4Dut(void);
void Set65CS4Dut(void);

void FillPartSpecChoices(void);
void GetPartSpecInfo(int nIndex);
void FillChannelInfo(void);
void FillChannelTreeInfo(void);
void PopulateSDNodes(void);
void PopulateDSNodes(void);
void PopulateRefNodes(void);

void GenMeasAtlas(char* chMessage);
void GenSimAtlas(char* chMessage);

/* Global variables */
ViStatus status; 
ViSession vi; 
ViInt16  nMaxSDIChanCnt = 16;
ViInt16  nMaxSDOChanCnt = 16; 
ViInt16  nMaxDSIChanCnt = 16;
ViInt16  nMaxDSOChanCnt = 16;
ViInt16  nMaxRefChanCnt = 4;

NA65CS4Found na65CS4inSystem[13];  
ViInt16 live = 0;
char slot[5],la[5],label[30],model[30];      
int picked = 0;                              


int __stdcall WinMain (HINSTANCE hInstance, HINSTANCE hPrevInstance,
                       LPSTR lpszCmdLine, int nCmdShow)
{
    int nSuccess;
    
    if (InitCVIRTE (hInstance, 0, 0) == 0)
        return -1;  /* out of memory */
    if ((panelHandle = LoadPanel (0, "fpNA65CS4.uir", na65CS)) < 0)
        return -1;
        
    /* Child Panels */
    if ((sdchpanel = LoadPanel (panelHandle, "fpNA65CS4.uir", SDCHPANEL)) < 0)
        return -1;
    if ((dschpanel = LoadPanel (panelHandle, "fpNA65CS4.uir", DSCHPANEL)) < 0)
        return -1;
    if ((refchpanel = LoadPanel (panelHandle, "fpNA65CS4.uir", REFCHPANEL)) < 0)
        return -1;
        
    InitControlIDArrays();
    
    DisplayPanel(sdchpanel);
    DisplayPanel(dschpanel);
    DisplayPanel(refchpanel);
    
    /* By default we are pre-selecting 65CS4 */
    Set65CS4Dut();
    DisplayPanel(panelHandle);
    Connect();
    RunUserInterface ();
	
	// Clean up
 	if(atlasHandle != 0)
		DiscardPanel(atlasHandle); 
    DiscardPanel(sdchpanel);
    DiscardPanel(dschpanel);
    DiscardPanel(refchpanel);
    DiscardPanel(panelHandle);
    return 0;
}

void InitControlIDArrays()
{

    /************************************/
    /*      S/D Panel Controls          */
    /************************************/
    aSD_ANGLE[0] = SDCHPANEL_SD_ANGLE_1;
    aSD_ANGLE[1] = SDCHPANEL_SD_ANGLE_2;
    aSD_ANGLE[2] = SDCHPANEL_SD_ANGLE_3;
    aSD_ANGLE[3] = SDCHPANEL_SD_ANGLE_4;
    aSD_ANGLE[4] = SDCHPANEL_SD_ANGLE_5;
    aSD_ANGLE[5] = SDCHPANEL_SD_ANGLE_6;
    aSD_ANGLE[6] = SDCHPANEL_SD_ANGLE_7;
    aSD_ANGLE[7] = SDCHPANEL_SD_ANGLE_8;

   

    aSD_IO[0] = SDCHPANEL_SD_IO_1;
    aSD_IO[1] = SDCHPANEL_SD_IO_2;
    aSD_IO[2] = SDCHPANEL_SD_IO_3;
    aSD_IO[3] = SDCHPANEL_SD_IO_4;
    aSD_IO[4] = SDCHPANEL_SD_IO_5;
    aSD_IO[5] = SDCHPANEL_SD_IO_6;
    aSD_IO[6] = SDCHPANEL_SD_IO_7;
    aSD_IO[7] = SDCHPANEL_SD_IO_8;

    aSD_RD[0] = SDCHPANEL_RD_1;
    aSD_RD[1] = SDCHPANEL_RD_2;
    aSD_RD[2] = SDCHPANEL_RD_3;
    aSD_RD[3] = SDCHPANEL_RD_4;
    aSD_RD[4] = SDCHPANEL_RD_5;
    aSD_RD[5] = SDCHPANEL_RD_6;
    aSD_RD[6] = SDCHPANEL_RD_7;
    aSD_RD[7] = SDCHPANEL_RD_8;

    /************************************/
    /*      D/S Panel Controls          */
    /************************************/
    aDS_ANGLE[0] = DSCHPANEL_DS_ANGLE_1;
    aDS_ANGLE[1] = DSCHPANEL_DS_ANGLE_2;
    aDS_ANGLE[2] = DSCHPANEL_DS_ANGLE_3;
    aDS_ANGLE[3] = DSCHPANEL_DS_ANGLE_4;
    aDS_ANGLE[4] = DSCHPANEL_DS_ANGLE_5;
    aDS_ANGLE[5] = DSCHPANEL_DS_ANGLE_6;
    aDS_ANGLE[6] = DSCHPANEL_DS_ANGLE_7;
    aDS_ANGLE[7] = DSCHPANEL_DS_ANGLE_8;

    aDS_IO[0] = DSCHPANEL_DS_IO_1;
    aDS_IO[1] = DSCHPANEL_DS_IO_2;
    aDS_IO[2] = DSCHPANEL_DS_IO_3;
    aDS_IO[3] = DSCHPANEL_DS_IO_4;
    aDS_IO[4] = DSCHPANEL_DS_IO_5;
    aDS_IO[5] = DSCHPANEL_DS_IO_6;
    aDS_IO[6] = DSCHPANEL_DS_IO_7;
    aDS_IO[7] = DSCHPANEL_DS_IO_8;

    aDS_WRT[0] = DSCHPANEL_WRT_1;
    aDS_WRT[1] = DSCHPANEL_WRT_2;
    aDS_WRT[2] = DSCHPANEL_WRT_3;
    aDS_WRT[3] = DSCHPANEL_WRT_4;
    aDS_WRT[4] = DSCHPANEL_WRT_5;
    aDS_WRT[5] = DSCHPANEL_WRT_6;
    aDS_WRT[6] = DSCHPANEL_WRT_7;
    aDS_WRT[7] = DSCHPANEL_WRT_8;

    /************************************/
    /*     REF Panel Controls          */
    /************************************/
    aREF_VOLT[0] = REFCHPANEL_REF_VOLT_1;
    aREF_VOLT[1] = REFCHPANEL_REF_VOLT_2;
    aREF_VOLT[2] = REFCHPANEL_REF_VOLT_3;
    aREF_VOLT[3] = REFCHPANEL_REF_VOLT_4;

    aREF_FREQ[0] = REFCHPANEL_REF_FREQ_1;
    aREF_FREQ[1] = REFCHPANEL_REF_FREQ_2;
    aREF_FREQ[2] = REFCHPANEL_REF_FREQ_3;
    aREF_FREQ[3] = REFCHPANEL_REF_FREQ_4;
    
    aREF_IO[0] = REFCHPANEL_REF_IO_1;
    aREF_IO[1] = REFCHPANEL_REF_IO_2;
    aREF_IO[2] = REFCHPANEL_REF_IO_3;
    aREF_IO[3] = REFCHPANEL_REF_IO_4;

    aREF_WRITE[0] = REFCHPANEL_REF_WRITE_1;
    aREF_WRITE[1] = REFCHPANEL_REF_WRITE_2;
    aREF_WRITE[2] = REFCHPANEL_REF_WRITE_3;
    aREF_WRITE[3] = REFCHPANEL_REF_WRITE_4;

    aREF_LBL[0] = REFCHPANEL_REF_LBL_1;
    aREF_LBL[1] = REFCHPANEL_REF_LBL_2;
    aREF_LBL[2] = REFCHPANEL_REF_LBL_3;
    aREF_LBL[3] = REFCHPANEL_REF_LBL_4;
    
    aREF_VOLT_LBL[0] = REFCHPANEL_REF_VOLT_LBL_1;
    aREF_VOLT_LBL[1] = REFCHPANEL_REF_VOLT_LBL_2;
    aREF_VOLT_LBL[2] = REFCHPANEL_REF_VOLT_LBL_3;
    aREF_VOLT_LBL[3] = REFCHPANEL_REF_VOLT_LBL_4;

    aREF_FREQ_LBL[0] = REFCHPANEL_REF_FREQ_LBL_1;
    aREF_FREQ_LBL[1] = REFCHPANEL_REF_FREQ_LBL_2;
    aREF_FREQ_LBL[2] = REFCHPANEL_REF_FREQ_LBL_3;
    aREF_FREQ_LBL[3] = REFCHPANEL_REF_FREQ_LBL_4;
}

                                                                                                                                              
void Connect()                                                                                                                                
{                                                                                                                                         
                                                                                                                                              
    ViUInt16 i;                                                                                                                               
    char     resource[80];                                                                                                                        
    char     string[128]; 
    ViInt16  cnt;
                                                                                                                                              
    SetCtrlAttribute (panelHandle, na65CS_CONNECT_LED, ATTR_OFF_COLOR, VAL_GRAY);                                                            
    ResetTextBox (panelHandle, na65CS_SLOT, ""  );                                                                                           
    ResetTextBox (panelHandle, na65CS_LA, ""  );                                                                                             
    ResetTextBox (panelHandle, na65CS_MODEL, ""  );                                                                                          
    SetCtrlAttribute (panelHandle, na65CS_ID_MESSAGE, ATTR_CTRL_VAL, "IDN" );                                                                
                                                                                                                                              
    /* Display seach window */                                                                                                                
    searchHandle = LoadPanel (panelHandle, "search.uir", search);                                                                             
    DisplayPanel(searchHandle);                                                                                                               
                                                                                                                                              
    /* Find Instruments */                                                                                                                    
    status = na65cs4_find( na65CS4inSystem, &cnt);                                                                                            
    DiscardPanel(searchHandle);                                                                                                               
                                                                                                                                              
                                                                                                                                              
    if (cnt > 0)                                                                                                                              
    {                                                                                                                                     
        /* add list process here */                                                                                                           
        if (cnt>1)                                                                                                                            
        {                                                                                                                                 
            connectHandle = LoadPanel (panelHandle, "connect.uir", CONNECT);                                                                 
                                                                                                                                              
            for(i=0;i<cnt;i++)                                                                                                                
            {                                                                                                                             
                sprintf(label,"  %2d             %3d",                                                                                        
                        na65CS4inSystem[i].slot,na65CS4inSystem[i].la);                                                                       
                InsertListItem (connectHandle, CONNECT_LIST, i, label, i);                                                                    
            }                                                                                                                             
                                                                                                                                              
            InstallPopup (connectHandle);                                                                                                     
        }                                                                                                                                 
        else                                                                                                                                 
        {  

            status = na65cs4_init(&na65CS4inSystem[0].resource[0], 1, 0, &vi);                                                             
            if (status == VI_SUCCESS)                                                                                                         
            {                                                                                                                             
                /* Update AutoConnect Display */                                                                                              
                sprintf(slot ,"%d", na65CS4inSystem[0].slot);                                                                                 
                sprintf(la  ,"%d", na65CS4inSystem[0].la);                                                                                    
                sprintf(model, "65CS4");                                                                                                      
                                                                                                                                              
                SetCtrlAttribute (panelHandle, na65CS_CONNECT_LED, ATTR_OFF_COLOR, VAL_GREEN);                                               
                SetCtrlAttribute (panelHandle, na65CS_SLOT, ATTR_CTRL_VAL, slot  );                                                          
                SetCtrlAttribute (panelHandle, na65CS_LA, ATTR_CTRL_VAL, la  );                                                              
                SetCtrlAttribute (panelHandle, na65CS_MODEL, ATTR_CTRL_VAL, model  );                                                        
                live = 1;                                                                                                                     
                if (status = na65cs4_query_id(vi, string))
                    strcpy(string,"<ERROR>");                                                       
                SetCtrlAttribute (panelHandle, na65CS_ID_MESSAGE, ATTR_CTRL_VAL, string  );                                                  
                SetCtrlAttribute (panelHandle, na65CS_ID_MESSAGE, ATTR_VISIBLE, 1);                                                          
                                                                                                                                              
            }                                                                                                                             
                                                                                                                                             
        }                                                                                                                                 
    }                                                                                                                                     
    else   
    {
        na65cs4_InitWithOptions ("VXI::14::INSTR", VI_TRUE, VI_TRUE,
                                 "Simulate=1,RangeCheck=1,QueryInstrStatus=0,Cache=1", 
                                 &vi);
        SetCtrlAttribute (panelHandle, na65CS_DEMO_LED, ATTR_OFF_COLOR, VAL_GREEN);  
    }
                                                                                                                                              
}                                                                                                                                             
                                                                                                                                              
                                                                                                                                              
                                                                                                                                              
int ConnectOk (int panel, int control, int event,                                                                                            
        void *callbackData, int eventData1, int eventData2)                                                                                   
{                                                                                                                                             
    ViStatus status;                                                                                                                          
    char string[128];                                                                                                                         
                                                                                                                                              
    switch (event) {                                                                                                                          
        case EVENT_COMMIT:                                                                                                                    
                                                                                                                                              
            GetCtrlVal (connectHandle, CONNECT_LIST, &picked);                                                                                
            DiscardPanel(connectHandle);                                                                                                      
                                                                                                                                              
            status = na65cs4_init(&na65CS4inSystem[picked].resource [0], 1, 0, &vi);                                                       
            if (status == VI_SUCCESS)                                                                                                         
                {                                                                                                                             
                /* Update AutoConnect Display */                                                                                              
                sprintf(slot ,"%d", na65CS4inSystem[picked].slot);                                                                            
                sprintf(la  ,"%d", na65CS4inSystem[picked].la);                                                                               
                sprintf(model ,"65CS4 SERIES");                                                                                               
                                                                                                                                              
                SetCtrlAttribute (panelHandle, na65CS_CONNECT_LED, ATTR_OFF_COLOR, VAL_GREEN);                                               
                SetCtrlAttribute (panelHandle, na65CS_SLOT, ATTR_CTRL_VAL, slot  );                                                          
                SetCtrlAttribute (panelHandle, na65CS_LA, ATTR_CTRL_VAL, la  );                                                              
                SetCtrlAttribute (panelHandle, na65CS_MODEL, ATTR_CTRL_VAL, model  );                                                        
                live = 1;                                                                                                                     
                if (status = na65cs4_query_id(vi, string))
                    strcpy(string,"<ERROR>");                                                       
                SetCtrlAttribute (panelHandle, na65CS_ID_MESSAGE, ATTR_CTRL_VAL, string  );                                                  
                SetCtrlAttribute (panelHandle, na65CS_ID_MESSAGE, ATTR_VISIBLE, 1);                                                          
                                                                                                                                              
                }                                                                                                                             
            else                                                                                                                              
                SetCtrlAttribute (panelHandle, na65CS_DEMO_LED, ATTR_OFF_COLOR, VAL_GREEN);                                                  
                                                                                                                                              
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
                                                                                                                                              
            break;                                                                                                                            
    }                                                                                                                                         
                                                                                                                                              
    return 0;                                                                                                                                 
}                                                                                                                                             
                                                                                                                                              
                                                                                                                                              
                                                                                                                                              
int  quit(int panel, int control, int event, void *callbackData, int eventData1, int eventData2)                                              
{                                                                                                                                             
    switch (event) {                                                                                                                          
        case EVENT_COMMIT:                                                                                                                    
            na65cs4_close (vi);                                                                                                            
            QuitUserInterface (0);                                                                                                            
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
            break;                                                                                                                            
    }                                                                                                                                         
    return 0;                                                                                                                                 
                                                                                                                                              
}                                                                                                                                             
                                                                                                                                              
                                                                                                                                              
int selftest (int panel, int control, int event,                                                                                              
        void *callbackData, int eventData1, int eventData2)                                                                                   
{                                                                                                                                             
    ViStatus status;                                                                                                                          
    ViInt16 testResult;                                                                                                                       
    char testMessage[512]= "Self Test Passed";                                                                                                
                                                                                                                                              
    switch (event) {                                                                                                                          
        case EVENT_COMMIT:                                                                                                                    
            testHandle = LoadPanel (panelHandle, "test.uir", TEST);                                                                           
            InstallPopup (testHandle);                                                                                                        
            status = na65cs4_self_test (vi , &testResult,testMessage);                                                                     
            if (testMessage[0] == '0') strcpy(testMessage,"Self Test Passed");                                                                
            MessagePopup ("SELF TEST", testMessage);                                                                                          
            DiscardPanel(testHandle);                                                                                                         
                                                                                                                                              
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
                                                                                                                                              
            break;                                                                                                                            
    }                                                                                                                                         
    return 0;                                                                                                                                 
}                                                                                                                                             
                                                                                                                                              
                                                                                                                                              
int about (int panel, int control, int event,                                                                                                 
        void *callbackData, int eventData1, int eventData2)                                                                                   
{                                                                                                                                             
    switch (event) {                                                                                                                          
        case EVENT_COMMIT:                                                                                                                    
            aboutHandle = LoadPanel (panelHandle, "about.uir", ABOUT);
            DisplayPanel (aboutHandle);                                                                                                       
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
                                                                                                                                              
            break;                                                                                                                            
    }                                                                                                                                         
    return 0;                                                                                                                                 
}                                                                                                                                             
                                                                                                                                              
int about_ok (int panel, int control, int event,                                                                                              
        void *callbackData, int eventData1, int eventData2)                                                                                   
{                                                                                                                                             
    switch (event) {                                                                                                                          
        case EVENT_COMMIT:                                                                                                                    
            DiscardPanel(aboutHandle);                                                                                                        
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
                                                                                                                                              
            break;                                                                                                                            
    }                                                                                                                                         
    return 0;                                                                                                                                 
}                                                                                                                                             
                                                                                                                                              
                                                                                                                                              

int CVICALLBACK SET_SD_ALL (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
ViInt16 chanpntr = 0; 
ViInt16 chan = 1;   
ViInt16 pntr = 0;   
ViReal64 num = 0;   
ViInt16 val = 0;
BOOL EvenChan = VI_FALSE;

    switch (event)
        {
        case EVENT_COMMIT:
        
            GetCtrlAttribute (panel, SDCHPANEL_SD_CHAN,ATTR_CTRL_VAL, &chanpntr);
            chan = chanpntr;
            
            if (chan == 2 | chan == 4 | chan == 6 | chan == 8) EvenChan = VI_TRUE;  
            if (chan == 10 | chan == 12 | chan == 14 | chan == 16) EvenChan = VI_TRUE; 
            
        
            GetCtrlVal (panel, SDCHPANEL_SD_MODE, &pntr);                        
            status = na65cs4_config_SDSignalMode(vi,chan,nSDChannelGrade,pntr);  
        
            GetCtrlVal (panel, SDCHPANEL_SD_TRACK, &pntr);      
            status = na65cs4_config_SDUpdateMode(vi,chan,nSDChannelGrade,pntr);
        
            GetCtrlVal (panel, SDCHPANEL_SD_BW, &pntr);      
            status = na65cs4_config_SDBandwidth(vi,chan,nSDChannelGrade,pntr);
        
            GetCtrlVal (panel, SDCHPANEL_SD_DC, &val);                               
            status = na65cs4_config_SDDCScale(vi,chan,nSDChannelGrade,val); 
            
            GetCtrlVal (panel, SDCHPANEL_SD_MAXT, &val);     
            status = na65cs4_config_SDMaxAngSettleTime(vi,chan,nSDChannelGrade,val);

            if (EvenChan == VI_TRUE){
            GetCtrlVal (panel, SDCHPANEL_SD_RATIO, &val);     
            status = na65cs4_config_SDRatio(vi,chan,nSDChannelGrade,val);
            }

            GetCtrlVal (panel, SDCHPANEL_SD_REF_SOURCE, &pntr);       
            status = na65cs4_config_SDRefSrcMode(vi,chan,nSDChannelGrade,pntr);

            break;
        }
    return 0;
}

int CVICALLBACK PANEL_HIDE (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    switch (event)
        {
        case EVENT_COMMIT:

            break;
        }
    return 0;
}

int CVICALLBACK RD_SD_ALL (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
ViInt16 chan = 0;
ViInt16 chanpntr = 1;  
ViInt16 pntr = 0;  
ViReal64 num = 0;  
ViInt16 val = 0;
char result[64] = "<bus error>"; 
BOOL EvenChan = VI_FALSE; 

    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel, SDCHPANEL_SD_CHAN,ATTR_CTRL_VAL, &chanpntr);
            chan = chanpntr;
           
            if (chan == 2 | chan == 4 | chan == 6 | chan == 8) EvenChan = VI_TRUE;  
            if (chan == 10 | chan == 12 | chan == 14 | chan == 16) EvenChan = VI_TRUE; 
           
           
            status = na65cs4_query_SDRelayIOState(vi, chan,nSDChannelGrade, &pntr); 
            if (pntr == NA65CS4_CLOSE)  sprintf(result ,"CLOSED"); 
            if (pntr == NA65CS4_OPEN)  sprintf(result ,"OPENED"); 
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel,SDCHPANEL_SD_IO_BOX, result);                                                                               
            if (status < 0) break;  
            
            status = na65cs4_query_SDVelocity(vi, chan,nSDChannelGrade, &num);    
            if (status >= 0)  sprintf(result,"%8.1f",num);   
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel,SDCHPANEL_SD_VEL_BOX, result);    
                                                          
            
            status = na65cs4_query_SDSignalMode(vi, chan,nSDChannelGrade, &pntr); 
            if (pntr == NA65CS4_SYNCHRO)  sprintf(result ,"SYN");  
            if (pntr == NA65CS4_RESOLVER)  sprintf(result ,"RSL"); 
            if (status < 0)  sprintf(result ,"<bus error>");       
            ResetTextBox (panel,SDCHPANEL_SD_MODE_BOX, result);              
             
            
            status = na65cs4_query_SDUpdateMode(vi, chan,nSDChannelGrade, &pntr);     
            if (pntr == NA65CS4_TRACK)  sprintf(result ,"TRACK");
            if (pntr == NA65CS4_LATCH)  sprintf(result ,"LATCH");
            if (status < 0)  sprintf(result ,"<bus error>");     
            ResetTextBox (panel,SDCHPANEL_SD_TRACK_BOX, result);  
                                                        
            
            status = na65cs4_query_SDBandwidth(vi, chan,nSDChannelGrade, &pntr);             
            if (pntr == NA65CS4_HIGH_BW)  sprintf(result ,"HIGH");             
            if (pntr == NA65CS4_LOW_BW)  sprintf(result ,"LOW");              
            if (status < 0)  sprintf(result ,"<bus error>");                  
            ResetTextBox (panel,SDCHPANEL_SD_BW_BOX, result);              
                                                        
            
            status = na65cs4_query_SDDCScale(vi, chan,nSDChannelGrade, &val); 
            if (status >= 0)  sprintf(result,"%d",val);     
            if (status < 0)  sprintf(result ,"<bus error>");   
            ResetTextBox (panel,SDCHPANEL_SD_DC_BOX, result); 
                                         
            
            status = na65cs4_query_SDMaxAngTime(vi, chan,nSDChannelGrade, &val); 
            if (status >= 0)  sprintf(result,"%3d",val);     
            if (status < 0)  sprintf(result ,"<bus error>");   
            ResetTextBox (panel,SDCHPANEL_SD_MAXT_BOX, result); 
                                         
            if (EvenChan == VI_TRUE){  
            status = na65cs4_query_SDRatio(vi, chan,nSDChannelGrade, &val); 
            if (status >= 0)  sprintf(result,"%3d",val);         
            if (status < 0)  sprintf(result ,"<bus error>");     
            ResetTextBox (panel,SDCHPANEL_SD_RATIO_BOX, result);  
            }
             
            if (EvenChan != VI_TRUE){  
            sprintf(result ,"---");     
            ResetTextBox (panel,SDCHPANEL_SD_RATIO_BOX, result);  
            }
            
            status = na65cs4_query_SDRefSrcMode(vi, chan,nSDChannelGrade, &pntr);       
            if (pntr == NA65CS4_EXT)  sprintf(result ,"EXT");     
            if (pntr == NA65CS4_INT)  sprintf(result ,"INT");        
            if (status < 0)  sprintf(result ,"<bus error>");           
            ResetTextBox (panel,SDCHPANEL_SD_REF_SOURCE_BOX, result);          
                                                 
            
            break;                                                                 
        }                                                                          
    return 0;                                                                      
}                        


int CVICALLBACK SET_DS_ALL (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
ViInt16 chanpntr = 0; 
ViInt16 chan = 1; 
ViInt16 pntr = 0;
ViReal64 num = 0;
ViInt16 val = 0; 
BOOL EvenChan = VI_FALSE; 

    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel, DSCHPANEL_DS_CHAN,ATTR_CTRL_VAL, &chanpntr);  
            chan = chanpntr; 
            
            if (chan == 2 | chan == 4 | chan == 6 | chan == 8) EvenChan = VI_TRUE;  
            if (chan == 10 | chan == 12 | chan == 14 | chan == 16) EvenChan = VI_TRUE; 
            
            GetCtrlVal (panel, DSCHPANEL_DS_DC, &num);
            status = na65cs4_config_DSDCScale(vi,chan,nDSChannelGrade,num); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_REF, &num);
            status = na65cs4_config_DSInputRefVolt(vi,chan,nDSChannelGrade,num); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_LTL, &num);
            status = na65cs4_config_DSLineToLineVoltage(vi,chan,nDSChannelGrade,num);
            
            GetCtrlVal (panel, DSCHPANEL_DS_MODE, &pntr);
            status = na65cs4_config_DSSignalMode(vi,chan,nDSChannelGrade,pntr); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_PH, &num);  
            status = na65cs4_config_DSPhaseShift(vi,chan,nDSChannelGrade,num); 
            
             if (EvenChan == VI_TRUE){
            GetCtrlVal (panel, DSCHPANEL_DS_RATIO, &val);     
            status = na65cs4_config_DSRatio(vi,chan,nDSChannelGrade,val);
            }
            
            GetCtrlVal (panel, DSCHPANEL_DS_REF_SOURCE, &pntr);
            status = na65cs4_config_DSRefSrcMode(vi,chan,nDSChannelGrade,pntr); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_TRIG_SOURCE, &pntr);
            status = na65cs4_config_DSTriggerSrc(vi,chan,nDSChannelGrade,pntr); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_TRIG_SLOPE, &pntr);
            status = na65cs4_config_DSTriggerSlope(vi,chan,nDSChannelGrade,pntr); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_ROT_MODE, &pntr);
            status = na65cs4_config_DSRotationMode(vi,chan,nDSChannelGrade,pntr); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_ROT_RATE, &num);
            status = na65cs4_config_DSRotationRate(vi,chan,nDSChannelGrade,num); 
            
            GetCtrlVal (panel, DSCHPANEL_DS_ROT_STOP_ANG, &num);  
            status = na65cs4_config_DSRotateStopAng(vi,chan,nDSChannelGrade,num); 
            
           
             
            break;
        }
    return 0;
}



int CVICALLBACK ROT_INIT (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
ViInt16 chanpntr = 0; 
ViInt16 chan = 1;
    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel, DSCHPANEL_DS_CHAN,ATTR_CTRL_VAL, &chanpntr);  
            chan = chanpntr;
            
            status = na65cs4_initiate_DSRotation(vi,chan,nDSChannelGrade); 
            break;
        }
    return 0;
}


int CVICALLBACK RD_DS_ALL (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
ViInt16 chanpntr = 0; 
ViInt16 chan = 1;
ViInt16 pntr = 0;
ViReal64 num = 0;  
ViInt16 val = 0; 
char result[64] = "<bus error>";
ViBoolean pntrTF = VI_TRUE;  
ViBoolean EvenChan = VI_FALSE;   

    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel, DSCHPANEL_DS_CHAN,ATTR_CTRL_VAL, &chanpntr);  
            chan = chanpntr;
            
            if (chan == 2 | chan == 4 | chan == 6 | chan == 8) EvenChan = VI_TRUE;  
            if (chan == 10 | chan == 12 | chan == 14 | chan == 16) EvenChan = VI_TRUE; 
            
            status = na65cs4_query_DSRelayIOState(vi, chan, nDSChannelGrade, &pntr); 
            if (pntr == NA65CS4_CLOSE)  sprintf(result ,"CLOSED"); 
            if (pntr == NA65CS4_OPEN)  sprintf(result ,"OPENED"); 
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel,DSCHPANEL_DS_IO_BOX, result);                                                                               
            if (status < 0) break;  
            
            status = na65cs4_query_DSAngle (vi, chan, nDSChannelGrade, &num);
            sprintf(result,"%8.4f",num);
            if (status < 0)  sprintf(result ,"<bus error>");
            ResetTextBox (panel, DSCHPANEL_DS_ANG_BOX,result); 
              
            status = na65cs4_query_DSDCScale(vi, chan, nDSChannelGrade, &val); 
            if (status >= 0)  sprintf(result,"%d",val);     
            if (status < 0)  sprintf(result ,"<bus error>");   
            ResetTextBox (panel,DSCHPANEL_DS_DC_BOX, result); 
            
            status = na65cs4_query_DSInputRefVolt (vi, chan, nDSChannelGrade, &num);
            sprintf(result,"%8.4f",num);
            if (status < 0)  sprintf(result ,"<bus error>");
            ResetTextBox (panel, DSCHPANEL_DS_REF_BOX,result); 
              
            status = na65cs4_query_DSLineToLineVoltage(vi, chan, nDSChannelGrade,&num);
            sprintf(result,"%8.4f",num);
            if (status < 0)  sprintf(result ,"<bus error>");
            ResetTextBox (panel, DSCHPANEL_DS_LTL_BOX,result); 
              
            
            status = na65cs4_query_DSSignalMode (vi, chan, nDSChannelGrade, &pntr);
            if (pntr == NA65CS4_SYNCHRO)  sprintf(result ,"SYN"); 
            if (pntr == NA65CS4_RESOLVER)  sprintf(result ,"RSL");
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel, DSCHPANEL_DS_MODE_BOX,result); 
              
            
            status = na65cs4_query_DSPhaseShift(vi, chan, nDSChannelGrade,&num);
            sprintf(result,"%8.4f",num);
            if (status < 0)  sprintf(result ,"<bus error>");
            ResetTextBox (panel, DSCHPANEL_DS_PH_BOX,result); 
              
            if (EvenChan == VI_TRUE){
            status = na65cs4_query_DSRatio(vi, chan, nDSChannelGrade, &val); 
            if (status >= 0)  sprintf(result,"%3d",val);         
            if (status < 0)  sprintf(result ,"<bus error>");     
            ResetTextBox (panel,DSCHPANEL_DS_RATIO_BOX, result);  
            }
                                           
            if (EvenChan != VI_TRUE){
            sprintf(result ,"--");     
            ResetTextBox (panel,DSCHPANEL_DS_RATIO_BOX, result);  
            }
            
            status = na65cs4_query_DSRefSrcMode(vi, chan, nDSChannelGrade, &pntr);       
            if (pntr == NA65CS4_EXT)  sprintf(result ,"EXT");     
            if (pntr == NA65CS4_INT)  sprintf(result ,"INT");        
            if (status < 0)  sprintf(result ,"<bus error>");           
            ResetTextBox (panel,DSCHPANEL_DS_REF_SOURCE_BOX, result);    


            status = na65cs4_query_DSTriggerSrc(vi, chan, nDSChannelGrade, &pntr);
            if (pntr == NA65CS4_INT)  sprintf(result ,"INT"); 
            if (pntr == NA65CS4_EXT)  sprintf(result ,"EXT");
            if (pntr == NA65CS4_BUS)  sprintf(result ,"BUS"); 
            if (pntr == NA65CS4_TTL_00)  sprintf(result ,"TTL0");
            if (pntr == NA65CS4_TTL_01)  sprintf(result ,"TTL1"); 
            if (pntr == NA65CS4_TTL_02)  sprintf(result ,"TTL2"); 
            if (pntr == NA65CS4_TTL_03)  sprintf(result ,"TTL3"); 
            if (pntr == NA65CS4_TTL_04)  sprintf(result ,"TTL4"); 
            if (pntr == NA65CS4_TTL_05)  sprintf(result ,"TTL5"); 
            if (pntr == NA65CS4_TTL_06)  sprintf(result ,"TTL6"); 
            if (pntr == NA65CS4_TTL_07)  sprintf(result ,"TTL7"); 
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel, DSCHPANEL_DS_TRIG_SOURCE_BOX, result); 


            status = na65cs4_query_DSTriggerSlope(vi, chan, nDSChannelGrade, &pntr);
            if (pntr == NA65CS4_POS)  sprintf(result ,"POS"); 
            if (pntr == NA65CS4_NEG)  sprintf(result ,"NEG");
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel, DSCHPANEL_DS_TRIG_SLOPE_BOX, result); 

            status = na65cs4_query_DSRotationMode(vi, chan, nDSChannelGrade, &pntr);
            if (pntr == NA65CS4_CONT)  sprintf(result ,"CONT"); 
            if (pntr == NA65CS4_STEP)  sprintf(result ,"STEP");
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel, DSCHPANEL_DS_ROT_MODE_BOX, result); 
              
            
            status = na65cs4_query_DSRotationRate (vi, chan, nDSChannelGrade, &num);
            sprintf(result,"%8.4f",num);
            if (status < 0)  sprintf(result ,"<bus error>");
            ResetTextBox (panel, DSCHPANEL_DS_ROT_RATE_BOX, result); 
              
            
            status = na65cs4_query_DSRotateStopAng (vi, chan, nDSChannelGrade, &num);
            sprintf(result,"%8.4f",num);
            if (status < 0)  sprintf(result ,"<bus error>");
            ResetTextBox (panel, DSCHPANEL_DS_ROT_STOP_ANG_BOX, result); 
              
            
            status = na65cs4_get_DSRotationComplete (vi, chan, nDSChannelGrade, &pntrTF);
            if (pntrTF == VI_TRUE)  sprintf(result ,"TRUE"); 
            if (pntrTF == VI_FALSE)  sprintf(result ,"FALSE");
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel, DSCHPANEL_DS_ROT_DONE_BOX, result); 
              
            
            break;
        }
    return 0;
}


int CVICALLBACK RD_REF_ALL (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
ViInt16 chanpntr = 0; 
ViInt16 chan = 1;                  
ViInt16 pntr = 0;                  
ViReal64 num = 0;                  
char result[64] = "<bus error>";   

    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel, REFCHPANEL_REF_CHAN,ATTR_CTRL_VAL, &chanpntr);
            chan = chanpntr;
            
            status = na65cs4_query_RefRelayIOState(vi, chan, &pntr); 
            if (pntr == NA65CS4_CLOSE)  sprintf(result ,"CLOSED"); 
            if (pntr == NA65CS4_OPEN)  sprintf(result ,"OPENED"); 
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel,REFCHPANEL_REF_IO_BOX, result);                                                                               
            if (status < 0) break;  
            
            status = na65cs4_query_RefVolt(vi, chan, &num);    
            if (status >= 0)  sprintf(result,"%8.4f",num);   
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel,REFCHPANEL_REF_VOLT_BOX, result);    
                                                          
            
            status = na65cs4_query_RefFreq(vi, chan, &num);                   
            if (status >= 0)  sprintf(result,"%8.4f",num);                    
            if (status < 0)  sprintf(result ,"<bus error>");                   
            ResetTextBox (panel,REFCHPANEL_REF_FRQ_BOX, result);              
                                                         
                         
            break;                                                                 
        }                                                                          
    return 0;                                                                      
}                        

                                                                                                  
int getStatus (int panel, int control, int event,                                                 
        void *callbackData, int eventData1, int eventData2)                                       
{                                                                                                 
    ViStatus status;                                                                              
    ViInt32 errCode;                                                                              
    char string[512];                                                                             
                                                                                                  
                                                                                                  
    switch (event) {                                                                              
        case EVENT_COMMIT:                                                                        
        status = na65cs4_error_query (vi,  &errCode, string );                              
        MessagePopup ("*ERR?", string);                                                       
                                                                                                  
            break;                                                                                
        case EVENT_RIGHT_CLICK:                                                                   
            break;                                                                                
    }                                                                                             
    return 0;                                                                                     
}                                                                                                 
                                                                                                  
int getSRHelp (int panel, int control, int event,                                                 
        void *callbackData, int eventData1, int eventData2)                                       
{                                                                                                 
    int retval; 
	PROCESS_INFORMATION pi;
	STARTUPINFOA si;
	
	//char sFilePath[] = """.\\ISS\\Config\\Help\\SR_NA65CS4\\65_CS4_A001_Rev_5.5.pdf"""; 
	//char sAppPath[] = """C:\\Program Files\\Adobe\\Reader 8.0\\Reader\\AcroRd32.exe""";
	//char  buffer[200];
	
    switch (event) {                                                                              
        case EVENT_COMMIT:                                                                        
                                      
	        //retval = system ("""C:\\Program Files\\Adobe\\Acrobat 6.0\\Reader\\AcroRd32"" ""C:\\program files\\vipert\\help\\SRHelp.pdf""");                        
	        
	       
	        //sprintf(buffer, "App Path = %s", sAppPath);
	        //sprintf(buffer, "File Path = %s", sFilePath);
        
        	ZeroMemory  (&si, sizeof (STARTUPINFO));

  			si.cb           =   sizeof (STARTUPINFO);
  			si.dwFlags      =   STARTF_USESHOWWINDOW;
  			si.wShowWindow  =   SW_SHOWNORMAL;
  			
			SetCurrentDirectory("""C:\\Program Files\\VIPERT\\ISS\\Bin\\"""); 
			//sFilePath[] = """C:\\Program Files\\VIPERT\\ISS\\Config\\Help\\SR_NA65CS4\\65_CS4_A001_Rev_5.5.pdf""";
	        CreateProcess(NULL,
	        			  """C:\\Program Files\\Adobe\\Reader 8.0\\Reader\\AcroRd32.exe"" ""..\\Config\\Help\\SR_NA65CS4\\65_CS4_A001_Rev_5.5.pdf""", 
	        			  NULL,
	        			  NULL,
	        			  FALSE,
	        			  0,  
	        			  NULL,
	        			  NULL,
	        			  &si, 
	        			  &pi); 
	            	
	        break;                                                                                
        
        case EVENT_RIGHT_CLICK:                                                                   
            break;                                                                                
    }                                                                                             
    return 0;                                                                                     
}                                                                                                     
                                                                                                    
                                                                                                  
                                                                                                  
int CVICALLBACK idn (int panel, int control, int event,                                           
        void *callbackData, int eventData1, int eventData2)                                       
{                                                                                                 
    ViStatus status;                                                                              
    ViInt16 testResult;                                                                           
    char testMessage[512]= "<bus error>";                                                         
                                                                                                  
    switch (event) {                                                                              
        case EVENT_COMMIT:                                                                        
            status = na65cs4_query_id(vi, testMessage);                                        
            MessagePopup ("*IDN?", testMessage);                                                  
                                                                                                  
            break;                                                                                
        case EVENT_RIGHT_CLICK:                                                                   
                                                                                                  
            break;                                                                                
    }                                                                                             
    return 0;                                                                                     
}                                                                                                 
                                                                                                  
int CVICALLBACK reset (int panel, int control, int event,                                         
        void *callbackData, int eventData1, int eventData2)                                       
{                                                                                                 
    switch (event)                                                                                
        {                                                                                         
        case EVENT_COMMIT:                                                                        
            status = na65cs4_reset(vi);                                                        
            break;                                                                                
        }                                                                                         
    return 0;                                                                                     
}                                                                                                 
                                                                                                  
                                                                                                  
int CVICALLBACK WrtDut (int panel, int control, int event,                                        
        void *callbackData, int eventData1, int eventData2)                                       
{                                                                                                 
char name[20];                                                                                    
char msg[255];                                                                                    
ViInt32 cnt;                                                                                                  
                                                                                                  
    switch (event)                                                                                
        {                                                                                         
        case EVENT_COMMIT:                                                                        
                                         
            GetCtrlAttribute (panel,na65CS_WRT_MSG,ATTR_CTRL_VAL,msg);                           
            status = na65cs4_WriteInstrData (vi,msg);                                          
            ResetTextBox (panelHandle, na65CS_RD_MSG, "");                                       
                                              
            break;                                                                                
        }                                                                                         
    return 0;                                                                                     
}                                                                                                 


int CVICALLBACK RdDut (int panel, int control, int event,                
        void *callbackData, int eventData1, int eventData2)               
{                                                                         
char msg[512];                                                            
ViInt32 cnt;                                                              
                                                                          
    switch (event)                                                        
        {                                                                 
        case EVENT_COMMIT:                                                
              
            status = na65cs4_ReadInstrData (vi,255, msg,&cnt);            
            ResetTextBox (panelHandle, na65CS_RD_MSG, msg);               
                                                                        
            break;                                                        
        }                                                                 
    return 0;                                                             
}                                                                         



int CVICALLBACK FreshStart (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    switch (event)
        {
        case EVENT_COMMIT:
            Connect();
            break;
        }
    return 0;
}



int CVICALLBACK SelectionTree (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    switch (event)
        {
        case EVENT_SELECTION_CHANGE:
            /* Check to make sure toggle on (i.e. selected) */
            if (eventData1 ==  1)
            {
                /* eventData2 contains itemIndex */
                /* Get the item tag value */
                if (GetTreeItemTag(panelHandle, na65CS_SELECTION_TREE, eventData2, &szBuffer[0]) >= 0)
                {
                    if (strstr(szBuffer, SD_SET1_TAG) != NULL)
                    {
                        /* Update label to show the first set of 8 channels */
                        nSDVisibleChannelGrp = 1;
                        SetSDChannelGroup();
                        DisplayPanel(sdchpanel);
                    }
                    else if (strstr(szBuffer, SD_SET2_TAG) != NULL)
                    {
                        /* Update label to show the second set of 8 channels */
                        nSDVisibleChannelGrp = 2;
                        SetSDChannelGroup();
                        DisplayPanel(sdchpanel);
                    }
                    else if (strstr(szBuffer, DSI_SET1_TAG) != NULL)
                    {
                        /* Update label to show the first set of 8 channels */
                        nDSVisibleChannelGrp = 1;
                        /* Instrument Grade */
                        nDSChannelGrade = 0;
                        SetDSChannelGradeAndGroup();
                        DisplayPanel(dschpanel);
                    }
                    else if (strstr(szBuffer, DSI_SET2_TAG) != NULL)
                    {
                        /* Update label to show the second set of 8 channels */
                        nDSVisibleChannelGrp = 2;
                        /* Instrument Grade */
                        nDSChannelGrade = 0;
                        SetDSChannelGradeAndGroup();
                        DisplayPanel(dschpanel);
                    }  
                    else if (strstr(szBuffer, DSO_SET1_TAG) != NULL)
                    {
                        /* Update label to show the first set of 8 channels */
                        nDSVisibleChannelGrp = 1;
                        /* Operational Grade */
                        nDSChannelGrade = 1;
                        SetDSChannelGradeAndGroup();
                        DisplayPanel(dschpanel);
                    }
                    else if (strstr(szBuffer, DSO_SET2_TAG) != NULL)
                    {
                        /* Update label to show the second set of 8 channels */
                        nDSVisibleChannelGrp = 2;
                        /* Operational Grade */
                        nDSChannelGrade = 1;
                        SetDSChannelGradeAndGroup();
                        DisplayPanel(dschpanel);
                    }                    
                    else if (strstr(szBuffer, REF_TAG) != NULL)
                    {
                        SetRefChannelGroup();
                        DisplayPanel(refchpanel);
                    }
                }
            }
            break;
        case EVENT_COMMIT:
            break;
        }
    return 0;
}


int CVICALLBACK DutFamilySelect (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    int nFamilyIndex;
    switch (event)
    {
        case EVENT_VAL_CHANGED:
            /* Get the value selected */
            GetCtrlIndex(panelHandle, na65CS_DUT_FAMILY_RING, &nFamilyIndex);
            /* eventData1 contains the row selected */
            Set65CS4Dut();
/* Only implement for the 65CS4 Dut
            if (nFamilyIndex == 0)
                Set65CS1Dut();
            else if (nFamilyIndex == 1)
                Set65CS2Dut();
            else if (nFamilyIndex == 2)
                Set65CS4Dut();
            else if (nFamilyIndex == 3)
                Set65CS4Dut();
*/
            break;
        case EVENT_COMMIT:

            break;
    }
    return 0;
}

int CVICALLBACK PartSpecSelect (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    int nPartIndex;
    switch (event)
    {
        case EVENT_VAL_CHANGED:
            /* Get the value selected */
            GetCtrlIndex(panelHandle, na65CS_PART_SPEC_RING, &nPartIndex);
            GetPartSpecInfo(nPartIndex);
            break;
        case EVENT_COMMIT:
            break;
        
    }
    return 0;
}

/* Utility Prototypes: */

void SetSDChannelGroup()
{
    int nCtrlDisabled;
    int i;
    
    if (nSDVisibleChannelGrp == 1)
    {
        /* Update Label */
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_1, ATTR_LABEL_TEXT , "SD CHANNEL 1");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_2, ATTR_LABEL_TEXT , "SD CHANNEL 2");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_3, ATTR_LABEL_TEXT , "SD CHANNEL 3");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_4, ATTR_LABEL_TEXT , "SD CHANNEL 4");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_5, ATTR_LABEL_TEXT , "SD CHANNEL 5");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_6, ATTR_LABEL_TEXT , "SD CHANNEL 6");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_7, ATTR_LABEL_TEXT , "SD CHANNEL 7");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_8, ATTR_LABEL_TEXT , "SD CHANNEL 8");
        
        /* Update Button */
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_1, ATTR_LABEL_TEXT , "RD CH01");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_2, ATTR_LABEL_TEXT , "RD CH02");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_3, ATTR_LABEL_TEXT , "RD CH03");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_4, ATTR_LABEL_TEXT , "RD CH04");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_5, ATTR_LABEL_TEXT , "RD CH05");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_6, ATTR_LABEL_TEXT , "RD CH06");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_7, ATTR_LABEL_TEXT , "RD CH07");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_8, ATTR_LABEL_TEXT , "RD CH08");
        
        /****************************************************/
        /* Handle disabling of controls that are not active */
        /****************************************************/
        for (i = 2; i <= 8; i++)
        {
            if (nMaxSDIChanCnt < i)
                /* Hide Channel i Controls */
                ShowDisableSDControls(i, 0, 0);
            else if (nSDIChanCnt < i)
                /* Disable Channel i Controls */
                ShowDisableSDControls(i, 1, 1);
            else
                /* Show and Enable Channel i Controls */
                ShowDisableSDControls(i, 1, 0);
        }

    }
    else if (nSDVisibleChannelGrp == 2)
    {
        /* Update Label */
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_1, ATTR_LABEL_TEXT , "SD CHANNEL 9");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_2, ATTR_LABEL_TEXT , "SD CHANNEL 10");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_3, ATTR_LABEL_TEXT , "SD CHANNEL 11");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_4, ATTR_LABEL_TEXT , "SD CHANNEL 12");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_5, ATTR_LABEL_TEXT , "SD CHANNEL 13");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_6, ATTR_LABEL_TEXT , "SD CHANNEL 14");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_7, ATTR_LABEL_TEXT , "SD CHANNEL 15");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_ANGLE_8, ATTR_LABEL_TEXT , "SD CHANNEL 16");
        
        /* Update Button */
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_1, ATTR_LABEL_TEXT , "RD CH09");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_2, ATTR_LABEL_TEXT , "RD CH10");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_3, ATTR_LABEL_TEXT , "RD CH11");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_4, ATTR_LABEL_TEXT , "RD CH12");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_5, ATTR_LABEL_TEXT , "RD CH13");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_6, ATTR_LABEL_TEXT , "RD CH14");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_7, ATTR_LABEL_TEXT , "RD CH15");
        SetCtrlAttribute (sdchpanel, SDCHPANEL_RD_8, ATTR_LABEL_TEXT , "RD CH16");
        
        /****************************************************/
        /* Handle disabling of controls that are not active */
        /****************************************************/
        for (i = 2; i <= 8; i++)
        {
            if (nMaxSDIChanCnt < (i+8))
                /* Hide Channel i Controls */
                ShowDisableSDControls(i, 0, 0);
            else if (nSDIChanCnt < (i+8))
                /* Disable Channel i Controls */
                ShowDisableSDControls(i, 1, 1);
            else
                /* Show and Enable Channel i Controls */
                ShowDisableSDControls(i, 1, 0);
        }
        
    }
}


void ShowDisableSDControls(int nCtrlNo, int nShow, int nDisable)
{
    if (nCtrlNo > 8)
        /* The maximum channel controls on panel is 8 */
        return;
        
    SetCtrlAttribute (sdchpanel, aSD_ANGLE[nCtrlNo-1], ATTR_VISIBLE, nShow);
  
    SetCtrlAttribute (sdchpanel, aSD_IO[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (sdchpanel, aSD_RD[nCtrlNo-1], ATTR_VISIBLE, nShow);
    
    SetCtrlAttribute (sdchpanel, aSD_ANGLE[nCtrlNo-1], ATTR_DIMMED, nDisable);
   
    SetCtrlAttribute (sdchpanel, aSD_IO[nCtrlNo-1], ATTR_DIMMED, nDisable);
    SetCtrlAttribute (sdchpanel, aSD_RD[nCtrlNo-1], ATTR_DIMMED, nDisable);
}


void SetDSChannelGradeAndGroup()
{
    int nCtrlDisabled;
    int i;

    if (nDSVisibleChannelGrp == 1)
    {
        if ((nDSChannelGrade == 1) || 
            ((nDSChannelGrade == 0) && (bSupportBothGrade == 0)))
        {
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_1, ATTR_LABEL_TEXT , "DS CHANNEL 1");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_2, ATTR_LABEL_TEXT , "DS CHANNEL 2");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_3, ATTR_LABEL_TEXT , "DS CHANNEL 3");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_4, ATTR_LABEL_TEXT , "DS CHANNEL 4");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_5, ATTR_LABEL_TEXT , "DS CHANNEL 5");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_6, ATTR_LABEL_TEXT , "DS CHANNEL 6");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_7, ATTR_LABEL_TEXT , "DS CHANNEL 7");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_8, ATTR_LABEL_TEXT , "DS CHANNEL 8");
        }
        else   /* Append 'H' to indicate high accuracy channel */
        {
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_1, ATTR_LABEL_TEXT , "DSH CHANNEL 1");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_2, ATTR_LABEL_TEXT , "DSH CHANNEL 2");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_3, ATTR_LABEL_TEXT , "DSH CHANNEL 3");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_4, ATTR_LABEL_TEXT , "DSH CHANNEL 4");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_5, ATTR_LABEL_TEXT , "DSH CHANNEL 5");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_6, ATTR_LABEL_TEXT , "DSH CHANNEL 6");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_7, ATTR_LABEL_TEXT , "DSH CHANNEL 7");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_8, ATTR_LABEL_TEXT , "DSH CHANNEL 8");
        }
            
        /* Update Button */
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_1, ATTR_LABEL_TEXT , "WRT CH01");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_2, ATTR_LABEL_TEXT , "WRT CH02");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_3, ATTR_LABEL_TEXT , "WRT CH03");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_4, ATTR_LABEL_TEXT , "WRT CH04");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_5, ATTR_LABEL_TEXT , "WRT CH05");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_6, ATTR_LABEL_TEXT , "WRT CH06");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_7, ATTR_LABEL_TEXT , "WRT CH07");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_8, ATTR_LABEL_TEXT , "WRT CH08");

        /****************************************************/
        /* Handle disabling of controls that are not active */
        /****************************************************/
        for (i = 2; i <= 8; i++)
        {
            if ( ((nDSChannelGrade == 0) && (nMaxDSIChanCnt < i)) ||
                 ((nDSChannelGrade == 1) && (nMaxDSOChanCnt < i)) )
                 /* Hide Channel i Controls */
                ShowDisableDSControls(i, 0, 0);
            else if ( ((nDSChannelGrade == 0) && (nDSIChanCnt < i)) ||
                      ((nDSChannelGrade == 1) && (nDSOChanCnt < i)) )
                /* Disable Channel i Controls */
                ShowDisableDSControls(i, 1, 1);
            else
                /* Show and Enable Channel i Controls */
                ShowDisableDSControls(i, 1, 0);
        }

    }
    else if (nDSVisibleChannelGrp == 2)
    {
        if ((nDSChannelGrade == 1) || 
            ((nDSChannelGrade == 0) && (bSupportBothGrade == 0)))
        {
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_1, ATTR_LABEL_TEXT , "DS CHANNEL 9");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_2, ATTR_LABEL_TEXT , "DS CHANNEL 10");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_3, ATTR_LABEL_TEXT , "DS CHANNEL 11");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_4, ATTR_LABEL_TEXT , "DS CHANNEL 12");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_5, ATTR_LABEL_TEXT , "DS CHANNEL 13");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_6, ATTR_LABEL_TEXT , "DS CHANNEL 14");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_7, ATTR_LABEL_TEXT , "DS CHANNEL 15");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_8, ATTR_LABEL_TEXT , "DS CHANNEL 16");
        }
        else   /* Append 'H' to indicate high accuracy channel */
        {
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_1, ATTR_LABEL_TEXT , "DSH CHANNEL 9");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_2, ATTR_LABEL_TEXT , "DSH CHANNEL 10");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_3, ATTR_LABEL_TEXT , "DSH CHANNEL 11");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_4, ATTR_LABEL_TEXT , "DSH CHANNEL 12");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_5, ATTR_LABEL_TEXT , "DSH CHANNEL 13");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_6, ATTR_LABEL_TEXT , "DSH CHANNEL 14");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_7, ATTR_LABEL_TEXT , "DSH CHANNEL 15");
            SetCtrlAttribute (dschpanel, DSCHPANEL_DS_ANGLE_8, ATTR_LABEL_TEXT , "DSH CHANNEL 16");
        }
            
        /* Update Button */
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_1, ATTR_LABEL_TEXT , "WRT CH09");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_2, ATTR_LABEL_TEXT , "WRT CH10");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_3, ATTR_LABEL_TEXT , "WRT CH11");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_4, ATTR_LABEL_TEXT , "WRT CH12");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_5, ATTR_LABEL_TEXT , "WRT CH13");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_6, ATTR_LABEL_TEXT , "WRT CH14");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_7, ATTR_LABEL_TEXT , "WRT CH15");
        SetCtrlAttribute (dschpanel, DSCHPANEL_WRT_8, ATTR_LABEL_TEXT , "WRT CH16");    

        /****************************************************/
        /* Handle disabling of controls that are not active */
        /****************************************************/
        for (i = 2; i <= 8; i++)
        {
            if ( ((nDSChannelGrade == 0) && (nMaxDSIChanCnt < (i+8))) ||
                 ((nDSChannelGrade == 1) && (nMaxDSOChanCnt < (i+8))) )
                 /* Hide Channel i Controls */
                ShowDisableDSControls(i, 0, 0);
            else if ( ((nDSChannelGrade == 0) && (nDSIChanCnt < (i+8))) ||
                      ((nDSChannelGrade == 1) && (nDSOChanCnt < (i+8))) )
                /* Disable Channel i Controls */
                ShowDisableDSControls(i, 1, 1);
            else
                /* Show and Enable Channel i Controls */
                ShowDisableDSControls(i, 1, 0);
        }

    }
}

void ShowDisableDSControls(int nCtrlNo, int nShow, int nDisable)
{
    if (nCtrlNo > 8)
        /* The maximum channel controls on panel is 8 */
        return;
        
    SetCtrlAttribute (dschpanel, aDS_ANGLE[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (dschpanel, aDS_IO[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (dschpanel, aDS_WRT[nCtrlNo-1], ATTR_VISIBLE, nShow);
    
    SetCtrlAttribute (dschpanel, aDS_ANGLE[nCtrlNo-1], ATTR_DIMMED, nDisable);
    SetCtrlAttribute (dschpanel, aDS_IO[nCtrlNo-1], ATTR_DIMMED, nDisable);
    SetCtrlAttribute (dschpanel, aDS_WRT[nCtrlNo-1], ATTR_DIMMED, nDisable);
}


void SetRefChannelGroup()
{
    int nCtrlDisabled;
    int i;

    /****************************************************/
    /* Handle disabling of controls that are not active */
    /****************************************************/
    
    for (i = 2; i <= 4; i++)
    {
        if (nMaxRefChanCnt < i)
            /* Hide Channel i Controls */
            ShowDisableRefControls(i, 0, 0);
        else if (nRefChanCnt < i)
            /* Disable Channel i Controls */
            ShowDisableRefControls(i, 1, 1);
        else
            /* Show and Enable Channel i Controls */
            ShowDisableSDControls(i, 1, 0);
    }

}

void ShowDisableRefControls(int nCtrlNo, int nShow, int nDisable)
{
    if (nCtrlNo > 4)
        /* The maximum channel controls on panel is 4 */
        return;
        
    SetCtrlAttribute (refchpanel, aREF_VOLT[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (refchpanel, aREF_FREQ[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (refchpanel, aREF_IO[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (refchpanel, aREF_WRITE[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (refchpanel, aREF_LBL[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (refchpanel, aREF_VOLT_LBL[nCtrlNo-1], ATTR_VISIBLE, nShow);
    SetCtrlAttribute (refchpanel, aREF_FREQ_LBL[nCtrlNo-1], ATTR_VISIBLE, nShow);

    SetCtrlAttribute (refchpanel, aREF_VOLT[nCtrlNo-1], ATTR_DIMMED, nDisable);
    SetCtrlAttribute (refchpanel, aREF_FREQ[nCtrlNo-1], ATTR_DIMMED, nDisable);
    SetCtrlAttribute (refchpanel, aREF_IO[nCtrlNo-1], ATTR_DIMMED, nDisable);
    SetCtrlAttribute (refchpanel, aREF_WRITE[nCtrlNo-1], ATTR_DIMMED, nDisable);

}



void Set65CS4Dut()
{
    char *ppParts = szPartSpec[0];
    
    /* Get the Model Information about the maximum channel supported */
    status = na65cs4_getModelInfo ("65CS4", &nMaxSDIChanCnt, &nMaxSDOChanCnt, &nMaxRefChanCnt, &nMaxDSIChanCnt, &nMaxDSOChanCnt);
    if (status != VI_SUCCESS)
    {
        //TODO Display error message
        return;
    }

    /* Determine if the Model supports both Instrument Grade and Operational Grade DS modules */
    if ((nMaxDSIChanCnt > 0) && (nMaxDSOChanCnt))
        bSupportBothGrade = 1;
    else
        bSupportBothGrade = 0;
    
    /* Get the different Part Numbers associated with the Model */
    status = na65cs4_getModelParts ("65CS4", MAX_PART_CNT, MAX_PART_LENGTH, &nPartSpecCnt, &ppParts);
    if (status != VI_SUCCESS)
    {
        //TODO Display error message
        return;
    }
    
    FillPartSpecChoices();

}


void FillPartSpecChoices()
{
    int i = 0;
    
    /* Clear the items from the list */
    ClearListCtrl(panelHandle, na65CS_PART_SPEC_RING);
   
    /* Insert the Part Specification items */
    for (i = 0; i < nPartSpecCnt; i++)
    {
        InsertListItem(panelHandle, na65CS_PART_SPEC_RING, -1, szPartSpec[i], 0);
    }
    
    if (nPartSpecCnt > 0)
    {
        /* Select first item and update the channel information for the first Part Number in the list */
        SetCtrlIndex(panelHandle, na65CS_PART_SPEC_RING, 0); 
        GetPartSpecInfo(0);
    }
}

/* Get the info about the Part selected - number of channels, etc. */
void GetPartSpecInfo(int nIndex)
{
    char szPartSelected[MAX_PART_LENGTH];
    
    if ((nIndex >= 0) && (nIndex < nPartSpecCnt))
    {
        status = na65cs4_getPartSpecInfo(szPartSpec[nIndex],
                                         &nSDIChanCnt,
                                         &nSDOChanCnt,
                                         &nDSIChanCnt,
                                         &nDSOChanCnt,
                                         &nRefChanCnt,
                                         &fMinRefFreq,
                                         &fMaxRefFreq,
                                         &fRef1PowerOutput,
                                         &fRef1MinVoltage,
                                         &fRef1MaxVoltage,
                                         &fRef2PowerOutput,
                                         &fRef2MinVoltage,
                                         &fRef2MaxVoltage,
                                         &nLanguage);
 
        if (status != VI_SUCCESS)
        {
            FillChannelInfo();
            //TODO Display error message
            return;
        }

        
        FillChannelInfo();
        FillChannelTreeInfo();
    }
}

void FillChannelInfo(void)
{
    sprintf(szBuffer, "%d", nSDIChanCnt);
    SetCtrlAttribute (panelHandle, na65CS_SD_CHAN_CNT, ATTR_CTRL_VAL , szBuffer);

    sprintf(szBuffer, "%d", nDSIChanCnt);
    SetCtrlAttribute (panelHandle, na65CS_DSH_CHAN_CNT, ATTR_CTRL_VAL , szBuffer);

    sprintf(szBuffer, "%d", nDSOChanCnt);
    SetCtrlAttribute (panelHandle, na65CS_DSO_CHAN_CNT, ATTR_CTRL_VAL , szBuffer);

    sprintf(szBuffer, "%d", nRefChanCnt);
    SetCtrlAttribute (panelHandle, na65CS_REF_CHAN_CNT, ATTR_CTRL_VAL , szBuffer);

}

void FillChannelTreeInfo(void)
{
    char buffer[10];
    int i;
    int MaxListItem = 1;
    /*ClearListCtrl (panelHandle, na65CS_SELECTION_TREE); */
    /* Clear tree control */
    DeleteListItem (panelHandle, na65CS_SELECTION_TREE, 0, -1); 
    
    PopulateSDNodes();    
    PopulateDSNodes();
    PopulateRefNodes();
    
    /* set chan rings to max chan */  
    ClearListCtrl(sdchpanel, SDCHPANEL_SD_CHAN);
    ClearListCtrl(dschpanel, DSCHPANEL_DS_CHAN);
    ClearListCtrl(refchpanel, REFCHPANEL_REF_CHAN);
    
    for (i = 16; i >= 1; i--) 
    {
     sprintf(buffer,"CH%2d",i);
     if (nSDIChanCnt >= i) InsertListItem(sdchpanel, SDCHPANEL_SD_CHAN,-1,buffer,i);
     if (nDSIChanCnt >= i) InsertListItem(dschpanel, DSCHPANEL_DS_CHAN,-1,buffer,i);  
     if (nRefChanCnt >= i) InsertListItem(refchpanel, REFCHPANEL_REF_CHAN,-1,buffer,i);  
    
    }
    
    GetNumListItems (sdchpanel, SDCHPANEL_SD_CHAN,&MaxListItem);
    if (nSDIChanCnt >= 1) SetCtrlAttribute (sdchpanel, SDCHPANEL_SD_CHAN, ATTR_CTRL_INDEX,MaxListItem - 1);      
    GetNumListItems (dschpanel, DSCHPANEL_DS_CHAN,&MaxListItem);  
    if (nDSIChanCnt >= 1) SetCtrlAttribute (dschpanel, DSCHPANEL_DS_CHAN, ATTR_CTRL_INDEX,MaxListItem - 1);  
    GetNumListItems (refchpanel, REFCHPANEL_REF_CHAN,&MaxListItem);  
    if (nRefChanCnt >= 1) SetCtrlAttribute (refchpanel, REFCHPANEL_REF_CHAN, ATTR_CTRL_INDEX,MaxListItem - 1);   
     
     
    if (nSDIChanCnt > 0)
    {
        /* Update label to show the first set of 8 channels */
        nSDVisibleChannelGrp = 1;
        SetSDChannelGroup();
        DisplayPanel(sdchpanel);
    }
    else if (nDSIChanCnt > 0)
    {
        /* Update label to show the first set of 8 channels */
        nDSVisibleChannelGrp = 1;
        /* Instrument Grade */
        nDSChannelGrade = 0;
        SetDSChannelGradeAndGroup();
        DisplayPanel(dschpanel);
    }
    else if (nDSOChanCnt > 0)
    {
        /* Update label to show the first set of 8 channels */
        nDSVisibleChannelGrp = 1;
        /* Operational Grade */
        nDSChannelGrade = 1;
        SetDSChannelGradeAndGroup();
        DisplayPanel(dschpanel);
    }
    else
    {
        SetRefChannelGroup();
        DisplayPanel(refchpanel);
    }
    
}


void PopulateSDNodes(void)
{ 
    int nLevel1Index;
    int nLevel2Index;
    
    if (nSDIChanCnt > 0)
    {
        /* Insert the "Measurement" Node */
        nLevel1Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_SIBLING, 0, VAL_LAST, "Measurement", SD_SET1_TAG, 0, 0);
        
        /* Insert the First Set of SD channels (1 - 8) */
        if (nSDIChanCnt < 8)
        {
            if (nSDIChanCnt == 1)
                strcpy(szFirstSetLabel, "Channel 1");
            else
                sprintf(szFirstSetLabel, "Channels 1 - %d", nSDIChanCnt);
        }
        else
        {
            strcpy(szFirstSetLabel, "Channels 1 - 8");
        }

        nLevel2Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel1Index, VAL_LAST, szFirstSetLabel, SD_SET1_TAG, 0, 0);

        /* Insert the Second Set of SD channels (9 - 16) */
        if (nSDIChanCnt > 8)
        {
            if (nSDIChanCnt == 9)
                strcpy(szSecondSetLabel, "Channel 9");
            else
                sprintf(szSecondSetLabel, "Channels 9 - %d", nSDIChanCnt);

            nLevel2Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel1Index, VAL_LAST, szSecondSetLabel, SD_SET2_TAG, 0, 0);
        }

    }
}

void PopulateDSNodes(void)
{ 
    int nLevel1Index;
    int nLevel2Index;
    int nLevel3Index;

    if ((nDSIChanCnt > 0) || (nDSOChanCnt > 0))
    {
        if (nDSIChanCnt > 0)
            /* Insert the "Stimulus" Node with DSI_SET1_TAG as the tag */
            nLevel1Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_SIBLING, 0, VAL_LAST, "Stimulus", DSI_SET1_TAG, 0, 0);
        else
            /* Insert the "Stimulus" Node with DSO_SET1_TAG as the tag */
            nLevel1Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_SIBLING, 0, VAL_LAST, "Stimulus", DSO_SET1_TAG, 0, 0);
    }   

    if (nDSIChanCnt > 0)
    {
        /* Insert the "Instrument" Node */
        nLevel2Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel1Index, VAL_LAST, "Instrument", DSI_SET1_TAG, 0, 0);
        /* Insert the First Set of SD channels (1 - 8) */
        if (nDSIChanCnt < 8)
        {
            if (nDSIChanCnt == 1)
                strcpy(szFirstSetLabel, "Channel 1");
            else
                sprintf(szFirstSetLabel, "Channels 1 - %d", nDSIChanCnt);
        }
        else
        {
            strcpy(szFirstSetLabel, "Channels 1 - 8");
        }

        nLevel3Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel2Index, VAL_LAST, szFirstSetLabel, DSI_SET1_TAG, 0, 0);

        /* Insert the Second Set of SD channels (9 - 16) */
        if (nDSIChanCnt > 8)
        {
            if (nDSIChanCnt == 9)
                strcpy(szSecondSetLabel, "Channel 9");
            else
                sprintf(szSecondSetLabel, "Channels 9 - %d", nDSIChanCnt);

            nLevel3Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel2Index, VAL_LAST, szSecondSetLabel, DSI_SET2_TAG, 0, 0);

        }
    }

    if (nDSOChanCnt > 0)
    {
        /* Insert the "Operational" Node */
        nLevel2Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel1Index, VAL_LAST, "Operational", DSI_SET1_TAG, 0, 0);
        /* Insert the First Set of SD channels (1 - 8) */
        if (nDSOChanCnt < 8)
        {
            if (nDSOChanCnt == 1)
                strcpy(szFirstSetLabel, "Channel 1");
            else
                sprintf(szFirstSetLabel, "Channels 1 - %d", nDSOChanCnt);
        }
        else
        {
            strcpy(szFirstSetLabel, "Channels 1 - 8");
        }

        nLevel3Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel2Index, VAL_LAST, szFirstSetLabel, DSO_SET1_TAG, 0, 0);

        /* Insert the Second Set of SD channels (9 - 16) */
        if (nDSOChanCnt > 8)
        {
            if (nDSOChanCnt == 9)
                strcpy(szSecondSetLabel, "Channel 9");
            else
                sprintf(szSecondSetLabel, "Channels 9 - %d", nDSOChanCnt);

            nLevel3Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel2Index, VAL_LAST, szSecondSetLabel, DSO_SET2_TAG, 0, 0);

        }
    }

}

void PopulateRefNodes(void)
{
    int nLevel1Index;
    int nLevel2Index;
    
    if (nRefChanCnt > 0)
    {
        /* Insert the "Reference" Node */
        nLevel1Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_SIBLING, 0, VAL_LAST, "Reference", REF_TAG, 0, 0);
        
        if (nRefChanCnt < 4)
        {
            if (nSDIChanCnt == 1)
                strcpy(szFirstSetLabel, "Channel 1");
            else
                sprintf(szFirstSetLabel, "Channels 1 - %d", nRefChanCnt);
        }
        else
        {
            strcpy(szFirstSetLabel, "Channels 1 - 4");
        }

        nLevel2Index = InsertTreeItem (panelHandle, na65CS_SELECTION_TREE, VAL_CHILD, nLevel1Index, VAL_LAST, szFirstSetLabel, REF_TAG, 0, 0);

    }
}

int GetChanFromName(char* name)
{
   char chstr[20]; 
   int nbytes = 0;
   ViInt16 chan = 1; 
   
   nbytes = StringLength (name);          
   CopyString(chstr,0,name,nbytes - 1,1); 
   chan = atoi(chstr);  
   
   return chan;

}
int CVICALLBACK SetREFio (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    char name[20]; 
    ViStatus status = 0;
    ViInt16 chan = 1;
    ViInt16 io = 0;

    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute(panel,control,ATTR_CONSTANT_NAME,name);
            chan = GetChanFromName(name); 
            
            GetCtrlVal (panel, control, &io); 
            status = na65cs4_config_REFRelayIOState(vi,chan,io); 
            
            break;
        }
    return 0;
}

int CVICALLBACK SetRef (int panel, int control, int event,                       
        void *callbackData, int eventData1, int eventData2)                      
{                                                                                        
    ViStatus status = 0;                                                         
    ViInt16 chan = 1;  
    ViInt16 chanpntr = 0;   
    char name[20];                                                               
    int CntrlStyle = 0;                                                          
    ViReal64 volt = 0,freq = 0;                                                                
                                                                                 
    switch (event)                                                               
        {                                                                        
        case EVENT_COMMIT:                                                       
            GetCtrlAttribute (panel , control,ATTR_CONSTANT_NAME,name);    
            chan = GetChanFromName(name); 
            chanpntr = chan - 1;
            
            GetCtrlVal (panel, aREF_FREQ[chanpntr], &freq); 
            status = na65cs4_config_REFFreq (vi,chan,freq);
            GetCtrlVal (panel, aREF_VOLT[chanpntr], &volt); 
            status = na65cs4_config_REFVolt (vi,chan,volt);                                                                      
                                                                                 
            GetCtrlAttribute (panel , control,ATTR_CTRL_STYLE, &CntrlStyle);     
            if (CntrlStyle == CTRL_NUMERIC) FakeKeystroke(VAL_END_VKEY);         
                                                                                 
            break;                                                               
        }                                                                        
    return 0;                                                                    
}                                                                                

int CVICALLBACK SetSDio (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    
    ViInt16 chan = 1;     
    ViInt16 chanpntr = 0; 
    char name[20];        
    ViInt16 io = 0; 
    
    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel , control,ATTR_CONSTANT_NAME,name);   
            chan = GetChanFromName(name);   
            chanpntr = chan - 1;                                                                          
            GetCtrlVal (panel, control, &io);                       
            status = na65cs4_config_SDRelayIOState(vi,chan,nSDChannelGrade,io); 
            break;
        }
    return 0;
}

int CVICALLBACK RdSD (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    ViInt16 mode; 
    ViInt16 chan = 1;     
    ViInt16 chanpntr = 0; 
    char name[20];  
    ViReal64 num = 0;
    char result[64]= "";
    
    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel , control,ATTR_CONSTANT_NAME,name);   
            chan = GetChanFromName(name);   
            chanpntr = chan - 1;                                                                          
            
            status = na65cs4_query_SDAngle(vi,chan,nSDChannelGrade,&num);
           
            if (status >= 0)  sprintf(result,"%8.4f",num);   
            if (status < 0)  sprintf(result ,"<bus error>"); 
            ResetTextBox (panel,aSD_ANGLE[chanpntr], result); 
            
            break;
        }
    return 0;
}

int CVICALLBACK SetDSio (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    ViInt16 chan = 1;     
    ViInt16 chanpntr = 0; 
    char name[20];        
    ViInt16 io = 0; 
    
    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel , control,ATTR_CONSTANT_NAME,name);   
            chan = GetChanFromName(name);   
            chanpntr = chan - 1;                                                                          
            GetCtrlVal (panel, control, &io);                       
            status = na65cs4_config_DSRelayIOState(vi,chan, nDSChannelGrade,io); 
            break;
        }
    return 0;   
    
}

int CVICALLBACK WrtDS (int panel, int control, int event,
        void *callbackData, int eventData1, int eventData2)
{
    ViInt16 chan = 1;     
    ViInt16 chanpntr = 0; 
    char name[20];     
    ViReal64 angle = 0;
    int style = 0; 
    
    switch (event)
        {
        case EVENT_COMMIT:
            GetCtrlAttribute (panel , control,ATTR_CONSTANT_NAME,name);   
            chan = GetChanFromName(name);   
            chanpntr = chan - 1; 
            GetCtrlVal (panel, aDS_ANGLE[chanpntr], &angle);  
        
            status = na65cs4_config_DSAngle(vi, chan, nDSChannelGrade, angle);
        
            GetCtrlAttribute (panel , control,ATTR_CTRL_STYLE, &style);  
            if (style == CTRL_NUMERIC) FakeKeystroke(VAL_END_VKEY); 

            break;
        }
    return 0;
}

int CVICALLBACK Atlas (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	switch (event)
	{
        case EVENT_COMMIT: 
			if (atlasHandle == 0)
			{
            atlasHandle = LoadPanel (panelHandle, "atlas.uir", 1);
			 
            DisplayPanel (atlasHandle);
			SetPanelPos (atlasHandle, VAL_AUTO_CENTER, VAL_AUTO_CENTER);
			}
			else
			{
				SetPanelAttribute(atlasHandle, ATTR_VISIBLE,1);
				SetPanelAttribute(atlasHandle, ATTR_ZPLANE_POSITION,0);
			}
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
                                                                                                                                              
            break;                                                                                                                            
    }                                                                                                                                         
    return 0;
} 


int CVICALLBACK Atlas_Close (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	switch (event)
	{
        case EVENT_COMMIT:                                                                                                                    
            //DiscardPanel(atlasHandle);
			ResetTextBox(atlasHandle, panAtlas_txtATLAS,"");
			SetPanelAttribute(atlasHandle, ATTR_VISIBLE,0);
            break;                                                                                                                            
        case EVENT_RIGHT_CLICK:                                                                                                               
                                                                                                                                              
            break;                                                                                                                            
    }                                                                                                                                         
    return 0;                                                                                                                                 
}    

     
int CVICALLBACK Generate (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	int item = 0;
	char* chMessage;
		
	switch (event)
	{
		case EVENT_COMMIT:
			
			chMessage = malloc (512);
			strcpy(chMessage,""); //clear memory
			GetPanelAttribute (sdchpanel, ATTR_ZPLANE_POSITION, &item);
			if (item == 1)
			{
				//Measurement Mode
				GenMeasAtlas(chMessage);
			}
			else
			{
				GetPanelAttribute (dschpanel, ATTR_ZPLANE_POSITION, &item);
				if (item == 1)
				{
					//Simulate Mode
					GenSimAtlas(chMessage);
				}
				
				//refchpanel; 
			}
			
			SetCtrlVal(atlasHandle, panAtlas_txtATLAS, chMessage);
			
			if (chMessage)
				free (chMessage);
			
			break;
	}
	
	return 0;
}

/*****************************************************************************/
// Atlas  Generation Functions
void GenMeasAtlas(char* chMessage)
{
	int index = -1, nType = -1, nChan = 0, i = 0;
	short nRatio = 0;
	char sTempStr[100] = "";
	int nIO[8] = {SDCHPANEL_SD_IO_1, SDCHPANEL_SD_IO_2, SDCHPANEL_SD_IO_3, SDCHPANEL_SD_IO_4,
				  SDCHPANEL_SD_IO_5, SDCHPANEL_SD_IO_6, SDCHPANEL_SD_IO_7, SDCHPANEL_SD_IO_8};
	
	 strcpy(chMessage,"MEASURE, (ANGLE");
	 
	 // Add "-RATE" if measuring Angle-Rate
	 
	 // Determine Mode
	 GetCtrlIndex(sdchpanel, SDCHPANEL_SD_MODE, &nType);
	 if (nType == 0)
		 strcat(chMessage," INTO 'MEASUREMENT'), RESOLVER, \n");
	 else if (nType == 1)
		 strcat(chMessage," INTO 'MEASUREMENT'), SYNCHRO, \n");
	 else
		 return;

	 //Angle
	 strcat(chMessage,"ANGLE MAX 359.99 DEG, \n");
	 
	 for ( i = 0; i < 8; i++)
	 {
		 GetCtrlIndex(sdchpanel, nIO[i], &index);
		 
		 if (index == 1)
		 {
			 nChan ++;
		 }
	 }
	 
	 //Speed-Ratio
	 if (nType == 1 && nChan >1)
	 {
		 GetCtrlVal(sdchpanel, SDCHPANEL_SD_RATIO, &nRatio);
		 Fmt(sTempStr, "SPEED-RATIO %i V,\n",nRatio);
		 strcat(chMessage, sTempStr);		 
	 }
		 
	 //Connections
	 strcat(chMessage, "CNX $\n");	 

}

void GenSimAtlas(char* chMessage)
{
	int index = -1, nType = -1, nChan = 0, i = 0;
	double dRefVolt = 0.0, dVolt = 0.0, dFreq = 0.0, dAngle = 0.0;
	short nRatio = 0;
	int nRefSourIndex= -1;
	char sTempStr[100] = "";
	int nIO[8] = {DSCHPANEL_DS_IO_1, DSCHPANEL_DS_IO_2, DSCHPANEL_DS_IO_3, DSCHPANEL_DS_IO_4,
				  DSCHPANEL_DS_IO_5, DSCHPANEL_DS_IO_6, DSCHPANEL_DS_IO_7, DSCHPANEL_DS_IO_8};
	
	// Determine Mode 
	GetCtrlIndex(dschpanel, DSCHPANEL_DS_MODE, &nType);
	 if (nType == 0)
	 	 strcpy(chMessage,"APPLY, RESOLVER,\n");
	 else if (nType == 1)
	 	 strcpy(chMessage,"APPLY, SYNCHRO,\n");
	 else
		 return;
	 
	 //Angle
	 for ( i = 0; i < 8; i++)
	 {
		 GetCtrlIndex(dschpanel, nIO[i], &index);
		 
		 if (index == 1)
		 {
			 GetCtrlVal(dschpanel, DSCHPANEL_DS_ANGLE_1 - i, &dAngle);
			 Fmt(sTempStr, "ANGLE %f[p2] DEG,\n",dAngle);
			 strcat(chMessage, sTempStr);
			 nChan ++;
		 }
	 }
	 
	 //Voltage
	 GetCtrlVal(dschpanel, DSCHPANEL_DS_LTL, &dVolt);
	 Fmt(sTempStr, "VOLTAGE %f[p2] V,\n",dVolt);
	 strcat(chMessage, sTempStr);
	 
	 //Frequency   
	 GetCtrlVal(refchpanel, REFCHPANEL_REF_FREQ_1, &dFreq);
	 Fmt(sTempStr, "FREQ %f[p0] HZ,\n",dFreq);
	 strcat(chMessage, sTempStr);
	 
	 
	 if (nType == 1)	//(Synchro) 
	 {
		 //Voltage Reference
		 GetCtrlVal(dschpanel, DSCHPANEL_DS_REF, &dRefVolt);
		 Fmt(sTempStr, "VOLTAGE-REF %f[p1] V,\n",dRefVolt);
		 strcat(chMessage, sTempStr);
	 
	 	 //Ref Source
		 GetCtrlIndex(dschpanel, DSCHPANEL_DS_REF_SOURCE, &nRefSourIndex);
		 if (nRefSourIndex == 0)
			 strcat(chMessage, "REF-SOURCE INT,\n");
		 else
			 strcat(chMessage, "REF-SOURCE EXT,\n");
	 
	 }
	 
	 //Speed-Ratio
	 if (nChan >1)
	 {
		 GetCtrlVal(dschpanel, DSCHPANEL_DS_RATIO, &nRatio);
		 Fmt(sTempStr, "SPEED-RATIO %i V,\n",nRatio);
		 strcat(chMessage, sTempStr);		 
	 }
		 
	 //Connections
	 strcat(chMessage, "CNX $\n");

}
