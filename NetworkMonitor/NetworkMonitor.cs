using Logger;
using NetTool;
using NetworkManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NetworkMonitors
{
    using RadioNetworks;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;

    public class NetworkMonitor
    {
        private readonly int _bufferSizeInBytes = SettingsHandler.GetInstance().BufferSizeInBytes;

        private readonly NetworkInterfaceManagerBase _networkInterface;

        private readonly int _pingCount = SettingsHandler.GetInstance().PingCount;

        private readonly int _timeoutInMsec = SettingsHandler.GetInstance().PingTimeoutInMsec;

        public NetworkMonitor(NetworkInterfaceManagerBase networkInterface)
        {
            this._networkInterface = networkInterface;
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
            Logger.Logger.AddMessage($"Testing for {this._networkInterface.IpAddress} done. Elapsed time {totalSecondsDiff}s");
            return mBytes / totalSecondsDiff;
        }

        public double DownloadViaHttp(string urlSource)
        {
            var startTime = DateTime.Now;
            var length = 0;
            var request = (HttpWebRequest)WebRequest.Create(urlSource);
            request.ServicePoint.BindIPEndPointDelegate = delegate
            {
                return new IPEndPoint(this._networkInterface.IpAddress, 0);
            };

            Logger.Logger.AddMessage($"Binding EndPoint to: {this._networkInterface.IpAddress}");

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);

                reader.ReadToEnd();
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

        public NetworkParameters EvaluateNetwork()
        {
            var networkParameters = new NetworkParameters
            {
                ResponseTimeInMsec = this.ResponseTest(),
                ThroughputInMbps = this.ThroughoutputTestNew(),
                PacketLossPercentage = this.PacketLossTest(),
                SecurityLevel = this.GetSecurityLevel()
            };

            return networkParameters;
        }

        private double ConvertBytestoMbytes(long bytes)
        {
            const double bytesToMegaBytesConst = 1024f;

            return (bytes / bytesToMegaBytesConst) / bytesToMegaBytesConst;
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

        private double PacketLossTest()
        {
            double packetLoss = 0.0d;
            var dnsAddresses = SettingsHandler.GetInstance().DnsAddresses;

            foreach (var dnsAddress in dnsAddresses)
            {
                packetLoss += PacketLossTestSignleRun(dnsAddress);
            }

            return packetLoss / dnsAddresses.Count;
        }

        private double PacketLossTestSignleRun(IPAddress destinationIpAddress)
        {
            var options = new PingOptions
            {
                DontFragment = true
            };

            var data = new char[_bufferSizeInBytes];

            for (var i = 0; i < _bufferSizeInBytes; i++)
            {
                data[i] = 'a';
            }

            var buffer = Encoding.ASCII.GetBytes(data);
            int failed = 0;

            for (int i = 0; i < _pingCount; i++)
            {
                var reply = Icmp.Send(this._networkInterface.IpAddress, destinationIpAddress, _timeoutInMsec, buffer, options);
                if (reply.Status != IPStatus.Success)
                {
                    failed += 1;
                    Logger.Logger.AddMessage($"Ping status for {i} - {reply.Status.ToString()} ", MessageThreshold.WARNING);
                }
            }

            return ((double)failed / _pingCount) * 100;
        }

        private double ResponseTest()
        {
            double response = 0.0d;
            var dnsAddresses = SettingsHandler.GetInstance().DnsAddresses;

            foreach (var dnsAddress in dnsAddresses)
            {
                response += ResponseTimeSingleTest(dnsAddress);
            }

            return response / dnsAddresses.Count;
        }

        private double ResponseTimeSingleTest(IPAddress destinationAddress)
        {
            double meanLatency = 0;

            var pingReply = Icmp.Send(this._networkInterface.IpAddress, destinationAddress, _timeoutInMsec);

            for (int i = 0; i < _pingCount; i++)
            {
                if (pingReply == null) continue;

                pingReply = Icmp.Send(this._networkInterface.IpAddress, destinationAddress);
                if (pingReply != null) meanLatency += pingReply.RoundTripTime.TotalMilliseconds;
            }

            return meanLatency / _pingCount;
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
    }
}