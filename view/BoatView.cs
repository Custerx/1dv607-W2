using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace View
{
    public class BoatView : GenericView
    {
        public void viewMemberBoats(Model.Member boatOwner)
        {
            for (int i = 0; i < boatOwner.Boats.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(this.ToString(boatOwner.Boats[i]));
                Console.ResetColor();
            }
        }

        public void viewAllBoatClubBoats(List<Model.Member> memberList)
        {      
            for (int i = 0; i < memberList.Count; i++)
            {
                this.viewMemberBoats(memberList[i]);
            }
        }
        
        public int getBoatLengthInput()
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

        public int getBoatTypeInput()
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

        private string ToString(Model.Boat boat) 
        {   
            return string.Join(" ", boat.BoatType + " on " + boat.Length + "m ID: " + boat.BoatID);
        }
    }
}