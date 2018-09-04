using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Logger;

namespace NetworkMonitors
{
    #region Usings

    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Threading;
    using RadioNetworks;

    #endregion

    public class NetworkMonitor
    {
        #region Constructors

        public NetworkMonitor(NetworkInterface networkInterface)
        {
            this._networkInterface = networkInterface;
            this._gatewayAddress = this._networkInterface.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address.ToString();
        }

        #endregion

        #region Properties

        private readonly NetworkInterface _networkInterface;
        private readonly string _gatewayAddress;

        private IPAddress IpAddress
        {
            get
            {
                var ipAddress = this._networkInterface.GetIPProperties().UnicastAddresses.FirstOrDefault(ua => ua.PrefixOrigin == PrefixOrigin.Dhcp)?.Address;
                return ipAddress;
            }
        }

        #endregion

        #region Public Methods

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

        private double GetSecurityLevel()
        {
            switch (this._networkInterface.NetworkInterfaceType)
            {
                case NetworkInterfaceType.Wireless80211:
                    return 5;
                default:
                    return 3;
                    
            }
        }

        #endregion

        #region Private Fields

        private double PacketLossTest()
        {
            var pingSender = new Ping();
            var options = new PingOptions
            {
                DontFragment = true
            };

            // Create a buffer of 32 bytes of data to be transmitted.
            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;
            int failed = 0;
            int pingAmount = 4000;
            for (int i = 0; i < pingAmount; i++)
            {
                
                var reply = pingSender.Send(this._gatewayAddress, timeout, buffer, options);
                if (reply.Status != IPStatus.Success)
                {
                    failed += 1;
                    Logger.Logger.AddMessage($"Ping status for {i} - {reply.Status.ToString()} ", MessageThreshold.WARNING);
                }
            } 

            return ((double)failed / pingAmount) * 100; ;
        }

        private double ResponseTest()
        {
            var ping = new Ping();
            
            double meanLatency = 0;
            const int iterations = 50;

            var pingReply = ping.Send(this._gatewayAddress);

            for (int i = 0; i < iterations; i++)
            {
                if (pingReply == null) continue;

                pingReply = ping.Send(this._gatewayAddress, TimeSpan.FromMilliseconds(80).Milliseconds);
                if (pingReply != null) meanLatency += pingReply.RoundtripTime;

            }

            return meanLatency / iterations;
        }

        private double ThroughoutputTest(string httpSource)
        {
            var startTime = DateTime.Now;
            var length = 0;
            var request = (HttpWebRequest)WebRequest.Create(httpSource);
            request.ServicePoint.BindIPEndPointDelegate = delegate {
                return new IPEndPoint(this.IpAddress, 0);
            };

            Logger.Logger.AddMessage($"Binding EndPoint to: {this.IpAddress}");

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                length = (int) response.ContentLength;
                Logger.Logger.AddMessage($"Starting stress test ...");

            }
            catch (Exception e)
            {
                Logger.Logger.AddMessage($"Error occurred while getting: {request.RequestUri} message {e.Message}");
            }

            var endTime = DateTime.Now;
            var totalSecondsDiff = (endTime - startTime).TotalSeconds;
            var mBytes = this.ConvertBytestoMbytes(length);
            Logger.Logger.AddMessage($"Testing for {this.IpAddress} done.");
            return mBytes / totalSecondsDiff;
        }

        private double ConvertBytestoMbytes(long bytes)
        {
            const double bytesToMegaBytesConst = 1024f;

            return (bytes / bytesToMegaBytesConst) / bytesToMegaBytesConst;
        }

        private double ThroughoutputTestNew()
        {
            var serverList = ServerListHandler.GetInstance().ServerList;
            List<double> results = new List<double>();
            foreach (var serverName in serverList)
            {
                var MbpsResult = 0.0;

                if (serverName.Contains("ftp"))
                {
                    MbpsResult = DownloadViaFtp(serverName);
                }

                if (serverName.Contains("http"))
                {
                    MbpsResult = DownloadViaHttp(serverName);
                }

                results.Add(MbpsResult);
                Logger.Logger.AddMessage($"Evaluation of network: {MbpsResult} MBps");
            }

            var result = results.Sum() / results.Count;
            return result;
        }

        public double DownloadViaFtp(string urlSource)
        {
            var startTime = DateTime.Now;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(urlSource);
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.UseBinary = true;
            request.UsePassive = true;
            request.Timeout = 20 * 10000;
            request.ReadWriteTimeout = 20 * 10000;
            request.Credentials = new NetworkCredential("anonymous", "anonymous");

            request.ServicePoint.BindIPEndPointDelegate = delegate
            {
                return new IPEndPoint(this.IpAddress, 0);
            };

            var response = (FtpWebResponse)request.GetResponse();
            var length = response.ContentLength;

            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            reader.ReadToEnd();

            //while (!reader.EndOfStream)
            //{
            //    var a = reader.Read();
            //}


            Console.WriteLine($"Download Complete via FTP, status {response.StatusDescription}");

            reader.Close();
            response.Close();

            var endTime = DateTime.Now;
            var totalSecondsDiff = (endTime - startTime).TotalSeconds;
            var mBytes = this.ConvertBytestoMbytes(length);
            Logger.Logger.AddMessage($"Testing for {this.IpAddress} done. Elapsed time {totalSecondsDiff}s" );
            return mBytes / totalSecondsDiff;
        }

        public double DownloadViaHttp(string urlSource)
        {
            var startTime = DateTime.Now;
            var length = 0;
            var request = (HttpWebRequest)WebRequest.Create(urlSource);
            request.ServicePoint.BindIPEndPointDelegate = delegate {
                return new IPEndPoint(this.IpAddress, 0);
            };

            Logger.Logger.AddMessage($"Binding EndPoint to: {this.IpAddress}");

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
            Logger.Logger.AddMessage($"Testing for {this.IpAddress} done.");
            return mBytes / totalSecondsDiff;
        }
        #endregion
    }
}