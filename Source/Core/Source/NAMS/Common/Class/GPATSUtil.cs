// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit


namespace NAM
{
    internal static class GPATSUtil
    {
        // Generate enumerations for GPATSUtils Service Commands
        internal enum GPATSUtilsCommands
        {
            // StopWorker begins enum at number 128, RestartWorker is 129, etc.
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

        internal static class Network
        {
            internal static class RegistryKeys
            {
                internal static class StringTypes
                {
                    internal static readonly string Interface   = "NetworkPortName";
                    internal static readonly string FWDirection = "FirewallDirection";
                    internal static readonly string FWProtocol  = "FirewallProtocol";
                    internal static readonly string FWPort      = "FirewallPort";
                    internal static readonly string FWRuleName  = "FirewallRuleName";
                }

                internal static class MultiStringTypes
                {
                    internal readonly static string IpAddress  = "IpAddress";
                    internal readonly static string SubNet     = "SubNet";
                    internal readonly static string GateWay    = "GateWay";
                    internal readonly static string NameServer = "NameServer";
                }

            }

            internal static class J15
            {
                internal const string PORTNAME = "Gigabit1";
                internal const string IP = "192.168.0.1";
                internal const string SUBNET = "255.255.255.0";
                internal const string GATEWAY = "";
            }

            internal static class J16
            {
                internal const string PORTNAME = "Gigabit2";
                internal const string IP = "192.168.200.1";
                internal const string SUBNET = "255.255.255.0";
                internal const string GATEWAY = "192.168.200.2";
            }

            internal static class J18
            {
                internal const string PORTNAME = "Local Area Connection";
                internal const string IP = "";
                internal const string SUBNET = "";
                internal const string GATEWAY = "";
            }

            internal static class J19
            {
                internal const string PORTNAME = "Gigabit4";
                internal const string IP = "192.168.20.1";
                internal const string SUBNET = "255.255.255.0";
                internal const string GATEWAY = "";
            }

            internal static class LocalAreaConnectionX
            {
                internal const string PORTNAME = "Local Area Connection X";
                internal const string IP = "192.168.30.1";
                internal const string SUBNET = "255.255.255.0";
                internal const string GATEWAY = "";
            }

            internal static class LocalAreaConnectionY
            {
                internal const string PORTNAME = "Local Area Connection Y";
                internal const string IP = "192.168.40.1";
                internal const string SUBNET = "255.255.255.0";
                internal const string GATEWAY = "";
            }
        }
    }
}
