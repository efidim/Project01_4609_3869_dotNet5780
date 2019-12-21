using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp : Idal
    {
        public void AddGuestRequest(GuestRequest guest)
        {
            DS.DataSource.GuestRequests.Add(guest.Clone());
        }

        public void AddHostUnit(HostingUnit host)
        {
            DS.DataSource.HostingUnits.Add(host.Clone());
        }

        public void AddOrder(Order ord)
        {
            DS.DataSource.Orders.Add(ord.Clone());
        }

        public List<GuestRequest> GetGuestsList()
        {
            return GuestRequests;
        }

        public List<Host> GetHostingUnitsList()
        {
            return HostingUnits;
        }

        public List<Order> GetOrdersList()
        {
            return Orders;
        }

        public List<BankAccount> ListBankBranches()
        {
            throw new NotImplementedException();
        }

        public void RemoveHostUnit(HostingUnit host)
        {
            int id = host.HostingUnitKey;
            int count = DS.DataSource.HostingUnits.RemoveAll(x=>x.HostingUnitKey == id);
            if (count == 0)
                throw new Exception("The host unit does not exixt");

        public void UpdateGuestRequest(GuestRequest guest)
        {
            int index=GuestRequests.FindIndex(g=>g.GuestRequestKey==guest.GuestRequestKey);
                if (index == -1)
                    throw new Exception("The guest request does not exixt");

                GuestRequests[index] = guest;
        }

        public void UpdateHostUnit(HostingUnit host)
        {
              int index = HostingUnits.FindIndex(h=> h.HostingUnitKey == host.HostingUnitKey);
                if (index == -1)
                    throw new Exception("The host unit does not exixt");

                HostingUnits[index] = host;
        }

        public void UpdateOrder(Order ord)
        {
                int index = Orders.FindIndex(o => o.OrderKey == ord.OrderKey);
                if (index == -1)
                    throw new Exception("The guest request does not exixt");

                HostingUnits[index] = host; 
        }
    }

}