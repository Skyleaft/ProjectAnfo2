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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageSetting.xaml
    /// </summary>
    public partial class PageSetting : Page
    {
        public PageSetting()
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
            new PaletteHelper().ReplacePrimaryColor("blue");
        }

        private void warna_merah_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("red");
        }

        private void warna_ijo_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("green");
        }

        private void warna_purple_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("deeppurple");
        }

        private void warna_kuning_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("yellow");
        }

        private void warna_oren_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("deeporange");
        }

        private void warna_pink_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("pink");
        }

        private void warna_teal_Click(object sender, RoutedEventArgs e)
        {
            new PaletteHelper().ReplacePrimaryColor("teal");
        } 
}
}
