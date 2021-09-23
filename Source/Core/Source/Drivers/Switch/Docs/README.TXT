***************************************************************************
*   Racal Instruments 1260 Series Switch Controller VXIplug&play Driver   *
*  Copyright (c) 1994-1996, Racal Instruments, Inc.  All rights reserved. *
***************************************************************************


Introduction

The Racal Instruments VXIplug&play instrument driver provides comprehensive
access to the capabilities of the 1260 Series of Switching and Digital
modules.  The following information is provided to give the user a better
idea of how and when to use these drivers and the supporting documentation.
It is highly recommended that the user read through this prior to using the
drivers and soft front panel, particularly the section titled Requirements
and the Copyright and Licensing Agreement at the end.


Inventory

After executing the SETUP.EXE file in the install disks, the following files
should be present in the vxipnp directory structure on the users hard disk.
If any of these files are missing, the user should attempt to re-install the
software or contact Racal Instruments customer support at (800) 722-3262.

Directory             File       Description
-----------------------------------------------------------------------------
vxipnp/kbase        ri1260.kb    Knowledge base

vxipnp/win/bin      ri1260.dll   Driver software in DLL format

vxipnp/win/doc      ri1260.doc   Driver documentation

vxipnp/win/include  ri1260.bas   Visual Basic header
vxipnp/win/include  ri1260.h     ANSI "C" language header

vxipnp/win/lib/msc  ri1260.lib   Microsoft "C" compatible library

vxipnp/win/ri1260   lvdevice.dll Required to support the Soft Front Panel
vxipnp/win/ri1260   readme.txt   This file
vxipnp/win/ri1260   ri1260.c     ANSI "C" language source code
vxipnp/win/ri1260   ri1260.def   Microsoft "C" DEF file for building the DLL
vxipnp/win/ri1260   ri1260fp.exe Executable Soft Front Panel
vxipnp/win/ri1260   ri1260.fp    LabWindows/CVI 3.1 front panels
vxipnp/win/ri1260   ri1260.hlp   Microsoft Windows 3.1 help file
vxipnp/win/ri1260   ri1260.mak   Microsoft "C" Make file for building the DLL
vxipnp/win/ri1260   ri1260.prj   LabWindows/CVI 3.1 project file


Requirements

In order to successfully utilize the various driver files, there are certain
requirements that must be met regarding the system that this software is to
be used on.  These minimum requirements are laid out in the VXIplug&play
specification document VPP-2.  In general, the minimum hardware requirements
for the WIN framework are:

    - The system computer must be 100% IBM PC compatible and:
    - It must be have an 80486/33 MHz or greater CPU with floating point
        (Racal Instruments recommends a 90 MHz Pentium)
    - It must have at least a 120-MB hard disk
    - It must have a VGA or higher compatible monitor
    - It must have a 3.5-inch, 1.44-MB floppy disk drive
    - It must have at least 16-MB RAM (Racal Instruments recommends 24-MB)
    - It must have a Windows compatible mouse
    - It must have the capability to control VXI message-based and
        register-based instruments

In addition, there are certain minimum requirements in regard to software
that must be met in order for the Racal Instruments 1260 drivers to operate
properly.  These minimum requirements are:

    - Microsoft DOS version 5.0 or higher and Microsoft Windows version 3.1
        or higher
    - VISA Transition Library version 3.0 or higher installed in the
        /WINDOWS/SYSTEM directory as VISA.DLL
    - The minimum necessary resource manager software needed to support
        the current version of VISA.DLL

                             ***** WARNING *****
Attempting to run the Soft Front Panel or any executable based on the 1260
drivers without having the VISA.DLL file installed will most likely result
in a User Application Error or General Protection Fault and may lock up the
users Windows environment.

If the user does not have VISA.DLL installed in the /WINDOWS/SYSTEM
directory, then a VISA install disk should be obtained from the company
that provided the Slot 0 Resource Manager device.


Using the Soft Front Panel

To run the 1260 soft front panel, either double click on the RI1260 icon in
the VXIPNP group in the Windows Program Manager or click on the ri1260fp.exe
file in the VXIPNP/WIN/RI1260 directory in the file manager and execute by
selecting the  "Run..." option under the "File" pull-down menu.  Before
executing, verify that the following has been accomplished:

    - At least one correctly configured 1260 card set has been installed in
        the VXI chassis (See the 1260 Instruction Manual or the knowledge
        base file for information on how to correctly configure and install
        a 1260 card set)
    - The VXI chassis has power applied
    - The appropriate resource manager software has been run; i.e.
        VXIInit and Resman for National Instruments based controllers
        or EPConnect for Radisys controllers
    - The resource manager has correctly identified and initialized the
        1260 card set(s)

If a single card set is installed, the soft front panel will automatically
connect to that device.  If multiple 1260 card sets are installed, the soft
front panel will display a dialog box titled "1260 Autoconnect" requesting
that the user specify which card set to establish a communication channel
with.  This dialog box lists the interface type/interface number, slot
number and logical address of each 1260 card that contains an Option 01
controller.  (Only the modules that have Option 01 controllers are recognized
since the Option 01 contains the VXI interface hardware and firmware used to
communicate with the actual modules).  To select a 1260 card set, use the
scroll bars to display the desired card set and click on the button to the
left of the displayed information.

If a successful connection has been made, the "Active" LED will be green and
the "Slot(s)" display will list the range of slot numbers for all of the 1260
modules that are currently under control.  If after selecting a 1260 card
set, the user wishes to communicate with other 1260 card sets, this is
accomplished by clicking on the "Exit" button on the soft front panel, then
clicking on the "Connect" button in the following dialog box and selecting
the 1260 card set to communicate with in the 1260 Autoconnect dialog box.

The soft front panel will automatically detect and display the names of the
modules that are installed as well as their module addresses.  (The module
address is set via DIP switch S1 on each module and must be unique for each
module controlled by a particular Option 01 controller.)  System messages
will appear in the "Messages" box.  Software or firmware revision information
may be obtained by clicking on the "About" button.  The Option 01 controllers
self test may be run by clicking on the "Self Test" button.  The results of
the self test will appear in the "Messages" box.  A device clear may be sent
to the instrument by clicking on the "Reset Box".

An area is provided for the user to enter in the slot numbers of the modules
that are installed.  When the soft front panel is terminated, the software
will save the slot information in a file in the /VXIPNP/WIN/RI1260 directory
with the file name RI1260SD.xxx where xxx is the logical address number for
the card set.

To close relays on one of the switch modules listed, the user must click once
over the module name.  This will bring up a secondary panel that will have
all the relays in graphical form.  To change a relays state, click all the
switches to be modified into the appropriate position, then click on the
"Execute Changes" button.  The user may restore the panel to the current true
state of the relays at any time prior to clicking on "Execute Changes" by
clicking on the "Cancel Changes" button.  Clicking on the "Open All" button
will return all relays in that module to the default or de-energized state.
Clicking on the "Sequence Mode" button will allow the user to specify the
order in which to modify the relay states.  The available sequence modes are
Break Before Make (BBM) , Make Before Break (MBB) and Immediate (IMM).
Clicking on the "Exit" button will return control back to the main panel.

Clicking on the "Option 01" label in the main panel will allow the user to
setup various options including selecting the TTLTRG and SYNC line; storing
and recalling relay states from non-volatile memory; specifying the exclude,
equate and scan lists; specifying the sync delay, power-up recall state and
scan list state; and sending word serial trigger commands to the 1260.  See
the Operations section of the 1260 Instruction Manual for information about
how these advanced features work.


Using the Drivers

There are two functions that are crucial to the proper use of the 1260
drivers and three others that the user should be aware of.  The two crucial
functions are ri1260_init and ri1260_close.

The ri1260_init function is used to establish a communication channel with
the 1260 instrument via the VISA I/O library.  It is necessary to call this
function prior to calling any other driver functions (with the exception of
the ri1260_closeAllRMSessions and ri1260_error_message functions) in order
for the driver software to know how to communicate with the instrument.
To use this function, the user will need to construct a string called an 
Instrument Descriptor.  This string specifies the type of interface, the
interface address, the VXI logical address and for GPIB-VXI interfaces the
GPIB primary address .  The syntax for this string may be obtained by looking
at the help documentation under the ri1260_init function.  The function will
return an error code that is a negative value if it encounters a problem.  If
the function successfully connects to the instrument, then the last parameter
of the function will contain a value known as an "Instrument Handle".  This
Instrument Handle must be used when calling subsequent 1260 driver functions
to allow those functions to utilize the I/O channel that was established by 
the ri1260_init function.

                             ***** Note *****
There is a maximum of 12 Instrument Handles that the 1260 driver can keep
track of.  Attempts to initialize a 1260 instrument when 12 1260's are
already open will result in an error.  The user must close one or more
instrument sessions to establish communications with subsequent 1260's.
Also note that repeatedly calling the 1260_init function with the same
Instrument Descriptor will return unique Instrument Handles to the same
instrument.  Doing this may cause unpredictable results since one session
may affect the other and the user may inadvertantly attempt to open more
than 12 sessions without properly closing previous sessions.

The ri1260_close function is used to close a communication channel that has
previously been established via the ri1260_init function.  It is important
that the user close out the instrument sessions when all communications are
complete since memory resources are being locked by that session.  Failure
to close old unused sessions may result in unpredictable behavior as more
sessions are opened since memory resources may become scarce.  This is
particularly important during the development process since errors may be
made by the user that necessitate the need to re-establish communications
to an instrument.  If there is any doubt, the user should call the close
function to ensure that the channel is closed.

In addition to the two functions noted above, there are three other functions
which the user may find useful.  The three functions are ri1260_find1260 and
ri1260_closeAllRMSessions, and ri1260_error_message.

The ri1260_find1260 function is used to determine the slot number, logical
address, interface type, and interface number for each 1260 that can be
found by the VISA I/O library.  This information is useful in that it is
needed for constructing Instrument Descriptor strings.

The ri1260_closeAllRMSessions function is used to close all 12 instrument
sessions simultaneously.  It is particularly useful in that it does not
require the user to specify the Instrument Handle's of the sessions to
close, (which may have been lost inadvertently due to problems during
the development phase.)  It is recommended that this function only be
used in extreme cases where the user has lost the Instrument Handle
information, has no way to recover it, and is getting an error indicating
that there are no more 1260 sessions available.

The ri1260_error_message function will take the error return code from
any of the 1260 driver functions and will provide a text string that
describes the error.  This is useful since the meanings of the error codes
that are listed in the VISA specification as well as the error codes that
are specific to the 1260 instrument drivers may not be immediately available
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
they will not be receiving the original version, so that any problems
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
