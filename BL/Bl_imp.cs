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
            if (GetAllGuests().Any(x => x.GuestRequestKey == guest.GuestRequestKey))
                throw new Exception("The guest request already exists");
            if (guest.ReleaseDate <= guest.RegistrationDate)
                throw new Exception("The Registration Date must be at least one day before the Release Date");
            dal.AddGuestRequest(guest.Clone());            
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            dal.UpdateGuestRequest(guest.Clone());
        }


        //************************************ Hosting unit *********************************************
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            return dal.GetHostingUnit(hostingUnitkey);
        }
        public void AddHostUnit(HostingUnit host)
        {
            dal.AddHostUnit(host.Clone());
        }
        public void RemoveHostUnit(HostingUnit host)
        {
            IEnumerable<Order> temp = GetAllOrders();
            if (temp.Any(x => x.HostingUnitKey == host.HostingUnitKey && x.Status >= 0 && x.Status < 2))
                throw new Exception("Cannot Remove Hosting Unit while Order linked to this Unit is opened");
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
            ord.CreateDate = DateTime.Now;
            dal.AddOrder(ord.Clone());
        }
        public Order GetOrder(int orderKey)
        {
            return dal.GetOrder(orderKey);
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
                                       where GetHostByUnit(item.HostingUnitKey).HostKey == hostKey 
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

            int originStatus = GetOrder(ord.OrderKey).Status; 
            // Order Status before the update
            if (originStatus == 0 && ord.Status == 1 && !GetHostByUnit(ord.HostingUnitKey).CollectionClearance)
                throw new Exception("Cannot sent order to the Guest without authorization " +
                    "for Collection Clearance");           
            if (originStatus == 2 || originStatus == 3)
                throw new Exception("Cannot update order after closing");

            if (originStatus != 2 && ord.Status == 2)
                // When Update to Closed Order, Calculates the Commission Sum
            {
                int days = DifferenceDays(GetRequest(ord.GuestRequestKey).ReleaseDate,
                    GetRequest(ord.GuestRequestKey).RegistrationDate) + 1;
                ord.Commission = Configuration.COMMISSION * days; 
            }
            if (originStatus == 0 && ord.Status == 1)
            {
                Console.WriteLine("Mail sent to the Guest");
                ord.OrderDate = DateTime.Now;
            }
            dal.UpdateOrder(ord);
            SetDiary(ord);
            if (ord.Status == 2 || ord.Status == 3)
            {
                DisactivateRequest(ord.GuestRequestKey);
                UpdateOtherOrders(GetHostByUnit(ord.HostingUnitKey).HostKey, ord.OrderKey);
            }
        }


        //************************************ Host **************************************************
        public Host GetHost(int hostKey)
        {
            return dal.GetHost(hostKey);
        }
        public Host GetHostByUnit(int hostingUnitkey)
        {
            return dal.GetHost(hostingUnitkey);
        }
        public void UpdateHost(Host host)
        {
            if (GetHost(host.HostKey).CollectionClearance && !host.CollectionClearance)
            {
                IEnumerable<Order> temp = GetAllOrders();
                if (temp.Any(x => GetHostByUnit(x.HostingUnitKey).HostKey == host.HostKey && x.Status != 0 && x.Status != 3))
                    throw new Exception("Cannot Change Collection Clearance Authorization while Active Order existed");
            }
            dal.UpdateHost(host);
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

        //public IEnumerable<Host> GetAllHost()
        //{
        //    return dal.GetAllHost();
        //}


        //************************************ Other Functions **************************************

        /// <summary>
        /// Checks whether a particular Unit is Available on given days 
        /// </summary>
        /// <param name="unit">the checked unit</param>
        /// <param name="registration">start day</param>
        /// <param name="duration">duration</param>
        /// <returns></returns>
        public bool IsItAvailaible(HostingUnit unit, DateTime registration, int duration)
        {
            DateTime current = registration;
            for (int i = 0; i < duration; i++, current.AddDays(1))
            {
                if (unit.Diary[current.Month, current.Day])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Returns List with all the available Units on given days
        /// </summary>
        /// <param name="registration">start day</param>
        /// <param name="duration">duration</param>
        /// <returns></returns>
        public IEnumerable<HostingUnit> AvailableUnits(DateTime registration, int duration)
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            IEnumerable<HostingUnit> temp2 = from item in temp1
                                             where IsItAvailaible(item, registration, duration)
                                             select item;
            return temp2;
        }

        /// <summary>
        /// Returns number of days between two dates or from a to today if only one parameter is sent
        /// </summary>
        /// <param name="a"> starting date </param>
        /// <param name="b"> ending date </param>
        /// <returns></returns>
        public int DifferenceDays(DateTime a, DateTime? b = null) 
        {
            DateTime c = b ?? DateTime.Today;
            return (c - a).Days;
        }

        /// <summary>
        /// Returns List of all Orders with CreateDate or OrderDate older than guven days
        /// </summary>
        /// <param name="days"> given days </param>
        /// <returns></returns>
        public IEnumerable<Order> OlderOrders(int days)
        {
            IEnumerable<Order> temp1 = GetAllOrders();
            IEnumerable<Order> temp2 = from item in temp1
                                       where (DifferenceDays(item.CreateDate) >= days
                                       || DifferenceDays(item.OrderDate) >= days)
                                       select item;
            return temp2;
        } 

        /// <summary>
        ///  Returns all Requests with given condition
        /// </summary>
        /// <param name="method"> given condition</param>
        /// <returns></returns>
        public IEnumerable<GuestRequest> RequestsByCondition(Func<GuestRequest, bool> method)
        {
            IEnumerable<GuestRequest> temp1 = GetAllGuests();
            IEnumerable<GuestRequest> temp2 = from item in temp1
                                              where method(item)
                                              select item;
            return temp2;
        }

        /// <summary>
        /// Returns number of Orders that have been sent to a Guest Request
        /// </summary>
        /// <param name="request"> given Guest Request</param>
        /// <returns></returns>
        public int OrdersByRequest(GuestRequest request)
        {
            int count = 0;
            IEnumerable<Order> temp1 = GetAllOrders();
            foreach(var item in temp1)
            {
                if (item.GuestRequestKey == request.GuestRequestKey && item.Status == 1)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Returns number of sent orders or completed orders to given Hosting Unit
        /// </summary>
        /// <param name="unit">given Hosting Unit</param>
        /// <returns></returns>
        public int OrdersByUnit(HostingUnit unit)
        {
            int count = 0;
            IEnumerable<Order> temp1 = GetAllOrders();
            foreach (var item in temp1)
            {
                if (item.HostingUnitKey == unit.HostingUnitKey && item.Status > 0 && item.Status < 3)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Returns Groups of Guest Request by wanted Area
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, GuestRequest>> RequestsByArea()
        {
            IEnumerable<GuestRequest> temp1 = GetAllGuests();
            IEnumerable<IGrouping<string, GuestRequest>> temp2 = from item in temp1
                                                                 group item by item.Area;
            return temp2;
        }

        /// <summary>
        /// Returns Groups of Guest Requests by number of Guests (Adults and Children)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, GuestRequest>> RequestsByGuests()
        {
            IEnumerable<GuestRequest> temp1 = GetAllGuests();
            IEnumerable<IGrouping<int, GuestRequest>> temp2 = from item in temp1
                                                                 group item by item.Adults + item.Children;
            return temp2;
        }

        /// <summary>
        /// Returns groups of Hosts By number of Units
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Host>> HostsByUnits()
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            var temp2 = from item in temp1
                        group item by item.Owner into g
                        select new 
                        {
                           owner = g.Key,
                           Count = g.Count()
                        };
            IEnumerable<IGrouping<int, Host>> temp3 = from item in temp2
                                                      group item.owner by item.Count;
            return temp3;                                                
        }

        /// <summary>
        /// Returns Groups of Hosting Unitst by Area
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, HostingUnit>> UnitsByArea()
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            IEnumerable<IGrouping<string, HostingUnit>> temp2 = from item in temp1
                                                                 group item by item.Area;
            return temp2;
        }

    }
}
