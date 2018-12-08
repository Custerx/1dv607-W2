using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Model
{
    public class Database
    {
        public void registerBoatOnList(string memberID, Model.Boat boat)
        {
            List<Model.Member> database = LoadMemberList();

            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].MemberID == memberID)
                {
                    database[i].Boats.Add(boat);
                    saveAllToFile(database);
                }
            }
        }

        public bool memberExists(string memberID)
        {
            List<Model.Member> database = LoadMemberList();

            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].MemberID == memberID)
                {
                    return true;
                }
            }

            return false;
        }

        public bool deleteMemberFromList(string memberID)
        {
            List<Model.Member> database = LoadMemberList();

            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].MemberID == memberID)
                {
                    database.RemoveAt(i);
                    saveAllToFile(database);
                    return true;
                }
            }

            return false;
        }

        public void CreateTestMembers(Test.MemberList a_testList)
        {
            List<Model.Member> memberListForTesting = a_testList.create50membersAnd200Boats();
            var json = JsonConvert.SerializeObject(memberListForTesting, Formatting.Indented);
            File.WriteAllText(this.filePath(), json);
        }

        public List<Model.Member> LoadMemberList() 
        {

            List<Model.Member> deserializedMemberlist = JsonConvert.DeserializeObject<List<Model.Member>>(File.ReadAllText(@filePath()));
            return deserializedMemberlist;
        }

        public void saveToFile(Model.Member memberToBeSaved)
        {
            List<Model.Member> database = LoadMemberList();
            database.Add(memberToBeSaved);
            var json = JsonConvert.SerializeObject(database, Formatting.Indented);
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

        public void saveAllToFile(List<Model.Member> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(this.filePath(), json);
        }
    }
}