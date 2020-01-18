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
    /// Interaction logic for AddUnitWindow.xaml
    /// </summary>
    public partial class AddUnitWindow : Window
    {
        HostingUnit unit;
        IBL bl;
        public AddUnitWindow()
        {
            InitializeComponent();
            unit = new HostingUnit();
            unit.Owner = new Host();
            unit.Owner.BankBranchDetails = new BankBranch();
            this.DataContext = unit;
            bl = FactoryBl.getBl();

            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Area));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int key;
                key = bl.AddHostUnit(unit);
                MessageBox.Show("The Hosting Unit has been successfully added and the unit key code is: " + key);

                unit = new HostingUnit();
                this.DataContext = unit;
            }
            //catch (FormatException)
            //{
            //    MessageBox.Show("check your input and try again");
            //} 
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
