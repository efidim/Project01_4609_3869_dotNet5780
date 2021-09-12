using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public int hostKey{ get; set; }
        public string privateName{ get; set; }
        public string familyName{ get; set; }
        public string phoneNumber{ get; set; }
        public string mailAddress{ get; set; }
        public BankBranch bankBranchDetails { get; set; }
        public int bankAccountNumber{ get; set; }
        public bool collectionClearance{ get; set; }

        public override string ToString()
        {
            string str = "Host Key: " + hostKey +
                "\n Private Name: " + privateName +
                "\n Family Name: " + familyName +
                "\n Phone Number: " + phoneNumber +
                "\n Mail Address: " + mailAddress +
                "\n Bank Account: " + bankBranchDetails.ToString() +
                "\n Collection Clearance: " + collectionClearance;
             return str;
        }
    }
}
