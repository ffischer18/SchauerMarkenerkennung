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
            else if (PLZ.IsChecked == true)
            {
                if (searchInput == "")
                {
                    TimeEntriesGrid();
                    return;
                }
                exportDataGrid.ItemsSource = _db.Kunden.Where(x => x.AdPostleitzahl.Contains(searchInput)).Select(x => new ExportDataGrid
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
                catch
                {
                    return;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (exportDataGrid.Items.Count > 0)
            {

                List<string> celles = getString();
                List<string> actualLines = new List<string>();
                string lineA = "";
                foreach (string line in celles)
                {
                    lineA = lineA + ";" + line;
                }


                save(lineA);

            }
        }

        public void save(string line)
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
            var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
            writer.WriteLine(line);
            writer.Close();
        }

        /*
       private void MenuSave_Click(object sender, RoutedEventArgs e)
    {
      var dlgSave = new SaveFileDialog
      {
        DefaultExt = "csv",
        FileName = "persons.csv",
        Filter = "CSV files|*.csv|All files|*.*",
        InitialDirectory = @"C:\Temp"
      };
      if (true != dlgSave.ShowDialog()) return;

      string filename = dlgSave.FileName;
      var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
      foreach (Person person in lstNames.Items)
      {
        writer.WriteLine(person.AsCsvString());
      }
      writer.Close();
    }
         
         
        */
        public List<string> getString()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < exportDataGrid.Items.Count; i++)
            {
                string line = exportDataGrid.Items[i].ToString();
                Console.WriteLine(line);

                var rows = GetDataGridRows(exportDataGrid);

                foreach (DataGridRow r in rows)
                {
                    //   DataRowView rv = (DataRowView)r.Item;
                    foreach (DataGridColumn column in exportDataGrid.Columns)
                    {
                        if (column.GetCellContent(r) is TextBlock)
                        {
                            TextBlock cellContent = column.GetCellContent(r) as TextBlock;
                            if (!strings.Contains(cellContent.Text))
                                strings.Add(cellContent.Text);
                        }
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

