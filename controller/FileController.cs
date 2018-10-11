using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public abstract class FileController : ISearchController
    {
        private static Random random = new Random();
        private int _thisYear = 2018;    

        public Model.Member getMemberByName(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = this.LoadMemberList();

            foreach(var Member in memberList.Where(member => member.Name.Equals(searchCriteria.Name)))
            {
                return Member;
            }

            return null;
        }

        public List<Model.Member> getListMemberByAge(Model.SearchMember searchCriteria, bool younger)
        {
            List<Model.Member> memberList = this.LoadMemberList();
            List<Model.Member> memberListByAge = new List<Model.Member>();

            if (younger)
            {
                foreach(var Member in memberList.Where(member => (_thisYear - int.Parse(member.PersonalNumber.Substring(0, 4))) < (_thisYear - int.Parse(searchCriteria.PersonalNumber.Substring(0, 4)))))
                {
                    memberListByAge.Add(Member);
                }
            } else
            {
                foreach(var Member in memberList.Where(member => (_thisYear - int.Parse(member.PersonalNumber.Substring(0, 4))) > (_thisYear - int.Parse(searchCriteria.PersonalNumber.Substring(0, 4)))))
                {
                    memberListByAge.Add(Member);
                }
            }

            return memberListByAge;
        }

        public List<Model.Member> getListMemberByName(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = this.LoadMemberList();
            List<Model.Member> memberListByName = new List<Model.Member>();

            foreach(var Member in memberList.Where(member => member.Name.ToLower().Contains(searchCriteria.SearchString.ToLower())))
            {
                memberListByName.Add(Member);
            }

            return memberListByName;
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
        public string RandomID(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}