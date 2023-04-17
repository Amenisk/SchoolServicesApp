using SchoolServicesApp.ADOApp;
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
    /// Логика взаимодействия для ServicesListPage.xaml
    /// </summary>
    public partial class ServicesListPage : Page
    {
        List<ServiceInformation> servicesListInformation = new List<ServiceInformation>();
        List<ServiceInformation> sortList = new List<ServiceInformation>();
        public ServicesListPage()
        {
            InitializeComponent();
            cmbSort.SelectedIndex = 0;
            cmbFilter.SelectedIndex = 0;
            List<Service> servicesList = App.Connection.Service.ToList();
            foreach (Service s in servicesList)
            {
                servicesListInformation.Add(new ServiceInformation(s.ID, s.Title, s.Cost, s.DurationInSeconds, s.Description, s.Discount, s.MainImagePath, Data.Role));
            }
            lvServices.ItemsSource = servicesListInformation;
            if (Data.Role == "Администратор")
            {
                btnAdd.Visibility = Visibility.Visible;
                btnAddEntry.Visibility = Visibility.Visible;
                btnEntries.Visibility = Visibility.Visible;
            }
                
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            var deleteService = App.Connection.Service.FirstOrDefault(x => x.ID == tag);

            foreach (ServiceInformation s in servicesListInformation)
            {
                if (s.ID == tag)
                {
                    servicesListInformation.Remove(s);
                    break;
                }
            }

            App.Connection.Service.Remove(deleteService);
            App.Connection.SaveChanges();
            MessageBox.Show("Сервис успешно удалён!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.Navigate(new ServicesListPage());
        }

        private void GoToChangeServicePage(object sender, RoutedEventArgs e)
        {
            var tag = (int)((Button)sender).Tag;
            var changeService = App.Connection.Service.FirstOrDefault(x => x.ID == tag);

            NavigationService.Navigate(new AddServicePage(changeService));
        }

        private void GoToAddServicePage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddServicePage());
        }

        private void Sort(object sender, SelectionChangedEventArgs e)
        {
            SortList();
        }

        private void SortList()
        {
            sortList = servicesListInformation.Where(x => x.Title.ToLower().Contains(tbFinder.Text.ToLower())).ToList();

            switch (cmbFilter.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    sortList = sortList.Where(x => x.DiscountPercent >= 0 && x.DiscountPercent < 0).ToList();
                    break;

                case 2:
                    sortList = sortList.Where(x => x.DiscountPercent >= 5 && x.DiscountPercent < 15).ToList();
                    break;

                case 3:
                    sortList = sortList.Where(x => x.DiscountPercent >= 15 && x.DiscountPercent < 30).ToList();
                    break;

                case 4:
                    sortList = sortList.Where(x => x.DiscountPercent >= 30 && x.DiscountPercent < 70).ToList();
                    break;

                case 5:
                    sortList = sortList.Where(x => x.DiscountPercent >= 70 && x.DiscountPercent < 100).ToList();
                    break;

                default:
                    break;
            }

            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    break;

                case 1:
                    sortList = sortList.OrderBy(x => x.RealCost).ToList();
                    break;

                case 2:
                    sortList = sortList.OrderBy(x => -x.RealCost).ToList();
                    break;

                default:
                    break;
            }

            lvServices.ItemsSource = sortList;
        }

        private void Find(object sender, TextChangedEventArgs e)
        {
            SortList();
        }

        private void GoToAddRecordPage(object sender, RoutedEventArgs e)
        {
            if(lvServices.SelectedItem != null)
            {
                NavigationService.Navigate(new CreateRecordPage(App.Connection.Service.FirstOrDefault
                    (x => x.ID == ((ServiceInformation)lvServices.SelectedItem).ID)));
            }
            else
            {
                MessageBox.Show("Выберите услугу!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GoToRecordsPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RecordsPage());
        }
    }
}
