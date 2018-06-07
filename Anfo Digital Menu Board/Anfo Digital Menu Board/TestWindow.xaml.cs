using MahApps.Metro.Controls;
using MaterialDesignColors;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Anfo_Digital_Menu_Board
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : MetroWindow
    {
        koneksi k = new koneksi();
        public TestWindow()
        {
            InitializeComponent();

        }

        private void toggleDark_Click(object sender, RoutedEventArgs e)
        {
            if (toggleDark.IsChecked == true)
            {
                new PaletteHelper().SetLightDark(true);
            }
            else
            {
                new PaletteHelper().SetLightDark(false);
            }
        }
        private static void ApplyPrimary(Swatch swatch)
        {
            new PaletteHelper().ReplacePrimaryColor(swatch);
        }

        private void warna_biru_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("blue");

        }

        private void warna_merah_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("red");
        }

        private void warna_ijo_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("green");
        }

        private void warna_purple_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("deeppurple");
        }

        private void warna_kuning_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("yellow");
        }

        private void warna_oren_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("deeporange");
        }

        private void warna_pink_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("pink");
        }

        private void warna_teal_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = this.FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("teal");
        }


    }

}
