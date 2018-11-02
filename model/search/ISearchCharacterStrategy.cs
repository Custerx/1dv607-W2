using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchCharacterStrategy
    {
        List<Model.Member> characterSearch(Model.SearchMember a_searchCriteria, List<Model.Member> a_memberList);
    }
}