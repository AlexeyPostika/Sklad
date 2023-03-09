using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace POS.FlexControls.FlexProgressBar
{
    /// <summary>
    /// Логика взаимодействия для ProgressBar.xaml
    /// </summary>
    ///
    public partial class FlexProgressBarUi : Window, INotifyPropertyChanged
    {
        FlexProgressBarData progressBarData;
        DispatcherTimer timer;

        private string titleValue;
        private Int32 pbStatusValue;
        private DateTime pbTimePrevious;
        private DateTime pbTimeCurrent;
        private Boolean isIndeterminate;
        double workTime; // сколько миллисекунд работали
        double estimateTime; // сколько миллисекунд осталось
        double realestimateTime; // текущее сколько миллисекунд осталось

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

        public DateTime PbTimePrevious
        {
            get
            {
                return pbTimePrevious;
            }

            set
            {
                pbTimePrevious = value;
                OnPropertyChanged("PbTimePrevious");
            }
        }

        public DateTime PbTimeCurrent
        {
            get
            {
                return pbTimeCurrent;
            }

            set
            {
                pbTimeCurrent = value;
                OnPropertyChanged("PbTimeCurrent");
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

        public DispatcherTimer Timer
        {
            get
            {
                return timer;
            }

            set
            {
                timer = value;
                OnPropertyChanged("Timer");
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

        public FlexProgressBarUi(FlexProgressBarData _progressBarData)
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
            PbTimeCurrent = DateTime.Now;
            PbTimePrevious = DateTime.Now;
            Timer = new DispatcherTimer();

            if (!_progressBarData.IsIndeterminate)
            {
                TitleValue = Properties.Resources.Waiting + " 0%";
                Timer.Tick += new EventHandler(timerTick);
                Timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
                Timer.Start();
            }

            else
            {
                TitleValue = Properties.Resources.Waiting;
            }

            workTime = 0;
            estimateTime = 0;
            realestimateTime = 0;

            progressBarData.PropertyChanged += (propertyName) =>
            {
                if (propertyName == "PbStatusValue")
                {
                    PbTimeCurrent = DateTime.Now;
                    TimeSpan diffDateTime = PbTimeCurrent.Subtract(PbTimePrevious);
                    PbTimePrevious = PbTimeCurrent;
                    workTime = workTime + diffDateTime.TotalMilliseconds;
                    if (progressBarData.PbStatusValue != PbStatusValue)
                    {
                        PbStatusValue = progressBarData.PbStatusValue;
                        estimateTime = workTime * 100 / PbStatusValue;
                        realestimateTime = estimateTime - workTime;
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
                        Timer.Stop();
                        Dispatcher.BeginInvoke(DispatcherPriority.Send, new Action(() =>
                        {
                            this.Close();
                        }));
                    }
                }
            };
        }

        private void ProgressBarWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Activate();
            if (progressBarData.PbStatusBreak == true)
            {
                this.Close();
            }
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (estimateTime != 0 && realestimateTime > 0)
            {
                realestimateTime = realestimateTime - Timer.Interval.TotalMilliseconds;
                var ts = TimeSpan.FromMilliseconds(realestimateTime);
                String outstring = String.Format(" {0}м. {1}с. {2}мс.", ts.Minutes, ts.Seconds, ts.Milliseconds.ToString().Length > 2 ? ts.Milliseconds.ToString().Remove(2) : ts.Milliseconds.ToString());
                TitleValue = Properties.Resources.Waiting + " " + progressBarData.PbStatusValue.ToString() + "% " + Properties.Resources.ProgressBarMessage1 + outstring;
            }
        }

        private void ProgressBarWindow_Closing(object sender, CancelEventArgs e)
        {
            if (!progressBarData.DoNotClose && progressBarData.PbStatusBreak == false)
            {
                progressBarData.PbStatusAbort = true;
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