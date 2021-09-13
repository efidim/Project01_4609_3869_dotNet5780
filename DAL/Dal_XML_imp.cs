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
        private XElement guestRequestsRoot;
        private const string guestRequestsPath = @"..\..\..\XML_Files\GuestRequests.xml";

        private XElement hostingUnitsRoot;
        private const string hostingUnitsPath = @"..\..\..\XML_Files\HostingUnits.xml";

        private XElement ordersRoot;
        private const string ordersPath = @"..\..\..\XML_Files\Orders.xml";

        private XElement bankBranchesRoot;
        private string bankBranchesPath = @"..\..\..\XML_Files\atm.xml";

        private XElement configRoot;
        private const string configPath = @"..\..\..\XML_Files\Config.xml";

        public bool isFileLoaded;

        protected Dal_XML_imp()
        {
            // GuestRequests loading
            if (!File.Exists(guestRequestsPath))
            {
                guestRequestsRoot = new XElement("GuestRequests");
                guestRequestsRoot.Save(guestRequestsPath);
            }
            else
            {
                Load(ref guestRequestsRoot, guestRequestsPath);
            }


            // HostingUnits Loading
            if (!File.Exists(hostingUnitsPath))
            {
                hostingUnitsRoot = new XElement("HostingUnits");
                hostingUnitsRoot.Save(hostingUnitsPath);
            }
            else
            {
                Load(ref hostingUnitsRoot, hostingUnitsPath);
            }

            // Orders Loading
            if (!File.Exists(ordersPath))
            {
                ordersRoot = new XElement("Orders");
                ordersRoot.Save(ordersPath);
            }
            else
            {
                Load(ref ordersRoot, ordersPath);
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
                    Load(ref bankBranchesRoot, bankBranchesPath);
                    isFileLoaded = true;
                }

            }).Start();


            // Config Loading
            if (!File.Exists(configPath))
            {
                configRoot = new XElement("Config");
                configRoot.Save(configPath);
            }
            else
            {
                Load(ref configRoot, configPath);
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
                throw new DirectoryNotFoundException("Error! File Not Found: " + a);
            }
        }


        #region Guest Request
        public void AddGuestRequest(GuestRequest guest)
        {
            try
            {
                Load(ref guestRequestsRoot, guestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            int key = int.Parse(configRoot.Element("guestKey").Value) + 1;
            XElement guestKey = configRoot;
            guestKey.Element("guestKey").Value = key.ToString();
            configRoot.Save(configPath);

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

            guestRequestsRoot.Add(GuestRequest);
            guestRequestsRoot.Save(guestRequestsPath);
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            try
            {
                Load(ref guestRequestsRoot, guestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement requestElement = (from req in guestRequestsRoot.Elements()
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

            guestRequestsRoot.Save(guestRequestsPath);
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
                Load(ref guestRequestsRoot, guestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            GuestRequest guest;

            guest = (from req in guestRequestsRoot.Elements()
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
                throw new KeyNotFoundException("Error! Request with specific key not found");

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
                Load(ref guestRequestsRoot, guestRequestsPath);
                XElement guest;
                guest = (from req in guestRequestsRoot.Elements()
                         where int.Parse(req.Element("GuestRequestKey").Value) == keyRequest
                         select req).FirstOrDefault();

                if (guest == null)
                    throw new KeyNotFoundException("Error! Request with specific key not found");

                guest.Remove();
                guestRequestsRoot.Save(guestRequestsPath);
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
        public List<GuestRequest> GetAllRequests()
        {
            try
            {
                Load(ref guestRequestsRoot, guestRequestsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            List<GuestRequest> requests = new List<GuestRequest>();

            try
            {
                requests = (from req in guestRequestsRoot.Elements()
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
                Load(ref hostingUnitsRoot, hostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in hostingUnitsRoot.Elements()
                     where hos.Element("hostingUnitName").Value == host.hostingUnitName
                     select hos).FirstOrDefault();
            if (host1 != null)
            {
                throw new KeyNotFoundException("Error! This host unit already exists");
            }

            int key = int.Parse(configRoot.Element("unitKey").Value) + 1;
            host.hostingUnitKey = key;
            XElement HostingUnitKey = configRoot;
            HostingUnitKey.Element("unitKey").Value = key.ToString();
            configRoot.Save(configPath);

            List<HostingUnit> temp = new List<HostingUnit>();
            host1 = (from hos in hostingUnitsRoot.Elements()
                     select hos).FirstOrDefault();
            if (host1 != null)
            {
                temp = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
            }
            temp.Add(host);
            saveToXML<List<HostingUnit>>(temp, hostingUnitsPath);

            return key;
        }


        public void RemoveHostUnit(HostingUnit host)
        {
            try
            {
                Load(ref hostingUnitsRoot, hostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            try
            {
                XElement host1;
                host1 = (from hos in hostingUnitsRoot.Elements()
                         where int.Parse(hos.Element("hostingUnitKey").Value) == host.hostingUnitKey
                         select hos).FirstOrDefault();

                if (host1 == null)
                    throw new KeyNotFoundException("Error! The Host Unit with specific key not found");

                host1.Remove();
                hostingUnitsRoot.Save(hostingUnitsPath);
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
                Load(ref hostingUnitsRoot, hostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in hostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("hostingUnitKey").Value) == host.hostingUnitKey
                     select hos).FirstOrDefault();
            if (host1 == null)
            {
                AddHostUnit(host);
                return;
            }

            List<HostingUnit> temp = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
            temp.RemoveAll(x => x.hostingUnitKey == host.hostingUnitKey);
            temp.Add(host);
            saveToXML<List<HostingUnit>>(temp, hostingUnitsPath);
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
                Load(ref hostingUnitsRoot, hostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in hostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("hostingUnitKey").Value) == hostingUnitkey
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("Error! Host Unit with specific key not found");
            List<HostingUnit> hostunits = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
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
                Load(ref hostingUnitsRoot, hostingUnitsPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement host1;
            host1 = (from hos in hostingUnitsRoot.Elements()
                     where hos.Element("hostingUnitName").Value == hostingUnitName
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("Error! Host Unit with specific name not found");
            List<HostingUnit> HostUnits = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
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
            List<HostingUnit> hostingUnits = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
            if (hostingUnits == null)
                throw new KeyNotFoundException("There are no host units in the database");
            return hostingUnits.Select(hu => (HostingUnit)hu.Clone()).ToList();
        }

        #endregion


        #region Order
        public int AddOrder(Order ord)
        {
            try
            {
                Load(ref ordersRoot, ordersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement ord1 = (from or in ordersRoot.Elements()
                             where int.Parse(or.Element("HostingUnitKey").Value) == ord.hostingUnitKey &&
                             int.Parse(or.Element("GuestRequestKey").Value) == ord.guestRequestKey
                             select or).FirstOrDefault();
            if (ord1 != null)
            {
                throw new KeyNotFoundException("This order already exists");
            }
            int key = int.Parse(configRoot.Element("orderKey").Value) + 1;
            ord.orderKey = key;
            XElement orderKey = configRoot;
            orderKey.Element("orderKey").Value = key.ToString();
            configRoot.Save(configPath);

            List<Order> temp = new List<Order>();
            ord1 = (from or in ordersRoot.Elements()
                    select or).FirstOrDefault();
            if (ord1 != null)
            {
                temp = LoadFromXML<List<Order>>(ordersPath);
            }
            temp.Add(ord);
            saveToXML<List<Order>>(temp, ordersPath);

            return key;
        }

        public void UpdateOrder(Order ord)
        {
            try
            {
                Load(ref ordersRoot, ordersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement ord1 = (from or in ordersRoot.Elements()
                             where int.Parse(or.Element("orderKey").Value) == ord.orderKey
                             select or).FirstOrDefault();
            if (ord1 == null)
            {
                AddOrder(ord);
                return;
            }

            List<Order> temp = LoadFromXML<List<Order>>(ordersPath);
            temp.RemoveAll(x => x.orderKey == ord.orderKey);
            temp.Add(ord);
            saveToXML<List<Order>>(temp, ordersPath);
        }

        public Order GetOrder(int orderKey)
        {
            try
            {
                Load(ref ordersRoot, ordersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            XElement ord1 = (from or in ordersRoot.Elements()
                             where int.Parse(or.Element("orderKey").Value) == orderKey
                             select or).FirstOrDefault();
            if (ord1 == null)
                throw new KeyNotFoundException("Error! Order with specific key not found");
            List<Order> ord = LoadFromXML<List<Order>>(ordersPath);
            return ord.FirstOrDefault(x => x.orderKey == orderKey);
        }

        public DateTime GetEntry(int GuestRequestKey)
        {
            try
            {
                Load(ref ordersRoot, ordersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            DateTime EntryDate;
            EntryDate = (from req in guestRequestsRoot.Elements()
                         where int.Parse(req.Element("GuestRequestKey").Value) == GuestRequestKey
                         select DateTime.Parse(req.Element("Dates").Element("Entry Date").Value)
                         ).FirstOrDefault();
            return EntryDate;
        }

        public DateTime GetRelease(int GuestRequestKey)
        {
            try
            {
                Load(ref ordersRoot, ordersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            DateTime Release;
            Release = (from req in guestRequestsRoot.Elements()
                       where int.Parse(req.Element("GuestRequestKey").Value) == GuestRequestKey
                       select DateTime.Parse(req.Element("Dates").Element("ReleaseDate").Value)
                         ).FirstOrDefault();
            return Release;
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                Load(ref ordersRoot, ordersPath);
            }
            catch (DirectoryNotFoundException r)
            {
                throw r;
            }

            List<Order> orders = new List<Order>();
            XElement temp = (from t in ordersRoot.Elements()
                             select t).FirstOrDefault();
            if (temp != null)
            {
                orders = LoadFromXML<List<Order>>(ordersPath);
            }
            return orders.Select(hu => (Order)hu.Clone()).ToList();
        }

        #endregion


        #region Host
        public Host GetHost(int hostKey)
        {
            XElement host1;
            host1 = (from hos in hostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("owner").Element("hostKey").Value) == hostKey
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("There is no host with id: " + hostKey);
            List<HostingUnit> hostunits = LoadFromXML<List<HostingUnit>>(hostingUnitsPath);
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
            host1 = (from h in hostingUnitsRoot.Elements()
                     where int.Parse(h.Element("Owner").Element("HostKey").Value) == host.hostKey
                     select h).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("There is no host with id: " + host.hostKey);
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
                branches = (from br in bankBranchesRoot.Elements()
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
            throw new DirectoryNotFoundException("Data Loading Error!");
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
            return configRoot.Element(s).Value;
        }

        #endregion
    }
}
