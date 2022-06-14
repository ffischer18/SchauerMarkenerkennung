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
    /// Interaction logic for ExportView.xaml
    /// </summary>
    public partial  class ExportView : UserControl
    {
        MarkenContext _db = new MarkenContext();
        public ExportView()
        {
            InitializeComponent();
            TimeEntriesGrid();
            Firmenbezeichnung.Foreground = new SolidColorBrush(Colors.White);
            PLZ.Foreground = new SolidColorBrush(Colors.White);
            Adressnummer.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TimeEntriesGrid()
        {
            List<Kunde> allCustomers = _db.Kunden.ToList();
            foreach(Kunde kunde in allCustomers)
            {
                List<Ohrmarke> allOhrmars = _db.Ohrmarken.Where(o => o.KundeAD_ADRESS_ID == kunde.AD_ADRESS_ID).ToList();
                kunde.Ohrmarken = allOhrmars;
            }
            
            exportDataGrid.ItemsSource = _db.Kunden.Select(x => new ExportDataGrid
            {
                AdAdressId = x.AD_ADRESS_ID,
                AdAdressNr = x.AD_ADRESS_NR,
                AdFirmenBezeichnung = x.AD_FIRMEN_BEZEICHNUNG, 
                AdStrasse = x.AD_STRASSE,
                AdPostleitzahl = x.AD_POSTLEITZAHL,
                AdOrt = x.AD_POSTLEITZAHL,
                AdLandname = x.AD_LANDNAME,
                AdNationalitaetsKz = x.AD_NATIONALITAETS_KZ,
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
               
                exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AD_FIRMEN_BEZEICHNUNG.Contains(searchInput)).Select(x => new ExportDataGrid
                {
                    AdAdressId = x.AD_ADRESS_ID,
                    AdAdressNr = x.AD_ADRESS_NR,
                    AdFirmenBezeichnung = x.AD_FIRMEN_BEZEICHNUNG,
                    AdStrasse = x.AD_STRASSE,
                    AdPostleitzahl = x.AD_POSTLEITZAHL,
                    AdOrt = x.AD_POSTLEITZAHL,
                    AdLandname = x.AD_LANDNAME,
                    AdNationalitaetsKz = x.AD_NATIONALITAETS_KZ,
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
                exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AD_POSTLEITZAHL.Contains(searchInput)).Select(x => new ExportDataGrid
                {
                    AdAdressNr = x.AD_ADRESS_NR,
                    AdFirmenBezeichnung = x.AD_FIRMEN_BEZEICHNUNG,
                    AdStrasse = x.AD_STRASSE,
                    AdPostleitzahl = x.AD_POSTLEITZAHL,
                    AdOrt = x.AD_POSTLEITZAHL,
                    AdLandname = x.AD_LANDNAME,
                    AdNationalitaetsKz = x.AD_NATIONALITAETS_KZ,
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
                    exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AD_ADRESS_NR == (int.Parse(searchInput))).Select(x => new ExportDataGrid
                    {
                        AdAdressNr = x.AD_ADRESS_NR,
                        AdFirmenBezeichnung = x.AD_FIRMEN_BEZEICHNUNG,
                        AdStrasse = x.AD_STRASSE,
                        AdPostleitzahl = x.AD_POSTLEITZAHL,
                        AdOrt = x.AD_POSTLEITZAHL,
                        AdLandname = x.AD_LANDNAME,
                        AdNationalitaetsKz = x.AD_NATIONALITAETS_KZ,
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

            List<Kunde> list = new List<Kunde>();
            foreach(var item in csv)
            {
               ExportDataGrid kundeItem = (ExportDataGrid)item;
                Kunde kunde = new Kunde
                {
                    AD_ADRESS_ID = kundeItem.AdAdressId,
                    AD_ADRESS_NR = kundeItem.AdAdressNr,
                    AD_FIRMEN_BEZEICHNUNG = kundeItem.AdFirmenBezeichnung,
                    AD_LANDNAME = kundeItem.AdLandname,
                    AD_ORT = kundeItem.AdOrt,
                    AD_NATIONALITAETS_KZ = kundeItem.AdNationalitaetsKz,
                    AD_POSTLEITZAHL = kundeItem.AdPostleitzahl,
                    AD_STRASSE = kundeItem.AdStrasse,
                };
               list.Add(kunde);
            }

            save(list);
        }

        public void save(List<Kunde> lines)
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
            string header = "AdAdressId;AdAdressNr;AdFirmenBezeichnung;AdStrasse;AdPostleitzahl;AdOrt;AdLandname;AdNationalitaetsKz";
            var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
            writer.WriteLine(header);

            foreach (var item in lines)
            {
                string kunde = item.AD_ADRESS_ID + ";" + item.AD_ADRESS_NR + ";" + item.AD_FIRMEN_BEZEICHNUNG + ";" + item.AD_STRASSE + ";" + item.AD_POSTLEITZAHL + ";" + item.AD_POSTLEITZAHL + ";" + item.AD_LANDNAME + ";" + item.AD_NATIONALITAETS_KZ;
                writer.WriteLine(kunde);
            }
            
            writer.Close();
        }

       
        public List<string> getString()
        {
            List<Kunde> k = _db.Kunden.Where(x => x.AD_ADRESS_NR == 0).Select(x => x).ToList();
            foreach(var kunde in k)
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

