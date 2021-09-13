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
        public int hostingUnitKey{ get; set; }
        public int guestRequestKey{ get; set; }
        public int orderKey{ get; set; }
        public int status{ get; set; }
        public DateTime createDate{ get; set; }
        public DateTime orderDate{ get; set; }
        public int commissionPerDay{ get; set; }

        public override string ToString()
        {
            string str = "Hosting Unit Key: " + hostingUnitKey +
                "\nGuest Request Key: " + guestRequestKey +
                "\nOrder Key: " + orderKey;
            if (status == 0)
                str += "\n Not yet addressed";
            if (status == 1)
                str += "\nMail sent";
            if (status == 2)
                str += "\nClosed due to customer unresponsiveness";
            if (status == 3)
                str += "\nClosed with customer responsiveness";
            if (status == 4)
                str += "\nClosed because expired date";

            str += "Create Date: " + createDate +
                "\nOrder Date: " + orderDate;

            return str;
        }
    }
}
