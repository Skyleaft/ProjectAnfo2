using Anfo_Digital_Menu_Board.Dialog;
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
    /// Interaction logic for PageMessage.xaml
    /// </summary>
    public partial class PageMessage : UserControl
    {
        koneksi k = new koneksi();

        public PageMessage()
        {
            InitializeComponent();
            showdata();
            kodeotomatis();
        }

        public void showdata()
        {
            k.sql = "select * from tb_message";
            k.setdt();
            dg_message.ItemsSource = k.dt.DefaultView;

        }

        public void bersih() {
            txt_alert.Text = "";
            txt_id.Text = "";
            txt_msg.Text = "";
        }

        private void kodeotomatis()
        {
            k.sql = "select * from tb_message order by id_message asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "MS-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "MS-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "MS-0" + tambah;
                }
                else
                {
                    baru = "MS-" + tambah;
                }
            }
            txt_id.Text = baru;
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_id.Text == "" || txt_msg.Text == "" || txt_alert.Text == "")
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
                    k.sql = "insert into tb_message select '" + txt_id.Text + "','" + txt_msg.Text + "','" + txt_alert.Text + "'";
                    k.setdt();

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
            showdata();
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            showdata();
        }

        private void dg_message_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg_message.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_message.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from tb_message  where id_message = '" + index + "'";
                k.setdt();

                String idmessage = k.dt.Rows[0][0].ToString();

                var showdialog = new DialogMessage(idmessage);

                DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);

            }
        }
    }

   

}
