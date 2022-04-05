using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;

namespace RazorPageHotelApp.Services
{
    /// <summary>
    /// Handles the display of hotels as wel as creating, updating and deleting hotels from the database.
    /// </summary>
    public class HotelService: Connection, IHotelService
    {
        private String queryString = "select * from po22_Hotel";
        private String queryNameString = "select * from po22_Hotel where Name like @Navn";
        private String queryStringFromID = "select * from po22_Hotel where Hotel_No = @ID";
        private String insertSql = "insert into po22_Hotel Values (@ID, @Navn, @Adresse)";
        private String deleteSql = "delete from po22_Hotel where Hotel_No = @ID";
        private String updateSql = "update po22_Hotel " +
                                   "set Hotel_No = @HotelID, Name = @Navn, Address = @Adresse " +
                                   "where Hotel_No = @ID";

        public HotelService(IConfiguration configuration) : base(configuration)
        {

        }

        public HotelService(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// Gets all hotels.
        /// </summary>
        /// <exception cref="SqlException">Throws an exception if there is an error in the database.</exception>
        /// <returns>Returns a list of hotels. If the number of hotels is 0, returns an empty list.</returns>
        public async Task<List<Hotel>> GetAllHotelAsync()
        {
            List<Hotel> hotels = new List<Hotel>();

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
                            int hotelNr = reader.GetInt32(0);
                            String hotelNavn = reader.GetString(1);
                            String hotelAdr = reader.GetString(2);

                            Hotel hotel = new Hotel(hotelNr, hotelNavn, hotelAdr);
                            hotels.Add(hotel);
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

            return hotels;
        }

        /// <summary>
        /// Gets a hotel by hotel number from the database.
        /// </summary>
        /// <param name="hotelNr">Hotel number of the hotel.</param>
        /// <exception cref="SqlException">Throws an exception if there is an error in the database.</exception>
        /// <returns>Returns a hotel.</returns>
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
                    throw sqlEx;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return new Hotel();
        }

        /// <summary>
        /// Takes a hotel and inserts it into the database.
        /// </summary>
        /// <param name="hotel">Hotel to be inserted into the database.</param>
        /// <exception cref="SqlException">Throws an exception if there is an error in the database.</exception>
        /// <returns>Returns true if successful</returns>
        public async Task<bool> CreateHotelAsync(Hotel hotel)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //try
                //{
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    command.Connection.Open();

                    int noOfRows = await command.ExecuteNonQueryAsync();

                    return noOfRows == 1;
                //}
                //catch (SqlException sqlEx)
                //{
                //    throw sqlEx;
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}
            }
        }

        /// <summary>
        /// Takes a hotel and updates the hotel from the database by hotel number.
        /// </summary>
        /// <param name="hotel">New hotel to update the old hotel.</param>
        /// <param name="hotelNr">Hotel number of the hotel to be updated</param>
        /// <exception cref="SqlException">Throws an exception if there is an error in the database.</exception>
        /// <returns>Returns true if successful</returns>
        public async Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(updateSql, connection);
                    command.Parameters.AddWithValue("@HotelID", hotelNr);
                    command.Parameters.AddWithValue("@ID", hotel.HotelNr);
                    command.Parameters.AddWithValue("@Navn", hotel.Navn);
                    command.Parameters.AddWithValue("@Adresse", hotel.Adresse);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();

                    return true;
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

        /// <summary>
        /// Deletes a hotel from the database by hotel number.
        /// </summary>
        /// <param name="hotelNr">Hotel number of the hotel to be deleted.</param>
        /// <exception cref="SqlException">Throws an exception if there is an error in the database.</exception>
        /// <returns>Returns the deleted hotel.</returns>
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

            return hotel;
        }

        /// <summary>
        /// Gets a hotel by name from the database.
        /// </summary>
        /// <param name="name">Name of the hotel.</param>
        /// <exception cref="SqlException">Throws an exception if there is an error in the database.</exception>
        /// <returns>Returns a List of hotels.</returns>
        public async Task<List<Hotel>> GetHotelsByNameAsync(string name)
        {
            List<Hotel> hotels = new List<Hotel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryNameString, connection);
                    command.Parameters.AddWithValue("@Navn", name);
                    command.Connection.Open();
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        int hotelNo = reader.GetInt32(0);
                        string hotelNavn = reader.GetString(1);
                        string hotelAdr = reader.GetString(2);

                        hotels.Add(new Hotel(hotelNo, hotelNavn, hotelAdr));
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

            return hotels;
        }
    }
}