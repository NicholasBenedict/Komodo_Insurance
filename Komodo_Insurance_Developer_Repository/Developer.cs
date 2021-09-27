using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Insurance_Developer_Repository
{
    public class Developer
    {
        //POCO
        //Plain old C# Object

        public Developer() { }
        public Developer(string name, int idNumber, bool pAccess)
        {
            Name = name;
            IDNumber = idNumber;
            PluralsightAccess = pAccess;
        }

        public string Name { get; set; }
        public int IDNumber { get; set; }
        public bool PluralsightAccess { get; set; }
    }
}
