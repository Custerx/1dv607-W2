using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchGenericStrategy
    {
        List<Model.Member> genericSearch(Model.SearchMember searchCriteria);
    }
}