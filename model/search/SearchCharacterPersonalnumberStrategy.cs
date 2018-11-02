using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchCharacterPersonalnumberStrategy : Model.Database, ISearchCharacterStrategy
    {
        public List<Model.Member> characterSearch(Model.SearchMember searchCriteria)
        {
            List<Model.Member> memberList = base.LoadMemberList();
            List<Model.Member> memberList_ByPersonalnumber = new List<Model.Member>();

            foreach(var Member in memberList.Where(member => member.PersonalNumber.ToLower().Contains(searchCriteria.SearchString.ToLower())))
            {
                memberList_ByPersonalnumber.Add(Member);
            }

            return memberList_ByPersonalnumber;
        }
    }
}