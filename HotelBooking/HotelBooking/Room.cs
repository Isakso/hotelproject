using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking
{   /// <summary>
///  enum RoomType
/// </summary>
    public enum RoomType
    {
        Standard, DeluxeSuite, Suite
    }

    class Room
    {/// <summary>
    /// declare room variables
    /// </summary>
        private RoomType type;
        private int costPerNight;

        public Room(): this(RoomType.Standard) // default
        {        
        }
        public Room(RoomType type)///set room cost
        {
            this.type = type;
            if (type == RoomType.Standard)
            {
                costPerNight = 800;
            }
            else if (type == RoomType.DeluxeSuite)
            {
                costPerNight = 1200;
            }
            else
            {
                costPerNight = 2000;
            }
        }
        /// <summary>
        /// get and set roomtype
        /// </summary>
        public RoomType RmType
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        ///  get and set cost per night
        /// </summary>
        public int CostPerNight
        {
            get { return costPerNight; }
        }
    }
}
