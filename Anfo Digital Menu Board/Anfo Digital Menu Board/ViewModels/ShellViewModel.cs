using System;
using System.Linq;
using MahApps.Metro.IconPacks;

namespace Anfo_Digital_Menu_Board.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            this.Menu.Add(new MenuItem() {Icon = new PackIconEntypo() {Kind = PackIconEntypoKind.Home}, Text = "Menu", NavigationDestination = new Uri("Views/MenuPage.xaml", UriKind.RelativeOrAbsolute)});
            this.Menu.Add(new MenuItem() {Icon = new PackIconMaterial() {Kind = PackIconMaterialKind.Food}, Text = "Data Makanan", NavigationDestination = new Uri("Views/PageMakanan.xaml", UriKind.RelativeOrAbsolute)});

            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.CogsSolid}, Text = "Pengaturan", NavigationDestination = new Uri("Views/SettingsPage.xaml", UriKind.RelativeOrAbsolute)});
            this.OptionsMenu.Add(new MenuItem() {Icon = new PackIconFontAwesome() {Kind = PackIconFontAwesomeKind.InfoCircleSolid}, Text = "Tentang Aplikasi", NavigationDestination = new Uri("Views/AboutPage.xaml", UriKind.RelativeOrAbsolute)});
        }

        public object GetItem(object uri)
        {
            return null == uri ? null : this.Menu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }

        public object GetOptionsItem(object uri)
        {
            return null == uri ? null : this.OptionsMenu.FirstOrDefault(m => m.NavigationDestination.Equals(uri));
        }
    }
}