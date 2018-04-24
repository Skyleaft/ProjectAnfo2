using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Anfo_Digital_Menu_Board
{
    public class ByteImageConverter
    {
        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            ms.Position = 0;
            biImg.BeginInit();
            biImg.DecodePixelWidth = 300;
            biImg.CacheOption = BitmapCacheOption.OnLoad;
            biImg.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
            biImg.StreamSource = ms;
            biImg.EndInit();
            
            ImageSource imgSrc = biImg as ImageSource;
            biImg.Freeze();
            return imgSrc;
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }

        //public static string ImageToByte(FileStream fs)
        //{

        //    byte[] imgBytes = new byte[fs.Length];
        //    fs.Read(imgBytes, 0, Convert.ToInt32(fs.Length));
        //    string encodeData = Convert.ToBase64String(imgBytes, Base64FormattingOptions.InsertLineBreaks);

        //    return encodeData;
        //}
    }
}
