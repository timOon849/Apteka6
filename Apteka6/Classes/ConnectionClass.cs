using Apteka6.DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apteka6.Classes
{
    internal class ConnectionClass
    {
        public static AptekaWPFEntities1 apteka = new AptekaWPFEntities1();
        public static Byers byers;
        public static Employee employee;
    }
}
