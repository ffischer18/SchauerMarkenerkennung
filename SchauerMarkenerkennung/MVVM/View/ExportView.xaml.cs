using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SchauerMarkenerkennung.MVVM.View
{
    /// <summary>
    /// Interaction logic for ExportView.xaml
    /// </summary>
    public partial  class ExportView : UserControl
    {
        MarkenContext _db = new MarkenContext();
        public ExportView()
        {
            InitializeComponent();
            TimeEntriesGrid();

        }

        private void TimeEntriesGrid()
        {
            exportDataGrid.ItemsSource = _db.Kunden.Select(x => new ExportDataGrid
            {
                Id = x.Id,
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
