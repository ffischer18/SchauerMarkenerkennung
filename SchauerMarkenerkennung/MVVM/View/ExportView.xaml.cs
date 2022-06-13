﻿using MarkenLib;
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
            List<Kunde> allCustomers = _db.Kunden.ToList();
            foreach(Kunde kunde in allCustomers)
            {
                List<Ohrmarke> allOhrmars = _db.Ohrmarken.Where(o => o.KundeId == kunde.Id).ToList();
                kunde.Ohrmarken = allOhrmars;
            }
            
            exportDataGrid.ItemsSource = _db.Kunden.Select(x => new ExportDataGrid
            {
                Id = x.Id,
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
                    Id = kundeItem.Id,
                    AdAdressNr = kundeItem.AdAdressNr,
                    AdFirmenBezeichnung = kundeItem.AdFirmenBezeichnung,
                    AdLandname = kundeItem.AdLandname,
                    AdOrt = kundeItem.AdOrt,
                    AdNationalitaetsKz = kundeItem.AdNationalitaetsKz,
                    AdPostleitzahl = kundeItem.AdPostleitzahl,
                    AdStrasse = kundeItem.AdStrasse,
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
            string header = "Id;AdAdressNr;AdFirmenBezeichnung;AdStrasse;AdPostleitzahl;AdOrt;AdLandname;AdNationalitaetsKz";
            var writer = new StreamWriter(filename, false, Encoding.GetEncoding("ISO-8859-1"));
            writer.WriteLine(header);

            foreach (var item in lines)
            {
                string kunde = item.Id + ";" + item.AdAdressNr + ";" + item.AdFirmenBezeichnung + ";" + item.AdStrasse + ";" + item.AdPostleitzahl + ";" + item.AdOrt + ";" + item.AdLandname + ";" + item.AdNationalitaetsKz;
                writer.WriteLine(kunde);
            }
            
            writer.Close();
        }

       
        public List<string> getString()
        {
            List<Kunde> k = _db.Kunden.Where(x => x.AdAdressNr == 0).Select(x => x).ToList();
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
                    //   DataRowView rv = (DataRowView)r.Item;
                    foreach (DataGridColumn column in exportDataGrid.Columns)
                    {
                        if (column.GetCellContent(r) is TextBlock)
                        {
                            TextBlock cellContent = column.GetCellContent(r) as TextBlock;
                           // if (!strings.Contains(cellContent.Text))
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

