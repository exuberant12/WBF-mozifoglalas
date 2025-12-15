using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WBF_mozifoglalas
{
    public partial class MainWindow : Window
    {
        
        public class Mozi : INotifyPropertyChanged
        {
            private int _szabadhelyek;

            public string cim { get; set; }
            public DateTime idopont { get; set; }
            public string terem { get; set; }

            public int szabadhelyek
            {
                get => _szabadhelyek;
                set
                {
                    _szabadhelyek = value;
                    PropertyChanged?.Invoke(this,
                        new PropertyChangedEventArgs(nameof(szabadhelyek)));
                }
            }

            public bool _3D { get; set; }

            public Mozi(string cim, DateTime idopont, string terem, int szabadhelyek, bool _3D)
            {
                this.cim = cim;
                this.idopont = idopont;
                this.terem = terem;
                this.szabadhelyek = szabadhelyek;
                this._3D = _3D;
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }

        // 🔹 Lista
        ObservableCollection<Mozi> mozifilmek;

        public MainWindow()
        {
            InitializeComponent();

            mozifilmek = new ObservableCollection<Mozi>()
            {
                new Mozi("FNAF 1", new DateTime(2025,12,15,18,0,0), "1-es terem", 12, true),
                new Mozi("FNAF 2", new DateTime(2026,12,15,18,0,0), "2-es terem", 5, false),
                new Mozi("FNAF 3", new DateTime(2027,12,15,18,0,0), "3-as terem", 0, true),
                new Mozi("FNAF 4", new DateTime(2028,12,15,18,0,0), "4-es terem", 20, false),
            };

            DataGrid.ItemsSource = mozifilmek;
        }

        private void adatokbetoltese(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource = mozifilmek;
        }

        private void foglalas(object sender, RoutedEventArgs e)
        {
            if (DataGrid.SelectedItem is Mozi m)
            {
                if (m.szabadhelyek > 0)
                    m.szabadhelyek--;
                else
                    MessageBox.Show("Nincs több szabad hely!");
            }
        }

        private void csakVanHely(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource =
                new ObservableCollection<Mozi>(mozifilmek.Where(x => x.szabadhelyek > 0));
        }

        private void csak3D(object sender, RoutedEventArgs e)
        {
            DataGrid.ItemsSource =
                new ObservableCollection<Mozi>(mozifilmek.Where(x => x._3D));
        }

        private void legnepszerubb(object sender, RoutedEventArgs e)
        {
            var film = mozifilmek.OrderBy(x => x.szabadhelyek).First();
            MessageBox.Show($"Legnépszerűbb film:\n{film.cim}");
        }

        private void atlagSzabadHely(object sender, RoutedEventArgs e)
        {
            double atlag = mozifilmek.Average(x => x.szabadhelyek);
            MessageBox.Show($"Átlagos szabad hely: {atlag:F1}");
        }

        private void hozzaadas(object sender, RoutedEventArgs e)
        {
            try
            {
                mozifilmek.Add(new Mozi(
                    txtCim.Text,
                    DateTime.Parse(txtIdopont.Text),
                    txtTerem.Text,
                    int.Parse(txtSzabad.Text),
                    chk3D.IsChecked == true
                ));

                
                DataGrid.ItemsSource = mozifilmek;
            }
            catch
            {
                MessageBox.Show("Hibás adat!");
            }
        }

    }
}
