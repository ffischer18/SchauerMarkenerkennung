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
        List<ST_ADRESSE> KundenList = new List<ST_ADRESSE>();

        public HomeViewModel()
        {
           // KundenList = _db.Kunden.Select(x => x).ToList();
            //Test = KundenList;
        }

        private List<ST_ADRESSE> Test;

        public List<ST_ADRESSE> test
        {
            get { return Test; }
            set { Test = value; }
        }


        public void filterKundenFirmaName()
        {
            List<ST_ADRESSE> kundenFirmaNames = new List<ST_ADRESSE>();
            foreach(var kunde in KundenList)
            {
                if (kunde.AD_FIRMEN_BEZEICHNUNG.Contains("a"))
                {
                    kundenFirmaNames.Add(kunde);
                }
            }
            KundenList = kundenFirmaNames;
        }


    }
}
