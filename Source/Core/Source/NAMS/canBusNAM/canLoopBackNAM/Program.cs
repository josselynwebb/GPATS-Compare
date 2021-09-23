// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace NAM
{
    class Program
    {
        #region Constants

        public const string GUID     = "70a40832-99dd-40c9-8bdd-9b17b3d417b9";
        public const string PROCTYPE = "NAM";

        // Program Unique Debug Constants
        public const string DEBUG_FILE   = "C:\\APS\\DATA\\DEBUGIT_CANBUSNAM";
        public const string DEBUG_RECORD = "C:\\APS\\DATA\\CANBUSNAM_DEBUG.txt";

        // ATLAS argument positions
        const int ATLAS_ARG   = 0;
        const int ACTION_ARG  = 0;
        const string TXCHAN_ARG  = "/TXCH:";
        const string RXCHAN_ARG  = "/RXCH:";
        const string ASYNC_ARG   = "/ASYNC";
        const string TIMING_ARG  = "/TIME:";
        const string SAMPLES_ARG = "/SMPL:";
        const string FILTER_ARG  = "/FLTR:";
        const string CODE_ARG    = "/CODE:";
        const string MASK_ARG    = "/MASK:";
        const string TIMEOUT_ARG = "/TMO:";
        const string IDENT_ARG   = "/ID:";
        const string IO_ARG      = "/IO:";
        const string DATA_ARG    = "/DATA:";
        const string HIDE_ARG    = "/HIDE";
        const string RETURN_ARG  = "RTN_ARG";

        const int MIN_ARGS = 1;
        const int MIN_ARGS_ATLAS = 2;

        // Actions
        const string TX = "TRANSMIT";
        const string RX = "RECEIVE";
        const string LB = "LOOPBACK";

        // Error Codes
        const int DLL_MISS  = -100;
        const int DLL_FAIL  = -101;
        const int VM_FAIL   = -102;
        const int ARG_NUM   = -103;
        const int ARG_RANGE = -104;
        const int ARG_TYPE  = -105;
        const int CAN_FAIL  = -106;

        public const uint DLlNOTFOUND = 0x8007007E;

        #endregion

        #region Variables

        // Assembly Info
        static readonly string AppTitle = typeof(Program).Assembly.GetName().Name.ToString();
        static readonly string AppVer   = typeof(Program).Assembly.GetName().Version.ToString();
        static readonly string AppDesc  = typeof(Program).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false).OfType<AssemblyDescriptionAttribute>().FirstOrDefault().Description;


        /// <summary>
        /// Boolean value identifies whether or not to output debug info
        /// - value is overriden if AsyncRead is specified
        /// </summary>
        public static bool debugMode = Debug.IsDebug();

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
        /// Transmit Channel
        /// </summary>
        static int txChanArg = 1;

        /// <summary>
        /// Receive Channel
        /// </summary>
        static int rxChanArg = 2;

        /// <summary>
        /// Asynchronous Operation
        /// </summary>
        static bool asyncArg = false;

        /// <summary>
        /// Timing
        /// </summary>
        static int timingArg = 20000;

        /// <summary>
        /// Samples
        /// </summary>
        static int samplesArg = 0;

        /// <summary>
        /// Single-Filter
        /// </summary>
        static int filterArg = 1;

        /// <summary>
        /// Acceptance Code
        /// </summary>
        static uint accCodeArg = 6682;

        /// <summary>
        /// Acceptance Mask
        /// </summary>
        static uint accMaskArg = 4294967295;

        /// <summary>
        /// Time-Out
        /// </summary>
        static int timeOutArg = 2000;

        /// <summary>
        /// Identifier
        /// </summary>
        static uint identityArg = 65244;

        /// <summary>
        /// IO Flag
        /// </summary>
        static int ioArg = 1;

        /// <summary>
        /// Transmission Data
        /// </summary>
        static string dataArg = "FE,DC,BA,98,76,54,32,10";

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

        public static bool isHidden = false;

        #endregion

        // Create instance of ATLAS
        static ATLAS atlas = new ATLAS();

        [STAThread]
        static int Main(string[] args)
        {
            int returnStatus  = 0;
            int actionArgAddr = -1;
            int iCnt = 0;

            // Check if this process was spawned from an async receive command
            for (int i = 0; i < args.Count(); i++)
            {
                if (args[i].ToUpper().Contains(HIDE_ARG))
                {
                    isHidden = true;
                }
            }

            string[] argAddr = new string[args.Count()];

            // Create debug file
            if (debugMode && !isHidden) { Debug.CreateDebugFile(); }

            if (!isHidden)
            {
                // Determine if this was executed from ATLAS program
                if (args.Count() > 0)
                {
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Checking Execution Mode"); }
                    bATLAS = atlas.IsATLAS(args[ATLAS_ARG]);
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - NAM was executed from ATLAS:  " + bATLAS.ToString()); }
                }
            }

            for (int i = 0; i < args.Count(); i++)
            {
                if (debugMode && !isHidden) { Debug.WriteDebugInfo("arg:  " + args[i].ToString()); }
            }

            if (bATLAS || isHidden)
            {
                // Hide the console window
                IntPtr hWnd = Kernel32.GetConsole();
                User32.ShowConsole(hWnd, User32.Console.Visibility.Hide);
            }

            // Display App Header Info
            DisplayHeader();

            // Check to see if minimum number of arguments was provided
            if (CheckArraySize(args, MIN_ARGS) == ARG_NUM)
            {
                // Not enough arguments provided, show help message
                ShowHelp();
                DisplayErrMsg(ARG_NUM);
                return ARG_NUM;
            }

            // Create Instance of CANBus
            CANBus canBus = new CANBus();

            if (canBus.Initialized != true)
            {
                if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Failed to Initialize CANBus"); }
                DisplayErrMsg(DLL_FAIL);
                return DLL_FAIL;
            }

            if (isHidden)
            {
                canBus.Asynchronous = true;
            }

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

                // Open VM file to interface with ATLAS program, loop in case other NAM has not let go of vm
                while ((returnStatus = atlas.OpenATLASvm(vmFile)) < 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    iCnt++;

                    if (returnStatus < 0 && iCnt > 5)
                    {
                        DisplayErrMsg(VM_FAIL);
                        return VM_FAIL;
                    }
                }

                // Get address for action argument
                actionArgAddr = atlas.GetATLASArgAddr(args[ACTION_ARG + execMode]);

                // Replace args[] address values with corresponding values passed from ATLAS program
                if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Getting args from ATLAS"); }
                for (int i = 1; i < args.Count(); i++)
                {
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - args value: " + args[i].ToString()); }
                    args[i] = atlas.GetArgsATLAS(args, i);
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - args value: " + args[i].ToString()); }
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - arg address value: " + argAddr[i].ToString()); }
                }

                // Get arguments and perform action
                returnArgAddr = atlas.GetATLASArgAddr(argAddr[args.Length - 2 + execMode]);
            }

            // Collect arguments and validate values
            if ((returnStatus = ValidateArgs(args)) != 0)
            {
                DisplayErrMsg(returnStatus);
                ShowHelp();
                return returnStatus;
            }

            /*// Check if this process was spawned from an async receive command
            for (int i = 0; i < args.Count(); i++)
            {
                if (args[i].ToUpper().Contains(HIDE_ARG))
                {
                    hideArg = true;
                }
            }

            if (hideArg)
            {
                // Hide the console window
                IntPtr hWnd = Kernel32.GetConsole();
                User32.ShowConsole(hWnd, User32.Console.Visibility.Hide);
            }*/

            // If Action argument not provided or unknown, return
            if (actionArg != TX && actionArg != RX && actionArg != LB)
            {
                if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Action: Unknown"); }
                returnStatus = -1; 
                ShowHelp();
                DisplayErrMsg(ARG_RANGE, "<Action>");
                return returnStatus;
            }

            if (asyncArg)
            {
                string newArgs = "";

                for (int i = 0; i < args.Count(); i++)
                {
                    if (args[i].ToUpper() != ASYNC_ARG)
                    {
                        if (i == 1)
                        {
                            newArgs = newArgs + HIDE_ARG + " ";
                        }
                        newArgs = newArgs + args[i] + " ";
                    }
                }
                if (debugMode && !isHidden) { Debug.WriteDebugInfo("process: " + Environment.GetCommandLineArgs()[0] + " " + newArgs); }
                
                System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0], newArgs);
                
                if (bATLAS) { atlas.CloseATLASvm(); }
                
                return 0;
            }

            // Display Parameter Info to be used in measurement
            if (!bATLAS && !isHidden) { DisplayParameters(); }

            // Set CAN Bus Properties
            canBus.Channel = 1;
            canBus.Timing = timingArg;
            canBus.Samples = samplesArg;
            canBus.Filter = filterArg;
            canBus.AcceptCode = accCodeArg;
            canBus.AcceptMask = accMaskArg;
            canBus.Identifier = identityArg;
            canBus.IOFlag = ioArg;
            canBus.WriteData = dataArg;
            canBus.TimeOut = timeOutArg;

            // Execute CAN Bus Operation
            switch (actionArg)
            {

                case TX:
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Action: Transmit"); }       
                    
                    if ((returnStatus = CheckArraySize(args, 2)) == -1)
                    {
                        if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Missing Transmit Arguments"); }     
                        return returnStatus;
                    }
                        
                    if ((returnStatus = canBus.Validate()) == 0)
                    {

                        canBus.Channel = txChanArg;
                        //returnStatus = canBus.Reset();

                        if ((returnStatus = canBus.Transmit()) == 0)
                        {
                            strRetVal = "Success";
                        }
                        else
                        {
                            strRetVal = "Failed";
                        }

                        returnStatus = canBus.Reset();
                        canBus.Channel = rxChanArg;
                        returnStatus = canBus.Reset();
                    }
                    
                    break;

                case RX:
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Action: Recieve"); }

                    if ((returnStatus = CheckArraySize(args, 2)) == -1)
                    {
                        if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Missing Receive Arguments"); }
                        return returnStatus;
                    }

                    if ((returnStatus = canBus.Validate()) == 0)
                    {

                        canBus.Channel = rxChanArg;
                        //returnStatus = canBus.Reset();
                        canBus.Receive(isHidden);
                        if ((canBus.Status) == 0)
                        {
                            strRetVal = canBus.ReadData;
                        }
                        else
                        {
                            strRetVal = "Failed";
                        }
                    }
                    break;

                case LB:
                    if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.Main() - Action: Loopback"); }
                    
                    if ((returnStatus = canBus.Validate()) == 0)
                    {
                        if ((returnStatus = canBus.LoopBack()) == 0)
                        {
                            strRetVal = "Passed";
                        }
                        else
                        {
                            strRetVal = "Failed";
                        }
                    }
                    break;

                default:
                    break;
            }           

            ProcessResults(); 

            if (canBus.ReadData.Contains("error"))
            {
                DisplayErrMsg(CAN_FAIL, canBus.ReadData);
            }

            canBus = null;

            return 0;
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
        /// Returns True is string value is hexadecimal
        /// </summary>
        /// <param name="text"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        static bool IsHex(string text, ref uint num)
        {
            bool status = false;
            uint uiRes = 0;
            
            System.Globalization.CultureInfo provider = new System.Globalization.CultureInfo("en-US");

            status = UInt32.TryParse(text, System.Globalization.NumberStyles.HexNumber, provider, out uiRes);

            num = uiRes;

            return status;
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
            if (!bATLAS && !isHidden)
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
            const string ERR_DLLFAIL = "DLL Operation Failed";
            const string ERR_VM = "Failed to Open ATLAS Virtual Memory File";
            const string ERR_ARGNUM = "Improper Number of Arguments Provided";
            const string ERR_ARGRNG = "Argument(s) Out of Range: ";
            const string ERR_TYPE = "Improper Data Type";
            const string ERR_CAN = "CAN Error: ";

            switch (Err)
            {
                case DLL_MISS:
                    errMsg = ERR_DLL;
                    break;
                case DLL_FAIL:
                    errMsg = ERR_DLLFAIL;
                    break;
                case VM_FAIL:
                    errMsg = ERR_VM;
                    break;
                case ARG_NUM:
                    errMsg = ERR_ARGNUM;
                    break;
                case ARG_RANGE:
                    errMsg = ERR_ARGRNG + txt1 + txt2;
                    break;
                case ARG_TYPE:
                    errMsg = ERR_TYPE + txt1 + txt2;
                    break;
                case CAN_FAIL:
                    errMsg = Environment.NewLine + ERR_CAN + txt1 + txt2;
                    break;
                default:
                    break;
            }

            if (!bATLAS && !isHidden) { Console.WriteLine(errMsg); }
            if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.DisplayErrMsg() - " + errMsg.TrimStart()); }
        }


        /// <summary>
        /// Display the measurement parameters in formatted list
        /// </summary>
        static void DisplayParameters()
        {
            string strAction = String.Empty;

            switch (actionArg)
            {
                case TX:
                    strAction = "CAN Bus Transmit";
                    break;
                case RX:
                    strAction = "CAN Bus Receive";
                    break;
                case LB:
                    strAction = "CAN Bus Loop-back Test";
                    break;
                default:
                    strAction = "Unknown";
                    break;
            }

            Console.WriteLine("Executing " + strAction + " with the following parameters:");
            Console.WriteLine();
            if (actionArg.ToUpper() == TX || actionArg.ToUpper() == LB) { Console.WriteLine("Transmit Channel:  {0}", txChanArg.ToString()); }
            if (actionArg.ToUpper() == RX || actionArg.ToUpper() == LB) { Console.WriteLine(" Receive Channel:  {0}", rxChanArg.ToString()); }
            Console.WriteLine("          Timing:  {0} {1}", timingArg.ToString(), CANBus.FREQ_UNITS);
            Console.WriteLine("       3 Samples:  {0}", samplesArg.ToString());
            Console.WriteLine("   Single-Filter:  {0}", filterArg.ToString());
            Console.WriteLine(" Acceptance Code:  {0}\n\t\t   {1}\n\t\t   {2}", accCodeArg.ToString(), accCodeArg.ToString("x").ToUpper(), ATXML.binToHL(Convert.ToString(accCodeArg, 2).PadLeft(32, '0').Trim())); 
            Console.WriteLine(" Acceptance Mask:  {0}\n\t\t   {1}\n\t\t   {2}", accMaskArg.ToString(), accMaskArg.ToString("x").ToUpper(), ATXML.binToHL(Convert.ToString(accMaskArg, 2).PadLeft(32, '0').Trim()));
            Console.WriteLine("        Time-Out:  {0} {1}", timeOutArg.ToString(), CANBus.TIME_UNITS);
            Console.WriteLine("      Identifier:  {0}\n\t\t   {1}", identityArg.ToString(), identityArg.ToString("x").ToUpper());
            Console.WriteLine("         IO Flag:  {0}", ioArg.ToString());
            if (actionArg.ToUpper() == TX || actionArg.ToUpper() == LB) { Console.WriteLine("            Data:  {0} {1}", dataArg.ToString(), "(" + ToBinary(dataArg) + ")"); }
            Console.WriteLine();
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
                if (debugMode && !isHidden) { Debug.WriteDebugInfo("Program.ProcessResults() - Sending return values back to ATLAS program"); }

                atlas.SetArgsATLAS(returnArgAddr, ATLAS.ATLAS_STRING, default(int), default(bool), strRetVal);
                
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Results");
                Console.WriteLine("_________________________");
                Console.WriteLine("   Return String:  " + strRetVal);
                Console.ForegroundColor = ConsoleColor.Gray;
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
            Console.Write("<Options>\n\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("<Action>");
            Console.Write(" - (string)  " + TX + ", " + RX + ", " + LB + " (required)\n\n");
            
            // TX
            Console.Write(TX + "\t");
            Console.ResetColor();
            Console.Write("- Transmit data over CAN Bus\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // TX CHAN
            Console.Write("\t   " + TXCHAN_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Transmit Channel\t<int>  1 thru " + CANBus.NUMCHANNELS + "\t\t  (default = 1)\n");
            // TIMING
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + TIMING_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Timing\t\t<int>  " + CANBus.MINTIMING + " thru " + CANBus.MAXTIMING + " (default = 20000)\n");
            // 3 SAMPLES
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + SAMPLES_ARG);
            Console.ResetColor();
            Console.Write("\t- Set 3 Samples\t\t<int>  " + CANBus.MINSAMPLES + " thru " + CANBus.MAXSAMPLES + "\t\t  (default = 0)\n");
            // SINGLE-FILTER
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + FILTER_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Filter\t\t<int>  " + CANBus.MINFILTER + " thru " + CANBus.MAXFILTER + "\t\t  (default = 1)\n");
            // ACCEPTANCE CODE
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + CODE_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Acceptance Code\t<uint> " + CANBus.MINACCCODE + " thru " + CANBus.MAXACCCODE + "  (default = 6682)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINACCCODE.ToString("x").ToUpper() + " thru " + CANBus.MAXACCCODE.ToString("x").ToUpper() + "\t  (default = " + 6682.ToString("x").ToUpper() + ")\n");
            // ACCEPTANCE MASK
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + MASK_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Acceptance Mask\t<uint> " + CANBus.MINMASK + " thru " + CANBus.MAXMASK + "  (default = 4294967295)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINMASK.ToString("x").ToUpper() + " thru " + CANBus.MAXMASK.ToString("x").ToUpper() + "\t  (default = " + 4294967295.ToString("x").ToUpper() + ")\n");
            // TIME-OUT
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + TIMEOUT_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Time-Out Period\t<int>  " + CANBus.MINTMO + " thru " + CANBus.MAXTMO + "\t  (default = 2000)\n");
            // IDENTIFIER
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + IDENT_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Set Identifier\t<uint> " + CANBus.MINIDENT + " thru " + CANBus.MAXIDENT + "  (default = 65244)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINIDENT.ToString("x").ToUpper() + " thru " + CANBus.MAXIDENT.ToString("x").ToUpper() + "\t  (default = " + 65244.ToString("x").ToUpper() + ")\n");
            // IO FLAG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + IO_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Set I/O Flag\t\t<int>  " + CANBus.MINIO + " thru " + CANBus.MAXIO + "\t  (default = 1)\n");
            // DATA
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + DATA_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Data\t\t<hex>  0 thru FFFFFFF\t  (default = FE,DC,BA,98,76,54,32,10)\n");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t<string> Success/Failed   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + TX + " " + TXCHAN_ARG + "2 " + TIMEOUT_ARG + "5000\n\n");
            Console.ResetColor();

            // RX
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(RX + "\t\t");
            Console.ResetColor();
            Console.Write("- Receive data over CAN Bus\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // RX CHAN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RXCHAN_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Receive Channel\t<int>  1 thru " + CANBus.NUMCHANNELS + "\t\t  (default = 2)\n");
            // TIMING
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + TIMING_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Timing\t\t<int>  " + CANBus.MINTIMING + " thru " + CANBus.MAXTIMING + " (default = 20000)\n");
            // 3 SAMPLES
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + SAMPLES_ARG);
            Console.ResetColor();
            Console.Write("\t- Set 3 Samples\t\t<int>  " + CANBus.MINSAMPLES + " thru " + CANBus.MAXSAMPLES + "\t\t  (default = 0)\n");
            // SINGLE-FILTER
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + FILTER_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Filter\t\t<int>  " + CANBus.MINFILTER + " thru " + CANBus.MAXFILTER + "\t\t  (default = 1)\n");
            // ACCEPTANCE CODE
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + CODE_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Acceptance Code\t<uint> " + CANBus.MINACCCODE + " thru " + CANBus.MAXACCCODE + "  (default = 6682)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINACCCODE.ToString("x").ToUpper() + " thru " + CANBus.MAXACCCODE.ToString("x").ToUpper() + "\t  (default = " + 6682.ToString("x").ToUpper() + ")\n");
            // ACCEPTANCE MASK
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + MASK_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Acceptance Mask\t<uint> " + CANBus.MINMASK + " thru " + CANBus.MAXMASK + "  (default = 4294967295)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINMASK.ToString("x").ToUpper() + " thru " + CANBus.MAXMASK.ToString("x").ToUpper() + "\t  (default = " + 4294967295.ToString("x").ToUpper() + ")\n");
            // TIME-OUT
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + TIMEOUT_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Time-Out Period\t<int>  " + CANBus.MINTMO + " thru " + CANBus.MAXTMO + "\t  (default = 2000)\n");
            // IDENTIFIER
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + IDENT_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Set Identifier\t<uint> " + CANBus.MINIDENT + " thru " + CANBus.MAXIDENT + "  (default = 65244)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINIDENT.ToString("x").ToUpper() + " thru " + CANBus.MAXIDENT.ToString("x").ToUpper() + "\t  (default = " + 65244.ToString("x").ToUpper() + ")\n");
            // IO FLAG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + IO_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Set I/O Flag\t\t<int>  " + CANBus.MINIO + " thru " + CANBus.MAXIO + "\t  (default = 1)\n");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t<string> Success/Failed   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + RX + " " + RXCHAN_ARG + "2 " + TIMEOUT_ARG + "5000\n\n");
            Console.ResetColor();

            // LB
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(LB + "\t");
            Console.ResetColor();
            Console.Write("- Perform Loopback Test over CAN Bus\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // TX CHAN
            Console.Write("\t   " + TXCHAN_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Transmit Channel\t<int>  1 thru " + CANBus.NUMCHANNELS + "\t\t  (default = 1)\n");
            // RX CHAN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RXCHAN_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Receive Channel\t<int>  1 thru " + CANBus.NUMCHANNELS + "\t\t  (default = 2)\n");
            // TIMING
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + TIMING_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Timing\t\t<int>  " + CANBus.MINTIMING + " thru " + CANBus.MAXTIMING + " (default = 20000)\n");
            // 3 SAMPLES
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + SAMPLES_ARG);
            Console.ResetColor();
            Console.Write("\t- Set 3 Samples\t\t<int>  " + CANBus.MINSAMPLES + " thru " + CANBus.MAXSAMPLES + "\t\t  (default = 0)\n");
            // SINGLE-FILTER
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + FILTER_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Filter\t\t<int>  " + CANBus.MINFILTER + " thru " + CANBus.MAXFILTER + "\t\t  (default = 1)\n");
            // ACCEPTANCE CODE
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + CODE_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Acceptance Code\t<uint> " + CANBus.MINACCCODE + " thru " + CANBus.MAXACCCODE + "  (default = 6682)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINACCCODE.ToString("x").ToUpper() + " thru " + CANBus.MAXACCCODE.ToString("x").ToUpper() + "\t  (default = " + 6682.ToString("x").ToUpper() + ")\n");
            // ACCEPTANCE MASK
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + MASK_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Acceptance Mask\t<uint> " + CANBus.MINMASK + " thru " + CANBus.MAXMASK + "  (default = 4294967295)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINMASK.ToString("x").ToUpper() + " thru " + CANBus.MAXMASK.ToString("x").ToUpper() + "\t  (default = " + 4294967295.ToString("x").ToUpper() + ")\n");
            // TIME-OUT
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + TIMEOUT_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Time-Out Period\t<int>  " + CANBus.MINTMO + " thru " + CANBus.MAXTMO + "\t  (default = 2000)\n");
            // IDENTIFIER
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + IDENT_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Set Identifier\t<uint> " + CANBus.MINIDENT + " thru " + CANBus.MAXIDENT + "  (default = 65244)\n");
            Console.Write("\t\t\t\t\t\t<hex>  " + CANBus.MINIDENT.ToString("x").ToUpper() + " thru " + CANBus.MAXIDENT.ToString("x").ToUpper() + "\t  (default = " + 65244.ToString("x").ToUpper() + ")\n");
            // IO FLAG
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + IO_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Set I/O Flag\t\t<int>  " + CANBus.MINIO + " thru " + CANBus.MAXIO + "\t  (default = 1)\n");
            // DATA
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + DATA_ARG);
            Console.ResetColor();
            Console.Write("\t- Set Data\t\t<hex>  0 thru FFFFFFF\t  (default = FE,DC,BA,98,76,54,32,10)\n");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t<string> Success/Failed   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + LB + " " + TXCHAN_ARG + "2 " + TIMEOUT_ARG + "5000\n\n");
            Console.ResetColor();
        }


        /// <summary>
        /// Validates arguments provided at commandline
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        static int ValidateArgs(string[] arguments)
        {
            string tmpArg = "";

            foreach (string arg in arguments)
            {
                tmpArg = arg.ToUpper();
    
                // Action Argument
                if (tmpArg.Contains(TX))
                {
                    actionArg = TX;
                }
                else if (tmpArg.Contains(RX))
                {
                    actionArg = RX;
                }
                else if (tmpArg.Contains(LB))
                {
                    actionArg = LB;
                }


                // Transmit Channel Argument
                if (tmpArg.Contains(TXCHAN_ARG))
                {
                    string temp = tmpArg.Replace(TXCHAN_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            txChanArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (TXCHAN_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (txChanArg < 1 || txChanArg > CANBus.NUMCHANNELS)
                        {
                            DisplayErrMsg(ARG_RANGE, " (TXCHAN_ARG) - " + txChanArg);
                            return ARG_RANGE;
                        }

                        if (txChanArg == 2)
                        {
                            rxChanArg = 1;
                        }
                    }
                }

                // Receive Channel Argument
                if (tmpArg.Contains(RXCHAN_ARG))
                {
                    string temp = tmpArg.Replace(RXCHAN_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            rxChanArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (RXCHAN_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (rxChanArg < 1 || rxChanArg > CANBus.NUMCHANNELS)
                        {
                            DisplayErrMsg(ARG_RANGE, " (RXCHAN_ARG) - " + rxChanArg);
                            return ARG_RANGE;
                        }

                        if (rxChanArg == 1)
                        {
                            txChanArg = 2;
                        }
                    }
                }

                // Asynchronous Argument
                if (tmpArg.Contains(ASYNC_ARG))
                {
                    asyncArg = true;
                    //if (debugMode && !isHidden) { Debug.WriteDebugInfo("Exiting Debug mode due to asynchronous operations"); }
                    //debugMode = false;
                }

                // Timing Argument
                if (tmpArg.Contains(TIMING_ARG))
                {
                    string temp = tmpArg.Replace(TIMING_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            timingArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (TIMING_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (timingArg < CANBus.MINTIMING || timingArg > CANBus.MAXTIMING)
                        {
                            DisplayErrMsg(ARG_RANGE, " (TIMING_ARG) - " + timingArg);
                            return ARG_RANGE;
                        }
                    }
                }

                // Identifier Argument
                if (tmpArg.Contains(IDENT_ARG))
                {
                    uint tmpNum = 0;
                    string temp = tmpArg.Replace(IDENT_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            identityArg = Convert.ToUInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (IDENT_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (identityArg < CANBus.MINIDENT || identityArg > CANBus.MAXIDENT)
                        {
                            DisplayErrMsg(ARG_RANGE, " (IDENT_ARG) - " + identityArg);
                            return ARG_RANGE;
                        }
                    }
                    else if (IsHex(temp,ref tmpNum))
                    {
                        try
                        {
                            identityArg = Convert.ToUInt32(tmpNum);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (IDENT_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (identityArg < CANBus.MINIDENT || identityArg > CANBus.MAXIDENT)
                        {
                            DisplayErrMsg(ARG_RANGE, " (IDENT_ARG) - " + identityArg);
                            return ARG_RANGE;
                        }
                    }
                }

                // IO Flag Argument
                if (tmpArg.Contains(IO_ARG))
                {
                    string temp = tmpArg.Replace(IO_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            ioArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (IO_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (ioArg < CANBus.MINIO || ioArg > CANBus.MAXIO)
                        {
                            DisplayErrMsg(ARG_RANGE, " (IO_ARG) - " + ioArg);
                            return ARG_RANGE;
                        }
                    }
                }

                // 3 Samples Argument
                if (tmpArg.Contains(SAMPLES_ARG))
                {
                    string temp = tmpArg.Replace(SAMPLES_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            samplesArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (SAMPLES_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (samplesArg < CANBus.MINSAMPLES || samplesArg > CANBus.MAXSAMPLES)
                        {
                            DisplayErrMsg(ARG_RANGE, " (SAMPLES_ARG) - " + samplesArg);
                            return ARG_RANGE;
                        }
                    }
                    else
                    {
                        DisplayErrMsg(ARG_TYPE, " (SAMPLES_ARG) - " + temp);
                        return ARG_TYPE;
                    }
                }

                // Single Filter Argument
                if (tmpArg.Contains(FILTER_ARG))
                {
                    string temp = tmpArg.Replace(FILTER_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            filterArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (FILTER_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (filterArg < CANBus.MINFILTER || filterArg > CANBus.MAXFILTER)
                        {
                            DisplayErrMsg(ARG_RANGE, " (FILTER_ARG) - " + filterArg);
                            return ARG_RANGE;
                        }
                    }
                    else
                    {
                        DisplayErrMsg(ARG_TYPE, " (FILTER_ARG) - " + temp);
                        return ARG_TYPE;
                    }
                }

                // Acceptance Code Argument
                if (tmpArg.Contains(CODE_ARG))
                {
                    uint tmpNum = 0;
                    string temp = tmpArg.Replace(CODE_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            accCodeArg = Convert.ToUInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (CODE_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (accCodeArg < CANBus.MINACCCODE || accCodeArg > CANBus.MAXACCCODE)
                        {
                            DisplayErrMsg(ARG_RANGE, " (CODE_ARG) - " + accCodeArg);
                            return ARG_RANGE;
                        }
                    }
                    else if (IsHex(temp,ref tmpNum))
                    {
                        try
                        {
                            accCodeArg = Convert.ToUInt32(tmpNum);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (CODE_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (accCodeArg < CANBus.MINACCCODE || accCodeArg > CANBus.MAXACCCODE)
                        {
                            DisplayErrMsg(ARG_RANGE, " (CODE_ARG) - " + accCodeArg);
                            return ARG_RANGE;
                        }
                    }
                    else
                    {
                        DisplayErrMsg(ARG_TYPE, " (CODE_ARG) - " + temp);
                        return ARG_TYPE;
                    }
                }

                // Acceptance Mask Argument
                if (tmpArg.Contains(MASK_ARG))
                {
                    uint tmpNum = 0;
                    string temp = tmpArg.Replace(MASK_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            accMaskArg = Convert.ToUInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (MASK_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (accMaskArg < CANBus.MINMASK || accMaskArg > CANBus.MAXMASK)
                        {
                            DisplayErrMsg(ARG_RANGE, " (MASK_ARG) - " + accMaskArg);
                            return ARG_RANGE;
                        }
                    }
                    else if (IsHex(temp,ref tmpNum))
                    {
                        try
                        {
                            accMaskArg = Convert.ToUInt32(tmpNum);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (MASK_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (accMaskArg < CANBus.MINMASK || accMaskArg > CANBus.MAXMASK)
                        {
                            DisplayErrMsg(ARG_RANGE, " (MASK_ARG) - " + accMaskArg);
                            return ARG_RANGE;
                        }
                    }
                    else
                    {
                        DisplayErrMsg(ARG_TYPE, " (MASK_ARG) - " + temp);
                        return ARG_TYPE;
                    }
                }

                // Time-Out
                if (tmpArg.Contains(TIMEOUT_ARG))
                {
                    string temp = tmpArg.Replace(TIMEOUT_ARG, "");
                    if (IsNumeric(temp))
                    {
                        try
                        {
                            timeOutArg = Convert.ToInt32(temp);
                        }
                        catch (Exception)
                        {
                            DisplayErrMsg(ARG_RANGE, " (TIMEOUT_ARG) - " + temp);
                            return ARG_RANGE;
                        }

                        if (timeOutArg < CANBus.MINTMO || timeOutArg > CANBus.MAXTMO)
                        {
                            DisplayErrMsg(ARG_RANGE, " (TIMEOUT_ARG) - " + timeOutArg);
                            return ARG_RANGE;
                        }
                    }
                    else
                    {
                        DisplayErrMsg(ARG_TYPE, " (TIMEOUT_ARG) - " + temp);
                        return ARG_TYPE;
                    }
                }

                // Data Argument
                if (tmpArg.Contains(DATA_ARG))
                {
                    string temp = tmpArg.Replace(DATA_ARG, "");

                    dataArg = temp;
                }
            }
            
            return 0;
        }


        /// <summary>
        /// Converts text string to Binary String
        /// </summary>
        /// <param name="data"></param>
        /// <param name="formatBits"></param>
        /// <returns></returns>
        public static string ToBinary(string data, bool formatBits = false)
        {
            char[] buffer = new char[(((data.Length * 8) + (formatBits ? (data.Length - 1) : 0)))];
            int index = 0;
            for (int i = 0; i < data.Length; i++)
            {
                string binary = Convert.ToString(data[i], 2).PadLeft(8, '0');
                for (int j = 0; j < 8; j++)
                {
                    buffer[index] = binary[j];
                    index++;
                }
                if (formatBits && i < (data.Length - 1))
                {
                    buffer[index] = ' ';
                    index++;
                }
            }
            return new string(buffer);
        }
    }
}
