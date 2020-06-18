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
using System.Windows.Shapes;
using BE;
using BL;


namespace PLWPF
{
    /// <summary>
    /// Interaction logic for UnitsByDayWindow.xaml
    /// </summary>
    public partial class UnitsByDayWindow : Window
    {
        IBL bl;
        private Calendar MyCalendar;

        public UnitsByDayWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            bl = FactoryBl.getBl();
            MyCalendar = CreateCalendar();
            chooseViewBox.Child = null;
            chooseViewBox.Child = MyCalendar;
        }

        private Calendar CreateCalendar()
        {
           Calendar MonthlyCalendar = new Calendar();
            MonthlyCalendar.Name = "MonthlyCalendar";
            MonthlyCalendar.DisplayMode = CalendarMode.Month;
            MonthlyCalendar.SelectionMode = CalendarSelectionMode.SingleRange;
            MonthlyCalendar.IsTodayHighlighted = true;
            return MonthlyCalendar;
        }

        private void chooseButton_Click(object sender, RoutedEventArgs e)
        {
           List<DateTime> selectedDays = MyCalendar.SelectedDates.ToList();
           List<HostingUnit> available = bl.AvailableUnits(selectedDays.First()
              , bl.DifferenceDays(selectedDays.First(), selectedDays.Last()));
           unitsListBox.ItemsSource = available;
        }

        /// <summary>
        /// A function that ensures that the first click will be updated on dates
        /// </summary>
        /// <param name="e">Mouse click event</param>
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new AppOwnerWindow().Show();
            this.Close();
        }

    }
}
