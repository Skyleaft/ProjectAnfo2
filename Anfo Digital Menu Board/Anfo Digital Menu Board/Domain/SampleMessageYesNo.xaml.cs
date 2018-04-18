using MaterialDesignThemes.Wpf;
using ProjectPengolahanNilaiWPF;
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

namespace MaterialDesignColors.WpfExample.Domain
{
    /// <summary>
    /// Interaction logic for SampleMessageDialog.xaml
    /// </summary>
    public partial class SampleMessageYesNo : UserControl
    {
        public SampleMessageYesNo()
        {
            InitializeComponent();
        }
        
        private void btn_yes_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
            Environment.Exit(1);
        }
    }
}
