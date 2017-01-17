using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P15216326
{
    [Serializable()]
    public class clsPhoneLot
    {
        // the CarLot Class contains a list of vehicles (Car and Vans)
        // using polymorphism we inherit from the abstract base class vehicle
        // and provide a method that differs for the two type to calculate
        // their approximate value based on their age and condition

        public List<clsMobilePhone> PhoneStock;

        private int phoneCurrentlyDisplayed = 0;

        public clsPhoneLot()
        {
            PhoneStock = new List<clsMobilePhone>();
        }

        public int PhoneCurrentlyDisplayed
        {
            get { return phoneCurrentlyDisplayed; }
        }

        public int NumberOfPhone
        {
            get { return PhoneStock.Count; }
        }


        public string DescribeCurrentPhone(int currentPhone)
        {
            string description;

            // if we have any vehicles we ask the displayed vehicle for its description
            if (PhoneStock.Count > 0)
            {
                description = PhoneStock[phoneCurrentlyDisplayed].Description();
            }
            else
            {
                description = "No phone in stock";
            }
            return description;
        }

        // we are not using these currently...
        public void AddPhone(clsMobilePhone phone)
        {
            PhoneStock.Add(phone);
            phoneCurrentlyDisplayed = PhoneStock.Count - 1;
        }

        // we are not using these currently...
        public void EditPhone(clsMobilePhone phone)
        {
            PhoneStock[phoneCurrentlyDisplayed] = phone;
        }

        public void RemovePhoneAt(int index)
        {
            if (index < PhoneStock.Count)
            {
                PhoneStock.RemoveAt(index);
                // make sure vehicleCurrentlyDisplayed is either zero or pointing at an existing vehicle
                LegalisePhoneCurentlyDisplayed();
            }
        }

        public clsMobilePhone GetPhoneCurrentlyDisplayed()
        {
            return PhoneStock[phoneCurrentlyDisplayed];
        }

        // we ensure that vehicleCurrentlyDisplayed indexes a vehicle that exists
        // (if there are any)
        private void LegalisePhoneCurentlyDisplayed()
        {
            if (phoneCurrentlyDisplayed > (PhoneStock.Count - 1))
            {
                phoneCurrentlyDisplayed = PhoneStock.Count - 1;     // not this will be -1 if stock is zero

                if (phoneCurrentlyDisplayed < 0)
                {
                    phoneCurrentlyDisplayed = 0;  // make sure its legal or zero....
                }
            }
        }

        public bool IsPreviousPhone()
        {
            if (phoneCurrentlyDisplayed > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            // we could write this in one line
            //return (vehicleCurrentlyDisplayed > 0);

        }

        public bool IsNextPhone()
        {
            if (phoneCurrentlyDisplayed < PhoneStock.Count - 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void StepToPreviousPhone()
        {
            if (IsPreviousPhone())
            {
                phoneCurrentlyDisplayed--;
            }
        }

        public void StepToNextPhone()
        {
            if (IsNextPhone())
            {
                phoneCurrentlyDisplayed++;
            }
        }

        public void Sort()
        {
            PhoneStock.Sort();
        }
    }
}
