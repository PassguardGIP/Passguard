using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;
using prjGipSOFO_2021.DA;
using System.Data.Common;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace prjGipSOFO_2021.DA
{
    public class RegistratieDA
    {
        public static List<Registratie> GetRegistraties()
        {
            MySqlConnection conn = Database.MakeConnection();

            // in deze lijst komt de Registratie die dan in lsvRegistraties komt
            List<Registratie> lstRegistraties = new List<Registratie>();

            string readquery = "SELECT tblregistratie.idRegistratie, tblregistratie.User_id, tblregistratie.Poort_id, tbluser.Voornaam, tbluser.Naam, tblpoort.Locatie, tblregistratie.Tijdstip, tblregistratie.Barcode, tblregistratie.Action_id, tblaction.Beschrijving AS 'Action', tblregistratie.Status FROM tblregistratie INNER JOIN tbluser ON User_id = idUser INNER JOIN tblpoort ON Poort_id = idPoort INNER JOIN tblaction ON idAction = Action_id ORDER BY idRegistratie DESC";

            MySqlCommand readCmd = new MySqlCommand(readquery, conn);
            DbDataReader reader = readCmd.ExecuteReader();

            // voeg toe aan lijst om daarna een object Registratie daarvan te maken
            // dat object Registratie komt in de lsvRegistraties terecht
            while (reader.Read())
            {
                lstRegistraties.Add(Create(reader));
            }

            Database.CloseConnection(conn);
            return lstRegistraties;
        }

        public static Registratie Create(System.Data.IDataRecord record)
        {
            return new Registratie()
            {
                idRegistratie = Convert.ToInt32(record["idRegistratie"]),
                User_id = Convert.ToInt32(record["User_id"]),
                Poort_id = Convert.ToInt32(record["Poort_id"]),
                Poort = Convert.ToString(record["Locatie"]),
                Voornaam = Convert.ToString(record["Voornaam"]),
                Naam = Convert.ToString(record["Naam"]),
                Barcode = Convert.ToString(record["Barcode"]),
                Tijdstip = Convert.ToDateTime(record["Tijdstip"]),
                Action_id = Convert.ToInt32(record["Action_id"]),
                Action = Convert.ToString(record["Action"]),
                Status = Convert.ToInt32(record["Status"])
            };
        }

    }
}
