using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
            DataContext = this;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            txt_diskon.Text = Convert.ToDecimal(0).ToString("c");
            
        }


        koneksi k = new koneksi();
        string _idprod;

        public void bersih() {
            txt_diskon.Text = "";
            txt_harga.Text = "";
            txt_idprod.Text = "";
            txt_nama.Text = "";
            lb_diskon.Content = "Rp.0";
        }

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

                txt_harga.Text = Convert.ToDecimal(baris[3].ToString()).ToString("c");
            }
        }

        private void txt_idprod_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txt_diskon_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txt_diskon.Text != "")
            {
                int val = 0;
                bool res = Int32.TryParse(txt_diskon.Text, out val);
                if (res == true && val > -1 && val < 101)
                {
                    // add record
                    double hdiskon;
                    int harga = int.Parse(txt_harga.Text, NumberStyles.Currency);
                    double diskon = double.Parse(txt_diskon.Text);

                    hdiskon = harga * (diskon / 100);
                    double hargasetelah = harga - hdiskon;
                    lb_diskon.Content = Convert.ToDecimal(hargasetelah.ToString()).ToString("c");
                }
                else
                {
                    txt_diskon.Text = "";
                    lb_diskon.Content = "Rp.0";
                }

                
            }
            else
            {
                lb_diskon.Content = "Rp.0";

            }
        }

        private void txt_diskon_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            //if (txt_idprod.Text == "" || txt_nama.Text == "" || txt_harga.Text == "" || txt_diskon.Text == "")
            //{
            //var sampleMessageDialog = new SampleMessageDialog
            //{
            //    Message = { Text = "Lengkapi Dulu Data" }
            //};
            //DialogHost.Show(sampleMessageDialog, "MainDialog");
            //}
            //else
            //{

            String kont = lb_diskon.Content.ToString();
            int harga = int.Parse(lb_diskon.Content.ToString(), System.Globalization.NumberStyles.Currency);

            try
                {
                    k.sql = "insert into tb_detail_katalog values(@id_katalog,@id_produk,@diskon,@harga_diskon)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@id_katalog", txt_idprod.Text);
                    k.perintah.Parameters.AddWithValue("@id_produk", txt_idprod.Text);
                    k.perintah.Parameters.AddWithValue("@diskon", txt_diskon.Text);
                    k.perintah.Parameters.AddWithValue("@harga_diskon", harga);

                    k.perintah.ExecuteNonQuery();
                    k.close();

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                    bersih();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            //}
        }
    }
}
