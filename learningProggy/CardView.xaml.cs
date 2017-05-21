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


namespace learningProggy
{

    public partial class CardView : Window
    {
        static sqlte_connector sc = new sqlte_connector();

        public static List<Card> card;
        bool changePriority = false;
        bool changeCorrect = false;
        bool changeWrong = false;
        bool changeCorrectTime = false;
        bool changeWrongTime = false;
        bool changeDuration = false;
        int index;
        TimeSpan duration;
        DispatcherTimer _timer;
        TimeSpan _time;

        public CardView(List<Card> card)
        {   
            this.index = 0;
            this.DataContext = card[this.index];
            InitializeComponent();
            updateView(index);

            InitializeComponent();


            // mit Hilfe von Olaf
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
            Zuletztrichtig.Content = "Zuletzt : " + card[index].correctTime;
            Zuletztfalsch.Content = "Zuletzt : " + card[index].wrongTime;
            _time = TimeSpan.Zero;
            erg.Content = card[index].question;
            richtiglbl.Content = card[index].correct;
            falschlbl.Content = card[index].wrong;
            PrioLbL.Content = card[index].priority;
            erg.Content = card[index].question;
            Zaehler.Content = ((index + 1) + " / " + card.Count);
            Kategorie.Content = card[index].stackName;
            durationlbl.Content = card[index].duration;

        }

        private void updateDB(int index)
        {
            if (changePriority) sc.changeIntVars("Priority", card[index].priority, card[index].ID);
            if (changeCorrect) sc.changeIntVars("Correkt", card[index].correct, card[index].ID);
            if (changeWrong) sc.changeIntVars("Wrong", card[index].wrong, card[index].ID);
            if (changeDuration) sc.changeDuration(card[index].ID, card[index].duration.Ticks);
            if (changeCorrectTime == true) sc.changeCorrectTime(card[index].ID);
            if (changeWrongTime == true) sc.changeWrongTime(card[index].ID);
            changeWrong = false;
            changeCorrect = false;
            changePriority = false;
            changeCorrectTime = false;
            changeWrongTime = false;
            changeDuration = false;
        }


        private void Button_Click_Zeige_Antwort(object sender, RoutedEventArgs e)
        {
            if (erg.Content == card[index].question)
            {
                erg.Content = card[index].answer;
            }
            else
            {
                erg.Content = card[index].question;
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
                index = card.Count() - 1;
            }
            updateView(index);
            updateDB(index);
        }

        private void Button_Click_Vor(object sender, RoutedEventArgs e)
        {
            updateDB(index);
            if (index < card.Count - 1)
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
            card[index].correct += 1;
            card[index].priority -= 1;
            card[index].correctTime = DateTime.Now;
            card[index].duration = _time;
            changeCorrect = true;
            changePriority = true;
            changeCorrectTime = true;
            changeDuration = true;
            updateView(index);
        }

        private void Button_Click_Wrong(object sender, RoutedEventArgs e)
        {
            card[index].wrong += 1;
            card[index].priority += 1;
            card[index].wrongTime = DateTime.Now;
            card[index].duration = _time;
            changeWrong = true;
            changePriority = true;
            changeWrongTime = true;
            changeDuration = true;

            updateView(index);
        }

        private void ReducePriority_Click(object sender, RoutedEventArgs e)
        {
            card[index].priority -= 1;
            changePriority = true;
            updateView(index);
        }

        private void AddPriority_Click(object sender, RoutedEventArgs e)
        {
            card[index].priority += 1;
            changePriority = true;
            updateView(index);
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            card[index].correct = 0;
            card[index].wrong = 0;
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
