using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class ID
    {
        private static Random _random = new Random();
        private List<string> _ID_list = new List<string>();

        public string createID()
        {
            string randomID = this.randomID();
            
            if (this.not_unique_ID(randomID))
            {
                return this.createID();
            }

            this._ID_list.Add(randomID);
            return randomID;
        }     

        private bool not_unique_ID(string randomID)
        {
            foreach(string id in _ID_list)
            {
                if(id.Contains(randomID))
                {
                    return true;
                }     
            }

            return false; 
        }

        private string randomID(int length = 6)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}