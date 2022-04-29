using Sklad_v1_001.GlobalVariable;
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
using System.Windows.Shapes;

namespace Sklad_v1_001
{
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();

            EditLogin.EditBoxUser.image.Source = ImageHelper.GenerateImage("IconUsers_X24.png");
            Login.Image.Source = ImageHelper.GenerateImage("IconOK_x24.png");
            Exit.Image.Source = ImageHelper.GenerateImage("IconClose_X24.png");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void UserLogin_DropDownClosed()
        {

        }

        private void EditLogin_ButtonClearClick()
        {

        }

        private void Login_ButtonClick()
        {

        }

        private void Exit_ButtonClick()
        {

        }
    }
}
