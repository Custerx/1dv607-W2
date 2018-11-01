using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchUniqueStrategy
    {
        Model.Member uniqueSearch(Model.SearchMember searchCriteria);
    }
}