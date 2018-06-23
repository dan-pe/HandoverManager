namespace NetworkManager
{
    public interface INetworkInterface
    {
        string GetInterfaceName();
        string GetInterfaceType();
        string GetInterfaceSpeed();
    }
}