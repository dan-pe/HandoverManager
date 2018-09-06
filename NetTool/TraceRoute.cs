using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;

namespace NetTool
{
        public class TraceRoute
        {
            private const string Data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            public static IEnumerable<IPAddress> GetTraceRoute(IPAddress localIpAddress, IPAddress hostNameOrAddress)
            {
                var result = GetTraceRoute(localIpAddress, hostNameOrAddress, 4);
                return result;
            }
            private static IEnumerable<IPAddress> GetTraceRoute(IPAddress localIpAddress, IPAddress hostNameOrAddress, int ttl)
            {
                var pingOptions = new PingOptions(ttl, true);
                int timeout = 10000;
                byte[] buffer = Encoding.ASCII.GetBytes(Data);

                var reply = Icmp.Send(localIpAddress, hostNameOrAddress, timeout, buffer, pingOptions);

                List<IPAddress> result = new List<IPAddress>();
                if (reply.Status == IPStatus.Success)
                {
                    result.Add(reply.IpAddress);
                }
                else if (reply.Status == IPStatus.TtlExpired || reply.Status == IPStatus.TimedOut)
                {
                    //add the currently returned address if an address was found with this TTL
                    if (reply.Status == IPStatus.TtlExpired) result.Add(reply.IpAddress);
                    //recurse to get the next address...
                    IEnumerable<IPAddress> tempResult = default(IEnumerable<IPAddress>);
                    tempResult = GetTraceRoute(localIpAddress, hostNameOrAddress, ttl + 1);
                    result.AddRange(tempResult);
                }
                else
                {
                    //failure 
                }

                return result;
            }
        }
}
