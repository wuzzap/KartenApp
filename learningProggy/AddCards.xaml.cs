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


using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Data.SQLite.Linq;



namespace learningProggy
{
    /// <summary>
    /// Interaktionslogik für AddCards.xaml
    /// </summary>
    public partial class AddCards : Window
    {
        static sqlte_connector sc = new sqlte_connector();
        Stack st = new Stack();
        static List<Stack> stacks = new List<Stack>();
        public AddCards()
        {
            InitializeComponent();
            stacks = st.createStacks(sc.get_list_allStackIds());
            ComboBoxItem cbi = new ComboBoxItem();
            for (int i = 0; i < stacks.Count; i++)
            {
                if (!stacks[i].gotChild)
                {
                    Stack_Combo.Items.Add(stacks[i].stackName);
                }
            }
        }


        
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (Stack_Combo.Text !="") { addNewCard(); }
            else { msg.Text = "Bitte einen Stapel auswählen"; }
        }

        public void addNewCard()            // todo: überlegung;  komprimierung vs überblick
        {
            string question = Question_Box.Text;
            string answer = Answer_Box.Text;
            int correct = 0;
            int wrong = 0;
            int priority = 10;
            //string stackName = Stack_Combo.Text;
            string stackName = Stack_Combo.Text;
            int stackID = sc.get_int_ID_Stack(Stack_Combo.Text);
            DateTime dt = DateTime.Now;
            TimeSpan duration = new TimeSpan(1);
            long dtx = dt.Ticks;
            long tsx = duration.Ticks;



            try
            {
                
                sc.add_NewCard(stackName,stackID, priority, question, answer, correct, wrong, dtx, tsx);
                msg.Text = "Die Karte wurde in "+ Stack_Combo.Text+" abgelegt";

            }
            catch (SQLiteException ex)
            {
                msg.Text = ex.Message;
            }
           /* catch ( Exception ex)
            {
                msg.Text = ex.ToString();
            }*/
            }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
