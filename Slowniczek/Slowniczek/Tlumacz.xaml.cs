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
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.IO;



namespace Slowniczek
{
    /// <summary>
    /// Logika interakcji dla klasy Tlumacz.xaml
    /// </summary>
    public partial class Tlumacz : Window
    {
        private ApiTlumaczen apiTlumaczen = new ApiTlumaczen();
        public Tlumacz()
        {
            InitializeComponent();
        }

      

        private void Zmiana_Click(object sender, RoutedEventArgs e)
        {
            string temp = Jezyk1.Text;
            Jezyk1.Text = Jezyk2.Text;
            Jezyk2.Text = temp;
        }

        private async void Tlumacz_button_Click(object sender, RoutedEventArgs e)
        {
            string tekstDoPrzetlumaczenia = TextBox1.Text;
            string jezykZrodlowy = Jezyk1.Text; // "PL" dla Polskiego, "EN" dla Angielskiego
            string jezykDocelowy = Jezyk2.Text; // "EN" dla Angielskiego, "PL" dla Polskiego

            try
            {
                var przetlumaczonyTekst = await apiTlumaczen.PrzetlumaczTekst(tekstDoPrzetlumaczenia, jezykZrodlowy, jezykDocelowy);
                TextBox2.Text = przetlumaczonyTekst;
                DodajDoUlubionych.Visibility = Visibility.Visible;

                // Logika zapisywania do historii
                string sciezkaDoPliku = @"C:\Users\dammm\Desktop\Slowniczek\Slowniczek\Historia.json";
                List<RekordHistorii> historia;
                if (File.Exists(sciezkaDoPliku))
                {
                    string json = File.ReadAllText(sciezkaDoPliku);
                    historia = JsonConvert.DeserializeObject<List<RekordHistorii>>(json) ?? new List<RekordHistorii>();
                }
                else
                {
                    historia = new List<RekordHistorii>();
                }

                var istniejacyRekord = historia.FirstOrDefault(h => h.SlowoPL == tekstDoPrzetlumaczenia && h.SlowoEN == przetlumaczonyTekst);
                if (istniejacyRekord != null)
                {
                    istniejacyRekord.DataWyszukania = DateTime.Now;
                }
                else
                {
                    historia.Add(new RekordHistorii
                    {
                        Index = historia.Count > 0 ? historia.Max(h => h.Index) + 1 : 1,
                        SlowoPL = tekstDoPrzetlumaczenia,
                        SlowoEN = przetlumaczonyTekst,
                        DataWyszukania = DateTime.Now
                    });
                }

                File.WriteAllText(sciezkaDoPliku, JsonConvert.SerializeObject(historia, Formatting.Indented));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas tłumaczenia: {ex.Message}");
            }
        }


        private void Powrot_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Visibility = Visibility.Hidden;
            mainWindow.Show();
        }

        // Definicja klasy reprezentującej pojedynczy rekord tłumaczenia
        public class Tlumaczenie
        {
            public int Index { get; set; }
            public string SlowoPL { get; set; }
            public string SlowoEN { get; set; }
        }


        private void DodajDoUlubionych_Click(object sender, RoutedEventArgs e)
        {
            string sciezkaDoPliku = @"C:\Users\dammm\Desktop\Slowniczek\Slowniczek\Ulubione.json"; // Bezpośrednia ścieżka do pliku

            string tekst1 = TextBox1.Text; // Tekst z pierwszego TextBoxa
            string tekst2 = TextBox2.Text; // Tekst z drugiego TextBoxa
            string jezyk1 = Jezyk1.Text; // Język z pierwszego TextBoxa

            // Odczyt istniejących tłumaczeń z pliku JSON
            List<Tlumaczenie> tlumaczenia = new List<Tlumaczenie>();
            if (File.Exists(sciezkaDoPliku))
            {
                string json = File.ReadAllText(sciezkaDoPliku);
                tlumaczenia = JsonConvert.DeserializeObject<List<Tlumaczenie>>(json) ?? new List<Tlumaczenie>();
            }

            // Sprawdzenie, czy rekord już istnieje
            bool rekordIstnieje = tlumaczenia.Any(t => t.SlowoPL == (jezyk1 == "PL" ? tekst1 : tekst2) && t.SlowoEN == (jezyk1 == "EN" ? tekst1 : tekst2));

            if (rekordIstnieje)
            {
                MessageBox.Show("Taki rekord już istnieje.");
                return; // Przerwanie metody, jeśli rekord już istnieje
            }

            // Tworzenie nowego tłumaczenia na podstawie wprowadzonych danych
            Tlumaczenie noweTlumaczenie = new Tlumaczenie
            {
                Index = tlumaczenia.Count + 1,
                SlowoPL = jezyk1 == "PL" ? tekst1 : tekst2,
                SlowoEN = jezyk1 == "EN" ? tekst1 : tekst2
            };

            // Dodanie nowego tłumaczenia do listy
            tlumaczenia.Add(noweTlumaczenie);

            // Zapisanie zaktualizowanej listy tłumaczeń do pliku JSON
            string zaktualizowanyJson = JsonConvert.SerializeObject(tlumaczenia, Formatting.Indented);
            File.WriteAllText(sciezkaDoPliku, zaktualizowanyJson);

            MessageBox.Show("Tłumaczenie zostało dodane do ulubionych.");
        }


    }

}
