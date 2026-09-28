using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace EntregableADONET.Data
{
    public class SqlConnectionFactory
    {
        private const string ConnectionString =
        "Server=localhost;" +
        "Database=VentasDB;" +
        "Integrated Security=True;" +
        "TrustServerCertificate=True;";

        public SqlConnection Create()
        {
            return new SqlConnection(ConnectionString);
        }


    }
}
