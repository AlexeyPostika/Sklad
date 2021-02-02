using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace Sklad_v1_001.GlobalVariable
{
    public static class ScreenType
    {
        public static String ScreenTypeGrid = "Grid";
        public static String ScreenTypeItem = "Item";
        public static String ScreenTypeInGrid = "InGrid";
        public static String Child = "Child";
        public static String ScreenTypeName = "Name";
        public static String Filter = "Filter";
        public static String ItemByStatus = "ItemByStatus";
    }

    public static class ImageHelper
    {
        public static List<String> imagesNameList = new List<String>();
        static String ResourceIconPath = "Icone";
        public static BitmapImage bitmapImage;

        public static BitmapImage GenerateImage(String iconName)
        {
            String imagePath = String.Concat("/Sklad_v1_001;component/", ResourceIconPath, "/", iconName);
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
                            uriImage = new Uri(String.Concat("/POS;component/", resourceIconPath, "/", imageName), UriKind.Relative);
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

    public static class ColorHelper
    {
        static SolidColorBrush ActiveButtonColor;
        static SolidColorBrush NonActiveButtonColor;

        public static SolidColorBrush GenerateActiveButtonColor()
        {
            //ActiveButtonColor = (SolidColorBrush)MainWindow.AppWindow.TryFindResource("ColorButtonPressedBackground");
            return ActiveButtonColor;
        }

        public static SolidColorBrush GenerateNonActiveButtonColor()
        {
            //NonActiveButtonColor = (SolidColorBrush)MainWindow.AppWindow.TryFindResource("ColorButtonStaticBackground");
            return NonActiveButtonColor;
        }
    }

    public static class SoundHelper
    {
        static String ResourceSoundPath = "Resources";

        public static Uri GenerateSoundPath(String soundName)
        {
            string helpFileName = System.IO.Path.Combine(GetExeDirectory(), ResourceSoundPath, soundName);
            return new Uri(helpFileName, UriKind.Absolute);
        }

        private static string GetExeDirectory()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            path = System.IO.Path.GetDirectoryName(path);
            return path;
        }

    }
}
