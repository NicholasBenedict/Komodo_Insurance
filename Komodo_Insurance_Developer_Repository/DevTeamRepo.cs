using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Insurance_Developer_Repository
{
    public class DevTeamRepo
    {
        //Crud
        private readonly List<DevTeam> _devTeam = new List<DevTeam>();
        //Create
        public bool AddDevTeamToDirectory(DevTeam devTeam)
        {
            int startingCount = _devTeam.Count;

            _devTeam.Add(devTeam);

            bool wasAdded = (_devTeam.Count > startingCount);

            return wasAdded;
        }
        //Read
        public List<DevTeam> GetDevTeam()
        {
            return _devTeam;
        }
        public DevTeam GetDevTeamByID(int ID)
        {
            foreach (DevTeam devTeam in _devTeam)
            {
                if(devTeam.TeamID == ID)
                {
                    return devTeam;
                }
            }
            return null;
        }
        public DevTeam ListDevelopersOnTeam(DevTeam devTeam)
        {
            for(int i = 0; i < devTeam.DevelopersOnTeam.Count; i++)
            {
                devTeam.DevelopersOnTeam[i].ToString();
                return devTeam;
            }
            return null;
        }
        //Update
        public bool UpdateExistingDevTeam(DevTeam existingDevTeam, DevTeam newDevTeam)
        {
            if (existingDevTeam != null)
            {
                existingDevTeam.TeamID = newDevTeam.TeamID;
                existingDevTeam.TeamName = newDevTeam.TeamName;
                return true;
            }
            else
            {
                return false;
            }
        }
        //Delete

        public bool DeleteDevTeam(DevTeam existingDevTeam)
        {
            bool result = _devTeam.Remove(existingDevTeam);
            return result;
        }
        public bool DeleteDevFromTeam(DevTeam existingTeam, int ID)
        {
            bool result = _devTeam.Remove((DevTeam)existingTeam.DevelopersOnTeam[ID]);
            return result;
        }
    }
}
