using System;
using System.Collections.Generic;
using System.Text;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services 
{
    

    public class RoomService : Connection
    {

        private string ListRoomSql = "select * from po22_Room";
        private string RoomFromIdSql = "select * from po22_Room where Hotel_No = @ID";
        private string CreateRoomSql = "insert into po22_Room values(@Room_No, @Hotel_No, @Types, @Price)";

        // lad klassen arve fra interfacet IRoomService og arve fra Connection klassen
        // indsæt sql strings

        //Implementer metoderne som der skal ud fra interfacet

        public List<Room> GetAllRoom()
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

        public Room GetRoomFromId(int roomNr, int hotelNr)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(RoomFromIdSql, connection);
                command.Parameters.AddWithValue("@ID", hotelNr);
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Room r = new Room();
                while (reader.Read())
                {
                    int roomNo = reader.GetInt32(0);
                    int hotelNo = reader.GetInt32(1);
                    char types = reader.GetString(2)[0];
                    double price = reader.GetDouble(3);
                    r = new Room(roomNr, types, price, hotelNo);
                }

                return r;
            }
        }

        bool CreateRoom(int hotelNr, Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(CreateRoomSql, connection);
                command.Parameters.AddWithValue("@Room_No", room.RoomNr);
                command.Parameters.AddWithValue("@Hotel_No", room.HotelNr);
                command.Parameters.AddWithValue("@Types", room.Types);
                command.Parameters.AddWithValue("@Price", room.Pris);
                command.Connection.Open();
                int noOfRows = command.ExecuteNonQuery();
                return noOfRows == 1;
            }
        }

         bool UpdateRoom(Room room, int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }

         Room DeleteRoom(int roomNr, int hotelNr)
        {
            throw new NotImplementedException();
        }
    }
}
