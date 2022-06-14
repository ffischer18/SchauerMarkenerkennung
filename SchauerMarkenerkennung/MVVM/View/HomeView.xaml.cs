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
           
            foreach (var marke in _db.Ohrmarken.ToList())
            {
                string kundenName = _db.Kunden.Where(x=>x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x=>x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                Kunden.Items.Add(new OhrenmarkeDTO
                {
                    KundenName = kundenName,
                    Markennummer = marke.MarkenNummer
                });

                string a = "";
            }
        }

        

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OhrenmarkeDTO selectedItem = Kunden.Items[Kunden.SelectedIndex] as OhrenmarkeDTO;
            Ohrmarke marke = _db.Ohrmarken.Where(x=>x.MarkenNummer == selectedItem.Markennummer).FirstOrDefault();
           
            _db.Ohrmarken.Remove(marke);
            
            
            _db.SaveChanges();

            Kunden.Items.Clear();
            fillListBoxOverViewWithCustomer();
        }
    }
}
