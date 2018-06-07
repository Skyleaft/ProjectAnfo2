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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Anfo_Digital_Menu_Board.Views
{
    /// <summary>
    /// Interaction logic for PageSetting.xaml
    /// </summary>
    public partial class PageSetting : UserControl
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

            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("blue");

        }

        private void warna_merah_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("red");
        }

        private void warna_ijo_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("green");
        }

        private void warna_purple_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("deeppurple");
        }

        private void warna_kuning_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("yellow");
        }

        private void warna_oren_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("deeporange");
        }

        private void warna_pink_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("pink");
        }

        private void warna_teal_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("teal");
        }

        private void warna_biruindigo_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("indigo");
        }

        private void warna_cyan_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("cyan");
        }

        private void warna_lightgreen_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("lightgreen");
        }

        private void warna_lime_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("lime");
        }

        private void warna_amber_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("amber");
        }

        private void warna_blgrey_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("bluegrey");
        }

        private void warna_brown_Click(object sender, RoutedEventArgs e)
        {
            Storyboard sb = FindResource("colour_change") as Storyboard;
            sb.Begin();
            new PaletteHelper().ReplacePrimaryColor("brown");
        }
    }
}
