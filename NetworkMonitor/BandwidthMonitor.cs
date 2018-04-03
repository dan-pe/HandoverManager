using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace NetworkMonitors
{
    using Logger;

    public class BandwidthMonitor : NetworkMonitorBase
    {
        private readonly System.Net.NetworkInformation.NetworkInterface _selectedInterface;

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

        public BandwidthMonitor()
        {
            
        }

        public void NewBandwidthMonitor()
        {
            var dnsHostAddresses = Dns.GetHostAddresses(Dns.GetHostName()).
                Where(addr => addr.AddressFamily == AddressFamily.InterNetwork);

            foreach (var ip in dnsHostAddresses)
            {
                Logger.AddMessage("Request from: " + ip);
                var request = (HttpWebRequest)WebRequest.Create("https://google.pl");
                request.ServicePoint.BindIPEndPointDelegate = delegate {
                    return new IPEndPoint(ip, 0);
                };
                var response = (HttpWebResponse)request.GetResponse();
                //Logger.AddMessage("Actual IP: " + response.GetResponseHeader("X-YourIP"));

                foreach (var responseHeader in response.Headers)           {
                    Logger.AddMessage($"header: {responseHeader.ToString()}");
                }
                response.Close();
            }
        }
    }
}