using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public class MemberController : FileController
    {
        private View.MemberView _memberView;
        private List<Model.Member> _memberModelList;
        public MemberController()
        {
            this._memberView = new View.MemberView();
            if (base.fileExists(base.filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                this._memberView.messageForSuccess("New file created at: " + base.filePath());
                this._memberModelList = new List<Model.Member>();
                this.registerMemberOnList();
            }
        }
        public void registerMemberOnList() 
        {
            string username = this._memberView.ReadUsernameInput("Chose username: ");
            
            if (this.verifyUserName(username))
            {
                this._memberView.messageForError("Username already taken.");
                this.registerMemberOnList();
            }
            
            long personalNumber = this._memberView.ReadPersonalnumberInput("Type the personal-number, example [8907076154]: ");
            string password = this._memberView.ReadMemberPasswordInput("Chose password: ");

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            this._memberModelList.Add(new Model.Member(username, personalNumber, base.RandomID(), password));

            this._memberView.messageForSuccess("Member successfully registered!");
                
            var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
            File.WriteAllText(filePath(), json);
        }

        private bool verifyUserName(string username)
        {
            Model.Member member = this._memberView.SearchForMemberByName(username);

            if (member.Password.Equals(username))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void Authorization()
        {
            string username = this._memberView.ReadUsernameInput("Username: ");
            string password = this._memberView.ReadMemberPasswordInput("Password: ");

            Model.Member member = this._memberView.SearchForMemberByName(username);
            
            if (member.Password.Equals(password))
            {
                this._memberView.messageForSuccess("Welcome!" + username + ".");
                Console.WriteLine("User authorized.");
            } else
            {
                this._memberView.messageForError("Wrong username or password.");

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
                    File.WriteAllText(base.filePath(), json);
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
                    File.WriteAllText(base.filePath(), json);
                    this.registerMemberOnList();
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully updated!");
                } else {
                    if (i == this._memberModelList.Count && this._memberModelList.Count != 1) 
                    {
                        this._memberView.messageForError("Member not found!");
                    }
                }
            }
        }
    }
}