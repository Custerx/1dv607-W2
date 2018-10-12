using System;
using System.Collections.Generic;
using System.Linq;

namespace Model
{
    public class Member
    {
        private string _name;
        private string _personalNumber;
        private string _memberID;
        private string _password;

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

        public string PersonalNumber 
        { 
            get => _personalNumber; 
            set 
            {
                if (value.Length < 12)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _personalNumber = value;
            }            
        }

        public string MemberID { get { return this._memberID;} set { this._memberID = value; } }

        public List<Model.Boat> Boats { get; set; }

        public string Password
        { 
            get => _password; 
            set 
            {
                if (value.Length < 6)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _password = value;
            }            
        }
        
        public Member(string Name, string PersonalNumber, string Id, string Password) 
        {
            this.Name = Name;
            this.PersonalNumber = PersonalNumber;
            this._memberID = Id;
            Boats = new List<Model.Boat>();
            this.Password = Password;
        }
    }
}