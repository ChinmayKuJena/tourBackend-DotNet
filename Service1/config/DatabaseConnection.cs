using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Service1.config
{
    public class DatabaseConnection
    {

        private readonly string _connectionString;
        // private string _connectionString=Environment.GetEnvironmentVariable("SQL_SERVER_CONNECTION_STRING");

        public DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

    }
}