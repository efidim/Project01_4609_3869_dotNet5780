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

namespace PLWPF.Order
{
    /// <summary>
    /// Interaction logic for CreateOrderWindow.xaml
    /// </summary>
    public partial class CreateOrderWindow : Window
    {
       // BE.Order order;
        BE.HostingUnit host;
        BL.IBL bl;
        public CreateOrderWindow()
        {
            InitializeComponent();
            order = new BE.Order();
            host = new BE.HostingUnit();
            bl = BL.FactoryBl.getBl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                host = bl.GetHostingUnit(int.Parse(this.key.Text));
                IEnumerable<GuestRequest> temp4 = bl.RequestsByCondition(x => x.Area == host.Area
                && x.Type == host.Type && bl.IsItAvailaible(host, x.EntryDate, bl.DifferenceDays(x.ReleaseDate, x.EntryDate))
                && (x.Status)&&x.Adults<=host.Adults
                && x.Children<=host.Children /*&& x.Pool==host.Pool*/);
                DataContext = temp4;
             //   this.requests.ItemsSource = temp4;


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
