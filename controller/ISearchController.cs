using System.Collections.Generic;

namespace Controller
{
    public interface ISearchController
    {
        Model.Member getMemberByName(Model.SearchMember searchCriteria);
        Model.Member getMemberById(Model.SearchMember searchCriteria);
        List<Model.Member> getListMemberByAge(Model.SearchMember searchCriteria);
        List<Model.Member> getListMemberByName(Model.SearchMember searchCriteria);
    }
}