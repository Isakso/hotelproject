using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{
   class Reservation
    { 
        // declare variables
        private Contact contact;
        private DateTime checkInDate;
        private DateTime checkOutDate;
        private UInt16 noOfChildren;
        private UInt16 noOfAdults;
        private Room room;
        private Card card = new Card();
        /// <summary>
        /// Default constructor
        /// </summary>
        public Reservation()
        {

        }
        public Reservation(Contact contact) : this(contact, DateTime.Now, DateTime.Now, 1, 0, new Room())// load constructor
        {
        }
        public Reservation(Contact contact, 
            DateTime checkInDate, DateTime checkOutDate, 
            UInt16 noOfAdults, UInt16 noOfChildren,
            Room room)
        {
            this.contact = contact;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
            this.noOfAdults = noOfAdults;
            this.noOfChildren = noOfChildren;
            this.room = room;
        }
        
        public Contact ContactData// get and set contact data
        {
            get
            {
                return contact;
            }
            set
            {
                contact = value;
            }
        }
        /// <summary>
        /// get and set number of children
        /// </summary>
        public UInt16 NumberOfChildren
        {
            get { return noOfChildren; }
            set { noOfChildren = value; }
        }
        /// <summary>
        /// Number of adults
        /// </summary>
        public UInt16 NumberOfAdults
        {
            get { return noOfAdults; }
            set { noOfAdults = value; }
        }
        /// <summary>
        /// Get and set Number of Nights
        /// </summary>
        public int NumberOfNights
        {
            get {
                TimeSpan numNights = checkOutDate.Subtract(checkInDate);
                return numNights.Days; 
            }
        }

        public string[] GetColumnStrings()
        {
            // There are 6 columns
            
            string[] itemRow = new string[6]{
                    contact.FirstName + ", " + contact.LastName,
                    NumberOfNights.ToString(),
                    room.RmType.ToString(),
                    noOfAdults.ToString(),
                    noOfChildren.ToString(),
                    room.CostPerNight.ToString()
                    };

            return itemRow;
        }
        /// <summary>
        /// string to display in the summary
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            // FullName, CardNo, privateEmail, PrivatePhone, City/Country, Chkin - CheckOut, 2 Adults, 1 child, 3 nights, RoomType, RoomCost
            string cardStr = card.CardNo.ToString();
            string roomType = room.RmType.ToString();
            string reservationString = contact.FirstName + ", " + contact.LastName + ", " +
                "CardNo: ***" + cardStr.Substring(cardStr.Length - 4) +
                ", Phone: " + contact.HomePhone + ", Email:" + contact.HomeEmail +
                ", City/Country: " + contact.Address.City + "/" + contact.Address.Country +
                ", From:" + CheckInDate.ToString("dd/MM") + " To:" + CheckOutDate.ToString("dd/MM") +
                ", Adults: " + NumberOfAdults.ToString() + ", Children: " + NumberOfChildren.ToString() +
                ", Nights: " + NumberOfNights.ToString() + 
                ", " + roomType + ", at SEK" + Room.CostPerNight.ToString();

            return reservationString;
        }  
        /// <summary>
        /// gets and returns card
        /// </summary>
        public Card Card
        {
            get { return card; }
            set { card = value; }
        }
        /// <summary>
        /// gets and sets checkindate
        /// </summary>
        public DateTime CheckInDate
        {
            get { return checkInDate; }
            set { checkInDate = value; }
        }
        /// <summary>
        /// gets and sets checkout date 
        /// </summary>
        public DateTime CheckOutDate
        {
            get { return checkOutDate; }
            set { checkOutDate = value; }
        }
        /// <summary>
        ///  gets and setsroom
        /// </summary>
        public Room Room
        {
            get { return room; }
            set { room = value; }
        }
    } 


}
