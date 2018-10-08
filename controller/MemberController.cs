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
        private List<Model.Member> _memberModelList;
        public MemberController(View.MemberView memberView)
        {
            this._memberView = memberView;
            if (fileExists(filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                this._memberView.messageForSuccess("New file created at: " + filePath());
                this._memberModelList = new List<Model.Member>();
                this.SaveMemberList();
            }
        }
        public void SaveMemberList() 
        {
            string username = this._memberView.ReadUsernameInput("Chose username: ");
            long personalNumber = this._memberView.ReadPersonalnumberInput("Type the personal-number, example [8907076154]: ");

            if (fileExists(filePath()) == true)
            {
                this._memberModelList = LoadMemberList();
            }

            this._memberModelList.Add(new Model.Member(username, personalNumber, RandomID()));

            this._memberView.messageForSuccess("Member successfully registered!");
                
            var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
            File.WriteAllText(filePath(), json);
        }

        public List<Model.Member> LoadMemberList() 
        {
            List<Model.Member> deserializedMemberlist = JsonConvert.DeserializeObject<List<Model.Member>>(File.ReadAllText(@filePath()));
            if (deserializedMemberlist.Count == 0) 
            {
                this._memberView.messageForError("No members found on file. Please start with register a new member.");
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
                    return false;
                }
        }
        public void DeleteMemberFromList() 
        {
            string id = this._memberView.ReadMemberIDInput("6-character ID on the member to be removed: ");

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully deleted!");
                    var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                    File.WriteAllText(filePath(), json);
                } else {
                    if (i == this._memberModelList.Count && this._memberModelList.Count != 1) 
                    {
                        this._memberView.messageForError("No matching member!");
                    }
                }
            }
        }
        public void UpdateMemberOnList() {
            string id = this._memberView.ReadMemberIDInput("6-character ID on the member to be updated: ");

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                    File.WriteAllText(filePath(), json);
                    SaveMemberList();
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully updated!");
                } else {
                    if (i == this._memberModelList.Count && this._memberModelList.Count != 1) 
                    {
                        this._memberView.messageForError("Member not found!");
                    }
                }
            }
        }
        public string filePath(){
            var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            var path = Path.Combine(systemPath , "JackSparrowBoatClub"); // C:\ProgramData\files. Later for app try "YourAppName\\YourApp.exe".
            return path;
        }
        public string RandomID(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}