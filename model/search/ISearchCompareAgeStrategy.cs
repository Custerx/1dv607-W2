using System.Collections.Generic;

namespace Model.Search
{
    public interface ISearchCompareAgeStrategy
    {
        List<Model.Member> compareAgeSearch(Model.SearchMember searchCriteria, bool younger);
    }
}