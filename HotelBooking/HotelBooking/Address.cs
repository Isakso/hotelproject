using System.Windows.Forms;

namespace HotelBooking
{
  
        class Address
        {  /// <summary>
        /// declare address variables
        /// </summary>
            private Countries country;
            private string city;
            private string zipcode;
            private string addressLine1;
            private string addressLine2;
            private string street1;
            private string street2;
           
        /// <summary>
        /// default constructor calls one with more parameters
        /// </summary>

        public Address() : this("", "", "", Countries.Sweden)
        {
        }
        public Address(string city, Countries country) : this(city, "", "", country)
            {
            }
            public Address(string city, string zipcode, string street1, Countries country)
            {
                this.city = city;
                this.zipcode = zipcode;
                this.street1 = street1;
                this.country = country;
            }
        /// <summary>
        /// load constructor with all variables
        /// </summary>
        /// <param name="city"></param>
        /// <param name="country"></param>
        /// <param name="street1"></param>
        /// <param name="street2"></param>
        /// <param name="zipcode"></param>
        /// <param name="addressLine1"></param>
        /// <param name="addressLine2"></param>
        public Address(
            string city, Countries country, 
            string street1, string street2, 
            string zipcode, string addressLine1, 
            string addressLine2)
        {
            this.city = city;
            this.country = country;
            this.street1 = street1;
            this.street2 = street2;
            this.zipcode = zipcode;
            this.addressLine1 = addressLine1;
            this.addressLine2 = addressLine2;
        }

        /// <summary>
        /// get and set for country
        /// </summary>
        public Countries Country
            {
                get
                {
                    return country;
                }
            }
             /// <summary>
             /// gets and sets city
             /// </summary>
            public string City
            {
            get { return city; }
            set { city = value; }
               
            }
        /// <summary>
        ///  get and set the zip code
        /// </summary>

        public string ZipCode
        {
            get{ return zipcode; }
            set{zipcode = value;}
        }

        /// <summary>
        /// gets and sets street
        /// </summary>
            public string Street1
            {
                get { return street1; }
                set { street1 = value; }
            }
        /// <summary>
        /// gets and sets street 2
        /// </summary>
        public string Street2
        {
            get { return street2; }
            set { street2 = value; }
        }
        /// <summary>
        /// gets and sets addressline1
        /// </summary>
        public string AddressLine1
        {
            get { return addressLine1; }
            set { addressLine1 = value; }
        }
        /// <summary>
        /// gets and sets addressline2
        /// </summary>

         public string AddressLine2
        {
            get { return addressLine2; }

            set { addressLine2 = value; }
          
        }
           /// <summary>
           /// method validates input of city
           /// </summary>
           /// <returns></returns>
            public bool CheckAddressData()
            {
                if (!string.IsNullOrEmpty(city))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            // override method toString()
            public override string ToString()
            {
                string strgout = country.ToString();
                return strgout;
            }
        }
  }


