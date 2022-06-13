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


        public ICommand AddCustomerCommand => new RelayCommand<string>(
            DoAddCustomer);

        private void DoAddCustomer(string obj)
        {
            _db.Kunden.Add(new Kunde
            {
                AdAdressId = AdAddressId,
                AdAdressNr = NewAdressNr,
                AdFirmenBezeichnung = NewCompanyDescription,
                AdStrasse = NewStreet,
                AdPostleitzahl = NewPLZ,
                AdOrt = NewCity,
                AdLandname = NewCountry,
                AdNationalitaetsKz = NewCountryKz
            });
            //_db.Kunden.Add(Kunde);
            _db.SaveChanges();
        }
    }
}