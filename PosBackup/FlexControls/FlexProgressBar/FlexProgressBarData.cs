using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace PosBackup.FlexControls.FlexProgressBar
{
    public enum ProgressBarType
    {
        Universal,
        Print,
        Payment,
        TaxFree
    }

    public class FlexProgressBarData
    {
        Boolean isIndeterminate;
        Boolean pbStatusStart;
        Int32 pbStatusValue;
        Boolean pbStatusBreak;
        Boolean pbResult;
        Boolean doNotClose;
        ProgressBarType pbType;
        String pbStatusMessage;

        String cardNumberCRM;
        Delegate @delegate;
        Boolean pbStatusAbort;

        public Boolean PbStatusAbort
        {
            get
            {
                return pbStatusAbort;
            }

            set
            {
                pbStatusAbort = value;
                OnPropertyChanged("PbStatusAbort");
            }
        }

        public Delegate @Delegate
        {
            get
            {
                return @delegate;
            }

            set
            {
                @delegate = value;
                OnPropertyChanged("@Delegate");
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

        public Int32 PbStatusValue
        {
            get
            {
                return pbStatusValue;
            }

            set
            {
                if (pbStatusValue != value)
                {
                    pbStatusValue = value;
                    OnPropertyChanged("PbStatusValue");
                }
            }
        }

        public Boolean PbStatusBreak
        {
            get
            {
                return pbStatusBreak;
            }

            set
            {
                pbStatusBreak = value;
                OnPropertyChanged("PbStatusBreak");
            }
        }

        public Boolean PbStatusStart
        {
            get
            {
                return pbStatusStart;
            }

            set
            {
                pbStatusStart = value;
                OnPropertyChanged("PbStatusStart");
            }
        }

        public Boolean PbResult
        {
            get
            {
                return pbResult;
            }

            set
            {
                pbResult = value;
                OnPropertyChanged("PbResult");
            }
        }

        public Boolean DoNotClose
        {
            get
            {
                return doNotClose;
            }

            set
            {
                doNotClose = value;
                OnPropertyChanged("DoNotClose");
            }
        }

        public ProgressBarType PbType
        {
            get
            {
                return pbType;
            }

            set
            {
                pbType = value;
                OnPropertyChanged("PbType");
            }
        }

        public string PbStatusMessage
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

        public String CardNumberCRM
        {
            get
            {
                return cardNumberCRM;
            }

            set
            {
                cardNumberCRM = value;
                OnPropertyChanged("CardNumberCRM");
            }
        }

        public delegate void PropertyChangedHandler(string propertyName);
        public event PropertyChangedHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(propertyName);
        }
    }
}
