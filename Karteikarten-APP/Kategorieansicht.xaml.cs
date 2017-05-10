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
    /// <summary>
    /// Interaktionslogik für Kategorieansicht.xaml
    /// </summary>
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
        public static int StapelID;


        public Kategorieansicht()
        {
            InitializeComponent();
            sqlkram.VerbindungAufbauen();
            List<int> StapelIds = new List<int>(sqq.HoleAlleStapelIDs());

            for (int i = 0; i <StapelIds.Count(); i++)
            {
                
                if (!sqq.hatUeber(StapelIds[i]))
                {
                    MyButton KategorieButton = new MyButton(StapelIds[i]);

                    KategorieButton.Width = 405;
                    KategorieButton.Width = KategorieBox.Width - 15;
                    KategorieButton.Height = 40;
                    KategorieButton.Name = "Kat" + StapelIds[i];
                    KategorieButton.Content = (i ) + " : " + sqq.zeigeKategorie(StapelIds[i]);
                    KategorieButton.Visibility = Visibility.Visible;
                    KategorieBox.Items.Add(KategorieButton);

                    MyButton FragenButton = new MyButton(StapelIds[i]);

                    FragenButton.Width = FragenBox.Width - 25;
                    FragenButton.Height = 40;
                    FragenButton.Name = "Fra" + StapelIds[i];
                    FragenButton.Content = sqq.ZaehleKarten(StapelIds[i]);
                    FragenButton.Visibility = Visibility.Visible;
                    FragenBox.Items.Add(FragenButton);


                    StapelID = StapelIds[i];
                    KategorieButton.Click += oeffneStapel;
                    FragenButton.Click += oeffneStapel;

                }
            }

            sqlkram.VerbindungBeenden();
        }

        private void oeffneStapel(object sender, RoutedEventArgs e)
         {
            int i = ((MyButton)sender).field;
            if (!sqq.HatUnter(i))
            {
                var wnd = new Kartenansicht(i);
                wnd.Show();
            }
            else
            {
                List<int> StapelIds = new List<int>(sqq.HoleStapelIDs(i));

                KategorieBox.Items.Clear();
                FragenBox.Items.Clear();
                InitializeComponent();
                sqlkram.VerbindungAufbauen();
             
                for (int x = 0; x < StapelIds.Count(); x++)
                {
                   
                    if (!sqq.hatUeber(x + 1))
                    {
                        MyButton KategorieButton = new MyButton(i + 1);
                        
                        KategorieButton.Width = KategorieBox.Width - 15;
                        KategorieButton.Height = 40;
                        KategorieButton.Name = "Kat" + x;
                        KategorieButton.Content = (x + 1) + " : " + sqq.zeigeKategorie(StapelIds[x]);
                        KategorieButton.Visibility = Visibility.Visible;

                        MyButton FragenButton = new MyButton(i + 1);
                        
                        FragenButton.Width = FragenBox.Width - 25;
                        FragenButton.Height = 40;
                        FragenButton.Name = "Fra" + x;
                        FragenButton.Content = sqq.ZaehleKarten(StapelIds[x]);
                        FragenButton.Visibility = Visibility.Visible;


                        KategorieBox.Items.Add(KategorieButton);
                        FragenBox.Items.Add(FragenButton);
                        StapelID = i + 1;
                        KategorieButton.Click += oeffneStapel;
                        FragenButton.Click += oeffneStapel;
                    }
                    sqlkram.VerbindungBeenden();
                }
            }
        }

        /* private void oeffneStapel(object sender, RoutedEventArgs e)
         {
             int i = ((MyButton)sender).field;

             var wnd = new Kartenansicht(i);
             wnd.Show();
         }*/

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
    }
}
