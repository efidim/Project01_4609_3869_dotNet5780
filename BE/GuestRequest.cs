using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest:IEnumerable
    {
        public int GuestRequestKey{get ; set;}
        public string PrivateName{get ; set;}
        public string FamilyName{get ; set;}
        public string MailAddress { get; set; }
        public bool Status{get ; set;}
        public DateTime RegistrationDate{get ; set;}
        public DateTime EntryDate{get ; set;}
        public DateTime ReleaseDate{get ; set;}
        public string Area{get ; set;}
        public string Type{get ; set;}
        public int Adults{get ; set;}
        public int Children{get ; set;}
        public int Pool{get ; set;}
        public int Jacuzzi{get ; set;}
        public int ChildrenAttractions{get ; set;}

        public override string ToString()
        {
            string str = "Guest Request Key: " + GuestRequestKey +
                "\n Private Name: " + PrivateName +
                "\n Family Name: " + FamilyName +
                "\n Mail Address: " + MailAddress;

            string temp;
            if (Status)
                temp = "Active";
            else
                temp = "Inactive";
            str += "\n Status? " + temp +
                "\n RegistrationDate: " + RegistrationDate +
                "\n EntryDate: " + EntryDate +
                "\n ReleaseDate: " + ReleaseDate +
                "\n Area: " + Area +
                "\n Type: " + Type +
                "\n Adults: " + Adults +
                "\n Children: " + Children;

            if (Pool == -1)
                temp = "Not interested";
            else if (Pool == 0)
                temp = "Possible";
            else if (Pool == 1)
                temp = "Necessary";
            str += "\n Pool? " + temp;

            if (Jacuzzi == -1)
                temp = "Not interested";
            else if (Jacuzzi == 0)
                temp = "Possible";
            else if (Jacuzzi == 1)
                temp = "Necessary";
            str += "\n Jacuzzi? " + temp;

            if (ChildrenAttractions == -1)
                temp = "Not interested";
            else if (ChildrenAttractions == 0)
                temp = "Possible";
            else if (ChildrenAttractions == 1)
                temp = "Necessary";

            str += "\n Children Attractions: " + temp;

            return str;
        }               
    }

    public interface IEnumerable
    { 

    }
}
