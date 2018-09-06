﻿#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logger;
using NetTool;
using NetworkManager;

#endregion

namespace NetworkMonitors
{
    #region Usings

    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using RadioNetworks;

    #endregion

    public class NetworkMonitor
    {
        #region Constructors

        public NetworkMonitor(NetworkInterfaceManagerBase networkInterface)
        {
            this._networkInterface = networkInterface;
        }

        #endregion

        #region Properties

        private readonly NetworkInterfaceManagerBase _networkInterface;

        #endregion

        #region Fields

        private int TimeoutInMsec = SettingsHandler.GetInstance().PingTimeoutInMsec;
        private int BufferSizeInBytes = SettingsHandler.GetInstance().BufferSizeInBytes;
        private int PingCount = SettingsHandler.GetInstance().PingCount;

        #endregion

        #region Public Methods

        public NetworkParameters EvaluateNetwork()
        {
            var networkParameters = new NetworkParameters
            {
                ResponseTimeInMsec = this.ResponseTest(),
                //ThroughputInMbps = this.ThroughoutputTestNew(),
                PacketLossPercentage = this.PacketLossTest(),
                SecurityLevel = this.GetSecurityLevel()
            };

            return networkParameters;
        }

        private double GetSecurityLevel()
        {
            switch (this._networkInterface.NetworkInterface.NetworkInterfaceType)
            {
                case NetworkInterfaceType.Wireless80211:
                    return 3;
                default:
                    return 5;
            }
        }

        #endregion

        #region Private Fields

        private double PacketLossTest()
        {
            

            var options = new PingOptions
            {
                DontFragment = true
            };

            var data = new char[BufferSizeInBytes];

            for (var i = 0; i < BufferSizeInBytes; i++)
            {
                data[i] = 'a';
            }

            var buffer = Encoding.ASCII.GetBytes(data);
            int failed = 0;
           

            for (int i = 0; i < PingCount; i++)
            {
                
                var reply = Icmp.Send(this._networkInterface.IpAddress, this._networkInterface.GatewayIpAddress, TimeoutInMsec, buffer, options);
                if (reply.Status != IPStatus.Success)
                {
                    failed += 1;
                    Logger.Logger.AddMessage($"Ping status for {i} - {reply.Status.ToString()} ", MessageThreshold.WARNING);
                }
            } 

            return ((double)failed / PingCount) * 100;
        }

        private double ResponseTest()
        {
            double meanLatency = 0;

            var pingReply = Icmp.Send(this._networkInterface.IpAddress, this._networkInterface.GatewayIpAddress, TimeoutInMsec);

            for (int i = 0; i < PingCount; i++)
            {
                if (pingReply == null) continue;

                pingReply = Icmp.Send(this._networkInterface.IpAddress, this._networkInterface.GatewayIpAddress);
                if (pingReply != null) meanLatency += pingReply.RoundTripTime.TotalMilliseconds;

            }

            return meanLatency / PingCount;
        }

        private double ConvertBytestoMbytes(long bytes)
        {
            const double bytesToMegaBytesConst = 1024f;

            return (bytes / bytesToMegaBytesConst) / bytesToMegaBytesConst;
        }

        private double ThroughoutputTestNew()
        {
            var serverList = SettingsHandler.GetInstance().ServerList;
            List<double> results = new List<double>();
            foreach (var serverName in serverList)
            {
                var mbpsResult = 0.0;

                if (serverName.Contains("ftp"))
                {
                    mbpsResult = DownloadViaFtp(serverName);
                }

                if (serverName.Contains("http"))
                {
                    mbpsResult = DownloadViaHttp(serverName);
                }

                results.Add(mbpsResult);
                Logger.Logger.AddMessage($"Evaluation of network: {mbpsResult} MBps");
            }

            var result = results.Sum() / results.Count;
            return result;
        }

        public double DownloadViaFtp(string urlSource)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlSource);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.Timeout = 20 * 10000;
            request.ReadWriteTimeout = 20 * 10000;
            request.Credentials = new NetworkCredential("anonymous", "anonymous");

            request.ServicePoint.BindIPEndPointDelegate = delegate
            {
                return new IPEndPoint(this._networkInterface.IpAddress, 0);
            };

            var response = (FtpWebResponse)request.GetResponse();
            var length = response.ContentLength;
            var startTime = DateTime.Now;

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            reader.ReadToEnd();
            Console.WriteLine($"Download Complete via FTP, status {response.StatusDescription}");

            reader.Close();
            response.Close();

            var endTime = DateTime.Now;
            var totalSecondsDiff = (endTime - startTime).TotalSeconds;
            var mBytes = this.ConvertBytestoMbytes(length);
            Logger.Logger.AddMessage($"Testing for {this._networkInterface.IpAddress} done. Elapsed time {totalSecondsDiff}s" );
            return mBytes / totalSecondsDiff;
        }

        public double DownloadViaHttp(string urlSource)
        {
            var startTime = DateTime.Now;
            var length = 0;
            var request = (HttpWebRequest)WebRequest.Create(urlSource);
            request.ServicePoint.BindIPEndPointDelegate = delegate {
                return new IPEndPoint(this._networkInterface.IpAddress, 0);
            };

            Logger.Logger.AddMessage($"Binding EndPoint to: {this._networkInterface.IpAddress}");

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                length = (int)response.ContentLength;
                Logger.Logger.AddMessage($"Starting stress test ...");

            }
            catch (Exception e)
            {
                Logger.Logger.AddMessage($"Error occurred while getting: {request.RequestUri} message {e.Message}");
            }

            var endTime = DateTime.Now;
            var totalSecondsDiff = (endTime - startTime).TotalSeconds;
            var mBytes = this.ConvertBytestoMbytes(length);
            Logger.Logger.AddMessage($"Testing for {this._networkInterface.IpAddress} done.");
            return mBytes / totalSecondsDiff;
        }
        
        #endregion
    }
}