// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using NAM;

namespace NAM
{
    public class CANBus
    {

        #region Constants

        private const string RESULT_PATH = "C:\\APS\\DATA";
        private const string RESULT_FILE = "CANBUS_DATA.TXT";
        internal const string FREQ_UNITS = "Hz";
        internal const string TIME_UNITS = "ms";

        internal const int NUMCHANNELS = 2;
        internal const int MINTIMING = 20000;
        internal const int MAXTIMING = 1000000;
        internal const uint MINIDENT = 0;
        internal const uint MAXIDENT = 4294967295;
        internal const int MINIO = 0;
        internal const int MAXIO = 255;
        internal const int MINSAMPLES = 0;
        internal const int MAXSAMPLES = 1;
        internal const int MINFILTER = 0;
        internal const int MAXFILTER = 1;
        internal const uint MINACCCODE = 0;
        internal const uint MAXACCCODE = 4294967295;
        internal const uint MINMASK = 0;
        internal const uint MAXMASK = 4294967295;
        internal const int MINTMO = 0;
        internal const int MAXTMO = 60000;

        #endregion

        private string GUID = "";
        private bool initialized;
        private int channel;
        private int timing;
        private int samples;
        private int filter;
        private uint acceptCode;
        private uint acceptMask;
        private int talker;
        private ulong identifier;
        private string hexID;
        private int ioFlag;
        private string writeData;
        private string readData;
        private int timeOut;
        private bool config;
        private int status;
        private bool asynchronous;
        private ATXML atxml;

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

        public int Channel
        {
            get
            {
                return channel;
            }
            set
            {
                if (value > 0 && value <= NUMCHANNELS)
                {
                    channel = value;
                }
            }
        }

        public int Timing
        {
            get
            {
                return timing;
            }
            set
            {
                if (value >= MINTIMING && value <= MAXTIMING)
                {
                    timing = value;
                }
            }
        }

        public int Samples
        {
            get
            {
                return samples;
            }
            set
            {
                if (value >= MINSAMPLES && value <= MAXSAMPLES)
                {
                    samples = value;
                }
            }
        }

        public int Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (value >= MINFILTER && value <= MAXFILTER)
                {
                    filter = value;
                }
            }
        }

        public uint AcceptCode
        {
            get
            {
                return acceptCode;
            }
            set
            {
                if (value >= MINACCCODE && value <= MAXACCCODE)
                {
                    acceptCode = value;
                }
            }
        }

        public uint AcceptMask
        {
            get
            {
                return acceptMask;
            }
            set
            {
                if (value >= MINMASK && value <= MAXMASK)
                {
                    acceptMask = value;
                }
            }
        }

        public int Talker
        {
            // 0 - Not Listen Only???
            // 1 - Listen Only???

            get
            {
                return talker;
            }
            set
            {
                if (value == 0 || value == 1)
                {
                    talker = value;
                }
            }
        }

        public ulong Identifier
        {
            get
            {
                return identifier;
            }
            set
            {
                if (value >= 0 || value <= 4294967295)
                {
                    identifier = value;
                    hexID = identifier.ToString("x").ToUpper();
                }
            }
        }

        public int IOFlag
        {
            get
            {
                return ioFlag;
            }
            set
            {
                if (value >= 0 && value <= 255)
                {
                    ioFlag = value;
                }

                //if (value == 0 || value == 1)
                //{
                //    ioFlag = value;
                //}
            }
        }

        public string WriteData
        {
            get
            {
                return writeData;
            }
            set
            {
                writeData = value;
            }
        }

        public string ReadData
        {
            get
            {
                return readData;
            }
            set
            {
                readData = value;
            }
        }

        public int TimeOut
        {
            get
            {
                return timeOut;
            }
            set
            {
                if (value >= MINTMO && value <= MAXTMO)
                {
                    timeOut = value;
                }
            }
        }

        public int Status
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public bool Asynchronous
        {
            get
            {
                return asynchronous;
            }
            set
            {
                asynchronous = value;
            }
        }

        private bool Config
        {
            get
            {
                return config;
            }
            set
            {
                config = value;
            }
        }

        private string Allocation = "";

        static CANBus()
        {
            // Static Constructor

        }

        /// <summary>
        /// CANBus Default Constructor
        /// </summary>
        public CANBus()
        {
            // Default Constructor

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Created Instance of CANBus with default constructor"); }
            Initialized = false;
            Channel = 1;
            Timing = 20000;
            Samples = 0;
            Filter = 1;
            AcceptCode = 6682;       // LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL
            AcceptMask = 4294967295; // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
            Talker = 0;              // Not Listen Only
            Identifier = 65244;      // FEDC
            IOFlag = 1;
            WriteData = "FE,DC,BA,98,76,54,32,10";
            ReadData = "";
            TimeOut = 20000;
            Status = 0;
            atxml = new ATXML();

            if (atxml.Initialize(Program.PROCTYPE, Program.GUID) != 0)
            {
                return;
            }
            else
            {
                Initialized = true;
            }

            try
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Checking Registry for PAWS Allocation Path"); }
                Allocation = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\ATS\\", "PAWSAllocationPath", -1);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden) 
                { 
                    Debug.WriteDebugInfo("CANBus() - Failed to get PAWS Allocation Path from registry");
                    Debug.WriteDebugInfo("CANBus() - " + Ex.Message);                
                }
            }
        }

        /// <summary>
        /// CANBus Constructor with channel arg
        /// </summary>
        /// <param name="chan"></param>
        public CANBus(int chan)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Created Instance of CANBus using constructor with channel argument"); }
            Initialized = false;
            Channel = chan;
            Timing = 20000;
            Samples = 0;
            Filter = 1;
            AcceptCode = 6682;       // LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL
            AcceptMask = 4294967295; // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
            Talker = 0;              // Not Listen Only
            Identifier = 65244;      // FEDC
            IOFlag = 1;
            WriteData = "FE,DC,BA,98,76,54,32,10";
            ReadData = "";
            TimeOut = 20000;
            Status = 0;
            atxml = new ATXML();

            if (atxml.Initialize(Program.PROCTYPE, GUID) != 0)
            {
                return;
            }
            else
            {
                Initialized = true;
            }

            try
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Checking Registry for PAWS Allocation Path"); }
                Allocation = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\ATS\\", "PAWSAllocationPath", -1);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("CANBus() - Failed to get PAWS Allocation Path from registry");
                    Debug.WriteDebugInfo("CANBus() - " + Ex.Message);
                }
            }
        }

        /// <summary>
        /// CANBus Constructor with channel & data args
        /// </summary>
        /// <param name="chan"></param>
        /// <param name="data"></param>
        public CANBus(int chan, string data)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Created Instance of CANBus using constructor with channel and data argurments"); }
            Initialized = false;
            Channel = chan;
            Timing = 20000;
            Samples = 0;
            Filter = 1;
            AcceptCode = 6682;       // LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL
            AcceptMask = 4294967295; // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
            Talker = 0;              // Not Listen Only
            Identifier = 65244;      // FEDC
            IOFlag = 1;
            WriteData = data;
            ReadData = "";
            TimeOut = 20000;
            atxml = new ATXML();

            if (atxml.Initialize(Program.PROCTYPE, Program.GUID) != 0)
            {
                return;
            }
            else
            {
                Initialized = true;
            }

            try
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Checking Registry for PAWS Allocation Path"); }
                Allocation = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\ATS\\", "PAWSAllocationPath", -1);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("CANBus() - Failed to get PAWS Allocation Path from registry");
                    Debug.WriteDebugInfo("CANBus() - " + Ex.Message);
                }
            }
        }

        /// <summary>
        /// CANBus Constructor with channel, data, & time-out args
        /// </summary>
        /// <param name="chan"></param>
        /// <param name="data"></param>
        /// <param name="tmo"></param>
        public CANBus(int chan, string data, int tmo)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Created Instance of CANBus using constructor with channel, data, & timeout argurments"); }
            Initialized = false;
            Channel = chan;
            Timing = 20000;
            Samples = 0;
            Filter = 1;
            AcceptCode = 6682;       // LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL
            AcceptMask = 4294967295; // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
            Talker = 0;              // Not Listen Only
            Identifier = 65244;      // FEDC
            IOFlag = 1;
            WriteData = data;
            ReadData = "";
            TimeOut = tmo;
            atxml = new ATXML();

            if (atxml.Initialize(Program.PROCTYPE, Program.GUID) != 0)
            {
                return;
            }
            else
            {
                Initialized = true;
            }

            try
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus() - Checking Registry for PAWS Allocation Path"); }
                Allocation = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\ATS\\", "PAWSAllocationPath", -1);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode && !Program.isHidden)
                {
                    Debug.WriteDebugInfo("CANBus() - Failed to get PAWS Allocation Path from registry");
                    Debug.WriteDebugInfo("CANBus() - " + Ex.Message);
                }
            }
        }

        /// <summary>
        /// CANBus Constructor with channel, timing, samples, & filter args
        /// </summary>
        /// <param name="chan"></param>
        /// <param name="timing"></param>
        /// <param name="samples"></param>
        /// <param name="filter"></param>
        public CANBus(int chan, int timing, int samples, int filter)
        {
            // Constructor with channel arg

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus(" + chan.ToString() + ", " + timing.ToString() + ") Constructor"); }
            Channel = chan;
            Timing = timing;
            atxml = new ATXML();

            int stat = atxml.Initialize(Program.PROCTYPE, Program.GUID);

            try
            {
                Allocation = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\ATS\\", "PAWSAllocationPath", -1);
            }
            catch (Exception Ex)
            {
                Debug.WriteDebugInfo("CANBus() - Failed to get PAWS Allocation Path from registry");
                Debug.WriteDebugInfo("CANBus() - " + Ex.Message);
            }
        }


        ~CANBus()
        {
            // Destructor / Finalizer
            atxml.Close();

        }


        public int Setup()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CanBus.Setup()"); }

            Config = false;

            Status = sendXmlString("Setup", "Ch" + Channel.ToString());
            
            return Status;
        }


        public int Enable()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CanBus.Enable()"); }

            Config = false;

            Status = sendXmlString("Enable", "Ch" + Channel.ToString());

            return Status;
        }


        public int Fetch()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CanBus.Fetch()"); }

            Config = false;

            Status = sendXmlString("Fetch", "Ch" + Channel.ToString());

            return Status;
        }


        public int Validate()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Validate()"); }

            string Response  = new string(' ', 4096);
            string xmlString = string.Empty;

            xmlString = "<AtXmlTestRequirements>";
            xmlString = String.Concat(xmlString, "<ResourceRequirement> " + "  <ResourceType>Source</ResourceType> ");
            xmlString = String.Concat(xmlString, "  <SignalResourceName>CAN_1</SignalResourceName> " + "</ResourceRequirement> ");
            xmlString = String.Concat(xmlString, "</AtXmlTestRequirements>");
                        
            Status = atxml.ValidateRequirements(xmlString, Allocation, Response, 4096);

            if (Status != 0)
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Validate() - CAN bus is not responding"); }
            }

            return Status;
        }


        public int LoopBack()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.LoopBack( " + Channel.ToString() + " )"); }

            Config = false;

            switch (Channel)
            {
                case 1:
                    Status = sendXmlString("Reset", "Ch2");
                    Status = getXmlString();
                    Status = sendXmlString("Setup", "Ch1");
                    Status = getXmlString();
                    Status = sendXmlString("Setup", "Ch2");
                    Status = getXmlString();
                    //Talker = 1;
                    Status = sendXmlString("Enable", "Ch2");  // Set ch2 to receive
                    //Talker = 0;
                    Status = sendXmlString("Enable", "Ch1");  // Set ch1 to transmit
                    Status = sendXmlString("Fetch", "Ch2");   // Read data
                    ReadData = parseXML(ReadData, "data");
                    ReadData = atxml.convertHLToHex(ReadData);
                    Status = sendXmlString("Reset", "Ch2");
                    break;

                case 2:
                    Status = sendXmlString("Reset", "Ch1");
                    Status = getXmlString();
                    Status = sendXmlString("Setup", "Ch2");
                    Status = getXmlString();
                    Status = sendXmlString("Setup", "Ch1");
                    Status = getXmlString();
                    //Talker = 1;
                    Status = sendXmlString("Enable", "Ch1");  // Set ch1 to receive
                    //Talker = 0;
                    Status = sendXmlString("Enable", "Ch2");  // Set ch2 to transmit
                    Status = sendXmlString("Fetch", "Ch1");   // Read data
                    ReadData = parseXML(ReadData, "data");
                    if (!ReadData.Contains("error"))
                    {
                        ReadData = atxml.convertHLToHex(ReadData);
                    }
                    
                    Status = sendXmlString("Reset", "Ch1");
                    break;

                default:
                    break;
            }

            if (Program.debugMode && !Program.isHidden) 
            {
                Debug.WriteDebugInfo("CANBus.LoopBack() - WriteData: " + WriteData); 
                Debug.WriteDebugInfo("CANBus.LoopBack() -  ReadData: " + ReadData); 
            }

            if (ReadData == WriteData)
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.LoopBack() - Loopback test passed"); }
                return 0;
            }
            else
            {
                if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.LoopBack() - Loopback test failed"); }
                return -1;
            }
        }


        public int Reset()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Reset()"); }

            Config = false;

            Status = sendXmlString("Reset", "Ch" + Channel.ToString());

            return Status;
        }


        public int Transmit()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Transmit()"); }

            Config = false;

            Status = Setup();
            //Talker = 0;
            Status = Enable();

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Transmit() -  WriteData: " + WriteData); }

            return Status;
        }


        public void Receive(object Operation)
        {
            bool Async = (bool)Operation;

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Receive()"); }
            
            Config = false;

            Status = Setup(); 
            //Talker = 0;
            Status = Enable();
            //Talker = 0;
            Status = Fetch();
            ReadData = parseXML(ReadData, "data");
            if (!ReadData.Contains("error"))
            {
                ReadData = atxml.convertHLToHex(ReadData);
            }

            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.Receive() -  ReadData: " + ReadData); }

            if (Async)
            {
                CreateResultsFile();
                WriteResults(ReadData);
            }
            return;
        }


        public void AsyncReceive()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.AsyncReceive()"); }

            Thread.Sleep(500);
            Thread thrd = new Thread(Receive);

            thrd.IsBackground = true;
            thrd.SetApartmentState(ApartmentState.MTA);
            thrd.Start(true);

            return;
        }


        private int sendXmlString(string Command, string Channel)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.sendXmlString(" + Command + ", " + Channel + ")"); }
            // ErrStatus = sendXmlString("Setup", "Ch1")
            // ErrStatus = sendXmlString("Enable", "Ch1")
            // ErrStatus = sendXmlString("Fetch", "Ch1")
            // ErrStatus = sendXmlString("Reset", "Ch1")

            const string ACTION = "exchange";

            int    status    = -1;
            string Response  = new string(' ', 4096);
            string xmlString = string.Empty;

            switch (Command.ToLower())
            {
                case "setup":
                    Command = "Setup";
                    break;
                case "enable":
                    Command = "Enable";
                    break;
                case "fetch":
                    Command = "Fetch";
                    break;
                case "reset":
                    Command = "Reset";
                    break;
                default:
                    // Illegal Command
                    return -1;
            }

            switch (Channel.ToLower())
            {
                case "ch1":
                    Channel = "1";
                    break;
                case "1":
                    Channel = "1";
                    break;
                case "ch2":
                    Channel = "2";
                    break;
                case "2":
                    Channel = "2";
                    break;

                default:
                    //Illegal Channel
                    return -1;
            }

            Response = new string(' ', 4096);
            xmlString = "<AtXmlSignalDescription xmlns:atXml=\"ATXML_TSF\">" + Environment.NewLine;
            xmlString = String.Concat(xmlString, "<SignalAction>" + Command + "</SignalAction>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<SignalResourceName>CAN_1</SignalResourceName>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<SignalSnippit>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<Signal Name=\"BUS_SIGNAL\" Out=\"exchange\">" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<CAN name=\"" + ACTION + "\"");
            xmlString = String.Concat(xmlString, " timingValue=\"" + Timing.ToString() + FREQ_UNITS + "\"");
            xmlString = String.Concat(xmlString, " threeSamples=\"" + Samples.ToString() + "\"");
            xmlString = String.Concat(xmlString, " singleFilter=\"" + Filter.ToString() + "\"");
            xmlString = String.Concat(xmlString, " acceptanceCode=\"" + ATXML.binToHL(Convert.ToString(AcceptCode, 2).PadLeft(32, '0').Trim() )); // LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL
            xmlString = String.Concat(xmlString, "\" acceptanceMask=\"" + ATXML.binToHL(Convert.ToString(AcceptMask, 2).PadLeft(32, '0').Trim() )); // HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH
            xmlString = String.Concat(xmlString, "\" listenOnly=\"" + Talker.ToString() + "\"");
            xmlString = String.Concat(xmlString, " channel=\"" + Channel + "\"");
            xmlString = String.Concat(xmlString, " data_bits=\"" + hexID + "," + IOFlag.ToString() + "," + WriteData + "\"");           // FEDC,1,FE,DC,BA,98,76,54,32,10
            xmlString = String.Concat(xmlString, " maxTime=\"" + TimeOut.ToString() + CANBus.TIME_UNITS + "\"");

            if (Command == "Fetch")
            {
                if (Config == false)
                {
                    xmlString = String.Concat(xmlString, " attribute=\"data\"");
                    // example data response
                    // < AtXmlResponse >
                    // < ReturnData >
                    // < ValuePair >
                    // < Attribute > data </ Attribute >
                    // < Value >
                    // < c:Datum xsi:type = "c:string" unit = "" >< c:Value > HHHHHHHL, HHLHHHLL, HLHHHLHL, HLLHHLLL, LHHHLHHL, LHLHLHLL, LLHHLLHL, LLLHLLLL </ c:Value ></ c:Datum >
                    // </ Value >
                    // </ ValuePair >
                    // </ ReturnData >
                    // </ AtXmlResponse >
                }
                else
                {
                    xmlString = String.Concat(xmlString, " attribute=\"config\"");
                    // see fnParseConfig for example
                }
            }

            xmlString = String.Concat(xmlString, " />" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "</Signal>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "</SignalSnippit>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "</AtXmlSignalDescription>");

            status = atxml.IssueSignal(xmlString, ref Response, ATXML.MAX_XML_SIZE);

            if (Command == "Fetch")
            {
                readData = Response;
                
            }

            return status;
        }


        private int getXmlString()
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.getXmlString()"); }
            // ErrStatus = getXmlString("Ch1")' "Ch1", "Ch2"

            int stat = -1;
            string Response = new string(' ', 4096);
            string xmlString = string.Empty;

            xmlString = "<AtXmlSignalDescription xmlns:atXml=\"ATXML_TSF\">" + Environment.NewLine;
            xmlString = String.Concat(xmlString, "<SignalAction>Status</SignalAction>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<SignalResourceName>CAN_1</SignalResourceName>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<SignalSnippit>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<Signal Name=\"\" Out=\"Meas\" In=\"Cha Ext Chb\">" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "<Instantaneous name=\"Meas\" In=\"Cha\" attribute=\"ac_ampl\"/>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "</Signal>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "</SignalSnippit>" + Environment.NewLine);
            xmlString = String.Concat(xmlString, "</AtXmlSignalDescription>");

            stat = atxml.IssueSignal(xmlString, ref Response, ATXML.MAX_XML_SIZE);

            return stat;
        }


        private string parseXMLErr(string xmlString)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.parseXMLErr(" + xmlString + ")"); }
            int index1 = -1;
            int index2 = -1;
            string searchString = "errText=\"";
            string delim = "\"";
            string retString = "";

            index1 = xmlString.IndexOf(searchString, 0);
            if (index1 == -1)
            {
                // No errors found
                return retString;
            }

            index1 = xmlString.IndexOf(delim, index1 + 1);
            index2 = xmlString.IndexOf(delim, index1 + 1);
            retString = xmlString.Substring(index1 + delim.Length, (index2 - index1 - delim.Length));
            retString = retString.Trim();
            return retString;
        }


        private string parseXML(string xmlString, string pData)
        {
            if (Program.debugMode && !Program.isHidden) { Debug.WriteDebugInfo("CANBus.parseXML(" + xmlString + ", " + pData + ")"); }

            int index1 = -1;
            int index2 = -1;
            int index3 = -1;
            string searchString = string.Empty;
            string retString = "";

            //s = "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>timingValue</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""20000""/>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>threeSamples</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""0""/>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>m_Channel</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""1""/>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>singleFilter</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:integer"" unit="""" value=""1""/>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>acceptanceCode</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:string"" unit=""""><c:Value>LLLLLLLLLLLLLLLLLLLHHLHLLLLHHLHL</c:Value></c:Datum>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>acceptanceMask</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:string"" unit=""""><c:Value>HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH</c:Value></c:Datum>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>maxTime</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:double"" unit="""" value=""2.000000000000E+001""/>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            //s = s & "<AtXmlResponse>"
            //s = s & "  	<ReturnData>"
            //s = s & "		<ValuePair>"
            //s = s & "			<Attribute>spec</Attribute>"
            //s = s & "			<Value>"
            //s = s & "				<c:Datum xsi:type=""c:string"" unit=""""><c:Value>CAN</c:Value></c:Datum>"
            //s = s & "			</Value>"
            //s = s & "		</ValuePair>"
            //s = s & "	</ReturnData>"
            //s = s & "</AtXmlResponse>"

            searchString = "<Attribute>" + pData + "</Attribute>";
            index1 = xmlString.IndexOf(searchString , 0);
            if (index1 == -1)
            {
                // Attribute not found check for errors
                return parseXMLErr(xmlString);
            }

            searchString = "</AtXmlResponse>";
            index2 = xmlString.IndexOf(searchString, index1);
            xmlString = xmlString.Substring(index1, (index2 - index1));

            index1 = 0;
            searchString = "type=\"c:string";
            if (xmlString.IndexOf(searchString, index1) > 0)
            {
                // Found String Type Response
                searchString = "c:Value>";
                index2 = xmlString.IndexOf(searchString, index1);
                if (index2 == -1)
                {
                    // Value not found
                    return retString;
                }

                // Move index to end of Value tag
                index2 = index2 + searchString.Length;

                searchString = "</";
                index3 = xmlString.IndexOf(searchString, index2);
                if (index3 == -1)
                {
                    return retString;
                }

                retString = xmlString.Substring(index2, (index3 - index2));
            }
            else
            {
                // Found Numeric Type Response
                searchString = "value=";
                index2 = xmlString.IndexOf(searchString, index1);
                if (index2 == -1)
                {
                    return retString;
                }

                index2 = index2 + 7;

                searchString = "/>";
                index3 = xmlString.IndexOf(searchString, index2);
                if (index3 == -1)
                {
                    return retString;
                }

                retString = xmlString.Substring(index2, (index3 - index2 - 1));
            }

            return retString; 
        }

        /// <summary>
        /// Create Output file for RX results
        /// </summary>
        /// <returns></returns>
        private int CreateResultsFile()
        {
            int stat = 0;

            try
            {
                System.IO.FileStream resFile = System.IO.File.Create(System.IO.Path.Combine(RESULT_PATH, RESULT_FILE));
                resFile.Close();
                resFile.Dispose();
            }
            catch (Exception Ex)
            {
                Console.Write(Ex.Message);
                stat = -1;
            }

            return stat;
        }

        /// <summary>
        /// Write result strings to output file for asynchronous receive operations
        /// </summary>
        /// <param name="debugString"></param>
        public static void WriteResults(string resultString)
        {
            // Send text string to results output file
            using (System.IO.StreamWriter resFile = System.IO.File.AppendText(System.IO.Path.Combine(RESULT_PATH, RESULT_FILE)))
            {
                try
                {
                    resFile.Write(resultString + Environment.NewLine);
                }
                catch (System.IO.IOException)
                {
                    throw;
                }
                catch (Exception Ex)
                {
                    Console.Write(Ex.Message);
                    throw;
                }

                resFile.Close();
            }
        }

    }
}