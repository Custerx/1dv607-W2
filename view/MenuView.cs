using System;
using System.Collections.Generic;
using System.Linq;

namespace View
{
    public class MenuView
    {
        public enum MenuChoice
        {
            Login,
            Register,
            Guest,
            Exit,
            Invalid
        }

        public enum GuestChoice
        {
            Compactlist,
            Verboselist,
            ClubsBoatlist,
            Search,
            Exit,
            Invalid
        }

        public enum StartMenuChoice
        {
            Member,
            Boat,
            Search,
            Exit,
            Invalid
        }

        public enum BoatMenuChoice
        {
            Register,
            Update,
            Delete,
            View,
            ClubsBoatlist,
            Back,
            Invalid
        }

        public enum SearchMenuChoice
        {
            Username,
            Age,
            UsernameBoatType,
            HardcodedGrade4Example,
            Back,
            Invalid
        }

        public enum MemberMenuChoice
        {
            Compactlist,
            Verboselist,
            Update,
            Delete,
            Back,
            Invalid
        }

        public MenuChoice getAuthorizationMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Login-system for Jack Sparrow's Boatclub");
                    Console.WriteLine("\nLogin-menu: [0] = [Login], [1] = [Register], [2] = [Guest], [3] = [Exit]. Navigate with a number between 0 and 3.\n");
                    Console.ResetColor();

                    input = Console.ReadLine();

                    if (!input.All(c => c >= '0' && c <= '3'))
                    {
                        throw new ApplicationException();
                    }

                    return (MenuChoice)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 3.\n");
                    Console.ResetColor();
                }
            }
        }

        public GuestChoice getGuestMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Guest-system for Jack Sparrow's Boatclub");
                    Console.WriteLine("\nGuest-menu: [0] = [Compact List], [1] = [Verbose List], [2] = [Club's Boat List], [3] = [Search], [4] = [Exit]. Navigate with a number between 0 and 4.\n");
                    Console.ResetColor();

                    input = Console.ReadLine();

                    if (!input.All(c => c >= '0' && c <= '4'))
                    {
                        throw new ApplicationException();
                    }

                    return (GuestChoice)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 4.\n");
                    Console.ResetColor();
                }
            }
        }

        public StartMenuChoice getNavigationMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Welcome to Jack Sparrow's Boatclub!");
                    Console.WriteLine("\nMenu: [0] = [Member], [1] = [Boat], [2] = [Search], [3] = [Exit]. Navigate with a number between 0 and 3.\n");
                    Console.ResetColor();

                    input = Console.ReadLine();

                    if (!input.All(c => c >= '0' && c <= '3'))
                    {
                        throw new ApplicationException();
                    }

                    return (StartMenuChoice)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 3.\n");
                    Console.ResetColor();
                }
            }
        }

        public BoatMenuChoice getBoatMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Boat-system for Jack Sparrow's Boatclub");
                    Console.Write("\nBoat-menu: [0] = [Register], [1] = [Update], [2] = [Delete], [3] = [View], [4] = [Club's Boat List], [5] = [Back]. Navigate with a number between 0 and 5.\n");
                    Console.ResetColor();
                    input = Console.ReadLine();


                    if (input.Length != 1)
                    {
                        throw new ApplicationException();
                    }

                    if (!input.All(c => c >= '0' && c <= '5'))
                    {
                        throw new ApplicationException();
                    }

                    return (BoatMenuChoice)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 5.\n");
                    Console.ResetColor();
                }
            }
        }

        public SearchMenuChoice getSearchMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Search-system for Jack Sparrow's Boatclub");
                    Console.Write("\nSearch-menu: [0] = [Username], [1] = [Age], [2] = [Username + BoatType], [3] = [HardcodedGrade4Example], [4] = [Back]. Navigate with a number between 0 and 4.\n");
                    Console.ResetColor();
                    input = Console.ReadLine();


                    if (input.Length != 1)
                    {
                        throw new ApplicationException();
                    }

                    if (!input.All(c => c >= '0' && c <= '4'))
                    {
                        throw new ApplicationException();
                    }

                    return (SearchMenuChoice)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 4.\n");
                    Console.ResetColor();
                }
            }
        }

        public MemberMenuChoice getMemberMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Member-system for Jack Sparrow's Boatclub");
                    Console.Write("\nMember-menu: [0] = [Compact List], [1] = [Verbose List], [2] = [Update], [3] = [Delete], [4] = [Back]. Navigate with a number between 0 and 4.\n");
                    Console.ResetColor();
                    input = Console.ReadLine();


                    if (input.Length != 1)
                    {
                        throw new ApplicationException();
                    }

                    if (!input.All(c => c >= '0' && c <= '4'))
                    {
                        throw new ApplicationException();
                    }
                    
                    return (MemberMenuChoice)int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 4.\n");
                    Console.ResetColor();
                }
            }
        }

        public void ExitMessage()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\nGoodbye!\n");
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}