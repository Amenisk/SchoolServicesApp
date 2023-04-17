using SchoolServicesApp.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace SchoolServicesApp.PagesApp
{
    /// <summary>
    /// Логика взаимодействия для RecordsPage.xaml
    /// </summary>  
    public partial class RecordsPage : Page
    {
        private DispatcherTimer timer;

        public RecordsPage()
        {
            InitializeComponent();
            UpdateRecords();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 30);
            timer.Tick += TimerTick;
            timer.Start();          
        }

        private void UpdateRecords()
        {
            List<ClientServiceInformation> list = new List<ClientServiceInformation>();

            foreach (var s in App.Connection.ClientService.ToList())
            {
                list.Add(new ClientServiceInformation(s.Service.Title, s.Client.FirstName, s.Client.LastName,
                    s.Client.Patronymic, s.Client.Email, s.Client.Phone, s.StartTime, s.Comment));
            }
            lvRecords.ItemsSource = list.Where(x => x.StartTime >= DateTime.Now && x.StartTime < DateTime.Now.AddDays(1)).OrderBy(x => x.StartTime);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            UpdateRecords();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timer = null;
        }
    }
}
