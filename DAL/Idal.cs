using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface Idal
    {
        void AddGuestRequest();
        void UpdateGuestRequest();

        void AddHostUnit();
        void RenoveHostUnit();
        void UpdateHostUnit();

        void AddOrder();
        void UpdateOrder();

        List<Host> GetHostingUnitsList();
        List<string> GetGuestsList();
        List<DateTime> GetOrdersList();
        List<int> ListBankBranches();
    }
}
