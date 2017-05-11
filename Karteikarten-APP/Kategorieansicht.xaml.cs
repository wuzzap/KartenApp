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
using System.Windows.Shapes;

namespace Karteikarten_APP
{

    class MyButton : Button
    {
        public int field;

        public MyButton(int i) : base()
        {
            this.field = i;
        }

    }
    
    public partial class Kategorieansicht : Window
    {
        public static sqlkram sqq = new sqlkram();
        // public static int StapelID;
        private bool firstRun = true;

        public Kategorieansicht()
        {
            InitializeComponent();
            showCategories(0);
        }

       

        private void oeffneStapel(object sender, RoutedEventArgs e)
        {
            int i = ((MyButton)sender).field;
            showCategories(i);
        }
        
        private void Button_Click_Beenden(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_NeueKategorie(object sender, RoutedEventArgs e)
        {
            var wnd = new NeueKategorie();
            wnd.Show();
        }

        private void Button_Click_NeueKarte(object sender, RoutedEventArgs e)
        {
            var wnd = new NeueKarte();
            wnd.Show();
        }


        private void showCategories(int i)
        {
            if (!sqq.HatUnter(i) && firstRun == false)
            {
                if (sqq.hatKarten(i))
                {
                    var wnd = new Kartenansicht(i);
                    wnd.Show();
                }
                else
                {
                    //hmmmmmmm
                }
            }
            else
            {
                firstRun = false;
                //List<int> Ids = new List<int>(sqq.HoleStapelIDs(i));
                List<int> StapelIds = new List<int>(sqq.getIntVarList(("select StapelID from Kategorien where UeberID==" + i), "StapelId"));
                KategorieBox.Items.Clear();
                AlleFragenBox.Items.Clear();
                InitializeComponent();

                for (int x = 0; x < StapelIds.Count(); x++)
                {

                    if (!sqq.hatUeber(x + 1))
                    {
                        MyButton KategorieButton = new MyButton(x + 1);

                        KategorieButton.Width = KategorieBox.Width - 15;
                        KategorieButton.Height = 40;
                        KategorieButton.Name = "Kat" + x;
                        KategorieButton.Content = (x + 1) + " : " + sqq.zeigeKategorie(StapelIds[x]);
                        KategorieButton.Visibility = Visibility.Visible;

                        MyButton AlleFragenButton = new MyButton(x + 1);

                        AlleFragenButton.Width = AlleFragenBox.Width - 25;
                        AlleFragenButton.Height = 40;
                        AlleFragenButton.Name = "Fra" + x;
                        AlleFragenButton.Content = sqq.count("KartenID", "Karten","StapelID=="+x);
                        AlleFragenButton.Visibility = Visibility.Visible;

                        MyButton OffeneFragenButton = new MyButton(x + 1);

                        OffeneFragenButton.Width = OffeneFragenBox.Width - 25;
                        OffeneFragenButton.Height = 40;
                        OffeneFragenButton.Name = "Frag" + x;
                        OffeneFragenButton.Content = sqq.count("KartenID", "Karten");
                        OffeneFragenButton.Visibility = Visibility.Visible;

                        KategorieBox.Items.Add(KategorieButton);
                        AlleFragenBox.Items.Add(OffeneFragenButton);
                        OffeneFragenBox.Items.Add(AlleFragenButton);

                        KategorieButton.Click += oeffneStapel;
                        AlleFragenButton.Click += oeffneStapel;

                        OffeneFragenButton.Click += oeffneStapel;
                    }
                    sqlkram.VerbindungBeenden();
                }
            }
        }
    }
}
