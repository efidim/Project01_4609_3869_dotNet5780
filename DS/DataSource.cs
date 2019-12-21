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
      public static List<GuestRequest> GuestRequests { get; set; } = new List<GuestRequest>();
      public static  List<HostingUnit> HostingUnits = new List<HostingUnit>();
      public static List<Order> Orders;
   }
}
