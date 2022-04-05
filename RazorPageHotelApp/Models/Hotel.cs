using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageHotelApp.Models
{
    public class Hotel
    {
        [Required(ErrorMessage = "Hotel Number Required")]
        public int HotelNr { get; set; }

        [Required(ErrorMessage = "Name Required")]
        public String Navn { get; set; }

        [Required(ErrorMessage = "Address Required")]
        public String Adresse { get; set; }

        public Hotel()
        {

        }

        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
        }

        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }
    }
}