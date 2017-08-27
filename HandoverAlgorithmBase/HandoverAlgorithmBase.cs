#region Usings

using System;
using System.Collections.Generic;
using RadioNetworks;

#endregion

namespace HandoverAlgorithmBase
{
    public abstract class HandoverAlgorithmBase
    {
        #region Properties

        /// <summary>
        /// The radio networks list.
        /// </summary>
        protected List<RadioNetworkModel> RadioNetworksList { get; set; }

        #endregion

        #region Constructors

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

        #endregion

        #region Methods

        public virtual void RunSelection()
        {
        }

        #endregion
    }
}
