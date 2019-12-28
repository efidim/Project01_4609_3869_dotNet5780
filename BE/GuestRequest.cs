using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest:IEnumerable
    {
        public int GuestRequestKey;
        public string PrivateName;
        public string FamilyName;
        public string MailAddress;
        public bool Status;
        public DateTime RegistrationDate;
        public DateTime EntryDate;
        public DateTime ReleaseDate;
        public string Area;
        public string subArea;
        public string Type;
        public int Adults;
        public int Children;
        public int Pool;
        public int Jacuzzi;
        public int ChildrenAttractions;

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
                temp = "Disactive";
            str += "\n Status? " + temp +
                "\n RegistrationDate: " + RegistrationDate +
                "\n EntryDate: " + EntryDate +
                "\n ReleaseDate: " + ReleaseDate +
                "\n Area: " + Area +
                "\n subArea: " + subArea +
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
