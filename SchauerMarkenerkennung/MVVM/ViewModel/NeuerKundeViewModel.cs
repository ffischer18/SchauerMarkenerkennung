using MarkenLib;
using MvvmTools;
using SchauerMarkenerkennung.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    //Die Klasse NeuerKundeViewModel ist dafür da um dafür zu sorgen neue Kunden hinzufügen zu können
    public class NeuerKundeViewModel : ObservableObject
    {
        private MarkenContext _db = new MarkenContext();

        public NeuerKundeViewModel()
        {

        }

        public NeuerKundeViewModel(MarkenContext db)
        {
            _db = db;
        }

        
        private int _newAdressNr = 0;
        public int NewAdressNr
        {
            get { return _newAdressNr; }
            set
            {
                _newAdressNr = value;
                OnPropertyChanged(nameof(_newAdressNr));
            }
        }

        private string _newCompanyDescription = "";
        public string NewCompanyDescription
        {
            get { return _newCompanyDescription; }
            set
            {
                _newCompanyDescription = value;
                OnPropertyChanged(nameof(_newCompanyDescription));
            }
        }

        private string _newStreet = "";
        public string NewStreet
        {
            get { return _newStreet; }
            set
            {
                _newStreet = value;
                OnPropertyChanged(nameof(_newStreet));
            }
        }

        private string _newPLZ = "";
        public string NewPLZ
        {
            get { return _newPLZ; }
            set
            {
                _newPLZ = value;
                OnPropertyChanged(nameof(_newPLZ));
            }
        }

        private string _newCity = "";
        public string NewCity
        {
            get { return _newCity; }
            set
            {
                _newCity = value;
                OnPropertyChanged(nameof(_newCity));
            }
        }

        private string _newCountry = "";
        public string NewCountry
        {
            get { return _newCountry; }
            set
            {
                _newCountry = value;
                OnPropertyChanged(nameof(_newCountry));
            }
        }

        private string _newCountryKz = "";
        public string NewCountryKz
        {
            get { return _newCountryKz; }
            set
            {
                _newCountryKz = value;
                OnPropertyChanged(nameof(_newCountryKz));
            }
        }

        private string _adAddressId = "";
        public string AdAddressId
        {
            get { return _adAddressId; }
            set
            {
                _adAddressId = value;
                OnPropertyChanged(nameof(_adAddressId));
            }
        }

        //Hier wird ein ICommand erstellt um den angelegten Kunden durch Button Click in die Datenbank zu speichern hierfür wird die Methode DoAddCustomer aufgerufen
        public ICommand AddCustomerCommand => new RelayCommand<string>(
            DoAddCustomer);

        //DoAddCustomer legt mit den neu eingegebenen Daten einen neuen Kunden an und speichert diesen in die Datenbank
        private void DoAddCustomer(string obj)
        {
            _db.ST_ADRESSEN.Add(new ST_ADRESSE
            {
                AD_ADRESS_ID = AdAddressId,
                AD_ADRESS_NR = NewAdressNr.ToString(),
                AD_FIRMEN_BEZEICHNUNG = NewCompanyDescription,
                AD_STRASSE = NewStreet,
                AD_POSTLEITZAHL = NewPLZ,
                AD_ORT = NewCity,
                AD_LANDNAME = NewCountry,
                AD_NATIONALITAETS_KZ = NewCountryKz
            });
            //_db.Kunden.Add(Kunde);
            _db.SaveChanges();
        }
    }
}