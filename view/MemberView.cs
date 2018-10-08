using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace View
{
    public class MemberView
    {
        public void successfullMemberCreation()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nMember successfully registered!\n");
            Console.ResetColor();
        }
        public void unsuccessfullUpdate()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nMember not found!\n");
            Console.ResetColor();
        }

        public void unsuccessfullFileLoad()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nNo members found on file. Please start with register a new member.\n");
            Console.ResetColor();
        }
        public void successfullyUpdated(string user)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nMember " + user + " successfully updated!\n");
            Console.ResetColor();
        }
        public void unsuccessfull()
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nNo matching member!\n");
            Console.ResetColor();
        }
        public void successfullyDeleted(string user)
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("\nMember " + user + " successfully deleted!\n");
            Console.ResetColor();
        }

        public string ReadUsernameInput(string message)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();


                    if (input.Length < 3)
                    {
                        throw new ApplicationException();
                    }

                    return input;
                    }
                    catch (Exception)
                    {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Username must contain atleast 3 characters.\n");
                    Console.ResetColor();
                }
            }
        }

        public long ReadPersonalnumberInput(string message)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();


                    if (input.Length != 10)
                    {
                        throw new ApplicationException();
                    }

                    if (!input.All(c => c >= '0' && c <= '9'))
                    {
                        throw new ApplicationException();
                    }

                    return long.Parse(input);
                    }
                    catch (Exception)
                    {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Personal number must contain 10 numbers.\n");
                    Console.ResetColor();
                }
            }
        }
        public string ReadMemberIDInput(string message)
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
                    Console.WriteLine("\nError! Member-id must contain 6 characters.\n");
                    Console.ResetColor();
                }
            }
        }
    }
}