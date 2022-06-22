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
    
    //Das UserControl HomeView ist dafür da um alle Marken mit ihrem zugehörigen Kunden anzuzeigen und einzelne Marken löschen zu können
    public partial class HomeView : UserControl
    {
        MarkenContext _db = new MarkenContext();

        public HomeView()
        {
            _db.Database.EnsureCreated();
            InitializeComponent();
            fillListBoxOverViewWithCustomer();
            Markennummer.Foreground =new SolidColorBrush(Colors.White);
            Firmenbezeichnung.Foreground =new SolidColorBrush(Colors.White);

        }

        //In der Methode fillListBoxOverViewWithCustomer werden alle Ohrenmarken mit ihrem zugehörigen Kunden in eine ListBox geschrieben
        
        public void fillListBoxOverViewWithCustomer()
        {
            foreach (var marke in _db.Ohrmarken.ToList())
            {
                string kundenName = _db.ST_ADRESSEN.Where(x=>x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x=>x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                Kunden.Items.Add(new OhrenmarkeDTO
                {
                    KundenName = kundenName,
                    Markennummer = marke.MarkenNummer
                });

            }
        }

        

       
        //Die Methode Button_Click wird verwendet um die ausgewählte Ohrenmarke löschen zu können
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Zuerst holt man sich hier das Selected Item und macht es zu einer OhrenmarkeDTO
            if (Kunden.SelectedIndex != -1)
            {
                OhrenmarkeDTO selectedItem = Kunden.Items[Kunden.SelectedIndex] as OhrenmarkeDTO;
                //Und hier wird dann das DTO in eine Ohrmarke verwandelt
                Ohrmarke markeToDelete = _db.Ohrmarken.Where(x => x.MarkenNummer == selectedItem.Markennummer).FirstOrDefault();

                _db.Ohrmarken.Remove(markeToDelete);
                _db.SaveChanges();


                //Nachdem die Ohrenmarke gelöscht wird wird die Anzeige gecleart und die Anzeige neu geladen
                Kunden.Items.Clear();
                if (Markennummer.IsChecked == true)
                {
                    if (Suche.Text == "")
                    {
                        foreach (var marke in _db.Ohrmarken.ToList())
                        {
                            string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                            Kunden.Items.Add(new OhrenmarkeDTO
                            {
                                KundenName = kundenName,
                                Markennummer = marke.MarkenNummer
                            });

                        }
                    }
                    else
                    {
                        foreach (var marke in _db.Ohrmarken.ToList())
                        {
                            string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                            if (marke.MarkenNummer.Contains(Suche.Text))
                            {
                                Kunden.Items.Add(new OhrenmarkeDTO
                                {
                                    KundenName = kundenName,
                                    Markennummer = marke.MarkenNummer
                                });
                            }

                        }
                    }
                }
                else if (Firmenbezeichnung.IsChecked == true)
                {
                    if (Suche.Text == "")
                    {
                        foreach (var marke in _db.Ohrmarken.ToList())
                        {
                            string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                            Kunden.Items.Add(new OhrenmarkeDTO
                            {
                                KundenName = kundenName,
                                Markennummer = marke.MarkenNummer
                            });

                        }
                    }
                    else
                    {
                        foreach (var marke in _db.Ohrmarken.ToList())
                        {
                            string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                            if (kundenName.ToUpper().Contains(Suche.Text.ToUpper()))
                            {
                                Kunden.Items.Add(new OhrenmarkeDTO
                                {
                                    KundenName = kundenName,
                                    Markennummer = marke.MarkenNummer
                                });
                            }

                            string a = "";
                        }
                    }
                }
                else
                {
                    fillListBoxOverViewWithCustomer();
                }
            }
           
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Kunden.Items.Clear();
            if (Markennummer.IsChecked==true)
            {
                if (Suche.Text == "")
                {
                    foreach (var marke in _db.Ohrmarken.ToList())
                    {
                        string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                        Kunden.Items.Add(new OhrenmarkeDTO
                        {
                            KundenName = kundenName,
                            Markennummer = marke.MarkenNummer
                        });

                        string a = "";
                    }
                }
                else
                {
                    foreach (var marke in _db.Ohrmarken.ToList())
                    {
                        string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                        if (marke.MarkenNummer.Contains(Suche.Text))
                        {
                        Kunden.Items.Add(new OhrenmarkeDTO
                        {
                            KundenName = kundenName,
                            Markennummer = marke.MarkenNummer
                        });
                        }

                    }
                }
            }else if (Firmenbezeichnung.IsChecked==true)
            {
                if (Suche.Text == "")
                {
                    foreach (var marke in _db.Ohrmarken.ToList())
                    {
                        string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                        Kunden.Items.Add(new OhrenmarkeDTO
                        {
                            KundenName = kundenName,
                            Markennummer = marke.MarkenNummer
                        });

                    }
                }
                else
                {
                    foreach (var marke in _db.Ohrmarken.ToList())
                    {
                        string kundenName = _db.ST_ADRESSEN.Where(x => x.AD_ADRESS_ID == marke.KundeAD_ADRESS_ID).Select(x => x.AD_FIRMEN_BEZEICHNUNG).FirstOrDefault();

                        if (kundenName.ToUpper().Contains(Suche.Text.ToUpper()))
                        {
                            Kunden.Items.Add(new OhrenmarkeDTO
                            {
                                KundenName = kundenName,
                                Markennummer = marke.MarkenNummer
                            });
                        }

                    }
                }
            }
        }
    }
}
