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
    /// Interaction logic for DialogMessage.xaml
    /// </summary>
    public partial class DialogMessage : UserControl
    {
        koneksi k = new koneksi();

        public DialogMessage()
        {
            InitializeComponent();
            showdata();
        }

        public void showdata()
        {
            k.sql = "select id_message,message,alert from tb_message";
            k.setdt();
            dg_message.ItemsSource = k.dt.DefaultView;

        }

        public void bersih()
        {
            txt_alert.Text = "";
            txt_id.Text = "";
            txt_msg.Text = "";
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                k.sql = "update tb_message set id_message = '" + txt_id.Text + ",message = '" + txt_msg.Text + ",alert = '" + txt_alert.Text + " where id_message = '" + txt_id.Text + "'";
                k.setdt();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            //MessageBox.Show("Data Berhasil Di Ubah", "Pemberitahuan", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Ubah" }
            };
            DialogHost.Show(sampleMessageDialog, "MainDialog");
            bersih();
            showdata();
        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
