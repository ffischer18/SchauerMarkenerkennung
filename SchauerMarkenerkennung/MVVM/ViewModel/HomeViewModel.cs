using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    public class HomeViewModel
    {
        MarkenContext _db = new MarkenContext();
        List<Kunde> KundenList = new List<Kunde>();

        public HomeViewModel()
        {
           // KundenList = _db.Kunden.Select(x => x).ToList();
            //Test = KundenList;
        }

        private List<Kunde> Test;

        public List<Kunde> test
        {
            get { return Test; }
            set { Test = value; }
        }


        public void filterKundenFirmaName()
        {
            List<Kunde> kundenFirmaNames = new List<Kunde>();
            foreach(var kunde in KundenList)
            {
                if (kunde.AdFirmenBezeichnung.Contains("a"))
                {
                    kundenFirmaNames.Add(kunde);
                }
            }
            KundenList = kundenFirmaNames;
        }


    }
}
