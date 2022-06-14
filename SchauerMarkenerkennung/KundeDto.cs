using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung
{
    public class KundeDto
    {
        public string AD_ADRESS_ID { get; set; } = null!;
        public string? AD_FIRMEN_BEZEICHNUNG { get; set; }

        public override string ToString()
        {
            return AD_FIRMEN_BEZEICHNUNG;
        }
    }
}
