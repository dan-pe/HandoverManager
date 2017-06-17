namespace RadioNetwork
{
    public class NetworkParameters
    {
        #region Properties

        public float ThroughputInMbps { get; set; }
        public float BitErrorRate { get; set; }
        public float BurstErrorRate { get; set; }
        public float PacketLossPercentage { get; set; }
        public float DelayInMsec { get; set; }
        public float ResponseTimeInMsec { get; set; }
        public float JitterInMsec { get; set; }
        public float SecurityLevel { get; set; }
        public float CostInUnitsPerByte { get; set; }

        #endregion
    }
}