using Sklad_v1_001.FormUsers.Users;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.HelperGlobal.StoreAPI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Sklad_v1_001.FormUsers.Company
{
    /// <summary>
    /// Логика взаимодействия для NewCompanyGrid.xaml
    /// </summary>
    public partial class NewCompanyGrid : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        // свойство зависимостей
        public static readonly DependencyProperty VisibilityShopItemProperty = DependencyProperty.Register(
                        "VisibilityShopItem",
                        typeof(Visibility),
                        typeof(NewCompanyGrid), new UIPropertyMetadata(Visibility.Collapsed));

        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Visibility VisibilityShopItem
        {
            get { return (Visibility)GetValue(VisibilityShopItemProperty); }
            set { SetValue(VisibilityShopItemProperty, value); }
        }

        Attributes attributes;
        LocaleRow localeRowCompany;

        CompanyLogic companyLogic;
        //работа с users
        UserLogic userLogic;

        Request request;
        public LocaleRow LocaleRowCompany
        {
            get
            {
                return localeRowCompany;
            }

            set
            {
                localeRowCompany = value;
                OnPropertyChanged("LocaleRowCompany");
            }
        }
        public NewCompanyGrid(Attributes _attributes)
        {
            InitializeComponent();
            this.attributes = _attributes;

            LocaleRowCompany = new LocaleRow();

            companyLogic = new CompanyLogic(attributes);
            userLogic = new UserLogic(attributes);

            this.attributes = _attributes;
            this.DataContext = LocaleRowCompany;
        }
        #region ToolBar
        private void toolbarCompany_ButtonSave()
        {
            Save();
        }

        private void toolbarCompany_ButtonSaveclose()
        {
            if (Save() > 0)
            {

            }
        }

        private void toolbarCompany_ButtonListCancel()
        {

        }

        private void toolbarCompany_ButtonApply()
        {
            Int32 tempID = Save();
            if (tempID > 0)
            {
                request = new Request(attributes);
                companyLogic.Convert(LocaleRowCompany, request.companyRequest);

                Response response = request.GetCommand(4);
                if (response != null && response.ErrorCode == 0)
                {
                    LocaleRowCompany = companyLogic.Convert(response.companyRequest, LocaleRowCompany);
                    LocaleRowCompany.ReffID = response.companyRequest.company.iD;
                    LocaleRowCompany.SyncDate = DateTime.Now;
                    LocaleRowCompany.SyncStatus = 3;
                    LocaleRowCompany.ID = tempID;
                    if (Save() > 0)
                    {
                        LocaleRowCompany.GeneralDirectory.SyncDate = DateTime.Now;
                        LocaleRowCompany.GeneralDirectory.SyncStatus = 3;
                        if (userLogic.SaveRow(LocaleRowCompany.GeneralDirectory) > 0)
                        {
                            LocaleRowCompany.SeniorAccount.SyncDate = DateTime.Now;
                            LocaleRowCompany.SeniorAccount.SyncStatus = 3;
                            userLogic.SaveRow(LocaleRowCompany.SeniorAccount);
                            MainWindow.AppWindow.ButtonListCompanyOpen();
                        }
                    }

                    //MainWindow.AppWindow.ButtonListCompanyOpen();
                }
                //Document.Status = response.SupplyDocumentOutput.Document.Status;
                //Document.ReffID = 0;
                //Document.ReffDate = response.SupplyDocumentOutput.Document.SyncDate;
                //if (SaveRequest() == 0)
                //{
                //}
            }
        }
        #endregion

        #region Save
        private Int32 Save()
        {
            return companyLogic.SaveRow(LocaleRowCompany);
        }
        #endregion

        private void checkBoxShop_ButtonCheckedClick(object sender, RoutedEventArgs e)
        {
            VisibilityShopItem = Visibility.Visible;
            localeRowCompany.Shop.Address = localeRowCompany.Adress;
            localeRowCompany.Shop.Active = true;
            localeRowCompany.Active = true;
        }

        private void checkBoxShop_ButtonUnCheckedClick(object sender, RoutedEventArgs e)
        {
            VisibilityShopItem = Visibility.Collapsed;
            localeRowCompany.Shop.Address = string.Empty;
            localeRowCompany.Shop.Active = false;
        }
    }
}
