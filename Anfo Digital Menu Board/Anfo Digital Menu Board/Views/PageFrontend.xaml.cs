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
using MaterialDesignColors.WpfExample.Domain;

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageFrontend.xaml
    /// </summary>
    public partial class PageFrontend : UserControl
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


        private string _idmsg;

        private void btn_carimsg_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new DialogPilihMessage();
            showdialog.Check += value => _idmsg = value;
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler2);
        }

        private void ClosingEventHandler2(object sender, DialogClosingEventArgs eventArgs)
        {
            if (_idmsg != null)
            {
                k.sql = "select *from tb_message where id_message = '" + _idmsg + "'";
                k.setdt();

                txt_idmsg.Text = k.dt.Rows[0][0].ToString();
                txt_pesan.Text = k.dt.Rows[0][1].ToString();
            }
        }


        private string _idktlog;

        private void btn_cari_Click(object sender, RoutedEventArgs e)
        {
            var showdialog = new DialogPilihKatalog();
            showdialog.Check += value => _idktlog = value;
            DialogHost.Show(showdialog, "MainDialog", ClosingEventHandler);
        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (_idktlog != null)
            {
                k.sql = "select *from tb_katalog where id_katalog = '" + _idktlog + "'";
                k.setdt();

                txt_idktlog.Text = k.dt.Rows[0][0].ToString();
                txt_deskripsi.Text = k.dt.Rows[0][1].ToString();
            }
        }

        private void btn_tampilkan_Click(object sender, RoutedEventArgs e)
        {
            int monitor = cmb_monitor.SelectedIndex;


            string pesan = txt_pesan.Text;

            if(pesan == null || pesan == "")
            {
                pesan = "Anfo Dgital Menu Board";
            }


            if (rb_md1.IsChecked == true)
            {
                if (cmb_data.SelectedIndex == 0)
                {
                    Tampilan1 tp1 = new Tampilan1();
                    tp1.tampilsemua();
                    tp1.txt_running.Text = pesan;
                    ShowOnMonitor(monitor, tp1);
                }
                else
                {
                    Tampilan1 tp1 = new Tampilan1();
                    tp1.showdata(txt_idktlog.Text);
                    tp1.txt_running.Text = pesan;
                    ShowOnMonitor(monitor, tp1);
                }
                

            }
            else if (rb_md2.IsChecked == true)
            {
                if (folder == null)
                {
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Harap Masukan Lokasi Folder Slideshow" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                }
                else
                {
                    if (cmb_data.SelectedIndex == 0)
                    {
                        Tampilan2 tp2 = new Tampilan2();
                        tp2.tampilsemua();
                        tp2.LoadImageFolder(folder);
                        tp2.getdrs(Convert.ToInt32(sld_durasi.Value));
                        tp2.loadSlideshow();
                        tp2.txt_running.Text = pesan;
                        ShowOnMonitor(monitor, tp2);
                    }
                    else
                    {
                        Tampilan2 tp2 = new Tampilan2();
                        tp2.showdata(txt_idktlog.Text);
                        tp2.LoadImageFolder(folder);
                        tp2.getdrs(Convert.ToInt32(sld_durasi.Value));
                        tp2.loadSlideshow();
                        tp2.txt_running.Text = pesan;
                        ShowOnMonitor(monitor, tp2);
                    }
                }
                
            }
            else if (rb_md3.IsChecked == true)
            {
                if (cmb_data.SelectedIndex == 0)
                {
                    Tampilan3 tp3 = new Tampilan3();
                    tp3.tampilsemua();
                    tp3.txt_running.Text = pesan;
                    ShowOnMonitor(monitor, tp3);
                }
                else
                {
                    Tampilan3 tp3 = new Tampilan3();
                    tp3.showdata(txt_idktlog.Text);
                    tp3.txt_running.Text = pesan;
                    ShowOnMonitor(monitor, tp3);
                }
            }
            else if (rb_md4.IsChecked == true)
            {
                if (folder == null)
                {
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Harap Masukan Lokasi Folder Slideshow" }
                    };
                    DialogHost.Show(sampleMessageDialog, "MainDialog");
                }
                else
                {
                    if (cmb_data.SelectedIndex == 0)
                    {
                        Tampilan4 tp4 = new Tampilan4();
                        tp4.tampilsemua();
                        tp4.getdrs(Convert.ToInt32(sld_durasi.Value));
                        tp4.loadSlideshow();
                        tp4.txt_running.Text = pesan;
                        tp4.LoadImageFolder(folder);

                        ShowOnMonitor(monitor, tp4);
                    }
                    else
                    {
                        Tampilan4 tp4 = new Tampilan4();
                        tp4.showdata(txt_idktlog.Text);
                        tp4.LoadImageFolder(folder);
                        tp4.getdrs(Convert.ToInt32(sld_durasi.Value));
                        tp4.loadSlideshow();
                        tp4.txt_running.Text = pesan;
                        ShowOnMonitor(monitor, tp4);
                    }
                }
            }

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
                pn_ktlog.Visibility = Visibility.Collapsed;
            }
        }

        private void rb_md2_Checked(object sender, RoutedEventArgs e)
        {
            pn_slideshow.Visibility = Visibility.Visible;
        }

        private void rb_md1_Checked(object sender, RoutedEventArgs e)
        {
            pn_slideshow.Visibility = Visibility.Collapsed;
        }

        private void rb_md3_Checked(object sender, RoutedEventArgs e)
        {
            pn_slideshow.Visibility = Visibility.Collapsed;
        }

        private void rb_md4_Checked(object sender, RoutedEventArgs e)
        {
            pn_slideshow.Visibility = Visibility.Visible;
        }

        private void btn_refresh_Click(object sender, RoutedEventArgs e)
        {
            cmb_monitor.Items.Refresh();
            foreach(RadioButton rb in pn_rb.Children)
            {
                rb.IsChecked = false;
            }
            txt_idktlog.Text = "";
            txt_deskripsi.Text = "";
            txt_idmsg.Text = "";
            txt_pesan.Text = "";
        }
    }
}
