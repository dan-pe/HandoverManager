namespace RadioNetworks
{
    /// <summary>
    /// The radio network model.
    /// </summary>
    public class RadioNetworkModel
    {
        #region Constructor

        public RadioNetworkModel()
        {
        }

        public RadioNetworkModel(string name, string networkType, NetworkParameters networkParameters)
        {
            this.NetworkName = name;
            this.NetworkType = networkType;
            this.Parameters = networkParameters;
        }

        #endregion

        #region Properties

        public string NetworkName { get; set; }
        public string NetworkType { get; set; }
        public NetworkParameters Parameters { get; set; }

        #endregion
    }
}
