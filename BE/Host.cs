using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int HostKey{ get; set; }
        public string PrivateName{ get; set; }
        public string FamilyName{ get; set; }
        public string PhoneNumber{ get; set; }
        public string MailAddress{ get; set; }
        public BankBranch BankBranchDetails { get; set; }
        public int BankAccountNumber{ get; set; }
        public bool CollectionClearance{ get; set; }

        public override string ToString()
        {
            string str = "Host Key: " + HostKey +
                "\n Private Name: " + PrivateName +
                "\n Family Name: " + FamilyName +
                "\n Phone Number: " + PhoneNumber +
                "\n Mail Address: " + MailAddress +
                "\n Bank Account: " + BankBranchDetails.ToString() +
                "\n Collection Clearance: " + CollectionClearance;
             return str;
        }
    }
}
