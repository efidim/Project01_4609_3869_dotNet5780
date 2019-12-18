using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Dal_imp :Idal
    {
        public void AddGuestRequest(GuestRequest guest)
        {
            DataSource.guests.Add(guest.Clone());
        }
        public void UpdateGuestRequest(GuestRequest guest)
        {
            if (guest.id)
            {

            }
            throw new NotImplementedException();
        }

        public void AddHostUnit(HostUnit host)
        {
            DataSource.orders.Add(guest.Clone());
        }

        public void AddOrder(Order ord)
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetGuestsList()
        {
            throw new NotImplementedException();
        }

        public List<Host> GetHostingUnitsList()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetOrdersList()
        {
            throw new NotImplementedException();
        }

        public List<int> ListBankBranches()
        {
            throw new NotImplementedException();
        }

        public void RemoveHostUnit(HostUnit host)
        {
            throw new NotImplementedException();
        }

     

        public void UpdateHostUnit(HostUnit host)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order ord)
        {
            throw new NotImplementedException();
        }
    }
}
