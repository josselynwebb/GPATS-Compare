#include <formatio.h>
#include "radioGroup.h"  
#include <ansi_c.h>
#include <userint.h>
#include "ATLAS.h"


extern int atlasHandle;


int CVICALLBACK ClearMsg (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	int status = 0;
	
	switch (event)
	{
		case EVENT_COMMIT:

			ResetTextBox(atlasHandle, panAtlas_txtATLAS,"");
			break;
	}
	return 0;
}

int CVICALLBACK Open_File (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	char pathName[MAX_PATHNAME_LEN]; 
	char myString[1024] = "";
	int nFileHandle = -1;
	
	switch (event)
	{
		case EVENT_COMMIT:

			FileSelectPopup("","*.txt","Text File (*.txt)","Save ATLAS Data",VAL_SELECT_BUTTON,0,0,1,0,pathName);
			
			
			// Open file
			nFileHandle = OpenFile (pathName, VAL_READ_ONLY, VAL_OPEN_AS_IS, VAL_ASCII);
			if (nFileHandle != -1)
			{
				ReadFile (nFileHandle, myString , 1024);
				
				
				ResetTextBox (atlasHandle, panAtlas_txtATLAS, myString);  
				
				CloseFile (nFileHandle);
			}
		break;
	}
	return 0;
}

int CVICALLBACK Save_File (int panel, int control, int event,
		void *callbackData, int eventData1, int eventData2)
{
	int numBytes = 0;
	char *myString = NULL;
	int nType = -1;
	int nFileHandle = -1;
	char pathName[MAX_PATHNAME_LEN];
	
	switch (event)
	{
		case EVENT_COMMIT:

			
			// Find the length of text in the textbox and alocate the memory for it
			GetCtrlAttribute (atlasHandle, panAtlas_txtATLAS, ATTR_STRING_TEXT_LENGTH, &numBytes);
			myString = malloc (numBytes + 1); // +1 because of the NUL character at the end
			// Retrive the ATLAS data
			GetCtrlVal (atlasHandle, panAtlas_txtATLAS, myString);
			
			Radio_GetMarkedOption (atlasHandle, panAtlas_OUTPUTTYPE, &nType);
			
			switch (nType)
			{
				case 0: //Text File
					FileSelectPopup("","*.txt","Text File (*.txt)","Save ATLAS Data",VAL_SAVE_BUTTON,0,0,1,1,pathName);
					
					
					// Open file (create it if not present)
					nFileHandle = OpenFile (pathName, VAL_WRITE_ONLY, VAL_TRUNCATE, VAL_ASCII);
					if (nFileHandle != -1)
					{
						WriteFile (nFileHandle, myString, strlen(myString));
						CloseFile (nFileHandle);
					}
					break;
					
				case 1: //Clipboard
					ClipboardPutText(myString);
					
					break;	
			}
			
			if (myString)
				free (myString);
			break;
	}
	return 0;
}
