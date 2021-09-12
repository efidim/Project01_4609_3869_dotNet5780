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
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);

            unit = new BE.HostingUnit();
            unit.owner = new Host();
            unit.owner.bankBranchDetails = new BankBranch();
            this.DataContext = unit;
            bl = FactoryBL.getBL();

            this.areaComboBox.ItemsSource = Enum.GetValues(typeof(Enums.area));
            this.typeComboBox.ItemsSource = Enum.GetValues(typeof(Enums.hostingUnitType));
        }

        private void tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int value;
            try
            {
                if (tabs.SelectedIndex == 1)
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

                if (tabs.SelectedIndex == 2)
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
               
            }
            catch (Exception ex)
            {
                if (tabs.SelectedIndex == 1)
                {
                    tabs.SelectedIndex = 0;
                }
                if (tabs.SelectedIndex == 2)
                {
                    tabs.SelectedIndex = 1;
                }
                MessageBox.Show(" הקלט לא תקין--" + ex.Message);

                return;
            }
        }
        

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                int value;
                BankBranch temp = new BankBranch();
                try
                {
                    value = int.Parse(this.bankNumTextBox.Text);
                    value = int.Parse(this.branchNumTextBox.Text);
                    value = int.Parse(this.accountTextBox.Text);
                    temp = CheckBranch(int.Parse(this.bankNumTextBox.Text), int.Parse(this.branchNumTextBox.Text));
                    unit.owner.bankBranchDetails.bankName = temp.bankName;
                    unit.owner.bankBranchDetails.branchAddress = temp.branchAddress;
                    unit.owner.bankBranchDetails.branchCity = temp.branchCity;

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

        private BankBranch CheckBranch(int codeBank, int codeBranch)
        {
            BankBranch temp = bl.CheckBranch(codeBank, codeBranch);
            if (temp == null)
                throw new KeyNotFoundException("סניף הבנק שהוזן אינו קיים במערכת");
            return temp;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            new HostingUnitWindow().Show();
            this.Close();
        }
    }

}

