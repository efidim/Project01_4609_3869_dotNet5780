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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void guestRequestButton_Click(object sender, RoutedEventArgs e)
        {
            new GuestRequestWindow().Show();
            this.Close();
        }
        private void hostingUnitButton_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnitWindow().Show();
            this.Close();
        }
        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            new OrderWindow().Show();
            this.Close();
        }
        private void appOwner_Click(object sender, RoutedEventArgs e)
        {
            new AppOwnerWindow().Show();
            this.Close();
        }


    }
}
