using RoomMates.Models;
using RoomMates.Repositories;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        //  This is the address of the database.
        //  We define it here as a constant since it will never change.
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true;Trust Server Certificate=true";

        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository roommateRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;
            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Name} has an Id of {r.Id} and a max occupancy of {r.MaxOccupancy}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());

                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll2();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}");       
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show Unassigned Chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}");
                        }
                       
                        Console.WriteLine("Press any key to continue");
                        

                        Console.ReadKey();
                        break;
                    case ("Assign chore to roommate"):
                        List<Chore> unassignedChores2 = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores2)
                        {
                            Console.WriteLine($"{c.Name} has an id of {c.Id}");
                        }
                        Console.WriteLine();

                        Console.Write("Select Id of chore to assign ");
                        int chosenChoreId = int.Parse(Console.ReadLine());
                        Chore choreToAssign = choreRepo.GetById(chosenChoreId);
                        List<Roommate> allRoommates = roommateRepo.GetAll();
                        foreach (Roommate rm in allRoommates)
                        {
                            Console.WriteLine($" Id {rm.Id} - {rm.FirstName} {rm.LastName}");
                        }
                        Console.WriteLine();    
                        Console.Write($"Select the Id of the roommate to assign the '{choreToAssign.Name}' chore to");
                        int chosenRoommateId = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(chosenRoommateId, chosenChoreId);
                        Console.WriteLine("Successful");

                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for Chore"):
                        Console.Write("Chore Id: ");
                        int ChoreId = int.Parse(Console.ReadLine());

                        Chore chore = choreRepo.GetById(ChoreId);

                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Add a chore"):
                        Console.Write("Chore name: ");
                        string choreName = Console.ReadLine();

                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName,
                        };

                        choreRepo.Insert(choreToAdd);

                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for Roommate"):
                        Console.Write("roommate Id: ");
                        int roomMateId = int.Parse(Console.ReadLine());

                        Roommate roommate = roommateRepo.GetById(roomMateId);

                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName}, Rent Portion: {roommate.RentPortion}, Room: {roommate.Room.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;
                }
            }

        }

        static string GetMenuSelection()
        {
            Console.Clear();

            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Show Unassigned Chores",
                "Assign chore to roommate",
                "Search for a Chore",
                "Add a Chore",
                "Search for Roommate",
                "Exit"
            };

            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];
                }
                catch (Exception)
                {

                    continue;
                }
            }
        }
    }
}