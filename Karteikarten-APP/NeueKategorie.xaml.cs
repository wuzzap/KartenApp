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
            List<int> StapelIds = new List<int>(sqq.HoleAlleStapelIDs());
            ComboBoxItem cbi = new ComboBoxItem();

            sqlkram.VerbindungAufbauen();
            Console.WriteLine("Stapelanz: " + sqq.ZaehleStapel());
            for (int i = 0; i < sqq.ZaehleStapel(); i++)
            {
                Console.WriteLine("Füge Kategorie " + sqq.zeigeKategorie(StapelIds[i]) + " hinzu");
                //combo.Items.Add(sqq.zeigeKategorie(i));

                if (!sqq.HatUnter(StapelIds[i]))
                {
                    combo.Items.Add(sqq.zeigeKategorie(StapelIds[i]));
                    sqlkram.VerbindungBeenden();
                }
            }


        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            string kategorie=textboxKategorie.Text;

            bool hatUeber=true;
            if((string)combo.SelectedItem == "standard")
            //if (combo.Items.CurrentItem =="standard")
            {
                hatUeber = false;
            }


            sqq.ErstelleKategorie(kategorie,3,false, hatUeber);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void testbutton_Click(object sender, RoutedEventArgs e)
        {
            testlabel.Content = textboxKategorie.Text;
        }
    }
}
