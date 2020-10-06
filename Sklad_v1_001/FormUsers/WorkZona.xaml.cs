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
using Sklad_v1_001.FormUsers.Kategor;
using Sklad_v1_001.FormUsers.Prixod;
using Sklad_v1_001.FormUsers.Tovar;

namespace Sklad_v1_001.FormUsers
{
    /// <summary>
    /// Interaction logic for WorkZona.xaml
    /// </summary>
    /// </summary>
    /// </summary>  
    public partial class WorkZona : UserControl
    {
        public string ViewModel { get; set; }
        public WorkZona()
        {
            InitializeComponent();
        }
        public void ShowViewModel()
        {
            MessageBox.Show(ViewModel);
        }

        private void ListTovar_Click(object sender, RoutedEventArgs e)
        {
            Docker1.Content=new TovarZona();
        }

        private void PrixodTovara_Click(object sender, RoutedEventArgs e)
        {
            //Docker1.Content = new RegistraciyTovara();
            Docker1.Navigate(new RegistraciyTovara()); // открытие страницы
        }

        private void Kategorii_Click(object sender, RoutedEventArgs e)
        {
            Docker1.Navigate(new Kategorii()); // открытие страницы
        }

        private void Admins_Click(object sender, RoutedEventArgs e)
        {
            Docker1.Content = new FormUsers.Userss.UserList();
        }

        private void Purchase_Click(object sender, RoutedEventArgs e)
        {
            Docker1.Navigate(new Zacupca.ZacupcaGrid()); // открытие страницы
        }
    }
}
