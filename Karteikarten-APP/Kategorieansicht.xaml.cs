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

        public void showPrioQuestions(int i)
        {

        }

       

        private void oeffneStapel(object sender, RoutedEventArgs e)
        {
            int i = ((MyButton)sender).field;

            DateTime dt = DateTime.Now;
            long dtx = dt.Ticks;
            sqq.changeTime(i,dtx,"Kategorie");
                showCategories(i);
        }
        private void oeffnePrioStapel(object sender, RoutedEventArgs e)
        {
            int i = ((MyButton)sender).field;
            showCategories(i,5,true);
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
        
        private void showCategories(int i, int priority = 5, bool justPriority=false)
        {
            if (!sqq.HatUnter(i) && firstRun == false)
            {
                if (sqq.hatKarten(i))
                {
                    if (!justPriority)
                    {
                        var wnd = new Kartenansicht(i);
                        wnd.Show();
                    }
                    else
                    {
                        var wnd = new Kartenansicht(i,priority,justPriority);
                        wnd.Show();
                    }
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

                        //AlleFragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                        AlleFragenButton.Content = sqq.count("KartenID", "Karten","StapelID=="+ StapelIds[x]);
                        AlleFragenButton.Visibility = Visibility.Visible;

                        MyButton OffeneFragenButton = new MyButton(x + 1);

                        OffeneFragenButton.Width = OffeneFragenBox.Width - 25;
                        OffeneFragenButton.Height = 40;
                        OffeneFragenButton.Name = "Frag" + x;
                        //OffeneFragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                        OffeneFragenButton.Content = sqq.count("KartenID","Karten"," Prioritaet>5 AND StapelID=="+StapelIds[x]);
                        OffeneFragenButton.Visibility = Visibility.Visible;

                        Label lastTry = new Label();

                        lastTry.Width = OffeneFragenBox.Width - 25;
                        lastTry.Height = 40;
                        lastTry.Name = "lastTry" + x;
                        //OffeneFragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                         long dt= sqq.getLong("select time from Kategorien where StapelID==" + StapelIds[x],"Time");
                        DateTime dtx = new DateTime();
                        dtx.AddTicks(dt);
                        //lastTry.Content = dtx.;
                        lastTry.Visibility = Visibility.Visible;



                        KategorieBox.Items.Add(KategorieButton);
                        AlleFragenBox.Items.Add(AlleFragenButton);
                        OffeneFragenBox.Items.Add(OffeneFragenButton);
                        ZuletztBox.Items.Add(lastTry);

                        KategorieButton.Click += oeffneStapel;
                        AlleFragenButton.Click += oeffneStapel;

                        OffeneFragenButton.Click += oeffnePrioStapel;
                    }
                    sqlkram.VerbindungBeenden();
                }
            }
        }
    }
}
