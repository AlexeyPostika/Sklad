﻿using System;
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

namespace Sklad_v1_001.Control.FlexTextBox
{
    /// <summary>
    /// Логика взаимодействия для FlexLabelTextBox.xaml
    /// </summary>
    public partial class FlexLabelTextBlock : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty = DependencyProperty.Register(
        "LabelText",
        typeof(String),
        typeof(FlexLabelTextBlock), new UIPropertyMetadata(Properties.Resources.Name));
        public String LabelText
        {
            get { return (String)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty = DependencyProperty.Register(
       "Description",
       typeof(String),
       typeof(FlexLabelTextBlock), new UIPropertyMetadata());
        public String Description
        {
            get { return (String)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty EnableTextBoxProperty = DependencyProperty.Register(
       "EnableTextBox",
       typeof(Boolean),
       typeof(FlexLabelTextBlock), new UIPropertyMetadata());
        public Boolean EnableTextBox
        {
            get { return (Boolean)GetValue(EnableTextBoxProperty); }
            set { SetValue(EnableTextBoxProperty, value); }
        }
        //HeightTextBlock
        public static readonly DependencyProperty HeightTextBlockProperty = DependencyProperty.Register(
       "HeightTextBlock",
       typeof(Int32),
       typeof(FlexLabelTextBlock), new UIPropertyMetadata(30));
        public Int32 HeightTextBlock
        {
            get { return (Int32)GetValue(HeightTextBlockProperty); }
            set { SetValue(HeightTextBlockProperty, value); }
        }
        public FlexLabelTextBlock()
        {
            InitializeComponent();
        }
    }
}
