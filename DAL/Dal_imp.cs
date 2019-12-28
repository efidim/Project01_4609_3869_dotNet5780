using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DAL
{
    class Dal_imp : IDAL
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
                throw new Exception("The guest request does not exist");

            DataSource.GuestRequests[index] = guest;
        }
        public GuestRequest GetRequest(int keyRequest)
        {
            /*List<GuestRequest> temp = DataSource.GuestRequests;
            var result = from item in temp
                         where item.GuestRequestKey == keyRequest
                         select item;*/
           int index = DataSource.GuestRequests.FindIndex(g => g.GuestRequestKey ==keyRequest);
            return DataSource.GuestRequests[index].Clone();
        }
        public List<GuestRequest> GetGuestsList()
        {
            return DataSource.GuestRequests.Select(gu => (GuestRequest)gu.Clone()).ToList();
        }
        public List<GuestRequest> GetHostingUnits(Func<GuestRequest, bool> predicate = null)
        {
            return DataSource.GuestRequests.Where(predicate).Select(hu => (GuestRequest)hu.Clone()).ToList();
        }
        #endregion

        //************************************ Hosting unit *********************************************
        #region Hosting unit
        public void AddHostUnit(HostingUnit host)
        {
            host.HostingUnitKey = Configuration.unitKey++;
            if (DataSource.HostingUnits.Any(x => x.HostingUnitKey == host.HostingUnitKey))
                throw new Exception("The host unit exists");

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
                throw new Exception("The host unit does not exist");

            DataSource.HostingUnits[index] = host;
        }
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            int index = DataSource.HostingUnits.FindIndex(o => o.HostingUnitKey == hostingUnitkey);
            return DataSource.HostingUnits[index].Clone();
        }
        public bool[,] GetDiary(int HostingUnitKey)
        {
            int index = DataSource.HostingUnits.FindIndex(o => o.HostingUnitKey == HostingUnitKey);
            return DataSource.HostingUnits[index].Diary;
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            return DataSource.HostingUnits.Select(hu => (HostingUnit)hu.Clone()).ToList();
        }
        public List<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicate = null)
        {
            return DataSource.HostingUnits.Where(predicate).Select(hu => (HostingUnit)hu.Clone()).ToList();
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
                throw new Exception("The order does not exist");

            DataSource.Orders[index] = ord;
        }
        public Order GetOrder(int orderKey)
        {
            int index = DataSource.Orders.FindIndex(o => o.OrderKey == orderKey);
            return DataSource.Orders[index].Clone();
        }
        public DateTime GetRegistration(int GuestRequestKey)
        {
            int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            return DataSource.GuestRequests[index].RegistrationDate;
        }
        DateTime GetRelease(int GuestRequestKey)
        {
            int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            return DataSource.GuestRequests[index].ReleaseDate;
        }
        public List<Order> GetOrdersList()
        {
            return DataSource.Orders.Select(Or => (Order)Or.Clone()).ToList();
        }
        #endregion
        //************************************ Host **************************************************
        #region Host
        Host GetHost(int hostKey)
        {
           int index = DataSource.HostingUnits.FindIndex(h => h.Owner.HostKey == hostKey);
            return DataSource.HostingUnits[index].Owner;
        }
        void UpdateHost(Host host)
        {

        }

        #endregion
        //************************************ Get lists *********************************************

        public List<BankAccount> ListBankBranches = new List<BankAccount>()
        {
           new BankAccount() {BankNumber=1,BankName="MyBank",BranchNumber=11,BranchAddress= "MyBank@gmail.com",BranchCity="Jerusalem",BankAccountNumber=111},
           new BankAccount() {BankNumber=2,BankName="Mizrahi",BranchNumber=22,BranchAddress= "Mizrahi@gmail.com",BranchCity="Jerusalem",BankAccountNumber=222},
           new BankAccount() {BankNumber=3,BankName="Discont",BranchNumber=33,BranchAddress= "Discont@gmail.com",BranchCity="Jerusalem",BankAccountNumber=333},
           new BankAccount() {BankNumber=4,BankName="Pagi",BranchNumber=44,BranchAddress= "Pagi@gmail.com",BranchCity="Jerusalem",BankAccountNumber=444},
           new BankAccount() {BankNumber=5,BankName="Leumi",BranchNumber=55,BranchAddress= "Leumi@gmail.com",BranchCity="Jerusalem",BankAccountNumber=555}
        };

   
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