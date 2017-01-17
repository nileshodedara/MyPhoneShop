using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P15216326
{
    [Serializable()]
    public class clsAndroidPhone:clsMobilePhone
    {
       // [Serializable()]
        public clsAndroidPhone(string make, string model, string operatingSystem, DateTime datePurchase, decimal originalPrice, Condition condition)
            : base(make, model, operatingSystem, datePurchase, originalPrice, condition)
        {
            //this.payload = payload;
        }

        // this calculation is overridden in both the inheriting classes
        public override decimal CalculateApproximateValue()
        {
            decimal value = 0;
            if (originalPrice > 0)
            {
                // we modify the van's value based on its condition
                if (currentCondition == Condition.mint)
                {
                    value = originalPrice * 0.8m;        // 70% of original value
                }
                else if (currentCondition == Condition.good)
                {
                    value = originalPrice * 0.7m;        // 60% of original value
                }
                else if (currentCondition == Condition.fair)
                {
                    value = originalPrice * 0.5m;        // 50% of original value
                }
                else if (currentCondition == Condition.poor)
                {
                    value = originalPrice * 0.4m;        // 40% of original value
                }

                // we also take into account the vehicles age
                int age = CalculateApproximateAgeInYears();

                // the loop below could be re-written as
                // decimal alternativeValue = value * (decimal)Math.Pow(0.95, age);    // we loose 10% of value for each year old... i.e. we keep 90% (0.9)

                // vans lose their value more slowly than cars. We lose 5% per year
                // we lose another 5% of the value every year - so we keep 95% or 0.95
                for (int i = 0; i < age; i++)
                {
                    value = value * 0.95m;
                }

                // Debug.Assert(value == alternativeValue); - was a check on alternative calculation
                value = Decimal.Round(value, 0);    // round to the nearest pound.          

                // the car lot rounds this down to the nearest £100 
                value = value - (value % 100);

                // and then adds £99
                value = value + 99;
            }

            return value;
        }

        //  public override string Description()
        // {
        // we add to the base class description with the extra information on payload.
        // string description = base.Description() + Environment.NewLine + string.Format("Payload: {0}kg", payload);
        // return description;
        //}
    }
}
