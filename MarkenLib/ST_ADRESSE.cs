using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenLib
{
    public class ST_ADRESSE
    {
        [Key]
        public string AD_ADRESS_ID { get; set; } = null!;
        public string? AD_ADRESS_NR { get; set; }
        public string? AD_FIRMEN_BEZEICHNUNG { get; set; }
        public string? AD_STRASSE { get; set; }
        public string? AD_POSTLEITZAHL { get; set; }
        public string? AD_ORT{ get; set; }
        public string? AD_LANDNAME { get; set; } 
        public string? AD_NATIONALITAETS_KZ { get; set; }

        public List<Ohrmarke>? Ohrmarken { get; set; }

        public string Display { get => AD_FIRMEN_BEZEICHNUNG + "; Addresse: " + AD_STRASSE; }

        public string ohrmarken { get => Ohrmarkennummer(); }

        public string Ohrmarkennummer()
        {
            string ohrmarkenString = "";

            foreach (var item in Ohrmarken)
            {
                ohrmarkenString = ohrmarkenString + item.MarkenNummer + ";";
            }

            return ohrmarkenString;
        }
    }
}
