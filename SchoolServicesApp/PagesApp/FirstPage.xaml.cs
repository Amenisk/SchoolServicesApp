using SchoolServicesApp.Classes;
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

namespace SchoolServicesApp.PagesApp
{
    /// <summary>
    /// Логика взаимодействия для FirstPage.xaml
    /// </summary>
    public partial class FirstPage : Page
    {
        public FirstPage()
        {
            InitializeComponent();
        }

        private void GoToApp(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesListPage());
        }

        private void GoToAppWithCode(object sender, RoutedEventArgs e)
        {
            if(tbCode.Text == Data.Code)
            {
                Data.Role = "Администратор";
                NavigationService.Navigate(new ServicesListPage());
            }
            else
            {
                MessageBox.Show("Неверный код!", "Ошибка!", MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }
    }
}
