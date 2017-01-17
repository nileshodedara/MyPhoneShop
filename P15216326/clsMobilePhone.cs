using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P15216326
{
    [Serializable()]
    public abstract class clsMobilePhone : IComparable
    {
        public enum Condition
        {
            poor,
            fair,
            good,
            mint
        };

        protected string operatingSystem;
        protected string make;
        protected string model;
        protected DateTime datePurchase;
        protected decimal originalPrice;
        protected Condition currentCondition;

        // get set

        public Condition CurrentCondition
        {
            get { return currentCondition; }
            set { currentCondition = value; }
        }

        public string Make
        {
            get { return make; }

            set { make = value; }
        }

        public string Model
        {
            get { return model; }

            set { model = value; }
        }

        public string OperatingSystem
        {
            get { return operatingSystem; }

            set { operatingSystem = value; }
        }

        public Decimal OriginalPrice
        {
            get { return originalPrice; }

            set { originalPrice = value; }
        }

        public DateTime DatePurchase
        {
            get { return datePurchase; }

            set { datePurchase = value; }
        }
       
        // constructor
        public clsMobilePhone(string make, string model, string operatingSystem, DateTime datePurchase, decimal originalPrice, Condition condition)
        {
            this.make = make;
            this.model = model;
            this.operatingSystem = operatingSystem;
            this.datePurchase = datePurchase;
            this.originalPrice = originalPrice;
            this.currentCondition = condition;

        }
        
        public int CalculateApproximateAgeInYears()
        {
            DateTime now = DateTime.Now;
            TimeSpan ageAsTimeSpan = now.Subtract(datePurchase);
            int ageInYears = ageAsTimeSpan.Days / 365;  // doesn't take into account leap years - just approximate
            return ageInYears;
        }

        // this method has to be implemented in the derived class
        //public abstract decimal CalculateApproximateValue();
        public abstract decimal CalculateApproximateValue();

        public virtual string Description()
        {
            // get a string describing the current vehicles condition from the names in the Condition enumeration
            string conditionName = Enum.GetName(typeof(Condition), currentCondition); // we can get the enumeration name here eg. good or fair as text as opposed to its value

            // build a string describing the current vehicle
            string description = string.Format("Make: {0}{1}Model: {2}{3}operating System: {4}{5}Condition: {6}{7}Current Value: {8:c}",
                make,
                Environment.NewLine,
                model,
                Environment.NewLine,
                operatingSystem,
                Environment.NewLine,
                conditionName,
                Environment.NewLine,
                CalculateApproximateValue());

            return description;
        }
        
        // Implement IComparable CompareTo method - provide default sort order.
        // this will be used if we need to sort the vehicles
        int IComparable.CompareTo(object obj)
        {
            // iComparable returns   +1, 0 or -1
            clsMobilePhone otherMobilePhone = (clsMobilePhone)obj;
            decimal differenceInPrice = this.CalculateApproximateValue() - otherMobilePhone.CalculateApproximateValue();
            // we want to return +1, 0 or -1
            return Math.Sign(differenceInPrice);
        }
    }
}
