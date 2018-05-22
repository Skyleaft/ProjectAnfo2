using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
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
using System.Windows.Threading;

namespace Anfo_Digital_Menu_Board.FrontEnd
{
    /// <summary>
    /// Interaction logic for Tampilan2.xaml
    /// </summary>
    /// 
    public partial class Tampilan2 : MetroWindow
    {
        private DispatcherTimer timerImageChange;
        private Image[] ImageControls;
        private List<ImageSource> Images = new List<ImageSource>();
        private static string[] ValidImageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        private static string[] TransitionEffects = new[] { "Fade" };
        private string TransitionType, strImagePath = "";
        private int CurrentSourceIndex, CurrentCtrlIndex, EffectIndex = 0;

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            PlaySlideShow();
            timerImageChange.IsEnabled = true;
        }

        public int IntervalTimer = 5;
        koneksi k = new koneksi();

        public int lebar { get; set; }
        public int tinggi { get; set; }

        public void valhw(int _lebar, int _tinggi)
        {
            lebar = _lebar;
            tinggi = _tinggi;
        }

        public Tampilan2()
        {
            InitializeComponent();

            //Initialize Image control, Image directory path and Image timer.
            strImagePath = ConfigurationManager.AppSettings["ImagePath"];
            ImageControls = new[] { myImage, myImage2 };

            LoadImageFolder(strImagePath);


            timerImageChange = new DispatcherTimer();
            timerImageChange.Interval = new TimeSpan(0, 0, IntervalTimer);
            timerImageChange.Tick += new EventHandler(timerImageChange_Tick);

        }





            public void showdata(string _idktlog)
        {
            k.sql = "select *from tb_katalog inner join tb_detail_katalog on tb_katalog.id_katalog = tb_detail_katalog.id_katalog inner join tb_produk on tb_detail_katalog.id_produk = tb_produk.id_produk where tb_katalog.id_katalog = '" + _idktlog + "'";
            k.setdt();
            lv_ktlog.ItemsSource = k.dt.DefaultView;

        }

        public void tampilsemua()
        {
            k.sql = "select *from tb_produk";
            k.setdt();
            lv_ktlog.ItemsSource = k.dt.DefaultView;
        }

        

        public void LoadImageFolder(string folder)
        {
            ErrorText.Visibility = Visibility.Collapsed;
            var sw = System.Diagnostics.Stopwatch.StartNew();
            if (!System.IO.Path.IsPathRooted(folder))
                folder = System.IO.Path.Combine(Environment.CurrentDirectory, folder);
            if (!System.IO.Directory.Exists(folder))
            {
                ErrorText.Text = "The specified folder does not exist: " + Environment.NewLine + folder;
                ErrorText.Visibility = Visibility.Visible;
                return;
            }
            Random r = new Random();
            var sources = from file in new System.IO.DirectoryInfo(folder).GetFiles().AsParallel()
                          where ValidImageExtensions.Contains(file.Extension, StringComparer.InvariantCultureIgnoreCase)
                          orderby r.Next()
                          select CreateImageSource(file.FullName, true);
            Images.Clear();
            Images.AddRange(sources);
            sw.Stop();
            Console.WriteLine("Total time to load {0} images: {1}ms", Images.Count, sw.ElapsedMilliseconds);
        }

        private ImageSource CreateImageSource(string file, bool forcePreLoad)
        {
            if (forcePreLoad)
            {
                var src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri(file, UriKind.Absolute);
                src.CacheOption = BitmapCacheOption.OnLoad;
                src.EndInit();
                src.Freeze();
                return src;
            }
            else
            {
                var src = new BitmapImage(new Uri(file, UriKind.Absolute));
                src.Freeze();
                return src;
            }
        }

        private void timerImageChange_Tick(object sender, EventArgs e)
        {
            PlaySlideShow();
        }

        private void PlaySlideShow()
        {
            try
            {
                if (Images.Count == 0)
                    return;
                var oldCtrlIndex = CurrentCtrlIndex;
                CurrentCtrlIndex = (CurrentCtrlIndex + 1) % 2;
                CurrentSourceIndex = (CurrentSourceIndex + 1) % Images.Count;

                Image imgFadeOut = ImageControls[oldCtrlIndex];
                Image imgFadeIn = ImageControls[CurrentCtrlIndex];
                ImageSource newSource = Images[CurrentSourceIndex];
                imgFadeIn.Source = newSource;

                TransitionType = TransitionEffects[EffectIndex].ToString();

                Storyboard StboardFadeOut = (Resources[string.Format("{0}Out", TransitionType.ToString())] as Storyboard).Clone();
                StboardFadeOut.Begin(imgFadeOut);
                Storyboard StboardFadeIn = Resources[string.Format("{0}In", TransitionType.ToString())] as Storyboard;
                StboardFadeIn.Begin(imgFadeIn);
            }
            catch (Exception ex) { }
        }
    }

}
