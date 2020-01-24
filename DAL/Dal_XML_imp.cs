using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using BE;

namespace DAL
{
    public class Dal_XML_imp : IDAL
    {
        //Singelton       

        protected static Dal_XML_imp instance = null;

        public static Dal_XML_imp GetInstance()
        {
            if (instance == null)
                instance = new Dal_XML_imp();
            return instance;
        }


        //Roots and paths of the files
        private XElement GuestRequestsRoot;
        private const string GuestRequestsPath = @"..\..\..\xml files\GuestRequest.xml";

        private XElement HostingUnitsRoot;
        private const string HostingUnitsPath = @"..\..\..\xml files\HostingUnits.xml";

        private XElement OrdersRoot;
        private const string OrdersPath = @"..\..\..\xml files\Orders.xml";

        private XElement BankBranchDetailsRoot;
        private string BankBranchDetailsPath = @"..\..\..\xml files\BankBranchDetails.xml";

        private XElement ConfigRoot;
        private const string ConfigPath = @"..\..\..\xml files\Config.xml";

        protected Dal_XML_imp()
        {
            // GuestRequests loading
            if (!File.Exists(GuestRequestsPath))
            {
                GuestRequestsRoot = new XElement("GuestRequests");
                GuestRequestsRoot.Save(GuestRequestsPath);
            }
            else
            {
                Load(ref GuestRequestsRoot, GuestRequestsPath);
            }

                
            // HostingUnits Loading
            if (!File.Exists(HostingUnitsPath))
            {
               HostingUnitsRoot = new XElement("HostingUnits");
                        HostingUnitsRoot.Save(HostingUnitsPath);
            }
            else
            {
                Load(ref HostingUnitsRoot, HostingUnitsPath);
            }

            // Orders Loading
            if (!File.Exists(OrdersPath))
            {
                OrdersRoot = new XElement("Orders");
                OrdersRoot.Save(OrdersPath);
            }
            else
            {
                Load(ref OrdersRoot, OrdersPath);
            }

            // BankBranchDetails Loading
            if (!File.Exists(BankBranchDetailsPath))
            {
                BankBranchDetailsRoot = new XElement("BankBranchDetails");
                BankBranchDetailsRoot.Save(BankBranchDetailsPath);
            }
            else
            {
                Load(ref BankBranchDetailsRoot, BankBranchDetailsPath);
            }

            // Config Loading
            if (!File.Exists(ConfigPath))
            {
                ConfigRoot = new XElement("Config");
                ConfigRoot.Save(ConfigPath);
            }
            else
            {
                Load(ref ConfigRoot, ConfigPath);
            }
        }

        private void Load(ref XElement t, string a)
        {
            try
            {
                t = XElement.Load(a);
            }
            catch
            {
                throw new DirectoryNotFoundException(" שגיאה! בעיית טעינת קובץ:" + a);
            }
        }


        #region Guest Request
        public void AddGuestRequest(GuestRequest guest)
        {
            try
            {
                Load(ref GuestRequestsRoot, GuestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            int key = int.Parse(GuestRequestsRoot.Element("guestKey").Value) + 1;

            XElement GuestRequestKey = new XElement("GuestRequestKey", guest.GuestRequestKey);
            XElement PrivateName = new XElement("PrivateName", guest.PrivateName);
            XElement FamilyName = new XElement("FamilyName", guest.FamilyName);
            XElement MailAddress = new XElement("Mail Address", guest.MailAddress);
            XElement Status = new XElement("Status", guest.Status);
            XElement RegistrationDate = new XElement("Registration Date", guest.RegistrationDate);
            XElement EntryDate = new XElement("Entry Date", guest.EntryDate);
            XElement ReleaseDate = new XElement("Release Date", guest.ReleaseDate);
            XElement Area = new XElement("Area", guest.Area);
            XElement Type = new XElement("Type", guest.Type);
            XElement Adults = new XElement("Adults", guest.Adults);
            XElement Children = new XElement("Children", guest.Children);
            XElement Pool = new XElement("Pool", guest.Pool);
            XElement Jacuzzi = new XElement("Jacuzzi", guest.Jacuzzi);
            XElement ChildrenAttractions = new XElement("ChildrenAttractions", guest.ChildrenAttractions);

            GuestRequestsRoot.Add("Guest Request", GuestRequestKey, PrivateName, FamilyName, MailAddress,
                Status, RegistrationDate, EntryDate, ReleaseDate, Area, Type, Adults, Children, Pool, Jacuzzi, ChildrenAttractions);
            GuestRequestsRoot.Save(GuestRequestsPath);
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            try
            {
                Load(ref GuestRequestsRoot, GuestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement requestElement = (from req in GuestRequestsRoot.Elements()
                                       where int.Parse(req.Element("GuestRequestKey").Value) == guest.GuestRequestKey
                                       select req).FirstOrDefault();

            if (requestElement == null) 
            {
                AddGuestRequest(guest);
                return;
            }
            requestElement.Element("PrivateName").Value = guest.PrivateName;
            requestElement.Element("FamilyName").Value = guest.FamilyName;
            requestElement.Element("Mail Address").Value = guest.MailAddress;
            requestElement.Element("Status").Value = guest.Status.ToString();
            requestElement.Element("Entry Date").Value = guest.EntryDate.ToString();
            requestElement.Element("Release Date").Value = guest.ReleaseDate.ToString();
            requestElement.Element("Area").Value = guest.Area;
            requestElement.Element("Type").Value = guest.Type;
            requestElement.Element("Adults").Value = guest.Adults.ToString();
            requestElement.Element("Children").Value = guest.Children.ToString();
            requestElement.Element("Pool").Value = guest.Pool.ToString();
            requestElement.Element("Jacuzzi").Value = guest.Jacuzzi.ToString();
            requestElement.Element("ChildrenAttractions").Value = guest.ChildrenAttractions.ToString();

            GuestRequestsRoot.Save(GuestRequestsPath);
        }

        /// <summary>
        ///  Returns a Guest Request request by the GuestRequestKey
        /// </summary>
        /// <param name="keyRequest">ID</param>
        /// <returns>GuestRequest</returns>
        public GuestRequest GetRequest(int keyRequest)
        {
            try
            {
                Load(ref GuestRequestsRoot, GuestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            GuestRequest guest;

            guest = (from req in GuestRequestsRoot.Elements()
                     where int.Parse(req.Element("GuestRequestKey").Value) == keyRequest
                     select new GuestRequest()
                     {
                         GuestRequestKey = int.Parse(req.Element("GuestRequestKey").Value),
                         MailAddress = req.Element("Mail Address").Value,
                         RegistrationDate = DateTime.Parse(req.Element("Registration Date").Value),
                         Status = bool.Parse(req.Element("Status").Value),
                         EntryDate = DateTime.Parse(req.Element("Entry Date").Value),
                         ReleaseDate = DateTime.Parse(req.Element("Release Date").Value),
                         Area = req.Element("Area").Value,
                         Type = req.Element("Type").Value,
                         Adults = int.Parse(req.Element("Adults").Value),
                         Children = int.Parse(req.Element("Children").Value),
                         Pool = int.Parse(req.Element("Pool").Value),
                         Jacuzzi= int.Parse(req.Element("Jacuzzi").Value),
                         ChildrenAttractions = int.Parse(req.Element("ChildrenAttractions").Value),
                     }).FirstOrDefault();

            if (guest == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת דרישה עם מפתח זה");

            return guest.Clone();
        }

        /// <summary>
        /// Removes Guest Request by GuestRequestKey
        /// </summary>
        /// <param name="keyRequest"> key </param>
        public void RemoveGuestRequest(int keyRequest)
        {
            try
            {
                Load(ref GuestRequestsRoot, GuestRequestsPath);
                XElement guest;
                guest = (from req in GuestRequestsRoot.Elements()
                         where int.Parse(req.Element("GuestRequestKey").Value) == keyRequest
                         select req).FirstOrDefault();

                if (guest == null)
                    throw new KeyNotFoundException("שגיאה! לא קיימת במערכת דרישה עם מפתח זה");

                guest.Remove();
                GuestRequestsRoot.Save(GuestRequestsPath);                
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }         
            catch (KeyNotFoundException r)
            {
                throw r;
            }
        }

        /// <summary>
        /// Returns GuestRequests list
        /// </summary>
        /// <returns>List of GuestRequest</returns>
        public List<GuestRequest> GetAllGuests()
        {
            try
            {
                Load(ref GuestRequestsRoot, GuestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            List<GuestRequest> requests;

            try
            {
                requests = (from req in GuestRequestsRoot.Elements()
                         select new GuestRequest()
                         {
                             GuestRequestKey = int.Parse(req.Element("GuestRequestKey").Value),
                             MailAddress = req.Element("Mail Address").Value,
                             RegistrationDate = DateTime.Parse(req.Element("Registration Date").Value),
                             Status = bool.Parse(req.Element("Status").Value),
                             EntryDate = DateTime.Parse(req.Element("Entry Date").Value),
                             ReleaseDate = DateTime.Parse(req.Element("Release Date").Value),
                             Area = req.Element("Area").Value,
                             Type = req.Element("Type").Value,
                             Adults = int.Parse(req.Element("Adults").Value),
                             Children = int.Parse(req.Element("Children").Value),
                             Pool = int.Parse(req.Element("Pool").Value),
                             Jacuzzi = int.Parse(req.Element("Jacuzzi").Value),
                             ChildrenAttractions = int.Parse(req.Element("ChildrenAttractions").Value),
                         }).ToList();
            }
            catch
            {
                requests = null;
            }

            return requests;
        }
        #endregion


        #region Hosting unit
        public int AddHostUnit(HostingUnit host)
        {
            host.HostingUnitKey = Configuration.unitKey++;
            DataSource.HostingUnits.Add(host);
            return host.HostingUnitKey;
        }

        public void RemoveHostUnit(HostingUnit host)
        {
            int id = host.HostingUnitKey;
            int count = DataSource.HostingUnits.RemoveAll(x => x.HostingUnitKey == id);
            if (count == 0)
                throw new Exception("The hosting unit does not exist");
        }

        public void UpdateHostUnit(HostingUnit host)
        {
            int index = DataSource.HostingUnits.FindIndex(h => h.HostingUnitKey == host.HostingUnitKey);
            if (index == -1)
                DataSource.HostingUnits.Add(host);
            DataSource.HostingUnits[index] = host;
        }
        /// <summary>
        ///  Returns a hosting unit by the ID
        /// </summary>
        /// <param name="hostingUnitkey">ID's unit</param>
        /// <returns>HostingUnit</returns>
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            List<HostingUnit> temp = DataSource.HostingUnits;
            IEnumerable<HostingUnit> temp1 = from item in temp
                                             where item.HostingUnitKey == hostingUnitkey
                                             select item;
            if (temp1.ToList().Count == 0)
            {
                throw new Exception("The hosting unit does not exist");
            }
            HostingUnit temp2 = temp1.First();
            return temp2.Clone();
        }

        /// <summary>
        ///  Returns a hosting unit by the Name
        /// </summary>
        /// <param name="hostingUnitName">Unit Name</param>
        /// <returns>HostingUnit</returns>
        public HostingUnit GetHostingUnitByName(string hostingUnitName)
        {
            List<HostingUnit> temp = DataSource.HostingUnits;
            IEnumerable<HostingUnit> temp1 = from item in temp
                                             where item.HostingUnitName == hostingUnitName
                                             select item;
            if (temp1.ToList().Count == 0)
            {
                throw new Exception("The hosting unit does not exist");
            }
            HostingUnit temp2 = temp1.First();
            return temp2.Clone();
        }


        public List<HostingUnit> GetAllHostingUnits()
        {
            if (DataSource.HostingUnits == null)
                throw new Exception("The HostingUnits list is empty");
            return DataSource.HostingUnits.Select(hu => (HostingUnit)hu.Clone()).ToList();
        }
        #endregion
    

        #region Order
        public int AddOrder(Order ord)
        {
            ord.OrderKey = Configuration.orderKey++;
            DataSource.Orders.Add(ord);
            return ord.OrderKey;
        }
        public void UpdateOrder(Order ord)
        {
            int index = DataSource.Orders.FindIndex(o => o.OrderKey == ord.OrderKey);
            if (index == -1)
                DataSource.Orders.Add(ord);

            DataSource.Orders[index] = ord;
        }
        public Order GetOrder(int orderKey)
        {
            List<Order> temp = DataSource.Orders;
            IEnumerable<Order> temp1 = from item in temp
                                       where item.OrderKey == orderKey
                                       select item;
            if (temp1.ToList().Count == 0)
            {
                throw new Exception("The order does not exist");
            }
            Order temp2 = temp1.First();
            return temp2.Clone();
        }
        public DateTime GetEntryDate(int GuestRequestKey)
        {
            int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            return DataSource.GuestRequests[index].EntryDate;
        }
        public DateTime GetRelease(int GuestRequestKey)
        {
            int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            return DataSource.GuestRequests[index].ReleaseDate;
        }
        public List<Order> GetAllOrders()
        {
            if (DataSource.Orders == null)
                throw new Exception("The Orders list is empty");
            return DataSource.Orders.Select(Or => (Order)Or.Clone()).ToList();
        }
        #endregion
        

        #region Host
        public Host GetHost(int hostKey)
        {
            int index = DataSource.HostingUnits.FindIndex(h => h.Owner.HostKey == hostKey);
            if (index == -1)
                throw new Exception("The host does not exist");
            return DataSource.HostingUnits[index].Owner;
        }
        public void UpdateHost(Host host)
        {
            int index = DataSource.HostingUnits.FindIndex(h => h.Owner.HostKey == host.HostKey);
            if (index == -1)
                throw new Exception("The host does not exist");
            DataSource.HostingUnits[index].Owner = host;
        }

        #endregion
       

        /// <summary>
        /// returns list of the five banks
        /// </summary>
        /// <returns>list of banks</returns>
        public List<BankBranch> ListBankBranches()
        {
            List<BankBranch> TheFiveBanks = new List<BankBranch>()
        {
           new BankBranch() {BankNumber=1,BankName="MyBank",BranchNumber=11,BranchAddress= "MyBank@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=2,BankName="Mizrahi",BranchNumber=22,BranchAddress= "Mizrahi@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=3,BankName="Discont",BranchNumber=33,BranchAddress= "Discont@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=4,BankName="Pagi",BranchNumber=44,BranchAddress= "Pagi@gmail.com",BranchCity="Jerusalem"},
           new BankBranch() {BankNumber=5,BankName="Leumi",BranchNumber=55,BranchAddress= "Leumi@gmail.com",BranchCity="Jerusalem"}
        };
            return TheFiveBanks;
        }
    }
}
