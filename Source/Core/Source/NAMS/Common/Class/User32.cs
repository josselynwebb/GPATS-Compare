// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2021-03-03 14:22#$: Date of last commit
//    $Rev:: 28146            $: Revision of last commit

using System;
using System.Runtime.InteropServices;

namespace NAM
{
    /// <summary>
    /// User32 Class
    /// </summary>
    static class User32
    {
        #region DLL Imports

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Logs off the interactive user, shuts down the system, or restarts the system
        /// </summary>
        /// <param name="uFlags">Type of shutdown to execute</param>
        /// <param name="dwReason">Shutdown reason</param>
        /// <returns>True if successful, False if not</returns>
        [DllImport("user32.dll")]
        private static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        /// <summary>
        /// Locks the work station
        /// </summary>
        [DllImport("user32.dll")]
        private static extern void LockWorkStation();

        #endregion

        [Flags]
        internal enum PowerSetting : int
        {
            LOGOFF = 0x00000000,
            SHUTDOWN = 0x00000001,
            REBOOT = 0x00000002,
            FORCE = 0x00000004,
            POWEROFF = 0x00000008,
            FORCEIFHUNG = 0x00000010
        }

        public struct Console
        {
            public struct Visibility
            {
                const int SW_HIDE = 0;
                const int SW_SHOW = 5;

                public static int Show
                {
                    get
                    {
                        return SW_SHOW;
                    }
                }
                public static int Hide
                {
                    get
                    {
                        return SW_HIDE;
                    }
                }
            }
        }

        /// <summary>
        /// Show / Hide Console with given Window Handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        public static bool ShowConsole(IntPtr hWnd, int Action)
        {

            bool succ = false;

            try
            {
                succ = ShowWindow(hWnd, Action);
            }
            catch (Exception)
            {
                throw;
            }

            return succ;
        }

        /// <summary>
        /// Exits Windows
        /// <para>Log off = ExitWindowsEx(0, 0)</para>
        /// <para>Shutdown = ExitWindowsEx(1, 0)</para>
        /// <para>Reboot = ExitWindowsEx(2, 0)</para>
        /// </summary>
        /// <param name="Flags">Type of shutdown to execute</param>
        /// <param name="Reason">Shutdown reason</param>
        /// <returns>True if successful, False if not</returns>
        public static bool ExitWindows(uint Flags, uint Reason = 0)
        {
            return ExitWindowsEx(Flags, Reason);
        }

    }
}
