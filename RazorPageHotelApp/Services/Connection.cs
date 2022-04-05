using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace RazorPageHotelApp.Services
{
    public abstract class Connection
    {
        protected string connectionString = @"Data Source=mssql8.unoeuro.com; Initial Catalog = catsmeow_dk_db_cats; User ID = catsmeow_dk; Password=gm5hr4ekcD6d; Connect Timeout = 30; Encrypt=False; TrustServerCertificate=False;ApplicationIntent=ReadWrite; MultiSubnetFailover=False";

        //public IConfiguration Configuration { get; }

        public Connection(IConfiguration configuration)
        {
            //Configuration = configuration;
            //connectionString = Configuration["ConnectionStrings:DefaultConnection"];
        }

        public Connection(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}