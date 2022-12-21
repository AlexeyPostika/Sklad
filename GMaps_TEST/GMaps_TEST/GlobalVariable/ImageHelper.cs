using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace GMaps_Test.GlobalVariable
{
    public static class ImageHelper
    {
        public static List<String> imagesNameList = new List<String>();
        static String ResourceIconPath = "Icon";
        public static BitmapImage bitmapImage;

        public static BitmapImage GenerateImage(String iconName)
        {
            String imagePath = String.Concat("/GMaps_Test;component/", ResourceIconPath, "/", iconName);
            bitmapImage = new BitmapImage(new Uri(imagePath, UriKind.Relative));
            if (imagesNameList.FirstOrDefault(x => x == iconName) == null)
                imagesNameList.Add(iconName);
            return bitmapImage;
        }

        public static BitmapImage GeneratePhotoImage(String iconName)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (var stream = new FileStream(iconName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        public static void SetResourceIconPath(String resourceIconPath)
        {
            String imageName;
            Uri uriImage;
            ResourceIconPath = resourceIconPath;
            foreach (Window currentWindow in App.Current.Windows)
            {
                foreach (Image targetImage in RecurseFindChildrens<Image>(currentWindow))
                {
                    if (targetImage.Source != null)
                    {
                        imageName = targetImage.Source.ToString().Split('/').Last();
                        if (imagesNameList.FirstOrDefault(x => x == imageName) != null)
                        {
                            uriImage = new Uri(String.Concat("/GMaps_Test;component/", resourceIconPath, "/", imageName), UriKind.Relative);
                            targetImage.Source = new BitmapImage(uriImage);
                            targetImage.InvalidateVisual();
                        }
                    }
                }
            }
        }

        public static IEnumerable<T> RecurseFindChildrens<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (Int32 i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    DependencyObject childObject = VisualTreeHelper.GetChild(dependencyObject, i);
                    if (childObject != null && childObject is T)
                        yield return (T)childObject;
                    foreach (T childOfChild in RecurseFindChildrens<T>(childObject))
                        yield return childOfChild;
                }
            }
        }

        public static void ClearImagesList()
        {
            imagesNameList.Clear();
        }
    }

}
