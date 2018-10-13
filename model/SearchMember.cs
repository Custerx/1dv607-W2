using System.Collections.Generic;

namespace Model
{
    public class SearchMember
    {
        public string Name {get; set;}
        public string PersonalNumber {get; set;}
        public string MemberID {get; set;}
        public List<Model.Boat> Boats { get; set; }
        public Enums.BoatTypes.Boats BoatType { get; set; }
        public int Password { get; set; }
        public string SearchString { get; set; }
    }
}