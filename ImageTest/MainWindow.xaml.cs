using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

namespace ImageTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<BitmapImage> listImage;
        public MainWindow()
        {
            InitializeComponent();
            listImage = new List<BitmapImage>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            image.Source = listImage.First();
            image1.Source = listImage[1];
            image2.Source = listImage[2];
            image3.Source = listImage[3];
            image4.Source = listImage[4];
        }
        public void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            // Открываем окно диалога с пользователем.
            if (openFileDialog.ShowDialog() == true)
            {
               
                // Получаем расширение файла, выбранного пользователем.
                var extension = openFileDialog.FileName;

                foreach (String file in openFileDialog.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        BitmapImage loadedImage = new BitmapImage(new Uri(file));
                        listImage.Add(loadedImage);                      
                    }
                    catch (Exception ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }                 
                }
                // Проверяем есть ли в ОС программа, которая может открыть
                // файл с указанным расширением.
                //if (HasRegisteredFileExstension(extension))
                //{
                //    // Открываем файл. 
                //    Process.Start(openFileDialog.FileName);
                //}
            }
        }

        // Проверяем есть ли в ОС программа, которая может открыть
        // файл с указанным расширением.
        private bool HasRegisteredFileExstension(string fileExstension)
        {
            RegistryKey rkRoot = Registry.ClassesRoot;
            RegistryKey rkFileType = rkRoot.OpenSubKey(fileExstension);

            return rkFileType != null;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            listImage.Clear();
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".jpg";
            saveFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                Save(saveFileDialog.FileName, listImage);
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void Save(string filePuth, List<BitmapImage> _ist)
        {
            string filename = "";
            Int32 temp = 0;
            String[] str = filePuth.Split('\\');
            filePuth = "";
            for (int i = 0; i < str.Length - 1; i++)
            {
                filePuth += str[i] + "\\";
            }
            string format = str.Last().Split('.')[1];

            foreach(BitmapImage image in _ist)
            {
                temp++;
                filename = temp + "." + format;
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)image));
                using (FileStream fs = new FileStream(filePuth + filename, FileMode.Create))
                {
                    encoder.Save(fs);
                }
            }          
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog();
        }
    }
}
