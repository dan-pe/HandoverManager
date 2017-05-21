#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion    

namespace MamdaniFuzzySystem
{
    public class FuzzyReguleSet
    {
        #region Properties

        private List<FuzzyValue> FuzzyRegulesList { get; set; }

        #endregion

        #region Constructors

        public FuzzyReguleSet(List<FuzzyValue> fuzzyValues )
        {
            FuzzyRegulesList = fuzzyValues;
        }

        #endregion

        #region Public Methods

        public FuzzyValue GetFuzzyValueByName(string name)
        {
            var fuzzyValue = FuzzyRegulesList.FirstOrDefault(
                value => value.FuzzyValueName.Equals(name));

            return fuzzyValue;
        }

        #endregion

    }
}
