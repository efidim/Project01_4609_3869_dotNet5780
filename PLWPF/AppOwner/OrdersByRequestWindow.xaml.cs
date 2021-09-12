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
    /// Interaction logic for OrdersByRequestWindow.xaml
    /// </summary>
    public partial class OrdersByRequestWindow : Window
    {
        IBL bl;
        GuestRequest req;
        public OrdersByRequestWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            bl = FactoryBL.getBL();
            req = new GuestRequest();          
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new AppOwnerWindow().Show();
            this.Close();

        }

        private void displayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                req = bl.GetRequest(int.Parse(KeyTextBox.Text));
                displayTextBox.Text = bl.OrdersByRequest(req).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
           
        }
    }
}
