using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalAttributes;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001.FormUsers.DeliveryDetails
{
    /// <summary>
    /// Логика взаимодействия для NewDeliveryItem.xaml
    /// </summary>
    public partial class NewDeliveryDetailsItem : Page, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewDeliveryDetailsItem), new PropertyMetadata(MessageBoxResult.Cancel));
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }



        Attributes attributes;

        GlobalList.DeliveryCompanyDetails deliveryCompanyDetailsRow;
        DeliveryDetailsLogic deliveryDetailsLogic;

        public GlobalList.DeliveryCompanyDetails DeliveryCompanyDetailsRow
        {
            get
            {
                return deliveryCompanyDetailsRow;
            }

            set
            {
                deliveryCompanyDetailsRow = value;
                this.DataContext = value;
                OnPropertyChanged("DeliveryCompanyDetailsRow");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewDeliveryDetailsItem(Attributes _attributes)
        {
            InitializeComponent();

            DeliveryCompanyDetailsRow = new GlobalList.DeliveryCompanyDetails();
            deliveryDetailsLogic = new DeliveryDetailsLogic();

            this.attributes = _attributes;

            this.control.DataContext = DeliveryCompanyDetailsRow;
        }
        
        #region OKCancel   
        private void OK_ButtonClick()
        {
            if (FieldVerify())
            {
                DeliveryCompanyDetailsRow.ID = deliveryDetailsLogic.SaveRow(DeliveryCompanyDetailsRow);
                if (DeliveryCompanyDetailsRow.ID > 0)
                {
                    attributes.FillDeliveryCompanyDetails();//обновить категории
                    IsClickButtonOK = MessageBoxResult.OK;
                    Window win = Parent as Window;
                    win.Close();
                }
            }
        }
        private void Cancel_ButtonClick()
        {          
            IsClickButtonOK = MessageBoxResult.Cancel;
            Window win = Parent as Window;
            win.Close();
        }
        #endregion

        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            if (String.IsNullOrEmpty(DeliveryCompanyDetailsRow.DeliveryIDString))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, NameCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                NameCompany.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryCompanyDetailsRow.Description))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, ManagerCompany.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                ManagerCompany.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(DeliveryCompanyDetailsRow.Phones))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, PhonesManager.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                PhonesManager.DescriptionInfo.Focus();
                return false;
            }
            
            return true;
        }

        
    }
}
