namespace RadioNetworks
{
    /// <summary>
    /// The radio network model.
    /// </summary>
    public class RadioNetworkModel
    {
        public RadioNetworkModel()
        {
        }

        public RadioNetworkModel(string name, string networkType, NetworkParameters networkParameters)
        {
            this.NetworkName = name;
            this.NetworkType = networkType;
            this.Parameters = networkParameters;
        }

        public string NetworkName { get; set; }
        public string NetworkType { get; set; }
        public NetworkParameters Parameters { get; set; }
    }
}