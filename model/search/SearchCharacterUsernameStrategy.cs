using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchCharacterUsernameStrategy : ISearchCharacterStrategy
    {
        public List<Model.Member> characterSearch(Model.SearchMember a_searchCriteria, List<Model.Member> a_memberList)
        {
            List<Model.Member> memberList = a_memberList;
            List<Model.Member> memberList_ByName = new List<Model.Member>();

            foreach(var Member in memberList.Where(member => member.Name.ToLower().Contains(a_searchCriteria.SearchString.ToLower())))
            {
                memberList_ByName.Add(Member);
            }

            return memberList_ByName;
        }
    }
}