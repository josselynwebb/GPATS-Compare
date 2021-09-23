// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;  // Add System.ServiceProcess framework reference from systemserviceprocess.dll
using System.Text;
using System.Threading.Tasks;

namespace NAM
{
    class Program
    {

        #region Constants

        public const string PROCTYPE = "NAM";

        // Program Unique Debug Constants
        public const string DEBUG_FILE   = "C:\\APS\\DATA\\DEBUGIT_FIREWALLUPDATENAM";
        public const string DEBUG_RECORD = "C:\\APS\\DATA\\FIREWALLUPDATE_DEBUG.txt";

        // ATLAS argument positions
        const int    ATLAS_ARG     = 0;
        const int    ACTION_ARG    = 0;
        const string DIRECTION_ARG = "/DIR:";
        const string PROTO_ARG     = "/PROTO:";
        const string PORT_ARG      = "/PORT:";
        const string RETURN_ARG    = "RTN_ARG";

        // Minimum number of arguments accepted from console
        const int    MIN_ARGS        = 3;
        // Minimum number of arguments accepted from ATLAS program
        const int    MIN_ARGS_ATLAS  = 4;

        // Actions
        const string OPEN  = "OPEN";
        const string CLOSE = "CLOSE";

        // Directions
        const string DIR_IN  = "IN";
        const string DIR_OUT = "OUT";
        const string DIR_MAX = "MAX";
        static List<string> directions = new List<string>() { DIR_IN, DIR_OUT, DIR_MAX };

        // Protocols
        const string PROTO_TCP = "TCP";
        const string PROTO_UDP = "UDP";
        const string PROTO_BOTH = "BOTH";
        static List<string> protocols = new List<string>() { PROTO_TCP, PROTO_UDP, PROTO_BOTH };

        // Error Codes
        const int DLL_MISS  = -100;
        const int DLL_FAIL  = -101;
        const int VM_FAIL   = -102;
        const int ARG_NUM   = -103;
        const int ARG_RANGE = -104;
        const int ARG_TYPE  = -105;
        const int ARG_MISS  = -106;
        const int NOT_FOUND = -107;
        const int NOT_AUTH  = -108;

        // Registry Keys
        const string SW = "SOFTWARE";

        public const uint DLlNOTFOUND = 0x8007007E;

        #endregion

        #region Variables

        // Assembly Info
        static readonly string AppTitle   = AppUtil.GetName();
        static readonly string AppVer     = AppUtil.GetVersion();
        static readonly string AppDesc    = AppUtil.GetDescription();
        static readonly string AppGuid    = AppUtil.GetGuid();
        static readonly string company    = AppUtil.GetCompanyName();
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
        /// Firewall Rule direction IN/OUT
        /// </summary>
        static string directionArg = string.Empty;

        /// <summary>
        /// Firewall Rule protocol TCP/UDP/BOTH
        /// </summary>
        static string protoArg = string.Empty;

        /// <summary>
        /// Firewall Rule Port Number
        /// </summary>
        static string portArg = string.Empty;

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

        // Create instance of the ATLAS class
        static ATLAS atlas = new ATLAS();

        // Create an instance of the ServiceController class
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
            if (actionArg != OPEN && actionArg != CLOSE)
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
            ConfigureRule(directionArg, protoArg, portArg);

            // Update the Network Interface via the GPATSUtils service
            if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Executing custom command in " + WinService + " service"); }

            if (actionArg == OPEN)
            {
                myService.ExecuteCommand((int)GPATSUtil.GPATSUtilsCommands.OpenPort);
            }
            else if (actionArg == CLOSE)
            {
                myService.ExecuteCommand((int)GPATSUtil.GPATSUtilsCommands.ClosePort);
            }

            // Check Windows Event Logs for status of operation
            if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Checking Windows Event Logs for status "); }

            EventViewer logViewer = new EventViewer();
            int numEntries = 3;
            List<System.Diagnostics.EventLogEntry> logs = logViewer.ReadLog("Application", "GPATSUtils", numEntries);

            if (logViewer.FindEntry(logs, "completed successfully") != null)
            {
                intRetVal = 0;
            }
            else
            {
                intRetVal = -1;
            }

            if (debugMode) { Debug.WriteDebugInfo("Program.Main() - Status of operation: " + intRetVal.ToString()); }

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
            const string ERR_NOTAUTH = "Port requested is Not Authorized For Use:  ";

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
                case NOT_AUTH:
                    errMsg = ERR_NOTAUTH + txt1;
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
                case OPEN:
                    strAction = "Configuring firewall to open port # [ " + portArg + " ]";
                    break;
                case CLOSE:
                    strAction = "Configuring firewall to close port # [ " + portArg + " ]";
                    break;
                default:
                    strAction = "Unknown";
                    break;
            }

            if (actionArg.ToUpper() == OPEN)
            {
                Console.WriteLine(strAction + " with the following parameters:");
                Console.WriteLine();
                Console.WriteLine("     Direction:  [ {0} ] {1}", directionArg.ToString(), directions[Convert.ToInt32(directionArg)]);
                Console.WriteLine("      Protocol:  [ {0} ] {1}", protoArg.ToString(), protocols[Convert.ToInt32(protoArg)]);
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
                string strStatus = intRetVal == 0 ? "Success" : "Failed";

                Console.WriteLine();
                Console.WriteLine("Results");
                Console.WriteLine("_________________________");
                Console.WriteLine("   Return String:  [ " + intRetVal.ToString() +" ] " + strStatus);
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
            Console.Write(" - (string)  " + OPEN + ", " + CLOSE + " (required)\n\n");

            // Open
            Console.Write(OPEN + "\t");
            Console.ResetColor();
            Console.Write("- Create TPS firewall rule allowing network traffic on given port\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // Direction
            Console.Write("\t   " + DIRECTION_ARG);
            Console.ResetColor();
            Console.Write("\t- Direction of traffic\t<string>  (" + DIR_IN + " / " + DIR_OUT + " / " + DIR_MAX + ")\n");
            // Protocol
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + PROTO_ARG);
            Console.ResetColor();
            Console.Write("\t- Desired Protocol(s)\t<string>  (" + PROTO_TCP + " / " + PROTO_UDP + " / " + PROTO_BOTH + ")\n");
            // Port
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + PORT_ARG);
            Console.ResetColor();
            Console.Write("\t- Desired Port\t\t<string>  (1 thru {0}\n", 65353 + ")");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t<int>\t  (-1 or 0)   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + OPEN + " " + DIRECTION_ARG + DIR_IN + " " + PROTO_ARG + PROTO_TCP + " " + PORT_ARG + "255 " + "\n\n");
            Console.ResetColor();

            // Close
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(CLOSE + "\t");
            Console.ResetColor();
            Console.Write("- Remove TPS Firewall Rule by name (based on input parameters)\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   <Options>\n");
            // Direction
            Console.Write("\t   " + DIRECTION_ARG);
            Console.ResetColor();
            Console.Write("\t- Direction of traffic\t<string>  (" + DIR_IN + " / " + DIR_OUT + " / " + DIR_MAX + ")\n");
            // Protocol
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + PROTO_ARG);
            Console.ResetColor();
            Console.Write("\t- Desired Protocol(s)\t<string>  (" + PROTO_TCP + " / " + PROTO_UDP + " / " + PROTO_BOTH + ")\n");
            // Port
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + PORT_ARG);
            Console.ResetColor();
            Console.Write("\t- Desired Port\t\t<string>  (1 thru {0}\n", 32767 + ")");
            // RETURN
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\t   " + RETURN_ARG);
            Console.ResetColor();
            Console.Write("\t- Return Status\t\t<int>\t  -1 or 0   ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("(ATLAS only)\n\n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("           i.e. " + AppTitle + " " + CLOSE + " " + DIRECTION_ARG + DIR_MAX + " " + PROTO_ARG + PROTO_UDP + " " + PORT_ARG + "5668 " + "\n\n");
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
            bool foundDir = false;
            bool foundProto = false;
            bool foundPort = false;

            foreach (string arg in arguments)
            {
                tmpArg = arg.ToUpper();

                // Action Argument (Open Port)
                if (tmpArg.Contains(OPEN))
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Action [ " + OPEN + " ]"); }
                    actionArg = OPEN;
                }

                // Action Argument (Close Port)
                else if (tmpArg.Contains(CLOSE))
                {
                    if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Action [ " + CLOSE + " ]"); }
                    actionArg = CLOSE;
                    foundProto = true;
                    foundPort = true;
                }

                // Firewall Direction Argument
                else if (tmpArg.Contains(DIRECTION_ARG))
                {
                    foundDir = true;
                    string temp = tmpArg.Replace(DIRECTION_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        // check if one of the available interfaces
                        if (directions.IndexOf(temp.ToUpper()) > -1)
                        {
                            switch (temp.ToUpper())
                            {
                                case DIR_IN:
                                    directionArg = ((int)Network.FIREWALL_DIRECTION.INBOUND).ToString();
                                    break;
                                case DIR_OUT:
                                    directionArg = ((int)Network.FIREWALL_DIRECTION.OUTBOUND).ToString();
                                    break;
                                case DIR_MAX:
                                    directionArg = ((int)Network.FIREWALL_DIRECTION.MAX).ToString();
                                    break;
                            }

                            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Direction [ " + directionArg + " ] " + directions[Convert.ToInt32(directionArg)]); }
                        }
                        else
                        {
                            DisplayErrMsg(NOT_FOUND, temp);
                            return NOT_FOUND;
                        }
                    }
                    else
                    {
                        // Direction not provided
                        DisplayErrMsg(ARG_RANGE, " (DIRECTION_ARG)");
                        return ARG_RANGE;
                    }
                }

                // Protocol Argument
                else if (tmpArg.Contains(PROTO_ARG))
                {
                    foundProto = true;
                    string temp = tmpArg.Replace(PROTO_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        if (protocols.IndexOf(temp.ToUpper()) > -1)
                        {
                            // Valid protocol was passed

                            switch (temp.ToUpper())
                            {
                                case PROTO_TCP:
                                    protoArg = ((int)Network.FIREWALL_PROTOCOL.TCP).ToString();
                                    break;
                                case PROTO_UDP:
                                    protoArg = ((int)Network.FIREWALL_PROTOCOL.UDP).ToString();
                                    break;
                                case PROTO_BOTH:
                                    protoArg = ((int)Network.FIREWALL_PROTOCOL.BOTH).ToString();
                                    break;
                            }

                            if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Protocol [ " + protoArg + " ] " + protocols[Convert.ToInt32(protoArg)]); }
                        }
                        else
                        {
                            // Invalid protocol was passed
                            DisplayErrMsg(ARG_RANGE, " (PROTO_ARG) - " + temp);
                            return ARG_RANGE;
                        }
                    }
                }

                // Port Argument
                else if (tmpArg.Contains(PORT_ARG))
                {
                    foundPort = true;
                    string temp = tmpArg.Replace(PORT_ARG, "");
                    if (!string.IsNullOrEmpty(temp))
                    {
                        int x;
                        if (int.TryParse(temp,out x))
                        {
                            if (Network.IsPortValid(x))
                            {
                                // Valid port was passed

                                portArg = temp;
                                if (debugMode) { Debug.WriteDebugInfo("Program.ValidateArgs() - Found Port [ " + portArg + " ]"); }

                                if (!Network.isPortAuthorized(x))
                                {
                                    DisplayErrMsg(NOT_AUTH, " (PORT_ARG) - " + temp);
                                    return NOT_AUTH;
                                }
                            }
                            else
                            {
                                // Invalid port number was passed
                                DisplayErrMsg(ARG_RANGE, " (PORT_ARG) - " + temp);
                                return ARG_RANGE;
                            }
                        }                       
                        else
                        {
                            // Invalid port number was passed
                            DisplayErrMsg(ARG_RANGE, " (PORT_ARG) - " + temp);
                            return ARG_RANGE;
                        }
                    }
                }

            }

            // If any required args were not provided
            if (!foundDir)
            {
                DisplayErrMsg(ARG_MISS, " (DIRECTION_ARG)");
                return ARG_MISS;
            }
            if (!foundProto)
            {
                DisplayErrMsg(ARG_MISS, " (PROTO_ARG)");
                return ARG_MISS;
            }
            if (!foundPort)
            {
                DisplayErrMsg(ARG_MISS, " (PORT_ARG)");
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
        static bool ConfigureRule(string direction, string protocol, string port)
        {
            bool stat = true;
            string regPath = SW + "\\" + company + "\\" + WinService + "\\Network";

            // Update Network Interface registry entry
            if (RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.StringTypes.FWDirection, direction, Microsoft.Win32.RegistryValueKind.String) &&
                RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.StringTypes.FWProtocol, protocol, Microsoft.Win32.RegistryValueKind.String) &&
                RegistryUtil.SetRegVal(regPath, GPATSUtil.Network.RegistryKeys.StringTypes.FWPort, port, Microsoft.Win32.RegistryValueKind.String))
            {
                // It worked
            }
            else
            {
                stat = false;
            }

            return stat;
        }

    }
}
