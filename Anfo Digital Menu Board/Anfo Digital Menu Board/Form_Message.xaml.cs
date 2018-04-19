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

namespace Anfo_Digital_Menu_Board.Resource
{
    /// <summary>
    /// Interaction logic for Form_Message.xaml
    /// </summary>
    public partial class Form_Message : UserControl
    {
        public Form_Message()
        {
            InitializeComponent();
            showdata();
        }

        koneksi k = new koneksi();
        
        public void showdata()
        {
            k.sql = "select * from tb_message";
            k.setdt();
            dg_message.ItemsSource = k.dt.DefaultView;

        }

        public void bersih()
        {
            txt_alert.Text = "";
            txt_kdmenu.Text = "";
            txt_msg.Text = "";
        }

        private void btn_simpan_Click(object sender, RoutedEventArgs e)
        {
            if (txt_kdmenu.Text == "" || txt_msg.Text == "" || txt_alert.Text == "")
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
                    k.sql = "insert into tb_message select '" + txt_kdmenu.Text + "','" + txt_msg.Text + "','" + txt_alert.Text + "'";
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
            showdata();
        }
    }
}
