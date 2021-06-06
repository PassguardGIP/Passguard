using MySql.Data.MySqlClient;
using prjGipSOFO_2021.Helper;
using prjGipSOFO_2021.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;

namespace prjGipSOFO_2021.DA
{
    public class MeldingDA
    {
        public static List<Melding> GetMeldingen()
        {
            List<Melding> lstMeldingen = new List<Melding>();

            MySqlConnection conn = Database.MakeConnection();
            string query = "SELECT tblmelding.idMelding, tblmelding.Registratie_id, tblregistratie.Tijdstip, tblmelding.User_id, tbluser.Voornaam, tbluser.Naam, tblmelding.Type_id, tblmeldingtype.Omschrijving, tblmelding.Poort_id, tblpoort.Locatie FROM tblmelding INNER JOIN tbluser ON idUser = User_id INNER JOIN tblregistratie ON idRegistratie = Registratie_id INNER JOIN tblmeldingtype ON idType = Type_id INNER JOIN tblpoort";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            DbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lstMeldingen.Add(CreateMelding(reader));
            }
            return lstMeldingen;
        }

        public static Melding CreateMelding(System.Data.IDataRecord record)
        {
            return new Melding()
            {
                idMelding = Convert.ToInt32(record["idMelding"]),
                Registratie_id = Convert.ToInt32((record["Registratie_id"])),
                Tijdstip = Convert.ToDateTime(record["Tijdstip"]),

                Type_id = Convert.ToInt32(record["Type_id"]),
                Omschrijving = Convert.ToString(record["Omschrijving"]),

                User_id = Convert.ToInt32(record["User_id"]),
                UserVoornaam = Convert.ToString(record["Voornaam"]),
                UserNaam = Convert.ToString(record["Naam"]),

                Poort_id = Convert.ToInt32(record["Poort_id"]),
                Locatie = Convert.ToString(record["Locatie"]),

            };
        }
    }
}
