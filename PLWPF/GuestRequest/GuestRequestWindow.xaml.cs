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
            this.poolComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Response));
            this.jacuzziComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Response));
            this.attractionComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Response));

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


        private void sentDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<DateTime> myList = MyCalendar.SelectedDates.ToList();
                MyCalendar = CreateCalendar();
                vbCalendar.Child = null;
                vbCalendar.Child = MyCalendar;
                addCurrentList(myList);

                if (poolComboBox.SelectedItem.ToString() == "אפשרי")
                    guest.Pool = 0;
                else if (poolComboBox.SelectedItem.ToString() == "לא_מעוניין")
                    guest.Pool = 1;
                else if (poolComboBox.SelectedItem.ToString() == "הכרחי")
                    guest.Pool = 2;

                if (jacuzziComboBox.SelectedItem.ToString() == "אפשרי")
                    guest.Jacuzzi = 0;
                else if (jacuzziComboBox.SelectedItem.ToString() == "לא_מעוניין")
                    guest.Jacuzzi = 1;
                else if (jacuzziComboBox.SelectedItem.ToString() == "הכרחי")
                    guest.Jacuzzi = 2;

                if (attractionComboBox.SelectedItem.ToString() == "אפשרי")
                    guest.ChildrenAttractions = 0;
                else if (attractionComboBox.SelectedItem.ToString() == "לא_מעוניין")
                    guest.ChildrenAttractions = 1;
                else if (attractionComboBox.SelectedItem.ToString() == "הכרחי")
                    guest.ChildrenAttractions = 2;

                guest.RegistrationDate = DateTime.Now;

                int value;
                try
                {
                    value = int.Parse(this.Adults.Text);
                    if (value <= 0)
                        throw new Exception();
                    value = int.Parse(this.Children.Text);
                    if (value <= 0)
                        throw new Exception();
                }
                catch
                {
                    MessageBox.Show(" הקלט לא תקין" + " נא הזן פרטים נכונים");
                    return;
                }


                bl.AddGuestRequest(guest);
                MessageBox.Show("The Guest Request has been successfully added.");

                new MainWindow().Show();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// A function that ensures that the first click will be updated on dates
        /// </summary>
        /// <param name="e">Mouse click event</param>
        protected override void OnPreviewMouseUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseUp(e);
            if (Mouse.Captured is System.Windows.Controls.Calendar || Mouse.Captured is System.Windows.Controls.Primitives.CalendarItem)
            {
                Mouse.Capture(null);
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }

    //public class StringTointConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    //    {
    //        switch (value.ToString())
    //        {
    //            case "אפשרי":
    //                return 0;
    //            case "לא_מעוניין":
    //                return 1;
    //            case "הכרחי":
    //                return 2;
    //        }
    //        return 0;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        if (value is int)
    //        {
    //            if ((int)value == 0)
    //                return "אפשרי";
    //            else if ((int)value == 1)
    //                return "לא_מעוניין";
    //            else if ((int)value == 2)
    //                return "הכרחי";
    //        }
    //        return "אפשרי";
    //    }
    //}


}


