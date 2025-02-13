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
using System.IO;
using Newtonsoft.Json;
namespace Slowniczek
{
    /// <summary>
    /// Logika interakcji dla klasy Ulubione_window.xaml
    /// </summary>
    public partial class Ulubione_window : Window
    {
        public Ulubione_window()
        {
            InitializeComponent();
            LadujDane();
        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

        private void ulubioneDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LadujDane()
        {
            string sciezkaDoPliku = @"C:\Users\dammm\Desktop\Slowniczek\Slowniczek\Ulubione.json";
            if (File.Exists(sciezkaDoPliku))
            {
                string jsonContent = File.ReadAllText(sciezkaDoPliku);
                var tlumaczenia = JsonConvert.DeserializeObject<List<Tlumaczenie>>(jsonContent);
                ulubioneDataGrid.ItemsSource = tlumaczenia;
            }
        }

    }
}
