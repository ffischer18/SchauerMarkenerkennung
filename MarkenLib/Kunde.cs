using MarkenverwaltungLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenLib
{
    public class Kunde
    {
        public string AdAdressId { get; set; } = null!;
        public string? AdAdressNr { get; set; }
        public string? AdFirmenBezeichnung { get; set; }
        public string? AdStrasse { get; set; }
        public string? AdPostleitzahl { get; set; }
        public string? AdOrt { get; set; }
        public string? AdLandname { get; set; }
        public string? AdNationalitaetsKz { get; set; }
        public List<Ohrmarke>? Ohrmarken { get; set; }
        
    }
}
