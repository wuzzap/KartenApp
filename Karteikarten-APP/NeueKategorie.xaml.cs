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
    /// Interaktionslogik für NeueKategorie.xaml
    /// </summary>
    public partial class NeueKategorie : Window
    {

        public static sqlkram sqq = new sqlkram();
        public NeueKategorie()
        {
            InitializeComponent();

            updateView();




        }

        private void updateView()
        {
            List<int> StapelIds = new List<int>(sqq.HoleAlleStapelIDs());
            ComboBoxItem cbi = new ComboBoxItem();
            sqlkram.VerbindungAufbauen();
            for (int i = 0; i < sqq.ZaehleStapel(); i++)
            {
                if (!sqq.hatKarten(StapelIds[i]))
                {
                    combo.Items.Add(sqq.zeigeKategorie(StapelIds[i]));

                }
            }
            sqlkram.VerbindungBeenden();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {

            if (sqq.nochFrei(Kategorie.Text))
            {
                string kategorie = Kategorie.Text;
                bool hatUeber=false;
                int ueberID = 0;


                if ((string)combo.SelectedItem ==null)
                {
                    hatUeber = false;
                }
                else
                {
                    hatUeber = true;
                    ueberID = sqq.HoleStapelIDs((string)combo.SelectedItem)[0];
                }

                try
                {
                    sqq.ErstelleKategorie(kategorie, ueberID, hatUeber);
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
