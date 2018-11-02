using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchUniquePersonalnumberStrategy : ISearchUniqueStrategy
    {
        public Model.Member uniqueSearch(Model.SearchMember a_searchCriteria, List<Model.Member> a_memberList)
        {
            List<Model.Member> memberList = a_memberList;

            foreach(var Member in memberList.Where(member => member.PersonalNumber.Equals(a_searchCriteria.PersonalNumber)))
            {
                return Member;
            }

            return null;
        }
    }
}