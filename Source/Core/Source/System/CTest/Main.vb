'Option Strict Off
'Option Explicit On

Imports System
Imports System.Windows.Forms
Imports System.Windows.Forms.Screen
Imports System.Text
Imports System.Drawing
Imports System.IO
Imports System.Runtime.InteropServices
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.Compatibility
Imports Microsoft.VisualBasic.Compatibility.VB6
Imports System.Management
Imports Microsoft.Win32
Imports FHDB
Imports RFMSLib

Public Module CTestMain


    '=========================================================
    '**************************************************************
    '**************************************************************
    '** ManTech Test Systems Software Module                     **
    '**                                                          **
    '** Nomenclature   : VIPERT SYSTEM CONFIDENCE TEST           **
    '** Version        : 2.3                                     **
    '** Written By     : Michael McCabe                          **
    '** Purpose        : This program performs a power up        **
    '**                  confidence test on the VIPERT system    **
    '** Program Begins Executing Instructions In Sub:MAIN        **
    '**                                                          **
    '**------ Revision History for V1.5 (VIPERT Release 2): -------**
    '** 05/04/98 GJohnson                                        **
    '**     Code added to wait for the SysLog program to         **
    '**     complete before closing the Ctest program. Increased **
    '**     delay time between the Ctest closing the local log   **
    '**     file and Syslog opening the local log file.          **
    '**                                                          **
    '**------ Revision History for V1.6 (VIPERT Release 3): -------**
    '** 05/13/98 JHill                                           **
    '**     Changed delay in InitMessageBasedInst() between      **
    '**     sending '*IDN?' and reception from 0.2 to 0.5 sec to **
    '**     make it work for RFPM ans RF-CT at -10 deg c.        **
    '** 07/24/98 JHill                                           **
    '**     'TestDownCnvtr' Function:                            **
    '**         Changed STATUS_REG write from hFFF0 to hFFFC to  **
    '**         "write ones to the device-dependant bits of the  **
    '**         control register" IAW page 2-5 of 1315A manual.  **
    '**           Set the Switch Control Register to known state **
    '**         to stop 'Ref Gen Module' failures in confidence  **
    '**         test due to the Internal Reference not being set **
    '**         to Internal-Off.                                 **
    '**     Swapped values for LO_CONTROL_REG and SWITCH_        **
    '**     Control_REG constants. They were reversed.           **
    '**     Cleaned up Echo and HandleDetails subs to show four  **
    '**     lines instead of two and eliminate flicker.          **
    '** 08/06/98 RZeger                                          **
    '**     InitModAnal Function:                                **
    '**         increased delay after CL to 1.5 and added a      **
    '**         delay 3 before and after ID to improve reliability*
    '**     CheckInit:                                           **
    '**         Created this sub to re-init if previous attempts **
    '**         at initialization failed.                        **
    '**     pctIcon_Click:                                       **
    '**         Added call to CheckInit to initialize if it      **
    '**         failed to pass initialization in the past.       **
    '** 08/06/98 GJohnson TDR98283 V1.6                          **
    '**     TestDigital:                                         **
    '**         Added code to check for file M910Test.exe prior  **
    '**         to running M910test.bat file.                    **
    '** 10/29/98 JHill                                           **
    '**     GetDateSerial Funciton:                              **
    '**         Modified DMM, ARB and OSCOPE cases to handle a   **
    '**         wider variety of date formats.                   **
    '**------ Revision History for V1.7 (VIPERT Release 4): -------**
    '** 2/15/99 Ver 1.7 GJohnson                                 **
    '**     CenterForm Funciton:                                 **
    '**         Added a CenterForm Funtion to Main.bas.  This was**
    '**         performed in the LoadForm Function.  It evaluates**
    '**         the size of the screen and sets the form to the  **
    '**         top = 0 if the screen height is less than 7200   **
    '**         Per ECO-3047, TDR99005                           **
    '** 2/15/99 Ver 1.7 GJohnson                                 **
    '**     ProcessCalDates Funciton:                            **
    '**         Added HP8481A & D Power Sensor dates to process  **
    '**         to help calculate SYSTEM EFFECTIVE DATE.  Per    **
    '**         ECO-3047-, TDR98330                              **
    '** 2/16/99 Ver 1.7 GJohnson                                 **
    '**     TestInstrument Function:                             **
    '**         Added separate case statement for the RFPM to    **
    '**         remind the user to attach the power sensor Cable **
    '**         if the Power meter fails.  Then retest RFPM.     **
    '**         ECO-3047-, TDR99004                              **
    '**------ Revision History for V1.9 (VIPERT Release 6): -------**
    '** 6/9/99 Ver 1.9 GJohnson                                  **
    '**     Test1553 Funciton:                                   **
    '**         Modified program to use 1553selftst.exe program  **
    '**         because of new ACE 1553 driver version 4.5.      **
    '** 6/22/99 Ver 1.9 GJohnson                                 **
    '**     MAIN Funciton:                                       **
    '**         Modified instrument initialization to set the    **
    '**         default value of the Counter/Timer and RF Counter**
    '**         impedance level from 50 Ohms to 1 Mohm.          **
    '**------ Revision History for V1.10                  -------**
    '** 5/23/00 Ver 1.10  Quoc Nguyen & Jeff Hill                **
    '**    'InitDownCnvtr' Function:                             **
    '**        Modified this function to check to see if the RF  **
    '**        Downconverter is available. This function also    **
    '**        determines if the EIP 1315A Downconverter or the  **
    '**        Giga-tronics 55210A Downconverter is installed    **
    '**    'TestDownCnvtr' Function:                             **
    '**        Modified this function to test the presence of    **
    '**        RF Downconverter in the system                    **
    '**    'Sub ProcessCalDates()' procedure:                    **
    '**        "SWITCH4" has been removed from this procedure    **
    '**        because The PLC data for the MF Switches is no    **
    '**        longer measured for each system and therefore has **
    '**        no associated calibration due date. It is         **
    '**        constant for all system                           **
    '**        "If RFOptionInstalled% Then ...... End If"        **
    '**        statement has been added to "Case HP8481A" and    **
    '**        "Case HP8481D" because on a (V)1 system (non-RF), **
    '**        the Confidence Test considers calibration dates   **
    '**        for the RF Power Meter sensors when determining   **
    '**        the overall system calibration due date. Since    **
    '**        there are no power sensors on a (V)1 system, the  **
    '**        date is always blank, causing the system to       **
    '**        always appear to be due for calibration when      **
    '**        started                                           **
    '**    'Sub PrintCalDate()' procedure:                       **
    '**        "(Instldx% = SWITCH4)" has been removed from      **
    '**        this procedure because The PLC data for the MF    **
    '**        Switches is no longer measured for each system    **
    '**        and therefore has no associated calibration due   **
    '**        date. It is constant for all system.              **
    '**    'Sub TestSwitching()' procedure:                      **
    '**        The problem with this sub is that the occasional  **
    '**        Error<8> or Error<9> occurs during burn-in. The   **
    '**        solution for this problem is to add "Delay 0.5"   **
    '**        to this procedure.                                **
    '**    'Dim Sub Delay(Seconds As Single)'procedure:          **
    '**        The problem with this sub is that if midnight     **
    '**        occurs while it is executing, the program will be **
    '**        stuck in an infinite loop. The solution to this   **
    '**        problem is to replace this procedure with         **
    '**        'Dim Sub Delay(ByVal dSeconds As Double)'         **
    '**        procedure and 'Dim Function dGetTime() As         **
    '**        Double' procedure                                 **
    '**------ Revision History for V1.11 ------------------------**
    '** 02/16/00 V1.11  Jeff Hill                                **
    '**   1. Modifications to support EOV.                       **
    '**   2. Changed variant detection logic to support multiple **
    '**      variants per IPR in January, 2001.                  **
    '**   3. Changed GUI so that instrument labels are grayed-out**
    '**      until they are initialized. This helps the user     **
    '**      visualize the three-pass process: 1) init all       **
    '**      instruments, 2) get all cal dates, 3) BIT for all.  **
    '**------ Revision History for V2.0       ECO-3047-569  -----**
    '** 04/18/01 V2.0   Dave Joiner                              **
    '**     VIPERT-ECP-023        Fault History Database(FHDB)     **
    '**   1. Added a DataServices Module for FHDB Specific       **
    '**      Definitions and Routines.                           **
    '**   2. Added calls to identify faults and record in the    **
    '**      Database utilizing the ReportFailure Routine        **
    '**      whenever possible.                                  **
    '**   3. Failure Step Codes are generated to the System Log  **
    '**      as well as the FHDB thus uniquely indenting a Fault.**
    '** Self-initiated -- Modified the About Form to get         **
    '**      Version Information from the Project Properties.    **
    '** VIPERT-DR-143  04/18/01  Dave Joiner                       **
    '**   1. Added Sub ReportVariant() to read the Option Keys   **
    '**      from the VIPERT.ini File, determine the Variant,      **
    '**      and Write it on the first line of the System        **
    '**      Log for that session. Sub is called after           **
    '**      Initialization is completed in the Sub Main.        **
    '** VIPERT-DR-232  10/15/01  Dave Joiner                       **
    '**   1. Modified function SwitchingError&() incorporating   **
    '**      the incorrect usage of the VB Mid$() function.      **
    '**      Changed the "Length" argument to the correct Error  **
    '**      Code length.                                        **
    '** VIPERT-DR-240  11/21/01  DJoiner                           **
    '**   1. Installed M9SelfTest.exe from DTI Version 4.0.      **
    '** VIPERT-DR-178  11/21/01  DJoiner                           **
    '**   1. Modified TestPowerSupplies() to report Status       **
    '**      Errors as well as BIT Errors.                       **
    '** Self Initiated  DJoiner  12/04/01                        **
    '**     Add Option Explicit to frmAbout and frmCTest,        **
    '**     Declare all non-declared variables.                  **
    '** Self Initiated  DJoiner  02/03/02                        **
    '**     Resized Ctest.frm to provide additional space in the **
    '**     DetailsText TextBox for displaying the Log readout.  **
    '**     Changed the Font to Courier New 8 Point.             **
    '**     Resized and moved other form objects to present a    **
    '**     uniform appearance.                                  **
    '** Self Initiated  DJoiner  02/04/02                        **
    '**     Added a Status Query test for each DC power supply   **
    '**     in Function InitPowerSupplies() to detect an absent  **
    '**     or non-communicating supply.                         **
    '** Self Initiated  DJoiner  02/11/02                        **
    '**     Modified HandleDetails() in module CTestMain to      **
    '**     allow form to be dynamically sized.                  **
    '**   + Renamed Module1 to Rfcnt.                            **
    '**   + Added an Error Handler routine in Sub Main to        **
    '**     gracefully handle errors generated as a result of    **
    '**     a call from this module.                             **
    '**   + Added Error routine in Sub TestInstrument() located  **
    '**     in CTestMain to handle individual test errors.       **
    '**==========================================================**
    '**------ Revision History for V2.1       ECO-3047-590  -----**
    '** VIPERT-DR-276  08/09/02  Nikolai Sazonov                   **
    '**    Substituted IRWIN commands for GPIB commands in       **
    '**    procedures: InitEOModule, iEOSync, TestEOModule.      **
    '**    The system now communicates with the EO modules       **
    '**    by  opening handle over the GPIB bus and browsing     **
    '**    the strings that the modules return. IRWin module     **
    '**    has been excluded from the Confidence Test program.   **
    '** Supervisor-directed  08/09/02  Nikolai Sazonov           **
    '**    All tests have been renumbered in according to        **
    '**    the new format. Renumbering was occurred everywhere   **
    '**    the older format numbers were present (comments,      **
    '**    Screen echo, and error log)                           **
    '** - - - - - - - - EO FAT Revisions - - - - - - - - - - - - **
    '** EO Revisions    DJoiner  09/12/02                        **
    '**    Added identification revisions for EO FAT to support  **
    '**    the LTM/ARM.                                          **
    '** DJoiner  09/20/02                                        **
    '**    Modified functions InitEOModule() and TestEOModule()  **
    '**    to support LTM/ARM. Added LTM/ARM BIT and related     **
    '**    functions: sGetBitErrorMsg(), StringToList2(),        **
    '**    sLTM_ARM_Indent(), sEOErrCheck(), dEORead(),          **
    '**    bErrHasLocation(), iFilterNum() for support.          **
    '** DJoiner  09/23/02                                        **
    '**    Modified initEOModule() to delay before and after     **
    '**    "*IDN?" query. Deleted viRead Loop and added Debug    **
    '**    statements. Deleted viRead Loop from TestEOModule().  **
    '** DJoiner  09/27/02                                        **
    '**    Added InitEoModule=1 when LTM/ARM is detected. Added  **
    '**    a delay before and after "*IDN?" in BIT. Added Debug  **
    '**    statements to function TestEOModule.                  **
    '** - - - - - - - -  HMV Revisions - - - - - - - - - - - - - **
    '** HMV revisions  JHill 10/23/02                            **
    '**     Multiple additions to support HMV instruments.       **
    '**     Modified sub ReportVariant() to support HMV.         **
    '**     General cleanup.                                     **
    '** VIPERT-DR-259  10/23/02  Jeff Hill                         **
    '**     Removed all program support for the EIP 1143A RF     **
    '**     Stim, which is no longer used in VIPERT.               **
    '** Self-Identified  10/23/02  Jeff Hill                     **
    '**     Found that if the C/T bit test returned '1', this    **
    '**     program would pass it. Fixed.                        **
    '** Self-Identified  10/23/02  Jeff Hill                     **
    '**     Rearranged order of tests for faster execution.      **
    '**==========================================================**
    '**----- Revisions for V2.2, VIPERT Release 10.0, ECO-3047-664 **
    '** ALTA Revisions  Dave Joiner 12/17/03                     **
    '**     Changed all instances of LTM/ARM to ALTA, including  **
    '**     comments. Modified Instrument String Array           **
    '**     assignments in Function InitEoModule() to identify   **
    '**     the ALTA. Modified *IDN? return string to            **
    '**     "*SBIR,ALTA,...".                                    **
    '** EO FAT Revisions    Q1 2004     Jeff Hill                **
    '**  1. Modified Sub pctIcon_Click() in ctest.frm to reset   **
    '**     the EO button caption to signify that a different EO **
    '**     module may be tested.                                **
    '**  2. Modified Function FixtureInstalled() to remove       **
    '**     annoying "Fixture detected" Echo message when this   **
    '**     is the expected condition.                           **
    '**  3. Modified Sub InitInstrument() to NOT turn off EO     **
    '**     power between a successful init and the BIT phase.   **
    '**  4. Modified Function TestEoModule() to use the VISA     **
    '**     instrument handle opened in INIT rather than create  **
    '**     a new one.                                           **
    '**  5. Modified Function iEoSync() to not wait for SRQ      **
    '**     status to clear. Alan Irwin sez "it doesn't matter". **
    '**  6. Modified Sub EoPower() to set the bEoPowerOn flag    **
    '**     eariler in the routine so that if an Close action    **
    '**     is initiated during it's run, the power supplies get **
    '**     shutdown.                                            **
    '**  7. Modified Function InitEoModule() to use the same     **
    '**     VISA session handle as everybody else and not create **
    '**     a new one.                                           **
    '**  8. Modified Function InitEoModule() to increase the     **
    '**     Visible module initialize timeout from 30 to 60 sec. **
    '** EO FAT Revisions    Q2 2004     Jeff Hill                **
    '**  1. Modified Function InitEoModule() to add a *IDN query **
    '**     to all EO modules, since they have been recently     **
    '**     added by SBIR. Removed the old ID criterion of       **
    '**     waiting for Ready. That is still done as part of BIT.**
    '**  2. Changed Function TestEoVcc() and the Diag32.exe      **
    '**     script file Customer.dgs to try to resolve FAT DR 57.**
    '**     Added 'exit' line to Customer.dgs and deleted the 2nd**
    '**     Alt-F4 SendKeys. Now, it closes itself. Changed the  **
    '**     1st Alt-F4 to 'Enter' to close the 'All Tests Passed'**
    '**     msgbox from Diag32.exe.                              **
    '**  3. Changed Function TestEoVcc() to do a recheck of the  **
    '**     log file because sometimes at +55 DegC the Shell cmd **
    '**     continues prematurely causing CTest to fail before   **
    '**     the "Passed all tests" OK box pops up.               **
    '**  4. Changed Function InitEoModule() to increase timeout  **
    '**     for ALTA from 1 min to 2 min. Necessary at +55 degC. **
    '**  5. Many changes to test the EO modules when 1st powered **
    '**     up during Init. Saves time.                          **
    '**  6. Added a 6 sec delay to dEORead so that when there IS **
    '**     module error, it will report it, not ER=0. I guess   **
    '**     takes awhile for the module to detect an error after **
    '**     power up.                                            **
    '**  7. If IR mod passes, added command to set the Ready     **
    '**     Window to VIPERT default of 0.010 degrees.             **
    '**  8. Changed Function InitEoModule() so the identification**
    '**     criteria is a GPIB hit AND a *IDN? match. If it does **
    '**     not find both on the 1st GPIB hit, it tries the next **
    '**     address. Necessary because due to our system monitor **
    '**     action, sometimes VISA would return success for the  **
    '**     wrong GPIB address.                                  **
    '** EO FAT Revisions    2004        Jeff Hill                **
    '**  1. 8/11/04 Changed Function TestEoModule() to increase  **
    '**     the Wait-for-Ready time from 120 to 240 sec for the  **
    '**     IR module. Necessary because of intermittant failure **
    '**     of test EIR-02-002 at -10 DegC during FAT.           **
    '**  2. 8/30/04  Deleted function sLTM_ARM_Indent() to       **
    '**     correct ALTA error messages. Also fixed incorrect    **
    '**     index in function sGetBitErrorMsg for instrument     **
    '**     description array in error message.                  **
    '==============================================================
    '---  Revisions for V2.3, VIPERT Release 10.1, ECO-3047-690   ---
    '** DR-331   11/30/04       Dave Joiner                      **
    '**   Modified Sub Main() to check the EO_OPTION_INSTALLED   **
    '**   key in the VIPERT.INI file, execution is based on the    **
    '**   result. Function CheckSetEoInstalled() was added to    **
    '**   read the EO_OPTION_INSTALLED key in the VIPERT.ini file  **
    '**   and analyze the returned value. "YES", set flag; "NO", **
    '**   set Flag; Blank, Ask the Operator to Identify if a EO  **
    '**   variant. The VIPERT.ini file is updated as well as the   **
    '**   form's GUI.                                            **
    '** DR-328  2/7/05  Jeff Hill                                **
    '**   Modified InitEoModule() and ProcessCalDates() to print **
    '**   and display the serial numbers of EO modules. This is  **
    '**   to relate specific modules to cal dates and remind the **
    '**   user which module goes with the VIPERT. This is necessary**
    '**   because the modules are easily swapped among VIPERT.     **
    '** Self-Identified  2/8/05  Jeff Hill                       **
    '**   Modified Declaration comments, InitVar() and           **
    '**   InitEoModule() to commonize the naming convention of   **
    '**   the EO modules to "Infrared Source", "Visible Source", **
    '**   "ALTA" and "Modulated Source".                         **
    '** Self-Identified  2/8/05  Jeff Hill                       **
    '**   Included the EO Video Capture Module in the            **
    '**   calibration dates processing.                          **
    '** Self-Identified  2/8/05  Jeff Hill                       **
    '**   Replaced references to video capture module OEM "ITI"  **
    '**   with "Coreco Imaging".                                 **
    '** Self-Identified  2/8/05  Jeff Hill                       **
    '**   Modified InitHmCAN() to report failure correctly when  **
    '**   VIPERT has the HMV option and the CAN card is missing.   **
    '** Self-Identified  2/24/05  Jeff Hill                      **
    '**   Changed EO constant names and VIPERT.INI EO key names to **
    '**   coordinate with Self Test and SysCal program.          **
    '** TASK 68218 3/13/2020
    '**   Changed LAST_CAL_INST = TCIM 
    '**   it was originally was DIG_REF, But all RF and EO cal checks
    '**   were removed 3/13/2020
    '**************************************************************


    'Functions used for EO thru VEO2.dll
    Dim A As Short
    Dim B As Short
    Dim C As Short
    Dim D As Short
    Dim E As Short
    Dim F As Short
    Dim G As Short
    Dim H As Short
    Dim I As Short
    Dim J As Short
    Dim K As Short
    Dim L As Short
    Dim M As Short
    Dim N As Short
    Dim O As Short
    Dim P As Short
    Dim Q As Short
    Dim R As Short
    Dim S As Short
    Dim T As Short
    Dim U As Short
    Dim V As Short
    Dim W As Short
    Dim X As Short
    Dim Y As Short
    Dim Z As Short

    Declare Function GET_BIT_DATA_INITIATE Lib "C:\IRWin2001\VEO2.dll" () As Object
    Declare Function GET_BIT_DATA_FETCH Lib "C:\IRWin2001\VEO2.dll" (ByVal Error_Number As Integer) As Object

    '-----------------API / DLL Declarations------------------------------
    Declare Function GetPrivateProfileString Lib "kernel32" Alias "GetPrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String, ByVal lpReturnedString As StringBuilder, ByVal nSize As Integer, ByVal lpFileName As String) As Integer
    Declare Function WritePrivateProfileString Lib "kernel32" Alias "WritePrivateProfileStringA" (ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpString As String, ByVal lpFileName As String) As Integer
    Declare Function GetWindow Lib "user32" (ByVal hWnd As Integer, ByVal wCmd As Integer) As Integer
    Declare Function GetWindowText Lib "user32" Alias "GetWindowTextA" (ByVal hWnd As Integer, ByVal lpString As String, ByVal cch As Integer) As Integer

    Declare Function terM9_init Lib "terM9_32.dll" (ByVal rsrcName As String, ByVal idQuery As Short, ByVal resetInstr As Short, ByRef vi As Integer) As Integer
    Declare Function terM9_getSystemInformation Lib "term9_32.dll" (ByVal vi As Integer, ByVal systemDesc As StringBuilder, ByRef cardCount As Integer, ByRef channelCount As Integer) As Integer
    Declare Function terM9_getCardInformation Lib "term9_32.dll" (ByVal vi As Integer, ByVal cardIdx As Integer, ByRef cardId As Integer, ByVal cardName As StringBuilder, ByRef boardCount As Integer, ByRef chanCount As Integer, ByRef chassis As Integer, ByRef slot As Integer, ByVal addlInfo As StringBuilder) As Integer
    Declare Function terM9_setLowPower Lib "term9_32.dll" (ByVal vi As Integer, ByVal scopeIdx As Integer, ByVal state As Short) As Integer
    Declare Function terM9_close Lib "term9_32.dll" (ByVal vi As Integer) As Integer
    Declare Function terM9_selfTest Lib "term9_32.dll" (ByVal vi As Integer, ByRef selfTestResult As Integer, ByVal testMessage As String) As Integer

    Declare Function tat964_init Lib "tat964_32.dll" (ByVal rsrcName As String, ByVal idQuery As Short, ByVal resetInstr As Short, ByRef vi As Integer) As Integer
    Declare Function tat964_close Lib "tat964_32.dll" (ByVal vi As Integer) As Integer
    Declare Function tat964_dcbQuery Lib "tat964_32.dll" (ByVal vi As Integer, ByRef serialNumber As Short, ByVal assemblyRevision As String, ByVal VXILogicRevision As String, ByVal sequencerLogicRevision As String) As Integer
    Declare Function tat964_drQuery Lib "tat964_32.dll" (ByVal vi As Integer, ByVal frontEnd As Short, ByVal boardType As String, ByVal serialNumber As String, ByVal assemblyRevision As String, ByVal logicRevision As String, ByVal calibrationRevision As String, ByVal calibrationDate As String) As Integer
    Declare Function tat964_self_test Lib "tat964_32.dll" (ByVal vi As Integer, ByRef selfTestResult As Short, ByVal selfTestMessage As String) As Integer
    Declare Function tat964_queryModuleData Lib "tat964_32.dll" (ByVal vi As Integer, ByVal queryDataType As Short, ByRef queryData As Short) As Integer

    Declare Function atxml_IssueDriverFunctionCall Lib "AtXmlApi.dll" (ByVal XmlBuffer As String, ByVal Response As String, ByVal BufferSize As Short) As Integer

    'EO_VC Declarations, Variables and types
    Public BoardNames() As Object ' Holds board names
    Public Current_Board As Short ' 0 to Capture_Boards
    Public Current_Camera As Short ' 0 to 4
    Public Capture_Boards As Short
    Public CapMod(4) As Integer 'CapMod Pointers
    Public ITICam(16) As Integer 'Main Camera Pointers

    Public Num_Cameras As Integer
    Public Attr As New CAM_ATTR(0)

    Public SrcBytesPP As Integer

    Public Image_DX As Integer 'Image X Size Vars

    Public Image_DY As Integer 'Image y Size Var
    Public Host_Frame(,) As Byte ' this is the default size
    Public Const NumRingFrames As Short = 4

    'DTI Declarations
    Public Const NumDTIModules = 4
    Public Const DTIFirmwareRevision = 0.24
    Public dtiHandles(NumDTIModules) As Integer
    Public dtiResources() As String = {"VXI0::33::INSTR", "VXI0::34::INSTR", "VXI0::35::INSTR", "VXI0::36::INSTR"}

    'Video Capture Card Definitions
    Public Declare Function PXD_PIXCIOPEN Lib "XCLIBWNT.DLL" Alias "pxd_PIXCIopen" (ByVal c_driverparms As String, ByVal c_formatname As String, ByVal c_formatfile As String) As Integer
    Public Declare Function PXD_PIXCICLOSE Lib "XCLIBWNT.DLL" Alias "pxd_PIXCIclose" () As Integer


    <StructLayout(LayoutKind.Sequential)> _
    Structure CAM_ATTR
        Dim dwWidth As Integer
        Dim dwHeight As Integer
        Dim dwBytesPerPixel As Integer
        Dim dwBitsPerPixel As Integer
        Dim color As Integer
        Dim xZoom As Single
        Dim yZoom As Single
        Dim camName() As Byte

        Public Sub New(ByVal unusedParam As Integer)
            ReDim camName(80 - 1)
        End Sub
    End Structure

    '********************************************************************************

    Declare Function SystemParametersInfo Lib "user32" Alias "SystemParametersInfoA" (ByVal uAction As Integer, ByVal uParam As Integer, ByRef lpvParam As IntPtr, ByVal fuWinIni As Integer) As Integer
    Const SPI_GETWORKAREA As Integer = 48 'uiAction Constant for SystemParametersInfo function

    Structure RectType 'Structure used in lpvParam of SystemParametersInfo function
        Dim Left As Integer
        Dim Top As Integer
        Dim Right As Integer
        Dim Bottom As Integer
    End Structure
    Dim Rect As RectType

    Const GW_HWNDFIRST As Short = 0 'API Constant for Changing a Window "Z-Order"
    Const GW_HWNDNEXT As Short = 2 'API Constant for Changing a Window "Z-Order"

    Const TERM9_SCOPE_SYSTEM As Integer = 32768
    Const LOG_FILE As String = "Logfile.txt"
    Const VIPERT_INI As String = "ATS.ini"

    Const FAILED As Boolean = False

    Public Const PASSED As Boolean = True
    Const INITIALIZED As Boolean = True
    Const MSG_RESP_REG As Short = 10
    Const DOR_MASK As Short = &H2000
    Const MODAL As Short = 1
    Const MANUF As Short = 1
    Const MODEL As Short = 2
    Const MANUF_REG As Short = 0
    Const MODEL_REG As Short = 2
    Const STATUS_REG As Short = 4
    Const LO_CONTROL_REG As Short = 2 'JHill
    Const SWITCH_CONTROL_REG As Short = 0 'JHill
    Const GRAY As Integer = &H808080
    Const LIGHT_GRAY As Integer = &HC0C0C0
    Const SECS_IN_DAY As Integer = 86400


    'Instrument Indices used throughout this program
    '---- This section is Core instruments: (V)1
    Const MIL_STD_1553 As Short = 0 'MIL-STD-1553 Bus
    Const PPU As Short = 1 'Programmable Power Unit
    Const DIGITAL As Short = 2 'Digital Test Sub-system
    Const FGEN As Short = 3 'Function Generator
    Const COUNTER As Short = 4 'Counter/Timer
    Const ARB As Short = 5 'Arbitrary Waveform Generator
    Const DMM As Short = 6 'Digital Multi-Meter
    Const OSCOPE As Short = 7 'Digitizing Oscilloscope
    Const SWITCH1 As Short = 8 : Const LFSWITCH1 As Short = 1 : Const SWITCH_CONTROLLER As Short = 0
    Const SWITCH2 As Short = 9 : Const LFSWITCH2 As Short = 2
    Const SWITCH3 As Short = 10 : Const PWRSWITCH As Short = 3
    Const SWITCH4 As Short = 11 : Const MFSWITCH As Short = 4
    Const SWITCH5 As Short = 12 : Const RFSWITCH As Short = 5
    Const SR As Short = 13 'Synchro/Resolver
    Const SERIAL As Short = 14 'Programmable Serial
    '---- This is for the Communications Bus RS232 and RS422-----
    Const Com1 As Short = 15
    Const Com2 As Short = 16
    '------------------------------------------------------------
    Const GBIT As Short = 17 'Gigabit Ethernet
    Const cddi As Short = 18 'Fiber Distributed Data Interface
    Const mic As Short = 19 'Metcam Interface Chip Bus
    Const can As Short = 20 'Controller Area Network Bus
    Const TCIM As Short = 21 'Tactical Communications Interface Modem
    '---- This section is RF-Option instruments: (V)2
    Public Const RFDC As Short = 22
    Const RFSTIM As Short = 23 'RF Stimulus
    Public Const RFLO As Short = 24 'this is the local occilator inikey$(RFLO) = "RFLO"
    Public Const RFDIG As Short = 25

    Public Const RFPM As Short = 28
    Public Const DIG_REF As Short = 29

    '---- This section is Electro-Optical Option instruments: (V)3
    Const EO_VC As Short = 26 'EO Video Capture
    Public Const EO_MOD As Short = 27 'Any EO Module, identified at run-time

    Public Const EO_IR As Short = 30
    Public Const EO_VIS As Short = 31
    Public Const EO_LASER As Short = 32

    'Const LAST_CAL_INST = DIG_REF 3/13/2020
    Const LAST_CAL_INST = TCIM
    Const RF_LAST = RFDIG
    Const RF_FIRST = RFDC

    Public sRFMSErr As String

    Const EO_FIRST = EO_VC
    Const EO_LAST = EO_VC
    'Const HM_FIRST = SR
    'Const HM_LAST = EO_LAST

    Const LAST_INSTRUMENT = EO_VC 'UNTIL EO is done
    'Const LAST_INSTRUMENT = HM_LAST        'Used for array-size declarations
    Const HP8481A = LAST_INSTRUMENT 'Used for unique power sensor information
    Const HP8481D As Short = LAST_INSTRUMENT + 1 'Used for unique power sensor information

    Dim iEO_Module As Short 'Tracks which EO module is identified at run-time
    'Const EO_IR = 1             'Infrared Source Identifier
    'Const EO_VIS = 2            'Visible Source Identifier
    'Const EO_LASER = 3          'ALTA (Active Laser Test Asset) Identifier
    Const EO_MS As Short = 4 'Modulated Source Identifier

    Dim instrumentHandle(EO_LAST + 9) As Integer

    Dim CardStatus(EO_LAST + 9) As Short
    Dim IDNResponse(EO_LAST + 9) As String
    Dim InstrumentSpec(EO_LAST + 9) As String
    Public InstrumentDescription(EO_LAST + 9) As String

    Public InstrumentInitialized(EO_LAST + 9) As Short 'Checked by TestInstrument
    Dim sTestMneu(EO_LAST + 9) As String 'Instrument ID Code for the FHDB
    Dim sComment As String 'Holds String to describe Failure
    Dim IniKey(EO_LAST + 9) As String
    Public VIPERTName(EO_LAST + 9) As String

    Public LiveMode(30) As Short
    Public ResourceName(30) As String
    Public PsResourceName(10) As String
    'Public gRFMa As New RFMa_if ' dimming a RFMS RFma object
    Public tets_RF_Installed = False

    Structure RFMS_status
        Dim DCStatus As Integer ' 0 if status is good, errorCode otherwize

        Dim DigStatus As Integer ' 0 if status is good, errorCode otherwize
        Dim LOStatus As Integer ' 0 if status is good, errorCode otherwize

        Dim CalModStatus As Integer ' 0 if status is good, errorCode otherwize
        Dim ErrorDesc As String ' if device failed init, then the description is here
        Dim RFMSInit As Boolean ' true if the RFMS initialized without errors
    End Structure
    Dim RFMSStatus As RFMS_status

    Public SupplyHandle(10) As Integer

    'These xxOptionInstalled flags are set to true if C-Test determines that that option
    'is installed. Different rules for detection are used among the options due to
    'the way different instruments are detected. Once an option is 'detected', then all
    'instruments in that option are enabled for testing in C-Test.
    'We use this option-detection scheme rather than a variant-detection scheme because
    'there are indications that some VIPERT variants may be combined in the future.
    Dim bRfOptionInstalled As Boolean 'Flag denotes if any RF instruments are found
    Dim bEoOptionInstalled As Boolean 'Flag denotes if the frame grabber instrument is found
    Dim bHmOptionInstalled As Boolean 'Flag denotes if any Heavy Metal instruments are found


    Dim CTestComplete As Short

    Dim FileLenSerial As Short
    Dim IDList(5) As String
    Dim IDData() As String

    Dim SwitchCardOK(5) As Boolean
    Dim ProgramPath As String
    Dim SessionHandle As Integer

    Dim DetailStatus As Short
    Dim EoVccInitialized As Boolean

    Dim FileLen1553(3) As Short
    Dim GoodLine1553(3, 18) As String
    Dim SupplyDate(10) As Integer
    Dim icpmod As Integer 'Pointer to IC-PCI Video Capture Card configuration record
    Dim Number_errors As Short

    Public ReadBuffer As New StringBuilder(Space(256), 256)

    Public retCount As Integer 'return count for cicl communications


    Const lpBufLength = 256
    Dim lpBuf As New StringBuilder(lpBufLength) 'Fixed-length string buffer required by some kernel calls
    'lp stands for long pointer
    Dim sMsg As String 'General purpose Message string
    Dim EchoText As String
    Dim SystErr As Integer = 0
    Dim ReturnOk As Integer
    Dim SystemDir As String
    Public PathVIPERTIni As String
    Dim PathSysLogExe As String
    'Dim Q As String 'Holds quote char Chr(34)
    Dim CurPos As Integer
    Dim sDownCnvtrType As Object
    'System Configuration Type Constants
    Public Const UNKNOWN As Short = 0
    Public Const TETS As Short = 1
    Public Const VIPERT_RF As Short = 2
    Public Const VIPERT_EO As Short = 3
    Public Const MNG As Short = 4

    Public SystemStatus As Short

    Dim nStoreColor As Color
    Public nSystErr As Integer

    'DIM's for CBTS
    Dim viStatus As Integer
    Dim defaultRM As Integer
    Public bCDDI As Short
    Public bCAN As Short
    Public bMIC As Short
    Public bTCIM As Short
    Public bCBTS As Short

    'Launch-Executables Declares

    Structure STARTUPINFO
        Dim cb As Integer
        Dim lpReserved As String
        Dim lpDeskTop As String
        Dim lpTitle As String
        Dim dwX As Integer
        Dim dwY As Integer
        Dim dwXSize As Integer
        Dim dwYSize As Integer
        Dim dwXCountChars As Integer
        Dim dwYCountChars As Integer
        Dim dwFillAttribute As Integer
        Dim dwFlags As Integer
        Dim wShowWindow As Short
        Dim cbReserved2 As Short
        Dim lpReserved2 As Integer
        Dim hStdInput As Integer
        Dim hStdOutput As Integer
        Dim hStdError As Integer
    End Structure

    Structure PROCESS_INFORMATION
        Dim hProcess As Integer
        Dim hThread As Integer
        Dim dwProcessID As Integer
        Dim dwThreadID As Integer
    End Structure

    Private Declare Function CreateProcessA Lib "kernel32" (ByVal lpApplicationName As String, ByVal lpCommandLine As String, ByVal lpProcessAttributes As Integer, ByVal lpThreadAttributes As Integer, ByVal bInheritHandles As Integer, ByVal dwCreationFlags As Integer, ByVal lpEnvironment As Integer, ByVal lpCurrentDirectory As String, ByRef lpStartupInfo As STARTUPINFO, ByRef lpProcessInformation As PROCESS_INFORMATION) As Integer
    Private Declare Function WaitForSingleObject Lib "kernel32" (ByVal hHandle As Integer, ByVal dwMilliseconds As Integer) As Integer
    Private Declare Function CloseHandle Lib "kernel32" (ByVal hObject As Integer) As Integer
    Private Declare Function GetExitCodeProcess Lib "kernel32" (ByVal hProcess As Integer, ByRef lpExitCode As Integer) As Integer
    Private Const NORMAL_PRIORITY_CLASS As Short = &H20
    Private Const STARTF_USESHOWWINDOW As Short = &H1
    Private Const SW_HIDE As Short = 0
    Private Const INFINITE As Short = -1

    'Define Data variables to pass to the FHDB

    Dim sStartTime As String 'System Startup Date/Time field value (Char Len 18)
    Dim sUUTSerialNo As String 'VIPERT Serial Number
    Dim nTestStatus As Integer 'Pass/Fail Status field value
    Dim bInitFail As Boolean 'Flag to indicate Initialization Failed
    Public bRetryInit As Boolean 'Flag to indicate an Initialization Retry

    'Used to Reference the VB DLL (FHDBCDriver.DLL) as an Object
    'NOTE: It must be Referenced in the Project References section
    Dim objData As New FHDB.DLLClass

    'PowerSupply names for CICL
    'Global instrumentHandle&(1 To 10)
    Public SignalResourceNameArray(10) As String

    'Heavy Metal MIC Declares
    Private Declare Function CreateFile Lib "kernel32.dll" Alias "CreateFileA" (ByVal lpFileName As String, ByVal dwDesiredAccess As Integer, ByVal dwShareMode As Integer, ByRef lpSecurityAttributes As System.Delegate, ByVal dwCreationDisposition As Integer, ByVal dwFlagsAndAttributes As Integer, ByVal hTemplateFile As Integer) As Integer
    Private Declare Function DeviceIoControl Lib "kernel32.dll" (ByVal hDevice As Integer, ByVal dwIoControlCode As Integer, ByRef lpInBuffer As System.Delegate, ByVal nInBufferSize As Integer, ByRef lpOutBuffer As System.Delegate, ByVal nOutBufferSize As Integer, ByRef lpBytesReturned As Integer, ByRef lpOverlapped As System.Delegate) As Integer
    Private Const GENERIC_READ As Integer = &H80000000
    Private Const GENERIC_WRITE As Integer = &H40000000
    Private Const FILE_SHARE_READ As Short = &H1
    Private Const FILE_SHARE_WRITE As Short = &H2
    Private Const CREATE_ALWAYS As Short = 2
    Private Const CREATE_NEW As Short = 1
    Private Const OPEN_ALWAYS As Short = 4
    Private Const OPEN_EXISTING As Short = 3
    Private Const TRUNCATE_EXISTING As Short = 5
    Private Const FILE_ATTRIBUTE_ARCHIVE As Short = &H20
    Private Const FILE_ATTRIBUTE_HIDDEN As Short = &H2
    Private Const FILE_ATTRIBUTE_NORMAL As Short = &H80
    Private Const FILE_ATTRIBUTE_READONLY As Short = &H1
    Private Const FILE_ATTRIBUTE_SYSTEM As Short = &H4
    Private Const FILE_FLAG_DELETE_ON_CLOSE As Integer = &H4000000
    Private Const FILE_FLAG_NO_BUFFERING As Integer = &H20000000
    Private Const FILE_FLAG_OVERLAPPED As Integer = &H40000000
    Private Const FILE_FLAG_POSIX_SEMANTICS As Integer = &H1000000
    Private Const FILE_FLAG_RANDOM_ACCESS As Integer = &H10000000
    Private Const FILE_FLAG_SEQUENTIAL_SCAN As Integer = &H8000000
    Private Const FILE_FLAG_WRITE_THROUGH As Integer = &H80000000
    Private Const MBDS_IOC_IF_OPEN As Integer = &HAD9C2454
    Private Const MBDS_IOC_INIT As Integer = &HAD9C2420
    Private Const MBDS_IOC_SELF As Integer = &HAD9C2404
    Private Const INVALID_HANDLE_VALUE As Integer = -1

    'MIC Variables
    Dim nSendBuf(3) As Integer
    Dim nSendBufSize As Integer
    Dim nRcvBuf(3) As Integer
    Dim nRcvBufSize As Integer

    'Flags for MIC selftest
    Const TEST_FAILED As Short = &H0
    Const AT_INTERFACE_PASSED As Short = &H1
    Const ADDRESS_DECODE_PASSED As Short = &H2
    Const CLOCK_GENERATION_PASSED As Short = &H4
    Const RESET_PASSED As Short = &H8
    Const MIC_PASSED As Short = &H10
    Const INTERRUPT_PASSED As Short = &H20
    Const SERIAL_BUS_CONTROL_PASSED As Short = &H40
    Const TIME_STAMP_PASSED As Short = &H80
    Const LOGIC_TESTS_PASSED As Short = &HFF

    'Variables and constants used for Com ports, Gigabit and serial bus
    Public Const MAX_XML_SIZE As Short = 5000
    Dim Wparity As String 'parity = "ODD", "EVEN", "MARK", "NONE"
    Dim Wbitrate As String 'bitrate = "9600Hz"
    Dim Wstopbits As String 'stopbits = "2bits"
    Dim Wwordlength As String 'wordlength = "8"
    Dim Wmaxt As String 'maxt = "1s"
    Dim Wdelay As String
    Dim Wterm As String
    Dim Wspec As String
    Dim Wdata As String

    Dim Wmask As String
    Dim WlocalIP As String
    Dim Wgateway As String
    Dim WremoteIp As String
    Dim Wremport As String

    Dim Rparity As String 'parity = "ODD", "EVEN", "MARK", "NONE"
    Dim Rbitrate As String 'bitrate = "9600Hz"
    Dim Rstopbits As String 'stopbits = "2bits"
    Dim Rwordlength As String 'wordlength = "8"
    Dim Rmaxt As String 'maxt = "1s"
    Dim Rdelay As String
    Dim Rterm As String
    Dim Rspec As String
    Dim Rdata As String

    Dim Rmask As String
    Dim RlocalIP As String
    Dim Rgateway As String
    Dim RremoteIp As String
    Dim Rremport As String

    Dim rName(7) As String
    Dim pName(7) As String

    Const xRS422 As Short = 1
    Const xRS232 As Short = 2
    Const xSerRS232 As Short = 3
    Const xSerRS422 As Short = 4
    Const xSerRS485 As Short = 5
    Const xGBitEth1 As Short = 6
    Const xGBitEth2 As Short = 7



    Sub CenterForm(ByVal Form As frmCTest)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM SELF TEST                   *
        '* Written By     : Michael McCabe                          *
        '*    DESCRIPTION:                                          *
        '*     This Module Centers One Form With Respect To The     *
        '*     User's Screen.                                       *
        '*    EXAMPLE:                                              *
        '*     CenterForm frmMain                                   *
        '************************************************************
        If PrimaryScreen.Bounds.Height > 7200 Then
            Form.Top = PrimaryScreen.Bounds.Height / 2 - Form.Height / 2
        Else
            Form.Top = 0
        End If
        Form.Left = PrimaryScreen.Bounds.Width / 2 - Form.Width / 2
    End Sub

    Function GetDateSerial(ByVal InstIdx As Short) As Integer
        'DESCRIPTION
        '   Reads the calibration date from an instrument.
        '   Adds one year to make it a 'calibration due' date.
        'PARAMETERS
        '   InstIdx%:    Index to the instrument from which to read a date.
        'RETURN VALUE
        '   Returns the date in 'DateSerial' format (Long).
        '   Returns 0 if date can't be read for some reason.
        '   PPU returns the lowest of 10 Cal Due Dates.

        Dim IHandle As Integer, LowDate As Integer, bytesRead As Integer, X As Integer, InstrDate As Integer
        Dim msg As String = ""
        Dim Supply As Short, address As Short



        Dim B1 As String = Space(1), B2 As String = Space(1), B3 As String = Space(1)

        Dim SupplyDate(10) As Integer

        If Not InstrumentInitialized(InstIdx) Then
            GetDateSerial = 0
            Exit Function
        Else
            IHandle = instrumentHandle(InstIdx)
        End If

        Select Case InstIdx
            Case DMM
                'Factory:   CALibration:STRing contents is undocumented
                '           CAL:STR format is a 40 character quoted string
                'VIPERT Cal:  Date of calibration in mm/dd/yyyy format

                WriteMsg(DMM, "CAL:STR?")
                SystErr = ReadMsg(DMM, msg)
                msg = sStripQuotes(msg)
                Dim newDate As DateTime = DateTime.ParseExact(msg, "MM/dd/yyyy", Nothing)
                'add one year for due date
                newDate = newDate.AddYears(1)
                If IsDate(newDate) = True Then
                    GetDateSerial = newDate.ToOADate
                Else
                    GetDateSerial = 0
                End If

            Case ARB

                WriteMsg(ARB, "*PUD?")
                SystErr = ReadMsg(ARB, msg)
                Dim dateIndex As Integer = msg.IndexOf("/")
                Dim dateString As String = Mid(msg, dateIndex - 1, 10)
                Dim newDate As DateTime = DateTime.ParseExact(dateString, "MM/dd/yyyy", Nothing)
                newDate = newDate.AddYears(1)
                'msg = sIEEEDefinite("From", msg)
                If IsDate(newDate) Then
                    GetDateSerial = newDate.ToOADate
                Else
                    GetDateSerial = 0
                End If

            Case PPU
                'PPU returns the lowest of 10 Cal Due Dates
                '
                'Factory:   Date stored in EEPROM memory location 0FAAh to 0FADh
                '           Format is 4 bytes:
                '               Byte 1 = Century
                '               Byte 2 = Year within century
                '               Byte 3 = Month
                '               Byte 4 = Day of month
                '           Example: For 11/24/97 the four byte values are 19,97,11,24.
                'VIPERT Cal:  Same format as factory

                delay(2) 'Allow prior resets to finish
                LowDate = 999999
                For Supply = 1 To 10
                    frmCTest.sbrUserInformation.Text = "Retrieving Calibration Due Dates for " & InstrumentDescription(InstIdx) & "(" & Supply & ")"

                    msg = ""
                    For address = &HFAA To &HFAD 'From DCPS EEPROM Memory Map
                        Application.DoEvents()

                        'Send EEProm read address
                        B1 = (Convert.ToString(Chr(Supply)))
                        B2 = (Convert.ToString(Chr(&H60 + (address \ &H100))))
                        B3 = (Convert.ToString(Chr(address Mod &H100)))
                        SystErr = atxmlDF_viWrite(SignalResourceNameArray(Supply), 0, B1 & B2 & B3, CLng(Len(B1 & B2 & B3)), bytesRead)
                        Application.DoEvents()

                        delay(0.5)
                        'Send Status Query
                        SystErr = atxmlDF_viWrite(SignalResourceNameArray(Supply), 0, Convert.ToString(Chr(Supply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(0)), CLng(Len(Convert.ToString(Chr(Supply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(0)))), bytesRead)
                        Application.DoEvents()

                        delay(0.5)

                        'Read Status
                        Dim buffer As String = Space(255)
                        SystErr = atxmlDF_viRead(SignalResourceNameArray(Supply), 0, buffer, 255, bytesRead)
                        delay(0.5)

                        'Accumulate date codes
                        msg &= Mid(buffer, 5, 1)
                    Next address

                    'Range-check-high for valid date codes to prevent program errors
                    If Asc(Strings.Left(msg, 1)) > 98 Then 'Test Century range
                        SupplyDate(Supply) = 0
                    ElseIf Asc(Mid(msg, 2, 1)) > 99 Then                        'Test Year range
                        SupplyDate(Supply) = 0
                    ElseIf Asc(Mid(msg, 3, 1)) > 12 Then                        'Test Month range
                        SupplyDate(Supply) = 0
                    ElseIf Asc(Mid(msg, 4, 1)) > 31 Then                        'Test Day range
                        SupplyDate(Supply) = 0
                    Else
                        'It's OK now, so add 1 year to make it a 'Cal Due' date
                        SupplyDate(Supply) = DateSerial(1 + (Asc(Strings.Left(msg, 1)) * 100) + Asc(Mid(msg, 2, 1)), Asc(Mid(msg, 3, 1)), Asc(Mid(msg, 4, 1))).ToOADate
                    End If

                    'Now, range-check-low the date
                    If SupplyDate(Supply) > 35400 Then 'If later than 1/1/97, else its garbage data
                        If SupplyDate(Supply) < LowDate Then LowDate = SupplyDate(Supply)
                    Else
                        SupplyDate(Supply) = 0
                    End If

                    ReturnOk = WritePrivateProfileString("Calibration", IniKey(InstIdx) & VB6.Format(Supply, 0), SupplyDate(Supply), PathVIPERTIni)

                    PrintCalDate(SupplyDate(Supply), "Power Supply" & Str(Supply), -1)
                Next Supply

                If LowDate <> 999999 Then
                    GetDateSerial = LowDate
                Else
                    GetDateSerial = 0
                End If

        End Select
    End Function


    Function sIEEEDefinite(ByVal sDirection As String, ByVal sMsg As String) As String
        sIEEEDefinite = ""

        'Returns IEEE 488.2 Definite length block formatted string
        'Example: "#15HELLO"

        Dim iNumDigits As Short

        Dim iNumChars As Short
        Dim i As Short

        On Error Resume Next
        If UCase(sDirection) = "FROM" Then
            iNumDigits = Mid(sMsg, 2, 1)
            iNumChars = Mid(sMsg, 3, iNumDigits)
            sIEEEDefinite = Mid(sMsg, 3 + iNumDigits, iNumChars)
        Else
            iNumChars = Len(sMsg)
            iNumDigits = Len(CStr(iNumChars))
            sIEEEDefinite = "#" & CStr(iNumDigits) & CStr(iNumChars) & sMsg
        End If

    End Function


    Function sStripQuotes(ByRef sMsg As String) As String
        sStripQuotes = ""
        If Strings.Left(sMsg, 1) = Convert.ToString(Chr(34)) Then
            sMsg = Mid(sMsg, 2)
        End If
        If Strings.Right(sMsg, 1) = Convert.ToString(Chr(34)) Then
            sMsg = Strings.Left(sMsg, Len(sMsg) - 1)
        End If
        sStripQuotes = sMsg
    End Function

    Function GetVisaErrorMessage(ByVal Handle As Integer, ByVal StatusCode As Integer) As String
        GetVisaErrorMessage = ""

        SystErr = atxmlDF_viStatusDesc("", Handle, StatusCode, lpBuf.ToString)
        GetVisaErrorMessage = Q & StripNullCharacters(lpBuf.ToString()) & Q

    End Function



    Function InitDigital() As Short
        'DESCRIPTION:
        '   This Routine checks to see if the Digital Test System (DTS) is available and
        '   configured properly.
        'RETURNS:
        '   True if the DTS initializes and is properly cofigured
        'MODIFIES:
        '   instrumentHandle&(DIGITAL)

        'Check if DTS is Astronics T940 or Teradyne M910
        Dim dtsModel As String = Space(256)
        dtsModel = GatherIniFileInformation("DTS", "DESCRIPTION", "")

        If (dtsModel.Contains("T940") = True) Then          'Astronics T940 DTS
            Dim InitStatus As Integer, viSession As Integer, cardCount As Integer, channelCount As Integer, cardIdx As Integer, cardId As Integer
            Dim boardCount As Integer, chanCount As Integer, chassis As Integer, slot As Integer
            Dim Items As Short
            Dim desc As String

            Dim cardName As String = Space(256)

            Dim addlInfo As String = Space(256)
            Dim iStepCount As Short 'Increments the Sub Test ID in the Faut ID

            InitDigital = True
            iStepCount = 5 'Initialize Step Counter

            'DTS-00-N0I+1
            'Init Modules
            For I As Integer = 0 To NumDTIModules - 1
                InitStatus = tat964_init(dtiResources(I), VI_TRUE, VI_FALSE, dtiHandles(I))
                Echo("DTS-00-N0" & I + 1)
                If (InitStatus <> VI_SUCCESS) Or (dtiHandles(I) = VI_NULL) Then
                    Echo("Error Initializing DTS Module in Slot " & I + 1)
                    Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                    RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 1, sOpComments:="DTS Module Slot " & I + 1 & " " & "initialization FAILED")
                    InitDigital = False
                    Exit Function
                Else
                    Echo(FormatResults(True, "    " & "DTS Module Slot" & I + 1 & " initialization "))
                End If
            Next I

            'DTS-00-N0I+3
            'Check Module Types
            Dim boardType As String = Space(256)
            Dim serialNumber As String = Space(256)
            Dim assemblyRevision As String = Space(256)
            Dim logicRevision As String = Space(256)
            Dim calibrationRevision As String = Space(256)
            Dim calibrationDate As String = Space(256)
            For I As Integer = 0 To NumDTIModules - 1
                Echo("DTS-00-N0" & I + 3)
                'DRA
                InitStatus = tat964_drQuery(dtiHandles(I), 0, boardType, serialNumber, assemblyRevision, logicRevision, calibrationRevision, calibrationDate)
                If I < NumDTIModules - 1 Then 'Should be a T964DR3E for slots 1-3
                    If Not boardType.Contains("T964DR3E") Then
                        Echo("Incorrect DTS Module In Slot " & I + 1)
                        Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                        RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 3, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect Module Type")
                        InitDigital = False
                    Else
                        Echo(FormatResults(True, "    " & "DTS Module Slot" & I + 1 & " DRA Correct Module Type "))
                    End If

                Else
                    If Not boardType.Contains("T964UR14") Then
                        Echo("Incorrect DTS Module In Slot " & I + 1)
                        Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                        RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 3, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect Module Type")
                        InitDigital = False
                    Else
                        Echo(FormatResults(True, "    " & "DTS Module Slot" & I + 1 & " DRA Correct Module Type "))
                    End If
                End If

                'DRB
                If I < NumDTIModules - 1 Then 'Only check DRB on the DR3E Modules
                    InitStatus = tat964_drQuery(dtiHandles(I), 1, boardType, serialNumber, assemblyRevision, logicRevision, calibrationRevision, calibrationDate)
                    If Not boardType.Contains("T964DR3E") Then
                        Echo("Incorrect DTS Module In Slot " & I + 1)
                        Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                        RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 3, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect Module Type")
                        InitDigital = False
                    Else
                        Echo(FormatResults(True, "    " & "DTS Module Slot" & I + 1 & " DRB Correct Module Type "))
                    End If
                End If
            Next I

            'DTS-00-N0I+7
            'Check Firmware Revision
            Dim serialNumberShort As Short
            Dim VXILogicRevision As String = Space(256)
            Dim SequencerLogicRevision As String = Space(256)
            Dim sequencerLogicRevisionDouble As Double = 0.0
            For I As Integer = 0 To NumDTIModules - 1
                Echo("DTS-00-N0" & I + 7)
                'DRA
                InitStatus = tat964_dcbQuery(dtiHandles(I), serialNumberShort, assemblyRevision, VXILogicRevision, SequencerLogicRevision)
                SequencerLogicRevision = SequencerLogicRevision.Replace(" ", "")
                Double.TryParse(SequencerLogicRevision, sequencerLogicRevisionDouble)
                If sequencerLogicRevisionDouble < DTIFirmwareRevision Then
                    Echo("Incorrect DTS Firmware In Slot " & I + 1)
                    Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                    RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 7, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect Module Type")
                    InitDigital = False
                Else
                    Echo(FormatResults(True, "    " & "DTS Module Slot" & I + 1 & " Firmware Revision " & sequencerLogicRevisionDouble))
                End If
            Next I

            'DTS-00-N0I+11
            'Check DRS Jumper Configuration
            Dim queryData As Short = -1
            For I As Integer = 0 To NumDTIModules - 1
                Echo("DTS-00-N0" & I + 11)
                'DRA
                InitStatus = tat964_queryModuleData(dtiHandles(I), 2, queryData)
                If I = 0 And queryData <> 2 Then
                    Echo("Incorrect DTS DRS Jumper Configuration In Slot " & I + 1)
                    Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                    RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 11, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect DRS Jumper Configuration")
                    InitDigital = False
                ElseIf I = NumDTIModules - 1 And queryData <> 0 Then
                    Echo("Incorrect DTS DRS Jumper Configuration In Slot " & I + 1)
                    Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                    RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 11, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect DRS Jumper Configuration")
                    InitDigital = False
                ElseIf I < NumDTIModules - 2 And I > 0 And queryData <> 1 Then
                    Echo("Incorrect DTS DRS Jumper Configuration In Slot " & I + 1)
                    Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                    RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N0" & I + 11, sOpComments:="DTS Module Slot " & I + 1 & " " & "Incorrect DRS Jumper Configuration")
                    InitDigital = False
                Else
                    Echo(FormatResults(True, "    " & "DTS Module Slot" & I + 1 & " DRS Jumper Configuration Correct"))
                End If
                queryData = -1
            Next I

        Else 'Teradyne M910 DTS

            Dim InitStatus As Integer, viSession As Integer, cardCount As Integer, channelCount As Integer, cardIdx As Integer, cardId As Integer
            Dim boardCount As Integer, chanCount As Integer, chassis As Integer, slot As Integer
            Dim Items As Short
            Dim desc As String

            Dim cardName As New StringBuilder(Space(33), 33)

            Dim addlInfo As New StringBuilder(Space(2049), 2049)
            Dim iStepCount As Short 'Increments the Sub Test ID in the Faut ID

            InitDigital = True
            iStepCount = 5 'Initialize Step Counter

            InitStatus = terM9_init("TERM9::#0", VI_FALSE, VI_FALSE, viSession) 'Chassis 0
            'DTS-00-N01
            Echo("DTS-00-N01")
            If (InitStatus <> VI_SUCCESS) Or (viSession = VI_NULL) Then
                Echo("Error locating CRB in VXI primary chassis, slot 4.")
                Echo(FormatResults(False, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N01", sOpComments:="Error locating CRB in VXI primary chassis, slot 4, initialization FAILED")
                InitDigital = False
                Exit Function
            Else
                Echo(FormatResults(True, "    " & InstrumentDescription(DIGITAL) & " initialization "))
                instrumentHandle(DIGITAL) = viSession
            End If

            InitStatus = terM9_getSystemInformation(viSession, lpBuf, cardCount, channelCount)
            'Retrieve M9 System Information
            'DTS-00-N02
            Echo("DTS-00-N02")
            If InitStatus <> VI_SUCCESS Then
                Echo("Error initializing CRB in VXI primary chassis, slot 4.")
                Echo("    Error retrieving M9 System Information.")
                RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N02", sOpComments:="Error initializing CRB in VXI primary chassis, slot 4." & vbCrLf & "Error retrieving M9 System Information.")
                InitDigital = False
                Exit Function
            Else
                desc = lpBuf.ToString()
                Echo("TERM9::0,4 => System Query => " & desc & "," & Str(channelCount) & " channels")
                Echo("    M9 System Information retrieved successfully.")
            End If
            'Instrument Identification
            'DTS-00-N03
            Echo("DTS-00-N03")
            If InStr(desc, "M9 Digital Test Instrument") = 0 Then
                Echo("    M9 Digital Test Instrument could not be Initialized.")
                RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N03", sOpComments:="M9 Digital Test Instrument counld not be Initialized, identification FAILED")
                InitDigital = False
                Exit Function
            Else
                Echo(FormatResults(True, "    " & desc & " initialization "))
            End If
            'Card Count
            'DTS-00-N04
            Echo("DTS-00-N04")
            If cardCount <> 5 Then
                Echo("    Incorrect number of M910 cards: " & cardCount & " found.")
                RegisterFailure(vInstrumentIndex:=DIGITAL, sFailStep:="DTS-00-N04", sOpComments:="Incorrect number of M910 cards: " & cardCount & " found.")
                InitDigital = False
                Exit Function
            Else
                Echo("    Correct number of M910 cards: " & cardCount & " found.")
            End If

            '*** WARNING: This information was obtained empirically.
            '***          Teradyne has not documented any values for these data.
            Dim ExpectDesc(4) As String : Dim ExpectSlot(4) As Short
            ExpectDesc(0) = "Global Channel Card" : ExpectSlot(0) = 4
            ExpectDesc(1) = "Central Resource Board" : ExpectSlot(1) = 4
            'Full string:    "Central Resource Board 2"
            ExpectDesc(2) = "Channel Card" : ExpectSlot(2) = 3
            ExpectDesc(3) = "Channel Card" : ExpectSlot(3) = 2
            ExpectDesc(4) = "Channel Card" : ExpectSlot(4) = 1
            'Full string:    "CCC2 Channel Card"

            For cardIdx = 0 To cardCount - 1
                'DTS-00-N05 ~ DTS-00-N09
                Echo("DTS-00-N" & VB6.Format(iStepCount, "00"))
                cardName = New StringBuilder(256)
                addlInfo = New StringBuilder(256)
                SystErr = terM9_getCardInformation(viSession, cardIdx, cardId, cardName, boardCount, chanCount, chassis, slot, addlInfo)
                desc = cardName.ToString()
                If (InStr(desc, ExpectDesc(cardIdx)) > 0) And (chassis = 1) And (slot = ExpectSlot(cardIdx)) Then
                    Echo("    Card Index(" & cardIdx & ") Name => " & desc)
                    Echo(FormatResults(True, "    " & desc & " identification "))
                Else
                    RegisterFailure(DIGITAL, "DTS-00-N" & VB6.Format(iStepCount, "00"), "M910 card in wrong VXI slot or VXI chassis." & vbCrLf & "Index:" & Str(cardIdx) & "; ID:" & Str(cardId) & "; Name: " & desc & "; Channels:" & Str(chanCount) & "; Chassis,Slot:" & Str(chassis) & "," & slot)
                    InitDigital = False
                    Echo(FormatResults(False, "    M910 card in wrong VXI slot or VXI chassis, identification "))
                    Echo("Index:" & Str(cardIdx) & "; ID:" & Str(cardId) & "; Name: " & desc & "; Channels:" & Str(chanCount) & "; Chassis,Slot:" & Str(chassis) & "," & slot)
                End If
                iStepCount += 1 'Increment Step Counter
            Next cardIdx

            SystErr = terM9_close(instrumentHandle(DIGITAL))

        End If

    End Function


    Function InitPowerSupplies() As Short
        'DESCRIPTION:
        '   This routine Initializes the power supplies and places their handle in SupplyHandle&

        Dim Supply As Short, mTry As Short
        Dim SystErr As Integer, bytesRead As Integer
        Dim lBytesRead As Integer

        SignalResourceNameArray(1) = "DCPS_1"
        SignalResourceNameArray(2) = "DCPS_2"
        SignalResourceNameArray(3) = "DCPS_3"
        SignalResourceNameArray(4) = "DCPS_4"
        SignalResourceNameArray(5) = "DCPS_5"
        SignalResourceNameArray(6) = "DCPS_6"
        SignalResourceNameArray(7) = "DCPS_7"
        SignalResourceNameArray(8) = "DCPS_8"
        SignalResourceNameArray(9) = "DCPS_9"
        SignalResourceNameArray(10) = "DCPS_10"

        InitPowerSupplies = PASSED
        For Supply = 10 To 1 Step -1
            Application.DoEvents()

            If Supply <> 10 Then Echo("")
            'P01-00-N01 ~ P10-00-N01
            Echo("P" & VB6.Format(Supply, "00") & "-00-N01")
            If SystErr <> VI_SUCCESS Then
                Echo("Could not get instrument handle for GPIB0::5::" & CStr(Supply))
                Echo(InstrumentDescription(PPU) & " Power Supply " & Supply & " initialization FAILED")
                Echo("VISA Error Code: " & GetVisaErrorMessage(SupplyHandle(Supply), SystErr))
                RegisterFailure(PPU, "P" & VB6.Format(Supply, "00") & "-00-N01", sOpComments:="    Initialization Failure for Supply " & CStr(Supply) & ". VISA Error Code: " & GetVisaErrorMessage(SupplyHandle(Supply), SystErr))
                InitPowerSupplies = FAILED
                SupplyHandle(Supply) = 0
            Else
                Echo(InstrumentDescription(PPU) & " Power Supply " & Supply & " initialization PASSED")
                'P01-00-N02 ~ P10-00-N02

                Echo("P" & VB6.Format(Supply, "00") & "-00-N02")
                SystErr = atxmlDF_viSetAttribute(SignalResourceNameArray(Supply), SupplyHandle(Supply), VI_ATTR_TMO_VALUE, 3000)

                'Send Status Query
                delay(0.5)
                SystErr = atxmlDF_viWrite(SignalResourceNameArray(Supply), 0, Convert.ToString(Chr(Supply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(0)), CLng(Len(Convert.ToString(Chr(Supply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(0)))), lBytesRead)
                'Read Status
                lpBuf = New StringBuilder(Convert.ToString(Chr(0)))
                delay(0.5)
                Dim buffer As String = Space(256)
                SystErr = atxmlDF_viRead(SignalResourceNameArray(Supply), 0, buffer, 255, lBytesRead)

                'If MODE byte is zero, supply module is absent or dead
                If Asc(Strings.Left(buffer, 1)) <> 0 Then
                    Echo(InstrumentDescription(PPU) & " Power Supply " & Supply & " identification PASSED")
                    For mTry = 1 To 5 'Try multiple times to talk to a supply
                        'Send Reset command which also runs BIT
                        SystErr = atxmlDF_viWrite(SignalResourceNameArray(Supply), 0, Convert.ToString(Chr(&H10 + Supply)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)), CLng(Len(Convert.ToString(Chr(&H10 + Supply)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0)))), lBytesRead)
                        If SystErr = VI_SUCCESS Then Exit For
                        delay(0.2)
                    Next
                Else
                    Echo(InstrumentDescription(PPU) & " Power Supply " & Supply & " identification FAILED")
                    RegisterFailure(PPU, ("P" & VB6.Format(Supply, "00") & "-00-N02"), sOpComments:="    Identification Failure for Supply " & CStr(Supply) & ".")
                    InitPowerSupplies = FAILED
                End If
            End If
        Next Supply

        If InitPowerSupplies = FAILED Then
            ReportFailure(PPU)
        End If

    End Function


    Sub TestInstrument(ByVal InstrumentToTest As Short, ByVal InitialSelfTest As Short)
        'DESCRIPTION:
        '   This routine performs the actual self test on an individual module
        'PARAMETERS:
        '   InstrumentToTest% = The index associated with the instrument desired to be tested
        '   InitialSelfTest% = Determines whether the Main sub or an icon click calls the routine
        Dim count As Short
        Dim SelfTestResponse As String = ""

        Try ' On Error GoTo ErrorHandler

            'Bail out now by option to prevent testing for it in the main loop.
            Select Case InstrumentToTest
                Case RF_FIRST To RF_LAST
                    If Not bRfOptionInstalled Then Exit Sub

            End Select

            With frmCTest
                bInitFail = False
                If (Not InitialSelfTest) And (Not CTestComplete) Then
                    Exit Sub
                End If
                nStoreColor = .InstrumentLabel(InstrumentToTest).ForeColor
                .InstrumentLabel(InstrumentToTest).ForeColor = Color.Blue

                If Not InitialSelfTest Then
                    .imgPassFrame(InstrumentToTest).Image = My.Resources.NOTEST
                    .imgFailFrame(InstrumentToTest).Image = My.Resources.NOTEST
                    Application.DoEvents()
                    Select Case InstrumentToTest
                        Case DMM, COUNTER, ARB, FGEN, RFLO, OSCOPE, SR
                            WriteMsg(InstrumentToTest, "*TST?")
                        Case RFDIG
                            WriteMsg(InstrumentToTest, "CH")
                    End Select
                End If

                If InstrumentInitialized(InstrumentToTest) Then
                    .sbrUserInformation.Text = "Testing " & InstrumentDescription(InstrumentToTest)
                    Echo("")
                    Select Case InstrumentToTest

                        'DMM-01-001, 'C/T-01-001, ARB-01-001, FGN-01-001, DSO-01-001, RST-01-001, RCT-01-001, HSR-01-001
                        Case DMM, COUNTER, ARB, FGEN, OSCOPE, RFSTIM, SR
                            'The RF-Stim must be handled differently because if the downconverter is the
                            'Gigatronics model (which uses the RF-Stim comm interface), the commands are
                            'out of sequence.
                            If ObjectToDouble(InstrumentToTest) = RFSTIM Then
                                WriteMsg(InstrumentToTest, "*TST?")
                            End If
TestTETSMbRf:
                            WaitForResponse(InstrumentToTest)
                            SystErr = ReadMsg(InstrumentToTest, SelfTestResponse)
                            Echo(sTestMneu(InstrumentToTest) & "-01-001")
                            Echo(InstrumentDescription(InstrumentToTest) & " => *TST? => " & SelfTestResponse)
                            If SystErr <> 0 Then
                                Echo(InstrumentSpec(InstrumentToTest) & " failed to respond to self test query.  VISA Error <" & Hex(SystErr) & ">")
                                RegisterFailure(InstrumentToTest, sTestMneu(InstrumentToTest) & "-01-001", InstrumentSpec(InstrumentToTest) & " failed to respond to self test query.  VISA Error <" & Hex(SystErr) & ">")
                                ReportFailure(InstrumentToTest)
                            Else
                                If Val(SelfTestResponse) = 0 Then
                                    ReportPass(InstrumentToTest)
                                    Echo(FormatResults(True, InstrumentDescription(InstrumentToTest) & " Built-In Test"))
                                    'RESET THE INSTRUMENT FOR USE IN OTHER LATER APS  FAT ISSUE
                                    WriteMsg(InstrumentToTest, "*RST")
                                Else
                                    Echo(FormatResults(False, InstrumentDescription(InstrumentToTest) & " Built-In Test"))
                                    RegisterFailure(InstrumentToTest, sTestMneu(InstrumentToTest) & "-01-001", "This instrument FAILED the Built In Test.")
                                    ReportFailure(InstrumentToTest)
                                End If
                            End If

                            'Pxx-01-001
                        Case PPU
                            If TestPowerSupplies() = PASSED Then
                                ReportPass(PPU)
                            Else
                                ReportFailure(PPU)
                            End If

                        Case SWITCH1, SWITCH2, SWITCH3, SWITCH4, SWITCH5
                            TestSwitching(InstrumentToTest)

                        Case DIGITAL
                            If TestDigital() = PASSED Then
                                ReportPass(DIGITAL)
                            Else
                                ReportFailure(DIGITAL)
                            End If

                        Case MIL_STD_1553
                            If Test1553() = PASSED Then
                                ReportPass(MIL_STD_1553)
                            Else
                                Echo(FormatResults(False, "The 1553 Bus Interface", "BUS-01-001"))
                                ReportFailure(MIL_STD_1553)
                            End If

                        Case SERIAL
                            If TestSerial() = PASSED Then
                                ReportPass(SERIAL)
                            Else
                                ReportFailure(SERIAL)
                            End If

                        Case EO_VC
                            If TestEoVcc() = PASSED Then
                                ReportPass(InstrumentToTest)
                                Echo("VCC-01-001")
                                Echo(FormatResults(True, "Video Capture" & " Test"))
                            Else
                                ReportFailure(InstrumentToTest)
                                RegisterFailure(InstrumentToTest, "VCC-01-001", "Instrument is not functioning properly")
                                Echo("VCC-01-001")
                                Echo(FormatResults(False, InstrumentDescription(InstrumentToTest) & " Test"))
                            End If

                            'EOM-01-001

                        Case can
                            'test debug

                            Dim path As ManagementPath = New ManagementPath()
                            path.Server = "."
                            path.NamespacePath = "root\CIMV2"
                            Dim foundtewscard As Boolean = False
                            Dim scope As ManagementScope = New ManagementScope(path)
                            Dim query As ObjectQuery = New ObjectQuery("SELECT * FROM Win32_PnPEntity")
                            Dim searcher As ManagementObjectSearcher = New ManagementObjectSearcher(scope, query)
                            Dim queryCollection As ManagementObjectCollection = searcher.Get()
                            Dim m As ManagementObject
                            Dim devicename As String = ""
                            For Each m In queryCollection
                                Console.WriteLine("Device Name : {0}", m("Name"))
                                devicename = m("Name")
                                If (InStr(devicename, "TEWS TECHNOLOGIES - TDRV010 Extended CAN (TPMC810)")) Then
                                    foundtewscard = True
                                End If
                            Next

                            If foundtewscard = True Then
                                Echo("CAN-01-001")
                                Echo(FormatResults(True, "CAN Controller Test"))
                                ReportPass(InstrumentToTest)
                            Else
                                Echo("CAN-01-001")
                                Echo(FormatResults(False, "CAN Controller Test"))
                                ReportFailure(InstrumentToTest)
                                RegisterFailure(InstrumentToTest, "CAN-01-001", "Instrument FAILED the BIT Test.")
                            End If

                            'ETH-00-001
                        Case GBIT
                            If TestGigabitEthernet() = PASSED Then
                                ReportPass(GBIT)
                                Echo("ETH-00-001")
                                Echo(FormatResults(True, InstrumentDescription(InstrumentToTest) & "Communication"))
                            Else
                                ReportFailure(GBIT)
                                Echo("ETH-00-001")
                                Echo(FormatResults(False, InstrumentDescription(InstrumentToTest) & "Communication"))
                                RegisterFailure(InstrumentToTest, "ETH-00-001", "This instrument FAILED communication with the ethernet bus.")
                            End If

                            'BUS-03-001
                        Case Com1
                            If TestCOM1() = PASSED Then
                                ReportPass(Com1)
                            Else
                                ReportFailure(Com1)
                                'Echo "COM-00-001"
                                'Echo FormatResults(False, InstrumentDescription$(InstrumentToTest%) & "Communication")
                                RegisterFailure(InstrumentToTest, "BUS-03-001", "This instrument FAILED the communications confidence Test.")
                            End If

                            'BUS-04-001
                        Case Com2
                            If TestCom2() = PASSED Then
                                ReportPass(Com2)
                            Else
                                ReportFailure(Com2)
                                'Echo "COM-01-001"
                                'Echo FormatResults(False, InstrumentDescription$(InstrumentToTest%) & "Communication")
                                RegisterFailure(InstrumentToTest, "BUS-04-001", "This instrument FAILED the communications confidence Test.")
                            End If
                           
                    End Select

                ElseIf Not InitialSelfTest Then
                    Echo(InstrumentDescription(InstrumentToTest) & " could not be initialized.")
                    MsgBox("This instrument could not be initialized.")
                    ReportFailure(InstrumentToTest)
                End If
                .InstrumentLabel(InstrumentToTest).ForeColor = nStoreColor

                Exit Sub
            End With

        Catch   ' ErrorHandler:
            If CompareErrNumber("<>", 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description, MsgBoxStyle.Exclamation)
                Err.Clear()
            End If

            ResumeNext()

        End Try
    End Sub
    Sub PrintCalDate(ByVal DateCode As Integer, ByRef InstName As String, ByVal InstIdx As Short)
        Dim ActionDue As String

        If Convert.ToString(InstName) = "" Then
            InstName = VIPERTName(InstIdx)
        End If

        If (ObjectToDouble(InstIdx) = SWITCH5) Then
            ActionDue = "Path Loss Calculation due: "
        Else
            ActionDue = "Calibration due: "
        End If

        If ObjectToDouble(DateCode) > 0 Then
            Echo(ActionDue & VB6.Format(Convert.ToString(DateCode), "dd-mmm-yyyy") & " for " & InstName)
        ElseIf ObjectToDouble(DateCode) = 0 Then
            Echo(ActionDue & "UNKNOWN for " & InstName)
        End If
    End Sub


    Sub ProcessCalDates()
        'DESCRIPTION:
        '   1. Reads Calibration dates from instruments that can store them, adds 1 year
        '       and writes them to the VIPERT.INI keys as "Calibration Due" dates.
        '   2. For other instruments, simply reads the dates from the keys.
        '   3. Finds the earliest date and writes it to the SYSTEM_EFFECTIVE key for SysMon.
        'Revision Log:
        ' 2/15/99 V1.7  Added code to get the calibration date of the HP8481A and the HP8481D
        '               From the VIPERT.INI file.  Per ECO-3047-,TDR98330 GJohnson

        Dim InstIdx As Short

        Dim X As Integer, LowDate As Integer, InstrDate As Integer
        'Added to allow only ProcessCalDates function to cycle through during process of cal dates
        'Per ECO-3047-, TDR98330 GJohnson 2/15/99

        LowDate = 999999

        'Get the System Cal Date to include it in the 'Earliest Date' test
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("Calibration", "SYSTEM", "0", lpBuf, lpBufLength, PathVIPERTIni)
        InstrDate = Val(StripNullCharacters(lpBuf.ToString()))
        PrintCalDate(InstrDate, "System", -1)

        'If a System date found, then record it in LowDate&
        If InstrDate > 0 Then LowDate = InstrDate

        For InstIdx = 0 To (LAST_CAL_INST)
            If InstIdx = TCIM Or InstIdx = cddi Or InstIdx = mic Or InstIdx = SWITCH5 Then
                Continue For
            End If
            frmCTest.sbrUserInformation.Text = "Retrieving Calibration Due Dates for " & InstrumentDescription(InstIdx) & " . . ."
            If InstIdx <= ObjectToDouble(LAST_INSTRUMENT) Then
                nStoreColor = frmCTest.InstrumentLabel(InstIdx).ForeColor
                frmCTest.InstrumentLabel(InstIdx).ForeColor = Color.Blue
            End If
            Select Case InstIdx
                'These have no NV-RAM storage capability, so get the VIPERT.INI key entries
                Case DIGITAL, COUNTER, FGEN, SR, OSCOPE
                    'lpBuf = New StringBuilder("")
                    X = GetPrivateProfileString("Calibration", IniKey(InstIdx), "0", lpBuf, lpBufLength, PathVIPERTIni)
                    InstrDate = Val(StripNullCharacters(lpBuf.ToString()))
                    PrintCalDate(InstrDate, "", InstIdx)
                   
                Case DMM, ARB
                    Try
                        InstrDate = GetDateSerial(InstIdx)
                        ReturnOk = WritePrivateProfileString("Calibration", IniKey(InstIdx), InstrDate, PathVIPERTIni)
                        PrintCalDate(InstrDate, "", InstIdx)
                    Catch ex As Exception
                        'do nothing
                    End Try

                    'This has NV-RAM storage capability but is 10 units
                Case PPU
                    InstrDate = GetDateSerial(InstIdx) 'Returns the lowest date

                Case Else
                    InstrDate = -1
            End Select

            If InstrDate > -1 Then 'If a calibratable instrument
                If InstrDate < LowDate Then LowDate = InstrDate 'Record the earliest date
            End If
            If InstIdx <= ObjectToDouble(LAST_INSTRUMENT) Then
                frmCTest.InstrumentLabel(InstIdx).ForeColor = nStoreColor
            End If

        Next InstIdx

        'Set the effective cal date for SysMon
        ReturnOk = WritePrivateProfileString("Calibration", "SYSTEM_EFFECTIVE", LowDate, PathVIPERTIni)
        PrintCalDate(LowDate, "System Effective", -1)

    End Sub


    Function TestPowerSupplies() As Short

        'DESCRIPTION:
        '   This routine tests the power supplies
        'Returns:
        '   PASSED if the supplies are passing and FAILED if they fail

        Dim TextColor As Color, bytesRead As Integer
        Dim TestStatus As Short, Supply As Short, ErrCode As Integer
        Dim TestCommandData As String, CommandData As String
        Dim DelayStart As Single, StartTime As Single

        Dim iErrStatusCode As Integer 'Error Status Code
        Dim sErrorText(13) As String 'Error Text Array
        Dim sErrText As String = "" 'Error Text to display in Message Box
        Dim sPassFail As String 'Self Test Pass/Fail Status
        Dim i As Short 'Index for loop
        Dim FailCount As Short 'keep track of number of failures


        TextColor = Color.Yellow
        TestStatus = PASSED
        FailCount = 0

        'Query each supply and check self test data which should be 80H if passing
Retest:
        For Supply = 10 To 1 Step -1
            TestCommandData = Convert.ToString(Chr(&H10 + Supply)) & Convert.ToString(Chr(&H0)) & Convert.ToString(Chr(&H0))
            TestCommandData = "1" & Hex(Supply) & "H,0H,0H"

            'Send a Status Query command
            CommandData = Convert.ToString(Chr(Supply)) & Convert.ToString(Chr(&H44)) & Convert.ToString(Chr(&H0))

            delay(0.5)
            SystErr = atxmlDF_viWrite(SignalResourceNameArray(Supply), 0, CommandData, CLng(Len(CommandData)), bytesRead)

            DelayStart = Microsoft.VisualBasic.Timer()
            Do
                If (Microsoft.VisualBasic.Timer() - StartTime) > 0.3 Then
                    StartTime = Microsoft.VisualBasic.Timer()
                    TextColor = IIf(TextColor <> Color.Red, Color.Red, Color.Yellow)
                    frmCTest.InstrumentLabel(PPU).ForeColor = TextColor
                    Application.DoEvents()
                End If
            Loop While (Microsoft.VisualBasic.Timer() - DelayStart) < 0.2

            CommandData = "0" & Hex(Supply) & "H,44H,0H"
            delay(0.5)
            Dim buffer As String = Space(255)
            SystErr = atxmlDF_viRead(SignalResourceNameArray(Supply), 0, buffer, 255, bytesRead)

            'P01-01-001 ~ P10-01-001
            If Asc(Mid(buffer, 2, 1)) = &H80 Then
                Echo(FormatResults(True, "Power Supply " & Str(Supply) & " => " & TestCommandData & " ; " & CommandData & " => " & Hex(Asc(Mid(buffer, 2, 1))) & "H..................", "P" & VB6.Format(Supply, "00") & "-01-001"))
            Else
                TestStatus = FAILED
                Echo("P" & VB6.Format(Supply, "00") & "-01-001")
                Echo("Power Supply " & Str(Supply) & " => " & TestCommandData & " ; " & CommandData & " => " & Hex(Asc(Mid(buffer, 2, 1))) & "H, Failed: Error Byte<" & Hex(Asc(Mid(buffer, 3, 1))) & "H>")

                'P01-02-001 ~ P10-02-001
                If SystErr <> 0 Then
                    Echo("P" & VB6.Format(Supply, "00") & "-02-001")
                    Echo("GPIB Communication Error.  Timeout occurred.")
                    RegisterFailure(PPU, "P" & VB6.Format(Supply, "00") & "-02-001", "GPIB Communication Error.  Timeout occurred.")
                Else

                    'Check Status Byte
                    iErrStatusCode = Asc(Mid(buffer, 2, 1))

                    If (iErrStatusCode And &H40) <> 0 Then
                        sErrorText(8) = "Calibration in Progress"
                    End If
                    If (iErrStatusCode And &H20) <> 0 Then
                        sErrorText(9) = "Invalid Command or Sequence"
                    End If
                    If (iErrStatusCode And &H10) <> 0 Then
                        sErrorText(10) = "Over Voltage Fault"
                    End If
                    If (iErrStatusCode And &H8) <> 0 Then
                        sErrorText(11) = "Over Current Fault"
                    End If
                    If (iErrStatusCode And &H4) <> 0 Then
                        sErrorText(12) = "Constant Current Mode"
                    End If
                    If (iErrStatusCode And &H1) <> 0 Then
                        sErrorText(13) = "Under Voltage Fault (may also indicate certain over current faults)"
                    End If

                    'If BIT Failed, Find out why BIT Failed
                    If Asc(Mid(buffer, 2, 1)) <> &H80 Then
                        ErrCode = Asc(Mid(buffer, 3, 1))
                        If (ErrCode And &H20) <> 0 Then
                            sErrorText(1) = "EEPROM test FAILED"
                        End If
                        If (ErrCode And &H10) <> 0 Then
                            sErrorText(2) = "65V test FAILED"
                        End If
                        If (ErrCode And &H8) <> 0 Then
                            sErrorText(3) = "40V test FAILED"
                        End If
                        If (ErrCode And &H4) <> 0 Then
                            sErrorText(4) = "20V test FAILED"
                        End If
                        If (ErrCode And &H2) <> 0 Then
                            sErrorText(5) = "10V test FAILED"
                        End If
                        If (ErrCode And &H1) <> 0 Then
                            sErrorText(6) = "2V test FAILED"
                        End If
                        If ErrCode = 0 Then
                            sErrorText(7) = "Failed to respond"
                        End If
                    End If
                    'Build Error Text Message
                    For i = 1 To 13
                        If Len(sErrorText(i)) > 0 Then
                            If Len(sErrText) > 0 Then
                                sErrText &= ", " & sErrorText(i)
                            Else
                                sErrText = sErrorText(i)
                            End If
                        End If
                    Next

                    'P01-03-001 ~ P10-03-001
                    Echo("P" & VB6.Format(Supply, "00") & "-03-001" & "    " & sErrText)
                    RegisterFailure(PPU, "P" & VB6.Format(Supply, "00") & "-03-001", "Power Supply" & Str(Supply) & "  :  " & sErrText)
                End If
            End If
        Next Supply

        If TestStatus = FAILED And FailCount = 0 Then
            FailCount = 1
            GoTo Retest
        Else
            TestPowerSupplies = TestStatus
        End If

    End Function
    Function CheckSetRfInstalled() As Boolean
        'DESCRIPTION:
        '   This routine determines if the VIPERT RF option is installed.
        '   The criteria is if ANY RF instrument is found, it returns True.
        '   It sets the RF_OPTION_INSTALLED key in the "System Startup" of VIPERT.Ini.
        '   It disables the RF instruments GUIs if False.
        '   DME CHANGE
        '   Now this function will check RF_OPTION_INSTALLED in the vipert.ini file if
        '        it is true then continue normally if FALSE then disable the RF instruments
        '        but not the RF Switches.  RF_OPTION_INSTALLED now set by User upon startup.
        'RETURNS:
        '   TRUE if the RF option is installed, else False

        Dim RfOption As String
        Dim i As Short, X As Integer

        CheckSetRfInstalled = False

        With frmCTest

            'DME change
            X = GetPrivateProfileString("System Startup", "RF_OPTION_INSTALLED", "0", lpBuf, lpBufLength, PathVIPERTIni)
            RfOption = UCase(Mid(lpBuf.ToString(), 1, 2))

            GetPrivateProfileString("System Startup", "SYSTEM_TYPE", "0", lpBuf, lpBufLength, PathVIPERTIni)
            If (lpBuf.ToString() = "AN/USM-657(V)2") Then
                tets_RF_Installed = True
            Else
                tets_RF_Installed = False
            End If

            'If InStr("YES", RfOption) = 0 Then
            If RfOption = "YE" Then
                'this is an RF station Continue Ctest (DME)
                CheckSetRfInstalled = True '(DME)
                If (tets_RF_Installed = True) Then
                    'rename rf buttons to reflect TETS RF Configuration
                    .InstrumentLabel_22.Text = "RF Power Meter"
                    .InstrumentLabel_24.Text = "RF Counter"
                    .InstrumentLabel_25.Text = "RF Measurement Analyzer"
                End If
            ElseIf RfOption = "NO" Then
                For i = RF_FIRST To RF_LAST
                    CardStatus(i) = False
                    .imgPassFrame(i).Visible = False
                    .imgFailFrame(i).Visible = False
                    .pctIcon(i).Visible = False
                    .InstrumentLabel(i).Visible = False
                    CheckSetRfInstalled = False '(DME)
                Next i
            Else
                'MsgBox "i got nothing", vbOKOnly
            End If
        End With

    End Function


    Function FileExists(ByVal path As String) As Short
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : SYSTEM CONFIDENCE TEST                  *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     This Module Checks To See If A Disk File Exists      *
        '*    EXAMPLE:                                              *
        '*     IsFileThere% = FileExists("C:\ISFILE.EX")            *
        '*    RETURNS:                                              *
        '*     TRUE if File is present.                             *
        '*     False if File is not present.                        *
        '************************************************************
        Dim X As Integer
        X = FreeFile()

        On Error Resume Next
        FileOpen(X, Convert.ToString(path), OpenMode.Input)
        If CompareErrNumber("=", 0) Then
            FileExists = True
        Else
            FileExists = False
        End If
        FileClose(X)

    End Function


    Sub CheckMassiveFailure()
        'DESCRIPTION:
        '   This routine determines if a massive failure has occurred in the system and reports
        '   whether or not a cassis/controller problem is likely the cause

        Dim Chassis1Functioning As Short, Chassis2Functioning As Short, StatusSum As Short

        Chassis1Functioning = True
        Chassis2Functioning = True

        StatusSum = CardStatus(DIGITAL) + CardStatus(SWITCH1) + CardStatus(DMM) + CardStatus(ARB)
        If StatusSum = 0 Then
            Chassis1Functioning = False
        End If
        StatusSum = CardStatus(OSCOPE) + CardStatus(COUNTER) + CardStatus(FGEN) + CardStatus(RFSTIM) + CardStatus(RFDC) + CardStatus(RFLO) + CardStatus(RFDIG)
        If StatusSum = 0 Then
            Chassis2Functioning = False
        End If
        If Not Chassis1Functioning And Not Chassis2Functioning Then
            'GEN-02-001
            Echo("GEN-02-001")
            sMsg = "All VXI instrumentation not responding.  Check controller, software configuration, MXI cable (W4), and MXI cards for possible failures."
            Echo(sMsg)
            MsgBox(sMsg)
            RegisterFailure(, ReturnTestNumber(, 2, 1), sMsg)

        Else
            'GEN-03-001
            If Not Chassis1Functioning Then
                Echo("GEN-03-001")
                sMsg = "All instrumentation in the primary VXI chassis is not functioning.  Check MXI cable (W4) and MXI controller on the primary chassis."
                Echo(sMsg)
                MsgBox(sMsg)
                RegisterFailure(, ReturnTestNumber(, 3, 1), sMsg)

            End If
            'GEN-04-001
            If Not Chassis2Functioning Then
                Echo("GEN-04-001")
                sMsg = "All instrumentation in the secondary VXI chassis is not functioning.  Check MXI cable (W5) and MXI controller on the secondary chassis."
                Echo(sMsg)
                MsgBox(sMsg)
                RegisterFailure(, ReturnTestNumber(, 4, 1), sMsg)

            End If
        End If
    End Sub

    Function ExecCmd(ByVal sCmdLine As String) As Integer
        Dim Proc As PROCESS_INFORMATION
        Dim Start As STARTUPINFO
        Dim Ret As Integer

        'Initialize the STARTUPINFO structure
        Start.cb = Len(Start)
        Start.dwFlags = STARTF_USESHOWWINDOW 'Enable wShowWindow flags
        Start.wShowWindow = SW_HIDE 'Hide the Console window

        'Start the shelled application
        Ret = CreateProcessA(vbNullString, sCmdLine, 0, 0, 1, NORMAL_PRIORITY_CLASS, 0, vbNullString, Start, Proc)

        'Wait for the shelled application to finish
        If Ret <> 0 Then 'If successful
            WaitForSingleObject(Proc.hProcess, INFINITE)
            GetExitCodeProcess(Proc.hProcess, ExecCmd)
            CloseHandle(Proc.hThread)
            CloseHandle(Proc.hProcess)
        Else
            ExecCmd = -99
        End If

    End Function

    Sub Echo(ByVal DataLine As String)
        'DESCRIPTION:
        '   This routine updates the test history of the system confidence test
        'PARAMETERS:
        '   DataLine$ =  Line of text to be appended to the test history
        'JHill modified for V1.6

        EchoText &= vbCrLf & DataLine
        CurPos = Len(EchoText) - Convert.ToString(DataLine).Length
        If DetailStatus = True Then
            frmCTest.DetailsText.Text = EchoText
            frmCTest.DetailsText.SelectionStart = CurPos
            frmCTest.DetailsText.Focus()
            Application.DoEvents()
        End If
    End Sub

    Sub EndProgram()
        'DESCRIPTION:
        '   This routine ends the program, logs the results to the system log, and
        '   notifies the main program of the results of the system confidence test
        '   and gracefully exits the program.
        Dim Supply As Short
        Dim filehandle As System.IO.StreamWriter, TaskID As Integer
        Dim InstrumentToClose As Short

        Dim ErrorStatus As Integer = 0

        'Clear all previous Errors
        Err.Clear()
        'Write Log File
        If SystemStatus = PASSED Then
            WritePrivateProfileString("System Startup", "SYSTEM_SURVEY", "PASS", PathVIPERTIni)
            'Added for FHDB  DJoiner  07/19/2001
            nTestStatus = 1 'PASSED
            'End Record for the System Confidence Test
            RegisterFailure("Last") 'Write final Record
        Else
            WritePrivateProfileString("System Startup", "SYSTEM_SURVEY", "FAIL", PathVIPERTIni)
        End If
        Try
            'Open ProgramPath$ & LOG_FILE For Output As filehandle&
            filehandle = System.IO.File.AppendText("C:\Users\Public\Documents\ATS\SYSLOG.txt")
            filehandle.WriteLine(EchoText)
            filehandle.Close()
        Catch ex As Exception
            'could not write to syslog
        End Try

        ErrorStatus = atxml_Close()
        Environment.Exit(0)

    End Sub

    Sub HandleDetails()
        'DESCRIPTION:
        '   This routine displays or hides the details text box
        '   It has been enhanced to open to the screen bottom when displayed.

        Dim iScreenHeight As Integer

        Dim i As Integer

        'SystemParametersInfo(SPI_GETWORKAREA, 0, CInt(Rect), 0)
        iScreenHeight = (Rect.Bottom - Rect.Top) * 15

        If DetailStatus = False Then
            DetailStatus = True
            frmCTest.Details.Text = "<< No &Details"
            frmCTest.Height = frmCTest.Height + frmCTest.DetailsText.Height
            frmCTest.DetailsText.Height = frmCTest.Height - 400

            frmCTest.DetailsText.Visible = True
            frmCTest.DetailsText.Text = EchoText
            frmCTest.DetailsText.Focus()
            'frmCTest.DetailsText.SelectionStart = Len(frmCTest.DetailsText.Text) - 1
            '.DetailsText.UpTo(Convert.ToString(Chr(160)), True, False)
            frmCTest.DetailsText.Focus()
            frmCTest.sbrUserInformation.Text = "Press this button to hide detailed testing information."
        Else
            DetailStatus = False
            frmCTest.Details.Text = "&Details >>"
            frmCTest.sbrUserInformation.Text = "Press this button to view detailed testing information."
            frmCTest.Height = frmCTest.Height - frmCTest.DetailsText.Height
            frmCTest.DetailsText.Visible = False
        End If


    End Sub


    Function InitMessageBasedInstrument(ByVal instrumentindex As Short) As Short
        'DESCRIPTION:
        '   This Routine initializes a message based instrument and verifies that it
        '   is the correct instrument
        'PARAMETERS:
        '   IHandle& = the handle to the instrument to write
        '   DevSpec$ = the unique symbolic name of the resource
        '   IDSpec$ =  the concatination of the first two elements returned from the *IDN?
        'RETURNS:
        '   A long integer representing any errors that may occur
        'EXAMPLE:
        '   SystErr& = ReceiveMessage (DMMHandle&, Ret$) '* This will retrieve a message from the DMM
        Dim InitStatus As Integer, ErrorStatusRead As Integer
        Dim Items As Short
        Dim ReadBuffer As String = ""
        Dim IHandle As Integer
        Dim DevSpec As String
        Dim IDSpec As String

        IHandle = instrumentHandle(instrumentindex) 'The VISA session handle
        DevSpec = InstrumentSpec(instrumentindex) 'The unique symbolic name of the resource
        IDSpec = IDNResponse(instrumentindex) 'The concatination of the first two elements returned from the *IDN?

        InitStatus = viOpen(SessionHandle, DevSpec, VI_NULL, VI_NULL, IHandle)
        ' InitStatus& = atxmlDF_viOpen(ResourceName(InstrumentIndex), SessionHandle&, DevSpec$, VI_NULL, IHandle&)

        'XXX-00-N01
        Echo(sTestMneu(instrumentindex) & "-00-N01")
        If InitStatus <> VI_SUCCESS Then
            Echo("Error getting handle for " & DevSpec)
            Echo("VISA Error Code: " & GetVisaErrorMessage(IHandle, InitStatus))
            Echo(FormatResults(False, InstrumentDescription(instrumentindex) & " initialization "))
            RegisterFailure(instrumentindex, sTestMneu(instrumentindex) & "-00-N01", sOpComments:="    Initialization Failure. VISA Error Code: " & GetVisaErrorMessage(IHandle, InitStatus))
            InstrumentInitialized(instrumentindex) = False
            ReportFailure(instrumentindex)
            InitMessageBasedInstrument = FAILED
            Exit Function
        End If
        Echo(FormatResults(True, InstrumentDescription(instrumentindex) & " initialization "))

        WriteMsg(instrumentindex, "*RST")
        WriteMsg(instrumentindex, "*CLS")
        delay(0.2)

        Do
            If ObjectToDouble(instrumentindex) = RFSTIM Then Exit Do 'It may not be set to SCPI language yet
            WriteMsg(instrumentindex, "SYST:ERR?")
            SystErr = ReadMsg(instrumentindex, ReadBuffer)
        Loop While Val(ReadBuffer) <> 0
        WriteMsg(instrumentindex, "*IDN?")
        delay(0.5) 'JHill changed from 0.2
        SystErr = ReadMsg(instrumentindex, ReadBuffer)

        'XXX-00-N02
        Echo(sTestMneu(instrumentindex) & "-00-N02")

        Echo(DevSpec & " => *IDN? => " & ReadBuffer)
        Items = StringToList(ReadBuffer, IDList, ",")

        '**************************************************************************************
        '*  DME check for all upper case so that way if an instrument comes back with lower case
        '*  description it will convert all string to upper case and then check.
        '*  fix for DME PCR VSYS - 264
        '**************************************************************************************
        If InStr(Trim(UCase(IDList(MANUF))) & Trim(UCase(IDList(MODEL))), UCase(IDSpec)) <> 0 Then
            Echo(FormatResults(True, InstrumentDescription(instrumentindex) & " identification "))
            InitMessageBasedInstrument = PASSED
            InstrumentInitialized(instrumentindex) = True
        Else
            Echo("Could not find instrument ID string: " & Q & IDSpec & Q & " in " & Q & Trim(IDList(MANUF)) & Trim(IDList(MODEL)) & Q)
            Echo(FormatResults(False, InstrumentDescription(instrumentindex) & " identification "))
            RegisterFailure(instrumentindex, sTestMneu(instrumentindex) & "-00-N02", sOpComments:="    Identification Failure. Unknown: " & Trim(IDList(MANUF)) & Trim(IDList(MODEL)))
            InstrumentInitialized(instrumentindex) = False
            ReportFailure(instrumentindex)
            InitMessageBasedInstrument = FAILED
        End If

        instrumentHandle(instrumentindex) = IHandle 'The VISA session handle


    End Function


    Function InitSwitching() As Short
        'DESCRIPTION:
        '   This Routine checks to see if the proper switching cards are available and
        '   functing.
        'RETURNS:
        '   True if all switching cards are available and configured correctly
        'MODIFIES:
        '   instrumentHandle&(SWITCH1)
        '   SwitchCardOK()
        'EXAMPLE:
        '   If InitSwitching() Then

        Dim InitStatus As Integer
        Dim RetDevParam As Short, MsgRespReg As Short, SwitchIDText As Short, Items As Short

        InitStatus = viOpen(SessionHandle, InstrumentSpec(SWITCH1), VI_NULL, VI_NULL, instrumentHandle(SWITCH1))
        'Initialization
        'LF1-00-N01
        Echo(sTestMneu(SWITCH1) & "-00-N01")
        If InitStatus <> VI_SUCCESS Then
            Echo("Error getting handle for " & InstrumentSpec(SWITCH1))
            Echo("VISA Error Code: " & GetVisaErrorMessage(SessionHandle, InitStatus))
            Echo(FormatResults(False, "    " & InstrumentDescription(SWITCH1) & " initialization "))
            RegisterFailure(SWITCH1, sTestMneu(SWITCH1) & "-00-N01", sOpComments:="    Initialization Failure. VISA Error Code: " & GetVisaErrorMessage(SessionHandle, InitStatus))
            InitSwitching = False
            ReportFailure(SWITCH1)
            Exit Function
        End If
        Echo(FormatResults(True, InstrumentDescription(SWITCH1) & " initialization"))
        InitSwitching = False
        If InitStatus = 0 Then
            WriteMsg(SWITCH1, "PDATAOUT 0-12")
            Echo(InstrumentSpec(SWITCH1) & " => PDATAOUT 0-12 => . . .")
            RetDevParam = 0
            Do
                If RetDevParam >= 30 Then Exit Do 'Escape for hardware error condition
                ReDim Preserve IDData(RetDevParam)
                SystErr = ReadMsg((SWITCH1), IDData(RetDevParam))
                If Len(IDData(RetDevParam)) > 0 Then
                    Echo(IDData(RetDevParam))
                End If
                RetDevParam += 1
                ' Delay enough time for switching module to output another line of data to the output
                '   buffer and then check the Data Output Ready(DOR) bit to see if there is another
                '   line of text to be downloaded.  Repeat process until
                delay(0.2)
                'SystErr& = viIn16(instrumentHandle&(SWITCH1), VI_A16_SPACE, MSG_RESP_REG, MsgRespReg%)
                SystErr = atxmlDF_viIn16(ResourceName(SWITCH1), instrumentHandle(SWITCH1), VI_A16_SPACE, MSG_RESP_REG, MsgRespReg)
            Loop While (MsgRespReg And DOR_MASK) <> 0
            For SwitchIDText = 0 To RetDevParam - 1
                Items = StringToList(IDData(SwitchIDText), IDList, ".")
                Select Case Val(IDList(1))
                    Case SWITCH_CONTROLLER
                        If InStr(IDList(2), "MODEL 1260 UNIVERSAL SWITCH CONTROLLER") Then
                            SwitchCardOK(SWITCH_CONTROLLER) = True
                            InitSwitching = True
                        End If
                    Case LFSWITCH1
                        If InStr(IDList(2), "1260-39") Then
                            SwitchCardOK(LFSWITCH1) = True
                        End If
                    Case LFSWITCH2
                        If InStr(IDList(2), "1260-39") Then
                            SwitchCardOK(LFSWITCH2) = True
                        End If
                    Case PWRSWITCH
                        If InStr(IDList(2), "1260-38A") Then ' & Chr$(13) & Chr$(10) & "SWITCH MODULE"
                            SwitchCardOK(PWRSWITCH) = True
                        End If
                    Case MFSWITCH
                        If InStr(IDList(2), "1260-58") Then
                            SwitchCardOK(MFSWITCH) = True
                        End If
                    Case RFSWITCH
                        If InStr(IDList(2), "1260-66A") Then
                            SwitchCardOK(RFSWITCH) = True
                        End If
                End Select
            Next

            'LF1-00-N02
            Echo("")
            Echo("LF1-00-N02")
            If Not SwitchCardOK(SWITCH_CONTROLLER) Then
                sMsg = "Racal 1260 Switch Controller (Slot 5) Identification FAILED" & vbCrLf & "  Check this module before others, its failure will cause the others to fail."
                Echo(sMsg)
                RegisterFailure(vInstrumentIndex:=SWITCH1, sFailStep:="LF1-00-N02", sOpComments:=sMsg)
                ReportFailure(SWITCH1)
            Else
                Echo(FormatResults(True, "The MODEL 1260 UNIVERSAL SWITCH CONTROLLER identification"))
            End If
            'LF1-00-N03
            Echo("")
            Echo("LF1-00-N03")
            If Not SwitchCardOK(LFSWITCH1) Then
                sMsg = InstrumentDescription(SWITCH1) & " Identification FAILED"
                Echo(sMsg)
                RegisterFailure(vInstrumentIndex:=SWITCH1, sFailStep:="LF1-00-N03", sOpComments:=sMsg)
                ReportFailure(SWITCH1)
            Else
                Echo(FormatResults(True, InstrumentDescription(SWITCH1) & " identification"))
            End If
            'LF2-00-N01
            Echo("")
            Echo("LF2-00-N01")
            If Not SwitchCardOK(LFSWITCH2) Then
                sMsg = InstrumentDescription(SWITCH2) & " Identification FAILED"
                Echo(sMsg)
                RegisterFailure(vInstrumentIndex:=LFSWITCH2, sFailStep:="LF2-00-N01", sOpComments:=sMsg)
                ReportFailure(SWITCH2)
            Else
                Echo(FormatResults(True, InstrumentDescription(SWITCH2) & " identification"))
            End If
            'LF3-00-N01
            Echo("")
            Echo("LF3-00-N01")
            If Not SwitchCardOK(PWRSWITCH) Then
                sMsg = InstrumentDescription(SWITCH3) & " Identification FAILED"
                Echo(sMsg)
                RegisterFailure(vInstrumentIndex:=SWITCH3, sFailStep:="LF3-00-N01", sOpComments:=sMsg)
                ReportFailure(SWITCH3)
            Else
                Echo(FormatResults(True, InstrumentDescription(SWITCH3) & " identification"))
            End If
            'MFS-00-N01
            Echo("")
            Echo("MFS-00-N01")
            If Not SwitchCardOK(MFSWITCH) Then
                sMsg = InstrumentDescription(SWITCH4) & " Identification FAILED"
                Echo(sMsg)
                RegisterFailure(vInstrumentIndex:=SWITCH4, sFailStep:="MFS-00-N01", sOpComments:=sMsg)
                ReportFailure(SWITCH4)
            Else
                Echo(FormatResults(True, InstrumentDescription(SWITCH4) & " identification"))
            End If
            'HFS-00-N01
            Echo("")
            Echo("HFS-00-N01")
            If Not SwitchCardOK(RFSWITCH) Then
                sMsg = InstrumentDescription(SWITCH5) & " Identification FAILED"
                Echo(sMsg)
                RegisterFailure(vInstrumentIndex:=SWITCH5, sFailStep:="HFS-00-N01", sOpComments:=sMsg)
                ReportFailure(SWITCH5)
            Else
                Echo(FormatResults(True, InstrumentDescription(SWITCH5) & " identification"))
            End If
            'LF1-00-N02



        Else
            Echo("")
            Echo("LF1-00-N02")
            sMsg = "Racal 1260 Switch Controller (Slot 5) Identification FAILED" & vbCrLf & "  Check this module before others, its failure will cause the others to fail."
            Echo(sMsg)
            RegisterFailure(vInstrumentIndex:=SWITCH1, sFailStep:="LF1-00-N02", sOpComments:=sMsg)
            ReportFailure(SWITCH1)
            ReportFailure(SWITCH2)
            ReportFailure(SWITCH3)
            ReportFailure(SWITCH4)
            'DME change to handle HF switches regaurdless of RF status
            
            If Not bEoOptionInstalled Then
                ReportFailure(SWITCH5)
            End If
            InitSwitching = False
        End If
    End Function

    Sub InitVar()
        'DESRIPTION:
        '   This routine initializes the program's variables
        Dim PathLen As Integer, NumChar As Integer
        Dim instrumentindex As Short, X As Integer, Y As String
        'Q = Convert.ToString(Chr(34))

        ProgramPath = Application.StartupPath & "\"

        'Find Windows System directory
        SystemDir = "C:\Windows\SysWOW64\"

        'Find VIPERT Ini File
        PathVIPERTIni = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        'Find VIPERT System Log directory
        NumChar = GetPrivateProfileString("File Locations", "SYSTEM_LOG", "x", lpBuf, lpBufLength, PathVIPERTIni)
        If NumChar < 2 Then MsgBox("Problem with " & PathVIPERTIni & ". Cannot find path to System Log.")
        PathSysLogExe = StripNullCharacters(lpBuf.ToString())

        SystemStatus = PASSED
        For instrumentindex = 0 To LAST_INSTRUMENT
            CardStatus(instrumentindex) = PASSED
        Next instrumentindex

        'These are the IEEE-488.2 *IDN? query command model responses
        'RMG altered these to read values from INI file
        CheckSetRfInstalled()

        '    IDNResponse$(DMM) = "E1412A"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DMM", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(DMM) = Y
        '    IDNResponse$(COUNTER) = "E1420B"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COUNTER", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(COUNTER) = Y
        '    IDNResponse$(ARB) = "E1445A"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("ARB", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(ARB) = Y
       
        X = GetPrivateProfileString("OSCOPE", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(OSCOPE) = Y
        '    IDNResponse$(FGEN) = "3152"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("FGEN", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(FGEN) = Y
        '    IDNResponse$(MIC) = ""
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIC", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(mic) = Y
        '    IDNResponse$(CAN) = ""
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CAN", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(can) = Y
        '    IDNResponse$(TCIM) = "PCIDM_V2"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("TCIM", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(TCIM) = Y
        '    IDNResponse$(CDDI) = "SK-5522"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CDDI", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(cddi) = Y
        '    IDNResponse$(GBIT) = "DGE-500T"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("GBIT", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(GBIT) = Y
        '    IDNResponse$(SR) = "5395"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SR", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(SR) = Y

        '    IDNResponse$(EO_IR) = ""
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_IR", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(EO_IR) = Y
        '    IDNResponse$(EO_VIS) = ""
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VIS", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(EO_VIS) = Y
        '    IDNResponse$(EO_LASER) = ""
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_LASER", "IDN", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IDNResponse(SR) = Y

        '*******************************************************************************
        '   These are the VXI or GPIB Addresses
        '   RMG altered these to read values from INI file
        '*******************************************************************************

        '    InstrumentSpec$(DMM) = "VXI0::44::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DMM", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(DMM) = Y
        '    InstrumentSpec$(ARB) = "VXI0::48::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("ARB", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(ARB) = Y
        '    InstrumentSpec$(FGEN) = "VXI0::19::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("FGEN", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(FGEN) = Y

       
        '    InstrumentSpec$(OSCOPE) = "VXI0::17::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("OSCOPE", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(OSCOPE) = Y
        '    InstrumentSpec$(SWITCH1) = "VXI0::38::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH1", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(SWITCH1) = Y
        '    InstrumentSpec$(COUNTER) = "VXI0::18::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COUNTER", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(COUNTER) = Y
        '    InstrumentSpec$(EO_MOD) = ""                    'Initially unknown. GPIB::1,2,3 or 6
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_MOD", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(EO_MOD) = Y
        '    InstrumentSpec$(MIC) = "\\.\W2K_PCI_MBDS1"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIC", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(mic) = Y
        '    InstrumentSpec$(CAN) = ""                    'CAN0::STD5 and CAN1::STD5
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CAN", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(can) = Y
        '    InstrumentSpec$(TCIM) = "10.10.10.1"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("TCIM", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(TCIM) = Y
        '    InstrumentSpec$(CDDI) = "PCI\VEN_1148"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CDDI", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(cddi) = Y
        '    InstrumentSpec$(GBIT) = "PCI\VEN_100B"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("GBIT", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(GBIT) = Y
        '    InstrumentSpec$(SR) = "VXI0::29::INSTR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SR", "ADDR", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentSpec(SR) = Y

        '********************************************************************************
        '   These are the manufacturer, model and name
        '   RMG altered these to read values from INI file
        '********************************************************************************

        '    InstrumentDescription$(DMM) = "HP E1412A Digital Multimeter"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DMM", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(DMM) = Y
        '    InstrumentDescription$(COUNTER) = "HP E1420B Counter/Timer (Slot 2)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COUNTER", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(COUNTER) = Y
        '    InstrumentDescription$(ARB) = "HP E1445A Arbitrary Waveform Generator"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("ARB", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(ARB) = Y
        '    InstrumentDescription$(DIGITAL) = "Teradyne M910 Digital Subsystem"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DTS", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(DIGITAL) = Y
        '    InstrumentDescription$(FGEN) = "Racal Instruments 3152 Function Generator"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("FGEN", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(FGEN) = Y
        '    InstrumentDescription$(RFPM) = "RF Power Meter"
        'lpBuf = New StringBuilder("")
       
        '    InstrumentDescription$(OSCOPE) = "HP E1428A Digitizing Oscilloscope"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("OSCOPE", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(OSCOPE) = Y
        '    InstrumentDescription$(EO_IR) = "VEO-2 Visible Sub-Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_IR", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(EO_IR) = Y
        '    InstrumentDescription$(EO_VIS) = "VEO-2 Infrared Sub-Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VIS", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(EO_VIS) = Y
        '    InstrumentDescription$(EO_LASER) = "VEO-2 LASER Sub-Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_LASER", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(EO_LASER) = Y

        '--- RMG This was commented out in legacy -----------------------------------------------
        '    InstrumentDescription$(FPU) = "APS-6061 Fixed Power Supply Unit"
        '    lpBuf$ = ""
        '    x& = GetPrivateProfileString("PPU", "FPUDESCRIPTION", "0", lpBuf$, Len(lpBuf$), PathVIPERTIni$)
        '    y$ = StripNullCharacters(lpBuf$)
        '    InstrumentDescription$(FPU) = y$
        '----------------------------------------------------------------------------------------

        '    InstrumentDescription$(PPU) = "APS 7081-PPU Programmable Power Unit"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("PPU", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(PPU) = Y
        '    InstrumentDescription$(SWITCH1) = "Racal 1260-39 Low Frequency Switches #1 (Slot 5)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH1", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SWITCH1) = Y
        '    InstrumentDescription$(SWITCH2) = "Racal 1260-39 Low Frequency Switches #2 (Slot 6)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH2", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SWITCH2) = Y
        '    InstrumentDescription$(SWITCH3) = "Racal 1260-38T Low Frequency Switches #3 (Slot 7)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH3", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SWITCH3) = Y
        '    InstrumentDescription$(SWITCH4) = "Racal 1260-58 Medium Frequency Switches (Slot 8)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH4", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SWITCH4) = Y
        '    InstrumentDescription$(SWITCH5) = "Racal 1260-66 High Frequency Switches (Slot 9)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH5", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SWITCH5) = Y
        '    InstrumentDescription$(MIL_STD_1553) = "DDC BUS-69080 MIL-STD-1553 Bus Interface"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIL_STD_1553", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(MIL_STD_1553) = Y
        '    Programmable Serial Bus CCA
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SERIAL", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SERIAL) = Y
        '    InstrumentDescription$(EO_VC) = "Coreco Imaging IC-FA Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VC", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(EO_VC) = Y
        '    InstrumentDescription$(EO_MOD) = "SBIR EO Module"   'Changed at run-time to
        'one of the following:
        '"SBIR Infrared Source"
        '"SBIR Visible Source"
        '"SBIR ALTA"
        '"SBIR Modulated Source"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_MOD", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(EO_MOD) = Y
        frmCTest.InstrumentLabel(EO_MOD).Text = "Any EO Module"
        '    InstrumentDescription$(MIC) = "Vetronix MBDS-PCI MIC Bus Development System"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIC", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(mic) = Y
        '    InstrumentDescription$(CAN) = "NI 777357-02 CAN Interface"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CAN", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(can) = Y
        '    InstrumentDescription$(TCIM) = "Innovative Concepts PCIDM-V2.5 Improved Data Modem"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("TCIM", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(TCIM) = Y
        '    InstrumentDescription$(CDDI) = "SysKonnect SK-5522 CDDI Adapter"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CDDI", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(cddi) = Y
        '    InstrumentDescription$(GBIT) = "D-Link DGE-500T Gigabit LAN Adapter"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("GBIT", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(GBIT) = Y
        '    InstrumentDescription$(COM1) = "Communication Port RS-422"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COM1", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(Com1) = Y
        '    InstrumentDescription$(COM2) = "Communication Port RS-232"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COM2", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(Com2) = Y
        '    InstrumentDescription$(SR) = "NAII 5395-S3481 Synchro/Resolver Processor"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SR", "DESCRIPTION", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        InstrumentDescription(SR) = Y

        '******************************************************************************
        '   These are the VIPERT instrument names
        '   RMG altered these to read values from INI file
        '******************************************************************************

        '    VIPERTName$(DMM) = "Digital Multimeter"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DMM", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(DMM) = Y
        '    VIPERTName$(COUNTER) = "Counter/Timer"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COUNTER", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(COUNTER) = Y
        '    VIPERTName$(ARB) = "Arbitrary Waveform Generator"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("ARB", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(ARB) = Y
        '    VIPERTName$(DIGITAL) = "Digital Test Subsystem (DTS)"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DTS", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(DIGITAL) = Y
        '    VIPERTName$(FGEN) = "Function Generator"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("FGEN", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(FGEN) = Y
        
        '    VIPERTName$(OSCOPE) = "Digitizing Oscilloscope"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("OSCOPE", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(OSCOPE) = Y
        '    VIPERTName$(PPU) = "Programmable Power Unit"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("PPU", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(PPU) = Y
        '    VIPERTName$(SWITCH1) = "LF-1 Switching"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH1", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SWITCH1) = Y
        '    VIPERTName$(SWITCH2) = "LF-2 Switching"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH2", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SWITCH2) = Y
        '    VIPERTName$(SWITCH3) = "LF-3 Switching"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH3", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SWITCH3) = Y
        '    VIPERTName$(SWITCH4) = "MF Switching"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH4", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SWITCH4) = Y
        '    VIPERTName$(SWITCH5) = "HF Switching"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH5", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SWITCH5) = Y
        '    VIPERTName$(MIL_STD_1553) = "MIL-STD-1553 Interface"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIL_STD_1553", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(MIL_STD_1553) = Y
        '   Programmable Serial Bus CCA
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SERIAL", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SERIAL) = Y
        '    VIPERTName$(EO_VC) = "Video Capture Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VC", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(EO_VC) = Y
        '    VIPERTName$(EO_MOD) = "EO Module"   'Changed at run-time to one of the following:
        '                       '"Infrared Source"
        '                       '"Visible Source"
        '                       '"ALTA"
        '                       '"Modulated Source"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_MOD", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(EO_MOD) = Y
        '    VIPERTName$(MIC) = "MIC Bus"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIC", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(mic) = Y
        '    VIPERTName$(CAN) = "CAN Bus"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CAN", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(can) = Y
        '    VIPERTName$(TCIM) = "TCIM"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("TCIM", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(TCIM) = Y
        '    VIPERTName$(CDDI) = "CDDI"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CDDI", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(cddi) = Y
        '    VIPERTName$(GBIT) = "G-Bit Ethernet"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("GBIT", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(GBIT) = Y
        '    VIPERTName$(SR) = "Synchro/Resolver Simulator"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SR", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(SR) = Y
        '    VIPERTName$(COM1) = "COM1"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COM1", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(Com1) = Y
        '    VIPERTName$(COM2) = "CDDI"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COM2", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(Com2) = Y
        '    VIPERTName$(EO_IR) = "EO IR Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_IR", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(EO_IR) = Y
        '    VIPERTName$(EO_VIS) = "EO Visual Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VIS", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(EO_VIS) = Y
        '    VIPERTName$(EO_LASER) = "EO Laser Module"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_LASER", "NAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        VIPERTName(EO_LASER) = Y

        '*******************************************************************************
        '   These are the key names used in the [Calibration] section of VIPERT.INI
        '   RMG altered these to read values from INI file
        '*******************************************************************************

        '    IniKey$(DMM) = "DMM"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DMM", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(DMM) = Y
        '    IniKey$(COUNTER) = "UCT"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COUNTER", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(COUNTER) = Y
        '    IniKey$(ARB) = "ARB"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("ARB", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(ARB) = Y
        'The DTS/DTI should not run thru the cicl
        '    IniKey$(DIGITAL) = "DTS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DTS", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(DIGITAL) = Y
        '    IniKey$(FGEN) = "FG"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("FGEN", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(FGEN) = Y
       
        '    IniKey$(OSCOPE) = "DSCOPE"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("OSCOPE", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(OSCOPE) = Y
        '    IniKey$(PPU) = "UUTPS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("PPU", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(PPU) = Y
        '    IniKey$(SWITCH1) = "LFS1"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH1", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SWITCH1) = Y
        '    IniKey$(SWITCH2) = "LFS2"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH2", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SWITCH2) = Y
        '    IniKey$(SWITCH3) = "LFS3"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH3", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SWITCH3) = Y
        '    IniKey$(SWITCH4) = "MFS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH4", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SWITCH4) = Y
        '    IniKey$(SWITCH5) = "HFS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH5", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SWITCH5) = Y
        '    IniKey$(MIL_STD_1553) = "MIL1553"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIL_STD_1553", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(MIL_STD_1553) = Y
        '    IniKey for Prog. Serial Bus CCA
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SERIAL", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SERIAL) = Y
        '    IniKey$(EO_VC) = "EO_VIDEOCAP"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VC", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(EO_VC) = Y
        '    IniKey$(MIC) = "MIC"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIC", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(mic) = Y
        '    IniKey$(CAN) = "CAN"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CAN", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(can) = Y
        '    IniKey$(TCIM) = "TCIM"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("TCIM", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(TCIM) = Y
        '    IniKey$(CDDI) = "CDDI"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CDDI", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(cddi) = Y
        '    IniKey$(GBIT) = "GBIT"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("GBIT", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(GBIT) = Y
        '    IniKey$(SR) = "SYNCHRO"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SR", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(SR) = Y
        '    IniKey$(EO_IR) = "EO_IR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_IR", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(EO_IR) = Y
        '    IniKey$(EO_VIS) = "EO_VIS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VIS", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(EO_VIS) = Y
        '    IniKey$(EO_LASER) = "EO_LASER"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_LASER", "CALNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        IniKey(EO_LASER) = Y

        '*******************************************************************************
        '   Test hardware mneumonics used for FHDB failure step field
        '   RMG altered these to read values from INI file
        '*******************************************************************************

        '   sTestMneu(DMM) = "DMM"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DMM", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(DMM) = Y
        '   sTestMneu(COUNTER) = "C/T"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("COUNTER", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(COUNTER) = Y
        '    sTestMneu(ARB) = "ARB"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("ARB", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(ARB) = Y
        '    sTestMneu(DIGITAL) = "DTS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("DTS", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(DIGITAL) = Y
        '    sTestMneu(FGEN) = "FGN"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("FGEN", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(FGEN) = Y

        
        '    sTestMneu(OSCOPE) = "DSO"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("OSCOPE", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(OSCOPE) = Y
        '    sTestMneu(PPU) = "PPU"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("PPU", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(PPU) = Y
        '    sTestMneu(SWITCH1) = "LF1"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH1", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SWITCH1) = Y
        '    sTestMneu(SWITCH2) = "LF2"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH2", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SWITCH2) = Y
        '    sTestMneu(SWITCH3) = "LF3"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH3", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SWITCH3) = Y
        '    sTestMneu(SWITCH4) = "MFS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH4", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SWITCH4) = Y
        '    sTestMneu(SWITCH5) = "HFS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SWITCH5", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SWITCH5) = Y
        '    sTestMneu(MIL_STD_1553) = "BUS"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIL_STD_1553", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(MIL_STD_1553) = Y
        '    sTestMneu(SERIAL) = "SER"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SERIAL", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SERIAL) = Y
        '    sTestMneu(EO_VC) = "EVC"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("E0_VC", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(EO_VC) = Y
        '    sTestMneu(EO_MOD) = "EOM"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_MOD", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(EO_MOD) = Y
        '    sTestMneu(MIC) = "HMC"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("MIC", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(mic) = Y
        '    sTestMneu(CAN) = "HCN"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CAN", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(can) = Y
        '    sTestMneu(TCIM) = "HTM"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("TCIM", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(TCIM) = Y
        '    sTestMneu(CDDI) = "HFD"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("CDDI", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(cddi) = Y
        '    sTestMneu(GBIT) = "HGB"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("GBIT", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(GBIT) = Y
        '    sTestMneu(SR) = "HSR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("SR", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(SR) = Y
        '    sTestMneu(EO_IR) = "EOR"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_IR", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(EO_IR) = Y
        '    sTestMneu(EO_VIS) = "EOV"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_VIS", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(EO_VIS) = Y
        '    sTestMneu(EO_LASER) = "EOL"
        'lpBuf = New StringBuilder("")
        X = GetPrivateProfileString("EO_LASER", "FHDBNAME", "0", lpBuf, lpBufLength, PathVIPERTIni)
        Y = StripNullCharacters(lpBuf.ToString())
        sTestMneu(EO_LASER) = Y

        'Passing Log file information for 1553 Bus
        FileLen1553(0) = 211
        GoodLine1553(0, 0) = "Advanced Communication Engine Integerated 1553 Terminal"
        GoodLine1553(0, 1) = "BU-69080 'C' Runtime Library"
        GoodLine1553(0, 2) = "Release Rev 3.0"
        GoodLine1553(0, 3) = "32Bit Windows DLL"
        GoodLine1553(0, 4) = "Microsoft C"
        GoodLine1553(0, 5) = ""
        GoodLine1553(0, 6) = "This is the input string"
        GoodLine1553(0, 7) = ""
        GoodLine1553(0, 8) = "Test results = Test Passed"
        GoodLine1553(0, 9) = "Test results = 1"
        GoodLine1553(0, 10) = ""
        GoodLine1553(0, 11) = ""

        FileLen1553(1) = 150
        GoodLine1553(1, 0) = "Advanced Communication Engine Integerated 1553 Terminal"
        GoodLine1553(1, 1) = "BU-69080 'C' Runtime Library"
        GoodLine1553(1, 2) = "Release Rev 3.0"
        GoodLine1553(1, 3) = "32Bit Windows DLL"
        GoodLine1553(1, 4) = "Microsoft C"
        GoodLine1553(1, 5) = ""
        GoodLine1553(1, 6) = "Test Passed"
        GoodLine1553(1, 7) = ""
        GoodLine1553(1, 8) = ""

        FileLen1553(2) = 539
        GoodLine1553(2, 0) = "Advanced Communication Engine Integerated 1553 Terminal"
        GoodLine1553(2, 1) = "BU-69080 'C' Runtime Library"
        GoodLine1553(2, 2) = "Release Rev 3.0"
        GoodLine1553(2, 3) = "32Bit Windows DLL"
        GoodLine1553(2, 4) = "Microsoft C"
        GoodLine1553(2, 5) = ""
        GoodLine1553(2, 6) = "Testing...Registers Passed test..."
        GoodLine1553(2, 7) = "Testing...Ram Passed aaaa test..."
        GoodLine1553(2, 8) = "Testing...Ram Passed aa55 test..."
        GoodLine1553(2, 9) = "Testing...Ram Passed 55aa test..."
        GoodLine1553(2, 10) = "Testing...Ram Passed 5555 test..."
        GoodLine1553(2, 11) = "Testing...Ram Passed ffff test..."
        GoodLine1553(2, 12) = "Testing...Ram Passed 1111 test..."
        GoodLine1553(2, 13) = "Testing...Ram Passed 8888 test..."
        GoodLine1553(2, 14) = "Testing...Ram Passed 0000 test..."
        GoodLine1553(2, 15) = "Testing...Protocol Unit Passed test..."
        GoodLine1553(2, 16) = "Testing...Interrupt Occurred, test passed..."
        GoodLine1553(2, 17) = ""
        GoodLine1553(2, 18) = ""

        FileLen1553(3) = 143
        GoodLine1553(3, 0) = "Advanced Communication Engine Integerated 1553 Terminal"
        GoodLine1553(3, 1) = "BU-69080 'C' Runtime Library"
        GoodLine1553(3, 2) = "Release Rev 3.0"
        GoodLine1553(3, 3) = "32Bit Windows DLL"
        GoodLine1553(3, 4) = "Microsoft C"
        GoodLine1553(3, 5) = ""
        GoodLine1553(3, 6) = "Pass"
        GoodLine1553(3, 7) = ""
        GoodLine1553(3, 8) = ""

        'Added to Support CICL
        '07/09/01
        ResourceName(PPU) = "DCP_1"
        ResourceName(MIL_STD_1553) = "MIL1553_1"
        ResourceName(DMM) = "DMM_1"
        ResourceName(COUNTER) = "CNTR_1"
        ResourceName(OSCOPE) = "DSO_1"
        ResourceName(FGEN) = "FUNC_GEN_1"
        ResourceName(ARB) = "ARB_GEN_1"
        ResourceName(DIGITAL) = "DWG_1"
        ResourceName(SWITCH1) = "PAWS_SWITCH"
        ResourceName(SWITCH2) = "PAWS_SWITCH"
        ResourceName(SWITCH3) = "PAWS_SWITCH"
        ResourceName(SWITCH4) = "PAWS_SWITCH"
        ResourceName(SWITCH5) = "PAWS_SWITCH"
        ResourceName(SR) = "SRS_1_DS1"
        ResourceName(RFSTIM) = "RFGEN_1"
        ResourceName(SERIAL) = "PCI_SERIAL_1"
        ResourceName(Com1) = "COM_1"
        ResourceName(Com2) = "COM_2"
        ResourceName(GBIT) = "ETHERNET_1"
        ResourceName(mic) = "MICCAN"
        ResourceName(cddi) = "CDDI"
        ResourceName(TCIM) = "TCIDM"

        PsResourceName(1) = "DCPS_40V5A_1"
        PsResourceName(2) = "DCPS_40V5A_2"
        PsResourceName(3) = "DCPS_40V5A_3"
        PsResourceName(4) = "DCPS_40V5A_4"
        PsResourceName(5) = "DCPS_40V5A_5"
        PsResourceName(6) = "DCPS_40V5A_6"
        PsResourceName(7) = "DCPS_40V5A_7"
        PsResourceName(8) = "DCPS_40V5A_8"
        PsResourceName(9) = "DCPS_40V5A_9"
        PsResourceName(10) = "DCPS_65V5A_10"

        sStartTime = Convert.ToString(Now) 'Initialize Start Time
        'Get VIPERT Serial Number. Trim to Database field length.
        'lpBuf = New StringBuilder("")
        NumChar = GetPrivateProfileString("System Startup", "SN", "UNK", lpBuf, lpBufLength, PathVIPERTIni)
        sUUTSerialNo = Strings.Left(lpBuf.ToString(), 15)

    End Sub


    Private Function CheckSetEoInstalled() As Boolean
        '*********************************************************************
        '**  Description:                                                   **
        '**     This routine determines if the VIPERT EO option is installed. **
        '**     The value in the "EO_OPTION_INSTALLED" key in the VIPERT.ini  **
        '**     file is used as an indicator. If the value is blank, the    **
        '**     operator is asked to indicate if an EO variant and the      **
        '**     VIPERT.ini key is set accordingly.                            **
        '**     If False, the EO instruments are disabled in the GUI.       **
        '**  Return:                                                        **
        '**    TRUE if EO option is installed, otherwise FALSE.             **
        '*********************************************************************
        Dim sEoInstalled As String 'Key value from the VIPERT.ini file

        'lpBuf = New StringBuilder("") 'Clear Buffer
        'Get value from the VIPERT.ini file
        ReturnOk = GetPrivateProfileString("System Startup", "EO_OPTION_INSTALLED", "", lpBuf, lpBufLength, PathVIPERTIni)
        'Prepare returned value for analysis, convert string to Upper Case
        sEoInstalled = UCase(StripNullCharacters(lpBuf.ToString()))

        'If EO is Installed, set flag
        'If EO is not installed, set flag
        'If Key is blank, ask user, set flag according to the users response.
        Select Case sEoInstalled
            Case "YES"
                CheckSetEoInstalled = True 'Set Flag to True
            Case "NO"
                CheckSetEoInstalled = False 'Set Flag to False

            Case ""
                If MsgBox("Is this VIPER/T an EOV " & vbCrLf & "(AN/USM-657 (V)3/4)?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2, "Identify VIPER/T Variant") = DialogResult.Yes Then
                    CheckSetEoInstalled = True 'Set Flag to True
                    ReturnOk = WritePrivateProfileString("System Startup", "EO_OPTION_INSTALLED", "YES", PathVIPERTIni)
                Else
                    CheckSetEoInstalled = False 'Set Flag to False
                    ReturnOk = WritePrivateProfileString("System Startup", "EO_OPTION_INSTALLED", "NO", PathVIPERTIni)
                End If
        End Select

        If CheckSetEoInstalled = False Then
            'Disable EO Instruments in GUI
            With frmCTest
                CardStatus(EO_MOD) = False
                .imgPassFrame(EO_MOD).Visible = False
                .imgFailFrame(EO_MOD).Visible = False
                .pctIcon(EO_MOD).Visible = False
            End With
        Else    'move eo icons
            With frmCTest
                Dim point As System.Drawing.Point
                point.X = 279
                point.Y = 90
                .imgPassFrame(EO_VC).Location = point
                point.X = 320
                point.Y = 90
                .imgFailFrame(EO_VC).Location = point
                point.X = 353
                point.Y = 94
                .pctIcon(EO_VC).Location = point
                point.X = 380
                point.Y = 94
                .InstrumentLabel_26.Location = point
                'removing HF Switch from CTEST see TIR 3.0.0.0005-003
                .imgPassFrame_12.Visible = False
                .imgFailFrame_12.Visible = False
                .InstrumentLabel_12.Visible = False



                .pctIcon_12.Visible = False
            End With
        End If

    End Function



    Public Sub Main()
        'LOAD T940DLL FIRST BEFORE APS PS CALLS TO FIX CRASH AT END
        'tat964_close(0)

        Const curOnErrorGoToLabel_Default As Integer = 0
        Const curOnErrorGoToLabel_MainErrorHandler As Integer = 1
        Dim vOnErrorGoToLabel As Integer = curOnErrorGoToLabel_Default
        Try

            '************************************************************
            '* Nomenclature   : VIPERT SYSTEM CONFIDENCE TEST             *
            '* Written By     : Michael McCabe                          *
            '*    DESCRIPTION:                                          *
            '*     This module is the program entry (starting) point.   *
            '************************************************************
            '* Revision Log:                                            *
            '* 2/14/98 V1.7 Changed the way the form was centered. Per  *
            '*              ECO-3047, TDR899005 GJohnson                *
            '* 6/22/99 V1.9 Changed the default settings of the C/T and *
            '*              RF Counter per ECO-3047-235, TDR99128       *
            '*              GJohnson                                    *
            '************************************************************
            Dim FindList As Integer, NumOfMatches As Integer, X As Integer
            Dim instrumentindex As Short, Answer As Short
            Dim Ret As Integer
            Dim VisaLibrary As String
            Dim status As Integer
            Dim XmlBuf As String
            Dim Allocation As String
            Dim Response As String

            XmlBuf = Space(4096)
            Response = Space(4096)

            If AppPrevInstance Then Application.Exit()
            'Keep for debugging
            'MsgBox("SYSTEM_SURVEY", MsgBoxStyle.OkOnly)
            HandleDetails()
            frmCTest.Refresh()

            EchoText = " "

            'Set-Up Main Form
            With frmCTest
                .Cursor = Cursors.AppStarting
                .sbrUserInformation.Text = "Initializing . . ."
                .sbrUserInformation.Font = VB6.FontChangeBold(frmCTest.sbrUserInformation.Font, False)
                Echo("")
                Echo("Initializing System Instruments. . .")

                'EO Module testing removed
                CardStatus(EO_MOD) = False
                .imgPassFrame(EO_MOD).Visible = False
                .imgFailFrame(EO_MOD).Visible = False
                .pctIcon(EO_MOD).Visible = False
                .InstrumentLabel(27).Visible = False

                'Initialize Variables
                InitVar()

                'Notify System Monitor Program that we are RUNNING
                ReturnOk = WritePrivateProfileString("System Startup", "SYSTEM_SURVEY", "RUNNING", PathVIPERTIni)

                'Verify presence of system
                VisaLibrary = SystemDir & "\VISA32.DLL"
                'GEN-01-001
                If Not FileExists(VisaLibrary) Then
                    Echo("GEN-01-001")
                    sMsg = "Cannot find VISA Run-Time System. Unable to perform System Survey."
                    MsgBox(sMsg, MsgBoxStyle.Exclamation)
                    Echo(sMsg)
                    RegisterFailure(sFailStep:=ReturnTestNumber(, 1, 1), sOpComments:=sMsg)
                    Application.Exit()
                Else

                    ' new cicl stuff, validate each instrument
                    '? On Error Resume Next 
                    status = atxml_Initialize(proctype, guid)
                    Allocation = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "PAWSAllocationPath", Nothing)

                    'Determine if the ARB is functioning
                    XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>ARB_GEN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"                    '
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

                    If status <> 0 Then
                        MsgBox("The ARB is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                        LiveMode(ARB) = False
                    Else
                        LiveMode(ARB) = True
                    End If


                    ' Determine if the C/T is functioning
                    XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>CNTR_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"                    '
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                    If status <> 0 Then
                        LiveMode(COUNTER) = False
                        MsgBox("The Counter/Timer Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
                    Else
                        LiveMode(COUNTER) = True
                    End If


                    'Determine if the DMM is functioning
                    XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>DMM_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"                    '
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                    If status <> 0 Then
                        LiveMode(DMM) = False
                        MsgBox("The DMM Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
                    Else
                        LiveMode(DMM) = True
                    End If

                    'Determine If The DSCOPE Is Functioning
                    XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "<ResourceType>Source</ResourceType>" & "<SignalResourceName>DSO_1</SignalResourceName> " & "</ResourceRequirement>" & "</AtXmlTestRequirements>"
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                    If status <> 0 Then
                        LiveMode(OSCOPE) = False
                        MsgBox("The DSO Is Not Responding.  Live Mode Disabled.", MsgBoxStyle.Information)
                    Else
                        LiveMode(OSCOPE) = True
                    End If

                    'Determine if the FG is functioning
                    XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>FUNC_GEN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                    If status <> 0 Then
                        LiveMode(FGEN) = False
                        MsgBox("The Function Generator is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                    Else
                        LiveMode(FGEN) = True
                    End If

                    'Determine if the Freedom PS is functioning
                    status = atxml_Initialize(proctype, guid)
                    XmlBuf = "<AtXmlTestRequirements>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_1</SignalResourceName> " & "</ResourceRequirement> "
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_2</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_3</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_4</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_5</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_6</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>DCPS_7</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_8</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_9</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>DCPS_10</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "</AtXmlTestRequirements>"
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, Len(XmlBuf))
                    If status Then
                        LiveMode(PPU) = False
                        MsgBox("The PPU is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                    Else
                        LiveMode(PPU) = True
                    End If


                    'Determine if the Switch module is functioning
                    XmlBuf = "<AtXmlTestRequirements> " & "    <ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>PAWS_SWITCH</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                    If status = conNoDLL Then
                        MsgBox("Error in loading Ri1260.DLL.  Switch Live mode is disabled.", MsgBoxStyle.Information)
                        LiveMode(SWITCH1) = False
                    ElseIf status <> 0 Then
                        MsgBox("The Switch Module is not responding.  Live mode is disabled.", MsgBoxStyle.Information)
                        LiveMode(SWITCH1) = False
                    Else
                        LiveMode(SWITCH1) = True
                    End If

                    'Determine if the RF STIM is functioning
                    If (CheckSetRfInstalled()) Then
                        XmlBuf = "<AtXmlTestRequirements> " & "    <ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>RFGEN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
                        Response = Space(4096)
                        status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                        If status <> 0 Then
                            MsgBox("The RF Stimulus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                            LiveMode(RFSTIM) = False
                        Else
                            LiveMode(RFSTIM) = True
                        End If
                    End If

                    If (tets_RF_Installed) Then
                        'Determine if the RF MEAS ANAL is functioning
                        If (CheckSetRfInstalled()) Then
                            XmlBuf = "<AtXmlTestRequirements> " & "    <ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>RF_MEASAN_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
                            Response = Space(4096)
                            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                            If status <> 0 Then
                                MsgBox("The RF Stimulus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                                LiveMode(RFSTIM) = False
                            Else
                                LiveMode(RFSTIM) = True
                            End If
                        End If

                        'Determine if the RF CNTR is functioning
                        If (CheckSetRfInstalled()) Then
                            XmlBuf = "<AtXmlTestRequirements> " & "    <ResourceRequirement> " & "       <ResourceType>Source</ResourceType> " & "       <SignalResourceName>RF_CNTR_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"
                            Response = Space(4096)
                            status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                            If status <> 0 Then
                                MsgBox("The RF Stimulus is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                                LiveMode(RFSTIM) = False
                            Else
                                LiveMode(RFSTIM) = True
                            End If
                        End If
                    End If

                    'Determine if the New Busses are functioning
                    Response = Space(4096)
                    XmlBuf = "<AtXmlTestRequirements>" & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>COM_1</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>COM_2</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>ETHERNET_1</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>ETHERNET_2</SignalResourceName> " & "  </ResourceRequirement> " & "<ResourceRequirement>" & "  <ResourceType>Source</ResourceType>" & "  <SignalResourceName>PCISERIAL_1</SignalResourceName> " & "  </ResourceRequirement> " & "</AtXmlTestRequirements>"
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)


                    'Determine if the S/R is functioning
                    XmlBuf = "<AtXmlTestRequirements>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_DS1</SignalResourceName> " & "</ResourceRequirement> "
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_DS2</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_SD1</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "<ResourceRequirement> " & "  <ResourceType>Source</ResourceType> " & "  <SignalResourceName>SRS_1_SD2</SignalResourceName> " & "</ResourceRequirement>"
                    XmlBuf &= "</AtXmlTestRequirements>"

                    Response = Space(4096)
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)
                    If status <> 0 Then
                        MsgBox("The Syncro/Resolver is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                        LiveMode(SR) = False
                    Else
                        LiveMode(SR) = True
                    End If

                    'Determine if the Ballard 1553 is functioning
                    XmlBuf = "<AtXmlTestRequirements>" & "    <ResourceRequirement>" & "        <ResourceType>Source</ResourceType>" & "        <SignalResourceName>MIL1553_1</SignalResourceName> " & "    </ResourceRequirement> " & "</AtXmlTestRequirements>"                    '
                    status = atxml_ValidateRequirements(XmlBuf, Allocation, Response, 4096)

                    If status <> 0 Then
                        MsgBox("The MIL1553 Interface is not responding.  Live mode is disabled.", MsgBoxStyle.Exclamation)
                        LiveMode(MIL_STD_1553) = False
                    Else
                        LiveMode(MIL_STD_1553) = True
                    End If

                    delay(0.5)
                    '? On Error GoTo 0 ' turn off error trapping

                End If

                'GEN-01-002
                SystErr = viOpenDefaultRM(SessionHandle)

                If SystErr <> VI_SUCCESS Then
                    Echo("GEN-01-002")
                    sMsg = "VISA Resource Manager Error: " & GetVisaErrorMessage(SessionHandle, SystErr) & vbCrLf & "Unable to perform System Confidence Test."
                    MsgBox(sMsg, MsgBoxStyle.Exclamation)
                    Echo(sMsg)
                    RegisterFailure(sFailStep:=ReturnTestNumber(, 1, 2), sOpComments:=sMsg)
                    Application.Exit()
                End If

                SystErr = viFindRsrc(SessionHandle, "?*VXI[0-9]*::?*INSTR", FindList, NumOfMatches, lpBuf.ToString)
                'GEN-01-004
                If SystErr <> VI_SUCCESS Then
                    Echo("GEN-01-004")
                    sMsg = "Error locating any VXI resources. Unable to perform System Survey. Error Message: " & Q & GetVisaErrorMessage(SessionHandle, SystErr) & Q
                    MsgBox(sMsg, MsgBoxStyle.Exclamation)
                    Echo(sMsg)
                    RegisterFailure(sFailStep:=ReturnTestNumber(, 1, 4), sOpComments:=sMsg)
                    SystErr = atxml_Close()
                    ReturnOk = WritePrivateProfileString("System Startup", "SYSTEM_SURVEY", "FAIL", PathVIPERTIni)
                    Application.Exit() 'Comment for Debuging only, uncomment "End" to run on VIPERT  'DJoiner  04/12/01
                End If

                vOnErrorGoToLabel = curOnErrorGoToLabel_MainErrorHandler    ' On Error GoTo MainErrorHandler 'Enable Error Handling routine

                bEoOptionInstalled = CheckSetEoInstalled()
                bRfOptionInstalled = CheckSetRfInstalled()

                Support.ZOrder(frmAbout, 0) 'Keep it up
                delay(2)
                Application.DoEvents()

                '**** Initialize Instruments ****
                '********************************
                frmCTest.Refresh()
                For instrumentindex = 0 To LAST_INSTRUMENT
                    Application.DoEvents()
                    If instrumentindex = TCIM Or instrumentindex = cddi Or instrumentindex = mic Then
                        Continue For
                    End If
                    InitInstrument(instrumentindex)
                    frmCTest.Refresh()
                Next instrumentindex

                ReportVariant()

                Echo("")
                Echo("Retrieving Calibration Due Dates . . .")
                .sbrUserInformation.Text = "Retrieving Calibration Due Dates . . ."
                ProcessCalDates()

                '****   Perform Self Tests   ****
                '********************************
                Echo("")
                Echo("Testing instruments . . .")
                .sbrUserInformation.Text = "Initiating Instrument Tests. . ."
                'Send *TST? to message based instruments
                For instrumentindex = 0 To LAST_INSTRUMENT
                    Application.DoEvents()
                    .sbrUserInformation.Text = "Initiating Instrument Tests " & InstrumentDescription(instrumentindex) & " . . ."
                    If InstrumentInitialized(instrumentindex) Then
                        Select Case instrumentindex
                            Case DMM, COUNTER, ARB, FGEN, RFLO, OSCOPE, SR
                                WriteMsg(instrumentindex, "*TST?")
                            Case RFDIG
                                If (tets_RF_Installed) Then
                                    WriteMsg(instrumentindex, "*TST?")
                                Else
                                    WriteMsg(instrumentindex, "CH")
                                End If
                        End Select
                    End If
                Next instrumentindex

                'Read *TST? results and perform special tests for those requiring it
                For instrumentindex = 0 To LAST_INSTRUMENT

                    If instrumentindex = TCIM Or instrumentindex = cddi Or instrumentindex = mic Then
                        Continue For
                    End If

                    If instrumentindex <> EO_MOD Then
                        TestInstrument(instrumentindex, True)
                    ElseIf bEoOptionInstalled = True Then
                        TestInstrument(instrumentindex, True)
                    End If
                    Application.DoEvents()
                Next instrumentindex

                If SystemStatus = FAILED Then
                    .sbrUserInformation.Text = "System Survey Failed"
                Else
                    .sbrUserInformation.Text = "System Survey Test Passed"
                End If

                'Check for Massive failures
                If SystemStatus = FAILED Then
                    CheckMassiveFailure()
                End If

                CTestComplete = True

                'Notify System Monitor Program of completion
                If SystemStatus = PASSED Then
                    WritePrivateProfileString("System Startup", "SYSTEM_SURVEY", "PASS", PathVIPERTIni)
                    'Added for FHDB  DJoiner  07/19/2001
                    nTestStatus = 1 'PASSED
                    'End Record for the System Confidence Test
                    RegisterFailure("Last") 'Write final Record
                Else
                    WritePrivateProfileString("System Startup", "SYSTEM_SURVEY", "FAIL", PathVIPERTIni)
                End If

                frmCTest.Cursor = Cursors.Default
                If Microsoft.VisualBasic.Command() = "" Then 'If C-Test was initiated by User
                    frmCTest.CloseButton.Visible = True
                Else                    'Else, it was initiated by SysMon-Startup
                    If SystemStatus = PASSED Then
                        EndProgram()
                    Else
                        frmCTest.CloseButton.Visible = True
                    End If
                End If

            End With

            ReleaseDCPS()

            Exit Sub

MainErrorHandler:
            If CompareErrNumber("<>", 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description, MsgBoxStyle.Exclamation)
                Err.Clear()
            End If

            ResumeNext()



        Catch
            Select Case vOnErrorGoToLabel
                Case curOnErrorGoToLabel_MainErrorHandler
                    GoTo MainErrorHandler
                Case curOnErrorGoToLabel_Default
                    ' ...
                Case Else
                    ' ...
            End Select
        End Try
    End Sub


    Function ReadMsg(ByVal Instrument As Object, ByRef ReturnMessage As String) As Integer
        'DESCRIPTION:
        '   This Routine is a pass through to the VISA layer using VB conventions to
        '   fascilitate clean Word Serial read communications to message based instruments.
        'PARAMETERS:
        '   IHandle& = the handle to the instrument to write
        '   ReturnMessage$ = the string returmed to VB
        'RETURNS:
        '   A long integer representing any errors that may occur
        'EXAMPLE:
        '   SystErr& = ReceiveMessage (DMMHandle&, Ret$) '* This will retrieve a message from the DMM

        Dim ReadBuffer = Space(4096)

        '    lpBuf$ = ""
        '    SystErr& = viRead(IHandle&, lpBuf$, Len(lpBuf$), retCount&)
        SystErr = atxml_ReadCmds(ResourceName(Instrument), ReadBuffer, 255, retCount)
        ReturnMessage = StripNullCharacters(ReadBuffer)
        ReturnMessage = Trim(Convert.ToString(ReturnMessage))
        ReadMsg = SystErr
    End Function


    Sub RegisterFailure(Optional ByVal vInstrumentIndex As Object = Nothing, Optional ByVal sFailStep As String = "", Optional ByVal sOpComments As String = " ")
        '******************************************************************
        '***    Procedure to Pass Data to the FHDB and return           ***
        '***    an ERROR Code.                                          ***
        '***                                  Dave Joiner  04/12/2001   ***
        '******************************************************************

        Dim sERO As String 'ERO field value
        Dim sSerialNum As String 'ID Serial Number field value
        Dim sUUTRev As String 'UUT Revision field value
        Dim sFaultCallout As String 'Fault Callout field value
        Dim nErrCode As Integer 'Error return code from DLL

        sERO = "" 'Initialize Defaults
        sSerialNum = ""
        sUUTRev = ""

        If bRetryInit Then Exit Sub 'Prevent duplicate failure entries

        Try ' On Error GoTo FHDBErrorHandler

            'If the Instrument Index is missing, Default to a General Failure.
            If IsNothing(vInstrumentIndex) Then
                sFaultCallout = "-----  General Failure or Information  -----"
            ElseIf IsNumeric(vInstrumentIndex) Then
                sFaultCallout = InstrumentDescription(vInstrumentIndex)
            End If

            sFaultCallout &= vbCrLf & sOpComments

            nErrCode = objData.SaveData(sStartTime, CStr(Now), sERO, "CONFIDENCE TEST", _
                sUUTSerialNo, sUUTRev, sSerialNum, nTestStatus, sFailStep, _
                sFaultCallout, MeasuredValue:=0, Dimension:="", _
                UpperLimit:=0, LowerLimit:=0, OperatorComments:="")

            nTestStatus = 0 'Reinitialize to Fail

            Exit Sub

        Catch   ' FHDBErrorHandler:
            If CompareErrNumber("<>", 0) Then
                MsgBox("Error Number: " & Err.Number & vbCrLf & "Error Description: " & Err.Description, MsgBoxStyle.Exclamation, "ERROR")
                Err.Clear()
            End If

            ResumeNext()

        End Try
    End Sub


    Sub ReportFailure(ByVal instrumentindex As Short)
        'DESCRIPTION:
        '   This routine places a red 'X' in the appropraite box of a failiing
        '   instrument
        'PARAMETERS:
        '   InstrumentIndex% = The index of the failing instrument

        frmCTest.imgFailFrame(instrumentindex).Image = My.Resources.FAIL
        CardStatus(instrumentindex) = FAILED
        SystemStatus = FAILED

    End Sub

    Sub ReportPass(ByVal instrumentindex As Short)
        'DESCRIPTION:
        '   This routine places a green check in the appropraite box of a passing
        '   instrument
        'PARAMETERS:
        '   InstrumentIndex% = The index of the failing instrument

        frmCTest.imgPassFrame(instrumentindex).Image = My.Resources.PASS
    End Sub



    Function SwitchingError(ByRef mModule As Short) As Integer
        'DESCRIPTION
        '   This routine returns any errors reported by the 1260 relay cards
        'PARAMETERS:
        '   This integer is returned with the erroring switching module number
        'RETURNS:
        '   The error code of the switching card

        Dim ErrorCode As String = ""
        Dim PerPosition As Short

        WriteMsg(SWITCH1, "YERR")
        SystErr = ReadMsg(SWITCH1, ErrorCode)
        If Len(ErrorCode) > 4 Then
            PerPosition = InStr(ErrorCode, ".")
            mModule = Val(Mid(ErrorCode, PerPosition - 3, 3))
            SwitchingError = Val(Mid(ErrorCode, PerPosition + 1, 2))
        Else
            SwitchingError = SystErr
        End If
    End Function

    Function Test1553() As Short
        'DESCRIPTION
        '   This routine runs the confidence tests on the MIL-STD-1553 Bus emmulator and returns
        '   PASSED or FAILED
        'RETURNS:
        '   PASSED if the MIL-STD-1553 Bus emmulator self tests passes or FAILED if a failure occurs
        'Revision
        '   Modified code to use the 1553Selftst.exe vice the Selftst.exe - Selftst4.exe
        '   Changed because of updated 1553 driver 4.5 G. Johnson 6/8/99


        Dim TestStatus As Short, TestCount As Short, LineCount As Short

        Dim TextColor As Color, FileL As Integer, AppHandle As Integer, STLogHandle As Integer, STFileHandle As Integer
        Dim StartTime As Single, BlinkTime As Single
        Dim TestCommand As String, TestLine As String = ""
        Dim SelfTestBat As String, SelfTestLog As String
        Dim errval As Integer = 0
        Dim hCard As IntPtr
        Dim hCore As IntPtr
        Dim errStr As String
        Dim Response As String = Space(256)

        TestStatus = PASSED
        TextColor = Color.Red

        'issue ATXML IST Signal
        errval = atxml_IssueIst("<AtXmlIssueIst>\n" &
                "              <SignalResourceName>MIL1553_1</SignalResourceName>\n" &
                "              <Level>2</Level>\n" &
                "</AtXmlIssueIst>\n", Response, 256)

        If (Not Response.Contains("status clean")) Then
            Echo(FormatResults(False, "1553 Bus Emulator failed BIT", "BUS-01-003"))
            RegisterFailure(MIL_STD_1553, "BUS-00-N01", "1553 Bus Emulator failed BIT")
            TestStatus = FAILED
        End If

ExitFunction:
        'BUS-01-001
        If TestStatus = PASSED Then
            Echo(FormatResults(True, "The 1553 Bus Interface", "BUS-01-001"))
        Else
            Echo(FormatResults(False, "The 1553 Bus Interface", "BUS-01-001"))
        End If
        Test1553 = TestStatus

    End Function
    Function TestSerial() As Short

        Dim testStatus As Short = True
        Dim response As String = Space(4096)
        Dim status As Integer = 0

        'COM 3
        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                 "              <SignalResourceName>COM_3</SignalResourceName>\n" &
                 "              <Level>2</Level>\n" &
                 "</AtXmlIssueIst>\n", response, 4096)

        If (Not (response.Contains("status clean"))) Then
            testStatus = False
        End If

        'COM 4
        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                 "              <SignalResourceName>COM_4</SignalResourceName>\n" &
                 "              <Level>2</Level>\n" &
                 "</AtXmlIssueIst>\n", Response, 4096)

        If (Not (response.Contains("status clean"))) Then
            testStatus = False
        End If

        'COM 5

        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                 "              <SignalResourceName>COM_5</SignalResourceName>\n" &
                 "              <Level>2</Level>\n" &
                 "</AtXmlIssueIst>\n", Response, 4096)

        If (Not (response.Contains("status clean"))) Then
            testStatus = False
        End If


ExitFunction:
        'SER-01-001
        If TestStatus = PASSED Then
            Echo(FormatResults(True, "The Serial Bus Interface", "SER-01-001"))
        Else
            Echo(FormatResults(False, "The Serial Bus Interface", "SER-01-001"))
        End If
        TestSerial = TestStatus

    End Function


    Function TestDigital() As Short

        Dim dtsModel As String = Space(256)
        dtsModel = GatherIniFileInformation("DTS", "DESCRIPTION", "")

        If (dtsModel.Contains("T940") = True) Then     'Astronics T940 DTS
            Dim TestStatus = 0
            Dim selfTestResult As Short = 0
            Dim selfTestMessage As String = Space(256)

            'DTS-01-00I+1
            'RUN BIT ON EACH MODULE
            For I As Integer = 0 To NumDTIModules - 1
                TestStatus = tat964_self_test(dtiHandles(I), selfTestResult, selfTestMessage)
                'Echo("DTS-00-00" & I + 1)
                If ((selfTestResult <> 0) Or (TestStatus <> 0)) Then
                    Echo(FormatResults(True, "DTS Self Test", "DTS-01-00" & I + 1))
                    RegisterFailure(DIGITAL, "DTS-01-00" & I + 1, "T940 Self Test FAILED.")
                    TestDigital = FAILED
                    Exit Function
                Else
                    Echo(FormatResults(True, "DTS Self Test Slot" & I + 1, "DTS-01-00" & I + 1))
                End If
            Next I

            TestDigital = PASSED

        Else 'Teradyne M910 Digital

            Dim STFileHandle As Integer, STLogHandle As Integer, AppHandle As Integer, TextColor As Color
            Dim StartTime As Single, AbsStartTime As Single

            Dim DigitalStatus As Short
            Dim TestLine As String = ""
            Dim SelfTestBat As String
            Dim SelfTestLog As String

            Dim DTSSelfTestPath As String

            DTSSelfTestPath = "c:\program files (x86)\ivi foundation\visa\winnt\term9\"
            StartTime = Microsoft.VisualBasic.Timer()
            AbsStartTime = Microsoft.VisualBasic.Timer()
            TextColor = Color.Yellow
            DigitalStatus = FAILED
            Echo("Running ""M9Selftest.exe -c""...")

            SelfTestBat = "C:\users\public\documents\ats\Testm910.bat"
            SelfTestLog = "C:\users\public\documents\ats\Testm910.log"

            On Error Resume Next
            Kill(SelfTestBat)
            Kill(SelfTestLog)

            Err.Clear()

            'Added code to check for M910test.exe V1.6 - GJohnson 8/24/98 TDR98283
            'Changed code to look for M9Selftest.exe V1.8.  Teradyne V3.0 of driver changed
            'name of M910Test.exe to M9Selftest.exe - GJohnson 3/15/99 Pre-FAT Patch
            'DTS-01-002
            If Not FileExists(DTSSelfTestPath & "M9Selftest.exe") Then
                Echo("DTS Failed due to missing file M9Selftest.exe.")
                Echo(FormatResults(False, "M910 Self Test.....", "DTS-01-002"))
                RegisterFailure(DIGITAL, "DTS-01-002", "DTS Failed due to missing file M9Selftest.exe." & vbCrLf & "File M9Selftest.exe NOT FOUND.")
                TestDigital = FAILED
                Exit Function
            End If


            STFileHandle = FreeFile()
            FileOpen(STFileHandle, SelfTestBat, OpenMode.Output)
            'DTS-01-003
            If Err.Number Then
                Echo("  Error opening M910 Self Test batch file.")
                Echo(FormatResults(False, "M910 Self Test.....", "DTS-01-003"))
                RegisterFailure(DIGITAL, "DTS-01-003", "Error opening M910 Self Test batch file.")
                TestDigital = FAILED
                Exit Function
            End If
            PrintLine(STFileHandle, "cd " & DTSSelfTestPath & vbCrLf)
            PrintLine(STFileHandle, "M9Selftest.exe -c >""" & SelfTestLog & """")
            FileClose(STFileHandle)

            Dim m9SelfTestProcess As New Process()
            m9SelfTestProcess.StartInfo.FileName = SelfTestBat
            m9SelfTestProcess.StartInfo.CreateNoWindow = True
            m9SelfTestProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            m9SelfTestProcess.Start()
            m9SelfTestProcess.WaitForExit()
            'AppHandle = Shell(Q & SelfTestBat & Q, AppWinStyle.Hide)

            FlashDelay(DIGITAL, 1)
            If Not Err.Number Then
                Do
                    Application.DoEvents() 'Moved here from inside the timer loop below to support Win2K
                    If Microsoft.VisualBasic.Timer() - StartTime > 0.5 Then
                        StartTime = Microsoft.VisualBasic.Timer()
                        If TextColor = Color.Red Then
                            TextColor = Color.Yellow
                        Else
                            TextColor = Color.Red
                        End If
                        frmCTest.InstrumentLabel(DIGITAL).ForeColor = TextColor
                    End If
                Loop While (FileLen(SelfTestLog) < 100) And (Microsoft.VisualBasic.Timer() - AbsStartTime < 60)
                'DTS-01-004
                If (Microsoft.VisualBasic.Timer() - AbsStartTime >= 60) Then
                    Echo("M910 Self Test Failed: Timeout.")
                    Echo(FormatResults(False, "M910 Self Test.....", "DTS-01-004"))
                    RegisterFailure(DIGITAL, "DTS-01-004", "M910 Self Test Failed: Timeout.")
                    TestDigital = FAILED
                    Exit Function
                End If

                Err.Clear()
                STLogHandle = FreeFile()
                FileOpen(STLogHandle, SelfTestLog, OpenMode.Input)
                'DTS-01-005
                If Err.Number Then
                    Echo("M910 Self Test Failed: No Log Generated.")
                    Echo(FormatResults(False, "M910 Self Test.....", "DTS-01-005"))
                    RegisterFailure(DIGITAL, "DTS-01-005", "M910 Self Test Failed: No Log Generated.")
                    TestDigital = FAILED
                    Exit Function
                End If

                Do
                    TestLine = LineInput(STLogHandle)
                    If InStr(TestLine, "***PASSED***") Then
                        DigitalStatus = PASSED
                    End If
                Loop While Not EOF(STLogHandle)
                FileClose(STLogHandle)
                'DTS-01-001
                If DigitalStatus = PASSED Then
                    Echo(FormatResults(True, "M910 Self Test", "DTS-01-001"))
                Else
                    STLogHandle = FreeFile()
                    FileOpen(STLogHandle, SelfTestLog, OpenMode.Input)
                    Do
                        TestLine = LineInput(STLogHandle)
                        Echo(TestLine)
                    Loop While Not EOF(STLogHandle)

                    FileClose(STLogHandle)
                    Echo(FormatResults(False, "M910 Self Test", "DTS-01-001"))
                    RegisterFailure(DIGITAL, "DTS-01-001", "M910 Self Test FAILED.")
                End If
                TestDigital = DigitalStatus
                Exit Function
                'DTS-00-N10
            Else
                Echo("DTS-00-N10       M910 Self Test Failed to initialize.")
                RegisterFailure(DIGITAL, "DTS-00-N10", "M910 Self Test Failed to initialize.")
                TestDigital = FAILED
                Exit Function
            End If


        End If

    End Function


    Sub TestSwitching(ByVal InstrumentToTest As Short)
        'DESCRIPTION
        '   This routine tests the Racal Switching cards.
        '            *** NOTE***
        '       SWITCH1 is the handle for all switch modules because it contains the controller

        Dim Channel As Short, mModule As Short
        Dim SelfTest As String = "", ChanChar As String
        Dim iSubStep As Short 'Sub Test Number Counter

        Select Case InstrumentToTest

            Case SWITCH1
                CardStatus(SWITCH1) = PASSED
                frmCTest.InstrumentLabel(SWITCH1).ForeColor = Color.Red
                Application.DoEvents()
                'FailFlag% = False
                'Non-Destructive RAM Test
                WriteMsg(SWITCH1, "TEST 0.1")
                delay(0.5)
                SystErr = ReadMsg(SWITCH1, SelfTest)
                Echo("LF1-01-001     Non-Destructive RAM Test")
                Echo("1260 Switching Module => TEST 0.1 => " & SelfTest)
                'LF1-01-001
                If Trim(SelfTest) <> "7F" Then
                    ReportFailure(SWITCH1)
                    Echo("1260 Switching Module Non-Destructive RAM Failure ERROR <" & SwitchingError(mModule) & ">")
                    RegisterFailure(SWITCH1, "LF1-01-001", "1260 Switching Module Non-Destructive RAM Failure ERROR <" & SwitchingError(mModule) & ">")
                Else
                    Echo(FormatResults(True, "1260 Switching Module Non-Destructive RAM Test"))
                End If

                frmCTest.InstrumentLabel(SWITCH1).ForeColor = Color.Yellow
                Application.DoEvents()

                'EPROM Checksum Test
                WriteMsg(SWITCH1, "TEST 0.2")
                delay(0.5)
                SystErr = ReadMsg(SWITCH1, SelfTest)
                Echo("LF1-02-001     EPROM Checksum Test")
                Echo("1260 Switching Module => TEST 0.2 => " & SelfTest)
                'LF1-02-001
                If Trim(SelfTest) <> "7F" Then
                    ReportFailure(SWITCH1)
                    Echo("1260 Switching Module EPROM Checksum Failure ERROR <" & SwitchingError(mModule) & ">")
                    RegisterFailure(SWITCH1, "LF1-02-001", "1260 Switching Module EPROM Checksum Failure ERROR <" & SwitchingError(mModule) & ">")
                Else
                    Echo(FormatResults(True, "1260 Switching Module EPROM Checksum Test"))
                End If
                frmCTest.InstrumentLabel(SWITCH1).ForeColor = Color.Red
                Application.DoEvents()

                'Non-Destructive Non-Volatile Memory Test
                WriteMsg(SWITCH1, "TEST 0.3")
                delay(0.5)
                SystErr = ReadMsg(SWITCH1, SelfTest)
                Echo("LF1-03-001     Non-Destructive Non-Volatile Memory Test")
                Echo("1260 Switching Module => TEST 0.3 => " & SelfTest)
                'LF1-03-001
                If Trim(SelfTest) <> "7F" Then
                    ReportFailure(SWITCH1)
                    Echo("1260 Switching Module Non-Volatile Memory Failure ERROR <" & SwitchingError(mModule) & ">")
                    RegisterFailure(SWITCH1, "LF1-03-001", "1260 Switching Module Non-Volatile Memory Failure ERROR <" & SwitchingError(mModule) & ">")
                Else
                    Echo(FormatResults(True, "1260 Switching Module Non-Destructive Non-Volatile Memory Test "))
                End If
                frmCTest.InstrumentLabel(SWITCH1).ForeColor = Color.Yellow
                Application.DoEvents()

                'Switch RESET confidence test
                WriteMsg(SWITCH1, "RESET")
                delay(0.1)
                WriteMsg(SWITCH1, "CNF ON")
                SystErr = SwitchingError(mModule)
                'LF1-04-001
                Echo("LF1-04-001     1260 Switching Module Reset Test")
                If SystErr <> 0 Then
                    Echo("1260 Switching Module Reset Test error<" & SystErr & "> in module" & Str(mModule))
                    RegisterFailure(SWITCH1, "LF1-04-001", "1260 Switching Module Reset Test error<" & SystErr & "> in module" & Str(mModule))
                    If mModule = 0 Then
                        ReportFailure(SWITCH1)
                    Else
                        ReportFailure(SWITCH1 + mModule - 1)
                    End If
                Else
                    Echo(FormatResults(True, "1260 Switching Module Reset Test"))
                End If
                frmCTest.InstrumentLabel(SWITCH1).ForeColor = Color.Red
                Application.DoEvents()

                'SWITCH 1 confidence test
                WriteMsg(SWITCH1, "CLOSE 1.0-4417")
                SystErr = SwitchingError(mModule)
                'LF1-05-001
                Echo("LF1-05-001     1260 Switching Module Close Test")
                If SystErr <> 0 Then
                    Echo("LF1-05-001")
                    Echo("1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                    RegisterFailure(SWITCH1, "LF1-01-005", "1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                    ReportFailure(SWITCH1)
                Else
                    Echo(FormatResults(True, "1260 Switching Module Close Test"))
                End If
                frmCTest.InstrumentLabel(SWITCH1).ForeColor = Color.Yellow
                Application.DoEvents()

                WriteMsg(SWITCH1, "OPEN 1.0-4417")
                SystErr = SwitchingError(mModule)
                'LF1-05-002
                Echo("LF1-05-002     1260 Switching Module Open Test")
                If SystErr <> 0 Then
                    Echo("1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                    RegisterFailure(SWITCH1, "LF1-05-002", "1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                    ReportFailure(SWITCH1)
                Else
                    If CardStatus(SWITCH1) = PASSED Then
                        ReportPass(SWITCH1)
                        Echo(FormatResults(True, "1260 Switching Module Open Test"))
                    End If
                End If
                Echo(FormatResults(True, InstrumentDescription(SWITCH1)))
                Application.DoEvents()
                If CardStatus(SWITCH1) <> PASSED Then
                    Echo(FormatResults(False, InstrumentDescription(SWITCH1)))
                End If

            Case SWITCH2
                'SWITCH 2 confidence test
                If CardStatus(SWITCH2) = PASSED Then

                    frmCTest.InstrumentLabel(SWITCH2).ForeColor = Color.Red
                    frmCTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH2)
                    Application.DoEvents()
                    WriteMsg(SWITCH1, "CLOSE 2.0-4417")
                    SystErr = SwitchingError(mModule)
                    'LF2-01-001
                    Echo("LF2-01-001     1260 Switching Module Close Test")
                    If SystErr <> 0 Then
                        Echo("1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                        RegisterFailure(SWITCH2, "LF2-01-001", "1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                        ReportFailure(SWITCH2)
                    Else
                        Echo(FormatResults(True, "1260 Switching Module Close Test"))
                    End If

                    WriteMsg(SWITCH1, "OPEN 2.0-4417")
                    frmCTest.InstrumentLabel(SWITCH2).ForeColor = Color.Yellow
                    Application.DoEvents()
                    SystErr = SwitchingError(mModule)
                    'LF2-01-002
                    Echo("LF2-01-002     1260 Switching Module Open Test")
                    If SystErr <> 0 Then
                        Echo("1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                        RegisterFailure(SWITCH2, "LF2-01-002", "1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                        ReportFailure(SWITCH2)
                    Else
                        If CardStatus(SWITCH2) = PASSED Then
                            ReportPass(SWITCH2)
                            Echo(FormatResults(True, "1260 Switching Module Open Test"))
                        End If
                    End If
                    Echo(FormatResults(True, InstrumentDescription(SWITCH2)))
                Else
                    Echo(FormatResults(False, InstrumentDescription(SWITCH2)))
                End If

            Case SWITCH3
                'SWITCH 3 confidence test
                If CardStatus(SWITCH3) = PASSED Then
                    frmCTest.InstrumentLabel(SWITCH3).ForeColor = Color.Red
                    frmCTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH3)
                    Application.DoEvents()
                    WriteMsg(SWITCH1, "CLOSE 3.0000-7002")
                    SystErr = SwitchingError(mModule)
                    'LF3-01-001
                    Echo("LF3-01-001     1260 Switching Module Close Test")
                    If SystErr <> 0 Then
                        Echo("1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                        RegisterFailure(SWITCH3, "LF3-01-001", "1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                        ReportFailure(SWITCH3)
                    Else
                        Echo(FormatResults(True, "1260 Switching Module Close Test"))
                    End If

                    frmCTest.InstrumentLabel(SWITCH3).ForeColor = Color.Yellow
                    Application.DoEvents()
                    WriteMsg(SWITCH1, "OPEN 3.0000-7002")
                    SystErr = SwitchingError(mModule)
                    'LF3-01-002
                    Echo("LF3-01-002     1260 Switching Module Open Test")
                    If SystErr <> 0 Then
                        Echo("1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                        RegisterFailure(SWITCH3, "LF3-01-002", "1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                        ReportFailure(SWITCH3)
                    Else
                        If CardStatus(SWITCH3) = PASSED Then
                            ReportPass(SWITCH3)
                            Echo(FormatResults(True, "1260 Switching Module Open Test"))
                        End If
                    End If
                    Echo(FormatResults(True, InstrumentDescription(SWITCH3)))
                Else
                    Echo(FormatResults(False, InstrumentDescription(SWITCH3)))
                End If

            Case SWITCH4
                'SWITCH 4 confidence test
                If CardStatus(SWITCH4) = PASSED Then
                    frmCTest.InstrumentLabel(SWITCH4).ForeColor = Color.Red
                    frmCTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH4)
                    Application.DoEvents()
                    iSubStep = 0
                    For Channel = 0 To 8
                        iSubStep += 1 'Increase Step by one
                        ChanChar = Channel
                        WriteMsg(SWITCH1, "CLOSE 4.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar)
                        SystErr = SwitchingError(mModule)
                        'MFS-01-001, MFS-01-003, MFS-01-005, MFS-01-007, MFS-01-009, MFS-01-011, MFS-01-013, MFS-01-015, MFS-01-017
                        If SystErr <> 0 Then
                            Echo("MFS-01-0" & VB6.Format(iSubStep, "00"))
                            Echo("1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                            RegisterFailure(SWITCH4, "MFS-01-0" & VB6.Format(iSubStep, "00"), "1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                            ReportFailure(SWITCH4)
                        End If
                        If Channel = 4 Then
                            frmCTest.InstrumentLabel(SWITCH4).ForeColor = Color.Yellow
                            Application.DoEvents()
                        End If
                        WriteMsg(SWITCH1, "OPEN 4.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar)
                        SystErr = SwitchingError(mModule)
                        'MFS-01-002, MFS-01-004, MFS-01-006, MFS-01-008, MFS-01-010, MFS-01-012, MFS-01-014, MFS-01-016, MFS-01-018
                        If SystErr <> 0 Then
                            Echo("MFS-01-0" & VB6.Format(iSubStep, "00"))
                            Echo("1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                            RegisterFailure(SWITCH4, "MFS-01-0" & VB6.Format(iSubStep, "00"), "1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                            ReportFailure(SWITCH4)
                        End If
                    Next Channel
                    If CardStatus(SWITCH4) = PASSED Then
                        ReportPass(SWITCH4)
                        Echo("MFS-01-001 ~ MFS-01-018     1260 Switching Module Open/Close Test")
                        Echo(FormatResults(True, InstrumentDescription(SWITCH4)))
                    End If
                    Application.DoEvents()
                End If

            Case SWITCH5
                'DME change to always test the HF switches
                If (CardStatus(SWITCH5) = PASSED) Then 'And bRfOptionInstalled Then
                    frmCTest.InstrumentLabel(SWITCH5).ForeColor = Color.Red
                    frmCTest.sbrUserInformation.Text = "Testing " & InstrumentDescription(SWITCH5)
                    iSubStep = 0
                    For Channel = 0 To 5
                        iSubStep += 1
                        If Channel = 2 Then
                            frmCTest.InstrumentLabel(SWITCH5).ForeColor = Color.Yellow
                            Application.DoEvents()
                        End If
                        If Channel = 4 Then
                            frmCTest.InstrumentLabel(SWITCH5).ForeColor = Color.Red
                            Application.DoEvents()
                        End If
                        ChanChar = Channel
                        WriteMsg(SWITCH1, "CLOSE 5.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar & ",4" & ChanChar & ",5" & ChanChar)
                        SystErr = SwitchingError(mModule)
                        'HFS-01-001, HFS-01-003, HFS-01-005, HFS-01-007, HFS-01-009, HFS-01-011
                        If SystErr <> 0 Then
                            Echo("HFS-01-0" & VB6.Format(iSubStep, "00"))
                            Echo("1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                            RegisterFailure(SWITCH5, "HFS-01-0" & VB6.Format(iSubStep, "00"), "1260 Switching Module Close Test error<" & SystErr & "> in module" & Str(mModule))
                            ReportFailure(SWITCH5)
                        End If
                        WriteMsg(SWITCH1, "OPEN 5.0" & ChanChar & ",1" & ChanChar & ",2" & ChanChar & ",3" & ChanChar & ",4" & ChanChar & ",5" & ChanChar)
                        SystErr = SwitchingError(mModule)
                        'HFS-01-002, HFS-01-004, HFS-01-006, HFS-01-008, HFS-01-010, HFS-01-012
                        If SystErr <> 0 Then
                            Echo("HFS-01-0" & VB6.Format(iSubStep, "00"))
                            Echo("1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                            RegisterFailure(SWITCH5, "HFS-01-0" & VB6.Format(iSubStep, "00"), "1260 Switching Module Open Test error<" & SystErr & "> in module" & Str(mModule))
                            ReportFailure(SWITCH5)
                        End If
                    Next Channel
                    Application.DoEvents()

                    If CardStatus(SWITCH5) = PASSED Then
                        ReportPass(SWITCH5)
                        Echo("HFS-01-001 ~ HFS-01-012     1260 Switching Module Open/Close Test")
                        Echo(FormatResults(True, InstrumentDescription(SWITCH5)))
                    End If
                End If
        End Select
        'Close interconnect relays RMG
        WriteMsg(SWITCH1, "OPEN 3.1000,1002,2000,2001,3002,4002,5000,5001,5002,6002,7002,8002")
        WriteMsg(SWITCH1, "CLOSE 3.1001,2002,3000,3001,4000,4001,6000,6001,7000,7001")
    End Sub

    Sub WaitForResponse(ByVal instrumentindex As Short)
        'DESCRIPTION
        '   This routine waits for a message to be placed in the instruments output
        '   buffer queue and blinks the appropriate text.
        'PARAMETERS:
        '   InstrumentIndex% = The Index of the instrument for which to wait


        Dim count As Short, ColorRed As Short, MsgRespReg As Short

        For count = 1 To 260 'This allows for a wait of about 80 secods
            'Blink color from red to yellow
            If ColorRed Then
                frmCTest.InstrumentLabel(instrumentindex).ForeColor = Color.Red
                ColorRed = False
            Else
                frmCTest.InstrumentLabel(instrumentindex).ForeColor = Color.Yellow
                ColorRed = True
            End If
            delay(0.3)
            'Check to see if Data Output Ready (DOR) bit goes high
            If (instrumentindex = OSCOPE) Then
                SystErr = viIn16(instrumentHandle(instrumentindex), VI_A16_SPACE, MSG_RESP_REG, MsgRespReg)
            Else
                SystErr = atxmlDF_viIn16(ResourceName(instrumentindex), instrumentHandle(instrumentindex), VI_A16_SPACE, MSG_RESP_REG, MsgRespReg)
            End If

            If (MsgRespReg And DOR_MASK) <> 0 Then
                Exit Sub
            End If
        Next count
    End Sub


    Sub WriteMsg(ByVal Instrument As Object, ByVal MessageToSend As String)
        'DESCRIPTION:
        '   This Routine is a pass through to the VISA layer using VB conventions to
        '   fascilitate clean Word Serial write communications to message based instruments.
        'PARAMETERS:
        '   IHandle& = the handle to the instrument to write
        '   MessageToSend$ = the string of data to be written
        'EXAMPLE:
        '   SendMessage DMMHandle&, "*TST?" '* This will run a self test on the DMM

        If Instrument Then
            '        SystErr& = viWrite(IHandle&, MessageToSend$, Len(MessageToSend$), retCount&)
            SystErr = atxml_WriteCmds(ResourceName(Instrument), MessageToSend, retCount)
        End If
    End Sub

    Function StringToList(ByVal strng As String, ByRef List() As String, ByVal delimiter As String) As Short
        'DESCRIPTION:
        ' Procedure to convert a delimited string into a list array
        'Parameters:
        ' strng$     : String to be converted.
        ' list$()    : Array in which to return list of strings
        ' Delimiter$ : Char array of valid delimiters.
        'Returns:
        ' Number of items in list
        ' Returns -1 if number of number of elements exceeds
        ' upper bound of passed array

        Dim numels As Short, inflag As Short, ListClear As Integer, slength As Integer, ch As Integer
        Dim mChar As String

        numels = 0
        inflag = 0

        'Clear Passed Array
        For ListClear = 0 To UBound(List)
            List(ListClear) = ""
        Next ListClear

        'Go through parsed string a character at a time.
        slength = Convert.ToString(strng).Length
        For ch = 1 To slength
            mChar = Mid(Convert.ToString(strng), ch, 1)
            'Test for delimiter
            If InStr(Convert.ToString(delimiter), mChar) = 0 Then
                If Not inflag Then
                    'Test for too many arguments.
                    If numels = UBound(List) Then
                        StringToList = -1
                        Exit For
                    End If
                    numels += 1
                    inflag = -1
                End If
                'Add the character to the current argument.
                List(numels) &= mChar
            Else
                'Found a delimiter.
                'Set "Not in element" flag to FALSE.
                inflag = 0
            End If
        Next ch
        StringToList = numels

    End Function

    Sub delay(ByVal dSeconds As Double)
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : VIPERT SYSTEM CONFIDENCE TEST             *
        '* Written By     : Jeff Hill                               *
        '* DESCRIPTION:                                             *
        '*   Delays for a specified number of seconds.              *
        '*   50 milli-second resolution.                            *
        '*   Minimum delay is about 50 milli-seconds.               *
        '* PARAMETERS:                                              *
        '*   dSeconds: The number of seconds to Delay.              *
        '************************************************************
        Dim t As Double

        System.Threading.Thread.Sleep(dSeconds * 1000)

    End Sub


    Function StripNullCharacters(ByVal Parsed As String) As String
        StripNullCharacters = ""
        'DESCRIPTION:
        '   This routine strips characters with ASCII values less than 32 from the
        '   end of a string
        'PARAMTERS:
        '   Parsed$ = String from which to remove null characters
        'RETURNS:
        '   A the resultant parsed string

        Dim X As Integer

        For X = Convert.ToString(Parsed).Length To 1 Step -1
            If Asc(Mid(Convert.ToString(Parsed), X, 1)) > 32 Then
                Exit For
            End If
        Next X
        StripNullCharacters = Strings.Left(Convert.ToString(Parsed), X)
    End Function

    Function FlashDelay(ByVal instrumentindex As Short, ByRef amount As Single) As Short
        'DESCRIPTION
        '   This delays for amount% in seconds and flashes the instrument name
        '   Added in V1.6 to provide a universal delay routine for the Mod Anal
        'PARAMETERS:
        '   InstrumentIndex% = The Index of the instrument for which to flash
        '   amount! = the number of seconds to delay

        Dim count As Short, ColorRed As Short

        amount /= 0.3
        For count = 1 To CInt(amount)
            'Blink color from red to yellow
            If ColorRed Then
                frmCTest.InstrumentLabel(instrumentindex).ForeColor = Color.Red
                ColorRed = False
            Else
                frmCTest.InstrumentLabel(instrumentindex).ForeColor = Color.Yellow
                ColorRed = True
            End If
            delay(0.3)
        Next count
    End Function

    Sub InitInstrument(ByVal instrumentindex As Short)
        'DESCRIPTION:
        '   Calls initialization/identification routines for individual instruments.
        'PARAMETERS:
        '   InstrumentIndex% = The index of the instrument to init

        'Bail out now by option to prevent testing for it in the main loop.
        Select Case instrumentindex
            Case RF_FIRST To RF_LAST
                If Not bRfOptionInstalled Then Exit Sub
                'Case EO_FIRST To EO_FIRST 'TEST VCC FOR MNG
                '    If Not bEoOptionInstalled Then Exit Sub
                '    '        Case HM_FIRST To HM_LAST:   If Not bHmOptionInstalled Then Exit Sub
        End Select

        Select Case instrumentindex
            Case MIL_STD_1553, SWITCH2 To SWITCH5, SERIAL, GBIT

            Case Else
                Echo("")
        End Select

        frmCTest.sbrUserInformation.Text = "Initializing " & InstrumentDescription(instrumentindex) & " . . ."
        frmCTest.InstrumentLabel(instrumentindex).ForeColor = Color.Blue


        Select Case instrumentindex
            Case MIL_STD_1553, SERIAL, GBIT, Com1, Com2
                InstrumentInitialized(instrumentindex) = True

            Case DMM, COUNTER, ARB, FGEN, OSCOPE, RFSTIM, SR
                If InitMessageBasedInstrument(instrumentindex) Then
                    If (ObjectToDouble(instrumentindex) = COUNTER) Then
                        'Set Counter/Timer's default impedance value to 1 Mohm.
                        WriteMsg(instrumentindex, ":DIAG:RST:INP1:IMP MAX")
                        WriteMsg(instrumentindex, ":DIAG:RST:INP2:IMP MAX")
                    ElseIf ObjectToDouble(instrumentindex) = RFSTIM Then
                        WriteMsg(instrumentindex, "SCPI")
                        'SCPI' Will generate error if already in SCPI
                        WriteMsg(instrumentindex, "*CLS") 'Clear error queue
                    End If
                End If
            Case SWITCH1
                ' Switching Modules
                InitSwitching()
                InstrumentInitialized(SWITCH1) = SwitchCardOK(LFSWITCH1)
                InstrumentInitialized(SWITCH2) = SwitchCardOK(LFSWITCH2)
                InstrumentInitialized(SWITCH3) = SwitchCardOK(PWRSWITCH)
                InstrumentInitialized(SWITCH4) = SwitchCardOK(MFSWITCH)
                InstrumentInitialized(SWITCH5) = SwitchCardOK(RFSWITCH)

            Case DIGITAL
                If Not InitDigital() Then
                    Echo("DTS failed to initialize.")
                    InstrumentInitialized(instrumentindex) = False
                    ReportFailure(DIGITAL)
                Else
                    InstrumentInitialized(instrumentindex) = True
                End If
            Case PPU
                InstrumentInitialized(instrumentindex) = InitPowerSupplies()

            Case EO_VC
                If InitEoVcc() = PASSED Then
                    Echo("VCC-00-001")
                    Echo(FormatResults(True, "Video Capture" & " Initialization"))
                    InstrumentInitialized(instrumentindex) = True
                Else
                    Echo("VCC-00-001")
                    RegisterFailure(instrumentindex, "VCC-01-001", "Instrument is not functioning")
                    InstrumentInitialized(instrumentindex) = False
                    ReportFailure(instrumentindex)
                End If

            Case can
                init_CBTS(instrumentindex)

        End Select

        If InstrumentInitialized(instrumentindex) = True Then
            frmCTest.InstrumentLabel(instrumentindex).ForeColor = Color.Black
        Else
            frmCTest.InstrumentLabel(instrumentindex).ForeColor = ColorTranslator.FromOle(GRAY)
        End If

    End Sub

    Sub init_CBTS(ByVal instrumentindex As Short)

        Dim tsMsg As String
        Dim insMsg As String
        Try ' On Error GoTo error_handler

            Select Case instrumentindex
                Case can
                    tsMsg = "CAN-00-001"
                    insMsg = "CAN"
            End Select

            Select Case instrumentindex

                Case can
                    InstrumentInitialized(instrumentindex) = True

            End Select
            Exit Sub

        Catch   ' error_handler:
            ReportFailure(instrumentindex)
            Echo(tsMsg)
            Echo(FormatResults(False, "CBTS Instrument " & insMsg & " Initialization"))
            InstrumentInitialized(instrumentindex) = False
            RegisterFailure(instrumentindex, tsMsg, "Instrument FAILED to Initialize.")

        End Try
    End Sub

    Function InitEoVcc() As Boolean
        Dim iNumChar As Short
        Dim sMsg As String
        Dim sBuf As String
        Dim iStep As Short

        InitEoVcc = True

    End Function

    Function TestEoVcc() As Boolean
        ' Sequence through boards
        ' Create object for each board

        Dim FORMAT As String = "default"
        Dim FORMATFILE As String = ""

        Try
            CapMod(0) = PXD_PIXCIOPEN("", FORMAT, FORMATFILE)
            If CapMod(0) = 0 Then
                Capture_Boards = 1
                PXD_PIXCICLOSE()
                TestEoVcc = True
            Else
                TestEoVcc = False
                PXD_PIXCICLOSE()
                Exit Function
            End If

        Catch ex As Exception
            TestEoVcc = False
        End Try

    End Function


    Sub EOPower(ByVal sState As String)
        Static bEoPowerOn As Boolean

        If UCase(sState) = "ON" Then
            If bEoPowerOn Then Exit Sub
            If iEO_Module = 0 Then
                'MsgBox "Ensure the Power and Ethernet cables are connected to " &
                '              frmCTest.InstrumentLabel(EO_MOD).Caption & ".", vbOKOnly
            End If
            bEoPowerOn = True

            'Set DC Supply 3 to -15 volts, Constant Current
            FlashDelay(EO_MOD, 0.3)
            DcpsSetPolarity(3, "-")
            FlashDelay(EO_MOD, 0.3)
            DcpsSetOptions(3, False, True, True, True)
            FlashDelay(EO_MOD, 0.3)
            DcpsSetCurrent(3, 5)
            FlashDelay(EO_MOD, 0.3)
            DcpsSetVoltage(3, 15)

            'Set DC Supply 2 to +28 volts slave mode
            FlashDelay(EO_MOD, 0.3)
            DcpsSetOptions(2, False, False, True, False)
            'Set DC Supply 1 to 28 volts master mode
            FlashDelay(EO_MOD, 0.3)
            DcpsSetOptions(1, False, True, True, False)
            FlashDelay(EO_MOD, 0.3)
            DcpsSetCurrent(1, 5)
            FlashDelay(EO_MOD, 0.3)
            DcpsSetCurrent(2, 5)
            FlashDelay(EO_MOD, 0.3)
            DcpsSetVoltage(1, 28)
            FlashDelay(EO_MOD, 2)
        Else
            If Not bEoPowerOn Then Exit Sub
            FlashDelay(EO_MOD, 0.3)
            SendDCPSCommand(3, Convert.ToString(Chr(&H23)) & Convert.ToString(Chr(&HA0)) & Convert.ToString(Chr(&H0)))
            FlashDelay(EO_MOD, 0.3)
            DcpsSetVoltage(1, 0)
            FlashDelay(EO_MOD, 0.3)
            SendDCPSCommand(1, Convert.ToString(Chr(&H21)) & Convert.ToString(Chr(&HA0)) & Convert.ToString(Chr(&H0)))
            FlashDelay(EO_MOD, 0.3)
            SendDCPSCommand(2, Convert.ToString(Chr(&H22)) & Convert.ToString(Chr(&HA0)) & Convert.ToString(Chr(&H0)))
            bEoPowerOn = False
        End If

    End Sub

    Sub ReportVariant()
        'DR-143     DJoiner  04/12/01
        'Added to report VIPERT Variant Information to the System Log
        'Modified by J. Hill on 10/24/02 to use existing flags and support HMV.

        Dim lpFileName As String
        Dim systemType As New StringBuilder(255)
        Dim sysType As String 'System Configuration
        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)
        'Read ini file to get system type
        GetPrivateProfileString("System Startup", "SYSTEM_TYPE", "", systemType, 255, lpFileName)

        sysType = systemType.ToString()


        sMsg = "--- SYSTEM SURVEY ---" & vbCrLf & "--- ATS System Configuration:" & sysType
        'Write ATS System configuration on first line of Details
        EchoText = vbCrLf & sMsg & EchoText

    End Sub


    Function ReturnTestNumber(Optional ByVal vInstrumentIndex As Object = Nothing, Optional ByVal iTestNum As Short = 0, Optional ByVal vSubTestNum As Short = 0, Optional ByVal iEOModID As Short = 0) As String
        ReturnTestNumber = ""
        '******************************************************************************************
        '   Returns a test number in the TTT-XX-SSS format.        TTT-XX-SSS                     *
        '   TTT : Instrument Identification code                                                  *
        '   XX  : Two digit test number (Begins with 01 for each Instrument)                      *
        '   SSS : Three digit subtest number (000 if no sub tests, begins with 001 if sub tests)  *
        '******************************************************************************************

        If IsNumeric(vSubTestNum) Then 'If Sub Test Number is a number then format it.
            If IsNothing(vInstrumentIndex) Then
                ReturnTestNumber = "GEN" & "-" & VB6.Format(iTestNum, "0#") & "-" & VB6.Format(vSubTestNum, "0##")
            Else
                ReturnTestNumber = sTestMneu(vInstrumentIndex) & "-" & VB6.Format(iTestNum, "0#") & "-" & VB6.Format(vSubTestNum, "0##")
            End If
        Else
            ReturnTestNumber = sTestMneu(vInstrumentIndex) & "-" & VB6.Format(iTestNum, "0#") & "-" & vSubTestNum
        End If

    End Function


    Function FormatResults(ByVal bStatus As Boolean, Optional ByVal sTextPassed As String = "", Optional ByVal sFaultCode As String = "") As String
        FormatResults = ""
        '**************************************************************************************
        '*    Returns a formated String depending on arguments sent.                          *
        '*    Formats either a single fault code or a range.                                  *
        '*    Formats an informational Pass/Fail Text line.                                   *
        '*                                                         Dave Joiner 08/31/2001     *
        '**************************************************************************************

        Dim sReturnField As String 'Text as the String is built
        Dim sStatus As String 'Text value of Pass/Fail Status
        Dim iSpaces As Short 'Number of spaces for formating

        'In the event Len(sReturnField) is greater than iSpaces Resume Next
        On Error Resume Next

        'Define Pass/Fail Status
        If bStatus = 0 Then
            sStatus = " FAILED"
        Else
            sStatus = " PASSED"
        End If

        iSpaces = 67

        'Format Text to return
        If Len(sFaultCode) > 0 Then
            sReturnField = sFaultCode & "     " & sTextPassed
            If Len(sReturnField) > 67 Then
                Echo(sFaultCode)
                sReturnField = sTextPassed
            End If
        Else
            sReturnField = sTextPassed
        End If

        sReturnField &= Space(iSpaces - Len(sReturnField))
        sReturnField &= sStatus

        'Return Formated Results

        FormatResults = sReturnField
    End Function


    Public Sub InitBusVar()
        'resource names
        rName(1) = "COM_1"
        rName(2) = "COM_2"
        rName(3) = "PCISERIAL_1"
        rName(4) = "PCISERIAL_1"
        rName(5) = "PCISERIAL_1"
        rName(6) = "ETHERNET_1"
        rName(7) = "ETHERNET_2"

        pName(1) = "RS_422"
        pName(2) = "RS_232"
        pName(3) = "RS_232"
        pName(4) = "RS_422"
        pName(5) = "RS_485"
        pName(6) = "TCP"
        pName(7) = "UDP"

    End Sub


    Public Function TestGigabitEthernet() As Short

        Dim status As Integer
        Dim bEthernet1 As Boolean
        Dim bEthernet2 As Boolean
        Dim Response As String = Space(4096)

        'initialized to false so only a true statment will allow to pass
        bEthernet1 = False
        bEthernet2 = False

        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                "              <SignalResourceName>ETHERNET_1</SignalResourceName>\n" &
                "              <Level>2</Level>\n" &
                "</AtXmlIssueIst>\n", Response, 4096)

        If (Response.Contains("No error") Or Response.Contains("status clean")) Then
            bEthernet1 = True
        End If

        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                "              <SignalResourceName>ETHERNET_2</SignalResourceName>\n" &
                "              <Level>2</Level>\n" &
                "</AtXmlIssueIst>\n", Response, 4096)

        If (Response.Contains("No error") Or Response.Contains("status clean")) Then
            bEthernet2 = True
        End If


        'if both tests pass then the whole test passed
        If bEthernet1 And bEthernet2 = True Then
            TestGigabitEthernet = PASSED
        Else
            TestGigabitEthernet = FAILED
        End If

        If TestGigabitEthernet = PASSED Then
            ReportPass(GBIT)

        ElseIf TestGigabitEthernet = FAILED Then
            ReportFailure(GBIT)
        Else
            'Report unknown error
        End If

    End Function


    Public Function TestSerialBUS() As Short

        'individual bus listing
        Dim s422 As Boolean
        Dim s485 As Boolean
        Dim s232 As Boolean

        'initialize to false
        s422 = False
        s485 = False
        s232 = False

        InitBusVar()

        'test the RS232 mode
        Wparity = "ODD"
        Wbitrate = "9600Hz"
        Wstopbits = "2bits"
        Wwordlength = "8"
        Wmaxt = "2s"
        Wterm = "OFF"
        Wspec = "RS_232"
        Wdelay = "0"
        Wdata = "HLLHHLLH, LHHLLHHL"

        If FnTestSetup(xSerRS232) = PASSED Then
            Echo(FormatResults(True, "Serial/RS232 Setup", "BUS-02-001"))
            s232 = PASSED
            ResetBustoDefault(xSerRS232)
        Else
            Echo(FormatResults(False, "Serial/RS232 Setup", "BUS-02-001"))
            TestSerialBUS = FAILED
            ResetBustoDefault(xSerRS232)
        End If

        'test the RS422 mode
        Wparity = "EVEN"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8"
        Wmaxt = "2s"
        Wterm = "OFF"
        Wspec = "RS_422"
        Wdelay = "0"
        Wdata = "LHHLLHHL, HLLHHLLH"

        If FnTestSetup(xSerRS422) = PASSED Then
            Echo(FormatResults(True, "Serial/RS422 Setup", "BUS-02-003"))
            s422 = True
            ResetBustoDefault(xSerRS422)
        Else
            Echo(FormatResults(False, "Serial/RS232 Setup", "BUS-02-003"))
            TestSerialBUS = FAILED
            ResetBustoDefault(xSerRS422)
        End If

        'test the RS485 mode
        Wparity = "EVEN"
        Wbitrate = "9600Hz"
        Wstopbits = "2bits"
        Wwordlength = "8"
        Wmaxt = "2s"
        Wterm = "OFF"
        Wspec = "RS_485"
        Wdelay = "0"
        Wdata = "HLLHHLLH, LHHLLHHL"

        If FnTestSetup(xSerRS485) = PASSED Then
            Echo(FormatResults(True, "Serial/RS485 Setup", "BUS-02-005"))
            s485 = True
            ResetBustoDefault(xSerRS485)
        Else
            Echo(FormatResults(False, "Serial/RS285 Setup", "BUS-02-005"))
            TestSerialBUS = FAILED
            ResetBustoDefault(xSerRS485)
        End If

        If s485 Or s422 And s232 = True Then
            TestSerialBUS = PASSED
        Else
            TestSerialBUS = FAILED
        End If

    End Function

    Public Sub ResetBustoDefault(ByVal InstIndx As Short)

        Dim status As Integer

        Wparity = "NONE"
        Wbitrate = "9600Hz"
        Wstopbits = "1bits"
        Wwordlength = "8"

        Select Case InstIndx
            Case xSerRS232
                'test the RS232 mode xSerRS232%
                Wmaxt = "2s"
                Wterm = "OFF"
                Wspec = "RS_232"
                Wdelay = "0"
                Wdata = "HLLHHLLH, LHHLLHHL"

                status = FnComRemove(InstIndx)
                status = FnComSetup(InstIndx)


            Case xSerRS422
                'test the RS422 mode xSerRS422%
                Wmaxt = "2s"
                Wterm = "OFF"
                Wspec = "RS_422"
                Wdelay = "0"
                Wdata = "LHHLLHHL, HLLHHLLH"

                status = FnComRemove(InstIndx)
                status = FnComSetup(InstIndx)

            Case xSerRS485
                'test the RS485 mode xSerRS485%
                Wmaxt = "2s"
                Wterm = "OFF"
                Wspec = "RS_485"
                Wdelay = "0"
                Wdata = "HLLHHLLH, LHHLLHHL"

                status = FnComRemove(InstIndx)
                status = FnComSetup(InstIndx)

            Case xRS232
            Case xRS422


        End Select

    End Sub


    Public Function TestCOM1() As Short

        InitBusVar()
        Dim response As String = Space(4096)
        Dim status As Integer = 0

        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                 "              <SignalResourceName>COM_1</SignalResourceName>\n" &
                 "              <Level>2</Level>\n" &
                 "</AtXmlIssueIst>\n", Response, 4096)

        If (response.Contains("status clean")) Then
            TestCOM1 = True
        Else
            TestCOM1 = False
        End If

    End Function




    Public Function TestCom2() As Short


        InitBusVar()
        Dim response As String = Space(4096)
        Dim status As Integer = 0

        status = atxml_IssueIst("<AtXmlIssueIst>\n" &
                 "              <SignalResourceName>COM_2</SignalResourceName>\n" &
                 "              <Level>2</Level>\n" &
                 "</AtXmlIssueIst>\n", response, 4096)

        If (response.Contains("status clean")) Then
            TestCom2 = True
        Else
            TestCom2 = False
        End If
    End Function


    Private Function FnTestSetup(ByVal target As Short) As Integer

        Dim status As Integer

        'test com1 or com2 setup
        Select Case target
            Case xRS422, xRS232
                status = FnComRemove(target)
                status = FnComSetup(target)
                delay(2)
                status = FnComFetchConfig(target)

                If Rparity = Wparity And Rbitrate = Wbitrate And Rstopbits = Wstopbits And Rmaxt = Wmaxt And Rwordlength = Wwordlength Then
                    FnTestSetup = PASSED
                Else
                    FnTestSetup = FAILED
                End If

                'test the serial bus setup
            Case xSerRS232, xSerRS422, xSerRS485
                status = FnComRemove(target)
                status = FnComSetup(target)
                delay(3)
                status = FnComFetchConfig(target)

                If Rparity = Wparity And Rbitrate = Wbitrate And Rstopbits = Wstopbits And Rwordlength = Wwordlength And Rmaxt = Wmaxt And Rdelay = Wdelay And Rspec = Wspec Then
                    FnTestSetup = PASSED
                Else
                    FnTestSetup = FAILED
                End If

                'test the ethernet ports setup
            Case xGBitEth1, xGBitEth2
                status = FnComRemove(target)
                delay(1)
                status = FnComSetup(target)
                delay(2)
                status = FnComFetchConfig(target)

                If Rspec = Wspec And Rmaxt = Wmaxt And RlocalIP = WlocalIP And Rmask = Wmask And Rgateway = Wgateway And RremoteIp = WremoteIp And Rremport = Wremport Then
                    FnTestSetup = PASSED
                Else
                    FnTestSetup = FAILED
                End If
        End Select
    End Function

    Function FnComFetchConfig(ByVal target As Short) As Integer
        '  ErrStatus = FnComFetchConfig(xRS232)' xRS422%,xSerRS232%,xSerRS422%,xSerRS485%, xGbitEth1, xGbitEth2

        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String

        Dim tempStr As New StringBuilder(Space(255), 255)
        Dim tempDbl As Double
        Dim tempInt As Short

        ' Com1 or Com2 or Serial
        If target = xRS232 Or target = xRS422 Or target = xSerRS232 Or target = xSerRS422 Or target = xSerRS485 Then
            ' Fetch
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription>" & "<SignalAction>Fetch</SignalAction>" & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & "<SignalSnippit>" & "  <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & "    <" & pName(target) & " name=""exchange"" attribute=""config"" />" & "  </Signal>" & "</SignalSnippit>" & "</AtXmlSignalDescription>"
            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
            FnComFetchConfig = status
            If status <> 0 Then
                MsgBox("Error - Fetch " + Str(status))
            End If

            Rparity = ""
            If InStr(Response, "parity") Then
                status = atxml_GetStringValue(Response, "parity", tempStr.ToString(), 255)
                If status <> 0 Then
                    MsgBox("Error - Get parity " + Str(status))
                End If
                Rparity = FnTrim(tempStr.ToString())
            End If

            Rbitrate = ""
            If InStr(Response, "baud_rate") Then
                tempDbl = 0
                status = atxml_GetSingleDblValue(Response, "baud_rate", tempDbl)
                If status <> 0 Then
                    MsgBox("Error - get baud_rate " + Str(status))
                End If
                Rbitrate = CStr(tempDbl) & "Hz"
            End If

            Rstopbits = ""
            If InStr(Response, "stop_bits") Then
                tempDbl = 0
                status = atxml_GetSingleDblValue(Response, "stop_bits", tempDbl)
                If status <> 0 Then
                    MsgBox("Error - get stop_bits " + Str(status))
                End If
                Rstopbits = CStr(tempDbl) & "bits"
            End If

            Rmaxt = ""
            If InStr(Response, "maxTime") Then
                tempDbl = 0
                status = atxml_GetSingleDblValue(Response, "maxTime", tempDbl)
                If status <> 0 Then
                    MsgBox("Error - get maxTime " + Str(status))
                End If
                Rmaxt = CStr(tempDbl) & "s"
            End If

            Rwordlength = ""
            If InStr(Response, "wordLength") Then
                tempInt = 0
                status = atxml_GetSingleIntValue(Response, "wordLength", tempInt)
                If status <> 0 Then
                    MsgBox("Error - get wordLength" + Str(status))
                End If
                Rwordlength = CStr(tempInt)
            End If

            Rspec = ""
            If InStr(Response, "spec") Then
                tempStr = New StringBuilder("")
                status = atxml_GetStringValue(Response, "spec", tempStr.ToString(), 255)
                Rspec = FnTrim(tempStr.ToString())
                If status <> 0 Then
                    MsgBox("Error - get spec " + Str(status))
                End If
            End If

            If (target = xSerRS232) Or (target = xSerRS422) Or (target = xSerRS485) Then

                Rterm = ""
                If InStr(Response, "terminated") Then
                    tempStr = New StringBuilder("")
                    status = atxml_GetStringValue(Response, "terminated", tempStr.ToString(), 255)
                    Rterm = FnTrim(tempStr.ToString())
                    If status <> 0 Then
                        MsgBox("Error - get terminated " + Str(status))
                    End If
                End If

                Rdelay = ""
                If InStr(Response, "delay") Then
                    tempDbl = 0
                    status = atxml_GetSingleDblValue(Response, "delay", tempDbl)
                    Rdelay = Trim(Str(tempDbl))
                    If status <> 0 Then
                        MsgBox("Error - get delay " + Str(status))
                    End If
                End If
            End If

            'Fetch Ethernet settings
        ElseIf target = xGBitEth1 Or target = xGBitEth2 Then
            Response = Space(4096)
            ' initialize resource name
            XmlBuf = "<AtXmlSignalDescription>" & "<SignalAction>Fetch</SignalAction>" & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & "<SignalSnippit>" & "  <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & "     <" & Wspec & " name=""exchange"" attribute=""config"" />" & "  </Signal>" & "</SignalSnippit>" & "</AtXmlSignalDescription>"
            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

            Rspec = ""
            If InStr(Response, "spec") Then
                tempStr = New StringBuilder("")
                status = atxml_GetStringValue(Response, "spec", tempStr.ToString(), 255)
                Rspec = FnTrim(tempStr.ToString())
            End If

            Rmaxt = ""
            If InStr(Response, "maxTime") Then
                tempDbl = 0
                status = atxml_GetSingleDblValue(Response, "maxTime", tempDbl)
                Rmaxt = CStr(tempDbl) + "s"
            End If

            RlocalIP = ""
            If InStr(Response, "localIP") Then
                tempStr = New StringBuilder("")
                status = atxml_GetStringValue(Response, "localIP", tempStr.ToString(), 255)
                RlocalIP = FnTrim(tempStr.ToString())
            End If

            Rmask = ""
            If InStr(Response, "localSubnetMask") Then
                tempStr = New StringBuilder("")
                status = atxml_GetStringValue(Response, "localSubnetMask", tempStr.ToString(), 255)
                Rmask = FnTrim(tempStr.ToString())
            End If

            Rgateway = ""
            If InStr(Response, "localGateway") Then
                tempStr = New StringBuilder("")
                status = atxml_GetStringValue(Response, "localGateway", tempStr.ToString(), 255)
                Rgateway = FnTrim(tempStr.ToString())
            End If

            RremoteIp = ""
            If InStr(Response, "remoteIP") Then
                tempStr = New StringBuilder("")
                status = atxml_GetStringValue(Response, "remoteIP", tempStr.ToString(), 255)
                RremoteIp = FnTrim(tempStr.ToString())
            End If

            Rremport = ""
            If InStr(Response, "remotePort") Then
                tempInt = 0
                status = atxml_GetSingleIntValue(Response, "remotePort", tempInt)
                Rremport = CStr(tempInt)
            End If

        End If
    End Function



    Function FnComRemove(ByVal target As Short) As Integer
        '  ErrStatus = FnComRemove(xRS232)
        'Removes COM 1 from kernel
        Dim status As Integer
        Dim XmlBuf As String = Space(4096)
        Dim Response As String = Space(4096)

        If target = xRS232 Or target = xRS422 Then
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription>" & vbCrLf & "<SignalAction>Remove</SignalAction>" & vbCrLf & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf & "<SignalSnippit>" & vbCrLf & "  <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf & "    <" & pName(target) & " name=""exchange"" parity=""" & Wparity & """ baud_rate=""" & Wbitrate & """ stop_bits=""" & Wstopbits & """ maxTime=""" & Wmaxt & """ wordLength=""" & Wwordlength & """ />" & vbCrLf & "  </Signal>" & vbCrLf & "</SignalSnippit>" & vbCrLf & "</AtXmlSignalDescription>"

        ElseIf target = xSerRS232 Or target = xSerRS422 Or target = xSerRS485 Then
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription>" & vbCrLf & "<SignalAction>Remove</SignalAction>" & vbCrLf & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf & "<SignalSnippit>" & vbCrLf & "  <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf & "    <" & pName(target) & " name=""exchange"" parity=""" & Wparity & """ baud_rate=""" & Wbitrate & """ stop_bits=""" & Wstopbits & """ maxTime=""" & Wmaxt & """ wordLength=""" & Wwordlength & """ delay=""" & Wdelay & """ terminate=""" & Wterm & """ />" & vbCrLf & "  </Signal>" & vbCrLf & "</SignalSnippit>" & vbCrLf & "</AtXmlSignalDescription>"

        ElseIf target = xGBitEth1 Or target = xGBitEth2 Then
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf & "   <SignalAction>Remove</SignalAction>" & vbCrLf & "   <SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf & "   <SignalSnippit>" & vbCrLf & "       <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf & "           <" & Wspec & " name=""exchange"" maxTime=""" & Wmaxt & """ localIP=""" & WlocalIP & """ localSubnetMask=""" & Wmask & """ localGateway=""" & Wgateway & """ remoteIP=""" & WremoteIp & """ remotePort=""" & Wremport & """ />" & vbCrLf & "       </Signal>" & vbCrLf & "   </SignalSnippit>" & vbCrLf & "</AtXmlSignalDescription>"

        End If
        status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
        FnComRemove = status

    End Function



    Function FnComSetup(ByVal target As Short) As Integer
        ' ErrStatus = FnComSetup(xRS232)' xRS422%,xSerRS232%,xSerRS422%,xSerRS485%

        Dim status As Integer
        Dim XmlBuf As String
        Dim Response As String
        Dim DataSent As String

        'Com1 or Com2
        If target = xRS232 Or target = xRS422 Then
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription>" & vbCrLf & "<SignalAction>Setup</SignalAction>" & vbCrLf & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf & "<SignalSnippit>" & vbCrLf & "   <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf & "      <" & pName(target) & " name=""exchange"" parity=""" & Wparity & """ baud_rate=""" & Wbitrate & """ stop_bits=""" & Wstopbits & """ maxTime=""" & Wmaxt & """ wordLength=""" & Wwordlength & """ />" & vbCrLf & "   </Signal>" & vbCrLf & "</SignalSnippit>" & vbCrLf & "</AtXmlSignalDescription>"
            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)


            'Serial Bus Card
        ElseIf target = xSerRS232 Or target = xSerRS422 Or target = xSerRS485 Then
            'setup
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription>" & vbCrLf & "<SignalAction>Setup</SignalAction>" & vbCrLf & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf & "<SignalSnippit>" & vbCrLf & "  <Signal Name=""RS_SIGNAL"" Out=""exchange"">" & vbCrLf & "    <" & pName(target) & " name=""exchange"" parity=""" & Wparity & """ baud_rate=""" & Wbitrate & """ stop_bits=""" & Wstopbits & """ maxTime=""" & Wmaxt & """ wordLength=""" & Wwordlength & """ terminate=""" & Wterm & """ delay=""" & Wdelay & """ />" & vbCrLf & "  </Signal>" & vbCrLf & "</SignalSnippit>" & vbCrLf & "</AtXmlSignalDescription>"
            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)

            'Ethernet card
        ElseIf target = xGBitEth1 Or target = xGBitEth2 Then
            Response = Space(4096)
            XmlBuf = "<AtXmlSignalDescription> " & vbCrLf & "<SignalAction>Setup</SignalAction>" & vbCrLf & "<SignalResourceName>" & rName(target) & "</SignalResourceName>" & vbCrLf & "<SignalSnippit>" & vbCrLf & "  <Signal Name=""ETHERNET_SIGNAL"" Out=""exchange"">" & vbCrLf & "     <" & Wspec & " name=""exchange"" maxTime=""" & Wmaxt & """ localIP=""" & WlocalIP & """ localSubnetMask=""" & Wmask & """ localGateway=""" & Wgateway & """ remoteIP=""" & WremoteIp & """ remotePort=""" & Wremport & """ />" & vbCrLf & "  </Signal>" & vbCrLf & "</SignalSnippit>" & vbCrLf & "</AtXmlSignalDescription>"
            status = atxml_IssueSignal(XmlBuf, Response, MAX_XML_SIZE)
        End If
        FnComSetup = status

    End Function


    Function FnTrim(ByRef temp As String) As String
        FnTrim = ""

        ' this routine will trim chr = 0 from the string
        Dim X As String
        Dim Newtemp As String = ""

        Dim i As Integer
        temp = Trim(Convert.ToString(temp)) ' get rid of the spaces on each end

        i = 1
        For i = 1 To Convert.ToString(temp).Length
            X = Mid(Convert.ToString(temp), i, 1)
            If X <> Convert.ToString(Chr(0)) Then
                Newtemp += X
            End If
        Next i

        FnTrim = Newtemp


    End Function

    Function GatherIniFileInformation(ByVal lpApplicationName As String, ByVal lpKeyName As String, ByVal lpDefault As String) As String
        GatherIniFileInformation = ""
        '************************************************************
        '* ManTech Test Systems Software Module                     *
        '************************************************************
        '* Nomenclature   : System Monitor  [SystemStartUp]         *
        '* Written By     : David W. Hartley                        *
        '*    DESCRIPTION:                                          *
        '*     Finds a value on in the TETS.INI File                *
        '*    PARAMETERS:                                           *
        '*     lpApplicationName$ -[Application] in TETS.INI File   *
        '*     lpKeyName$ - KEYNAME= in TETS.INI File               *
        '*     lpDefault$ - Default value to return if not found    *
        '*    RETURNS                                               *
        '*     String containing the key value or the lpDefault     *
        '*    EXAMPLE:                                              *
        '*     FilePath$ = GatherIniFileInformation("Heading", ...  *
        '*      ..."MY_FILE", "")                                   *
        '************************************************************

        Dim lpReturnedString As New StringBuilder(255) 'Return Buffer
        Dim nSize As Integer 'Return Buffer Size
        Dim lpFileName As String 'INI File Key Name "Key=?"
        Dim ReturnValue As Integer 'Return Value Buffer
        Dim FileNameInfo As String 'Formatted Return String
        Dim lpString As String 'String to write to INI File

        lpFileName = Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\ATS\", "IniFilePath", Nothing)

        nSize = 255
        FileNameInfo = ""
        'Find File Locations
        Try
            ReturnValue = GetPrivateProfileString(lpApplicationName, lpKeyName, lpDefault, lpReturnedString, nSize, lpFileName)
        Catch ex As Exception
            MessageBox.Show("Exception: " & ex.ToString())
        End Try

        FileNameInfo = Trim(lpReturnedString.ToString())
        FileNameInfo = Mid(FileNameInfo, 1, Len(FileNameInfo))
        'If File Locations Missing, then create empty keys
        If FileNameInfo = lpDefault + Convert.ToString(Chr(0)) Or FileNameInfo = lpDefault Then
            lpString = Trim(lpDefault)
            ReturnValue = WritePrivateProfileString(lpApplicationName, lpKeyName, lpString, lpFileName)
        End If

        'Return Information In INI File
        GatherIniFileInformation = FileNameInfo

    End Function

    Public Function ObjectToDouble(ByVal v As Object) As Double
        Dim d As Double
        If TypeOf v Is String Then
            d = Val(v)
        ElseIf TypeOf v Is DateTime Then
            d = CType(v, DateTime).ToOADate()
        Else
            d = Convert.ToDouble(v)
        End If
        Return d
    End Function

    Public ReadOnly Property AppPrevInstance() As Boolean
        Get
            AppPrevInstance = False
        End Get
    End Property

    Public Function CompareErrNumber(ByVal sConditon As String, ByVal Value As Integer) As Boolean
        Dim te As Integer = Err.Number
        ' te = xFun(te)

        Select Case sConditon
            Case "<=" : Return te <= Value
            Case ">=" : Return te >= Value
            Case "<>" : Return te <> Value
            Case "=" : Return te = Value
            Case ">" : Return te > Value
            Case "<" : Return te < Value
            Case Else
                Return False    ' - ???
        End Select

    End Function

    Public Sub ResumeNext(Optional ByVal sFunction As String = "")
        ' ...
    End Sub

End Module