using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchCharacterPersonalnumberStrategy : ISearchCharacterStrategy
    {
        public List<Model.Member> characterSearch(Model.SearchMember a_searchCriteria, List<Model.Member> a_memberList)
        {
            List<Model.Member> memberList = a_memberList;
            List<Model.Member> memberList_ByPersonalnumber = new List<Model.Member>();

            foreach(var Member in memberList.Where(member => member.PersonalNumber.ToLower().Contains(a_searchCriteria.SearchString.ToLower())))
            {
                memberList_ByPersonalnumber.Add(Member);
            }

            return memberList_ByPersonalnumber;
        }
    }
}