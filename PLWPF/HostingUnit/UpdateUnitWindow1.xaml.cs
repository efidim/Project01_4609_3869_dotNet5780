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
    /// Interaction logic for UpdateUnitWindow1.xaml
    /// </summary>
    public partial class UpdateUnitWindow1 : Window
    {
        HostingUnit unit;
        IBL bl;

        public UpdateUnitWindow1()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            unit = new HostingUnit();
            bl = FactoryBL.getBL();
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsDigitsOnly(this.InputTextBox.Text))
                     unit = bl.GetHostingUnit(int.Parse(this.InputTextBox.Text));

                else
                    unit = bl.GetHostingUnitByName(this.InputTextBox.Text);


                new UpdateUnitWindow2(unit).Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }       
                        
        }

        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsDigitsOnly(this.InputTextBox.Text)) 
                {
                    unit = bl.GetHostingUnit(int.Parse(this.InputTextBox.Text));
                    bl.RemoveHostUnit(unit);
                }

                else
                {
                    unit = bl.GetHostingUnitByName(this.InputTextBox.Text);
                    bl.RemoveHostUnit(unit);
                }

                MessageBox.Show("The Hosting Unit has been successfully removed");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnitWindow().Show();
            this.Close();
        }
    }
}
