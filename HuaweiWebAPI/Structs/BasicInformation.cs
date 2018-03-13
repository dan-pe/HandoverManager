using System.Xml.Serialization;

namespace HuaweiWebAPI.Structs
{
    //[XmlRoot(ElementName = "response", DataType = "string")]
    [XmlRoot("response")]
    public class BasicInformation
    {
        [XmlElement("productfamily")]
        public string Productfamily;

        [XmlElement("classify")]
        public string Classify;

        [XmlElement("multimode")]
        public string Multimode;

        [XmlElement("restore_default_status")]
        public string RestoreDefaultStatus;

        [XmlElement("autoupdate_guide_status")]
        public string AutoupdateGuideStatus;

        [XmlElement("sim_save_pin_enable")]
        public string SimSavePinEnable;

        [XmlElement("devicename")]
        public string Devicename;

        [XmlElement("SoftwareVersion")]
        public string SoftwareVersion;

        [XmlElement("WebUIVersion")]
        public string WebUIVersion;
    }
}

