using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {
        //static IBL bl;
        //static Program()
        //{
        //    IBL bl = FactoryBl.getBl();
        //}
        static void Main(string[] args)
        {
            IBL bl = FactoryBl.getBl();

            #region GuestRequest testing
            GuestRequest req = new GuestRequest();
            req.PrivateName = "efi";
            req.FamilyName = "dim";
            req.MailAddress = "efi@org.zehut.il";
            req.Status = true;
            req.RegistrationDate = DateTime.Now;
            req.EntryDate = new DateTime(2019, 12, 02);
            req.ReleaseDate = new DateTime(2019, 12, 03);
            req.Area = "Tel Aviv";
            req.subArea = "Habima";
            req.Adults = 3;
            req.Children = 4;
            req.Jacuzzi = 1;
            req.Pool = 1;
            req.Type = "zimmer";
            req.ChildrenAttractions = 0;

            // exception testing
            //req.EntryDate = new DateTime(2019, 12, 03);     
            //req.ReleaseDate = new DateTime(2019, 12, 02);

            try
            {
                bl.AddGuestRequest(req);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: \n" + ex.Message);
            }

            //req.GuestRequestKey = 10000009;
            req.GuestRequestKey = 10000007;
            req.Adults = 4;
            bl.UpdateGuestRequest(req);

            GuestRequest temp = bl.GetRequest(10000009);
            //Console.WriteLine(temp.ToString());

            //List<GuestRequest> temp1 = bl.GetAllGuests();
            //foreach (var item in temp1)
            //{
            //    Console.WriteLine(item.ToString() + "\n");
            //}

            //IEnumerable<IGrouping<string, GuestRequest>> temp2 = bl.RequestsByArea();

            //foreach(IGrouping<string, GuestRequest> g in temp2)
            //{
            //    switch (g.Key)
            //    {
            //        case "Jerusalem":
            //            Console.WriteLine("Jerusalem: \n");
            //            foreach (var item in g)
            //            {
            //                Console.WriteLine("{0} ", item + "\n");
            //            }
            //            Console.WriteLine("\n");
            //            break;

            //        case "Tel Aviv":
            //            Console.WriteLine("Tel Aviv: \n");
            //            foreach (var item in g)
            //            {
            //                Console.WriteLine("{0} ", item + "\n");
            //            }
            //            Console.WriteLine("\n");
            //            break;
            //    }
            //}



            //IEnumerable<IGrouping<int, GuestRequest>> temp3 = bl.RequestsByGuests();

            //foreach (IGrouping<int, GuestRequest> g in temp3)
            //{
            //    switch (g.Key)
            //    {
            //        case 7:
            //            Console.WriteLine("Guests: 7 \n");
            //            foreach (var item in g)
            //            {
            //                Console.WriteLine("{0} ", item + "\n");
            //            }
            //            Console.WriteLine("\n");
            //            break;

            //        case 8:
            //            Console.WriteLine("Guests: 8 \n");
            //            foreach (var item in g)
            //            {
            //                Console.WriteLine("{0} ", item + "\n");
            //            }
            //            Console.WriteLine("\n");
            //            break;
            //    }
            //}



            //IEnumerable<GuestRequest> temp4 = bl.RequestsByCondition(x => x.PrivateName == "efi");
            //foreach (var item in temp4)
            //    Console.WriteLine(item.ToString() + "\n");

            #endregion

            #region HostinUnit testing


            #endregion

        }
    }
}
