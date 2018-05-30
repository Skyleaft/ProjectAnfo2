using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace Anfo_Digital_Menu_Board
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DaftarAkun : MetroWindow
    {
        koneksi k = new koneksi();
        public DaftarAkun()
        {
            InitializeComponent();
            kodeotomatis();
        }
            
        public void bersih()
        {
            txt_username.Text = "";
            txt_password.Password = "";
            txt_id.Text = "";
        }

        private void dissapear()
        {
            this.Closing -= MetroWindow_Closing;
            this.Closing += keluar;
            Close();
        }

        private void kodeotomatis()
        {
            k.sql = "select *from tb_user order by id_user asc";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            String baru;
            int tambah;
            if (cekbaris == 0)
            {
                baru = "KR-001";
            }
            else
            {
                tambah = Convert.ToInt32(k.dt.Rows[cekbaris - 1][0].ToString().Split('-')[1]) + 1;
                if (tambah < 10)
                {
                    baru = "KR-00" + tambah;
                }
                else if (tambah < 100)
                {
                    baru = "KR-0" + tambah;
                }
                else
                {
                    baru = "KR-" + tambah;
                }
            }
            txt_id.Text = baru;
        }

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            if (txt_id.Text == "" || txt_username.Text == "" || txt_password.Password == "")
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
                    k.sql = "insert into tb_user values(@id,@username,@password)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@id", txt_id.Text);
                    k.perintah.Parameters.AddWithValue("@username", txt_username.Text);
                    k.perintah.Parameters.AddWithValue("@password", txt_password.Password);
                    k.perintah.ExecuteNonQuery();
                    k.close();

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");

                    bersih();
                    kodeotomatis();

                    MainWindow wm = new MainWindow();
                    wm.Show();
                    dissapear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            bersih();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var keluar = new SampleMessageYesNo
            {
                Message = { Text = "Yakin Keluar Aplikasi?" }
            };
            DialogHost.Show(keluar, "LoginDialog");
        }

        private void keluar(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
