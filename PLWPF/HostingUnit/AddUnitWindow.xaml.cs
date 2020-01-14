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
            bl = FactoryBl.getBl();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                unit.HostingUnitName = this.nameTextBox.Text;
                unit.Area = this.areaComboBox.Text;
                unit.Adults = int.Parse(this.adultsTextBox.Text);
                unit.Children = int.Parse(this.childrenTextBox.Text);
                unit.Type = this.typeComboBox.Text;
                unit.Pool = (bool)this.poolCheckBox.IsChecked;
                unit.Jacuzzi = (bool)this.jacuzziCheckBox.IsChecked;
                unit.ChildrenAttractions = (bool)this.attractionsCheckBox.IsChecked;

                unit.Owner.PrivateName = this.privateNameTextBox.Text;
                unit.Owner.FamilyName = this.familyNameTextBox.Text;
                unit.Owner.HostKey = int.Parse(this.idTextBox.Text);
                unit.Owner.PhoneNumber = this.phoneTextBox.Text;
                unit.Owner.MailAddress = this.mailTextBox.Text;

                unit.Owner.BankBranchDetails.BankName = this.bankNameTextBox.Text;
                unit.Owner.BankBranchDetails.BankNumber = int.Parse(this.bankNumTextBox.Text);
                unit.Owner.BankBranchDetails.BranchNumber = int.Parse(this.branchNumTextBox.Text);
                unit.Owner.BankBranchDetails.BranchCity = this.citylTextBox.Text;
                unit.Owner.BankBranchDetails.BranchAddress = this.addressBranchTextBox.Text;
                unit.Owner.BankAccountNumber = int.Parse(this.accountTextBox.Text);
                unit.Owner.CollectionClearance = (bool)this.collectionCheckBox.IsChecked;

                bl.AddHostUnit(unit);
                MessageBox.Show(":היחידה נוספה בהצלחה. קוד הזיהוי הוא");

                unit = new HostingUnit();

                this.nameTextBox.Text = "" ;
                this.areaComboBox.Text = "";
                this.adultsTextBox.Text = "";
                this.childrenTextBox.Text = "";
                this.typeComboBox.Text = "";
                this.poolCheckBox.IsChecked = false;
                this.jacuzziCheckBox.IsChecked = false;
                this.attractionsCheckBox.IsChecked = false;

                this.privateNameTextBox.Text = "";
                this.familyNameTextBox.Text = "";
                this.idTextBox.Text = "";
                this.phoneTextBox.Text = "";
                this.mailTextBox.Text = "";

                this.bankNameTextBox.Text = "";
                this.bankNumTextBox.Text = "";
                this.branchNumTextBox.Text = "";
                this.citylTextBox.Text = "";
                this.addressBranchTextBox.Text = "";
                this.accountTextBox.Text = "";
                this.collectionCheckBox.IsChecked = false;
            }
            catch (FormatException)
            {
                MessageBox.Show("check your input and try again");
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
