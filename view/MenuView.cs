using System;
using System.Collections.Generic;
using System.Linq;

namespace View
{
    public class MenuView
    {     
        public static void RegisterMember(Controller.MemberController memberController)
        {
            memberController.SaveMemberList();
        }
        public static void compactList(Controller.MemberController memberController)
        {
            List<Model.Member> viewMemberList = memberController.LoadMemberList();

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Name: " + viewMemberList[i].Name + " Member-id: " + viewMemberList[i].MemberID + " Boats registered: " + viewMemberList[i].Boats.Count);
                Console.ResetColor();
            }
        }

        public static void verboseList(Controller.MemberController memberController)
        {
            List<Model.Member> viewMemberList = memberController.LoadMemberList();

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Name: " + viewMemberList[i].Name + " Personal-number: " + viewMemberList[i].PersonalNumber + " Member-id: " + viewMemberList[i].MemberID + " Boats registered: " + viewMemberList[i].Boats.Count);
                Console.ResetColor();
            }
        }
        public static void DeleteMember(Controller.MemberController memberController)
        {
            memberController.DeleteMemberFromList();
        }

        public static void UpdateMember(Controller.MemberController memberController)
        {
            memberController.UpdateMemberOnList();
        }

        public static void DeleteBoat(Controller.BoatController boatController)
        {
            boatController.DeleteBoatFromList();
        }

        public static void RegisterBoat(Controller.BoatController boatController)
        {
            boatController.SaveBoatList();
        }
        public static void viewAllBoats(Controller.BoatController boatController)
        {
            Model.Member member = boatController.LoadMemberBoatList();
            List<Model.Boat> viewBoatList = member.Boats;

            for (int i = 0; i < viewBoatList.Count; i++)
            {
                if (viewBoatList.Count == 0) 
                {
                    Console.WriteLine("User have no boats registrated");
                }
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("Boat-type: " + viewBoatList[i].BoatType + " length: " + viewBoatList[i].Length + "m boat-id: " + viewBoatList[i].BoatID);
                Console.ResetColor();
            }
        }
        public void ReadMenuInput(View.MemberView memberView, Controller.MemberController memberController, View.BoatView boatView, Controller.BoatController boatController)
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

                    if (input == "0")
                    {
                        ReadMenuMemberInput(memberView, memberController, boatView, boatController);
                    } else if (input == "1")
                    {
                        ReadMenuBoatInput(memberView, memberController, boatView, boatController);
                    } else if (input == "2") 
                    {
                        ExitMessage();
                    } else 
                    {
                        throw new ApplicationException();
                    }
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

        private int ReadMenuBoatInput(View.MemberView memberView, Controller.MemberController memberController, View.BoatView boatView, Controller.BoatController boatController)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nBoat-menu: [0] = [View Member Boat(s)], [1] = [Register], [2] = [Update], [3] = [Delete], [4] = [Back]. Navigate with a number between 0 and 4.\n");
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
                    if (input == "0")
                    {
                        viewAllBoats(boatController);
                    } else if (input == "1")
                    {
                        RegisterBoat(boatController);
                    } else if (input == "2") 
                    {
                        throw new ApplicationException("Update boat not implented yet.");
                    } else if (input == "3")
                    {
                       DeleteBoat(boatController);
                    } else if (input == "4") 
                    {
                        ReadMenuInput(memberView, memberController, boatView, boatController);
                    } else
                    {
                        throw new ApplicationException();
                    }
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
        private int ReadMenuMemberInput(View.MemberView memberView, Controller.MemberController memberController, View.BoatView boatView, Controller.BoatController boatController)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("\nMember-menu: [0] = [Compact List], [1] = [Verbose List], [2] = [Register], [3] = [Update], [4] = [Delete], [5] = [Back]. Navigate with a number between 0 and 5.\n");
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
                    
                    if (input == "0")
                    {
                        compactList(memberController);
                    } else if (input == "1")
                    {
                        verboseList(memberController);
                    } else if (input == "2") 
                    {
                        RegisterMember(memberController);
                    } else if (input == "3")
                    {
                        UpdateMember(memberController);
                    } else if (input == "4") 
                    {
                        DeleteMember(memberController);
                    } else if (input == "5") 
                    {
                        ReadMenuInput(memberView, memberController, boatView, boatController);
                    } else 
                    {
                        // throw new ApplicationException();
                    }
                }
                catch (Exception)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! Your choice must contain 1 number between 0 and 5. Just started? Chose [2] - Register.\n");
                    Console.ResetColor();
                }
            }
        }

        private static void ExitMessage()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("\nGoodbye!\n");
            Console.ResetColor();
            Environment.Exit(0);
        }
    }
}