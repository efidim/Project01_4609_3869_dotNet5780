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
        public int Commission;

        public override string ToString()
        {
            string str = "Hosting Unit Key: " + HostingUnitKey +
                "\n Guest Request Key: " + GuestRequestKey +
                "\n Order Key: " + OrderKey;
            if (Status == 0)
                str += "\n Not yet addressed";
            if (Status == 1)
                str += "\n Mail sent";
            if (Status == 2)
                str += "\n Closed due to customer unresponsiveness";
            if (Status == 3)
                str += "\n Closed with customer responsiveness";

            str += "Create Date: " + CreateDate +
                "\n Order Date: " + OrderDate;

            return str;
        }
    }
}
