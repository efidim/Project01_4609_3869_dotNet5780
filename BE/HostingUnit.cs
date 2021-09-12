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
        public Host owner { get; set; }
        public string hostingUnitName { get; set; }
        [XmlIgnore]
        public bool[,] diary = new bool[12, 31];
        [XmlArray("Diary")]
        public bool[] BoardDto
        {
            get { return diary.Flatten(); }
            set { diary = value.Expand(12); }
        }

        public string area { get; set; }
        public string subArea { get; set; }
        public string type { get; set; }
        public int adults { get; set; }
        public int children { get; set; }
        public bool pool { get; set; }
        public bool jacuzzi { get; set; }
        public bool childrenAttractions { get; set; }


        public override string ToString()
        {

            string str = "Hosting Unit Number:" + this.hostingUnitKey.ToString() +
                "\nHosting Unit Name: " + hostingUnitName +
                "\nArea: " + area +
                "\nType: " + type +
                "\nAdults: " + adults +
                "\nChildren: " + children +
                "\nPool: " + pool +
                "\nJacuzzi: " + jacuzzi +
                "\nChildrenAttractions: " + childrenAttractions +
                "\nOwner:\n " + owner +
                "\nList of occupied dates:\n";

            DateTime current = DateTime.Today.AddMonths(-1);
            // intialization to the matrice first day

            if (diary[current.Month - 1, current.Day - 1]) // first day check
                str += current.Day + "/" + current.Month + "-";

            current = current.AddDays(1);

            for (; current < DateTime.Today.AddYears(1).AddMonths(-1).AddDays(-1); current = current.AddDays(1))
            {
                if (diary[current.Month - 1, current.Day - 1] &&
                    !diary[current.AddDays(-1).Month - 1, current.AddDays(-1).Day - 1])
                    str += current.Day + "/" + current.Month + "-";

                if (diary[current.Month - 1, current.Day - 1] &&
                    !diary[current.AddDays(-1).Month - 1, current.AddDays(1).Day - 1])
                    str += current.Day + "/" + current.Month + "\n";
            }

            current = current.AddDays(1); // last day check
            if (diary[current.Month - 1, current.Day - 1])
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

