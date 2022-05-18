using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkenverwaltungLib
{
    public  class Ohrmarke
    {
        public int Id { get; set; }
        public string? KundenId { get; set; } 
        public Kunde Kunde { get; set; }
        public string? Beschreibung { get; set; }
        public DateOnly? Datum { get; set; }
        public string? Lieferant { get; set; }
        public string? Markentyp { get; set; }

    }
}
