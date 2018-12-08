using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public class MemberController : Model.Database
    {
        private string _memberID;
        private View.MemberView _memberView;
        private List<Model.Member> _memberModelList;
        private Model.MemberFactory _memberFactory;
        private Controller.SearchController _searchController;

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

        public enum Login
        {
            Success,
            Failed,
            Invalid
        }

        public MemberController(Controller.SearchController a_searchController)
        {
            this._memberFactory = new Model.MemberFactory();
            this._memberView = new View.MemberView();
            this._searchController = a_searchController;

            if (base.fileExists(base.filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                var memberList = new Test.MemberList(this._memberFactory);
                List<Model.Member> memberListForTesting = memberList.create50membersAnd200Boats();

                base.saveToFile(memberListForTesting);
                
                this._memberView.messageForSuccess("New file created at: " + base.filePath() + " with 50 members and 4 boats each.");
                
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

            string personalNumber = this._memberView.getPersonalnumberInput("Type the personal-number, example [198907076154]: ");
            if (this.verifyPersonalNumber(personalNumber) == true)
            {
                this._memberView.messageForError("Personal-number already taken.");
                this.registerMemberOnList();
                return;
            }          

            this.loadMemberListFromFile();

            string password = this._memberView.getMemberPasswordInput("Chose password: ");
            
            Model.Member member = this._memberFactory.Create(username, personalNumber, password);
            this._memberModelList.Add(member);
            base.saveToFile(this._memberModelList); 
            
            this._memberView.messageForSuccess("Member successfully registered! Please login.");              
        }

        public void deleteMemberFromList() 
        {
            string id = this._memberView.getIDInput("(Your ID: " + this.MemberID + ")" + " Type the 6-character ID on the member to be removed: ");

            this.loadMemberListFromFile();

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

        public void updateMemberOnList() {
            string id = this._memberView.getIDInput("(Your ID: " + this.MemberID + ")" + " Type 6-character ID on the member to be updated: ");

            this.loadMemberListFromFile();

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

        public Login authorization()
        {
            string username = this._memberView.getUsernameInput("Username: ");
            string password = this._memberView.getMemberPasswordInput("Password: ");

            Model.Member member = this._searchController.searchForMemberByName(username);

            if (member == null)
            {
                this._memberView.messageForError("Wrong username or password.");
                return Login.Invalid;
            } else if (member.Password.Equals(password))
            {
                this._memberView.messageForSuccess("Welcome " + username + "!");
                this.MemberID = member.MemberID;
                return Login.Success;
            } else
            {
                this._memberView.messageForError("Wrong username or password.");
                return Login.Failed;
            }
        }

        private void loadMemberListFromFile()
        {
            if (base.fileExists(base.filePath()) == true)
            {
                this._memberModelList = base.LoadMemberList();
            }
        }

        private bool verifyUserName(string username)
        {
            Model.Member member = this._searchController.searchForMemberByName(username);
            
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

        private bool verifyPersonalNumber(string personalNumber)
        {
            Model.Member member = this._searchController.searchForMemberByPersonalNumber(personalNumber);
            
            if (member == null)
            {
                return false;
            } 
            
            if (member.PersonalNumber.Equals(personalNumber))
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}