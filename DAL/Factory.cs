using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class FactoryDal
    {
        protected static FactoryDal instance;
        protected FactoryDal() { }
        public static FactoryDal GetInstance()//singleton
        {
            if (instance == null)
                instance = new FactoryDal();
            return instance;
        }
       /* public bool CheckStudent(int id)
        {
            return DS.DataSource.GuestRequests.Any(stud->stud.Id == id);
        }*/
    }
    
}
