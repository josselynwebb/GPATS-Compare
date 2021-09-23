// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading;

namespace NAM
{
    class ATXML
    {

        #region DLLImports

        // ATXML functions
        [DllImport("AtXmlApi.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Ansi, SetLastError=true)]
        static extern unsafe int atxml_Initialize(string proctype, string guid);

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern unsafe int atxml_ValidateRequirements(string TestRequirements, string Allocation, string Available, short BufferSize);

        [DllImport("AtXmlApi.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true)]
        static extern unsafe int atxml_Close();

        [DllImport("AtXmlApi.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Ansi, SetLastError=true)]
        static extern unsafe int atxml_IssueSignal(StringBuilder SignalDescription, StringBuilder Response, short BufferSize);

        // VISA functions

        /// <summary>
        /// Set VISA Attribute of given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="attrName"></param>
        /// <param name="attrValue"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viSetAttribute(string ResourceName, int vi, int attrName, int attrValue);

        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viOut16(string ResourceName, int vi, short accSpace, int offset, short val16);

        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viStatusDesc(string ResourceName, int vi, int status, string desc);

        /// <summary>
        /// Read Waveform Data Points from Oscope
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="Source"></param>
        /// <param name="TransferType"></param>
        /// <param name="WaveFormArray"></param>
        /// <param name="NumberOfPoints"></param>
        /// <param name="AcquisitionCount"></param>
        /// <param name="SampleInterval"></param>
        /// <param name="TimeOffset"></param>
        /// <param name="XREFERENCE"></param>
        /// <param name="VoltIncrement"></param>
        /// <param name="VoltOffset"></param>
        /// <param name="YREFERENCE"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_zt1428_read_waveform(string ResourceName, int vi, int Source, int TransferType, ref double WaveFormArray, ref int NumberOfPoints, ref int AcquisitionCount, ref double SampleInterval, ref double TimeOffset, ref int XREFERENCE, ref double VoltIncrement, ref double VoltOffset, ref int YREFERENCE);

        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_eip_selectCorrectionsTable(string ResourceName, string FilePath, string TableName);

        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_eip_setCorrectedPower(string ResourceName, int instrSession, double Frequency, float Power);

        /// <summary>
        /// Execute device clear for given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viClear(string ResourceName, int vi);

        /// <summary>
        /// Write [string] value to given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <param name="retCount"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viWrite(string ResourceName, int vi, string buffer, int count, ref int retCount);

        /// <summary>
        /// Read [string] return value from given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="vi"></param>
        /// <param name="buffer"></param>
        /// <param name="count"></param>
        /// <param name="retCount"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viRead(string ResourceName, int vi, ref string buffer, int count, ref int retCount);

        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viIn16(string ResourceName, int vi, short space, int offset, ref short val16);

        /// <summary>
        /// Open a VISA sesion to given instrument
        /// </summary>
        /// <param name="ResourceName"></param>
        /// <param name="sesn"></param>
        /// <param name="name"></param>
        /// <param name="timeout"></param>
        /// <param name="vi"></param>
        /// <returns></returns>
        [DllImport("AtXmlDriverFunc.dll")]
        static extern int atxmlDF_viOpen(string ResourceName, int sesn, string name, int timeout, ref int vi);

        #endregion

        #region Constancts

        public const short conNoDLL = 48;
        public const short MAX_XML_SIZE = 4096;

        #endregion

        #region Variables

        private int Vi = -1;
        private bool sessionActive = false;
        private bool initialized = false;

        public bool Initialized
        {
            get
            {
                return initialized;
            }
            set
            {
                initialized = value;
            }
        }

        #endregion


        static ATXML()
        {
            // Constructor
            
        }

        public ATXML()
        {
            // Default Constructor

        }

        public static string binToHL(string binString)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.binToHL(" + binString + ")"); }

            string newString = string.Empty;
            char[] hlString = new char[] { };

            for (int i = 0; i < binString.Length; i++)
            {
                if (binString[i] == '0')
                {
                    //hlString[i] = 'L';
                    newString = String.Concat(newString, "L");
                }
                else if (binString[i] == '1')
                {
                    //hlString[i] = 'H';
                    newString = String.Concat(newString, "H");
                }
            }

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.binToHL(" + binString + ") return (" + newString + ")"); }
            
            return newString;
        }


        public string convertHLToHex(string hlString)
        {
            //s = "HHHHHHHL, HHLHHHLL, HLHHHLHL, HLLHHLLL, LHHHLHHL, LHLHLHLL, LLHHLLHL, LLLHLLLL"
            //  = "FE,DC,BA,98,76,54,32,10"

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.convertHLToHex(" + hlString + ")"); }

            int index = -1;
            int iVal = 0;
            string tempString = "";
            string retString = "";
            
            if (hlString.Length >= 8)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (hlString.StartsWith(","))
                    {
                        // Remove ',' if at index 0 - only occurs after first iteration
                        hlString = hlString.Remove(0, 1).Trim();
                    }

                    tempString = hlString;
                    index = hlString.IndexOf(",", 0);
                    
                    if(index > -1)
                    {
                        tempString = hlString.Remove(index);
                        hlString = hlString.Substring(index);
                    }

                    iVal = 0;

                    for (int j = 0; j < 8; j++)
                    {
                        switch (j)
                        {
                            case 0:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 128;
                                }
                                break;

                            case 1:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 64;
                                }
                                break;

                            case 2:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 32;
                                }
                                break;

                            case 3:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 16;
                                }
                                break;

                            case 4:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 8;
                                }
                                break;

                            case 5:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 4;
                                }
                                break;

                            case 6:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 2;
                                }
                                break;

                            case 7:
                                if (tempString.Substring(j, 1) == "H")
                                {
                                    iVal += 1;
                                }
                                break;

                            default:
                                break;
                        }
                    }

                    if (iVal > 15)
                    {
                        retString = retString + iVal.ToString("x").ToUpper();
                    }
                    else
                    {
                        retString = retString + "0" + iVal.ToString("x").ToUpper();
                    }

                    if (i < 7)
                    {
                        retString = String.Concat(retString, ",");
                    }
                }
            }

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.convertHLToHex(" + hlString + ") return (" + retString + ")"); }

            return retString;
        }
      

        public int Initialize(string ProcType, string GUID)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.Initialize(" + ProcType + ", " + GUID + ")"); }

            int stat = -1;

            try
            {
                stat = atxml_Initialize(ProcType, GUID);
                Initialized = true;
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("ATXML.Initialize() - failed to perform action");
                    Debug.WriteDebugInfo("ATXML.Initialize() - Source: " + Ex.Source + Environment.NewLine + "ATXML.Initialize() - Err: " + Ex.HResult.ToString() + Environment.NewLine + "ATXML.Initialize() - Message: " + Ex.Message);
                }
            }

            return stat;
        }


        public int ValidateRequirements(string Requirements, string Alloc, string Rspns, short BuffSz)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.ValidateRequirements(" + Requirements + ", " + Alloc + ", " + Rspns + ", " + BuffSz.ToString() + ")"); }

            int stat = -1;

            try
            {
                stat = atxml_ValidateRequirements(Requirements, Alloc, Rspns, BuffSz);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("ATXML.ValidateRequirements() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }

            return stat;
        }


        public int Close()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.Close()"); }

            int stat = -1;

            if (Initialized)
            {
                try
                {
                    stat = atxml_Close();
                }
                catch (Exception Ex)
                {
                    if (Program.debugMode && !Program.isHidden)
                    {
                        Debug.WriteDebugInfo("ATXML.Close() - failed to perform action");
                        Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                    }
                }
            }
            else
            {
                stat = 0;
            }

            return stat;
        }


        public int IssueSignal(string SigDescr, ref string Rspns, short BuffSz)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.IssueSignal(" + SigDescr + ", " + Rspns + ", " + BuffSz.ToString() + ")"); }

            int stat = -1;
            StringBuilder sbDescr = new StringBuilder(SigDescr, BuffSz);
            StringBuilder sbRspns = new StringBuilder(Rspns, BuffSz);

            try
            {
                stat = atxml_IssueSignal(sbDescr, sbRspns, BuffSz);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("ATXML.IssueSignal() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }
            
            SigDescr = sbDescr.ToString();
            Rspns = sbRspns.ToString();

            return stat;
        }


        public void OpenSession(string Instr, int Session, string SessionName, int TimeOut, ref int Instance)
        {
            int ret = atxmlDF_viOpen(Instr, Session, SessionName, TimeOut, ref Instance);

            Vi = Instance;

            if (Vi > 0)
            {
                sessionActive = true;
            }

            return;
        }


        public void SetAttribute(string Instr, int viSession, int attr, int attrVal)
        {

        }


        public string Read(string Instr, int Session, string Msg, int charCount, ref int returnCount)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.Read(" + Instr + ", " + Session.ToString() + ", " + Msg + ", " + charCount.ToString() + ", " + returnCount.ToString() + ")"); }

            int stat = -1;

            try
            {
                stat = atxmlDF_viRead(Instr, Session, ref Msg, charCount, ref returnCount);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("ATXML.Read() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }

            return Msg;
        }


        public int Write(string Instr, int Session, string Msg, int charCount, ref int returnCount)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("ATXML.Write(" + Instr + ", " + Session.ToString() + ", " + Msg + ", " + charCount.ToString() + ", " + returnCount.ToString() + ")"); }

            int stat = -1;

            try
            {
                stat = atxmlDF_viWrite(Instr, Session, Msg, charCount, ref returnCount);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("ATXML.Write() - failed to perform action");
                    Debug.WriteDebugInfo("Source: " + Ex.Source + Environment.NewLine + "Message: " + Ex.Message);
                }
            }

            return stat;
        }

    }
}