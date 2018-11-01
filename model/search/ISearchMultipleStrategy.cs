using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchMultipleStrategy
    {
        List<Model.Member> multipleSearch(Model.SearchMember searchCriteria, List<Model.Member> memberList_ByName);
    }
}