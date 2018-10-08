using System;
using System.Collections.Generic;
using System.Linq;

namespace View
{
    public class MenuView
    {
        private MemberView _memberView;
        private BoatView _boatView;
        public MenuView(MemberView memberView, BoatView boatView) {
            this._memberView = memberView;
            this._boatView = boatView;
        }

        public void compactList(Controller.MemberController memberController)
        {
            List<Model.Member> viewMemberList = memberController.LoadMemberList();

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(this._memberView.ToString("Compact", viewMemberList[i]));
                Console.ResetColor();
            }
        }

        public void verboseList(Controller.MemberController memberController)
        {
            List<Model.Member> viewMemberList = memberController.LoadMemberList();

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(this._memberView.ToString("Verbose", viewMemberList[i]));
                Console.ResetColor();
            }
        }

        public void viewAllBoats(Controller.BoatController boatController)
        {
            Model.Member member = boatController.LoadMemberBoatList();
            List<Model.Boat> viewBoatList = member.Boats;

            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(this._boatView.displayBoat(viewBoatList));
            Console.ResetColor();
        }
        public int ReadMenuInput()
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
        public int ReadMenuMemberInput()
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
                    
                    return int.Parse(input);
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