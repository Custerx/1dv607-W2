using System.Collections.Generic;

namespace Controller
{
    public interface ISearchController
    {
        Model.Member getMemberByName(Model.SearchMember searchCriteria);
        List<Model.Member> getList_Member_Age(Model.SearchMember searchCriteria, bool younger);
        List<Model.Member> getList_Member_Name(Model.SearchMember searchCriteria);
        List<Model.Member> getList_Member_NameAndBoatType(Model.SearchMember searchCriteria, List<Model.Member> memberList_ByName);
    }
}