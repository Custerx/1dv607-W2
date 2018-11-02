using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchCharacterStrategy
    {
        List<Model.Member> characterSearch(Model.SearchMember searchCriteria);
    }
}