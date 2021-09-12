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
        public int hostingUnitKey { get; set; }
        public Host Owner { get; set; }
        public string HostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] Diary = new bool[12, 31];
        [XmlArray("Diary")]
        public bool[] BoardDto
        {
            get { return Diary.Flatten(); }
            set { Diary = value.Expand(12); }
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

            string str = "Hosting Unit Number:" + this.hostingUnitKey.ToString() +
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

            if (Diary[current.Month - 1, current.Day - 1]) // first day check
                str += current.Day + "/" + current.Month + "-";

            current = current.AddDays(1);

            for (; current < DateTime.Today.AddYears(1).AddMonths(-1).AddDays(-1); current = current.AddDays(1))
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

    public static class Tools
    {
        public static T[] Flatten<T>(this T[,] arr)
        {
            int k = 0;
            int rows = arr.GetLength(0);
            int columns = arr.GetLength(1);
            T[] arrFlattened = new T[rows * columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrFlattened[k++] = arr[j, i];
                }
            }
            return arrFlattened;
        }
        public static T[,] Expand<T>(this T[] arr, int rows)
        {
            int k = 0;
            int length = arr.GetLength(0);
            int columns = length / rows;
            T[,] arrExpanded = new T[rows, columns];
            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < columns; i++)
                {
                    arrExpanded[j, i] = arr[k++];
                }
            }
            return arrExpanded;
        }
    }
}

