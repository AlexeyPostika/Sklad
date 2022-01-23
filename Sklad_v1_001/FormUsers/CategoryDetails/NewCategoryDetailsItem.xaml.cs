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

namespace Sklad_v1_001.FormUsers.CategoryDetails
{
    /// <summary>
    /// Логика взаимодействия для NewCategoryItem.xaml
    /// </summary>
    public partial class NewCategoryDetailsItem : Page, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewCategoryDetailsItem), new PropertyMetadata(MessageBoxResult.Cancel));
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }

       

        Attributes attributes;

        GlobalList.CategoryDetails categoryDetailsRow;
        CategoryDetailsLigic categoryDetailsLogic;

        public GlobalList.CategoryDetails CategoryDetailsRow
        {
            get
            {
                return categoryDetailsRow;
            }

            set
            {
                categoryDetailsRow = value;
                this.DataContext = value;
                OnPropertyChanged("CategoryDetailsRow");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewCategoryDetailsItem(Attributes _attributes)
        {
            InitializeComponent();

            CategoryDetailsRow = new GlobalList.CategoryDetails();
            categoryDetailsLogic = new CategoryDetailsLigic();

            this.attributes = _attributes;

            this.control.DataContext = CategoryDetailsRow;
        }
       


        #region OKCancel       
        private void Cancel_ButtonClick()
        {

        }

        private void OK_ButtonClick()
        {
            if (FieldVerify())
            {
                CategoryDetailsRow.ID = categoryDetailsLogic.SaveRow(CategoryDetailsRow);
                if (CategoryDetailsRow.ID > 0)
                {
                    attributes.FillCategory();//обновить категории
                    attributes.FillCategoryDetails();

                    IsClickButtonOK = MessageBoxResult.OK;
                    Window win = Parent as Window;
                    win.Close();
                }            
            }          
        }
        #endregion

        private Boolean FieldVerify()
        {
            FlexMessageBox mb;

            if (String.IsNullOrEmpty(CategoryDetailsRow.Name))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryName.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(CategoryDetailsRow.CategoryIDString))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryCat.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryCat.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(CategoryDetailsRow.Description))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryDescription.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryDescription.DescriptionInfo.Focus();
                return false;
            }
            return true;
        }

    }
}
