﻿using MahApps.Metro.Controls;
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

namespace Anfo_Digital_Menu_Board
{
    /// <summary>
    /// Interaction logic for FrontEndWindow.xaml
    /// </summary>
    public partial class FrontEndWindow : MetroWindow
    {
        koneksi k = new koneksi();
        public FrontEndWindow()
        {
            InitializeComponent();
            showdata();
            
        }

        public void showdata()
        {
            k.sql = "select * from tb_produk";
            k.setdt();
            dg_ktlog.ItemsSource = k.dt.DefaultView;

        }

    }
}
