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
    /// Interaktionslogik für NewCategory.xaml
    /// </summary>
    public partial class NewCategory : Window
    {
        static sqlte_connector sc = new sqlte_connector();
        Stack sk = new Stack();
        public NewCategory()
        {
            InitializeComponent();
            updateView();
        }

        private void updateView()
        {
            List<int> StackIDs = new List<int>(sc.getAllStackIds());
            ComboBoxItem cbi = new ComboBoxItem();
            combo.Items.Clear();
            for (int i = 0; i < StackIDs.Count(); i++)
            {
                if (!sc.gotCards(StackIDs[i]))
                {
                    combo.Items.Add(sk.getCategoryName(StackIDs[i]));
                }
            }
            sqlte_connector.dc();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

            if (sc.gotCards(Kategorie.Text))
            {
                string kategorie = Kategorie.Text;
                bool gotParent = false;
                bool gotChild = false;

                int parentID = 0;


                if ((string)combo.SelectedItem == null)
                {
                    gotParent = false;       // neuer stapel
                }
                else
                {
                    gotChild = true;        // mutterstapel
                    gotParent = true;        // neuer stapel
                    parentID = sc.getParentId(sc.getStackIntbyName((string)combo.SelectedItem,"ID"));
                }

                try
                {
                    if (gotChild)
                    {
                        sc.setgotChild(parentID);
                    }
                    sc.ErstelleKategorie(kategorie, parentID, gotParent);

                    message_lbl.Content = "Die Kategorie " + Kategorie.Text + " wurde erfolgreich hinzugefügt!";
                }
                catch
                {
                    message_lbl.Content = "Sorry konnte die Kategorie " + Kategorie.Text + " nicht hinzufügen :(";
                }
            }
            else
            {
                message_lbl.Content = "Die Kategorie " + Kategorie.Text + " ist schon vorhanden";
            }

            updateView();
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
