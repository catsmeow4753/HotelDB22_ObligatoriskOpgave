using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    public class HotelService : Connection, IHotelService
    {
        private string queryString = "select * from po22_Hotel";
        private String queryStringFromID = "select * from Hotel where Hotel_No = @ID";
        private string insertSql ="insert into po22_Hotel Values(@ID, @Navn, @Adresse)";
        private string deleteSql = "delete from po22_Hotel where Hotel_No = @ID";
        private string updateSql = "update po22_Hotel set Name = @Navn, Address = @Adresse where Hotel_No = @ID" ;
        private string ListRoomSql = "select * from po22_Room";
        private string RoomInHotSql = "select * from po22_Room where Hotel_No = @ID";
        // lav selv sql strengene færdige og lav gerne yderligere sqlstrings


        public List<Hotel> GetAllHotel()
        {
            List<Hotel> hoteller = new List<Hotel>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
               
                    SqlCommand command = new SqlCommand(queryString, connection);
                    command.Connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        int hotelNr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hoteller.Add(hotel);

                    }


            }
            

            return hoteller;
        }

        public Hotel GetHotelFromId(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryStringFromID, connection);
                command.Connection.Open();
                //SqlDataReader reader = command.ExecuteReader();
                foreach (var h in GetAllHotel())
                {
                    if (h.HotelNr == hotelNr )
                    {
                        return h;
                    }
                }

            }

            return null;
        }

        public bool CreateHotel(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(insertSql, connection);
                command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                command.Connection.Open();
                int noOfRows = command.ExecuteNonQuery();
                return noOfRows == 1;
            }
        }

        public bool UpdateHotel(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(updateSql, connection);
                command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                command.Parameters.AddWithValue("@Navn", hotel.Navn);
                command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }

            return true;
        }

       
        public Hotel DeleteHotel(int hotelNr)
            {
                Hotel hotel = GetHotelFromId(hotelNr); 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    command.Connection.Open();
                    command.ExecuteNonQuery();
                }
                return hotel;
            }


        public List<Hotel> GetHotelsByName(string name)
        {
            throw new NotImplementedException();
        }


        public List<Room> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(ListRoomSql, connection);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int roomNr = reader.GetInt32(0);
                    int hotelNr = reader.GetInt32(1);
                    char types = reader.GetString(2)[0];
                    double price = reader.GetDouble(3);
                    Room r = new Room(roomNr, types, price, hotelNr);
                    rooms.Add(r);
                }
            }

            return rooms;
        }

        public List<Room> RoomInHot(int hotelNr)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(RoomInHotSql, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int roomNr = reader.GetInt32(0);
                    int hotelNo = reader.GetInt32(1);
                    char types = reader.GetString(2)[0];
                    double price = reader.GetDouble(3);
                    Room r = new Room(roomNr, types, price, hotelNo);
                    rooms.Add(r);
                }
            }

            return rooms;
        }
    }

}
