using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using SchoolServicesApp.Classes;

namespace SchoolServicesApp.Classes
{
    public class ServiceInformation
    {
        public int ID { get; private set; }
        public string MainImagePath { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Duration { get; private set; }
        public string Cost { get; private set; }
        public string CostWithAction { get; private set; }
        public string Discount { get; private set; }    
        public double RealCost { get; private set; }
        public double DiscountPercent { get; private set; }
        public int DurationInSeconds { get; private set; }
        public string BackgroundColor { get; private set; }

        public string ButtonVisibility { get; private set; }

        public ServiceInformation(int id, string title, decimal cost, 
            int durationInSeconds, string description, double? discount, string mainImagePath, string Role)
        {
            ID = id;
            MainImagePath = mainImagePath;
            Title = title;
            Description = description;
            DurationInSeconds = durationInSeconds;
            RealCost = double.Parse(cost.ToString());
            DiscountPercent = (double) discount;
            ButtonVisibility = Role == "Администратор" ? "Visible" : "Hidden";

            Duration = $"{DurationInSeconds / 60} минут";
            if(DiscountPercent > 0)
            {
                CostWithAction = Math.Round((1 - DiscountPercent / 100) * RealCost, 2) + " ₽";
                Cost = Math.Round(RealCost, 2).ToString();
                Discount = $"* скидка {DiscountPercent} %";
            }
            else
            {
                CostWithAction = Math.Round(RealCost, 2).ToString() + " ₽";
                Cost = "";
            }

            if(discount > 0)
            {
                BackgroundColor = "LightGreen";
            }
            else
            {
                BackgroundColor= "White";
            }
        }
    }
}
