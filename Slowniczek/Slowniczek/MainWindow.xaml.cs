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

namespace Slowniczek
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Ulubione_Click(object sender, RoutedEventArgs e)
        {
            Ulubione_window ulubione_window = new Ulubione_window(); 
            this.Visibility = Visibility.Hidden;
            ulubione_window.Show();
        }

        private void Historia_Click(object sender, RoutedEventArgs e)
        {
            Historia_window historia_window = new Historia_window();
            this.Visibility = Visibility.Hidden;
            historia_window.Show();
        }

        private void Wyszukiwarka_Click(object sender, RoutedEventArgs e)
        {
            Wyszukiwarka wyszukiwarka_window = new Wyszukiwarka();
            this.Visibility = Visibility.Hidden;
            wyszukiwarka_window.Show();
        }

        private void Tlumacz_Click(object sender, RoutedEventArgs e)
        {
            Tlumacz tlumaczenie = new Tlumacz();
            this.Visibility = Visibility.Hidden;
            tlumaczenie.Show();
        }
    }
}
