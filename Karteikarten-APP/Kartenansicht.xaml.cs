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
    /// Interaktionslogik für Kartenansicht.xaml
    /// </summary>
    public partial class Kartenansicht : Window
    {
        public static sqlkram sqq = new sqlkram();
        public static List<Karte> karte;
        int index;


        public Kartenansicht(int StapelID)
        {

            karte = new List<Karte>(sqq.ErstelleKarten(StapelID));
            this.index = 0;
            this.DataContext = karte[this.index];

            InitializeComponent();
            erg.Content = karte[index].frage;
      
        }

        private void Schließen_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Zeige_Antwort(object sender, RoutedEventArgs e)
        {
            if (erg.Content == karte[index].frage)
            {

                erg.Content = karte[index].antwort;
            }
            else
            {
                erg.Content = karte[index].frage;
            }
        }

        private void Button_Click_Zurück(object sender, RoutedEventArgs e)
        {
            if (index >0)
            {
                index -= 1;
            }
            else
            {
                index = karte.Count() - 1;
            }
            erg.Content = karte[index].frage;
        }

        private void Button_Click_Vor(object sender, RoutedEventArgs e)
        {
            if (index < karte.Count - 1)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
            erg.Content = karte[index].frage;
        }
    }
}
