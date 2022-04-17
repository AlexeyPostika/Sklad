﻿using Sklad_v1_001.GlobalVariable;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
using System.Windows.Shapes;

namespace Sklad_v1_001.Control.FlexFilter
{
    /// <summary>
    /// Логика взаимодействия для FlexFilterContenerProductWindows.xaml
    /// </summary>
    public partial class FlexFilterContenerProductWindows : DialogWindow
    {        
        public static readonly DependencyProperty Value1Property = DependencyProperty.Register(
                     "Value1",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Value2Property = DependencyProperty.Register(
                     "Value2",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Value3Property = DependencyProperty.Register(
                     "Value3",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Value4Property = DependencyProperty.Register(
                     "Value4",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Value5Property = DependencyProperty.Register(
                     "Value5",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Value6Property = DependencyProperty.Register(
                     "Value6",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Value7Property = DependencyProperty.Register(
                     "Value7",
                      typeof(String),
                      typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(String.Empty));

        public static readonly DependencyProperty Quantity_MinProperty = DependencyProperty.Register(
                    "Quantity_Min",
                     typeof(Double),
                     typeof(FlexFilterContenerProductWindows));

        public static readonly DependencyProperty Quantity_MaxProperty = DependencyProperty.Register(
                    "Quantity_Max",
                     typeof(Double),
                     typeof(FlexFilterContenerProductWindows));

        public static readonly DependencyProperty TagPrice_MinProperty = DependencyProperty.Register(
                   "TagPrice_Min",
                    typeof(Double),
                    typeof(FlexFilterContenerProductWindows));

        public static readonly DependencyProperty TagPrice_MaxProperty = DependencyProperty.Register(
                   "TagPrice_Max",
                    typeof(Double),
                    typeof(FlexFilterContenerProductWindows));

        public static readonly DependencyProperty DataTableFilter1Property = DependencyProperty.Register(
                  "DataTableFilter1",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));

        public static readonly DependencyProperty DataTableFilter2Property = DependencyProperty.Register(
                  "DataTableFilter2",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));

        public static readonly DependencyProperty DataTableFilter3Property = DependencyProperty.Register(
                  "DataTableFilter3",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));

        public static readonly DependencyProperty DataTableFilter4Property = DependencyProperty.Register(
                  "DataTableFilter4",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));

        public static readonly DependencyProperty DataTableFilter5Property = DependencyProperty.Register(
                  "DataTableFilter5",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));

        public static readonly DependencyProperty DataTableFilter6Property = DependencyProperty.Register(
                  "DataTableFilter6",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));
        
        public static readonly DependencyProperty DataTableFilter7Property = DependencyProperty.Register(
                  "DataTableFilter7",
                   typeof(DataTable),
                   typeof(FlexFilterContenerProductWindows), new UIPropertyMetadata(new DataTable()));

        public String Value1
        {
            get { return (String)GetValue(Value1Property); }
            set { SetValue(Value1Property, value); }
        }

        public String Value2
        {
            get { return (String)GetValue(Value2Property); }
            set { SetValue(Value2Property, value); }
        }
        public String Value3
        {
            get { return (String)GetValue(Value3Property); }
            set { SetValue(Value3Property, value); }
        }
        public String Value4
        {
            get { return (String)GetValue(Value4Property); }
            set { SetValue(Value4Property, value); }
        }
        public String Value5
        {
            get { return (String)GetValue(Value5Property); }
            set { SetValue(Value5Property, value); }
        }
        public String Value6
        {
            get { return (String)GetValue(Value6Property); }
            set { SetValue(Value6Property, value); }
        }
        public String Value7
        {
            get { return (String)GetValue(Value7Property); }
            set { SetValue(Value7Property, value); }
        }

        public Double Quantity_Min
        {           
            get { return (Double)GetValue(Quantity_MinProperty); }
            set { SetValue(Quantity_MinProperty, value); }
        }

        public Double Quantity_Max
        {          
            get { return (Double)GetValue(Quantity_MaxProperty); }
            set { SetValue(Quantity_MaxProperty, value); }
        }

        public Double TagPrice_Min
        {          
            get { return (Double)GetValue(TagPrice_MinProperty); }
            set { SetValue(TagPrice_MinProperty, value); }
        }

        public Double TagPrice_Max
        {            
            get { return (Double)GetValue(TagPrice_MaxProperty); }
            set { SetValue(TagPrice_MaxProperty, value); }
        }

        public DataTable DataTableFilter1
        {
            get { return (DataTable)GetValue(DataTableFilter1Property); }
            set { SetValue(DataTableFilter1Property, value); }
        }

        public DataTable DataTableFilter2
        {
            get { return (DataTable)GetValue(DataTableFilter2Property); }
            set { SetValue(DataTableFilter2Property, value); }
        }

        public DataTable DataTableFilter3
        {
            get { return (DataTable)GetValue(DataTableFilter3Property); }
            set { SetValue(DataTableFilter3Property, value); }
        }

        public DataTable DataTableFilter4
        {
            get { return (DataTable)GetValue(DataTableFilter4Property); }
            set { SetValue(DataTableFilter4Property, value); }
        }

        public DataTable DataTableFilter5
        {
            get { return (DataTable)GetValue(DataTableFilter5Property); }
            set { SetValue(DataTableFilter5Property, value); }
        }

        public DataTable DataTableFilter6
        {
            get { return (DataTable)GetValue(DataTableFilter6Property); }
            set { SetValue(DataTableFilter6Property, value); }
        }

        public DataTable DataTableFilter7
        {
            get { return (DataTable)GetValue(DataTableFilter7Property); }
            set { SetValue(DataTableFilter7Property, value); }
        }

        Boolean needResize;

        public Boolean NeedResize
        {
            get
            {
                return needResize;
            }

            set
            {
                needResize = value;
            }
        }
        public delegate void ButtonFilter1ClickHandler(String text);
        public delegate void ButtonFilter2ClickHandler(String text);
        public delegate void ButtonFilter3ClickHandler(String text);
        public delegate void ButtonFilter4ClickHandler(String text);
        public delegate void ButtonFilter5ClickHandler(String text);
        public delegate void ButtonFilter6ClickHandler(String text);
        public delegate void ButtonFilter7ClickHandler(String text);
        public delegate void ButtonQuantityMinClickHandler(Double text);
        public delegate void ButtonQuantityMaxClickHandler(Double text);
        public delegate void ButtonTagPriceMinClickHandler(Double text);
        public delegate void ButtonTagPriceMaxClickHandler(Double text);
        public event ButtonFilter1ClickHandler ButtonFilter1Click;
        public event ButtonFilter2ClickHandler ButtonFilter2Click;
        public event ButtonFilter3ClickHandler ButtonFilter3Click;
        public event ButtonFilter4ClickHandler ButtonFilter4Click;
        public event ButtonFilter5ClickHandler ButtonFilter5Click;
        public event ButtonFilter6ClickHandler ButtonFilter6Click;
        public event ButtonFilter7ClickHandler ButtonFilter7Click;
        public event ButtonQuantityMinClickHandler ButtonQuantityMinClick;
        public event ButtonQuantityMaxClickHandler ButtonQuantityMaxClick;
        public event ButtonTagPriceMinClickHandler ButtonTagPriceMinClick;
        public event ButtonTagPriceMaxClickHandler ButtonTagPriceMaxClick;

        public FlexFilterContenerProductWindows()
        {
            InitializeComponent();
            
            this.Activated += ContentActivated;
            Filter1.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
            Filter2.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
            Filter3.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
            Filter4.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
            Filter5.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
            Filter6.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
            Filter7.ImageSource = ImageHelper.GenerateImage("IconFilter.png");
        }

        private void Filter1_ButtonApplyClick(string text)
        {
            Value1 = text;
            ButtonFilter1Click?.Invoke(Value1);
        }

        private void Filter2_ButtonApplyClick(string text)
        {
            Value2 = text;
            ButtonFilter2Click?.Invoke(Value2);
        }

        private void Filter3_ButtonApplyClick(string text)
        {
            Value3 = text;
            ButtonFilter3Click?.Invoke(Value3);
        }

        private void Filter4_ButtonApplyClick(string text)
        {
            Value4 = text;
            ButtonFilter4Click?.Invoke(Value4);
        }

        private void Filter5_ButtonApplyClick(string text)
        {
            Value5 = text;
            ButtonFilter5Click?.Invoke(Value5);
        }

        private void Filter6_ButtonApplyClick(string text)
        {
            Value6 = text;
            ButtonFilter6Click?.Invoke(Value6);
        }

        private void Filter7_ButtonApplyClick(string text)
        {
            Value7 = text;
            ButtonFilter7Click?.Invoke(Value7);
        }

        private void Close_ButtonCloseClick()
        {
            this.Visibility = Visibility.Hidden;
            this.Activated += ContentActivated;
            NeedResize = false;
        }

        private void ContentActivated(object sender, EventArgs e)
        {
            NeedResize = true;
            var frameWorkAreaX = MainWindow.AppWindow.frameWorkArea.PointToScreen(new Point(0, 0)).X;
            var frameWorkAreaActualWidth = MainWindow.AppWindow.frameWorkArea.ActualWidth;
            if (this.Left - this.ActualWidth <= frameWorkAreaX)
                this.Left = frameWorkAreaX;
            else
                this.Left = this.Left - this.ActualWidth;
            if (this.Left + this.ActualWidth > frameWorkAreaX + frameWorkAreaActualWidth)
            {
                this.MaxWidth = frameWorkAreaActualWidth - this.Left + frameWorkAreaX;
            }
            this.Activated -= ContentActivated;
        }

        private void quantityMin_TextChanged()
        {
            ButtonQuantityMinClick?.Invoke(Quantity_Min);
        }

        private void quantityMax_TextChanged()
        {
            ButtonQuantityMaxClick?.Invoke(Quantity_Max);
        }

        private void tagPriceRusMin_TextChanged()
        {
            ButtonTagPriceMinClick?.Invoke(TagPrice_Min);
        }

        private void tagPriceRusMax_TextChanged()
        {
            ButtonTagPriceMaxClick?.Invoke(TagPrice_Max);
        }
    }
}