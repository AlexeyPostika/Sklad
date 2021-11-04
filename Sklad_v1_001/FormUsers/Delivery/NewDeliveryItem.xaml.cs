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
using Sklad_v1_001.GlobalVariable;

namespace Sklad_v1_001.FormUsers.Delivery
{
    /// <summary>
    /// Логика взаимодействия для NewDeliveryItem.xaml
    /// </summary>
    public partial class NewDeliveryItem : Page
    {
        FileWork fileWork;
        public NewDeliveryItem()
        {
            InitializeComponent();
            fileWork = new FileWork();
        }

        private void Invoice_ButtonAddClick()
        {
            fileWork.OpenPDFtoImage();
            if (fileWork.Source!=null)
                this.Invoice.Source = ImageHelper.GenerateImage("IconClose.png");
            //OpenPDFtoImage
        }

        private void TTN_ButtonAddClick()
        {

        }
    }
}
