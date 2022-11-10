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

        Attributes attributes;
        LocaleRow localeRowCompany;

        CompanyLogic companyLogic;

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
            LocaleRowCompany = new LocaleRow();

            companyLogic = new CompanyLogic(attributes);

            this.attributes = _attributes;
            this.DataContext = LocaleRowCompany;
        }
        #region ToolBar
        private void toolbarCompany_ButtonSave()
        {

        }

        private void toolbarCompany_ButtonSaveclose()
        {

        }

        private void toolbarCompany_ButtonListCancel()
        {

        }

        private void toolbarCompany_ButtonApply()
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

               if (Save() > 0)
                {

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
    }
}
