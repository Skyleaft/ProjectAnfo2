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
    /// Interaction logic for DialogPilihMessage.xaml
    /// </summary>
    public partial class DialogPilihMessage : UserControl
    {

        koneksi k = new koneksi();
        public DialogPilihMessage()
        {
            InitializeComponent();
            showdata();
        }

        public string idmsg;
        public event Action<string> Check;

        public void showdata()
        {
            k.sql = "select * from tb_message";
            k.setdt();
            dg_msg.ItemsSource = k.dt.DefaultView;

        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            if (dg_msg.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_msg.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from tb_message  where id_message = '" + index + "'";
                k.setdt();

                idmsg = k.dt.Rows[0][0].ToString();
                Check(idmsg);


                DialogHost.CloseDialogCommand.Execute(null, this);


            }
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
