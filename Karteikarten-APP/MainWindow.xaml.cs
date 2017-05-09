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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Karteikarten_APP
{
    public partial class MainWindow : Window
    {
        public static sqlkram sqq = new sqlkram();
        public static int StapelID;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Beenden(object sender, RoutedEventArgs e)
        {
            Beenden();
        }

        private void Beenden()
        {
            this.Close();
        }

        private void Button_Click_DBInfo(object sender, RoutedEventArgs e)
        {
            var wnd = new DbInfo();
            wnd.Show();
        }

        private void Button_Click_Start(object sender, RoutedEventArgs e)
        {
            var wnd = new Kategorieansicht();
            wnd.Show();
        }
    }
}
