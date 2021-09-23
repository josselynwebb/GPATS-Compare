using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NAM
{
    /// <summary>
    /// <c>PowerAPI</c> class
    /// Contains dll imports and methods relating to the Windows Power Profiler library (powerprof.dll)
    /// </summary>
    static class PowerAPI
    {
        internal enum SuspendState : int
        {
            Sleep     = 0,
            Hibernate = 1
        }

        /// <summary>
        /// Suspends the system by shutting power down
        /// </summary>
        /// <param name="hiberate">true - hibernate, false - sleep/standby</param>
        /// <param name="force">true/false on whether this is a critcal action or not</param>
        /// <param name="wakeupEventDisabled">true/false to disable the wake event</param>
        /// <returns>system event to hibernate or sleep</returns>
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern bool SetSuspendState(bool hiberate, bool force, bool wakeupEventDisabled);

        /// <summary>
        /// Bring the computer into hibernate or sleep/standby
        /// </summary>
        /// <param name="suspendState">True - hibernate, False - sleep/standby</param>
        /// <param name="eventsDisabled">True - disable all wake events, False - events remain enabled</param>
        /// <returns>True if successful, False if not</returns>
        public static bool Suspend(bool hibernate, bool eventsDisabled=false)
        {
            return SetSuspendState(hibernate, false, eventsDisabled);
        }
    }
}
