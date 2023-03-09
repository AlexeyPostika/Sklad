using System;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;

namespace Sklad_v1_001.Control.FlexProgressBar
{
    public enum ProgressBarType
    {
        Universal,
        Print,
        PrintReport,
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

        //LocaleRow saleDocumentPaymentRow;
        //AuthAnswer13 authAnswer13;
        //AuthAnswer authAnswer;
        //Response responseTinkoff;
        //UCSRequest ucsRequest;

        XpsDocument xpsDoc;
        String xpsDocPath;

       // BalanceResponseBase balanceResponseBaseCRM;

        Delegate @delegate;
        Boolean pbStatusAbort;

        public Int32 OwnerBank { get; set; }

        //public UCSRequest UcsRequest
        //{
        //    get
        //    {
        //        return ucsRequest;
        //    }

        //    set
        //    {
        //        ucsRequest = value;
        //        OnPropertyChanged("UcsRequest");
        //    }
        //}

        public XpsDocument XpsDoc
        {
            get
            {
                return xpsDoc;
            }

            set
            {
                xpsDoc = value;
                OnPropertyChanged("XpsDoc");
            }
        }

        public String XpsDocPath
        {
            get
            {
                return xpsDocPath;
            }

            set
            {
                xpsDocPath = value;
                OnPropertyChanged("XpsDocPath");
            }
        }

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
        //public LocaleRow SaleDocumentPaymentRow
        //{
        //    get
        //    {
        //        return saleDocumentPaymentRow;
        //    }

        //    set
        //    {
        //        saleDocumentPaymentRow = value;
        //        OnPropertyChanged("SaleDocumentPaymentRow");
        //    }
        //}

        //public AuthAnswer13 AuthAnswer13
        //{
        //    get
        //    {
        //        return authAnswer13;
        //    }

        //    set
        //    {
        //        authAnswer13 = value;
        //        OnPropertyChanged("AuthAnswer13");
        //    }
        //}

        //public AuthAnswer AuthAnswer
        //{
        //    get
        //    {
        //        return authAnswer;
        //    }

        //    set
        //    {
        //        authAnswer = value;
        //        OnPropertyChanged("AuthAnswer");
        //    }
        //}

        //public Response ResponseTinkoff
        //{
        //    get
        //    {
        //        return responseTinkoff;
        //    }

        //    set
        //    {
        //        responseTinkoff = value;
        //        OnPropertyChanged("ResponseTinkoff");
        //    }
        //}

        //public BalanceResponseBase BalanceResponseBaseCRM
        //{
        //    get
        //    {
        //        return balanceResponseBaseCRM;
        //    }

        //    set
        //    {
        //        balanceResponseBaseCRM = value;
        //        OnPropertyChanged("BalanceResponseBaseCRM");
        //    }
        //}

        public delegate void PropertyChangedHandler(string propertyName);
        public event PropertyChangedHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(propertyName);
        }
    }
}
