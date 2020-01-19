using System;
using System.Collections.Generic;
using System.Globalization;
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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
        BE.GuestRequest guest;
        BL.IBL bl;
        private System.Windows.Controls.Calendar MyCalendar;
        public GuestRequestWindow()
        {
            InitializeComponent();
            guest = new BE.GuestRequest();
            this.GuestRequestGrid.DataContext = guest;
            bl = BL.FactoryBl.getBl();
            this.DataContext = guest;

            this.areaComboBox.ItemsSource =Enum.GetValues(typeof(Enums.Area));
            this.Type.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
            this.Pool.ItemsSource = Enum.GetValues(typeof(Enums.Response));
            this.Jacuuzi.ItemsSource = Enum.GetValues(typeof(Enums.Response));
            this.Atraction.ItemsSource = Enum.GetValues(typeof(Enums.Response));

            MyCalendar = CreateCalendar();
            vbCalendar.Child = null;
            vbCalendar.Child = MyCalendar;                                 
        }

        private System.Windows.Controls.Calendar CreateCalendar()
        {
            System.Windows.Controls.Calendar MonthlyCalendar = new System.Windows.Controls.Calendar();
            MonthlyCalendar.Name = "MonthlyCalendar";
            MonthlyCalendar.DisplayMode = CalendarMode.Month;
            MonthlyCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            MonthlyCalendar.IsTodayHighlighted = true;
            return MonthlyCalendar;
        }
        private void addCurrentList(List<DateTime> tList)
        {
            guest.EntryDate = tList.First();
            guest.ReleaseDate = tList.Last();
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DateTime> myList = MyCalendar.SelectedDates.ToList();
                MyCalendar = CreateCalendar();
                vbCalendar.Child = null;
                vbCalendar.Child = MyCalendar;
                addCurrentList(myList);
                this.DataContext = guest;
                DataContext = guest;
              
                bl.AddGuestRequest(guest);
                guest = new GuestRequest();


            }
            
           catch (FormatException)
            {
                MessageBox.Show("check your input and try again");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }

    public class StringTointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "אפשרי":
                    return 0;
                case "לא_מעוניין":
                    return 1;
                case "הכרחי":
                    return 2;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

      
    }
}


