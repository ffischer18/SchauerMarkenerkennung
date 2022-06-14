using MarkenLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for ScanView.xaml
    /// </summary>
    public partial class ScanView : UserControl
    {

        MarkenContext _db = new MarkenContext();

        bool isStarted = false;
        public ScanView()
        {
            InitializeComponent();
            FillLbWithCustomers();
        }

        private void FillLbWithCustomers()
        {
            foreach (var kunde in _db.Kunden.ToList())
            {
                lbCustomers.Items.Add(new KundeDto
                {
                    AD_FIRMEN_BEZEICHNUNG = kunde.AD_FIRMEN_BEZEICHNUNG,
                    AD_ADRESS_ID = kunde.AD_ADRESS_ID,
                });
            }
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            isStarted = true;
            tbInput.IsEnabled = true;
            btnStop.IsEnabled = true;
            btnStart.IsEnabled = false;
        }

        private async void tbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isStarted && tbInput.Text.Length > 8)
            {
                await AddToListBox();
            }
        }

        private async Task<string> AddToListBox()
        {
            await Task.Delay(1000);
            if (tbInput.Text.Length > 8)
                lbNumbers.Items.Add(tbInput.Text);
            tbInput.Text = "";
            return tbInput.Text;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            isStarted = false;
            tbInput.IsEnabled = false;
            tbInput.Text = "";
            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            lbNumbers.Items.Remove(lbNumbers.SelectedItem);
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (CeckTextBoxes())
            {
                string customerId = lbCustomers.SelectedItem + "";
                string cusId = _db.Kunden.Where(x => x.AD_ADRESS_ID == customerId)
                    .Select(x => x.AD_ADRESS_ID)
                    .FirstOrDefault();

                foreach (string oNumber in lbNumbers.Items)
                {
                    var ohrmarke = new Ohrmarke
                    {
                        Beschreibung = tbBeschreibung.Text,
                        Datum = DateTime.Now,
                        KundeAD_ADRESS_ID = cusId,
                        MarkenNummer = oNumber,
                        Kommissionierer = tbBeschreibung.Text,
                        Markentyp = tbBeschreibung.Text
                    };
                    _db.Ohrmarken.Add(ohrmarke);
                }
                _db.SaveChanges();
                ClearTextBoxes();
            }
        }

        private void ClearTextBoxes()
        {
            tbBeschreibung.Text = "";
            tbLieferant.Text = "";
            tbInput.Text = "";
            tbType.Text = "";
            lbNumbers.Items.Clear();
            dgCustomerInfo.Visibility = Visibility.Hidden;
        }

        private bool CeckTextBoxes()
        {
            bool allFieldsFilled = true;
            if (String.IsNullOrEmpty(tbBeschreibung.Text))
            {
                lblPbeschreibung.Visibility = Visibility.Visible;
                allFieldsFilled = false;
            }
            else
            {
                lblPbeschreibung.Visibility = Visibility.Hidden;
                allFieldsFilled = true;
            }
            if (String.IsNullOrEmpty(tbLieferant.Text))
            {
                lblPlieferant.Visibility = Visibility.Visible;
                allFieldsFilled = false;
            }
            else
            {
                lblPlieferant.Visibility = Visibility.Hidden;
                allFieldsFilled = true;
            }
            if (String.IsNullOrEmpty(tbType.Text))
            {
                lblPtyp.Visibility = Visibility.Visible;
                allFieldsFilled = false;
            }
            else
            {
                lblPtyp.Visibility = Visibility.Hidden;
                allFieldsFilled = true;
            }
            if (lbNumbers.Items.Count < 1)
            {
                lblPnummern.Visibility = Visibility.Visible;
                allFieldsFilled = false;
            }
            else
            {
                lblPnummern.Visibility = Visibility.Hidden;
                allFieldsFilled = true;
            }
            if (lbCustomers.SelectedItem == null)
            {
                lblPKunden.Visibility = Visibility.Visible;
                allFieldsFilled = false;
            }
            else
            {
                lblPKunden.Visibility = Visibility.Hidden;
                allFieldsFilled = true;
            }

            return allFieldsFilled;
        }

        private void lbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selKunde = lbCustomers.SelectedItem as KundeDto;


            dgCustomerInfo.ItemsSource = _db.Kunden.Where(x => x.AD_ADRESS_ID == selKunde.AD_ADRESS_ID)
                .Select(x => new GridKunde
                {
                    AD_FIRMEN_BEZEICHNUNG = x.AD_FIRMEN_BEZEICHNUNG,
                    AD_STRASSE = x.AD_STRASSE,
                    AD_ORT = x.AD_ORT,
                    AD_POSTLEITZAHL = x.AD_POSTLEITZAHL
                }).ToList();

            dgCustomerInfo.Visibility = Visibility.Visible;
        }
    }
}
