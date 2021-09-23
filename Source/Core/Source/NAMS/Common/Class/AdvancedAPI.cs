using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NAM
{
    /// <summary>
    /// <c>AdvancedAPI</c> class
    /// Contains dll imports and methods relating to the Windows Advanced API Services library (advapi32.dll)
    /// </summary>
    static class AdvancedAPI
    {
        #region Properties
        [Flags]
        internal enum ServiceType : int
        {
            SERVICE_KERNEL_DRIVER = 0x00000001,
            SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
            SERVICE_WIN32_OWN_PROCESS = 0x00000010,
            SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
            SERVICE_INTERACTIVE_PROCESS = 0x00000100
        }

        [Flags]
        internal enum CONTROLS_ACCEPTED : int
        {
            SERVICE_ACCEPT_NETBINDCHANGE = 0x00000010,
            SERVICE_ACCEPT_PARAMCHANGE = 0x00000008,
            SERVICE_ACCEPT_PAUSE_CONTINUE = 0x00000002,
            SERVICE_ACCEPT_PRESHUTDOWN = 0x00000100,
            SERVICE_ACCEPT_SHUTDOWN = 0x00000004,
            SERVICE_ACCEPT_STOP = 0x00000001,

            //supported only by HandlerEx
            SERVICE_ACCEPT_HARDWAREPROFILECHANGE = 0x00000020,
            SERVICE_ACCEPT_POWEREVENT = 0x00000040,
            SERVICE_ACCEPT_SESSIONCHANGE = 0x00000080,
            SERVICE_ACCEPT_TIMECHANGE = 0x00000200,
            SERVICE_ACCEPT_TRIGGEREVENT = 0x00000400,
            SERVICE_ACCEPT_USERMODEREBOOT = 0x00000800
        }

        internal enum ServiceState : int
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ServiceStatus
        {
            internal ServiceType dwServiceType; // service type (file system driver, device driver, win32/user own/share process)
            internal ServiceState dwCurrentState; // current state (running, paused, stopped, pending (state))
            internal uint dwControlsAccepted; // control code accepted and processed in its handler function
            internal uint dwWin32ExitCode; // reports any error codes given when starting or stoping the service
            internal uint dwServiceSpecificExitCode; // reports any service-specific error codes given when starting or stoping 
            internal uint dwCheckPoint; // increments periodically to report progress during operation
            internal uint dwWaitHint; // est time required for start/stop/pause/continue operation
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokenPrivLuid
        {
            public int Count;
            public long Luid;
            public int Attr;
        }

        internal const int TOKEN_QUERY = 0x00000008;
        internal const int TOKEN_ADJUST_PRIVILEGES = 0x00000020;
        internal const int CO_E_FAILEDTOOPENPROCESSTOKEN = unchecked((int)0x8001013C);
        internal const int SE_PRIVILEGE_ENABLED = 0x00000002;
        internal const string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";

        #endregion Properties

        #region DllImports

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, int DesiredAccess, ref IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string SystemName, string Name, ref long Luid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr TokenHandle, bool DisableAllPriveleges, ref TokenPrivLuid NewState, int BufferLength, IntPtr PreviousState, IntPtr ReturnLength);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern UInt32 InitiateShutdown(string lpMachineName, string lpMessage, UInt32 dwGracePeriod, UInt32 dwShutdownFlags, UInt32 dwReason);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern int RegSetKeySecurity(IntPtr handle, uint securityInformation, IntPtr pSecurityDescriptor);

        #endregion

        /// <summary>
        /// Set the Service Status
        /// </summary>
        /// <param name="handle">handle delegate</param>
        /// <param name="state">service state delegate</param>
        /// <param name="wait">wait set to false by default</param>
        /// <param name="waitHint">unsigned integer wait time for hint</param>
        /// <returns></returns>
        public static Tuple<bool, ServiceStatus> SetServiceStatus(IntPtr handle, ServiceState state, bool wait = false, uint waitHint = 100000)
        {
            // begin failing
            bool stat = false;

            // Create new instance of the Service Status
            ServiceStatus serviceStatus = new ServiceStatus();
            
            // Record the current state of the Service
            serviceStatus.dwCurrentState = state;

            // if wait = true
            if (wait)
            {
                // have the service status wait hint be 100 seconds
                serviceStatus.dwWaitHint = waitHint;
            }

            // Based on the given handle, set the service status. Return true if successfully calling SetServiceStatus.
            stat = SetServiceStatus(handle, ref serviceStatus);

            // Return the result. Pass/fail service call & service status
            return new Tuple<bool, ServiceStatus>(stat, serviceStatus);
        }

        /// <summary>
        /// Get the access token associated with the current process
        /// </summary>
        /// <returns>Returns nonzero for successful function call. Zero for unsuccessful.</returns>
        public static IntPtr OpenProcessToken()
        {
            IntPtr hproc = Kernel32.GetProcess();
            IntPtr htok = IntPtr.Zero;

            // hproc = ProcessHandle. A handle to the process whose access token is opened.
            // TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY = DesiredAccess. Specifies an access mask that
            //  specifies the requested types of access to the access token. These requested access
            //  types are compared with the discretionary access control list (DACL) of the token to
            //  determine which accesses are granted or denied.
            // htok = TokenHandle. A pointer to a handle that identifies the nwly opened access
            //  token when the function returns.
            bool stat = OpenProcessToken(hproc, TOKEN_ADJUST_PRIVILEGES | TOKEN_QUERY, ref htok);

            // If successful function call, return nonzero.  If failed, zero.
            return htok;
        }

        /// <summary>
        /// Retrieve the locally unique identifier (LUID) used on a specified system
        /// to locally represent the specified privilege name.
        /// </summary>
        /// <param name="Name">Pointer to a null-terminated string that
        ///     specifies the name of the privilege.</param>
        /// <param name="tokenPrivLuid">Pointer to a variable that receives
        ///     the LUID by which the privilege is known on the system specified
        ///     by the system name parameter.</param>
        /// <returns></returns>
        public static Tuple<bool, long> LookupPrivilegeValue(string Name, long tokenPrivLuid)
        {
            // system name = null. 
            bool stat = LookupPrivilegeValue(null, Name, ref tokenPrivLuid);

            // Return the result. Pass/fail LookupPrivilegeValue and its LUID
            return new Tuple<bool, long>(stat, tokenPrivLuid);
        }

        /// <summary>
        /// Enable or disable privileges in the specified access token.
        /// </summary>
        /// <param name="tokenHandle">Handle to the access token that contains the
        ///     privileges to be modified.</param>
        /// <param name="state">Pointer to a TOKEN_PRIVILEGES structure that specifies
        ///     an array of privileges (enabled, removed, or none) and their attributes. 
        ///     If false, AdjustTokenPrivileges function enables, disables, or removes
        ///     these privileges for the token. If disableAll is true, the function
        ///     ignores this parameter.</param>
        /// <param name="disableAll">disableAllPrivileges. If true, function disables
        ///     all privileges and ignores the NewState parameter. If false, function
        ///     modifies privileges based on the information pointed to by
        ///     the NewState parameter.</param>
        /// <returns></returns>
        public static Tuple<bool, TokenPrivLuid> AdjustTokenPrivileges(IntPtr tokenHandle, TokenPrivLuid state, bool disableAll = false)
        {
            // Declare variables
            int bufferLen = 0;
            IntPtr prevState = IntPtr.Zero;
            IntPtr rtnLen = IntPtr.Zero;

            // Return true/false if successfully processed method
            // tokenHandle, disableAll & state - see param names above
            // bufferLen - specifies buffer size, in bytes, pointed to by prevState. Can be zero if prevState is null.
            // prevState - contains previous state of any privileges that the function modifies. If bufferLen is
            // too small to hold the complete list of modified privileges, the function will fail and not adjust any.
            // rtnLen - if function fails rtnLen will be 0, if it succeeds it will be nonzero.
            bool stat = AdjustTokenPrivileges(tokenHandle, disableAll, ref state, bufferLen, prevState, rtnLen);

            // Return the result. Pass/fail AdjustTokenPrivileges and its state
            return new Tuple<bool, TokenPrivLuid>(stat, state);
        }
    }

}
