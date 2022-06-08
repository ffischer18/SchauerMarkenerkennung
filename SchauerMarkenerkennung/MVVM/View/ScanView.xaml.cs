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
            lbCustomers.ItemsSource = _db.Kunden.Select(x => x.AdAdressNr).ToList();
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
    }
}
