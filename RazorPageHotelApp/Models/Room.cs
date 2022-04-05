using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Models
{
    public class Room
    {
        [Required(ErrorMessage = "Room Number Required")]
        public int RoomNr { get; set; }

        [Required(ErrorMessage = "Hotel Number Required")]
        public int HotelNr { get; set; }

        [Required(ErrorMessage = "Type Required")]
        public char Types { get; set; }

        [Required(ErrorMessage = "Price Required")]
        public double Price { get; set; }

        public Room(int roomNr, int hotelNr, char types, double price)
        {
            this.RoomNr = roomNr;
            this.HotelNr = hotelNr;
            this.Types = types;
            this.Price = price;
        }

        public Room()
        {
            
        }

        public override string ToString()
        {
            return $"VærelsesNr: {RoomNr} HotelNr: {HotelNr} Værelsestype: {Types} Pris: {Price}";
        }
    }
}