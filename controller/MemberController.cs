using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Controller
{
    public class MemberController
    {
        private string _memberID;
        private View.MemberView _memberView;
        private Model.MemberFactory _memberFactory;
        private Controller.SearchController _searchController;
        private Model.Database _database;

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
            this._database = new Model.Database();
            this._searchController = a_searchController;

            if (_database.fileExists(_database.filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                var memberList = new Test.MemberList(this._memberFactory);
                this._database.CreateTestMembers(memberList);
                
                this._memberView.messageForSuccess("New file created at: " + this._database.filePath() + " with 50 members and 4 boats each.");
                
                this.registerMemberOnList();
            }
        }

        public void verboseList()
        {
            this._memberView.viewVerboseList(this._database.LoadMemberList());
        }

        public void compactList()
        {
            this._memberView.viewCompactList(this._database.LoadMemberList());
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

            string password = this._memberView.getMemberPasswordInput("Chose password: ");
            
            Model.Member member = this._memberFactory.Create(username, personalNumber, password);
            this._database.saveToFile(member);
            
            this._memberView.messageForSuccess("Member successfully registered! Please login.");              
        }

        public void deleteMemberFromList() 
        {
            string id = this._memberView.getIDInput("(Your ID: " + this.MemberID + ")" + " Type the 6-character ID on the member to be removed: ");

            bool delete = this._database.deleteMemberFromList(id);

            if(delete)
            {
                this._memberView.messageForSuccess("Member successfully deleted!");
            } else
            {
                this._memberView.messageForError("No matching member!");
            }
        }

        public void updateMemberOnList() {
            string id = this._memberView.getIDInput("(Your ID: " + this.MemberID + ")" + " Type 6-character ID on the member to be updated: ");

            bool delete = this._database.deleteMemberFromList(id);

            if (delete)
            {
                this.registerMemberOnList();
                this._memberView.messageForSuccess("Member successfully updated!");
            }
            else
            {
                this._memberView.messageForError("Member not found!");
            }
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