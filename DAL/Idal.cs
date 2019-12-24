using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        void AddGuestRequest(GuestRequest guest);
        void UpdateGuestRequest(GuestRequest guest);
        IEnumerable<GuestRequest> GetAllGuests(Func<GuestRequest, bool> predicate = null);

        void AddHostUnit(HostingUnit host);
        void RemoveHostUnit(HostingUnit host);
        void UpdateHostUnit(HostingUnit host);
        IEnumerable<HostingUnit> GetAllHostingUnits(Func<HostingUnit, bool> predicate = null);

        void AddOrder(Order ord);
        void UpdateOrder(Order ord);
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);

        List<HostingUnit> GetHostingUnitsList();
        List<GuestRequest> GetGuestsList();
        List<Order> GetOrdersList();
        List<BankAccount> ListBankBranches();

    }
}
