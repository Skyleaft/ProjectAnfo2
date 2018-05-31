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
using System.Windows.Threading;

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for MenuPage.xaml
    /// </summary>
    public partial class MenuPage : Page
    {

        koneksi k = new koneksi();
        public MenuPage()
        {
            InitializeComponent();

            lb_cpu.Text = HardwareInfo.GetProcessorname();
            lb_ram.Text = HardwareInfo.GetPhysicalMemory();
            lb_bios.Text = HardwareInfo.GetBIOSmaker();
            lb_mobo.Text = HardwareInfo.GetBoardMaker();
            lb_vga.Text = HardwareInfo.Getvganame();

            

            //timer-------------
            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render);

            timer.Interval = TimeSpan.FromMilliseconds(2500);
            timer.Tick += timer_refreshData;
            timer.Start();

            //---------

        }

        void timer_refreshData(object sender, EventArgs e)
        {
            k.sql = "select COUNT(id_produk) from tb_produk";
            k.setdt();
            tile_jmlprod.Count = k.dt.Rows[0][0].ToString();

            k.sql = "select COUNT(id_katalog) from tb_katalog";
            k.setdt();
            tile_jmlktlog.Count = k.dt.Rows[0][0].ToString();

            int l_WindowCount = System.Windows.Forms.Screen.AllScreens.Count();


            tile_jmlmonitor.Count = l_WindowCount.ToString();


        }

    }
}
