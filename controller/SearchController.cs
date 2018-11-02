using System;
using System.Collections.Generic;

namespace Controller
{
    public class SearchController
    {
        private View.MemberView _memberView;
        private Model.Search.SearchFactory _searchFactory;
        private Model.Search.ISearchMultipleStrategy _searchNameAndBoat;
        private Model.Search.ISearchCompareAgeStrategy _compareAgeSearch;
        private Model.Search.ISearchCharacterStrategy _searchCharacterUsername;
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
            this._uniqueNameSearch = _searchFactory.getUniqueNameSearch();
            this._uniquePersonalnumberSearch = _searchFactory.getUniquePersonalnumberSearch();
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

            List<Model.Member> memberList = this._compareAgeSearch.compareAgeSearch(new Model.SearchMember{PersonalNumber = personalNumber}, younger);

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
            
            List<Model.Member> memberList = this._searchCharacterUsername.characterSearch(new Model.SearchMember{SearchString = searchString});

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
            Model.Member Member = this._uniqueNameSearch.uniqueSearch(new Model.SearchMember{Name = name});
            return Member;
        }

        public Model.Member searchForMemberByPersonalNumber(string personalNumber)
        {
            Model.Member Member = this._uniquePersonalnumberSearch.uniqueSearch(new Model.SearchMember{PersonalNumber = personalNumber});
            return Member;
        }
    }
}