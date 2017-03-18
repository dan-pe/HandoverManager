using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioNetworkModel
{
    public abstract class RadioNetworkModelBase
    {
        #region Properties

        protected RadioNetworkType RadioNetworkType { get; set; }
        protected RadioNetworkParameters RadioNetworkParameters { get; set; }

        #endregion

    }

    #region Enums
    public enum RadioNetworkType
    {
        LTE,
        WiFi,
        LTE_Advanced,
        UMTS,
        GPRS
    }
    #endregion

}
