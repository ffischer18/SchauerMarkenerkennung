using MarkenLib;
using SchauerMarkenerkennung.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    public class NeuerKundeViewModel : ObservableObject
    {
        MarkenContext _db = new MarkenContext();
        public RelayCommand NeuerKundeCommand { get; set; }

        List<Kunde> testList = new List<Kunde>();
        public NeuerKundeViewModel()
        {
            //testList = _db.Kunden.Select(x => x).ToList();
            Test = testList;
        }

        private List<Kunde> Test;

        public List<Kunde> test
        {
            get { return Test; }
            set { Test = value; }
        }


        private List<Kunde> Kunden;

        public List<Kunde> kunden
        {
            get { return Kunden; }
            set { Kunden = value; }
        }

        /*
        public void addButtonClicked()
        {
            NeuerKundeCommand =  new RelayCommand(x =>
            {
                
                Kunde newKunde = new Kunde
                {
                    Id = 1,
                    AdFirmenBezeichnung = selectedOdSupplier,
                    AdAdressId = "a",
                    AdAdressNr = "59",
                    AdLandname = "Österreich",
                    AdNationalitaetsKz = "AT",
                    AdOrt="Ried im Innkries",
                    AdStrasse = "Am Pfarrgrund",
                    AdPostleitzahl="4910",
                    
                    
                };

                Ohrmarke newOhrmarke = new Ohrmarke
                {
                    Beschreibung = "Wurde von Kunde gekauft",
                    Datum =DateTime.Now,
                    Id = 1,
                    Kunde = newKunde,
                    KundenId = "1",
                };
                
                

                _db.Kunden.Add(newKunde);
                _db.SaveChanges();

                
            });
                
        }
*/

        private String selectedOdSupplier;

        public String SelectedOdSupplier
        {
            get { return selectedOdSupplier; }
            set
            {
                selectedOdSupplier = value;
                OnPropertyChanged();  
            }
        }

    }
}
