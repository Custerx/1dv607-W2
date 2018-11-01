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

        private MenuChoice getMenuChoice(int choice)
        {
            if (choice == 0)
            {
                return MenuChoice.Login;
            }
            
            if (choice == 1)
            {
                return MenuChoice.Register;
            }

            if (choice == 2)
            {
                return MenuChoice.Guest;
            }

            if (choice == 3)
            {
                return MenuChoice.Exit;
            }

            return MenuChoice.Invalid;
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

                    return getMenuChoice(int.Parse(input));
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

        public enum GuestChoice
        {
            Compactlist,
            Verboselist,
            ClubsBoatlist,
            Search,
            Exit,
            Invalid
        }

        private GuestChoice getGuestMenuChoice(int choice)
        {
            if (choice == 0)
            {
                return GuestChoice.Compactlist;
            }
            
            if (choice == 1)
            {
                return GuestChoice.Verboselist;
            }

            if (choice == 2)
            {
                return GuestChoice.ClubsBoatlist;
            }

            if (choice == 3)
            {
                return GuestChoice.Search;
            }

            if (choice == 4)
            {
                return GuestChoice.Exit;
            }

            return GuestChoice.Invalid;
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

                    return getGuestMenuChoice(int.Parse(input));
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

        public enum StartMenuChoice
        {
            Member,
            Boat,
            Search,
            Exit,
            Invalid
        }

        private StartMenuChoice getStartMenuChoice(int choice)
        {
            if (choice == 0)
            {
                return StartMenuChoice.Member;
            }
            
            if (choice == 1)
            {
                return StartMenuChoice.Boat;
            }

            if (choice == 2)
            {
                return StartMenuChoice.Search;
            }

            if (choice == 3)
            {
                return StartMenuChoice.Exit;
            }

            return StartMenuChoice.Invalid;
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

                    return getStartMenuChoice(int.Parse(input));
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

        private BoatMenuChoice getBoatMenuChoice(int choice)
        {
            if (choice == 0)
            {
                return BoatMenuChoice.Register;
            }
            
            if (choice == 1)
            {
                return BoatMenuChoice.Update;
            }

            if (choice == 2)
            {
                return BoatMenuChoice.Delete;
            }

            if (choice == 3)
            {
                return BoatMenuChoice.View;
            }

            if (choice == 4)
            {
                return BoatMenuChoice.ClubsBoatlist;
            }

            if (choice == 5)
            {
                return BoatMenuChoice.Back;
            }

            return BoatMenuChoice.Invalid;
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

                    return getBoatMenuChoice(int.Parse(input));
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

        public enum SearchMenuChoice
        {
            Username,
            Age,
            UsernameBoatType,
            Back,
            Invalid
        }

        private SearchMenuChoice getSearchMenuChoice(int choice)
        {
            if (choice == 0)
            {
                return SearchMenuChoice.Username;
            }
            
            if (choice == 1)
            {
                return SearchMenuChoice.Age;
            }

            if (choice == 2)
            {
                return SearchMenuChoice.UsernameBoatType;
            }

            if (choice == 3)
            {
                return SearchMenuChoice.Back;
            }

            return SearchMenuChoice.Invalid;
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
                    Console.Write("\nSearch-menu: [0] = [Username], [1] = [Age], [2] = [Username + BoatType], [3] = [Back]. Navigate with a number between 0 and 3.\n");
                    Console.ResetColor();
                    input = Console.ReadLine();


                    if (input.Length != 1)
                    {
                        throw new ApplicationException();
                    }

                    if (!input.All(c => c >= '0' && c <= '3'))
                    {
                        throw new ApplicationException();
                    }

                    return getSearchMenuChoice(int.Parse(input));
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

        public enum MemberMenuChoice
        {
            Compactlist,
            Verboselist,
            Update,
            Delete,
            Back,
            Invalid
        }

        private MemberMenuChoice getMemberMenuChoice(int choice)
        {
            if (choice == 0)
            {
                return MemberMenuChoice.Compactlist;
            }
            
            if (choice == 1)
            {
                return MemberMenuChoice.Verboselist;
            }

            if (choice == 2)
            {
                return MemberMenuChoice.Update;
            }

            if (choice == 3)
            {
                return MemberMenuChoice.Delete;
            }

            if (choice == 4)
            {
                return MemberMenuChoice.Back;
            }

            return MemberMenuChoice.Invalid;
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
                    
                    return getMemberMenuChoice(int.Parse(input));
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