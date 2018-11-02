namespace Model.Search
{
    public class SearchFactory
    {
        public ISearchCharacterStrategy getCharacterUsernameSearch()
        {
            return new SearchCharacterUsernameStrategy();
        }

        public ISearchCharacterStrategy getCharacterPersonalnumberSearch()
        {
            return new SearchCharacterPersonalnumberStrategy();
        }

        public ISearchMultipleStrategy getMultipleSearch_NameAndBoat()
        {
            return new SearchNameAndBoatSearchStrategy();
        }

        public ISearchCompareAgeStrategy getCompareAgeSearch()
        {
            return new CompareAgeStrategy();
        }

        public ISearchUniqueStrategy getUniqueNameSearch()
        {
            return new SearchUniqueNameStrategy();
        }

        public ISearchUniqueStrategy getUniquePersonalnumberSearch()
        {
            return new SearchUniquePersonalnumberStrategy();
        }
    }
}