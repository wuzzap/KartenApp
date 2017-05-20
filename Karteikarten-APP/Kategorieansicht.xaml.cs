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
using System;
using System.Windows;
using System.Windows.Threading;


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

        int index;
        DateTime dt = new DateTime();
        public static sqlkram sqq = new sqlkram();
        // public static int StapelID;
        private bool firstRun = true;

        public Kategorieansicht()
        {
            InitializeComponent();
            showCategories(0);
            fillAutoCombo();
        }



        private void openStack(int i)
        {

            sqq.changeCategoryTime(i);
            showCategories(i);
        }

        private void openStack(int i,bool firstRun)
        {

            sqq.changeCategoryTime(i);
            showCategories(i,firstRun);
        }

        private void oeffneStapel(object sender, RoutedEventArgs e)
        {
            int i = ((MyButton)sender).field;
            openStack(i,firstRun=false);
        }
        private void oeffnePrioStapel(object sender, RoutedEventArgs e)
        {
            int i = ((MyButton)sender).field;
            showCategories(i,false,5,true);
        }

        private void Button_Click_Beenden(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            openStack(this.index);
        }

        private void Button_Click_NeueKategorie(object sender, RoutedEventArgs e)
        {
            var wnd = new NeueKategorie();
            wnd.Closed += Window_Closed;
            wnd.ShowDialog();
           

        }

        private void Button_Click_NeueKarte(object sender, RoutedEventArgs e)
        {
            var wnd = new NeueKarte();
            wnd.Show();
           // wnd.Closed += Window_Closed;
           // wnd.Show.ShowDialog();
        }
        
        private void showCategories(int i,bool firstRun=true, int priority = 5, bool justPriority=false)
        {
            this.index = i;
            bool hatUnter = sqq.HatUnter(i);

            if (!hatUnter && firstRun == false)
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
                        var wnd = new Kartenansicht(i,"", priority, justPriority);
                        wnd.Show();
                    }
                }
                else
                {
                    showCategories(i,true);
                }
            }
            else
            {
               /* if (firstRun)
                {
                    i = 0;
                }*/
                firstRun = false;
                //List<int> Ids = new List<int>(sqq.HoleStapelIDs(i));
                List<int> StapelIds = new List<int>(sqq.getIntVarList(("select StapelID from Kategorien where UeberID==" + i), "StapelId"));
                KategorieBox.Items.Clear();
                AlleFragenBox.Items.Clear();
                OffeneFragenBox.Items.Clear();
                ZuletztBox.Items.Clear();
                InitializeComponent();

                for (int x = 0; x < StapelIds.Count(); x++)
                {
                    int UeberID = sqq.getInt("select UeberId from Kategorien where StapelId==" + x + "","UeberId");


                    //if (!sqq.hatUeber(StapelIds[x]))

                   // if (UeberID==i)
                    //{
                        MyButton KategorieButton = new MyButton(StapelIds[x]);

                        KategorieButton.Width = KategorieBox.Width - 15;
                        KategorieButton.Height = 40;
                        KategorieButton.Name = "Kat" + x;
                        KategorieButton.Content = (x + 1) + " : " + sqq.getCategoryName(StapelIds[x]);
                        KategorieButton.Visibility = Visibility.Visible;

                        MyButton AlleFragenButton = new MyButton((StapelIds[x]));

                        AlleFragenButton.Width = AlleFragenBox.Width - 25;
                        AlleFragenButton.Height = 40;
                        AlleFragenButton.Name = "Fra" + x;
                    //AlleFragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                       // AlleFragenButton.Content = sqq.count("KartenID", "Karten","StapelID=="+ StapelIds[x]);
                       if (sqq.HatUnter(StapelIds[x]))
                    {
                        AlleFragenButton.Content = sqq.countAllCards((StapelIds[x]));
                    }
                    else
                    {
                        AlleFragenButton.Content = sqq.count("KartenID", "Karten", "StapelID==" + StapelIds[x]);
                    }
                    AlleFragenButton.Visibility = Visibility.Visible;

                    MyButton OffeneFragenButton = new MyButton((StapelIds[x]));

                        OffeneFragenButton.Width = OffeneFragenBox.Width - 25;
                        OffeneFragenButton.Height = 40;
                        OffeneFragenButton.Name = "Frag" + x;
                    //OffeneFragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                    int openQuestions= sqq.count("KartenID", "Karten", " Prioritaet>5 AND StapelID==" + StapelIds[x]);
                    OffeneFragenButton.Content = openQuestions;
                  if (openQuestions > 0)
                    {
                        OffeneFragenButton.Content = openQuestions;
                       // OffeneFragenButton.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        OffeneFragenButton.Content = "X";
                        //OffeneFragenButton.Visibility = Visibility.Hidden;
                    }
                        Label lastTry = new Label();

                        lastTry.Width = ZuletztBox.Width-25;
                        lastTry.Height = 40;
                        lastTry.Name = "lastTry" + x;
                        //OffeneFragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                        long dt= sqq.getLong("select time from Kategorien where StapelID==" + StapelIds[x],"Time");
                        DateTime dtx = new DateTime(dt);
                        //dtx.AddTicks(dt);
                        lastTry.Content = dtx;
                        lastTry.Visibility = Visibility.Visible;



                        KategorieBox.Items.Add(KategorieButton);
                        AlleFragenBox.Items.Add(AlleFragenButton);
                        OffeneFragenBox.Items.Add(OffeneFragenButton);
                        ZuletztBox.Items.Add(lastTry);

                        KategorieButton.Click += oeffneStapel;
                        AlleFragenButton.Click += oeffneStapel;

                        OffeneFragenButton.Click += oeffnePrioStapel;
                  //  }
                    sqlkram.VerbindungBeenden();
                }
            }
        }

        private void fillAutoCombo()
        {
            autoCombo.Items.Add("Random");
            autoCombo.Items.Add("RandomPrio");
            autoCombo.Items.Add("Meiste Fehler");
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            openStack(0);
        }

        private void autoStack_ClickBtn(object sender, RoutedEventArgs e)
        {
            autoStack();
        }

        public void showPrioQuestions(int i)
        {

        }

        private void autoStack()
        {
            int anz = Convert.ToInt32(anzAuto.Text);
            switch (autoCombo.Text)
            {
                case "Random":
                    var wnd = new Kartenansicht(anz,autoCombo.Text,5,false);
                    wnd.Show(); break;

                case "RandomPrio": wnd=new Kartenansicht(anz, autoCombo.Text, 5, false); wnd.Show(); break;
                case "Meiste Fehler": break;
            }
        }



    }
}
