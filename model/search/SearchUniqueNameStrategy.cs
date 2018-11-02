using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchUniqueNameStrategy : ISearchUniqueStrategy
    {
        public Model.Member uniqueSearch(Model.SearchMember a_searchCriteria, List<Model.Member> a_memberList)
        {
            List<Model.Member> memberList = a_memberList;

            foreach(var Member in memberList.Where(member => member.Name.Equals(a_searchCriteria.Name)))
            {
                return Member;
            }

            return null;           
        }
    }
}