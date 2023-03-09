using Sklad_v1_001.Control.FlexProgressBar;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Sklad_v1_001.Controls.FlexProgressBar
{
    /// <summary>
    /// Логика взаимодействия для ProgressBar.xaml
    /// </summary>
    ///
    public partial class FlexTaxFreeUi : Window, INotifyPropertyChanged
    {
        FlexProgressBarData progressBarData;

        String titleValue;
        Int32 pbStatusValue;
        Boolean isIndeterminate;
        String pbStatusMessage;

        public Action abortOperation;

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

        public FlexTaxFreeUi(FlexProgressBarData _progressBarData)
        {
            InitializeComponent();
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
            TaxFreeLogo.BeginAnimation(FrameworkElement.WidthProperty, doubleWidthAnimation);

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

        private void ButtonCancelIssue_Click(object sender, RoutedEventArgs e)
        {
            abortOperation?.Invoke();
        }
    }
}