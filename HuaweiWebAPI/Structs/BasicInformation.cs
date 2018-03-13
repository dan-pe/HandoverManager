using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    [XmlRoot("response")]
    public class BasicInformation
    {
        [XmlElement("productfamily")]
        public string ProductFamily;

        [XmlElement("classify")]
        public string Classify;

        [XmlElement("multimode")]
        public int Multimode;

        [XmlElement("restore_default_status")]
        public int RestoreDefaultStatus;

        [XmlElement("autoupdate_guide_status")]
        public int AutoupdateGuideStatus;

        [XmlElement("sim_save_pin_enable")]
        public int SimSavePinEnable;

        [XmlElement("devicename")]
        public string DeviceName;

        [XmlElement("SoftwareVersion")]
        public string SoftwareVersion;

        [XmlElement("WebUIVersion")]
        public string WebUIVersion;
    }
}

