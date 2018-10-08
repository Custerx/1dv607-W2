using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Member
    {
        private string _name;
        private long _personalNumber;
        private string _memberID;

        public string Name 
        { 
            get => _name; 
            set 
            {
                if (value.Length < 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _name = value;
            }            
        }

        public long PersonalNumber 
        { 
            get => _personalNumber; 
            set 
            {
                if (value < 10)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _personalNumber = value;
            }            
        }

        public string MemberID { get { return this._memberID;} set { this._memberID = value; } }

        public List<Model.Boat> Boats { get; set; }
        
        public Member(string Name, long PersonalNumber, string Id) {
            this.Name = Name;
            this.PersonalNumber = PersonalNumber;
            this._memberID = Id;
            Boats = new List<Model.Boat>();
        }
    }
}