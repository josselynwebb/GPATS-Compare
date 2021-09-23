// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2021-03-03 14:22#$: Date of last commit
//    $Rev:: 28146            $: Revision of last commit

using System;
using System.Runtime.InteropServices;

namespace NAM
{
    /// <summary>
    /// <c>Kernel32</c> class
    /// Contains dll imports and methods relating to the Windows 32bit Kernal library (kernel32.dll)
    /// </summary>
    static class Kernel32
    {
        #region DLL Imports

        /// <summary>
        /// Gets a pseudo handle for the current process
        /// </summary>
        /// <returns>a psuedo handle to the current process ((HANDLE) - 1)</returns>
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetCurrentProcess();


        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        #endregion

        /// <summary>
        /// Get the currently running process
        /// </summary>
        /// <returns>pointer/handle to the current process</returns>
        public static IntPtr GetProcess()
        {
            return GetCurrentProcess();
        }

        /// <summary>
        /// Get handle to Console Window
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetConsole()
        {
            IntPtr hWnd = default(IntPtr);

            try
            {
                hWnd = GetConsoleWindow();
            }
            catch (Exception)
            {
                throw;
            }

            return hWnd;
        }
    }
}
