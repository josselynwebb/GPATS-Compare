/****************************************************************************
 *	File:	allnams.h														*
 *																			*
 *	Creation Date:	19 Oct 2001												*
 *																			*
 *	Created By:		Richard Chaffin											*
 *																			*
 *	Revision Log:															*
 *		2.0		Assigned it a version number.								*
 *																			*
 ***************************************************************************/

/****************************************************************************
*		External Variables and Routines										*
****************************************************************************/

extern void v_init(void);
extern void v_opn(int, char*, int);
extern void mov_dfv(struct DAT_ITM*, long);
extern void mov_dtv(long, struct DAT_ITM*);
extern void v_cls(int);
