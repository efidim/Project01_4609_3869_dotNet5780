﻿using System;
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
        #region GuestRequest
        void AddGuestRequest(GuestRequest guest);
        void UpdateGuestRequest(GuestRequest guest);
        GuestRequest GetRequest(int keyRequest);
        List<GuestRequest> GetAllGuests();
        IEnumerable<IGrouping<string, GuestRequest>> RequestsByArea();
        IEnumerable<IGrouping<int, GuestRequest>> RequestsByGuests();
        IEnumerable<GuestRequest> RequestsByCondition(Func<GuestRequest, bool> method);
        #endregion

        #region Hosting unit
        int AddHostUnit(HostingUnit host);
        void UpdateHostUnit(HostingUnit host);
        void RemoveHostUnit(HostingUnit host);
        HostingUnit GetHostingUnit(int hostingUnitkey);
        HostingUnit GetHostingUnitByName(string hostingUnitName);
        int OrdersByUnit(HostingUnit unit);
        bool IsItAvailaible(HostingUnit unit, DateTime entry, int duration);
        List<HostingUnit> GetAllHostingUnits();
        IEnumerable<IGrouping<string, HostingUnit>> UnitsByArea();
        List<HostingUnit> AvailableUnits(DateTime entry, int duration);
        #endregion

        #region Order
        bool CheckOrder(Order ord);
        void AddOrder(Order ord);        
        Order GetOrder(int orderKey);
        bool[,] GetDiary(int HostingUnitKey);
        DateTime GetEntry(int GuestRequestKey);
        DateTime GetRelease(int GuestRequestKey);
        void SetDiary(Order ord);
        void DisactivateRequest(int requestKey);
        void UpdateOtherOrders(int hostKey, int orderKey);
        void UpdateOrder(Order ord);
        int OrdersByRequest(GuestRequest request);
        List<Order> GetAllOrders();
        List<Order> OlderOrders(int days);        
        #endregion

        #region Host
        Host GetHost(int hostKey);
        Host GetHostByUnit(int hostingUnitkey);
        void UpdateHost(Host host);
        IEnumerable<IGrouping<int, Host>> HostsByUnits();
        
        #endregion
        
        #region Others                         
        int DifferenceDays(DateTime a, DateTime? b = null);              
        List<BankBranch> ListBankBranches();
        #endregion
    }
}
