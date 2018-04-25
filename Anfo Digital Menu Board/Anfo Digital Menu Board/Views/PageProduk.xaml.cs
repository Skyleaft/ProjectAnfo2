using Anfo_Digital_Menu_Board.Dialog;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageMakanan.xaml
    /// </summary>
    public partial class PageProduk : Page
    {

        koneksi k = new koneksi();

        private String alamat_foto;

        public PageProduk()
        {
            InitializeComponent();

            showdata();
            kodeotomatis();

            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("id-ID");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("id-ID");

            txt_harga.Text = Convert.ToDecimal(0).ToString("c");
        }

       
        public void showdata()
        {
            k.sql = "select foto,nama,jenis,harga,id_produk from tb_produk";
            k.setdt();
            dg_produk.ItemsSource = k.dt.DefaultView;

        }

        private void kodeotomatis()
        {
            k.sql = "select *from tb_produk order by id_produk asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "MN-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "MN-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "MN-0" + tambah;
                }
                else
                {
                    baru = "NL-" + tambah;
                }
            }
            txt_id.Text = baru;
        }

        public void bersih()
        {
            txt_id.Text = "";
            txt_nama.Text = "";
            txt_harga.Text = "";
            txt_harga.Text = Convert.ToDecimal(0).ToString("c");
            rb_makanan.IsChecked = false;
            rb_minuman.IsChecked = false;
            img_foto.Source = null;
        }

        private void btn_ambil_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Masukan Foto";
            op.Filter = "Semua Gambar|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                alamat_foto = op.FileName;
                img_foto.Source = new BitmapImage(new Uri(op.FileName));
                
            }
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_id.Text == "" || txt_nama.Text == "" || txt_harga.Text == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");
            }
            else
            {
                try
                {
                    String jenis = "";
                    int harga = int.Parse(txt_harga.Text, System.Globalization.NumberStyles.Currency);

                    if (rb_makanan.IsChecked == true)
                    {
                        jenis = "Makanan";
                    }
                    else
                    {
                        jenis = "Minuman";
                    }

                    var img = ByteImageConverter.ConvertBitmapSourceToByteArray(img_foto.Source);

                    k.sql = "insert into tb_produk values(@id,@nama,@jenis,@harga,@foto)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@id",txt_id.Text);
                    k.perintah.Parameters.AddWithValue("@nama", txt_nama.Text);
                    k.perintah.Parameters.AddWithValue("@jenis", jenis);
                    k.perintah.Parameters.AddWithValue("@harga", harga);
                    k.perintah.Parameters.AddWithValue("@foto", img);
                    
                    k.perintah.ExecuteNonQuery();
                    k.close();

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                    showdata();
                    bersih();
                    kodeotomatis();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
        }

        private void btn_batal_Click(object sender, RoutedEventArgs e)
        {
            bersih();
            kodeotomatis();
        }

        private void txt_harga_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
            
        }

        private void txt_harga_TextChanged(object sender, TextChangedEventArgs e)
        {
            UInt32 a = 0;
            if (txt_harga.Text.Length <= 2) // jika panjang karakter pada textbox1 <= 2
            {
                txt_harga.Text = Convert.ToDecimal(0).ToString("c");
            }
            else
            {
                txt_harga.Text = decimal.Parse(txt_harga.Text, System.Globalization.NumberStyles.Currency).ToString("c");
                a = UInt32.Parse(txt_harga.Text, System.Globalization.NumberStyles.Currency);

                txt_harga.SelectionStart = txt_harga.Text.Length; // menetapkan titik awal dari teks yang dipilih pada textbox
            }
        }


        private void dg_produk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_produk.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_produk.SelectedItem;
                string index = dataRow.Row[4].ToString();

                k.sql = "select *from tb_produk  where id_produk = '" + index + "'";
                k.setdt();

                String idprod = k.dt.Rows[0][0].ToString();

                var showdialog = new DialogProduk(idprod);
                DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);

            }
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdata();
        }
    }
}
