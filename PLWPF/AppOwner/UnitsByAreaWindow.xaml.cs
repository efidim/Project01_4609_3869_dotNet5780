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
    /// Interaction logic for UnitsByAreaWindow.xaml
    /// </summary>
    public partial class UnitsByAreaWindow : Window
    {
        IBL bl;
        List<HostingUnit> jerusalem;
        List<HostingUnit> north;
        List<HostingUnit> south;
        List<HostingUnit> center;

        public UnitsByAreaWindow()
        {
            InitializeComponent();
            bl = FactoryBl.getBl();           
            IEnumerable<IGrouping<string, HostingUnit>> unitsList = bl.UnitsByArea();
            
            jerusalem = new List<HostingUnit>();
            north = new List<HostingUnit>();
            south = new List<HostingUnit>();
            center = new List<HostingUnit>();

            foreach (IGrouping<string, HostingUnit> g in unitsList)
            {
                switch (g.Key)
                {
                    case "ירושלים":
                        foreach (var item in g)
                        {
                            jerusalem.Add(item);
                        }
                        break;

                    case "מרכז":
                        foreach (var item in g)
                        {
                            center.Add(item);
                        }
                        break;

                    case "צפון":
                        foreach (var item in g)
                        {
                            north.Add(item);
                        }
                        break;

                    case "דרום":
                        foreach (var item in g)
                        {
                            south.Add(item);
                        }
                        break;
                }
            }

            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Area));
        }

        private void areaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (areaComboBox.SelectedItem.ToString() == "ירושלים")
            {
                unitsListBox.ItemsSource = jerusalem;
            }

            if (areaComboBox.SelectedItem.ToString() == "מרכז")
            {
                unitsListBox.ItemsSource = center;
            }

            if (areaComboBox.SelectedItem.ToString() == "צפון")
            {
                unitsListBox.ItemsSource = north;
            }

            if (areaComboBox.SelectedItem.ToString() == "דרום")
            {
                unitsListBox.ItemsSource = south;
            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new AppOwnerWindow().Show();
            this.Close();
        }
    }
}
