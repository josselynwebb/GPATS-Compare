/****************************************************************************
 *	File:	callbacks.h														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *																			*
 ***************************************************************************/

extern void _stdcall start_of_test(char *);
extern void _stdcall end_of_test(char *);
extern void _stdcall probe_point_ready(char *);
extern void _stdcall probe_sequence_started(char *);
extern void _stdcall probe_sequence_ended(char *);
extern void _stdcall probe_button_pressed(char *);
extern void _stdcall diagnose_test(DiagStatus);
extern void _stdcall start_of_pattern(char *);
extern void _stdcall end_of_pattern(char *);
