using MySql.Data.MySqlClient;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Windows.Forms;

namespace prjGipSOFO_2021.DA
{
    public class UserDA
    {
        public static bool Login(string username, string password)
        {
            MySqlConnection conn = new MySqlConnection();
            string query = "SELECT Password FROM tbllogin WHERE Username = @Username";

            MySqlCommand cmd = new MySqlCommand(query, Database.MakeConnection());
            cmd.Parameters.AddWithValue("@Username", username);

            DbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            string passwordhash = reader.GetString(0);

            bool valid = BCrypt.Net.BCrypt.Verify(password, passwordhash, false);

            Database.CloseConnection(conn);

            if (valid)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static string Hash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static void AddLogin(string username, string password)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "INSERT INTO tbllogin (Username, Password) VALUES (@user, @pass)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@user", username);
            cmd.Parameters.AddWithValue("@pass", password);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static List<User> GetUsers()
        {
            List<User> lstUsers = new List<User>();

            string readquery = "SELECT DISTINCT tbluser.idUser, tbluser.Voornaam, tbluser.Naam, tbluser.Barcode, tbluser.Emailadres, tbluser.Status, tbluser.Rol_id, tblrol.Rol, tbluser.MagBuiten FROM tbluser INNER JOIN tblrol ON idRol = Rol_id";

            MySqlConnection conn = Database.MakeConnection();
            MySqlCommand readCmd = new MySqlCommand(readquery, conn);
            DbDataReader reader = readCmd.ExecuteReader();

            while (reader.Read())
            {
                lstUsers.Add(CreateUser(reader));
            }

            Database.CloseConnection(conn);
            return lstUsers;
        }

        public static User GetUser(int id)
        {
            User user = new User();
            MySqlConnection conn = Database.MakeConnection();

            string readquery = "SELECT DISTINCT * FROM tbluser WHERE idUser = @id LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(readquery, conn);
            cmd.Parameters.AddWithValue("@id", id);

            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                user = GetUserInfo(reader);
            }

            Database.CloseConnection(conn);
            return user;
        }

        public static List<ListViewItem> GetAllAanwezig()
        {
            List<ListViewItem> lstUsers = new List<ListViewItem>();
            MySqlConnection conn = Database.MakeConnection();

            string query = "SELECT DISTINCT tbluser.Voornaam, tbluser.Naam, tblregistratie.Tijdstip FROM tbluser INNER JOIN tblregistratie ON idUser = User_id WHERE tblregistratie.Status = 1 AND Tijdstip = (SELECT MAX(Tijdstip) FROM tblregistratie WHERE tblregistratie.User_id = tbluser.idUser)";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lstUsers.Add(new ListViewItem(new String[] { reader.GetString(0), reader.GetString(1), reader.GetString(2) }));
            }

            Database.CloseConnection(conn);
            return lstUsers;
        }

        public static int[] GetData()
        {
            MySqlConnection conn = Database.MakeConnection();
            // SELECT count(*) AS total, SUM(case when Status = 1 then 1 else 0 end) AS 'Binnen', SUM(case when Status = 0 then 1 else 0 end) AS 'Buiten' FROM tbluser
            /*
             *  case in mysql = if statement:
             *      case when Status = 1  -> SUM(1)
             *      case when Statis = 0  -> SUM(0)
             *      
             *      => SUM(1, 0)
             *      
             *      dan 'end' op het einde
             */

            string query = "SELECT SUM(case when Status = 1 then 1 else 0 end) AS 'Binnen', SUM(case when Status = 0 then 1 else 0 end) AS 'Buiten', count(*) AS 'Totaal' FROM tbluser";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            DbDataReader reader = cmd.ExecuteReader();
            reader.Read();

            if (reader.HasRows)
            {
                int aantalBinnen = Convert.ToInt32(reader["Binnen"]);
                int aantalBuiten = Convert.ToInt32(reader["Buiten"]);
                int totaal = Convert.ToInt32(reader["Totaal"]);

                int[] arrData = { aantalBinnen, aantalBuiten, totaal };
                return arrData;
            }

            Database.CloseConnection(conn);
            return null;
            
        }
        
        // result met inner join zonder adres enzo
        public static User CreateUser(System.Data.IDataRecord record)
        {
            return new User()
            {
                idUser = Convert.ToInt32(record["idUser"]),
                Voornaam = record["Voornaam"].ToString(),
                Naam = record["Naam"].ToString(),
                Barcode = record["Barcode"].ToString(),
                Emailadres = record["Emailadres"].ToString(),
                Status = Convert.ToBoolean(record["Status"]),
                Rol_id = Convert.ToInt32(record["Rol_id"]),
                Rol = Convert.ToString(record["Rol"]),
                MagBuiten = Convert.ToInt32(record["MagBuiten"])
            };
        }

        // result zonder inner join, alleen tabel
        public static User GetUserInfo(System.Data.IDataRecord record)
        {
            return new User()
            {
                idUser = Convert.ToInt32(record["idUser"]),
                Voornaam = record["Voornaam"].ToString(),
                Naam = record["Naam"].ToString(),
                Adres = record["Adres"].ToString(),
                Postcode = Convert.ToInt32(record["Postcode"]),
                Gemeente = record["Gemeente"].ToString(),
                Barcode = record["Barcode"].ToString(),
                Emailadres = record["Emailadres"].ToString(),
                MagBuiten = Convert.ToInt32(record["MagBuiten"])
            };
        }

        public static void AddUser(User user)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "INSERT INTO tbluser (Voornaam, Naam, Adres, Postcode, Gemeente, Barcode, Status, Rol_id, Emailadres) VALUES (@voornaam, @naam, @adres, @postcode, @gemeente, @barcode, @status, @rol_id, @email)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@voornaam", user.Voornaam);
            cmd.Parameters.AddWithValue("@naam", user.Naam);
            cmd.Parameters.AddWithValue("@adres", user.Adres);
            cmd.Parameters.AddWithValue("@postcode", user.Postcode);
            cmd.Parameters.AddWithValue("@gemeente", user.Gemeente);
            cmd.Parameters.AddWithValue("@barcode", user.Barcode);
            cmd.Parameters.AddWithValue("@status", user.Status);
            cmd.Parameters.AddWithValue("@rol_id", user.Rol_id);
            cmd.Parameters.AddWithValue("@email", user.Emailadres);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);

        }

        public static void UpdateUser(int id, User user)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "UPDATE tbluser SET Voornaam = @voornaam, Naam = @naam, Adres = @adres, Postcode = @postcode, Gemeente = @gemeente, Barcode = @barcode, Emailadres = @email WHERE idUser = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@voornaam", user.Voornaam);
            cmd.Parameters.AddWithValue("@naam", user.Naam);
            cmd.Parameters.AddWithValue("@adres", user.Adres);
            cmd.Parameters.AddWithValue("@postcode", user.Postcode);
            cmd.Parameters.AddWithValue("@gemeente", user.Gemeente);
            cmd.Parameters.AddWithValue("@barcode", user.Barcode);
            cmd.Parameters.AddWithValue("@email", user.Emailadres);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static void DeleteUser(int id)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "DELETE FROM tbluser WHERE idUser = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static string GenerateBarcode()
        {
            Random random = new Random();

            string code = "";

            for (int j = 0; j < 9; j++)
            {
                code += random.Next(0, 10);
            }

            // check voor unieke barcode
            MySqlConnection conn = Database.MakeConnection();
            string query = "SELECT COUNT(*) FROM tbluser WHERE Barcode = @barcode";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@barcode", code);

            int count = Convert.ToInt32(cmd.ExecuteScalar());

            if (count > 0)
            {
                Console.WriteLine("Barcode {0} already exists, creating new one", code);
                GenerateBarcode();
            }

            Database.CloseConnection(conn);
            return code;

        }

        


    }
}
