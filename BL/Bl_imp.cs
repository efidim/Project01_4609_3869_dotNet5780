using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    class Bl_imp : IBL
    {
        IDAL dal = FactoryDal.getdal();

        //************************************ Guest Request *********************************************
        public void AddGuestRequest(GuestRequest guest)
        {
            if (guest.ReleaseDate <= guest.RegistrationDate)
                throw new Exception("The Registration Date must be at least one day before the Release Date");
            dal.AddGuestRequest(guest.Clone());            
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            dal.UpdateGuestRequest(guest.Clone());
        }

        //************************************ Hosting unit *********************************************
        public void AddHostUnit(HostingUnit host)
        {
            dal.AddHostUnit(host.Clone());
        }

        public void RemoveHostUnit(HostingUnit host)
        {
            dal.RemoveHostUnit(host);
        }

        public void UpdateHostUnit(HostingUnit host)
        {
            dal.UpdateHostUnit(host.Clone());
        }

        //************************************ Order *********************************************
        public bool CheckOrder(Order ord) 
        {
            bool[,] HostingUnitDiary = dal.GetDiary(ord.HostingUnitKey);
            DateTime current = dal.GetRegistration(ord.GuestRequestKey);
            DateTime end = dal.GetRelease(ord.GuestRequestKey);
            for (; current < end.AddDays(1); current.AddDays(1))
            {
                if (HostingUnitDiary[current.Month, current.Day])
                    throw new Exception("The requested dates are not available");
            }
        }
        public void AddOrder(Order ord)
        {
            CheckOrder(ord);
            dal.AddOrder(ord.Clone());
        }

        public Host GetHost(int hostingUnitkey)
        {
            return dal.GetHost(hostingUnitkey);
        }

        public Order GetOrder(int orderKey)
        {
            return dal.GetOrder(orderKey);
        }

        public HostingUnit GetHostingUnit(int hostingUnitkey) 
        {
            return dal.GetHostingUnit(hostingUnitkey);
        }

        public GuestRequest GetRequest(int keyRequest)
        {
            return dal.GetRequest(keyRequest);
        }

        /// <summary>
        /// Updates Hosting Unit Diary with dates order
        /// </summary>
        /// <param name="ord"> update order </param>
        public void SetDiary(Order ord)
        { 
            HostingUnit temp = GetHostingUnit(ord.HostingUnitKey);
            DateTime current = dal.GetRegistration(ord.GuestRequestKey);
            DateTime end = dal.GetRelease(ord.GuestRequestKey);

            for(; current < end.AddDays(1); current.AddDays(1))
            {
                temp.Diary[current.Month, current.Day] = true;
            }

            UpdateHostUnit(temp);
        }

        /// <summary>
        /// Updates Status Request to Disactive
        /// </summary>
        /// <param name="requestKey"> Guest Request key </param>
        public void DisactivateRequest(int requestKey)
        {
            GuestRequest temp = GetRequest(requestKey);
            temp.Status = false;
            UpdateGuestRequest(temp);
        }

        /// <summary>
        /// Updates other host orders after Closing Order
        /// </summary>
        /// <param name="hostKey"></param>
        /// <param name="orderKey"> Closed Order Key</param>
        public void UpdateOtherOrders(int hostKey, int orderKey)
        {
            IEnumerable<Order> temp1 = GetAllOrders();
            IEnumerable<Order> temp2 = from item in temp1
                                       where GetHost(item.HostingUnitKey).HostKey == hostKey 
                                       && item.OrderKey != orderKey 
                                       select item;

            foreach (var item in temp2)
            {
                item.Status = 2;
                UpdateOrder(item.Clone());
            }
        }
        public void UpdateOrder(Order ord)
        {
            if (ord.Status == 1 && !GetHost(ord.HostingUnitKey).CollectionClearance)
                throw new Exception("Cannot sent order to the Guest without authorization" +
                    "for Collection Clearance");
            if (GetOrder(ord.OrderKey).Status == 2 || GetOrder(ord.OrderKey).Status == 3)
                throw new Exception("Cannot update order after closing"); 

            dal.UpdateOrder(ord);
            SetDiary(ord);
            if (ord.Status == 2 || ord.Status == 3)
            {
                DisactivateRequest(ord.GuestRequestKey);
                UpdateOtherOrders(GetHost(ord.HostingUnitKey).HostKey, ord.OrderKey);
            }

        }

        //************************************ Get lists *********************************************
        public List<GuestRequest> GetGuestsList()
        {
           
        }

        public List<HostingUnit> GetHostingUnitsList()
        {
            
        }

        public List<Order> GetOrdersList()
        {
          
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
