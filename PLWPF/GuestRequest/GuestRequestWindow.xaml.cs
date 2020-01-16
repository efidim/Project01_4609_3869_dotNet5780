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
    /// Interaction logic for GuestRequestWindow.xaml
    /// </summary>
    public partial class GuestRequestWindow : Window
    {
        BE.GuestRequest guest;
        BL.IBL bl;
        private Calendar MyCalendar;
        public GuestRequestWindow()
        {
            InitializeComponent();
            guest = new BE.GuestRequest();
            this.GuestRequestGrid.DataContext = guest;
            bl = BL.FactoryBl.getBl();
            this.DataContext = guest;

            this.areaComboBox.ItemsSource =Enum.GetValues(typeof(Enums.Area));
            this.Type.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
            this.Pool.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
            this.Jacuuzi.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
            this.Atraction.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));

            MyCalendar = CreateCalendar();
            vbCalendar.Child = null;
            vbCalendar.Child = MyCalendar;                                 
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.GuestRequestGrid.DataContext = guest;
                guest.PrivateName =this.PrivateName.Text;
                guest.FamilyName = this.FamilyName.Text;
                guest.MailAddress= this.Mail.Text;

                guest.Area = this.areaComboBox.Text;
                guest.Type = this.Type.Text;

                guest.Adults = int.Parse(this.Adults.Text);
                guest.Children = int.Parse(this.Children.Text);

                guest.Pool = int.Parse(this.Pool.Text);
                guest.Jacuzzi = int.Parse(this.Jacuuzi.Text);
                guest.ChildrenAttractions = int.Parse(this.Atraction.Text);

                bl.AddGuestRequest(guest);
                guest = new BE.GuestRequest();

                this.PrivateName.ClearValue(TextBox.TextProperty);
                this.FamilyName.ClearValue(TextBox.TextProperty);
                this.Mail.ClearValue(TextBox.TextProperty);
                this.vbCalendar.ClearValue(TextBox.TextProperty);
                this.areaComboBox.ClearValue(TextBox.TextProperty);
                this.Type.ClearValue(TextBox.TextProperty);
                this.Adults.ClearValue(TextBox.TextProperty);
                this.Children.ClearValue(TextBox.TextProperty);
                this.Pool.ClearValue(TextBox.TextProperty);
                this.Jacuuzi.ClearValue(TextBox.TextProperty);
                this.Atraction.ClearValue(TextBox.TextProperty);

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
}
