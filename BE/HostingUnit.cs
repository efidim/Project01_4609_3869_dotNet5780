using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BE
{
    public class HostingUnit
    {
        public int HostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] Diary = new bool[12, 31];
        [XmlArray("Diary")]
        public string TempDiary
        {
            get
            {
                if (Diary == null)
                    return null;
                string result = "";
                if (Diary != null)
                {

                    int sizeA = Diary.GetLength(0);
                    int sizeB = Diary.GetLength(1);
                    result += "" + sizeA + "," + sizeB;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            result += "," + Diary[i, j];
                }
                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                string[] values = value.Split(',');
                
                Diary = new bool[12,31];
                int index = 2;
                for (int i = 0; i < 12; i++)
                    for (int j = 0; j < 31; j++)
                        Diary[i, j] = bool.Parse(values[index++]);
            }
        }
    }



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
            
            if (Diary[current.Month -1, current.Day -1]) // first day check
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

