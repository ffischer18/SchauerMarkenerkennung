using MarkenLib;
using Microsoft.EntityFrameworkCore;
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
        //Hier im Konstruktor werden die Farben für den Text der RadioButtons verändert
        public ExportViewOhrmarkenView()
        {
            InitializeComponent();
            TimeEntriesGrid();
            Firmenbezeichnung.Foreground = new SolidColorBrush(Colors.White);
            Markentyp.Foreground = new SolidColorBrush(Colors.White);
            Datum.Foreground = new SolidColorBrush(Colors.White);
        }

        //TimeEntriesGrid befüllt das Datagrid mit der Anischt von den ganzen Marken mit ihrem zugehörigen Kunden
        private void TimeEntriesGrid()
        {
            exportDataGrid.ItemsSource = _db.Ohrmarken.Select(x => new ExportOhrmarkenDataGrid
            {
                Kundenname = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                Kundennummer = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                Beschreibung = x.Beschreibung,
                Datum = x.Datum.ToString("dd.MM.yyyy"),
                Lieferant = x.Kommissionierer,
                Markennummer = x.MarkenNummer,
                Markentyp = x.Markentyp,
                AD_ADRESS_ID = x.KundeAD_ADRESS_ID

            })
            .ToList();
        }

        /*In Search_KeyUp wird wird die Filter Möglichkeit im Fenster wo die Kunden angezeigt werden programmiert. 
         * Es gibt einmal die Möglichkeit nach der Firmenbezeichnung zu filtern nach dem Markentyp und nach dem Auftragsdatum.
         * Diese Möglichten kann man auswählen indem man auf den gewünschten RadioButton klickt und dann im Textfeld daneben die
         * gewünschte suche eingibt.
         * 
         */
        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            string searchInput = Search.Text;
            //Hier wird die Firmenbezeichnung Filter Möglichkeit programmiert
            if (Firmenbezeichnung.IsChecked == true)
            {
                //wenn das searchInput Feld leer ist sollen wieder alle Einträge geladen ohne eine Filtereinstellung
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                //hier wird dann in der Datenbank nach Ohrmarken mit der zu suchenden Firmenbezeichnung gefiltert
                exportDataGrid.ItemsSource = _db.Ohrmarken.Include(x => x.Kunde).Where(x => x.Kunde.AD_FIRMEN_BEZEICHNUNG.Contains(searchInput)).Select(x => new ExportOhrmarkenDataGrid
                {
                    Kundenname = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                    Kundennummer = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                    Beschreibung = x.Beschreibung,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Lieferant = x.Kommissionierer,
                    Markennummer = x.MarkenNummer,
                    Markentyp = x.Markentyp,
                    AD_ADRESS_ID = x.KundeAD_ADRESS_ID

                })
            .ToList();

            }
            //hier wird die Markentyp Filterfunktion programmiert
            //Wenn der RadioButton Markentyp markiert ist wird nach dem Markentyp gesucht
            else if (Markentyp.IsChecked == true)
            {
                //wenn das searchInput Feld leer ist sollen wieder alle Einträge geladen ohne eine Filtereinstellung
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                //Hier werden die Ohrmarken mit der gesuchten Markenbezeichnung aus der Datenbank gefiltert und darauf hin wird das DataGrid 
                //mit den passenden Marken gefüllt
                exportDataGrid.ItemsSource = _db.Ohrmarken.Where(x => x.Markentyp.Contains(searchInput)).Select(x => new ExportOhrmarkenDataGrid
                {
                    Kundenname = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                    Kundennummer = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                    Beschreibung = x.Beschreibung,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Lieferant = x.Kommissionierer,
                    Markennummer = x.MarkenNummer,
                    Markentyp = x.Markentyp,
                    AD_ADRESS_ID = x.KundeAD_ADRESS_ID
                })
            .ToList();

            }
            //hier wird die Suchfunktion nach dem Datum programmiert
            //Wenn der RadioButton Datum angeklick wurde, wird nach dem richtigen Datum gesucht
            else if (Datum.IsChecked == true)
            {
                //wenn das searchInput Feld leer ist sollen wieder alle Einträge geladen ohne eine Filtereinstellung
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                List<Ohrmarke> ohrmarkes = _db.Ohrmarken.ToList();
                //Hier wird geschaut ob es Einträge gibt wo das Datum dem Text Text entspricht nach dem zu suchen ist
                exportDataGrid.ItemsSource = _db.Ohrmarken.Where(x => x.Datum.ToString().Contains(searchInput)).Select(x => new ExportOhrmarkenDataGrid
                {
                    Kundenname = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                    Kundennummer = _db.ST_ADRESSEN.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                    Beschreibung = x.Beschreibung,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Lieferant = x.Kommissionierer,
                    Markennummer = x.MarkenNummer,
                    Markentyp = x.Markentyp,
                    AD_ADRESS_ID = x.KundeAD_ADRESS_ID
                })
            .ToList();
            }

        }

        public void csv()
        {
            var csv = exportDataGrid.Items;
        }
        //Diese Button Clicked Methode wird geklickt wenn ein Export als CSV gemacht werden soll
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Hier werden alle Einträge des DataGrid in eine Liste  gespeichert
            var csv = exportDataGrid.ItemsSource;
            
            List<Ohrmarke> list = new List<Ohrmarke>();
            //Mit der foreach werden alle Einträge durchgegangen und in eine Klasse ExportOhrnmarkenDataGrid geparst
            foreach (var item in csv)
            {
                ExportOhrmarkenDataGrid ohrmarkeItem = (ExportOhrmarkenDataGrid)item;
                //Hier wird eine Ohrenmarke erstellt mit den im Grid stehenden Daten 
                Ohrmarke ohrmarke = new Ohrmarke
                {
                    KundeAD_ADRESS_ID = ohrmarkeItem.AD_ADRESS_ID,
                    MarkenNummer = ohrmarkeItem.Markennummer,
                    Beschreibung = ohrmarkeItem.Beschreibung,
                    Datum = DateTime.Parse(ohrmarkeItem.Datum),
                    Kommissionierer = ohrmarkeItem.Lieferant,
                    Markentyp = ohrmarkeItem.Markentyp,

                };

                //die erstellte Ohrmarke wird dann in eine Liste gespeichert
                list.Add(ohrmarke);
            }
            //die Liste mit den Ohrmarken wird dann der Methode save mitgegeben
            save(list);
        }
        //die Methode save ist dafür zuständig die übergebenen Ohrmarken in eine CSV Datei zu schreiben
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
            string header = "KundenId;MarkenNummer;Beschreibung;Datum;Lieferant;Markentyp;Kundenname;Kundennummer";
            var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
            writer.WriteLine(header);
            //Mit dieser foreach wird dann die übergebenen Liste durchgegangen und mit jedem item eine zeile in die CSV geschrieben
            foreach (var item in lines)
            {
                string name = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == item.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();
                string kundennummer = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == item.KundeAD_ADRESS_ID).Select(x => x.AD_ADRESS_NR).FirstOrDefault().ToString();
                string kunde = item.KundeAD_ADRESS_ID + ";" + item.MarkenNummer + ";" + item.Beschreibung + ";" + item.Datum + ";" + item.Kommissionierer + ";" + item.Markentyp + ";" + name + ";" + kundennummer;
                writer.WriteLine(kunde);
            }

            writer.Close();
        }


    }
}
