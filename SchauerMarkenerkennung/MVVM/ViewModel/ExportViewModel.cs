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
    public class ExportViewModel : ObservableObject
    {
        MarkenContext _db = new MarkenContext();

        public ExportViewModel()
        {

        }
        public ExportViewModel(MarkenContext db)
        {
            _db = db;
            TimeEntriesGrid();
        }

        private List<ExportDataGrid> exportDataGrid;
        public List<ExportDataGrid> ExportDataGrid
        {
            get { return exportDataGrid; }
            set
            {
                exportDataGrid = value;
                OnPropertyChanged(nameof(ExportDataGrid));
            }
        }


        private void TimeEntriesGrid()
        {
            exportDataGrid = _db.Kunden.Select(x => new ExportDataGrid
            {
                AdAdressNr = x.AD_ADRESS_NR,
                AdFirmenBezeichnung = x.AD_FIRMEN_BEZEICHNUNG,
                AdStrasse = x.AD_STRASSE,
                AdPostleitzahl = x.AD_POSTLEITZAHL,
                AdOrt = x.AD_POSTLEITZAHL,
                AdLandname = x.AD_LANDNAME,
                AdNationalitaetsKz = x.AD_NATIONALITAETS_KZ
            })
            .ToList();
        }
    }
}
