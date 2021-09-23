using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace NAM
{
    static class Program
    {
        public static readonly bool debugMode = false;

        // Program Unique Debug Constants
        public const string DEBUG_FILE = "C:\\APS\\DATA\\DEBUGIT_GPATSUTILS";
        public const string DEBUG_RECORD = "C:\\APS\\DATA\\GPATSUTILS_DEBUG.txt";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Accept an array of strings</param>
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new GPATSUtils(args)
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
