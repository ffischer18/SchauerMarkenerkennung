using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung
{
    public class OhrenmarkeDTO
    {
        public string KundenName { get; set; }
        public string Markennummer { get; set; }

        public override string ToString()
        {
            return "Firmenbezeichnung: " + KundenName + " Markennummer: " + Markennummer;
        }

       
    }
}
