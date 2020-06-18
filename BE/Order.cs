using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BE
{
    public class Order 
    {
        public int HostingUnitKey{ get; set; }
        public int GuestRequestKey{ get; set; }
        public int OrderKey{ get; set; }
        public int Status{ get; set; }
        public DateTime CreateDate{ get; set; }
        public DateTime OrderDate{ get; set; }
        public int CommissionPerDay{ get; set; }

        public override string ToString()
        {
            string str = "Hosting Unit Key: " + HostingUnitKey +
                "\nGuest Request Key: " + GuestRequestKey +
                "\nOrder Key: " + OrderKey;
            if (Status == 0)
                str += "\n Not yet addressed";
            if (Status == 1)
                str += "\nMail sent";
            if (Status == 2)
                str += "\nClosed due to customer unresponsiveness";
            if (Status == 3)
                str += "\nClosed with customer responsiveness";
            if (Status == 4)
                str += "\nClosed because expired date";

            str += "Create Date: " + CreateDate +
                "\nOrder Date: " + OrderDate;

            return str;
        }
    }
}
