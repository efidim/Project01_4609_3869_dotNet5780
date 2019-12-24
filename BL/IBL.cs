using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public interface IBL
    {
        void AddGuestRequest(GuestRequest guest);
        void UpdateGuestRequest(GuestRequest guest);
        IEnumerable<GuestRequest> GetAllGuests(Func<GuestRequest, bool> predicate = null);

        void AddHostUnit(HostingUnit host);
        void RemoveHostUnit(HostingUnit host);
        void UpdateHostUnit(HostingUnit host);
        IEnumerable<HostingUnit> GetAllHostingUnits(Func<HostingUnit, bool> predicate = null);

        bool CheckOrder(Order ord);
        void AddOrder(Order ord);
        Host GetHost(int hostingUnitkey);
        Order GetOrder(int orderKey);
        HostingUnit GetHostingUnit(int hostingUnitkey);
        void SetDiary(Order ord);
        void DisactivateRequest(int requestKey);
        void UpdateOtherOrders(int hostKey, int orderKey);
        void UpdateOrder(Order ord);
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);

        List<HostingUnit> GetHostingUnitsList();
        List<GuestRequest> GetGuestsList();
        List<Order> GetOrdersList();
        List<BankAccount> ListBankBranches();
    }
}
