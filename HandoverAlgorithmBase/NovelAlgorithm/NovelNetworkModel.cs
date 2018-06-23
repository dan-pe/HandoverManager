#region Usings

using RadioNetworks;

#endregion

namespace HandoverAlgorithmBase.NovelAlgorithm
{
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
