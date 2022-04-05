using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HotelDBConsole21.Interfaces;
using HotelDBConsole21.Models;
using Microsoft.Data.SqlClient;

namespace HotelDBConsole21.Services
{
    class HotelServiceAsync : Connection, IHotelServiceAsync
    {
        private string queryString = "select * from po22_Hotel";
        private String queryStringFromID = "select * from po22_Hotel where Hotel_No = @ID";
        private string insertSql = "insert into po22_Hotel Values(@ID, @Navn, @Adresse)";
        private string deleteSql = "delete from po22_Hotel where Hotel_No = @ID";
        private string updateSql = "update po22_Hotel set Name = @Navn, Address = @Adresse where Hotel_No = @ID";
        private string ListRoomSql = "select * from po22_Room";
        private string RoomInHotSql = "select * from po22_Room where Hotel_No = @ID";


        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hotels = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        int hotelNr = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                        hotels.Add(hotel);
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database Error\n" + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel Error\n" + ex.Message);
                }
            }

            return hotels;
        }

        public async Task<Hotel> GetHotelFromIdAsync(int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryStringFromID, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);
                        Hotel hotel = new Hotel(hotelNo, hotelNavn, hotelAdr);
                        return hotel;
                    }
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database Error\n" + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel Error\n" + ex.Message);
                }
            }

            return null;
        }

        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database Error\n" + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel Error\n" + ex.Message);
                }
            }

            return false;
        }

        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    return true;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database Error\n" + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel Error\n" + ex.Message);
                }
            }

            return false;
        }

        public async Task<Hotel> DeleteHotelAsync(int hotelNr)
        {
            Hotel hotel = await GetHotelFromIdAsync(hotelNr);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@ID", hotelNr);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    return hotel;
                }
                catch (SqlException sqlEx)
                {
                    Console.WriteLine("Database Error\n" + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel Error\n" + ex.Message);
                }
            }

            return new Hotel();
        }

        public Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
