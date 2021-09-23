/////////////////////////////////////////////////////////////////////////////
//	File:	insert_file.cpp													/
//																			/
//	Creation Date:	19 Oct 2001												/
//																			/
//	Created By:		Richard Chaffin											/
//																			/
//	Revision Log:															/
//		0.31	Assigned it a version number.								/
//		0.32	In the function insert_into_fault_file(), modified the what /
//				I am looking for.  Now after I find Guided I now look for a /
//				'D', 'P', 'O', 'T'.  These are what I have found that       /
//				Teradyne uses for different fault statements. After I find  /
//				the above I then look for DIAGNOSED FA for the D,			/
//				Possible Fee for the P, Test is Unst for the T and			/
//				OPEN detecte for the O.	Also had to redo how I check for	/
//				end of the line on the previous character, this is do to	/
//				Possible being at the start of the line where the others	/
//				start at the fourth charater on the line.  I use a variable /
//				not_first_time to solve this.								/
//		1.0.0.0	Insured that the character array dtb_file_contents had a	/
//				NULL character at the end of the string.					/
//		1.0.1.0	Removed the variable declaration of the FILE *dtbfp and		/
//				the declaration int dtbfd.									/
//				Moved the _stat function call up to just before the opening	/
//				of the files for simplicity.								/
//				Changed the fopen function call to a _open function call	/
//				opening the dtb_file_buf.									/
//				Changed the fread function call to a _read function call	/
//				this seems to be the problem with Microsoft API call when	/
//				the file size was 1312 bytes long, the OS would buffer it	/
//				and would not release the buffer so the file pointer could	/
//				be closed.  Fixed the call byu going to an unbuffered		/
//				function call.												/
//				Removed the 2 fclose function calls and added the function	/
//				call _close right after the _read dtbfd function call.  I	/
//				only needed the 1 function call instead of the 2 and the	/
//				function call had to be changed to _close any way.			/
//				Modified the for loop in t he Guided_detected section.		/
//				Thought that this was causing a buffer overrun the way it	/
//				done, it wasn't, but this way will prevent it from happening/
//				Instead of looking for the last character being a NULL, I	/
//				now look for i < dtbfz.st_size.								/
//																			/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//	Include Files															/
/////////////////////////////////////////////////////////////////////////////

#include <io.h>
#include <fcntl.h>
#include <malloc.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <ctype.h>
#include <errno.h>
#include "gpnam.h"

/////////////////////////////////////////////////////////////////////////////
//		External Variables and Routines										/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Local Constants														/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Globals																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
//		Modules																/
/////////////////////////////////////////////////////////////////////////////

/////////////////////////////////////////////////////////////////////////////
// insert_into_fault_file:	This program will extract the digital info from	/
//								the M910NAM.dia file and insert it into the	/
//								fault file at the proper place.				/
//																			/
// Parameters:																/
//		NONE			This function will use the gp_info structure for	/
//							its information.								/
//																			/
// Returns:																	/
//		SUCCESS:	  0		= successful completion of the function.		/
//		GP_ERROR:	(-1)	= failure of a required task.					/
/////////////////////////////////////////////////////////////////////////////

int insert_into_fault_file(void)
{

	int				i, fffd, Guided_detected, Guided_detect_point = 0, leading_white_spaces;
	int				fault_file_pos, file_pos, dtbfd;
	unsigned int	read_value;
	char			fault_file_buf[GP_MAX_PATH], dtb_file_buf[GP_MAX_PATH], char_to_read[8];
	char			*dtb_file_contents;
	struct _stat	dtbfz;

//
// Make sure that all files are written and the info is there before
// this programs opens the files for reading and writing.
//

	fflush(NULL);

//
// Set up the buffers with the proper file names.
//

	sprintf(fault_file_buf, "%s%s", gp_info.log_location, gp_info.log_file);

	sprintf(dtb_file_buf, "%s%s", gp_info.log_location, DIA_FILE);

//
// Now open the fault file that is going to have stuff put in to it, and
// the dtb file from where the stuff is coming from.
//

	if ((_stat(dtb_file_buf, &dtbfz)) == GP_ERROR) {
		dodebug(errno, "insert_into_file()", NULL, NULL);
		gp_info.return_value = FILE_READ_ERROR;
		return(GP_ERROR);
	}

	if ((fffd = _open(fault_file_buf, _O_RDWR | _O_BINARY)) == GP_ERROR) {
		dodebug(errno, "insert_into_fault_file()", NULL, NULL);
		gp_info.return_value = FILE_OPEN_ERROR;
		return(GP_ERROR);
	}

	if ((dtbfd = _open(dtb_file_buf, _O_RDONLY | _O_BINARY)) == GP_ERROR) {
		dodebug(errno, "insert_into_fault_file()", NULL, NULL);
		gp_info.return_value = FILE_OPEN_ERROR;
		return(GP_ERROR);
	}

/* 
   Get the size of the dtb file and read it into the dtb_file_buf.  Determine if it is either 
   a fault dictionary file or a guided probe file.
*/


	if ((dtb_file_contents = (char *)malloc((size_t)dtbfz.st_size)) != NULL) {
		if ((read_value = _read(dtbfd, dtb_file_contents, (size_t)dtbfz.st_size))
															 != (size_t)dtbfz.st_size) {
			free(dtb_file_contents);
			dodebug(errno, "insert_into_file()", NULL, NULL);
			gp_info.return_value = FILE_READ_ERROR;
			return(GP_ERROR);
		}
		dtb_file_contents[(size_t)dtbfz.st_size] = '\0';
	}
	else {
		dodebug(errno, "insert_into_file()", NULL, NULL);
		gp_info.return_value = FILE_READ_ERROR;
		return(GP_ERROR);
	}
		
	_close(dtbfd);

//
// Now we will find what the end of the fault file location is for later reference.
// This will be done by going to the end of the file and reading its position.
//

	if ((fault_file_pos = _lseek(fffd, 0L, SEEK_END)) == GP_ERROR) {
		dodebug(errno, "insert_into_fault_file()", NULL, NULL);
		gp_info.return_value = FILE_POSITION_ERROR;
		return(GP_ERROR);
	}

//
// Now we will find if the FAULT-FILE has the required starting point for the diagnostis
// info to be inserted.  We are looking for PCOF: with or without a trailing space.  We
// will start by going to the end of the file and working backwards.  PCOF: should be the
// last thing written to the file.
//

	for (file_pos = 1; file_pos != 20; file_pos++) {

		if ((_lseek(fffd, fault_file_pos - file_pos, SEEK_SET)) != GP_ERROR) {
			if ((_read(fffd, char_to_read, 1)) != 1) {
				dodebug(FILE_READ_ERROR, "insert_into_fault_file()", NULL, NULL);
				gp_info.return_value = FILE_READ_ERROR;
				return(GP_ERROR);
			}

			if (char_to_read[0] == ':') {

				file_pos += 4;

				if ((_lseek(fffd, fault_file_pos - file_pos, SEEK_SET)) != GP_ERROR) {
					if ((_read(fffd, char_to_read, 5)) != 5) {
						dodebug(FILE_READ_ERROR, "insert_into_fault_file()", NULL, NULL);
						gp_info.return_value = FILE_READ_ERROR;
						return(GP_ERROR);
					}

					char_to_read[5] = '\0';

					if (!strncmp(char_to_read, "PCOF:", 5)) {

						file_pos -= 5;

						if ((_read(fffd, char_to_read, 1)) != 1) {
							dodebug(FILE_READ_ERROR, "insert_into_fault_file()", NULL, NULL);
							gp_info.return_value = FILE_READ_ERROR;
							return(GP_ERROR);
						}

						if (char_to_read[0] != ' ') {
							if ((_write(fffd, " ", 1)) != 1) {
								dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL);
								gp_info.return_value = FILE_WRITE_ERROR;
								return(GP_ERROR);
							}
						}

						break;

					}
					else {
						dodebug(FILE_READ_ERROR, "insert_into_fault_file()", NULL, NULL);
						gp_info.return_value = FILE_READ_ERROR;
						return(GP_ERROR);
					}
				}
				else {
					dodebug(errno, "insert_into_fault_file()", NULL, NULL);
					gp_info.return_value = FILE_POSITION_ERROR;
					return(GP_ERROR);
				}
			}
		}
		else {
			dodebug(errno, "insert_into_fault_file()", NULL, NULL);
			gp_info.return_value = FILE_POSITION_ERROR;
			return(GP_ERROR);
		}
	}

//
// Now we will find out how many characters sense the last CRLF to where we are
// when we found the space or put it there after the PCOF:.  This will set the
// variable leading_white_spaces.  Which will be used when we take info from the
// .dia file and stick it in the FAULT-FILE.  Then put the pointer back to where
// it was.
//

	for (i = (file_pos - 1), leading_white_spaces = 1; i < 80; i++, leading_white_spaces++) {

		if ((_lseek(fffd, fault_file_pos - i, SEEK_SET)) == GP_ERROR) {
			dodebug(errno, "insert_into_fault_file()", NULL, NULL);
			gp_info.return_value = FILE_POSITION_ERROR;
			return(GP_ERROR);
		}
		else if ((_read(fffd, char_to_read, 1)) != 1) {
			dodebug(FILE_READ_ERROR, "insert_into_fault_file()", NULL, NULL);
			gp_info.return_value = FILE_READ_ERROR;
			return(GP_ERROR);
		}

		if (char_to_read[0] == '\n') {
			leading_white_spaces--;
			break;
		}
	}

	if ((_lseek(fffd, fault_file_pos - (file_pos - 1), SEEK_SET)) == GP_ERROR) {
		dodebug(errno, "insert_into_fault_file()", NULL, NULL);
		gp_info.return_value = FILE_POSITION_ERROR;
		return(GP_ERROR);
	}

//
// Seeing that we have gotten this far we will now read the .dia file and see if there
// was any guided probing done.  If so then set the variable Guided_detected.
//

	Guided_detected = 0;

	for (i = 0; (size_t)i < (size_t)dtbfz.st_size; i++) {

		if (dtb_file_contents[i] == 'G') {

			if (!strncmp(&dtb_file_contents[i], "Guided", 6)) {
				Guided_detected++;
				Guided_detect_point = i;
				break;
			}
		}
	}

//
// Seeing that I like doing the easy stuff first, we will do the guided probe stuffing.
// We will start the stuffing when we find the string starting with the below, this is what
// we want to put into the FAULT-FILE.  Also we will make the FAULT-FILE pretty by
// removing any extra spaces and *.  We will also wrap at the 80th character.
//

	if (Guided_detected) {

		int j, k, not_first_time;

		not_first_time = 0;

		for (i = Guided_detect_point; (size_t)i < (size_t)dtbfz.st_size; i++) {

			if (dtb_file_contents[i] == 'D' || dtb_file_contents[i] == 'O' ||
				dtb_file_contents[i] == 'T' || dtb_file_contents[i] == 'P') {

				if (!strncmp(&dtb_file_contents[i], "DIAGNOSED FA", strlen("DIAGNOSED FA")) ||
					!strncmp(&dtb_file_contents[i], "OPEN detecte", strlen("OPEN detecte")) ||
					!strncmp(&dtb_file_contents[i], "Test is Unst", strlen("Test is Unst")) ||
					!strncmp(&dtb_file_contents[i], "Possible Fee", strlen("Possible Fee"))) {

					for (j = leading_white_spaces; dtb_file_contents[i] != '\0'; j++, i++) {

						if (j == 80 || dtb_file_contents[i - 1] == '\n' && not_first_time) {

							if (j == 80) {

								if ((_write(fffd, "\r\n", 2)) != 2) {
									dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
									gp_info.return_value = FILE_WRITE_ERROR;
									return(GP_ERROR);
								}
							}

							j = leading_white_spaces;

							for (k = 1; k < leading_white_spaces; k++) {

								if ((_write(fffd, " ", 1)) != 1) {
									dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
									gp_info.return_value = FILE_WRITE_ERROR;
									return(GP_ERROR);
								}
							}
						}

						if (dtb_file_contents[i] == ' ' && dtb_file_contents[i - 1] == '*') {
							i++;
						}

						if (dtb_file_contents[i] == ' ' && dtb_file_contents[i - 1] == '\n') {
							i++;
						}

						if (dtb_file_contents[i] == '*') {
							i++;
						}
						else {

							if ((_write(fffd, &dtb_file_contents[i], 1)) != 1) {
								dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
								gp_info.return_value = FILE_WRITE_ERROR;
								return(GP_ERROR);
							}
						}

						not_first_time++;
					}
				}
			}
		}

		_close(fffd);
		free(dtb_file_contents);

	}

//
// Now for the hard part, this must be fault dictionary info.  We start at the string
// that begins with "One of the following", or "Fault:".  Also we will remove the info
// starting with the string that starts with Det until we have either the end of the
// file or back with the string starting with "One of the following" or "Fault:".  We
// will also make it look pretty by removing extra junk at the begining of some lines,
// and at the ends of other lines.
//

	else {

		int		j, k;
		char	first_line[] = "Fault Dictionary Diagnosis:\r\n\r\n";

		for (i = 10; dtb_file_contents[i] != '\0'; i++) {

			if (dtb_file_contents[i] == 'O') {
				if (!strncmp(&dtb_file_contents[i], "One of the following fault",
					 strlen("One of the following fault"))) {
					break;
				}
			}

			else if (dtb_file_contents[i] == 'F') {
				if (!strncmp(&dtb_file_contents[i], "Fault:", strlen("Fault:"))) {
					break;
				}
			}
		}

//
// Now i will put in a standard default line.
//

		for (j = 0; (size_t)j < strlen(first_line); j++) {

			if ((_write(fffd, &first_line[j], 1)) != 1) {
				dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
				gp_info.return_value = FILE_WRITE_ERROR;
				return(GP_ERROR);
			}
		}

		for (k = 1; k < leading_white_spaces; k++) {

			if ((_write(fffd, " ", 1)) != 1) {
				dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
				gp_info.return_value = FILE_WRITE_ERROR;
				return(GP_ERROR);
			}
		}

		for (j = leading_white_spaces; dtb_file_contents[i] != '\0'; j++, i++) {

			if (dtb_file_contents[i] == 'D') {

				if (!strncmp(&dtb_file_contents[i], "Det", 3)) {

					for (; dtb_file_contents[i] != '\0'; i++) {

						if (dtb_file_contents[i] == 'F') {

							if (!strncmp(&dtb_file_contents[i], "Fau", 3)) {

								if ((_write(fffd, "\r\n", 2)) != 2) {
									dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
									gp_info.return_value = FILE_WRITE_ERROR;
									return(GP_ERROR);
								}

								for (k = 1; k < leading_white_spaces; k++) {

									if ((_write(fffd, " ", 1)) != 1) {
										dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
										gp_info.return_value = FILE_WRITE_ERROR;
										return(GP_ERROR);
									}
								}

								j = leading_white_spaces;
								break;

							}
						}
					}

					if (dtb_file_contents[i] == '\0') {

						if ((_write(fffd, "\r\n", 2)) != 2) {
							dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
							gp_info.return_value = FILE_WRITE_ERROR;
							return(GP_ERROR);
						}

						break;

					}
				}
			}

			if (dtb_file_contents[i - 1] == '\n' && dtb_file_contents[i] != '\0') {

				j = leading_white_spaces;

				for (k = 1; k < leading_white_spaces; k++) {

					if ((_write(fffd, " ", 1)) != 1) {
						dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
						gp_info.return_value = FILE_WRITE_ERROR;
						return(GP_ERROR);
					}
				}
			}

			if (dtb_file_contents[i] == ' ' && dtb_file_contents[i - 1] == '*') {
				i++;
			}

			if ((dtb_file_contents[i] == ' ' && dtb_file_contents[i - 1] == '\n') ||
				(dtb_file_contents[i] == ' ' && dtb_file_contents[i - 2] == '\n')) {
				i++;
			}

			else {

				if (dtb_file_contents[i] != '\0') {

					if ((_write(fffd, &dtb_file_contents[i], 1)) != 1) {
						dodebug(FILE_WRITE_ERROR, "insert_into_fault_file()", NULL, NULL);
						gp_info.return_value = FILE_WRITE_ERROR;
						return(GP_ERROR);
					}
				}
			}
		}

		_close(fffd);
		free(dtb_file_contents);

	}

	return(SUCCESS);

}
