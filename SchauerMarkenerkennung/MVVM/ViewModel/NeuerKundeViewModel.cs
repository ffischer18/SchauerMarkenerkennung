using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    public class NeuerKundeViewModel
    {
        MarkenContext _db = new MarkenContext();

        List<Kunde> testList = new List<Kunde>();
        public NeuerKundeViewModel()
        {
            testList = _db.Kunden.Select(x => x).ToList();
        }

        private List<Kunde> Kunden;

        public List<Kunde> kunden
        {
            get { return Kunden; }
            set { Kunden = value; }
        }

       // public ICommand AddKundeCommand => new RelayCommand<string>(

         //   );

    }
}
