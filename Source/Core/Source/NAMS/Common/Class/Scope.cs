// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using NAM;

namespace NAM
{
    /// <summary>
    /// Oscilloscope Class
    /// </summary>
    static class Scope
    {
        #region Constants

        #endregion

        #region Structures

        public struct Channel
        {
            public const int MIN = 1;
            public const int MAX = 2;
        }

        public struct Voltage
        {
            public const int MIN    = 0;
            public const int MAX    = 7;
            public const int _10V   = 0;
            public const int _5V   = 1;
            public const int _2V   = 2;
            public const int _1V    = 3;
            public const int _500MV    = 4;
            public const int _250MV    = 5;
            public const int _100MV = 6;
            public const int _50MV  = 7;
        }

        public struct Timebase
        {
            public const int MIN    = 0;
            public const int MAX    = 7;
            public const int _20US   = 0;
            public const int _10US = 1;
            public const int _5US = 2;
            public const int _2US  = 3;
            public const int _1US  = 4;
            public const int _500NS = 5;
            public const int _200NS = 6;
            public const int _100NS = 7;
        }

        /// <summary>
        /// Oscilloscope Trigger Configuration
        /// </summary>
        public struct Trigger
        {
            /// <summary>
            /// Oscilloscope Trigger Source Configuration
            /// </summary>
            public struct Source
            {
                public const int MIN = 1;
                public const int MAX = 3;
                public const int CHANNEL_1 = 1;
                public const int CHANNEL_2 = 2;
                public const int EXTERNAL = 3;
            }

            /// <summary>
            /// Oscilloscope Trigger Level Configuration
            /// </summary>
            public struct Level
            {
                public const double MIN = -10.0;
                public const double MAX = 10.0;
            }

            /// <summary>
            /// Oscilloscope Trigger Slope Configuration
            /// </summary>
            public struct Slope
            {
                public const int MIN = 1;
                public const int MAX = 2;
                public const int POS = 1;
                public const int NEG = 2;
            }
        }

        #endregion
    }

}
