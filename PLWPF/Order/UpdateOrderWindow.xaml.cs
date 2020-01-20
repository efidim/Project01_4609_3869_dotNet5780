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
    /// Interaction logic for UpdateOrderWindow.xaml
    /// </summary>
    public partial class UpdateOrderWindow : Window
    {
        IBL bl;
        BE.Order ord;
        public UpdateOrderWindow()
        {
            InitializeComponent();
            bl = FactoryBl.getBl();
            ord = new BE.Order();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
              ord = bl.GetOrder(int.Parse(keyTextBox.Text));
              new UpdateOrderWindow2(ord).Show();
              this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);            
            }
          
        }
    }
}
