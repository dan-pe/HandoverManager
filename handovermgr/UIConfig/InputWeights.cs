namespace handovermgr.UIConfig
{
    internal class InputWeights
    {
        public static int BitErrorRateWeight { get; set; }
        public static int BurstErrorRateWeight { get; set; }
        public static int CostWeight { get; set; }
        public static int DelayWeight { get; set; }
        public static int JitterInMsecWeight { get; set; }
        public static int PacketLossWeight { get; set; }
        public static int ResponseWeight { get; set; }
        public static int SecurityWeight { get; set; }
        public static int ThroughputWeight { get; set; }
    }
}