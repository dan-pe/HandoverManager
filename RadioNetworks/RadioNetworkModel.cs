#region Usings

using System.Collections.Generic;

#endregion


namespace RadioNetwork
{
    public class RadioNetworkModel
    {
        public RadioNetworkModel(string name, NetworkType networkType, NetworkParameters networkParameters)
        {
            NetworkName = name;
            NetworkType = networkType;
            Parameters = networkParameters;
        }

        #region Properties

        public string NetworkName { get; set; }
        public NetworkType NetworkType { get; set; }
        public NetworkParameters Parameters { get; set; }

        #endregion

    }

    #region Enums
    public enum NetworkType
    {
        LTE,
        WiFi,
        WiMax,
        LTE_Advanced,
        UMTS,
        GPRS
    }
    #endregion

}
