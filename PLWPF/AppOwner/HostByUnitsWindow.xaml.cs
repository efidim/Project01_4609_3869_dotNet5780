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
    /// Interaction logic for HostByUnitsWindow.xaml
    /// </summary>
    public partial class HostByUnitsWindow : Window
    {
        IBL bl;
        List<Host> one;
        List<Host> two;
        List<Host> three;
        List<Host> four;
        List<Host> five;

        public HostByUnitsWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            bl = FactoryBl.getBl();            

            IEnumerable<IGrouping<int, int>> hostByUnits = bl.HostsByUnits();

            one = new List<Host>();
            two = new List<Host>();
            three = new List<Host>();
            four = new List<Host>();
            five = new List<Host>();

            foreach (IGrouping<int, int> g in hostByUnits)
            {
                switch (g.Key)
                {
                    case 1:
                        foreach (var item in g)
                        {
                            one.Add(bl.GetHost(item));
                        }
                        break;

                    case 2:
                        foreach (var item in g)
                        {
                            two.Add(bl.GetHost(item));
                        }
                        break;

                    case 3:
                        foreach (var item in g)
                        {
                            three.Add(bl.GetHost(item));
                        }
                        break;

                    case 4:
                        foreach (var item in g)
                        {
                            four.Add(bl.GetHost(item));
                        }
                        break;

                    case 5:
                        foreach (var item in g)
                        {
                            five.Add(bl.GetHost(item));
                        }
                        break;
                }
            }
        }

        private void numComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (numComboBox.SelectedIndex == 0)
            {
                hostsListBox.ItemsSource = one;
            }

            if (numComboBox.SelectedIndex == 1)
            {
               hostsListBox.ItemsSource = two;
            }

            if (numComboBox.SelectedIndex == 2)
            {
                hostsListBox.ItemsSource = three;
            }

            if (numComboBox.SelectedIndex == 3)
            {
               hostsListBox.ItemsSource = four;
            }

            if (numComboBox.SelectedIndex == 4)
            {
                hostsListBox.ItemsSource = five;
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new AppOwnerWindow().Show();
            this.Close();
        }
    }
}
