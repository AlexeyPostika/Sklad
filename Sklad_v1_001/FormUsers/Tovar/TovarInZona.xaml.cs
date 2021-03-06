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

namespace Sklad_v1_001.FormUsers.Tovar
{
    /// <summary>
    /// Логика взаимодействия для TovarInZona.xaml
    /// </summary>
    public partial class TovarInZona : Page
    {
        LocalRow localRow;
        public LocalRow LocalRow {
            get
            {
                return localRow;
            }

            set
            {
                localRow = value;
                this.Edit.LocalDocument = LocalRow;
            }
        }
        public TovarInZona()
        {
            InitializeComponent();
            LocalRow = new LocalRow();
        }     
    }
}
