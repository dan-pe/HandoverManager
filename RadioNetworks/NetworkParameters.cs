using System;

namespace RadioNetworks
{
    public class NetworkMetricAttribute : Attribute
    {
    }

    public class NetworkParameters
    {
        [NetworkMetric]
        public double DelayInMsec { get; set; }

        [NetworkMetric]
        public double PacketLossPercentage { get; set; }

        [NetworkMetric]
        public double ResponseTimeInMsec { get; set; }

        [NetworkMetric]
        public double SecurityLevel { get; set; }

        [NetworkMetric]
        public double ThroughputInMbps { get; set; }
    }
}