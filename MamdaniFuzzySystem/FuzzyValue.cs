namespace MamdaniFuzzySystem
{
    public class FuzzyValue
    {
        #region Properties

        public string FuzzyValueName { get; private set; }

        public float CrispValueMin { get; private set; }

        public float CrispValueMax { get; private set; }

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
