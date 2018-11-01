using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class CharacterSearchStrategy : Model.Database, ISearchGenericStrategy
    {
        public List<Model.Member> genericSearch(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = base.LoadMemberList();
            List<Model.Member> memberList_ByName = new List<Model.Member>();

            foreach(var Member in memberList.Where(member => member.Name.ToLower().Contains(searchCriteria.SearchString.ToLower())))
            {
                memberList_ByName.Add(Member);
            }

            return memberList_ByName;
        }
    }
}