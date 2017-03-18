using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioNetwork;

namespace HandoverAlgorithmBase
{
    public class NovelNetworkAlgorithm : HandoverAlgorithmBase
    {
        #region Constructors
        public NovelNetworkAlgorithm(List<RadioNetworkModel> radioNetworksList )
        {
            RadioNetworksList = radioNetworksList;
        }

        #endregion

    }
}
