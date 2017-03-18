using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioNetworkModel;

namespace HandoverAlgorithmBase
{
    class NovelHandoverAlgorithm : HandoverAlgorithmBase
    {
        #region Properties

        public string HandoverAlgorithmName { get; set; }
        public string HandoverAlgorithmDescription { get; set; }
        protected RadioNetworkModelBase RadioNetworkModel{ get; set; }

        #endregion

    }
}
