using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int bankNumber { get; set; }
        public string bankName { get; set; }
        public int branchNumber { get; set; }
        public string branchAddress { get; set; }
        public string branchCity { get; set; }

        public override string ToString()
        {
            string str = "Bank Number: " + bankNumber +
                "\n Bank Name: " + bankName +
                "\n Branch Number: " + branchNumber +
                "\n Branch Address: " + branchAddress +
                "\n Branch City: " + branchCity;
            return str;
        }
    }
}
