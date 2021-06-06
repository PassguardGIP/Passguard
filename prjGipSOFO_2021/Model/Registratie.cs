using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGipSOFO_2021.Model
{
    public class Registratie
    {
        public int idRegistratie { get; set; }
        public int User_id { get; set; }
        public int Poort_id { get; set; }
        public string Poort { get; set; }
        public string Barcode { get; set; }
        public string Voornaam { get; set; }
        public string Naam { get; set; }
        public DateTime Tijdstip { get; set; }
        public int Action_id { get; set; }
        public string Action { get; set; }
        public int Status { get; set; }

    }
}
