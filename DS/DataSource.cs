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
           new GuestRequest(){ GuestRequestKey=4589, PrivateName="yos",FamilyName="lev",MailAddress="yos@org.zehut.il",Status=true }

        };
        public static List<HostingUnit> HostingUnits { get; set; } = new List<HostingUnit>()
        { 
         new HostingUnit() {HostingUnitKey=469834,HostingUnitName="Tsimer",Diary=}
        };
        public static List<Order> Orders { get; set; } = new List<Order>()
        {
            new Order{HostingUnitKey=469834, GuestRequestKey=4589,OrderKey=123,Status=2,CreateDate=new DateTime(2019, 12, 9), OrderDate=new DateTime(2019, 12, 12)}
        };
  
    }

}