using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{   /// <summary>
///  class to manage the reservations
/// </summary>
     class ReservationManager
    {
        List<Reservation> reservationLst;
        /// <summary>
        ///  default contructor 
        ///  takes in a new object of the reservation list
        /// </summary>
        public ReservationManager()
        {
            reservationLst = new List<Reservation>();
        }
        /// <summary>
        /// returns a count of the list
        /// </summary>
        /// <returns></returns>
        public int ReservationCount()
        {
            return reservationLst.Count; 

        }
         /// <summary>
         /// method gets reservation at a particular index in the 
         /// reservation list
         /// </summary>
         /// <param name="index"></param>
         /// <returns></returns>
        public Reservation GetReservation( int index)
        {
            return reservationLst[index];
        }
        /// <summary>
        /// gets ReservationList
        /// Property useful in reading from file
        /// </summary>
        public List<Reservation> ReservationList
        {
            get { return reservationLst; }
            set { reservationLst = value; }
        }

        /// <summary>
        /// Adds a reservation to a reservation list
        /// </summary>
        /// <param name="reservation"></param>
        public void AddReservation(Reservation reservation)
        {
            reservationLst.Add(reservation);
        }
       /// <summary>
       /// Each reservation has new information
       ///changes reservation at a particular index and updates infor
       /// </summary>
       /// <param name="reservationid"></param>
       /// <param name="newReservationInfor"></param>
        public void UpdateReservationAt(int index, Reservation reservation)
        {    
             if (reservation != null)
             {
                 reservationLst[index] = reservation;
             }
        }
        /// <summary>
        ///  deletes reservation at a particular index
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        /// 
        public bool DeleteReservation(Reservation reservation)
        {
            bool deletereservation = false;
            for( int i = 0; i< reservationLst.Count; i++)
            {
                if(reservation == reservationLst[i])
                {
                    reservationLst.RemoveAt(i);
                       deletereservation = true;
                  break;
               }
            }
            return deletereservation;
        }              
    }
}
