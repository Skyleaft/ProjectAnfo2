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
    /// Interaction logic for DialogPilihKatalog.xaml
    /// </summary>
    public partial class DialogPilihKatalog : UserControl
    {
        public DialogPilihKatalog()
        {
            InitializeComponent();
            showdata();
        }

        public string idktlog;

        public event Action<string> Check;
        koneksi k = new koneksi();

        public void showdata()
        {
            k.sql = "select * from tb_katalog";
            k.setdt();
            dg_katalog.ItemsSource = k.dt.DefaultView;

        }

        private void btn_pilih_Click(object sender, RoutedEventArgs e)
        {
            if (dg_katalog.SelectedIndex >= 0)
            {
                DataRowView dataRow = (DataRowView)dg_katalog.SelectedItem;
                string index = dataRow.Row[0].ToString();

                k.sql = "select *from tb_katalog  where id_katalog = '" + index + "'";
                k.setdt();

                idktlog = k.dt.Rows[0][0].ToString();
                Check(idktlog);


                DialogHost.CloseDialogCommand.Execute(null, this);


            }
        }

        private void txt_cari_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
