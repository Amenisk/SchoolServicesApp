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
    /// Логика взаимодействия для AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        private Service _service;
        public AddServicePage(Service service)
        {
            InitializeComponent();
            _service = service;
            DataContext = _service;
            spAdditionalImages.Visibility = Visibility.Visible;
            UploadAdditionalsImages();
        }

        public AddServicePage() 
        {
            InitializeComponent();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
           
            if(tbName.Text != "" && tbCost.Text != "" && tbDuration.Text != "" && tbDiscount.Text != "" && tbImageSourse.Text != "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(!decimal.TryParse(tbCost.Text, out var num1))
            {
                MessageBox.Show("Неверный формат стоимости!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbDuration.Text, out var num2))
            {
                MessageBox.Show("Неверный формат длительности!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(tbDiscount.Text, out var num3))
            {
                MessageBox.Show("Неверный формат скидки!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_service == null)
            {
                _service = new Service()
                {
                    Title = tbName.Text,
                    Cost = Convert.ToDecimal(tbCost.Text),
                    DurationInSeconds = Convert.ToInt32(tbDuration.Text),
                    Discount = Convert.ToInt32(tbDiscount.Text),
                    MainImagePath = tbImageSourse.Text
                };

                App.Connection.Service.Add(_service);
                MessageBox.Show("Добавление прошло успешно!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);                  
            }
            else
            {
                MessageBox.Show("Редактирование прошло успешно!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            App.Connection.SaveChanges();
            NavigationService.Navigate(new ServicesListPage());
        }

        private void AddAdditionalImage(object sender, RoutedEventArgs e)
        {
            if (tbAddImageSource.Text != "")
            {
                var addImage = new ServicePhoto()
                { 
                    ServiceID = _service.ID,
                    PhotoPath = tbAddImageSource.Text,
                };
                App.Connection.ServicePhoto.Add(addImage);
                App.Connection.SaveChanges();
                MessageBox.Show("Добавление дополнительного фото прошло успешно!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                UploadAdditionalsImages();
            }
            else
            {
                MessageBox.Show("Сначала укажите  путь!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteAdditionalImage(object sender, RoutedEventArgs e)
        {
            if(lvAdditionalsImages.SelectedIndex != -1)
            {
                App.Connection.ServicePhoto.Remove((ServicePhoto)lvAdditionalsImages.SelectedItem);
                App.Connection.SaveChanges();
                MessageBox.Show("Удаление дополнительного фото прошло успешно!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                UploadAdditionalsImages();
            }
            else
            {
                MessageBox.Show("Сначала выберите фото для удаления!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadAdditionalsImages()
        {
            lvAdditionalsImages.ItemsSource = App.Connection.ServicePhoto.Where(x => x.ServiceID == _service.ID).ToList();
        }
        
    }
}
