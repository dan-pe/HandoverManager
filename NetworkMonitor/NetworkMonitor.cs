namespace NetworkMonitors
{
    #region Usings

    using System;
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
        }

        #endregion

        #region Properties

        private readonly NetworkInterface _networkInterface;

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
            var initialPacketsDiscarded = this._networkInterface.GetIPv4Statistics().OutgoingPacketsDiscarded;

            StressNetwork();

            var endPacketsDiscarded = this._networkInterface.GetIPv4Statistics().OutgoingPacketsDiscarded;

            return (double)(endPacketsDiscarded - initialPacketsDiscarded) / 100;
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
                pingReply = ping.Send(this.IpAddress.ToString());
                if (pingReply != null) meanLatency += pingReply.RoundtripTime;
            }

            return meanLatency / iterations;
        }

        private double ThroughoutputTest()
        {
            var initialBytesRecived = this._networkInterface.GetIPv4Statistics().BytesReceived;
            const int bytesToMegaBytesConst = 1048576;

            this.StressNetwork();

            var endBytesRecived = this._networkInterface.GetIPv4Statistics().BytesReceived;

            return (double)(endBytesRecived - initialBytesRecived) / bytesToMegaBytesConst;
        }

        private void StressNetwork()
        {
                var request = (HttpWebRequest)WebRequest.Create("http://gameranx.com/wp-content/uploads/2016/06/Scalebound-4K-Wallpaper.jpg");
                request.ServicePoint.BindIPEndPointDelegate = delegate {
                    return new IPEndPoint(this.IpAddress, 0);
                };
                var response = (HttpWebResponse)request.GetResponse();

                response.Close();
        }

        #endregion
    }
}