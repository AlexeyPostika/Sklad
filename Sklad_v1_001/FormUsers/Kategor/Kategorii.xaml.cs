﻿using Sklad_v1_001.GlobalList;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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

namespace Sklad_v1_001.FormUsers.Kategor
{
   
    /// <summary>
    /// Interaction logic for Kategorii.xaml
    /// </summary>
    public partial class Kategorii : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public static readonly DependencyProperty VisibilityGreenProperty = DependencyProperty.Register(
         "VisibilityZonaGreen",
         typeof(Visibility),
         typeof(Kategorii), new UIPropertyMetadata(Visibility.Collapsed));     
        public Visibility VisibilityZonaGreen
        {
            get { return (Visibility)GetValue(VisibilityGreenProperty); }
            set { SetValue(VisibilityGreenProperty, value); }
        }
        public static readonly DependencyProperty VisibilityZonaYellowProperty = DependencyProperty.Register(
        "VisibilityZonaYellow",
        typeof(Visibility),
        typeof(Kategorii), new UIPropertyMetadata(Visibility.Collapsed));
        public Visibility VisibilityZonaYellow
        {
            get { return (Visibility)GetValue(VisibilityZonaYellowProperty); }
            set { SetValue(VisibilityZonaYellowProperty, value); }
        }
        public static readonly DependencyProperty VisibilityRedProperty = DependencyProperty.Register(
        "VisibilityZonaRed",
        typeof(Visibility),
        typeof(Kategorii), new UIPropertyMetadata(Visibility.Collapsed));
        public Visibility VisibilityZonaRed
        {
            get { return (Visibility)GetValue(VisibilityRedProperty); }
            set { SetValue(VisibilityRedProperty, value); }
        }

        LocalRow document;


        public static readonly DependencyProperty IsEnableSaveCancelProperty = DependencyProperty.Register(
        "IsEnableSaveCancel",
        typeof(Boolean),
        typeof(Kategorii), new UIPropertyMetadata(false));
        public Boolean IsEnableSaveCancel
        {
            get { return (Boolean)GetValue(IsEnableSaveCancelProperty); }
            set { SetValue(IsEnableSaveCancelProperty, value); }
        }

        private Int32 typeTable;
        private String text;
        private String description;

        public int TypeTable
        {
            get
            {
                return typeTable;
            }

            set
            {
                typeTable = value;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public LocalRow Document
        {
            get
            {
                return document;
            }

            set
            {
                document = value;
                OnPropertyChanged("Document");
            }
        }

        Kategor.KategoriiLogic kategoriiLogic;
        ObservableCollection<Kategor.LocalRow> dataCategorComboBox;
        ObservableCollection<Kategor.KategoryType> dataCategorTreeView; //Выводим категории в TreeView
        ObservableCollection<Kategor.KategoryType> dataCategorTreeViewRelay; //Выводим категории в TreeView

       
        public Kategorii()
        {
            InitializeComponent();
            //загружаем данные в комбо
            kategoriiLogic = new KategoriiLogic();
            dataCategorComboBox = new ObservableCollection<LocalRow>();
            dataCategorTreeView = new ObservableCollection<Kategor.KategoryType>();
            dataCategorTreeViewRelay = new ObservableCollection<Kategor.KategoryType>();

            Document = new LocalRow();

            this.YellowZona.comboBox.ItemsSource = dataCategorComboBox;
            this.treeView1.ItemsSource = dataCategorTreeView;
            this.treeView2.ItemsSource = dataCategorTreeViewRelay;

            InitTreeViewProduct();
            InitTreeViewRelay();
        }

        private void page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        #region производим сортировку зон
        //1 - производим выбор таблицы (товар или сопуствующий товар)
        private void WhiteZona_ButtonSelectChanged()
        {
            if (this.WhiteZona.comboBox.SelectedValue != null & this.WhiteZona.Value!=0)
            {
                VisibilityZonaRed = Visibility.Visible;
                TypeTable = WhiteZona.Value == 1 ? 0 : 1;
            }
            else
            {
                VisibilityZonaRed = Visibility.Collapsed;
                VisibilityZonaYellow = Visibility.Collapsed;
                VisibilityZonaGreen = Visibility.Collapsed;
                IsEnableSaveCancel = false;
            }
            Document.TempTable = TypeTable.ToString();
        }
        //2 - выбираем тип категории (категория или подкатегория)
        private void SelectCategoryRedTable_ButtonSelectChanged()
        {
            dataCategorComboBox.Clear();
            InitComboBox(TypeTable);
            if (this.RedZona.comboBox.SelectedValue != null & this.RedZona.Value != 0)
            {
                if (RedZona.Value == 2)
                {
                    VisibilityZonaYellow = Visibility.Visible;
                    IsEnableSaveCancel = false;
                }
                else
                    VisibilityZonaYellow = Visibility.Collapsed;
                VisibilityZonaGreen = RedZona.Value == 1 ? Visibility.Visible : Visibility.Collapsed;
                IsEnableSaveCancel = VisibilityZonaGreen == Visibility.Visible ? true : false;
            }
            else
            {
                VisibilityZonaYellow = Visibility.Collapsed;
                VisibilityZonaGreen = Visibility.Collapsed;
                IsEnableSaveCancel = false;
            }
        }
        //3 - выбираем какой именно категории относиться (подкатегория)
        private void SelectCategoryYellowTable_ButtonSelectChanged()
        {           
            if (this.YellowZona.comboBox.SelectedValue != null & this.YellowZona.Value != 0)
            {
                VisibilityZonaGreen = Visibility.Visible;
                IsEnableSaveCancel = true;
            }
            else
            {
                VisibilityZonaGreen = Visibility.Collapsed;
                IsEnableSaveCancel = false;
            }
        }

        #endregion

        #region заполнение комбобоксы данными и дерево данными
        private void InitComboBox(Int32 _typeTable)
        {
            dataCategorComboBox.Clear();
            //получили данные
            DataTable table = kategoriiLogic.SelectCategory(_typeTable);

            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                dataCategorComboBox.Add(kategoriiLogic.ConvertCategory(row, new LocalRow()));
            }
        }
        private void InitTreeViewProduct()
        {
            //получили данные
            DataTable table = kategoriiLogic.SelectCategory();
            List<KategoryType> listKategory = new List<KategoryType>();
            KategoryType kategoryType;
            TypeCategory typeCategory;
            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                kategoryType = new KategoryType();
                listKategory.Add(kategoriiLogic.ConvertCategory(row, kategoryType));         //записали лист             

            }

            //внутренний запрос в List
            var queryNumericRange =
                from kategory in listKategory
                let kategorytype1 = kategory.KategoryName
                group new { kategory.ID, kategory.TypeCategoryName } by kategorytype1 into kategorytype2
                orderby kategorytype2.Key
                select kategorytype2;


           
            foreach (var kategorytype in queryNumericRange)
            {
                kategoryType = new KategoryType();
                kategoryType.KategoryName = kategorytype.Key;
                //typeCategory = new TypeCategory();
                foreach (var item in kategorytype)
                {
                    kategoryType.ID = item.ID;
                    if (!String.IsNullOrEmpty(item.TypeCategoryName))
                    {
                        typeCategory = new TypeCategory();
                        typeCategory.Title = item.TypeCategoryName;
                        typeCategory.ID = item.ID;
                        kategoryType.Category.Add(typeCategory);
                    }
                    //dataCategorTreeView.Add(item);
                }
                dataCategorTreeView.Add(kategoryType);
            }
        }

        private void InitTreeViewRelay()
        {           
            //получили данные
            DataTable table = kategoriiLogic.SelectCategoryRelay();
            List<KategoryType> listKategory = new List<KategoryType>();
            KategoryType kategoryType;
            TypeCategory typeCategory;
            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                kategoryType = new KategoryType();
                listKategory.Add(kategoriiLogic.ConvertCategory(row, kategoryType));         //записали лист             

            }

            //внутренний запрос в List
            var queryNumericRange =
                from kategory in listKategory
                let kategorytype1 = kategory.KategoryName
                group new { kategory.ID, kategory.TypeCategoryName } by kategorytype1 into kategorytype2
                orderby kategorytype2.Key
                select kategorytype2;



            foreach (var kategorytype in queryNumericRange)
            {
                kategoryType = new KategoryType();
                kategoryType.KategoryName = kategorytype.Key;
                //typeCategory = new TypeCategory();
                foreach (var item in kategorytype)
                {
                    kategoryType.ID = item.ID;
                    if (!String.IsNullOrEmpty(item.TypeCategoryName))
                    {
                        typeCategory = new TypeCategory();
                        typeCategory.Title = item.TypeCategoryName;
                        typeCategory.ID = item.ID;                      
                        kategoryType.Category.Add(typeCategory);
                    }
                    //dataCategorTreeView.Add(item);
                }
                dataCategorTreeViewRelay.Add(kategoryType);
            }
        }
        #endregion


        #region Save
        private Boolean Save()
        {
            Document.MassCategoryProduct = "";
            Document.MassCategoryIDProduct = "";
            Document.MassCategoryDescriptionProduct = "";

            Document.MassCategoryProductDetails = "";
            Document.MassCategoryProductID = "";
            Document.MassCategoryIDProductDetails = "";
            Document.MassDescriptionProductDetails = "";

            Document.MassCategoryRelay = "";
            Document.MassCategoryIDRelay = "";
            Document.MassCategoryDescriptionRelay = "";           

            Document.MassCategoryRelayDetails = "";
            Document.MassCategoryRelayID = "";
            Document.MassCategoryIDRelayDetails = "";
            Document.MassDescriptionRelayDetails = "";

            if (Document.TempTable == "0")
            {               
                if (this.RedZona.comboBox.SelectedValue.ToString() == "1")
                {
                    Document.TempTable = "0";
                    Document.MassCategoryProduct = this.GreenZona.Value + "|";
                    Document.MassCategoryIDProduct = "0" + "|";
                    Document.MassCategoryDescriptionProduct = this.GreenZona.Description + "|";
                }

                if (this.RedZona.comboBox.SelectedValue.ToString() == "2")
                {
                    Document.TempTable = "1";
                    Document.MassCategoryProductDetails = this.GreenZona.Value + "|";
                    Document.MassCategoryProductID = this.YellowZona.comboBox.SelectedValue.ToString() != "0" ? this.YellowZona.comboBox.SelectedValue + "|" : "0" + "|"; 
                    Document.MassCategoryIDProductDetails = "0" + "|";
                    Document.MassDescriptionProductDetails = this.GreenZona.Description + "|";
                }
            }

            if (Document.TempTable == "1")
            {
                if (this.RedZona.comboBox.SelectedValue.ToString() == "1")
                {
                    Document.TempTable = "0";
                    Document.MassCategoryRelay = this.GreenZona.Value + "|";
                    Document.MassCategoryIDRelay = "0" + "|"; ;
                    Document.MassCategoryDescriptionRelay = this.GreenZona.Description + "|";
                }

                if (this.RedZona.comboBox.SelectedValue.ToString() == "2")
                {
                    Document.TempTable = "1";
                    Document.MassCategoryRelayDetails = this.GreenZona.Value + "|";
                    Document.MassCategoryRelayID = this.YellowZona.comboBox.SelectedValue.ToString() != "0" ? this.YellowZona.comboBox.SelectedValue + "|" : "0" + "|";
                    Document.MassCategoryIDRelayDetails = "0" + "|";
                    Document.MassDescriptionRelayDetails = this.GreenZona.Description + "|";
                }
            }

           if ( kategoriiLogic.SetCategorySave(Document).Rows.Count>0)
                return true;
            return false;
        }

        #endregion

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show(Text + "----" + Description);
        //}

        private void SelectCategoryGreenTable_ButtonInputTextBox()
        {
            Document.MassCategoryDescriptionProduct = "";
            Document.MassCategoryDescriptionRelay = "";
            Document.MassCategoryIDProduct = "";
            Document.MassCategoryIDRelay = "";
            Document.MassCategoryProduct = "";
            Document.MassCategoryRelay = "";

            //перебор коллекции продуктов 

            //перебор коллекции сопутсвующего товара
        }

        private void GreenZona_ButtonInputTextBoxDescription()
        {
            Description = GreenZona.Description;
        }

        private void GreenZona_ButtonInputTextBox()
        {
            Text = GreenZona.Value;
        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {         
            InitComboBox(0);
            // Tovar.LocalRow local = DataGrid.SelectedItem as Tovar.LocalRow;
            var row = this.treeView1.SelectedItem as Kategor.KategoryType;
            if (row!=null)
            {
                Document.TempTable = "0";
                this.WhiteZona.comboBox.SelectedValue = 1;
                this.RedZona.comboBox.SelectedValue = 2;             
                this.YellowZona.comboBox.SelectedValue = row.ID;
                this.GreenZona.Visibility = Visibility.Visible;
                IsEnableSaveCancel = true;
                this.GreenZona.LabelNameText = Properties.Resources.NameTypeCategory;
            }
            // Tovar.LocalRow local = DataGrid.SelectedItem as Tovar.LocalRow;
            
        }

        private void treeView2_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {          
            InitComboBox(1);
            var row1 = this.treeView2.SelectedItem as Kategor.KategoryType;
            if (row1 != null)
            {
                Document.TempTable = "1";
                this.WhiteZona.comboBox.SelectedValue = 2;
                this.RedZona.comboBox.SelectedValue = 2;
                this.YellowZona.comboBox.SelectedValue = row1.ID;
                this.GreenZona.Visibility = Visibility.Visible;
                IsEnableSaveCancel = true;
                this.GreenZona.LabelNameText = Properties.Resources.NameTypeCategory;
            }
        }

        private void ToolBarSaveCancel_ButtonApply()
        {
            if (Save() == true)
            {

                dataCategorTreeView.Clear();
                dataCategorTreeViewRelay.Clear();

                InitTreeViewProduct();
                InitTreeViewRelay();

                this.GreenZona.Value = "";
                this.GreenZona.Description = "";

                VisibilityZonaGreen = Visibility.Collapsed;
                VisibilityZonaRed= Visibility.Collapsed;
                VisibilityZonaYellow= Visibility.Collapsed;
                IsEnableSaveCancel = false;
            }
            else
            {
                MessageBox.Show("Произошла ошибка при добавлении данных в БД");
            }
            
        }
    }
}
