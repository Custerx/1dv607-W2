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
        private Model.CreateMember _createMember;

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
            this._createMember = new Model.CreateMember();
            this._memberView = memberView;
            if (base.fileExists(base.filePath()) == false) // No file -> creates a new file and user have to register a new member.
            {
                var memberList = new Test.MemberList();
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
            
            Model.Member member = this._createMember.create(username, personalNumber, password);
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

        public int authorization()
        {
            string username = this._memberView.getUsernameInput("Username: ");
            string password = this._memberView.getMemberPasswordInput("Password: ");

            Model.Member member = this.searchForMemberByName(username);

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

        public void searchAndViewMembersByNameBoatType()
        {
            List<Model.Member> memberList_ByName = this.searchAndViewMembersByName(true);
            
            if (memberList_ByName == null)
            {
                return;
            }

            int boatTypeAsNumber = this._memberView.getBoatTypeInput();
            Enums.BoatTypes.Boats boatType = (Enums.BoatTypes.Boats)Convert.ToInt32(boatTypeAsNumber);

            List<Model.Member> memberList_ByNameAndBoatType = base.getList_Member_NameAndBoatType(new Model.SearchMember{BoatType = boatType}, memberList_ByName);

            if (memberList_ByNameAndBoatType.Count < 1)
            {
                this._memberView.messageForError("No member matched with your boat type.");
                return;
            } else
            {
                this._memberView.messageForSuccess("Following member(s) matched with your search criteria(s).");
                this._memberView.viewVerboseList(memberList_ByNameAndBoatType);
            }
        }

        public void searchAndViewMembersByAge()
        {
            string personalNumber = this._memberView.getPersonalnumberInput("Get members according to age. Type personal-number to be set as reference, example [198907076154]: ");
            bool younger = this._memberView.getBoolInput();

            List<Model.Member> memberList = base.getList_Member_Age(new Model.SearchMember{PersonalNumber = personalNumber}, younger);

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

        public List<Model.Member> searchAndViewMembersByName(bool extendedSearch)
        {
            string searchString = this._memberView.getSearchInput("Get all username(s) with character(s): ");
            List<Model.Member> memberList = base.getList_Member_Name(new Model.SearchMember{SearchString = searchString});

            if (memberList.Count < 1)
            {
                this._memberView.messageForError("No username matched with your character(s).");
                return null;
            }

            if (extendedSearch)
            {
                return memberList;
            } else
            {
                this._memberView.messageForSuccess("Following username(s) matched with your character(s).");
                this._memberView.viewVerboseList(memberList);

                return null;
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
            Model.Member member = this.searchForMemberByName(username);
            
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

        private Model.Member searchForMemberByName(string name)
        {
            Model.Member Member = base.getMemberByName(new Model.SearchMember{Name = name});
            return Member;
        }

        private bool verifyPersonalNumber(string personalNumber)
        {
            Model.Member member = this.searchForMemberByPersonalNumber(personalNumber);
            
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

        private Model.Member searchForMemberByPersonalNumber(string personalNumber)
        {
            Model.Member Member = base.getMemberByPersonalNumber(new Model.SearchMember{PersonalNumber = personalNumber});
            return Member;
        }
    }
}