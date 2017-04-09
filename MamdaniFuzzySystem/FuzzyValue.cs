using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MamdaniFuzzySystem
{
    public class FuzzyValue
    {
        #region Properties

        public string FuzzyValueName { get; set; }

        public float CrispValueMin { get; set; }

        public float CrispValueMax { get; set; }

        #endregion

        #region Constructors
        public FuzzyValue(string fuzzyValueName, float crispValueMin, float crispValueMax)
        {
            FuzzyValueName = fuzzyValueName;
            CrispValueMin = crispValueMin;
            CrispValueMax = crispValueMax;
        }

        #endregion
    }


}
