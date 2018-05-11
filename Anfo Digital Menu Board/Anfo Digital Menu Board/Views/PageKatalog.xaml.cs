using Anfo_Digital_Menu_Board.Dialog;
using MaterialDesignColors.WpfExample.Domain;
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
            kodeotomatis();
        }

        public void showdataprod()
        {
            //k.sql = "select foto,nama,jenis,harga,id_produk from tb_produk";
            //k.setdt();
            //dg_produk.ItemsSource = k.dt.DefaultView;

        }

        public void bersih()
        {
            txt_deskripsi.Text = "";
            txt_id.Text = "";
        }

        private void kodeotomatis()
        {
            k.sql = "select * from tb_katalog order by id_katalog asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "KT-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "KT-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "KT-0" + tambah;
                }
                else
                {
                    baru = "KT-" + tambah;
                }
            }
            txt_id.Text = baru;
        }

        private void btn_batal_Click(object sender, RoutedEventArgs e)
        {
            bersih();
            kodeotomatis();
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_id.Text == "" || txt_deskripsi.Text == "")
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
                    k.sql = "insert into tb_katalog select '" + txt_id.Text + "','" + txt_deskripsi.Text + "'";
                    k.setdt();

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                    bersih();
                    kodeotomatis();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
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
