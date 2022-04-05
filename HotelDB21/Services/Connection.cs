using System;
using System.Collections.Generic;
using System.Text;

namespace HotelDBConsole21.Services
{
    public abstract class Connection
    {
        //indsæt din egen connectionstring
        //protected String connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Hotel02032020;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //protected String connectionString =
        //    @"Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = HotelDB250322; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected String connectionString =
            @"Data Source = mssql15.unoeuro.com; Initial Catalog = patr_zealand_dk_db_patrick; User ID = patr_zealand_dk; Password=fGHywAxB9t62;Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
    }
}
