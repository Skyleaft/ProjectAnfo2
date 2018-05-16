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

namespace Anfo_Digital_Menu_Board.DialogPilihProduk

{
    /// <summary>
    /// Interaction logic for DialogDetailKatalog.xaml
    /// </summary>
    public partial class DialogDetailKatalog : UserControl
    {
        koneksi k = new koneksi();
        
        public string idktlog;

        string _idprod;
        public DialogDetailKatalog(String iddetkatalog)
        {
            InitializeComponent();
            DataContext = this;
            idktlog = iddetkatalog;

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            txt_diskon.Text = Convert.ToDecimal(0).ToString("c");

            k.sql = "select * from tb_detail_katalog inner join tb_produk " +
                    "on tb_detail_katalog.id_produk = tb_produk.id_produk where tb_produk.nama = '" + iddetkatalog + "'";
            k.setdt();
            foreach (DataRow baris in k.dt.Rows)
            {
                txt_idprod.Text = baris[1].ToString();
                txt_nama.Text = baris[6].ToString();

                txt_harga.Text = Convert.ToDecimal(baris[8].ToString()).ToString("c");
                txt_diskon.Text = baris[2].ToString();
                lb_diskon.Content = Convert.ToDecimal(baris[3].ToString()).ToString("c"); 
            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            k.sql = "select *from tb_produk where id_produk = '" + _idprod + "'";
            k.setdt();
            foreach (DataRow baris in k.dt.Rows)
            {
                txt_idprod.Text = baris[0].ToString();
                txt_nama.Text = baris[1].ToString();

                txt_harga.Text = Convert.ToDecimal(baris[4].ToString()).ToString("c");
                txt_diskon.Text = "";
                lb_diskon.Content = txt_harga.Text;
            }
        }

        private void txt_diskon_TextInput(object sender, TextCompositionEventArgs e)
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

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Hapus data Message : " + txt_idprod.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from tb_detail_katalog where id_produk = '" + txt_idprod.Text + "'";
            k.setdt();

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Hapus" }
            };
            DialogHost.CloseDialogCommand.Execute(null, this);

            DialogHost.Show(sampleMessageDialog, "MainDialog");
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            int harga = int.Parse(lb_diskon.Content.ToString(), System.Globalization.NumberStyles.Currency);
            try
            {
                k.sql = "update tb_detail_katalog set diskon=@diskon,harga_diskon=@hardis where id_produk = '" + txt_idprod.Text + "'";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@diskon", txt_diskon.Text);
                k.perintah.Parameters.AddWithValue("@hardis", harga);

                k.perintah.ExecuteNonQuery();
                k.close();

                DialogHost.CloseDialogCommand.Execute(null, this);

                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Data Berhasil Diubah" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");


            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.ToString());
            }
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
                    lb_diskon.Content = txt_harga.Text;
                }


            }
            else
            {
                lb_diskon.Content = txt_harga.Text;

            }
        }

        
        private void btn_pilih_Click_1(object sender, RoutedEventArgs e)
        {
            var showdialog = new DialogPilihProduk();
            showdialog.Check += value => _idprod = value;
            DialogHost.Show(showdialog, "KatalogDialog", ClosingEventHandler);

            //var showdialog = new DialogPilihProduk();
            //DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }
    }
}
