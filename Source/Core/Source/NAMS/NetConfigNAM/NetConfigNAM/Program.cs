// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NAM
{
    class Program
    {
        #region Constants

        public const string PROCTYPE = "NAM";

        // Program Unique Debug Constants
        public const string DEBUG_FILE = "C:\\APS\\DATA\\DEBUGIT_NETCONFIGNAM";
        public const string DEBUG_RECORD = "C:\\APS\\DATA\\NETCONFIGNAM_DEBUG.txt";

        // ATLAS argument positions
        const int ATLAS_ARG = 0;
        const int ACTION_ARG = 0;
        const string INTERFACE_ARG = "/INTFC:";
        const string ADDR_ARG = "/ADDR:";
        const string MASK_ARG = "/MASK:";
        const string GW_ARG = "/GW:";
        const string RETURN_ARG = "RTN_ARG";

        const int MIN_ARGS = 2;
        const int MIN_ARGS_ATLAS = 3;
        const int MIN_ARGS_STATIC = 5;

        // Actions
        const string STATICIP = "STATICIP";
        const string DHCP = "DHCP";
        const string RESET = "RESET";

        // Error Codes
        const int DLL_MISS = -100;
        const int DLL_FAIL = -101;
        const int VM_FAIL = -102;
        const int ARG_NUM = -103;
        const int ARG_RANGE = -104;
        const int ARG_TYPE = -105;
        const int ARG_MISS = -106;
        const int NOT_FOUND = -107;

        // Registry Keys
        const string SW = "SOFTWARE";

        public const uint DLlNOTFOUND = 0x8007007E;

        #endregion

        #region Variables

        // Assembly Info
        static readonly string AppTitle = AppUtil.GetName();
        static readonly string AppVer = AppUtil.GetVersion();
        static readonly string AppDesc = AppUtil.GetDescription();
        static readonly string AppGuid = AppUtil.GetGuid();
        static readonly string company = AppUtil.GetCompanyName();
        static readonly string WinService = "GPATSUtils";

        /// <summary>
        /// Boolean value identifies whether or not to output debug info
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
        /// Network Interface
        /// </summary>
        static string interfaceArg = string.Empty;

        /// <summary>
        /// IP Address
        /// </summary>
        static string ipAddrArg = string.Empty;

        /// <summary>
        /// Net Mask
        /// </summary>
        static string netMaskArg = string.Empty;

        /// <summary>
        /// Gateway Address
        /// </summary>
        static string gatewayArg = string.Empty;

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

        // Create a new service called "GPATSUtils" which can be viewed in the Service app when executed
        static ServiceController myService = new ServiceController(WinService);

        [STAThread]
        static int Main(string[] args)
        {
            int actionArgAddr = -1;
            int iCnt = 0;
            string[] argAddr = new string[args.Count()];

            // Create debug file
            if (debugMode) { Debug.CreateDebugFile(); }

            // Determine if this was executed from ATLAS program
            if (args.Count() > 0)
            {
                if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Checking Execution Mode"); }
                bATLAS = atlas.IsATLAS(args[ATLAS_ARG]);
                if (debugMode) { Debug.WriteDebugInfo("Program.Main() - NAM was executed from ATLAS:  " + bATLAS.ToString()); }
            }

            for (int i = 0; i < args.Count(); i++)
            {
                if (debugMode) { Debug.WriteDebugInfo("Program.Main() - arg:  " + args[i].ToString()); }
            }

            if (bATLAS)
            {
                // Hide the console window
                IntPtr hWnd = Kernel32.GetConsole();
                User32.ShowConsole(hWnd, User32.Console.Visibility.Hide);
            }

            // Display App Header Info
            DisplayHeader();

            // Check to see if minimum number of arguments was provided
            if ((intRetVal = CheckArraySize(args, MIN_ARGS)) == ARG_NUM)
            {
                // Not enough arguments provided, show help message
                ShowHelp();
                DisplayErrMsg(intRetVal);
                ProcessResults();
                return intRetVal;
            }

            if (bATLAS)
            {
                if (!atlas.dllExists())
                {
                    intRetVal = DLL_MISS;
                    DisplayErrMsg(intRetVal);
                    ProcessResults();
                    return intRetVal;
                }

                execMode = 1;

                // Ensure correct number of arguments were passed for ATLAS execution
                if ((intRetVal = CheckArraySize(args, MIN_ARGS_ATLAS)) == ARG_NUM)
                {
                    DisplayErrMsg(intRetVal);
                    ProcessResults();
                    return intRetVal;
                }

                // this array will be used to store the argument pointers passed from ATLAS
                for (int i = 0; i < args.Count(); i++)
                {
                    argAddr[i] = args[i];
                }

                // Get virtual memory filename
                vmFile = args[ATLAS_ARG];

                // Open VM file to interface with ATLAS program, loop in case other NAM has not let go of vm
                while ((intRetVal = atlas.OpenATLASvm(vmFile)) < 0)
                {
                    System.Threading.Thread.Sleep(1000);
                    iCnt++;

                    if (intRetVal < 0 && iCnt > 5)
                    {
                        DisplayErrMsg(VM_FAIL);
                        ProcessResults();
                        return VM_FAIL;
                    }
                }

                // Get address for action argument
                actionArgAddr = atlas.GetATLASArgAddr(args[ACTION_ARG + execMode]);

                // Replace args[] address values with corresponding values passed from ATLAS program
                if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Getting args from ATLAS"); }
                for (int i = 1; i < args.Count(); i++)
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.Main() - args value: " + args[i].ToString()); }
                    args[i] = atlas.GetArgsATLAS(args, i);
                    if (debugMode) { Debug.WriteDebugInfo("Program.Main() - args value: " + args[i].ToString()); }
                    if (debugMode) { Debug.WriteDebugInfo("Program.Main() - arg address value: " + argAddr[i].ToString()); }
                }

                // Get ATLAS return argument
                returnArgAddr = atlas.GetATLASArgAddr(argAddr[args.Length - 2 + execMode]);
            }

            // Collect arguments and validate values
            if ((intRetVal = ValidateArgs(args)) != 0)
            {
                ShowHelp();
                ProcessResults();
                return intRetVal;
            }

            // If Action argument not provided or unknown, return
            if (actionArg != STATICIP && actionArg != DHCP && actionArg != RESET)
            {
                if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Action: Unknown"); }
                intRetVal = ARG_RANGE;
                ShowHelp();
                DisplayErrMsg(intRetVal, "<Action>");
                ProcessResults();
                return intRetVal;
            }

            // Display Parameter Info to be used in measurement
            if (!bATLAS) { DisplayParameters(); }

            // Initialize Windows service GPATSUtils
            InitService();

            // Update registry
            ConfigurePort(interfaceArg, actionArg);

            // Update the Network Interface via the GPATSUtils service
            if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Executing custom command in " + WinService + " service"); }

            if (actionArg == STATICIP || actionArg == RESET)
            {
                myService.ExecuteCommand((int)GPATSUtil.GPATSUtilsCommands.IPUpdate);          
            }
            else if(actionArg == DHCP)
            {
                myService.ExecuteCommand((int)GPATSUtil.GPATSUtilsCommands.SetDhcp);
            }
           
            // ToDo:  Check Event log for result of service call
            intRetVal = 0;
            ProcessResults();

            return intRetVal;
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
            const string ERR_DLLFAIL = "DLL Operation Failed";
            const string ERR_VM = "Failed to Open ATLAS Virtual Memory File";
            const string ERR_ARGNUM = "Improper Number of Arguments Provided";
            const string ERR_ARGRNG = "Argument(s) Out of Range: ";
            const string ERR_TYPE = "Improper Data Type";
            const string ERR_ARGMISS = "Required Argument(s) Missing";
            const string ERR_NOTFOUND = "Network Interface Not Found:  ";

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
                case ARG_MISS:
                    errMsg = Environment.NewLine + ERR_ARGMISS + txt1 + txt2;
                    break;
                case NOT_FOUND:
                    errMsg = ERR_NOTFOUND + txt1;
                    break;
                default:
                    break;
            }

            if (!bATLAS) { Console.WriteLine(errMsg + "\n"); }
            if (debugMode) { Debug.WriteDebugInfo("Program.DisplayErrMsg() - " + errMsg.TrimStart()); }
        }


        /// <summary>
        /// Display the measurement parameters in formatted list
        /// </summary>
        static void DisplayParameters()
        {
            string strAction = String.Empty;

            switch (actionArg)
            {
                case STATICIP:
                    strAction = "Configuring network interface [ " + interfaceArg + " ] for static IP";
                    break;
                case DHCP:
                    strAction = "Configuring network interface [ " + interfaceArg + " ] for DHCP";
                    break;
                case RESET:
                    strAction = "Resetting network interface [ " + interfaceArg + " ] to default configuration";
                    break;
                default:
                    strAction = "Unknown";
                    break;
            }

            if (actionArg.ToUpper() == STATICIP) 
            {
                Console.WriteLine(strAction + " with the following parameters:");
                Console.WriteLine();

                Console.WriteLine("     IP Address:  {0}", ipAddrArg.ToString());
                Console.WriteLine("   Network Mask:  {0}", netMaskArg.ToString());
                Console.WriteLine("GateWay Address:  {0}", gatewayArg.ToString());
            }
            else
            {
                Console.WriteLine(strAction);
            }

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
                if (debugMode) { Debug.WriteDebugInfo("Program.ProcessResults() - Sending return values back to ATLAS program"); }

                atlas.SetArgsATLAS(returnArgAddr, ATLAS.ATLAS_INT, intRetVal);

            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Results");
                Console.WriteLine("_________________________");
                Console.WriteLine("   Return String:  " + intRetVal.ToString());
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
            Console.Write(" - (string)  " + STATICIP + ", " + DHCP + ", " + RESET + " (required)\n\n");

            // Static IP
            Console.Write(STATICIP + "\t");
            Console.ResetColor();
            Console.Write("- Configure given network interface with a static IP address\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // Interface
            Console.Write("\t   " + INTERFACE_ARG);
            Console.ResetColor();
            Console.Write("\t- Target Network Interface\t<string>\n");
            // IP Address
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + ADDR_ARG);
            Console.ResetColor();
            Console.Write("\t- Desired IP Address\t\t<string>  0.0.0.0 thru 255.255.255.255\n");
            // Net Mask
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + MASK_ARG);
            Console.ResetColor();
            Console.Write("\t- Desired Network Mask\t\t<string>  0.0.0.0 thru 255.255.255.255\n");
            // Gateway
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + GW_ARG);
            Console.ResetColor();
            Console.Write("\t\t- Desired Gateway Address\t<string>  0.0.0.0 thru 255.255.255.255\n");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t\t<int>\t  -1 or 0   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + STATICIP + " " + INTERFACE_ARG + "\"Ethernet 1\" " + ADDR_ARG + "192.168.1.5 " + MASK_ARG + "255.255.255.0 " + GW_ARG + "192.168.1.1\n\n");
            Console.ResetColor();

            // DHCP
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(DHCP + "\t\t");
            Console.ResetColor();
            Console.Write("- Configure given network interface for DHCP\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // Interface
            Console.Write("\t   " + INTERFACE_ARG);
            Console.ResetColor();
            Console.Write("\t- Target Network Interface\t<string>\n");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t\t<int>\t  -1 or 0   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + DHCP + " " + INTERFACE_ARG + "Gigabit2\n\n");
            Console.ResetColor();

            // RESET
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(RESET + "\t");
            Console.ResetColor();
            Console.Write("- Reset given network interface to default configuration\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // Interface
            Console.Write("\t   " + INTERFACE_ARG);
            Console.ResetColor();
            Console.Write("\t- Target Network Interface\t<string>\n");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t\t<int>\t  -1 or 0   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + RESET + " " + INTERFACE_ARG + "Lan1\n\n");
            Console.ResetColor();
        }

        /// <summary>
        /// Validates arguments provided at commandline
        /// </summary>
        /// <param name="arguments"></param>
        /// <returns></returns>
        static int ValidateArgs(string[] arguments)
        {
            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Validating Arguments"); }

            string tmpArg = "";
            bool foundIntfc = false;
            bool foundIpAddr = false;
            bool foundNetMask = false;
            bool foundGW = false;

            foreach (string arg in arguments)
            {
                tmpArg = arg.ToUpper();

                // Action Argument (Static IP)
                if (tmpArg.Contains(STATICIP))
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Action [ " + STATICIP + " ]"); }
                    actionArg = STATICIP;

                    // Check for required args relative to given action argument provided
                    if (CheckArraySize(arguments, MIN_ARGS_STATIC) == ARG_NUM)
                    {
                        DisplayErrMsg(ARG_NUM);
                        return ARG_NUM;
                    }
                }

                // Action Argument (DHCP)
                else if (tmpArg.Contains(DHCP))
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Action [ " + DHCP + " ]"); }
                    actionArg = DHCP;
                    foundIpAddr  = true;
                    foundNetMask = true;
                    foundGW      = true;
                }

                // Action Argument (Reset)
                else if (tmpArg.Contains(RESET))
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Action [ " + RESET + " ]"); }
                    actionArg = RESET;
                    foundIpAddr  = true;
                    foundNetMask = true;
                    foundGW      = true;
                }

                // Network Interface Argument
                else if (tmpArg.Contains(INTERFACE_ARG))
                {
                    foundIntfc  = true;
                    string temp = tmpArg.Replace(INTERFACE_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        // check if one of the available interfaces
                        if (Network.InterfaceExists(temp))
                        {
                            interfaceArg = temp;
                            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Netork Interface [ " + interfaceArg + " ]"); }
                        }
                        else
                        {
                            DisplayErrMsg(NOT_FOUND, temp);
                            return NOT_FOUND;
                        }
                    }
                    else
                    {
                        // Interface name not provided
                        DisplayErrMsg(ARG_RANGE, " INTERFACE_ARG");
                        return ARG_RANGE;
                    }
                }

                // Static IP Address Argument
                else if (tmpArg.Contains(ADDR_ARG))
                {
                    foundIpAddr = true;
                    string temp = tmpArg.Replace(ADDR_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (Network.IsValidIp(temp))
                        {
                            // Valid ip address was passed
                            ipAddrArg = temp;
                            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found IP Address [ " + ipAddrArg + " ]"); }
                        }
                        else
                        {
                            // Invalid ip address was passed
                            DisplayErrMsg(ARG_RANGE, " (ADDR_ARG) - " + temp);
                            return ARG_RANGE;
                        }
                    }
                }

                // Network Mask Argument
                else if (tmpArg.Contains(MASK_ARG))
                {
                    foundNetMask = true;
                    string temp  = tmpArg.Replace(MASK_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (Network.IsValidIp(temp))
                        {
                            // Valid ip address was passed
                            netMaskArg = temp;
                            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Network Mask [ " + netMaskArg + " ]"); }
                        }
                        else
                        {
                            // Invalid ip address was passed
                            DisplayErrMsg(ARG_RANGE, " (MASK_ARG) - " + temp);
                            return ARG_RANGE;
                        }
                    }
                }

                // Gateway Argument
                else if (tmpArg.Contains(GW_ARG))
                {
                    foundGW = true;
                    string temp = tmpArg.Replace(GW_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (Network.IsValidIp(temp))
                        {
                            // Valid address was passed
                            gatewayArg = temp;
                            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Gateway Address [ " + gatewayArg + " ]"); }
                        }
                        else
                        {
                            // Invalid address was passed
                            DisplayErrMsg(ARG_RANGE, " (GW_ARG) - " + temp);
                            return ARG_RANGE;
                        }
                    }
                }
            }

            // If any required args were not provided
            if (!foundIntfc)
            {
                DisplayErrMsg(ARG_MISS, " (INTFC_ARG)");
                return ARG_MISS;
            }
            if (!foundIpAddr)
            {
                DisplayErrMsg(ARG_MISS, " (ADDR_ARG)");
                return ARG_MISS;
            }
            if (!foundNetMask)
            {
                DisplayErrMsg(ARG_MISS, " (MASK_ARG)");
                return ARG_MISS;
            }
            if (!foundGW)
            {
                DisplayErrMsg(ARG_MISS, " (GW_ARG)");
                return ARG_MISS;
            }

            return 0;
        }

        /// <summary>
        /// Initialize the GPATSUtils windows service, if it is not already running
        /// </summary>
        static void InitService()
        {
            if (debugMode) { Debug.WriteDebugInfo("Program.InitService() - Verifying GPATSUtils service is running"); }
            // While the service is not running
            while (myService.Status != ServiceControllerStatus.Running)
            {
                // If the service is not pending
                if (myService.Status != ServiceControllerStatus.StartPending)
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.InitService() - Starting service"); }
                    // Start the service and wait up to 10 seconds for its status to update to "running"
                    myService.Start();
                    myService.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(10));
                }
            }
            if (debugMode) { Debug.WriteDebugInfo("Program.InitService() - Service is running"); }
        }

        /// <summary>
        /// Configure port related registry entries under \HKLM\SOFTWARE\TMDE\GPATSUtils\Network
        /// </summary>
        /// <param name="portName">Network Port Name</param>
        /// <param name="operation">IP Config Operation</param>
        /// <returns>True if successful, False if not</returns>
        static bool ConfigurePort(string portName, string operation)
        {
            bool stat = true;
            string regPath = SW + "\\" + company + "\\" + WinService + "\\Network";

            // Update Network Interface registry entry
            if (RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.StringTypes.Interface, portName, Microsoft.Win32.RegistryValueKind.String))
            {
                // Update Static IP registry entries
                if (operation == STATICIP)
                {
                    // IP Address, SubNet Mask, & Gateway Address
                    stat = RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.MultiStringTypes.IpAddress, new string[] { ipAddrArg }, Microsoft.Win32.RegistryValueKind.MultiString) &&
                    RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.MultiStringTypes.SubNet, new string[] { netMaskArg }, Microsoft.Win32.RegistryValueKind.MultiString) &&
                    RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.MultiStringTypes.GateWay, new string[] { gatewayArg }, Microsoft.Win32.RegistryValueKind.MultiString);
                }
                // Update registry entries for default settings of selected port
                else if (operation == RESET)
                {
                    switch (portName)
                    {
                        case GPATSUtil.Network.J15.PORTNAME:
                            // IP Address, SubNet Mask, & Gateway Address
                            stat = RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J15.IP, new string[] { ipAddrArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J15.SUBNET, new string[] { netMaskArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J15.GATEWAY, new string[] { gatewayArg }, Microsoft.Win32.RegistryValueKind.MultiString);
                            break;

                        case GPATSUtil.Network.J16.PORTNAME:
                            // IP Address, SubNet Mask, & Gateway Address
                            stat = RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J16.IP, new string[] { ipAddrArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J16.SUBNET, new string[] { netMaskArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J16.GATEWAY, new string[] { gatewayArg }, Microsoft.Win32.RegistryValueKind.MultiString);
                            break;

                        case GPATSUtil.Network.J18.PORTNAME:
                            // IP Address, SubNet Mask, & Gateway Address
                            stat = RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J18.IP, new string[] { ipAddrArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J18.SUBNET, new string[] { netMaskArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J18.GATEWAY, new string[] { gatewayArg }, Microsoft.Win32.RegistryValueKind.MultiString);
                            break;

                        case GPATSUtil.Network.J19.PORTNAME:
                            // IP Address, SubNet Mask, & Gateway Address
                            stat = RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J19.IP, new string[] { ipAddrArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J19.SUBNET, new string[] { netMaskArg }, Microsoft.Win32.RegistryValueKind.MultiString)
                                && RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.J19.GATEWAY, new string[] { gatewayArg }, Microsoft.Win32.RegistryValueKind.MultiString);
                            break;

                        default:
                            //No default parameters for unknown port
                            stat = false;
                            break;
                    }
                }
            }
            else
            {
                stat = false;
            }
            
            return stat;
        }
    }
}
