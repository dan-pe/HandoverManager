using HuaweiWebAPI;
using HuaweiWebAPI.Structs;

namespace NetworkManager
{
    public class NdisNetworkInterfaceManager : NetworkInterfaceManagerBase, INetworkInterface
    {
        private readonly BasicInformation _basicInformation;

        public NdisNetworkInterfaceManager()
        {
            this._basicInformation = HuaweiWebAPI.HuaweiWebApi.BasicInformation();
        }

        public string GetInterfaceName()
        {
            return _basicInformation.DeviceName;
        }

        public string GetInterfaceType()
        {
            return ((NetworkType)HuaweiWebApi.MonitoringStatus().CurrentNetworkType).ToString();
        }

        public string GetInterfaceSpeed()
        {
            return "NotImplemented";
        }
    }

}
