﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class Host
    {
        public int HostKey;
        public string PrivateName;
        public string FamilyName;
        public string PhoneNumber;
        public string MailAddress;
        public BankAccount HostBankAccount;
        public bool CollectionClearance;

        public override string ToString()
        {
            string str = "Host Key: " + HostKey +
                "\n Private Name: " + PrivateName +
                "\n Family Name: " + FamilyName +
                "\n Phone Number: " + PhoneNumber +
                "\n Mail Address: " + MailAddress +
                "\n Bank Account: " + HostBankAccount.ToString() +
                "\n Collection Clearance: " + CollectionClearance;
             return str;
        }
    }
}