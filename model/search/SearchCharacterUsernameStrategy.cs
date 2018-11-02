using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchCharacterUsernameStrategy : Model.Database, ISearchCharacterStrategy
    {
        public List<Model.Member> characterSearch(Model.SearchMember searchCriteria)
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