using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DAL
{
    public interface IDAL
    {
        #region GuestRequest       

        void AddGuestRequest(GuestRequest guest);
        void UpdateGuestRequest(GuestRequest guest);
        GuestRequest GetRequest(int keyRequest);
        List<GuestRequest> GetGuestsList();
        IEnumerable<GuestRequest> GetAllGuests(Func<GuestRequest, bool> predicate = null);
        List<GuestRequest> getGuestRequests(Func<GuestRequest, bool> predicate = null);
        #endregion

        #region HostingUnit   
        void AddHostUnit(HostingUnit host);
        void RemoveHostUnit(HostingUnit host);
        void UpdateHostUnit(HostingUnit host);
        HostingUnit GetHostingUnit(int hostingUnitkey);
        bool[,] GetDiary(int HostingUnitKey);
        List<HostingUnit> GetHostingUnitsList();
        List<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicate = null);
        #endregion

        #region Order
        void AddOrder(Order ord);
        void UpdateOrder(Order ord);
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);
        bool CheckOrder(Order ord);
        Order GetOrder(int orderKey);
        DateTime GetRegistration(int GuestRequestKey);
        DateTime GetRelease(int GuestRequestKey);
        void SetDiary(Order ord);
        void DisactivateRequest(int requestKey);
        void UpdateOtherOrders(int hostKey, int orderKey);

        //List<Order> getOrders(Func<Order, bool> predicate);
        List<Order> GetOrdersList();
        #endregion
        //************************************ Host **************************************************
        #region Host
        Host GetHost(int hostKey);
        void UpdateHost(Host host);
        #endregion
        List<BankAccount> ListBankBranches();

       #region Other Functions
        
        /*bool IsItAvailaible(HostingUnit unit, DateTime registration, int duration);
        IEnumerable<HostingUnit> AvailableUnits(DateTime registration, int duration);
        int DifferenceDays(DateTime a, DateTime? b = null);
        IEnumerable<Order> OlderOrders(int days);
        IEnumerable<GuestRequest> RequestsByCondition(Func<GuestRequest, bool> method);

        int OrdersByUnit(HostingUnit unit);
        IEnumerable<IGrouping<string, GuestRequest>> RequestsByArea();
        IEnumerable<IGrouping<int, GuestRequest>> RequestsByGuests();
        IEnumerable<IGrouping<int, Host>> HostsByUnits();
        IEnumerable<IGrouping<string, HostingUnit>> UnitsByArea();*/

        #endregion
    }
}
