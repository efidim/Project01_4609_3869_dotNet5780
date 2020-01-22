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
    class Dal_XML_imp:IDAL
    {
        //Singelton
        static Dal_XML_imp instance = new Dal_XML_imp();
        public static Dal_XML_imp Instance { get { return instance; } }


        //Roots and paths of the files
        XElement GuestRequestsRoot;
        string GuestRequestsPath;

        XElement HostingUnitsRoot;
        string HostingUnitsPath;

        XElement OrdersRoot;
        string OrdersPath;

        /// <summary>
        /// C-tor 
        /// activates the config change tread and creates files if they dont exist
        /// </summary>
        private Dal_XML_imp()
        {
            Thread configUpdateTread = new Thread(CheckIfUpdated);
            configUpdateTread.Start();
            try
            {
                //giving the pathes to the strings (Path etc.)
                string str = Assembly.GetExecutingAssembly().Location;
                string localPath = System.IO.Path.GetDirectoryName(str);
                for (int i = 0; i < 3; i++)
                    localPath = Path.GetDirectoryName(localPath);

                GuestRequestsPath = localPath + @"\GuestRequestsXml.xml";
                HostingUnitsPath = localPath + @"\HostingUnitsXml.xml";
                OrdersPath = localPath + @"\OrdersXml.xml";
                
                //creation of the files
                if (!File.Exists(GuestRequestsPath))
                    CreateGuestRequestsFile();

                if (!File.Exists(HostingUnitsPath))
                    CreateHostingUnitsFile();

                if (!File.Exists(OrdersPath))
                    CreateOrderFile();
            }
            catch (FileLoadException a)//if couldn't create
            {
                throw a;
            }
        }
        /// <summary>
        /// Checking if the flag of change config is true 
        /// if it is, it will operate the configUpdate event
        /// if it is not, it will make the thread sleep for 1 second
        /// </summary>
        void CheckIfUpdated()
        {
            while (true)
            {
                if (updated)
                {
                    updated = false;
                    instance?.ConfigUpdated();
                }
                Thread.Sleep(1000);
            }
        }
        public void AddGuestRequest(GuestRequest guest)
        {
            throw new NotImplementedException();
        }

        public int AddHostUnit(HostingUnit host)
        {
            throw new NotImplementedException();
        }

        public int AddOrder(Order ord)
        {
            throw new NotImplementedException();
        }

        public List<GuestRequest> GetAllGuests()
        {
            throw new NotImplementedException();
        }

        public List<HostingUnit> GetAllHostingUnits()
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrders()
        {
            throw new NotImplementedException();
        }

        public DateTime GetEntryDate(int GuestRequestKey)
        {
            throw new NotImplementedException();
        }

        public Host GetHost(int hostKey)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnit(int hostingUnitkey)
        {
            throw new NotImplementedException();
        }

        public HostingUnit GetHostingUnitByName(string hostingUnitName)
        {
            throw new NotImplementedException();
        }

        public Order GetOrder(int orderKey)
        {
            throw new NotImplementedException();
        }

        public DateTime GetRelease(int GuestRequestKey)
        {
            throw new NotImplementedException();
        }

        public GuestRequest GetRequest(int keyRequest)
        {
            throw new NotImplementedException();
        }

        public List<BankBranch> ListBankBranches()
        {
            throw new NotImplementedException();
        }

        public void RemoveHostUnit(HostingUnit host)
        {
            throw new NotImplementedException();
        }

        public void UpdateGuestRequest(GuestRequest guest)
        {
            throw new NotImplementedException();
        }

        public void UpdateHost(Host host)
        {
            throw new NotImplementedException();
        }

        public void UpdateHostUnit(HostingUnit host)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order ord)
        {
            throw new NotImplementedException();
        }
    }
}
