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
        public int? KundeId { get; set; } 
        public Kunde Kunde { get; set; }
        public string? Beschreibung { get; set; }
        public DateTime Datum { get; set; }
        public string? Lieferant { get; set; }
        public string? Markentyp { get; set; }


    }
}
