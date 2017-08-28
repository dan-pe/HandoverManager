namespace HandoverAlgorithmBase.PlainAlgorithms.NovelAlgorithm
{
    #region Usings

    using RadioNetworks;

    #endregion

    #region Constructor

    /// <summary>
    /// Novel network model.
    /// </summary>
    public class NovelNetworkModel
    {
        public RadioNetworkModel RadioNetworkModel { get; set; }
        public double GrcFactor { get; set; }
    }

    #endregion
}
