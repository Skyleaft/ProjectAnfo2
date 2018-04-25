using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
    /// Interaction logic for DialogProduk.xaml
    /// </summary>
    public partial class DialogProduk : UserControl
    {
        koneksi k = new koneksi();
        private String alamat_foto;

        public DialogProduk(String id_produk)
        {
            InitializeComponent();
            

            k.sql = "select *from tb_produk where id_produk ='"+id_produk+"'";
            k.setdt();
            Byte[] tmpfoto = new Byte[0];
            foreach (DataRow baris in k.dt.Rows)
            {
                txt_id.Text = baris[0].ToString();
                txt_nama.Text = baris[1].ToString();
                if (baris[2].ToString() == "Makanan")
                {
                    rb_makanan.IsChecked = true;
                }
                else
                {
                    rb_minuman.IsChecked = true;
                }
                txt_harga.Text = Convert.ToDecimal(baris[3].ToString()).ToString("c");

                tmpfoto = (Byte[])baris[4];
                img_foto.Source = ByteImageConverter.ByteToImage(tmpfoto);
            }
            
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

        private void txt_harga_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1)) e.Handled = true;
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

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            String jenis = "";
            if (rb_makanan.IsChecked == true)
            {
                jenis = "Makanan";
            }
            else
            {
                jenis = "Minuman";
            }

            int harga = int.Parse(txt_harga.Text, System.Globalization.NumberStyles.Currency);
            var img = ByteImageConverter.ConvertBitmapSourceToByteArray(img_foto.Source);

            try
            {
                

                k.sql = "update tb_produk set nama=@nama,jenis=@jenis,harga=@harga,foto=@foto where id_produk = '"+txt_id.Text+"'";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@nama", txt_nama.Text);
                k.perintah.Parameters.AddWithValue("@jenis", jenis);
                k.perintah.Parameters.AddWithValue("@harga", harga);
                k.perintah.Parameters.AddWithValue("@foto", img);

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
                MessageBox.Show("error "+ex.ToString());
            }
        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Yakin Hapus Produk : " + txt_id.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from tb_produk where id_produk = '" + txt_id.Text + "'";
            k.setdt();

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Hapus" }
            };
            DialogHost.CloseDialogCommand.Execute(null, this);

            DialogHost.Show(sampleMessageDialog, "MainDialog");
            
        }
    }
}
