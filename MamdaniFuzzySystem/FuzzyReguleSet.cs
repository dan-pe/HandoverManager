using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamdaniFuzzySystem
{
    public class FuzzyReguleSet<T>
    {
        #region Properties

        

        #endregion
        public Dictionary<T,float> RegulesSet { get; set; }

        #region Public Methods

        public FuzzyReguleSet()
        {
            RegulesSet = new Dictionary<T, float>();
        }

        #endregion

    }
}
