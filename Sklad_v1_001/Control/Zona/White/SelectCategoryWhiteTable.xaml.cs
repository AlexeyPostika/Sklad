﻿using Sklad_v1_001.GlobalList;
using System;
using System.Collections.Generic;
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

namespace Sklad_v1_001.Control.Zona.White
{
    /// <summary>
    /// Логика взаимодействия для SelectCategoryTable.xaml
    /// </summary>
    public partial class SelectCategoryWhiteTable : UserControl
    {
        // свойство зависимостей
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
                        "Value",
                        typeof(Int32),
                        typeof(SelectCategoryWhiteTable), new UIPropertyMetadata(0));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 Value
        {
            get
            {
                return (Int32)GetValue(ValueProperty);
            }
            set
            {
                SetValue(ValueProperty, value);
            }
        }
        // свойство зависимостей
        public static readonly DependencyProperty LabelNameTextProperty = DependencyProperty.Register(
                        "LabelNameText",
                        typeof(String),
                        typeof(SelectCategoryWhiteTable), new UIPropertyMetadata(""));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public String LabelNameText
        {
            get
            {
                return (String)GetValue(LabelNameTextProperty);
            }
            set
            {
                SetValue(LabelNameTextProperty, value);
            }
        }

        // свойство зависимостей
        public static readonly DependencyProperty WidthLabelProperty = DependencyProperty.Register(
                        "WidthLabel",
                        typeof(Int32),
                        typeof(SelectCategoryWhiteTable), new UIPropertyMetadata(10));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 WidthLabel
        {
            get
            {
                return (Int32)GetValue(WidthLabelProperty);
            }
            set
            {
                SetValue(WidthLabelProperty, value);
            }
        }
        // свойство зависимостей
        public static readonly DependencyProperty WidthComboBoxProperty = DependencyProperty.Register(
                        "WidthComboBox",
                        typeof(Int32),
                        typeof(SelectCategoryWhiteTable), new UIPropertyMetadata(10));
        // Обычное свойство .NET  - обертка над свойством зависимостей
        public Int32 WidthComboBox
        {
            get
            {
                return (Int32)GetValue(WidthComboBoxProperty);
            }
            set
            {
                SetValue(WidthComboBoxProperty, value);
            }
        }
        CategoryList сategoryList;
        public SelectCategoryWhiteTable()
        {
            InitializeComponent();
            сategoryList = new CategoryList();
            this.comboBox.ItemsSource = сategoryList.innerList;

        }
        public event Action ButtonSelectChanged;
        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ButtonSelectChanged?.Invoke();
        }

        private void comboBox_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
