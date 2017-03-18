namespace RadioNetwork
{
    public class RadioNetworkParameters
    {
        #region Properties

        protected float ThroughputInMbps { get; set; }
        protected float BitErrorRate { get; set; }
        protected float BurstErrorRate { get; set; }
        protected float PacketLossPercentage { get; set; }
        protected int DelayInMsec { get; set; }
        protected int ResponseTimeInMsec { get; set; }
        protected int JitterInMsec { get; set; }
        protected int SecurityLevel { get; set; }
        protected int CostInUnitsPerByte { get; set; }

        #endregion
    }
}