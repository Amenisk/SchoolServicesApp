using SchoolServicesApp.Classes;
using SchoolServicesApp.PagesApp;
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

namespace SchoolServicesApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button exit;
        Button back;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new FirstPage());
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (Data.Role == "Администратор")
            {
                Data.Role = "";
            }
            MainFrame.Navigate(new FirstPage());
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if(MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }
    }
}
