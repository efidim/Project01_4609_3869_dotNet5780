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
        static void Main(string[] args)
        {
            IBL bl = FactoryBl.getBl();


            #region GuestRequest testing

            //GuestRequest req = new GuestRequest();
            //req.PrivateName = "efi";
            //req.FamilyName = "dim";
            //req.MailAddress = "efi@org.zehut.il";
            //req.Status = true;
            //req.RegistrationDate = DateTime.Now;
            //req.EntryDate = new DateTime(2019, 12, 02);
            //req.ReleaseDate = new DateTime(2019, 12, 03);
            //req.Area = "Tel Aviv";
            //req.subArea = "Habima";
            //req.Adults = 3;
            //req.Children = 4;
            //req.Jacuzzi = 1;
            //req.Pool = 1;
            //req.Type = "zimmer";
            //req.ChildrenAttractions = 0;

            //// exception testing
            ////req.EntryDate = new DateTime(2019, 12, 03);     
            ////req.ReleaseDate = new DateTime(2019, 12, 02);

            //try
            //{
            //    bl.AddGuestRequest(req);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("ERROR: \n" + ex.Message);
            //}

            ////req.GuestRequestKey = 10000009;
            //req.GuestRequestKey = 10000007;
            //req.Adults = 4;
            //bl.UpdateGuestRequest(req);

            //GuestRequest temp = bl.GetRequest(10000009);
            ////Console.WriteLine(temp.ToString());

            ////List<GuestRequest> temp1 = bl.GetAllGuests();
            ////foreach (var item in temp1)
            ////{
            ////    Console.WriteLine(item.ToString() + "\n");
            ////}

            ////IEnumerable<IGrouping<string, GuestRequest>> temp2 = bl.RequestsByArea();

            ////foreach(IGrouping<string, GuestRequest> g in temp2)
            ////{
            ////    switch (g.Key)
            ////    {
            ////        case "Jerusalem":
            ////            Console.WriteLine("Jerusalem: \n");
            ////            foreach (var item in g)
            ////            {
            ////                Console.WriteLine("{0} ", item + "\n");
            ////            }
            ////            Console.WriteLine("\n");
            ////            break;

            ////        case "Tel Aviv":
            ////            Console.WriteLine("Tel Aviv: \n");
            ////            foreach (var item in g)
            ////            {
            ////                Console.WriteLine("{0} ", item + "\n");
            ////            }
            ////            Console.WriteLine("\n");
            ////            break;
            ////    }
            ////}



            ////IEnumerable<IGrouping<int, GuestRequest>> temp3 = bl.RequestsByGuests();

            ////foreach (IGrouping<int, GuestRequest> g in temp3)
            ////{
            ////    switch (g.Key)
            ////    {
            ////        case 7:
            ////            Console.WriteLine("Guests: 7 \n");
            ////            foreach (var item in g)
            ////            {
            ////                Console.WriteLine("{0} ", item + "\n");
            ////            }
            ////            Console.WriteLine("\n");
            ////            break;

            ////        case 8:
            ////            Console.WriteLine("Guests: 8 \n");
            ////            foreach (var item in g)
            ////            {
            ////                Console.WriteLine("{0} ", item + "\n");
            ////            }
            ////            Console.WriteLine("\n");
            ////            break;
            ////    }
            ////}



            ////IEnumerable<GuestRequest> temp4 = bl.RequestsByCondition(x => x.PrivateName == "efi");
            ////foreach (var item in temp4)
            ////    Console.WriteLine(item.ToString() + "\n");

            #endregion

            #region HostinUnit testing

            //HostingUnit hos = new HostingUnit()
            //{
            //    HostingUnitKey = 469834,
            //    HostingUnitName = "Tsameret",
            //    Owner = new Host()
            //    {
            //        HostKey = 332484609,
            //        PrivateName = "efi",
            //        FamilyName = "dim",
            //        PhoneNumber = "054-1234567",
            //        MailAddress = "ef@org.zehut.il",
            //        BankBranchDetails = new BankBranch()
            //        {
            //            BankNumber = 2,
            //            BankName = "MyBank",
            //            BranchNumber = 11,
            //            BranchAddress = "MyBank@gmail.com",
            //            BranchCity = "Tel Aviv"
            //        },
            //        BankAccountNumber = 111,
            //        CollectionClearance = true
            //    },
            //    Area = "Tel Aviv",
            //    subArea = "Giloh",
            //    Adults = 3,
            //    Children = 5,
            //    Jacuzzi = 1,
            //    Pool = 1,
            //    Type = "zimmer",
            //    ChildrenAttractions = 0
            //};

            //bl.AddHostUnit(hos);


            ////bl.RemoveHostUnit(hos);
            ////hos.HostingUnitKey = 333333;

            ////try
            ////{
            ////    bl.RemoveHostUnit(hos);
            ////}
            ////catch (Exception ex)
            ////{
            ////    Console.WriteLine(ex.Message + "\n");
            ////}


            //hos.Pool = 0;
            //bl.UpdateHostUnit(hos);

            //Order ord_temp = new Order()
            //{
            //    GuestRequestKey = 4589,
            //    HostingUnitKey = 469834,
            //    Status = 1,
            //};
            //bl.SetDiary(ord_temp);
            //HostingUnit temp = bl.GetHostingUnit(hos.HostingUnitKey);
            //Console.WriteLine(temp.ToString() + "\n");

            //Console.WriteLine(bl.OrdersByUnit(hos) + "\n");
            //DateTime temp_entry = new DateTime(2020, 3, 3);
            //Console.WriteLine(bl.IsItAvailaible(hos, temp_entry, 3) + "\n");


            //HostingUnit hos1 = new HostingUnit()
            //{
            //    HostingUnitName = "Tsa",
            //    Owner = new Host()
            //    {
            //        HostKey = 332484609,
            //        PrivateName = "efi",
            //        FamilyName = "dim",
            //        PhoneNumber = "054-1234567",
            //        MailAddress = "ef@org.zehut.il",
            //        BankBranchDetails = new BankBranch()
            //        {
            //            BankNumber = 2,
            //            BankName = "MyBank",
            //            BranchNumber = 11,
            //            BranchAddress = "MyBank@gmail.com",
            //            BranchCity = "Tel Aviv"
            //        },
            //        BankAccountNumber = 111,
            //        CollectionClearance = true
            //    },
            //    Area = "Jerusalem",
            //    subArea = "Giloh",
            //    Adults = 3,
            //    Children = 5,
            //    Jacuzzi = 1,
            //    Pool = 1,
            //    Type = "zimmer",
            //    ChildrenAttractions = 0
            //};

            //bl.AddHostUnit(hos1);

            //IEnumerable<IGrouping<string, HostingUnit>> temp10 = bl.UnitsByArea();

            //foreach (IGrouping<string, HostingUnit> g in temp10)
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

            //List<HostingUnit> temp11 = bl.AvailableUnits(temp_entry, 3);
            //foreach (var item in temp11)
            //{
            //    Console.WriteLine(item.ToString() + "\n");
            //}
            //Console.WriteLine("\n");

            #endregion

            #region Order testing

            Order ord = new Order();
            ord.GuestRequestKey = 4589;
            ord.HostingUnitKey = 469834;
            ord.OrderKey = 3124;
            ord.Status = 0;
            ord.CreateDate = DateTime.Now;
            ord.OrderDate = new DateTime(2019, 12, 02);
            ord.CommissionPerDay = 5;
            try
            {
                bl.AddOrder(ord);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: \n" + ex.Message);
            }
            // exception testing
            // ord.GuestRequestKey = 222;
            // try
            //{
            //      bl.AddOrder(ord);
            // }
            //  catch (Exception ex)
            //  {
            //      Console.WriteLine("ERROR: \n" + ex.Message);
            // }
            ord.Status = 1;
            try
            {
                bl.UpdateOrder(ord);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: \n" + ex.Message);
            }

            List<Order> temp7 = bl.GetAllOrders();
            foreach (var item in temp7)
            {
                Console.WriteLine(item.ToString() + "\n");
            }

            //  int temp8;
            //  Console.WriteLine(temp8 = bl.OrdersByRequest(req));
            #endregion

            #region Others testing

            //DateTime temp12 = new DateTime(2019, 12, 4);
            //DateTime temp13 = new DateTime(2019, 12, 6);
            //Console.WriteLine(bl.DifferenceDays(temp12, temp13) + "\n");


            //List<BankBranch> temp14 = bl.ListBankBranches();
            //foreach (var item in temp14)
            //{
            //    Console.WriteLine(item.ToString() + "\n");
            //}
            //Console.WriteLine("\n");
            #endregion

            Console.ReadLine();
        }
    }
}
