using SchauerMarkenerkennung.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
    //MainViewModel ist dafür da um zwischen den einzelnen Ansichten wechseln zu können 
    public class MainViewModel : ObservableObject
    {
        
        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand ExportViewCommand { get; set; }

        public RelayCommand ScanViewCommand { get; set; }

        public RelayCommand NeuerKundeCommand { get; set; }

        public RelayCommand ExportOhrmarkenCommand { get; set; }


        public HomeViewModel HomeVm { get; set; }

        public ScanViewModel ScanVm { get; set; }
        public ExportViewModel ExportVm { get; set; }

        public NeuerKundeViewModel NeuerKundeVm { get; set; }

        public ExportOhrmarkenViewModel ExportOhrmarkenViewModel { get; set; }


        private object _currentView;
        //Current View sagt auch welche Ansicht gerade anzeigt wird/Angezeigt werden sollen
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVm = new HomeViewModel();
            CurrentView = HomeVm;
            ExportVm = new ExportViewModel();
            ScanVm = new ScanViewModel();
            NeuerKundeVm = new NeuerKundeViewModel();
            ExportOhrmarkenViewModel = new ExportOhrmarkenViewModel();
                 
        //Hier wird ein Command angelegt um die CurrrentView also um das aktuell geöffnete Fenster auf die Home Ansicht zu wechseln
        HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVm;
            });
        //Hier wird ein Command angelegt um die CurrrentView also um das aktuell geöffnete Fenster auf die ExportAnsicht der Ohrenmarken zu wechseln

            ExportOhrmarkenCommand = new RelayCommand(x =>
            {
                CurrentView = ExportOhrmarkenViewModel;
            });
            //Hier wird ein Command angelegt um die CurrrentView also um das aktuell geöffnete Fenster auf die ExportAnsicht der Kunden zu wechseln

            ExportViewCommand = new RelayCommand(x =>
            {
                CurrentView = ExportVm;
            });
            //Hier wird ein Command angelegt um die CurrrentView also um das aktuell geöffnete Fenster auf die  NeueMarkenScan Ansicht zu wechseln

            ScanViewCommand = new RelayCommand(x =>
            {
                CurrentView = ScanVm;
            });
            //Hier wird ein Command angelegt um die CurrrentView also um das aktuell geöffnete Fenster auf die NeueKunden Ansicht zu wechseln

            NeuerKundeCommand = new RelayCommand(x =>
            {
                CurrentView = NeuerKundeVm;
            });




        }
    }
}

