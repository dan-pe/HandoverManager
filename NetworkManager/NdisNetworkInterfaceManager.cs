namespace NetworkManager
{
    public class NdisNetworkInterfaceManager : NetworkInterfaceManager
    {
        public NdisNetworkInterfaceManager()
        {
            this.Name = HuaweiWebAPI.HuaweiWebApi.GetBasicInformation()["productfamily"];
            var basicInfo = HuaweiWebAPI.HuaweiWebApi.BasicInformationFrom();

            // "productfamily"
        }
    }

}
