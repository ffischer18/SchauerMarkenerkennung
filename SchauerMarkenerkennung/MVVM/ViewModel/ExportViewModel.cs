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
                AdAdressNr = x.AdAdressNr,
                AdFirmenBezeichnung = x.AdFirmenBezeichnung,
                AdStrasse = x.AdStrasse,
                AdPostleitzahl = x.AdPostleitzahl,
                AdOrt = x.AdOrt,
                AdLandname = x.AdLandname,
                AdNationalitaetsKz = x.AdNationalitaetsKz
            })
            .ToList();
        }
    }
}
