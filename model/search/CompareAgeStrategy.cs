using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class CompareAgeStrategy : ISearchCompareAgeStrategy
    {
        private int _thisYear = 2018; // Used to calculate age from personalnumber.

        public List<Model.Member> compareAgeSearch(Model.SearchMember a_searchCriteria, bool younger, List<Model.Member> a_memberList)
        {
            List<Model.Member> memberList = a_memberList;
            List<Model.Member> memberList_ByAge = new List<Model.Member>();

            if (younger)
            {
                foreach(var Member in memberList.Where(member => (_thisYear - int.Parse(member.PersonalNumber.Substring(0, 4))) < 
                    (_thisYear - int.Parse(a_searchCriteria.PersonalNumber.Substring(0, 4)))))
                {
                    memberList_ByAge.Add(Member);
                }
            } else
            {
                foreach(var Member in memberList.Where(member => (_thisYear - int.Parse(member.PersonalNumber.Substring(0, 4))) > 
                    (_thisYear - int.Parse(a_searchCriteria.PersonalNumber.Substring(0, 4)))))
                {
                    memberList_ByAge.Add(Member);
                }
            }

            return memberList_ByAge;
        }
    }
}