using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung
{
    public class ExportOhrmarkenDataGrid
    {
        public string Markennummer { get; set; }
        public string? AD_ADRESS_ID { get; set; }
        public string? Beschreibung { get; set; }
        public DateTime Datum { get; set; }
        public string? Lieferant { get; set; }
        public string? Markentyp { get; set; }
        public string? Kundenname { get; set; }
        public string? Kundennummer { get; set; }
    }
}
