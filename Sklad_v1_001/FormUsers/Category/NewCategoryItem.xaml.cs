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

namespace Sklad_v1_001.FormUsers.Category
{
    /// <summary>
    /// Логика взаимодействия для NewCategoryItem.xaml
    /// </summary>
    public partial class NewCategoryItem : Page, INotifyPropertyChanged
    {
        public static readonly DependencyProperty IsClickButtonOKProperty = DependencyProperty.Register(
                    "IsClickButtonOK",
                    typeof(MessageBoxResult),
                   typeof(NewCategoryItem), new PropertyMetadata(MessageBoxResult.Cancel));
        public MessageBoxResult IsClickButtonOK
        {
            get { return (MessageBoxResult)GetValue(IsClickButtonOKProperty); }
            set { SetValue(IsClickButtonOKProperty, value); }
        }

       

        Attributes attributes;

        GlobalList.Category categoryRow;
        CategoryLogic categoryLogic;

        public GlobalList.Category CategoryRow
        {
            get
            {
                return categoryRow;
            }

            set
            {
                categoryRow = value;
                this.DataContext = value;
                OnPropertyChanged("CategoryRow");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public NewCategoryItem(Attributes _attributes)
        {
            InitializeComponent();

            this.attributes = _attributes;

            CategoryRow = new GlobalList.Category();
            categoryLogic = new CategoryLogic(attributes);

            this.control.DataContext = CategoryRow;
        }
       


        #region OKCancel       
        private void Cancel_ButtonClick()
        {
            IsClickButtonOK = MessageBoxResult.Cancel;
            Window win = Parent as Window;
            win.Close();
        }

        private void OK_ButtonClick()
        {
            if (FieldVerify())
            {
                CategoryRow.ID = categoryLogic.SaveRow(CategoryRow);
                if (CategoryRow.ID > 0)
                {
                    attributes.FillCategory();//обновить категории
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

            if (String.IsNullOrEmpty(CategoryRow.Name))
            {
                mb = new FlexMessageBox();
                mb.Show(Properties.Resources.ErrorEmptyField, GenerateTitle(TitleType.Error, Properties.Resources.EmptyField, CategoryName.LabelText), MessageBoxButton.OK, MessageBoxImage.Error);
                CategoryName.DescriptionInfo.Focus();
                return false;
            }

            if (String.IsNullOrEmpty(CategoryRow.Description))
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
