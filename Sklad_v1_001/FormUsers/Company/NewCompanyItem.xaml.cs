using Sklad_v1_001.GlobalAttributes;
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
            this.attributes = _attributes;
            this.DataContext = LocaleRowCompany;
        }
    }
}
