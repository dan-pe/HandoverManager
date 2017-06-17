namespace handovermgr.UIConfig
{
    class InputWeights
    {
        #region Properties

        public static int ThroughputWeight { get; set; }
        public static int BitErrorRateWeight { get; set; }
        public static int BurstErrorRateWeight { get; set; }
        public static int PacketLossWeight { get; set; }
        public static int DelayWeight { get; set; }
        public static int ResponseWeight { get; set; }
        public static int JitterInMsecWeight { get; set; }
        public static int SecurityWeight { get; set; }
        public static int CostWeight { get; set; }

    #endregion
    }
}
