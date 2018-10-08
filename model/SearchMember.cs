using System.Collections.Generic;

namespace Model
{
    public class SearchMember
    {
        public string Name {get; set;}
        public long PersonalNumber {get; set;}
        public string MemberID {get; set;}
        public List<Model.Boat> Boats { get; set; }
    }
}