using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{  /// <summary>
/// handles validation
/// </summary>
    public class Utilities
    {  
        string fName; string lName;
        string city; int countryIdx; DateTime checkIn; DateTime checkOut; 
        int crdTypIndex;  string cardNo;  string validity;

        public Utilities(string fName, string lName,
            string city, int countryIdx, DateTime checkIn, DateTime checkOut, int crdTypIndex, string cardNo, string validity)
            {
            this.fName = fName;  
            this.lName = lName;
            this.city = city;  
            this.countryIdx = countryIdx;  
            this.checkIn = checkIn; 
            this.checkOut = checkOut;
            this.cardNo = cardNo;
            this.validity = validity;
            this.crdTypIndex = crdTypIndex;
        }
        /// <summary>
        /// validate inputs
        /// </summary>
        /// <param name="errorString"></param>
        /// <returns></returns>
              
        public bool ValidateInputs(out string errorString)
        {
            bool inputOK = true;
            errorString = string.Empty;

            // check name
            if (string.IsNullOrEmpty(fName))
            {
                inputOK = false;
                errorString += "\n*First name can not be empty.";
            }
            if (string.IsNullOrEmpty(lName))
            {
                inputOK = false;
                errorString += "\n*Last name cannot be empty.";
            }
            //  check address
            if (string.IsNullOrEmpty(city))
            {
                inputOK = false;
                errorString += "\n*City cannot be empty.";
            }
            if (countryIdx < 1)
            {
                inputOK = false;
                errorString += "\n*Country cannot be empty.";
            }
            // Check dates
            if (checkIn < DateTime.Today)
            {
                inputOK = false;
                errorString += "\n*Check in date cannot be earlier than today.";
            }
            if (checkOut < checkIn)
            {
                inputOK = false;
                errorString += "\n*Check out date cannot be earlier than check in.";
            }

            if (!string.IsNullOrEmpty(cardNo))
            {
                /*
                 * Visa 4 (16 )
                 * electron 4026|417500|4405|4508|4844|4913|4917
                 * Mastercard 51-55 (16 digits)  2221-2720                 * 
                 */
                if (cardNo.Trim().Length != 16 && cardNo.Trim().Length != 19)
                {                    
                    inputOK = false;
                    errorString += "\n*Card number length should be 16 or 19.";
                }
                else
                {
                    if ((CardType)crdTypIndex == CardType.Visa)
                    {
                        if (cardNo.StartsWith("4") && cardNo.Length == 16)
                        {
                            // valid visa card
                        }
                        else
                        {
                            inputOK = false;
                            errorString += "\n*Visa Card not valid.";
                        }
                    }
                    else if ((CardType)crdTypIndex == CardType.VisaElectron)
                    {
                        if ((cardNo.StartsWith("4026") || cardNo.StartsWith("417500") || cardNo.StartsWith("4405") ||
                        cardNo.StartsWith("4508") || cardNo.StartsWith("4844") || cardNo.StartsWith("4913") ||
                        cardNo.StartsWith("4917")) && cardNo.Length == 16)
                        {
                            // valid visa electron card
                        }
                        else
                        {
                            inputOK = false;
                            errorString += "\n*VisaElectron Card not valid.";
                        }
                    }
                    else if ((CardType)crdTypIndex == CardType.MasterCard)
                    {
                        int prefix1 = Convert.ToInt32(cardNo.Substring(0, 2));
                        int prefix2 = Convert.ToInt32(cardNo.Substring(0, 4));
                        if ((prefix1 >= 51 && prefix1 <= 55 && cardNo.Length == 16) ||
                            (prefix2 >= 2221 && prefix2 <= 2720 && cardNo.Length == 19))
                        {
                            // valid Master card
                        }
                        else
                        {
                            inputOK = false;
                            errorString += "\n*MasterCard not valid.";
                        }
                    }
                }
            }
            else
            {
                inputOK = false;
                errorString += "\n*Please enter card number.";
            }

            return inputOK;
        }
    }
}
