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

    public partial class NewCategory : Window
    {
        static sqlte_connector sc = new sqlte_connector();
        Stack sk = new Stack();
        public NewCategory()
        {
            InitializeComponent();
            updateView();
            parentStack_Combo.Text = "Root";
        }

        private void updateView()
        {
            List<int> StackIDs = new List<int>(sc.get_list_allStackIds());
            ComboBoxItem cbi = new ComboBoxItem();
            parentStack_Combo.Items.Clear();
            for (int i = 0; i < StackIDs.Count(); i++)
            {
                if (!sc.get_bool_gotCards(StackIDs[i]))
                {
                    parentStack_Combo.Items.Add(sc.get_string_stackName_stacks(StackIDs[i]));
                }
            }
            sqlte_connector.dc();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (!sc.get_bool_gotCards(parentStack_Combo.Text))
            {
                string Stack = StackName_Box.Text;
                //bool gotParent = false;
                int parentID = 0;

                if (parentStack_Combo.SelectedItem == null)
                {
                    parentID = 0;      
                }
                else
                {
                    //gotParent = true; 
                    int y= sc.get_int_ID_Stack((string)parentStack_Combo.SelectedItem);
                    parentID = y;
                }

                try
                {
                    if (parentID>0)
                    {
                        sc.set_bool_gotChild(parentID,true);
                    }
                    sc.add_NewStack(Stack, parentID);

                    message_lbl.Content = "Der Stapel " + StackName_Box.Text + " wurde erfolgreich hinzugefügt!";
                }
                catch
                {
                    message_lbl.Content = "Konnte den Stapel " + StackName_Box.Text + " nicht hinzufügen :(";
                    // todo: sql report ziehen
                }
            }
            else
            {
                message_lbl.Content = "Die Kategorie " + StackName_Box.Text + " besitzt bereits Karten!";
                // todo verschieben anbieten


            }
            updateView();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
