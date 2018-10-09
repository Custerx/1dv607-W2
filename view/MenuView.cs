using System;
using System.Collections.Generic;
using System.Linq;

namespace View
{
    public class MenuView
    {
        public int getAuthorizationMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Login-system for Jack Sparrow's Boatclub");
                    Console.WriteLine("\nMenu: [0] = [Login], [1] = [Register], [2] = [Guest], [3] = [Exit]. Navigate with a number between 0 and 3.\n");
                    Console.ResetColor();

                    input = Console.ReadLine();

                    if (!input.All(c => c >= '0' && c <= '3'))
                    {
                        throw new ApplicationException();
                    }

                    return int.Parse(input);
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
        public int getNavigationMenuInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("Welcome to Jack Sparrow's Boatclub!");
                    Console.WriteLine("\nMenu: [0] = [Member], [1] = [Boat], [2] = [Exit]. Navigate with a number between 0 and 2.\n");
                    Console.ResetColor();

                    input = Console.ReadLine();

                    if (!input.All(c => c >= '0' && c <= '2'))
                    {
                        throw new ApplicationException();
                    }

                    return int.Parse(input);
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 2.\n");
                    Console.ResetColor();
                }
            }
        }

        public int ReadMenuBoatInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nBoat-menu: [0] = [Register], [1] = [Update], [2] = [Delete], [3] = [Back]. Navigate with a number between 0 and 3.\n");
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

                    return int.Parse(input);
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
        public int ReadMenuMemberInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
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
                    
                    return int.Parse(input);
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