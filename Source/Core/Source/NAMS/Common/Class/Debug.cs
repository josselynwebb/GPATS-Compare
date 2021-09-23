// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2021-03-03 14:22#$: Date of last commit
//    $Rev:: 28146            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace NAM
{
    static class Debug
    {
        #region Constants


        #endregion

        /// <summary>
        /// Check for existence of debug output file
        /// </summary>
        /// <returns>boolean</returns>
        public static bool IsDebug()
        {
            if (System.IO.File.Exists(Program.DEBUG_FILE))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Create Output file for debug statements
        /// </summary>
        /// <returns></returns>
        public static int CreateDebugFile()
        {
            int stat = 0;

            try
            {
                System.IO.FileStream dbgFile = System.IO.File.Create(Program.DEBUG_RECORD);
                dbgFile.Close();
                dbgFile.Dispose();
            }
            catch (Exception Ex)
            {
                Console.Write(Ex.Message);
                stat = -1;
            }

            return stat;
        }

        /// <summary>
        /// Write debug strings to output file for troubleshooting purposes
        /// </summary>
        /// <param name="debugString"></param>
        public static void WriteDebugInfo(string debugString)
        {
            // Send text string to debug output file
            using (System.IO.StreamWriter dbgFile = System.IO.File.AppendText(Program.DEBUG_RECORD))
            {
                try
                {
                    dbgFile.Write(debugString + Environment.NewLine);
                }
                catch (System.IO.IOException)
                {
                    throw;
                }
                /*catch (Exception Ex)
                {
                    Console.Write(Ex.Message);
                    throw;
                }*/

                dbgFile.Close();
            }
        }

    }
}
