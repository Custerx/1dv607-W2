using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public abstract class FileController : ISearchController
    {
        private int _thisYear = 2018; // Used to calculate age from personalnumber.

        public Model.Member getMemberByName(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = this.LoadMemberList();

            foreach(var Member in memberList.Where(member => member.Name.Equals(searchCriteria.Name)))
            {
                return Member;
            }

            return null;
        }

        public Model.Member getMemberByPersonalNumber(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = this.LoadMemberList();

            foreach(var Member in memberList.Where(member => member.PersonalNumber.Equals(searchCriteria.PersonalNumber)))
            {
                return Member;
            }

            return null;
        }

        public List<Model.Member> getList_Member_Age(Model.SearchMember searchCriteria, bool younger)
        {
            List<Model.Member> memberList = this.LoadMemberList();
            List<Model.Member> memberList_ByAge = new List<Model.Member>();

            if (younger)
            {
                foreach(var Member in memberList.Where(member => (_thisYear - int.Parse(member.PersonalNumber.Substring(0, 4))) < 
                    (_thisYear - int.Parse(searchCriteria.PersonalNumber.Substring(0, 4)))))
                {
                    memberList_ByAge.Add(Member);
                }
            } else
            {
                foreach(var Member in memberList.Where(member => (_thisYear - int.Parse(member.PersonalNumber.Substring(0, 4))) > 
                    (_thisYear - int.Parse(searchCriteria.PersonalNumber.Substring(0, 4)))))
                {
                    memberList_ByAge.Add(Member);
                }
            }

            return memberList_ByAge;
        }

        public List<Model.Member> getList_Member_Name(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = this.LoadMemberList();
            List<Model.Member> memberList_ByName = new List<Model.Member>();

            foreach(var Member in memberList.Where(member => member.Name.ToLower().Contains(searchCriteria.SearchString.ToLower())))
            {
                memberList_ByName.Add(Member);
            }

            return memberList_ByName;
        }

        public List<Model.Member> getList_Member_NameAndBoatType(Model.SearchMember searchCriteria, List<Model.Member> memberList_ByName)
        {
            List<Model.Member> memberList_ByBoat = new List<Model.Member>();

            foreach(var Member in memberList_ByName)
            {
                bool memberGotMatchingBoat = this.isBoatOnList(searchCriteria, Member.Boats);

                List<Model.Boat> listWithMatchingBoat = this.getBoatList(searchCriteria, Member.Boats);

                if (memberGotMatchingBoat)
                {
                    Member.Boats = listWithMatchingBoat;
                    memberList_ByBoat.Add(Member);
                }
            }

            return memberList_ByBoat;
        }

        public List<Model.Member> LoadMemberList() 
        {
            List<Model.Member> deserializedMemberlist = JsonConvert.DeserializeObject<List<Model.Member>>(File.ReadAllText(@filePath()));

            return deserializedMemberlist;
        }

        public void saveToFile(List<Model.Member> memberListToBeSaved)
        {
            var json = JsonConvert.SerializeObject(memberListToBeSaved, Formatting.Indented);
            File.WriteAllText(this.filePath(), json);          
        }

        public bool fileExists(string FileName) 
        {
            try 
            {
                FileInfo fInfo = new FileInfo(FileName);

                if (!fInfo.Exists)
                {
                    throw new FileNotFoundException();
                }

                return true;
            }   catch (Exception)
                {
                    return false;
                }
        }

        public string filePath(){
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var path = Path.Combine(systemPath , "JackSparrowBoatClub");
            return path;
        }

        private bool isBoatOnList(Model.SearchMember searchCriteria, List<Model.Boat> boatList)
        {
            if (boatList.Count > 0)
            {
                foreach (Model.Boat Boat in boatList.Where(boat => boat.BoatType.Equals(searchCriteria.BoatType)))
                {
                    return true;
                }               
            } else {
                return false;
            }
            
            return false;
        }

        private List<Model.Boat> getBoatList(Model.SearchMember searchCriteria, List<Model.Boat> boatList)
        {
            List<Model.Boat> listOfMatchingBoatType = new List<Model.Boat>();
            if (boatList.Count > 0)
            {
                foreach (Model.Boat Boat in boatList.Where(boat => boat.BoatType.Equals(searchCriteria.BoatType)))
                {
                    listOfMatchingBoatType.Add(Boat);
                }               
            } else {
                return null;
            }
            
            return listOfMatchingBoatType;
        }
    }
}