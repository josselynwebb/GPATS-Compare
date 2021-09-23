***************************************************************************
*  Racal Instruments 3151 & 3152 Arbitrary Waveform Generator VXIplug&play Driver*
*  Copyright (c) 1994-1995, Racal Instruments, Inc.  All rights reserved. *
***************************************************************************


Introduction

The Racal Instruments VXIplug&play instrument driver provides comprehensive
access to the capabilities of the 3151 & 3152 Arbitrary Waveform Generator.
The following information is provided to give the user a better
idea of how and when to use these drivers and the supporting documentation.
It is highly recommended that the user read through this prior to using the
drivers and soft front panel, particularly the section titled Requirements
and the Copyright and Licensing Agreement at the end.


Inventory

After executing the SETUP.EXE file in the install disks, the following files
should be present in the vxipnp directory structure on the users hard disk.
If any of these files are missing, the user should attempt to re-install the
software or contact Racal Instruments customer support at (800) 722-3262.
The latest copy of the install software may be downloaded from the Racal
Instruments BBS at (714) 859-2527 (8 bits - No parity - 1 stop bit).

Directory             File       Description
-----------------------------------------------------------------------------
vxipnp/kbase        ri3151.kb    Knowledge base

vxipnp/win/bin      ri3151.dll   Driver software in DLL format

vxipnp/win/doc      ri3151.doc   Driver documentation

vxipnp/win/include  ri3151.bas   Visual Basic header
vxipnp/win/include  ri3151.h     ANSI "C" language header

vxipnp/win/lib/msc  ri3151.lib   Microsoft "C" compatible library

vxipnp/win/ri3151   readme.txt   This file
vxipnp/win/ri3151   ri3151.c	 ANSI "C" language source code
vxipnp/win/ri3151   ri3151.def   Microsoft "C" DEF file for building the DLL
vxipnp/win/ri3151   ri3151.exe   Executable Soft Front Panel
vxipnp/win/ri3151   ri3151.fp    LabWindows/CVI 3.0 front panels
vxipnp/win/ri3151   ri3151.hlp   Microsoft Windows 3.1 help file
vxipnp/win/ri3151   ri3151.mak   Microsoft "C" Make file for building the DLL
vxipnp/win/ri3151   ri3151.prj   LabWindows/CVI 3.0 project file


Requirements

In order to successfully utilize the various driver files, there are certain
requirements that must be met regarding the system that this software is to
be used on.  These minimum requirements are laid out in the VXIplug&play
specification document VPP-2.  In general, the minimum hardware requirements
for the WIN framework are:

    - The system computer must be 100% IBM PC compatible and:
    - It must be have an 80486/33 MHz or greater CPU with floating point
    - It must have at least a 120-MB hard disk
    - It must have a VGA or higher compatible monitor
    - It must have a 3.5-inch, 1.44-MB floppy disk drive
    - It must have at least 8-MB RAM (Racal Instruments recommends 16-MB)
    - It must have a Windows compatible mouse
    - It must have the capability to control VXI message-based and
        register-based instruments

In addition, there are certain minimum requirements in regard to software
that must be met in order for the Racal Instruments 3151 & 3152 drivers to operate
properly.  These minimum requirements are:

    - Microsoft DOS version 5.0 or higher and Microsoft Windows version 3.1
        or higher
    - VISA Transition Library version 3.0 or higher installed in the
        /WINDOWS/SYSTEM directory as VISA.DLL
    - The minimum necessary resource manager software needed to support
        the current version of VISA.DLL

                             ***** WARNING *****
Attempting to run the Soft Front Panel or any executable based on the 3151 & 3152
drivers without having the VISA.DLL file installed will most likely result
in a User Application Error or General Protection Fault and may lock up the
users Windows environment.

If the user does not have VISA.DLL installed in the /WINDOWS/SYSTEM
directory, then a VISA install disk should be obtained from the company
that provided the Slot 0 Resource Manager device.


Using the Soft Front Panel

To run the 3151 & 3152 soft front panel, either double click on the RI3151 iconin the VXIPNP group in the Windows Program Manager or click on the ri3151.exe
file in the VXIPNP/WIN/RI3151 directory in the file manager and execute by
selecting the  "Run..." option under the "File" pull-down menu.  Before
executing, verify that the following has been accomplished:

    - At least one correctly configured 3151 or 3152 module has been installed in
        the VXI chassis (See the 3151 & 3152 Instruction Manuals or the knowledge
        base file for information on how to correctly configure and install
        a 3151 & 3152 module)
    - The VXI chassis has power applied
    - The appropriate resource manager software has been run; i.e.
        VXIInit and Resman for National Instruments based controllers
        or EPConnect for Radisys controllers
    - The resource manager has correctly identified and initialized the
        3151 & 3152 module(s)

    NOTE:  if VISA is installed, and no 3151 & 3152s are found, the soft front
           panel will execute in a "demonstration mode", which can be
           used to illustrate the capabilities of the 3151 & 3152

If a single module is installed, the soft front panel will automatically
connect to that device.  If multiple 3151 & 3152 modules are installed, the soft
front panel will display a dialog box titled "3151 Autoconnect" requesting
that the user specify which module to establish a communication channel
with.  This dialog box lists the interface type/interface number, slot
number and logical address of each 3151.  To select a 3151 or 3152, click on 
the check box to the left of the desired 3151 or 3152.

If a successful connection has been made, the "Active" LED will be green.

Clicking on the "Back" button of each sub-panel will return control back
to the main panel.

Using the Drivers

There are two functions that are crucial to the proper use of the 3151 & 3152
drivers and three others that the user should be aware of.  The two crucial
functions are ri3151_init and ri3151_close.

The ri3151_init function is used to establish a communication channel with
the 3151 & 3152 instrument via the VISA I/O library.  It is necessary to call this
function prior to calling any other driver functions.
  To use this function,
the user will need to construct a string called an "Instrument Descriptor".
This string specifies the type of interface, the interface address, the VXI
logical address and for GPIB-VXI interfaces the GPIB primary address.
The syntax for this string may be obtained by looking at the help 
documentation under the ri3151_init function.  The function will return an 
error code that is a negative value if it encounters a problem.  If
the function successfully connects to the instrument, then the last parameter
of the function will contain a value known as an "Instrument Handle".  This
Instrument Handle must be used when calling subsequent 3151 & 3152 driver functions
to allow those functions to utilize the I/O channel that was established by 
the ri3151_init function.

                             ***** Note *****
There is a maximum of 12 Instrument Handles that the 3151 & 3152 driver can keep
track of.  Attempts to initialize a 3151 & 3152 instrument when 12 3151 & 3152's are
already open will result in an error.  The user must close one or more
instrument sessions to establish communications with subsequent 3151 & 3152's.
Also note that repeatedly calling the ri3151_init function with the same
Instrument Descriptor will return unique Instrument Handles to the same
instrument.  Doing this may cause unpredictable results since one session
may affect the other and the user may inadvertantly attempt to open more
than 12 sessions without properly closing previous sessions.

The ri3151_close function is used to close a communication channel that has
previously been established via the ri3151_init function.  It is important
that the user close out the instrument sessions when all communications are
complete since memory resources are being locked by that session.  Failure
to close old unused sessions may result in unpredictable behavior as more
sessions are opened since memory resources may become scarce.  This is
particularly important during the development process since errors may be
made by the user that necessitate the need to re-establish communications
to an instrument.  If there is any doubt, the user should call the close
function to ensure that the channel is closed.

In addition to the two functions noted above, there is another function
which the user may find useful.  This is the ri3151_error_message function.
The ri3151_error_message function will take the error return code from
any of the 3151 & 3152 driver functions and will provide a text string that
describes the error.  This is useful since the meanings of the error codes
that are listed in the VISA specification as well as the error codes that
are specific to the 3151 & 3152 instrument drivers may not be immediately available
to the user.


             RACAL INSTRUMENTS, INC. PROGRAM LICENSE AGREEMENT

This is a legal agreement between you (either an individual or an entity)
and Racal Instruments, Inc. (Racal Instruments).   By using the Program
provided on this disk, you agree to be bound by the terms of this Agreement.
If you do not agree to the terms of this Agreement, promptly remove all
installed copies of the Program and return the installation disks to Racal
Instruments.  The Program (including any images or text incorporated into
the Program) and documentation on this installation disk are owned by Racal
Instruments and must be treated as any other copyrighted material.  You
are permitted to use this Program and have a royalty-free right to produce
and distribute executable files created using this Program.  You are also
permitted to modify the source files for your own use, as well as for
re-distribution.  Also, when you (or any other party) modify and/or
re-distribute the Program as permitted herein, Racal Instruments wants
to make certain all recipients of the modified Program understand that
they will not be receiving the the original version, so that any problems
introduced by others will not reflect on Racal Instruments' reputation.

                                  License

You are granted a nonexclusive license to use, copy, distribute and/or
modify the Program.  Title and ownership of the original Program remains
in Racal Instruments or its suppliers.  When copying the program in its
original form, you agree to respect and not remove or conceal from view
any copyright, trademark or confidentiality notices appearing on the
Program, and to reproduce any such copyright, trademark or confidentiality
notices on all copies of the Program or any portion thereof made by you
as permitted hereunder and on all portions contained in or merged into
other programs.

To the extent that Racal Instruments or its authorized suppliers provide
to you after delivery of the Program any improvements or new releases
for the Program, such improvements and new releases shall be subject to
the terms and conditions of this Agreement, unless otherwise provided
for under a separate agreement.

                                    Term

This license is effective until terminated.  You may terminate it at
any time by destroying the Program and documentation with all copies,
modifications and merged portions in any form.  It will also terminate
upon conditions set forth elsewhere in this Agreement if you fail to
comply with any term or condition of this Agreement.  You agree upon
such termination to destroy the Program and documentation together with
all copies and merged portions in any form.

                                 NO WARRANTY

BECAUSE THE PROGRAM IS LICENSED FREE OF CHARGE, THERE IS NO WARRANTY
FOR THE PROGRAM, TO THE EXTENT PERMITTED BY APPLICABLE LAW.  EXCEPT
WHEN OTHERWISE STATED IN WRITING, RACAL INSTRUMENTS PROVIDES THE 
PROGRAM "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESS OR
IMPLIED, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF
MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE.  THE ENTIRE
RISK AS TO THE QUALITY AND PERFORMANCE OF THE PROGRAM IS WITH YOU.
RACAL INSTRUMENTS DOES NOT WARRANT THAT THE FUNCTIONS CONTAINED IN
THE PROGRAM WILL MEET YOUR REQUIREMENTS OR THAT THE OPERATION OF THE
PROGRAM WILL BE UNINTERRUPTED OR ERROR FREE.  RACAL INSTRUMENTS AND/OR
ITS SUPPLIERS ENTIRE LIABILITY AND YOUR EXCLUSIVE REMEDY UNDER THIS
AGREEMENT IS THAT RACAL INSTRUMENTS SHALL REPLACE ANY DISKETTE FAILING
TO PERFORM SOLELY DUE TO DEFECTIVE MEDIA.

SOME STATES DO NOT ALLOW THE EXCLUSION OF IMPLIED WARRANTIES, SO THE
ABOVE EXCLUSION MAY NOT APPLY TO YOU.  THIS WARRANTY GIVES YOU SPECIFIC
LEGAL RIGHTS, AND YOU MAY ALSO HAVE OTHER RIGHTS WHICH VARY FROM STATE
TO STATE.

IN NO EVENT UNLESS REQUIRED BY APPLICABLE LAY OR AGREED TO IN WRITING
WILL RACAL INSTRUMENTS OR ANY OTHER PARTY WHO MAY MODIFY AND/OR
REDISTRIBUTE THE PROGRAM AS PERMITTED ABOVE, BE LIABLE TO YOU FOR
DAMAGES, INCLUDING ANY GENERAL, SPECIAL, INCIDENTAL OR CONSEQUENTIAL
DAMAGES ARISING OUT OF THE USE OR INABILITY TO USE THE PROGRAM
(INCLUDING BUT NOT LIMITED TO LOSS OF DATA OR DATA BEING RENDERED
INACCURATE OR LOSSES SUSTAINED BY YOU OR THIRD PARTIES OR A FAILURE
OF THE PROGRAM TO OPERATE WITH ANY OTHER PROGRAMS), EVEN IN SUCH HOLDER
OR OTHER PARTY HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES.

SOME STATES DO NOT ALLOW THE LIMITATION OR EXCLUSION OF LIABILITY FOR
INCIDENTAL OR CONSEQUENTIAL DAMAGES SO THE ABOVE LIMITATION OR
EXCLUSION MAY NOT APPLY TO YOU.

This Agreement will be governed by the internal laws of the State
of California.

YOU ACKNOWLEDGE THAT YOU HAVE READ THIS AGREEMENT, UNDERSTAND IT AND
AGREE TO BE BOUND BY ITS TERMS AND CONDITIONS.  YOU FURTHER AGREE THAT
IT IS THE COMPLETE AND EXCLUSIVE STATEMENT OF THE AGREEMENT BETWEEN YOU
AND RACAL INSTRUMENTS (US) WHICH SUPERSEDES ANY PROPOSAL, PRIOR OR
CONTEMPORANEOUS AGREEMENT, ORAL OR WRITTEN, AND ANY OTHER COMMUNICATIONS
BETWEEN US RELATING TO THE SUBJECT MATTER OF THIS AGREEMENT.

                                  APPENDIX

How to Comply With These Terms if You Modify the Program:

IF YOU PROCEED TO MODIFY THE PROGRAM, AS THE AUTHOR OF THE MODIFIED
VERSION, YOU ARE REQUIRED TO AFFIX AN APPROPRIATE "COPYRIGHT" NOTICE
IDENTIFYING YOU AS THE AUTHOR OF THE REVISED PROGRAM, AND CONVEY THE
EXCLUSION OF WARRANTY TERMS TO EACH RECIPIENT.  IF THERE IS ANY QUESTION
AS TO THE FORM OR PLACEMENT OF THE REQUIRED NOTICE, PLEASE CONTACT
RACAL INSTRUMENTS AT THE ADDRESS RECITED ABOVE.
