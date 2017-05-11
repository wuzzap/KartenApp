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
        bool changePriority = false;
        bool changeCorrect = false;
        bool changeWrong = false;
        int index;


        public Kartenansicht(int StapelID)
        {
            karte = new List<Karte>(sqq.ErstelleKarten(StapelID));
            this.index = 0;
            this.DataContext = karte[this.index];
            InitializeComponent();
            updateView(index);


        }

        private void updateView(int index)
        {
            erg.Content = karte[index].frage;
            richtiglbl.Content = karte[index].correct;
            falschlbl.Content = karte[index].wrong;
            PrioLbL.Content = karte[index].priority;
            erg.Content = karte[index].frage;
        }

        private void updateDB(int index)
        {
            if (changePriority) sqq.changeIntVars("Prioritaet", karte[index].priority, karte[index].kartenID);
            if (changeCorrect) sqq.changeIntVars("Korrekt", karte[index].correct, karte[index].kartenID);
            if (changeWrong)  sqq.changeIntVars("Falsch", karte[index].wrong, karte[index].kartenID);
         
            changeWrong = false;
            changeCorrect = false;
            changePriority = false;
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
            updateDB(index);
            if (index > 0)
            {
                index -= 1;
            }
            else
            {
                index = karte.Count() - 1;
            }
            updateView(index);
            updateDB(index);
        }

        private void Button_Click_Vor(object sender, RoutedEventArgs e)
        {
            updateDB(index);
            if (index < karte.Count - 1)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
            updateView(index);
 
        }

        private void Button_Click_Richtig(object sender, RoutedEventArgs e)
        {
            karte[index].correct += 1;
            karte[index].priority -= 1;
            changeCorrect = true;
            changePriority = true;
            updateView(index);
        }

        private void Button_Click_Wrong(object sender, RoutedEventArgs e)
        {
            karte[index].wrong += 1;
            karte[index].priority += 1;
            changeWrong = true;
            changePriority = true;
            updateView(index);

        }

        private void ReducePriority_Click(object sender, RoutedEventArgs e)
        {
            karte[index].priority -= 1;
            changePriority = true;
            updateView(index);
        }

        private void AddPriority_Click(object sender, RoutedEventArgs e)
        {
            karte[index].priority += 1;
            changePriority = true;
            updateView(index);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            karte[index].correct = 0;     
            karte[index].wrong = 0;
            changeWrong = true;
            changeCorrect = true;
            updateView(index);
        }

        private void Schließen_Click(object sender, RoutedEventArgs e)
        {
            updateDB(index);
            this.Close();
        }
    }
}
