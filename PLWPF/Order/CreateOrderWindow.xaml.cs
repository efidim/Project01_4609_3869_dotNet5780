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
        // BE.Order order;
        BE.HostingUnit unit;
        BL.IBL bl;
        public CreateOrderWindow()
        {
            InitializeComponent();
            //    order = new BE.Order();
            unit = new BE.HostingUnit();
            bl = BL.FactoryBl.getBl();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                unit = bl.GetHostingUnit(int.Parse(this.key.Text));
                IEnumerable<GuestRequest> temp4 = bl.RequestsByCondition(x => x.Area == unit.Area
                && x.Type == unit.Type && bl.IsItAvailaible(unit, x.EntryDate, bl.DifferenceDays(x.ReleaseDate, x.EntryDate))
                && (x.Status) && x.Adults <= unit.Adults
                && x.Children <= unit.Children && (IntToBool(x.Pool)==unit.Pool||x.Pool==0)
                && (IntToBool(x.Jacuzzi) == unit.Jacuzzi|| x.Jacuzzi==0)
                && (IntToBool(x.ChildrenAttractions) == unit.ChildrenAttractions||x.ChildrenAttractions==0));
                DataContext = temp4;
                this.requests.ItemsSource = temp4;


            }
            catch (Exception)
            {
                throw ;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private bool IntToBool(int value)
        {
            switch (value.ToString().ToLower())
            {
                case "0"://אפשרי
                    return true;
                case "1"://לא מעוניין
                    return false;
                case "2"://הכרחי
                    return true;
            }
            return false;
        }
    }


}
