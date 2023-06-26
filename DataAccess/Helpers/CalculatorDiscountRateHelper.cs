using System;

namespace DataAccess.Helpers
{
    public class CalculatorDiscountRateHelper
    {
        public static double Calculate(double? listPrice, double? salesPrice)
        {
            var startCalculate = 100 * salesPrice / listPrice;
            var discountRate = 100 - startCalculate;
            if (discountRate > 0)
            {
                return (double)discountRate;
            }
            return 0;
        }
    }
}
