using System.Collections.Generic;
using System.Linq;

namespace Model.Search
{
    public class SearchNameAndBoatSearchStrategy : ISearchMultipleStrategy
    {
        public List<Model.Member> multipleSearch(Model.SearchMember searchCriteria, List<Model.Member> memberList_ByName)
        {
            List<Model.Member> memberList_ByBoat = new List<Model.Member>();

            foreach(var Member in memberList_ByName)
            {
                bool memberGotMatchingBoat = isBoatOnList(searchCriteria, Member.Boats);

                List<Model.Boat> listWithMatchingBoat = getBoatList(searchCriteria, Member.Boats);

                if (memberGotMatchingBoat)
                {
                    Member.Boats = listWithMatchingBoat;
                    memberList_ByBoat.Add(Member);
                }
            }

            return memberList_ByBoat;
        }

        private bool isBoatOnList(Model.SearchMember searchCriteria, List<Model.Boat> boatList)
        {
            if (boatList.Count > 0)
            {
                foreach (Model.Boat Boat in boatList.Where(boat => boat.BoatType.Equals(searchCriteria.BoatType)))
                {
                    return true;
                }               
            } else {
                return false;
            }
            
            return false;
        }

        private List<Model.Boat> getBoatList(Model.SearchMember searchCriteria, List<Model.Boat> boatList)
        {
            List<Model.Boat> listOfMatchingBoatType = new List<Model.Boat>();
            if (boatList.Count > 0)
            {
                foreach (Model.Boat Boat in boatList.Where(boat => boat.BoatType.Equals(searchCriteria.BoatType)))
                {
                    listOfMatchingBoatType.Add(Boat);
                }               
            } else {
                return null;
            }
            
            return listOfMatchingBoatType;
        }
    }
}