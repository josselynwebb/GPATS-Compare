// SVN Information
// $Author:: wileyj             $: Author of last commit
//   $Date:: 2020-07-06 16:01:5#$: Date of last commit
//    $Rev:: 27851              $: Revision of last commit
/****************************************************************************
 *	File:	PLCParesUtilities.c												*
 *																			*
 *	Creation Date:	14 Oct 2008												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

#include <sys/types.h>
#include <sys/stat.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include "visa.h"
#include "tets.h"

char    path_id[SWX_USED][SWX_LENGTH]; //made path_id multi-dimensional QN 09/27/00

//#define SWX_USED 78
//#define SWX_LENGTH 6


/****************************************************************************
*		External Variables and Routines										*
****************************************************************************/

/****************************************************************************
*		Local Constants														*
****************************************************************************/

/****************************************************************************
*		Globals																*
****************************************************************************/

double	ResPLCValue[3][16][48];
double	RFS8PLCValue[32][10];
double	RFS9PLCValue[36][85];

char	*BLK1[] = {
	"MOD1000", "MOD2000", "MOD2100", "MOD2200", "MOD2300",
	"MOD2400", "MOD2500", "MOD3000", "MOD3100", "MOD3200",
	"MOD4000", "MOD4100", "MOD4200", "MOD4300", "MOD4400",
	NULL,
};

char	*BLK2[] = {
	"MOD1000", "MOD2000", "MOD2100", "MOD2200", "MOD2300",
	"MOD2400", "MOD2500", "MOD3000", "MOD3100", "MOD3200",
	"MOD4000", "MOD4100", "MOD4200", "MOD4300", "MOD4400",
	NULL,
};

char	*BLK3[] = {
	"MOD0",  "MOD1",  "MOD2",  "MOD3",
	"MOD4",  "MOD5",  "MOD6",  "MOD7",
	"MOD8",  "MOD9",  "MOD10", "MOD11",
	"MOD12", "MOD13", "MOD14", "MOD15",
	NULL,
};

char	*Switch8[] = {
	"S801-2",  "S801-3",  "S801-4",  "S801-5",
	"S801-6",  "S801-7",  "S801-8",  "S801-9",
	"S802-2",  "S802-3",  "S802-4",  "S802-5",
	"S802-6",  "S802-7",  "S802-8",  "S802-9",
	"S803-2",  "S803-3",  "S803-4",  "S803-5",
	"S803-6",  "S803-7",  "S803-8",  "S803-9",
	"S804-2",  "S804-3",  "S804-4",  "S804-5",
	"S804-6",  "S804-7",  "S804-8",  "S804-9",
	NULL,
};

char	*Switch9[] = {
	"S901-2", "S901-3", "S901-4", "S901-5", "S901-6", "S901-7",
	"S902-2", "S902-3", "S902-4", "S902-5", "S902-6", "S902-7",
	"S903-2", "S903-3", "S903-4", "S903-5", "S903-6", "S903-7",
	"S904-2", "S904-3", "S904-4", "S904-5", "S904-6", "S904-7",
	"S905-2", "S905-3", "S905-4", "S905-5", "S905-6", "S905-7",
	"S906-2", "S906-3", "S906-4", "S906-5", "S906-6", "S906-7",
	NULL,
};

/*
 * fillInPLCTable:		This function will find the location of the path loss compensation
 *						file using the GetWindowsDirectory and GetPrivateProfileString.  The
 *						name of path loss table is passed to it.  The function will open the
 *						file, allocate the required memory buffer and read in the path loss
 *						table.  The function will then return a pointer to the allocated
 *						memory in which the path loss table resides.
 *
 * Parameters
 *		dataTable:		This is the name of the path loss table that needs to be retrieved.
 *
 * Return
 *		char*:			A char pointer to the location of the path loss table.
 *		NULL:			An error occured during the retrieval of the path loss table..
 *
 */

char *fillInPLCTable(char *dataTable)
{

	//int				LenBuf;
	char			tmpBuf[256], IniBuf[256];
	char			*table;
	FILE			*tmpfp;
	struct _stat	faultfz;

    strcpy_s( tmpBuf, sizeof(tmpBuf), "C:\\Users\\Public\\Documents\\ATS" );
	//if ((LenBuf = GetWindowsDirectory(tmpBuf, sizeof(tmpBuf))) != 0) {
	
		sprintf_s(IniBuf, sizeof(IniBuf), "%s%s",tmpBuf, ININAME);
	//}

	ZeroMemory(tmpBuf, 256);

	GetPrivateProfileString("File Locations", dataTable, "x", tmpBuf, 256, IniBuf);

	if ((fopen_s( &tmpfp, tmpBuf, "rb")) != 0) {
	
		return (NULL);
	}
			
	if ((_stat(tmpBuf, &faultfz)) != (-1)) {
				
		if ((table = (char *)calloc((size_t)faultfz.st_size + 128, sizeof(char))) != NULL) {
				
			if ((fread(table, sizeof(char), (size_t)faultfz.st_size,
			   tmpfp)) != (size_t)faultfz.st_size) {
						
				if (table != NULL) {
						
					free(table);
				}

				fclose(tmpfp);
				return(NULL);
			}
					
			table[(size_t)faultfz.st_size] = '\0';
		}
		else {
			
			fclose(tmpfp);
			return(NULL);
		}

	}
	else {

		fclose(tmpfp);
		return(NULL);
	}

	return (table);
}

/*
 * parseResPLCDataTable:	This function will first look to see if the character is 
 *							proper by checking t he front end of the character array.
 *							Then the function will then parase the array into segments
 *							that relate to the block part of a switch triplett.  The
 *							will then pass the character array and the address to the
 *							index of the begining of the block to parseFillinResValue.
 *
 * Parameters
 *		PLCDataTable:		This is the character array containing the path loss
 *							compensation table.
 *
 * Return
 *		0:				Success, everything worked as stated.
 *		(-1):			An error occured during the parsing of the path loss table..
 *
 */

int parseResPLCDataTable(char *PLCDataTable)
{

	int		i, FirstNumber;
	char	*charLoc;

	if ((charLoc = strstr(PLCDataTable, STARTPOINT)) - PLCDataTable != 0) {
		return(-1);
	}

	for (i = 0; PLCDataTable[i] != '\0'; i++) {

		if (PLCDataTable[i] == 'B' &&
		   (!_strnicmp(&PLCDataTable[i], BLOCK1, (size_t)strlen(BLOCK1)))) {

			FirstNumber = 0;

			parseFillinResValue(&i, PLCDataTable, FirstNumber);
		}
		else if (PLCDataTable[i] == 'B' &&
				(!_strnicmp(&PLCDataTable[i], BLOCK2, (size_t)strlen(BLOCK2)))) {

			FirstNumber = 1;

			parseFillinResValue(&i, PLCDataTable, FirstNumber);
		}
		if (PLCDataTable[i] == 'B' &&
		   (!_strnicmp(&PLCDataTable[i], BLOCK3, (size_t)strlen(BLOCK3)))) {

			FirstNumber = 2;

			parseFillinResValue(&i, PLCDataTable, FirstNumber);
		}
	}
	return 0;
}

/*
 * parseFillinResValue:		This function will parse through the character array
 *							table that was passed to it, starting at the location
 *							of idx in the character array.  The function will loop
 *							through the character array looking for the MOD and PTH
 *							part of the switch triplett.  Upon finding the MOD part
 *							of the triplett, the function will look through a 
 *							character array of strings that match the searchString.
 *							The index into this character array of strings will be
 *							the index value of the y element of ResPLCValue variable.
 *							The x element is the value of the variable blk that was
 *							passed to it.  Also Upon finding the PTH part of the 
 *							switch triplett the function will convert the ascii #
 *							of the path to the z element of the variable ResPLCValue.
 *							The function will convert the ascii value of the compensation
 *							for this path into a double at the xyz of ResPLCValue.
 *							The looping will break upon finding the BLK part or the
 *							end of the character array.
 *
 * Parameters
 *		idx:				This is the address of the variable that is used to index
 *							into the character array table.
 *		table:				This is the character array containing the path loss
 *							compensation table to be parsed.
 *		blk:				This is the value of the x element of the variable ResPLCValue
 *
 * Return
 *		none:				Void function
 *
 */

void parseFillinResValue(int *idx, char *table, int blk)
{

	int		i, j, FirstNumber = 0, SecondNumber = 0, ThirdNumber = 0;
	char	searchString[10];

	FirstNumber = blk;

	for (i = *idx; table[i] != '\0'; i++) {

		if (table[i] == 10 && (!_strnicmp(&table[i + 1], "MOD", (size_t)strlen("MOD")))) {

			for (;;i++){
				if (table[i] == ' ') {
					break;
				}
			}
			
			sprintf_s(searchString, sizeof(searchString), "MOD%d", atoi(&table[i]));
			
			for (j = 0;
				 blk == 0 ? BLK1[j] != NULL : blk == 1 ? BLK2[j] != NULL : BLK3[j] != NULL;
				 j++) {

				if (!strncmp(blk == 0 ? BLK1[j] : blk == 1 ? BLK2[j] : BLK3[j],
							 searchString, (size_t)strlen(searchString))) {
					SecondNumber = j;
					break;
				}
			}
		}
		else if (table[i] == 10 && (!_strnicmp(&table[i + 1], "PTH", (size_t)strlen("PTH")))) {

			for (;;i++){
				if (table[i] == ' ') {
					break;
				}
			}
			
			ThirdNumber = atoi(&table[i]);
			i++;
			
			for (;;i++){
				if (table[i] == ' ') {
					break;
				}
			}

			ResPLCValue[FirstNumber][SecondNumber][ThirdNumber] = atof(&table[i]);
		}
		else if (table[i] == 10 && (!_strnicmp(&table[i + 1], "BLK", (size_t)strlen("BLK")))) {
			break;
		}
	}

	*idx = i;
}

/*
 * parsePLCDataTables:		This function will call the functions to load the two 
 *							path loss tables, and then parse them.
 *
 * Parameters
 *		none:
 *
 * Return
 *		0:				Success, everything worked as stated.
 *		(-1):			An error occured during the exection of the task.
 *
 */

int initPLCDataTables(void)
{

	int 	rtnval;
	char	*PLCDataTable;

	if ((PLCDataTable = fillInPLCTable(RESPLCDATA)) != NULL) {

		rtnval = parseResPLCDataTable(PLCDataTable);
		free(PLCDataTable);

		if (rtnval) {
			return(-1);
		}
	}
	else {
		return(-1);
	}

	if ((PLCDataTable = fillInPLCTable(RFPLCDATA)) != NULL) {

		parseRFPLCDataTable(PLCDataTable);
		free(PLCDataTable);
	}
	else {
		return(-1);
	}

	return 0;
}

/*
 * parseRFPLCDataTable:		This function will loop through the character string
 *							arrays Scwitch8 and Switch9, doing a string search
 *							from the begining of the character array PLCDataTable.
 *							Once found the function then looks far a carriage return.
 *							From the return of strstr to the location of the carriage
 *							return, this portion of the character string is transfered
 *							to a tmpString for further parsing.  From here we will use
 *							strtok function to further parse the table.  The parsed
 *							elements will be converted and stored into the correct 2
 *							dimemnitional variables RFS8PLCValue or RFS9PLCValue.
 *							After elements have been parsed the functions returns.
 *
 * Parameters
 *		PLCDataTable:		This is the character array containing the path loss
 *							compensation table.
 *
 * Return
 *		none:				Void function
 *
 */

void parseRFPLCDataTable(char *PLCDataTable)
{

	int		i, j, indx;
	char	*StringIndx, *token;
	char * tokloc = NULL;
	char	tmpString[750];

	for (i = 0; Switch8[i] != NULL; i++) {

		StringIndx = strstr(PLCDataTable, Switch8[i]);

		for (j = (StringIndx - PLCDataTable); PLCDataTable[j] != '\0'; j++) {

			if (PLCDataTable[j] == CARRIAGE_RETURN) {

				ZeroMemory(tmpString, (size_t)sizeof(tmpString));
				_snprintf_s(tmpString, sizeof(tmpString), (size_t)(j - (StringIndx - PLCDataTable)), "%s",
						  &PLCDataTable[StringIndx - PLCDataTable]);
				tmpString[j - (StringIndx - PLCDataTable)] = '\0';
				token = strtok_s(tmpString, ",", &tokloc);
				token = strtok_s(NULL, ",", &tokloc);
				indx = 0;

				while(token != NULL) {
					RFS8PLCValue[i][indx] = atof(token);
					indx++;
					token = strtok_s(NULL, ",", &tokloc);
				}

				break;
			}
		}
	}

	for (i = 0; Switch9[i] != NULL; i++) {

		StringIndx = strstr(PLCDataTable, Switch9[i]);

		for (j = (StringIndx - PLCDataTable); PLCDataTable[j] != '\0'; j++) {

			if (PLCDataTable[j] == CARRIAGE_RETURN) {

				ZeroMemory(tmpString, (size_t)sizeof(tmpString));
				_snprintf_s(tmpString, sizeof(tmpString), (size_t)(j - (StringIndx - PLCDataTable)), "%s",
						  &PLCDataTable[StringIndx - PLCDataTable]);
				tmpString[j - (StringIndx - PLCDataTable)] = '\0';
				token = strtok_s(tmpString, ",", &tokloc);
				token = strtok_s(NULL, ",", &tokloc);
				indx = 0;

				while(token != NULL) {
					RFS9PLCValue[i][indx] = atof(token);
					indx++;
					token = strtok_s(NULL, ",", &tokloc);
				}

				break;
			}
		}
	}
}

double GetPLC(double FreqValue, char switchesUsed[SWX_USED][SWX_LENGTH])
{

	int			i, count;
	double 		compValue;
	char 		switchPath[100][10];


	ZeroMemory(switchPath, (size_t)sizeof(switchPath));

	compValue = 0.0;

	//CJWfor(count = 0; count < (int)(strlen((switchesUsed[0])) / 8); count++) {
	for(count = 0; switchesUsed[count][0] != 0; count++) {

		strncpy_s(switchPath[count],sizeof(switchPath),&switchesUsed[count][0], 7);
	}

	for (i = 0; i < count; i++) {

		int	j;
		int		stringCmp, indxPoint;

		stringCmp = (_strnicmp(switchPath[i], "S8", 2));

		for (j = 0; stringCmp == 0 ? Switch8[j] != NULL: Switch9[j] != NULL; j++) {
		
			if (stringCmp == 0 ? (!_strnicmp(switchPath[i], Switch8[j], 6)) :
								 (!_strnicmp(switchPath[i], Switch9[j], 6))) {
				FreqValue = stringCmp != 0 ? FreqValue : FreqValue > 500e6 ? 500e6 : FreqValue;
				indxPoint = (int) ((FreqValue - (stringCmp == 0 ? 25e6 : 50e6)) /
								   (stringCmp == 0 ? 5e7 : 1e8));
				compValue += stringCmp == 0 ? RFS8PLCValue[j][indxPoint] :
											  RFS9PLCValue[j][indxPoint];
				break;
			}
		}
	}

	return(compValue);
}


double GetResPLC(int switchTriplett[][3], int numberOfSwitches)
{

	int		i = 0;
	double	compValue;

	compValue = 0.0;

	for (i = (numberOfSwitches - 1); i >= 0; i--) {

		int firstNo, secondNo = 0, thirdNo, j;
		char	tmpString[10];

		firstNo = (switchTriplett[i][1]) - 1;
		sprintf_s(tmpString, sizeof(tmpString), "MOD%d", switchTriplett[i][2]);

		for (j = 0;
			 firstNo == 0 ? BLK1[j] != NULL : firstNo == 1 ? BLK2[j] != NULL : BLK3[j] != NULL;
			 j++) {

			if (!strncmp(firstNo == 0 ? BLK1[j] : firstNo == 1 ? BLK2[j] : BLK3[j],
						 tmpString, (size_t)strlen(tmpString))) {
				secondNo = j;
				break;
			}
		}

		thirdNo = switchTriplett[i][3];
		compValue += ResPLCValue[firstNo][secondNo][thirdNo];
	}
	
	return(compValue);
}

