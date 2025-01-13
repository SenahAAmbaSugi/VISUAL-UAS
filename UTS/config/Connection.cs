using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace UTS.config
{
    internal class Connection
    {
        private readonly MySqlConnection connection;
        private readonly string connectionString;

        public Connection()
        {
            connectionString = "Server=localhost;Database=klinikdb;Uid=root;Pwd=;Convert Zero Datetime=True;";
            connection = new MySqlConnection(connectionString);
        }

        public MySqlConnection GetConnection()
        {
            return connection;
        }

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
    }
}