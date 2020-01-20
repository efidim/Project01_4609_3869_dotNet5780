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


        #region GuestRequest
        public void AddGuestRequest(GuestRequest guest)
        {
           if (guest.ReleaseDate <= guest.EntryDate)
               throw new Exception("The Entry Date must be at least one day before the Release Date");
           dal.AddGuestRequest(guest.Clone());
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
          if (guest.ReleaseDate <= guest.EntryDate)
             throw new Exception("The Entry Date must be at least one day before the Release Date");
          dal.UpdateGuestRequest(guest.Clone());
        }

        public GuestRequest GetRequest(int keyRequest)
        {
            return dal.GetRequest(keyRequest);
        }

        public List<GuestRequest> GetAllGuests()
        {
            return dal.GetAllGuests();
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
        #endregion


        #region HostingUnit
       
        public int AddHostUnit(HostingUnit unit)
        {
            return dal.AddHostUnit(unit.Clone());
        }       
        public void UpdateHostUnit(HostingUnit unit)
        {
            dal.UpdateHostUnit(unit.Clone());
        }
        public void RemoveHostUnit(HostingUnit unit)
        {
            IEnumerable<Order> temp = GetAllOrders();
            if (temp.Any(x => x.HostingUnitKey == unit.HostingUnitKey && x.Status >= 0 && x.Status < 2))
                throw new Exception("Cannot Remove Hosting Unit while Order linked to this Unit is opened");
            dal.RemoveHostUnit(unit);
        } 
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            return dal.GetHostingUnit(hostingUnitkey);
        }

        public HostingUnit GetHostingUnitByName(string hostingUnitName)
        {
            return dal.GetHostingUnitByName(hostingUnitName);
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
        /// Checks whether a particular Unit is Available on given days 
        /// </summary>
        /// <param name="unit">the checked unit</param>
        /// <param name="entry">start day</param>
        /// <param name="duration">duration</param>
        /// <returns></returns>
        public bool IsItAvailaible(HostingUnit unit, DateTime entry, int duration)
        {
            DateTime current = entry;
            for (int i = 0; i < duration; i++)
            {
                if (unit.Diary[current.Month-1, current.Day-1])
                    return false;
                current = current.AddDays(1);
            }
            return true;
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            return dal.GetAllHostingUnits();
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
        public IEnumerable<HostingUnit> UnitsByCondition(Func<HostingUnit, bool> method)
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            IEnumerable<HostingUnit> temp5 = from item in temp1
                                              where method(item)
                                              select item;
            return temp5;
        }
        /// <summary>
        /// Returns List with all the available Units on given days
        /// </summary>
        /// <param name="entry">start day</param>
        /// <param name="duration">duration</param>
        /// <returns></returns>
        public List<HostingUnit> AvailableUnits(DateTime entry, int duration)
        {
            List<HostingUnit> temp1 = GetAllHostingUnits();
            List<HostingUnit> temp2 = temp1.FindAll(x => IsItAvailaible(x, entry, duration)); 
            return temp2;
        }
        #endregion


        #region Order

        /// <summary>
        /// Checks whether the requested dates of an Order are available 
        /// </summary>
        /// <param name="ord"> order to check</param>
        /// <returns></returns>
        public bool CheckOrder(Order ord)
        {
            DateTime entry = GetEntry(ord.GuestRequestKey);
            DateTime release = GetRelease(ord.GuestRequestKey);
            int duration = DifferenceDays(entry, release);
            if (IsItAvailaible(GetHostingUnit(ord.HostingUnitKey), entry, duration))
                return true;
            throw new Exception("The requested dates are not available");               
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

        public bool[,] GetDiary(int hostingUnitKey)
        {
            return GetHostingUnit(hostingUnitKey).Diary;
        }

        public DateTime GetEntry(int guestRequestKey)
        {
            return GetRequest(guestRequestKey).EntryDate;
        }

        public DateTime GetRelease(int guestRequestKey)
        {
            return GetRequest(guestRequestKey).ReleaseDate;
        }

        /// <summary>
        /// Updates Hosting Unit Diary with dates order
        /// </summary>
        /// <param name="ord"> update order </param>
        public void SetDiary(Order ord)
        {
            HostingUnit temp = GetHostingUnit(ord.HostingUnitKey);
            DateTime current = GetEntry(ord.GuestRequestKey);
            DateTime end = GetRelease(ord.GuestRequestKey);
            end = end.AddDays(1);

            for (; current < end; )
            {
               temp.Diary[current.Month-1,current.Day-1] = true;
               current=current.AddDays(1);   
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
            List<Order> temp1 = GetAllOrders();
            List<Order> temp2 = temp1.FindAll(x => GetHostByUnit(x.HostingUnitKey).HostKey == hostKey
                                       && x.OrderKey != orderKey);

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
                GetRequest(ord.GuestRequestKey).EntryDate) + 1;
                ord.CommissionPerDay = Configuration.COMMISSION * days;
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

        public List<Order> GetAllOrders()
        {
            return dal.GetAllOrders();
        }

        /// <summary>
        /// Returns List of all Orders with CreateDate or OrderDate older than given days
        /// </summary>
        /// <param name="days"> given days </param>
        /// <returns></returns>
        public List<Order> OlderOrders(int days)
        {
           List<Order> temp1 = GetAllOrders();
           List<Order> temp2 = temp1.FindAll(x => DifferenceDays(x.CreateDate) >= days
                                        || DifferenceDays(x.OrderDate) >= days);
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
            foreach (var item in temp1)
            {
                if (item.GuestRequestKey == request.GuestRequestKey && item.Status == 1)
                    count++;
            }
            return count;
        }
        #endregion


        #region Host
        public Host GetHost(int hostKey)
        {
            return dal.GetHost(hostKey);
        }
        
        public Host GetHostByUnit(int hostingUnitkey)
        {
            return GetHostingUnit(hostingUnitkey).Owner;
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
        #endregion


        #region Others

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

        public List<BankBranch> ListBankBranches()
        {
            return dal.ListBankBranches();
        }
        #endregion
    
    }
}    


