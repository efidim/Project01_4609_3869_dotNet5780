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
    /// Interaction logic for AppOwnerWindow.xaml
    /// </summary>
    public partial class AppOwnerWindow : Window
    {
        public AppOwnerWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private void unitsByAreaButton_Click(object sender, RoutedEventArgs e)
        {
            new UnitsByAreaWindow().Show();
            this.Close();
        }

        private void unitsByDayButton_Click(object sender, RoutedEventArgs e)
        {
            new UnitsByDayWindow().Show();
            this.Close();
        }

        private void numOfOrdersButton_Click(object sender, RoutedEventArgs e)
        {
            new OrdersByRequestWindow().Show();
            this.Close();
        }

        private void HostsByUnitsButton_Click(object sender, RoutedEventArgs e)
        {
            new HostByUnitsWindow().Show();
            this.Close();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
