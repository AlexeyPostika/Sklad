using Sklad_v1_001.GlobalList;
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

        Kategor.KategoriiLogic kategoriiLogic;
        ObservableCollection<Kategor.LocalRow> dataCategor;
        ObservableCollection<Kategor.KategoryType> dataCategorTreeView; //Выводим категории в TreeView
        ObservableCollection<Kategor.KategoryType> dataCategorTreeView1; //Выводим категории в TreeView
        ObservableCollection<Kategor.CategoryType> dataCategoryTypeTreeView; //Выводим категории в TreeView
        public Kategorii()
        {
            InitializeComponent();
            //загружаем данные в комбо
            kategoriiLogic = new KategoriiLogic();
            dataCategor = new ObservableCollection<LocalRow>();
            dataCategorTreeView = new ObservableCollection<Kategor.KategoryType>();
            dataCategorTreeView1 = new ObservableCollection<Kategor.KategoryType>();
            dataCategoryTypeTreeView = new ObservableCollection<CategoryType>();
            //dataCategorTreeView = new ObservableCollection<Kategor.KategoryType>() { 
            //new KategoryType
            //{
            //    KategoryName ="Европа",
            //    Category = new ObservableCollection<CategoryType>
            //    {
            //        new CategoryType {Title="Германия" },
            //        new CategoryType {Title="Франция" },
            //        //new KategoryType
            //        //{
            //        //    KategoryName ="Великобритания",
            //        //    Category = new ObservableCollection<CategoryType>
            //        //    {
            //        //        new CategoryType {Title="Англия" },
            //        //        new CategoryType {Title="Шотландия" },
            //        //        new CategoryType {Title="Уэльс" },
            //        //        new CategoryType {Title="Сев. Ирландия" },
            //        //    }
            //        //}
            //    }
            //},
            //new KategoryType
            //{
            //    KategoryName ="Азия",
            //    Category = new ObservableCollection<CategoryType>
            //    {
            //        new CategoryType {Title="Китай" },
            //        new CategoryType {Title="Япония" },
            //        new CategoryType { Title ="Индия" }
            //    }
            //},
            //new KategoryType { KategoryName="Африка" },
            //new KategoryType { KategoryName="Америка" },
            //new KategoryType { KategoryName="Австралия" }
            //};

            this.YellowZona.comboBox.ItemsSource = dataCategor;
            this.treeView1.ItemsSource = dataCategorTreeView;
            InitTreeView();
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
            }
        }
        //2 - выбираем тип категории (категория или подкатегория)
        private void SelectCategoryRedTable_ButtonSelectChanged()
        {
            if (this.RedZona.comboBox.SelectedValue != null & this.RedZona.Value != 0)
            {
                if (RedZona.Value == 2)
                {
                    VisibilityZonaYellow = Visibility.Visible;
                    InitComboBox(TypeTable);
                }
                else
                    VisibilityZonaYellow = Visibility.Collapsed;
                VisibilityZonaGreen = RedZona.Value == 1 ? Visibility.Visible : Visibility.Collapsed; 
            }
            else
            {
                VisibilityZonaYellow = Visibility.Collapsed;
                VisibilityZonaGreen = Visibility.Collapsed;
            }
        }
        //3 - выбираем какой именно категории относиться (подкатегория)
        private void SelectCategoryYellowTable_ButtonSelectChanged()
        {
            if (this.YellowZona.comboBox.SelectedValue != null & this.YellowZona.Value != 0)
            {
                VisibilityZonaGreen = Visibility.Visible;
            }
            else
            {
                VisibilityZonaGreen = Visibility.Collapsed;
            }
        }

        #endregion

        #region заполнение комбобоксы данными и дерево данными
        private void InitComboBox(Int32 _typeTable)
        {
            dataCategor.Clear();
            //получили данные
            DataTable table = kategoriiLogic.SelectCategory(_typeTable);

            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                dataCategor.Add(kategoriiLogic.ConvertCategory(row, new LocalRow()));
            }
        }
        private void InitTreeView()
        {
            dataCategor.Clear();
            //получили данные
            DataTable table = kategoriiLogic.SelectCategory();
            List<KategoryType> listKategory = new List<KategoryType>();
            KategoryType kategoryType;
            TypeCategory typeCategory;
            //внутренний запрос в List
            var queryNumericRange =
                from kategory in listKategory
                let kategorytype1 = kategory.KategoryName
                group new { kategory.ID, kategory.TypeCategoryName } by kategorytype1 into kategorytype2
                orderby kategorytype2.Key
                select kategorytype2;
            //
            //{ 


            //заполнили данные
            foreach (DataRow row in table.Rows)
            {
                kategoryType = new KategoryType();
                listKategory.Add(kategoriiLogic.ConvertCategory(row, kategoryType));         //записали лист             

            }
            foreach (var kategorytype in queryNumericRange)
            {
                kategoryType = new KategoryType();
                kategoryType.KategoryName = kategorytype.Key;
                //typeCategory = new TypeCategory();
                foreach (var item in kategorytype)
                {
                    typeCategory = new TypeCategory();
                    typeCategory.Title = item.TypeCategoryName;
                    kategoryType.Category.Add( typeCategory);
                    //dataCategorTreeView.Add(item);
                }
                dataCategorTreeView.Add(kategoryType);
            }

            treeView1.ItemsSource = dataCategorTreeView1;
        }
        #endregion

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Text + "----" + Description);
        }

        private void SelectCategoryGreenTable_ButtonInputTextBox()
        {
            
        }

        private void GreenZona_ButtonInputTextBoxDescription()
        {
            Description = GreenZona.Description;
        }

        private void GreenZona_ButtonInputTextBox()
        {
            Text = GreenZona.Value;
        }
    }
}
