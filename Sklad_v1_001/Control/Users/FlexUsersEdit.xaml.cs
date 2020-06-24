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

namespace Sklad_v1_001.Control.Users
{
    /// <summary>
    /// Interaction logic for FlexUsersEdit.xaml
    /// </summary>
    public partial class FlexUsersEdit : UserControl
    {
        public static readonly DependencyProperty LastName1PageProperty = DependencyProperty.Register(
        "LastName1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(String.Empty));

        public String LastName1
        {
            get { return (String)GetValue(LastName1PageProperty); }
            set { SetValue(LastName1PageProperty, value); }
        }

        public static readonly DependencyProperty FirstName1PageProperty = DependencyProperty.Register(
        "FirstName1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(String.Empty));

        public String FirstName1
        {
            get { return (String)GetValue(FirstName1PageProperty); }
            set { SetValue(FirstName1PageProperty, value); }
        }

        public static readonly DependencyProperty Otchestvo1PageProperty = DependencyProperty.Register(
       "Otchestvo1",
       typeof(String),
       typeof(FlexUsersEdit), new UIPropertyMetadata(String.Empty));

        public String Otchestvo1
        {
            get { return (String)GetValue(Otchestvo1PageProperty); }
            set { SetValue(Otchestvo1PageProperty, value); }
        }


        public static readonly DependencyProperty Otdel1PageProperty = DependencyProperty.Register(
        "Otdel1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(String.Empty));

        public String Otdel1
        {
            get { return (String)GetValue(Otdel1PageProperty); }
            set { SetValue(Otdel1PageProperty, value); }
        }

        public static readonly DependencyProperty Phone1PageProperty = DependencyProperty.Register(
        "Phone1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(String.Empty));

        public String Phone1
        {
            get { return (String)GetValue(Phone1PageProperty); }
            set { SetValue(Phone1PageProperty, value); }
        }

        public static readonly DependencyProperty NumberSotrudnika1PageProperty = DependencyProperty.Register(
       "NumberSotrudnika1",
       typeof(String),
       typeof(FlexUsersEdit), new UIPropertyMetadata(String.Empty));

        public String NumberSotrudnika1
        {
            get { return (String)GetValue(NumberSotrudnika1PageProperty); }
            set { SetValue(NumberSotrudnika1PageProperty, value); }
        }

        public FlexUsersEdit()
        {
            InitializeComponent();
        }
    }
}
