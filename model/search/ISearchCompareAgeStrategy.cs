using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchCompareAgeStrategy
    {
        List<Model.Member> compareAgeSearch(Model.SearchMember a_searchCriteria, bool younger, List<Model.Member> a_memberList);
    }
}