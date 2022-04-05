using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Models;
using HotelDBConsole21.Services;
using Console = System.Console;

namespace HotelDBConsole21
{
    public static class MainMenu
    {
        //Lav selv flere menupunkter til at vælge funktioner for Rooms
        public static void showOptions()
        {
            Console.Clear();
            Console.WriteLine("Vælg et menupunkt");
            Console.WriteLine("1) List hoteller");
            Console.WriteLine("1a) List hoteller async");
            Console.WriteLine("2) Opret nyt Hotel");
            Console.WriteLine("3) Fjern Hotel");
            Console.WriteLine("4) Søg efter hotel udfra hotelnr");
            Console.WriteLine("4a) Søg efter hotel udfra hotelnr async");
            Console.WriteLine("5) Opdater et hotel");
            Console.WriteLine("6) List alle værelser");
            Console.WriteLine("7) List alle værelser til et bestemt hotel");
            Console.WriteLine("8) Vis alle værelser");
            Console.WriteLine("9) Opret værelse");
            Console.WriteLine("Q) Afslut");
        }

        public static bool Menu()
        {
            showOptions();
            switch (Console.ReadLine())
            {
                case "1":
                    ShowHotels();
                    return true;
                case "1a":
                    ShowHotelsAsync();
                    DoSomething();
                    return true;
                case "2":
                    CreateHotel();
                    return true;
                
                case "3":
                    DeleteHotel();
                    return true;
                case "4":
                    GetHotelFromId();
                    return true;
                case "4a":
                    GetHotelFromIdAsync();
                    return true;
                case "5":
                    UpdateHotel();
                    return true;
                case "6":
                    ListRooms();
                    return true;
                case "7":
                    ListHotRooms();
                    return true;
                case "8":
                    GetAllRooms();
                    return true;
                case "9":
                    CreateRoom();
                    return true;
                case "Q": return false;
                case "q": return false;
                default: return true;
            }

        }

        private static void CreateRoom()
        {
            Console.Clear();
            Console.WriteLine("indtast værelses nummer");
            int roomNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("indtast hotel nummer");
            int hotelNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("intast værelses type");
            string types = Convert.ToString(Console.ReadLine());
            Console.WriteLine("indtast pris");
            double price = Convert.ToDouble(Console.ReadLine());
        }

        private async static void GetHotelFromIdAsync()
        {
            Console.Clear();
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            HotelServiceAsync hs = new HotelServiceAsync();
            Hotel hotel = await hs.GetHotelFromIdAsync(hotelNr);
            Console.WriteLine(hotel);
        }

        private async static void ShowHotelsAsync()
        {
            Console.Clear();
            HotelServiceAsync hs = new HotelServiceAsync();
            List<Hotel> hotels = await hs.GetAllHotelAsync();
            foreach (Hotel hotel in hotels)
            {
                Console.WriteLine($"HotelNr{hotel.HotelNr} name {hotel.Navn } address {hotel.Adresse}");
            }
        }

        private static void GetAllRooms()
        {
           
        }

        private static void ListHotRooms()
        {
            Console.Clear();
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            HotelService hs = new HotelService();
            List<Room> rooms = hs.RoomInHot(hotelNr);
            foreach (var room in rooms)
            {
                Console.WriteLine(room);
            }
        }


        private static void ListRooms()
        {
            Console.Clear();
            HotelService hs = new HotelService();
            List<Room> rooms = hs.GetAllRooms();
            foreach (var room in rooms)
            {
                Console.WriteLine(room);
            }
        }

        private static void UpdateHotel()
        {
            Console.Clear();
            Console.WriteLine("indtast hotelNr");
            int hotelNo = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotel Navn");
            string name = Console.ReadLine();
            Console.WriteLine("Indlæs Hotel Adresse");
            string adresse = Console.ReadLine();
            HotelService hs = new HotelService();
            bool ok = hs.UpdateHotel(new Hotel(hotelNo, name, adresse), hotelNo);
            if (ok)
            {
                Console.WriteLine("Hotellet er opdateret");
            }
            else
            {
                Console.WriteLine("Hotellet er ikke opdateret");
            }
        }

        private static void GetHotelFromId()
        {
            Console.Clear();
            Console.WriteLine("Indtast HotelNr");
            int hotelNr = Convert.ToInt32(Console.ReadLine());
            HotelService hs = new HotelService();
            Hotel hotel = hs.GetHotelFromId(hotelNr);
            Console.WriteLine(hotel.ToString());
        }

        private static void DeleteHotel()
        {
                Console.Clear();
                Console.WriteLine("Indlæs hotelnr");
                int hotelnr = Convert.ToInt32(Console.ReadLine());



                HotelService hs = new HotelService();
                Hotel hotel = hs.DeleteHotel(hotelnr);
                Console.WriteLine($"Deleted hotel\n" + hotel.ToString());
        }

        
    
        private static void CreateHotel()
        {
            //Indlæs data
            Console.Clear();
            Console.WriteLine("Indlæs hotelnr");
            int hotelnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Indlæs hotelnavn");
            string navn = Console.ReadLine();
            Console.WriteLine("Indlæs hotel adresse");
            string adresse = Console.ReadLine();

            //Kalde hotelservice vise resultatet
            HotelService hs = new HotelService();
            bool ok = hs.CreateHotel(new Hotel(hotelnr, navn, adresse));
            if (ok)
            {
                Console.WriteLine("Hotellet blev oprettet!");
            }
            else
            {
                Console.WriteLine("Fejl. Hotellet blev ikke oprettet!");
            }

        }

        private static void ShowHotels()
        {
            Console.Clear();
            HotelService hs = new HotelService();
            List<Hotel> hotels = hs.GetAllHotel();
            foreach (Hotel h in hotels)
            {
                Console.WriteLine($"Hotel Nummer {h.HotelNr} Navn {h.Navn} Adresse {h.Adresse}");
            }
        }

        private static void DoSomething()
        {
            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i + " i GUI i main thread");
            }
        }

    }
}
