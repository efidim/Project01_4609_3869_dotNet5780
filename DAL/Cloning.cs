using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Cloning
    {
            public static GuestRequest Clone(this GuestRequest original)
            {
            guestRequest target = new guestRequest();
                target.id = original.id;    
                ...       return target; }
    }
    
}
