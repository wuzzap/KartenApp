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
        public CategorieView()
        {
            InitializeComponent();

            showCategories();
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

        }

        private void Button_Click_CloseBtn(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void fillAutoCombo()
        {

        }
        private void showCategories()
        {

        }
    }
}
