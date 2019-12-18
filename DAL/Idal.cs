using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface Idal
    {
        void AddGuestRequest(GuestRequest guest);
        void UpdateGuestRequest(GuestRequest guest);

        void AddHostUnit(HostUnit host);
        void RemoveHostUnit(int id);
        void UpdateHostUnit(HostUnit host);

        void AddOrder(Order ord);
        void UpdateOrder(Order ord);

        List<Host> GetHostingUnitsList();
        List<GuestRequest> GetGuestsList();
        List<Order> GetOrdersList();
        List<BankAccount> ListBankBranches();
    }
}
