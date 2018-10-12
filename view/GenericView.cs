using System;
using System.Text.RegularExpressions;

namespace View
{
    public abstract class GenericView
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

        public string getIDInput(string message)
        {
            string input;

            while (true)
            {
                try
                {
                    Console.Write(message);
                    input = Console.ReadLine();

                    this.StripHTML(input);

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

        public string StripHTML(string input)
        {
            return Regex.Replace(input, "<.*?>", String.Empty);
        }
    }
}