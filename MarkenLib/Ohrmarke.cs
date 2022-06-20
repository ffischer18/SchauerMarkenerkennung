using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenLib
{
    public  class Ohrmarke
    {
        public int Id { get; set; } 
        public string KundeAD_ADRESS_ID { get; set; } 
        public ST_ADRESSE Kunde { get; set; }
        public string MarkenNummer { get; set; }
        public string? Beschreibung { get; set; }
        public DateTime Datum { get; set; }
        public string? Kommissionierer { get; set; }
        public string? Markentyp { get; set; }

        public string Display { get => Kunde.AD_FIRMEN_BEZEICHNUNG + "; Markennummer: " + MarkenNummer; }

    }
}
