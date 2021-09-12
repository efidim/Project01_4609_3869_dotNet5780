using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest:IEnumerable
    {
        public int guestRequestKey{get ; set;}
        public string privateName{get ; set;}
        public string familyName{get ; set;}
        public string mailAddress { get; set; }
        public bool status{get ; set;}
        public DateTime registrationDate{get ; set;}
        public DateTime entryDate{get ; set;}
        public DateTime releaseDate{get ; set;}
        public string area{get ; set;}
        public string type{get ; set;}
        public int adults{get ; set;}
        public int children{get ; set;}
        public int pool{get ; set;}
        public int jacuzzi{get ; set;}
        public int childrenAttractions{get ; set;}

        public override string ToString()
        {
            string str = "Guest Request Key: " + guestRequestKey +
                "\n Private Name: " + privateName +
                "\n Family Name: " + familyName +
                "\n Mail Address: " + mailAddress;

            string temp;
            if (status)
                temp = "Active";
            else
                temp = "Inactive";
            str += "\n Status? " + temp +
                "\n RegistrationDate: " + registrationDate +
                "\n EntryDate: " + entryDate +
                "\n ReleaseDate: " + releaseDate +
                "\n Area: " + area +
                "\n Type: " + type +
                "\n Adults: " + adults +
                "\n Children: " + children;

            if (pool == -1)
                temp = "Not interested";
            else if (pool == 0)
                temp = "Possible";
            else if (pool == 1)
                temp = "Necessary";
            str += "\n Pool? " + temp;

            if (jacuzzi == -1)
                temp = "Not interested";
            else if (jacuzzi == 0)
                temp = "Possible";
            else if (jacuzzi == 1)
                temp = "Necessary";
            str += "\n Jacuzzi? " + temp;

            if (childrenAttractions == -1)
                temp = "Not interested";
            else if (childrenAttractions == 0)
                temp = "Possible";
            else if (childrenAttractions == 1)
                temp = "Necessary";

            str += "\n Children Attractions: " + temp;

            return str;
        }               
    }

    public interface IEnumerable
    { 

    }
}
