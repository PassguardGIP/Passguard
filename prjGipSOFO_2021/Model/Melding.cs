using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prjGipSOFO_2021.Model
{
    public class Melding
    {
        public int idMelding { get; set; }
        public int Registratie_id { get; set; }
        public DateTime Tijdstip { get; set; }

        public int Type_id { get; set; }
        public string Omschrijving { get; set; }

        public int User_id { get; set; }
        public string UserVoornaam { get; set; }
        public string UserNaam { get; set; }

        public int Poort_id { get; set; }
        public string Locatie { get; set; }
    }
}
