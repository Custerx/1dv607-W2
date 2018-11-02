using System;
using System.Collections.Generic;

namespace Controller
{
    public class SearchController : Model.Database
    {
        private View.MemberView _memberView;
        private Model.Search.SearchFactory _searchFactory;
        private Model.Search.ISearchMultipleStrategy _searchNameAndBoat;
        private Model.Search.ISearchCompareAgeStrategy _compareAgeSearch;
        private Model.Search.ISearchCharacterStrategy _searchCharacterUsername;
        private Model.Search.ISearchCharacterStrategy _searchCharacterPersonalnumber;
        private Model.Search.ISearchUniqueStrategy _uniqueNameSearch;
        private Model.Search.ISearchUniqueStrategy _uniquePersonalnumberSearch;

        public SearchController()
        {
            this._memberView = new View.MemberView();
            this._searchFactory = new Model.Search.SearchFactory();
            // Creating specific search patterns.
            this._searchNameAndBoat = _searchFactory.getMultipleSearch_NameAndBoat();
            this._compareAgeSearch = _searchFactory.getCompareAgeSearch();
            this._searchCharacterUsername = _searchFactory.getCharacterUsernameSearch();
            this._searchCharacterPersonalnumber = _searchFactory.getCharacterPersonalnumberSearch();
            this._uniqueNameSearch = _searchFactory.getUniqueNameSearch();
            this._uniquePersonalnumberSearch = _searchFactory.getUniquePersonalnumberSearch();
        }

        public void hardcodedGrade4Example()
        {
            this.complexSearchGrade4("r", "1988", "199406043654", false, Enums.BoatTypes.Boats.Sailboat); // Hardcoded.
            this.complexSearchGrade4("e", "197", "199406043654", false, Enums.BoatTypes.Boats.Motorsailer); // Hardcoded.
        }

        public void complexSearchGrade4(string a_username, string a_birthYear, string a_personalNumber, bool a_younger, Enums.BoatTypes.Boats a_boatType)
        {
            List<Model.Member> completeMemberList = base.LoadMemberList();      
            List<Model.Member> firstMatchMemberList = this._searchCharacterUsername.characterSearch(new Model.SearchMember{SearchString = a_username}, completeMemberList);

            if (firstMatchMemberList.Count < 1)
            {
                this._memberView.messageForError("No username matched with your character(s).");
                return;
            }
         
            List<Model.Member> secondMatchMemberList = this._searchCharacterPersonalnumber.characterSearch(new Model.SearchMember{SearchString = a_birthYear}, firstMatchMemberList);

            if (secondMatchMemberList.Count < 1)
            {
                this._memberView.messageForError("No personalnumber matched with your character(s).");
                return;
            }

            List<Model.Member> thirdMatchMemberList = this._compareAgeSearch.compareAgeSearch(new Model.SearchMember{PersonalNumber = a_personalNumber}, a_younger, secondMatchMemberList);

            if (thirdMatchMemberList.Count < 1)
            {
                if (a_younger)
                {
                    this._memberView.messageForError("No members are younger than your personalnumber.");
                } else
                {
                    this._memberView.messageForError("No members are older than your personalnumber.");
                }

                return;
            }

            List<Model.Member> fourthMatchMemberList = this._searchNameAndBoat.multipleSearch(new Model.SearchMember{BoatType = a_boatType}, thirdMatchMemberList);

            if (fourthMatchMemberList.Count < 1)
            {
                this._memberView.messageForError("No member matched with your boat type.");
                return;
            } else
            {
                this._memberView.messageForSuccess("Following member(s) matched with your search criteria(s).");
                this._memberView.viewVerboseList(fourthMatchMemberList);
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

            List<Model.Member> memberList_ByNameAndBoatType = this._searchNameAndBoat.multipleSearch(new Model.SearchMember{BoatType = boatType}, memberList_ByName);

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

            List<Model.Member> completeMemberList = base.LoadMemberList();
            List<Model.Member> memberList = this._compareAgeSearch.compareAgeSearch(new Model.SearchMember{PersonalNumber = personalNumber}, younger, completeMemberList);

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
            
            List<Model.Member> completeMemberList = base.LoadMemberList();
            List<Model.Member> memberList = this._searchCharacterUsername.characterSearch(new Model.SearchMember{SearchString = searchString}, completeMemberList);

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

        public Model.Member searchForMemberByName(string name)
        {
            List<Model.Member> memberList = base.LoadMemberList();
            Model.Member Member = this._uniqueNameSearch.uniqueSearch(new Model.SearchMember{Name = name}, memberList);
            return Member;
        }

        public Model.Member searchForMemberByPersonalNumber(string personalNumber)
        {
            List<Model.Member> memberList = base.LoadMemberList();
            Model.Member Member = this._uniquePersonalnumberSearch.uniqueSearch(new Model.SearchMember{PersonalNumber = personalNumber}, memberList);
            return Member;
        }
    }
}