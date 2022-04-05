using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    public class RoomService : Connection, IRoomService 
    {
        private String queryString = "select * from po22_Room";
        private String queryNameString = "select * from po22_Room where Types like @Types";
        private String queryStringFromID = "select * from po22_Room where Room_No = @ID and Hotel_No = @HotelID";
        private String insertSql = "insert into po22_Room Values (@ID, @Hotel_No, @Types, @Price)";
        private String deleteSql = "delete from po22_Room where Room_No = @ID and Hotel_No = @HotelID";
        private String updateSql = "update po22_Room set Room_No = @RoomID, Types = @Types, Price = @Price where Room_No = @ID and Hotel_No = @HotelID";

        public RoomService(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<List<Room>> GetAllRoomAsync()
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queryString, connection))
                {
                    try
                    {
                        await command.Connection.OpenAsync();
                        SqlDataReader reader = await command.ExecuteReaderAsync();
                        while (await reader.ReadAsync())
                        {
                            int roomNr = reader.GetInt32(0);
                            int hotelNr = reader.GetInt32(1);
                            Char types = reader.GetString(2)[0];
                            double price = reader.GetDouble(3);
                            Room room = new Room(roomNr, hotelNr, types, price);

                            rooms.Add(room);
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        throw sqlEx;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return rooms;
        }

        public async Task<Room> GetRoomFromIdAsync(int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringFromID, connection);
                    command.Parameters.AddWithValue("@ID", roomNr);
                    command.Parameters.AddWithValue("@HotelID", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        char type = reader.GetString(2)[0];
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNr, hotelNr, type, price);

                        return room;
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return new Room();
        }

        public async Task<bool> CreateRoomAsync(Room room)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", room.RoomNr);
                    command.Parameters.AddWithValue("@Hotel_No", room.HotelNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Price);
                    await command.Connection.OpenAsync();

                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@ID", roomNr);
                    command.Parameters.AddWithValue("@HotelID", hotelNr);
                    command.Parameters.AddWithValue("@RoomID", room.RoomNr);
                    command.Parameters.AddWithValue("@Types", room.Types);
                    command.Parameters.AddWithValue("@Price", room.Price);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return true;
        }

        public async Task<Room> DeleteRoomAsync(int roomNr, int hotelNr)
        {
            Room room = await GetRoomFromIdAsync(roomNr, hotelNr);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", roomNr);
                    command.Parameters.AddWithValue("@HotelID", hotelNr);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return room;
        }

        public async Task<List<Room>> GetRoomsByTypeAsync(string types)
        {
            List<Room> rooms = new List<Room>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryNameString, connection);
                    command.Parameters.AddWithValue("@Types", types);
                    command.Connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int roomNo = reader.GetInt32(0);
                        int hotelNo = reader.GetInt32(1);
                        char type = reader.GetString(2)[0];
                        double price = reader.GetDouble(3);
                        Room room = new Room(roomNo, hotelNo, type, price);
                        rooms.Add(room);
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return rooms;
        }
    }
}
