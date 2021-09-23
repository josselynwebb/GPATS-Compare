// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Management;  // Add System.Management framework reference from system.management.dll
using System.Text;
using System.Threading.Tasks;
using NetFwTypeLib;       // Add type lib reference from firewallapi.dll

namespace NAM
{
    /// <summary>
    /// <c>Network</c> class
    /// Contains dll imports and methods relating to the Windows IP Helper API library (iphlpapi.dll)
    /// </summary>
    class Network
    {
        private enum ERROR : uint
        {
            ERROR_SUCCESS = 0,
            ERROR_NO_DATA = 232,
            ERROR_BUFFER_OVERFLOW = 111,
            ERROR_INVALID_PARAMETER = 87
        }

        private static readonly List<string> fireWallAction = new List<string>() { "ALLOW", "BLOCK", "MAX" };
        internal enum FIREWALL_ACTION : int
        {
            ALLOW = 0,
            BLOCK = 1,
            MAX = 2
        }

        private static readonly List<string> fireWallDirection = new List<string>() { "INBOUND", "OUTBOUND", "MAX" };
        internal enum FIREWALL_DIRECTION : int
        {
            INBOUND = 0,
            OUTBOUND = 1,
            MAX = 2
        }

        private static readonly List<string> fireWallProtocol = new List<string>() { "TCP", "UDP", "BOTH" };
        internal enum FIREWALL_PROTOCOL : int
        {
            TCP = 0,
            UDP = 1,
            BOTH = 2
        }

        private enum DHCP : int
        {
            DISABLE = 0x00000000,
            ENABLE  = 0x00000001
        }

        private enum NETSH_ACTIONS : int
        {
            DHCP_RELEASE = 0,
            DHCP_RENEW,
            DHCP,
            STATIC
        }

        private static readonly List<int> AllowedPorts = new List<int> { 55555, 55556 };
        public const int MAXFWPORT = 65535;
        public const string DEFAULTNAME = "TPS_FW_RULE_";
        private const string clsidFireWall = "{304CE942-6E39-40D8-943A-B913C40C9CD4}";

        /// <summary>
        /// Gets information on the IP adapters on the machine.
        /// </summary>
        /// <param name="Family">0 (AF_UNSPEC) = IPv4 & IPv6, 2 (AF_INET) = IPv4 only, 23 (AF_INET6) = IPv6 only</param>
        /// <param name="Flags">The type of address to retrieve</param>
        /// <param name="Reserved">Unused / for future use. Pass null to this.</param>
        /// <param name="pAdapterAddresses">pointer to a buffer that contains a linked list 
        ///     of IP_ADAPTER_ADDRESSES structures on successful return</param>
        /// <param name="pOutBufLen">pointer to a variable that specifies the size 
        ///     of the buffer pointed to by AdapterAddresses</param>
        /// <returns>returns a range of failing codes or a success code. Success = ERROR_SUCCESS (NO_ERROR)</returns>
        [DllImport("iphlpapi.dll")]
        private static extern ERROR GetAdaptersAddresses(uint Family, uint Flags, IntPtr Reserved, IntPtr pAdapterAddresses, ref uint pOutBufLen);

        /// <summary>
        /// Adds an IP address to an interface specified by the interface index
        /// </summary>
        /// <param name="Address">The IPv4 address to add to the adapter, in the form of an IPAddr structure</param>
        /// <param name="IpMaskint"><The subnet mask/param>
        /// <param name="IfIndex">Index of the adapter</param>
        /// <param name="NTEContext">A pointer to a ULONG variable. If successful retrun, points 
        ///     to the Net Table Entry Context for the IPv4 address that was added.</param>
        /// <param name="NTEInstance">A pointer to a ULONG variable. If successful retrun, points 
        ///     to the Net Table Entry Instance for the IPv4 address that was added.</param>
        /// <returns>Successful return = NO_ERROR. Failed return returns a range of return codes.</returns>
        [DllImport("iphlpapi.dll", EntryPoint = "AddIPAddress", SetLastError = true)]
        private static extern UInt32 AddIPAddress(UInt32 Address, UInt32 IpMaskint, int IfIndex, out IntPtr NTEContext, out IntPtr NTEInstance);

        /// <summary>
        /// Provides "adapter index" to be used with other IP Helper API functions based on the adapter name string in the form of "\\DEVICE\\TCPIP_" + deviceGuid.
        /// </summary>
        /// <param name="adapter">Pointer to a Unicode string that specifies the name of the adapter.</param>
        /// <param name="index">Pointer to a ULONG variable that points to the index of the adapter.</param>
        /// <returns>Successful return = NO_ERROR.</returns>
        [DllImport("iphlpapi.dll", SetLastError = true)]
        static extern int GetAdapterIndex(string adapter, out int index);

        /// <summary>
        /// Obtains a list of the network interface adapters
        /// </summary>
        /// <param name="PIfTableBuffer">A pointer to a buffer that specifies an IP_INTERFACE_INFO
        ///     structure that receives the list of adapters. This buffer must be allocated by the caller.</param>
        /// <param name="size">Pointer to DWORD variable which specifies the size of the buffer pointed
        ///     to by PIfTableBuffer to receive IP_INTERFACE_INFO structure.</param>
        /// <returns></returns>
        [DllImport("Iphlpapi.dll", CharSet = CharSet.Auto)]
        public static extern int GetInterfaceInfo(Byte[] PIfTableBuffer, ref int size);

        // Return a list of all available Network Interface Cards
        private static List<System.Net.NetworkInformation.NetworkInterface> GetAllWiredNics()
        {
            // Create a new list of networkAdapters and the networkInterfaceTypes
            List<System.Net.NetworkInformation.NetworkInterface> networkAdapters = new List<System.Net.NetworkInformation.NetworkInterface>();
            List<System.Net.NetworkInformation.NetworkInterfaceType> networkInterfaceTypes = new List<System.Net.NetworkInformation.NetworkInterfaceType>()
            {
                System.Net.NetworkInformation.NetworkInterfaceType.Ethernet,
                System.Net.NetworkInformation.NetworkInterfaceType.FastEthernetFx,
                System.Net.NetworkInformation.NetworkInterfaceType.FastEthernetT,
                System.Net.NetworkInformation.NetworkInterfaceType.GigabitEthernet,
                System.Net.NetworkInformation.NetworkInterfaceType.Ethernet3Megabit
            };

            // Get all the network interfaces, then do it again
            System.Net.NetworkInformation.NetworkInterface[] networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            
            // For all network interfaces found do the following
            foreach (System.Net.NetworkInformation.NetworkInterface networkInterface in networkInterfaces)
            {
                // For debugging purpose, record its info
                System.Diagnostics.Debug.Print("_________________________");
                System.Diagnostics.Debug.Print("ID: " + networkInterface.Id);
                System.Diagnostics.Debug.Print("Name:  " + networkInterface.Name);
                System.Diagnostics.Debug.Print("Descr:  " + networkInterface.Description);
                System.Diagnostics.Debug.Print("Type:  " + networkInterface.NetworkInterfaceType.ToString());
                System.Diagnostics.Debug.Print("Status:  " + networkInterface.OperationalStatus.ToString());
                System.Diagnostics.Debug.Print("MAC:  " + networkInterface.GetPhysicalAddress().ToString());
                try
                {
                    // Get the IPv4 Properties for the current network interface
                    System.Net.NetworkInformation.IPv4InterfaceProperties ipv4Properties = networkInterface.GetIPProperties().GetIPv4Properties();
                    System.Diagnostics.Debug.Print("DHCP:  " + ipv4Properties.IsDhcpEnabled);
                }
                catch (System.Net.NetworkInformation.NetworkInformationException)
                {
                    System.Diagnostics.Debug.Print("DHCP:  N/A");
                }

                System.Diagnostics.Debug.Print("IP Count  " + networkInterface.GetIPProperties().UnicastAddresses.Count);

                if (networkInterfaceTypes.Contains(networkInterface.NetworkInterfaceType))
                {
                    networkAdapters.Add(networkInterface);
                }

                // Get the unicast addresses assigned to this interface
                System.Net.NetworkInformation.UnicastIPAddressInformationCollection IPCollection = networkInterface.GetIPProperties().UnicastAddresses;

                // For each assigned address
                foreach (System.Net.NetworkInformation.UnicastIPAddressInformation IPInfo in IPCollection)
                {
                    // If they match what is in the network list
                    if (IPInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        System.Diagnostics.Debug.Print("IP Addr:  " + IPInfo.Address);
                    }
                }
            }

            // Return the list of network adapters
            return networkAdapters;
        }

        // Return Network Interface Controller, if present
        private static System.Net.NetworkInformation.NetworkInterface GetNic(List<System.Net.NetworkInformation.NetworkInterface> Adapters, string interfaceName)
        { 
            // for every adapter listed in adapters
            foreach (System.Net.NetworkInformation.NetworkInterface adapter in Adapters)
            {
                // If it matches with interfaceName
                if (adapter.Name.Equals(interfaceName, StringComparison.OrdinalIgnoreCase))
                {
                    // Retrun the adapter
                    return adapter;
                }
            }

            // Else, return null
            return null;
        }

        // Return true/false is DHCP enabled
        private static bool IsDhcpEnabled(System.Net.NetworkInformation.NetworkInterface networkInterface)
        {
            return networkInterface.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
        }

        // Return the Network Interface Card Index, if available
        private static int GetNicIndex(string interfaceName)
        {
            // Get a list of all wired NICs
            List<System.Net.NetworkInformation.NetworkInterface> adapters = Network.GetAllWiredNics();

            // Get the adapter for the selected current interfacename
            System.Net.NetworkInformation.NetworkInterface adapter = GetNic(adapters, interfaceName);

            // Execute the following if the current adapter is not null
            if (adapter != null)
            {
                //return adapter index 
                return adapter.GetIPProperties().GetIPv4Properties().Index;
            }

            // If not found, return -1
            return -1;
        }

        // Return the Network Interface Card ID, if available
        private static string GetNicId(string interfaceName)
        {
            // Get a list of all wired NICs
            List<System.Net.NetworkInformation.NetworkInterface> adapters = Network.GetAllWiredNics();

            // Get the adapter for the selected current interfacename
            System.Net.NetworkInformation.NetworkInterface adapter = GetNic(adapters, interfaceName);

            // Execute the following if the current adapter is not null
            if (adapter != null)
            {
                // Return the NIC ID
                return adapter.Id;
            }

            // Return null if not found
            return null;
        }

        // Return the associated IP Address, if available
        private static System.Net.NetworkInformation.UnicastIPAddressInformation GetIpInfo(System.Net.NetworkInformation.NetworkInterface networkInterface)
        {
            // Get the IP congig
            System.Net.NetworkInformation.IPInterfaceProperties ipProperties = networkInterface.GetIPProperties();

            // If not null, do the following
            if (ipProperties != null)
            {
                // Get the unicast addresses assigned to this interface
                System.Net.NetworkInformation.UnicastIPAddressInformationCollection ipInfoColl = ipProperties.UnicastAddresses;

                // For each assigned address
                foreach (System.Net.NetworkInformation.UnicastIPAddressInformation ipInfo in ipInfoColl)
                {
                    // If the IP Address of the Address Family = the address for IP version 4
                    if (ipInfo.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        // Return the associated IP Address
                        return ipInfo;
                    }
                }
            }

            // If not found, return null
            return null;
        }

        private static bool ExecNetsh(string interfaceName, int action, string ipAddress = null, string subNet = null, string gw = null)
        {
            const string NETSH = "netsh";
            const int SUCCESS = 0;
            const string DISABLE = " admin=DISABLE";
            const string ENABLE = " admin=ENABLE";
            const string STATIC_IP = " static ";
            const string DHCP_IP = " source=dhcp";
            const string METRIC = " 1";
            string interfaceString = "";

            System.Diagnostics.ProcessStartInfo pStartInfo = null;
            System.Diagnostics.Process process = null;

            switch (action)
            {
                case (int)NETSH_ACTIONS.DHCP_RELEASE:
                    interfaceString = "interface set interface name=" + Convert.ToChar(34) + interfaceName + Convert.ToChar(34) + DISABLE;
                    break;

                case (int)NETSH_ACTIONS.DHCP_RENEW:
                    interfaceString = "interface set interface name=" + Convert.ToChar(34) + interfaceName + Convert.ToChar(34) + ENABLE;
                    break;

                case (int)NETSH_ACTIONS.DHCP:
                    interfaceString = "interface ipv4 set address name=" + Convert.ToChar(34) + interfaceName + Convert.ToChar(34) + DHCP_IP;
                    break;

                case (int)NETSH_ACTIONS.STATIC:
                    if (ipAddress != null)
                    {
                        if (subNet == null)
                        {
                            subNet = "255.255.255.0";
                        }

                        if (gw == null)
                        {
                            gw = "";
                        }

                        interfaceString = "interface ipv4 set address name=" + Convert.ToChar(34) + interfaceName + Convert.ToChar(34) + STATIC_IP + ipAddress + " " + subNet + " " + gw + METRIC;
                    }
                    break;

                default:
                    return false;
            }

            if (interfaceString != "")
            {
                pStartInfo = new System.Diagnostics.ProcessStartInfo(NETSH, interfaceString);
                pStartInfo.CreateNoWindow = true;
                pStartInfo.UseShellExecute = false;
                process = new System.Diagnostics.Process();
                process.StartInfo = pStartInfo;
                process.Start();
                process.WaitForExit();

                if (process.ExitCode == SUCCESS)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Check if network interface exists
        /// </summary>
        /// <param name="interfaceName">selected interface name</param>
        /// <returns>True if interface exists, False if not</returns>
        public static bool InterfaceExists(string interfaceName)
        {
            // Set fail by default
            bool stat = false;

            // Get a list of all wired NICs
            List<System.Net.NetworkInformation.NetworkInterface> adapters = Network.GetAllWiredNics();

            // for every adapter listed in adapters
            foreach (System.Net.NetworkInformation.NetworkInterface adapter in adapters)
            {
                // If it matches with interfaceName
                if (adapter.Name.Equals(interfaceName, StringComparison.OrdinalIgnoreCase))
                {
                    // Set status to true
                    stat = true;
                }
            }

            return stat;
        }

        /// <summary>
        /// Return true/false is DHCP enabled for the specific interface
        /// </summary>
        /// <param name="interfaceName">selected interface name</param>
        /// <returns>true/false is DHCP enabled</returns>
        public static bool IsDhcpEnabled(string interfaceName)
        {
            // Set fail by default
            bool stat = false;

            // Get a list of all wired NICs
            List<System.Net.NetworkInformation.NetworkInterface> adapters = Network.GetAllWiredNics();

            // Get the adapter for the selected current interfacename
            System.Net.NetworkInformation.NetworkInterface adapter = GetNic(adapters, interfaceName);

            // Execute the following if the current adapter is not null
            if (adapter != null)
            {
                // Set true if the current adapter has DCHP enabled
                stat = IsDhcpEnabled(adapter);
            }

            // Return result
            return stat;
        }

        /// <summary>
        /// Set the assigned Interface to DHCP
        /// </summary>
        /// <param name="InterfaceName">delagate used for obtaining NIC ID and NIC Index</param>
        /// <returns>true/false for successful SetRegVal method call</returns>
        //public static bool SetDhcp(string InterfaceName)
        //{
        //    // declare variables
        //    bool stat = true;
        //    string[] mltiStr = { };
        //    string interfaceId = GetNicId(InterfaceName);
        //    int interfaceIndex = GetNicIndex(InterfaceName);
        //    string key = @"SYSTEM\ControlSet001\Services\Tcpip\Parameters\Interfaces\" + interfaceId;

        //    // Return true/false if successfully called SetRegVal to Enable DHCP
        //    stat = RegistryUtil.SetRegVal(key, "EnableDHCP", (object)DHCP.ENABLE, Microsoft.Win32.RegistryValueKind.DWord);

        //    // If true
        //    if (stat)
        //    {
        //        // Stat will be false if any of the following are false
        //        stat = RegistryUtil.RemoveRegVal(key, "DefaultGateway")
        //        && RegistryUtil.RemoveRegVal(key, "DefaultGatewayMetric")               
        //        && RegistryUtil.SetRegVal(key, "IPAddress", (object)mltiStr, Microsoft.Win32.RegistryValueKind.MultiString)
        //        && RegistryUtil.SetRegVal(key, "NameServer", (object)string.Empty, Microsoft.Win32.RegistryValueKind.String)
        //        && RegistryUtil.SetRegVal(key, "SubnetMask", (object)mltiStr, Microsoft.Win32.RegistryValueKind.MultiString)
        //        && ResetNic(InterfaceName);
        //    }
            
        //    // Return true if all SetRegVal were successfully executed. False if any fail.
        //    return stat;
        //}

        //public static bool SetDhcp(string InterfaceName)
        //{
        //    string interfaceDescr = InterfaceName;
        //    int interfaceIndex = GetNicIndex(InterfaceName);
        //    ManagementClass mgmntClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection mgmntObjColl = mgmntClass.GetInstances();

        //    System.Net.NetworkInformation.NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        //    System.Net.NetworkInformation.NetworkInterface networkInterface = null; // = interfaces.First(x => x.Name == InterfaceName);

        //    foreach (System.Net.NetworkInformation.NetworkInterface ifc in interfaces)
        //    {
        //        if (ifc.Name.ToUpper() == InterfaceName.ToUpper())
        //        {
        //            networkInterface = ifc;
        //            interfaceDescr = networkInterface.Name;
        //            break;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    foreach (ManagementObject mgmntObj in mgmntObjColl)
        //    {
        //        if ((bool)mgmntObj["IPEnabled"] == true && mgmntObj["Description"].Equals(interfaceDescr) == true)
        //        {
        //            try
        //            {
        //                ManagementBaseObject dnsConfig = mgmntObj.GetMethodParameters("SetDNSServerSearchOrder");

        //                dnsConfig["DNSServerSearchOrder"] = null;
        //                ManagementBaseObject enableDHCP = mgmntObj.InvokeMethod("EnableDHCP", null, null);
        //                ManagementBaseObject setDNS = mgmntObj.InvokeMethod("SetDNSServerSearchOrder", dnsConfig, null);
        //                ResetNic(interfaceIndex);
        //            }
        //            catch (Exception e)
        //            {
        //                //System.Windows.Forms.MessageBox.Show(e.Message);
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        public static bool SetDhcp(string InterfaceName)
        {
            if (ExecNetsh(InterfaceName, (int)NETSH_ACTIONS.DHCP))
            {
                return ResetNic(InterfaceName);
            }

            return false;
        }

        /// <summary>
        /// Return the IP Address of the Interface, if available
        /// </summary>
        /// <param name="interfaceName">selected interface name</param>
        /// <returns>If available, return IP Address. If not, return null</returns>
        public static System.Net.IPAddress GetIp(string interfaceName)
        {
            // Retrieve all wired NICs
            List<System.Net.NetworkInformation.NetworkInterface> adapters = Network.GetAllWiredNics();

            // Get the adapter for the current interfaceName
            System.Net.NetworkInformation.NetworkInterface adapter = Network.GetNic(adapters, interfaceName);

            // As long as not null, do the following
            if (adapter != null)
            {
                // Get the IP Address for the current adapter
                System.Net.NetworkInformation.UnicastIPAddressInformation ipInfo = Network.GetIpInfo(adapter);

                // Return the IP Address of current adapter
                return ipInfo.Address;
            }

            // Return null if not available
            return null;
        }

        /// <summary>
        /// Set the Static IP Address
        /// </summary>
        /// <param name="InterfaceName">interface name delegate</param>
        /// <param name="IPAddress">IP Address delegate</param>
        /// <param name="SubnetMask">subnet mask delagate</param>
        /// <param name="Gateway">gateway delegate</param>
        /// <returns>true/false if successfully called SetRegVal</returns>
        //public static bool SetStaticIp(string InterfaceName, string IPAddress, string SubnetMask, string Gateway)
        //{
        //    // declare variables
        //    bool stat = true;
        //    string[] mltiStr = { };
        //    string interfaceId = GetNicId(InterfaceName);
        //    int interfaceIndex = GetNicIndex(InterfaceName);
        //    string[] newIP = { IPAddress };
        //    string[] newSubnet = { SubnetMask };
        //    string[] newGateway = { Gateway };
        //    string key = @"SYSTEM\ControlSet001\Services\Tcpip\Parameters\Interfaces\" + interfaceId;

        //    // Return true/false if successfully called SetRegVal to Disable the DHCP
        //    stat = RegistryUtil.SetRegVal(key, "EnableDHCP", (object)DHCP.DISABLE, Microsoft.Win32.RegistryValueKind.DWord);

        //    if (stat)
        //    {
        //        // Stat will be false if any of the following are false
        //        stat = RegistryUtil.SetRegVal(key, "DefaultGateway", (object)newGateway, Microsoft.Win32.RegistryValueKind.MultiString)
        //            && RegistryUtil.SetRegVal(key, "DefaultGatewayMetric", (object)new string[] { "0" }, Microsoft.Win32.RegistryValueKind.MultiString)
        //            && RegistryUtil.RemoveRegVal(key, "DhcpDefaultGateway")
        //            && RegistryUtil.RemoveRegVal(key, "DhcpDomain")
        //            && RegistryUtil.RemoveRegVal(key, "DhcpIPAddress")
        //            && RegistryUtil.RemoveRegVal(key, "DhcpNameServer")
        //            && RegistryUtil.SetRegVal(key, "DhcpServer", (object)"255.255.255.255", Microsoft.Win32.RegistryValueKind.String)
        //            && RegistryUtil.RemoveRegVal(key, "DhcpSubnetMask")
        //            && RegistryUtil.RemoveRegVal(key, "DhcpSubnetMaskOpt")
        //            && RegistryUtil.SetRegVal(key, "EnableDHCP", (object)0, Microsoft.Win32.RegistryValueKind.DWord)
        //            && RegistryUtil.SetRegVal(key, "IPAddress", (object)newIP, Microsoft.Win32.RegistryValueKind.MultiString)
        //            && RegistryUtil.SetRegVal(key, "SubnetMask", (object)newSubnet, Microsoft.Win32.RegistryValueKind.MultiString)
        //            && ResetNic(InterfaceName);
        //    }

        //    // Return true if successfully called SetRegVal, false if failed
        //    return stat;
        //}

        //public static bool SetStaticIp(string InterfaceName, string IPAddress, string SubnetMask, string GateWay = null, string dns1 = null, string dns2 = null)
        //{
        //    string interfaceDescr = InterfaceName;
        //    int interfaceIndex = GetNicIndex(InterfaceName);
        //    ManagementClass mgmntClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
        //    ManagementObjectCollection mgmntObjColl = mgmntClass.GetInstances();

        //    System.Net.NetworkInformation.NetworkInterface[] interfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        //    System.Net.NetworkInformation.NetworkInterface networkInterface = null; // = interfaces.First(x => x.Name == InterfaceName);
            
        //    foreach (System.Net.NetworkInformation.NetworkInterface ifc in interfaces)
        //    {
        //        if (ifc.Name.ToUpper() == InterfaceName.ToUpper())
        //        {
        //            networkInterface = ifc;
        //            interfaceDescr = networkInterface.Name;
        //            break;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }

        //    foreach (ManagementObject mgmntObj in mgmntObjColl)
        //    {
        //        if ((bool)mgmntObj["IPEnabled"] == true && mgmntObj["Description"].Equals(interfaceDescr) == true)
        //        {
        //            try
        //            {
        //                ManagementBaseObject ipConfig = mgmntObj.GetMethodParameters("EnableStatic");

        //                ipConfig["IPAddress"] = new string[] { IPAddress };
        //                ipConfig["SubnetMask"] = new string[] { SubnetMask };

        //                ManagementBaseObject setIP = mgmntObj.InvokeMethod("EnableStatic", ipConfig, null);

        //                if (GateWay != null)
        //                {
        //                    ManagementBaseObject gwConfig = mgmntObj.GetMethodParameters("SetGateways");

        //                    gwConfig["DefaultIPGateway"] = new string[] { GateWay };
        //                    gwConfig["GatewayCostMetric"] = new int[] { 1 };

        //                    ManagementBaseObject setGW = mgmntObj.InvokeMethod("SetGateways", gwConfig, null);
        //                }

        //                if (dns1 != null || dns2 != null)
        //                {
        //                    ManagementBaseObject dnsConfig = mgmntObj.GetMethodParameters("SetDNSServerSearchOrder");

        //                    List<string> dns = new List<string>();

        //                    if (dns1 != null)
        //                    {
        //                        dns.Add(dns1);
        //                    }

        //                    if (dns2 != null)
        //                    {
        //                        dns.Add(dns2);
        //                    }

        //                    dnsConfig["DNSServerSearchOrder"] = dns.ToArray();

        //                    ManagementBaseObject setDNS = mgmntObj.InvokeMethod("SetDNSServerSearchOrder", dnsConfig, null);

        //                    ResetNic(interfaceIndex);
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                //System.Windows.Forms.MessageBox.Show(e.Message);
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}

        public static bool SetStaticIp(string InterfaceName, string IPAddress, string SubnetMask, string Gateway)
        {
            if (ExecNetsh(InterfaceName, (int)NETSH_ACTIONS.STATIC, IPAddress, SubnetMask, Gateway))
            {
                return ResetNic(InterfaceName);
            }

            return false;
        }

        // Disable the Network Interface Card for the selected interface index
        //private static bool DisableNic(int interfaceIndex)
        //{
        //    // Create new instance of the ManagementClass and retrieve all instances
        //    using (System.Management.ManagementClass objMC = new System.Management.ManagementClass("Win32_NetworkAdapterConfiguration"))
        //    {
        //        using (System.Management.ManagementObjectCollection objMOC = objMC.GetInstances())
        //        {
        //            foreach (System.Management.ManagementObject objMO in objMOC)
        //            {
        //                if (Convert.ToInt32(objMO["Index"]) == interfaceIndex)
        //                {
        //                    object obj = objMO.InvokeMethod("Disable", new object[] { });
        //                    System.Threading.Thread.Sleep(1000);

        //                    return true;
        //                }
        //            }
        //        }
        //    }

        //    // Return failed - did not disable the selected interfaceIndex NIC
        //    return false;
        //}
        
        private static bool DisableNic(string interfaceName)
        {
            return ExecNetsh(interfaceName, (int)NETSH_ACTIONS.DHCP_RELEASE);
        }

        // Enable the Network Interface Card for the selected interface index
        //private static bool EnableNic(int interfaceIndex)
        //{
        //    // Create new instance of the ManagementClass and retrieve all instances
        //    using (System.Management.ManagementClass objMC = new System.Management.ManagementClass("Win32_NetworkAdapter"))
        //    {
        //        using (System.Management.ManagementObjectCollection objMOC = objMC.GetInstances())
        //        {
        //            foreach (System.Management.ManagementObject objMO in objMOC)
        //            {
        //                if (Convert.ToInt32(objMO["Index"]) == interfaceIndex)
        //                {
        //                    object obj = objMO.InvokeMethod("Enable", null);
        //                    System.Threading.Thread.Sleep(1000);

        //                    return true;
        //                }
        //            }
        //        }
        //    }

        //    // Return failed - did not enable the selected interfaceIndex NIC
        //    return false;
        //}

        private static bool EnableNic(string interfaceName)
        {
            return ExecNetsh(interfaceName, (int)NETSH_ACTIONS.DHCP_RENEW);
        }

        // Reset the Network Interface Card for the selected interface index
        //private static bool ResetNic(int interfaceIndex)
        //{
        //    using (System.Management.ManagementClass objMC = new System.Management.ManagementClass("Win32_NetworkAdapter"))
        //    {
        //        using (System.Management.ManagementObjectCollection objMOC = objMC.GetInstances())
        //        {
        //            foreach (System.Management.ManagementObject objMO in objMOC)
        //            {
        //                if (Convert.ToInt32(objMO["Index"]) == interfaceIndex)
        //                {
        //                    object obj = objMO.InvokeMethod("Disable", new object[] { });
        //                    System.Threading.Thread.Sleep(1000);
        //                    obj = objMO.InvokeMethod("Enable", null);
        //                    System.Threading.Thread.Sleep(2000);

        //                    return true;
        //                }
        //            }
        //        }
        //    }

        //    return false;
        //}

        private static bool ResetNic(string interfaceName)
        {
            if (ExecNetsh(interfaceName, (int)NETSH_ACTIONS.DHCP_RELEASE))
            {
                return ExecNetsh(interfaceName, (int)NETSH_ACTIONS.DHCP_RENEW);
            }

            return false;
        }

        /// <summary>
        /// Validate given IP Address with regular expression
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <returns></returns>
        public static bool IsValidIp(string ipAddr)
        {
            bool result = false;
            System.Net.IPAddress ipAddress;

            // Valid Pattern to match
            string regExPattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";
            
            //Regular Expression object    
            System.Text.RegularExpressions.Regex regExValidate = new System.Text.RegularExpressions.Regex(regExPattern);

            if (!string.IsNullOrEmpty(ipAddr))
            {
                if (regExValidate.IsMatch(ipAddr))
                {
                    result = System.Net.IPAddress.TryParse(ipAddr, out ipAddress);
                }
            }

            return result;
        }

        #region Firewall methods

        /// <summary>
        /// Find all Firewall Rules by Port Number
        /// </summary>
        /// <param name="portNum"></param>
        /// <returns>List of Firewall Rules</returns>
        private static List<INetFwRule> findRulesByPort(int portNum)
        {
            bool portFound = false;
            string[] ports;
            List<INetFwRule> rulesWithPort = new List<INetFwRule>();

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRules firewallRules = firewallPolicy.Rules;

            foreach (INetFwRule firewallRule in firewallRules)
            {
                portFound = false;

                if (firewallRule.LocalPorts != null)
                {
                    if (firewallRule.LocalPorts.Contains(","))
                    {
                        ports = firewallRule.LocalPorts.Split(',');

                        foreach (string port in ports)
                        {

                            if (port == portNum.ToString())
                            {
                                portFound = true;
                            }
                        }
                    }
                    else if (firewallRule.LocalPorts.Contains("-"))
                    {
                        ports = firewallRule.LocalPorts.Split('-');
                        int low;
                        int hi;

                        try
                        {
                            low = Convert.ToInt32(ports[0]);
                            hi = Convert.ToInt32(ports[1]);
                        }
                        catch (Exception Ex)
                        {
                            throw;
                        }

                        if (portNum < hi && portNum > low)
                        {
                            portFound = true;
                        }
                    }
                    else
                    {

                        if (firewallRule.LocalPorts == portNum.ToString())
                        {
                            portFound = true;
                        }
                    }

                    if (portFound)
                    {
                        rulesWithPort.Add(firewallRule);
                    }
                }
            }

            return rulesWithPort;
        }

        /// <summary>
        /// Find all Firewall Rules by Name
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns>List of Firewall Rules</returns>
        private static List<INetFwRule> findRulesByName(string ruleName)
        {
            bool nameFound = false;
            List<INetFwRule> matchingRules = new List<INetFwRule>();

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRules firewallRules = firewallPolicy.Rules;

            foreach (INetFwRule firewallRule in firewallRules)
            {
                nameFound = false;

                if (firewallRule.Name != null)
                {
                    if (firewallRule.Name == ruleName)
                    {
                        nameFound = true;
                    }

                    if (nameFound)
                    {
                        matchingRules.Add(firewallRule);
                    }
                }
            }

            return matchingRules;
        }

        /// <summary>
        /// Find all Firewall Rules that have a given phrase in the name
        /// </summary>
        /// <param name="ruleName"></param>
        /// <returns>List of Firewall Rules</returns>
        public static List<INetFwRule> findRulesContaining(string ruleNamePhrase)
        {
            bool nameFound = false;
            List<INetFwRule> matchingRules = new List<INetFwRule>();

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRules firewallRules = firewallPolicy.Rules;

            foreach (INetFwRule firewallRule in firewallRules)
            {
                nameFound = false;

                if (firewallRule.Name != null)
                {
                    if (firewallRule.Name.Contains(ruleNamePhrase))
                    {
                        nameFound = true;
                    }

                    if (nameFound)
                    {
                        matchingRules.Add(firewallRule);
                    }
                }
            }

            return matchingRules;
        }

        /// <summary>
        /// Find all Firewall Rules that allow traffic thru
        /// </summary>
        /// <param name="rules"></param>
        /// <returns>List of Firewall Rules</returns>
        private static List<INetFwRule> findAllowed(List<INetFwRule> rules)
        {
            List<INetFwRule> rulesAllowed = new List<INetFwRule>();

            foreach (INetFwRule rule in rules)
            {
                if (rule.Action == NET_FW_ACTION_.NET_FW_ACTION_ALLOW)
                {
                    rulesAllowed.Add(rule);
                }
            }

            return rulesAllowed;
        }

        /// <summary>
        /// Find all Firewall Rules that are enabled
        /// </summary>
        /// <param name="rules"></param>
        /// <returns>List of Firewall Rules</returns>
        private static List<INetFwRule> findEnabled(List<INetFwRule> rules)
        {
            List<INetFwRule> rulesEnabled = new List<INetFwRule>();

            foreach (INetFwRule rule in rules)
            {
                if (rule.Enabled == true)
                {
                    rulesEnabled.Add(rule);
                }
            }

            return rulesEnabled;
        }

        /// <summary>
        /// Evaluate whether given port is a valid port number or not
        /// </summary>
        /// <param name="portNum"></param>
        /// <returns>true if yes, false if no</returns>
        public static bool IsPortValid(int portNum)
        {
            if (portNum >= 0 && portNum <= MAXFWPORT)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Evaluate whether given port is open or not
        /// </summary>
        /// <param name="portNum"></param>
        /// <returns>true if open, false if not</returns>
        private static bool isPortOpen(int portNum)
        {

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRules firewallRules = firewallPolicy.Rules;

            List<INetFwRule> found = findRulesByPort(portNum);

            found = findAllowed(found);
            found = findEnabled(found);

            if (found.Count() > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Evaluate whether or not port is authorized for use
        /// </summary>
        /// <param name="portNum"></param>
        /// <returns>true / false</returns>
        public static bool isPortAuthorized(int portNum)
        {
            if (AllowedPorts.IndexOf(portNum) > -1)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Add Firewall Rule to Firewall Policy
        /// </summary>
        /// <param name="action"></param>
        /// <param name="direction"></param>
        /// <param name="protocol"></param>
        /// <param name="portNum"></param>
        /// <param name="name"></param>
        /// <param name="descr"></param>
        /// <returns></returns>
        public static bool AddFireWallRule(int action, int direction, int protocol, int portNum, string name = DEFAULTNAME, string descr = "Rule Created By TPS")
        {
            if (action > (int)FIREWALL_ACTION.MAX || action < (int)FIREWALL_ACTION.ALLOW)
            {
                return false;
            }

            if (direction > (int)FIREWALL_DIRECTION.MAX || direction < (int)FIREWALL_DIRECTION.INBOUND)
            {
                return false;
            }

            if (protocol > (int)FIREWALL_PROTOCOL.BOTH || protocol < (int)FIREWALL_PROTOCOL.TCP)
            {
                return false;
            }

            // Check if port is authorized for use
            if (!isPortAuthorized(Convert.ToInt32(portNum)))
            {
                return false;
            }

            if (isPortOpen(Convert.ToInt32(portNum)))
            {
                return true;
            }

            if (name == DEFAULTNAME)
            {
                name = name + fireWallAction[action] + "_" + fireWallDirection[direction] + "_" + fireWallProtocol[protocol] + "_" + portNum;
            }

            INetFwRule2 fireWallRule = (INetFwRule2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FWRule"));

            switch (action)
            {
                case (int)FIREWALL_ACTION.ALLOW:
                    fireWallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_ALLOW;
                    break;

                case (int)FIREWALL_ACTION.BLOCK:
                    fireWallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_BLOCK;
                    break;

                case (int)FIREWALL_ACTION.MAX:
                    fireWallRule.Action = NET_FW_ACTION_.NET_FW_ACTION_MAX;
                    break;
            }

            switch (direction)
            {
                case (int)FIREWALL_DIRECTION.INBOUND:
                    fireWallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_IN;
                    break;

                case (int)FIREWALL_DIRECTION.OUTBOUND:
                    fireWallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_OUT;
                    break;

                case (int)FIREWALL_DIRECTION.MAX:
                    fireWallRule.Direction = NET_FW_RULE_DIRECTION_.NET_FW_RULE_DIR_MAX;
                    break;
            }

            switch (protocol)
            {
                case (int)FIREWALL_PROTOCOL.TCP:
                    fireWallRule.Protocol = (int)NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP;
                    break;

                case (int)FIREWALL_PROTOCOL.UDP:
                    fireWallRule.Protocol = (int)NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP;
                    break;

                case (int)FIREWALL_PROTOCOL.BOTH:
                    fireWallRule.Protocol = (int)NetFwTypeLib.NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY;
                    break;
            }

            
            fireWallRule.LocalPorts = portNum.ToString();
            fireWallRule.Name = name;
            fireWallRule.Description = descr;
            fireWallRule.InterfaceTypes = "All";
            fireWallRule.Enabled = true;

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            firewallPolicy.Rules.Add(fireWallRule);

            return true;
        }

        /// <summary>
        /// Remove Firewall Rule from Firewall Policy with given name
        /// </summary>
        /// <param name="action"></param>
        /// <param name="direction"></param>
        /// <param name="protocol"></param>
        /// <param name="portNum"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool RemoveFireWallRule(int action, int direction, int protocol, string portNum, string name = DEFAULTNAME)
        {
            // Check if port is authorized for use
            if (!isPortAuthorized(Convert.ToInt32(portNum)))
            {
                return false;
            }

            if (name == DEFAULTNAME)
            {
                name = name + fireWallAction[action] + "_" + fireWallDirection[direction] + "_" + fireWallProtocol[protocol] + "_" + portNum;
            }

            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRules firewallRules = firewallPolicy.Rules;

            List<INetFwRule> found = findRulesByName(name);

            foreach (INetFwRule rule in found)
            {
                firewallPolicy.Rules.Remove(rule.Name);
            }

            return true;
        }

        public static bool RemoveFireWallRule(string name)
        {
            INetFwPolicy2 firewallPolicy = (INetFwPolicy2)Activator.CreateInstance(Type.GetTypeFromProgID("HNetCfg.FwPolicy2"));

            INetFwRules firewallRules = firewallPolicy.Rules;

            List<INetFwRule> found = findRulesByName(name);

            foreach (INetFwRule rule in found)
            {
                firewallPolicy.Rules.Remove(rule.Name);
            }

            return true;
        }

        #endregion
    }
}
