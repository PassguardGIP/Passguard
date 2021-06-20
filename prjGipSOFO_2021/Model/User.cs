using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prjGipSOFO_2021.DA;
using prjGipSOFO_2021.Helper;


namespace prjGipSOFO_2021.Model
{
    public class User
    {
        public int idUser { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public string Barcode { get; set; }
        public string Adres { get; set; }
        public int Postcode { get; set; }
        public string Gemeente { get; set; }
        public string Emailadres { get; set; }
        public bool Status { get; set; }
        public int Rol_id { get; set; }
        public string Rol { get; set; }
        public int MagBuiten { get; set; }
    }
}
