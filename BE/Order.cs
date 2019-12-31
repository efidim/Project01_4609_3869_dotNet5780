using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Order
    {
        public int HostingUnitKey;
        public int GuestRequestKey;
        public int OrderKey;
        public int Status;
        public DateTime CreateDate;
        public DateTime OrderDate;
        public int CommissionPerDay;

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

            str += "Create Date: " + CreateDate +
                "\nOrder Date: " + OrderDate;

            return str;
        }
    }
}
