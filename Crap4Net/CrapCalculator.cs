using System;

namespace Crap4Net
{
    public class CrapCalculator
    {
        public static double CalculateCrap(int ccValue, int coverageValue)
        {
            if (coverageValue<0 ||  coverageValue>100)
            {
                throw new ArgumentException("Illegal Coverage Value: "+coverageValue+ "Value must be between 0-100");
            }
            if (ccValue<0)
            {
            	throw new ArgumentException("CCValue cant be negative");
            }

            var result = Math.Pow(ccValue, 2) * Math.Pow(1 - coverageValue / 100d, 3) + ccValue;
            return Math.Round(result, 2);
        }

    }

}
