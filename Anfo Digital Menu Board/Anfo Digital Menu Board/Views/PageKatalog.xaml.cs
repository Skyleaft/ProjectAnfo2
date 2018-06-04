using Anfo_Digital_Menu_Board.DialogPilihProduk;
using MaterialDesignColors.WpfExample.Domain;
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

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageKatalog.xaml
    /// </summary>
    public partial class PageKatalog : UserControl
    {
        koneksi k = new koneksi();

        public PageKatalog()
        {
            InitializeComponent();
            kodeotomatis();
            showdataktlog();


    }

        private void showdataprod()
        {
            k.sql = "select * from tb_detail_katalog inner join tb_produk on tb_detail_katalog.id_produk = tb_produk.id_produk where tb_detail_katalog.id_katalog = '"+txt_id.Text+"'";
            k.setdt();
            dg_produk.ItemsSource = k.dt.DefaultView;

        }

        private void showdataktlog()
        {
            k.sql = "select * from tb_katalog";
            k.setdt();
            dg_katalog.ItemsSource = k.dt.DefaultView;

        }

        private void bersih()
        {
            txt_deskripsi.Text = "";
            txt_id.Text = "";
            //dg_produk.Items.Clear();
            dg_produk.ItemsSource = null;
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
                baru = "KL-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "KL-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "KL-0" + tambah;
                }
                else
                {
                    baru = "KL-" + tambah;
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
                    showdataktlog();
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
            var showdialog = new DialogKatalog(txt_id.Text);
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdataprod();
        }

        private void dg_produk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_produk.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_produk.SelectedItem;
                string index = dataRow.Row[5].ToString();

                k.sql = "select * from tb_detail_katalog inner join tb_produk " +
                    "on tb_detail_katalog.id_produk = tb_produk.id_produk where tb_produk.nama = '" + index + "'";
                k.setdt();

                String iddetkatalog = k.dt.Rows[0][5].ToString();

                var showdialog = new DialogDetailKatalog(iddetkatalog);
                DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);

            }
            ////if (dg_produk.SelectedIndex >= 0)
            ////{
            ////    k.sql = "select * from tb_detail_katalog inner join tb_produk " +
            ////            "on tb_detail_katalog.id_produk = tb_produk.id_produk where tb_detail_katalog.id_katalog = '" + txt_id.Text + "'";
            ////    k.setdt();

            ////    DataRowView dataRow = (DataRowView)dg_produk.SelectedItem;
            ////    string index = dataRow.Row[1].ToString();

            ////    try
            ////    {
            ////        k.sql = "delete from tb_detail_katalog where id_produk = '" + index + "'";
            ////        k.setdt();
            ////        showdataprod();

            ////        var sampleMessageDialog = new SampleMessageDialog
            ////        {
            ////            Message = { Text = "Data Berhasil Diubah" }
            ////        };
            ////        DialogHost.Show(sampleMessageDialog, "MainDialog");
            ////    }
            ////    catch (Exception ex)
            ////    {
            ////        MessageBox.Show("error " + ex.ToString());

            ////    }

        //}
                

        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {
            k.sql = "select * from tb_katalog where deskripsi like'%" + txt_cari.Text + "%' or id_katalog like'%" + txt_cari.Text + "%' ";
            k.setdt();
            dg_katalog.ItemsSource = k.dt.DefaultView;
        }

        private void dg_katalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_katalog.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_katalog.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from tb_katalog  where id_katalog = '" + index + "'";
                k.setdt();

                txt_id.Text = k.dt.Rows[0][0].ToString();
                txt_deskripsi.Text = k.dt.Rows[0][1].ToString();

                showdataprod();

            }
        }
    }
}
