using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{  
    class Contact
    {
        // declare variables
        // a contact has the following
        private string firstName;
        private string lastName;
        private Address address;
        private string homeEmail;
        private string workEmail;
        private string homePhone;
        private string workPhone;
       

    
        public Contact() : this("_", "_", "_", "_", new Address(), "_", "_")// generate constructor
        {
        }
   /// <summary>
   /// Load constructor
   /// </summary>
   /// <param name="homeEmail"></param>
   /// <param name="address"></param>
   /// <param name="homePhone"></param>
        public Contact(string homeEmail, Address address, string homePhone) : this("_", "_", homeEmail, "_", address, homePhone, "_")
        {
        }
      
        public Contact(
            string firstName, string lastName, 
            string homeEmail, string workEmail, 
            Address address, 
            string homePhone, string workPhone)//load constructor with all parameters
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.homeEmail = homeEmail;
            this.workEmail = workEmail;
            this.address = address;
            this.homePhone = homePhone;
            this.workPhone = workPhone;
        }
        public string FirstName// gets and sets first name
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName // gets and sets last name
        {
            get { return lastName; }
            set { lastName = value; }
        }

       public Address Address // property gets and returns address
        {
            get { return address; }
            set { address = value; }
        }     
        //gets and sets home phone
        public string HomePhone
        {
            get { return homePhone; }
            set { homePhone = value; }
        }
        //gets and sets workphone
        public string WorkPhone
        {
            get { return workPhone; }
            set { workPhone = value; }
        }
        /// <summary>
        /// gets and sets homeemail
        /// </summary>
        public string HomeEmail
        {
            get { return homeEmail; }
            set { homeEmail = value; }
        }
        /// <summary>
        /// property gets and sets work email
        /// </summary>
        public string WorkEmail
        {
            get { return workEmail; }
            set { workEmail = value; }
        }
    }       
}


