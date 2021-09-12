using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Net;
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
        private const string GuestRequestsPath = @"..\..\..\XML_Files\GuestRequests.xml";

        private XElement HostingUnitsRoot;
        private const string HostingUnitsPath = @"..\..\..\XML_Files\HostingUnits.xml";

        private XElement OrdersRoot;
        private const string OrdersPath = @"..\..\..\XML_Files\Orders.xml";

        private XElement BankBranchesRoot;
        private string BankBranchesPath = @"..\..\..\XML_Files\atm.xml";

        private XElement ConfigRoot;
        private const string ConfigPath = @"..\..\..\XML_Files\Config.xml";

        public bool isFileLoaded;

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

            // ATM Loading
            isFileLoaded = false;
            new Thread( () =>
            {
                const string xmlLocalPath = @"atm.xml";
                WebClient wc = new WebClient();
                try
                {
                    string xmlServerPath = @"http://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                catch (Exception)
                {
                    string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                }
                finally
                {
                    wc.Dispose();
                    Load(ref BankBranchesRoot, BankBranchesPath);
                    isFileLoaded = true;
                }

            }).Start();


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

            int key = int.Parse(ConfigRoot.Element("guestKey").Value) + 1;
            XElement guestKey = ConfigRoot;
            guestKey.Element("guestKey").Value = key.ToString();
            ConfigRoot.Save(ConfigPath);

            XElement GuestRequestKey = new XElement("GuestRequestKey", key);
            XElement PrivateName = new XElement("PrivateName", guest.privateName);
            XElement FamilyName = new XElement("FamilyName", guest.familyName);
            XElement MailAddress = new XElement("MailAddress", guest.mailAddress);
            XElement Name = new XElement("Name", PrivateName, FamilyName);
            XElement Status = new XElement("Status", guest.status);
            XElement RegistrationDate = new XElement("RegistrationDate", guest.registrationDate);
            XElement EntryDate = new XElement("EntryDate", guest.entryDate);
            XElement ReleaseDate = new XElement("ReleaseDate", guest.releaseDate);
            XElement Dates = new XElement("Dates", RegistrationDate, EntryDate, ReleaseDate);
            XElement Area = new XElement("Area", guest.area);
            XElement Type = new XElement("Type", guest.type);
            XElement Adults = new XElement("Adults", guest.adults);
            XElement Children = new XElement("Children", guest.children);
            XElement Guests = new XElement("Guests", Adults, Children);
            XElement Pool = new XElement("Pool", guest.pool);
            XElement Jacuzzi = new XElement("Jacuzzi", guest.jacuzzi);
            XElement ChildrenAttractions = new XElement("ChildrenAttractions", guest.childrenAttractions);
            XElement Attractions = new XElement("Attractions", ChildrenAttractions, Jacuzzi, Pool);
            XElement GuestRequest = new XElement("GuestRequest", GuestRequestKey, Name, MailAddress,
                Status, Dates, Area, Type, Guests, Attractions);

            GuestRequestsRoot.Add(GuestRequest);
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
                                       where int.Parse(req.Element("GuestRequestKey").Value) == guest.guestRequestKey
                                       select req).FirstOrDefault();

            if (requestElement == null)
            {
                AddGuestRequest(guest);
                return;
            }
            requestElement.Element("Name").Element("PrivateName").Value = guest.privateName;
            requestElement.Element("Name").Element("FamilyName").Value = guest.familyName;
            requestElement.Element("MailAddress").Value = guest.mailAddress;
            requestElement.Element("Status").Value = guest.status.ToString();
            requestElement.Element("Dates").Element("EntryDate").Value = guest.entryDate.ToString();
            requestElement.Element("Dates").Element("ReleaseDate").Value = guest.releaseDate.ToString();
            requestElement.Element("Area").Value = guest.area;
            requestElement.Element("Type").Value = guest.type;
            requestElement.Element("Guests").Element("Adults").Value = guest.adults.ToString();
            requestElement.Element("Guests").Element("Children").Value = guest.children.ToString();
            requestElement.Element("Attractions").Element("Pool").Value = guest.pool.ToString();
            requestElement.Element("Attractions").Element("Jacuzzi").Value = guest.jacuzzi.ToString();
            requestElement.Element("Attractions").Element("ChildrenAttractions").Value = guest.childrenAttractions.ToString();

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
                         guestRequestKey = int.Parse(req.Element("GuestRequestKey").Value),
                         privateName = req.Element("Name").Element("PrivateName").Value,
                         familyName = req.Element("Name").Element("FamilyName").Value,
                         mailAddress = req.Element("MailAddress").Value,
                         registrationDate = DateTime.Parse(req.Element("Dates").Element("RegistrationDate").Value),
                         status = bool.Parse(req.Element("Status").Value),
                         entryDate = DateTime.Parse(req.Element("Dates").Element("EntryDate").Value),
                         releaseDate = DateTime.Parse(req.Element("Dates").Element("ReleaseDate").Value),
                         area = req.Element("Area").Value,
                         type = req.Element("Type").Value,
                         adults = int.Parse(req.Element("Guests").Element("Adults").Value),
                         children = int.Parse(req.Element("Guests").Element("Children").Value),
                         pool = int.Parse(req.Element("Attractions").Element("Pool").Value),
                         jacuzzi = int.Parse(req.Element("Attractions").Element("Jacuzzi").Value),
                         childrenAttractions = int.Parse(req.Element("Attractions").Element("ChildrenAttractions").Value),
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

            List<GuestRequest> requests = new List<GuestRequest>();

            try
            {
                requests = (from req in GuestRequestsRoot.Elements()
                            select new GuestRequest()
                            {
                                guestRequestKey = int.Parse(req.Element("GuestRequestKey").Value),
                                privateName = req.Element("Name").Element("PrivateName").Value,
                                familyName = req.Element("Name").Element("FamilyName").Value,
                                mailAddress = req.Element("MailAddress").Value,
                                registrationDate = DateTime.Parse(req.Element("Dates").Element("RegistrationDate").Value),
                                status = bool.Parse(req.Element("Status").Value),
                                entryDate = DateTime.Parse(req.Element("Dates").Element("EntryDate").Value),
                                releaseDate = DateTime.Parse(req.Element("Dates").Element("ReleaseDate").Value),
                                area = req.Element("Area").Value,
                                type = req.Element("Type").Value,
                                adults = int.Parse(req.Element("Guests").Element("Adults").Value),
                                children = int.Parse(req.Element("Guests").Element("Children").Value),
                                pool = int.Parse(req.Element("Attractions").Element("Pool").Value),
                                jacuzzi = int.Parse(req.Element("Attractions").Element("Jacuzzi").Value),
                                childrenAttractions = int.Parse(req.Element("Attractions").Element("ChildrenAttractions").Value),
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
            try
            {
                Load(ref HostingUnitsRoot, HostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where hos.Element("HostingUnitName").Value == host.hostingUnitName
                     select hos).FirstOrDefault();
            if (host1 != null)
            {
                throw new KeyNotFoundException("יחידת אירוח זו כבר קיימת במערכת");
            }

            int key = int.Parse(ConfigRoot.Element("unitKey").Value) + 1;
            host.hostingUnitKey = key;
            XElement HostingUnitKey = ConfigRoot;
            HostingUnitKey.Element("unitKey").Value = key.ToString();
            ConfigRoot.Save(ConfigPath);

            List<HostingUnit> temp = new List<HostingUnit>();
            host1 = (from hos in HostingUnitsRoot.Elements()
                     select hos).FirstOrDefault();
            if (host1 != null)
            {
                temp = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            }
            temp.Add(host);
            saveToXML<List<HostingUnit>>(temp, HostingUnitsPath);

            return key;
        }


        public void RemoveHostUnit(HostingUnit host)
        {
            try
            {
                Load(ref HostingUnitsRoot, HostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            try
            {
                XElement host1;
                host1 = (from hos in HostingUnitsRoot.Elements()
                         where int.Parse(hos.Element("HostingUnitKey").Value) == host.hostingUnitKey
                         select hos).FirstOrDefault();

                if (host1 == null)
                    throw new KeyNotFoundException("שגיאה! לא קיימת במערכת יחידה עם מפתח זה");

                host1.Remove();
                HostingUnitsRoot.Save(HostingUnitsPath);
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


        public void UpdateHostUnit(HostingUnit host)
        {
            try
            {
                Load(ref HostingUnitsRoot, HostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("HostingUnitKey").Value) == host.hostingUnitKey
                     select hos).FirstOrDefault();
            if (host1 == null)
            {
                AddHostUnit(host);
                return;
            }

            List<HostingUnit> temp = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            temp.RemoveAll(x => x.hostingUnitKey == host.hostingUnitKey);
            temp.Add(host);
            saveToXML<List<HostingUnit>>(temp, HostingUnitsPath);
        }

        /// <summary>
        ///  Returns a hosting unit by the ID
        /// </summary>
        /// <param name="hostingUnitkey">ID's unit</param>
        /// <returns>HostingUnit</returns>
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            try
            {
                Load(ref HostingUnitsRoot, HostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("HostingUnitKey").Value) == hostingUnitkey
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת יחידה עם מפתח זה");
            List<HostingUnit> hostunits = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            HostingUnit host = new HostingUnit();
            foreach (var item in hostunits)
            {
                if (item.hostingUnitKey == hostingUnitkey)
                { host = item.Clone(); break; }

            }
            return host.Clone();


        }

        /// <summary>
        ///  Returns a hosting unit by the Name
        /// </summary>
        /// <param name="hostingUnitName">Unit Name</param>
        /// <returns>HostingUnit</returns>
        public HostingUnit GetHostingUnitByName(string hostingUnitName)
        {
            try
            {
                Load(ref HostingUnitsRoot, HostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where hos.Element("HostingUnitName").Value == hostingUnitName
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת יחידה עם שם זה");
            List<HostingUnit> HostUnits = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            HostingUnit host = new HostingUnit();
            foreach (var item in HostUnits)
            {
                if (item.hostingUnitName == hostingUnitName)
                { host = item.Clone(); break; }

            }
            return host.Clone();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            List<HostingUnit> hostingUnits = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            if (hostingUnits == null)
                throw new KeyNotFoundException("אין יחידות אירוח במאגר הנתונים");
            return hostingUnits.Select(hu => (HostingUnit)hu.Clone()).ToList();
        }

        #endregion


        #region Order
        public int AddOrder(Order ord)
        {
            try
            {
                Load(ref OrdersRoot, OrdersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement ord1 = (from or in OrdersRoot.Elements()
                             where int.Parse(or.Element("HostingUnitKey").Value) == ord.hostingUnitKey &&
                             int.Parse(or.Element("GuestRequestKey").Value) == ord.guestRequestKey
                             select or).FirstOrDefault();
            if (ord1 != null)
            {
                throw new KeyNotFoundException("הזמנה זו קיימת כבר במערכת");
            }
            int key = int.Parse(ConfigRoot.Element("orderKey").Value) + 1;
            ord.orderKey = key;
            XElement orderKey = ConfigRoot;
            orderKey.Element("orderKey").Value = key.ToString();
            ConfigRoot.Save(ConfigPath);

            List<Order> temp = new List<Order>();
            ord1 = (from or in OrdersRoot.Elements()
                    select or).FirstOrDefault();
            if (ord1 != null)
            {
                temp = LoadFromXML<List<Order>>(OrdersPath);
            }
            temp.Add(ord);
            saveToXML<List<Order>>(temp, OrdersPath);

            return key;
        }

        public void UpdateOrder(Order ord)
        {
            try
            {
                Load(ref OrdersRoot, OrdersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement ord1 = (from or in OrdersRoot.Elements()
                             where int.Parse(or.Element("OrderKey").Value) == ord.orderKey
                             select or).FirstOrDefault();
            if (ord1 == null)
            {
                AddOrder(ord);
                return;
            }

            List<Order> temp = LoadFromXML<List<Order>>(OrdersPath);
            temp.RemoveAll(x => x.orderKey == ord.orderKey);
            temp.Add(ord);
            saveToXML<List<Order>>(temp, OrdersPath);
        }

        public Order GetOrder(int orderKey)
        {
            try
            {
                Load(ref OrdersRoot, OrdersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement ord1 = (from or in OrdersRoot.Elements()
                             where int.Parse(or.Element("OrderKey").Value) == orderKey
                             select or).FirstOrDefault();
            if (ord1 == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת הזמנה עם מפתח זה");
            List<Order> ord = LoadFromXML<List<Order>>(OrdersPath);
            return ord.FirstOrDefault(x => x.orderKey == orderKey);
        }

        public DateTime GetEntryDate(int GuestRequestKey)
        {
            try
            {
                Load(ref OrdersRoot, OrdersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            DateTime EntryDate;
            EntryDate = (from req in GuestRequestsRoot.Elements()
                         where int.Parse(req.Element("GuestRequestKey").Value) == GuestRequestKey
                         select DateTime.Parse(req.Element("Dates").Element("Entry Date").Value)
                         ).FirstOrDefault();
            return EntryDate;
        }

        public DateTime GetRelease(int GuestRequestKey)
        {
            try
            {
                Load(ref OrdersRoot, OrdersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            DateTime Release;
            Release = (from req in GuestRequestsRoot.Elements()
                       where int.Parse(req.Element("GuestRequestKey").Value) == GuestRequestKey
                       select DateTime.Parse(req.Element("Dates").Element("ReleaseDate").Value)
                         ).FirstOrDefault();
            return Release;
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                Load(ref OrdersRoot, OrdersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            List<Order> orders = new List<Order>();
            XElement temp = (from t in OrdersRoot.Elements()
                             select t).FirstOrDefault();
            if (temp != null)
            {
                orders = LoadFromXML<List<Order>>(OrdersPath);
            }
            return orders.Select(hu => (Order)hu.Clone()).ToList();
        }

        #endregion


        #region Host
        public Host GetHost(int hostKey)
        {
            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("Owner").Element("HostKey").Value) == hostKey
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("לא קיים במערכת מארח עם מפתח שמספרו" + hostKey);
            List<HostingUnit> hostunits = LoadFromXML<List<HostingUnit>>(HostingUnitsPath);
            Host host = new Host();
            foreach (var item in hostunits)
            {
                if (item.owner.hostKey == hostKey)
                { host = item.owner; break; }

            }
            return host.Clone();

        }
        public void UpdateHost(Host host)
        {
            XElement host1;
            host1 = (from h in HostingUnitsRoot.Elements()
                     where int.Parse(h.Element("Owner").Element("HostKey").Value) == host.hostKey
                     select h).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException(" לא קיים במערכת מארח שמספרו" + host.hostKey);
            HostingUnit hos = new HostingUnit();
            hos.owner = host;
            AddHostUnit(hos);
        }

        #endregion


        #region Others

        /// <summary>
        /// returns list of Israel branchBanks
        /// </summary>
        /// <returns>list of banks</returns>
        public List<BankBranch> ListBankBranches()
        {
            List<BankBranch> branches = new List<BankBranch>();


            if (isFileLoaded)
            {
                branches = (from br in BankBranchesRoot.Elements()
                            select new BankBranch()
                            {
                                bankNumber = int.Parse(br.Element("קוד_בנק").Value),
                                bankName = br.Element("שם_בנק").Value,
                                branchNumber = int.Parse(br.Element("קוד_סניף").Value),
                                branchAddress = br.Element("כתובת_ה-ATM").Value,
                                branchCity = br.Element("ישוב").Value,
                            }).ToList();
                return branches;
            }
            throw new DirectoryNotFoundException("ישנה בעיה בטעינת הנתונים, נא נסה במועד מאוחר יותר");
        }

        public static void saveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }

        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }

        public string GetFromConfig(string s)
        {
            return ConfigRoot.Element(s).Value;
        }

        #endregion
    }
}
