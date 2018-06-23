#region Usings

using System;

#endregion

namespace RadioNetworks
{

    public class NetworkParameters
    {
        #region Properties

        [NetworkMetric]
        public double ThroughputInMbps { get; set; }

        [NetworkMetric]
        public double PacketLossPercentage { get; set; }

        [NetworkMetric]
        public double DelayInMsec { get; set; }

        [NetworkMetric]
        public double ResponseTimeInMsec { get; set; }

        [NetworkMetric]
        public double SecurityLevel { get; set; }
        
        #endregion
    }

    public class NetworkMetricAttribute : Attribute
    {
    }
}