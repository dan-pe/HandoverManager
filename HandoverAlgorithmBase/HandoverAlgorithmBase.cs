using RadioNetworks;
using System.Collections.Generic;

namespace HandoverAlgorithmBase
{
    public abstract class HandoverAlgorithmBase
    {
        /// <summary>
        /// The Handover algorithm base
        /// </summary>
        /// <param name="radioNetworksList">
        /// The radio networks list.
        /// </param>
        protected HandoverAlgorithmBase(List<RadioNetworkModel> radioNetworksList)
        {
            RadioNetworksList = radioNetworksList;
        }

        /// <summary>
        /// The radio networks list.
        /// </summary>
        protected List<RadioNetworkModel> RadioNetworksList { get; set; }

        public virtual void RunSelection()
        {
        }
    }
}