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
        }

        public void bersih() {
            txt_username.Text = "";
            txt_password.Password = "";
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
    }
}
