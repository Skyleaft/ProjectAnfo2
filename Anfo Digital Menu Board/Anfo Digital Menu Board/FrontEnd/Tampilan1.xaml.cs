using MahApps.Metro.Controls;
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

namespace Anfo_Digital_Menu_Board.FrontEnd
{
    /// <summary>
    /// Interaction logic for Tampilan1.xaml
    /// </summary>
    public partial class Tampilan1 : MetroWindow
    {
        koneksi k = new koneksi();
        private string idktlog;
        public Tampilan1()
        {
            InitializeComponent();
        }

        public void showdata(string _idktlog)
        {
            k.sql = "select *from tb_katalog inner join tb_detail_katalog on tb_katalog.id_katalog = tb_detail_katalog.id_katalog inner join tb_produk on tb_detail_katalog.id_produk = tb_produk.id_produk where tb_katalog.id_katalog = '"+_idktlog+"'";
            k.setdt();
            lv_ktlog.ItemsSource = k.dt.DefaultView;

        }

        public void tampilsemua()
        {
            k.sql = "select *from tb_produk";
            k.setdt();
            lv_ktlog.ItemsSource = k.dt.DefaultView;
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.SystemKey == Key.F10)
            {
                if (this.ShowTitleBar == true)
                {
                    this.ShowTitleBar = false;
                    this.ShowCloseButton = false;
                }
                else
                {
                    this.ShowTitleBar = true;
                    this.ShowCloseButton = true;
                }
            }
        }

        private void btn_setting_Click(object sender, RoutedEventArgs e)
        {
            if (fy_atas.IsOpen == true)
            {
                fy_atas.IsOpen = false;
            }
            else
            {
                fy_atas.IsOpen = true;
            }
        }

    }
}
