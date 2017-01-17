using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P15216326
{
    [Serializable()]
    public class clsApplePhone : clsMobilePhone
    {
       // [Serializable()]
        public clsApplePhone(string make, string model, string operatingSystem, DateTime datePurchase, decimal originalPrice, Condition condition)
            : base(make, model, operatingSystem, datePurchase, originalPrice, condition)
        {
            // nothing else to do in the constructor
            // as the car class doesn't add any new attributes
        }


        // this calculation is overridden in both the inheriting classes
        public override decimal CalculateApproximateValue()
        {
            decimal value = 0;
            if (originalPrice > 0)
            {
                // we modify the phones value based on its condition
                if (currentCondition == Condition.mint)
                {
                    value = originalPrice * 0.9m;        // 60% of original value
                }
                else if (currentCondition == Condition.good)
                {
                    value = originalPrice * 0.8m;        // 50% of original value
                }
                else if (currentCondition == Condition.fair)
                {
                    value = originalPrice * 0.7m;        // 40% of original value
                }
                else if (currentCondition == Condition.poor)
                {
                    value = originalPrice * 0.5m;        // 30% of original value
                }

                // we also take into account the cars age
                int age = CalculateApproximateAgeInYears();

                // the loop below could be re-written as
                // decimal alternativeValue = value * (decimal)Math.Pow(0.9, age);    // we loose 10% of value for each year old... i.e. we keep 90% (0.9)
                // we lose another 10% of the value every year - so we keep 90% or 0.9

                for (int i = 0; i < age; i++)
                {
                    value = value * 0.9m;
                }
                // this loop could be re-written as
                // value = value * (decimal)Math.Pow(0.9, age);    // we loose 10% of value for each year old... i.e. we keep 90% (0.9)

                value = Decimal.Round(value, 0);    // round to the nearest pound.          

                // the car lot rounds this down to the nearest £100 
                value = value - (value % 100);

                // and then adds £99
                value = value + 99;
            }
            return value;
        }

    }
}
