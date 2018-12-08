namespace Model
{
    public class MemberFactory
    {
        private Model.ID _ID;

        public MemberFactory()
        {
            _ID = new Model.ID();
        }

        public Model.Member Create(string username, string personalNumber, string password)
        {
            return new Model.Member(username, personalNumber, _ID.createID(), password);
        }
    }
}