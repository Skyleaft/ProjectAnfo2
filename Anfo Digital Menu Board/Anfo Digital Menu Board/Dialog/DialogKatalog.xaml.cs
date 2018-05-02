using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for DialogKatalog.xaml
    /// </summary>
    public partial class DialogKatalog : UserControl
    {
        public DialogKatalog()
        {
            InitializeComponent();
        }
        koneksi k = new koneksi();
        string _idprod;

        private void txt_harga_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void txt_harga_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new DialogPilihProduk();
            showdialog.Check += value => _idprod = value;
            DialogHost.Show(showdialog, "KatalogDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            k.sql = "select *from tb_produk where id_produk = '" + _idprod + "'";
            k.setdt();
            foreach (DataRow baris in k.dt.Rows)
            {
                txt_idprod.Text = baris[0].ToString();
                txt_nama.Text = baris[1].ToString();
                txt_harga.Text = baris[3].ToString();
            }
        }
    }
}
