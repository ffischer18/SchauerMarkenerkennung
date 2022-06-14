using MarkenLib;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        MarkenContext _db = new MarkenContext();

        public HomeView()
        {
            InitializeComponent();
            fillListBoxOverViewWithCustomer();


        }

        public void fillListBoxOverViewWithCustomer()
        {
           Kunden.ItemsSource = _db.Kunden.Select(x => x).ToList();
        }

        

        private void Kunden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                Kunde selectedItem = null;
                selectedItem = Kunden.Items[Kunden.SelectedIndex] as Kunde;
                List<Ohrmarke> ohrmarke = _db.Ohrmarken.Where(x => x.KundeId == selectedItem.Id).ToList();
                foreach (var item in ohrmarke)
                {
                    _db.Ohrmarken.Remove(item);
                }
                _db.Kunden.Remove(selectedItem);
                _db.SaveChanges();

            Kunden.ItemsSource = _db.Kunden.ToList();
            
        }
    }
}
