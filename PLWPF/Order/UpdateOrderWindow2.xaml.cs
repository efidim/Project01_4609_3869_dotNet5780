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
    /// Interaction logic for UpdateOrderWindow2.xaml
    /// </summary>
    public partial class UpdateOrderWindow2 : Window
    {
        BE.Order ord;
        IBL bl;
        public UpdateOrderWindow2(BE.Order order)
        {
            InitializeComponent();
            ord = order;
            this.DataContext = ord;
            bl = FactoryBl.getBl();

            this.ChangeS.ItemsSource = Enum.GetValues(typeof(Enums.OrderStatus));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateOrder(ord);
                MessageBox.Show("The Order has been successfully updated");
                new OrderWindow().Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new UpdateOrderWindow().Show();
            this.Close();
        }
    }
}
