Option Strict Off
Option Explicit On

Module DiagMgr
    ' Copyright 1998 by Teradyne, Inc., Boston, MA
    '
    ' Module:   DM_Services.bas
    ' Creator:  Michel H. Pradieu
    '
    ' Abstract: This file contains the function prototypes of the routines in
    '           DiagMgr.dll exported for Visual Basic.  It is based on the
    '           following C header file:  DM_Services.h
    '
    ' Revision History:
    '
    ' Version    Date    Who   What
    '
    ' DM1.2.000  2/23/99 djl   Change DM_g/s/etDiagOutputDir to DM_g/s/etDiagnosticOutputDir
    ' DM1.1.006  1/12/99 DMD   Updated with new function prototypes:
    '                          DM_setDiagOutputDir, DM_getDiagOutputDir
    ' DM1.1.005  1/12/99 DMD   Updated with new function prototypes:
    '                          DM_setScreenDiagEnable,
    '                          DM_getScreenDiagEnable. Also added return
    '                          value for DM_getDiagnosticText, DM_getFaultInfo.
    ' DM1.1.003  12/8/98 DMD   Updated with new function prototypes:
    '                          DM_setDiagnosticOutputFile,
    '                          DM_get_DiagnosticOutputFile,
    '                          DM_getDiagnosticText,
    '                          DM_getFaultInfo.
    ' 1.0        980601  mhp   Creation
    '

    ' Note:  the format of the functions is:
    '   Declare Function functionName Lib "DiagMgr.dll" ([ByVal] parameterName As parameterType, ...) As Long
    '
    '   Every function declaration must be declared on a single line
    '

    ' typedef DiagFaultHandle DM_DiagFaultHandle;

    ' DM_getVersion
    '
    ' This function is used to obtain the version of the diagnostic manager.
    '
    ' Paremeters:
    '   version - should point to a buffer of at least 32 characters. gets
    '   filled in with the null terminated version string
    Declare Function DM_getVersion Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal version As String) As Integer

    Declare Function DM_diagnoseTest Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal testName As String, ByVal assemblyName As String, ByVal unitID As String, ByVal vi As Integer) As Integer

    Declare Function DM_diagnosticCompleted Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal diagnostic As Integer, ByVal status As Integer, ByRef diagnosisHandle As Integer) As Integer

    Declare Function DM_setDiagnosticEnable Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal diagnostic As Integer, ByVal enable As Integer) As Integer

    Declare Function DM_getDiagnosticEnable Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal diagnostic As Integer, ByRef enable As Integer) As Integer

    Declare Function DM_getDiagnosticAvailable Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal testName As String, ByVal diagnostic As Integer, ByRef available As Integer) As Integer

    'Declare Function DM_getProbeThroughTesterPin Lib "DiagMgr.dll" (ByRef enable As Long) As Long

    'Declare Function DM_setProbeThroughTesterPin Lib "DiagMgr.dll" (ByVal enable As Long) As Long



    ' DM_errorMessage
    '
    ' This function takes a DIAG status and returns the severity of the error,
    ' and the string describing the error.
    '
    ' INPUT PARAMETERS:
    '   status - DiagStatus value which is to be described
    '   maxSize - the size of the buffer
    '
    ' OUTPUT PARAMETERS:
    '   severity - a pointer to one character that describes the severity of the error:
    '    'W' - warning
    '    'E' - error
    '    'I' - informational
    '    'F' - fatal
    '   buffer - pointer to memoty of size maxSize, to be filled in with the description
    '            of the DIAG status
    '
    ' RETURN VALUES:
    '   DIAG_SUCCESS - error interpreted successfully
    '   DIAG_ERROR_NAME_NOT_FOUND - status was not recognized
    '   DIAG_WARN_BUFFER_TOO_SMALL - buffer was too small to fit all of the description.
    Declare Function DM_errorMessage Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal status As Integer, ByVal maxSize As Short, ByVal severity As String, ByVal buffer As String) As Integer


    Declare Function DM_setDiagnosticOutputFile Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal fileName As String) As Integer
    '
    '       PURPOSE:        To specify the name of the file in which to put
    '                       the ASCII diagnosis.
    '
    '       RETURN VALUE:   DIAG_SUCCESS, if service completed successfully.
    '                      DIAG_ERROR_FILE_CANNOT_BE_OPENED, if the file
    '                          was NULL or couldn't be opened. NULL will be
    '                          assigned to outfile, and default file name in
    '                          current working directory will be used instead
    '                          when diagnosis is performed. The default file
    '                          name is <test name>.dia.
    '                      DIAG_WARN_BUFFER_TOO_SMALL if the filename was longer
    '                          than DIAG_MAX_NAMELEN characters. In that case,
    '                          the filename is truncated.
    '
    '       REQUIREMENTS:   See invariants.
    '
    '       SIDE EFFECTS:   Allocates memory and sets global variable "outfile".
    '                       Frees existing filename. If a file of the given or
    '                      default name exists, will overwrite it with 0 bytes.
    '                      Will overwrite outfile with NULL if file was NULL or
    '                      couldn't be opened.
    '
    '       Description     The name can be any of the following:
    '
    '                       File name using default path ("test.dia").
    '                       File name using relative path ("mytest\\test.dia").
    '                       File name using absolute path ("C:\\tests\\mytest\test.dia").
    '
    '                       The file extension is NOT assumed. If you
    '                       specify a filename without an extension, a
    '                       file with no extension will be created. The
    '                      file name must not exceed DIAG_MAX_NAMELEN
    '                      characters in length.
    '

    Declare Function DM_getDiagnosticOutputFile Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByRef fileName As String) As Integer
    '
    '       PURPOSE:        To copy into the passed parameter the name of the file
    '                       into which the ASCII diagnosis is written.
    '
    '       RETURN VALUE:   DIAG_SUCCESS, if service completed successfully.
    '                      DIAG_ERROR_NULL_POINTER if fileName is NULL.
    '
    '       REQUIREMENTS:   If outfile is NULL, returns a null string.
    '                      Assumes the buffer is at least DIAG_MAX_NAMELEN+1
    '                      characters in length.
    '
    '       SIDE EFFECTS:   Reads global variable "output" and copies into string
    '                      buffer.
    '
    '       Description     The name can be any of the following:
    '
    '                       File name using default path ("test.dia").
    '                       File name using relative path ("mytest\\test.dia").
    '                       File name using absolute path ("C:\\tests\\mytest\test.dia").
    '

    Declare Function DM_getDiagnosisText Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal diagnostic As Integer, ByVal diagnosisMaxSize As Integer, ByRef diagnosis As String) As Integer
    '
    '       PURPOSE:        To put into a user-provided buffer the diagnosis string
    '                      of the most recently performed diagnostic of the given
    '                      type, if any.
    '
    '       RETURN VALUE:   DIAG_SUCCESS, if service completed successfully.
    '                       DIAG_WARN_BUFFER_TOO_SMALL if the buffer was too small.
    '                      In that case, the message is truncated.
    '                      DIAG_ERROR_UNKNOWN_DIAGNOSTIC if a bad diagnostic type
    '                      is passed.
    '                      DIAG_ERROR_NULL_POINTER if diagnosis is NULL.
    '                      DIAG_ERROR_FILE_CANNOT_BE_OPENED if the given diagnostic
    '                      file was not found or could not be opened. In this case,
    '                      a null string is returned.
    '
    '       REQUIREMENTS:   See invariants.
    '
    '       SIDE EFFECTS:   Writes into passed-in buffer.
    '
    '       Description     This is an overload of the existing function
    '                       DM_getDiagnosisText, which is specified to take
    '                       different parameters.
    '

    Declare Function DM_getFaultInfo Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal testName As String, ByVal diagnostic As Integer, ByVal maxMsgSize As Integer, ByRef faultStr As String, ByRef msgStr As String, ByVal prevFault As Integer, ByRef nextFault As Integer) As Integer

    '       PURPOSE:        To get the faulty object and associated fault
    '                       message.
    '
    '       RETURN VALUE:   DIAG_SUCCESS if successful, otherwise:
    '                       DIAG_WARN_BUFFER_TOO_SMALL if a buffer was too
    '                      small. In that case, the relevant string is
    '                      truncated.
    '                      DIAG_ERROR_UNKNOWN_DIAGNOSTIC if a bad diagnostic
    '                      type was passed.
    '                      DIAG_ERROR_NULL_POINTER if faultStr, msgStr, or
    '                      nextFault is NULL.
    '                      DIAG_ERROR_FILE_CANNOT_BE_OPENED if the relevant
    '                      diagnostic database was not found or could not be
    '                      opened.
    '                      DIAG_ERROR_FAILS_NOT_FOUND if failure information
    '                      could not be found in the database.
    '                      DIAG_ERROR_FILE_IO if there was an error reading
    '                      the file.
    '
    '                      For all errors but NULL_POINTER, "NONE" is returned
    '                      in faultStr and a null string is returned in msgStr.
    '                      The LAST call returns a nextFault of NULL along with
    '                      the last actual fault data.
    '
    '       REQUIREMENTS:   See invariants.
    '
    '       SIDE EFFECTS:   Writes into string buffers and into variable passed
    '                       in as "nextFault". Opens files and databases,
    '                      allocates memory, closes files and databases when
    '                      the last item is returned and *nextFault is set to
    '                      NULL.
    '
    '       Description     Caller calls routine with the given diagnostic type,
    '                       with the string buffers set to allocated memory,
    '                       and with nextFault set to the address of a variable
    '                       of type DM_DiagFaultHandle.
    '

    Declare Function DM_setScreenDiagEnable Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal value As Integer) As Integer

    '       PURPOSE:        To set a flag that allows the diagnosis to be printed
    '                       to the screen.
    '
    '       RETURN VALUE:   DIAG_ERROR_NAME_NOT_FOUND if value is out of range,
    '                      otherwise DIAG_SUCCESS.
    '
    '       REQUIREMENTS:   None.
    '
    '       SIDE EFFECTS:   Writes into static global ScreenDiagEnable.
    '
    '       Description     First checks the input value is in range, then sets it.

    Declare Function DM_getScreenDiagEnable Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByRef value_ptr As Integer) As Integer

    '       PURPOSE:        To get the current value of a flag that allows the
    '                       diagnosis to be printed to the screen.
    '
    '       RETURN VALUE:   DIAG_ERROR_NULL_POINTER if value_ptr is NULL,
    '                      DIAG_ERROR_NAME_NOT_FOUND if ScreenDiagEnable is out
    '                       of range, otherwise DIAG_SUCCESS.
    '
    '       REQUIREMENTS:   See invariants. ScreenDiagEnable must be in range.
    '
    '       SIDE EFFECTS:   Sets the variable pointed to by value_ptr to
    '                      DIAG_ENABLE or DIAG_DISABLE.
    '
    '       Description     Checks for null pointer and out-of-range value,
    '                       then sets output parameter.


    Declare Function DM_setDiagnosticOutputDir Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByVal dirName As String) As Integer

    '       PURPOSE:        To specify the directory in which to put the
    '                       diagnosis files.
    '
    '       RETURN VALUE:   DIAG_SUCCESS, if service completed successfully.
    '                      DIAG_ERROR_FILE_CANNOT_BE_OPENED, if the file
    '                          was NULL or couldn't be opened. NULL will be
    '                          assigned to outputDirectory, and the default
    '                          directory (.\) will be used instead.
    '                      DIAG_WARN_BUFFER_TOO_SMALL if the dirName was longer
    '                          than DIAG_MAX_NAMELEN characters. In that case,
    '                          dirName is truncated.
    '
    '       REQUIREMENTS:   See invariants. The file name must not exceed
    '                      DIAG_MAX_NAMELEN characters in length.
    '
    '       SIDE EFFECTS:   Allocates memory and sets global variable
    '                       "outputDirectory". Frees existing outputDirectory.
    '                      Will overwrite outputDirectory with NULL if dirName
    '                      was NULL.
    '
    '       Description     This routine interacts with the routine
    '                      DM_setDiagnosticOutputFile as follows:
    '
    '                      If DM_setDiagnosticOutputFile is called with a bare
    '                      filename, the diagnostic log file is placed in the
    '                      directory specified here.
    '
    '                      If DM_setDiagnosticOutputFile is called with a
    '                      full pathname, the diagnostic log file is placed
    '                      in the location given by the full pathname,
    '                      regardless of what directory is specified here.

    Declare Function DM_getDiagnosticOutputDir Lib "C:\Program Files\Teradyne\Diagnostics\bin\DiagMgr.dll" (ByRef dirName_ptr As String) As Integer

    '       PURPOSE:        To copy into the passed parameter the name of the
    '                       directory which the diagnosis output is placed.
    '
    '       RETURN VALUE:   DIAG_SUCCESS, if service completed successfully.
    '                      DIAG_ERROR_NULL_POINTER if dirName_ptr is NULL.
    '
    '       REQUIREMENTS:   If outputDirectory is NULL, returns a null string.
    '                      ASSUMES THE BUFFER IS AT LEAST DIAG_MAX_NAMELEN+1
    '                      CHARACTERS IN LENGTH.
    '
    '       SIDE EFFECTS:   Reads global variable "outputDirectory" and copies
    '                      string into string buffer.
    '
    '       Description
    '
End Module