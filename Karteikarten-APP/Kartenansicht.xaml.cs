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
using System.Windows.Threading;

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
        bool changeCorrectTime = false;
        bool changeWrongTime = false;
        int index;
        DispatcherTimer _timer;
        TimeSpan _time;

        public Kartenansicht(int StapelID,string mode="", int priority = 5, bool justPriority = false)
        {
            switch (mode)
            {
                case "":
                    if (!justPriority)
                    {
                        karte = new List<Karte>(sqq.ErstelleKarten(StapelID));
                    }
                    else
                    {
                        karte = new List<Karte>(sqq.createPriorityCards(StapelID, priority));

                    }
                    break;

                case "Random": karte = new List<Karte>(sqq.createRandomCards(StapelID)); break;    // StapelID ist in diesem Falle die Anzahl der Fragen
                case "RandomPrio": karte = new List<Karte>(sqq.createRandomPrioCards(StapelID)); break;      // StapelID ist in diesem Falle die Anzahl der Fragen
            }


            this.index = 0;
            this.DataContext = karte[this.index];
            InitializeComponent();
            updateView(index);

            InitializeComponent();

            _time = TimeSpan.Zero;

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                timer.Content = _time.ToString("c");
                _time = _time.Add(TimeSpan.FromSeconds(+1));
            }, Application.Current.Dispatcher);

            _timer.Start();


        }

        private void updateView(int index)
        {

            //bei karte anheften
          //  long dt = sqq.getLong("select correctTime from Karten where KartenID==" + karte[index].kartenID, "correctTime");
            //DateTime dtx = new DateTime(dt);
            DateTime dtx = sqq.get_correctTime(karte[index].kartenID);
            Zuletztrichtig.Content = "Zuletzt : " + dtx;

            DateTime dtx2 = sqq.get_wrongTime(karte[index].kartenID);
            Zuletztfalsch.Content = "Zuletzt : " + dtx2;

            _time = TimeSpan.Zero;
            erg.Content = karte[index].frage;
            richtiglbl.Content = karte[index].correct;
            falschlbl.Content = karte[index].wrong;
            PrioLbL.Content = karte[index].priority;
            erg.Content = karte[index].frage;
            Zaehler.Content = ((index+1) + " / " + karte.Count);
            Kategorie.Content = karte[index].kategorie;
            
        }

        private void updateDB(int index)
        {
            if (changePriority) sqq.changeIntVars("Prioritaet", karte[index].priority, karte[index].kartenID);
            if (changeCorrect) sqq.changeIntVars("Korrekt", karte[index].correct, karte[index].kartenID);
            if (changeWrong)  sqq.changeIntVars("Falsch", karte[index].wrong, karte[index].kartenID);
            if (changeCorrectTime) sqq.changeTime(karte[index].kartenID,"CorrectTime","Karten");
            if (changeWrongTime) sqq.changeTime(karte[index].correct, "wrongTime", "Karten");
            changeWrong = false;
            changeCorrect = false;
            changePriority = false;
            changeCorrectTime = false;
            changeWrongTime = false;
            // changeWrongTime = false;
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
            changeCorrectTime = true;
            updateView(index);
        }

        private void Button_Click_Wrong(object sender, RoutedEventArgs e)
        {
            karte[index].wrong += 1;
            karte[index].priority += 1;
            changeWrong = true;
            changePriority = true;
            changeWrongTime = true;
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
            changeCorrectTime = true;
            changeWrongTime = true;
            updateView(index);
        }

        private void Schließen_Click(object sender, RoutedEventArgs e)
        {
            updateDB(index);
            this.Close();
        }
    }
}
