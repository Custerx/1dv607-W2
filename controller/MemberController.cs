using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public class MemberController
    {
        private static Random random = new Random();
        private View.MemberView _memberView;
        public MemberController(View.MemberView memberView)
        {
            this._memberView = memberView;
        }
        public void SaveMemberList() 
        {
            string username = this._memberView.ReadUsernameInput("Chose username: ");
            long personalNumber = this._memberView.ReadPersonalnumberInput("Type the personal-number, example [8907076154]: ");

            List<Model.Member> MemberList = fileExists(filePath()) == true ? LoadMemberList() : new List<Model.Member>();
            MemberList.Add(new Model.Member(username, personalNumber, RandomID()));

            this._memberView.successfullMemberCreation();
                
            var json = JsonConvert.SerializeObject(MemberList, Formatting.Indented);
            File.WriteAllText(filePath(), json);
        }

        public List<Model.Member> LoadMemberList() 
        {
            List<Model.Member> deserializedMemberlist = JsonConvert.DeserializeObject<List<Model.Member>>(File.ReadAllText(@filePath()));
            if (deserializedMemberlist.Count == 0) 
            {
                this._memberView.unsuccessfullFileLoad();
            }
            return deserializedMemberlist;
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
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("\nError! The file was not found. " + FileName + "\n");
                    Console.ResetColor();
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("\nNew file created at: " + FileName + "\n");
                    Console.ResetColor();
                    return false;
                }
        }
        public void DeleteMemberFromList() 
        {
            List<Model.Member> viewMemberList = LoadMemberList();
            string id = this._memberView.ReadMemberIDInput("6-character ID on the member to be removed: ");

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                if (viewMemberList[i].MemberID == id) {
                    viewMemberList.RemoveAt(i);
                    this._memberView.successfullyDeleted(viewMemberList[i].Name);
                    var json = JsonConvert.SerializeObject(viewMemberList, Formatting.Indented);
                    File.WriteAllText(filePath(), json);
                } else {
                    if (i == viewMemberList.Count && viewMemberList.Count != 1) 
                    {
                        this._memberView.unsuccessfull();
                    }
                }
            }
        }
        public void UpdateMemberOnList() {
            List<Model.Member> viewMemberList = LoadMemberList();
            string id = this._memberView.ReadMemberIDInput("6-character ID on the member to be updated: ");

            for (int i = 0; i < viewMemberList.Count; i++)
            {
                if (viewMemberList[i].MemberID == id) {
                    viewMemberList.RemoveAt(i);
                    var json = JsonConvert.SerializeObject(viewMemberList, Formatting.Indented);
                    File.WriteAllText(filePath(), json);
                    SaveMemberList();
                    this._memberView.successfullyUpdated(viewMemberList[i].Name);
                } else {
                    if (i == viewMemberList.Count && viewMemberList.Count != 1) 
                    {
                        this._memberView.unsuccessfullUpdate();
                    }
                }
            }
        }
        public string filePath(){
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var path = Path.Combine(systemPath , "files"); // C:\ProgramData\files. Later for app try "YourAppName\\YourApp.exe".
            return path;
        }
        public string RandomID(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}