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
        //In der Methode TimeEntriesGrid werden alle Kunden mit ihren zugehörigen Daten geladen
        private void TimeEntriesGrid()
        {
            //Hier werden alle Ohrenmarken eines kunden rausgesucht und dem Kunden zugewiesen
            List<ST_ADRESSE> allCustomers = _db.ST_ADRESSEN.ToList();
            foreach(ST_ADRESSE kunde in allCustomers)
            {
                List<Ohrmarke> allOhrmars = _db.Ohrmarken.Where(o => o.KundeAD_ADRESS_ID == kunde.AD_ADRESS_ID).ToList();
                kunde.Ohrmarken = allOhrmars;
            }
            //Hier wird dann das DataGrid mit den Kunden befüllt, indem man aus jedem Kunden in der Datenbank ein ExportDataGrid macht
            exportDataGrid.ItemsSource = _db.ST_ADRESSEN.Select(x => new ExportDataGrid
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


        // In der Methode Search_KeyUp Methode werden die Filtermöglichkeiten programmiert
        //Es ist Möglich nach der Firmenbezeichnung zu suchen, nach der PLZ und nach der genauen Addressnummer
        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            // searchInput is der Text der im Suchfeld eingegeben wird
            string searchInput = Search.Text;
            //Hier wird die Möglichkeit nach der Firmenbezeichnung zu Filtern
            if (Firmenbezeichnung.IsChecked == true)
            {
                //Wenn der RadioButton Firmenbezeichnung ausgwählt ist aber der SearchInput Text leer is soll das DataGrid
                //mit allen Kunden befüllt werden ohne Filtereinstellung
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
               
                //falls jedoch ein Text vorhanden ist wird die Datenbank durchsucht ob Kunden mit passender Firmenbezeichnung vorhanden sind
                //wenn ja wird das DataGrid mit diesen Kunden befüllt
                exportDataGrid.ItemsSource = _db.ST_ADRESSEN.Where(x => x.AD_FIRMEN_BEZEICHNUNG.Contains(searchInput)).Select(x => new ExportDataGrid
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
            //Wenn der Radiobutton PLZ angeklickt ist wird nach Kunden mit der eingegebenen PLZ gesucht
            else if (PLZ.IsChecked == true)
            {
                //Falls der RadioButton angeklickt ist jedoch kein Text vorhanden ist nachdem gesucht werden soll wird
                //das DataGrid wieder mit allen Kunden ohne Filtereinstellung befüllt
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                //Wenn jedoch ein Text vorhanden ist wird hier das DataGrid mit den passenden Kunden befüllt
                exportDataGrid.ItemsSource = _db.ST_ADRESSEN.Where(x => x.AD_POSTLEITZAHL.Contains(searchInput)).Select(x => new ExportDataGrid
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
            //Wenn der Radiobutton Adressnummer angeklickt ist wird nach Kunden mit der eingegebenen Adressnummber gesucht
            else if (Adressnummer.IsChecked == true)
            {
                //Falls der RadioButton angeklickt ist jedoch kein Text vorhanden ist nachdem gesucht werden soll wird
                //das DataGrid wieder mit allen Kunden ohne Filtereinstellung befüllt
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                
                try
                {
                    exportDataGrid.ItemsSource = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_NR == searchInput).Select(x => new ExportDataGrid
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
            //hier werden alle Einträge des DataGrid´s in eine Liste gespeichert
            var csv = exportDataGrid.ItemsSource;

            List<ST_ADRESSE> list = new List<ST_ADRESSE>();
            //hier werden alle Einträge des DataGrid´s durchgeschaut und anschließend ein ExportDataGrid Item erzeugt
            foreach(var item in csv)
            {
                //aus jedem item in der Loste wird ein ExportDataGrid item gemacht und anschließend ein neuer Kunde erstellt
                //mit den Daten die im Grid eingetragen sind
               ExportDataGrid kundeItem = (ExportDataGrid)item;
                ST_ADRESSE kunde = new ST_ADRESSE
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
                //Diese neu angelegeten Kunden werden dann in eine Liste gepspeichert
               list.Add(kunde);
            }
            //Anschließend wird diese Liste der Methode save mitgegben um sie in eine CSV-Datei zu speichern
            save(list);
        }

        //Die Methode save sorgt dafür  das die im DataGrid angezeigten Daten in eine CSV-Datei gespeichert werden
        public void save(List<ST_ADRESSE> lines)
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
            //In der foreach werden alle Kunden durchgegangen und aus ihnen eine Zeile gemacht die in die CSV Datei geschrieben wird
            foreach (var item in lines)
            {
                string kunde = item.AD_ADRESS_ID + ";" + item.AD_ADRESS_NR + ";" + item.AD_FIRMEN_BEZEICHNUNG + ";" + item.AD_STRASSE + ";" + item.AD_POSTLEITZAHL + ";" + item.AD_POSTLEITZAHL + ";" + item.AD_LANDNAME + ";" + item.AD_NATIONALITAETS_KZ;
                writer.WriteLine(kunde);
            }
            
            writer.Close();
        }

       
        public List<string> getString()
        {
            List<ST_ADRESSE> k = _db.ST_ADRESSEN.Where(x => Int32.Parse(x.AD_ADRESS_NR) == 0).Select(x => x).ToList();
            foreach(var kunde in k)
            {
                _db.ST_ADRESSEN.Remove(kunde);
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

