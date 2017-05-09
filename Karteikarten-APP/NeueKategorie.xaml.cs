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
        }

        private void Button_Click_NeueKategorie(object sender, RoutedEventArgs e)
        {
            var wnd = new Kategorieansicht();
            wnd.Show();
            sqq.ErstelleKategorie();
        }
    }
}
