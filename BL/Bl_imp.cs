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
            dal.AddGuestRequest(guest);            
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            dal.UpdateGuestRequest(guest);
        }

        //************************************ Hosting unit *********************************************
        public void AddHostUnit(HostingUnit host)
        {
            dal.AddHostUnit(host);
        }

        public void RemoveHostUnit(HostingUnit host)
        {
            dal.RemoveHostUnit(host);
        }

        public void UpdateHostUnit(HostingUnit host)
        {
            dal.UpdateHostUnit(host);
        }

        //************************************ Order *********************************************
        public void AddOrder(Order ord)
        {
            
            dal.AddOrder(ord);
        }
        public void UpdateOrder(Order ord)
        {
            bool x = dal.FindHostCollectionClearance(ord);
            if (ord.Status == 1 && !x)
                throw new Exception("Cannot sent order to the Guest without authorization" +
                    "for Collection Clearance");
            dal.UpdateOrder(ord);
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

        public IEnumerable<Order> GetAllHOrders(Func<Order, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<HostingUnit> GetAllHostingUnits(Func<HostingUnit, bool> predicate = null)
        {
            throw new NotImplementedException();
        }



    }
}
