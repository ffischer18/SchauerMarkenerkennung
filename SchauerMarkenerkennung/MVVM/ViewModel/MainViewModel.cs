﻿using SchauerMarkenerkennung.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchauerMarkenerkennung.MVVM.ViewModel
{
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
                 

        HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = HomeVm;
            });

            ExportOhrmarkenCommand = new RelayCommand(x =>
            {
                CurrentView = ExportOhrmarkenViewModel;
            });

                        ExportViewCommand = new RelayCommand(x =>
            {
                CurrentView = ExportVm;
            });

            ScanViewCommand = new RelayCommand(x =>
            {
                CurrentView = ScanVm;
            });
            NeuerKundeCommand = new RelayCommand(x =>
            {
                CurrentView = NeuerKundeVm;
            });




        }
    }
}

