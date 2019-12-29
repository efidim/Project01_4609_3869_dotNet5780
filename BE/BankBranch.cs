using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class BankBranch
    {
        public int BankNumber;
        public string BankName;
        public int BranchNumber;
        public string BranchAddress;
        public string BranchCity;

        public override string ToString()
        {
            string str = "Bank Number: " + BankNumber +
                "\n Bank Name: " + BankName +
                "\n Branch Number: " + BranchNumber +
                "\n Branch Address: " + BranchAddress +
                "\n Branch City: " + BranchCity;
            return str;
        }
    }
}
