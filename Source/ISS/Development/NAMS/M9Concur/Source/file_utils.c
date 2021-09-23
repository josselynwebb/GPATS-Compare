/****************************************************************************
 *	File:	file_utils.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		1.0		Assigned it a version number.								*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

#pragma warning(disable : 4115)
#include <windows.h>
#pragma warning(default : 4115)
#include <io.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <fcntl.h>
#include <errno.h>
#include <time.h>
#include <malloc.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <ctype.h>
#include <direct.h>
#include "visa.h"
#include "visatype.h"
#include "m9concur.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 * fileexten:		This function will check and make sure that the dtb and
 *						circuit file have the proper file extensions. This
 *						function will also see if they are available.
 *
 * Parameters:
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */
int fileexten(void)
{
	char	*lexp;
	FILE	*tmpfp;

	//Check to see if there was an extension passed and if so remove it.
	if ((lexp = strrchr(dtb_info.dtb_file, '.')) != NULL)
	{
		*lexp = '\0';
	}
	
	strcat(dtb_info.dtb_file, DTBSUFFIX);

	if ((tmpfp = fopen(dtb_info.dtb_file, "rb")) == NULL)
	{
		char	tmpbuf[256];
		sprintf(tmpbuf, "The dtb file listed doesn't exists - %s", dtb_info.dtb_file);
		dodebug(0, "fileexten()", tmpbuf);
		dtb_info.dtb_errno = NO_DTB_FILE;
		return(M9_ERROR);
	}

	fclose(tmpfp);
	return(SUCCESS);
}

/*
 * check_header:	This function will check and make sure that the magic
 *						header is correct and in the proper location.
 *
 * Parameters:
 *		file_name:		This is the file that will be checked.
 *		magic_header:	This is what should be there.
 *		start_point:	This is the starting point of the check, if (-1) then
 *						start at the first none-white space character.
 *		length:			This is the length of the header.
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */

#pragma warning(disable : 4701)

int check_header(char *file_name, char *magic_header, int start_point, int length)
{

	int				had_error = 0;
	char			*filecontents;
	FILE			*tmpfp;
	struct _stat	fz;

	/*Open the file with the b option otherwise stat and fread doesn't work
	like it is suppose to*/
	if ((tmpfp = fopen(file_name, "rb")) == NULL) 
	{
		dodebug(0, "check_header()", "fopen failed to open file_name");
		return(M9_ERROR);
	}

	/*Find the size of the file and allocate memory for it, then store it to be 
	munipulated a little later. If fread doesn't read the correct amount then 
	errno will be set to indicate the error and an ERROR will be return'd */
	if (_stat(file_name, &fz) != M9_ERROR) 
	{
		if ((filecontents = malloc((size_t)fz.st_size)) != NULL) 
		{
			if ((fread(filecontents, sizeof(char), (size_t)fz.st_size, tmpfp)) != (size_t)fz.st_size) 
			{
				free(filecontents);
				dodebug(FILE_READ_ERROR, "check_header()", NULL);
				had_error++;
			}
		}
		else 
		{
			dodebug(0, "check_header()", "malloc failed to allocated the required memory");
			had_error++;
		}
	}
	else 
	{
		dodebug(errno, "check_header()", NULL);
		had_error++;
	}

	if (had_error) 
	{
		fclose(tmpfp);
		return (M9_ERROR);
	}

	/*Check the file to see if it has the required info in it and it,
	is in the proper place. If start_point = (-1) then we will
	move the start_point to the first none-white character and go from there.*/
	if (start_point == (-1)) 
	{
		int i;

		for (i = 0; i < fz.st_size; i++)
		{
			if (!isspace(filecontents[i])) 
			{
				start_point = i;
				break;
			}
		}
		if (i == fz.st_size) 
		{
			start_point = i;
		}
	}

	if (_strnicmp(&filecontents[start_point], magic_header, length)) 
	{
		dodebug(FILE_FORMAT_ERROR, "check_header()", NULL);
		had_error++;
	}
	
	free(filecontents);
	fclose(tmpfp);

	if (had_error) 
	{
		return(M9_ERROR);
	}

	return(SUCCESS);

}

#pragma warning(default : 4701)

/*
 * compare_command:		This function will compare the command string passed
 *							to it against the character string array
 *							cmd_string, and return the index of the match, else
 *							if no match, return ERROR.
 *
 * Parameters:
 *		command:		This is the command string from the file passed to m910nam.
 *
 * Returns:
 *		index value:	The index of the cmd_string of the match.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */
int compare_command(char *command)
{
	int		index;
	char	*cmd_string[] = {
				"dtb_file",
				"reset",
				"diag_type",
				"keep_pin_state",
				"concurrent",
				"circuit_file",
				NULL };

	for (index = 0; cmd_string[index] != NULL; index++) 
	{
		if (!_stricmp(command, cmd_string[index]))
		{
			break;
		}
	}

	if (cmd_string[index] == NULL) 
	{
		index = M9_ERROR;
	}

	return(index);
}

/*
 * fill_in_struct_data:	This function fill in the different data structures
 *							using the command_name value and the argument
 *							string.  There is error checking done for proper
 *							useage and proper strings.
 *
 * Parameters:
 *		command_name:	This is an integer value representing the command.
 *		argument:		This is the argument string right out of the file.
 *
 * Returns:
 *		SUCCESS:		  0	 - successful execution of this function.
 *		M9_ERROR:		(-1) - improper info passed for arguments.
 *
 */
int fill_in_struct_data(int command_name, char *argument)
{
	char			*testpt;
	testpt = strchr(argument, '=');

	if (testpt != NULL) 
	{
		dodebug(FILE_FORMAT_ERROR, "fill_in_struct_data()", NULL);
		return(M9_ERROR);
	}

	/*First we will do the ones that are allowed to have no arguments.
	These will cause their defaults to be set. */
	if (argument == NULL) 
	{
		switch(command_name)
		{

			case RESET_DTI:
				dtb_info.reset_flag = 1;
				break;

			case DIAG_TYPE:
				dtb_info.diag_type = NO_DIAG;
				break;

			case PIN_STATE:
				dtb_info.pin_state = 0;
				break;

			case CONCURRENT:
				dtb_info.concurrent = 0;
				break;

			default:
				break;

		}
	}

	//Now do the ones only if the argument is not NULL.
	if (argument != NULL) 
	{
		char	dirpath[M9_MAX_PATH];

		if ((_getcwd(dirpath, M9_MAX_PATH)) == NULL)
		{
			dodebug(errno, "fill_in_struct_data()", NULL);
			dtb_info.dtb_errno = BURST_NOT_RUN;
			return(M9_ERROR);
		}

		if (dirpath[(strlen(dirpath) - 1)] == '\\')
		{
			dirpath[(strlen(dirpath) - 1)] = '\0';
		}

		switch(command_name) 
		{
			case DTB_FILE_NAME:
				if ((argument[0] == '\\') || (argument[0] == '/') || (argument[1] == ':'   &&  argument[2] == '\\')) 
				{
					sprintf(dtb_info.dtb_file, argument);
				}
				else 
				{
					sprintf(dtb_info.dtb_file, "%s\\%s", dirpath, argument);
				}
				break;

			case CIRCUIT_FILE_NAME:
				if (((testpt = strrchr(argument, '\\')) != NULL) ||	((testpt = strrchr(argument, '/' )) != NULL))
				{
					sprintf(dtb_info.cir_file, "%s", testpt+1);
				}
				else 
				{
					sprintf(dtb_info.cir_file, "%s", argument);
				}
				break;

			case RESET_DTI:
				if (!_strnicmp(argument, "y", 1)) 
				{
					dtb_info.reset_flag = 1;
				}
				else if (!_strnicmp(argument, "n", 1)) 
				{
					dtb_info.reset_flag = 0;
				}
				else 
				{
					dodebug(RESET_VALUE, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}
				break;

			case DIAG_TYPE:
				if (!_strnicmp(argument, "n", 1)) 
				{
					dtb_info.diag_type = NO_DIAG;
				}
				else 
				{
					dodebug(DIAG_NOT_ALLOWED, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}
				break;

			case PIN_STATE:
				if (!_strnicmp(argument, "y", 1)) 
				{
					dtb_info.pin_state = 1;
				}
				else if (!_strnicmp(argument, "n", 1)) 
				{
					dtb_info.pin_state = 0;
				}
				else 
				{
					dodebug(PIN_STATE, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}
				break;

			case CONCURRENT:
				if (!_strnicmp(argument, "y", 1)) 
				{
					dtb_info.concurrent = 1;
				}
				else if (!_strnicmp(argument, "n", 1)) 
				{
					dtb_info.concurrent = 0;
				}
				else 
				{
					dodebug(CONCURRENT, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}
				break;

			default:
				break;
		}
	}
	return(SUCCESS);
}

/*
 * parse_file:		This function will parse the file that was passed as an
 *						argument to the m910nam.  The function will check for
 *						proper file format. Also the variables and/or structures
 *						will be filled, they will however will not be checked
 *						for proper info. That is done later, I guess.
 *
 * Parameters:
 *		file_name:		This is the file that will be checked.
 *		start_point:	This is the starting point of the check.
 *		white_space:	This will require that the start_point to begin after any
 *						leading white spaces are accounted for, this was done because
 *						TYX along with Microslop adds a white space when a file is
 *						created. A 1 will cause the deletion of leading white spaces,
 *						and a 0 will do nothing.
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */

int parse_file(char *file_name, int start_point, int white_spaces)
{
	int				i;
	int				read_size = 0;
	int				cmd_is_allocd = 0;
	int				arg_is_allocd = 0;
	int				filecontent_allocd = 0;
	int				had_error = 0;
	char			*str1pt;
	char			*str2pt;
	char			*cmd_string;
	char			*arg_string;
	char			*filecontents;
	FILE			*tmpfp;
	struct _stat	fz;

	/*Open the file with the b option otherwise stat and fread doesn't work
	like it is suppose to*/
	if ((tmpfp = fopen(file_name, "rb")) == NULL) 
	{
		dodebug(0, "parse_file()", "fopen failed to open file_name");
		return(M9_ERROR);
	}

	/*Find the size of the file and allocate memory for it, then store it to be
	munipulated a little later. If fread doesn't read the correct amount then
	errno will be set to indicate the error and an ERROR will be return'd*/
	if (_stat(file_name, &fz) != M9_ERROR) 
	{
		if ((filecontents = malloc((size_t)fz.st_size)) != NULL) 
		{
			if ((size_t)(read_size = fread(filecontents, sizeof(char), (size_t)fz.st_size, tmpfp)) != (size_t)fz.st_size) 
			{
				free(filecontents);
				dodebug(FILE_READ_ERROR, "parse_file()", NULL);
				had_error++;
			}
			else 
			{
				filecontent_allocd++;
			}
		}
		else 
		{
			dodebug(0, "parse_file()", "malloc failed to allocated the required memory");
			had_error++;
		}
	}
	else 
	{
		dodebug(errno, "parse_file()", NULL);
		had_error++;
	}

	if (had_error) 
	{
		fclose(tmpfp);
		return (M9_ERROR);
	}

	/*Check the file to see if it has the required info in it and is in the proper place. 
	If start_point = (-1) then we will move the start_point to the first none-white 
	character and go from there.*/
	if (white_spaces) 
	{
		for (i = 0; i < fz.st_size; i++)
		{
			if (!isspace(filecontents[i])) 
			{
				start_point += i;
				break;
			}
		}
		if (i == fz.st_size) 
		{
			start_point = i;
		}
	}

	/*Check the file to see what info is in it and if it is properly
	formatted. Any comparision is done ignoring the case.  The checking is started after the
	magic header info block.*/
	for (i = start_point; i < read_size; i++) 
	{
		int	index, is_argstring;

		/*The first thing in the file after the magic header info should be an alpha
		character.  If it is an alpha character then find the length by locating the =
		and subtracting the 2 pointers. If no = then error out. With the difference 
		alloc the memory for the character string, also checking for proper structure.
		If all is good then the command is loaded into cmd_string. i is incremented to
		the proper value.*/
		if (isalpha(filecontents[i]))
		{
			str1pt = filecontents + i;
			str2pt = strchr(str1pt, '=');
			if (str2pt != NULL) 
			{
				if ((cmd_string = malloc((size_t)(str2pt - str1pt) + 1)) != NULL)
				{
					cmd_is_allocd = 1;
					strncpy(cmd_string, str1pt, str2pt - str1pt);
					cmd_string[str2pt - str1pt] = '\0';
					i += (str2pt - str1pt) + 1;

					/*Call the compare function to see if the command is a legal command, if
					so then index is set to the command value, else error out.*/					
					if ((index = compare_command(cmd_string)) == M9_ERROR) 
					{
						dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
						had_error++;
						break;
					}
				}
				else 
				{
					dodebug(0, "parse_file()", "malloc failed to allocated the required memory");
					had_error++;
					break;
				}
			}
			else 
			{
				dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
				had_error++;
				break;
			}

			/*After the command string and the = sign comes the argument string, which can contain
			';', /\a-z0-9 as its first character and goes to a ';'. If these 2 parameters are met, the
			length of the argument string is found by subtracting the 2 pointers.  With the
			difference alloc the memory for the arg string.  If all is good then the argument is
			loaded into the arg_string. i is incremented to the proper value.*/
			if (isalnum(filecontents[i]) || filecontents[i] == '\\' || filecontents[i] == '/')
			{
				str1pt = filecontents + i;
				str2pt = strchr(str1pt, ';');
				if (str2pt != NULL) 
				{
					if ((arg_string = malloc((size_t)(str2pt - str1pt) + 1)) != NULL) 
					{
						arg_is_allocd = 1;
						strncpy(arg_string, str1pt, str2pt - str1pt);
						arg_string[str2pt - str1pt] = '\0';
						i += (str2pt - str1pt) + 1;
						is_argstring = TRUE;
					}
					else 
					{
						dodebug(0, "parse_file()", "malloc failed to allocated the required memory");
						had_error++;
						break;
					}
				}
				else 
				{
					dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
					had_error++;
					break;
				}
			}

			/*The ';' was used right after the = sign, so the default will be used if any.
			This is allowed due to the fact that the programmer may be using a template file
			and only filling in what he/she needs, so I will allow this to be done.*/
			else if (filecontents[i] == ';') 
			{
				is_argstring = FALSE;
			}
			else 
			{
				dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
				had_error++;
				break;
			}

			/*Now pass the command index number and the argument string if any to the function for
			proper use and error checking.*/
			if(fill_in_struct_data(index, is_argstring == FALSE ? NULL : arg_string) == M9_ERROR) 
			{
				had_error++;
				break;
			}

			// Now free if used my 2 buffers for the next if any commands and or argument strings.
			if (cmd_is_allocd)
			{
				free(cmd_string);
				cmd_is_allocd = 0;
			}
			if (arg_is_allocd) 
			{
				free(arg_string);
				arg_is_allocd = 0;
			}
		}
	}

	free(filecontents);
	fclose(tmpfp);

	if (had_error) 
	{
		if (cmd_is_allocd) 
		{
			free(cmd_string);
		}
		if (arg_is_allocd) 
		{
			free(arg_string);
		}
		return(M9_ERROR);
	}

	return(SUCCESS);
}