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
                if (!sc.gotCards(StackIDs[i])) combo.Items.Add(sc.getCategoryName(StackIDs[i]));
            }
            
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

            if (sc.nochFrei(Kategorie.Text))
            {
                string kategorie = Kategorie.Text;
                bool hatUeber = false;
                bool hatUnter = false;

                int ueberID = 0;


                if ((string)combo.SelectedItem == null)
                {
                    hatUeber = false;       // neuer stapel
                }
                else
                {
                    hatUnter = true;        // mutterstapel
                    hatUeber = true;        // neuer stapel
                    ueberID = sc.HoleStapelIDs((string)combo.SelectedItem)[0];
                }

                try
                {

                    if (hatUnter)
                    {
                        sc.setHatUnter(ueberID);
                    }
                    sc.ErstelleKategorie(kategorie, ueberID, hatUeber);

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
