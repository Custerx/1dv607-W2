using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    public class MemberList
    {
        private static Random _random = new Random();
        private string _password = "testtest";
        private Model.CreateMember m_createMember;
        private List<Model.Member> _memberList50;

        public List<Model.Member> create50membersAnd200Boats()
        {
            _memberList50 = this.create50Members();
            
            for (int i = 0; i < _memberList50.Count; i++)
            {
                this.create4Boats(_memberList50[i]);
            }

            return _memberList50;
        }

        public MemberList(Model.CreateMember a_createMember)
        {
            m_createMember = a_createMember;
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
            return new Model.Boat(this.randomBoatType(), this.randomBoatLength(), this.randomID());
        }

        private int randomBoatLength()
        {
            return _random.Next(1, 15); // 1m to 15m
        }

        private Enums.BoatTypes.Boats randomBoatType()
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
            return m_createMember.create(this.randomName(), "19" + this.randomPersonalNumber(), _password);
        }

        private string randomPersonalNumber(int length = 10)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string randomName(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        private string randomID(int length = 6)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}