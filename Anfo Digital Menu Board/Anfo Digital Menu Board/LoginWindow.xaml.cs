using MahApps.Metro.Controls;
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

        private void btn_login_Click(object sender, RoutedEventArgs e)
        {
            try {
                k.sql = "select *from tb_user where username = '" + txt_username.Text + "' and password = '" + txt_password.Password + "'";
                k.setdt();

                var showdialog = new MainWindow();
                DialogHost.Show(showdialog, "MainDialog");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Username atau Password salah " + ex);
            }
            
            
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            bersih();
        }
    }
}
