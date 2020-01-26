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
        private const string GuestRequestsPath = @"..\..\..\xml files\GuestRequest.xml";

        private XElement HostingUnitsRoot;
        private const string HostingUnitsPath = @"..\..\..\xml files\HostingUnits.xml";

        private XElement OrdersRoot;
        private const string OrdersPath = @"..\..\..\xml files\Orders.xml";

        private XElement BankBranchesRoot;
        private string BankBranchesPath = @"..\..\..\xml files\BankBranchDetails.xml";

        private XElement ConfigRoot;
        private const string ConfigPath = @"..\..\..\xml files\Config.xml";

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
            new Thread((obj) =>
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
                }
                Load(ref BankBranchesRoot, BankBranchesPath);
                obj = true;
            }).Start(isFileLoaded);


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

            XElement GuestRequestKey = new XElement("GuestRequestKey", key);
            XElement PrivateName = new XElement("PrivateName", guest.PrivateName);
            XElement FamilyName = new XElement("FamilyName", guest.FamilyName);
            XElement MailAddress = new XElement("Mail Address", guest.MailAddress);
            XElement Name = new XElement("Name", PrivateName, FamilyName);
            XElement Status = new XElement("Status", guest.Status);
            XElement RegistrationDate = new XElement("Registration Date", guest.RegistrationDate);
            XElement EntryDate = new XElement("Entry Date", guest.EntryDate);
            XElement ReleaseDate = new XElement("Release Date", guest.ReleaseDate);
            XElement Dates = new XElement("Dates", RegistrationDate, EntryDate, ReleaseDate);
            XElement Area = new XElement("Area", guest.Area);
            XElement Type = new XElement("Type", guest.Type);
            XElement Adults = new XElement("Adults", guest.Adults);
            XElement Children = new XElement("Children", guest.Children);
            XElement Pool = new XElement("Pool", guest.Pool);
            XElement Jacuzzi = new XElement("Jacuzzi", guest.Jacuzzi);
            XElement ChildrenAttractions = new XElement("ChildrenAttractions", guest.ChildrenAttractions);
            XElement Attractions = new XElement("Attractions", ChildrenAttractions, Jacuzzi, Pool);

            GuestRequestsRoot.Add("Guest Request", GuestRequestKey,Name, MailAddress,
                Status, Dates, Area, Type, Adults,Attractions);
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
            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("HostingUnitKey").Value) == host.HostingUnitKey
                     select hos).FirstOrDefault();
            if (host1 != null)
            {
                throw new KeyNotFoundException("יחידת אירוח זו קיימת כבר במערכת");
            }

            int key = int.Parse(ConfigRoot.Element("HostingUnitKey").Value) + 1;
            XElement HostingUnitKey = ConfigRoot;
            HostingUnitKey.Element("HostingUnitKey").Value = key.ToString();
        
            HostingUnitsRoot.Add(host);
            saveToXML<HostingUnit>(host, HostingUnitsPath);

            return key;
        }    


        public void RemoveHostUnit(HostingUnit host)
        {  
            try
            {
                XElement host1; 
                host1 = (from hos in HostingUnitsRoot.Elements()
                         where int.Parse(hos.Element("HostingUnitKey").Value) == host.HostingUnitKey
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
            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("HostingUnitKey").Value) == host.HostingUnitKey
                     select hos).FirstOrDefault();
            if (host1 == null)
            {
                AddHostUnit(host);
                return;
            }
            host1.Remove();
            List<HostingUnit> HostUnits = LoadListFromXML(HostingUnitsPath);
            HostUnits.Add(host);
            saveToXML<HostingUnit>(host, HostingUnitsPath);
        }

        /// <summary>
        ///  Returns a hosting unit by the ID
        /// </summary>
        /// <param name="hostingUnitkey">ID's unit</param>
        /// <returns>HostingUnit</returns>
        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
           
            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("HostingUnitKey").Value) == hostingUnitkey
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת יחידה עם מפתח זה");
           List <HostingUnit> hostunits = LoadListFromXML(HostingUnitsPath);
            HostingUnit host = new HostingUnit();
            foreach (var item in hostunits)
            {
                if (item.HostingUnitKey == hostingUnitkey)
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
            XElement host1;
            host1 = (from hos in HostingUnitsRoot.Elements()
                     where hos.Element("HostingUnitName").Value == hostingUnitName
                     select hos).FirstOrDefault();
            if (host1 == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת יחידה עם שם זה");
            List<HostingUnit> HostUnits = LoadListFromXML(HostingUnitsPath);
            HostingUnit host=new HostingUnit();
            foreach (var item in HostUnits)
            {
                if (item.HostingUnitName == hostingUnitName)
                { host = item.Clone(); break; }

            }
            return host.Clone();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            List<HostingUnit> hostingUnits = LoadListFromXML(HostingUnitsPath);
            if (hostingUnits == null)
                throw new KeyNotFoundException("אין יחידות אירוח במאגר הנתונים");
            return hostingUnits.Select(hu => (HostingUnit)hu.Clone()).ToList();
        }
        public static List<HostingUnit> LoadListFromXML(string path)
            {
                FileStream file = new FileStream(path, FileMode.Open);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HostingUnit>));
                List<HostingUnit> list = (List<HostingUnit>)xmlSerializer.Deserialize(file);
                file.Close();
                return list;
            }
        
        #endregion
    

        #region Order
        public int AddOrder(Order ord)
        {
            XElement ord1 = (from hos in HostingUnitsRoot.Elements()
                     where int.Parse(hos.Element("OrderKey").Value) == ord.OrderKey
                     select hos).FirstOrDefault();
            if (ord1 != null)
            {
                throw new KeyNotFoundException("הזמנה זו קיימת כבר במערכת");
            }
            int key = int.Parse(ConfigRoot.Element("orderKey").Value) + 1;
            XElement orderKey = ConfigRoot;
            orderKey.Element("orderKey").Value = key.ToString();
            OrdersRoot.Add(ord);
            saveToXML<Order>(ord, OrdersPath);

            return key;
        }
        public void UpdateOrder(Order ord)
        {
            
           XElement ord1 = (from or in HostingUnitsRoot.Elements()
                     where int.Parse(or.Element("orderKey").Value) == ord.OrderKey
                     select or).FirstOrDefault();
            if (ord1 == null)
            {
                AddOrder(ord);
                return;
            }
            ord1.Remove();
            OrdersRoot.Add(ord);
            saveToXML<Order>(ord, HostingUnitsPath);
        }
        public Order GetOrder(int orderKey)
        {
            XElement ord1 = (from or in HostingUnitsRoot.Elements()
                             where int.Parse(or.Element("orderKey").Value) == orderKey
                             select or).FirstOrDefault();
            if (ord1 == null)
                throw new KeyNotFoundException("שגיאה! לא קיימת במערכת הזמנה עם מפתח זה");
            List<Order> ord = LoadListFromXML1(OrdersPath);
            return ord.FirstOrDefault(x => x.OrderKey==orderKey);
        }
        public DateTime GetEntryDate(int GuestRequestKey)
        {
            //int index = DataSource.GuestRequests.FindIndex(o => o.GuestRequestKey == GuestRequestKey);
            //return DataSource.GuestRequests[index].EntryDate;
            DateTime EntryDate;
            EntryDate = (from req in GuestRequestsRoot.Elements()
                         where int.Parse(req.Element("GuestRequestKey").Value) == GuestRequestKey
                         select DateTime.Parse(req.Element("Dates").Element("Entry Date").Value)
                         ).FirstOrDefault();
            return EntryDate;
         }
        public DateTime GetRelease(int GuestRequestKey)
        {
            DateTime Release;
            Release = (from req in GuestRequestsRoot.Elements()
                        where int.Parse(req.Element("GuestRequestKey").Value) == GuestRequestKey
                       select DateTime.Parse(req.Element("Dates").Element("ReleaseDate").Value)
                         ).FirstOrDefault();
            return Release;
        }
        public List<Order> GetAllOrders()
        {
         
            List<Order> hostingUnits = LoadListFromXML1(OrdersPath);
            if (hostingUnits == null)
                throw new KeyNotFoundException("אין הזמנות במאגר הנתונים");
            return hostingUnits.Select(hu => (Order)hu.Clone()).ToList();
        }
        public static List<Order> LoadListFromXML1(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<HostingUnit>));
            List<Order> list = (List<Order>)xmlSerializer.Deserialize(file);
            file.Close();
            return list;
        }
        #endregion


        #region Host
        public Host GetHost(int hostKey)
        {
            //int index = DataSource.HostingUnits.FindIndex(h => h.Owner.HostKey == hostKey);
            //if (index == -1)
            //    throw new Exception("The host does not exist");
            //return DataSource.HostingUnits[index].Owner;
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
        /// returns list of Israel branchBanks
        /// </summary>
        /// <returns>list of banks</returns>
        public List<BankBranch> ListBankBranches()
        {
            List<BankBranch> branches = new List<BankBranch>();
            try
            {
                if (isFileLoaded)
                {
                    branches = (from bra in BankBranchesRoot.Elements()
                                select new BankBranch()
                                {
                                    BankNumber = int.Parse(bra.Element("קוד_בנק").Value),
                                    BankName = bra.Element("שם_בנק").Value,
                                    BranchNumber = int.Parse(bra.Element("קוד_סניף").Value),
                                    BranchAddress = bra.Element("כתובת_ה-ATM").Value,
                                    BranchCity = bra.Element("ישוב").Value,
                                }).ToList();
                }
            }
            catch
            {
                branches = null;
            }
            return branches;
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

    }
}
