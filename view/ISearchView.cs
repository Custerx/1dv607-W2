using System.Collections.Generic;

namespace View
{
    public interface ISearchView
    {
        string getInputSearchString();
        Model.Member getMemberById(Model.SearchMember searchCriteria);
        List<Model.Member> getListMemberByAge(Model.SearchMember searchCriteria);
    }
    /*
    //use 
        ISearchView searchView = new ISearchView(); // Class instead of interface.
        Model.Member Member1 = searchView.getMemberById(new Model.SearchMember{Id = 5});
        List<Model.Member> memberList = searchView.GetListMemberByAge(new Model.SearchMember{PersonalNumber = 8899774455});
    */
}