using Sklad_v1_001.Control.FlexMessageBox;
using Sklad_v1_001.GlobalAttributes;
using Sklad_v1_001.GlobalVariable;
using Sklad_v1_001.HelperGlobal;
using System;
using System.Collections.Generic;
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
using static Sklad_v1_001.HelperGlobal.MessageBoxTitleHelper;

namespace Sklad_v1_001
{
    /// <summary>
    /// Логика взаимодействия для LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        Attributes attributes;

        ConvertData convertData;
        FormUsers.Users.UserLogic userLogic;

        public LoginScreen()
        {
            InitializeComponent();
            attributes = new Attributes();
            convertData = new ConvertData();

            userLogic = new FormUsers.Users.UserLogic(attributes);

            UserLogin.ComboBoxElement.ItemsSource = attributes.datalistUsers;

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
            if (UserLogin.Value != 0)
            {
                EditLogin.Text = attributes.datalistUsers.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(UserLogin.Value.ToString())) != null ?
                    attributes.datalistUsers.FirstOrDefault(x => x.ID == convertData.FlexDataConvertToInt32(UserLogin.Value.ToString())).Login :
                    Properties.Resources.UndefindField;
            }

            UserLogin.Visibility = Visibility.Collapsed;
            EditLogin.Visibility = Visibility.Visible;
        }

        private void EditLogin_ButtonClearClick()
        {
            UserLogin.Visibility = Visibility.Visible;
            UserLogin.ComboBoxElement.IsDropDownOpen = true;
            EditLogin.Visibility = Visibility.Collapsed;
        }

        private void Login_ButtonClick()
        {
            FlexMessageBox mb = new FlexMessageBox();

            DataTable data = userLogic.FillGrid(EditLogin.Text);
            string etalonPassword = "";

            if (data != null && data.Rows.Count > 0)
            {

                FormUsers.Users.LocalRow localeRow = new FormUsers.Users.LocalRow();
                userLogic.Convert(data.Rows[0], localeRow);

                etalonPassword = localeRow.Password;               

                if (Password.TextPassword == localeRow.Password)
                {
                    mb.Show(Properties.Resources.LoginSuccessfulMessage, Properties.Resources.LoginSuccessfulTitle, MessageBoxButton.OK, MessageBoxImage.Asterisk, "Privet.mp3");
                    attributes.numeric.userEdit.AddUserID = localeRow.ID;
                    attributes.numeric.userEdit.RoleID = localeRow.RoleID;                   

                    Hide();

                    MainWindow _mainWindow = new MainWindow(attributes);
                    _mainWindow.ShowDialog();

                    //_mainWindow.PageframeMenuLevel1.CheckNotification();

                    EditLogin.Text = "";
                    Password.TextBox.Text = "";
                    Password.PasswordTextBox.Password = "";
                    Password.IsShowPassword = false;
                    UserLogin.ComboBoxElement.SelectedValue = 0;

                    EditLogin.EditBoxUser.image.Source = ImageHelper.GenerateImage("IconUsers_X24.png");
                    Login.Image.Source = ImageHelper.GenerateImage("IconOK_x24.png");
                    Exit.Image.Source = ImageHelper.GenerateImage("IconClose_X24.png");

                    Show();

                    return;
                }
                else
                    mb.Show(Properties.Resources.LoginErrorMessage, GenerateTitle(TitleType.Error, Properties.Resources.BadData), MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                mb.Show(Properties.Resources.LoginErrorMessage, GenerateTitle(TitleType.Error, Properties.Resources.BadData), MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Exit_ButtonClick()
        {
            FlexMessageBox mb = new FlexMessageBox();
            mb.Owner = this;
            MessageBoxResult dialogresult = mb.Show(Properties.Resources.ExitMessage, GenerateTitle(TitleType.Question, Properties.Resources.SystemExit), MessageBoxButton.OKCancel, MessageBoxImage.Question, 3);

            if (dialogresult == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
    }
}
