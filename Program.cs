using System;
using System.IO;
using System.Linq;

namespace w2
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller.MasterController BoatClub = new Controller.MasterController();          
            BoatClub.Run();
        }
    }
}
