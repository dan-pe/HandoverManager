using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

namespace NetworkMonitors
{
    public class BandwidthMonitor : NetworkMonitorBase
    {
        private readonly NetworkInterface _selectedInterface;

        public long GetThroughoutputInBytes()
        {
            var startingBytes = this._selectedInterface.GetIPv4Statistics().BytesSent;

            // Do some stress tests.
            Console.WriteLine("Stress testing in progress...");
            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            var endingBytes = this._selectedInterface.GetIPv4Statistics().BytesSent;
            var result = endingBytes - startingBytes;
            Console.WriteLine($"Bytes sent: {result}");
            Console.WriteLine($"Packets lost: {this._selectedInterface.GetIPv4Statistics().IncomingPacketsWithErrors}");

            return result;
        }

        public BandwidthMonitor(IPAddress ipAddress) : base(ipAddress)
        {
            this._selectedInterface = this.NetworkInterfaces.First(i => i.NetworkInterfaceType == NetworkInterfaceType.Ethernet);
            Console.WriteLine($"Selected: {this._selectedInterface.Description}");
            Console.WriteLine($"Interface type: {this._selectedInterface.NetworkInterfaceType.ToString()}");
            Console.WriteLine($"Supported Interface speed: {this._selectedInterface.Speed}");
        }
    }
}