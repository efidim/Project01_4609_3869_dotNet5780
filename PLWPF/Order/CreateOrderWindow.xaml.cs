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

namespace PLWPF.Order
{
    /// <summary>
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
        BE.Order order;
        BE.HostingUnit unit;
        BL.IBL bl;
        public CreateOrderWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            order = new BE.Order();
            unit = new BE.HostingUnit();
            this.DataContext = unit;
            bl = BL.FactoryBl.getBl();

            this.commission.Text =  "שח " + bl.GetFromConfig("COMMISSION") ;
        }
            
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            int hostKey = (int.Parse(this.key.Text));
            IEnumerable<HostingUnit> temp = bl.UnitsByHostKey(hostKey);
            hostUnits.ItemsSource = temp;        
        }

        private void hostsUnitsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            unit = hostUnits.SelectedItem as HostingUnit;
            IEnumerable<GuestRequest> temp4 = bl.RelevantRequest(unit);
            requests.ItemsSource = temp4;     

        }

         private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GuestRequest reqTemp = requests.SelectedItem as GuestRequest;
                order.HostingUnitKey = unit.hostingUnitKey;
                order.GuestRequestKey = reqTemp.GuestRequestKey;
                order.Status = 0;
                order.CreateDate = DateTime.Today;
                order.CommissionPerDay = int.Parse(bl.GetFromConfig("COMMISSION"));
                int orderKey = bl.AddOrder(order);
                MessageBox.Show("The Order has been successfully created and the key is: " + orderKey);
                new OrderWindow().Show();
                this.Close();

            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message); 
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show();
            this.Close();
        }

    }


}
