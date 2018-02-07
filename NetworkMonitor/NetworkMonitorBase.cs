using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace NetworkMonitors
{
    public class NetworkMonitorBase
    {
        protected readonly IPAddress IpAddress;

        protected IEnumerable<NetworkInterface> NetworkInterfaces;

        protected readonly List<NetworkInterfaceType> SupportedInterfaces = new List<NetworkInterfaceType>();

        public NetworkMonitorBase(IPAddress ipAddress)
        {
            this.RegisterSupportedInterfaces();
            this.IpAddress = ipAddress;
            this.NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        }

        private void RegisterSupportedInterfaces()
        {
            // For testing purposes
            this.SupportedInterfaces.Add(NetworkInterfaceType.Ethernet);

            this.SupportedInterfaces.AddRange(
                new List<NetworkInterfaceType>()
                {
                    NetworkInterfaceType.Wireless80211,
                    NetworkInterfaceType.Wwanpp,
                    NetworkInterfaceType.Wwanpp2,
                    NetworkInterfaceType.Wman
                });

        }

        public void GetStressTestMock()
        {
            //Console.WriteLine("Specify IpAddress: ");
            ////var inputAddress = Console.ReadLine();
            //var inputAddress = "192.168.1.1";

            //IPAddress address = IPAddress.Parse(inputAddress ?? throw new InvalidOperationException());
            //var latencyMonitor = new LatencyMonitor(address);

            //Task<long> getLantencyTask = latencyMonitor.GetLatencyAsync();

            //    while (!getLantencyTask.IsCompleted)
            //{
            //    Console.WriteLine("Asynchronous operations is not completed!");
            //}

            //var latency = getLantencyTask;

            //Console.WriteLine($"Mean latency for address: {inputAddress} is {getLantencyTask.Result}ms \n");

            //var bandwidthMonitor = new BandwidthMonitor(address);
            //bandwidthMonitor.GetThroughoutputInBytes();

            //Console.ReadKey();
        }

    }
}