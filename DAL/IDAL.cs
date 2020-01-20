﻿using System;
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
        List<GuestRequest> GetAllGuests();
        #endregion

        #region HostingUnit   
        int AddHostUnit(HostingUnit host);
        void RemoveHostUnit(HostingUnit host);
        void UpdateHostUnit(HostingUnit host);
        HostingUnit GetHostingUnit(int hostingUnitkey);
        HostingUnit GetHostingUnitByName(string hostingUnitName);
        List<HostingUnit> GetAllHostingUnits();
      
        #endregion

        #region Order
        int AddOrder(Order ord);
        void UpdateOrder(Order ord);
        Order GetOrder(int orderKey);
        DateTime GetEntryDate(int GuestRequestKey);
        DateTime GetRelease(int GuestRequestKey);
        List<Order> GetAllOrders();
        #endregion
 
        #region Host
        Host GetHost(int hostKey);
        void UpdateHost(Host host);
        #endregion
        List<BankBranch> ListBankBranches();

    }
}
