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
using DataGrid.MusicSalesDataSetTableAdapters;

namespace DataGrid
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

        private MusicSalesDataSet musicSalesDataSet;
        private ArtistsTableAdapter artistsTableAdapter;
        private CollectionViewSource artistsViewSource;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            musicSalesDataSet = ((DataGrid.MusicSalesDataSet)(this.FindResource("musicSalesDataSet")));
            // Load data into the table Artists. You can modify this code as needed.
            artistsTableAdapter = new DataGrid.MusicSalesDataSetTableAdapters.ArtistsTableAdapter();
            artistsTableAdapter.Fill(musicSalesDataSet.Artists);
            artistsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("artistsViewSource")));
            artistsViewSource.View.MoveCurrentToFirst();
        }

        private void highsalesbutton_Click(object sender, RoutedEventArgs e)
        {
            decimal minsales = Convert.ToDecimal(salesabovetextbox.Text);
            artistsTableAdapter.FillBySales(musicSalesDataSet.Artists, minsales);
        }
    }
}
