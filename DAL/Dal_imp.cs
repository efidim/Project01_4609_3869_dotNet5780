using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp:IDAL
    {
        // *********************************** Singleton ****************************************
        protected Dal_imp() { }

        protected static Dal_imp instance = null;

        public static Dal_imp GetInstance()
        {
            if (instance == null)
                instance = new Dal_imp();
            return instance;
        }

        //************************************ Guest Request *********************************************
        public void AddGuestRequest(GuestRequest guest)
        {
            if (DataSource.GuestRequests.Any(x => x.GuestRequestKey == guest.GuestRequestKey))
                throw new Exception("The guest request exists");
            DataSource.GuestRequests.Add(guest.Clone());
        }
        public void UpdateGuestRequest(GuestRequest guest)
        {
            int index =  DataSource.GuestRequests.FindIndex(g => g.GuestRequestKey == guest.GuestRequestKey);
            if (index == -1)
                throw new Exception("The guest request does not exist");

            DataSource.GuestRequests[index] = guest.Clone();
        }

        //************************************ Hosting unit *********************************************
        public void AddHostUnit(HostingUnit host)
        {
            if (DataSource.HostingUnits.Any(x => x.HostingUnitKey == host.HostingUnitKey))
                throw new Exception("The host unit exists");

            DataSource.HostingUnits.Add(host.Clone());
        }

        public void RemoveHostUnit(HostingUnit host)
        {
            int id = host.HostingUnitKey;
            int count = DataSource.HostingUnits.RemoveAll(x => x.HostingUnitKey == id);
            if (count == 0)
                throw new Exception("The host unit does not exist");
        }

        public void UpdateHostUnit(HostingUnit host)
        {
            int index = DataSource.HostingUnits.FindIndex(h => h.HostingUnitKey == host.HostingUnitKey);
            if (index == -1)
                throw new Exception("The host unit does not exist");

            DataSource.HostingUnits[index] = host.Clone();
        }
        //************************************ Order *********************************************
        public void AddOrder(Order ord)
        {
            DataSource.Orders.Add(ord.Clone());
        }
        public void UpdateOrder(Order ord)
        {
            int index = DataSource.Orders.FindIndex(o => o.OrderKey == ord.OrderKey);
            if (index == -1)
                throw new Exception("The order does not exist");

            DataSource.Orders[index] = ord.Clone();
        }

        //************************************ Host **************************************************
        
        //************************************ Get lists *********************************************
        public List<GuestRequest> GetGuestsList()
        {
            return  DataSource.GuestRequests;
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            return  DataSource.HostingUnits;
        }

        public List<Order> GetOrdersList()
        {
            return  DataSource.Orders;
        }

        public List<BankAccount> ListBankBranches()
        {
            throw new NotImplementedException();
        }

   
         public IEnumerable<GuestRequest> GetAllGuests(Func<GuestRequest, bool> predicate = null)
            {
            throw new NotImplementedException();
            }

            public IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null)
            {
            throw new NotImplementedException();
            }

           public IEnumerable<HostingUnit> GetAllHostingUnits(Func<HostingUnit, bool> predicate = null)
           {
            throw new NotImplementedException();
           }
    }
}