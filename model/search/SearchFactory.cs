namespace Model.Search
{
    public class SearchFactory
    {
        public ISearchGenericStrategy getGenericCharacterSearch()
        {
            return new CharacterSearchStrategy();
        }

        public ISearchMultipleStrategy getMultipleNameAndBoatSearch()
        {
            return new NameAndBoatSearchStrategy();
        }

        public ISearchCompareAgeStrategy getCompareAgeSearch()
        {
            return new CompareAgeStrategy();
        }

        public ISearchUniqueStrategy getUniqueMemberSearch()
        {
            return new NameSearchStrategy();
        }

        public ISearchUniqueStrategy getUniquePersonalnumberSearch()
        {
            return new PersonalnumberSearchStrategy();
        }
    }
}