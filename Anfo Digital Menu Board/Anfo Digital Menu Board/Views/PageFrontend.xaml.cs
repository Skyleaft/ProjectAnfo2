using Anfo_Digital_Menu_Board.Dialog;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
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
using Microsoft.WindowsAPICodePack.Dialogs;
using Anfo_Digital_Menu_Board.FrontEnd;
using System.Management;
using WpfScreenHelper;
using MahApps.Metro.Controls;

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageFrontend.xaml
    /// </summary>
    public partial class PageFrontend : Page
    {

        public PageFrontend()
        {
            InitializeComponent();

            //testlv.ItemsSource = System.Windows.Forms.Screen.AllScreens.ToList();
            int jmlmonitor = System.Windows.Forms.Screen.AllScreens.Count();
            List<String> monitor = new List<string>();
            for(int i = 1; i <= jmlmonitor; i++)
            {
                monitor.Add("Display\\\\" + i);
            }

            cmb_monitor.ItemsSource = monitor;


        }

        private void ShowOnMonitor(int monitor, MetroWindow window)
        {
            Screen screens = Screen.AllScreens.ToList<Screen>()[monitor];

            window.WindowStartupLocation = WindowStartupLocation.Manual;

            window.Left = screens.Bounds.Left;
            window.Top = screens.Bounds.Top;

            window.SourceInitialized += (snd, arg) =>
                window.WindowState = WindowState.Maximized;


            window.Show();
        }



        koneksi k = new koneksi();
        string folder;



        private string _idktlog;

        private void btn_cari_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new DialogPilihKatalog();
            showdialog.Check += value => _idktlog = value;
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            k.sql = "select *from tb_katalog where id_katalog = '" + _idktlog + "'";
            k.setdt();

            txt_idktlog.Text = k.dt.Rows[0][0].ToString();
            txt_deskripsi.Text = k.dt.Rows[0][1].ToString();
        }

        private void btn_tampilkan_Click(object sender, RoutedEventArgs e)
        {
            int monitor = cmb_monitor.SelectedIndex;
            if (rb_md1.IsChecked == true)
            {
                if (cmb_data.SelectedIndex == 0)
                {
                    Tampilan1 tp1 = new Tampilan1();
                    tp1.tampilsemua();
                    ShowOnMonitor(monitor, tp1);
                }
                else
                {
                    Tampilan1 tp1 = new Tampilan1();
                    tp1.showdata(txt_idktlog.Text);
                    ShowOnMonitor(monitor, tp1);
                }
                

            }
            else if (rb_md2.IsChecked == true)
            {
                if (cmb_data.SelectedIndex == 0)
                {
                    Tampilan2 tp2 = new Tampilan2();
                    tp2.tampilsemua();
                    tp2.LoadImageFolder(folder);
                    tp2.valhw(Convert.ToInt32(sld_lebar.Value), Convert.ToInt32(sld_tinggi.Value));

                    ShowOnMonitor(monitor, tp2);
                }
                else
                {
                    Tampilan2 tp2 = new Tampilan2();
                    tp2.showdata(txt_idktlog.Text);
                    tp2.LoadImageFolder(folder);
                    

                    ShowOnMonitor(monitor, tp2);
                }
            }
            //FrontEndWindow fw = new FrontEndWindow();
            //fw.LoadImageFolder(folder);
            //fw.Show();
        }

        private void btn_ambillokasi_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            //CommonFileDialogResult result = dialog.ShowDialog();

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                folder = dialog.FileName;
                txt_lokasifolder.Text = folder;
            }

        }

        private void cmb_data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_data.SelectedIndex == 1)
            {
                pn_ktlog.Visibility = Visibility.Visible;
            }
            else
            {
                pn_ktlog.Visibility = Visibility.Hidden;
            }
        }

        private void rb_md2_Checked(object sender, RoutedEventArgs e)
        {
            pn_slideshow.Visibility = Visibility.Visible;
        }

        private void rb_md1_Checked(object sender, RoutedEventArgs e)
        {
            pn_slideshow.Visibility = Visibility.Hidden;
        }

        private void txt_lebar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                int lebar = int.Parse(txt_lebar.Text);
                if (lebar <= 400 || lebar >= 100)
                {
                    sld_lebar.Value = lebar;
                }
            }
        }

        private void txt_tinggi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                int tinggi = int.Parse(txt_tinggi.Text);
                if (tinggi <= 400 || tinggi >= 100)
                {
                    sld_tinggi.Value = tinggi;
                }
            }
        }
    }
}
