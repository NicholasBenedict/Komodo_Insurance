using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Insurance_Developer_Repository
{
    public class DevTeam : Developer
    {
        //POCO
        //Plain Old C# Object
        public DevTeam() { }

        public DevTeam(string teamName, int teamID)
        {
            TeamName = teamName;
            TeamID = teamID;
        }

        public DevTeam(string teamName, int teamID, List<Developer> developersOnTeam)
        {
            TeamName = teamName;
            TeamID = teamID;
            DevelopersOnTeam = developersOnTeam;
        }


        public string TeamName { get; set; }
        public int TeamID { get; set; }
        public List<Developer> DevelopersOnTeam { get; set; } = new List<Developer>();
    }
}
