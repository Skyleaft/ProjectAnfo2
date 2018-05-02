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

namespace Anfo_Digital_Menu_Board.Dialog
{
    /// <summary>
    /// Interaction logic for DialogPilihProduk.xaml
    /// </summary>
    public partial class DialogPilihProduk : UserControl
    {
        koneksi k = new koneksi();

        //private String alamat_foto;

        public DialogPilihProduk()
        {
            InitializeComponent();

            showdata();
        }

        public void showdata()
        {
            k.sql = "select foto,nama,jenis,harga,id_produk from tb_produk";
            k.setdt();
            dg_produk.ItemsSource = k.dt.DefaultView;

        }

        private void dg_produk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
