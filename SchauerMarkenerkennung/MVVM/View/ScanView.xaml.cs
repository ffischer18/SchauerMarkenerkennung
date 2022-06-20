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
    public partial class ScanView : UserControl
    {

        MarkenContext _db = new MarkenContext();

        //Dient zur Überprüfung ob man das Textfeld zur Eingabe der Markennummern aktiviert hatt.
        bool isStarted = false;
        public ScanView()
        {
            InitializeComponent();
            FillLbWithCustomers();
        }

        //Füllt Combobox der Customer mit Kunden Dtos. Diese werden gebraucht, um Id zwischenzuspeicher
        //um später Ohrmarken auf den selectieren Kunden zuweisen zu können.
        private void FillLbWithCustomers()
        {
            foreach (var kunde in _db.ST_ADRESSEN.ToList())
            {
                lbCustomers.Items.Add(new KundeDto
                {
                    AD_FIRMEN_BEZEICHNUNG = kunde.AD_FIRMEN_BEZEICHNUNG,
                    AD_ADRESS_ID = kunde.AD_ADRESS_ID,
                });
            }
        }

        //Aktiviert die Eingabe für Markennummern. Benötigte Objekte werden aktiviert/deaktiviert
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            isStarted = true;
            tbInput.IsEnabled = true;
            btnStop.IsEnabled = true;
            btnStart.IsEnabled = false;
        }

        //Wird aufgerufen wenn man Markennummer in Textfeld gibt.
        private async void tbInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isStarted && tbInput.Text.Length > 8)
            {
                //Ruft Task auf, der diese dann nach kurzer Wartezeit in die Listbox überträgt und Textfäld cleared.
                await AddToListBox();
            }
        }

        //Task der in der obrigen Methode aufgerufen wird. Dient zur Übertragen der Markennummern in Listbox
        private async Task<string> AddToListBox()
        {
            await Task.Delay(1000);
            if (tbInput.Text.Length > 8)
                lbNumbers.Items.Add(tbInput.Text);
            tbInput.Text = "";
            return tbInput.Text;
        }

        //PreviewTextInput des Textfeldes im XAML ruft diese Methode auf. Dient dazu, dass ausschließlich
        //Zahlen und keine Buchstaben in Textfeld eingegeben werden können.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        //Dient zum Stoppen der Eingabe für Marken. Aktiviert/Deaktiviert benötigte Felder/Buttons.
        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
            isStarted = false;
            tbInput.IsEnabled = false;
            tbInput.Text = "";
            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        //Löscht ausgewählte Markennummer aus Listbox
        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            lbNumbers.Items.Remove(lbNumbers.SelectedItem);
        }

        //Speichert alle Ohrmarken in der Listbox in die Datenbank
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            //Funktion CheckTextBoxes prüft ob alle benötigen Felder einen Inhalt haben
            if (CeckTextBoxes())
            {   
                //holt ausgewähltes Kunden Dto aus der Combobox für Kunden
                KundeDto kunde = lbCustomers.SelectedItem as KundeDto;

                //Kunden ID, prüft ob diese null ist
                string kundId = "";
                if (kunde != null)
                {
                    kundId = kunde.AD_ADRESS_ID;
                }

                //Geht alle Nummern in der Listbox durch und Speichert diese mitsamt den benötigten
                //Daten in die Datenbank
                foreach (string oNumber in lbNumbers.Items)
                {
                    var ohrmarke = new Ohrmarke
                    {
                        Beschreibung = tbBeschreibung.Text,
                        Datum = DateTime.Now,
                        KundeAD_ADRESS_ID = kundId,
                        MarkenNummer = oNumber,
                        Kommissionierer = tbKommisionier.Text,
                        Markentyp = tbType.Text
                    };
                    _db.Ohrmarken.Add(ohrmarke);
                }
                _db.SaveChanges();

                //Methode wird nach erfolgreichem Speichern aufgerufen um Textfelder und Listbox zu leeren
                ClearTextBoxes();
            }
        }

        //Leert Textfelder und Listbox, versteck die Customerinformation wieder
        private void ClearTextBoxes()
        {
            tbBeschreibung.Text = "";
            tbKommisionier.Text = "";
            tbInput.Text = "";
            tbType.Text = "";
            lbNumbers.Items.Clear();
            dgCustomerInfo.Visibility = Visibility.Hidden;
        }

        //Überprüft ob Textfelder leer sind. Ist dass der Fall, wird ein Hinweis eingeblendet,
        //dass man etwas in dieses Feld eintragen muss. Wenn Feld bereits ausgefüllt ist, wird
        //dieser Hinweis versteckt
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
            if (String.IsNullOrEmpty(tbKommisionier.Text))
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

        //Reagiert auf neues Auswählen eines Kunden in der Datenbank und zeig anschließend mehr Information dazu an
        private void lbCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selKunde = lbCustomers.SelectedItem as KundeDto;

            //Information wird in ein Datagrid eingetragen. Dazu wird eine eigene Klasse GridKunde verwendet. Diese
            //beinhaltet lediglich die benötigten Informationen
            dgCustomerInfo.ItemsSource = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == selKunde.AD_ADRESS_ID)
                .Select(x => new GridKunde
                {
                    AD_FIRMEN_BEZEICHNUNG = x.AD_FIRMEN_BEZEICHNUNG,
                    AD_STRASSE = x.AD_STRASSE,
                    AD_ORT = x.AD_ORT,
                    AD_POSTLEITZAHL = x.AD_POSTLEITZAHL
                }).ToList();

            //Information wird Eingeblendet
            dgCustomerInfo.Visibility = Visibility.Visible;
        }
    }
}
