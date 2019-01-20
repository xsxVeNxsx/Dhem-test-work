using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace DHEM_TestWork.DB
{
    static class DBConnection
    {
        static public MySqlConnection CreateConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString());

            connection.Open();

            return connection;
        }

        static private string ConnectionString()
        {
            return String.Format(
                "uid={0};password={1};server={2};database={3};Port={4};Connect Timeout=50;old guids=true;",
                Properties.Settings.Default.DB_User,
                Properties.Settings.Default.DB_Password,
                Properties.Settings.Default.DB_Host,
                Properties.Settings.Default.DB_Name,
                Properties.Settings.Default.DB_Port
            );
        }
    }
}
