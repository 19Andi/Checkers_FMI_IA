using Checkers.Views;
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

namespace Checkers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About aboutPage = new About();
            aboutPage.Show();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gamePage = new GameWindow();
            gamePage.Show();
            this.Close();
        }
    }
}
