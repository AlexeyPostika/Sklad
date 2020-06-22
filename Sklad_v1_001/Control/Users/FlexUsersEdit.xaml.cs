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

namespace Sklad_v1_001.Control.Users
{
    /// <summary>
    /// Interaction logic for FlexUsersEdit.xaml
    /// </summary>
    public partial class FlexUsersEdit : UserControl
    {
        public static readonly DependencyProperty LastNamePageProperty = DependencyProperty.Register(
        "LastName1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(""));

        public String LastName1
        {
            get { return (String)GetValue(LastNamePageProperty); }
            set { SetValue(LastNamePageProperty, value); }
        }

        public static readonly DependencyProperty FirstNamePageProperty = DependencyProperty.Register(
        "FirstName1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(""));

        public String FirstName1
        {
            get { return (String)GetValue(FirstNamePageProperty); }
            set { SetValue(FirstNamePageProperty, value); }
        }

        public static readonly DependencyProperty OtchestvoPageProperty = DependencyProperty.Register(
       "Otchestvo1",
       typeof(String),
       typeof(FlexUsersEdit), new UIPropertyMetadata(""));

        public String Otchestvo1
        {
            get { return (String)GetValue(OtchestvoPageProperty); }
            set { SetValue(OtchestvoPageProperty, value); }
        }


        public static readonly DependencyProperty OtdelPageProperty = DependencyProperty.Register(
        "Otdel1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(""));

        public String Otdel1
        {
            get { return (String)GetValue(OtdelPageProperty); }
            set { SetValue(OtdelPageProperty, value); }
        }

        public static readonly DependencyProperty PhonePageProperty = DependencyProperty.Register(
        "Phone1",
        typeof(String),
        typeof(FlexUsersEdit), new UIPropertyMetadata(""));

        public String Phone1
        {
            get { return (String)GetValue(PhonePageProperty); }
            set { SetValue(PhonePageProperty, value); }
        }

        public static readonly DependencyProperty NumberSotrudnikaPageProperty = DependencyProperty.Register(
       "NumberSotrudnika1",
       typeof(String),
       typeof(FlexUsersEdit), new UIPropertyMetadata(""));

        public String NumberSotrudnika1
        {
            get { return (String)GetValue(NumberSotrudnikaPageProperty); }
            set { SetValue(NumberSotrudnikaPageProperty, value); }
        }
        public FlexUsersEdit()
        {
            InitializeComponent();
        }
    }
}
