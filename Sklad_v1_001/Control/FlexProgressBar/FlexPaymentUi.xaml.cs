using POS.GlobalVariable;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace POS.FlexControls.FlexProgressBar
{
    /// <summary>
    /// Логика взаимодействия для ProgressBar.xaml
    /// </summary>
    ///
    public partial class FlexPaymentUi : Window, INotifyPropertyChanged
    {
        FlexProgressBarData progressBarData;

        String titleValue;
        Int32 pbStatusValue;
        Boolean isIndeterminate;
        String pbStatusMessage;
        //pack://application:,,,/Resources/IconSberbank.png
        // свойство зависимостей
        public static readonly DependencyProperty ImageSourceIconProperty = DependencyProperty.Register(
                        "ImageSourceIcon",
                        typeof(ImageSource),
                        typeof(FlexPaymentUi), new UIPropertyMetadata(ImageHelper.GenerateImage("IconSberbank.png")));
        public ImageSource ImageSourceIcon
        {
            get { return (ImageSource)GetValue(ImageSourceIconProperty); }
            set { SetValue(ImageSourceIconProperty, value); }
        }

        public string TitleValue
        {
            get
            {
                return titleValue;
            }
            set
            {
                titleValue = value;
                OnPropertyChanged("TitleValue");
            }
        }

        public Int32 PbStatusValue
        {
            get
            {
                return pbStatusValue;
            }
            set
            {
                pbStatusValue = value;
                OnPropertyChanged("PbStatusValue");
            }
        }

        public Boolean IsIndeterminate
        {
            get
            {
                return isIndeterminate;
            }

            set
            {
                isIndeterminate = value;
                OnPropertyChanged("IsIndeterminate");
            }
        }

        public String PbStatusMessage
        {
            get
            {
                return pbStatusMessage;
            }

            set
            {
                pbStatusMessage = value;
                OnPropertyChanged("PbStatusMessage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        private static extern bool EnableMenuItem(IntPtr hMenu, uint uIDEnableItem, uint uEnable);
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            if (progressBarData.DoNotClose)
            {
                var hWnd = new WindowInteropHelper(this);
                var sysMenu = GetSystemMenu(hWnd.Handle, false);
                EnableMenuItem(sysMenu, 0xF060, 0x00000000 | 0x00000001);
            }
        }

        public FlexPaymentUi(FlexProgressBarData _progressBarData)
        {
            InitializeComponent();

            switch (_progressBarData.OwnerBank)
            {
                case 0:
                    ImageSourceIcon = ImageHelper.GenerateImage("IconSberbank.png");
                    break;
                case 1:
                    ImageSourceIcon = ImageHelper.GenerateImage("IconTinkoff.png");
                    break;
            }
            this.DataContext = this;
            try
            {
                this.Owner = MainWindow.AppWindow;
            }
            catch (Exception e)
            {
            }
            this.ShowInTaskbar = false;

            progressBarData = _progressBarData;
            IsIndeterminate = _progressBarData.IsIndeterminate;
            TitleValue = Properties.Resources.Waiting;
            PbStatusMessage = progressBarData.PbStatusMessage;

            DoubleAnimation doubleWidthAnimation = new DoubleAnimation();
            doubleWidthAnimation.From = 0;
            doubleWidthAnimation.To = 60;
            doubleWidthAnimation.Duration = new Duration(TimeSpan.FromSeconds(1));
            doubleWidthAnimation.AutoReverse = true;
            doubleWidthAnimation.RepeatBehavior = RepeatBehavior.Forever;
            SberbankLogo.BeginAnimation(FrameworkElement.WidthProperty, doubleWidthAnimation);

            progressBarData.PropertyChanged += (propertyName) =>
            {
                if (propertyName == "PbStatusValue")
                {
                    if (progressBarData.PbStatusValue != PbStatusValue)
                    {
                        PbStatusValue = progressBarData.PbStatusValue;
                    }

                    if (progressBarData.IsIndeterminate)
                    {
                        TitleValue = Properties.Resources.Waiting + " " + progressBarData.PbStatusValue.ToString();
                    }
                }

                if (propertyName == "PbStatusBreak")
                {
                    if (progressBarData.PbStatusBreak == true)
                    {
                        Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                        {
                            this.Close();
                        }));
                    }
                }
            };
        }

        private void ProgressBarWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!progressBarData.DoNotClose && progressBarData.PbStatusBreak == false)
            {
                progressBarData.PbStatusAbort = true;
            }
        }

        private void ProgressBarWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
            if (progressBarData.PbStatusBreak == true)
            {
                this.Close();
            }
        }

        private void ProgressBarWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.System && e.SystemKey == Key.F4)
            {
                e.Handled = true;
            }
        }
    }
}