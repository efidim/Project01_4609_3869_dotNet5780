using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    class HostingUnit
    {
        public int HostingUnitKey;
        public Host Owner;
        public string HostingUnitName;
        public bool[,] Diary = new bool[12, 31];

        public override string ToString()
        {

            string str = "Hosting Unit Number-" + this.HostingUnitKey.ToString() +
                "\n Hosting Unit Name: " + HostingUnitName +
                "\n Owner: " + Owner +
                "\nList of occupied dates:\n\n";
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 31; j++)
                {

                    if ((i == 0) && (Diary[i, Math.Abs(j - 1)] == false) && (Diary[i, j] == true))
                    // month 12 beginning case
                    {
                        {
                            str += j + "/12-";
                        }
                    }
                    if ((j == 0) && (i != 0) && (Diary[Math.Abs(i - 1), 30] == false) && (Diary[i, 0] == true))
                    // day 31 beginning case
                    {
                        {
                            str += "31/" + (i - 1) + "-";
                        }
                    }
                    if ((j != 0) && (i != 0) && (Diary[i, Math.Abs(j - 1)] == false) && (Diary[i, j] == true))
                    // regular beginning case
                    {
                        str += j + "/" + i + "-";
                    }

                    if (j == 30) // end day 30 case
                    {

                        if (i == 11) // last day in the matrice case
                        {
                            str += 30 + "/" + 11 + " \n";
                        }

                        else if ((Diary[i, j] == true) && (Diary[(i + 1), 0] == false))
                        {
                            str += j + "/" + i + " \n";
                        }
                    }

                    if ((j != 30) && (Diary[i, j] == true) && (Diary[i, (j + 1)] == false))
                    // regular end case
                    {
                        if (i == 0)
                        {
                            str += j + "/12" + " \n";
                        }
                        if (j == 0)
                        {
                            str += "31/" + Math.Abs(i - 1) + " \n";
                        }
                        else if (i != 0)
                        {
                            str += j + "/" + i + " \n";
                        }
                    }
                }
            }
            return str;
        }
    }
}
