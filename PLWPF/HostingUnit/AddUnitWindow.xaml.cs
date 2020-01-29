using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            unit = new BE.HostingUnit();
            unit.Owner = new Host();
            unit.Owner.BankBranchDetails = new BankBranch();
            this.DataContext = unit;
            bl = FactoryBl.getBl();

            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(Enums.Area));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(Enums.HostingUnitType));
        }

        private void TabItem_Click1(object sender, RoutedEventArgs e)
        {


            int value;
            try
            {
                value = int.Parse(this.adultsTextBox.Text);
                if (value <= 0)
                    throw new Exception(" נא להכניס ערך נכון במספר מבוגרים מקסימלי");
                value = int.Parse(this.childrenTextBox.Text);
                if (value < 0)
                    throw new Exception(" נא להכניס ערך נכון במספר ילדים מקסימלי");
                string str = this.nameTextBox.Text;
                CheckStr(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show(" הקלט לא תקין--" + ex.Message);
                return;
            }

        }
        private void TabItem_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string str1 = this.privateNameTextBox.Text;
                CheckStr(str1);
                string str2 = this.familyNameTextBox.Text;
                CheckStr(str2);
                string mail = this.mailTextBox.Text;
                CheckMail(mail);
                string Id = this.idTextBox.Text;
                CheckId(Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(" הקלט לא תקין--" + ex.Message);
                return;
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int value;
                try
                {
                    value = int.Parse(this.bankNumTextBox.Text);
                    value = int.Parse(this.branchNumTextBox.Text);
                    value = int.Parse(this.accountTextBox.Text);

                }

                catch (Exception ex)
                {
                    MessageBox.Show(" הקלט לא תקין--" + ex.Message);
                    return;
                }



                int key;
                key = bl.AddHostUnit(unit);
                MessageBox.Show("The Hosting Unit has been successfully added and the unit key code is: " + key);


                new HostingUnitWindow().Show();
                this.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void CheckStr(string str)
        {
            bool hasNUmber = str.Any(char.IsDigit);
            if (hasNUmber)
            {
                throw new KeyNotFoundException("  יש להכניס אותיות בלבד בשדות פרטי שמות ");
            }
        }
        private void CheckMail(string str)
        {
            if (!(Regex.IsMatch(str, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")))
                throw new KeyNotFoundException("המייל שהוזן אינו תקין");
        }
        private void CheckId(string str)
        {
            bool res = str.Any(char.IsLetter);
            if (res)
            {
                throw new KeyNotFoundException("  יש להכניס מספר בלבד בשדה תעודת זהות ");
            }
        }


    }

}
