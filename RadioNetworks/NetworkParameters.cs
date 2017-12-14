namespace RadioNetworks
{
    #region Usings

    using System;

    #endregion


    public class NetworkParameters
    {
        #region Properties

        [NetworkMetric]
        public double ThroughputInMbps { get; set; }

        [NetworkMetric]
        public double BitErrorRate { get; set; }

        [NetworkMetric]
        public double BurstErrorRate { get; set; }

        [NetworkMetric]
        public double PacketLossPercentage { get; set; }

        [NetworkMetric]
        public double DelayInMsec { get; set; }

        [NetworkMetric]
        public double ResponseTimeInMsec { get; set; }

        [NetworkMetric]
        public double JitterInMsec { get; set; }

        [NetworkMetric]
        public double SecurityLevel { get; set; }

        [NetworkMetric]
        public double CostInUnitsPerByte { get; set; }

        #endregion
    }

    public class NetworkMetricAttribute : Attribute
    {
    }
}