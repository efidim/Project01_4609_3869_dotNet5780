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
        public static List<HostingUnit> HostingUnits { get; set; } = new List<HostingUnit>();
        public static List<Order> Orders { get; set; } = new List<Order>();
        /*  public static List<GuestRequest> GetGuestRequests()
          {
              GuestRequest[] guestsArr = new GuestRequest[DataSource.GuestRequests.Count];
              DataSource.GuestRequests.CopyTo(guestsArr);
              return guestsArr.ToList();
          }נספח בעמוד 27 צריך לפתח את זה */
    }

}