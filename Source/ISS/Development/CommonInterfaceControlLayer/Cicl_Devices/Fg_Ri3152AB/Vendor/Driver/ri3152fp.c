/*= Racal Instruments 3152 Arbitrary Waveform Generator ===================*/
/* VXI Plug&Play WIN Framework Instrument Driver                           */
/* Original Release: 12/19/95                                              */
/* By: S. Javed                                                            */
/* Instrument Driver Revision 6.1                                          */
/* VXI Plug&Play WIN Framework Revision: 3.0                               */
/* 3152 Minimum Hardware Revision:                                         */
/* 3152 Minimum Firmware Revision:                                         */
/* Modification History: None   										   */
/*	Revision	Date		Comment									By	   */
/*	A			7/13/01		Original Release						JDT    */
/*																		   */
/*	B			5/06/03		Updated to enable PLL feature and       JDT    */
/*							added "Browse" button to download 		       */
/*							WaveCAD waveform file					       */
/*=========================================================================*/

#include <userint.h>
#include <ansi_c.h>
#include <analysis.h>
#include <utility.h>
#include <math.h>
#include <visa.h>
#include "ri3152a.h"
#include "ri3152fp.h"



/* constants for control selections */
#define STD 0
#define ARB 1
#define SWE 2

#define CONT  0
#define TRIG  1
#define GATED 2
#define BURST 3


#define NO 0
#define YES 1

#define RI3152A_MANF_ID              ((ViAttrState) 0xFFB)
#define RI3152_MODEL_CODE			((ViAttrState) 3150)
#define RI3152A_MODEL_CODE          ((ViAttrState) 3152)


/* 
 * The following line is commented out so that the
 * code talks to the instrument (and includes VISA)
 */
/***************************************
#define DEMONSTRATION_ONLY		1
****************************************/

static ViSession vi, rm_handle;
static char active_model[10];
static ViStatus error;
static int demo_mode = 0;
static int hdl, hdl1, hdl2, hdl3, hdl4, hdl5, hdl6, hdl7;
int close_connect_panel = 0;
int close_std_panel = 0;
int close_arb_panel = 0;
int close_delete_panel = 0;
int close_sine_panel = 0;
int close_triangle_panel = 0;
int close_square_panel = 0;
int close_pulse_panel = 0;
int close_ramp_panel = 0;
int close_sinc_panel = 0;
int close_gauss_panel = 0;
int close_exponential_panel = 0;
int close_dc_panel = 0;
int close_am_panel = 0;
int close_trigger_panel = 0;
int close_burst_panel = 0;
int close_about_panel = 0;
int close_params_panel = 0;
int close_sync_panel = 0;
int close_wavecad_panel = 0;
int close_pll_panel = 0;
int close_sweep_panel = 0;

int number_of_burst_cycles = 1;
int sync_pos = 2;
int sync_widt = 1;
int sync_src = 0;
int current_trigger_mode = RI3152A_MODE_CONT;

int handler;

/*--------------------------- User Interface Function Protocols ------------------------------*/

int keep_displaying_standard ( int handler );
int keep_displaying_arbitrary ( int handler );
int keep_displaying_delete ( int handler );
int keep_displaying_sine (int handler );
int keep_displaying_triangle (int handler );
int keep_displaying_square (int handler );
int keep_displaying_pulse (int handler );
int keep_displaying_ramp (int handler );
int keep_displaying_gauss (int handler );
int keep_displaying_exponential (int handler );
int keep_displaying_dc (int handler );
int keep_displaying_am (int handler );
int keep_displaying_trigger (int handler);
int keep_displaying_burst (int handler);
int keep_displaying_connect (int handler);
int keep_displaying_about (int handler);
int keep_displaying_params (int handler);
int keep_displaying_sync (int handler);
int keep_displaying_wavecad (int handler);
int keep_displaying_pll (int handler);
int keep_displaying_sweep (int handler);

int autoconnect( void );
int is_315X(char *descriptor);
int validate_params(double amplitude, double offset);

void ReportError( char *title, char *action, ViStatus error);
int run_demo_mode_query(void);

/*--------------------------------------------------------------------------------------------*/



main ()
{
	static short slot;
	ViStatus error;

	if ( ! autoconnect() )
		{
		hdl = LoadPanel ( 0, "ri3152fp.uir", PANEL);
		SetPanelAttribute (hdl, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl, ATTR_TOP, VAL_AUTO_CENTER);
		slot = 0;
		error = viGetAttribute (vi, VI_ATTR_SLOT, &slot);
		DisplayPanel (hdl);
		SetCtrlVal (hdl, PANEL_LAMP, 1);
		SetCtrlAttribute (hdl, PANEL_PLL, ATTR_DIMMED, 0);

		if (demo_mode)
			{
			SetCtrlVal(hdl, PANEL_SLOT, 12);
			SetCtrlVal(hdl, PANEL_MODEL, "3152A");
			}
		else
			{
			SetCtrlVal( hdl, PANEL_SLOT, slot);
			SetCtrlVal( hdl, PANEL_MODEL, active_model );
			}

		RunUserInterface ();
		}
}




int reset (int panel, int control, int event, void *callbackData, int eventdata1,
                   int eventdata2)
{
	ViStatus error;

	error = 0;
	
	if (event == EVENT_COMMIT )
		{
#ifndef DEMONSTRATION_ONLY
		if (!demo_mode)	
			error = ri3152a_reset(vi);
#endif
		if (error < 0)
			{
			ReportError("Reset Error", "resetting the 3152", error);
			return(0);
			}
			
		SetCtrlVal (hdl, PANEL_SELECT_MODE, 0);
		SetCtrlVal (hdl, PANEL_FILTER, 0);
		SetCtrlVal (hdl, PANEL_WAVEFORM, 0);

#ifndef DEMONSTRATION_ONLY
		if (!demo_mode)
			error = ri3152a_output(vi, RI3152A_OUTPUT_OFF);
#endif

		if (error < 0)
			{
			ReportError("Output Off Error", "turning off the output", error);
			return(0);
			}
		
		SetCtrlVal (hdl, PANEL_OUTPUT_SWITCH, 0);

#ifndef DEMONSTRATION_ONLY
		if (!demo_mode)
			error = ri3152a_change_mode(vi, RI3152A_MODE_NORMAL);
#endif

		if (error < 0)
			{
			ReportError("Normal mode Error", "turning mode to Normal", error);
			return(0);
			}
		
		SetCtrlVal (hdl, PANEL_MODE_SWITCH, 0);
        }

	return (0);
}

int about (int panel, int control, int event, void *callbackData, int eventdata1,
           int eventdata2)
{
	ViStatus error;
	ViChar revision[100];
	ViChar driver_rev[100];

	if (event == EVENT_COMMIT )
		{
		hdl4 = LoadPanel ( 0, "ri3152fp.uir", ABOUT);
		SetPanelAttribute (hdl4, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl4, ATTR_TOP, VAL_AUTO_CENTER);
		DisplayPanel (hdl4);
		SetPanelAttribute (hdl, ATTR_DIMMED, 1);
              
		handler = hdl4;
		error = 0;

		
		if ( ! demo_mode )
			{
#ifndef DEMONSTRATION_ONLY
			error = ri3152a_revision_query (vi, driver_rev, revision);
#else
			error = 0;
#endif
			}
		else
			strcpy(revision, "Demo");

		if (error < 0)
			{
			ReportError ("Revision Query Error", "reading firmware revision", error);
			strcpy(revision, "Error reading revision");
			}
	
		SetCtrlVal(hdl4, ABOUT_FWREV, revision);
		
		
		keep_displaying_about (handler);
		SetPanelAttribute (hdl,ATTR_DIMMED, 0);
		DiscardPanel (hdl4);
        }

	return (0);
}


int quit (int panel, int control, int event, void *callbackData, int eventdata1,
                   int eventdata2)
{

	if (event == EVENT_COMMIT )
		{
#ifndef DEMONSTRATION_ONLY
		SetWaitCursor (1);
		if (!demo_mode)
			if ( (error = ri3152a_close(vi)) < 0)
				ReportError("Closing the 3152A", "closing the 3152a", error);
		SetWaitCursor (0);
#endif
		SetCtrlVal (hdl, PANEL_LAMP, 0);
		QuitUserInterface(0);
		}

	return (0);
}


int output_switch( int panel, int control, int event, void *callbackData, int eventdata1,
                   int eventdata2)
{
	int output_sw;
	ViStatus error;
     
	error = 0;

	GetCtrlVal (panel, PANEL_OUTPUT_SWITCH, &output_sw);
	if (event == EVENT_VAL_CHANGED)
		{
#ifndef DEMONSTRATION_ONLY
		if ( ! demo_mode )
			error = ri3152a_output (vi, output_sw);
#endif

		if ( error < 0)
			ReportError("output error", "changing output on/off state", error);
		}

	return (0);
}


int mode_switch( int panel, int control, int event, void *callbackData, int eventdata1,
                   int eventdata2)
{
	int mode_sw;
	ViStatus error;
     
	error = 0;

	GetCtrlVal (panel, PANEL_MODE_SWITCH, &mode_sw);
	if (event == EVENT_VAL_CHANGED)
		{
#ifndef DEMONSTRATION_ONLY
		if ( ! demo_mode )
			error = ri3152a_change_mode (vi, mode_sw);
#endif

		if ( error < 0)
			ReportError("output error", "changing mode", error);
		}

	return (0);
}



int select_mode (int panel, int control, int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int signal_mode;

	SetActiveCtrl (panel, PANEL_QUIT);  /* getting focus on another panel everytime checking event value */

	error = 0;
	
	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, PANEL_SELECT_MODE, &signal_mode);
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_operating_mode (vi, signal_mode);

		if ( error < 0)
			ReportError ("Select Mode Error", "changing the operating mode", error);
#endif

		switch ( signal_mode )
			{
            case CONT:		break;
            
            case TRIG:
            case BURST:		current_trigger_mode = signal_mode;
	                        hdl4 = LoadPanel ( 0, "ri3152fp.uir", TRIGGER);
    	                    SetPanelAttribute (hdl4, ATTR_LEFT, VAL_AUTO_CENTER);
        	                SetPanelAttribute (hdl4, ATTR_TOP, VAL_AUTO_CENTER);
            	            DisplayPanel (hdl4);
                	        SetPanelAttribute (hdl, ATTR_DIMMED, 1);
							handler = hdl4;
							keep_displaying_trigger (handler);
							SetPanelAttribute (hdl,ATTR_DIMMED, 0);
							DiscardPanel (hdl4);
							break;

            case GATED:		break;
			}
		}

	return (0);
}


int select_waveform (int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int waveform_mode;

	GetCtrlVal (panel, PANEL_WAVEFORM, &waveform_mode);
	SetActiveCtrl (panel, PANEL_QUIT);  /* getting focus on another panel everytime checking event value*/

	error = 0;
	
	if (event == EVENT_COMMIT) 
	{
		
#ifndef DEMONSTRATION_ONLY
		if (! demo_mode )
			error = ri3152a_select_waveform_mode (vi, waveform_mode);
		if ( error < 0)
			ReportError("Select Waveform Error", "selecting the waveform mode", error);
#endif
		switch ( waveform_mode )
			{
            case STD:	
            			hdl1 = LoadPanel ( 0, "ri3152fp.uir", STANDARD);
						SetPanelAttribute (hdl1, ATTR_LEFT, VAL_AUTO_CENTER);
						SetPanelAttribute (hdl1, ATTR_TOP, VAL_AUTO_CENTER);
						DisplayPanel (hdl1);
						SetPanelAttribute (hdl, ATTR_DIMMED, 1);
						handler = hdl1;
						break;

            case ARB:	
            			hdl2 = LoadPanel (0, "ri3152fp.uir", ARBITRARY);
						SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
						SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
						DisplayPanel (hdl2);
						SetPanelAttribute (hdl, ATTR_DIMMED, 1);
						handler = hdl2;
						keep_displaying_arbitrary (handler);
						SetPanelAttribute (hdl, ATTR_DIMMED, 0);
						DiscardPanel (hdl2);
						break;

            case SWE:	
            			hdl7 = LoadPanel (0, "ri3152fp.uir", SWEEP);
//            			Delay (5);
						SetPanelAttribute (hdl7, ATTR_LEFT, VAL_AUTO_CENTER);
						SetPanelAttribute (hdl7, ATTR_TOP, VAL_AUTO_CENTER);
						DisplayPanel (hdl7);
						SetPanelAttribute (hdl, ATTR_DIMMED, 1);
						handler = hdl7;
						keep_displaying_sweep (handler);
						SetPanelAttribute (hdl, ATTR_DIMMED, 0);
						DiscardPanel (hdl7);
						break;
			}
		}


	return (0);
}


int select_amp_modulation (int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	SetActiveCtrl (hdl, PANEL_QUIT);  /* getting focus on another panel everytime checking event value*/

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		hdl3 = LoadPanel ( 0, "ri3152fp.uir", AM);
		SetPanelAttribute (hdl3, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl3, ATTR_TOP, VAL_AUTO_CENTER);
		DisplayPanel (hdl3);
		SetPanelAttribute (hdl, ATTR_DIMMED, 1);
		handler = hdl3;
		keep_displaying_am (handler);
		SetPanelAttribute (hdl, ATTR_DIMMED, 0);
		DiscardPanel (hdl3);
		SetCtrlVal (hdl, PANEL_AMP_MODULATION, 0);
		}

    return (0);
}



int sync_switch (int panel, int control, int event, void *callbackData, int eventData1,
                          int eventData2)
{
	int switch_on_off;
	ViStatus error;
	
	
	SetActiveCtrl (panel, PANEL_QUIT);  /* getting focus on another panel everytime checking event value */

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal(hdl, PANEL_SYNC_SWITCH, &switch_on_off);
		if (switch_on_off)
			{
	        hdl4 = LoadPanel ( 0, "ri3152fp.uir", SYNC);
			SetPanelAttribute (hdl4, ATTR_LEFT, VAL_AUTO_CENTER);
			SetPanelAttribute (hdl4, ATTR_TOP, VAL_AUTO_CENTER);
			SetCtrlVal (hdl4, SYNC_SYNCPOS, sync_pos);
			SetCtrlVal (hdl4, SYNC_SYNCWIDT, sync_widt);
			SetCtrlVal (hdl4, SYNC_SYNCSRC, sync_src);
			DisplayPanel (hdl4);
			SetPanelAttribute (hdl, ATTR_DIMMED, 1);

			handler = hdl4;
			keep_displaying_sync (handler);
			SetPanelAttribute (hdl,ATTR_DIMMED, 0);
			DiscardPanel (hdl4);
			}
		else
			{
			if ( !demo_mode )
				{
#ifndef DEMONSTRATION_ONLY
				error = ri3152a_output_sync (vi, RI3152A_SYNC_OFF, sync_pos, sync_widt, 1);
				if (error < 0)
					ReportError("Turn SYNC Off Error", "turning off sync pulse", error);
#else
				error = 0;
#endif
				
				}
			}
		}

	return (0);
}





int wavecad_file_support (int panel, int control, int event, void *callbackData, int eventData1,
                          int eventData2)
{
	SetActiveCtrl (panel, PANEL_QUIT);  /* getting focus on another panel everytime checking event value */

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		hdl4 = LoadPanel ( 0, "ri3152fp.uir", WAVECAD);
		SetPanelAttribute (hdl4, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl4, ATTR_TOP, VAL_AUTO_CENTER);
		SetCtrlVal (hdl4, WAVECAD_FILENAME, "C:\\Program Files\\WaveCad\\Rac3152a\\sample.wav");
		DisplayPanel (hdl4);
		SetPanelAttribute (hdl, ATTR_DIMMED, 1);

		handler = hdl4;
		keep_displaying_wavecad (handler);
		SetPanelAttribute (hdl,ATTR_DIMMED, 0);
		DiscardPanel (hdl4);
		}

	return (0);
}


/*=========================================================================*/
/* Function: Choose WaveCAD File										   */
/* Purpose:  This function loads an WaveCAD file						   */
/* containing waveform information for a User Waveform.					   */
/*=========================================================================*/
int choose_wavecad_file (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	char buffer[256], filename[256];

	switch (event) {
		case EVENT_COMMIT:
			error = FileSelectPopup ("", "*.wav",
									 "WaveCAD Waveform Files (*.wav)",
									 "Browse for WaveCAD Waveform File",
									 VAL_LOAD_BUTTON, 0, 0, 1, 0, buffer);

			if (error==1) { /* File was selected */
				error = DefaultCtrl (panel, WAVECAD_FILENAME);
				error = SetCtrlVal (panel, WAVECAD_FILENAME, buffer);
			}
			break;
		case EVENT_RIGHT_CLICK:

			break;
	}
	return 0;
}


/*=========================================================================*/
/* Function: Choose ASCII File										       */
/* Purpose:  This function loads an ASCII file							   */
/*=========================================================================*/
int choose_ascii_file (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	char buffer[256], filename[256];

	switch (event) {
		case EVENT_COMMIT:
			error = FileSelectPopup ("", "*.asc",
									 "*.*",
									 "Browse for ASCII Data File",
									 VAL_LOAD_BUTTON, 0, 0, 1, 0, buffer);

			if (error==1) { /* File was selected */
				error = DefaultCtrl (panel, ARBITRARY_FILENAME);
				error = SetCtrlVal (panel, ARBITRARY_FILENAME, buffer);
			}
			break;
		case EVENT_RIGHT_CLICK:

			break;
	}
	return 0;
}




int apply_wavecad_file (int panel, int control,  int event, void *callbackData, int eventData1,
                      	int eventData2)
{
	ViStatus error;
    int segment_number;
    int clock_source;
	double sampling_frequency;
    double amplitude;
    double offset;
	char filename[256], buffer[256];
	char *file_ext, *cp;


	SetActiveCtrl (panel, WAVECAD_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, WAVECAD_FILENAME, filename);
		
		/*
		 * get the file name extension, check which type of file
		 * we are dealing with
		 */
		cp = filename;
		while (*cp = tolower(*cp))
			++cp;
			
		file_ext = filename + strlen(filename) - 1;
		
		while ((file_ext > filename) && (*file_ext != '.'))
			--file_ext;

		if ( !strcmp(file_ext, ".wav"))
			{
			GetCtrlVal (panel, WAVECAD_SEGMENT_NUM, &segment_number);
			GetCtrlVal (panel, WAVECAD_CLOCK, &sampling_frequency);
			GetCtrlVal (panel, WAVECAD_AMPLITUDE, &amplitude);
			GetCtrlVal (panel, WAVECAD_OFFSET, &offset);
			GetCtrlVal (panel, WAVECAD_CLOCK_SOURCE, &clock_source);

			if (validate_params(amplitude, offset))
				return(0);

			error = 0;
			
			if ( !demo_mode )
				{
#ifndef DEMONSTRATION_ONLY
				error = ri3152a_output (vi, VI_TRUE);
				if (error < 0)
				{
					ReportError("Output Error", "setting output ON",
									error);
					
					return( 0 );
				}
				
				error = ri3152a_select_waveform_mode (vi, RI3152A_MODE_ARB);
				if (error < 0)
				{
					ReportError("Select Waveform Mode Error", "setting waveform mode to ARB",
									error);
					
					return( 0 );
				}
				
				error = ri3152a_load_wavecad_wave_file (vi, segment_number, filename);
#endif
				if (error < 0)
					{
					ReportError("WaveCAD File Load Error", "loading WaveCAD \".WAV\" file",
									error);
					
					return( 0 );
					}


				 switch( clock_source )
				 	{
				 	case 0:		clock_source = RI3152A_CLK_SOURCE_INT;		break;
				 	case 1:		clock_source = RI3152A_CLK_SOURCE_EXT;		break;
				 	case 2:		clock_source = RI3152A_CLK_SOURCE_ECLTRG0;	break;
				 	}

#ifndef DEMONSTRATION_ONLY
				error = ri3152a_output_arb_waveform (vi, segment_number, sampling_frequency,
    	                                        	amplitude, offset, clock_source);
#endif
				if ( error < 0)
					{
					ReportError ("Output Arbitrary Waveform Error", 
									"outputting arbitrary waveform", error);
					return (0);
					}
				}
			}
		else /* no file name extension match */
			{
			MessagePopup("Unrecognized File Extension Error", "The file name does not include a \".WAV\" extension.  The file name is not recognized.");
			}
		}

	return (0);
}




int send_arb_segment (int panel, int control,  int event, void *callbackData, int eventData1,
                      int eventData2)
  {
    int segment_number;
    int clock_source;
    char data;
	double sampling_frequency;
    double amplitude;
    double offset;
	char filename[256], buffer[256];
	FILE *fin;
	int count;
	int data_pt;


	SetActiveCtrl (panel, ARBITRARY_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, ARBITRARY_SEGMENT_NUM, &segment_number);
		GetCtrlVal (panel, ARBITRARY_CLOCK, &sampling_frequency);
		GetCtrlVal (panel, ARBITRARY_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, ARBITRARY_OFFSET, &offset);
		GetCtrlVal (panel, ARBITRARY_CLOCK_SOURCE, &clock_source);
		GetCtrlVal (panel, ARBITRARY_FILENAME, filename);

		fin = fopen (filename, "r");
		if (!fin)
     		{
			sprintf(buffer, "Can't open file '%-.128s'", filename);
			MessagePopup("File Error", buffer);
			return(0);
			}
			
		/* make sure all data is between 0 and 4095 */
		for (count = 0;  count < 64536L;  ++count)
			{
			if (fscanf(fin, "%d", &data_pt) != 1)
				break;
				
			if (data_pt < -2048 || data_pt > 2047)
				{
				sprintf(buffer, "Error at data point %d of file '%s'\nData point = %d, outside of range -2048 to 2047\n",
							count+1, filename, data_pt);
							
				MessagePopup ("File Data Error", buffer);
				fclose(fin);
				return(0);
				}
			}
				
		fclose(fin);

		if (count == 0)
		{
			sprintf(buffer, "Cannot read ASCII data file in file '%s'\n", filename);
			MessagePopup ("File Reading Error", buffer);
			return (0);
		}
			
		if (validate_params(amplitude, offset))
			return(0);
			
		error = 0;
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_define_arb_segment (vi, segment_number, count);
#endif

		if ( error < 0)
			{
            ReportError("Define Segment Error", "defining arbitrary segment", error);
            return (0);
            }

#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_load_ascii_file (vi, segment_number, filename, count);
#endif

		if ( error < 0)
			{
			ReportError( "ASCII Load Error", "loading ASCII file", error);
			return (0);
			}

		 switch( clock_source )
		 	{
		 	case 0:		clock_source = RI3152A_CLK_SOURCE_INT;		break;
		 	case 1:		clock_source = RI3152A_CLK_SOURCE_EXT;		break;
		 	}

#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_output_arb_waveform (vi, segment_number, sampling_frequency,
    	                                        amplitude, offset, clock_source);
#endif

		if ( error < 0)
			{
			ReportError ("Output Arbitrary Waveform Error", "outputting arbitrary waveform", error);
			return (0);
			}
		}

	return (0);
}



int select_delete_segments (int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int value;

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		hdl6 = LoadPanel ( 0, "ri3152fp.uir", DELETE);
		SetPanelAttribute (hdl6, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl6, ATTR_TOP, VAL_AUTO_CENTER);
		DisplayPanel (hdl6);
		SetPanelAttribute (hdl2, ATTR_DIMMED, 1);
		handler = hdl6;
		keep_displaying_delete (handler);
		SetPanelAttribute (hdl2, ATTR_DIMMED, 0);
		DiscardPanel (hdl6);
		SetCtrlVal (hdl2, ARBITRARY_DELETE_SEG, 0);
		}

	return (0);
}



int delete_segments ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int segment_number;

	SetActiveCtrl (hdl6, DELETE_DONE);         /* get focus on another control */

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

		GetCtrlVal (panel, DELETE_SEGMENT_NUM, &segment_number);
		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error =  ri3152a_delete_segments (vi, segment_number, 0);
			
		if ( error < 0)
			ReportError ("Delete Segment Error", "deleting segments", error);
#endif
       }

	return (0);
}



int delete_all_segments ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int delete_all_segments;
	int value;

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
     	{
		value = ConfirmPopup ("DELETE ALL SEGMENTS?",
                               "Are you sure you want all segments to be deleted?");

		if (value != 1)
			{
			SetCtrlVal (hdl6, DELETE_DELETE_ALL, 0);
			SetActiveCtrl (hdl6, DELETE_DONE);         /* get focus on another control */
			}
		else
			{
			delete_all_segments = 1;
			error = 0;
#ifndef DEMONSTRATION_ONLY
			if ( !demo_mode )
				error =  ri3152a_delete_segments (vi, 1, delete_all_segments);
#endif

			if ( error < 0)
                ReportError ("Delete Segments Error", "deleting segments", error);

			SetCtrlVal (hdl6, DELETE_DELETE_ALL, 0);
			SetActiveCtrl (hdl6, DELETE_DONE);         /* get focus on another control */

			delete_all_segments = 0;
			}
		}

	return (0);
}



int amp_modulation ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int am_frequency;
	int am_percent;

	SetActiveCtrl (panel, AM_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

		GetCtrlVal (panel, AM_AM_FREQUENCY, &am_frequency);
		GetCtrlVal (panel, AM_AM_PERCENT, &am_percent);
		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( ! demo_mode )
			error = ri3152a_amplitude_modulation (vi, am_percent, am_frequency);

		if ( error < 0)
			ReportError ("Amplitude Modulation Erorr", "setting AM", error);
#endif
		}


	return (0);
}



int phase_lock_loop (int panel, int control, int event, void *callbackData, int eventData1,
                          int eventData2)
{
#ifndef DEMONSTRATION_ONLY
	ViStatus error;
	ViReal64 ext_freq;
	ViReal64 fine_phase;
	ViReal64 coarse_phase;
	ViBoolean pll_state;
	

	SetActiveCtrl (panel, PANEL_QUIT);  /* getting focus on another panel everytime checking event value */

	if (event == EVENT_COMMIT)
		{
		hdl5 = LoadPanel ( 0, "ri3152fp.uir", PLL);
		SetPanelAttribute (hdl5, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl5, ATTR_TOP, VAL_AUTO_CENTER);
	
		coarse_phase = fine_phase = ext_freq = 0.0;
		pll_state = 0;

#ifndef DEMONSTRATION_ONLY
		if (!demo_mode)
			{
			error = ri3152a_pll_query (vi, &pll_state, &coarse_phase, &fine_phase,
									  &ext_freq);
			if (error < 0)
				ReportError ("PLL Query Error", "querying phase lock loop", error);
			}
#endif

		if (pll_state == 0)
			ext_freq = 0.0;

		SetCtrlVal(hdl5, PLL_ON_OFF, pll_state);
		SetCtrlVal(hdl5, PLL_COARSE_PHASE, coarse_phase);
		SetCtrlVal(hdl5, PLL_FINE_PHASE, fine_phase);
		SetCtrlVal(hdl5, PLL_INDICATE_FREQ, ext_freq);
		SetCtrlVal(hdl5, PLL_INDICATE_COARSE, coarse_phase);
		SetCtrlVal(hdl5, PLL_INDICATE_FINE, fine_phase);
		
		DisplayPanel (hdl5);
		SetPanelAttribute (hdl, ATTR_DIMMED, 1);

		handler = hdl5;
		keep_displaying_pll (handler);
		SetPanelAttribute (hdl,ATTR_DIMMED, 0);
		DiscardPanel (hdl5);
		}
#endif
	return (0);
}





//int keep_displaying_standard (int handler)
//{
//	close_std_panel = 0;
//	
//	while (!close_std_panel)
//		ProcessSystemEvents ();
//
//	return (0);
//}



int keep_displaying_arbitrary (int handler)
{
	close_arb_panel = 0;

	while (!close_arb_panel)
		ProcessSystemEvents ();

	return (0);
}


int keep_displaying_wavecad (int handler)
{
	close_wavecad_panel = 0;

	while (!close_wavecad_panel)
		ProcessSystemEvents ();

	return (0);
}


int keep_displaying_delete (int handler)
{
	close_delete_panel = 0;

	while (!close_delete_panel)
		ProcessSystemEvents ();

	return (0);
}


int keep_displaying_sine (int handler)
{
	close_sine_panel = 0;

	while (!close_sine_panel)
		ProcessSystemEvents ();

	return (0);
}



int keep_displaying_triangle (int handler)
{
	close_triangle_panel = 0;
	
	while (!close_triangle_panel)
		ProcessSystemEvents ();

	return (0);
}



int keep_displaying_square (int handler)
{
	close_square_panel = 0;

	while (!close_square_panel)
		ProcessSystemEvents ();

	return (0);
}


int keep_displaying_pulse (int handler)
{
	close_pulse_panel = 0;

	while (!close_pulse_panel)
		ProcessSystemEvents ();

	return (0);
}


int keep_displaying_ramp (int handler)
{
	close_ramp_panel = 0;
	
	while (!close_ramp_panel)
		ProcessSystemEvents ();

	return (0);
}


int keep_displaying_sinc (int handler)

  {
      close_sinc_panel = 0;

      while (!close_sinc_panel)
             ProcessSystemEvents ();

      return (0);

  }

int keep_displaying_gauss (int handler)

  {
      close_gauss_panel = 0;

      while (!close_gauss_panel)
             ProcessSystemEvents ();

      return (0);

  }

int keep_displaying_exponential (int handler)

  {
      close_exponential_panel = 0;

      while (!close_exponential_panel)
             ProcessSystemEvents ();

      return (0);

  }

int keep_displaying_dc (int handler)

  {
      close_dc_panel = 0;

      while (!close_dc_panel)
             ProcessSystemEvents ();

      return (0);

  }

int keep_displaying_am (int handler)

  {
      close_am_panel = 0;

      while (!close_am_panel)
             ProcessSystemEvents ();

      return (0);

  }


int keep_displaying_trigger (int handler)

  {
      close_trigger_panel = 0;

      while (!close_trigger_panel)
             ProcessSystemEvents ();

      return (0);

  }

int keep_displaying_burst (int handler)

  {
      close_burst_panel = 0;

      while (!close_burst_panel)
             ProcessSystemEvents ();

      return (0);

  }

int keep_displaying_about (int handler)

  {
      close_about_panel = 0;

      while (!close_about_panel)
             ProcessSystemEvents ();

      return (0);

  }


int keep_displaying_params (int handler)

  {
      close_params_panel = 0;

      while (!close_params_panel)
             ProcessSystemEvents ();

      return (0);

  }


int keep_displaying_sync (int handler)

  {
      close_sync_panel = 0;

      while (!close_sync_panel)
             ProcessSystemEvents ();

      return (0);

  }
  
int keep_displaying_sweep (int handler)

  {
  	close_sweep_panel = 0;

      while (!close_sweep_panel)
             ProcessSystemEvents ();

      return (0);
  }
  

int keep_displaying_pll (int handler)
{
	close_pll_panel = 0;
	
	while (!close_pll_panel)
		ProcessSystemEvents ();

	return (0);
}

int exit_std_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                     int eventData2)
  {

     if ((event == EVENT_COMMIT))
     	{
		SetPanelAttribute (hdl, ATTR_DIMMED, 0);
		HidePanel(panel);
		DiscardPanel (panel);
		}
     	
//         close_std_panel = 1;


     return(0);

   }

int exit_arb_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                     int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_arb_panel = 1;


     return(0);

   }

int exit_sweep_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                     int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_sweep_panel = 1;


     return(0);

   }

int exit_delete_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                     int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_delete_panel = 1;


     return(0);

   }

int exit_sine_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                     int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_sine_panel = 1;


     return(0);

   }



int exit_triangle_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_triangle_panel = 1;


     return(0);

   }



int exit_square_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_square_panel = 1;


     return(0);

  }


int exit_pulse_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_pulse_panel = 1;


     return(0);

  }

int exit_ramp_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_ramp_panel = 1;


     return(0);

  }

int exit_sinc_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_sinc_panel = 1;


     return(0);

  }

int exit_gauss_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_gauss_panel = 1;


     return(0);

  }

int exit_exponential_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_exponential_panel = 1;


     return(0);

  }

int exit_dc_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_dc_panel = 1;


     return(0);

  }

int exit_am_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_am_panel = 1;


     return(0);

  }


int exit_trigger_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_trigger_panel = 1;


     return(0);

  }


int exit_burst_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_burst_panel = 1;


     return(0);

  }


int exit_about_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_about_panel = 1;


     return(0);

  }


int exit_params_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_params_panel = 1;


     return(0);

  }


int exit_sync_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
{
	if ((event == EVENT_COMMIT))
		close_sync_panel = 1;

	return(0);
}



int exit_wavecad_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                	       int eventData2)
{
	if ((event == EVENT_COMMIT))
		close_wavecad_panel = 1;

	return(0);
}



int exit_pll_panel ( int panel, int control,  int event, void *callbackData, int eventData1,
                     int eventData2)
  {

     if ((event == EVENT_COMMIT))
         close_pll_panel = 1;


     return(0);

   }



int select_std ( int panel, int control, int event, void *callbackData, int eventData1,
                     int eventData2)
  {
    int waveform_type;

    SetActiveCtrl (panel, STANDARD_DONE);

     if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
     {
	    GetCtrlVal (panel, STANDARD_SELECT_STD, &waveform_type);

          switch ( waveform_type )

            {

              case 0: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", SINE);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_sine (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }

              case 1: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", TRIANGLE);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_triangle (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }


              case 2: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", SQUARE);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_square (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }


              case 3: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", PULSE);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_pulse (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }

              case 4: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", RAMP);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_ramp (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }


              case 5: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", SINC);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_sinc (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }

              case 6: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", GAUSS);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_gauss (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }


              case 7: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", EXPONENT);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_exponential (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }

              case 8: {
                        hdl2 = LoadPanel ( 0, "ri3152fp.uir", DC);
                        SetPanelAttribute (hdl2, ATTR_LEFT, VAL_AUTO_CENTER);
                        SetPanelAttribute (hdl2, ATTR_TOP, VAL_AUTO_CENTER);
                        DisplayPanel (hdl2);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 1);
                        keep_displaying_dc (handler);
                        SetPanelAttribute (hdl1, ATTR_DIMMED, 0);
                        DiscardPanel (hdl2);
                        break;
                      }


            }


       }

          return (0);

     }




int sine_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
    {

     double frequency;
     int phase;
     int power;
     float amplitude;
     float offset;
     
     SetActiveCtrl (panel, SINE_DONE);


	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

	     GetCtrlVal (panel, SINE_FREQUENCY, &frequency);
	     GetCtrlVal (panel, SINE_PHASE, &phase);
	     GetCtrlVal (panel, SINE_PWR_SINE, &power);
	     GetCtrlVal (panel, SINE_AMPLITUDE, &amplitude);
	     GetCtrlVal (panel, SINE_OFFSET, &offset);
	     
		if (validate_params(amplitude, offset))
			return( 0 );
			
		error = 0;
#ifndef DEMONSTRATION_ONLY

		if ( !demo_mode )
			error = ri3152a_sine_wave (vi, frequency, amplitude, offset, phase, power);

		if ( error < 0)
			ReportError ("Sine Wave Error", "programming sine wave", error);
#endif
		}

	return (0);
}


int triangle_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	double frequency;
	int phase;
	int power;
	float amplitude;
	float offset;

	SetActiveCtrl (panel, TRIANGLE_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

		GetCtrlVal (panel, TRIANGLE_FREQUENCY, &frequency);
		GetCtrlVal (panel, TRIANGLE_PHASE, &phase);
		GetCtrlVal (panel, TRIANGLE_PWR_TRIANGLE, &power);
		GetCtrlVal (panel, TRIANGLE_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, TRIANGLE_OFFSET, &offset);

		if (validate_params(amplitude, offset))
			return(0);
		
		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_triangular_wave (vi, frequency, amplitude, offset, phase, power);

		if ( error < 0)
			ReportError ("Triangle Wave Error", "programming triangle wave", error);
#endif
       }
       
	return (0);
}


int square_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	double frequency;
	float amplitude;
	float offset;
	int duty_cycle;

	SetActiveCtrl (panel, SQUARE_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

		GetCtrlVal (panel, SQUARE_FREQUENCY, &frequency);
		GetCtrlVal (panel, SQUARE_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, SQUARE_OFFSET, &offset);
		GetCtrlVal (panel, SQUARE_DUTY_CYCLE, &duty_cycle);

		if (validate_params(amplitude, offset))
			return(0);

		error = 0;
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_square_wave (vi, frequency, amplitude, offset, duty_cycle);
			
		if ( error < 0)
			ReportError ("Square Wave Error", "programming square wave", error);
#endif
		}

	return (0);
}


int pulse_waveform ( int panel, int control, int event, void *callbackData, int eventData1,
                   int eventData2)
{
	double frequency;
	float amplitude;
	float offset;
	float delay_time;
	float rise_time;
	float high_time;
	float fall_time;
	
	SetActiveCtrl (panel, PULSE_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

		GetCtrlVal (panel, PULSE_FREQUENCY, &frequency);
		GetCtrlVal (panel, PULSE_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, PULSE_OFFSET, &offset);
		GetCtrlVal (panel, PULSE_DELAY_TIME, &delay_time);
		GetCtrlVal (panel, PULSE_RISE_TIME, &rise_time);
		GetCtrlVal (panel, PULSE_HIGH_TIME, &high_time);
		GetCtrlVal (panel, PULSE_FALL_TIME, &fall_time);

		if (validate_params(amplitude, offset))
			return(0);


        if (  delay_time + rise_time + high_time + fall_time > 100 )
			{
            MessagePopup ("Parameters Out of Range",
                         " delay_time + rise_time + high_time + fall_time  <= 100 ");

			return (0);
     		}


		error = 0;
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_pulse_wave (vi, frequency, amplitude, offset, delay_time, rise_time,
    	                               high_time, fall_time);
		if ( error < 0)
			ReportError ("Pulse Wave Error", "programming pulse wave", error);
#endif
		}

	return (0);
}


int ramp_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	double frequency;
	float amplitude;
	float offset;
	float delay_time;
	float rise_time;
	float fall_time;
	
	SetActiveCtrl (panel, RAMP_DONE);


	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

		GetCtrlVal (panel, RAMP_FREQUENCY, &frequency);
		GetCtrlVal (panel, RAMP_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, RAMP_OFFSET, &offset);
		GetCtrlVal (panel, RAMP_DELAY_TIME, &delay_time);
		GetCtrlVal (panel, RAMP_RISE_TIME, &rise_time);
		GetCtrlVal (panel, RAMP_FALL_TIME, &fall_time);

		if (validate_params(amplitude, offset))
			return(0);


		if (  delay_time + rise_time + fall_time > 100 )
			{
			MessagePopup ("Parameters Out of Range",
                           " delay_time + rise_time + fall_time  <= 100 ");

			return (0);
			}
			
		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_ramp_wave (vi, frequency, amplitude, offset, delay_time, rise_time,
    	                              fall_time);
		if ( error < 0)
			ReportError ("Ramp Wave Error", "programming ramp wave", error);
#endif
       	}

	return (0);
}


int sinc_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
    double frequency;
    float amplitude;
    float offset;
    int cycle_number;

    SetActiveCtrl (panel, SINC_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{

	    GetCtrlVal (panel, SINC_FREQUENCY, &frequency);
	    GetCtrlVal (panel, SINC_AMPLITUDE, &amplitude);
	    GetCtrlVal (panel, SINC_OFFSET, &offset);
	    GetCtrlVal (panel, SINC_CYCLE_NUMBER, &cycle_number);

		if (validate_params(amplitude,offset))
			return(0);

		error = 0;
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_sinc_wave (vi, frequency, amplitude, offset, cycle_number);

		if ( error < 0)
			ReportError ("Sinc Wave Error", "programming sinc wave", error);
#endif
		}

	return (0);
}


int gauss_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{

    double frequency;
    float amplitude;
    float offset;
    int time_constant;

    SetActiveCtrl (panel, GAUSS_DONE);


	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
	    GetCtrlVal (panel, GAUSS_FREQUENCY, &frequency);
	    GetCtrlVal (panel, GAUSS_AMPLITUDE, &amplitude);
	    GetCtrlVal (panel, GAUSS_OFFSET, &offset);
	    GetCtrlVal (panel, GAUSS_TIME_CONSTANT, &time_constant);

		if (validate_params(amplitude,offset))
			return(0);

		error = 0;

#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_gaussian_wave (vi, frequency, amplitude, offset, time_constant);

		if ( error < 0)
			ReportError ("Gaussian Wave Error", "programming gaussian wave", error);
#endif
		}

	return (0);
}


int exponential_waveform ( int panel, int control,  int event, void *callbackData, int eventData1,
                           int eventData2)
{
	double frequency;
	float amplitude;
	float offset;
	float time_constant;

	SetActiveCtrl (panel, EXPONENT_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, EXPONENT_FREQUENCY, &frequency);
		GetCtrlVal (panel, EXPONENT_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, EXPONENT_OFFSET, &offset);
		GetCtrlVal (panel, EXPONENT_TIME_CONSTANT, &time_constant);

		if (validate_params(amplitude, offset))
			return(0);

		if ( time_constant == 0 )
			{
			MessagePopup ("Parameters Out of Range", " Zero time constant not allowed ");
			return (0);
             }

		error = 0;
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_exponential_wave (vi, frequency, amplitude, offset, time_constant);

		if ( error < 0)
			ReportError ("Exponential Wave Error", "programming exponential wave", error);
#endif
		}

	return (0);
}




int dc_signal ( int panel, int control,  int event, void *callbackData, int eventData1,
                           int eventData2)
{

	float amplitude_level;
	int pct;

	SetActiveCtrl (panel, DC_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, DC_AMP_LEVEL, &amplitude_level);
		pct = 100 * (amplitude_level / 8.0);

		error = 0;
	
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			{
			error = ri3152a_set_offset (vi, 0.0);
			if (error < 0)
				ReportError ("DC Offset Error", "programming DC offset", error);
			error = ri3152a_set_amplitude (vi, 16.0);
			if (error < 0)
				ReportError ("DC Amplitude Error", "programming DC amplitude", error);
			error = ri3152a_dc_signal (vi, pct);
			}
		if ( error < 0)
			ReportError ("DC Signal Error", "programming DC signal percent amplitude", error);
#endif

		}

	return (0);
}




int set_freq_ampl_offset (int panel, int control, int event, void *callbackData, int eventData1,
                          int eventData2)
{
	ViStatus error;
	ViInt16 filter;
	ViReal64 offset;
	ViReal64 freq;
	ViReal64 ampl;
	
	SetActiveCtrl (panel, PANEL_QUIT);  /* getting focus on another panel everytime checking event value */


	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
        hdl4 = LoadPanel ( 0, "ri3152fp.uir", PARAMS);
		SetPanelAttribute (hdl4, ATTR_LEFT, VAL_AUTO_CENTER);
		SetPanelAttribute (hdl4, ATTR_TOP, VAL_AUTO_CENTER);

#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			{
			error = ri3152a_status_query (vi, &ampl, &freq, &offset, &filter);
			if (error < 0)
				{
				ReportError("Status Query Error", "reading frequency, amplitude, &offset", error);
				}
	
			SetCtrlVal(hdl4, PARAMS_FREQUENCY, freq);
			SetCtrlVal(hdl4, PARAMS_AMPLITUDE, ampl);
			SetCtrlVal(hdl4, PARAMS_OFFSET, offset);
			}
		
#endif
		DisplayPanel (hdl4);
		SetPanelAttribute (hdl, ATTR_DIMMED, 1);

		handler = hdl4;
		keep_displaying_params (handler);
		SetPanelAttribute (hdl,ATTR_DIMMED, 0);
		DiscardPanel (hdl4);
		}

	return (0);
}


int apply_parameters ( int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
    {

     double frequency;
     float amplitude;
     float offset;

     SetActiveCtrl (panel, PARAMS_DONE);


    if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, PARAMS_FREQUENCY, &frequency);
		GetCtrlVal (panel, PARAMS_AMPLITUDE, &amplitude);
		GetCtrlVal (panel, PARAMS_OFFSET, &offset);
		
		if (validate_params(amplitude, offset))
			return(0);

		error = 0;
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
	        error = ri3152a_set_frequency (vi, frequency);

		if ( error < 0)
			ReportError ("Set Frequency Error", "setting frequency", error);

		if ( !demo_mode )
			error = ri3152a_set_offset (vi, 0.0);

		if ( error < 0)
			ReportError ("Set Offset Error", "setting the Offset to 0.0", error);

		if ( !demo_mode )
			error = ri3152a_set_amplitude (vi, amplitude);

		if ( error < 0)
			ReportError ("Set Amplitude Error", "setting the amplitude", error);

		if ( !demo_mode )
			error = ri3152a_set_offset (vi, offset);

		if ( error < 0)
			ReportError ("Set Offset Error", "setting the offset", error);
#endif
		}

	return (0);
}





int apply_sync ( int panel, int control,  int event, void *callbackData, int eventData1,
                   		int eventData2)
{

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, SYNC_SYNCPOS, &sync_pos);
		GetCtrlVal (panel, SYNC_SYNCWIDT, &sync_widt);
		GetCtrlVal (panel, SYNC_SYNCSRC, &sync_src);

		error = 0;

#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_output_sync (vi, sync_src + 1, sync_pos, sync_widt, 1);

		if (error < 0)
			ReportError ("Set SYNC Position & Width Error", "setting SYNC Position and Width", error);
#endif
		}

	return (0);
}





int apply_sweep ( int panel, int control,  int event, void *callbackData, int eventData1,
                   		int eventData2)
{
	int sweep_func, sweep_dir, sweep_space;
	short sweep_step;
	double sweep_time, sweep_start_freq, sweep_stop_freq, sweep_sclk, sweep_marker;
	double actual_time;  
	ViInt16 response;
	char error_message[100];
	ViUInt32 cnt;
	int value;

//	Delay (5);
	SetActiveCtrl (panel, SWEEP_DONE);
	
	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		value = MessagePopup ("SWEEP START", "Sending Sweep Commands... Please Wait!");
		GetCtrlVal (panel, SWEEP_FUNCTION, &sweep_func);
		GetCtrlVal (panel, SWEEP_DIRECTION, &sweep_dir);
		GetCtrlVal (panel, SWEEP_SPACING, &sweep_space);
		GetCtrlVal (panel, SWEEP_TIME, &sweep_time);
		GetCtrlVal (panel, SWEEP_STEP, &sweep_step);
		GetCtrlVal (panel, SWEEP_START_FREQ, &sweep_start_freq);
		GetCtrlVal (panel, SWEEP_STOP_FREQ, &sweep_stop_freq);
		GetCtrlVal (panel, SWEEP_S_CLOCK, &sweep_sclk);
		GetCtrlVal (panel, SWEEP_MARKER, &sweep_marker);

		error = 0;

#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			{
			Delay (5);
			error = ri3152a_sweep_function (vi, sweep_func);
			if (error < 0)
				ReportError ("Set Sweep Function Error", "setting Sweep Function", error);

			Delay (1);
			error = ri3152a_set_sweep_direction (vi, sweep_dir);
			if (error < 0)
				ReportError ("Set Sweep Direction Error", "setting Sweep Direction", error);

			Delay (1);
			error = ri3152a_set_sweep_spacing (vi, sweep_space);
			if (error < 0)
				ReportError ("Set Sweep Spacing Error", "setting Sweep Spacing", error);
				
			Delay (1);
			error = ri3152a_set_sweep_time (vi, sweep_time);
			if (error < 0)
				ReportError ("Set Sweep Time Error", "setting Sweep Time", error);
				
			Delay (10);
			error = ri3152a_set_sweep_step (vi, sweep_step);
			if (error < 0)
				ReportError ("Set Sweep Step Error", "setting Sweep Step", error);
				
			Delay (1);
			error = ri3152a_set_sweep_freq_start (vi, sweep_start_freq);
			if (error < 0)
				ReportError ("Set Sweep Start Frequency Error", "setting Sweep Start Frequency", error);
				
			Delay (1);
			error = ri3152a_set_sweep_freq_stop (vi, sweep_stop_freq);
			if (error < 0)
				ReportError ("Set Sweep Stop Frequency Error", "setting Sweep Direction", error);
				
			Delay (1);
			error = ri3152a_set_sweep_freq_raster (vi, sweep_sclk);
			if (error < 0)
				ReportError ("Set Sweep Frequency Raster Error", "setting Sweep Frequency Raster", error);
				
			Delay (1);
			error = ri3152a_set_sweep_freq_marker (vi, sweep_marker);
			if (error < 0)
				ReportError ("Set Sweep Marker Error", "setting Sweep Marker", error);
				
			}
			value = MessagePopup ("SWEEP DONE", "Sweep Completed");
#endif
		}
		


	return (0);
}



int filter (int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int filter_sw;

	GetCtrlVal (hdl, PANEL_FILTER, &filter_sw);

	if (event == EVENT_VAL_CHANGED)
		{
     	error = 0;
     	
#ifndef DEMONSTRATION_ONLY
     	if ( !demo_mode )
     		error = ri3152a_filter (vi, filter_sw);

		if ( error < 0)
			ReportError ("Filter Error", "selecting the filter", error);
#endif
		}

	return (0);
}



int trigger_mode (int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int trigger_source;

	GetCtrlVal (panel, TRIGGER_SOURCE, &trigger_source);


	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		if (current_trigger_mode == BURST) {
			SetCtrlAttribute (panel, TRIGGER_BURSTCYCLE, ATTR_DIMMED, 0);
			SetCtrlAttribute (panel, TRIGGER_LEVEL, ATTR_DIMMED, 1);
		}
		else
			SetCtrlAttribute (panel, TRIGGER_BURSTCYCLE, ATTR_DIMMED, 1);
		
		switch ( trigger_source )
			{
            case RI3152A_TRIGGER_INTERNAL:

				SetCtrlAttribute (panel, TRIGGER_RATE, ATTR_DIMMED, 0);
				SetCtrlAttribute (panel, TRIGGER_INTERNAL, ATTR_DIMMED, 0);
				SetCtrlAttribute (panel, TRIGGER_LEVEL, ATTR_DIMMED, 1);

				error = 0;
#ifndef DEMONSTRATION_ONLY
				if ( !demo_mode )
					error = ri3152a_trigger_source (vi, RI3152A_TRIGGER_INTERNAL);

				if ( error < 0)
					ReportError ("Internal Trigger error", "selecting internal trigger", error);
#endif
				break;

			case RI3152A_TRIGGER_EXTERNAL:
				SetCtrlAttribute (panel, TRIGGER_RATE, ATTR_DIMMED, 1);
				
				if (!strcmp (active_model, "3152"))
					SetCtrlAttribute (panel, TRIGGER_LEVEL, ATTR_DIMMED, 0);
				else
					SetCtrlAttribute (panel, TRIGGER_LEVEL, ATTR_DIMMED, 1);

				error = 0;
#ifndef DEMONSTRATION_ONLY
				if ( !demo_mode )
					error = ri3152a_trigger_source (vi, RI3152A_TRIGGER_EXTERNAL);

				if ( error < 0)
					ReportError ("External Trigger error", "selecting external trigger", error);
#endif
				break;


            case RI3152A_TRIGGER_TTLTRG0:
            case RI3152A_TRIGGER_TTLTRG1:
            case RI3152A_TRIGGER_TTLTRG2:
            case RI3152A_TRIGGER_TTLTRG3:
            case RI3152A_TRIGGER_TTLTRG4:
            case RI3152A_TRIGGER_TTLTRG5:
            case RI3152A_TRIGGER_TTLTRG6:
            case RI3152A_TRIGGER_TTLTRG7:

				SetCtrlAttribute (panel, TRIGGER_RATE, ATTR_DIMMED, 1);
				SetCtrlAttribute (panel, TRIGGER_LEVEL, ATTR_DIMMED, 1);
				
				error = 0;
#ifndef DEMONSTRATION_ONLY
				if ( !demo_mode )
					error = ri3152a_trigger_source (vi, trigger_source);

				if (error < 0)
					ReportError ("TTLTRGx Trigger error", "selecting TTLTRGx", error);
#endif
				break;
			}
		}

	return (0);
}


int internal_trigger (int panel, int control,  int event, void *callbackData, int eventData1,
                   int eventData2)
{
	int trigger_source;
	double trigger_rate;

	SetActiveCtrl (panel, TRIGGER_DONE);

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, TRIGGER_SOURCE, &trigger_source);
		GetCtrlVal (panel, TRIGGER_RATE, &trigger_rate);

		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_trigger_source (vi, trigger_source);

		if (error < 0)
			ReportError ("Trigger Error", "selecting trigger source", error);

		error = 0;
		
		if ( !demo_mode )
			error = ri3152a_trigger_rate(vi, (ViReal64) trigger_rate);
          	
		if (error < 0)
			ReportError ("Trigger Error", "programming trigger rate", error);
#endif
		}


	return (0);
}



int set_burst_cycles (int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
{
	int burst_source;
	int burst_cycle;

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, TRIGGER_BURSTCYCLE, &burst_cycle);
    
		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_burst_mode (vi, burst_cycle);
			
		if (error < 0)
			ReportError ("Burst Mode Error", "programming the burst mode/# cycles", error);
#endif
		}

	return (0);
}



int trigger_level (int panel, int control,  int event, void *callbackData, int eventData1,
                       int eventData2)
{
	double trigger_level;

	if ((event == EVENT_COMMIT) || (event == EVENT_GOT_FOCUS))
		{
		GetCtrlVal (panel, TRIGGER_LEVEL, &trigger_level);
    
		error = 0;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			error = ri3152a_set_trigger_level (vi, (ViReal64) trigger_level);
			
		if (error < 0)
			ReportError ("Trigger Level Error", "programming the trigger level", error);
#endif
		}

	return (0);
}




int apply_pll ( int panel, int control,  int event, void *callbackData, int eventData1,
                           int eventData2)
{

	ViReal64 coarse_phase, fine_phase, ext_freq;
	ViBoolean pll_state;
	ViStatus error;

	SetActiveCtrl (panel, PLL_DONE);

	if (event == EVENT_COMMIT)
		{
		GetCtrlVal (panel, PLL_COARSE_PHASE, &coarse_phase);
		GetCtrlVal (panel, PLL_FINE_PHASE, &fine_phase);
		GetCtrlVal (panel, PLL_INDICATE_FREQ, &ext_freq);

	
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			{
			error = ri3152a_pll_phase (vi, coarse_phase);
			if ( error < 0)
				{
				ReportError("PLL Phase Error", "setting phase lock loop phase", error);
				return(0);
				}

			if (fine_phase != 0.0)
				{
				error = ri3152a_pll_fine_phase (vi, fine_phase);
				if ( error < 0)
					{
					ReportError("PLL Phase Error", "setting phase lock loop fine phase", error);
					return(0);
					}
				}

			error = ri3152a_pll_query (vi, &pll_state, &coarse_phase, &fine_phase, &ext_freq);
			if ( error < 0)
				{
				ReportError("PLL Phase Error", "querying phase lock loop parameters", error);
				return(0);
				}
			}
#endif

		
		SetCtrlVal(panel, PLL_INDICATE_FREQ, ext_freq);
		SetCtrlVal(panel, PLL_INDICATE_COARSE, coarse_phase);
		SetCtrlVal(panel, PLL_INDICATE_FINE, fine_phase);
		SetCtrlVal(panel, PLL_ON_OFF, pll_state);
		}

	return (0);
}




int pll_switch ( int panel, int control,  int event, void *callbackData, int eventData1,
                           int eventData2)
{

	ViReal64 coarse_phase, fine_phase, ext_freq;
	ViBoolean pll_state;
	int int_pll_state;
	ViStatus error;


	SetActiveCtrl (panel, PLL_DONE);

	if (event == EVENT_COMMIT)
		{
		GetCtrlVal(panel, PLL_ON_OFF, &int_pll_state);
		pll_state = int_pll_state;

		if (pll_state == 0)
			ext_freq = 0.0;
		else
			ext_freq = 123.0E5;
		
#ifndef DEMONSTRATION_ONLY
		if ( !demo_mode )
			{
			error = ri3152a_phase_lock_loop (vi, pll_state);
			if ( error < 0)
				{
				ReportError("PLL On/Off Error", "turning phase lock loop on or off", error);
				return(0);
				}

			error = ri3152a_pll_query (vi, &pll_state, &coarse_phase, &fine_phase, &ext_freq);
			if ( error < 0)
				{
				ReportError("PLL Phase Error", "querying phase lock loop parameters", error);
				return(0);
				}
			
			}
#endif
		SetCtrlVal(panel, PLL_INDICATE_FREQ, ext_freq);
		}

	return (0);
}






/********************************************************************************
 * This function performs an autoconnect to the 3152(s)                         *
 ********************************************************************************/
int autoconnect( void )
{
#ifndef DEMONSTRATION_ONLY
	int panel;
	int selected;
	ViSession local_vi;
	ViStatus error;
	static ViChar desc_list[8][256];
	char descriptor[256];
	char slot_string[32], log_addr_string[32];
	ViChar *desc_ptr;
	int desc_count, select_count;
	ViFindList vi_find_list;
	ViUInt32 vi_count, cnt;
	int i;
	ViUInt16 log_addr, slot;
	int select_list[8];
	char error_message[256];
	char error_string[1024];
	static int checkboxes[8] = { SELECT_CHECKBOX_1, SELECT_CHECKBOX_2,
								 SELECT_CHECKBOX_3, SELECT_CHECKBOX_4,
								 SELECT_CHECKBOX_5, SELECT_CHECKBOX_6,
								 SELECT_CHECKBOX_7, SELECT_CHECKBOX_8 };
	static int controllers[8] = { SELECT_CONTROLLER_1, SELECT_CONTROLLER_2,
								  SELECT_CONTROLLER_3, SELECT_CONTROLLER_4,
								  SELECT_CONTROLLER_5, SELECT_CONTROLLER_6,
								  SELECT_CONTROLLER_7, SELECT_CONTROLLER_8 };
	static int slots[8] =		{ SELECT_SLOT_1, SELECT_SLOT_2, SELECT_SLOT_3,
								  SELECT_SLOT_4, SELECT_SLOT_5, SELECT_SLOT_6,
								  SELECT_SLOT_7, SELECT_SLOT_8 };
	static int log_addrs[8] = 	{ SELECT_LOGADDR_1, SELECT_LOGADDR_2,
								  SELECT_LOGADDR_3, SELECT_LOGADDR_4,
								  SELECT_LOGADDR_5, SELECT_LOGADDR_6,
								  SELECT_LOGADDR_7, SELECT_LOGADDR_8 };


	error = viOpenDefaultRM (&rm_handle);
	if (error < 0)
		{
		ReportError("VISA Error", "viOpenDefaultRM() call failed\nPlease ensure VISA has been installed on this computer\n",error);
		return( run_demo_mode_query() );
		}

	/*
	 * find each 3152 on the GPIB-VXI or VXI interface
	 */
	desc_count = 0;
	
	error = viFindRsrc (rm_handle, "VXI?*INSTR", &vi_find_list, 
						&vi_count, descriptor);
						
	if (vi_count > 0)
		{
		if (is_315X(descriptor))
			strcpy(desc_list[desc_count++],descriptor);
			
		while ( --vi_count && (desc_count < 8) )
			{
			error = viFindNext (vi_find_list, descriptor);
			if (error >= 0)
				{
				if (is_315X(descriptor))
					strcpy(desc_list[desc_count++], descriptor);
				}
			}
		}


	error = viFindRsrc (rm_handle, "GPIB-VXI?*INSTR", &vi_find_list, 
						&vi_count, descriptor);
						
	if (vi_count > 0)
		{
		if (is_315X(descriptor))
			strcpy(desc_list[desc_count++],descriptor);
			
		while ( --vi_count && (desc_count < 8) )
			{
			error = viFindNext (vi_find_list, descriptor);
			if (error >= 0)
				{
				if (is_315X(descriptor))
					strcpy(desc_list[desc_count++], descriptor);
				}
			}
		}

	/*
	 * assume we are NOT in demonstration mode
	 */
	demo_mode = 0;

	/*
	 * Case 1:  No 3152s
	 */
	if (desc_count == 0)
		{
		return( run_demo_mode_query() );
		}
		
	/*
	 * case 2:  A single 3152
	 */
	if (desc_count == 1)
		{
		error = ri3152a_init(desc_list[0], VI_TRUE, VI_TRUE, &local_vi);
		if (error < 0)
			{
			sprintf(error_message, "initializing 3152 descriptor '%s'", descriptor);
			ReportError("Initialization Error", error_message, error);
			return( run_demo_mode_query() );
			}
		/*
		 * success
		 */
		
		error = ri3152a_output(local_vi, RI3152A_OUTPUT_OFF);
		if (error < 0)
			ReportError("Output On/Off Error", "turning output off", error);
		
		vi = local_vi;

		/* get the model code from *IDN? query reply */
		error = viPrintf(local_vi, "*IDN?\n");
		if (error < 0)
			{
			sprintf(error_message, "sending *IDN? query to descriptor '%s'", descriptor);
			ReportError("Initialization Error", error_message, error);
			return( run_demo_mode_query() );
			}


		error = viRead(local_vi, (unsigned char *) error_message, (ViUInt32) (sizeof(error_message)-1), &cnt);
		if (error < 0)
			{
			sprintf(error_message, "reading *IDN? reply from descriptor '%s'", descriptor);
			ReportError("Initialization Error", error_message, error);
			return( run_demo_mode_query() );
			}
		
		error_message[cnt] = 0;
		if (strstr(error_message, "3152A") != NULL)
			strcpy (active_model, "3152A");
		else
			strcpy (active_model, "3152");

		return( 0 );
		}

	/*
	 * case 3:  Multiple 3152s
	 */
	select_count = 0;
	
	panel = LoadPanel ( 0, "ri3152fp.uir", SELECT);
	SetPanelAttribute (panel, ATTR_LEFT, VAL_AUTO_CENTER);
	SetPanelAttribute (panel, ATTR_TOP, VAL_AUTO_CENTER);
	DisplayPanel (panel);

	for (i = 0;  i < 8;  ++i)
		SetCtrlVal(panel, checkboxes[i], 0);
		
	for (i = 0;   i < desc_count;  ++i)
		{
		error = viOpen (rm_handle, desc_list[i], VI_NULL, VI_NULL, &local_vi);
		if (error >= 0)
			{
			log_addr = 0;
			error = viGetAttribute (local_vi, VI_ATTR_VXI_LA, &log_addr);
			if (error >= 0)
				{
				slot = 0;
				error = viGetAttribute(local_vi, VI_ATTR_SLOT, &slot);
				if (error >= 0)
					{
					SetCtrlVal(panel, controllers[i], desc_list[i]);
					sprintf(slot_string, "%d", slot);
					SetCtrlVal(panel, slots[i], slot_string);
					sprintf(log_addr_string, "%d", log_addr);
					SetCtrlVal(panel, log_addrs[i], log_addr_string);
					select_list[select_count++] = i;
					}
				}
			}

		viClose( local_vi );
		}
		
	/*
	 * this checks the case where more than 1 3152 has been found, but
	 * errors were encountered in reading attributes for them
	 */
	if (select_count == 0)
		{
		MessagePopup("Autoconnect Error", "VISA errors prevent selection of 3152");
		return( run_demo_mode_query() );
		}
		
	/*
	 *  wait until the user selects a 3152
	 */
	selected = 0;
	while ( !selected )
		{
		for (i = 0;  i < select_count && !selected;  ++i)
			{
			ProcessSystemEvents();
			GetCtrlVal(panel, checkboxes[i], &selected);
			if (selected)
				break;
			}
		}
	
	strcpy(descriptor, desc_list[i]);
	
	error = ri3152a_init(descriptor, VI_TRUE, VI_TRUE, &local_vi);
	if (error < 0)
		{
		sprintf(error_message, "initializing 3152 descriptor '%s'", descriptor);
		ReportError("Initialization Error", error_message, error);
		return( run_demo_mode_query() );
		}

			
	error = ri3152a_output(local_vi, RI3152A_OUTPUT_OFF);
	if (error < 0)
		ReportError("Output On/Off Error", "turning output off", error);
		
	vi = local_vi;
	
	/* get the model code from *IDN? query reply */
	error = viPrintf(local_vi, "*IDN?\n");
	if (error < 0)
		{
		sprintf(error_message, "sending *IDN? query to descriptor '%s'", descriptor);
		ReportError("Initialization Error", error_message, error);
		return( run_demo_mode_query() );
		}


	error = viRead(local_vi, (unsigned char *) error_message, (ViUInt32) (sizeof(error_message)-1), &cnt);
	if (error < 0)
		{
		sprintf(error_message, "reading *IDN? reply from descriptor '%s'", descriptor);
		ReportError("Initialization Error", error_message, error);
		return( run_demo_mode_query() );
		}
		
	error_message[cnt] = 0;
	if (strstr(error_message, "3152") != NULL)
		strcpy (active_model, "3152");
	else if (strstr(error_message, "3152A") != NULL)
		strcpy (active_model, "3152A");
	else
		strcpy (active_model, "3152");

	DiscardPanel(panel);
	
#else	/* DEMONSTRATION_ONLY is #defined */
	demo_mode = 1;
	strcpy (active_model, "3152");
#endif

	return(0);
}
		
		
				

/********************************************************************************
 * This function opens the descriptor and checks the model code and manufacturer*
 * to see if the device is a 3152.  Returns 0 if NOT a 3152, 1 if it is a 3152  *
 ********************************************************************************/
int is_315X(char *descriptor)
{
#ifndef DEMONSTRATION_ONLY
	ViStatus error;
	ViSession local_vi;
	ViUInt16 manf_id, model_code;
	
	
	error = viOpen (rm_handle, descriptor, VI_NULL, VI_NULL, &local_vi);
	if (error < 0)
		return( 0 );
		
	error = viGetAttribute (local_vi, VI_ATTR_MANF_ID, &manf_id);
	manf_id &= 0xFFFF;
	if (error < 0 || manf_id != RI3152A_MANF_ID)
		{
		viClose(local_vi);
		return( 0 );
		}
		
	error = viGetAttribute (local_vi, VI_ATTR_MODEL_CODE, &model_code);
	model_code &= 0xFFFF;
	if ((error < 0)
	||  (model_code != RI3152A_MODEL_CODE && model_code != RI3152_MODEL_CODE))
		{
		viClose(local_vi);
		return( 0 );
		}
		
	viClose( local_vi );

	return( 1 );
#else
	return(0);
#endif
}


/********************************************************************************
 * This function queries the user to see if the demo mode should be used        *
 ********************************************************************************/
int run_demo_mode_query(void)
{
	int reply;
	
	reply = ConfirmPopup ("No 3152's Found",
					  	   "No 3152's have been found in this system.\n\n\
Do you want to run the demonstration?");

	if (reply != 1)
		return( -1 );
	else
		{
		demo_mode = 1;
		}

	return(0);
}

/********************************************************************************
 * This function validates the amplitude and offset to ensure that they are     *
 * valid.  At no time may the sum of (Amplitude / 2) + ABS( offset ) exceed 8.0 *
 * Also, the amplitude selects the range, and the value of (Amplitude / 2) +    *
 * ABS(offset) cannot exceed that range.  The ranges are 80 mV, 800 mV, and 8V  *
 ********************************************************************************/
int validate_params(double amplitude, double offset)
{
	double range_max;
	double half_amplitude;
	double max_volts;
	
	half_amplitude = amplitude / 2.0;
	max_volts = half_amplitude + fabs(offset);
	
	
	if ( max_volts > 8.00 )
		{
		MessagePopup ("Amplitude/Offset Out of Range", " Amplitude/2 + |Offset| > 8.00  ");
		return (-1);
        }
    
        
    /* 3152 is more forgiving of range errors */
    if (!strcmp (active_model, "3152"))
    	return(0);

#ifndef DEMONSTRATION_ONLY
	/* get firmware revision, if 1.6 or later, wider tolerance range also */
	if ( !demo_mode )
		{
		ViStatus error;
		ViChar firmware_rev[40];
		ViChar driver_rev[40];
		double revision;
		
		strcpy(firmware_rev, "1.6");
		error = ri3152a_revision_query (vi, driver_rev, firmware_rev);
		if (error < 0)
			ReportError("Revision Query Error", "reading the firmware revision", error);
		
		
		revision = atof(firmware_rev);
		
		if (revision > 1.5)
			return(0);
		}
	else
		return(0);
#endif
    	
    if (half_amplitude <= 0.080)
    	{
    	if (max_volts > 0.080)
    		{
    		MessagePopup("Amplitude/Offset 80 mV Range Error", "Amplitude/2 + |Offset| > 0.080 ");
    		return(-1);
    		}
    	}
    else if (half_amplitude <= 0.800)
    	{
    	if (max_volts > 0.800)
    		{
    		MessagePopup("Amplitude/Offset 800 mV Range Error", "Amplitude/2 + |Offset| > 0.800 ");
    		return(-1);
    		}
		}

	return(0);
}


/********************************************************************************
 * This function produces a VISA error message in a MessagePopup panel          *
 ********************************************************************************/
void ReportError( char *title, char *action, ViStatus error)
{
	ViChar error_message[256];
	ViChar errbuf[256];

#ifndef DEMONSTRATION_ONLY

	ri3152a_error_message(vi, error, error_message);
	
	sprintf(errbuf,"Error while %s\nError code = %d (%X)\nError Message = '%-.80s'\n",
				action, error, error, error_message);
				
	MessagePopup(title, errbuf);
#endif
}
