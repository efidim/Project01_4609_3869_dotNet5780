using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    public class DataSource
    {

        public static List<GuestRequest> GuestRequests { get; set; } = new List<GuestRequest>()
        {
           new GuestRequest(){ GuestRequestKey=4589, PrivateName="yos",FamilyName="lev",MailAddress="yos@org.zehut.il",Status=true,RegistrationDate=new DateTime(2019,12,01),EntryDate=new DateTime(2019,12,02),
           ReleaseDate=new DateTime(2019,12,03),Area="Jerusalem",Adults=3,Children=5,Jacuzzi=1,Pool=1,Type="zimmer",ChildrenAttractions=0 }

        };
        public static List<HostingUnit> HostingUnits { get; set; } = new List<HostingUnit>()
        {
         new HostingUnit() {HostingUnitKey=469834,HostingUnitName="Tsimer",Owner=new Host(){HostKey=12,PrivateName="yo",FamilyName="le",PhoneNumber="054-1234567",MailAddress="yo@org.zehut.il"
         ,BankBranchDetails=new BankBranch(){BankNumber=1,BankName="MyBank",BranchNumber=11,BranchAddress= "MyBank@gmail.com",BranchCity="Jerusalem" },BankAccountNumber=111,CollectionClearance=true }
<<<<<<< HEAD
         ,Area="Jerusalem",Adults=3,Children=5,Jacuzzi=1,Pool=1,Type="zimmer",ChildrenAttractions=0}
=======
         ,Area="Jerusalem",subArea="Giloh",Adults=3,Children=5,Jacuzzi=true,Pool=true,Type="zimmer",ChildrenAttractions=false}
>>>>>>> HostingUnitWindow
        };
        public static List<Order> Orders { get; set; } = new List<Order>()
        {
           new Order{HostingUnitKey=469834, GuestRequestKey=4589,OrderKey=123,Status=1,CreateDate=new DateTime(2019, 12, 9), OrderDate=new DateTime(2019, 12, 12),CommissionPerDay=10}
        };
       
    }

}