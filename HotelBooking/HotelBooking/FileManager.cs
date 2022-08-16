//using Newtonsoft.Json;

using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace HotelBooking
{
    class FileManager
    {

        private string fileName;
        private ReservationManager reservationMgr;
        /// <summary>
        /// constructor with file name calls constructor with more parameters 
        /// </summary>
        /// <param name="fileName"></param>
        public FileManager(string fileName) : this(fileName, new ReservationManager())
        {
        }

        public FileManager(string fileName, ReservationManager reservationMgr)
        {
            this.fileName = fileName;
            this.reservationMgr = reservationMgr;
        }
        
        /// <summary>
        /// write to file using json
        public ReservationManager ReadFromFile()
        {
            ReservationManager reservationMgr = new ReservationManager();
            
            using (StreamReader file = File.OpenText(@fileName))
            {
                string jsonString = file.ReadToEnd();
                reservationMgr = JsonSerializer.Deserialize<ReservationManager>(jsonString);
            }

            return reservationMgr;
        }

        /// <summary>
        /// </summary>save to file using jason serializer
        public void SaveToFile()
        {
            if (!fileName.EndsWith(".rsvndb"))
            {
                fileName += ".rsvndb"; // adds the 'reservation database' file extension
            }                       

            using (StreamWriter file = File.CreateText(@fileName))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                string jsonString = JsonSerializer.Serialize(reservationMgr, options);
                file.WriteLine(jsonString);
            }
        }
    }
}

    

