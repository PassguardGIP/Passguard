using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;

namespace prjGipSOFO_2021.DA
{
    public class PoortDA
    {
        // SELECT tblpoort.Locatie, group_concat(tbltijden.Van) AS 'Starttijden', group_concat(tbltijden.Tot) AS 'Sluitingstijden' FROM tblpoort INNER JOIN tbltijden ON idPoort = Poort_id group by tblpoort.Locatie
        public static List<Poort> GetPoorten()
        {
            List<Poort> lstPoorten = new List<Poort>();

            MySqlConnection conn = Database.MakeConnection();
            string query = "SELECT DISTINCT * FROM gipdb.tblpoort";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lstPoorten.Add(Create(reader));
            }

            Database.CloseConnection(conn);
            return lstPoorten;
        }

        public static List<Tijden> getTijden(int id)
        {
            List<Tijden> lstTijden = new List<Tijden>();

            MySqlConnection conn = Database.MakeConnection();
            string query = "SELECT tblpoort.idPoort, tblpoort.Locatie, group_concat(tbltijden.Van) AS 'Openingstijden', group_concat(tbltijden.Tot) AS 'Sluitingstijden' FROM tblpoort INNER JOIN tbltijden ON idPoort = Poort_id WHERE Poort_id = @id GROUP BY tblpoort.Locatie";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lstTijden.Add(CreateTijden(reader));
            }

            return lstTijden;
        }

        public static long AddPoort(Poort p)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "INSERT INTO tblpoort (Locatie, Beschikbaar) VALUES (@Locatie, 1)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Locatie", p.Locatie);

            cmd.ExecuteNonQuery();

            long id = cmd.LastInsertedId;
            Database.CloseConnection(conn);

            return id;
        }

        public static void EditPoort(int id, Poort p)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "UPDATE tblpoort SET Locatie = @Locatie WHERE idPoort = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Locatie", p.Locatie);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static void DeletePoort(int id)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "DELETE FROM tblpoort WHERE idPoort = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);

        }

        public static void AddTijd(int id, TimeSpan van, TimeSpan tot)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "INSERT INTO tbltijden (Poort_id, Van, Tot) VALUES (@id, @van, @tot)";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@van", van);
            cmd.Parameters.AddWithValue("@tot", tot);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static void DeleteTijd(int id, TimeSpan van, TimeSpan tot)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "DELETE FROM tbltijden WHERE Poort_id = @id AND Van = @van AND Tot = @tot";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@van", van);
            cmd.Parameters.AddWithValue("@tot", tot);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static void WipeTijden(int id)
        {
            MySqlConnection conn = Database.MakeConnection();
            string query = "DELETE FROM tbltijden WHERE Poort_id = @id";

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();
            Database.CloseConnection(conn);
        }

        public static Poort Create(System.Data.IDataRecord record)
        {
            return new Poort()
            {
                idPoort = Convert.ToInt32(record["idPoort"]),
                Locatie = Convert.ToString(record["Locatie"]),
                Beschikbaar = Convert.ToBoolean(record["Beschikbaar"])
            };
        }

        public static Tijden CreateTijden(System.Data.IDataRecord record)
        {
            return new Tijden()
            {
                Poort_id = Convert.ToInt32(record["idPoort"]),
                Openingstijden = record["Openingstijden"].ToString(),
                Sluitingstijden = record["Sluitingstijden"].ToString()
            };
        }

        public static IEnumerable<(DateTime, DateTime)> Combine(IEnumerable<(DateTime, DateTime)> intervals)
        {
            if (intervals is null) throw new ArgumentNullException(nameof(intervals));
            (DateTime, DateTime)? combined = null;
            foreach (var interval in intervals.OrderBy(i => i.Item1))
            {
                if (interval.Item2 < interval.Item1) throw new ArgumentException($"Invalid range in {nameof(intervals)}: Item2 < Item1.", nameof(intervals));
                (DateTime, DateTime)? newCombined = default;
                if (combined.HasValue && (newCombined = Union(combined.Value, interval)).HasValue)
                {
                    combined = newCombined;
                }
                else
                {
                    if (combined.HasValue)
                    {
                        yield return combined.Value;
                    }
                    combined = interval;
                }
            }
            if (combined.HasValue)
            {
                yield return combined.Value;
            }
        }


        public static (DateTime, DateTime)? Union((DateTime, DateTime) a, (DateTime, DateTime) b) =>
            a.Item2 < b.Item1 || b.Item2 < a.Item1 ? ((DateTime, DateTime)?)null : (Min(a.Item1, b.Item1), Max(a.Item2, b.Item2));

        public static DateTime Min(DateTime a, DateTime b) => b < a ? b : a;

        // = zelfde als:
        //
        //    if (b < a)
        //    {
        //        return b;
        //    }
        //    else
        //    {
        //        return a;
        //    }


        public static DateTime Max(DateTime a, DateTime b) => b > a ? b : a;
    }
}

