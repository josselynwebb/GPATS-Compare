
README.TXT for Racal 4152A Multimeter 
Driver Revision A.01.01
May 12, 1998

Contents:

1    Introduction

2    VTL/VISA Specific Information
2.1    Instrument Addresses
2.2    Using Callbacks and Soft Front Panel Timeouts
2.3    Executing the Soft Front Panel without VTL/VISA

3    Using the Racal 4152A Driver in Application Development Environments
3.1    Microsoft Visual C/C++
3.2    Borland C/C++
3.3    Microsoft Visual Basic

4.0  Online Information

5.0  Revision History

- ---------------------------------------------------------------------------

1.   Introduction

The help file included with the Racal 4152A VXIplug&play driver contains
instrument and programming information. This readme file contains
additional information for the Racal VXIplug&play programmer.

- ---------------------------------------------------------------------------

2.   VTL/VISA Specific Information

The following topics refer to instances when the driver is used with
different versions of VTL.

2.1    Instrument Addresses

When using Racal VXIplug&play instrument drivers, instrument addresses must
be all uppercase letters. Implementation of the addressing scheme is
vendor specific and some vendors support mixed cases. However, for
maximum portability, the instrument address should use uppercase
characters only.

For example:  use "VXI0::56::INSTR" instead of "vxi0::56::instr"

2.2   Using Callbacks and Soft Front Panel Timeouts

Callbacks are not supported in win32 environment.

2.3  Executing the Soft Front Panel without VTL/VISA

The VTL/VISA dynamic link library provides a communication library
between your program and the instrument.  If you receive a message
that VISA.DLL cannot be found when executing the soft front panel
(Racal4152A.exe), VTL/VISA is not installed on your system. To
communicate with the Racal 4152A, you must install VTL/VISA from discs
supplied with your computer or I/O card.  However, it is still
possible to execute the soft front panel in a demonstration mode
without VTL/VISA and/or the Racal 4152A.  Simply press 'ok' when the
dialog box appears and the soft front panel will then become
available.

- --------------------------------------------------------------------------

3.   Using the Racal 4152A Driver in Application Development Environments

This section offers suggestions on using the Racal4152A.dll within
various application development environments.

3.1    Microsoft Visual C/C++

Refer to your Microsoft Visual C/C++ manuals for information on linking
and calling .DLLs.

    In addition to the Racal4152A.h header file located in the
    the ~vxipnp\win\include directory, the driver needs additional
    header files located in the ~\vxipnp\win\Racal4152A directory.  The
    ~ refers to the directory in the VXIPNP variable.  By default this
    is set to C:\.

3.2    Borland C/C++

Refer to your Borland C/C++ manuals for information on linking and
calling .DLLs.

    In addition to the Racal4152A.h header file located in the
    the ~vxipnp\win\include directory, the driver needs additional
    header files located in the ~\vxipnp\win\Racal4152A directory.  The
    ~ refers to the directory in the VXIPNP variable.  By default this
    is set to C:\.

3.3   Microsoft Visual Basic

Refer to the Microsoft Visual BASIC manual for information on calling DLLs.
The BASIC include file is Racal4152A.bas, which is contained in the directory
~vxipnp\win\include. The ~ refers to the directory in the VXIPNP variable.  
By default this is set to C:\.

- ---------------------------------------------------------------------------

4.0  Online Information

  The latest copy of this driver and other Racal VXIplug&play drivers can
be obtained via anonymous ftp @ fcext3.external.Racal.com in the directory
dist/mxd/vxipnp/win/supported.  The Racal4152A is located in a self-extracting
archive file called 4152A_32.exe.  If you do not have ftp access the driver
can be obtained or by calling Racal On-line Support at (800) 859-8999

- ---------------------------------------------------------------------------

5.0  Revision History

  A.01.00  May 15, 1998  Initial Release.

- ---------------------------------------------------------------------------

