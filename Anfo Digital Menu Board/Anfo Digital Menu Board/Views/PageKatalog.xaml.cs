using Anfo_Digital_Menu_Board.Dialog;
using MaterialDesignThemes.Wpf;
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

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageKatalog.xaml
    /// </summary>
    public partial class PageKatalog : Page
    {
        koneksi k = new koneksi();

        public PageKatalog()
        {
            InitializeComponent();
        }

        public void showdataprod()
        {
            //k.sql = "select foto,nama,jenis,harga,id_produk from tb_produk";
            //k.setdt();
            //dg_produk.ItemsSource = k.dt.DefaultView;

        }

        private void btn_batal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_tambah_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new DialogKatalog();
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdataprod();
        }

        private void dg_produk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
