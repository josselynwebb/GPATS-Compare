// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using NetFwTypeLib;

namespace NAM
{
    public partial class GPATSUtils : ServiceBase
    {
        // Define public variables
        string eventSourceName = "GPATSUtils";
        string company = "";
        string app = "";
        string logName = "Application";
        const string SW = "SOFTWARE";
        ServiceBase serviceBase = new ServiceBase();

        // Local variables
        private int eventId = 1;

        private enum GPATSUtilsCommands
        {
            // enum variables start counting from 128
            StopWorker = 128,
            RestartWorker,
            CheckWorker,
            HostNameUpdate,
            IPUpdate,
            SetDhcp,
            Reboot,
            OpenPort,
            ClosePort
        }
        
        /// <summary>
        /// Default Constructor
        /// </summary>
        public GPATSUtils()
        {
            InitializeComponent();

            initLog();
            
            getAppInfo();

            while (!System.Diagnostics.EventLog.SourceExists(eventSourceName) || !System.Diagnostics.EventLog.Exists(logName))
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Constructor takes two arguments: eventSourceName and logName
        /// </summary>
        /// <param name="args">A string array of command line arguments</param>
        public GPATSUtils(string[] args)
        {
            InitializeComponent();

            if (args.Length > 0)
            {
                eventSourceName = args[0];
            }

            if (args.Length > 1)
            {
                logName = args[1];
            }

            initLog();

            getAppInfo();

            while (!System.Diagnostics.EventLog.SourceExists(eventSourceName) || !System.Diagnostics.EventLog.Exists(logName))
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

        #region Events

        /// <summary>
        /// On Start Event
        /// </summary>
        /// <param name="args">A list of command line arguments</param>
        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            Tuple<bool, AdvancedAPI.ServiceStatus> rtn;

            // Record event to the System Event Viewer
            eventLog1.WriteEntry("Service Starting", EventLogEntryType.Information);

            // Update the service status every 100 seconds
            rtn = AdvancedAPI.SetServiceStatus(this.ServiceHandle, AdvancedAPI.ServiceState.SERVICE_START_PENDING, true, 100000);

            // If the first item
            if (rtn.Item1)
            {
                // Update the event log with information from the service status Item2
                eventLog1.WriteEntry("Service Status:  " + rtn.Item2.dwCurrentState.ToString(), EventLogEntryType.Information);
            }
            else
            {
                // Status Status Change Failed
                eventLog1.WriteEntry("Error occurred setting service status", EventLogEntryType.Information);
            }

            // Reset service status variable
            rtn = null;

            // Initialize Registry
            initRegistry();

            // Remove any open TPS Firewall rules
            initFirewall();

            // Update the service state to Running.
            rtn = AdvancedAPI.SetServiceStatus(this.ServiceHandle, AdvancedAPI.ServiceState.SERVICE_RUNNING);

            // If first itme
            if (rtn.Item1)
            {
                // Update System Event Log Viewer with current state info
                eventLog1.WriteEntry("Service Status:  " + rtn.Item2.dwCurrentState.ToString(), EventLogEntryType.Information);
            }
            else
            {
                // Update System Event Log Viewer with Failed Status Change
                eventLog1.WriteEntry("Error occurred setting service status", EventLogEntryType.Information);
            }

            // Exit OnStart leaving rtn null
            rtn = null;
        }

        /// <summary>
        /// On Pause Event
        /// </summary>
        protected override void OnPause()
        {
            // Update System Event Log Viewer with Service Paused message
            eventLog1.WriteEntry("Service Paused", EventLogEntryType.Information);
        }

        /// <summary>
        /// On Continue Event
        /// </summary>
        protected override void OnContinue()
        {
            // Update System Event Log Viewer with Service Continuing message
            eventLog1.WriteEntry("Service Continuing", EventLogEntryType.Information);
        }

        /// <summary>
        /// On Stop Event
        /// </summary>
        protected override void OnStop()
        {
            // Update the service state to Stop Pending.
            Tuple<bool, AdvancedAPI.ServiceStatus> rtn;

            eventLog1.WriteEntry("Service Stopping", EventLogEntryType.Information);

            rtn = AdvancedAPI.SetServiceStatus(this.ServiceHandle, AdvancedAPI.ServiceState.SERVICE_STOP_PENDING, true, 100000);

            if (rtn.Item1)
            {
                eventLog1.WriteEntry("Service Status:  " + rtn.Item2.dwCurrentState.ToString(), EventLogEntryType.Information);
            }
            else
            {
                // Status Status Change Failed
                eventLog1.WriteEntry("Error occurred setting service status", EventLogEntryType.Information);
            }

            rtn = null;

            // Update the service state to Stopped.
            rtn = AdvancedAPI.SetServiceStatus(this.ServiceHandle, AdvancedAPI.ServiceState.SERVICE_STOPPED);

            if (rtn.Item1)
            {
                eventLog1.WriteEntry("Service Status:  " + rtn.Item2.dwCurrentState.ToString(), EventLogEntryType.Information);
            }
            else
            {
                // Status Status Change Failed
                eventLog1.WriteEntry("Error occurred setting service status", EventLogEntryType.Information);
            }

            rtn = null;
        }

        /// <summary>
        /// On Shut Down Event
        /// </summary>
        protected override void OnShutdown()
        {
            eventLog1.WriteEntry("Service Shutdown", EventLogEntryType.Information);
        }

        /// <summary>
        /// On Custom Command Event
        /// </summary>
        /// <param name="command"></param>
        protected override void OnCustomCommand(int command)
        {
            object oPortName = new object();

            switch (command)
            {
                case (int)GPATSUtilsCommands.StopWorker:
                    eventLog1.WriteEntry("StopWorker Command " + command + " successfully called", EventLogEntryType.Information);
                    break;

                case (int)GPATSUtilsCommands.RestartWorker:
                    eventLog1.WriteEntry("ReStartWorker Command " + command + " successfully called", EventLogEntryType.Information);
                    break;

                case (int)GPATSUtilsCommands.CheckWorker:
                    eventLog1.WriteEntry("CheckWorker Command " + command + " successfully called", EventLogEntryType.Information);
                    break;

                case (int)GPATSUtilsCommands.HostNameUpdate:
                    eventLog1.WriteEntry("HostNameUpdate Command " + command + " successfully called", EventLogEntryType.Information);
                    UpdateHostName("Testing");
                    break;

                case (int)GPATSUtilsCommands.IPUpdate:
                    eventLog1.WriteEntry("IPUpdate Command " + command + " successfully called", EventLogEntryType.Information);
                    // Get reg value for networkportname, ip, subnet, gateway
                    oPortName = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "NetworkPortName", Microsoft.Win32.RegistryValueKind.String);
                    object oIPAddress = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "IpAddress", Microsoft.Win32.RegistryValueKind.MultiString);
                    object oSubnet = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "SubNet", Microsoft.Win32.RegistryValueKind.MultiString);
                    object oGateway = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "GateWay", Microsoft.Win32.RegistryValueKind.MultiString);
                    
                    if (UpdateNicStaticIp((string)oPortName, (string[])oIPAddress, (string[])oSubnet, (string[])oGateway))
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - IPUpdate completed successfully", EventLogEntryType.Information);
                    }
                    else
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - IPUpdate operation failed", EventLogEntryType.Error);
                    }
                    break;

                case (int)GPATSUtilsCommands.SetDhcp:
                    eventLog1.WriteEntry("SetDhcp Command " + command + " successfully called", EventLogEntryType.Information);
                    // Get reg value for networkportname
                    oPortName = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "NetworkPortName", Microsoft.Win32.RegistryValueKind.String);
                    // Wipeout the registry entries
                    initRegistry();
                    if (SetNicDhcp((string)oPortName))
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - SetDhcp completed successfully", EventLogEntryType.Information);
                    }
                    else
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - SetDhcp operation failed", EventLogEntryType.Error);
                    }
                    break;

                case (int)GPATSUtilsCommands.Reboot:
                    eventLog1.WriteEntry("ShutDown Command " + command + " successfully called", EventLogEntryType.Information);
                    ShutDown((int)User32.PowerSetting.REBOOT);
                    break;

                case (int)  GPATSUtilsCommands.OpenPort:
                    eventLog1.WriteEntry("OpenPort Command " + command + " successfully called", EventLogEntryType.Information);
                    if (UpdateFirewall((int)GPATSUtilsCommands.OpenPort))
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - OpenPort completed successfully", EventLogEntryType.Information);
                    }
                    else
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - OpenPort operation failed", EventLogEntryType.Error);
                    }
                    break;

                case (int)GPATSUtilsCommands.ClosePort:
                    eventLog1.WriteEntry("ClosePort Command " + command + " successfully called", EventLogEntryType.Information);
                    if (UpdateFirewall((int)GPATSUtilsCommands.ClosePort))
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - ClosePort completed successfully", EventLogEntryType.Information);
                    }
                    else
                    {
                        eventLog1.WriteEntry("GPATSUtils.OnCustomCommand() - ClosePort operation failed", EventLogEntryType.Error);
                    }
                    break;

                default:
                    eventLog1.WriteEntry("Undefined command receieved", EventLogEntryType.Warning);
                    break;
            }

            base.OnCustomCommand(command);
        }

        #endregion

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {
            // TBD
        }

        private void getAppInfo()
        {
            company = AppUtil.GetCompanyName();
            app = AppUtil.GetName();
        }

        // Create a new system event log
        private void initLog()
        {
            eventLog1 = new System.Diagnostics.EventLog(logName, System.Environment.MachineName, eventSourceName);
        }

        // Create new entries in registry for the IP Network (if not already created)
        private void initRegistry()
        {
            bool stat = false;
            string netSubKey = "Network";
            List<string> regKeyStrValues = new List<string>{ "NetworkPortName", "FirewallDirection", "FirewallProtocol", "FirewallPort", "FirewallRuleName" };
            List<string> regKeyMltiStrValues = new List<string> { "IpAddress", "SubNet", "GateWay", "NameServer" };

            if (RegistryUtil.CheckRegSubKey(SW, company) == false)
            {
                stat = RegistryUtil.CreateRegSubKey(SW, company);
                eventLog1.WriteEntry("Registry Key " + SW + "/" + company + " created: " + stat.ToString(), EventLogEntryType.Information);
            }

            if (RegistryUtil.CheckRegSubKey(SW + "\\" + company, app) == false)
            {
                stat = RegistryUtil.CreateRegSubKey(SW + "\\" + company, app);
                eventLog1.WriteEntry("Registry Key " + app + " created: " + stat.ToString(), EventLogEntryType.Information);
            }

            if (RegistryUtil.CheckRegSubKey(SW + "\\" + company + "\\" + app, netSubKey) == false)
            {
                stat = RegistryUtil.CreateRegSubKey(SW + "\\" + company + "\\" + app, netSubKey);
                eventLog1.WriteEntry("Registry Key " + netSubKey + " created: " + stat.ToString(), EventLogEntryType.Information);
            }

            //Create empty registry values for NetwokPortName, IPaddress, subnet, gateway, & nameserver
            eventLog1.WriteEntry("Adding default registry values", EventLogEntryType.Information);

            foreach (string regKeyVal in regKeyStrValues)
            {
                if (RegistryUtil.SetRegVal(SW + "\\" + company + "\\" + app + "\\" + netSubKey, regKeyVal, (object)String.Empty, Microsoft.Win32.RegistryValueKind.String))
                {
                    eventLog1.WriteEntry(regKeyVal + " string value added successfully", EventLogEntryType.Information);
                }
                else
                {
                    eventLog1.WriteEntry(regKeyVal + " string value was unable to be added", EventLogEntryType.Warning);
                }
            }

            foreach (string regKeyVal in regKeyMltiStrValues)
            {
                if (RegistryUtil.SetRegVal(SW + "\\" + company + "\\" + app + "\\" + netSubKey, regKeyVal, (object)new string[] { }, Microsoft.Win32.RegistryValueKind.MultiString))
                {
                    eventLog1.WriteEntry(regKeyVal + " multistring value added successfully", EventLogEntryType.Information);
                }
                else
                {
                    eventLog1.WriteEntry(regKeyVal + " multistring value was unable to be added", EventLogEntryType.Warning);
                }
            }
        }

        // Remove any/all TPS Firewall Rules
        private void initFirewall()
        {
            // Get a list of all firewall rules containing the TPS firewall rule string 
            List<INetFwRule> rules = Network.findRulesContaining(Network.DEFAULTNAME);
            
            // Remove each rule found
            foreach (INetFwRule rule in rules)
            {
                eventLog1.WriteEntry("Removing firewall rule " + rule.Name, EventLogEntryType.Information);
                Network.RemoveFireWallRule(rule.Name);
            }
        }

        private bool UpdateHostName(string newComputerName)
        {
            // Get the computer name
            String computerPath = string.Format("Win32_ComputerSystem.Name='{0}'", Environment.MachineName);
            // Create a new instance of the Management Object Class
            ManagementObject computerSystemObject = new ManagementObject(new ManagementPath(computerPath));
            
            // Variable set to fail by default
            bool result = false;

            // Get the name of the Rename parameter
            ManagementBaseObject renameParameters = computerSystemObject.GetMethodParameters("Rename");
            // Set the renameParameter value to be what's passed in to this method string newComputerName
            renameParameters["Name"] = newComputerName;

            // Attempt to assign output parameters and return value of the method to "output"
            // "Rename" is the method name being called. "renameParameters" holds the input parameter to the method.
            // "null" is for additional execution options of the method.
            ManagementBaseObject output = computerSystemObject.InvokeMethod("Rename", renameParameters, null);

            // If successful, do the following
            if (output != null)
            {
                // Attempt to change the output ReturnValue property to uint32.
                UInt32 returnValue = (uint)Convert.ChangeType(output.Properties["ReturnValue"].Value, typeof(uint));
                // Assign the returnValue (which should equal 0) to the bool result.
                result = returnValue == 0;
            }

            // Return the result
            return result;
        }

        // Update to DHCP if not already set
        private bool SetNicDhcp(string interfaceName)
        {
            // Set variable to successful DHCP call
            bool stat = true;

            // If not already DHCP
            if (!Network.IsDhcpEnabled(interfaceName))
            {
                // Set DHCP. Return false if failed call. True if successful call.
                stat = Network.SetDhcp(interfaceName);
            }

            // Return result
            return stat;
        }

        // Update the NIC Static IP address
        private bool UpdateNicStaticIp(string interfaceName, string[] newIpAddress, string[] subNet, string[] gateway)
        {
            // set variable to failing / false by default
            bool stat = false;

            // Get the IP Address for the current interface
            System.Net.IPAddress ipAddress = Network.GetIp(interfaceName);
            
            // Do the following if interface is found
            if (ipAddress != null)
            {
                // If the chosen IP address is not already assigned, do the following
                if (ipAddress.ToString() != newIpAddress[0])
                {
                    // set ipaddress. record true if successful. false if not successful
                    stat = Network.SetStaticIp(interfaceName, newIpAddress[0], subNet[0], gateway[0]);
                }
                else
                {
                    // The address is already correctly set. Pass the method call.
                    stat = true;
                }
            }

            // Return result
            return stat;
        }

        // Shut down the computer
        private bool ShutDown(uint flg)
        {
            // Initial variable state set for unsuccessful shutdown 
            bool stat = false;
            AdvancedAPI.TokenPrivLuid tokenPrivLuid;
            IntPtr htok = AdvancedAPI.OpenProcessToken();

            tokenPrivLuid.Count = 1;
            tokenPrivLuid.Luid = 0;
            tokenPrivLuid.Attr = AdvancedAPI.SE_PRIVILEGE_ENABLED;

            // Retrieve the LUID value for shutdown
            Tuple<bool, long> rtn = AdvancedAPI.LookupPrivilegeValue(AdvancedAPI.SE_SHUTDOWN_NAME, tokenPrivLuid.Luid);

            if (rtn.Item1)
            {
                tokenPrivLuid.Luid = rtn.Item2;
            }
            else
            {
                // failed to lookup privilege value
            }

            rtn = null;

            Tuple<bool, AdvancedAPI.TokenPrivLuid> rtnAdj = AdvancedAPI.AdjustTokenPrivileges(htok, tokenPrivLuid);

            // Log off = ExitWindows(0), Shutdown = ...(1), Reboot = ...(2)
            stat = User32.ExitWindows(flg);

            // Return true if successfully called shutdown
            return stat;
        }

        // Update Firewall Ruleset
        private bool UpdateFirewall(int operation)
        {
            bool stat = false;
            // Get reg value for firewall direction, protocol, and port
            object oDirection = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "FirewallDirection", Microsoft.Win32.RegistryValueKind.String);
            object oProtocol  = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "FirewallProtocol", Microsoft.Win32.RegistryValueKind.String);
            object oPort      = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "FirewallPort", Microsoft.Win32.RegistryValueKind.String);
            object oName      = RegistryUtil.GetRegVal(SW + "\\" + company + "\\" + app + "\\Network", "FirewallRuleName", Microsoft.Win32.RegistryValueKind.String);

            if (operation == (int)GPATSUtilsCommands.OpenPort)
            {
                // Add firewall rule
                stat = Network.AddFireWallRule((int)Network.FIREWALL_ACTION.ALLOW, Convert.ToInt32((string)oDirection), Convert.ToInt32((string)oProtocol), Convert.ToInt32((string)oPort));
            }
            else
            {
                // Remove firewall rule
                if ((string)oName != string.Empty && (string)oName != "")
                {
                    stat = Network.RemoveFireWallRule((int)Network.FIREWALL_ACTION.ALLOW, Convert.ToInt32((string)oDirection), Convert.ToInt32((string)oProtocol), (string)oPort, (string)oName);
                }
                else
                {
                    stat = Network.RemoveFireWallRule((int)Network.FIREWALL_ACTION.ALLOW, Convert.ToInt32((string)oDirection), Convert.ToInt32((string)oProtocol), (string)oPort);
                }
            }            

            return stat;
        }
    }
}
