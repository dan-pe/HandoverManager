using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    [XmlRoot("response")]
    public class BasicInformation
    {
        [XmlElement("autoupdate_guide_status")]
        public int AutoupdateGuideStatus;

        [XmlElement("classify")]
        public string Classify;

        [XmlElement("devicename")]
        public string DeviceName;

        [XmlElement("multimode")]
        public int Multimode;

        [XmlElement("productfamily")]
        public string ProductFamily;

        [XmlElement("restore_default_status")]
        public int RestoreDefaultStatus;

        [XmlElement("sim_save_pin_enable")]
        public int SimSavePinEnable;

        [XmlElement("SoftwareVersion")]
        public string SoftwareVersion;

        [XmlElement("WebUIVersion")]
        public string WebUIVersion;
    }
}