using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using prjGipSOFO_2021.DA;

namespace prjGipSOFO_2021.Helper
{
    public class Database
    {
        public static MySqlConnection MakeConnection()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder
            {
                Server = "xxxx",
                Database = "xxxx",
                UserID = "xxxxx"
                Password = "xxxxx",
                Port = 3306,
            };

            MySqlConnection conn = new MySqlConnection(builder.ConnectionString);
            conn.Open();
            return conn;
        }
        public static void CloseConnection(MySqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }
    }
}
