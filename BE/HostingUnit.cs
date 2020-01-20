using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class HostingUnit
    {
        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }

        public bool[,] Diary = new bool[12, 31];

        public string Area { get; set; }
        public string subArea { get; set; }
        public string Type { get; set; }
        public int Adults { get; set; }
        public int Children { get; set; }
        public bool Pool { get; set; }
        public bool Jacuzzi { get; set; }
        public bool ChildrenAttractions { get; set; }


        public override string ToString()
        {

            string str = "Hosting Unit Number:" + this.HostingUnitKey.ToString() +
                "\nHosting Unit Name: " + HostingUnitName +
                "\nArea: " + Area +
                "\nType: " + Type +
                "\nAdults: " + Adults +
                "\nChildren: " + Children +
                "\nPool: " + Pool +
                "\nJacuzzi: " + Jacuzzi +
                "\nChildrenAttractions: " + ChildrenAttractions +
                "\nOwner:\n " + Owner +
                "\nList of occupied dates:\n";

            DateTime current = DateTime.Today.AddMonths(-1); 
            // intialization to the matrice first day
            
            if (Diary[current.Month, current.Day]) // first day check
                str += current.Day + "/" + current.Month + "-";

            current = current.AddDays(1);

            for (;current < DateTime.Today.AddYears(1).AddMonths(-1).AddDays(-1); current = current.AddDays(1))
            {
                if (Diary[current.Month - 1, current.Day - 1] &&
                    !Diary[current.AddDays(-1).Month - 1, current.AddDays(-1).Day - 1])
                    str += current.Day + "/" + current.Month + "-";

                if (Diary[current.Month - 1, current.Day - 1] &&
                    !Diary[current.AddDays(-1).Month - 1, current.AddDays(1).Day - 1])
                    str += current.Day + "/" + current.Month + "\n";
            }

            current = current.AddDays(1); // last day check
            if (Diary[current.Month - 1, current.Day - 1])
                str += current.Day + "/" + current.Month + "\n";

            return str;
        }
    }
}
