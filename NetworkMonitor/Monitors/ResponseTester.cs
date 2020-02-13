using System;
using System.Net.NetworkInformation;

namespace NetworkMonitors.Monitors
{
    public class ResponseTester
    {
        private readonly NetworkInterface _networkInterface;
        private readonly string testedAddress;
        private int pingInterval;

        public ResponseTester(NetworkInterface networkInterface, string testedAddress)
        {
            this._networkInterface = networkInterface;
            this.testedAddress = testedAddress;
            this.pingInterval = 2;
        }

        public double ResponseTest()
        {
            var ping = new Ping();

            double meanLatency = 0;
            const int iterations = 50;

            var pingReply = ping.Send(this.testedAddress);

            for (int i = 0; i < iterations; i++)
            {
                if (pingReply == null) continue;

                pingReply = ping.Send(this.testedAddress ?? throw new InvalidOperationException());
                //Thread.Sleep(TimeSpan.FromSeconds(SettingsHandler.GetInstance().PingInterval));
                if (pingReply != null) meanLatency += pingReply.RoundtripTime;
            }

            return meanLatency / iterations;
        }

        //bool onetaskCompleted = false;
        //private readonly Object obj = new Object();

        //private async Task<List<PingReply>> PingAsync()
        //{
        //    Ping pingSender = new Ping();
        //    var tasks = theListOfIPs.Select(ip => pingSender.SendPingAsyn(ip, 2000));
        //    List<Task> continuetask = new List<Task>();
        //    foreach (Task t in taks)
        //    {
        //        continuetask.Add(t.ContinueWith(Action));
        //    }

        //    var results = await Task.WhenAll(continuetask);

        //    return results.ToList();
        //}

        //private void ContinuationAction()
        //{
        //    lock (obj)
        //    {
        //        if (!onetaskCompleted)
        //            onetaskCompleted = true;
        //    }
        //    if (!onetaskCompleted)
        //    {
        //        //execute code
        //    }
        //}
    }
}