using Microsoft.Win32;
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
       
        public static readonly DependencyProperty ImageNextProperty = DependencyProperty.Register(
         "ImageNext",
         typeof(BitmapImage),
         typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("chevron_right_30px.png")));
       
        public static readonly DependencyProperty ImageBrakeProperty = DependencyProperty.Register(
        "ImageBrake",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("chevron_left_30px.png")));
       
        public static readonly DependencyProperty ImageDowloadProperty = DependencyProperty.Register(
        "ImageDowload",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("IconDownload.png")));      

        public static readonly DependencyProperty ImageClearProperty = DependencyProperty.Register(
        "ImageClear",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("IconErase.png")));
        
        public static readonly DependencyProperty ImageSaveProperty = DependencyProperty.Register(
        "ImageSave",
        typeof(BitmapImage),
        typeof(ImageList), new UIPropertyMetadata(ImageHelper.GenerateImage("IconSaveAs.png")));
       
        public static readonly DependencyProperty BareCodeProperty = DependencyProperty.Register(
        "BareCode",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));       

        public static readonly DependencyProperty NameStringProperty = DependencyProperty.Register(
        "NameString",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));
       
        public static readonly DependencyProperty CategoryProperty = DependencyProperty.Register(
        "Category",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty CategoryDetailsProperty = DependencyProperty.Register(
        "CategoryDetails",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
        "Status",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));
        
        public static readonly DependencyProperty TagPriceProperty = DependencyProperty.Register(
        "TagPrice",
        typeof(Decimal),
        typeof(ImageList));

        public static readonly DependencyProperty ShowcaseProperty = DependencyProperty.Register(
        "Showcase",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
        "Description",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty SizeStringProperty = DependencyProperty.Register(
        "SizeString",
        typeof(String),
        typeof(ImageList), new UIPropertyMetadata(String.Empty));

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public BitmapImage ImageNext
        {
            get { return (BitmapImage)GetValue(ImageNextProperty); }
            set { SetValue(ImageNextProperty, value); }
        }

        public BitmapImage ImageBrake
        {
            get { return (BitmapImage)GetValue(ImageBrakeProperty); }
            set { SetValue(ImageBrakeProperty, value); }
        }

        public BitmapImage ImageDowload
        {
            get { return (BitmapImage)GetValue(ImageDowloadProperty); }
            set { SetValue(ImageDowloadProperty, value); }
        }

        public BitmapImage ImageClear
        {
            get { return (BitmapImage)GetValue(ImageClearProperty); }
            set { SetValue(ImageClearProperty, value); }
        }

        public BitmapImage ImageSave
        {
            get { return (BitmapImage)GetValue(ImageSaveProperty); }
            set { SetValue(ImageSaveProperty, value); }
        }

        public String BareCode
        {
            get { return (String)GetValue(BareCodeProperty); }
            set { SetValue(BareCodeProperty, value); }
        }

        public String NameString
        {
            get { return (String)GetValue(NameStringProperty); }
            set { SetValue(NameStringProperty, value); }
        }

        public String Category
        {
            get { return (String)GetValue(CategoryProperty); }
            set { SetValue(CategoryProperty, value); }
        }

        public String CategoryDetails
        {
            get { return (String)GetValue(CategoryDetailsProperty); }
            set { SetValue(CategoryDetailsProperty, value); }
        }

        public String Status
        {
            get { return (String)GetValue(StatusProperty); }
            set { SetValue(StatusProperty, value); }
        }

        public Decimal TagPrice
        {
            get { return (Decimal)GetValue(TagPriceProperty); }
            set { SetValue(TagPriceProperty, value); }
        }

        public String Showcase
        {
            get { return (String)GetValue(ShowcaseProperty); }
            set { SetValue(ShowcaseProperty, value); }
        }

        public String Description
        {
            get { return (String)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public String SizeString
        {
            get { return (String)GetValue(SizeStringProperty); }
            set { SetValue(SizeStringProperty, value); }
        }
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

        public ImageList()
        {
            InitializeComponent();
            ListImageControl = new List<BitmapImage>();
           
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
