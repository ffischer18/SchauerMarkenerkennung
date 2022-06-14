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
        public ExportViewOhrmarkenView()
        {
            InitializeComponent();
            TimeEntriesGrid();
            Firmenbezeichnung.Foreground = new SolidColorBrush(Colors.White);
            Markentyp.Foreground = new SolidColorBrush(Colors.White);
            Datum.Foreground = new SolidColorBrush(Colors.White);
        }

        private void TimeEntriesGrid()
        {


            exportDataGrid.ItemsSource = _db.Ohrmarken.Select(x => new ExportOhrmarkenDataGrid
            {
                Kundenname = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                Kundennummer = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                Beschreibung = x.Beschreibung,
                Datum = x.Datum.ToString("dd.MM.yyyy"),
                Lieferant = x.Kommissionierer,
                Markennummer = x.MarkenNummer,
                Markentyp = x.Markentyp,
                AD_ADRESS_ID = x.KundeAD_ADRESS_ID

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

                exportDataGrid.ItemsSource = _db.Ohrmarken.Include(x => x.Kunde).Where(x => x.Kunde.AD_FIRMEN_BEZEICHNUNG.Contains(searchInput)).Select(x => new ExportOhrmarkenDataGrid
                {
                    Kundenname = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                    Kundennummer = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                    Beschreibung = x.Beschreibung,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Lieferant = x.Kommissionierer,
                    Markennummer = x.MarkenNummer,
                    Markentyp = x.Markentyp,
                    AD_ADRESS_ID = x.KundeAD_ADRESS_ID

                })
            .ToList();

            }
            else if (Markentyp.IsChecked == true)
            {
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                exportDataGrid.ItemsSource = _db.Ohrmarken.Where(x => x.Markentyp.Contains(searchInput)).Select(x => new ExportOhrmarkenDataGrid
                {
                    Kundenname = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                    Kundennummer = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
                    Beschreibung = x.Beschreibung,
                    Datum = x.Datum.ToString("dd.MM.yyyy"),
                    Lieferant = x.Kommissionierer,
                    Markennummer = x.MarkenNummer,
                    Markentyp = x.Markentyp,
                    AD_ADRESS_ID = x.KundeAD_ADRESS_ID
                })
            .ToList();

            }
            else if (Datum.IsChecked == true)
            {
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                List<Ohrmarke> ohrmarkes = _db.Ohrmarken.ToList();
                exportDataGrid.ItemsSource = _db.Ohrmarken.Where(x => x.Datum.ToString().Contains(searchInput)).Select(x => new ExportOhrmarkenDataGrid
                {
                    Kundenname = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault(),
                    Kundennummer = _db.Kunden.Where(p => p.AD_ADRESS_ID == x.KundeAD_ADRESS_ID).Select(p => p.AD_ADRESS_NR).FirstOrDefault().ToString(),
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var csv = exportDataGrid.ItemsSource;

            List<Ohrmarke> list = new List<Ohrmarke>();
            foreach (var item in csv)
            {
                ExportOhrmarkenDataGrid ohrmarkeItem = (ExportOhrmarkenDataGrid)item;

                Ohrmarke ohrmarke = new Ohrmarke
                {
                    KundeAD_ADRESS_ID = ohrmarkeItem.AD_ADRESS_ID,
                    MarkenNummer = ohrmarkeItem.Markennummer,
                    Beschreibung = ohrmarkeItem.Beschreibung,
                    Datum = DateTime.Parse(ohrmarkeItem.Datum),
                    Kommissionierer = ohrmarkeItem.Lieferant,
                    Markentyp = ohrmarkeItem.Markentyp,

                };


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
            string header = "KundenId;MarkenNummer;Beschreibung;Datum;Lieferant;Markentyp;Kundenname;Kundennummer";
            var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
            writer.WriteLine(header);

            foreach (var item in lines)
            {
                string name = _db.Kunden.Where(x => x.AD_ADRESS_ID == item.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();
                string kundennummer = _db.Kunden.Where(x => x.AD_ADRESS_ID == item.KundeAD_ADRESS_ID).Select(x => x.AD_ADRESS_NR).FirstOrDefault().ToString();
                string kunde = item.KundeAD_ADRESS_ID + ";" + item.MarkenNummer + ";" + item.Beschreibung + ";" + item.Datum + ";" + item.Kommissionierer + ";" + item.Markentyp + ";" + name + ";" + kundennummer;
                writer.WriteLine(kunde);
            }

            writer.Close();
        }


    }
}
