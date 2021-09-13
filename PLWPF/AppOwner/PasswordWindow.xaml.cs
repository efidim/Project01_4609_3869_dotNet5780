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
    /// Interaction logic for PasswordWindow.xaml
    /// </summary>
    public partial class PasswordWindow : Window
    {
        IBL bl;
        public PasswordWindow()
        {
            InitializeComponent();
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            bl = FactoryBL.getBL();
        }

        private void passwordButton_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == bl.GetFromConfig("OwnerUser") && Pass.Password.ToString() == bl.GetFromConfig("OwnerPassword"))
            {
                new AppOwnerWindow().Show();
                this.Close();                
            }
            else
                MessageBox.Show("שם משתמש או סיסמא שהוזנו שגויים");
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
