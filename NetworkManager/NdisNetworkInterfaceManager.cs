namespace NetworkManager
{
    public class NdisNetworkInterfaceManager : NetworkInterfaceManager
    {
        public NdisNetworkInterfaceManager()
        {
            var basicInterfaceInformation = HuaweiWebAPI.HuaweiWebApi.BasicInformation();

            this.Name = basicInterfaceInformation.DeviceName;
            this.Type = basicInterfaceInformation.ProductFamily;

            var costam = HuaweiWebAPI.HuaweiWebApi.MonitoringStatus();
        }
    }

}
