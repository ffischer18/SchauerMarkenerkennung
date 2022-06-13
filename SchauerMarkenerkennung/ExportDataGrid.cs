using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung
{
    public class ExportDataGrid
    {
        public int Id { get; set; }
        public int AdAdressNr { get; set; }
        public string AdFirmenBezeichnung { get; set; } = null!;
        public string AdStrasse { get; set; } = null!;
        public string AdPostleitzahl { get; set; } = null!;
        public string AdOrt { get; set; } = null!;
        public string AdLandname { get; set; } = null!;
        public string AdNationalitaetsKz { get; set; } = null!;
        public string Markennummern { get; set; } = null!;
    }
}
