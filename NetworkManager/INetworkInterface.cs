namespace NetworkManager
{
    public interface INetworkInterface
    {
        string GetInterfaceName();

        string GetInterfaceSpeed();

        string GetInterfaceType();
    }
}