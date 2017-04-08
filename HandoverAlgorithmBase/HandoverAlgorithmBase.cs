#region Usings

using System;
using System.Collections.Generic;
using RadioNetwork;

#endregion


namespace HandoverAlgorithmBase
{
    public abstract class HandoverAlgorithmBase
    {
        #region Properties

        protected List<RadioNetwork.RadioNetworkModel> RadioNetworksList { get; set; }

        #endregion

        #region Constructors

        protected HandoverAlgorithmBase(List<RadioNetwork.RadioNetworkModel> radioNetworksList)
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
