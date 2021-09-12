using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BE;
using DAL;


namespace BL
{
    public class Bl_imp : IBL
    {
        IDAL dal = FactoryDal.getdal();

        public Bl_imp()
        {
            Thread updateOrders = new Thread(UpdateOldOrdersDaily);
            updateOrders.Start();
        }

        #region GuestRequest
        public void AddGuestRequest(GuestRequest guest)
        {
            if (guest.releaseDate <= guest.entryDate)
                throw new Exception("The Entry Date must be at least one day before the Release Date");
            dal.AddGuestRequest(guest.Clone());
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            if (guest.releaseDate <= guest.entryDate)
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
                                                                 group item by item.area;
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
                                                              group item by item.adults + item.children;
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

        public List<GuestRequest> RelevantRequest(HostingUnit unit)
        {
            List<GuestRequest> temp1 = GetAllGuests();
            List<GuestRequest> temp2 = temp1.FindAll(x => x.area == unit.area
            && x.type == unit.type && IsItAvailaible(unit, x.entryDate, DifferenceDays(x.entryDate, x.releaseDate))
            && (x.status) && x.adults <= unit.adults
            && x.children <= unit.children && (IntToBool(x.pool) == unit.pool || x.pool == 0)
            && (IntToBool(x.jacuzzi) == unit.jacuzzi || x.jacuzzi == 0)
            && (IntToBool(x.childrenAttractions) == unit.childrenAttractions || x.childrenAttractions == 0));

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
            if (temp.Any(x => x.hostingUnitKey == unit.hostingUnitKey && x.status >= 0 && x.status < 2))
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
                if (item.hostingUnitKey == unit.hostingUnitKey && item.status > 0 && item.status < 3)
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
                if (unit.diary[current.Month - 1, current.Day - 1])
                    return false;
                current = current.AddDays(1);
            }
            return true;
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            return dal.GetAllHostingUnits();
        }

        public IEnumerable<HostingUnit> UnitsByHostKey(int hostKey)
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            IEnumerable<HostingUnit> temp2 = from item in temp1
                                             where item.owner.hostKey == hostKey
                                             select item;
            return temp2;
        }

        /// <summary>
        /// Returns Groups of Hosting Unitst by Area
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, HostingUnit>> UnitsByArea()
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            IEnumerable<IGrouping<string, HostingUnit>> temp2 = from item in temp1
                                                                group item by item.area;
            return temp2;
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
            DateTime entry = GetEntry(ord.guestRequestKey);
            DateTime release = GetRelease(ord.guestRequestKey);
            int duration = DifferenceDays(entry, release);
            if (IsItAvailaible(GetHostingUnit(ord.hostingUnitKey), entry, duration))
                return true;
            throw new Exception("The requested dates are not available");
        }

        public int AddOrder(Order ord)
        {
            CheckOrder(ord);
            ord.createDate = DateTime.Now;
            return dal.AddOrder(ord.Clone());
        }

        public Order GetOrder(int orderKey)
        {
            return dal.GetOrder(orderKey);
        }

        public bool[,] GetDiary(int hostingUnitKey)
        {
            return GetHostingUnit(hostingUnitKey).diary;
        }

        public DateTime GetEntry(int guestRequestKey)
        {
            return GetRequest(guestRequestKey).entryDate;
        }

        public DateTime GetRelease(int guestRequestKey)
        {
            return GetRequest(guestRequestKey).releaseDate;
        }

        /// <summary>
        /// Updates Hosting Unit Diary with dates order
        /// </summary>
        /// <param name="ord"> update order </param>
        public void SetDiary(Order ord)
        {
            HostingUnit temp = GetHostingUnit(ord.hostingUnitKey);
            DateTime current = GetEntry(ord.guestRequestKey);
            DateTime end = GetRelease(ord.guestRequestKey);
            end = end.AddDays(1);

            for (; current < end;)
            {
                temp.diary[current.Month - 1, current.Day - 1] = true;
                current = current.AddDays(1);
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
            temp.status = false;
            UpdateGuestRequest(temp);
        }

        /// <summary>
        /// Updates other host orders after Closing Order
        /// </summary>
        /// <param name="hostKey"></param>
        /// <param name="orderKey"> Closed Order Key</param>
        public void UpdateOtherOrders(string mail, int orderKey)
        {
            List<Order> temp1 = GetAllOrders();
            List<Order> temp2 = temp1.FindAll(x => GetRequest(x.guestRequestKey).mailAddress == mail
                                       && x.orderKey != orderKey);

            foreach (var item in temp2)
            {
                item.status = 3;
                UpdateOrder(item.Clone());
            }
        }

        public void UpdateOrder(Order ord)
        {
            int originStatus = GetOrder(ord.orderKey).status;
            // Order Status before the update
            if (originStatus == 0 && ord.status == 1 && !GetHostByUnit(ord.hostingUnitKey).collectionClearance)
                throw new Exception("Cannot sent mail to the Guest without authorization " +
                    "for Collection Clearance");
            if (originStatus == 2 || originStatus == 3)
                throw new Exception("Cannot update order after closing");

            if (originStatus != 2 && ord.status == 2)
            // When Update to Closed Order, Calculates the Commission Sum
            {
                int days = DifferenceDays(GetRequest(ord.guestRequestKey).releaseDate,
                GetRequest(ord.guestRequestKey).entryDate) + 1;
                ord.commissionPerDay = int.Parse(GetFromConfig("COMMISSION")) * days;
            }
            if (originStatus == 0 && ord.status == 1)
            {
                ord.orderDate = DateTime.Now;
            }
            dal.UpdateOrder(ord);            
            if (ord.status == 1 || ord.status == 3)
            {
                SetDiary(ord);
                DisactivateRequest(ord.guestRequestKey);
                UpdateOtherOrders(GetRequest(ord.guestRequestKey).mailAddress, ord.orderKey);
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
            List<Order> temp2 = temp1.FindAll(x => DifferenceDays(x.createDate) >= days
                                         || DifferenceDays(x.orderDate) >= days);
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
                if (item.guestRequestKey == request.guestRequestKey && item.status == 1)
                    count++;
            }
            return count;
        }

        public void UpdateOldOrders()
        {
            List<Order> temp1 = GetAllOrders();
            List<Order> temp2 = temp1.FindAll(x => DifferenceDays(x.orderDate) >= 30 && x.orderDate.ToString() != "01/01/0001 0:00:00");
            foreach (var item in temp2)
            {
                item.status = 4;
                dal.UpdateOrder(item);
            }
        }

        public void UpdateOldOrdersDaily()
        {
            while (true)
            {
                UpdateOldOrders();
                Thread.Sleep(86400000);
            }
        }
        #endregion


        #region Host
        public Host GetHost(int hostKey)
        {
            return dal.GetHost(hostKey);
        }

        public Host GetHostByUnit(int hostingUnitkey)
        {
            return GetHostingUnit(hostingUnitkey).owner;
        }

        public void UpdateHost(Host host)
        {
            if (GetHost(host.hostKey).collectionClearance && !host.collectionClearance)
            {
                IEnumerable<Order> temp = GetAllOrders();
                if (temp.Any(x => GetHostByUnit(x.hostingUnitKey).hostKey == host.hostKey && x.status != 0 && x.status != 3))
                    throw new Exception("Cannot Change Collection Clearance Authorization while Active Order existed");
            }
            dal.UpdateHost(host);
        }

        /// <summary>
        /// Returns groups of Hosts By number of Units
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, int>> HostsByUnits()
        {
            IEnumerable<HostingUnit> temp1 = GetAllHostingUnits();
            var temp2 = from item in temp1
                        group item by item.owner.hostKey into g
                        select new
                        {
                            ownerKey = g.Key,
                            Count = g.Count()
                        };
            IEnumerable<IGrouping<int, int>> temp3 = from item in temp2
                                                     group item.ownerKey by item.Count;
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
            return (c - a).Days + 1;
        }

        public List<BankBranch> ListBankBranches()
        {
            return dal.ListBankBranches();
        }

        public BankBranch CheckBranch(int codeBank, int codeBranch)
        {
            List<BankBranch> list = ListBankBranches();
            BankBranch temp = (from item in list
                               where item.bankNumber == codeBank && item.branchNumber == codeBranch
                               select item).FirstOrDefault();

            return temp;
        }

        public bool IntToBool(int value)
        {
            switch (value.ToString())
            {
                case "0"://אפשרי
                    return true;
                case "1"://לא מעוניין
                    return false;
                case "2"://הכרחי
                    return true;
            }
            return false;
        }

        public string GetFromConfig(string s)
        {
            return dal.GetFromConfig(s);
        }
        #endregion

    }
}


