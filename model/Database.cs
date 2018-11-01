using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Model
{
    public abstract class Database
    {
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
    }
}