// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using VEO2_Library;

namespace NAM
{
    static class Program
    {
        #region Constants

        // Program Unique Debug Constants
        public const string DEBUG_FILE   = "C:\\APS\\DATA\\DEBUGIT_VEO2NAM";
        public const string DEBUG_RECORD = "C:\\APS\\DATA\\VEO2NAM_DEBUG.txt";

        // ATLAS argument positions
        const int ATLAS_ARG  = 0;
        const int ACTION_ARG = 0;
        const int OPTION_ARG = 1;
        const int RETURN_ARG = 2;

        const int MIN_ARGS = 1;
        const int MIN_ARGS_ATLAS = 3;

        // Actions
        const string BIT_GET           = "BIT_GET";
        const string CAL_VALUE_GET     = "CAL_VALUE_GET";
        const string STATUS_GET        = "STATUS_GET";
        const string PARTNUM_GET       = "PARTNUM_GET";
        const string SERNUM_GET        = "SERNUM_GET";
        const string MODE_SET          = "MODE_SET";
        const string SENSOR_SET        = "SENSOR_SET";
        const string SOURCE_SET        = "SOURCE_SET";
        const string TARGET_SET        = "TARGET_SET";
        const string LASER_DIODE_SET   = "LASER_DIODE_SET";
        const string LASER_TEST_SET    = "LASER_TEST_SET";
        const string LASER_TRIGGER_SET = "LASER_TRIGGER_SET";
        const string LASER_OPERATE_SET = "LASER_OPERATE_SET";
        const string LARRS_AZ_SET      = "LARRS_AZ_SET";
        const string LARRS_EL_SET      = "LARRS_EL_SET";
        const string LARRS_POL_SET     = "LARRS_POL_SET";
        const string LIGHT_RAD_SET     = "LIGHT_RAD_SET";
        const string IR_ABS_TEMP_SET   = "IR_ABS_TEMP_SET";
        const string IR_DIFF_TEMP_SET  = "IR_DIFF_TEMP_SET";
        const string CAMERA_SET        = "CAMERA_SET";

        // Error Codes
        const int DLL_MISS  = -100;
        const int VM_FAIL   = -101;
        const int ARG_NUM   = -102;
        const int ARG_RANGE = -103;
        const int ARG_TYPE  = -104;

        #endregion

        #region Variables

        // Assembly Info
        static readonly string AppTitle = typeof(Program).Assembly.GetName().Name.ToString();
        static readonly string AppVer   = typeof(Program).Assembly.GetName().Version.ToString();
        static readonly string AppDesc  = typeof(Program).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault().Description;


        /// <summary>
        /// Boolean value identifies whether or not to output debug info
        /// </summary>
        public static readonly bool debugMode = Debug.IsDebug();

        /// <summary>
        /// ATLAS virutal memory file
        /// </summary>
        static string vmFile = String.Empty;

        /// <summary>
        /// Boolean value identifies whether or not this is an ATLAS execution
        /// </summary>
        public static bool bATLAS = false;

        /// <summary>
        /// Integer value is 1 for ATLAS else 0, this is used to shift arguments
        /// </summary>
        static int execMode = 0;

        /// <summary>
        /// Return parameter (int)
        /// </summary>
        static int returnArgAddr = -1;

        /// <summary>
        /// Action to perform
        /// </summary>
        static string actionArg = String.Empty;

        /// <summary>
        /// Option parameter (int)
        /// </summary>
        static int optionArg = -1;

        /// <summary>
        /// Option parameter (float)
        /// </summary>
        static float optionArg_flt = -1;

        /// <summary>
        /// ATLAS boolean return value 
        /// </summary>
        static bool boolRetVal = false;

        /// <summary>
        /// ATLAS integer return value
        /// </summary>
        static int intRetVal = -1;

        /// <summary>
        /// ATLAS string return value
        /// </summary>
        static string strRetVal = string.Empty;

        /// <summary>
        /// ATLAS float return value
        /// </summary>
        static float fltRetVal = -1;

        #endregion

        // Create instance of ATLAS
        static ATLAS atlas = new ATLAS();

        [STAThread]
        static int Main(string[] args)
        {
            Application.EnableVisualStyles();

            int returnStatus       = 0;
            int actionArgAddr      = -1;
            int optionArgAddr      = -1;

            string[] argAddr = new string[args.Count()];

            // Determine if this was executed from ATLAS program
            if (args.Count() > 0)
            {
                if (debugMode) { Debug.WriteDebugInfo("Checking Execution Mode"); }
                bATLAS = atlas.IsATLAS(args[ATLAS_ARG]);
            }

            if (bATLAS)
            {
                if (debugMode) { Debug.WriteDebugInfo("Instance was executed from ATLAS"); }

                // Hide the console window
                IntPtr hWnd = Kernel32.GetConsole();
                User32.ShowConsole(hWnd, User32.Console.Visibility.Hide);
            }

            // Display App Header Info
            DisplayHeader();

            // Create debug file
            if (debugMode) { Debug.CreateDebugFile(); }

            // Check to see if minimum number of arguments was provided
            if (CheckArraySize(args, MIN_ARGS) == ARG_NUM)
            {
                // Not enough arguments provided, show help message
                ShowHelp();
                DisplayErrMsg(ARG_NUM);
                return ARG_NUM;
            }

            // Create Instance of VEO2_Client (uses socket)
            VEO2_Client veo2Client = new VEO2_Client();

            // Create Instance of VEO2 (uses veo2.dll)
            
            if (bATLAS)
            {
                if (!atlas.dllExists())
                {
                    DisplayErrMsg(DLL_MISS);
                    return DLL_MISS;
                }

                execMode = 1;

                // Ensure correct number of arguments were passed for ATLAS execution
                if (CheckArraySize(args, MIN_ARGS_ATLAS) == ARG_NUM)
                {
                    DisplayErrMsg(ARG_NUM);
                    return ARG_NUM;
                }

                // this array will be used to store the argument pointers passed from ATLAS
                for (int i = 0; i < args.Count(); i++)
                {
                    argAddr[i] = args[i];
                }

                // Get virtual memory filename
                vmFile = args[ATLAS_ARG];

                // Open VM file to interface with ATLAS program
                if ((returnStatus = atlas.OpenATLASvm(vmFile)) < 0)
                {
                    DisplayErrMsg(VM_FAIL);
                    return VM_FAIL;
                }

                if (debugMode) { Debug.WriteDebugInfo("ShowWarning() Executed"); }

                // Get address for action argument
                actionArgAddr = atlas.GetATLASArgAddr(args[ACTION_ARG + execMode]);

                // Replace args[] address values with corresponding values passed from ATLAS program
                if (debugMode) { Debug.WriteDebugInfo("Getting args from ATLAS"); }
                for (int i = 1; i < args.Count(); i++)
                {
                    if (debugMode) { Debug.WriteDebugInfo("args value: " + args[i].ToString()); }
                    args[i] = atlas.GetArgsATLAS(args, i);
                    if (debugMode) { Debug.WriteDebugInfo("args value: " + args[i].ToString()); }
                    if (debugMode) { Debug.WriteDebugInfo("arg address value: " + argAddr[i].ToString()); }
                }
            }

            // Get arguments and perform action
            if (bATLAS)
            {
                returnArgAddr = atlas.GetATLASArgAddr(argAddr[RETURN_ARG + execMode]);
            }

            // Collect arguments and validate values
            if ((returnStatus = ValidateArgs(args)) != 0)
            {
                DisplayErrMsg(returnStatus);
                return returnStatus;
            }

            // Display Parameter Info to be used in measurement
            if (!bATLAS) { DisplayParameters(); }

            // Get remaining argguments and perform action
            switch (actionArg)
                {
                case BIT_GET:
                    intRetVal = VEO2.BIT_Data_Get();
                    break;

                case CAL_VALUE_GET:

                    switch (optionArg)
                    {
                        case VEO2.CAL1064:
                            fltRetVal = Convert.ToSingle(VEO2.LaserEnergyCal1064);
                            break;

                        case VEO2.CAL1540:
                            fltRetVal = Convert.ToSingle(VEO2.LaserEnergyCal1540);
                            break;

                        case VEO2.CAL1570:
                            fltRetVal = Convert.ToSingle(VEO2.LaserEnergyCal1570);
                            break;
                    }
                    break;

                case STATUS_GET:
                    intRetVal = VEO2.Status_Get();
                    break;

                case PARTNUM_GET:
                    // Uses Socket to connect to silent command port of VEO2

                    strRetVal = veo2Client.GetPartNumber();
                    break;

                case SERNUM_GET:
                    // Uses Socket to connect to silent command port of VEO2

                    strRetVal = veo2Client.GetSerialNumber();
                    break;

                case MODE_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.Mode_Set(optionArg);
                    break;

                case SENSOR_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.Sensor_Set(optionArg);
                    break;

                case SOURCE_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.Source_Set(optionArg);
                    break;

                case TARGET_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.Target_Set(optionArg);
                    break;

                case LASER_DIODE_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LaserDiode_Set(optionArg);
                    break;

                case LASER_TEST_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LaserTest_Set(optionArg);
                    break;


                case LASER_TRIGGER_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LaserTrigger_Set(optionArg);
                    break;

                case LASER_OPERATE_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LaserOperation_Set(optionArg);
                    break;

                case LARRS_AZ_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LARRS_AZ_Set(optionArg);
                    break;

                case LARRS_EL_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LARRS_EL_Set(optionArg);
                    break;

                case LARRS_POL_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.LARRS_Polarizer_Set(optionArg);
                    break;

                case LIGHT_RAD_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.Radiance_Set(optionArg_flt);
                    break;

                case IR_ABS_TEMP_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }
                    intRetVal = VEO2.AbsoluteTemp_Set(optionArg_flt);
                    break;

                case IR_DIFF_TEMP_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }
                    intRetVal = VEO2.DifferentialTemp_Set(optionArg_flt);
                    break;

                case CAMERA_SET:
                    if (bATLAS)
                    {
                        if ((returnStatus = CheckArraySize(args, 4)) == -1)
                        {
                            return returnStatus;
                        }
                    }

                    intRetVal = VEO2.CameraPower_Set(optionArg);
                    break;

                default:
                    break;
            }

            veo2Client = null;
            VEO2.ShutDown();
            //veo2 = null;

            ProcessResults();

            if (bATLAS)
            {
                if (debugMode) { Debug.WriteDebugInfo("Closing virtual memory file"); }
                returnStatus = atlas.CloseATLASvm();
                atlas = null;
            }
            return returnStatus;
        }

        /// <summary>
        /// Returns True if string value is numeric
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        static bool IsNumeric(string text)
        {
            int inum = -1;
            float fnum = -1f;
            bool stat = false;

            if (int.TryParse(text, out inum))
            {
                stat = true;
            }
            else if (float.TryParse(text, out fnum))
            {
                stat = true;
            }

            return stat;
        }

        /// <summary>
        /// Validate number of elements in array
        /// </summary>
        /// <param name="inputArgs"></param>
        /// <param name="expectedCount"></param>
        /// <returns></returns>
        static int CheckArraySize(string[] inputArry, int elementCount)
        {
            int iStatus = 0;

            if (inputArry.Count() < elementCount)
            {
                iStatus = ARG_NUM;
            }
            return iStatus;
        }

        /// <summary>
        /// Display App Title, Version, & Description
        /// </summary>
        static void DisplayHeader()
        {
            if (!bATLAS)
            {
                Console.Clear();
                Console.Title = AppTitle + " v" + AppVer;
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(AppTitle + " v" + AppVer + "\n\n");
                Console.Write("Description:  " + AppDesc + "\n\n");
            }
        }

        /// <summary>
        /// Log and/or Display Error Messages
        /// </summary>
        /// <param name="Err"></param>
        /// <param name="txt1"></param>
        /// <param name="txt2"></param>
        static void DisplayErrMsg(int Err, string txt1 = "", string txt2 = "")
        {
            string errMsg = "";

            // Error Messages
            const string ERR_DLL = "DLL Not Found";
            const string ERR_VM = "Failed to Open ATLAS Virtual Memory File";
            const string ERR_ARGNUM = "Improper Number of Arguments Provided";
            const string ERR_ARGRNG = "Argument(s) Out of Range: ";
            const string ERR_TYPE = "Improper Data Type";

            switch (Err)
            {
                case DLL_MISS:
                    errMsg = ERR_DLL;
                    break;
                case VM_FAIL:
                    errMsg = ERR_VM;
                    break;
                case ARG_NUM:
                    errMsg = ERR_ARGNUM;
                    break;
                case ARG_RANGE:
                    errMsg = ERR_ARGRNG;
                    break;
                case ARG_TYPE:
                    errMsg = ERR_TYPE;
                    break;

                default:
                    break;
            }

            if (!bATLAS) { Console.WriteLine(errMsg); }
            if (debugMode) { Debug.WriteDebugInfo(ERR_ARGNUM); }
        }

        /// <summary>
        /// Sends results to ATLAS program or displays them to the console
        /// </summary>
        /// <param name="results"></param>
        static void ProcessResults()
        {
            if (bATLAS)
            {
                // Sending return values back to ATLAS program
                if (debugMode) { Debug.WriteDebugInfo("Sending return values back to ATLAS program"); }

                switch (actionArg)
                {
                    case PARTNUM_GET:
                        atlas.SetArgsATLAS(returnArgAddr, ATLAS.ATLAS_STRING, default(int), default(bool), strRetVal);
                        break;

                    case SERNUM_GET:
                        atlas.SetArgsATLAS(returnArgAddr, ATLAS.ATLAS_STRING, default(int), default(bool), strRetVal);
                        break;

                    case CAL_VALUE_GET:
                        atlas.SetArgsATLAS(returnArgAddr, ATLAS.ATLAS_REAL, default(int), default(bool), default(string), fltRetVal);
                        break;

                    default:
                        atlas.SetArgsATLAS(returnArgAddr, ATLAS.ATLAS_INT, intRetVal);
                        break;
                }
                
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Results");
                Console.WriteLine("_________________________");
                if (actionArg == PARTNUM_GET || actionArg == SERNUM_GET)
                {
                    Console.WriteLine("   Return String:  " + strRetVal);
                }
                else if (actionArg == CAL_VALUE_GET)
                {
                    Console.WriteLine("  Return Value:  " + fltRetVal);
                }
                else
                {
                    Console.WriteLine("   Return Value:  " + intRetVal);
                }
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        /// <summary>
        /// Display return value at console
        /// </summary>
        /// <param name="iOperation"></param>
        /// <param name="retVal"></param>
        static void ShowReturn(int iType, int retVal, bool boolVal = default(bool), string strVal = default(string), float fltVal = default(float))
        {
            switch (iType)
            {
                case ATLAS.ATLAS_INT:
                    Console.WriteLine(retVal.ToString());
                    break;

                case ATLAS.ATLAS_BOOL:
                    Console.WriteLine(boolVal.ToString());
                    break;

                case ATLAS.ATLAS_STRING:
                    Console.WriteLine(strVal);
                    break;

                default:
                    Console.WriteLine("undefined type");
                    break;
            }
        }

        /// <summary>
        /// Display help information at console
        /// </summary>
        static void ShowHelp()
        {

            if (Console.WindowWidth < 131)
            {
                if (Console.LargestWindowWidth >= 131)
                {
                    Console.WindowWidth = 131;
                }
                else
                {
                    Console.WindowWidth = Console.LargestWindowWidth;
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("      Usage:  ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(AppTitle + " ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("<Action> ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("<Option>\n\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("<Action>");
            Console.Write(" -  (string)  " + BIT_GET + ", " + CAL_VALUE_GET + ", " + STATUS_GET + ", " + PARTNUM_GET + ", " + SERNUM_GET + ", " + MODE_SET + ", " + SENSOR_SET + ", " +
                                  SOURCE_SET + ",\n           " + TARGET_SET + ", " + LASER_DIODE_SET + ", " + LASER_TEST_SET + ", " + LASER_TRIGGER_SET + ", " + LASER_OPERATE_SET + ", " + 
                                  LARRS_AZ_SET + ", " + LARRS_EL_SET + ",\n           " + LARRS_POL_SET + ", " + LIGHT_RAD_SET + ", " + IR_DIFF_TEMP_SET + ", " + IR_ABS_TEMP_SET + ", " +
                                  CAMERA_SET + " (required)\n\n");
            // BIT Get
            Console.Write(BIT_GET + "             ");
            Console.ResetColor();
            Console.Write("- Get VEO2 BIT Data\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + BIT_GET + "\n\n");
            Console.ResetColor();
            // Calibration Value Get
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(CAL_VALUE_GET + "       ");
            Console.ResetColor();
            Console.Write("- Get Calibration/Responsivity Value for Given Wavelength\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Wavelength: 10 thru 12 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + CAL_VALUE_GET + " 10\n\n");
            Console.ResetColor();
            // Status Get
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(STATUS_GET + "          ");
            Console.ResetColor();
            Console.Write("- Get Status of VEO2\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + STATUS_GET + "\n\n");
            Console.ResetColor();
            // Part Number Get
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(PARTNUM_GET + "         ");
            Console.ResetColor();
            Console.Write("- Get VEO2 Part Number\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + PARTNUM_GET + "\n\n");
            Console.ResetColor();
            // Serial Number Get
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(SERNUM_GET + "          ");
            Console.ResetColor();
            Console.Write("- Get VEO2 Serial Number\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + SERNUM_GET + "\n\n");
            Console.ResetColor();
            // Mode Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(MODE_SET + "            ");
            Console.ResetColor();
            Console.Write("- Set mode/configuration of VEO2\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Mode: 1 thru 6 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + MODE_SET + " 2\n\n");
            Console.ResetColor();
            // Sensor Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(SENSOR_SET + "          ");
            Console.ResetColor();
            Console.Write("- Set sensor stage position of VEO2\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Position: 1 thru 3 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + SENSOR_SET + " 1\n\n");
            Console.ResetColor();
            // Source Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(SOURCE_SET + "          ");
            Console.ResetColor();
            Console.Write("- Set source stage position of VEO2\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Position: 1 thru 3 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + SOURCE_SET + " 3\n\n");
            Console.ResetColor();
            // Target Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(TARGET_SET + "          ");
            Console.ResetColor();
            Console.Write("- Set source stage position of VEO2\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Position: 1 thru 14 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + TARGET_SET + " 12\n\n");
            Console.ResetColor();
            // LASER Diode Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LASER_DIODE_SET + "     ");
            Console.ResetColor();
            Console.Write("- Set PPLS Active Laser Diode\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Diode: 0 thru 2 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LASER_DIODE_SET + " 2\n\n");
            Console.ResetColor();
            // LASER Test Mode Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LASER_TEST_SET + "      ");
            Console.ResetColor();
            Console.Write("- Set PPLS Laser Test Mode\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Diode: 0 thru 1 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LASER_TEST_SET + " 1\n\n");
            Console.ResetColor();
            // LASER Trigger Source Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LASER_TRIGGER_SET + "   ");
            Console.ResetColor();
            Console.Write("- Set PPLS Laser Trigger Source\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Diode: 10 thru 12 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LASER_TRIGGER_SET + " 1\n\n");
            Console.ResetColor();
            // LASER Operation Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LASER_OPERATE_SET + "   ");
            Console.ResetColor();
            Console.Write("- Set PPLS Laser Operation\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Diode: 0 thru 1 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LASER_OPERATE_SET + " 1\n\n");
            Console.ResetColor();
            // LARRS AZ Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LARRS_AZ_SET + "        ");
            Console.ResetColor();
            Console.Write("- Set LARRS azimuth stage position\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Position: 0 thru 20000 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LARRS_AZ_SET + " 2500\n\n");
            Console.ResetColor();
            // LARRS EL Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LARRS_EL_SET + "        ");
            Console.ResetColor();
            Console.Write("- Set LARRS elevation stage position\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Position: 0 thru 20000 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LARRS_EL_SET + " 3000\n\n");
            Console.ResetColor();
            // LARRS POL Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LARRS_POL_SET + "       ");
            Console.ResetColor();
            Console.Write("- Set LARRS polarization stage position\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Position: 0 thru 360 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LARRS_POL_SET + " 180\n\n");
            Console.ResetColor();
            // Radiance Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LIGHT_RAD_SET + "       ");
            Console.ResetColor();
            Console.Write("- Set Visible Light Radiance\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Radiance: 0 thru 5000 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LIGHT_RAD_SET + " 3000\n\n");
            Console.ResetColor();
            // IR Absolute Temperature Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(IR_ABS_TEMP_SET + "     ");
            Console.ResetColor();
            Console.Write("- Set IR Absolute Temperature\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Temperature: 10 thru 60 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + IR_ABS_TEMP_SET + " 15\n\n");
            Console.ResetColor();
            // IR Differential Temperature Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(IR_DIFF_TEMP_SET + "    ");
            Console.ResetColor();
            Console.Write("- Set IR Differential Temperature\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Temperature: -15 thru 35 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + IR_DIFF_TEMP_SET + " 10\n\n");
            Console.ResetColor();
            // Camera Set
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(CAMERA_SET + "          ");
            Console.ResetColor();
            Console.Write("- Set VEO2 camera power\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("           <Option>");
            Console.ResetColor();
            Console.Write(" - Operation: 0 thru 1 (required)\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + CAMERA_SET + " 1\n\n");
            Console.ResetColor();

        }

        /// <summary>
        /// Validates arguments provided at commandline
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        static int ValidateArgs(string[] arguments)
        {

            // Action Argument
            if ((arguments[ACTION_ARG + execMode]) is string)
            {

                actionArg = (arguments[ACTION_ARG + execMode]).ToUpper();

                if (actionArg != BIT_GET && actionArg != CAL_VALUE_GET && actionArg != STATUS_GET && actionArg != PARTNUM_GET && actionArg != SERNUM_GET && actionArg != MODE_SET && 
                    actionArg != SENSOR_SET && actionArg != SOURCE_SET && actionArg != TARGET_SET && actionArg != LASER_DIODE_SET && actionArg != LASER_TEST_SET &&
                    actionArg != LASER_TRIGGER_SET && actionArg != LASER_OPERATE_SET && actionArg != LARRS_AZ_SET && actionArg != LARRS_EL_SET && actionArg != LARRS_POL_SET &&
                    actionArg != LIGHT_RAD_SET && actionArg != IR_ABS_TEMP_SET && actionArg != IR_DIFF_TEMP_SET && actionArg != CAMERA_SET)
                {
                    DisplayErrMsg(ARG_RANGE, " (" + ACTION_ARG + ") - " + actionArg);
                    return ARG_RANGE;
                }
            }
            else
            {
                DisplayErrMsg(ARG_TYPE, " (" + ACTION_ARG + ") - " + actionArg);
                return ARG_TYPE;
            }

            // Option Argument - only required for a set action
            if (actionArg.EndsWith("_SET") || actionArg == (CAL_VALUE_GET))
            {
                if (IsNumeric(arguments[OPTION_ARG + execMode]))
                {
                    // check action arg
                    if (actionArg == LIGHT_RAD_SET || actionArg == IR_ABS_TEMP_SET || actionArg == IR_DIFF_TEMP_SET)
                    {
                        optionArg_flt = float.Parse(arguments[OPTION_ARG + execMode]);
                    }
                    else
                    {
                        optionArg = Convert.ToInt32(arguments[OPTION_ARG + execMode]);
                    }
                }
                else
                {
                    DisplayErrMsg(ARG_TYPE, " (" + OPTION_ARG + ") - " + optionArg);
                    return ARG_TYPE;
                }
            }
            
            return 0;
        }

        /// <summary>
        /// Display the measurement parameters in formatted list
        /// </summary>
        static void DisplayParameters()
        {
            string actionDesc = "";
            string optionDesc = "";

            switch (actionArg)
            {
                case BIT_GET:
                    actionDesc = "BIT Status";
                    break;

                case CAL_VALUE_GET:
                    actionDesc = "Calibration Value";
                    switch (optionArg)
                    {
                        case VEO2.CAL1064:
                            optionDesc = "1064nm";
                            break;
                        case VEO2.CAL1540:
                            optionDesc = "1540nm";
                            break;
                        case VEO2.CAL1570:
                            optionDesc = "1570nm";
                            break;
                    }
                    break;

                case STATUS_GET:
                    actionDesc = "Status Byte Info";
                    break;

                case PARTNUM_GET:
                    actionDesc = "Part Number";
                    break;

                case SERNUM_GET:
                    actionDesc = "Serial Number";
                    break;

                case MODE_SET:
                    actionDesc = "System Configuration";

                    switch (optionArg)
                    {
                        case VEO2.NONE:
                            optionDesc = "None";
                            break;

                        case VEO2.BBODY_IR:
                            optionDesc = "InfraRed";
                            break;

                        case VEO2.VISIBLE:
                            optionDesc = "Visible";
                            break;

                        case VEO2.VISIBLEALIGN:
                            optionDesc = "Visible Alignment";
                            break;

                        case VEO2.LASER:
                            optionDesc = "Laser";
                            break;

                        case VEO2.LASERALIGN:
                            optionDesc = "Laser Alignment";
                            break;

                        default:
                            break;
                    }
                    break;

                case SENSOR_SET:
                    actionDesc = "Sensor Stage Position";

                    switch (optionArg)
                    {
                        case VEO2.NONE:
                            optionDesc = "None (Open Aperture)";
                            break;

                        case VEO2.ENERGY:
                            optionDesc = "Laser (Energy Probe)";
                            break;

                        case VEO2.CAMERA:
                            optionDesc = "Camera (Beam Splitter)";
                            break;

                        default:
                            break;
                    }
                    break;

                case SOURCE_SET:
                    actionDesc = "Source Stage Position";

                    switch (optionArg)
                    {
                        case VEO2.IR:
                            optionDesc = "InfraRed (Open Aperture)";
                            break;

                        case VEO2.LAS:
                            optionDesc = "Laser (Reflector)";
                            break;

                        case VEO2.VISIBLE:
                            optionDesc = "Visible (Fold Mirror)";
                            break;

                        default:
                            break;
                    }
                    break;

                case TARGET_SET:
                    actionDesc = "Target Wheel Position";

                    switch (optionArg)
                    {
                        case VEO2.OPEN:
                            optionDesc = "Open Aperture (0)";
                            break;

                        case VEO2.PINHOLE:
                            optionDesc = "Pinhole (1)";
                            break;

                        case VEO2.PIE:
                            optionDesc = "Pie (2)";
                            break;

                        case VEO2.BAR_5:
                            optionDesc = "5.00 Cyc/mrad 4 bar (3)";
                            break;

                        case VEO2.BAR_3_8:
                            optionDesc = "3.8325 Cyc/mrad 4 bar (4)";
                            break;

                        case VEO2.BAR_2_6:
                            optionDesc = "2.665 Cyc/mrad 4 bar (5)";
                            break;

                        case VEO2.BAR_1_5:
                            optionDesc = "1.4975 Cyc/mrad 4 bar (6)";
                            break;

                        case VEO2.BAR_0_3:
                            optionDesc = "0.33 Cyc/mrad 4 bar (7)";
                            break;

                        case VEO2.DIAGONAL:
                            optionDesc = "Diagonal Slit (8)";
                            break;

                        case VEO2.MULTI_PINHOLE:
                            optionDesc = "Multi-Pinhole (9)";
                            break;

                        case VEO2.CROSS:
                            optionDesc = "Alignment Cross (10)";
                            break;

                        case VEO2.RESOLUTION:
                            optionDesc = "USAF 1951, Groups 0-4 Resolution (11)";
                            break;

                        case VEO2.BAR_1_0:
                            optionDesc = "1.0 Cyc/mrad 4 bar (12)";
                            break;

                        case VEO2.BAR_0_6:
                            optionDesc = "0.66 Cyc/mrad 4 bar (13)";
                            break;

                        case VEO2.SQUARE:
                            optionDesc = "21 mrad Square";
                            break;

                        default:
                            break;
                    }

                    break;

                case LASER_DIODE_SET:
                    actionDesc = "Active Laser Diode";

                    switch (optionArg)
                    {
                        case VEO2.LASER1570:
                            optionDesc = "1570nm";
                            break;

                        case VEO2.LASER1540:
                            optionDesc = "1540nm";
                            break;

                        case VEO2.LASER1064:
                            optionDesc = "1064nm";
                            break;

                        default:
                            break;
                    }
                    break;

                case LASER_TEST_SET:
                    actionDesc = "Laser Test Mode";

                    switch (optionArg)
                    {
                        case VEO2.ON:
                            optionDesc = "On";
                            break;

                        case VEO2.OFF:
                            optionDesc = "Off";
                            break;

                        default:
                            break;
                    }
                    break;

                case LASER_TRIGGER_SET:
                    actionDesc = "Laser Trigger";

                    switch (optionArg)
                    {
                        case VEO2.ALIGNMENT:
                            optionDesc = "Alignment";
                            break;

                        case VEO2.FREERUN:
                            optionDesc = "Free Run";
                            break;

                        case VEO2.OPTICAL:
                            optionDesc = "Optical (Fast Detector)";
                            break;

                        case VEO2.EXTERNAL:
                            optionDesc = "External";
                            break;

                        case VEO2.CALIBRATE:
                            optionDesc = "Calibration";
                            break;

                        default:
                            break;
                    }
                    break;

                case LASER_OPERATE_SET:
                    actionDesc = "Laser Operation";

                    switch (optionArg)
                    {
                        case VEO2.ON:
                            optionDesc = "On";
                            break;

                        case VEO2.OFF:
                            optionDesc = "Off";
                            break;

                        default:
                            break;
                    }
                    break;

                case LARRS_AZ_SET:
                    actionDesc = "LARRS Azimuth Stage Position (absolute)";
                    optionDesc = optionArg.ToString() + " steps";
                    break;

                case LARRS_EL_SET:
                    actionDesc = "LARRS Elevation Stage Position (absolute)";
                    optionDesc = optionArg.ToString() + " steps";
                    break;

                case LARRS_POL_SET:
                    actionDesc = "LARS Polarization Stage Position (absolute)";
                    optionDesc = optionArg.ToString() + " degrees";
                    break;

                case LIGHT_RAD_SET:
                    actionDesc = "Visible Source Radiance Level";
                    optionDesc = optionArg_flt.ToString() + " uW/sr/cm2";
                    break;

                case IR_ABS_TEMP_SET:
                    actionDesc = "IR Absolute Temperature";
                    optionDesc = optionArg_flt.ToString() + " degrees C";
                    break;

                case IR_DIFF_TEMP_SET:
                    actionDesc = "IR Differential Temperature";
                    optionDesc = optionArg_flt.ToString() + " degrees C";
                    break;

                case CAMERA_SET:
                    actionDesc = "Camera Operation";

                    switch (optionArg)
                    {
                        case VEO2.ON:
                            optionDesc = "On";
                            break;

                        case VEO2.OFF:
                            optionDesc = "Off";
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

            if (actionArg.EndsWith("_GET"))
            {
                Console.WriteLine("Getting VEO2 " + actionDesc);
                if (actionArg == CAL_VALUE_GET)
                {
                    Console.WriteLine();
                    Console.WriteLine("Parameter " + optionDesc);
                }
                Console.WriteLine();
            }
            else if (actionArg.EndsWith("SET"))
            {
                Console.WriteLine("Setting VEO2 " + actionDesc);
                Console.WriteLine();
                Console.WriteLine("Parameter " + optionDesc);
                Console.WriteLine();
            }
        }
    }
}
