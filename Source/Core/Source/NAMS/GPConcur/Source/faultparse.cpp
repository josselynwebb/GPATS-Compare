/////////////////////////////////////////////////////////////////////////////
//	File:	faultparse.cpp													/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		1.0.0.0		Assigned it a version number.							/
//		1.0.1.0		Added the include of windows.h, this is need for the	/
//					ZeroMemory() call.										/
//					GetInfo()												/
//					Added the call to ZeroMemory() to zero out the FaultFp	/
//					structure elements before each use.						/
//					GetDateTime()											/
//					Modified the code where the call to free() was being	/
//					called.  I now check to see if the variable is not NULL	/
//					before calling free to free up the memory.				/
//					Modified the return() statement, and the comment above	/
//					it.  I need to check for the date 29 Feb and change it	/
//					to 28 Feb, to prevent any future problems with the		/
//					access database.										/
//					newformat()												/
//					Modified the code where the call to free() was being	/
//					called.  I now check to see if the variable is not NULL	/
//					before calling free to free up the memory.				/
//					ExtractInfo()											/
//					Modified the code where the call to free() was being	/
//					called.  I now check to see if the variable is not NULL	/
//					before calling free to free up the memory.				/
//		1.0.2.0		Modified the way comments are commented,this allows for	/
//					blocks of code to be commented out easily				/
//					RemoveSpaces()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					GetItem()												/
//					Changed the name from get_this to the present. Corrected/
//					the info header to correctly reflect the function.		/
//					Changed the way the variables are displayed (camel back	/
//					style). Changed the way the string variables are zero'd	/
//					out, the piror way didn't do anything, now using memset	/
//					to clear the variables. Changed the name of some of the	/
//					to correctly reflect what they are (cur_time -			/
//					CurrentTime; curtp - CurrentTimePt; tmpt - TimeStructPt;/
//					mon - MonthSent). Deleted the declaration of the char	/
//					tmpbuf1, the function call to memset and the sprintf	/
//					function call after the if (indexPos, all is now done in/
//					the DoDebug function call.								/
//					GetInfo()												/
//					Changed the name from get_this to the present. Corrected/
//					the info header to correctly reflect the function.		/
//					Changed the way the variables are written				/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables.						/
//					DoMonthYer()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(cur_time - CurrentTime; curtp -	/
//					CurrentTimePt; tmpt - TimeStructPt; mon - MonthSent).	/
//					GetDateTime()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the malloc to calloc to initialize the variable	/
//					to all null characters. Changed the name of some of the	/
//					variables to correctly reflect what they are (k -		/
//					ScanReturn; tmpbuf1 - ErrorMessage). Deleted the		/
//					declaration of the char tmpbuf1, the function call to	/
//					memset and the sprintf function call after the sscanf,	/
//					all is now done in the DoDebug function call.			/
//					NumberOfThis()											/
//					Corrected the info header to correctly reflect the		/
//					function. Changed the way the variables are written		/
//					(camel back style).										/
//					FindStringRight()										/
//					Changed the name from strrstr to the present. Corrected	/
//					the info header to correctly reflect the function.		/
//					Changed the way the variables are written				/
//					(camel back style). Changed the way the string variables/
//					are zero'd out, the prior way didn't do anything, now	/
//					using memset to zero the variables. Deleted the			/
//					declaration of the char tmpbuf, the function call to	/
//					memset and the sprintf function call after the if		/
//					(StartPoint, all is now done in the DoDebug function	/
//					call.													/
//					FindPoint()												/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(pt_found - PointFound; tmpbuf -	/
//					ErrorMessage). Deleted the declaration of the char		/
//					tmpbuf, the function call to memset and the sprintf		/
//					function call after the if((PointFound, all is now done	/
//					in the DoDebug function									/
//					MoveToPoint()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(pt_found - PointFound; tmpbuf -	/
//					ErrorMessage). Deleted the declaration of the char		/
//					tmpbuf, the function call to memset and the sprintf		/
//					function call after the if((PointFound, all is now done	/
//					in the DoDebug function									/
//					GotoTermNumber()										/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(tmpbuf - ErrorMessage). Deleted	/
//					the declaration of the char	tmpbuf, the function call	/
//					to memset and the sprintf function call after the		/
//					if((MoveToPoint, all is now done in the DoDebug function/
//					NewFormat()												/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(tmpbuf1 - ErrorMessage). Changed	/
//					the function call strrstr to FindStringRight. Deleted	/
//					the declaration of the char tmpbuf1, the function call	/
//					to memset and the sprintf function call after the call	/
//					for(i = 0;, all is now done in the DoDebug function		/
//					call. Deleted the call to sprintf and the tmpbuf[0] =	/
//					'\0'; after the strrstr(), the DoDebug now does this.	/
//					OldFormat()												/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the name of some of the variables to correctly	/
//					reflect what they are(tmpbuf1 - ErrorMessage). Changed	/
//					the function call strrstr to FindStringRight. Added code/
//					to free allocated memory if an error happened and also	/
//					after the use of it was no longer needed (DifferentUUT)	/
//					Deleted the declaration of the char tmpbuf1, the		/
//					function call to memset and the sprintf function call	/
//					after the call to strrstr, all is now done in the		/
//					DoDebug function call. Deleted the call to sprintf and	/
//					the tmpbuf[0] =	'\0'; after the strrstr(), the DoDebug	/
//					now does this.											/
//					ExtractInfo()											/
//					Corrected the info header to correctly reflect the		/
//					function, also modified some of the source comments to	/
//					correct the errors being made. Changed the way the		/
//					variables are written(camel back style). Changed the way/
//					the string variablesare zero'd out, the prior way didn't/
//					do anything, now using memset to zero the variables		/
//					Changed the malloc to calloc to initialize the variable	/
//					to all null characters. Changed the name of some of the	/
//					variables to correctly reflect what they are (faultfz -	/
//					FaultFileStatPt; fffp - FaultFilePt; tmpbuf -			/
//					ErrorMessage). Added a test to see if a variable needed	/
//					its' memory freed (FaultfileContents). Deleted the		/
//					declaration of the char tmpbuf, the function call to	/
//					memset and the sprintf function call after the call		/
//					if((num_of_this, all is now done in DoDebug function.	/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <windows.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <direct.h>
#include <fcntl.h>
#include <errno.h>
#include <time.h>
#include <malloc.h>
#include <ctype.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "gpconcur.h"

/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

FAULTFILE	FaultFp;

GET_THIS	GetThis[] = {

//	StartPoint				EndPoint				Size				
{	"START",				"TEST ***",				TPS_NAME_SIZE		},
{	"ERO:",					"\015\012",				ERO_NUM_SIZE		},
{	"UUT P/N:",				"\015\012",				PART_NUM_SIZE		},
{	"UUT S/N:",				"\015\012",				SERIAL_NUM_SIZE		},
{	":\\",					"\\",					APS_NAME_SIZE		}
};

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// RemoveSpaces:	This function will remove any white spaces from either  /
//						the left or right direction up to the first non		/
//						white space. This function will either increment or /
//						decrement the IndexPointer's value.					/
//																			/
// Parameters:																/
//		IndexPointer:		This is the address to the integer value which	/
//							is requested to be changed.						/
//		FaultFileContents:	This is the character string where we remove the/
//							white spaces.									/
//		Direction:			This is the direction in which the white spaces	/
//							are removed.									/
//																			/
// Return:																	/
//		none:				This is a void function. Always works (right).	/
//																			/
/////////////////////////////////////////////////////////////////////////////

void RemoveSpaces(int *IndexPointer, char *FaultFileContents, int Direction)
{

	while (FaultFileContents[*IndexPointer] == '\040' ||
		   FaultFileContents[*IndexPointer] == '\011') {
		if (Direction == FORWARD) {
			(*IndexPointer)++;
		}
		else {
			(*IndexPointer)--;
		}
	}

}

/////////////////////////////////////////////////////////////////////////////
// GetItem:			This function will extract the information that is		/
//						between the StartPoint and the EndPoint and check	/
//						to see if the size is less then the getthis[item]	/
//						.size.  If all is well then it will return to the	/
//						calling process the string that it found, else NULL./
//																			/
// Parameters:																/
//		FaultFileContents:	This is the character string that the tps name	/
//							will come from.									/
//		Item:				The index value used in the getthis structure,	/
//							of what we are looking for.						/
//																			/ 
// Return:																	/
//		TmpBuf:			A character pointer to the string that was found	/
//						successfully.										/
//		NULL:			What is return when it didn't go well. (An Error)	/
//																			/
/////////////////////////////////////////////////////////////////////////////

char *GetItem(char *FaultFileContents, int Item)
{

	int		TmpIndex, IndexPos = 0;
	char	*TmpBuf;

	if (MoveToPoint(GetThis[Item].StartPoint,
					  &IndexPos, FaultFileContents, IndexPos) == GP_ERROR) {
		return(NULL);
	}

	RemoveSpaces(&IndexPos, FaultFileContents, FORWARD);

	TmpIndex = IndexPos;

	if (FindPoint(GetThis[Item].EndPoint, &IndexPos,
				   FaultFileContents, IndexPos) == GP_ERROR) {
		return(NULL);
	}

	if (IndexPos <= TmpIndex) {
		return(NULL);
	}

	RemoveSpaces(&IndexPos, FaultFileContents, BACKWARD);

	if (((IndexPos - TmpIndex) + 1) > GetThis[Item].Size) {

		DoDebug(0, "GetItem()", "%s%s%d%s%d", GetThis[Item].StartPoint,
				" is larger then the buffer, only ", GetThis[Item].Size,
				" is allowed but you want ", (IndexPos - TmpIndex) + 1, (char*)NULL);
		return(NULL);
	}

	if ((TmpBuf = (char *)calloc((IndexPos - TmpIndex) + 2, sizeof(char))) == NULL) {
		DoDebug(errno, "GetItem()", (char*)NULL);
		return(NULL);
	}

	strncpy(TmpBuf, &FaultFileContents[TmpIndex], (IndexPos - TmpIndex) + 1);
	TmpBuf[(IndexPos - TmpIndex) + 1] = '\0';

	return(TmpBuf);
}

/////////////////////////////////////////////////////////////////////////////
// GetInfo:			This function will call the required functions to		/
//						extract the info and insert it into the data		/
//						structure.  If the info to be extracted is required	/
//						to be present in the InfoString is not present then	/
//						the function returns no_info and sets the element	/
//						InfoGood to NO_INFO.								/
//																			/
// Parameters:																/
//		InfoString:		This is the character string the info may be found	/
//						in.													/
//		Required:		This flag is determines if the item should be there./
//		APSName:		TRUE  = Look up the aps name.						/
//						FALSE = Don't look up the aps name.					/
//																			/
// Return:																	/
//		SUCCESS:		The item was found if it was Required.				/
//		NO_INFO:		No serial number could be found there for there is	/
//						no info.											/
//																			/
/////////////////////////////////////////////////////////////////////////////

int GetInfo(char *InfoString, int Required, int APSName)
{

	char	*TmpBuf;

	FaultFp.InfoGood = SUCCESS;

//
// First we will see if the serial number is listed.  This is for the old format mainly.
// If there is no serial number then there is no reason to save any data, also it is not
// really an error either, the old format doesn't list the serial number until at least
// a module is run.
//

	if ((TmpBuf = GetItem(InfoString, TPS_SN)) == NULL && Required == YES) {
		DoDebug(0, "GetInfo()", "%s", "Failed to get the serial number, perhaps it ain't there", (char*)NULL);
		FaultFp.InfoGood = NO_INFO;
		return(NO_INFO);
	}

	sprintf(FaultFp.Serial, "%s", TmpBuf == NULL && Required == NO ? "none" : TmpBuf);

	if (TmpBuf != NULL) {
		free(TmpBuf);
	}

//
// Now get the date from the character string and convert it to a usable format
// for the data base, also give it a somewhat believable year.
//

	if (GetDateTime(InfoString) == GP_ERROR && Required == YES) {
		DoDebug(0, "GetInfo()", "%s", "Failed to get the date/time info. look at file", (char*)NULL);
		FaultFp.InfoGood = NO_INFO;
		return(NO_INFO);
	}

//
// Now lets see if the Test Program Name can be gotten.  This will be the name
// from the character string, not the one that is on the CD.  I write the program
// therefore I make the rules, like or not that is the way it is.
//

	if ((TmpBuf = GetItem(InfoString, TPS_NAME)) == NULL && Required == YES) {
		DoDebug(0, "GetInfo()", "%s", "Failed to get the TPS name info. look at file", (char*)NULL);
		FaultFp.InfoGood = NO_INFO;
		return(NO_INFO);
	}

	sprintf(FaultFp.TPSName, "%s", TmpBuf == NULL && Required == NO ? "none" : TmpBuf);

	if (TmpBuf != NULL) {
		free(TmpBuf);
	}

//
// Next we will try to get the part number of the TPS, it may or may not be
// here, it depends on which shell the TPS programmer used.
//

	if ((TmpBuf = GetItem(InfoString, TPS_PN)) == NULL && Required == YES) {
		DoDebug(0, "GetInfo()", "%s", "Failed to get the part number info. look at file", (char*)NULL);
		FaultFp.InfoGood = NO_INFO;
		return(NO_INFO);
	}

	sprintf(FaultFp.PartNumber, "%s", TmpBuf == NULL && Required == NO ? "none" : TmpBuf);

	if (TmpBuf != NULL) {
		free(TmpBuf);
	}

//
// Next we will try to get the ero number, but this is only there if the program was
// done with the tps shell ver2.3 or higher.
//

	if ((TmpBuf = GetItem(InfoString, TPS_ERO)) == NULL && Required == YES) {
		DoDebug(0, "GetInfo()", "%s", "Failed to get the ERO number info. look at file", (char*)NULL);
		FaultFp.InfoGood = NO_INFO;
		return(NO_INFO);
	}

	sprintf(FaultFp.ERONumber, "%s", TmpBuf == NULL && Required == NO ? "none" : TmpBuf);

	if (TmpBuf != NULL) {
		free(TmpBuf);
	}

//
// Finally we will get the APS name if there is one.  In the old format way it will be
// impossible to find APS name, this is due to the fact the APS name is only available
// on the APS CD.
//

	TmpBuf = NULL;

	if (APSName) {

		char	DirPath[GP_MAX_PATH];

		memset(DirPath, '\0', sizeof(DirPath));

		if ((_getcwd(DirPath, GP_MAX_PATH)) == NULL) {
			DoDebug(errno, "get_info()", (char*)NULL);
			return(GP_ERROR);
		}

		if (DirPath[(strlen(DirPath) - 1)] == '\\') {
			DirPath[(strlen(DirPath) - 1)] = '\0';
		}

		if ((TmpBuf = GetItem(DirPath, APS_NAME)) == NULL && Required == YES) {
			DoDebug(0, "GetInfo()", "%s", "Failed to get the APS name info. look at CDROM", (char*)NULL);
			FaultFp.InfoGood = NO_INFO;
			return(NO_INFO);
		}
		sprintf(FaultFp.APSName, "%s", TmpBuf);

		if (TmpBuf != NULL) {
			free(TmpBuf);
		}
	}
	else {
		if ((TmpBuf = SearchForAPSName(FaultFp.TPSName, FaultFp.PartNumber)) == NULL) {
			FaultFp.InfoGood = NO_INFO;
			return(NO_INFO);
		}
		sprintf(FaultFp.APSName, "%s", TmpBuf);
	}


	return(SUCCESS);
}


/////////////////////////////////////////////////////////////////////////////
// DoMonthYear:		This function will convert the month that was passed as	/
//						a character string to an integer.  The function		/
//						will then determine what year should be inserted	/
//						into the structure, based on the current date and	/
//						the date of the file.								/
//																			/
// Parameters:																/
//		MonthSent:		The is the 3 letter abrev for the month.			/
//																			/
//	Return:																	/
//		SUCCESS:		The month was converted.							/
//		GP_ERROR:		The month passed was incorrect, thanks BILL.		/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	DoMonthYear(char *MonthSent)
{

	int			i;
	time_t		CurrentTimePt;
	struct tm	*TimeStructPt;

	char		*Month[] = { "", "Jan", "Feb", "Mar",
								 "Apr", "May", "Jun",
								 "Jul", "Aug", "Sep",
								 "Oct", "Nov", "Dec", NULL };

	for (i = 1; Month[i] != NULL; i++) {

		if (!strncmp(MonthSent, Month[i], strlen(Month[i]))) {

			char	CurrentTime[30];

			memset(CurrentTime, '\0', sizeof(CurrentTime));

			time(&CurrentTimePt);

			sprintf(CurrentTime, "%s", ctime(&CurrentTimePt));

			TimeStructPt = localtime(&CurrentTimePt);

			if ((i - 1) > TimeStructPt->tm_mon ||
			   ((i -1) == TimeStructPt->tm_mon && FaultFp.DateTime.day > TimeStructPt->tm_mday)) {
				FaultFp.DateTime.year = (short)((TimeStructPt->tm_year + 1900) - 1);
			}
			else {
				FaultFp.DateTime.year = (short)(TimeStructPt->tm_year + 1900);
			}

			FaultFp.DateTime.month = (unsigned short)i;

			break;
		}
	}

	return(Month[i] == NULL ? GP_ERROR : SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// GetDateTime:		This function will convert the date/time it finds in	/
//						the character string that was passed to it. If the	/
//						time is 00:00:0 then this function will make the	/
//						time 00:00:10, this is done because MicroSlop		/
//						access db pukes on it.  If it can't find or			/
//						properly convert the date/time then it will return	/
//						an error.											/
//																			/
// Parameters:																/
//		FaultFileContents:	This is the character string the search will be	/
//							performed on.									/			
//																			/
// Return:																	/
//		SUCCESS:		The date/time was found and converted, and the		/
//						structure was updated.								/
//		GP_ERROR:		The date/time could not be found or converted.		/
//																			/
/////////////////////////////////////////////////////////////////////////////

int GetDateTime(char *FaultFileContents)
{

	int		ScanReturn, DatePointPt, CRLFPointPt, IndexPt = 0;
	char	day[4], month[4], year[5];
	char	*TmpBuf;

	memset(day, '\0', sizeof(day));
	memset(month, '\0', sizeof(month));

//
// First we will move to the point after *** START and the to TEST ***, this should be the
// first line in the character string.  The next line should be the date/time string we
// are after.
//

	if (MoveToPoint("*** START", &IndexPt, FaultFileContents, IndexPt) == GP_ERROR) {
		return(GP_ERROR);
	}

	if (MoveToPoint("TEST ***", &DatePointPt, FaultFileContents, IndexPt) == GP_ERROR) {
		return(GP_ERROR);
	}

	IndexPt = DatePointPt + 4;

//
// Now find the end of the line of the date/time.
//

	if (MoveToPoint("\015\012", &CRLFPointPt, FaultFileContents, IndexPt) == GP_ERROR) {
		return(GP_ERROR);
	}

	if ((TmpBuf = (char *)calloc((CRLFPointPt - DatePointPt) + 1, sizeof (char))) == NULL) {
		DoDebug(errno, "GetDateTime()", (char*)NULL);
		return(GP_ERROR);
	}

//
// Copy from the begining of the line, hopefully, to the end of the line into TmpBuf.
//

	strncpy(TmpBuf, &FaultFileContents[DatePointPt], CRLFPointPt - DatePointPt);
	TmpBuf[(CRLFPointPt - DatePointPt)] = '\0';

//
// Now convert the date time string into its values and insert them into the structure.
//

	if ((ScanReturn = sscanf(TmpBuf, "%s%s%d%d:%d:%d %s", day, month, &FaultFp.DateTime.day,
					&FaultFp.DateTime.hour, &FaultFp.DateTime.min, &FaultFp.DateTime.sec, year)) != 7) {

		DoDebug(0, "GetDateTime()", "%s%d%s", "sscanf only converted ", ScanReturn,
				" and needed to do 6 conversions", (char*)NULL);

		if (TmpBuf != NULL) {
			free(TmpBuf);
		}

		return(GP_ERROR);
	}

	if ((FaultFp.DateTime.hour + FaultFp.DateTime.min + FaultFp.DateTime.sec) == 0) {
		FaultFp.DateTime.sec = 10;
	}

	if (TmpBuf != NULL) {
		free(TmpBuf);
	}
//
// Here we will call the function that will convert the month into a integer and set the
// correct year for this report ticket.  Also we will check for date that equates to
// 29 Feb, and change it to 28 Feb to prevent access database for causing an error.
//

	if (DoMonthYear(month)) {
		return(GP_ERROR);
	}
	else {
		if (FaultFp.DateTime.month == 2 && FaultFp.DateTime.day == 29) {
			FaultFp.DateTime.day -= 1;
		}
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// NumberOfThis:This function will find the total number of passed string	/
//					that are in the charater string FaultFileContents being	/
//					passed to it. This function will loop through looking	/
//					for the character string passed to it and increment the	/
//					variable MunberOfThis everytime it finds one. If the	/
//					check_compare is set true then it compare from the		/
//					first character of LookForThis to the end of the line	/
//					to see if they are the same and if increment is set		/
//					true then the function will increment the count. This	/
//					is then the return value ofthe function, as long as		/
//					there were no errors encountered.						/
//																			/
// Parameters:																/
//		FaultFileContents:	This is the character string the search is		/
//							being done on.									/
//		LookForThis:		This is what we are searching for.				/
//																			/
// Return:																	/		
//		numofthis:			This is the total number of LookForThis	that	/
//							were found.										/
//																			/
/////////////////////////////////////////////////////////////////////////////

int NumberOfThis(char *FaultFileContents, char *LookForThis)
{


	int	IndexPt, StartPoint = 0, CheckReturn, MunberOfThis = 0;

	CheckReturn = TEST_TRUE;
	IndexPt     = StartPoint;

	while (CheckReturn == TEST_TRUE) {

		if ((CheckReturn = MoveToPoint(LookForThis, &IndexPt,
			 FaultFileContents, StartPoint)) != GP_ERROR) {
			MunberOfThis++;
			StartPoint = IndexPt;
		}
	}

	return(MunberOfThis);
}

/////////////////////////////////////////////////////////////////////////////
// FindStringRight:	This function will find the location of the string passed/
//					in CheckString in the string FaultFileContents			/
//					begining at location of StartPoint.  If the string is	/
//					found within the string then the value of the IndexPt	/
//					is set to the location of the first character of the	/
//					string CheckString. This function works on the string	/
//					from the right to left direction.						/
//																			/
// Parameters																/
//		CheckString:		This is the string that is being searched for	/
//							in the character string.						/
//		FaultFileContents:	This is the character string that the search is	/
//							being done on.									/
//		StartPoint:			This is the starting point where we look for	/
//							CheckString.									/
//		CaseType:			This is used to determine if the case is		/
//							ignored.										/
//																			/
// Return																	/
//		NULL:				The CheckString was not found.					/
//		pointer:			A pointer to the first character in the			/
//							character string that match CheckString.		/
//																			/
/////////////////////////////////////////////////////////////////////////////

char *FindStringRight(char *CheckString, char *FaultFileContents, int StartPoint, int CaseType)
{

	int	i, TmpFileChar, TmpCheckChar;

//
// If StartPoint = -1 then we start at the end of the string else the starting point was passed.
// But we will do a little checking first to make sure the value passed is legal.
//

	if (StartPoint == -1) {
		StartPoint = strlen(FaultFileContents) - 1;
	}

	else if (StartPoint > (int)strlen(FaultFileContents) - 1) {

		DoDebug(0, "FindStringRight()", "%s%d%s%d",
				"You want me to start looking at this position in the string - ",
				StartPoint, " but the string is only this long - ",
				(int)strlen(FaultFileContents) - 1, (char*)NULL);
		return(NULL);
	}


	else {

		StartPoint = StartPoint > (int)strlen(FaultFileContents) - ((int)strlen(CheckString) - 1) ?
				   (int)strlen(FaultFileContents) - ((int)strlen(CheckString) - 1) : StartPoint;
		
		if (StartPoint <= 0) {
			return(NULL);
		}
	}

//
// Now we will loop backwards looking for the character string, CheckString, int the character
// string FaultFileContents.  We will look for the starting character then if it matches check
// for the string. If the string is found then IndexPt will be set to this location.
//

	if (isalpha(CheckString[0]) && islower(CheckString[0])) {
		TmpCheckChar = toupper(CheckString[0]);
	}
	else {
		TmpCheckChar = CheckString[0];
	}


	for (i = StartPoint; i != 0; i--) {

		if (CaseType == DISREGARD) {
			if (isalpha(FaultFileContents[i]) && islower(FaultFileContents[i])) {
				TmpFileChar = toupper(FaultFileContents[i]);
			}
			else {
				TmpFileChar = FaultFileContents[i];
			}

			if (TmpFileChar == TmpCheckChar) {
				if (!_strnicmp(FaultFileContents + i, CheckString, strlen(CheckString))) {
					break;
				}
			}
		}
		else {
					
			if (FaultFileContents[i] == CheckString[0]) {
				if (!strncmp(FaultFileContents + i, CheckString, strlen(CheckString))) {
					break;
				}
			}
		}
	}

	return(i == 0 ? NULL : FaultFileContents + i);
}


/////////////////////////////////////////////////////////////////////////////
// FindPoint:		This function will find the location of the string		/
//						passed in CheckString in the string					/
//						FaultFileContents begining at location of StartPoint/
//						If the string is found within the string then the	/
//						value of the IndexPt is set to the location of the	/
//						first character of the string CheckString.			/	
//																			/
// Parameters																/	
//		CheckString:		This is the string that is being searched for	/
//							in the character string.						/
//		FaultFileContents:	This is the character string that the search is	/
//							being done on.									/
//		IndexPt:			This is the address to the integer value which	/
//							is requested to be changed.						/
//		StartPoint:			This is the starting point where we look for	/
//							CheckString.									/
//																			/
// Return																	/
//		SUCCESS:			 0  - The CheckString was found and the			/
//								  IndexPt was set to its address index		/
//								  value.									/	
//		GP_ERROR:			-1  - The CheckString wasn't found, IndexPt		/
//								  value is left unchanged.					/
//																			/
/////////////////////////////////////////////////////////////////////////////

int FindPoint(char *CheckString, int *IndexPt, char *FaultFileContents, int StartPoint)
{

	char	*PointFound;


	 if ((PointFound = strstr( &FaultFileContents[StartPoint], CheckString)) != NULL) {
		*IndexPt = PointFound - (FaultFileContents + 1);
	}

	else {

		DoDebug(0, "FindPoint()", "%s%s%s", "Failed to find CheckString (",
				CheckString, ") in the passed character string", (char*)NULL);
		return(GP_ERROR);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// MoveToPoint:		This function will find the location of the string		/
//						passed in CheckString in the string					/
//						FaultFileContents begining at location of StartPoint/
//						If the string is found within the string then the	/
//						value of the IndexPt is set to the location of one	/
//						character past the last character of the string		/
//						CheckString.										/
//																			/
// Parameters																/
//		CheckString:		This is the string that is being searched for	/
//							in the character string.						/
//		FaultFileContents:	This is the character string that the search is	/
//							being done on.									/	
//		IndexPt:			This is the address to the integer value which	/
//							is requested to be changed.						/
//		StartPoint:			This is the starting point where we look for	/
//							CheckString.									/
//																			/
// Return																	/
//		SUCCESS:		 0  - The CheckString was found and the IndexPt		/
//							  was set to its address index value.			/
//		GP_ERROR:		-1  - The CheckString wasn't found, IndexPt value	/
//							  is left unchanged.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

int MoveToPoint(char *CheckString, int *IndexPt, char *FaultFileContents, int StartPoint)
{

	char	*PointFound;


	if ((PointFound = strstr( &FaultFileContents[StartPoint], CheckString)) != NULL) {
		*IndexPt = PointFound - FaultFileContents;
		*IndexPt += (size_t)strlen(CheckString);
	}

	else {

		DoDebug(0, "MoveToPoint()", "%s%s%s", "Failed to find CheckString (",
				CheckString, ") in the passed character string", (char*)NULL);
		return(GP_ERROR);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// GotoTermNumber:		This function will find go to the line after the	/
//							requested TERMINATE in the character string		/
//							that was passed to it.  If the requested		/
//							TERMINATE was found then the IndexPt that was	/
//							passed to it will be set to this location.		/
//																			/
// Parameters																/
//		FaultFileContents:	This is the character string that the search is	/
//							being done on.									/
//		TermNumber:			This is the terminate number that this function	/
//							is looking for.									/
//		IndexPt:			This is the address to the integer value which	/
//							is requested to be changed.						/
//																			/
// Return																	/
//		SUCCESS:		 0  - The CheckString was found and the IndexPt		/
//							  was set to its address index value.			/
//		GP_ERROR:		-1  - The CheckString wasn't found, IndexPt value	/
//							  is left unchanged.							/
//																			/
/////////////////////////////////////////////////////////////////////////////

int GotoTermNumber(char *FaultFileContents, int TermNumber, int *IndexPt)
{

	int	CheckReturn, NumberOfTerms = 0, StartIndex = 0;

	CheckReturn = TEST_TRUE;

	while (CheckReturn == TEST_TRUE && NumberOfTerms < TermNumber) {

		if ((CheckReturn = MoveToPoint("TERMINATE", IndexPt, FaultFileContents,
										  StartIndex)) != GP_ERROR) {
			StartIndex = *IndexPt;
			NumberOfTerms++;
		}
	}

	if (NumberOfTerms == TermNumber && CheckReturn == TEST_TRUE) {

		if (MoveToPoint("***\015\012", IndexPt, FaultFileContents, StartIndex) == GP_ERROR) {
			return(GP_ERROR);
		}
	}
	else {

		DoDebug(0, "GotoTermNumber()", "%s%d%s", "You want to go to term number - ",
				TermNumber, " but it can't be found", (char*)NULL);
		return(GP_ERROR);
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// NewFormat:	This function will parse the the string faultfileconts.  If	/
//					the Type is set to PRINTIT, then it will parse just the	/
//					last run of the UUT, else parse all runs of the Test	/
//					Program.  If the FAULT-FILE contains more then one Type	/
//					of test program runs then this function only handles	/
//					the last tp run.										/
//																			/
// Parameters																/
//		FaultFileContents:	This is the character string that the search is	/
//							being done on.									/
//																			/
// Return																	/
//		SUCCESS:	 0  - The CheckString was found and the IndexPt was		/
//						  set to its address index value.					/
//		GP_ERROR:	-1  - The CheckString wasn't found, IndexPt value is	/
//						  left unchanged.									/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	NewFormat(char *FaultFileContents, int Type)
{

	int		i, NumberOfRuns, IndexPt;
	char	*UUTRun;

//
// First we will find out how many times a different serial numbers of the UUT that were run.
// If there was only one UUT then life will be easy, else get the number of UUTs that were ran.
//

	if ((NumberOfRuns = NumberOfThis(FaultFileContents, "UUT S/N:")) == 0) {
		DoDebug(0, "NewFormat()", "%s",
			    "This can't happen unless you have a non standard ATLAS program", (char*)NULL);
		return(GP_ERROR);
	}

	NumberOfRuns = Type == PRINTIT ? NumberOfRuns - (NumberOfRuns - 1) : NumberOfRuns;

	for (i = 0; i < NumberOfRuns; i++) {

		char	*TmpBuf;

		if ((TmpBuf = FindStringRight("UUT S/N:", FaultFileContents, -1, KEEP)) == NULL) {
			DoDebug(0, "NewFormat()", "%s%d%s%d", "This can't possibly happen, I just found ",
					NumberOfRuns, " UUT S/Ns and now I can't find UUT S/N #", i, (char*)NULL);
			return(GP_ERROR);
		}

		IndexPt = (TmpBuf - FaultFileContents);

		if ((TmpBuf = FindStringRight(" *** START", FaultFileContents, IndexPt, KEEP)) == NULL) {

			DoDebug(0, "NewFormat()", "%s%s", "Something is very wrong in the tester world ",
					"I am looking for *** START and I didn't find it, not good.", (char*)NULL);
			return(GP_ERROR);
		}

		IndexPt = (TmpBuf - FaultFileContents) - 1;

		if ((UUTRun = _strdup(&FaultFileContents[IndexPt])) == NULL) {
			DoDebug(errno, "NewFormat()", (char*)NULL);
			return(GP_ERROR);
		}

		FaultFileContents[IndexPt] = '\0';

		UUTRun[strlen(UUTRun) - 1] = '\0';

		GetInfo(UUTRun, NO, YES);

		if ((FaultFp.FileInfo = _strdup(UUTRun)) == NULL) {
			DoDebug(errno, "OldFormat()", (char*)NULL);
			FaultFp.InfoGood = NO_INFO;
		}


		if (FaultFp.InfoGood != NO_INFO) {
			FillInDataBase();
		}

		FaultFileContents[IndexPt] = '\0';

		if (UUTRun != NULL) {
			free(UUTRun);
		}

		if (FaultFp.FileInfo != NULL) {
			free(FaultFp.FileInfo);
		}

	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// OldFormat:	This function will parse the string FaultFileContents. This	/
//					function is use	when there are more then one type of	/
//					test program run in the FAULT-FILE. The parsing is		/
//					started at the next to last TERMINATE in the FAULT-FILE,/
//					and works on the last in first out rule working from	/
//					the last TERMINATE in the string FaultFileContents		/
//					working towards postion 0 in the string.  This function	/
//					will move the end of the string foward as it finishes	/
//					with the section it was parsing.						/
//																			/
// Parameters																/
//		FaultFileContents:	This is the character string that the search is	/
//							being done on.									/
//		TermNumber:		This is the terminate number that this function	/
//							is looking for.									/
//																			/
// Return																	/
//		SUCCESS:	 0  - The CheckString was found and the IndexPt was		/
//						  set to its address index value.					/
//		GP_ERROR:	-1  - The CheckString wasn't found, IndexPt value is	/
//						  left unchanged.									/
//																			/
/////////////////////////////////////////////////////////////////////////////

int	OldFormat(char *FaultFileContents, int NumberOfTerms)
{

	int		i, j, NumberOfRuns, IndexPt, TermNumber;
	char	*UUTRun, **DifferentUUT;

	
	if ((DifferentUUT = (char **)malloc(sizeof(char *) * NumberOfTerms)) == NULL) {
		DoDebug(errno, "OldFormat()", (char*)NULL);
	}

	for (j = 1; j < NumberOfTerms; j++) {

		if ((TermNumber = NumberOfTerms - j) > 0) {

			if ((GotoTermNumber(FaultFileContents, TermNumber, &IndexPt)) == GP_ERROR) {

				for (j = j - 1; j > 1; j--) {
					if (DifferentUUT[j - 1] != NULL) {
						free(DifferentUUT[j - 1]);
					}
				}
				return(GP_ERROR);
			}

			if ((DifferentUUT[j - 1] = _strdup(&FaultFileContents[IndexPt])) == NULL) {
				DoDebug(errno, "OldFormat()", (char*)NULL);

				for (j = j - 1; j > 1; j--) {
					if (DifferentUUT[j - 1] != NULL) {
						free(DifferentUUT[j - 1]);
					}
				}
				return(GP_ERROR);
			}

			FaultFileContents[IndexPt] = '\0';
		}
	}

	if ((DifferentUUT[j - 1] = _strdup(FaultFileContents)) == NULL) {
		DoDebug(errno, "OldFormat()", (char*)NULL);

		for (j = j - 1; j > 1; j--) {
			if (DifferentUUT[j - 1] != NULL) {
				free(DifferentUUT[j - 1]);
			}
		}
		return(GP_ERROR);
	}

//
// First we will find out how many times a different serial numbers of the UUT that were run.
// If there was only one UUT then life will be easy, else get the number of UUTs that were ran.
//

	for (j = 0; j < NumberOfTerms; j++) {

		if ((NumberOfRuns = NumberOfThis(DifferentUUT[j], "UUT S/N:")) == 0) {
			DoDebug(0, "OldFormat()", "%s",
				    "This can't happen unless you have a non standard ATLAS program", (char*)NULL);
		}

		for (i = 0; i < NumberOfRuns; i++) {

			char	*TmpBuf;

			if ((TmpBuf = FindStringRight("UUT S/N:", DifferentUUT[j], -1, KEEP)) == NULL) {

				DoDebug(0, "OldFormat()", "%s%d%s%d", "This can't possibly happen, I just found ",
						NumberOfRuns, " UUT S/Ns and now I can't find UUT S/N #", i, (char*)NULL);
				break;
			}

			IndexPt = (TmpBuf - DifferentUUT[j]);

			if ((TmpBuf = FindStringRight(" *** START", DifferentUUT[j], IndexPt, KEEP)) == NULL) {

				DoDebug(0, "OldFormat()", "%s%s", "Something is very wrong in the tester world ",
						"I am looking for *** START and I didn't find it, not good.", (char*)NULL);
				break;
			}

			IndexPt = (TmpBuf - DifferentUUT[j]) - 1;

			if ((UUTRun = _strdup(&DifferentUUT[j][IndexPt])) == NULL) {
				DoDebug(errno, "OldFormat()", NULL);
				break;
			}

			DifferentUUT[j][IndexPt] = '\0';

			UUTRun[strlen(UUTRun) - 1] = '\0';

			GetInfo(UUTRun, NO, NO);

			if ((FaultFp.FileInfo = _strdup(UUTRun)) == NULL) {
				DoDebug(errno, "OldFormat()", NULL);
				FaultFp.InfoGood = NO_INFO;
			}

			if (FaultFp.InfoGood != NO_INFO) {
				FillInDataBase();
			}

			if (UUTRun != NULL) {
				free(UUTRun);
			}

			if (FaultFp.FileInfo != NULL) {
				free(FaultFp.FileInfo);
			}
		}
		
		if (DifferentUUT[j] != NULL) {
			free(DifferentUUT[j]);
		}
	}

	return(SUCCESS);
}

/////////////////////////////////////////////////////////////////////////////
// ExtractInfo:	This function will find the number of TERMINATES in the		/
//						string FaultFile that was passed to it, and			/
//						depending on the Type that was passed to it, will	/
//						either due the cleanup function or printit function./
//						If the Type is CLEANUP and the number of terms is	/
//						greater then 1 then the OldFormat function must be	/
//						called, if the number of terms is not at least 1	/
//						and	CLEANUP was requested then error.  If the the	/
//						Type was PRINTIT and the number of terms is not 0	/
//						then move the index to just after the last			/
//						terminate in the string.  If the Type is CLEANUP	/
//						and there is only 1 terminate the call NewFormat.	/
//																			/
// Parameters																/
//		FaultFile:			This is the file that the info is being			/
//							extracted from.									/
//		Type:				This is the option of the function the program	/
//							is performing.									/
//																			/
// Return																	/
//		SUCCESS:	 0  - The FaultFile didn't contain the correct info or	/
//						  the FaultFile was incorrect format.				/
//		GP_ERROR:	-1  - The CheckString wasn't found, IndexPt value is	/
//						  left unchanged.									/
//																			/
/////////////////////////////////////////////////////////////////////////////

int ExtractInfo(char *FaultFile, int Type)
{

	int				TermNumber;
	int				IndexPos = 0, NumberOfTerms = 0;
	char			*FaultFileContents, *FileSaveInfo;
	struct _stat	FaultFileStatPt;
	FILE			*FaultFilePt;
 
//
// First we will insure that the processor has written to the hard drive so that the information
// that we need to parse out is there before we open the file. Once we open the file then we will
// malloc the space required to hold the entire file.  All work will be preformed on this
// character string and not on the file, should speed things up a little.
//

	fflush(NULL);

	if ((FaultFilePt = fopen(FaultFile, "rb")) == NULL) {
		DoDebug(FILE_OPEN_ERROR, "ExtractInfo()", (char*)NULL);
		return (GP_ERROR);
	}

	if ((_stat(FaultFile, &FaultFileStatPt)) != GP_ERROR) {
		if ((FaultFileContents = (char *)calloc((size_t)FaultFileStatPt.st_size, sizeof(char))) != NULL) {
			if ((fread(FaultFileContents, sizeof(char), (size_t)FaultFileStatPt.st_size,
					   FaultFilePt)) != (size_t)FaultFileStatPt.st_size) {

				if (FaultFileContents != NULL) {
					free(FaultFileContents);
				}

				DoDebug(FILE_READ_ERROR, "ExtractInfo()", (char*)NULL);
				fclose(FaultFilePt);
				return(GP_ERROR);
			}
			FaultFileContents[(size_t)FaultFileStatPt.st_size] = '\0';
		}
		else {
			DoDebug(errno, "ExtractInfo()", (char*)NULL);
			fclose(FaultFilePt);
			return(GP_ERROR);
		}
	}
	else {
		DoDebug(errno, "ExtractInfo()", (char*)NULL);
		fclose(FaultFilePt);
		return(GP_ERROR);
	}

//
// First lets find out just how many times this file has been written to, we will do this by
// looking for the number of terminates in the file.  There should only be one if all the
// TPS have been developed using ver 2.3 shell or higher.  If there is more then 1 then some
// of the info is in the old format.
//

	if ((NumberOfTerms = NumberOfThis(FaultFileContents, "TERMINATE")) == 0 && Type == CLEANUP) {

		DoDebug(0,"ExtractInfo()", "The string is (%s)", FaultFileContents, (char*)NULL);
		DoDebug(0,"ExtractInfo()", "The size was %d", (size_t)FaultFileStatPt.st_size, (char*)NULL);

		if (FaultFileContents != NULL) {
			free(FaultFileContents);
		}

		fclose(FaultFilePt);

		return(GP_ERROR);
	}

//
// Now we will see if there is more then one terminate if so we will first move to the next to
// last terminate and copy from that point to the end of the character string into a save buf.
// Then we null the FaultFileContents string there and extract the info in the old format.
// After we are thru we will first free FaultFileContents and then copy save buffer back into
// this character string and the next section will continue from there.  That is all.
//

	if (Type == CLEANUP) {

		if ((TermNumber = NumberOfTerms - 1) > 0) {

			if ((GotoTermNumber(FaultFileContents, TermNumber, &IndexPos)) == GP_ERROR) {

				if (FaultFileContents != NULL) {
					free(FaultFileContents);
				}
				
				fclose(FaultFilePt);
				return(GP_ERROR);
			}

			if ((FileSaveInfo = (char *)malloc(IndexPos + 2)) == NULL) {
				DoDebug(errno, "ExtractInfo()", (char*)NULL);

				if (FaultFileContents != NULL) {
					free(FaultFileContents);
				}
				
				fclose(FaultFilePt);
				return(GP_ERROR);
			}

			strncpy(FileSaveInfo, FaultFileContents, IndexPos);
			FileSaveInfo[strlen(FileSaveInfo)] = '\0';

//
// Now we will send the character string off to be parsed in the old format way.
//

			if (OldFormat(FileSaveInfo, TermNumber) == GP_ERROR) {
				DoDebug(0, "ExtractInfo()", "%s", "Failed in the OldFormat somehow", (char*)NULL);
				fclose(FaultFilePt);
				
				if (FaultFileContents != NULL) {
					free(FaultFileContents);
				}
				
				if (FileSaveInfo != NULL) {
					free(FileSaveInfo);
				}
				return(GP_ERROR);
			}

			if (FileSaveInfo != NULL) {
				free(FileSaveInfo);
			}
		}
	}
	else {

		if (NumberOfTerms != 0) {
		
			if ((GotoTermNumber(FaultFileContents, NumberOfTerms, &IndexPos)) == GP_ERROR) {

				if (FaultFileContents != NULL) {
					free(FaultFileContents);
				}
				
				fclose(FaultFilePt);
				return(GP_ERROR);
			}
		}
		else {

			DoDebug(0, "ExtractInfo()", "Failed to find any TERMINATEs, test program not run yet",
					(char*)NULL);
			if (FaultFileContents != NULL) {
				free(FaultFileContents);
			}
			
			fclose(FaultFilePt);
			return(GP_ERROR);
		}
	}

//
// Here we will send the character string off to be parsed in the new format way.  If there was
// info that was in the old format, it has been removed and stored in the data base.  This
// function will do it for the info in the new format way.
//

	if (NewFormat(&FaultFileContents[IndexPos], Type) == GP_ERROR) {
		DoDebug(0, "ExtractInfo()", "%s", "Failed in the NewFormat somehow", (char*)NULL);

		if (FaultFileContents != NULL) {
			free(FaultFileContents);
		}
		
		fclose(FaultFilePt);
		return(GP_ERROR);
	}

	if (FaultFileContents != NULL) {
		free(FaultFileContents);
	}

	fclose(FaultFilePt);

	return(SUCCESS);

}
