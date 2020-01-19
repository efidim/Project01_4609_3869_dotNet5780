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
    /// Interaction logic for UpdateUnitWindow2.xaml
    /// </summary>
    public partial class UpdateUnitWindow2 : Window
    {
        HostingUnit unitToUpdate;
        IBL bl;
        public UpdateUnitWindow2(HostingUnit unit)
        {
            InitializeComponent();
            unitToUpdate = unit;
            this.DataContext = unit;
            bl = FactoryBl.getBl();


            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Area));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateHostUnit(unitToUpdate);
                   MessageBox.Show("The Hosting Unit has been successfully updated");
                   new HostingUnitWindow().Show();
                   this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }

}
