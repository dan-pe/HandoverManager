namespace NetworkMonitors
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.NetworkInformation;
    using System.Threading;
    using RadioNetworks;

    #endregion

    public class NetworkMonitorBase
    {
        #region Constructors

        public NetworkMonitorBase(INetworkInterface iNetworkInterface, IPAddress ipAddress)
        {
            //this.IpAddress = ipAddress;
            this.NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        }

        public NetworkMonitorBase(IPAddress ipAddress)
        {
            //this.IpAddress = ipAddress;
            this.NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        }

        public NetworkMonitorBase()
        {
            this.NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        }

        #endregion

        #region Properties

        protected IPAddress IpAddress
        {
            get
            {
                return new IPAddress(new byte[] { 192, 168, 1, 1 });
            }
        }

        protected IEnumerable<NetworkInterface> NetworkInterfaces;

        protected readonly List<NetworkInterfaceType> SupportedInterfaces = new List<NetworkInterfaceType>();

        #endregion

        #region Public Methods

        public NetworkInterface GetSelectedInterface()
        {
            return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(i => i.OperationalStatus == OperationalStatus.Up);
        }

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
            var initialPacketsDiscarded = this.GetSelectedInterface().GetIPv4Statistics().OutgoingPacketsDiscarded;

            // TODO: Perform stress tests.
            Thread.Sleep(TimeSpan.FromSeconds(20));

            var endPacketsDiscarded = this.GetSelectedInterface().GetIPv4Statistics().OutgoingPacketsDiscarded;

            return (double)(endPacketsDiscarded - initialPacketsDiscarded) / 100;
        }

        private double ResponseTest()
        {
            var ping = new Ping();
            double meanLatency = 0;
            const int iterations = 10;

            //var pingReply = ping.Send(this.IpAddress.ToString());
            var ipAddrString = this.IpAddress.ToString();
            var pingReply = ping.Send("192.168.1.1");
            var pingReply2 = ping.Send(this.IpAddress.ToString());


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
            var initialBytesRecived = this.GetSelectedInterface().GetIPv4Statistics().BytesReceived;
            const int bytesToMegaBytesConst = 1048576;

            // TODO: Perform stress tests.
            Thread.Sleep(TimeSpan.FromSeconds(20));

            var endBytesRecived = this.GetSelectedInterface().GetIPv4Statistics().BytesReceived;

            return (double)(endBytesRecived - initialBytesRecived) / bytesToMegaBytesConst;
        }

        #endregion
    }
}