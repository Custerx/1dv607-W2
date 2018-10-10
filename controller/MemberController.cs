using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public class MemberController : FileController
    {
        private string _memberID;
        private View.MemberView _memberView;
        private List<Model.Member> _memberModelList;
        public string MemberID
        { 
            get => _memberID; 
            private set 
            {
                if (value.Length != 6)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _memberID = value;
            }            
        }
        public MemberController(View.MemberView memberView)
        {
            this._memberView = memberView;
            if (base.fileExists(base.filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                this._memberModelList = new List<Model.Member>();
                var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                File.WriteAllText(filePath(), json);

                this._memberView.messageForSuccess("New file created at: " + base.filePath());
                
                this.registerMemberOnList();
            }
        }

        public void verboseList()
        {
            List<Model.Member> memberList = base.LoadMemberList();
            this._memberView.viewVerboseList(memberList);
        }

        public void compactList()
        {
            List<Model.Member> memberList = base.LoadMemberList();
            this._memberView.viewCompactList(memberList);
        }
        public void registerMemberOnList() 
        {
            string username = this._memberView.ReadUsernameInput("Chose username: ");
            
            if (this.verifyUserName(username) == true)
            {
                this._memberView.messageForError("Username already taken.");
                this.registerMemberOnList();
                return;
            }

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            long personalNumber = this._memberView.ReadPersonalnumberInput("Type the personal-number, example [8907076154]: ");
            string password = this._memberView.ReadMemberPasswordInput("Chose password: ");

            this._memberModelList.Add(new Model.Member(username, personalNumber, base.RandomID(), password));
            
            this._memberView.messageForSuccess("Member successfully registered! Please login.");
                
            var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
            File.WriteAllText(filePath(), json);
        }

        private Model.Member SearchForMemberByName(string name)
        {
            Model.Member Member = base.getMemberByName(new Model.SearchMember{Name = name});
            return Member;
        }

        private List<Model.Member> SearchForMembersByAge(int personalNumber)
        {
            List<Model.Member> memberList = base.getListMemberByAge(new Model.SearchMember{PersonalNumber = personalNumber});
            return memberList;
        }

        public void SearchAndDisplayMembersByName()
        {
            string searchString = this._memberView.getSearchInput("Get all username with character(s): ");
            List<Model.Member> memberList = base.getListMemberByName(new Model.SearchMember{SearchString = searchString});

            if (memberList.Count < 1)
            {
                this._memberView.messageForError("No username matched with your character(s).");
                return;
            }

            this._memberView.viewCompactList(memberList);
        }

        private bool verifyUserName(string username)
        {
            Model.Member member = this.SearchForMemberByName(username);
            
            if (member == null)
            {
                return false;
            } 
            
            if (member.Name.Equals(username))
            {
                return true;
            } else
            {
                return false;
            }
        }

        public int Authorization()
        {
            string username = this._memberView.ReadUsernameInput("Username: ");
            string password = this._memberView.ReadMemberPasswordInput("Password: ");

            Model.Member member = this.SearchForMemberByName(username);

            if (member == null)
            {
                this._memberView.messageForError("Wrong username or password.");
                return 0;
            } else if (member.Password.Equals(password))
            {
                this._memberView.messageForSuccess("Welcome " + username + "!");
                this.MemberID = member.MemberID;
                return 1;
            } else
            {
                this._memberView.messageForError("Wrong username or password.");
                return 0;
            }
        }

        public void DeleteMemberFromList() 
        {
            string id = this._memberView.ReadMemberIDInput("(Your ID: " + this.MemberID + ")" + " Type the 6-character ID on the member to be removed: ");

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully deleted!");
                    var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                    File.WriteAllText(base.filePath(), json);
                    return;
                }
            }
            
            this._memberView.messageForError("No matching member!");
        }
        public void UpdateMemberOnList() {
            string id = this._memberView.ReadMemberIDInput("(Your ID: " + this.MemberID + ")" + " Type 6-character ID on the member to be updated: ");

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    var json = JsonConvert.SerializeObject(this._memberModelList, Formatting.Indented);
                    File.WriteAllText(base.filePath(), json);
                    this.registerMemberOnList();
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully updated!");
                    return;
                }
            }

            this._memberView.messageForError("Member not found!");
        }
    }
}