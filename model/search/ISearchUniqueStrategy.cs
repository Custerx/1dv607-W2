using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchUniqueStrategy
    {
        Model.Member uniqueSearch(Model.SearchMember a_searchCriteria, List<Model.Member> a_memberList);
    }
}