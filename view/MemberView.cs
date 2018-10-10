using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace View
{
    
    public class MemberView
    {
        public void viewCompactList(List<Model.Member> memberList)
        {
            if (memberList.Count < 1)
            {
                return;
            }

            for (int i = 0; i < memberList.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(this.ToString("Compact", memberList[i]));
                Console.ResetColor();
            }
        }

        public void viewVerboseList(List<Model.Member> memberList)
        {
            if (memberList.Count < 1)
            {
                return;
            }

            for (int i = 0; i < memberList.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(this.ToString("Verbose", memberList[i]));
                Console.ResetColor();
            }
        }

        private string ToString(string format, Model.Member member) 
        {
            if (format == "Verbose" || format.Length == 0 || format == null) 
            {     
                return string.Join(" ", "Name: " + member.Name, "Personal-number: " + member.PersonalNumber, "Member-id: " + member.MemberID, "Boats: " + this.displayBoat(member.Boats));
            }
            if (format == "Compact") {
                return string.Join(" ", "Name: " + member.Name, "Member-id: " + member.MemberID, "Boats: " + member.Boats.Count + ".");
            }
            throw new FormatException(nameof(format));
        }

        private string displayBoat(List<Model.Boat> boats)
        {
            string displayBoats = "";
            if (boats.Count > 0)
            {
                 foreach (Model.Boat boat in boats)
                {
                    displayBoats += boat.BoatType + " on " + boat.Length + "m with Boat-id: " + boat.BoatID + ". " ;
                }               
            } else {
                displayBoats = "No boats registered.";
            }
            return displayBoats;
        }

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
        public string getSearchInput(string message)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();

                    StripHTML(input);

                    if (input.Length < 1)
                    {
                        throw new ApplicationException();
                    }

                    return input;
                    }
                    catch (Exception)
                    {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Username must contain atleast 1 characters.\n");
                    Console.ResetColor();
                }
            }
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

                    StripHTML(input);

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

                    StripHTML(input);

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

                    StripHTML(input);

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

        public string ReadMemberPasswordInput(string message)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();

                    StripHTML(input);

                    if (input.Length > 24 || input.Length < 6)
                    {
                        throw new ApplicationException("\nPassword has too few characters, at least 6 characters.\n");
                    }

                    return input;
                    }
                    catch (Exception)
                    {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nPassword has too few characters, at least 6 characters.\n");
                    Console.ResetColor();
                }
            }
        }

        private static string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}