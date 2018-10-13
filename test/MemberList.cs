using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class MemberList
    {
        private static Random _random = new Random();
        private string _password = "testtest";

        private List<Model.Member> _memberList50;

        public List<Model.Member> create50membersAnd200BoatsThenSaveTofile()
        {
            _memberList50 = this.create50Members();
            
            for (int i = 0; i < _memberList50.Count; i++)
            {
                this.create4Boats(_memberList50[i]);
            }

            return _memberList50;
        }

        private void create4Boats(Model.Member memberToGet4Boats)
        {
            int amountBoats = 4;
      
            for (int i = 0; i < amountBoats; i++)
            {
                memberToGet4Boats.Boats.Add(this.genericBoat());
            }
        }

        private Model.Boat genericBoat()
        {
            return new Model.Boat(this.RandomBoatType(), this.RandomBoatLength(), this.RandomID());
        }

        private int RandomBoatLength()
        {
            return _random.Next(1, 15); // 1m to 15m
        }

        private Enums.BoatTypes.Boats RandomBoatType()
        {
            int generateRandomBoatType = _random.Next(0, 3);
            Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(generateRandomBoatType);
            return boatType;
        }

        private List<Model.Member> create50Members()
        {
            List<Model.Member> memberList = new List<Model.Member>();
            int amountMembers = 50;
            
            for (int i = 0; i < amountMembers; i++)
            {
                memberList.Add(this.genericMember());
            }

            return memberList;
        }
        private Model.Member genericMember()
        {
            return new Model.Member(this.RandomName(), "19" + this.RandomPersonalNumber(), this.RandomID(), _password);
        }

        private string RandomPersonalNumber(int length = 10)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string RandomName(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string RandomID(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}