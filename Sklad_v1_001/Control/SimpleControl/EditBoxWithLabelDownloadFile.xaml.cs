using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Sklad_v1_001.Report;
using System.Xml.Linq;


namespace Sklad_v1_001.Control.SimpleControl
{
    /// <summary>
    /// Логика взаимодействия для EditBoxWithLabelDownloadFile.xaml
    /// </summary>
    public partial class EditBoxWithLabelDownloadFile : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        // свойство зависимостей
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                        "Text",
                        typeof(string),
                        typeof(EditBoxWithLabelDownloadFile), new UIPropertyMetadata(String.Empty));

        // свойство зависимостей
        public static readonly DependencyProperty IsRequiredProperty = DependencyProperty.Register(
                        "IsRequired",
                        typeof(Visibility),
                        typeof(EditBoxWithLabelDownloadFile), new UIPropertyMetadata(Visibility.Collapsed));
        
        // свойство зависимостей
        public static readonly DependencyProperty MaxLengthProperty = DependencyProperty.Register(
                        "MaxLength",
                        typeof(Int32),
                        typeof(EditBoxWithLabelDownloadFile), new UIPropertyMetadata(50));
        
        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
                        "Source",
                        typeof(ImageSource),
                       typeof(EditBoxWithLabelDownloadFile));
        //SourceLoop
        public static readonly DependencyProperty SourceLoopProperty = DependencyProperty.Register(
                        "SourceLoop",
                        typeof(ImageSource),
                       typeof(EditBoxWithLabelDownloadFile));

        //
        public static readonly DependencyProperty SourceCleareProperty = DependencyProperty.Register(
                       "SourceCleare",
                       typeof(ImageSource),
                      typeof(EditBoxWithLabelDownloadFile));

        public static readonly DependencyProperty VisibilityCleareProperty = DependencyProperty.Register(
                       "VisibilityCleare",
                       typeof(Visibility),
                      typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty VisibilityLoopProperty = DependencyProperty.Register(
                      "VisibilityLoop",
                      typeof(Visibility),
                     typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Visible));
      
        public static readonly DependencyProperty VisibilityAddProperty = DependencyProperty.Register(
                     "VisibilityAdd",
                     typeof(Visibility),
                    typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Visible));

        public static readonly DependencyProperty VisibilityLoadProperty = DependencyProperty.Register(
                   "VisibilityLoad",
                   typeof(Visibility),
                  typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(Visibility.Collapsed));

        public static readonly DependencyProperty IsEnableCleareProperty = DependencyProperty.Register(
                       "IsEnableCleare",
                       typeof(Boolean),
                      typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableLoopProperty = DependencyProperty.Register(
                      "IsEnableLoop",
                      typeof(Boolean),
                     typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(false));

        public static readonly DependencyProperty IsEnableAddProperty = DependencyProperty.Register(
                     "IsEnableAdd",
                     typeof(Boolean),
                    typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(true));

        public static readonly DependencyProperty FilterPuthProperty = DependencyProperty.Register(
                     "FilterPuth",
                     typeof(String),
                    typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(String.Empty));

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(
                     "Mode",
                     typeof(String),
                    typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata("PDF"));

        public static readonly DependencyProperty ByteFaileProperty = DependencyProperty.Register(
                     "ByteFaile",
                     typeof(byte[]),
                    typeof(EditBoxWithLabelDownloadFile));
        // Обычное свойство .NET  - обертка над свойством зависимостей          
        public static readonly DependencyProperty NameFileProperty = DependencyProperty.Register(
                    "NameFile",
                    typeof(String),
                   typeof(EditBoxWithLabelDownloadFile), new PropertyMetadata(""));
        public ImageSource Source
        {
            get
            {
                return (ImageSource)this.button.Image.Source;
            }
            set
            {
                this.button.Image.Source = value as ImageSource;
                OnPropertyChanged("Source");
            }
        }

        public ImageSource SourceLoop
        {
            get
            {
                return (ImageSource)this.buttonLoop.Image.Source;
            }
            set
            {
                this.buttonLoop.Image.Source = value as ImageSource;
                OnPropertyChanged("SourceLoop");
            }
        }
        public ImageSource SourceCleare
        {
            get
            {
                return (ImageSource)this.buttonClear.Image.Source;
            }
            set
            {
                this.buttonClear.Image.Source = value as ImageSource;
                OnPropertyChanged("SourceCleare");
            }
        }

        public Visibility VisibilityCleare
        {
            get { return (Visibility)GetValue(VisibilityCleareProperty); }
            set { SetValue(VisibilityCleareProperty, value); }         
            
        }
        public Visibility VisibilityLoop
        {
            get { return (Visibility)GetValue(VisibilityLoopProperty); }
            set { SetValue(VisibilityLoopProperty, value); }               
        }
        //VisibilityAdd
        public Visibility VisibilityAdd
        {
            get { return (Visibility)GetValue(VisibilityAddProperty); }
            set { SetValue(VisibilityAddProperty, value); }
        }
        //VisibilityLoad
        public Visibility VisibilityLoad
        {
            get { return (Visibility)GetValue(VisibilityLoadProperty); }
            set { SetValue(VisibilityLoadProperty, value); }
        }
        public Boolean IsEnableCleare
        {
            get { return (Boolean)GetValue(IsEnableCleareProperty); }
            set { SetValue(IsEnableCleareProperty, value); }

        }
        public Boolean IsEnableLoop
        {
            get { return (Boolean)GetValue(IsEnableLoopProperty); }
            set { SetValue(IsEnableLoopProperty, value); }
        }
        //VisibilityAdd
        public Boolean IsEnableAdd
        {
            get { return (Boolean)GetValue(IsEnableAddProperty); }
            set { SetValue(IsEnableAddProperty, value); }
        }
        //FilterPuth
        public String FilterPuth
        {
            get { return (String)GetValue(FilterPuthProperty); }
            set { SetValue(FilterPuthProperty, value); }
        }
        public String Mode
        {
            get { return (String)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }
        //ByteFaile
        public Byte[] ByteFaile
        {
            get { return (Byte[])GetValue(ByteFaileProperty); }
            set { SetValue(ByteFaileProperty, value); }
        }

        public String NameFile
        {
            get { return (String)GetValue(NameFileProperty); }
            set { SetValue(NameFileProperty, value); }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public string Text
        {
            get
            {
                return (string)GetValue(TextProperty);
            }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged("Text");
            }
        }

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility IsRequired
        {
            get
            {
                return (Visibility)GetValue(IsRequiredProperty);
            }
            set
            {
                SetValue(IsRequiredProperty, value);
                OnPropertyChanged("IsRequired");
            }
        }
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 MaxLength
        {
            get
            {
                return (Int32)GetValue(MaxLengthProperty);
            }
            set
            {
                SetValue(MaxLengthProperty, value);
                OnPropertyChanged("MaxLength");
            }
        }
        public string LabelText
        {
            get
            {
                return this.label.Content.ToString();
            }

            set
            {
                this.label.Content = value;
            }
        }

        public double LabelWidth
        {
            get
            {
                return this.wrapPanel.Width;
            }

            set
            {
                this.wrapPanel.Width = value;
            }
        }
        FileWork fileWork; 

        public event Action ButtonAddClick;
        public event Action ButtonLoopClick;
        public event Action ButtonClearClick;
        public EditBoxWithLabelDownloadFile()
        {
            InitializeComponent();

            fileWork = new FileWork();

            this.button.Image.Source= ImageHelper.GenerateImage("IconAddProduct.png");
            this.buttonLoop.Image.Source = ImageHelper.GenerateImage("IconSearchPage.png");
            this.buttonClear.Image.Source = ImageHelper.GenerateImage("IconEraseFaile.png");
        }
        private async void button_ButtonClick()
        {
            VisibilityLoad = Visibility.Visible;
            VisibilityAdd = Visibility.Collapsed;
            fileWork = new FileWork();
            switch (Mode)
            {
                case "PDF":
                    fileWork.OpenPDF(FilterPuth);
                    fileWork = await LoadInvoiceAsync(fileWork);
                    if (fileWork.BufferDocument != null)
                    {
                        VisibilityLoad = Visibility.Collapsed;
                        VisibilityAdd = Visibility.Visible;
                        IsEnableAdd = false;
                        IsEnableLoop = true;
                        IsEnableCleare = true;
                        ByteFaile = fileWork.BufferDocument;
                    }
                    else
                    {
                        VisibilityLoad = Visibility.Collapsed;
                        VisibilityAdd = Visibility.Visible;
                        IsEnableAdd = true;
                        IsEnableLoop = false;
                        IsEnableCleare = false;
                        ByteFaile = null;
                    }
                    break;
                default:
                    fileWork.LoadImage(FilterPuth);                 
                    if (fileWork.BufferDocument != null)
                    {
                        VisibilityLoad = Visibility.Collapsed;
                        VisibilityAdd = Visibility.Visible;
                        IsEnableAdd = false;
                        IsEnableLoop = true;
                        IsEnableCleare = true;
                        ByteFaile = fileWork.BufferDocument;
                    }
                    else
                    {
                        VisibilityLoad = Visibility.Collapsed;
                        VisibilityAdd = Visibility.Visible;
                        IsEnableAdd = true;
                        IsEnableLoop = false;
                        IsEnableCleare = false;
                        ByteFaile = null;
                    }
                    break;
            }
            ButtonAddClick?.Invoke();
        }

        #region загрузка данных в PDF
        static async Task<FileWork> LoadInvoiceAsync(FileWork _fileWork)
        {
            return await Task.Run(() => LoadInvoice(_fileWork));
        }

        static FileWork LoadInvoice(FileWork _fileWork)
        {
            _fileWork.PDFToByte();
            return _fileWork;
        }
        #endregion

        private void buttonLoop_ButtonClick()
        {
           
            using (Stream stream = new MemoryStream(ByteFaile))
            {
                FlexDocumentWindows flexDocumentWindows = new FlexDocumentWindows();            
                XpsDocument doc = fileWork.ByteToXPS(ByteFaile, NameFile);
                flexDocumentWindows.DocumentXps = doc.GetFixedDocumentSequence();              
                doc.Close();
                flexDocumentWindows.ShowDialog();
            }
            //XpsDocument xpsDocument = new XpsDocument(package, CompressionOption.SuperFast);              
        }  

        private void buttonClear_ButtonClick()
        {
            ByteFaile = null;
            IsEnableLoop = false;
            IsEnableCleare = false;
            IsEnableAdd = true;
            ButtonClearClick?.Invoke();
        }
    }
}
