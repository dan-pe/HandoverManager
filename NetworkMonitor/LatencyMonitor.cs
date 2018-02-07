using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace NetworkMonitors
{
    public class LatencyMonitor : NetworkMonitorBase
    {
        private const int Iterations = 10;

        public async Task<long> GetLatencyAsync()
        {
            var ping = new Ping();
            var ip = this.IpAddress.GetAddressBytes();
            long meanLatency = 0;

            var pingReply = ping.Send(new IPAddress(ip));
            for (int i = 0; i < Iterations; i++)
            {
                if (pingReply == null) continue;
                pingReply = ping.Send(new IPAddress(ip));
                if (pingReply != null) meanLatency += pingReply.RoundtripTime;
            }

            return meanLatency / Iterations;
        }

        public LatencyMonitor(IPAddress ipAddress) : base(ipAddress)
        {
        }
    }
}