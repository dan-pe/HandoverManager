namespace handovermgr.UIConfig {
    class InputWeights
    {
        #region Properties

        public int ThroughputWeight { get; set; }
        public int BitErrorRateWeight { get; set; }
        public int BurstErrorRateWeight { get; set; }
        public int PacketLossWeight { get; set; }
        public int DelayWeight { get; set; }
        public int ResponseWeight { get; set; }
        public int JitterInMsecWeight { get; set; }
        public int SecurityWeight { get; set; }
        public int CostWeight { get; set; }

    #endregion
    }
}
