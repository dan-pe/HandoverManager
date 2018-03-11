namespace NetworkManager
{
    public abstract class NetworkInterface
    {
        protected InterfaceType InterfaceType;

        protected string InterfaceName;

        protected int InterfaceSpeed;
    }

    public enum InterfaceType
    {
        Wifi,
        RDI
    }
}