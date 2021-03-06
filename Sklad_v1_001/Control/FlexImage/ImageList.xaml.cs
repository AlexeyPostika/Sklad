﻿using Microsoft.Win32;
using Sklad_v1_001.FormUsers.Tovar;
using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Sklad_v1_001.Control.FlexImage
{
    /// <summary>
    /// Логика взаимодействия для ImageList.xaml
    /// </summary>
    public partial class ImageList : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static readonly DependencyProperty ImageNextProperty = DependencyProperty.Register(
         "ImageNext",
         typeof(BitmapImage),
         typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("chevron_right_30px.png")));
        public BitmapImage ImageNext
        {
            get { return (BitmapImage)GetValue(ImageNextProperty); }
            set { SetValue(ImageNextProperty, value); }
        }

        public static readonly DependencyProperty ImageBrakeProperty = DependencyProperty.Register(
        "ImageBrake",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("chevron_left_30px.png")));
        public BitmapImage ImageBrake
        {
            get { return (BitmapImage)GetValue(ImageBrakeProperty); }
            set { SetValue(ImageBrakeProperty, value); }
        }

        public static readonly DependencyProperty ImageDowloadProperty = DependencyProperty.Register(
        "ImageDowload",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("IconDownload.png")));
        public BitmapImage ImageDowload
        {
            get { return (BitmapImage)GetValue(ImageDowloadProperty); }
            set { SetValue(ImageDowloadProperty, value); }
        }


        public static readonly DependencyProperty ImageClearProperty = DependencyProperty.Register(
        "ImageClear",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("IconErase.png")));
        public BitmapImage ImageClear
        {
            get { return (BitmapImage)GetValue(ImageClearProperty); }
            set { SetValue(ImageClearProperty, value); }
        }

        public static readonly DependencyProperty ImageSaveProperty = DependencyProperty.Register(
        "ImageSave",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("IconSaveAs.png")));
        public BitmapImage ImageSave
        {
            get { return (BitmapImage)GetValue(ImageSaveProperty); }
            set { SetValue(ImageSaveProperty, value); }
        }

        public static readonly DependencyProperty ShtrixCodeTextProperty = DependencyProperty.Register(
        "ShtrixCodeText",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));
        public String ShtrixCodeText
        {
            get { return (String)GetValue(ShtrixCodeTextProperty); }
            set { SetValue(ShtrixCodeTextProperty, value); }
        }

        public static readonly DependencyProperty NameProductTextProperty = DependencyProperty.Register(
        "NameProductText",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));
        public String NameProductText
        {
            get { return (String)GetValue(NameProductTextProperty); }
            set { SetValue(NameProductTextProperty, value); }
        }

        public static readonly DependencyProperty TypeProductTextProperty = DependencyProperty.Register(
        "TypeProductText",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));
        public String TypeProductText
        {
            get { return (String)GetValue(TypeProductTextProperty); }
            set { SetValue(TypeProductTextProperty, value); }
        }

        public static readonly DependencyProperty StatusTextProperty = DependencyProperty.Register(
      "StatusText",
      typeof(String),
      typeof(ImageList), new UIPropertyMetadata(String.Empty));
        public String StatusText
        {
            get { return (String)GetValue(StatusTextProperty); }
            set { SetValue(StatusTextProperty, value); }
        }

        LocalRow localDocument;

        List<BitmapImage> listImageControl;
        Int32 tempClick;

        public int TempClick { get => tempClick; set => tempClick = value; }
        public List<BitmapImage> ListImageControl
        {
            get
            {
                return listImageControl;
            }
            set
            {
                listImageControl = value;
                if (ListImageControl.Count > 0)
                {
                    TempClick++;
                    if (Math.Abs(TempClick) < ListImageControl.Count - 1)
                    {
                        image4.Source = image3.Source;
                        image3.Source = image2.Source;
                        image2.Source = image1.Source;
                        image1.Source = image.Source;
                        image.Source = ListImageControl[Math.Abs(TempClick)];
                        buttonNext.IsEnabled = true;
                    }
                    else
                    {
                        buttonNext.IsEnabled = true;
                        buttonBrak.IsEnabled = false;
                    }
                }
                
            }
        }
        public LocalRow LocalDocument
        {
            get
            {
                return localDocument;
            }

            set
            {
                localDocument = value;
                if (LocalDocument.ListImage.Count > 0)
                {
                    ListImageControl = LocalDocument.ListImage;
                    this.DescriptionInform.DataContext = LocalDocument;
                }
                OnPropertyChanged("LocalDocument");
            }
        }

        public ImageList()
        {
            InitializeComponent();
            ListImageControl = new List<BitmapImage>();
            LocalDocument = new LocalRow();
           

            TempClick = 0;
            //buttonNext.IsEnabled = false;
            //buttonBrak.IsEnabled = false;

            ImageNext = ImageHelper.GenerateImage("chevron_right_30px.png");
            ImageBrake = ImageHelper.GenerateImage("chevron_left_30px.png");
            ImageDowload = ImageHelper.GenerateImage("IconDownload.png");
            ImageClear = ImageHelper.GenerateImage("IconErase.png");
            ImageSave = ImageHelper.GenerateImage("IconSaveAs.png");
          
        }

        private void ButtonBrak_Click(object sender, RoutedEventArgs e)
        {
            if (ListImageControl.Count > 0)
            {
                TempClick++;
                if (Math.Abs(TempClick) < ListImageControl.Count - 1)
                {
                    image4.Source = image3.Source;
                    image3.Source = image2.Source;
                    image2.Source = image1.Source;
                    image1.Source = image.Source;
                    image.Source = ListImageControl[Math.Abs(TempClick)];
                    buttonNext.IsEnabled = true;
                }
                else
                {
                    buttonNext.IsEnabled = true;
                    buttonBrak.IsEnabled = false;
                }
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (ListImageControl.Count > 0)
            {
                TempClick--;
                if (TempClick > 0 && Math.Abs(TempClick) < ListImageControl.Count - 1)
                {
                    image.Source = image1.Source;
                    image1.Source = image2.Source;
                    image2.Source = image3.Source;
                    image3.Source = image4.Source;
                    image4.Source = ListImageControl[Math.Abs(TempClick)];
                    buttonBrak.IsEnabled = true;
                }
                else
                {
                    buttonNext.IsEnabled = false;
                    buttonBrak.IsEnabled = true;
                }
            }
        }

        #region FileDocument
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
                        ListImageControl.Add(loadedImage);
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

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".jpg";
            saveFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                Save(saveFileDialog.FileName, ListImageControl);
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

            foreach (BitmapImage image in _ist)
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
        #endregion

        private void ButtonDowload_Click(object sender, RoutedEventArgs e)
        {
            OpenFile();
            image.Source = ListImageControl.First();
            image1.Source = ListImageControl[1];
            image2.Source = ListImageControl[2];
            image3.Source = ListImageControl[3];
            image4.Source = ListImageControl[4];
            TempClick = ListImageControl.Count - 1;
            buttonNext.IsEnabled = true;
            buttonBrak.IsEnabled = false;
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            ListImageControl.Clear();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog();
        }
    }
}
