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
static int index;
        TimeSpan duration;
        DispatcherTimer _timer;
        TimeSpan _time;

        public CardView(List<Card> cardx)
        {
            card = new List<Card>(cardx);
            index = 0;
            this.DataContext = card[index];
            InitializeComponent();
            updateView();
            time_progress.Value = 0;
            InitializeComponent();


            // mit Hilfe von Olaf

            startTimer();



        }

        private void startTimer()
        {
            time_progress.Foreground = new SolidColorBrush(Colors.Green);
            _time = TimeSpan.Zero;
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                timer.Content = _time.ToString("c");
                _time = _time.Add(TimeSpan.FromSeconds(+1));
                time_progress.Value += 1;
                if (time_progress.Value >= card[index].duration.TotalSeconds)
                {
                    time_progress.Foreground = new SolidColorBrush(Colors.Red);
                }
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void updateView()
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
            time_progress.Minimum = 0;
            time_progress.Maximum = card[index].duration.TotalSeconds;
            time_progress.Value = 0;
            durationlbl.BringIntoView();
        }

        private void updateDB()
        {
            if (changePriority) sc.set_int_priority(card[index].ID, card[index].priority);
            if (changeCorrect) sc.set_int_correct(card[index].ID,card[index].correct);
            if (changeWrong) sc.set_int_wrong(card[index].ID,card[index].wrong);
            if (changeDuration) sc.set_duration(card[index].ID, card[index].duration.Ticks);
            if (changeCorrectTime == true) sc.set_dateTime_correctTime(card[index].ID);
            if (changeWrongTime == true) sc.set_dateTime_wrongTime(card[index].ID);
            changeWrong = false;
            changeCorrect = false;
            changePriority = false;
            changeCorrectTime = false;
            changeWrongTime = false;
            changeDuration = false;
        }


        private void Button_Click_Show_Answer(object sender, RoutedEventArgs e)
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

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            lastCard();
        }

        private void lastCard()
        {
            updateDB();
            if (index > 0)
            {
                index -= 1;
            }
            else
            {
                index = card.Count() - 1;
            }
            updateView();
            updateDB();
        }

        private void Button_Click_Next(object sender, RoutedEventArgs e)
        {
            nextCard();
        }

        public void nextCard()
        {
            updateDB();

            if (index < card.Count - 1)
            {
                index += 1;
            }
            else
            {
                index = 0;
            }
            updateView();
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
            nextCard();

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

            nextCard();
        }

        private void ReducePriority_Click(object sender, RoutedEventArgs e)
        {
            card[index].priority -= 1;
            changePriority = true;
            updateView();
        }

        private void AddPriority_Click(object sender, RoutedEventArgs e)
        {
            card[index].priority += 1;
            changePriority = true;
            updateView();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            card[index].correct = 0;
            card[index].wrong = 0;
            changeWrong = true;
            changeCorrect = true;
            changeCorrectTime = true;
            changeWrongTime = true;
            updateView();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            updateDB();
            this.Close();
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
