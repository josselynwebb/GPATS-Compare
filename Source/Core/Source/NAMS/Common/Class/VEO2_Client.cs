// SVN Information
// $Author:: wileyj           $: Author of last commit
//   $Date:: 2020-07-06 16:01#$: Date of last commit
//    $Rev:: 27851            $: Revision of last commit

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NAM
{
    public class Listener
    {

        public Socket ClientSocket = null;
        public const int BufferSize = 256;
        public byte[] Rcv_buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();

    }

    public class AsynchronousClient
    {
        //Remote Ports
        private const int CommandPort = 7788;
        private const int SilentPort = 7789;

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);

        //Remote Device response string variable
        private static string strResponse = string.Empty;

        private static void ConnectCallback(IAsyncResult AR)
        {
            try
            {
                //get socket from the Listener
                Socket client = (Socket)AR.AsyncState;

                client.EndConnect(AR);

                if (Program.debugMode) { Debug.WriteDebugInfo("Socket Connected to " + client.RemoteEndPoint.ToString()); }
                connectDone.Set();
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo(Ex.Message); }
            }
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            try
            {
                Listener listener = (Listener)AR.AsyncState;
                Socket client = listener.ClientSocket;

                int bytesRead = client.EndReceive(AR);

                if (Program.debugMode) { Debug.WriteDebugInfo("Bytes Read:  " + bytesRead.ToString()); }

                if (bytesRead > 0)
                {
                    listener.sb.Append(Encoding.ASCII.GetString(listener.Rcv_buffer, 0, bytesRead));
                    if (Program.debugMode) { Debug.WriteDebugInfo(Encoding.ASCII.GetString(listener.Rcv_buffer, 0, bytesRead)); }
                    client.BeginReceive(listener.Rcv_buffer, 0, Listener.BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), listener);
                    //only one line of strings to be returned
                    strResponse = listener.sb.ToString();
                    receiveDone.Set();
                }
                else
                {
                    if (listener.sb.Length > 1)
                    {
                        strResponse = listener.sb.ToString();
                        if (Program.debugMode) { Debug.WriteDebugInfo("StringBuilder String:  " + strResponse); }
                    }
                    receiveDone.Set();
                    if (Program.debugMode) { Debug.WriteDebugInfo("Receive Done"); }
                }
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo(Ex.Message); }
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                Listener listener = new Listener();
                listener.ClientSocket = client;

                client.BeginReceive(listener.Rcv_buffer, 0, Listener.BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), listener);
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo(Ex.Message); }
            }
        }

        private static void SendCallback(IAsyncResult AR)
        {
            try
            {
                Socket client = (Socket)AR.AsyncState;

                int bytesSent = client.EndSend(AR);
                if (Program.debugMode) { Debug.WriteDebugInfo("Sent " + bytesSent + " bytes to server"); }

                sendDone.Set();
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo(Ex.Message); }
            }
        }

        private static void Send(Socket client, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, SocketFlags.None, new AsyncCallback(SendCallback), client);
        }

        private static void ClientStart(String Device, String CMD)
        {
            try
            {
                //IPHostEntry ipHost = Dns.GetHostEntry(IPAddress.Parse("192.168.200.160"));
                //IPAddress ipAddress = ipHost.AddressList[0];
                //IPAddress ipAddress = IPAddress.Parse("192.168.200.160");
                IPAddress ipAddress=null;

                switch (Device)
                {
                    case "LOCAL":
                        ipAddress = IPAddress.Loopback;  //Parse("127.0.0.1");
                        break;
                    case "VEO2":
                        ipAddress = IPAddress.Parse("192.168.200.160");
                        break;
                    case "BlackBody":
                        ipAddress = IPAddress.Parse("192.168.200.161");
                        break;
                    default:
                        ipAddress = IPAddress.Parse(Device);
                        break;
                }

                if (Program.debugMode) { Debug.WriteDebugInfo("Client Started with IPAddress:  " + ipAddress.ToString()); }

                IPEndPoint ACC_Server = new IPEndPoint(ipAddress, SilentPort);  //CommandPort

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                client.BeginConnect(ACC_Server, new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();
                if (Program.debugMode) { Debug.WriteDebugInfo("VEO2_Client connected"); }
                
                //Testing this out
                if (ACC_Server.Port == CommandPort)
                {
                    Send(client, "\n");
                    sendDone.WaitOne();
                    Debug.WriteDebugInfo("Clearing out welcome string");
                    Receive(client);
                    receiveDone.WaitOne();
                }
                //Testing end

                Send(client, CMD);
                sendDone.WaitOne();

                if (Program.debugMode) { Debug.WriteDebugInfo("Sent Command:  " + CMD); }

                Receive(client);

                Debug.WriteDebugInfo("Receive(client) Processed");
                receiveDone.WaitOne();

                if (Program.debugMode) { Debug.WriteDebugInfo("Response received: " + strResponse); }

                Send(client, "quit \n");  //Added this in to close the connection from the server side
                sendDone.WaitOne();       //and this line
                client.Shutdown(SocketShutdown.Both);
                if (Program.debugMode) { Debug.WriteDebugInfo("Shutdown Connection"); }
                client.Close();
            }
            catch (Exception Ex)
            {
                if (Program.debugMode) { Debug.WriteDebugInfo(Ex.Message); }
            }
        }

        public static string Main(String Device, String args)
        {
            ClientStart(Device, args);
            return strResponse;
        }
    }

    public class SynchronousClient
    {
        //Remote Ports
        private const int CommandPort = 7788;
        private const int SilentPort = 7789;

        //Remote Device response string variable
        private static string strResponse = string.Empty;

        public static int StartClient(String Device, String CMD)
        {
            //Incoming Data buffer
            byte[] bytes = new byte[1024];

            try
            {

                IPAddress ipAddress = null;

                switch (Device)
                {
                    case "VEO2":
                        ipAddress = IPAddress.Parse("192.168.200.160");
                        break;
                    case "Blackbody":
                        ipAddress = IPAddress.Parse("192.168.200.161");
                        break;
                }

                IPEndPoint ACC_Server = new IPEndPoint(ipAddress, SilentPort);  // or CommandPort

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //connect to server
                    client.Connect(ACC_Server);
                    if (Program.debugMode) { Debug.WriteDebugInfo("Socket Connected to " + client.RemoteEndPoint.ToString()); }

                    Thread.Sleep(1000);
                    byte[] byteData;
                    int bytesSent = -1;
                    int bytesRead = -1;

                    if (ACC_Server.Port == CommandPort)
                    {
                        //encode outgoing message
                        byteData = Encoding.ASCII.GetBytes("\n\r");

                        //send outgoing message
                        if (Program.debugMode) { Debug.WriteDebugInfo("Sending command: carriage return and line feed"); }
                        bytesSent = client.Send(byteData, byteData.Length, SocketFlags.None);

                        Thread.Sleep(1000);

                        //get incoming message
                        if (Program.debugMode) { Debug.WriteDebugInfo("Getting Return String"); }
                        bytesRead = client.Receive(bytes, client.Available, SocketFlags.None);
                        if (Program.debugMode) { Debug.WriteDebugInfo("Bytes Read:  " + bytesRead.ToString()); }

                        if (bytesRead > 0)
                        {
                            strResponse = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                        }
                        if (Program.debugMode) { Debug.WriteDebugInfo("Result:  " + strResponse); }

                    }

                    //encode outgoing message
                    if (Program.debugMode) { Debug.WriteDebugInfo("Sending command: " + CMD); }
                    CMD = CMD + "\n";
                    byteData = Encoding.ASCII.GetBytes(CMD);

                    //send outgoing message
                    bytesSent = client.Send(byteData, byteData.Length, SocketFlags.None);

                    Thread.Sleep(1000);

                    //get incoming message
                    if (Program.debugMode) { Debug.WriteDebugInfo("Getting Return String"); }
                    bytesRead = client.Receive(bytes, client.Available, SocketFlags.None);
                    if (Program.debugMode) { Debug.WriteDebugInfo("Bytes Read:  " + bytesRead.ToString()); }

                    if (bytesRead > 0)
                    {
                        strResponse = Encoding.ASCII.GetString(bytes, 0, bytesRead);
                    }
                    if (Program.debugMode) { Debug.WriteDebugInfo("Result:  " + strResponse); }

                    Thread.Sleep(1000);

                    //close connection to server
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                catch (ArgumentNullException ANE)
                {
                    Console.WriteLine("Failed to Connect\n" + ANE.Message);
                    return -1;
                }
                catch (SocketException)
                {
                    Console.WriteLine("Failed to Connect to VEO2.\n\n Check all cable connections!\n\n"); // + SE.Message);
                    return -2;
                }
                catch (Exception Ex)
                {
                    Console.WriteLine("Failed to Connect\n" + Ex.Message);
                    return -3;
                }
            }
            catch (SocketException SE)
            {
                Console.WriteLine("Failed to Connect\n" + SE.Message);
                return -4;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Failed to Connect\n" + Ex.Message);
                return -5;
            }
            return 0;
        }

        public static string Main(String Device, String args)
        {
            int iErr = 0;

            iErr = StartClient(Device, args);

            if (iErr == 0)
            {
                return strResponse;
            }
            else
            {
                return iErr.ToString();
            }
        }
    }

    public class VEO2_Client
    {

        private const string VEO2_IP = "192.168.200.160";

        #region System

        /// <summary>
        /// Completes a full cycle of status and error condition checking
        /// </summary>
        public void CheckStatus()
        {
            string strStatus = String.Empty;
            string[] strDevices;
            string strErrCode = string.Empty;
            string strErrStr = string.Empty;

            while (strStatus != "0")
            {
                strStatus = GetSystemStatusRegister();

                switch (strStatus)
                {
                    case "16":
                        //System busy
                        break;
                    case "32":
                        //Errors present
                        strDevices = GetDevicesWithErrors().Split(Convert.ToChar(" "));

                        foreach (string strDevice in strDevices)
                        {
                            Debug.WriteDebugInfo(strDevice);
                            strErrCode = GetDeviceError(strDevice);
                            Debug.WriteDebugInfo(strErrCode);
                            strErrStr = GetErrorString(strDevice);
                            Debug.WriteDebugInfo(strErrStr);
                            ClearErrors(strDevice);
                        }

                        break;
                    case "48":
                        //System busy & Errors present
                        break;
                    default:
                        break;
                }
            }
        }

        public string GetBITStatus()
        {
            string strRes = SynchronousClient.Main("VEO2", "MK \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetErrorCodes()
        {
            string strRes = SynchronousClient.Main("VEO2", "ME \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetErrorCodeString(string ErrorCode)
        {
            string strRes = SynchronousClient.Main("VEO2", "MES " + ErrorCode + "\n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        /// <summary>
        /// Returns a space delimited list of device address of modules that have errors
        /// </summary>
        /// <returns></returns>
        public string GetDevicesWithErrors()
        {
            string strRes = SynchronousClient.Main("VEO2", "MERRDEV \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        /// <summary>
        /// Returns the first error code for the specified device address
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public string GetDeviceError(string Device)
        {
            string strRes = SynchronousClient.Main("VEO2", "MERRGET " + Device + " \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        /// <summary>
        /// Returns the string explanation for the error code of the specified device address
        /// </summary>
        /// <param name="Error"></param>
        /// <returns></returns>
        public string GetErrorString(string Error)
        {
            string strRes = SynchronousClient.Main("VEO2", "MERRSTR " + Error + "\n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        /// <summary>
        /// Clears the error from the specified device address
        /// </summary>
        /// <param name="Device"></param>
        /// <returns></returns>
        public string ClearErrors(String Device)
        {
            string strRes = SynchronousClient.Main("VEO2", "ERRCLR " + Device + " \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetIDN()
        {
            string strRes = SynchronousClient.Main("VEO2", "*IDN? \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetIP()
        {
            string strRes = SynchronousClient.Main("VEO2", "MSYSIP \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetMainAppStatus()
        {
            string strRes = SynchronousClient.Main("VEO2", "MAPPSTAT \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetName()
        {
            string strRes = SynchronousClient.Main("VEO2", "MSYSNAME \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetPartNumber()
        {
            string strRes = SynchronousClient.Main("VEO2", "MPARTNUM\n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 1);
            strRes = strRes.Replace(" ", "");
            strRes = strRes.Replace("\n\r", "");
            strRes = strRes.Trim();
            return strRes;
        }

        public string GetQueueSize()
        {
            string strRes = SynchronousClient.Main("VEO2", "MQSZ \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetSerialNumber()
        {
            string strRes = SynchronousClient.Main("VEO2", "MSERNUM\n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 1);
            strRes = strRes.Replace(" ", "");
            strRes = strRes.Replace("\n\r", "");
            strRes = strRes.Trim();
            return strRes;
        }

        public string GetSystemStatusRegister()
        {
            string strRes = SynchronousClient.Main("VEO2", "MS \n");

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        #endregion

        #region Common

        public string GetCameraPower()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MCPWR \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetLarrsAz()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MWB \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetLarrsEl()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MWA \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        #endregion

        #region Blackbody

        public string GetAbsoluteTemp()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MG \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetAmbientTemp()
        {
            string strRes = AsynchronousClient.Main("VEO2", "M2 \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetBlackbodyMode()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MSG \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetDifferentialTemp()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MD \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetTargetWheelTemp()
        {
            string strRes = AsynchronousClient.Main("VEO2", "M1 \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }

            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        #endregion

        #region Visible Source

        public string GetColorTemp()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MVG \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetCornerCubePosition()
        {
            string strRes = SynchronousClient.Main("VEO2", "MCC");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Replace("You have connected to the ACC remote control server.", "");
            strRes = strRes.Replace("Press Enter for prompt.", "");
            strRes = strRes.Replace("ACC >", "");
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            strRes = strRes.Trim();
            return strRes;
        }

        public string GetRadianceSetPoint()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MRADSP \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetRadiance()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MR \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        public string GetVaneControlStatus()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MRSTATE \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        #endregion

        #region Laser

        public string GetSelectedLaserDiode()
        {
            string strRes = AsynchronousClient.Main("VEO2", "MLSRSEL \n");
            int i = 0;
            while (strRes == string.Empty || i < 20)
            {
                i++;
            }
            strRes = strRes.Substring(strRes.IndexOf("=") + 2);
            strRes = strRes.Replace("\n\r", "");
            return strRes;
        }

        #endregion

        public bool IsReady()
        {
            bool bSuccess = false;

            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();

            System.Net.NetworkInformation.PingReply reply = ping.Send(VEO2_IP);

            System.Net.NetworkInformation.IPStatus status = reply.Status;

            switch (status)
            {
                case System.Net.NetworkInformation.IPStatus.Success:
                    bSuccess = true;
                    break;
                case System.Net.NetworkInformation.IPStatus.DestinationNetworkUnreachable:
                    break;
                case System.Net.NetworkInformation.IPStatus.DestinationHostUnreachable:
                    break;
                case System.Net.NetworkInformation.IPStatus.DestinationProtocolUnreachable:
                    break;
                case System.Net.NetworkInformation.IPStatus.DestinationPortUnreachable:
                    break;
                case System.Net.NetworkInformation.IPStatus.NoResources:
                    break;
                case System.Net.NetworkInformation.IPStatus.BadOption:
                    break;
                case System.Net.NetworkInformation.IPStatus.HardwareError:
                    break;
                case System.Net.NetworkInformation.IPStatus.PacketTooBig:
                    break;
                case System.Net.NetworkInformation.IPStatus.TimedOut:
                    break;
                case System.Net.NetworkInformation.IPStatus.BadRoute:
                    break;
                case System.Net.NetworkInformation.IPStatus.TtlExpired:
                    break;
                case System.Net.NetworkInformation.IPStatus.TtlReassemblyTimeExceeded:
                    break;
                case System.Net.NetworkInformation.IPStatus.ParameterProblem:
                    break;
                case System.Net.NetworkInformation.IPStatus.SourceQuench:
                    break;
                case System.Net.NetworkInformation.IPStatus.BadDestination:
                    break;
                case System.Net.NetworkInformation.IPStatus.DestinationUnreachable:
                    break;
                case System.Net.NetworkInformation.IPStatus.TimeExceeded:
                    break;
                case System.Net.NetworkInformation.IPStatus.BadHeader:
                    break;
                case System.Net.NetworkInformation.IPStatus.UnrecognizedNextHeader:
                    break;
                case System.Net.NetworkInformation.IPStatus.IcmpError:
                    break;
                case System.Net.NetworkInformation.IPStatus.DestinationScopeMismatch:
                    break;
                case System.Net.NetworkInformation.IPStatus.Unknown:
                    break;
                default:
                    break;
            }
            return bSuccess;
        }
    }
}
