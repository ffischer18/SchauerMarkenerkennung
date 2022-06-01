using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    public class ScanViewModel
    {
        MarkenContext _db = new MarkenContext();

        List<Kunde> testList = new List<Kunde>();
        public ScanViewModel()
        {
            //testList = _db.Kunden.Select(x => x).ToList();
        }

        private List<Kunde> Kunden;

        public List<Kunde> kunden
        {
            get { return Kunden; }
            set { Kunden = value; }
        }

    }
}
