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

namespace Anfo_Digital_Menu_Board.Dialog
{
    /// <summary>
    /// Interaction logic for DialogMessage.xaml
    /// </summary>
    public partial class DialogMessage : UserControl
    {
        koneksi k = new koneksi();
        public DialogMessage(String id_message)
        {
            InitializeComponent();

            k.sql = "select *from tb_message where id_message ='" + id_message + "'";
            k.setdt();
            Byte[] tmpfoto = new Byte[0];
            foreach (DataRow baris in k.dt.Rows)
            {
                txt_id.Text = baris[0].ToString();
                txt_msg.Text = baris[1].ToString();
                txt_alert.Text = baris[2].ToString();
            }
        }

        public DialogMessage()
        {
        }

        private void btn_ubah_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                k.sql = "update tb_message set message=@message,alert=@alert where id_message = '" + txt_id.Text + "'";
                k.setparam();
                k.perintah.Parameters.AddWithValue("@message", txt_msg.Text);
                k.perintah.Parameters.AddWithValue("@alert", txt_alert.Text);
                
                k.perintah.ExecuteNonQuery();
                k.close();

                DialogHost.CloseDialogCommand.Execute(null, this);

                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Data Berhasil Diubah" }
                };
                DialogHost.Show(sampleMessageDialog, "MainDialog");


            }
            catch (Exception ex)
            {
                MessageBox.Show("error " + ex.ToString());
            }
        }

        private void btn_hapus_Click(object sender, RoutedEventArgs e)
        {
            Message.Text = "Hapus data Message : "+ txt_id.Text;
        }

        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            k.sql = "delete from tb_message where id_message = '" + txt_id.Text + "'";
            k.setdt();

            var sampleMessageDialog = new SampleMessageDialog
            {
                Message = { Text = "Data Berhasil Di Hapus" }
            };
            DialogHost.CloseDialogCommand.Execute(null, this);

            DialogHost.Show(sampleMessageDialog, "MainDialog");
        }

        private void btn_batal_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
