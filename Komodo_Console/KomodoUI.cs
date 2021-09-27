using Komodo_Insurance_Developer_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Komodo_Console
{
    class KomodoUI
    {
        private readonly DeveloperRepo _repo = new DeveloperRepo();
        private readonly DevTeamRepo _devTeamRepo = new DevTeamRepo();

        public void Run()
        {
            SeedData();
            RunMenu();
        }

        private void RunMenu()
        {
            bool isRunning = true;
            while(isRunning)
            {
                Console.Clear();
                Console.WriteLine
                (
                    //1. Add new developer
                    //2. add new devTeam
                    //3. Show all devs
                    //4. Show all devTeams
                    //5. Add dev to dev team
                    //6. remove dev from dev team
                    //7. get list of devs that do not have access to pluaralsight
                    //8. exit
                    "Enter the number of your selection:\n" +
                    "1. Add a new Developer\n" +
                    "2. Add a new Developer Team\n" +
                    "3. Show all Developers\n" +
                    "4. Show all Developer Teams \n" +
                    "5. Add Developer to a Developer Team \n" +
                    "6. Remove Developer from a Developer Team\n" +
                    "7. Get a list of Developers that need Pluaralsight Access\n" +
                    "8. Delete team or developer\n" +
                    "9. Exit"
                );
                string userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        CreateDeveloper();
                        break;
                    case "2":
                        CreateDevTeam();
                        break;
                    case "3":
                        //Show all Developers
                        ShowAllDevelopers();
                        break;
                    case "4":
                        ShowAllDevTeams();
                        break;
                    case "5":
                        AddDeveloperToDevTeam();
                        break;
                    case "6":
                        RemoveDeveloperFromDevTeam();
                        break;
                    case "7":
                        PluralSightAccess();
                        break;
                    case "8":
                        Console.WriteLine("Enter 1 to delete a developer. Enter 2 to delete a devteam: ");
                        string selection = Console.ReadLine();
                        if(selection == "1")
                        {
                            DeleteDeveloper();
                        }
                        else
                        {
                            DeleteDevTeam();
                        }
                        break;
                    case "9":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid number between 1 and 8");
                        PressAnyKeyToContinue();
                        break;
                }
            }
        }
        private void CreateDeveloper()
        {
            Console.Clear();
            Developer developer = new Developer();
            List<Developer> listOfDevelopers = _repo.GetDevelopers();
            Console.WriteLine("Please enter the new employees ID#: ");
            int IDNumber = Convert.ToInt32(Console.ReadLine());

            foreach (Developer developer1 in listOfDevelopers)
            {
                if(developer1.IDNumber == IDNumber)
                {
                    Console.WriteLine("Sorry that ID Number is already taken press any key to return to the main menu.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    developer.IDNumber = IDNumber;
                }
            }

            Console.WriteLine("Please enter the employees name: ");
            developer.Name = Console.ReadLine();

            Console.WriteLine("Does this user have Pluralsight access (Y or N");
            string reply = Console.ReadLine().ToUpper();

            if(reply == "Y" || reply == "Yes")
            {
                developer.PluralsightAccess = true;
            }
            else
            {
                developer.PluralsightAccess = false;
            }
            _repo.AddDeveloperToDirectory(developer);
        } //1
        private void CreateDevTeam()
        {
            Console.Clear();
            DevTeam devTeam = new DevTeam();
            List<DevTeam> listOfDevTeams = _devTeamRepo.GetDevTeam();
            Console.WriteLine("Please enter the new DevTeams ID#: ");
            int IDNumber = Convert.ToInt32(Console.ReadLine());
            foreach (DevTeam devTeam1 in listOfDevTeams)
            {
                if(devTeam1.TeamID == IDNumber)
                {
                    Console.WriteLine("Sorry that devTeam ID number is already taken press any key to return to the main menu.");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    devTeam.TeamID = IDNumber;
                }
            }

            Console.WriteLine("Please enter the devTeam name: ");
            devTeam.TeamName = Console.ReadLine();
            _devTeamRepo.AddDevTeamToDirectory(devTeam);
        } //2
        private void ShowAllDevelopers()
        {
            Console.Clear();
            List<Developer> listOfAllDevelopers = _repo.GetDevelopers();

            foreach (Developer developer in listOfAllDevelopers)
            {
                DisplayDeveloper(developer);
            }
            PressAnyKeyToContinue();
        }//3
        private void ShowAllDevTeams()
        {
            Console.Clear();
            List<DevTeam> listOfDevTeams = _devTeamRepo.GetDevTeam();
            foreach (DevTeam devTeam in listOfDevTeams)
            {
                DisplayDevTeam(devTeam);
            }
        }//4
        private void AddDeveloperToDevTeam()
        {
            Console.Clear();
            ShowAllDevTeams();
            Console.WriteLine("Which Dev Team(ID#) would you like to add a Developer to?: ");
            int teamIDNumber = Convert.ToInt32(Console.ReadLine());
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(teamIDNumber);
            if (teamIDNumber <= 0)
            {
                Console.WriteLine("Sorry that team does not exist");
                PressAnyKeyToContinue();
                return;
            }
            Console.Clear();
            ShowAllDevelopers();
            Console.WriteLine("Which developer would you like to add to that team (enter developer ID#): ");
            int developerIDNumber = Convert.ToInt32(Console.ReadLine());
            Developer developer = _repo.GetDeveloperByID(developerIDNumber);
            if (developerIDNumber <= 0)
            {
                Console.WriteLine("Sorry that developer does not exist");
                PressAnyKeyToContinue();
                return;
            }
            devTeam.DevelopersOnTeam.Add(developer);
        }//5
        private void RemoveDeveloperFromDevTeam()
        {
            Console.Clear();
            ShowAllDevTeams();
            Console.WriteLine("Which Dev Team(ID#) would you like to remove a Developer from?: ");
            int teamIDNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Which developer do you want to remove from team(enter ID#): ");
            int devIDNumber = Convert.ToInt32(Console.ReadLine());
            DevTeam devTeam = _devTeamRepo.GetDevTeamByID(teamIDNumber);

            for(int i = 0; i < devTeam.DevelopersOnTeam.Count; i++)
            {
                if(devIDNumber == devTeam.IDNumber)
                {
                    _devTeamRepo.DeleteDevFromTeam(devTeam, devIDNumber);
                }
            }

            PressAnyKeyToContinue();
        }//6
        private void DeleteDeveloper()
        {
            Console.Clear();
            int index = 1;
            List<Developer> devList = _repo.GetDevelopers();
            foreach (Developer developer in devList)
            {
                Console.WriteLine($"{index}. {developer.Name}");
                index++;
            }
            Console.WriteLine("Select the number of the Developer you want to delete");
            int targetDev = Convert.ToInt32(Console.ReadLine());
            int targetIndex = targetDev - 1;
            if(targetIndex >= 0 && targetIndex < devList.Count)
            {
                Developer developer = devList[targetIndex];
                if(_repo.DeleteDeveloper(developer))
                {
                    Console.WriteLine($"{developer.Name} was deleted.");
                }
                else
                {
                    Console.WriteLine("Something went wrong!");
                }
            }
            else
            {
                Console.WriteLine("That is not a valid selection");
            }
            PressAnyKeyToContinue();
        }//7
        private void DeleteDevTeam()
        {
            int index = 1;
            List<DevTeam> devTeams = _devTeamRepo.GetDevTeam();
            foreach (DevTeam devTeam in devTeams)
            {
                Console.WriteLine($"{index}. {devTeam.TeamName}");
                index++;
            }
            Console.WriteLine("Select the number of the devTeam you want to delete");
            int targetDevTeam = Convert.ToInt32(Console.ReadLine());
            int targetIndex = targetDevTeam - 1;
            if(targetIndex >= 0 && targetIndex < devTeams.Count)
            {
                DevTeam devTeam = devTeams[targetIndex];
                if(_devTeamRepo.DeleteDevTeam(devTeam))
                {
                    Console.WriteLine($"{devTeam.TeamName} has been deleted");
                }
                else
                {
                    Console.WriteLine("Something went wrong!");
                }
            }
            else
            {
                Console.WriteLine("That is not a valid selection");
            }
            PressAnyKeyToContinue();
        }//7
        private void PluralSightAccess()
        {
            Console.Clear();
            List<Developer> listOfDevelopers = _repo.GetDevelopers();
            int counter = 1;
            foreach(Developer developer in listOfDevelopers)
            {
                if(developer.PluralsightAccess == false)
                {
                    Console.WriteLine($"{counter}. {developer.Name}");
                    counter++;
                }
            }
            PressAnyKeyToContinue();

        }//8
        //helper methods
        private void DisplayDevTeam(DevTeam devTeam)
        {
            Console.WriteLine($"Team ID#: {devTeam.TeamID}\n" +
                $"Team Name: {devTeam.TeamName}\n" +
                $"Developers on the Team: ");
            if (devTeam.DevelopersOnTeam == null)
            {
                Console.WriteLine("No devs currently on this team");
                PressAnyKeyToContinue();
            }
            else
            { 
                for (int i = 0; i < devTeam.DevelopersOnTeam.Count; i++)
                {
                    Console.WriteLine($" {devTeam.DevelopersOnTeam[i].IDNumber}. {devTeam.DevelopersOnTeam[i].Name}");
                }
                PressAnyKeyToContinue();
            }
        }
        private void DisplayDeveloper(Developer developer)
        {
            Console.WriteLine($"Name: {developer.Name}\n" +
                $"ID#: {developer.IDNumber}\n" +
                $"Pluralsight access: {developer.PluralsightAccess}\n");
        }
        private void SeedData()
        {
            Developer dev1 = new Developer("Nick", 1, true);
            Developer dev2 = new Developer("Simon", 2, false);
            Developer dev3 = new Developer("Adam", 3, true);
            Developer dev4 = new Developer("Nick", 4, false);
            _repo.AddDeveloperToDirectory(dev1);
            _repo.AddDeveloperToDirectory(dev2);
            _repo.AddDeveloperToDirectory(dev3);
            _repo.AddDeveloperToDirectory(dev4);

            List<Developer> example = new List<Developer>();
            example.Add(dev4);
            example.Add(dev3);
            List<Developer> example2 = new List<Developer>();
            example2.Add(dev1);
            example2.Add(dev2);
            DevTeam devTeam1 = new DevTeam("Team1", 1, example);
            _devTeamRepo.AddDevTeamToDirectory(devTeam1);
            DevTeam devTeam2 = new DevTeam("Team2", 2, example2);
            _devTeamRepo.AddDevTeamToDirectory(devTeam2);
        }
        private void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any Key to Continue...");
            Console.ReadKey();
        }

    }
}
