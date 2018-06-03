using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Anfo_Digital_Menu_Board
{
    [ValueConversion(typeof(byte[]), typeof(ImageSource))]
    public class ByteToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var byteArrayImage = value as byte[];

            if (byteArrayImage != null && byteArrayImage.Length > 0)
            {
                var ms = new MemoryStream(byteArrayImage);

                var bitmapImg = new BitmapImage();

                bitmapImg.BeginInit();
                bitmapImg.DecodePixelWidth = 500;
                bitmapImg.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImg.StreamSource = ms;
                bitmapImg.EndInit();
                ms.SetLength(0);

                return bitmapImg;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
