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
    /// Logika interakcji dla klasy Historia_window.xaml
    /// </summary>
    public partial class Historia_window : Window
    {
        public Historia_window()
        {
            InitializeComponent();
            LadujDaneDoHistorii();
        }

        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

        private void LadujDaneDoHistorii()
        {
            string sciezkaDoPliku = @"C:\Users\dammm\Desktop\Slowniczek\Slowniczek\Historia.json"; // Aktualizuj ścieżkę zgodnie z potrzebami

            if (File.Exists(sciezkaDoPliku))
            {
                var json = File.ReadAllText(sciezkaDoPliku);
                var rekordyHistorii = JsonConvert.DeserializeObject<List<RekordHistorii>>(json);
                historiaDataGrid.ItemsSource = rekordyHistorii;
            }
        }

    }
}
