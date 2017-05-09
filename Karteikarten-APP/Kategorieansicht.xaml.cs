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
                    MyButton btn = new MyButton(StapelIds[i]);
                    btn.Width = 405;
                    btn.Width = listBox.Width - 15;
                    btn.Height = 40;
                    btn.Name = "Kat" + StapelIds[i];

                    btn.Content = (i ) + " : " + sqq.zeigeKategorie(StapelIds[i]);
                    btn.Visibility = Visibility.Visible;
                    listBox.Items.Add(btn);
                    StapelID = StapelIds[i];
                    btn.Click += oeffneStapel;
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
                listBox.Items.Clear();
                InitializeComponent();
                sqlkram.VerbindungAufbauen();
                Console.WriteLine("wtfiii "+i);
                for (int x = 0; x < sqq.ZaehleStapel(i); x++)
                {
                    Console.WriteLine("wtfxxxx "+x);
                    if (!sqq.hatUeber(x + 1))
                    {
                        MyButton btn = new MyButton(i + 1);
                        btn.Width = 405;
                        btn.Width = listBox.Width - 15;
                        btn.Height = 40;
                        btn.Name = "Kat" + x;
                        btn.Content = (x + 1) + " : " + sqq.zeigeUnterKategorien(i);
                        btn.Visibility = Visibility.Visible;
                        listBox.Items.Add(btn);
                        StapelID = i + 1;
                        btn.Click += oeffneStapel;
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
    }
}
