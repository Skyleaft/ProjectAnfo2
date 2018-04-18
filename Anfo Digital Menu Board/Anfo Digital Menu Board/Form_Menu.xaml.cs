using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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

namespace Anfo_Digital_Menu_Board
{
    /// <summary>
    /// Interaction logic for Form_Menu.xaml
    /// </summary>
    public partial class Form_Menu : UserControl
    {
        public Form_Menu()
        {
            InitializeComponent();
            showdata();
        }

        koneksi k = new koneksi();
        private String alamat_foto;

        public void showdata()
        {
            k.sql = "select foto,id_produk,nama,harga from tb_produk";
            k.setdt();
            dg_menu.ItemsSource = k.dt.DefaultView;

        }

        public void bersih()
        {
            txt_harga.Text = "";
            txt_kdmenu.Text = "";
            txt_namamenu.Text = "";
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
            if (txt_kdmenu.Text=="" || txt_namamenu.Text == "" || txt_harga.Text == "")
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
                    k.sql = "insert into tb_produk select '" + txt_kdmenu.Text + "','" + txt_namamenu.Text + "','" + txt_harga.Text + "',bulkcolumn from openrowset(bulk'" + alamat_foto + "',single_blob) as gambar";
                    k.setdt();

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                    showdata();
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
        }
    }
}
