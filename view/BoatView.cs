using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace View
{
    public class BoatView
    {
        public void messageForError(string message)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n " + message + " \n");
            Console.ResetColor();
        }

        public void messageForSuccess(string message)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\n " + message + " \n");
            Console.ResetColor();
        }

        public string ReadIDInput(string message)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();

                    if (input.Length != 6)
                    {
                        throw new ApplicationException();
                    }

                    return input;
                    }
                    catch (Exception)
                    {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! ID must contain 6 characters.\n");
                    Console.ResetColor();
                }
            }
        }
        
        public int ReadBoatLengthInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write("Type your boats length [meter]: ");
                    input = Console.ReadLine();

                    if (input.Length == 0)
                    {
                        throw new ApplicationException();
                    }

                    if (!input.All(c => c >= '0' && c <= '9'))
                    {
                        throw new ApplicationException();
                    }

                    return int.Parse(input);
                    }
                    catch (Exception)
                    {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Boat length must be above 0m and only contain numbers.\n");
                    Console.ResetColor();
                }
            }
        }

        public int ReadBoatTypeInput()
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write("What type of boat do you have? Chose the number that represent your boat [0] = Sailboat, [1] = Motorsailer, [2] = Kayak/Canoe, [3] = Other: ");
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
                    Console.WriteLine("\nError! Your boat-type must be a number between 0 and 3.\n");
                    Console.ResetColor();
                }
            }
        }
    }
}