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
    /// Interaktionslogik für DbInfo.xaml
    /// </summary>
    public partial class DbInfo : Window
    {

        public static sqlkram sqq = new sqlkram();
        public DbInfo()
        {
            InitializeComponent();
            //anzKarten.Content = "Es gibt "+sqq.ZaehleKarten()+" Karten";
            //anzStapel.Content="Es gibt "+sqq.ZaehleStapel() + " Stapel";

            anzKarten.Content = "Es gibt " + sqq.count("Karten","Karten") + " Karten";
            anzStapel.Content = "Es gibt " + sqq.countStacks() + " Stapel";
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
