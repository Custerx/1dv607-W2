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
                base.saveToFile(new List<Model.Member>());

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
            string username = this._memberView.getUsernameInput("Chose username: ");
            
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

            string personalNumber = this._memberView.getPersonalnumberInput("Type the personal-number, example [198907076154]: ");
            string password = this._memberView.getMemberPasswordInput("Chose password: ");

            this._memberModelList.Add(new Model.Member(username, personalNumber, base.RandomID(), password));
            base.saveToFile(this._memberModelList); 
            
            this._memberView.messageForSuccess("Member successfully registered! Please login.");              
        }

        public void DeleteMemberFromList() 
        {
            string id = this._memberView.getIDInput("(Your ID: " + this.MemberID + ")" + " Type the 6-character ID on the member to be removed: ");

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully deleted!");
                    base.saveToFile(this._memberModelList);
                    return;
                }
            }
            
            this._memberView.messageForError("No matching member!");
        }
        public void UpdateMemberOnList() {
            string id = this._memberView.getIDInput("(Your ID: " + this.MemberID + ")" + " Type 6-character ID on the member to be updated: ");

            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }

            for (int i = 0; i < this._memberModelList.Count; i++)
            {
                if (this._memberModelList[i].MemberID == id) {
                    this._memberModelList.RemoveAt(i);
                    base.saveToFile(this._memberModelList);
                    this.registerMemberOnList();
                    this._memberView.messageForSuccess("Member " + this._memberModelList[i].Name + " successfully updated!");
                    return;
                }
            }

            this._memberView.messageForError("Member not found!");
        }

        public int Authorization()
        {
            string username = this._memberView.getUsernameInput("Username: ");
            string password = this._memberView.getMemberPasswordInput("Password: ");

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

        public void SearchAndViewMembersByAge()
        {
            string personalNumber = this._memberView.getPersonalnumberInput("Get members according to age. Type personal-number to be set as reference, example [198907076154]: ");
            bool younger = this._memberView.getBoolInput();

            List<Model.Member> memberList = base.getListMemberByAge(new Model.SearchMember{PersonalNumber = personalNumber}, younger);

            if (memberList.Count < 1)
            {
                if (younger)
                {
                    this._memberView.messageForError("No members are younger than your personalnumber.");
                } else
                {
                    this._memberView.messageForError("No members are older than your personalnumber.");
                }

                return;
            } else
            {
                if (younger)
                {
                    this._memberView.messageForSuccess("Following members are younger than your personalnumber.");
                } else
                {
                    this._memberView.messageForSuccess("Following members are older than your personalnumber.");
                }
            }

            this._memberView.viewVerboseList(memberList);
        }

        public void SearchAndViewMembersByName()
        {
            string searchString = this._memberView.getSearchInput("Get all username with character(s): ");
            List<Model.Member> memberList = base.getListMemberByName(new Model.SearchMember{SearchString = searchString});

            if (memberList.Count < 1)
            {
                this._memberView.messageForError("No username matched with your character(s).");
                return;
            }
            this._memberView.messageForSuccess("Following username(s) matched with your character(s).");

            this._memberView.viewVerboseList(memberList);
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

        private Model.Member SearchForMemberByName(string name)
        {
            Model.Member Member = base.getMemberByName(new Model.SearchMember{Name = name});
            return Member;
        }
    }
}