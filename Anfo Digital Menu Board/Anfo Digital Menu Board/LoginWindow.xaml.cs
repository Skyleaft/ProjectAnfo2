using MahApps.Metro.Controls;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Anfo_Digital_Menu_Board
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        koneksi k = new koneksi();
        public LoginWindow()
        {
            InitializeComponent();
            cek();
        }

        public void bersih() {
            txt_username.Text = "";
            txt_password.Password = "";
        }

        public void bersihreg()
        {
            txt_username2.Text = "";
            txt_nama.Text = "";
            txt_password2.Password = "";
        }

        private void keluar(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void dissapear()
        {
            this.Closing -= MetroWindow_Closing;
            this.Closing += keluar;
            Close();
        }

        private void cek()
        {
            String pass = "";
            String user = "";
            k.sql = "select *from tb_user";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            foreach (DataRow baris in k.dt.Rows)
            {
                user = baris[1].ToString();
                pass = baris[2].ToString();
            }

            if (cekbaris == 0)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Anda Belum punya akun" }
                };
                DialogHost.Show(sampleMessageDialog, "AkunDialog");
                txt_username.Text = "";
                txt_password.Password = "";

                ////DaftarAkun wm = new DaftarAkun();
                ////wm.Show();
                ////dissapear();
            }
            else {
            }

        }


        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            String pass = "";
            String user = "";
            k.sql = "select *from tb_user where " +
                "username='" + txt_username.Text + "' and password='" + txt_password.Password + "'";
            k.setdt();
            int cekbaris = k.dt.Rows.Count;
            foreach (DataRow baris in k.dt.Rows)
            {
                user = baris[1].ToString();
                pass = baris[2].ToString();
            }


            if (txt_username.Text == "" && txt_password.Password == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Username dan Password Belum Di isi" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
            }
            else if (cekbaris == 0)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Username atau Password Salah" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
                txt_username.Text = "";
                txt_password.Password = "";
            }
            else {
                MainWindow wm = new MainWindow();
                wm.Show();
                dissapear();

            }


        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            bersihreg();
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

        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            //pn_log.Visibility = Visibility.Collapsed;
            //pn_reg.Visibility = Visibility.Visible;

            Storyboard sb = this.FindResource("slide1") as Storyboard;
            sb.Begin();

            

            rect_reg.Visibility = Visibility.Visible;
            rect_log.Visibility = Visibility.Hidden;
        }

        private void btn_log_Click(object sender, RoutedEventArgs e)
        {
            //pn_log.Visibility = Visibility.Visible;
            //pn_reg.Visibility = Visibility.Collapsed;

            Storyboard sb = this.FindResource("slide2") as Storyboard;
            sb.Begin();


            rect_reg.Visibility = Visibility.Hidden;
            rect_log.Visibility = Visibility.Visible;
        }

        private void txt_username2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            if (txt_username2.Text == "" || txt_nama.Text == "" || txt_password2.Password == "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Lengkapi Dulu Data" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
            }
            else if (txt_password2.Password.Length<8)
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Password minimal 8 karakter" }
                };
                DialogHost.Show(sampleMessageDialog, "LoginDialog");
            }
            else
            {
                try
                {
                    k.sql = "insert into tb_user values(@user,@nama,@pass)";
                    k.setparam();
                    k.perintah.Parameters.AddWithValue("@user", txt_username2.Text);
                    k.perintah.Parameters.AddWithValue("@nama", txt_nama.Text);
                    k.perintah.Parameters.AddWithValue("@pass", txt_password2.Password);
                    k.perintah.ExecuteNonQuery();
                    k.close();

                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Data Berhasil Tersimpan" }
                    };
                    DialogHost.Show(sampleMessageDialog, "LoginDialog");
                    bersih();

                    Storyboard sb = this.FindResource("slide2") as Storyboard;
                    sb.Begin();
                    rect_reg.Visibility = Visibility.Hidden;
                    rect_log.Visibility = Visibility.Visible;

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Data Gagal Didaftarkan " + ex);
                }
            }
        }
    }
}
