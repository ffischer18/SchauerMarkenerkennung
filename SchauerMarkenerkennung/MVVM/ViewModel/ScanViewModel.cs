using MarkenLib;
using SchauerMarkenerkennung.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    public class ScanViewModel : ObservableObject
    {
        bool isStarted = true;
        //MarkenContext _db = new MarkenContext();

        //List<Kunde> testList = new List<Kunde>();
        //public ScanViewModel()
        //{
        //    testList = _db.Kunden.Select(x => x).ToList();
        //}

        private List<string> _markenNummern = new();

        public List<string> MarkenNummern
        {
            get { return _markenNummern; }
            set
            {
                _markenNummern = value;
                OnPropertyChanged(nameof(MarkenNummern));
            }
        }

        private string _markenNr = "";
        public string MarkenNr
        {
            get { return _markenNr; }
            set
            { 
                _markenNr = value;
                if(value.Length > 8)
                CheckForInput();
                OnPropertyChanged(nameof(_markenNr));

            }
        }
        
        private async void CheckForInput()
        {
            if (isStarted && MarkenNr.Length > 8)
            {
                await AddToListBox();
            }
        }

        private async Task<string> AddToListBox()
        {
            await Task.Delay(1000);
            if (MarkenNr.Length > 8)
                MarkenNummern.Add(MarkenNr);
            MarkenNr = "";
            return "";
        }

    }
}
