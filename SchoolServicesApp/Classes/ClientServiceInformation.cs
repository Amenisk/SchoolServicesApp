using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SchoolServicesApp.Classes
{
    public class ClientServiceInformation
    {
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Patronymic { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public DateTime StartTime { get; private set; }
        public string Comment { get; private set; }
        public Brush ColorFont { get; private set; }
        public string RemainingTime { get; private set; }

        public ClientServiceInformation(string title, string firstName, string lastName, string patronymic, string email, string phone, DateTime startTime, string comment)
        {
            Title = title;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            Email = email;
            Phone = phone;
            StartTime = startTime;
            Comment = comment;

            var remTime = StartTime - DateTime.Now;
            if (remTime < new TimeSpan(1, 0, 0))
            {
                ColorFont = Brushes.Red;
            }
            else
            {
                ColorFont = Brushes.Black;
            }

            RemainingTime = $"осталось {remTime.Hours} часов {remTime.Minutes} минут";
        }
    }
}
