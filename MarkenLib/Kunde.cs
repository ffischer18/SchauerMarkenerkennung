using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenLib
{
    public class Kunde
    {
        public int Id { get; set; }
        //public string AdAdressId { get; set; }
        public int AdAdressNr { get; set; }
        public string? AdFirmenBezeichnung { get; set; }
        public string? AdStrasse { get; set; }
        public string? AdPostleitzahl { get; set; }
        public string? AdOrt { get; set; }
        public string? AdLandname { get; set; }
        public string? AdNationalitaetsKz { get; set; }
        public List<Ohrmarke>? Ohrmarken { get; set; }

        public string Display { get => AdFirmenBezeichnung + "; Addresse: " + AdStrasse; }

        public string ohrmarken { get => Ohrmarkennummer(); }

        public string Ohrmarkennummer()
        {
            string ohrmarkenString = "";

            foreach (var item in Ohrmarken)
            {
                ohrmarkenString = ohrmarkenString + ";" + item.Id.ToString();
            }

            return ohrmarkenString;
        }
    }
}
