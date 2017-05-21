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

namespace learningProggy
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_CategoriesBtn(object sender, RoutedEventArgs e){
            var wnd = new CategorieView();
            wnd.Show();
        }

        private void Button_Click_OptionsBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_ExitBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
