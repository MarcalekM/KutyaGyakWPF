using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace KutyaGyakWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Kutya> kutyaLista = new List<Kutya>();
        public MainWindow()
        {
            string connectionString = "server=localhost;user=root;password=;database=kutya";



            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string kutyaQuery =
                    "SELECT kutyak.id, kutyak.nev, kan, fajtak.id, fajtak.fajta, gazdak.id, gazdak.nev, gazdak.tel, kor, chipdatum, kepurl FROM kutyak inner join gazdak on kutyak.gazdaid = gazdak.id inner join fajtak on kutyak.fajtaid = fajtak.id";
                using (MySqlCommand command = new MySqlCommand(kutyaQuery, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string nev = reader.GetString(1);
                        bool kan = reader.GetBoolean(2);
                        Fajta fajta = new Fajta(reader.GetInt32(3), reader.GetString(4));
                        Gazda gazda = new Gazda(reader.GetInt32(5), reader.GetString(6), reader.GetString(7));
                        int kor = reader.GetInt32(8);
                        DateTime chipDatum = reader.GetDateTime(9);
                        string kepUrl = reader.GetString(10);

                        Kutya kutya = new Kutya(id, nev, kan, kor, chipDatum, kepUrl, fajta, gazda);
                        kutyaLista.Add(kutya);
                    }
                }
                connection.Close();
            }

            InitializeComponent();
        }

        private void Kilépés_Click(object sender, RoutedEventArgs e)
        {
                this.Close();
        }

        private void Adatok_Click(object sender, RoutedEventArgs e)
        {
            foreach (var kutya in kutyaLista) Kutyak.Items.Add(kutya.Nev);
            Kutyak.SelectedIndex = 0;
            KutyaDB.Content = kutyaLista.Count().ToString() + " darab adat került betöltésre";
        }

        private void Kutyak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            KutyaAdatok.Items.Clear();
            var kutya = kutyaLista.Where(k => k.Nev.Equals(Kutyak.SelectedItem.ToString())).First();
            KutyaAdatok.Items.Add(kutya.Kan ? "Kan" : "Szuka");
            KutyaAdatok.Items.Add("Fajta: " + kutya.Fajta.FajtaNev);
            KutyaAdatok.Items.Add("Gazda: " + kutya.Gazda.Nev);
            KutyaAdatok.Items.Add("Telefon: " + kutya.Gazda.Tel);
            KutyaAdatok.Items.Add($"Kor:  {kutya.Kor} éves");
            KutyaAdatok.Items.Add($"Chippelés: {kutya.ChipDatum:yyyy.MM.dd.}");
        }
    }
}