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

        public string idprod;

        public event Action<string> Check;

        public void showdata()
        {
            k.sql = "select * from tb_produk";
            k.setdt();
            dg_produk.ItemsSource = k.dt.DefaultView;

        }

        private void dg_produk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            if (dg_produk.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_produk.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from tb_produk  where id_produk = '" + index + "'";
                k.setdt();

                idprod = k.dt.Rows[0][0].ToString();
                Check(idprod);

                
                DialogHost.CloseDialogCommand.Execute(null, this);


            }
            
        }
    }
}
