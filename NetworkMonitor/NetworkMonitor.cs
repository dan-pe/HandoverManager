using System;
using System.CodeDom;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Logger;

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

        public NetworkMonitor(NetworkInterface networkInterface)
        {
            this._networkInterface = networkInterface;
            this._gatewayAddress = this._networkInterface.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address.ToString();
        }

        #endregion

        #region Properties

        private readonly NetworkInterface _networkInterface;
        private string _gatewayAddress;

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
                ThroughputInMbps = this.ThroughoutputTest(),
                PacketLossPercentage = this.PacketLossTest(),
                SecurityLevel = 5
            };

            return networkParameters;
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
            const int iterations = 10;

            var pingReply = ping.Send(this.IpAddress);

            for (int i = 0; i < iterations; i++)
            {
                if (pingReply == null) continue;

                var address = this._networkInterface.GetIPProperties().GatewayAddresses.FirstOrDefault()?.Address.ToString();
                pingReply = ping.Send(address ?? throw new InvalidOperationException());
                if (pingReply != null) meanLatency += pingReply.RoundtripTime;
            }

            return meanLatency / iterations;
        }

        private double ThroughoutputTest()
        {
            var startTime = DateTime.Now;
            var length = 0;
            var request = (HttpWebRequest)WebRequest.Create("http://gameranx.com/wp-content/uploads/2016/06/Scalebound-4K-Wallpaper.jpg");
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
            Logger.Logger.AddMessage($"Testing for {this._gatewayAddress} done.");
            return mBytes / totalSecondsDiff;
        }

        private void StressNetwork()
        {

            //var tcpUtil = new TcpUtil.TcpUtil(this.IpAddress.ToString());


                var request = (HttpWebRequest)WebRequest.Create("http://gameranx.com/wp-content/uploads/2016/06/Scalebound-4K-Wallpaper.jpg");
                request.ServicePoint.BindIPEndPointDelegate = delegate {
                    return new IPEndPoint(this.IpAddress, 0);
                };

            Logger.Logger.AddMessage($"Binding EndPoint to: {this.IpAddress}");

            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var length = response.ContentLength;
                var inBytes = this.ConvertBytestoMbytes(length);
                Logger.Logger.AddMessage($"Starting stress test");

            }
            catch (Exception e)
            {
                Logger.Logger.AddMessage($"Error occurred while getting: {request.RequestUri} message {e.Message}");

            }
        }

        private double ConvertBytestoMbytes(long bytes)
        {
            const double bytesToMegaBytesConst = 1024f;

            return (bytes / bytesToMegaBytesConst) / bytesToMegaBytesConst;
        }

        #endregion
    }
}