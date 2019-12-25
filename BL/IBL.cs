using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace BL
{
    public interface IBL
    {
        //************************************ Guest Request *********************************************
        void AddGuestRequest(GuestRequest guest);
        void UpdateGuestRequest(GuestRequest guest);
        IEnumerable<GuestRequest> GetAllGuests(Func<GuestRequest, bool> predicate = null);


        //************************************ Hosting unit *********************************************
        HostingUnit GetHostingUnit(int hostingUnitkey);
        void AddHostUnit(HostingUnit host);
        void RemoveHostUnit(HostingUnit host);
        void UpdateHostUnit(HostingUnit host);
        IEnumerable<HostingUnit> GetAllHostingUnits(Func<HostingUnit, bool> predicate = null);


        //************************************ Order *********************************************
        bool CheckOrder(Order ord);
        void AddOrder(Order ord);        
        Order GetOrder(int orderKey);       
        void SetDiary(Order ord);
        void DisactivateRequest(int requestKey);
        void UpdateOtherOrders(int hostKey, int orderKey);
        void UpdateOrder(Order ord);


        //************************************ Host **************************************************
        Host GetHost(int hostKey);
        Host GetHostByUnit(int hostingUnitkey);
        void UpdateHost(Host host);


        //************************************ Get lists *********************************************
        IEnumerable<Order> GetAllOrders(Func<Order, bool> predicate = null);
        List<HostingUnit> GetHostingUnitsList();
        List<GuestRequest> GetGuestsList();
        List<Order> GetOrdersList();
        List<BankAccount> ListBankBranches();


        //************************************ Other Functions **************************************
        bool IsItAvailaible(HostingUnit unit, DateTime registration, int duration);
        IEnumerable<HostingUnit> AvailableUnits(DateTime registration, int duration);
        int DifferenceDays(DateTime a, DateTime? b = null);
        IEnumerable<Order> OlderOrders(int days);
        IEnumerable<GuestRequest> RequestsByCondition(Func<GuestRequest, bool> method);
        int OrdersByRequest(GuestRequest request);

        int OrdersByUnit(HostingUnit unit);
    }
}
