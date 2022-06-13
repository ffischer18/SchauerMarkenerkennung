using MarkenLib;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ExportViewOhrmarkenView.xaml
    /// </summary>
    public partial class ExportViewOhrmarkenView : UserControl
    {
        MarkenContext _db = new MarkenContext();
        public ExportViewOhrmarkenView()
        {
            InitializeComponent();
            TimeEntriesGrid();
        }

        private void TimeEntriesGrid()
        {
            List<Kunde> allCustomers = _db.Kunden.ToList();
            foreach (Kunde kunde in allCustomers)
            {
                List<Ohrmarke> allOhrmars = _db.Ohrmarken.Where(o => o.KundeId == kunde.Id).ToList();
                kunde.Ohrmarken = allOhrmars;
            }

            exportDataGrid.ItemsSource = _db.Kunden.Select(x => new ExportDataGrid
            {
                AdAdressId = x.AdAdressId,
                AdAdressNr = x.AdAdressNr,
                AdFirmenBezeichnung = x.AdFirmenBezeichnung,
                AdStrasse = x.AdStrasse,
                AdPostleitzahl = x.AdPostleitzahl,
                AdOrt = x.AdOrt,
                AdLandname = x.AdLandname,
                AdNationalitaetsKz = x.AdNationalitaetsKz,
                Markennummern = x.ohrmarken

            })
            .ToList();
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            string searchInput = Search.Text;
            if (Firmenbezeichnung.IsChecked == true)
            {
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }

                exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AdFirmenBezeichnung.Contains(searchInput)).Select(x => new ExportDataGrid
                {
                    AdAdressId = x.AdAdressId,
                    AdAdressNr = x.AdAdressNr,
                    AdFirmenBezeichnung = x.AdFirmenBezeichnung,
                    AdStrasse = x.AdStrasse,
                    AdPostleitzahl = x.AdPostleitzahl,
                    AdOrt = x.AdOrt,
                    AdLandname = x.AdLandname,
                    AdNationalitaetsKz = x.AdNationalitaetsKz,
                    Markennummern = x.ohrmarken

                })
            .ToList();
            }
            else if (PLZ.IsChecked == true)
            {
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AdPostleitzahl.Contains(searchInput)).Select(x => new ExportDataGrid
                {
                    AdAdressNr = x.AdAdressNr,
                    AdFirmenBezeichnung = x.AdFirmenBezeichnung,
                    AdStrasse = x.AdStrasse,
                    AdPostleitzahl = x.AdPostleitzahl,
                    AdOrt = x.AdOrt,
                    AdLandname = x.AdLandname,
                    AdNationalitaetsKz = x.AdNationalitaetsKz,
                    Markennummern = x.ohrmarken
                })
            .ToList();

            }
            else if (Adressnummer.IsChecked == true)
            {
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }

                try
                {
                    exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AdAdressNr == (int.Parse(searchInput))).Select(x => new ExportDataGrid
                    {
                        AdAdressNr = x.AdAdressNr,
                        AdFirmenBezeichnung = x.AdFirmenBezeichnung,
                        AdStrasse = x.AdStrasse,
                        AdPostleitzahl = x.AdPostleitzahl,
                        AdOrt = x.AdOrt,
                        AdLandname = x.AdLandname,
                        AdNationalitaetsKz = x.AdNationalitaetsKz,
                        Markennummern = x.ohrmarken
                    })
                .ToList();
                }
                catch
                {
                    return;
                }
            }
        }

        public void csv()
        {
            var csv = exportDataGrid.Items;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var csv = exportDataGrid.ItemsSource;

            List<Ohrmarke> list = new List<Ohrmarke>();
            foreach (var item in csv)
            {
                ExportOhrmarkenDataGrid ohrmarkeItem = (ExportOhrmarkenDataGrid)item;
                
                Ohrmarke ohrmarke = new Ohrmarke
                {
                    MarkenNummer = ohrmarkeItem.Markennummer,
                    Beschreibung = ohrmarkeItem.Beschreibung,
                    Datum = ohrmarkeItem.Datum,
                    Lieferant = ohrmarkeItem.Lieferant,
                    Markentyp = ohrmarkeItem.Markentyp,
                    
                };
                Kunde kundenname = _db.Kunden.Where(x => x.Id == ohrmarke.KundeId).FirstOrDefault();
                ohrmarke.Kunde = kundenname;
                
                list.Add(ohrmarke);
            }

            save(list);
        }

        public void save(List<Ohrmarke> lines)
        {
            var dlgSave = new SaveFileDialog
            {
                DefaultExt = "csv",
                FileName = "Export.csv",
                Filter = "CSV files|*.csv|All files|*.*",
                InitialDirectory = @"C:\Temp"
            };
            if (true != dlgSave.ShowDialog()) return;

            string filename = dlgSave.FileName;
            string header = "MarkenNummer;Beschreibung;Datum;Lieferant;Markentyp;Kundenname;Kundennummer";
            var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
            writer.WriteLine(header);

            foreach (var item in lines)
            {
                string kunde = item.KundeId + ";" + item.MarkenNummer + ";" + item.Beschreibung + ";" + item.Datum + ";" + item.Lieferant + ";" + item.Markentyp + ";" + item.Kunde.AdFirmenBezeichnung + ";" + item.Kunde.AdAdressNr;
                writer.WriteLine(kunde);
            }

            writer.Close();
        }


        public List<string> getString()
        {
            List<Kunde> k = _db.Kunden.Where(x => x.AdAdressNr == 0).Select(x => x).ToList();
            foreach (var kunde in k)
            {
                _db.Kunden.Remove(kunde);
                _db.SaveChanges();
            }
            List<string> strings = new List<string>();
            int count = exportDataGrid.Items.Count;



            var rows = GetDataGridRows(exportDataGrid);

            foreach (DataGridRow r in rows)
            {
                foreach (DataGridColumn column in exportDataGrid.Columns)
                {
                    if (column.GetCellContent(r) is TextBlock)
                    {
                        TextBlock cellContent = column.GetCellContent(r) as TextBlock;
                        strings.Add(cellContent.Text);
                    }
                }
            }



            return strings;

        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }
    }
}
