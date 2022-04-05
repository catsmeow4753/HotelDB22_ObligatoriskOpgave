using Microsoft.Data.SqlClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RazorPageHotelApp.Interfaces;
using RazorPageHotelApp.Models;
using RazorPageHotelApp.Services;

namespace TestProject
{
    [TestClass]
    public class UnitTest1
    {
        protected string connectionString = @"Data Source=mssql8.unoeuro.com; Initial Catalog = catsmeow_dk_db_cats; User ID = catsmeow_dk; Password=gm5hr4ekcD6d; Connect Timeout = 30; Encrypt=False; TrustServerCertificate=False;ApplicationIntent=ReadWrite; MultiSubnetFailover=False";

        [TestMethod]
        public void TestCount()
        {
            // Arrange
            IHotelService service = new HotelService(connectionString);

            int countBefore = service.GetAllHotelAsync().Result.Count;

            Hotel hotel1 = new Hotel(countBefore + 1, "Test count", "Address");

            // Act
            service.CreateHotelAsync(hotel1);

            int countAfter = service.GetAllHotelAsync().Result.Count;

            // Assert
            Assert.AreEqual(countBefore + 1, countAfter);
        }
    }
}