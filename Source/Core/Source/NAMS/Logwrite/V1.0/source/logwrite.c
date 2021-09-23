/*	File: logwrite.c
 *	
 *  This program is used as a NAM call from an ATLAS program.  The ATLAS program
 *	passes an argument which is the name of the local log file.  The NAM program
 *	Then examinees the ATLAS program name.TMP file and extracts the argument and
·*	Passes this argument to the Syslog.exe for amendment onto the Syslog.txt.
 *
 *	File Creation Date: 5/12/98
 *  Version: 1.0
 *  Created By: Grady Johnson
 *  Last Revision by:
 *
 *	Revision Log:
 *	
 *		2.0.0.0		Combined Source from Astronics with						
 *					source from VIPERT 1.3.2.0.  																		
 *
 */

#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <visa.h>
#include <process.h>
#include "nam.h"
#include <Windows.h>

#define SUFFIX ".TMP"
#define SYSLOG_FILE "C:\\Program Files (x86)\\ATS\\LOG\\SYSLOG.EXE"  
#define STRING_SIZE 4096

char LocLogName[STRING_SIZE];
long LocLogNameAddress;
char TempFileName[40];
ViSession vi;

int count;

void main (int argc, char *argv[])
{
	/* Define Variables */
	ViString LocLogFile = "";
	ViString LocLogFile2 = "";
	
	/* Define ATLAS temp file name and open it */
	strcpy(TempFileName, argv[1]);
	strcat(TempFileName, SUFFIX);
	vmOpen (TempFileName);
	
	//MessageBox(NULL, "Log Write Main", "Note", MB_OK);  //This is used for debugging purposes, do not remove
	/* Copy passed arguments */
	LocLogNameAddress = atol(argv[2]);
	vmGetText(LocLogNameAddress, LocLogName, STRING_SIZE);
	strcpy(LocLogFile, LocLogName);

	/* shell to the Syslog.exe program */
	printf ("Name of Log file is : %s", LocLogFile);
	execl(SYSLOG_FILE, SYSLOG_FILE, LocLogFile, NULL);

	
	/* close temp file and application */
	vmClose();

	exit (0);


} /* End main */


