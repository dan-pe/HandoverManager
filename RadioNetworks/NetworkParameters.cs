namespace RadioNetworks
{
    public class NetworkParameters
    {
        #region Properties

        public double ThroughputInMbps { get; set; }
        public double BitErrorRate { get; set; }
        public double BurstErrorRate { get; set; }
        public double PacketLossPercentage { get; set; }
        public double DelayInMsec { get; set; }
        public double ResponseTimeInMsec { get; set; }
        public double JitterInMsec { get; set; }
        public double SecurityLevel { get; set; }
        public double CostInUnitsPerByte { get; set; }

        #endregion
    }
}