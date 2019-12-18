using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class Cloning
    {
        public static class Cloning
        {
            public static Student Clone(this Student original)
            { Student target = new Student();
                target.id = original.id;    
                ...       return target; }
        }
    }
}
