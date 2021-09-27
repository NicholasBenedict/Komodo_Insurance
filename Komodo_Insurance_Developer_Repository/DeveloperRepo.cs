using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Insurance_Developer_Repository
{
    public class DeveloperRepo
    {
        //Repository Pattern
        //CRUD - Create Read Update Delete
        private readonly List<Developer> _developer = new List<Developer>();

        //Create
        public bool AddDeveloperToDirectory(Developer developer)
        {
            int startingCount = _developer.Count;

            _developer.Add(developer);

            bool wasAdded = (_developer.Count > startingCount) ? true : false;

            return wasAdded;
        }
        //Read
        public List<Developer> GetDevelopers()
        {
            return _developer;
        }
        public Developer GetDeveloperByID(int devID)
        {
            foreach (Developer developer in _developer)
            {
                if(developer.IDNumber == devID)
                {
                    return developer;
                }
            }
            return null;
        }
        //Update
        public bool UpdateExistingDeloper(Developer existingDeveloper, Developer newDeveloper)
        {
            if(existingDeveloper != null)
            {
                existingDeveloper.IDNumber = newDeveloper.IDNumber;
                existingDeveloper.Name = newDeveloper.Name;
                existingDeveloper.PluralsightAccess = newDeveloper.PluralsightAccess;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Delete
        public bool DeleteDeveloper(Developer existingDeveloper)
        {
            bool result = _developer.Remove(existingDeveloper);
            return result;
        }
    }
}
