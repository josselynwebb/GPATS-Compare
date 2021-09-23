/****************************************************************************
 *	File:	file_utils.c													*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *		2.1		Removed the fprintf that was in the fill_in_struct_data		*
 *				function call : case DIAG_OUTPUT_DIRECTORY - else			*
 *				statement.  Was doing a write to an unopened file.  A dumb	*
 *				mistake.													*
 *		2.2		In the function fill_in_struct_data() the case statement	*
 *				for CIRCUIT_FILE, this case statement was modified to first	*
 *				see if the file was either an absolute or relative file. If	*
 *				relative then prefix the file name with the current working	*
 *				directory, else do nothing.									*
 *				It use to remove any path info and just get the file name	*
 *				but this caused the error checking that was added at a		*
 *				latter time to not be able to find the *.cxs file.			*
 *		2.3		In the function create_ide() added the declaration FILE		*
 *				*tmpfp, char dummy info which was filled with an info		*
 *				statement.  Added a comment above the call to the function	*
 *				create file.  The call to the function create file was		*
 *				modified to correct for an error that was caused when a		*
 *				static program is run.  The running of a static program does*
 *				create a .dia file, so error checking is done to see if the *
 *				function call to create fails, if so then see if the file	*
 *				was going to be copied exist.  If it doesn't create it with	*
 *				the info in the character string dummy info, and try the	*
 *				function call create file one more time. If it does	exist	*
 *				then error out.												*
 *		2.5		Added the function call fflush to the end of the function	*
 *				create_file.												*
 *				In the function insert_text, a break statement was added to	*
 *				the for loop where the insertation happens.  Once an error	*
 *				was found I didn't break out of the loop which could cause	*
 *				a Dr. Watson.												*
 *				Corrected the header block of the function					*
 *				check_dtb_cxs_exten.										*
 *				In the function check_dtb_cxs_exten a call to the function	*
 *				convert_files_set_dir was added.  This was done due to the	*
 *				fact that the directory that the digital program resides was*
 *				needed so that the working directory could be changedto that*
 *				prior to execution of the digital burst.					*
 *				Corrected the header block of the function create_ide		*
 *				In the fill_in_struct_data function correct the lower limit	*
 *				check for both PROBE_STABILITY_COUNT and MAX_SEEDING_VALUE	*
 *				the value zero is not allowed.								*
 *				Added the function change_dir, this is used to change to the*
 *				directory of where the digital program or the ATLAS program	*
 *				resides to fix STR#744.										*
 *				Added the convert_files_set_dir function, this will change	*
 *				the .dtb and .cxs files to just the file names with no path	*
 *				info.  It will also set the dtb_info.digital_dir from the	*
 *				path info ofthe full dtb path name.  to fix STR#744			*
 *																			*
 ***************************************************************************/

/****************************************************************************
 *	Include Files															*
 ***************************************************************************/	

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
#include "diagnostics.h"
#include "DM_Services.h"
#include "callbacks.h"
#include "m910nam.h"

/****************************************************************************
 *	Local Constants															*
 ***************************************************************************/

/****************************************************************************
 *	Modules																	*
 ***************************************************************************/

/*
 *	create_file:	This function will create, make a copy of, a file that
 *						is a copy of another file. Simple function.
 *
 *	Parameters:
 *		file_to_copy:	Pointer to the coping file.
 *		file_to_create:	Pointer to the file that needs creation.
 *
 *	Returns:
 *		SUCCESS:	Everything worked, what a concept.
 *		M9_ERROR:	Something happened that shouldn't.  The variable errno
 *					was set to reflect the cause.
 *
 *	errno values:
 *		E2BIG:		Argument list is too big.
 *		ENOENT:		Command interpreter can't be found.
 *		ENOEXEC:	Command interpreter file has invalid format and can't
 *					be executed.
 *		ENOMEM:		Not enough memory, a common problem with M$Slop.
 *		FILENAME:	Improper file name was passed.
 */

#pragma warning(disable : 4701)

int create_file(char *file_to_copy, char *file_to_create)
{

	int				had_error;
	unsigned int	read_size;
	char			*filecontents;
	FILE			*ftcfp, *tmpfp;
	struct _stat	fz;

/*
 * Make sure that all files are written and the info is there before
 * this programs copies the file. Common software practice.
 */

	fflush(NULL);

/*
 * This is a check to insure that proper file names were passed.
 */

	if (file_to_copy[0] == '\0' || file_to_create[0] == '\0') {
		dodebug(FILE_NAME, "create_file()", NULL);
		return(M9_ERROR);
	}

	errno = had_error = 0;

	if ((tmpfp = fopen(file_to_create, "wb")) != NULL) {
		if ((ftcfp = fopen(file_to_copy, "rb")) == NULL) {
			dodebug(0, "create_file()", "fopen failed to open file_to_copy check perms");
			fclose(tmpfp);
			return(M9_ERROR);
		}
	}
	else {
		dodebug(0, "create_file()", "fopen failed to open file_to_create");
		return(M9_ERROR);
	}

	if (_stat(file_to_copy, &fz) != M9_ERROR) {
		if ((filecontents = malloc((size_t)fz.st_size)) != NULL) {
			if ((read_size = fread(filecontents, sizeof(char), (size_t)fz.st_size,
					   ftcfp)) != (size_t)fz.st_size) {
				free(filecontents);
				dodebug(FILE_READ_ERROR, "create_file()", NULL);
				had_error++;
			}
		}
		else {
			dodebug(0, "create_file()", "malloc failed to allocated the required memory");
			had_error++;
		}
	}
	else {
		dodebug(errno, "create_file()", NULL);
		had_error++;
	}

	if (had_error) {
		if (ftcfp != NULL) {
			fclose(ftcfp);
		}
		if (tmpfp != NULL) {
			fclose(tmpfp);
		}
	}

	else {

		if (fwrite(filecontents, sizeof(char), (size_t)fz.st_size,
					tmpfp) != (size_t)fz.st_size) {
			dodebug(FILE_WRITE_ERROR, "create_file()", NULL);
			had_error++;
		}

		free(filecontents);
		fclose(ftcfp);
		fclose(tmpfp);
	}

	fflush(NULL);

	return(had_error == FALSE ? SUCCESS : M9_ERROR);

}

#pragma warning(default : 4701)

/*
 *	insert_text:	This function will insert the text that was passed to it
 *						into the file that was also passed to it at the location
 *						that also passed to it.
 *
 *	Parameters:
 *		file_to_change:	Pointer to the file that getting changed.
 *		location:		Location in the file were it is going.
 *		loc_type:		This is set to LINE or CHARACTER, and will set weither
 *						the insertion will be done on a line or character bases.
 *		test_info:		This is the text that is being inserted.
 *
 *	Returns:
 *		SUCCESS:	Everything worked, what a concept.
 *		M9_ERROR:	Something happened that shouldn't.  The variable errno
 *					was set to reflect the cause.
 *
 *	errno values:
 *		ENOENT:			File or path can't be found.
 *		USAGEERROR:		Improper usage of this function.
 *		FILEWRITEERROR:	Error while writing to a file, number mismatch.
 *		FILEREADERROR:	Error while reading from a file, number mismatch.
 */

#pragma warning(disable : 4701)

 int insert_text(char *file_to_change, long location, int loc_type, char *text_info[])
{

	int		had_error = 0;
	FILE	*ftcfp;

/*
 * Chose to do the hard part first. Need to create some new files to move
 * the contents around so they can be put into the final file.  Made this
 * a little more flexiable then just putting in stuff at the begining and end.
 */

	if (location != END) {

		unsigned int	i;
		char			tmpfilename[M9_MAX_PATH], tmpfile[M9_MAX_PATH];
		char			*lexp, *filecontents;
		time_t			tp;
		struct _stat	fz;
		FILE			*tmpfp;

/*
 * Create the tmp files to allow for insertion of text. tmpfile is used just
 * to strip off the actual file that was passed to it and leave the directory.
 * Then the tmpfilename will be created.
 */

		strcpy(tmpfile, file_to_change);

		time(&tp);

		if ((lexp = strrchr(tmpfile, '\\')) != NULL) {
			lexp++;
			*lexp = '\0';
		}
		else if ((lexp = strrchr(tmpfile, '/')) != NULL) {
			lexp++;
			*lexp = '\0';
		}
		else {
			tmpfile[0] = '\0';
		}

		sprintf(tmpfilename, "%stmp%ld", tmpfile, tp);

		if (create_file(file_to_change, tmpfilename)) {
			return(M9_ERROR);
		}

		if ((tmpfp = fopen(tmpfilename, "rb")) != NULL) {
			if ((ftcfp = fopen(file_to_change, "wb")) == NULL) {
				dodebug(0, "insert_text()", "fopen failed to open file_to_change check write perms");
				fclose(tmpfp);
				return(M9_ERROR);
			}
		}
		else {
			dodebug(0, "insert_text()", "fopen failed to open tmpfilename");
			return(M9_ERROR);
		}

/*
 * Find the size of the file and allocate memory for it, then store it to be
 * munipulated a little later. If fread doesn't read the correct amount then
 * errno will be set to indicate the error and an ERROR will be return'd
 */

		if (_stat(tmpfilename, &fz) != M9_ERROR) {
			if ((filecontents = malloc((size_t)fz.st_size)) != NULL) {
				if ((fread(filecontents, sizeof(char), (size_t)fz.st_size,
						   tmpfp)) != (size_t)fz.st_size) {
					free(filecontents);
					dodebug(FILE_READ_ERROR, "insert_text()", NULL);
					had_error++;
				}
			}
			else {
				dodebug(0, "insert_text()", "malloc failed to allocated the required memory");
				had_error++;
			}
		}
		else {
			dodebug(errno, "insert_text()", NULL);
			had_error++;
		}

		if (had_error) {
			fclose(ftcfp);
			fclose(tmpfp);
			return(M9_ERROR);
		}

/*
 * This is the place where the insertion will happen at the begining of the file,
 * if no errors have happen yet.  However if durning the write an error happens
 * the files will be closed and the memory will be free'd and errno will be set
 * to the error that caused the problem.
 */

		if (location == BEGINING) {

			for (i = 0; text_info[i] != NULL; i++) {
				if ((fwrite(text_info[i], sizeof(char), strlen(text_info[i]),
							ftcfp)) != strlen(text_info[i])) {
					free(filecontents);
					fclose(ftcfp);
					fclose(tmpfp);
					dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
					return(M9_ERROR);
				}
			}
			if ((fwrite(filecontents, sizeof(char), (size_t)fz.st_size,
						ftcfp)) != (size_t)fz.st_size) {
				dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
				had_error++;
			}
		}

/*
 * This is the place where the insertion is not at the begining or the end, but
 * somewhere in between.  First check to see if CHARACTER was selected, if it was
 * then characters will be counted until the character count matches the location.
 * Then the insertion will happen. The same rules as above apply here for errors.
 */

		else if (location != BEGINING) {

			int j, l, done;

			done = 0;

			if (loc_type == CHARACTER) {
				for (i = 0; i < (size_t)fz.st_size && !had_error; i++) {
					if ((int)i == location) {
						for (j = 0; text_info[j] != NULL && !had_error; j++) {
							if ((fwrite(text_info[j], sizeof(char), strlen(text_info[j]),
										ftcfp)) != strlen(text_info[j])) {
								had_error++;
								dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
								break;
							}
						}
						done++;
					}
					if (had_error) {
						break;
					}
					if ((fwrite(&filecontents[i], sizeof(char), 1, ftcfp)) != 1) {
						dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
						had_error++;
						break;
					}
				}
			}

/*
 * Check to see if it was LINE selected, if it was then as the characters from the
 * filecontents are written one at a time to the file they are check'd to see if
 * they are the character '\n' and if so the counter is increased by 1 until the count
 * equals the location passed, then the insertion happens.  The same rules as above
 * apply here for errors.
 */

			else if (loc_type == LINE) {

				l = 0;

				for (i = 0; i < (size_t)fz.st_size && !had_error; i++) {
					if (l == location && !done) {
						for (j = 0; text_info[j] != NULL && !had_error; j++) {
							if (fwrite(text_info[j], sizeof(char), strlen(text_info[j]),
										ftcfp) != strlen(text_info[j])) {
								dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
								had_error++;
								break;
							}
						}
						done++;
					}
					if (had_error) {
						break;
					}
					if (fwrite(&filecontents[i], sizeof(char), 1, ftcfp) != 1) {
						dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
						had_error++;
						break;
					}
					if (filecontents[i] == '\n') {
						l++;
						if (l == location && !done) {
							for (j = 0; text_info[j] != NULL && !had_error; j++) {
								if (fwrite(text_info[j], sizeof(char), strlen(text_info[j]),
											ftcfp) != strlen(text_info[j])) {
									dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
									had_error++;
									break;
								}
							}
							done++;
						}
					}
				}
			}
			else {
				dodebug(USAGE_ERROR, "insert_text()", NULL);
				had_error;
			}
			if (!done) {
				dodebug(0, "insert_text()", "Need to check the character/line value");
				had_error++;
			}
		}

		free(filecontents);
		fclose(tmpfp);
		fflush(ftcfp);
		fclose(ftcfp);
		remove(tmpfilename);

	}

/*
 * This is the place where the insertion happens at the end of the file, the simple
 * one to perform. Same rules apply for ERRORS.
 */

	else {

		int		i;

		if ((ftcfp = fopen(file_to_change, "a+b")) != NULL) {
			for (i = 0; text_info[i] != NULL; i++) {
				if ((fwrite(text_info[i], sizeof(char), strlen(text_info[i]),
							ftcfp)) != strlen(text_info[i])) {
					fclose(ftcfp);
					dodebug(FILE_WRITE_ERROR, "insert_text()", NULL);
					had_error++;
					break;
				}
			}
		}
		else {
			dodebug(0, "insert_text()", "fopen failed to open file_to_change check write perms");
			had_error++;
		}

		if (!had_error) {
			fflush(ftcfp);
			fclose(ftcfp);
		}

	}

	return(had_error == FALSE ? SUCCESS : M9_ERROR);

}

#pragma warning(default : 4701)

/*
 *	change_char:	This function will change a character to another character.
 *						This character change is a one for one exchange that is
 *						passed to this function in a list and what they get
 *						changed to in another list.
 *
 *	Parameters:
 *		file_to_change:	Pointer to the file that needs to get changed.
 *		char_list:		This is the list of characters to change.
 *		change_list:	This is what they will be change to.
 *
 *	Returns:
 *		SUCCESS:	Everything worked, what a concept.
 *		M9_ERROR:	Something happened that shouldn't.  The variable errno
 *					was set to reflect the cause.
 *
 *	errno values:
 *		EACCES:		Improper access to file. Not owner.
 *		ENVAL:		Invalid oflag argument.
 *		ENOENT:		File or path can't be found.
 *		EMFILE:		No more file handles aviable.
 *		LISTERROR:	The size of char_list != size of change_list.
 */

int	change_char(char *file_to_change, char *char_list, char *change_list)
{

	int				fd;
	unsigned int	listlength, i;
	char			ch[1];

	if ((listlength = strlen(char_list)) != strlen(change_list)) {
		dodebug(LIST_ERROR, "change_char()", NULL);
		return(M9_ERROR);
	}

	if ((fd = _open(file_to_change, _O_RDWR | _O_BINARY)) == M9_ERROR) {
		dodebug(errno, "change_char()", NULL);
		_close(fd);
		return(M9_ERROR);
	}

	if ((_lseek(fd, 0L, SEEK_SET)) == M9_ERROR) {
		dodebug(errno, "change_char()", NULL);
		_close(fd);
		return(M9_ERROR);
	}

/*
 * Read the file 1 character at a time comparing it to the character(s) in the
 * char_list.  If there is a match then back up the pointer 1 position and write
 * the character in the change_list in place of the character that was there.
 * That is all it does.
 */

	while (_read(fd, ch, 1) == 1) {
		for (i = 0; i < listlength; i++) {
			if (ch[0] == char_list[i]) {
				if (_lseek(fd, -1L, SEEK_CUR) == M9_ERROR) {
					dodebug(errno, "change_char()", NULL);
					_close(fd);
					return(M9_ERROR);
				}
				ch[0] = change_list[i];
				if (_write(fd, ch, 1) == M9_ERROR) {
					dodebug(errno, "change_char()", NULL);
					_close(fd);
					return(M9_ERROR);
				}
			}
		}
	}
	_close(fd);
	return(SUCCESS);
}


/*
 * exten_routine:		This function will check the string that was passed
 *						to it for the proper extension, that was also passed
 *						to it, and if need be attach the extension if not there.
 *						Also if present replace the exten with the one passed to
 *						it.
 *
 * Parameters:
 *		the_string:		This is the string that we are going to check for the
 *						extension and swap if need be.
 *		exten:			This is the extension that we are looking for on
 *						the_string
 *		new_exten:		This is the extension that we will swap to on the_string.
 *
 * Returns:
 *		the_new_string:	This is pointer to the new string we created.
 *		NULL:			Something very bad happened.
 *
 */

char *exten_routine(char *the_string, char *exten, char *new_exten)
{

	char	*the_new_string;

/*
 * First we will allocate the storage space for the new string variable.
 */

	if ((the_new_string = malloc(strlen(the_string) + strlen(exten) + 2)) == NULL) {
			dodebug(0, "exten_routine()", "malloc failed to allocated the required memory");
			return(the_new_string);
	}

/*
 * Now see if we are to swap the extensions, new_exten should not be NULL.  Then
 * check the fourth from the end character to see if it is a ".".  If it is there
 * then there is an extension on this string.  Then we see if the extension passed
 * is the same that is there before we swap.  If it isn't return NULL, else swap.
 * If no extension present then stick the new one on and send it back.
 */

	if (new_exten != NULL) {

		if (!_strnicmp(the_string + strlen(the_string) - (strlen(exten) + 1), ".", 1)) {

			if (_strnicmp(the_string + strlen(the_string) - strlen(exten),
				exten, strlen(exten))) {
				dodebug(0, "exten_routine()", "The extensions don't match, try again.");
				return(NULL);
			}
			else {
				sprintf(the_new_string, "%s", the_string);
				the_new_string[strlen(the_string) - strlen(exten)] = '\0';
				strcat(the_new_string, new_exten);
			}
		}
		else {
			sprintf(the_new_string, "%s.%s", the_string, new_exten);
		}
	}

/*
 * Here we are just going to see if the extension is present or not, and if it isn't
 * there then stick it on.  That's all folks.
 */

	else {

		if (!_strnicmp(the_string + strlen(the_string) - (strlen(exten) + 1), ".", 1)) {

			if (_strnicmp(the_string + strlen(the_string) - strlen(exten),
				exten, strlen(exten))) {
				dodebug(0, "exten_routine()", "The extensions don't match, try again.");
				return(NULL);
			}
			else {
				sprintf(the_new_string, "%s", the_string);
			}
		}
		else {
			sprintf(the_new_string, "%s.%s", the_string, exten);
		}
	}

	return(the_new_string);

}

/*
 * check_dtb_cxs_exten:	This function will check and make sure that the dtb and
 *						circuit file have the proper file extensions. This
 *						function will also see if they are available.
 *
 * Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */

int check_dtb_cxs_exten(void) {

	char	*stringpt;
	FILE	*tmpfp;

/*
 * Check to see if there was an extension passed, if no extension
 * then add the correct one.
 */

	if ((stringpt = exten_routine(dtb_info.dtb_file, DTBSUFFIX, NULL)) != NULL) {
		dtb_info.dtb_file[0] = '\0';
		sprintf(dtb_info.dtb_file, "%s", stringpt);
	}
	else {
		return(M9_ERROR);
	}

	if ((tmpfp = fopen(dtb_info.dtb_file, "rb")) == NULL) {

		char	tmpbuf[256];

		sprintf(tmpbuf, "The dtb file listed doesn't exists - %s", dtb_info.dtb_file);
		dodebug(0, "check_dtb_cxs_exten()", tmpbuf);
		dtb_info.dtb_errno = NO_DTB_FILE;
		return(M9_ERROR);
	}

	fclose(tmpfp);

/*
 * See if there is going to be any diag and if so check the file extension of the
 * circuit is proper. Also will check to see if the file exists if needed. If not
 * this will be an error.
 */

	if (dtb_info.diag_type > 0) {
		char	tmpcirfn[M9_MAX_PATH];
		FILE	*tmpfp;

		sprintf(tmpcirfn, "%s", dtb_info.cir_file);

		if ((stringpt = exten_routine(tmpcirfn, "CXS", NULL)) != NULL) {
			sprintf(dtb_info.cir_file, "%s", stringpt);
		}
		else {
			return(M9_ERROR);
		}

		if ((tmpfp = fopen(dtb_info.cir_file, "rb")) == NULL) {

			char	tmpbuf[M9_MAX_PATH];

			sprintf(tmpbuf, "The cxs file listed doesn't exists - %s", dtb_info.cir_file);
			dodebug(0, "check_dtb_cxs_exten()", tmpbuf);
			dtb_info.dtb_errno = NO_CIR_FILE;
			return(M9_ERROR);
		}

		dtb_info.cir_file[strlen(dtb_info.cir_file) - 4] = '\0';

		fclose(tmpfp);
	}

	if (convert_files_set_dir()) {
		return(M9_ERROR);
	}

	return(SUCCESS);
}

/*
 * create_ide:		This function will create and format the m910nam.ide file from the
 *						m910nam.dia file.  It will also put the IADS format in the file.
 *
 * Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */

int	create_ide(void)
{


	char	*tmppt;
	char	Extension[5];
	char	file_to_copy[M9_MAX_PATH];
	char	ide_xml_file[M9_MAX_PATH];
	//char	ide_file[M9_MAX_PATH]; // Not Used
	FILE	*tmpfp;

	char	*header_ide_list[] = {
			"<FILE TITLE='M910 DIAGNOSTIC INFORMATION'>\r\n",
			"<FRAME LABEL='Diagnostics'>\r\n",
			"<VERBATIM>\r\n",
			NULL };

	char	*end_ide_list[] = {
			"</VERBATIM>\r\n",
			"</FRAME>\r\n",
			"</FILE>\r\n",
			NULL };

		char	*header_xml_list[] = {
			"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n",
			"<!--Arbortext, Inc., 1988-2013, v.4002-->\r\n",
			"<body>\r\n",
			"<code>\r\n",
			NULL };

	char	*end_xml_list[] = {
			"</code>\r\n",
			"</body>\r\n",
			NULL };

	char	*dummy_info[] = {
			"This file has been generated by the ",
			"M910nam.  The reason this file was created\r\n",
			"by the M910nam is either due to the fact that ",
			"a static burst was run or an error has occurred.\r\n",
			"\r\n",
			"This file needs to exist to prevent any errors from ",
			"stopping the execution of the ATLAS program.\r\n",
			"This is a non-fatal error.\r\n",
			NULL };

	/*
	 * Create the file names, one that will be copied, and the ide file name.
	 */
	sprintf(Extension, "%s", dtb_info.iads_version == BASE3413 ? "xml" : "ide");
	sprintf(file_to_copy, "%s%s", dtb_info.log_location, dtb_info.log_file);

	if ((tmppt = exten_routine(file_to_copy, "dia", Extension)) != NULL) 
	{
		sprintf(ide_xml_file, "%s", tmppt);
	}

	else 
	{
		sprintf(ide_xml_file, "%s%s", dtb_info.log_location,
				dtb_info.iads_version == BASE3413 ?
				"m910nam.xml" : "m910nam.ide");
	}

	/*
	 * Here we will try first to copy the one file, but if create_file fails
	 * then we will see if the file exists if not then create a dummy one and
	 * try it again, else error out.
	 */

	if (create_file(file_to_copy, ide_xml_file)) {

		if ((tmpfp = fopen(file_to_copy, "rb")) == NULL) {

			if ((tmpfp = fopen(file_to_copy, "wb")) != NULL) {

				fclose(tmpfp);
		
				if (insert_text(file_to_copy, BEGINING, 0, dummy_info)) {
					dodebug(0, "create_ide()", "insert_text return an error");
					return(M9_ERROR);
				}
				else {

					if (create_file(file_to_copy, ide_xml_file)) {
						dodebug(0, "create_ide()", "create_file return an error");
						return(M9_ERROR);
					}
				}
			}
			else {
				dodebug(errno, "create_ide()", NULL);
				return(M9_ERROR);
			}
		}
		else {
			dodebug(0, "create_ide()", "File exists but something went wrong");
			return(M9_ERROR);
		}
	}

	if (change_char(ide_xml_file, "<>", "[]")) 
	{
		dodebug(0, "create_ide()", "change_char return an error");
		return(M9_ERROR);
	}

	if (insert_text(ide_xml_file, BEGINING, 0, dtb_info.iads_version == BASE3413 ? header_xml_list : header_ide_list)) 
	{
		dodebug(0, "create_ide()", "insert_text return an error");
		return(M9_ERROR);
	}

	if (insert_text(ide_xml_file, END, 0, dtb_info.iads_version == BASE3413 ? end_xml_list : end_ide_list)) 
	{
		dodebug(0, "create_ide()", "insert_text return an error");
		return(M9_ERROR);
	}

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

	/* Open the file with the b option otherwise stat and fread doesn't work
	like it is suppose to. */
	dodebug(0, "check_header()", file_name);

	if ((tmpfp = fopen(file_name, "rb")) == NULL) {
		dodebug(0, "check_header()", "fopen failed to open file_name");
		return(M9_ERROR);
	}

	/*
	 * Find the size of the file and allocate memory for it, then store it to be
	 * munipulated a little later. If fread doesn't read the correct amount then
	 * errno will be set to indicate the error and an ERROR will be return'd
	 */
	if (_stat(file_name, &fz) != M9_ERROR) {
		if ((filecontents = malloc((size_t)fz.st_size)) != NULL) {
			if ((fread(filecontents, sizeof(char), (size_t)fz.st_size,
					   tmpfp)) != (size_t)fz.st_size) {
				free(filecontents);
				dodebug(FILE_READ_ERROR, "check_header()", NULL);
				had_error++;
			}
		}
		else {
			dodebug(0, "check_header()", "malloc failed to allocated the required memory");
			had_error++;
		}
	}
	else {
		dodebug(errno, "check_header()", NULL);
		had_error++;
	}

	if (had_error) {
		return (M9_ERROR);
	}

	/*Check the file to see if it has the required info in it and is in the 
	proper place. If start_point = (-1) then move the start_point to the 
	first none-white character and go from there*/
	if (start_point == WHITE_SPACES) {

		int i;

		for (i = 0; i < fz.st_size; i++){
			if (!isspace(filecontents[i])) {
				start_point = i;
				break;
			}
		}
		if (i == fz.st_size) {
			start_point = i;
		}
	}

	if (_strnicmp(&filecontents[start_point], magic_header, length)) {
		dodebug(FILE_FORMAT_ERROR, "check_header()", NULL);
		return (M9_ERROR);
	}
	
	free(filecontents);

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
				"diag_output_dir",
				"diag_output_file",
				"dtb_file",
				"circuit_file",
				"reset",
				"diag_type",
				"keep_pin_state",
				"concurrent",
				"start_of_test_callback",
				"end_of_test_callback",
				"probe_point_ready_callback",
				"probe_sequence_started_callback",
				"probe_sequence_ended_callback",
				"probe_button_pressed_callback",
				"diag_test_callback",
				"start_of_pattern_callback",
				"end_of_pattern_callback",
				"probe_stability",
				"probe_reset_enable",
				"probe_mismatch_value",
				"max_seeding_value",
				"probe_wait",
				"prog",
				NULL };

	for (index = 0; cmd_string[index] != NULL; index++) {
		if (!_stricmp(command, cmd_string[index])) {
			break;
		}
	}

	if (cmd_string[index] == NULL) {
		index = M9_ERROR;
	}

	return(index);
}

/*
 * parse_argument:	This function will parse the argument list and check to see if
 *						the different programs exist.  If they exist then the proper
 *						structures will be filled in with the correct data.  The cb_func
 *						structure will indicate that the callback function has been used.
 *						The cb_info structure will have the different program(s) that
 *						need to be called for the different pattern(s).
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

int parse_argument(int command_name, char *argument)
{

	int		i, j, first_time, index_value = 0, first_colon;
	char	tmp_buf[M9_MAX_PATH];
	char	num_buf[6];
	char	tmp_argument_holder[M9_MAX_PATH];
	char	dirpath[M9_MAX_PATH];
	struct _stat	fz;

/*
 * First we will get the path of the current working directory,  then we will use this
 * path to setup the absolute path to the executable program.
 */

	if ((_getcwd(dirpath, M9_MAX_PATH)) == NULL) {
		dodebug(errno, "parse_argument()", NULL);
		dtb_info.dtb_errno = BURST_NOT_RUN;
		return(M9_ERROR);
	}

	if (dirpath[(strlen(dirpath) - 1)] == '\\') {
		dirpath[(strlen(dirpath) - 1)] = '\0';
	}

/*
 * Here we are going to check to see if the callback program(s) exist, but first we will check to see
 * if there is any : in the argument string. If there is then there is more then 1 progams that will
 * be used for this callback.  We will go to the character after the : and put it into the tmp buf
 * and prefix the root directory if needed to see if the program exists.  If there is not a : then
 * check to see if the program exists with the root directory prefixed to the argument.
 */

	if ((strchr(argument, ',')) != NULL) {

		for (i = 0, j = 0, first_time = 0; argument[i] != '\0'; i++) {
			if (argument[i] != ':') {
				if (!isdigit(argument[i]) && !isspace(argument[i])) {
					dodebug(IMPROPER_ARG, "parse_argument()", NULL);
					return(M9_ERROR);
				}
				if (isdigit(argument[i])) {
					num_buf[j] = argument[i];
					j++;
				}
				if (argument[i + 1] == ':') {
					num_buf[j] = '\0';
					j = 0;
					if (first_time) {
						if (index_value < atoi(num_buf)) {
							index_value = atoi(num_buf);
						}
					}
					else {
						index_value = atoi(num_buf);
						first_time++;
					}
					if ((strchr(argument + i, ',')) != NULL) {
						i = (int)(strchr(argument + i, ',') - argument);
					}
					else {
						break;
					}
				}
			}
		}

/*
* Here we will allocate the memory required for the different programs that will be ran for different
* patterns.  Also we will store the last pattern index in the cb_func structure in the options field.
*/

		if ((cb_info = malloc(sizeof(CB_INFO) * (index_value + 1))) == NULL) {
			dodebug(0, "parse_argument()", "malloc failed to allocated the required memory");
			return(M9_ERROR);
		}

		for (i = 0; i < index_value; i++) {
			cb_info[i].used = FALSE;
		}

		sprintf(num_buf, "%s", index_value);

		if((cb_func[(command_name - INDEX_OFFSET)].options = malloc((size_t)(strlen(num_buf) + 1))) != NULL) {
			sprintf(cb_func[(command_name - INDEX_OFFSET)].options, "%s", num_buf);
			cb_func[(command_name - INDEX_OFFSET)].used = TRUE;
		}
		else {
			dodebug(0, "parse_argument()", "malloc failed to allocated the required memory");
			return(M9_ERROR);
		}

		for (i = 0, j = 0, first_colon = 1; argument[i] != '\0'; i++) {
			if (argument[i] != ':' && first_colon) {
				if (!isdigit(argument[i]) && !isspace(argument[i])) {
					dodebug(IMPROPER_ARG, "parse_argument()", NULL);
					return(M9_ERROR);
				}
				num_buf[j] = argument[i];
				j++;
				if (argument[i + 1] == ':') {
					num_buf[j] = '\0';
					j = 0;
					i++;
					index_value = atoi(num_buf);
					first_colon = 0;
				}
			}
			else if (argument[i] != ',' && argument[i] != ';') {
				tmp_buf[j] = argument[i];
				j++;
			}
			if (argument[i] == ',' || argument[i] == ';' || argument[i + 1] == '\0') {

				char	*stringpt;

				tmp_buf[j] = '\0';
				if ((tmp_buf[0] == '\\') || (tmp_buf[0] == '/') ||
					(tmp_buf[1] == ':'   &&  tmp_buf[2] == '\\')) {
					sprintf(tmp_argument_holder, "%s", tmp_buf);
				}
				else {
					sprintf(tmp_argument_holder, "%s\\%s", dirpath, tmp_buf);
				}

/*
 * Check to see if there was an extension passed, if no extension
 * then add the correct one.
 */

				if ((stringpt = exten_routine(tmp_argument_holder, "exe", NULL)) != NULL) {
					sprintf(tmp_argument_holder, "%s", stringpt);
				}
				else {
					return(M9_ERROR);
				}

				if (_stat(tmp_argument_holder, &fz) != M9_ERROR) {
					if (!(fz.st_mode & _S_IEXEC)) {
						dodebug((command_name + 802), "parse_argument()", NULL);
						return(M9_ERROR);
					}
					else if ((cb_info[index_value].program = strdup(tmp_buf)) == NULL) {
						dodebug(0, "parse_argument()", "malloc failed to allocated the required memory");
					}
					else {
						cb_info[index_value].used = 1;
					}
				}
				else {
					dodebug((command_name + 802), "parse_argument()", NULL);
					return(M9_ERROR);
				}
				if (argument[i] == ';') {
					break;
				}

				j = 0;
				first_colon = 1;
			}
		}
	}

/*
* Here there is only one program for all patterns so we will setup the structures to work this way.
*/

	else {

		int		alloc_num;
		char	*stringpt;

		i = 0;

		sprintf(num_buf, "%s", "-1");

		if (!isdigit(argument[0])) {

			for (i = 0, j = 0; argument[i] != '\0'; i++) {

				if (argument[i] != ':') {
					num_buf[j] = argument[i];
					j++;

					if (argument[i + 1] == ':') {
						num_buf[j] = '\0';
						i++;
						break;
					}
				}
			}
		}

		alloc_num = atoi(num_buf) == (-1) ? 0 : atoi(num_buf);
			
		if ((cb_info = malloc(sizeof(CB_INFO) * alloc_num + 1)) == NULL) {
			dodebug(0, "parse_argument()", "malloc failed to allocated the required memory");
			return(M9_ERROR);
		}

		if (alloc_num != 0) {

			for (i = 0; i < alloc_num; i++) {
				cb_info[i].used = FALSE;
			}
		}
		else {
			cb_info[i].used = FALSE;
		}

		if((cb_func[(command_name - INDEX_OFFSET)].options = malloc((size_t)(strlen(num_buf) + 1))) != NULL) {
			sprintf(cb_func[(command_name - INDEX_OFFSET)].options, "%s", num_buf);
			cb_func[(command_name - INDEX_OFFSET)].used = TRUE;
		}
		else {
			dodebug(0, "parse_argument()", "malloc failed to allocated the required memory");
			return(M9_ERROR);
		}

		sprintf(tmp_buf, "%s", &argument[i]);

		if ((argument[i] == '\\') || (argument[i] == '/') ||
			(argument[i + 1] == ':'   &&  argument[i + 2] == '\\')) {
			sprintf(tmp_buf, "%s", argument);
		}
		else {
			sprintf(tmp_buf, "%s\\%s", dirpath, argument);
		}


/*
 * Check to see if there was an extension passed, if no extension
 * then add the correct one.
 */

		if ((stringpt = exten_routine(tmp_buf, "exe", NULL)) != NULL) {
			sprintf(tmp_buf, "%s", stringpt);
		}
		else {
			return(M9_ERROR);
		}

		if (_stat(tmp_buf, &fz) != M9_ERROR) {
			if (!(fz.st_mode & _S_IEXEC)) {
				dodebug((command_name + 802), "parse_argument()", NULL);
				return(M9_ERROR);
			}
			else if ((cb_info[alloc_num].program = strdup(tmp_buf)) == NULL) {
				dodebug(0, "parse_argument()", "malloc failed to allocated the required memory");
			}
			else {
				cb_info[alloc_num].used = TRUE;
			}
		}
		else {
			dodebug((command_name + 802), "parse_argument()", NULL);
			return(M9_ERROR);
		}
	}

	return(SUCCESS);
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

	int				strpt = 1;
	char			*testpt;
	struct _stat	fz;

	testpt = strchr(argument, '=');
	if (testpt != NULL) {
		dodebug(FILE_FORMAT_ERROR, "fill_in_struct_data()", NULL);
		return(M9_ERROR);
	}

	{
		char	tmpString[80];
		sprintf(tmpString, "The command # is %d, and the arguemnt is (%s)", command_name, argument);
		dodebug(0, "fill_in_struct_data", tmpString);
	}

	/*
	 * First we will do the ones that are allowed to have no arguments.
	 * These will cause their defaults to be set.
	 */
	if (argument == NULL) {

		switch(command_name) {

			case RESET_DTI:

				dtb_info.reset_flag = RESET_BEFORE;

				break;

			case DIAG_TYPE:

				dtb_info.diag_type = NO_DIAG;

			break;

			case PIN_STATE:

				dtb_info.pin_state = RESET_AFTER;

			break;

			case CONCURRENT:

				dtb_info.concurrent = FALSE;

			break;

			case PROBE_RESET_ENABLE:

				dtb_info.probe_reset = DIAG_ENABLE;

			break;

			case PROBE_WAIT:

				dtb_info.probe_nowait = DIAG_ENABLE;

			break;

			default:

			break;

		}
	}

	/*
	 * Now do the ones only if the argument is not NULL.
	 */
	if (argument != NULL) {

		char	*stringpt;
		char	dirpath[M9_MAX_PATH];
		char	tmp_argument_holder[M9_MAX_PATH];

		if ((_getcwd(dirpath, M9_MAX_PATH)) == NULL) {
			dodebug(errno, "fill_in_struct_data()", NULL);
			dtb_info.dtb_errno = BURST_NOT_RUN;
			return(M9_ERROR);
		}

		if (dirpath[(strlen(dirpath) - 1)] == '\\') {
			dirpath[(strlen(dirpath) - 1)] = '\0';
		}


		switch(command_name) {

			case DIAG_OUTPUT_DIRECTORY:

#pragma warning(disable : 4127)

				while (TRUE) {

					if (argument[((size_t)strlen(argument) - strpt)] == '\\') {
						argument[(size_t)strlen(argument) - strpt] = '\0';
					}
					else {
						break;
					}

					strpt++;
				}

#pragma warning(default : 4127)

				if (_stat(argument, &fz) != M9_ERROR) {
					if (!(fz.st_mode & _S_IFDIR)) {
						dodebug(DIAG_DIR_IS_FILE, "fill_in_struct_data()", NULL);
						return(M9_ERROR);
					}
				}
				else {
					dodebug(DIAG_DIR_ERROR, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

				sprintf(dtb_info.log_location, "%s", argument);

				if (argument[((size_t)strlen(argument) - 1)] != '\\') {
					strcat(dtb_info.log_location, "\\");
				}

			break;

			case DIAG_OUTPUT_FILE_NAME:


				if ((stringpt = exten_routine(argument, "dia", NULL)) != NULL) {
					sprintf(dtb_info.log_file, "%s", stringpt);
				}
				else {
					sprintf(dtb_info.log_file, "%s", "null");
				}

			break;

			case DTB_FILE_NAME:

				if ((argument[0] == '\\') || (argument[0] == '/') || (argument[1] == ':'   &&  argument[2] == '\\')|| (argument[1] == ':'   &&  argument[2] == '/')) 
				{
					sprintf(dtb_info.dtb_file, argument);
				}

				else 
				{
					sprintf(dtb_info.dtb_file, "%s\\%s", dirpath, argument);
				}


			break;

			case CIRCUIT_FILE_NAME:

				if ((argument[0] == '\\') || (argument[0] == '/') || (argument[1] == ':'   &&  argument[2] == '\\')|| (argument[1] == ':'   &&  argument[2] == '/')) 
				{				
					sprintf(dtb_info.cir_file, argument);
				}

				else 
				{
					sprintf(dtb_info.cir_file, "%s\\%s", dirpath, argument);
				}

			break;

			case RESET_DTI:

				if (!_strnicmp(argument, "y", 1)) {
					dtb_info.reset_flag = RESET_BEFORE;
				}
				else if (!_strnicmp(argument, "n", 1)) {
					dtb_info.reset_flag = NO_RESET_BEFORE;
				}
				else {
					dodebug(RESET_VALUE, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

			break;

			case DIAG_TYPE:

				if (!_strnicmp(argument, "n", 1)) {
					dtb_info.diag_type = NO_DIAG;
				}
				else if (!_strnicmp(argument, "s", 1)) {
					dtb_info.diag_type = SEEDED_PROBE;
				}
				else if (!_strnicmp(argument, "f", 1)) {
					dtb_info.diag_type = FAULT_DICT;
				}
				else if (!_strnicmp(argument, "p", 1)) {
					dtb_info.diag_type = PROBE_ONLY;
				}
				else {
					dodebug(DIAG_TYPE, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

			break;

			case PIN_STATE:

				if (!_strnicmp(argument, "y", 1)) {
					dtb_info.pin_state = NO_RESET_AFTER;
				}
				else if (!_strnicmp(argument, "n", 1)) {
					dtb_info.pin_state = RESET_AFTER;
				}
				else {
					dodebug(PIN_STATE, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

			break;

			case CONCURRENT:

				if (!_strnicmp(argument, "y", 1)) {
					dtb_info.concurrent = START_IT;
				}
				else if (!_strnicmp(argument, "n", 1)) {
					dtb_info.concurrent = FALSE;
				}
				else if (!_strnicmp(argument, "h", 1)) {
					dtb_info.concurrent = HALT_IT;
				}
				else {
					dodebug(CONCURRENT, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

			break;

			case START_OF_TEST_CALLBACK:

			case END_OF_TEST_CALLBACK:

			case PROBE_POINT_READY_CALLBACK:

			case PROBE_SEQUENCE_STARTED_CALLBACK:

			case PROBE_SEQUENCE_ENDED_CALLBACK:

			case PROBE_BUTTON_PRESSED_CALLBACK:

			case DIAG_TEST_CALLBACK:

			/*Here we will check to see if the callback program exists, and if it don't well we will
			 * error out, kind of make sense huh. On the dodebug function it takes the int value of the
			 * comman_name adds 802 to it and you will find the correct define in m910nam.h starting at
			 * 810 and going up from there.  This is the same with the sprint function except we minus 8.
			 * and the first one starts at 0 and goes up from there. this is used to fill out the cb_func
			 * structure in the diag_routine.c file. That's it.*/
				if ((argument[0] == '\\') || (argument[0] == '/') || (argument[1] == ':'   &&  argument[2] == '\\')|| (argument[1] == ':'   &&  argument[2] == '/')) 
				{
					sprintf(tmp_argument_holder, "%s", argument);
				}

				else 
				{
					sprintf(tmp_argument_holder, "%s\\%s", dirpath, argument);
				}

				/*Check to see if there was an extension passed, if no extension
				 * then add the correct one.*/
				if ((stringpt = exten_routine(tmp_argument_holder, "exe", NULL)) != NULL) 
				{
					sprintf(tmp_argument_holder, "%s", stringpt);
				}

				else 
				{
					return(M9_ERROR);
				}

				if (_stat(tmp_argument_holder, &fz) != M9_ERROR) 
				{
					if (!(fz.st_mode & _S_IEXEC)) 
					{
						dodebug((command_name + 802), "fill_in_struct_data()", NULL);
						return(M9_ERROR);
					}
				}
				else {
					dodebug((command_name + 802), "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

				if(((cb_func[(command_name - INDEX_OFFSET)].options) = malloc((size_t)tmp_argument_holder + 1)) != NULL) 
				{
					cb_func[(command_name - INDEX_OFFSET)].used = TRUE;
					sprintf(cb_func[(command_name - INDEX_OFFSET)].options, "%s", tmp_argument_holder);
				}
				else 
				{
					dodebug(0, "fill_in_struct_data()", "malloc failed to allocated the required memory");
					return(M9_ERROR);
				}

			break;

			case START_OF_PATTERN_CALLBACK:

			case END_OF_PATTERN_CALLBACK:

				if ((parse_argument(command_name, argument)) == M9_ERROR) {
					dodebug(0, "fill_in_struct_data()", "parse_argument() failed");
					return(M9_ERROR);
				}

			break;

			case PROBE_STABILITY_COUNT:

				if (atoi(argument) < 1 || atoi(argument) > 9) {
					dodebug(PROBE_STAB_VAL, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

				dtb_info.stability_count = atol(argument);

			break;

			case PROBE_RESET_ENABLE:

				if (!_strnicmp(argument, "y", 1)) {
					dtb_info.probe_reset = DIAG_ENABLE;
				}
				else if (!_strnicmp(argument, "n", 1)) {
					dtb_info.probe_reset = DIAG_DISABLE;
				}
				else {
					dodebug(PROBE_RESET_VAL, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

			break;

			case PROBE_MISMATCH_VALUE:

				if (atoi(argument) < 0 || atoi(argument) > 9) {
					dodebug(PROBE_MIS_VAL, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

				dtb_info.mismatch_value = atoi(argument);

			break;

			case MAX_SEEDING_VALUE:

				if (atoi(argument) < 1 || atoi(argument) > 9) {
					dodebug(MAX_SEED_VALUE, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

				dtb_info.seed_value = atoi(argument);

			break;

			case PROBE_WAIT:

				if (!_strnicmp(argument, "y", 1)) {
					dtb_info.probe_nowait = DIAG_DISABLE;
				}
				else if (!_strnicmp(argument, "n", 1)) {
					dtb_info.probe_nowait = DIAG_ENABLE;
				}
				else {
					dodebug(PROBE_NOWAIT, "fill_in_struct_data()", NULL);
					return(M9_ERROR);
				}

			break;

			case EXECUTABLE_PROGRAM:

				dtb_info.dtb_errno = PROGRAM_NOT_RUN;

				if ((argument[0] == '\\') || (argument[0] == '/') || (argument[1] == ':'   &&  argument[2] == '\\')|| (argument[1] == ':'   &&  argument[2] == '/')) 
				{
					sprintf(tmp_argument_holder, argument);
				}
				else 
				{
					sprintf(tmp_argument_holder, "%s\\%s", dirpath, argument);
				}

				dodebug(0, "fill_in_struct_daTA()", tmp_argument_holder);

				/*Check to see if there was an extension passed, if no extension
				 * then add the correct one.*/

				if ((stringpt = exten_routine(tmp_argument_holder, "exe", NULL)) != NULL) 
				{
					sprintf(tmp_argument_holder, "%s", stringpt);
				}

				else 
				{
					return(M9_ERROR);
				}

				if (_stat(tmp_argument_holder, &fz) != M9_ERROR)
				{
					if (!(fz.st_mode & _S_IEXEC))
					{

						char	tmpbuf[80];

						sprintf(tmpbuf, "Program %s is not an executable program", tmp_argument_holder);

						dodebug(0, "fill_in_struct_data()", tmpbuf);
						return(M9_ERROR);
					}
				}

				else 
				{
					dodebug(0, "fill_in_struct_data()", "Can't stat the executable program");
					return(M9_ERROR);
				}

				sprintf(dtb_info.execute_prog, "%s", tmp_argument_holder);
				FaultCalloutFp = NULL;
				FaultMessageFp = NULL;

			break;

			default:

			break;

		}
	}

	return(SUCCESS);
}

/*
 * parse_file:	This function will parse the file that was passed as an
 *		        argument to the m910nam.  The function will check for
 *		        proper file format. Also the variables and/or structures
 *		        will be filled, they will however will not be checked
 *		        for proper info. That is done later, I guess.
 *
 * Parameters:
 *	file_name:		This is the file that will be checked.
 *	start_point:	This is the starting point of the check.
 *	white_space:	This will require that the start_point to begin after any
 *			leading white spaces are accounted for. 
 *			A 1 will cause the deletion of leading white spaces,
 *			and a 0 will do nothing.
 *
 * Returns:
 *	SUCCESS:		0    - successful execution of this function.
 *	M9_ERROR:		(-1) - files weren't there.
 *
 */
#pragma warning(disable : 4701)
int parse_file(char *file_name, int start_point, int white_spaces)
{

	int				i;
	int				read_size;
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
	 * like it is suppose to */
	if ((tmpfp = fopen(file_name, "rb")) == NULL) 
	{
		dodebug(0, "parse_file()", "fopen failed to open file_name");
		return(M9_ERROR);
	}

	/*Find the size of the file and allocate memory for it, then store it to be
	 * munipulated a little later. If fread doesn't read the correct amount then
	 * errno will be set to indicate the error and an ERROR will be return'd*/
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

	if (had_error) {
		return (M9_ERROR);
	}

	/* Now we will check the file to see if it has the required info in it and 
	is in the proper place.  If start_point = (-1) then we will move the 
	start_point to the first none-white character and go from there. */
	if (white_spaces) {

		for (i = 0; i < fz.st_size; i++){
			if (!isspace(filecontents[i])) {
				start_point += i;
				break;
			}
		}
		if (i == fz.st_size) {
			start_point = i;
		}
	}

	/*Now we will check the file to see what info is in it and if it is properly
	 * formatted.  There is no checks to see if the characters are of proper case.
	 * Any comparision is done ignoring the case.  The checking is started after the
	 * magic header info block.*/
	for (i = start_point; i < read_size; i++) 
	{
		int	index, is_argstring;

		/*The first thing in the file after the magic header info should be an alpha
		 * character.  If it is an alpha character then find the length by locating the =
		 * and subtracting the 2 pointers. If no = then error out. With the difference 
		 * alloc the memory for the character string, also checking for proper structure.
		 * If all is good then the command is loaded into cmd_string. i is incremented to
		 * the proper value.*/
		if (isalpha(filecontents[i])) {
			str1pt = filecontents + i;
			str2pt = strchr(str1pt, '=');
			if (str2pt != NULL) {
				if ((cmd_string = malloc((size_t)(str2pt - str1pt) + 1)) != NULL) {
					cmd_is_allocd = 1;
					strncpy(cmd_string, str1pt, str2pt - str1pt);
					cmd_string[str2pt - str1pt] = '\0';
					i += (str2pt - str1pt) + 1;

					/*Here we call the compare function to see if the command is a legal command, if
					 * so then index is set to the command value, else error out.*/
					if ((index = compare_command(cmd_string)) == M9_ERROR) {
						dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
						had_error++;
						break;
					}
				}
				else {
					dodebug(0, "parse_file()", "malloc failed to allocated the required memory");
					had_error++;
					break;
				}
			}
			else {
				dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
				had_error++;
				break;
			}

			/*After the command string and the = sign comes the argument string, which can contain
			 * ;/\a-z0-9 as its first character and goes to a ;. If these 2 parameters are meet, the 
			 * length of the argument string is found by subtracting the 2 pointers.  With the
			 * difference alloc the memory for the arg string.  If all is good then the argument is
			 * loaded into the arg_string. i is incremented to the proper value.*/
			if (isalnum(filecontents[i]) || filecontents[i] == '\\' || filecontents[i] == '/') {
				str1pt = filecontents + i;
				str2pt = strchr(str1pt, ';');
				if (str2pt != NULL) {
					if ((arg_string = malloc((size_t)(str2pt - str1pt) + 1)) != NULL) {
						arg_is_allocd = 1;
						strncpy(arg_string, str1pt, str2pt - str1pt);
						arg_string[str2pt - str1pt] = '\0';
						i += (str2pt - str1pt) + 1;
						is_argstring = TRUE;
					}
					else {
						dodebug(0, "parse_file()", "malloc failed to allocated the required memory");
						had_error++;
						break;
					}
				}
				else {
					dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
					had_error++;
					break;
				}
			}

			/*Here the ; was used right after the = sign, so the default will be used if any.
			 * This is allowed due to the fact that the programmer may be using a template file
			 * and only filling in what he/she needs, so I will allow this to be done.*/
			else if (filecontents[i] == ';') {
				is_argstring = FALSE;
			}
			else {
				dodebug(FILE_FORMAT_ERROR, "parse_file()", NULL);
				had_error++;
				break;
			}

			/*Now pass the command index number and the argument string if any to the function for
			 * proper use and error checking.*/
			{
				char	tmpString[80];
				sprintf(tmpString, "the index is %d, and the argument is (%s)", index, is_argstring == FALSE ? "NULL" : arg_string);
				dodebug(0, "parse_file()", tmpString);
			}

			if(fill_in_struct_data(index, is_argstring == FALSE ? NULL : arg_string) == M9_ERROR) {
				had_error++;
				break;
			}

			/** Now free if used my 2 buffers for the next if any commands and or argument strings.*/
			if (cmd_is_allocd) {
				free(cmd_string);
				cmd_is_allocd = 0;
			}
			if (arg_is_allocd) {
				free(arg_string);
				arg_is_allocd = 0;
			}
		}
	}

	free(filecontents);

	if (had_error) {
		if (cmd_is_allocd) {
			free(cmd_string);
		}
		if (arg_is_allocd) {
			free(arg_string);
		}
	}

	return(had_error == FALSE ? SUCCESS : M9_ERROR);

}

#pragma warning(default : 4701)

/*
 * change_dir:		This function will change to either the directory where the
 *						.dtb/.cxs file resides or to the directory where the ATLAS
 *						program resides.
 *
 * Parameters:
 *		location:	This flag will determine which directory the function will set
 *						as the working directory.
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */

int change_dir(int location)
{

	if (location == NEW) {
		if (_chdir(dtb_info.digital_dir)) {
			dodebug(0, "change_dir()", "Failed to change to digital directory");
			return(M9_ERROR);
		}
	}
	else {
		if (_chdir(dtb_info.atlas_dir)) {
			dodebug(0, "change_dir()", "Failed to change to ATLAS directory");
			return(M9_ERROR);
		}
	}

	return(SUCCESS);		
}

/*
 * convert_files_set_dir:	This function will change the .dtb and .cxs files to
 *							just the file name with no path info.  It will also
 *							set the dtb_info.digital_dir from the path info of the
 *							dtb full path name.
 *
 * Parameters:
 *		NONE			This function will use the dtb_info structure for
 *						its information.
 *
 * Returns:
 *		SUCCESS:		0    - successful execution of this function.
 *		M9_ERROR:		(-1) - files weren't there.
 *
 */

int convert_files_set_dir(void)
{

	int		i;
	char	tmp_name[M9_MAX_PATH];

	//Get the directory of where the dtb resides, and set the current working directory to that.
	_getcwd(dtb_info.atlas_dir, M9_MAX_PATH);

	for (i = strlen(dtb_info.dtb_file); i > 0; i--) {

		if (dtb_info.dtb_file[i - 1] == '\\' || dtb_info.dtb_file[i - 1] == '/') {

			_snprintf(dtb_info.digital_dir, (i - 1), "%s", dtb_info.dtb_file);
			dtb_info.digital_dir[i - 1] = '\0';
			sprintf(tmp_name, "%s", &dtb_info.dtb_file[i]);
			dtb_info.dtb_file[0] = '\0';
			sprintf(dtb_info.dtb_file, "%s", tmp_name);
			break;
		}
	}

	for (i = strlen(dtb_info.cir_file); i > 0; i--) {

		if (dtb_info.cir_file[i - 1] == '\\' || dtb_info.cir_file[i - 1] == '/') {

			tmp_name[0] = '\0';
			sprintf(tmp_name, "%s", &dtb_info.cir_file[i]);
			dtb_info.cir_file[0] = '\0';
			sprintf(dtb_info.cir_file, "%s", tmp_name);
			break;
		}
	}

	return(SUCCESS);
}
