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

namespace learningProggy
{
    /// <summary>
    /// Interaktionslogik für CategorieView.xaml
    /// </summary>
    public partial class CategorieView : Window
    {
        static sqlte_connector sc = new sqlte_connector();
        static Stack st = new Stack();
        static List<Stack> stacks = new List<Stack>();
        public CategorieView()
        {
            InitializeComponent();
            st=st.createRoot();
            showCategories(st.childIdList);
            fillAutoCombo();


        }

        class MyButton : Button
        {
            public int field;

            public MyButton(int i) : base()
            {
                this.field = i;
            }

        }
        
        private void Button_Click_NewCategoryBtn(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_NewCardBtn(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_autoStackBtn(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_HomeBtn(object sender, RoutedEventArgs e)
        {
            showCategories(st.childIdList);
        }

        private void Button_Click_CloseBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fillAutoCombo()
        {

        }
        private void showCategories(List<int> list)
        {
            List<Stack> stacks = new List<Stack>(st.createStacks(list));
            CategoryNameBox.Items.Clear();
            QuantityOfQuestionBox.Items.Clear();
            QuantityOfPrioQuestionBox.Items.Clear();
            LastTryBox.Items.Clear();


            for (int i = 0; i < stacks.Count; i++)
            {
                MyButton CategoryButton = new MyButton(stacks[i].iD);
                CategoryButton.Width = CategoryNameBox.Width - 15;
                CategoryButton.Height = 40;
                CategoryButton.Name = "Category" + i;
                CategoryButton.Content = ("Category: "+(i + 1)) + " : " + stacks[i].stackName;
                CategoryButton.Visibility = Visibility.Visible;
                

                MyButton AlleFragenButton = new MyButton(stacks[i].iD);
                AlleFragenButton.Width = QuantityOfQuestionBox.Width - 25;
                AlleFragenButton.Height = 40;
                AlleFragenButton.Name = "Question" + i;
                int all = 0;
                if (stacks[i].gotChild)
                {
                    all= sc.countChildStackCards(stacks[i].childIdList);
                }
                else
                {
                    all = sc.countStackCards(stacks[i].iD);
                }
                AlleFragenButton.Content = all;
                if (all < 1) AlleFragenButton.Content = "x"; 
                AlleFragenButton.Visibility = Visibility.Visible;

                MyButton OffeneFragenButton = new MyButton(stacks[i].iD);
                OffeneFragenButton.Width = QuantityOfPrioQuestionBox.Width - 25;
                OffeneFragenButton.Height = 40;
                OffeneFragenButton.Name = "PrioQuestion"+i;
                int open = 0;
                if (stacks[i].gotChild)
                {
                    open= sc.countChildStackPrioCards(stacks[i].childIdList);
                }
                else
                {
                    open= sc.countStackPrioCards(stacks[i].iD);
                }
                if (open > 0) OffeneFragenButton.Content = open;
                else OffeneFragenButton.Content = "x";



                Label lastTry = new Label();
                lastTry.Width = LastTryBox.Width - 25;
                lastTry.Height = 40;
                lastTry.Name = "lastView" + i;
                lastTry.Content = stacks[i].lastView;
                lastTry.Visibility = Visibility.Visible;

                
                CategoryNameBox.Items.Add(CategoryButton);
                QuantityOfQuestionBox.Items.Add(AlleFragenButton);
                QuantityOfPrioQuestionBox.Items.Add(OffeneFragenButton);
                LastTryBox.Items.Add(lastTry);
                
                CategoryButton.Click += openStack;
                AlleFragenButton.Click += openStack; 
        }

        }

        private void openStack(object sender, RoutedEventArgs e)
        {
            int i = ((MyButton)sender).field;
            Stack stack = new Stack(i);
            if(stack.gotChild) showCategories(sc.getChildStackIds(i));
            if (stack.gotCards)
            {
               List<Card> cards = Card.createCards(new List<int>(sc.get_CardStack(i)));
               var wnd = new CardView(cards);
               wnd.Show();
            }
        }


    }
}
