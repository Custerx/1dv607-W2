using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchUniqueNameStrategy : Model.Database, ISearchUniqueStrategy
    {
        public Model.Member uniqueSearch(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = base.LoadMemberList();

            foreach(var Member in memberList.Where(member => member.Name.Equals(searchCriteria.Name)))
            {
                return Member;
            }

            return null;           
        }
    }
}