using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;


namespace DAL
{
    public static class Cloning
    {
        public static GuestRequest Clone(this GuestRequest original)
            {
            GuestRequest target = new GuestRequest();
            target.guestRequestKey = original.guestRequestKey;
            target.privateName = original.privateName;
            target.familyName = original.familyName;
            target.mailAddress = original.mailAddress;
            target.status = original.status;
            target.registrationDate = original.registrationDate;
            target.entryDate = original.entryDate;
            target.releaseDate = original.releaseDate;
            target.area = original.area;
            target.type = original.type;
            target.adults = original.adults;
            target.children = original.children;
            target.pool = original.pool;
            target.jacuzzi = original.jacuzzi;
            target.childrenAttractions = original.childrenAttractions;
            
             return target;
        }
        public static HostingUnit Clone(this HostingUnit original)
        {
            HostingUnit target = new HostingUnit();
            target.hostingUnitKey = original.hostingUnitKey;
            target.owner = original.owner;
            target.hostingUnitName = original.hostingUnitName;
            for (int j = 0; j < 12; j++)
            {
                for (int i = 0; i < 31; i++)
                {
                    target.diary[j, i] = original.diary[j, i];
                }
            }
            target.area = original.area;
            target.type = original.type;
            target.adults = original.adults;
            target.children = original.children;
            target.pool = original.pool;
            target.jacuzzi = original.jacuzzi;
            target.childrenAttractions = original.childrenAttractions;

            return target;
        }
        public static Order Clone(this Order original)
        {
            Order target = new Order();
            target.hostingUnitKey = original.hostingUnitKey;
            target.guestRequestKey = original.guestRequestKey;
            target.orderKey = original.orderKey;
            target.status = original.status;
            target.createDate = original.createDate;
            target.orderDate = original.orderDate;
            target.commissionPerDay = original.commissionPerDay;

            return target;
        }
        public static BankBranch Clone(this BankBranch original)
        {
            BankBranch target = new BankBranch();
            target.bankNumber = original.bankNumber;
            target.bankName = original.bankName;
            target.branchNumber = original.branchNumber;
            target.branchAddress = original.branchAddress;
            target.branchCity = original.branchCity;          

            return target;
        }
        public static Host Clone(this Host original)
        {
            Host target = new Host();
            target.hostKey = original.hostKey;
            target.privateName = original.privateName;
            target.familyName = original.familyName;
            target.phoneNumber = original.phoneNumber;
            target.mailAddress = original.mailAddress;
            target.bankBranchDetails = original.bankBranchDetails;
            target.bankAccountNumber = original.bankAccountNumber;
            target.collectionClearance = original.collectionClearance;

            return target;
        }
    }
    
}
