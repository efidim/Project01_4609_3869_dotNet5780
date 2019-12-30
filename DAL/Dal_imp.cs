using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    public class Dal_imp : IDAL
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
        #region Guest Request
        public void AddGuestRequest(GuestRequest guest)
        {
            guest.GuestRequestKey = Configuration.guestKey++;
            DataSource.GuestRequests.Add(guest);
        }
        public void UpdateGuestRequest(GuestRequest guest)
        {
            int index = DataSource.GuestRequests.FindIndex(g => g.GuestRequestKey == guest.GuestRequestKey);
            if (index == -1)
            {
                DataSource.GuestRequests.Add(guest);
                return;
            }
            DataSource.GuestRequests[index] = guest;
        }
        public GuestRequest GetRequest(int keyRequest)
        {
            int index = DataSource.GuestRequests.FindIndex(g => g.GuestRequestKey == keyRequest);
            if (index == -1)
                throw new Exception("The guest request does not exist");
            return DataSource.GuestRequests[index].Clone();
        }
        public List<GuestRequest> GetAllGuests()
        {
            if (DataSource.GuestRequests==null)
                throw new Exception("The GuestRequests list is empty");
            return DataSource.GuestRequests.Select(gu => (GuestRequest)gu.Clone()).ToList();
        }
        #endregion

        //************************************ Hosting unit *********************************************
        #region Hosting unit
        public void AddHostUnit(HostingUnit host)
        {
            host.HostingUnitKey = Configuration.unitKey++;
           /* if (DataSource.HostingUnits.Any(x => x.HostingUnitKey == host.HostingUnitKey))
                throw new Exception("The host unit exists");*/

            DataSource.HostingUnits.Add(host);
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
                DataSource.HostingUnits.Add(host);
            DataSource.HostingUnits[index] = host;
        }
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            int index = DataSource.HostingUnits.FindIndex(g => g.HostingUnitKey == hostingUnitkey);
            if (index == -1)
                throw new Exception("The host unit does not exist");
            
            return DataSource.HostingUnits[index].Clone();
        }


        public List<HostingUnit> GetAllHostingUnits()
        {
            if (DataSource.HostingUnits == null)
                throw new Exception("The HostingUnits list is empty");
            return DataSource.HostingUnits.Select(hu => (HostingUnit)hu.Clone()).ToList();
        }
        #endregion
        //************************************ Order *********************************************
        #region Order
        public void AddOrder(Order ord)
        {
            DataSource.Orders.Add(ord);
        }
        public void UpdateOrder(Order ord)
        {
            int index = DataSource.Orders.FindIndex(o => o.OrderKey == ord.OrderKey);
            if (index == -1)
                DataSource.Orders.Add(ord);

            DataSource.Orders[index] = ord;
        }
        public Order GetOrder(int orderKey)
        {
            int index = DataSource.Orders.FindIndex(o => o.OrderKey == orderKey);
            if (index == -1)
                throw new Exception("The order does not exist");
            return DataSource.Orders[index].Clone();
        }
        public DateTime GetEntryDate(int GuestRequestKey)
        {
            int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            return DataSource.GuestRequests[index].EntryDate;
        }
        public DateTime GetRelease(int GuestRequestKey)
        {
            int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            return DataSource.GuestRequests[index].ReleaseDate;
        }
        public List<Order> GetAllOrders()
        {
            if (DataSource.Orders == null)
                throw new Exception("The Orders list is empty");
            return DataSource.Orders.Select(Or => (Order)Or.Clone()).ToList();
        }
        #endregion
        //************************************ Host **************************************************
        #region Host
       public Host GetHost(int hostKey)
        {
           int index = DataSource.HostingUnits.FindIndex(h => h.Owner.HostKey == hostKey);
            if (index == -1)
                throw new Exception("The host does not exist");
            return DataSource.HostingUnits[index].Owner;
        }
       public void UpdateHost(Host host)
        {
          int index = DataSource.HostingUnits.FindIndex(h => h.Owner.HostKey == host.HostKey);
            if (index == -1)
                throw new Exception("The host does not exist");
            DataSource.HostingUnits[index].Owner =host;
        }

        #endregion
        //************************************ Get lists *********************************************

        public List<BankBranch> ListBankBranches()
        {
            List<BankBranch> TheFiveBanks = new List<BankBranch>()
        {
           new BankBranch() {BankNumber=1,BankName="MyBank",BranchNumber=11,BranchAddress= "MyBank@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=2,BankName="Mizrahi",BranchNumber=22,BranchAddress= "Mizrahi@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=3,BankName="Discont",BranchNumber=33,BranchAddress= "Discont@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=4,BankName="Pagi",BranchNumber=44,BranchAddress= "Pagi@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=5,BankName="Leumi",BranchNumber=55,BranchAddress= "Leumi@gmail.com",BranchCity="Jerusalem"}
        };
            return TheFiveBanks;
        }
         
    }
}