// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2021-03-03 14:22#$: Date of last commit
//    $Rev:: 28146            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace NAM
{
    class ATLAS
    {
        #region Constants

        public const int ATLAS_BOOL   = 0;
        public const int ATLAS_INT    = 1;
        public const int ATLAS_REAL   = 2;
        public const int ATLAS_STRING = 4;

        const string ATLAS_TEMP_FOLDER = "\\AppData\\Local\\Temp\\";
        const string XP_ATLAS_TEMP_FOLDER = "\\LOCALS~1\\Temp\\";

        const string NAMDLL_PATH = "C:\\Windows\\nam.dll";
        #endregion

        #region Variables


        #endregion

        #region DLLImports

        // These imports pertain to nam.dll wil SHA256 HASH 0C60CFA1EC54BFFB32E59A41A516867F9E1505888F058F695542D502000375ED

        /// <summary>
        /// Close PAWS virtual memmory file
        /// </summary>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#1")]
        private extern static int VmClose();

        //vmCloseEx #2

        /// <summary>
        /// Get boolean value passed to NAM from PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#3")]
        private static extern bool VmGetBool(Int32 vad);

        /// <summary>
        /// Get size of passed value from PAWS in bytes
        /// </summary>
        /// <param name="vad"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#4")]
        private extern static int VmGetDataSize(Int32 vad);

        /// <summary>
        /// Get type of value passed from PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#5")]
        private static extern int VmGetDataType(Int32 vad);

        /// <summary>
        /// Get decimal value passed from PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#6")]
        private static extern double VmGetDecimal(Int32 vad);

        /// <summary>
        /// Get digital value passed from PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <param name="prDig"></param>
        /// <param name="nMax"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#7")]
        private static extern int VmGetDig(Int32 vad, UInt16 prDig, int nMax);

        /// <summary>
        /// Get integer value passed from PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#8")]
        private static extern Int32 VmGetInteger(Int32 vad);

        /// <summary>
        /// Get string value passed from PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <param name="prTxt"></param>
        /// <param name="nMax"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#9")]
        private extern static int VmGetText(Int32 vad, StringBuilder prTxt, short nMax);

        /// <summary>
        /// Open PAWS virtual memmory file
        /// </summary>
        /// <param name="fname"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#10")]
        private static extern int VmOpen(string fname);

        /// <summary>
        /// Set boolean value returned to PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <param name="nMax"></param>
        /// <returns></returns>
        [DllImport(NAMDLL_PATH, EntryPoint = "#11")]
        private static extern void VmSetBool(Int32 vad, bool bval);

        /// <summary>
        /// Set decimal value returned to PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <param name="dval"></param>
        [DllImport(NAMDLL_PATH, EntryPoint = "#12")]
        private static extern void VmSetDecimal(Int32 vad, double dval);

        [DllImport(NAMDLL_PATH, EntryPoint = "#13")]
        private static extern void vmSetDig(Int32 vad, string pDig, int nWords);

        /// <summary>
        /// Set integer value returned to PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <param name="ival"></param>
        [DllImport(NAMDLL_PATH, EntryPoint = "#14")]
        private static extern void VmSetInteger(Int32 vad, Int32 ival);

        /// <summary>
        /// Set text string returned to PAWS
        /// </summary>
        /// <param name="vad"></param>
        /// <param name="pTxt"></param>
        [DllImport(NAMDLL_PATH, EntryPoint = "#15")]
        private static extern void VmSetText(Int32 vad, string pTxt);

        #endregion

        /// <summary>
        /// Checks to see if nam.dll exists on system
        /// </summary>
        /// <returns></returns>
        public bool dllExists()
        {
            if (Program.debugMode) { Debug.WriteDebugInfo("dllExists() - Checking for dll: " + NAMDLL_PATH); }
            return System.IO.File.Exists(NAMDLL_PATH);
        }

        /// <summary>
        /// Determines whether or not this instance was executed from an ATLAS program
        /// </summary>
        /// <param name="strArg"></param>
        /// <returns>bATLAS</returns>
        public bool IsATLAS(string strArg)
        {
            bool bATLAS = false;

            if (strArg != null)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.IsATLAS() - Checking arg[0] - " + strArg); }
                if (strArg.Contains(ATLAS_TEMP_FOLDER) || strArg.Contains(XP_ATLAS_TEMP_FOLDER))
                {
                    bATLAS = true;
                }
            }

            return bATLAS;
        }

        /// <summary>
        /// Gets address for ATLAS argument (converts string value to integer value)
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public int GetATLASArgAddr(string arg)
        {
            int addr = -1;

            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetATLASArgAddr() - getting address for value " + arg); }

            try
            {
                addr = Convert.ToInt32(arg);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetATLASArgAddr() - Convert to Int32 Exception: \n" + Ex.Message); }
                Console.WriteLine("ATLAS.GetATLASArgAddr() - Convert to Int32 Exception: \n" + Ex.Message);
            }

            return addr;
        }

        /// <summary>
        /// Gets ATLAS argument values
        /// </summary>
        /// <param name="Aarg"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public string GetArgsATLAS(string[] Aarg, int i)
        {
            int itmp = Convert.ToInt32(Aarg[i]);
            long lType = VmGetDataType(Convert.ToInt32(Aarg[i]));
            long lSize = VmGetDataSize(Convert.ToInt32(Aarg[i]));
            string stmp = "";
            short sRet = -1;
            int iRet = -1;
            double dRet = -1d;
            float fRet = -1f;
            bool bRet = false;
            StringBuilder prTxt = new StringBuilder(1024, 2048);

            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - " + Aarg[i].ToString() + " was passed in"); }
            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - vad = " + Convert.ToInt32(Aarg[i]).ToString()); }
            switch (lType)
            {
                case ATLAS_BOOL:
                    // boolean
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is boolean"); }
                    bRet = VmGetBool(Convert.ToInt32(Aarg[i]));

                    if (bRet == true)
                    {
                        stmp = "true";
                    }
                    else
                    {
                        stmp = "false";
                    }
                    break;

                case ATLAS_INT:
                    // integer
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is integer"); }
                    itmp = VmGetInteger(Convert.ToInt32(Aarg[i]));
                    stmp = itmp.ToString();
                    break;

                case ATLAS_REAL:
                    // real
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is double"); }
                    try
                    {
                        dRet = (double)(VmGetDecimal(Convert.ToInt32(Aarg[i])));
                    }
                    catch (Exception Ex)
                    {
                        if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Exception Occurred getting (double) value"); }
                        if (Program.debugMode) { Debug.WriteDebugInfo(Ex.Message); }
                    }

                    stmp = dRet.ToString();
                    break;

                case 3:
                    // pointer
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is pointer"); }
                    break;

                case ATLAS_STRING:
                    // Text String
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is string"); }
                    iRet = VmGetText(Convert.ToInt32(Aarg[i]), prTxt, 1024);
                    stmp = prTxt.ToString();
                    break;

                case 5:
                    // digital pin set
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is digital pin set"); }
                    break;

                case 6:
                    // Conn
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is Conn"); }
                    break;

                case 7:
                    // digital
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is digital"); }
                    break;

                case 8:
                    // file control block
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is file control block"); }
                    break;

                default:
                    // undefined
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Type is undefined"); }
                    break;
            }
            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.GetArgsATLAS() - Value is " + stmp); }
            return stmp;
        }

        /// <summary>
        /// Sets ATLAS return values
        /// </summary>
        /// <param name="iAddr"></param>
        /// <param name="iType"></param>
        /// <param name="returnVal"></param>
        public void SetArgsATLAS(int iAddr, int iType, int returnInt = -1, bool returnBool = true, string returnStr = "", double returnDbl = 0)
        {

            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - vad = " + iAddr); }

            if (iType == ATLAS_BOOL)
            {
                // boolean
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - Type is boolean"); }
                try
                {
                    VmSetBool(iAddr, (bool)returnBool);
                }
                catch (Exception Ex)
                {
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - VmSetBool Exception: \n" + Ex.Message); }
                    Console.WriteLine("ATLAS.SetArgsATLAS() - VmSetBool Exception: \n" + Ex.Message);
                }
            }
            else if (iType == ATLAS_INT)
            {
                // integer
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - Type is integer, Value is " + returnInt.ToString()); }
                try
                {
                    VmSetInteger(iAddr, returnInt);
                }
                catch (Exception Ex)
                {
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - VmSetInteger Exception: \n" + Ex.Message); }
                    Console.WriteLine("ATLAS.SetArgsATLAS() - VmSetInteger Exception: \n" + Ex.Message);
                }
            }
            else if (iType == ATLAS_STRING)
            {
                // Text String
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - Type is string, Text is " + returnStr); }
                try
                {
                    VmSetText(iAddr, (string)returnStr);
                }
                catch (Exception Ex)
                {
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - VmSetText Exception: \n" + Ex.Message); }
                    Console.WriteLine("ATLAS.SetArgsATLAS() - VmSetText Exception: \n" + Ex.Message);
                }
            }
            else if (iType == ATLAS_REAL)
            {
                // float
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - Type is double, Value is " + returnDbl.ToString());  }
                try
                {
                    VmSetDecimal(iAddr, returnDbl);
                }
                catch (Exception Ex)
                {
                    if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.SetArgsATLAS() - VmSetDecimal Exception: \n" + Ex.Message); }
                    Console.WriteLine("ATLAS.SetArgsATLAS.VmSetDecimal Exception: \n" + Ex.Message);
                }
            }
            else
            {
                // Add more types
            }
        }

        /// <summary>
        /// Open ATLAS virtual memory file for access to argument values
        /// </summary>
        /// <param name="vmFile"></param>
        /// <returns>iStat</returns>
        public int OpenATLASvm(string vmFile)
        {
            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.OpenATLASvm() - Opening virtual memory file"); }

            int iStat = -1;

            try
            {
                iStat = VmOpen(vmFile);
            }
            catch (Exception Ex)
            {
                // Failed to open virtual memmory
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.OpenATLASvm() - Failed to open virtual memory file"); }
                Console.WriteLine("ATLAS.OpenATLASvm() - Exception: \n" + Ex.Message);
            }

            return iStat;
        }

        /// <summary>
        /// Close ATLAS virtual memory file
        /// </summary>
        /// <param name="vmFile"></param>
        /// <returns>iStat</returns>
        public int CloseATLASvm()
        {
            if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.CloseATLASvm() - Closing virtual memory file"); }

            int iStat = -1;

            try
            {
                iStat = VmClose();
            }
            catch (Exception Ex)
            {
                // Failed to close virtual memmory
                if (Program.debugMode) { Debug.WriteDebugInfo("ATLAS.CloseATLASvm() - Failed to close virtual memory file"); }
                Console.WriteLine("ATLAS.CloseATLASvm() - Exception: \n" + Ex.Message);
            }

            return iStat;
        }

    }
}
