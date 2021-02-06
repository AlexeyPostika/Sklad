using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;
 namespace Sklad_v1_001.HelperGlobal
{
    public class ImageSql
    {
        public byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            if (imageSource == null)
                return null;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.QualityLevel = 72;
            byte[] bytes = null;
            var bitmapSourceIn = imageSource as BitmapSource;
            double MaxSize = 800;
            double AspectRatio = 1.0;
            double SourceInWidth = bitmapSourceIn.PixelWidth;
            double SourceInHeight = bitmapSourceIn.PixelHeight;
            if (SourceInWidth > MaxSize || SourceInHeight > MaxSize)
            {
                if (SourceInWidth > MaxSize)
                    AspectRatio = MaxSize / SourceInWidth;
                if (SourceInHeight > MaxSize)
                    AspectRatio = MaxSize / SourceInHeight;
            }
            TransformedBitmap bitmapSourceOut = new TransformedBitmap
            (
                bitmapSourceIn,
                new ScaleTransform(AspectRatio, AspectRatio)
            );
            if (bitmapSourceOut != null)
            {
                encoder.Frames.Add(BitmapFrame.Create(bitmapSourceOut));
                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }
            }
            return bytes;
        }
        public BitmapImage BytesToImageSource(byte[] imageData)
        {
            try
            {
                if (imageData == null || imageData.Length == 0) return null;
                var image = new BitmapImage();
                using (var mem = new MemoryStream(imageData))
                {
                    mem.Position = 0;
                    image.BeginInit();
                    image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.UriSource = null;
                    image.StreamSource = mem;
                    image.EndInit();
                }
                image.Freeze();
                return image;
            }
            catch { return null; }
        }
        public static List<String> imagesNameList = new List<String>();
        static String ResourceIconPath = "Resources";
        public static BitmapImage bitmapImage;
        public static BitmapImage GenerateImage(String iconName)
        {
            String imagePath = String.Concat("/Sklad_v1_001;component/", ResourceIconPath, "/", iconName);
            bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            if (imagesNameList.FirstOrDefault(x => x == iconName) == null)
                imagesNameList.Add(iconName);
            return bitmapImage;

        }

        public static byte[] ConvertToBytes(BitmapImage _bitmapImage)
        {           
            //Stream stream = _bitmapImage.StreamSource;
            Byte[] buffer = null;
          
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(_bitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                buffer = ms.ToArray();
            }
            //if (stream != null && stream.Length > 0)
            //{
            //    using (BinaryReader br = new BinaryReader(stream))
            //    {
            //        buffer = br.ReadBytes((Int32)stream.Length);
            //    }
            //}
            return buffer;
        }
    }
}
