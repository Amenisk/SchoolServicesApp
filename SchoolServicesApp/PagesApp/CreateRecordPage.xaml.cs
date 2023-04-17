using SchoolServicesApp.ADOApp;
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
    /// Логика взаимодействия для CreateRecordPage.xaml
    /// </summary>
    public partial class CreateRecordPage : Page
    {
        private Service _service;
        private TimeSpan recordTime;
        public CreateRecordPage(Service service)
        {
            InitializeComponent();
            tbServiceTitle.Text = service.Title;
            _service = service;
            cmbClients.ItemsSource = App.Connection.Client.ToList();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if(cmbClients.SelectedItem != null && dpDate.SelectedDate.Value != null && tbTime.Text != "")
            {
                try
                {
                    recordTime = TimeSpan.Parse(tbTime.Text);
                }
                catch
                {
                    MessageBox.Show("Неверный формат времени!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

               

                if(dpDate.SelectedDate.Value.Add(recordTime) < DateTime.Now)
                {
                    MessageBox.Show("Дата не может быть выбрана в прошлом!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var clentService = new ClientService()
                {
                    ClientID = ((Client) cmbClients.SelectedItem).ID,
                    ServiceID = _service.ID,
                    StartTime = dpDate.SelectedDate.Value.Add(recordTime),
                    Comment = tbComment.Text,
                };
                App.Connection.ClientService.Add(clentService);
                App.Connection.SaveChanges();
                MessageBox.Show("Запись добавлена!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new ServicesListPage());

            }
            else
            {
                MessageBox.Show("Введите всю информацию!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ServicesListPage());
        }

        private void ChooseTime(object sender, RoutedEventArgs e)
        {
            if(dpDate.SelectedDate.Value != null)
            {
                try
                {
                    var dateTime = dpDate.SelectedDate.Value;
                    recordTime = TimeSpan.Parse(tbTime.Text);
                    dateTime = dateTime.Add(recordTime);
                    dateTime = dateTime.AddMinutes(_service.DurationInSeconds / 60);
                    tbFinalData.Text = dateTime.ToString("g");
                }
                catch
                {
                    MessageBox.Show("Неверный формат времени!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            else
            {
                MessageBox.Show("Сначала выфберите дату!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                          
        }
    }
}
